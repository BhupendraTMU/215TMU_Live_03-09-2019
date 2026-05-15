using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
public partial class Student_ExamResultDisplay : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                bindsemester();
                bindStduentGradeReport();
                bindStduentPVT();
               
            }
        }
        catch (Exception ex) { }
    }
    public void bindsemester()
    {
        SqlCommand cmd = new SqlCommand("BindSemester", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@StudentNo", Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlSem.DataSource = dt;
        ddlSem.DataTextField = "sem";
        ddlSem.DataValueField = "semcode";
        ddlSem.DataBind();

    }
    public void bindStduentGradeReport()
    {
        try
        {

            SqlCommand cmd = new SqlCommand("Sp_getExamResultDisplayTheory", con);//sp_bindfacultyFeedbackreportforHOD
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UID", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Semester", ddlSem.SelectedValue);
             SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            GrdExamResults.DataSource = dt;
            GrdExamResults.DataBind();

            if (dt.Rows.Count > 0)
            {
                BtnPrint.Visible = true;
                pnlheader.Visible = true;
            }
            else
            {
                BtnPrint.Visible = false;
                pnlheader.Visible = false;
            }
        }
        catch (Exception ex) { }
    }

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindStduentGradeReport();
        bindStduentPVT();

    }
    public void bindStduentPVT()
    {
        try
        {

            SqlCommand cmd = new SqlCommand("Sp_getExamResultDisplayPractical", con);//sp_bindfacultyFeedbackreportforHOD
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UID", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Semester", ddlSem.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            GridPV.DataSource = dt;
            GridPV.DataBind();

          
        }
        catch (Exception ex) { }
    }

}