using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using WSNAVLIVE_VP;
public partial class TEST1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //VoucherPosting vpWeb = new VoucherPosting();
        //vpWeb.UseDefaultCredentials = true;
        //vpWeb.Url = "http://172.0.1.102:8047/TMU/WS/TMU/Codeunit/VoucherPosting";//Live
        //vpWeb.Credentials = new NetworkCredential("tmunaverp\\erpapp", "corp@123!@#");//Live
        ////   vpWeb.Url = "http://172.14.7.107:6047/TEST/WS/TMU/Codeunit/VoucherPosting";//Test
        //// vpWeb.Credentials = new NetworkCredential("shubham\\administrator", "abcd@1234");//Test
        //bool t = vpWeb.CreateJournalApplicationFee("PAYTM", "ENQ/17-18/02251", "ENQ--17-18--022481722", 1);
        //lblmsg.Text = Convert.ToString(t);
    }
}