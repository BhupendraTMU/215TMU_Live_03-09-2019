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
public partial class Faculty_PaYTMSport : System.Web.UI.Page
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

        SqlCommand cmdc = new SqlCommand("Select * from [OnlinePaymentLog] where OrderID='" + Request.Form["ORDERID"].ToString() + "'", con);
        DataTable dtOrderc = new DataTable();
        SqlDataAdapter dac = new SqlDataAdapter(cmdc);
        dac.Fill(dtOrderc);
       
        if (dtOrderc.Rows.Count > 0 && Request.Form["STATUS"].ToString() == "TXN_SUCCESS")
        {
            SqlCommand command = new SqlCommand("update [OnlinePaymentLog] set GatewayStatus=1 where OrderID='" + Request.Form["ORDERID"].ToString() + "'", con);

            command.ExecuteNonQuery();
            pnlSuccess.Visible = true;
        }
        else
        {
            pnlFalure.Visible = true;
        }


        SqlCommand cmd = new SqlCommand("Select *,(Select Sum(GatewayStatus) from [OnlinePaymentLog] where ID<=[OnlinePaymentLog].ID) as 'SRno',UserID as 'Enroll',isnull((select top 1 Name from [TMU$Dimension Value] where Code= ((Select top 1 isnull([Global Dimension 1 Code],'') from [TMU$Employee] where [No_]=[OnlinePaymentLog].UserID))),'') as 'CollegeName' from [OnlinePaymentLog] where OrderID='" + Request.Form["ORDERID"].ToString() + "'", con);
        DataTable dtOrder = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dtOrder);
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

        Session["TXNID"] = Request.Form["TXNID"].ToString();
        lblAgentId.Text = dtOrder.Rows[0]["Enroll"].ToString();
        Session["UserNO"] = dtOrder.Rows[0]["userid"].ToString();
        Session["EntryFlag"] = dtOrder.Rows[0]["Temp 1"].ToString();
        lblCollegeCode.Text = dtOrder.Rows[0]["CollegeName"].ToString();

        con.Open();
        SqlCommand cmd1 = new SqlCommand("select No_,[First Name],[Global Dimension 1 Code],convert(date,[Birth Date]) as 'DOB',[Father Name] from [TMU$Employee] where [No_]='" + Session["UserNO"].ToString() + "'", con);
        DataTable dt = new DataTable();
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        da1.Fill(dt);
        con.Close();


       
        lblStudentNo.Text = dt.Rows[0]["No_"].ToString();
        lblStudentName.Text = dt.Rows[0]["First Name"].ToString();
        lblDob.Text = dt.Rows[0]["DOB"].ToString();
        lblFatherName.Text = dt.Rows[0]["Father Name"].ToString();
        lblCollegeCode.Text = dtOrder.Rows[0]["CollegeName"].ToString();
        srno.Text = "SR NO:-" + dtOrder.Rows[0]["SRno"].ToString();


    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (Session["EntryFlag"].ToString() == "")
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select [No_],[Web Portal Password] as 'Password' from [TMU$Employee] where [No_]='" + Session["UserNO"].ToString() + "'", con);
            DataTable dtOrder = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtOrder);
            con.Close();
            Session["Enroll"] = dtOrder.Rows[0]["No_"].ToString();
            Session["Pass"] = dtOrder.Rows[0]["Password"].ToString();

            Response.Redirect("../Default.aspx");
        }
        else
        {
            Response.Redirect("../OnlineFeePayment.aspx");
        }
    }
}