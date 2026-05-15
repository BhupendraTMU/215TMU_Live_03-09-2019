using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using System.Collections.Specialized;
using DL;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Web.UI.HtmlControls;
public partial class Faculty_AttendanceApproval : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsPostBack)
            {
                //BindAcademicYear();
                //bindDrpCourseList();
                bindAttendanceDetails();
            }
        }


    }


    public void bindAttendanceDetails()
    {
        try
        {
            string Query ="";
            if (Session["uid"].ToString() == "TMU06152")
            {
                Query = "select *,case when DATEDIFF(day,[Date], getdate())>2 then 'true' else 'false' end enab from HRMSPortal.dbo.tbl_timetableApproval where  Status=1 and DATEDIFF(day,[Date], getdate())>2 and  AcademicYear='23-24' order by CollegeCode";
            }
            else
            {
                Query = "select *,case when DATEDIFF(day,[Date], getdate())>2 then 'false' else 'true' end enab from HRMSPortal.dbo.tbl_timetableApproval where  Status=1 and PrincipalID='" + Session["uid"].ToString() + "' and  AcademicYear='23-24' order by CollegeCode";
            }

            SqlDataAdapter da1 = new SqlDataAdapter(Query, con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                BtnSubmit.Visible = true;
                GrdExamList.DataSource = dt1;
                GrdExamList.DataBind();

            }
            else
            {
                BtnSubmit.Visible = false;
            }
        }


        catch (Exception ex)
        {
        }
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkBoxheader = (CheckBox)GrdExamList.HeaderRow.FindControl("chkAll");
        foreach (GridViewRow Row in GrdExamList.Rows)
        {
            CheckBox checkRows = (CheckBox)Row.FindControl("chkStudent");
            if (checkBoxheader.Checked == true)
            {
                if (checkRows.Enabled == true)
                {
                    checkRows.Checked = true;
                }

            }
            else
            {
                checkRows.Checked = false;
            }

        }
    }
    protected void chkStudent_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkBoxheader = (CheckBox)GrdExamList.HeaderRow.FindControl("chkAll");
        foreach (GridViewRow Row in GrdExamList.Rows)
        {
            CheckBox checkRows = (CheckBox)Row.FindControl("chkStudent");
            if (checkRows.Checked == false)
            {
                checkBoxheader.Checked = false;

            }
            else
            {
                // checkBoxheader.Checked = true;
            }

        }
    }
    //protected void GrdExamList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    GrdExamList.PageIndex = e.NewPageIndex;
    //    bindAttendanceDetails();
    //}
    protected void BtnShow_Click(object sender, EventArgs e)
    {

        try
        {
            string Query = "";
            if (Session["uid"].ToString() == "TMU06152")
            {
                Query = "select *,case when DATEDIFF(day,[Date], getdate())>2 then 'true' else 'false' end enab from HRMSPortal.dbo.tbl_timetableApproval where  Status=1 and DATEDIFF(day,[Date], getdate())>2 and (Course like '%" + txtSearch.Text + "' or [Subject] like '%" + txtSearch.Text + "' or CONVERT(NVARCHAR(50),Lecture) ='" + txtSearch.Text + "' or [Date] ='" + txtSearch.Text + "' or Semester = '" + txtSearch.Text + "' or facultyCode = '" + txtSearch.Text + "' or CollegeCode like '%" + txtSearch.Text + "') and Date >=convert(date,'" + txtFromDate.Text + "') and Date<=convert(date,'" + txtTodate.Text + "')  order by CollegeCode";
            }
            else
            {
                Query = "select *,case when DATEDIFF(day,[Date], getdate())>2 then 'false' else 'true' end enab from HRMSPortal.dbo.tbl_timetableApproval where  Status=1 and PrincipalID='" + Session["uid"].ToString() + "' and (Course like '%" + txtSearch.Text + "' or [Subject] like '%" + txtSearch.Text + "' or CONVERT(NVARCHAR(50),Lecture) ='" + txtSearch.Text + "' or [Date] ='" + txtSearch.Text + "' or Semester = '" + txtSearch.Text + "' or facultyCode = '" + txtSearch.Text + "' or CollegeCode like '%" + txtSearch.Text + "') and Date >=convert(date,'" + txtFromDate.Text + "') and Date<=convert(date,'" + txtTodate.Text + "')  order by CollegeCode";
            }

            SqlDataAdapter da1 = new SqlDataAdapter(Query, con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                BtnSubmit.Visible = true;
                GrdExamList.DataSource = dt1;
                GrdExamList.DataBind();

            }
            else
            {
                GrdExamList.DataSource = "";
                GrdExamList.DataBind();
                BtnSubmit.Visible = false;
            }
        }


        catch (Exception ex)
        {
        }

    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int i = 0;
            foreach (GridViewRow row in GrdExamList.Rows)
            {
                CheckBox check = (CheckBox)row.FindControl("chkStudent");
                Label lblSemester = (Label)row.FindControl("lblSemester");
                var id = GrdExamList.DataKeys[row.RowIndex].Value;





                if (check.Checked == true)
                {

                    con.Open();

                    SqlCommand cmd = new SqlCommand("update HRMSPortal.dbo.tbl_timetableApproval set Status=2,HODApprovalDate=GETDATE(),ApprovedBy='" + Session["uid"].ToString() + "' where ID=" + id + "", con);
                    cmd.ExecuteNonQuery();

                    con.Close();
                    i++;
                }

            }


            if (i > 0)
            { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Approved  Successfully')", true); }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Data.. !')", true);
            }





            bindAttendanceDetails();
        }
        catch (Exception ex)
        {
        }
    }
}