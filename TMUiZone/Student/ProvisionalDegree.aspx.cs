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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

public partial class Student_ProvisionalDegree : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                bindReport();
            }
        }
        catch
        {
        }
    }
    public void bindReport()
    {
        DataTable dt = new DataTable();
        if (Session["Degreename"].ToString() == "DEGREE")
        {
            SqlCommand cmd = new SqlCommand("[proc_GetDegree]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sno", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@certificate", Session["certificate"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ReportViewer1.Visible = true;
                lblmsg.Visible = false;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/Degree.rdlc");
                ReportDataSource datasource = new ReportDataSource("DataSet_Result", dt);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
            }
            else
            {
                lblmsg.ForeColor = System.Drawing.Color.Black;
                lblmsg.Visible = true;
                lblmsg.Text = "No Data Found.....";
                ReportViewer1.Visible = false;
            }
        }
        if (Session["Degreename"].ToString() == "DUPLICATE PROVISIONAL DEGREE" || Session["Degreename"].ToString() == "PROVISIONAL DEGREE")
        {
            SqlCommand cmd = new SqlCommand("[proc_GetProvisionalCertificate]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sno", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@certificate", Session["certificate"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                ReportViewer1.Visible = true;
                lblmsg.Visible = false;

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/ProvisionalDegree.rdlc");
                ReportDataSource datasource = new ReportDataSource("DataSet_Result", dt);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
            }
            else
            {
                lblmsg.ForeColor = System.Drawing.Color.Black;
                lblmsg.Visible = true;
                lblmsg.Text = "No Data Found.....";
                ReportViewer1.Visible = false;
            }
        }
        
    }

}