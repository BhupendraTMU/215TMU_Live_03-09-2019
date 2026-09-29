using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using ReapReference;

public partial class ReappearedExamination : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    private readonly string CS = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["enroll"] == null || Session["uid"] == null)
            {
                Response.Redirect("../Default.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (!IsPostBack)
            {
                DataTable dt = CheckFormOpenClose();

                if (dt.Rows.Count == 0)
                {
                    ShowMessage("The Reappear Examination Form is yet to be Open");
                    return;
                }

                if (dt.Rows[0]["LOCK"].ToString() == "1")
                {
                    PnlMain.Visible = false;
                    PnlMsg.Visible = true;
                    btnSubmit.Visible = false;

                    ScriptManager.RegisterClientScriptBlock(
                        this,
                        GetType(),
                        "msg",
                        "alert('Consolidated mark sheet already generated with Audit');",
                        true);

                    return;
                }

                if (dt.Rows[0]["OpenClose"].ToString() != "OPEN")
                {
                    ShowMessage("The Reappear Examination Form is yet to be Open");
                    return;
                }

                ReappearSemesterDropdown();

                GetDeclaration();
                GetExaminationDetail();
                GetStudentInformation();
                GetReappearDetail();
                GetPreviousExaminationDetails();
                GetExaminationFeeDetails();
                GetStudentImage();
            }
        }
        catch (Exception ex)
        {
            // TODO : Log Error

            Response.Redirect("../Default.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }

    private void ShowMessage(string msg)
    {
        PnlMain.Visible = false;
        PnlMsg.Visible = true;
        btnSubmit.Visible = false;

        string safeMsg = HttpUtility.JavaScriptStringEncode(msg);

        ScriptManager.RegisterClientScriptBlock(
            this,
            GetType(),
            Guid.NewGuid().ToString(),
            string.Format("alert('{0}');", safeMsg),
            true);
    }

    public DataTable CheckFormOpenClose()
    {
        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(CS))
        using (SqlCommand cmd = new SqlCommand("sp_ValidateFormDateREAP", con))
        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@loginid", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@FormName", "RE-APPEAR");

            da.Fill(dt);
        }

        return dt;
    }

    public void ReappearSemesterDropdown()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(CS))
            using (SqlCommand cmd = new SqlCommand("sp_FetchReappearSemesterFromSubjectcollege", con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@EnrollmentNo",
                    Session["enroll"].ToString());

                DataTable dt = new DataTable();

                da.Fill(dt);

                DataRow dr = dt.NewRow();
                dr["Sem"] = "--Select--";
                dr["Semester"] = "";

                dt.Rows.InsertAt(dr, 0);

                ddlSem.DataSource = dt;
                ddlSem.DataTextField = "Sem";
                ddlSem.DataValueField = "Semester";
                ddlSem.DataBind();

                div_visible_TF.Visible = false;
                btnSubmit.Visible = false;
                BtnPrint.Visible = false;
            }
        }
        catch (Exception ex)
        {
            // TODO : Log Error
        }
    }

    public void GetStudentInformation()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(CS))
            using (SqlCommand cmd = new SqlCommand("Sp_ExaminationDataFetch_Reap", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@EnrollmentNo", SqlDbType.VarChar, 50)
                   .Value = Session["uid"].ToString();

                cmd.Parameters.Add("@Sem", SqlDbType.VarChar, 20)
                   .Value = ddlSem.SelectedValue;


                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        LblType.Text = reader["ExaminationType"].ToString();

                        TxtSession.Text = reader["Academic Year"].ToString();

                        txtExaminationName.Text = reader["Course Name"].ToString();

                        if (!string.IsNullOrEmpty(ddlSem.SelectedValue))
                        {
                            txtSemester.Text = ddlSem.SelectedItem.Text;
                        }

                        TxtBranch.Text = reader["Course Code"].ToString();

                        TxtEnrollmentNo.Text = reader["Enrollment No_"].ToString();

                        TxtStudentName.Text = reader["Student Name"].ToString();

                        lblName.Text = reader["Student Name"].ToString();

                        TxtFathersName.Text = reader["Fathers Name"].ToString();

                        TxtMothersName.Text = reader["Mothers Name"].ToString();

                        TxtHindiName.Text = reader["StudentHindiName"].ToString();

                        TxtHindiFathersName.Text = reader["FatherHindiName"].ToString();

                        TxtHindiMothersName.Text = reader["MotherHindiName"].ToString();

                        TxtPostalAddress.Text = reader["PAdress"].ToString();

                        TxtContactNo.Text = reader["Mobile Number"].ToString();

                        TxtPermanentAdd.Text = reader["Adress"].ToString();


                        string ImgStudent = reader["Student Image"].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log exception
            // Example:
            // Response.Write(ex.Message);
        }
    }
    public void GetExaminationDetail()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(CS))
            using (SqlCommand cmd = new SqlCommand("Sp_FetchExaminationDetails_Reap", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@EnrollmentNo", SqlDbType.VarChar, 50)
                   .Value = Session["uid"].ToString();

                cmd.Parameters.Add("@Filtersemester", SqlDbType.VarChar, 20)
                   .Value = ddlSem.SelectedValue;

                cmd.Parameters.Add("@AdacdemicYear", SqlDbType.VarChar, 20)
                   .Value = Session["AcademicYear"].ToString();


                DataTable dt = new DataTable();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }


                GrdAppliedExamination.DataSource = dt;
                GrdAppliedExamination.DataBind();


                btnCinvoice.Visible = dt.Rows.Count > 0;
            }
        }
        catch (Exception ex)
        {
            // Log error
            // Example:
            // lblMessage.Text = ex.Message;

        }
    }

    public void GetReappearDetail()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(CS))
            using (SqlCommand cmd = new SqlCommand("Sp_FetchExaminationDetails_ReapInvoice", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@EnrollmentNo", SqlDbType.VarChar, 50)
                   .Value = Session["enroll"].ToString();

                cmd.Parameters.Add("@Filtersemester", SqlDbType.VarChar, 20)
                   .Value = ddlSem.SelectedValue;

                cmd.Parameters.Add("@AdacdemicYear", SqlDbType.VarChar, 20)
                   .Value = Session["AcademicYear"].ToString();


                DataTable dt = new DataTable();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }


                GrdAppliedRep.DataSource = dt;
                GrdAppliedRep.DataBind();


                if (dt.Rows.Count > 0)
                {
                    btnSubmit.Visible = true;
                    SubmitCheck();
                }
                else
                {
                    btnSubmit.Visible = false;

                    if (ddlSem.SelectedIndex > 0)
                    {
                        if (GrdDeclaration.HeaderRow != null)
                        {
                            CheckBox chkDeclare =
                                (CheckBox)GrdDeclaration.HeaderRow.FindControl("chkDeclare");

                            if (chkDeclare != null)
                            {
                                chkDeclare.Checked = false;
                                chkDeclare.Enabled = true;
                            }
                        }

                        PanelHide.Visible = false;
                        BtnPrint.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log Exception
            // Example:
            // lblMessage.Text = ex.Message;
        }
    }

    public void GetPreviousExaminationDetails()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(CS))
            using (SqlCommand cmd = new SqlCommand("Sp_PreviousExaminationDetail_Reap", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@EnrollmentNo", SqlDbType.VarChar, 50)
                   .Value = Session["enroll"].ToString();


                DataTable dt = new DataTable();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }


                GrdPreviousExam.DataSource = dt;
                GrdPreviousExam.DataBind();
            }
        }
        catch (Exception ex)
        {
            // Log Exception
            // Example: WriteLog(ex.Message);
        }
    }

    public void GetExaminationFeeDetails()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(CS))
            using (SqlCommand cmd = new SqlCommand("Sp_FetchExaminationFeeDetails_Reap", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("@StudentNo", SqlDbType.VarChar, 50)
                   .Value = Session["uid"].ToString();


                cmd.Parameters.Add("@Filtersemester", SqlDbType.VarChar, 20)
                   .Value = ddlSem.SelectedValue;


                DataTable dt = new DataTable();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }


                GridViewFees.DataSource = dt;
                GridViewFees.DataBind();
            }
        }
        catch (Exception ex)
        {
            // Log Exception
            // Example: WriteLog(ex.Message);
        }
    }
    public void GetStudentImage()
    {
        try
        {
            string enrollmentNo = Session["enroll"].ToString();

            if (string.IsNullOrEmpty(enrollmentNo))
                return;


            byte[] bytes = null;


            using (SqlConnection con = new SqlConnection(CS))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT [Student Image] FROM [TMU$Student - COLLEGE] WHERE [Enrollment No_]=@EnrollmentNo", con))
            {
                cmd.Parameters.Add("@EnrollmentNo", SqlDbType.VarChar, 50)
                   .Value = enrollmentNo;


                con.Open();

                object result = cmd.ExecuteScalar();


                if (result != null && result != DBNull.Value)
                {
                    bytes = (byte[])result;
                }
            }


            if (bytes != null && bytes.Length > 0)
            {
                string base64String = Convert.ToBase64String(bytes);

                ImgStudent.ImageUrl = "data:image/png;base64," + base64String;
            }
            else
            {
                ImgStudent.ImageUrl = "~/Images/no-image.png";
            }
        }
        catch (Exception ex)
        {
            // Log Exception
        }
    }
    private DataTable GetData(string query)
    {
        DataTable dt = new DataTable();

        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // log error
        }

        return dt;
    }
    public void GetDeclaration()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(CS))
            using (SqlCommand cmd = new SqlCommand("Sp_ExaminationDeclaration_Reap", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@DeclareType", SqlDbType.VarChar, 50)
                   .Value = "Examination";

                DataTable dt = new DataTable();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                GrdDeclaration.DataSource = dt;
                GrdDeclaration.DataBind();
            }
        }
        catch (Exception ex)
        {
            // Log Exception
            // Example: WriteLog(ex.Message);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {



        DataTable dt = GetData("SELECT COUNT(*) as 'C' FROM [dbo].[TMU$Gen_ Journal Line] where [Account No_]='" + Session["uid"].ToString() + "' and [Academic Year]=(Select [Academic Year] from [TMU$Student - COLLEGE] where No_='" + Session["uid"].ToString() + "')    and Course=(Select [Course Code] from [TMU$Student - COLLEGE] where No_='" + Session["uid"].ToString() + "')   and   (Semester='" + ddlSem.SelectedValue + "' or [Year Code]='" + ddlSem.SelectedValue + "' )");

        int mooc = 0;

        foreach (GridViewRow row in GrdAppliedRep.Rows)
        {

            HiddenField hfMOOC = (row.Cells[0].FindControl("hfMOOC") as HiddenField);
            if(hfMOOC.Value!="1")
            {
                mooc = 1;
            }
        }

        if (Convert.ToInt32(dt.Rows[0]["C"]) > 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('First Clear Your Dues.')", true);
            return;
        }
        CheckBox chkDeclare = (CheckBox)GrdDeclaration.HeaderRow.FindControl("chkDeclare");
        if (chkDeclare.Checked == false)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Kindly Declare The Policy box ')", true);
            return;
        }
        if (GridViewFees.Rows.Count == 0 && mooc!=0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Contact to Your HOD.')", true);
            return;

        }


        if (GridViewFees.Rows.Count > 0)
        {
            for (int i = 1; i <= GridViewFees.Rows.Count; i++)
            {
                HiddenField hhindi = (HiddenField)GridViewFees.Rows[i - 1].FindControl("hdhindi");
                Label lblFee = (Label)GridViewFees.Rows[i - 1].FindControl("lblFee");
                if (hhindi.Value == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fill  Your Hindi Name.')", true);
                    return;
                }
                if (Convert.ToDecimal(lblFee.Text) > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('First Clear Your Dues.')", true);
                    return;
                }
            }
        }

        SqlCommand cmd1 = new SqlCommand("sp_FetchReappearPaperFee", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
        cmd1.Parameters.AddWithValue("@Sem", ddlSem.SelectedValue);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        da1.Fill(dt1);
        con.Close();
        if (dt1.Rows.Count > 0)
        {
            //    decimal fee = 0;
            //    decimal dfee = 0;
            //    fee = Convert.ToDecimal(dt1.Rows[0]["Amount"]);
            //    dfee = Convert.ToDecimal(dt1.Rows[0]["Detainee Max_ Amount"]);

            //    decimal counter = 0;
            //    decimal hdtRnee = 0;
            //    int countdtRne = 0;
            //    foreach (GridViewRow row in GrdAppliedRep.Rows)
            //    {
            //        Label lblPay = (row.Cells[0].FindControl("LblPracticalName1") as Label);
            //        HiddenField hdAR = (row.Cells[0].FindControl("hdtAp") as HiddenField);
            //        HiddenField hdSTy = (row.Cells[0].FindControl("hdStyAr") as HiddenField);


            //        hdtRnee = Convert.ToDecimal(hdAR.Value.ToString());
            //        if (Convert.ToString(hdSTy.Value) == "")
            //        {
            //            if (hdtRnee > 0)
            //            {
            //                countdtRne = countdtRne + 1;
            //                counter = counter + 0;
            //            }
            //            else
            //            {
            //                counter = counter + 1;
            //            }
            //        }
            //    }

            //    decimal total = 0;
            //    decimal talatDR = 0;
            //    talatDR = countdtRne * fee;


            //    talatDR = countdtRne * fee;
            //    if (dfee > talatDR)
            //    {
            //        total = (countdtRne + counter) * fee;
            //    }
            //    else
            //    {
            //        total = (counter) * fee + dfee;
            //    }



            //    decimal count = 0;

            //    foreach (GridViewRow row in GridViewFees.Rows)
            //    {

            //        Label lbla = (row.Cells[0].FindControl("lblPaidFee") as Label);
            //        count = count + Convert.ToDecimal(lbla.Text.ToString());
            //    }

            //if (total == count)
            //{

            try
            {

                foreach (GridViewRow row in GrdAppliedRep.Rows)
                {


                    int ReappearExamFormSubmitted = 1;
                    Label LblPracticalCode = (Label)row.FindControl("LblPracticalCode1");
                    SqlCommand cmd = new SqlCommand("sp_subjectsubmissionforreappearexam", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
                    cmd.Parameters.AddWithValue("@Semester", ddlSem.SelectedValue);
                    cmd.Parameters.AddWithValue("@SubjectCode", LblPracticalCode.Text);
                    cmd.Parameters.AddWithValue("@ReappearExamFormSubmitted", ReappearExamFormSubmitted);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                GetExaminationDetail();
                GetReappearDetail();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Your Reappear Exam Form Submitted')", true);
            }
            catch (Exception ex)
            {
            }



        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fee set up does not create please contact to Admin')", true);
            return;
        }



    }
    public void SubmitCheck()
    {
        try
        {
            int result = 1;

            using (SqlConnection con = new SqlConnection(CS))
            using (SqlCommand cmd = new SqlCommand("sp_SubmitCheck_Reap", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@EnrollmentNo", SqlDbType.VarChar, 50)
                    .Value = Session["enroll"].ToString();

                cmd.Parameters.Add("@SemYear", SqlDbType.VarChar, 20)
                    .Value = ddlSem.SelectedValue;


                con.Open();

                object obj = cmd.ExecuteScalar();

                if (obj != null)
                {
                    result = Convert.ToInt32(obj);
                }
            }


            CheckBox chkDeclare = null;

            if (GrdDeclaration.HeaderRow != null)
            {
                chkDeclare = (CheckBox)GrdDeclaration.HeaderRow.FindControl("chkDeclare");
            }


            if (result == 0)
            {
                btnSubmit.Visible = false;

                if (chkDeclare != null)
                {
                    chkDeclare.Checked = true;
                    chkDeclare.Enabled = false;
                }

                PanelHide.Visible = true;
                BtnPrint.Visible = true;
            }
            else
            {
                btnSubmit.Visible = true;

                if (chkDeclare != null)
                {
                    chkDeclare.Checked = false;
                    chkDeclare.Enabled = true;
                }

                PanelHide.Visible = false;
                BtnPrint.Visible = false;
            }

        }
        catch (Exception ex)
        {
            // Log Exception
            // WriteLog(ex.Message);
            throw;
        }
    }

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(CS))
            using (SqlCommand cmd = new SqlCommand(@"
            SELECT TOP 1 
                [Hold Result],
                [Hold Remarks]
            FROM [TMU$Posted Student Ext_Int Line] With(NOLOCK)
            WHERE [Enrollement No_] = @EnrollmentNo
            AND (Semester = @Semester OR Year = @Semester)", con))
            {
                cmd.Parameters.Add("@EnrollmentNo", SqlDbType.VarChar, 50)
                    .Value = Session["enroll"].ToString();

                cmd.Parameters.Add("@Semester", SqlDbType.VarChar, 20)
                    .Value = ddlSem.SelectedValue;


                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }


            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Hold Result"].ToString() == "1")
                {
                    string remarks = HttpUtility.JavaScriptStringEncode(
                        dt.Rows[0]["Hold Remarks"].ToString()
                    );

                    ScriptManager.RegisterClientScriptBlock(
                        this,
                        this.GetType(),
                        "alertMessage",
                        "alert('Your Result is Hold due to {remarks}'); window.location='ReappearedExamination.aspx';",
                        true
                    );

                    return;
                }
            }



            if (ddlSem.SelectedIndex > 0)
            {
                div_visible_TF.Visible = true;

                GetReappearDetail();
                GetExaminationDetail();
                GetExaminationFeeDetails();
                GetPreviousExaminationDetails();
                GetStudentInformation();
            }
            else
            {
                div_visible_TF.Visible = false;
                BtnPrint.Visible = false;
                btnSubmit.Visible = false;
            }

        }
        catch (Exception ex)
        {
            // Log Exception
            throw;
        }
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkBoxheader = (CheckBox)GrdAppliedExamination.HeaderRow.FindControl("chkAll");
        foreach (GridViewRow Row in GrdAppliedExamination.Rows)
        {
            CheckBox checkRows = (CheckBox)Row.FindControl("chkStudent");
            if (checkBoxheader.Checked == true)
            {
                checkRows.Checked = true;

            }
            else
            {
                checkRows.Checked = false;
            }

        }

    }

    protected void chkStudent_CheckedChanged(object sender, EventArgs e)
    {

        CheckBox checkBoxheader = (CheckBox)GrdAppliedExamination.HeaderRow.FindControl("chkAll");

        foreach (GridViewRow Row in GrdAppliedExamination.Rows)
        {
            CheckBox checkRows = (CheckBox)Row.FindControl("chkStudent");

            Label LblPracticalCode = (Label)Row.FindControl("LblPracticalCode");
            foreach (GridViewRow Row1 in GrdAppliedExamination.Rows)
            {
                HiddenField hdStyp = (HiddenField)Row1.FindControl("hdStyp");
                CheckBox checkRows1 = (CheckBox)Row1.FindControl("chkStudent");
                if (hdStyp.Value != "")
                {
                    if (checkRows.Checked == true)
                    {
                        if (LblPracticalCode.Text == hdStyp.Value)
                        {
                            checkRows1.Checked = true;
                        }
                    }
                    else
                    {
                        if (LblPracticalCode.Text == hdStyp.Value)
                        {
                            checkRows1.Checked = false;
                        }
                    }


                }

            }
            if (checkRows.Checked == false)
            {
                checkBoxheader.Checked = false;

            }
            else
            {
                // checkBoxheader.Checked = true;
            }

        }
    }


    protected void GrdAppliedExamination_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        CheckBox chkDeclare = (CheckBox)GrdDeclaration.HeaderRow.FindControl("chkDeclare");

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkAll = (CheckBox)GrdAppliedExamination.HeaderRow.FindControl("chkAll");
            string hfReappear = (e.Row.FindControl("hfReappear") as HiddenField).Value;
            CheckBox chk = (CheckBox)e.Row.FindControl("chkStudent");
            //if (chkDeclare.Checked == true) { chk.Enabled = false; chkAll.Enabled = false; }
            if (hfReappear == "1") { chk.Checked = true; }
            else
            {
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('subject not selected)", true);
            }
        }
    }
    protected void btnCinvoice_Click(object sender, EventArgs e)
    {
        btnCinvoice.Visible = false;
        btnCinvoice.Enabled = false;
        int Count = 0;
        foreach (GridViewRow row in GrdAppliedExamination.Rows)
        {
            CheckBox chkRow = (row.Cells[0].FindControl("chkStudent") as CheckBox);
            Label SubjectCode = (row.Cells[0].FindControl("LblPracticalCode") as Label);
            HiddenField Dependent = (row.Cells[0].FindControl("hdStyp") as HiddenField);
            HiddenField FeeGenerate = (row.Cells[0].FindControl("FeeGenerate") as HiddenField);

            if (chkRow.Checked)
            {
                DataTable dt4 = GetData("select count(*) as num from  [TMU$Re-Appear Student Subject] where[Enrollment No] = '" + Session["enroll"].ToString() + "'  and [Subject Code] = '" + SubjectCode.Text + "' and [Re-App Doc No] = (Select Code from[TMU$Re-Appear Setup] where Active = 1 and CONVERT(date, GETDATE()) between[Start Date] and[End Date])");
                if (Convert.ToInt32(dt4.Rows[0]["num"]) > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You have already submit Reappear form for this subject')", true);
                    return;
                }
            }
        }

        SqlCommand cmd1 = new SqlCommand("sp_FetchReappearPaperFee", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
        cmd1.Parameters.AddWithValue("@Sem", ddlSem.SelectedValue);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        da1.Fill(dt1);
        con.Close();
        if (dt1.Rows.Count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fee set up does not create please contact to Admin')", true);
            return;
        }


        if (Convert.ToInt32(dt1.Rows[0]["Amount"]) == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Course Fee setup not created,please contact to account section.')", true); return;

        }



        if (dt1.Rows.Count > 0)
        {


            decimal fee = 0;
            decimal dfee = 0;
            decimal dsubmit = 0;
            decimal dtAmount = 0;
            decimal ReAmount = 0;
            fee = Convert.ToDecimal(dt1.Rows[0]["Amount"]);
            dfee = Convert.ToDecimal(dt1.Rows[0]["Detainee Max_ Amount"]);
            dsubmit = Convert.ToDecimal(dt1.Rows[0]["DTAmount"]);
            decimal hdtnee = 0;
            int s = 0;
            int s1 = 0;
            foreach (GridViewRow row in GrdAppliedExamination.Rows)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkStudent") as CheckBox);
                HiddenField hdte = (row.Cells[0].FindControl("hddetani") as HiddenField);

                HiddenField hfMOOC = (row.Cells[0].FindControl("hfMOOC") as HiddenField);
                HiddenField hSt = (row.Cells[0].FindControl("hdStyp") as HiddenField);
                HiddenField hdResult = (row.Cells[0].FindControl("hdResult") as HiddenField);
                HiddenField FeeGenerate = (row.Cells[0].FindControl("FeeGenerate") as HiddenField);
                if (chkRow.Checked)
                {
                    if (hfMOOC.Value != "1")
                    {
                        if (TxtBranch.Text == "PT-001")
                        {
                            if (Convert.ToString(hdResult.Value) == "1" || FeeGenerate.Value == "1")
                            {

                                hdtnee = Convert.ToInt32(hdte.Value.ToString());
                                if (hdtnee > 0 && (dtAmount + Convert.ToDecimal(dt1.Rows[0]["DTAmount"])) < Convert.ToDecimal(dt1.Rows[0]["Detainee Max_ Amount"]) && Convert.ToDecimal(dt1.Rows[0]["DTAmount"]) < Convert.ToDecimal(dt1.Rows[0]["Detainee Max_ Amount"]))
                                {
                                    dtAmount = dtAmount + Convert.ToDecimal(dt1.Rows[0]["Amount"]);

                                }
                                else if (hdtnee == 0)
                                {
                                    ReAmount = ReAmount + Convert.ToDecimal(dt1.Rows[0]["Amount"]);
                                }
                                if (dtAmount > Convert.ToDecimal(dt1.Rows[0]["Detainee Max_ Amount"]))
                                {
                                    dtAmount = Convert.ToDecimal(dt1.Rows[0]["Detainee Max_ Amount"]);
                                }

                            }
                            s++;

                        }


                        else
                        {


                            if (Convert.ToString(hdResult.Value) == "2" && Count == 0)
                            {
                                if (hSt.Value.ToString() != "")
                                {
                                    Count = 1;
                                }
                                hdtnee = Convert.ToInt32(hdte.Value.ToString());
                                if (hdtnee > 0 && (dtAmount + Convert.ToDecimal(dt1.Rows[0]["DTAmount"])) < Convert.ToDecimal(dt1.Rows[0]["Detainee Max_ Amount"]) && Convert.ToDecimal(dt1.Rows[0]["DTAmount"]) < Convert.ToDecimal(dt1.Rows[0]["Detainee Max_ Amount"]))
                                {
                                    dtAmount = dtAmount + Convert.ToDecimal(dt1.Rows[0]["Amount"]);

                                }
                                else if (hdtnee == 0)
                                {
                                    ReAmount = ReAmount + Convert.ToDecimal(dt1.Rows[0]["Amount"]);
                                }
                                if (dtAmount > Convert.ToDecimal(dt1.Rows[0]["Detainee Max_ Amount"]))
                                {
                                    dtAmount = Convert.ToDecimal(dt1.Rows[0]["Detainee Max_ Amount"]);
                                }
                            }

                            s++;
                        }
                    }
                    else
                    {
                        s1 = 1;
                    }
                }

            }
            if (s == 0 && s1 != 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please selected subject ')", true); return;

            }
            btnCinvoice.Visible = false;
            btnCinvoice.Enabled = false;
            decimal total = 0;
            total = dtAmount + ReAmount;
            //
            //11000;
            //dtAmount + ReAmount;
            //return;
            if (total > 0)
            {
                DataTable dtNAV = new DataTable();
                SqlCommand cmdNAV = new SqlCommand("Proc_GetNAVCreditionalLive", con);
                cmdNAV.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter daNAV = new SqlDataAdapter(cmdNAV);
                daNAV.Fill(dtNAV);
                VoucherPosting nvp = new VoucherPosting();
                nvp.UseDefaultCredentials = true;
                nvp.Url = dtNAV.Rows[0]["URL"].ToString();
                nvp.Credentials = new NetworkCredential(dtNAV.Rows[0]["UserID"].ToString(), dtNAV.Rows[0]["Password"].ToString());
                nvp.ReappearStudentFee(Session["uid"].ToString(), Convert.ToDecimal(total), ddlSem.SelectedValue, ddlSem.SelectedValue);
            }
            else if (total == 0 && dt1.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fee set up does not create please contact to Admin')", true);
                return;
            }

            int b = 0;
            try
            {

                con.Open();
                foreach (GridViewRow row in GrdAppliedExamination.Rows)
                {
                    CheckBox check = (CheckBox)row.FindControl("chkStudent");
                    HiddenField hdtt = (HiddenField)row.FindControl("hddetani");
                    Label lblDec = (Label)row.FindControl("LblPracticalName");
                    int ReappearExamFormSubmitted = 0;
                    if (check.Checked == true)
                    {
                        ReappearExamFormSubmitted = 1;

                        //}
                        Label LblPracticalCode = (Label)row.FindControl("LblPracticalCode");
                        //var id = GrdAppliedExamination.DataKeys[row.RowIndex].Value;
                        SqlCommand cmd = new SqlCommand("sp_subjectsubmissionReappearinvoice", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
                        cmd.Parameters.AddWithValue("@Semester", ddlSem.SelectedValue);
                        cmd.Parameters.AddWithValue("@SubjectCode", LblPracticalCode.Text);
                        cmd.Parameters.AddWithValue("@ReappearExamFormSubmitted", ReappearExamFormSubmitted);
                        cmd.Parameters.AddWithValue("@Description", lblDec.Text);
                        cmd.Parameters.AddWithValue("@Detanee", hdtt.Value);
                        cmd.Parameters.AddWithValue("@Amount", Convert.ToDecimal(dt1.Rows[0]["Amount"]));
                        //if (check.Checked == true)
                        //{

                        cmd.ExecuteNonQuery();

                    }
                    b++;
                }
                if (b == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('subject not selected')", true); return;
                }
                else
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invoice  Form Submitted')", true);
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }



                    GetExaminationDetail();
                    GetReappearDetail();


                    GetExaminationFeeDetails();

                }
            }
            catch (Exception ex) { }
            finally { if (con.State == ConnectionState.Open) { con.Close(); } }

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fee set up does not creat please contact to Admin')", true);
            return;
        }

        //btnCinvoice.Visible = true;
        // btnCinvoice.Enabled = true;
        //}
    }
    protected void GrdAppliedRep_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        CheckBox chkDeclare = (CheckBox)GrdDeclaration.HeaderRow.FindControl("chkDeclare");

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkAll = (CheckBox)GrdAppliedRep.HeaderRow.FindControl("chkAllrep");
            string hfReappear = (e.Row.FindControl("hfReappear1") as HiddenField).Value;
            CheckBox chk = (CheckBox)e.Row.FindControl("chkStudentrep");
            //if (chkDeclare.Checked == true) { chk.Enabled = false; chkAll.Enabled = false; }
            if (hfReappear == "1")
            {
                chk.Checked = true;

            }

            else
            {
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('subject not selected)", true);
            }
        }
    }
    protected void chkAllrep_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkBoxheader = (CheckBox)GrdAppliedRep.HeaderRow.FindControl("chkAllrep");
        foreach (GridViewRow Row in GrdAppliedRep.Rows)
        {
            CheckBox checkRowsrr = (CheckBox)Row.FindControl("chkStudentrep");
            if (checkBoxheader.Checked == true)
            {
                checkRowsrr.Checked = true;

            }
            else
            {
                checkRowsrr.Checked = false;
            }

        }
    }
    protected void chkStudentrep_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkBoxheaderr = (CheckBox)GrdAppliedRep.HeaderRow.FindControl("chkAllrep");
        foreach (GridViewRow Row in GrdAppliedRep.Rows)
        {
            CheckBox checkRowsr = (CheckBox)Row.FindControl("chkStudentrep");
            if (checkRowsr.Checked == false)
            {
                checkBoxheaderr.Checked = false;

            }
            else
            {
                // checkBoxheader.Checked = true;
            }

        }
    }


}