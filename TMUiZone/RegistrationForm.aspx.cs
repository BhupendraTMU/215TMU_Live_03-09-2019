using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using paytm;

public partial class RegistrationForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnPay_Click(object sender, EventArgs e)
    {
        showdata();
    }
    public void showdata()
    {
        String masterKey = "@8OjvioAWTk7mx05";//test       // String masterKey = "WDsyj5CtsaL7thf@";//live
        //String masterKey = "@8OjvioAWTk7mx05";
        string orderid = txtRegistrationID.Text  + DateTime.Now.Ticks.ToString();
        Dictionary<String, String> parameters = new Dictionary<string, string>();

        parameters.Add("MID", "KVInte52546870297091");//Test // parameters.Add("MID", "Intell28698017523154");//Live        
        parameters.Add("ORDER_ID", orderid);
        parameters.Add("CUST_ID", Session["uid"].ToString());
        //parameters.Add("MOBILE_NO", "9015762885");// parameters.Add("MOBILE_NO", Session["MobilenoPay"].ToString());
        //parameters.Add("EMAIL", "join2ashu@gmail.com"); //parameters.Add("EMAIL", Session["email"].ToString());
        parameters.Add("CHANNEL_ID", "WEB");
        parameters.Add("INDUSTRY_TYPE_ID", "Retail");
        parameters.Add("WEBSITE", "intellipropweb");

        parameters.Add("CALLBACK_URL", "http://localhost:1077/TMUiZone/transactionstatus.aspx");
        string paytmURL = "https://pguat.paytm.com/oltp-web/processTransaction";

        // parameters.Add("CALLBACK_URL", "http://localhost:4304/TMU/transactionstatus.aspx");
        ////parameters.Add("CALLBACK_URL", "http://www.smartclasseducational.com/Agent_OrderTRNStatus.aspx");//Educomp
        //string paytmURL = "https://pguat.paytm.com/oltp-web/processTransaction";//Test string paytmURL = "https://secure.paytm.in/oltp-web/processTransaction";//Deduct
              
        parameters.Add("TXN_AMOUNT", txtPaymentAmount.Text);
        string checksum = CheckSum.generateCheckSum(masterKey, parameters);

        string outputHTML = "<html>";
        outputHTML += "<head>";
        outputHTML += "<title>Merchant Check Out Page</title>";
        outputHTML += "</head>";
        outputHTML += "<body>";
        outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
        outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
        outputHTML += "<table border='1'>";
        outputHTML += "<tbody>";

        foreach (string key in parameters.Keys)
        {
            outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>'";
        }


        outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
        outputHTML += "</tbody>";
        outputHTML += "</table>";
        outputHTML += "<script type='text/javascript'>";
        outputHTML += "document.f1.submit();";
        outputHTML += "</script>";
        outputHTML += "</form>";
        outputHTML += "</body>";
        outputHTML += "</html>";

        Response.Write(outputHTML);

    }
}