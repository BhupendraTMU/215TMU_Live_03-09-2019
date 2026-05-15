using System;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data.OleDb;




public partial class Faculty_ODApplication : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal con;
    Connection navconn;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Portalcon = new Connection();
            con = new ServicePoratal();
            navconn = new Connection();

            if (!IsPostBack)
            {

                if (Session["uid"].ToString() == "TMU00049" || Session["uid"].ToString() == "TMU06618" || Session["uid"].ToString() == "TMU00075" || Session["uid"].ToString() == "TMU02444" || Session["uid"].ToString() == "TMU07987" || Session["uid"].ToString() == "TMU00142" || Session["uid"].ToString() == "TMU08719")
                {
                    if (Session["uid"].ToString() == "TMU00049")
                    {
                        pnlApproval.Visible = true;
                        pnlODApplication.Visible = false;
                        lnkODApplication.Visible = false;
                        lnkApproval.Text = "Approval";
                        btnReject.Visible = true;
                        btnApprove.Visible = true;
                        Show_HODData();

                    }

                    if (Session["uid"].ToString() == "TMU06618" || Session["uid"].ToString() == "TMU02444" || Session["uid"].ToString() == "TMU00075" || Session["uid"].ToString() == "TMU07987" || Session["uid"].ToString() == "TMU00142" || Session["uid"].ToString() == "TMU08719")
                    {
                        SetInitialRow();
                    }
                    if (Session["uid"].ToString() == "TMU07987" || Session["uid"].ToString() == "TMU08719")
                    {
                        lnkApproval.Visible = false;
                        lblCountODAppoval.Visible = false;
                        pnlViewStatus.Visible = false;
                        pnlODApplication.Visible = true;
                        lnkODApplication.Visible = true;
                    }

                }
                else
                {
                    Response.Redirect("../Default.aspx");
                }




            }

            VisiblilitybyHOD();
            ShowPendingApprovalCount();


        }
        catch (Exception)
        {
            Response.Redirect("../Default.aspx");
        }
    }

    public void SMS(String MobileNo, string Msg)
    {
        //  Website: http://www.universalsmsadvertising.com/     //  Username: 9837016352   //  Password: 9837016352 
        MobileNo = "91" + MobileNo;
        // MobileNo = "91" + 9015762885;
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
    public void SendSMSHODExam(string uname, string FromDate, string ToDate)
    {
        string tablenameemployeedata = "";
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");

        tablenameemployeedata = "[" + rccname + "$Employee" + "]";
        string mobilnoemp = "";

        string smsdata = "Dear sir your faculty " + uname + ",  OD  from " + FromDate + " to " + ToDate + " has been applied. " + "Thanks ";



        // As per Subham Gupta 29-12-2018
        SqlDataReader dr = Portalcon.Show_AthorityNo(Session["uid"].ToString(), tablenameemployeedata);
        dr.Read();
        if (dr.HasRows)
        {
            mobilnoemp = dr["MobilePhoneNo"].ToString();
            dr.Close();
            Portalcon.DisConnect();
            if (mobilnoemp.Trim() == "")
            { }
            else
            {
                try
                {
                    SMS(mobilnoemp, smsdata);
                }
                catch (Exception)
                {

                }
            }
        }
        else
        {
            dr.Close();
            Portalcon.DisConnect();
        }
    }



    public void sendAproved(string FromDate, string todate, string Eid, string Status)
    {
        string tablenameemployeedata = "";
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");

        tablenameemployeedata = "[" + rccname + "$Employee" + "]";
        string mobilnoemp = "";

        string smsdata = "Dear Applicant your OD leave from " + FromDate + " to " + todate + " has been " + Status + ".";

        // As per Subham Gupta 29-12-2018

        SqlDataReader dr = Portalcon.SHow_EmployeeMobileNo(Eid, tablenameemployeedata);
        dr.Read();
        if (dr.HasRows)
        {
            mobilnoemp = dr["MobilePhoneNo"].ToString();
            dr.Close();
            Portalcon.DisConnect();
            if (mobilnoemp.Trim() == "")
            { }
            else
            {
                try
                {
                    SMS(mobilnoemp, smsdata);
                }
                catch (Exception)
                {

                }
            }
        }
        else
        {
            dr.Close();
            Portalcon.DisConnect();
        }
    }




    protected void lnkODApplication_Click(object sender, EventArgs e)
    {
        lblHeader.Text = "Application";
        pnlViewStatus.Visible = false;
        pnlODApplication.Visible = true;
        pnlApproval.Visible = false;
        lblODSuccess.Visible = false;
        clear();
    }
    public void VisiblilitybyHOD()
    {

        if (Session["uid"].ToString() == "TMU00075")
        {
            lnkApproval.Visible = true;
            lblCountODAppoval.Visible = true;
        }
        else if (Session["uid"].ToString() == "TMU07987" || Session["uid"].ToString() == "TMU08719")
        {
            lnkApproval.Visible = false;
            lblCountODAppoval.Visible = false;
            pnlViewStatus.Visible = false;
        }
        else
        {

            lnkApproval.Visible = false;
            lblCountODAppoval.Visible = false;
        }
    }

    protected void lnkODView_Click(object sender, EventArgs e)
    {
        lblHeader.Text = "Report";
        pnlViewStatus.Visible = true;
        pnlODApplication.Visible = false;
        pnlApproval.Visible = false;
        txtFromDate_ViewStatus.Text = "";
        txtTodate_ViewStatus.Text = "";
        ddStatus_ViewStatus.SelectedValue = "Pending";
        Show_ODStatusByOwn();
        grdView_Status.Columns[8].Visible = false;
        grdView_Status.Columns[9].Visible = false;

    }
    protected void lnkApproval_Click(object sender, EventArgs e)
    {

        btnApprove.Text = "Approval";
        lblHeader.Text = "Approval";
        lnkApproval.Text = "Approval";

        pnlViewStatus.Visible = false;
        pnlODApplication.Visible = false;
        pnlApproval.Visible = true;
        btnFIlterGet_Approval.Visible = false;
        ddStatus_Approval.SelectedValue = "Pending";
        pnlFilterDate.Visible = false;
        pnlFilterByIDName.Visible = false;
        rdDatewise.Checked = false;
        rdEmployeeID.Checked = false;
        rdEmployeeName.Checked = false;
        Show_HODData();


        grdApproval.Columns[0].Visible = true;
        grdApproval.Columns[11].Visible = false;
        ddStatus_Approval.Enabled = false;

    }
    protected void rdEmployeeID_CheckedChanged(object sender, EventArgs e)
    {
        pnlFilterByIDName.Visible = true;
        pnlFilterDate.Visible = false;
        btnFIlterGet_Approval.Visible = true;
        lblEmployeeIDNameText.Text = "Employee ID";
        ddStatus_Approval.Enabled = true;
    }
    protected void rdEmployeeName_CheckedChanged(object sender, EventArgs e)
    {
        pnlFilterByIDName.Visible = true;
        pnlFilterDate.Visible = false;
        btnFIlterGet_Approval.Visible = true;
        lblEmployeeIDNameText.Text = "Employee Name";
        ddStatus_Approval.Enabled = true;
    }
    protected void rdDatewise_CheckedChanged(object sender, EventArgs e)
    {
        pnlFilterByIDName.Visible = false;
        pnlFilterDate.Visible = true;
        btnFIlterGet_Approval.Visible = true;
        ddStatus_Approval.Enabled = true;
    }
    protected void btnFIlterGet_Approval_Click(object sender, EventArgs e)
    {
        if (ddStatus_Approval.SelectedValue == "Pending")
        {
            grdApproval.Columns[0].Visible = true;
            grdApproval.Columns[11].Visible = false;
        }


        if (ddStatus_Approval.SelectedValue == "Rejected")
        {
            grdApproval.Columns[0].Visible = false;
            grdApproval.Columns[11].Visible = true;
        }

        Show_HODData();
    }

    protected void btnGet_ViewStatus_Click(object sender, EventArgs e)
    {
        if (ddStatus_ViewStatus.SelectedValue == "Rejected" || ddStatus_ViewStatus.SelectedValue == "All")
        {
            grdView_Status.Columns[8].Visible = true;
            grdView_Status.Columns[9].Visible = true;

            Show_ODStatusByOwn();

        }
        else
        {


            if (ddStatus_ViewStatus.SelectedValue == "Recommend")
            {
                grdView_Status.Columns[8].Visible = true;
                grdView_Status.Columns[9].Visible = false;
            }
            if (ddStatus_ViewStatus.SelectedValue == "Approved")
            {
                grdView_Status.Columns[8].Visible = true;
                grdView_Status.Columns[9].Visible = false;
            }
            if (ddStatus_ViewStatus.SelectedValue == "Pending")
            {
                grdView_Status.Columns[8].Visible = false;
                grdView_Status.Columns[9].Visible = false;
            }


            Show_ODStatusByOwn();

        }

    }









    protected void btnSendForApproval_Click(object sender, EventArgs e)
    {


        for (int i = 0; i < grdTempLeave.Rows.Count; i++)
        {
            try
            {
                Label FromDate = (Label)grdTempLeave.Rows[i].Cells[3].FindControl("lblFromDate");
                Label ToDate = (Label)grdTempLeave.Rows[i].Cells[4].FindControl("lblToDate");
                SqlDataAdapter da = new SqlDataAdapter("SELECT [Payroll Processing Month Date] FROM [EDUCOLLEGELIVE-R2].dbo.[TMU$Pay Company Policy] WHERE ((MONTH([Payroll Processing Month Date]) = MONTH('" + FromDate.Text + "') AND YEAR([Payroll Processing Month Date]) = YEAR('" + FromDate.Text + "'))  and   (MONTH([Payroll Processing Month Date]) = MONTH('" + ToDate.Text + "') AND YEAR(Payroll Processing Month Date]) = YEAR('" + ToDate.Text + "'))) select DATENAME(MONTH, [Payroll Processing Month Date]) + ' ' +   CAST(YEAR([Payroll Processing Month Date]) AS VARCHAR(4)) AS MonthYear FROM [EDUCOLLEGELIVE-R2].dbo.[TMU$Pay Company Policy] ", con.Con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0 && FromDate.Text!="")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You can Apply OD for month and year " + ds.Tables[1].Rows[0]["MonthYear"] + " !!');", true);
                    return;
                }


            }
            catch
            {

            }

        }

        for (int i = 0; i < grdTempLeave.Rows.Count; i++)
        {

            Label Userid = (Label)grdTempLeave.Rows[i].Cells[1].FindControl("lblEmployeeCode");
            if (Userid.Text != "")
            {
                try
                {
                    Label FromDate = (Label)grdTempLeave.Rows[i].Cells[3].FindControl("lblFromDate");
                    Label ToDate = (Label)grdTempLeave.Rows[i].Cells[4].FindControl("lblToDate");
                    Label LeaveType = (Label)grdTempLeave.Rows[i].Cells[5].FindControl("lblLeaveType");
                    Label Destination = (Label)grdTempLeave.Rows[i].Cells[6].FindControl("lblDestination");
                    Label Purpose = (Label)grdTempLeave.Rows[i].Cells[7].FindControl("lblPurpose");
                    Label Remark = (Label)grdTempLeave.Rows[i].Cells[8].FindControl("lblRemark");
                    Insert_tbl_OD_Application(Userid.Text.ToUpper(), "", FromDate.Text.Trim(), "00:00", ToDate.Text.Trim(), "00:00", Destination.Text.Trim(), Purpose.Text.Trim(), Remark.Text.Trim(), System.DateTime.Now.ToString("yyyy-MM-dd"), "", "", Session["Company"].ToString());

                }
                catch (Exception ex)
                {
                    lblODSuccess.ForeColor = System.Drawing.Color.Red;
                    lblODSuccess.Text = "Some thing went wrong.";
                    lblODSuccess.Visible = true;
                }
            }


        }
        lblODSuccess.Visible = true;
        grdApproval.DataSource = "";
        grdApproval.DataBind();




        //if (DuplicateOD() > 0)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You have already Applied  ! check the OD Applied date');", true);
        //    return;
        //}
        //else
        //{
        //    string ccname = Session["Company"].ToString();
        //    string rccname = ccname.Replace(".", "_");
        //    string Pay_Daily_Attendence_Detail = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
        //    SqlDataReader dr = navconn.Show_LeaveAppliedFOROD(Pay_Daily_Attendence_Detail, txtFromDate.Text.Trim(), txtTodate.Text.Trim(), txtEmployeeCode.Text.ToUpper());
        //    dr.Read();
        //    if (dr.HasRows)
        //    {
        //        dr.Close();
        //        navconn.DisConnect();
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You have already Applied Leave ! check the OD Applied date from Leave Date');", true);
        //    }
        //    else
        //    {
        //        dr.Close();
        //        navconn.DisConnect();

        //Insert_tbl_OD_Application(txtEmployeeCode.Text.ToUpper(), "", txtFromDate.Text.Trim(), "00:00", txtTodate.Text.Trim(), txtToTime.Text.Trim(), txtDestination.Text.Trim(), txtPurpose.Text.Trim(), txtRemarks.Text.Trim(), System.DateTime.Now.ToString("yyyy-MM-dd"), "", "", Session["Company"].ToString());

        //lblODSuccess.Visible = true;
        //SendSMSHODExam(txtEmployeeCode.Text.ToUpper(), txtFromDate.Text.Trim(), txtTodate.Text.Trim());



        //clear();
        //    }
        //}
    }
    public void Insert_tbl_OD_Application(string EmployeeNo, string EmployeeName, string FromDate, string FromTime, string ToDate, string ToTime, string Destination, string Purpose, string Remarks, string CreatedDate, string HOD, string HOD1, string Company)
    {
        con1.Open();
        string sqlq = "";
        if (txtDocNo.Text == "")
        {
            sqlq = "insert into tbl_OD_Application([Employee No],[Employee Name],[From Date],[From Time],[To Date],[To Time],[Destination],[Purpose],Remarks,[Created Date],HOD,HOD1,Company,SpecialODApproval,DocumentNo,ApplyById) values('" + EmployeeNo.ToUpper() + "',(select [First Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_='" + EmployeeNo.ToUpper() + "'), '" + FromDate + "', '" + FromTime + "','" + ToDate + "','" + ToTime + "', '" + Destination + "', '" + Purpose + "', '" + Remarks + "',getdate(),'" + HOD + "','" + HOD1 + "','" + Company + "','TMU00049','','" + Session["uid"].ToString() + "') ";
        }
        else
        {
            sqlq = "insert into tbl_OD_Application([Employee No],[Employee Name],[From Date],[From Time],[To Date],[To Time],[Destination],[Purpose],Remarks,[Created Date],HOD,HOD1,Company,SpecialODApproval,DocumentNo,ApplyById) values('" + EmployeeNo.ToUpper() + "',(select [First Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_='" + EmployeeNo.ToUpper() + "'), '" + FromDate + "', '" + FromTime + "','" + ToDate + "','" + ToTime + "', '" + Destination + "', '" + Purpose + "', '" + Remarks + "',getdate(),'" + HOD + "','" + HOD1 + "','" + Company + "','TMU00049'," + txtDocNo.Text + ",'" + Session["uid"].ToString() + "') update tbl_ODAttachment set Status=1 where ID='" + txtDocNo.Text + "'";
        }
        SqlCommand cmd = new SqlCommand(sqlq, con1);
        cmd.ExecuteNonQuery();
        con1.Close();

    }
    public void clear()
    {
        txtFromDate.Text = "";

        txtTodate.Text = "";
        txtToTime.Text = "00:00";
        txtDestination.Text = "";
        txtPurpose.Text = "";
        txtRemarks.Text = "";
    }

    public void Show_ODStatusByOwn()
    {
        if (txtFromDate_ViewStatus.Text.Trim() == "" || txtTodate_ViewStatus.Text.Trim() == "")
        {
            SqlDataAdapter dac = new SqlDataAdapter("SELECT [id] ,[Employee No] ,[Employee Name] , CONVERT (varchar(10), convert(date,[From Date]), 105) 'From Date'  ,[From Time], CONVERT (varchar(10), convert(date,[To Date]), 105) 'To Date' ,[To Time] ,[Destination],[Purpose] ,[Remarks] ,[Created Date] ,[Approval] ,[Approval Date] ,[Approval By] ,[Rejected Remarks] ,[Approval By Name] ,[HOD]  ,[HOD1]  ,[Company] ,[VCUserid],[VCName] ,[VCEmail] ,[SpecialODApproval] ,[DocumentNo],isnull([ApplyById],'Uploaded') 'ApplyById',DATEDIFF(day, [From Date], [To Date])+1  AS date_difference FROM tbl_OD_Application  where [SpecialODApproval]='" + Session["uid"].ToString() + "'  and Approval='" + ddStatus_Approval.SelectedValue.ToString().Trim() + "' and Company='" + Session["Company"].ToString() + "' ", con1);
            DataTable dtc = new DataTable();
            dac.Fill(dtc);
            grdView_Status.DataSource = dtc;
            grdView_Status.DataBind();
            // string s = "select * from tbl_OD_Application where [Employee No]='" + employeeNo + "'  and Approval='" + Approval_status + "' and Company='" + Company + "'";





        }
        if (txtFromDate_ViewStatus.Text.Trim() != "" || txtTodate_ViewStatus.Text.Trim() != "")
        {

            if (ddStatus_ViewStatus.SelectedValue.Trim() == "All")
            {
                if (txtEmployee.Text == "")
                {
                    pnlViewStatus.Visible = true;
                    SqlDataAdapter dac = new SqlDataAdapter("SELECT [id] ,[Employee No] ,[Employee Name] , CONVERT (varchar(10), convert(date,[From Date]), 105) 'From Date'  ,[From Time], CONVERT (varchar(10), convert(date,[To Date]), 105) 'To Date' ,[To Time] ,[Destination],[Purpose] ,[Remarks] ,[Created Date] ,[Approval] ,[Approval Date] ,[Approval By] ,[Rejected Remarks] ,[Approval By Name] ,[HOD]  ,[HOD1]  ,[Company] ,[VCUserid],[VCName] ,[VCEmail] ,[SpecialODApproval] ,[DocumentNo],isnull([ApplyById],'Uploaded') 'ApplyById',DATEDIFF(day, [From Date], [To Date])+1  AS date_difference FROM tbl_OD_Application   WHERE (convert(date,[From Date],111)    between '" + txtFromDate_ViewStatus.Text.Trim() + "' and '" + txtTodate_ViewStatus.Text.Trim() + "' or convert(date,[To Date],111) between '" + txtFromDate_ViewStatus.Text.Trim() + "' and '" + txtTodate_ViewStatus.Text.Trim() + "') and ([SpecialODApproval]='TMU00049' or ApplyById='" + Session["uid"].ToString() + "') and Company='" + Session["Company"].ToString() + "' ", con1);
                    DataTable dtc = new DataTable();
                    dac.Fill(dtc);
                    grdView_Status.DataSource = dtc;
                    grdView_Status.DataBind();
                }
                else
                {
                    pnlViewStatus.Visible = true;
                    SqlDataAdapter dac = new SqlDataAdapter("SELECT [id] ,[Employee No] ,[Employee Name] , CONVERT (varchar(10), convert(date,[From Date]), 105) 'From Date'  ,[From Time], CONVERT (varchar(10), convert(date,[To Date]), 105) 'To Date' ,[To Time] ,[Destination],[Purpose] ,[Remarks] ,[Created Date] ,[Approval] ,[Approval Date] ,[Approval By] ,[Rejected Remarks] ,[Approval By Name] ,[HOD]  ,[HOD1]  ,[Company] ,[VCUserid],[VCName] ,[VCEmail] ,[SpecialODApproval] ,[DocumentNo],isnull([ApplyById],'Uploaded') 'ApplyById',DATEDIFF(day, [From Date], [To Date])+1  AS date_difference FROM tbl_OD_Application   WHERE (convert(date,[From Date],111)    between '" + txtFromDate_ViewStatus.Text.Trim() + "' and '" + txtTodate_ViewStatus.Text.Trim() + "' or convert(date,[To Date],111) between '" + txtFromDate_ViewStatus.Text.Trim() + "' and '" + txtTodate_ViewStatus.Text.Trim() + "') and ([SpecialODApproval]='TMU00049' or ApplyById='" + Session["uid"].ToString() + "') and [Employee No]='" + txtEmployee.Text + "' and Company='" + Session["Company"].ToString() + "' ", con1);
                    DataTable dtc = new DataTable();
                    dac.Fill(dtc);
                    grdView_Status.DataSource = dtc;
                    grdView_Status.DataBind();
                }

            }
            else
            {
                if (txtEmployee.Text == "")
                {
                    pnlViewStatus.Visible = true;
                    SqlDataAdapter dac = new SqlDataAdapter(" SELECT [id] ,[Employee No] ,[Employee Name] , CONVERT (varchar(10), convert(date,[From Date]), 105) 'From Date'  ,[From Time], CONVERT (varchar(10), convert(date,[To Date]), 105) 'To Date' ,[To Time] ,[Destination],[Purpose] ,[Remarks] ,[Created Date] ,[Approval] ,[Approval Date] ,[Approval By] ,[Rejected Remarks] ,[Approval By Name] ,[HOD]  ,[HOD1]  ,[Company] ,[VCUserid],[VCName] ,[VCEmail] ,[SpecialODApproval] ,[DocumentNo],isnull([ApplyById],'Uploaded') 'ApplyById',DATEDIFF(day, [From Date], [To Date])+1  AS date_difference FROM tbl_OD_Application  WHERE (convert(date,[From Date],111) >= '" + txtFromDate_ViewStatus.Text.Trim() + "' and convert(date,[To Date],111) <='" + txtTodate_ViewStatus.Text.Trim() + "') and ([SpecialODApproval]='TMU00049' or ApplyById='" + Session["uid"].ToString() + "')  and Approval='" + ddStatus_ViewStatus.SelectedValue.ToString().Trim() + "' and Company='" + Session["Company"].ToString() + "' ", con1);
                    DataTable dtc = new DataTable();
                    dac.Fill(dtc);
                    grdView_Status.DataSource = dtc;
                    grdView_Status.DataBind();
                }
                else
                {
                    pnlViewStatus.Visible = true;
                    SqlDataAdapter dac = new SqlDataAdapter(" SELECT [id] ,[Employee No] ,[Employee Name] , CONVERT (varchar(10), convert(date,[From Date]), 105) 'From Date'  ,[From Time], CONVERT (varchar(10), convert(date,[To Date]), 105) 'To Date' ,[To Time] ,[Destination],[Purpose] ,[Remarks] ,[Created Date] ,[Approval] ,[Approval Date] ,[Approval By] ,[Rejected Remarks] ,[Approval By Name] ,[HOD]  ,[HOD1]  ,[Company] ,[VCUserid],[VCName] ,[VCEmail] ,[SpecialODApproval] ,[DocumentNo],isnull([ApplyById],'Uploaded') 'ApplyById',DATEDIFF(day, [From Date], [To Date])+1  AS date_difference FROM tbl_OD_Application  WHERE (convert(date,[From Date],111) >= '" + txtFromDate_ViewStatus.Text.Trim() + "' and convert(date,[To Date],111) <='" + txtTodate_ViewStatus.Text.Trim() + "') and ([SpecialODApproval]='TMU00049' or ApplyById='" + Session["uid"].ToString() + "')  and Approval='" + ddStatus_ViewStatus.SelectedValue.ToString().Trim() + "' and [Employee No]='" + txtEmployee.Text + "' and Company='" + Session["Company"].ToString() + "' ", con1);
                    DataTable dtc = new DataTable();
                    dac.Fill(dtc);
                    grdView_Status.DataSource = dtc;
                    grdView_Status.DataBind();
                }
            }
        }
    }

    public int DuplicateOD()
    {
        int dupDays = 0;
        DateTime FromDate = DateTime.ParseExact(txtFromDate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime ToDate = DateTime.ParseExact(txtTodate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string procName = "SP_OD_Duplicate_Control";

        string[] ParamName = new string[4];
        object[] ParamValue = new object[4];
        ParamValue[0] = txtEmployeeCode.Text;
        ParamName[0] = "UserId";
        ParamValue[1] = FromDate;
        ParamName[1] = "FromDate";
        ParamValue[2] = ToDate;
        ParamName[2] = "ToDate";

        ParamValue[3] = Session["Company"].ToString();
        ParamName[3] = "comp_ny";
        DataTable dt = GetDataTableCmdParams(procName, ParamName, ParamValue);
        dupDays = Convert.ToInt32(dt.Rows[0][0].ToString());
        return dupDays;
    }
    public DataTable GetDataTableCmdParams(string ProcName, string[] ParamName, object[] ParamValue) //ashu
    {
        con.Connect();
        DataTable dt;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con.Con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = ProcName;

        //this.cmd = CmdProc(ProcName);
        for (int i = 0; i < ParamName.Length; i++)
        {
            cmd.Parameters.AddWithValue(ParamName[i], ParamValue[i]);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            dt = new DataTable();
            da.Fill(dt);
        }
        catch
        {
            throw;
        }
        finally
        {
            con.DisConnect();
        }
        return dt;
    }

    public void Show_HODData()
    {


        if (rdEmployeeID.Checked == false && rdEmployeeName.Checked == false && rdDatewise.Checked == false)
        {

            SqlDataAdapter dac = new SqlDataAdapter("select * from tbl_OD_Application where [SpecialODApproval]='" + Session["uid"].ToString() + "'  and Approval='" + ddStatus_Approval.SelectedValue.ToString().Trim() + "' and Company='" + Session["Company"].ToString() + "' ", con1);
            DataTable dtc = new DataTable();
            dac.Fill(dtc);
            grdApproval.DataSource = dtc;
            grdApproval.DataBind();

        }
        if (rdEmployeeID.Checked == true)
        {
            SqlDataAdapter dac = new SqlDataAdapter("select * from tbl_OD_Application where [SpecialODApproval]='" + Session["uid"].ToString() + "'  and Approval='" + ddStatus_Approval.SelectedValue.ToString().Trim() + "' and Company='" + Session["Company"].ToString() + "' and [Employee No]='" + txtEmployeeIDNameFilter.Text.Trim() + "'", con1);
            DataTable dtc = new DataTable();
            dac.Fill(dtc);
            grdApproval.DataSource = dtc;
            grdApproval.DataBind();
        }

        if (rdEmployeeName.Checked == true)
        {
            SqlDataAdapter dac = new SqlDataAdapter("select * from tbl_OD_Application where [SpecialODApproval]='" + Session["uid"].ToString() + "'  and Approval='" + ddStatus_Approval.SelectedValue.ToString().Trim() + "' and Company='" + Session["Company"].ToString() + "' and UPPER([Employee Name]) LIKE UPPER('%" + txtEmployeeIDNameFilter.Text.Trim() + "%')", con1);
            DataTable dtc = new DataTable();
            dac.Fill(dtc);
            grdApproval.DataSource = dtc;
            grdApproval.DataBind();
        }

        if (rdDatewise.Checked == true)
        {
            SqlDataAdapter dac = new SqlDataAdapter("select * from tbl_OD_Application where [SpecialODApproval]='" + Session["uid"].ToString() + "'  and Approval='" + ddStatus_Approval.SelectedValue.ToString().Trim() + "' and Company='" + Session["Company"].ToString() + "' and convert(date,[From Date],111) >= '" + txtFromDate_Approval.Text.Trim() + "' and convert(date,[To Date],111) <='" + txtTodate_Approval.Text.Trim() + "'", con1);
            DataTable dtc = new DataTable();
            dac.Fill(dtc);
            grdApproval.DataSource = dtc;
            grdApproval.DataBind();
        }
    }
    protected void DownloadFile(object sender, EventArgs e)
    {
        string AssignmentNo = (sender as LinkButton).CommandArgument;
        byte[] bytes;
        string fileName, contentType;
        //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select Attachment,[Content Type] as 'AttachmentType',[File Name] as 'FileName' from tbl_ODAttachment where ID='" + AssignmentNo + "' ";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Attachment"];
                    contentType = sdr["AttachmentType"].ToString();
                    fileName = sdr["FileName"].ToString(); ;
                }
                con1.Close();
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
    public void ShowPendingApprovalCount()
    {

        SqlDataAdapter dac = new SqlDataAdapter(" select COUNT([Employee No]) as C from tbl_OD_Application where [SpecialODApproval]='" + Session["uid"].ToString() + "' and Approval='Pending'", con1);
        DataTable dtc = new DataTable();
        dac.Fill(dtc);
        lblCountODAppoval.Text = dtc.Rows[0]["C"].ToString();


    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void btnExporttoexcel_viewStatus_Click(object sender, EventArgs e)
    {

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=AttendanceView.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        string headerTable = @"<Table bgcolor=gray><tr><td colspan=10 align=center bgcolor=gold ><font size=16><h1>OD Report</h1></td></tr></Table>";
        Response.Write(headerTable);
        StringWriter sw1 = new StringWriter();
        Response.Write(sw1.ToString());

        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //To Export all pages
            grdView_Status.AllowPaging = false;
            Show_ODStatusByOwn();
            //grdView_Status.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grdView_Status.HeaderRow.Cells)
            {
                cell.BackColor = grdView_Status.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grdView_Status.Rows)
            {
                //row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grdView_Status.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grdView_Status.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }
            grdView_Status.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

    }

    protected void btnApproveExport_Click(object sender, EventArgs e)
    {

        grdView_Status.AllowPaging = false;
        if (ddStatus_Approval.SelectedValue == "Pending" || ddStatus_Approval.SelectedValue == "Recommend")
        {

            grdApproval.Columns[0].Visible = true;
            grdApproval.Columns[11].Visible = false;
        }

        if (ddStatus_Approval.SelectedValue != "Pending" || ddStatus_Approval.SelectedValue != "Recommend")
        {

            grdApproval.Columns[0].Visible = false;
            grdApproval.Columns[11].Visible = false;
        }
        if (ddStatus_Approval.SelectedValue == "Rejected")
        {
            grdApproval.Columns[0].Visible = false;
            grdApproval.Columns[11].Visible = true;
        }
        grdView_Status.AllowPaging = false;
        Show_HODData();


        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grdApproval.RenderControl(htmlWrite);

        Response.Clear();
        string str = "Approvaldata" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();

    }

    string Emp_No_OD = ""; string frmmdate_OD = ""; string Todate_OD = ""; string Purpose_OD = ""; string fromtime_od = ""; string Totime_OD = ""; string Approval_Status_OD = "";
    public void ShowApprovalData(string id)
    {
        SqlDataReader dr = con.Show_ApprovalODid(id);
        dr.Read();
        if (dr.HasRows)
        {
            frmmdate_OD = dr["From Date"].ToString();
            fromtime_od = dr["From Time"].ToString();
            DateTime frmmdate_OD1 = Convert.ToDateTime(frmmdate_OD);
            frmmdate_OD = frmmdate_OD1.ToString("yyyy-MM-dd");
            Todate_OD = dr["To Date"].ToString();
            DateTime Todate_OD1 = Convert.ToDateTime(Todate_OD);
            Todate_OD = Todate_OD1.ToString("yyyy-MM-dd");
            Totime_OD = dr["To Time"].ToString();

            if (Totime_OD == "00:00")
            {
                Totime_OD = "1753-01-01 00:00:00.000";
            }

            if (fromtime_od == "00:00")
            {
                fromtime_od = "1753-01-01 00:00:00.000";
            }

            Purpose_OD = dr["Purpose"].ToString();

            Approval_Status_OD = dr["Approval"].ToString();
            Emp_No_OD = dr["Employee No"].ToString();

            dr.Close();
            con.DisConnect();

            if (Approval_Status_OD == "Pending")
            {


                string ccname = Session["Company"].ToString();
                string rccname = ccname.Replace(".", "_");
                string tblenameAttendence = "[" + rccname + "$Pay Daily Attendence Detail" + "]";

                navconn.Update_OD_Approval(tblenameAttendence, fromtime_od.Trim(), Totime_OD.Trim(), Emp_No_OD.Trim(), frmmdate_OD.Trim(), Todate_OD.Trim(), Purpose_OD.Trim());
                navconn.DisConnect();
                Update_ODStatus(id, Session["uname"].ToString(), Session["uid"].ToString(), "Approved");
                con.DisConnect();

            }

        }
        else
        {
            dr.Close();
            con.DisConnect();

        }
    }


    public void Update_ODStatus(string id, string ApprovalByName, string ApprovalBy, string Status)
    {

        con1.Open();

        string sqlq = "update tbl_OD_Application set Approval='" + Status + "',[Approval Date]=GetDate(),[Approval By Name]='" + ApprovalByName + "',[Approval By]='" + ApprovalBy + "' where id='" + id + "'";
        SqlCommand cmd = new SqlCommand(sqlq, con1);
        cmd.ExecuteNonQuery();
        con1.Close();

    }





    string Approval_Status_OD_reject = "";
    public void ShowApprovalDataRejected(string id)
    {
        SqlDataReader dr = con.Show_ApprovalODid(id);
        dr.Read();
        if (dr.HasRows)
        {


            Approval_Status_OD_reject = dr["Approval"].ToString();

            dr.Close();
            con.DisConnect();

            if (Approval_Status_OD_reject == "Pending" || Approval_Status_OD_reject == "Recommend")
            {

                con.Update_ODStatusReject(id, Session["uname"].ToString(), Session["uid"].ToString(), txtRemarks_ByHOD.Text.Trim());
                con.DisConnect();
            }
        }
        else
        {
            dr.Close();
            con.DisConnect();

        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grdApproval.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {

                CheckBox chkRow = (row.Cells[0].FindControl("chkMark") as CheckBox);

                Label lblid = (row.Cells[0].FindControl("lblid") as Label);
                Label lblFDate = (Label)row.FindControl("lblFromDate");
                Label lblTdate = (Label)row.FindControl("lblToDate");
                Label EmpId = (Label)row.FindControl("lblEmployeeid");

                if (chkRow.Checked == true)
                {

                    ShowApprovalData(lblid.Text.Trim());
                    sendAproved(lblFDate.Text.Trim(), lblTdate.Text.Trim(), EmpId.Text, "Approved");
                }
            }
        }



        Show_HODData();
        ShowPendingApprovalCount();

    }
    protected void grdView_Status_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdView_Status.PageIndex = e.NewPageIndex;
        if (ddStatus_ViewStatus.SelectedValue == "Rejected" || ddStatus_ViewStatus.SelectedValue == "All")
        {
            Show_ODStatusByOwn();
            grdView_Status.Columns[8].Visible = true;
            grdView_Status.Columns[9].Visible = true;

        }
        else
        {



            if (ddStatus_ViewStatus.SelectedValue == "Approved")
            {
                grdView_Status.Columns[8].Visible = true;
                grdView_Status.Columns[9].Visible = false;
            }
            if (ddStatus_ViewStatus.SelectedValue == "Pending" || ddStatus_ViewStatus.SelectedValue == "Recommend")
            {
                grdView_Status.Columns[8].Visible = false;
                grdView_Status.Columns[9].Visible = false;
            }


            Show_ODStatusByOwn();
        }

    }
    protected void grdApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdApproval.PageIndex = e.NewPageIndex;
        if (ddStatus_Approval.SelectedValue == "Pending" || ddStatus_Approval.SelectedValue == "Recommend")
        {

            grdApproval.Columns[0].Visible = true;
            grdApproval.Columns[11].Visible = false;
        }

        if (ddStatus_Approval.SelectedValue != "Pending" || ddStatus_Approval.SelectedValue != "Recommend")
        {

            grdApproval.Columns[0].Visible = false;
            grdApproval.Columns[11].Visible = false;
        }
        if (ddStatus_Approval.SelectedValue == "Rejected")
        {
            grdApproval.Columns[0].Visible = false;
            grdApproval.Columns[11].Visible = true;
        }

        Show_HODData();
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grdApproval.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {

                CheckBox chkRow = (row.Cells[0].FindControl("chkMark") as CheckBox);

                Label lblid = (row.Cells[0].FindControl("lblid") as Label);
                Label lblFDate = (Label)row.FindControl("lblFromDate");
                Label lblTdate = (Label)row.FindControl("lblToDate");
                Label EmpId = (Label)row.FindControl("lblEmployeeid");

                if (chkRow.Checked == true)
                {

                    ShowApprovalDataRejected(lblid.Text.Trim());
                    sendAproved(lblFDate.Text.Trim(), lblTdate.Text.Trim(), EmpId.Text, "Rejected");
                }
            }
        }


        //if (ddStatus_Approval.SelectedValue == "Pending" || ddStatus_Approval.SelectedValue == "Recommend")
        //{

        //    grdApproval.Columns[0].Visible = true;
        //    grdApproval.Columns[11].Visible = false;
        //}

        //if (ddStatus_Approval.SelectedValue != "Pending" || ddStatus_Approval.SelectedValue != "Recommend")
        //{

        //    grdApproval.Columns[0].Visible = false;
        //    grdApproval.Columns[11].Visible = false;
        //}
        //if (ddStatus_Approval.SelectedValue == "Rejected")
        //{
        //    grdApproval.Columns[0].Visible = false;
        //    grdApproval.Columns[11].Visible = true;
        //}

        Show_HODData();
        ShowPendingApprovalCount();


    }

    public void ValidateDate()
    {

        DateTime frodatecom = DateTime.ParseExact(txtFromDate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime Todatecom = DateTime.ParseExact(txtTodate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //DateTime frodatecom = DateTime.ParseExact(txtFromDate.Text.Trim() + " " + txtFromTime.Text.Trim(), "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
        //DateTime Todatecom = DateTime.ParseExact(txtTodate.Text.Trim() + " " + txtToTime.Text.Trim(), "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
        if (frodatecom > Todatecom)
        {
            txtTodate.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('To Date should always be greater than From Date');", true);

        }
        else
        {


        }

    }
    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ValidateDate();
        }
        catch (Exception)
        { }
    }
    protected void txtFromTime_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ValidateDate();
        }
        catch (Exception)
        { }
    }
    protected void txtToTime_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ValidateDate();
        }
        catch (Exception)
        { }
    }
    protected void txtTodate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ValidateDate();
        }
        catch (Exception)
        { }
    }
    private void SetInitialRow()
    {

        DataTable dt = new DataTable();

        DataRow dr = null;


        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Userid", typeof(string)));
        dt.Columns.Add(new DataColumn("UserName", typeof(string)));
        dt.Columns.Add(new DataColumn("FromDate", typeof(string)));

        dt.Columns.Add(new DataColumn("ToDate", typeof(string)));
        dt.Columns.Add(new DataColumn("LeaveType", typeof(string)));

        dt.Columns.Add(new DataColumn("Destination", typeof(string)));

        dt.Columns.Add(new DataColumn("Purpose", typeof(string)));
        dt.Columns.Add(new DataColumn("Remark", typeof(string)));
        dr = dt.NewRow();
        dr["RowNumber"] = 1;


        dr["Userid"] = string.Empty;
        dr["UserName"] = string.Empty;
        dr["FromDate"] = string.Empty;

        dr["ToDate"] = string.Empty;


        dr["LeaveType"] = string.Empty;

        dr["Destination"] = string.Empty;

        dr["Purpose"] = string.Empty;
        dr["Remark"] = string.Empty;

        dt.Rows.Add(dr);



        ViewState["CurrentTable"] = dt;



        grdTempLeave.DataSource = dt;

        grdTempLeave.DataBind();

    }


    private void AddNewRowToGrid()
    {

        int rowIndex = 0;



        if (ViewState["CurrentTable"] != null)
        {

            btnSendForApproval.Visible = true;

            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];

            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {


                //Label Userid = (Label)grdTempLeave.Rows[rowIndex].Cells[1].FindControl("lblEmployeeCode");

                //Label FromDate = (Label)grdTempLeave.Rows[rowIndex].Cells[2].FindControl("lblFromDate");

                //Label ToDate = (Label)grdTempLeave.Rows[rowIndex].Cells[3].FindControl("lblToDate");

                //Label LeaveType = (Label)grdTempLeave.Rows[rowIndex].Cells[4].FindControl("lblLeaveType");

                //Label Destination = (Label)grdTempLeave.Rows[rowIndex].Cells[5].FindControl("lblDestination");

                //Label Purpose = (Label)grdTempLeave.Rows[rowIndex].Cells[6].FindControl("lblPurpose");
                //Label Remark = (Label)grdTempLeave.Rows[rowIndex].Cells[7].FindControl("lblRemark");




                drCurrentRow = dtCurrentTable.NewRow();

                drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;
                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["Userid"] = txtEmployeeCode.Text;





                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["UserName"] = Session["Name"].ToString();
                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["FromDate"] = txtFromDate.Text;

                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["ToDate"] = txtTodate.Text;
                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["LeaveType"] = drpLeaveType.SelectedValue;

                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["Destination"] = txtDestination.Text;

                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["Purpose"] = txtPurpose.Text;

                dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["Remark"] = txtRemarks.Text;



                rowIndex++;



                dtCurrentTable.Rows.Add(drCurrentRow);

                //for (int i = dtCurrentTable.Rows.Count - 1; i >= 0; i--)
                //{
                //    DataRow dr = dtCurrentTable.Rows[i];
                //    if (dr["Userid"].ToString() == "")
                //        dtCurrentTable.Rows.Remove(dr);
                //}
                //dtCurrentTable.AcceptChanges();




                ViewState["CurrentTable"] = dtCurrentTable;



                grdTempLeave.DataSource = dtCurrentTable;

                grdTempLeave.DataBind();




            }
        }

        else
        {

            //Response.Write("ViewState is null");

        }



        //Set Previous Data on Postbacks

        SetPreviousData();

    }
    private void SetPreviousData()
    {

        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {

            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    int co = grdTempLeave.Rows.Count;
                    Label Userid = (Label)grdTempLeave.Rows[i].Cells[1].FindControl("lblEmployeeCode");
                    Label UserName = (Label)grdTempLeave.Rows[i].Cells[1].FindControl("lblEmployeeName");
                    Label FromDate = (Label)grdTempLeave.Rows[i].Cells[2].FindControl("lblFromDate");
                    Label ToDate = (Label)grdTempLeave.Rows[i].Cells[3].FindControl("lblToDate");
                    Label LeaveType = (Label)grdTempLeave.Rows[i].Cells[4].FindControl("lblLeaveType");
                    Label Destination = (Label)grdTempLeave.Rows[i].Cells[5].FindControl("lblDestination");
                    Label Purpose = (Label)grdTempLeave.Rows[i].Cells[6].FindControl("lblPurpose");
                    Label Remark = (Label)grdTempLeave.Rows[i].Cells[7].FindControl("lblRemark");



                    if (i < dt.Rows.Count - 1)
                    {

                        Userid.Text = dt.Rows[i]["Userid"].ToString();
                        UserName.Text = dt.Rows[i]["UserName"].ToString();
                        FromDate.Text = dt.Rows[i]["FromDate"].ToString();
                        ToDate.Text = dt.Rows[i]["ToDate"].ToString();
                        LeaveType.Text = dt.Rows[i]["LeaveType"].ToString();
                        Destination.Text = dt.Rows[i]["Destination"].ToString();
                        Purpose.Text = dt.Rows[i]["Purpose"].ToString();
                        Remark.Text = dt.Rows[i]["Remark"].ToString();



                    }

                    rowIndex++;
                }

            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        SqlDataAdapter dac = new SqlDataAdapter("select * from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where [No_]='" + txtEmployeeCode.Text.ToUpper() + "' ", con1);
        DataTable dtc = new DataTable();
        dac.Fill(dtc);
        if (dtc.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('This Employee ID is not Available.');", true);
            return;
        }
        else
        {
            Session["Name"] = dtc.Rows[0]["First Name"].ToString();
        }


        if (DuplicateOD() > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You have already Applied  ! check the OD Applied date');", true);
            return;
        }



        else
        {
            string ccname = Session["Company"].ToString();
            string rccname = ccname.Replace(".", "_");
            string Pay_Daily_Attendence_Detail = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
            SqlDataReader dr = navconn.Show_LeaveAppliedFOROD(Pay_Daily_Attendence_Detail, txtFromDate.Text.Trim(), txtTodate.Text.Trim(), txtEmployeeCode.Text.ToUpper());
            dr.Read();
            if (dr.HasRows)
            {
                dr.Close();
                navconn.DisConnect();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You have already Applied Leave ! check the OD Applied date from Leave Date');", true);
            }
            else
            {

                AddNewRowToGrid();
            }
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {

        if (FileUpload.HasFile)
        {
            string contentType = FileUpload.PostedFile.ContentType;
            string filename = Path.GetFileName(FileUpload.PostedFile.FileName);
            using (Stream fs = FileUpload.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
                    {
                        SqlCommand cmd = new SqlCommand("proc_UploadODAttach", con2);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@AttachmentType", contentType);
                        cmd.Parameters.Add("@FileName", filename);
                        cmd.Parameters.Add("@Attachment", bytes);
                        cmd.Parameters.Add("@NewId", SqlDbType.Int).Direction = ParameterDirection.Output;
                        if (con2.State == ConnectionState.Closed)
                            con2.Open();
                        cmd.ExecuteNonQuery();
                        int NEWID = Convert.ToInt32(cmd.Parameters["@NewId"].Value);
                        con2.Close();
                        txtDocNo.Text = NEWID.ToString();

                    }
                }
            }
        }



    }


    protected void btnAddDoc_Click(object sender, EventArgs e)
    {
        pnlPopup.Visible = true;

        SqlCommand cmd = new SqlCommand("select * from [tbl_ODAttachment] where [Status]=0", con1);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ModalPopupExtender1.Show();
        grdInbox.DataSource = dt;
        grdInbox.DataBind();
    }
    protected void chkselect_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grdInbox.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkselect");
            Label lblFaculty = (Label)row.FindControl("lblFCode");

            if (chk.Checked == true)
            {

                txtDocNo.Text = lblFaculty.Text;
            }
        }



    }
    protected void txtEmployeeCode_TextChanged(object sender, EventArgs e)
    {
        SqlDataAdapter dac = new SqlDataAdapter("select * from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where [No_]='" + txtEmployeeCode.Text.ToUpper() + "' ", con1);
        DataTable dtc = new DataTable();
        dac.Fill(dtc);
        if (dtc.Rows.Count > 0)
        {

            txtEmpName.Text = dtc.Rows[0]["First Name"].ToString();

        }
    }

    protected void btnUploadExcel_Click(object sender, EventArgs e)
    {

        if (FileUpload1.HasFile)
        {
            string fileExt = Path.GetExtension(FileUpload1.FileName).ToLower();

            // Allow only Excel files
            if (fileExt == ".xls" || fileExt == ".xlsx")
            {
                string filePath = Server.MapPath("~/Uploads/" + FileUpload1.FileName);
                FileUpload1.SaveAs(filePath);

                string conStr = "";
                if (fileExt == ".xls") // Excel 97-03
                {
                    conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath +
                             ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'";
                }
                else if (fileExt == ".xlsx") // Excel 2007+
                {
                    conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath +
                             ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;'";
                }

                // Read the Excel sheet
                using (OleDbConnection conn = new OleDbConnection(conStr))
                {
                    conn.Open();
                    DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string sheetName = dtSheet.Rows[0]["TABLE_NAME"].ToString();

                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheetName + "]", conn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Button1.Visible = true;
                        grdTempLeave.DataSource = dt;
                        grdTempLeave.DataBind();
                    }
                    else
                    {
                        Button1.Visible = false;
                        grdTempLeave.DataSource = "";
                        grdTempLeave.DataBind();
                    }
                }
            }
            else
            {
                Response.Write("Only Excel files are allowed (.xls, .xlsx)");
            }
        }
        else
        {
            Response.Write("Please select an Excel file to upload.");
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < grdTempLeave.Rows.Count; i++)
        {
            try
            {
                Label FromDate = (Label)grdTempLeave.Rows[i].Cells[3].FindControl("lblFromDate");
                Label ToDate = (Label)grdTempLeave.Rows[i].Cells[4].FindControl("lblToDate");
                SqlDataAdapter da = new SqlDataAdapter("SELECT [Payroll Processing Month Date] FROM [EDUCOLLEGELIVE-R2].dbo.[TMU$Pay Company Policy] WHERE ((MONTH([Payroll Processing Month Date]) = MONTH('" + FromDate.Text + "') AND YEAR([Payroll Processing Month Date]) = YEAR('" + FromDate.Text + "'))  and   (MONTH([Payroll Processing Month Date]) = MONTH('" + ToDate.Text + "') AND YEAR([Payroll Processing Month Date]) = YEAR('" + ToDate.Text + "'))) select DATENAME(MONTH, [Payroll Processing Month Date]) + ' ' +   CAST(YEAR([Payroll Processing Month Date]) AS VARCHAR(4)) AS MonthYear FROM [EDUCOLLEGELIVE-R2].dbo.[TMU$Pay Company Policy] ", con.Con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0 && FromDate.Text != "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You can Apply OD for month and year " + ds.Tables[1].Rows[0]["MonthYear"] + " !!');", true);
                    return;
                }
            }
            catch
            {

            }

        }
        for (int i = 0; i < grdTempLeave.Rows.Count; i++)
        {

            Label Userid = (Label)grdTempLeave.Rows[i].Cells[1].FindControl("lblEmployeeCode");
            if (Userid.Text != "")
            {
                try
                {
                    Label FromDate = (Label)grdTempLeave.Rows[i].Cells[3].FindControl("lblFromDate");
                    Label ToDate = (Label)grdTempLeave.Rows[i].Cells[4].FindControl("lblToDate");
                    Label LeaveType = (Label)grdTempLeave.Rows[i].Cells[5].FindControl("lblLeaveType");
                    Label Destination = (Label)grdTempLeave.Rows[i].Cells[6].FindControl("lblDestination");
                    Label Purpose = (Label)grdTempLeave.Rows[i].Cells[7].FindControl("lblPurpose");
                    Label Remark = (Label)grdTempLeave.Rows[i].Cells[8].FindControl("lblRemark");
                    Insert_tbl_OD_Application(Userid.Text.ToUpper(), "", FromDate.Text.Trim(), "00:00", ToDate.Text.Trim(), "00:00", Destination.Text.Trim(), Purpose.Text.Trim(), Remark.Text.Trim(), System.DateTime.Now.ToString("yyyy-MM-dd"), "", "", Session["Company"].ToString());

                }
                catch (Exception ex)
                {
                    lblODSuccess.ForeColor = System.Drawing.Color.Red;
                    lblODSuccess.Text = "Some thing went wrong.";
                    lblODSuccess.Visible = true;
                }
            }


        }
        lblODSuccess.Visible = true;
        grdTempLeave.DataSource = "";
        grdTempLeave.DataBind();
    }
}