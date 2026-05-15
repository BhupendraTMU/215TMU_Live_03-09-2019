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
using WebReference;
public partial class Student_NoDuesResponse : System.Web.UI.Page
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
            SqlCommand command = new SqlCommand("update [OnlinePaymentLog] set GatewayStatus=1 where OrderID='" + Request.Form["ORDERID"].ToString() + "' update HRMSPortal.dbo.tbl_MigrationRequest set Payment_Status = 1 where App_No = '" + dtOrderc.Rows[0]["Temp 1"].ToString() + "'", con);
            command.ExecuteNonQuery();
            pnlSuccess.Visible = true;
        }
        else
        {
            pnlFalure.Visible = true;
        }
        SqlCommand cmd = new SqlCommand("select SC.[Enrollment No_] as Enroll,OP.UserID as 'userid',OP.[Temp 1],No_,[Student Name],SC.No_,OP.Amount,SC.Semester,SC.Year from [TMU$Student - COLLEGE] SC inner join [OnlinePaymentLog] OP on SC.No_=OP.UserID where  OrderID='" + Request.Form["ORDERID"].ToString() + "'", con);
        DataTable dtOrder = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dtOrder);
        con.Close();
        if (dtOrder.Rows.Count > 0)
        {
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

            lblStudentNo.Text = dtOrder.Rows[0]["No_"].ToString();
            lblStudentName.Text = dtOrder.Rows[0]["Student Name"].ToString();

            DataTable dtNAV = new DataTable();
            SqlCommand cmdNAV = new SqlCommand("Proc_GetNAVCreditionalLive", con);
            cmdNAV.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter daNAV = new SqlDataAdapter(cmdNAV);
            daNAV.Fill(dtNAV);
            VoucherPosting nvp = new VoucherPosting();
            nvp.UseDefaultCredentials = true;
            nvp.Url = dtNAV.Rows[0]["URL"].ToString();
            nvp.Credentials = new NetworkCredential(dtNAV.Rows[0]["UserID"].ToString(), dtNAV.Rows[0]["Password"].ToString());
            nvp.MigrationCertificateStudentFee(dtOrder.Rows[0]["No_"].ToString(), Convert.ToDecimal(dtOrder.Rows[0]["Amount"]), dtOrder.Rows[0]["Semester"].ToString(), dtOrder.Rows[0]["Year"].ToString());
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        try
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
        catch(Exception ex)
        {
            Response.Redirect("../Default.aspx");
        }

    }
}
