using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PMS_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Viewreport(Session["PMS_PDF_VIEW_ID"].ToString().Trim());
        }
    }
    public void Viewreport(string id)
    {
        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        //   ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/nonskd_Bulk_report.rdlc");
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Faculty/pms_report.rdlc");

        pms_connection con = new pms_connection();
        con.Connect();
        SqlDataReader dr = con.sp_Get_PMS_DataWithID(id);
        DataTable dt = new DataTable();
        dt.Load(dr);
        //ReportDataSource datasource = new ReportDataSource("ds_Bulk_Non_Skd", dt);
        ReportDataSource datasource = new ReportDataSource("rpt_DS_PMS", dt);

        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(datasource);
        this.ReportViewer1.LocalReport.EnableExternalImages = true;

        ReportViewer1.LocalReport.Refresh();
        con.DisConnect();

        string[] filePaths = Directory.GetFiles(Server.MapPath(@"PMSReportView\"));
        foreach (string filePath in filePaths)
            File.Delete(filePath);

        Warning[] warnings;

        string[] streamids;

        string mimeType;

        string encoding;

        string extension;



        byte[] bytes = ReportViewer1.LocalReport.Render(

          "PDF", null, out mimeType, out encoding, out extension,

          out streamids, out warnings);



        FileStream fs = new FileStream(Server.MapPath(@"PMSReportView\" + "PMS" + ".pdf"), FileMode.Create);

        fs.Write(bytes, 0, bytes.Length);

        fs.Close();


        string FilePath = Server.MapPath(@"PMSReportView\" + "PMS" + ".pdf");

        WebClient User = new WebClient();

        Byte[] FileBuffer = User.DownloadData(FilePath);

        if (FileBuffer != null)
        {

            Response.ContentType = "application/pdf";

            Response.AddHeader("content-length", FileBuffer.Length.ToString());

            Response.BinaryWrite(FileBuffer);

        }
    }

}