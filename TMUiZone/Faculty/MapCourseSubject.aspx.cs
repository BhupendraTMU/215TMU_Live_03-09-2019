using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Faculty_MapCourseSubject : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            bindCourse();
            BindGrid();
        }
    }

    public void bindCourse()
    {
        SqlCommand cmd = new SqlCommand("select Code,Description from [TMU$Course - COLLEGE]  where [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "' and  Description!=''", con);
        con.Open();
        lstCourse.DataSource = cmd.ExecuteReader();
        lstCourse.DataTextField = "Description";
        lstCourse.DataValueField = "Code";
        lstCourse.DataBind();
        con.Close();
    }

    public void bindSubjects()
    {
        string CourseCode = "";
        foreach (ListItem item in lstCourse.Items)
        {
            if (item.Selected)
            {
                if (CourseCode == "")
                    CourseCode = "'" + item.Value + "'";
                else
                    CourseCode = CourseCode + ",'" + item.Value + "'";
            }
        }
        if (CourseCode != "")
        {
            SqlCommand cmd = new SqlCommand("select * from [TMU$Subject - COLLEGE] where Course in (" + CourseCode + ")", con);
            con.Open();
            grdSubjectDetails.DataSource = cmd.ExecuteReader();
            grdSubjectDetails.DataBind();
            con.Close();
            pnlGrid.Visible = true;
            btnSave.Visible = true;
            txtSubjectGroup.Visible = true;
        }
        else
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Please select the courses');", true);
    }

    protected void btnGetSubject_Click(object sender, ImageClickEventArgs e)
    {

        bindSubjects();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        BindTable();
    }

    public void BindTable()
    {
        if (pnlGrid.Visible == true)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < grdSubjectDetails.Columns.Count; i++)
            {
                dt.Columns.Add(grdSubjectDetails.HeaderRow.Cells[i].Text);

            }
            foreach (GridViewRow row in grdSubjectDetails.Rows)
            {
                var cb = (CheckBox)row.Cells[2].FindControl("chkboxSelectSubject");
                DataRow dr = dt.NewRow();
                for (int j = 0; j < grdSubjectDetails.Columns.Count; j++)
                {
                    if (cb.Checked == true)
                        dr[grdSubjectDetails.HeaderRow.Cells[j].Text] = row.Cells[j].Text;
                }
                if (cb.Checked == true)
                    dt.Rows.Add(dr);
            }

            if (dt.Rows.Count != 0)
            {

                SqlCommand cmd2 = new SqlCommand("select * from [GroupSubject] where SubjectGroup='" + txtSubjectGroup.Text + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da.Fill(dt2);
                if (dt2.Rows.Count <= 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        SqlCommand cmd = new SqlCommand("sp_insertGroupSubject", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CourseCode", dt.Rows[i]["Course"]);
                        cmd.Parameters.Add("@SemesterYear", "");
                        cmd.Parameters.Add("@SubjectCode", dt.Rows[i]["Subject Code"]);
                        cmd.Parameters.Add("@SubjectDescription", dt.Rows[i]["Subject Description"]);
                        cmd.Parameters.Add("@SubjectGroup", txtSubjectGroup.Text);
                        cmd.Parameters.Add("@SubjectGroupCode", "SubjGrp_" + txtSubjectGroup.Text);
                        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Save Successfully');", true);
                    pnlGrid.Visible = false;
                    btnSave.Visible = false;
                    txtSubjectGroup.Visible = false;
                    txtSubjectGroup.Text = "";
                    BindGrid();
                }
                else
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'This Groupcode has been already exist');", true);

            }
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Please select the Subject');", true);
        }
        else
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Please select the Subject');", true);

    }

    public void BindGrid()
    {
        SqlCommand cmd = new SqlCommand("select * from [GroupSubject] where CollegeCode='" + Session["GlobalDimension1Code"].ToString() + "'", con);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        grdSubjectGroup.DataSource = dt;
        grdSubjectGroup.DataBind();
    }
}