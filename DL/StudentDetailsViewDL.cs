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
    public class StudentDetailsViewDL : DataUtility
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        public DataTable GetStudentDetails(string No)
        {
            string procName = "proc_fatchStudentDetailsView1";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, No);
            return dt;
        }

        public DataTable GetStudentDetailsForSurl(string StudentNo)
        {

            string Query = "select [Student Name],No_,replace(convert(nvarchar,[Date of Birth],106),' ','-') as DOB,[Fathers Name]  from [TMU$Student - COLLEGE] where No_='" + StudentNo + "'";
            DataUtility objDut = new DataUtility();
            //  String ApplicationNo = objDut.ExecScalarCmdText(Query);
            DataTable dt = objDut.GetDataTableText(Query);
            return dt;
            //return ApplicationNo;
        }
        public int insertGen_JournalLine(string StudentNo, decimal Amount, string Description, string AccountNo, string TransactionID,string EntryNo,string JournalBatchName)
        {
            int result = 0;
            con.Open();
            SqlCommand cmd = new SqlCommand("proc_insertGen_JournalLineForFeeDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentNo", StudentNo);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@AccountNo", AccountNo);
            cmd.Parameters.AddWithValue("@Amount", Amount);
            cmd.Parameters.AddWithValue("@TransactionID", TransactionID);
            cmd.Parameters.AddWithValue("@EntryNo", EntryNo);
            cmd.Parameters.AddWithValue("@JournalBatchName", JournalBatchName);
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;

        }
    }
}
