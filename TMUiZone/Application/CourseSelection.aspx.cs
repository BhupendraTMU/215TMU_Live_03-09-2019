using AjaxControlToolkit;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Windows.Input;


public partial class Application_CourseSelection : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {
                BindStream();
                BindMULTIStream();
                bindCoreCourse(Session["uid"].ToString());
                bindSelectedData(Session["uid"].ToString());
            }
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }

    }
    public void bindSelectedData(string StudentNo_)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_FetchSelectedDataStudentBise", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@StudentNo_", StudentNo_);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            con.Open();
            da.Fill(ds);
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                drpStreamMajor.SelectedValue = ds.Tables[0].Rows[0]["Stream"].ToString();
                grdMajor.DataSource = ds.Tables[0];
                grdMajor.DataBind();
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                drpchkMinor.SelectedValue = ds.Tables[1].Rows[0]["Stream"].ToString();
                GrdMinor.DataSource = ds.Tables[1];
                GrdMinor.DataBind();
            }
            if (ds.Tables[2].Rows.Count > 0)
            {
                grdCoreCourse.DataSource = ds.Tables[2];
                grdCoreCourse.DataBind();
            }
            if (ds.Tables[3].Rows.Count > 0)
            {
                chkmulti.SelectedValue = ds.Tables[3].Rows[0]["Multi D Stream"].ToString();
                grdMultiDesc.DataSource = ds.Tables[3];
                grdMultiDesc.DataBind();
            }



        }
        catch (Exception ex)
        {

        }
    }
    public void bindCoreCourse(string StudentNo_)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_FetchAllSubject", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@StudentNo_", StudentNo_);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                grdCoreCourse.DataSource = dt;
                grdCoreCourse.DataBind();
            }
            else
            {
                grdCoreCourse.DataSource = "";
                grdCoreCourse.DataBind();
            }




        }
        catch (Exception ex)
        {

        }
    }
    public void BindStream()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        string strSQL = " select * from [TMU$Course Specialization] where [Course Code]= '" + Session["CourseCode"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            drpchkMinor.DataSource = dt;
            drpchkMinor.DataValueField = "Code";
            drpchkMinor.DataTextField = "Description";

            drpchkMinor.DataBind();
            drpStreamMajor.DataSource = dt;
            drpStreamMajor.DataValueField = "Code";
            drpStreamMajor.DataTextField = "Description";

            drpStreamMajor.DataBind();



        }
    }

    public void BindMULTIStream()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        string strSQL = " select * from [TMU$Multi Dis  Specialization] where [College Code]!= '" + Session["College"].ToString() + "'  ";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            chkmulti.DataSource = dt;
            chkmulti.DataValueField = "Code";
            chkmulti.DataTextField = "Description";
            chkmulti.DataBind();




        }
    }

    protected void drpStreamMajo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpStreamMajor.SelectedItem != null)
        {

            string Selected = drpStreamMajor.SelectedItem.Value.ToString();
            for (int i = 0; i < drpStreamMajor.Items.Count; i++)
            {
                //if (drpStreamMajor.Items[i].Selected == true)
                //{
                //    count++;
                //    itemselect = drpStreamMajor.Items[i].Value.ToString();
                //}
                //if (count > 1)
                //{
                drpStreamMajor.Items[i].Selected = false;
                //}
            }


            string Minoritemselect = "";
            for (int i = 0; i < drpchkMinor.Items.Count; i++)
            {
                if (drpchkMinor.Items[i].Selected == true)
                {
                    Minoritemselect = drpchkMinor.Items[i].Value.ToString();

                }

            }
            if (Minoritemselect != Selected)
            {
                drpStreamMajor.SelectedValue = Selected;
                bindMajorSubject(Selected, Session["uid"].ToString());
            }
            else
            {
                grdMajor.DataSource = "";
                grdMajor.DataBind();
            }





        }
        else
        {
            grdMajor.DataSource = "";
            grdMajor.DataBind();
        }



    }
    public void bindMajorSubject(string Stream, string StudentNo_)
    {

        try
        {
            SqlCommand cmd = new SqlCommand("Sp_FetchMajorSubject", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@StudentNo_", StudentNo_);
            cmd.Parameters.AddWithValue("@Stream", Stream);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                grdMajor.DataSource = dt;
                grdMajor.DataBind();
            }
            else
            {
                grdMajor.DataSource = "";
                grdMajor.DataBind();
            }




        }
        catch (Exception ex)
        {

        }


    }
    public void bindMultiSubject(string Stream, string StudentNo_)
    {

        try
        {
            SqlCommand cmd = new SqlCommand("Sp_FetchMultiSubject", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@StudentNo_", StudentNo_);
            cmd.Parameters.AddWithValue("@Stream", Stream);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                grdMultiDesc.DataSource = dt;
                grdMultiDesc.DataBind();
            }
            else
            {
                grdMultiDesc.DataSource = "";
                grdMultiDesc.DataBind();
            }




        }
        catch (Exception ex)
        {

        }


    }

    public void bindMinorSubject(string Stream, string StudentNo_)
    {

        try
        {
            SqlCommand cmd = new SqlCommand("Sp_FetchMinorSubject", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@StudentNo_", StudentNo_);
            cmd.Parameters.AddWithValue("@Stream", Stream);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                GrdMinor.DataSource = dt;
                GrdMinor.DataBind();
            }
            else
            {
                GrdMinor.DataSource = "";
                GrdMinor.DataBind();
            }




        }
        catch (Exception ex)
        {

        }


    }

    protected void drpchkMinor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpchkMinor.SelectedItem != null)
        {

            string Selected = drpchkMinor.SelectedItem.Value.ToString();
            for (int i = 0; i < drpchkMinor.Items.Count; i++)
            {

                drpchkMinor.Items[i].Selected = false;

            }

            string Mijoritemselect = "";
            for (int i = 0; i < drpStreamMajor.Items.Count; i++)
            {
                if (drpStreamMajor.Items[i].Selected == true)
                {
                    Mijoritemselect = drpStreamMajor.Items[i].Value.ToString();

                }

            }
            if (Mijoritemselect != Selected)
            {
                drpchkMinor.SelectedValue = Selected;
                bindMinorSubject(Selected, Session["uid"].ToString());
            }
            else
            {
                GrdMinor.DataSource = "";
                GrdMinor.DataBind();
            }
        }
        else
        {
            GrdMinor.DataSource = "";
            GrdMinor.DataBind();
        }
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string Mijoritemselect = "";
        for (int i = 0; i < drpStreamMajor.Items.Count; i++)
        {
            if (drpStreamMajor.Items[i].Selected == true)
            {
                Mijoritemselect = drpStreamMajor.Items[i].Value.ToString();

            }

        }

        if (Mijoritemselect == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('Please Select Major Course')", true);
            return;
        }


        string Minoritemselect = "";
        for (int i = 0; i < drpchkMinor.Items.Count; i++)
        {
            if (drpchkMinor.Items[i].Selected == true)
            {
                Minoritemselect = drpchkMinor.Items[i].Value.ToString();

            }

        }
        if (Minoritemselect == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('Please Select Minor Course')", true);
            return;
        }
        string Multiitemselect = "";
        for (int i = 0; i < chkmulti.Items.Count; i++)
        {
            if (chkmulti.Items[i].Selected == true)
            {
                Multiitemselect = chkmulti.Items[i].Value.ToString();

            }

        }
        if (Multiitemselect == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('Please Select MULTI DISCIPLINARY COURSE')", true);
            return;
        }
        DataTable StudentSubjectCode = new DataTable();
        StudentSubjectCode.Clear();
        StudentSubjectCode.Columns.Add("StudentNo_");
        StudentSubjectCode.Columns.Add("CourseCode");
        StudentSubjectCode.Columns.Add("Semester");
        StudentSubjectCode.Columns.Add("Year");
        StudentSubjectCode.Columns.Add("AcademicYear");
        StudentSubjectCode.Columns.Add("SubjectCode");
        StudentSubjectCode.Columns.Add("Stream");
        StudentSubjectCode.Columns.Add("SubjectType");
        StudentSubjectCode.Columns.Add("CreatedBy");
        StudentSubjectCode.Columns.Add("MultiDStream");

        if (grdMajor.Rows.Count > 0)
        {
            foreach (GridViewRow row in grdMajor.Rows)
            {
                DataRow _DataLine = StudentSubjectCode.NewRow();
                if (row.RowType == DataControlRowType.DataRow)
                {

                    Label LblProgramCode = (Label)row.FindControl("LblProgramCode");
                    Label LblPracticalCode = (Label)row.FindControl("LblPracticalCode");
                    Label LblSemester = (Label)row.FindControl("LblSemester");
                    HiddenField Hftypeofcourse = (HiddenField)row.FindControl("Hftypeofcourse");
                    HiddenField HfACYear = (HiddenField)row.FindControl("HfACYear");

                    _DataLine["StudentNo_"] = Session["uid"].ToString();
                    _DataLine["CourseCode"] = LblProgramCode.Text;

                    if (Hftypeofcourse.Value == "1")
                    {
                        _DataLine["Semester"] = LblSemester.Text;
                        _DataLine["Year"] = "";
                    }
                    else
                    {
                        _DataLine["Semester"] = "";
                        _DataLine["Year"] = LblSemester.Text;
                    }
                    _DataLine["AcademicYear"] = HfACYear.Value;
                    _DataLine["SubjectCode"] = LblPracticalCode.Text;
                    _DataLine["Stream"] = Mijoritemselect;
                    _DataLine["SubjectType"] = "1";
                    _DataLine["CreatedBy"] = Session["uid"].ToString();
                    _DataLine["MultiDStream"] = "";
                }
                StudentSubjectCode.Rows.Add(_DataLine);
            }

            foreach (GridViewRow row in GrdMinor.Rows)
            {
                DataRow _DataLine = StudentSubjectCode.NewRow();
                if (row.RowType == DataControlRowType.DataRow)
                {

                    Label LblProgramCode = (Label)row.FindControl("LblProgramCode");
                    Label LblPracticalCode = (Label)row.FindControl("LblPracticalCode");
                    Label LblSemester = (Label)row.FindControl("LblSemester");
                    HiddenField Hftypeofcourse = (HiddenField)row.FindControl("Hftypeofcourse");
                    HiddenField HfACYear = (HiddenField)row.FindControl("HfACYear");

                    _DataLine["StudentNo_"] = Session["uid"].ToString();
                    _DataLine["CourseCode"] = LblProgramCode.Text;

                    if (Hftypeofcourse.Value == "1")
                    {
                        _DataLine["Semester"] = LblSemester.Text;
                        _DataLine["Year"] = "";
                    }
                    else
                    {
                        _DataLine["Semester"] = "";
                        _DataLine["Year"] = LblSemester.Text;
                    }
                    _DataLine["AcademicYear"] = HfACYear.Value;
                    _DataLine["SubjectCode"] = LblPracticalCode.Text;
                    _DataLine["Stream"] = Minoritemselect;
                    _DataLine["SubjectType"] = "2";
                    _DataLine["CreatedBy"] = Session["uid"].ToString();
                    _DataLine["MultiDStream"] = "";
                }
                StudentSubjectCode.Rows.Add(_DataLine);
            }
            foreach (GridViewRow row in grdCoreCourse.Rows)
            {
                DataRow _DataLine = StudentSubjectCode.NewRow();
                if (row.RowType == DataControlRowType.DataRow)
                {

                    Label LblProgramCode = (Label)row.FindControl("LblProgramCode");
                    Label LblPracticalCode = (Label)row.FindControl("LblPracticalCode");
                    Label LblSemester = (Label)row.FindControl("LblSemester");
                    HiddenField Hftypeofcourse = (HiddenField)row.FindControl("Hftypeofcourse");
                    HiddenField HfACYear = (HiddenField)row.FindControl("HfACYear");

                    _DataLine["StudentNo_"] = Session["uid"].ToString();
                    _DataLine["CourseCode"] = LblProgramCode.Text;

                    if (Hftypeofcourse.Value == "1")
                    {
                        _DataLine["Semester"] = LblSemester.Text;
                        _DataLine["Year"] = "";
                    }
                    else
                    {
                        _DataLine["Semester"] = "";
                        _DataLine["Year"] = LblSemester.Text;
                    }
                    _DataLine["AcademicYear"] = HfACYear.Value;
                    _DataLine["SubjectCode"] = LblPracticalCode.Text;
                    _DataLine["Stream"] = "";
                    _DataLine["SubjectType"] = "";
                    _DataLine["CreatedBy"] = Session["uid"].ToString();
                    _DataLine["MultiDStream"] = "";
                }
                StudentSubjectCode.Rows.Add(_DataLine);
            }

            foreach (GridViewRow row in grdMultiDesc.Rows)
            {
                DataRow _DataLine = StudentSubjectCode.NewRow();
                if (row.RowType == DataControlRowType.DataRow)
                {

                    Label LblProgramCode = (Label)row.FindControl("LblProgramCode");
                    Label LblPracticalCode = (Label)row.FindControl("LblPracticalCode");
                    Label LblSemester = (Label)row.FindControl("LblSemester");
                    HiddenField Hftypeofcourse = (HiddenField)row.FindControl("Hftypeofcourse");
                    HiddenField HfACYear = (HiddenField)row.FindControl("HfACYear");

                    _DataLine["StudentNo_"] = Session["uid"].ToString();
                    _DataLine["CourseCode"] = LblProgramCode.Text;

                    if (Hftypeofcourse.Value == "1")
                    {
                        _DataLine["Semester"] = LblSemester.Text;
                        _DataLine["Year"] = "";
                    }
                    else
                    {
                        _DataLine["Semester"] = "";
                        _DataLine["Year"] = LblSemester.Text;
                    }
                    _DataLine["AcademicYear"] = HfACYear.Value;
                    _DataLine["SubjectCode"] = LblPracticalCode.Text;
                    _DataLine["Stream"] = "";
                    _DataLine["SubjectType"] = "3";
                    _DataLine["CreatedBy"] = Session["uid"].ToString();
                    _DataLine["MultiDStream"] = Multiitemselect;

                }
                StudentSubjectCode.Rows.Add(_DataLine);
            }
            SqlCommand cmd = new SqlCommand("sp_InsertChooseSubject", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dtfinal", StudentSubjectCode);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Data Submitted Successfully');", true);






        }
    }

    protected void chkmulti_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (chkmulti.SelectedItem != null)
        {

            string Selected = chkmulti.SelectedItem.Value.ToString();
            for (int i = 0; i < chkmulti.Items.Count; i++)
            {

                chkmulti.Items[i].Selected = false;

            }

            string Multiitemselect = "";
            for (int i = 0; i < chkmulti.Items.Count; i++)
            {
                if (chkmulti.Items[i].Selected == true)
                {
                    Multiitemselect = chkmulti.Items[i].Value.ToString();

                }

            }
            if (Multiitemselect != Selected)
            {
                chkmulti.SelectedValue = Selected;
                bindMultiSubject(Selected, Session["uid"].ToString());
            }
            else
            {
                grdMultiDesc.DataSource = "";
                grdMultiDesc.DataBind();
            }
        }
        else
        {
            grdMultiDesc.DataSource = "";
            grdMultiDesc.DataBind();
        }
    }
}