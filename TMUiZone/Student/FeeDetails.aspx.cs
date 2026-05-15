using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DL;
using PL;
using System.IO;
using System.Text;
using paytm;
using System.Security.Cryptography;
using System.Web.Mail;
using paytm;
using WebReference;
using System.Net;
using DotNetIntegrationKit;

public partial class Student_FeeDetails : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    decimal Fees = 0;
    public string action1 = string.Empty;
    public string hash1 = string.Empty;
    public string txnid1 = string.Empty;
    string TransId = "";
    string FeeType = "REGISTRATION";
    string EmailID = "";
    string MobileNo = "";
    DataTable dt = new DataTable();
    string AccountNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Local Process
            //Response.Redirect("~/Student/Error.aspx", false);
            //HttpContext.Current.ApplicationInstance.CompleteRequest();
            //Live
            if (Session["uid"].ToString() != "ST/046560" )
            {
                Response.Redirect("Error.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }

            key.Value = ConfigurationManager.AppSettings["MERCHANT_KEY"];
            // lblName.Text = Session["Name"].ToString() + " (" + Session["AcademicYear"].ToString() + ")";
            lblPaidFee.Text = "Paid Fee (" + Session["AcademicYear"].ToString() + " )";
            lblUnpaidFee.Text = "Unpaid  Fee";
            if (!IsPostBack)
            {
                string result;
                DataTable dtNAV = new DataTable();
                SqlCommand cmdNAV = new SqlCommand("Proc_GetNAVCreditionalLive", con);
                cmdNAV.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter daNAV = new SqlDataAdapter(cmdNAV);
                daNAV.Fill(dtNAV);
                VoucherPosting nvp = new VoucherPosting();
                nvp.UseDefaultCredentials = true;
                nvp.Url = dtNAV.Rows[0]["URL"].ToString();
                nvp.Credentials = new NetworkCredential(dtNAV.Rows[0]["UserID"].ToString(), dtNAV.Rows[0]["Password"].ToString());
                result = nvp.UpdateStudentFineAmount(Session["uid"].ToString(), Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")));
                bindGrid();
                BindPaidGrid();
            }
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }
    }
    protected void grdFeedbackReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdFeeDetails.PageIndex = e.NewPageIndex;
        bindGrid();
    }
    public void bindGrid()
    {
        if (Session["uid"] != null)
        {
            //grdFeeDetails.DataSource = "";
            grdFeeDetails.DataBind();
            con.Open();
            //SqlCommand cmd = new SqlCommand("select [Entry No_], Description,[Sales (LCY)] as Amount from [TMU$Cust_ Ledger Entry] where [Open]=1 and [Customer No_]='" + Session["uid"].ToString() + "'", con);//ashu 29-07-2016
            // SqlCommand cmd = new SqlCommand("select [Entry No_], Description,[Sales (LCY)] as Amount from [TMU$Cust_ Ledger Entry] where [Open]=1 and [Customer No_]='" + Session["uid"].ToString() + "' and [Paid_Unpaid Online Fee]=0", con);//comment on 04-04-2017

            //---------------------------Ashu-----------------04-04-2017-----------------------
            SqlCommand cmd = new SqlCommand("Sp_StudentRemainingFee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StudentNo_", Session["uid"].ToString());
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //--------------------------Ashu------------------04-04-2017-----------------------
            // SqlDataAdapter da = new SqlDataAdapter(cmd);     //DataTable dt = new DataTable();
            con.Close();
            da.Fill(dt);
            grdFeeDetails.DataSource = dt;
            grdFeeDetails.DataBind();
            if (dt.Rows.Count > 0)
            {
                decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("RemainingAmount"));
                grdFeeDetails.FooterRow.Cells[1].Text = "Total";
                grdFeeDetails.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                Label tb = new Label();
                tb.ID = "tb1";
                grdFeeDetails.FooterRow.Cells[2].Controls.Add(tb);
                grdFeeDetails.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                tb.Text = "0";
                tb.CssClass = "txtTotal";
                //tb.Width = 100;
                Fees = Convert.ToDecimal(tb.Text);
            }
            else
                btnPay.Visible = false;
        }
        else
        {
            Response.Redirect("../Default.aspx");
        }
    }
    public void bindStudentDetails()
    {
        SqlCommand cmd = new SqlCommand("select [E-Mail Address],[Mobile Number] from [TMU$Student - COLLEGE] where No_='" + Session["uid"].ToString() + "'", con);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        EmailID = dt.Rows[0]["E-Mail Address"].ToString();
        MobileNo = dt.Rows[0]["Mobile Number"].ToString();
    }
    protected void btnPay_Click(object sender, EventArgs e)
    {
        String response = "";
        String strResponse = "";
        string orderid = "TMUFEE" + DateTime.Now.Ticks.ToString();
        int temp = 0;
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO [OnlinePaymentLog]([Payment Date],[Amount],[Status],[UserID],[GATEWAY],[GatewayStatus],[OrderID],[CLE Entry No],[Semester FEE],[Year FEE],[Temp 1],[Temp 2],[Temp 3],[Orgnized Date])VALUES(GETUTCDATE(),1 ,0 ,'" + Session["uid"].ToString() + "' ,'PAYTM'  ,0 ,'" + orderid + "','',(select Semester from [TMU$Student - COLLEGE] where [No_]='" + Session["uid"].ToString() + "'),(select Year from [TMU$Student - COLLEGE] where [No_]='" + Session["uid"].ToString() + "'),'',0,(select [Student Name] from [TMU$Student - COLLEGE] where [No_]='" + Session["uid"].ToString() + "'), DATEADD(Minute,330,Getdate()))", con);
            temp = cmd.ExecuteNonQuery();
            con.Close();
            if (temp == 1)
            {
                RequestURL objRequestURL = new RequestURL();
                response = objRequestURL.SendRequest
                          (
                                    "T"
                                  , "T955031"
                                  , orderid
                                  , Session["Name"].ToString()
                                  , "1"
                                  , "INR"
                                  , orderid
                                  , "https://portal2.tmu.ac.in/Student/ResponsePage.aspx"
                                  , ""
                                  , ""
                                  , "FIRST_1.00_0.0"
                                  , "29-05-2024"
                                   , EmailID
                                  , MobileNo
                                  , "470"
                                  , ""
                                  , ""
                                  , ""
                                  , "1025180466GPADTD"
                                  , "3232693456UWOWHC"

                          );
                strResponse = response.ToUpper();
                bool IsValid = false;

                if (strResponse.StartsWith("ERROR"))
                {
                    if (strResponse == "ERROR073")
                    {
                        IsValid = false;
                        // lblError.Text = null;
                        response = objRequestURL.SendRequest
                           (
                          "T"
                                  , "T955031"
                                  , orderid
                                  , Session["Name"].ToString()
                                  , "1"
                                  , "INR"
                                  , orderid
                                  , "https://portal2.tmu.ac.in/Student/ResponsePage.aspx"
                                  , ""
                                  , ""
                                  , "FIRST_1.00_0.0"
                                  , "29-05-2024"
                                   , EmailID
                                  , MobileNo
                                  , "470"
                                  , ""
                                  , ""
                                  , ""
                                  , "1025180466GPADTD"
                                  , "3232693456UWOWHC"

                          );
                        strResponse = response.ToUpper();
                    }
                    else
                    {
                        // lblResponse.Text = response;
                    }
                }
                else
                {
                    IsValid = true;
                }


                if (IsValid)
                {
                    Session["Merchant_Code"] = "T3348";
                    Session["IsKey"] = "1025180466GPADTD";
                    Session["IsIv"] = "3232693456UWOWHC";

                    Response.Write("<form name='s1_2' id='s1_2' action='" + response + "' method='post'> ");
                    Response.Write("<script type='text/javascript' language='javascript' >document.getElementById('s1_2').submit();");
                    Response.Write("</script>");
                    Response.Write("<script language='javascript' >");
                    Response.Write("</script>");
                    Response.Write("</form> ");
                }

                else
                {
                    if (response == "")
                    {
                        //lblResponse.Text = "Transaction Fail " + "ERROR:";
                    }
                    else
                    {
                        // lblResponse.Text = response;
                    }
                }
            }
        }
        catch (Exception ex)
        {

            throw;
        }


        //BindTable();
        //bindAccountNo();
        //PayUmoney();
        //Amt.Value = "0";
    }
    public void BindTable()
    {
        for (int i = 0; i < grdFeeDetails.Columns.Count; i++)
        {
            dt.Columns.Add(grdFeeDetails.HeaderRow.Cells[i].Text);

        }
        foreach (GridViewRow row in grdFeeDetails.Rows)
        {
            var cb = (CheckBox)row.Cells[0].FindControl("chkboxSelectAmount");
            DataRow dr = dt.NewRow();
            for (int j = 0; j < grdFeeDetails.Columns.Count; j++)
            {
                if (cb.Checked == true)
                    dr[grdFeeDetails.HeaderRow.Cells[j].Text] = row.Cells[j].Text;
            }
            if (cb.Checked == true)
                dt.Rows.Add(dr);
        }
    }
    public void bindAccountNo()
    {
        foreach (DataRow row in dt.Rows)
        {
            AccountNo = AccountNo + "'" + row.Field<string>("Description") + "',";
        }
        if (AccountNo.Length > 1)
            AccountNo = AccountNo.Substring(0, AccountNo.Length - 1);
        SqlCommand cmd = new SqlCommand("select [G_L Account] from [TMU$Fee Components] where Description in (" + AccountNo + ")", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da.Fill(dt1);
        Session["AccountNo"] = dt1;
        Session["AmountDetails"] = dt;
    }
    public void BindPaidGrid()
    {
        con.Open();
        //        SqlCommand cmd = new SqlCommand("select c.[Entry No_],c.Description,ABS(d.Amount) as Amount from [TMU$Detailed Cust_ Ledg_ Entry] d join [TMU$Cust_ Ledger Entry] c on c.[Entry No_]=d.[Cust_ Ledger Entry No_]"+@"
        //            where c.[Customer No_]='" + Session["uid"].ToString() + "' and c.[Document Type]='1'and d.[Entry Type]=1", con);//ashu 29-07-2016
        //  SqlCommand cmd = new SqlCommand("select [Entry No_], Description,[Sales (LCY)] as Amount from [TMU$Cust_ Ledger Entry] where ([Open]=0 or [Paid_Unpaid Online Fee]=1) and [Customer No_]='" + Session["uid"].ToString() + "'", con); //Comment Ashu on 04-04-2017
        //  SqlCommand cmd = new SqlCommand("select CLE.[Entry No_], Description,DCLE.[Credit Amount] as Amount from [TMU$Cust_ Ledger Entry] CLE inner join [TMU$Detailed Cust_ Ledg_ Entry] DCLE on DCLE.[Cust_ Ledger Entry No_]=CLE.[Entry No_] where DCLE.[Credit Amount]<>0 and CLE.[Customer No_]='" + Session["uid"].ToString() + "'", con);
        //  SqlDataAdapter da = new SqlDataAdapter(cmd);
        //  DataTable dt = new DataTable();
        //---------------------------Ashu-----------------04-04-2017-----------------------
        SqlCommand cmd = new SqlCommand("Sp_StudentPaidFee", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@StudentNo_", Session["uid"].ToString());
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //--------------------------Ashu------------------04-04-2017-----------------------
        con.Close();
        da.Fill(dt);
        grdPaidFeesDetails.DataSource = dt;
        grdPaidFeesDetails.DataBind();
        if (dt.Rows.Count > 0)
        {
            decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
            grdPaidFeesDetails.FooterRow.Cells[0].Text = "Total";
            grdPaidFeesDetails.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            grdPaidFeesDetails.FooterRow.Cells[1].Text = total.ToString("N1");
        }
    }
    protected void grdPaidFeesDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    public void PayUmoney()
    {
        bindStudentDetails();
        Random rnd1 = new Random();
        TransId = rnd1.Next(0, 999999).ToString();
        String sDate = DateTime.Now.ToString();
        DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
        String dy = datevalue.Day.ToString();
        String mn = datevalue.Month.ToString();
        String yy = datevalue.Year.ToString();
        string date1 = mn + "/" + dy + "/" + yy;
        string time1 = System.DateTime.Now.TimeOfDay.ToString();

        // end save record in database
        try
        {
            string[] hashVarsSeq; string hash_string = string.Empty;
            if (string.IsNullOrEmpty(Request.Form["txnid"])) // generating txnid
            {
                Random rnd = new Random();
                string strHash = Generatehash512(rnd.ToString() + DateTime.Now);
                txnid1 = TransId;
            }
            else
            {
                txnid1 = TransId;
            }
            if (string.IsNullOrEmpty(Request.Form["hash"])) // generating hash value
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["MERCHANT_KEY"]))
                {
                    lblMsg.Text = "Error on Page";
                    return;
                }
                else
                {
                    lblMsg.Text = "";
                    hashVarsSeq = ConfigurationManager.AppSettings["hashSequence"].Split('|'); // spliting hash sequence from config
                    hash_string = "";
                    foreach (string hash_var in hashVarsSeq)
                    {
                        if (hash_var == "key")
                        {
                            hash_string = hash_string + ConfigurationManager.AppSettings["MERCHANT_KEY"];
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "txnid")
                        {
                            hash_string = hash_string + txnid1;
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "amount")
                        {
                            Fees = Convert.ToDecimal(Amt.Value);
                            //Fees = 1;
                            hash_string = hash_string + Convert.ToDecimal(Fees).ToString("g29");
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "productinfo")  //new code
                        { //  hash_string = hash_string + Request.Form[hash_var];
                            hash_string += "{\"paymentIdentifiers\":" + GetJson().ToString() + "}";
                            hash_string = hash_string + '|';
                        }

                        else
                        {
                            if (hash_var == "firstname")
                            {
                                //hash_string = hash_string + (Request.Form[hash_var] != null ? Request.Form[hash_var] : txtApplicantName.Text);// isset if else
                                hash_string = hash_string + Session["Name"].ToString();
                                hash_string = hash_string + '|';
                            }
                            else if (hash_var == "email")
                            {
                                //hash_string = hash_string + (Request.Form[hash_var] != null ? Request.Form[hash_var] : txtEmailAddress.Text);// isset if else
                                hash_string = hash_string + EmailID;
                                hash_string = hash_string + '|';
                            }
                            else
                            {
                                hash_string = hash_string + (Request.Form[hash_var] != null ? Request.Form[hash_var] : "");// isset if else
                                hash_string = hash_string + '|';
                            }

                        }
                    }
                    hash_string += ConfigurationManager.AppSettings["SALT"];
                    hash1 = Generatehash512(hash_string).ToLower();
                    action1 = ConfigurationManager.AppSettings["PAYU_BASE_URL"] + "/_payment";
                }
            }
            else if (!string.IsNullOrEmpty(Request.Form["hash"]))
            {
                hash1 = Request.Form["hash"];
                action1 = ConfigurationManager.AppSettings["PAYU_BASE_URL"] + "/_payment";
            }
            if (!string.IsNullOrEmpty(hash1))
            {
                hash.Value = hash1; txnid.Value = txnid1;
                System.Collections.Hashtable data = new System.Collections.Hashtable(); // adding values in gash table for data post
                data.Add("hash", hash.Value);
                data.Add("txnid", txnid.Value);
                data.Add("key", key.Value);
                Session["Amount1"] = Fees;
                //string AmountForm = Convert.ToDecimal(Amt.Value).ToString("g29");// eliminating trailing zeros
                data.Add("amount", 1);
                data.Add("firstname", Session["Name"].ToString());
                data.Add("email", EmailID);
                data.Add("phone", MobileNo);
                data.Add("productinfo", HttpUtility.HtmlEncode("{\"paymentIdentifiers\":" + GetJson().ToString() + "}"));  //-->new code gives checksum failed
                data.Add("surl", Path.GetDirectoryName(HttpContext.Current.Request.Url.OriginalString) + "/Student/surl.aspx");
                // data.Add("surl", "http://172.0.1.105:82/Student/surl.aspx");      // data.Add("surl", Request.Form["surl"].Trim());//http://tmu.ac.in/feepay/surl.aspx
                data.Add("furl", "http://172.0.1.105:82/furl.aspx");      // data.Add("furl", Request.Form["furl"].Trim());//http://tmu.ac.in/feepay/furl.aspx
                data.Add("lastname", "");
                data.Add("curl", "http://172.0.1.105:82/curl.aspx");      // data.Add("curl", Request.Form["curl"].Trim());//http://tmu.ac.in/feepay/curl.aspx
                data.Add("address1", "");
                data.Add("address2", "");
                data.Add("city", "");
                data.Add("state", "");
                data.Add("country", "");
                data.Add("zipcode", "");
                data.Add("udf1", "");
                data.Add("udf2", "");
                data.Add("udf3", "");
                data.Add("udf4", "");
                data.Add("udf5", "");
                data.Add("pg", "");
                data.Add("service_provider", "payu_paisa");                 //data.Add("service_provider", Request.Form["service_provider"].Trim());//payu_paisa
                string strForm = PreparePOSTForm(action1, data);
                //strForm ="<form id='PostForm' name='PostForm' action='https://secure.payu.in/_payment/_payment' method='POST'><input type='hidden' name='lastname' value=''><input type='hidden' name='address2' value=''><input type='hidden' name='udf5' value=''><input type='hidden' name='service_provider' value='payu_paisa'><input type='hidden' name='curl' value='http://localhost:17068/TMUiZone/Enquiry/curl.aspx'><input type='hidden' name='udf4' value=''><input type='hidden' name='txnid' value='100000'><input type='hidden' name='furl' value='http://localhost:17068/TMUiZone/Enquiry/furl.aspx'><input type='hidden' name='state' value=''><input type='hidden' name='udf2' value=''><input type='hidden' name='udf1' value=''><input type='hidden' name='zipcode' value=''><input type='hidden' name='amount' value='1'><input type='hidden' name='email' value='pramoddanu09@gmail.com'><input type='hidden' name='city' value=''><input type='hidden' name='country' value=''><input type='hidden' name='udf3' value=''><input type='hidden' name='address1' value=''><input type='hidden' name='hash' value='6985606df1b2b2dfd1256bcf71e0fc2492403b5892b7fab7c5f82d195b032c457654c65933c77ca2562ae836a41d9460d3753b2abbea263befafc42c199f08b7'><input type='hidden' name='key' value='rZAKq6'><input type='hidden' name='pg' value=''><input type='hidden' name='surl' value='http://localhost:17068/TMUiZone/Enquiry/surl.aspx'><input type='hidden' name='firstname' value='AJAY RAGHUVANSHI'><input type='hidden' name='productinfo' value='{&quot;paymentIdentifiers&quot;:[{&quot;field&quot;:&quot;StudentName&quot;,&quot;value&quot;:&quot;AJAY RAGHUVANSHI&quot;},{&quot;field&quot;:&quot;Fee_Type&quot;,&quot;value&quot;:&quot;REGISTRATION&quot;}]}'><input type='hidden' name='phone' value='8882074374'></form><script language='javascript'>var vPostForm = document.PostForm;vPostForm.submit();</script>";
                Page.Controls.Add(new LiteralControl(strForm));
            }
            else
            {
                //no hash
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }

    }
    public string GetJson()
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row = null;
        DataTable dtEmployee = new DataTable();
        dtEmployee.Columns.Add("field", typeof(string));
        dtEmployee.Columns.Add("value", typeof(string));
        dtEmployee.Rows.Add("StudentName", Session["Name"].ToString());
        dtEmployee.Rows.Add("Fee_Type", FeeType);
        foreach (DataRow dr in dtEmployee.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dtEmployee.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }
    public string Generatehash512(string text)
    {
        byte[] message = Encoding.UTF8.GetBytes(text);
        UnicodeEncoding UE = new UnicodeEncoding();
        byte[] hashValue;
        SHA512Managed hashString = new SHA512Managed();
        string hex = "";
        hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }
    private string PreparePOSTForm(string url, System.Collections.Hashtable data)      // post form
    {
        string formID = "PostForm";
        StringBuilder strForm = new StringBuilder();
        strForm.Append("<form id=\"" + formID + "\" name=\"" + formID + "\" action=\"" + url + "\" method=\"POST\">");
        foreach (System.Collections.DictionaryEntry key in data)
        {
            strForm.Append("<input type=\"hidden\" name=\"" + key.Key + "\" value=\"" + key.Value + "\">");
        }
        strForm.Append("</form>");
        StringBuilder strScript = new StringBuilder();
        strScript.Append("<script language='javascript'>");
        strScript.Append("var v" + formID + " = document." + formID + ";");
        strScript.Append("v" + formID + ".submit();");
        strScript.Append("</script>");
        return strForm.ToString() + strScript.ToString();
    }

    public void showdata()
    {

        string orderid = "DCO44" + DateTime.Now.Ticks.ToString();
        string Fine = "";
        int temp = 0;
        for (int i = 0; i <= grdFeeDetails.Rows.Count - 1; i++)
        {
            string Semester = "";
            string Year = "";
            GridViewRow row = grdFeeDetails.Rows[i];
            CheckBox Chbox = (CheckBox)row.FindControl("chkboxSelectAmount");
            Label sem = (Label)row.FindControl("lblSem");
            HiddenField FineFees = (HiddenField)row.FindControl("hdfDesc");
            HiddenField EntryNo = (HiddenField)row.FindControl("hdfEntryNo");
            HiddenField hdfAmount = (HiddenField)row.FindControl("hdfAmount");
            if (Chbox.Checked == true)
            {
                if (sem.Text.Contains("YEAR"))
                {
                    Year = sem.Text;
                }
                else
                {
                    Semester = sem.Text;
                }
                if (FineFees.Value.Contains("Fine") == true)
                {
                    Fine = "1";
                    EntryNo.Value = "0";
                }
                else
                {
                    Fine = "";
                }
                Fees = Convert.ToDecimal(Amt.Value);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[TMU$Online Payment Log]([Payment Date],[Amount],[Status],[UserID],[GATEWAY],[GatewayStatus],[OrderID],[CLE Entry No],[Semester FEE],[Year FEE],[Temp 1],[Temp 2],[Temp 3],[Orgnized Date])VALUES(GETUTCDATE()," + hdfAmount.Value + ",0 ,'" + Session["uid"].ToString() + "' ,'PAYTM'  ,0 ,'" + orderid + "','" + EntryNo.Value + "','" + Semester + "','" + Year + "','','" + Fine + "','', DATEADD(Minute,330,Getdate()))", con);
                temp = cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        if (temp == 1)
        {


            Dictionary<string, string> parameters = new Dictionary<string, string>();



            //-------->Test
            String merchantKey = "7v_qN#jfvvCiLSOB";
            // Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("MID", "Teerth64420690832928");
            parameters.Add("CHANNEL_ID", "WEB");
            parameters.Add("INDUSTRY_TYPE_ID", "Education");
            parameters.Add("WEBSITE", "DEFAULT");
            parameters.Add("EMAIL", "");
            parameters.Add("MOBILE_NO", "");
            //parameters.Add("CUST_ID", Session["uid"].ToString().Replace("/", "").ToString());
            parameters.Add("CUST_ID", Session["enroll"].ToString());
            parameters.Add("ORDER_ID", orderid);
            parameters.Add("TXN_AMOUNT", "1");
            // parameters.Add("TXN_AMOUNT", Fees.ToString());
            //parameters.Add("EXTENDINFO", Session["uid"].ToString());
            // parameters.Add("mercUnqRef", Session["enroll"].ToString());

            parameters.Add("CALLBACK_URL", "http://172.0.1.105:82/Student/PaytmTransactionStatus.aspx");//http://14.139.238.130:82/
            // parameters.Add("CALLBACK_URL", "http://localhost:24386/TMUiZone/Student/PaytmTransactionStatus.aspx");
            string checksum = CheckSum.generateCheckSum(merchantKey, parameters);
            string paytmURL = "https://securegw.paytm.in/order/process";
            //string paytmURL = "https://securegw-stage.paytm.in/theia/processTransaction?orderid=" + orderid;
            Session["uid"] = null;

            string outputHTML = "<html>";






            outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
            outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
            outputHTML += "<table border='1'>";
            outputHTML += "<tbody>";
            foreach (string key in parameters.Keys)
            {
                outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
            }
            outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            outputHTML += "<script type='text/javascript'>";
            outputHTML += "document.f1.submit();";
            outputHTML += "</script>";
            outputHTML += "</form>";
            outputHTML += "</script>";
            outputHTML += "</body>";
            outputHTML += "</html>";
            Response.Write(outputHTML);
        }
    }


    protected void btnPaytm_Click(object sender, ImageClickEventArgs e)
    {

        BindTable();
        bindAccountNo();
        showdata();

        //VarifyCheckSum();
    }
    protected void btncancelpopup_Click(object sender, EventArgs e)
    {
        Response.Redirect("../FeeDetails.aspx");
    }
}