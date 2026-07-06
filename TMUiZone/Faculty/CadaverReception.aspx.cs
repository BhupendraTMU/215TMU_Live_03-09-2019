using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Faculty_CadaverReception : System.Web.UI.Page
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

        Session["DoctorMembers"] = dt;

        gvDoctorMembers.DataSource = dt;
        gvDoctorMembers.DataBind();
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
                   + "/s"
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
                    Session["DoctorMembers"] as DataTable;

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        SqlCommand cmdMember =
                            new SqlCommand(@"

                        INSERT INTO uk_body_police_member_return_body
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

                Session.Remove("DoctorMembers");

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "msg",
                    "alert('Unknown Body Saved Successfully. Body ID : "
                    + bodyId +
                    "');window.location='CadaverReception.aspx';",
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
   protected void btnAddDoctorMember_Click(object sender, EventArgs e)
    {
        DataTable dt = Session["DoctorMembers"] as DataTable;

        if (dt == null)
        {
            CreatePoliceTable();
            dt = Session["DoctorMembers"] as DataTable;
        }

        int editIndex = Convert.ToInt32(hfEditIndex.Value);

        if (editIndex >= 0)
        {
            dt.Rows[editIndex]["ConstableNo"] = txtConstableNo.Text.Trim();
            dt.Rows[editIndex]["MemberName"] = txtMemberName.Text.Trim();
            dt.Rows[editIndex]["MobileNo"] = txtMobileNo.Text.Trim();

            hfEditIndex.Value = "-1";
            btnAddDoctorMember.Text = "Add";
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

        Session["DoctorMembers"] = dt;
        txtReceivedDate.Text = hfCreateReceivedDate.Value;
        txtPanchanamaDate.Text = hfCreatePanchanamaDate.Value;
        txtApproximateAge.Text = hfCreateApproximateAge.Value;
        uptxtPMPMEndDate.Text = hfCreatePMPMEndDate.Value;
        uptxtPMPMStartDate.Text = hfCreatePMPMStartDate.Value;
        gvDoctorMembers.DataSource = dt;
        gvDoctorMembers.DataBind();

        txtConstableNo.Text = "";
        txtMemberName.Text = "";
        txtMobileNo.Text = "";
    }
    protected void gvDoctorMembers_RowCommand(
    object sender,
    GridViewCommandEventArgs e)
    {
        DataTable dt = Session["DoctorMembers"] as DataTable;

        if (dt == null)
        {
            CreatePoliceTable();
            dt = Session["DoctorMembers"] as DataTable;
        }

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
            uptxtPMPMEndDate.Text = hfCreatePMPMEndDate.Value;
            uptxtPMPMStartDate.Text = hfCreatePMPMStartDate.Value;
            btnAddDoctorMember.Text = "Update";
        }

        if (e.CommandName == "DeleteRow")
        {
            dt.Rows.RemoveAt(index);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["SrNo"] = i + 1;
            }

            Session["DoctorMembers"] = dt;
            txtReceivedDate.Text = hfCreateReceivedDate.Value;
            txtPanchanamaDate.Text = hfCreatePanchanamaDate.Value;
            txtApproximateAge.Text = hfCreateApproximateAge.Value;
            uptxtPMPMEndDate.Text = hfCreatePMPMEndDate.Value;
            uptxtPMPMStartDate.Text = hfCreatePMPMStartDate.Value;
            gvDoctorMembers.DataSource = dt;
            gvDoctorMembers.DataBind();
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

        btnAddDoctorMember.Text = "Add";

        CreatePoliceTable();

        ScriptManager.RegisterStartupScript(
            this,
            this.GetType(),
            "openModal",
            "$('#CadaverModal').modal('show');",
            true);
    }

    // GRID BIND
    private void BindGrid()
    {
        grdCadavers.DataSource = GetCadaverData("");
        grdCadavers.DataBind();
    }
    protected void grdCadavers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCadavers.PageIndex = e.NewPageIndex;

        // Agar search chal rahi hai to search wala data bind karo
        if (!string.IsNullOrWhiteSpace(txtSearchingValue.Text.Trim()))
        {
            btnSearch_Click(null, null);
        }
        else
        {
            BindGrid();
        }
    }
    protected void grdCadavers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnReturn =
                (LinkButton)e.Row.FindControl("btnReturn");

            object returnDate =
                DataBinder.Eval(e.Row.DataItem, "return_date");

            if (returnDate != DBNull.Value && returnDate != null)
            {
                btnReturn.Enabled = false;
                btnReturn.Text = "Returned";
                btnReturn.CssClass = "btn btn-sm btn-secondary";
            }
            else
            {
                btnReturn.Enabled = true;
                btnReturn.Text = "Return";
                btnReturn.CssClass = "btn btn-sm btn-success";
            }
        }
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
                string search = txtSearchingValue.Text.Trim();

                string query = @"

        SELECT
    b.srno,
    b.body_id,
    b.body_name,
    b.gender,
    b.return_date,
    b.approximate_age,
    b.police_station_pm_no,
    b.police_station_name,

    pm.bodypmno,
    pm.received_date,
    pm.pm_startdate,
    pm.pm_enddate,
    pm.remarks AS pm_remarks,

    -- Doctor Members
    STUFF
    (
        (
            SELECT
                '<br/>' +
                ISNULL(m.doctor_name, '') +
                ' (' +
                ISNULL(m.employee_code, '') +
                ' - ' +
                ISNULL(m.mobile_no, '') +
                ')'
            FROM uk_body_doctor_member m
            WHERE m.body_id = b.srno
            FOR XML PATH(''), TYPE
        ).value('.', 'NVARCHAR(MAX)')
    ,1,5,'') AS DoctorMembers,

    -- Return Police Members
    STUFF
    (
        (
            SELECT
                '<br/>' +
                ISNULL(r.member_name, '') +
                ' (' +
                ISNULL(r.constable_no, '') +
                ' - ' +
                ISNULL(r.mobile_no, '') +
                ')'
            FROM uk_body_police_member_return_body r
            WHERE r.body_id = b.srno
            FOR XML PATH(''), TYPE
        ).value('.', 'NVARCHAR(MAX)')
    ,1,5,'') AS ReturnPoliceMembers

