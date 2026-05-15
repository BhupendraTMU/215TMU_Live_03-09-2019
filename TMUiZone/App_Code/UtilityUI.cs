using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for UtilityUI
/// </summary>
public class UtilityUI
{
		public static void ShowAlert(Page p, string message)
        {
            ScriptManager.RegisterClientScriptBlock(p, p.GetType(), "key", string.Format("alert('{0}');", message), true);
        }
        #region DropDown
        public static void ddlFillByDataReader(DropDownList ddlObj, SqlDataReader dr, string DataField, string DataValue)
        {
            try
            {
                ddlObj.DataSource = dr;
                ddlObj.DataTextField = DataField;
                ddlObj.DataValueField = DataValue;
                ddlObj.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception("Error In Filling DropDown");
            }
            finally
            {
                if (dr != null)
                {
                    dr.Dispose();
                }
            }
        }
        public static void ddlFillByDataReaderWithAll(DropDownList ddlObj, SqlDataReader dr, string DataField, string DataValue)
        {
            try
            {
                ListItem li = new ListItem("All", "0");
                ddlObj.DataSource = dr;
                ddlObj.DataTextField = DataField;
                ddlObj.DataValueField = DataValue;
                ddlObj.DataBind();
                ddlObj.Items.Insert(0,li);

            }
            catch (Exception)
            {
                throw new Exception("Error In Filling DropDown");
            }
            finally
            {
                if (dr != null)
                {
                    dr.Dispose();
                }
            }
        }
        public static void ddlFillByDataTable(DropDownList ddlObj, DataTable dt, string DataField, string DataValue)
        {
            try
            {
                ddlObj.DataSource = dt;
                ddlObj.DataTextField = DataField;
                ddlObj.DataValueField = DataValue;
                ddlObj.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception("Error In Filling DropDown");
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        public static void ddlFillByDataTableWithAll(DropDownList ddlObj, DataTable dt, string DataField, string DataValue)
        {
            try
            {
                ListItem li = new ListItem("All", "0");
                ddlObj.DataSource = dt;
                ddlObj.DataTextField = DataField;
                ddlObj.DataValueField = DataValue;
                ddlObj.DataBind();
                ddlObj.Items.Insert(0, li);

            }
            catch (Exception)
            {
                throw new Exception("Error In Filling DropDown");
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        #endregion
        #region Gridview
        public static void FillGrid(GridView grd, DataTable dt)
        {
            try
            {
                grd.DataSource = dt;
                grd.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        public static void FillGrid(GridView grd, DataSet ds, string datakey, string sort)
        {
            DataView dv = null;
            try
            {
                dv = new DataView();
                dv = ds.Tables[0].DefaultView;
                dv.Sort = sort;
                grd.DataSource = dv;
                grd.DataKeyNames = new String[] { datakey };
                grd.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                dv.Dispose();
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        #endregion
        #region DataList
        public static void FillDatalist(DataList dl, DataTable dt)
        {
            DataView dv = null;
            try
            {
                dv = new DataView();
                dv = dt.DefaultView;
                dl.DataSource = dv;
                dl.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                dv.Dispose();
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        public static void FillDatalist(DataList dl, DataTable dt, string DataKey)
        {
            try
            {
                dl.DataSource = dt;
                dl.DataKeyField = DataKey;
                dl.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        public static void FillDatalist(DataList dl, DataSet ds)
        {
            DataView dv = null;
            try
            {
                dv = new DataView();
                dv = ds.Tables[0].DefaultView;
                dl.DataSource = dv;
                dl.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                dv.Dispose();
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        public static void FillDatalist(DataList dl, SqlDataReader rdr)
        {
            DataTable dt = null;
            try
            {
                dt = new DataTable();
                dt.Load(rdr);
                dl.DataSource = dt;
                dl.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (rdr != null)
                {
                    rdr.Dispose();
                    rdr = null;
                }
            }
        }    
        public static void FillDatalist(DataList dl, SqlDataReader rdr,string DataKey)
        {
            DataTable dt = null;
            try
            {
                dt = new DataTable();
                dt.Load(rdr);
                dl.DataSource = dt;
                dl.DataKeyField = DataKey;
                dl.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (rdr != null)
                {
                    rdr.Dispose();
                    rdr = null;
                }
            }
        }
        #endregion
        # region Validation
        public static bool IsValidDecimal(string value)
        {
            try
            {
                Convert.ToDecimal(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsValidDouble(string value)
        {
            try
            {
                Convert.ToDouble(value);
                return true;
            }
            catch
            {
                return false;   
            }
        }
        public static bool IsValidInt(string value)
        {
            try
            {
                Convert.ToInt32(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsValidInt64(string value)
        {
            try
            {
                Convert.ToInt64(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        public static void SendMail(string SenderId, string DisplayName, string MailToIds, string Subject, string Message, string Company, string SmtpFor)
        {
            try
            {
                string MailFrom = "", SMTPFromPortal = "", Pass = "";
                Int32 PortNo = 0;
                string query = "select * from tblMailSetup where CompanyName='" + Company + "' and SMTPFor='" + SmtpFor + "'";
                using (SqlDataReader rdr = DBM.getReader(query))
                {
                    if (rdr.HasRows)
                    {
                        rdr.Read();
                        MailFrom = rdr["from_Email"].ToString();
                        SMTPFromPortal = rdr["smtp"].ToString();
                        Pass = rdr["Password_From"].ToString();
                        PortNo = Convert.ToInt32(rdr["Port_No"]);
                    }
                }

                using(System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage())
                {
                    if (MailFrom != "" && MailToIds != "")
                    {
                        msg.From = new MailAddress(SenderId.Trim(), "  " + DisplayName);
                        foreach (string MailId in MailToIds.Split(','))
                        {
                            if (MailId != "")
                            {
                                msg.To.Add(MailId);
                            }
                        }
                        //msg.CC.Add(ccm);
                        if (msg.To.Count > 0)
                        {
                            msg.Subject = Subject;
                            msg.Body = Message;

                            SmtpClient smtp = new SmtpClient();
                            smtp.Port = PortNo;
                            smtp.Host = SMTPFromPortal;
                            smtp.EnableSsl = true;
                            NetworkCredential credential = new NetworkCredential(MailFrom, Pass);
                            smtp.UseDefaultCredentials = true;
                            smtp.Credentials = credential;

                            try
                            {
                                smtp.Send(msg);
                                msg.Dispose();

                            }
                            catch (Exception ex)
                            {
                                msg.Dispose();
                                throw new Exception(ex.Message);
                            }

                            //SmtpClient smtp = new SmtpClient();
                            //smtp.Port = PortNo;
                            //smtp.Host = SMTPFromPortal;
                            //smtp.EnableSsl = true;
                            //NetworkCredential credential = new NetworkCredential(MailFrom, Pass);
                            //smtp.UseDefaultCredentials = true;
                            //smtp.Credentials = credential;
                            //try
                            //{
                            //    smtp.Send(msg);
                            //}
                            //catch (Exception ex)
                            //{
                            //    throw new Exception(ex.Message);
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
}