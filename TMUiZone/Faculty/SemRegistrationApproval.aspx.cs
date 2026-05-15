using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Web.Services;
using System.Web.Script.Services;

using System.Text;

public partial class Faculty_SemRegistrationApproval : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {

                bindDrpCourseList();
                bindAcademicYear();
                //bindSubject();
                //bindApprovalList();



            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    public void bindAcademicYear()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            con.Open();
            da.Fill(dt1);
            con.Close();
            drpAcademicYear.DataSource = dt1;
            drpAcademicYear.DataTextField = "Details";
            drpAcademicYear.DataValueField = "No_";
            drpAcademicYear.DataBind();
        }
        catch
        {
        }
    }
    public void bindDrpCourseList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetCourseFromCourseWiseFacultyCollege_Role1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Open();
        da.Fill(dt);
        Session["UserRole"] = dt.Rows[0]["UserRole"].ToString();
        con.Close();
        drpCourse.DataSource = dt;
        drpCourse.DataTextField = "Details";
        drpCourse.DataValueField = "No_";
        drpCourse.DataBind();
    }
    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpSemesterList();
    }
    public void bindDrpSemesterList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetSemester_Role", con);//proc_GetSemesterFromCourseWiseFaculty_Role comment on 27-12-2017
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSemester.DataSource = dt;
        drpSemester.DataTextField = "Details";
        drpSemester.DataValueField = "No_";
        drpSemester.DataBind();
    }

    public void getExaminationDetail()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_FetchCourseForsemRegistration", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EnrollmentNo", HfEnrollment_No.Value);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            GrdAppliedExamination.DataSource = dt;
            GrdAppliedExamination.DataBind();
            //if (dt.Rows.Count > 0)
            //{
            //    btnCinvoice.Visible = true;
            //}
            //else
            //{
            //    btnCinvoice.Visible = false;
            //}

        }
        catch (Exception ex)
        {

        }
    }

    public void getStudentInformation()
    {
        try
        {
            con.Close();
            SqlCommand cmd = new SqlCommand("Sp_StudentDetailforSemRegistration", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@EnrollmentNo", HfEnrollment_No.Value);
            cmd.Parameters.AddWithValue("@Sem", "");
            SqlDataReader reader = cmd.ExecuteReader();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            if (reader.Read())
            {
                txtProgramName.Text = reader["Course Name"].ToString();
                txtAcademic.Text = "20" + reader["Academic Year"].ToString();
                TxtACategory.Text = "NS";
                TxtStudentFName.Text = reader["Student Name"].ToString();
                TxtFatherName.Text = reader["Fathers Name"].ToString();
                TxtFatherMobile.Text = reader["Father Mobile No"].ToString();
                TxtFathersOcc.Text = reader["Fathers Occupation"].ToString();
                TxtMotherName.Text = reader["Mothers Name"].ToString();
                TxtMothersMobile.Text = reader["Mother Mobile No"].ToString();
                TxtHindiMotherOcc.Text = reader["Mothers Occupation"].ToString();
                TxtNationality.Text = reader["Nationality1"].ToString();
                TxtDOB.Text = reader["DOB"].ToString();

                TxtGender.Text = reader["Gender1"].ToString();

                txtCategory.Text = reader["Category1"].ToString();

                txtEmergencyContact.Text = reader["Emergency Contact No_"].ToString();


                txtAddress.Text = reader["Address2"].ToString();
                txtCity.Text = reader["City"].ToString();

                txtState.Text = reader["State"].ToString();

                txtCountry.Text = reader["Country"].ToString();

                txtPin.Text = reader["Post Code"].ToString();

                txtTel.Text = reader["Telephone"].ToString();

                txtFax.Text = reader["Fax"].ToString();

                txtMobile.Text = reader["Mobile Number"].ToString();

                txtEmail.Text = reader["E-Mail Address"].ToString();

                txtPAddress.Text = reader["Address1"].ToString();

                txtPCity.Text = reader["City"].ToString();

                txtPState.Text = reader["State"].ToString();

                txtPCountry.Text = reader["Country"].ToString();

                txtPPin.Text = reader["Post Code"].ToString();

                txtPTel.Text = reader["Telephone"].ToString();

                txtPFax.Text = reader["Fax"].ToString();
                txtDate.Text = DateTime.Now.ToString("dd-MM-yyyy");


                TxtFatherMobile.Text = reader["Father_Mobile_No"].ToString();
                TxtFathersOcc.Text = reader["Father_Occupation"].ToString();
                TxtMothersMobile.Text = reader["Mother_MobileNo"].ToString();
                TxtHindiMotherOcc.Text = reader["MotherOccupation"].ToString();
                txtEmergencyContact.Text = reader["EmergencyContactNo"].ToString();
                txtPin.Text = reader["CPin"].ToString();
                txtTel.Text = reader["CTel"].ToString();
                txtFax.Text = reader["CFax"].ToString();
                txtPPin.Text = reader["PPin"].ToString();
                txtPTel.Text = reader["PTel"].ToString();
                txtPFax.Text = reader["PFax"].ToString();
                txtLGName.Text = reader["LGName"].ToString();
                txtLGAddress.Text = reader["LGAddress"].ToString();
                txtLGCity.Text = reader["LGCity"].ToString();
                txtLGState.Text = reader["LGState"].ToString();
                txtLGCountry.Text = reader["LGCountry"].ToString();
                txtLGPin.Text = reader["LGPin"].ToString();
                txtLGTel.Text = reader["LGTel"].ToString();
                txtLGMobile.Text = reader["LGMobile"].ToString();
                txtLGEmail.Text = reader["LGEmail"].ToString();
                txtHAddress.Text = reader["HAddress"].ToString();
                txtHCity.Text = reader["HCity"].ToString();
                txtHPin.Text = reader["HPin"].ToString();
                txtHTel.Text = reader["HTel"].ToString();
                if (reader["Sichness"].ToString() == "1")
                {
                    chkYes.Checked = true;
                    chkNo.Checked = false;
                    DivDocDetail.Visible = true;

                }
                else
                {
                    chkNo.Checked = true;
                    chkYes.Checked = false;
                    DivDocDetail.Visible = false;

                }
                txtDrName.Text = reader["DrName"].ToString();
                txtDrAddress.Text = reader["DrAddress"].ToString();
                txtDrCity.Text = reader["DrCity"].ToString();
                txtDRState.Text = reader["DrState"].ToString();
                txtDRPin.Text = reader["DrPin"].ToString();
                txtDrTel.Text = reader["DrTel"].ToString();
                txtDRMobile.Text = reader["DrMobile"].ToString();
                txtDREmail.Text = reader["DrEmail"].ToString();
                txtBloodGroup.Text = reader["DrBloodGroup"].ToString();
                txtDate.Text = reader["Date"].ToString();
                txtPlace.Text = reader["Place"].ToString();
                TxtDDAte.Text = reader["Date1"].ToString();
                TextBox2.Text = reader["Date2"].ToString();
                TextBox3.Text = reader["Place1"].ToString();
                





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
    private DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
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
            return dt;
        }
    }
    public void BINDimage()
    {
        string id = Session["uid"].ToString();

        byte[] bytes = GetData("select [Student Image]  from [TMU$Student - COLLEGE] where [Enrollment No_]='" + HfEnrollment_No.Value + "'").Rows[0]["Student Image"].ToString() == "" ? null : (byte[])GetData("select [Student Image]  from [TMU$Student - COLLEGE] where [Enrollment No_]='" + HfEnrollment_No.Value + "'").Rows[0]["Student Image"];
        if (bytes != null)
        {
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            ImgStudent.ImageUrl = "data:image/png;base64," + base64String;

        }
    }

    public void BindGrid()
    {
        try
        {

            SqlCommand cmd = new SqlCommand("Sp_GetSemesterRegistration", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AcademicYear", drpAcademicYear.SelectedValue);
            cmd.Parameters.AddWithValue("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.AddWithValue("@Semester", drpSemester.SelectedValue);

            cmd.Parameters.AddWithValue("@LoginId", Session["uid"].ToString());


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            GrdExamList.DataSource = dt;
            GrdExamList.DataBind();
            if (dt.Rows.Count > 0)
            {
                GrdExamList.Visible = true;
                BtnSubmit.Visible = true;
            }



        }
        catch (Exception ex)
        {
        }
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindGrid();


    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int i = 0;
            foreach (GridViewRow row in GrdExamList.Rows)
            {
                CheckBox check = (CheckBox)row.FindControl("chkStudent");
                Label lblSemester = (Label)row.FindControl("lblSemester");
                var id = GrdExamList.DataKeys[row.RowIndex].Value;
                SqlCommand cmd = new SqlCommand("SemRegistrationApproval", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                cmd.Parameters.AddWithValue("@AcadmicYear", drpAcademicYear.SelectedValue);
                cmd.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());

                cmd.Parameters.AddWithValue("@EnrollmentNo", id);
                cmd.Parameters.AddWithValue("@semester", lblSemester.Text);
                if (check.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@AdmitCardapproval", "1");
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    i++;
                }


            }
            if (i > 0) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Submitted')", true);
            BindGrid();
            }
            else { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Data.. !')", true); }
            BtnSubmit.Visible = false;

        }
        catch (Exception ex)
        {
        }
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkBoxheader = (CheckBox)GrdExamList.HeaderRow.FindControl("chkAll");
        foreach (GridViewRow Row in GrdExamList.Rows)
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

        CheckBox checkBoxheader = (CheckBox)GrdExamList.HeaderRow.FindControl("chkAll");
        foreach (GridViewRow Row in GrdExamList.Rows)
        {
            CheckBox checkRows = (CheckBox)Row.FindControl("chkStudent");
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
    protected void btnview_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int i = Convert.ToInt32(row.RowIndex);
        HfEnrollment_No = (HiddenField)row.FindControl("HfEnrollmentNo");
        HFsemester = (HiddenField)row.FindControl("HfSesterr");
        getStudentInformation();


        getExaminationDetail();
        BINDimage();
        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModalB').modal('show');</script>", false);

    }
    protected void GrdExamList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdExamList.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}