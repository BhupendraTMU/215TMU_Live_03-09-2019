using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
//using Microsoft.Reporting.WebForms;
public partial class Faculty_ExamTimesheet : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindAcademicYear();
            bindDrpCourseList();
            //  BindSemYear();
            GetCollegeCodeForprincipal();
            getgriddata();
        }
    }
    public void BindAcademicYear()
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
            ddlAcademicYear.DataSource = dt1;
            ddlAcademicYear.DataTextField = "Details";
            ddlAcademicYear.DataValueField = "No_";
            ddlAcademicYear.DataBind();
        }
        catch
        {
        }
    }
    public void BindSemYear()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_GetSemYearFilterByOddEvenExamSp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", Session["uid"].ToString());
            cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            cmd.Parameters.Add("@CourseCode", ddlCourse.SelectedValue);
            cmd.Parameters.Add("@Examtype", "0");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            con.Open();
            da.Fill(dt1);
            con.Close();
            ddlSem.DataSource = dt1;
            ddlSem.DataTextField = "Details";
            ddlSem.DataValueField = "Code";
            ddlSem.DataBind();
        }
        catch
        {
        }
    }
    public void bindDrpCourseList()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetCourse_RoleMatrix", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", Session["uid"].ToString());
            cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            ddlCourse.DataSource = dt;
            ddlCourse.DataTextField = "Details";
            ddlCourse.DataValueField = "No_";
            ddlCourse.DataBind();
        }
        catch (Exception) { }
    }
    public void GetCollegeCodeForprincipal()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("sp_GetCollegeCodeforprincipal", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", Session["uid"].ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            DdlCollege.DataSource = dt;
            DdlCollege.DataTextField = "Details";
            DdlCollege.DataValueField = "No_";
            DdlCollege.DataBind();
        }
        catch (Exception ex) { }
    }

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpCourseList();
        BtnSubmit.Visible = false;
        BtnReject.Visible = false;
        GrdExamTimeSheet.Visible = false;
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
         BindSemYear();
         BtnSubmit.Visible = false;
         BtnReject.Visible = false;
         GrdExamTimeSheet.Visible = false;
    }

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        BtnSubmit.Visible = false;
        BtnReject.Visible = false;
        GrdExamTimeSheet.Visible = false;
    }

    protected void DdlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {

        BtnSubmit.Visible = false;
        BtnReject.Visible = false;
        GrdExamTimeSheet.Visible = false;
    }
    public void getgriddata()
    {

        try
        {
            SqlCommand cmd = new SqlCommand("Sp_getExamTimesheetdataRelease", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AcademicYear", ddlAcademicYear.SelectedValue);
            cmd.Parameters.AddWithValue("@Sem", ddlSem.SelectedValue);
            cmd.Parameters.AddWithValue("@CourseCode", ddlCourse.SelectedValue);
            cmd.Parameters.AddWithValue("@CollegeCode", DdlCollege.SelectedValue);
            cmd.Parameters.AddWithValue("@Status", Rblist.SelectedValue);
            cmd.Parameters.AddWithValue("@FacultyCode", Session["uid"].ToString());
            if (CHKSpecial.Checked == true)
            {
                cmd.Parameters.AddWithValue("@SpecialDateSheet", 1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SpecialDateSheet", 0);
            }
            if (ChkOpen.Checked == true)
            {
                cmd.Parameters.AddWithValue("@Open", 1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Open", 0);
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);

            con.Close();
            GrdExamTimeSheet.DataSource = dt;
            GrdExamTimeSheet.DataBind();
            if (dt.Rows.Count > 0)
            {

                GrdExamTimeSheet.Visible = true;
             
             if (Rblist.SelectedValue == "1")
             {
                     BtnPrint.Visible = false;
                    BtnSubmit.Visible = true;
                    BtnReject.Visible = true;
                }
                else if (Rblist.SelectedValue == "2")
                {
                    BtnSubmit.Visible = true;
                    BtnReject.Visible = true;
                }
                else if (Rblist.SelectedValue == "3")
                {
                    BtnSubmit.Visible = false;
                    BtnReject.Visible = false;
                }
                else if (Rblist.SelectedValue == "4")
                {
                    BtnSubmit.Visible = false;
                    BtnReject.Visible = true;
                }
                else if (Rblist.SelectedValue == "5")
                {
                    BtnSubmit.Visible = true;
                    BtnReject.Visible = false;
                }
                else if (Rblist.SelectedValue == "6")
                {
                    BtnPrint.Visible = true;
                    BtnSubmit.Visible = false;
                    BtnReject.Visible = false;
                }
                else if (Rblist.SelectedValue == "7")
                {
                    BtnSubmit.Visible = true;
                    BtnReject.Visible = true;
                }
            }
            else
            {

                BtnSubmit.Visible = false;
                BtnReject.Visible = false;
                GrdExamTimeSheet.Visible = false;
            }



        }
        catch (Exception) { }
    }
    protected void BtnShow_Click(object sender, EventArgs e)
    {
        getgriddata();
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow Row in GrdExamTimeSheet.Rows)
        {


            HiddenField semester = (HiddenField)Row.FindControl("hfsemester");

            SqlCommand cmd = new SqlCommand("sp_updatestatusExamSheet1", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AcademicYear", ddlAcademicYear.SelectedValue);
            cmd.Parameters.AddWithValue("@CourseCode", ddlCourse.SelectedValue);
            cmd.Parameters.AddWithValue("@Sem", semester.Value);
            cmd.Parameters.AddWithValue("@CollegeCode", DdlCollege.SelectedValue);
            cmd.Parameters.AddWithValue("@gradeStatus", "4");
            cmd.Parameters.AddWithValue("@remarks", txtRemarks.Text);
            if (CHKSpecial.Checked == true)
            {
                cmd.Parameters.AddWithValue("@SpecialDateSheet", 1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SpecialDateSheet", 0);
            }
            if (ChkOpen.Checked == true)
            {
                cmd.Parameters.AddWithValue("@Open", 1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Open", 0);
            }

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data updated Successfully')", true);
        getgriddata();
    }

    protected void BtnReject_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModalB').modal('show');</script>", false);

    }

    protected void BtnYes_Click(object sender, EventArgs e)
    {


        if (txtRemarks.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Remarks is Necessary !')", true);
            txtRemarks.Focus();
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModalB').modal('show');</script>", false);

        }
        else
        {
            foreach (GridViewRow Row in GrdExamTimeSheet.Rows)
            {
                HiddenField semester = (HiddenField)Row.FindControl("hfsemester");
                SqlCommand cmd = new SqlCommand("sp_updatestatusExamSheet1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AcademicYear", ddlAcademicYear.SelectedValue);
                cmd.Parameters.AddWithValue("@CourseCode", ddlCourse.SelectedValue);
                cmd.Parameters.AddWithValue("@Sem", semester.Value);
                cmd.Parameters.AddWithValue("@CollegeCode", DdlCollege.SelectedValue);
                cmd.Parameters.AddWithValue("@gradeStatus", "5");
                cmd.Parameters.AddWithValue("@remarks", txtRemarks.Text);
                if (CHKSpecial.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@SpecialDateSheet", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@SpecialDateSheet", 0);
                }

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data rejected Successfully')", true);
            getgriddata();
        }
    }

    protected void GrdExamTimeSheet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField HdnStatus = (HiddenField)e.Row.FindControl("hfstatus");
            if (HdnStatus.Value == "Released")
            {
                BtnSubmit.Visible = true;
                BtnReject.Visible = true;


            }
            else
            {
                BtnSubmit.Visible = false;
                BtnReject.Visible = false;
            }

        }
    }
    protected void Rblist_SelectedIndexChanged(object sender, EventArgs e)
    {
        BtnSubmit.Visible = false;
        BtnReject.Visible = false;
        GrdExamTimeSheet.Visible = false;
        getgriddata();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
  

    protected void BtnPrint_Click1(object sender, EventArgs e)
    {
       


    }
    protected void ChkOpen_CheckedChanged(object sender, EventArgs e)
    {

        if (ChkOpen.Checked == true)
        {
            ddlCourse.Enabled = false;
          
            ddlSem.Enabled = false;
          

        }
        else
        {

            ddlCourse.Enabled = true;
          
            ddlSem.Enabled = true;
           
        }

    }
}