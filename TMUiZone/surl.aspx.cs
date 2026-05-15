using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;
using DL;

using System.Net;
//using NAVWeb;  //Test
using WSNAVLIVE_VP;

public partial class surl : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    string feestatusfromPayU = "";
    string MIHPayIDfromPayU = "";
    string TransactionId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        ApplicationFormDL apdl = new ApplicationFormDL();      
        LabelError.Text = "";        
        try
        {

            string[] merc_hash_vars_seq;
            string merc_hash_string = string.Empty;
            string merc_hash = string.Empty;
            string order_id = string.Empty;            
            string hash_seq = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10";
            if (Request.Form["status"] == "success")
            {
               
                merc_hash_vars_seq = hash_seq.Split('|');
                Array.Reverse(merc_hash_vars_seq);
                merc_hash_string = Request.Form["additionalCharges"] + "|" + ConfigurationManager.AppSettings["SALT"] + "|" + Request.Form["status"];
                //merc_hash_string =ConfigurationManager.AppSettings["SALT"] + "|" + Request.Form["status"];

                foreach (string merc_hash_var in merc_hash_vars_seq)
                {
                    merc_hash_string += "|";
                    merc_hash_string = merc_hash_string + (Request.Form[merc_hash_var] != null ? Request.Form[merc_hash_var] : "");

                }
                //Response.Write(merc_hash_string);
                ///exit;
                ///

                merc_hash = Generatehash512(merc_hash_string).ToLower();



                if (merc_hash != Request.Form["hash"])
                {
                    Response.Write("Hash value did not matched");

                }
                else
                {
                    order_id = Request.Form["txnid"];
                    lblApplicationNo.Text = "";
                    pnlSuccess.Visible = true;
                    lbltxtid.Text = Request.Form["txnid"].ToString();
                    lblAmt.Text = Request.Form["amount"].ToString();
                    //string EnquiryNo = Session["EnquiryNo"].ToString();                    
                    decimal ApplicationCost = Convert.ToDecimal(lblAmt.Text);
                  //  decimal ApplicationCost = Convert.ToDecimal(Session["Amount"]);
                  //  lblAmt.Text = ApplicationCost.ToString();
                    lblOrderid.Text = Request.Form["payuMoneyId"].ToString();                    
                    TransactionId = order_id;
                    lblPaymentMode.Text = Request.Form["mode"].ToString();
                    MIHPayIDfromPayU=Request.Form["mihpayid"];
                    lblPaymentDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");                   
                    lblBankName.Text = Request.Form["bank_ref_num"].ToString();
                    string EnquiryNo =lbltxtid.Text.Substring(0, lbltxtid.Text.Length - 5);
                    
                    //====================== insert into TMU$Gen_ Journal Line===========================
                    try
                    {
                        VoucherPosting vpWeb = new VoucherPosting();
                        vpWeb.UseDefaultCredentials = true;
                        //vpWeb.Url = "http://172.14.7.107:6047/TEST/WS/TMU/Codeunit/VoucherPosting";//Test
                        //vpWeb.Credentials = new NetworkCredential("shubham\\administrator", "abcd@1234");//Test


                        //
                        DataTable dtNAV = new DataTable();
                        SqlCommand cmdNAV = new SqlCommand("Proc_GetNAVCreditionalLive", con);
                        cmdNAV.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter daNAV = new SqlDataAdapter(cmdNAV);
                        daNAV.Fill(dtNAV);
                        
                        vpWeb.Url = dtNAV.Rows[0]["URL"].ToString();
                        vpWeb.Credentials = new NetworkCredential(dtNAV.Rows[0]["UserID"].ToString(), dtNAV.Rows[0]["Password"].ToString()); 








                        //vpWeb.Url = "http://172.0.1.102:8047/TMU/WS/TMU/Codeunit/VoucherPosting";//Live
                        //vpWeb.Credentials = new NetworkCredential("tmunaverp\\erpapp", "Vinay@cs");//Live
                        bool t = vpWeb.CreateJournalApplicationFee("PAYU", EnquiryNo, lblOrderid.Text, ApplicationCost);
                        //bool t = apdl.insertGen_JournalLine(EnquiryNo, ApplicationCost, lblOrderid.Text,"PAYU");

                        DataTable dt = apdl.GetApplicationNoByEnquiryId(EnquiryNo);
                        lblApplicationNo.Text = dt.Rows[0]["ApplicationNo"].ToString();
                        lblApplicantName.Text = dt.Rows[0]["ApplicantName"].ToString();
                        lblDob.Text = dt.Rows[0]["DOB"].ToString();
                        lblFatherName.Text = dt.Rows[0]["FatherName"].ToString();
                        string MobileNo = dt.Rows[0]["Mobile Number"].ToString();
                        try
                        {
                            SendMessage(MobileNo, "Your Application No is: '" + lblApplicationNo.Text + "' ");//Message 
                        }
                        catch
                        {

                        }
                        if (t == true )
                        {
                            LabelError.Text = "Thank you";
                        }
                    }
                    catch
                    {
                      //  LabelError.Text = "Thank you";
                    }
                    
                    
                    
                    //====================Make Payment==========
                   
                }

            }

            else
            {
                //Hash value did not matched
                Response.Write("Hash value did not matched");
                // osc_redirect(osc_href_link(FILENAME_CHECKOUT, 'payment' , 'SSL', null, null,true));

            }
        }

        catch (Exception ex)
        {
            Response.Write("<span style='color:red'>" + ex.Message + "</span>");

        }



        // end response handling
    }


    //below cod not used==
    int updatestatus(string TransactionId, string MIHPayIDfromPayU, string mode, string amount, string productinfo, string bank_ref_num, string PG_TYPE, string paymentId, string key)
    {
               con.Open();
        string status = "success";
        cmd = new SqlCommand("update FEE_TRANSACTION set  BANK_AMOUNT='" + amount + "',  MODE='" + mode + "' , BANKFEE_TYPE='" + productinfo + "' , BANK_REF_NO='" + bank_ref_num + "' , PG_TYPE='" + PG_TYPE + "' , PAYMENT_ID='" + paymentId + "' , MERCHANTKEY='" + key + "',STATUS='" + status + "', MIHPayID='" + MIHPayIDfromPayU + "' where TRANSACTION_ID='" + TransactionId + "'", con);
        int temp = cmd.ExecuteNonQuery();
        if (temp == 1)
        {
          //  LabelError.Text = "Informations updated successfully";
        }

        con.Close();
        return temp;
    }
       

    protected void LinkButtonPayFee_Click(object sender, EventArgs e)
    {
       // Response.Redirect("http://tmu.ac.in/feepay/PayFee.aspx");
        Response.Redirect("http://localhost:17068/TMUiZone/Enquiry/Application.aspx");
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

    public bool SendMessage(string MobileNo, string Message)
    {
        MobileNo = MobileNo.Substring(MobileNo.Length - 10, 10);
        SMS(MobileNo, Message);
        return true;

    }
    public void SMS(String MobileNo, string Msg)
    {
        //  Website: http://www.universalsmsadvertising.com/     //  Username: 9837016352   //  Password: 9837016352 
        // MobileNo = "91" + MobileNo;
        // MobileNo = "91" + 9899906658;
        string url = "http://www.universalsmsadvertising.com/universalsmsapi.php?user_name=9837016352&user_password=9837016352&mobile=" + MobileNo + "&sender_id=TMUniv&type=F&text=" + Msg + "";
        System.Net.HttpWebRequest fr;
        Uri targetURI = new Uri(url);
        fr = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(targetURI);
        if (fr.GetResponse().ContentLength > 0)
        {
            System.IO.StreamReader str = new System.IO.StreamReader(fr.GetResponse().GetResponseStream());
            Response.Write(str.ReadToEnd());
            str.Close();
        }
    }

}