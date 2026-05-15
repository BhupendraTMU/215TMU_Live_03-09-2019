using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_Co_PO_Mapping : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                bindDrpCourseList();
                bindDrpSemesterList();
                // bindCOPOType();

            }
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }
    }
    //public void bindCOPOType()
    //{
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand("proc_GetSemester_Role", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.CommandTimeout = 1000000;
    //        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
    //        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
    //        DataTable dt = new DataTable();
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        con.Open();
    //        da.Fill(dt);
    //        con.Close();
    //        drpType.DataSource = dt;
    //        drpType.DataTextField = "Details";
    //        drpType.DataValueField = "No_";
    //        drpType.DataBind();

    //    }
    //    catch (Exception ex) { }
    //}



    public void bindDrpSemesterList()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetSemester_Role", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1000000;
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(dt);
            con.Close();
            drpSemester.DataSource = dt;
            drpSemester.DataTextField = "Details";
            drpSemester.DataValueField = "No_";
            drpSemester.DataBind();
            if (Session["drpSemester"].ToString() != null)
            {
                drpSemester.SelectedValue = Session["drpSemester"].ToString();
            }
            else { drpSemester.SelectedValue = "--Semester--"; }
        }
        catch (Exception ex) { }
    }
    public void bindDrpCourseList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetCourseFromCourseWiseFacultyCollege_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Open();
        da.Fill(dt);
        con.Close();
        drpCourse.DataSource = dt;
        drpCourse.DataTextField = "Details";
        drpCourse.DataValueField = "No_";
        drpCourse.DataBind();
    }


    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpSemesterList();
    }
    protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSubject();
    }
    public void bindSubject()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("sp_getSubjrctCodeFacultyWise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@AcademicYear", Session["AcademicYear"].ToString());
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedItem.Value.ToString());
            cmd.Parameters.Add("@Semester", drpSemester.SelectedItem.Value.ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            drpSubject.DataSource = dt;

            drpSubject.DataTextField = "Description";
            drpSubject.DataValueField = "Subject Code";
            drpSubject.DataBind();
            drpSubject.Items.Insert(0, "--Course--");
            if (Session["Subject"].ToString() != null)
            {
                drpSubject.SelectedValue = Session["Subject"].ToString();
            }
            else { drpSubject.SelectedValue = "--Course--"; }
        }
        catch (Exception ex) { }
    }

   
    public void bindCOPODetails()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("sp_getCOPOMappingDetail_New", con);
            cmd.CommandType = CommandType.StoredProcedure;
           
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedItem.Value.ToString());
            cmd.Parameters.Add("@Semester", drpSemester.SelectedItem.Value.ToString());
            cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
           


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridCOPODetails.DataSource = dt;
                GridCOPODetails.DataBind();
                GridView1.DataSource = dt;
                GridView1.DataBind();
                if (dt.Rows[0]["Locked"].ToString()=="1")
                {
                    btnSave.Visible = false;
                    btnSubmit.Visible = false;
                }
                else
                {
                    btnSave.Visible = true;
                    btnSubmit.Visible = true;
                }
               
            }



        }
        catch (Exception ex) { }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //SqlCommand cmd1 = new SqlCommand("proc_fetchCOPO", con);
        //cmd1.CommandType = CommandType.StoredProcedure;
        //cmd1.Parameters.AddWithValue("@CourseCode", drpCourse.SelectedValue);
        ////cmd1.Parameters.AddWithValue("@Type", drpCo.SelectedValue);
        ////cmd1.Parameters.AddWithValue("@Type2", drpPO.SelectedItem.Text);
        //cmd1.Parameters.AddWithValue("@SubjectCode", drpSubject.SelectedValue);

        //if (con.State == ConnectionState.Closed)
        //    con.Open();

        //SqlDataAdapter da = new SqlDataAdapter(cmd1);
        //DataTable dt1 = new DataTable();
        //con.Close();
        //da.Fill(dt1);
        //if (Convert.ToInt32(dt1.Rows[0]["c"]) > 0)
        //{

        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Mapping Already Exit.')", true);
        //    return;
        //}
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add("OUTCOME");
        dt.Columns.Add("PO-1");
        dt.Columns.Add("PO-2");
        dt.Columns.Add("PO-3");
        dt.Columns.Add("PO-4");
        dt.Columns.Add("PO-5");
        dt.Columns.Add("PO-6");
        dt.Columns.Add("PO-7");
        dt.Columns.Add("PO-8");
        dt.Columns.Add("PO-9");
        dt.Columns.Add("PO-10");
        dt.Columns.Add("PO-11");
        dt.Columns.Add("PO-12");
        dt.Columns.Add("PSO-1");
        dt.Columns.Add("PSO-2");
        dt.Columns.Add("PSO-3");
        foreach (GridViewRow row in GridCOPODetails.Rows)
        {
            DataRow dr = dt.NewRow();
            if (row.RowType == DataControlRowType.DataRow)
            {
                Label lblOutCome = (Label)row.FindControl("lblOutCome");
                DropDownList lblPO1 = (DropDownList)row.FindControl("lblPO1");
                DropDownList lblPO2 = (DropDownList)row.FindControl("lblPO2");
                DropDownList lblPO3 = (DropDownList)row.FindControl("lblPO3");
                DropDownList lblPO4 = (DropDownList)row.FindControl("lblPO4");
                DropDownList lblPO5 = (DropDownList)row.FindControl("lblPO5");
                DropDownList lblPO6 = (DropDownList)row.FindControl("lblPO6");
                DropDownList lblPO7 = (DropDownList)row.FindControl("lblPO7");
                DropDownList lblPO8 = (DropDownList)row.FindControl("lblPO8");
                DropDownList lblPO9 = (DropDownList)row.FindControl("lblPO9");
                DropDownList lblPO10 = (DropDownList)row.FindControl("lblPO10");
                DropDownList lblPO11 = (DropDownList)row.FindControl("lblPO11");
                DropDownList lblPO12 = (DropDownList)row.FindControl("lblPO12");
                DropDownList lblPSO1 = (DropDownList)row.FindControl("lblPSO1");
                DropDownList lblPSO2 = (DropDownList)row.FindControl("lblPSO2");
                DropDownList lblPSO3 = (DropDownList)row.FindControl("lblPSO3");


                dr["OUTCOME"] = lblOutCome.Text;
                dr["PO-1"] = lblPO1.SelectedValue;
                dr["PO-2"] = lblPO2.SelectedValue;
                dr["PO-3"] = lblPO3.SelectedValue;
                dr["PO-4"] = lblPO4.SelectedValue;
                dr["PO-5"] = lblPO5.SelectedValue;
                dr["PO-6"] = lblPO6.SelectedValue;
                dr["PO-7"] = lblPO7.SelectedValue;
                dr["PO-8"] = lblPO8.SelectedValue;
                dr["PO-9"] = lblPO9.SelectedValue;
                dr["PO-10"] = lblPO10.SelectedValue;
                dr["PO-11"] = lblPO11.SelectedValue;
                dr["PO-12"] = lblPO12.SelectedValue;
                dr["PSO-1"] = lblPSO1.SelectedValue;
                dr["PSO-2"] = lblPSO2.SelectedValue;
                dr["PSO-3"] = lblPSO3.SelectedValue;
                dt.Rows.Add(dr);
            }
        }
        SqlCommand cmd = new SqlCommand("Insert_CO_PO_Mapping", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Coded"].ToString());
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedItem.Value.ToString());
        cmd.Parameters.Add("@Semester", drpSemester.SelectedItem.Value.ToString());
        cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
        cmd.Parameters.Add("@UserId", Session["uid"].ToString());
        cmd.Parameters.Add("@Status", "1");
        cmd.Parameters.AddWithValue("@dtfinal", dt);

        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Data Submit Successfully');", true);
        bindCOPODetails();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }



    protected void btnExport_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        GridView1.RenderControl(htmlWrite);

        Response.Clear();
        string str = "CO_PO_Mapping" + drpCourse.SelectedValue;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();

    }

    protected void drpValue_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (drpValue.SelectedValue == "0")
        //{
        //    txtDesc.Text = "Not Correlated";
        //}
        //if (drpValue.SelectedValue == "1")
        //{
        //    txtDesc.Text = "Low Correlated";
        //}
        //if (drpValue.SelectedValue == "2")
        //{
        //    txtDesc.Text = "Moderate Correlated";
        //}
        //if (drpValue.SelectedValue == "3")
        //{
        //    txtDesc.Text = "Highly Correlated";
        //}
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        bindCOPODetails();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
       
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add("OUTCOME");
        dt.Columns.Add("PO-1");
        dt.Columns.Add("PO-2");
        dt.Columns.Add("PO-3");
        dt.Columns.Add("PO-4");
        dt.Columns.Add("PO-5");
        dt.Columns.Add("PO-6");
        dt.Columns.Add("PO-7");
        dt.Columns.Add("PO-8");
        dt.Columns.Add("PO-9");
        dt.Columns.Add("PO-10");
        dt.Columns.Add("PO-11");
        dt.Columns.Add("PO-12");
        dt.Columns.Add("PSO-1");
        dt.Columns.Add("PSO-2");
        dt.Columns.Add("PSO-3");        
        foreach (GridViewRow row in GridCOPODetails.Rows)
        {
               DataRow dr = dt.NewRow();
            if (row.RowType == DataControlRowType.DataRow)
            {
                Label lblOutCome = (Label)row.FindControl("lblOutCome");
                DropDownList lblPO1 = (DropDownList)row.FindControl("lblPO1");
                DropDownList lblPO2 = (DropDownList)row.FindControl("lblPO2");
                DropDownList lblPO3 = (DropDownList)row.FindControl("lblPO3");
                DropDownList lblPO4 = (DropDownList)row.FindControl("lblPO4");
                DropDownList lblPO5 = (DropDownList)row.FindControl("lblPO5");
                DropDownList lblPO6 = (DropDownList)row.FindControl("lblPO6");
                DropDownList lblPO7 = (DropDownList)row.FindControl("lblPO7");
                DropDownList lblPO8 = (DropDownList)row.FindControl("lblPO8");
                DropDownList lblPO9 = (DropDownList)row.FindControl("lblPO9");
                DropDownList lblPO10 = (DropDownList)row.FindControl("lblPO10");
                DropDownList lblPO11 = (DropDownList)row.FindControl("lblPO11");
                DropDownList lblPO12 = (DropDownList)row.FindControl("lblPO12");
                DropDownList lblPSO1 = (DropDownList)row.FindControl("lblPSO1");
                DropDownList lblPSO2 = (DropDownList)row.FindControl("lblPSO2");
                DropDownList lblPSO3 = (DropDownList)row.FindControl("lblPSO3");



                dr["OUTCOME"] = lblOutCome.Text;
                dr["PO-1"] = lblPO1.SelectedValue;
                dr["PO-2"] = lblPO2.SelectedValue;
                dr["PO-3"] = lblPO3.SelectedValue;
                dr["PO-4"] = lblPO4.SelectedValue;
                dr["PO-5"] = lblPO5.SelectedValue;
                dr["PO-6"] = lblPO6.SelectedValue;
                dr["PO-7"] = lblPO7.SelectedValue;
                dr["PO-8"] = lblPO8.SelectedValue;
                dr["PO-9"] = lblPO9.SelectedValue;
                dr["PO-10"] = lblPO10.SelectedValue;
                dr["PO-11"] = lblPO11.SelectedValue;
                dr["PO-12"] = lblPO12.SelectedValue;
                dr["PSO-1"] = lblPSO1.SelectedValue;
                dr["PSO-2"] = lblPSO2.SelectedValue;
                dr["PSO-3"] = lblPSO3.SelectedValue;

                dt.Rows.Add(dr);
            }
        }
        SqlCommand cmd = new SqlCommand("Insert_CO_PO_Mapping", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Coded"].ToString());
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedItem.Value.ToString());
        cmd.Parameters.Add("@Semester", drpSemester.SelectedItem.Value.ToString());
        cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
        cmd.Parameters.Add("@UserId", Session["uid"].ToString());
        cmd.Parameters.Add("@Status", "0");
        cmd.Parameters.AddWithValue("@dtfinal", dt);
       
        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Data Save Successfully');", true);
        bindCOPODetails();
    }
}