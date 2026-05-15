using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class LeaveApproval : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal con;
    string HRHODsame = "";
    string forHRisHOD = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Portalcon = new Connection();
            con = new ServicePoratal();
            Showpermission();
            if (Session["uid"].ToString() == Session["HRID"].ToString())
            {
                HRHODsame = "HRHOD";
            }
            if (Session["uid"].ToString() != Session["HRID"].ToString())
            {

                HRHODsame = "HR";
            }
            if (Session["uid"].ToString() == Session["HODLoginPage"].ToString())
            {
                forHRisHOD = "HRHOD";
            }
            if (Session["uid"].ToString() != Session["HODLoginPage"].ToString())
            {

                forHRisHOD = "HR";
            }
            if (!IsPostBack)
            {
                pnlrejectedDetail.Visible = false;
                ModalPopupExtender1.Hide();
            }
        }
        catch (Exception)
        {
             Response.Redirect("Default.aspx");
        }
    }
    protected void lnkleaveview_Click(object sender, EventArgs e)
    {
        pnlProfileView.Visible = true;
        btnSearch.Visible = false;
        pnlDate.Visible = false;
        pnlEmployeeidName.Visible = false;
        pnlProfileRejected.Visible = false;
        pnlApprovedDetail.Visible = false;
        pnlMain.Visible = false;
    }
    protected void rdEmployeeID_CheckedChanged(object sender, EventArgs e)
    {
        pnlEmployeeidName.Visible = true;
        pnlDate.Visible = false;
        btnSearch.Visible = true;
     
    }
    protected void rdEmployeeName_CheckedChanged(object sender, EventArgs e)
    {
        pnlEmployeeidName.Visible = true;
        pnlDate.Visible = false;
        btnSearch.Visible = true;
       
    }
    protected void rdDatewise_CheckedChanged(object sender, EventArgs e)
    {
        pnlEmployeeidName.Visible = false;
        pnlDate.Visible = true;
        btnSearch.Visible = true;
        
    }
    protected void rdLeaveRectedEMPID_CheckedChanged(object sender, EventArgs e)
    {
        pnlLeaveRejectedEMPID.Visible = true;
        btnrejectedsearch.Visible = true;
        pnlLeaveRejectedDatewise.Visible = false;
    }
    protected void rdrdLeaveRectedName_CheckedChanged(object sender, EventArgs e)
    {
        pnlLeaveRejectedEMPID.Visible = true;
        btnrejectedsearch.Visible = true;
        pnlLeaveRejectedDatewise.Visible = false;
    }
    protected void rdLeaveRectedDatewise_CheckedChanged(object sender, EventArgs e)
    {
        pnlLeaveRejectedEMPID.Visible = false;
        btnrejectedsearch.Visible = true;
        pnlLeaveRejectedDatewise.Visible = true;
    }
    protected void lnkRejectLeaveDetail_Click(object sender, EventArgs e)
    {
        pnlProfileRejected.Visible = true;
        pnlProfileView.Visible = false;
        btnrejectedsearch.Visible = false;
        pnlLeaveRejectedEMPID.Visible = false;
        pnlLeaveRejectedDatewise.Visible = false;
        pnlApprovedDetail.Visible = false;
        pnlMain.Visible = false;
    }
    protected void lnkApprovedApproveddetail_Click(object sender, EventArgs e)
    {
        pnlMain.Visible = false;
        pnlApprovedDetail.Visible = true;
        pnlProfileRejected.Visible = false;
        pnlProfileView.Visible = false;
        btnApprovedSearch.Visible = false;
        pnlLeaveApprovedDetailDatewise.Visible = false;
        pnlLeaveApprovedDetailEmpid.Visible = false;
    }
    protected void rdApprovedEmpid_CheckedChanged(object sender, EventArgs e)
    {
        pnlLeaveApprovedDetailEmpid.Visible = true;
        pnlLeaveApprovedDetailDatewise.Visible = false;
        btnApprovedSearch.Visible = true;
    }
    protected void rdApprovedEMPName_CheckedChanged(object sender, EventArgs e)
    {
        pnlLeaveApprovedDetailEmpid.Visible = true;
        pnlLeaveApprovedDetailDatewise.Visible = false;
        btnApprovedSearch.Visible = true;
    }
    protected void rdApprovedDatewise_CheckedChanged(object sender, EventArgs e)
    {
        pnlLeaveApprovedDetailEmpid.Visible = false;
        pnlLeaveApprovedDetailDatewise.Visible = true;
        btnApprovedSearch.Visible = true;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ReimbursmentApproval_Search();
    }


    public void FindCheckDataRejected()
    {
        SqlDataAdapter da = new SqlDataAdapter("select * from tbl_reimbursmentDetail where Userid='" + chkUSerIDRejected + "' and Company_Name='" + Session["Company"].ToString() + "' and Create_Date ='" + profilechangedaterejected + "' and SerialNo='" + lblnoofchangeReject1 + "'", con.Con);
        DataSet ds = new DataSet();
        da.Fill(ds, "tbl_reimbursmentDetail");
        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
        {
            string Expense_Type = ""; string Credit_limit = ""; string Expense_Amount = ""; string Remarks = ""; string Bill_Detail = ""; string Userid = ""; string Department = ""; string Designation = ""; string HODUserid = ""; string HODRemarks = ""; string HRRemarks = ""; string Company_Name = ""; string SerialNo = ""; string Create_Date = ""; string HOD_Remarks_Date = ""; string Hr_Remarks_Date = ""; string HR_Userid = ""; string Status = ""; string UName = ""; string RejectedApproval = ""; string CompanyName1 = "";
            Expense_Type = ds.Tables[0].Rows[i]["Expense_Type"].ToString();
            Credit_limit = ds.Tables[0].Rows[i]["Credit_limit"].ToString();
            Expense_Amount = ds.Tables[0].Rows[i]["Expense_Amount"].ToString();
            Remarks = ds.Tables[0].Rows[i]["Remarks"].ToString();
            Bill_Detail = ds.Tables[0].Rows[i]["Bill_Detail"].ToString();
            Userid = ds.Tables[0].Rows[i]["Userid"].ToString();
            Department = ds.Tables[0].Rows[i]["Department"].ToString();
            Designation = ds.Tables[0].Rows[i]["Designation"].ToString();
            HODUserid = ds.Tables[0].Rows[i]["HODUserid"].ToString();
            HODRemarks = ds.Tables[0].Rows[i]["HODRemarks"].ToString();
            HRRemarks = ds.Tables[0].Rows[i]["HRRemarks"].ToString();
            Company_Name = ds.Tables[0].Rows[i]["Company_Name"].ToString();
            SerialNo = ds.Tables[0].Rows[i]["SerialNo"].ToString();
            Create_Date = ds.Tables[0].Rows[i]["Create_Date"].ToString();
            HOD_Remarks_Date = ds.Tables[0].Rows[i]["HOD_Remarks_Date"].ToString();
            Hr_Remarks_Date = ds.Tables[0].Rows[i]["Hr_Remarks_Date"].ToString();
            HR_Userid = ds.Tables[0].Rows[i]["HR_Userid"].ToString();
            Status = ds.Tables[0].Rows[i]["Status"].ToString();
            UName = ds.Tables[0].Rows[i]["UName"].ToString();
            RejectedApproval = ds.Tables[0].Rows[i]["Rejected Approval"].ToString();


            CompanyName1 = ds.Tables[0].Rows[i]["Company_Name"].ToString();
            con.Update_tbl_reimbursmentDetail_Rejected(txtRemarksRejected.Text, "Yes", System.DateTime.Now.ToString("dd/MM/yyyy"), chkUSerIDRejected, profilechangedaterejected, lblnoofchangeReject1, Session["Company"].ToString());
        }

    }

    string profilechangedaterejected = "";
    string chkUSerIDRejected = ""; string lblnoofchangeReject1 = "";
    protected void btnRejectProfile1_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grdReimbursment.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkMark") as CheckBox);
                Label lblProfilechangedate1 = (row.Cells[0].FindControl("lblProfilechangedate") as Label);
                Label lblNoofchange = (row.Cells[0].FindControl("lblNoofchange") as Label);
                if (chkRow.Checked == true)
                {

                    profilechangedaterejected = lblProfilechangedate1.Text;
                    chkUSerIDRejected = chkRow.Text;
                    lblnoofchangeReject1 = lblNoofchange.Text;
                    FindCheckDataRejected();


                }
            }
        }
        ReimbursmentApproval_Search();


    }

    protected void lnkView_Command(object sender, CommandEventArgs e)
    {
        string valuedata = e.CommandArgument.ToString();
        // Response.Redirect(valuedata);
        Page.ClientScript.RegisterStartupScript(
    this.GetType(), "OpenWindow", "window.open('" + valuedata + "','_newtab');", true);

    }
    string HODapr = "";
    string HRapr = "";
    string Blankapr = "";
    string PriorityHRapr = "";
    string PriorityHODapr = "";
    public void Showpermission()
    {
        string type = "For Reimbursment";
        SqlDataReader dr = con.SHow_tblesetupforpriorityforApproval(type, Session["Company"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            HODapr = dr["HOD"].ToString();
            HRapr = dr["HR"].ToString();
            Blankapr = dr["Blank"].ToString();
            PriorityHRapr = dr["PriorityHR"].ToString();
            PriorityHODapr = dr["PriorityHOD"].ToString();
        }
        dr.Close();
        con.DisConnect();
    }
    public void ReimbursmentApproval_Search()
    {


        if (rdAllReimApproval.Checked == true)
        {
            if (Blankapr == "1")
            {

                SqlDataReader odr = con.Reimbursment_Approval_Detail_withAllForBlank(Session["uid"].ToString(), Session["Company"].ToString());
                DataTable Dt = new DataTable();
                Dt.Load(odr);
                grdReimbursment.DataSource = Dt;
                grdReimbursment.DataBind();
                odr.Close();
                con.DisConnect();

                SqlDataReader dr = con.Reimbursment_Approval_Detail_withAllForBlank(Session["uid"].ToString(), Session["Company"].ToString());
                dr.Read();
                if (dr.HasRows)
                {
                    btnApprove.Visible = true;
                    btnreject.Visible = true;
                }
                else
                {
                    btnApprove.Visible = false;
                    btnreject.Visible = false;
                }
                dr.Close();
                con.DisConnect();
            }
            if (PriorityHODapr == "1")
            {
                if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                {
                    SqlDataReader odr = con.Reimbursment_Approval_Detail_withAllForHR(Session["uid"].ToString(),Session["Company"].ToString());
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdReimbursment.DataSource = Dt;
                    grdReimbursment.DataBind();
                    odr.Close();
                    con.DisConnect();

                    SqlDataReader dr = con.Reimbursment_Approval_Detail_withAllForHR(Session["uid"].ToString(), Session["Company"].ToString());
                    dr.Read();
                    if (dr.HasRows)
                    {
                        btnApprove.Visible = true;
                        btnreject.Visible = true;
                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnreject.Visible = false;
                    }
                    dr.Close();
                    con.DisConnect();

                }
                else
                {
                    SqlDataReader odr = con.Reimbursment_Approval_Detail_withAll(Session["uid"].ToString(),  Session["Company"].ToString());
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdReimbursment.DataSource = Dt;
                    grdReimbursment.DataBind();
                    odr.Close();
                    con.DisConnect();

                    SqlDataReader dr = con.Reimbursment_Approval_Detail_withAll(Session["uid"].ToString(),  Session["Company"].ToString());
                    dr.Read();
                    if (dr.HasRows)
                    {
                        btnApprove.Visible = true;
                        btnreject.Visible = true;
                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnreject.Visible = false;
                    }
                    dr.Close();
                    con.DisConnect();

                }
            }
            if (PriorityHRapr == "1")
            {
                if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                {

                    SqlDataReader odr = con.Reimbursment_Approval_Detail_withAllForHR(Session["uid"].ToString(),Session["Company"].ToString());
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdReimbursment.DataSource = Dt;
                    grdReimbursment.DataBind();
                    odr.Close();
                    con.DisConnect();

                    SqlDataReader dr = con.Reimbursment_Approval_Detail_withAllForHR(Session["uid"].ToString(), Session["Company"].ToString());
                    dr.Read();
                    if (dr.HasRows)
                    {
                        btnApprove.Visible = true;
                        btnreject.Visible = true;
                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnreject.Visible = false;
                    }
                    dr.Close();
                    con.DisConnect();


                }
                else
                {
                    SqlDataReader odr = con.Reimbursment_Approval_Detail_withAll(Session["uid"].ToString(), Session["Company"].ToString());
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdReimbursment.DataSource = Dt;
                    grdReimbursment.DataBind();
                    odr.Close();
                    con.DisConnect();

                    SqlDataReader dr = con.Reimbursment_Approval_Detail_withAll(Session["uid"].ToString(),Session["Company"].ToString());
                    dr.Read();
                    if (dr.HasRows)
                    {
                        btnApprove.Visible = true;
                        btnreject.Visible = true;
                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnreject.Visible = false;
                    }
                    dr.Close();
                    con.DisConnect();

                }
            }
        }



        if (rdDatewise.Checked == true)
        {
            if (Blankapr == "1")
            {
                

                SqlDataReader odr = con.Reimbursment_Approval_Detail_withdateForBlank(Session["uid"].ToString(), txtFromDate.Text, txtToDate.Text, Session["Company"].ToString());
                DataTable Dt = new DataTable();
                Dt.Load(odr);
                grdReimbursment.DataSource = Dt;
                grdReimbursment.DataBind();
                odr.Close();
                con.DisConnect();

                SqlDataReader dr = con.Reimbursment_Approval_Detail_withdateForBlank(Session["uid"].ToString(), txtFromDate.Text, txtToDate.Text, Session["Company"].ToString());
                dr.Read();
                if (dr.HasRows)
                {
                    btnApprove.Visible = true;
                    btnreject.Visible = true;
                }
                else
                {
                    btnApprove.Visible = false;
                    btnreject.Visible = false;
                }
                dr.Close();
                con.DisConnect();
            }
            if (PriorityHODapr == "1")
            {
                if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                {
                    SqlDataReader odr = con.Reimbursment_Approval_Detail_withdateForHR(Session["uid"].ToString(), txtFromDate.Text, txtToDate.Text, Session["Company"].ToString());
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdReimbursment.DataSource = Dt;
                    grdReimbursment.DataBind();
                    odr.Close();
                    con.DisConnect();

                    SqlDataReader dr = con.Reimbursment_Approval_Detail_withdateForHR(Session["uid"].ToString(), txtFromDate.Text, txtToDate.Text, Session["Company"].ToString());
                    dr.Read();
                    if (dr.HasRows)
                    {
                        btnApprove.Visible = true;
                        btnreject.Visible = true;
                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnreject.Visible = false;
                    }
                    dr.Close();
                    con.DisConnect();

                }
                else
                {
                    SqlDataReader odr = con.Reimbursment_Approval_Detail_withdate(Session["uid"].ToString(), txtFromDate.Text, txtToDate.Text, Session["Company"].ToString());
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdReimbursment.DataSource = Dt;
                    grdReimbursment.DataBind();
                    odr.Close();
                    con.DisConnect();

                    SqlDataReader dr = con.Reimbursment_Approval_Detail_withdate(Session["uid"].ToString(), txtFromDate.Text, txtToDate.Text, Session["Company"].ToString());
                    dr.Read();
                    if (dr.HasRows)
                    {
                        btnApprove.Visible = true;
                        btnreject.Visible = true;
                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnreject.Visible = false;
                    }
                    dr.Close();
                    con.DisConnect();
                
                }
            }
            if (PriorityHRapr == "1")
            {
                if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                {

                    SqlDataReader odr = con.Reimbursment_Approval_Detail_withdateForHR(Session["uid"].ToString(), txtFromDate.Text, txtToDate.Text, Session["Company"].ToString());
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdReimbursment.DataSource = Dt;
                    grdReimbursment.DataBind();
                    odr.Close();
                    con.DisConnect();

                    SqlDataReader dr = con.Reimbursment_Approval_Detail_withdateForHR(Session["uid"].ToString(), txtFromDate.Text, txtToDate.Text, Session["Company"].ToString());
                    dr.Read();
                    if (dr.HasRows)
                    {
                        btnApprove.Visible = true;
                        btnreject.Visible = true;
                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnreject.Visible = false;
                    }
                    dr.Close();
                    con.DisConnect();


                }
                else
                {
                    SqlDataReader odr = con.Reimbursment_Approval_Detail_withdate(Session["uid"].ToString(), txtFromDate.Text, txtToDate.Text, Session["Company"].ToString());
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdReimbursment.DataSource = Dt;
                    grdReimbursment.DataBind();
                    odr.Close();
                    con.DisConnect();

                    SqlDataReader dr = con.Reimbursment_Approval_Detail_withdate(Session["uid"].ToString(), txtFromDate.Text, txtToDate.Text, Session["Company"].ToString());
                    dr.Read();
                    if (dr.HasRows)
                    {
                        btnApprove.Visible = true;
                        btnreject.Visible = true;
                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnreject.Visible = false;
                    }
                    dr.Close();
                    con.DisConnect();
                
                }
            }
        }

        if (rdEmployeeID.Checked == true)
        {
            if (Blankapr == "1")
            {
                //Rebursment_Approval_Detail_withUserID
                
                SqlDataReader odr = con.Rebursment_Approval_Detail_withUserIDBlank(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                DataTable Dt = new DataTable();
                Dt.Load(odr);
                grdReimbursment.DataSource = Dt;
                grdReimbursment.DataBind();
                odr.Close();
                con.DisConnect();

                SqlDataReader dr = con.Rebursment_Approval_Detail_withUserIDBlank(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                dr.Read();
                if (dr.HasRows)
                {
                    btnApprove.Visible = true;
                    btnreject.Visible = true;
                }
                else
                {
                    btnApprove.Visible = false;
                    btnreject.Visible = false;
                }
                dr.Close();
                con.DisConnect();

            }
            if (PriorityHODapr == "1")
            {
                if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                {
                SqlDataReader odr = con.Rebursment_Approval_Detail_withUserIDHR(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                DataTable Dt = new DataTable();
                Dt.Load(odr);
                grdReimbursment.DataSource = Dt;
                grdReimbursment.DataBind();
                odr.Close();
                con.DisConnect();

                SqlDataReader dr = con.Rebursment_Approval_Detail_withUserIDHR(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                dr.Read();
                if (dr.HasRows)
                {
                    btnApprove.Visible = true;
                    btnreject.Visible = true;
                }
                else
                {
                    btnApprove.Visible = false;
                    btnreject.Visible = false;
                }
                dr.Close();
                con.DisConnect();
                
                }
                else
                {

                    SqlDataReader odr = con.Rebursment_Approval_Detail_withUserID(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdReimbursment.DataSource = Dt;
                    grdReimbursment.DataBind();
                    odr.Close();
                    con.DisConnect();

                    SqlDataReader dr = con.Rebursment_Approval_Detail_withUserID(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                    dr.Read();
                    if (dr.HasRows)
                    {
                        btnApprove.Visible = true;
                        btnreject.Visible = true;
                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnreject.Visible = false;
                    }
                    dr.Close();
                    con.DisConnect();
                }


            }
            if (PriorityHRapr == "1")
            {
                if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                {
                    SqlDataReader odr = con.Rebursment_Approval_Detail_withUserIDHR(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdReimbursment.DataSource = Dt;
                    grdReimbursment.DataBind();
                    odr.Close();
                    con.DisConnect();

                    SqlDataReader dr = con.Rebursment_Approval_Detail_withUserIDHR(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                    dr.Read();
                    if (dr.HasRows)
                    {
                        btnApprove.Visible = true;
                        btnreject.Visible = true;
                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnreject.Visible = false;
                    }
                    dr.Close();
                    con.DisConnect();

                }
                else
                {

                    SqlDataReader odr = con.Rebursment_Approval_Detail_withUserID(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdReimbursment.DataSource = Dt;
                    grdReimbursment.DataBind();
                    odr.Close();
                    con.DisConnect();

                    SqlDataReader dr = con.Rebursment_Approval_Detail_withUserID(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                    dr.Read();
                    if (dr.HasRows)
                    {
                        btnApprove.Visible = true;
                        btnreject.Visible = true;
                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnreject.Visible = false;
                    }
                    dr.Close();
                    con.DisConnect();
                }


            }
        }

        if (rdEmployeeName.Checked == true)
        {
            if (Blankapr == "1")
            {
                //Reimbursment_Approval_Detail_withUserName

                SqlDataReader odr = con.Reimbursment_Approval_Detail_withUserNameBlank(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                DataTable Dt = new DataTable();
                Dt.Load(odr);
                grdReimbursment.DataSource = Dt;
                grdReimbursment.DataBind();
                odr.Close();
                con.DisConnect();

                SqlDataReader dr = con.Reimbursment_Approval_Detail_withUserNameBlank(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                dr.Read();
                if (dr.HasRows)
                {
                    btnApprove.Visible = true;
                    btnreject.Visible = true;
                }
                else
                {
                    btnApprove.Visible = false;
                    btnreject.Visible = false;
                }
                dr.Close();
                con.DisConnect();

            }
            if (PriorityHODapr == "1")
            {
                if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                {
                    SqlDataReader odr = con.Reimbursment_Approval_Detail_withUserNameHR(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdReimbursment.DataSource = Dt;
                    grdReimbursment.DataBind();
                    odr.Close();
                    con.DisConnect();

                    SqlDataReader dr = con.Reimbursment_Approval_Detail_withUserNameHR(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                    dr.Read();
                    if (dr.HasRows)
                    {
                        btnApprove.Visible = true;
                        btnreject.Visible = true;
                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnreject.Visible = false;
                    }
                    dr.Close();
                    con.DisConnect();
                }
                else
                {

                    SqlDataReader odr = con.Reimbursment_Approval_Detail_withUserName(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdReimbursment.DataSource = Dt;
                    grdReimbursment.DataBind();
                    odr.Close();
                    con.DisConnect();

                    SqlDataReader dr = con.Reimbursment_Approval_Detail_withUserName(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                    dr.Read();
                    if (dr.HasRows)
                    {
                        btnApprove.Visible = true;
                        btnreject.Visible = true;
                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnreject.Visible = false;
                    }
                    dr.Close();
                    con.DisConnect();
                }
            }
            if (PriorityHRapr == "1")
            {
                if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                {
                    SqlDataReader odr = con.Reimbursment_Approval_Detail_withUserNameHR(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdReimbursment.DataSource = Dt;
                    grdReimbursment.DataBind();
                    odr.Close();
                    con.DisConnect();

                    SqlDataReader dr = con.Reimbursment_Approval_Detail_withUserNameHR(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                    dr.Read();
                    if (dr.HasRows)
                    {
                        btnApprove.Visible = true;
                        btnreject.Visible = true;
                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnreject.Visible = false;
                    }
                    dr.Close();
                    con.DisConnect();
                }
                else
                {

                    SqlDataReader odr = con.Reimbursment_Approval_Detail_withUserName(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdReimbursment.DataSource = Dt;
                    grdReimbursment.DataBind();
                    odr.Close();
                    con.DisConnect();

                    SqlDataReader dr = con.Reimbursment_Approval_Detail_withUserName(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                    dr.Read();
                    if (dr.HasRows)
                    {
                        btnApprove.Visible = true;
                        btnreject.Visible = true;
                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnreject.Visible = false;
                    }
                    dr.Close();
                    con.DisConnect();
                }
            }

        }



    }

    protected void grdReimbursment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblProfilechangedate1 = (e.Row.Cells[0].FindControl("lblBill_Detail") as Label);

            string textdata = lblProfilechangedate1.Text;
            string output = textdata.Substring(textdata.IndexOf('$') + 1);
            lblProfilechangedate1.Text = output;
        }
    }
    protected void grdReimbursment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdReimbursment.PageIndex = e.NewPageIndex;
        ReimbursmentApproval_Search();
    }
    string chkUSerID = ""; string profilechangedate = ""; string lblnoofchangeu1 = "";
    protected void btnApprove_Click(object sender, EventArgs e)
    {

        foreach (GridViewRow row in grdReimbursment.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {

                CheckBox chkRow = (row.Cells[0].FindControl("chkMark") as CheckBox);
                Label lblProfilechangedate1 = (row.Cells[0].FindControl("lblProfilechangedate") as Label);
                Label lblNoofchange = (row.Cells[0].FindControl("lblNoofchange") as Label);
                if (chkRow.Checked == true)
                {
                    profilechangedate = lblProfilechangedate1.Text;
                    chkUSerID = chkRow.Text;
                    lblnoofchangeu1 = lblNoofchange.Text;
                    FindCheckData();


                }
            }
        }
        ReimbursmentApproval_Search();
    
    }


    public void FindCheckData()
    {
        SqlDataAdapter da = new SqlDataAdapter("select * from tbl_reimbursmentDetail where Userid='" + chkUSerID + "' and Company_Name='" + Session["Company"].ToString() + "' and Create_Date ='" + profilechangedate + "' and SerialNo='" + lblnoofchangeu1 + "' ", con.Con);
        DataSet ds = new DataSet();
        da.Fill(ds, "tble_ProfileApprovalStatus");
        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
        {
            string Expense_Type = ""; string Credit_limit = ""; string Expense_Amount = ""; string Remarks = ""; string Bill_Detail = ""; string Userid = ""; string Department = ""; string Designation = ""; string HODUserid = ""; string HODRemarks = ""; string HRRemarks = ""; string Company_Name = ""; string SerialNo = ""; string Create_Date = ""; string HOD_Remarks_Date = ""; string Hr_Remarks_Date = ""; string HR_Userid = ""; string Status = ""; string UName = ""; string RejectedApproval = ""; string CompanyName1 = "";
            Expense_Type = ds.Tables[0].Rows[i]["Expense_Type"].ToString();
            Credit_limit = ds.Tables[0].Rows[i]["Credit_limit"].ToString();
            Expense_Amount = ds.Tables[0].Rows[i]["Expense_Amount"].ToString();
            Remarks = ds.Tables[0].Rows[i]["Remarks"].ToString();
            Bill_Detail = ds.Tables[0].Rows[i]["Bill_Detail"].ToString();
            Userid = ds.Tables[0].Rows[i]["Userid"].ToString();
            Department = ds.Tables[0].Rows[i]["Department"].ToString();
            Designation = ds.Tables[0].Rows[i]["Designation"].ToString();
            HODUserid = ds.Tables[0].Rows[i]["HODUserid"].ToString();
            HODRemarks = ds.Tables[0].Rows[i]["HODRemarks"].ToString();
            HRRemarks = ds.Tables[0].Rows[i]["HRRemarks"].ToString();
            Company_Name = ds.Tables[0].Rows[i]["Company_Name"].ToString();
            SerialNo = ds.Tables[0].Rows[i]["SerialNo"].ToString();
            Create_Date = ds.Tables[0].Rows[i]["Create_Date"].ToString();
            HOD_Remarks_Date = ds.Tables[0].Rows[i]["HOD_Remarks_Date"].ToString();
            Hr_Remarks_Date = ds.Tables[0].Rows[i]["Hr_Remarks_Date"].ToString();
            HR_Userid = ds.Tables[0].Rows[i]["HR_Userid"].ToString();
            Status = ds.Tables[0].Rows[i]["Status"].ToString();
            UName = ds.Tables[0].Rows[i]["UName"].ToString();
            RejectedApproval = ds.Tables[0].Rows[i]["Rejected Approval"].ToString();


            CompanyName1 = ds.Tables[0].Rows[i]["Company_Name"].ToString();

          
            con.Update_tbl_reimbursmentDetail_Resolved(System.DateTime.Now.ToString("dd/MM/yyyy"), chkUSerID, profilechangedate, lblnoofchangeu1, Session["Company"].ToString());
        }

    }

    protected void btnreject_Click(object sender, EventArgs e)
    {
        pnlrejectedDetail.Visible = true;
        ModalPopupExtender1.Show();
    }
    protected void rdAllReimApproval_CheckedChanged(object sender, EventArgs e)
    {
        pnlEmployeeidName.Visible = false;
      
        pnlDate.Visible = false;
        btnSearch.Visible = false;

        ReimbursmentApproval_Search();
    }
}