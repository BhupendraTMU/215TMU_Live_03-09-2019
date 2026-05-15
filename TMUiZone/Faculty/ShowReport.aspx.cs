using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


public partial class Faculty_ShowReport : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
    string constr1 = ConfigurationSettings.AppSettings["strPortal"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showreport();
        }
    }
    public void showreport()
    {
        using (SqlConnection con = new SqlConnection(constr1))
        {

            string str = Session["mysession"].ToString();
            SqlCommand cmd = new SqlCommand("GetExitIterviewForm", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpCode", str);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ReportDataSource ds1 = new ReportDataSource("ExitfrmReport", dt);

            ReportViewer1.LocalReport.ReportPath = "Report/ExitfrmReport.rdlc";

            ReportViewer1.LocalReport.DataSources.Add(ds1);
            ReportViewer1.LocalReport.Refresh();
        }
    }
}