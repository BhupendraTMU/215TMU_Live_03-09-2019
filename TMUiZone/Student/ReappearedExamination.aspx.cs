using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
//Test
//using WEBStudentFine;//Test
using ReapReference;
using AjaxControlToolkit;

public partial class ReappearedExamination : System.Web.UI.Page
{

    static string EnrollmentNo = "";
    string StudentNo = "";
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["enroll"].ToString() == null)
            {
                Response.Redirect("../Default.aspx");
            }

            else
            {

                if (!IsPostBack)
                {
                    DataTable dt = new DataTable();
                    dt = CheckFormOpenClose();
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["OpenClose"].ToString() == "OPEN")
                        {
                            EnrollmentNo = Session["enroll"].ToString();
                            StudentNo = Session["uid"].ToString();
                            ReappearSemesterDropdown();

                            getDeclaration();
                            getExaminationDetail();
                            getStudentInformation();
                            getReappearDetail();
                            getPreviousExaminationDetails();
                            getExaminationFeeDetails();
                            getStudentImage();
                        }
                        else if (dt.Rows[0]["LOCK"].ToString() == "1")
                        {
                            PnlMain.Visible = false;
                            PnlMsg.Visible = true;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Consolidated mark sheet already generated with Audit')", true);
                            btnSubmit.Visible = false;
                        }
                        else
                        {
                            PnlMain.Visible = false;
                            PnlMsg.Visible = true;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The Reappear Examination Form is yet to be Open')", true);
                            btnSubmit.Visible = false;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The Reappear Examination Form is yet to be Open')", true);
                        btnSubmit.Visible = false;
                        PnlMain.Visible = false;
                        PnlMsg.Visible = true;
                    }
                }


            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../Default.aspx");

        }

    }

    public DataTable CheckFormOpenClose()
    {
        SqlCommand cmd = new SqlCommand("sp_ValidateFormDateREAP", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@loginid", Session["uid"].ToString());
        cmd.Parameters.Add("@FormName", "RE-APPEAR");
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        //con.Open();
        da.Fill(dt);
        con.Close();
        return dt;
    }
    public void ReappearSemesterDropdown()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("sp_FetchReappearSemesterFromSubjectcollege", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            DataRow newRow = dt.NewRow();
            newRow[0] = "--Select--";
            newRow[1] = "--Select--";
            dt.Rows.InsertAt(newRow, 0);
            con.Close();
            ddlSem.DataSource = dt;
            ddlSem.DataTextField = "Sem";
            ddlSem.DataValueField = "Semester";
            ddlSem.DataBind();
            if (dt.Rows.Count > 0)
            {
                if (ddlSem.SelectedIndex == 0)
                {
                    div_visible_TF.Visible = false;
                    btnSubmit.Visible = false;
                    BtnPrint.Visible = false;
                }
                else
                {
                    div_visible_TF.Visible = true;
                    btnSubmit.Visible = true;
                    BtnPrint.Visible = true;
                }
            }
            else
            {
                Response.Redirect("../Default.aspx");
            }



        }
        catch (Exception e)
        {

        }

    }

    public void getStudentInformation()
    {
        try
        {
            con.Close();
            SqlCommand cmd = new SqlCommand("Sp_ExaminationDataFetch_Reap", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@EnrollmentNo", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Sem", ddlSem.SelectedValue);
            SqlDataReader reader = cmd.ExecuteReader();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            if (reader.Read())
            {
                LblType.Text = reader["ExaminationType"].ToString();
                TxtSession.Text = reader["Academic Year"].ToString();
                //  txtSNo.Text = reader["No_"].ToString();
                txtExaminationName.Text = reader["Course Name"].ToString();
                if (ddlSem.SelectedValue != "")
                {
                    txtSemester.Text = ddlSem.SelectedItem.Text; //reader["Sem"].ToString();
                }
                // hfsem.Value = reader["Semester"].ToString();

                TxtBranch.Text = reader["Course Code"].ToString();
                TxtEnrollmentNo.Text = reader["Enrollment No_"].ToString();
                TxtStudentName.Text = reader["Student Name"].ToString();
                lblName.Text = reader["Student Name"].ToString();
                TxtFathersName.Text = reader["Fathers Name"].ToString();
                TxtMothersName.Text = reader["Mothers Name"].ToString();
                TxtHindiName.Text = reader["StudentHindiName"].ToString();
                TxtHindiFathersName.Text = reader["FatherHindiName"].ToString();
                TxtHindiMothersName.Text = reader["MotherHindiName"].ToString();
                //TxtAdharNo.Text = reader["Aadhar No_"].ToString();
                TxtPostalAddress.Text = reader["PAdress"].ToString();
                TxtContactNo.Text = reader["Mobile Number"].ToString();
                TxtPermanentAdd.Text = reader["Adress"].ToString();

                string ImgStudent = reader["Student Image"].ToString();
                con.Close();
                reader.Close();
            }
            else
            {

            }
        }
        catch
        {
        }
    }
    public void getExaminationDetail()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_FetchExaminationDetails_Reap", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EnrollmentNo", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Filtersemester", ddlSem.SelectedValue);
            cmd.Parameters.AddWithValue("@AdacdemicYear", Session["AcademicYear"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            GrdAppliedExamination.DataSource = dt;
            GrdAppliedExamination.DataBind();
            if (dt.Rows.Count > 0)
            {
                btnCinvoice.Visible = true;
            }
            else
            {
                btnCinvoice.Visible = false;
            }

        }
        catch (Exception ex)
        {

        }
    }

    public void getReappearDetail()
    {

        try
        {

            SqlCommand cmd = new SqlCommand("Sp_FetchExaminationDetails_ReapInvoice", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
            cmd.Parameters.AddWithValue("@Filtersemester", ddlSem.SelectedValue);
            cmd.Parameters.AddWithValue("@AdacdemicYear", Session["AcademicYear"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            GrdAppliedRep.DataSource = dt;
            GrdAppliedRep.DataBind();
            if (dt.Rows.Count > 0)
            {
                btnSubmit.Visible = true;
                SubmitCheck();

            }
            else
            {
                if (ddlSem.SelectedIndex > 0)
                {
                    //btnSubmit.Visible = true;
                    CheckBox chkDeclare = (CheckBox)GrdDeclaration.HeaderRow.FindControl("chkDeclare");
                    chkDeclare.Checked = false;
                    chkDeclare.Enabled = true;
                    PanelHide.Visible = false;
                    BtnPrint.Visible = false;
                }
                else
                {
                    btnSubmit.Visible = false;
                }

            }

        }
        catch (Exception ex)
        {

        }
    }


    public void getPreviousExaminationDetails()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_PreviousExaminationDetail_Reap", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            GrdPreviousExam.DataSource = dt;
            GrdPreviousExam.DataBind();
            // }

        }
        catch (Exception e)
        {

        }
        //finally
        //{
        //    con.Close();

        //}


    }

    public void getExaminationFeeDetails()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_FetchExaminationFeeDetails_Reap", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@StudentNo", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Filtersemester", ddlSem.SelectedValue);
            //SqlDataReader reader = cmd.ExecuteReader();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();



            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            da.Fill(dt);
            con.Close();
            GridViewFees.DataSource = dt;
            GridViewFees.DataBind();

        }
        catch (Exception ex)
        {

        }
    }
    public void getStudentImage()
    {

        string id = Session["enroll"].ToString();
        byte[] bytes = GetData("select [Student Image]  from [TMU$Student - COLLEGE] where [Enrollment No_]='" + id + "'").Rows[0]["Student Image"].ToString() == "" ? null : (byte[])GetData("select [Student Image]  from [TMU$Student - COLLEGE] where [Enrollment No_]='" + id + "'").Rows[0]["Student Image"];
        if (bytes != null)
        {
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            ImgStudent.ImageUrl = "data:image/png;base64," + base64String;
        }

    }
    private DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                    }
                }
                // return dt;
            }
        }
        catch { }
        finally
        {
            con.Close();
        }
        return dt;
    }
    public void getDeclaration()
    {
        SqlCommand cmd = new SqlCommand("Sp_ExaminationDeclaration_Reap", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@DeclareType", "Examination");
        con.Open();
        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GrdDeclaration.DataSource = dt;
        GrdDeclaration.DataBind();
        con.Close();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {



        DataTable dt = GetData("SELECT COUNT(*) as 'C' FROM [dbo].[TMU$Gen_ Journal Line] where [Account No_]='" + Session["uid"].ToString() + "' and [Academic Year]=(Select [Academic Year] from [TMU$Student - COLLEGE] where No_='" + Session["uid"].ToString() + "')    and Course=(Select [Course Code] from [TMU$Student - COLLEGE] where No_='" + Session["uid"].ToString() + "')   and   (Semester='" + ddlSem.SelectedValue + "' or [Year Code]='" + ddlSem.SelectedValue + "' )");


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
        if (GridViewFees.Rows.Count == 0)
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
        con.Open();
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

                getExaminationDetail();
                getReappearDetail();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Your Reappear Exam Form Submitted')", true);
            }
            catch (Exception ex)
            {
            }

            //}
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('First Clear Your  Dues.')", true);
            //    return;
            //}

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fee set up does not create please contact to Admin')", true);
            return;
        }



    }

    public void SubmitCheck()
    {
        SqlCommand cmd = new SqlCommand("sp_SubmitCheck_Reap", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        try
        {
            cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
            cmd.Parameters.AddWithValue("@SemYear", ddlSem.SelectedValue);
            if (cmd.ExecuteScalar().ToString() == "0")
            {
                btnSubmit.Visible = false;
                CheckBox chkDeclare = (CheckBox)GrdDeclaration.HeaderRow.FindControl("chkDeclare");
                chkDeclare.Checked = true;
                chkDeclare.Enabled = false;
                PanelHide.Visible = true;
                BtnPrint.Visible = true;
            }
            else
            {
                btnSubmit.Visible = true;
                CheckBox chkDeclare = (CheckBox)GrdDeclaration.HeaderRow.FindControl("chkDeclare");
                chkDeclare.Checked = false;
                chkDeclare.Enabled = true;
                PanelHide.Visible = false;
                BtnPrint.Visible = false;
            }
        }
        catch (Exception ex) { }
        finally { con.Close(); }

    }


    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = GetData("select top 1 [Hold Result],[Hold Remarks] from[TMU$Posted Student Ext_Int Line] where[Enrollement No_] = '" + Session["enroll"].ToString() + "' and (Semester = '" + ddlSem.SelectedValue + "' or Year ='" + ddlSem.SelectedValue + "' )");

        if (dt.Rows[0]["Hold Result"].ToString() == "1")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage",
"alert('Your Result is Hold due to "+ dt.Rows[0]["Hold Remarks"].ToString() + "'); window.location='ReappearedExamination.aspx';", true);
            return;
        }







        if (ddlSem.SelectedIndex > 0)
        {
            div_visible_TF.Visible = true;
            getReappearDetail();
            getExaminationDetail();
            getExaminationFeeDetails();
            getPreviousExaminationDetails();
            getStudentInformation();
        }
        else
        {
            div_visible_TF.Visible = false;
            BtnPrint.Visible = false;
            btnSubmit.Visible = false;
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
        //SqlCommand cmdInvoice = new SqlCommand("Sp_FetchExaminationFeeDetails_Reap", con);
        //cmdInvoice.CommandType = CommandType.StoredProcedure;

        //cmdInvoice.Parameters.AddWithValue("@StudentNo", Session["uid"].ToString());
        //cmdInvoice.Parameters.AddWithValue("@Filtersemester", ddlSem.SelectedValue);
        ////SqlDataReader reader = cmd.ExecuteReader();
        //SqlDataAdapter da = new SqlDataAdapter(cmdInvoice);
        //DataTable dt11 = new DataTable();
        //if (con.State == ConnectionState.Open)
        //{
        //    con.Close();
        //}

        //con.Open();
        //da.Fill(dt11);
        //con.Close();
        //if (dt11.Rows.Count > 0)
        //{
        //    Response.Redirect("ReappearedExamination.aspx");
        //}
        //else
        //{
        SqlCommand cmd1 = new SqlCommand("sp_FetchReappearPaperFee", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
        cmd1.Parameters.AddWithValue("@Sem", ddlSem.SelectedValue);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        con.Open();
        da1.Fill(dt1);
        con.Close();
        if (dt1.Rows.Count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fee set up does not create please contact to Admin')", true);
            return;
        }
        //if (Convert.ToInt32(dt1.Rows[0]["Flag"]) > 0)
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invoice already created,please contact to account section.')", true); return;

        //}

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
                }

            }
            if (s == 0)
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



                    getExaminationDetail();
                    getReappearDetail();


                    getExaminationFeeDetails();

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