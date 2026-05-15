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



public partial class AdmitCard : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    int count = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {


                BindAcademicYear();
                bindDrpCourseList();
                getExaminationStudent();
            }

        }
        catch
        {
        }

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

            SqlCommand cmd = new SqlCommand("Sp_GetSemYearFilterByOddEvenExam", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            cmd.Parameters.Add("@CourseCode", ddlCourse.SelectedValue);
            cmd.Parameters.Add("@Examtype", ddlReaapear.SelectedValue);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(dt);
            con.Close();
            ddlSem.DataSource = dt;
            ddlSem.DataTextField = "Details";
            ddlSem.DataValueField = "Code";
            ddlSem.DataBind();
        }
        catch
        {
        }
    }



    protected void ddlAcademicYear_SelectedIndexChanged1(object sender, EventArgs e)
    {
        bindDrpCourseList();
        GrdAppliedExamination.Visible = false;
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpSemesterList();
        GrdAppliedExamination.Visible = false;
        GrdAppliedExamination.Visible = false;
    }



    public void getExaminationStudent()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_FetchAdmitCardStudentDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            //cmd.Parameters.AddWithValue("@EnrollmentNo", "");
            cmd.Parameters.AddWithValue("@Appear", ddlReaapear.SelectedValue);
            cmd.Parameters.AddWithValue("@AcademicYear", ddlAcademicYear.SelectedValue);
            cmd.Parameters.AddWithValue("@CourseCode", ddlCourse.SelectedValue);
            cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            cmd.Parameters.AddWithValue("@LoginId", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@semester", ddlSem.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            GrdAppliedExamination.DataSource = dt;
            GrdAppliedExamination.DataBind();
            rowcount();

        }
        catch (Exception ex)
        {

        }
    }




    protected void DownloadAll_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable dt = new DataTable();

            foreach (GridViewRow row in GrdAppliedExamination.Rows)
            {
                CheckBox check = (CheckBox)row.FindControl("ChkDownload");


                if (check.Checked == true)
                {

                    HFsemester = (HiddenField)row.FindControl("HfSesterr");
                    var id = GrdAppliedExamination.DataKeys[row.RowIndex].Value;
                    SqlCommand cmd = new SqlCommand("Sp_AdmitCardDataFetch_PNC", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EnrollmentNo", id);
                    cmd.Parameters.AddWithValue("@Sem", HFsemester.Value);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    con.Close();
                    da.Fill(dt);
                    if (ddlReaapear.Text == "1")
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string ExaminationType = dt.Rows[i]["ExaminationType"].ToString();
                            if (!ExaminationType.Contains("Re-appear / Supplementary"))
                            {
                                ExaminationType = ExaminationType + " (Re-appear / Supplementary)";
                                dt.Rows[i]["ExaminationType"] = ExaminationType;
                            }
                         
                        }

                    }
                    RepAugust.DataSource = dt;
                    RepAugust.DataBind();
                    //if (dt.Rows[])
                    ddlAcademicYear.Visible = false;
                    ddlCourse.Visible = false;
                    BtnShow.Visible = false;
                    updateheading.Visible = false;
                    PnlFilter.Visible = false;

                    if (dt.Rows.Count > 0)
                    {
                        btntest.Visible = true;
                        RepAugust.Visible = true;
                        btnback.Visible = true;
                        GrdAppliedExamination.Visible = false;
                        ddlReaapear.Visible = false;
                        ddlSem.Visible = false;
                    }
                }
            }



        }
        catch (Exception ex)
        {
        }
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        GrdAppliedExamination.Visible = true;
        btntest.Visible = false;
        RepAugust.Visible = false;
        btnback.Visible = false;
        ddlReaapear.Visible = true;
        ddlSem.Visible = true;
        ddlCourse.Visible = true;
        ddlAcademicYear.Visible = true;
        BtnShow.Visible = true;
        updateheading.Visible = true;
        PnlFilter.Visible = true;


    }

    protected void RepAugust_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {


            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataSet ds = new DataSet();
                string customerId = (e.Item.FindControl("hfCustomerId") as HiddenField).Value;
                int index = e.Item.ItemIndex;
                GridViewRow gvr1 = (GridViewRow)GrdAppliedExamination.Rows[e.Item.ItemIndex];


                HiddenField hf1 = (HiddenField)gvr1.FindControl("HfSesterr");
                string sem1 = hf1.Value;
                string sem = (e.Item.FindControl("HfSem") as HiddenField).Value;
                GridView GrdSubject = e.Item.FindControl("GrdSubjectDetail") as GridView;
                GridView GrdSubject1 = e.Item.FindControl("GrdSubjectDetail1") as GridView;
                HtmlGenericControl dvYN = (HtmlGenericControl)e.Item.FindControl("Extrasubject");
                SqlCommand cmd = new SqlCommand("Sp_FetchAdmitCardExaminationDetailsA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@EnrollmentNo", customerId);
                cmd.Parameters.AddWithValue("@Appear", ddlReaapear.SelectedValue);
                cmd.Parameters.AddWithValue("@Semester", sem1);
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
                //cmd1.Parameters.AddWithValue("@Semester", sem1);
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

    protected void ChlAll_CheckedChanged(object sender, EventArgs e)
    {
        count = 0;
        CheckBox checkBoxheader = (CheckBox)GrdAppliedExamination.HeaderRow.FindControl("ChlAll");
        foreach (GridViewRow Row in GrdAppliedExamination.Rows)
        {

            CheckBox checkRows = (CheckBox)Row.FindControl("ChkDownload");
            if (checkBoxheader.Checked == true)
            {
                count = count + 1;
                checkRows.Checked = true;
            }
            else
            {
                checkRows.Checked = false;
            }

        }
        rowcount();
    }

    //developed by : Bhupendra Yadav
    //Purpose by   : Grid view Selected Row Count
    protected void rowcount()
    {
        int count = 0;

        foreach (GridViewRow Row in GrdAppliedExamination.Rows)
        {

            CheckBox checkRows = (CheckBox)Row.FindControl("ChkDownload");
            if (checkRows.Checked == true)
            {
                count = count + 1;
                checkRows.Checked = true;
            }
            else
            {
                checkRows.Checked = false;
            }

        }
        lblRowNumber.Text = count.ToString();
    }


    protected void ChkDownload_CheckedChanged(object sender, EventArgs e)
    {

        CheckBox checkBoxheader = (CheckBox)GrdAppliedExamination.HeaderRow.FindControl("ChlAll");
        foreach (GridViewRow Row in GrdAppliedExamination.Rows)
        {
            CheckBox checkRows = (CheckBox)Row.FindControl("ChkDownload");
            if (checkRows.Checked == false)
            {

                checkBoxheader.Checked = false;
            }
            else
            {

                //checkBoxheader.Checked = true;
            }


        }
        rowcount();
    }

    protected void ddlReaapear_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GrdAppliedExamination.Visible = true;
        try
        {
            if (ddlReaapear.SelectedValue == "0")
            {
                getExaminationStudent();
            }
            else
            {
                //string sem = (e.Item.FindControl("HfSem") as HiddenField).Value;
                //if (ddlSem.SelectedValue == "")
                //{
                //    GrdAppliedExamination.DataSource =null;
                //    GrdAppliedExamination.DataBind();
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Semester is mandatory')", true);
                //    return;

                //}
                //else
                //{
                SqlCommand cmd = new SqlCommand("Sp_FetchAdmitCardStudentDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Appear", ddlReaapear.SelectedValue);
                cmd.Parameters.AddWithValue("@AcademicYear", ddlAcademicYear.SelectedValue);
                cmd.Parameters.AddWithValue("@CourseCode", ddlCourse.SelectedValue);
                cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                cmd.Parameters.AddWithValue("@LoginId", Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@semester", ddlSem.SelectedValue);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                GrdAppliedExamination.DataSource = dt;
                GrdAppliedExamination.DataBind();
                ddlSem.Visible = true;
            }

        }

        ////}

        catch
        {
        }
        rowcount();
    }

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_FetchAdmitCardStudentDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Appear", ddlReaapear.SelectedValue);
            cmd.Parameters.AddWithValue("@AcademicYear", ddlAcademicYear.SelectedValue);
            cmd.Parameters.AddWithValue("@CourseCode", ddlCourse.SelectedValue);
            cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            cmd.Parameters.AddWithValue("@LoginId", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@semester", ddlSem.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            GrdAppliedExamination.DataSource = dt;
            GrdAppliedExamination.DataBind();
        }
        catch
        {

        }
        rowcount();

    }

    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{

    //}

    protected void BtnShow_Click(object sender, EventArgs e)
    {

        if (ddlSem.SelectedValue == "" && ddlReaapear.SelectedValue == "1")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Semester is mandatory')", true);
            return;
        }
        else
        {
            getExaminationStudent();
            GrdAppliedExamination.Visible = true;
            rowcount();
        }
    }

    protected void GrdAppliedExamination_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdAppliedExamination.PageIndex = e.NewPageIndex;
        getExaminationStudent();
    }

    protected void btntest_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GrdAppliedExamination.Rows)
            {
                CheckBox check = (CheckBox)row.FindControl("chkStudent");
                var id = GrdAppliedExamination.DataKeys[row.RowIndex].Value;
                SqlCommand cmd = new SqlCommand("AdmitCardReleaseFor_AdmitCard_final", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                cmd.Parameters.AddWithValue("@AcadmicYear", ddlAcademicYear.SelectedValue);
                cmd.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@Appear", ddlReaapear.SelectedValue);
                cmd.Parameters.AddWithValue("@EnrollmentNo", id);
                if (check.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@AdmitCardapproval", "2");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@AdmitCardapproval", "1");
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Checkbox not Selected')", true);
                }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        catch
        {
        }
    }


}
