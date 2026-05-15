using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Faculty_FacultyLoadCalculation : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["uid"].ToString() != null)
            {
                if (Session["UserGroup"].ToString() == "PRINCIPAL" || Session["UserGroup"].ToString() == "HOD" || Session["Hod"].ToString()!="")
                {
                    if (!IsPostBack)
                    {
                        bindAcademicYear();
                        bindcourse();
                        SemYear();
                        Subject();
                        Faculty();
                        Section();
                        Group();
                        Batch();
                        ShowAssignSubject();
                    }
                }
                else
                {
                    Response.Redirect("../Default.aspx");
                }
            }
            else
            {
                Response.Redirect("../Default.aspx"); 
            }
        }

        catch
        {
            Response.Redirect("../Default.aspx"); 
        }
    }
    public void bindAcademicYear()
    {
        SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da.Fill(dt1);
        ddlAcademicYear.DataSource = dt1;
        ddlAcademicYear.DataTextField = "Details";
        ddlAcademicYear.DataValueField = "No_";
        ddlAcademicYear.DataBind();
    }
    public void bindcourse()
    {
        SqlCommand cmd = new SqlCommand("Sp_Course_Load", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CollageCode", Session["GlobalDimension1Code"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlCourse.DataSource = dt;
        ddlCourse.DataTextField = "Details";
        ddlCourse.DataValueField = "No_";
        ddlCourse.DataBind();
    }
    public void SemYear()
    {
        SqlCommand cmd = new SqlCommand("Sp_SemYear_Load", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", ddlCourse.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlSemYear.DataSource = dt;
        ddlSemYear.DataTextField = "Details";
        ddlSemYear.DataValueField = "No_";
        ddlSemYear.DataBind();
    }

    public void Subject()
    {
        SqlCommand cmd = new SqlCommand("Sp_Subject_Load", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", ddlCourse.SelectedValue);
        cmd.Parameters.Add("@SemYear", ddlSemYear.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlSubject.DataSource = dt;
        ddlSubject.DataTextField = "Details";
        ddlSubject.DataValueField = "No_";
        ddlSubject.DataBind();
    }
    public void Faculty()
    {

        SqlCommand cmd = new SqlCommand("Sp_Faculty_Load", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", ddlCourse.SelectedValue);
        cmd.Parameters.Add("@SemYear", ddlSemYear.SelectedValue);
        cmd.Parameters.Add("@SubjectCode", ddlSubject.SelectedValue);
        
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlFaculty.DataSource = dt;
        ddlFaculty.DataTextField = "Details";
        ddlFaculty.DataValueField = "No_";
        ddlFaculty.DataBind();
    }
    public void Section()
    {

        SqlCommand cmd = new SqlCommand("Sp_Section_Load", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", ddlCourse.SelectedValue);
        cmd.Parameters.Add("@SemYear", ddlSemYear.SelectedValue);
        cmd.Parameters.Add("@SubjectCode", ddlSubject.SelectedValue);
        //cmd.Parameters.Add("@FacultyCode", ddlFaculty.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlSection.DataSource = dt;
        ddlSection.DataTextField = "Details";
        ddlSection.DataValueField = "No_";
        ddlSection.DataBind();
    }

    public void Group()
    {

        SqlCommand cmd = new SqlCommand("Sp_Group_Load", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", ddlCourse.SelectedValue);
        cmd.Parameters.Add("@SemYear", ddlSemYear.SelectedValue);
        cmd.Parameters.Add("@SubjectCode", ddlSubject.SelectedValue);
        cmd.Parameters.Add("@Section", ddlSection.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlGroup.DataSource = dt;
        ddlGroup.DataTextField = "Details";
        ddlGroup.DataValueField = "No_";
        ddlGroup.DataBind();
    }

    public void Batch()
    {

        SqlCommand cmd = new SqlCommand("Sp_Batch_Load", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", ddlCourse.SelectedValue);
        cmd.Parameters.Add("@SemYear", ddlSemYear.SelectedValue);
        cmd.Parameters.Add("@SubjectCode", ddlSubject.SelectedValue);
        cmd.Parameters.Add("@Section", ddlSection.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlBatch.DataSource = dt;
        ddlBatch.DataTextField = "Details";
        ddlBatch.DataValueField = "No_";
        ddlBatch.DataBind();
    }
    public void CheckAndInsert()
    {
        int Load = 0, LoadCount = 0; 
        // fatch max load of Faculty
        SqlCommand cmdMaxLoad = new SqlCommand("Sp_MaxLoad_Laod", con);
        cmdMaxLoad.CommandType = CommandType.StoredProcedure;
        cmdMaxLoad.Parameters.Add("@FaucultyCode", ddlFaculty.SelectedValue);
        con.Open();
        SqlDataReader dr = cmdMaxLoad.ExecuteReader();
        dr.Read();
        int MaxLoad=int.Parse(dr["MaxLoad"].ToString());
        con.Close();
        if (MaxLoad == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Load Assign On Desgination');", true);
            
            return;
        }
        //end maxLoad
        else
        {

            // Load Of Subject And Load count of subject ..... load count means -->how many times the subject will assign 


            SqlCommand cmdLoadAndLoadCount = new SqlCommand("Sp_LoadAndLoadCount_Load", con);
            cmdLoadAndLoadCount.CommandType = CommandType.StoredProcedure;
            cmdLoadAndLoadCount.Parameters.Add("@FacultyCode", ddlFaculty.SelectedValue);
            cmdLoadAndLoadCount.Parameters.Add("@SubjectCode", ddlSubject.SelectedValue);
            cmdLoadAndLoadCount.Parameters.Add("@CourseCode", ddlCourse.SelectedValue);
            cmdLoadAndLoadCount.Parameters.Add("@SemYear", ddlSemYear.SelectedValue);
            con.Open();
            SqlDataReader drLoadAndLoadCount = cmdLoadAndLoadCount.ExecuteReader();
            drLoadAndLoadCount.Read();
             Load = int.Parse(drLoadAndLoadCount["Load"].ToString());
             LoadCount = int.Parse(drLoadAndLoadCount["LoadCount"].ToString());
            con.Close();
            if (Load == 0 || LoadCount == 0)
            {
                if (Load == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Assign Subject Load ! ');", true);

                    return;
                }
                if (LoadCount == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Assign No of Section !');", true);

                    return;
                }
            }
            //end of load And Load Count

            else
            {


                SqlCommand cmdLoadOfFaculty = new SqlCommand("select * from TMUCourseSubjectAssign where [Faculty Code]='" + ddlFaculty.SelectedValue + "' and Inactive=0",con);
                cmdLoadAndLoadCount.CommandType = CommandType.Text;
                con.Open();
                //SqlDataReader drLoadOfFaculty = cmdLoadOfFaculty.ExecuteReader();
                SqlDataAdapter da = new SqlDataAdapter(cmdLoadOfFaculty);
                DataTable dt = new DataTable();
                da.Fill(dt);
               int FacultyTotalLoad=0;
                for (int i=0; i < dt.Rows.Count;i++ )
                {
                    
                    string SemYear = "";
                    if (dt.Rows[i]["Semester"].ToString() == "") { SemYear = dt.Rows[i]["Year"].ToString(); } else { SemYear = dt.Rows[i]["Semester"].ToString(); }
                    SqlCommand cmd1 = new SqlCommand("Sp_LoadAndLoadCount_Load", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add("@FacultyCode", dt.Rows[i]["Faculty Code"].ToString());
                    cmd1.Parameters.Add("@SubjectCode", dt.Rows[i]["Subject Code"].ToString());
                    cmd1.Parameters.Add("@CourseCode", dt.Rows[i]["Course Code"].ToString());
                    cmd1.Parameters.Add("@SemYear", SemYear);
                   // drLoadOfFaculty.Close();
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dr1.Read();
                    FacultyTotalLoad += int.Parse(dr1["Load"].ToString());
                    dr1.Close();
                }
                con.Close();

                if (FacultyTotalLoad + Load <= MaxLoad)
                {
                    string SemYear = "";

                    // if (drLoadOfFaculty["Semester"].ToString() == "") { SemYear = drLoadOfFaculty["Year"].ToString(); } else { SemYear = drLoadOfFaculty["Semester"].ToString(); }
                    SqlCommand cmdSubjectAssignNoOfTimes = new SqlCommand("Sp_CountSubjectAssignNoOfTimes", con);
                    cmdSubjectAssignNoOfTimes.CommandType = CommandType.StoredProcedure;
                    cmdSubjectAssignNoOfTimes.Parameters.Add("@SubjectCode", ddlSubject.SelectedValue);
                    cmdSubjectAssignNoOfTimes.Parameters.Add("@CourseCode", ddlCourse.SelectedValue);
                    cmdSubjectAssignNoOfTimes.Parameters.Add("@Semester", ddlSemYear.SelectedValue);
                    con.Open();
                    SqlDataReader dr2 = cmdSubjectAssignNoOfTimes.ExecuteReader();
                    dr2.Read();
                    int CountSubjectAssignNoOfTimes = int.Parse(dr2["SubjectAssignNoOfTimes"].ToString());
                    con.Close();
                    if (CountSubjectAssignNoOfTimes+1<= LoadCount)
                    {
                        //insert
                        SqlCommand cmdDublicate = new SqlCommand("Sp_ChkDublicateReordInAssign_Load", con);
                        cmdDublicate.CommandType = CommandType.StoredProcedure;

                        cmdDublicate.Parameters.Add("@CourseCode", ddlCourse.SelectedValue);
                        cmdDublicate.Parameters.Add("@SubjectCode", ddlSubject.SelectedValue);
                         cmdDublicate.Parameters.Add("@SemesterYear",ddlSemYear.SelectedValue);
                         cmdDublicate.Parameters.Add("@Section", ddlSection.SelectedValue);

                         cmdDublicate.Parameters.Add("@FacultyCode", ddlFaculty.SelectedValue);
                         cmdDublicate.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
                         con.Open();
                         SqlDataReader drDublicate = cmdDublicate.ExecuteReader();

                         if (drDublicate.Read())
                         {
                             con.Close();
                             ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Dublicate Record not allow');", true);
                             return;
                         }
                         else
                         {
                             con.Close();

                             SqlCommand cmdInsertAssignNoOfTimes = new SqlCommand("Sp_InsertTMUCourseSubjectAssign", con);
                             cmdInsertAssignNoOfTimes.CommandType = CommandType.StoredProcedure;
                             cmdInsertAssignNoOfTimes.Parameters.Add("@CourseCode", ddlCourse.SelectedValue);
                             cmdInsertAssignNoOfTimes.Parameters.Add("@SubjectCode", ddlSubject.SelectedValue);
                             // cmdInsertAssignNoOfTimes.Parameters.Add("@Semester",);
                             //cmdInsertAssignNoOfTimes.Parameters.Add("@Year",);
                             cmdInsertAssignNoOfTimes.Parameters.Add("@Section", ddlSection.SelectedValue);
                             cmdInsertAssignNoOfTimes.Parameters.Add("@StudentGroup", ddlGroup.SelectedValue);
                             cmdInsertAssignNoOfTimes.Parameters.Add("@StudentBatch", ddlBatch.SelectedValue);
                             cmdInsertAssignNoOfTimes.Parameters.Add("@FacultyCode", ddlFaculty.SelectedValue);
                             cmdInsertAssignNoOfTimes.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
                             cmdInsertAssignNoOfTimes.Parameters.Add("@Description", ddlSubject.SelectedItem.Text);
                             cmdInsertAssignNoOfTimes.Parameters.Add("@GlobalDimension1Code", Session["GlobalDimension1Code"].ToString());
                             cmdInsertAssignNoOfTimes.Parameters.Add("@Createdby", Session["uid"].ToString());
                             cmdInsertAssignNoOfTimes.Parameters.Add("@UpdatedBy", Session["uid"].ToString());
                             con.Open();
                             cmdInsertAssignNoOfTimes.ExecuteNonQuery();
                             con.Close();
                             ShowAssignSubject();
                         }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'All Section Assign');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Faculty Load Exceed');", true);
                    return;
                }

            }


        }//end os maxLoad If

    }

    public void ShowAssignSubject()
    {
        SqlCommand cmdInsertAssignNoOfTimes = new SqlCommand("Sp_GetAssignSubjectOnFaculty", con);
        cmdInsertAssignNoOfTimes.CommandType = CommandType.StoredProcedure;
        cmdInsertAssignNoOfTimes.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmdInsertAssignNoOfTimes.Parameters.Add("@CreatedBy", Session["uid"].ToString());
        cmdInsertAssignNoOfTimes.Parameters.Add("@AcademicYear",ddlAcademicYear.SelectedValue);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmdInsertAssignNoOfTimes);
        DataTable dt = new DataTable();
        da.Fill(dt);
        grvAssignSubject.DataSource = dt;
        grvAssignSubject.DataBind();
        ViewState["SubjectList"] = dt;
    }

    public void SearchInGrid()
    {
        
        DataTable dtList = new DataTable();
        dtList = (DataTable)ViewState["SubjectList"];
        if (dtList.Rows.Count > 0)
        {
            DataView dv = new DataView(dtList);
            if (ddlCourse.SelectedValue != "")
            {
                if (ddlSemYear.SelectedValue != "")
                {
                    if (ddlSubject.SelectedValue != "")
                    {
                        if (ddlFaculty.SelectedValue != "")
                        {
                            if (ddlSection.SelectedValue != "")
                            {
                                if (ddlGroup.SelectedValue != "")
                                {
                                    if (ddlBatch.SelectedValue != "")
                                    {
                                        dv.RowFilter = "Course like '%" + ddlCourse.SelectedValue + "%' and [Semester/Year] = '" + ddlSemYear.SelectedValue + "' and Subject like '%" + ddlSubject.SelectedValue + "%' and Faculty like '%" + ddlFaculty.SelectedValue + "%' and Section='" + ddlSection.SelectedValue + "' and Group='" + ddlGroup.SelectedValue + "' and Batch='"+ddlBatch.SelectedValue+"'";

                                    }
                                    else
                                    {


                                        dv.RowFilter = "Course like '%" + ddlCourse.SelectedValue + "%' and [Semester/Year] = '" + ddlSemYear.SelectedValue + "' and Subject like '%" + ddlSubject.SelectedValue + "%' and Faculty like '%" + ddlFaculty.SelectedValue + "%' and Section='" + ddlSection.SelectedValue + "' and Group='" + ddlGroup.SelectedValue + "'";

                                    }
                                }
                                else
                                {
                                    dv.RowFilter = "Course like '%" + ddlCourse.SelectedValue + "%' and [Semester/Year] = '" + ddlSemYear.SelectedValue + "' and Subject like '%" + ddlSubject.SelectedValue + "%' and Faculty like '%" + ddlFaculty.SelectedValue + "%' and Section='" + ddlSection.SelectedValue + "' ";
                                }
                            }
                            else
                            {
                                dv.RowFilter = "Course like '%" + ddlCourse.SelectedValue + "%' and [Semester/Year] = '" + ddlSemYear.SelectedValue + "' and Subject like '%" + ddlSubject.SelectedValue + "%' and Faculty like '%" + ddlFaculty.SelectedValue + "%'";
                            }
                        }
                        else
                        {
                            dv.RowFilter = "Course like '%" + ddlCourse.SelectedValue + "%' and [Semester/Year] = '" + ddlSemYear.SelectedValue + "' and Subject like '%" + ddlSubject.SelectedValue + "%'";
                        }
                    }
                    else
                    {
                        dv.RowFilter = "Course like '%" + ddlCourse.SelectedValue + "%' and [Semester/Year] = '" + ddlSemYear.SelectedValue + "'";
                    }

                    

                }
                else
                {
                    dv.RowFilter = "Course like '%" + ddlCourse.SelectedValue + "%'";
                }

                
            }

            grvAssignSubject.DataSource = dv;
            grvAssignSubject.DataBind();
        }

        
    }

    public void DeleteSubjectLoad(int lineNo)
    {
        SqlCommand cmdDeleteAssignNoOfTimes = new SqlCommand("Sp_ChkTimeTableFor_Load", con);
        cmdDeleteAssignNoOfTimes.CommandType = CommandType.StoredProcedure;
        cmdDeleteAssignNoOfTimes.Parameters.Add("@LineNo_", lineNo);
        con.Open();
        cmdDeleteAssignNoOfTimes.ExecuteNonQuery();
        con.Close();
        ShowAssignSubject();
    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        SemYear();
        Subject();
        Faculty();
        Section();
        Group();
        Batch();
    }
    protected void ddlSemYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Subject();
        Faculty();
        Section();
        Group();
        Batch();
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        Faculty();
        Section();
        Group();
        Batch();
    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        Section();
        Group();
        Batch();
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        Group();
        Batch();
    }
    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlCourse.SelectedValue != "" && ddlSemYear.SelectedValue != "" && ddlSubject.SelectedValue != "" && ddlFaculty.SelectedValue != "")
        {
            CheckAndInsert();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Please Select Mandatory fields');", true);
            return;
        }
    }
    
   
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
        int index = row.RowIndex;
        LinkButton chk = (LinkButton)grvAssignSubject.Rows[index].FindControl("btnDelete");
        
        int nstockid = Convert.ToInt32(grvAssignSubject.DataKeys[index].Value);
        DeleteSubjectLoad(nstockid);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SearchInGrid();
    }
}