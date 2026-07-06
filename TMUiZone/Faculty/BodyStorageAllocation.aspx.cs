using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_BodyStorageAllocation : System.Web.UI.Page
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
    cs.StorageId,
    cs.CadaverId,
    cs.RoomNumber,
    cs.FreezerNumber,
    cs.RackNumber,
    cs.AllocatedAt,
    cs.ReleasedAt,
    cs.IsActive,

    -- Storage Status
    CASE 
        WHEN cs.IsActive = 1 THEN 'In'
        ELSE 'Out'
    END AS StorageStatus,

    -- Cadaver Info
    c.CadaverNumber,
    c.CadaverCode,
    c.DonorId,
    c.Name,
    c.DOB,
    c.Age,
    c.Gender,
    c.DateOfDeath,
    c.TimeOfDeath,
    c.PlaceOfDeath,
    c.CauseOfDeath,
    c.AadhaarNumber,
    c.StatusId,

    -- Status Name
    ISNULL(sm.StatusName, 'Not Received') AS StatusName,

    -- Donor Info
    bd.DonorId AS DonorIID

FROM CadaverStorage cs

LEFT JOIN Cadavers c 
    ON cs.CadaverId = c.CadaverId

LEFT JOIN BodyDonors bd
    ON c.DonorId = bd.Id

-- 🔥 Status Join
LEFT JOIN StatusMaster sm
    ON sm.StatusId = c.StatusId

