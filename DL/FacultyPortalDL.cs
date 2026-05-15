using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Utility;
using System.Configuration;
using System.Data.SqlClient;

namespace DL
{    
    public class FacultyPortalDL:DataUtility
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        public DataTable GetFacultyDetails(string No)
        {
            string procName = "proc_fatchFacultyDetailsView";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, No);
            return dt;
        }
        public DataTable GetSubjectTypeList()
        {
            string procName = "proc_GetSubjectType";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName);
            return dt;
        }
        public DataTable GetCourseList()
        {
            string procName = "proc_GetCourseDdl";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName);
            return dt;
        }
        public DataTable GetCourseListByCollege(string CollegeCode)
        {
            string procName = "proc_GetCourseDdlByCollege";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName);
            return dt;
        }
        public DataTable GetCourseList(String FacultuCode)
        {
            string procName = "proc_GetCourseFromCourseWiseFaculty";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName,FacultuCode);
            return dt;
        }
        public DataTable GetCourseList(String FacultuCode,string CollegeCode) //add by ashu
        {
            string procName = "proc_GetCourseFromCourseWiseFacultyCollege";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, FacultuCode, CollegeCode);
            return dt;
        }
        public DataTable GetCourseListFromTimeTable(String FacultuCode, string CollegeCode, string AttendanceDate)  // added on 24-02-017 by ashu
        {
            string procName = "proc_GetCourseFromTimeTableFacultyCollegeAttendanceDate";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, FacultuCode, CollegeCode,AttendanceDate);
            return dt;
        }
        
        public DataTable GetCourseListForArrangement(String AttDate, string HourNo,String FacultyCode) //add by ashu on 23 feb 2017 
        {
            string procName = "proc_GetCourseFromCourseWiseFacultyCollege";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, AttDate, HourNo, FacultyCode);            
            return dt;
        }
        public DataTable GetMentorshipDetails(String FacultyCode)//not in use
        {
            string procName = "proc_GetMentorshipDetails";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, FacultyCode);
            return dt;
        }
        public DataTable GetMentorshipDetails(String FacultyCode,String CourseCode)
        {
            string procName = "proc_GetMentorshipDetailsFacultyCourse";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, FacultyCode, CourseCode);
            return dt;
        }
        public DataTable GetSemesterList(string CourseCode)
        {
            string procName = "proc_GetSemesterByCourse";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName,CourseCode);
            return dt;
        }
        public DataTable GetSemesterList(string CourseCode,string FacultyCode)
        {
            string procName = "proc_GetSemesterFromCourseWiseFaculty";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, CourseCode, FacultyCode);
            return dt;
        }
        public DataTable GetSectionList()
        {
            string procName = "proc_GetSection";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName);
            return dt;
        }
        public DataTable GetSubjectList(string CourseCode,string Semester)
        {
            string procName = "proc_GetSubjectBySemester";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName,CourseCode,Semester);
            return dt;
        }
        public String GetNextStudentMarkAttendanceNumber()// Ex- 24 Feb 2015
        {
            string Query = "select [dbo].[NextStudentMarkAttendanceNumber]()";
            DataUtility objDut = new DataUtility();
            String GetAge = objDut.ExecScalarCmdText(Query);
            return GetAge;
        }
        //public DataTable GetUnitForMarkAttendance(string SubjectCode, string AcademicYear)//close --20-10-2016
        //{
        //    string procName = "proc_GetUnitForMarkAttendance";
        //    DataUtility objDut = new DataUtility();
        //    DataTable dt = objDut.GetDataTableProc(procName, SubjectCode, AcademicYear);
        //    return dt;
        //}
        public DataTable GetUnitForMarkAttendance(string CourseCode ,string SubjectCode, string AcademicYear)
        {
            string procName = "proc_GetUnitForMarkAttendance";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName,SubjectCode, AcademicYear,CourseCode);
            return dt;
        }
        public void InsertStudentAttendanceLine(DataTable dt,string CourseCode, string DocumentNo, string SubjectType, string Semester, string Section, string Date,string AcademicYear,
            string SubjectCode, string FacultyCode, int Hour, string CollegeCode,string Group,string Batch)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SqlCommand cmd = new SqlCommand("[dbo].[proc_insertStudentAttendanceLine]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EnrollmentNo", "ex");
                cmd.Parameters.AddWithValue("@DocumentNo", DocumentNo);
                cmd.Parameters.AddWithValue("@StudentName", dt.Rows[i]["Student Name"]);
                cmd.Parameters.AddWithValue("@StudentNo", dt.Rows[i]["Student No"]);
                cmd.Parameters.AddWithValue("@CourseCode", CourseCode);
                cmd.Parameters.AddWithValue("@Attendance", dt.Rows[i]["Today"]);
                cmd.Parameters.AddWithValue("@SubjectType", SubjectType);
                cmd.Parameters.AddWithValue("@Semester", Semester);
                cmd.Parameters.AddWithValue("@Section", Section);
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear);
                cmd.Parameters.AddWithValue("@SubjectCode", SubjectCode);
                cmd.Parameters.AddWithValue("@StaffCode", FacultyCode);
                cmd.Parameters.AddWithValue("@Hour", Hour);
                cmd.Parameters.AddWithValue("@CollegeCode", CollegeCode);
                cmd.Parameters.AddWithValue("@Group", Group);
                cmd.Parameters.AddWithValue("@Batch", Batch);
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }
        public void InsertStudentAttendanceHeader(string DocumentNo, string SubjectType,int AttendanceType,string FacultyCode,string Course, string Semester, string Section, string Date, string AcademicYear,
            string SubjectCode, int Hour, string Unit, string Topic, string SubjectDescription, string CollegeCode, string Group, string Batch)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
                SqlCommand cmd = new SqlCommand("[dbo].[proc_insertStudentAttendanceHeader]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DocumentNo", DocumentNo);
                cmd.Parameters.AddWithValue("@SubjectType", SubjectType);
                cmd.Parameters.AddWithValue("@Semester", Semester);
                cmd.Parameters.AddWithValue("@Section", Section);
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear);
                cmd.Parameters.AddWithValue("@Course", Course);
                cmd.Parameters.AddWithValue("@FacultyCode", FacultyCode);
                cmd.Parameters.AddWithValue("@AttendanceType", AttendanceType);
                cmd.Parameters.AddWithValue("@SubjectCode", SubjectCode);
                cmd.Parameters.AddWithValue("@Hour", Hour);
                cmd.Parameters.AddWithValue("@Unit", Unit);
                cmd.Parameters.AddWithValue("@Topic", Topic);
                cmd.Parameters.AddWithValue("@SubjectDescription", SubjectDescription);
                cmd.Parameters.AddWithValue("@CollegeCode", CollegeCode);
                cmd.Parameters.AddWithValue("@Group", Group);
                cmd.Parameters.AddWithValue("@Batch", Batch);
                cmd.ExecuteNonQuery();            
                con.Close();
        }
        public void updateSeriesLineLastNoUsed(string LastNo)
        {
            if (con.State == ConnectionState.Closed)
            con.Open();
            SqlCommand cmd = new SqlCommand("[dbo].[proc_updateSeriesLineLastNoUsed]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LastNo", LastNo);           
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public string GetSubjectTypebySemester(string CourseCode, string Semester,  string Description)
        {
            if (con.State == ConnectionState.Closed)
            con.Open();
            string SubjectType = "";
            SqlCommand cmd = new SqlCommand("[dbo].[proc_GetSubjectTypebySemester]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CourseCode", CourseCode);
            cmd.Parameters.AddWithValue("@Semester", Semester);
            cmd.Parameters.AddWithValue("@Description", Description);

            var s = cmd.ExecuteScalar();
            if (s != null)
                SubjectType = s.ToString();
            con.Close();
            return SubjectType;
        }
        public DataTable GetCourseFromTimeTable(string No)
        {
            string procName = "proc_GetCourseFromTimeTable";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, No);
            return dt;
        }
        public DataTable GetSemesterFromTimeTable(string No)
        {
            string procName = "proc_GetSemesterFromTimeTable";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, No);
            return dt;
        }
        public DataTable GetSectionFromTimeTable(string No)
        {
            string procName = "proc_GetSectionFromTimeTable";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, No);
            return dt;
        }
        public void insertAssignmentHeader(string FacultyCode,string CourseCode,string Semester,string Section,string AcademicYear,string SubjectType,
            string SubjectCode, string AssignmentDescriptionn, DateTime DueDate, decimal MaximumMarks, DateTime CloseDate, string SubjectDescription)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("[dbo].[proc_insertAssignmentHeader]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyCode", FacultyCode);
                cmd.Parameters.AddWithValue("@CourseCode", CourseCode);
                cmd.Parameters.AddWithValue("@Semester", Semester);
                cmd.Parameters.AddWithValue("@Section", Section);
                cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear);
                cmd.Parameters.AddWithValue("@SubjectType", SubjectType);
                cmd.Parameters.AddWithValue("@SubjectCode ", SubjectCode);
                cmd.Parameters.AddWithValue("@AssignmentDescriptionn", AssignmentDescriptionn);
                cmd.Parameters.AddWithValue("@DueDate", DueDate);
                cmd.Parameters.AddWithValue("@MaximumMarks", MaximumMarks);
                cmd.Parameters.AddWithValue("@CloseDate", CloseDate);
                cmd.Parameters.AddWithValue("@SubjectDescription", SubjectDescription);
                cmd.ExecuteNonQuery();            
                con.Close();
        }
        public void insertAssignmentLine(DataTable dt,string CourseCode, string Semester, string Section, string AcademicYear,string Path,string FacultyCode)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SqlCommand cmd = new SqlCommand("[dbo].[proc_insertAssignmentLine]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentNo", dt.Rows[i]["No_"]);
                cmd.Parameters.AddWithValue("@StudentName", dt.Rows[i]["Student Name"]);
                cmd.Parameters.AddWithValue("@Semester", Semester);
                cmd.Parameters.AddWithValue("@CourseCode", CourseCode);
                cmd.Parameters.AddWithValue("@Section", Section);
                cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear);
                cmd.Parameters.AddWithValue("@Path ", Path);
                cmd.Parameters.AddWithValue("@EmployeeCode ", FacultyCode);
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }
        public DataTable GetStudentDetails(string CourseCode, string Semester, string Section, string AcademicYear, string Group, string Batch, string SubjectCode, string SubjectType)  //sandeep on 14-01-2016 for group/batch
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            // SqlCommand cmd = new SqlCommand("proc_GetStudentDetailForAssignment", con);
            SqlCommand cmd = new SqlCommand("proc_GetStudentDetailForAssignmentWithGroupBatch", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CourseCode", CourseCode);
            cmd.Parameters.AddWithValue("@Semester", Semester);
            cmd.Parameters.AddWithValue("@Section", Section);
            cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear);
            cmd.Parameters.AddWithValue("@Group", Group);
            cmd.Parameters.AddWithValue("@Batch", Batch);
            cmd.Parameters.AddWithValue("@SubjectCode", SubjectCode);
            cmd.Parameters.AddWithValue("@SubjectType", SubjectType);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;

        }
        public DataTable GetStudentDetails(string CourseCode, string Semester, string Section, string AcademicYear)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("proc_GetStudentDetailForAssignment", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CourseCode",CourseCode);
            cmd.Parameters.AddWithValue("@Semester",Semester);
            cmd.Parameters.AddWithValue("@Section",Section);
            cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
            
        }

        //ashu  add below function on 26-08-2016
        public string InsertStudentAttendanceHeaderAndLine(string DocumentNo, string AcademicYear, string CourseCode, string Semester, string Section, string Group, string Batch, string SubjectCode, string SubjectType, int Hour, string Date, string FacultyCode, int AttendanceType, string Unit, string Topic, string CollegeCode, DataTable dt)
        {
            string Result = "";
            if (con.State == ConnectionState.Closed)
                con.Open();
            //try
            //{
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[proc_insertStudentAttendanceHeaderAndLine]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DocumentNo", DocumentNo);
                    cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear);
                    cmd.Parameters.AddWithValue("@Course", CourseCode);
                    cmd.Parameters.AddWithValue("@Semester", Semester);
                    cmd.Parameters.AddWithValue("@Section", Section);
                    cmd.Parameters.AddWithValue("@Group", Group);
                    cmd.Parameters.AddWithValue("@Batch", Batch);
                    cmd.Parameters.AddWithValue("@SubjectCode", SubjectCode);
                    cmd.Parameters.AddWithValue("@SubjectType", SubjectType);
                    cmd.Parameters.AddWithValue("@Hour", Hour);
                    cmd.Parameters.AddWithValue("@Date", Date);
                    cmd.Parameters.AddWithValue("@FacultyCode", FacultyCode);
                    cmd.Parameters.AddWithValue("@AttendanceType", AttendanceType);
                    cmd.Parameters.AddWithValue("@Unit", Unit);
                    cmd.Parameters.AddWithValue("@Topic", Topic);
                    cmd.Parameters.AddWithValue("@CollegeCode", CollegeCode);

                    cmd.Parameters.AddWithValue("@EnrollmentNo", "ex");
                    cmd.Parameters.AddWithValue("@StudentName", dt.Rows[i]["Student Name"]);
                    cmd.Parameters.AddWithValue("@StudentNo", dt.Rows[i]["Student No"]);
                    cmd.Parameters.AddWithValue("@Attendance", dt.Rows[i]["Today"]);// attendance absent/persent                
                    cmd.CommandTimeout = 300;
                    cmd.ExecuteNonQuery();
                }
            //}
            //catch { Result = "Error"; }
            //finally
            //{
            //    con.Close();                
            //}
            return Result;
        }
       
        
       
        public string GetSubjectTypebyCourseSubject(string CourseCode, string SubjectCode)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            string SubjectType = "";
            SqlCommand cmd = new SqlCommand("[dbo].[proc_GetSubjectTypebyCourseSubject]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CourseCode", CourseCode);
            cmd.Parameters.AddWithValue("@SubjectCode", SubjectCode);          

            var s = cmd.ExecuteScalar();
            if (s != null)
                SubjectType = s.ToString();
            con.Close();
            return SubjectType;
        }

        //Add InsertStudentAttendanceHeaderAndLine Method Only For InsertStudent By Using TABLE ==========12-04-2017 Strat
        public string InsertStudentAttendanceHeaderAndLine(string AcademicYear, string CourseCode, string Semester, string Section, string Group, string Batch, string SubjectCode, string SubjectType, int Hour, string Date, string FacultyCode, int AttendanceType, string Unit, string Topic, string CollegeCode, DataTable dt)
        {
            string Result = "";
            if (con.State == ConnectionState.Closed)
                con.Open();
            //try
            //{
                SqlCommand cmd = new SqlCommand("[dbo].[proc_insertStudentAttendanceHeaderAndLine_byTable]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear);
                cmd.Parameters.AddWithValue("@Course", CourseCode);
                cmd.Parameters.AddWithValue("@Semester", Semester);
                cmd.Parameters.AddWithValue("@Section", Section);
                cmd.Parameters.AddWithValue("@Group", Group);
                cmd.Parameters.AddWithValue("@Batch", Batch);
                cmd.Parameters.AddWithValue("@SubjectCode", SubjectCode);
                cmd.Parameters.AddWithValue("@SubjectType", SubjectType);
                cmd.Parameters.AddWithValue("@Hour", Hour);
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@FacultyCode", FacultyCode);
                cmd.Parameters.AddWithValue("@AttendanceType", AttendanceType);
                cmd.Parameters.AddWithValue("@Unit", Unit);
                cmd.Parameters.AddWithValue("@Topic", Topic);
                cmd.Parameters.AddWithValue("@CollegeCode", CollegeCode);
                cmd.Parameters.AddWithValue("@tblAttendance", dt);
                cmd.CommandTimeout = 300;
                cmd.ExecuteNonQuery();
            //}
            //catch { Result = "Error"; }
            //finally
            //{
            //    con.Close();
            //}
            return Result;
        }

        //Add InsertStudentAttendanceHeaderAndLine Method Only For InsertStudent By Using TABLE ==========12-04-2017 End
    }
}
