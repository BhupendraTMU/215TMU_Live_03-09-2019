using OfficeOpenXml.FormulaParsing.Excel.Functions.Database;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_PayrollDashboard : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindAcademicYear();
            BindMonth();
            BindGrid();
            

        }
    }
    private void BindAcademicYear()
    {
        ddlAcademicYear.Items.Clear();

        ddlAcademicYear.Items.Add("2026");
        ddlAcademicYear.Items.Add("2027");

        ddlAcademicYear.SelectedIndex = 0;
    }

    private void BindMonth()
    {
        ddlMonth.Items.Clear();

        ddlMonth.Items.Add(new ListItem("January", "1"));
        ddlMonth.Items.Add(new ListItem("February", "2"));
        ddlMonth.Items.Add(new ListItem("March", "3"));
        ddlMonth.Items.Add(new ListItem("April", "4"));
        ddlMonth.Items.Add(new ListItem("May", "5"));
        ddlMonth.Items.Add(new ListItem("June", "6"));
        ddlMonth.Items.Add(new ListItem("July", "7"));
        ddlMonth.Items.Add(new ListItem("August", "8"));
        ddlMonth.Items.Add(new ListItem("September", "9"));
        ddlMonth.Items.Add(new ListItem("October", "10"));
        ddlMonth.Items.Add(new ListItem("November", "11"));
        ddlMonth.Items.Add(new ListItem("December", "12"));

        ddlMonth.SelectedValue = DateTime.Now.Month.ToString(); 
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblMonthYear.Text = " " +
                            ddlMonth.SelectedItem.Text +
                            " " +
                            ddlAcademicYear.SelectedItem.Text;

        BindGrid();
    }

    private void BindGrid()
    {


        SqlCommand cmd = new SqlCommand("HRMSPortal.dbo.proc_GetHRACount", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Month", ddlMonth.SelectedValue);
        cmd.Parameters.AddWithValue("@Year", ddlAcademicYear.SelectedValue);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        da.Fill(ds);



        // Calculate Totals
        decimal totalSalary = 0;
        decimal totalHRA = 0;
        decimal totalSalary1 = 0;
        decimal totalHRA1 = 0;

        if (ds.Tables[0].Rows.Count > 0)
        {
            totalSalary = Convert.ToDecimal(ds.Tables[0].Compute("SUM(Salary)", ""));
            totalHRA = Convert.ToDecimal(ds.Tables[0].Compute("SUM([Fixed HRA Amount])", ""));
        }

        if(ds.Tables[1].Rows.Count > 0)
        {
            totalSalary1 = Convert.ToDecimal(ds.Tables[1].Compute("SUM(Salary)", ""));
            totalHRA1 = Convert.ToDecimal(ds.Tables[1].Compute("SUM([Fixed HRA Amount])", ""));
        }

        // Create Summary Table
        DataTable dtSummary = new DataTable();

        dtSummary.Columns.Add("SNo");
        dtSummary.Columns.Add("Description");
        dtSummary.Columns.Add("Employees", typeof(int));
        dtSummary.Columns.Add("SalaryAmount", typeof(decimal));
        dtSummary.Columns.Add("FixedHRA", typeof(decimal));
        dtSummary.Columns.Add("MonthlyCTC", typeof(decimal));

        // Add Summary Row
        dtSummary.Rows.Add(
            "A.",
            "Fixed HRA criteria (Perquisite provided through salary sheets)",
            ds.Tables[0].Rows.Count,
            totalSalary,
            totalHRA,
            totalSalary + totalHRA
        );




        dtSummary.Rows.Add("B.",
            "RFA criteria (As per Hospitality Department by Shri Pawan Gupta Ji Payment through cheque)",
            0.0, 0.0, 0.0, 0.0);

        dtSummary.Rows.Add("C.",
            "Fixed HRA provided by GVC sir in June 2025 at the time of Increment",
            ds.Tables[1].Rows.Count, totalSalary1, totalHRA1, totalSalary1 + totalHRA1);

        dtSummary.Rows.Add("D.",
            "Vacant Flat Details (As per Hospitality Department by Shri Pawan Gupta Ji)",
            0.0, 0.0, 0.0, 0.0);

        gvSummary.DataSource = dtSummary;
        gvSummary.DataBind();
    }


    protected void gvSummary_RowDataBound(object sender,
        System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        // Footer total logic here if required
    }
    protected void lnkEmployees_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;

        int employees = Convert.ToInt32(lnk.CommandArgument);

        Session["Month"] = ddlMonth.SelectedValue;

        Session["Year"] = ddlAcademicYear.SelectedValue;

        string url = "~/Faculty/Fixed_HRA_report.aspx";

        ScriptManager.RegisterStartupScript(
            this,
            this.GetType(),
            "OpenReport",
            "window.open('" + ResolveUrl(url) + "', '_blank');",
            true);
    }
}