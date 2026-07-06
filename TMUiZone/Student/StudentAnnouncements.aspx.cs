using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;

public partial class Student_StudentAnnouncement : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(
        ConfigurationManager.ConnectionStrings["TMUCON"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindStudentAnnouncement();
        }
    }

    private void BindStudentAnnouncement()
    {
        SqlCommand cmd = new SqlCommand("Sp_GetAnnouncement1_Student", con);
        cmd.CommandType = CommandType.StoredProcedure;

        // Use Convert.ToString to avoid NullReferenceException when Session keys are missing
        cmd.Parameters.AddWithValue("@CollegeID", Convert.ToString(Session["College"]));
        cmd.Parameters.AddWithValue("@CourseID", Convert.ToString(Session["CourseCode"]));
        cmd.Parameters.AddWithValue("@Semester", Convert.ToString(Session["Semester"]));
        cmd.Parameters.AddWithValue("@StudentID", Convert.ToString(Session["uid"]));

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        gvStudentAnnouncement.DataSource = dt;
        gvStudentAnnouncement.DataBind();
    }

    //private void BindNotificationCount()
    //{
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand("usp_AnnouncementsNotification", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //
    //        cmd.Parameters.AddWithValue("@StudentID", Convert.ToString(Session["uid"]));
    //        cmd.Parameters.AddWithValue("@CollegeID", Convert.ToString(Session["College"]));
    //        cmd.Parameters.AddWithValue("@CourseID", Convert.ToString(Session["CourseCode"]));
    //        cmd.Parameters.AddWithValue("@Semester", Convert.ToString(Session["Semester"]));
    //
    //        con.Open();
    //        int count = Convert.ToInt32(cmd.ExecuteScalar());
    //        con.Close();
    //
    //        lblNotificationCount.Text = count.ToString();
    //    }
    //    catch
    //    {
    //        con.Close();
    //        lblNotificationCount.Text = "0";
    //    }
    //}
    protected void gvStudentAnnouncement_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)

    {
        if (e.CommandName == "ViewFile")
        {
            if (e.CommandArgument == null) return;

            string[] args = e.CommandArgument.ToString().Split('|');
            if (args.Length < 5) return;

            int announcementID = Convert.ToInt32(args[0]);

            // 🔴 MARK AS READ (VIEW PE)
            SqlCommand cmdRead = new SqlCommand("Sp_MarkAnnouncementRead", con);
            cmdRead.CommandType = CommandType.StoredProcedure;
            cmdRead.Parameters.AddWithValue("@AnnouncementID", announcementID);
            cmdRead.Parameters.AddWithValue("@StudentID", Convert.ToString(Session["uid"]));

            con.Open();
            cmdRead.ExecuteNonQuery();
            con.Close();

            // Refresh master notification count so UI updates immediately
            var master = this.Master as IndexMaster;
            if (master != null)
            {
                try
                {
                    master.BindNotificationCount();
                }
                catch
                {
                    // swallow to avoid breaking modal open if master refresh fails
                }
            }

            // Modal data bind
            lblModalTitle.Text = args[1];
            lblDescription.Text = args[2];
            lblDate.Text = args[3];
            iframeFile.Attributes["src"] = ResolveUrl(args[4]);

            hfAnnouncementID.Value = announcementID.ToString();

            ScriptManager.RegisterStartupScript(
                this, GetType(),
                "openModal",
                "$('#fileModal').modal('show');",
                true
            );
        }
        else if (e.CommandName == "DownloadFile")
        {
            if (e.CommandArgument == null) return;

            string[] args = e.CommandArgument.ToString().Split('|');
            if (args.Length < 2) return;

            DownloadFile(args[1]);
        }
    }

    protected void lnkDownloadModal_Click(object sender, EventArgs e)
    {
        string filePath = hfFilePath.Value;
        if (string.IsNullOrEmpty(filePath))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "noFile", "alert('No file available to download.');", true);
            return;
        }

        DownloadFile(filePath);
    }

    private void DownloadFile(string filePath)
    {
        try
        {
            // Resolve virtual path to physical path
            string resolvedUrl = ResolveUrl(filePath);
            string physicalPath = Server.MapPath(resolvedUrl);

            if (!File.Exists(physicalPath))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "fileMissing", "alert('File not found on server.');", true);
                return;
            }

            string fileName = Path.GetFileName(physicalPath);

            // Determine MIME type using built-in mapping
            string contentType = MimeMapping.GetMimeMapping(physicalPath) ?? "application/octet-stream";

            Response.Clear();
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + HttpUtility.UrlPathEncode(fileName) + "\"");
            Response.TransmitFile(physicalPath);
            Response.Flush();
            // avoid Response.End which causes ThreadAbortException
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            return;
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "downloadError",
                "alert('Error while downloading file.');", true);
        }
    }
}