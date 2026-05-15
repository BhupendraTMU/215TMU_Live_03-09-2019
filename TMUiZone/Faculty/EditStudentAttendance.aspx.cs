using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using OfficeOpenXml;
using System.Web;

public partial class Faculty_EditStudentAttendance : System.Web.UI.Page
{
    static DataTable dt = new System.Data.DataTable();
    DL.StudentFineDL sdl = new DL.StudentFineDL();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    static string FacultyCode = ""; static string CollegeCode = ""; static int updatecount = 0;
    static int count = 0;
    private static Page page;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                if (Session["UserGroup"].ToString() == "PRINCIPAL" || Session["EventCo-Ordinator"] != "")
                {

                    try
                    {
                        FacultyCode = Session["uid"].ToString();
                        CollegeCode = Session["GlobalDimension1Code"].ToString();
                        DataTable dt = new DataTable();
                        grdAttendanceDetails.DataSource = dt;
                        grdAttendanceDetails.DataBind();
                        BinddlEventType();

                    }
                    catch
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }

            }
        }
        catch
        {
            Response.Redirect("~/Default.aspx");

        }

    }

    public void BinddlEventType()
    {
        SqlCommand cmd = new SqlCommand("Sp_getEventattedance", con); //ok
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlEventType.DataSource = dt;
        ddlEventType.DataTextField = "Details";
        ddlEventType.DataValueField = "Value";
        ddlEventType.DataBind();


    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchCustomers(string prefixText, int count)
  {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                //  cmd.CommandText = "select [Student Name],No_ from [TMU$Student - COLLEGE] where  Upper([Student Name]) like '" + prefixText.ToUpper() + "%'" + @"
                //                and [Course Code] in (select distinct([Course Code]) from [TMU$Course Wise Faculty] where [Global Dimension 1 Code]=
                //(select [Global Dimension 1 Code] from [TMU$Employee] where [No_]='" + FacultyCode + "'))"; 
                cmd.CommandText = "select [Student Name],[Enrollment No_] from [TMU$Student - COLLEGE] where  Upper([Student Name]) like '" + prefixText.ToUpper() + "%'" + @"
                    and [Course Code] in (select distinct([Course Code]) from [TMU$Course Wise Faculty] where [Global Dimension 1 Code]='" + CollegeCode + "') and [Student Status]=1";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["Student Name"].ToString() + " (" + sdr["Enrollment No_"].ToString() + ")");
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }
    public class Customer
    {
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string CourseCode { get; set; }
        public string Section { get; set; }
        public string Semester { get; set; }
        public string EnrollmentNo { get; set; }
        public string StudentNo { get; set; }
        public string Lecture { get; set; }
        public string remarks { get; set; }
        public string ddlEventType { get; set; }

        public string Admission_Date { get; set; }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<Customer> GetCustomers(string StudentName)
    {
        string StudentNo = "";
        if (StudentName.Contains("(") && StudentName.Contains(")"))
            StudentNo = StudentName.Split('(', ')')[1];
        if (StudentNo == "")
        {
            StudentNo = "ERROR";
        }
        string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("select top 1 convert (varchar(11),AC.[Admission Date],113) as [Admission Date] ,* from [TMU$Student - COLLEGE] SC inner join [TMU$Application - COLLEGE] AC on SC.[Application No_]=AC.No_  where [Enrollment No_]='" + StudentNo + "'", con))
            {
                cmd.Connection = con;
                List<Customer> customers = new List<Customer>();
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(new Customer
                        {
                            EnrollmentNo = sdr["Enrollment No_"].ToString(),
                            StudentNo = sdr["No_"].ToString(),
                            StudentName = sdr["Student Name"].ToString(),
                            FatherName = sdr["Fathers Name"].ToString(),
                            CourseCode = sdr["Course Code"].ToString(),
                            Section = sdr["Section"].ToString(),
                            Semester = sdr["Semester"].ToString(),
                            Admission_Date = sdr["Admission Date"].ToString(),
                        });
                    }
                }
                con.Close();
                return customers;
            }
        }
    }
    public static string presentstudentlist(DataTable dt1, string datefrom, string dateto, DataTable dtlecture)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString);
        string EnrollNo = "'" + dt1.Rows[0]["StudentNo"].ToString() + "'";
        string Returnmessage = "";
        for (int i = 1; i < dt1.Rows.Count; i++)
        {
            EnrollNo = EnrollNo + ",'" + dt1.Rows[i]["StudentNo"].ToString() + "'";
        }
        string LectureNo = "'" + dtlecture.Rows[0]["LectureNo"].ToString() + "'";
        for (int i = 1; i < dtlecture.Rows.Count; i++)
        {
            LectureNo = LectureNo + ",'" + dtlecture.Rows[i]["LectureNo"].ToString() + "'";
        }
        SqlCommand cmd = new System.Data.SqlClient.SqlCommand("select distinct [Student No_],Date  from [TMU$Student Attendance Line - COL] t1 where ([Attendance Type]=0) and (Date between '" + datefrom + "' and '" + dateto + "')  and [Hour] in (" + LectureNo + ") and [Student No_] in (" + EnrollNo + ")", con);


        SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        DataTable dtstudentNo = new DataTable();
        da.Fill(dtstudentNo);
        string PresentSt = "";
        if (dtstudentNo.Rows.Count > 0)
        {
            Returnmessage = "Students No : ";
            PresentSt = "" + dtstudentNo.Rows[0]["Student No_"].ToString() + ""; 
          
            
            for (int i = 1; i < dtstudentNo.Rows.Count; i++)
            {

                if (!PresentSt.Contains(dtstudentNo.Rows[i]["Student No_"].ToString()))
                {
                    PresentSt =PresentSt +","+ dtstudentNo.Rows[i]["Student No_"].ToString();
                    //lectureno = dtstudentNo.Rows[0]["Hour"].ToString();
                    //Returnmessage = Returnmessage + "Students No : " + PresentSt + " is present in lecture no : " + lectureno + "\n";
                }

            }
            con.Close();
            return Returnmessage + PresentSt + " is present. So you can not mark OD,Cultral events and Sports Events for the above Student.Please Correct your input and try again..!";
        }
        return "true";
       
            
        //if (dtstudentNo.Rows.Count > 0)
        //{
        //    Returnmessage = "Students No : ";
        //    string PresentSt = "" + dtstudentNo.Rows[0]["Student No_"].ToString() + "", lectureno = "" + dtstudentNo.Rows[0]["Hour"].ToString() + "";
        //    Returnmessage = Returnmessage + PresentSt + " is present in lecture no : " + lectureno + "\n";
        //    for (int i = 1; i < dtstudentNo.Rows.Count; i++)
        //    {

        //        if (!PresentSt.Contains(dtstudentNo.Rows[i]["Student No_"].ToString()))
        //        {
        //            PresentSt = dtstudentNo.Rows[i]["Student No_"].ToString();
        //            lectureno = dtstudentNo.Rows[0]["Hour"].ToString();
        //            Returnmessage = Returnmessage + "Students No : " + PresentSt + " is present in lecture no : " + lectureno + "\n";
        //        }

        //    }
        //}
        //= Returnmessage + PresentSt + " is present in lecture no : " + lectureno + " So you can not mark OD,Cultral events and Sports Events.Please Correct your input and try again..!";
    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string Save(List<string> StudentNolist, string chkPresent, string EventType, string remarks, string DateFrom, string DateTo, List<int> Lecture)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString);


        Boolean result = false; string qry = ""; int P = 0;

        DataTable dt = new DataTable(); dt.Clear(); dt.Columns.Add("StudentNo");
        DataTable dtLecture = new DataTable(); dtLecture.Clear(); dtLecture.Columns.Add("LectureNo");

        foreach (var StudentNo in StudentNolist)
        {
            if (!String.IsNullOrEmpty(StudentNo))
            {
                DataRow dr = dt.NewRow();
                dr[0] = StudentNo.Trim();
                dt.Rows.Add(dr);
            }
        }


        foreach (int LectureNo in Lecture)
        {
            DataRow dr = dtLecture.NewRow();
            dr[0] = LectureNo;
            dtLecture.Rows.Add(dr);
        }
        string studentlist = "";
        if (EventType == "OD" || EventType == "Cultural Events" || EventType == "Sports Events")
        {
            studentlist = presentstudentlist(dt, DateFrom, DateTo, dtLecture);

        }
        if (studentlist == "true" || studentlist=="")
        {
            if (FacultyCode != "")
            {
                try
                {
                    SqlCommand cmd = con1.CreateCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "Sp_UpdateAttendance";

                    cmd.Parameters.AddWithValue("@StudentDetails", dt);
                    if (chkPresent.ToUpper() == "NO")
                    {
                        cmd.Parameters.AddWithValue("@AttendanceType", 1);
                    }
                    else if (chkPresent.ToUpper() == "NC")
                    {
                        cmd.Parameters.AddWithValue("@AttendanceType", 4);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@AttendanceType", 0);
                    }
                    cmd.Parameters.AddWithValue("@EventType", EventType);
                    cmd.Parameters.AddWithValue("@Remarks", remarks);
                    cmd.Parameters.AddWithValue("@FromDate", DateFrom);
                    cmd.Parameters.AddWithValue("@ToDate", DateTo);
                    cmd.Parameters.AddWithValue("@Lecture", dtLecture);
                    cmd.Parameters.AddWithValue("@FacultyCode", FacultyCode);
                    con1.Open();
                    updatecount = cmd.ExecuteNonQuery();
                    con1.Close();
                    if (updatecount > 0)
                    {
                        return "true";
                    }
                    else
                    {
                        return "false";
                    }

                }
                catch (Exception ex)
                {
                    con1.Close();
                    return "false";

                }
            }
            return "true";
        }
        else
        {
            return studentlist;
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        ExcelUpload();
    }

    public void ExcelUpload()
    {
        if (fileUploadStudent.HasFile)
        {
            if (Path.GetExtension(fileUploadStudent.FileName) == ".xlsx" || Path.GetExtension(fileUploadStudent.FileName) == ".xls")
            {
                DataTable dtvs = new DataTable();
                ExcelPackage package = new ExcelPackage(fileUploadStudent.FileContent);
                dtvs = package.ToDataTable();

                if (dtvs.Rows.Count > 0)
                {
                    string EnrollNo = "'" + dtvs.Rows[0]["Enrollment No"].ToString() + "'";
                    for (int i = 1; i < dtvs.Rows.Count; i++)
                    {
                        EnrollNo = EnrollNo + ",'" + dtvs.Rows[i]["Enrollment No"].ToString() + "'";
                    }
                    SqlCommand cmd = new System.Data.SqlClient.SqlCommand("select SC.[Enrollment No_],SC.No_,SC.[Student Name],SC.[Fathers Name],SC.[Course Code],SC.Section,SC.Semester, convert (varchar(11),AC.[Admission Date],113) as [Admission Date] from [TMU$Student - COLLEGE]SC inner join [TMU$Application - COLLEGE] as AC on SC.[Application No_]=AC.No_  where SC.[Enrollment No_] in (" + EnrollNo + ")  and SC.[Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "'", con);
                    SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);

                    dt.Rows.Clear();
                    da.Fill(dt);
                    grdAttendanceDetails.DataSource = dt;
                    grdAttendanceDetails.DataBind();
                    btnSave.Enabled = true;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Please Check Excell Sheeet');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Upload Excell Sheeet Only !');", true);
                return;
            }

        }

    }

    protected void grdAttendanceDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            GridViewRow rowItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int index = rowItem.RowIndex;

            GridViewRow row = grdAttendanceDetails.Rows[index];
            string Enroll = row.Cells[0].Text;

            DataRow[] result = dt.Select("[Enrollment No_] = '" + Enroll + "'");
            foreach (DataRow row1 in result)
            {
                if (row1["Enrollment No_"].ToString().Trim().ToUpper().Contains(Enroll))
                    dt.Rows.Remove(row1);
            }
            grdAttendanceDetails.DataSource = dt;
            grdAttendanceDetails.DataBind();
            if (dt.Rows.Count <= 0)
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "fader", "$('[id$=pnlGrid]').hide(); ", true);
        }
    }




}