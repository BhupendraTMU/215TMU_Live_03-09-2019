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
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class Faculty_SecondaryLoad : System.Web.UI.Page
{

    TMUConnection con; string CollegeCode = "";
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
       

        try
        {
            if (Session["UserGroup"].ToString() == "PRINCIPAL" || Session["UserGroup"].ToString() == "ADMIN" || Session["Hod"].ToString() != "")
            {
                try
                {

                    CollegeCode = Session["GlobalDimension1Code"].ToString();
                    if (!IsPostBack)
                    {
                        bindAcademicYear();// ashu on 24-02-2017
                       // bindaddgrid();
                        BindGrid();
                        BindCourse();
                        GetFacultyList();
                        BindCollege();
                        secondryload();
                    }

                }

                catch (Exception ex)
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        catch
        { Response.Redirect("~/Default.aspx"); }


    
    }
    public void bindAcademicYear()
    {
        SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da.Fill(dt1);
        drpAcademicYear.DataSource = dt1;
        drpAcademicYear.DataTextField = "Details";
        drpAcademicYear.DataValueField = "No_";
        drpAcademicYear.DataBind();
    }
    public void BindCollege()
    {
        SqlCommand cmd = new SqlCommand("Sp_CollegeCodeForFaculty", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", ddlFaculty.SelectedValue);
        cmd.Parameters.Add("@UniversityCollege", rblUnivCollege.SelectedValue);
       
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlCollege.DataSource = dt;
        ddlCollege.DataTextField = "Details";
        ddlCollege.DataValueField = "No_";
        ddlCollege.DataBind();
        //ddlCollege.Items.Insert(0, new ListItem("UniversityCollege", "UniversityCollege"));
       
    }
    public void BindCourse()
    {
        string AsPrincipal = "";
        AsPrincipal = "True";
        SqlCommand cmd = new SqlCommand("proc_GetCourseForReviewAttendance", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@AsPrincipal", AsPrincipal);
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpCourse.DataSource = dt;
        drpCourse.DataTextField = "Details";
        drpCourse.DataValueField = "No_";
        drpCourse.DataBind();
        BindSemester();

    }

    public void BindSemester()
    {
        string FacultyCode = "";
        //  FacultyCode = Session["uid"].ToString();

        SqlCommand cmd = new SqlCommand("proc_GetSemester", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", FacultyCode);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpSemester.DataSource = dt;
        drpSemester.DataTextField = "Details";
        drpSemester.DataValueField = "No_";
        drpSemester.DataBind();

    }

    public void GetFacultyList()
    {
        con = new TMUConnection();
        SqlCommand cmd = new SqlCommand("SP_GetFacultyFromTimeTable_CollegeWise", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"]);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.DisConnect();
        ddlFaculty.DataSource = dt;
        ddlFaculty.DataTextField = "Name";//Name
        ddlFaculty.DataValueField = "Code";
        
        ddlFaculty.DataBind();
    }

    public void secondryload()
    {
        SqlCommand cmd2 = new SqlCommand("select Code, Description from [TMU$Secondary load Master]", con1);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataTable dt2 = new DataTable();
        da2.Fill(dt2);
        ddlload.DataTextField = "Description";
        ddlload.DataValueField = "Code";        
        ddlload.DataSource = dt2;
        ddlload.DataBind();
        ddlload.Items.Insert(0, "-- Description --");

    }

    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSemester();
    }
    protected void Btnadd_Click(object sender, EventArgs e)
    {
        
        try
        {                      
           // SqlCommand cmd = new SqlCommand();
            //cmd.CommandText = "insert into [TMU$Employee Secondary Load]([Employee Code],[Course Code],[Course Name],[Semester],[Year],[College Code],[Secondary Load Code],[Secondary Load Description],[Employee Name],University_College,[Academic Year],Remarks,[Global Dimension 1 Code],[Created By],[Created Date],[Updated By], [Updated Date] ,Inactive, [Odd Even Yearly]) values(@Employeecode,@coursecode, @coursename,@semester, @year,@collegecode,@secondrylodecode, @secondrylodediscription,@Employeename,@UniversityCollege,@AcademicYear,@Remarks,@GlobalDimensionCode,@CreatedBy,convert(varchar,getdate(),106),@CreatedBy,convert(varchar,getdate(),106),'0',0)";
            SqlCommand cmd = new SqlCommand("SP_InserUpdateEmployeeSecondaryLoad", con1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Employeecode", ddlFaculty.SelectedValue);
            cmd.Parameters.AddWithValue("@Employeename", ddlFaculty.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@coursecode", drpCourse.SelectedValue);
            if (drpCourse.SelectedValue == "") { cmd.Parameters.AddWithValue("@coursename", ""); }
            else { cmd.Parameters.AddWithValue("@coursename", drpCourse.SelectedItem.Text); }
            cmd.Parameters.AddWithValue("@collegecode", ddlCollege.SelectedValue);
            cmd.Parameters.AddWithValue("@secondrylodecode", ddlload.SelectedValue);
            cmd.Parameters.AddWithValue("@secondrylodediscription", ddlload.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@UniversityCollege",rblUnivCollege.SelectedValue);//
            cmd.Parameters.AddWithValue("@AcademicYear", drpAcademicYear.SelectedValue);//
            cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text);//
            cmd.Parameters.AddWithValue("@GlobalDimensionCode", Session["GlobalDimension1Code"].ToString());//
            cmd.Parameters.AddWithValue("@CreatedBy", Session["uid"].ToString());//  
            if(Btnadd.Text=="Add"){hfEntryNo.Value="0";}          
            cmd.Parameters.AddWithValue("@EntryNo", Convert.ToInt16(hfEntryNo.Value));//
            
            if (drpSemester.SelectedValue == "YEAR 1" || drpSemester.SelectedValue == "YEAR 2" || drpSemester.SelectedValue == "YEAR 3" || drpSemester.SelectedValue == "YEAR 4" || drpSemester.SelectedValue == "YEAR 5" || drpSemester.SelectedValue == "YEAR 6" || drpSemester.SelectedValue == "YEAR 7")
            {

                cmd.Parameters.AddWithValue("@year", drpSemester.SelectedValue);
                cmd.Parameters.AddWithValue("@semester","");
            }
            else
            {
                cmd.Parameters.AddWithValue("@semester", drpSemester.SelectedValue);
                cmd.Parameters.AddWithValue("@year", "");
            }
            cmd.Connection = con1;
            con1.Open();
            cmd.ExecuteNonQuery();
            con1.Close();
            Btnadd.Text = "Add";
            hfEntryNo.Value = "0";
            drpCourse.SelectedValue = ""; ddlFaculty.SelectedValue = ""; drpSemester.SelectedValue = "";
            ddlload.SelectedIndex = -1; ddlCollege.SelectedIndex = -1; txtRemarks.Text = "";
        }

        catch
        {

        }
        finally
        {
            if (con1.State == ConnectionState.Open)
                con1.Close();
            BindGrid();    //bindaddgrid(); comment by ashu 25 feb 2017
            
        }

        
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        this.BindGrid();
    }

    private void bindaddgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_secondryloadbindadd", con))
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AcademicYear", drpAcademicYear.SelectedValue);
                cmd.Parameters.AddWithValue("@collegecode", Session["GlobalDimension1Code"].ToString());
                cmd.Parameters.AddWithValue("@Employeecode", ddlFaculty.SelectedValue);
                cmd.Parameters.AddWithValue("@coursecode", drpCourse.SelectedValue);
                cmd.Parameters.AddWithValue("@semester", drpSemester.SelectedValue);                
                cmd.Parameters.AddWithValue("@secondrylodecode", ddlload.SelectedValue);
                
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    if (dt.Rows.Count > 0)
                    {
                    }
                }
            }
        }

    }


    private void BindGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_secondryload_", con))
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AcademicYear", drpAcademicYear.SelectedValue);
                cmd.Parameters.AddWithValue("@collegecode", Session["GlobalDimension1Code"].ToString());
                cmd.Parameters.AddWithValue("@Employeecode", ddlFaculty.SelectedValue);
                cmd.Parameters.AddWithValue("@coursecode", drpCourse.SelectedValue);              
                cmd.Parameters.AddWithValue("@semester", drpSemester.SelectedValue);

                if (ddlload.SelectedIndex > 0)
                {
                    cmd.Parameters.AddWithValue("@secondrylodecode", ddlload.SelectedValue);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@secondrylodecode", "");
                }
                //cmd.Parameters.AddWithValue("@fromdate", txtDateFrom.Text);
                //cmd.Parameters.AddWithValue("@todate", txtDateTo.Text);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    if (dt.Rows.Count > 0)
                    {
                        }
                }
            }
        }

    }


    protected void rblUnivCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCollege.Enabled = true;
        ddlCollege.DataSource = "";
        ddlCollege.DataBind();
        GetFacultyList();
       
    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCollege();
    }

    protected void rblOddEvenYear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = sender as Button;
            GridViewRow grow = btn.NamingContainer as GridViewRow;
              hfEntryNo.Value = GridView1.DataKeys[grow.RowIndex].Value.ToString();            
            drpCourse.SelectedValue = ((HiddenField)GridView1.Rows[grow.RowIndex].FindControl("hfCourseCode")).Value;
            BindSemester();
            drpSemester.SelectedValue = ((Label)GridView1.Rows[grow.RowIndex].FindControl("lblSemYear")).Text.Trim(); 
            rblUnivCollege.SelectedValue = ((HiddenField)GridView1.Rows[grow.RowIndex].FindControl("hfCollUniv")).Value; 
            ddlFaculty.SelectedValue = ((HiddenField)GridView1.Rows[grow.RowIndex].FindControl("hfFacultyCode")).Value;
            BindCollege();
            ddlCollege.SelectedValue = ((Label)GridView1.Rows[grow.RowIndex].FindControl("lblCollegeCode")).Text;
            ddlload.SelectedValue = ((HiddenField)GridView1.Rows[grow.RowIndex].FindControl("hfDescriptionCode")).Value; 
            txtRemarks.Text = ((Label)GridView1.Rows[grow.RowIndex].FindControl("lblRemarks")).Text; 
            Btnadd.Text = "Update";
        }
        catch
        {

        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {  
           // string pk = storyGridView.DataKeys[row.RowIndex].Values[0].ToString();
            Button btn = sender as Button;
            GridViewRow grow = btn.NamingContainer as GridViewRow;
            int EntryNo = Convert.ToInt16(GridView1.DataKeys[grow.RowIndex].Value.ToString());
            SqlCommand cmd = new SqlCommand("Sp_DeleteOrInactiveSecondaryLoad", con1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EntryNo", EntryNo);
            cmd.Parameters.AddWithValue("@UserId", (Session["uid"].ToString()));
            cmd.Parameters.AddWithValue("@InactiveDelete", 2);
            con1.Open();
            int ID = cmd.ExecuteNonQuery();
            con1.Close();
            BindGrid();
        }
        catch
        {

        }
        finally
        {
            if (con1.State == ConnectionState.Open)
                con1.Close();
        }
    }
    protected void chkInactive_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            CheckBox  chk = sender as CheckBox ;
            GridViewRow grow = chk.NamingContainer as GridViewRow;
            int EntryNo = Convert.ToInt16(GridView1.DataKeys[grow.RowIndex].Value.ToString());
            SqlCommand cmd = new SqlCommand("Sp_DeleteOrInactiveSecondaryLoad", con1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EntryNo", EntryNo);
            cmd.Parameters.AddWithValue("@UserId", (Session["uid"].ToString()));
            cmd.Parameters.AddWithValue("@InactiveDelete", 1);
            con1.Open();            
            int ID = cmd.ExecuteNonQuery();            
            con1.Close();
            BindGrid();
        }
        catch
        {

        }
        finally
        {
            if (con1.State == ConnectionState.Open)
                con1.Close();
        }
    }
}