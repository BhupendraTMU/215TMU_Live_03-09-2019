using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

public partial class Faculty_HandOutDownload : System.Web.UI.Page
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
                BindGrid();
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
            SqlCommand cmd = new SqlCommand(@"
            SELECT DISTINCT [AcademicYear] AS No_, 
                            CAST([AcademicYear] AS VARCHAR) AS Details
            FROM [EDUCOLLEGELIVE-R2].[dbo].[FacultyhangOut]
            ORDER BY [AcademicYear] DESC", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            con.Open();
            da.Fill(dt1);
            con.Close();

            drpAcademicYear.DataSource = dt1;
            drpAcademicYear.DataTextField = "Details";
            drpAcademicYear.DataValueField = "No_";
            drpAcademicYear.DataBind();

            drpAcademicYear.Items.Insert(0, new ListItem("-- Select Academic Year --", "0"));
        }
        catch (Exception ex)
        {
            // handle error logging
            throw;
        }
    }

    public void bindDrpCourseList()
    {
        try
        {
            SqlCommand cmd = new SqlCommand(@"
            SELECT DISTINCT [Course] AS No_,
                            [Course] AS Details
            FROM [EDUCOLLEGELIVE-R2].[dbo].[FacultyhangOut]
            WHERE [AcademicYear] = @AcademicYear
            ORDER BY [Course]", con);

            cmd.Parameters.AddWithValue("@AcademicYear", drpAcademicYear.SelectedValue);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();

            drpCourse.DataSource = dt;
            drpCourse.DataTextField = "Details";
            drpCourse.DataValueField = "No_";
            drpCourse.DataBind();

            drpCourse.Items.Insert(0, new ListItem("-- Select Course --", "0"));
        }
        catch (Exception ex)
        {
            // handle error logging
            throw;
        }
    }

    public void bindSubject()
    {
        try
        {
            SqlCommand cmd = new SqlCommand(@"
            SELECT DISTINCT [Subject] AS [SubjectCode],
                            [Subject] AS [Description]
            FROM [EDUCOLLEGELIVE-R2].[dbo].[FacultyhangOut]
            WHERE [AcademicYear] = @AcademicYear
              AND [Course] = @Course
            ORDER BY [Subject]", con);

            cmd.Parameters.AddWithValue("@AcademicYear", drpAcademicYear.SelectedValue);
            cmd.Parameters.AddWithValue("@Course", drpCourse.SelectedValue);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlSubject.DataSource = dt;
            ddlSubject.DataTextField = "Description";
            ddlSubject.DataValueField = "SubjectCode";
            ddlSubject.DataBind();

            ddlSubject.Items.Insert(0, new ListItem("-- Select Subject --", "0"));
        }
        catch (Exception ex)
        {
            // log error
            throw;
        }
    }

    private void BindGrid(string academicYear = null, string course = null, string subject = null)
    {
        string query = @"
        SELECT [AcademicYear],[Course],[Subject],[FilePath],[CreateBy],[CreatedAt] 
        FROM [EDUCOLLEGELIVE-R2].[dbo].[FacultyhangOut]
        WHERE 1=1";   // <-- always true, so we can append conditions dynamically

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = con;

            if (!string.IsNullOrEmpty(academicYear))
            {
                query += " AND AcademicYear = @AcademicYear";
                cmd.Parameters.AddWithValue("@AcademicYear", academicYear);
            }

            if (!string.IsNullOrEmpty(course) || !string.IsNullOrEmpty(subject))
            {
                query += " AND (";

                if (!string.IsNullOrEmpty(course))
                {
                    query += " Course = @Course";
                    cmd.Parameters.AddWithValue("@Course", course);
                }

                if (!string.IsNullOrEmpty(subject))
                {
                    if (!string.IsNullOrEmpty(course))
                        query += " OR";

                    query += " Subject = @Subject";
                    cmd.Parameters.AddWithValue("@Subject", subject);
                }

                query += " )";
            }


            cmd.CommandText = query;

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

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindGrid(drpAcademicYear.SelectedValue, drpCourse.SelectedValue, ddlSubject.SelectedValue);
    }
    }