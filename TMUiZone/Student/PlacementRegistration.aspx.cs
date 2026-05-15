using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class PlacementRegistration : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["uid"] == "" || Session["AcademicYear"].ToString() == "")
            {
                Response.Redirect("~/default.aspx");
            }
        }

        catch { Response.Redirect("~/default.aspx"); }

        if (!IsPostBack)
        {
            getplacementschedule();

        }

    }
    public void getplacementschedule()
    {
        try
        {
            DataTable dt = new DataTable();
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_GetPlacementRegistrationDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", Session["uid"].ToString());
            cmd.Parameters.Add("@AcademicYear", Session["AcademicYear"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Close();
            da.Fill(dt);
            // dt = CommanCsfile.GetDataTable("sp_GetPlacementRegistrationDetails '" + Session["EnrollmentNo"].ToString() + "','" + Session["AcademicYear"].ToString() + "'");
            grdplacementform.DataSource = dt;
            grdplacementform.DataBind();
            Btnsubmit.Visible = false;
            if (dt.Rows.Count > 0)
            {

                foreach (GridViewRow gvrow in grdplacementform.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("chkbox");
                    if (chk.Enabled == true)
                    {
                        Btnsubmit.Visible = true;
                        return;
                    }
                    else
                    {
                    }

                }

            }
            else { Btnsubmit.Visible = false; }
        }
        catch(Exception ex)
        {
        }

    }

    protected void chkbox_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void Btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            bool selected = false; 
            DataTable dt = new DataTable(); 
            con.Open();
            SqlCommand cmdReg = new SqlCommand("sp_GetRegistrationNo", con);
            cmdReg.CommandType = CommandType.StoredProcedure;
            cmdReg.Parameters.Add("@UserID", Session["uid"].ToString());
            cmdReg.Parameters.Add("@AcademicYear", Session["AcademicYear"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmdReg);
            con.Close();
            da.Fill(dt);
            string RegistrationNoSeries = dt.Rows[0][0].ToString();
            if (RegistrationNoSeries =="") 
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Number series not generated contact admin.')", true);
                return;
            }
         //   string RegistrationNoSeries = CommanCsfile.ResultNav("sp_GetRegistrationNo '" + Session["EnrollmentNo"].ToString() + "','" + Session["AcademicYear"].ToString() + "'");
            foreach (GridViewRow gvrow in grdplacementform.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkbox");
                if (chk.Checked == true && chk.Enabled == true)
                {
                    selected = true;
                    int i = gvrow.RowIndex;// grdplacementform.SelectedIndex;
                    HiddenField hfcompanyCode = (HiddenField)grdplacementform.Rows[i].FindControl("hfcompanyCode");
                    Label lblcompanyName = (Label)grdplacementform.Rows[i].FindControl("lblcompanyname");
                    HiddenField hfScheduleNo = (HiddenField)grdplacementform.Rows[i].FindControl("hfScheduleno");
                   // SqlConnection con = CommanCsfile.getConnection();
                    SqlCommand cmd1 = new SqlCommand("sp_Qulificationcritarea", con);
                    con.Open();
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@AcademicYear", Session["AcademicYear"].ToString());
                    cmd1.Parameters.AddWithValue("@CourseCode", Session["CourseCode"].ToString());
                    cmd1.Parameters.AddWithValue("@ScheduleNo", hfScheduleNo.Value);                    
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataTable dtQuli = new DataTable();
                    da1.Fill(dtQuli);
                    con.Close();
                    if (dtQuli.Rows.Count> 0)
                    {
                        for (int j = 0; j < dtQuli.Rows.Count; j++)
                        {
                            SqlCommand cmd2 = new SqlCommand("sp_CheckQulificationcritarea", con);
                            con.Open();
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Parameters.AddWithValue("@AcademicYear", Session["AcademicYear"].ToString());
                            cmd2.Parameters.AddWithValue("@QulificationCode", dtQuli.Rows[j]["Qualification"].ToString());
                            cmd2.Parameters.AddWithValue("@Percentage", dtQuli.Rows[j]["Evaluation Marks _"].ToString());
                            cmd2.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
                            cmd2.Parameters.AddWithValue("@ScheduleNo", hfScheduleNo.Value);
                            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                            DataTable dtQulicheck = new DataTable();
                            da2.Fill(dtQulicheck);
                            con.Close();
                            if (dtQulicheck.Rows.Count > 0)
                            {

                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You are not eligible for the desired job placement')", true);
                                return;
                            }
                        }
                    }
                    else
                    {
                       // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You are not eligible for the desired job placement')", true);
                       // return;
                    }

                    SqlCommand cmd = new SqlCommand("sp_RegistrationForPlacement", con);
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RegistrationNoSeries", RegistrationNoSeries);
                    cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
                    cmd.Parameters.AddWithValue("@StudentName", Session["Name"].ToString());
                    cmd.Parameters.AddWithValue("@AcademicYear", Session["AcademicYear"].ToString());
                    cmd.Parameters.AddWithValue("@CompanyCode", hfcompanyCode.Value);
                    cmd.Parameters.AddWithValue("@CompanyName", lblcompanyName.Text);
                    cmd.Parameters.AddWithValue("@ScheduleNo", hfScheduleNo.Value);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                }
            }
            if (selected == false)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Selection required for record Submission.')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Registration Completed.')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Error During Data submission!!!')", true);
        }
        getplacementschedule();
    }

    protected void lnkbtnJobDesciption_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = sender as LinkButton;
            GridViewRow grow = btn.NamingContainer as GridViewRow;
            lblCompany.Text = (grow.FindControl("lblcompanyname") as Label).Text;
            string chk = (grow.FindControl("hfScheduleNo") as HiddenField).Value;
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("sp_GetJobDescriptions", con);
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ScheduleNo", chk);
            cmd.Parameters.AddWithValue("@CourseCode", Session["CourseCode"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Close();
            da.Fill(dt);
            // dt = CommanCsfile.GetDataTable("sp_GetJobDescriptions '" + chk + "'");
            dtAlertJobDescriptionMsg.DataSource = dt;
            dtAlertJobDescriptionMsg.DataBind();
            hlnk.Text = dt.Rows[0]["Link"].ToString();
            hlnk.Attributes.Add("href", dt.Rows[0]["Link"].ToString());
            hlnk.Attributes.Add("target", "_blank");
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal').modal('show');</script>", false);
        }
        catch
        {

        }
    }

    protected void grdplacementform_RowDataBound(object sender, GridViewRowEventArgs e)
    {

            DataTable dtQuli = new DataTable();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               string customerId = (e.Row.FindControl("hfScheduleNo") as HiddenField).Value;
               
               CheckBox chk = (CheckBox)e.Row.FindControl("chkbox");

                SqlCommand cmd1 = new SqlCommand("sp_Qulificationcritarea", con);
                con.Open();
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@AcademicYear", Session["AcademicYear"].ToString());
                cmd1.Parameters.AddWithValue("@CourseCode", Session["CourseCode"].ToString());
                cmd1.Parameters.AddWithValue("@ScheduleNo", customerId);                    

                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                              

                da1.Fill(dtQuli);
                con.Close();
                if (dtQuli.Rows.Count > 0)
                {
                    for (int j = 0; j < dtQuli.Rows.Count; j++)
                    {
                        SqlCommand cmd2 = new SqlCommand("sp_CheckQulificationcritarea", con);
                        con.Open();
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.AddWithValue("@AcademicYear", Session["AcademicYear"].ToString());
                        cmd2.Parameters.AddWithValue("@QulificationCode", dtQuli.Rows[j]["Qualification"].ToString());
                        cmd2.Parameters.AddWithValue("@Percentage", dtQuli.Rows[j]["Evaluation Marks _"].ToString());
                        cmd2.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
                        cmd2.Parameters.AddWithValue("@ScheduleNo", customerId);
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                        DataTable dtQulicheck = new DataTable();
                        da2.Fill(dtQulicheck);
                        con.Close();
                        if (dtQulicheck.Rows.Count > 0)
                        {

                        }
                        else
                        {
                            e.Row.Cells[0].BackColor = System.Drawing.Color.Red;                           
                            chk.Enabled = false;
                        }
                    }
                }
                else
                {
                    // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You are not eligible for the desired job placement')", true);
                    // return;
                }



            }
        }
        
    
}

