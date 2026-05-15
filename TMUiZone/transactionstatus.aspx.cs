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


public partial class transactionstatus : System.Web.UI.Page
{
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
            Response.Redirect("errorpage.aspx");
        }
    }
    public void show()
    {

        
        String masterKey = "@8OjvioAWTk7mx05";//test    // String masterKey = "WDsyj5CtsaL7thf@";//live

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
            //Response.Redirect("maintenence.aspx");
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
        lblAgentId.Text = Session["uid"].ToString();
       
    }
}