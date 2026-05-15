using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Drawing.Printing;
using System.Drawing;
using System.Data;


using System.IO;


public partial class StudentReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Label1.Text=  Session["mysession"].ToString();
         if (!IsPostBack)
        {
            try
            {
                BindReport();              

            }
            catch
            {
                Response.Redirect("~/Default.aspx");
            }
           //SavePDF(ReportViewer1, "");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
      // string a = "TED1501001";

        BindReport();

    }

    public void BindReport()
    {
       string StudentNo = Session["MStudentId"].ToString();
       txtEnrollmentNo.Text = StudentNo;
        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report.rdlc");
        StudentDetailsMentor dsUsersInfo = new StudentDetailsMentor();
        StudentInteraction dsInteraction = new StudentInteraction();
        StudentAttendanceDS dsStudentAttendance = new StudentAttendanceDS();
        StudentAward dsStudentAward = new StudentAward();
        StudentPunishment dsStudentPunishment = new StudentPunishment();
        string conString = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
        using (SqlConnection con = new SqlConnection(conString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select top 1 D.Name as CollegeName, S.[Enrollment No_] as RollNo,SM.Faculty as Mentor,SM.[Student Name] as Name,STUFF(SM.[Program], PATINDEX('%[^a-z]%', SM.[Program]), 15, '') as Program,left(S.[Admitted Year],2) as Batch,SM.[Hostler_ Day Scholar] as HostelDayScholar,SM.[Fathers Name] as FatherName,SM.[Mothers Name] as MotherName,replace(SM.[Correspondence Address]+','+SM.[City]+','+SM.[State],',,',' ') as CorrAddress,convert(varchar(15),SM.[Date of Birth],103) as DOB,SM.[Phone Number Student] as PhoneStudent,SM.[Father_Mother Mobile No_] as PhoneFatherMother,SM.[E-Mail Address Student] as EmailStudent,'' as Parent,SM.[High School _] as HighSchoolPerc,[Intermediate _]  as IntermediatePerc,[Graduation _] as GraduationPerc,S.[Admitted Year] as AdmissionSession,S.[Session],S.[Student Image] as StudentImage from [TMU$Student Details Mentorship] SM left outer join [TMU$Dimension Value] D on SM.[Global Dimension 1 Code]=D.Code left outer join [TMU$Employee] E on E.No_=SM.Faculty left outer join [TMU$Student - COLLEGE] S on S.[No_]=SM.[No_]  where D.[Dimension Code]='COLLEGE' and SM.[No_]='" + StudentNo + "'", con);
            // SqlCommand cmd = new SqlCommand("select '' as  CollegeName, [No_] as No_,'a' as Name,'25 dec 2015' as DOB,[Picture] as StudentImage from [TMU$Employee] where No_='TMUS0843'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dsUsersInfo, "DataTable1");


            SqlCommand cmd1 = new SqlCommand("proc_InteractionDetailsWithMentor_ForReport", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@StudentNo", txtEnrollmentNo.Text);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            da1.Fill(dsInteraction, "DataTable1");

            //SqlCommand cmd2 = new SqlCommand("Proc_GetStudAttendanceRecordForMentor_ForReport", con);
            SqlCommand cmd2 = new SqlCommand("Proc_GetStudAttendanceRecordForMentor_ForReport_Dental", con);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add("@StudentCode", txtEnrollmentNo.Text);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            da2.Fill(dsStudentAttendance, "DataTable1");

            SqlCommand cmdAward = new SqlCommand("proc_GetAwardDataByMentor", con);
            cmdAward.CommandType = CommandType.StoredProcedure;
            cmdAward.Parameters.Add("@StudentNo", txtEnrollmentNo.Text);
            SqlDataAdapter daAward = new SqlDataAdapter(cmdAward);
            daAward.Fill(dsStudentAward, "DataTable1");
            con.Close();
            SqlCommand cmdDeciplineFine = new SqlCommand("Get_StudentDeciplineFineReportForMentor", con);
            cmdDeciplineFine.CommandType = CommandType.StoredProcedure;
            cmdDeciplineFine.Parameters.Add("@StudentNo", txtEnrollmentNo.Text);
            SqlDataAdapter daDeciplineFine = new SqlDataAdapter(cmdDeciplineFine);
            daDeciplineFine.Fill(dsStudentPunishment, "DataTable1");
            con.Close();
        }
        ReportDataSource datasource = new ReportDataSource("StudentDetailsMentor", dsUsersInfo.Tables[0]);
        ReportDataSource datasource1 = new ReportDataSource("StudentInteraction", dsInteraction.Tables[0]);
        ReportDataSource datasource2 = new ReportDataSource("StudentAttendanceDS", dsStudentAttendance.Tables[0]);
        ReportDataSource datasource3 = new ReportDataSource("StudentAwardDS", dsStudentAward.Tables[0]);
        ReportDataSource datasource4 = new ReportDataSource("StudentPunishmentDS", dsStudentPunishment.Tables[0]);
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(datasource);
        ReportViewer1.LocalReport.DataSources.Add(datasource1);
        ReportViewer1.LocalReport.DataSources.Add(datasource2);
        ReportViewer1.LocalReport.DataSources.Add(datasource3);
        ReportViewer1.LocalReport.DataSources.Add(datasource4);
        ReportViewer1.LocalReport.Refresh();
    }
    
protected void btnPrint_Click(object sender, EventArgs e)
{
    
   //PrintDocument rp = new PrintDocument(ReportViewer1.ServerReport);
   // rp.Print();  
}
public void SavePDF(ReportViewer viewer, string savePath)
{
    
    string reportName = "StudentReport";
    string deviceInfo =
        "<DeviceInfo>" +
        "  <OutputFormat>EMF</OutputFormat>" +
        "  <PageWidth>8.5in</PageWidth>" +
        "  <PageHeight>11in</PageHeight>" +
        "  <MarginTop>0.25in</MarginTop>" +
        "  <MarginLeft>0.25in</MarginLeft>" +
        "  <MarginRight>0.25in</MarginRight>" +
        "  <MarginBottom>0.25in</MarginBottom>" +
        "</DeviceInfo>";
    Warning[] warnings;
    string[] streamids;
    string mimeType;
    string encoding;
    string extension;

    byte[] bytes = viewer.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);

    Response.Clear();
    Response.ContentType = mimeType;
    Response.AddHeader("Content-Disposition", "attachment; filename=" + reportName + "." + extension);
    Response.OutputStream.Write(bytes, 0, bytes.Length);
    Response.End();
}

}