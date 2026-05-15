using System;
using System.Collections.Generic;                                //Creation Date : 17-02-09
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
//using Microsoft.ApplicationBlocks.Data;
namespace Utility
{
    public class DataUtility
    {
        //string conString = ;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        DataTable dt;
        string strIDList;
        public DataUtility()
        {

        }
        # region PublicMethods
        public void OpenConnection()
        {
            if (this.con == null)
            {
                this.con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString);
            }
            try
            {
                if (this.con.State == ConnectionState.Closed)
                { this.con.Open(); }
            }
            catch
            {
                throw;
            }
        }
        public void CloseConnection()
        {
            if (this.con.State == ConnectionState.Open)
            {
                this.con.Close();
            }
        }      

        public DataTable GetDataTableProc1(string ProcName)
        {
            try
            {
                OpenConnection();
                this.dt = new DataTable();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = ProcName;
                //cmd.Parameters.AddWithValue("ID");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(this.dt);
            }
            catch
            {
                throw;
            }
            finally
            {
                //cmd.Dispose();
                CloseConnection();
            }
            return (this.dt);
        }

        public DataSet GetDataSetProc(string ProcName, string ID)
        {
            try
            {
                OpenConnection();
                this.ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = ProcName;
                cmd.Parameters.AddWithValue("ID", ID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(this.ds);
            }
            catch
            {
                throw;
            }
            finally
            {
                //cmd.Dispose();
                CloseConnection();
            }
            return (this.ds);
        }
        public DataSet GetDataSetProc(string ProcName, string ID,string ID1)
        {
            try
            {
                OpenConnection();
                this.ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = ProcName;
                cmd.Parameters.AddWithValue("ID", ID);
                cmd.Parameters.AddWithValue("ID1", ID1);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(this.ds);
            }
            catch
            {
                throw;
            }
            finally
            {
                //cmd.Dispose();
                CloseConnection();
            }
            return (this.ds);
        }
       
        public SqlCommand CmdProc(string ProcName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = ProcName;
            return cmd;
        }
        public SqlCommand CmdText(string strSql)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSql;
            return cmd;
        }
        public SqlDataAdapter DAProc(string ProcName)
        {
            SqlDataAdapter da = new SqlDataAdapter(CmdProc(ProcName));
            return da;
        }
        public SqlDataAdapter DAText(string strSql)
        {
            SqlDataAdapter da = new SqlDataAdapter(CmdText(strSql));
            return da;
        }

