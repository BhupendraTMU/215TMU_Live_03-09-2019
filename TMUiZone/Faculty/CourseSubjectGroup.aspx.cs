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

public partial class Faculty_CourseSubjectGroup : System.Web.UI.Page
{
    static string StuNo = "";
    DataTable dt = new DataTable();
    DL.StudentFineDL sdl = new DL.StudentFineDL();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ViewState["SubjectList"] = dt;
                pnlGrid.Visible = false;
                btnSave.Visible = false;
                txtSubjectGroup.Visible = false;
                chkAllow.Visible = false; lblAllow.Visible = false;
                bindAcademicYear();
                bindSubject();
                BindGrid();


            }
        }
        catch
        { Response.Redirect("~/Default.aspx"); }
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
    public void bindSubject()
    {
        string YearType = "";
        if (drpYearType.SelectedValue == "ODD")
            YearType = "Semester in ('I','III','V','VII','IX')";
        else if (drpYearType.SelectedValue == "EVEN")
            YearType = "Semester in ('II','IV','VI','VIII','X')";
        else
            YearType = "Year != ''";
        string SubjectClassification1 = "";
        SubjectClassification1 = drpSubjectClassification.SelectedValue == "PRACTICAL" ? " in ('PRACTICAL','LAB')" : " not in ('PRACTICAL','LAB')";

        SqlCommand cmd = new SqlCommand("select '' as Description1,'' as Code  union select distinct Description+' ('+Course+')' as Description1,Code from [TMU$Subject - COLLEGE] where Description!='' and Course!='' and Code!='' and   [Subject Classification] " + SubjectClassification1 + " and (" + YearType + ") and [Global Dimension 1 Code] = '" + Session["GlobalDimension1Code"].ToString() + "' and [Academic Year]=(select max([Academic Year]) from [TMU$Subject - COLLEGE] where [Global Dimension 1 Code] = '" + Session["GlobalDimension1Code"].ToString() + "' )", con);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        ddlSubject.DataSource = dt;
        ddlSubject.DataTextField = "Description1";
        ddlSubject.DataValueField = "Code";
        ddlSubject.DataBind();
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
            for (int i = 0; i < grdSunjectCourse.Columns.Count; i++)
            {
                dt.Columns.Add(grdSunjectCourse.HeaderRow.Cells[i].Text);
            }
            foreach (GridViewRow row in grdSunjectCourse.Rows)
            {
                var cb = (CheckBox)row.Cells[2].FindControl("chkSelect");
                DataRow dr = dt.NewRow();
                for (int j = 0; j < grdSunjectCourse.Columns.Count; j++)
                {
                    if (cb.Checked == true)
                        dr[grdSunjectCourse.HeaderRow.Cells[j].Text] = row.Cells[j].Text;
                }
                if (cb.Checked == true)
                    dt.Rows.Add(dr);
            }

            if (dt.Rows.Count != 0)
            {
                SqlCommand cmd2 = new SqlCommand("select * from [GroupSubject] where SubjectGroup='" + txtSubjectGroup.Text + "' and [Academic Year]='" + drpAcademicYear.SelectedValue + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da.Fill(dt2);
                int exist = 0;
                if (chkAllow.Checked == true) { exist = 0; } else { exist = dt2.Rows.Count; }
                if (exist <= 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        SqlCommand cmd = new SqlCommand("sp_insertGroupSubject", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CourseCode", dt.Rows[i]["Course"]);
                        cmd.Parameters.Add("@SemesterYear", dt.Rows[i]["Semester"]);
                        cmd.Parameters.Add("@SubjectCode", dt.Rows[i]["Subject Code"]);
                        cmd.Parameters.Add("@SubjectDescription", dt.Rows[i]["Description"]);
                        cmd.Parameters.Add("@SubjectGroup", txtSubjectGroup.Text);
                        cmd.Parameters.Add("@SubjectGroupCode", "SUBJGRP_" + txtSubjectGroup.Text);
                        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                        cmd.Parameters.Add("@Load", hdnValue.Value);
                        cmd.Parameters.AddWithValue("@AcademicYear", drpAcademicYear.SelectedValue);
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Save Successfully');", true);
                    pnlGrid.Visible = false;
                    btnSave.Visible = false;
                    txtSubjectGroup.Visible = false;
                    chkAllow.Visible = false; lblAllow.Visible = false; chkAllow.Checked = false;
                    txtSubjectGroup.Text = "";
                    BindGrid();
                    ((DataTable)ViewState["SubjectList"]).Clear();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'This Groupcode has been already exist');", true);
                }
            }
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Please select the Subject');", true);
        }
        else
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Please select the Subject');", true);

    }
    public void BindGrid()
    {
        SqlCommand cmd = new SqlCommand("select * from [GroupSubject] where CollegeCode='" + Session["GlobalDimension1Code"].ToString() + "'and [Academic Year]='" + drpAcademicYear.SelectedValue + "'", con);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        grdSubjectGroup.DataSource = dt;
        grdSubjectGroup.DataBind();
    }

    protected void grdSunjectCourse_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            GridViewRow rowItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int index = rowItem.RowIndex;

            GridViewRow row = grdSunjectCourse.Rows[index];
            string Enroll = row.Cells[1].Text;
            dt = (DataTable)ViewState["SubjectList"];
            DataRow[] result = dt.Select("[Code] = '" + Enroll + "'");
            foreach (DataRow row1 in result)
            {
                if (row1["Code"].ToString().Trim().ToUpper().Contains(Enroll))
                    dt.Rows.Remove(row1);
            }
            grdSunjectCourse.DataSource = dt;
            grdSunjectCourse.DataBind();
            if (dt.Rows.Count <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "fader", "$('[id$=pnlGrid]').hide(); ", true);
                txtSubjectGroup.Visible = false;
                chkAllow.Visible = false; lblAllow.Visible = false;
                btnSave.Visible = false;
            }
            if (((DataTable)ViewState["SubjectList"]).Rows.Count > 0)
            {
                drpSubjectClassification.Enabled = false;
                drpYearType.Enabled = false;
            }
            else
            {
                drpSubjectClassification.Enabled = true;
                drpYearType.Enabled = true;
            }
        }
    }
    protected void grdSubjectGroup_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        try
        {
            string SubjectGroupCode = grdSubjectGroup.DataKeys[e.RowIndex].Value.ToString();
            HiddenField hfId = (HiddenField)grdSubjectGroup.FindControl("hfId");
            SqlCommand cmd = new SqlCommand("sp_DeleteSubjectGroup", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SubjectGroupCode", SubjectGroupCode);
            cmd.Parameters.AddWithValue("@ID", hfId.Value);
            if (con.State == ConnectionState.Closed)
                con.Open();
            string res = cmd.ExecuteScalar().ToString();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', '" + res + "');", true);
            BindGrid();
        }
        catch (Exception ex) { }
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubject.SelectedValue != "")
        {
            string Sub = ddlSubject.SelectedItem.Text;
            string SubDescription = ""; string Course1 = "";
            if (Sub.Contains("(") && Sub.Contains(")"))
                Course1 = Sub.Split('(', ')')[1];
            SubDescription = Sub.Substring(0, Sub.IndexOf('('));

            SqlCommand cmd2 = new SqlCommand("select top 1 Load from [TMU$Subject - COLLEGE] where Code='" + ddlSubject.SelectedValue + "' and [Academic Year]=(select max([Academic Year]) from [TMU$Subject - COLLEGE] where [Global Dimension 1 Code] = '" + Session["GlobalDimension1Code"].ToString() + "' )", con);
            con.Open();
            SqlDataReader dr = cmd2.ExecuteReader();
            dr.Read();
            int Load = int.Parse(dr["Load"].ToString());
            con.Close();
            if (Load == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Load Assign On Desgination');", true);
            }
            else
            {
                DataTable dt31 = (DataTable)ViewState["SubjectList"];
                bool exists = dt31.AsEnumerable().Where(c => c.Field<string>("Course").Equals(Course1) && c.Field<string>("Description").Equals(SubDescription.Trim()) && c.Field<string>("Code").Equals(ddlSubject.SelectedValue)).Count() > 0;
                if (exists == false)
                {
                    SqlCommand cmd = new SqlCommand("select * from [TMU$Subject - COLLEGE]  where Code ='" + ddlSubject.SelectedValue + "' and [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "' and [Academic Year]=(select max([Academic Year]) from [TMU$Subject - COLLEGE] where [Global Dimension 1 Code] = '" + Session["GlobalDimension1Code"].ToString() + "' )", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt1 = new DataTable();
                    da.Fill(dt1);

                    if (dt1.Rows.Count > 0)
                    {
                        dt1.Merge(dt31);
                        hdnValue.Value = dt1.Rows[0]["Load"].ToString();
                    }
                    ViewState["SubjectList"] = dt1;
                    grdSunjectCourse.DataSource = dt1;
                    grdSunjectCourse.DataBind();
                    pnlGrid.Visible = true;
                    btnSave.Visible = true;
                    txtSubjectGroup.Visible = true;
                    chkAllow.Visible = true; lblAllow.Visible = true;
                    if (((DataTable)ViewState["SubjectList"]).Rows.Count > 0)
                    {
                        drpSubjectClassification.Enabled = false;
                        drpYearType.Enabled = false;
                    }
                    else
                    {
                        drpSubjectClassification.Enabled = true;
                        drpYearType.Enabled = true;
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Can not add duplicate entry');", true);
                }
            }
        }
        bindSubject();
    }
    protected void drpSubjectClassification_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSubject();
    }
    protected void drpYearType_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSubject();
    }

    protected void LnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int i = Convert.ToInt32(row.RowIndex);

            Label SubjectGroupCode = (Label)grdSubjectGroup.Rows[i].FindControl("SubjectGroupCode");
            HiddenField hfId = (HiddenField)grdSubjectGroup.Rows[i].FindControl("hfId");
            SqlCommand cmd = new SqlCommand("sp_DeleteSubjectGroup", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SubjectGroupCode", SubjectGroupCode.Text);
            cmd.Parameters.AddWithValue("@ID", hfId.Value);
            if (con.State == ConnectionState.Closed)
                con.Open();
            string res = cmd.ExecuteScalar().ToString();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', '" + res + "');", true);
            BindGrid();
        }
        catch (Exception ex) { }
    }

    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
}
