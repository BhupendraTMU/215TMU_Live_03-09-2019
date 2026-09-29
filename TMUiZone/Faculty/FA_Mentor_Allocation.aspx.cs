using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class FA_Mentor_Allocation : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
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

                   // SP_FA_Get_Count_Mentee();
                    //SP_FA_MM_Get_Mentnee_Record();
                    //SP_FA_MM_Get_Menters_Record(txt_filterby_name.Text.Trim(), "", "");


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
        
    }
    private void SP_FA_Get_Count_Mentee()
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = con.SP_FA_Get_Count_Mentee(dd_AcademicYear.SelectedValue.ToString(), ddl_course.SelectedValue.ToString(), lbl_TotalMentee.Text.Trim().ToString(), lbl_UnassignedMentee.Text.Trim().ToString(), dd_Semester.SelectedValue.Trim(), dd_Section.SelectedValue.Trim());
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

    public void SP_FA_MM_Get_Mentnee_Record()
    {
        pms_connection con = new pms_connection();

        SqlDataReader drMentee1 = con.SP_FA_MM_Get_Mentnee_Record(txt_Studentfilterby_name.Text.Trim(), dd_AcademicYear.SelectedValue.Trim(), ddl_course.SelectedValue.Trim(), dd_Semester.SelectedValue.Trim(), dd_Section.SelectedValue.Trim());
        DataTable dtMentee1 = new DataTable();
        dtMentee1.Load(drMentee1);
        gv_Mentee.DataSource = dtMentee1;
        gv_Mentee.DataBind();
        drMentee1.Close();
        con.DisConnect();
    }

    public void SP_FA_MM_Get_Menters_Record(string Name_No, string AcademicYear, string Coursecode)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = con.SP_FA_MM_Get_Menters_Record(Name_No.Trim(), AcademicYear.Trim(), Coursecode.Trim(), Session["uid"].ToString().Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        grd_Menter_details_popup.DataSource = dt;
        grd_Menter_details_popup.DataBind();
        dr.Close();
        con.DisConnect();
    }

    private void BindCourses(string loginID, string SP_FA_MM_Get_CourseCode)
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
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    GridView gv_Mentee = (GridView)e.Row.Cells[0].FindControl("gv_Mentee");
        //    Label lbl_No_grid = (Label)e.Row.Cells[0].FindControl("lbl_No_grid");
        //    TextBox txt_Studentfilterby_name = (TextBox)e.Row.Cells[0].FindControl("txt_Studentfilterby_name");
        //    txt_Studentfilterby_name.Text = txt_Studentfilterby_name.Text.Replace(",", "").Trim();

        //    Label lbl_FullName_grid = (Label)e.Row.Cells[0].FindControl("lbl_FullName_grid");
        //    Label lbl_St_Code_grid = (Label)e.Row.Cells[0].FindControl("lbl_St_Code_grid");


        //    // Create a new connection for each query
        //    pms_connection con = new pms_connection();


        //    SqlDataReader drMentee1 = con.SP_FA_MM_Get_Mentnee_Record(txt_Studentfilterby_name.Text.Trim(), dd_AcademicYear.SelectedValue.Trim(), ddl_course.SelectedValue.Trim(),dd_Semester.SelectedValue.Trim(),dd_Section.SelectedValue.Trim());
        //    DataTable dtMentee1 = new DataTable();
        //    dtMentee1.Load(drMentee1);
        //    gv_Mentee.DataSource = dtMentee1;
        //    gv_Mentee.DataBind();
        //    drMentee1.Close();
        //    con.DisConnect();
        //}
    }

    protected void btn_filter_Click1(object sender, EventArgs e)
    {
        //SP_FA_MM_Get_Menters_Record(txt_filterby_name.Text.Trim(), "", "");       
        SP_FA_Get_Count_Mentee();
        SP_FA_MM_Get_Mentnee_Record();
    }
    protected void btn_assign_Mentee_Click(object sender, EventArgs e)
    {
        try
        {
            bool isChecked = false;
            foreach (GridViewRow row in grd_Menter_details_popup.Rows)
            {
                CheckBox chk = row.FindControl("chk_Mentee_Detail") as CheckBox;
                Label Name33 = row.FindControl("lbl_FullName_grid") as Label;
                string nn = Name33.Text;
                if (chk != null && chk.Checked)
                {
                    isChecked = true;
                    break;
                }
            }
            if (!isChecked)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select Menter.');", true);
                return;
            }
            foreach (GridViewRow mentorRow in grd_Menter_details_popup.Rows)
            {
                CheckBox chk_Mentor = (CheckBox)mentorRow.FindControl("chk_Mentee_Detail");
                if (chk_Mentor != null && chk_Mentor.Checked)
                {
                    Label lbl_No_grid = (Label)mentorRow.FindControl("lbl_No_grid");
                    Label lbl_FullName_grid = (Label)mentorRow.FindControl("lbl_FullName_grid");
                    //DropDownList ddl_course = (DropDownList)mentorRow.FindControl("ddl_course");
                    //DropDownList dd_AcademicYear = (DropDownList)mentorRow.FindControl("dd_AcademicYear");
                    // TextBox txt_Studentfilterby_name = (TextBox)mentorRow.FindControl("txt_Studentfilterby_name");
                    // txt_Studentfilterby_name.Text = txt_Studentfilterby_name.Text.Replace(",", "");
                    // Find the nested GridView for mentees
                    // GridView gv_Mentee = (GridView)mentorRow.FindControl("gv_Mentee");

                    foreach (GridViewRow menteeRow in gv_Mentee.Rows)
                    {                       
                        CheckBox chk_Mentee = (CheckBox)menteeRow.FindControl("chk_Mentee");
                        string Allocated = "";
                        string CreatedON = System.DateTime.Now.ToString("dd MMM yyyy HH:mm").Trim();
                      
                        if (chk_Mentee != null && chk_Mentee.Checked)
                        {
                            Allocated = "Yes";                          
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
                        }
                    }
                    
                    md_Employee_Count_Details.Hide();
                    SP_FA_Get_Count_Mentee();
                    SP_FA_MM_Get_Mentnee_Record();
                    up_MenteeCount.Update();
                    up_Mentee.Update();

                    ScriptManager.RegisterStartupScript(up_MentorDetails, up_MentorDetails.GetType(), "HideMentorPopup", "$find('" + Md_md_Menter_details.ClientID + "').hide();", true);

                    up_MentorDetails.Update();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Mentor Assigned SuccesFully');", true);
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void gv_Mentee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gv_Mentor = (GridView)sender;         
            GridViewRow parentRow = e.Row;

            Label lbl_No_grid =
                (Label)parentRow.FindControl("lbl_No_grid");

            Label lbl_FullName_grid =
                (Label)parentRow.FindControl("lbl_FullName_grid");

            Label lbl_St_Code_grid =
                (Label)e.Row.FindControl("lbl_St_Code_grid");

            Label lblAllocated =
                (Label)e.Row.FindControl("lblAllocated");

            CheckBox chk_Mentee =
                (CheckBox)e.Row.FindControl("chk_Mentee");

            LinkButton lnk_remove =
                (LinkButton)e.Row.FindControl("lnk_remove");

            Label lblMenterID =
                (Label)e.Row.FindControl("lblMenterID");

            Label lbl_Academic_Year =
                (Label)e.Row.FindControl("lbl_Academic_Year");

            Label lbl_Course_Code =
                (Label)e.Row.FindControl("lbl_Course_Code");

            Label lbl_St_Name_grid =
                (Label)e.Row.FindControl("lbl_St_Name_grid");


            //if (lblAllocated.Text == "Yes" && lbl_No_grid.Text == lblMenterID.Text.Trim())
            //{
            //    chk_Mentee.Checked = true;

            //}
            if (lblAllocated.Text == "Yes")
            {
                e.Row.Visible = false;   // Hide row
            }
            else
            {
                e.Row.Visible = true;    // Show row
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
        
        pms_connection con = new pms_connection();
        con.SP_FA_MM_Remove_Mentnee_Record(id.ToString().Trim());

        con.DisConnect();
        Response.Redirect("FA_Mentor_Allocation.aspx");
        SP_FA_Get_Count_Mentee();

    }

    protected void btn_Studentfilter_Click(object sender, EventArgs e)
    {
        // foreach (GridViewRow mentorRow in grd_Menter_details_popup.Rows)
        //{
        //   GridView gv_Mentee = (GridView)mentorRow.FindControl("gv_Mentee");

        //  TextBox txt_Studentfilterby_name = (TextBox)mentorRow.FindControl("txt_Studentfilterby_name");
        txt_Studentfilterby_name.Text = txt_Studentfilterby_name.Text.Replace(",", "");
        pms_connection con = new pms_connection();
        SqlDataReader dr = con.SP_FA_MM_Get_Mentnee_Record(txt_Studentfilterby_name.Text.Trim(), dd_AcademicYear.SelectedValue.Trim(), ddl_course.SelectedValue.Trim(), dd_Semester.SelectedValue.Trim(), dd_Section.SelectedValue.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gv_Mentee.DataSource = dt;
        gv_Mentee.DataBind();
        dr.Close();
        con.DisConnect();
        txt_Studentfilterby_name.Text = "";

        //  }
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

        //SP_FA_MM_Get_Menters_Record(txt_filterby_name.Text.Trim(), "", "");
        // SP_FA_Get_Count_Mentee();
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
        // SP_FA_MM_Get_Menters_Record(txt_filterby_name.Text.Trim(), "", "");
        // SP_FA_Get_Count_Mentee();

    }

    protected void lnk_TotalMentee_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Applicablefor_fa"] = "Total";

            lbl_md_txt.Text = "Total No. Of Mentee";

            SP_FA_Get_Count_MenteeDetails(dd_AcademicYear.SelectedValue.Trim().ToString(),ddl_course.SelectedValue.ToString(),ViewState["Applicablefor_fa"].ToString().Trim());
            btn_Reassign.Visible = false;
            md_Employee_Count_Details.Show();
            Md_md_Menter_details.Hide();
            
        }
        catch
        {
        }
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
        lbl_md_txt.Text = "Assigned Mentee";
        SP_FA_Get_Count_MenteeDetails(dd_AcademicYear.SelectedValue.Trim().ToString(), ddl_course.SelectedValue.ToString(), ViewState["Applicablefor_fa"].ToString().Trim());

        // SP_FA_Get_Count_MenteeDetails(dd_AcademicYear.SelectedValue.Trim().ToString(), ddl_course.SelectedValue.ToString(), lbl_UnassignedMentee.Text.Trim());
        // SP_FA_Get_Count_Mentee();

        //  pnl_md_Mentee.Visible = true;
        // md_Employee_Count_Details.Show();

        btn_Reassign.Visible = true;
        md_Employee_Count_Details.Show();
        Md_md_Menter_details.Hide();
      
    }

    protected void md_grd_Mentee_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        md_grd_Mentee.PageIndex = e.NewPageIndex;

        SP_FA_Get_Count_MenteeDetails(dd_AcademicYear.SelectedValue.Trim().ToString(), ddl_course.SelectedValue.ToString(), ViewState["Applicablefor_fa"].ToString().Trim());
        md_Employee_Count_Details.Show();
    }
    public void ExportGridToExcel()
    {
        try
        {
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();

            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;

            Response.AddHeader(
                "Content-Disposition",
                "attachment;filename=Mentee_Export.xls"
            );

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    md_grd_Mentee.AllowPaging = false;

                    md_grd_Mentee.RenderControl(hw);

                    Response.Write("<html><head>");
                    Response.Write("<meta http-equiv='Content-Type' content='text/html; charset=utf-8'/>");
                    Response.Write("</head><body>");

                    Response.Write(sw.ToString());
                    Response.Write("</body></html>");
                }
            }
            Response.Flush();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            // Error handle
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "ExportError",
                "alert('Excel Export Error: " +
                ex.Message.Replace("'", "\\'") +
                "');",
                true
            );
        }
    }

    

    //public void ExportGridToExcel()
    //{
    //    HttpContext.Current.Response.Clear();
    //    HttpContext.Current.Response.Buffer = true;
    //    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "Excel" + ".xls");
    //    HttpContext.Current.Response.Charset = "";
    //    HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
    //    using (StringWriter sw = new StringWriter())
    //    {
    //        HtmlTextWriter hw = new HtmlTextWriter(sw);

    //        // To Export all pages, we need to render the GridView with all data
    //        md_grd_Mentee.RenderControl(hw);

    //        // Style to format the cells
    //        string style = @"<style> .textmode { } </style>";
    //        HttpContext.Current.Response.Write(style);
    //        HttpContext.Current.Response.Output.Write(sw.ToString());
    //        HttpContext.Current.Response.Flush();
    //        HttpContext.Current.Response.End();
    //    }
    //}

    protected void btn_Excel_Export_Click(object sender, EventArgs e)
    {
        //md_grd_Mentee.AllowPaging = false;
        //SP_FA_Get_Count_MenteeDetails(dd_AcademicYear.SelectedValue.Trim().ToString(), ddl_course.SelectedValue.ToString(), "");
       
        SP_FA_Get_Count_MenteeDetails(dd_AcademicYear.SelectedValue.Trim().ToString(), ddl_course.SelectedValue.ToString(), ViewState["Applicablefor_fa"].ToString().Trim());
                 
        ExportGridToExcel();
       // md_grd_Mentee.AllowPaging = true;
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
        // SP_FA_MM_Get_Menters_Record(txt_filterby_name.Text.Trim(), "", "");
        // SP_FA_Get_Count_Mentee();
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
        ///SP_FA_MM_Get_Menters_Record(txt_filterby_name.Text.Trim(), "", "");
        //SP_FA_Get_Count_Mentee();
    }

    protected void btn_AssignMEntorList_Click(object sender, EventArgs e)
    {
        try
        {
            bool isChecked = false;

            foreach (GridViewRow row in gv_Mentee.Rows)
            {
                CheckBox chk = row.FindControl("chk_Mentee") as CheckBox;
                Label Name33 = row.FindControl("lbl_St_Name_grid") as Label;
                string nn = Name33.Text;
                if (chk != null && chk.Checked)
                {
                    isChecked = true;
                    break;
                }
            }

            if (!isChecked)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select at least one Mentee.');", true);
                return;
            }


            // pnl_md_Menter_details.Visible = true;
            Md_md_Menter_details.Show();
            // SP_FA_Get_Count_Mentee();
            // SP_FA_MM_Get_Mentnee_Record();
            SP_FA_MM_Get_Menters_Record(txt_filterby_name.Text.Trim(), "", "");
        }
        catch
        {

        }
    }

    protected void btn_SearchMentee_Click(object sender, EventArgs e)
    {
        try
        {
            pms_connection con = new pms_connection();

            SqlDataReader dr = con.SP_FA_MM_Get_Menters_Record(
                txt_SearchMentee.Text.Trim(),
                "",
                "",
                Session["uid"].ToString().Trim()
            );

            DataTable dt = new DataTable();
            dt.Load(dr);

            grd_Menter_details_popup.DataSource = dt;
            grd_Menter_details_popup.DataBind();

            dr.Close();
            con.DisConnect();

            //up_MentorDetails.Update();

            // Keep popup open
            //Md_md_Menter_details.Show();
        }
        catch (Exception ex)
        {

        }
    }

    public void SP_ReAssign_Mentnee_Record(string St_Code, string St_Name_grid)
    {
        con.Open();
        string sqlq = "SP_ReAssign_Mentnee_Record";
        SqlCommand cmd = new SqlCommand();
        cmd = new SqlCommand(sqlq, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Student_Code", St_Code.Trim());
        cmd.Parameters.AddWithValue("@Student_name", St_Name_grid.Trim());
        cmd.ExecuteNonQuery();
        con.Close();
    }


    protected void btn_Reassign_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnApprovalConfirm.Value == "Yes")
            {


                foreach (GridViewRow mentorRow in md_grd_Mentee.Rows)
                {
                    GridView gv_Mentee = (GridView)mentorRow.FindControl("gv_Mentee");

                    Label lbl_St_Code_grid = (Label)mentorRow.FindControl("lbl_St_Code_grid");
                    Label lbl_St_Name_grid = (Label)mentorRow.FindControl("lbl_St_Name_grid");

                    pms_connection con = new pms_connection();
                    SP_ReAssign_Mentnee_Record(lbl_St_Code_grid.Text.Trim(), lbl_St_Name_grid.Text.Trim());
                    DataTable dt = new DataTable();
                    con.DisConnect();
                }

                SP_FA_Get_Count_Mentee();
                SP_FA_MM_Get_Mentnee_Record();
                up_MenteeCount.Update();
                up_Mentee.Update();
                if (md_grd_Mentee.Rows.Count != 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Mentor has been resigned. Please proceed to assign a new mentor.');", true);
                }
            }
          
        }
        catch (Exception ex)
        {
        }
    }
}