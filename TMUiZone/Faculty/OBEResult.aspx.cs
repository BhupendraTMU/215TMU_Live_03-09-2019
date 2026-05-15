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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Text.RegularExpressions;
using System.Net;
using System.Text;
using WebReference;

public partial class Faculty_OBEResult : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());

    DL.FacultyPortalDL FDL = new DL.FacultyPortalDL();
    int prentcount = 0;
    int UFMCount = 0;
    int AbCount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {


                //bindAcademicYear();
                bindDrpCourseList();


            }
        }
        catch (Exception ex)
        {
            // Response.Redirect("~/Default.aspx");
        }



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
        SqlCommand cmd = new SqlCommand("proc_GetCourseFromPEAD", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        cmd.Parameters.Add("@ID1", drpAcademicYear.SelectedValue);

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
    public void bindDrpSemesterList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetSemesterFromPEAD", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 1000000;

        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        cmd.Parameters.Add("@AcYear", drpAcademicYear.SelectedValue);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        con.Open();
        da.Fill(dt);
        con.Close();
        drpSemester.DataSource = dt;
        drpSemester.DataTextField = "Details";
        drpSemester.DataValueField = "No_";
        drpSemester.DataBind();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("proc_GetResultsOBE", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@Program", drpCourse.SelectedValue);
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@Semester", drpSemester.SelectedValue);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Open();
        da.Fill(dt);
        con.Close();
        grdResult.DataSource = dt;
        grdResult.DataBind();

    }
   

    protected void lnkResAn_Click(object sender, EventArgs e)
    {

        string Link = "";
        LinkButton btn = sender as LinkButton;
        GridViewRow row = btn.NamingContainer as GridViewRow;
        string pk = grdResult.DataKeys[row.RowIndex].Values[0].ToString();


        DataTable dtNAV = new DataTable();
        SqlCommand cmdNAV = new SqlCommand("Proc_GetNAVCreditionalLive", con);
        cmdNAV.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter daNAV = new SqlDataAdapter(cmdNAV);
        daNAV.Fill(dtNAV);
        VoucherPosting nvp = new VoucherPosting();
        nvp.UseDefaultCredentials = true;
        nvp.Url = dtNAV.Rows[0]["URL"].ToString();
        nvp.Credentials = new NetworkCredential(dtNAV.Rows[0]["UserID"].ToString(), dtNAV.Rows[0]["Password"].ToString());
        Link = nvp.ExporDecodePublishedDataPrincipal(pk);
        Link = "C://tab//ReportDecode.pdf";


        if (Link != "")
        {


            string strFilePath = "C://tab//ReportDecode.pdf";
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.AppendHeader("content-disposition", "attachment; filename=" + "ReportDecode.pdf");
            response.ContentType = "application/octet-stream";
            response.WriteFile(strFilePath);
            response.Flush();
            response.End();
        }
    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpSemesterList();
    }
}