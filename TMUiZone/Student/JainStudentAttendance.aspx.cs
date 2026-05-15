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
using System.Net;
using System.Net.Mail;


public partial class Student_JainStudentAttendance : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                JainStudentDivShow();
                BindAcademicYear();
                Bindyearlist();
                int currentYear = DateTime.Now.Year;
                int currentMonth = DateTime.Now.Month;
                ddlYear.SelectedValue = currentYear.ToString();
                ddlMonth.SelectedValue = currentMonth.ToString();
                getDate();
            }

        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
    }

   

    private void Bindyearlist()
    {
        int currentYear = DateTime.Now.Year;
        List<int> yearsList = new List<int>();
        for (int i = 0; i <= 5; i++)
        {
            yearsList.Add(currentYear - i);
        }
        ddlYear.DataSource = yearsList;
        ddlYear.DataBind();
    }
    public void BindAcademicYear()
    {
        try
        {

            SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            con.Open();
            da.Fill(dt1);
            con.Close();
            ddlAcademicYear.DataSource = dt1;
            ddlAcademicYear.DataTextField = "Details";
            ddlAcademicYear.DataValueField = "No_";
            ddlAcademicYear.DataBind();
        }
        catch
        {
        }
    }
    public void JainStudentDivShow()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        {
            string strSQL = ("SELECT Religion,[Enrollment No_],[Student Name],No_ FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$Student - COLLEGE] where No_='"+ Session["uid"].ToString() + "'");
            {
                SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Religion"].ToString()=="JAIN")
                    {
                        Label3.Text = "Jain Student Attendance";
                        JainStudent.Visible = true;
                    }
                    else
                    {
                        Label3.Text = "You are not authorized for this page.";
                        JainStudent.Visible = false;
                    }
                }
                else
                {
                    Label3.Text = "You are not authorized for this page.";
                    JainStudent.Visible = false;
                }

            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        getDate();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
             string status = DataBinder.Eval(e.Row.DataItem, "AttendanceStatus").ToString();

             if (status == "A")
            {
                e.Row.BackColor = System.Drawing.Color.LightSalmon; 

            }
            else
            {
                e.Row.BackColor = System.Drawing.Color.LightGray; 
            }
        }
    }
    private void getDate()
    {
        try

        {
            SqlCommand cmd = new SqlCommand("Get_JainStudentViewStudent", con1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Session", ddlAcademicYear.SelectedValue.ToString());
            cmd.Parameters.Add("@Month", ddlMonth.SelectedValue.ToString());
            cmd.Parameters.Add("@Year", ddlYear.SelectedValue.ToString());
            cmd.Parameters.Add("@EnrollmentNo", Session["uid"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                DataColumnCollection columns = dt.Columns;
                DataColumn totalPresentColumn = new DataColumn("TotalPercentage", typeof(int));  // Type can be adjusted
                dt.Columns.Add(totalPresentColumn);
                foreach (DataRow row in dt.Rows)
                {
                    Double val1 = Double.Parse(row["Total_Present"].ToString());
                    Double val2 = Double.Parse(row["Total_Days"].ToString());
                    Double val3 = val1 / val2 * 100;
                    row["TotalPercentage"] = val3;
                }
                    columns.Cast<DataColumn>().OrderBy(col => col.ColumnName).ToList();
                JainStudentList.DataSource = dt;
                JainStudentList.DataBind();
            }
else 
{
JainStudentList.DataSource = "";
                JainStudentList.DataBind();
}
        }
        catch (Exception ex)
        {
            DataTable dt = new DataTable();
            JainStudentList.DataSource = dt;
            JainStudentList.DataBind();
        }
    }
    protected void lblDel_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
            int index = row.RowIndex;

            Label lblFingerNo = (Label)JainStudentList.Rows[index].FindControl("lblFingerNo");



            JainStudentListDetail(Session["uid"].ToString(), lblFingerNo.Text, 0, "0");
            GridViewDetails.Show();
        }
        catch (Exception ex) { }
    }
    public void JainStudentListDetail(string str, string lblFingerNo, int i, string EventType)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("[Get_JainStudentAttendanceDetailsBySTNo]", con1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Session", ddlAcademicYear.SelectedValue.ToString());
            cmd.Parameters.Add("@Month", ddlMonth.SelectedValue.ToString());
            cmd.Parameters.Add("@Year", ddlYear.SelectedValue.ToString());
            cmd.Parameters.Add("@EnrollmentNo", Session["uid"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                grdAttandanceDetails.DataSource = dt;

                grdAttandanceDetails.DataBind();
            }


        }
        catch (Exception ex) { }
    }

    //public void DownloadAttachment(string AttachmentName)
    //{
    //    if (AttachmentName == "All")
    //    {
    //        byte[] bytes;
    //        string fileName, contentType = "";
    //        string sConn = ConfigurationManager.ConnectionStrings["TMUCON"].ToString();
    //        SqlConnection objConn = new SqlConnection(sConn);
    //        objConn.Open();
    //        string sTSQL = "select Attachment,[Content Type],[File Name],(Select Top 1 [Student Name] from [TMU$Student - COLLEGE] where No_=EnrollmentNo) as 'Student Name' from tbl_MDMSAttachment   where EnrollmentNo=(Select Top 1 [No_] from [TMU$Student - COLLEGE] where [Enrollment No_]='" + hdfApplicationNo.Value + "' )";
    //        SqlCommand objCmd = new SqlCommand(sTSQL, objConn);
    //        objCmd.CommandType = CommandType.Text;
    //        SqlDataAdapter adapter = new SqlDataAdapter();
    //        DataTable dt = new DataTable();
    //        adapter.SelectCommand = objCmd;
    //        adapter.Fill(dt);
    //        objConn.Close();
    //        string clientName = "Test";
    //        string path = @"C:" + "//" + clientName + "//" + dt.Rows[0]["Student Name"].ToString();
    //        if (!Directory.Exists(path))
    //        {
    //            Directory.CreateDirectory(path);
    //        }
    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            contentType = dt.Rows[i]["Content Type"].ToString();
    //            string filename = dt.Rows[i]["File Name"].ToString();
    //            object img = dt.Rows[i]["Attachment"];
    //            System.IO.File.WriteAllBytes(path + "//" + filename + "", (byte[])img);
    //        }
    //    }
    //    else
    //    {
    //        byte[] bytes;
    //        string fileName, contentType;
    //        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["str"]))
    //        {
    //            using (SqlCommand cmd2 = new SqlCommand())
    //            {
    //                cmd2.CommandText = "select Attachment,[Content Type],[File Name] from tbl_MDMSAttachment   where EnrollmentNo='" + hdfApplicationNo.Value + "' and [Document name]='" + AttachmentName + "'  ";
    //                cmd2.Connection = con2;
    //                con2.Open();
    //                using (SqlDataReader sdr = cmd2.ExecuteReader())
    //                {
    //                    if (!sdr.HasRows)
    //                    {
    //                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Document not found')", true);
    //                        return;
    //                    }
    //                    sdr.Read();
    //                    bytes = (byte[])sdr["Attachment"];
    //                    contentType = sdr["Content Type"].ToString();
    //                    fileName = sdr["File Name"].ToString();

    //                }
    //                con.Close();
    //            }
    //        }
    //        Response.Clear();
    //        Response.Buffer = true;
    //        Response.Charset = "";
    //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //        Response.ContentType = contentType;
    //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
    //        Response.BinaryWrite(bytes);
    //        Response.Flush();
    //        Response.End();
    //    }
    //}


    //protected void btnDownload_Click(object sender, EventArgs e)
    //{
    //    byte[] bytes;
    //    string fileName, contentType = "";
    //    string sConn = ConfigurationManager.ConnectionStrings["TMUCON"].ToString();
    //    SqlConnection objConn = new SqlConnection(sConn);
    //    objConn.Open();
    //    string sTSQL = "select Attachmentdata,AttachmentFileType,AttachmentFilename,concat(UserID,From_Date) as name from HRMSPortal.dbo.tble_Leave_Approval where Leave_Type='AL' and From_Date>='2024-07-01' and To_Date<='2025-06-30' and isnull(AttachmentFilename,'')!='' and AttachmentFilename!='??. ???? ????? ??????????.pdf'";
    //    SqlCommand objCmd = new SqlCommand(sTSQL, objConn);
    //    objCmd.CommandType = CommandType.Text;
    //    SqlDataAdapter adapter = new SqlDataAdapter();
    //    DataTable dt = new DataTable();
    //    adapter.SelectCommand = objCmd;
    //    adapter.Fill(dt);
    //    objConn.Close();
    //    string clientName = "Test";
    //    string path = @"C:" + "//" + clientName + "//" + dt.Rows[0]["name"].ToString();
    //    if (!Directory.Exists(path))
    //    {
    //        Directory.CreateDirectory(path);
    //    }
    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        try
    //        {
    //            contentType = dt.Rows[i]["AttachmentFileType"].ToString();
    //            string filename = dt.Rows[i]["AttachmentFilename"].ToString();
    //            object img = dt.Rows[i]["Attachmentdata"];
    //            System.IO.File.WriteAllBytes(path + "//" + filename + "", (byte[])img);
    //        }
    //        catch(Exception ex)
    //        {

    //        }
    //    }
    //}
}