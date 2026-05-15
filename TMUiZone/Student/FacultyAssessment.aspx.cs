using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;

public partial class Student_FacultyAssessment : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString);
    DataTable dt = new DataTable();
    static string chk;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            chkrecord();
            btnSubmit.Visible = false;// for some days Requested by 
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    // check for record exit or not ----- sandeep--- 28-11-2016

    public void chkrecord()
    {
        con.Close();
        SqlCommand cmd = new SqlCommand("select * from TMU$Feedback where StudentNo='" + Session["uid"].ToString() + "' and AcademicYear='" + Session["AcademicYear"].ToString() + "' and ([Semester]='" + Session["Semester"].ToString() + "' or [Semester]='" + Session["Year"].ToString() + "')", con);
        con.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            con.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Feed Back Already submitted !');", true);
            //Thread.Sleep(20000);
            //Response.Redirect("~/Student/StudentDetailsView1.aspx");
            StudetailPanel.Visible = false;
            btnSubmit.Enabled = false;
            btnSubmit.Visible = false;
            lblmsg.Visible = true;
            return;
        }
        else
        {
            con.Close();
            StudetailPanel.Visible = true;
            btnSubmit.Visible = true;
            lblmsg.Visible = false;

            btnSubmit.Enabled = true;
            if (!IsPostBack)
            {
                lblCourse.Text = Session["CourseCode"].ToString();
                lblSemester.Text = Session["Semester"].ToString();
                lblSection.Text = Session["Section"].ToString();
                lblYear.Text = Session["Year"].ToString();
                lblDate.Text = System.DateTime.Today.ToShortDateString();
                lblCollegeName.Text = Session["College"].ToString();
                BindAssessmentTable("");
            }
            else
                BindAssessmentTable("");
        }
        con.Close();
    }
    //end 
    public void BindData()
    {
        SqlCommand cmd = new SqlCommand("select [Faculty Name]+'='+[Faculty Code] as [Faculty Name],[Subject Code] from [TMU$Faculty Subject - COL] where [Course Code]='" + Session["CourseCode"].ToString() + "' " + @"
        and [Semester Code]='" + Session["Semester"].ToString() + "' and [Section Code]='" + Session["Section"].ToString() + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
    }

    public void BindAssessmentTable(string d)
    {
        string CollegeCode=Session["College"].ToString();
        string ProcName="";
        if (CollegeCode == "TMDC" || CollegeCode == "TMMC") { ProcName = "SP_FacultyAssesmentSubjectForTMDCAndTMMC"; } else{ ProcName = "SP_FacultyAssesmentSubject"; }
        SqlCommand cmd = new SqlCommand(ProcName, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        Session["FacultySubject"] = dt;
        TextBox tb;
        DropDownList dp;
        TableHeaderRow hr = new TableHeaderRow();
        int tab=1;
        for (int i = 1; i < 13; i++)
        {
            TableRow tr = new TableRow();
            for (int j = 1; j < dt.Rows.Count + 3; j++)
            {
                TableCell tc = new TableCell();
                Label l = new Label();
                tb = new TextBox();
                string rowcol = (i - 1) + "_" + (j - 1);
                tb.ID = rowcol;
                tb.MaxLength = 2;
                dp = new DropDownList();                
                dp.TabIndex =(short)(tab+1);
                dp.ForeColor = System.Drawing.Color.Blue;
                dp.CssClass = "form-control";
                dp.ID = rowcol;
                dp.Width = 70;
                dp.Items.Add("");
                dp.Items.Add("P");
                dp.Items.Add("A");
                dp.Items.Add("G");
                dp.Items.Add("VG");
                dp.Items.Add("E");
               
                if (j == 1)
                {


                    if (i == 1)
                    {
                        l.Text = "Criteria / Dimensions";
                        tc.CssClass = "Bold";
                    }
                    if (i == 2)
                    {
                        tc.CssClass = "Bold";
                    }

                    if (i == 3)
                        l.Text = "Punctuality of Teacher";
                    if (i == 4)
                        l.Text = "Communication Skills";
                    if (i == 5)
                        l.Text = "Subject knowledge";
                    if (i == 6)
                        l.Text = "Timely coverage of syllabus";
                    if (i == 7)
                        l.Text = "Syllabus coverage with relevant practical examples, case studies, presentation,numerical and quiz etc.";
                    if (i == 8)
                        l.Text = "Teacher's behaviour in terms of impartiality, decency and focus on teaching";
                    if (i == 9)
                        l.Text = "Efficiency and confidence in handling class/queries";
                    if (i == 10)
                        l.Text = "Giving appropriate comments, suggestions while returning answer papers and assignment";
                    if (i == 11)
                        l.Text = "Encourage and assist, Students to enhance knowledge, skills and personality and takes keen interest in their mentoring";
                    if (i == 12)
                        l.Text = "Your rating on considering the teacher as your role model";
                    l.ID = rowcol;
                    tc.Controls.Add(l);
                    tr.Cells.Add(tc);
                }
                else
                    if (j == 2)
                    {
                        if (i == 1)
                        {
                            l.Text = "Subject Code";
                            tc.CssClass = "Bold";
                        }
                        else
                            if (i == 2)
                            {
                                l.Text = "Faculty Name";
                                tc.CssClass = "Bold";
                            }
                            else
                                tc.CssClass = "Color";

                        tc.Controls.Add(l);
                        tr.Cells.Add(tc);
                    }
                    else
                       
                        tc.Controls.Add(dp);
                tr.Cells.Add(tc);
                FacultyAssessmentTable.Rows.Add(tr);
            }
        }

        for (int i = 0; i < 13; i++)
        {
            for (int j = 2; j < dt.Rows.Count + 2; j++)
            {
                if (i == 0)
                {
                    FacultyAssessmentTable.Rows[i].Cells[j].Text = dt.Rows[j - 2]["Subject Code"].ToString();
                    FacultyAssessmentTable.Rows[i + 1].Cells[j].Text = dt.Rows[j - 2]["Faculty Name"].ToString();
                    FacultyAssessmentTable.Rows[i].Cells[j].CssClass = "Bold";
                    FacultyAssessmentTable.Rows[i + 1].Cells[j].CssClass = "Bold";
                }
            }
        }
        if (dt.Rows.Count <= 0)
            btnSubmit.Visible = false;

    }


    public void chkblank()  //sandeep chek all option fill by student 16/12/2016
    {
        for (int i = 2; i < FacultyAssessmentTable.Rows.Count; i++)//row
        {
            for (int j = 0; j < FacultyAssessmentTable.Rows[i].Cells.Count; j++)//col
            {
                if (i > 1 && j > 1) //dropdown
                {
                    string rating = "";
                    Label hy = (Label)FacultyAssessmentTable.Rows[i].Cells[0].FindControl(i + "_" + 0);
                    DropDownList s = (DropDownList)FacultyAssessmentTable.Rows[i].Cells[j].FindControl(i + "_" + j);
                    rating = s.Text;

                    if (rating == "")
                    {
                        chk = "1";
                    }
                    else
                    {
                        chk = "0";
                    }
                }
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Exist();
       chkblank();
        DataTable dt=(DataTable)Session["FacultySubject"];
        if (chk == "0")
        {
            for (int i = 2; i < FacultyAssessmentTable.Rows.Count; i++)//row
            {
                for (int j = 0; j < FacultyAssessmentTable.Rows[i].Cells.Count; j++)//col
                {
                    if (i > 1 && j > 1) //dropdown
                    {
                        string rating = "", criteria = ""; int rate=0;
                        DropDownList s = (DropDownList)FacultyAssessmentTable.Rows[i].Cells[j].FindControl(i + "_" + j);
                        rating = s.Text;
                        if (rating == "P")
                        {
                            rate=1;
                        }
                        if (rating == "A")
                        {
                            rate = 2;
                        }
                        if (rating == "G")
                        {
                            rate = 3;
                        }
                        if (rating == "VG")
                        {
                            rate = 4;
                        }
                        if (rating == "E")
                        {
                            rate = 5;
                        }
                        
                        
                        Label hy = (Label)FacultyAssessmentTable.Rows[i].Cells[0].FindControl(i + "_" + 0);
                        criteria = hy.Text.Replace("'", "''");
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        string Subject = FacultyAssessmentTable.Rows[0].Cells[j].Text;
                        string str = dt.Rows[j - 2]["Faculty Subject"].ToString(); //FacultyAssessmentTable.Rows[1].Cells[j].Text;
                        string[] str1 = str.Split('=');
                        string fcode = str1[1];
                        string fname = str1[0];
                        string ColCode = Session["College"].ToString();

                        string SemYear="";
                        if (lblSemester.Text != "") { SemYear = lblSemester.Text; } else { SemYear = lblYear.Text; }
                        SqlCommand cmd1 = new SqlCommand("INSERT INTO [dbo].[TMU$Feedback] ([StudentNo],[Date],[FeedbackFor],[Remarks],[Type],[Rate],Course,Semester, Section, AcademicYear,subjectcode,facultycode,CollegeCode ) VALUES ('" + Session["uid"].ToString() + "',convert(date,getdate()),'" + fname + "' ,'" + criteria + "','STUDENT'," + rate + ",'" + Session["CourseCode"].ToString() + "','" + SemYear + "','" + Session["Section"].ToString() + "','" + Session["AcademicYear"].ToString() + "','" + Subject + "','" + fcode + "','" + ColCode + "')", con);

                        cmd1.ExecuteNonQuery();
                    }
                }
            }
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Okay');", true);
        }
        else
        {    
         ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Please fill all Option', 'Please fill all Option');", true);
        }

    }
    public void Exist()
    {
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlCommand cmd = new SqlCommand("select case  count(*)  when '0' then 'False' else 'True' end as Exist from [TMU$Feedback] where [StudentNo]='" + Session["uid"].ToString() + "' and Course='" + Session["CourseCode"].ToString() + "' " + @"
        and [Semester]='" + Session["Semester"].ToString() + "' and [Section]='" + Session["Section"].ToString() + "' and AcademicYear='" + Session["AcademicYear"].ToString() + "'", con);
        bool result=Convert.ToBoolean(cmd.ExecuteScalar().ToString());
        con.Close();
        if (result == true)
        {
            return;
        }
    }
}