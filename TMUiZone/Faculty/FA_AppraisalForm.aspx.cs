using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web;
using System.IO;

public partial class Faculty_FA_AppraisalForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Departmentcode"].ToString().Trim() == "D228" || Session["uid"].ToString().Trim() == "TMU08026")
        {
            if (!IsPostBack)
            {

                BindFinancialYears_For_Filter();
                FetchDepartment();
            }
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    private void BindFinancialYears_For_Filter()
    {
        // Get the current year
        int currentYear = DateTime.Now.Year;

        // Create a list to hold financial years
        List<string> financialYears = new List<string>();

        // Populate the list with financial years for the last 10 years
        for (int i = 2023; i <= currentYear; i++)
        {
            int year = i;
            string financialYear = year.ToString().Substring(2) + "-" + (year + 1).ToString().Substring(2);
            financialYears.Add(financialYear);
        }

        // Bind the list to the dropdown
        dd_AcademicYear.DataSource = financialYears;
        dd_AcademicYear.DataBind();
        dd_AcademicYear.SelectedValue = currentYear.ToString().Substring(2) + "-" + (currentYear + 1).ToString().Substring(2);
    }
    public void SP_Performance_Appraisal_Fetch(string ddl_College_Deptt, string dd_AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("SP_PMS_Performance_Appraisal_Fetch", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Academic_Year", dd_AcademicYear);
        cmd.Parameters.AddWithValue("@College_Department", ddl_College_Deptt);
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());


        try
        {
            con.Connect();
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    GridView2.DataSource = dt;
                    GridView2.DataBind();
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    GridView2.DataSource = null;
                    GridView2.DataBind();

                    // Use a label to show message instead of Response.Write

                }
            }
            con.DisConnect();
        }
        catch (Exception ex)
        {
            // Log the error or display an error message
            
        }
        finally
        {
            con.DisConnect();
        }
    }

    public void FetchDepartment()
    {
        ddl_College_Deptt.Items.Clear();
        pms_connection con = new pms_connection();
        SqlDataReader dr = con.SP_PMS_Get_Department();
        if (dr.HasRows)
        {
            ddl_College_Deptt.DataSource = dr;
            ddl_College_Deptt.DataTextField = "Department Name";
            ddl_College_Deptt.DataValueField = "Department Name";
            ddl_College_Deptt.DataBind();
        }
        dr.Close();
        con.DisConnect();
        ddl_College_Deptt.Items.Insert(0, new ListItem("All", "All"));
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExportInExcel_Click(object sender, EventArgs e)
    {
        GridView2.Visible = true;
        string File="Performance Appraisal";
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename="+File+ ".xls");
        HttpContext.Current.Response.Charset = "";
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            // To Export all pages, we need to render the GridView with all data
            GridView2.RenderControl(hw);

            // Style to format the cells
            string style = @"<style> .textmode { } </style>";
            HttpContext.Current.Response.Write(style);
            HttpContext.Current.Response.Output.Write(sw.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        GridView2.Visible = false;
    }

   

    protected void ddl_College_Deptt_SelectedIndexChanged(object sender, EventArgs e)
    {
        SP_Performance_Appraisal_Fetch(ddl_College_Deptt.SelectedValue, dd_AcademicYear.SelectedValue);
    }

    protected void dd_AcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        SP_Performance_Appraisal_Fetch(ddl_College_Deptt.SelectedValue, dd_AcademicYear.SelectedValue);

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        SP_Performance_Appraisal_Fetch(ddl_College_Deptt.SelectedValue, dd_AcademicYear.SelectedValue);

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PMS.aspx");
    }
}
