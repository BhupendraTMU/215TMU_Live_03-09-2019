using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Faculty_AssignmentUploadView : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]);
    DL.FacultyPortalDL FDL = new DL.FacultyPortalDL();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Label2.Text = Session["GlobalDimension1Code"].ToString();
        try
        {
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {
                    if ((this.Master as IndexMaster).GetLinkYesNo("AssignmentUploadView") == "True")
                    {
                        BindCourse();
                        bindsubject();
                        bindfaculty();

                    }
                    else
                    { Response.Redirect("~/Default.aspx"); }
                } 
                

            }
        }
        catch
        {
            Response.Redirect("../Default.aspx"); 
        }
    }
    public void BindCourse()
    {
        string proc = "Sp_GetCourseRoleWise_HOD_Role_ForAssignUpload";
        if (Session["GlobalDimension1Code"].ToString() == "TMDC" || Session["GlobalDimension1Code"].ToString() == "TMMC")
        {
            proc = "Sp_GetCourseRoleWise_HOD_Role_ForAssignUploadMD";
        }
        SqlCommand cmd = new SqlCommand(proc, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@userid", Session["uid"].ToString());
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        DrpCourse.DataSource = dt;
        DrpCourse.DataTextField = "No_";
        DrpCourse.DataValueField = "No_";
        DrpCourse.DataBind();
    }
 
    
    
    //select  distinct SubjectCode,SubjectDescription from [HRMSPortal].[dbo].[tblAttachmentFiles] where [HRMSPortal].[dbo].[tblAttachmentFiles].[CourseCode]='MCA-001'

    public void bindsubject()
    {
        using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {
            //SqlCommand cmd = new SqlCommand("select  distinct SubjectCode,SubjectDescription from [HRMSPortal].[dbo].[tblAttachmentFiles] where [HRMSPortal].[dbo].[tblAttachmentFiles].[CourseCode]='" + DrpCourse.SelectedValue + "'", Conn);
            string proc = "Sp_GetSubjectRoleWise_HOD_Role_ForAssignUpload";
            if (Session["GlobalDimension1Code"].ToString() == "TMDC" || Session["GlobalDimension1Code"].ToString() == "TMMC")
            {
                proc = "Sp_GetSubjectRoleWise_HOD_Role_ForAssignUploadMD";
            }
            SqlCommand cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CourseCode",DrpCourse.SelectedValue);
            cmd.Parameters.Add("@UserId", Session["uid"].ToString());
            cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            //cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            DrpSubject.DataSource = dt1;
            DrpSubject.DataTextField = "SubjectDescription";
            DrpSubject.DataValueField = "SubjectCode";
            DrpSubject.DataBind();
            DrpSubject.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));
        }
    }

    //select distinct FacultyCode,FacultyName from [HRMSPortal].[dbo].[tblAttachmentFiles] where CourseCode='MCA-001' and SubjectCode='MCA 402';
    public void bindfaculty()
    {
        using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("select distinct FacultyCode,FacultyName from [HRMSPortal].[dbo].[tblAttachmentFiles] where CourseCode='" + DrpCourse.SelectedValue + "' and SubjectCode='" + DrpSubject.SelectedValue + "';", Conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            DrpFaculty.DataSource = dt1;
            DrpFaculty.DataTextField = "FacultyName";
            DrpFaculty.DataValueField = "FacultyCode";
            DrpFaculty.DataBind();
            DrpFaculty.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));
        }
    }

    public void gridbind()
    {
        string str = Session["GlobalDimension1Code"].ToString();
        SqlCommand cmd = new SqlCommand("getassigmentdetailview", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@coursecode", DrpCourse.SelectedValue);
        cmd.Parameters.AddWithValue("@subjectcode", DrpSubject.SelectedValue);
        cmd.Parameters.AddWithValue("@faculaticode", DrpFaculty.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        grdAssignmentOutboxReport.DataSource = dt;
        grdAssignmentOutboxReport.DataBind();
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
    public void bindgridfor(string str)
    {
       // select *,convert(varchar,UploadDate,106) as UploadDate1,convert(varchar,DueDate,106) as DueDate1 from tblAttachmentFiles where FacultyCode=@faculaticode and  CourseCode=@coursecode and SubjectCode=@subjectcode and  Attachment is not null  order by UploadDate desc

        using (SqlConnection Conn = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            SqlCommand cmd = new SqlCommand("select CourseCode,case isnull(Semester,'') when '' then [Year] else isnull(Semester,'') end as [Sem Year],Section,SubjectDescription,AssignmentNo_,convert(varchar,UploadDate,106) as UploadDate1,convert(varchar,DueDate,106) as DueDate1 from tblAttachmentFiles where " + str + " and  Attachment is not null  order by UploadDate desc", Conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            grdAssignmentOutboxReport.DataSource = dt1;
            grdAssignmentOutboxReport.DataBind();
           
        }


    }
    protected void DrpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
       bindsubject();
    }
    protected void DrpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindfaculty();
    }
protected void btnshow_Click(object sender, EventArgs e)
{
    string quer = "";
    if (DrpCourse.Text != "-- Select --")
    {
        quer = "and CourseCode ='" + DrpCourse.SelectedValue + "'";
    }
    if (DrpSubject.Text != "-- Select --")
    {
        quer += " and SubjectCode='"+DrpSubject.SelectedValue+"'";
    }

    if (DrpFaculty.Text != "-- Select --")
    {
        quer += "and FacultyCode='" + DrpFaculty.SelectedValue + "'";
    }



    if (quer.Length != 0)
    {
        quer = quer.Remove(0, 4);
        bindgridfor(quer);
    }
    else 
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert(' Please Select Any Option !');", true);
      //  gridbind();
    }

    //
}
}