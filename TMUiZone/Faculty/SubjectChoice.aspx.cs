using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_SubjectChoice : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                bindAcademicYear();
                bindSubjectGroup();
                bindGrid();
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Default.aspx");
        }
    }


    public void bindAcademicYear()
    {
        SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da.Fill(dt1);
        drpAcademicYear.DataSource = dt1;
        drpAcademicYear.DataTextField = "Details";
        drpAcademicYear.DataValueField = "No_";
        drpAcademicYear.DataBind();
    }

    public void bindSubjectGroup()
    {
        SqlCommand cmd = new SqlCommand("sp_GetSubjectGroup", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        drpGroupSubject.DataSource = cmd.ExecuteReader();
        drpGroupSubject.DataTextField = "SubjectGroupDescription";
        drpGroupSubject.DataValueField = "SubjectGroupCode";
        drpGroupSubject.DataBind();
        con.Close();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try {
            if (drpGroupSubject.SelectedValue == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Selecting Group Is Mandatory!')", true);
                drpGroupSubject.Focus();
                return;
            }
            else
            {
                SqlCommand cmd = new SqlCommand("sp_insertFacultySubjectsChoice", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SubjectGroupCode", drpGroupSubject.SelectedValue);
                cmd.Parameters.Add("@SubjectGroupDescription", drpGroupSubject.SelectedItem.Text);
                cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
                cmd.Parameters.Add("@UserId", Session["uid"].ToString());
                cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                cmd.Parameters.AddWithValue("@AcademicYear", drpAcademicYear.SelectedValue);

                if (con.State == ConnectionState.Closed)
                    con.Open();
                string result = cmd.ExecuteScalar().ToString();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', '" + result + "');", true);
                con.Close();
                bindGrid();
                bindSubjectGroup();
            }
        }catch(Exception ex) { }
        }

    public void bindGrid()
    {
        SqlCommand cmd = new SqlCommand("select SubjectGroupCode,SubjectGroupDescription,CollegeCode,SubjectClassification from FacultySubjects where FacultyCode='" + Session["uid"].ToString() + "' and [Academic Year]='" + drpAcademicYear.SelectedValue + "'", con);
        if (con.State == ConnectionState.Closed)
            con.Open();
        grdFacultySubject.DataSource = cmd.ExecuteReader();
        grdFacultySubject.DataBind();
        con.Close();
    }
    protected void grdFacultySubject_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string SubjectCode = grdFacultySubject.DataKeys[e.RowIndex].Value.ToString();
        SqlCommand cmd = new SqlCommand("delete from FacultySubjects where SubjectGroupCode='" + SubjectCode + "'", con);
        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        bindGrid();
        bindSubjectGroup();
    }

    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGrid();
    }
}