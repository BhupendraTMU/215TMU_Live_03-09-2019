using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_BodyReception : System.Web.UI.Page
{
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    string cs = ConfigurationManager.ConnectionStrings["TMUCON"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
            BindCondition();
        }
    }
 
    // SAVE DONOR
    private string SaveFile(FileUpload fu, string prefix)
    {
        if (fu.HasFile)
        {
            string folderPath = Server.MapPath("~/Uploads/Cadavers/");

            if (!System.IO.Directory.Exists(folderPath))
                System.IO.Directory.CreateDirectory(folderPath);

            string fileName = prefix + "_" + DateTime.Now.Ticks + "_" + fu.FileName;
            string fullPath = folderPath + fileName;

            fu.SaveAs(fullPath);

            return "~/Uploads/Cadavers/" + fileName;
        }
        return null;
    }
    private DataTable GetCadaverData(string search = "")
    {
        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(cs))
        {
            string query = @"
SELECT 
    r.ReceptionId,
    c.CadaverId,
    c.CadaverCode,
    c.Name,

    d.DonorId,

    r.ArrivalDateTime,
    smsrc.SourceName,
    r.IdentificationDetails,
    r.ConsentDetails,
    r.WitnessAadhaar,
    r.CreatedAt,

    CASE 
        WHEN c.IsRegisteredDonor = 1 THEN 'Yes'
        ELSE 'No'
    END AS RegisteredDonor,

    c.DOB,
    c.Age,
    c.Gender,
    c.DateOfDeath,
    c.AadhaarNumber,
    c.TimeOfDeath,
    c.PlaceOfDeath,
    c.CauseOfDeath,

    CASE 
        WHEN c.StatusId IS NULL THEN 'Not Received'
        ELSE sms.StatusName
    END AS StatusName,

    ISNULL(cond.ConditionName, 'N/A') AS ConditionName

FROM CadaverReception r

INNER JOIN Cadavers c 
    ON r.CadaverId = c.CadaverId

LEFT JOIN BodyDonors d 
    ON c.DonorId = d.Id

LEFT JOIN SourceMaster smsrc 
    ON smsrc.SourceId = r.SourceId

LEFT JOIN StatusMaster sms 
    ON sms.StatusId = c.StatusId

LEFT JOIN (
    SELECT 
        ch.CadaverId, 
        cm.ConditionName,
        ROW_NUMBER() OVER 
        (
            PARTITION BY ch.CadaverId 
            ORDER BY ch.UpdatedAt DESC
        ) rn
    FROM CadaverConditionHistory ch
    INNER JOIN ConditionMaster cm 
        ON ch.ConditionIdRef = cm.ConditionId
) cond 
    ON cond.CadaverId = c.CadaverId 
    AND cond.rn = 1

WHERE
(
    @search IS NULL OR @search = ''
    OR c.CadaverCode LIKE '%' + @search + '%'
    OR c.AadhaarNumber LIKE '%' + @search + '%'
    OR c.Name LIKE '%' + @search + '%'
    OR CAST(d.DonorId AS NVARCHAR) LIKE '%' + @search + '%'
)

ORDER BY c.CreatedAt DESC";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@search", search);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        }

        return dt;
    }
    private void BindGrid()
    {
        DataTable dt = GetCadaverData();

        grdDonors.DataSource = dt;
        grdDonors.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string search = txtDonorNo.Text.Trim();

        DataTable dt = GetCadaverData(search);

        ViewState["SearchData"] = dt;

        grdDonors.PageIndex = 0;
        grdDonors.DataSource = dt;
        grdDonors.DataBind();
    }
    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        bool isPaging = grdDonors.AllowPaging;

        grdDonors.AllowPaging = false;

        DataTable dt;

        if (ViewState["SearchData"] != null)
        {
            dt = (DataTable)ViewState["SearchData"];
        }
        else
        {
            dt = GetCadaverData();
        }

        grdDonors.DataSource = dt;
        grdDonors.DataBind();

        // Hide Action column
        grdDonors.Columns[grdDonors.Columns.Count - 1].Visible = false;
        grdDonors.Columns[grdDonors.Columns.Count - 2].Visible = false;
        grdDonors.Columns[grdDonors.Columns.Count - 3].Visible = false;
        grdDonors.Columns[grdDonors.Columns.Count - 4].Visible = false;
        grdDonors.PagerSettings.Visible = false;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=CadaverList.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        grdDonors.RenderControl(hw);

        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();

        // Restore
        grdDonors.AllowPaging = isPaging;
        grdDonors.Columns[grdDonors.Columns.Count - 1].Visible = true;
        grdDonors.Columns[grdDonors.Columns.Count - 2].Visible = true;
        grdDonors.Columns[grdDonors.Columns.Count - 3].Visible = true;
        grdDonors.Columns[grdDonors.Columns.Count - 4].Visible = true;
        grdDonors.PagerSettings.Visible = true;

        BindGrid();
    }
    //private void BindGrid()
    //{
    //    SqlDataAdapter da = new SqlDataAdapter(@"
    //SELECT 
    //    r.ReceptionId,
    //    c.CadaverId,
    //    c.CadaverCode,
    //    c.Name,

    //    -- ✅ FIX (Donor जोड़ दिया)
    //    d.DonorId,

    //    r.ArrivalDateTime,
    //    smsrc.SourceName,
    //    r.IdentificationDetails,
    //    r.ConsentDetails,
    //    r.WitnessAadhaar,
    //    r.CreatedAt,

    //    CASE 
    //        WHEN c.IsRegisteredDonor = 1 THEN 'Yes'
    //        ELSE 'No'
    //    END AS RegisteredDonor,

    //    c.DOB,
    //    c.Age,
    //    c.Gender,
    //    c.DateOfDeath,
    //    c.AadhaarNumber,
    //    c.TimeOfDeath,
    //    c.PlaceOfDeath,
    //    c.CauseOfDeath,

    //    CASE 
    //        WHEN c.StatusId IS NULL THEN 'Not Received'
    //        ELSE sms.StatusName
    //    END AS StatusName,

    //    -- ✅ CONDITION
    //    ISNULL(cond.ConditionName, 'N/A') AS ConditionName

    //FROM CadaverReception r

    //INNER JOIN Cadavers c 
    //    ON r.CadaverId = c.CadaverId

    //-- ✅ FIX (JOIN ADD)
    //LEFT JOIN BodyDonors d 
    //    ON c.DonorId = d.Id

    //LEFT JOIN SourceMaster smsrc 
    //    ON smsrc.SourceId = r.SourceId

    //LEFT JOIN StatusMaster sms 
    //    ON sms.StatusId = c.StatusId

    //-- 🔥 LAST CONDITION
    //LEFT JOIN (
    //    SELECT ch.CadaverId, cm.ConditionName,
    //           ROW_NUMBER() OVER (PARTITION BY ch.CadaverId ORDER BY ch.UpdatedAt DESC) rn
    //    FROM CadaverConditionHistory ch
    //    INNER JOIN ConditionMaster cm 
    //        ON ch.ConditionIdRef = cm.ConditionId
    //) cond 
    //    ON cond.CadaverId = c.CadaverId AND cond.rn = 1

    //ORDER BY c.CreatedAt DESC
    //", con1);

    //    DataTable dt = new DataTable();
    //    da.Fill(dt);

    //    grdDonors.DataSource = dt;
    //    grdDonors.DataBind();
    //}
    protected void grdDonors_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDonors.PageIndex = e.NewPageIndex;

        BindGrid(); // 🔥 data reload
    }
    // EXPORT
  //  protected void btnexporttoexcel_Click(object sender, EventArgs e)
  //  {
  //      bool isPaging = grdDonors.AllowPaging;

  //      grdDonors.AllowPaging = false;

  //      DataTable dt;

  //      // 🔥 Check if search data exists
  //      if (ViewState["SearchData"] != null)
  //      {
  //          dt = (DataTable)ViewState["SearchData"];
  //      }
  //      else
  //      {
  //          // fallback full data
  //          SqlDataAdapter da = new SqlDataAdapter(@"
  //      SELECT 
  //    C.CadaverId,
	 // D.DonorId,
  //    C.CadaverCode,
  //    C.Name,
  //    C.[AadhaarNumber],
  //    C.Gender,
  //    C.DateOfDeath,
  //    C.PlaceOfDeath,
  //    C.CauseOfDeath,
  //    C.CreatedAt,
  //    CASE 
  //        WHEN C.IsRegisteredDonor = 1 THEN 'Yes'
  //        ELSE 'No'
  //    END AS RegisteredDonor
  //FROM Cadavers C left join [BodyDonors] D on  C.DonorId=D.Id
  //ORDER BY C.CreatedAt DESC", con1);

  //          dt = new DataTable();
  //          da.Fill(dt);
  //      }

  //      grdDonors.DataSource = dt;
  //      grdDonors.DataBind();

  //      // 🔥 Hide Action column
  //      grdDonors.Columns[grdDonors.Columns.Count - 1].Visible = false;
  //      grdDonors.PagerSettings.Visible = false;

  //      Response.Clear();
  //      Response.AddHeader("content-disposition", "attachment;filename=CadaverList.xls");
  //      Response.ContentType = "application/vnd.ms-excel";

  //      System.IO.StringWriter sw = new System.IO.StringWriter();
  //      System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);

  //      grdDonors.RenderControl(hw);

  //      Response.Write(sw.ToString());
  //      Response.End();

  //      // Restore
  //      grdDonors.AllowPaging = isPaging;
  //      grdDonors.Columns[grdDonors.Columns.Count - 1].Visible = true;
  //      grdDonors.PagerSettings.Visible = true;

  //      BindGrid();
  //  }

    public override void VerifyRenderingInServerForm(Control control) { }
