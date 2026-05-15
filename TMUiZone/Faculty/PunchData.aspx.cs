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
using System.Drawing;
using System.Web.Services;
using System.Web.Script.Services;

public partial class Faculty_PunchData : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    ServicePoratal con1;
    protected void Page_Load(object sender, EventArgs e)
    {
        con1 = new ServicePoratal();
        if (!IsPostBack)
        {
            bindReport();
            BindYear();
            //bindDesignation();
            bindDepartment();
        }
    }
    protected void BtnPresent_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
        int index = row.RowIndex;

        Label UserId = (Label)grdPunchData.Rows[index].FindControl("lblUserID");

        Label Month = (Label)grdPunchData.Rows[index].FindControl("Label5");
        Label Year = (Label)grdPunchData.Rows[index].FindControl("lblYear");

        SqlCommand cmd = new SqlCommand("Sp_ReviewPunchData", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@EmployeeCode", UserId.Text);
        cmd.Parameters.Add("@Month", Month.Text);
        cmd.Parameters.Add("@Year", Year.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        grdAttandanceDetails.DataSource = dt;
        grdAttandanceDetails.DataBind();
        GridViewDetails.Show();
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        GridViewDetails.Hide();
    }
    public void BindYear()
    {

        int Currentyear = System.DateTime.Now.Year;

        for (int i = Currentyear - 1; i <= Currentyear; i++)
            ddlYear1.Items.Add(i.ToString());
    }
    //public void bindDesignation()
    //{
    //    string query = "Select distinct [Designation Code], [Designation Description] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Designation Master] where [Designation Description]!=''";
    //    SqlDataAdapter da = new SqlDataAdapter(query, con1.Con);
    //    DataTable dt = new DataTable();

    //    da.Fill(dt);
    //    con1.DisConnect();
    //    drpDesignation.DataSource = dt;
    //    drpDesignation.DataTextField = "Designation Description";
    //    drpDesignation.DataValueField = "Designation Code";
    //    drpDesignation.DataBind();
    //    drpDesignation.Items.Insert(0, new ListItem("--Select--", "0"));

    //}
    public void bindDepartment()
    {

        string query = "select distinct [Department Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where [Status]=0";
        SqlDataAdapter da = new SqlDataAdapter(query, con1.Con);
        DataTable dt = new DataTable();

        da.Fill(dt);
        con1.DisConnect();
        drpDepartment.DataSource = dt;
        drpDepartment.DataTextField = "Department Name";
        drpDepartment.DataValueField = "Department Name";
        drpDepartment.DataBind();
        drpDepartment.Items.Insert(0, new ListItem("--Select--", "0"));


    }
    protected void btnexporttoexel_Click(object sender, EventArgs e)
    {

    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        bindReport();
    }

    public void bindReport()
    {
        SqlCommand cmd = new SqlCommand("proc_getEmployeePunch", con);
        cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.Add("@FromDate", "");
        //cmd.Parameters.Add("@ToDate", "");
        //cmd.Parameters.Add("@UserId", "");
        //if (drpDesignation.SelectedIndex == 0)
        //{
        //    cmd.Parameters.Add("@Designation", "");
        //}
        //else
        //{
        //    cmd.Parameters.Add("@Designation", drpDesignation.SelectedValue);
        //}
        if (drpDepartment.SelectedIndex == 0)
        {
            cmd.Parameters.Add("@Department", "");
        }
        else
        {
            cmd.Parameters.Add("@Department", drpDepartment.SelectedValue);
        }
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);

        grdPunchData.DataSource = dtCL;
        grdPunchData.DataBind();
    }
    protected void btnGet_Click(object sender, EventArgs e)
    {
        bindReport();


    }
    protected void btnReport_Click(object sender, EventArgs e)
    {



    }
}