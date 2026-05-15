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

public partial class Faculty_FacultyLessonPlanMD : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                BindCourse();
            }
            catch
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        SqlCommand cmd1 = new SqlCommand("sp_GetAccessForLessonPlan_RoleMD", con);
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
    
    public void BindCourse()
    {
        string userid = "";
        if (chkboxPrinciple.Checked == true)
        {
            userid = ddlFaculty.SelectedValue;
        }
        else
        {
            userid = Session["uid"].ToString();
        }
        string proc = "Sp_GetCourseRoleWise_Role_ForLessonPlanDM";
        SqlCommand cmd = new SqlCommand(proc, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyId", userid);
        cmd.Parameters.Add("@userid", Session["uid"].ToString());
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpCourse.DataSource = dt;
        drpCourse.DataTextField = "No_";
        drpCourse.DataValueField = "No_";
        drpCourse.DataBind();
    }
    public void BindSemester()
    {
        string userid = "";
        if (chkboxPrinciple.Checked == true)
        {
            userid = ddlFaculty.SelectedValue;
        }
        else
        {
            userid = Session["uid"].ToString();
        }
        string proc = "Sp_GetSemesterRoleWise_Role_ForLessonPlanDM";
        SqlCommand cmd = new SqlCommand(proc, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyId", userid);
        cmd.Parameters.Add("@userid", Session["uid"].ToString());
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpSemesterYear.DataSource = dt;
        drpSemesterYear.DataTextField = "No_";
        drpSemesterYear.DataValueField = "No_";
        drpSemesterYear.DataBind();
    }
    public void BindSection()
    {
        if (chkboxPrinciple.Checked == true)
        {
            SqlCommand cmd = new SqlCommand("proc_GetSection", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@SemesterYear", drpSemesterYear.SelectedValue);
            cmd.Parameters.Add("@FacultyCode", ddlFaculty.SelectedValue);
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
            SqlCommand cmd = new SqlCommand("proc_GetSection", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@SemesterYear", drpSemesterYear.SelectedValue);
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            drpSection.DataSource = dt;
            drpSection.DataTextField = "Details";
            drpSection.DataValueField = "No_";
            drpSection.DataBind();
        }
    }
    public void BindSubject()
    {
         string userid = "";
        if (chkboxPrinciple.Checked == true)
        {
            userid = ddlFaculty.SelectedValue;
        }
        else
        {
            userid = Session["uid"].ToString();
        }
        string proc = "proc_GetSubject_ForLessonPlanMD";
        SqlCommand cmd = new SqlCommand(proc, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterYear", drpSemesterYear.SelectedValue);
        cmd.Parameters.Add("@FacultyId", userid);
        cmd.Parameters.Add("@userid", Session["uid"].ToString());
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpSubjectCode.DataSource = dt;
        drpSubjectCode.DataTextField = "Details";
        drpSubjectCode.DataValueField = "No_";
        drpSubjectCode.DataBind();
    }

    public void BindData()
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
        SqlCommand cmd = new SqlCommand("proc_GetPraposedLessonPlan_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SubjectCode", drpSubjectCode.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", FacultyCode);
        cmd.Parameters.Add("@SemesterYear", drpSemesterYear.SelectedValue);
        cmd.Parameters.Add("@Section", drpSection.SelectedValue);        
        
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
        SqlCommand cmd = new SqlCommand("proc_GetActualLessonPlanDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Course",drpCourse.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", FacultyCode);
        cmd.Parameters.Add("@SemesterYear",drpSemesterYear.SelectedValue);
        cmd.Parameters.Add("@SubjectCode",drpSubjectCode.SelectedValue);
        cmd.Parameters.Add("@Section",drpSection.SelectedValue);
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
        SqlCommand cmd = new SqlCommand("Sp_GetFacultyoleWise_HOD_RoleMD", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@userid", Session["uid"]);  
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"]);        
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlFaculty.DataSource = dt;
        ddlFaculty.DataTextField = "Details";
        ddlFaculty.DataValueField = "No_";
        ddlFaculty.DataBind();
    }
}