        public DataSet GetDataSetProc(string ProcName)
        {
            try
            {
                this.ds = new DataSet();
                OpenConnection();
                DAProc(ProcName).Fill(this.ds);
                //return this.ds;
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return (this.ds);
        }
        public DataSet GetDataSetCmdParams(string ProcName, string[] ParamName, object[] ParamValue)
        {
            OpenConnection();
            this.cmd = CmdProc(ProcName);
            for (int i = 0; i < ParamName.Length; i++)
            {
                this.cmd.Parameters.AddWithValue(ParamName[i], ParamValue[i]);
            }
            SqlDataAdapter da = new SqlDataAdapter(this.cmd);
            try
            {
                this.ds = new DataSet();
                da.Fill(this.ds);
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return this.ds;
        }         
        public DataSet GetDataSetText(string strSql)
        {
            try
            {
                this.ds = new DataSet();
                OpenConnection();
                DAText(strSql).Fill(this.ds);
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return this.ds;
        }
        public DataSet GetDataSetText(string strSql, string strSql1)
        {
            try
            {
                this.ds = new DataSet();
                OpenConnection();
                DAText(strSql).Fill(this.ds, strSql1);
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return this.ds;
        }
        
        public DataTable GetDataTableProc(string ProcName)
        {
            try
            {
                this.dt = new DataTable();
                OpenConnection();
                DAProc(ProcName).Fill(this.dt);
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return (this.dt);
        }
        public DataTable GetDataTableProc(string ProcName, string ID)
        {
            try
            {
                OpenConnection();
                this.dt = new DataTable();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = ProcName;                
                cmd.Parameters.AddWithValue("ID", ID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(this.dt);
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                //cmd.Dispose();
                CloseConnection();
            }
            return (this.dt);
        }
        public DataTable GetDataTableProc(string ProcName, string ID, string ID1)
        {
            try
            {
                OpenConnection();
                this.dt = new DataTable();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = ProcName;
                cmd.Parameters.AddWithValue("ID", ID);
                cmd.Parameters.AddWithValue("ID1", ID1);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(this.dt);
            }
            catch
            {
                throw;
            }
            finally
            {
                //cmd.Dispose();
                CloseConnection();
            }
            return (this.dt);
        }
        public DataTable GetDataTableProc(string ProcName, string ID, string ID1,string ID2)
        {
            try
            {
                OpenConnection();
                this.dt = new DataTable();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = ProcName;
                cmd.Parameters.AddWithValue("ID", ID);
                cmd.Parameters.AddWithValue("ID1", ID1);
                cmd.Parameters.AddWithValue("ID2", ID2);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(this.dt);
            }
            catch
            {
                throw;
            }
            finally
            {
                //cmd.Dispose();
                CloseConnection();
            }
            return (this.dt);
        }
        public DataTable GetDataTableProc(string ProcName, decimal ID)
        {
            try
            {
                OpenConnection();
                this.dt = new DataTable();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = ProcName;
                cmd.Parameters.AddWithValue("ID", ID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(this.dt);
            }
            catch
            {
                throw;
            }
            finally
            {
                //cmd.Dispose();
                CloseConnection();
            }
            return (this.dt);
        }
        public DataTable GetDataTableProc(string ProcName, decimal ID, string strID)
        {
            try
            {
                OpenConnection();
                this.dt = new DataTable();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = ProcName;
                cmd.Parameters.AddWithValue("ID", ID);
                cmd.Parameters.AddWithValue("strID", strID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(this.dt);
            }
            catch
            {
                throw;
            }
            finally
            {
                //cmd.Dispose();
                CloseConnection();
            }
            return (this.dt);
        }
        public DataTable GetDataTableCmdParams(string ProcName, string[] ParamName, object[] ParamValue)
        {
            OpenConnection();
            this.cmd = CmdProc(ProcName);
            for (int i = 0; i < ParamName.Length; i++)
            {
                this.cmd.Parameters.AddWithValue(ParamName[i], ParamValue[i]);
            }
            SqlDataAdapter da = new SqlDataAdapter(this.cmd);
            try
            {
                this.dt = new DataTable();
                da.Fill(this.dt);
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return this.dt;
        }
        public DataTable GetDataTableText(string strSql)
        {
            try
            {
                this.dt = new DataTable();
                OpenConnection();
                DAText(strSql).Fill(this.dt);
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return this.dt;
        }
        
        public void ExecCommandProc(string ProcName)
        {
            try
            {
                OpenConnection();
                SqlCommand cmd = CmdProc(ProcName);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                
                CloseConnection();
            }
        }
        public void ExecCommandProc(string ProcName, string ID)
        {
            try
            {
                OpenConnection();
                SqlCommand cmd = CmdProc(ProcName);
                cmd.Parameters.AddWithValue("ID", ID);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                //cmd.Dispose();
                CloseConnection();
            }
        }
        public void ExecCommandProc(string ProcName, string ID, string ID1)
        {
            try
            {
                OpenConnection();
                SqlCommand cmd = CmdProc(ProcName);
                cmd.Parameters.AddWithValue("ID", ID);
                cmd.Parameters.AddWithValue("ID1", ID1);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                //cmd.Dispose();
                CloseConnection();
            }
        }
        public void ExecCommandText(string strSql)
        {
            try
            {
                OpenConnection();
                SqlCommand cmd = CmdText(strSql);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                //cmd.Dispose();
                CloseConnection();
            }
        }
        public void ExecCmdParams(string ProcName, string[] ParamName, object[] ParamValue)
        {
            OpenConnection();
            this.cmd = CmdProc(ProcName);
            for (int i = 0; i < ParamName.Length; i++)
            {
                this.cmd.Parameters.AddWithValue(ParamName[i], ParamValue[i]);
            }
            try
            {
                this.cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
       
        public Boolean chkDuplication(string TableName, string colName, String Value)
        {
            string StrCommand = "";
            StrCommand = "SELECT * FROM " + TableName + " WHERE " + colName + "='" + Value + "' and Active=1";

            try
            {
                DataTable Dt = GetDataTableText(StrCommand);
                if (Dt.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public Boolean chkDuplication(string TableName, string colName, string Value, string ColName, string ChkValue)
        {
            string StrCommand = "";
            StrCommand = "SELECT * FROM " + TableName + " WHERE " + colName + "='" + Value + "' and " + ColName + "='" + ChkValue + "'and  Active=1";
            try
            {
                DataTable Dt = GetDataTableText(StrCommand);
                if (Dt.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch
            {

                return false;
            }
        }

        public DataTable Returntable(string str)
        {


            OpenConnection();

            this.dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.con;
            cmd.CommandText = str;
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            dt.AcceptChanges();
            CloseConnection();
            return dt;
        }

        public int checkField(string ProcName, decimal ID, string FieldName)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = new SqlConnection();
                cmd.Connection = this.con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = ProcName;
                cmd.Parameters.AddWithValue("ID", ID);
                cmd.Parameters.AddWithValue("FieldName", FieldName);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

            }
            catch
            {
                throw;
            }
            finally
            {
                //cmd.Dispose();
                CloseConnection();
            }
            return (dt.Rows.Count);
        }

        public string ExecScalarCmdText(string strSql)
        {
            OpenConnection();
            try
            {
                string Result;
                SqlCommand cmd = CmdText(strSql);
                Result = cmd.ExecuteScalar().ToString();
                return Result;
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
        public object ExecScalarCmdText(string ProcName, string ID)//Return String Value
        {
            OpenConnection();
            this.cmd = CmdProc(ProcName);
            this.cmd.Parameters.AddWithValue("ID", ID);            
            object Result=new object();
            Result = cmd.ExecuteScalar();
            this.cmd.Parameters.Clear();
            return Result;
        }
        public string ExecScalarCmdText(string ProcName, string[] ParamName, object[] ParamValue)//Return String Value
        {
            OpenConnection();            
            this.cmd = CmdProc(ProcName);
            for (int i = 0; i < ParamName.Length; i++)
            {
                this.cmd.Parameters.AddWithValue(ParamName[i], ParamValue[i]);
            }
              string Result;
               Result = cmd.ExecuteScalar().ToString();
               this.cmd.Parameters.Clear();
               return Result;
        }
        //--new
        protected internal string ExecScalarCmdParams(string ProcName, string[] ParamName, object[] ParamValue)
        {
            string Result;
            CmdProc(ProcName);
            for (int i = 0; i < ParamName.Length; i++)
            {
                this.cmd.Parameters.AddWithValue(ParamName[i], ParamValue[i]);
            }
            Result = this.cmd.ExecuteScalar().ToString();
            this.cmd.Parameters.Clear();
            return Result;
        }
        //new end




        // 
        public DataTable GetAge(String DOB, String TillDate )// Ex- 20 Feb 2015
        {
            string procName = "[Proc_GetAGeCalculation]";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName,DOB,TillDate);
            return dt;
        }     
       
       

        #endregion PublicMethods
        #region Delete Method
        public void DeleteRecords(decimal PKList, string UserName, string TableName, string PrimaryKeyColumn)
        {
            string strSql = "Update " + TableName + " set Active=0,MdDate=getdate(),MdBy='" + UserName + "' where " + PrimaryKeyColumn + " in(" + PKList + ")";
            ExecCommandText(strSql);
        }
        public void DeleteRecords(List<byte> PKList, string UserName, string TableName, string PrimaryKeyColumn)
        {
            this.strIDList = "";
            for (int i = 0; i < PKList.Count; i++)
            {
                this.strIDList += (PKList[i].ToString() + ",");
            }
            this.strIDList = this.strIDList.Substring(0, this.strIDList.Length - 1);
            string strSql = "Update " + TableName + " set Active=0,MdDate=getdate(),MdBy='" + UserName + "' where " + PrimaryKeyColumn + " in(" + this.strIDList + ")";
            ExecCommandText(strSql);
        }
        public void DeleteRecords(List<decimal> PKList, string UserName, string TableName, string PrimaryKeyColumn)
        {
            this.strIDList = "";
            for (int i = 0; i < PKList.Count; i++)
            {
                this.strIDList += (PKList[i].ToString() + ",");
            }
            this.strIDList = this.strIDList.Substring(0, this.strIDList.Length - 1);
            string strSql = "Update " + TableName + " set Active=0,MdDate=getdate(),MdBy='" + UserName + "' where " + PrimaryKeyColumn + " in(" + this.strIDList + ")";
            ExecCommandText(strSql);
        }
        public void DeleteRecords(List<int> PKList, string UserName, string TableName, string PrimaryKeyColumn)
        {
            this.strIDList = "";
            for (int i = 0; i < PKList.Count; i++)
            {
                this.strIDList += (PKList[i].ToString() + ",");
            }
            this.strIDList = this.strIDList.Substring(0, this.strIDList.Length - 1);
            string strSql = "Update " + TableName + " set Active=0,MdDate=getdate(),MdBy='" + UserName + "' where " + PrimaryKeyColumn + " in(" + this.strIDList + ")";
            ExecCommandText(strSql);
        }
        public void DeleteRecords(List<long> PKList, string UserName, string TableName, string PrimaryKeyColumn)
        {
            this.strIDList = "";
            for (int i = 0; i < PKList.Count; i++)
            {
                this.strIDList += (PKList[i].ToString() + ",");
            }
            this.strIDList = this.strIDList.Substring(0, this.strIDList.Length - 1);
            string strSql = "Update " + TableName + " set Active=0,MdDate=getdate(),MdBy='" + UserName + "' where " + PrimaryKeyColumn + " in(" + this.strIDList + ")";
            ExecCommandText(strSql);
        }

        public Boolean chkDuplication(string TableName, string colName, string Value, string ColName, string ChkValue, string PrimaryKeyCol, string ChkID)
        {
            string StrCommand = "";
            StrCommand = "SELECT * FROM " + TableName + " WHERE " + colName + "='" + Value + "' and " + ColName + "='" + ChkValue + "'and " + PrimaryKeyCol + "<>" + ChkID + " and  Active=1";
            try
            {
                DataTable Dt = GetDataTableText(StrCommand);
                if (Dt.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch
            {

                return false;
            }
        }
        public Boolean chkDuplication(string TableName, string colName1, string Value1, string ColName2, string Value2, string ColName3, string Value3, string ColName4, string Value4, string ColName5, string Value5)
        {
            string StrCommand = "";
            StrCommand = "SELECT * FROM " + TableName + " WHERE " + colName1 + "='" + Value1 + "' and " + ColName2 + "='" + Value2 + "'and " + ColName3 + "='" + Value3 + "'and " + ColName4 + "='" + Value4 + "'and " + ColName5 + "='" + Value5 + "'and  Active=1";
            try
            {
                DataTable Dt = GetDataTableText(StrCommand);
                if (Dt.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch
            {

                return false;
            }
        }




        public void DeleteRecords(List<short> PKList, string UserName, string TableName, string PrimaryKeyColumn)
        {
            this.strIDList = "";
            for (int i = 0; i < PKList.Count; i++)
            {
                this.strIDList += (PKList[i].ToString() + ",");
            }
            this.strIDList = this.strIDList.Substring(0, this.strIDList.Length - 1);
            string strSql = "Update " + TableName + " set Active=0,MdDate=getdate(),MdBy='" + UserName + "' where " + PrimaryKeyColumn + " in(" + this.strIDList + ")";
            ExecCommandText(strSql);
        }

        #endregion
       
        public Boolean chkDuplication(string TableName, string colName, string Value, string ColName, decimal ChkValue)
        {
            string StrCommand = "";
            StrCommand = "SELECT * FROM " + TableName + " WHERE " + colName + "='" + Value + "' and " + ColName + "='" + ChkValue + "'and  Active=1";
            try
            {
                DataTable Dt = GetDataTableText(StrCommand);
                if (Dt.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch
            {

                return false;
            }
        }

        
        public Boolean chkDuplication(string TableName, string colName, string Value, string ColName, string ChkValue, string ForeignKeyCol, decimal ForeginKeyID)
        {
            string StrCommand = "";
            StrCommand = "SELECT * FROM " + TableName + " WHERE " + colName + "='" + Value + "' and " + ColName + "='" + ChkValue + "'and " + ForeignKeyCol + "=" + ForeginKeyID + " and  Active=1";
            try
            {
                DataTable Dt = GetDataTableText(StrCommand);
                if (Dt.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch
            {

                return false;
            }
        }
    }
}