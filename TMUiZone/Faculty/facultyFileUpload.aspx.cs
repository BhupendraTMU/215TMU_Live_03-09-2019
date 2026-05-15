using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Net;
using System.Drawing;
public partial class Faculty_facultyFileUpload : System.Web.UI.Page
{
    string  constr = ConfigurationManager.AppSettings["strPortal"];

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {


            if (Session["UserGroup"].ToString() == "PRINCIPAL" || Session["UserGroup"].ToString() == "REGISTRAR")
            {
                Session["UserGroup"] = Session["UserGroup"].ToString();
            }
            if (Session["uid"] == null)
            {

                Response.Redirect("~/Default.aspx");

            }
            if (!IsPostBack)
            {
                showdownload();
            }
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }

    }
    public void InserAndSavefile()
    {
        if (FacultyFileUpload.HasFile)
        {
            try
            {
                string filename = Path.GetFileName(FacultyFileUpload.FileName);
                string folderName = @"C:\fileupload\" + Session["GlobalDimension1Code"].ToString() + "";
                string pathString = System.IO.Path.Combine(folderName, Session["uid"].ToString());
                System.IO.Directory.CreateDirectory(pathString);
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("select max(Sno) as sno from tbl_Fileuplaod", con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        string no = dr["sno"].ToString();
                        string newno;
                        if (no == "")
                        {
                            newno = "1";
                        }
                        else
                        {

                            newno = (int.Parse(no) + 1).ToString();
                        }

                        filename = newno+filename;
                        con.Close();
                    }

                    con.Close();
                
                
                }
                pathString = System.IO.Path.Combine(pathString, filename);

                using (SqlConnection con = new SqlConnection(constr))
                {
                    string []ext = filename.Split('.');
                    string ext1 = ext[1];
                    SqlCommand cmd = new SqlCommand("InsertFileuploadinfo", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FacultyCode", Session["uid"].ToString());
                    cmd.Parameters.AddWithValue("@FacultyCollegeCode", Session["GlobalDimension1Code"].ToString());
                    cmd.Parameters.AddWithValue("@FacultyFileName", filename);
                    cmd.Parameters.AddWithValue("@FacultyFileExtension", ext1);
                    cmd.Parameters.AddWithValue("@FacultyFilePath", pathString);
                    con.Close();
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                  

                }

                FacultyFileUpload.SaveAs(pathString);
                StatusLabel.Text = "Upload status: File uploaded!";
                showdownload();
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
            }
        }
    }

    public void showdownload()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_UploadedFile", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@postemp", Session["UserGroup"].ToString());
            cmd.Parameters.AddWithValue("@FacultyCollegeCode", Session["GlobalDimension1Code"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            Filedownload.DataSource = dt1;
            Filedownload.DataBind();
        }

    }


    public void Fileremoveanddelte(string str)
    {
        string filepath, filename;
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("FileDonload_proc", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Sno", str);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();
            filepath = dr["FacultyFilePath"].ToString();
            filename = dr["FacultyFileName"].ToString();
            con.Close();
            File.Delete(filepath);


            SqlCommand cmd1 = new SqlCommand("DELETE FROM tbl_Fileuplaod WHERE Sno=" + str + "", con);
            con.Open();
            cmd1.ExecuteNonQuery();
            con.Close();

        }
        showdownload();

    }










    protected void btnUpload_Click(object sender, EventArgs e)
    {
        InserAndSavefile();
    }

    protected void Filedownload_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "btnDelete")
        {
            Fileremoveanddelte(e.CommandArgument.ToString());
        }




        if (e.CommandName == "download")
        {
            try
            {
                string filepath, filename;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("FileDonload_proc", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Sno", e.CommandArgument);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    dr.Read();
                    filepath = dr["FacultyFilePath"].ToString();
                    filename = dr["FacultyFileName"].ToString();
                    con.Close();
                    WebClient Client = new WebClient();
                    // Client.DownloadFile("http://i.stackoverflow.com/Content/Img/stackoverflow-logo-250.png", @"C:\folder\");
                    // Client.DownloadFile(filepath, new System.IO.Path(@"D:\" + filename));
                    //Client.DownloadFile(filepath, filename);

                    //StatusLabel.ForeColor = Color.Green;
                    //StatusLabel.Text = "File Downloaded in D Drive and file Name is " + filename + "";




                    Response.ClearContent();
                    Response.Clear();
                    Response.ContentType = "text/plain";
                    Response.AddHeader("Content-Disposition",
                                       "attachment; filename=" + filename + ";");
                    //Response.TransmitFile(Server.MapPath(filepath));
                    Response.TransmitFile(filepath);
                    Response.Flush();
                    Response.End();
                }
            }
            catch
            {
            }
           
        }
    }
    protected void Filedownload_DataBound(object sender, EventArgs e)
    {
       
    }
}