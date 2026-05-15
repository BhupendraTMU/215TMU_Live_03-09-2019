using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Drawing;
using System.Web.SessionState;
using paytm;
using System.Net;
using System.Net.Mail;
using DL;

using WSNAVLIVE_VP;
//using NAVWeb;

public partial class PaytmTransactionStatus : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    ApplicationFormDL apdl = new ApplicationFormDL();      
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                // Show_Expiry();               
               show();
            }
        }
        catch (Exception)
        {
            Response.Redirect("PaytmErrorPage.aspx");
        }
    }
    public void show()
    {
       //  String masterKey = "tvo4V_UPQrYk@8Co";//Test
        String masterKey = "7v_qN#jfvvCiLSOB";//Live     //"tvo4V_UPQrYk@8Co";//test    // String masterKey = "WDsyj5CtsaL7thf@";//live
        Dictionary<String, String> parameters = new Dictionary<string, string>();

        parameters.Add("MID", Request.Form["MID"]);
        parameters.Add("TXNID", Request.Form["TXNID"]);
        parameters.Add("ORDER_ID", Request.Form["ORDERID"]);
        parameters.Add("BANKTXNID", Request.Form["BANKTXNID"]);
        parameters.Add("TXNAMOUNT", Request.Form["TXNAMOUNT"]);
        parameters.Add("CURRENCY", Request.Form["CURRENCY"]);
        parameters.Add("STATUS", Request.Form["STATUS"]);
        parameters.Add("RESPCODE", Request.Form["RESPCODE"]);
        parameters.Add("RESPMSG", Request.Form["RESPMSG"]);
        parameters.Add("TXNDATE", Request.Form["TXNDATE"]);
        parameters.Add("GATEWAYNAME", Request.Form["GATEWAYNAME"]);
        parameters.Add("BANKNAME", Request.Form["BANKNAME"]);
        parameters.Add("PAYMENTMODE", Request.Form["PAYMENTMODE"]);

        string str = Request.Form["CHECKSUMHASH"];

        if (CheckSum.verifyCheckSum(masterKey, parameters, str))
        {
            //  Response.Redirect("tarnsactstatus.aspx");
        }
        else
        {
            Response.Redirect("PaytmErrorPage.aspx");//Response.Redirect("maintenence.aspx");
        }

        lblStatus.Text = Request.Form["STATUS"].ToString();
        lblStatusCode.Text = Request.Form["RESPCODE"].ToString();
        lblOrderid.Text = Request.Form["ORDERID"].ToString();
        lblAmount.Text = Request.Form["TXNAMOUNT"].ToString();
        lblPaymentDate.Text = Request.Form["TXNDATE"].ToString();
        lblBankName.Text = Request.Form["BANKNAME"].ToString();
        lblPaymentMode.Text = Request.Form["PAYMENTMODE"].ToString();
        lbltxtid.Text = Request.Form["TXNID"].ToString();
        Session["TXNID"] = Request.Form["TXNID"].ToString();
        //lblAgentId.Text = Session["uid"].ToString();
        
        //====================== insert into TMU$Gen_ Journal Line===========================
        try
        {
            string txnid = lblOrderid.Text.Replace("--", "/");
            string EnquiryNo = txnid.Substring(0, txnid.Length - 5);
            decimal ApplicationCost = Convert.ToDecimal(lblAmount.Text);
            
            //int t = apdl.insertGen_JournalLine(EnquiryNo, ApplicationCost, lblOrderid.Text,"PAYTM");
            DataTable dtNAV = new DataTable();
            SqlCommand cmdNAV = new SqlCommand("Proc_GetNAVCreditionalLive", con);
            cmdNAV.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter daNAV = new SqlDataAdapter(cmdNAV);
            daNAV.Fill(dtNAV);
            VoucherPosting vpWeb = new VoucherPosting();
            vpWeb.UseDefaultCredentials = true;
            vpWeb.Url = dtNAV.Rows[0]["URL"].ToString();
            vpWeb.Credentials = new NetworkCredential(dtNAV.Rows[0]["UserID"].ToString(), dtNAV.Rows[0]["Password"].ToString()); 
         //   vpWeb.Url = "http://172.14.7.107:6047/TEST/WS/TMU/Codeunit/VoucherPosting";//Test
           // vpWeb.Credentials = new NetworkCredential("shubham\\administrator", "abcd@1234");//Test
            bool t = vpWeb.CreateJournalApplicationFee("PAYTM", EnquiryNo, lblOrderid.Text, Convert.ToDecimal(lblAmount.Text));

            DataTable dt = apdl.GetApplicationNoByEnquiryId(EnquiryNo);
            lblApplicationNo.Text = dt.Rows[0]["ApplicationNo"].ToString();
            lblApplicantName.Text = dt.Rows[0]["ApplicantName"].ToString();
            lblDob.Text = dt.Rows[0]["DOB"].ToString();
            lblFatherName.Text = dt.Rows[0]["FatherName"].ToString();
            lblAgentId.Text = EnquiryNo;
            string MobileNo = dt.Rows[0]["Mobile Number"].ToString();
            pnlSuccess.Visible = true;
            try
            {
                SendMessage(MobileNo, "Your Application No is: '" + lblApplicationNo .Text+ "' ");//Message 
            }
            catch
            { 

            }
           
            if (t == true)
            {
                LabelError.Text = "Thank you";
                pnlSuccess.Visible = true;
            }
        }
        catch
        {
            //  LabelError.Text = "Thank you";
        }
                    
        //====================== insert into TMU$Gen_ Journal Line===========================
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