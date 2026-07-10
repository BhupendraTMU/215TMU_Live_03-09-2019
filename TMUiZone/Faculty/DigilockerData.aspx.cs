using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;


public partial class Faculty_DigilockerData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }
    private void BindGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(constr))
        {
            string query = "";
            if (Session["uid"].ToString() == "TMU04870")
            {
                query = @"
        SELECT ID,
               INVALID_COLUMNS,
               ORG_NAME,
               ACADEMIC_COURSE_ID,
               COURSE_NAME,
               STREAM,
               SESSION,
               REGN_NO,
               RROLL,
               CNAME,
               GENDER,
               DOB,
               FNAME,
               MNAME,
               UploadDate,
               collegeCode,
               ispost,
               UploadBy,
               IsCorrected
        FROM StudentABCErrorData
        WHERE 1=1";

                if (!string.IsNullOrWhiteSpace(txtCollegeCode.Text))
                    query += " AND collegeCode = @CollegeCode";

                if (!string.IsNullOrWhiteSpace(txtEnrollmentNo.Text))
                    query += " AND REGN_NO = @EnrollmentNo";

                if (!string.IsNullOrWhiteSpace(ddlIsCorrected.SelectedValue))
                    query += " AND IsCorrected = @IsCorrected";

                query += " ORDER BY ID ASC";
            }
            else
            {
                query = @"
        SELECT ID,
               INVALID_COLUMNS,
               ORG_NAME,
               ACADEMIC_COURSE_ID,
               COURSE_NAME,
               STREAM,
               SESSION,
               REGN_NO,
               RROLL,
               CNAME,
               GENDER,
               DOB,
               FNAME,
               MNAME,
               UploadDate,
               collegeCode,
               ispost,
               UploadBy,
               IsCorrected
        FROM StudentABCErrorData
        WHERE collegeCode COLLATE Latin1_General_100_CS_AS IN
        (
            SELECT DISTINCT [Global Dimenison 1 Code]
            FROM [EDUCOLLEGELIVE-R2].dbo.[TMU$User Role Matrix]
            WHERE Principal = @UID
        )";

                if (!string.IsNullOrWhiteSpace(txtCollegeCode.Text))
                    query += " AND collegeCode = @CollegeCode";

                if (!string.IsNullOrWhiteSpace(txtEnrollmentNo.Text))
                    query += " AND REGN_NO = @EnrollmentNo";

                if (!string.IsNullOrWhiteSpace(ddlIsCorrected.SelectedValue))
                    query += " AND IsCorrected = @IsCorrected";

                query += " ORDER BY ID ASC";
            }
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@UID", Session["uid"].ToString());

            if (!string.IsNullOrWhiteSpace(txtCollegeCode.Text))
                cmd.Parameters.AddWithValue("@CollegeCode", txtCollegeCode.Text.Trim());

            if (!string.IsNullOrWhiteSpace(txtEnrollmentNo.Text))
                cmd.Parameters.AddWithValue("@EnrollmentNo", txtEnrollmentNo.Text.Trim());

            if (!string.IsNullOrWhiteSpace(ddlIsCorrected.SelectedValue))
                cmd.Parameters.AddWithValue("@IsCorrected", ddlIsCorrected.SelectedValue);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (Session["uid"].ToString() == "TMU04870")
            {
                gvStudentDataUpdate.Visible = true;
                gvStudentData.Visible = false;
                gvStudentDataUpdate.DataSource = dt;
                gvStudentDataUpdate.DataBind();
            }
            else
            {
                gvStudentDataUpdate.Visible = false;
                gvStudentData.Visible = true;
                gvStudentData.DataSource = dt;
                gvStudentData.DataBind();
            }

            ViewState["ExportData"] = dt;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = ViewState["ExportData"] as DataTable;

        if (dt == null || dt.Rows.Count == 0)
            return;

        GridView gvExport = new GridView();
        gvExport.DataSource = dt;
        gvExport.DataBind();

        Response.Clear();
        Response.Buffer = true;

        Response.AddHeader(
            "content-disposition",
            "attachment;filename=StudentABCErrorData_" +
            DateTime.Now.ToString("ddMMyyyyHHmmss") +
            ".xls");

        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            gvExport.RenderControl(hw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    protected void gvStudentDataUpdate_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "UpdateRecord")
        {
          

          

            int rowIndex = ((GridViewRow)((Button)e.CommandSource).NamingContainer).RowIndex;

            string id = e.CommandArgument.ToString();
            hfID.Value = gvStudentDataUpdate.DataKeys[rowIndex].Value.ToString();
            DropDownList ddlIncorrectEntry =
                (DropDownList)gvStudentDataUpdate.Rows[rowIndex].FindControl("ddlIncorrectEntry");

            TextBox txtCorrectedValue =
                (TextBox)gvStudentDataUpdate.Rows[rowIndex].FindControl("txtCorrectedValue");

            string columnName = ddlIncorrectEntry.SelectedValue;
            string correctedValue = txtCorrectedValue.Text.Trim();

            if (string.IsNullOrEmpty(columnName))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                    "alert('Please select Incorrect Entry.');", true);
                return;
            }

            string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {

                if (columnName == "CNAME")
                {

                    string oldValue = "";

                    string query = "select [Student Name] from [TMU$Student - COLLEGE] where [Enrollment No_]=@ID";


                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();

                    object obj = cmd.ExecuteScalar();

                    if (obj != null)
                        oldValue = obj.ToString();

                    lblEnroll.Text = id;
                    lblPopupColumn.Text = "Student Name";
                    lblOldValue.Text = oldValue;
                    lblNewValue.Text = correctedValue;


                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "Popup",
                        "$('#divConfirmUpdate').modal('show');",
                        true);
                }

                if (columnName == "FNAME")
                {
                    string oldValue = "";

                    string query = "select [Fathers Name] from [TMU$Student - COLLEGE] where [Enrollment No_]=@ID";


                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();

                    object obj = cmd.ExecuteScalar();

                    if (obj != null)
                        oldValue = obj.ToString();

                    lblEnroll.Text = id;
                    lblPopupColumn.Text = "Father Name";
                    lblOldValue.Text = oldValue;
                    lblNewValue.Text = correctedValue;



                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "Popup",
                        "$('#divConfirmUpdate').modal('show');",
                        true);
                }
                if (columnName == "MNAME")
                {
                    string oldValue = "";

                    string query = "select [Mothers Name] from [TMU$Student - COLLEGE] where [Enrollment No_]=@ID";


                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();

                    object obj = cmd.ExecuteScalar();

                    if (obj != null)
                        oldValue = obj.ToString();

                    lblEnroll.Text = id;
                    lblPopupColumn.Text = "Mother Name";
                    lblOldValue.Text = oldValue;
                    lblNewValue.Text = correctedValue;



                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "Popup",
                        "$('#divConfirmUpdate').modal('show');",
                        true);
                }
                if (columnName == "CONTACTNO")
                {
                    string oldValue = "";

                    string query = "select [Mobile Number] from [TMU$Student - COLLEGE] where [Enrollment No_]=@ID";


                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();

                    object obj = cmd.ExecuteScalar();

                    if (obj != null)
                        oldValue = obj.ToString();

                    lblEnroll.Text = id;
                    lblPopupColumn.Text = "Contact Number";
                    lblOldValue.Text = oldValue;
                    lblNewValue.Text = correctedValue;



                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "Popup",
                        "$('#divConfirmUpdate').modal('show');",
                        true);
                }
                if (columnName == "EMAILID")
                {
                    string oldValue = "";

                    string query = "select [E-Mail Address] from [TMU$Student - COLLEGE] where [Enrollment No_]=@ID";


                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();

                    object obj = cmd.ExecuteScalar();

                    if (obj != null)
                        oldValue = obj.ToString();

                    lblEnroll.Text = id;
                    lblPopupColumn.Text = "E-Mail";
                    lblOldValue.Text = oldValue;
                    lblNewValue.Text = correctedValue;



                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "Popup",
                        "$('#divConfirmUpdate').modal('show');",
                        true);
                }
                if (columnName == "ABCID")
                {
                    string oldValue = "";

                    string query = "select [Visa No_] from [TMU$Student - COLLEGE] where [Enrollment No_]=@ID";


                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();

                    object obj = cmd.ExecuteScalar();

                    if (obj != null)
                        oldValue = obj.ToString();

                    lblEnroll.Text = id;
                    lblPopupColumn.Text = "ABC ID";
                    lblOldValue.Text = oldValue;
                    lblNewValue.Text = correctedValue;


                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "Popup",
                        "$('#divConfirmUpdate').modal('show');",
                        true);
                }
                if (columnName == "Address")
                {
                    string oldValue = "";

                    string query = "select [Address1] from [TMU$Student - COLLEGE] where [Enrollment No_]=@ID";


                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();

                    object obj = cmd.ExecuteScalar();

                    if (obj != null)
                        oldValue = obj.ToString();

                    lblEnroll.Text = id;
                    lblPopupColumn.Text = "Address";
                    lblOldValue.Text = oldValue;
                    lblNewValue.Text = correctedValue;



                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "Popup",
                        "$('#divConfirmUpdate').modal('show');",
                        true);
                }
                if (columnName == "AdharNumber")
                {
                    string oldValue = "";

                    string query = "select [Aadhar No_] from [TMU$Student - COLLEGE] where [Enrollment No_]=@ID";


                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();

                    object obj = cmd.ExecuteScalar();

                    if (obj != null)
                        oldValue = obj.ToString();

                    lblEnroll.Text = id;
                    lblPopupColumn.Text = "AdharNumber";
                    lblOldValue.Text = oldValue;
                    lblNewValue.Text = correctedValue;



                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "Popup",
                        "$('#divConfirmUpdate').modal('show');",
                        true);
                }


            }
        }

    }

    protected void btnConfirmUpdate_Click(object sender, EventArgs e)
    {
        string Enrollment = lblEnroll.Text;
        string columnName = lblPopupColumn.Text;
        string oldValue = lblOldValue.Text;
        string newValue = lblNewValue.Text;

        string query = "";
        string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;

        if (columnName == "Student Name")
        {
            query = "UPDATE [TMU$Student - COLLEGE] SET [Student Name]=@NewValue WHERE [Enrollment No_]=@ID update  HRMSPortal.dbo.StudentABCErrorData set IsCorrected=1 where ID=@IDTbale ";
        }
        else if (columnName == "Father Name")
        {
            query = "UPDATE [TMU$Student - COLLEGE] SET [Fathers Name]=@NewValue WHERE [Enrollment No_]=@ID update  HRMSPortal.dbo.StudentABCErrorData set IsCorrected=1 where ID=@IDTbale ";
        }
        else if (columnName == "Mother Name")
        {
            query = "UPDATE [TMU$Student - COLLEGE] SET [Mothers Name]=@NewValue WHERE [Enrollment No_]=@ID update  HRMSPortal.dbo.StudentABCErrorData set IsCorrected=1 where ID=@IDTbale ";
        }
        else if (columnName == "Contact Number")
        {
            query = "UPDATE [TMU$Student - COLLEGE] SET [Mobile Number]=@NewValue WHERE [Enrollment No_]=@ID update  HRMSPortal.dbo.StudentABCErrorData set IsCorrected=1 where ID=@IDTbale ";
        }
        else if (columnName == "E-Mail")
        {
            query = "UPDATE [TMU$Student - COLLEGE] SET [E-Mail Address]=@NewValue WHERE [Enrollment No_]=@ID update  HRMSPortal.dbo.StudentABCErrorData set IsCorrected=1 where ID=@IDTbale ";
        }
        else if (columnName == "ABC ID")
        {
            query = "UPDATE [TMU$Student - COLLEGE] SET [Visa No_]=@NewValue WHERE [Enrollment No_]=@ID update  HRMSPortal.dbo.StudentABCErrorData set IsCorrected=1 where ID=@IDTbale ";
        }
        else if (columnName == "Address")
        {
            query = "UPDATE [TMU$Student - COLLEGE] SET [Address1]=@NewValue WHERE [Enrollment No_]=@ID update  HRMSPortal.dbo.StudentABCErrorData set IsCorrected=1 where ID=@IDTbale ";
        }
        else if (columnName == "AdharNumber")
        {
            query = "UPDATE [TMU$Student - COLLEGE] SET [Aadhar No_]=@NewValue WHERE [Enrollment No_]=@ID update  HRMSPortal.dbo.StudentABCErrorData set IsCorrected=1 where ID=@IDTbale ";
        }

        if (!string.IsNullOrEmpty(query))
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();

                SqlTransaction tran = con.BeginTransaction();

                try
                {
                    // Update Student Record
                    using (SqlCommand cmd = new SqlCommand(query, con, tran))
                    {
                        cmd.Parameters.AddWithValue("@NewValue", newValue);
                        cmd.Parameters.AddWithValue("@ID", Enrollment); 
                            cmd.Parameters.AddWithValue("@IDTbale", hfID.Value);
                        cmd.ExecuteNonQuery();
                    }

                    // Insert History
                    string historyQuery = @"
                INSERT INTO StudentCorrectionHistory
                (
                    EnrollmentNo,
                    ColumnName,
                    OldValue,
                    NewValue,
                    UpdatedBy,
                    UpdatedOn
                )
                VALUES
                (
                    @EnrollmentNo,
                    @ColumnName,
                    @OldValue,
                    @NewValue,
                    @UpdatedBy,
                    GETDATE()
                )";

                    using (SqlCommand cmdHistory = new SqlCommand(historyQuery, con, tran))
                    {
                        cmdHistory.Parameters.AddWithValue("@EnrollmentNo", Enrollment);
                        cmdHistory.Parameters.AddWithValue("@ColumnName", columnName);
                        cmdHistory.Parameters.AddWithValue("@OldValue", oldValue);
                        cmdHistory.Parameters.AddWithValue("@NewValue", newValue);
                        cmdHistory.Parameters.AddWithValue("@UpdatedBy", Session["uid"].ToString());

                        cmdHistory.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }


    }

}