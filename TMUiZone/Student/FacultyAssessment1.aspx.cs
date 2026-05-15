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

public partial class Student_FacultyAssessment1 : System.Web.UI.Page
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
                lblSemester.Text = Session["Semester"].ToString();
                lblSection.Text = Session["Section"].ToString();
                lblYear.Text = Session["Year"].ToString();
                lblDate.Text = System.DateTime.Today.ToShortDateString();
                lblCollegeName.Text = Session["College"].ToString();
                if ((OpenForm() == 0 || OpenForm() == 2) && Session["College"].ToString() != "TMFA")
                {
                    if (OpenForm() == 2)
                    {
                        StudetailPanel.Visible = false;
                        btnSubmit.Visible = false;
                    }
                    else
                    {
                        BindAssessmentTable("");
                        btnSubmit.Visible = false;
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
        SqlCommand cmd = new SqlCommand("select * from FacultyFeedback where StudentNo='" + Session["uid"].ToString() + "' and AcademicYear='" + Session["AcademicYear"].ToString() + "' and ([Semester]='" + Session["Semester"].ToString() + "' or [Semester]='" + Session["Year"].ToString() + "')", con);
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
        SqlCommand cmd12 = new SqlCommand("GetFormVisibility", con);
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
        string CollegeCode = Session["College"].ToString();
        string ProcName = "";
        if (CollegeCode == "TMDC" || CollegeCode == "TMMC") { ProcName = "SP_FacultyAssesmentSubjectForTMDCAndTMMC"; } else { ProcName = "SP_FacultyAssesmentSubject"; }
        SqlCommand cmd = new SqlCommand(ProcName, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        Session["FacultySubject"] = dt;
        TextBox tb;
        DropDownList dp;
        TableHeaderRow hr = new TableHeaderRow();
        int tab = 1;
        for (int i = 1; i < 14; i++)
        {
            TableRow tr = new TableRow();
            for (int j = 1; j < dt.Rows.Count + 3; j++)
            {

                TableCell tc = new TableCell();
                Label l = new Label();
                tb = new TextBox();
                string rowcol = (i - 1) + "_" + (j - 1);
                tb.ID = rowcol;
                tb.Width = 120;
                RequiredFieldValidator reqFieldVal = new RequiredFieldValidator();
                reqFieldVal.ID = "validator_" + j;
                reqFieldVal.ControlToValidate = tb.ID;
                reqFieldVal.ForeColor = System.Drawing.Color.Red;
                reqFieldVal.ErrorMessage = "**";
                reqFieldVal.Display = ValidatorDisplay.Dynamic;
                reqFieldVal.ValidationGroup = "R1";
                dp = new DropDownList();
                dp.TabIndex = (short)(tab + 1);
                dp.ForeColor = System.Drawing.Color.Blue;
                dp.CssClass = "form-control";
                dp.ID = rowcol;
                dp.Width = 70;
                dp.Items.Add("");
                dp.Items.Add(new ListItem("1", "1"));
                dp.Items.Add(new ListItem("2", "2"));
                dp.Items.Add(new ListItem("3", "3"));
                dp.Items.Add(new ListItem("4", "4"));
                dp.Items.Add(new ListItem("5", "5"));

                if (j == 1)
                {
                    if (i == 1)
                    {
                        l.Text = "Parameters";
                        tc.CssClass = "Bold";
                    }
                    if (i == 2)
                    {
                        tc.CssClass = "Bold";
                    }

                    if (i == 3)
                        l.Text = "Subject Knowledge";
                    if (i == 4)
                        l.Text = "Explanation Power";
                    if (i == 5)
                        l.Text = "Speed of Teaching";
                    if (i == 6)
                        l.Text = "Problem Solving Ability";
                    if (i == 7)
                        l.Text = "Punctuality in Class";
                    if (i == 8)
                        l.Text = "Participation in Class";
                    if (i == 9)
                        l.Text = "Presentation in Skills";
                    if (i == 10)
                        l.Text = "Quality of Assignments";
                    if (i == 11)
                        l.Text = "Understanding of the Content";
                    if (i == 12)
                        l.Text = "Comfort Level with Faculty";
                    if (i == 13)
                        l.Text = "Any Suggestions for further Improvements";
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
                {
                    if (i != 13)
                    {
                        tc.Controls.Add(dp);
                        tr.Cells.Add(tc);
                    }
                    else
                    {
                        tc.Controls.Add(tb);
                        tc.Controls.Add(reqFieldVal);
                        tr.Cells.Add(tc);
                    }
                }
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
                string str = dt.Rows[i - 2]["Faculty Subject"].ToString();
                string[] str1 = str.Split('=');
                string fcode = str1[1];
                string fname = str1[0];
                dr[0] = fcode;
                dr[1] = fname;
                dr[2] = dt.Rows[i - 2]["Subject Code"].ToString();

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
                TextBox s11 = (TextBox)FacultyAssessmentTable.Rows[12].Cells[i].FindControl(12 + "_" + i);
                dr[13] = s11.Text;
                if (s1.SelectedValue == "" || s2.SelectedValue == "" || s3.SelectedValue == "" || s4.SelectedValue == "" || s5.SelectedValue == "" || s6.SelectedValue == ""
                        || s7.SelectedValue == "" || s8.SelectedValue == "" || s9.SelectedValue == "" || s10.SelectedValue == "") { Result = "ERROR"; break; }

            }
            if (i > 1)
                dt21.Rows.Add(dr);
        }
        if (Result == "SUCCESS")
        {
            SqlCommand cmd12 = new SqlCommand("sp_insertFacultyFeedback", con);
            cmd12.CommandType = CommandType.StoredProcedure;
            cmd12.Parameters.Add("@StudentNo", Session["uid"].ToString());
            cmd12.Parameters.Add("@Course", Session["CourseCode"].ToString());
            if (Session["Semester"].ToString() == "") { cmd12.Parameters.Add("@Semester", Session["Year"].ToString()); }
            else { cmd12.Parameters.Add("@Semester", Session["Semester"].ToString()); }
            cmd12.Parameters.Add("@Section", Session["Section"].ToString());
            cmd12.Parameters.Add("@AcademicYear", Session["AcademicYear"].ToString());
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