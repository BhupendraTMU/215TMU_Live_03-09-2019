
using DL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

public partial class Student_ResponsePage : System.Web.UI.Page
{
    StudentDetailsViewDL SDL = new StudentDetailsViewDL();

    private readonly string ConnectionString =
        ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;

    private const string MerchantKey = "bGxnW3";
    private const string Salt = "zssT9zGfAsLwvoFzYLbC0ndS0nVbaIGn";


    // ============================================================
    // PAGE LOAD
    // ============================================================

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProcessPayUResponse();
        }
    }


    // ============================================================
    // PAYU RESPONSE
    // ============================================================

    private void ProcessPayUResponse()
    {
        try
        {
            string status =
                Convert.ToString(Request.Form["status"]);

            string txnid =
                Convert.ToString(Request.Form["txnid"]);

            string amount =
                Convert.ToString(Request.Form["amount"]);

            string productinfo =
                Convert.ToString(Request.Form["productinfo"]);

            string firstname =
                Convert.ToString(Request.Form["firstname"]);

            string email =
                Convert.ToString(Request.Form["email"]);

            string receivedHash =
                Convert.ToString(Request.Form["hash"]);

            string mihpayid =
                Convert.ToString(Request.Form["mihpayid"]);

            string bankCode =
                Convert.ToString(Request.Form["bankcode"]);

            string mode =
                Convert.ToString(Request.Form["mode"]);

            string bankRefNum =
                Convert.ToString(Request.Form["bank_ref_num"]);

            string error =
                Convert.ToString(Request.Form["error"]);

            string errorMessage =
                Convert.ToString(Request.Form["error_Message"]);


            // ----------------------------------------------------
            // Transaction ID check
            // ----------------------------------------------------

            if (string.IsNullOrWhiteSpace(txnid))
            {
                ShowFailure("Transaction ID is missing.");
                return;
            }


            // ----------------------------------------------------
            // Get Order
            // ----------------------------------------------------

            DataTable dtOrder =
                GetOrder(txnid);

            if (dtOrder == null ||
                dtOrder.Rows.Count == 0)
            {
                ShowFailure(
                    "Payment response received, but order record was not found."
                );

                return;
            }


            // IMPORTANT:
            // Row AFTER Fill()
            DataRow row =
                dtOrder.Rows[0];


            string dbAmount =
                Convert.ToString(row["Amount"]);


            // ----------------------------------------------------
            // HASH VERIFICATION
            // ----------------------------------------------------

            string reverseHashString =
                Salt + "|" +
                status + "|||||||||||" +
                email + "|" +
                firstname + "|" +
                productinfo + "|" +
                amount + "|" +
                txnid + "|" +
                MerchantKey;


            string calculatedHash =
                GenerateSHA512(reverseHashString);


            if (string.IsNullOrWhiteSpace(receivedHash) ||
                !string.Equals(
                    calculatedHash,
                    receivedHash,
                    StringComparison.OrdinalIgnoreCase))
            {
                LogError(
                    "PayU Hash mismatch. TxnID = " + txnid
                );

                ShowFailure(
                    "Payment response verification failed."
                );

                return;
            }


            // ----------------------------------------------------
            // AMOUNT VERIFICATION
            // ----------------------------------------------------

            decimal payuAmount = 0;
            decimal databaseAmount = 0;

            decimal.TryParse(
                amount,
                out payuAmount
            );

            decimal.TryParse(
                dbAmount,
                out databaseAmount
            );


            if (payuAmount != databaseAmount)
            {
                LogError(
                    "Amount mismatch. TxnID=" + txnid +
                    " PayU=" + amount +
                    " DB=" + dbAmount
                );

                ShowFailure(
                    "Payment amount verification failed."
                );

                return;
            }


            // ====================================================
            // SUCCESS
            // ====================================================

            if (string.Equals(
                status,
                "success",
                StringComparison.OrdinalIgnoreCase))
            {
                ProcessSuccessfulPayment(
                    row,
                    txnid,
                    amount,
                    mihpayid,
                    bankCode,
                    mode,
                    bankRefNum
                );

                return;
            }


            // ====================================================
            // FAILURE
            // ====================================================

            ProcessFailedPayment(
                txnid,
                error,
                errorMessage
            );
        }
        catch (Exception ex)
        {
            LogError(
                "ProcessPayUResponse : " +
                ex.ToString()
            );

            ShowFailure(
                "Payment response processing failed. Please contact support."
            );
        }
    }


    // ============================================================
    // GET ORDER
    // ============================================================

    private DataTable GetOrder(string orderId)
    {
        DataTable dt =
            new DataTable();

        using (SqlConnection con =
            new SqlConnection(ConnectionString))
        {
            con.Open();

            string query = @"
                SELECT
                    OP.*,

                    (
                        SELECT SUM(ISNULL(GatewayStatus, 0))
                        FROM [OnlinePaymentLog]
                        WHERE ID <= OP.ID
                    ) AS SRno,

                    (
                        SELECT ISNULL([Enrollment No_], '')
                        FROM [TMU$Student - COLLEGE]
                        WHERE [No_] = OP.UserID
                    ) AS Enroll,

                    (
                        SELECT Name
                        FROM [TMU$Dimension Value]
                        WHERE Code =
                        (
                            SELECT ISNULL([Global Dimension 1 Code], '')
                            FROM [TMU$Student - COLLEGE]
                            WHERE [No_] = OP.UserID
                        )
                    ) AS CollegeName

                FROM [OnlinePaymentLog] OP

                WHERE OP.OrderID = @OrderID";


            using (SqlCommand cmd =
                new SqlCommand(query, con))
            {
                cmd.Parameters.Add(
                    "@OrderID",
                    SqlDbType.NVarChar,
                    100
                ).Value = orderId;


                using (SqlDataAdapter da =
                    new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
        }

        return dt;
    }


    // ============================================================
    // SUCCESS
    // ============================================================

    private void ProcessSuccessfulPayment(
        DataRow row,
        string txnid,
        string amount,
        string mihpayid,
        string bankCode,
        string mode,
        string bankRefNum)
    {
        try
        {
            string userNo =
                Convert.ToString(row["UserID"]);


            if (string.IsNullOrWhiteSpace(userNo))
            {
                ShowFailure(
                    "Payment successful but student number is missing."
                );

                return;
            }


            // ----------------------------------------------------
            // UPDATE PAYMENT LOG
            // ----------------------------------------------------

            using (SqlConnection con =
                new SqlConnection(ConnectionString))
            {
                con.Open();

                string updateQuery = @"
                    UPDATE [OnlinePaymentLog]
                    SET
                        GatewayStatus = 1,
                        Status = 1
                    WHERE OrderID = @OrderID";


                using (SqlCommand cmd =
                    new SqlCommand(updateQuery, con))
                {
                    cmd.Parameters.Add(
                        "@OrderID",
                        SqlDbType.NVarChar,
                        100
                    ).Value = txnid;

                    cmd.ExecuteNonQuery();
                }
            }


            // ----------------------------------------------------
            // GET STUDENT
            // ----------------------------------------------------

            DataTable dtStudent =
                SDL.GetStudentDetailsForSurl(userNo);


            if (dtStudent == null ||
                dtStudent.Rows.Count == 0)
            {
                ShowFailure(
                    "Payment successful but student details were not found."
                );

                return;
            }


            DataRow studentRow =
                dtStudent.Rows[0];


            // ====================================================
            // SHOW RECEIPT
            // ====================================================

            pnlSuccess.Visible = true;
            pnlFalure.Visible = false;


            // ----------------------------------------------------
            // STATUS
            // ----------------------------------------------------

            lblStatustext.Text =
                "✓ Payment Successful";

            lblStatus.Text =
                "SUCCESS";


            // ----------------------------------------------------
            // RECEIPT / SR NO
            // ----------------------------------------------------

            //srno.Text =
            //    "Receipt No. : " +
            //    Convert.ToString(row["SRno"]);


            // ----------------------------------------------------
            // STUDENT DETAILS
            // ----------------------------------------------------

            lblStudentNo.Text =
                Convert.ToString(
                    studentRow["No_"]
                );


            lblStudentName.Text =
                Convert.ToString(
                    studentRow["Student Name"]
                );


            lblDob.Text =
                Convert.ToString(
                    studentRow["DOB"]
                );


            lblFatherName.Text =
                Convert.ToString(
                    studentRow["Fathers Name"]
                );


            lblAgentId.Text =
                Convert.ToString(
                    row["Enroll"]
                );


            lblCollegeCode.Text =
                Convert.ToString(
                    row["CollegeName"]
                );


            // ----------------------------------------------------
            // PAYMENT DETAILS
            // ----------------------------------------------------

            lblAmount.Text =
                Convert.ToDecimal(amount)
                    .ToString("0.00");


            lblOrderid.Text =
                txnid;


            if (!string.IsNullOrWhiteSpace(mode))
            {
                lblPaymentMode.Text =
                    mode.ToUpper();
            }
            else
            {
                lblPaymentMode.Text =
                    "ONLINE";
            }


            lblUTRNo.Text =
                bankRefNum;


            lblPaymentDate.Text =
                DateTime.Now.ToString(
                    "dd-MM-yyyy hh:mm:ss tt"
                );


            // ----------------------------------------------------
            // MESSAGE
            // ----------------------------------------------------

            lblValidate.Text =
                "Your payment has been successfully received.";


            // ----------------------------------------------------
            // SESSION
            // ----------------------------------------------------

            Session["UserNO"] =
                userNo;


            Session["EntryFlag"] =
                Convert.ToString(
                    row["Temp 1"]
                );


            Session["Name"] =
                Convert.ToString(
                    row["Temp 3"]
                );


            // ----------------------------------------------------
            // STUDENT LOGIN SESSION
            // ----------------------------------------------------

            SetStudentLoginSession(userNo);
        }
        catch (Exception ex)
        {
            LogError(
                "ProcessSuccessfulPayment : " +
                ex.ToString()
            );

            ShowFailure(
                "Payment was successful but receipt generation failed."
            );
        }
    }


    // ============================================================
    // FAILED PAYMENT
    // ============================================================

    private void ProcessFailedPayment(
        string txnid,
        string error,
        string errorMessage)
    {
        try
        {
            string message =
                errorMessage;


            if (string.IsNullOrWhiteSpace(message))
                message = error;


            if (string.IsNullOrWhiteSpace(message))
                message =
                    "Payment transaction failed.";


            // ----------------------------------------------------
            // UPDATE FAILED TRANSACTION
            // ----------------------------------------------------

            using (SqlConnection con =
                new SqlConnection(ConnectionString))
            {
                con.Open();

                string query = @"
                    UPDATE [OnlinePaymentLog]
                    SET
                        GatewayStatus = 0,
                        Status = 0
                    WHERE OrderID = @OrderID";


                using (SqlCommand cmd =
                    new SqlCommand(query, con))
                {
                    cmd.Parameters.Add(
                        "@OrderID",
                        SqlDbType.NVarChar,
                        100
                    ).Value = txnid;


                    cmd.ExecuteNonQuery();
                }
            }


            pnlSuccess.Visible = false;
            pnlFalure.Visible = true;


            lblstatusfauluere.Text =
                "✗ Payment Failed";


            lblStatus.Text =
                "FAILED";


            lblOrderid.Text =
                txnid;


            lblValidate.Text =
                "Transaction Failed : " +
                message;
        }
        catch (Exception ex)
        {
            LogError(
                "ProcessFailedPayment : " +
                ex.ToString()
            );

            ShowFailure(
                "Transaction failed."
            );
        }
    }


    // ============================================================
    // SHOW FAILURE
    // ============================================================

    private void ShowFailure(string message)
    {
        pnlSuccess.Visible = false;
        pnlFalure.Visible = true;


        lblstatusfauluere.Text =
            "✗ Payment Failed";


        lblStatus.Text =
            "FAILED";


        lblValidate.Text =
            message;
    }


    // ============================================================
    // STUDENT LOGIN SESSION
    // ============================================================

    private void SetStudentLoginSession(string userNo)
    {
        DataTable dt =
            new DataTable();


        using (SqlConnection con =
            new SqlConnection(ConnectionString))
        {
            con.Open();


            string query = @"
                SELECT
                    [Enrollment No_],
                    [Password],
                    [No_],[Course Code],Semester,[Global Dimension 1 Code],
[Phone Number],[Admitted Year]
                FROM [TMU$Student - COLLEGE]
                WHERE [No_] = @UserNo";


            using (SqlCommand cmd =
                new SqlCommand(query, con))
            {
                cmd.Parameters.Add(
                    "@UserNo",
                    SqlDbType.NVarChar,
                    50
                ).Value = userNo;


                using (SqlDataAdapter da =
                    new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
        }


        if (dt.Rows.Count > 0)
        {
            DataRow row =
              dt.Rows[0];
            lblAdmitted.Text= Convert.ToString(
                    row["Admitted Year"]
                );
            lblContact.Text = Convert.ToString(
                      row["Phone Number"]
                  );

            Session["College"] =
                Convert.ToString(
                    row["Global Dimension 1 Code"]
                );
            Session["Semester"] =
                Convert.ToString(
                    row["Semester"]
                );
            Session["CourseCode"] =
                Convert.ToString(
                    row["Course Code"]
                );

            Session["Enroll"] =
                Convert.ToString(
                    row["Enrollment No_"]
                );


            Session["Passw"] =
                Convert.ToString(
                    row["Password"]
                );


            Session["Pass"] =
                Convert.ToString(
                    row["Password"]
                );


            Session["uid"] =
                Convert.ToString(
                    row["No_"]
                );
        }
    }


    // ============================================================
    // SHA512
    // ============================================================

    private string GenerateSHA512(string input)
    {
        using (SHA512 sha512 =
            SHA512.Create())
        {
            byte[] bytes =
                Encoding.UTF8.GetBytes(input);


            byte[] hash =
                sha512.ComputeHash(bytes);


            StringBuilder sb =
                new StringBuilder();


            for (int i = 0;
                 i < hash.Length;
                 i++)
            {
                sb.Append(
                    hash[i].ToString("x2")
                );
            }


            return sb.ToString();
        }
    }


    // ============================================================
    // OK BUTTON
    // ============================================================

    protected void btnOk_Click(object sender, EventArgs e)
    {
        try
        {
            string entryFlag =
                Convert.ToString(
                    Session["EntryFlag"]
                );


            string userNo =
                Convert.ToString(
                    Session["UserNO"]
                );


            if (string.IsNullOrWhiteSpace(userNo))
            {
                Response.Redirect(
                    "../Default.aspx",
                    false
                );

                Context.ApplicationInstance
                    .CompleteRequest();

                return;
            }


            if (string.IsNullOrWhiteSpace(entryFlag))
            {
                Response.Redirect(
                    "../Default.aspx",
                    false
                );
            }
            else
            {
                Response.Redirect(
                    "../OnlineFeePayment.aspx",
                    false
                );
            }


            Context.ApplicationInstance
                .CompleteRequest();
        }
        catch (Exception ex)
        {
            LogError(
                "btnOk_Click : " +
                ex.ToString()
            );

            lblValidate.Text =
                "Unable to continue. Please try again.";
        }
    }


    // ============================================================
    // ERROR LOG
    // ============================================================

    private void LogError(string message)
    {
        System.Diagnostics.Trace.WriteLine(
            "Student_ResponsePage : " +
            message
        );
    }
}

