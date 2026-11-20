
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class Faculty_EmailDashboardaspx : System.Web.UI.Page
{
    string connectionString =
        ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["Filter"] = "ALL";

            LoadDashboard();
            LoadGrid("ALL");
        }
    }

    // ==========================================
    // DASHBOARD SUMMARY
    // ==========================================

    private void LoadDashboard()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(
                "sp_EmailDashboard_Summary", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        lblTotal.Text = Convert.ToString(dr["TotalRecords"]);
                        lblUnique.Text = Convert.ToString(dr["UniqueEmails"]);
                        lblDuplicate.Text = Convert.ToString(dr["DuplicateEmails"]);
                        lblDuplicateRecords.Text =
                            Convert.ToString(dr["DuplicateRecords"]);
                    }
                }
            }
        }
    }

    // ==========================================
    // LOAD GRID
    // ==========================================

    private DataTable GetEmailData(string filter)
    {
        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(
                "sp_EmailDashboard_Data", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Filter", SqlDbType.VarChar, 30).Value =
                    filter;

                cmd.Parameters.Add("@Search", SqlDbType.VarChar, 200).Value =
                    txtSearch.Text.Trim();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
        }

        return dt;
    }

    private void LoadGrid(string filter)
    {
        DataTable dt = GetEmailData(filter);

        gvEmail.DataSource = dt;
        gvEmail.DataBind();

        lblRecordCount.Text =
            "Showing " + dt.Rows.Count.ToString() + " records";
    }

    // ==========================================
    // SUMMARY CARD BUTTONS
    // ==========================================

    protected void btnTotal_Click(object sender, EventArgs e)
    {
        SetFilter("ALL", "All Employee Email Records");
    }

    protected void btnUnique_Click(object sender, EventArgs e)
    {
        SetFilter("UNIQUE", "Unique Email IDs");
    }

    protected void btnDuplicate_Click(object sender, EventArgs e)
    {
        SetFilter("DUPLICATE", "Duplicate Email IDs");
    }

    protected void btnDuplicateRecords_Click(object sender, EventArgs e)
    {
        SetFilter("DUPLICATE_RECORDS", "Duplicate Email Records");
    }

    private void SetFilter(string filter, string title)
    {
        ViewState["Filter"] = filter;

        gvEmail.PageIndex = 0;

        lblGridTitle.Text = title;

        LoadGrid(filter);
    }

    // ==========================================
    // SEARCH
    // ==========================================

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string filter = GetCurrentFilter();

        gvEmail.PageIndex = 0;

        LoadGrid(filter);
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";

        ViewState["Filter"] = "ALL";

        gvEmail.PageIndex = 0;

        lblGridTitle.Text = "All Employee Email Records";

        LoadGrid("ALL");
    }

    private string GetCurrentFilter()
    {
        if (ViewState["Filter"] != null)
        {
            return Convert.ToString(ViewState["Filter"]);
        }

        return "ALL";
    }

    // ==========================================
    // GRID PAGING
    // ==========================================

    protected void gvEmail_PageIndexChanging(
        object sender,
        GridViewPageEventArgs e)
    {
        gvEmail.PageIndex = e.NewPageIndex;

        LoadGrid(GetCurrentFilter());
    }

    // ==========================================
    // OPEN ADD EMPLOYEE MODAL
    // ==========================================

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ClearEmployeeForm();

        lblMessage.Text = "";

        ShowEmployeeModal();
    }

    private void ShowEmployeeModal()
    {
        ScriptManager.RegisterStartupScript(
            upDashboard,
            upDashboard.GetType(),
            "ShowEmployeeModal",
            "$('#employeeModal').modal('show');",
            true
        );
    }

    private void HideEmployeeModal()
    {
        ScriptManager.RegisterStartupScript(
            upDashboard,
            upDashboard.GetType(),
            "HideEmployeeModal",
            "$('#employeeModal').modal('hide');",
            true
        );
    }

    private void ClearEmployeeForm()
    {
        txtTMUCode.Text = "";
        txtEmployeeName.Text = "";
        txtMobileNo.Text = "";
        txtEmailID.Text = "";
        txtDepartment.Text = "";
        txtDesignation.Text = "";

        ddlStatus.SelectedValue = "1";
    }

    // ==========================================
    // SAVE EMPLOYEE
    // ==========================================

    protected void btnSaveEmployee_Click(object sender, EventArgs e)
    {
        string tmuCode = txtTMUCode.Text.Trim();
        string employeeName = txtEmployeeName.Text.Trim();
        string mobileNo = txtMobileNo.Text.Trim();
        string emailID = txtEmailID.Text.Trim();
        string department = txtDepartment.Text.Trim();
        string designation = txtDesignation.Text.Trim();
        int status = Convert.ToInt32(ddlStatus.SelectedValue);

        // VALIDATION

        if (tmuCode == "")
        {
            ShowMessage("Please enter TMU Code.", false);
            ShowEmployeeModal();
            return;
        }

        if (employeeName == "")
        {
            ShowMessage("Please enter Employee Name.", false);
            ShowEmployeeModal();
            return;
        }

        if (mobileNo == "")
        {
            ShowMessage("Please enter Mobile Number.", false);
            ShowEmployeeModal();
            return;
        }

        if (emailID == "")
        {
            ShowMessage("Please enter Email ID.", false);
            ShowEmployeeModal();
            return;
        }

        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(
                    "sp_EmailDashboard_Insert", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@TMUCode", SqlDbType.VarChar, 30).Value =
                        tmuCode;

                    cmd.Parameters.Add("@EmployeeName", SqlDbType.VarChar, 150).Value =
                        employeeName;

                    cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar, 15).Value =
                        mobileNo;

                    cmd.Parameters.Add("@EmailID", SqlDbType.VarChar, 200).Value =
                        emailID;

                    cmd.Parameters.Add("@Department", SqlDbType.VarChar, 150).Value =
                        department;

                    cmd.Parameters.Add("@Designation", SqlDbType.VarChar, 150).Value =
                        designation;

                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value =
                        status;

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }

            // REFRESH DASHBOARD

            LoadDashboard();

            gvEmail.PageIndex = 0;

            ViewState["Filter"] = "ALL";

            txtSearch.Text = "";

            lblGridTitle.Text = "All Employee Email Records";

            LoadGrid("ALL");

            ClearEmployeeForm();

            HideEmployeeModal();

            ShowSuccessMessage("Employee email record added successfully.");
        }
        catch (SqlException ex)
        {
            ShowMessage(
                "Database Error: " + ex.Message,
                false
            );

            ShowEmployeeModal();
        }
        catch (Exception ex)
        {
            ShowMessage(
                "Error: " + ex.Message,
                false
            );

            ShowEmployeeModal();
        }
    }

    private void ShowMessage(string message, bool success)
    {
        lblMessage.Text = message;

        lblMessage.ForeColor = success ?
            Color.Green : Color.Red;
    }

    private void ShowSuccessMessage(string message)
    {
        string safeMessage = HttpUtility.JavaScriptStringEncode(message);

        ScriptManager.RegisterStartupScript(
            upDashboard,
            upDashboard.GetType(),
            "EmployeeSuccess",
            "alert('" + safeMessage + "');",
            true
        );
    }

    // ==========================================
    // EXPORT TO EXCEL
    // ==========================================

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        string filter = GetCurrentFilter();

        DataTable dt = GetEmailData(filter);

        if (dt.Rows.Count == 0)
        {
            ShowSuccessMessage("No records available for export.");
            return;
        }

        GridView gvExport = new GridView();

        gvExport.DataSource = dt;
        gvExport.DataBind();

        gvExport.AllowPaging = false;
        gvExport.GridLines = GridLines.Both;
        gvExport.HeaderStyle.BackColor = ColorTranslator.FromHtml("#253858");
        gvExport.HeaderStyle.ForeColor = Color.White;
        gvExport.HeaderStyle.Font.Bold = true;

        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";

        Response.AddHeader(
            "Content-Disposition",
            "attachment;filename=EmployeeEmailDashboard_" +
            DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls"
        );

        Response.ContentType = "application/vnd.ms-excel";

        Response.ContentEncoding = System.Text.Encoding.UTF8;

        Response.BinaryWrite(
            System.Text.Encoding.UTF8.GetPreamble()
        );

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                gvExport.RenderControl(hw);

                Response.Output.Write(sw.ToString());
            }
        }

        Response.Flush();
        Response.End();
    }

    // REQUIRED FOR GRIDVIEW EXPORT

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Required for Excel export
    }
}
