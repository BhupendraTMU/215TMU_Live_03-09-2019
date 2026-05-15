using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;

public partial class ViewEmployeeAttendance : System.Web.UI.Page
{
    Connection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            con = new Connection();
            if (Session["uname"].ToString() == null && Session["uid"].ToString()!="TMU00864") 
            {
                Response.Redirect("../Default.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    showHRHODisexhist();
                    BindYear();
                    ddlMonth.SelectedValue = System.DateTime.Now.ToString("MM");
                    ddlYear.SelectedValue = System.DateTime.Now.ToString("yyyy");
                    ShowEMployeeDetails();
                    ViewAttendance();
                   
                }

                // show_Attendence();
            }
        }
        catch (Exception)
        {
            Response.Redirect("../Default.aspx");
        }
    }
    public void BindYear()
    {

        int Currentyear = System.DateTime.Now.Year;

        for (int i = Currentyear - 1; i <= Currentyear; i++)
            ddlYear.Items.Add(i.ToString());
    }

    public void showHRHODisexhist()
    {

        SqlDataReader dr = con.SHow_showHODExhistavv(Session["uid"].ToString(), Session["CompanyTableEmployee"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            dr.Close();
            con.DisConnect();


        }
        else
        {
            dr.Close();
            con.DisConnect();
            Response.Redirect("Default.aspx");
        }
    }

    public void ViewAttendance()
    { 

           string ccnameUN = Session["Company"].ToString();
        string rccname = ccnameUN.Replace(".", "_");
        string tbl_EmployeeActualpunchData = "[" + rccname + "$Employee Actual Punch Data" + "]";

        SqlDataReader dr = con.Show_EmployeeActualPunchData_viewAttendance(tbl_EmployeeActualpunchData, txtUserid.SelectedValue.Trim(), ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        grd_ViewAttendance.DataSource = dt;
        grd_ViewAttendance.DataBind();
        dr.Close();
        con.DisConnect();

    }

    private void ShowEMployeeDetails()
    {

        DataTable dt = new DataTable();
        string id = string.Empty;
        string fname = string.Empty;
        string sname = string.Empty;
        string lname = string.Empty;
        
        string newName = string.Empty;
        string ccnameUN = Session["Company"].ToString();
        string rccname = ccnameUN.Replace(".", "_");
        string tbl_EmployeeActualpunchData = "[" + rccname + "$Employee" + "]";


        string sqlStatement = "SELECT * FROM " + tbl_EmployeeActualpunchData + " where Status=0 order by No_ asc";
        SqlCommand sqlCmd = new SqlCommand(sqlStatement, con.Con);
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);

        sqlDa.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                id = dt.Rows[i]["No_"].ToString();

                fname = dt.Rows[i]["First Name"].ToString();
                sname = dt.Rows[i]["Middle Name"].ToString();
                lname = dt.Rows[i]["Last Name"].ToString();
                newName = id + "  " + fname + "    " + lname +  " "  ;

                txtUserid.Items.Add(new ListItem(newName, id));

                txtUserid.SelectedValue = id;
            }
        }

    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        ViewAttendance();
    }
    protected void grd_ViewAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Label lblStatus = (Label)e.Row.FindControl("lblStatus");

            //Label lblLateEarlyBy = (Label)e.Row.FindControl("lblLateEarlyBy");
            //Label lblLateBY_GRID = (Label)e.Row.FindControl("lblLateBY_GRID");

            string lblLateEarlyBy = e.Row.Cells[6].Text.Trim();


            decimal lblLateEarlyBy1 = Convert.ToDecimal(lblLateEarlyBy.Trim());
            string lblLateEarlyBy2 = lblLateEarlyBy1.ToString("00");



            int min = int.Parse(lblLateEarlyBy2);
            int hr = (min / 60);
            int hr1 = (min % 60);
            
            string str = hr.ToString() + ":" + hr1.ToString();
            e.Row.Cells[6].Text = str;


            ///

            string lblLateBY_GRID = e.Row.Cells[7].Text.Trim();


            decimal lblLateBY_GRID1 = Convert.ToDecimal(lblLateBY_GRID.Trim());
            string lblLateBY_GRID2 = lblLateBY_GRID1.ToString("00");



            int min1 = int.Parse(lblLateBY_GRID2);
            int hr3 = (min1 / 60);
            int hr11 = (min1 % 60);

            string str12 = hr3.ToString() + ":" + hr11.ToString();
            e.Row.Cells[7].Text = str12;

            if (lblStatus.Text == "1")
            {
               // lblStatus.Text = "PP";
                string od="";
                e.Row.BackColor = System.Drawing.Color.DarkSeaGreen;
                e.Row.ForeColor = System.Drawing.Color.Black;
                string ccnameUN = Session["Company"].ToString();
                string rccname = ccnameUN.Replace(".", "_");
                string tble_paydailyAtten = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
                string attendaye=e.Row.Cells[0].Text;
                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, Session["uid"].ToString(), attendaye);
                dr.Read();
                if (dr.HasRows)
                {
                     od = dr["OD"].ToString();
                }
                dr.Close();
                con.DisConnect();
                if (od == "1")
                {
                    lblStatus.Text = "OD";
                }
                if (od == "0")
                {
                    lblStatus.Text = "PP";
                }
            }
            if (lblStatus.Text == "2")
            {
                lblStatus.Text = "HD";
                e.Row.BackColor = System.Drawing.Color.Gray;
                e.Row.ForeColor = System.Drawing.Color.White;
            }
            if (lblStatus.Text == "3")
            {
                lblStatus.Text = "WO";
                e.Row.BackColor = System.Drawing.Color.Gray;
                e.Row.ForeColor = System.Drawing.Color.White;
            }
            if (lblStatus.Text == "4")
            {
                lblStatus.Text = "Leave";
                e.Row.BackColor = System.Drawing.Color.Salmon;
                e.Row.ForeColor = System.Drawing.Color.White;
                 string ccnameUN = Session["Company"].ToString();
                string rccname = ccnameUN.Replace(".", "_");
                string tble_paydailyAtten = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
                string attendaye=e.Row.Cells[0].Text;
                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, Session["uid"].ToString(), attendaye);
                dr.Read();
                if (dr.HasRows)
                {
                    lblStatus.Text = dr["Leave Code"].ToString();
                }
                dr.Close();
                con.DisConnect();
            }
            if (lblStatus.Text == "5")
            {
                lblStatus.Text = "LWP(Half-Day)";
                e.Row.BackColor = System.Drawing.Color.Salmon;
                e.Row.ForeColor = System.Drawing.Color.White;

                string ccnameUN = Session["Company"].ToString();
                string rccname = ccnameUN.Replace(".", "_");
                string tble_paydailyAtten = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
                string attendaye = e.Row.Cells[0].Text;
                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, Session["uid"].ToString(), attendaye);
                dr.Read();
                if (dr.HasRows)
                {
                    lblStatus.Text = dr["Leave Code"].ToString();
                    if (lblStatus.Text.Trim() == "")
                    {
                        lblStatus.Text = "LWPH";
                    }
                }
                dr.Close();
                con.DisConnect();
            }

            if (lblStatus.Text == "6")
            {
                lblStatus.Text = "LWP(Full-Day)";
                e.Row.BackColor = System.Drawing.Color.Salmon;
                e.Row.ForeColor = System.Drawing.Color.White;
                string ccnameUN = Session["Company"].ToString();
                string rccname = ccnameUN.Replace(".", "_");
                string tble_paydailyAtten = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
                string attendaye = e.Row.Cells[0].Text;
                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, Session["uid"].ToString(), attendaye);
                dr.Read();
                if (dr.HasRows)
                {
                    lblStatus.Text = dr["Leave Code"].ToString();
                    if (lblStatus.Text.Trim() == "")
                    {
                        lblStatus.Text = "LWPF";
                    }
                }
                dr.Close();
                con.DisConnect();
            }

            if (lblStatus.Text == "7")
            {
                lblStatus.Text = "Special Leave";
                e.Row.BackColor = System.Drawing.Color.Salmon;
                e.Row.ForeColor = System.Drawing.Color.White;
                string ccnameUN = Session["Company"].ToString();
                string rccname = ccnameUN.Replace(".", "_");
                string tble_paydailyAtten = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
                string attendaye = e.Row.Cells[0].Text;
                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, Session["uid"].ToString(), attendaye);
                dr.Read();
                if (dr.HasRows)
                {
                    lblStatus.Text = dr["Leave Code"].ToString();
                    if (lblStatus.Text.Trim() == "")
                    {
                        lblStatus.Text = "SP";
                    }
                }
                dr.Close();
                con.DisConnect();
            }
            if (lblStatus.Text == "8")
            {
               // lblStatus.Text = "Absent(Half-Day)"; Discussion
                e.Row.BackColor = System.Drawing.Color.Salmon;
                e.Row.ForeColor = System.Drawing.Color.White;


                string ccnameUN = Session["Company"].ToString();
                string rccname = ccnameUN.Replace(".", "_");
                string tble_paydailyAtten = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
                string attendaye = e.Row.Cells[0].Text;
                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, Session["uid"].ToString(), attendaye);
                dr.Read();
                if (dr.HasRows)
                {
                    lblStatus.Text = dr["Leave Code"].ToString();
                    if (lblStatus.Text.Trim() == "")
                    {
                        lblStatus.Text = "AHD";
                    }
                }
                dr.Close();
                con.DisConnect();
            }

            if (lblStatus.Text == "9")
            {
              //  lblStatus.Text = "Absent(Full-Day)"; discussion
                //lblStatus.Text = "AA";
                e.Row.BackColor = System.Drawing.Color.Salmon;
                e.Row.ForeColor = System.Drawing.Color.White;

                string ccnameUN = Session["Company"].ToString();
                string rccname = ccnameUN.Replace(".", "_");
                string tble_paydailyAtten = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
                string attendaye = e.Row.Cells[0].Text;
                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, Session["uid"].ToString(), attendaye);
                dr.Read();
                if (dr.HasRows)
                {
                    lblStatus.Text = dr["Leave Code"].ToString();
                    if (lblStatus.Text.Trim() == "")
                    {
                        lblStatus.Text = "AA";
                    }
                }
                dr.Close();
                con.DisConnect();
            }

            //decimal cel6 =Convert.ToDecimal(e.Row.Cells[6].Text);
            //decimal cel7 = Convert.ToDecimal(e.Row.Cells[7].Text);
            string cel6 = e.Row.Cells[6].Text;
            string cel7 = e.Row.Cells[7].Text;
            if (cel6 != "0:0" )

            {
                e.Row.Cells[6].BackColor = System.Drawing.Color.Pink;
                e.Row.Cells[6].ForeColor = System.Drawing.Color.Black;
            }

            if (cel7 != "0:0")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.Pink;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
            }
            //if (cel6 > cel7)
            //{
            //    e.Row.Cells[6].BackColor = System.Drawing.Color.Red;
            //    e.Row.Cells[6].ForeColor = System.Drawing.Color.White;

            //}

            //if (cel6 < cel7)
            //{

            //    e.Row.Cells[6].BackColor = System.Drawing.Color.Red;
            //    e.Row.Cells[6].ForeColor = System.Drawing.Color.White;

            //}

           
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grd_ViewAttendance.RenderControl(htmlWrite);

        Response.Clear();
        string str = "ActualAttendance" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();

    }
}