ORDER BY cs.AllocatedAt DESC;
    ", con1);

        DataTable dt = new DataTable();
        da.Fill(dt);

        grdBodyStorage.DataSource = dt;
        grdBodyStorage.DataBind();
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        ClearStorageForm();

        ScriptManager.RegisterStartupScript(this, this.GetType(),
            "Popup", "$('#StorageModal').modal('show');", true);
    }
    protected void btnSearchCadaver_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        using (SqlConnection con = new SqlConnection(cs))
        {
            SqlCommand cmd = new SqlCommand(@"
        SELECT CadaverId, CadaverCode, Name, AadhaarNumber, StatusId
        FROM Cadavers
        WHERE 
            CadaverCode=@Search 
            OR AadhaarNumber=@Search", con);

            cmd.Parameters.AddWithValue("@Search", txtSearchCadaver.Text.Trim());

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                int cadaverId = Convert.ToInt32(dr["CadaverId"]);
                int statusId = Convert.ToInt32(dr["StatusId"]);

                // 🔴 RULE
                if (statusId != 2 && statusId != 3 && statusId != 5)
                {
                    Alert("❌ Only Received / Stored / Returned cadaver can be stored");
                    return;
                }

                ViewState["CadaverId"] = cadaverId;

                txtCadaverCode.Text = dr["CadaverCode"].ToString();
                txtName.Text = dr["Name"].ToString();
                txtAadhaar.Text = dr["AadhaarNumber"].ToString();

                dr.Close();

                // 🔥 Load existing storage
                LoadStorage(cadaverId);
            }
            else
            {
                Alert("Cadaver not found");
            }
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(),
            "Popup", "$('#StorageModal').modal('show');", true);
    }
    private void LoadStorage(int cadaverId)
    {
        using (SqlConnection con = new SqlConnection(cs))
        {
            SqlCommand cmd = new SqlCommand(@"
        SELECT TOP 1 *
        FROM CadaverStorage
        WHERE CadaverId=@Id AND IsActive=1", con);

            cmd.Parameters.AddWithValue("@Id", cadaverId);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                txtRoom.Text = dr["RoomNumber"].ToString();
                txtFreezer.Text = dr["FreezerNumber"].ToString();
                txtRack.Text = dr["RackNumber"].ToString();

                btnSaveStorage.Text = "Update Storage";
            }
            else
            {
                txtRoom.Text = "";
                txtFreezer.Text = "";
                txtRack.Text = "";

                btnSaveStorage.Text = "Allocate Storage";
            }
        }
    }
    //protected void btnSaveStorage_Click(object sender, EventArgs e)
    //{
    //    if (ViewState["CadaverId"] == null)
    //    {
    //        Alert("Search cadaver first");
    //        return;
    //    }

    //    int cadaverId = Convert.ToInt32(ViewState["CadaverId"]);

    //    using (SqlConnection con = new SqlConnection(cs))
    //    {
    //        con.Open();
    //        SqlTransaction trans = con.BeginTransaction();

    //        try
    //        {
    //            // Duplicate check
    //            SqlCommand check = new SqlCommand(
    //                "SELECT COUNT(*) FROM CadaverStorage WHERE CadaverId=@Id AND IsActive=1",
    //                con, trans);

    //            check.Parameters.AddWithValue("@Id", cadaverId);

    //            int exists = (int)check.ExecuteScalar();

    //            if (exists > 0)
    //                throw new Exception("Already allocated");

    //            // Insert
    //            SqlCommand cmd = new SqlCommand(@"
    //        INSERT INTO CadaverStorage
    //        (CadaverId, RoomNumber, FreezerNumber, RackNumber, AllocatedAt, IsActive)
    //        VALUES
    //        (@Cid, @Room, @Freezer, @Rack, GETDATE(), 1)
    //        ", con, trans);

    //            cmd.Parameters.AddWithValue("@Cid", cadaverId);
    //            cmd.Parameters.AddWithValue("@Room", txtRoom.Text);
    //            cmd.Parameters.AddWithValue("@Freezer", txtFreezer.Text);
    //            cmd.Parameters.AddWithValue("@Rack", txtRack.Text);

    //            cmd.ExecuteNonQuery();

    //            // Update status
    //            SqlCommand upd = new SqlCommand(@"
    //        UPDATE Cadavers SET StatusId=3 WHERE CadaverId=@Cid",
    //            con, trans);

    //            upd.Parameters.AddWithValue("@Cid", cadaverId);
    //            upd.ExecuteNonQuery();

    //            trans.Commit();

    //            Alert("✅ Storage Allocated");
    //            ClearStorageForm();
    //        }
    //        catch (Exception ex)
    //        {
    //            trans.Rollback();
    //            Alert(ex.Message);
    //        }
    //    }
    //}
    protected void btnSaveStorage_Click(object sender, EventArgs e)
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

        using (SqlConnection con = new SqlConnection(cs))
        {
            con.Open();
            SqlTransaction trans = con.BeginTransaction();

            try
            {
                // 🔴 1. Status validation
                SqlCommand chk = new SqlCommand(
                    "SELECT StatusId FROM Cadavers WHERE CadaverId=@Id",
                    con, trans);

                chk.Parameters.AddWithValue("@Id", cadaverId);

                int statusId = (int)chk.ExecuteScalar();

                if (statusId != 2 && statusId != 3 && statusId != 5)
                {
                    Alert("❌ Only Received / Stored / Returned allowed");
                    trans.Rollback();
                    return;
                }

                // 🔴 2. Duplicate location check
                SqlCommand dup = new SqlCommand(@"
            SELECT COUNT(*) FROM CadaverStorage
            WHERE RoomNumber=@Room 
            AND FreezerNumber=@Freezer 
            AND RackNumber=@Rack 
            AND IsActive=1", con, trans);

                dup.Parameters.AddWithValue("@Room", txtRoom.Text.Trim());
                dup.Parameters.AddWithValue("@Freezer", txtFreezer.Text.Trim());
                dup.Parameters.AddWithValue("@Rack", txtRack.Text.Trim());

                if ((int)dup.ExecuteScalar() > 0)
                {
                    Alert("⚠ Storage already occupied");
                    trans.Rollback();
                    return;
                }

                // 🔴 3. Deactivate old storage
                SqlCommand old = new SqlCommand(@"
            UPDATE CadaverStorage
            SET IsActive = 0, ReleasedAt = GETDATE()
            WHERE CadaverId=@Cid AND IsActive=1", con, trans);

                old.Parameters.AddWithValue("@Cid", cadaverId);
                old.ExecuteNonQuery();

                // 🟢 4. Insert new storage
                SqlCommand cmd = new SqlCommand(@"
            INSERT INTO CadaverStorage
            (CadaverId, RoomNumber, FreezerNumber, RackNumber)
            VALUES
            (@Cid,@Room,@Freezer,@Rack)", con, trans);

                cmd.Parameters.AddWithValue("@Cid", cadaverId);
                cmd.Parameters.AddWithValue("@Room", txtRoom.Text.Trim());
                cmd.Parameters.AddWithValue("@Freezer", txtFreezer.Text.Trim());
                cmd.Parameters.AddWithValue("@Rack", txtRack.Text.Trim());

                cmd.ExecuteNonQuery();

                // 🟡 5. Update status → Stored
                SqlCommand upd = new SqlCommand(@"
            UPDATE Cadavers
            SET StatusId=3, UpdatedAt=GETDATE()
            WHERE CadaverId=@Cid", con, trans);

                upd.Parameters.AddWithValue("@Cid", cadaverId);
                upd.ExecuteNonQuery();

                trans.Commit();

                Alert("✅ Storage Saved");
                ClearStorageForm();
                BindGrid();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                Alert(ex.Message);
            }
        }
    }
    private void ClearStorageForm()
    {
        txtSearchCadaver.Text = "";
        txtCadaverCode.Text = "";
        txtName.Text = "";
        txtAadhaar.Text = "";
        txtRoom.Text = "";
        txtFreezer.Text = "";
        txtRack.Text = "";

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
    cs.StorageId,
    cs.CadaverId,
    cs.RoomNumber,
    cs.FreezerNumber,
    cs.RackNumber,
    cs.AllocatedAt,
    cs.ReleasedAt,
    cs.IsActive,

    -- Storage Status
    CASE 
        WHEN cs.IsActive = 1 THEN 'In'
        ELSE 'Out'
    END AS StorageStatus,

    -- Cadaver Info
    c.CadaverNumber,
    c.CadaverCode,
    c.DonorId,
    c.Name,
    c.DOB,
    c.Age,
    c.Gender,
    c.DateOfDeath,
    c.TimeOfDeath,
    c.PlaceOfDeath,
    c.CauseOfDeath,
    c.AadhaarNumber,
    c.StatusId,

    -- Status Name
    ISNULL(sm.StatusName, 'Not Received') AS StatusName,

    -- Donor Info
    bd.DonorId AS DonorIID

FROM CadaverStorage cs

LEFT JOIN Cadavers c 
    ON cs.CadaverId = c.CadaverId

LEFT JOIN BodyDonors bd 
    ON c.DonorId = bd.Id

LEFT JOIN StatusMaster sm
    ON sm.StatusId = c.StatusId

WHERE 
(
    @search IS NULL OR @search = ''
    OR c.CadaverCode LIKE '%' + @search + '%'
    OR c.AadhaarNumber LIKE '%' + @search + '%'
    OR c.Name LIKE '%' + @search + '%'
    OR CAST(bd.DonorId AS NVARCHAR) LIKE '%' + @search + '%'
)

ORDER BY cs.AllocatedAt DESC;";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@search", search);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ViewState["SearchData"] = dt;
            grdBodyStorage.PageIndex = 0; // 🔥 pagination reset
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
    cs.StorageId,
    cs.CadaverId,
    cs.RoomNumber,
    cs.FreezerNumber,
    cs.RackNumber,
    cs.AllocatedAt,
    cs.ReleasedAt,
    cs.IsActive,

    -- Storage Status
    CASE 
        WHEN cs.IsActive = 1 THEN 'In'
        ELSE 'Out'
    END AS StorageStatus,

    -- Cadaver Info
    c.CadaverNumber,
    c.CadaverCode,
    c.DonorId,
    c.Name,
    c.DOB,
    c.Age,
    c.Gender,
    c.DateOfDeath,
    c.TimeOfDeath,
    c.PlaceOfDeath,
    c.CauseOfDeath,
    c.AadhaarNumber,
    c.StatusId,

    -- Status Name
    ISNULL(sm.StatusName, 'Not Received') AS StatusName,

    -- Donor Info
    bd.DonorId AS DonorIID

FROM CadaverStorage cs

LEFT JOIN Cadavers c 
    ON cs.CadaverId = c.CadaverId

LEFT JOIN BodyDonors bd
    ON c.DonorId = bd.Id

-- 🔥 Status Join
LEFT JOIN StatusMaster sm
    ON sm.StatusId = c.StatusId

ORDER BY cs.AllocatedAt DESC;", con1);

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