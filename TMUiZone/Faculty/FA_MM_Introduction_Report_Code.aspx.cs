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

public partial class Faculty_FA_MM_Introduction_Report_Code : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {           

            Viewreport("","",""); // Ensure you're passing the correct arguments
        }

    }
    public void Viewreport(string id, string Name, string Age)
    {
        ReportViewer1.ProcessingMode = ProcessingMode.Local;

        ReportViewer1.LocalReport.ReportPath = Server.MapPath("FA_MM_Introduction_Report.rdlc");

        pms_connection con = new pms_connection();
        con.Connect();

        SqlDataReader dr = con.sp_fa_student_profile(id, Name, Age);

        DataTable dt = new DataTable();
        dt.Load(dr);

        ReportDataSource datasource = new ReportDataSource("ds_FA_MM_Report", dt);

        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(datasource);
        this.ReportViewer1.LocalReport.EnableExternalImages = true;

        ReportViewer1.LocalReport.Refresh();
        con.DisConnect();

        string[] filePaths = Directory.GetFiles(Server.MapPath(@"ReportView\"));
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



        FileStream fs = new FileStream(Server.MapPath(@"ReportView\" + "SOP" + ".pdf"), FileMode.Create);

        fs.Write(bytes, 0, bytes.Length);

        fs.Close();


        string FilePath = Server.MapPath(@"ReportView\" + "SOP" + ".pdf");

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