//    protected void btnSearch_Click(object sender, EventArgs e)
//    {
//        using (SqlConnection con = new SqlConnection(cs))
//        {
//            string search = txtDonorNo.Text.Trim();

//            string query = @"
//          SELECT 
//    -- Reception
//    r.ReceptionId,
//    c.CadaverId,
//    c.CadaverCode,
//    c.Name,
//    r.ArrivalDateTime,
//    smsrc.SourceName,
//    r.IdentificationDetails,
//    r.ConsentDetails,
//    r.WitnessAadhaar,
//    r.CreatedAt,

//    -- Registered Donor
//    CASE 
//        WHEN c.IsRegisteredDonor = 1 THEN 'Yes'
//        ELSE 'No'
//    END AS RegisteredDonor,

//    -- Cadaver Info
//    c.DOB,
//    c.Age,
//    c.Gender,
//    c.DateOfDeath,
//    c.AadhaarNumber,
//    c.TimeOfDeath,
//    c.PlaceOfDeath,
//    c.CauseOfDeath,

//    -- Status
//    CASE 
//        WHEN c.StatusId IS NULL THEN 'Not Received'
//        ELSE sms.StatusName
//    END AS StatusName,

//    -- Donor Info
//    d.DonorId,
//    d.DonorNumber,
//    d.FatherOrHusbandName,
//    d.Address,
//    d.Mobile,
//    d.Email,
//    d.Religion,
//    d.IsActive

