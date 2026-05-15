using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Utility;


namespace DL
{
    public class StudentFineDL : DataUtility
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        public DataTable GetStudentDetails(string No,string CollegeCode)
        {
            string procName = "proc_fatchStudentDetailForFine";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, No, CollegeCode);
            return dt;
        }

        public DataTable GetStudentDetails(string No)
        {
            string procName = "proc_fatchStudentDetailForSecurityFine";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, No);
            return dt;
        }
        public DataTable GetStudentDetailsForAdmin(string No)  // 10 jan2018 by Ashu
        {
            string procName = "proc_fetchStudentDetailForFineAdmin";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, No);
            return dt;
        }
        public void insertStudentDeciplineLine(string studentNo, string course, string semester, string section,
                string AcademicYear, DateTime DateCommited, string ActionTaken, string StaffCode, decimal FineAmount, string Remarks)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("proc_insertStudentDeciplineLine", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@studentNo", studentNo);
            cmd.Parameters.AddWithValue("@course", course);
            cmd.Parameters.AddWithValue("@semester", semester);
            cmd.Parameters.AddWithValue("@section", section);
            cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear);
            cmd.Parameters.AddWithValue("@DateCommited", DateCommited);
            cmd.Parameters.AddWithValue("@ActionTaken", ActionTaken);
            cmd.Parameters.AddWithValue("@StaffCode", StaffCode);
            cmd.Parameters.AddWithValue("@FineAmount", FineAmount);
            cmd.Parameters.AddWithValue("@RemedialMeasures", Remarks);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public DataTable GetStudentFineDetails(string No)
        {
            string procName = "proc_getStudentFineDetails";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, No);
            return dt;
        }

        public void insertFineGL(string StudentNo, decimal Amount, string Section, string AcademicYear, string Course, string Semester,string CollegeCode)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("proc_InsertGenJournalLineFromStudentFine", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentNo", StudentNo);
            cmd.Parameters.AddWithValue("@Amount", Amount);
            cmd.Parameters.AddWithValue("@Section", Section);
            cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear);
            cmd.Parameters.AddWithValue("@Course", Course);
            cmd.Parameters.AddWithValue("@Semester", Semester);
            cmd.Parameters.AddWithValue("@CollegeCode", CollegeCode);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
