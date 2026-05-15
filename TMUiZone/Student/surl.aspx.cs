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

public partial class surl : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    string feestatusfromPayU = "";
    string MIHPayIDfromPayU = "";
    string TransactionId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        StudentDetailsViewDL SDL = new StudentDetailsViewDL();    
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
                    lblStudentNo.Text = "";
                    pnlSuccess.Visible = true;
                    lbltxtid.Text = Request.Form["txnid"].ToString();
                    string StudentNo = Session["uid"].ToString();
                    decimal ApplicationCost = Convert.ToDecimal(Session["Amount1"]);
                    lblAmt.Text = ApplicationCost.ToString();
                    lblOrderid.Text = Request.Form["payuMoneyId"].ToString();                    
                    TransactionId = order_id;
                    lblPaymentMode.Text = Request.Form["mode"].ToString();
                    MIHPayIDfromPayU=Request.Form["mihpayid"];
                    lblPaymentDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");                   
                    lblBankName.Text = Request.Form["bank_ref_num"].ToString();
                    
                    //====================== insert into TMU$Gen_ Journal Line===========================
                    DataTable dt = SDL.GetStudentDetailsForSurl(StudentNo);
                    lblStudentNo.Text = dt.Rows[0]["No_"].ToString();
                    lblStudentName.Text = dt.Rows[0]["Student Name"].ToString();
                    lblDob.Text = dt.Rows[0]["DOB"].ToString();
                    lblFatherName.Text = dt.Rows[0]["Fathers Name"].ToString();
                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                    dt1 = (DataTable)Session["AmountDetails"];
                    dt2 = (DataTable)Session["AccountNo"];
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            SDL.insertGen_JournalLine(Session["uid"].ToString(), Convert.ToDecimal(dt1.Rows[i]["Amount"].ToString()), dt1.Rows[i]["Description"].ToString(), dt2.Rows[i]["G_L Account"].ToString(), lblOrderid.Text, dt1.Rows[i]["Entry No"].ToString(),"PAYU");
                        }
                    }
                    
                    //if (t == 2)
                    //{
                    //    LabelError.Text = "Thank you, Click Back Button to see details";
                    //}
                    
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



}