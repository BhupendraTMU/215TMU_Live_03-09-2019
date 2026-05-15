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


public partial class StudentFeedbackSyllabus : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString);
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {
                SqlCommand cmd = new SqlCommand("select * from SyllabusFeedback where StudentNo='" + Session["uid"].ToString() + "' and AcademicYear='" + Session["AcademicYear"].ToString() + "' and ([Semester]='" + Session["Semester"].ToString() + "' or [Semester]='" + Session["Year"].ToString() + "')", con);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Student.Visible = false;
                    pnlError.Visible = true;
                }
                else
                {
                    Student.Visible = true;
                    pnlError.Visible = false;
                    BindAssessmentTable("");
                    getSubject();
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
    public void getSubject()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Get_SubjectForFeedback", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                lblCLG.Text = dt.Rows[0]["CollegeCode"].ToString();
                lblPRG.Text = dt.Rows[0]["Course Code"].ToString();
                lblSession.Text = dt.Rows[0]["Semester"].ToString();
                grdCourse.DataSource = dt;
                grdCourse.DataBind();
            }



        }
        catch (Exception ex)
        {

        }
    }
    public void chkrecord()
    {
        con.Close();
        SqlCommand cmd = new SqlCommand("select * from SyllabusFeedback where StudentNo='" + Session["uid"].ToString() + "' and AcademicYear='" + Session["AcademicYear"].ToString() + "' and ([Semester]='" + Session["Semester"].ToString() + "' or [Semester]='" + Session["Year"].ToString() + "')", con);
        con.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            con.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Feed Back Already submitted !');", true);
            btnSubmit.Visible = false;
            return;
        }
        else
        {
            con.Close();
            btnSubmit.Visible = true;
            BindAssessmentTable("");
        }
        con.Close();
    }


    public void BindAssessmentTable(string d)
    {
        string CollegeCode = Session["College"].ToString();
        string ProcName = "";
        ProcName = "Get_FeedbackQuestion";
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
        for (int i = 1; i < 10; i++)
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
                dp.TabIndex = (short)(tab + 1);
                dp.ForeColor = System.Drawing.Color.Blue;
                dp.CssClass = "form-control";
                dp.ID = rowcol;
                dp.Width = 70;
                dp.Items.Add(new ListItem("0", "0"));
                dp.Items.Add(new ListItem("1", "1"));
                dp.Items.Add(new ListItem("2", "2"));
                dp.Items.Add(new ListItem("3", "3"));
                dp.Items.Add(new ListItem("4", "4"));
                dp.Items.Add(new ListItem("5", "5"));
                dp.Items.Add(new ListItem("6", "6"));
                dp.Items.Add(new ListItem("7", "7"));
                dp.Items.Add(new ListItem("8", "8"));
                dp.Items.Add(new ListItem("9", "9"));
                dp.Items.Add(new ListItem("10", "10"));

                if (j == 2)
                {

                    if (i == 1)
                    {
                        l.Text = "Parameters";
                        tc.CssClass = "Bold";
                        l.Width = 200;


                    }

                    if (i == 2)
                        l.Text = "Depth of the course content including project  work if any";
                    if (i == 3)
                        l.Text = "Extent of coverage of course";
                    if (i == 4)
                        l.Text = "Applicability / Relevance to real life situations";
                    if (i == 5)
                        l.Text = "Learning value (in terms of knowledge,  concepts, manual skills, analytical abilities  and broadening perspectives)";
                    if (i == 6)
                        l.Text = "Clarity and relevance of textual reading  material";
                    if (i == 7)
                        l.Text = "Relevance of additional source material  (Library)";
                    if (i == 8)
                        l.Text = "Extent of efforts required by students";
                    if (i == 9)
                        l.Text = "Overall rating";

                    l.ID = rowcol;
                    tc.Controls.Add(l);
                    tr.Cells.Add(tc);
                }
                else
                    if (j == 1)
                    {

                        if (i == 1)
                        {
                            l.Text = "S No";
                            tc.CssClass = "Bold";
                        }
                        else
                        {
                            l.Text = (i - 1).ToString();
                        }

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
                    //FacultyAssessmentTable.Rows[i + 1].Cells[j].Text = dt.Rows[j - 2]["Faculty Name"].ToString();
                    FacultyAssessmentTable.Rows[i].Cells[j].CssClass = "Bold";
                    FacultyAssessmentTable.Rows[i + 1].Cells[j].CssClass = "Bold";
                }
            }
        }
        DataTable dtnew = new DataTable();




        if (dt.Rows.Count <= 0)
            btnSubmit.Visible = false;

    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        String Result = "SUCCESS";
        DataTable dt = (DataTable)Session["FacultySubject"];
        DataTable dt21 = new DataTable();
        for (int i = 0; i <= 25; i++)
        {
            //if (i == 0)
            //    dt21.Columns.Add("Faculty Code");
            //else if (i == 1)
            //    dt21.Columns.Add("Faculty Name");
            if (i == 0)
                dt21.Columns.Add("Question");
            else
                dt21.Columns.Add("Ans" + (i - 1).ToString());
        }

        for (int i = 0; i < FacultyAssessmentTable.Rows.Count; i++)
        {
            DataRow dr = dt21.NewRow();


            if (i > 0)
            {
                for (int j = 0; j <= 25; j++)
                {
                    int row = 1;
                     if (i < FacultyAssessmentTable.Rows.Count)
                    {
                        Label Question = (Label)FacultyAssessmentTable.Rows[1].Cells[0].FindControl(i + "_" + row);

                        dr[row - 1] = Question.Text;
                    }

                    for (int k = 2; k <= 25; k++)
                    {
                        if (k <= dt.Rows.Count + 1 && j <= dt.Rows.Count)
                        {
                            DropDownList s = (DropDownList)FacultyAssessmentTable.Rows[i].Cells[j].FindControl(i + "_" + k);

                            dr[k - 1] = dt.Rows[k - 2]["Subject Code"].ToString() + "-" + s.SelectedValue;
                        }


                    }


                }

            }
            if (i > 0)
                dt21.Rows.Add(dr);
        }
        if (Result == "SUCCESS")
        {
            SqlCommand cmd12 = new SqlCommand("sp_insertSyllabusFeedback", con);
            cmd12.CommandType = CommandType.StoredProcedure;
            cmd12.Parameters.Add("@StudentNo", Session["uid"].ToString());
            cmd12.Parameters.Add("@Course", Session["CourseCode"].ToString());
            if (Session["Semester"].ToString() == "") { cmd12.Parameters.Add("@Semester", Session["Year"].ToString()); }
            else { cmd12.Parameters.Add("@Semester", Session["Semester"].ToString()); }
            cmd12.Parameters.Add("@Section", Session["Section"].ToString());
            cmd12.Parameters.Add("@AcademicYear", Session["AcademicYear"].ToString());
            cmd12.Parameters.Add("@CollegeCode", Session["College"].ToString());
            cmd12.Parameters.Add("@FurtherSuggest", lblSuggestion.Text);
            cmd12.Parameters.Add("@SyllabusFeedbackDetails", dt21);
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