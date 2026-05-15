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

public partial class Faculty_ReleaseMarksEntry : System.Web.UI.Page
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

            }
        }
        catch (Exception ex)
        {
            // Response.Redirect("~/Default.aspx");
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
            da.Fill(dt1);
            drpAcademicYear.DataSource = dt1;
            drpAcademicYear.DataTextField = "Details";
            drpAcademicYear.DataValueField = "No_";
            drpAcademicYear.DataBind();
        }
        catch
        {
        }
    }

    public void bindDrpSemesterList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetSemesterFromCourseWiseFaculty_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID1", Session["uid"].ToString());
        cmd.Parameters.Add("@ID", drpCourse.SelectedValue);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSemester.DataSource = dt;
        drpSemester.DataTextField = "Details";
        drpSemester.DataValueField = "No_";
        drpSemester.DataBind();
    }


    public void bindDrpCourseList()
    {
        //SqlCommand cmd = new SqlCommand("proc_GetCourseFromCourseWiseFacultyCollege_Role", con);
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.Add("@ID", Session["uid"].ToString());
        //cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());

        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //da.Fill(dt);
        //drpCourse.DataSource = dt;
        //drpCourse.DataTextField = "Details";
        //drpCourse.DataValueField = "No_";
        //drpCourse.DataBind();


        SqlCommand cmd = new SqlCommand("proc_GetCourse_RoleMatrix", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpCourse.DataSource = dt;
        drpCourse.DataTextField = "Details";
        drpCourse.DataValueField = "No_";
        drpCourse.DataBind();
    }
   
    
    public void bindgrid()
    {
        try
        {

            SqlCommand cmd = new SqlCommand("usp_GetMarkEntrytable1", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AcadmicYear", drpAcademicYear.SelectedValue);
            cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            cmd.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.AddWithValue("@Semester", drpSemester.SelectedValue);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            grdmarktable.DataSource = dt;
            grdmarktable.DataBind();
            grdmarktable.Visible = true;
            if (dt.Rows.Count > 0)
            {

            }
        }
        catch
        {
        }
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        bindgrid();

        grdViewmarksEntrySubmit.Visible = false;
        lblError.Text = "";

        btnSubmit.Visible = false;
        brnReject.Visible = false;

    }
    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        grdViewmarksEntrySubmit.Visible = false;
        grdmarktable.Visible = false;
        btnSubmit.Visible = false;
        brnReject.Visible = false;
        bindDrpSemesterList();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)    
    {
        try
        {


            lblError.Text = "";
            //Boolean checsubmit1 = false;
            Button btn = sender as Button;
            GridViewRow grow = btn.NamingContainer as GridViewRow;

            SqlCommand cmd3 = new SqlCommand("usp_GetCheckMarkEntry1", con);
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.Parameters.AddWithValue("@AcadmicYear", drpAcademicYear.SelectedValue);
            cmd3.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            cmd3.Parameters.AddWithValue("@CourseCode", drpCourse.SelectedValue);
            cmd3.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
            cmd3.Parameters.AddWithValue("@Semester", drpSemester.SelectedValue);
            cmd3.Parameters.AddWithValue("@SubjectCode", Session["hfSubject"].ToString());
            cmd3.Parameters.AddWithValue("@SubjectType", Session["grdlblSubjectType"].ToString());
            cmd3.Parameters.AddWithValue("@DocumentNo", Session["grdlblDocumentNo"].ToString());
            cmd3.Parameters.AddWithValue("@Status", Session["hfStatus"].ToString());
            cmd3.Parameters.AddWithValue("@Method", Session["grdlblMethod"].ToString());
            cmd3.Parameters.AddWithValue("@Group", Session["grdlblgroup"].ToString());
            SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
            DataTable dt3 = new DataTable();
            con.Open();
            da3.Fill(dt3);
            con.Close();
            if (dt3.Rows.Count > 0)
            {
                int i = 0;

                if (Convert.ToString(dt3.Rows[i]["Status"]) == "3")
                {


                    foreach (GridViewRow row in grdViewmarksEntrySubmit.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            TextBox grdtxtMarks = (TextBox)row.FindControl("grdtxtMarks");
                            string txtMark = (row.FindControl("grdtxtMarks") as TextBox).Text;

                            if (txtMark == "")
                            {
                                // txtMark = "0";
                                lblError.Text = "Marks can't be blank.";
                                grdtxtMarks.Focus();
                                grdtxtMarks.BorderStyle = BorderStyle.Solid;
                                grdtxtMarks.BorderColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                    }
                    // ChkBoxRows.Checked = true;
                    foreach (GridViewRow row in grdViewmarksEntrySubmit.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            var id = grdViewmarksEntrySubmit.DataKeys[row.RowIndex].Value;
                            SqlCommand cmd2 = new SqlCommand("proc_MarksEntryLineupdateHod", con);
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                            cmd2.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
                            cmd2.Parameters.AddWithValue("@StudentNo", id);
                            cmd2.Parameters.AddWithValue("@Status", "4");
                            cmd2.Parameters.AddWithValue("@StatusL", "4");
                            cmd2.Parameters.AddWithValue("@AcadmicYear", drpAcademicYear.SelectedValue);
                            cmd2.Parameters.AddWithValue("@CourseCode", drpCourse.SelectedValue);
                            cmd2.Parameters.AddWithValue("@Semester", drpSemester.SelectedValue);
                            cmd2.Parameters.AddWithValue("@SubjectCode", Session["hfSubject"].ToString());
                            cmd2.Parameters.AddWithValue("@SubjectType", Session["grdlblSubjectType"].ToString());
                            cmd2.Parameters.AddWithValue("@DocumentNo", Session["grdlblDocumentNo"].ToString());
                            cmd2.Parameters.AddWithValue("@Method", Session["grdlblMethod"].ToString());
                            cmd2.Parameters.AddWithValue("@Group", Session["grdlblgroup"].ToString());
                            con.Open();
                            cmd2.ExecuteNonQuery();
                            con.Close();
                        }
                    }

                    Session["grdlblDocumentNo"] = null;
                    Session["grdlblMethod"] = null;
                    Session["grdlblgroup"] = null;
                    Session["hfSubject"] = null;
                    Session["hfStatus"] = null;
                    Session["grdlblSubjectType"] = null;
                    btnSubmit.Visible = false;
                    brnReject.Visible = false;
                    grdViewmarksEntrySubmit.Visible = false;
                    bindgrid();
                    grdmarktable.Visible = true;
                }
            }
        }
        catch
        {
        }
    }
    protected void btntblmarksshow_Click(object sender, EventArgs e)
    {
        try
        {

            LinkButton btn = sender as LinkButton;
            GridViewRow grow = btn.NamingContainer as GridViewRow;

            Label grdlblDocumentNo = (Label)grow.FindControl("grdlblDocumentNo");
            Label grdlblMethod = (Label)grow.FindControl("grdlblMethod");
            Label grdlblgroup = (Label)grow.FindControl("grdlblgroup");
            Label grdlblSubjectType = (Label)grow.FindControl("grdlblSubjectType");
            HiddenField hfStatus = (HiddenField)grow.FindControl("hfStatus");
            HiddenField hfSubject = (HiddenField)grow.FindControl("hfSubject");
            Session["grdlblDocumentNo"] = grdlblDocumentNo.Text;
            Session["grdlblMethod"] = grdlblMethod.Text;
            Session["grdlblgroup"] = grdlblgroup.Text;
            Session["hfSubject"] = hfSubject.Value;
            Session["hfStatus"] = hfStatus.Value;
            Session["grdlblSubjectType"] = grdlblSubjectType.Text;

            SqlCommand cmd1 = new SqlCommand("usp_GetCheckMarkEntry1", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            cmd1.Parameters.AddWithValue("@AcadmicYear", drpAcademicYear.SelectedValue);
            cmd1.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
            cmd1.Parameters.AddWithValue("@CourseCode", drpCourse.SelectedValue);
            cmd1.Parameters.AddWithValue("@Semester", drpSemester.SelectedValue);
            cmd1.Parameters.AddWithValue("@SubjectCode", hfSubject.Value);
            cmd1.Parameters.AddWithValue("@SubjectType", grdlblSubjectType.Text);
            cmd1.Parameters.AddWithValue("@DocumentNo", grdlblDocumentNo.Text);
            cmd1.Parameters.AddWithValue("@Status", hfStatus.Value);
            cmd1.Parameters.AddWithValue("@Method", grdlblMethod.Text);
            cmd1.Parameters.AddWithValue("@Group", grdlblgroup.Text);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            con.Open();
            da1.Fill(dt1);
            con.Close();
            if (dt1.Rows.Count > 0)
            {
                int i = 0;


                if (Convert.ToString(dt1.Rows[i]["Status"]) == "0" || Convert.ToString(dt1.Rows[i]["Status"]) == "1" || Convert.ToString(dt1.Rows[i]["Status"]) == "2" )
                {
                    grdViewmarksEntrySubmit.Visible = false;
                    btnSubmit.Visible = false;
                    brnReject.Visible = false;
                }

                else if (Convert.ToString(dt1.Rows[i]["Status"]) == "3")
                {
                    btnSubmit.Text = "Locked";
                    brnReject.Text = "Reject";
                    SqlCommand cmd2 = new SqlCommand("usp_GetMarkEntryStudentSubmit", con);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                    cmd2.Parameters.AddWithValue("@AcadmicYear", drpAcademicYear.SelectedValue);
                    cmd2.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
                    cmd2.Parameters.AddWithValue("@CourseCode", drpCourse.SelectedValue);
                    cmd2.Parameters.AddWithValue("@Semester", drpSemester.SelectedValue);
                    cmd2.Parameters.AddWithValue("@SubjectCode", hfSubject.Value);
                    cmd2.Parameters.AddWithValue("@SubjectType", grdlblSubjectType.Text);
                    cmd2.Parameters.AddWithValue("@DocumentNo", grdlblDocumentNo.Text);
                    cmd2.Parameters.AddWithValue("@Status", hfStatus.Value);
                    cmd2.Parameters.AddWithValue("@Method", grdlblMethod.Text);
                    cmd2.Parameters.AddWithValue("@Group", grdlblgroup.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable();
                    con.Open();
                    da.Fill(dt2);
                    con.Close();
                    grdViewmarksEntrySubmit.DataSource = dt2;
                    grdViewmarksEntrySubmit.DataBind();
                    if (dt2.Rows.Count > 0)
                    {
                        btnSubmit.Visible = true;
                        brnReject.Visible = true;
                        foreach (GridViewRow row in grdViewmarksEntrySubmit.Rows)
                        {
                            DropDownList ddp = (DropDownList)row.FindControl("grdattedGet");
                            TextBox grdmark = (TextBox)row.FindControl("grdtxtMarks");
                            grdmark.Enabled = false;
                            ddp.Enabled = false;
                        }

                        grdmarktable.Visible = false;

                    }
                    grdViewmarksEntrySubmit.Visible = true;
                }
                else if (Convert.ToString(dt1.Rows[i]["Status"]) == "4")
                {

                    SqlCommand cmd2 = new SqlCommand("usp_GetMarkEntryStudentSubmit", con);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                    cmd2.Parameters.AddWithValue("@AcadmicYear", drpAcademicYear.SelectedValue);
                    cmd2.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
                    cmd2.Parameters.AddWithValue("@CourseCode", drpCourse.SelectedValue);
                    cmd2.Parameters.AddWithValue("@Semester", drpSemester.SelectedValue);
                    cmd2.Parameters.AddWithValue("@SubjectCode", hfSubject.Value);
                    cmd2.Parameters.AddWithValue("@SubjectType", grdlblSubjectType.Text);
                    cmd2.Parameters.AddWithValue("@DocumentNo", grdlblDocumentNo.Text);
                    cmd2.Parameters.AddWithValue("@Status", hfStatus.Value);
                    cmd2.Parameters.AddWithValue("@Method", grdlblMethod.Text);
                    cmd2.Parameters.AddWithValue("@Group", grdlblgroup.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable();
                    con.Open();
                    da.Fill(dt2);
                    con.Close();
                    grdViewmarksEntrySubmit.DataSource = dt2;
                    grdViewmarksEntrySubmit.DataBind();
                    if (dt2.Rows.Count > 0)
                    {
                        btnSubmit.Visible = false;
                        brnReject.Visible = false;
                        foreach (GridViewRow row in grdViewmarksEntrySubmit.Rows)
                        {
                            DropDownList ddp = (DropDownList)row.FindControl("grdattedGet");
                            TextBox grdmark = (TextBox)row.FindControl("grdtxtMarks");
                            grdmark.Enabled = false;
                            ddp.Enabled = false;
                        }

                        grdmarktable.Visible = false;

                    }
                    grdViewmarksEntrySubmit.Visible = true;
                }

            }


        }
        catch
        {
        }
       
       
    }
   
    
    
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        grdViewmarksEntrySubmit.Visible = false;
        grdmarktable.Visible = false;
        btnSubmit.Visible = false;
        brnReject.Visible = false;
        bindDrpSemesterList();
    }
    protected void brnReject_Click(object sender, EventArgs e)
    {
        try
        {

            lblError.Text = "";
            //Boolean checsubmit1 = false;
            Button btn = sender as Button;
            GridViewRow grow = btn.NamingContainer as GridViewRow;

            SqlCommand cmd3 = new SqlCommand("usp_GetCheckMarkEntry1", con);
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.Parameters.AddWithValue("@AcadmicYear", drpAcademicYear.SelectedValue);
            cmd3.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            cmd3.Parameters.AddWithValue("@CourseCode", drpCourse.SelectedValue);
            cmd3.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
            cmd3.Parameters.AddWithValue("@Semester", drpSemester.SelectedValue);
            cmd3.Parameters.AddWithValue("@SubjectCode", Session["hfSubject"].ToString());
            cmd3.Parameters.AddWithValue("@SubjectType", Session["grdlblSubjectType"].ToString());
            cmd3.Parameters.AddWithValue("@DocumentNo", Session["grdlblDocumentNo"].ToString());
            cmd3.Parameters.AddWithValue("@Status", Session["hfStatus"].ToString());
            cmd3.Parameters.AddWithValue("@Method", Session["grdlblMethod"].ToString());
            cmd3.Parameters.AddWithValue("@Group", Session["grdlblgroup"].ToString());
            SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
            DataTable dt3 = new DataTable();
            con.Open();
            da3.Fill(dt3);
            con.Close();
            if (dt3.Rows.Count > 0)
            {
                int i = 0;

                if (Convert.ToString(dt3.Rows[i]["Status"]) == "3")
                {


                    foreach (GridViewRow row in grdViewmarksEntrySubmit.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            TextBox grdtxtMarks = (TextBox)row.FindControl("grdtxtMarks");
                            string txtMark = (row.FindControl("grdtxtMarks") as TextBox).Text;

                            if (txtMark == "")
                            {
                                // txtMark = "0";
                                lblError.Text = "Marks can't be blank.";
                                grdtxtMarks.Focus();
                                grdtxtMarks.BorderStyle = BorderStyle.Solid;
                                grdtxtMarks.BorderColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                    }
                    // ChkBoxRows.Checked = true;
                    foreach (GridViewRow row in grdViewmarksEntrySubmit.Rows)
                    {
                        CheckBox chek = (CheckBox)row.FindControl("SChkD");
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            var id = grdViewmarksEntrySubmit.DataKeys[row.RowIndex].Value;
                            SqlCommand cmd2 = new SqlCommand("proc_MarksEntryLineupdateHod", con);
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                            cmd2.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
                            cmd2.Parameters.AddWithValue("@StudentNo", id);
                            if (chek.Checked == true)
                            {
                                cmd2.Parameters.AddWithValue("@Status", "1");
                            }
                            else
                            {
                                cmd2.Parameters.AddWithValue("@Status", "2");
                            }
                            cmd2.Parameters.AddWithValue("@StatusL", "1");
                            cmd2.Parameters.AddWithValue("@AcadmicYear", drpAcademicYear.SelectedValue);
                            cmd2.Parameters.AddWithValue("@CourseCode", drpCourse.SelectedValue);
                            cmd2.Parameters.AddWithValue("@Semester", drpSemester.SelectedValue);
                            cmd2.Parameters.AddWithValue("@SubjectType", Session["grdlblSubjectType"].ToString());
                            cmd2.Parameters.AddWithValue("@SubjectCode", Session["hfSubject"].ToString());
                            cmd2.Parameters.AddWithValue("@DocumentNo", Session["grdlblDocumentNo"].ToString());
                            cmd2.Parameters.AddWithValue("@Method", Session["grdlblMethod"].ToString());
                            cmd2.Parameters.AddWithValue("@Group", Session["grdlblgroup"].ToString());
                            con.Open();
                            cmd2.ExecuteNonQuery();
                            con.Close();
                        }
                    }

                    Session["grdlblDocumentNo"] = null;
                    Session["grdlblMethod"] = null;
                    Session["grdlblgroup"] = null;
                    Session["hfSubject"] = null;
                    Session["hfStatus"] = null;
                    Session["grdlblSubjectType"] = null;
                    btnSubmit.Visible = false;
                    brnReject.Visible = false;
                    grdViewmarksEntrySubmit.Visible = false;
                    bindgrid();
                    grdmarktable.Visible = true;
                }
            }
        }
        catch
        {
        }
    }
    protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        grdViewmarksEntrySubmit.Visible = false;
        grdmarktable.Visible = false;
        btnSubmit.Visible = false;
        brnReject.Visible = false;
    }
}