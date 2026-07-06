using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class Faculty_BodyTransfertoAnatomyDepartment : System.Web.UI.Page
{
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    string cs = ConfigurationManager.ConnectionStrings["TMUCON"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
            pnlDonor.Visible = false;
            
        }
       
    }
    private void BindGrid()
    {
        SqlDataAdapter da = new SqlDataAdapter(@"
    SELECT 
        cu.UsageId,
        cu.CadaverId,
        cu.StorageId, -- 🔥 IMPORTANT

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

    -- 🔥 FIXED JOIN (MOST IMPORTANT)
    LEFT JOIN CadaverStorage cs 
        ON cu.StorageId = cs.StorageId

    LEFT JOIN BodyDonors bd 
        ON c.DonorId = bd.Id

    ORDER BY cu.StartDate DESC
    ", con1);

        DataTable dt = new DataTable();
        da.Fill(dt);

        grdBodyStorage.DataSource = dt;
        grdBodyStorage.DataBind();
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        ClearUseForm();
        txtStartDate.Text = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");
        ScriptManager.RegisterStartupScript(this, this.GetType(),
            "Popup", "$('#UseModal').modal('show');", true);
    }
    protected void btnSearchUse_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
       

        using (SqlConnection con = new SqlConnection(cs))
        {
            con.Open();

            // =========================
            // 🔴 1. GET CADAVER FROM ACTIVE STORAGE
            // =========================
            SqlCommand cmd = new SqlCommand(@"
        SELECT TOP 1 
            cs.CadaverId,
            cs.StorageId,
            c.CadaverCode,
            c.Name,
            c.AadhaarNumber,
            c.StatusId
        FROM CadaverStorage cs
        INNER JOIN Cadavers c ON cs.CadaverId = c.CadaverId
        WHERE cs.IsActive = 1
        AND (
            c.CadaverCode = @Search 
            OR c.AadhaarNumber = @Search
        )", con);

            cmd.Parameters.AddWithValue("@Search", txtSearchUse.Text.Trim());

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                int cadaverId = Convert.ToInt32(dr["CadaverId"]);
                int statusId = Convert.ToInt32(dr["StatusId"]);

                // =========================
                // 🔴 2. STATUS CHECK
                // =========================
                if (statusId != 3) // Only Stored allowed
                {
                    dr.Close();
                    Alert("❌ Only stored cadaver can be used");
                    return;
                }

                // =========================
                // 🔴 3. CHECK ALREADY IN USE
                // =========================
                dr.Close();

                SqlCommand check = new SqlCommand(@"
            SELECT COUNT(*) 
            FROM CadaverUsage 
            WHERE CadaverId=@Id AND StatusId=4", con);

                check.Parameters.AddWithValue("@Id", cadaverId);

                if ((int)check.ExecuteScalar() > 0)
                {
                    Alert("⚠ Already in use");
                    return;
                }

                // =========================
                // 🟢 4. LOAD INTO FORM
                // =========================
                SqlCommand load = new SqlCommand(@"
            SELECT CadaverCode, Name, AadhaarNumber
            FROM Cadavers WHERE CadaverId=@Id", con);

                load.Parameters.AddWithValue("@Id", cadaverId);

                SqlDataReader dr2 = load.ExecuteReader();

                if (dr2.Read())
                {
                    ViewState["CadaverId"] = cadaverId;

                    txtUseCadaverCode.Text = dr2["CadaverCode"].ToString();
                    txtUseName.Text = dr2["Name"].ToString();
                    txtUseAadhaar.Text = dr2["AadhaarNumber"].ToString();
                }

                dr2.Close();
            }
            else
            {
                Alert("❌ Cadaver not found in storage");
            }
        }

        // 🔥 Modal reopen
        ScriptManager.RegisterStartupScript(this, this.GetType(),
            "Popup", "$('#UseModal').modal('show');", true);
    }
    protected void btnSaveUse_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        if (ViewState["CadaverId"] == null)
        {
            Alert("Search first");
            return;
        }     

        int cadaverId = Convert.ToInt32(ViewState["CadaverId"]);

        DateTime startDate = DateTime.Now; // 🔥 server time (safe)

        using (SqlConnection con = new SqlConnection(cs))
        {
            con.Open();
            SqlTransaction trans = con.BeginTransaction();

            try
            {
                // =========================
                // 🔴 1. VALIDATE STATUS
                // =========================
                SqlCommand st = new SqlCommand(
                "SELECT StatusId FROM Cadavers WHERE CadaverId=@Id",
                con, trans);

                st.Parameters.AddWithValue("@Id", cadaverId);

                int statusId = (int)st.ExecuteScalar();

                if (statusId != 3) // Only Stored allowed
                {
                    throw new Exception("❌ Only stored cadaver can be used");
                }

                // =========================
                // 🔴 2. CHECK ALREADY IN USE
                // =========================
                SqlCommand check = new SqlCommand(@"
            SELECT COUNT(*) 
            FROM CadaverUsage 
            WHERE CadaverId=@Id AND StatusId=4", con, trans);

                check.Parameters.AddWithValue("@Id", cadaverId);

                if ((int)check.ExecuteScalar() > 0)
                    throw new Exception("⚠ Already in use");

                // =========================
                // 🔴 3. GET ACTIVE STORAGE
                // =========================
                SqlCommand getStorage = new SqlCommand(@"
            SELECT TOP 1 StorageId 
            FROM CadaverStorage 
            WHERE CadaverId=@Id AND IsActive=1", con, trans);

                getStorage.Parameters.AddWithValue("@Id", cadaverId);

                object result = getStorage.ExecuteScalar();

                if (result == null)
                    throw new Exception("❌ Storage not found");

                int storageId = Convert.ToInt32(result);

                // =========================
                // 🔴 4. RELEASE STORAGE
                // =========================
                SqlCommand release = new SqlCommand(@"
            UPDATE CadaverStorage
            SET IsActive = 0,
                ReleasedAt = GETDATE()
            WHERE StorageId=@Sid", con, trans);

                release.Parameters.AddWithValue("@Sid", storageId);
                release.ExecuteNonQuery();

                // =========================
                // 🟢 5. INSERT USAGE
                // =========================
                SqlCommand cmd = new SqlCommand(@"
            INSERT INTO CadaverUsage
            (CadaverId, StorageId, Department, Batch, DissectionHall, InChargeName, StartDate, StatusId, CreatedBy)
            VALUES
            (@Cid, @StorageId, @Dept, @Batch, @DH, @Incharge, @Start, 4, @CreatedBy)
            ", con, trans);

                cmd.Parameters.AddWithValue("@Cid", cadaverId);
                cmd.Parameters.AddWithValue("@StorageId", storageId);
                cmd.Parameters.AddWithValue("@Dept", txtDepartment.Text.Trim());
                cmd.Parameters.AddWithValue("@Batch", txtBatch.Text.Trim());
                cmd.Parameters.AddWithValue("@DH", ddlDH.SelectedValue);
                cmd.Parameters.AddWithValue("@Incharge", txtIncharge.Text.Trim());
                cmd.Parameters.AddWithValue("@Start", startDate);
                cmd.Parameters.AddWithValue("@CreatedBy", Session["uid"] ?? "Admin");

                cmd.ExecuteNonQuery();

                // =========================
                // 🟡 6. UPDATE STATUS
                // =========================
                SqlCommand upd = new SqlCommand(@"
            UPDATE Cadavers 
            SET StatusId=4, UpdatedAt=GETDATE()
            WHERE CadaverId=@Cid", con, trans);

                upd.Parameters.AddWithValue("@Cid", cadaverId);
                upd.ExecuteNonQuery();

                trans.Commit();

                Alert("✅ Moved to In Use");

                ClearUseForm();
                BindGrid();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                Alert(ex.Message);
            }
        }
    }
    private void ClearUseForm()
    {
        // 🔍 Search
        txtSearchUse.Text = "";

        // 📄 Cadaver Details
        txtUseCadaverCode.Text = "";
        txtUseName.Text = "";
        txtUseAadhaar.Text = "";

        // 🧠 Use Details
        txtDepartment.Text = "";
        txtBatch.Text = "";
        ddlDH.SelectedIndex = 0;
        txtIncharge.Text = "";
        txtStartDate.Text = "";

        // 🔥 Reset ViewState
        ViewState["CadaverId"] = null;
    }
    private void Alert(string msg)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(),
            "alert", "alert('" + msg.Replace("'", "") + "');", true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(cs))
        {
            string search = txtDonorNo.Text.Trim();

            string query = @"
        SELECT 
            cu.UsageId,
            cu.CadaverId,
            cu.StorageId, -- 🔥 IMPORTANT

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

        -- 🔥 FIXED JOIN
        LEFT JOIN CadaverStorage cs 
            ON cu.StorageId = cs.StorageId

        LEFT JOIN BodyDonors bd 
            ON c.DonorId = bd.Id

        WHERE 
        (
            @search IS NULL OR @search = ''
            OR c.CadaverCode LIKE '%' + @search + '%'
            OR c.AadhaarNumber LIKE '%' + @search + '%'
            OR c.Name LIKE '%' + @search + '%'
            OR CAST(bd.DonorId AS NVARCHAR) LIKE '%' + @search + '%'
        )

        ORDER BY cu.StartDate DESC
        ";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@search", search);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ViewState["SearchData"] = dt;
            grdBodyStorage.PageIndex = 0;
            grdBodyStorage.DataSource = dt;
            grdBodyStorage.DataBind();
        }
    }
    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        bool isPaging = grdBodyStorage.AllowPaging;

        grdBodyStorage.AllowPaging = false;

        DataTable dt;

        // 🔥 Check if search data exists
        if (ViewState["SearchData"] != null)
        {
            dt = (DataTable)ViewState["SearchData"];
        }
        else
        {
            // fallback full data
            SqlDataAdapter da = new SqlDataAdapter(@"
        SELECT 
        cu.UsageId,
        cu.CadaverId,
        cu.StorageId, -- 🔥 IMPORTANT

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

    -- 🔥 FIXED JOIN (MOST IMPORTANT)
    LEFT JOIN CadaverStorage cs 
        ON cu.StorageId = cs.StorageId

    LEFT JOIN BodyDonors bd 
        ON c.DonorId = bd.Id

    ORDER BY cu.StartDate DESC", con1);

            dt = new DataTable();
            da.Fill(dt);
        }

        grdBodyStorage.DataSource = dt;
        grdBodyStorage.DataBind();

        // 🔥 Hide Action column
        grdBodyStorage.Columns[grdBodyStorage.Columns.Count - 1].Visible = false;
        grdBodyStorage.PagerSettings.Visible = false;

        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=CadaverList.xls");
        Response.ContentType = "application/vnd.ms-excel";

        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);

        grdBodyStorage.RenderControl(hw);

        Response.Write(sw.ToString());
        Response.End();

        // Restore
        grdBodyStorage.AllowPaging = isPaging;
        grdBodyStorage.Columns[grdBodyStorage.Columns.Count - 1].Visible = true;
        grdBodyStorage.PagerSettings.Visible = true;

       // BindGrid();
    }

    public override void VerifyRenderingInServerForm(Control control) { }
    protected void grdBodyStorage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewCadaver")
        {
            int cadaverId = Convert.ToInt32(e.CommandArgument);

            LoadBodyStorage(cadaverId);

            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "Popup", "$('#cadaverViewModal').modal('show');", true);
        }
        if (e.CommandName == "ReleaseStorage")
        {
            string[] data = e.CommandArgument.ToString().Split(',');

            int storageId = Convert.ToInt32(data[0]);
            int cadaverId = Convert.ToInt32(data[1]);

            ReleaseStorage(storageId, cadaverId);
        }
    }
    protected void grdBodyStorage_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdBodyStorage.PageIndex = e.NewPageIndex;

        if (ViewState["SearchData"] != null)
        {
            grdBodyStorage.DataSource = (DataTable)ViewState["SearchData"];
        }
        else
        {
            BindGrid();
        }

        grdBodyStorage.DataBind();
    }
    protected void ReleaseStorage(int storageId, int cadaverId)
    {
        using (SqlConnection con = new SqlConnection(cs))
        {
            con.Open();
            SqlTransaction trans = con.BeginTransaction();

            try
            {
                SqlCommand cmd = new SqlCommand(@"
                UPDATE CadaverStorage
                SET ReleasedAt = GETDATE(),
                    IsActive = 0
                WHERE StorageId=@Id", con, trans);

                cmd.Parameters.AddWithValue("@Id", storageId);
                cmd.ExecuteNonQuery();

                // 🔥 Status = Returned (5)
                SqlCommand upd = new SqlCommand(@"
                UPDATE Cadavers SET StatusId=5 WHERE CadaverId=@Cid",
                    con, trans);

                upd.Parameters.AddWithValue("@Cid", cadaverId);
                upd.ExecuteNonQuery();

                trans.Commit();

                Alert("Released");
                BindGrid();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                Alert(ex.Message);
            }
        }
    }
    private void LoadBodyStorage(int storageId)
    {
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
        WHERE cs.StorageId = @Id", con);

            cmd.Parameters.AddWithValue("@Id", storageId);

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
                }
                else if (j == 2)
                {
                    lblCW2Name.Text = drCW["Name"].ToString();
                    lblCW2Address.Text = drCW["Address"].ToString();
                    lblCW2Relation.Text = drCW["Relationship"].ToString();
                    lblCW2Mobile.Text = drCW["MobileOrEmail"].ToString();
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
        }
    }
    
}