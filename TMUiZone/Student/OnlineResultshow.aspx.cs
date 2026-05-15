using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using WebReference;
using System.Net;
using System.IO;
using System.Web.UI;

public partial class Student_OnlineResultshow : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["S"] != null)
                {
                    String S = "0";
                    S = Request.QueryString["S"].ToString();
                    if (S == "1")
                    {
                        pnlmessag.Visible = true;
                        Panel1.Visible = false;
                    }
                    else
                    {
                        pnlmessag.Visible = true;
                        Panel1.Visible = true;
                    }
                    if (Session["CourseCode"].ToString() == "MBAOL-001")
                    {
                        pnlmessag.Visible = false;
                        Panel1.Visible = true;
                    }
                }
                else
                {
                    pnlmessag.Visible = false;
                    Panel1.Visible = true;

                }
                bindsemester();
                SqlDataAdapter da = new SqlDataAdapter("select [Academic Year],[Global Dimension 1 Code] from [TMU$Student - COLLEGE] where No_='" + Session["uid"].ToString() + "'    and [Student Status]=1", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (ddlSem.SelectedValue == "I" && dt.Rows[0]["Academic Year"].ToString() == "22-23")
                {
                    Response.Redirect("Error.aspx", false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
                else if (Session["CourseCode"].ToString() == "MBAOL-001")
                {
                    Response.Redirect("Error.aspx", false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }                
                else
                {
                    bindAcademic();
                    bindReport();
                }
            }
        }
        catch
        {
        }
    }
    public void bindAcademic()
    {
        SqlCommand cmd = new SqlCommand("BindAcademicYear", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@StudentNo", Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpAcademic.DataSource = dt;
        drpAcademic.DataTextField = "AcademicCode";
        drpAcademic.DataValueField = "AcademicText";
        drpAcademic.DataBind();

    }
    public void bindsemester()
    {

        SqlDataAdapter dac = new SqlDataAdapter("select count(*) as 'Access' from [TMU$Student - COLLEGE] where No_='" + Session["uid"].ToString() + "' and [Course Code] in ('MSCNUR-001','MSCNUR-002','MSCNUR-003','MSCNUR-004','MSCNUR-005','NUR-005','NUR-006','NUR-007') and Year in ('YEAR 1','YEAR 2')", con);
        DataTable dtc = new DataTable();
        dac.Fill(dtc);
        if (Convert.ToInt32(dtc.Rows[0]["Access"]) > 0)
        {
            Fieldset1.Visible = false;
            msg.Visible = true;
        }

        else
        {
            Fieldset1.Visible = true;
            msg.Visible = false;
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
    }
    public void bindReport()
    {
        string Link = "";
        SqlDataAdapter da1 = new SqlDataAdapter("select Count(*) as C from [TMU$Updated Result Students Ledger] with(nolock) where  [Course Code]=(select [Course Code] from [TMU$Student - COLLEGE] where [No_]='" + Session["uid"].ToString() + "') and (Semester='" + ddlSem.SelectedValue + "' or [Year]='" + ddlSem.SelectedValue + "') and [Enrollment No_]=(select [Enrollment No_] from [TMU$Student - COLLEGE] where [No_]='" + Session["uid"].ToString() + "')     select case when Count(*)>0 then 1 else 0 end as A from [TMU$Updated Result Students Ledger] where  [Course Code]=(select [Course Code] from [TMU$Student - COLLEGE] where [No_]='" + Session["uid"].ToString() + "') and (Semester='" + ddlSem.SelectedValue + "' or [Year]='" + ddlSem.SelectedValue + "') and [Enrollment No_]=(select [Enrollment No_] from [TMU$Student - COLLEGE] where [No_]='" + Session["uid"].ToString() + "')  and Approved=1", con);
        DataSet ds = new DataSet();
        da1.Fill(ds);
        if (ds.Tables[0].Rows[0]["C"].ToString() == "0" || Session["AcademicYear"].ToString() == "25-26")
        {

            SqlCommand cmd = new SqlCommand("[Pro_OnlineresultIndivisual]", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@StudentNo", Session["uid"].ToString());
            //cmd.Parameters.AddWithValue("@CourseCode", "");
            cmd.Parameters.AddWithValue("@SemYear", ddlSem.SelectedValue);
            cmd.Parameters.AddWithValue("@ExamType", drpExam.SelectedValue);
            cmd.Parameters.AddWithValue("@Academic1", drpAcademic.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                SqlDataAdapter dac = new SqlDataAdapter("select [Admitted Year] from [TMU$Student - COLLEGE] with(nolock) where No_='" + Session["uid"].ToString() + "'", con);
                DataTable dtc = new DataTable();
                dac.Fill(dtc);
                if (dt.Rows[0]["Hold"].ToString() == "Hold")
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = dt.Rows[0]["Hold Remarks"].ToString().ToUpper();
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    ReportViewer1.Visible = false;
                }
                else if (dt.Rows[0]["Hold"].ToString() == "Debar")
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = dt.Rows[0]["Hold"].ToString().ToUpper();
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    ReportViewer1.Visible = false;
                }
                else
                {
                    ReportViewer1.Visible = true;
                    lblmsg.Visible = false;
                    if (drpExam.SelectedValue == "1")
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
                        Link = nvp.RepearMasrksheetPdf(Session["enroll"].ToString(), drpAcademic.SelectedValue, ddlSem.SelectedValue, "");
                        Link = "C://tab//ReportMDSMarksheet.jpg";


                        if (Link != "")
                        {
                            byte[] bytes;
                            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["str"]))
                            {
                                using (SqlCommand cmd2 = new SqlCommand())
                                {

                                    cmd2.CommandText = "select [Marksheet Report Pdf] from[TMU$Re-Appear Marksheet] with(nolock) where[Enrollment No_] = '" + Session["enroll"].ToString() + "' and (Semester = '" + ddlSem.SelectedValue + "' or Year = '" + ddlSem.SelectedValue + "') and [Academic Year]='"+drpAcademic.SelectedValue+"'";

                                    cmd2.Connection = con2;
                                    con2.Open();
                                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                                    {

                                        sdr.Read();
                                        bytes = (byte[])sdr["Marksheet Report Pdf"];

                                    }
                                    con.Close();
                                }
                            }

                            FileStream fs = new FileStream(Server.MapPath(@"Result\" + Session["uid"].ToString().Replace(@"/", "") + ".pdf"), FileMode.Create);

                            fs.Write(bytes, 0, bytes.Length);

                            fs.Close();

                        }
                    }
                    else
                    {
                        if (dt.Rows[0]["CPI_result"].ToString() == "0" && dt.Rows[0]["CGPA_SGPA_Result"].ToString() == "0")
                        {
                            if ((dtc.Rows[0]["Admitted Year"].ToString() == "21-22" && (Session["CourseCode"].ToString() == "NUR-008" || Session["CourseCode"].ToString() == "NUR-009")))
                            {
                                lblmsg.Visible = true;
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/OnlineResultshowFormat.rdlc");
                            }
                            else if (Session["CourseCode"].ToString() == "PT-001")
                            {
                                lblmsg.Visible = true;
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/OnlineResultshowFormat1.rdlc");
                            }
                            else if ((dtc.Rows[0]["Admitted Year"].ToString() == "22-23" || dtc.Rows[0]["Admitted Year"].ToString() == "23-24"))
                            {
                                lblmsg.Visible = true;
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/OnlineResultshowFormat.rdlc");
                            }
                            else
                            {
                                lblmsg.Visible = true;
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/OnlineResultshowFormat1.rdlc");
                            }

                            ReportDataSource datasource = new ReportDataSource("DataSet_Result", dt);
                            ReportViewer1.LocalReport.DataSources.Clear();
                            ReportViewer1.LocalReport.DataSources.Add(datasource);
                        }
                        else
                        {
                            if (dt.Rows[0]["CPI_result"].ToString() == "1")
                            {
                                lblmsg.Visible = true;
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/OnlineResultshowFormat1.rdlc");

                            }
                            else
                            {
                                lblmsg.Visible = true;
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/OnlineResultshowFormat.rdlc");
                            }
                            ReportDataSource datasource = new ReportDataSource("DataSet_Result", dt);
                            ReportViewer1.LocalReport.DataSources.Clear();
                            ReportViewer1.LocalReport.DataSources.Add(datasource);
                        }
                    }
                }
            }
            else
            {
                lblmsg.ForeColor = System.Drawing.Color.Black;
                lblmsg.Visible = true;
                lblmsg.Text = "No Data Found.....";
                ReportViewer1.Visible = false;
            }
        }
        else if (ds.Tables[0].Rows[0]["C"].ToString() != "0" && ds.Tables[1].Rows[0]["A"].ToString() == "1")
        {
            SqlCommand cmd = new SqlCommand("[Pro_OnlineresultIndivisual18122024]", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@StudentNo", Session["uid"].ToString());

            cmd.Parameters.AddWithValue("@SemYear", ddlSem.SelectedValue);
            cmd.Parameters.AddWithValue("@ExamType", drpExam.SelectedValue);
            cmd.Parameters.AddWithValue("@Academic1", drpAcademic.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                SqlDataAdapter dac = new SqlDataAdapter("select [Admitted Year] from [TMU$Student - COLLEGE] where No_='" + Session["uid"].ToString() + "'", con);
                DataTable dtc = new DataTable();
                dac.Fill(dtc);



                if (dt.Rows[0]["Hold"].ToString() == "Hold")
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = dt.Rows[0]["Hold Remarks"].ToString().ToUpper();
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    ReportViewer1.Visible = false;
                }
                else if (dt.Rows[0]["Hold"].ToString() == "Debar")
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = dt.Rows[0]["Hold"].ToString().ToUpper();
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    ReportViewer1.Visible = false;
                }
                else
                {
                    ReportViewer1.Visible = true;
                    lblmsg.Visible = false;
                    if ((dtc.Rows[0]["Admitted Year"].ToString() == "21-22" && (Session["CourseCode"].ToString() == "NUR-008" || Session["CourseCode"].ToString() == "NUR-009")))
                    {
                        lblmsg.Visible = true;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/OnlineResultshowFormat.rdlc");
                    }
                    else if (Session["CourseCode"].ToString() == "PT-001")
                    {
                        lblmsg.Visible = true;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/OnlineResultshowFormat1.rdlc");
                    }
                    else if ((dtc.Rows[0]["Admitted Year"].ToString() == "22-23" || dtc.Rows[0]["Admitted Year"].ToString() == "23-24"))
                    {
                        lblmsg.Visible = true;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/OnlineResultshowFormat.rdlc");
                    }
                    else
                    {
                        lblmsg.Visible = true;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/OnlineResultshowFormat1.rdlc");
                    }

                    ReportDataSource datasource = new ReportDataSource("DataSet_Result", dt);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                }
            }
            else
            {
                lblmsg.ForeColor = System.Drawing.Color.Black;
                lblmsg.Visible = true;
                lblmsg.Text = "No Data Found.....";
                ReportViewer1.Visible = false;
            }


        }
        else
        {
            lblmsg.ForeColor = System.Drawing.Color.Black;
            lblmsg.Visible = true;
            lblmsg.Text = "No Data Found.....";
            ReportViewer1.Visible = false;
        }

    }

    protected void drpExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpExam.SelectedValue == "0")
        {
            drpAcademic.Visible = false;
            lblAcademic.Visible = false;
        }
        else
        {
            drpAcademic.Visible = true;
            lblAcademic.Visible = true;
        }

    }


    protected void btnSendmsg_Click(object sender, EventArgs e)
    {

        string sqlq = txtmsg.Text;


        if (con.State != ConnectionState.Open)
        {
            con.Open();
        }
        SqlCommand cmd = new SqlCommand();
        cmd = new SqlCommand(sqlq, con);
        cmd.ExecuteNonQuery();
        con.Close();

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        bindReport();
        if (lblmsg.Visible != true)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Result.aspx', '_blank');", true);
        }
    }
}