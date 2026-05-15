using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using paytm;
using System.IO;
using System.Text;
public partial class FeeStructure : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void showdata()
    {

        String masterKey = "@8OjvioAWTk7mx05";
        string orderid = "DCO4" + DateTime.Now.Ticks.ToString();
        Dictionary<String, String> parameters = new Dictionary<string, string>();
        parameters.Add("MID", "KVInte52546870297091");
        parameters.Add("ORDER_ID", orderid);
        parameters.Add("CUST_ID", Session["uid"].ToString());
        parameters.Add("CHANNEL_ID", "WEB");
        parameters.Add("INDUSTRY_TYPE_ID", "Retail");
        parameters.Add("WEBSITE", "intellipropweb");

        parameters.Add("CALLBACK_URL", "http://localhost:4304/TMU/transactionstatus.aspx");


        string paytmURL = "https://pguat.paytm.com/oltp-web/processTransaction";//?orderid=" + orderid;
        //if (PaytmConstants.MODE == "PROD")
        //{
        //   paytmURL = "https://secure.paytm.in/oltp-web/processTransaction";
        // }
        parameters.Add("TXN_AMOUNT", "1");
        string checksum = CheckSum.generateCheckSum(masterKey, parameters);

        //HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(paytmURL);
        //webRequest.Host = paytmURL;
        //webRequest.Method = "POST";
        //webRequest.Accept = "application/x-www-form-urlencoded";
        //webRequest.ContentType = "application/x-www-form-urlencoded";
        //webRequest.

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

    protected void Button1_Click(object sender, EventArgs e)
    {
        showdata();
    }
}