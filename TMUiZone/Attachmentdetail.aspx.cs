using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
//using Ionic.Zip;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Collections;
using System.Configuration;

public partial class Attachmentdetail : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal con;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Portalcon = new Connection();
            con = new ServicePoratal();
            showdataattachementCSV();
        }
        catch (Exception)
        {
            Response.Redirect("Default.aspx");
        }
    }


    public void showdataattachementCSV()
    {
        SqlDataReader dr = con.SHow_Attachement(Session["Docid"].ToString(), Session["Company"].ToString(), Session["uid"].ToString());
        DataTable Dt = new DataTable();
        Dt.Load(dr);
        grdAttachmentcsv.DataSource = Dt;
        grdAttachmentcsv.DataBind();
        dr.Close();
        con.DisConnect();

    }
    protected void DownloadFile(object sender, EventArgs e)
    {
        string id = ((sender as LinkButton).CommandArgument);
        byte[] bytes;
        string fileName, contentType;
        //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        //using (SqlConnection con = new SqlConnection())
        //{
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "select File_Attachment_Name, Data, ContentType from tble_Attachment where id=@Id";
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Connection = con.Con;
            con.Connect();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                sdr.Read();
                bytes = (byte[])sdr["Data"];
                contentType = sdr["ContentType"].ToString();
                fileName = sdr["File_Attachment_Name"].ToString();
            }
            con.DisConnect();
        }
        //  }
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
    protected void lnkDeketeattachementcsv_Command(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        con.delete_tble_Attachment(id);
        con.DisConnect();
        showdataattachementCSV();
    }
}