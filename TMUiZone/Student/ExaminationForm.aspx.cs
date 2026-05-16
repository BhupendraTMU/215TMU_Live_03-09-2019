using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System;

public partial class ExaminationForm : System.Web.UI.Page
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
                    try
                    {

                        if ((Session["CourseCode"].ToString() == "BCA-001" || Session["CourseCode"].ToString() == "BCA-002" || Session["CourseCode"].ToString() == "BCA-004" || Session["CourseCode"].ToString() == "BSC-001" || Session["CourseCode"].ToString() == "BBA-001" || Session["CourseCode"].ToString() == "BCOM-001" || Session["CourseCode"].ToString() == "BPES-001") && Session["Semester"].ToString() == "VI")
                        {
                            SqlDataAdapter da1 = new SqlDataAdapter("select count(*) as 'C' from StudentNEPOptionForm where EnrollmentNo='" + Session["enroll"].ToString() + "'", con);
                            DataTable dt1 = new DataTable();
                            da1.Fill(dt1);
                            if (Convert.ToInt32(dt1.Rows[0]["C"]) == 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
           "alert('Please fill the NEP Undertaking Form by clicking on the NEP Undertaking menu.'); window.location='StudentDetailsView1.aspx';",
           true);

                                return;
                            }
                        }







                        DataTable dt = new DataTable();
                        dt = CheckFormOpenClose();
                        if (dt.Rows.Count > 0 )
                        {
                            if (dt.Rows[0]["OpenClose"].ToString() == "OPEN" )
                            {
                                divSem.Visible = false;
                                SemesterDropdown();

                                //EnrollmentNo = Session["enroll"].ToString();
                                //StudentNo = Session["uid"].ToString();
                                //getExaminationDetail();
                                //getStudentInformation();

                                // getPreviousExaminationDetails();
                                //getExaminationFeeDetails();
                                //getStudentImage();
                                // getDeclaration();
                                //SubmitCheck();
                            }

                            else
                            {
                                if (dt.Rows[0]["OpenClose"].ToString() == "Not Approved")
                                {
                                    PnlMain.Visible = false;
                                    Panel1.Visible = true;
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Your Document not Verified, Please Contact to Admission Cell.')", true);
                                    btnSubmit.Visible = false;
                                }
                                else
                                {
                                    PnlMain.Visible = false;
                                    PnlMsg.Visible = true;
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The Examination Form is yet to be Open')", true);
                                    btnSubmit.Visible = false;
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The Examination Form is yet to be Open')", true);
                            btnSubmit.Visible = false;
                            PnlMain.Visible = false;
                            PnlMsg.Visible = true;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }

            }


        }
        catch { Response.Redirect("../Default.aspx"); }

    }
    public void SemesterDropdown()
    {
        try
        {
            divSem.Visible = true;
            SqlCommand cmd = new SqlCommand("sp_FetchSemesterforExamForm", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            DataRow newRow = dt.NewRow();
            newRow[0] = "--Select--";

            dt.Rows.InsertAt(newRow, 0);
            con.Close();
            drpSemester.DataSource = dt;
            drpSemester.DataTextField = "Sem";
            drpSemester.DataValueField = "Semester";
            drpSemester.DataBind();




        }
        catch (Exception e)
        {

        }

    }
    public DataTable CheckFormOpenClose()
    {
        SqlCommand cmd = new SqlCommand("sp_ValidateFormDate", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@loginid", Session["uid"].ToString());
        cmd.Parameters.Add("@FormName", "EXAM");
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        //con.Open();
        da.Fill(dt);
        con.Close();
        return dt;
    }

    public void getStudentInformation()
    {
        SqlCommand cmd = new SqlCommand("Sp_ExaminationDataFetch", con);

        cmd.CommandType = CommandType.StoredProcedure;
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
        cmd.Parameters.AddWithValue("@Sem", drpSemester.SelectedValue);
        SqlDataReader reader = cmd.ExecuteReader();
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        if (reader.Read())
        {
            LblType.Text = reader["ExaminationType"].ToString();
            TxtSession.Text = reader["Academic Year"].ToString();
            //  txtSNo.Text = reader["No_"].ToString();
            txtExaminationName.Text = reader["Course Name"].ToString();
            txtSemester.Text = reader["Sem"].ToString();
            hfsem.Value = reader["Semester"].ToString();
            TxtBranch.Text = reader["Course Code"].ToString();
            TxtEnrollmentNo.Text = reader["Enrollment No_"].ToString();
            TxtStudentName.Text = reader["Student Name"].ToString();
            lblName.Text = reader["Student Name"].ToString();
            TxtFathersName.Text = reader["Fathers Name"].ToString();
            TxtMothersName.Text = reader["Mothers Name"].ToString();
            TxtHindiName.Text = reader["StudentHindiName"].ToString();
            TxtHindiFathersName.Text = reader["FatherHindiName"].ToString();
            TxtHindiMothersName.Text = reader["MotherHindiName"].ToString();
            // TxtAdharNo.Text = reader["Aadhar No_"].ToString();
            txtABCID.Text = reader["Visa No_"].ToString();
            TxtPostalAddress.Text = reader["PAdress"].ToString();
            TxtContactNo.Text = reader["Mobile Number"].ToString();
            TxtPermanentAdd.Text = reader["Adress"].ToString();
            string ImgStudent = reader["Student Image"].ToString();
            con.Close();
            reader.Close();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record not Inserted ')", true);
        }
    }
    public void getExaminationDetail()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_FetchExaminationDetailsmain", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
            cmd.Parameters.AddWithValue("@Semester", drpSemester.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Close();
            da.Fill(dt);
            GrdAppliedExamination.DataSource = dt;
            GrdAppliedExamination.DataBind();
            if (dt.Rows.Count > 0)
            {
            }
            else
            {
                btnSubmit.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Contact Your Hod.');document.location.href='ExaminationForm.aspx';", true);
                return;
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            con.Close();

        }
    }
    public void getPreviousExaminationDetails()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_PreviousExaminationDetail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Close();
            da.Fill(dt);
            GrdPreviousExam.DataSource = dt;
            GrdPreviousExam.DataBind();

        }
        catch (Exception e)
        {

        }

    }




    public void getExaminationFeeDetails()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_FetchExaminationFeeDetailsmain", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.Parameters.AddWithValue("@StudentNo", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@filtersemester", drpSemester.SelectedValue);
            SqlDataReader reader = cmd.ExecuteReader();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Close();
            da.Fill(dt);
            GridViewFees.DataSource = dt;
            GridViewFees.DataBind();
            if (dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0]["Due Amount"]) == 0 && Convert.ToInt32(dt.Rows[0]["flag"]) == 0)
            {
                btnSubmit.Visible = true;
            }
            else
            {
                btnSubmit.Visible = false;
            }
        }
        catch (Exception ex) { }
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
        SqlCommand cmd = new SqlCommand("Sp_ExaminationDeclaration", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@DeclareType", "Examination");
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
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
        CheckBox chkDeclare = (CheckBox)GrdDeclaration.HeaderRow.FindControl("chkDeclare");
        decimal TotalCreditPoint = 0;
        HiddenField hdfTotalCreditPoint = new HiddenField();
        if (GrdAppliedExamination.Rows.Count > 0)
        {
            for (int i = 1; i <= GrdAppliedExamination.Rows.Count; i++)
            {
                hdfTotalCreditPoint = (HiddenField)GrdAppliedExamination.Rows[i - 1].FindControl("HdfTotalCreditPoint");
                HiddenField CreditPoint = (HiddenField)GrdAppliedExamination.Rows[i - 1].FindControl("HdfCreditPoint");
                TotalCreditPoint = TotalCreditPoint + Convert.ToDecimal(CreditPoint.Value);

            }
        }
        if (TotalCreditPoint != Convert.ToDecimal(hdfTotalCreditPoint.Value))
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Check No. of Subjects With Your Course Coordinator.')", true);
            return;
        }

        SqlDataAdapter da1 = new SqlDataAdapter("select Nationality from [TMU$Student - COLLEGE] where [Enrollment No_]='" + Session["enroll"].ToString() + "' ", con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);

        if (txtABCID.Text == "" && dt1.Rows[0]["Nationality"] == "IND")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please update ABC ID on Student Profile.')", true);
            return;
        }
        if (GridViewFees.Rows.Count > 0)
        {
            for (int i = 1; i <= GridViewFees.Rows.Count; i++)
            {
                Label lblFee = (Label)GridViewFees.Rows[i - 1].FindControl("lblFee");
                HiddenField hhindi = (HiddenField)GridViewFees.Rows[i - 1].FindControl("hdhindi");
                HiddenField hdsma = (HiddenField)GridViewFees.Rows[i - 1].FindControl("hdSmaj");
                if (hdsma.Value == "0")
                {
                    if (Convert.ToDecimal(lblFee.Text) > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('First Clear Your Dues.')", true);
                        return;
                    }
                }

                if (hhindi.Value == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fill  Your Hindi Name.')", true);
                    return;
                }

            }

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fee details not avialable .')", true);
            return;
        }
        if (chkDeclare.Checked == true)
        {
            SqlCommand cmd = new SqlCommand("Sp_GetExaminationFormCheck", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
            cmd.Parameters.AddWithValue("@Semester", drpSemester.SelectedValue);
            int T = cmd.ExecuteNonQuery();

            con.Close();

            if (T > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Examination Form Submitted')", true);
                SubmitCheck();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Not Submitted Try Again After Some Time')", true);
                return;
            }

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Kindly Declare The Policy box ')", true);
        }
    }

    public void SubmitCheck()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("sp_SubmitCheck", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
            cmd.Parameters.AddWithValue("@Semester", drpSemester.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);
            //int T = cmd.ExecuteNonQuery();
            if (dt.Rows.Count > 0)
            {
                btnSubmit.Visible = false;
                CheckBox chkDeclare = (CheckBox)GrdDeclaration.HeaderRow.FindControl("chkDeclare");
                chkDeclare.Checked = true;
                chkDeclare.Enabled = false;
                PanelHide.Visible = true;
                BtnPrint.Visible = true;

            }
            con.Close();
        }
        catch (Exception ex)
        {
            btnSubmit.Visible = true;
            CheckBox chkDeclare = (CheckBox)GrdDeclaration.HeaderRow.FindControl("chkDeclare");
            chkDeclare.Checked = false;
            chkDeclare.Enabled = true;
            PanelHide.Visible = false;
            BtnPrint.Visible = false;

        }
    }
    protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        // if (drpSemester.SelectedItem.Text != "YEAR 5")
        //  {
        //   ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The Examination Form is yet to be Open')", true);
        //  return;
        // }
        //  else
        //  {

        PnlMain.Visible = true;
        EnrollmentNo = Session["enroll"].ToString();
        StudentNo = Session["uid"].ToString();
        getExaminationDetail();
        getStudentInformation();
        getPreviousExaminationDetails();
        getExaminationFeeDetails();
        getStudentImage();
        getDeclaration();
        SubmitCheck();
        // }



    }



}