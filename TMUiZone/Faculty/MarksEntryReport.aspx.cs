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

public partial class MarksEntryReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    DL.FacultyPortalDL FDL = new DL.FacultyPortalDL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                BindDropdown();
                
            }
        }
        catch (Exception ex)
        {
            // Response.Redirect("~/Default.aspx");
        }
    }
    public void BindDropdown()
    {
        //SqlCommand cmd = new SqlCommand("select distinct([Course Code]) from [TMU$Course Wise Faculty] where [Faculty Code]='" + Session["uid"].ToString() + "'", con); //Ashu 01-12-2106
        SqlCommand cmd = new SqlCommand("Sp_getCourseindropdown", con); //Ashu 
        cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@uid", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Gd1", Session["GlobalDimension1Code"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlcourse.DataSource = dt;
        ddlcourse.DataTextField = "Course Code";
        ddlcourse.DataBind();
        ddlcourse.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));
    }
    public void bindFacultyCode()
    {
        SqlCommand cmd = new SqlCommand("Sp_getFacultyInDropdown", con); //Ashu 
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Course",ddlcourse.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpFaculty.DataSource = dt;
        drpFaculty.DataTextField = "Faculty Name";
        drpFaculty.DataValueField = "Faculty Code";
        drpFaculty.DataBind();
        drpFaculty.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));

    }

    protected void ddlcourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindFacultyCode();
    }
}