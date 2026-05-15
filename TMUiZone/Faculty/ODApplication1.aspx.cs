using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


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

                if (Session["uid"].ToString() == "TMU00049" || Session["uid"].ToString() == "TMU06618" || Session["uid"].ToString() == "TMU02444")
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

        if (Session["uid"].ToString() == "TMU00049")
        {           
            lnkApproval.Visible = true;
            lblCountODAppoval.Visible = true;
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


        if (txtFromTime.Text.Trim() == "")
        {
            txtFromTime.Text = "00:00";
        }
        if (txtToTime.Text.Trim() == "")
        {
            txtToTime.Text = "00:00";
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
                dr.Close();
                navconn.DisConnect();

                Insert_tbl_OD_Application(txtEmployeeCode.Text.ToUpper(), "", txtFromDate.Text.Trim(), txtFromTime.Text.Trim(), txtTodate.Text.Trim(), txtToTime.Text.Trim(), txtDestination.Text.Trim(), txtPurpose.Text.Trim(), txtRemarks.Text.Trim(), System.DateTime.Now.ToString("yyyy-MM-dd"), "", "", Session["Company"].ToString());

                lblODSuccess.Visible = true;
                SendSMSHODExam(txtEmployeeCode.Text.ToUpper(), txtFromDate.Text.Trim(), txtTodate.Text.Trim());



                clear();
            }
        }
    }
    public void Insert_tbl_OD_Application(string EmployeeNo, string EmployeeName, string FromDate, string FromTime, string ToDate, string ToTime, string Destination, string Purpose, string Remarks, string CreatedDate, string HOD, string HOD1, string Company)
    {
        con1.Open();
        string sqlq = "insert into tbl_OD_Application([Employee No],[Employee Name],[From Date],[From Time],[To Date],[To Time],[Destination],[Purpose],Remarks,[Created Date],HOD,HOD1,Company,SpecialODApproval) values('" + EmployeeNo + "',(select [First Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_='" + txtEmployeeCode.Text.ToUpper() + "'), '" + FromDate + "', '" + FromTime + "','" + ToDate + "','" + ToTime + "', '" + Destination + "', '" + Purpose + "', '" + Remarks + "','" + CreatedDate + "','" + HOD + "','" + HOD1 + "','" + Company + "','TMU00049')";
        SqlCommand cmd = new SqlCommand(sqlq, con1);
        cmd.ExecuteNonQuery();
        con1.Close();

    }
    public void clear()
    {
        txtFromDate.Text = "";
        txtFromTime.Text = "00:00";
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
            SqlDataAdapter dac = new SqlDataAdapter("select * from tbl_OD_Application where [SpecialODApproval]='" + Session["uid"].ToString() + "'  and Approval='" + ddStatus_Approval.SelectedValue.ToString().Trim() + "' and Company='" + Session["Company"].ToString() + "' ", con1);
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

                SqlDataAdapter dac = new SqlDataAdapter("SELECT * FROM tbl_OD_Application   WHERE (convert(date,[From Date],111)    between '" + txtFromDate_ViewStatus.Text.Trim() + "' and '" + txtTodate_ViewStatus.Text.Trim() + "' or convert(date,[To Date],111) between '" + txtFromDate_ViewStatus.Text.Trim() + "' and '" + txtTodate_ViewStatus.Text.Trim() + "') and [SpecialODApproval]='" + Session["uid"].ToString() + "' and Company='" + Session["Company"].ToString() + "' ", con1);
                DataTable dtc = new DataTable();
                dac.Fill(dtc);
                grdView_Status.DataSource = dtc;
                grdView_Status.DataBind();



               
            }
            else
            {
                SqlDataAdapter dac = new SqlDataAdapter(" SELECT * FROM tbl_OD_Application WHERE convert(date,[From Date],111) >= '" + txtFromDate_ViewStatus.Text.Trim() + "' and convert(date,[To Date],111) <='" + txtTodate_ViewStatus.Text.Trim() + "' and [SpecialODApproval]='" + Session["uid"].ToString() + "'  and Approval='" + ddStatus_ViewStatus.SelectedValue.ToString().Trim() + "' and Company='" + Session["Company"].ToString() + "' ", con1);
                DataTable dtc = new DataTable();
                dac.Fill(dtc);
                grdView_Status.DataSource = dtc;
                grdView_Status.DataBind();




              
               
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
        ParamValue[0] = txtEmployeeCode.Text.ToUpper();
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
            SqlDataAdapter dac = new SqlDataAdapter("select * from tbl_OD_Application where [SpecialODApproval]='" + Session["uid"].ToString() + "'  and Approval='" + ddStatus_Approval.SelectedValue.ToString().Trim() + "' and Company='" + Session["Company"].ToString() + "' and UPPER([Employee Name]) LIKE UPPER('%" + txtEmployeeIDNameFilter.Text.Trim() + "%')" , con1);
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
        grdView_Status.AllowPaging = false;
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grdView_Status.RenderControl(htmlWrite);

        Response.Clear();
        string str = "Data" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();

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
                    con.Update_ODStatus(id, Session["uname"].ToString(), Session["uid"].ToString(), "Approved");
                    con.DisConnect();
                
            }
           
        }
        else
        {
            dr.Close();
            con.DisConnect();

        }
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
   
}