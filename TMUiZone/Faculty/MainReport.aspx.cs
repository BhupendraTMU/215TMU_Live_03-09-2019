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

public partial class Faculty_MainReport : System.Web.UI.Page
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
            SqlCommand cmd = new SqlCommand("[Pro_GetAwardList]", con);//Pro_GetAwardList_Test
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Acadyear", Session["AcademicYear"].ToString());
            cmd.Parameters.AddWithValue("@Subject", Session["Subject"].ToString());
            if (Session["UserRole"].ToString() == "HOD" || Session["UserRole"].ToString() == "Pricipal")
            {
                cmd.Parameters.AddWithValue("@facultyCode", Session["FacultyCode"].ToString());
            }
            else
            {
                cmd.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
            }
            cmd.Parameters.AddWithValue("@Course", Session["drpCourse"].ToString());
            cmd.Parameters.AddWithValue("@Sem", Session["drpSemester"].ToString());
            cmd.Parameters.AddWithValue("@ExamType", Session["Examtype"].ToString());



            cmd.Parameters.AddWithValue("@Section", Session["Section"].ToString());

            cmd.Parameters.AddWithValue("@StudentGroup", Session["Group"].ToString());


            cmd.CommandTimeout = 150;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
               // lblReportStatus.Text = dt.Rows[0]["ReportStatus"].ToString();
                if (Session["UserRole"].ToString() == "HOD")
                {
                    //  btnSendForApproval.Visible = false;
                    if (Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 2 || Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 3)
                    {
                        //  tblAR.Visible = false;
                    }
                    else
                    {
                        //  tblAR.Visible = true;
                    }


                }
                else if (Session["UserRole"].ToString() == "Pricipal")
                {
                    //btnSendForApproval.Visible = false;
                    if (Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 4 || Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 5)
                    {
                      //  tblAR.Visible = false;
                    }
                    else
                    {
                     //    tblAR.Visible = true;
                    }
                }

                else
                {
                    if (Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 0)
                    {
                        //     btnSendForApproval.Visible = true;
                        // tblAR.Visible = false;
                    }
                    else
                    {
                        // btnSendForApproval.Visible = false;
                        // tblAR.Visible = false;
                    }
                }
                // lblmsg.Visible = false;
                ReportViewer1.Visible = true;
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                if (dt.Rows[0]["Classification"].ToString() == "THEORY")
                {
                    if ((Session["UserRole"].ToString() == "HOD" || Session["UserRole"].ToString() == "Pricipal") && Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 0)
                    {
                        //     tblAR.Visible = false;
                        ReportViewer1.Visible = true;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/AwardlistReport.rdlc");
                    }
                    else if (Session["UserRole"].ToString() == "HOD")
                    {
                        if (Convert.ToInt32(dt.Rows[0]["Award List Status"])>=1)
                        {
                            // tblAR.Visible = true;
                            ReportViewer1.Visible = true;
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/AwardlistReport.rdlc");
                        }
                        else
                        {
                               //tblAR.Visible = false;
                            ReportViewer1.Visible = false;
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/AwardlistReport.rdlc");
                        }
                    }
                    else if (Session["UserRole"].ToString() == "Pricipal")
                    {
                        if (Convert.ToInt32(dt.Rows[0]["Award List Status"])>= 2)
                        {
                            ReportViewer1.Visible = true;
                             //tblAR.Visible = true;
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/AwardlistReport.rdlc");
                        }
                        else
                        {
                            ReportViewer1.Visible = false;
                            // tblAR.Visible = false;
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/AwardlistReport.rdlc");
                        }
                    }
                    else
                    {
                        ReportViewer1.Visible = true;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/AwardlistReport.rdlc");
                    }


                }
                else
                {
                    if ((Session["UserRole"].ToString() == "HOD" || Session["UserRole"].ToString() == "Pricipal") && Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 0)
                    {
                        ReportViewer1.Visible = false;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/InternalPracticalAwardlistReport.rdlc");
                        lblmsg.Visible = true;
                        lblmsg.Text = "Report is either Rejected or Not Submitted.....";
                      
                    }
                    else if (Session["UserRole"].ToString() == "HOD" && (Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 0 || Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 3 || Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 5 ))
                    {
                        ReportViewer1.Visible = false;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/InternalPracticalAwardlistReport.rdlc");
                        lblmsg.Visible = true;
                        lblmsg.Text = "Report is either Rejected or Not Submitted.....";
                    }
                    else if (Session["UserRole"].ToString() == "Pricipal" && (Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 0 ||
                        Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 3 || Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 5))
                    {
                        ReportViewer1.Visible = false;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/InternalPracticalAwardlistReport.rdlc");
                        lblmsg.Visible = true;
                        lblmsg.Text = "Report is either Rejected or Not Submitted.....";
                    }
                    else
                    {
                        ReportViewer1.Visible = true;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/InternalPracticalAwardlistReport.rdlc");
                        lblmsg.Visible = false;
                    }

                }
                ReportDataSource datasource = new ReportDataSource("DataSet_Result", dt);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
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