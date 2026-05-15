
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebReference;

public partial class Faculty_OBEReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                bindAcademicYear(); bindDrpCourseList(); bindDrpSemesterList();
                bindSubject();




            }
        }
        catch
        {
            Response.Redirect("../Default.aspx");
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
    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpCourseList();
        //bindReport();
    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpSemesterList();
        // bindReport();
    }
    public void bindDrpSemesterList()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetSemester_Role", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1000000;
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(dt);
            con.Close();
            drpSemester.DataSource = dt;
            drpSemester.DataTextField = "Details";
            drpSemester.DataValueField = "No_";
            drpSemester.DataBind();
            if (Session["drpSemester"].ToString() != null)
            {
                drpSemester.SelectedValue = Session["drpSemester"].ToString();
            }
            else { drpSemester.SelectedValue = "--Semester--"; }
        }
        catch (Exception ex) { }
    }
    protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSubject();

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {

        try
        {
            string Link = "";
            string result = "";
            DataTable dtNAV = new DataTable();
            SqlCommand cmdNAV = new SqlCommand("Proc_GetNAVCreditionalLive", con);
            cmdNAV.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter daNAV = new SqlDataAdapter(cmdNAV);
            daNAV.Fill(dtNAV);
            VoucherPosting nvp = new VoucherPosting();
            nvp.UseDefaultCredentials = true;

            nvp.Url = dtNAV.Rows[0]["URL"].ToString();

            nvp.Credentials = new NetworkCredential(dtNAV.Rows[0]["UserID"].ToString(), dtNAV.Rows[0]["Password"].ToString());
            nvp.Timeout = 3600000;
            result = nvp.POCOPSOREPORT(drpCourse.SelectedValue, ddlSubject.SelectedValue, drpAcademicYear.SelectedValue, Session["uid"].ToString(), Convert.ToDecimal(txtPer.Text.ToString()), drpSemester.SelectedValue, Session["GlobalDimension1Coded"].ToString());


            Link = "C:/COPODETAILS/" + drpCourse.SelectedValue + ".pdf";


            if (Link != "")
            {

                try
                {

                    string filePath = "C:/COPODETAILS/" + drpCourse.SelectedValue + ".pdf";

                    if (System.IO.File.Exists(filePath))
                    {
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", "inline; filename=" + drpCourse.SelectedValue + ".pdf");
                        Response.TransmitFile(filePath);
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                    else
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                    //// Generate file path
                    //string FilePath = Link;

                    //// Check if file exists
                    //if (System.IO.File.Exists(FilePath))
                    //{
                    //    // Send the PDF file path to the client
                    //    string pdfFilePath = "~/Result/" + drpCourse.SelectedValue + ".pdf";
                    //    ViewState["PDFFilePath"] = pdfFilePath;  // Store the file path in ViewState or Session for use on the client side
                    //}
                    //else
                    //{
                    //    Response.Redirect("~/Default.aspx");
                    //}
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Default.aspx");
                }









                //string strFilePath = Link;
                //HttpResponse response = HttpContext.Current.Response;
                //response.Clear();
                //response.AppendHeader("content-disposition", "attachment; filename=" + drpCourse.SelectedValue + ".pdf");
                //response.ContentType = "application/octet-stream";
                //response.WriteFile(strFilePath);
                //response.Flush();
                //response.End();
            }
        }

        catch (Exception ex)
        {
            Response.Redirect("~/Default.aspx");
        }






        //bindReport();
        ////if (lblmsg.Visible != true)
        ////{
        ////    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('OBEResult.aspx', '_blank');", true);
        ////}
    }
    public void bindReport()
    {
        try
        {
            string Link = "";
            string result = "";
            DataTable dtNAV = new DataTable();
            SqlCommand cmdNAV = new SqlCommand("Proc_GetNAVCreditionalLive", con);
            cmdNAV.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter daNAV = new SqlDataAdapter(cmdNAV);
            daNAV.Fill(dtNAV);
            VoucherPosting nvp = new VoucherPosting();
            nvp.UseDefaultCredentials = true;

            nvp.Url = dtNAV.Rows[0]["URL"].ToString();

            nvp.Credentials = new NetworkCredential(dtNAV.Rows[0]["UserID"].ToString(), dtNAV.Rows[0]["Password"].ToString());
            nvp.Timeout = 3600000;
            result = nvp.POCOPSOREPORT(drpCourse.SelectedValue, ddlSubject.SelectedValue, drpAcademicYear.SelectedValue, Session["uid"].ToString(), Convert.ToDecimal(txtPer.Text.ToString()), drpSemester.SelectedValue, Session["GlobalDimension1Coded"].ToString());


            Link = "C://COPODETAILS//" + drpCourse.SelectedValue + ".pdf";


            if (Link != "")
            {


                string strFilePath = Link;
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.AppendHeader("content-disposition", "attachment; filename=" + drpCourse.SelectedValue + ".pdf");
                response.ContentType = "application/octet-stream";
                response.WriteFile(strFilePath);
                response.Flush();
                response.End();
            }
        }

        catch (Exception ex)
        {
            Response.Redirect("~/Default.aspx");
        }

    }



    public void bindSubject()
{
    try
    {
        SqlCommand cmd = new SqlCommand("sp_getSubjrctCodeFacultyWise", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedItem.Value.ToString());
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedItem.Value.ToString());
        cmd.Parameters.Add("@Semester", drpSemester.SelectedItem.Value.ToString());

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlSubject.DataSource = dt;

        ddlSubject.DataTextField = "Description";
        ddlSubject.DataValueField = "Subject Code";
        ddlSubject.DataBind();
        ddlSubject.Items.Insert(0, "--Course--");
        if (Session["Subject"].ToString() != null)
        {
            ddlSubject.SelectedValue = Session["Subject"].ToString();
        }
        else { ddlSubject.SelectedValue = "--Course--"; }
    }
    catch (Exception ex) { }
}
}