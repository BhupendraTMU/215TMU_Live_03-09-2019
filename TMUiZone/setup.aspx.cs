using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
public partial class createlevel : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal con;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Portalcon = new Connection();
            con = new ServicePoratal();
            if (Session["UserType"].ToString() == "1")
            {

                ShowApprVisible();

                showPriority();
                if (!IsPostBack)
                {
                   
                   
                    showoptionforsmtp();
                    
                }
                ShowEmailApr();
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
        catch (Exception)
        {
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }
    }

    protected void lnkCreateLevel_Click(object sender, EventArgs e)
    {
        pnlReimbursementApproval.Visible = false;
       
        pnlCreate.Visible = true;
       
        pnlForAttendencesetup.Visible = false;
        pnlLeavesetup.Visible = false;
        pnlMailSetup.Visible = false;
        pnlReimTypeMaster.Visible = false;
        showPriority();
    }
    protected void lnkreimbursment_Click(object sender, EventArgs e)
    {
        pnlReimbursementApproval.Visible = false;
      
        pnlReimTypeMaster.Visible = false;
        pnlCreate.Visible = false;
      
        pnlLeavesetup.Visible = false;
        pnlForAttendencesetup.Visible = false;
       
        pnlMailSetup.Visible = false;
        //clear();
   
        //salaryBasedDisable();
       


        //showReimBursment();
        
    }

   




  



   



   


    //public void showReimbusrmentwithSearch()
    //{
    //    SqlDataReader dr = con.Show_setupReimbursmentWithSearch(txtSearch.Text, Session["Company"].ToString());

    //    DataTable Dt = new DataTable();
    //    Dt.Load(dr);
    //    grdReimbursment.DataSource = Dt;
    //    grdReimbursment.DataBind();
    //    dr.Close();
    //    con.DisConnect();
    //    pnlSearch.Visible = true;

    //}


    //public void showReimbusrmentwithSearchINDV()
    //{
    //    SqlDataReader dr = con.Show_setupReimbursmentWithSearchINDV(txtSearch.Text, Session["Company"].ToString());

    //    DataTable Dt = new DataTable();
    //    Dt.Load(dr);
    //    grdEXpenseForIND.DataSource = Dt;
    //    grdEXpenseForIND.DataBind();
    //    dr.Close();
    //    con.DisConnect();
    //    pnlSearch.Visible = true;

    //}
    //public void show_Department()
    //{
    //    string tble_Name = "[" + Session["Company"] + "$" + "Department Master]";
    //    SqlDataReader dr = Portalcon.Show_Department(tble_Name);
    //    ddDepartment.DataSource = dr;
    //    ddDepartment.DataTextField = "Department Description";
    //    ddDepartment.DataValueField = "Department Code";
    //    ddDepartment.DataBind();
    //    dr.Close();
    //    Portalcon.DisConnect();

    //}

    //public void show_Location()
    //{
    //    string tble_Name = "[" + Session["Company"] + "$" + "Location Master]";
    //    SqlDataReader dr = Portalcon.Show_Location(tble_Name);
    //    ddLocation.DataSource = dr;
    //    ddLocation.DataTextField = "Description";
    //    ddLocation.DataValueField = "Location Code";
    //    ddLocation.DataBind();
    //    dr.Close();
    //    Portalcon.DisConnect();

    //}


    //public void show_BranchName()
    //{
    //    string tble_Name = "[" + Session["Company"] + "$" + "Pay Branch]";
    //    SqlDataReader dr = Portalcon.Show_Branch(tble_Name);
    //    ddBranch.DataSource = dr;
    //    ddBranch.DataTextField = "Name";
    //    ddBranch.DataValueField = "Code";
    //    ddBranch.DataBind();
    //    dr.Close();
    //    Portalcon.DisConnect();

    //}

    //public void show_Grade()
    //{
    //    string tble_Name = "[" + Session["Company"] + "$" + "Pay Grade]";
    //    SqlDataReader dr = Portalcon.Show_Location(tble_Name);
    //    ddGrade.DataSource = dr;
    //    ddGrade.DataTextField = "Designation Code";
    //    ddGrade.DataValueField = "Designation Code";
    //    ddGrade.DataBind();
    //    dr.Close();
    //    Portalcon.DisConnect();

    //}


    ////public void show_Designation()
    ////{
    ////    string tble_Name = "[" + Session["Company"] + "$" + "Job Titles]";
    ////    SqlDataReader dr = Portalcon.Show_Designation(tble_Name);
    ////    ddDesignation.DataSource = dr;
    ////    ddDesignation.DataTextField = "Description";
    ////    ddDesignation.DataValueField = "Job Code";
    ////    ddDesignation.DataBind();
    ////    dr.Close();
    ////    Portalcon.DisConnect();
    ////}

    //public void show_Designation()
    //{
    //    string tble_Name = "[" + Session["Company"] + "$" + "Occupation]";
    //    SqlDataReader dr = Portalcon.Show_Designation(tble_Name);
    //    ddDesignation.DataSource = dr;
    //    ddDesignation.DataTextField = "Description";
    //    ddDesignation.DataValueField = "Code";
    //    ddDesignation.DataBind();
    //    dr.Close();
    //    Portalcon.DisConnect();
    //}

    string slarybasedopt = "";
   
    string Salary_Based_Type = ""; string Applicable_For = ""; string Applicable_UserID = "";
    string Departmentwise2 = ""; string Locationwise2 = ""; string Bandwise2 = ""; string Gradewise2 = ""; string Enddatewise2 = ""; string BranchWise2 = "";
   
    string DepartmentwiseAlledit = "";
    string LocationwiseAlledit = "";
    string BandwiseAlledit = "";
    string GradewiseAlledit = "";
    string EnddatewiseAlledit = "";
 

   
    string slarybasedoptup = "";
   
    public void showPriority()
    {
        SqlDataReader dr = con.Show_tblesetupforpriority1(Session["Company"].ToString());
        DataTable dt = new DataTable();
        dt.Load(dr);
        grdPriority.DataSource = dt;
        grdPriority.DataBind();
        dr.Close();
        con.DisConnect();

    }

    int blank; int hod; int hr; int hrp; int hodp; string EmailBlank = ""; string EmailHR = ""; string EmailHOD = "";
    protected void btnsetup_Click(object sender, EventArgs e)
    {

        if (chkBlank.Checked == true)
        {
            blank = 1;
        }
        if (chkBlank.Checked == false)
        {
            blank = 0;
        }
        if (CHKHOD.Checked == true)
        {
            hod = 1;
        }
        if (CHKHOD.Checked == false)
        {
            hod = 0;
        }
        if (chkHR.Checked == true)
        {
            hr = 1;
        }
        if (chkHR.Checked == false)
        {
            hr = 0;
        }
        if (ddPriority.Text == "HOD")
        {
            hodp = 1;
            hrp = 0;
        }
        if (ddPriority.Text == "HR")
        {
            hodp = 0;
            hrp = 1;
        }
        if (chkNONEemail.Checked == true)
        {
            EmailBlank = "True";
        }
        if (chkNONEemail.Checked == false)
        {
            EmailBlank = "false";
        }

        if (chkHREmail.Checked == true)
        {
            EmailHR = "True";
        }
        if (chkHREmail.Checked == false)
        {
            EmailHR = "false";
        }

        if (chkHODEmail.Checked == true)
        {
            EmailHOD = "True";
        }
        if (chkHODEmail.Checked == false)
        {
            EmailHOD = "false";
        }

        if (chkHR.Checked == true && CHKHOD.Checked == true)
        {
            if (ddPriority.Text == "-----")
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please select first priority');", true);
            }
            else
            {
                SqlDataReader dr = con.Show_tblesetupforpriority(ddType.Text, Session["Company"].ToString());
                dr.Read();
                if (dr.HasRows)
                {
                    dr.Close();
                    con.DisConnect();
                    con.Update_tblesetupforpriority(hod, hr, blank, hodp, hrp, ddType.SelectedValue.ToString(), Session["Company"].ToString(), EmailBlank, EmailHR, EmailHOD);
                    con.DisConnect();
                }
                else
                {
                    dr.Close();
                    con.DisConnect();
                    con.Insert_tblesetupforpriority(hod, hr, blank, hodp, hrp, ddType.SelectedValue.ToString(), Session["Company"].ToString(), EmailBlank, EmailHR, EmailHOD);
                    con.DisConnect();
                }
                showPriority();
            }
        }

        if (chkHR.Checked != true && CHKHOD.Checked == true)
        {

            if (chkHR.Checked == true && CHKHOD.Checked != true)
            {
                ddPriority.Text = "HR";
                hrp = 1;
                hodp = 0;
            }
            if (chkHR.Checked != true && CHKHOD.Checked == true)
            {
                ddPriority.Text = "HOD";
                hrp = 0;
                hodp = 1;
            }

            SqlDataReader dr = con.Show_tblesetupforpriority(ddType.SelectedValue.ToString(), Session["Company"].ToString());
            dr.Read();
            if (dr.HasRows)
            {
                dr.Close();
                con.DisConnect();
                con.Update_tblesetupforpriority(hod, hr, blank, hodp, hrp, ddType.SelectedValue.ToString(), Session["Company"].ToString(), EmailBlank, EmailHR, EmailHOD);
                con.DisConnect();
            }
            else
            {
                dr.Close();
                con.DisConnect();
                con.Insert_tblesetupforpriority(hod, hr, blank, hodp, hrp, ddType.SelectedValue.ToString(), Session["Company"].ToString(), EmailBlank, EmailHR, EmailHOD);
                con.DisConnect();
            }
            showPriority();

        }
        if (chkHR.Checked == true && CHKHOD.Checked != true)
        {

            if (chkHR.Checked == true && CHKHOD.Checked != true)
            {
                ddPriority.Text = "HR";
                hrp = 1;
                hodp = 0;
            }
            if (chkHR.Checked != true && CHKHOD.Checked == true)
            {
                ddPriority.Text = "HOD";
                hrp = 0;
                hodp = 1;
            }

            SqlDataReader dr = con.Show_tblesetupforpriority(ddType.SelectedValue.ToString(), Session["Company"].ToString());
            dr.Read();
            if (dr.HasRows)
            {
                dr.Close();
                con.DisConnect();
                con.Update_tblesetupforpriority(hod, hr, blank, hodp, hrp, ddType.SelectedValue.ToString(), Session["Company"].ToString(), EmailBlank, EmailHR, EmailHOD);
                con.DisConnect();
            }
            else
            {
                dr.Close();
                con.DisConnect();
                con.Insert_tblesetupforpriority(hod, hr, blank, hodp, hrp, ddType.SelectedValue.ToString(), Session["Company"].ToString(), EmailBlank, EmailHR, EmailHOD);
                con.DisConnect();
            }
            showPriority();

        }


        if (chkBlank.Checked == true)
        {
            SqlDataReader dr = con.Show_tblesetupforpriority(ddType.SelectedValue.ToString(), Session["Company"].ToString());
            dr.Read();
            if (dr.HasRows)
            {
                dr.Close();
                con.DisConnect();
                con.Update_tblesetupforpriority(hod, hr, blank, hodp, hrp, ddType.SelectedValue.ToString(), Session["Company"].ToString(), EmailBlank, EmailHR, EmailHOD);
                con.DisConnect();
            }
            else
            {
                dr.Close();
                con.DisConnect();
                con.Insert_tblesetupforpriority(hod, hr, blank, hodp, hrp, ddType.SelectedValue.ToString(), Session["Company"].ToString(), EmailBlank, EmailHR, EmailHOD);
                con.DisConnect();
            }
            showPriority();

        }

    }
    protected void chkBlank_CheckedChanged(object sender, EventArgs e)
    {
        if (chkBlank.Checked == true)
        {
            CHKHOD.Enabled = false;
            chkHR.Enabled = false;
            chkHR.Checked = false;
            CHKHOD.Checked = false;
            ddPriority.Enabled = false;
            ddPriority.Text = "-----";
            chkHREmail.Enabled = true;
            chkNONEemail.Enabled = true;
            chkHODEmail.Enabled = true;

            chkHREmail.Checked = false;
            chkNONEemail.Checked = false;
            chkHODEmail.Checked = false;
        }
        else
        {
            CHKHOD.Enabled = true;
            chkHR.Enabled = true;
            ddPriority.Enabled = true;
            ddPriority.Text = "-----";
        }
    }

    public void ShowEmailApr()
    {

        if (CHKHOD.Checked == true && chkHR.Checked == true)
        {
            chkNONEemail.Checked = false;
            chkHREmail.Checked = false;
            chkHODEmail.Checked = false;

            chkNONEemail.Enabled = false;
            chkHREmail.Enabled = false;
            chkHODEmail.Enabled = false;
        }
    }
    protected void chkHR_CheckedChanged(object sender, EventArgs e)
    {
        if (chkHR.Checked == true)
        {
            chkBlank.Enabled = false;
            chkBlank.Checked = false;

            chkNONEemail.Checked = false;
            chkHREmail.Checked = false;
            chkHODEmail.Checked = false;

            chkNONEemail.Enabled = false;
            chkHREmail.Enabled = false;
            chkHODEmail.Enabled = true;

        }
        else if (CHKHOD.Checked == true && chkHR.Checked == true)
        {
            chkNONEemail.Checked = false;
            chkHREmail.Checked = false;
            chkHODEmail.Checked = false;

            chkNONEemail.Enabled = false;
            chkHREmail.Enabled = false;
            chkHODEmail.Enabled = false;
        }
        else
        {
            chkBlank.Enabled = true;

            //chkNONEemail.Checked = false;
            //chkHREmail.Checked = false;
            //chkHODEmail.Checked = false;

            //chkNONEemail.Enabled = false;
            //chkHREmail.Enabled = false;
            //chkHODEmail.Enabled = true;
        }
    }

    protected void CHKHOD_CheckedChanged(object sender, EventArgs e)
    {
        if (CHKHOD.Checked == true)
        {
            chkBlank.Enabled = false;
            chkBlank.Checked = false;

            chkNONEemail.Checked = false;
            chkHREmail.Checked = false;
            chkHODEmail.Checked = false;

            chkNONEemail.Enabled = false;
            chkHREmail.Enabled = true;
            chkHODEmail.Enabled = false;

        }
        else if (CHKHOD.Checked == true && chkHR.Checked == true)
        {
            chkNONEemail.Checked = false;
            chkHREmail.Checked = false;
            chkHODEmail.Checked = false;

            chkNONEemail.Enabled = false;
            chkHREmail.Enabled = false;
            chkHODEmail.Enabled = false;
        }
        else
        {
            chkBlank.Enabled = true;

            //chkNONEemail.Checked = false;
            //chkHREmail.Checked = false;
            //chkHODEmail.Checked = false;

            //chkNONEemail.Enabled = false;
            //chkHREmail.Enabled = false;
            //chkHODEmail.Enabled = true;
        }
    }
    protected void grdPriority_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string t1 = e.Row.Cells[0].Text;
            if (t1 == "1")
            {
                e.Row.Cells[0].Text = "True";
            }
            if (t1 == "0")
            {
                e.Row.Cells[0].Text = "False";

            }
            string t2 = e.Row.Cells[1].Text;
            if (t2 == "1")
            {
                e.Row.Cells[1].Text = "True";
            }
            if (t2 == "0")
            {
                e.Row.Cells[1].Text = "False";
            }
            string t3 = e.Row.Cells[2].Text;
            if (t3 == "1")
            {
                e.Row.Cells[2].Text = "True";
            }
            if (t3 == "0")
            {
                e.Row.Cells[2].Text = "False";
            }
            string t4 = e.Row.Cells[3].Text;
            if (t4 == "1")
            {
                e.Row.Cells[3].Text = "True";
            }
            if (t4 == "0")
            {
                e.Row.Cells[3].Text = "False";
            }
            string t5 = e.Row.Cells[4].Text;
            if (t5 == "1")
            {
                e.Row.Cells[4].Text = "True";
            }
            if (t5 == "0")
            {
                e.Row.Cells[4].Text = "False";
            }
            string Approvalfor = e.Row.Cells[5].Text;
            e.Row.Cells[5].Text = Approvalfor.Replace("For", "");
        }
    }
    //public void showAttendenceExpiry()
    //{

    //    SqlDataReader dr = con.Show_tbl_Attendence_Expiry(Session["Company"].ToString());
    //    dr.Read();
    //    if (dr.HasRows)
    //    {
    //        lblNoofdays.Text = dr["No_of_Days"].ToString();
    //    }
    //    dr.Read();
    //    con.DisConnect();
    //}

    public void showAttendenceExpiry()
    {

        SqlDataReader dr = con.Show_tbl_Attendence_Expiry(Session["Company"].ToString());
        DataTable dt = new DataTable();
        dt.Load(dr);
        grdSetATDExpiry.DataSource = dt;
        grdSetATDExpiry.DataBind();
        dr.Close();
        con.DisConnect();
    }

    public void Showtble_LeavesetupClub()
    {

        SqlDataReader dr = con.Show_tble_leave_setupAll(Session["Company"].ToString());
        DataTable dt = new DataTable();
        dt.Load(dr);
        grdLeaveSetup.DataSource = dt;
        grdLeaveSetup.DataBind();
        dr.Close();
        con.DisConnect();
    }

    public void showAttendenceExpiryINDV()
    {

        SqlDataReader dr1 = con.Show_tbl_Attendence_ExpiryINDS(Session["Company"].ToString());
        dr1.Read();
        if (dr1.HasRows)
        {
            dr1.Close();
            con.DisConnect();
            lblsetupAtt.Visible = true;
            SqlDataReader dr = con.Show_tbl_Attendence_ExpiryINDS(Session["Company"].ToString());
            DataTable dt = new DataTable();
            dt.Load(dr);
            grdAttendINDExpiry.DataSource = dt;
            grdAttendINDExpiry.DataBind();
            dr.Close();
            con.DisConnect();
        }
        else
        {
            dr1.Close();
            con.DisConnect();
            lblsetupAtt.Visible = false;
            SqlDataReader dr = con.Show_tbl_Attendence_ExpiryINDS(Session["Company"].ToString());
            DataTable dt = new DataTable();
            dt.Load(dr);
            grdAttendINDExpiry.DataSource = dt;
            grdAttendINDExpiry.DataBind();
            dr.Close();
            con.DisConnect();
        }

    }

    public void ShowAtten_Data()
    {
//update tbl_Attendence_Expiry  set No_of_Days ='" + days + "' ,Option_fromTime_toime_All='" + Option_fromTime_toime_All + "',From_Time='" + From_Time + "', To_Time ='" + To_Time + "', In_time_Out_TimeRquired='" + In_time_Out_TimeRquired + "'
        SqlDataReader dr = con.Show_tbl_Attendence_Expiry(Session["Company"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            txtNoOdays.Text = dr["No_of_Days"].ToString();
            ddOption.SelectedValue = dr["Option_fromTime_toime_All"].ToString();
            txtFromTime.Text = dr["From_Time"].ToString();
            txtTotime.Text = dr["To_Time"].ToString();
            ddIntimeOuttimerequired.SelectedValue = dr["In_time_Out_TimeRquired"].ToString();
        }
        dr.Close();
        con.DisConnect();
    }

    protected void btnattendenceno_Click(object sender, EventArgs e)
    {
        if (txtNoOdays.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Set No of days which Perevoius attendence mark will be allowed');", true);
        }

        else
        {

            if (rdAll.Checked == true)
            {


                if (ddIntimeOuttimerequired.Text == "Yes" && ddOption.Text == "Disable")
                {
                    if (txtFromTime.Text == "" || txtTotime.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please fill in time and out time');", true);
                    }
                    else
                    {

                        SqlDataReader dr = con.Show_tbl_Attendence_Expiry(Session["Company"].ToString());
                        dr.Read();
                        if (dr.HasRows)
                        {
                            dr.Close();
                            con.DisConnect();
                            Timerequired();
                            con.update_tbl_Attendence_Expiry(txtNoOdays.Text, Session["Company"].ToString(), ddOption.Text, txtFromTime.Text, txtTotime.Text, ddIntimeOuttimerequired.Text,ddCardAttendanceChanging.SelectedItem.Text);
                            con.DisConnect();
                            showAttendenceExpiry();
                        }
                        else
                        {
                            dr.Close();
                            con.DisConnect();
                            Timerequired();
                            con.Insert_tbl_Attendence_Expiry(txtNoOdays.Text, Session["Company"].ToString(), ddOption.Text, txtFromTime.Text, txtTotime.Text, ddIntimeOuttimerequired.Text,ddCardAttendanceChanging.SelectedItem.Text);
                            con.DisConnect();
                            showAttendenceExpiry();
                        }

                    }
                }

                if (ddIntimeOuttimerequired.Text == "Yes" && ddOption.Text == "Enable")
                {
                    //if (txtFromTime.Text == "" || txtTotime.Text == "")
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please fill in time and out time');", true);
                    //}
                    //else
                    //{

                        SqlDataReader dr = con.Show_tbl_Attendence_Expiry(Session["Company"].ToString());
                        dr.Read();
                        if (dr.HasRows)
                        {
                            dr.Close();
                            con.DisConnect();
                            Timerequired();
                            con.update_tbl_Attendence_Expiry(txtNoOdays.Text, Session["Company"].ToString(), ddOption.Text, txtFromTime.Text, txtTotime.Text, ddIntimeOuttimerequired.Text, ddCardAttendanceChanging.SelectedItem.Text);
                            con.DisConnect();
                            showAttendenceExpiry();
                        }
                        else
                        {
                            dr.Close();
                            con.DisConnect();
                            Timerequired();
                            con.Insert_tbl_Attendence_Expiry(txtNoOdays.Text, Session["Company"].ToString(), ddOption.Text, txtFromTime.Text, txtTotime.Text, ddIntimeOuttimerequired.Text, ddCardAttendanceChanging.SelectedItem.Text);
                            con.DisConnect();
                            showAttendenceExpiry();
                        }

                   // }
                }




                if (ddIntimeOuttimerequired.Text == "No")
                {

                    SqlDataReader dr = con.Show_tbl_Attendence_Expiry(Session["Company"].ToString());
                    dr.Read();
                    if (dr.HasRows)
                    {
                        dr.Close();
                        con.DisConnect();
                        Timerequired();
                        con.update_tbl_Attendence_Expiry(txtNoOdays.Text, Session["Company"].ToString(), ddOption.Text, txtFromTime.Text, txtTotime.Text, ddIntimeOuttimerequired.Text,ddCardAttendanceChanging.SelectedItem.Text);
                        con.DisConnect();
                        showAttendenceExpiry();
                    }
                    else
                    {
                        dr.Close();
                        con.DisConnect();
                        Timerequired();
                        con.Insert_tbl_Attendence_Expiry(txtNoOdays.Text, Session["Company"].ToString(), ddOption.Text, txtFromTime.Text, txtTotime.Text, ddIntimeOuttimerequired.Text, ddCardAttendanceChanging.SelectedItem.Text);
                        con.DisConnect();
                        showAttendenceExpiry();
                    }
                }
            }




        }
    }
    protected void lnkSetattendenceExpiry_Click(object sender, EventArgs e)
    {
        pnlReimbursementApproval.Visible = false;
      
        pnlCreate.Visible = false;
        pnlForAttendencesetup.Visible = true;
       
        pnlLeavesetup.Visible = false;
        pnlMailSetup.Visible = false;
        pnlReimTypeMaster.Visible = false;
        showAttendenceExpiry();
        showAttendenceExpiryINDV();

        txtempidduration.Enabled = false;
        txtempidduration.Text = "";
        btnattendenceno.Visible = true;
        btnAdd.Enabled = false;
        rdAll.Checked = true;
        rdEmployeeidwise.Checked = false;
        ShowAtten_Data();
        Timerequired();
    }
    protected void lnkleaveSetup_Click(object sender, EventArgs e)
    {
        pnlReimbursementApproval.Visible = false;
      
        pnlCreate.Visible = false;
     
        pnlForAttendencesetup.Visible = false;
        pnlLeavesetup.Visible = true;
        pnlMailSetup.Visible = false;
        pnlReimTypeMaster.Visible = false;
        ShowCoLUpto();
        SaveLeavetype();
       Showtble_LeavesetupClub();
       ShowLeaveuptodetails();
       
       // showLeave();
    }


    //public void showLeaveSetup()
    //{

    //    SqlDataReader dr = con.Show_tble_leave_setup(Session["Company"].ToString());
    //    dr.Read();
    //    if (dr.HasRows)
    //    {
    //        string valief = dr["Holiday_Count"].ToString();
    //        string offDatf = dr["Off_Day_Count"].ToString();
    //        if (valief == "0")
    //        {
    //            ddHolidayCount.SelectedValue = "0";
    //        }
    //        if (valief == "1")
    //        {
    //            ddHolidayCount.SelectedValue = "1";
    //        }
    //        if (offDatf == "0")
    //        {
    //            ddOffDaycount.SelectedValue = "0";
    //        }
    //        if (offDatf == "1")
    //        {
    //            ddOffDaycount.SelectedValue = "1";
    //        }
    //    }
    //    dr.Close();
    //    con.DisConnect();
    //}


    public void SaveLeavetype()
    {
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        string tblePayLeave = "[" + rccname + "$Pay Leave" + "]";

        SqlDataAdapter daa = new SqlDataAdapter(" select 'LWP' as [Leave Code] union SELECT DISTINCT [Leave Code] FROM  " + tblePayLeave + " order by [Leave Code]", Portalcon.Con);
            DataSet dsa = new DataSet();
            daa.Fill(dsa, "tblePayLeave");

            for (int j = 0; j <= dsa.Tables[0].Rows.Count - 1; j++)
            {

                string leaveType = "";
                leaveType = dsa.Tables[0].Rows[j]["Leave Code"].ToString();

                SqlDataReader dr = con.Show_tble_leave_setupClub(Session["Company"].ToString(), leaveType);
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
                    con.Insert_tble_leave_setup(leaveType, "", Session["Company"].ToString());
                    con.DisConnect();
                   
                }
                Portalcon.Connect();

            }
    }

  


  
    protected void lnkMailSetup_Click(object sender, EventArgs e)
    {
        pnlReimbursementApproval.Visible = false;
        //pnlReimbursType.Visible = false;
        pnlCreate.Visible = false;
        //pnlreimbursmentregister.Visible = false;
        //pnlMain.Visible = false;
        pnlForAttendencesetup.Visible = false;
        pnlLeavesetup.Visible = false;
        pnlMailSetup.Visible = true;
        pnlReimTypeMaster.Visible = false;
        showMailSetupdata();
        showMailSetup();
    }
    public void showMailSetup()
    {
        SqlDataReader dr = con.Show_tble_MailSetup1(Session["Company"].ToString());
        DataTable dt = new DataTable();
        dt.Load(dr);
        grdMailSetup.DataSource = dt;
        grdMailSetup.DataBind();
        dr.Close();
        con.DisConnect();

    }

    public void showMailSetupdata()
    {
        string chkProfilechange1 = ""; string chkProfileApproval1 = ""; string chkAttedenceMark1 = ""; string chkAttendenceApproval1 = ""; string chkLeaveApply1 = ""; string chkLeaveApproval1 = ""; string Mail_Sending_Option = ""; string chkReimbusrsmentApply1 = ""; string chkReimbusrmentApproval1 = "";
        SqlDataReader dr = con.Show_tble_MailSetup(Session["Company"].ToString(),ddsmtpfor.Text);
        dr.Read();
        if (dr.HasRows)
        {
            txtCCMail.Text = dr["CCMail"].ToString();
            txtFromEMailid.Text = dr["from_Email"].ToString();
            txtSMTP.Text = dr["smtp"].ToString();
            txtPassword.Text = dr["Password_From"].ToString();
            txtPortNo.Text = dr["Port_No"].ToString();
            chkProfilechange1 = dr["Profile_Change"].ToString();
            chkProfileApproval1 = dr["Profile_Approval"].ToString();
            chkAttedenceMark1 = dr["Attendence_Mark"].ToString();
            chkAttendenceApproval1 = dr["Attendence_Approval"].ToString();
            chkLeaveApply1 = dr["Leave_Apply"].ToString();
            chkLeaveApproval1 = dr["Leave_Approval"].ToString();
            Mail_Sending_Option = dr["Mail_Sending_Option"].ToString();
            chkReimbusrsmentApply1 = dr["Reimbursment_Apply"].ToString();
            chkReimbusrmentApproval1 = dr["Reimbursment_Approval"].ToString();
            ddPortalMail.SelectedValue = Mail_Sending_Option.ToString();
            if (chkProfilechange1 == "True")
            {
                chkProfilechange.Checked = true;
            }
            if (chkProfilechange1 == "False")
            {
                chkProfilechange.Checked = false;
            }

            if (chkProfileApproval1 == "True")
            {
                chkProfileApproval.Checked = true;
            }
            if (chkProfileApproval1 == "False")
            {
                chkProfileApproval.Checked = false;
            }

            if (chkAttedenceMark1 == "True")
            {
                chkAttendenceMark.Checked = true;
            }
            if (chkAttedenceMark1 == "False")
            {
                chkAttendenceMark.Checked = false;
            }

            if (chkAttendenceApproval1 == "True")
            {
                chkAttendenceApproval.Checked = true;
            }
            if (chkAttendenceApproval1 == "False")
            {
                chkAttendenceApproval.Checked = false;
            }

            if (chkLeaveApply1 == "True")
            {
                chkLeaveApply.Checked = true;
            }
            if (chkLeaveApply1 == "False")
            {
                chkLeaveApply.Checked = false;
            }

            if (chkLeaveApproval1 == "True")
            {
                chkLeaveApproval.Checked = true;
            }
            if (chkLeaveApproval1 == "False")
            {
                chkLeaveApproval.Checked = false;
            }

            if (chkReimbusrsmentApply1 == "True")
            {
                chkReimbursmentApply.Checked = true;
            }
            if (chkReimbusrsmentApply1 == "False")
            {
                chkReimbursmentApply.Checked = false;
            }

            if (chkReimbusrmentApproval1 == "True")
            {
                chkReimbusrApproval.Checked = true;
            }
            if (chkReimbusrmentApproval1 == "False")
            {
                chkReimbusrApproval.Checked = false;
            }

        }
        dr.Close();
        con.DisConnect();
    }

    protected void btnmailsetup_Click(object sender, EventArgs e)
    {
        string chkProfilechange1 = ""; string chkProfileApproval1 = ""; string chkAttedenceMark1 = ""; string chkAttendenceApproval1 = ""; string chkLeaveApply1 = ""; string chkLeaveApproval1 = ""; string chkReimbursmentApply1 = ""; string chkReimbursmentApproval1 = "";
        if (chkProfilechange.Checked == true)
        {
            chkProfilechange1 = "True";
        }
        if (chkProfilechange.Checked == false)
        {
            chkProfilechange1 = "False";
        }

        if (chkProfileApproval.Checked == true)
        {
            chkProfileApproval1 = "True";
        }
        if (chkProfileApproval.Checked == false)
        {
            chkProfileApproval1 = "False";
        }
        if (chkAttendenceMark.Checked == true)
        {
            chkAttedenceMark1 = "True";
        }
        if (chkAttendenceMark.Checked == false)
        {
            chkAttedenceMark1 = "False";
        }
        if (chkAttendenceApproval.Checked == true)
        {
            chkAttendenceApproval1 = "True";
        }
        if (chkAttendenceApproval.Checked == false)
        {
            chkAttendenceApproval1 = "False";
        }

        if (chkLeaveApply.Checked == true)
        {
            chkLeaveApply1 = "True";
        }
        if (chkLeaveApply.Checked == false)
        {
            chkLeaveApply1 = "False";
        }
        if (chkLeaveApproval.Checked == true)
        {
            chkLeaveApproval1 = "True";
        }
        if (chkLeaveApproval.Checked == false)
        {
            chkLeaveApproval1 = "False";
        }
        if (chkReimbursmentApply.Checked == true)
        {
            chkReimbursmentApply1 = "True";
        }
        if (chkReimbursmentApply.Checked == false)
        {
            chkReimbursmentApply1 = "False";
        }

        if (chkReimbusrApproval.Checked == true)
        {
            chkReimbursmentApproval1 = "True";
        }
        if (chkReimbusrApproval.Checked == false)
        {
            chkReimbursmentApproval1 = "False";
        }

        SqlDataReader dr = con.Show_tble_MailSetup(Session["Company"].ToString(),ddsmtpfor.Text);
        dr.Read();
        if (dr.HasRows)
        {

            dr.Close();
            con.DisConnect();
            con.update_tble_MailSetup(Session["Company"].ToString(), txtFromEMailid.Text.Trim(), txtSMTP.Text.Trim(), txtPassword.Text.Trim(), txtPortNo.Text.Trim(), Convert.ToInt32(ddPortalMail.SelectedValue.Trim()), chkProfilechange1, chkProfileApproval1, chkAttedenceMark1, chkAttendenceApproval1, chkLeaveApply1, chkLeaveApproval1, txtCCMail.Text, chkReimbursmentApply1, chkReimbursmentApproval1, ddsmtpfor.Text);
            con.DisConnect();
        }
        else
        {
            dr.Close();
            con.DisConnect();
            con.insert_tble_MailSetup(Session["Company"].ToString(), txtFromEMailid.Text.Trim(), txtSMTP.Text.Trim(), txtPassword.Text.Trim(), txtPortNo.Text.Trim(), Convert.ToInt32(ddPortalMail.SelectedValue.Trim()), chkProfilechange1, chkProfileApproval1, chkAttedenceMark1, chkAttendenceApproval1, chkLeaveApply1, chkLeaveApproval1, txtCCMail.Text, chkReimbursmentApply1, chkReimbursmentApproval1,ddsmtpfor.Text);
            con.DisConnect();

        }
        showMailSetup();
    }
    protected void rdAll_CheckedChanged(object sender, EventArgs e)
    {
        txtempidduration.Enabled = false;
        txtempidduration.Text = "";
        btnattendenceno.Visible = true;
        btnAdd.Enabled = false;
    }
    protected void rdEmployeeidwise_CheckedChanged(object sender, EventArgs e)
    {
        txtempidduration.Enabled = true;
        txtempidduration.Text = "";
        btnattendenceno.Visible = false;
        btnAdd.Enabled = true;
        showEmployeeNo();
    }
    protected void lnkrembursmenttype_Click(object sender, EventArgs e)
    {
        pnlReimTypeMaster.Visible = false;
        pnlReimbursementApproval.Visible = false;
        pnlCreate.Visible = false;
        //pnlreimbursmentregister.Visible = false;
        //pnlMain.Visible = false;
        //btnSave.Visible = false;
        pnlLeavesetup.Visible = false;
        pnlForAttendencesetup.Visible = false;
        //btnUpdate.Visible = false;
        //btnCancel.Visible = false;
        pnlMailSetup.Visible = false;
      
        //pnlReimbursType.Visible = true;
        //pnlMain.Visible = false;
        //chkblock.Enabled = false;
        //txtReimbursmenttype.Text = "";
        //chkblock.Checked = false;
       
        //btnUpdatereimburType.Visible = false;
        //btnreimbursmentsave.Visible = true;
    }
    //protected void btnreimbursmentsave_Click(object sender, EventArgs e)
    //{
    //    if (txtReimbursmenttype.Text.Trim() == "")
    //    {
    //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please Enter Reimbursment Type');", true);
    //    }
    //    else
    //    {
    //        SqlDataReader dr = con.Show_tbl_reimbursmenttypewithtype(Session["Company"].ToString(), txtReimbursmenttype.Text);
    //        dr.Read();
    //        if (dr.HasRows)
    //        {
    //            dr.Close();
    //            con.DisConnect();
    //            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please Enter another Reimbursment Type this is created');", true);
    //        }
    //        else
    //        {
    //            dr.Close();
    //            con.DisConnect();
    //            con.Insert_tbl_reimbursmenttype(txtReimbursmenttype.Text, Session["Company"].ToString(), "False");
    //            con.DisConnect();
    //            txtReimbursmenttype.Text = "";
    //            chkblock.Checked = false;
    //            showReimbursmentType();
    //        }
    //    }
    //}
    //public void showReimbursmentType()
    //{

    //    SqlDataReader dr = con.Show_tbl_reimbursmenttype(Session["Company"].ToString());
    //    DataTable dt = new DataTable();
    //    dt.Load(dr);
    //    grdreimburmestTyepe.DataSource = dt;
    //    grdreimburmestTyepe.DataBind();
    //    dr.Close();
    //    con.DisConnect();
    //}

    protected void grdreimburmestTyepe_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    //protected void btnreimbursEdit_Command(object sender, CommandEventArgs e)
    //{


    //    Session["itrty"] = e.CommandArgument.ToString();
    //    SqlDataReader dr = con.Show_tbl_reimbursmenttypeid(Session["itrty"].ToString());
    //    dr.Read();
    //    if (dr.HasRows)
    //    {
    //        txtReimbursmenttype.Text = dr["Reimbursment_Type"].ToString();
    //        lblreimtype.Text = dr["Reimbursment_Type"].ToString();
    //        lblexpcode.Text = dr["code"].ToString();
    //        string block = dr["Block"].ToString();
    //        if (block == "False")
    //        {
    //            chkblock.Checked = false;
    //        }
    //        else if (block == "True")
    //        {
    //            chkblock.Checked = true;
    //        }
    //    }
    //    dr.Close();
    //    con.DisConnect();
    //    chkblock.Enabled = true;
    //    btnUpdatereimburType.Visible = true;
    //    btnreimbursmentsave.Visible = false;
    //}

    //public void showReimburstDesc()
    //{
    //    SqlDataReader dr = con.Show_tbl_reimbursmenttypecodedesc(Session["Company"].ToString());


    //    //txtExpensivetype.Text = dr[""].ToString();
    //    txtExpensivetype.DataSource = dr;

    //    txtExpensivetype.DataTextField = "Reimbursment_Type";
    //    txtExpensivetype.DataValueField = "code";

    //    txtExpensivetype.DataBind();
    //    dr.Close();
    //    con.DisConnect();

    //}
    //protected void btnUpdatereimburType_Click(object sender, EventArgs e)
    //{


    //    if (lblreimtype.Text == txtReimbursmenttype.Text)
    //    {

    //        string block = "";
    //        if (chkblock.Checked == true)
    //        {
    //            block = "True";
    //            con.Update_tbl_reimbursmenttypeexpense("True", Session["Company"].ToString(), lblexpcode.Text);
    //            con.DisConnect();
    //            con.Update_tbl_reimbursmentsetupIndvidualuserFinalBlock("True", Session["Company"].ToString(), lblexpcode.Text);
    //            con.DisConnect();
    //        }
    //        if (chkblock.Checked == false)
    //        {
    //            block = "False";
    //            con.Update_tbl_reimbursmenttypeexpense("False", Session["Company"].ToString(), lblexpcode.Text);
    //            con.DisConnect();
    //            con.Update_tbl_reimbursmentsetupIndvidualuserFinalBlock("False", Session["Company"].ToString(), lblexpcode.Text);
    //            con.DisConnect();
    //        }

    //        con.Update_tbl_reimbursmenttype(txtReimbursmenttype.Text, block, Session["itrty"].ToString());
    //        con.DisConnect();
    //        txtReimbursmenttype.Text = "";
    //        chkblock.Checked = false;
    //        chkblock.Enabled = false;
    //        btnreimbursmentsave.Visible = true;
    //        btnUpdatereimburType.Visible = false;
    //        showReimbursmentType();
    //    }
    //    else
    //    {

    //        if (txtReimbursmenttype.Text.Trim() == "")
    //        {
    //            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please Enter Reimbursment Type');", true);
    //        }
    //        else
    //        {

    //            SqlDataReader dr = con.Show_tbl_reimbursmenttypewithtype(Session["Company"].ToString(), txtReimbursmenttype.Text);
    //            dr.Read();
    //            if (dr.HasRows)
    //            {
    //                dr.Close();
    //                con.DisConnect();
    //                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please Enter another Reimbursment Type this is created');", true);
    //            }
    //            else
    //            {
    //                dr.Close();
    //                con.DisConnect();
    //                string block = "";
    //                if (chkblock.Checked == true)
    //                {
    //                    block = "True";
    //                    con.Update_tbl_reimbursmenttypeexpense("True", Session["Company"].ToString(), lblexpcode.Text);
    //                    con.DisConnect();

    //                    con.Update_tbl_reimbursmentsetupIndvidualuserFinalBlock("True", Session["Company"].ToString(), lblexpcode.Text);
    //                    con.DisConnect();

    //                }
    //                if (chkblock.Checked == false)
    //                {
    //                    block = "False";
    //                    con.Update_tbl_reimbursmenttypeexpense("False", Session["Company"].ToString(), lblexpcode.Text);
    //                    con.DisConnect();

    //                    con.Update_tbl_reimbursmentsetupIndvidualuserFinalBlock("False", Session["Company"].ToString(), lblexpcode.Text);
    //                    con.DisConnect();
    //                }

    //                con.Update_tbl_reimbursmenttype(txtReimbursmenttype.Text, block, Session["itrty"].ToString());
    //                con.DisConnect();
    //                txtReimbursmenttype.Text = "";
    //                chkblock.Checked = false;
    //                chkblock.Enabled = false;
    //                btnreimbursmentsave.Visible = true;
    //                btnUpdatereimburType.Visible = false;
    //                showReimbursmentType();
    //            }
    //        }
    //    }
    //}

    //protected void grdReimbursment_RowDataBound(object sender, GridViewRowEventArgs e)
    //{

    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {

    //        Button btnDelete = (Button)e.Row.FindControl("btnDelete");
    //        Label lblBlock = (Label)e.Row.FindControl("lblBlock");
    //        //Button btnfinaldelete = (Button)e.Row.FindControl("btnFinalDelete");
    //        if (lblBlock.Text == "False")
    //        {
    //            btnDelete.Text = "Block";
    //        }
    //        if (lblBlock.Text == "True")
    //        {
    //            btnDelete.Text = "Unblock";
    //        }

    //    }

    //}

    //protected void btnseach_Click(object sender, EventArgs e)
    //{
    //    if (rdIndvidualAll.Checked == false)
    //    {
    //        showReimbusrmentwithSearch();
    //    }
    //    if (rdIndvidualAll.Checked == true)
    //    {
    //        showReimbusrmentwithSearchINDV();
    //    }
    //}
    //public void salaryBasedenable()
    //{

    //    pnlsalarybased.Visible = true;
    //    rdBasicsalary.Checked = false;
    //    rdGrosssSalary.Checked = false;
    //    rdFixedSalary.Checked = false;
    //    txtEXAmount.Enabled = false;

    //}
    //public void salaryBasedDisable()
    //{

    //    pnlsalarybased.Visible = false;
    //    rdBasicsalary.Checked = false;
    //    rdGrosssSalary.Checked = false;
    //    rdFixedSalary.Checked = false;
    //    txtEXAmount.Enabled = true;

    //}

  
    //protected void rdBasicsalary_CheckedChanged(object sender, EventArgs e)
    //{
    //    txtEXAmount.Enabled = false;
    //    txtEXAmount.Text = "0";
    //}
    //protected void rdGrosssSalary_CheckedChanged(object sender, EventArgs e)
    //{
    //    txtEXAmount.Enabled = false;
    //    txtEXAmount.Text = "0";
    //}
    //protected void rdFixedSalary_CheckedChanged(object sender, EventArgs e)
    //{
    //    txtEXAmount.Enabled = true;
    //    txtEXAmount.Text = "";
    //}
    //protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    //{
        
    //    }

       
  
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtNoOdays.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Set No of days which Perevoius attendence mark will be allowed');", true);
        }
        if (txtempidduration.SelectedValue == "")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please Select Employee');", true);
        }
        else
        {
            if (rdEmployeeidwise.Checked == true)
            {


                if (ddIntimeOuttimerequired.Text == "Yes" && ddOption.Text == "Disable")
                {
                    if (txtFromTime.Text == "" || txtTotime.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please fill in time and out time');", true);
                    }
                    else
                    {

                        SqlDataReader dr = con.Show_tbl_Attendence_ExpiryINDV(Session["Company"].ToString(), txtempidduration.Text);
                        dr.Read();
                        if (dr.HasRows)
                        {
                            dr.Close();
                            con.DisConnect();
                            con.update_tbl_Attendence_Expiryempidwise(txtNoOdays.Text, txtempidduration.Text, Session["Company"].ToString(), ddOption.Text, txtFromTime.Text, txtTotime.Text, ddIntimeOuttimerequired.Text,ddCardAttendanceChanging.SelectedItem.Text);
                            con.DisConnect();
                            showAttendenceExpiryINDV();
                        }
                        else
                        {
                            dr.Close();
                            con.DisConnect();
                            con.Insert_tbl_Attendence_ExpiryEMPIDWISE(txtNoOdays.Text, txtempidduration.Text, Session["Company"].ToString(), ddOption.Text, txtFromTime.Text, txtTotime.Text, ddIntimeOuttimerequired.Text,ddCardAttendanceChanging.SelectedItem.Text);
                            con.DisConnect();
                            showAttendenceExpiryINDV();
                        }
                    }
                }

                if (ddIntimeOuttimerequired.Text == "Yes" && ddOption.Text == "Enable")
                {
                    //if (txtFromTime.Text == "" || txtTotime.Text == "")
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please fill in time and out time');", true);
                    //}
                    //else
                    //{

                        SqlDataReader dr = con.Show_tbl_Attendence_ExpiryINDV(Session["Company"].ToString(), txtempidduration.Text);
                        dr.Read();
                        if (dr.HasRows)
                        {
                            dr.Close();
                            con.DisConnect();
                            con.update_tbl_Attendence_Expiryempidwise(txtNoOdays.Text, txtempidduration.Text, Session["Company"].ToString(), ddOption.Text, txtFromTime.Text, txtTotime.Text, ddIntimeOuttimerequired.Text, ddCardAttendanceChanging.SelectedItem.Text);
                            con.DisConnect();
                            showAttendenceExpiryINDV();
                        }
                        else
                        {
                            dr.Close();
                            con.DisConnect();
                            con.Insert_tbl_Attendence_ExpiryEMPIDWISE(txtNoOdays.Text, txtempidduration.Text, Session["Company"].ToString(), ddOption.Text, txtFromTime.Text, txtTotime.Text, ddIntimeOuttimerequired.Text, ddCardAttendanceChanging.SelectedItem.Text);
                            con.DisConnect();
                            showAttendenceExpiryINDV();
                        }
                    //}
                }

                if (ddIntimeOuttimerequired.Text == "No")
                {
                    SqlDataReader dr = con.Show_tbl_Attendence_ExpiryINDV(Session["Company"].ToString(), txtempidduration.Text);
                    dr.Read();
                    if (dr.HasRows)
                    {
                        dr.Close();
                        con.DisConnect();
                        con.update_tbl_Attendence_Expiryempidwise(txtNoOdays.Text, txtempidduration.Text, Session["Company"].ToString(), ddOption.Text, txtFromTime.Text, txtTotime.Text, ddIntimeOuttimerequired.Text, ddCardAttendanceChanging.SelectedItem.Text);
                        con.DisConnect();
                        showAttendenceExpiryINDV();
                    }
                    else
                    {
                        dr.Close();
                        con.DisConnect();
                        con.Insert_tbl_Attendence_ExpiryEMPIDWISE(txtNoOdays.Text, txtempidduration.Text, Session["Company"].ToString(), ddOption.Text, txtFromTime.Text, txtTotime.Text, ddIntimeOuttimerequired.Text, ddCardAttendanceChanging.SelectedItem.Text);
                        con.DisConnect();
                        showAttendenceExpiryINDV();
                    }
                
                }

            }


        }
    }


    private void showEmployeeNo()
    {

        DataTable dt = new DataTable();
        string id = string.Empty;
        string name = string.Empty;
        string newName = string.Empty;


        string sqlStatement = "SELECT * FROM " + Session["CompanyTableEmployee"].ToString() + "";
        SqlCommand sqlCmd = new SqlCommand(sqlStatement, Portalcon.Con);
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);

        sqlDa.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                id = dt.Rows[i]["No_"].ToString();

                name = dt.Rows[i]["First Name"].ToString();
                newName = id + " --------- " + name;

                txtempidduration.Items.Add(new ListItem(newName, id));
                txtempidduration.SelectedValue = id;

            }
        }

    }



    //private void showEmployeeNoForReimbusr()
    //{

    //    DataTable dt = new DataTable();
    //    string id = string.Empty;
    //    string name = string.Empty;
    //    string newName = string.Empty;


    //    string sqlStatement = "SELECT * FROM " + Session["CompanyTableEmployee"].ToString() + "";
    //    SqlCommand sqlCmd = new SqlCommand(sqlStatement, Portalcon.Con);
    //    SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);

    //    sqlDa.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            id = dt.Rows[i]["No_"].ToString();

    //            name = dt.Rows[i]["First Name"].ToString();
    //            newName = id + " --------- " + name;
               
    //            txtEmployeeid.Items.Add(new ListItem(newName, id));

    //            txtEmployeeid.SelectedValue = id;
    //        }
    //    }

    //}



    protected void btnDel_Command(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        con.Delete_tbl_Atten_Expiryforemployee(id);
        showAttendenceExpiryINDV();
    }
    protected void grdAttendINDExpiry_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAttendINDExpiry.PageIndex = e.NewPageIndex;

        showAttendenceExpiryINDV();
    }
    //protected void btnaddexpense_Click(object sender, EventArgs e)
    //{
    //    if (rdYes.Checked == true)
    //    {
    //        slarybasedopt = "Yes";
    //    }
    //    if (rdNo.Checked == true)
    //    {
    //        slarybasedopt = "No";
    //    }

    //    if (slarybasedopt == "Yes")
    //    {

    //        if (rdBasicsalary.Checked == true)
    //        {
    //            Salary_Based_Type = "Basic Salary";
    //        }
    //        if (rdGrosssSalary.Checked == true)
    //        {
    //            Salary_Based_Type = "Gross Salary";
    //        }
    //        if (rdFixedSalary.Checked == true)
    //        {
    //            Salary_Based_Type = "Fixed Amount";
    //        }
    //        if (Salary_Based_Type == "")
    //        {
    //            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please select Type of option which you provide Amount for Reimbursment');", true);
    //        }


    //        else
    //        {
    //            if (Salary_Based_Type == "Fixed Amount" && txtEXAmount.Text == "")
    //            {

    //                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please enter limit of Amount because you have select Fixed Amount Option');", true);
    //            }
    //            else
    //            {
    //                savenewdata();
    //            }
    //        }
    //    }

    //    if (slarybasedopt == "")
    //    {
    //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please select option for salary based reimbursment');", true);


    //    }
    //    if (slarybasedopt == "No")
    //    {
    //        if (txtEXAmount.Text == "")
    //        {

    //            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please enter limit of Amount');", true);
    //        }
    //        else
    //        {
    //            savenewdata();
    //        }
    //    }

    //}
    //protected void btnCancel_Click(object sender, EventArgs e)
    //{
    //    btnSave.Visible = true;
    //    btnUpdate.Visible = false;
    //    btnCancel.Visible = false;


    //    showReimBursment();
    //    rdYes.Checked = false;
    //    rdNo.Checked = false;
    //    salaryBasedDisable();
    //    clear();
    //    rdIndvidualAll.Enabled = true;
    //    rdIndvidualAll.Checked = false;
    //    chkALLOption.Checked = false;
    //    chkALLOption.Enabled = true;
    //    txtExpensivetype.Enabled = true;
    //    ddDepartment.Enabled = false;
    //    ddDesignation.Enabled = false;
    //    ddLocation.Enabled = false;
    //    ddGrade.Enabled = false;
    //    ddBranch.Enabled = false;

    //    chkDepartment.Enabled = true;
    //    chkLocation.Enabled = true;
    //    chkGrade.Enabled = true;
    //    chkBand.Enabled = true;
    //    chkEndDate.Enabled = true;
    //    chkBranchName.Enabled = true;

    //    chkDepartment.Checked = false;
    //    chkLocation.Checked = false;
    //    chkGrade.Checked = false;
    //    chkBand.Checked = false;
    //    chkEndDate.Checked = false;
    //    chkBranchName.Checked = false;

    //    ddDepartment.SelectedValue = "";
    //    ddDesignation.SelectedValue = "";
    //    ddLocation.SelectedValue = "";
    //    ddGrade.SelectedValue = "";
    //    ddBranch.SelectedValue = "";
    //    txtExpensivetype.SelectedValue = "";
    //}

    //protected void btnEditINDV_Command(object sender, CommandEventArgs e)
    //{
    //    Button button1 = (Button)sender;
    //    GridViewRow row1 = (GridViewRow)button1.NamingContainer;
    //    Label lblBlock = (Label)row1.FindControl("lblBlock");
    //    if (lblBlock.Text == "True")
    //    {
    //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('This is block  you can not modified ');", true);
    //    }
    //    else
    //    {
    //        string id = e.CommandArgument.ToString();
    //        Session["Setupreimbindvc"] = id.ToString();
    //        SqlDataReader dr = con.Show_tblRembursment_ForEditINDV(id);
    //        dr.Read();
    //        if (dr.HasRows)
    //        {
    //            txtExpensivetype.SelectedValue = dr["Expense_Code"].ToString();
    //            txtEmployeeid.SelectedValue = dr["Applicable_UserID"].ToString();
    //            txtEmployeeid.Enabled = false;
    //            txtEmployeeid.Visible = true;
    //            lblEmployee.Visible = true;
                
    //            EnddatewiseAlledit = dr["Enddatewise"].ToString();
                
    //            if (EnddatewiseAlledit == "True")
    //            {
    //                chkEndDate.Checked = true;
    //                chkEndDate.Enabled = true;
    //            }
    //            if (EnddatewiseAlledit == "False")
    //            {
    //                chkEndDate.Checked = false;
    //                chkEndDate.Enabled = true;
    //            }


    //            txtEXAmount.Text = dr["Expense_AmountMax"].ToString();

    //            txtStartDate.Text = dr["Start_Date"].ToString();
    //            lblexpensetype.Text = dr["Expense_Code"].ToString();

    //            string Salary_Based = dr["Salary_Based"].ToString();
    //            if (Salary_Based == "Yes")
    //            {
    //                rdYes.Checked = true;
    //                rdNo.Checked = false;
    //                salaryBasedenable();
    //            }
    //            if (Salary_Based == "No")
    //            {
    //                rdNo.Checked = true;
    //                rdYes.Checked = false;
    //                salaryBasedDisable();
    //            }
    //            if (Salary_Based == "")
    //            {
    //                rdYes.Checked = false;
    //                rdNo.Checked = false;

    //            }
    //            string Salary_Based_Type1 = dr["Salary_Based_Type"].ToString();


    //            if (Salary_Based_Type1 == "Basic Salary")
    //            {
    //                rdBasicsalary.Checked = true;
    //                txtEXAmount.Enabled = false;
    //            }
    //            if (Salary_Based_Type1 == "Gross Salary")
    //            {
    //                rdGrosssSalary.Checked = true;
    //                txtEXAmount.Enabled = false;
    //            }
    //            if (Salary_Based_Type1 == "Fixed Amount")
    //            {
    //                rdFixedSalary.Checked = true;
    //                txtEXAmount.Enabled = true;
    //            }
    //            txtendDate.Text = dr["End_Date"].ToString();
    //            rdIndvidualAll.Checked = true;
    //            rdIndvidualAll.Enabled = false;
    //            chkALLOption.Checked = false;
    //            chkALLOption.Enabled = false;
              
    //            btnUpdateindv.Visible = true;
    //            btnCancelIndv.Visible = true;
    //            btnaddexpense.Visible = false;
    //            btnSave.Visible = false;
    //            chkDepartment.Checked = false;
    //            chkLocation.Checked = false;
    //            chkBand.Checked = false;
    //            chkGrade.Checked = false;
    //            chkBranchName.Checked = false;

    //            chkDepartment.Enabled = false;
    //            chkLocation.Enabled = false;
    //            chkBand.Enabled = false;
    //            chkGrade.Enabled = false;
    //            chkBranchName.Enabled = false;

    //            ddDepartment.Enabled = false;
    //            ddLocation.Enabled = false;
    //            ddDesignation.Enabled = false;
    //            ddGrade.Enabled = false;
    //            ddBranch.Enabled = false;

    //        }

    //        dr.Close();
    //        con.DisConnect();

    //        showprofiledetail();
    //    }
    //}
    //protected void btnCancelIndv_Click(object sender, EventArgs e)
    //{
    //    btnaddexpense.Visible = false;
    //    btnUpdate.Visible = false;
    //    btnCancel.Visible = false;
    //    btnSave.Visible = true;
    //    showReimBursment();
    //    rdYes.Checked = false;
    //    rdNo.Checked = false;
    //    salaryBasedDisable();
    //    clear();
    //    rdIndvidualAll.Enabled = true;
    //    rdIndvidualAll.Checked = false;
    //    chkALLOption.Checked = false;
    //    chkALLOption.Enabled = true;
    //    txtExpensivetype.Enabled = true;
       
    //    btnUpdateindv.Visible = false;
    //    btnCancelIndv.Visible = false;


      
    //    txtExpensivetype.Enabled = true;
    //    ddDepartment.Enabled = false;
    //    ddDesignation.Enabled = false;
    //    ddLocation.Enabled = false;
    //    ddGrade.Enabled = false;
    //    ddBranch.Enabled = false;

    //    chkDepartment.Enabled = true;
    //    chkLocation.Enabled = true;
    //    chkGrade.Enabled = true;
    //    chkBand.Enabled = true;
    //    chkEndDate.Enabled = true;
    //    chkBranchName.Enabled = true;

    //    chkDepartment.Checked = false;
    //    chkLocation.Checked = false;
    //    chkGrade.Checked = false;
    //    chkBand.Checked = false;
    //    chkEndDate.Checked = false;
    //    chkBranchName.Checked = false;

    //    ddDepartment.SelectedValue = "";
    //    ddDesignation.SelectedValue = "";
    //    ddLocation.SelectedValue = "";
    //    ddGrade.SelectedValue = "";
    //    ddBranch.SelectedValue = "";
    //    txtExpensivetype.SelectedValue = "";
    //    lblEmployee.Visible = false;
    //    txtEmployeeid.Visible = false;

    //}
    //protected void grdEXpenseForIND_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    grdEXpenseForIND.PageIndex = e.NewPageIndex;
    //    if (txtSearch.Text == "")
    //    {
    //        showReimBursmentINDV();
    //    }
    //    else if (txtSearch.Text != "")
    //    {
    //        showReimbusrmentwithSearchINDV();
    //    }

    //}
    //protected void grdEXpenseForIND_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {

    //        Button btnDelete = (Button)e.Row.FindControl("btnDeleteINDV");
    //        Label lblBlock = (Label)e.Row.FindControl("lblBlock");
    //        if (lblBlock.Text == "False")
    //        {
    //            btnDelete.Text = "Block";
    //        }
    //        if (lblBlock.Text == "True")
    //        {
    //            btnDelete.Text = "Unblock";
    //        }

    //    }
    //}

   
    protected void chkNONEemail_CheckedChanged(object sender, EventArgs e)
    {
        if (chkNONEemail.Checked == true)
        {
            //CHKHOD.Enabled = false;
            //chkHR.Enabled = false;
            //chkHR.Checked = false;
            //CHKHOD.Checked = false;
            //ddPriority.Enabled = false;
            //ddPriority.Text = "-----";
            chkHODEmail.Enabled = false;
            chkHREmail.Enabled = false;
            chkHODEmail.Checked = false;
            chkHREmail.Checked = false;

        }
        if (chkNONEemail.Checked == false)
        {
            chkHODEmail.Enabled = true;
            chkHREmail.Enabled = true;
            chkHODEmail.Checked = false;
            chkHREmail.Checked = false;
            chkNONEemail.Enabled = false;

        }
    }
    protected void chkHREmail_CheckedChanged(object sender, EventArgs e)
    {
        if (chkHREmail.Checked == true)
        {
            chkNONEemail.Enabled = false;
            chkNONEemail.Checked = false;
        }
        else
        {
            chkNONEemail.Enabled = true;
        }
    }
    protected void chkHODEmail_CheckedChanged(object sender, EventArgs e)
    {
        if (chkHODEmail.Checked == true)
        {
            chkNONEemail.Enabled = false;
            chkNONEemail.Checked = false;
        }
        else
        {
            chkNONEemail.Enabled = true;
        }
    }
 
    

    public void ShowApprVisible()
    {

        if (ddType.Text == "For Profile" || ddType.Text == "For Attendance")
        {

            chkBlank.Visible = true;
            chkBlank.Enabled = true;
            CHKHOD.Visible = false;
            chkHR.Visible = false;
            ddPriority.Text = "-----";
        }

      else if (ddType.Text == "For Leave")
        {
            chkBlank.Visible = false;
            CHKHOD.Visible = true;
            CHKHOD.Enabled = true;
            chkHR.Visible = false;
        
        }

        else
        {

            chkBlank.Visible = true;
            CHKHOD.Visible = true;
            chkHR.Visible = true;
        }
    }
    protected void ddType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowApprVisible();
    }

    public void Timerequired()
    {
        if (ddIntimeOuttimerequired.Text == "Yes")
        {
            txtFromTime.Enabled = true;
            txtTotime.Enabled = true;
            ddOption.Enabled = true;
        }
        if (ddIntimeOuttimerequired.Text == "No")
        {
            txtFromTime.Enabled = false;
            txtTotime.Enabled = false;
            txtFromTime.Text = "";
            txtTotime.Text = "";
            ddOption.SelectedValue = "Disable";
            ddOption.Enabled = false;
        }
    }
    protected void ddIntimeOuttimerequired_SelectedIndexChanged(object sender, EventArgs e)
    {
        Timerequired();
    }

    public void showoptionforsmtp()
    {
        if (ddsmtpfor.Text == "Attendance")
        {
            chkAttendenceMark.Enabled = true;
            chkAttendenceApproval.Enabled = true;
            chkProfilechange.Enabled = false;
            chkProfileApproval.Enabled = false;

            chkLeaveApply.Enabled = false;
            chkLeaveApproval.Enabled = false;

            chkLeaveApply.Checked = false;
            chkLeaveApproval.Checked = false;
            chkReimbursmentApply.Enabled = false;
            chkReimbusrApproval.Enabled = false;
            chkReimbusrApproval.Checked = false;
            chkReimbursmentApply.Checked = false;
        }

        if (ddsmtpfor.Text == "Profile")
        {
            chkAttendenceMark.Enabled = false;
            chkAttendenceApproval.Enabled = false;

            chkAttendenceMark.Checked = false;
            chkAttendenceApproval.Checked = false;

            chkProfilechange.Enabled = true;
            chkProfileApproval.Enabled = true;
            chkLeaveApply.Enabled = false;
            chkLeaveApproval.Enabled = false;

            chkLeaveApply.Checked = false;
            chkLeaveApproval.Checked = false;
            chkReimbursmentApply.Enabled = false;
            chkReimbusrApproval.Enabled = false;
           
        }

        if (ddsmtpfor.Text == "Leave")
        {
            chkAttendenceMark.Enabled = false;
            chkAttendenceApproval.Enabled = false;
            chkAttendenceMark.Checked = false;
            chkAttendenceApproval.Checked = false;
            chkProfilechange.Enabled = false;
            chkProfileApproval.Enabled = false;
            chkProfilechange.Checked = false;
            chkProfileApproval.Checked = false;

            chkLeaveApply.Enabled = true;
            chkLeaveApproval.Enabled = true;

            chkReimbursmentApply.Enabled = false;
            chkReimbusrApproval.Enabled = false;
            

        }


        if (ddsmtpfor.Text == "Reimbursement")
        {
            chkAttendenceMark.Enabled = false;
            chkAttendenceApproval.Enabled = false;
            chkAttendenceMark.Checked = false;
            chkAttendenceApproval.Checked = false;
            chkProfilechange.Enabled = false;
            chkProfileApproval.Enabled = false;
            chkProfilechange.Checked = false;
            chkProfileApproval.Checked = false;

            chkLeaveApply.Enabled = false;
            chkLeaveApproval.Enabled = false;

            chkReimbursmentApply.Enabled = true;
            chkReimbusrApproval.Enabled = true;

        }


    }

    protected void ddsmtpfor_SelectedIndexChanged(object sender, EventArgs e)
    {
        showMailSetupdata();
        showoptionforsmtp();
    }
    protected void lnkReimbursementApproval_Click(object sender, EventArgs e)
    {
       
      
        pnlCreate.Visible = false;
      
        pnlForAttendencesetup.Visible = false;
        pnlLeavesetup.Visible = false;
        pnlMailSetup.Visible = false;
        pnlReimbursementApproval.Visible = true;
        pnlReimTypeMaster.Visible = false;
        Show_Rem_NewApproval();
        clearremNew();
    }
    protected void rdDepartmentRemAPr_CheckedChanged(object sender, EventArgs e)
    {
        ddDepartmentRemApr.Visible = true;
        ddIND_Employee.Visible = false;
        show_DepartmentNewReim();
    }
    protected void rdALLRemAPpr_CheckedChanged(object sender, EventArgs e)
    {
        ddDepartmentRemApr.Visible = false;
        ddIND_Employee.Visible = false;
    }
    protected void rdIndividualremAll_CheckedChanged(object sender, EventArgs e)
    {
        ddDepartmentRemApr.Visible = false;
        ddIND_Employee.Visible = true;
        showEmployeeIDforReim();
    }
    protected void rdfixed_amountRemApr_CheckedChanged(object sender, EventArgs e)
    {
        txtFixedAmountRemApr.Text = "0";
        txtFixedAmountRemApr.Visible = true;
        lblperfixed.Text = " Rs/- ";
    }
    protected void rdPercent_of_basic_Salary_CheckedChanged(object sender, EventArgs e)
    {
        txtFixedAmountRemApr.Text = "0";
        txtFixedAmountRemApr.Visible = true;
        lblperfixed.Text = " % ";
    }
    string chkself_Approval = ""; string rm1_Approval = ""; string rm2_Approval = ""; string App_forRem = ""; string limitrem = ""; string AmountremOptPercentage = ""; string AmountremOptFixed = "";
    protected void btnSaveRemApr_Click(object sender, EventArgs e)
    {
        if (txtTypeRemAPR.Text == "")
        {

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Fill Type');", true);
        }
        else
        {

            if (chkSelfAPP.Checked == true)
            {
                chkself_Approval = "Yes";

            }
            if (chkSelfAPP.Checked == false)
            {
                chkself_Approval = "No";

            }

            if (chkRM1.Checked == true)
            {
                rm1_Approval = "Yes";

            }
            if (chkRM1.Checked == false)
            {
                rm1_Approval = "No";

            }

            if (chkRM2.Checked == true)
            {
                rm2_Approval = "Yes";

            }
            if (chkRM2.Checked == false)
            {
                rm2_Approval = "No";

            }

            if (rdALLRemAPpr.Checked == true)
            {
                App_forRem = "All";

            }
            if (rdDepartmentRemAPr.Checked == true)
            {
                App_forRem = "Department";

            }
            if (rdIndividualremAll.Checked == true)
            {
                App_forRem = "Individual";

            }
            if (rdMonthlyRemApr.Checked == true)
            {
                limitrem = "Monthly";
            }


            if (rdYearlyRemApp.Checked == true)
            {
                limitrem = "Yearly";
            }
            if (rdDaily.Checked == true)
            {
                limitrem = "Daily";
            }

            if (rdPercent_of_basic_Salary.Checked == true)
            {
                AmountremOptPercentage = "Yes";
                blankdataforper = txtFixedAmountRemApr.Text;

            }
            if (rdPercent_of_basic_Salary.Checked == false)
            {
                AmountremOptPercentage = "No";
                blankdataforper = "0";
            }
            if (rdfixed_amountRemApr.Checked == true)
            {
                AmountremOptFixed = "Yes";
                blankdataforFixed = txtFixedAmountRemApr.Text;

            }
            if (rdfixed_amountRemApr.Checked == false)
            {
                AmountremOptFixed = "No";
                blankdataforFixed = "0";
            }


            if (chkself_Approval == "Yes" && txtLimitSelfREmAPR.Text.Trim() == "0" || txtLimitSelfREmAPR.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Fill Self Approval Limit');", true);

            }
            else if (rm1_Approval == "Yes" && txtLimitRM1.Text.Trim() == "0" || txtLimitRM1.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Fill Reporting Manager 1 Limit');", true);

            }
            else if (rm2_Approval == "Yes" && txtLimitRM2.Text.Trim() == "0" || txtLimitRM2.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Fill Reporting Manager 2 Limit');", true);

            }
            else if (AmountremOptPercentage == "Yes" && txtFixedAmountRemApr.Text.Trim() == "0" || txtFixedAmountRemApr.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Fill Percentage of basic salary');", true);

            }
            else if (AmountremOptFixed == "Yes" && txtFixedAmountRemApr.Text.Trim() == "0" || txtFixedAmountRemApr.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Fill Fixed Amount');", true);

            }


            else
            {
                SqlDataReader dr1 = con.Show_tble_ReimbursmentsetupRepeatationType(Session["Company"].ToString(), txtTypeRemAPR.Text.Trim());
                dr1.Read();
                if (dr1.HasRows)
                {
                    dr1.Close();
                    con.DisConnect();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please enter another type');", true);


                }
                else
                {
                    dr1.Close();
                    con.DisConnect();

                    if (rdALLRemAPpr.Checked == true)
                    {

                       // SqlDataReader dr = con.Show_tble_ReimbursmentsetupRepeatation(Session["Company"].ToString(), App_forRem, txtTypeRemAPR.Text.Trim());
                        SqlDataReader dr = con.Show_tble_ReimbursmentsetupRepeatationType(Session["Company"].ToString(), txtTypeRemAPR.Text.Trim());
                        dr.Read();
                        if (dr.HasRows)
                        {
                            dr.Close();
                            con.DisConnect();
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please enter another type');", true);

                        }
                        else
                        {
                            string blankvalue = "";
                            dr.Close();
                            con.DisConnect();
                            con.Insert_tble_Reimbursmentsetup(txtTypeRemAPR.Text, txtDescriptionREmAPP.Text, chkself_Approval, txtLimitSelfREmAPR.Text, rm1_Approval, txtLimitRM1.Text, rm2_Approval, txtLimitRM2.Text, App_forRem, blankvalue, blankvalue, limitrem, AmountremOptPercentage, blankdataforper, AmountremOptFixed, blankdataforFixed, Session["Company"].ToString(),dddisplaylimit.SelectedItem.Text,ddDisplayBalance.SelectedItem.Text);
                            con.DisConnect();
                            clearremNew();
                            Show_Rem_NewApproval();
                        }
                    }

                    if (rdDepartmentRemAPr.Checked == true)
                    {

                        //SqlDataReader dr = con.Show_tble_ReimbursmentsetupRepeatationDepart(Session["Company"].ToString(), App_forRem, ddDepartmentRemApr.SelectedValue.ToString(), txtTypeRemAPR.Text.Trim());
                        SqlDataReader dr = con.Show_tble_ReimbursmentsetupRepeatationType(Session["Company"].ToString(), txtTypeRemAPR.Text.Trim());
                        dr.Read();
                        if (dr.HasRows)
                        {
                            dr.Close();
                            con.DisConnect();
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please enter another type');", true);

                        }
                        else
                        {
                            string blankvalue = "";
                            dr.Close();
                            con.DisConnect();
                            con.Insert_tble_Reimbursmentsetup(txtTypeRemAPR.Text, txtDescriptionREmAPP.Text, chkself_Approval, txtLimitSelfREmAPR.Text, rm1_Approval, txtLimitRM1.Text, rm2_Approval, txtLimitRM2.Text, App_forRem, ddDepartmentRemApr.SelectedValue.ToString(), blankvalue, limitrem, AmountremOptPercentage, blankdataforper, AmountremOptFixed, blankdataforFixed, Session["Company"].ToString(), dddisplaylimit.SelectedItem.Text, ddDisplayBalance.SelectedItem.Text);

                            con.DisConnect();
                            clearremNew();
                            Show_Rem_NewApproval();
                        }
                    }

                    if (rdIndividualremAll.Checked == true)
                    {

                        //SqlDataReader dr = con.Show_tble_ReimbursmentsetupRepeatationINDV(Session["Company"].ToString(), App_forRem, ddIND_Employee.SelectedValue.ToString(), txtTypeRemAPR.Text);

                        SqlDataReader dr = con.Show_tble_ReimbursmentsetupRepeatationType(Session["Company"].ToString(), txtTypeRemAPR.Text.Trim());
                        dr.Read();
                        if (dr.HasRows)
                        {
                            dr.Close();
                            con.DisConnect();
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please enter another type');", true);

                        }
                        else
                        {
                            string blankvalue = "";
                            dr.Close();
                            con.DisConnect();
                            con.Insert_tble_Reimbursmentsetup(txtTypeRemAPR.Text, txtDescriptionREmAPP.Text, chkself_Approval, txtLimitSelfREmAPR.Text, rm1_Approval, txtLimitRM1.Text, rm2_Approval, txtLimitRM2.Text, App_forRem, blankvalue, ddIND_Employee.SelectedValue.ToString(), limitrem, AmountremOptPercentage, blankdataforper, AmountremOptFixed, blankdataforFixed, Session["Company"].ToString(), dddisplaylimit.SelectedItem.Text, ddDisplayBalance.SelectedItem.Text);
                            con.DisConnect();
                            clearremNew();
                            Show_Rem_NewApproval();
                        }
                    }
                }

            }
        }
       
    }


    public void Show_Rem_NewApproval()
    {
        //pnlsearchdataremApproval
        SqlDataReader dr1 = con.Show_tble_ReimbursmentsetupNew(Session["Company"].ToString());
        dr1.Read();
        if (dr1.HasRows)
        {
            dr1.Close();
            con.DisConnect();
            pnlsearchdataremApproval.Visible = true;
            SqlDataReader dr = con.Show_tble_ReimbursmentsetupNew(Session["Company"].ToString());

            DataTable Dt = new DataTable();
            Dt.Load(dr);
            grdNewreimApprosetup.DataSource = Dt;
            grdNewreimApprosetup.DataBind();
            dr.Close();
            con.DisConnect();
        
        }
        else
        {

            pnlsearchdataremApproval.Visible = false;
            dr1.Close();
            con.DisConnect();
            SqlDataReader dr = con.Show_tble_ReimbursmentsetupNew(Session["Company"].ToString());

            DataTable Dt = new DataTable();
            Dt.Load(dr);
            grdNewreimApprosetup.DataSource = Dt;
            grdNewreimApprosetup.DataBind();
            dr.Close();
            con.DisConnect();
        }




        
    }

    public void Show_Rem_Searchs()
    {
        SqlDataReader dr = con.Show_tble_Reim_Approval_Setupsearch(Session["Company"].ToString(), txtTypeapr.Text);

        DataTable Dt = new DataTable();
        Dt.Load(dr);
        grdNewreimApprosetup.DataSource = Dt;
        grdNewreimApprosetup.DataBind();
        dr.Close();
        con.DisConnect();
    }



    public void show_DepartmentNewReim()
    {
        string tble_Name = "[" + Session["Company"] + "$" + "Department Master]";
        SqlDataReader dr = Portalcon.Show_Department(tble_Name);
        ddDepartmentRemApr.DataSource = dr;
        ddDepartmentRemApr.DataTextField = "Department Description";
        ddDepartmentRemApr.DataValueField = "Department Code";
        ddDepartmentRemApr.DataBind();
        dr.Close();
        Portalcon.DisConnect();

    }


    private void ShowEmployeeForLeaveUptoDropDown()
    {

        DataTable dt = new DataTable();
        string id = string.Empty;
        string name = string.Empty;
        string newName = string.Empty;


        string sqlStatement = "SELECT * FROM " + Session["CompanyTableEmployee"].ToString() + "";
        SqlCommand sqlCmd = new SqlCommand(sqlStatement, Portalcon.Con);
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);

        sqlDa.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                id = dt.Rows[i]["No_"].ToString();

                name = dt.Rows[i]["First Name"].ToString();
                newName = id + " --------- " + name;

                txtEmployeeidcoupto.Items.Add(new ListItem(newName, id));

                txtEmployeeidcoupto.SelectedValue = id;
            }
        }

    }

    private void showEmployeeIDforReim()
    {

        DataTable dt = new DataTable();
        string id = string.Empty;
        string name = string.Empty;
        string newName = string.Empty;


        string sqlStatement = "SELECT * FROM " + Session["CompanyTableEmployee"].ToString() + "";
        SqlCommand sqlCmd = new SqlCommand(sqlStatement, Portalcon.Con);
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);

        sqlDa.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                id = dt.Rows[i]["No_"].ToString();

                name = dt.Rows[i]["First Name"].ToString();
                newName = id + " --------- " + name;

                ddIND_Employee.Items.Add(new ListItem(newName, id));

                ddIND_Employee.SelectedValue = id;
            }
        }

    }
    protected void grdNewreimApprosetup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdNewreimApprosetup.PageIndex = e.NewPageIndex;
        Show_Rem_NewApproval();
    }
    protected void chkSelfAPP_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSelfAPP.Checked == true)
        {
            txtLimitSelfREmAPR.Enabled = true;

            if (chkRM1.Checked == true && chkRM2.Checked == true)
            {
                decimal selflimit = Convert.ToDecimal(txtLimitSelfREmAPR.Text);
                decimal rmlimit = Convert.ToDecimal(txtLimitRM1.Text);
                if (selflimit <= rmlimit)
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Self Approval limit will be less than reporting manager 1 limit');", true);
                    txtLimitSelfREmAPR.Text = "0";
                 
                }
            }


            if (chkRM1.Checked == false && chkRM2.Checked == true)
            {
                decimal selflimit = Convert.ToDecimal(txtLimitSelfREmAPR.Text);
                decimal rmlimit = Convert.ToDecimal(txtLimitRM2.Text);
                if (selflimit <= rmlimit)
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Self Approval limit will be less than reporting manager 2 limit');", true);
                    txtLimitSelfREmAPR.Text = "0";
                  
                }
            }

        }

        if (chkSelfAPP.Checked == false)
        {
            txtLimitSelfREmAPR.Enabled = false;
            txtLimitSelfREmAPR.Text = "0";
        }
    }
    protected void chkRM1_CheckedChanged(object sender, EventArgs e)
    {
        if (chkRM1.Checked == true)
        {
            txtLimitRM1.Enabled = true;
            if (chkSelfAPP.Checked == true && chkRM2.Checked == false)
            {

                decimal selflimit = Convert.ToDecimal(txtLimitSelfREmAPR.Text);
                decimal rmlimit = Convert.ToDecimal(txtLimitRM1.Text);
                if (selflimit >= rmlimit)
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Reporting Manager 1 limit will be greater than self approval limit');", true);
                    txtLimitRM1.Text = "0";
                    //txtLimitRM1.Enabled = false;
                    //chkRM1.Checked = false;
                }
            }

            if (chkSelfAPP.Checked == false && chkRM2.Checked == true)
            {

                decimal selflimit = Convert.ToDecimal(txtLimitRM2.Text);
                decimal rmlimit = Convert.ToDecimal(txtLimitRM1.Text);
                if (selflimit <= rmlimit)
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Reporting Manager 1 limit will be less than Reporting Manager 2 limit');", true);
                    txtLimitRM1.Text = "0";
                    //txtLimitRM1.Enabled = false;
                    //chkRM1.Checked = false;
                }
            }

            if (chkSelfAPP.Checked == true && chkRM2.Checked == true)
            {

                decimal selflimit = Convert.ToDecimal(txtLimitSelfREmAPR.Text);
                decimal rmlimit = Convert.ToDecimal(txtLimitRM1.Text);
                decimal rmlimit2 = Convert.ToDecimal(txtLimitRM2.Text);
                //if (selflimit <= rmlimit || rmlimit <= rmlimit2)
                if (rmlimit <= selflimit)
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Reporting Manager 1 limit will be greater than self approval limit');", true);
                    txtLimitRM1.Text = "0";
                    //txtLimitRM1.Enabled = false;
                    //chkRM1.Checked = false;
                }

                if (rmlimit2 <= rmlimit)
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Reporting Manager 1 limit will be less than Reporting Manager 2 limit');", true);
                    txtLimitRM1.Text = "0";
                    //txtLimitRM1.Enabled = false;
                    //chkRM1.Checked = false;

                }

            }

        }

        if (chkRM1.Checked == false)
        {
            txtLimitRM1.Enabled = false;
            txtLimitRM1.Text = "0";
        }
    }
    protected void chkRM2_CheckedChanged(object sender, EventArgs e)
    {
        //
        if (chkRM2.Checked == true)
        {
            txtLimitRM2.Enabled = true;

            if (chkSelfAPP.Checked == true && chkRM1.Checked == true || chkSelfAPP.Checked == false && chkRM1.Checked == true)
            {

                decimal rmlimit1 = Convert.ToDecimal(txtLimitRM1.Text);
                decimal rmlimit2 = Convert.ToDecimal(txtLimitRM2.Text);
                if (rmlimit1 >= rmlimit2)
                {

                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Reporting Manager 2 limit will be greater than Reporting Manager 1 limit');", true);
                    txtLimitRM2.Text = "0";
                    //txtLimitRM2.Enabled = false;
                    //chkRM2.Checked = false;
                }
                else
                {
                    chkRM2.Checked = true;
                }
            }

            if (chkSelfAPP.Checked == true && chkRM1.Checked == false)
            {

                decimal selflimit = Convert.ToDecimal(txtLimitSelfREmAPR.Text);
                decimal rmlimit2 = Convert.ToDecimal(txtLimitRM2.Text);
                if (selflimit >= rmlimit2)
                {

                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Reporting Manager 2 limit will be greater than Self Approval limit');", true);
                    txtLimitRM2.Text = "0";
                    //txtLimitRM2.Enabled = false;
                    //chkRM2.Checked = false;
                }
                else
                {
                    chkRM2.Checked = true;
                }
            }
       

        }

        if (chkRM2.Checked == false)
        {
            txtLimitRM2.Enabled = false;
            txtLimitRM2.Text = "0";
        }
    }
    protected void btnEditnewApproRem_Command(object sender, CommandEventArgs e)
    {
        show_DepartmentNewReim();
        showEmployeeIDforReim();
        string id=e.CommandArgument.ToString();
        Session["idremnew"]=id.ToString();
        SqlDataReader dr = con.Show_tble_ReimbursmentsetupforUpdateID(id);
        dr.Read();
        if (dr.HasRows)
        {
            txtTypeRemAPR.Enabled = false;
            rdALLRemAPpr.Enabled = true;
            rdDepartmentRemAPr.Enabled = true;
            rdIndividualremAll.Enabled = true;
            btnSaveRemApr.Visible = false;
            btnClearREmapr.Visible = true;
            btnUpdateRemApr.Visible = true;
            txtTypeRemAPR.Text=dr["Type"].ToString();
            dddisplaylimit.SelectedValue = dr["Display_limit"].ToString();
            ddDisplayBalance.SelectedValue = dr["Display_Balance"].ToString();
            txtDescriptionREmAPP.Text = dr["Description"].ToString();
            string chkself_Approval = dr["Self_Approval"].ToString();
            if (chkself_Approval == "Yes")
            {
                chkSelfAPP.Checked = true;
                txtLimitSelfREmAPR.Enabled = true;
            }

            if (chkself_Approval == "No")
            {
                chkSelfAPP.Checked = false;
                txtLimitSelfREmAPR.Enabled = false;
            }
           txtLimitSelfREmAPR.Text = dr["Self_Limit"].ToString();

           string rm1_Approval = dr["RM1_Approval"].ToString();
           if (rm1_Approval == "Yes")
           {
               chkRM1.Checked = true;
               txtLimitRM1.Enabled = true;
           }

           if (rm1_Approval == "No")
           {
               chkRM1.Checked = false;
               txtLimitRM1.Enabled = false;
           }

           txtLimitRM1.Text = dr["RM1_Limit"].ToString();
           string rm2_Approval = dr["RM2_Approval"].ToString();
           if (rm2_Approval == "Yes")
           {
               chkRM2.Checked = true;
               txtLimitRM2.Enabled = true;
           }

           if (rm2_Approval == "No")
           {
               chkRM2.Checked = false;
               txtLimitRM2.Enabled = false;
           }

           txtLimitRM2.Text = dr["RM2_Limit"].ToString();
           
           string Limit = dr["Limit"].ToString();
           if (Limit == "Yearly")
           {
               rdDaily.Checked = false;
               rdYearlyRemApp.Checked = true;
               rdMonthlyRemApr.Checked = false;
           }

           if (Limit == "Monthly")
           {
               rdDaily.Checked = false;
               rdMonthlyRemApr.Checked = true;
               rdYearlyRemApp.Checked = false;
           }
           if (Limit == "Daily")
           {
               rdDaily.Checked = true;
               rdMonthlyRemApr.Checked = false;
               rdYearlyRemApp.Checked = false;
           }
           txtFixedAmountRemApr.Text = dr["Amount"].ToString();
           
            string  App_forRem =dr["Applicable_For"].ToString();
            if (App_forRem == "All")
            {
                ddDepartmentRemApr.Enabled = true;
                rdALLRemAPpr.Checked = true;
                rdDepartmentRemAPr.Checked = false;
                ddDepartmentRemApr.Visible = false;
                ddIND_Employee.Visible = false;
                rdIndividualremAll.Checked = false;
                ddIND_Employee.Enabled = true;
            }


            if (App_forRem == "Department")
            {
                rdDepartmentRemAPr.Checked = true;
                ddDepartmentRemApr.Visible = true;
                rdIndividualremAll.Checked = false;
                rdALLRemAPpr.Checked = false;
                ddIND_Employee.Visible = false;
                ddIND_Employee.Enabled = true;
                ddDepartmentRemApr.SelectedValue = dr["Department"].ToString();
                ddDepartmentRemApr.Enabled = false;
            }

            if (App_forRem == "Individual")
            {
                ddDepartmentRemApr.Enabled = true;
                rdALLRemAPpr.Checked = false;
                rdIndividualremAll.Checked = true;
                ddDepartmentRemApr.Visible = false;
                ddIND_Employee.Visible = true;
                rdDepartmentRemAPr.Checked = false;
                ddIND_Employee.Enabled = false;
                ddIND_Employee.SelectedValue = dr["Individual_Userid"].ToString();
               
            }
           
           


             string  Percentage_Of_Basic_Pay =dr["Percentage_Of_Basic_Pay"].ToString();
             if (Percentage_Of_Basic_Pay == "Yes")
             {
                 rdPercent_of_basic_Salary.Checked = true;
                 rdfixed_amountRemApr.Checked = false;
                 txtFixedAmountRemApr.Text = dr["Percentage"].ToString();
                 lblperfixed.Text = " % ";
             }
             if (Percentage_Of_Basic_Pay == "No")
             {
                 rdPercent_of_basic_Salary.Checked = false;
                 lblperfixed.Text = " Rs/- ";
             }

            
             string  Fixed_Amount =dr["Fixed_Amount"].ToString();
             if (Fixed_Amount == "Yes")
             {
                 rdfixed_amountRemApr.Checked = true;
                 rdPercent_of_basic_Salary.Checked = false;
                 txtFixedAmountRemApr.Text = dr["Amount"].ToString();
                 lblperfixed.Text = " Rs/- ";
             }
             if (Fixed_Amount == "No")
             {
                 rdfixed_amountRemApr.Checked = false;
                 lblperfixed.Text = " % ";
             }

              

            dr.Close();
            con.DisConnect();

        
        }
    }


    public void clearremNew()
    {

        txtTypeRemAPR.Enabled = true;
        txtDescriptionREmAPP.Text = "";
        rdALLRemAPpr.Enabled = true;
        rdALLRemAPpr.Checked = true;
        rdDepartmentRemAPr.Enabled = true;
        rdIndividualremAll.Enabled = true;
        rdIndividualremAll.Checked = true;
        btnSaveRemApr.Visible = true;
        btnClearREmapr.Visible = false;
        btnUpdateRemApr.Visible = false;
        rdDepartmentRemAPr.Checked = false;
        rdYearlyRemApp.Checked = true;
        rdIndividualremAll.Checked = false;
        rdfixed_amountRemApr.Checked = true;
        txtTypeRemAPR.Text = "";
        chkSelfAPP.Checked = false;
        txtLimitSelfREmAPR.Text = "0";
        txtLimitSelfREmAPR.Enabled = false;
        chkRM1.Checked = false;
        txtLimitRM1.Text = "0";
        txtLimitRM1.Enabled = false;
        chkRM2.Checked = false;
        txtLimitRM2.Text = "0";
        txtLimitRM2.Enabled = false;
        txtFixedAmountRemApr.Text = "0";
        ddDepartmentRemApr.Visible = false;
        ddIND_Employee.Visible = false;

    }
    protected void btnClearREmapr_Click(object sender, EventArgs e)
    {
        //txtTypeRemAPR.Enabled = true;
        //rdALLRemAPpr.Enabled = true;
        //rdDepartmentRemAPr.Enabled = true;
        //rdIndividualremAll.Enabled = true;
        //btnSaveRemApr.Visible = true;
        //btnClearREmapr.Visible = false;
        //btnUpdateRemApr.Visible = false;
        clearremNew();
    }
    string blankdataforper = "0"; string blankdataforFixed = "0"; string App_forRemTypemaster = "";
    protected void btnUpdateRemApr_Click(object sender, EventArgs e)
    {

        if (chkSelfAPP.Checked == true)
        {
            chkself_Approval = "Yes";

        }
        if (chkSelfAPP.Checked == false)
        {
            chkself_Approval = "No";

        }

        if (chkRM1.Checked == true)
        {
            rm1_Approval = "Yes";

        }
        if (chkRM1.Checked == false)
        {
            rm1_Approval = "No";

        }

        if (chkRM2.Checked == true)
        {
            rm2_Approval = "Yes";

        }
        if (chkRM2.Checked == false)
        {
            rm2_Approval = "No";

        }

        if (rdALLRemAPpr.Checked == true)
        {
            App_forRem = "All";

        }
        if (rdDepartmentRemAPr.Checked == true)
        {
            App_forRem = "Department";

        }
        if (rdIndividualremAll.Checked == true)
        {
            App_forRem = "Individual";

        }
        if (rdMonthlyRemApr.Checked == true)
        {
            limitrem = "Monthly";
        }
        if (rdDaily.Checked == true)
        {
            limitrem = "Daily";
        }

        if (rdYearlyRemApp.Checked == true)
        {
            limitrem = "Yearly";
        }

        if (rdPercent_of_basic_Salary.Checked == true)
        {
            AmountremOptPercentage = "Yes";
            blankdataforper = txtFixedAmountRemApr.Text;
            
        }
        if (rdPercent_of_basic_Salary.Checked == false)
        {
            AmountremOptPercentage = "No";
            blankdataforper = "0";
        }
        if (rdfixed_amountRemApr.Checked == true)
        {
            AmountremOptFixed = "Yes";
            blankdataforFixed = txtFixedAmountRemApr.Text;

        }
        if (rdfixed_amountRemApr.Checked == false)
        {
            AmountremOptFixed = "No";
            blankdataforFixed = "0";
        }

        if (chkself_Approval == "Yes" && txtLimitSelfREmAPR.Text.Trim() == "0" || txtLimitSelfREmAPR.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Fill Self Approval Limit');", true);
        
        }
       else if (rm1_Approval == "Yes" && txtLimitRM1.Text.Trim() == "0" || txtLimitRM1.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Fill Reporting Manager 1 Limit');", true);

        }
        else if (rm2_Approval == "Yes" && txtLimitRM2.Text.Trim() == "0" || txtLimitRM2.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Fill Reporting Manager 2 Limit');", true);

        }
        else if (AmountremOptPercentage == "Yes" && txtFixedAmountRemApr.Text.Trim() == "0" || txtFixedAmountRemApr.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Fill Percentage of basic salary');", true);

        }
        else if (AmountremOptFixed == "Yes" && txtFixedAmountRemApr.Text.Trim() == "0" || txtFixedAmountRemApr.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Fill Fixed Amount');", true);

        }
        else
        {

               if (rdALLRemAPpr.Checked == true)
            {
                App_forRemTypemaster = "All";

            }
            if (rdDepartmentRemAPr.Checked == true)
            {
                App_forRemTypemaster = "Department";

            }
            if (rdIndividualremAll.Checked == true)
            {
                App_forRemTypemaster = "Individual";

            }



            //con.Update_tble_Reimbursmentsetup(txtDescriptionREmAPP.Text, chkself_Approval, txtLimitSelfREmAPR.Text, rm1_Approval, txtLimitRM1.Text, rm2_Approval, txtLimitRM2.Text, limitrem, AmountremOptPercentage, blankdataforper, AmountremOptFixed, blankdataforFixed, Session["idremnew"].ToString());
            //con.DisConnect();
            if (App_forRemTypemaster == "All")
            {
                string dep = "";
                con.Update_tble_Reimbursmentsetup(txtDescriptionREmAPP.Text, chkself_Approval, txtLimitSelfREmAPR.Text, rm1_Approval, txtLimitRM1.Text, rm2_Approval, txtLimitRM2.Text, limitrem, AmountremOptPercentage, blankdataforper, AmountremOptFixed, blankdataforFixed, Session["idremnew"].ToString(), App_forRemTypemaster,dep,dep,dddisplaylimit.SelectedItem.Text,ddDisplayBalance.SelectedItem.Text);

              
              con.Update_tble_ReimBursementtype_MasteraccordingtoApproval(App_forRemTypemaster, dep, dep, txtTypeRemAPR.Text, Session["Company"].ToString());
            
            }

            if (App_forRemTypemaster == "Department")
            {
                string dep = "";
                con.Update_tble_Reimbursmentsetup(txtDescriptionREmAPP.Text, chkself_Approval, txtLimitSelfREmAPR.Text, rm1_Approval, txtLimitRM1.Text, rm2_Approval, txtLimitRM2.Text, limitrem, AmountremOptPercentage, blankdataforper, AmountremOptFixed, blankdataforFixed, Session["idremnew"].ToString(), App_forRemTypemaster, ddDepartmentRemApr.SelectedValue.ToString(), dep,dddisplaylimit.SelectedItem.Text,ddDisplayBalance.SelectedItem.Text);


                con.Update_tble_ReimBursementtype_MasteraccordingtoApproval(App_forRemTypemaster, ddDepartmentRemApr.SelectedValue.ToString(), dep, txtTypeRemAPR.Text, Session["Company"].ToString());

            }


            if (App_forRemTypemaster == "Individual")
            {
                string dep = "";
                con.Update_tble_Reimbursmentsetup(txtDescriptionREmAPP.Text, chkself_Approval, txtLimitSelfREmAPR.Text, rm1_Approval, txtLimitRM1.Text, rm2_Approval, txtLimitRM2.Text, limitrem, AmountremOptPercentage, blankdataforper, AmountremOptFixed, blankdataforFixed, Session["idremnew"].ToString(), App_forRemTypemaster, dep, ddIND_Employee.SelectedValue.ToString(), dddisplaylimit.SelectedItem.Text, ddDisplayBalance.SelectedItem.Text);
                con.DisConnect();

                con.Update_tble_ReimBursementtype_MasteraccordingtoApproval(App_forRemTypemaster, dep, ddIND_Employee.SelectedValue.ToString(), txtTypeRemAPR.Text, Session["Company"].ToString());
                con.DisConnect();
            }

            clearremNew();
            Show_Rem_NewApproval();
        }

    }


    public void ShowRem_Approval_type()
    { 
    SqlDataReader dr=con.ShowRem_Approval_type(Session["Company"].ToString());
    ddApprovalTypeMaster.DataSource = dr;
    ddApprovalTypeMaster.DataTextField = "Type";
    ddApprovalTypeMaster.DataValueField = "Type";
    ddApprovalTypeMaster.DataBind();
    dr.Close();
    con.DisConnect();
    
    }
    protected void lnkReimTypeMaster_Click(object sender, EventArgs e)
    {
        
        pnlCreate.Visible = false;
      
        pnlForAttendencesetup.Visible = false;
        pnlLeavesetup.Visible = false;
        pnlMailSetup.Visible = false;
        pnlReimbursementApproval.Visible = false;

        pnlReimTypeMaster.Visible = true;
        ShowRem_Approval_type();
        Show_Reim_master();
        clear_rem_Master();
        showGLAccountDebit();
        showGLAccountCredit();
    }


    public void Show_Reim_master()
    {

        SqlDataReader dr1 = con.ShowRem_Mastertble_ReimBursementtype_Master(Session["Company"].ToString());
        dr1.Read();
        if (dr1.HasRows)
        {
            dr1.Close();
            con.DisConnect();
            pnlReimTypeserach.Visible = true;
            SqlDataReader dr = con.ShowRem_Mastertble_ReimBursementtype_Master(Session["Company"].ToString());

            DataTable Dt = new DataTable();
            Dt.Load(dr);
            grdReimTypeMaster.DataSource = Dt;
            grdReimTypeMaster.DataBind();
            dr.Close();
            con.DisConnect();
        }

        else
        {
            dr1.Close();
            con.DisConnect();
            pnlReimTypeserach.Visible = false;
            SqlDataReader dr = con.ShowRem_Mastertble_ReimBursementtype_Master(Session["Company"].ToString());

            DataTable Dt = new DataTable();
            Dt.Load(dr);
            grdReimTypeMaster.DataSource = Dt;
            grdReimTypeMaster.DataBind();
            dr.Close();
            con.DisConnect();
        }
    }


    public void Show_Reim_mastersearch()
    {
        SqlDataReader dr = con.ShowRem_ReimTypeserach(Session["Company"].ToString(), txtReimTypeSearch.Text);

        DataTable Dt = new DataTable();
        Dt.Load(dr);
        grdReimTypeMaster.DataSource = Dt;
        grdReimTypeMaster.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void clear_rem_Master()
    { 
    txtRemTypeMaster.Text="";
        txtDescriptionRemTypeMaster.Text="";
        ddApprovalTypeMaster.Enabled = true;
        txtEffectiveReimMaster.Text = "";
        chkActiveReimMaster.Checked = true;
        chkActiveReimMaster.Enabled = false;
        btnSaveReim_Master.Visible = true;
        btnUpdate_ReimMaster.Visible = false;
        btnCancelReimMaster.Visible = false;
        txtRemTypeMaster.Enabled = true;
    
    }

    protected void btnSaveReim_Master_Click(object sender, EventArgs e)
    {

        SqlDataReader dr = con.ShowRem_Mastertble_ReimBursementtype_MasterRepetion(Session["Company"].ToString(), txtRemTypeMaster.Text.Trim(),ddApprovalTypeMaster.Text);
        dr.Read();
        if (dr.HasRows)
        {
            dr.Close();
            con.DisConnect();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Type Already Exhist');", true);

        
        }
        else
        {


            string drName = ddGLAccountDr.SelectedItem.Text.ToString().Substring(ddGLAccountDr.SelectedItem.Text.ToString().IndexOf(" . ") + 2);
            string CrName = ddGLAccountCr.SelectedItem.Text.ToString().Substring(ddGLAccountCr.SelectedItem.Text.ToString().IndexOf(" . ") + 2);
            dr.Close();
            con.DisConnect();
            SqlDataReader dra = con.SHow_Tyepfor_App_cable(ddApprovalTypeMaster.Text);
            dra.Read();
            string App_for_Approval = dra["Applicable_For"].ToString();
            string Department_apr = dra["Department"].ToString();
            string Individual_Userid_apr = dra["Individual_Userid"].ToString();
            string Approval_typeid = dra["id"].ToString();

            dra.Close();
            con.DisConnect();

            con.insert_tble_ReimBursementtype_Master(txtRemTypeMaster.Text, txtDescriptionRemTypeMaster.Text, ddApprovalTypeMaster.Text, "Yes", txtEffectiveReimMaster.Text, Session["Company"].ToString(), App_for_Approval, Department_apr, Individual_Userid_apr, Approval_typeid, drName.TrimStart(), ddGLAccountDr.SelectedValue.Trim(), CrName.TrimStart(), ddGLAccountCr.SelectedValue.Trim());
            con.DisConnect();

            Show_Reim_master();
            clear_rem_Master();
        }
    }
    protected void grdReimTypeMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdReimTypeMaster.PageIndex = e.NewPageIndex;
        Show_Reim_master();
    }
    protected void btnCancelReimMaster_Click(object sender, EventArgs e)
    {
        clear_rem_Master();
    }
    protected void btneditreim_master_Command(object sender, CommandEventArgs e)
    {
        chkActiveReimMaster.Enabled = true;
        btnSaveReim_Master.Visible = false;
        btnUpdate_ReimMaster.Visible = true;
        btnCancelReimMaster.Visible = true;
        txtRemTypeMaster.Enabled = false;
        ddApprovalTypeMaster.Enabled = false;
        string id = e.CommandArgument.ToString();
        Session["reim_Type_Master"] = id.ToString();
        SqlDataReader dr = con.ShowRem_Mastertble_ReimBursementtype_MasterID(id);
        dr.Read();
        if (dr.HasRows)
        { 
        txtRemTypeMaster.Text=dr["Reim_Type"].ToString();
        ddGLAccountDr.SelectedValue = dr["Gl_Account_Dr_Id"].ToString();
        ddGLAccountCr.SelectedValue = dr["Gl_Account_CR_ID"].ToString();
            txtDescriptionRemTypeMaster.Text=dr["Reim_Description"].ToString();
            ddApprovalTypeMaster.SelectedValue=dr["Approval_type"].ToString();
            string Activerem_type_Master = dr["Active"].ToString();
            if (Activerem_type_Master == "Yes")
            {
                chkActiveReimMaster.Checked = true;
            }
            if (Activerem_type_Master == "No")
            {
                chkActiveReimMaster.Checked = false;
            }
            string effectivedate = dr["Effective_date"].ToString();
            DateTime eff = Convert.ToDateTime(effectivedate);
            txtEffectiveReimMaster.Text = eff.ToString("yyyy-MM-dd");
           
        }
        dr.Close();
        con.DisConnect();
    }
    protected void btnUpdate_ReimMaster_Click(object sender, EventArgs e)
    {
        string activemaster="";
        if(chkActiveReimMaster.Checked==true)
        {
        activemaster="Yes";
        }

        if(chkActiveReimMaster.Checked==false)
        {
        activemaster="No";
        }


     
        string drName = ddGLAccountDr.SelectedItem.Text.ToString().Substring(ddGLAccountDr.SelectedItem.Text.ToString().IndexOf(" . ") + 2);
        string CrName = ddGLAccountCr.SelectedItem.Text.ToString().Substring(ddGLAccountCr.SelectedItem.Text.ToString().IndexOf(" . ") + 2);
        con.Update_tble_ReimBursementtype_Master(txtDescriptionRemTypeMaster.Text, ddApprovalTypeMaster.Text, activemaster, txtEffectiveReimMaster.Text, Session["reim_Type_Master"].ToString(), drName.TrimStart(), ddGLAccountDr.SelectedValue.Trim(), CrName.TrimStart(), ddGLAccountCr.SelectedValue.Trim());
        con.DisConnect();
        Show_Reim_master();
        clear_rem_Master();
    }

    protected void txtLimitRM2_TextChanged(object sender, EventArgs e)
    {
        if (chkSelfAPP.Checked == true && chkRM1.Checked == true || chkSelfAPP.Checked == false && chkRM1.Checked == true)
        {

            decimal rmlimit1 = Convert.ToDecimal(txtLimitRM1.Text);
            decimal rmlimit2 = Convert.ToDecimal(txtLimitRM2.Text);
            if (rmlimit1 >= rmlimit2)
            {

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Reporting Manager 2 limit will be greater than Reporting Manager 1 limit');", true);
                txtLimitRM2.Text = "0";
                txtLimitRM2.Enabled = false;
                chkRM2.Checked = false;
            }
            else
            {
                chkRM2.Checked = true;
            }
        }

        if (chkSelfAPP.Checked == true && chkRM1.Checked == false)
        {

            decimal selflimit = Convert.ToDecimal(txtLimitSelfREmAPR.Text);
            decimal rmlimit2 = Convert.ToDecimal(txtLimitRM2.Text);
            if (selflimit >= rmlimit2)
            {

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Reporting Manager 2 limit will be greater than Self Approval limit');", true);
                txtLimitRM2.Text = "0";
                txtLimitRM2.Enabled = false;
                chkRM2.Checked = false;
            }
            else
            {
                chkRM2.Checked = true;
            }
        }
        
        

    }
    protected void txtLimitSelfREmAPR_TextChanged(object sender, EventArgs e)
    {
      


        if (chkRM1.Checked == true && chkRM2.Checked == true)
        {
            decimal selflimit = Convert.ToDecimal(txtLimitSelfREmAPR.Text);
            decimal rmlimit = Convert.ToDecimal(txtLimitRM1.Text);
            if (selflimit >= rmlimit)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Self Approval limit will be less than reporting manager 1 limit');", true);
                txtLimitSelfREmAPR.Text = "0";
                txtLimitRM1.Enabled = false;
                chkRM1.Checked = false;
            }
        }


        if (chkRM1.Checked == false && chkRM2.Checked == true)
        {
            decimal selflimit = Convert.ToDecimal(txtLimitSelfREmAPR.Text);
            decimal rmlimit = Convert.ToDecimal(txtLimitRM2.Text);
            if (selflimit >= rmlimit)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Self Approval limit will be less than reporting manager 2 limit');", true);
                txtLimitSelfREmAPR.Text = "0";
                txtLimitRM1.Enabled = false;
                chkRM1.Checked = false;
            }
        }

        
    }
    protected void txtLimitRM1_TextChanged(object sender, EventArgs e)
    {

        if (chkSelfAPP.Checked == true && chkRM2.Checked==false)
        {

            decimal selflimit = Convert.ToDecimal(txtLimitSelfREmAPR.Text);
            decimal rmlimit = Convert.ToDecimal(txtLimitRM1.Text);
            if (selflimit >= rmlimit)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Reporting Manager 1 limit will be greater than self approval limit');", true);
                txtLimitRM1.Text = "0";
                txtLimitRM1.Enabled = false;
                chkRM1.Checked = false;
            }
        }

        if (chkSelfAPP.Checked == false && chkRM2.Checked == true)
        {

            decimal selflimit = Convert.ToDecimal(txtLimitRM2.Text);
            decimal rmlimit = Convert.ToDecimal(txtLimitRM1.Text);
            if (selflimit <= rmlimit)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Reporting Manager 1 limit will be less than Reporting Manager 2 limit');", true);
                txtLimitRM1.Text = "0";
                txtLimitRM1.Enabled = false;
                chkRM1.Checked = false;
            }
        }
        if (chkSelfAPP.Checked == true && chkRM2.Checked == true)
        {

            decimal selflimit = Convert.ToDecimal(txtLimitSelfREmAPR.Text);
            decimal rmlimit = Convert.ToDecimal(txtLimitRM1.Text);
            decimal rmlimit2 = Convert.ToDecimal(txtLimitRM2.Text);
            //if (selflimit <= rmlimit || rmlimit <= rmlimit2)
            if (rmlimit <= selflimit  )

            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Reporting Manager 1 limit will be greater than self approval limit');", true);
                txtLimitRM1.Text = "0";
                txtLimitRM1.Enabled = false;
                chkRM1.Checked = false;
            }
           
            if (rmlimit2 <= rmlimit)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Reporting Manager 1 limit will be less than Reporting Manager 2 limit');", true);
                txtLimitRM1.Text = "0";
                txtLimitRM1.Enabled = false;
                chkRM1.Checked = false;
               
            }
           
        }
    }
    protected void btn_Approval_search_Click(object sender, EventArgs e)
    {
        Show_Rem_Searchs();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
         SqlDataAdapter da;
        if (txtTypeapr.Text == "")
        {
           da = new SqlDataAdapter("select * from tble_Reimbursment_Approval_setup where  Company_Name='" + Session["Company"].ToString() + "' order by Type", con.Con);
           
        }
        else
        {
             da = new SqlDataAdapter("select * from tble_Reimbursment_Approval_setup where (UPPER([Type]) LIKE UPPER('%" + txtTypeapr.Text + "%')) and  Company_Name='" + Session["Company"].ToString() + "' order by Type ", con.Con);
        }
        DataSet ds = new DataSet();
        da.Fill(ds);

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);


        grdApprovalreimsetup.DataSource = ds;


        //   htmlWrite.WriteLine("hello");

        grdApprovalreimsetup.DataBind();//Data Grid Bind

        grdApprovalreimsetup.RenderControl(htmlWrite);

        Response.Clear();
      
        Response.AddHeader("content-disposition", "attachment;filename='ApprovalType'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();
        con.DisConnect();

    }
    protected void btnExporttoexcel_reimserch_Click(object sender, EventArgs e)
    {

        SqlDataAdapter da;
        if (txtReimTypeSearch.Text == "")
        {
            da = new SqlDataAdapter("select * from tble_ReimBursementtype_Master where  Company_Name='" + Session["Company"].ToString() + "' order by Reim_Type", con.Con);

        }
        else
        {
            //da = new SqlDataAdapter("select * from tble_Reimbursment_Approval_setup where (UPPER([Type]) LIKE UPPER('%" + txtTypeapr.Text + "%')) and  Company_Name='" + Session["Company"].ToString() + "' order by Type ", con.Con);
            da = new SqlDataAdapter("select * from tble_ReimBursementtype_Master where (UPPER([Reim_Type]) LIKE UPPER('%" + txtReimTypeSearch.Text + "%')) and  Company_Name='" + Session["Company"].ToString() + "' order by Reim_Type ", con.Con);
        }
        DataSet ds = new DataSet();
        da.Fill(ds);

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);


        grdReimtype_excel.DataSource = ds;


        //   htmlWrite.WriteLine("hello");

        grdReimtype_excel.DataBind();//Data Grid Bind

        grdReimtype_excel.RenderControl(htmlWrite);

        Response.Clear();

        Response.AddHeader("content-disposition", "attachment;filename='Reimbursement_Type'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();
        con.DisConnect();

    }
    protected void btnReimtypesearch_Click(object sender, EventArgs e)
    {
        Show_Reim_mastersearch();
    }

    private void showGLAccountDebit()
    {

        DataTable dt = new DataTable();
        string id = string.Empty;
        string name = string.Empty;
        string newName = string.Empty;
        string stable =Session["Company"].ToString();
        string rtable = stable.Replace(".", "_");
        string comanyname = "[" + rtable + "$G_L Account" + "]";


        string sqlStatement = "SELECT * FROM " + comanyname + " order by Name";
        SqlCommand sqlCmd = new SqlCommand(sqlStatement, Portalcon.Con);
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);

        sqlDa.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                id = dt.Rows[i]["No_"].ToString();

                name = dt.Rows[i]["Name"].ToString();
                newName = id + " . " + name;

                ddGLAccountDr.Items.Add(new ListItem(newName, id));

                ddGLAccountDr.SelectedValue = id;
            }
        }

    }

    private void showGLAccountCredit()
    {

        DataTable dt = new DataTable();
        string id = string.Empty;
        string name = string.Empty;
        string newName = string.Empty;
        string stable = Session["Company"].ToString();
        string rtable = stable.Replace(".", "_");
        string comanyname = "[" + rtable + "$G_L Account" + "]";


        string sqlStatement = "SELECT * FROM " + comanyname + " order by Name";
        SqlCommand sqlCmd = new SqlCommand(sqlStatement, Portalcon.Con);
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);

        sqlDa.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                id = dt.Rows[i]["No_"].ToString();

                name = dt.Rows[i]["Name"].ToString();
                newName = id + " . " + name;

                ddGLAccountCr.Items.Add(new ListItem(newName, id));

                ddGLAccountCr.SelectedValue = id;
            }
        }

    }

    protected void grdLeaveSetup_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdLeaveSetup.EditIndex = e.NewEditIndex;
        Showtble_LeavesetupClub();
    }
    protected void grdLeaveSetup_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DropDownList ddClub = grdLeaveSetup.Rows[e.RowIndex].FindControl("ddClubHolidays") as DropDownList;
        Label lblLeaveTypedd = grdLeaveSetup.Rows[e.RowIndex].FindControl("lblLeaveTypedd") as Label;

        SqlDataReader dr = con.Show_tble_leave_setupClub(Session["Company"].ToString(), lblLeaveTypedd.Text);
        dr.Read();
        if (dr.HasRows)
        {

            dr.Close();
            con.DisConnect();
            con.update_tble_leave_setup(ddClub.SelectedItem.Text, lblLeaveTypedd.Text, Session["Company"].ToString());
            con.DisConnect();
           
            //showLeaveSetup();
        }
        else
        {
            dr.Close();
        }
        grdLeaveSetup.EditIndex = -1;
        Showtble_LeavesetupClub();
       
    }
    protected void grdLeaveSetup_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdLeaveSetup.EditIndex = -1;
        Showtble_LeavesetupClub();
    }

    public void ShowCoLUpto()
    { 
    SqlDataReader dr = con.ShowCoLeaveupto(Session["Company"].ToString());
        dr.Read();
        if (dr.HasRows)
        {

            txtLeaveupto.Text = dr["Comp Leave Upto"].ToString();
        }
        dr.Close();
        con.DisConnect();
    }


    public void ShowLeaveuptodetails()
    {
        SqlDataReader dr = con.SHowCoupto(Session["Company"].ToString());
        DataTable dt = new DataTable();
        dt.Load(dr);
        grdLeaveuptodetails.DataSource = dt;
        grdLeaveuptodetails.DataBind();
        dr.Close();
        Portalcon.DisConnect();
       
    }


    protected void btnSaveleaveupto_Click(object sender, EventArgs e)
    {

        if (rdAllCOupto.Checked == true)
        {
            SqlDataReader dr = con.ShowCoLeaveupto(Session["Company"].ToString());
            dr.Read();
            if (dr.HasRows)
            {

                dr.Close();
                con.DisConnect();
                con.update_tble_Co_Leave_Upto(Convert.ToInt32(txtLeaveupto.Text.Trim()), Session["Company"].ToString());
                con.DisConnect();

                //showLeaveSetup();
            }
            else
            {
                dr.Close();
                con.DisConnect();
                con.Insert_tble_Co_Leave_Upto(Convert.ToInt32(txtLeaveupto.Text.Trim()), Session["Company"].ToString());
                con.DisConnect();

            }
        }
        if (rdINDCoupto.Checked == true)
        {
            SqlDataReader dr = con.ShowCoLeaveupto(Session["Company"].ToString());
            dr.Read();
            if (dr.HasRows)
            {

                dr.Close();
                con.DisConnect();
                con.update_tble_Co_Leave_UptoINDsetup(Convert.ToInt32(txtLeaveupto.Text.Trim()), Session["Company"].ToString(), txtEmployeeidcoupto.SelectedValue.Trim());
                con.DisConnect();

                //showLeaveSetup();
            }
            else
            {
                dr.Close();
                con.DisConnect();
                con.Insert_tble_Co_Leave_UptoINDSetup(Convert.ToInt32(txtLeaveupto.Text.Trim()), Session["Company"].ToString(), txtEmployeeidcoupto.SelectedValue.Trim());
                con.DisConnect();

            }
        }
        ShowLeaveuptodetails();
    }
    protected void rdINDCoupto_CheckedChanged(object sender, EventArgs e)
    {
        if (rdINDCoupto.Checked == true)
        {
            ShowEmployeeForLeaveUptoDropDown();
            txtEmployeeidcoupto.Enabled = true;
        }
       
    }
    protected void rdAllCOupto_CheckedChanged(object sender, EventArgs e)
    {
        if (rdINDCoupto.Checked == false)
        {
            txtEmployeeidcoupto.Enabled = false;
            txtEmployeeidcoupto.Text = "";
        }
    }
}