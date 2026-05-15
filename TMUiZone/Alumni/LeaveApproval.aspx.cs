using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Configuration;//ashuss
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
                if (Session["CompanyEmail"].ToString() != "vicechancellor@tmu.ac.in") { btnApprove.Text = "Recommend / Approve"; }
                pnlrejectedDetail.Visible = false;
                ModalPopupExtender1.Hide();
                CHKAllPending.Checked = true;
                LeaveApproval_Search();


            }

        }
        catch (Exception)
        {
            Response.Redirect("../Default.aspx");
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
        pnlrejectedDetail.Visible = false;
        ModalPopupExtender1.Hide();
        rdEmployeeID.Checked = false;
        rdEmployeeName.Checked = false;
        rdDatewise.Checked = false;
        CHKAllPending.Checked = true;
        LeaveApproval_Search();

    }
    protected void rdEmployeeID_CheckedChanged(object sender, EventArgs e)
    {
        pnlEmployeeidName.Visible = true;
        pnlDate.Visible = false;
        btnSearch.Visible = true;
        txtSearchName.Text = "";
        txtfromDate.Text = "";
        txtTodate.Text = "";
        btnreject.Visible = false;
        btnApprove.Visible = false;
        grdViewApproval.Visible = false;
        pnlrejectedDetail.Visible = false;
        btnselectchecked.Visible = false;
        btnuncheked.Visible = false;
        ModalPopupExtender1.Hide();

    }
    protected void rdEmployeeName_CheckedChanged(object sender, EventArgs e)
    {
        pnlEmployeeidName.Visible = true;
        pnlDate.Visible = false;
        btnSearch.Visible = true;
        txtSearchName.Text = "";
        txtfromDate.Text = "";
        txtTodate.Text = "";
        grdViewApproval.Visible = false;
        pnlrejectedDetail.Visible = false;
        btnreject.Visible = false;
        btnApprove.Visible = false;
        btnselectchecked.Visible = false;
        btnuncheked.Visible = false;
        ModalPopupExtender1.Hide();
    }
    protected void rdDatewise_CheckedChanged(object sender, EventArgs e)
    {
        pnlEmployeeidName.Visible = false;
        pnlDate.Visible = true;
        btnSearch.Visible = true;
        txtSearchName.Text = "";
        txtfromDate.Text = "";
        txtTodate.Text = "";
        btnreject.Visible = false;
        btnApprove.Visible = false;
        grdViewApproval.Visible = false;
        pnlrejectedDetail.Visible = false;
        btnselectchecked.Visible = false;
        btnuncheked.Visible = false;
        ModalPopupExtender1.Hide();
    }
    protected void rdLeaveRectedEMPID_CheckedChanged(object sender, EventArgs e)
    {
        pnlLeaveRejectedEMPID.Visible = true;
        btnrejectedsearch.Visible = true;
        pnlLeaveRejectedDatewise.Visible = false;
        grdRejected.Visible = false;
        pnlrejectedDetail.Visible = false;
        ModalPopupExtender1.Hide();
        txtRejectedSearch.Text = "";
        txtRejectedFromDate.Text = "";
        txtRejectedToDate.Text = "";
    }
    protected void rdrdLeaveRectedName_CheckedChanged(object sender, EventArgs e)
    {
        pnlLeaveRejectedEMPID.Visible = true;
        btnrejectedsearch.Visible = true;
        pnlLeaveRejectedDatewise.Visible = false;
        txtRejectedSearch.Text = "";
        txtRejectedFromDate.Text = "";
        txtRejectedToDate.Text = "";
        pnlrejectedDetail.Visible = false;
        ModalPopupExtender1.Hide();
        grdRejected.Visible = false;

    }
    protected void rdLeaveRectedDatewise_CheckedChanged(object sender, EventArgs e)
    {
        pnlLeaveRejectedEMPID.Visible = false;
        btnrejectedsearch.Visible = true;
        pnlLeaveRejectedDatewise.Visible = true;
        pnlrejectedDetail.Visible = false;
        ModalPopupExtender1.Hide();
        txtRejectedSearch.Text = "";
        txtRejectedFromDate.Text = "";
        txtRejectedToDate.Text = "";
        grdRejected.Visible = false;
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

        rdProfileRectedEMPID.Checked = false;
        rdProfileRectedDatewise.Checked = false;
        rdrdProfileRectedName.Checked = false;
        txtRejectedSearch.Text = "";
        txtRejectedFromDate.Text = "";
        txtRejectedToDate.Text = "";
        grdRejected.Visible = true;
        pnlrejectedDetail.Visible = false;
        rdAllReject.Checked = true;
        LeaveRejected_Search();

        ModalPopupExtender1.Hide();

    }
    protected void lnkApprovedApproveddetail_Click(object sender, EventArgs e)
    {
        pnlApprovedDetail.Visible = true;
        pnlProfileRejected.Visible = false;
        pnlProfileView.Visible = false;
        btnApprovedSearch.Visible = false;
        pnlLeaveApprovedDetailDatewise.Visible = false;
        pnlLeaveApprovedDetailEmpid.Visible = false;
        pnlMain.Visible = false;
        rdApprovedEmpid.Checked = false;
        rdApprovedEMPName.Checked = false;
        rdApprovedDatewise.Checked = false;
        txtResolvedSearch.Text = "";
        txtApprovedFromDate.Text = "";
        txtApprovedToDate.Text = "";
        grdResolved.Visible = true;

        pnlrejectedDetail.Visible = false;
        rdAllApprove.Checked = true;

        LeaveResolved_Search();
        ModalPopupExtender1.Hide();
    }
    protected void rdApprovedEmpid_CheckedChanged(object sender, EventArgs e)
    {
        pnlLeaveApprovedDetailEmpid.Visible = true;
        pnlLeaveApprovedDetailDatewise.Visible = false;
        btnApprovedSearch.Visible = true;
        txtResolvedSearch.Text = "";
        txtApprovedFromDate.Text = "";
        txtApprovedToDate.Text = "";
        grdResolved.Visible = false;
    }
    protected void rdApprovedEMPName_CheckedChanged(object sender, EventArgs e)
    {
        pnlLeaveApprovedDetailEmpid.Visible = true;
        pnlLeaveApprovedDetailDatewise.Visible = false;
        btnApprovedSearch.Visible = true;
        pnlrejectedDetail.Visible = false;
        txtResolvedSearch.Text = "";
        txtApprovedFromDate.Text = "";
        txtApprovedToDate.Text = "";
        ModalPopupExtender1.Hide();
        grdResolved.Visible = false;
    }
    protected void rdApprovedDatewise_CheckedChanged(object sender, EventArgs e)
    {
        pnlLeaveApprovedDetailEmpid.Visible = false;
        pnlLeaveApprovedDetailDatewise.Visible = true;
        btnApprovedSearch.Visible = true;
        pnlrejectedDetail.Visible = false;
        txtResolvedSearch.Text = "";
        txtApprovedFromDate.Text = "";
        txtApprovedToDate.Text = "";
        ModalPopupExtender1.Hide();
        grdResolved.Visible = false;
    }
    string HODapr = "";
    string HRapr = "";
    string Blankapr = "";
    string PriorityHRapr = "";
    string PriorityHODapr = "";

    string EmailBlank = "";
    string EmailHR = "";
    string EmailHOD = "";
    public void Showpermission()
    {
        string type = "For Leave";
        SqlDataReader dr = con.SHow_tblesetupforpriorityforApproval(type, Session["Company"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            HODapr = dr["HOD"].ToString();
            HRapr = dr["HR"].ToString();
            Blankapr = dr["Blank"].ToString();
            PriorityHRapr = dr["PriorityHR"].ToString();
            PriorityHODapr = dr["PriorityHOD"].ToString();

            EmailBlank = dr["EmailBlank"].ToString();
            EmailHR = dr["EmailHR"].ToString();
            EmailHOD = dr["EmailHOD"].ToString();
        }
        dr.Close();
        con.DisConnect();
    }


    public void LeaveApproval_Search()
    {








        if (rdDatewise.Checked == true)
        {


            if (PriorityHODapr == "1")
            {

                if (Session["CompanyEmail"].ToString() == "vicechancellor@tmu.ac.in")
                {


                    SqlDataReader odr = con.Leave_Approval_Detail_withdateVC(Session["uid"].ToString(), txtfromDate.Text, txtTodate.Text, Session["Company"].ToString());
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdViewApproval.DataSource = Dt;
                    grdViewApproval.DataBind();
                    odr.Close();
                    con.DisConnect();

                    SqlDataReader dr = con.Leave_Approval_Detail_withdateVC(Session["uid"].ToString(), txtfromDate.Text, txtTodate.Text, Session["Company"].ToString());
                    dr.Read();
                    if (dr.HasRows)
                    {
                        btnApprove.Visible = true;
                        btnreject.Visible = true;
                        btnselectchecked.Visible = true;
                        btnselectchecked.Visible = true;

                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnreject.Visible = false;
                        btnselectchecked.Visible = false;
                        btnselectchecked.Visible = false;

                    }
                    dr.Close();
                    con.DisConnect();
                }
                else
                {





                    SqlDataReader odr = con.Leave_Approval_Detail_withdate(Session["uid"].ToString(), txtfromDate.Text, txtTodate.Text, Session["Company"].ToString());
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdViewApproval.DataSource = Dt;
                    grdViewApproval.DataBind();
                    odr.Close();
                    con.DisConnect();
                    SqlDataReader dr = con.Leave_Approval_Detail_withdate(Session["uid"].ToString(), txtfromDate.Text, txtTodate.Text, Session["Company"].ToString());
                    dr.Read();
                    if (dr.HasRows)
                    {
                        btnApprove.Visible = true;
                        btnreject.Visible = true;
                        btnselectchecked.Visible = true;
                        btnselectchecked.Visible = true;

                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnreject.Visible = false;
                        btnselectchecked.Visible = false;
                        btnselectchecked.Visible = false;

                    }
                    dr.Close();
                    con.DisConnect();
                    //}
                }
            }

        }

        if (rdEmployeeID.Checked == true)
        {


            if (PriorityHODapr == "1")
            {


                if (Session["CompanyEmail"].ToString() == "vicechancellor@tmu.ac.in")
                {
                    SqlDataReader odr = con.Leave_Approval_Detail_withUserIDVC(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdViewApproval.DataSource = Dt;
                    grdViewApproval.DataBind();
                    odr.Close();
                    con.DisConnect();

                    SqlDataReader dr = con.Leave_Approval_Detail_withUserIDVC(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                    dr.Read();
                    if (dr.HasRows)
                    {
                        btnApprove.Visible = true;
                        btnreject.Visible = true;
                        btnselectchecked.Visible = true;
                        btnselectchecked.Visible = true;

                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnreject.Visible = false;
                        btnselectchecked.Visible = false;
                        btnselectchecked.Visible = false;

                    }
                    dr.Close();
                    con.DisConnect();


                }
                else
                {

                    SqlDataReader odr = con.Leave_Approval_Detail_withUserID(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdViewApproval.DataSource = Dt;
                    grdViewApproval.DataBind();
                    odr.Close();
                    con.DisConnect();

                    SqlDataReader dr = con.Leave_Approval_Detail_withUserID(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                    dr.Read();
                    if (dr.HasRows)
                    {
                        btnApprove.Visible = true;
                        btnreject.Visible = true;
                        btnselectchecked.Visible = true;
                        btnselectchecked.Visible = true;

                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnreject.Visible = false;
                        btnselectchecked.Visible = false;
                        btnselectchecked.Visible = false;

                    }
                    dr.Close();
                    con.DisConnect();

                }
            }
        }
        if (rdEmployeeName.Checked == true)
        {

            if (PriorityHODapr == "1")
            {


                if (Session["CompanyEmail"].ToString() == "vicechancellor@tmu.ac.in")
                {
                    SqlDataReader odr = con.Leave_Approval_Detail_withUserNameVC(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdViewApproval.DataSource = Dt;
                    grdViewApproval.DataBind();
                    odr.Close();
                    con.DisConnect();

                    SqlDataReader dr = con.Leave_Approval_Detail_withUserNameVC(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                    dr.Read();
                    if (dr.HasRows)
                    {
                        btnApprove.Visible = true;
                        btnreject.Visible = true;
                        btnselectchecked.Visible = true;
                        btnselectchecked.Visible = true;

                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnreject.Visible = false;
                        btnselectchecked.Visible = false;
                        btnselectchecked.Visible = false;

                    }
                    dr.Close();
                    con.DisConnect();

                }

                else
                {

                    SqlDataReader odr = con.Leave_Approval_Detail_withUserName(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdViewApproval.DataSource = Dt;
                    grdViewApproval.DataBind();
                    odr.Close();
                    con.DisConnect();

                    SqlDataReader dr = con.Leave_Approval_Detail_withUserName(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                    dr.Read();
                    if (dr.HasRows)
                    {
                        btnApprove.Visible = true;
                        btnreject.Visible = true;
                        btnselectchecked.Visible = true;
                        btnselectchecked.Visible = true;

                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnreject.Visible = false;
                        btnselectchecked.Visible = false;
                        btnselectchecked.Visible = false;

                    }
                    dr.Close();
                    con.DisConnect();
                }
            }


        }

        if (CHKAllPending.Checked == true)
        {

            if (PriorityHODapr == "1")
            {
                if (Session["CompanyEmail"].ToString() == "vicechancellor@tmu.ac.in")
                {

                    SqlDataAdapter da = new SqlDataAdapter("select count(*) as 'Final' from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Pay Leave] where ([SA for Non-Teach Staff]='" + Session["uid"].ToString() + "' or [SA for Teaching Staff]='" + Session["uid"].ToString() + "')and [Leave Code] collate Latin1_General_100_CS_AS in (select Leave_Type from tble_Leave_Approval where  (HODUserid='" + Session["uid"].ToString() + "' or HODUserid1='" + Session["uid"].ToString() + "' ) and Company_Name='TMU' and (Status='Approved' )  and [Rejected Approval]='No'  )", con.Con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (Convert.ToInt32(dt.Rows[0]["Final"]) > 0)
                    {
                        SqlDataReader odr = con.Leave_Approval_Detail_withHODALLPendingVC1(Session["uid"].ToString(), Session["Company"].ToString());
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        try
                        {
                            DataTable dtnew = Dt.Select("FinalApprovalStatus=0").CopyToDataTable();
                            //DataTable dtnew = Dt.Select("FinalApprovalStatus=0");
                            grdViewApproval.DataSource = dtnew;
                            grdViewApproval.DataBind();
                            odr.Close();
                            con.DisConnect();
                        }
                        catch (Exception ex)
                        {
                        }

                        SqlDataReader dr = con.Leave_Approval_Detail_withHODALLPendingVC1(Session["uid"].ToString(), Session["Company"].ToString());
                        dr.Read();
                        if (dr.HasRows)
                        {
                            btnApprove.Visible = true;
                            btnreject.Visible = true;
                            btnselectchecked.Visible = true;
                            btnselectchecked.Visible = true;

                        }
                        else
                        {
                            btnApprove.Visible = false;
                            btnreject.Visible = false;
                            btnselectchecked.Visible = false;
                            btnselectchecked.Visible = false;

                        }
                        dr.Close();
                        con.DisConnect();

                    }
                    else
                    {





                        SqlDataReader odr = con.Leave_Approval_Detail_withHODALLPendingVC(Session["uid"].ToString(), Session["Company"].ToString());
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdViewApproval.DataSource = Dt;
                        grdViewApproval.DataBind();
                        odr.Close();
                        con.DisConnect();

                        SqlDataReader dr = con.Leave_Approval_Detail_withHODALLPendingVC(Session["uid"].ToString(), Session["Company"].ToString());
                        dr.Read();
                        if (dr.HasRows)
                        {
                            btnApprove.Visible = true;
                            btnreject.Visible = true;
                            btnselectchecked.Visible = true;
                            btnselectchecked.Visible = true;

                        }
                        else
                        {
                            btnApprove.Visible = false;
                            btnreject.Visible = false;
                            btnselectchecked.Visible = false;
                            btnselectchecked.Visible = false;

                        }
                        dr.Close();
                        con.DisConnect();
                    }
                }
                else
                {
                    if (Session["GlobalDimension1Coded"].ToString() == "TMMC")
                    {

                        SqlDataAdapter daOrd = new SqlDataAdapter("select convert(varchar(11),(convert(date,From_Date )),113) as F_Date,convert(varchar(11),(convert(date,To_Date )),113) as T_Date,UserId,Status,PostLunch,PreLunch,id,Leave_Period,No_Of_Days_Leave_Period,Leave_Type,Half_Day_type_Desc,No_Of_Days_Leave_Period,Arrangement,Reason,AttachmentFilename,AutoNo,Address_Phone_No,case isnull(AttachmentName,'') when '' then 'No' else 'Download' end as Upload ,UserID,UName from tble_Leave_Approval where  [FinalApprovalId]='" + Session["uid"].ToString() + "'  and Company_Name='" + Session["Company"].ToString() + "' and [FinalApprovalStatus]=0 and [Rejected Approval]='No' and Status='Approved'  union select convert(varchar(11),(convert(date,From_Date )),113) as F_Date,convert(varchar(11),(convert(date,To_Date )),113) as T_Date,UserId,Status,PostLunch,PreLunch,id,Leave_Period,No_Of_Days_Leave_Period,Leave_Type,Half_Day_type_Desc,No_Of_Days_Leave_Period,Arrangement,Reason,AttachmentFilename,AutoNo,Address_Phone_No,case isnull(AttachmentName,'') when '' then 'No' else 'Download' end as Upload ,UserID,UName from tble_Leave_Approval where  [HODUserid]='" + Session["uid"].ToString() + "'  and Company_Name='" + Session["Company"].ToString() + "' and [FinalApprovalStatus]=0 and [Rejected Approval]='No' and Status='Pending'  ", con.Con);
                        DataTable Dtord = new DataTable();
                        daOrd.Fill(Dtord);


                        grdViewApproval.DataSource = Dtord;
                        grdViewApproval.DataBind();

                        con.DisConnect();



                        if (Dtord.Rows.Count > 0)
                        {
                            btnApprove.Visible = true;
                            btnreject.Visible = true;
                            btnselectchecked.Visible = true;
                            btnselectchecked.Visible = true;

                        }
                        else
                        {
                            btnApprove.Visible = false;
                            btnreject.Visible = false;
                            btnselectchecked.Visible = false;
                            btnselectchecked.Visible = false;

                        }




                    }

                    else
                    {










                        SqlDataAdapter da = new SqlDataAdapter("select count(*) as 'Final' from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Pay Leave] where ([SA for Non-Teach Staff]='" + Session["uid"].ToString() + "' or [SA for Teaching Staff]='" + Session["uid"].ToString() + "')and [Leave Code] collate Latin1_General_100_CS_AS in (select Leave_Type from tble_Leave_Approval where  (HODUserid='" + Session["uid"].ToString() + "' or HODUserid1='" + Session["uid"].ToString() + "' ) and Company_Name='TMU' and (Status='Approved' )  and [Rejected Approval]='No'  ) ", con.Con);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        if (Convert.ToInt32(ds.Tables[0].Rows[0]["Final"]) > 0)
                        {

                            SqlDataAdapter daOrd = new SqlDataAdapter("select convert(varchar(11),(convert(date,From_Date )),113) as F_Date,convert(varchar(11),(convert(date,To_Date )),113) as T_Date,UserId,Status,PostLunch,PreLunch,id,Leave_Period,No_Of_Days_Leave_Period,Leave_Type,Half_Day_type_Desc,No_Of_Days_Leave_Period,Arrangement,Reason,AttachmentFilename,AutoNo,Address_Phone_No,case isnull(AttachmentName,'') when '' then 'No' else 'Download' end as Upload ,UserID,UName from tble_Leave_Approval where  [FinalApprovalId]='" + Session["uid"].ToString() + "'  and Company_Name='" + Session["Company"].ToString() + "' and [FinalApprovalStatus]=0 and [Rejected Approval]='No' and Status='Approved'  union select convert(varchar(11),(convert(date,From_Date )),113) as F_Date,convert(varchar(11),(convert(date,To_Date )),113) as T_Date,UserId,Status,PostLunch,PreLunch,id,Leave_Period,No_Of_Days_Leave_Period,Leave_Type,Half_Day_type_Desc,No_Of_Days_Leave_Period,Arrangement,Reason,AttachmentFilename,AutoNo,Address_Phone_No,case isnull(AttachmentName,'') when '' then 'No' else 'Download' end as Upload ,UserID,UName from tble_Leave_Approval where  [HODUserid]='" + Session["uid"].ToString() + "'  and Company_Name='" + Session["Company"].ToString() + "' and [FinalApprovalStatus]=0 and [Rejected Approval]='No' and Status='Pending'  ", con.Con);
                            DataTable Dtord = new DataTable();
                            daOrd.Fill(Dtord);

                            //SqlDataReader odr = con.Leave_Approval_Detail_withHODALLPending1(Session["uid"].ToString(), Session["Company"].ToString());
                            //DataTable Dt = new DataTable();
                            //Dt.Load(odr);
                            grdViewApproval.DataSource = Dtord;
                            grdViewApproval.DataBind();
                            //odr.Close();
                            con.DisConnect();

                            SqlDataReader dr = con.Leave_Approval_Detail_withHODALLPending1(Session["uid"].ToString(), Session["Company"].ToString());
                            dr.Read();

                            if (dr.HasRows)
                            {
                                btnApprove.Visible = true;
                                btnreject.Visible = true;
                                btnselectchecked.Visible = true;
                                btnselectchecked.Visible = true;

                            }
                            else
                            {
                                btnApprove.Visible = false;
                                btnreject.Visible = false;
                                btnselectchecked.Visible = false;
                                btnselectchecked.Visible = false;

                            }

                            dr.Close();
                            con.DisConnect();
                        }
                        else
                        {

                            SqlDataReader odr = con.Leave_Approval_Detail_withHODALLPending(Session["uid"].ToString(), Session["Company"].ToString());
                            DataTable Dt = new DataTable();
                            Dt.Load(odr);
                            grdViewApproval.DataSource = Dt;
                            grdViewApproval.DataBind();
                            odr.Close();
                            con.DisConnect();

                            SqlDataReader dr = con.Leave_Approval_Detail_withHODALLPending(Session["uid"].ToString(), Session["Company"].ToString());
                            dr.Read();

                            if (dr.HasRows)
                            {
                                btnApprove.Visible = true;
                                btnreject.Visible = true;
                                btnselectchecked.Visible = true;
                                btnselectchecked.Visible = true;

                            }
                            else
                            {
                                btnApprove.Visible = false;
                                btnreject.Visible = false;
                                btnselectchecked.Visible = false;
                                btnselectchecked.Visible = false;

                            }

                            dr.Close();
                            con.DisConnect();
                        }
                    }
                }
                //}
            }

        }
    }
    public void showoffLeavesetup(string LeaveType)
    {
        ///under Process

        SqlDataReader dr = con.Show_tble_leave_setup(Session["Company"].ToString(), LeaveType);
        dr.Read();
        if (dr.HasRows)
        {
            lblHolidayexpect.Text = dr["Club Holiday"].ToString();
            if (lblHolidayexpect.Text == "Yes")
            {
                lblHolidayexpect.Text = "1";
            }
            if (lblHolidayexpect.Text == "No")
            {
                lblHolidayexpect.Text = "0";
            }

            lblOffdayexpect.Text = dr["Club Holiday"].ToString();
            if (lblOffdayexpect.Text == "Yes")
            {

                lblOffdayexpect.Text = "1";
            }
            if (lblOffdayexpect.Text == "No")
            {

                lblOffdayexpect.Text = "0";
            }
        }
        dr.Close();
        con.DisConnect();

    }
    double unapprovedleavess;
    public void UpdatePayPayEmployeeLeaveEntitled(string Leave_Code, string UserID, double unapprovedleave, string To_No_OfLeave)
    {
        string ccnameUN = Session["Company"].ToString();
        string rccname = ccnameUN.Replace(".", "_");
        string tble_Pay_Employee_Leave_Entitled = "[" + rccname + "$Pay Employee Leave Entitled" + "]";

        SqlDataReader dr = Portalcon.ShowLeaveBalance_DetailwithOption(tble_Pay_Employee_Leave_Entitled, UserID, Leave_Code);
        dr.Read();
        if (dr.HasRows)
        {
            string lb = dr["Leave Balance"].ToString();
            double To_No_OfLeave1 = Convert.ToDouble(To_No_OfLeave);
            string unapproved = dr["Unapproved Leave"].ToString();
            double UnAP1 = Convert.ToDouble(unapproved);
            double lbc = Convert.ToDouble(lb);
            double lbc1 = Convert.ToDouble(lbc - To_No_OfLeave1);


            unapprovedleavess = UnAP1 - To_No_OfLeave1;


            dr.Close();
            Portalcon.DisConnect();

            Portalcon.Update_Pay_Employee_Leave_EntitledLeave_BalanceUnaprovedafterApproval(lbc1, tble_Pay_Employee_Leave_Entitled, UserID, Leave_Code, unapprovedleavess);
            Portalcon.DisConnect();

        }
        else
        {
            dr.Close();
            Portalcon.DisConnect();
        }
    }






    protected void btnSearch_Click(object sender, EventArgs e)
    {
        grdViewApproval.Visible = true;
        LeaveApproval_Search();
    }
    string chkUSerID = ""; string profilechangedate = ""; string lblnoofchangeu1 = ""; string HRAPRStatus = "";
    string HODAPRStatus = "";
    string tblenameAttendence = "";
    string ststatusapprovalss = ""; string tablenameemployeedata = ""; string tblePayEmployeeNet = ""; string Finalstatus = "";
    public void FindCheckData(string LeaveType)
    {

        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        tblenameAttendence = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
        tablenameemployeedata = "[" + rccname + "$Employee" + "]";
        tblePayEmployeeNet = "[" + rccname + "$Pay Employee Net" + "]";

        SqlDataReader drall = con.Approved_LeaveData(Session["Company"].ToString(), chkUSerID, profilechangedate);
        drall.Read();
        if (drall.HasRows)
        {


            string monthAppy = ""; string yearApply = "";
            string AutoNo = ""; string Half_Day_type_Code = ""; string PreLunchvar = ""; string PostLunchvar = "";
            string Uname = ""; string From_Date = ""; string To_Date = ""; string Leave_Period = ""; string No_Of_Days_Leave_Period = ""; string Leave_Type = ""; string timein = ""; string timeout = ""; string hourpresent = "00.00"; string status = ""; string userEmailid = ""; string HR_Userid = ""; string HREmailidleav = ""; string HODEmailIDleav = ""; string HRNameleav = ""; string HODNameleav = "";
            Uname = drall["Uname"].ToString();
            From_Date = drall["From_Date"].ToString();
            To_Date = drall["To_Date"].ToString();
            userEmailid = drall["user_Emailid"].ToString();
            HR_Userid = drall["HR_Userid"].ToString();
            AutoNo = drall["AutoNo"].ToString();
            ststatusapprovalss = drall["Status"].ToString();
            HREmailidleav = drall["HREmailid"].ToString();
            HODEmailIDleav = drall["HODEmailID"].ToString();
            HRNameleav = drall["HRName"].ToString();
            HODNameleav = drall["HODName"].ToString();
            PreLunchvar = drall["PreLunch"].ToString();
            PostLunchvar = drall["PostLunch"].ToString();
            string Leavep = drall["Leave_Period"].ToString();
            if (Leavep == "(Full-Day)")
            {
                Leave_Period = "3";
            }

            if (Leavep == "(Half-Day)")
            {
                Leave_Period = "2";
            }
            No_Of_Days_Leave_Period = drall["No_Of_Days_Leave_Period"].ToString();
            Leave_Type = drall["Leave_Type"].ToString();

            drall.Close();
            drall.Dispose();




            if (Leave_Type == "LWP" && Leavep == "(Full-Day)")
            {
                status = "6";
            }
            if (Leave_Type == "LWP" && Leavep == "(Half-Day)")
            {
                status = "5";
            }
            if (Leave_Type == "Special Leave")
            {
                status = "7";
            }
            else if (status == "")
            {
                status = "4";
            }

            DateTime ApplieddateFrom = DateTime.ParseExact(From_Date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            DateTime Applieddateto = DateTime.ParseExact(To_Date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

            SqlDataReader drSlGNRT = Portalcon.SHow_SalaryProcessingMonth_PayEmployeeNet(tblePayEmployeeNet, ApplieddateFrom.ToString("MM"), ApplieddateFrom.ToString("yyyy"), chkUSerID, Applieddateto.ToString("MM"), Applieddateto.ToString("yyyy"));
            drSlGNRT.Read();
            if (drSlGNRT.HasRows)
            {

                drSlGNRT.Close();
                Portalcon.DisConnect();
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Leave is not approved because salary has been generated please contact payroll department and reject from rejection option');", true);
            }
            else
            {

                drSlGNRT.Close();
                Portalcon.DisConnect();


                SqlDataReader dr = Portalcon.Show_AttendenceDateFromNavisionforLeave(tblenameAttendence, From_Date, To_Date, chkUSerID);
                dr.Read();
                if (dr.HasRows)
                {
                    dr.Close();
                    Portalcon.DisConnect();

                    if (Leave_Type == "CL")
                    {
                        string weekdaycode = ""; string holidaycode = "";
                        string shiftpattern = ""; string shiftday = ""; string compholidayallowed = ""; string branchcode = "";
                        string ccnamedd = Session["Company"].ToString();
                        string rccnamedd = ccname.Replace(".", "_");
                        string tblemployeed = "[" + rccname + "$Employee" + "]";
                        SqlDataReader drcl = Portalcon.Show_weekofforapproval(tblemployeed, chkUSerID);
                        drcl.Read();
                        if (drcl.HasRows)
                        {
                            shiftpattern = drcl["Shift Pattern"].ToString();

                            shiftday = drcl["Weekly Off only Fixed Shift"].ToString();
                            compholidayallowed = drcl["Company Holiday Allowed"].ToString();
                            branchcode = drcl["Global Dimension 1 Code"].ToString();
                            drcl.Close();
                            if (shiftday == "1")
                            {

                                if (ApplieddateFrom.ToString("yyyy-MM-dd") == "2023-05-28")
                                {
                                    weekdaycode = "Sunday_Working";
                                }
                                else
                                {
                                    weekdaycode = "Sunday";
                                }
                            }
                            if (shiftday == "2")
                            {
                                weekdaycode = "Monday";
                            }
                            if (shiftday == "3")
                            {
                                weekdaycode = "Tuesday";
                            }
                            if (shiftday == "4")
                            {
                                weekdaycode = "Wednesday";
                            }
                            if (shiftday == "5")
                            {
                                weekdaycode = "Thursday";
                            }
                            if (shiftday == "6")
                            {
                                weekdaycode = "Friday";
                            }
                            if (shiftday == "7")
                            {
                                weekdaycode = "Saturday";
                            }


                            if (shiftpattern == "0")
                            {


                                if (compholidayallowed == "0")
                                {
                                    if (ststatusapprovalss == "Pending")
                                    {
                                        string ccnamse = Session["Company"].ToString();
                                        string rccnasme = ccnamse.Replace(".", "_");
                                        string tblPayHolidays = "[" + rccnasme + "$Pay Holidays" + "]";

                                        holidaycode = weekdaycode;
                                        Portalcon.Update_attendanceforfixedshift(tblenameAttendence, timein, timeout, status, hourpresent, chkUSerID, From_Date, To_Date, Leave_Period, Leave_Type, Half_Day_type_Code.Trim(), weekdaycode, holidaycode);
                                        Portalcon.DisConnect();


                                        SqlDataAdapter da = new SqlDataAdapter("select convert(varchar(11), [Date],113) as Date1 ,*  from " + tblPayHolidays + " where convert(date, [Date],103)  >='" + From_Date + "' and  convert(date, [Date],103)  <='" + To_Date + "' and [Branch Code]='" + branchcode + "' and DATENAME(dw,Date) !='" + weekdaycode + "'", Portalcon.Con);
                                        DataSet ds = new DataSet();
                                        da.Fill(ds, "tbl_EmployeepunchData");
                                        for (int j = 0; j <= ds.Tables[0].Rows.Count - 1; j++)
                                        {

                                            string holidaydate = "";
                                            // holidaydate = ds.Tables[0].Rows[j]["Date"].ToString();
                                            holidaydate = ds.Tables[0].Rows[j]["Date1"].ToString();

                                            Portalcon.Update_attendanceHoliday(tblenameAttendence, chkUSerID, holidaydate);

                                        }


                                        Portalcon.Update_attendanceOFFday(tblenameAttendence, chkUSerID, From_Date, To_Date, weekdaycode);
                                        Portalcon.DisConnect();

                                        con.Update_tble_Leaveapr_ResolvedHODFinal(System.DateTime.Now.ToString("dd-MM-yyyy"), profilechangedate, Session["uname"].ToString(), Session["uid"].ToString());
                                        con.DisConnect();

                                        UpdatePayPayEmployeeLeaveEntitled(Leave_Type, chkUSerID, unapprovedleavess, No_Of_Days_Leave_Period);
                                        con.DisConnect();

                                        UpdateCO(Leave_Type, AutoNo, chkUSerID);
                                        try
                                        {
                                            SendSMSForApprovedLeave(chkUSerID.Trim(), From_Date, To_Date, Leave_Type, Uname);
                                        }
                                        catch (Exception)
                                        { }
                                    }
                                }
                                else
                                {
                                    if (ststatusapprovalss == "Pending")
                                    {

                                        holidaycode = weekdaycode;
                                        Portalcon.Update_attendanceforfixedshift(tblenameAttendence, timein, timeout, status, hourpresent, chkUSerID, From_Date, To_Date, Leave_Period, Leave_Type, Half_Day_type_Code.Trim(), weekdaycode, holidaycode);
                                        Portalcon.DisConnect();
                                        Portalcon.Update_attendanceOFFday(tblenameAttendence, chkUSerID, From_Date, To_Date, weekdaycode);
                                        Portalcon.DisConnect();

                                        con.Update_tble_Leaveapr_ResolvedHODFinal(System.DateTime.Now.ToString("dd-MM-yyyy"), profilechangedate, Session["uname"].ToString(), Session["uid"].ToString());
                                        con.DisConnect();

                                        UpdatePayPayEmployeeLeaveEntitled(Leave_Type, chkUSerID, unapprovedleavess, No_Of_Days_Leave_Period);
                                        con.DisConnect();

                                        UpdateCO(Leave_Type, AutoNo, chkUSerID);
                                        try
                                        {
                                            SendSMSForApprovedLeave(chkUSerID.Trim(), From_Date, To_Date, Leave_Type, Uname);
                                        }
                                        catch (Exception)
                                        { }

                                    }
                                }
                            }

                            if (shiftpattern == "2")
                            {
                                if (ststatusapprovalss == "Pending")
                                {

                                    Portalcon.Update_AttendencewithApprovalforLeavenotinholidayoffday(tblenameAttendence, timein, timeout, status, hourpresent, chkUSerID, From_Date, To_Date, Leave_Period, Leave_Type, Half_Day_type_Code.Trim());
                                    Portalcon.DisConnect();
                                    con.Update_tble_Leaveapr_ResolvedHODFinal(System.DateTime.Now.ToString("dd-MM-yyyy"), profilechangedate, Session["uname"].ToString(), Session["uid"].ToString());
                                    con.DisConnect();

                                    UpdatePayPayEmployeeLeaveEntitled(Leave_Type, chkUSerID, unapprovedleavess, No_Of_Days_Leave_Period);
                                    con.DisConnect();

                                    UpdateCO(Leave_Type, AutoNo, chkUSerID);

                                    string ccnamewe = Session["Company"].ToString();
                                    string rccnamewe = ccnamewe.Replace(".", "_");
                                    string tblPayHolidayswe = "[" + rccnamewe + "$Pay Holidays" + "]";
                                    string tblEmployeeWeekShiftMasterwe = "[" + rccnamewe + "$Employee Week Shift Master" + "]";

                                    SqlDataAdapter da = new SqlDataAdapter("select *  from " + tblEmployeeWeekShiftMasterwe + " where convert(date, [Start Date],103)  >='" + From_Date + "' and  convert(date, [Start Date],103)  <='" + To_Date + "' and [Employee No_]='" + chkUSerID + "' and [Shift Pattern]='2' and [Shift Code]='0'", Portalcon.Con);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds, "tblEmployeeWeekShiftMasterwe");
                                    for (int j = 0; j <= ds.Tables[0].Rows.Count - 1; j++)
                                    {

                                        string weekdatedd = "";
                                        weekdatedd = ds.Tables[0].Rows[j]["Start Date"].ToString();

                                        Portalcon.Update_attendanceOFFday_monthly(tblenameAttendence, chkUSerID, From_Date);


                                    }

                                    SqlDataAdapter dah = new SqlDataAdapter("select convert(varchar(11), [Date],113) as Date1,*  from " + tblPayHolidayswe + " where convert(date, [Date],103)  >='" + From_Date + "' and  convert(date, [Date],103)  <='" + To_Date + "' and [Branch Code]='" + branchcode + "'", Portalcon.Con);
                                    DataSet dsh = new DataSet();
                                    dah.Fill(dsh, "tblPayHolidayswe");
                                    for (int j = 0; j <= dsh.Tables[0].Rows.Count - 1; j++)
                                    {

                                        string holidaydate = "";
                                        // holidaydate = dsh.Tables[0].Rows[j]["Date"].ToString();
                                        holidaydate = dsh.Tables[0].Rows[j]["Date1"].ToString();

                                        Portalcon.Update_attendanceHoliday(tblenameAttendence, chkUSerID, holidaydate);

                                    }
                                    try
                                    {
                                        SendSMSForApprovedLeave(chkUSerID.Trim(), From_Date, To_Date, Leave_Type, Uname);
                                    }
                                    catch (Exception)
                                    { }

                                }
                            }

                        }
                        else
                        {
                            drcl.Close();
                        }



                    }
                    else
                    {
                        if (Leave_Type == "AL" || Leave_Type == "VL")
                        {

                            if (Session["CompanyEmail"].ToString() == "vicechancellor@tmu.ac.in")
                            {
                                Portalcon.Update_AttendencewithApprovalforLeavenotinholidayoffday(tblenameAttendence, timein, timeout, status, hourpresent, chkUSerID, From_Date, To_Date, Leave_Period, Leave_Type, Half_Day_type_Code.Trim());
                                Portalcon.DisConnect();
                                if (PostLunchvar == "Yes")
                                {
                                    Portalcon.UpdatePRELunchDatad_FromDate(tblenameAttendence, status, chkUSerID, From_Date, "2", "2");
                                }
                                if (PreLunchvar == "Yes")
                                {
                                    Portalcon.UpdatePRELunchDatad_FromDate(tblenameAttendence, status, chkUSerID, To_Date, "2", "1");
                                }

                                con.Update_tble_Leaveapr_ResolvedHODFinal(System.DateTime.Now.ToString("dd-MM-yyyy"), profilechangedate, Session["uname"].ToString(), Session["uid"].ToString());
                                con.DisConnect();

                                UpdatePayPayEmployeeLeaveEntitled(Leave_Type, chkUSerID, unapprovedleavess, No_Of_Days_Leave_Period);
                                con.DisConnect();

                                UpdateCO(Leave_Type, AutoNo, chkUSerID);
                                try
                                {
                                    SendSMSForApprovedLeave(chkUSerID.Trim(), From_Date, To_Date, Leave_Type, Uname);
                                }
                                catch (Exception)
                                { }
                            }

                            if (Session["CompanyEmail"].ToString() != "vicechancellor@tmu.ac.in")
                            {
                                con.Update_tble_LeaveaRecomdeded(profilechangedate);
                                con.DisConnect();
                            }
                        }
                        else
                        {
                            if (ststatusapprovalss == "Pending")
                            {

                                Portalcon.Update_AttendencewithApprovalforLeavenotinholidayoffday(tblenameAttendence, timein, timeout, status, hourpresent, chkUSerID, From_Date, To_Date, Leave_Period, Leave_Type, Half_Day_type_Code.Trim());
                                Portalcon.DisConnect();
                                if (PostLunchvar == "Yes")
                                {
                                    Portalcon.UpdatePRELunchDatad_FromDate(tblenameAttendence, status, chkUSerID, From_Date, "2", "2");
                                }
                                if (PreLunchvar == "Yes")
                                {
                                    Portalcon.UpdatePRELunchDatad_FromDate(tblenameAttendence, status, chkUSerID, To_Date, "2", "1");
                                }


                                con.Update_tble_Leaveapr_ResolvedHODFinal(System.DateTime.Now.ToString("dd-MM-yyyy"), profilechangedate, Session["uname"].ToString(), Session["uid"].ToString());
                                con.DisConnect();

                                UpdatePayPayEmployeeLeaveEntitled(Leave_Type, chkUSerID, unapprovedleavess, No_Of_Days_Leave_Period);
                                con.DisConnect();

                                UpdateCO(Leave_Type, AutoNo, chkUSerID);
                                try
                                {
                                    SendSMSForApprovedLeave(chkUSerID.Trim(), From_Date, To_Date, Leave_Type, Uname);
                                }
                                catch (Exception)
                                { }
                            }
                        }
                    }

                }
                else
                {
                    dr.Close();
                    Portalcon.DisConnect();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please update Attendance table from Navision, date is not available ');", true);

                }


            }

        }
        else
        {
            drall.Close();
            drall.Dispose();
        }
    }

    public void FindCheckData1(string LeaveType)
    {

        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        tblenameAttendence = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
        tablenameemployeedata = "[" + rccname + "$Employee" + "]";
        tblePayEmployeeNet = "[" + rccname + "$Pay Employee Net" + "]";

        SqlDataReader drall = con.Approved_LeaveData(Session["Company"].ToString(), chkUSerID, profilechangedate);
        drall.Read();
        if (drall.HasRows)
        {


            string monthAppy = ""; string yearApply = "";
            string AutoNo = ""; string Half_Day_type_Code = ""; string PreLunchvar = ""; string PostLunchvar = "";
            string Uname = ""; string From_Date = ""; string To_Date = ""; string Leave_Period = ""; string No_Of_Days_Leave_Period = ""; string Leave_Type = ""; string timein = ""; string timeout = ""; string hourpresent = "00.00"; string status = ""; string userEmailid = ""; string HR_Userid = ""; string HREmailidleav = ""; string HODEmailIDleav = ""; string HRNameleav = ""; string HODNameleav = "";
            Uname = drall["Uname"].ToString();
            From_Date = drall["From_Date"].ToString();
            To_Date = drall["To_Date"].ToString();
            userEmailid = drall["user_Emailid"].ToString();
            HR_Userid = drall["HR_Userid"].ToString();
            AutoNo = drall["AutoNo"].ToString();
            ststatusapprovalss = drall["Status"].ToString();
            Finalstatus = drall["FinalApprovalStatus"].ToString();
            HREmailidleav = drall["HREmailid"].ToString();
            HODEmailIDleav = drall["HODEmailID"].ToString();
            HRNameleav = drall["HRName"].ToString();
            HODNameleav = drall["HODName"].ToString();
            PreLunchvar = drall["PreLunch"].ToString();
            PostLunchvar = drall["PostLunch"].ToString();
            string Leavep = drall["Leave_Period"].ToString();
            if (Leavep == "(Full-Day)")
            {
                Leave_Period = "3";
            }

            if (Leavep == "(Half-Day)")
            {
                Leave_Period = "2";
            }
            No_Of_Days_Leave_Period = drall["No_Of_Days_Leave_Period"].ToString();
            Leave_Type = drall["Leave_Type"].ToString();

            drall.Close();
            drall.Dispose();




            if (Leave_Type == "LWP" && Leavep == "(Full-Day)")
            {
                status = "6";
            }
            if (Leave_Type == "LWP" && Leavep == "(Half-Day)")
            {
                status = "5";
            }
            if (Leave_Type == "Special Leave")
            {
                status = "7";
            }
            else if (status == "")
            {
                status = "4";
            }

            DateTime ApplieddateFrom = DateTime.ParseExact(From_Date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            DateTime Applieddateto = DateTime.ParseExact(To_Date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

            SqlDataReader drSlGNRT = Portalcon.SHow_SalaryProcessingMonth_PayEmployeeNet(tblePayEmployeeNet, ApplieddateFrom.ToString("MM"), ApplieddateFrom.ToString("yyyy"), chkUSerID, Applieddateto.ToString("MM"), Applieddateto.ToString("yyyy"));
            drSlGNRT.Read();
            if (drSlGNRT.HasRows)
            {

                drSlGNRT.Close();
                Portalcon.DisConnect();
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Leave is not approved because salary has been generated please contact payroll department and reject from rejection option');", true);
            }
            else
            {

                drSlGNRT.Close();
                Portalcon.DisConnect();


                SqlDataReader dr = Portalcon.Show_AttendenceDateFromNavisionforLeave(tblenameAttendence, From_Date, To_Date, chkUSerID);
                dr.Read();
                if (dr.HasRows)
                {
                    dr.Close();
                    Portalcon.DisConnect();

                    if (Leave_Type == "CL")
                    {
                        string weekdaycode = ""; string holidaycode = "";
                        string shiftpattern = ""; string shiftday = ""; string compholidayallowed = ""; string branchcode = "";
                        string ccnamedd = Session["Company"].ToString();
                        string rccnamedd = ccname.Replace(".", "_");
                        string tblemployeed = "[" + rccname + "$Employee" + "]";
                        SqlDataReader drcl = Portalcon.Show_weekofforapproval(tblemployeed, chkUSerID);
                        drcl.Read();
                        if (drcl.HasRows)
                        {
                            shiftpattern = drcl["Shift Pattern"].ToString();

                            shiftday = drcl["Weekly Off only Fixed Shift"].ToString();
                            compholidayallowed = drcl["Company Holiday Allowed"].ToString();
                            branchcode = drcl["Global Dimension 1 Code"].ToString();
                            drcl.Close();
                            if (shiftday == "1")
                            {
                                weekdaycode = "Sunday";
                            }
                            if (shiftday == "2")
                            {
                                weekdaycode = "Monday";
                            }
                            if (shiftday == "3")
                            {
                                weekdaycode = "Tuesday";
                            }
                            if (shiftday == "4")
                            {
                                weekdaycode = "Wednesday";
                            }
                            if (shiftday == "5")
                            {
                                weekdaycode = "Thursday";
                            }
                            if (shiftday == "6")
                            {
                                weekdaycode = "Friday";
                            }
                            if (shiftday == "7")
                            {
                                weekdaycode = "Saturday";
                            }


                            if (shiftpattern == "0")
                            {


                                if (compholidayallowed == "0")
                                {
                                    if (Finalstatus == "0")
                                    {
                                        string ccnamse = Session["Company"].ToString();
                                        string rccnasme = ccnamse.Replace(".", "_");
                                        string tblPayHolidays = "[" + rccnasme + "$Pay Holidays" + "]";

                                        holidaycode = weekdaycode;
                                        Portalcon.Update_attendanceforfixedshift(tblenameAttendence, timein, timeout, status, hourpresent, chkUSerID, From_Date, To_Date, Leave_Period, Leave_Type, Half_Day_type_Code.Trim(), weekdaycode, holidaycode);
                                        Portalcon.DisConnect();


                                        SqlDataAdapter da = new SqlDataAdapter("select convert(varchar(11), [Date],113) as Date1 ,*  from " + tblPayHolidays + " where convert(date, [Date],103)  >='" + From_Date + "' and  convert(date, [Date],103)  <='" + To_Date + "' and [Branch Code]='" + branchcode + "' and DATENAME(dw,Date) !='" + weekdaycode + "'", Portalcon.Con);
                                        DataSet ds = new DataSet();
                                        da.Fill(ds, "tbl_EmployeepunchData");
                                        for (int j = 0; j <= ds.Tables[0].Rows.Count - 1; j++)
                                        {

                                            string holidaydate = "";
                                            // holidaydate = ds.Tables[0].Rows[j]["Date"].ToString();
                                            holidaydate = ds.Tables[0].Rows[j]["Date1"].ToString();

                                            Portalcon.Update_attendanceHoliday(tblenameAttendence, chkUSerID, holidaydate);

                                        }


                                        Portalcon.Update_attendanceOFFday(tblenameAttendence, chkUSerID, From_Date, To_Date, weekdaycode);
                                        Portalcon.DisConnect();



                                        if (Session["GlobalDimension1Coded"].ToString() == "TMMC" || Session["uid"].ToString() == "TMU00223" || Session["uid"].ToString() == "TMU04621")
                                        {
                                            SqlConnection Conn = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]);
                                            Conn.Open();
                                            SqlCommand cmd = new SqlCommand();
                                            string sqlq = "update tble_Leave_Approval set FinalApprovalStatus=1 ,Status='Approved' , HOD_Remarks_Date='" + System.DateTime.Now.ToString("dd MMM yyyy") + "', [Rejected Approval]='No',CountStatusHOD='Approved',ApprovedBy='" + Session["uname"].ToString() + "',Approvalbyid='" + Session["uid"].ToString() + "',ApprovalDate='" + System.DateTime.Now.ToString("dd MMM yyyy") + "'  where id ='" + profilechangedate + "'";
                                             
                                            
                                            
                                            
                                            cmd = new SqlCommand(sqlq, Conn);
                                            cmd.ExecuteNonQuery();
                                            Conn.Close();

                                        }
                                        else
                                        {
                                            con.Update_tble_Leaveapr_ResolvedHODFinal1(System.DateTime.Now.ToString("dd-MM-yyyy"), profilechangedate, Session["uname"].ToString(), Session["uid"].ToString());

                                        }
                                        con.DisConnect();





                                        UpdatePayPayEmployeeLeaveEntitled(Leave_Type, chkUSerID, unapprovedleavess, No_Of_Days_Leave_Period);
                                        con.DisConnect();

                                        UpdateCO(Leave_Type, AutoNo, chkUSerID);
                                        try
                                        {
                                            SendSMSForApprovedLeave(chkUSerID.Trim(), From_Date, To_Date, Leave_Type, Uname);
                                        }
                                        catch (Exception)
                                        { }
                                    }
                                }
                                else
                                {
                                    if (Finalstatus == "0")
                                    {

                                        holidaycode = weekdaycode;
                                        Portalcon.Update_attendanceforfixedshift(tblenameAttendence, timein, timeout, status, hourpresent, chkUSerID, From_Date, To_Date, Leave_Period, Leave_Type, Half_Day_type_Code.Trim(), weekdaycode, holidaycode);
                                        Portalcon.DisConnect();
                                        Portalcon.Update_attendanceOFFday(tblenameAttendence, chkUSerID, From_Date, To_Date, weekdaycode);
                                        Portalcon.DisConnect();

                                        con.Update_tble_Leaveapr_ResolvedHODFinal1(System.DateTime.Now.ToString("dd-MM-yyyy"), profilechangedate, Session["uname"].ToString(), Session["uid"].ToString());
                                        con.DisConnect();

                                        UpdatePayPayEmployeeLeaveEntitled(Leave_Type, chkUSerID, unapprovedleavess, No_Of_Days_Leave_Period);
                                        con.DisConnect();

                                        UpdateCO(Leave_Type, AutoNo, chkUSerID);
                                        try
                                        {
                                            SendSMSForApprovedLeave(chkUSerID.Trim(), From_Date, To_Date, Leave_Type, Uname);
                                        }
                                        catch (Exception)
                                        { }

                                    }
                                }
                            }

                            if (shiftpattern == "2")
                            {
                                if (Finalstatus == "0")
                                {

                                    Portalcon.Update_AttendencewithApprovalforLeavenotinholidayoffday(tblenameAttendence, timein, timeout, status, hourpresent, chkUSerID, From_Date, To_Date, Leave_Period, Leave_Type, Half_Day_type_Code.Trim());
                                    Portalcon.DisConnect();

                                    if (Session["GlobalDimension1Coded"].ToString() == "TMMC" || Session["uid"].ToString() == "TMU00223" || Session["uid"].ToString() == "TMU04621")
                                    {
                                        SqlConnection Conn = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]);
                                        Conn.Open();
                                        SqlCommand cmd = new SqlCommand();
                                        string sqlq = "update tble_Leave_Approval set FinalApprovalStatus=1 ,Status='Approved' , HOD_Remarks_Date='" + System.DateTime.Now.ToString("dd MMM yyyy") + "', [Rejected Approval]='No',CountStatusHOD='Approved',ApprovedBy='" + Session["uname"].ToString() + "',Approvalbyid='" + Session["uid"].ToString() + "',ApprovalDate='" + System.DateTime.Now.ToString("dd MMM yyyy") + "'  where id ='" + profilechangedate + "'";




                                        cmd = new SqlCommand(sqlq, Conn);
                                        cmd.ExecuteNonQuery();
                                        Conn.Close();

                                    }
                                    else
                                    {
                                        con.Update_tble_Leaveapr_ResolvedHODFinal1(System.DateTime.Now.ToString("dd-MM-yyyy"), profilechangedate, Session["uname"].ToString(), Session["uid"].ToString());

                                    }


// con.Update_tble_Leaveapr_ResolvedHODFinal(System.DateTime.Now.ToString("dd-MM-yyyy"), profilechangedate, Session["uname"].ToString(), Session["uid"].ToString());
                                    con.DisConnect();

                                    UpdatePayPayEmployeeLeaveEntitled(Leave_Type, chkUSerID, unapprovedleavess, No_Of_Days_Leave_Period);
                                    con.DisConnect();

                                    UpdateCO(Leave_Type, AutoNo, chkUSerID);

                                    string ccnamewe = Session["Company"].ToString();
                                    string rccnamewe = ccnamewe.Replace(".", "_");
                                    string tblPayHolidayswe = "[" + rccnamewe + "$Pay Holidays" + "]";
                                    string tblEmployeeWeekShiftMasterwe = "[" + rccnamewe + "$Employee Week Shift Master" + "]";

                                    SqlDataAdapter da = new SqlDataAdapter("select *  from " + tblEmployeeWeekShiftMasterwe + " where convert(date, [Start Date],103)  >='" + From_Date + "' and  convert(date, [Start Date],103)  <='" + To_Date + "' and [Employee No_]='" + chkUSerID + "' and [Shift Pattern]='2' and [Shift Code]='0'", Portalcon.Con);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds, "tblEmployeeWeekShiftMasterwe");
                                    for (int j = 0; j <= ds.Tables[0].Rows.Count - 1; j++)
                                    {

                                        string weekdatedd = "";
                                        weekdatedd = ds.Tables[0].Rows[j]["Start Date"].ToString();

                                        Portalcon.Update_attendanceOFFday_monthly(tblenameAttendence, chkUSerID, From_Date);


                                    }

                                    SqlDataAdapter dah = new SqlDataAdapter("select convert(varchar(11), [Date],113) as Date1,*  from " + tblPayHolidayswe + " where convert(date, [Date],103)  >='" + From_Date + "' and  convert(date, [Date],103)  <='" + To_Date + "' and [Branch Code]='" + branchcode + "'", Portalcon.Con);
                                    DataSet dsh = new DataSet();
                                    dah.Fill(dsh, "tblPayHolidayswe");
                                    for (int j = 0; j <= dsh.Tables[0].Rows.Count - 1; j++)
                                    {

                                        string holidaydate = "";
                                        // holidaydate = dsh.Tables[0].Rows[j]["Date"].ToString();
                                        holidaydate = dsh.Tables[0].Rows[j]["Date1"].ToString();

                                        Portalcon.Update_attendanceHoliday(tblenameAttendence, chkUSerID, holidaydate);

                                    }
                                    try
                                    {
                                        SendSMSForApprovedLeave(chkUSerID.Trim(), From_Date, To_Date, Leave_Type, Uname);
                                    }
                                    catch (Exception)
                                    { }

                                }
                            }

                        }
                        else
                        {
                            drcl.Close();
                        }



                    }
                    else
                    {
                        if (Leave_Type == "AL" || Leave_Type == "VL")
                        {

                            if (Session["CompanyEmail"].ToString() == "vicechancellor@tmu.ac.in" || Session["GlobalDimension1Coded"].ToString() == "TMMC" || Session["uid"].ToString() == "TMU00223" || Session["uid"].ToString() == "TMU04621")
                            {
                                Portalcon.Update_AttendencewithApprovalforLeavenotinholidayoffday(tblenameAttendence, timein, timeout, status, hourpresent, chkUSerID, From_Date, To_Date, Leave_Period, Leave_Type, Half_Day_type_Code.Trim());
                                Portalcon.DisConnect();
                                if (PostLunchvar == "Yes")
                                {
                                    Portalcon.UpdatePRELunchDatad_FromDate(tblenameAttendence, status, chkUSerID, From_Date, "2", "2");
                                }
                                if (PreLunchvar == "Yes")
                                {
                                    Portalcon.UpdatePRELunchDatad_FromDate(tblenameAttendence, status, chkUSerID, To_Date, "2", "1");
                                }
                                SqlConnection Conn = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]);
                                Conn.Open();
                                SqlCommand cmd = new SqlCommand();
                                string sqlq = "update tble_Leave_Approval set FinalApprovalStatus=1 ,Status='Approved', [Rejected Approval]='No',CountStatusHOD='Approved',ApprovedBy='" + Session["uname"].ToString() + "',Approvalbyid='" + Session["uid"].ToString() + "',ApprovalDate='" + System.DateTime.Now.ToString("dd MMM yyyy") + "'  where id ='" + profilechangedate + "'";
                                cmd = new SqlCommand(sqlq, Conn);
                                cmd.ExecuteNonQuery();
                                Conn.Close();

                                //con.Update_tble_Leaveapr_ResolvedHODFinal(System.DateTime.Now.ToString("dd-MM-yyyy"), profilechangedate, Session["uname"].ToString(), Session["uid"].ToString());
                                con.DisConnect();

                                UpdatePayPayEmployeeLeaveEntitled(Leave_Type, chkUSerID, unapprovedleavess, No_Of_Days_Leave_Period);
                                con.DisConnect();

                                UpdateCO(Leave_Type, AutoNo, chkUSerID);
                                try
                                {
                                    SendSMSForApprovedLeave(chkUSerID.Trim(), From_Date, To_Date, Leave_Type, Uname);
                                }
                                catch (Exception)
                                { }
                            }

                            if (Session["CompanyEmail"].ToString() != "vicechancellor@tmu.ac.in" && Session["GlobalDimension1Coded"].ToString() != "TMMC" && Session["uid"].ToString() != "TMU00223" && Session["uid"].ToString() != "TMU04621")
                            {
                                 // if (Session["GlobalDimension1Coded"].ToString() == "TMMC")
                                //{
                                //    SqlConnection Conn = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]);
                                //    Conn.Open();
                                //    SqlCommand cmd = new SqlCommand();
                                //    string sqlq = "update tble_Leave_Approval set FinalApprovalStatus=1 ,Status='Approved' , HOD_Remarks_Date='" + System.DateTime.Now.ToString("dd MMM yyyy") + "', [Rejected Approval]='No',CountStatusHOD='Approved',ApprovedBy='" + Session["uname"].ToString() + "',Approvalbyid='" + Session["uid"].ToString() + "',ApprovalDate='" + System.DateTime.Now.ToString("dd MMM yyyy") + "'  where id ='" + profilechangedate + "'";
                                //    cmd = new SqlCommand(sqlq, Conn);
                                //    cmd.ExecuteNonQuery();
                                //    Conn.Close();
                                //    UpdatePayPayEmployeeLeaveEntitled(Leave_Type, chkUSerID, unapprovedleavess, No_Of_Days_Leave_Period);

                                //}
                                //else
                                //{
                                    con.Update_tble_LeaveaRecomdeded(profilechangedate);
                                    con.DisConnect();
                                //}
                            }
                        }
                        else
                        {
                            if (Finalstatus == "0")
                            {

                                Portalcon.Update_AttendencewithApprovalforLeavenotinholidayoffday(tblenameAttendence, timein, timeout, status, hourpresent, chkUSerID, From_Date, To_Date, Leave_Period, Leave_Type, Half_Day_type_Code.Trim());
                                Portalcon.DisConnect();
                                if (PostLunchvar == "Yes")
                                {
                                    Portalcon.UpdatePRELunchDatad_FromDate(tblenameAttendence, status, chkUSerID, From_Date, "2", "2");
                                }
                                if (PreLunchvar == "Yes")
                                {
                                    Portalcon.UpdatePRELunchDatad_FromDate(tblenameAttendence, status, chkUSerID, To_Date, "2", "1");
                                }


                                if (Session["GlobalDimension1Coded"].ToString() == "TMMC" || Session["uid"].ToString() == "TMU00223" || Session["uid"].ToString() == "TMU04621")
                                {
                                    SqlConnection Conn = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]);
                                    Conn.Open();
                                    SqlCommand cmd = new SqlCommand();
                                    string sqlq = "update tble_Leave_Approval set FinalApprovalStatus=1 ,Status='Approved' , HOD_Remarks_Date='" + System.DateTime.Now.ToString("dd MMM yyyy") + "', [Rejected Approval]='No',CountStatusHOD='Approved',ApprovedBy='" + Session["uname"].ToString() + "',Approvalbyid='" + Session["uid"].ToString() + "',ApprovalDate='" + System.DateTime.Now.ToString("dd MMM yyyy") + "'  where id ='" + profilechangedate + "'";
                                    cmd = new SqlCommand(sqlq, Conn);
                                    cmd.ExecuteNonQuery();
                                    Conn.Close();


                                }
                                else
                                {
                                    con.Update_tble_Leaveapr_ResolvedHODFinal1(System.DateTime.Now.ToString("dd-MM-yyyy"), profilechangedate, Session["uname"].ToString(), Session["uid"].ToString());
                                }
                                con.DisConnect();

                                UpdatePayPayEmployeeLeaveEntitled(Leave_Type, chkUSerID, unapprovedleavess, No_Of_Days_Leave_Period);
                                con.DisConnect();

                                UpdateCO(Leave_Type, AutoNo, chkUSerID);
                                try
                                {
                                    SendSMSForApprovedLeave(chkUSerID.Trim(), From_Date, To_Date, Leave_Type, Uname);
                                }
                                catch (Exception)
                                { }
                            }
                        }
                    }

                }
                else
                {
                    dr.Close();
                    Portalcon.DisConnect();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please update Attendance table from Navision, date is not available ');", true);

                }


            }

        }
        else
        {
            drall.Close();
            drall.Dispose();
        }
    }


    public void SMS(String MobileNo, string Msg)
    {
        //  Website: http://www.universalsmsadvertising.com/     //  Username: 9837016352   //  Password: 9837016352 
        MobileNo = "91" + MobileNo;
        // MobileNo = "91" + 9015762885;
        string url = "http://www.universalsmsadvertising.com/universalsmsapi.php?user_name=9837016352&user_password=9837016352&mobile=" + MobileNo + "&sender_id=TMUniv&type=F&text=" + Msg + "";

        // string url = "http://www.universalsmsadvertising.com/universalsmsapi.php?user_name=myuserid&user_password=mypassword&mobile=919834567120&sender_id=SWEET&type=F&text= " + Msg + "";



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


    public void UpdateCO(string Leave_Type, string AutoNo, string userid)
    {

        if (Leave_Type == "CO")
        {
            con.Update_tbl_Co_Leave_DetailsStatus("Approved", AutoNo, Session["Company"].ToString());
            con.DisConnect();


            SqlDataAdapter da = new SqlDataAdapter("select *,(select From_Date from [tble_Leave_Approval] where AutoNo=tbl_Co_Leave_Details.[Based on working]) as d from tbl_Co_Leave_Details where Userid='" + userid + "' and Company='" + Session["Company"].ToString() + "' and [Based on working]='" + AutoNo + "'", con.Con);
            DataSet ds = new DataSet();
            da.Fill(ds, "tbl_Co_Leave_Details");
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                string lblAttendacedate4 = "", lblAttendacedate40 = "";

                lblAttendacedate4 = ds.Tables[0].Rows[i]["Co_Date"].ToString();
                DateTime lblAttendacedate5 = Convert.ToDateTime(lblAttendacedate4);
                lblAttendacedate4 = lblAttendacedate5.ToString("yyyy-MM-dd");
                Portalcon.updateCoLeaveStatusinPaydailyAttendance(tblenameAttendence, lblAttendacedate4, chkUSerID, "Approved");
                Portalcon.DisConnect();
                lblAttendacedate40 = ds.Tables[0].Rows[i]["d"].ToString();
                DateTime lblAttendacedate50 = Convert.ToDateTime(lblAttendacedate40);
                lblAttendacedate40 = lblAttendacedate50.ToString("yyyy-MM-dd");
                SqlConnection Conn = new SqlConnection(ConfigurationManager.AppSettings["str"]);
                Conn.Open();
                SqlCommand cmd = new SqlCommand();
                string sqlq = "update [TMU$Pay Employee Leave Credited] set Remarks='CO VALIDITY EXPIRED AFTER 90 DAYS -  " + lblAttendacedate40 + "' where [Leave Code]='CO' and [Employee Code]='" + chkUSerID + "' and Date='" + lblAttendacedate4 + "'";




                cmd = new SqlCommand(sqlq, Conn);
                cmd.ExecuteNonQuery();
                Conn.Close();
               





            }

            // SqlDataReader drautono = con.ApprovedStatusofPaydeialyAttendance(Session["Company"].ToString(), userid, AutoNo);
            //while (drautono.Read)
            //{

            //    string atte = drautono["Co_Date"].ToString();
            //    drautono.Close();
            //    con.DisConnect();
            //    Portalcon.updateCoLeaveStatusinPaydailyAttendance(tblenameAttendence, Convert.ToDateTime(atte), chkUSerID, "Approved");
            //    Portalcon.DisConnect();
            //    con.Connect();
            //    drautono.Read();
            //}

        }
    }





    double unapprovedleavessRejected;
    public void UpdatePayPayEmployeeLeaveEntitledRejected(string Leave_Code, string UserID, string To_No_OfLeave)
    {
        string ccnameUN = Session["Company"].ToString();
        string rccname = ccnameUN.Replace(".", "_");
        string tble_Pay_Employee_Leave_Entitled = "[" + rccname + "$Pay Employee Leave Entitled" + "]";

        SqlDataReader dr = Portalcon.ShowLeaveBalance_DetailwithOption(tble_Pay_Employee_Leave_Entitled, UserID, Leave_Code);
        dr.Read();
        if (dr.HasRows)
        {
            //string lb = dr["Leave Balance"].ToString();
            double To_No_OfLeave1 = Convert.ToDouble(To_No_OfLeave);
            string unapproved = dr["Unapproved Leave"].ToString();
            double UnAP1 = Convert.ToDouble(unapproved);
            //double lbc = Convert.ToDouble(lb);
            unapprovedleavessRejected = Convert.ToDouble(UnAP1 - To_No_OfLeave1);






            dr.Close();
            Portalcon.DisConnect();


            Portalcon.Update_Pay_Employee_Leave_EntitledLeave_BalanceApply(unapprovedleavessRejected, tble_Pay_Employee_Leave_Entitled, UserID, Leave_Code);
            Portalcon.DisConnect();

        }
        else
        {
            dr.Close();
            Portalcon.DisConnect();
        }
    }


    public void FindCheckDataRejected(string LeaveType)
    {

        string tble_Pay_Employee_Leave_Entitledlb = "";
        //showoffLeavesetup();
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        tblenameAttendence = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
        string ccnamelb = Session["Company"].ToString();
        string rccnamelb = ccnamelb.Replace(".", "_");
        tble_Pay_Employee_Leave_Entitledlb = "[" + rccnamelb + "$Pay Employee Leave Entitled" + "]";

        SqlDataReader drall = con.rejected_LeaveApprovaldata1(Session["Company"].ToString(), chkUSerIDRejected, profilechangedaterejected);
        drall.Read();
        if (drall.HasRows)
        {



            string AutoNo = ""; string Uname = ""; string From_Date = ""; string To_Date = ""; string Leave_Period = ""; string No_Of_Days_Leave_Period = ""; string Leave_Type = ""; string lbBalance_Leave_entiled = ""; string emailId = ""; string HREmailidrej = ""; string HODEmailIDrej = ""; string HRNamerej = ""; string HODNamerej = "";
            Uname = drall["Uname"].ToString();
            From_Date = drall["From_Date"].ToString();
            AutoNo = drall["AutoNo"].ToString();
            To_Date = drall["To_Date"].ToString();
            emailId = drall["user_Emailid"].ToString();
            Leave_Period = drall["Leave_Period"].ToString();
            No_Of_Days_Leave_Period = drall["No_Of_Days_Leave_Period"].ToString();
            Leave_Type = drall["Leave_Type"].ToString();

            HREmailidrej = drall["HREmailid"].ToString();
            HODEmailIDrej = drall["HODEmailID"].ToString();
            HRNamerej = drall["HRName"].ToString();
            HODNamerej = drall["HODName"].ToString();

            drall.Close();
            con.DisConnect();
            if (Blankapr == "1")
            {

                SqlDataReader dr = Portalcon.ShowLeaveBalance_Detail_LBType(tble_Pay_Employee_Leave_Entitledlb, chkUSerIDRejected, Leave_Type);
                dr.Read();
                if (dr.HasRows)
                {
                    lbBalance_Leave_entiled = dr["Leave Balance"].ToString();
                    double prbal = Convert.ToDouble(No_Of_Days_Leave_Period);
                    double curbal = Convert.ToDouble(lbBalance_Leave_entiled);
                    double curprbal = prbal + curbal;

                    dr.Close();
                    Portalcon.DisConnect();
                    con.Update_tble_LeaveApprovalStatus_RejectedbyUser(txtRemarksRejected.Text, "Yes", System.DateTime.Now.ToString("dd/MM/yyyy"), profilechangedaterejected);
                    con.DisConnect();
                    //Portalcon.Update_Pay_Employee_Leave_EntitledLeave_Balance(curprbal, tble_Pay_Employee_Leave_Entitledlb, chkUSerIDRejected, Leave_Type);
                    //Portalcon.DisConnect();

                    UpdatePayPayEmployeeLeaveEntitledRejected(Leave_Type, chkUSerIDRejected, No_Of_Days_Leave_Period);
                    con.DisConnect();
                    UpdateCoDetailsrejected(Leave_Type, AutoNo);

                    try
                    {
                        SendSMSForApprovedLeaveRejected(chkUSerIDRejected.Trim(), From_Date, To_Date, Leave_Type, Uname);
                    }
                    catch (Exception)
                    { }

                    //subject1 = "Your Leave  is Rejected";

                    //Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}", Environment.NewLine, "" + Uname + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Your Leave is rejected of From Date : " + From_Date + " and To Date : " + To_Date + "", Environment.NewLine, "Thanking you,", Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                    //ShowMailDataTo(emailId);

                    //if (EmailHOD == "True")
                    //{
                    //    subject1 = "Leave  is Rejected of  " + Uname;

                    //    Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}", Environment.NewLine, "" + HODNamerej + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Leave is rejected of From Date : " + From_Date + " and To Date : " + To_Date + "", Environment.NewLine, "Thanking you,", Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                    //    ShowMailDataTo(HODEmailIDrej);
                    //}

                    //if (EmailHR == "True")
                    //{
                    //    subject1 = "Leave  is Rejected of  " + Uname;
                    //    string hrm3 = "HR Manager";
                    //    Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}", Environment.NewLine, "" + hrm3 + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Leave is rejected of From Date : " + From_Date + " and To Date : " + To_Date + "", Environment.NewLine, "Thanking you,", Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                    //    ShowMailDataTo(HREmailidrej);
                    //}
                }
                dr.Close();
                con.DisConnect();

            }
            if (PriorityHODapr == "1")
            {
                if (Session["UserType"].ToString() == "2")
                {

                    SqlDataReader dr = Portalcon.ShowLeaveBalance_Detail_LBType(tble_Pay_Employee_Leave_Entitledlb, chkUSerIDRejected, Leave_Type);
                    dr.Read();
                    if (dr.HasRows)
                    {
                        lbBalance_Leave_entiled = dr["Leave Balance"].ToString();
                        double prbal = Convert.ToDouble(No_Of_Days_Leave_Period);
                        double curbal = Convert.ToDouble(lbBalance_Leave_entiled);
                        double curprbal = prbal + curbal;

                        dr.Close();
                        Portalcon.DisConnect();
                        con.Update_tble_LeaveApprovalStatus_RejectedByHR(txtRemarksRejected.Text, "Yes", System.DateTime.Now.ToString("dd/MM/yyyy"), profilechangedaterejected);
                        con.DisConnect();
                        //Portalcon.Update_Pay_Employee_Leave_EntitledLeave_Balance(curprbal, tble_Pay_Employee_Leave_Entitledlb, chkUSerIDRejected, Leave_Type);
                        //Portalcon.DisConnect();

                        UpdatePayPayEmployeeLeaveEntitledRejected(Leave_Type, chkUSerIDRejected, No_Of_Days_Leave_Period);
                        con.DisConnect();

                        UpdateCoDetailsrejected(Leave_Type, AutoNo);

                        try
                        {
                            SendSMSForApprovedLeaveRejected(chkUSerIDRejected.Trim(), From_Date, To_Date, Leave_Type, Uname);
                        }
                        catch (Exception)
                        { }
                        //subject1 = "Your Leave  is Rejected";

                        //Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}", Environment.NewLine, "" + Uname + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Your Leave is rejected of From Date : " + From_Date + " and To Date : " + To_Date + "", Environment.NewLine, "Thanking you,", Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                        //ShowMailDataTo(emailId);


                        //if (EmailHR == "True")
                        //{
                        //    subject1 = "Leave  is Rejected by HOD of " + Uname;
                        //    string hrt = "HR Manager";
                        //    Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}", Environment.NewLine, "" + hrt + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Leave is rejected of From Date : " + From_Date + " and To Date : " + To_Date + "", Environment.NewLine, "Thanking you,", Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                        //    ShowMailDataTo(HREmailidrej);


                        //}
                    }
                    dr.Close();
                    con.DisConnect();
                }
                else
                {
                    if (Leave_Type == "LWP")
                    {
                        con.Update_tble_LeaveApprovalStatus_Rejected(txtRemarksRejected.Text, "Yes", System.DateTime.Now.ToString("dd/MM/yyyy"), profilechangedaterejected, Session["uname"].ToString(), Session["uid"].ToString());
                        con.DisConnect();
                        try
                        {
                            SendSMSForApprovedLeaveRejected(chkUSerIDRejected.Trim(), From_Date, To_Date, Leave_Type, Uname);
                        }
                        catch (Exception)
                        { }

                    }
                    SqlDataReader dr = Portalcon.ShowLeaveBalance_Detail_LBType(tble_Pay_Employee_Leave_Entitledlb, chkUSerIDRejected, Leave_Type);
                    dr.Read();
                    if (dr.HasRows)
                    {
                        lbBalance_Leave_entiled = dr["Leave Balance"].ToString();
                        double prbal = Convert.ToDouble(No_Of_Days_Leave_Period);
                        double curbal = Convert.ToDouble(lbBalance_Leave_entiled);
                        double curprbal = prbal + curbal;

                        dr.Close();
                        Portalcon.DisConnect();

                        con.Update_tble_LeaveApprovalStatus_Rejected(txtRemarksRejected.Text, "Yes", System.DateTime.Now.ToString("dd/MM/yyyy"), profilechangedaterejected, Session["uname"].ToString(), Session["uid"].ToString());
                        con.DisConnect();
                        //Portalcon.Update_Pay_Employee_Leave_EntitledLeave_Balance(curprbal, tble_Pay_Employee_Leave_Entitledlb, chkUSerIDRejected, Leave_Type);
                        //Portalcon.DisConnect();
                        UpdatePayPayEmployeeLeaveEntitledRejected(Leave_Type, chkUSerIDRejected, No_Of_Days_Leave_Period);
                        con.DisConnect();
                        UpdateCoDetailsrejected(Leave_Type, AutoNo);

                        try
                        {
                            SendSMSForApprovedLeaveRejected(chkUSerIDRejected.Trim(), From_Date, To_Date, Leave_Type, Uname);
                        }
                        catch (Exception)
                        { }
                        //subject1 = "Your Leave  is Rejected";

                        //Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}", Environment.NewLine, "" + Uname + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Your Leave is rejected of From Date : " + From_Date + " and To Date : " + To_Date + "", Environment.NewLine, "Thanking you,", Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                        //ShowMailDataTo(emailId);

                        //if (EmailHR == "True")
                        //{
                        //    subject1 = "Leave  is Rejected by HOD of " + Uname;
                        //    string hry = "HR Manager";
                        //    Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}", Environment.NewLine, "" + hry + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Leave is rejected of From Date : " + From_Date + " and To Date : " + To_Date + "", Environment.NewLine, "Thanking you,", Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());
                        //    ShowMailDataTo(HREmailidrej);
                        //}
                    }
                    dr.Close();
                    con.DisConnect();
                }
            }

            if (PriorityHRapr == "1")
            {
                if (Session["UserType"].ToString() == "2")
                {
                    SqlDataReader dr = Portalcon.ShowLeaveBalance_Detail_LBType(tble_Pay_Employee_Leave_Entitledlb, chkUSerIDRejected, Leave_Type);
                    dr.Read();
                    if (dr.HasRows)
                    {
                        lbBalance_Leave_entiled = dr["Leave Balance"].ToString();
                        double prbal = Convert.ToDouble(No_Of_Days_Leave_Period);
                        double curbal = Convert.ToDouble(lbBalance_Leave_entiled);
                        double curprbal = prbal + curbal;

                        dr.Close();
                        Portalcon.DisConnect();


                        con.Update_tble_LeaveApprovalStatus_RejectedByHR(txtRemarksRejected.Text, "Yes", System.DateTime.Now.ToString("dd/MM/yyyy"), profilechangedaterejected);
                        con.DisConnect();
                        //Portalcon.Update_Pay_Employee_Leave_EntitledLeave_Balance(curprbal, tble_Pay_Employee_Leave_Entitledlb, chkUSerIDRejected, Leave_Type);
                        //Portalcon.DisConnect();
                        UpdatePayPayEmployeeLeaveEntitledRejected(Leave_Type, chkUSerIDRejected, No_Of_Days_Leave_Period);
                        con.DisConnect();
                        UpdateCoDetailsrejected(Leave_Type, AutoNo);

                        try
                        {
                            SendSMSForApprovedLeaveRejected(chkUSerIDRejected.Trim(), From_Date, To_Date, Leave_Type, Uname);
                        }
                        catch (Exception)
                        { }
                        //subject1 = "Your Leave  is Rejected";

                        //Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}", Environment.NewLine, "" + Uname + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Your Leave is rejected of From Date : " + From_Date + " and To Date : " + To_Date + "", Environment.NewLine, "Thanking you,", Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                        //ShowMailDataTo(emailId);

                        //if (EmailHOD == "True")
                        //{
                        //    subject1 = "Leave is Rejected by HR of " + Uname;

                        //    Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}", Environment.NewLine, "" + HODNamerej + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Leave is rejected of From Date : " + From_Date + " and To Date : " + To_Date + "", Environment.NewLine, "Thanking you,", Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                        //    ShowMailDataTo(emailId);

                        //}
                    }
                    dr.Close();
                    con.DisConnect();
                }
                else
                {
                    SqlDataReader dr = Portalcon.ShowLeaveBalance_Detail_LBType(tble_Pay_Employee_Leave_Entitledlb, chkUSerIDRejected, Leave_Type);
                    dr.Read();
                    if (dr.HasRows)
                    {
                        lbBalance_Leave_entiled = dr["Leave Balance"].ToString();
                        double prbal = Convert.ToDouble(No_Of_Days_Leave_Period);
                        double curbal = Convert.ToDouble(lbBalance_Leave_entiled);
                        double curprbal = prbal + curbal;

                        dr.Close();
                        Portalcon.DisConnect();

                        con.Update_tble_LeaveApprovalStatus_Rejected(txtRemarksRejected.Text, "Yes", System.DateTime.Now.ToString("dd/MM/yyyy"), profilechangedaterejected, Session["uname"].ToString(), Session["uid"].ToString());
                        con.DisConnect();
                        //Portalcon.Update_Pay_Employee_Leave_EntitledLeave_Balance(curprbal, tble_Pay_Employee_Leave_Entitledlb, chkUSerIDRejected, Leave_Type);
                        //Portalcon.DisConnect();
                        UpdatePayPayEmployeeLeaveEntitledRejected(Leave_Type, chkUSerIDRejected, No_Of_Days_Leave_Period);
                        con.DisConnect();
                        UpdateCoDetailsrejected(Leave_Type, AutoNo);
                        try
                        {
                            SendSMSForApprovedLeaveRejected(chkUSerIDRejected.Trim(), From_Date, To_Date, Leave_Type, Uname);
                        }
                        catch (Exception)
                        { }
                        //subject1 = "Your Leave  is Rejected";

                        //Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}", Environment.NewLine, "" + Uname + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Your Leave is rejected of From Date : " + From_Date + " and To Date : " + To_Date + "", Environment.NewLine, "Thanking you,", Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                        //ShowMailDataTo(emailId);

                        //if (EmailHOD == "True")
                        //{
                        //    subject1 = "Leave  is Rejected by HR " + Uname;

                        //    Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}", Environment.NewLine, "" + Uname + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Leave is rejected of From Date : " + From_Date + " and To Date : " + To_Date + "", Environment.NewLine, "Thanking you,", Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                        //    ShowMailDataTo(emailId);

                        //}
                    }
                    dr.Close();
                    con.DisConnect();

                }
            }


        }
        drall.Close();
        drall.Dispose();
    }

    public void UpdateCoDetailsrejected(string Leave_Type, string AutoNo)
    {
        if (Leave_Type == "CO")
        {
            con.Update_tbl_Co_Leave_DetailsStatus("Pending", AutoNo, Session["Company"].ToString());
            con.DisConnect();
        }

    }
    string mailfrom = ""; string subject1 = ""; string Body1 = ""; string smtpfromportal = ""; int portNo1; string Pass_From = ""; string Mail_Sending_Option = ""; string Leave_Applymail = ""; string CCMail = "";
    public void ShowMailDataTo(string mailTo1)
    {

        SqlDataReader dr = con.Show_tble_MailSetup(Session["Company"].ToString(), "Leave");
        dr.Read();
        if (dr.HasRows)
        {
            mailfrom = dr["from_Email"].ToString();
            smtpfromportal = dr["smtp"].ToString();
            Pass_From = dr["Password_From"].ToString();
            CCMail = dr["CCMail"].ToString();
            string portNo = dr["Port_No"].ToString();
            portNo1 = Convert.ToInt32(portNo);
            Mail_Sending_Option = dr["Mail_Sending_Option"].ToString();
            Leave_Applymail = dr["Leave_Approval"].ToString();

        }

        dr.Close();
        con.DisConnect();
        if (Mail_Sending_Option == "1" && Leave_Applymail == "True")
        {
            SendMail(mailTo1);
        }

    }


    public void SendMail(string MailTo)
    {


        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
        if (mailfrom == "" && MailTo == "")
        { }

        else
        {
            if (mailfrom == "")
            {
            }

            else
            {

                msg.From = new MailAddress(Session["CompanyEmail"].ToString().Trim(), "  " + Session["Fulname"].ToString());
                if (MailTo == "")
                { }
                else
                {

                    string[] multi = MailTo.Split(',');
                    foreach (string multiTo in multi)
                    {
                        msg.To.Add(multiTo);
                    }
                }
                if (CCMail == "")
                { }
                else
                {
                    string[] ccmulti = CCMail.Split(',');
                    foreach (string ccm in ccmulti)
                    {
                        msg.CC.Add(ccm);
                    }
                }
                msg.Subject = subject1;
                msg.Body = Body1;
                SmtpClient smtp = new SmtpClient();

                smtp.Port = portNo1;
                smtp.Host = smtpfromportal;
                smtp.EnableSsl = true;
                NetworkCredential credential = new NetworkCredential(mailfrom, Pass_From);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = credential;

                try
                {
                    smtp.Send(msg);
                    msg.Dispose();

                }
                catch (Exception)
                {
                    msg.Dispose();
                }
            }
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {

        foreach (GridViewRow row in grdViewApproval.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {

                CheckBox chkRow = (row.Cells[0].FindControl("chkMark") as CheckBox);
                Label lblProfilechangedate1 = (row.Cells[0].FindControl("lblProfilechangedate") as Label);
                Label lblLeaveTypegrid = (row.Cells[0].FindControl("lblLeaveTypegrid") as Label);

                if (chkRow.Checked == true)
                {
                    //DateTime lblProfilechangedate11 = DateTime.ParseExact(lblProfilechangedate1.Text, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    //profilechangedate = lblProfilechangedate11.ToString("yyyy-MM-dd");
                    profilechangedate = lblProfilechangedate1.Text;
                    chkUSerID = chkRow.Text;
                    SqlDataAdapter da = new SqlDataAdapter("select isnull([FinalApprovalId],'') as 'FinalApprovalId',isnull(FinalApprovalStatus,'') as 'FinalApprovalStatus' from tble_Leave_Approval where id='" + profilechangedate + "'", con.Con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows[0]["FinalApprovalId"] == "" || dt.Rows[0]["FinalApprovalId"].ToString() == Session["uid"].ToString())
                    {
                        if (dt.Rows[0]["FinalApprovalId"].ToString() == Session["uid"].ToString())
                        {
                            FindCheckData1(lblLeaveTypegrid.Text);
                        }
                        else
                        {
                            FindCheckData(lblLeaveTypegrid.Text);
                        }
                    }
                    else
                    {
                        con.Update_tble_Leaveapr_ResolvedHODFinal(System.DateTime.Now.ToString("dd-MM-yyyy"), profilechangedate, Session["uname"].ToString(), Session["uid"].ToString());
                        con.DisConnect();

                    }

                }
            }
        }
        LeaveApproval_Search();



    }
    string profilechangedaterejected = "";
    string chkUSerIDRejected = ""; string lblnoofchangeReject1 = "";

    protected void btnreject_Click(object sender, EventArgs e)
    {

        pnlrejectedDetail.Visible = true;
        ModalPopupExtender1.Show();


    }
    protected void btnRejectProfile1_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grdViewApproval.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkMark") as CheckBox);
                Label lblProfilechangedate1 = (row.Cells[0].FindControl("lblProfilechangedate") as Label);
                Label lblLeaveTypegrid1 = (row.Cells[0].FindControl("lblLeaveTypegrid") as Label);

                if (chkRow.Checked == true)
                {

                    profilechangedaterejected = lblProfilechangedate1.Text;
                    chkUSerIDRejected = chkRow.Text;

                    FindCheckDataRejected(lblLeaveTypegrid1.Text);


                }
            }
        }
        LeaveApproval_Search();

    }

    public void LeaveRejected_Search()
    {
        if (rdProfileRectedDatewise.Checked == true)
        {

            if (PriorityHODapr == "1")
            {
                if (Session["CompanyEmail"].ToString() == "vicechancellor@tmu.ac.in")
                {

                    SqlDataReader odr = con.Leave_Approval_Reject_withdateVC(txtRejectedFromDate.Text.Trim(), txtRejectedToDate.Text.Trim(), Session["Company"].ToString(), Session["uid"].ToString());
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdRejected.DataSource = Dt;
                    grdRejected.DataBind();
                    odr.Close();
                    con.DisConnect();

                }

                else
                {


                    SqlDataReader odr = con.Leave_Approval_Reject_withdate(Session["uid"].ToString(), txtRejectedFromDate.Text.Trim(), txtRejectedToDate.Text.Trim(), Session["Company"].ToString());
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdRejected.DataSource = Dt;
                    grdRejected.DataBind();
                    odr.Close();
                    con.DisConnect();
                }

            }


        }

        if (rdProfileRectedEMPID.Checked == true)
        {

            if (PriorityHODapr == "1")
            {
                if (Session["CompanyEmail"].ToString() == "vicechancellor@tmu.ac.in")
                {
                    SqlDataReader odr = con.Leave_Approval_Rejected_withUserIDVC(Session["Company"].ToString(), Session["uid"].ToString(), txtRejectedSearch.Text.Trim());
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdRejected.DataSource = Dt;
                    grdRejected.DataBind();
                    odr.Close();
                    con.DisConnect();

                }
                else
                {
                    SqlDataReader odr = con.Leave_Approval_Rejected_withUserID(Session["uid"].ToString(), Session["Company"].ToString(), txtRejectedSearch.Text.Trim());
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdRejected.DataSource = Dt;
                    grdRejected.DataBind();
                    odr.Close();
                    con.DisConnect();

                }
            }


        }

        if (rdrdProfileRectedName.Checked == true)
        {

            if (PriorityHODapr == "1")
            {
                if (Session["CompanyEmail"].ToString() == "vicechancellor@tmu.ac.in")
                {

                    SqlDataReader odr = con.Leave_Approval_rejected_withUserNameVC(Session["Company"].ToString(), Session["uid"].ToString(), txtRejectedSearch.Text.Trim());
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdRejected.DataSource = Dt;
                    grdRejected.DataBind();
                    odr.Close();
                    con.DisConnect();
                }
                else
                {
                    SqlDataReader odr = con.Leave_Approval_rejected_withUserName(Session["uid"].ToString(), Session["Company"].ToString(), txtRejectedSearch.Text.Trim());
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdRejected.DataSource = Dt;
                    grdRejected.DataBind();
                    odr.Close();
                    con.DisConnect();
                }
            }





        }
        if (rdAllReject.Checked == true)
        {


            if (PriorityHODapr == "1")
            {
                SqlDataReader odr = con.Leave_Approval_rejected_withUserNameAllRejectVC(Session["uid"].ToString(), Session["Company"].ToString());
                DataTable Dt = new DataTable();
                Dt.Load(odr);
                grdRejected.DataSource = Dt;
                grdRejected.DataBind();
                odr.Close();
                con.DisConnect();

            }
            else
            {
                SqlDataReader odr = con.Leave_Approval_rejected_withUserNameAllReject(Session["uid"].ToString(), Session["Company"].ToString());
                DataTable Dt = new DataTable();
                Dt.Load(odr);
                grdRejected.DataSource = Dt;
                grdRejected.DataBind();
                odr.Close();
                con.DisConnect();
            }
        }



    }




    public void LeaveResolved_Search()
    {
        if (rdApprovedDatewise.Checked == true)
        {

            if (PriorityHODapr == "1")
            {

                if (Session["CompanyEmail"].ToString() == "vicechancellor@tmu.ac.in")
                {
                    SqlDataReader odr = con.Leave_Approval_Resolved_withdateVC(Session["uid"].ToString(), txtApprovedFromDate.Text, txtApprovedToDate.Text, Session["Company"].ToString());
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdResolved.DataSource = Dt;
                    grdResolved.DataBind();
                    odr.Close();
                    con.DisConnect();

                }
                else
                {

                    SqlDataReader odr = con.Leave_Approval_Resolved_withdate(Session["uid"].ToString(), txtApprovedFromDate.Text, txtApprovedToDate.Text, Session["Company"].ToString());
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdResolved.DataSource = Dt;
                    grdResolved.DataBind();
                    odr.Close();
                    con.DisConnect();
                }
            }

        }

        if (rdApprovedEmpid.Checked == true)
        {

            if (PriorityHODapr == "1")
            {

                if (Session["CompanyEmail"].ToString() == "vicechancellor@tmu.ac.in")
                {
                    SqlDataReader odr = con.Leave_Approval_Resolved_withUserIDVC(Session["Company"].ToString(), Session["uid"].ToString(), txtResolvedSearch.Text.Trim());
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdResolved.DataSource = Dt;
                    grdResolved.DataBind();
                    odr.Close();
                    con.DisConnect();

                }
                else
                {

                    SqlDataReader odr = con.Leave_Approval_Resolved_withUserID(Session["uid"].ToString(), Session["Company"].ToString(), txtResolvedSearch.Text);
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdResolved.DataSource = Dt;
                    grdResolved.DataBind();
                    odr.Close();
                    con.DisConnect();

                }
            }

        }

        if (rdApprovedEMPName.Checked == true)
        {

            if (PriorityHODapr == "1")
            {

                if (Session["CompanyEmail"].ToString() == "vicechancellor@tmu.ac.in")
                {

                    SqlDataReader odr = con.Leave_Approval_Resolved_withUserNameVC(Session["Company"].ToString(), Session["uid"].ToString(), txtResolvedSearch.Text.Trim());
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdResolved.DataSource = Dt;
                    grdResolved.DataBind();
                    odr.Close();
                    con.DisConnect();
                }
                else
                {
                    SqlDataReader odr = con.Leave_Approval_Resolved_withUserName(Session["uid"].ToString(), Session["Company"].ToString(), txtResolvedSearch.Text.Trim());
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdResolved.DataSource = Dt;
                    grdResolved.DataBind();
                    odr.Close();
                    con.DisConnect();

                }



            }

        }
        if (rdAllApprove.Checked == true)
        {

            if (PriorityHODapr == "1")
            {

                if (Session["CompanyEmail"].ToString() == "vicechancellor@tmu.ac.in")
                {

                    SqlDataReader odr = con.Leave_Approval_Resolved_withUserNameAllVC(Session["Company"].ToString(), Session["uid"].ToString());
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdResolved.DataSource = Dt;
                    grdResolved.DataBind();
                    odr.Close();
                    con.DisConnect();

                }
                else
                {

                    SqlDataAdapter da = new SqlDataAdapter("select count(*) as 'Final' from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Pay Leave] where ([SA for Non-Teach Staff]='" + Session["uid"].ToString() + "' or [SA for Teaching Staff]='" + Session["uid"].ToString() + "')and [Leave Code] collate Latin1_General_100_CS_AS in (select Leave_Type from tble_Leave_Approval where  (HODUserid='" + Session["uid"].ToString() + "' or HODUserid1='" + Session["uid"].ToString() + "' ) and Company_Name='TMU' and (Status='Approved' )  and [Rejected Approval]='No'  )", con.Con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (Convert.ToInt32(dt.Rows[0]["Final"]) > 0)
                    {
                        SqlDataReader odr = con.Leave_Approval_Resolved_withUserNameAll1(Session["uid"].ToString(), Session["Company"].ToString(), txtResolvedSearch.Text);
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdResolved.DataSource = Dt;
                        grdResolved.DataBind();
                        odr.Close();
                        con.DisConnect();
                    }
                    else
                    {


                        SqlDataReader odr = con.Leave_Approval_Resolved_withUserNameAll(Session["uid"].ToString(), Session["Company"].ToString(), txtResolvedSearch.Text);
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdResolved.DataSource = Dt;
                        grdResolved.DataBind();
                        odr.Close();
                        con.DisConnect();
                    }

                }
            }

        }


    }

    protected void btnrejectedsearch_Click(object sender, EventArgs e)
    {
        grdRejected.Visible = true;
        LeaveRejected_Search();
    }
    protected void btnApprovedSearch_Click(object sender, EventArgs e)
    {
        grdResolved.Visible = true;
        LeaveResolved_Search();
    }
    protected void grdViewApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdViewApproval.PageIndex = e.NewPageIndex;
        LeaveApproval_Search();
    }
    protected void grdResolved_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdResolved.PageIndex = e.NewPageIndex;
        LeaveResolved_Search();
    }
    protected void grdRejected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdRejected.PageIndex = e.NewPageIndex;
        LeaveRejected_Search();
    }

    protected void CHKAllPending_CheckedChanged(object sender, EventArgs e)
    {

        pnlEmployeeidName.Visible = false;
        pnlDate.Visible = false;
        btnSearch.Visible = false;
        txtSearchName.Text = "";
        txtfromDate.Text = "";
        txtTodate.Text = "";

        pnlrejectedDetail.Visible = false;
        grdViewApproval.Visible = true;
        LeaveApproval_Search();

    }
    protected void rdAllReject_CheckedChanged(object sender, EventArgs e)
    {
        pnlLeaveRejectedEMPID.Visible = false;
        btnrejectedsearch.Visible = false;
        pnlLeaveRejectedDatewise.Visible = false;
        pnlrejectedDetail.Visible = true;
        ModalPopupExtender1.Hide();
        txtRejectedSearch.Text = "";
        txtRejectedFromDate.Text = "";
        txtRejectedToDate.Text = "";
        grdRejected.Visible = true;
        LeaveRejected_Search();

    }
    protected void rdAllApprove_CheckedChanged(object sender, EventArgs e)
    {
        pnlLeaveApprovedDetailEmpid.Visible = false;
        pnlLeaveApprovedDetailDatewise.Visible = false;
        btnApprovedSearch.Visible = false;
        pnlrejectedDetail.Visible = false;
        txtResolvedSearch.Text = "";
        txtApprovedFromDate.Text = "";
        txtApprovedToDate.Text = "";
        ModalPopupExtender1.Hide();

        grdResolved.Visible = true;
        LeaveResolved_Search();
    }

    protected void btnchangestatus_Command(object sender, CommandEventArgs e)
    {

        Session["chgsUserid"] = e.CommandArgument.ToString();
        //     foreach (GridViewRow row in grdViewApproval.Rows)
        //     {
        //         if (row.RowType == DataControlRowType.DataRow)
        //         {

        //             Label lblProfilechangedate1 = (row.Cells[0].FindControl("lblProfilechangedate") as Label);
        //               Session["chgsProfilechangeDate"] = lblProfilechangedate1.Text;
        //             Label lblNoofchange = (row.Cells[0].FindControl("lblNoofchange") as Label);
        //             Session["chgsNoOfchanges"] = lblNoofchange.Text;



        //         }
        //     }
        //     Page.ClientScript.RegisterStartupScript(
        //this.GetType(), "OpenWindow", "window.open('changeStatus.aspx','_newtab');", true);

    }






    string chgsUserid = ""; string chgsProfilechangeDate = ""; string chgsNoOfchanges = "";




    protected void btnchangestusrejected_Command(object sender, CommandEventArgs e)
    {
        Session["chgsUserid"] = e.CommandArgument.ToString();
    }

    protected void btnresolved_Command(object sender, CommandEventArgs e)
    {
        Session["chgsUserid"] = e.CommandArgument.ToString();
    }

    private void ToggleCheckState(bool checkState)
    {

        foreach (GridViewRow row in grdViewApproval.Rows)
        {

            CheckBox cb = (CheckBox)row.FindControl("chkMark");
            if (cb != null)
                cb.Checked = checkState;
        }
    }

    protected void btnselectchecked_Click(object sender, EventArgs e)
    {
        ToggleCheckState(true);
        btnselectchecked.Visible = false;
        btnuncheked.Visible = true;
    }
    protected void btnuncheked_Click(object sender, EventArgs e)
    {
        ToggleCheckState(false);
        btnselectchecked.Visible = true;
        btnuncheked.Visible = false;
    }

    protected void grdViewApproval_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Button btnWorkingDetails = (Button)e.Row.FindControl("btnWorkingDetails");
            Label lblLeaveTypegrid = (Label)e.Row.FindControl("lblLeaveTypegrid");
            Label lblleaveAttachmentFilename = (Label)e.Row.FindControl("lblleaveAttachmentFilename");
            LinkButton lnkDownloadgrid = (LinkButton)e.Row.FindControl("lnkDownloadgrid");
            if (lblleaveAttachmentFilename.Text.Trim() == "")
            {
                lnkDownloadgrid.Visible = false;
            }

            if (lblleaveAttachmentFilename.Text.Trim() != "")
            {
                lnkDownloadgrid.Visible = true;
            }
            if (lblLeaveTypegrid.Text == "CO")
            {
                btnWorkingDetails.Visible = true;
            }
            if (lblLeaveTypegrid.Text != "CO")
            {
                btnWorkingDetails.Visible = false;
            }
        }
    }

    public void ShowGridworkingdetails(string AutoNo)
    {
        SqlDataReader dr = con.Show_tble_cowithAutono(AutoNo);

        DataTable dt = new DataTable();
        dt.Load(dr);
        grdworkingdetails.DataSource = dt;
        grdworkingdetails.DataBind();
        dr.Close();
        Portalcon.DisConnect();

    }
    protected void btnWorkingDetails_Command(object sender, CommandEventArgs e)
    {
        pnlrejectedDetail.Visible = true;
        string AutoNo = e.CommandArgument.ToString();

        ShowGridworkingdetails(AutoNo);
        ModalPopupExtender2.Show();

    }
    protected void btnworkingDetailsrejected_Command(object sender, CommandEventArgs e)
    {
        pnlProfileRejected.Visible = true;
        string AutoNo = e.CommandArgument.ToString();

        ShowGridworkingdetails(AutoNo);
        ModalPopupExtender2.Show();

    }
    protected void grdRejected_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Button btnworkingDetailsrejected = (Button)e.Row.FindControl("btnworkingDetailsrejected");
            Label lblLeavetypeAppliedrejected = (Label)e.Row.FindControl("lblLeavetypeAppliedrejected");
            Label lblleaveAttachmentFilename = (Label)e.Row.FindControl("lblleaveAttachmentFilenameRej");
            LinkButton lnkDownloadgrid = (LinkButton)e.Row.FindControl("lnkDownloadgridRej");
            if (lblleaveAttachmentFilename.Text.Trim() == "")
            {
                lnkDownloadgrid.Visible = false;
            }

            if (lblleaveAttachmentFilename.Text.Trim() != "")
            {
                lnkDownloadgrid.Visible = true;
            }

            if (lblLeavetypeAppliedrejected.Text == "CO")
            {
                btnworkingDetailsrejected.Visible = false;
            }
            if (lblLeavetypeAppliedrejected.Text != "CO")
            {
                btnworkingDetailsrejected.Visible = false;
            }
        }
    }

    protected void grdResolved_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Button btnworkingDetailsApproved = (Button)e.Row.FindControl("btnworkingDetailsApproved");
            Label lblLeavetypeAppliedApproved = (Label)e.Row.FindControl("lblLeavetypeAppliedApproved");

            Label lblleaveAttachmentFilename = (Label)e.Row.FindControl("lblleaveAttachmentFilenameApprove");
            LinkButton lnkDownloadgrid = (LinkButton)e.Row.FindControl("lnkDownloadgridApprove");
            if (lblleaveAttachmentFilename.Text.Trim() == "")
            {
                lnkDownloadgrid.Visible = false;
            }

            if (lblleaveAttachmentFilename.Text.Trim() != "")
            {
                lnkDownloadgrid.Visible = true;
            }

            if (lblLeavetypeAppliedApproved.Text == "CO")
            {
                btnworkingDetailsApproved.Visible = true;
            }
            if (lblLeavetypeAppliedApproved.Text != "CO")
            {
                btnworkingDetailsApproved.Visible = false;
            }
        }
    }
    protected void btnworkingDetailsApproved_Command(object sender, CommandEventArgs e)
    {
        pnlApprovedDetail.Visible = true;
        string AutoNo = e.CommandArgument.ToString();

        ShowGridworkingdetails(AutoNo);
        ModalPopupExtender2.Show();

    }

    //protected void btnViewAttachment_Command(object sender, CommandEventArgs e)
    //{
    //     string AutoNo = e.CommandArgument.ToString();      
    //     ShowAttachment(AutoNo);
    //}
    //public void ShowAttachment(string AutoNo)
    //{
    //   SqlDataReader dr = con.Show_AttachmentNo(AutoNo);
    //   DataTable dt = new DataTable();
    //   string Extension="";  string FileName="" ;string PrintFileName;string attachment;
    //   dt.Load(dr);       
    //   byte[] buffer=null ;      //--assign null  
    //   if (dt.Rows.Count > 0)
    //   {

    //       buffer = (Byte[])dt.Rows[0]["Attachmentdata"];
    //       Extension = dt.Rows[0]["AttachmentFileType"].ToString();
    //       FileName = dt.Rows[0]["AttachmentFilename"].ToString();
    //   }

    //    if( FileName == "") {  PrintFileName = ""; }
    //    else{PrintFileName = FileName;}        
    //    attachment = "attachment; filename=Attachment" + " " + PrintFileName;
    //    Response.ClearContent();
    //    Response.AddHeader("content-disposition", attachment);
    //    //Response.ContentType = ""; //ReturnExtension(Extension);
    //    Response.ContentType = "";
    //    StringWriter stringWrite = new System.IO.StringWriter();
    //    Response.BinaryWrite(buffer);
    //    Response.End();
    //    dr.Close();
    //    Portalcon.DisConnect();    
    //}


    protected void DownloadFile(object sender, EventArgs e)
    {
        try
        {
            int id = int.Parse((sender as LinkButton).CommandArgument);
            byte[] bytes;
            string fileName, contentType;
            SqlConnection Conn = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]);
            Conn.Open();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select AttachmentFilename, Attachmentdata, AttachmentFileType from tble_Leave_Approval where AutoNo=@AutoNo";
                cmd.Parameters.AddWithValue("@AutoNo", id);
                cmd.Connection = Conn;
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Attachmentdata"];
                    contentType = sdr["AttachmentFileType"].ToString();
                    fileName = sdr["AttachmentFilename"].ToString();
                }
                con.DisConnect();

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
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Attachment not found ');", true);

        }
    }

    protected void btnexport_Pending_Click(object sender, EventArgs e)
    {
        grdViewApproval.AllowPaging = false;
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grdViewApproval.RenderControl(htmlWrite);

        Response.Clear();
        string str = "pending" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();


    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }


    protected void btnrejectedexport_Click(object sender, EventArgs e)
    {
        grdRejected.AllowPaging = false;
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grdRejected.RenderControl(htmlWrite);

        Response.Clear();
        string str = "rejected" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();

    }
    public void SendSMSForApprovedLeave(string userid, string FromDate, string Todate, string leavettpe, string uname)
    {
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");

        tablenameemployeedata = "[" + rccname + "$Employee" + "]";
        string mobilnoemp = "";
        string smsdata = "Dear " + uname + ", Your leave " + leavettpe + " from " + FromDate + " to " + Todate + " has been Approved. " + "Thanks - TMU";
        SqlDataReader dr = Portalcon.SHow_EmployeeMobileNo(userid, tablenameemployeedata);
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


    public void SendSMSForApprovedLeaveRejected(string userid, string FromDate, string Todate, string leavettpe, string uname)
    {
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");

        tablenameemployeedata = "[" + rccname + "$Employee" + "]";
        string mobilnoemp = "";
        string smsdata = "Dear " + uname + ", Your leave " + leavettpe + " from " + FromDate + " to " + Todate + " has been Rejected. " + "Thanks - TMU ";
        SqlDataReader dr = Portalcon.SHow_EmployeeMobileNo(userid, tablenameemployeedata);
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


    protected void btnExportApprove_Click(object sender, EventArgs e)
    {
        grdResolved.AllowPaging = false;
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grdResolved.RenderControl(htmlWrite);

        Response.Clear();
        string str = "approved" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();
    }

}