using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_MapFacultySubject1 : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    DataTable dt = new DataTable();
    DataRow row;
    static int p = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                bindAcademicYear();
                bindSubject();
                p = 0;
               
                BindAssignSubjectGroup();
                hdnLoad.Value = "0";
            }
        }
        catch
        { Response.Redirect("../Default.aspx");}
    }
    public void bindAcademicYear()
    {
        SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da.Fill(dt1);
        drpAcademicYear.DataSource = dt1;
        drpAcademicYear.DataTextField = "Details";
        drpAcademicYear.DataValueField = "No_";
        drpAcademicYear.DataBind();
    }
    public void GetLoad()
    {
        //string Subject = "";
        //if (txtSubject.Text.Contains("(") && txtSubject.Text.Contains(")"))
        //    Subject = txtSubject.Text.Split('(', ')')[1];

        SqlCommand cmd1 = new SqlCommand("sp_GetLoad", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.Add("@Batch", drpBatch.SelectedValue);
        cmd1.Parameters.Add("@Group", drpGroup.SelectedValue);
        cmd1.Parameters.Add("@FacultyCode", drpFaculty.SelectedValue);
        cmd1.Parameters.Add("@SubjectCode", DdlSubject.SelectedValue);
        cmd1.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd1.Parameters.Add("@SemYear", drpSemester.SelectedValue);
        cmd1.Parameters.Add("@Section", drpSection.SelectedValue);
        cmd1.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd1.Parameters.Add("@Return1", SqlDbType.VarChar, 30);
        cmd1.Parameters.Add("@Return2", SqlDbType.VarChar, 30);
        cmd1.Parameters["@Return1"].Direction = ParameterDirection.Output;
        cmd1.Parameters["@Return2"].Direction = ParameterDirection.Output;
        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd1.ExecuteNonQuery();
        if (Convert.ToInt16(cmd1.Parameters["@Return1"].Value.ToString()) < 0)
            txtLoad.Text = "0";
        else
            txtLoad.Text = cmd1.Parameters["@Return1"].Value.ToString();
        string SubjectClassification = cmd1.Parameters["@Return2"].Value.ToString();
        if (SubjectClassification == "PRACTICAL" || SubjectClassification == "LAB")
            txtLoad.Enabled = true;
        else
            txtLoad.Enabled = false;
     
        con.Close();
    }
    public void bindFaculty()
    {
        string ShowAllFaculty="";
        if (chkShowAllFaculty.Checked == true)
            ShowAllFaculty = "1";
        else
            ShowAllFaculty = "0";
        SqlCommand cmd = new SqlCommand("sp_GetFacultyForChoice", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SubjectGroupCode", drpSubjectGroup.SelectedValue);
        cmd.Parameters.Add("@ShowAllFaculty", ShowAllFaculty);
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        if (con.State == ConnectionState.Closed)
            con.Open();
        drpFaculty.DataSource = cmd.ExecuteReader();
        drpFaculty.DataTextField = "Search Name";
        drpFaculty.DataValueField = "No_";
        drpFaculty.DataBind();
        con.Close();
    }
    public void bindSubject()
    {
        SqlCommand cmd = new SqlCommand("sp_GetSubjectGroupForChoice", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@OddEven", drpOddEven.SelectedValue);
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        if (con.State == ConnectionState.Closed)
            con.Open();
        drpSubjectGroup.DataSource = cmd.ExecuteReader();
        drpSubjectGroup.DataTextField = "SubjectGroupDescription";
        drpSubjectGroup.DataValueField = "SubjectGroupCode";
        drpSubjectGroup.DataBind();
        con.Close();
    }
    protected void drpFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindCourse();
    }
    protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindFaculty();
        bindCourse();
        bindSemester();
        bindSection();
        bindGroup();
        bindBatch();
    }
    public void bindCourse()
    {
        SqlCommand cmd = new SqlCommand("sp_GetCourseForChoice", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SubjectGroupCode", drpSubjectGroup.SelectedValue);
        if (con.State == ConnectionState.Closed)
            con.Open();
        drpCourse.DataSource = cmd.ExecuteReader();
        drpCourse.DataTextField = "CourseCode1";
        drpCourse.DataValueField = "CourseCode";
        drpCourse.DataBind();
        con.Close();
    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSubjectText();
        bindSemester();
    }
    public void bindSubjectText()
    {
        try {
            SqlCommand cmd = new SqlCommand("sp_GetSubjectForChoiceDDl", con);//sp_GetSubjectForChoice
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SubjectGroupCode", drpSubjectGroup.SelectedValue);
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //txtSubject.Text = cmd.ExecuteScalar().ToString();
            DdlSubject.DataSource = dt;
            DdlSubject.DataTextField = "Description";
            DdlSubject.DataValueField = "Code";
            DdlSubject.DataBind();
            con.Close();
            GetLoad();
        }catch(Exception ex) { }
        }
    public void bindSemester()
    {
        //string Subject = "";
        //if (txtSubject.Text.Contains("(") && txtSubject.Text.Contains(")"))
        //    Subject = txtSubject.Text.Split('(', ')')[1];

        SqlCommand cmd = new SqlCommand("sp_GetSemesterForChoice", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Course", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SubjectCode", DdlSubject.SelectedValue);
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        if (con.State == ConnectionState.Closed)
            con.Open();
        drpSemester.DataSource = cmd.ExecuteReader();
        drpSemester.DataTextField = "Description";
        drpSemester.DataValueField = "Code";
        drpSemester.DataBind();
        con.Close();
    }
    protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGroup();
        bindBatch();
        bindSection();
        GetLoad();
    }
    public void bindSection()
    {
        SqlCommand cmd = new SqlCommand("sp_GetSection_Load", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Course", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterYear", drpSemester.SelectedValue);
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        if (con.State == ConnectionState.Closed)
            con.Open();
        drpSection.DataSource = cmd.ExecuteReader();
        drpSection.DataTextField = "Description";
        drpSection.DataValueField = "Code";
        drpSection.DataBind();
        con.Close();
    }
    public void bindGroup()
    {
        SqlCommand cmd = new SqlCommand("sp_GetGroupForChoice", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Course", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterYear", drpSemester.SelectedValue);
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        if (con.State == ConnectionState.Closed)
            con.Open();
        drpGroup.DataSource = cmd.ExecuteReader();
        drpGroup.DataTextField = "Description";
        drpGroup.DataValueField = "Code";
        drpGroup.DataBind();
        con.Close();
    }
    public void bindBatch()
    {
        SqlCommand cmd = new SqlCommand("sp_GetBatchForChoice", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Course", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterYear", drpSemester.SelectedValue);
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        if (con.State == ConnectionState.Closed)
            con.Open();
        drpBatch.DataSource = cmd.ExecuteReader();
        drpBatch.DataTextField = "Batch Code Description";
        drpBatch.DataValueField = "Batch Code";
        drpBatch.DataBind();
        con.Close();
    }
    protected void chkShowAllFaculty_CheckedChanged(object sender, EventArgs e)
    {
        bindFaculty();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (hdnLoad.Value == "0")
        {
            hdnLoad.Value = txtLoad.Text;
        }
        if (ViewState["datatable"] != null) { p = 0; }

        if (p == 0)
        {
            DataColumn dc;
            dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "Subject";
            dt.Columns.Add(dc);
            dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "Faculty";
            dt.Columns.Add(dc);
            dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "Course";
            dt.Columns.Add(dc);
            dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "Semester";
            dt.Columns.Add(dc);
            dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "Section";
            dt.Columns.Add(dc);
            dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "Group1";
            dt.Columns.Add(dc);
            dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "Batch";
            dt.Columns.Add(dc);
            dc = new DataColumn();
     
            p = 8;
        }

       // string Subject = "";
        //if (txtSubject.Text.Contains("(") && txtSubject.Text.Contains(")"))
        //    Subject = txtSubject.Text.Split('(', ')')[1];

        if (ViewState["datatable"] != null)
            dt = (DataTable)ViewState["datatable"];

        bool exists = dt.AsEnumerable().Where(c => c.Field<string>("Course").Equals(drpCourse.SelectedValue) && c.Field<string>("Semester").Equals(drpSemester.SelectedValue) && c.Field<string>("Section").Equals(drpSection.SelectedValue) && c.Field<string>("Group1").Equals(drpGroup.SelectedValue) && c.Field<string>("Batch").Equals(drpBatch.SelectedValue)).Count() > 0;

        if (exists == false)
        {
            if (hdnLoad.Value == txtLoad.Text)
            {
                SqlCommand cmd = new SqlCommand("select Count(*) from [Course_Subject_Assign] where SubjectGRoupCode='" + drpSubjectGroup.SelectedValue + "' and [Course Code]='" + drpCourse.SelectedValue + "' and [Subject Code]='" + DdlSubject.SelectedValue + "' and Semester='" + drpSemester.SelectedValue + "' and Section='" + drpSection.SelectedValue + "' and [Student Group]='" + drpGroup.SelectedValue + "' and [Student Batch]='" + drpBatch.SelectedValue + "'", con);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                int res = Convert.ToInt16(cmd.ExecuteScalar());
                con.Close();
                //if (res == 0)
                {
                    row = dt.NewRow();
                    row["Subject"] = DdlSubject.SelectedValue;
                    row["Faculty"] = drpFaculty.SelectedValue;
                    row["Course"] = drpCourse.Text;
                    row["Semester"] = drpSemester.SelectedValue;
                    row["Section"] = drpSection.SelectedValue;
                    row["Group1"] = drpGroup.SelectedValue;
                    row["Batch"] = drpBatch.SelectedValue;

                    dt.Rows.Add(row);
                    ViewState["datatable"] = dt;
                    grdFacultySubjectGroup.DataSource = dt;
                    grdFacultySubjectGroup.DataBind();
                    grdFacultySubjectGroup.Visible = true;
                    btnSave.Visible = true;
                    drpSubjectGroup.Enabled = false;
                    drpFaculty.Enabled = false;
                    drpOddEven.Enabled = false;
                    Clear();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Different Load can not be added!');", true);
            }
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'You already assigned this values');", true);
            //}
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Can not add duplicate entry');", true);
        }
        if (grdFacultySubjectGroup.Rows.Count > 0) { btnSave.Visible = true; } else { btnSave.Visible = false; }
    }
    public void Clear()
    {
    }
    protected void grdFacultySubjectGroup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteItem")
        {
            GridViewRow rowItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int index = rowItem.RowIndex;

            GridViewRow row = grdFacultySubjectGroup.Rows[index];
            string Enroll = row.Cells[2].Text;
            dt = (DataTable)ViewState["datatable"];
            DataRow[] result = dt.Select("[Course] = '" + Enroll + "'");
            foreach (DataRow row1 in result)
            {
                if (row1["Course"].ToString().Trim().ToUpper().Contains(Enroll))
                    dt.Rows.Remove(row1);
            }
            grdFacultySubjectGroup.DataSource = dt;
            grdFacultySubjectGroup.DataBind();
            ViewState["datatable"] = dt;
            if (grdFacultySubjectGroup.Rows.Count == 0)
            {
                grdFacultySubjectGroup.Visible = false;
                btnSave.Visible = false;
                drpSubjectGroup.Enabled = true;
                drpFaculty.Enabled = true;
                drpOddEven.Enabled = true;
                Clear();
            }
            GetLoad();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {       
        DataTable dt1 = ViewState["datatable"] as DataTable;
        int Load = 0, LoadCount = 0;
        // fatch max load of Faculty
        SqlCommand cmdMaxLoad = new SqlCommand("Sp_MaxLoad_Laod", con);
        cmdMaxLoad.CommandType = CommandType.StoredProcedure;
        cmdMaxLoad.Parameters.Add("@FaucultyCode", dt1.Rows[0]["Faculty"].ToString());
        con.Open();
        SqlDataReader dr = cmdMaxLoad.ExecuteReader();
        dr.Read();
        int MaxLoad = int.Parse(dr["MaxLoad"].ToString());
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
            SqlCommand cmdLoadAndLoadCount = new SqlCommand("Sp_LoadAndLoadCount_Load_New", con);
            cmdLoadAndLoadCount.CommandType = CommandType.StoredProcedure;
            cmdLoadAndLoadCount.Parameters.Add("@SubjectGroupCode", drpSubjectGroup.SelectedValue);
            con.Open();
            Load = int.Parse(cmdLoadAndLoadCount.ExecuteScalar().ToString());
            con.Close();
            if (Load == 0)
            {
                if (Load == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Assign Subject Load ! ');", true);
                    return;
                }
            }
            //end of load And Load Count

            else
            {
                SqlCommand cmdLoadOfFaculty = new SqlCommand("select DocumentNo,Load from [Course_Subject_Assign]  where [Faculty Code]='" + dt1.Rows[0]["Faculty"].ToString() + "'  and Inactive=0 group by DocumentNo,Load", con);
                cmdLoadAndLoadCount.CommandType = CommandType.Text;
                con.Open();
                //SqlDataReader drLoadOfFaculty = cmdLoadOfFaculty.ExecuteReader();
                SqlDataAdapter da = new SqlDataAdapter(cmdLoadOfFaculty);
                DataTable dt = new DataTable();
                da.Fill(dt);
                int FacultyTotalLoad = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    FacultyTotalLoad += Convert.ToInt16(dt.Rows[i]["Load"].ToString());
                }
                con.Close();

                if (FacultyTotalLoad + Load <= MaxLoad)
                {
                    //if (drDublicate.Read())
                    if ("1"=="")
                    {
                        con.Close();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Dublicate Record not allow');", true);
                        return;
                    }
                    else
                    {
                        con.Close();
                        SqlCommand cmdInsertAssignNoOfTimes = new SqlCommand("sp_InsertCourseSubjectAssign", con);
                        cmdInsertAssignNoOfTimes.CommandType = CommandType.StoredProcedure;
                        cmdInsertAssignNoOfTimes.Parameters.Add("@CourseSub", dt1);
                        cmdInsertAssignNoOfTimes.Parameters.Add("@SubjectGroupCode", drpSubjectGroup.SelectedValue);
                        cmdInsertAssignNoOfTimes.Parameters.Add("@SubjectGroupDescription", drpSubjectGroup.SelectedItem.Text);
                        cmdInsertAssignNoOfTimes.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                        cmdInsertAssignNoOfTimes.Parameters.Add("@CreatedBy", Session["uid"].ToString());
                        cmdInsertAssignNoOfTimes.Parameters.Add("@UpdateBy", Session["uid"].ToString());
                        cmdInsertAssignNoOfTimes.Parameters.Add("@Load", hdnLoad.Value);
                        con.Open();
                        cmdInsertAssignNoOfTimes.ExecuteNonQuery();
                        con.Close();
                        //ShowAssignSubject();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Assign successfully');", true);
                        drpSubjectGroup.Enabled = true;
                        drpFaculty.Enabled = true;
                        drpOddEven.Enabled = true; drpSemester.SelectedIndex = -1;

                       
                        ViewState["datatable"] = dt1;
                        grdFacultySubjectGroup.DataSource = dt1;
                        grdFacultySubjectGroup.DataBind();
                        dt1.Rows.Clear();
                        BindAssignSubjectGroup();
                        hdnLoad.Value = "0";
                        Response.Redirect("MapFacultySubject1.aspx");
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
    public void BindAssignSubjectGroup()
    {
        SqlCommand cmd = new SqlCommand("sp_GetAssignSubjects", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@OddEven", drpOddEven.SelectedValue);
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        if (con.State == ConnectionState.Closed)
            con.Open();
        grdAssignSubjects.DataSource = cmd.ExecuteReader();
        grdAssignSubjects.DataBind();
        con.Close();
    }
    protected void grdAssignSubjects_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string SubjectCode = grdAssignSubjects.DataKeys[e.RowIndex].Value.ToString();
        SqlCommand cmd = new SqlCommand("delete from FacultySubjects where SubjectGroupCode='" + SubjectCode + "'", con);
        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        BindAssignSubjectGroup();
    }
    protected void grdAssignSubjects_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteItem")
        {
            GridViewRow rowItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int index = rowItem.RowIndex;

            GridViewRow row = grdAssignSubjects.Rows[index];
            string SubjectCode = row.Cells[0].Text == "&nbsp;" ? "" : row.Cells[0].Text;
            string SubjectGroup = row.Cells[1].Text == "&nbsp;" ? "" : row.Cells[1].Text;
            string FacultyCode = row.Cells[2].Text == "&nbsp;" ? "" : row.Cells[2].Text;
            string CourseCode = row.Cells[3].Text == "&nbsp;" ? "" : row.Cells[3].Text;
            string Semester = row.Cells[4].Text == "&nbsp;" ? "" : row.Cells[4].Text;
            string Section = row.Cells[5].Text == "&nbsp;" ? "" : row.Cells[5].Text;
            string Group = row.Cells[6].Text == "&nbsp;" ? "" : row.Cells[6].Text;
            string Batch = row.Cells[7].Text == "&nbsp;" ? "" : row.Cells[7].Text;
            SqlCommand cmd = new SqlCommand("delete from Course_Subject_Assign where [Course Code]='" + CourseCode + "' and Semester='" + Semester + "' and Section='" + Section + "' and [Student Group]='" + Group + "' and [Student Batch]='" + Batch + "' and [Subject Code]='" + SubjectCode + "' and SubjectGRoupDescription='" + SubjectGroup + "'", con);
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            BindAssignSubjectGroup();
        }
    }
    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetLoad();
    }
    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAssignSubjectGroup();
    }
    protected void drpOddEven_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSubject();
        BindAssignSubjectGroup();
    }

    protected void DdlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetLoad();
        bindSemester();
    }
}