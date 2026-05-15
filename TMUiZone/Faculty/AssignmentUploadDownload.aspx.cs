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
using System.Net;
using System.Net.Mail;
using Microsoft.Reporting.WebForms;
using System.Transactions;
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Text.RegularExpressions;
using System.Net;
using System.Text;
using WebReference;
using System.Transactions;
public partial class Faculty_AssignmentUploadDownload : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]);
    DL.FacultyPortalDL FDL = new DL.FacultyPortalDL();
    DataTable dt = new DataTable();
    DateTime dt3 = Convert.ToDateTime(System.DateTime.Now.ToString("MMM-dd-yyyy"));
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {
                    //if ((this.Master as IndexMaster).GetLinkYesNo("AssignmentUploadDownload") == "True")
                    if ("True" == "True")
                    {
                        BindCourse();
                        bindAcademicYear();
                    }
                    else
                    { Response.Redirect("~/Default.aspx"); }
                }

            }
            BindAssignmentReport();
            BindAssignmentOutboxReport();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "fader", "$('[id$=pnlInbox]').hide(); ", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "fader1", "$('[id$=pnlOutbox]').hide(); ", true);
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            //{
            try
            {
                DataTable dt = FDL.GetStudentDetails(drpCourse.SelectedValue, drpSemester.SelectedItem.Text, drpSection.SelectedValue, drpAcademicYear.SelectedValue, ddlGroup.SelectedValue, ddlBatch.SelectedValue, drpSubject.SelectedValue, lblSubjectType.Text);

                if (dt.Rows.Count > 0)
                {
                    string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string contentType = FileUpload1.PostedFile.ContentType;
                    using (Stream fs = FileUpload1.PostedFile.InputStream)
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            byte[] bytes = br.ReadBytes((Int32)fs.Length);
                            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
                            {
                                //hrms portal
                                SqlCommand cmd = new SqlCommand("proc_InsertIntoAttachmentType", con2);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
                                cmd.Parameters.Add("@FacultyName", Session["Fulname"].ToString());
                                cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
                                cmd.Parameters.Add("@SemesterYear", drpSemester.SelectedValue);
                                cmd.Parameters.Add("@Section", drpSection.SelectedValue);
                                cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
                                cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
                                cmd.Parameters.Add("@SubjectDescription", drpSubject.SelectedItem.ToString());
                                cmd.Parameters.Add("@SubjectType", lblSubjectType.Text);
                                cmd.Parameters.Add("@DueDate", txtDueDate.Text);
                                cmd.Parameters.Add("@CloseDate", txtCloseDate.Text);
                                cmd.Parameters.Add("@Type", "FACULTY");
                                cmd.Parameters.Add("@AssignmentStatus", "PENDING");
                                cmd.Parameters.Add("@MaximumMarks", txtMaximumMarks.Text);
                                cmd.Parameters.Add("@MarksObtained", "0");
                                cmd.Parameters.Add("@Comments", "");
                                cmd.Parameters.Add("@AttachmentType", contentType);
                                cmd.Parameters.Add("@Attachment", bytes);
                                cmd.Parameters.Add("@FileName", filename);
                                if (con2.State == ConnectionState.Closed)
                                    con2.Open();
                                cmd.ExecuteNonQuery();
                                con2.Close();
                                //nav Entry
                                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
                                SqlCommand cmdnav = new SqlCommand("proc_InsertAssignmentHeader", con);
                                cmdnav.CommandType = CommandType.StoredProcedure;
                                cmdnav.Parameters.Add("@FacultyCode", Session["uid"].ToString());
                                cmdnav.Parameters.Add("@FacultyName", Session["Fulname"].ToString());
                                cmdnav.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
                                cmdnav.Parameters.Add("@SemesterYear", drpSemester.SelectedValue);
                                cmdnav.Parameters.Add("@Section", drpSection.SelectedValue);
                                cmdnav.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
                                cmdnav.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
                                cmdnav.Parameters.Add("@SubjectDescription", drpSubject.SelectedItem.ToString());
                                cmdnav.Parameters.Add("@SubjectType", lblSubjectType.Text);
                                cmdnav.Parameters.Add("@DueDate", txtDueDate.Text);
                                cmdnav.Parameters.Add("@CloseDate", txtCloseDate.Text);
                                cmdnav.Parameters.Add("@Type", "FACULTY");
                                //cmdnav.Parameters.Add("@AssignmentStatus",0);
                                cmdnav.Parameters.Add("@MaximumMarks", txtMaximumMarks.Text);
                                cmdnav.Parameters.Add("@MarksObtained", "0");
                                cmdnav.Parameters.Add("@Comments", "");
                                cmdnav.Parameters.Add("@AttachmentType", contentType);
                                cmdnav.Parameters.Add("@FileName", filename);
                                if (con.State == ConnectionState.Closed)
                                    con.Open();
                                cmdnav.ExecuteNonQuery();
                                con.Close();
                                for (int j = 0; j < dt.Rows.Count; j++)
                                {
                                    //hrms portal
                                    SqlCommand cmd1 = new SqlCommand("proc_InsertIntoAttachmentFile", con2);
                                    cmd1.CommandType = CommandType.StoredProcedure;
                                    cmd1.Parameters.Add("@FacultyCode", Session["uid"].ToString());
                                    cmd1.Parameters.Add("@FacultyName", Session["Fulname"].ToString());
                                    cmd1.Parameters.Add("@StudentCode", dt.Rows[j]["No_"]);
                                    cmd1.Parameters.Add("@StudentName", dt.Rows[j]["Student Name"]);
                                    cmd1.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
                                    cmd1.Parameters.Add("@SemesterYear", drpSemester.SelectedValue);
                                    cmd1.Parameters.Add("@Section", drpSection.SelectedValue);
                                    cmd1.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
                                    cmd1.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
                                    cmd1.Parameters.Add("@SubjectDescription", drpSubject.SelectedItem.ToString());
                                    cmd1.Parameters.Add("@SubjectType", lblSubjectType.Text);
                                    cmd1.Parameters.Add("@DueDate", txtDueDate.Text);
                                    cmd1.Parameters.Add("@CloseDate", txtCloseDate.Text);
                                    cmd1.Parameters.Add("@Type", "STUDENT");
                                    cmd1.Parameters.Add("@AssignmentStatus", "PENDING");
                                    cmd1.Parameters.Add("@MaximumMarks", txtMaximumMarks.Text);
                                    cmd1.Parameters.Add("@MarksObtained", "0");
                                    cmd1.Parameters.Add("@Comments", "");
                                    cmd1.Parameters.Add("@AttachmentType", "");
                                    cmd1.Parameters.Add("@FileName", filename);
                                    if (con2.State == ConnectionState.Closed)
                                        con2.Open();
                                    cmd1.ExecuteNonQuery();
                                    con2.Close();

                                    string Message = "Please submit the assignment on the topic '" + txtSubjectDescription.Text + "' before " + txtDueDate.Text + "";
                                    try
                                    {
                                        SMS(dt.Rows[j]["Mobile Number"].ToString(), Message);
                                    }
                                    catch { }

                                    //Nav entry

                                    SqlCommand cmdnav1 = new SqlCommand("proc_InsertAssignmentLine", con);
                                    cmdnav1.CommandType = CommandType.StoredProcedure;
                                    cmdnav1.Parameters.Add("@FacultyCode", Session["uid"].ToString());
                                    cmdnav1.Parameters.Add("@FacultyName", Session["Fulname"].ToString());
                                    cmdnav1.Parameters.Add("@StudentCode", dt.Rows[j]["No_"]);
                                    cmdnav1.Parameters.Add("@StudentName", dt.Rows[j]["Student Name"]);
                                    cmdnav1.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
                                    cmdnav1.Parameters.Add("@SemesterYear", drpSemester.SelectedValue);
                                    cmdnav1.Parameters.Add("@Section", drpSection.SelectedValue);
                                    cmdnav1.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
                                    cmdnav1.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
                                    cmdnav1.Parameters.Add("@SubjectDescription", drpSubject.SelectedItem.ToString());
                                    cmdnav1.Parameters.Add("@SubjectType", lblSubjectType.Text);
                                    cmdnav1.Parameters.Add("@DueDate", txtDueDate.Text);
                                    cmdnav1.Parameters.Add("@CloseDate", txtCloseDate.Text);
                                    cmdnav1.Parameters.Add("@Type", "STUDENT");
                                    //cmdnav1.Parameters.Add("@AssignmentStatus", 0);
                                    cmdnav1.Parameters.Add("@MaximumMarks", txtMaximumMarks.Text);
                                    cmdnav1.Parameters.Add("@MarksObtained", "0");
                                    cmdnav1.Parameters.Add("@Comments", "");
                                    cmdnav1.Parameters.Add("@AttachmentType", "");
                                    cmdnav1.Parameters.Add("@FileName", filename);
                                    if (con.State == ConnectionState.Closed)
                                        con.Open();
                                    cmdnav1.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }
                    }
                    //SendMail();
                    //string Message = "Please submit the assignment on the topic '" + txtSubjectDescription.Text + "' before " + txtDueDate.Text + "";
                    //SMS("", Message);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success','Upload Successfully');", true);
                    SqlConnection conNoSeries = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
                    if (conNoSeries.State == ConnectionState.Closed)
                        conNoSeries.Open();
                    SqlCommand cmdnoseries = new SqlCommand("Pro_Update_NoSeries", conNoSeries);
                    cmdnoseries.CommandType = CommandType.StoredProcedure;
                    cmdnoseries.ExecuteNonQuery();
                    cmdnoseries.Clone();
                    Blank();
                    BindAssignmentOutboxReport();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error','There is no Student');", true);
                }



                //scope.Complete();
            }
            catch (Exception ex)
            {
                //scope.Dispose();
                // Response.Redirect("~/Default.aspx");
            }
        }
    }

    public void BindStudentDetails()
    {
        dt = FDL.GetStudentDetails(drpCourse.SelectedValue, drpSemester.SelectedItem.Text, drpSection.SelectedValue, drpAcademicYear.SelectedValue, ddlGroup.SelectedValue, ddlBatch.SelectedValue, drpSubject.SelectedValue, lblSubjectType.Text);
    }
    public void BindCourse()
    {
        SqlCommand cmd = new SqlCommand("proc_GetCourseFromCourseWiseFacultyCollege_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpCourse.DataSource = dt;
        drpCourse.DataTextField = "Details";
        drpCourse.DataValueField = "No_";
        drpCourse.DataBind();
        //Report Purpose
        drpCourse1.DataSource = dt;
        drpCourse1.DataTextField = "Details";
        drpCourse1.DataValueField = "No_";
        drpCourse1.DataBind();
    }
    public void BindSemester()
    {
        SqlCommand cmd = new SqlCommand("proc_GetSemesterFromCourseWiseFaculty_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID1", Session["uid"].ToString());
        cmd.Parameters.Add("@ID", drpCourse.SelectedValue);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSemester.DataSource = dt;
        drpSemester.DataTextField = "Details";
        drpSemester.DataValueField = "No_";
        drpSemester.DataBind();
    }
    public void BindSection()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetSectionFromCourseWiseFaculty_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSection.DataSource = dt;
        drpSection.DataTextField = "Details";
        drpSection.DataValueField = "No_";
        drpSection.DataBind();
    }
    public void BindSectionforReport()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetSectionFromCourseWiseFaculty_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@CourseCode", drpCourse1.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester1.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        ddlSection.DataSource = dt;
        ddlSection.DataTextField = "Details";
        ddlSection.DataValueField = "No_";
        ddlSection.DataBind();
    }
    public void BindSubject()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetSubjectFromTimeTableForAssignment", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@Section", drpSection.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSubject.DataSource = dt;
        drpSubject.DataTextField = "Details";
        drpSubject.DataValueField = "No_";
        drpSubject.DataBind();
    }
    //for Report Purpose
    public void BindSubjectforReport()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetSubjectFromTimeTableForAssignment", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse1.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester1.SelectedValue);
        cmd.Parameters.Add("@Section", "");
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@AcademicYear", drpAcademic1.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        ddlSubject.DataSource = dt;
        ddlSubject.DataTextField = "Details";
        ddlSubject.DataValueField = "No_";
        ddlSubject.DataBind();
    }
    //for Report Purppose
    public void BindSemesterforReport()
    {
        SqlCommand cmd = new SqlCommand("proc_GetSemesterFromCourseWiseFaculty_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID1", Session["uid"].ToString());
        cmd.Parameters.Add("@ID", drpCourse1.SelectedValue);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSemester1.DataSource = dt;
        drpSemester1.DataTextField = "Details";
        drpSemester1.DataValueField = "No_";
        drpSemester1.DataBind();
    }
    //for Report Purpose
    //public void BindAssigment()
    //{
    //    SqlCommand cmd = new SqlCommand("proc_GetAssignmentNo", con1);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.Add("@ID1", Session["uid"].ToString());
    //    cmd.Parameters.Add("@academic", drpAcademic1.SelectedValue);
    //    cmd.Parameters.Add("@Course", drpCourse1.SelectedValue);
    //    cmd.Parameters.Add("@Sem", drpSemester1.SelectedValue);
    //    cmd.Parameters.Add("@Subject", ddlSubject.SelectedValue);
    //    DataTable dt = new DataTable();
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    da.Fill(dt);
    //    drpAssignmentNo.DataSource = dt;
    //    drpAssignmentNo.DataTextField = "AssignmentNo_";
    //    drpAssignmentNo.DataValueField = "AssignmentNo_";
    //    drpAssignmentNo.DataBind();
    //}
    public void BindSubjectType()
    {
        DataTable dt = new DataTable();
        dt = FDL.GetSubjectList(drpCourse.SelectedItem.Text, drpSemester.SelectedItem.Text);
        drpSubject.DataSource = dt;
        drpSubject.DataTextField = "Details";
        drpSubject.DataValueField = "No_";
        drpSubject.DataBind();
    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSemester();
    }
    protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSection();
        bindGroupList();
        bindBatchList();
        BindSubject();
    }


    public void Blank()
    {
        drpCourse.SelectedIndex = 0;
        drpSemester.DataSource = "";
        drpSemester.DataBind();
        drpSection.DataSource = "";
        drpSection.DataBind();
        drpSubject.DataSource = "";
        drpSubject.DataBind();
        txtDueDate.Text = "";
        txtCloseDate.Text = "";
        txtMaximumMarks.Text = "";
        txtSubjectDescription.Text = "";
        drpAcademicYear.SelectedIndex = 0;
        lblSubjectType.Text = "";
    }
    protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSubjectType.Text = FDL.GetSubjectTypebySemester(drpCourse.SelectedValue, drpSemester.SelectedItem.ToString(), drpSubject.SelectedItem.ToString());
    }
    public void BindAssignmentReport()
    {
        SqlCommand cmd = new SqlCommand("proc_GetSubmitAssignment", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        grdAssignmentReport.DataSource = dt;
        grdAssignmentReport.DataBind();
    }
    protected void DownloadInboxFile(object sender, EventArgs e)
    {
        string Eval = (sender as LinkButton).CommandArgument;
        string AssignmentNo = Eval.Split('[', ']')[1];
        string StudentNo = Eval.Split('(', ')')[1];
        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select Attachment,AttachmentType,FileName from tblAttachmentFiles where AssignmentNo_='" + AssignmentNo + "' and StudentCode='" + StudentNo + "'";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Attachment"];
                    contentType = sdr["AttachmentType"].ToString();
                    fileName = sdr["FileName"].ToString();
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
    protected void DownloadOutboxFile(object sender, EventArgs e)
    {
        try
        {
            string AssignmentNo = (sender as LinkButton).CommandArgument;
            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "select Attachment,AttachmentType,FileName from tblAttachmentFiles where AssignmentNo_='" + AssignmentNo + "' and isnull([StudentCode],'')='' ";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Attachment"];
                        contentType = sdr["AttachmentType"].ToString();
                        fileName = sdr["FileName"].ToString();
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
        catch
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
        }
    }
    protected void grdAssignmentReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAssignmentReport.PageIndex = e.NewPageIndex;
        BindAssignmentReport();
    }
    public void SendMail()
    {
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["E-Mail Address"].ToString() != "")
            {
                using (MailMessage mm = new MailMessage("ashutosh.kumar@corporateserve.com", dt.Rows[i]["E-Mail Address"].ToString()))
                {
                    mm.Subject = txtSubjectDescription.Text;
                    mm.Body = "Please submit the assignment on the topic '" + txtSubjectDescription.Text + "'(" + drpSubject.SelectedItem.ToString() + ") befor:" + txtDueDate.Text + "";
                    mm.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("pramoddanu09@gmail.com", "123haldwani");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent.');", true);
                }
            }
        }
    }
    public void SMS(String MobileNo, string Msg)
    {
        //  Website: http://www.universalsmsadvertising.com/     //  Username: 9837016352   //  Password: 9837016352 
        MobileNo = "91" + MobileNo;
        //MobileNo = "91" + 9015762885;
        string url = "http://www.universalsmsadvertising.com/universalsmsapi.php?user_name=9837016352&user_password=9837016352&mobile=" + MobileNo + "&sender_id=TMUniv&type=F&text=" + Msg + "";
        System.Net.HttpWebRequest fr;
        Uri targetURI = new Uri(url);
        fr = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(targetURI);
        if (fr.GetResponse().ContentLength > 0)
        {
            System.IO.StreamReader str = new System.IO.StreamReader(fr.GetResponse().GetResponseStream());
            Response.Write(str.ReadToEnd());
            str.Close();
        }
    }
    public void bindAcademicYear()
    {
        SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da.Fill(dt1);
        drpAcademicYear.DataSource = dt1;
        drpAcademicYear.DataTextField = "Details";
        drpAcademicYear.DataValueField = "No_";
        drpAcademicYear.DataBind();
        // Report Purpose
        drpAcademic1.DataSource = dt1;
        drpAcademic1.DataTextField = "Details";
        drpAcademic1.DataValueField = "No_";
        drpAcademic1.DataBind();
    }
    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubject();
    }
    public void BindAssignmentOutboxReport()
    {
        SqlCommand cmd = new SqlCommand("proc_GetUploadAssignment", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        grdAssignmentOutboxReport.DataSource = dt;
        grdAssignmentOutboxReport.DataBind();
    }
    protected void txtDateFrom_TextChanged(object sender, EventArgs e)
    {
        if (txtDateTo.Text != "")
        {
            BindAssignmentOutboxReport1();
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "fader2", "$('[id$=pnlOutbox]').show();", true);
    }
    protected void txtDateTo_TextChanged(object sender, EventArgs e)
    {
        if (txtDateFrom.Text != "")
        {
            BindAssignmentOutboxReport1();
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "fader3", "$('[id$=pnlOutbox]').show();", true);
    }
    public void BindAssignmentOutboxReport1()
    {
        SqlCommand cmd = new SqlCommand("proc_GetUploadAssignment1", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@DateFrom", txtDateFrom.Text);
        cmd.Parameters.Add("@DateTo", txtDateTo.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        grdAssignmentOutboxReport.DataSource = dt;
        grdAssignmentOutboxReport.DataBind();
    }
    //sandeep 23/12/2016
    public void bindGroupList()
    {
        SqlCommand cmd = new SqlCommand("sp_GetGroup_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        ddlGroup.DataSource = dt;
        ddlGroup.DataTextField = "Details";
        ddlGroup.DataValueField = "No_";
        ddlGroup.DataBind();
    }
    public void bindBatchList()
    {
        SqlCommand cmd = new SqlCommand("sp_GetBatch_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        ddlBatch.DataSource = dt;
        ddlBatch.DataTextField = "Details";
        ddlBatch.DataValueField = "No_";
        ddlBatch.DataBind();
    }

    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void drpAcademic1_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubjectforReport();
    }
    protected void drpCourse1_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSemesterforReport();
    }
    protected void drpSemester1_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubjectforReport();
        BindSectionforReport();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindReport();

    }
    public void BindReport()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetAssignment1", con1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID1", Session["uid"].ToString());
            cmd.Parameters.Add("@academic", drpAcademic1.SelectedValue);
            cmd.Parameters.Add("@Course", drpCourse1.SelectedValue);
            cmd.Parameters.Add("@Sem", drpSemester1.SelectedValue);
            cmd.Parameters.Add("@Subject", ddlSubject.SelectedValue);

            //if (drpAssignmentNo.SelectedIndex > 0)
            //{ cmd.Parameters.AddWithValue("@AssigNo", drpAssignmentNo.SelectedValue); }
            //else { }
            // cmd.Parameters.AddWithValue("@AssigNo", "");


            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            ReportViewer1.Visible = true;
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.Visible = true;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/AssignmentReport.rdlc");
            ReportDataSource datasource = new ReportDataSource("DataSet_Result", dt);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            //grdReport.DataSource = dt;
            //grdReport.DataBind();
        }
        catch (Exception ex) { }
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindAssigment();
        BindReport();
    }
    protected void grdReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        //grdReport.PageIndex = e.NewPageIndex;
        BindReport();

    }
    protected void drpAssignmentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindReport();
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}