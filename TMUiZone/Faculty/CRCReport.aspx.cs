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
public partial class Faculty_CRCReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["uid"].ToString() == "TMU03871")
        {
            GETCRC();

        }
        else
        {
            Response.Redirect("../Default.aspx");
        }





    }

    public void GETCRC()
    {

        SqlCommand cmd = new SqlCommand("proc_getcrcreport", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@admittedYear", drpadmittedyear.SelectedItem.Text);

        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdcrcreport.DataSource = dtCL;
        grdcrcreport.DataBind();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnexporttoexel_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grdcrcreport.RenderControl(htmlWrite);

        Response.Clear();
        string str = "CRCReport" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();
    }

    protected void show_Click(object sender, EventArgs e)
    {

        GETCRC();
    }
    protected void lnkResume_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        GridViewRow row = btn.NamingContainer as GridViewRow;
        string id = grdcrcreport.DataKeys[row.RowIndex].Values[0].ToString();    
        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select Resume from tbl_Placement where ID=" + id + "";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Resume"];
                    contentType = "application/pdf";
                    //image/jpeg
                    fileName = "Resume";
                }
                con.Close();
            }
        }
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = contentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();

    }
    protected void lnkPhoto_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        GridViewRow row = btn.NamingContainer as GridViewRow;
        string id = grdcrcreport.DataKeys[row.RowIndex].Values[0].ToString();
        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select Photo from tbl_Placement where ID=" + id + "";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Photo"];
                    contentType = "image/jpeg";
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
        Response.ContentType = contentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();

    }
}

