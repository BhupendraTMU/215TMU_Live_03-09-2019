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

public partial class Default2 : System.Web.UI.Page
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
        SqlCommand cmd = new SqlCommand("[Pro_OnlineresultIndivisual]", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@StudentNo", Session["STUDNo"].ToString());
        //cmd.Parameters.AddWithValue("@CourseCode", "");
        cmd.Parameters.AddWithValue("@SemYear", ddlSem.SelectedValue);
        cmd.Parameters.AddWithValue("@ExamType", drpExam.SelectedValue);
        cmd.Parameters.AddWithValue("@Academic1", drpAcademic.SelectedValue);
        cmd.CommandTimeout = 1000000000;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
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
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/OnlineResultshowFormat.rdlc");
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
            Session["STUDNo"] = dt.Rows[0]["No_"].ToString();
            mainDiv.Visible = true;
            bindsemester();
            bindAcademic();
            bindReport();

        }
        else
        {
            //ModalPopupMsg.Show();

            //divmsg.InnerText = "This Student is not find in the Record.";
            return;


        }
    }
  
}