using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Drawing;
using System.Web.SessionState;

/// <summary>
/// Summary description for Connection
/// </summary>
public class TMUConnection
{
    //SqlConnection Conn = null;
    SqlCommand cmd;
    SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString);

	
    public TMUConnection()
    {
        //string str = val.ToString();
        //Conn = new SqlConnection(ConfigurationSettings.AppSettings["str"].ToString());

    }

    public SqlConnection Con
    {
        get
        {
            return Conn;
        }
    }

    public void Connect()
    {

        if (Conn.State == ConnectionState.Closed)
            Conn.Open();
    }
    public void DisConnect()
    {
        Conn.Close();
    }


    public SqlDataReader LoginAccess(string  companyName,string userid,string password )
    {
        Connect();
        SqlDataReader dr=null ;
        try
        {
            SqlCommand cmd = new SqlCommand("Proc_GetLoginByIdPwd", Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyName", companyName);
            cmd.Parameters.AddWithValue("@Id", userid);
            cmd.Parameters.AddWithValue("@Password", password);
            dr = cmd.ExecuteReader();
            return dr;
            // Dim da As New SqlDataAdapter(cmd)
            //Dim ds As New DataSet()
            //da.Fill(ds)
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
        catch
        {
            DisConnect();
            
        }
        return dr;
            
        //string s = "select * from " + companyName + " where [Course Code]='093' and [No_]='" + userid + "' and Password='" + password + "'";
        //cmd = new SqlCommand(s, Conn);
        //SqlDataReader dr = cmd.ExecuteReader();
        //return dr;
    }


    public SqlDataReader LoginAccess_Faculty(string companyName, string userid, string password)
    {
        Connect();


        string s = "select * from " + companyName + " where  [No_]='" + userid + "' and Password='" + password + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader ShowProfile(string companyName, string userid)
    {
        Connect();


        string s = "select * from " + companyName + " where [Course Code]='093' and [No_]='" + userid + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader ShowFaculty(string CompanyName, String userid)
    {
        Connect();
        string str = "select * from " + CompanyName + " where No_='" + userid + "'";
        cmd = new SqlCommand(str, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader Show_Courses(string tableStudentCollege, string userid,string semester)
    {
        Connect();


        string s = "select [Subject Code],Semester,  Description,[Subject Type] ,[Internal Mark],[External Mark] ,Total,Attendancedetails from " + tableStudentCollege + " where [Student No_]='" + userid + "' and Semester='" + semester + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader Show_ExamSchedule(string tblexamscheduleline, string semester)
    {
        Connect();


        string s = "select [Subject Code],[Subject Type],[Academic Year],[Exam Date],[Start Time],[End Time],[Hall Code] from " + tblexamscheduleline + "  where [Course Code]='093' and [Semester Code]='" + semester + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader Show_Semester(string tableStudentCollege, string userid)
    {
        Connect();


        string s = "select distinct (Semester) as Semester from " + tableStudentCollege + " where [Student No_]='" + userid + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader Show_SemesterforLine(string tableStudentCollege)
    {
        Connect();


        string s = "select distinct ([Semester Code]) as Semester from " + tableStudentCollege + " ";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader Show_SemesterwithStudentExternalLineCOL(string tableStudentCollege, string userid)
    {
        Connect();


        string s = "select distinct (Semester) as Semester from " + tableStudentCollege + " where [Student No_]='" + userid + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader ShowExamResult(string tblStudentExternalLine,string tblSubjectCOLLEGE, string userid,string semester)
    {
        Connect();


        string s = " select exct.Semester,exct.[Subject Code],subjectdd.Description ,exct.[Total Maximum] ,exct.Grade as [GO], exct.Points as [GP],exct.[Cr Points] as [cp] from " + tblStudentExternalLine + " as exct join " + tblSubjectCOLLEGE + " as subjectdd on  exct.[Subject Code]=subjectdd.Code where exct.[Course]='093' and exct.[Student No_]='" + userid + "' and exct.Semester='"+semester+"'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader ShowBackpapers(string Studentid)
    {
        Connect();


        string s = "select * from tbl_Studentexamresult where Studentid='" + Studentid + "' and Backpaper!=0 ";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public void Insert_tbl_Studentexamresult(string Studentid,string  Semester,string SGPA,string CGPA,string Backpaper)
    {
        Connect();
        string sqlq = "insert into tbl_Studentexamresult (Studentid,Semester,SGPA,CGPA,Backpaper) values('"+Studentid+"','"+Semester+"','"+SGPA+"','"+CGPA+"','"+Backpaper+"')";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }

    public SqlDataReader ShowSGPA(string Studentid)
    {
        Connect();


        string s = " select * from tbl_Studentexamresult where Studentid='" + Studentid + "' ";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader CountAbsent(string tbleName)
    {
        Connect();


        string s = "select count([Attendance Type]) as [Attendance Type] from [Ashoka University$Student Subject - COLLEGE] where [Attendance Type]=2";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader CountBackpaper(string Studentid, string tblename, string Semester)
    {
        Connect();


        string s = " select count(Result) as Result from " + tblename + " where [Student No_]='" + Studentid + "' and Result='2' and Semester='" + Semester + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader showABsentAttendance( string tblename)
    {
        Connect();


        string s = "select [Student No_],[Student Name],GetDate() as[Attendance Date],[Attendance Type] from " + tblename + " where [Attendance Type]=2";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public void Delete_tbl_Studentexamresult(string Studentid)
    {
        Connect();
        string sqlq = "delete from tbl_Studentexamresult where Studentid='" + Studentid + "'";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }

    public void Update_FacultyProfile( string tablename,string userid,string FirstName, string Address, string Address2, string Country, string PostCode, string email, string MobileNumber, string Gender,string City)
    {
        Connect();
        string str = "Update " + tablename + "Set [First Name]='" + FirstName + "', Address='" + Address + "',[Address 2]= '" + Address2 + "',County='" + Country + "',[Post Code]='" + PostCode + "', [E-Mail]='" + email + "',  [Mobile Phone No_]='" + MobileNumber + "', Gender='" + Gender + "', City='" + City + "' where  [No_]='" + userid + "'";
        cmd = new SqlCommand(str, Conn);
        cmd.ExecuteNonQuery();


    }

    public void Update_Profile(string tblecompanyName, string userid, string FathersName, string MothersName, string Address1, string Address2, string CountryCode, string PostCode, string email, string MobileNumber, string Gender,string City)
    {
        Connect();
        string sqlq = "update " + tblecompanyName + " set [Fathers Name]='" + FathersName + "',[Mothers Name]='" + MothersName + "',Address1='" + Address1 + "' , Address2='" + Address2 + "',[Country Code]='" + CountryCode + "',City='"+City+"',[Post Code]='" + PostCode + "' ,[E-Mail Address]='" + email + "',[Mobile Number]='" + MobileNumber + "',Gender='" + Gender + "' where [No_]='" + userid + "'";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }

}