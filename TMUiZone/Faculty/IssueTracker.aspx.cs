using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_IssueTracker : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                binddata();
                getIssuetrackerforall();
                getIssuetrackerlist();
            }
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }
    }

    protected void btn_Reset_Click(object sender, EventArgs e)
    {
        Response.Redirect("IssueTracker.aspx");
    }
    public void getIssuetrackerlist()
    {

        SqlCommand cmd = new SqlCommand("pro_getIssueTrackerList", con);
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdissuetrackerforall.DataSource = dtCL;
        grdissuetrackerforall.DataBind();
    }
    public void binddata()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        string strSQL = "select No_,[First Name],[Branch Name],[Mobile Phone No_],[E-Mail],[Department Name] from TMU$Employee where [No_]='" + Session["uid"].ToString() + "' select * from [TMU$Dimension Value] where [Dimension Code]='DEPARTMENT' and [Name]!=''";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        if (ds.Tables.Count > 0)
        {
            txtemployeeceode.Text = ds.Tables[0].Rows[0]["No_"].ToString();
            txtemployeename.Text = ds.Tables[0].Rows[0]["First Name"].ToString();
            txtemployeedept.Text = ds.Tables[0].Rows[0]["Department Name"].ToString();
            txtcollege.Text = ds.Tables[0].Rows[0]["Branch Name"].ToString();
            txtmobile.Text = ds.Tables[0].Rows[0]["Mobile Phone No_"].ToString();
            //drpinvolvedept.SelectedItem.Text= ds.Tables[0].Rows[0][""].ToString();
            txtemail.Text = ds.Tables[0].Rows[0]["E-Mail"].ToString();





        }
    }
    public void getIssuetrackerforall()
    {

        SqlCommand cmd = new SqlCommand("Pro_DepartmentNAme", con);

        cmd.CommandType = CommandType.StoredProcedure;
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        drpinvolvedept.DataSource = dtCL;
        //grdissuetrackerforall.DataSource = Name;

        drpinvolvedept.DataTextField = "Name";
        drpinvolvedept.DataValueField = "Code";
        drpinvolvedept.DataBind();
    }

    //protected void drpinvolvedept_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    getIssuetrackerforall();
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtcomplaintype.Text == "")
        {
            string message1 = "Please Fill Complain Type.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (txtcomplaintdetail.Text == "")
        {
            string message1 = "Please Fill Complain Details.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (drpstatus.SelectedItem.Text == "SELECT")
        {
            string message1 = "Please Fill Select Status.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (drpinvolvedept.SelectedItem.Text == "SELECT")
        {
            string message1 = "Please Fill Select Department.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (txtremark.Text == "")
        {
            string message1 = "Please Fill Fill Remarks.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }

        SqlCommand cmd = new SqlCommand("Pro_InsertIssueforall", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@CreationTime", "");
        cmd.Parameters.AddWithValue("@EmployeeName", txtemployeename.Text);
        cmd.Parameters.AddWithValue("@EmployeeCode", txtemployeeceode.Text);
        cmd.Parameters.AddWithValue("@DepartmentName", txtemployeedept.Text);
        cmd.Parameters.AddWithValue("@CollegeName", txtcollege.Text);
        cmd.Parameters.AddWithValue("@ComplainType", txtcomplaintype.Text);
        cmd.Parameters.AddWithValue("@ComplainDetail", txtcomplaintdetail.Text);
        cmd.Parameters.AddWithValue("@InvolveDepartment", drpinvolvedept.SelectedValue);
        cmd.Parameters.AddWithValue("@Status", "Open");
        cmd.Parameters.AddWithValue("@MobileNo", txtmobile.Text);
        cmd.Parameters.AddWithValue("@EmailId", txtemail.Text);
        cmd.Parameters.AddWithValue("@Remarks", txtremark.Text);
        cmd.Parameters.AddWithValue("@ID", txtID.Text);
        cmd.Parameters.AddWithValue("@ResolvedBy", "");
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "Your details have been saved successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        getIssuetrackerlist();
    }

    protected void btnselect_Click(object sender, EventArgs e)
    {

        LinkButton btn = sender as LinkButton;
        GridViewRow grow = btn.NamingContainer as GridViewRow;
        string pk = grdissuetrackerforall.DataKeys[grow.RowIndex].Values[0].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "select * from Tbl_IssueTrackerForAll  where ID='" + pk + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        Label2.Text = dt.Rows[0]["ID"].ToString();
        Label4.Text = dt.Rows[0]["Creation Time"].ToString();
        Label6.Text = dt.Rows[0]["Status"].ToString();
        txtmobile.Text = dt.Rows[0]["Mobile No"].ToString();
        txtemail.Text = dt.Rows[0]["Email Id"].ToString();
        txtremark.Text = dt.Rows[0]["Remarks"].ToString();
        txtcomplaintype.Text = dt.Rows[0]["Complain Type"].ToString();
        txtcomplaintdetail.Text = dt.Rows[0]["Complain Detail"].ToString();
        drpstatus.SelectedItem.Text = dt.Rows[0]["Status"].ToString();
        drpinvolvedept.SelectedItem.Text = dt.Rows[0]["Involve Department"].ToString();
        Label8.Text = dt.Rows[0]["Resolved By"].ToString();

    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void btn_ExportToexel_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grdissuetrackerforall.RenderControl(htmlWrite);

        Response.Clear();
        string str = "IssueTracker" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();
    }



}