FROM uk_body b

OUTER APPLY
(
    SELECT TOP 1
        bodypmno,
        received_date,
        pm_startdate,
        pm_enddate,
        remarks
    FROM uk_body_pm
    WHERE body_id = b.srno
    ORDER BY srno DESC
) pm

WHERE
(
    @Search = ''
    OR b.body_id LIKE '%' + @Search + '%'
    OR b.police_station_pm_no LIKE '%' + @Search + '%'
    OR b.police_station_name LIKE '%' + @Search + '%'
    OR ISNULL(pm.bodypmno, '') LIKE '%' + @Search + '%'

    OR EXISTS
    (
        SELECT 1
        FROM uk_body_doctor_member dm
        WHERE dm.body_id = b.srno
          AND
          (
              ISNULL(dm.doctor_name, '') LIKE '%' + @Search + '%'
              OR ISNULL(dm.employee_code, '') LIKE '%' + @Search + '%'
              OR ISNULL(dm.mobile_no, '') LIKE '%' + @Search + '%'
          )
    )
)

ORDER BY b.srno DESC;";
                SqlCommand cmd =
                new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@search", search);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
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
        grdCadavers.PageIndex = 0;

        DataTable dt = GetCadaverData(txtSearchingValue.Text.Trim());

        ViewState["SearchData"] = dt;

        grdCadavers.DataSource = dt;
        grdCadavers.DataBind();
    }
    private DataTable GetCadaverData(string search = "")
    {
        DataTable dt = new DataTable();
        string query = @"

        SELECT
    b.srno,
    b.body_id,
    b.body_name,
    b.gender,
    b.return_date,
    b.approximate_age,
    b.police_station_pm_no,
    b.police_station_name,

    pm.bodypmno,
    pm.received_date,
    pm.pm_startdate,
    pm.pm_enddate,
    pm.remarks AS pm_remarks,

    -- Doctor Members
    STUFF
    (
        (
            SELECT
                '<br/>' +
                ISNULL(m.doctor_name, '') +
                ' (' +
                ISNULL(m.employee_code, '') +
                ' - ' +
                ISNULL(m.mobile_no, '') +
                ')'
            FROM uk_body_doctor_member m
            WHERE m.body_id = b.srno
            FOR XML PATH(''), TYPE
        ).value('.', 'NVARCHAR(MAX)')
    ,1,5,'') AS DoctorMembers,

    -- Return Police Members
    STUFF
    (
        (
            SELECT
                '<br/>' +
                ISNULL(r.member_name, '') +
                ' (' +
                ISNULL(r.constable_no, '') +
                ' - ' +
                ISNULL(r.mobile_no, '') +
                ')'
            FROM uk_body_police_member_return_body r
            WHERE r.body_id = b.srno
            FOR XML PATH(''), TYPE
        ).value('.', 'NVARCHAR(MAX)')
    ,1,5,'') AS ReturnPoliceMembers

FROM uk_body b

OUTER APPLY
(
    SELECT TOP 1
        bodypmno,
        received_date,
        pm_startdate,
        pm_enddate,
        remarks
    FROM uk_body_pm
    WHERE body_id = b.srno
    ORDER BY srno DESC
) pm

WHERE
(
    @Search = ''
    OR b.body_id LIKE '%' + @Search + '%'
    OR b.police_station_pm_no LIKE '%' + @Search + '%'
    OR b.police_station_name LIKE '%' + @Search + '%'
    OR ISNULL(pm.bodypmno, '') LIKE '%' + @Search + '%'

    OR EXISTS
    (
        SELECT 1
        FROM uk_body_doctor_member dm
        WHERE dm.body_id = b.srno
          AND
          (
              ISNULL(dm.doctor_name, '') LIKE '%' + @Search + '%'
              OR ISNULL(dm.employee_code, '') LIKE '%' + @Search + '%'
              OR ISNULL(dm.mobile_no, '') LIKE '%' + @Search + '%'
          )
    )
)

ORDER BY b.srno DESC;";
        using (SqlConnection con =
            new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Search", search);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        }

        return dt;
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
        if (e.CommandName == "ReturnCadaver")
        {
            int CadaverId = Convert.ToInt32(e.CommandArgument);
             Session.Remove("upPoliceMembersReturn");
            LoadForReturn(CadaverId);

            // 🔥 Modal open karne ke liye JS call
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "Popup", "$('#upCadaverModalReturn').modal('show');", true);
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
            // POLICE MEMBERS
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
        }
    }

    private void LoadForUpdate(long id)
    {
        uphfEditIndex.Value = "-1";

        hfCreatePMPMStartDate.Value = "";
        hfCreatePMPMEndDate.Value = "";
        hfAge.Value = "";
        hfReceivedDate.Value = "";
        hfPanchanamaDate.Value = "";

        Session.Remove("upDoctorMembers1");
        using (SqlConnection con = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
        {
            con.Open();

            // Body Details
            SqlCommand cmd = new SqlCommand(
                "SELECT\r\n    b.srno AS body_srno,\r\n    b.body_id,\r\n    b.police_station_pm_no,\r\n    b.body_name,\r\n    b.gender,\r\n    b.approximate_age,\r\n    b.is_unknown,\r\n    b.father_name,\r\n    b.body_address,\r\n    b.received_date AS body_received_date,\r\n    b.panchanama_date,\r\n    b.police_station_name,\r\n    b.is_cms_registered,\r\n    b.remarks AS body_remarks,\r\n    b.created_on,\r\n    b.created_by,\r\n    b.modified_on AS body_modified_on,\r\n    b.modified_by AS body_modified_by,\r\n\r\n    bpm.srno AS pm_srno,\r\n    bpm.body_id AS pm_body_id,\r\n    bpm.received_date AS pm_received_date,\r\n    bpm.bodypmno,\r\n    bpm.pm_startdate,\r\n    bpm.pm_enddate,\r\n    bpm.remarks AS pm_remarks,\r\n    bpm.modified_on AS pm_modified_on,\r\n    bpm.modified_by AS pm_modified_by\r\nFROM [EDUCOLLEGELIVE-R2].[dbo].[uk_body] b\r\nLEFT JOIN [EDUCOLLEGELIVE-R2].[dbo].[uk_body_pm] bpm\r\n    ON b.srno = bpm.body_id\r\nWHERE b.srno = @id;", con);

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dr = cmd.ExecuteReader();

            string bodyId = "";

            if (dr.Read())
            {
                uphfBodyId.Value = dr["body_srno"].ToString();

                bodyId = dr["body_id"].ToString();
                uptxtBodyId.Text = bodyId;
                uptxtBodyName.Text = dr["body_name"].ToString();
                uptxtPoliceStationPMNo.Text = dr["police_station_pm_no"].ToString();

                    upddlGender.Text = dr["gender"].ToString();

                uptxtApproximateAge.Text = dr["approximate_age"].ToString();

                if (dr["pm_received_date"] != DBNull.Value)
                {
                    uptxtPMReceivedDate.Text =
                        Convert.ToDateTime(dr["pm_received_date"])
                        .ToString("yyyy-MM-ddTHH:mm");
                }

                if (dr["panchanama_date"] != DBNull.Value)
                {
                    uptxtPanchanamaDate.Text =
                        Convert.ToDateTime(dr["panchanama_date"])
                        .ToString("yyyy-MM-ddTHH:mm");
                }
                uptxtPMPMNo.Text =
                   dr["bodypmno"].ToString();

                if (dr["pm_startdate"] != DBNull.Value)
                {
                    uptxtPMPMStartDate.Text =
                        Convert.ToDateTime(dr["pm_startdate"])
                        .ToString("yyyy-MM-ddTHH:mm");
                }

                if (dr["pm_enddate"] != DBNull.Value)
                {
                    uptxtPMPMEndDate.Text =
                        Convert.ToDateTime(dr["pm_enddate"])
                        .ToString("yyyy-MM-ddTHH:mm");
                }

                uptxtPMRemarks.Text =
                    dr["pm_remarks"].ToString();
            }

            dr.Close();

            // Doctor Members
            DataTable dtMembers = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter(
                @"SELECT id,
                    [body_id]
                  ,[employee_code]
                  ,[doctor_name]
                  ,[mobile_no]
              FROM uk_body_doctor_member
              WHERE body_id=@body_id", con);

            da.SelectCommand.Parameters.AddWithValue("@body_id", id);

            da.Fill(dtMembers);
            Session["upDoctorMembers1"] = dtMembers;
            upgvDoctorMembers.DataSource = dtMembers;
            upgvDoctorMembers.DataBind();

            
        }
    }

    private void LoadForReturn(long id)
    {
        uphfEditIndexReturn.Value = "-1";

        hfAgeReturn.Value = "";
        hfReceivedDateReturn.Value = "";
        hfPanchanamaDateReturn.Value = "";

        Session.Remove("upPoliceMembersReturn");
        using (SqlConnection con = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
        {
            con.Open();

            // Body Details
            SqlCommand cmd = new SqlCommand(
                "SELECT\r\n    b.srno AS body_srno,\r\n    b.body_id,\r\n    b.police_station_pm_no,\r\n    b.body_name,\r\n    b.gender,\r\n    b.approximate_age,\r\n    b.is_unknown,\r\n    b.father_name,\r\n    b.body_address,\r\n    b.received_date AS body_received_date,\r\n    b.panchanama_date,\r\n    b.police_station_name,\r\n    b.is_cms_registered,\r\n    b.remarks AS body_remarks,\r\n    b.created_on,\r\n    b.created_by,\r\n    b.modified_on AS body_modified_on,\r\n    b.modified_by AS body_modified_by,\r\n\r\n    bpm.srno AS pm_srno,\r\n    bpm.body_id AS pm_body_id,\r\n    bpm.received_date AS pm_received_date,\r\n    bpm.bodypmno,\r\n    bpm.pm_startdate,\r\n    bpm.pm_enddate,\r\n    bpm.remarks AS pm_remarks,\r\n    bpm.modified_on AS pm_modified_on,\r\n    bpm.modified_by AS pm_modified_by\r\nFROM [EDUCOLLEGELIVE-R2].[dbo].[uk_body] b\r\nLEFT JOIN [EDUCOLLEGELIVE-R2].[dbo].[uk_body_pm] bpm\r\n    ON b.srno = bpm.body_id\r\nWHERE b.srno = @id;", con);

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dr = cmd.ExecuteReader();

            string bodyId = "";

            if (dr.Read())
            {
                uphfBodyIdReturn.Value = dr["body_srno"].ToString();

                bodyId = dr["body_id"].ToString();
                uptxtBodyIdReturn.Text = bodyId;
                uptxtBodyNameReturn.Text = dr["body_name"].ToString();
                uptxtPoliceStationPMNoReturn.Text = dr["police_station_pm_no"].ToString();

                upddlGenderReturn.Text = dr["gender"].ToString();

                uptxtApproximateAgeReturn.Text = dr["approximate_age"].ToString();

                if (dr["pm_received_date"] != DBNull.Value)
                {
                    uptxtPMReceivedDateReturn.Text =
                        Convert.ToDateTime(dr["pm_received_date"])
                        .ToString("dd-MM-yyyy hh:mm tt");
                }

                if (dr["panchanama_date"] != DBNull.Value)
                {
                    uptxtPanchanamaDateReturn.Text =
                        Convert.ToDateTime(dr["panchanama_date"])
                        .ToString("dd-MM-yyyy hh:mm tt");
                }
                uptxtPMPMNoReturn.Text =
                   dr["bodypmno"].ToString();

                if (dr["pm_startdate"] != DBNull.Value)
                {
                    uptxtPMPMStartDateReturn.Text =
                        Convert.ToDateTime(dr["pm_startdate"])
                        .ToString("dd-MM-yyyy hh:mm tt");
                }

                if (dr["pm_enddate"] != DBNull.Value)
                {
                    uptxtPMPMEndDateReturn.Text =
                        Convert.ToDateTime(dr["pm_enddate"])
                        .ToString("dd-MM-yyyy hh:mm tt");
                }

                uptxtPMRemarksReturn.Text =
                    dr["pm_remarks"].ToString();
            }

            dr.Close();

            // Doctor Members
            DataTable dtMembers = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter(
                @"SELECT  [id]
                  ,[body_id]
                  ,[constable_no]
                  ,[member_name]
                  ,[mobile_no]
              FROM [EDUCOLLEGELIVE-R2].[dbo].[uk_body_police_member_return_body]
              WHERE body_id=@body_id", con);

            da.SelectCommand.Parameters.AddWithValue("@body_id", id);

            da.Fill(dtMembers);
            Session["upPoliceMembersReturn"] = dtMembers;
            upgvPoliceMembersReturn.DataSource = dtMembers;
            upgvPoliceMembersReturn.DataBind();


        }
    }

    protected void upbtnAddDoctorMember_Click(object sender, EventArgs e)
    {
        DataTable dt = Session["upDoctorMembers1"] as DataTable;

        if (dt == null)
        {
            dt = new DataTable();

            dt.Columns.Add("id");
            dt.Columns.Add("employee_code");
            dt.Columns.Add("doctor_name");
            dt.Columns.Add("mobile_no");

            Session["upDoctorMembers1"] = dt;
        }
        ViewState["Age"] = uptxtApproximateAge.Text;
        ViewState["ReceivedDate"] = uptxtPMReceivedDate.Text;
        ViewState["PanchanamaDate"] = uptxtPanchanamaDate.Text;
        ViewState["PMStartDate"] = uptxtPMPMStartDate.Text;
        ViewState["PMEndDate"] = uptxtPMPMEndDate.Text;
        int editIndex = Convert.ToInt32(uphfEditIndex.Value);

        if (editIndex >= 0)
        {
            dt.Rows[editIndex]["employee_code"] = uptxtPMEmployeeCode.Text.Trim();
            dt.Rows[editIndex]["doctor_name"] = uptxtPMDoctorName.Text.Trim();
            dt.Rows[editIndex]["mobile_no"] = uptxtPMMobileNo.Text.Trim();

            uphfEditIndex.Value = "-1";
            upbtnAddDoctorMember.Text = "Add";
        }
        else
        {
            DataRow dr = dt.NewRow();

            dr["id"] = dt.Rows.Count + 1;
            dr["employee_code"] = uptxtPMEmployeeCode.Text.Trim();
            dr["doctor_name"] = uptxtPMDoctorName.Text.Trim();
            dr["mobile_no"] = uptxtPMMobileNo.Text.Trim();

            dt.Rows.Add(dr);
        }

        Session["upDoctorMembers1"] = dt;

        upgvDoctorMembers.DataSource = dt;
        upgvDoctorMembers.DataBind();

        uptxtApproximateAge.Text = hfAge.Value;
        uptxtPMReceivedDate.Text = hfReceivedDate.Value;
        uptxtPanchanamaDate.Text = hfPanchanamaDate.Value;

        uptxtPMPMStartDate.Text = hfCreatePMPMStartDate.Value;
        uptxtPMPMEndDate.Text = hfCreatePMPMEndDate.Value;

        uptxtPMEmployeeCode.Text = "";
        uptxtPMDoctorName.Text = "";
        uptxtPMMobileNo.Text = "";
    }

    protected void upgvDoctorMembers_RowCommand(
      object sender,
      GridViewCommandEventArgs e)
    {
        DataTable dt =
            Session["upDoctorMembers1"] as DataTable;

        int index =
            Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "EditRow")
        {
            uptxtPMEmployeeCode.Text =
                dt.Rows[index]["employee_code"].ToString();

            uptxtPMDoctorName.Text =
                dt.Rows[index]["doctor_name"].ToString();

            uptxtPMMobileNo.Text =
                dt.Rows[index]["mobile_no"].ToString();

            uphfEditIndex.Value = index.ToString();

            upbtnAddDoctorMember.Text = "Update";
            uptxtApproximateAge.Text = hfAge.Value;
            uptxtPMReceivedDate.Text = hfReceivedDate.Value;
            uptxtPanchanamaDate.Text = hfPanchanamaDate.Value;
            uptxtPMPMEndDate.Text = hfCreatePMPMEndDate.Value;
            uptxtPMPMStartDate.Text = hfCreatePMPMStartDate.Value;
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

            Session["upDoctorMembers1"] = dt;

            upgvDoctorMembers.DataSource = dt;
            upgvDoctorMembers.DataBind();
            uptxtApproximateAge.Text = hfAge.Value;
            uptxtPMReceivedDate.Text = hfReceivedDate.Value;
            uptxtPanchanamaDate.Text = hfPanchanamaDate.Value;
            uptxtPMPMEndDate.Text = hfCreatePMPMEndDate.Value;
            uptxtPMPMStartDate.Text = hfCreatePMPMStartDate.Value;
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
                // UPDATE BODY
                //====================================

                SqlCommand cmd = new SqlCommand(@"

IF EXISTS (SELECT 1 FROM uk_body_pm WHERE body_id = @body_id)
BEGIN
    UPDATE uk_body_pm
    SET
        received_date = @received_date,
        bodypmno = @bodypmno,
        pm_startdate = @pm_startdate,
        pm_enddate = @pm_enddate,
        remarks = @remarks,
        modified_on = GETDATE(),
        modified_by = @modified_by
    WHERE body_id = @body_id
END
ELSE
BEGIN
    INSERT INTO uk_body_pm
    (
        body_id,
        received_date,
        bodypmno,
        pm_startdate,
        pm_enddate,
        remarks,
        modified_on,
        modified_by
    )
    VALUES
    (
        @body_id,
        @received_date,
        @bodypmno,
        @pm_startdate,
        @pm_enddate,
        @remarks,
        GETDATE(),
        @modified_by
    )
END

", con, trans);

                cmd.Parameters.AddWithValue("@body_id", bodyId);

                DateTime receivedDate;

                if (DateTime.TryParse(uptxtPMReceivedDate.Text, out receivedDate))
                {
                    cmd.Parameters.AddWithValue("@received_date", receivedDate);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@received_date", DBNull.Value);
                }

                cmd.Parameters.AddWithValue("@bodypmno",
                    uptxtPMPMNo.Text.Trim());

                DateTime pmStart;

                if (DateTime.TryParse(uptxtPMPMStartDate.Text, out pmStart))
                {
                    cmd.Parameters.AddWithValue("@pm_startdate", pmStart);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@pm_startdate", DBNull.Value);
                }

                DateTime pmEnd;

                if (DateTime.TryParse(uptxtPMPMEndDate.Text, out pmEnd))
                {
                    cmd.Parameters.AddWithValue("@pm_enddate", pmEnd);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@pm_enddate", DBNull.Value);
                }

                cmd.Parameters.AddWithValue("@remarks",
                    uptxtPMRemarks.Text.Trim());

                cmd.Parameters.AddWithValue("@modified_by",
                    Session["uid"] == null
                        ? "Admin"
                        : Session["uid"].ToString());

                cmd.ExecuteNonQuery();

                //====================================
                // DELETE OLD MEMBERS
                //====================================

                SqlCommand delCmd = new SqlCommand(@"
            DELETE FROM uk_body_doctor_member
            WHERE body_id=@body_id
            ", con, trans);

                delCmd.Parameters.AddWithValue("@body_id", bodyId);

                delCmd.ExecuteNonQuery();

                //====================================
                // INSERT UPDATED MEMBERS
                //====================================

                DataTable dt =
                    Session["upDoctorMembers1"] as DataTable;

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        SqlCommand memberCmd =
                            new SqlCommand(@"

                        INSERT INTO uk_body_doctor_member
                        (
                            body_id,
                            employee_code,
                            doctor_name,
                            mobile_no
                        )
                        VALUES
                        (
                            @body_id,
                            @employee_code,
                            @doctor_name,
                            @mobile_no
                        )

                        ", con, trans);

                        memberCmd.Parameters.AddWithValue(
                            "@body_id", bodyId);

                        memberCmd.Parameters.AddWithValue(
                            "@employee_code",
                            dr["employee_code"].ToString());

                        memberCmd.Parameters.AddWithValue(
                            "@doctor_name",
                            dr["doctor_name"].ToString());

                        memberCmd.Parameters.AddWithValue(
                            "@mobile_no",
                            dr["mobile_no"].ToString());

                        memberCmd.ExecuteNonQuery();
                    }
                }

                trans.Commit();

                Session.Remove("upDoctorMembers1");
                uphfEditIndex.Value = "-1";

                uptxtPMReceivedDate.Text = "";
                uptxtPMPMNo.Text = "";
                uptxtPMPMStartDate.Text = "";
                uptxtPMPMEndDate.Text = "";
                uptxtPMRemarks.Text = "";

                hfCreatePMPMStartDate.Value = "";
                hfCreatePMPMEndDate.Value = "";
                hfAge.Value = "";
                hfReceivedDate.Value = "";
                hfPanchanamaDate.Value = "";

                upgvDoctorMembers.DataSource = null;
                upgvDoctorMembers.DataBind();
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "msg",
                    "alert('Cadaver Updated Successfully');window.location='CadaverReception.aspx';",
                    true);
            }
            catch (Exception ex)
            {
                trans.Rollback();
                ScriptManager.RegisterStartupScript(
                              this,
                              this.GetType(),
                              "msg",
                              "alert('Something went wrong.');$('#upCadaverModal').modal('show');",
                              true);
              
            }
        }
    }
    protected void upbtnAddPoliceMemberReturn_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        DataTable dt = Session["upPoliceMembersReturn"] as DataTable;
        if (dt == null)
        {
            dt = new DataTable();

            dt.Columns.Add("id");
            dt.Columns.Add("constable_no");
            dt.Columns.Add("member_name");
            dt.Columns.Add("mobile_no");

            Session["upPoliceMembersReturn"] = dt;
        }
        ViewState["Age"] = uptxtApproximateAgeReturn.Text;
        ViewState["ReceivedDate"] = "";// uptxtReceivedDateReturn.Text;
        ViewState["PanchanamaDate"] = uptxtPanchanamaDateReturn.Text;
        int editIndex = Convert.ToInt32(uphfEditIndexReturn.Value);

        if (editIndex >= 0)
        {
            dt.Rows[editIndex]["constable_no"] = uptxtConstableNoReturn.Text.Trim();
            dt.Rows[editIndex]["member_name"] = uptxtMemberNameReturn.Text.Trim();
            dt.Rows[editIndex]["mobile_no"] = uptxtMobileNoReturn.Text.Trim();

            uphfEditIndexReturn.Value = "-1";
            upbtnAddPoliceMemberReturn.Text = "Add";
        }
        else
        {
            DataRow dr = dt.NewRow();

            dr["id"] = dt.Rows.Count + 1;
            dr["constable_no"] = uptxtConstableNoReturn.Text.Trim();
            dr["member_name"] = uptxtMemberNameReturn.Text.Trim();
            dr["mobile_no"] = uptxtMobileNoReturn.Text.Trim();

            dt.Rows.Add(dr);
        }

        Session["upPoliceMembersReturn"] = dt;

        upgvPoliceMembersReturn.DataSource = dt;
        upgvPoliceMembersReturn.DataBind();

        uptxtApproximateAgeReturn.Text = hfAgeReturn.Value;
        uptxtPanchanamaDateReturn.Text = hfPanchanamaDateReturn.Value;


        uptxtConstableNoReturn.Text = "";
        uptxtMemberNameReturn.Text = "";
        uptxtMobileNoReturn.Text = "";
    }
    protected void upgvPoliceMembersReturn_RowCommand(
     object sender,
     GridViewCommandEventArgs e)
    {
        DataTable dt =
            Session["upPoliceMembersReturn"] as DataTable;

        int index =
            Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "EditRow")
        {
            uptxtConstableNoReturn.Text =
                dt.Rows[index]["constable_no"].ToString();

            uptxtMemberNameReturn.Text =
                dt.Rows[index]["member_name"].ToString();

            uptxtMobileNoReturn.Text =
                dt.Rows[index]["mobile_no"].ToString();

            uphfEditIndexReturn.Value = index.ToString();

            upbtnAddPoliceMemberReturn.Text = "Update";
            uptxtApproximateAgeReturn.Text = hfAgeReturn.Value;
            uptxtPanchanamaDateReturn.Text = hfPanchanamaDateReturn.Value;
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

            Session["upPoliceMembersReturn"] = dt;

            upgvPoliceMembersReturn.DataSource = dt;
            upgvPoliceMembersReturn.DataBind();
            uptxtApproximateAgeReturn.Text = hfAgeReturn.Value;
            uptxtPanchanamaDateReturn.Text = hfPanchanamaDateReturn.Value;
            //ScriptManager.RegisterStartupScript(
            //    this,
            //    this.GetType(),
            //    "Popup",
            //    "$('#upCadaverModal').modal('show');",
            //    true);
        }

    }
    protected void upbtnReturn_Click(object sender, EventArgs e)
    {
        using (SqlConnection con =
            new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
        {
            con.Open();

            SqlTransaction trans = con.BeginTransaction();

            try
            {
                long bodyId;
                if (!long.TryParse(uphfBodyIdReturn.Value, out bodyId))
                {
                    throw new Exception("Invalid Body Id : " + uphfBodyIdReturn.Value);
                }
                //====================================
                // UPDATE BODY
                //====================================

                SqlCommand cmd = new SqlCommand(@"
                  UPDATE uk_body
                SET
                    return_date = @return_date
                WHERE srno = @srno", con, trans);

                cmd.Parameters.AddWithValue("@srno", bodyId);

                DateTime returnDate;

                if (DateTime.TryParse(uptxtPMReturnDateReturn.Text, out returnDate))
                {
                    cmd.Parameters.AddWithValue("@return_date", returnDate);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@return_date", DBNull.Value);
                }

                cmd.ExecuteNonQuery();

                //====================================
                // DELETE OLD MEMBERS
                //====================================

                SqlCommand delCmd = new SqlCommand(@"
            DELETE FROM uk_body_police_member_return_body
            WHERE body_id=@body_id
            ", con, trans);

                delCmd.Parameters.AddWithValue("@body_id", bodyId);

                delCmd.ExecuteNonQuery();

                //====================================
                // INSERT UPDATED MEMBERS
                //====================================

                DataTable dt =
                    Session["upPoliceMembersReturn"] as DataTable;

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        SqlCommand memberCmd =
                            new SqlCommand(@"

                        INSERT INTO uk_body_police_member_return_body
                        (
                               body_id
                            , [constable_no]
                            ,[member_name]
                            , mobile_no
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

                Session.Remove("upPoliceMembersReturn");
                uphfEditIndexReturn.Value = "-1";

                uptxtPMReceivedDateReturn.Text = "";
                uptxtPMPMNoReturn.Text = "";
                uptxtPMPMStartDateReturn.Text = "";
                uptxtPMPMEndDateReturn.Text = "";
                uptxtPMRemarksReturn.Text = "";

                hfAgeReturn.Value = "";
                hfReceivedDateReturn.Value = "";
                hfPanchanamaDateReturn.Value = "";

                upgvPoliceMembersReturn.DataSource = null;
                upgvPoliceMembersReturn.DataBind();
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "msg",
                    "alert('Cadaver Return Successfully');window.location='CadaverReception.aspx';",
                    true);
            }
            catch (Exception ex)
            {
                trans.Rollback();
                ScriptManager.RegisterStartupScript(
                              this,
                              this.GetType(),
                              "msg",
                              "alert('Something went wrong.');$('#upCadaverModalReturn').modal('show');",
                              true);

            }
        }
    }
}