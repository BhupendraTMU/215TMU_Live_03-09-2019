using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_BodyRegistration : System.Web.UI.Page
{
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    string cs = ConfigurationManager.ConnectionStrings["TMUCON"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }
    protected void btnCheckDonor_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(cs))
        {
            string search = txtSearchValue.Text.Trim();

            SqlCommand cmd = new SqlCommand(@"
        SELECT TOP 1 BD.*, C.CadaverId
        FROM BodyDonors BD
        LEFT JOIN Cadavers C ON BD.Id = C.DonorId
        WHERE 
            BD.DonorId = @search
            OR BD.AadhaarNumber = @search
        ", con);

            cmd.Parameters.AddWithValue("@search", search);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                // ❌ अगर पहले से Cadaver बना हुआ है
                if (dr["CadaverId"] != DBNull.Value)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                        "alert('❌ This donor is already used in Cadaver entry');", true);

                    ViewState["DonorId"] = null;
                    dr.Close();

                    // Modal reopen
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "Popup", "$('#CadaverModal').modal('show');", true);

                    return;
                }

                // ✅ Fill data
                txtDName.Text = dr["Name"].ToString();

                if (dr["DOB"] != DBNull.Value)
                    txtDDOB.Text = Convert.ToDateTime(dr["DOB"]).ToString("yyyy-MM-dd");

                txtDAge.Text = dr["Age"].ToString();
                ddlDGender.SelectedValue = dr["Gender"].ToString();
                txtAadhaar.Text = dr["AadhaarNumber"].ToString();
                ViewState["DonorId"] = Convert.ToInt32(dr["Id"]);

                txtDName.ReadOnly = true;
                txtDDOB.ReadOnly = true;
                txtDAge.ReadOnly = true;
                ddlDGender.Enabled = false;
                txtAadhaar.Enabled = false;
            }
            else
            {
                // After Death Case
                ViewState["DonorId"] = null;

                txtDName.Text = "";
                txtDDOB.Text = "";
                txtDAge.Text = "";

                txtDName.ReadOnly = false;
                txtDDOB.ReadOnly = false;
                txtDAge.ReadOnly = false;
                ddlDGender.Enabled = true;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "alert('Donor not found. Enter manually.');", true);
            }

            dr.Close();
        }

        // 🔥 Modal reopen
        ScriptManager.RegisterStartupScript(this, this.GetType(),
            "Popup", "$('#CadaverModal').modal('show');", true);
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
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        // 🔥 पूरा form reset
        ClearCadaverForm();

        // 🔥 Edit mode remove
        ViewState["EditCadaverId"] = null;

        // 🔥 Button text reset
        btnSaveCadaver.Text = "Save Body";

        // 🔥 Modal open
        ScriptManager.RegisterStartupScript(this, this.GetType(),
            "Popup", "$('#CadaverModal').modal('show');", true);
    }
    private void InsertDoc(SqlConnection con, SqlTransaction trans, int cadaverId, int docId, string path)
    {
        if (path == null) return;

        SqlCommand cmdDoc = new SqlCommand(@"
    INSERT INTO CadaverDocuments (CadaverId, DocumentId, FilePath)
    VALUES (@Cid,@DocId,@Path)", con, trans);

        cmdDoc.Parameters.AddWithValue("@Cid", cadaverId);
        cmdDoc.Parameters.AddWithValue("@DocId", docId);
        cmdDoc.Parameters.AddWithValue("@Path", path);

        cmdDoc.ExecuteNonQuery();
    }
    protected void btnSaveCadaver_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(cs))
        {
            con.Open();
            SqlTransaction trans = con.BeginTransaction();

            try
            {
                int? editId = ViewState["EditCadaverId"] as int?;

                // =========================
                // 🔥 UPDATE MODE
                // =========================
                if (editId != null)
                {
                    // =========================
                    // 🔹 Cadaver Update
                    // =========================
                    SqlCommand cmd = new SqlCommand(@"
                UPDATE Cadavers SET
                    Name=@Name,
                    DOB=@DOB,
                    Age=@Age,
                    Gender=@Gender,
                    DateOfDeath=@DeathDate,
                    TimeOfDeath=@DeathTime,
                    PlaceOfDeath=@Place,
                    CauseOfDeath=@Cause,
                    AadhaarNumber=@Aadhaar
                WHERE CadaverId=@Id", con, trans);

                    cmd.Parameters.AddWithValue("@Id", editId);
                    cmd.Parameters.AddWithValue("@Name", txtDName.Text);

                    cmd.Parameters.Add("@DOB", SqlDbType.Date).Value =
                        string.IsNullOrEmpty(txtDDOB.Text) ? (object)DBNull.Value : Convert.ToDateTime(txtDDOB.Text);

                    cmd.Parameters.Add("@Age", SqlDbType.Int).Value =
                        string.IsNullOrEmpty(txtDAge.Text) ? (object)DBNull.Value : Convert.ToInt32(txtDAge.Text);

                    cmd.Parameters.AddWithValue("@Gender", ddlDGender.SelectedValue);

                    cmd.Parameters.Add("@DeathDate", SqlDbType.Date).Value =
                        string.IsNullOrEmpty(txtDeathDate.Text) ? (object)DBNull.Value : Convert.ToDateTime(txtDeathDate.Text);

                    cmd.Parameters.Add("@DeathTime", SqlDbType.Time).Value =
                        string.IsNullOrEmpty(txtDeathTime.Text) ? (object)DBNull.Value : TimeSpan.Parse(txtDeathTime.Text);

                    cmd.Parameters.AddWithValue("@Place", txtPlace.Text);
                    cmd.Parameters.AddWithValue("@Cause", txtCause.Text);
                    cmd.Parameters.AddWithValue("@Aadhaar", txtAadhaar.Text);

                    cmd.ExecuteNonQuery();

                    // =========================
                    // 🔹 Witness 1 Update
                    // =========================
                    string w1AadhaarFile = null;

                    if (fuCW1Aadhaar.HasFile)
                    {
                        w1AadhaarFile = SaveFile(fuCW1Aadhaar, "Witness1Aadhaar");
                    }

                    SqlCommand cmdW1 = new SqlCommand(@"
                            IF EXISTS (SELECT 1 FROM CadaverWitnesses
                                       WHERE CadaverId=@Cid AND WitnessType='Witness1')
                            BEGIN
                                UPDATE CadaverWitnesses
                                SET Name=@Name,
                                    Address=@Address,
                                    Relationship=@Relation,
                                    MobileOrEmail=@Mobile,
                                    AadhaarNumber=@Aadhaar,
                                    AadhaarFilePath = ISNULL(@AadhaarFilePath, AadhaarFilePath)
                                WHERE CadaverId=@Cid
                                  AND WitnessType='Witness1'
                            END
                            ELSE
                            BEGIN
                                INSERT INTO CadaverWitnesses
                                (
                                    CadaverId,
                                    WitnessType,
                                    Name,
                                    Address,
                                    Relationship,
                                    MobileOrEmail,
                                    AadhaarNumber,
                                    AadhaarFilePath
                                )
                                VALUES
                                (
                                    @Cid,
                                    'Witness1',
                                    @Name,
                                    @Address,
                                    @Relation,
                                    @Mobile,
                                    @Aadhaar,
                                    @AadhaarFilePath
                                )
                            END", con, trans);

                    cmdW1.Parameters.AddWithValue("@Cid", editId);
                    cmdW1.Parameters.AddWithValue("@Name", txtCW1Name.Text);
                    cmdW1.Parameters.AddWithValue("@Address", txtCW1Address.Text);
                    cmdW1.Parameters.AddWithValue("@Relation", txtCW1Relation.Text);
                    cmdW1.Parameters.AddWithValue("@Mobile", txtCW1Mobile.Text);
                    cmdW1.Parameters.AddWithValue("@Aadhaar", txtCW1Aadhaar.Text);
                    cmdW1.Parameters.AddWithValue("@AadhaarFilePath",
                        (object)w1AadhaarFile ?? DBNull.Value);

                    cmdW1.ExecuteNonQuery();

                    // =========================
                    // 🔹 Next Of Kin Update
                    // =========================
                    if (!string.IsNullOrEmpty(txtCW2Name.Text))
                    {
                        string w2AadhaarFile = null;

                        if (fuCW2Aadhaar.HasFile)
                        {
                            w2AadhaarFile = SaveFile(fuCW2Aadhaar, "Witness2Aadhaar");
                        }
                        SqlCommand cmdW2 = new SqlCommand(@"
                            IF EXISTS (SELECT 1 FROM CadaverWitnesses
                                       WHERE CadaverId=@Cid AND WitnessType='NextOfKin')
                            BEGIN
                                UPDATE CadaverWitnesses
                                SET Name=@Name,
                                    Address=@Address,
                                    Relationship=@Relation,
                                    MobileOrEmail=@Mobile,
                                    AadhaarNumber=@Aadhaar,
                                    AadhaarFilePath = ISNULL(@AadhaarFilePath, AadhaarFilePath)
                                WHERE CadaverId=@Cid
                                  AND WitnessType='NextOfKin'
                            END
                            ELSE
                            BEGIN
                                INSERT INTO CadaverWitnesses
                                (
                                    CadaverId,
                                    WitnessType,
                                    Name,
                                    Address,
                                    Relationship,
                                    MobileOrEmail,
                                    AadhaarNumber,
                                    AadhaarFilePath
                                )
                                VALUES
                                (
                                    @Cid,
                                    'NextOfKin',
                                    @Name,
                                    @Address,
                                    @Relation,
                                    @Mobile,
                                    @Aadhaar,
                                    @AadhaarFilePath
                                )
                            END", con, trans);

                        cmdW2.Parameters.AddWithValue("@Cid", editId);
                        cmdW2.Parameters.AddWithValue("@Name", txtCW2Name.Text);
                        cmdW2.Parameters.AddWithValue("@Address", txtCW2Address.Text);
                        cmdW2.Parameters.AddWithValue("@Relation", txtCW2Relation.Text);
                        cmdW2.Parameters.AddWithValue("@Mobile", txtCW2Mobile.Text);
                        cmdW2.Parameters.AddWithValue("@Aadhaar", txtCW2Aadhaar.Text);
                        cmdW2.Parameters.AddWithValue("@AadhaarFilePath",
                                               (object)w2AadhaarFile ?? DBNull.Value);
                        cmdW2.ExecuteNonQuery();
                    }

                    // =========================
                    // 🔹 Documents Update (ONLY IF FILE SELECTED)
                    // =========================
                    Action<int, FileUpload, string> UpdateDoc = (docId, fu, prefix) =>
                    {
                        if (fu.HasFile)
                        {
                            string path = SaveFile(fu, prefix);

                            SqlCommand cmdDoc = new SqlCommand(@"
                        IF EXISTS (SELECT 1 FROM CadaverDocuments WHERE CadaverId=@Cid AND DocumentId=@Did)
                        UPDATE CadaverDocuments 
                        SET FilePath=@Path 
                        WHERE CadaverId=@Cid AND DocumentId=@Did
                        ELSE
                        INSERT INTO CadaverDocuments (CadaverId, DocumentId, FilePath)
                        VALUES (@Cid,@Did,@Path)", con, trans);

                            cmdDoc.Parameters.AddWithValue("@Cid", editId);
                            cmdDoc.Parameters.AddWithValue("@Did", docId);
                            cmdDoc.Parameters.AddWithValue("@Path", path);

                            cmdDoc.ExecuteNonQuery();
                        }
                    };

                    UpdateDoc(1, fuDeath, "Death");
                    UpdateDoc(2, fuID, "ID");
                    UpdateDoc(3, fuPhoto, "Photo");
                    UpdateDoc(4, fuPolice, "Police");
                    UpdateDoc(5, fuVolontaryBodyDonation, "VolontaryBodyDonation");
                    UpdateDoc(6, fuThanksLetter, "ThanksLetter");

                    trans.Commit();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ok",
                        "alert('Updated Successfully');", true);

                    ViewState["EditCadaverId"] = null;
                    btnSaveCadaver.Text = "Save Body";

                    ClearCadaverForm();
                    BindGrid();

                    return;
                }

                // =========================
                // 🔥 INSERT MODE (AS IS)
                // =========================
                int? donorId = ViewState["DonorId"] as int?;

                SqlCommand insertCmd = new SqlCommand(@"
            INSERT INTO Cadavers
            (Name, DOB, Age, Gender, DateOfDeath, TimeOfDeath, PlaceOfDeath, CauseOfDeath,
            AadhaarNumber, DonorId, IsRegisteredDonor, StatusId, CreatedBy,UpdatedAt)
            OUTPUT INSERTED.CadaverId
            VALUES
            (@Name,@DOB,@Age,@Gender,@DeathDate,@DeathTime,@Place,@Cause,
            @Aadhaar,@DonorId,@IsReg,1,@CreatedBy,@UpdatedAt)", con, trans);

                insertCmd.Parameters.AddWithValue("@Name", txtDName.Text);

                insertCmd.Parameters.Add("@DOB", SqlDbType.Date).Value =
                    string.IsNullOrEmpty(txtDDOB.Text) ? (object)DBNull.Value : Convert.ToDateTime(txtDDOB.Text);

                insertCmd.Parameters.Add("@Age", SqlDbType.Int).Value =
                    string.IsNullOrEmpty(txtDAge.Text) ? (object)DBNull.Value : Convert.ToInt32(txtDAge.Text);

                insertCmd.Parameters.AddWithValue("@Gender", ddlDGender.SelectedValue);

                insertCmd.Parameters.Add("@DeathDate", SqlDbType.Date).Value =
                    string.IsNullOrEmpty(txtDeathDate.Text) ? (object)DBNull.Value : Convert.ToDateTime(txtDeathDate.Text);

                insertCmd.Parameters.Add("@DeathTime", SqlDbType.Time).Value =
                    string.IsNullOrEmpty(txtDeathTime.Text) ? (object)DBNull.Value : TimeSpan.Parse(txtDeathTime.Text);

                insertCmd.Parameters.AddWithValue("@Place", txtPlace.Text);
                insertCmd.Parameters.AddWithValue("@Cause", txtCause.Text);
                insertCmd.Parameters.AddWithValue("@Aadhaar", txtAadhaar.Text);

                insertCmd.Parameters.AddWithValue("@DonorId", (object)donorId ?? DBNull.Value);
                insertCmd.Parameters.AddWithValue("@IsReg", donorId != null ? 1 : 0);
                insertCmd.Parameters.AddWithValue("@CreatedBy", Session["uid"].ToString() ?? "Admin");
                insertCmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now); 
                int cadaverId = Convert.ToInt32(insertCmd.ExecuteScalar());

                // Witness insert
                SqlCommand cmdW1Insert = new SqlCommand(@"
            INSERT INTO CadaverWitnesses
            (CadaverId, WitnessType, Name, Address, Relationship, MobileOrEmail, AadhaarNumber)
            VALUES
            (@Cid,'Witness1',@Name,@Address,@Relation,@Mobile,@Aadhaar)", con, trans);

                cmdW1Insert.Parameters.AddWithValue("@Cid", cadaverId);
                cmdW1Insert.Parameters.AddWithValue("@Name", txtCW1Name.Text);
                cmdW1Insert.Parameters.AddWithValue("@Address", txtCW1Address.Text);
                cmdW1Insert.Parameters.AddWithValue("@Relation", txtCW1Relation.Text);
                cmdW1Insert.Parameters.AddWithValue("@Mobile", txtCW1Mobile.Text);
                cmdW1Insert.Parameters.AddWithValue("@Aadhaar", txtCW1Aadhaar.Text);

                cmdW1Insert.ExecuteNonQuery();

                // Next of kin insert
                if (!string.IsNullOrEmpty(txtCW2Name.Text))
                {
                    SqlCommand cmdW2Insert = new SqlCommand(@"
                INSERT INTO CadaverWitnesses
                (CadaverId, WitnessType, Name, Address, Relationship, MobileOrEmail, AadhaarNumber)
                VALUES
                (@Cid,'NextOfKin',@Name,@Address,@Relation,@Mobile,@Aadhaar)", con, trans);

                    cmdW2Insert.Parameters.AddWithValue("@Cid", cadaverId);
                    cmdW2Insert.Parameters.AddWithValue("@Name", txtCW2Name.Text);
                    cmdW2Insert.Parameters.AddWithValue("@Address", txtCW2Address.Text);
                    cmdW2Insert.Parameters.AddWithValue("@Relation", txtCW2Relation.Text);
                    cmdW2Insert.Parameters.AddWithValue("@Mobile", txtCW2Mobile.Text);
                    cmdW2Insert.Parameters.AddWithValue("@Aadhaar", txtCW2Aadhaar.Text);

                    cmdW2Insert.ExecuteNonQuery();
                }

                // Documents
                InsertDoc(con, trans, cadaverId, 1, SaveFile(fuDeath, "Death"));
                InsertDoc(con, trans, cadaverId, 2, SaveFile(fuID, "ID"));
                InsertDoc(con, trans, cadaverId, 3, SaveFile(fuPhoto, "Photo"));
                InsertDoc(con, trans, cadaverId, 4, SaveFile(fuPolice, "Police"));
                InsertDoc(con, trans, cadaverId, 5, SaveFile(fuVolontaryBodyDonation, "VolontaryBodyDonation"));
                InsertDoc(con, trans, cadaverId, 6, SaveFile(fuThanksLetter, "ThanksLetter"));

                trans.Commit();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ok",
                    "alert('Saved Successfully');", true);

                ClearCadaverForm();
                BindGrid();
            }
            catch (Exception ex)
            {
                trans.Rollback();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "err",
                    "alert('Error: " + ex.Message.Replace("'", "") + "');", true);
            }
        }
    }
    private void ClearCadaverForm()
    {
        // 🔹 Search
        txtSearchValue.Text = "";

        // 🔹 Deceased
        txtDName.Text = "";
        txtDDOB.Text = "";
        txtDAge.Text = "";
        ddlDGender.SelectedIndex = 0;
        txtDeathDate.Text = "";
        txtDeathTime.Text = "";
        txtPlace.Text = "";
        txtCause.Text = "";
        txtAadhaar.Text = "";

        // 🔹 Witness 1
        txtCW1Name.Text = "";
        txtCW1Relation.Text = "";
        txtCW1Mobile.Text = "";
        txtCW1Address.Text = "";
        txtCW1Aadhaar.Text = "";

        // 🔹 Next Of Kin
        txtCW2Name.Text = "";
        txtCW2Relation.Text = "";
        txtCW2Mobile.Text = "";
        txtCW2Address.Text = "";
        txtCW2Aadhaar.Text = "";

        // 🔹 Unlock fields
        txtDName.ReadOnly = false;
        txtDDOB.ReadOnly = false;
        txtDAge.ReadOnly = false;
        ddlDGender.Enabled = true;

        // 🔹 Reset donor
        ViewState["DonorId"] = null;
    }
    private DataTable GetCadaverData(string search = "")
    {
        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(cs))
        {
            string query = @"
        SELECT 
            C.CadaverId,
            BD.DonorId,
            C.CadaverCode,
            C.Name,
            C.AadhaarNumber,
            C.Gender,
            C.DateOfDeath,
            C.PlaceOfDeath,
            C.CauseOfDeath,
            C.CreatedAt,

            CASE 
                WHEN C.IsRegisteredDonor = 1 THEN 'Yes'
                ELSE 'No'
            END AS RegisteredDonor,

            ISNULL(SM.StatusName, 'Not Received') AS StatusName

        FROM Cadavers C

        LEFT JOIN BodyDonors BD 
            ON C.DonorId = BD.Id

        LEFT JOIN StatusMaster SM 
            ON SM.StatusId = C.StatusId

        WHERE
        (
            @search IS NULL OR @search = ''
            OR C.CadaverCode LIKE '%' + @search + '%'
            OR C.AadhaarNumber LIKE '%' + @search + '%'
            OR C.Name LIKE '%' + @search + '%'
            OR CAST(BD.DonorId AS NVARCHAR) LIKE '%' + @search + '%'
        )

        ORDER BY C.CreatedAt DESC";

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

        // Hide Action Column safely
        if (grdDonors.Columns.Count > 0)
        {
            grdDonors.Columns[grdDonors.Columns.Count - 1].Visible = false;
        }

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=CadaverList.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            grdDonors.RenderControl(hw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        grdDonors.AllowPaging = isPaging;

        if (grdDonors.Columns.Count > 0)
        {
            grdDonors.Columns[grdDonors.Columns.Count - 1].Visible = true;
        }

        BindGrid();
    }
    // GRID BIND
    //    private void BindGrid()
    //    {
    //        SqlDataAdapter da = new SqlDataAdapter(@"
    //       SELECT 
    //    C.CadaverId,
    //    D.DonorId,
    //    C.CadaverCode,
    //    C.Name,
    //    C.AadhaarNumber,
    //    C.Gender,
    //    C.DateOfDeath,
    //    C.PlaceOfDeath,
    //    C.CauseOfDeath,
    //    C.CreatedAt,

    //    -- Registered Donor
    //    CASE 
    //        WHEN C.IsRegisteredDonor = 1 THEN 'Yes'
    //        ELSE 'No'
    //    END AS RegisteredDonor,

    //    -- Status Name
    //    CASE 
    //        WHEN C.StatusId IS NULL THEN 'Not Received'
    //        ELSE SM.StatusName
    //    END AS StatusName

    //FROM Cadavers C

    //LEFT JOIN BodyDonors D 
    //    ON C.DonorId = D.Id

    //LEFT JOIN StatusMaster SM 
    //    ON SM.StatusId = C.StatusId

    //ORDER BY C.CreatedAt DESC;
    //    ", con1);

    //        DataTable dt = new DataTable();
    //        da.Fill(dt);

    //        grdDonors.DataSource = dt;
    //        grdDonors.DataBind();
    //    }
    protected void grdDonors_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDonors.PageIndex = e.NewPageIndex;

        BindGrid(); // 🔥 data reload
    }
    // EXPORT
//    protected void btnexporttoexcel_Click(object sender, EventArgs e)
//    {
//        bool isPaging = grdDonors.AllowPaging;

//        grdDonors.AllowPaging = false;

//        DataTable dt;

//        // 🔥 Check if search data exists
//        if (ViewState["SearchData"] != null)
//        {
//            dt = (DataTable)ViewState["SearchData"];
//        }
//        else
//        {
//            // fallback full data
//            SqlDataAdapter da = new SqlDataAdapter(@"
//         SELECT 
//    C.CadaverId,
//    D.DonorId,
//    C.CadaverCode,
//    C.Name,
//    C.AadhaarNumber,
//    C.Gender,
//    C.DateOfDeath,
//    C.PlaceOfDeath,
//    C.CauseOfDeath,
//    C.CreatedAt,

//    -- Registered Donor
//    CASE 
//        WHEN C.IsRegisteredDonor = 1 THEN 'Yes'
//        ELSE 'No'
//    END AS RegisteredDonor,

//    -- Status Name
//    CASE 
//        WHEN C.StatusId IS NULL THEN 'Not Received'
//        ELSE SM.StatusName
//    END AS StatusName

//FROM Cadavers C

//LEFT JOIN BodyDonors D 
//    ON C.DonorId = D.Id

//LEFT JOIN StatusMaster SM 
//    ON SM.StatusId = C.StatusId

//ORDER BY C.CreatedAt DESC", con1);

//            dt = new DataTable();
//            da.Fill(dt);
//        }

//        grdDonors.DataSource = dt;
//        grdDonors.DataBind();

//        // 🔥 Hide Action column
//        grdDonors.Columns[grdDonors.Columns.Count - 1].Visible = false;
//        grdDonors.PagerSettings.Visible = false;

//        Response.Clear();
//        Response.AddHeader("content-disposition", "attachment;filename=CadaverList.xls");
//        Response.ContentType = "application/vnd.ms-excel";

//        System.IO.StringWriter sw = new System.IO.StringWriter();
//        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);

//        grdDonors.RenderControl(hw);

//        Response.Write(sw.ToString());
//        Response.End();

//        // Restore
//        grdDonors.AllowPaging = isPaging;
//        grdDonors.Columns[grdDonors.Columns.Count - 1].Visible = true;
//        grdDonors.PagerSettings.Visible = true;

//        BindGrid();
//    }

    public override void VerifyRenderingInServerForm(Control control) { }
//    protected void btnSearch_Click(object sender, EventArgs e)
//    {
//        using (SqlConnection con = new SqlConnection(cs))
//        {
//            string search = txtDonorNo.Text.Trim();

//            string query = @"
//        SELECT 
//    C.CadaverId,
//    C.CadaverCode,
//    BD.DonorId,
//    C.AadhaarNumber,
//    C.Name,
//    C.Gender,
//    C.DateOfDeath,
//    C.PlaceOfDeath,
//    C.CauseOfDeath,

//    -- Donor info
//    BD.DonorId AS DonorRegNo,
//    BD.Name AS DonorName,

//    -- Registered Donor
//    CASE 
//        WHEN C.IsRegisteredDonor = 1 THEN 'Yes'
//        ELSE 'No'
//    END AS RegisteredDonor,

//    -- Status
//    ISNULL(SM.StatusName, 'Not Received') AS StatusName

//FROM Cadavers C

//LEFT JOIN BodyDonors BD 
//    ON C.DonorId = BD.Id

//LEFT JOIN StatusMaster SM 
//    ON SM.StatusId = C.StatusId

//WHERE 
//(
//    @search IS NULL OR @search = ''
//    OR C.CadaverCode LIKE '%' + @search + '%'
//    OR C.AadhaarNumber LIKE '%' + @search + '%'
//    OR C.Name LIKE '%' + @search + '%'
//    OR CAST(BD.DonorId AS NVARCHAR) LIKE '%' + @search + '%'
//    OR BD.Name LIKE '%' + @search + '%'
//)

//ORDER BY C.CadaverId DESC";

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
    protected void grdDonors_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int cadaverId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "CadaverId"));

            // =========================
            // 🔹 FIND CONTROLS
            // =========================
            HyperLink lnkDeath = (HyperLink)e.Row.FindControl("lnkDeathDoc");
            HyperLink lnkID = (HyperLink)e.Row.FindControl("lnkIDDoc");
            HyperLink lnkPhoto = (HyperLink)e.Row.FindControl("lnkPhotoDoc");
            HyperLink lnkPolice = (HyperLink)e.Row.FindControl("lnkPoliceDoc");
            HyperLink lnkVolontaryBodyDonation = (HyperLink)e.Row.FindControl("lnkVolontaryBodyDonationDoc");
            HyperLink lnkThanksLetter = (HyperLink)e.Row.FindControl("lnkThanksLetterDoc");

            LinkButton btnReceive = (LinkButton)e.Row.FindControl("btnReceive");

            // =========================
            // 🔹 DEFAULT TEXT
            // =========================
            lnkDeath.Text = "Death : N/A";
            lnkID.Text = "ID : N/A";
            lnkPhoto.Text = "Photo : N/A";
            lnkPolice.Text = "Police : N/A";

            lnkVolontaryBodyDonation.Text = "Volontary : N/A";
            lnkThanksLetter.Text = "ThanksLetter : N/A";
            // =========================
            // 🔹 LOAD DATA
            // =========================
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                // =========================
                // 🔹 DOCUMENTS
                // =========================
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

                // =========================
                // 🔹 DISABLE N/A LINKS
                // =========================
                if (lnkDeath.Text.Contains("N/A")) lnkDeath.Enabled = false;
                if (lnkID.Text.Contains("N/A")) lnkID.Enabled = false;
                if (lnkPhoto.Text.Contains("N/A")) lnkPhoto.Enabled = false;
                if (lnkPolice.Text.Contains("N/A")) lnkPolice.Enabled = false;
                if (lnkVolontaryBodyDonation.Text.Contains("N/A")) lnkVolontaryBodyDonation.Enabled = false;
                if (lnkThanksLetter.Text.Contains("N/A")) lnkThanksLetter.Enabled = false;

                // =========================
                // 🔥 RECEIVE BUTTON LOGIC
                // =========================
                SqlCommand cmdReceive = new SqlCommand(
                    "SELECT COUNT(*) FROM CadaverReception WHERE CadaverId=@Id", con);

                cmdReceive.Parameters.AddWithValue("@Id", cadaverId);

                int count = (int)cmdReceive.ExecuteScalar();

                if (count > 0)
                {
                    btnReceive.Text = "Received";
                    btnReceive.Enabled = false;
                    btnReceive.CssClass = "btn btn-secondary btn-sm";
                }
                else
                {
                    btnReceive.Text = "Receive";
                    btnReceive.Enabled = true;
                    btnReceive.CssClass = "btn btn-success btn-sm";
                }
            }
        }
    }
    protected void grdDonors_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewCadaver")
        {
            int cadaverId = Convert.ToInt32(e.CommandArgument);

            LoadCadaverView(cadaverId);

            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "Popup", "$('#cadaverViewModal').modal('show');", true);
        }

        if (e.CommandName == "EditCadaver")
        {
            int cadaverId = Convert.ToInt32(e.CommandArgument);

            LoadCadaverForEdit(cadaverId);

            btnSaveCadaver.Text = "Update Body";

            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "Popup", "$('#CadaverModal').modal('show');", true);
        }
        if (e.CommandName == "ReceiveCadaver")
        {
            int cadaverId = Convert.ToInt32(e.CommandArgument);

            OpenReceptionModal(cadaverId);
        }
    }
    private void LoadCadaverForEdit(int id)
    {
        using (SqlConnection con = new SqlConnection(cs))
        {
            con.Open();

            // =========================
            // 🔹 Cadaver
            // =========================
            SqlCommand cmd = new SqlCommand("SELECT * FROM Cadavers WHERE CadaverId=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                txtDName.Text = dr["Name"].ToString();
                txtDDOB.Text = dr["DOB"] == DBNull.Value ? "" : Convert.ToDateTime(dr["DOB"]).ToString("yyyy-MM-dd");
                txtDAge.Text = dr["Age"].ToString();
                ddlDGender.SelectedValue = dr["Gender"].ToString();

                txtDeathDate.Text = dr["DateOfDeath"] == DBNull.Value ? "" : Convert.ToDateTime(dr["DateOfDeath"]).ToString("yyyy-MM-dd");
                txtDeathTime.Text = dr["TimeOfDeath"] == DBNull.Value ? "" : dr["TimeOfDeath"].ToString();

                txtPlace.Text = dr["PlaceOfDeath"].ToString();
                txtCause.Text = dr["CauseOfDeath"].ToString();
                txtAadhaar.Text = dr["AadhaarNumber"].ToString();
            }
            dr.Close();

            // =========================
            // 🔹 Witness Load
            // =========================
            SqlCommand cmdW = new SqlCommand("SELECT * FROM CadaverWitnesses WHERE CadaverId=@Id", con);
            cmdW.Parameters.AddWithValue("@Id", id);

            SqlDataReader drW = cmdW.ExecuteReader();

            while (drW.Read())
            {
                string type = drW["WitnessType"].ToString();

                if (type == "Witness1")
                {
                    txtCW1Name.Text = drW["Name"].ToString();
                    txtCW1Address.Text = drW["Address"].ToString();
                    txtCW1Relation.Text = drW["Relationship"].ToString();
                    txtCW1Mobile.Text = drW["MobileOrEmail"].ToString();
                    txtCW1Aadhaar.Text = drW["AadhaarNumber"].ToString();
                }
                else if (type == "NextOfKin")
                {
                    txtCW2Name.Text = drW["Name"].ToString();
                    txtCW2Address.Text = drW["Address"].ToString();
                    txtCW2Relation.Text = drW["Relationship"].ToString();
                    txtCW2Mobile.Text = drW["MobileOrEmail"].ToString();
                    txtCW2Aadhaar.Text = drW["AadhaarNumber"].ToString();
                }
            }

            drW.Close();

            ViewState["EditCadaverId"] = id;
        }
    }
    private void ClearLabels()
    {
        lblCName.Text = "";
        lblCGender.Text = "";
        lblCAge.Text = "";
        lblCDOB.Text = "";
        lblDeathDate.Text = "";
        lblDeathTime.Text = "";
        lblPlace.Text = "";
        lblCause.Text = "";
        lblCAadhaar.Text = "";

        lblCW1Name.Text = "";
        lblCW1Address.Text = "";
        lblCW1Relation.Text = "";
        lblCW1Mobile.Text = "";
        lblCW1Aadhaar.Text = "";
        hlCW1Aadhaar.NavigateUrl = "";
        hlCW1Aadhaar.Text = "";
        hlCW2Aadhaar.NavigateUrl = "";
        hlCW2Aadhaar.Text = "";
        lblCW2Name.Text = "";
        lblCW2Address.Text = "";
        lblCW2Relation.Text = "";
        lblCW2Mobile.Text = "";
        lblCW2Aadhaar.Text = "";

        // 🔥 DOCUMENT LINKS CLEAR
        lnkDeath.NavigateUrl = "";
        lnkID.NavigateUrl = "";
        lnkPhoto.NavigateUrl = "";
        lnkPolice.NavigateUrl = "";
        lnkThanksLetter.NavigateUrl = "";
        lnkVolontaryBodyDonation.NavigateUrl = "";
        lnkDeath.Text = "";
        lnkID.Text = "";
        lnkPhoto.Text = "";
        lnkPolice.Text = "";
        lnkThanksLetter.Text = "";
        lnkVolontaryBodyDonation.Text = "";
    }
    private void LoadCadaverView(int id)
    {
        using (SqlConnection con = new SqlConnection(cs))
        {
            con.Open();

            // 🔥 पहले clear करो (IMPORTANT)
            ClearLabels();

            // =========================
            // 🔹 Cadaver
            // =========================
            SqlCommand cmd = new SqlCommand("SELECT * FROM Cadavers WHERE CadaverId=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                lblCName.Text = dr["Name"].ToString();
                lblCGender.Text = dr["Gender"].ToString();
                lblCAge.Text = dr["Age"].ToString();

                if (dr["DOB"] != DBNull.Value)
                    lblCDOB.Text = Convert.ToDateTime(dr["DOB"]).ToString("dd-MM-yyyy");
                else
                    lblCDOB.Text = "";

                if (dr["DateOfDeath"] != DBNull.Value)
                    lblDeathDate.Text = Convert.ToDateTime(dr["DateOfDeath"]).ToString("dd-MM-yyyy");
                else
                    lblDeathDate.Text = "";

                if (dr["TimeOfDeath"] != DBNull.Value)
                    lblDeathTime.Text = dr["TimeOfDeath"].ToString();
                else
                    lblDeathTime.Text = "";

                if (dr["PlaceOfDeath"] != DBNull.Value)
                    lblPlace.Text = dr["PlaceOfDeath"].ToString();
                else
                    lblPlace.Text = "";

                if (dr["CauseOfDeath"] != DBNull.Value)
                    lblCause.Text = dr["CauseOfDeath"].ToString();
                else
                    lblCause.Text = "";

                if (dr["AadhaarNumber"] != DBNull.Value)
                    lblCAadhaar.Text = dr["AadhaarNumber"].ToString();
                else
                    lblCAadhaar.Text = "";
            }

            dr.Close();

            // =========================
            // 🔹 Witness
            // =========================
            SqlCommand cmdW = new SqlCommand("SELECT * FROM CadaverWitnesses WHERE CadaverId=@Id", con);
            cmdW.Parameters.AddWithValue("@Id", id);

            SqlDataReader drW = cmdW.ExecuteReader();

            while (drW.Read())
            {
                string type = drW["WitnessType"].ToString();

                if (type == "Witness1")
                {
                    lblCW1Name.Text = drW["Name"] != DBNull.Value ? drW["Name"].ToString() : "";
                    lblCW1Address.Text = drW["Address"] != DBNull.Value ? drW["Address"].ToString() : "";
                    lblCW1Relation.Text = drW["Relationship"] != DBNull.Value ? drW["Relationship"].ToString() : "";
                    lblCW1Mobile.Text = drW["MobileOrEmail"] != DBNull.Value ? drW["MobileOrEmail"].ToString() : "";
                    lblCW1Aadhaar.Text = drW["AadhaarNumber"] != DBNull.Value ? drW["AadhaarNumber"].ToString() : "";
                    hlCW1Aadhaar.NavigateUrl= drW["AadhaarFilePath"] != DBNull.Value ? drW["AadhaarFilePath"].ToString() : "";
                    hlCW1Aadhaar.Text= drW["AadhaarFilePath"] != DBNull.Value ? "Download" : "";
                }
                else if (type == "NextOfKin")
                {
                    lblCW2Name.Text = drW["Name"] != DBNull.Value ? drW["Name"].ToString() : "";
                    lblCW2Address.Text = drW["Address"] != DBNull.Value ? drW["Address"].ToString() : "";
                    lblCW2Relation.Text = drW["Relationship"] != DBNull.Value ? drW["Relationship"].ToString() : "";
                    lblCW2Mobile.Text = drW["MobileOrEmail"] != DBNull.Value ? drW["MobileOrEmail"].ToString() : "";
                    lblCW2Aadhaar.Text = drW["AadhaarNumber"] != DBNull.Value ? drW["AadhaarNumber"].ToString() : "";
                    hlCW2Aadhaar.NavigateUrl = drW["AadhaarFilePath"] != DBNull.Value ? drW["AadhaarFilePath"].ToString() : "";
                    hlCW2Aadhaar.Text = drW["AadhaarFilePath"] != DBNull.Value ? "Download" : "";
                }
            }

            drW.Close();

            // =========================
            // 🔹 Documents (🔥 NEW ADD)
            // =========================
            SqlCommand cmdDoc = new SqlCommand(@"
        SELECT DocumentId, FilePath 
        FROM CadaverDocuments 
        WHERE CadaverId=@Id", con);

            cmdDoc.Parameters.AddWithValue("@Id", id);

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
                else if (docId == 5) // VolontaryBodyDonation
                {
                    lnkVolontaryBodyDonation.NavigateUrl = path;
                    lnkVolontaryBodyDonation.Text = "⬇ Download";
                    lnkVolontaryBodyDonation.Enabled = true;
                }
                else if (docId == 6) // ThanksLetter
                {
                    lnkThanksLetter.NavigateUrl = path;
                    lnkThanksLetter.Text = "⬇ Download";
                    lnkThanksLetter.Enabled = true;
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
            if (lnkVolontaryBodyDonation.Text == "")
            {
                lnkVolontaryBodyDonation.Text = "Not Uploaded";
                lnkVolontaryBodyDonation.Enabled = false;
            }

            if (lnkThanksLetter.Text == "")
            {
                lnkThanksLetter.Text = "Not Uploaded";
                lnkThanksLetter.Enabled = false;
            }
        }
    }
    private void OpenReceptionModal(int cadaverId)
    {
        BindSourceDropdown();

        // 🔥 Cadaver dropdown bind
        using (SqlConnection con = new SqlConnection(cs))
        {
            SqlCommand cmd = new SqlCommand(@"
        SELECT CadaverId, Name + ' (' + CAST(CadaverId AS NVARCHAR(10)) + ')' AS DisplayName
        FROM Cadavers
        WHERE CadaverId=@Id", con);

            cmd.Parameters.AddWithValue("@Id", cadaverId);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            ddlCadaver.Items.Clear();

            if (dr.Read())
            {
                ddlCadaver.Items.Add(new ListItem(
                    dr["DisplayName"].ToString(),
                    dr["CadaverId"].ToString()
                ));
            }

            dr.Close();
        }

        // 🔒 Lock dropdown
        ddlCadaver.Enabled = false;

        // 🔥 Auto date set
        txtArrival.Text = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");

        // 🔥 Modal open
        ScriptManager.RegisterStartupScript(this, this.GetType(),
            "Popup", "$('#ReceptionModal').modal('show');", true);
    }
    private void ClearReceptionForm()
    {
        ddlCadaver.SelectedIndex = 0;
        ddlSource.SelectedIndex = 0;
        txtArrival.Text = "";
        txtIdentification.Text = "";
        txtConsent.Text = "";
        txtWitness.Text = "";
    }
    private void ResetReceptionAfterSave()
    {
        ddlCadaver.Enabled = true;
        ClearReceptionForm();
    }
    private void BindCadaverDropdown()
    {
        using (SqlConnection con = new SqlConnection(cs))
        {
            SqlDataAdapter da = new SqlDataAdapter(@"
        SELECT 
    CadaverId, 
    Name + ' (' + CAST(CadaverId AS NVARCHAR(10)) + ')' AS DisplayName
FROM Cadavers
WHERE CadaverId NOT IN (SELECT CadaverId FROM CadaverReception)
        ", con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlCadaver.DataSource = dt;
            ddlCadaver.DataTextField = "DisplayName";
            ddlCadaver.DataValueField = "CadaverId";
            ddlCadaver.DataBind();

            ddlCadaver.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
    }
    private void BindSourceDropdown()
    {
        using (SqlConnection con = new SqlConnection(cs))
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT SourceId, SourceName FROM SourceMaster", con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlSource.DataSource = dt;
            ddlSource.DataTextField = "SourceName";
            ddlSource.DataValueField = "SourceId";
            ddlSource.DataBind();

            ddlSource.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
    }
     private void Alert(string msg)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(),
            "alert", "alert('" + msg.Replace("'", "") + "');", true);
    }
    protected void btnSaveReception_Click(object sender, EventArgs e)
    {
        if (ddlSource.SelectedValue == "0")
        {
            Alert("Select Source");
            return;
        }

        if (txtWitness.Text.Length != 12)
        {
            Alert("Invalid Aadhaar");
            return;
        }

        using (SqlConnection con = new SqlConnection(cs))
        {
            using (SqlCommand cmd = new SqlCommand("ReceiveCadaver", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CadaverId", ddlCadaver.SelectedValue);
                cmd.Parameters.AddWithValue("@ArrivalDateTime", Convert.ToDateTime(txtArrival.Text));
                cmd.Parameters.AddWithValue("@SourceId", ddlSource.SelectedValue);
                cmd.Parameters.AddWithValue("@IdentificationDetails", txtIdentification.Text);
                cmd.Parameters.AddWithValue("@ConsentDetails", txtConsent.Text);
                cmd.Parameters.AddWithValue("@WitnessAadhaar", txtWitness.Text);
                cmd.Parameters.AddWithValue("@CreatedBy", "Admin");

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    Alert("✅ Body Received Successfully");

                    ResetReceptionAfterSave();
                    BindGrid(); // 🔥 Refresh grid
                }
                catch (Exception ex)
                {
                    Alert(ex.Message);
                }
            }
        }
    }
}