using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


public partial class Faculty_ExaminationFormRelease : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal conn;

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Portalcon = new Connection();
            conn = new ServicePoratal();
            if (Session["UserGroup"].ToString() == "PRINCIPAL" || Session["UserGroup"].ToString() == "DEAN")
            {

                if (!IsPostBack)
                {
                    BindAcademicYear();
                    bindDrpCourseList();
                    bindStudentExamDetails();
                }
            }
            else
            {
                Response.Redirect("../Default.aspx");
            }

        }
        catch
        {
        }

    }


    public void SMS(String MobileNo, string Msg)
    {
        //  Website: http://www.universalsmsadvertising.com/     //  Username: 9837016352   //  Password: 9837016352 
        MobileNo = "91" + MobileNo;
        // MobileNo = "91" + 9015762885;
        string url = "http://www.universalsmsadvertising.com/universalsmsapi.php?user_name=9837016352&user_password=9837016352&mobile=" + MobileNo + "&sender_id=TMUniv&type=F&text=" + Msg + "";
        System.Net.HttpWebRequest fr;
        Uri targetURI = new Uri(url);
        fr = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(targetURI);
        if (fr.GetResponse().ContentLength > 0)
        {
            System.IO.StreamReader str = new System.IO.StreamReader(fr.GetResponse().GetResponseStream());
            Response.Write(str.ReadToEnd());
            str.Close();
        }
    }



    public void SendSMSStudentExam(string No_,string userid, string courscode, string sem)
    {
        string tablenameemployeedata = "";
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");

        tablenameemployeedata = "[" + rccname + "$Student - COLLEGE" + "]";
        string mobilnoemp = "";
         string smsdata = "Dear student, " + userid + ", Your examination form for " + courscode + ", " + sem + ",AY " + ddlAcademicYear.SelectedValue + " has been Rejected by Principal";

        //string smsdata = "Dear " + userid + ", Your Examination Form  has been Rejected by Principal";


        SqlDataReader dr = Portalcon.SHow_StudentMobileNo(No_, tablenameemployeedata);
        dr.Read();
        if (dr.HasRows)
        {
            mobilnoemp = dr["MobilePhoneNo"].ToString();
            dr.Close();
            Portalcon.DisConnect();
            if (mobilnoemp.Trim() == "")
            { }
            else
            {
                try
                {
                    SMS(mobilnoemp, smsdata);
                }
                catch (Exception)
                {

                }
            }
        }
        else
        {
            dr.Close();
            Portalcon.DisConnect();
        }
    }


    public void SendSMSHODExam(string course, string userid, string sem)
    {
        string tablenameemployeedata = "";
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");

        tablenameemployeedata = "[" + rccname + "$Employee" + "]";
        string mobilnoemp = "";
         string smsdata = "Dear sir, " + userid + ", Your student form for " + course + ", " + sem + ",AY " + ddlAcademicYear.SelectedValue + " has been Rejected by Principal";

        //string smsdata = "Dear sir your " + course + ", " + userid + "Student Examination Form  has been Rejected by Principal";
        

        SqlDataReader dr = Portalcon.SHow_HODMobileNo(course, tablenameemployeedata);
        dr.Read();
        if (dr.HasRows)
        {
            mobilnoemp =dr["MobilePhoneNo"].ToString();
            dr.Close();
            Portalcon.DisConnect();
            if (mobilnoemp.Trim() == "")
            { }
            else
            {
                try
                {
                    SMS(mobilnoemp, smsdata);
                }
                catch (Exception)
                {

                }
            }
        }
        else
        {
            dr.Close();
            Portalcon.DisConnect();
        }
    }
   




    public void BindAcademicYear()
    {
        SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        con.Open();
        da.Fill(dt1);
        con.Close();
        ddlAcademicYear.DataSource = dt1;
        ddlAcademicYear.DataTextField = "Details";
        ddlAcademicYear.DataValueField = "No_";
        ddlAcademicYear.DataBind();
    }

    public void bindDrpCourseList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetHODCourse_RoleMatrix", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Open();
        da.Fill(dt);
        con.Close();
        ddlCourse.DataSource = dt;
        ddlCourse.DataTextField = "Details";
        ddlCourse.DataValueField = "No_";
        ddlCourse.DataBind();
        if (dt.Rows.Count < 2)
        {
            Response.Redirect("../Default.aspx");
        }
    }
    public void bindDrpSemesterList()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_GetSemYearFilterByOddEvenExamSp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", Session["uid"].ToString());
            cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            cmd.Parameters.Add("@CourseCode", ddlCourse.SelectedValue);
            if (Chekap.Checked == true)
            {
                cmd.Parameters.Add("@Examtype", "1");
            }
            else
            {
                cmd.Parameters.Add("@Examtype", "0");
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            con.Open();
            da.Fill(dt1);
            con.Close();
            ddlSem.DataSource = dt1;
            ddlSem.DataTextField = "Details";
            ddlSem.DataValueField = "Code";
            ddlSem.DataBind();
        }
        catch
        {
        }
    }

    protected void Chekap_CheckedChanged(object sender, EventArgs e)
    {
        bindDrpSemesterList();
        GrdExamList.Visible = false;
        BtnSubmit.Visible = false;
        BtnRejected.Visible = false;
    }

    protected void ddlAcademicYear_SelectedIndexChanged1(object sender, EventArgs e)
    {
        bindDrpCourseList();
        GrdExamList.Visible = false;
        BtnSubmit.Visible = false;
        BtnRejected.Visible = false;
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpSemesterList();
        GrdExamList.Visible = false;
        BtnSubmit.Visible = false;
        BtnRejected.Visible = false;
    }
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        GrdExamList.Visible = false;
        BtnSubmit.Visible = false;
        BtnRejected.Visible = false;
    }
    public void bindStudentExamDetails()
    {
        SqlCommand cmd = new SqlCommand("Sp_GetStudentExaminationDetailsPrincipal", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@AcademicYear", ddlAcademicYear.SelectedValue);
        cmd.Parameters.AddWithValue("@CourseCode", ddlCourse.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", ddlSem.SelectedValue);
        cmd.Parameters.AddWithValue("@Appear", ddlReaapear.SelectedValue);
        cmd.Parameters.AddWithValue("@LoginId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.AddWithValue("@Status", Rblist.SelectedValue);
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
            if (Rblist.SelectedValue == "0")
            {
                BtnSubmit.Visible = false;
                BtnRejected.Visible = false;
            }
            else if (Rblist.SelectedValue == "1")
            {
                BtnSubmit.Visible = true ;
                BtnRejected.Visible = true;
            }
            else if (Rblist.SelectedValue == "2")
            {
                BtnSubmit.Visible = false;
                BtnRejected.Visible = true;
            }
            else if (Rblist.SelectedValue == "3")
            {
                BtnSubmit.Visible = false;
                BtnRejected.Visible = false;
            }
            else if (Rblist.SelectedValue == "4")
            {
                BtnSubmit.Visible = true;
                BtnRejected.Visible = false;
            }
            else if (Rblist.SelectedValue == "5")
            {
                BtnSubmit.Visible = false;
                BtnRejected.Visible = false;
            }
        }
        else
        {
            BtnSubmit.Visible = false;
            BtnRejected.Visible = false;
            GrdExamList.Visible = false;
        }

    }

    protected void ddlReaapear_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlReaapear.SelectedValue == "1")
        {
            divSp.Visible = true;
        }
        else
        {
            Chekap.Checked = false;
            divSp.Visible = false;
        }


        BindAcademicYear();
        bindDrpCourseList();
        bindDrpSemesterList();
        GrdExamList.Visible = false;
        BtnSubmit.Visible = false;
        BtnRejected.Visible = false;
        bindStudentExamDetails();
     


    }

    protected void BtnShow_Click(object sender, EventArgs e)
    {
        bindStudentExamDetails();
        GrdExamList.Visible = true;
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

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int i = 0;
            foreach (GridViewRow row in GrdExamList.Rows)
            {
                var id = GrdExamList.DataKeys[row.RowIndex].Value;
                CheckBox check = (CheckBox)row.FindControl("chkStudent");
                Label lblSemester = (Label)row.FindControl("lblSemester");
                SqlCommand cmd = new SqlCommand("ExaminationFormRelease", con);
                if (check.Checked == true)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                    cmd.Parameters.AddWithValue("@AcadmicYear", ddlAcademicYear.SelectedValue);
                    cmd.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
                    cmd.Parameters.AddWithValue("@EnrollmentNo", id);
                    cmd.Parameters.AddWithValue("@ExamFormStatus", "2");
                    cmd.Parameters.AddWithValue("@Appear", ddlReaapear.SelectedValue);
                    cmd.Parameters.AddWithValue("@Semester", lblSemester.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    i++;

                }
               
            }
            if (i > 0) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Submitted')", true); }
            else { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Data.. !')", true); }
            bindStudentExamDetails();

            //BtnSubmit.Visible = false;
            //GrdExamList.Visible = false;
        }
        catch
        {
        }

    }

  
    protected void GrdExamList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdExamList.PageIndex = e.NewPageIndex;
        bindStudentExamDetails();
    }

    protected void btnview_Click(object sender, EventArgs e)
    {
        HfEnrollment_No.Value = ""; HfStudent_No.Value = ""; HFsemester.Value = "";
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int i = Convert.ToInt32(row.RowIndex);
        HfEnrollment_No = (HiddenField)row.FindControl("HfEnrollmentNo");
        HfStudent_No = (HiddenField)row.FindControl("HfStudentNo");
        HFsemester = (HiddenField)row.FindControl("HfSemester_S");
        GetExamFormInfo();
        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModalB').modal('show');</script>", false);

    }
    
    public void GetExamFormInfo()
    {
        getStudentInformation();
        getExaminationDetail();
        getPreviousExaminationDetails();
        getExaminationFeeDetails();
        
        getDeclaration();
    }
    public void getExaminationDetail()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_FetchExaminationDetails", con);

            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@EnrollmentNo", HfEnrollment_No.Value);
            cmd.Parameters.AddWithValue("@Appear", ddlReaapear.SelectedValue);
            cmd.Parameters.AddWithValue("@Sem", HFsemester.Value);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Close();
            da.Fill(dt);
            GrdAppliedExamination.DataSource = dt;
            GrdAppliedExamination.DataBind();
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
            SqlCommand cmd = new SqlCommand("Sp_PreviousExaminationDetail", con); //Sp_PreviousExaminationDetail 
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@EnrollmentNo", HfEnrollment_No.Value);
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

    public void getStudentInformation()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_ExaminationDataFetch", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@EnrollmentNo", HfEnrollment_No.Value);
            cmd.Parameters.AddWithValue("@Sem", HFsemester.Value);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Close();
            da.Fill(dt);
            if (ddlReaapear.SelectedValue == "1")
            {


                string academicyear = dt.Rows[0]["Academic Year"].ToString();
                if (!academicyear.Contains("Re-appear / Supplementary"))
                {

                    academicyear = academicyear + ")</br> Re-appear / Supplementary";
                    dt.Rows[0]["Academic Year"] = academicyear;
                }
            }
            else
            {
                string academicyear = dt.Rows[0]["Academic Year"].ToString();
                academicyear = academicyear + ")";
                dt.Rows[0]["Academic Year"] = academicyear;
            }
            RepSinformation.DataSource = dt;
            RepSinformation.DataBind();
            
        }
        catch (Exception ex)
        { }
    }

    public void getExaminationFeeDetails()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_FetchExaminationFeeDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@StudentNo", HfStudent_No.Value);
            cmd.Parameters.AddWithValue("@filtersemester", HFsemester.Value);
            if (ddlReaapear.SelectedValue.ToString() == "0")
            {
            cmd.Parameters.AddWithValue("@ExamFee", "EXAM");
            }
            else
            {
                cmd.Parameters.AddWithValue("@ExamFee", "REAP");
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();               
                con.Close();
                da.Fill(dt);
                GridViewFees.DataSource = dt;
                GridViewFees.DataBind();
            
        }
        catch (Exception ex) { }
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
        con.Open();
        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GrdDeclaration.DataSource = dt;
        GrdDeclaration.DataBind();
        con.Close();
    }


    protected void Rblist_SelectedIndexChanged(object sender, EventArgs e)
    {



        bindStudentExamDetails();



    }

    protected void BtnRejected_Click(object sender, EventArgs e)
    {
        try
        {

            int i = 0;
            foreach (GridViewRow row in GrdExamList.Rows)
            {
                var id = GrdExamList.DataKeys[row.RowIndex].Value;
                CheckBox check = (CheckBox)row.FindControl("chkStudent");
                Label lblSemester = (Label)row.FindControl("lblSemester");
                HiddenField HNo_ = (HiddenField)row.FindControl("HfStudentNo");
                Label lbEnroll = (Label)row.FindControl("lblEnrollNo");
                Label lblCourC = (Label)row.FindControl("lblCourse");
                HiddenField Semy = (HiddenField)row.FindControl("HFSemY");
                SqlCommand cmd = new SqlCommand("ExaminationFormRelease", con);
                if (check.Checked == true)
                {
                    SendSMSStudentExam(HNo_.Value, lbEnroll.Text, lblCourC.Text, Semy.Value);
                    SendSMSHODExam(lblCourC.Text, lbEnroll.Text, Semy.Value);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                    cmd.Parameters.AddWithValue("@AcadmicYear", ddlAcademicYear.SelectedValue);
                    cmd.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
                    cmd.Parameters.AddWithValue("@EnrollmentNo", id);
                    cmd.Parameters.AddWithValue("@ExamFormStatus", "4");
                    cmd.Parameters.AddWithValue("@Appear", ddlReaapear.SelectedValue);
                    cmd.Parameters.AddWithValue("@Semester", lblSemester.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    i++;

                }

            }
            if (i > 0) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Submitted')", true); }
            else { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Data.. !')", true); }
            bindStudentExamDetails();


        }
        catch
        {
        }

    }
   
}