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

public partial class IndentList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    DL.FacultyPortalDL FDL = new DL.FacultyPortalDL();
    static Boolean SearchData = false;
          
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {                
                bindAcademicYear();
                bindDdlFaculty();
                bindDdlCourseList();
                bindDdlSubjectList();
                BindAppliedCoursePlanHeaderList();
                BindDdlSearchLine();
            }
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {       
        bindDdlFaculty();
        bindDdlCourseList();
        bindDdlSubjectList();        
       
    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDdlCourseList();
        bindDdlSubjectList();  
    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDdlSubjectList();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindAppliedCoursePlanHeaderList();

    }
    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = grdFacultyCoursePlanHeader.SelectedRow;
        hfDocumentNo.Value =grdFacultyCoursePlanHeader.SelectedDataKey.Value.ToString();            //row.Cells[0].Text;
        BindAppliedCoursePlanLine();
        mpe.Show();
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
    public void bindDdlFaculty()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetFacultyForCoursePlanList", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
        cmd.Parameters.Add("@UserID", Session["uid"].ToString());

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        ddlFaculty.DataSource = dt;
        ddlFaculty.DataTextField = "Details";
        ddlFaculty.DataValueField = "No_";
        ddlFaculty.DataBind();
    }
    public void bindDdlCourseList()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetCourseForCoursePlanList", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
        cmd.Parameters.Add("@UserID", Session["uid"].ToString());
        cmd.Parameters.Add("@FacultyCode", ddlFaculty.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        ddlCourse.DataSource = dt;
        ddlCourse.DataTextField = "Details";
        ddlCourse.DataValueField = "No_";
        ddlCourse.DataBind();
    }   
    public void bindDdlSubjectList()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetSubjectForCoursePlanList", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
        cmd.Parameters.Add("@UserID", Session["uid"].ToString());
        cmd.Parameters.Add("@FacultyCode", ddlFaculty.SelectedValue);
        cmd.Parameters.Add("@CourseCode", ddlCourse.SelectedValue);
        
        
        
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        ddlSubject.DataSource = dt;
        ddlSubject.DataTextField = "Details";
        ddlSubject.DataValueField = "No_";
        ddlSubject.DataBind();
    }
    
    public void BindAppliedCoursePlanHeaderList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetCoursePlanListHeader", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@UserID", Session["uid"].ToString());
        cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", ddlFaculty.SelectedValue);       
        cmd.Parameters.Add("@CourseCode", ddlCourse.SelectedValue);
        cmd.Parameters.Add("@SubjectCode", ddlSubject.SelectedValue);
        cmd.Parameters.Add("@PlanStatus", rblStatus.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        grdFacultyCoursePlanHeader.DataSource = dt;
        grdFacultyCoursePlanHeader.DataBind();
    }
    public void BindAppliedCoursePlanLine()
    {
        SearchData = false;
        SqlCommand cmd = new SqlCommand("proc_GetCoursePlanListLine", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@DocumentNo", hfDocumentNo.Value);
        cmd.Parameters.Add("@Status", rblStatus.SelectedValue);        
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dtLine = new DataTable();        
        da.Fill(dtLine);//da.Fill(dt);
        grdFacultyCoursePlanLine.DataSource = dtLine;
        Session["dtLine"] = dtLine;
        grdFacultyCoursePlanLine.DataBind();       
        showhideCondition();
    }
    public void ApprovedRejectCoursePlan(int ApproveReject,string Remarks)
    {
        txtRemarks.Text = "";
        SqlCommand cmd = new SqlCommand("proc_ApprovedRejectedCoursePlanList", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@DocumentNo", hfDocumentNo.Value);
        cmd.Parameters.Add("@ApprovedReject", ApproveReject);
        cmd.Parameters.Add("@Remarks", Remarks);        
        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();        
        con.Close();
        BindAppliedCoursePlanLine();
        if (ApproveReject == 2)
          ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Approved Successfully');", true);
        else
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Rejected Successfully');", true);
    }
    public void visible(Boolean TF)
    {
        txtRemarks.Visible = TF;
        btnApproved.Visible = TF;
        btnReject.Visible = TF;
       // btnExport.Visible = TF;        
    }
    public void showhideCondition()
    {
        if (grdFacultyCoursePlanLine.Rows.Count > 0)
        {
            mpe.Show();
            visible(true);
            if (rblStatus.SelectedValue.ToString().ToUpper() != "1") { visible(false); } else { visible(true); }
            btnExport.Visible = true;
        }
        else
        {
            visible(false);
            btnExport.Visible = false;
        }
        if (Session["UserGroup"].ToString() != "PRINCIPAL") { visible(false); }
    }
    public void BindDdlSearchLine()
    {
        SqlCommand cmd = new SqlCommand("proc_SearchDdlPlanListLine", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);        
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlSearch.DataSource = dt;
        ddlSearch.DataTextField = "Details";
        ddlSearch.DataValueField = "Code";
        ddlSearch.DataBind();

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {        
        DataTable dtfilter = new DataTable();
        dtfilter = (DataTable)Session["dtLine"];
        if (ddlSearch.SelectedValue != "" && txtSearch.Value !="")
        {            
            var rows = dtfilter.AsEnumerable()
              .Where(i => i.Field<string>(ddlSearch.SelectedValue).ToUpper().Contains(txtSearch.Value.ToUpper()));                             
            var newTable = rows.Any() ? rows.CopyToDataTable() : dtfilter.Clone();
           
            grdFacultyCoursePlanLine.DataSource = newTable;
            grdFacultyCoursePlanLine.DataBind();
            Session["Search"] = newTable;
            if (newTable.Rows.Count == dtfilter.Rows.Count)
            {
                visible(true);
                showhideCondition();
                SearchData = false;
            }
            else
            {
                visible(false);
                SearchData = true;
            }
        }
        else
        {
            SearchData = false;
            grdFacultyCoursePlanLine.DataSource = dtfilter;
            grdFacultyCoursePlanLine.DataBind();
            showhideCondition();
            //if (dtfilter.Rows.Count > 0) { visible(true); }           
        }
       
        mpe.Show();
    }
    protected void btnApproved_Click(object sender, EventArgs e)
    {
        ApprovedRejectCoursePlan(2,txtRemarks.Text);
        mpe.Show();
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        ApprovedRejectCoursePlan(3,txtRemarks.Text);
        mpe.Show();
    }
    

    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        mpe.Show();        
        Response.Clear(); Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=FacultyCourseList.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //To Export all pages
            DataTable dtExport = new DataTable();
            if (SearchData == true) { dtExport = (DataTable)Session["Search"]; } else { dtExport = (DataTable)Session["dtLine"]; }
            
            grdFacultyCoursePlanLine.DataSource = dtExport;
            grdFacultyCoursePlanLine.DataBind();

            grdFacultyCoursePlanLine.HeaderRow.BackColor = Color.YellowGreen;

            foreach (TableCell cell in grdFacultyCoursePlanLine.HeaderRow.Cells)
            {
                cell.BackColor = grdFacultyCoursePlanLine.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grdFacultyCoursePlanLine.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grdFacultyCoursePlanLine.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grdFacultyCoursePlanLine.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }
            
            grdFacultyCoursePlanLine.RenderControl(hw);            
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
}