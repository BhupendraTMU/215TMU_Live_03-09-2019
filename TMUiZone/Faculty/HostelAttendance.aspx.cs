using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_HostelAttendance : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            if (!IsPostBack)
            {                
                txtFromTime.Text = "19:00";
                txtToTime.Text = "23:00";
                txtDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                BindHostel();
                getData(ddlHostel.SelectedValue, txtDate.Text, txtFromTime.Text, txtToTime.Text);
            }

        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
    }


    public void BindHostel()
    {
        try
        {

            SqlCommand cmd = new SqlCommand("proc_bindHostel", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserId", Session["uid"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            con.Open();
            da.Fill(dt1);
            con.Close();
            ddlHostel.DataSource = dt1;
            ddlHostel.DataTextField = "name";
            ddlHostel.DataValueField = "Code";
            ddlHostel.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string fromTime = Request.Form[txtFromTime.UniqueID];
        string toTime = Request.Form[txtToTime.UniqueID];

        txtFromTime.Text = fromTime;
        txtToTime.Text = toTime;
        getData(ddlHostel.SelectedValue, txtDate.Text, txtFromTime.Text, txtToTime.Text);
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
    private void getData(string Hostel, string date, string fromtime, string totime)
    {
        try

        {
            SqlCommand cmd = new SqlCommand("GetAttendanceNewDevBhupii", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@HostelCode", Hostel);
            cmd.Parameters.Add("@AttDate", date);
            cmd.Parameters.Add("@fromtime", fromtime);
            cmd.Parameters.Add("@totime", totime);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                JainStudentList.DataSource = ds.Tables[0];
                JainStudentList.DataBind();
               
            }
            else
            {
                JainStudentList.DataSource = "";
                JainStudentList.DataBind();
               
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                lblTotalHosteller.Text = ds.Tables[1].Rows[0]["TOTAL"].ToString();
                lblPresent.Text = ds.Tables[1].Rows[0]["PRESENT"].ToString();
                lblTotalAbsent.Text = ds.Tables[1].Rows[0]["ABSENT"].ToString();
                lblHL.Text = ds.Tables[1].Rows[0]["HL"].ToString();
            }
            else
            {
                lblTotalHosteller.Text = "";
                lblPresent.Text = "";
                lblTotalAbsent.Text = "";
                lblHL.Text = "";

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Required for GridView Export
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        JainStudentList.Columns[2].Visible = false;

        Response.Clear();
        string str = "Hostel_Attendance_" + txtDate.Text + "_" + ddlHostel.SelectedValue;
        Response.AddHeader("content-disposition", "attachment;filename=" + str + ".xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";

        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        /* ================= HEADER SECTION ================= */
        sw.Write("<table border='1' style='width:100%; font-weight:bold;'>");
        sw.Write("<tr>");
        sw.Write("<td colspan='4' style='text-align:center; font-size:16px;'>Hostel Attendance Report</td>");
        sw.Write("</tr>");

        sw.Write("<tr>");
        sw.Write("<td>Total Hosteller</td>");
        sw.Write("<td>" + lblTotalHosteller.Text + "</td>");
        sw.Write("<td>Present</td>");
        sw.Write("<td>" + lblPresent.Text + "</td>");
        sw.Write("</tr>");

        sw.Write("<tr>");
        sw.Write("<td>Absent</td>");
        sw.Write("<td>" + lblTotalAbsent.Text + "</td>");
        sw.Write("<td>HL</td>");
        sw.Write("<td>" + lblHL.Text + "</td>");
        sw.Write("</tr>");

        sw.Write("</table><br/>");
        /* ================= END HEADER ================= */

        // Render GridView
        JainStudentList.RenderControl(hw);

        // Write output
        Response.Write(sw.ToString());
        Response.End();


    }
    protected void btnPresent_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "ManualPresent")
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            // Read values from GridView row
            string fingerNo = ((Label)row.FindControl("lblFingerNo")).Text;
            string no_ = ((Label)row.FindControl("lblNo_")).Text;
            string phone = ((Label)row.FindControl("lblPhoneNo")).Text;
            string student = ((Label)row.FindControl("lblStudentName")).Text;
            string course = ((Label)row.FindControl("lblCourseName")).Text;
            string hostel = ((Label)row.FindControl("lblHostelCode")).Text;
            string roomNo = ((Label)row.FindControl("lblRoomNo_")).Text;
            string roomType = ((Label)row.FindControl("lblRoomType")).Text;
            string punchTime = ((Label)row.FindControl("lblPunchTime")).Text;
            string status = "PRESENT";
            string userid = Session["uid"].ToString();
            string date = txtDate.Text;
            InsertManualAttendance(
                fingerNo, no_, phone, student,
                course, hostel, roomNo, roomType,
                punchTime, status, userid, date
            );

            // Optional: refresh grid
            getData(ddlHostel.SelectedValue, txtDate.Text, txtFromTime.Text, txtToTime.Text);
        }
    }
    private void InsertManualAttendance(
    string fingerNo, string no_, string phone, string student,
    string course, string hostel, string roomNo, string roomType,
    string punchTime, string status,string userid,string date)
    {
        using (SqlConnection con = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {
            string sql = @"
        INSERT INTO StudentAttendanceManual
        ([Finger No_], [No_], [Phone Number], [Student Name],
         [Course Code], [Hostel Code], [Room No_], [Room Type],
         [Punch Time], [Status], [Create Date],Createby)
        VALUES
        (@FingerNo, @No, @Phone, @Student,
         @Course, @Hostel, @RoomNo, @RoomType,
         @PunchTime, @Status, @date,@userid)";

            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@FingerNo", fingerNo);
                cmd.Parameters.AddWithValue("@No", no_);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Student", student);
                cmd.Parameters.AddWithValue("@Course", course);
                cmd.Parameters.AddWithValue("@Hostel", hostel);
                cmd.Parameters.AddWithValue("@RoomNo", roomNo);
                cmd.Parameters.AddWithValue("@RoomType", roomType);

                // Handle Punch Time safely
                DateTime pt;
                if (DateTime.TryParse(punchTime, out pt))
                    cmd.Parameters.AddWithValue("@PunchTime", pt);
                else
                    cmd.Parameters.AddWithValue("@PunchTime", DBNull.Value);

                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@userid", userid);
                cmd.Parameters.AddWithValue("@date", date);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

}