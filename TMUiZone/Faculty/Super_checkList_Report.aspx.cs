using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_Super_checkList_Report : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        getsuperchecklist();

    }
    public void getsuperchecklist()
    {

        SqlCommand cmd = new SqlCommand("sp_superchecklist", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable(); ;
        daCL.Fill(dtCL);
        grdsuperchecklist.DataSource = dtCL;
        grdsuperchecklist.DataBind();
    }
    public void ValidateDate()
    {

        DateTime frodatecom = DateTime.ParseExact(txtFromDate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime Todatecom = DateTime.ParseExact(txtTodate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //DateTime frodatecom = DateTime.ParseExact(txtFromDate.Text.Trim() + " " + txtFromTime.Text.Trim(), "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
        //DateTime Todatecom = DateTime.ParseExact(txtTodate.Text.Trim() + " " + txtToTime.Text.Trim(), "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
        if (frodatecom > Todatecom)
        {
            txtTodate.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('To Date should always be greater than From Date');", true);

        }
        else
        {


        }
    }


    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ValidateDate();
        }
        catch (Exception)
        { }
    }

    protected void txtTodate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ValidateDate();
        }
        catch (Exception)
        { }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (txtFromDate.Text == "")
        {
            string message1 = "Please Fill From Date.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (txtTodate.Text == "")
        {
            string message1 = "Please Fill To Date.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }

        SqlCommand cmd = new SqlCommand("sp_searhsupervisorcheckdata", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@StartDate", txtFromDate.Text));
        cmd.Parameters.Add(new SqlParameter("@EndDate", txtTodate.Text));
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable(); ;
        daCL.Fill(dtCL);
        grdsuperchecklist.DataSource = dtCL;
        grdsuperchecklist.DataBind();
        txtFromDate.Text = "";
        txtTodate.Text = "";
    }

    protected void Search_Click(object sender, EventArgs e)
    {
        if (txtsupervisor.Text == "")
        {
            string message1 = "Please Fill Supervisor Name.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;

        }
        SqlCommand cmd = new SqlCommand("sp_searchward", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@SupervisorName", txtsupervisor.Text));
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable(); ;
        daCL.Fill(dtCL);
        grdsuperchecklist.DataSource = dtCL;
        grdsuperchecklist.DataBind();
        txtsupervisor.Text = "";
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void btnexporttoexel_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        grdsuperchecklist.RenderControl(htmlWrite);
        Response.Clear();
        string str = "CHECKLISTREPORT" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        Response.Write(stringWrite.ToString());
        Response.End();
    }
}

