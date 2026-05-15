using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;

public partial class Student_StudentAssignment : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    protected void Submit(object sender, EventArgs e)
    {
        SqlCommand cmd1 = new SqlCommand("select * from tblAttachmentFiles where AssignmentNo_='" + ViewState["AssignmentNo"].ToString() + "' and StudentCode='" + Session["uid"].ToString() + "'", con1);
        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (FileUpload1.HasFile)
        {
            string contentType = FileUpload1.PostedFile.ContentType;
            string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
            using (Stream fs = FileUpload1.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
                    {
                        if (dt.Rows.Count > 0)
                        {
                            SqlCommand cmd = new SqlCommand("proc_UpdateAttachmentInAttachmentFile", con2);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@StudentCode", Session["uid"].ToString());
                            cmd.Parameters.Add("@AssignmentNo", ViewState["AssignmentNo"].ToString());
                            cmd.Parameters.Add("@AttachmentType", contentType);
                            cmd.Parameters.Add("@FileName", filename);
                            cmd.Parameters.Add("@Attachment", bytes);
                            if (con2.State == ConnectionState.Closed)
                                con2.Open();
                            cmd.ExecuteNonQuery();
                            con2.Close();
                        }
                    }
                }
            }
        }
        BindGrid();
    }
    protected void DownloadFile(object sender, EventArgs e)
    {
        string AssignmentNo = (sender as LinkButton).CommandArgument;
        byte[] bytes;
        string fileName, contentType;
        //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select Attachment,AttachmentType,FileName from tblAttachmentFiles where AssignmentNo_='" + AssignmentNo + "' and Type='FACULTY'";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Attachment"];
                    contentType = sdr["AttachmentType"].ToString();
                    fileName = sdr["FileName"].ToString(); ;
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

    //public string returnExtension(string fileExtension)
    //{
    //    #region
    //     fileExtension FileExtensionNew =new StrConv(Replace(Trim(fileExtension), "'", "''"), VbStrConv.Lowercase)
    //   string FileExtensionNew="";
    //       FileExtensionNew =fileExtension.ToLower();        
    //    switch (FileExtensionNew)
    //    {
    //        case ".htm":
    //            return "text/HTML";
    //        case ".html":
    //            return "text/HTML";
    //        case ".log":
    //            return "text/HTML";
    //        case ".txt":
    //            return "text/plain";
    //        case ".doc":
    //            return "application/msword";
    //        case ".docx":
    //            return "application/msword";
    //        case ".tiff":
    //            return "image/tiff";
    //        case ".tif":
    //            return "image/tiff";
    //        case ".asf":
    //            return "video/x-ms-asf";
    //        case ".avi":
    //            return "video/avi";
    //        case ".zip":
    //            return "application/zip";
    //        case ".xls":
    //            return "application/vnd.ms-excel";
    //        case ".xlsx":
    //            return "application/vnd.ms-excel";
    //        case ".csv":
    //            return "application/vnd.ms-excel";
    //        case ".gif":
    //            return "image/gif";
    //        case ".jpg":
    //            return "image/jpeg";
    //        case ".jpeg":
    //            return "image/jpeg";
    //        case ".bmp":
    //            return "image/bmp";
    //        case ".wav":
    //            return "audio/wav";
    //        case ".mp3":
    //            return "audio/mpeg";
    //        case ".mp2":
    //            return "audio/mpeg";
    //        case ".mpga":
    //            return "audio/mpeg";
    //        case ".mpg":
    //            return "video/mpeg";
    //        case ".dat":
    //            return "video/mpeg";
    //        case ".mpeg":
    //            return "video/mpeg";
    //        case ".rtf":
    //            return "application/rtf";
    //        case ".asp":
    //            return "text/asp";
    //        case ".pdf":
    //            return "application/pdf";
    //        case ".fdf":
    //            return "application/vnd.fdf";
    //        case ".ppt":
    //            return "application/mspowerpoint";
    //        case ".dwg":
    //            return "image/vnd.dwg";
    //        case ".msg":
    //            return "application/msoutlook";
    //        case ".xml":
    //            return "application/xml";
    //        case ".sdxl":
    //            return "application/xml";
    //        case ".xdp":
    //            return "application/vnd.adobe.xdp+xml";
    //        default:
    //           return "application/octet-stream";
    //     }
    //    #endregion
    //}

    protected void grdAssignmentReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void Reply(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        GridViewRow grow = btn.NamingContainer as GridViewRow;
        HiddenField hfCheckenable = (HiddenField)grow.FindControl("hfCheckenable");
        if (hfCheckenable.Value == "True")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal2').modal();", true);
            ViewState["AssignmentNo"] = (sender as LinkButton).CommandArgument;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Your Submission date is Over....');", true);
            return;
        }


    }
    public void BindGrid()
    {
        SqlCommand cmd = new SqlCommand("proc_getAssignmentDetailsForStudent", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@StudentNo", Session["uid"].ToString());
        cmd.Parameters.Add("@CourseCode", Session["CourseCode"].ToString());
        cmd.Parameters.Add("@Semester", Session["Semester"].ToString());
        cmd.Parameters.Add("@Year", Session["Year"].ToString());
        cmd.Parameters.Add("@Section", Session["Section"].ToString());
        cmd.Parameters.Add("@AcademicYear", Session["AcademicYear"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        grdAssignmentReport.DataSource = dt;
        grdAssignmentReport.DataBind();
        ViewState["Table"] = dt;

    }

}