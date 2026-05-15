using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class AttendenceDetail : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal con;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Portalcon = new Connection();
            con = new ServicePoratal();
            //if (!IsPostBack)
            //{
            //   // showEmployee();
               
            //}
            
        }
        catch (Exception)
        {
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }
    }


    private void showEmployeeDetailsingrid()
    {

        DataTable dt = new DataTable();
        string id = string.Empty;
        string name = string.Empty;
        string newName = string.Empty;

        string sqlStatement = "";
        SqlCommand sqlCmd;
        SqlDataAdapter sqlDa;
        if (Session["UserType"].ToString() == "2")
        {

            sqlStatement = "SELECT * FROM " + Session["CompanyTableEmployee"].ToString() + "  where HR='" + Session["uid"].ToString() + "'";
            sqlCmd = new SqlCommand(sqlStatement, Portalcon.Con);
            sqlDa = new SqlDataAdapter(sqlCmd);

            sqlDa.Fill(dt);
            grdViewAttendancehod.DataSource = dt;
            grdViewAttendancehod.DataBind();

        }
        else
        {
            sqlStatement = "SELECT * FROM " + Session["CompanyTableEmployee"].ToString() + "  where HOD='" + Session["uid"].ToString() + "'";
            sqlCmd = new SqlCommand(sqlStatement, Portalcon.Con);
            sqlDa = new SqlDataAdapter(sqlCmd);

            sqlDa.Fill(dt);
            grdViewAttendancehod.DataSource = dt;
            grdViewAttendancehod.DataBind();

        }
     
    }



    private void showEmployeeDetailsingridFilterOption()
    {
       
        DataTable dt = new DataTable();
        string id = string.Empty;
        string name = string.Empty;
        string newName = string.Empty;

        string sqlStatement = "";
        SqlCommand sqlCmd;
        SqlDataAdapter sqlDa;
        if (Session["UserType"].ToString() == "2")
        {
            if (rdEmployeeid.Checked == true)
            {

                sqlStatement = "SELECT * FROM " + Session["CompanyTableEmployee"].ToString() + "  where HR='" + Session["uid"].ToString() + "' and [No_]='"+txtNameid.Text.Trim()+"'";
                sqlCmd = new SqlCommand(sqlStatement, Portalcon.Con);
                sqlDa = new SqlDataAdapter(sqlCmd);

                sqlDa.Fill(dt);
                grdViewAttendancehod.DataSource = dt;
                grdViewAttendancehod.DataBind();

                
            }

            if (rdEmployeeName.Checked == true)
            {
                sqlStatement = "SELECT * FROM " + Session["CompanyTableEmployee"].ToString() + "  where HR='" + Session["uid"].ToString() + "' and (UPPER([First Name]) LIKE UPPER('%" + txtNameid.Text + "%')) ";
                sqlCmd = new SqlCommand(sqlStatement, Portalcon.Con);
                sqlDa = new SqlDataAdapter(sqlCmd);

                sqlDa.Fill(dt);
                grdViewAttendancehod.DataSource = dt;
                grdViewAttendancehod.DataBind();


            }

            
        }
        else
        {
            if (rdEmployeeid.Checked == true)
            {
                sqlStatement = "SELECT * FROM " + Session["CompanyTableEmployee"].ToString() + "  where HOD='" + Session["uid"].ToString() + "' and [No_]='" + txtNameid.Text.Trim() + "'";
                sqlCmd = new SqlCommand(sqlStatement, Portalcon.Con);
                sqlDa = new SqlDataAdapter(sqlCmd);

                sqlDa.Fill(dt);
                grdViewAttendancehod.DataSource = dt;
                grdViewAttendancehod.DataBind();
            }

            if (rdEmployeeName.Checked == true)
            {
                sqlStatement = "SELECT * FROM " + Session["CompanyTableEmployee"].ToString() + "  where HOD='" + Session["uid"].ToString() + "' and (UPPER([First Name]) LIKE UPPER('%" + txtNameid.Text + "%'))";
                sqlCmd = new SqlCommand(sqlStatement, Portalcon.Con);
                sqlDa = new SqlDataAdapter(sqlCmd);

                sqlDa.Fill(dt);
                grdViewAttendancehod.DataSource = dt;
                grdViewAttendancehod.DataBind();
            }

        }

    }



    private void showEmployee()
    {

        DataTable dt = new DataTable();
        string id = string.Empty;
        string name = string.Empty;
        string newName = string.Empty;

        string sqlStatement = "";
        SqlCommand sqlCmd ;
        SqlDataAdapter sqlDa;
        if (Session["UserType"].ToString() == "2")
        {

            sqlStatement = "SELECT * FROM " + Session["CompanyTableEmployee"].ToString() + "  where HR='" + Session["uid"].ToString() + "'";
            sqlCmd = new SqlCommand(sqlStatement, Portalcon.Con);
            sqlDa = new SqlDataAdapter(sqlCmd);

            sqlDa.Fill(dt);
        }
        else
        {
            sqlStatement = "SELECT * FROM " + Session["CompanyTableEmployee"].ToString() + "  where HOD='" + Session["uid"].ToString() + "'";
            sqlCmd = new SqlCommand(sqlStatement, Portalcon.Con);
            sqlDa = new SqlDataAdapter(sqlCmd);

            sqlDa.Fill(dt);
        }
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                id = dt.Rows[i]["No_"].ToString();

                name = dt.Rows[i]["First Name"].ToString();
                newName = id + " --------- " + name;

               

               // ddEmployee.Items.Add(new ListItem(newName, id));
             
            }
        }

    }

    int expiryatt; string EmployeeIDday = ""; int EmployeeWiseNo_Of_Daysint; string ToTimeAll = ""; string FromTimeALL = ""; string optForFromTimeTotimeAll = "";
    public void showAttendenceExpiry()
    {
        SqlDataReader dr = con.Show_tbl_Attendence_Expiry(Session["Company"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            string attenexpprev1 = dr["No_of_Days"].ToString();
            expiryatt = Convert.ToInt32(attenexpprev1);
            FromTimeALL = dr["From_Time"].ToString();
            ToTimeAll = dr["To_Time"].ToString();
          
            optForFromTimeTotimeAll = dr["Option_fromTime_toime_All"].ToString();
          
        }
        dr.Read();
        con.DisConnect();
    }


    string optForFromTimeTotimeind = "";
    public void showAttendenceExpiryIND()
    {
        SqlDataReader dr = con.Show_tbl_Attendence_ExpiryIND(Session["Company"].ToString(), Session["viewatthod"].ToString());
        dr.Read();
        if (dr.HasRows)
        {

            string EmployeeWiseNo_Of_Days = dr["Duration"].ToString();
            if (EmployeeWiseNo_Of_Days == "")
            {
                EmployeeWiseNo_Of_Days = "0";
            }
            EmployeeWiseNo_Of_Daysint = Convert.ToInt32(EmployeeWiseNo_Of_Days);
            EmployeeIDday = dr["EmployeeID"].ToString();

          
            optForFromTimeTotimeind = dr["Option_fromTime_toime_Individual"].ToString();
          
        }
        else
        {

           
            optForFromTimeTotimeind = optForFromTimeTotimeAll;
        }

        dr.Read();
        con.DisConnect();
    }
    public DataTable GetDatesofAttendence()
    {
        DataTable dt = new DataTable();
        SqlCommand sqlCmd = new SqlCommand("select CONVERT(date, [Atte_Date],103) as Atte_Date from tbl_attendence where Userid='" + Session["viewatthod"].ToString() + "' and CompanyName='" + Session["Company"].ToString() + "' and [Rejected Approval]='No' and ApprovalStatus!='Rejected' and Status='Present'", con.Con);
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        sqlDa.Fill(dt);
        return dt;
    }

    public DataTable GetDatesofAttendenceApproved()
    {
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        string Pay_Daily_Attendence_Detail_tble = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
        DataTable dt = new DataTable();
        SqlCommand sqlCmd = new SqlCommand("select CONVERT(date, [Attendance Date],103) as AtteNdnce_Date from " + Pay_Daily_Attendence_Detail_tble + " where [Attendance Marked]='1' and Status='1' and [Employee Code]='" + Session["viewatthod"].ToString() + "'", Portalcon.Con);
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        sqlDa.Fill(dt);
        return dt;
    }

    public DataTable GetDatesofLeaveApproved()
    {
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        string Pay_Daily_Attendence_Detail_tble = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
        DataTable dt = new DataTable();
        SqlCommand sqlCmd = new SqlCommand("select CONVERT(date, [Attendance Date],103) as AtteNdnce_Date from " + Pay_Daily_Attendence_Detail_tble + " where [Applied Leave]='1'  and Status!='1'  and [Employee Code]='" + Session["viewatthod"].ToString() + "'", Portalcon.Con);
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        sqlDa.Fill(dt);
        return dt;
    }

    public DataTable GetDatesofLeaveApprovedHalf()
    {
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        string Pay_Daily_Attendence_Detail_tble = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
        DataTable dt = new DataTable();
        SqlCommand sqlCmd = new SqlCommand("select CONVERT(date, [Attendance Date],103) as AtteNdnce_Date from " + Pay_Daily_Attendence_Detail_tble + " where [Applied Leave]='1' and [Leave Type]='2' and [Employee Code]='" + Session["viewatthod"].ToString() + "'", Portalcon.Con);
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        sqlDa.Fill(dt);
        return dt;
    }
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        try
        {
            if (EmployeeIDday == Session["viewatthod"].ToString())
            {
                if ((e.Day.Date > System.DateTime.Now.AddDays(0)))
                {
                    e.Day.IsSelectable = false;
                    e.Cell.Font.Bold = false;
                    e.Cell.BorderColor = System.Drawing.Color.Pink;
                    e.Cell.ForeColor = System.Drawing.Color.Wheat;
                }
                if ((e.Day.Date < System.DateTime.Now.AddDays(-(EmployeeWiseNo_Of_Daysint))))
                {
                    e.Day.IsSelectable = false;
                    e.Cell.Font.Bold = false;
                    e.Cell.BorderColor = System.Drawing.Color.Pink;
                    //e.Cell.ForeColor = System.Drawing.Color.Red;
                    e.Cell.BackColor = System.Drawing.Color.Gray;
                }

            }
            else
            {
                if ((e.Day.Date > System.DateTime.Now.AddDays(0)))
                {
                    e.Day.IsSelectable = true;
                    e.Cell.Font.Bold = false;
                    e.Cell.BorderColor = System.Drawing.Color.Pink;
                    e.Cell.ForeColor = System.Drawing.Color.Wheat;
                }
                if ((e.Day.Date < System.DateTime.Now.AddDays(-(expiryatt))))
                {
                    e.Day.IsSelectable = false;
                    e.Cell.Font.Bold = false;
                    e.Cell.BorderColor = System.Drawing.Color.Pink;
                    //e.Cell.ForeColor = System.Drawing.Color.Red;
                    e.Cell.BackColor = System.Drawing.Color.Gray;

                }
            }
            DataTable dt = GetDatesofAttendence();
            DateTime eventDate;
            string eventType = String.Empty;
            if ((dt.Rows.Count > 0))
            {
                int i = 0;
                while ((i < dt.Rows.Count))
                {
                    eventDate = Convert.ToDateTime(dt.Rows[i]["Atte_Date"]);

                    if ((e.Day.Date == eventDate))
                    {
                        //e.Cell.ForeColor = System.Drawing.Color.Green;
                        e.Cell.BackColor = System.Drawing.Color.LawnGreen;
                        e.Cell.Font.Bold = true;
                        e.Day.IsSelectable = true;
                    }
                    i = (i + 1);
                }
            }
            //Approval Date
            try
            {

                DataTable dtapro = GetDatesofAttendenceApproved();
                DateTime eventDateapro;
                string eventTypeapro = String.Empty;
                if ((dtapro.Rows.Count > 0))
                {
                    int ij = 0;
                    while ((ij < dtapro.Rows.Count))
                    {
                        eventDateapro = Convert.ToDateTime(dtapro.Rows[ij]["AtteNdnce_Date"]);

                        if ((e.Day.Date == eventDateapro))
                        {
                            e.Cell.BackColor = System.Drawing.Color.SandyBrown;
                            e.Cell.Font.Bold = true;
                            e.Day.IsSelectable = true;
                        }
                        ij = (ij + 1);
                    }
                }
            }
            catch (Exception)
            { }


            try
            {

                DataTable dtaprolea = GetDatesofLeaveApproved();
                DateTime eventDateaprolea;
                string eventTypeaprolea = String.Empty;
                if ((dtaprolea.Rows.Count > 0))
                {
                    int ij = 0;
                    while ((ij < dtaprolea.Rows.Count))
                    {
                        eventDateaprolea = Convert.ToDateTime(dtaprolea.Rows[ij]["AtteNdnce_Date"]);

                        if ((e.Day.Date == eventDateaprolea))
                        {
                            //e.Cell.BackColor = System.Drawing.Color.YellowGreen;
                            e.Cell.BackColor = System.Drawing.Color.LightBlue;
                            e.Cell.Font.Bold = true;
                            e.Day.IsSelectable = false;
                        }
                        ij = (ij + 1);
                    }
                }
            }
            catch (Exception)
            { }

            //try
            //{

            //    DataTable dtaprolea = GetDatesofLeaveApprovedHalf();
            //    DateTime eventDateaprolea;
            //    string eventTypeaprolea = String.Empty;
            //    if ((dtaprolea.Rows.Count > 0))
            //    {
            //        int ij = 0;
            //        while ((ij < dtaprolea.Rows.Count))
            //        {
            //            eventDateaprolea = Convert.ToDateTime(dtaprolea.Rows[ij]["AtteNdnce_Date"]);

            //            if ((e.Day.Date == eventDateaprolea))
            //            {
            //                e.Cell.BackColor = System.Drawing.Color.Aqua;

            //                e.Cell.Font.Bold = true;
            //                e.Day.IsSelectable = true;
            //            }
            //            ij = (ij + 1);
            //        }
            //    }
            //}
            //catch (Exception)
            //{ }
            ModalPopupExtender1.Show();
        }
        catch (Exception)
        { }

    }
   
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {

        SqlDataReader dr = con.ShowTimeOf_Attendance(Session["Company"].ToString(), Session["viewatthod"].ToString(), Calendar1.SelectedDate.ToString("yyyy-MM-dd"));
        dr.Read();
        if (dr.HasRows)
        {
            pnlsetupattendance.Visible = true;
            lblFromTime.Text = dr["fromTime"].ToString();
            lbltoTime.Text = dr["ToTime"].ToString();
            txtRemarks.Text = dr["Remarks"].ToString();
            if (EmployeeIDday == Session["viewatthod"].ToString())
            {
                lblfromTimetotime.Text = optForFromTimeTotimeind.ToString();
                lblMarkupto.Text = EmployeeWiseNo_Of_Daysint.ToString() + "  Days  ";
            }
            if (EmployeeIDday != Session["viewatthod"].ToString())
            {
                lblfromTimetotime.Text = optForFromTimeTotimeAll.ToString();
                lblMarkupto.Text = expiryatt.ToString() + "  Days  ";
            }
            lblselecteddate.Text = Calendar1.SelectedDate.ToString("dd-MM-yyyy");
        }
        else
        {
            pnlsetupattendance.Visible = false;
        }
        dr.Close();
        con.DisConnect();

        showAttendenceExpiry();
        showAttendenceExpiryIND();
        ModalPopupExtender1.Show();
    }
    string tble_Pay_Employee_Leave_Entitled = "";
    public void showLeaveBlance()
    {
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        tble_Pay_Employee_Leave_Entitled = "[" + rccname + "$Pay Employee Leave Entitled" + "]";

        SqlDataReader dr = Portalcon.ShowLeaveBalance_Detail(tble_Pay_Employee_Leave_Entitled, Session["viewatthod"].ToString());
        DataTable Dt = new DataTable();
        Dt.Load(dr);
        grdleave.DataSource = Dt;
        grdleave.DataBind();
        dr.Close();
    }
    //protected void btnLeaveViewSearch_Click(object sender, EventArgs e)
    //{
    //    pnlsetupattendance.Visible = false;
    //    showLeaveBlance();
    //}
    protected void btnviewAttendanced_Command(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        Session["viewatthod"] = id.ToString();
        showNoofworkingdays();
        showNoofpresentdays();
        showNoofleavedays();
        showAttendenceExpiry();
        showAttendenceExpiryIND();
        pnlsetupattendance.Visible = false;
        showLeaveBlance();
        ShowAllattendanceposted();
        ShowCardTimechangedAttendance();
        ModalPopupExtender1.Show();
    }
    protected void ddEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    public void showNoofworkingdays()
    {
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        string Pay_Daily_Attendence_Detail_tble = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
        SqlDataReader dr = Portalcon.count_workingdays(Pay_Daily_Attendence_Detail_tble, txtfromDate.Text, txtTodate.Text, Session["viewatthod"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            lblworkingdays.Text = dr["Attendance Date"].ToString();
        }
        dr.Close();
        Portalcon.DisConnect();

    }


    public void showNoofpresentdays()
    {
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        string Pay_Daily_Attendence_Detail_tble = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
        SqlDataReader dr = Portalcon.count_persentdays(Pay_Daily_Attendence_Detail_tble, txtfromDate.Text, txtTodate.Text, Session["viewatthod"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            lblpresent.Text = dr["Attendance Date"].ToString();
        }
        dr.Close();
        Portalcon.DisConnect();

    }

    public void showNoofleavedays()
    {
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        string Pay_Daily_Attendence_Detail_tble = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
        SqlDataReader dr = Portalcon.count_Leavedays(Pay_Daily_Attendence_Detail_tble, txtfromDate.Text, txtTodate.Text, Session["viewatthod"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            lblLeave.Text = dr["Attendance Date"].ToString();
        }
        dr.Close();
        Portalcon.DisConnect();

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (txtfromDate.Text == "" || txtTodate.Text == "")
        { }
        else
        {
            showEmployeeDetailsingrid();
            pnlFilter.Visible = true;

            lblDaterange.Text = txtfromDate.Text + "   To   " + txtTodate.Text;
        }
        //showNoofworkingdays();
        //showNoofpresentdays();
        //showNoofleavedays();
        //showAttendenceExpiry();
        //showAttendenceExpiryIND();
        //pnlsetupattendance.Visible = false;
        //showLeaveBlance();
        //pnlactivitydata.Visible = true;
        //ModalPopupExtender1.Show();
    }
    protected void grdViewAttendancehod_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Label lblEmpCodeGrid = (Label)e.Row.FindControl("lblEmpCodeGrid");
            Label lblPresentgrid = (Label)e.Row.FindControl("lblPresentgrid");
            Label lblLeaveGrid = (Label)e.Row.FindControl("lblLeaveGrid");
            Label lblworkingDaysGrid = (Label)e.Row.FindControl("lblworkingDaysGrid");
            string ccname = Session["Company"].ToString();
            string rccname = ccname.Replace(".", "_");
            string Pay_Daily_Attendence_Detail_tble = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
            SqlDataReader dr = Portalcon.count_persentdays(Pay_Daily_Attendence_Detail_tble, txtfromDate.Text, txtTodate.Text, lblEmpCodeGrid.Text);
            dr.Read();
            if (dr.HasRows)
            {
                lblPresentgrid.Text = dr["Attendance Date"].ToString();
            }
            dr.Close();
            Portalcon.DisConnect();

            SqlDataReader dr1 = Portalcon.count_Leavedays(Pay_Daily_Attendence_Detail_tble, txtfromDate.Text, txtTodate.Text, lblEmpCodeGrid.Text);
            dr1.Read();
            if (dr1.HasRows)
            {
                lblLeaveGrid.Text = dr1["Attendance Date"].ToString();
            }
            dr1.Close();
            Portalcon.DisConnect();


            SqlDataReader dr2 = Portalcon.count_workingdays(Pay_Daily_Attendence_Detail_tble, txtfromDate.Text, txtTodate.Text, lblEmpCodeGrid.Text);
            dr2.Read();
            if (dr2.HasRows)
            {
                lblworkingDaysGrid.Text = dr2["Attendance Date"].ToString();
            }
            dr2.Close();
            Portalcon.DisConnect();
        }
    }
    protected void grdviewApprovalofnav_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdviewApprovalofnav.PageIndex = e.NewPageIndex;
        ShowAllattendanceposted();
        ModalPopupExtender1.Show();
    }
    public void ShowAllattendanceposted()
    {
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
       string tblenameAttendence = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
       SqlDataReader dr = Portalcon.Show_AllAttendancedata(tblenameAttendence, txtfromDate.Text, txtTodate.Text, Session["viewatthod"].ToString());
        DataTable dt = new DataTable();
        dt.Load(dr);
        grdviewApprovalofnav.DataSource = dt;
        grdviewApprovalofnav.DataBind();
        dr.Close();
        Portalcon.DisConnect();
    }


    public void ShowCardTimechangedAttendance()
    {
        
        SqlDataReader dr = con.ShowCardChangedtimeAttend(Session["viewatthod"].ToString(),txtfromDate.Text, txtTodate.Text,Session["Company"].ToString() );
        DataTable dt = new DataTable();
        dt.Load(dr);
        grdcardtimechanged.DataSource = dt;
        grdcardtimechanged.DataBind();
        dr.Close();
        con.DisConnect();
    }

    protected void grdviewApprovalofnav_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Label lblStatusStatus = (Label)e.Row.FindControl("lblStatusStatus");
            Label lbltimeinnav = (Label)e.Row.FindControl("lbltimeinnav");
            Label lbltimeoutnav = (Label)e.Row.FindControl("lbltimeoutnav");

            if (lbltimeinnav.Text == lbltimeoutnav.Text)
            {
                lbltimeinnav.Text = "";
                lbltimeoutnav.Text = "";
            }
            if (lbltimeinnav.Text != lbltimeoutnav.Text)
            {

            }

            if (lblStatusStatus.Text == "1")
            {
                lblStatusStatus.Text = "Present";
            }

            if (lblStatusStatus.Text == "2")
            {
                lblStatusStatus.Text = "Holiday";
            }
            if (lblStatusStatus.Text == "3")
            {
                lblStatusStatus.Text = "Off-Day";
            }
            if (lblStatusStatus.Text == "4")
            {
                lblStatusStatus.Text = "Leave";
            }
            if (lblStatusStatus.Text == "5")
            {
                lblStatusStatus.Text = "LWP(Half-Day)";
            }

            if (lblStatusStatus.Text == "6")
            {
                lblStatusStatus.Text = "LWP(Full-Day)";
            }

            if (lblStatusStatus.Text == "7")
            {
                lblStatusStatus.Text = "Special Leave";
            }
        }

    }
    protected void lnkClose_Click(object sender, EventArgs e)
    {
        showEmployeeDetailsingrid();

        lblDaterange.Text = txtfromDate.Text + "   To   " + txtTodate.Text;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        showEmployeeDetailsingridFilterOption();
    }
    protected void grdcardtimechanged_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdcardtimechanged.PageIndex = e.NewPageIndex;
        ShowCardTimechangedAttendance();
    }
    protected void grdcardtimechanged_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Updated time of Card Attendance";
            HeaderCell.ColumnSpan = 5;
            HeaderGridRow.Cells.Add(HeaderCell);
            //HeaderGridRow.BackColor = System.Drawing.Color.Wheat;
            //HeaderGridRow.ForeColor = System.Drawing.Color.White;


            HeaderCell.CssClass = "HeaderStyleofgrid";
            HeaderGridRow.Cells.Add(HeaderCell);
            //HeaderCell = new TableCell();
            //HeaderCell.Text = "Date";
            //HeaderCell.ColumnSpan = 2;
            //HeaderGridRow.Cells.Add(HeaderCell);

            grdcardtimechanged.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
}