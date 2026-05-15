
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

//using System.Linq;
//using System.Web;
//using System.Text;
//using System.Collections;
//using System.Data.OleDb;

namespace Utility
{
    public class DataUtilityTrn
    {
        SqlTransaction SqlTrn;
        SqlConnection con;
        SqlCommand cmd;
        string strIDList;
        protected internal DataUtilityTrn()
        {

        }
        # region Methods
        protected internal void OpenConnection()
        {
            if (this.con == null)
            {
                this.con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString);
            }
            if (con.State == ConnectionState.Closed)
            {
                this.con.Open();
            }
        }
        protected void CloseConnection()
        {
            if (this.con.State == ConnectionState.Open)
            {
                this.con.Close();
            }
        }
        public void DeleteRecords(decimal PKList, string UserName, string TableName, string PrimaryKeyColumn)
        {
            ///this.strIDList = "";      

            string strSql = "Update " + TableName + " set Active=0,MdDate=getdate(),MdBy='" + UserName + "' where " + PrimaryKeyColumn + " in(" + PKList + ")";
            ExecCommandText(strSql);
        }
        public void DeleteRecords(string PKList, string UserName, string TableName, string PrimaryKeyColumn)
        {
            ///this.strIDList = "";      

            string strSql = "Update " + TableName + " set Active=0,MdDate=getdate(),MdBy='" + UserName + "' where " + PrimaryKeyColumn + " in(" + PKList + ")";
            ExecCommandText(strSql);
        }
        
