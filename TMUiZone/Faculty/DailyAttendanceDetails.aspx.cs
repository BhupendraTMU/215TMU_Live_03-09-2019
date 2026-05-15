using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class Faculty_DailyAttendanceDetails : System.Web.UI.Page
{
    TMUConnection con; string CollegeCode = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            try
            {
                CollegeCode = Session["GlobalDimension1Code"].ToString();

                GetAttendanceList();

                if (!IsPostBack)
                {

                    //if ((this.Master as IndexMaster).GetLinkYesNo("DailyAttendanceDetails") == "True")
                    if ("True" == "True")
                    {
                        BindCourse();
                        GetFacultyList();
                    }
                    else
                    { Response.Redirect("~/Default.aspx"); }
                    

                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Default.aspx");
            }

        }
        catch { Response.Redirect("~/Default.aspx"); }
    }



    public void GetAttendanceList()
    {
        con = new TMUConnection();
        //  SqlCommand cmd = new SqlCommand("SP_GetDailyAttendanceReport_CollegeWise", con.Con);// 
        SqlCommand cmd = new SqlCommand("SP_GetDailyAttendanceReport_CollegeWise_DEL", con.Con);  //for DENTAL 12-04 2017
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"]);
        cmd.Parameters.Add("@Date", txtDateOfAttendance.Text);
        cmd.Parameters.Add("@course", ddCourse.SelectedValue);
        cmd.Parameters.Add("@Semester", ddSemester_Year.SelectedValue);
        cmd.Parameters.Add("@Section", ddSection.SelectedValue);      
        cmd.Parameters.Add("@Subject", ddSubject.SelectedValue);
        cmd.Parameters.Add("@name", ddlFaculty.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.DisConnect();
        grdAttendance.DataSource = dt;
        grdAttendance.DataBind();
        if (dt.Rows.Count > 0)
        {
            btnExportToExcel.Visible = true;
            Btnpdf.Visible = true;
           
        }
        else
        {
            btnExportToExcel.Visible = false;
            Btnpdf.Visible = false;
          
        }


    }

    public void BindCourse()
    {
        SqlCommand cmd = new SqlCommand("Sp_GetCourseRoleWise_HOD_Role_ForAssignUpload", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@userid", Session["uid"].ToString());
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddCourse.DataSource = dt;
        ddCourse.DataTextField = "No_";
        ddCourse.DataValueField = "No_";
        ddCourse.DataBind();

    }
    public void BindSemester()
    {
        string FacultyCode = "";
        //if (chkboxPrinciple.Checked == false)
        //{
        //    FacultyCode = Session["uid"].ToString();
        //}
        if (Session["UserGroup"].ToString() == "FACULTY") { FacultyCode = Session["uid"].ToString(); }
        SqlCommand cmd = new SqlCommand("proc_GetSemester_Role", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", ddCourse.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", FacultyCode);
        
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddSemester_Year.DataSource = dt;
        ddSemester_Year.DataTextField = "Details";
        ddSemester_Year.DataValueField = "No_";
        ddSemester_Year.DataBind();

    }

    public void bindSectionList()
    {
        string FacultyCode = "";
        //if (chkboxPrinciple.Checked == false)
        //{
        //    FacultyCode = Session["uid"].ToString();
        //}
        if (Session["UserGroup"].ToString() == "FACULTY") { FacultyCode = Session["uid"].ToString(); }
        SqlCommand cmd = new SqlCommand("proc_GetSection_Role", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", ddCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterYear", ddSemester_Year.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", FacultyCode);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddSection.DataSource = dt;
        ddSection.DataTextField = "Details";
        ddSection.DataValueField = "No_";
        ddSection.DataBind();
       
    }

    public void binsubject()
    {


       SqlCommand cmd = new SqlCommand("proc_GetSubjectDdlTheoryLab", con.Con);
       cmd.CommandType = CommandType.StoredProcedure;
       // cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"]);
        cmd.Parameters.Add("@ID", ddCourse.SelectedValue);
        cmd.Parameters.Add("@ID1", ddSemester_Year.SelectedValue);
        
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddSubject.DataSource = dt;
        ddSubject.DataTextField = "Details";
        ddSubject.DataValueField = "No_";
        ddSubject.DataBind();
    }


    public void GetFacultyList()
    {
        con = new TMUConnection();

        SqlCommand cmd1 = new SqlCommand("proc_GetAcademicYear_ForDailyReport", con.Con);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.Add("@AcademicYear", SqlDbType.VarChar, 30);
        cmd1.Parameters["@AcademicYear"].Direction = ParameterDirection.Output;
        con.Con.Open();
        cmd1.ExecuteNonQuery();
        string res = cmd1.Parameters["@AcademicYear"].Value.ToString();
        con.Con.Close();

        SqlCommand cmd = new SqlCommand("SP_GetFacultyFromTimeTable_CollegeWise", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"]);
        cmd.Parameters.Add("@AcademicYear", res);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.DisConnect();
        ddlFaculty.DataSource = dt;
        ddlFaculty.DataTextField = "Name";
        ddlFaculty.DataValueField = "Code";
        ddlFaculty.DataBind();
    }


    protected void ddCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSemester();
       binsubject();
       lblSerchDAte.Text = txtDateOfAttendance.Text;
    }

    protected void ddSemester_Year_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSectionList();
        binsubject();
    }

    protected void grdAttendance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAttendance.PageIndex = e.NewPageIndex;
        GetAttendanceList();
    }
    
    protected void btnExportToExcel_Click(object sender, ImageClickEventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Dailyreport" + DateTime.Now.ToString("dd/MM/yy") + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        // string headerTable = @"<Table><tr><td colspan=10 align=center  bgcolor=gold ><font size=14><h1>Student Fine Report : " + DateTime.Now.ToString("dd-MMM-yyyy") + "</h1></font> </td></tr><tr><td colspan=2 bgcolor=gold><b>Session: " + hfR_Session.Value + "</b></td><td colspan=2 bgcolor=gold><b>By:-" + Session["uname"].ToString() + "</b></td><td colspan=2 bgcolor=gold><b>Course: " + hfR_Course.Value + "</b></td><td colspan=2 bgcolor=gold><b>Semester: " + hfR_SemYear.Value + "</b></td><td colspan=2 bgcolor=gold><b>ActionTaken: " + hfR_Action.Value + "</b></td></tr></Table>";
        // Response.Write(headerTable);
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //To Export all pages
            grdAttendance.AllowPaging = false;
            GetAttendanceList();
            grdAttendance.HeaderRow.BackColor = System.Drawing.Color.DarkSeaGreen;
            foreach (TableCell cell in grdAttendance.HeaderRow.Cells)
            {
                cell.BackColor = grdAttendance.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grdAttendance.Rows)
            {
                row.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grdAttendance.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grdAttendance.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }
            grdAttendance.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    //protected void Btnpdf_Click(object sender, ImageClickEventArgs e)
    //{
    //    using (StringWriter sw = new StringWriter())
    //    {
    //        using (HtmlTextWriter hw = new HtmlTextWriter(sw))
    //        {

    //            //To Export all pages
    //            grdAttendance.AllowPaging = false;
    //            GetAttendanceList();

    //            grdAttendance.RenderControl(hw);
    //            StringReader sr = new StringReader(sw.ToString());
    //            Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
    //            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
    //            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
    //            pdfDoc.Open();
    //            htmlparser.Parse(sr);
    //            pdfDoc.Close();

    //            Response.ContentType = "application/pdf";
    //            Response.AddHeader("content-disposition", "attachment;filename=Dailyreport" + DateTime.Now.ToString("dd/MM/yy") + ".pdf");
    //            //string headerTable = @"<Table><tr><td colspan=10 align=center  bgcolor=gold ><font size=14><h1>Student Fine Report : " + DateTime.Now.ToString("dd-MMM-yyyy") + "</h1></font> </td></tr><tr><td colspan=2 bgcolor=gold><b>Session: " + hfR_Session.Value + "</b></td><td colspan=2 bgcolor=gold><b>By:-" + Session["uname"].ToString() + "</b></td><td colspan=2 bgcolor=gold><b>Course: " + hfR_Course.Value + "</b></td><td colspan=2 bgcolor=gold><b>Semester: " + hfR_SemYear.Value + "</b></td><td colspan=2 bgcolor=gold><b>ActionTaken: " + hfR_Action.Value + "</b></td></tr></Table>";
    //            //Response.Write(headerTable);
    //            Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //            Response.Write(pdfDoc);
    //            //if (btnExportToExcel.Visible == true || Btnpdf.Visible == true)
    //            //{
    //            //    System.Threading.Thread.Sleep(120000);
    //            //}
    //            Response.End();
    //        }
    //    }
    //}

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }

    
    
}