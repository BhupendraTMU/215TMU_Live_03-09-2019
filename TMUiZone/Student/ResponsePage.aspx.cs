using DotNetIntegrationKit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DL;
public partial class Student_ResponsePage : System.Web.UI.Page
{
    StudentDetailsViewDL SDL = new StudentDetailsViewDL();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    string strHEX, strPGActualReponseWithChecksum, strPGActualReponseEncrypted, strPGActualReponseDecrypted, strPGresponseChecksum, strPGTxnStatusCode;
    //string strPGActualReponse="status=0300|amount=125.00|hash=3243453454353453";
    //string strPGActualReponse=PGResponse,strPGTxnStatusCode;
    string[] strPGChecksum, strPGTxnString;
    bool isDecryptable = false;

    string strPG_TxnStatus = string.Empty,
    strPG_ClintTxnRefNo = string.Empty,
    strPG_TPSLTxnBankCode = string.Empty,
    strPG_TPSLTxnID = string.Empty,
    strPG_TxnAmount = string.Empty,
    strPG_TxnDateTime = string.Empty,
    strPG_TxnDate = string.Empty,
    strPG_TxnTime = string.Empty,
    Customername = string.Empty;
    string strPGResponse;
    string[] strSplitDecryptedResponse;
    string[] strArrPG_TxnDateTime;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                show();
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../Default.aspx");
        }
    }
    public void show()
    {

        try
        {
            strPGResponse = Request["msg"].ToString();  //Reading response of PG
            Label LBL_DisplayResult = new Label();
            Label lblResponseDecrypted = new Label();
            Label lblValidate = new Label();

            if (strPGResponse != "" || strPGResponse != null)
            {
                LBL_DisplayResult.Text = "Response :: " + strPGResponse;

                RequestURL objRequestURL = new RequestURL();    //Creating Object of Class DotNetIntegration_1_1.RequestURL
                string strDecryptedVal = null;                  //Decrypting the PG response

                if (!String.IsNullOrEmpty(Convert.ToString(Session["PropertyFile"])))
                {
                    string strFilePath = ConfigurationSettings.AppSettings["FilePath"];
                    string[] FilePath = strFilePath.Split('\\');
                    string MerchantCode = Convert.ToString(Session["Merchant_Code"]);


                    strDecryptedVal = objRequestURL.VerifyPGResponse(strPGResponse, strFilePath);
                }
                else
                {
                    string strIsKey = Convert.ToString("1025180466GPADTD");
                    string strIsIv = Convert.ToString("3232693456UWOWHC");

                    strDecryptedVal = objRequestURL.VerifyPGResponse(strPGResponse, strIsKey, strIsIv);
                }
                lblResponseDecrypted.Text = strDecryptedVal;

                if (strDecryptedVal.StartsWith("ERROR"))
                {
                    lblValidate.Text = strDecryptedVal;
                }
                else
                {
                    strSplitDecryptedResponse = strDecryptedVal.Split('|');
                    GetPGRespnseData(strSplitDecryptedResponse);

                    if (strPG_TxnStatus == "0300")
                    {
                        //strPG_ClintTxnRefNo
                        con.Open();


                        SqlCommand cmdc = new SqlCommand("Select * from [OnlinePaymentLog] where OrderID='" + strPG_ClintTxnRefNo + "'", con);
                        DataTable dtOrderc = new DataTable();
                        SqlDataAdapter dac = new SqlDataAdapter(cmdc);
                        dac.Fill(dtOrderc);
                        if (dtOrderc.Rows.Count > 0 )
                        {
                            SqlCommand command = new SqlCommand("update [OnlinePaymentLog] set GatewayStatus=1 where OrderID='" + strPG_ClintTxnRefNo + "'", con);

                            command.ExecuteNonQuery();
                            pnlSuccess.Visible = true;
                        }
                        else
                        {
                            pnlFalure.Visible = true;
                        }
                        SqlCommand cmd = new SqlCommand("Select *,(Select Sum(GatewayStatus) from [OnlinePaymentLog] where ID<=[OnlinePaymentLog].ID) as 'SRno',(Select isnull([Enrollment No_],'') from [TMU$Student - COLLEGE] where [No_]=[OnlinePaymentLog].UserID) as 'Enroll',(select Name from [TMU$Dimension Value] where Code= ((Select isnull([Global Dimension 1 Code],'') from [TMU$Student - COLLEGE] where [No_]=[OnlinePaymentLog].UserID))) as 'CollegeName' from [OnlinePaymentLog] where OrderID='" + strPG_ClintTxnRefNo + "'", con);
                        DataTable dtOrder = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtOrder);
                        con.Close();
                        lblStatus.Text = strPG_TxnStatus;
                        //lblStatusCode.Text = Request.Form["RESPCODE"].ToString();
                        //lblOrderid.Text = Request.Form["ORDERID"].ToString();
                        //lblAmount.Text = Request.Form["TXNAMOUNT"].ToString();
                        //lblPaymentDate.Text = Request.Form["TXNDATE"].ToString();
                        //if (Request.Form["BANKNAME"] == null)
                        //{
                        //    lblBankName.Text = "UPI";
                        //}
                        //else
                        //{
                        //    lblBankName.Text = Request.Form["BANKNAME"].ToString();
                        //}
                        //lblPaymentMode.Text = Request.Form["PAYMENTMODE"].ToString();

                        //Session["TXNID"] = Request.Form["TXNID"].ToString();
                        lblAgentId.Text = dtOrder.Rows[0]["Enroll"].ToString();
                        Session["UserNO"] = dtOrder.Rows[0]["userid"].ToString();
                        Session["EntryFlag"] = dtOrder.Rows[0]["Temp 1"].ToString();
                        DataTable dt = SDL.GetStudentDetailsForSurl(dtOrder.Rows[0]["UserID"].ToString());
                        lblStudentNo.Text = dt.Rows[0]["No_"].ToString();
                        lblStudentName.Text = dt.Rows[0]["Student Name"].ToString();
                        lblDob.Text = dt.Rows[0]["DOB"].ToString();
                        lblFatherName.Text = dt.Rows[0]["Fathers Name"].ToString();
                        lblCollegeCode.Text = dtOrder.Rows[0]["CollegeName"].ToString();
                        srno.Text = "SR NO:-" + dtOrder.Rows[0]["SRno"].ToString();
                        Session["Name"]= dtOrder.Rows[0]["Temp 3"].ToString();
                        lblValidate.Text = "Transaction Success " + strPGTxnStatusCode;

                        con.Open();
                        SqlCommand cmd1 = new SqlCommand("select [Enrollment No_],[Password],No_ from [TMU$Student - COLLEGE] where [No_]='" + Session["UserNO"].ToString() + "'", con);
                        DataTable dtOrder1 = new DataTable();
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                        da1.Fill(dtOrder1);
                        con.Close();
                        Session["Enroll"] = dtOrder1.Rows[0]["Enrollment No_"].ToString();

                        Session["Passw"] = dtOrder1.Rows[0]["Password"].ToString();
                        Session["uid"]= dtOrder1.Rows[0]["No_"].ToString();
                        //Response.Redirect("~/Default.aspx");
                    }
                    else if (strPG_TxnStatus == "0200")
                    {
                        lblValidate.Text = "Transaction Success " + strPGTxnStatusCode;
                    }
                    else
                    {
                        strPGTxnString = strSplitDecryptedResponse[2].Split('=');
                        lblValidate.Text = "Transaction Fail " + "ERROR:" + strPGTxnString[1];
                    }
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/Default.aspx");
        }


    }
    public void GetPGRespnseData(string[] parameters)
    {

        string[] strGetMerchantParamForCompare;

        for (int i = 0; i < parameters.Length; i++)
        {
            strGetMerchantParamForCompare = parameters[i].ToString().Split('=');
            if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TXN_STATUS")
            {
                strPG_TxnStatus = Convert.ToString(strGetMerchantParamForCompare[1]);
            }
            else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "CLNT_TXN_REF")
            {
                strPG_ClintTxnRefNo = Convert.ToString(strGetMerchantParamForCompare[1]);
            }
            else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TPSL_BANK_CD")
            {
                strPG_TPSLTxnBankCode = Convert.ToString(strGetMerchantParamForCompare[1]);
            }
            else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TPSL_TXN_ID")
            {
                strPG_TPSLTxnID = Convert.ToString(strGetMerchantParamForCompare[1]);
            }
            else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TXN_AMT")
            {
                strPG_TxnAmount = Convert.ToString(strGetMerchantParamForCompare[1]);
            }
            else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TPSL_TXN_TIME")
            {
                strPG_TxnDateTime = Convert.ToString(strGetMerchantParamForCompare[1]);
                strArrPG_TxnDateTime = strPG_TxnDateTime.Split(' ');
                strPG_TxnDate = Convert.ToString(strArrPG_TxnDateTime[0]);
                strPG_TxnTime = Convert.ToString(strArrPG_TxnDateTime[1]);
            }
            else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "clnt_rqst_meta")
            {
                Customername = Convert.ToString(strGetMerchantParamForCompare[1]);
            }
        }
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