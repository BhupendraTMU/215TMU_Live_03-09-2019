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

public partial class furl : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    string feestatusfromPayU ="";
    string MIHPayIDfromPayU = "";
    string TransactionId = "";
 
    protected void Page_Load(object sender, EventArgs e)
    {
        //Connection c = new Connection();
        //con = c.connect();
        //LabelError.Text = "";
        //if (Session["LoggedIn"] == null)
        //{
        // Response.Redirect("http://tmu.ac.in/feepay/StuLogin.aspx");
        //}
     
    
     // start response handling
        try
        {

            string[] merc_hash_vars_seq;
            string merc_hash_string = string.Empty;
            string merc_hash = string.Empty;
            string order_id = string.Empty;
            string hash_seq = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10";
           // string hash_seq = "diEVMlUc|status|udf10|udf9|udf8|udf7|udf6|udf5|udf4|udf3|udf2|udf1|email|firstname|productinfo|amount|txnid|key";

            if (Request.Form["status"] == "failure")
            {



                order_id = Request.Form["txnid"];
                //start my code
                TransactionId = order_id;
                MIHPayIDfromPayU = Request.Form["mihpayid"];
               // int t = updatestatus(TransactionId, MIHPayIDfromPayU, Request.Form["mode"], Request.Form["amount"], Request.Form["productinfo"], Request.Form["bank_ref_num"], Request.Form["PG_TYPE"], Request.Form["payuMoneyId"], Request.Form["key"]);
                int t = 1;

                if (t == 1)
                    LabelError.Text = "Sorry, transaction has been failed";
                //end my code
               
                merc_hash_vars_seq = hash_seq.Split('|');
                Array.Reverse(merc_hash_vars_seq);
                merc_hash_string = ConfigurationManager.AppSettings["SALT"] + "|" + Request.Form["status"];


                foreach (string merc_hash_var in merc_hash_vars_seq)
                {
                    merc_hash_string += "|";
                    merc_hash_string = merc_hash_string + (Request.Form[merc_hash_var] != null ? Request.Form[merc_hash_var] : "");

                }
              //  Response.Write(merc_hash_string);
                ///exit;
                ///

                merc_hash = Generatehash512(merc_hash_string).ToLower();



                if (merc_hash != Request.Form["hash"])
                {
                    //Response.Write("Hash value did not matched");
                    //order_id = Request.Form["txnid"];
                    ////start my code
                    //TransactionId = order_id;
                    //MIHPayIDfromPayU = Request.Form["mihpayid"];
                    //int t = updatestatus(TransactionId, MIHPayIDfromPayU, Request.Form["mode"], Request.Form["amount"], Request.Form["productinfo"], Request.Form["bank_ref_num"], Request.Form["PG_TYPE"], Request.Form["payuMoneyId"], Request.Form["key"]);

                    //if (t == 1)
                    //    LabelError.Text = "Sorry, transaction has been failed";
                    ////end my code
                }
                else
                {
                    
                   // Response.Write("value matched");

                    
                }

            }

            else
            {
                //Hash value did not matched
              //  Response.Write("Hash value did not matched");
                // osc_redirect(osc_href_link(FILENAME_CHECKOUT, 'payment' , 'SSL', null, null,true));
                
            

            }
        }

        catch (Exception ex)
        {
            Response.Write("<span style='color:red'>" + ex.Message + "</span>");

        }



        // end response handling

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
    //int updatestatus(string TransactionId, string MIHPayIDfromPayU, string mode, string amount, string productinfo, string bank_ref_num, string PG_TYPE, string paymentId, string key)
    //{
    //    con.Open();
    //    string status = "failure";
    //    cmd = new SqlCommand("update FEE_TRANSACTION set  BANK_AMOUNT='" + amount + "',  MODE='" + mode + "' , BANKFEE_TYPE='" + productinfo + "' , BANK_REF_NO='" + bank_ref_num + "' , PG_TYPE='" + PG_TYPE + "' , PAYMENT_ID='" + paymentId + "' , MERCHANTKEY='" + key + "',STATUS='" + status + "', MIHPayID='" + MIHPayIDfromPayU + "' where TRANSACTION_ID='" + TransactionId + "'", con);
    //    int temp = cmd.ExecuteNonQuery();
    //    if (temp == 1)
    //    {
    //        //  LabelError.Text = "Informations updated successfully";
    //    }
    //    con.Close();
    //    return temp;
    //}


    //protected void LinkButtonSigOut_Click(object sender, EventArgs e)
    //{
    //    Session["LoggedIn"] = null;
    //    Session.Clear();
    //    Session.Abandon();
    //    Response.Redirect("http://tmu.ac.in/feepay/StuLogin.aspx");
    //}

    protected void LinkButtonPayFee_Click(object sender, EventArgs e)
    {
        //Response.Redirect("http://tmu.ac.in/feepay/PayFee.aspx");
        Response.Redirect("http://localhost:17068/TMUiZone/Enquiry/Application.aspx");
    }

}