using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Faculty_ReAwardlist : System.Web.UI.Page
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
        try
        {
            SqlCommand cmd = new SqlCommand("[Pro_GetReAppearAwardListExPractical]", con);//Pro_GetAwardList_Test
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Acadyear", Session["AcademicYear"].ToString());
            cmd.Parameters.AddWithValue("@Subject", Session["Subject"].ToString());

            cmd.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());

            cmd.Parameters.AddWithValue("@Course", Session["drpCourse"].ToString());
            cmd.Parameters.AddWithValue("@Sem", Session["drpSemester"].ToString());
            cmd.CommandTimeout = 300;
            //cmd.Parameters.AddWithValue("@examType", Convert.ToInt32(Session["examType1"]));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                // lblReportStatus.Text = dt.Rows[0]["ReportStatus"].ToString();

                ReportViewer1.Visible = true;
                ReportViewer1.ProcessingMode = ProcessingMode.Local;

                ReportViewer1.Visible = true;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/ReAppearAwardList.rdlc");

                ReportDataSource datasource = new ReportDataSource("DataSet_Result", dt);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
            }
            else
            {
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'No data Found.. ');", true);
                lblmsg.Visible = true;
                lblmsg.Text = "No Data Found.....";
                ReportViewer1.Visible = false;
            }
        }
        catch (Exception ex) { }
    }
}