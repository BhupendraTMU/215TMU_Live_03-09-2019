using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FA_Mentor_Allocation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["DesignationCode"].ToString() == "D0375" || Session["DesignationCode"].ToString() == "D045" || Session["DesignationCode"].ToString() == "D003" || Session["DesignationCode"].ToString() == "D016" || Session["DesignationCode"].ToString() == "D0376")
            {
                if (!IsPostBack)
                {
                    BindCourses(Session["uid"].ToString().Trim(), Session["GlobalDimension1Coded"].ToString().Trim());
                    SP_FA_Get_Section();
                
                    SP_FA_MM_Get_Semester();
                    GetAcademicYearData();
                    SP_FA_MM_Get_Menters_Record(txt_filterby_name.Text.Trim(), "", "");
                    SP_FA_Get_Count_Mentee();
                
                }
            }
            else
            {
                Response.Redirect("../Default.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../Default.aspx", false);
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    private void SP_FA_Get_Count_Mentee()
    {

        pms_connection con = new pms_connection();
        SqlDataReader dr = con.SP_FA_Get_Count_Mentee(dd_AcademicYear.SelectedValue.ToString(), ddl_course.SelectedValue.ToString(),lbl_TotalMentee.Text.Trim().ToString(), lbl_UnassignedMentee.Text.Trim().ToString(), dd_Semester.SelectedValue.Trim(), dd_Section.SelectedValue.Trim());
        dr.Read();
        if (dr.HasRows)
        {

            int TotalCount = Convert.ToInt32(dr["TotalCount"].ToString().Trim());
            int PendingCount = Convert.ToInt32(dr["PendingCount"].ToString().Trim());

            lbl_TotalMentee.Text = TotalCount.ToString();
            lbl_UnassignedMentee.Text = PendingCount.ToString();

        }


        dr.Close();
        con.DisConnect();
    }

    public void SP_FA_MM_Get_Menters_Record(string Name_No, string AcademicYear, string Coursecode)
    {

        pms_connection con = new pms_connection();
        SqlDataReader dr = con.SP_FA_MM_Get_Menters_Record(Name_No.Trim(), AcademicYear.Trim(), Coursecode.Trim(),Session["uid"].ToString().Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        grd_Menter_details_popup.DataSource = dt;
        grd_Menter_details_popup.DataBind();
        dr.Close();
        con.DisConnect();
    }

    private void BindCourses(string loginID,string SP_FA_MM_Get_CourseCode)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dtcourse = con.SP_FA_MM_Get_CourseCode(loginID.Trim(), SP_FA_MM_Get_CourseCode.Trim());

        ddl_course.DataSource = dtcourse;
        ddl_course.DataTextField = "Course Name";
        ddl_course.DataValueField = "Course Code";
        ddl_course.DataBind();
        dtcourse.Close();
        con.DisConnect();
    }

    private void SP_FA_MM_Get_Semester()
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = con.SP_FA_MM_Get_Semester();
        DataTable dt = new DataTable();
        dt.Load(dr);
        dd_Semester.DataSource = dt;
        dd_Semester.DataTextField = "Semester";
        dd_Semester.DataValueField = "Semester";
        dd_Semester.DataBind();
        dr.Close();
        con.DisConnect();

    }
    private void SP_FA_Get_Section()
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = con.SP_FA_Get_Section();
        DataTable dt = new DataTable();
        dt.Load(dr);
        dd_Section.DataSource = dt;
        dd_Section.DataTextField = "Section";
        dd_Section.DataValueField = "Section";
        dd_Section.DataBind();
        dr.Close();
        con.DisConnect();

    }
    private void GetAcademicYearData()
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = con.SP_FA_MM_Get_Academic_Year();
        DataTable dt = new DataTable();
        dt.Load(dr);
        dd_AcademicYear.DataSource = dt;
        dd_AcademicYear.DataTextField = "Academic Year";
        dd_AcademicYear.DataValueField = "Academic Year";
        dd_AcademicYear.DataBind();
        dr.Close();
        con.DisConnect();

    }
    protected void grd_Menter_details_popup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gv_Mentee = (GridView)e.Row.Cells[0].FindControl("gv_Mentee");
            Label lbl_No_grid = (Label)e.Row.Cells[0].FindControl("lbl_No_grid");
            TextBox txt_Studentfilterby_name = (TextBox)e.Row.Cells[0].FindControl("txt_Studentfilterby_name");
            txt_Studentfilterby_name.Text = txt_Studentfilterby_name.Text.Replace(",", "").Trim();

            Label lbl_FullName_grid = (Label)e.Row.Cells[0].FindControl("lbl_FullName_grid");
            Label lbl_St_Code_grid = (Label)e.Row.Cells[0].FindControl("lbl_St_Code_grid");


            // Create a new connection for each query
            pms_connection con = new pms_connection();


            SqlDataReader drMentee1 = con.SP_FA_MM_Get_Mentnee_Record(txt_Studentfilterby_name.Text.Trim(), dd_AcademicYear.SelectedValue.Trim(), ddl_course.SelectedValue.Trim(),dd_Semester.SelectedValue.Trim(),dd_Section.SelectedValue.Trim());
            DataTable dtMentee1 = new DataTable();
            dtMentee1.Load(drMentee1);
            gv_Mentee.DataSource = dtMentee1;
            gv_Mentee.DataBind();
            drMentee1.Close();
            con.DisConnect();
        }
    }

    protected void btn_filter_Click1(object sender, EventArgs e)
    {
        //SP_FA_MM_Get_Menters_Record(txt_filterby_name.Text.Trim(), "", "");
        SP_FA_MM_Get_Menters_Record(txt_filterby_name.Text.Trim(), "", "");
        SP_FA_Get_Count_Mentee();
    }
    protected void gv_Mentee_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        GridView gv_Mentee = (GridView)sender;
        gv_Mentee.PageIndex = e.NewPageIndex;
        gv_Mentee.PageSize = 20;

        GridViewRow parentRow = (GridViewRow)gv_Mentee.NamingContainer;

        Label lbl_No_grid = (Label)parentRow.FindControl("lbl_No_grid");
        TextBox txt_Studentfilterby_name = (TextBox)parentRow.FindControl("txt_Studentfilterby_name");
        txt_Studentfilterby_name.Text = txt_Studentfilterby_name.Text.Replace(",", "").Trim();

        pms_connection con = new pms_connection();

        SqlDataReader drMentee1 = con.SP_FA_MM_Get_Mentnee_Record(txt_Studentfilterby_name.Text.Trim(), dd_AcademicYear.SelectedValue.Trim(), ddl_course.SelectedValue.Trim(),dd_Semester.SelectedValue.Trim(),dd_Section.SelectedValue.Trim());
        DataTable dtMentee1 = new DataTable();
        dtMentee1.Load(drMentee1);

        gv_Mentee.DataSource = dtMentee1;
        gv_Mentee.DataBind();

        drMentee1.Close();

        con.DisConnect();
    }


    protected void btn_assign_Mentee_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow mentorRow in grd_Menter_details_popup.Rows)
        {
            Label lbl_No_grid = (Label)mentorRow.FindControl("lbl_No_grid");
            Label lbl_FullName_grid = (Label)mentorRow.FindControl("lbl_FullName_grid");
            //DropDownList ddl_course = (DropDownList)mentorRow.FindControl("ddl_course");
            //DropDownList dd_AcademicYear = (DropDownList)mentorRow.FindControl("dd_AcademicYear");
            TextBox txt_Studentfilterby_name = (TextBox)mentorRow.FindControl("txt_Studentfilterby_name");
            txt_Studentfilterby_name.Text = txt_Studentfilterby_name.Text.Replace(",", "");
            // Find the nested GridView for mentees
            GridView gv_Mentee = (GridView)mentorRow.FindControl("gv_Mentee");

            foreach (GridViewRow menteeRow in gv_Mentee.Rows)
            {
                // Find the checkbox in the mentee row
                CheckBox chk_Mentee = (CheckBox)menteeRow.FindControl("chk_Mentee");
                string Allocated = "";
                string CreatedON = System.DateTime.Now.ToString("dd MMM yyyy HH:mm").Trim();

                // Check if the checkbox is checked
                if (chk_Mentee != null && chk_Mentee.Checked)
                {
                    Allocated = "Yes";
                    // Find the labels or other controls to extract data
                    Label lbl_St_Code_grid = (Label)menteeRow.FindControl("lbl_St_Code_grid");
                    Label lbl_St_Name_grid = (Label)menteeRow.FindControl("lbl_St_Name_grid");
                    Label lbl_St_DOB_grid = (Label)menteeRow.FindControl("lbl_St_DOB_grid");
                    Label lbl_St_Father_grd = (Label)menteeRow.FindControl("lbl_St_Father_grd");
                    Label lbl_St_Mother_grd = (Label)menteeRow.FindControl("lbl_St_Mother_grd");
                    Label lbl_St_Mobile_grd = (Label)menteeRow.FindControl("lbl_St_Mobile_grd");




                    pms_connection con = new pms_connection();
                    con.SP_FA_MM_Insert_Mentnee_Record(lbl_St_Code_grid.Text.Trim(), lbl_St_Name_grid.Text.Trim(),
                        lbl_St_DOB_grid.Text.Trim(), lbl_St_Father_grd.Text.Trim(), lbl_St_Mother_grd.Text.Trim(),
                        lbl_St_Mobile_grd.Text.Trim(), lbl_No_grid.Text.Trim(), lbl_FullName_grid.Text.Trim(),
                        dd_AcademicYear.SelectedValue.Trim(), ddl_course.SelectedValue.Trim(), Allocated.Trim(), Session["uid"].ToString(), Session["fulname"].ToString(), CreatedON.Trim());
                    con.DisConnect();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Mentor Assigned SuccesFully');", true);

                }

            }

        }
        SP_FA_MM_Get_Menters_Record(txt_filterby_name.Text.Trim(), "", "");
        SP_FA_Get_Count_Mentee();


    }


    protected void grd_Menter_details_popup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Menter_details_popup.PageIndex = e.NewPageIndex;
        SP_FA_MM_Get_Menters_Record(txt_filterby_name.Text.Trim(), "", "");
        SP_FA_Get_Count_Mentee();


    }
    protected void gv_Mentee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gv_Mentor = (GridView)sender;

            // Access the parent GridView row using NamingContainer
            GridViewRow parentRow = (GridViewRow)gv_Mentor.NamingContainer;

            // Find the Label in the parent row that holds the value of ParentColumn1
            Label lbl_No_grid = (Label)parentRow.FindControl("lbl_No_grid");
            Label lbl_FullName_grid = (Label)parentRow.FindControl("lbl_FullName_grid");

            Label lbl_St_Code_grid = (Label)e.Row.Cells[0].FindControl("lbl_St_Code_grid");

            Label lblAllocated = (Label)e.Row.Cells[0].FindControl("lblAllocated");
            CheckBox chk_Mentee = (CheckBox)e.Row.Cells[0].FindControl("chk_Mentee");
            LinkButton lnk_remove = (LinkButton)e.Row.Cells[0].FindControl("lnk_remove");

            Label lblMenterID = (Label)e.Row.Cells[0].FindControl("lblMenterID");
            Label lbl_Academic_Year = (Label)e.Row.Cells[0].FindControl("lbl_Academic_Year");
            Label lbl_Course_Code = (Label)e.Row.Cells[0].FindControl("lbl_Course_Code");
            Label lbl_St_Name_grid = (Label)e.Row.Cells[0].FindControl("lbl_St_Name_grid");

            if (lblAllocated.Text == "Yes" && lbl_No_grid.Text == lblMenterID.Text.Trim())
            {
                chk_Mentee.Checked = true;

            }
            if (lblAllocated.Text == "Yes" && lbl_No_grid.Text != lblMenterID.Text.Trim())
            {
                e.Row.Visible = false;
            }
            if (chk_Mentee.Checked)
            {
                lnk_remove.Visible = true;
            }
            else
            {
                lnk_remove.Visible = false;
            }

        }
    }


    protected void lnk_remove_Command(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString().Trim();

        // Assuming pms_connection is a defined class and you are calling a stored procedure
        pms_connection con = new pms_connection();
        con.SP_FA_MM_Remove_Mentnee_Record(id.ToString().Trim());

        con.DisConnect();
        Response.Redirect("FA_Mentor_Allocation.aspx");
        SP_FA_Get_Count_Mentee();

    }

    protected void btn_Studentfilter_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow mentorRow in grd_Menter_details_popup.Rows)
        {
            GridView gv_Mentee = (GridView)mentorRow.FindControl("gv_Mentee");

            TextBox txt_Studentfilterby_name = (TextBox)mentorRow.FindControl("txt_Studentfilterby_name");
            txt_Studentfilterby_name.Text = txt_Studentfilterby_name.Text.Replace(",", "");
            pms_connection con = new pms_connection();
            SqlDataReader dr = con.SP_FA_MM_Get_Mentnee_Record(txt_Studentfilterby_name.Text.Trim(), dd_AcademicYear.SelectedValue.Trim(), ddl_course.SelectedValue.Trim(),dd_Semester.SelectedValue.Trim(),dd_Section.SelectedValue.Trim());
            DataTable dt = new DataTable();
            dt.Load(dr);
            gv_Mentee.DataSource = dt;
            gv_Mentee.DataBind();
            dr.Close();
            con.DisConnect();
            txt_Studentfilterby_name.Text = "";



        }
    }
    protected void dd_AcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {

        //foreach (GridViewRow mentorRow in grd_Menter_details_popup.Rows)
        //{
        //    GridView gv_Mentee = (GridView)mentorRow.FindControl("gv_Mentee");

        //    TextBox txt_Studentfilterby_name = (TextBox)mentorRow.FindControl("txt_Studentfilterby_name");
        //    txt_Studentfilterby_name.Text = txt_Studentfilterby_name.Text.Replace(",", "");
        //    pms_connection con = new pms_connection();
        //    SqlDataReader dr = con.SP_FA_MM_Get_Mentnee_Record(txt_Studentfilterby_name.Text.Trim(), dd_AcademicYear.SelectedValue.Trim(), ddl_course.SelectedValue.Trim(),dd_Semester.SelectedValue.Trim(),dd_Section.SelectedValue.Trim());
        //    DataTable dt = new DataTable();
        //    dt.Load(dr);
        //    gv_Mentee.DataSource = dt;
        //    gv_Mentee.DataBind();
        //    dr.Close();
        //    con.DisConnect();
        //    txt_Studentfilterby_name.Text = "";



        //}
        //SP_FA_Get_Count_Mentee();

        SP_FA_MM_Get_Menters_Record(txt_filterby_name.Text.Trim(), "", "");
        SP_FA_Get_Count_Mentee();
    }

    protected void ddl_course_SelectedIndexChanged(object sender, EventArgs e)
    {

        //foreach (GridViewRow mentorRow in grd_Menter_details_popup.Rows)
        //{
        //    GridView gv_Mentee = (GridView)mentorRow.FindControl("gv_Mentee");

        //    TextBox txt_Studentfilterby_name = (TextBox)mentorRow.FindControl("txt_Studentfilterby_name");
        //    txt_Studentfilterby_name.Text = txt_Studentfilterby_name.Text.Replace(",", "");
        //    pms_connection con = new pms_connection();
        //    SqlDataReader dr = con.SP_FA_MM_Get_Mentnee_Record(txt_Studentfilterby_name.Text.Trim(), dd_AcademicYear.SelectedValue.Trim(), ddl_course.SelectedValue.Trim(),dd_Semester.SelectedValue.Trim(),dd_Section.SelectedValue.Trim());
        //    DataTable dt = new DataTable();
        //    dt.Load(dr);
        //    gv_Mentee.DataSource = dt;
        //    gv_Mentee.DataBind();
        //    dr.Close();
        //    con.DisConnect();
        //    txt_Studentfilterby_name.Text = "";


        //}
        SP_FA_MM_Get_Menters_Record(txt_filterby_name.Text.Trim(), "", "");
        SP_FA_Get_Count_Mentee();

    }

    protected void lnk_TotalMentee_Click(object sender, EventArgs e)
    {
        ViewState["Applicablefor_fa"] = "Total";
        lbl_md_txt.Text = "Total No. Of Mentee";

        //lbl_TotalMentee.Text = Convert.ToString("Total");
        SP_FA_Get_Count_MenteeDetails(dd_AcademicYear.SelectedValue.Trim().ToString(), ddl_course.SelectedValue.ToString(), ViewState["Applicablefor_fa"].ToString().Trim());
       // SP_FA_Get_Count_Mentee();
        pnl_md_Mentee.Visible = true;

        md_Employee_Count_Details.Show();
        
    }
    public void SP_FA_Get_Count_MenteeDetails(string Academic_Year, string Course, string Applicable_For)
    {

        pms_connection con = new pms_connection();
        SqlDataReader dr = con.SP_FA_Get_Count_MenteeDetails(dd_AcademicYear.SelectedValue.Trim().ToString(), ddl_course.SelectedValue.ToString(), Applicable_For.Trim(), dd_Semester.SelectedValue.Trim(), dd_Section.SelectedValue.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        md_grd_Mentee.DataSource = dt;
        md_grd_Mentee.DataBind();
        dr.Close();
        con.DisConnect();
    }

    protected void lnl_UnassignedMentee_Click(object sender, EventArgs e)
    {
        //lbl_UnassignedMentee.Text = Convert.ToString("Pending");
        ViewState["Applicablefor_fa"] = "Pending";
        lbl_md_txt.Text = "Unassigned Mentee";
        SP_FA_Get_Count_MenteeDetails(dd_AcademicYear.SelectedValue.Trim().ToString(), ddl_course.SelectedValue.ToString(), ViewState["Applicablefor_fa"].ToString().Trim());

       // SP_FA_Get_Count_MenteeDetails(dd_AcademicYear.SelectedValue.Trim().ToString(), ddl_course.SelectedValue.ToString(), lbl_UnassignedMentee.Text.Trim());
       // SP_FA_Get_Count_Mentee();
        pnl_md_Mentee.Visible = true;
        md_Employee_Count_Details.Show();

    }

    protected void md_grd_Mentee_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        md_grd_Mentee.PageIndex = e.NewPageIndex;

        SP_FA_Get_Count_MenteeDetails(dd_AcademicYear.SelectedValue.Trim().ToString(), ddl_course.SelectedValue.ToString(), ViewState["Applicablefor_fa"].ToString().Trim());
        md_Employee_Count_Details.Show();
    }
    public void ExportGridToExcel()
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "Excel" + ".xls");
        HttpContext.Current.Response.Charset = "";
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            // To Export all pages, we need to render the GridView with all data
            md_grd_Mentee.RenderControl(hw);

            // Style to format the cells
            string style = @"<style> .textmode { } </style>";
            HttpContext.Current.Response.Write(style);
            HttpContext.Current.Response.Output.Write(sw.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }

    protected void btn_Excel_Export_Click(object sender, EventArgs e)
    {
        md_grd_Mentee.AllowPaging = false;
        //SP_FA_Get_Count_MenteeDetails(dd_AcademicYear.SelectedValue.Trim().ToString(), ddl_course.SelectedValue.ToString(), "");
        SP_FA_Get_Count_MenteeDetails(dd_AcademicYear.SelectedValue.Trim().ToString(), ddl_course.SelectedValue.ToString(), ViewState["Applicablefor_fa"].ToString().Trim());
        ExportGridToExcel();
        md_grd_Mentee.AllowPaging = true;
       // SP_FA_Get_Count_MenteeDetails(dd_AcademicYear.SelectedValue.Trim().ToString(), ddl_course.SelectedValue.ToString(), "");


    }

    protected void dd_Semester_SelectedIndexChanged(object sender, EventArgs e)
    {
        //foreach (GridViewRow mentorRow in grd_Menter_details_popup.Rows)
        //{
        //    GridView gv_Mentee = (GridView)mentorRow.FindControl("gv_Mentee");

        //    TextBox txt_Studentfilterby_name = (TextBox)mentorRow.FindControl("txt_Studentfilterby_name");
        //    txt_Studentfilterby_name.Text = txt_Studentfilterby_name.Text.Replace(",", "");
        //    pms_connection con = new pms_connection();
        //    SqlDataReader dr = con.SP_FA_MM_Get_Mentnee_Record(txt_Studentfilterby_name.Text.Trim(), dd_AcademicYear.SelectedValue.Trim(), ddl_course.SelectedValue.Trim(), dd_Semester.SelectedValue.Trim(), dd_Section.SelectedValue.Trim());
        //    DataTable dt = new DataTable();
        //    dt.Load(dr);
        //    gv_Mentee.DataSource = dt;
        //    gv_Mentee.DataBind();
        //    dr.Close();
        //    con.DisConnect();
        //    txt_Studentfilterby_name.Text = "";


        //}
        //SP_FA_Get_Count_Mentee();
        SP_FA_MM_Get_Menters_Record(txt_filterby_name.Text.Trim(), "", "");
        SP_FA_Get_Count_Mentee();
    }

    protected void dd_Section_SelectedIndexChanged(object sender, EventArgs e)
    {
        //foreach (GridViewRow mentorRow in grd_Menter_details_popup.Rows)
        //{
        //    GridView gv_Mentee = (GridView)mentorRow.FindControl("gv_Mentee");

        //    TextBox txt_Studentfilterby_name = (TextBox)mentorRow.FindControl("txt_Studentfilterby_name");
        //    txt_Studentfilterby_name.Text = txt_Studentfilterby_name.Text.Replace(",", "");
        //    pms_connection con = new pms_connection();
        //    SqlDataReader dr = con.SP_FA_MM_Get_Mentnee_Record(txt_Studentfilterby_name.Text.Trim(), dd_AcademicYear.SelectedValue.Trim(), ddl_course.SelectedValue.Trim(), dd_Semester.SelectedValue.Trim(), dd_Section.SelectedValue.Trim());
        //    DataTable dt = new DataTable();
        //    dt.Load(dr);
        //    gv_Mentee.DataSource = dt;
        //    gv_Mentee.DataBind();
        //    dr.Close();
        //    con.DisConnect();
        //    txt_Studentfilterby_name.Text = "";


        //}
        //SP_FA_Get_Count_Mentee();
        SP_FA_MM_Get_Menters_Record(txt_filterby_name.Text.Trim(), "", "");
        SP_FA_Get_Count_Mentee();
    }
}