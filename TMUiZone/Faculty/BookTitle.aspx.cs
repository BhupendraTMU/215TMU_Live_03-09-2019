using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DL;
using System.IO;
using System.Web.Script.Serialization;
public partial class Faculty_BookTitle : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindTitle(Session["uid"].ToString());
                BindData(Session["uid"].ToString(), drpTitle.SelectedValue);
                bindAttachment();
            }
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }

    }
    //select * from [172.0.1.103].[NAAC_ADV_TEST].[dbo].[TMU Advertisement$Book Title Record Line] where [Author Code]='TMU00004'

    public void BindTitle(string FacultyCode)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("SELECT [Title of the Research] FROM [NAAC_ADV_TEST].[dbo].[TMU Advertisement$Book Title Record Line] where [Author Code]=  '" + Session["uid"].ToString() + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                drpTitle.DataSource = dt;
                drpTitle.DataValueField = "Title of the Research";
                drpTitle.DataTextField = "Title of the Research";
                drpTitle.DataBind();

            }
            else
            {

            }

        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }
    }
    public void BindData(string FacultyCode, string Article)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("sp_GetBookTitleRecord", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AuthorCode", FacultyCode);
            cmd.Parameters.AddWithValue("@BookTitle", Article);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtAuthors.Text = dt.Rows[0]["Authors Position"].ToString();
                txtAuthorCode.Text = dt.Rows[0]["Author Code"].ToString();
                txtAName.Text = dt.Rows[0]["Author Name"].ToString();
                txtTitleofJournal.Text = dt.Rows[0]["Title of the Research"].ToString();
                txtclaimType.Text = dt.Rows[0]["Claim Type"].ToString();
                if (dt.Rows[0]["Claim Type"].ToString() == "Book/Book Chapter")
                {
                    tdTOB.Visible = true;
                    tdTOB1.Visible = true;
                    tdNOP.Visible = true;
                    tdNOP1.Visible = true;
                    tdTOP.Visible = true;
                    tdTOP1.Visible = true;
                    tdP.Visible = false;
                    tdP1.Visible = false;
                    tdTOG.Visible = false;
                    tdTOG1.Visible = false;
                    tdIOJ.Visible = false;
                    tdIOJ1.Visible = false;
                }
                else if (dt.Rows[0]["Claim Type"].ToString() == "Patent")
                {
                    tdTOB.Visible = false;
                    tdTOB1.Visible = false;
                    tdNOP.Visible = false;
                    tdNOP1.Visible = false;
                    tdTOP.Visible = false;
                    tdTOP1.Visible = false;
                    tdP.Visible = true;
                    tdP1.Visible = true;
                    tdTOG.Visible = false;
                    tdTOG1.Visible = false;
                    tdIOJ.Visible = false;
                    tdIOJ1.Visible = false;
                }
                else if (dt.Rows[0]["Claim Type"].ToString() == "Research Article")
                {
                    tdTOB.Visible = false;
                    tdTOB1.Visible = false;
                    tdNOP.Visible = false;
                    tdNOP1.Visible = false;
                    tdTOP.Visible = false;
                    tdTOP1.Visible = false;
                    tdP.Visible = false;
                    tdP1.Visible = false;
                    tdTOG.Visible = true;
                    tdTOG1.Visible = true;
                    tdIOJ.Visible = true;
                    tdIOJ1.Visible = true;
                }
                else
                {
                    tdTOB.Visible = false;
                    tdTOB1.Visible = false;
                    tdNOP.Visible = false;
                    tdNOP1.Visible = false;
                    tdTOP.Visible = false;
                    tdTOP1.Visible = false;
                    tdP.Visible = false;
                    tdP1.Visible = false;
                    tdTOG.Visible = false;
                    tdTOG1.Visible = false;
                    tdIOJ.Visible = false;
                    tdIOJ1.Visible = false;



                }
                if (dt.Rows[0]["Request Submitted"].ToString() == "1")
                {
                    btnSubmit.Visible = false;
                    btnUpload.Visible = false;
                }
                else
                {
                    btnSubmit.Visible = true;
                    btnUpload.Visible = true;
                }

                hfAuthor.Value = dt.Rows[0]["AuthorsN"].ToString();


                //txtWebLink.Text = dt.Rows[0]["Web Link of the Res_ Article"].ToString();
                txtATBP.Text = dt.Rows[0]["AmountToBePaid"].ToString();
                //txtStatus.Text = dt.Rows[0]["Status1"].ToString();
                //txtWhetherSCOPUS.Text = dt.Rows[0]["Whether SCOPUS"].ToString();

            }
            else
            {

            }

        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string filename = Path.GetFileName(flupload.PostedFile.FileName);
        string contentType = flupload.PostedFile.ContentType;
        using (Stream fs = flupload.PostedFile.InputStream)
        {
            using (BinaryReader br = new BinaryReader(fs))
            {
                byte[] bytes = br.ReadBytes((Int32)fs.Length);
                using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["str"]))
                {

                    SqlCommand cmd = new SqlCommand("proc_InsertINCAttachment", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Add("@DocName", txtTOJ.Text);
                    cmd.Parameters.Add("@Author", hfAuthor.Value);
                    cmd.Parameters.Add("@AuthorCode", txtAuthorCode.Text);
                    cmd.Parameters.Add("@TOJ", txtTitleofJournal.Text);
                    cmd.Parameters.Add("@AttachmentFor", drpattachtype.SelectedValue);
                    cmd.Parameters.Add("@AuthorName", txtAName.Text);
                    cmd.Parameters.Add("@Attachment", bytes);
                    cmd.Parameters.Add("@AttachmentType", contentType);
                    cmd.Parameters.Add("@FileName", filename);


                    if (con2.State == ConnectionState.Closed)
                        con2.Open();
                    cmd.ExecuteNonQuery();
                    con2.Close();



                }
            }
        }
        bindAttachment();
    }
    public void bindAttachment()
    {
        SqlCommand cmd = new SqlCommand("proc_GetINCAttachment", con);
        cmd.Parameters.AddWithValue("@Fid", Session["uid"].ToString());

        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        con.Open();
        da.Fill(dt1);
        grdDocument.DataSource = dt1;
        grdDocument.DataBind();
    }
    protected void DownloadInboxFile(object sender, EventArgs e)
    {
        string Eval = (sender as LinkButton).CommandArgument;
        string AssignmentNo = (sender as LinkButton).CommandArgument;

        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["str"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select Attachment,[Content Type],[File Name] from [NAAC_ADV_TEST].[dbo].[TMU Advertisement$Author Documents] where CONCAT([Line No_],[Author Code]) collate Latin1_General_CI_AI ='" + AssignmentNo + "' ";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Attachment"];
                    contentType = sdr["Content Type"].ToString();
                    fileName = sdr["File Name"].ToString();
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

    protected void DeleteFile(object sender, EventArgs e)
    {
        string Eval = (sender as LinkButton).CommandArgument;
        string AssignmentNo = (sender as LinkButton).CommandArgument;


        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["str"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "delete from [NAAC_ADV_TEST].[dbo].[TMU Advertisement$Author Documents] where CONCAT([Line No_],[Author Code]) collate Latin1_General_CI_AI ='" + AssignmentNo + "' ";
                cmd2.Connection = con2;
                con2.Open();
                cmd2.ExecuteNonQuery();
                con2.Close();
            }
        }
        bindAttachment();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        con.Open();
        SqlCommand cmd = new SqlCommand("Pro_INCSubmit", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@AuthorCode", txtAuthorCode.Text);
        cmd.Parameters.AddWithValue("@TOB", txtTOTB.Text);
        cmd.Parameters.AddWithValue("@NOP", txtNOP.Text);
        cmd.Parameters.AddWithValue("@TOP", DRPTOP.SelectedValue);        
        cmd.Parameters.AddWithValue("@DOP", txtDOP.Text);
        cmd.Parameters.AddWithValue("@TOJ", txtTitleofGen.Text);
        cmd.Parameters.AddWithValue("@IOJ", txtInOfJour.Text);
        cmd.Parameters.AddWithValue("@Title", txtTitleofJournal.Text);
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data saved successfully')", true);
        BindData(Session["uid"].ToString(), drpTitle.SelectedValue);
    }
    protected void drpTitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTOTB.Text = "";
        txtNOP.Text = "";
        DRPTOP.SelectedIndex = 0;
        txtDOP.Text = "";
        txtTitleofGen.Text = "";
        txtInOfJour.Text = "";

        BindData(Session["uid"].ToString(), drpTitle.SelectedValue);
    }
}