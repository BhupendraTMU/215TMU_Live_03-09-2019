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


public partial class Default3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         if (!IsPostBack)
        {
            
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
      // string a = "TED1501001";
        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report3.rdlc");
        StudentInformation dsUsersInfo = new StudentInformation();
        string conString = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
        using (SqlConnection con = new SqlConnection(conString)) 
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select top 1 D.Name as CollegeName, [Enrollment No_] as EnrollmentNo_,'0591-2476823' as CollegePhoneNo,No_,[Student Name] as Name,[Fathers Name] as FatherName,convert(varchar(15),[Date of Birth],103) as DOB,STUFF([Course Code], PATINDEX('%[^a-z]%', [Course Code]), 15, '') as Programme,[Father’s_Guardian Mobile No_] as Guardian,[Address1]+', '+[Address2]+ ', City-'+[City]+ ', '+[Post Code] as Residance,[Admitted Year] as AdmissionSession,[Session] as Session,case [Blood Group] when '0' then ''  when '1' then 'A+' when '2' then 'A-' when '3' then 'B+' when '4' then 'B-' when '5' then 'AB+' when '6' then 'AB-' when '7' then 'O+' when '8' then 'O-' end as BloodGroup,[Student Image] as StudentImage from [TMU$Student - COLLEGE]  S inner join [TMU$Dimension Value] D on S.[Global Dimension 1 Code]=D.Code where D.[Dimension Code]='COLLEGE' and [Enrollment No_]='" + txtEnrollmentNo.Text + "'", con);
            // SqlCommand cmd = new SqlCommand("select '' as  CollegeName, [No_] as No_,'a' as Name,'25 dec 2015' as DOB,[Picture] as StudentImage from [TMU$Employee] where No_='TMUS0843'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dsUsersInfo, "DataTable1");
            con.Close();
        }
        ReportDataSource datasource = new ReportDataSource("StudentInformation", dsUsersInfo.Tables[0]);
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(datasource);
        ReportViewer1.LocalReport.Refresh();
    }


    
protected void btnPrint_Click(object sender, EventArgs e)
{
    
   //PrintDocument rp = new PrintDocument(ReportViewer1.ServerReport);
   // rp.Print();  
}
}