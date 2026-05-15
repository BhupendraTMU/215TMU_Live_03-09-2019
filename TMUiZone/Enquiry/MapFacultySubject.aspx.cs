using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DL;


public partial class Enquiry_MapFacultySubject : System.Web.UI.Page
{   
    DL.FacultyPortalDL FDL = new DL.FacultyPortalDL();
    EnquiryDL FacultySubjectDL = new EnquiryDL();
    MapFacultySubjectDL objMapFacultySubjectDL = new MapFacultySubjectDL();
    string UserName = string.Empty; static string Search="";    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["uid"] != null)
        {
          
            if (!IsPostBack)
            {
                bindDrpCourseList();
                //BindCourseDdl();
                bindSemester();
                BindSubjectDdl();
                BindSubjectListGrid();
                BindDdlSearch();
                Search = "";
            }
        }
        else
        {
            Response.Redirect("../Default.aspx");
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        string Result = objMapFacultySubjectDL.SaveMapFacultySubject(ddlCourse.SelectedValue.ToString(), ddlSubject.SelectedValue.ToString(), Session["uid"].ToString(), Session["uid"].ToString(), Session["GlobalDimension1Code"].ToString(),ddlSemester.SelectedValue);
        if (Result.Contains("Duplicate") == false)
        {
            BindSubjectListGrid();
            Clear();
            lblMsg.ForeColor = System.Drawing.Color.Green;
        }
        
        lblMsg.Text = Result;
        mpe.Show();
    }

    public void bindDrpCourseList()
    {
        DataTable dt = new DataTable();
        dt = objMapFacultySubjectDL.GetCourseDdl(Session["GlobalDimension1Code"].ToString());
        ddlCourse.DataSource = dt;
        ddlCourse.DataTextField = "Details";
        ddlCourse.DataValueField = "No_";
        ddlCourse.DataBind();
    }

    public void BindCourseDdl()
    {
        DataTable dt = new DataTable();        
        dt = FacultySubjectDL.GetCourseDdl();
        ddlCourse.DataTextField = "Details";
        ddlCourse.DataValueField = "No_";
        ddlCourse.DataSource = dt;
        ddlCourse.DataBind();
    }
    public void BindSubjectDdl()
    {
        DataTable dt = new DataTable();
        dt = objMapFacultySubjectDL.GetSubjectDdl(ddlCourse.SelectedValue, ddlSemester.SelectedValue);
        ddlSubject.DataTextField = "Details";
        ddlSubject.DataValueField = "No_";
        ddlSubject.DataSource = dt;
        ddlSubject.DataBind();
    }
    public void bindSemester()
    {
        DataTable dt = new DataTable();
        dt = objMapFacultySubjectDL.GetSemYear(ddlCourse.SelectedValue);
        ddlSemester.DataTextField = "Semester/Year";
        ddlSemester.DataValueField = "No_";
        ddlSemester.DataSource = dt;
        ddlSemester.DataBind();
        
    }
    public void BindSearchGrid()
    {
        Search = "Search";
        DataTable dtList = new DataTable();
        dtList = (DataTable)ViewState["MapFacultyList"];
        if (dtList.Rows.Count > 0)
        {
            DataView dv = new DataView(dtList);
            if (ddlCourse.SelectedValue != "")
            {
                ddlSearch.SelectedIndex = -1;
                txtSearch.Text = "";
                dv.RowFilter = "Course like '%" + ddlCourse.SelectedValue + "%'";
            }
            if(ddlCourse.SelectedValue != "" && ddlSemester.SelectedValue!="")
            {
                ddlSearch.SelectedIndex = -1;
                txtSearch.Text = "";
                dv.RowFilter = "CourseCode = '" + ddlCourse.SelectedValue + "' AND SemYear = '" + ddlSemester.SelectedValue + "'";
               // dv.RowFilter = "SemYear = '" + ddlSemester.SelectedValue + "'";
            }
            if (ddlSubject.SelectedValue != "" && ddlCourse.SelectedValue != "" && ddlSemester.SelectedValue != "")
            {
                ddlSearch.SelectedIndex = -1;
                txtSearch.Text = "";
                dv.RowFilter = "SubjectCode like '%" + ddlSubject.SelectedValue + "%' AND CourseCode = '" + ddlCourse.SelectedValue + "' AND SemYear = '" + ddlSemester.SelectedValue + "'";
            }
            if (ddlSearch.SelectedValue == "1")
            {
                dv.RowFilter = "Name like '%" + txtSearch.Text + "%'";

            }
            if (ddlSearch.SelectedValue == "2")
            {
                dv.RowFilter = "Course like '%" + txtSearch.Text + "%'";
            }
            if (ddlSearch.SelectedValue == "3")
            {
                dv.RowFilter = "Subject like '%" + txtSearch.Text + "%'";
            }
            
            grdFacultySubject.DataSource = dv;
            grdFacultySubject.DataBind();
            if (dtList.Rows.Count > 0)
            {
                if (dtList.Rows[0]["UserGroup"].ToString() == "REGISTRAR")
                {                    
                    grdFacultySubject.Columns[grdFacultySubject.Columns.Count - 1].Visible = false;
                    
                  
                }
                else
                {
                   // Subject.Visible = false;
                    
                }
                if (dtList.Rows[0]["UserGroup"].ToString() == "PRINCIPAL")
                {
                    grdFacultySubject.Columns[grdFacultySubject.Columns.Count - 1].Visible = true;
                  //  Subject.Visible = true;
                    
                  

                }
                

            }
        }
        chkenable();
    }
    public void BindDdlSearch()
    {
        DataTable dt = new DataTable();

        dt = objMapFacultySubjectDL.GetSearchEnquiryDdl();
        ddlSearch.DataSource = dt;
        ddlSearch.DataTextField = "Details";
        ddlSearch.DataValueField = "No_";
        ddlSearch.DataBind();

    }
    public void BindSubjectListGrid()
    {
        Search = "";
        DataTable dt = new DataTable();
        dt = objMapFacultySubjectDL.GetMappedFacultySubject(Session["uid"].ToString(), Session["GlobalDimension1Code"].ToString(), Session["UserGroup"].ToString());
        if (dt.Rows.Count > 0)
        {
            grdFacultySubject.DataSource = dt;
            grdFacultySubject.DataBind();
            if (Session["UserGroup"].ToString() != "REGISTRAR")
            {
                grdFacultySubject.Columns[grdFacultySubject.Columns.Count - 1].Visible = false;

            }

            else
            {
               // Subject.Visible = false;
            }
            if (Session["UserGroup"].ToString() == "PRINCIPAL")
            {
                grdFacultySubject.Columns[grdFacultySubject.Columns.Count - 1].Visible = true;
               // Subject.Visible = true;


            }
          
            chkenable();
            Clear();
        }
        else
        {
            //btnapp.Visible = false;
        }
        ViewState["MapFacultyList"] = dt;
    }
    public void Clear()
    {
        ddlCourse.SelectedIndex = -1;
        ddlSubject.SelectedIndex = -1;
        ddlSemester.SelectedIndex = -1;
    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSemester();
        
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {     
       BindSearchGrid();
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {       
        Response.Redirect("MapFacultySubject.aspx");
    }
    protected void grdFacultySubject_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdFacultySubject.PageIndex = e.NewPageIndex;
        if (Search == "Search")
        {
            BindSearchGrid();
        }
        else
        {
            BindSubjectListGrid();
        }
        
    }
    
    protected void grdFacultySubject_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        lblMsg.Text="";
        Int32 ID = Convert.ToInt32(grdFacultySubject.DataKeys[e.RowIndex].Value.ToString());
        objMapFacultySubjectDL.DeleteMappedFacultySubject(ID, Session["uid"].ToString());
        lblMsg.Text = "Record Deleted !";       
        BindSubjectListGrid();
        mpe.Show();
    }


    public bool  updatesubject(string str,int i)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("proc_assignMappedFacultySubject", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", str);
            cmd.Parameters.AddWithValue("@strID", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@app", i);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            bool success = true;
            if (dt1.Rows.Count > 0 && dt1.Columns.Count>1)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Load Exceed Limit');", true);
                success = false;
            }
            if (dt1.Rows.Count > 0 && dt1.Columns.Count == 1)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', '" + dt1.Rows[0]["Error"].ToString() + "');", true);
                success = false;
            }
            return success;
           // grvGrid.DataSource = dt1;
            //grvGrid.DataBind();
            //con.Open();
            //cmd.ExecuteNonQuery();
            //con.Close();
        }
    }
    public void chkenable()
    {
        if (Session["UserGroup"].ToString() != "PRINCIPAL")
        {
          //  btnapp.Visible = false;
            foreach (GridViewRow gdv in grdFacultySubject.Rows)
            {
                CheckBox chk = (CheckBox)gdv.FindControl("cbRows");
                int nstockid = Convert.ToInt32(grdFacultySubject.DataKeys[gdv.RowIndex].Value);
                chk.Enabled = false;
                {
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand("select ApprovedSubject from TMUFacultySubjects where  ID=" + nstockid + "", con);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        dr.Read();
                        if (dr["ApprovedSubject"].ToString() == "1")
                        {

                            chk.Checked = true;
                        }
                        if (dr["ApprovedSubject"].ToString() == "0")
                        {

                            chk.Checked = false;
                        }
                        con.Close();
                    }
                }
            }
        }
        else
        {
            foreach (GridViewRow gdv in grdFacultySubject.Rows)
            {
                CheckBox chk = (CheckBox)gdv.FindControl("cbRows");
                int nstockid = Convert.ToInt32(grdFacultySubject.DataKeys[gdv.RowIndex].Value);
                
                {
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand("select ApprovedSubject from TMUFacultySubjects where  ID=" + nstockid + "", con);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        dr.Read();
                        if (dr["ApprovedSubject"].ToString() == "1")
                        {

                            chk.Checked = true;
                        }
                        if (dr["ApprovedSubject"].ToString() == "0")
                        {

                            chk.Checked = false;
                        }
                        con.Close();
                    }
                }
            }
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
    }
   
    protected void cbRows_CheckedChanged(object sender, EventArgs e)
    {        
        GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
        int index = row.RowIndex;
        CheckBox chk = (CheckBox)grdFacultySubject.Rows[index].FindControl("cbRows");
        //CheckBox chk = (CheckBox)grdFacultySubject.FindControl("cbRows");
        bool success = true;
        if (chk.Checked == true)
        {
            int nstockid = Convert.ToInt32(grdFacultySubject.DataKeys[index].Value);
            success=updatesubject(nstockid.ToString(), 1);

        }
        if (chk.Checked == false)
        {
            int nstockid = Convert.ToInt32(grdFacultySubject.DataKeys[index].Value);
            success=updatesubject(nstockid.ToString(), 0);
        }
        if (success == false) { chk.Checked = false; }

    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubjectDdl();
    }
}