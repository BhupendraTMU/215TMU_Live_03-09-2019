using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Controls;

public partial class Faculty_CadaverRegistration : System.Web.UI.Page
{
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    string cs = ConfigurationManager.ConnectionStrings["TMUCON"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
            CreatePoliceTable();
            txtReceivedDate.Text = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");
        }
    }
    private void CreatePoliceTable()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("SrNo");
        dt.Columns.Add("ConstableNo");
        dt.Columns.Add("MemberName");
        dt.Columns.Add("MobileNo");

        Session["PoliceMembers"] = dt;

        gvPoliceMembers.DataSource = dt;
        gvPoliceMembers.DataBind();
    }
    private string GenerateBodyId()
    {
        string bodyId = "";

        using (SqlConnection con =
            new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
        {
            con.Open();

            int currentYear = DateTime.Now.Year;

            SqlCommand cmd = new SqlCommand(@"

        IF NOT EXISTS
        (
            SELECT 1
            FROM uk_body_sequence
            WHERE sequence_year=@year
        )
        BEGIN
            INSERT INTO uk_body_sequence
            (
                sequence_year,
                last_no
            )
            VALUES
            (
                @year,
                0
            )
        END

        UPDATE uk_body_sequence
        SET last_no = last_no + 1
        WHERE sequence_year=@year

        SELECT last_no
        FROM uk_body_sequence
        WHERE sequence_year=@year

        ", con);

            cmd.Parameters.AddWithValue("@year", currentYear);

            int nextNo = Convert.ToInt32(cmd.ExecuteScalar());

            bodyId = "UKB/"
                   + currentYear.ToString()
                   + "/"
                   + nextNo.ToString("D4");
        }

        return bodyId;
    }
    // SAVE Cadaver
    protected void btnSave_Click(object sender, EventArgs e)
    {
        using (SqlConnection con =
            new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
        {
            con.Open();

            SqlTransaction trans = con.BeginTransaction();

            try
            {
                long bodySrNo = 0;
                string bodyId = "";

                //=========================================
                // CHECK DUPLICATE PM NUMBER
                //=========================================

                SqlCommand chkCmd = new SqlCommand(@"
            SELECT COUNT(*)
            FROM uk_body
            WHERE LTRIM(RTRIM(police_station_pm_no))
                  = LTRIM(RTRIM(@pm_no))
            ", con, trans);

                chkCmd.Parameters.AddWithValue("@pm_no",
                    txtPoliceStationPMNo.Text.Trim());

                int count =
                    Convert.ToInt32(chkCmd.ExecuteScalar());

                if (count > 0)
                {
                    trans.Rollback();

                    ScriptManager.RegisterStartupScript(
                               this,
                               this.GetType(),
                               "msg",
                               "alert('Police Station PM No. already exists.');$('#CadaverModal').modal('show');",
                               true);

                    return;
                }

                //=========================================
                // GENERATE BODY ID
                //=========================================

                SqlCommand seqCmd = new SqlCommand(@"

            IF NOT EXISTS
            (
                SELECT 1
                FROM uk_body_sequence
                WHERE sequence_year=@year
            )
            BEGIN
                INSERT INTO uk_body_sequence
                (
                    sequence_year,
                    last_no
                )
                VALUES
                (
                    @year,
                    0
                )
            END

            UPDATE uk_body_sequence
            SET last_no = last_no + 1
            WHERE sequence_year=@year

            SELECT last_no
            FROM uk_body_sequence
            WHERE sequence_year=@year

            ", con, trans);

                seqCmd.Parameters.AddWithValue("@year",
                    DateTime.Now.Year);

                int nextNo =
                    Convert.ToInt32(seqCmd.ExecuteScalar());

                bodyId =
                    "UKB/"
                    + DateTime.Now.Year.ToString()
                    + "/"
                    + nextNo.ToString("D4");

                //=========================================
                // INSERT BODY
                //=========================================

                SqlCommand cmd = new SqlCommand(@"

            INSERT INTO uk_body
            (
                body_id,
                body_name,
                police_station_pm_no,
                gender,
                approximate_age,
                received_date,
                panchanama_date,
                police_station_name,
                is_cms_registered,
                remarks,
                created_by,
                modified_by
            )
            OUTPUT INSERTED.srno
            VALUES
            (
                @body_id,
                @body_name,
                @pm_no,
                @gender,
                @age,
                @received_date,
                @panchanama_date,
                @police_station,
                @cms,
                @remarks,
                @created_by,
                @modified_by
            )

            ", con, trans);

                cmd.Parameters.AddWithValue("@body_id", bodyId);
                cmd.Parameters.AddWithValue("@body_name", txtBodyName.Text.Trim());

                cmd.Parameters.AddWithValue("@pm_no",
                    txtPoliceStationPMNo.Text.Trim());

                cmd.Parameters.AddWithValue("@gender",
                    ddlGender.SelectedValue);

                cmd.Parameters.AddWithValue("@age",
                    string.IsNullOrWhiteSpace(txtApproximateAge.Text)
                    ? (object)DBNull.Value
                    : Convert.ToInt32(txtApproximateAge.Text));

                cmd.Parameters.AddWithValue("@received_date",
                    Convert.ToDateTime(txtReceivedDate.Text));

                cmd.Parameters.AddWithValue("@panchanama_date",
                    string.IsNullOrWhiteSpace(txtPanchanamaDate.Text)
                    ? (object)DBNull.Value
                    : Convert.ToDateTime(txtPanchanamaDate.Text));

                cmd.Parameters.AddWithValue("@police_station",
                    txtPoliceStationName.Text.Trim());

                cmd.Parameters.AddWithValue("@cms",
                    chkCMSRegistered.Checked);

                cmd.Parameters.AddWithValue("@remarks",
                    txtRemarks.Text.Trim());

                cmd.Parameters.AddWithValue("@created_by",
                    Session["uid"] == null
                    ? "Admin"
                    : Session["uid"].ToString());

                cmd.Parameters.AddWithValue("@modified_by",
                    Session["uid"] == null
                    ? "Admin"
                    : Session["uid"].ToString());

                bodySrNo =
                    Convert.ToInt64(cmd.ExecuteScalar());

                //=========================================
                // INSERT POLICE MEMBERS
                //=========================================

                DataTable dt =
                    Session["PoliceMembers"] as DataTable;

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        SqlCommand cmdMember =
                            new SqlCommand(@"

                        INSERT INTO uk_body_police_member
                        (
                            body_id,
                            constable_no,
                            member_name,
                            mobile_no
                        )
                        VALUES
                        (
                            @body_id,
                            @constable_no,
                            @member_name,
                            @mobile_no
                        )

                        ", con, trans);

                        cmdMember.Parameters.AddWithValue("@body_id",
                            bodySrNo);

                        cmdMember.Parameters.AddWithValue("@constable_no",
                            dr["ConstableNo"].ToString());

                        cmdMember.Parameters.AddWithValue("@member_name",
                            dr["MemberName"].ToString());

                        cmdMember.Parameters.AddWithValue("@mobile_no",
                            dr["MobileNo"].ToString());

                        cmdMember.ExecuteNonQuery();
                    }
                }

                //=========================================
                // COMMIT
                //=========================================

                trans.Commit();

                Session.Remove("PoliceMembers");

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "msg",
                    "alert('Unknown Body Saved Successfully. Body ID : "
                    + bodyId +
                    "');window.location='CadaverRegistration.aspx';",
                    true);
            }
            catch (Exception ex)
            {
                trans.Rollback();

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "msg",
                    "alert('Error : "
                    + ex.Message.Replace("'", "")
                    + "');",
                    true);
            }
        }
    }
    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    using (SqlConnection con =
    //        new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
    //    {
    //        con.Open();

    //        SqlTransaction trans = con.BeginTransaction();

    //        try
    //        {
    //            long bodySrNo = 0;
    //            string bodyId = "";

    //            //=========================================
    //            // GENERATE BODY ID
    //            //=========================================

    //            SqlCommand seqCmd = new SqlCommand(@"

    //        IF NOT EXISTS
    //        (
    //            SELECT 1
    //            FROM uk_body_sequence
    //            WHERE sequence_year=@year
    //        )
    //        BEGIN
    //            INSERT INTO uk_body_sequence
    //            (
    //                sequence_year,
    //                last_no
    //            )
    //            VALUES
    //            (
    //                @year,
    //                0
    //            )
    //        END

    //        UPDATE uk_body_sequence
    //        SET last_no = last_no + 1
    //        WHERE sequence_year=@year

    //        SELECT last_no
    //        FROM uk_body_sequence
    //        WHERE sequence_year=@year

    //        ", con, trans);

    //            seqCmd.Parameters.AddWithValue("@year", DateTime.Now.Year);

    //            int nextNo =
    //                Convert.ToInt32(seqCmd.ExecuteScalar());

    //            bodyId =
    //                "UKB-"
    //                + DateTime.Now.Year.ToString()
    //                + "-"
    //                + nextNo.ToString("D4");

    //            //=========================================
    //            // INSERT BODY
    //            //=========================================

    //            SqlCommand cmd = new SqlCommand(@"

    //        INSERT INTO uk_body
    //        (
    //            body_id,
    //            body_name,
    //            police_station_pm_no,
    //            gender,
    //            approximate_age,
    //            received_date,
    //            panchanama_date,
    //            police_station_name,
    //            is_cms_registered,
    //            remarks,
    //            created_by,
    //            modified_by
    //        )
    //        OUTPUT INSERTED.srno
    //        VALUES
    //        (
    //            @body_id,
    //            @body_name,
    //            @pm_no,
    //            @gender,
    //            @age,
    //            @received_date,
    //            @panchanama_date,
    //            @police_station,
    //            @cms,
    //            @remarks,
    //            @created_by,
    //            @modified_by
    //        )

    //        ", con, trans);

    //            cmd.Parameters.AddWithValue("@body_id", bodyId);
    //            cmd.Parameters.AddWithValue("@body_name", txtBodyName.Text);

    //            cmd.Parameters.AddWithValue("@pm_no",
    //                txtPoliceStationPMNo.Text.Trim());

    //            cmd.Parameters.AddWithValue("@gender",
    //                ddlGender.SelectedValue);

    //            cmd.Parameters.AddWithValue("@age",
    //                string.IsNullOrWhiteSpace(txtApproximateAge.Text)
    //                ? (object)DBNull.Value
    //                : Convert.ToInt32(txtApproximateAge.Text));

    //            cmd.Parameters.AddWithValue("@received_date",
    //                Convert.ToDateTime(txtReceivedDate.Text));

    //            cmd.Parameters.AddWithValue("@panchanama_date",
    //                string.IsNullOrWhiteSpace(txtPanchanamaDate.Text)
    //                ? (object)DBNull.Value
    //                : Convert.ToDateTime(txtPanchanamaDate.Text));

    //            cmd.Parameters.AddWithValue("@police_station",
    //                txtPoliceStationName.Text.Trim());

    //            cmd.Parameters.AddWithValue("@cms",
    //                chkCMSRegistered.Checked);

    //            cmd.Parameters.AddWithValue("@remarks",
    //                txtRemarks.Text.Trim());
    //            cmd.Parameters.AddWithValue("@created_by", Session["uid"].ToString() ?? "Admin");
    //            cmd.Parameters.AddWithValue("@modified_by", Session["uid"].ToString() ?? "Admin");
    //            bodySrNo =
    //                Convert.ToInt64(cmd.ExecuteScalar());

    //            //=========================================
    //            // INSERT POLICE MEMBERS
    //            //=========================================

    //            DataTable dt =
    //                Session["PoliceMembers"] as DataTable;

    //            if (dt != null && dt.Rows.Count > 0)
    //            {
    //                foreach (DataRow dr in dt.Rows)
    //                {
    //                    SqlCommand cmdMember =
    //                        new SqlCommand(@"

    //                    INSERT INTO uk_body_police_member
    //                    (
    //                        body_id,
    //                        constable_no,
    //                        member_name,
    //                        mobile_no
    //                    )
    //                    VALUES
    //                    (
    //                        @body_id,
    //                        @constable_no,
    //                        @member_name,
    //                        @mobile_no
    //                    )

    //                    ", con, trans);

    //                    cmdMember.Parameters.AddWithValue("@body_id",
    //                        bodySrNo);

    //                    cmdMember.Parameters.AddWithValue("@constable_no",
    //                        dr["ConstableNo"].ToString());

    //                    cmdMember.Parameters.AddWithValue("@member_name",
    //                        dr["MemberName"].ToString());

    //                    cmdMember.Parameters.AddWithValue("@mobile_no",
    //                        dr["MobileNo"].ToString());

    //                    cmdMember.ExecuteNonQuery();
    //                }
    //            }

    //            //=========================================
    //            // COMMIT
    //            //=========================================

    //            trans.Commit();

    //            Session.Remove("PoliceMembers");

    //            ScriptManager.RegisterStartupScript(
    //                this,
    //                this.GetType(),
    //                "msg",
    //                "alert('Unknown Body Saved Successfully. Body ID : "
    //                + bodyId +
    //                "');window.location='CadaverRegistration.aspx';",
    //                true);
    //        }
    //        catch (Exception ex)
    //        {
    //            trans.Rollback();

    //            ScriptManager.RegisterStartupScript(
    //                this,
    //                this.GetType(),
    //                "msg",
    //                "alert('Error : "
    //                + ex.Message.Replace("'", "")
    //                + "');",
    //                true);
    //        }
    //    }
    //}
    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    using (SqlConnection con =
    //        new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
    //    {
    //        con.Open();

    //        SqlTransaction trans = con.BeginTransaction();

    //        try
    //        {
    //            long bodySrNo = 0;

    //            SqlCommand cmd = new SqlCommand(@"
    //        INSERT INTO uk_body
    //        (
    //            body_id,
    //            police_station_pm_no,
    //            gender,
    //            approximate_age,
    //            received_date,
    //            panchanama_date,
    //            police_station_name,
    //            is_cms_registered,
    //            remarks
    //        )
    //        OUTPUT INSERTED.srno
    //        VALUES
    //        (
    //            @body_id,
    //            @pm_no,
    //            @gender,
    //            @age,
    //            @received_date,
    //            @panchanama_date,
    //            @police_station,
    //            @cms,
    //            @remarks
    //        )", con, trans);

    //            cmd.Parameters.AddWithValue("@body_id", txtBodyId.Text);
    //            cmd.Parameters.AddWithValue("@pm_no", txtPoliceStationPMNo.Text);
    //            cmd.Parameters.AddWithValue("@gender", ddlGender.SelectedValue);
    //            cmd.Parameters.AddWithValue("@age",
    //                string.IsNullOrWhiteSpace(txtApproximateAge.Text)
    //                ? (object)DBNull.Value
    //                : Convert.ToInt32(txtApproximateAge.Text));

    //            cmd.Parameters.AddWithValue("@received_date",
    //                Convert.ToDateTime(txtReceivedDate.Text));

    //            cmd.Parameters.AddWithValue("@panchanama_date",
    //                string.IsNullOrWhiteSpace(txtPanchanamaDate.Text)
    //                ? (object)DBNull.Value
    //                : Convert.ToDateTime(txtPanchanamaDate.Text));

    //            cmd.Parameters.AddWithValue("@police_station",
    //                txtPoliceStationName.Text);

    //            cmd.Parameters.AddWithValue("@cms",
    //                chkCMSRegistered.Checked);

    //            cmd.Parameters.AddWithValue("@remarks",
    //                txtRemarks.Text);

    //            bodySrNo = Convert.ToInt64(cmd.ExecuteScalar());

    //            // Save Police Members

    //            DataTable dt =
    //                Session["PoliceMembers"] as DataTable;

    //            if (dt != null)
    //            {
    //                foreach (DataRow dr in dt.Rows)
    //                {
    //                    SqlCommand cmdMember =
    //                        new SqlCommand(@"
    //                    INSERT INTO uk_body_police_member
    //                    (
    //                        body_id,
    //                        constable_no,
    //                        member_name,
    //                        mobile_no
    //                    )
    //                    VALUES
    //                    (
    //                        @body_id,
    //                        @constable_no,
    //                        @member_name,
    //                        @mobile_no
    //                    )", con, trans);

    //                    cmdMember.Parameters.AddWithValue("@body_id", bodySrNo);
    //                    cmdMember.Parameters.AddWithValue("@constable_no", dr["ConstableNo"]);
    //                    cmdMember.Parameters.AddWithValue("@member_name", dr["MemberName"]);
    //                    cmdMember.Parameters.AddWithValue("@mobile_no", dr["MobileNo"]);

    //                    cmdMember.ExecuteNonQuery();
    //                }
    //            }

    //            trans.Commit();
    //            GenerateBodyId();
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
    //            "alert('Unknown Body Saved Successfully'); window.location='CadaverRegistration.aspx';", true);
    //         }
    //        catch (Exception ex)
    //        {
    //            trans.Rollback();

    //            ScriptManager.RegisterStartupScript(
    //                this,
    //                GetType(),
    //                "msg",
    //                "alert('" + ex.Message.Replace("'", "") + "');",
    //                true);
    //        }
    //    }
    //}
    protected void btnAddPoliceMember_Click(object sender, EventArgs e)
    {
        DataTable dt = Session["PoliceMembers"] as DataTable;

        int editIndex = Convert.ToInt32(hfEditIndex.Value);

        if (editIndex >= 0)
        {
            dt.Rows[editIndex]["ConstableNo"] = txtConstableNo.Text.Trim();
            dt.Rows[editIndex]["MemberName"] = txtMemberName.Text.Trim();
            dt.Rows[editIndex]["MobileNo"] = txtMobileNo.Text.Trim();

            hfEditIndex.Value = "-1";
            btnAddPoliceMember.Text = "Add";
        }
        else
        {
            DataRow dr = dt.NewRow();

            dr["SrNo"] = dt.Rows.Count + 1;
            dr["ConstableNo"] = txtConstableNo.Text.Trim();
            dr["MemberName"] = txtMemberName.Text.Trim();
            dr["MobileNo"] = txtMobileNo.Text.Trim();

            dt.Rows.Add(dr);
        }

        Session["PoliceMembers"] = dt;
        txtReceivedDate.Text = hfCreateReceivedDate.Value;
        txtPanchanamaDate.Text = hfCreatePanchanamaDate.Value;
        txtApproximateAge.Text = hfCreateApproximateAge.Value;
        gvPoliceMembers.DataSource = dt;
        gvPoliceMembers.DataBind();

        txtConstableNo.Text = "";
        txtMemberName.Text = "";
        txtMobileNo.Text = "";
    }
    protected void gvPoliceMembers_RowCommand(
    object sender,
    GridViewCommandEventArgs e)
    {
        DataTable dt =
            Session["PoliceMembers"] as DataTable;

        int index =
            Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "EditRow")
        {
            txtConstableNo.Text =
                dt.Rows[index]["ConstableNo"].ToString();

            txtMemberName.Text =
                dt.Rows[index]["MemberName"].ToString();

            txtMobileNo.Text =
                dt.Rows[index]["MobileNo"].ToString();

            hfEditIndex.Value = index.ToString();
            txtReceivedDate.Text = hfCreateReceivedDate.Value;
            txtPanchanamaDate.Text = hfCreatePanchanamaDate.Value;
            txtApproximateAge.Text = hfCreateApproximateAge.Value;
            btnAddPoliceMember.Text = "Update";
        }

        if (e.CommandName == "DeleteRow")
        {
            dt.Rows.RemoveAt(index);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["SrNo"] = i + 1;
            }

            Session["PoliceMembers"] = dt;
            txtReceivedDate.Text = hfCreateReceivedDate.Value;
            txtPanchanamaDate.Text = hfCreatePanchanamaDate.Value;
            txtApproximateAge.Text = hfCreateApproximateAge.Value;
            gvPoliceMembers.DataSource = dt;
            gvPoliceMembers.DataBind();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtBodyName.Text = "";
        txtPoliceStationPMNo.Text = "";

        ddlGender.SelectedIndex = 0;

        txtApproximateAge.Text = "";

        txtPanchanamaDate.Text = "";

        txtPoliceStationName.Text = "";

        chkCMSRegistered.Checked = false;

        txtRemarks.Text = "";

        txtConstableNo.Text = "";
        txtMemberName.Text = "";
        txtMobileNo.Text = "";

        hfEditIndex.Value = "-1";

        btnAddPoliceMember.Text = "Add";

        CreatePoliceTable();

        ScriptManager.RegisterStartupScript(
            this,
            this.GetType(),
            "openModal",
            "$('#CadaverModal').modal('show');",
            true);
    }
    protected void btnAddPoliceMember_Click1(object sender, EventArgs e)
    {
    }
    // GRID BIND
    private void BindGrid()
    {
        SqlDataAdapter da = new SqlDataAdapter(
            "SELECT [srno],[body_id],[police_station_pm_no],[body_name],[gender] ,[approximate_age],[is_unknown],[received_date],[panchanama_date] ,[police_station_name] ,[is_cms_registered] ,[remarks] ,[created_on] ,[created_by],[modified_on] ,[modified_by], STUFF\r\n(\r\n(\r\n    SELECT\r\n        '<br/>' +\r\n        pm.member_name +\r\n        ' (' +\r\n        ISNULL(pm.constable_no,'') +\r\n        ' - ' +\r\n        ISNULL(pm.mobile_no,'') +\r\n        ')'\r\n    FROM uk_body_police_member pm\r\n    WHERE pm.body_id = b.srno\r\n    FOR XML PATH(''), TYPE\r\n).value('.', 'nvarchar(max)')\r\n,1,5,'') AS PoliceMembers  FROM [EDUCOLLEGELIVE-R2].[dbo].[uk_body] as b ORDER BY srno DESC", con1);

        DataTable dt = new DataTable();
        da.Fill(dt);

        grdCadavers.DataSource = dt;
        grdCadavers.DataBind();
    }
    protected void grdCadavers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCadavers.PageIndex = e.NewPageIndex;

        BindGrid(); // 🔥 data reload
    }
    // EXPORT

    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        bool isPaging = grdCadavers.AllowPaging;

        grdCadavers.AllowPaging = false;

        DataTable dt;

        // Search Result Export
        if (ViewState["SearchData"] != null)
        {
            dt = (DataTable)ViewState["SearchData"];
        }
        else
        {
            using (SqlConnection con =
                new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(@"

            SELECT
                srno,
                body_id,
                body_name,
                police_station_pm_no,
                gender,
                approximate_age,
                received_date,
                panchanama_date,
                police_station_name,
                is_cms_registered,
                is_unknown,
                remarks,
                    STUFF
                    (
                    (
                        SELECT
                            '<br/>' +
                            pm.member_name +
                            ' (' +
                            ISNULL(pm.constable_no,'') +
                            ' - ' +
                            ISNULL(pm.mobile_no,'') +
                            ')'
                        FROM uk_body_police_member pm
                        WHERE pm.body_id = b.srno
                        FOR XML PATH(''), TYPE
                    ).value('.', 'nvarchar(max)')
                    ,1,5,'') AS PoliceMembers
            FROM uk_body b
            ORDER BY srno DESC

            ", con);

                dt = new DataTable();
                da.Fill(dt);
            }
        }

        grdCadavers.DataSource = dt;
        grdCadavers.DataBind();

        // Action Column Hide
        if (grdCadavers.Columns.Count > 0)
            grdCadavers.Columns[grdCadavers.Columns.Count - 1].Visible = false;

        Response.Clear();
        Response.Buffer = true;

        Response.AddHeader(
            "content-disposition",
            "attachment;filename=UnknownBodyList.xls");

        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";

        System.IO.StringWriter sw =
            new System.IO.StringWriter();

        HtmlTextWriter hw =
            new HtmlTextWriter(sw);

        grdCadavers.RenderControl(hw);

        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();

        // Restore Grid
        grdCadavers.AllowPaging = isPaging;

        if (grdCadavers.Columns.Count > 0)
            grdCadavers.Columns[grdCadavers.Columns.Count - 1].Visible = true;

        BindGrid();
    }

    public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
    {
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        using (SqlConnection con =
            new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
        {
            string search = txtCadaverNo.Text.Trim();

            string query = @"

        SELECT
            srno,
            is_unknown,
            body_id,
            body_name,
            police_station_pm_no,
            panchanama_date,
            gender,
            is_cms_registered,
            approximate_age,
            police_station_name,
            received_date,
            remarks,
            STUFF
            (
            (
                SELECT
                    '<br/>' +
                    pm.member_name +
                    ' (' +
                    ISNULL(pm.constable_no,'') +
                    ' - ' +
                    ISNULL(pm.mobile_no,'') +
                    ')'
                FROM uk_body_police_member pm
                WHERE pm.body_id = b.srno
                FOR XML PATH(''), TYPE
            ).value('.', 'nvarchar(max)')
            ,1,5,'') AS PoliceMembers
        FROM uk_body b
        WHERE
        (
            @search = ''
            OR body_id LIKE '%' + @search + '%'
            OR police_station_pm_no LIKE '%' + @search + '%'
            OR body_name LIKE '%' + @search + '%'
            OR police_station_name LIKE '%' + @search + '%'
        )
        ORDER BY srno DESC";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@search", search);

            SqlDataAdapter da =
                new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            ViewState["SearchData"] = dt;

            grdCadavers.DataSource = dt;
            grdCadavers.DataBind();
        }
    }
    protected void grdCadavers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewCadaver")
        {
            int CadaverId = Convert.ToInt32(e.CommandArgument);

            LoadCadaver(CadaverId);

            // 🔥 Modal open karne ke liye JS call
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "Popup", "$('#CadaverModal1').modal('show');", true);
        }
        if (e.CommandName == "UpdateCadaver")
        {
            int CadaverId = Convert.ToInt32(e.CommandArgument);

            LoadForUpdate(CadaverId);

            // 🔥 Modal open karne ke liye JS call
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "Popup", "$('#upCadaverModal').modal('show');", true);
        }
    }

    private void LoadCadaver(long id)
    {
        using (SqlConnection con =
            new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
        {
            con.Open();

            //=================================
            // BODY DETAILS
            //=================================

            SqlCommand cmd = new SqlCommand(@"
        SELECT uk.[srno]
      ,uk.[body_id]
      ,uk.[police_station_pm_no]
      ,uk.[body_name]
      ,uk.[gender]
      ,uk.[approximate_age]
      ,uk.[is_unknown]
      ,uk.[received_date]
      ,uk.[panchanama_date]
      ,uk.[police_station_name]
      ,uk.[is_cms_registered]
      ,uk.[remarks]
      ,uk.body_address
      ,uk.father_name
	  ,ukpm.[srno] as pm_srno
      ,ukpm.[body_id]
      ,ukpm.[received_date] as pmreceived_date
      ,ukpm.[bodypmno]
      ,ukpm.[pm_startdate]
      ,ukpm.[pm_enddate]
      ,ukpm.[remarks] as pmremarks
      ,uk.[created_on]
      ,uk.[created_by]
      ,uk.[modified_on]
      ,uk.[modified_by]
      FROM [EDUCOLLEGELIVE-R2].[dbo].[uk_body] uk left join 
       [EDUCOLLEGELIVE-R2].[dbo].[uk_body_pm] ukpm on uk.srno=ukpm.body_id
        WHERE uk.srno=@id", con);

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                lblBodyId.Text = dr["body_id"].ToString();

                lblBodyName.Text = dr["body_name"].ToString();

                lblPMNo.Text =
                    dr["police_station_pm_no"].ToString();

                lblGender.Text =
                    dr["gender"].ToString();

                lblAge.Text =
                    dr["approximate_age"].ToString();

                lblAddress.Text =
                   dr["body_address"].ToString();

                lblFatherName.Text =
                   dr["father_name"].ToString();

                //lblUnknown.Text =
                //    Convert.ToBoolean(dr["is_unknown"])
                //    ? "Yes"
                //    : "No";
                lblReceivedDate.Text =
                        dr["panchanama_date"] == DBNull.Value
                        ? ""
                        : Convert.ToDateTime(dr["received_date"])
                            .ToString("dd-MM-yyyy hh:mm tt");

                lblPanchanamaDate.Text =
                     dr["panchanama_date"] == DBNull.Value
                     ? ""
                     : Convert.ToDateTime(dr["panchanama_date"])
                         .ToString("dd-MM-yyyy hh:mm tt");

                lblPoliceStation.Text =
                    dr["police_station_name"].ToString();

                lblCMS.Text =
                    Convert.ToBoolean(dr["is_cms_registered"])
                    ? "Yes"
                    : "No";

                lblRemarks.Text =
                    dr["remarks"].ToString();

                lblPMPMStartDate.Text =
                     dr["pm_startdate"] == DBNull.Value
                     ? ""
                     : Convert.ToDateTime(dr["pm_startdate"])
                         .ToString("dd-MM-yyyy hh:mm tt");

                lblPMPMEndDate.Text =
                 dr["pm_enddate"] == DBNull.Value
                 ? ""
                 : Convert.ToDateTime(dr["pm_enddate"])
                     .ToString("dd-MM-yyyy hh:mm tt");

                lblPMPMNo.Text =
                   dr["bodypmno"].ToString();

                lblPMReceivedDate.Text =
                    dr["pmreceived_date"] == DBNull.Value
                    ? ""
                    : Convert.ToDateTime(dr["pmreceived_date"])
                        .ToString("dd-MM-yyyy hh:mm tt");

                lblPMRemarks.Text =
                   dr["pmremarks"].ToString();
            }

            dr.Close();

            //=================================
            // DOCTOR MEMBERS
            //=================================

            SqlCommand cmdPolice = new SqlCommand(@"
        SELECT
            id,
             [body_id]
      ,[employee_code]
      ,[doctor_name]
      ,[mobile_no]
        FROM uk_body_doctor_member
        WHERE body_id=@id", con);

            cmdPolice.Parameters.AddWithValue("@id", id);

            SqlDataAdapter da =
                new SqlDataAdapter(cmdPolice);

            DataTable dt = new DataTable();

            da.Fill(dt);
            Session["upDoctorMembers1"] = dt;
            gvDoctorMembersView.DataSource = dt;
            gvDoctorMembersView.DataBind();


            //=================================
            // Return POLICE MEMBERS
            //=================================

            SqlCommand cmdReturnPolice = new SqlCommand(@"
        SELECT
            id,
            constable_no,
            member_name,
            mobile_no
        FROM uk_body_police_member_return_body
        WHERE body_id=@id", con);

            cmdReturnPolice.Parameters.AddWithValue("@id", id);

            SqlDataAdapter daReturn =
                new SqlDataAdapter(cmdReturnPolice);

            DataTable dtReturn = new DataTable();

            daReturn.Fill(dtReturn);
            Session["upDoctorMembers2"] = dtReturn;
            gvReturnDoctorMembersView.DataSource = dtReturn;
            gvReturnDoctorMembersView.DataBind();

            //=================================
            // POLICE MEMBERS
            //=================================

            SqlCommand cmdPolice1 = new SqlCommand(@"
        SELECT
            id,
            constable_no,
            member_name,
            mobile_no
        FROM uk_body_police_member
        WHERE body_id=@id", con);

            cmdPolice1.Parameters.AddWithValue("@id", id);

            SqlDataAdapter daPolice =
                new SqlDataAdapter(cmdPolice1);

            DataTable dtPolice = new DataTable();

            daPolice.Fill(dtPolice);
            Session["upPoliceMembers5"] = dtPolice;
            gvPoliceDetail.DataSource = dtPolice;
            gvPoliceDetail.DataBind();
        }
    }

    private void LoadForUpdate(long id)
    {
        using (SqlConnection con = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
        {
            con.Open();

            // Body Details
            SqlCommand cmd = new SqlCommand(
                "SELECT * FROM uk_body WHERE srno=@id", con);

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dr = cmd.ExecuteReader();

            string bodyId = "";
        
            if (dr.Read())
            {
                uphfBodyId.Value = dr["srno"].ToString();

                bodyId = dr["body_id"].ToString();
                uptxtBodyId.Text = bodyId;
                uptxtBodyName.Text = dr["body_name"].ToString();
                uptxtFatherName.Text = dr["father_name"].ToString();
                uptxtAddress.Text = dr["body_address"].ToString();
                uptxtPoliceStationPMNo.Text = dr["police_station_pm_no"].ToString();

                if (upddlGender.Items.FindByValue(dr["gender"].ToString()) != null)
                    upddlGender.SelectedValue = dr["gender"].ToString();

                uptxtApproximateAge.Text = dr["approximate_age"].ToString();

                if (dr["received_date"] != DBNull.Value)
                {
                    uptxtReceivedDate.Text =
                        Convert.ToDateTime(dr["received_date"])
                        .ToString("yyyy-MM-ddTHH:mm");
                }

                if (dr["panchanama_date"] != DBNull.Value)
                {
                    uptxtPanchanamaDate.Text =
                        Convert.ToDateTime(dr["panchanama_date"])
                        .ToString("yyyy-MM-ddTHH:mm");
                }

                uptxtPoliceStationName.Text =
                    dr["police_station_name"].ToString();

                upchkCMSRegistered.Checked =
                    Convert.ToBoolean(dr["is_cms_registered"]);

                uptxtRemarks.Text =
                    dr["remarks"].ToString();
            }

            dr.Close();

            // Police Members
            DataTable dtMembers = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter(
                @"SELECT id,
                     constable_no,
                     member_name,
                     mobile_no
              FROM uk_body_police_member
              WHERE body_id=@body_id", con);

            da.SelectCommand.Parameters.AddWithValue("@body_id", id);

            da.Fill(dtMembers);
            Session["upPoliceMembers1"] = dtMembers;
            upgvPoliceMembers.DataSource = dtMembers;
            upgvPoliceMembers.DataBind();
        }
    }
    protected void upbtnAddPoliceMember_Click(object sender, EventArgs e)
    {
        DataTable dt = Session["upPoliceMembers1"] as DataTable;
        ViewState["Age"] = uptxtApproximateAge.Text;
        ViewState["ReceivedDate"] = uptxtReceivedDate.Text;
        ViewState["PanchanamaDate"] = uptxtPanchanamaDate.Text;
        int editIndex = Convert.ToInt32(uphfEditIndex.Value);

        if (editIndex >= 0)
        {
            dt.Rows[editIndex]["constable_no"] = uptxtConstableNo.Text.Trim();
            dt.Rows[editIndex]["member_name"] = uptxtMemberName.Text.Trim();
            dt.Rows[editIndex]["mobile_no"] = uptxtMobileNo.Text.Trim();

            uphfEditIndex.Value = "-1";
            upbtnAddPoliceMember.Text = "Add";
        }
        else
        {
            DataRow dr = dt.NewRow();

            dr["id"] = dt.Rows.Count + 1;
            dr["constable_no"] = uptxtConstableNo.Text.Trim();
            dr["member_name"] = uptxtMemberName.Text.Trim();
            dr["mobile_no"] = uptxtMobileNo.Text.Trim();

            dt.Rows.Add(dr);
        }

        Session["upPoliceMembers1"] = dt;

        upgvPoliceMembers.DataSource = dt;
        upgvPoliceMembers.DataBind();

        uptxtApproximateAge.Text = hfAge.Value;
        uptxtReceivedDate.Text = hfReceivedDate.Value;
        uptxtPanchanamaDate.Text = hfPanchanamaDate.Value;


        uptxtConstableNo.Text = "";
        uptxtMemberName.Text = "";
        uptxtMobileNo.Text = "";
    }

    //  protected void upgvPoliceMembers_RowCommand(
    //object sender,
    //GridViewCommandEventArgs e)
    //  {
    //      DataTable dt =
    //          Session["upPoliceMembers1"] as DataTable;

    //      int index =
    //          Convert.ToInt32(e.CommandArgument);

    //      if (e.CommandName == "EditRow")
    //      {
    //          uptxtConstableNo.Text =
    //              dt.Rows[index]["constable_no"].ToString();

    //          uptxtMemberName.Text =
    //              dt.Rows[index]["member_name"].ToString();

    //          uptxtMobileNo.Text =
    //              dt.Rows[index]["mobile_no"].ToString();

    //          uphfEditIndex.Value = index.ToString();

    //          upbtnAddPoliceMember.Text = "Update";
    //      }

    //      if (e.CommandName == "DeleteRow")
    //      {
    //          dt.Rows.RemoveAt(index);

    //          for (int i = 0; i < dt.Rows.Count; i++)
    //          {
    //              dt.Rows[i]["SrNo"] = i + 1;
    //          }

    //          Session["upPoliceMembers1"] = dt;

    //          upgvPoliceMembers.DataSource = dt;
    //          upgvPoliceMembers.DataBind();
    //      }
    //  }
    protected void upgvPoliceMembers_RowCommand(
      object sender,
      GridViewCommandEventArgs e)
    {
        DataTable dt =
            Session["upPoliceMembers1"] as DataTable;

        int index =
            Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "EditRow")
        {
            uptxtConstableNo.Text =
                dt.Rows[index]["constable_no"].ToString();

            uptxtMemberName.Text =
                dt.Rows[index]["member_name"].ToString();

            uptxtMobileNo.Text =
                dt.Rows[index]["mobile_no"].ToString();

            uphfEditIndex.Value = index.ToString();

            upbtnAddPoliceMember.Text = "Update";
            uptxtApproximateAge.Text = hfAge.Value;
            uptxtReceivedDate.Text = hfReceivedDate.Value;
            uptxtPanchanamaDate.Text = hfPanchanamaDate.Value;
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "Popup",
                "$('#upCadaverModal').modal('show');",
                true);
        }

        if (e.CommandName == "DeleteRow")
        {
            dt.Rows.RemoveAt(index);

            Session["upPoliceMembers1"] = dt;

            upgvPoliceMembers.DataSource = dt;
            upgvPoliceMembers.DataBind();
            uptxtApproximateAge.Text = hfAge.Value;
            uptxtReceivedDate.Text = hfReceivedDate.Value;
            uptxtPanchanamaDate.Text = hfPanchanamaDate.Value;
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "Popup",
                "$('#upCadaverModal').modal('show');",
                true);
        }
      
    }
    protected void upbtnSave_Click(object sender, EventArgs e)
    {
        using (SqlConnection con =
            new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
        {
            con.Open();

            SqlTransaction trans = con.BeginTransaction();

            try
            {
                long bodyId =
                    Convert.ToInt64(uphfBodyId.Value);

                //====================================
                // DUPLICATE PM NO CHECK
                //====================================

                SqlCommand chkCmd = new SqlCommand(@"
            SELECT COUNT(*)
            FROM uk_body
            WHERE LTRIM(RTRIM(police_station_pm_no))
                  = LTRIM(RTRIM(@pm_no))
            AND srno <> @id
            ", con, trans);

                chkCmd.Parameters.AddWithValue("@pm_no",
                    uptxtPoliceStationPMNo.Text.Trim());

                chkCmd.Parameters.AddWithValue("@id",
                    bodyId);

                int count =
                    Convert.ToInt32(chkCmd.ExecuteScalar());

                if (count > 0)
                {
                    trans.Rollback();

                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "msg",
                        "alert('Police Station PM No already exists.');$('#upCadaverModal').modal('show');",
                        true);

                    return;
                }

                //====================================
                // UPDATE BODY
                //====================================

                SqlCommand cmd = new SqlCommand(@"

            UPDATE uk_body
            SET
                body_name=@body_name,
                police_station_pm_no=@pm_no,
                gender=@gender,
                approximate_age=@age,
                received_date=@received_date,
                panchanama_date=@panchanama_date,
                police_station_name=@police_station,
                is_cms_registered=@cms,
                remarks=@remarks,
                father_name=@father_name,
                body_address=@body_address,
                modified_on=GETDATE(),
                modified_by=@modified_by
            WHERE srno=@id

            ", con, trans);

                cmd.Parameters.AddWithValue("@id", bodyId);

                cmd.Parameters.AddWithValue("@body_name",
                    uptxtBodyName.Text.Trim());

                cmd.Parameters.AddWithValue("@father_name",
                    uptxtFatherName.Text.Trim());

                cmd.Parameters.AddWithValue("@body_address",
                    uptxtAddress.Text.Trim());

                cmd.Parameters.AddWithValue("@pm_no",
                    uptxtPoliceStationPMNo.Text.Trim());

                cmd.Parameters.AddWithValue("@gender",
                    upddlGender.SelectedValue);

                cmd.Parameters.AddWithValue("@age",
                    string.IsNullOrWhiteSpace(uptxtApproximateAge.Text)
                    ? (object)DBNull.Value
                    : Convert.ToInt32(uptxtApproximateAge.Text));

                cmd.Parameters.AddWithValue("@received_date",
                    Convert.ToDateTime(uptxtReceivedDate.Text));

                cmd.Parameters.AddWithValue("@panchanama_date",
                    string.IsNullOrWhiteSpace(uptxtPanchanamaDate.Text)
                    ? (object)DBNull.Value
                    : Convert.ToDateTime(uptxtPanchanamaDate.Text));

                cmd.Parameters.AddWithValue("@police_station",
                    uptxtPoliceStationName.Text.Trim());

                cmd.Parameters.AddWithValue("@cms",
                    upchkCMSRegistered.Checked);

                cmd.Parameters.AddWithValue("@remarks",
                    uptxtRemarks.Text.Trim());

                cmd.Parameters.AddWithValue("@modified_by",
                    Session["uid"] == null
                    ? "Admin"
                    : Session["uid"].ToString());

                cmd.ExecuteNonQuery();

                //====================================
                // DELETE OLD MEMBERS
                //====================================

                SqlCommand delCmd = new SqlCommand(@"
            DELETE FROM uk_body_police_member
            WHERE body_id=@body_id
            ", con, trans);

                delCmd.Parameters.AddWithValue("@body_id", bodyId);

                delCmd.ExecuteNonQuery();

                //====================================
                // INSERT UPDATED MEMBERS
                //====================================

                DataTable dt =
                    Session["upPoliceMembers1"] as DataTable;

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        SqlCommand memberCmd =
                            new SqlCommand(@"

                        INSERT INTO uk_body_police_member
                        (
                            body_id,
                            constable_no,
                            member_name,
                            mobile_no
                        )
                        VALUES
                        (
                            @body_id,
                            @constable_no,
                            @member_name,
                            @mobile_no
                        )

                        ", con, trans);

                        memberCmd.Parameters.AddWithValue(
                            "@body_id", bodyId);

                        memberCmd.Parameters.AddWithValue(
                            "@constable_no",
                            dr["constable_no"].ToString());

                        memberCmd.Parameters.AddWithValue(
                            "@member_name",
                            dr["member_name"].ToString());

                        memberCmd.Parameters.AddWithValue(
                            "@mobile_no",
                            dr["mobile_no"].ToString());

                        memberCmd.ExecuteNonQuery();
                    }
                }

                trans.Commit();

                Session.Remove("upPoliceMembers1");

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "msg",
                    "alert('Cadaver Updated Successfully');window.location='CadaverRegistration.aspx';",
                    true);
            }
            catch (Exception ex)
            {
                trans.Rollback();

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "msg",
                    "alert('Error : " +
                    ex.Message.Replace("'", "") +
                    "');$('#upCadaverModal').modal('show');",
                    true);
            }
        }
    }
}