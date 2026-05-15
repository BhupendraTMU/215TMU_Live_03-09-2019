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

public partial class Student_PaytmTransactionStatus : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    StudentDetailsViewDL SDL = new StudentDetailsViewDL();
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
        catch (Exception ex)
        {
            Response.Redirect("PaytmErrorPage.aspx");
        }
    }
    public void show()
    {
        String masterKey = "7v_qN#jfvvCiLSOB";
       
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
        //parameters.Add("EXTENDINFO", Request.Form["EXTENDINFO"]);
        //parameters.Add("MERCUNQREF", Request.Form["MERCUNQREF"]);


        string str = Request.Form["CHECKSUMHASH"];

        if (CheckSum.verifyCheckSum(masterKey, parameters, str))
        {

            //  Response.Redirect("tarnsactstatus.aspx");

        }
        else
        {
            //Response.Redirect("maintenence.aspx");
        }
        con.Open();


        SqlCommand cmd = new SqlCommand("Select *,(Select isnull([Enrollment No_],'') from [TMU$Student - COLLEGE] where [No_]=[TMU$Online Payment Log].UserID) as 'Enroll' from [TMU$Online Payment Log] where OrderID='" + Request.Form["ORDERID"].ToString() + "'", con);
        DataTable dtOrder = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dtOrder);
        if (dtOrder.Rows.Count > 0 && Request.Form["STATUS"].ToString() == "TXN_SUCCESS")
        {
            SqlCommand command = new SqlCommand("update [TMU$Online Payment Log] set GatewayStatus=1 where OrderID='" + Request.Form["ORDERID"].ToString() + "'", con);

            command.ExecuteNonQuery();
            pnlSuccess.Visible = true;
        }
        else
        {
            pnlFalure.Visible = true;
        }
        con.Close();
        lblStatus.Text = Request.Form["STATUS"].ToString();
        lblStatusCode.Text = Request.Form["RESPCODE"].ToString();
        lblOrderid.Text = Request.Form["ORDERID"].ToString();
        lblAmount.Text = Request.Form["TXNAMOUNT"].ToString();
        lblPaymentDate.Text = Request.Form["TXNDATE"].ToString();
        if (Request.Form["BANKNAME"] == null)
        {
            lblBankName.Text = "UPI";
        }
        else
        {
            lblBankName.Text = Request.Form["BANKNAME"].ToString();
        }
        lblPaymentMode.Text = Request.Form["PAYMENTMODE"].ToString();
        lbltxtid.Text = Request.Form["TXNID"].ToString();
        Session["TXNID"] = Request.Form["TXNID"].ToString();
        lblAgentId.Text = dtOrder.Rows[0]["Enroll"].ToString();
        Session["UserNO"] = dtOrder.Rows[0]["userid"].ToString();
        Session["EntryFlag"] = dtOrder.Rows[0]["Temp 1"].ToString();
        //====================== insert into TMU$Gen_ Journal Line===========================
        DataTable dt = SDL.GetStudentDetailsForSurl(dtOrder.Rows[0]["userid"].ToString());
        lblStudentNo.Text = dt.Rows[0]["No_"].ToString();
        lblStudentName.Text = dt.Rows[0]["Student Name"].ToString();
        lblDob.Text = dt.Rows[0]["DOB"].ToString();
        lblFatherName.Text = dt.Rows[0]["Fathers Name"].ToString();
        //DataTable dt1 = new DataTable();
        //DataTable dt2 = new DataTable();
        //dt1 = (DataTable)Session["AmountDetails"];
        //dt2 = (DataTable)Session["AccountNo"];
        //if (dt1.Rows.Count > 0)
        //{
        //    for (int i = 0; i < dt1.Rows.Count; i++)
        //    {
        //        //SDL.insertGen_JournalLine(Session["uid"].ToString(), Convert.ToDecimal(dt1.Rows[i]["Amount"].ToString()), dt1.Rows[i]["Description"].ToString(), dt2.Rows[i]["G_L Account"].ToString(), lblOrderid.Text);// comment by ashu
        //        SDL.insertGen_JournalLine(Session["uid"].ToString(), Convert.ToDecimal(dt1.Rows[i]["Amount"].ToString()), dt1.Rows[i]["Description"].ToString(), dt2.Rows[i]["G_L Account"].ToString(), lblOrderid.Text, dt1.Rows[i]["Entry No"].ToString(), "PAYTM");//ashu add Entry No and PAYTM 28-07-2016
        //    }
        //}
       
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (Session["EntryFlag"].ToString() == "")
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select [Enrollment No_],[Password] from [TMU$Student - COLLEGE] where [No_]='" + Session["UserNO"].ToString() + "'", con);
            DataTable dtOrder = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtOrder);
            con.Close();
            Session["Enroll"] = dtOrder.Rows[0]["Enrollment No_"].ToString();
            Session["Pass"] = dtOrder.Rows[0]["Password"].ToString();

            Response.Redirect("../Default.aspx");
        }
        else
        {
            Response.Redirect("../OnlineFeePayment.aspx");
        }
    }
}