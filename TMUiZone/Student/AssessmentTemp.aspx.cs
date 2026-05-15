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

public partial class Faculty_AssessmentTemp : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString);
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {

                lblCourse.Text = Session["CourseCode"].ToString();
                con.Close();
                SqlCommand cmd = new SqlCommand("select distinct Semester,Year,[Section] from [TMU$Student Subject - COLLEGE] where [Course]='" + Session["CourseCode"].ToString() + "' and [Academic Year]='19-20' and [Enrollment No]='" + Session["enroll"].ToString() + "' and Semester =(select top 1  Semester from [TMU$Student Subject - COLLEGE] where [Course]='" + Session["CourseCode"].ToString() + "' and [Academic Year]='19-20' and [Enrollment No]='" + Session["enroll"].ToString() + "' order by Semester desc)", con);

                SqlDataAdapter customerDA = new SqlDataAdapter();
                customerDA.SelectCommand = cmd;
                con.Open();
                DataTable customerDS = new DataTable();
                customerDA.Fill(customerDS);
                con.Close();
                if (customerDS.Rows.Count > 0)
                {
                    lblSemester.Text = customerDS.Rows[0]["Semester"].ToString();
                    lblSection.Text = customerDS.Rows[0]["Section"].ToString();
                    lblYear.Text = customerDS.Rows[0]["Year"].ToString();
                    lblDate.Text = System.DateTime.Today.ToShortDateString();
                    lblCollegeName.Text = Session["College"].ToString();
                }
                if (OpenForm() == 0 || OpenForm() == 2)
                {
                    if (OpenForm() == 2) { StudetailPanel.Visible = false; btnSubmit.Visible = false; }
                    else
                    {
                        //BindAssessmentTable("");
                        chkrecord();
                        //btnSubmit.Visible = false;
                    }
                }
                else
                {
                    chkrecord();
                }
            }
            else
            {
                chkrecord();
            }

        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    public void chkrecord()
    {
        con.Close();
        SqlCommand cmd = new SqlCommand("select * from FacultyFeedback where StudentNo='" + Session["uid"].ToString() + "' and AcademicYear='" + lblAcad.Text + "' and ([Semester]='" + lblSemester.Text + "' or [Semester]='" + lblYear.Text + "')", con);
        con.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            con.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Feed Back Already submitted !');", true);
            StudetailPanel.Visible = false; lblmsg.Visible = true; btnSubmit.Visible = false;
            return;
        }
        else
        {
            con.Close();
            StudetailPanel.Visible = true; lblmsg.Visible = false; btnSubmit.Visible = true;
            BindAssessmentTable("");
        }
        con.Close();
    }
    public int OpenForm()
    {
        int a = 2;
        String OddEvenYear = "YEAR";
        SqlCommand cmd12 = new SqlCommand("GetFormVisibilitytemp", con);
        cmd12.CommandType = CommandType.StoredProcedure;
        if (lblSemester.Text.Trim() == "I" || lblSemester.Text.Trim() == "III" || lblSemester.Text.Trim() == "V"
          || lblSemester.Text.Trim() == "VII" || lblSemester.Text.Trim() == "IX" || lblSemester.Text.Trim() == "XI")
        { OddEvenYear = "ODD"; }
        if (lblSemester.Text.Trim() == "II" || lblSemester.Text.Trim() == "IV" || lblSemester.Text.Trim() == "VI"
         || lblSemester.Text.Trim() == "VIII" || lblSemester.Text.Trim() == "X" || lblSemester.Text.Trim() == "XII")
        { OddEvenYear = "EVEN"; }

        cmd12.Parameters.Add("@OddEvenYear", OddEvenYear);
        cmd12.Parameters.Add("@AcademicYear", Session["AcademicYear"].ToString());
        cmd12.Parameters.Add("@FormName", "Faculty Assessment");
        if (con.State == ConnectionState.Closed)
            con.Open();
        a = Convert.ToInt16(cmd12.ExecuteScalar());
        con.Close();
        return a;

    }

    public void BindAssessmentTable(string d)
    {
        try
        {
            string CollegeCode = Session["College"].ToString();
            string ProcName = "";
            if (CollegeCode == "TMDC" || CollegeCode == "TMMC")
            { ProcName = "SP_FacultyAssesmentSubjectForTMDCAndTMMCTEMP"; }
            else { ProcName = "SP_FacultyAssesmentSubjectTemp"; }
            SqlCommand cmd = new SqlCommand(ProcName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AcademicYear", lblAcad.Text);
            cmd.Parameters.AddWithValue("@ProgramCode", lblCourse.Text);
            cmd.Parameters.AddWithValue("@Semester", lblSemester.Text);
            cmd.Parameters.AddWithValue("@Year", lblYear.Text);
            cmd.Parameters.AddWithValue("@Section", lblSection.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            Session["FacultySubject"] = dt;
            TextBox tb;
            DropDownList dp;
            DropDownList dp1;
            TextBox txtSubject;
            TextBox txtFaculty;
            
            TableHeaderRow hr = new TableHeaderRow();
            int tab = 1;
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
                    dp1 = new DropDownList();
                    txtSubject = new TextBox();
                    txtFaculty = new TextBox();
                    if (i == 1)
                    {
                        txtSubject.ID = rowcol;
                    }
                    if (i == 2)
                    {
                        txtFaculty.ID = rowcol;
                    }
                    txtSubject.CssClass = "form-control";
                    txtFaculty.CssClass = "form-control";
                    dp.TabIndex = (short)(tab + 1);
                    dp.ForeColor = System.Drawing.Color.Blue;
                    dp.CssClass = "form-control";
                    dp1.CssClass = "form-control";
                    dp.ID = rowcol;
                   
                    dp.Width = 70;
                    dp.Items.Add("");
                    dp.Items.Add(new ListItem("P", "1"));
                    dp.Items.Add(new ListItem("A", "2"));
                    dp.Items.Add(new ListItem("G", "3"));
                    dp.Items.Add(new ListItem("VG", "4"));
                    dp.Items.Add(new ListItem("E", "5"));
                    con.Close();
                    //SqlCommand cmdFaculty = new SqlCommand("select  distinct [Faculty Code],[Faculty Name] from [TMU$Course Wise Faculty] where [Academic Year]='19-20'  and [Course Code]='" + Session["CourseCode"].ToString() + "' and [Semester Code]='" + lblSemester.Text + "'", con);

                    //SqlDataAdapter FacultyDA = new SqlDataAdapter();
                    //FacultyDA.SelectCommand = cmdFaculty;
                    //con.Open();
                    //DataTable DTFaculty = new DataTable();
                    //FacultyDA.Fill(DTFaculty);
                    //con.Close();
                    //dp1.DataSource = DTFaculty;
                    //dp1.DataValueField = "Faculty Code";
                    //dp1.DataTextField = "Faculty Name";
                    //dp1.DataBind();
                    //dp1.Items.Insert(0,"--Select--");
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
                            if (dt.Rows[j - 3]["Faculty Name"].ToString() == "")
                                if (i == 1)
                                {
                                    tc.Controls.Add(txtSubject);
                                }
                                else if (i == 2)
                                {
                                    tc.Controls.Add(txtFaculty);
                                }
                               
                            else
                            {
                                tc.Controls.Add(dp);
                            }
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
                        if (dt.Rows[j - 2]["Subject Code"].ToString() == "9")
                        {
                            FacultyAssessmentTable.Rows[i].Cells[j].Text = "Subject";
                        }
                        if (dt.Rows[j - 2]["Faculty Name"].ToString() == "9")
                        {
                            FacultyAssessmentTable.Rows[i + 1].Cells[j].Text = "Faculty";
                        }
                        if (dt.Rows[j - 2]["Subject Code"].ToString() != "" && dt.Rows[j - 2]["Faculty Name"].ToString() != "")
                        {
                            FacultyAssessmentTable.Rows[i].Cells[j].Text = dt.Rows[j - 2]["Subject Code"].ToString();
                            FacultyAssessmentTable.Rows[i + 1].Cells[j].Text = dt.Rows[j - 2]["Faculty Name"].ToString();
                            FacultyAssessmentTable.Rows[i].Cells[j].CssClass = "Bold";
                            FacultyAssessmentTable.Rows[i + 1].Cells[j].CssClass = "Bold";
                        }
                    }
                }
            }
            if (dt.Rows.Count <= 0)
                btnSubmit.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        String Result = "SUCCESS";
        DataTable dt = (DataTable)Session["FacultySubject"];
        DataTable dt21 = new DataTable();
        for (int i = 0; i <= FacultyAssessmentTable.Rows.Count; i++)
        {
            if (i == 0)
                dt21.Columns.Add("Faculty Code");
            else if (i == 1)
                dt21.Columns.Add("Faculty Name");
            else if (i == 2)
                dt21.Columns.Add("Subject Code");
            else
                dt21.Columns.Add("Q" + (i - 2).ToString());
        }

        for (int i = 0; i < FacultyAssessmentTable.Rows[0].Cells.Count; i++)
        {
            DataRow dr = dt21.NewRow();

            if (i > 1)
            {
                //string str = dt.Rows[i - 2]["Faculty Subject"].ToString();
                //string[] str1 = str.Split('=');
                //string fcode = str1[1];
                //string fname = str1[0];
                dr[0] = "";
                TextBox txtFaculty = (TextBox)FacultyAssessmentTable.Rows[0].Cells[i].FindControl(0 + "_" + i);
                if (txtFaculty.Text != "")
                {
                    dr[1] = txtFaculty.Text;
                    TextBox txtSubject = (TextBox)FacultyAssessmentTable.Rows[1].Cells[i].FindControl(1 + "_" + i);
                    dr[2] = txtSubject.Text;


                    DropDownList s1 = (DropDownList)FacultyAssessmentTable.Rows[2].Cells[i].FindControl(2 + "_" + i);
                    dr[3] = s1.SelectedValue;
                    DropDownList s2 = (DropDownList)FacultyAssessmentTable.Rows[3].Cells[i].FindControl(3 + "_" + i);
                    dr[4] = s2.SelectedValue;
                    DropDownList s3 = (DropDownList)FacultyAssessmentTable.Rows[4].Cells[i].FindControl(4 + "_" + i);
                    dr[5] = s3.SelectedValue;
                    DropDownList s4 = (DropDownList)FacultyAssessmentTable.Rows[5].Cells[i].FindControl(5 + "_" + i);
                    dr[6] = s4.SelectedValue;
                    DropDownList s5 = (DropDownList)FacultyAssessmentTable.Rows[6].Cells[i].FindControl(6 + "_" + i);
                    dr[7] = s5.SelectedValue;
                    DropDownList s6 = (DropDownList)FacultyAssessmentTable.Rows[7].Cells[i].FindControl(7 + "_" + i);
                    dr[8] = s6.SelectedValue;
                    DropDownList s7 = (DropDownList)FacultyAssessmentTable.Rows[8].Cells[i].FindControl(8 + "_" + i);
                    dr[9] = s7.SelectedValue;
                    DropDownList s8 = (DropDownList)FacultyAssessmentTable.Rows[9].Cells[i].FindControl(9 + "_" + i);
                    dr[10] = s8.SelectedValue;
                    DropDownList s9 = (DropDownList)FacultyAssessmentTable.Rows[10].Cells[i].FindControl(10 + "_" + i);
                    dr[11] = s9.SelectedValue;
                    DropDownList s10 = (DropDownList)FacultyAssessmentTable.Rows[11].Cells[i].FindControl(11 + "_" + i);
                    dr[12] = s10.SelectedValue;
                    if (s1.SelectedValue == "" || s2.SelectedValue == "" || s3.SelectedValue == "" || s4.SelectedValue == "" || s5.SelectedValue == "" || s6.SelectedValue == ""
                        || s7.SelectedValue == "" || s8.SelectedValue == "" || s9.SelectedValue == "" || s10.SelectedValue == "") { Result = "ERROR"; break; }


                    if (i > 1)
                        dt21.Rows.Add(dr);
                }
            }
        }
        if (Result == "SUCCESS")
        {
            SqlCommand cmd12 = new SqlCommand("sp_insertFacultyFeedback", con);
            cmd12.CommandType = CommandType.StoredProcedure;
            cmd12.Parameters.Add("@StudentNo", Session["uid"].ToString());
            cmd12.Parameters.Add("@Course", lblCourse.Text);
            if (lblSemester.Text == "")
            { cmd12.Parameters.Add("@Semester", lblYear.Text); }
            else { cmd12.Parameters.Add("@Semester", lblSemester.Text); }
            cmd12.Parameters.Add("@Section", lblSection.Text);
            cmd12.Parameters.Add("@AcademicYear", lblAcad.Text);
            cmd12.Parameters.Add("@CollegeCode", Session["College"].ToString());
            cmd12.Parameters.Add("@FacultyFeedbackDetail", dt21);
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd12.ExecuteNonQuery();
            con.Close();
            btnSubmit.Visible = false;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Okay');", true);
        }
        //}
        else
        {

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Please fill all Option', 'Please fill all Option');", true);
        }

    }

}