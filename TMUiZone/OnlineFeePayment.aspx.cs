using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using paytm;

public partial class OnlineFeePayment : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    string StudentNo = "", DOB = "", Fees = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
               
        }
        catch
        {
        }

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("[proc_GetStudentNoforFee]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cmd.Parameters.Add("@StuNo", txtUserid.Text);
        cmd.Parameters.Add("@DOB", txtpassword.Text);
      
        DataTable dt = new DataTable();
        da.Fill(dt);

        
         if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["Enrollment No_"].ToString() != "")
            {
                mainDiv.Visible = false;
                DivMsg.Visible = true;
                DivMsg.InnerHtml = "Your Enrolment No " + dt.Rows[0]["Enrollment No_"].ToString() + " is already generated. Please click  <a href='http://portal.tmu.ac.in/'>portal.tmu.ac.in</a> and login to make payment..";
            }
            else
            {
                //mainDiv.InnerText = "";
                Session["STUDNo"] = dt.Rows[0]["No_"].ToString();
                lblStudentName.Text = dt.Rows[0]["Student Name"].ToString();
                lblFather.Text = dt.Rows[0]["Fathers Name"].ToString();
                lblCourse.Text = dt.Rows[0]["Course Name"].ToString();
                lblCollege.Text = dt.Rows[0]["College Name"].ToString();
                hfSem.Value = dt.Rows[0]["Semester"].ToString();
                hfyear.Value = dt.Rows[0]["Year"].ToString();
                mainDiv.Visible = true;
                DivMsg.Visible = false;
            }
        }
        else
        {
            mainDiv.Visible = false;
            DivMsg.Visible = true;
            DivMsg.InnerHtml = "This Student is not find in the Record.";
        }
    }

    protected void btnPay_Click(object sender, EventArgs e)
    {
        if (txtamount.Text == "" || txtamount.Text == "0" || txtamount.Text == "0.00")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter the Amount.')", true);
            return;
        }
        else
        {
            string Semester = hfSem.Value;
            string Year = hfyear.Value;
            Fees = Convert.ToDecimal(txtamount.Text).ToString();
            string orderid = "DCO44" + DateTime.Now.Ticks.ToString();
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[TMU$Online Payment Log]([Payment Date],[Amount],[Status],[UserID],[GATEWAY],[GatewayStatus],[OrderID],[CLE Entry No],[Semester FEE],[Year FEE],[Temp 1],[Temp 2],[Temp 3],[Orgnized Date])VALUES(getdate()," + Fees + ",0 ,'" + Session["STUDNo"].ToString() + "' ,'PAYTM'  ,0 ,'" + orderid + "','" + 0 + "','" + Semester + "','" + Year + "','1','','', DATEADD(Minute,330,Getdate()))", con);
            int temp = cmd.ExecuteNonQuery();
            con.Close();
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
                parameters.Add("CUST_ID", Session["STUDNo"].ToString().Replace("/", "").ToString());
                //parameters.Add("CUST_ID", Session["enroll"].ToString());
                parameters.Add("ORDER_ID", orderid);
                parameters.Add("TXN_AMOUNT", Fees.ToString());
                parameters.Add("CALLBACK_URL", "http://172.0.1.105:82/Student/PaytmTransactionStatus.aspx");//http://14.139.238.130:82/
                //parameters.Add("CALLBACK_URL", "http://localhost:24386/TMUiZone/Student/PaytmTransactionStatus.aspx");
                string checksum = CheckSum.generateCheckSum(merchantKey, parameters);
                string paytmURL = "https://securegw.paytm.in/order/process";
                //string paytmURL = "https://securegw-stage.paytm.in/theia/processTransaction?orderid=" + orderid;
                Session["STUDNo"] = null;
                // Fees = 1;
                //-----Live
                // String merchantKey = "tvo4V_UPQrYk@8Co";
                // parameters.Add("MID", " tvo4V_UPQrYk@8Co");
                // parameters.Add("CHANNEL_ID", "WEB");
                // parameters.Add("INDUSTRY_TYPE_ID", "Education");
                // parameters.Add("WEBSITE", "DEFAULT");
                // parameters.Add("EMAIL", EmailID);
                // parameters.Add("MOBILE_NO", MobileNo);
                // parameters.Add("CUST_ID", Session["uid"].ToString().Replace("/", "").ToString());
                // parameters.Add("ORDER_ID", orderid);
                // parameters.Add("TXN_AMOUNT", Fees.ToString()); //parameters.Add("TXN_AMOUNT", "1");
                // // parameters.Add("CALLBACK_URL", Path.GetDirectoryName(HttpContext.Current.Request.Url.OriginalString) + "/Student/PaytmTransactionStatus.aspx");
                //// parameters.Add("CALLBACK_URL", "http://localhost:1164/TMUiZone/Student/PaytmTransactionStatus.aspx");//http://14.139.238.130:82/
                // parameters.Add("CALLBACK_URL", "http://localhost:1048/TMUiZone/Student/PaytmTransactionStatus.aspx");//
                // string checksum = CheckSum.generateCheckSum(merchantKey, parameters);
                // string paytmURL = "https://securegw-stage.paytm.in/theia/processTransaction?orderid=" + orderid;
                //Path.GetDirectoryName(HttpContext.Current.Request.Url.AbsolutePath);

                string outputHTML = "<html>";
                //outputHTML += "<head> <script type='application/javascript' src='https://securegw.paytm.in/merchantpgpui/checkoutjs/merchants/Teerth23597085988517.js' crossorigin='anonymous' ></script>";
                //outputHTML += "<title>JS Checkout Demo</title>";
                //outputHTML += "</head>";
                //outputHTML += "<body><div id='paytm-checkoutjs'></div>";
                //outputHTML += "<script type='application/javascript' crossorigin='anonymous' src='https://securegw.paytm.in/merchantpgpui/checkoutjs/merchants/Teerth23597085988517.js' onload='onScriptLoad();'></script>";
                //outputHTML += "<script>";
                //outputHTML += "function onScriptLoad(){ if(window.Paytm && window.Paytm.CheckoutJS){ window.Paytm.CheckoutJS.onLoad(function excecuteAfterCompleteLoad() { window.Paytm.CheckoutJS.init(config).then(function onSuccess() {window.Paytm.CheckoutJS.invoke();}).catch(function onError(error){console.log('error => ',error);});});}  } ";






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
    }
}