//FROM CadaverReception r

//INNER JOIN Cadavers c 
//    ON r.CadaverId = c.CadaverId

//LEFT JOIN BodyDonors d 
//    ON c.DonorId = d.Id

//LEFT JOIN SourceMaster smsrc 
//    ON smsrc.SourceId = r.SourceId

//LEFT JOIN StatusMaster sms 
//    ON sms.StatusId = c.StatusId

//WHERE 
//(
//    @search IS NULL OR @search = ''
//    OR c.CadaverCode LIKE '%' + @search + '%'
//    OR c.AadhaarNumber LIKE '%' + @search + '%'
//    OR c.Name LIKE '%' + @search + '%'
//    OR CAST(d.DonorId AS NVARCHAR) LIKE '%' + @search + '%'
//    OR d.Name LIKE '%' + @search + '%'
//)

//ORDER BY c.CadaverId DESC;";

//            SqlCommand cmd = new SqlCommand(query, con);
//            cmd.Parameters.AddWithValue("@search", search);

//            SqlDataAdapter da = new SqlDataAdapter(cmd);
//            DataTable dt = new DataTable();
//            da.Fill(dt);
//            ViewState["SearchData"] = dt;
//            grdDonors.PageIndex = 0; // 🔥 pagination reset
//            grdDonors.DataSource = dt;
//            grdDonors.DataBind();
//        }
//    }
    //protected void grdDonors_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        int cadaverId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "CadaverId"));

    //        // =========================
    //        // 🔹 FIND CONTROLS
    //        // =========================
    //        HyperLink lnkDeath = (HyperLink)e.Row.FindControl("lnkDeathDoc");
    //        HyperLink lnkID = (HyperLink)e.Row.FindControl("lnkIDDoc");
    //        HyperLink lnkPhoto = (HyperLink)e.Row.FindControl("lnkPhotoDoc");
    //        HyperLink lnkPolice = (HyperLink)e.Row.FindControl("lnkPoliceDoc");

    //        LinkButton btnReceive = (LinkButton)e.Row.FindControl("btnReceive");

    //        // =========================
    //        // 🔹 DEFAULT TEXT
    //        // =========================
    //        lnkDeath.Text = "Death : N/A";
    //        lnkID.Text = "ID : N/A";
    //        lnkPhoto.Text = "Photo : N/A";
    //        lnkPolice.Text = "Police : N/A";

    //        // =========================
    //        // 🔹 LOAD DATA
    //        // =========================
    //        using (SqlConnection con = new SqlConnection(cs))
    //        {
    //            con.Open();

    //            // =========================
    //            // 🔹 DOCUMENTS
    //            // =========================
    //            SqlCommand cmdDoc = new SqlCommand(@"
    //            SELECT DocumentId, FilePath 
    //            FROM CadaverDocuments 
    //            WHERE CadaverId=@Id", con);

    //            cmdDoc.Parameters.AddWithValue("@Id", cadaverId);

    //            SqlDataReader dr = cmdDoc.ExecuteReader();

    //            while (dr.Read())
    //            {
    //                int docId = Convert.ToInt32(dr["DocumentId"]);
    //                string path = dr["FilePath"].ToString();

    //                if (docId == 1)
    //                {
    //                    lnkDeath.NavigateUrl = path;
    //                    lnkDeath.Text = "Death : Download";
    //                }
    //                else if (docId == 2)
    //                {
    //                    lnkID.NavigateUrl = path;
    //                    lnkID.Text = "ID : Download";
    //                }
    //                else if (docId == 3)
    //                {
    //                    lnkPhoto.NavigateUrl = path;
    //                    lnkPhoto.Text = "Photo : Download";
    //                }
    //                else if (docId == 4)
    //                {
    //                    lnkPolice.NavigateUrl = path;
    //                    lnkPolice.Text = "Police : Download";
    //                }
    //            }

    //            dr.Close();

    //            // =========================
    //            // 🔹 DISABLE N/A LINKS
    //            // =========================
    //            if (lnkDeath.Text.Contains("N/A")) lnkDeath.Enabled = false;
    //            if (lnkID.Text.Contains("N/A")) lnkID.Enabled = false;
    //            if (lnkPhoto.Text.Contains("N/A")) lnkPhoto.Enabled = false;
    //            if (lnkPolice.Text.Contains("N/A")) lnkPolice.Enabled = false;           
    //        }
    //    }
    //}
    protected void grdDonors_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int cadaverId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "CadaverId"));

            string status = DataBinder.Eval(e.Row.DataItem, "StatusName").ToString();
            string condition = DataBinder.Eval(e.Row.DataItem, "ConditionName").ToString();

            // =========================
            // 🔹 DOCUMENT LINKS
            // =========================
            HyperLink lnkDeath = (HyperLink)e.Row.FindControl("lnkDeathDoc");
            HyperLink lnkID = (HyperLink)e.Row.FindControl("lnkIDDoc");
            HyperLink lnkPhoto = (HyperLink)e.Row.FindControl("lnkPhotoDoc");
            HyperLink lnkPolice = (HyperLink)e.Row.FindControl("lnkPoliceDoc");
            HyperLink lnkVolontaryBodyDonation = (HyperLink)e.Row.FindControl("lnkVolontaryBodyDonationDoc");
            HyperLink lnkThanksLetter = (HyperLink)e.Row.FindControl("lnkThanksLetterDoc");

            lnkDeath.Text = "Death : N/A";
            lnkID.Text = "ID : N/A";
            lnkPhoto.Text = "Photo : N/A";
            lnkPolice.Text = "Police : N/A";

            lnkVolontaryBodyDonation.Text = "Volontary : N/A";
            lnkThanksLetter.Text = "ThanksLetter : N/A";

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                SqlCommand cmdDoc = new SqlCommand(@"
            SELECT DocumentId, FilePath 
            FROM CadaverDocuments 
            WHERE CadaverId=@Id", con);

                cmdDoc.Parameters.AddWithValue("@Id", cadaverId);

                SqlDataReader dr = cmdDoc.ExecuteReader();

                while (dr.Read())
                {
                    int docId = Convert.ToInt32(dr["DocumentId"]);
                    string path = dr["FilePath"].ToString();

                    if (docId == 1)
                    {
                        lnkDeath.NavigateUrl = path;
                        lnkDeath.Text = "Death : Download";
                    }
                    else if (docId == 2)
                    {
                        lnkID.NavigateUrl = path;
                        lnkID.Text = "ID : Download";
                    }
                    else if (docId == 3)
                    {
                        lnkPhoto.NavigateUrl = path;
                        lnkPhoto.Text = "Photo : Download";
                    }
                    else if (docId == 4)
                    {
                        lnkPolice.NavigateUrl = path;
                        lnkPolice.Text = "Police : Download";
                    }
                    else if (docId == 5)
                    {
                        lnkVolontaryBodyDonation.NavigateUrl = path;
                        lnkVolontaryBodyDonation.Text = "Volontary : Download";
                    }
                    else if (docId == 6)
                    {
                        lnkThanksLetter.NavigateUrl = path;
                        lnkThanksLetter.Text = "Thanks Letter : Download";
                    }
                }

                dr.Close();
            }

            // =========================
            // 🔹 BUTTON CONTROL
            // =========================
            LinkButton btnEnd = (LinkButton)e.Row.FindControl("btnEnd");
            LinkButton btnDispose = (LinkButton)e.Row.FindControl("btnFinalDisposal");

            btnEnd.Visible = false;
            btnDispose.Visible = false;

            // ❌ Disposed
            if (status == "Disposed")
                return;

            // ✅ Condition Update
            if (status == "Stored" || status == "In Use" || status == "Returned")
                btnEnd.Visible = true;

            // ✅ Dispose only if Fully Used
            if (condition == "Fully Used")
            {
                btnDispose.Visible = true;
                btnEnd.Visible = false;
            }
        }
    }
    private void BindCondition()
    {
        using (SqlConnection con = new SqlConnection(cs))
        {
            SqlDataAdapter da = new SqlDataAdapter(
            "SELECT ConditionId, ConditionName FROM ConditionMaster", con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlCondition.DataSource = dt;
            ddlCondition.DataTextField = "ConditionName";   // 👁️ show
            ddlCondition.DataValueField = "ConditionId";    // 💾 save
            ddlCondition.DataBind();

            ddlCondition.Items.Insert(0, new ListItem("--Select Condition--", ""));
        }
    }
    protected void grdDonors_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewCadaver")
        {
            int cadaverId = Convert.ToInt32(e.CommandArgument);

            LoadBodyStorage(cadaverId);

            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "Popup", "$('#cadaverViewModal').modal('show');", true);
        }
        if (e.CommandName == "OpenEndModal")
        {
            int cadaverId = Convert.ToInt32(e.CommandArgument);

            hfCadaverId.Value = cadaverId.ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "Popup", "$('#endModal').modal('show');", true);
        }
        if (e.CommandName == "OpenFinalDisposalModal")
        {
            int cadaverId = Convert.ToInt32(e.CommandArgument);

            hfDisposeCadaverId.Value = cadaverId.ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "Popup", "$('#FinalDisposalModal').modal('show');", true);
        }
    }
    private void ClearCadaverDetails()
    {
        // =========================
        // 🔹 DONOR LABELS
        // =========================
        lblName.Text = "";
        lblFather.Text = "";
        lblDOB.Text = "";
        lblAge.Text = "";
        lblGender.Text = "";
        lblMobile.Text = "";
        lblEmail.Text = "";
        lblReligion.Text = "";
        lblAadhaar.Text = "";
        lblAddress.Text = "";

        lblDW1Name.Text = "";
        lblDW1Address.Text = "";
        lblDW1Relation.Text = "";
        lblDW1Mobile.Text = "";

        lblCW1Aadhaar.Text = "";
        lblCW2Aadhaar.Text = "";
        hlCW1Aadhaar.NavigateUrl = "";
        hlCW1Aadhaar.Text = "";
        hlCW2Aadhaar.NavigateUrl = "";
        hlCW2Aadhaar.Text = "";

        lblDW2Name.Text = "";
        lblDW2Address.Text = "";
        lblDW2Relation.Text = "";
        lblDW2Mobile.Text = "";

        // =========================
        // 🔹 CADAVER LABELS
        // =========================
        lblCadaverName.Text = "";
        lblCadaverAge.Text = "";
        lblCadaverGender.Text = "";
        lblCadaverAadhaar.Text = "";
        lblDOD.Text = "";
        lblPlace.Text = "";

        lblCW1Name.Text = "";
        lblCW1Address.Text = "";
        lblCW1Relation.Text = "";
        lblCW1Mobile.Text = "";

        lblCW2Name.Text = "";
        lblCW2Address.Text = "";
        lblCW2Relation.Text = "";
        lblCW2Mobile.Text = "";

        // =========================
        // 🔹 DOCUMENT LINKS
        // =========================
        lnkDeath.NavigateUrl = "";
        lnkID.NavigateUrl = "";
        lnkPhoto.NavigateUrl = "";
        lnkPolice.NavigateUrl = "";

        lnkDeath.Text = "";
        lnkID.Text = "";
        lnkPhoto.Text = "";
        lnkPolice.Text = "";

        // =========================
        // 🔹 GRID CLEAR
        // =========================
        grdStorageHistory.DataSource = null;
        grdStorageHistory.DataBind();


        grdCadaverCondition.DataSource = null;
        grdCadaverCondition.DataBind();

        // =========================
        // 🔹 PANEL HIDE (OPTIONAL)
        // =========================
        pnlDonor.Visible = false;
    }
    private void LoadBodyStorage(int cadaverId1)
    {
        ClearCadaverDetails();
        using (SqlConnection con = new SqlConnection(cs))
        {
            con.Open();

            int cadaverId = 0;
            int donorId = 0;

            // 🔥 CLEAR ALL LABELS

            // Donor
            lblName.Text = lblFather.Text = lblDOB.Text = lblAge.Text = lblGender.Text = "";
            lblAddress.Text = lblMobile.Text = lblEmail.Text = lblReligion.Text = lblAadhaar.Text = "";

            // Donor Witness
            lblDW1Name.Text = lblDW2Name.Text = "";
            lblDW1Address.Text = lblDW2Address.Text = "";
            lblDW1Relation.Text = lblDW2Relation.Text = "";
            lblDW1Mobile.Text = lblDW2Mobile.Text = "";

            // Cadaver
            lblCadaverName.Text = lblCadaverAge.Text = lblCadaverGender.Text = "";
            lblCadaverAadhaar.Text = lblDOD.Text = lblPlace.Text = "";

            // Cadaver Witness
            lblCW1Name.Text = lblCW2Name.Text = "";
            lblCW1Address.Text = lblCW2Address.Text = "";
            lblCW1Relation.Text = lblCW2Relation.Text = "";
            lblCW1Mobile.Text = lblCW2Mobile.Text = "";

            // ===============================
            // 🔥 STEP 1: CADAVER (ALWAYS LOAD)
            // ===============================
            SqlCommand cmd = new SqlCommand(@"
        SELECT c.CadaverId, c.DonorId, c.Name, c.Age, c.Gender,
               c.AadhaarNumber, c.DateOfDeath, c.PlaceOfDeath
        FROM CadaverStorage cs
        INNER JOIN Cadavers c ON cs.CadaverId = c.CadaverId
        WHERE cs.CadaverId = @Id", con);

            cmd.Parameters.AddWithValue("@Id", cadaverId1);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                cadaverId = Convert.ToInt32(dr["CadaverId"]);

                donorId = dr["DonorId"] != DBNull.Value
                    ? Convert.ToInt32(dr["DonorId"])
                    : 0;

                // ✅ CADAVER → ALWAYS SHOW
                lblCadaverName.Text = dr["Name"].ToString();
                lblCadaverAge.Text = dr["Age"].ToString();
                lblCadaverGender.Text = dr["Gender"].ToString();
                lblCadaverAadhaar.Text = dr["AadhaarNumber"].ToString();

                lblDOD.Text = dr["DateOfDeath"] != DBNull.Value
                    ? Convert.ToDateTime(dr["DateOfDeath"]).ToString("dd-MM-yyyy")
                    : "";

                lblPlace.Text = dr["PlaceOfDeath"].ToString();
            }
            dr.Close();

            // ===============================
            // 🔥 STEP 2: DONOR (OPTIONAL)
            // ===============================
            if (donorId > 0)
            {
                pnlDonor.Visible = true;
                SqlCommand d = new SqlCommand("SELECT * FROM BodyDonors WHERE Id=@Id", con);
                d.Parameters.AddWithValue("@Id", donorId);

                SqlDataReader dr2 = d.ExecuteReader();

                if (dr2.Read())
                {
                    lblName.Text = dr2["Name"].ToString();
                    lblFather.Text = dr2["FatherOrHusbandName"].ToString();

                    lblDOB.Text = dr2["DOB"] != DBNull.Value
                        ? Convert.ToDateTime(dr2["DOB"]).ToString("dd-MM-yyyy")
                        : "";

                    lblAge.Text = dr2["Age"].ToString();
                    lblGender.Text = dr2["Gender"].ToString();
                    lblAddress.Text = dr2["Address"].ToString();
                    lblMobile.Text = dr2["Mobile"].ToString();
                    lblEmail.Text = dr2["Email"].ToString();
                    lblReligion.Text = dr2["Religion"].ToString();
                    lblAadhaar.Text = dr2["AadhaarNumber"].ToString();
                }
                dr2.Close();
            }
            else
            {
                pnlDonor.Visible = false;
            }

            // ===============================
            // 🔥 STEP 3: DONOR WITNESS
            // ===============================
            if (donorId > 0)
            {
                SqlCommand dw = new SqlCommand(@"
            SELECT TOP 2 * FROM BodyDonorsWitnesses 
            WHERE DonorId=@Id ORDER BY WitnessId", con);

                dw.Parameters.AddWithValue("@Id", donorId);

                SqlDataReader drDW = dw.ExecuteReader();

                int i = 1;
                while (drDW.Read())
                {
                    if (i == 1)
                    {
                        lblDW1Name.Text = drDW["Name"].ToString();
                        lblDW1Address.Text = drDW["Address"].ToString();
                        lblDW1Relation.Text = drDW["Relationship"].ToString();
                        lblDW1Mobile.Text = drDW["MobileOrEmail"].ToString();

                    }
                    else if (i == 2)
                    {
                        lblDW2Name.Text = drDW["Name"].ToString();
                        lblDW2Address.Text = drDW["Address"].ToString();
                        lblDW2Relation.Text = drDW["Relationship"].ToString();
                        lblDW2Mobile.Text = drDW["MobileOrEmail"].ToString();

                    }
                    i++;
                }
                drDW.Close();
            }

            // ===============================
            // 🔥 STEP 4: CADAVER WITNESS
            // ===============================
            SqlCommand cw = new SqlCommand(@"
        SELECT TOP 2 * FROM CadaverWitnesses 
        WHERE CadaverId=@Cid ORDER BY WitnessId", con);

            cw.Parameters.AddWithValue("@Cid", cadaverId);

            SqlDataReader drCW = cw.ExecuteReader();

            int j = 1;
            while (drCW.Read())
            {
                if (j == 1)
                {
                    lblCW1Name.Text = drCW["Name"].ToString();
                    lblCW1Address.Text = drCW["Address"].ToString();
                    lblCW1Relation.Text = drCW["Relationship"].ToString();
                    lblCW1Mobile.Text = drCW["MobileOrEmail"].ToString();
                    lblCW1Aadhaar.Text = drCW["AadhaarNumber"] != DBNull.Value ? drCW["AadhaarNumber"].ToString() : "";
                    hlCW1Aadhaar.NavigateUrl = drCW["AadhaarFilePath"] != DBNull.Value ? drCW["AadhaarFilePath"].ToString() : "";
                    hlCW1Aadhaar.Text = drCW["AadhaarFilePath"] != DBNull.Value ? "Download" : "";
                }
                else if (j == 2)
                {
                    lblCW2Name.Text = drCW["Name"].ToString();
                    lblCW2Address.Text = drCW["Address"].ToString();
                    lblCW2Relation.Text = drCW["Relationship"].ToString();
                    lblCW2Mobile.Text = drCW["MobileOrEmail"].ToString();
                    lblCW2Aadhaar.Text = drCW["AadhaarNumber"] != DBNull.Value ? drCW["AadhaarNumber"].ToString() : "";
                    hlCW2Aadhaar.NavigateUrl = drCW["AadhaarFilePath"] != DBNull.Value ? drCW["AadhaarFilePath"].ToString() : "";
                    hlCW2Aadhaar.Text = drCW["AadhaarFilePath"] != DBNull.Value ? "Download" : "";
                }
                j++;
            }
            drCW.Close();

            // ===============================
            // 🔥 STEP 5: STORAGE HISTORY
            // ===============================
            SqlDataAdapter da = new SqlDataAdapter(@"
        SELECT RoomNumber, FreezerNumber, RackNumber, AllocatedAt, ReleasedAt
        FROM CadaverStorage 
        WHERE CadaverId=@Cid
        ORDER BY AllocatedAt DESC", con);

            da.SelectCommand.Parameters.AddWithValue("@Cid", cadaverId);

            DataTable dt = new DataTable();
            da.Fill(dt);

            grdStorageHistory.DataSource = dt;
            grdStorageHistory.DataBind();


            // ===============================
            // 🔥 STEP 6: CADAVER CONDITION
            // ===============================
            SqlDataAdapter da1 = new SqlDataAdapter(@"
         SELECT  cm.ConditionId,
            cm.ConditionName,
            cch.CadaverId,
            cch.Remarks,
            cch.UpdatedAt
            FROM [EDUCOLLEGELIVE-R2].[dbo].[ConditionMaster] cm
            INNER JOIN [EDUCOLLEGELIVE-R2].[dbo].[CadaverConditionHistory] cch
            ON cm.ConditionId = cch.ConditionIdRef
            WHERE cch.CadaverId=@Cid
            ORDER BY cch.UpdatedAt DESC", con);

            da1.SelectCommand.Parameters.AddWithValue("@Cid", cadaverId);

            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            grdCadaverCondition.DataSource = dt1;
            grdCadaverCondition.DataBind();


            // ===============================
            // 🔥 STEP 7: Department
            // ===============================
            SqlDataAdapter da2 = new SqlDataAdapter(@"
         SELECT 
    cu.UsageId,
    cu.CadaverId,
    cu.StorageId, 

    cu.Department,
    cu.Batch,
    cu.DissectionHall,
    cu.InChargeName,
    cu.StartDate,
    cu.EndDate,

    cs.RoomNumber,
    cs.FreezerNumber,
    cs.RackNumber,
    cs.AllocatedAt,
    cs.ReleasedAt,
    cs.IsActive,

    CASE 
        WHEN cu.StatusId = 4 THEN 'In Use'
        WHEN cu.StatusId = 5 THEN 'Returned'
        ELSE 'Stored'
    END AS UsageStatus,

    c.CadaverNumber,
    c.CadaverCode,
    c.Name,
    c.DOB,
    c.Age,
    c.Gender,
    c.DateOfDeath,
    c.PlaceOfDeath,
    c.CauseOfDeath,
    c.AadhaarNumber,

    bd.DonorId AS DonorRegNo,
    bd.Name AS DonorName

FROM CadaverUsage cu

INNER JOIN Cadavers c 
    ON cu.CadaverId = c.CadaverId

LEFT JOIN CadaverStorage cs 
    ON cu.StorageId = cs.StorageId

LEFT JOIN BodyDonors bd 
    ON c.DonorId = bd.Id
where cu.CadaverId=@Cid
ORDER BY cu.StartDate DESC", con);

            da2.SelectCommand.Parameters.AddWithValue("@Cid", cadaverId);

            DataTable dt2 = new DataTable();
            da2.Fill(dt2);

            grdDepartment.DataSource = dt2;
            grdDepartment.DataBind();


            // =========================
            // 🔹 Documents (🔥 NEW ADD)
            // =========================
            SqlCommand cmdDoc = new SqlCommand(@"
                 SELECT DocumentId, FilePath 
                 FROM CadaverDocuments 
                 WHERE CadaverId=@Id", con);

            cmdDoc.Parameters.AddWithValue("@Id", cadaverId1);

            SqlDataReader drDoc = cmdDoc.ExecuteReader();

            while (drDoc.Read())
            {
                int docId = Convert.ToInt32(drDoc["DocumentId"]);
                string path = drDoc["FilePath"].ToString();

                if (docId == 1) // Death Certificate
                {
                    lnkDeath.NavigateUrl = path;
                    lnkDeath.Text = "⬇ Download";
                    lnkDeath.Enabled = true;
                }
                else if (docId == 2) // ID Proof
                {
                    lnkID.NavigateUrl = path;
                    lnkID.Text = "⬇ Download";
                    lnkID.Enabled = true;
                }
                else if (docId == 3) // Photo
                {
                    lnkPhoto.NavigateUrl = path;
                    lnkPhoto.Text = "⬇ Download";
                    lnkPhoto.Enabled = true;
                }
                else if (docId == 4) // Police
                {
                    lnkPolice.NavigateUrl = path;
                    lnkPolice.Text = "⬇ Download";
                    lnkPolice.Enabled = true;
                }
            }

            drDoc.Close();

            // 🔥 अगर कोई doc नहीं है
            if (lnkDeath.Text == "")
            {
                lnkDeath.Text = "Not Uploaded";
                lnkDeath.Enabled = false;
            }

            if (lnkID.Text == "")
            {
                lnkID.Text = "Not Uploaded";
                lnkID.Enabled = false;
            }

            if (lnkPhoto.Text == "")
            {
                lnkPhoto.Text = "Not Uploaded";
                lnkPhoto.Enabled = false;
            }

            if (lnkPolice.Text == "")
            {
                lnkPolice.Text = "Not Uploaded";
                lnkPolice.Enabled = false;
            }
        
    }
    }
    private void Alert(string msg)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(),
            "alert", "alert('" + msg.Replace("'", "") + "');", true);
    }
    protected void btnConfirmEnd_Click(object sender, EventArgs e)
    {
        if (hfCadaverId.Value == "")
        {
            Alert("Invalid");
            return;
        }

        if (ddlCondition.SelectedIndex == 0)
        {
            Alert("Select condition");
            return;
        }

        int cadaverId = Convert.ToInt32(hfCadaverId.Value);

        EndSessionWithCondition(cadaverId);
    }
    private void EndSessionWithCondition(int cadaverId)
    {
        using (SqlConnection con = new SqlConnection(cs))
        {
            con.Open();
            SqlTransaction trans = con.BeginTransaction();

            try
            {
                SqlCommand cond = new SqlCommand(@"
            INSERT INTO CadaverConditionHistory
            (CadaverId, ConditionIdRef, Remarks)
            VALUES
            (@Cid, @ConditionId, @Remarks)", con, trans);

                cond.Parameters.AddWithValue("@Cid", cadaverId);
                cond.Parameters.AddWithValue("@ConditionId", ddlCondition.SelectedValue);
                cond.Parameters.AddWithValue("@Remarks",
                    string.IsNullOrWhiteSpace(txtRemarks.Text) ? "" : txtRemarks.Text);

                cond.ExecuteNonQuery();

                // ✅ VERY IMPORTANT
                trans.Commit();

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "Close", "$('#endModal').modal('hide');", true);

                ddlCondition.SelectedIndex = 0;
                txtRemarks.Text = "";

                Alert("Condition Saved");

                BindGrid();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                Alert(ex.Message);
            }
        }
    }
    protected void btnDispose_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        if (hfDisposeCadaverId.Value == "")
        {
            Alert("Invalid");
            return;
        }

        if (ddlMethod.SelectedIndex == 0)
        {
            Alert("Select disposal method");
            return;
        }

        if (string.IsNullOrEmpty(txtDisposalDate.Text))
        {
            Alert("Select disposal date");
            return;
        }

        int cadaverId = Convert.ToInt32(hfDisposeCadaverId.Value);

        SaveDisposal(cadaverId);
    }
    private void SaveDisposal(int cadaverId)
    {
        using (SqlConnection con = new SqlConnection(cs))
        {
            con.Open();
            SqlTransaction trans = con.BeginTransaction();

            try
            {
                string photoPath = null;

                if (fuDisposalPhoto.HasFile)
                {
                    string folder = Server.MapPath("~/Uploads/Disposal/");

                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    string fileName = "DISP_" + DateTime.Now.Ticks + "_" + fuDisposalPhoto.FileName;
                    string fullPath = folder + fileName;

                    fuDisposalPhoto.SaveAs(fullPath);

                    photoPath = "~/Uploads/Disposal/" + fileName;
                }

                SqlCommand cmd = new SqlCommand(@"
            INSERT INTO CadaverDisposal
            (CadaverId, DisposalDate, Method, ApprovedBy, PhotoPath)
            VALUES
            (@Cid, @Date, @Method, @ApprovedBy, @Photo)", con, trans);

                cmd.Parameters.AddWithValue("@Cid", cadaverId);
                cmd.Parameters.AddWithValue("@Date", txtDisposalDate.Text);
                cmd.Parameters.AddWithValue("@Method", ddlMethod.SelectedValue);
                cmd.Parameters.AddWithValue("@ApprovedBy", txtApprovedBy.Text);
                cmd.Parameters.AddWithValue("@Photo", (object)photoPath ?? DBNull.Value);

                cmd.ExecuteNonQuery();

                // ✅ FINAL STATUS
                SqlCommand status = new SqlCommand(@"
            UPDATE Cadavers
            SET StatusId = 6,
                UpdatedAt = GETDATE()
            WHERE CadaverId=@Cid", con, trans);

                status.Parameters.AddWithValue("@Cid", cadaverId);
                status.ExecuteNonQuery();

                trans.Commit();

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "Close", "$('#FinalDisposalModal').modal('hide');", true);

                Alert("Disposal Saved Successfully");

                txtDisposalDate.Text = "";
                ddlMethod.SelectedIndex = 0;
                txtApprovedBy.Text = "";

                BindGrid();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                Alert(ex.Message);
            }
        }
    }
}