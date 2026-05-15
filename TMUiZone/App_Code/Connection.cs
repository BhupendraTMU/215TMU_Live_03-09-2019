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
public class Connection
{
    SqlConnection Conn = null;
    SqlCommand cmd;


    public Connection()
    {
             

        //string str = val.ToString();
        Conn = new SqlConnection(ConfigurationSettings.AppSettings["str"].ToString());
       
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


    public SqlDataReader CompanyName()
    {
        Connect();

        string s = "select * from Company where [Web Portal Access]='1'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }



    public SqlDataReader LoginDetail(string companyName, string userid, string pass)
    {
        Connect();
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("sp_LoginValidation", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@UserId", userid);
        cmd.Parameters.Add("@Pwd", pass );        
        SqlDataReader dr = cmd.ExecuteReader();
               
        //string s = "select * from " + companyName + " where [No_]='" + userid + "' and [Web Portal Password]='" + pass + "' and [Web portal Access]=1";
        //cmd = new SqlCommand(s, Conn);
        //SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    //public SqlDataReader LoginDetail(string companyName, string userid)
    //{
    //    Connect();


    //    string s = "select * from " + companyName + " where [Company E-Mail]='" + userid + "' and [Web portal Access]=1";
    //    cmd = new SqlCommand(s, Conn);
    //    SqlDataReader dr = cmd.ExecuteReader();
    //    return dr;
    //}

    public SqlDataReader UserCOdetails(string companyName, string userid,string fdate,string tdate)
    {
        Connect();

        //upper([Weekly Days])='SUNDAY'
        //string s = "select * from " + companyName + " where [Off Day]='1' and [Attendance Marked]='1' and [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) >='" + fdate + "'  and CONVERT(date, [Attendance Date],103) <='" + tdate + "' and [CO]='1' and [CO Status]='Pending' or Holiday='1' and [Attendance Marked]='1' and [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) >='" + fdate + "'  and CONVERT(date, [Attendance Date],103) <='" + tdate + "' and [CO]='1' and [CO Status]='Pending'";
        string s = "select * from " + companyName + " where upper([Weekly Days])='SUNDAY' and [Attendance Marked]='1' and [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) >='" + fdate + "'  and CONVERT(date, [Attendance Date],103) <='" + tdate + "' and [CO]='1' and [CO Status]='Pending' or Holiday='1' and [Attendance Marked]='1' and [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) >='" + fdate + "'  and CONVERT(date, [Attendance Date],103) <='" + tdate + "' and [CO]='1' and [CO Status]='Pending'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }



    public SqlDataReader SHow_HODIDForApproval(string userid, string companyName)
    {
        Connect();
        string s = "select * from " + companyName + " where  [No_]='" + userid + "'";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader Profile_Detail(string companyName, string userid)
    {
        Connect();

        string s = "select * from " + companyName + " where [No_]='" + userid + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader Show_State(string companyName)
    {
        Connect();

        string s = "select * from " + companyName + " ";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader Show_StatewithStateCode(string companyName,string Code )
    {
        Connect();

        string s = "select * from " + companyName + " where Code ='"+Code+"'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader Show_Pincode(string companyName)
    {
        Connect();

        string s = "select * from " + companyName + " ";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader Show_PincodewithCity(string companyName,string Code)
    {
        Connect();

        string s = "select * from " + companyName + " where  Code='" + Code + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader Show_PincodewithPincode(string companyName, string City)
    {
        Connect();

        string s = "select * from " + companyName + " where  City='" + City + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader ShowLeaveBalance_Detail_CL(string companyName, string userid)
    {
        Connect();

        string s = "select * from " + companyName + " where [Employee Code]='" + userid + "' and [Leave code]='CL'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader ShowLeaveBalance_Detail(string companyName, string userid)
    {
        Connect();
        //and [Leave Balance]>0"; -------condition added by ashu 13-07-2016
        string s = "select * from " + companyName + " where [Employee Code]='" + userid + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader ShowLeaveBalance_Detail_LBType(string companyName, string userid,string lbtype)
    {
        Connect();
        string s = "select * from " + companyName + " where [Employee Code]='" + userid + "' and [Leave code]='" + lbtype + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader ShowLeaveBalance_DetailwithOption(string companyName, string userid,string LeaveCode)
    {
        Connect();              
      //  string s = "select * from " + companyName + " where [Employee Code]='" + userid + "' and [Leave code]='" + LeaveCode + "'";//Comment by ashu
        string s = "select [Leave Balance],[Unapproved Leave] from " + companyName + " where [Employee Code]='" + userid + "' and [Leave code]='" + LeaveCode + "'";  // by ashu 14-07-2016 ---- [Leave Balance]-[Unapproved Leave] 
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    
    public SqlDataReader ShowLeaveBalanceType(string tbleName)
    {
        Connect();

        string s = "select '-------' as [Leave Code] union select 'LWP' as [Leave Code] union SELECT DISTINCT [Leave Code] FROM  " + tbleName + " order by [Leave Code] ";

        //string s = "select '-------' as [Leave Code] union select 'LWP' as [Leave Code] union  select 'Special Leave' as [Leave Code] union SELECT DISTINCT [Leave Code] FROM  " + tbleName + " order by [Leave Code] ";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    // Overload method created by Ashu 13-07-2016----------
    public SqlDataReader ShowLeaveBalanceType(string tbleName, string userid)
    {
        Connect();
        string s = "select '-------' as [Leave Code] union select 'LWP' as [Leave Code] union select [Leave code] as [Leave Code] from " + tbleName + " where [Employee Code]='" + userid + "' and [Leave Balance]>0 order by [Leave Code] ";        
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public SqlDataReader ShowLeaveBalance_Detail_PL(string companyName, string userid)
    {
        Connect();
        string s = "select * from " + companyName + " where [Employee Code]='" + userid + "' and [Leave code]='PL'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader Show_Department(string tbl_Name)
    {
        Connect();

        string s = "select * from " + tbl_Name + "";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public SqlDataReader Show_Department1(string tbl_Name)//ashu 25-05-2016
    {
        Connect();

        string s = "select Code as [Department Code],Name as [Department Description] from " + tbl_Name + " where [Dimension Code]='DEPARTMENT' order by Name";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
   


    public SqlDataReader Show_Location(string tbl_Name)
    {
        Connect();

        string s = "select * from " + tbl_Name + "";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader Show_Branch(string tbl_Name)
    {
        Connect();

       // string s = "select * from " + tbl_Name + " where [Dimension Code] ='BRANCH' and Blocked='0'";
        string s = "select * from " + tbl_Name + " ";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader Show_HRID(string tbl_Name ,string EmPID)
    {
        Connect();

        string s = "select * from " + tbl_Name + " where [Web Portal Type]='2' and [No_]='" + EmPID + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader Show_HODEmail(string tbl_Name, string hodid)
    {
        Connect();

        string s = "select * from " + tbl_Name + " where [No_]='" + hodid + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader Show_Designation(string tbl_Name)
    {
        Connect();

        string s = "select * from " + tbl_Name + "";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader Show_HolidayList(string tbl_Name,string datet)
    {
        Connect();

        string s = "select * from " + tbl_Name + " where convert(date, [Attendance Date],103) ='" + datet + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }



  
    public SqlDataReader Show_LeaveforAttendence(string tbl_Name, string datet,string userid)
    {
        Connect();

        string s = "select * from " + tbl_Name + " where convert(date, [Attendance Date],103) ='" + datet + "' and [Employee Code]='" + userid + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public void Update_Navision_Resolved_Profile(string Title, string FirstName, string MiddleName, string LastName, string Address, string Address2, string City, string County, string PostCode, string EMail, string MaritalStatus, string PhoneNo, string MobilePhoneNo, string BirthDate, string FatherName, string HusbandName, string PermanentAddress1, string PermanentAddress2, string PermanentCity, string PermanentState, string PANNo, string AccountNo, string CompName, string userid, string Picture, string ProfilePhoto,string State)
    {
        Connect();
        string sqlq = "update " + CompName + " set Title='" + Title + "' ,[First Name] ='" + FirstName + "',[Middle Name]='" + MiddleName + "',[Last Name]='" + LastName + "',Address='" + Address + "',[Address 2]='" + Address2 + "', City='" + City + "',County='" + County + "',[Post Code]='" + PostCode + "',[E-Mail]='" + EMail + "',[Marital Status]='" + MaritalStatus + "',[Phone No_]='" + PhoneNo + "',[Mobile Phone No_]='" + MobilePhoneNo + "',[Birth Date]='" + BirthDate + "',[Father Name]='" + FatherName + "',[Husband Name]='" + HusbandName + "',[Permanent Address1]='" + PermanentAddress1 + "',[Permanent Address2]='" + PermanentAddress2 + "', [Permanent City]='" + PermanentCity + "',[Permanent State]='" + PermanentState + "',[PAN No]='" + PANNo + "',[Account No]='" + AccountNo + "',Picture='" + Picture + "',ProfilePhoto='" + ProfilePhoto + "', State='" + State + "' where [No_]='" + userid + "'";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }
    public void Update_AttendencewithApproval(string tbleName, string Timein, string TimeOut, string Status, string HoursPresent, string userid,  string attendenceDate,string LeaveCompensation,string CoStatus ,string CoRemarks)
    {
        Connect();
        string sqlq = "update " + tbleName + " set [Time in]='" + Timein + "',[Time Out]='" + TimeOut + "',Status='" + Status + "' , [Hours Present]='" + HoursPresent + "',[Attendance Marked]='1', [CO]='" + LeaveCompensation + "' , [CO Status]='" + CoStatus + "',[CO Remarks]='"+CoRemarks+"' where [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103)='" + attendenceDate + "'";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }





    public void Update_AttendencewithApprovalforLeave(string tbleName, string Timein, string TimeOut, string Status, string HoursPresent, string userid, string fdate,string tdate, string leavePeriod, string leavecode,string HalfDayLeaveType)
    {
        Connect();
        string sqlq = "update " + tbleName + " set [Time in]='" + Timein + "',[Time Out]='" + TimeOut + "',Status='" + Status + "' , [Hours Present]='" + HoursPresent + "',[Attendance Marked]='0',[Applied Leave]='1',[Leave Type]='" + leavePeriod + "',[Leave Code]='" + leavecode + "',[Half Day Leave Type]='" + HalfDayLeaveType + "' where [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) >='" + fdate + "'  and CONVERT(date, [Attendance Date],103) <='" + tdate + "'";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }

    public void CancelLeave_AttendencewithApprovalforLeave(string tbleName, string Timein, string TimeOut, string Status, string HoursPresent, string userid, string fdate, string tdate, string leavePeriod, string leavecode)
    {
        Connect();
        string sqlq = "update " + tbleName + " set [Time in]='" + Timein + "',[Time Out]='" + TimeOut + "',Status='" + Status + "' , [Hours Present]='" + HoursPresent + "',[Attendance Marked]='0',[Applied Leave]='0',[Leave Type]='" + leavePeriod + "',[Leave Code]='" + leavecode + "' where [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) >='" + fdate + "'  and CONVERT(date, [Attendance Date],103) <='" + tdate + "'";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }


    public void updateCoLeaveStatusinPaydailyAttendance(string tablename,string AttendanceDate,string EmployeeCode,string Costatus)
    {
        Connect();
        string sqlq = "update " + tablename + " set [CO Status]='" + Costatus + "' where convert(date,[Attendance Date],103)='" + AttendanceDate + "' and [Employee Code]='" + EmployeeCode + "' ";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }

    //public void insert_Table_Credit_Leave(string tablename, string EmployeeCode, DateTime dates,string leaveCode,decimal noofleave,int active,string LeaveID)
    //{
    //    Connect();
    //    string sqlq = "insert into " + tablename + " ([Employee Code],Date,[Leave Code],[No_ of Leaves],Active,[Leave ID]) values('" + EmployeeCode + "','" + dates + "','" + leaveCode + "','" + noofleave + "'," + active + ",'" + LeaveID + "')";
    //    cmd = new SqlCommand(sqlq, Conn);
    //    cmd.ExecuteNonQuery();

    //}



    public void Update_AttendencewithApprovalforLeavenotinholidayoffday(string tbleName, string Timein, string TimeOut, string Status, string HoursPresent, string userid, string fdate, string tdate, string leavePeriod, string leavecode ,string HalfDayLeaveType)
    {
        Connect();
        //-------------------------------Ashu--------------------------------04-04-2017----for LWP---
        if (leavecode == "LWP")
        {
            leavecode = ""; leavePeriod = "";
        }
        //-------------------------------Ashu--------------------------------04-04-2017----for LWP---
          //string sqlq = "update " + tbleName + " set [Time in]='" + Timein + "',[Time Out]='" + TimeOut + "',Status='" + Status + "' , [Hours Present]='" + HoursPresent + "',[Attendance Marked]='0',[Applied Leave]='1',[Leave Type]='" + leavePeriod + "',[Leave Code]='" + leavecode + "' where [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) >='" + fdate + "'  and CONVERT(date, [Attendance Date],103) <='" + tdate + "' and Holiday='0' and [Off Day]='0'";
            string sqlq = "update " + tbleName + " set [Time in]='" + Timein + "',[Time Out]='" + TimeOut + "',Status='" + Status + "' , [Hours Present]='" + HoursPresent + "',[Attendance Marked]='0',[Applied Leave]='1',[Leave Type]='" + leavePeriod + "',[Leave Code]='" + leavecode + "',[Half Day Leave Type]='" + HalfDayLeaveType + "' where [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) >='" + fdate + "'  and CONVERT(date, [Attendance Date],103) <='" + tdate + "'";
        
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }


    public void Update_attendanceforfixedshift(string tbleName, string Timein, string TimeOut, string Status, string HoursPresent, string userid, string fdate, string tdate, string leavePeriod, string leavecode, string HalfDayLeaveType,string fixedweekday,string holidayfixed)
    {
        Connect();
        //string sqlq = "update " + tbleName + " set [Time in]='" + Timein + "',[Time Out]='" + TimeOut + "',Status='" + Status + "' , [Hours Present]='" + HoursPresent + "',[Attendance Marked]='0',[Applied Leave]='1',[Leave Type]='" + leavePeriod + "',[Leave Code]='" + leavecode + "' where [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) >='" + fdate + "'  and CONVERT(date, [Attendance Date],103) <='" + tdate + "' and Holiday='0' and [Off Day]='0'";
        string sqlq = "update " + tbleName + " set [Time in]='" + Timein + "',[Time Out]='" + TimeOut + "',Status='" + Status + "' , [Hours Present]='" + HoursPresent + "',[Attendance Marked]='0',[Applied Leave]='1',[Leave Type]='" + leavePeriod + "',[Leave Code]='" + leavecode + "',[Half Day Leave Type]='" + HalfDayLeaveType + "' where [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) >='" + fdate + "'  and CONVERT(date, [Attendance Date],103) <='" + tdate + "'  and (DATENAME(dw,[Attendance Date]) !='" + fixedweekday + "' or  DATENAME(dw,[Attendance Date]) !='" + holidayfixed + "') ";
        //
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }

  

    public void Update_attendanceOFFday(string tbleName, string userid, string fdate, string tdate, string fixedweekday)
    {
        Connect();
        //string sqlq = "update " + tbleName + " set [Time in]='" + Timein + "',[Time Out]='" + TimeOut + "',Status='" + Status + "' , [Hours Present]='" + HoursPresent + "',[Attendance Marked]='0',[Applied Leave]='1',[Leave Type]='" + leavePeriod + "',[Leave Code]='" + leavecode + "' where [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) >='" + fdate + "'  and CONVERT(date, [Attendance Date],103) <='" + tdate + "' and Holiday='0' and [Off Day]='0'";
        string sqlq = "update " + tbleName + " set [Off Day]='1',Status='1',[Applied Leave]='0',[Leave Type]='0',[Leave Code]='' where [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) >='" + fdate + "'  and CONVERT(date, [Attendance Date],103) <='" + tdate + "'  and DATENAME(dw,[Attendance Date]) ='" + fixedweekday + "' ";
        //
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }


    public void Update_attendanceOFFday_monthly(string tbleName, string userid, string fdate)
    {
        Connect();
        //string sqlq = "update " + tbleName + " set [Time in]='" + Timein + "',[Time Out]='" + TimeOut + "',Status='" + Status + "' , [Hours Present]='" + HoursPresent + "',[Attendance Marked]='0',[Applied Leave]='1',[Leave Type]='" + leavePeriod + "',[Leave Code]='" + leavecode + "' where [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) >='" + fdate + "'  and CONVERT(date, [Attendance Date],103) <='" + tdate + "' and Holiday='0' and [Off Day]='0'";
        string sqlq = "update " + tbleName + " set [Off Day]='1',Status='1',[Applied Leave]='0',[Leave Type]='0',[Leave Code]='' where [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) ='" + fdate + "'  ";
        //
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }


    public void Update_attendanceHoliday(string tbleName, string userid, string fdate)
    {
        Connect();
        //string sqlq = "update " + tbleName + " set [Time in]='" + Timein + "',[Time Out]='" + TimeOut + "',Status='" + Status + "' , [Hours Present]='" + HoursPresent + "',[Attendance Marked]='0',[Applied Leave]='1',[Leave Type]='" + leavePeriod + "',[Leave Code]='" + leavecode + "' where [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) >='" + fdate + "'  and CONVERT(date, [Attendance Date],103) <='" + tdate + "' and Holiday='0' and [Off Day]='0'";
        string sqlq = "update " + tbleName + " set [Holiday]='1',Status='1',[Applied Leave]='0' ,[Leave Type]='0',[Leave Code]='' where [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) ='" + fdate + "' ";
        //
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }




    public void Update_attendanceforfixedHoliday(string tbleName, string Timein, string TimeOut, string Status, string HoursPresent, string userid, string fdate, string tdate, string leavePeriod, string leavecode, string HalfDayLeaveType, string fixedweekday, string holidayfixed)
    {
        Connect();
        //string sqlq = "update " + tbleName + " set [Time in]='" + Timein + "',[Time Out]='" + TimeOut + "',Status='" + Status + "' , [Hours Present]='" + HoursPresent + "',[Attendance Marked]='0',[Applied Leave]='1',[Leave Type]='" + leavePeriod + "',[Leave Code]='" + leavecode + "' where [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) >='" + fdate + "'  and CONVERT(date, [Attendance Date],103) <='" + tdate + "' and Holiday='0' and [Off Day]='0'";
        string sqlq = "update " + tbleName + " set [Time in]='" + Timein + "',[Time Out]='" + TimeOut + "',Status='" + Status + "' , [Hours Present]='" + HoursPresent + "',[Attendance Marked]='0',[Applied Leave]='1',[Leave Type]='" + leavePeriod + "',[Leave Code]='" + leavecode + "',[Half Day Leave Type]='" + HalfDayLeaveType + "' where [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) >='" + fdate + "'  and CONVERT(date, [Attendance Date],103) <='" + tdate + "'  and (DATENAME(dw,[Attendance Date]) !='" + fixedweekday + "' or  CONVERT(date, [Attendance Date],103) !='" + holidayfixed + "') ";
        //
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }

    public void Update_AttendencewithApprovalforLeavenotinoffday(string tbleName, string Timein, string TimeOut, string Status, string HoursPresent, string userid, string fdate, string tdate, string leavePeriod, string leavecode,string Half_Day_type_Code)
    {
        Connect();
        string sqlq = "update " + tbleName + " set [Time in]='" + Timein + "',[Time Out]='" + TimeOut + "',Status='" + Status + "' , [Hours Present]='" + HoursPresent + "',[Attendance Marked]='0',[Applied Leave]='1',[Leave Type]='" + leavePeriod + "',[Leave Code]='" + leavecode + "',[Half Day Leave Type]='"+Half_Day_type_Code+"' where [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) >='" + fdate + "'  and CONVERT(date, [Attendance Date],103) <='" + tdate + "' and Holiday='0'";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }


    public void Update_AttendencewithApprovalforLeavenotinholiday(string tbleName, string Timein, string TimeOut, string Status, string HoursPresent, string userid, string fdate, string tdate, string leavePeriod, string leavecode,string HalfDayLeaveType)
    {
        Connect();
        //string sqlq = "update " + tbleName + " set [Time in]='" + Timein + "',[Time Out]='" + TimeOut + "',Status='" + Status + "' , [Hours Present]='" + HoursPresent + "',[Attendance Marked]='0',[Applied Leave]='1',[Leave Type]='" + leavePeriod + "',[Leave Code]='" + leavecode + "' where [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) >='" + fdate + "'  and CONVERT(date, [Attendance Date],103) <='" + tdate + "' and [Off Day]='0'";
        string sqlq = "update " + tbleName + " set [Time in]='" + Timein + "',[Time Out]='" + TimeOut + "',Status='" + Status + "' , [Hours Present]='" + HoursPresent + "',[Attendance Marked]='0',[Applied Leave]='1',[Leave Type]='" + leavePeriod + "',[Leave Code]='" + leavecode + "',[Half Day Leave Type]='"+HalfDayLeaveType+"' where [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) >='" + fdate + "'  and CONVERT(date, [Attendance Date],103) <='" + tdate + "' and upper([Weekly Days])!='SUNDAY'";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }
    public SqlDataReader Count_Holiday(string tbleName, string fromdate,string toDdate,string userid)
    {
        Connect();

        string s = "select COUNT(Holiday) as Holiday from " + tbleName + " where Holiday='1' and convert(date, [Attendance Date],103) >='" + fromdate + "' and  convert(date, [Attendance Date],103)  <='" + toDdate + "' and [Employee Code]='" + userid + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader Count_Off_Day(string tbleName, string fromdate, string toDdate, string userid)
    {
        Connect();

      //  string s = "select COUNT([Off Day]) as OffDay from " + tbleName + " where [Off Day]='1' and convert(date, [Attendance Date],103) >='" + fromdate + "' and  convert(date, [Attendance Date],103)  <='" + toDdate + "' and [Employee Code]='" + userid + "'";
        string s = "select COUNT([Weekly Days]) as OffDay from " + tbleName + " where 'SUNDAY' and convert(date, [Attendance Date],103) >='" + fromdate + "' and  convert(date, [Attendance Date],103)  <='" + toDdate + "' and [Employee Code]='" + userid + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader Show_AttendenceDateFromNavision(string tbl_Name, string datet , string userid)
    {
        Connect();

        string s = "select * from " + tbl_Name + " where convert(date, [Attendance Date],103) ='" + datet + "' and [Employee Code]='" + userid + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }



    public SqlDataReader Show_GLAccount(string tbl_Name)
    {
        Connect();

        string s = "select * from " + tbl_Name + " ";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader Show_AttendenceDateFromNavisionforLeave(string tbl_Name, string fdate,string tdate, string userid)
    {
        Connect();

        string s = "select * from " + tbl_Name + " where convert(date, [Attendance Date],103)  >='" + fdate + "' and convert(date, [Attendance Date],103)  <='" + tdate + "' and [Employee Code]='" + userid + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public void Update_Pay_Employee_Leave_EntitledLeave_Balance(double Leave_Balance, string tbleName, string employeeid, string LeaveCode)
    {
        Connect();
        string sqlq = "update " + tbleName + "set [Leave Balance]=" + Leave_Balance + " where [Employee Code]='" + employeeid + "' and [Leave code] ='" + LeaveCode + "' ";

        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }


    

  

    public SqlDataReader ShowOayEmployeleaveunapproved(string tbleName, string employeeid, string LeaveCode)
    {
        Connect();

        string s = "select * from " + tbleName + " where [Employee Code]='" + employeeid + "' and [Leave code] ='" + LeaveCode + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public void Update_Pay_Employee_Leave_EntitledLeave_BalancecancelafterApproval(double Leave_Balance, string tbleName, string employeeid, string LeaveCode)
    {
        Connect();
       // string sqlq = "update " + tbleName + " set [Leave Balance]=" + Leave_Balance + " where [Employee Code]='" + employeeid + "' and [Leave code] ='" + LeaveCode + "' ";//comment by ashu 14-07-2016
        string sqlq = "update " + tbleName + " set [Leave Balance]=" + Leave_Balance + " where [Employee Code]='" + employeeid + "' and [Leave code] ='" + LeaveCode + "' ";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }



    public void Update_Pay_Employee_Leave_EntitledLeave_BalanceUnaprovedafterApproval(double Leave_Balance, string tbleName, string employeeid, string LeaveCode,double Unapproved_Leave)
    {
        Connect();
        //string sqlq = "update " + tbleName + " set [Leave Balance]=" + Leave_Balance + " ,[Unapproved Leave]=" + Unapproved_Leave + " where [Employee Code]='" + employeeid + "' and [Leave code] ='" + LeaveCode + "' ";
        string sqlq = "update " + tbleName + " set [Leave Balance]=" + Leave_Balance + " ,[Unapproved Leave]=" + Unapproved_Leave + " where [Employee Code]='" + employeeid + "' and [Leave code] ='" + LeaveCode + "' ";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }

    public void Update_Pay_Employee_Leave_EntitledLeave_BalanceApply(double Leave_Balance, string tbleName, string employeeid, string LeaveCode)
    {
        Connect();
        //string sqlq = "update " + tbleName + " set [Leave Balance]=" + Leave_Balance + " where [Employee Code]='" + employeeid + "' and [Leave code] ='" + LeaveCode + "' ";
        string sqlq = "update " + tbleName + " set [Unapproved Leave]=" + Leave_Balance + " where [Employee Code]='" + employeeid + "' and [Leave code] ='" + LeaveCode + "' ";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }



    public void Update_Employee_Password_Change_Status(string changestatus,string NewPass, string comp_name,string userID)
    {
        Connect();
        string sqlq = "update " + comp_name + " set Change_PasswordStatus=" + changestatus + ",[Web Portal Password]='" + NewPass + "'  where [No_]='" + userID + "'";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }

    public SqlDataReader ShowEmployeeid(string tbleName)
    {
        Connect();

        string s = "select * from " + tbleName + "";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


 


    public SqlDataReader Show_DetailOFemployee(string tbl_Name, string userid)
    {
        Connect();

        string s = "select * from " + tbl_Name + " where [No_]='" + userid + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public SqlDataReader SHow_showHRExhist(string userid, string companyName)
    {
        Connect();
        string s = "select * from " + companyName + " where  [HR]='" + userid + "'";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public SqlDataReader SHow_showHODExhist(string userid, string companyName)
    {
        Connect();
        //string s = "select * from " + companyName + " where  [HOD]='" + userid + "'  or [HOD 1]='" + userid + "' or [Company E-Mail]='vicechancellor@tmu.ac.in'";//comment by Ashu 0n 03-04-2017 for Leave Approval Show hide
        string s = "select * from " + companyName + " where  [HOD]='" + userid + "'  or [HOD 1]='" + userid + "' or (No_='" + userid + "' and [Company E-Mail]='vicechancellor@tmu.ac.in')";//Add by Ashu 0n 03-04-2017 for Leave Approval Show hide

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader SHow_showHODExhistavv(string userid, string companyName)
    {
        Connect();
        string s = "select * from " + companyName + " where  [HOD]='" + userid + "'  or [HOD 1]='" + userid + "'";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader SHow_Employee_Basic_Pay(string userid, string companyName, DateTime paydate)
    {
        Connect();
        string s = "select * from " + companyName + " where [Pay Element Code] ='BASIC' and [Employee No]='" + userid + "' and [Pay Structure Date]='"+paydate+"'";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader SHow_Employee_Basic_MaxDate(string userid, string companyName)
    {
        Connect();
        string s = "select Max([Pay Structure Date]) as PayDate from " + companyName + " where [Pay Element Code] ='BASIC' and [Employee No]='" + userid + "'";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader ShowPayEmployeePayDetails(string tbleName, int month, int year, string userid, string ShowPayMaxDate)
    {
        Connect();

        string s = "select * from " + tbleName + " where Month =" + month + " and Year=" + year + " and [Employee No]='" + userid + "' and [Pay Element Code]!='BONUS' and [Pay Element Code]!='EXGRATIA' and [Paid Category]='1' and Type!='2' and ForMonthDate='" + ShowPayMaxDate + "' order by [Pay Element Code] asc  ";

        //string s = "select * from " + tbleName + " where Month =" + month + " and Year=" + year + " and [Employee No]='" + userid + "' and [Pay Element Code]!='BONUS' and [Pay Element Code]!='EXGRATIA' and [Paid Category]='1' and Type!='2' and ForMonthDate='" + ShowPayMaxDate + "' order by [Pay Element Code] asc select  ForMonthDate from  " + tbleName + " where Month =" + month + " and Year=" + year + " and [Employee No]='" + userid + "' and [Pay Element Code]!='BONUS' and [Pay Element Code]!='EXGRATIA' and [Paid Category]='1' and Type!='2' and [Arrear Amount]!=0  order by ForMonthDate asc ";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    
    public SqlDataReader ShowPayEmployeePayDetailsArear(string tbleName, int month, int year, string userid)
    {
        Connect();

        //string s = "select * from " + tbleName + " where Month =" + month + " and Year=" + year + " and [Employee No]='" + userid + "' and [Pay Element Code]!='BONUS' and [Pay Element Code]!='EXGRATIA' and [Paid Category]='1' and Type!='2' and ForMonthDate='" + ShowPayMaxDate + "' order by [Pay Element Code] asc  ";

        string s = "select top 1 ForMonthDate from  " + tbleName + " where Month =" + month + " and Year=" + year + " and [Employee No]='" + userid + "' and [Pay Element Code]!='BONUS' and [Pay Element Code]!='EXGRATIA' and [Paid Category]='1' and Type!='2' and [Arrear Amount]!=0  order by ForMonthDate asc ";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader ShowPayMaxDate(string tbleName, int month, int year, string userid)
    {
        Connect();

        //string s = "select * from " + tbleName + " where Month =" + month + " and Year=" + year + " and [Employee No]='" + userid + "' and [Pay Element Code]!='BONUS' and [Pay Element Code]!='EXGRATIA' and [Paid Category]='1' and Type!='2' order by [Pay Element Code] asc";

        string s = "select top 1 ForMonthDate as ForMonthDate from  " + tbleName + " where Month =" + month + " and Year=" + year + " and [Employee No]='" + userid + "' and [Pay Element Code]!='BONUS' and [Pay Element Code]!='EXGRATIA' and [Paid Category]='1' and Type!='2'  order by ForMonthDate desc";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader ShowPayNowofDaysworking(string tbleName, int month, int year, string userid, string ShowPayMaxDate)
    {
        Connect();

        //string s = "select * from " + tbleName + " where Month =" + month + " and Year=" + year + " and [Employee No]='" + userid + "' and [Pay Element Code]!='BONUS' and [Pay Element Code]!='EXGRATIA' and [Paid Category]='1' and Type!='2' order by [Pay Element Code] asc";

        string s = "select * from  " + tbleName + " where Month =" + month + " and Year=" + year + " and [Employee No]='" + userid + "' and [Pay Element Code]='BASIC' and ForMonthDate='" + ShowPayMaxDate + "' ";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader SHow_CompanyInformation(string tbleName)
    {
        Connect();
        string s = "select * from " + tbleName + "";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    //public void Insert_ApprovalData_Gen_Journal_Line(string tble_Name, string  JournalTemplateName,string JournalBatchName,int  LineNo_,string ItemNo_,DateTime PostingDate,int EntryType,string SourceNo_,string DocumentNo_,string Description,string LocationCode,string InventoryPostingGroup,string SourcePostingGroup,decimal Quantity, decimal InvoicedQuantity,decimal UnitAmount,decimal UnitCost,decimal Amount, decimal DiscountAmount,string Salespers__Purch_Code,string SourceCode ,int Applies_toEntry,int ItemShpt_EntryNo_,string  ShortcutDimension1Code,string ShortcutDimension2Code,decimal IndirectCost_,int SourceType,string ReasonCode,int RecurringMethod,DateTime ExpirationDate,string RecurringFrequency,int DropShipment,string TransactionType,string TransportMethod,string Country_RegionCode,string NewLocationCode,string  NewShortcutDimension1Code,string NewShortcutDimension2Code,decimal Qty_Calculated,decimal Qty_Phys_Inventory,int LastItemLedgerEntryNo_,int Phys_Inventory,string Gen_Bus_PostingGroup,string Gen_Prod_PostingGroup,string Entry_ExitPoint,DateTime DocumentDate ,string ExternalDocumentNo_,string Area,string TransactionSpecification,string PostingNo_Series,decimal UnitCost_ACY,string SourceCurrencyCode,int DocumentType,int DocumentLineNo_,int OrderType,string OrderNo_,int OrderLineNo_,int DimensionSetID ,int NewDimensionSetID,int AssembletoOrder,string JobNo_,string JobTaskNo_,int JobPurchase,int JobContractEntryNo_,string VariantCode,string BinCode,decimal Qty_perUnitofMeasure,string NewBinCode,string UnitofMeasureCode,int DerivedfromBlanketOrder,decimal QuantityBase,decimal InvoicedQty_Base,int Level,int FlushingMethod,int ChangedbyUser,string Cross_ReferenceNo_,string OriginallyOrderedNo_,string OriginallyOrderedVar_Code,string Out-of-Stock Substitution],[Item Category Code],[Nonstock],[Purchasing Code],[Product Group Code],[Planned Delivery Date],[Order Date],[Value Entry Type],[Item Charge No_],[Inventory Value (Calculated)],[Inventory Value (Revalued)],[Variance Type],[Inventory Value Per],[Partial Revaluation],[Applies-from Entry],[Invoice No_],[Unit Cost (Calculated)],[Unit Cost (Revalued)],[Applied Amount],[Update Standard Cost],[Amount (ACY)],[Correction],[Adjustment],[Applies-to Value Entry],[Invoice-to Source No_],[Type],[No_],[Operation No_],[Work Center No_],[Setup Time],[Run Time],[Stop Time],[Output Quantity],[Scrap Quantity],[Concurrent Capacity],[Setup Time (Base)],[Run Time (Base)],[Stop Time (Base)],[Output Quantity (Base)],[Scrap Quantity (Base)],[Cap_ Unit of Measure Code],[Qty_ per Cap_ Unit of Measure],[Starting Time],[Ending Time],[Routing No_],[Routing Reference No_],[Prod_ Order Comp_ Line No_],[Finished],[Unit Cost Calculation],[Subcontracting],[Stop Code],[Scrap Code],[Work Center Group Code],[Work Shift Code],[Serial No_],[Lot No_],[Warranty Date],[New Serial No_],[New Lot No_],[New Item Expiration Date],[Item Expiration Date],[Return Reason Code],[Warehouse Adjustment],[Phys Invt Counting Period Code],[Phys Invt Counting Period Type],[BED _],[BED Amount],[Other Duties _],[Other Duties Amount],[Reverse Excise],[RG23PartI Entry No_],[RG23PartII Entry No_],[Return],[RG Correction Type],[RG Line No_],[Excise Record],[PO_SO Line No_],[Applies-to Entry (RG 23 D)],[Excise Entry],[Laboratory Test],[Other Usage],[Nature of Disposal],[Type of Disposal],[Captive Consumption],[From Transfer Order],[Re-Dispatch],[Assessable Value,[Rcpt_ Posting],[Transfer Cost],[Subcon Order No_],[Requisition No_],[Requisition Line No_],[NRGP_RRGP_RGP_MRO],[Ref_ Entry No_],[Overhead Rate] ,[Single-Level Material Cost],[Single-Level Capacity Cost],[Single-Level Subcontrd_ Cost],[Single-Level Cap_ Ovhd Cost],[Single-Level Mfg_ Ovhd Cost],[Rolled-up Material Cost],[Rolled-up Capacity Cost],[Rolled-up Subcontracted Cost],[Rolled-up Mfg_ Ovhd Cost],[Rolled-up Cap_ Overhead Cost],[GRN No_],[GRN Date])
    //{
    //    Connect();
    //    string sqlq = "insert into " + tble_Name + " ([Journal Template Name],[Journal Batch Name] ,[Line No_],[Item No_],[Posting Date],[Entry Type],[Source No_],[Document No_],[Description],[Location Code],[Inventory Posting Group],[Source Posting Group],[Quantity],[Invoiced Quantity],[Unit Amount],[Unit Cost],[Amount],[Discount Amount],[Salespers__Purch_ Code],[Source Code],[Applies-to Entry],[Item Shpt_ Entry No_],[Shortcut Dimension 1 Code],[Shortcut Dimension 2 Code],[Indirect Cost _],[Source Type],[Reason Code],[Recurring Method],[Expiration Date],[Recurring Frequency],[Drop Shipment],[Transaction Type],[Transport Method],[Country_Region Code],[New Location Code],[New Shortcut Dimension 1 Code],[New Shortcut Dimension 2 Code],[Qty_ (Calculated)],[Qty_ (Phys_ Inventory)],[Last Item Ledger Entry No_],[Phys_ Inventory],[Gen_ Bus_ Posting Group],[Gen_ Prod_ Posting Group],[Entry_Exit Point],[Document Date],[External Document No_],[Area],[Transaction Specification],[Posting No_ Series],[Unit Cost (ACY)],[Source Currency Code],[Document Type],[Document Line No_],[Order Type],[Order No_],[Order Line No_],[Dimension Set ID],[New Dimension Set ID],[Assemble to Order],[Job No_],[Job Task No_],[Job Purchase],[Job Contract Entry No_],[Variant Code],[Bin Code],[Qty_ per Unit of Measure],[New Bin Code],[Unit of Measure Code],[Derived from Blanket Order],[Quantity (Base)],[Invoiced Qty_ (Base)],[Level],[Flushing Method],[Changed by User],[Cross-Reference No_],[Originally Ordered No_],[Originally Ordered Var_ Code],[Out-of-Stock Substitution],[Item Category Code],[Nonstock],[Purchasing Code],[Product Group Code],[Planned Delivery Date],[Order Date],[Value Entry Type],[Item Charge No_],[Inventory Value (Calculated)],[Inventory Value (Revalued)],[Variance Type],[Inventory Value Per],[Partial Revaluation],[Applies-from Entry],[Invoice No_],[Unit Cost (Calculated)],[Unit Cost (Revalued)],[Applied Amount],[Update Standard Cost],[Amount (ACY)],[Correction],[Adjustment],[Applies-to Value Entry],[Invoice-to Source No_],[Type],[No_],[Operation No_],[Work Center No_],[Setup Time],[Run Time],[Stop Time],[Output Quantity],[Scrap Quantity],[Concurrent Capacity],[Setup Time (Base)],[Run Time (Base)],[Stop Time (Base)],[Output Quantity (Base)],[Scrap Quantity (Base)],[Cap_ Unit of Measure Code],[Qty_ per Cap_ Unit of Measure],[Starting Time],[Ending Time],[Routing No_],[Routing Reference No_],[Prod_ Order Comp_ Line No_],[Finished],[Unit Cost Calculation],[Subcontracting],[Stop Code],[Scrap Code],[Work Center Group Code],[Work Shift Code],[Serial No_],[Lot No_],[Warranty Date],[New Serial No_],[New Lot No_],[New Item Expiration Date],[Item Expiration Date],[Return Reason Code],[Warehouse Adjustment],[Phys Invt Counting Period Code],[Phys Invt Counting Period Type],[BED _],[BED Amount],[Other Duties _],[Other Duties Amount],[Reverse Excise],[RG23PartI Entry No_],[RG23PartII Entry No_],[Return],[RG Correction Type],[RG Line No_],[Excise Record],[PO_SO Line No_],[Applies-to Entry (RG 23 D)],[Excise Entry],[Laboratory Test],[Other Usage],[Nature of Disposal],[Type of Disposal],[Captive Consumption],[From Transfer Order],[Re-Dispatch],[Assessable Value,[Rcpt_ Posting],[Transfer Cost],[Subcon Order No_],[Requisition No_],[Requisition Line No_],[NRGP_RRGP_RGP_MRO],[Ref_ Entry No_],[Overhead Rate] ,[Single-Level Material Cost],[Single-Level Capacity Cost],[Single-Level Subcontrd_ Cost],[Single-Level Cap_ Ovhd Cost],[Single-Level Mfg_ Ovhd Cost],[Rolled-up Material Cost],[Rolled-up Capacity Cost],[Rolled-up Subcontracted Cost],[Rolled-up Mfg_ Ovhd Cost],[Rolled-up Cap_ Overhead Cost],[GRN No_],[GRN Date]) ";
    //    cmd = new SqlCommand(sqlq, Conn);
    //    cmd.ExecuteNonQuery();

    //}

    //public void Insert_ApprovalData_Gen_Journal_Line(string tble_Name,string JournalTemplateName, string JournalBatchName, int AccountType, string AccountNo, DateTime PostingDate, int DocumentType, string DocumentNo, string Description, decimal VAT, string BalAccountNo, string CurrencyCode, decimal Amount, decimal DebitAmount, decimal CreditAmount, decimal AmountLCY, decimal BalanceLCY, decimal CurrencyFactor, decimal SalesPurchLCY, decimal ProfitLCY, decimal InvDiscountLCY, string BilltoPaytoNo, string PostingGroup, string ShortcutDimension1Code, string ShortcutDimension2Code, string SalespersPurchCode, string SourceCode, string SystemCreatedEntry, string OnHold, int AppliestoDoc_Type, string AppliestoDocNo, DateTime DueDate, DateTime PmtDiscountDate, decimal PaymentDiscount, string JobNo, decimal Quantity, decimal VATAmount, int VATPosting, string PaymentTermsCode, string AppliestoID, string BusinessUnitCode, string ReasonCode, int RecurringMethod, DateTime ExpirationDate, string RecurringFrequency, int GenPostingType, string Gen_BusPostingGroup, string GenProdPostingGroup, int VATCalculationType, string EU3PartyTrade, string AllowApplication, int BalAccountType, int BalGenPostingType, string BalGenBusPostingGroup, string BalGenProdPostingGroup, int BalVATCalculationType, decimal BalVAT, decimal BalVATAmount, int BankPaymentType, decimal VATBaseAmount, decimal BalVATBaseAmount, string Correction, string CheckPrinted, DateTime DocumentDate, string ExternalDocumentNo, int SourceType, string SourceNo, string PostingNoSeries, string TaxAreaCode, string TaxLiable, string TaxGroupCode, string UseTax, string BalTaxAreaCode, string BalTaxLiable, string BalTaxGroupCode, string BalUseTax, string VATBusPostingGroup, string VATProdPostingGroup, string BalVATBusPostingGroup, string BalVATProdPostingGroup, int AdditionalCurrencyPosting, decimal FAAddCurrencyFactor, string SourceCurrencyCode, decimal SourceCurrencyAmount, decimal SourceCurrVATBaseAmount, decimal SourceCurrVATAmount, decimal VATBaseDiscount, decimal VATAmountLCY, decimal VATBaseAmountLCY, decimal BalVATAmountLCY, decimal BalVATBaseAmountLCY, string ReversingEntry, string AllowZeroAmountPosting, string ShiptoOrderAddressCode, decimal VATDifference, decimal BalVATDifference, string ICPartnerCode, int ICDirection, string ICPartnerGLAccNo, int ICPartnerTransactionNo, string SelltoBuyfromNo, string VATRegistrationNo, string CountryRegionCode, string Prepayment, string FinancialVoid, string IncomingDocumentEntryNo, string CreditorNo, string PaymentReference, string PaymentMethodCode, string AppliestoExtDocNo, string RecipientBankAccount, string MessagetoRecipient, string ExportedtoPaymentFile, string HasPaymentExportError, string DimensionSetID, string CreditCardNo, string JobTaskNo, Decimal JobUnitPriceLCY, decimal JobTotalPriceLCY, decimal JobQuantity, decimal JobUnitCostLCY, decimal JobLineDiscount, decimal JobLineDiscAmountLCY, string JobUnitOfMeasureCode, string JobLineType, decimal JobUnitPrice, decimal JobTotalPrice, decimal JobUnitCost, decimal JobTotalCost, decimal JobLineDiscountAmount, decimal JobLineAmount, decimal JobTotalCostLCY, decimal JobLineAmountLCY, decimal JobCurrencyFactor, string JobCurrencyCode, int JobPlanningLineNo, decimal JobRemainingQty, string DirectDebitMandateID, int PostingExchEntryNo, string PayerInformation, string TransactionInformation, int PostingExchLineNo, string AppliedAutomatically, string CampaignNo, string ProdOrderNo, DateTime FAPostingDate, string FAPostingType, string DepreciationBookCode, decimal SalvageValue, int NoofDepreciationDays, string DepruntilFAPostingDate, string DeprAcquisitionCost, string MaintenanceCode, string InsuranceNo, string BudgetedFANo, string DuplicateinDepreciationBook, string UseDuplicationList, string FAReclassificationEntry, int FAErrorEntryNo, string IndexEntry, int SourceLineNo, string Comment, decimal SourceCurrExciseAmount, decimal SourceCurrTaxAmount, string StateCode, string ExciseBusPostingGroup, string ExciseProdPostingGroup, decimal ExciseAmount, decimal BED, string BEDCalculationType, decimal AmountIncludingExcise, decimal ExciseBaseAmount, decimal TDSTCSAmount, decimal ServiceTax, decimal TaxAmount, string PLA, decimal Tax, decimal TaxBaseAmount, string Cenvat, string LocationCode, decimal SourceCurrTaxBaseAmount, decimal TaxAmountLCY, decimal TaxBaseAmountLCY, string TDSNatureofDeduction, string AssesseeCode, decimal TDSTCS, decimal TDSTCSAmtInclSurcharge, decimal BalTDSTCSIncludingSHECESS, string PartyType, string PartyCode, string FormCode, string FormNo, string LCNo, decimal WorkTaxBaseAmount, decimal WorkTaxpercentage, decimal WorkTaxAmount, string TDSCategory, decimal Surcharge, decimal SurchargeAmount, string ConcessionalCode, string WorkTaxPaid, string PayTDS, string PayWorkTax, string TDSEntry, string PayExcise, decimal TDSTCSBaseAmount, string ChallanNo, DateTime ChallanDate, string Adjustment, string PaySalesTax, string ECCNo, decimal BalanceWorkTaxAmount, string PayVAT, decimal VATClaimAmount, string RefundVAT, decimal BalanceSurchargeAmount, DateTime ChequeDate, decimal SurchargeBaseAmount, string TDSGroup, string WorkTaxNatureOfDeduction, string WorkTaxGroup, decimal BalanceTDSTCSAmount, decimal TempTDSTCSBase, string ExcisePosting, string ProductType, string ExciseCharge, string DeferredClaimFAExcise, string ChequeNo, string Deferred, string ServiceTaxType, string ServiceTaxGroupCode, string ServiceTaxRegistrationNo, decimal ServiceTaxBaseAmountLCY, decimal ServiceTaxAmount, string ServiceTaxEntry, decimal eCESS, decimal eCESSonTDSTCSAmount, decimal TotalTDSTCSInclSHECESS, decimal BalanceeCESSonTDSTCSAmt, string PerContract, string CapitalItem, string ItemNo, string ServiceTaxeCessAmount, string GoestoExciseEntry, string FromExcise, string TANNo, string VATType, string TDSFromOrders, string ConsignmentNoteNo, string DeclarationFormGTA, string ServiceTypeRevChrg, string TCSNatureofCollection, string PayTCS, string TCSEntry, string TCSType, string TCANNo, string TCSFromOrders, int FAShiftLineNo, decimal NonITCClaimableUsage, decimal InputCreditOutputTaxAmount, decimal AmountLoadedonItem, decimal TaxAmountLoadedonInventory, string Defferment, string VATEntry, decimal StandardDeductionAmount, decimal ServiceTaxRoundingPrecision, string ServiceTaxRoundingType, decimal StandardDeductionAmountACY, decimal InputOutputTaxAmount, string ExciseRefund, string VATAdjustmentEntry, string Trading, string SalesReturnOrder, string ExciseasServiceTaxCredit, string ServTaxonAdvancePayment, int TransactionNoServTax, decimal ADCVATAmount, string CVD, decimal SourceCurrADCVATAmount, decimal ServiceTaxSHECessAmount, decimal SHECessonTDSTCS, decimal SHECessonTDSTCSAmount, decimal BalSHECessonTDSTCSAmt, string TDSCertificateReceivable, string InputServiceDistribution, string StaleCheque, string STPureAgent, string NatureofServices, decimal WorkTaxApplied, decimal WTAmount, string WorkTax, string ReverseWorkTax, string CWIPGLType, string CWIP, string ShiftType, string IndustryType, int NoofDaysforShift, decimal VATablePurchaseTax, string SaleReturnType, DateTime RGServiceTaxSetOffDate, DateTime PLASetOffDate, string InsertSTRecoverable, string OfflineApplication, string STFromOrder, string UnApplicationEntry, string IncludeServTaxinTDSBase, decimal TDSLineAmount, string Posting, decimal AppliedTDSBaseAmount, string PoT, decimal TDSTCSBaseAmountApplied, string TDSTCSBaseAmountAdjusted, decimal WorkTaxBaseAmountApplied, string WorkTaxBaseAmountAdjusted, decimal TotServTaxAmountIntm, string Approvalstatus, string ApprovedBy, DateTime ApprovedOn, string GroupCompany, string EmployeeDimension, int Month, int Year)
    //{
    //    Connect();

    //    string sqlq = "insert into " + tble_Name + " values('" + JournalTemplateName + "','" + JournalBatchName + "'," + AccountType + ",'" + AccountNo + "','" + PostingDate + "', " + DocumentType + ",'" + DocumentNo + "',' " + Description + "','" + VAT + "', '" + BalAccountNo + "', '" + CurrencyCode + "', '" + Amount + "', '" + DebitAmount + "', '" + CreditAmount + "','" + AmountLCY + "','" + BalanceLCY + "','" + CurrencyFactor + "','" + SalesPurchLCY + "','" + ProfitLCY + "', '" + InvDiscountLCY + "','" + BilltoPaytoNo + "', '" + PostingGroup + "','" + ShortcutDimension1Code + "','" + ShortcutDimension2Code + "','" + SalespersPurchCode + "', '" + SourceCode + "','" + SystemCreatedEntry + "','" + OnHold + "'," + AppliestoDoc_Type + ",'" + AppliestoDocNo + "', '" + DueDate + "','" + PmtDiscountDate + "','" + PaymentDiscount + "', '" + JobNo + "', '" + Quantity + "', '" + VATAmount + "'," + VATPosting + ",'" + PaymentTermsCode + "','" + AppliestoID + "','" + BusinessUnitCode + "', '" + ReasonCode + "', " + RecurringMethod + ",'" + ExpirationDate + "','" + RecurringFrequency + "'," + GenPostingType + ",'" + Gen_BusPostingGroup + "','" + GenProdPostingGroup + "'," + VATCalculationType + ",'" + EU3PartyTrade + "','" + AllowApplication + "', " + BalAccountType + "," + BalGenPostingType + ",'" + BalGenBusPostingGroup + "', '" + BalGenProdPostingGroup + "', " + BalVATCalculationType + ", '" + BalVAT + "', '" + BalVATAmount + "', " + BankPaymentType + ",'" + VATBaseAmount + "','" + BalVATBaseAmount + "', '" + Correction + "', '" + CheckPrinted + "','" + DocumentDate + "', '" + ExternalDocumentNo + "'," + SourceType + ", '" + SourceNo + "','" + PostingNoSeries + "', '" + TaxAreaCode + "', '" + TaxLiable + "','" + TaxGroupCode + "', '" + UseTax + "','" + BalTaxAreaCode + "','" + BalTaxLiable + "', '" + BalTaxGroupCode + "', '" + BalUseTax + "','" + VATBusPostingGroup + "','" + VATProdPostingGroup + "','" + BalVATBusPostingGroup + "','" + BalVATProdPostingGroup + "'," + AdditionalCurrencyPosting + ",'" + FAAddCurrencyFactor + "', '" + SourceCurrencyCode + "','" + SourceCurrencyAmount + "','" + SourceCurrVATBaseAmount + "','" + SourceCurrVATAmount + "', '" + VATBaseDiscount + "', '" + VATAmountLCY + "', '" + VATBaseAmountLCY + "', '" + BalVATAmountLCY + "', '" + BalVATBaseAmountLCY + "','" + ReversingEntry + "','" + AllowZeroAmountPosting + "','" + ShiptoOrderAddressCode + "','" + VATDifference + "','" + BalVATDifference + "', '" + ICPartnerCode + "', " + ICDirection + ",'" + ICPartnerGLAccNo + "', " + ICPartnerTransactionNo + ",'" + SelltoBuyfromNo + "', '" + VATRegistrationNo + "','" + CountryRegionCode + "','" + Prepayment + "','" + FinancialVoid + "','" + IncomingDocumentEntryNo + "','" + CreditorNo + "','" + PaymentReference + "','" + PaymentMethodCode + "','" + AppliestoExtDocNo + "', '" + RecipientBankAccount + "','" + MessagetoRecipient + "','" + ExportedtoPaymentFile + "','" + HasPaymentExportError + "', '" + DimensionSetID + "','" + CreditCardNo + "', '" + JobTaskNo + "','" + JobUnitPriceLCY + "','" + JobTotalPriceLCY + "','" + JobQuantity + "','" + JobUnitCostLCY + "','" + JobLineDiscount + "','" + JobLineDiscAmountLCY + "','" + JobUnitOfMeasureCode + "','" + JobLineType + "','" + JobUnitPrice + "', '" + JobTotalPrice + "' ,'" + JobUnitCost + "', '" + JobTotalCost + "','" + JobLineDiscountAmount + "','" + JobLineAmount + "','" + JobTotalCostLCY + "','" + JobLineAmountLCY + "', '" + JobCurrencyFactor + "', '" + JobCurrencyCode + "'," + JobPlanningLineNo + ", '" + JobRemainingQty + "', '" + DirectDebitMandateID + "'," + PostingExchEntryNo + ",'" + PayerInformation + "', '" + TransactionInformation + "','" + PostingExchLineNo + "', '" + AppliedAutomatically + "','" + CampaignNo + "', '" + ProdOrderNo + "','" + FAPostingDate + "','" + FAPostingType + "','" + DepreciationBookCode + "','" + SalvageValue + "'," + NoofDepreciationDays + ", '" + DepruntilFAPostingDate + "', '" + DeprAcquisitionCost + "','" + MaintenanceCode + "', '" + InsuranceNo + "','" + BudgetedFANo + "','" + DuplicateinDepreciationBook + "','" + UseDuplicationList + "','" + FAReclassificationEntry + "'," + FAErrorEntryNo + ",'" + IndexEntry + "'," + SourceLineNo + " , '" + Comment + "','" + SourceCurrExciseAmount + "','" + SourceCurrTaxAmount + "','" + StateCode + "','" + ExciseBusPostingGroup + "', '" + ExciseProdPostingGroup + "','" + ExciseAmount + "','" + BED + "', '" + BEDCalculationType + "','" + AmountIncludingExcise + "','" + ExciseBaseAmount + "', '" + TDSTCSAmount + "', '" + ServiceTax + "', '" + TaxAmount + "','" + PLA + "','" + Tax + "','" + TaxBaseAmount + "','" + Cenvat + "','" + LocationCode + "','" + SourceCurrTaxBaseAmount + "','" + TaxAmountLCY + "','" + TaxBaseAmountLCY + "','" + TDSNatureofDeduction + "','" + AssesseeCode + "','" + TDSTCS + "','" + TDSTCSAmtInclSurcharge + "','" + BalTDSTCSIncludingSHECESS + "','" + PartyType + "','" + PartyCode + "','" + FormCode + "','" + FormNo + "','" + LCNo + "','" + WorkTaxBaseAmount + "', '" + WorkTaxpercentage + "','" + WorkTaxAmount + "','" + TDSCategory + "', '" + Surcharge + "', '" + SurchargeAmount + "','" + ConcessionalCode + "','" + WorkTaxPaid + "', '" + PayTDS + "','" + PayWorkTax + "','" + TDSEntry + "','" + PayExcise + "','" + TDSTCSBaseAmount + "','" + ChallanNo + "','" + ChallanDate + "', '" + Adjustment + "', '" + PaySalesTax + "','" + ECCNo + "','" + BalanceWorkTaxAmount + "','" + PayVAT + "','" + VATClaimAmount + "','" + RefundVAT + "', '" + BalanceSurchargeAmount + "','" + ChequeDate + "','" + SurchargeBaseAmount + "','" + TDSGroup + "','" + WorkTaxNatureOfDeduction + "', '" + WorkTaxGroup + "','" + BalanceTDSTCSAmount + "', '" + TempTDSTCSBase + "', '" + ExcisePosting + "','" + ProductType + "','" + ExciseCharge + "','" + DeferredClaimFAExcise + "','" + ChequeNo + "','" + Deferred + "','" + ServiceTaxType + "','" + ServiceTaxGroupCode + "', '" + ServiceTaxRegistrationNo + "','" + ServiceTaxBaseAmountLCY + "','" + ServiceTaxAmount + "','" + ServiceTaxEntry + "','" + eCESS + "','" + eCESSonTDSTCSAmount + "','" + TotalTDSTCSInclSHECESS + "','" + BalanceeCESSonTDSTCSAmt + "','" + PerContract + "','" + CapitalItem + "','" + ItemNo + "','" + ServiceTaxeCessAmount + "','" + GoestoExciseEntry + "','" + FromExcise + "','" + TANNo + "','" + VATType + "','" + TDSFromOrders + "','" + ConsignmentNoteNo + "','" + DeclarationFormGTA + "','" + ServiceTypeRevChrg + "','" + TCSNatureofCollection + "','" + PayTCS + "','" + TCSEntry + "','" + TCSType + "','" + TCANNo + "','" + TCSFromOrders + "'," + FAShiftLineNo + ",'" + NonITCClaimableUsage + "', '" + InputCreditOutputTaxAmount + "','" + AmountLoadedonItem + "','" + TaxAmountLoadedonInventory + "','" + Defferment + "','" + VATEntry + "','" + StandardDeductionAmount + "', '" + ServiceTaxRoundingPrecision + "','" + ServiceTaxRoundingType + "','" + StandardDeductionAmountACY + "','" + InputOutputTaxAmount + "', '" + ExciseRefund + "','" + VATAdjustmentEntry + "','" + Trading + "', '" + SalesReturnOrder + "','" + ExciseasServiceTaxCredit + "','" + ServTaxonAdvancePayment + "'," + TransactionNoServTax + ",'" + ADCVATAmount + "','" + CVD + "','" + SourceCurrADCVATAmount + "', '" + ServiceTaxSHECessAmount + "','" + SHECessonTDSTCS + "','" + SHECessonTDSTCSAmount + "','" + BalSHECessonTDSTCSAmt + "','" + TDSCertificateReceivable + "','" + InputServiceDistribution + "','" + StaleCheque + "','" + STPureAgent + "','" + NatureofServices + "','" + WorkTaxApplied + "','" + WTAmount + "','" + WorkTax + "','" + ReverseWorkTax + "','" + CWIPGLType + "','" + CWIP + "', '" + ShiftType + "','" + IndustryType + "', " + NoofDaysforShift + ",'" + VATablePurchaseTax + "','" + SaleReturnType + "','" + RGServiceTaxSetOffDate + "','" + PLASetOffDate + "','" + InsertSTRecoverable + "','" + OfflineApplication + "','" + STFromOrder + "','" + UnApplicationEntry + "','" + IncludeServTaxinTDSBase + "','" + TDSLineAmount + "','" + Posting + "','" + AppliedTDSBaseAmount + "', '" + PoT + "','" + TDSTCSBaseAmountApplied + "','" + TDSTCSBaseAmountAdjusted + "','" + WorkTaxBaseAmountApplied + "', '" + WorkTaxBaseAmountAdjusted + "','" + TotServTaxAmountIntm + "','" + Approvalstatus + "','" + ApprovedBy + "','" + ApprovedOn + "','" + GroupCompany + "','" + EmployeeDimension + "'," + Month + "," + Year + ")";


    //   // string sqlq = "update " + tbleName + " set [Leave Balance]=" + Leave_Balance + " where [Employee Code]='" + employeeid + "' and [Leave code] ='" + LeaveCode + "' ";
      
    //   cmd = new SqlCommand(sqlq, Conn);
    //    cmd.ExecuteNonQuery();

    //}



    public SqlDataReader count_workingdays(string tbl_Name, string fromdate, string todate, string userid)
    {
        Connect();

        //string s = "select count([Attendance Date]) as [Attendance Date] from " + tbl_Name + " where convert(date, [Attendance Date],103)  >='" + fromdate + "' and  convert(date, [Attendance Date],103)  <='" + todate + "' and [Off Day]='0' and Holiday='0' and [Employee Code]='" + userid + "'";
        string s = "select count([Attendance Date]) as [Attendance Date] from " + tbl_Name + " where convert(date, [Attendance Date],103)  >='" + fromdate + "' and  convert(date, [Attendance Date],103)  <='" + todate + "' and upper([Weekly Days])!='SUNDAY' and Holiday='0' and [Employee Code]='" + userid + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }




    public SqlDataReader count_persentdays(string tbl_Name, string fromdate, string todate, string userid)
    {
        Connect();

        string s = "select count([Attendance Date]) as [Attendance Date] from " + tbl_Name + " where convert(date, [Attendance Date],103)  >='" + fromdate + "' and  convert(date, [Attendance Date],103)  <='" + todate + "' and [Employee Code]='" + userid + "' and [Attendance Marked]='1'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader count_Leavedays(string tbl_Name, string fromdate, string todate, string userid)
    {
        Connect();

        string s = "select count([Attendance Date]) as [Attendance Date] from " + tbl_Name + " where convert(date, [Attendance Date],103)  >='" + fromdate + "' and  convert(date, [Attendance Date],103)  <='" + todate + "' and [Employee Code]='" + userid + "' and [Applied Leave]='1'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader Show_AllAttendancedata(string tbl_Name, string fromdate, string todate, string userid)
    {
        Connect();

        string s = "select * from " + tbl_Name + " where convert(date, [Attendance Date],103)  >='" + fromdate + "' and convert(date, [Attendance Date],103)  <='" + todate + "' and [Employee Code]='" + userid + "' and [Attendance Marked]='1' or   convert(date, [Attendance Date],103)  >='" + fromdate + "' and convert(date, [Attendance Date],103)  <='" + todate + "' and [Employee Code]='" + userid + "' and [Applied Leave]='1' ";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader Show_Store(string tbl_Name)
    {
        Connect();

        string s = "select * from " + tbl_Name + "  ";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public void Insert_Requition_data_afterApproval(string tableName,string No, string ShortcutDimension1Code, DateTime PostingDate, string NoSeries, DateTime LastDateModified, string UserID, int Authorized, int SentforAuthorization, int Declined, int DocumentType, string ShortcutDimension2Code, string LocationCode, int Authorise, string BranchCode, string Autherise_Decline_By, int IndentClose, int Release, string AuthorizeUserID, DateTime AuthorizeDate, DateTime AuthorizeTime, DateTime CreationDate, DateTime CreationTime, int Posted, string RequisitionBy, string IssueTo, string IssuedBy, DateTime IssuedDateTime, string PreparedBy, string CheckedBy, string ApprovedBy, int ShortClose, int Transfer, int SentforApproval, int Approved, int Status,DateTime ApprovedDateTime,int RequisitionType)
    {
        Connect();
        string sqlq = "insert into " + tableName + " ([No_],[Shortcut Dimension 1 Code],[Posting Date],[No_ Series],[Last Date Modified],[User ID],[Authorized],[Sent for Authorization],Declined,[Document Type],[Shortcut Dimension 2 Code],[Location Code],Authorise,[Branch Code],[Autherise_Decline By],[Indent Close],[Release],[Authorize User ID],[Authorize Date],[Authorize Time],[Creation Date],[Creation Time],Posted,[Requisition By],[Issue To],[Issued By],[Issued Date Time],[Prepared By],[Checked By],[Approved By],[Short Close],Transfer,[Sent for Approval],[Approved],[Status],[Approved Date Time],[Requisition Type]) values('" + No + "','" + ShortcutDimension1Code + "','" + PostingDate + "', '" + NoSeries + "', '" + LastDateModified + "', '" + UserID + "', " + Authorized + "," + SentforAuthorization + "," + Declined + ", " + DocumentType + ",'" + ShortcutDimension2Code + "', '" + LocationCode + "'," + Authorise + ", '" + BranchCode + "', '" + Autherise_Decline_By + "'," + IndentClose + ", " + Release + ", '" + AuthorizeUserID + "','" + AuthorizeDate + "','" + AuthorizeTime + "','" + CreationDate + "', '" + CreationTime + "'," + Posted + ", '" + RequisitionBy + "','" + IssueTo + "','" + IssuedBy + "','" + IssuedDateTime + "','" + PreparedBy + "', '" + CheckedBy + "', '" + ApprovedBy + "', " + ShortClose + ", " + Transfer + ", " + SentforApproval + "," + Approved + "," + Status + ",'" + ApprovedDateTime + "'," + RequisitionType + ")";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }



    public void Insert_Requition_data_afterApprovalinLine(string tableName, string RequisitionNo_, int Line_No, string No, string Description, string UnitofMeasureCode, decimal Quantity, string RequiredTime, string MonthlyConsumption, decimal LastQuantity, decimal LastRate, string SuppliersCode, string SuppliersName,  string DocumentType, decimal Rate, decimal Valuer, string Description2, string LocationCode, string Typeu, decimal POAssigneQty, decimal RemainingQty, decimal MRNQty, int IndentLineClosed, string CenterType, string MachineCenterNo, string Remarks, string MachineCenterName,  decimal QuantitytobeIssue, string IndentNo, int IndentLineNo, decimal QuantityToIssue, string ShortcutDimension1Code, string ShortcutDimension2Code, string ReasonForRequisition, decimal IndentQuantity, int IndentRequired, DateTime DueDate, decimal PendingIndentForRequisition,  string Status, int Selected, int RequisitionSelected, decimal ApprovedQty, string VariantCode, string ApproverID, DateTime OnDate, decimal ApproxAmount, int RequisitionType)
    {
        Connect();
        string sqlq = "insert into " + tableName + " ([Requisition No_],[Line No_],[No_],Description,[Unit of Measure Code],Quantity,[Required Time],[Monthly Consumption],[Last Quantity],[Last Rate],[Supplier_s Code],[Supplier_s Name],[Document Type],Rate,Value,[Description 2],[Location Code],Type,[PO Assigne Qty],[Remaining Qty],[MRN Qty],[Indent Line Closed],[Center Type],[Machine Center No_],Remarks,[Machine Center Name],[Quantity to be Issue],[Indent No_],[Indent Line No_],[Quantity To Issue],[Shortcut Dimension 1 Code],[Shortcut Dimension 2 Code],[Reason For Requisition],[Indent Quantity],[Indent Required],[Due Date],[Pending Indent For Requisition],[Status],Selected,[Requisition Selected],[Approved Qty],[Variant Code],[Approver ID],[On Date],[Approx Amount],[Requisition Type]) values('" + RequisitionNo_ + "' , " + Line_No + ",'" + No + "','" + Description + "', '" + UnitofMeasureCode + "','" + Quantity + "','" + RequiredTime + "', '" + MonthlyConsumption + "', '" + LastQuantity + "', '" + LastRate + "', '" + SuppliersCode + "', '" + SuppliersName + "', '" + DocumentType + "', '" + Rate + "', '" + Valuer + "', '" + Description2 + "', '" + LocationCode + "', '" + Typeu + "', '" + POAssigneQty + "','" + RemainingQty + "', '" + MRNQty + "', " + IndentLineClosed + ", '" + CenterType + "','" + MachineCenterNo + "','" + Remarks + "','" + MachineCenterName + "', '" + QuantitytobeIssue + "', '" + IndentNo + "', " + IndentLineNo + ", '" + QuantityToIssue + "', '" + ShortcutDimension1Code + "', '" + ShortcutDimension2Code + "','" + ReasonForRequisition + "','" + IndentQuantity + "', " + IndentRequired + ", '" + DueDate + "', '" + PendingIndentForRequisition + "', '" + Status + "', " + Selected + "," + RequisitionSelected + ",'" + ApprovedQty + "','" + VariantCode + "','" + ApproverID + "','" + OnDate + "','" + ApproxAmount + "'," + RequisitionType + ")";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }



    //public void Insert_Requition_data_afterApprovalinLine1(string tableName, string WorksheetTemplateName, string JournalBatchName, string LineNo, int Type, string No_, string Description, string Description2, decimal Quantity, string VendorNo, decimal DirectUnitCost, DateTime DueDate, string RequesterID, int Confirmed, string ShortcutDimension1Code, string ShortcutDimension2Code, string LocationCode, int RecurringMethod, DateTime ExpirationDate, string RecurringFrequency, DateTime OrderDate, string VendorItemNo, string SalesOrderNo, int SalesOrderLineNo, string SelltoCustomerNo, string ShiptoCode, string OrderAddressCode, string CurrencyCode, decimal CurrencyFactor, decimal QuantitytobeIssue, string IndentNo, int IndentLineNo, decimal QuantityToIssue, string ShortcutDimension1Code, string ShortcutDimension2Code, string ReasonForRequisition, decimal IndentQuantity, int IndentRequired, DateTime DueDate, decimal PendingIndentForRequisition, decimal ReservedQuantity, string Status, int Selected, int RequisitionSelected, decimal ApprovedQty, string VariantCode, string ApproverID, DateTime OnDate, decimal ApproxAmount, int RequisitionType)
    //{
    //    Connect();
    //    string sqlq = "insert into " + tableName + " ([Requisition No_],[Line No_],[No_],Description,[Unit of Measure Code],Quantity,[Required Time],[Monthly Consumption],[Last Quantity],[Last Rate],[Supplier's Code],[Supplier's Name],[Stock in Hand],[Document Type],Rate,Value,[Description 2],[Location Code],Type,[PO Assigne Qty],[Remaining Qty],[MRN Qty],[Indent Line Closed],[Center Type],[Machine Center No_],Remarks,[Machine Center Name],[Issued Quantity],[Quantity to be Issue],[Indent No_],[Indent Line No_],[Quantity To Issue],[Shortcut Dimension 1 Code],[Shortcut Dimension 2 Code],[Reason For Requisition],[Indent Quantity],[Indent Required],[Due Date],[Pending Indent For Requisition],[Reserved Quantity],[Status],Selected,[Requisition Selected],[Approved Qty],[Variant Code],[Approver ID],[On Date],[Approx Amount],[Requisition Type]) values('" + RequisitionNo_ + "' , " + Line_No + ",'" + No + "','" + Description + "', '" + UnitofMeasureCode + "','" + Quantity + "','" + RequiredTime + "', '" + MonthlyConsumption + "', '" + LastQuantity + "', '" + LastRate + "', '" + SuppliersCode + "', '" + SuppliersName + "', '" + StockinHand + "','" + DocumentType + "', '" + Rate + "', '" + Valuer + "', '" + Description2 + "', '" + LocationCode + "', '" + Typeu + "', '" + POAssigneQty + "','" + RemainingQty + "', '" + MRNQty + "', " + IndentLineClosed + ", '" + CenterType + "','" + MachineCenterNo + "','" + Remarks + "','" + MachineCenterName + "','" + IssuedQuantity + "', '" + QuantitytobeIssue + "', '" + IndentNo + "', " + IndentLineNo + ", '" + QuantityToIssue + "', '" + ShortcutDimension1Code + "', '" + ShortcutDimension2Code + "','" + ReasonForRequisition + "','" + IndentQuantity + "', " + IndentRequired + ", '" + DueDate + "', '" + PendingIndentForRequisition + "', '" + ReservedQuantity + "','" + Status + "', " + Selected + "," + RequisitionSelected + ",'" + ApprovedQty + "','" + VariantCode + "','" + ApproverID + "','" + OnDate + "','" + ApproxAmount + "'," + RequisitionType + ")";
    //    cmd = new SqlCommand(sqlq, Conn);
    //    cmd.ExecuteNonQuery();

    //}

    public SqlDataReader Show_NoSeries(string tbl_Name)
    {
        Connect();

        string s = "select * from " + tbl_Name + " where [Series Code]='REQ'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader Show_TopNoSeries(string tbl_Name)
    {
        Connect();

        string s = "select top 1 ([No_]) as  No from " + tbl_Name + " where [No_ Series]='REQ' order by [No_] desc";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public String UserGroup(string userid)
    {
        Connect(); String UserGroup = "";
        string Query = "select [User Group] from [Portal Users]  where [Login ID]='" + userid + "' ";
        cmd = new SqlCommand(Query, Conn);
        if (cmd.ExecuteScalar() != null)
        {
            UserGroup = cmd.ExecuteScalar().ToString();
        }
        return UserGroup;
    }
    public String FacultyCollege(string userid)
    {
        Connect(); String FacultyCollege = "";
        string Query = "select top 1 [Global Dimension 1 Code] from [Portal Users]  where [Login ID]='" + userid + "' order by [Global Dimension 1 Code] asc";
        cmd = new SqlCommand(Query, Conn);
        if (cmd.ExecuteScalar() != null)
        {
            FacultyCollege = cmd.ExecuteScalar().ToString();
        }
        return FacultyCollege;
    }



    // Date 01-09-2016 by Dhirendra
    public SqlDataReader Show_PayLeavedetailof_CL(string tbl_Name)
    {
        Connect();

        string s = "select * from " + tbl_Name + " where [Leave Code]='CL'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    // Date 01-09-2016 by Dhirendra
    public SqlDataReader Show_PayCompanyPolicy(string tbl_Name)
    {
        Connect();

        string s = "select * from " + tbl_Name + " with (NoLOCK) ";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    // Date 02-09-2016 by Dhirendra
    public SqlDataReader Show_EmployeeActualPunchData_viewAttendance(string tbl_Name,string EmployeeNo,string Month,string Year)
    {
        Connect();

        string s = "select CONVERT(varchar(11) ,[Attendance Date],106) as [Attendance Date] ,[Week Day], (FORMAT(CAST([Shift Time In] AS DATETIME),'HH:mm')  + '   -   ' + FORMAT(CAST([Shift Time Out] AS DATETIME),'HH:mm') ) as ShiftTime, FORMAT(CAST([Time From] AS DATETIME),'HH:mm') AS [Time From],FORMAT(CAST([Time To] AS DATETIME),'HH:mm') AS [Time To],[dbo].DecimalToTime([Total Hours]) as WorkingHour,cast([Total Hours] as decimal(10,2)) as WorkingHour1, cast([Morning Late] as decimal(10,2)) as LateBy, cast([Early Departure in Evening] as decimal(10,2)) as EarlyBy,Status,FORMAT(CAST([Shift Time In] AS DATETIME),'HH:mm') as shiftTimeIn, FORMAT(CAST([Shift Time Out] AS DATETIME),'HH:mm') as ShiftTimeOut from " + tbl_Name + " where [Employee No]='" + EmployeeNo + "' and DATEPART(mm,[Attendance Date])='" + Month + "' and DATEPART(yyyy,[Attendance Date])='" + Year + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    // Date 02-09-2016 by Dhirendra
    public SqlDataReader Show_PayDailyAttendance_viewAttendance(string tbl_Name, string EmployeeNo, string Attend_Date)
    {
        Connect();

        string s = "select * from " + tbl_Name + " where [Employee Code]='" + EmployeeNo + "' and convert(date,[Attendance Date],131)='" + Attend_Date + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    // Date 06-09-2016 by Dhirendra
    public SqlDataReader Show_9PunchdataInTime(string tbl_Name, string EmployeeMachineCode, string PunchDate)
    {
        Connect();

        string s = "select MIN((FORMAT(CAST([Punch Time] AS DATETIME),'HH:mm'))) as InTime from " + tbl_Name + " where [Employee Machine Code]='" + EmployeeMachineCode + "' and convert(date,[Punch Date],131)='" + PunchDate + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public SqlDataReader Show_9PunchdataOutTime(string tbl_Name, string EmployeeMachineCode, string PunchDate)
    {
        Connect();

        string s = "select MAX((FORMAT(CAST([Punch Time] AS DATETIME),'HH:mm'))) as OutTime from " + tbl_Name + " where [Employee Machine Code]='" + EmployeeMachineCode + "' and convert(date,[Punch Date],131)='" + PunchDate + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader Show_Punchesdata9withloop(string tbl_Name, string EmployeeMachineCode, string PunchDate)
    {
        Connect();

        string s = "select * from " + tbl_Name + " where [Employee Machine Code]='" + EmployeeMachineCode + "' and convert(date,[Punch Date],131)='" + PunchDate + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public void Update_OD_Approval(string tbleName, string Timein, string TimeOut,string userid, string fdate, string tdate,string odRemarks)
    {
        Connect();
        string sqlq = "update " + tbleName + " set [Time in]='" + Timein + "',[Time Out]='" + TimeOut + "',Status='1',[Attendance Marked]='1',[Applied Leave]='0',OD='1',[OD Remarks]='" + odRemarks + "' where [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) >='" + fdate + "'  and CONVERT(date, [Attendance Date],103) <='" + tdate + "'";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }


    public SqlDataReader Show_LeaveAppliedFOROD(string tbl_Name, string fromdate, string Todate, string userid)
    {
        Connect();

        string s = "select * from " + tbl_Name + " where convert(date, [Attendance Date],103) >='" + fromdate + "' and convert(date, [Attendance Date],103) <='" + Todate + "' and [Employee Code]='" + userid + "' and [Applied Leave]='1'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    //dhirendra
    public SqlDataReader Show_HolidayListPayHolidaysss(string tbl_Name, string fromdate, string todate, string BranchCode,string weekday)
    {
        Connect();

        string s = "select count(Date) as CountDate  from " + tbl_Name + " where convert(date, [Date],103)  >='" + fromdate + "' and  convert(date, [Date],103)  <='" + todate + "' and [Branch Code]='" + BranchCode + "' and DATENAME(dw,Date) !='"+weekday+"'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader Show_CountMonthlyWeekoffemployee(string tbl_Name, string fromdate, string todate, string ShiftPattern, string ShiftCode,string employeeno)
    {
        Connect();

        string s = "select count([Shift Code]) as ShiftCode  from " + tbl_Name + " where convert(date, [Start Date],103)  >='" + fromdate + "' and  convert(date, [Start Date],103)  <='" + todate + "' and [Employee No_]='" + employeeno + "' and [Shift Pattern]='" + ShiftPattern + "' and [Shift Code]='"+ShiftCode+"'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


   
   


    public SqlDataReader Show_HolidayListPayHolidayMonthly(string tbl_Name, string fromdate, string todate, string BranchCode, string weekdate)
    {
        Connect();

        string s = "select count(Date) as CountDate  from " + tbl_Name + " where convert(date, [Date],103)  >='" + fromdate + "' and  convert(date, [Date],103)  <='" + todate + "' and [Branch Code]='" + BranchCode + "' and convert(date, [Date],103)!='"+weekdate+"'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader Show_weekofforapproval(string companyName, string userid)
    {
        Connect();
        string s = "select * from " + companyName + " where [No_]='" + userid + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader Show_HolidayListforapproval(string tbl_Name, string fromdate, string todate, string BranchCode, string weekday)
    {
        Connect();

        string s = "select *  from " + tbl_Name + " where convert(date, [Date],103)  >='" + fromdate + "' and  convert(date, [Date],103)  <='" + todate + "' and [Branch Code]='" + BranchCode + "' and DATENAME(dw,Date) !='" + weekday + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }



    //12-11-2016
    public void insert_Table_Credit_Leave(string tablename, string EmployeeCode, DateTime dates, string leaveCode, decimal noofleave, int active, string LeaveID)
    {
        Connect();
        string sqlq = "insert into " + tablename + " ([Employee Code],Date,[Leave Code],[No_ of Leaves],Active,[Leave ID]) values('" + EmployeeCode + "','" + dates + "','" + leaveCode + "','" + noofleave + "'," + active + ",'" + LeaveID + "')";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }

    public void Update_Co_ApplicationApproval(string tbleName, string userid, string attendadet, string CoRemarks)
    //public void Update_Co_ApplicationApproval(string tbleName, string Timein, string TimeOut, string hourpresnt, string userid, string attendadet, string CoRemarks)
    {
        Connect();
        //string sqlq = "update " + tbleName + " set [Time in]='" + Timein + "',[Time Out]='" + TimeOut + "', [Hours Present]='" + hourpresnt + "',Status='1', [Attendance Marked]='1',[Applied Leave]='0',[CO]='1' , [CO Status]='Pending',[CO Remarks]='" + CoRemarks + "' where [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) ='" + attendadet + "'";
        string sqlq = "update " + tbleName + " set Status='1', [Attendance Marked]='1',[Applied Leave]='0',[CO]='1' , [CO Status]='Pending',[CO Remarks]='" + CoRemarks + "' where [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) ='" + attendadet + "'";

        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }
    public void insert_Table_Credit_LeaveCO(string tablename, string EmployeeCode, string dates, string leaveCode, decimal noofleave, int active, string LeaveID, string Remarks)
    {
        Connect();
        string sqlq = "insert into " + tablename + " ([Employee Code],Date,[Leave Code],[No_ of Leaves],Active,[Leave ID],Remarks) values('" + EmployeeCode + "','" + dates + "','" + leaveCode + "','" + noofleave + "'," + active + ",'" + LeaveID + "','" + Remarks + "')";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }

    public SqlDataReader ShowCo_Leave_BalancePOstCo(string tblename, string userid)
    {
        Connect();

        string s = "select * from " + tblename + " where [Employee Code]='" + userid + "' and [Leave code]='CO'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public void updateCoLeaveBalancePlusHRCO(string tablename, string LeaveBalance, string EmployeeCode)
    {
        Connect();
        string sqlq = "update " + tablename + " set [Leave Balance]='" + LeaveBalance + "' where [Employee Code]='" + EmployeeCode + "' and [Leave code]='CO'";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }
    //dhirendra 13-11-2016
    public SqlDataReader Show_HolidayListPayHolidayDate(string tbl_Name, string applieddate, string BranchCode)
    {
        Connect();

        string s = "select * from " + tbl_Name + " where convert(date, [Date],103) ='" + applieddate + "' and [Branch Code]='" + BranchCode + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public SqlDataReader Show_CountMonthlyWeekoffemployeeDate(string tbl_Name, string fromdate, string ShiftPattern, string ShiftCode, string employeeno)
    {
        Connect();

        string s = "select * from " + tbl_Name + " where convert(date, [Start Date],103) ='" + fromdate + "' and [Employee No_]='" + employeeno + "' and [Shift Pattern]='" + ShiftPattern + "' and [Shift Code]='" + ShiftCode + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader Show_HolidayListPayHolidayMonthlyDate(string tbl_Name, string fromdate, string BranchCode)
    {
        Connect();

        string s = "select *  from " + tbl_Name + " where convert(date, [Date],103)  ='" + fromdate + "' and [Branch Code]='" + BranchCode + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public void updateCoLeaveInative(string tablename, string EmployeeCode, string Attendate)
    {
        Connect();
        string sqlq = "update " + tablename + " set [Active]='0' where [Employee Code]='" + EmployeeCode + "' and [Leave Code]='CO' and convert(Date,Date,103)='" + Attendate + "'";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }



    public SqlDataReader ShowCOleaveBalanceIncative(string tbl_Name, string EmployeeCode)
    {
        Connect();

        string s = "select *  from " + tbl_Name + " where [Employee Code] ='" + EmployeeCode + "' and [Leave code]='CO'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public void updateCoBalanceforInactivedd(string tablename, string LeaveBalance, string EmployeeCode)
    {
        Connect();
        string sqlq = "update " + tablename + " set [Leave Balance] ='" + LeaveBalance + "' where [Employee Code] ='" + EmployeeCode + "' and [Leave code]='CO'";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }

    public SqlDataReader SHow_showHODExhistCO(string userid, string companyName)
    {
        Connect();
        string s = "select * from " + companyName + " where  [HOD]='" + userid + "'  or [HOD 1]='" + userid + "'";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    //dhirendra 2016-11-22
    public void UpdatePRELunchDatad_FromDate(string tbleName, string Status, string userid, string fdate, string leaveType, string HalfDayLeaveType)
    {
        Connect();

        string sqlq = "update " + tbleName + " set Status='" + Status + "',[Attendance Marked]='0',[Applied Leave]='1',[Leave Type]='" + leaveType + "',[Half Day Leave Type]='" + HalfDayLeaveType + "' where [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) ='" + fdate + "'";
 
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }


    public SqlDataReader SHow_EmployeeMobileNo(string userid, string companyName)
    {
        Connect();
        string s = "select [Mobile Phone No_] as MobilePhoneNo from " + companyName + " where  [No_]='" + userid + "'";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    // Examination
    public SqlDataReader SHow_HODMobileNo(string course, string companyName)
    {
        Connect();
        string s = "select [Mobile Phone No_] as MobilePhoneNo from " + companyName + " where  [No_]=(select [HOD] from [TMU$User Role Matrix] where [Course Code]='" + course + "')";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    //Transport
    public SqlDataReader Show_TransportNo(string EmpNo, string companyName)
    {

        Connect();
        string s = "select  [Mobile Phone No_] as MobilePhoneNo from " + companyName + " where  [No_]=(select top 1 [Transport Approval] from [TMU$User Role Matrix])";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    //Management
    public SqlDataReader Show_Management(string EmpNo, string companyName)
    {

        Connect();
        string s = "select  [Mobile Phone No_] as MobilePhoneNo from " + companyName + " where  [No_]=(select top 1 [Management approval] from [TMU$User Role Matrix])";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    // sms leave co Od Acording by subham gupta

    public SqlDataReader Show_AthorityNo(string EmpNo, string companyName)
    {

        Connect();
        string s = "select  [Mobile Phone No_] as MobilePhoneNo from " + companyName + " where  [No_]=(select [HOD] from [TMU$Employee] where No_='" + EmpNo + "')";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }



    public SqlDataReader SHow_PrinciplaNo(string Globle1, string companyName)
    {
        Connect();
        string s = "select  [Mobile Phone No_] as MobilePhoneNo from " + companyName + " where  [No_]=(select top 1 [Principal] from [TMU$User Role Matrix] where [Global Dimenison 1 Code]='" + Globle1 + "')";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader SHow_StudentMobileNo(string No_, string companyName)
    {
        Connect();
        string s = "select [Mobile Number] as MobilePhoneNo from " + companyName + " where  [No_]='" + No_ + "'";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    //


    public SqlDataReader SHow_EmployeeBasicdetails(string userid, string companyName)
    {
        Connect();
        string s = "select * from " + companyName + " where  [No_]='" + userid + "'";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    //29-11-2016 
    public SqlDataReader SHow_CourseDetails29m(string companyName,string College)
    {
        Connect();
        string s = "select (Code + '     ' + Description) as Description, Code from " + companyName + " where [Global Dimension 1 Code]='"+College+"' order by Description,Code";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader SHow_CourseDetails29mSemYear(string companyName,string Code)
    {
        Connect();
        string s = "select * from " + companyName + " where Code='" + Code + "'";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader SHow_CourseDetails29semester(string companyName, string Code)
    {
        Connect();
        string s = "select distinct([Semester Code]) as semcode from " + companyName + " where [Course Code]='" + Code + "' order by semcode";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader SHow_CourseDetails29CourseYear(string companyName, string Code)
    {
        Connect();
        string s = "select distinct([Year Code]) as YearCode from " + companyName + " where [Course Code]='" + Code + "' order by YearCode";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader SHow_CourseDetails29CourseSection(string companyName, string Code,string semester,string year,string typeofSemesteryear)
    {
        Connect();
        //string s = "";
        //string s = "select distinct([Section Code]) as SectionCode from " + companyName + " where [Course Code]='" + Code + "' order by [Section Code]"; //by ashu 08-12-2016
        string s = "select distinct([Section Code]) as SectionCode,[Section Code] as SectionDesc from " + companyName + " where [Course Code]='" + Code + "' order by [Section Code]";
        //if (typeofSemesteryear == "Semester")
        //{
        //    s = "select distinct([Section Code]) as SectionCode from " + companyName + " where [Course Code]='" + Code + "' and Semester ='" + semester + "' order by SectionCode";

        //}
        //if (typeofSemesteryear == "Year")
        //{
        //    s = "select distinct([Section Code]) as SectionCode from " + companyName + " where [Course Code]='" + Code + "' Year='" + year + "' order by SectionCode";

        //}
        
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader SHow_CourseDetails29SubjectCOLLEGE(string companyName, string Course, string Semester, string Year, string semseteryear)
    {
        Connect();
        string s = "";

        if (semseteryear == "Semester")
        {
            s = "select (Code + '     ' + Description) as Description, Code from " + companyName + " where Course='" + Course + "' and Semester='" + Semester + "'  order by Description,Code";

        }
        else if (semseteryear == "Year")
        {
            s = "select (Code + '     ' + Description) as Description, Code from " + companyName + " where Course='" + Course + "' and  Year='" + Year + "' order by Description,Code";

        }
        else
        {
            s = "select (Code + '     ' + Description) as Description, Code from " + companyName + " where Course='" + Course + "' and (Semester='" + Semester + "' or Year='" + Year + "') order by Description,Code";

        }
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    
    
    
    public SqlDataReader SHow_CourseDetails29Faculty(string companyName, string Course, string Semester, string Year)
    {
        Connect();
        string s = "select distinct(([Faculty Code] +'   ' + [Faculty Name])) as Faculty ,[Faculty Code] from " + companyName + " where [Course Code]='" + Course + "' and ([Semester Code]='" + Semester + "' or [Year Code]='" + Year + "') order by [Faculty Code],Faculty";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader SHow_CourseDetails29Faculty_Asu(string companyName, string Employeetable, string Course, string Semester, string Year, string Subject, string section, string academicyrs, string semseteryear)
    {
        Connect();
        string s = "";
        //string s = "select distinct((CS.[Faculty Code] + '   ' + E.[Search Name])) as Faculty , CS.[Faculty Code] from " + companyName + " CS inner join " + Employeetable + " E on E.No_=CS.[Faculty Code] where CS.[Course Code]='" + Course + "' and (CS.Semester='" + Semester + "' or CS.[Year]='" + Year + "') and CS.[Subject Code]='" + Subject + "' and CS.Section='" + section + "' and CS.[Academic Year]='"+academicyrs+"' order by CS.[Faculty Code],Faculty ";
        if (section == "--Faculty--")
        {
            if (semseteryear == "Semester")
            {

                s = "select distinct((CS.[Faculty Code] + '   ' + E.[Search Name])) as Faculty , CS.[Faculty Code] as [Faculty Code]  from " + companyName + " CS inner join " + Employeetable + " E on E.No_=CS.[Faculty Code] where CS.[Course Code]='" + Course + "' and CS.Semester='" + Semester + "' and CS.[Subject Code]='" + Subject + "' and CS.[Academic Year]='" + academicyrs + "' order by [Faculty Code],Faculty ";
            
            }

            if (semseteryear == "Year")
            {

                s = "select distinct((CS.[Faculty Code] + '   ' + E.[Search Name])) as Faculty , CS.[Faculty Code] as [Faculty Code]  from " + companyName + " CS inner join " + Employeetable + " E on E.No_=CS.[Faculty Code] where CS.[Course Code]='" + Course + "' and  CS.[Year]='" + Year + "' and CS.[Subject Code]='" + Subject + "' and CS.[Academic Year]='" + academicyrs + "' order by [Faculty Code],Faculty ";
            
            
            
            }


        }
        else
        {
            if (semseteryear == "Semester")
            {
                s = "select distinct((CS.[Faculty Code] + '   ' + E.[Search Name])) as Faculty , CS.[Faculty Code] as [Faculty Code]  from " + companyName + " CS inner join " + Employeetable + " E on E.No_=CS.[Faculty Code] where CS.[Course Code]='" + Course + "' and CS.Semester='" + Semester + "' and CS.[Subject Code]='" + Subject + "' and CS.Section='" + section + "' and CS.[Academic Year]='" + academicyrs + "' order by [Faculty Code],Faculty ";
           
            
            }
            if (semseteryear == "Year")
            {
                s = "select distinct((CS.[Faculty Code] + '   ' + E.[Search Name])) as Faculty , CS.[Faculty Code] as [Faculty Code]  from " + companyName + " CS inner join " + Employeetable + " E on E.No_=CS.[Faculty Code] where CS.[Course Code]='" + Course + "' and CS.[Year]='" + Year + "' and CS.[Subject Code]='" + Subject + "' and CS.Section='" + section + "' and CS.[Academic Year]='" + academicyrs + "' order by [Faculty Code],Faculty ";

            }
        }
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader SHow_CourseDetails29StudentList(string companyName, string Course, string Semester, string Year, string Section, string SubjectCode, string academicyrs, string semesteryrs, string Method,string Sdate, string Tdate, string HnFrom,string HnTill)
    {
        Connect();
        string s = "";
        if (Section == "--Section--")
        {
            if (semesteryrs == "Semester")
            {
                if (Method == "")
                {
                    s = "select [Enrollment No], [Student Name],[Student No_] from " + companyName + " where Course='" + Course + "' and [Semester]='" + Semester + "' and [Subject Code]='" + SubjectCode + "' and Remedial='0' and [Academic Year]='" + academicyrs + "' and [Student No_] COLLATE Latin1_General_CI_AS not in (select [Student No] from HRMSPortal.dbo.tbl_RemedialClass where [Start Date]='" + Sdate + "'and [End Date]='" + Tdate + "' and [Hour No From]='" + HnFrom + "' and [Hour No Till]='" + HnTill + "') order by [Enrollment No],[Student Name]";
                 
                }
                else
                {
                   // s = "select [Enrollment No], [Student Name],[Student No_] from " + companyName + " where Course='" + Course + "' and [Semester]='" + Semester + "' and [Subject Code]='" + SubjectCode + "' and Remedial='0' and [Academic Year]='" + academicyrs + "'  order by [Enrollment No],[Student Name]";
                    s = "select * from  (select [Enrollement No] as [Enrollment No], [Student Name],[Student No_],[Global Dimension 1 Code],[Exam Method],isnull((select [Maximum] from [TMU$Course Subj Ex Group Line-COL] where  [Exam Method]=SIL.[Exam Method] and [Academic year]=SIL.[Academic Year] and [Global Dimension 1 Code]=SIL.[Global Dimension 1 Code] and [Document No_]=SIL.[Subject Exam Craiteria] ),0)  as maximummark ,isnull((select [Minimum] from [TMU$Course Subj Ex Group Line-COL] where  [Exam Method]=SIL.[Exam Method] and [Academic year]=SIL.[Academic Year] and [Global Dimension 1 Code]=SIL.[Global Dimension 1 Code] and [Document No_]=SIL.[Subject Exam Craiteria] ),0) as Minimumper  ,[Internal Assesment (IA) Marks],([Internal Assesment (IA) Marks]*100/isnull((select [Maximum] from [TMU$Course Subj Ex Group Line-COL] where [Exam Method]=SIL.[Exam Method] and [Academic year]=SIL.[Academic Year] and [Global Dimension 1 Code]=SIL.[Global Dimension 1 Code] and [Document No_]=SIL.[Subject Exam Craiteria] ),0) ) as per from [TMU$Student Internal Line -TMU] SIL where Course='" + Course + "'  and [Semester]='" + Semester + "' and Section='" + Section + "' and [Subject Code]='" + SubjectCode + "'   and [Academic Year]='" + academicyrs + "' and SIL.[Exam Method]='" + Method + "' and SIL.[Status]='4') as t   where t.Minimumper>=t.per   order by [Enrollment No],[Student Name]";
               
                
                }
            }
            if (semesteryrs == "Year")
            {
                if (Method == "")
                {
                    s = "select [Enrollment No], [Student Name],[Student No_] from " + companyName + " where Course='" + Course + "' and [Year]='" + Year + "' and [Subject Code]='" + SubjectCode + "' and Remedial='0' and [Academic Year]='" + academicyrs + "' and [Student No_] COLLATE Latin1_General_CI_AS not in (select [Student No] from HRMSPortal.dbo.tbl_RemedialClass where [Start Date]='" + Sdate + "'and [End Date]='" + Tdate + "' and [Hour No From]='" + HnFrom + "' and [Hour No Till]='" + HnTill + "') order by [Enrollment No],[Student Name]";
                }
                else
                {

                    s = "select * from  (select [Enrollement No] as [Enrollment No], [Student Name],[Student No_],[Global Dimension 1 Code],[Exam Method],isnull((select [Maximum] from [TMU$Course Subj Ex Group Line-COL] where  [Exam Method]=SIL.[Exam Method] and [Academic year]=SIL.[Academic Year] and [Global Dimension 1 Code]=SIL.[Global Dimension 1 Code] and [Document No_]=SIL.[Subject Exam Craiteria] ),0)  as maximummark ,isnull((select [Minimum] from [TMU$Course Subj Ex Group Line-COL] where  [Exam Method]=SIL.[Exam Method] and [Academic year]=SIL.[Academic Year] and [Global Dimension 1 Code]=SIL.[Global Dimension 1 Code] and [Document No_]=SIL.[Subject Exam Craiteria] ),0) as Minimumper  ,[Internal Assesment (IA) Marks],([Internal Assesment (IA) Marks]*100/isnull((select [Maximum] from [TMU$Course Subj Ex Group Line-COL] where [Exam Method]=SIL.[Exam Method] and [Academic year]=SIL.[Academic Year] and [Global Dimension 1 Code]=SIL.[Global Dimension 1 Code] and [Document No_]=SIL.[Subject Exam Craiteria] ),0) ) as per from [TMU$Student Internal Line -TMU] SIL where Course='" + Course + "'  and [Year]='" + Year + "' and Section='" + Section + "' and [Subject Code]='" + SubjectCode + "'   and [Academic Year]='" + academicyrs + "'  and SIL.[Exam Method]='" + Method + "' and SIL.[Status]='4') as t   where t.Minimumper>=t.per   order by [Enrollment No],[Student Name]";

                }
                  
             }
        }
        else
        {
            if (semesteryrs == "Semester")
            {
                if (Method == "")
                {
                    s = "select [Enrollment No], [Student Name],[Student No_] from " + companyName + " where Course='" + Course + "' and [Semester]='" + Semester + "' and Section='" + Section + "' and [Subject Code]='" + SubjectCode + "'  and [Academic Year]='" + academicyrs + "' and [Student No_] COLLATE Latin1_General_CI_AS not in (select [Student No] from HRMSPortal.dbo.tbl_RemedialClass where [Start Date]>='" + Sdate + "'and [End Date]<='" + Tdate + "' and [Hour No From]>='" + HnFrom + "' and [Hour No Till]<='" + HnTill + "') order by [Enrollment No],[Student Name]";
                }
                else
                {
                    s = "select * from  (select [Enrollement No] as [Enrollment No], [Student Name],[Student No_],[Global Dimension 1 Code],[Exam Method],isnull((select [Maximum] from [TMU$Course Subj Ex Group Line-COL] where  [Exam Method]=SIL.[Exam Method] and [Academic year]=SIL.[Academic Year] and [Global Dimension 1 Code]=SIL.[Global Dimension 1 Code] and [Document No_]=SIL.[Subject Exam Craiteria] ),0)  as maximummark ,isnull((select [Minimum] from [TMU$Course Subj Ex Group Line-COL] where  [Exam Method]=SIL.[Exam Method] and [Academic year]=SIL.[Academic Year] and [Global Dimension 1 Code]=SIL.[Global Dimension 1 Code] and [Document No_]=SIL.[Subject Exam Craiteria] ),0) as Minimumper  ,[Internal Assesment (IA) Marks],([Internal Assesment (IA) Marks]*100/isnull((select [Maximum] from [TMU$Course Subj Ex Group Line-COL] where [Exam Method]=SIL.[Exam Method] and [Academic year]=SIL.[Academic Year] and [Global Dimension 1 Code]=SIL.[Global Dimension 1 Code] and [Document No_]=SIL.[Subject Exam Craiteria] ),0) ) as per from [TMU$Student Internal Line -TMU] SIL where Course='" + Course + "'  and [Semester]='" + Semester + "' and Section='" + Section + "' and [Subject Code]='" + SubjectCode + "'   and [Academic Year]='" + academicyrs + "'  and SIL.[Exam Method]='" + Method + "' and SIL.[Status]='4') as t   where t.Minimumper>=t.per   order by [Enrollment No],[Student Name]";
               
                }
            
            }
            if (semesteryrs == "Year")
            {
                if (Method == "")
                {
                    s = "select [Enrollment No], [Student Name],[Student No_] from " + companyName + " where Course='" + Course + "' and [Year]='" + Year + "' and Section='" + Section + "' and [Subject Code]='" + SubjectCode + "' and Remedial='0' and [Academic Year]='" + academicyrs + "' and [Student No_] COLLATE Latin1_General_CI_AS not in (select [Student No] from HRMSPortal.dbo.tbl_RemedialClass where [Start Date]='" + Sdate + "'and [End Date]='" + Tdate + "' and [Hour No From]='" + HnFrom + "' and [Hour No Till]='" + HnTill + "') order by [Enrollment No],[Student Name]";
                }
                else
                {
                    s = "select * from  (select [Enrollement No] as [Enrollment No], [Student Name],[Student No_],[Global Dimension 1 Code],[Exam Method],isnull((select [Maximum] from [TMU$Course Subj Ex Group Line-COL] where  [Exam Method]=SIL.[Exam Method] and [Academic year]=SIL.[Academic Year] and [Global Dimension 1 Code]=SIL.[Global Dimension 1 Code] and [Document No_]=SIL.[Subject Exam Craiteria] ),0)  as maximummark ,isnull((select [Minimum] from [TMU$Course Subj Ex Group Line-COL] where  [Exam Method]=SIL.[Exam Method] and [Academic year]=SIL.[Academic Year] and [Global Dimension 1 Code]=SIL.[Global Dimension 1 Code] and [Document No_]=SIL.[Subject Exam Craiteria] ),0) as Minimumper  ,[Internal Assesment (IA) Marks],([Internal Assesment (IA) Marks]*100/isnull((select [Maximum] from [TMU$Course Subj Ex Group Line-COL] where [Exam Method]=SIL.[Exam Method] and [Academic year]=SIL.[Academic Year] and [Global Dimension 1 Code]=SIL.[Global Dimension 1 Code] and [Document No_]=SIL.[Subject Exam Craiteria] ),0) ) as per from [TMU$Student Internal Line -TMU] SIL where Course='" + Course + "'  and [Year]='" + Year + "' and Section='" + Section + "' and [Subject Code]='" + SubjectCode + "'   and [Academic Year]='" + academicyrs + "' and SIL.[Exam Method]='" + Method + "' and SIL.[Status]='4') as t   where t.Minimumper>=t.per   order by [Enrollement No],[Student Name]";
                }
            }
        }
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public void UpdateStudentSubjectCollegeDel(string tbleName, string Remedial, string Course, string Section, string SubjectCode, string studentNo, string EnrolNo, string Semester)
    {
        Connect();

        string sqlq = "update " + tbleName + " set Remedial='" + Remedial + "' where Course='" + Course + "' and Section='" + Section + "' and [Subject Code]='" + SubjectCode + "' and [Student No_]='" + studentNo + "' and [Enrollment No]='" + EnrolNo + "' and ([Semester]='" + Semester + "' or [Year]='" + Semester + "') ";

        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }


    public SqlDataReader SHow_RoomAllocation(string companyName, string Course)
    {
        Connect();
        string s = "select distinct([Room No_]) as RoomNo from " + companyName + " where Course='" + Course + "' order by RoomNo";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader SHow_SubjectClassification(string companyName, string Code)
    {
        Connect();
        string s = "select distinct([Subject Classification]) as SubClassification,[Subject Type] from " + companyName + " where Code='" + Code + "' order by SubClassification";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public SqlDataReader SHow_SubjectClassification1(string companyName, string Code, string course)
    {
        Connect();
        string s = "select distinct([Subject Classification]) as SubClassification,[Subject Type] from " + companyName + " where Code='" + Code + "' and Course='" + course + "'  order by SubClassification";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader SHow_EntryNoAuto(string tbleName)
    {
        Connect();
        //cmd = new SqlCommand("select top 1(CONVERT(int,[Entry No_])) as [Entry No] from " + tbleName + " order by [Entry No] desc ", Conn);
        string s = "select top 1(CONVERT(int,[Entry No_])) as [Entry No] from " + tbleName + " order by [Entry No] desc";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public void InsertTimeTableGeneration(string tbleName, string DayNo, string HourNo, string SubjectCode, string CourseCode, string SemesterCode, string FacultyCode, string AcademicYear, string SectionCode, string SubjectType, string FacultyName, string SubjectDescription, string GlobalDimension1Code, string GlobalDimension2Code, string TypeOfCourse, string Year, string TimeDuration, string RoomAllocation, string SubjectClassification, string AttendanceDate, string SubstituteFacultyCode, string SubstituteFacultyName, string UserID, string PortalID, string Group, string Batch, string RemedialPortalId)
    {
        Connect();

        string sqlq = "insert into " + tbleName + " ([Day No],[Hour No],[Subject Code],[Course Code],[Semester Code],[Faculty Code],[Academic Year],[Section Code],[Subject Type],[Faculty Name],[Subject Description],[Global Dimension 1 Code],[Global Dimension 2 Code],[Type Of Course],[Year],[Time Duration],[Room Allocation],[Subject Classification],[Attendance Date],[Substitute Faculty Code],[Substitute Faculty Name],[User ID],[Portal ID],[Group],[Batch],[Remedial Portal ID],[Combined ID],[Active Combined ID],[Detained]) values('" + DayNo + "','" + HourNo + "','" + SubjectCode + "','" + CourseCode + "','" + SemesterCode + "','" + FacultyCode + "','" + AcademicYear + "','" + SectionCode + "','" + SubjectType + "','" + FacultyName + "','" + SubjectDescription + "','" + GlobalDimension1Code + "','" + GlobalDimension2Code + "','" + TypeOfCourse + "','" + Year + "','" + TimeDuration + "','" + RoomAllocation + "','" + SubjectClassification + "','" + AttendanceDate + "','" + SubstituteFacultyCode + "','" + SubstituteFacultyName + "','" + UserID + "','" + PortalID + "','" + Group + "','" + Batch + "','" + RemedialPortalId + "',0,0,0)";

        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }
    public SqlDataReader SHow_RemedialStudentData(string companyName, string Course, string Semester, string Year, string Section, string SubjectCode)
    {
        Connect();
        string s = "";
        if (Section == "--Section--")
        {

            s = "select Remedial from " + companyName + " where Course='" + Course + "' and ([Semester]='" + Semester + "' or [Year]='" + Year + "') and [Subject Code]='" + SubjectCode + "' and Remedial='1'";
        }
        else
        {
            s = "select Remedial from " + companyName + " where Course='" + Course + "' and ([Semester]='" + Semester + "' or [Year]='" + Year + "') and Section='" + Section + "' and [Subject Code]='" + SubjectCode + "' and Remedial='1'";
        }
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader SHow_TimeTableGeneration(string companyName, string SubjectCode, string CourseCOde, string Semester,string facultyCode,string academicyrs,string year,string roomAllocation, string Sdate, string Tdate,string HFNo, string HTNo)
    {
        Connect();
        string s = "select * from " + companyName + " where [Subject Code]='" + SubjectCode + "' and [Course Code]='" + CourseCOde + "' and ([Semester Code] ='" + Semester + "' or Year='" + year + "') and [Faculty Code]='" + facultyCode + "' and [Academic Year]='" + academicyrs + "' and [Room Allocation]='" + roomAllocation + "' and [Hour No]>='" + HFNo + "' and [Hour No]<='" + HTNo + "'  and [Attendance Date]>='" + Sdate + "' and [Attendance Date]<='" + Tdate + "' ";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

  

    public void Delete_TimeTableGeneration(string companyName, string SubjectCode, string CourseCOde, string Semester, string facultyCode, string academicyrs, string year, string roomAllocation)
    {
        Connect();
        string sqlq = "delete from " + companyName + " where [Subject Code]='" + SubjectCode + "' and [Course Code]='" + CourseCOde + "' and ([Semester Code] ='" + Semester + "' or Year='" + year + "') and [Faculty Code]='" + facultyCode + "' and [Academic Year]='" + academicyrs + "' and [Room Allocation]='" + roomAllocation + "'  ";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }


    public SqlDataReader SHow_TimeTableGenerationHourValidation(string companyName, string fromhour,string tohour,string attendancedate,string RoomAllocation)
    {
        Connect();
        string s = "select * from " + companyName + " where  convert(date,[Attendance Date],103)='" + attendancedate + "' and [Room Allocation] ='" + RoomAllocation + "' and ([Hour No]='" + fromhour + "' or [Hour No]='" + tohour + "')";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader SHow_DeleteStudentAttendanceLine(string companyName, string CourseCode, string Semester, string year, string SubjectCode,string StudentNo,string academicyrs,string globaldimnsion,string fromdate,string todate)
    {
        Connect();
        string s = "select * from " + companyName + " where [Course Code]='" + CourseCode + "' and (Semester='" + Semester + "' or Year='" + year + "') and [Subject Code]='" + SubjectCode + "' and [Student No_]='" + StudentNo + "' and [Academic Year]='" + academicyrs + "' and [Global Dimension 1 Code]='" + globaldimnsion + "' and convert(date,[Date],103)>='" + fromdate + "' and convert(date,[Date],103)<='" + todate+ "'";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    //12/12/2016

    public SqlDataReader SHow_indentDepartment(string companyName)
    {
        Connect();
        //string s = "Declare @val Varchar(MAX); Select @val = COALESCE(@val + '| ' + [Field Filter], [Field Filter])        From [TMU$Table Filter] where [Table Name]='TMU03798' Select @val; ";
        string s = "select ([Code] + '  ' + Name) as Description, [Code] , Name  from " + companyName + " where Name!='' and [Dimension Code]='DEPARTMENT' order by Name ";
       cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader SHow_indentEmployee(string companyName)
    {
        Connect();
        string s = "select ([No_] + ' ' +[First Name] + ' ' + [Middle Name] + ' ' + [Last Name]) as Name, [No_] as Employeeid,[First Name] from " + companyName + " order by [First Name]";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public SqlDataReader SHow_indentEmployee(string companyName,string LoginId)
    {
        Connect(); //where No_='TMU00727' order by [First Name]
        //string s = "select ([No_] + ' ' +[First Name] + ' ' + [Middle Name] + ' ' + [Last Name]) as Name, [No_] as Employeeid,[First Name] from " + companyName + " order by [First Name]";
        string s = "select ([No_] + ' ' +[First Name] + ' ' + [Middle Name] + ' ' + [Last Name]) as Name, [No_] as Employeeid,[First Name] from " + companyName + " where No_='" + LoginId + "' order by [First Name]";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader SHow_indentItem(string companyName)
    {
        Connect();
        string s = "select ([No_] + ' ' + Description) as Name ,[No_] as Itemcode ,[Base Unit of Measure] as baseuniofmeasure from " + companyName + " where Description!='' and Blocked=0 order by Description";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader SHow_indentItemCode(string companyName, string Code)
    {
        Connect();
        string s = "select Description ,[No_],[Base Unit of Measure],[Gen_ Prod_ Posting Group] from " + companyName + " where [No_]='" + Code + "'";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public SqlDataReader SHow_OrderNoDocumentHeader(string companyName)
    {
        Connect();
        string s = "select top(1) ([No_]) as No,[Academic Year]  from " + companyName + " order by No desc";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader SHow_NoSeriesLine(string companyName)
    {
        Connect();
        string s = "select * from " + companyName + " where [Series Code]='ITMIND'";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public void Insert_IndentHeader(string tbleName, string No, string IssueFor, string Course, string Semister, string Section, string AcademicYear, string IssueDate, string UserId, string NoSeries, string ItemNo, string Description, string SameItem, string Status, string issuedall, string IssueId, string IssueName, string PostedIndent, string IssueAllItem, string EmployeeID, string ApprovalID, string Remarks)
    {
        Connect();

        string sqlq = "insert into " + tbleName + " (No_,[Issue For],Course,Semister,Section,[Academic Year],[Issue Date],[User Id],[No_ Series],[Item No],Description,[Same Item],Status,[Issued All],[Issue Id] ,[Issue Name],[Posted Indent],[Issue All Item],[Employee ID],[Approval ID],Remarks) values('" + No + "','" + IssueFor + "','" + Course + "','" + Semister + "','" + Section + "','" + AcademicYear + "','" + IssueDate + "','" + UserId + "','" + NoSeries + "','" + ItemNo + "','" + Description + "','" + SameItem + "','" + Status + "','" + issuedall + "','" + IssueId + "' ,'" + IssueName + "','" + PostedIndent + "','" + IssueAllItem + "','" + EmployeeID + "','" + ApprovalID + "','" + Remarks + "')";

        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }



    public void Insert_IndentLine(string tbleName, string DocumentNo, string No, string IndentFor, string ItemNo, string Name, string Description, string UnitPrice, string Quantity, string SerialNo, string LineAmount, string IssueDate, string IssueIndent, string Release, string Selects, string Cancel, string IndentStatus, string Location, string UnitofMeasure, string Types, string VarientCode, string Rem_Qty, string Userid, string Purpose, string PostingDate, string Gen_Prod_PostingGroup, string IssueQty, string Remarks, string ProductSubGroupCode, string Gen_BusPostingGroup, string PostedIndent, string IssuedQty)
    {
        Connect();

        string sqlq = "insert into " + tbleName + " ([Document No],[No_],[Indent For],[Item No],Name,Description,[Unit Price],Quantity,[Serial No],[Line Amount],[Issue Date],[Issue Indent],Release,[Select],Cancel,[Indent Status],Location,[Unit of Measure],Type,[Varient Code],Rem_Qty,[User id],Purpose,[Posting Date],[Gen_ Prod_ Posting Group],[Issue Qty],Remarks,[Product Sub Group Code],[Gen_Bus Posting Group],[Posted Indent],[Issued Qty],[Total Amount],[Chart of Accounts],[Update Chart of Accounts]) values('" + DocumentNo + "','" + No + "','" + IndentFor + "','" + ItemNo + "' ,'" + Name + "','" + Description + "','" + UnitPrice + "','" + Quantity + "', '" + SerialNo + "','" + LineAmount + "', '" + IssueDate + "','" + IssueIndent + "','" + Release + "','" + Selects + "','" + Cancel + "','" + IndentStatus + "','" + Location + "','" + UnitofMeasure + "','" + Types + "','" + VarientCode + "','" + Rem_Qty + "','" + Userid + "','" + Purpose + "', '" + PostingDate + "','" + Gen_Prod_PostingGroup + "','" + IssueQty + "','" + Remarks + "','" + ProductSubGroupCode + "','" + Gen_BusPostingGroup + "','" + PostedIndent + "','" + IssuedQty + "',0,0,0)";

        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }

    public SqlDataReader SHow_SumOfQuantity(string companyName, string itemno)
    {
        Connect();
        string s = "select SUM(Quantity) as Inventory from " + companyName + " where [Item No_]='" + itemno + "'";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader SHow_NoseriesLast(string companyName)
    {
        Connect();
        //string s = "select * from " + companyName + " where [Series Code]='ITMIND' and CONVERT(date,[Starting Date],103)>='" + startingfrom + "' and CONVERT(date,[Starting Date],103)<'" + startingto + "'";

        string s = "select Top(1)([Starting Date]),* from " + companyName + " where [Series Code]='ITMIND' order by CONVERT(date,[Starting Date],103) desc";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public SqlDataReader SHow_Top1lineno(string companyName)
    {
        Connect();
        string s = "select top 1([Line No_]) from " + companyName + " order by [Line No_] desc";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader SHow_IndentHeaderdataforduplicaet(string companyName, string No_)
    {
        Connect();
        string s = "select * from " + companyName + " where [No_]='" + No_ + "' ";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader SHow_IndentLinedataforduplicaet(string companyName, string DocumentNo)
    {
        Connect();
        string s = "select * from " + companyName + " where  [Document No]='" + DocumentNo + "' ";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader SHow_IndentLineDuplicatedata(string companyName, string DocumentNo, string no, string indentfor, string itemno)
    {
        Connect();
        string s = "select * from " + companyName + " where  [Document No]='" + DocumentNo + "' and [No_]='" + no + "' and  [Indent For]='" + indentfor + "' and [Item No]='" + itemno + "'";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public void UpdateINoSeriesAuto(string tbleName, string LastNo_Used, string fromdate, string todate)
    {
        Connect();
        string sqlq = "update " + tbleName + " set [Last No_ Used]='" + LastNo_Used + "' where [Series Code]='ITMIND' and CONVERT(date,[Starting Date],103)>='" + fromdate + "' and CONVERT(date,[Starting Date],103)<'" + todate + "' ";

        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }


    public void DeleteIndentline(string tbleName, string lineno)
    {
        Connect();

        string sqlq = "delete from " + tbleName + " where [Line No_]='" + lineno + "'";

        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }


    public SqlDataReader SHow_DocumentNowithLine(string companyName, string lineno)
    {
        Connect();
        string s = "select * from " + companyName + " where [Line No_]='" + lineno + "'";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader SHow_DocumentNowithLineofDocument(string companyName, string DocumentNo)
    {
        Connect();
        string s = "select * from " + companyName + " where [Document No]='" + DocumentNo + "'";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public void DeleteIndentHeader(string tbleName, string documentno)
    {
        Connect();

        string sqlq = "delete from " + tbleName + " where [No_]='" + documentno + "'";

        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }


    public void UpdateSendForApprovalHeader(string tbleName, string DocumentNo, string status)
    {
        Connect();

        string sqlq = "update " + tbleName + " set [Status]='" + status + "' where [No_]='" + DocumentNo + "' ";

        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }


    public void UpdateSendForApprovalLine(string tbleName, string DocumentNo, string status)
    {
        Connect();

        string sqlq = "update " + tbleName + " set [Indent Status]='" + status + "' where [Document No]='" + DocumentNo + "' ";

        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }

    public SqlDataReader SHow_DepartmentNamess(string companyName, string DepartmentCOde)
    {
        Connect();
        string s = "select * from " + companyName + " where Name!='' and [Dimension Code]='DEPARTMENT' and Code='" + DepartmentCOde + "'";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader SHow_IndentHeaderforApprovalbyhod(string companyName, string ApprovalID)
    {
        Connect();
        string s = "select No_ as DocumentNo ,[Issue Date] , [Issue For] = CASE WHEN [Issue For] =null THEN '' WHEN [Issue For] ='1' THEN 'Department' WHEN [Issue For] ='2' THEN 'Employee' ELSE '' END, [Status] = CASE WHEN [Status] =null THEN '' WHEN [Status] ='0' THEN 'Open' WHEN [Status] ='1' THEN 'Processed for Approval' WHEN [Status] ='2' THEN 'Approved' WHEN [Status] ='3' THEN 'Released' WHEN [Status] ='4' THEN 'Rejected' ELSE '' END  ,[Issue Id],[Issue Name] from " + companyName + " where [Approval ID]='" + ApprovalID + "' and Status=1";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader SHow_IndentHeaderforApprovalbyhodDateFilter(string companyName, string ApprovalID, string fromdate, string tilldate, string status)
    {
        Connect();
        string s = "select No_ as DocumentNo ,[Issue Date] , [Issue For] = CASE WHEN [Issue For] =null THEN '' WHEN [Issue For] ='1' THEN 'Department' WHEN [Issue For] ='2' THEN 'Employee' ELSE '' END, [Status] = CASE WHEN [Status] =null THEN '' WHEN [Status] ='0' THEN 'Open' WHEN [Status] ='1' THEN 'Processed for Approval' WHEN [Status] ='2' THEN 'Approved' WHEN [Status] ='3' THEN 'Released' WHEN [Status] ='4' THEN 'Rejected' ELSE '' END  ,[Issue Id],(select [Department Name] from [TMU$Employee] where [No_]=[TMU$Indent Header].[Employee ID]) as [Issue Name] from " + companyName + " where [Approval ID]='" + ApprovalID + "' and convert(date,[Issue Date],103)>='" + fromdate + "' and convert(date,[Issue Date],103)<='" + tilldate + "' and [Status]='" + status + "'";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader SHow_IndentHeaderforApprovalbyhodNOFilter(string companyName, string ApprovalID, string No_)
    {
        Connect();
        string s = "select No_ as DocumentNo ,[Issue Date] , [Issue For] = CASE WHEN [Issue For] =null THEN '' WHEN [Issue For] ='1' THEN 'Department' WHEN [Issue For] ='2' THEN 'Employee' ELSE '' END, [Status] = CASE WHEN [Status] =null THEN '' WHEN [Status] ='0' THEN 'Open' WHEN [Status] ='1' THEN 'Processed for Approval' WHEN [Status] ='2' THEN 'Approved' WHEN [Status] ='3' THEN 'Released' WHEN [Status] ='4' THEN 'Rejected' ELSE '' END  ,[Issue Id],[Issue Name] from " + companyName + " where [Approval ID]='" + ApprovalID + "' and [No_]='" + No_ + "'";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader SHow_IndentHeaderforApprovalbyUSerNOFilter(string companyName, string EmployeeID, string No_)
    {
        Connect();
        string s = "select No_ as DocumentNo ,[Issue Date] , [Issue For] = CASE WHEN [Issue For] =null THEN '' WHEN [Issue For] ='1' THEN 'Department' WHEN [Issue For] ='2' THEN 'Employee' ELSE '' END, [Status] = CASE WHEN [Status] =null THEN '' WHEN [Status] ='0' THEN 'Open' WHEN [Status] ='1' THEN 'Processed for Approval' WHEN [Status] ='2' THEN 'Approved' WHEN [Status] ='3' THEN 'Released' WHEN [Status] ='4' THEN 'Rejected' ELSE '' END  ,[Issue Id],[Issue Name] from " + companyName + " where [Employee ID]='" + EmployeeID + "' and [No_]='" + No_ + "'";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader SHow_IndentHeaderforApprovalbyUserDateFilter(string companyName, string EmployeeidID, string fromdate, string tilldate, string status)
    {
        Connect();
        string s = "select No_ as DocumentNo ,[Issue Date] , [Issue For] = CASE WHEN [Issue For] =null THEN '' WHEN [Issue For] ='1' THEN 'Department' WHEN [Issue For] ='2' THEN 'Employee' ELSE '' END, [Status] = CASE WHEN [Status] =null THEN '' WHEN [Status] ='0' THEN 'Open' WHEN [Status] ='1' THEN 'Processed for Approval' WHEN [Status] ='2' THEN 'Approved' WHEN [Status] ='3' THEN 'Released' WHEN [Status] ='4' THEN 'Rejected' ELSE '' END  ,[Issue Id],[Issue Name] from " + companyName + " where [Employee ID]='" + EmployeeidID + "' and convert(date,[Issue Date],103)>='" + fromdate + "' and convert(date,[Issue Date],103)<='" + tilldate + "' and [Status]='" + status + "'";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader SHow_IndentHeaderforApprovalbyUserid(string companyName, string Employeeid)
    {
        Connect();
        string s = "select No_ as DocumentNo ,[Issue Date] , [Issue For] = CASE WHEN [Issue For] =null THEN '' WHEN [Issue For] ='1' THEN 'Department' WHEN [Issue For] ='2' THEN 'Employee' ELSE '' END, [Status] = CASE WHEN [Status] =null THEN '' WHEN [Status] ='0' THEN 'Open' WHEN [Status] ='1' THEN 'Processed for Approval' WHEN [Status] ='2' THEN 'Approved' WHEN [Status] ='3' THEN 'Released' WHEN [Status] ='4' THEN 'Rejected' ELSE '' END  ,[Issue Id],[Issue Name] from " + companyName + " where [Employee ID]='" + Employeeid + "' and Status=0";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader SHow_CurrentAcademicyrs(string companyName, string startdate)
    {
        Connect();
        string s = "select * from  " + companyName + " where CONVERT(date,[Start Date],103)>='" + startdate + "' and CONVERT(date,[End Date],103)<='" + startdate + "' Union select * from " + companyName + " where CONVERT(date,[Start Date],103)>='" + startdate + "' and CONVERT(date,[End Date],103)<='" + startdate + "' union select * from " + companyName + " where '" + startdate + "'>=CONVERT(date,[Start Date],103) and '" + startdate + "'<=CONVERT(date,[End Date],103)";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public void Update_Indentheaderhoddata(string companyName, string Approvalid, string loginid)
    {
        Connect();
        string sqlq = "update " + companyName + " set [Approval ID]='" + Approvalid + "' where [Employee ID]='" + loginid + "' ";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }
    //---------------Ashu---------16-12-2016-----------------
    public String IndentApprovalID(string userid)
    {
        Connect(); String IndentApprovalID = "";
        string Query = "select top 1 [Indent Approval] from [TMU$Employee] with (NOLOCK) where [Indent Approval]='" + userid + "' ";
        cmd = new SqlCommand(Query, Conn);
        if (cmd.ExecuteScalar() != null)
        {
            IndentApprovalID = cmd.ExecuteScalar().ToString();
        }
        return IndentApprovalID;
    }
    public void UpdateItemQuantity(string tbleName, string Quantity,string LineNo_)
    {
        Connect();
        string sqlq = "update " + tbleName + " set [Quantity]='" + Quantity + "',[Rem_Qty]='" + Quantity + "',[Issue Qty]='" + Quantity + "' where [Line No_]=" + LineNo_ + "  ";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }
    
    
    // Sandeep==>15022017 this function use for check Role in User Role Matrix

    public SqlDataReader CheckRole(string UserId, String CollegeCode)
    {
        Connect();
        SqlCommand cmd = new SqlCommand("Sp_UserRole", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", UserId);
        cmd.Parameters.Add("@ID1", CollegeCode);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;


    }
    // Dhirendra 21-04-2017
    public SqlDataReader SHow_SalaryProcessingMonth_PayEmployeeNet(string companyName, string frommonth, string Fromyear,string employeecode,string tomonth,string toyear)
    {
        Connect();
        string s = "select * from  " + companyName + " where (MONTH ([Pay Date]) ='" + frommonth + "' and YEAR([Pay Date])='" + Fromyear + "' and [Employee No]='" + employeecode + "') or (MONTH ([Pay Date]) ='" + tomonth + "' and YEAR([Pay Date])='" + toyear + "' and [Employee No]='" + employeecode + "')";

        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public void Update_Co_AfterCancelled(string tbleName, string userid, string attendadet)
       {
        Connect();
        //string sqlq = "update " + tbleName + " set [Time in]='" + Timein + "',[Time Out]='" + TimeOut + "', [Hours Present]='" + hourpresnt + "',Status='1', [Attendance Marked]='1',[Applied Leave]='0',[CO]='1' , [CO Status]='Pending',[CO Remarks]='" + CoRemarks + "' where [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) ='" + attendadet + "'";
        string sqlq = "update " + tbleName + " set [CO]='0' , [CO Status]='',[CO Remarks]='' where [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) ='" + attendadet + "'";

        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }



    public void Delete_Co_LeavefromCretidedtable(string tbleName, string userid, string attendadet)
    {
        Connect();
        //string sqlq = "update " + tbleName + " set [Time in]='" + Timein + "',[Time Out]='" + TimeOut + "', [Hours Present]='" + hourpresnt + "',Status='1', [Attendance Marked]='1',[Applied Leave]='0',[CO]='1' , [CO Status]='Pending',[CO Remarks]='" + CoRemarks + "' where [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) ='" + attendadet + "'";
        string sqlq = "delete from " + tbleName + " where [Employee Code]='" + userid + "' and CONVERT(date,[Date],103)='" + attendadet + "' and [Leave Code]='CO'";

        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }

    // 22-04-2017
    public SqlDataReader Show_HolidaydateFORCL(string tbl_Name, string fromdate, string BranchCode, string weekdate)
    {
        Connect();

        string s = "select [Date] CountDate  from " + tbl_Name + " where convert(date, [Date],103)  ='" + fromdate + "' and [Branch Code]='" + BranchCode + "' and convert(date, [Date],103)!='" + weekdate + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    // 22 - 04 - 2017
    public SqlDataReader Show_SMonthlyWeekoffemployee(string tbl_Name, string fromdate, string todate, string ShiftPattern, string ShiftCode, string employeeno)
    {
        Connect();

        string s = "select count([Shift Code]) as ShiftCode  from " + tbl_Name + " where convert(date, [Start Date],103)  >='" + fromdate + "' and  convert(date, [Start Date],103)  <='" + todate + "' and [Employee No_]='" + employeeno + "' and [Shift Pattern]='" + ShiftPattern + "' and [Shift Code]='" + ShiftCode + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    // 22 - 04 - 2017
    public SqlDataReader Show_MonthlyWeekdayorholiday(string tbl_Name, string fromdate, string employeeno)
    {
        Connect();

        string s = "select *  from " + tbl_Name + " where convert(date, [Attendance Date],103)  >='" + fromdate + "' and [Employee No]='" + employeeno + "' and (Status='2' or Status='3')";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public String UserGroup(string userid,string CollegeCode)
    {
        Connect(); String UserGroup = "";
        string Query = "select [User Group] from [Portal Users]  where [Login ID]='" + userid + "' and [Global Dimension 1 Code]='" + CollegeCode + "' ";
        cmd = new SqlCommand(Query, Conn);
        if (cmd.ExecuteScalar() != null)
        {
            UserGroup = cmd.ExecuteScalar().ToString();
        }
        return UserGroup;
    }
    public string ValidateIndentHeaderNo_Exist_Or_Not(string companyName, string No_,string UserId )//
    {
        Connect(); String Result = "";
        string s = "select No_ from " + companyName + " where [No_]='" + No_ + "' and [Employee ID]='" + UserId + "' ";        
        cmd = new SqlCommand(s, Conn);
        if (cmd.ExecuteScalar() != null)
        {
            Result = cmd.ExecuteScalar().ToString();
        }
        return Result;
    }
    //Work from Home
    public void Update_WFH_Approval(string tbleName, string Timein, string TimeOut, string userid, string fdate, string tdate, string odRemarks)
    {
        Connect();
        string sqlq = "update " + tbleName + " set [Time in]='" + Timein + "',[Time Out]='" + TimeOut + "',Status='1',[Attendance Marked]='1',[Applied Leave]='0',OD='1',[Mark WFH]='1',[OD Remarks]='" + odRemarks + "' where [Employee Code]='" + userid + "' and CONVERT(date, [Attendance Date],103) >='" + fdate + "'  and CONVERT(date, [Attendance Date],103) <='" + tdate + "'";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();

    }

}