        private void CmdProcTrn(string ProcName)
        {
            if (this.cmd == null)
            {
                this.cmd = new SqlCommand();
                this.cmd.Connection = this.con;
                this.SqlTrn = con.BeginTransaction();
                this.cmd.Transaction = this.SqlTrn;
            }
            this.cmd.CommandType = CommandType.StoredProcedure;
            this.cmd.CommandText = ProcName;
        }
        private void CmdProc(string ProcName)
        {
            if (this.cmd == null)
            {
                this.cmd = new SqlCommand();
                this.cmd.Connection = this.con;
            }
            this.cmd.CommandType = CommandType.StoredProcedure;
            this.cmd.CommandText = ProcName;
        }
        public DataTable GetDataTableCmdParams(string ProcName, string[] ParamName, object[] ParamValue)
        {
            CmdProc(ProcName);
            for (int i = 0; i < ParamName.Length; i++)
            {
                this.cmd.Parameters.AddWithValue(ParamName[i], ParamValue[i]);
            }
            SqlDataAdapter da = new SqlDataAdapter(this.cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        private void CmdTextTrn(string strSql)
        {
            if (this.cmd == null)
            {
                this.cmd = new SqlCommand();
                this.cmd.Connection = this.con;
                this.SqlTrn = con.BeginTransaction();
                this.cmd.Transaction = this.SqlTrn;
            }
            this.cmd.CommandType = CommandType.Text;
            this.cmd.CommandText = strSql;
        }
        private void CmdText(string strSql)
        {
            if (this.cmd == null)
            {
                this.cmd = new SqlCommand();
                this.cmd.Connection = this.con;
            }
            this.cmd.CommandType = CommandType.Text;
            this.cmd.CommandText = strSql;
        }
        protected internal void ExecCommandProc(string ProcName)
        {
            CmdProc(ProcName);
            this.cmd.ExecuteNonQuery();
            this.cmd.Parameters.Clear();
        }
        protected internal string ExecScalarCmdProc(string ProcName)
        {
            string Result;
            CmdProc(ProcName);
            Result = this.cmd.ExecuteScalar().ToString();
            this.cmd.Parameters.Clear();
            return Result;
        }
        protected internal string ExecScalarCmdText(string strSql)
        {
            string Result;
            CmdText(strSql);
            Result = this.cmd.ExecuteScalar().ToString();
            return Result;
        }
        protected internal void ExecCommandProc(string ProcName, string ID)
        {
            CmdProc(ProcName);
            this.cmd.Parameters.AddWithValue("ID", ID);
            this.cmd.ExecuteNonQuery();
            this.cmd.Parameters.Clear();
        }
        protected internal void ExecCommandText(string strSql)
        {
            CmdText(strSql);
            this.cmd.ExecuteNonQuery();
            this.cmd.Parameters.Clear();
        }
       
        public DataTable GetDataTableProc(string ProcName)
        {
            DataTable dt = new DataTable();
            CmdProc(ProcName);
            SqlDataAdapter da = new SqlDataAdapter(this.cmd);
            da.Fill(dt);
            this.cmd.Parameters.Clear();
            return dt;
        }
        public DataTable GetDataTableProc(string ProcName, decimal ID)
        {
            DataTable dt = new DataTable();
            CmdProc(ProcName);
            this.cmd.Parameters.AddWithValue("ID", ID);
            SqlDataAdapter da = new SqlDataAdapter(this.cmd);
            da.Fill(dt);
            this.cmd.Parameters.Clear();
            return dt;
        }
        
        public DataTable GetDataTableProc(string ProcName, string ID)
        {
            DataTable dt = new DataTable();
            CmdProcTrn(ProcName);
            this.cmd.Parameters.AddWithValue("ID", ID);
            SqlDataAdapter da = new SqlDataAdapter(this.cmd);
            da.Fill(dt);
            this.cmd.Parameters.Clear();
            return dt;
        }
        
        public DataTable GetDataTableText(string strSql)
        {

            DataTable dt = new DataTable();
            CmdText(strSql);
            SqlDataAdapter da = new SqlDataAdapter(this.cmd);
            da.Fill(dt);
            this.cmd.Parameters.Clear();
            return dt;
        }
        public DataSet GetDataSetProc(string ProcName)
        {
            DataSet ds = new DataSet();
            CmdProc(ProcName);
            SqlDataAdapter da = new SqlDataAdapter(this.cmd);
            da.Fill(ds);
            this.cmd.Parameters.Clear();
            return ds;
        }
        public DataSet GetDataSetProc(string ProcName, decimal ID)
        {
            DataSet ds = new DataSet();
            CmdProc(ProcName);
            this.cmd.Parameters.AddWithValue("ID", ID);
            SqlDataAdapter da = new SqlDataAdapter(this.cmd);
            da.Fill(ds);
            this.cmd.Parameters.Clear();
            return ds;
        }
        protected internal void RollBackTrn()
        {
            this.SqlTrn.Rollback();
        }
        protected internal void CommitTransaction()
        {
            this.SqlTrn.Commit();
            this.SqlTrn.Dispose();
        }
        protected internal void DisposeCommand()
        {
            this.cmd.Dispose();
        }
     
       
        public string CheckExistingField(List<string> FieldList, string TableName, string UniqueColumnName)
        {
            this.strIDList = "";
            for (int i = 0; i < FieldList.Count; i++)
            {
                this.strIDList += (FieldList[i] + ",");
            }
            this.strIDList = this.strIDList.Substring(0, this.strIDList.Length - 1);
            string strSql = "Select " + UniqueColumnName + " from " + TableName + " where " + UniqueColumnName + " in(" + this.strIDList + ")";
            DataTable dt = GetDataTableText(strSql);
            if (dt.Rows.Count > 0)
            {
                this.strIDList = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.strIDList += (dt.Rows[i][0].ToString() + ",");
                }
                this.strIDList = this.strIDList.Substring(0, this.strIDList.Length - 1);
            }
            return strIDList;
        }
        public void DeleteCustReq(decimal id, string username)
        {
            OpenConnection();
            string qty = "exec ssp_DeleteCustEnq " + id + ", " + username + "";
            SqlCommand cmd = new SqlCommand(qty, con);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }
        //=============================ADD NEW===============
        public DataTable GetAge(String DOB, String TillDate)// Ex- 20 Feb 2015
        {
            string procName = "[Proc_GetAGeCalculation]";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, DOB, TillDate);
            return dt;
        }
        public String GetNextEnquiryNo()// Ex- 24 Feb 2015
        {
            string Query = "select [dbo].[NextEnquiryNumber]()";
            DataUtility objDut = new DataUtility();
             String GetAge = objDut.ExecScalarCmdText(Query);
            return GetAge;
        }
            

        protected internal string ExecScalarCmdParams(string ProcName, string[] ParamName, object[] ParamValue)
        {
            string Result;
            CmdProcTrn(ProcName);
            for (int i = 0; i < ParamName.Length; i++)
            {
                this.cmd.Parameters.AddWithValue(ParamName[i], ParamValue[i]);
            }
            Result = this.cmd.ExecuteScalar().ToString();
            this.cmd.Parameters.Clear();
            return Result;
        }
        #endregion Methods

       

        
    }
}
