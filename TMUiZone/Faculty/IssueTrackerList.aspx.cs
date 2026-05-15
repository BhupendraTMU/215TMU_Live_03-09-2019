using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_IssueTrackerList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                getIssueTrackerlist();
            }
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }
    }

    public void getIssueTrackerlist()
    {

        SqlCommand cmd = new SqlCommand("pro_getIssueTrackerlistforAll", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@InvolveDepartment ", Session["Departmentcode"].ToString());     
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdissuetrackerlistforall.DataSource = dtCL;
        grdissuetrackerlistforall.DataBind();


    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grdissuetrackerlistforall.RenderControl(htmlWrite);

        Response.Clear();
        string str = "IssueTrackerList" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();
    }

    protected void btn_Reset_Click(object sender, EventArgs e)
    {
        Response.Redirect("IssueTrackerList.aspx");
    }

    protected void btnselect_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        GridViewRow grow = btn.NamingContainer as GridViewRow;
        string pk = grdissuetrackerlistforall.DataKeys[grow.RowIndex].Values[0].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "select * from Tbl_IssueTrackerForAll  where ID='" + pk + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        txtemployeename.Text= dt.Rows[0]["Employee Name"].ToString();
        txtemployeeceode.Text= dt.Rows[0]["Employee Code"].ToString();
        txtemployeedept.Text= dt.Rows[0]["Department Name"].ToString();
        txtcollege.Text= dt.Rows[0]["College Name"].ToString();
        txtinvolvedept.Text= dt.Rows[0]["Involve Department"].ToString();
        Label2.Text= dt.Rows[0]["ID"].ToString();
        Label4.Text= dt.Rows[0]["Creation Time"].ToString();
        Label6.Text= dt.Rows[0]["Status"].ToString();
        txtmobile.Text = dt.Rows[0]["Mobile No"].ToString();
        txtemail.Text = dt.Rows[0]["Email Id"].ToString();
        txtremark.Text = dt.Rows[0]["Remarks"].ToString();
        txtcomplaintype.Text = dt.Rows[0]["Complain Type"].ToString();
        txtcomplaindetail.Text = dt.Rows[0]["Complain Detail"].ToString();
        drpstatus.SelectedItem.Text = dt.Rows[0]["Status"].ToString();
        txtID.Text= dt.Rows[0]["ID"].ToString();
        //drpinvolvedept.SelectedItem.Text = dt.Rows[0]["Involve Department"].ToString();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (drpstatus.SelectedItem.Text == "Open")
        {
            string message1 = "Please Fill Complain Status .";
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
        cmd.Parameters.AddWithValue("@ComplainDetail", txtcomplaindetail.Text);
        cmd.Parameters.AddWithValue("@InvolveDepartment", txtinvolvedept.Text);
        cmd.Parameters.AddWithValue("@Status", drpstatus.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@MobileNo", txtmobile.Text);
        cmd.Parameters.AddWithValue("@EmailId", txtemail.Text);
        cmd.Parameters.AddWithValue("@Remarks", txtremark.Text);
        cmd.Parameters.AddWithValue("@ID", txtID.Text);
        cmd.Parameters.AddWithValue("@ResolvedBy", Session["uid"].ToString());
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
    }
}
