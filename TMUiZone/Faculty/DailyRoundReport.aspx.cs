using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_DailyRoundReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {


                getsuper();
            }
            catch
            {
                Response.Redirect("../Default.aspx");
            }
        }
    }


    public void getsuper()
    {

        SqlCommand cmd = new SqlCommand("pro_getroundreport", con);     
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@UserId", "");
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable(); ;
        daCL.Fill(dtCL);
        grdroundreport.DataSource = dtCL;
        grdroundreport.DataBind();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnexporttoexel_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        grdroundreport.RenderControl(htmlWrite);
        Response.Clear();
        string str = "DAILYROUNDREPORT" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        Response.Write(stringWrite.ToString());
        Response.End();
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
        SqlCommand cmd = new SqlCommand("sp_searhdata", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@StartDate", txtFromDate.Text));
        cmd.Parameters.Add(new SqlParameter("@EndDate", txtTodate.Text));
        if (con.State == ConnectionState.Closed)
        con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable(); ;
        daCL.Fill(dtCL);
        grdroundreport.DataSource = dtCL;
        grdroundreport.DataBind();
        txtFromDate.Text = "";
        txtTodate.Text = "";
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
    protected void txtFromTime_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ValidateDate();
        }
        catch (Exception)
        { }
    }
    protected void txtToTime_TextChanged(object sender, EventArgs e)
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

    protected void Search_Click(object sender, EventArgs e)
    {
        if (txtsupervisor.Text == "")
        {
            string message1 = "Please Fill Ward_Assistant Name.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;

        }
        SqlCommand cmd = new SqlCommand("sp_searchassistant", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@Ward_Assistant_Name", txtsupervisor.Text));
        if (con.State == ConnectionState.Closed)
        con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable(); ;
        daCL.Fill(dtCL);
        grdroundreport.DataSource = dtCL;
        grdroundreport.DataBind();
        txtsupervisor.Text = "";
    }

    protected void lnkPhoto_Click(object sender, EventArgs e)
    {

        LinkButton btn = sender as LinkButton;
        GridViewRow row = btn.NamingContainer as GridViewRow;
        string id = grdroundreport.DataKeys[row.RowIndex].Values[0].ToString();
        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select Upload_Photo from tbl_supervisorround where ID=" + id + "";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Upload_Photo"];

                    //
                    fileName = "Photo";
                }
                con.Close();
            }
        }
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".jpeg");
        Response.ContentType = "image/jpeg"; ;
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }
}
