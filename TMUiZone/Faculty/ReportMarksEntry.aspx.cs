using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Faculty_ReportMarksEntry : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindReport();
        }
    }
    public void bindReport()
    {
        SqlCommand cmd = new SqlCommand("Pro_ReportNew", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Acadyear", Session["AcademicYear"].ToString());
        cmd.Parameters.AddWithValue("@Subject", Session["Subject"].ToString());
        cmd.Parameters.AddWithValue("@facultyCode", Session["FacultyCode1"].ToString());
        cmd.Parameters.AddWithValue("@Course", Session["drpCourse"].ToString());
        cmd.Parameters.AddWithValue("@Sem", Session["drpSemester"].ToString());
        cmd.Parameters.AddWithValue("@ExamMethod", Session["ExamMethod"].ToString());
        cmd.Parameters.AddWithValue("@currentUser", Session["FacultyCode1"].ToString());
        cmd.Parameters.AddWithValue("@Section", Session["Section"].ToString());
        cmd.Parameters.AddWithValue("@Group", Session["StudGroup"].ToString());  
        cmd.CommandTimeout = 240;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblmsg.Visible = false;
            ReportViewer1.Visible = true;
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/Report.rdlc");
            ReportDataSource datasource = new ReportDataSource("DataSet_Result", dt);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "No Data Found.....";
            ReportViewer1.Visible = false;
        }
    }
}