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

public partial class Faculty_DownloadResult : System.Web.UI.Page
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


                bindAcademicYear();
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
        SqlCommand cmd = new SqlCommand("proc_GetCourseFromCourseWiseFacultyCollege_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@Academic", drpAcademicYear.SelectedItem.Text);
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

    protected void btnShow_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("proc_GetResults", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@Program", drpCourse.SelectedValue);
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Open();
        da.Fill(dt);
        con.Close();
        grdResult.DataSource = dt;
        grdResult.DataBind();

    }
    protected void lnkTeb_Click(object sender, EventArgs e)
    {
        try
        {

            string Link = "";
            LinkButton btn = sender as LinkButton;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            string pk = grdResult.DataKeys[row.RowIndex].Values[0].ToString();
            con.Open();
            SqlCommand cmd = new SqlCommand("proc_GetOpenElectforResult", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ResultNo", pk);
            cmd.Parameters.Add("@ID", Session["uid"].ToString());


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            grdOpenElective.DataSource = dt;
            grdOpenElective.DataBind();

            if (dt.Rows.Count > 0)
            {
                ModalPopupMsg.Show();

            }
            else
            {
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
                Link = nvp.ExporTabulationReportPDF(pk, "", false, false, false);
                Link = "C://tab//ReportTabulation.pdf";


                if (Link != "")
                {
                    byte[] bytes = GetData("select  [Tabulation Report Pdf] from [TMU$Posted Student Ext_Int Header] where No_ = '" + pk + "'").Rows[0]["Tabulation Report Pdf"].ToString() == "" ? null : (byte[])GetData("select  [Tabulation Report Pdf] from [TMU$Posted Student Ext_Int Header] where No_ = '" + pk + "'").Rows[0]["Tabulation Report Pdf"];

                    using (SqlCommand cmd1 = new SqlCommand())
                    {
                        cmd1.CommandText = "update [TMU$Posted Student Ext_Int Header] set [Tabulation Report Pdf]=''  where No_ = '" + pk + "'";
                        cmd1.Connection = con;
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();
                    }

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ReportTabulation.pdf");
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();

                    //string strFilePath = "C://tab//ReportTabulation.pdf";
                    //HttpResponse response = HttpContext.Current.Response;
                    //response.Clear();
                    //response.AppendHeader("content-disposition", "attachment; filename=" + "ReportTabulation.pdf");
                    //response.ContentType = "application/octet-stream";
                    //response.WriteFile(strFilePath);
                    //response.Flush();
                    //response.End();
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Time out error,Please try after some time');", true);
        }









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
        nvp.Timeout = 3600000;
        nvp.Credentials = new NetworkCredential(dtNAV.Rows[0]["UserID"].ToString(), dtNAV.Rows[0]["Password"].ToString());
        Link = nvp.ExporAnalysisReportPDF(pk);
        Link = "C://tab//ReportAnalysis.pdf";


        if (Link != "")
        {
            byte[] bytes = GetData("select  Reports from [TMU$Posted Student Ext_Int Header] where No_ = '" + pk + "'").Rows[0]["Reports"].ToString() == "" ? null : (byte[])GetData("select  Reports from [TMU$Posted Student Ext_Int Header] where No_ = '" + pk + "'").Rows[0]["Reports"];

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "update [TMU$Posted Student Ext_Int Header] set Reports=''  where No_ = '" + pk + "'";
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + "AnalysisReport.pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();


           



            //string strFilePath = "C://tab//ReportAnalysis.pdf";
            //HttpResponse response = HttpContext.Current.Response;
            //response.Clear();
            //response.AppendHeader("content-disposition", "attachment; filename=" + "AnalysisReport.pdf");
            //response.ContentType = "application/octet-stream";
            //response.WriteFile(strFilePath);
            //response.Flush();
            //response.End();
        }
    }
    private DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                }
            }
            return dt;
        }
    }
    protected void lnkTeb_Click1(object sender, EventArgs e)
    {
        string Link = "";
        LinkButton btn = sender as LinkButton;
        GridViewRow row = btn.NamingContainer as GridViewRow;

        string SubjectCode = (string)this.grdOpenElective.DataKeys[row.RowIndex]["SubjectCode"];
        string No_ = (string)this.grdOpenElective.DataKeys[row.RowIndex]["No_"];



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
        Link = nvp.ExporTabulationReportPDF(No_, SubjectCode, false, false, false);
        Link = "C://tab//ReportTabulation.pdf";


        if (Link != "")
        {
            byte[] bytes = GetData("select  [Tabulation Report Pdf] from [TMU$Posted Student Ext_Int Header] where No_ = '" + No_ + "'").Rows[0]["Tabulation Report Pdf"].ToString() == "" ? null : (byte[])GetData("select  [Tabulation Report Pdf] from [TMU$Posted Student Ext_Int Header] where No_ = '" + No_ + "'").Rows[0]["Tabulation Report Pdf"];

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "update [TMU$Posted Student Ext_Int Header] set [Tabulation Report Pdf]=''  where No_ = '" + No_ + "'";
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ReportTabulation.pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();






            //string strFilePath = "C://tab//ReportTabulation.pdf";
            //HttpResponse response = HttpContext.Current.Response;
            //response.Clear();
            //response.AppendHeader("content-disposition", "attachment; filename=" + "ReportTabulation.pdf");
            //response.ContentType = "application/octet-stream";
            //response.WriteFile(strFilePath);
            //response.Flush();
            //response.End();
        }


    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        grdOpenElective.DataSource = "";
        divmsg.Visible = false;
    }
    protected void lnkMooc_Click(object sender, EventArgs e)
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
        nvp.Timeout = 3600000;
        Link = nvp.ExporTabulationReportPDF(pk, "", false, true, false);
        Link = "C://tab//ReportTabulation.pdf";


        if (Link != "")
        {
            byte[] bytes = GetData("select  [Tabulation Report Pdf] from [TMU$Posted Student Ext_Int Header] where No_ = '" + pk + "'").Rows[0]["Tabulation Report Pdf"].ToString() == "" ? null : (byte[])GetData("select  [Tabulation Report Pdf] from [TMU$Posted Student Ext_Int Header] where No_ = '" + pk + "'").Rows[0]["Tabulation Report Pdf"];

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "update [TMU$Posted Student Ext_Int Header] set [Tabulation Report Pdf]=''  where No_ = '" + pk + "'";
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ReportTabulation.pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();


            //string strFilePath = "C://tab//ReportTabulation.pdf";
            //HttpResponse response = HttpContext.Current.Response;
            //response.Clear();
            //response.AppendHeader("content-disposition", "attachment; filename=" + "ReportTabulation.pdf");
            //response.ContentType = "application/octet-stream";
            //response.WriteFile(strFilePath);
            //response.Flush();
            //response.End();
        }
    }
    protected void lnkCTLD_Click(object sender, EventArgs e)
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
        nvp.Timeout = 3600000;
        Link = nvp.ExporTabulationReportPDF(pk, "", true, false, false);
        Link = "C://tab//ReportTabulation.pdf";


        if (Link != "")
        {
            byte[] bytes = GetData("select  [Tabulation Report Pdf] from [TMU$Posted Student Ext_Int Header] where No_ = '" + pk + "'").Rows[0]["Tabulation Report Pdf"].ToString() == "" ? null : (byte[])GetData("select  [Tabulation Report Pdf] from [TMU$Posted Student Ext_Int Header] where No_ = '" + pk + "'").Rows[0]["Tabulation Report Pdf"];

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "update [TMU$Posted Student Ext_Int Header] set [Tabulation Report Pdf]=''  where No_ = '" + pk + "'";
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ReportTabulation.pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();

            //string strFilePath = "C://tab//ReportTabulation.pdf";
            //HttpResponse response = HttpContext.Current.Response;
            //response.Clear();
            //response.AppendHeader("content-disposition", "attachment; filename=" + "ReportTabulation.pdf");
            //response.ContentType = "application/octet-stream";
            //response.WriteFile(strFilePath);
            //response.Flush();
            //response.End();
        }
    }
    protected void lnkBridge_Click(object sender, EventArgs e)
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
        nvp.Timeout = 3600000;
        Link = nvp.ExporTabulationReportPDF(pk, "", false, false, true);
        Link = "C://tab//ReportTabulation.pdf";


        if (Link != "")
        {
            byte[] bytes = GetData("select  [Tabulation Report Pdf] from [TMU$Posted Student Ext_Int Header] where No_ = '" + pk + "'").Rows[0]["Tabulation Report Pdf"].ToString() == "" ? null : (byte[])GetData("select  [Tabulation Report Pdf] from [TMU$Posted Student Ext_Int Header] where No_ = '" + pk + "'").Rows[0]["Tabulation Report Pdf"];

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "update [TMU$Posted Student Ext_Int Header] set [Tabulation Report Pdf]=''  where No_ = '" + pk + "'";
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ReportTabulation.pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();


            //string strFilePath = "C://tab//ReportTabulation.pdf";
            //HttpResponse response = HttpContext.Current.Response;
            //response.Clear();
            //response.AppendHeader("content-disposition", "attachment; filename=" + "ReportTabulation.pdf");
            //response.ContentType = "application/octet-stream";
            //response.WriteFile(strFilePath);
            //response.Flush();
            //response.End();
        }
    }
    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpCourseList();
    }
}
