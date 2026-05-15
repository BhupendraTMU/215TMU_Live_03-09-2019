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

public partial class OnlineResult : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    string StudentNo = "", DOB = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {



        }
        catch
        {
        }

    }
    public void bindAcademic()
    {
        SqlCommand cmd = new SqlCommand("BindAcademicYear", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@StudentNo", Session["STUDNo"].ToString());
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
        SqlCommand cmd = new SqlCommand("BindSemester", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@StudentNo", Session["STUDNo"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlSem.DataSource = dt;
        ddlSem.DataTextField = "sem";
        ddlSem.DataValueField = "semcode";
        ddlSem.DataBind();

    }
    public void bindReport()
    {
        SqlDataAdapter da1 = new SqlDataAdapter("select Count(*) as C from [TMU$Updated Result Students Ledger] where  [Course Code]=(select [Course Code] from [TMU$Student - COLLEGE] where [No_]='" + Session["STUDNo"].ToString() + "') and (Semester='" + ddlSem.SelectedValue + "' or [Year]='" + ddlSem.SelectedValue + "') and [Enrollment No_]=(select [Enrollment No_] from [TMU$Student - COLLEGE] where [No_]='" + Session["STUDNo"].ToString() + "')     select Count(*) as A from [TMU$Updated Result Students Ledger] where  [Course Code]=(select [Course Code] from [TMU$Student - COLLEGE] where [No_]='" + Session["STUDNo"].ToString() + "') and (Semester='" + ddlSem.SelectedValue + "' or [Year]='" + ddlSem.SelectedValue + "') and [Enrollment No_]=(select [Enrollment No_] from [TMU$Student - COLLEGE] where [No_]='" + Session["STUDNo"].ToString() + "')  and Approved=1", con);
        DataSet ds = new DataSet();
        da1.Fill(ds);


        if (ds.Tables[0].Rows[0]["C"].ToString() == "0")
        {

            SqlCommand cmd = new SqlCommand("[Pro_OnlineresultIndivisual]", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@StudentNo", Session["STUDNo"].ToString());
            //cmd.Parameters.AddWithValue("@CourseCode", "");
            cmd.Parameters.AddWithValue("@SemYear", ddlSem.SelectedValue);
            cmd.Parameters.AddWithValue("@ExamType", drpExam.SelectedValue);
            cmd.Parameters.AddWithValue("@Academic1", drpAcademic.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                SqlDataAdapter dac = new SqlDataAdapter("select [Admitted Year],[Course Code] from [TMU$Student - COLLEGE] where No_='" + Session["STUDNo"].ToString() + "'", con);
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
                    if ((dtc.Rows[0]["Admitted Year"].ToString() == "21-22" && (dtc.Rows[0]["Course Code"].ToString() == "NUR-008" || dtc.Rows[0]["Course Code"].ToString() == "NUR-009")))
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/OnlineResultshowFormat.rdlc");
                    }
                    else if (dtc.Rows[0]["Course Code"].ToString() == "PT-001")
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/OnlineResultshowFormat1.rdlc");
                    }
                    else if ((dtc.Rows[0]["Admitted Year"].ToString() == "22-23" || dtc.Rows[0]["Admitted Year"].ToString() == "23-24"))
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/OnlineResultshowFormat.rdlc");
                    }
                    else
                    {
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
        else if (ds.Tables[0].Rows[0]["C"].ToString() != "0" && ds.Tables[1].Rows[0]["A"].ToString() == "1")
        {
            SqlCommand cmd = new SqlCommand("[Pro_OnlineresultIndivisual]", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@StudentNo", Session["STUDNo"].ToString());
            //cmd.Parameters.AddWithValue("@CourseCode", "");
            cmd.Parameters.AddWithValue("@SemYear", ddlSem.SelectedValue);
            cmd.Parameters.AddWithValue("@ExamType", drpExam.SelectedValue);
            cmd.Parameters.AddWithValue("@Academic1", drpAcademic.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                SqlDataAdapter dac = new SqlDataAdapter("select [Admitted Year],[Course Code] from [TMU$Student - COLLEGE] where No_='" + Session["STUDNo"].ToString() + "'", con);
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
                    if ((dtc.Rows[0]["Admitted Year"].ToString() == "21-22" && (dtc.Rows[0]["Course Code"].ToString() == "NUR-008" || dtc.Rows[0]["Course Code"].ToString() == "NUR-009")))
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/OnlineResultshowFormat.rdlc");
                    }
                    else if (dtc.Rows[0]["Course Code"].ToString() == "PT-001")
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/OnlineResultshowFormat1.rdlc");
                    }
                    else if ((dtc.Rows[0]["Admitted Year"].ToString() == "22-23" || dtc.Rows[0]["Admitted Year"].ToString() == "23-24"))
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/OnlineResultshowFormat.rdlc");
                    }
                    else
                    {
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
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindReport();
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
        bindReport();
    }
    protected void drpAcademic_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindReport();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("[proc_GetStudentNo]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cmd.Parameters.Add("@StuNo", txtUserid.Text);
        cmd.Parameters.Add("@DOB", txtpassword.Text);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            //mainDiv.InnerText = "";
            Session["STUDNo"] = dt.Rows[0]["No_"].ToString();

            Session["STUDNo"] = dt.Rows[0]["No_"].ToString();

            Session["STUDNo"] = dt.Rows[0]["No_"].ToString();
            mainDiv.Visible = true;
            DivMsg.Visible = false;
            bindsemester();


            bindAcademic();


            SqlDataAdapter daN = new SqlDataAdapter("select [Academic Year] from [TMU$Student - COLLEGE] where No_='" + Session["STUDNo"].ToString() + "' and [Academic Year]='22-23'   and [Student Status]=1", con);
            DataTable dtN = new DataTable();
            daN.Fill(dtN);

            if (ddlSem.SelectedValue == "I" && dt.Rows[0]["Academic Year"].ToString() == "22-23")
            {
                mainDiv.Visible = false;
                DivMsg.Visible = true;
                DivMsg.InnerHtml = "This Student is not find in the Record.";
            }
            else
            {

                bindAcademic();
                bindReport();
            }
        
            

        }
        else
        {
            mainDiv.Visible = false;
            DivMsg.Visible = true;
            DivMsg.InnerHtml = "This Student is not find in the Record.";
            


        }
    }
}