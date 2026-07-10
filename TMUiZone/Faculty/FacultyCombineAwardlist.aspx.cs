using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using System.Net;
using WebReference;

public partial class Faculty_FacultyCombineAwardlist : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindReport();
        }
    }
    public void bindReport()
    {
        try
        {

            SqlCommand cmd = new SqlCommand("[Pro_GetAwardList_facultyCombine]", con);//Pro_GetAwardList_Test
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Acadyear", Session["AcademicYear"].ToString());
            cmd.Parameters.AddWithValue("@Subject", Session["Subject"].ToString());
            cmd.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Course", Session["drpCourse"].ToString());
            cmd.Parameters.AddWithValue("@Sem", Session["drpSemester"].ToString());
            cmd.Parameters.AddWithValue("@examType", Convert.ToInt32(Session["examType1"]));
            if (Session["Section"] == null)
            {
                cmd.Parameters.AddWithValue("@Section", "");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Section", Session["Section"].ToString());
            }
            if (Session["StudGroup"] == null)
            {
                cmd.Parameters.AddWithValue("@StudentGroup", "");
            }
            else
            {
                cmd.Parameters.AddWithValue("@StudentGroup", Session["StudGroup"].ToString());
            }


            cmd.CommandTimeout = 180;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //DataTable dtNAV = new DataTable();
            //SqlCommand cmdNAV = new SqlCommand("Proc_GetNAVCreditionalLive", con);
            //cmdNAV.CommandType = CommandType.StoredProcedure;
            //SqlDataAdapter daNAV = new SqlDataAdapter(cmdNAV);
            //daNAV.Fill(dtNAV);
            //VoucherPosting nvp = new VoucherPosting();
            //nvp.UseDefaultCredentials = true;
            //nvp.Url = dtNAV.Rows[0]["URL"].ToString();
            //nvp.Credentials = new NetworkCredential(dtNAV.Rows[0]["UserID"].ToString(), dtNAV.Rows[0]["Password"].ToString());
            //string result;
            if (ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    //if (Convert.ToInt32(Session["examType1"]) == 1 && Convert.ToInt32(ds.Tables[0].Rows[0]["Open"]) != 1)
                    //{
                    //    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    //    {

                    //        result = nvp.InternalTotalMarks(ds.Tables[1].Rows[i]["Subject Exam Craiteria"].ToString(), ds.Tables[1].Rows[i]["Global Dimension 1 Code"].ToString(), ds.Tables[1].Rows[i]["Academic Year"].ToString(), ds.Tables[1].Rows[i]["Subject Code"].ToString(), ds.Tables[1].Rows[i]["Course"].ToString(), ds.Tables[1].Rows[i]["Student No_"].ToString());
                    //    }
                    //}


                    // lblReportStatus.Text = dt.Rows[0]["ReportStatus"].ToString();


                    ReportViewer1.Visible = true;
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    if (ds.Tables[0].Rows[0]["Classification"].ToString() == "THEORY")
                    {

                        ReportViewer1.Visible = true;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/AwardlistReport.rdlc");
                    }



                    else
                    {
                        if (Convert.ToInt32(Session["examType1"]) == 1)
                        {
                            ReportViewer1.Visible = true;
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/InternalPracticalAwardlistReport.rdlc");
                        }
                        else
                        {
                            ReportViewer1.Visible = true;
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/External_Practical_AwardlistReport.rdlc");
                        }
                    }
                    ReportDataSource datasource = new ReportDataSource("DataSet_Result", ds.Tables[0]);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                }
                catch (Exception)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Something went wrong ,Please try after some time ";
                    ReportViewer1.Visible = false;
                }
            }
            else
            {
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'No data Found.. ');", true);
                lblmsg.Visible = true;
                lblmsg.Text = "No Data Found.....";
                ReportViewer1.Visible = false;
            }
        }
        catch (Exception ex) { }
    }
}