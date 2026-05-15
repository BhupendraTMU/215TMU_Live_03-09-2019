using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Faculty_HandOutUpload : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                 bindAcademicYear();
                 bindDrpCourseList();
                 BindGrid(Session["uid"].ToString());
            }
        }
        catch (Exception ex)
        {
            // Response.Redirect("~/Default.aspx");
        }
    }
    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpCourseList();
        bindSubject();      
    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSubject();   
    }
    public void bindAcademicYear()
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
            drpAcademicYear.DataSource = dt1;
            drpAcademicYear.DataTextField = "Details";
            drpAcademicYear.DataValueField = "No_";
            drpAcademicYear.DataBind();
        }
        catch
        {
        }
    }
    public void bindDrpCourseList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetCourseFromCourseWiseFacultyCollege_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@Academic", drpAcademicYear.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Open();
        da.Fill(dt);
        con.Close();
        drpCourse.DataSource = dt;
        drpCourse.DataTextField = "Details";
        drpCourse.DataValueField = "No_";
        drpCourse.DataBind();
    }
    public void bindSubject()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("sp_getSubjrctCodeFacultyWiseforHandout", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedItem.Value.ToString());
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedItem.Value.ToString());
            cmd.Parameters.Add("@Semester", "");
            

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddlSubject.DataSource = dt;
            ddlSubject.DataTextField = "Description";
            ddlSubject.DataValueField = "Subject Code";
            ddlSubject.DataBind();
            ddlSubject.Items.Insert(0, "--Course--");
        }
        catch (Exception ex) { }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if(drpCourse.SelectedValue== "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select program !')", true);
            return ;
        }
        if (ddlSubject.SelectedValue == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select course !')", true);
            return;
        }

        if (FileUpload1.HasFile)
        {
            string ext = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
            if (ext==".pdf")
            {
                try
                {
                    string folderPath = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    string originalFileName = Path.GetFileNameWithoutExtension(FileUpload1.FileName);
                    string extension = Path.GetExtension(FileUpload1.FileName);
                    string timeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

                    // Final unique filename
                    string fileName = originalFileName + "_" + timeStamp + extension;
                    string fullPath = Path.Combine(folderPath, fileName);

                    FileUpload1.SaveAs(fullPath);

                    // Save file details in DB (optional)
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO FacultyhangOut(AcademicYear, Course, Subject, FilePath, CreateBy, CreatedAt) VALUES (@AcademicYear,@Course,@Subject,@FilePath,@CreateBy,@CreatedAt)", con))
                    {
                        cmd.Parameters.AddWithValue("@AcademicYear", drpAcademicYear.SelectedValue);
                        cmd.Parameters.AddWithValue("@Course", drpCourse.SelectedValue);
                        cmd.Parameters.AddWithValue("@Subject", ddlSubject.SelectedValue);
                        cmd.Parameters.AddWithValue("@FilePath", "~/Uploads/" + fileName);
                        cmd.Parameters.AddWithValue("@CreateBy", Session["uid"].ToString());
                        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('File uploaded successfully'); document.location.href='HandOutUpload.aspx';", true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('File not uploaded'); document.location.href='HandOutUpload.aspx';", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select only pdf file');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select a file');", true);       
        }
    }

    private void BindGrid(string createdBy = null)
    {
        string query = @"
        SELECT [AcademicYear],[Course],[Subject],[FilePath],[CreateBy],[CreatedAt] 
        FROM [EDUCOLLEGELIVE-R2].[dbo].[FacultyhangOut]";

        if (!string.IsNullOrEmpty(createdBy))
        {
            query += " WHERE CreateBy = @CreateBy";
        }

        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            if (!string.IsNullOrEmpty(createdBy))
            {
                cmd.Parameters.AddWithValue("@CreateBy", createdBy);
            }

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvHandOuts.DataSource = dt;
                gvHandOuts.DataBind();
            }
        }
    }



    protected void gvHandOuts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHandOuts.PageIndex = e.NewPageIndex;
        BindGrid(); // rebind data
    }

}