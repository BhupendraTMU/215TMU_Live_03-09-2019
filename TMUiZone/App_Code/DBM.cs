using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DBM
/// </summary>
/// 
public class DBM
{
		static string getConnectionString()
        {
            return ConfigurationManager.AppSettings["strPortal"].ToString();
        }

        public static SqlConnection getConnection()
        {
            SqlConnection ConnSql = null;
            try
            {
                ConnSql = new SqlConnection(getConnectionString());
                ConnSql.Open();
                return (ConnSql);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in connecting to server, Please Check your Internet Connection",ex);
            }
        }

        public static SqlCommand GetCommandSP(string SPName)
        {
            SqlCommand comd = null;
            try
            {
                comd = new SqlCommand(SPName);
                comd.CommandType = CommandType.StoredProcedure;
                return comd;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.Replace("'",""));
            }
        }

        public static SqlDataReader getReader(SqlCommand ConnCommand)
        {
            SqlConnection ConnSql = null;
            try
            {
                ConnSql = getConnection();
                ConnCommand.Connection = ConnSql;
                SqlDataReader ConnReader = ConnCommand.ExecuteReader(CommandBehavior.CloseConnection);
                return (ConnReader);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.Replace("'",""));
            }
            finally
            {
                ConnCommand.Dispose();
            }
        }
        public static SqlDataReader getReader(string query)
        {
            try
            {
                using (SqlCommand ConnCommand = new SqlCommand(query, getConnection()))
                {
                    return ConnCommand.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.Replace("'",""));
            }
        }

        public static void WriteToDb(SqlCommand ConnCommand)
        {
            SqlConnection ConnSql = getConnection();
            try
            {
                ConnCommand.Connection = ConnSql;
                ConnCommand.ExecuteNonQuery();
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message.Replace("'",""));
            }
            finally
            {
                ConnCommand.Dispose();
                ConnSql.Close();
                ConnSql.Dispose();
            }
        }
        public static void WriteToDb(string query)
        {
            try
            {
                using (SqlConnection ConnSql = getConnection())
                {
                    using (SqlCommand ConnCommand = new SqlCommand(query, ConnSql))
                    {
                        ConnCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message.Replace("'",""));
            }
        }
        public static void WriteToDbWithTransaction(string query)
        {
            try
            {
                using (SqlConnection ConnSql = getConnection())
                {
                    using (SqlTransaction ConnTransaction = ConnSql.BeginTransaction())
                    {
                        using (SqlCommand ConnCommand = new SqlCommand(query, ConnSql, ConnTransaction))
                        {
                            try
                            {
                                ConnCommand.CommandTimeout = 999999999;
                                ConnCommand.ExecuteNonQuery();
                                ConnTransaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                ConnTransaction.Rollback();
                                throw new Exception(ex.Message.Replace("'","").Replace("'",""));
                            }
                        }
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message.Replace("'","").Replace("'","").Replace("'",""));
            }
        }

        public static DataSet GetDataSet(SqlCommand ConnCommand)
        {
            SqlConnection ConnSql = null;
            try
            {
                ConnSql = getConnection();
                ConnCommand.Connection = ConnSql;
                SqlDataAdapter da = new SqlDataAdapter(ConnCommand);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.Replace("'",""));
            }
            finally
            {
                ConnCommand.Dispose();
                ConnSql.Close();
                ConnSql.Dispose();
            }
        }
        public static DataSet GetDataSet(string query)
        {
            try
            {
                using (SqlConnection ConnSql = getConnection())
                {
                    using (SqlCommand ConnCommand = new SqlCommand(query, ConnSql))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(ConnCommand))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                da.Fill(ds);
                                return ds;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.Replace("'", ""));
            }
        }
}