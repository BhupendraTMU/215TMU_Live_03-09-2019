using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
public partial class Attendance_Form : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //lbl_AcademicYear.Text = Session["MM_Academic_Year"].ToString();
            lbl_StudentName.Text = Session["MM_StudentName"].ToString();
            lbl_Course.Text = Session["MM_Course"].ToString();
            lbl_Program.Text = Session["MM_Program"].ToString();

            lbl_AcademicYear.Text = Session["MM_Academic_Year"].ToString();

            lbl_AutoNo.Text = Session["MM_AutoNo"].ToString();

            SP_FA_MM_Get_Semester();
            sp_FA_MM_Count_Attachement();

        }
    }
    protected void UploadFile_Click(object sender, EventArgs e)
    {
        if (fu_AttendancePdf.HasFile)
        {
            string fileExtension = Path.GetExtension(fu_AttendancePdf.FileName).ToLower();

            // Define allowed extensions
            string[] allowedExtensions = { ".pdf", ".jpg", ".jpeg", ".png" };

            // Check if the file extension is allowed
            if (allowedExtensions.Contains(fileExtension))
            {

                if (fu_AttendancePdf.PostedFile.ContentLength <= 204800) // 200 KB file size check
                {
                    string FileName = Path.GetFileName(fu_AttendancePdf.PostedFile.FileName);
                    string FileType = Path.GetExtension(FileName);
                    byte[] FileData = fu_AttendancePdf.FileBytes;

                    pms_connection con = new pms_connection();
                    SqlCommand cmd = new SqlCommand("sp_FA_Attendence_Attachment_Upload", con.Con);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@Attachment_FileName", FileName);
                    cmd.Parameters.AddWithValue("@Attachment_FileType", FileType);
                    cmd.Parameters.AddWithValue("@Attachment_Data", FileData);
                    cmd.Parameters.AddWithValue("@Student_Id", lbl_StudentName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Academic_Year", lbl_AcademicYear.Text.Trim());
                    cmd.Parameters.AddWithValue("@Applicable_For", lbl_AutoNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy_ID", "");
                    cmd.Parameters.AddWithValue("@CreatedBy_Name", "");
                    cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
                    cmd.Parameters.AddWithValue("@Semester", "");
                    cmd.Parameters.AddWithValue("@Course", lbl_Course.Text.Trim());



                    con.Connect();
                    cmd.ExecuteNonQuery();
                    con.DisConnect();
                    Response.Write("<script>alert('PDF uploaded successfully!');</script>");
                    sp_FA_MM_Count_Attachement();
                }
                else
                {
                    Response.Write("<script>alert('File Size should be under 200kb');</script>");

                }

            }
            else
            {
                Response.Write("<script>alert('Please Upload Only .pdf .jpg .jpeg .png Type Files');</script>");
            }
        }
        else
        {
            Response.Write("<script>alert('Please select a PDF file to upload.');</script>");
        }
    }

    protected void btnPrint_Command(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        Session["SOP_PDF_VIEW_ID"] = id;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('Attendance_Form_Code.aspx','_newtab');", true);

    }

    private void DownloadFile(string StudentId, string AcademicYear, string Course, string Semester)
    {
        pms_connection con = new pms_connection();

        SqlCommand cmd = new SqlCommand("sp_FA_MM_GetAttachmentByID_For_Download", con.Con);

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@StudentId", lbl_StudentName.Text.Trim());
        cmd.Parameters.AddWithValue("@AcademicYear", lbl_AcademicYear.Text.Trim());
        cmd.Parameters.AddWithValue("@Course", lbl_Course.Text.Trim());
        cmd.Parameters.AddWithValue("@Semester", ddl_Semster.SelectedValue.Trim());



        con.Connect();
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                string fileName = reader["Attachment_FileName"].ToString();
                string fileType = reader["Attachment_FileType"].ToString();
                byte[] fileData = (byte[])reader["Attachment_Data"];

                Response.Clear();
                Response.ContentType = fileType;
                //Response.AddHeader("Content-Disposition", $"attachment; filename={fileName}");
                Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", fileName));

                Response.OutputStream.Write(fileData, 0, fileData.Length);
                Response.Flush();
                Response.End();
            }
            reader.Close();
        }
        con.DisConnect();
    }

    private void sp_FA_MM_Count_Attachement()
    {

        pms_connection con = new pms_connection();
        SqlDataReader dr = con.sp_FA_MM_Count_Attachement(lbl_StudentName.Text.Trim(), lbl_AcademicYear.Text.Trim(), lbl_Course.Text.Trim(), ddl_Semster.SelectedValue.Trim());
        dr.Read();
        if (dr.HasRows)
        {

            int count = Convert.ToInt32(dr["CountData"].ToString().Trim());

            if (count == 0)
            {
                if (Session["UserGroup"].ToString() == "STUDENT")
                {
                    pnl_Attendence_Upload.Visible = true;
                }

                else
                {
                    pnl_Attendence_Upload.Visible = false;
                    pnl_Attendence_View.Visible = false;
                    lbl_NoRecords.Visible = true;
                }
            }
            else
            {
                pnl_Attendence_Upload.Visible = false;
                pnl_Attendence_View.Visible = true;
            }
        }
        else
        {
            // No rows returned from the stored procedure, you can handle this case accordingly.
            pnl_Attendence_Upload.Visible = false;  // Example: make the panel visible if no data
            lbl_NoRecords.Visible = true;
        }

        dr.Close();
        con.DisConnect();
    }

    protected void btn_View__Click(object sender, EventArgs e)
    {
        DownloadFile(lbl_StudentName.Text.Trim(), lbl_AcademicYear.Text.Trim(), lbl_Course.Text.Trim(), ddl_Semster.SelectedValue.Trim());
    }

    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        SP_FA_MM_Delete_Attachment();
        sp_FA_MM_Count_Attachement();
        Response.Write("<script>alert('File Deleted Successfully');</script>");
        pnl_Attendence_View.Visible = false;

    }

    public void SP_FA_MM_Delete_Attachment()
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = con.SP_FA_MM_Delete_Attachment(lbl_StudentName.Text.Trim(), lbl_AcademicYear.Text.Trim(), lbl_Course.Text.Trim(), ddl_Semster.SelectedValue.Trim());
        dr.Close();
        con.DisConnect();

    }

    protected void btn_Print_Click(object sender, EventArgs e)
    {

    }





    protected void lbl_AcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        sp_FA_MM_Count_Attachement();
    }

    protected void ddl_Semster_SelectedIndexChanged(object sender, EventArgs e)
    {
        sp_FA_MM_Count_Attachement();

    }


    public void SP_FA_MM_Get_Semester()
    {
        pms_connection con = new pms_connection();
        con.Connect();

        try
        {

            SqlCommand cmd = new SqlCommand("SP_FA_MM_Get_Semester", con.Con);
            cmd.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable();


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            // Bind the dropdown list to the data
            ddl_Semster.DataSource = dt;
            ddl_Semster.DataTextField = "Semester";
            ddl_Semster.DataValueField = "Semester";
            ddl_Semster.DataBind();

            // Optionally, add a default item

        }
        finally
        {

            con.DisConnect(); // Ensure the connection is closed in the finally block

        }
    }

}