using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using System.Collections.Specialized;
using DL;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Web.UI.HtmlControls;


public partial class Faculty_AdmitCardRelease : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        //string UserGroup = Session["UserGroup"].ToString();
        //if (Session["UserGroup"].ToString() == "HOD")
        //{
        if (!IsPostBack)
        {
            BindAcademicYear();
            bindDrpCourseList();
            bindStudentExamDetails();
        }
        //else
        //{
        //    Response.Redirect("../Default.aspx");
        //}

    }
    public void BindAcademicYear()
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
            ddlAcademicYear.DataSource = dt1;
            ddlAcademicYear.DataTextField = "Details";
            ddlAcademicYear.DataValueField = "No_";
            ddlAcademicYear.DataBind();
        }
        catch
        {
        }
    }

    public void bindDrpCourseList()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetPrincipalCourse_RoleMatrix", con);
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
            if (dt.Rows.Count < 2) { Response.Redirect("../Default.aspx"); }
        }
        catch
        {
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
        GrdExamList.Visible = false;
        BtnSubmit.Visible = false;
        bindDrpSemesterList();
    }

    protected void ddlAcademicYear_SelectedIndexChanged1(object sender, EventArgs e)
    {
        bindDrpCourseList();
        GrdExamList.Visible = false;
        BtnSubmit.Visible = false;
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)

    {
        bindDrpSemesterList();
        bindStudentExamDetails();       // bindDrpSemesterList();
        GrdExamList.Visible = false;
        BtnSubmit.Visible = false;
    }
    public void bindStudentExamDetails()
    
       { 
        try
        {

            SqlCommand cmd = new SqlCommand("Sp_GetStudentExaminationDetailsFor_Admit_Card_release", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AcademicYear", ddlAcademicYear.SelectedValue);
            cmd.Parameters.AddWithValue("@CourseCode", ddlCourse.SelectedValue);
            cmd.Parameters.AddWithValue("@Semester", ddlSem.SelectedValue);
            cmd.Parameters.AddWithValue("@Appear", ddlReaapear.SelectedValue);
            cmd.Parameters.AddWithValue("@LoginId", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());

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
            else
            {
                SqlCommand cmd1 = new SqlCommand("Sp_GetAdmitCardStatus", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@AcademicYear", ddlAcademicYear.SelectedValue);
                cmd1.Parameters.AddWithValue("@CourseCode", ddlCourse.SelectedValue);
                cmd1.Parameters.AddWithValue("@Semester", ddlSem.SelectedValue);
                cmd1.Parameters.AddWithValue("@Appear", ddlReaapear.SelectedValue);
                cmd1.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                cmd1.Parameters.AddWithValue("@LoginId", Session["uid"].ToString());
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                con.Open();
                da1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt1.Rows[0]["Admit Card Status"]) == 1)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Admid Card Already Approved...')", true);
                    }
                    if (Convert.ToInt32(dt1.Rows[0]["Admit Card Status"]) == 2)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Admid Card Approved by Principal...')", true);
                    }

                }

                BtnSubmit.Visible = false;
            }

        }
        catch (Exception e)

        {
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
                CheckBox check = (CheckBox)row.FindControl("chkStudent");
                Label lblSemester = (Label)row.FindControl("lblSemester");
                var id = GrdExamList.DataKeys[row.RowIndex].Value;
                SqlCommand cmd = new SqlCommand("ExaminationFormApproveFor_AdmitCard_Release", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                cmd.Parameters.AddWithValue("@AcadmicYear", ddlAcademicYear.SelectedValue);
                cmd.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@Appear", ddlReaapear.SelectedValue);
                cmd.Parameters.AddWithValue("@EnrollmentNo", id);
                cmd.Parameters.AddWithValue("@semester", lblSemester.Text);
                if (check.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@AdmitCardapproval", "2");
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    i++;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@AdmitCardapproval", "1");
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Checkbox not Selected')", true);
                }
                //con.Open();
                //cmd.ExecuteNonQuery();
                //con.Close();
            }
            if (i > 0) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Submitted')", true); }
            else { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Data.. !')", true); }
            BtnSubmit.Visible = false;
            bindStudentExamDetails();
        }
        catch(Exception ex)
        {
        }
    }


    protected void GrdExamList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdExamList.PageIndex = e.NewPageIndex;
        bindStudentExamDetails();
    }
    protected void RepAugust_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataSet ds = new DataSet();
                string customerId = (e.Item.FindControl("hfCustomerId") as HiddenField).Value;
                GridView GrdSubject = e.Item.FindControl("GrdSubjectDetail") as GridView;
                GridView GrdSubject1 = e.Item.FindControl("GrdSubjectDetail1") as GridView;
                HtmlGenericControl dvYN = (HtmlGenericControl)e.Item.FindControl("Extrasubject");

                SqlCommand cmd = new SqlCommand("Sp_FetchAdmitCardExaminationDetailsAAdmit", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@EnrollmentNo", customerId);
                cmd.Parameters.AddWithValue("@Appear", ddlReaapear.SelectedValue);
                cmd.Parameters.AddWithValue("@Semester", HFsemester.Value);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GrdSubject.Visible = true;
                    GrdSubject.DataSource = ds.Tables[0];
                    GrdSubject.DataBind();
                }
                else
                {
                    GrdSubject.Visible = false;
                }

                //SqlCommand cmd1 = new SqlCommand("Sp_FetchAdmitCardExaminationDetailsB", con);
                //cmd1.CommandType = CommandType.StoredProcedure;
                //con.Open();
                //cmd1.Parameters.AddWithValue("@EnrollmentNo", customerId);
                //cmd1.Parameters.AddWithValue("@Appear", ddlReaapear.SelectedValue);
                //cmd1.Parameters.AddWithValue("@Semester", HFsemester.Value);
                //SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                //da1.Fill(dt1);
                //con.Close();
                if (ds.Tables.Count > 1)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        GrdSubject1.Visible = true;
                        GrdSubject1.DataSource = ds.Tables[1];
                        GrdSubject1.DataBind();
                    }
                    else
                    {
                        GrdSubject1.Visible = false;
                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {

                        dvYN.Visible = true;

                    }
                    else
                    {

                        dvYN.Visible = false;
                    }
                }
                else
                {

                    dvYN.Visible = false;
                }


            }
        }
        catch (Exception ex)
        {
        }
    }




    public void bindAdmitcard()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_AdmitCardDataFetch_PNC", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EnrollmentNo", HfEnrollment_No.Value);
            cmd.Parameters.AddWithValue("@Sem", HFsemester.Value);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            RepAugust.DataSource = dt;
            if (ddlReaapear.SelectedValue == "1")
            {


                string academicyear = dt.Rows[0]["Description"].ToString();
                if (!academicyear.Contains("Re-appear / Supplementary"))
                {

                    academicyear = academicyear + ")</br> Re-appear / Supplementary";
                    dt.Rows[0]["Description"] = academicyear;
                }
            }
            else
            {
                string academicyear = dt.Rows[0]["Description"].ToString();
                academicyear = academicyear + ")";
                dt.Rows[0]["Description"] = academicyear;
            }
            RepAugust.DataBind();
        }
        catch
        {
        }
    }


    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        GrdExamList.Visible = false;
        BtnSubmit.Visible = false;
        bindStudentExamDetails();
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int i = Convert.ToInt32(row.RowIndex);
        HfEnrollment_No = (HiddenField)row.FindControl("HfEnrollmentNo");
        HFsemester = (HiddenField)row.FindControl("HfSesterr");
        bindAdmitcard();
        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModalB').modal('show');</script>", false);

    }
  
}