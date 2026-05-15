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
using Utility;

public partial class Faculty_FacultyLessonPlan : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["uid"].ToString() == "")
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        catch
        { 
            Response.Redirect("~/Default.aspx"); 
        }
        if (!IsPostBack)
        {
            try
            {
                bindAcademicYear();
                BindCourse();
            }
            catch
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        SqlCommand cmd1 = new SqlCommand("sp_GetAccessForTimeTable_Role", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.Add("@userid", Session["uid"].ToString());
        cmd1.Parameters.Add("@Return1", SqlDbType.VarChar, 30);
        cmd1.Parameters["@Return1"].Direction = ParameterDirection.Output;
        con.Open();
        cmd1.ExecuteNonQuery();
        string res = cmd1.Parameters["@Return1"].Value.ToString();
        con.Close();

        if (res == "1")
            chkboxPrinciple.Visible = true;
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        lblActualLessonPlan.Visible = true;
        lblProposedLessonPlan.Visible = true;
        BindData();
        BindActualLessonPlan();
    }

    public void BindCourse() //Add Academic Year
    {
        if (chkboxPrinciple.Checked == true)
        {
            string qry = "";
            qry = "[Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "'  and [Faculty Code]='" + ddlFaculty.SelectedValue + "' and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'";
            //(select [Global Dimension 1 Code] from [TMU$Employee] where No_='" + Session["uid"].ToString() + "')";
            SqlCommand cmd = new SqlCommand("select '' as No_,'-- Course --'   as Details union select [Course Code] as No_,[Course Code]   as Details  from [TMU$Course Wise Faculty] where " + qry + " and [Portal ID]='1' and [Course Code] in (SELECT [Course Code] FROM [TMU$User Role Matrix] WHERE HOD = '" + Session["uid"].ToString() + "' or Principal='" + Session["uid"].ToString() + "' or [Course Co-Ordinator]='" + Session["uid"].ToString() + "' or [Class Co-Ordinator]='" + Session["uid"].ToString() + "')", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            drpCourse.DataSource = dt;
            drpCourse.DataTextField = "Details";
            drpCourse.DataValueField = "No_";
            drpCourse.DataBind();
        }
        else
        {
            string qry = "";
            qry = "[Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "'  and [Faculty Code]='" + Session["uid"].ToString() + "' and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' ";
            //(select [Global Dimension 1 Code] from [TMU$Employee] where No_='" + Session["uid"].ToString() + "')";
            SqlCommand cmd = new SqlCommand("select '' as No_,'-- Course --'   as Details union select [Course Code] as No_,[Course Code]   as Details  from [TMU$Course Wise Faculty] where " + qry + " and [Portal ID]='1' ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            drpCourse.DataSource = dt;
            drpCourse.DataTextField = "Details";
            drpCourse.DataValueField = "No_";
            drpCourse.DataBind();
        }
    }
    public void BindSemester() //Add Academic Year
    {
        if (chkboxPrinciple.Checked == true)
        {
            SqlCommand cmd = new SqlCommand("proc_GetSemesterAcademicYearWise", con);  //proc_GetSemester // on 06-10-2017 by Ashu
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@FacultyCode", ddlFaculty.SelectedValue);
            cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue); // on 06-10-2017 by Ashu
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            drpSemesterYear.DataSource = dt;
            drpSemesterYear.DataTextField = "Details";
            drpSemesterYear.DataValueField = "No_";
            drpSemesterYear.DataBind();
        }
        else
        {
            SqlCommand cmd = new SqlCommand("proc_GetSemesterAcademicYearWise", con);  //proc_GetSemester // on 06-10-2017 by Ashu
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue); // on 06-10-2017 by Ashu
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            drpSemesterYear.DataSource = dt;
            drpSemesterYear.DataTextField = "Details";
            drpSemesterYear.DataValueField = "No_";
            drpSemesterYear.DataBind();
        }
    }
    public void BindSection() //Add Academic Year
    {
        if (chkboxPrinciple.Checked == true)
        {
            SqlCommand cmd = new SqlCommand("proc_GetSectionAcademicYearWise", con); //proc_GetSection  // on 06-10-2017 by Ashu
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@SemesterYear", drpSemesterYear.SelectedValue);
            cmd.Parameters.Add("@FacultyCode", ddlFaculty.SelectedValue);
            cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue); // on 06-10-2017 by Ashu
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            drpSection.DataSource = dt;
            drpSection.DataTextField = "Details";
            drpSection.DataValueField = "No_";
            drpSection.DataBind();
        }
        else
        {
            SqlCommand cmd = new SqlCommand("proc_GetSectionAcademicYearWise", con);//proc_GetSection // on 06-10-2017 by Ashu
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@SemesterYear", drpSemesterYear.SelectedValue);
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue); // on 06-10-2017 by Ashu
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            drpSection.DataSource = dt;
            drpSection.DataTextField = "Details";
            drpSection.DataValueField = "No_";
            drpSection.DataBind();
        }
    }
    public void BindSubject()  //Add Academic Year
    {
        if (chkboxPrinciple.Checked == true)
        {
            SqlCommand cmd = new SqlCommand("proc_GetSubjectAcademicYearWiseproc_GetSubject", con); //proc_GetSubject comment on 06-10-2017
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@SemesterYear", drpSemesterYear.SelectedValue);
            cmd.Parameters.Add("@FacultyCode", ddlFaculty.SelectedValue);
            cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue); // on 06-10-2017 by Ashu
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            drpSubjectCode.DataSource = dt;
            drpSubjectCode.DataTextField = "Details";
            drpSubjectCode.DataValueField = "No_";
            drpSubjectCode.DataBind();
        }
        else
        {
            SqlCommand cmd = new SqlCommand("proc_GetSubjectAcademicYearWise", con); //proc_GetSubject comment on 06-10-2017
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@SemesterYear", drpSemesterYear.SelectedValue);
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue); // on 06-10-2017 by Ashu
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            drpSubjectCode.DataSource = dt;
            drpSubjectCode.DataTextField = "Details";
            drpSubjectCode.DataValueField = "No_";
            drpSubjectCode.DataBind();
        }
    }

    public void BindData()// Add Academic Year
    {
        string FacultyCode = "";
        if (chkboxPrinciple.Checked == false)
        {
            FacultyCode = Session["uid"].ToString();
        }        
        else
        {
            FacultyCode = ddlFaculty.SelectedValue;
        }
        SqlCommand cmd = new SqlCommand("proc_GetPraposedLessonPlan_RoleAcademicYearWise", con); //proc_GetPraposedLessonPlan_Role Comment on 0610-2017
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SubjectCode", drpSubjectCode.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", FacultyCode);
        cmd.Parameters.Add("@SemesterYear", drpSemesterYear.SelectedValue);
        cmd.Parameters.Add("@Section", drpSection.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue); // on 06-10-2017 by Ashu
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        grdLessonPlan.DataSource = dt;
        grdLessonPlan.DataBind();
        if (dt.Rows.Count > 0)
        {
            btnExportToexcellProp.Visible = true;
        }
    }
    protected void drpCourse_SelectedIndexChanged1(object sender, EventArgs e)
    {
        BindSemester();
    }
    protected void chkboxPrinciple_CheckedChanged(object sender, EventArgs e)
    {
        if (chkboxPrinciple.Checked == true)
        {
            lblFaculty.Visible = true;
            ddlFaculty.Visible = true;
            rfvFaculty.Visible = true;
            BindFaculty();
            BindCourse();
        }
        else
        {
            lblFaculty.Visible = false;
            ddlFaculty.Visible = false;
            rfvFaculty.Visible = false;
            BindCourse();
        }
        
    }
    
    protected void drpSemesterYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubject();
        BindSection();
    }
    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubject();
    }
    
    public void BindActualLessonPlan()
    {
        string FacultyCode = "";
        if (chkboxPrinciple.Checked == false)
        {
            FacultyCode = Session["uid"].ToString();
        }
        else
        {
            FacultyCode = ddlFaculty.SelectedValue;
        }
        SqlCommand cmd = new SqlCommand("proc_GetActualLessonPlanDetailsAcademicYearWise", con); //proc_GetActualLessonPlanDetails comment on 07-10-2017 by ashu
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Course",drpCourse.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", FacultyCode);
        cmd.Parameters.Add("@SemesterYear",drpSemesterYear.SelectedValue);
        cmd.Parameters.Add("@SubjectCode",drpSubjectCode.SelectedValue);
        cmd.Parameters.Add("@Section",drpSection.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue); // on 06-10-2017 by Ashu
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt=new DataTable ();
        da.Fill(dt);
        grdActualLessonPlan.DataSource = dt;
        grdActualLessonPlan.DataBind();
        if(dt.Rows.Count>0)
        { btnExportToexcellAct.Visible = true; 
        }
    }
    protected void btnExportToexcellProp_Click(object sender, ImageClickEventArgs e)
    {

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=ProposedLessonPlan.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //To Export all pages
            grdLessonPlan.AllowPaging = false;
            BindData();
            grdLessonPlan.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grdLessonPlan.HeaderRow.Cells)
            {
                cell.BackColor = grdLessonPlan.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grdLessonPlan.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grdLessonPlan.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grdLessonPlan.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }
            grdLessonPlan.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }
    protected void btnExportToexcellAct_Click(object sender, ImageClickEventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=ActualLessonPlan.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //To Export all pages
            grdActualLessonPlan.AllowPaging = false;
            BindActualLessonPlan();
            grdActualLessonPlan.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grdActualLessonPlan.HeaderRow.Cells)
            {
                cell.BackColor = grdActualLessonPlan.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grdActualLessonPlan.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grdActualLessonPlan.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grdActualLessonPlan.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }
            grdActualLessonPlan.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblActualLessonPlan.Visible = false;
        lblProposedLessonPlan.Visible = false;
        BindCourse();
        BindData();
    }
    public void BindFaculty()
    {
        SqlCommand cmd = new SqlCommand("Sp_GetFacultyoleWise_HOD_RoleAcademicYearWise", con);// Sp_GetFacultyoleWise_HOD_Role //comment on 06-10-2017 by Ashu
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@userid", Session["uid"]);  
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"]);
        cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue); // on 06-10-2017 by Ashu
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlFaculty.DataSource = dt;
        ddlFaculty.DataTextField = "Details";
        ddlFaculty.DataValueField = "No_";
        ddlFaculty.DataBind();
    }
    public void bindAcademicYear()
    {
        SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da.Fill(dt1);
        ddlAcademicYear.DataSource = dt1;
        ddlAcademicYear.DataTextField = "Details";
        ddlAcademicYear.DataValueField = "No_";
        ddlAcademicYear.DataBind();        
    }
    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
}