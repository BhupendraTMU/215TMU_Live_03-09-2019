using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;

using System.Configuration;
public partial class LeaveApproval : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal con;
    string HRHODsame = ""; string forHRisHOD = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Portalcon = new Connection();
            con = new ServicePoratal();
            if (!IsPostBack)
            {
                pnlrejectedDetail.Visible = false;
                ModalPopupExtender1.Hide();
            }
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
        pnlrejectedDetail.Visible = false;
        ModalPopupExtender1.Hide();
        rdEmployeeID.Checked = false;
        rdEmployeeName.Checked = false;
        rdDatewise.Checked = false;
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
        grdRejected.Visible = false;
        pnlrejectedDetail.Visible = false;
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
        grdResolved.Visible = false;
        pnlrejectedDetail.Visible = false;

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
        string type = "For Profile";
        SqlDataReader dr = con.SHow_tblesetupforpriorityforApproval(type,Session["Company"].ToString());
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


    public void profileApproval_Search()
    {

        if (rdDatewise.Checked == true)
        {

            //if (Session["UserType"].ToString() == "1")
            //{
            //    SqlDataReader odr = con.Profile_Approval_Detail_For_Admin_Datewise(txtfromDate.Text, txtTodate.Text, Session["Company"].ToString());
            //    DataTable Dt = new DataTable();
            //    Dt.Load(odr);
            //    grdViewApproval.DataSource = Dt;
            //    grdViewApproval.DataBind();
            //    odr.Close();
            //    con.DisConnect();

               
            
            //}

            //else
            //{



                if (Blankapr == "1")
                {
                    SqlDataReader odr = con.Profile_Approval_Detail_withdateUser(Session["uid"].ToString(), txtfromDate.Text, txtTodate.Text, Session["Company"].ToString());
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdViewApproval.DataSource = Dt;
                    grdViewApproval.DataBind();
                    odr.Close();
                    con.DisConnect();

                    SqlDataReader dr = con.Profile_Approval_Detail_withdateUser(Session["uid"].ToString(), txtfromDate.Text, txtTodate.Text, Session["Company"].ToString());
                    dr.Read();
                    if (dr.HasRows)
                    {
                        btnApprove.Visible = true;
                        btnreject.Visible = true;
                        btnselectchecked.Visible = true;
                        btnuncheked.Visible = false;
                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnreject.Visible = false;
                        btnselectchecked.Visible = false;
                        btnuncheked.Visible = false;
                    }
                    dr.Close();
                    con.DisConnect();

                }
                if (PriorityHODapr == "1")
                {
                    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                    {
                        SqlDataReader odr = con.Profile_Approval_Detail_withdateHR(Session["uid"].ToString(), txtfromDate.Text, txtTodate.Text, Session["Company"].ToString());
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdViewApproval.DataSource = Dt;
                        grdViewApproval.DataBind();
                        odr.Close();
                        con.DisConnect();

                        SqlDataReader dr = con.Profile_Approval_Detail_withdateHR(Session["uid"].ToString(), txtfromDate.Text, txtTodate.Text, Session["Company"].ToString());
                        dr.Read();
                        if (dr.HasRows)
                        {
                            btnApprove.Visible = true;
                            btnreject.Visible = true;
                            btnselectchecked.Visible = true;
                            btnuncheked.Visible = false;
                        }
                        else
                        {
                            btnApprove.Visible = false;
                            btnreject.Visible = false;
                            btnselectchecked.Visible = false;
                            btnuncheked.Visible = false;
                        }
                        dr.Close();
                        con.DisConnect();
                    }
                    else
                    {

                        SqlDataReader odr = con.Profile_Approval_Detail_withdate(Session["uid"].ToString(), txtfromDate.Text, txtTodate.Text, Session["Company"].ToString());
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdViewApproval.DataSource = Dt;
                        grdViewApproval.DataBind();
                        odr.Close();
                        con.DisConnect();

                        SqlDataReader dr = con.Profile_Approval_Detail_withdate(Session["uid"].ToString(), txtfromDate.Text, txtTodate.Text, Session["Company"].ToString());
                        dr.Read();
                        if (dr.HasRows)
                        {
                            btnApprove.Visible = true;
                            btnreject.Visible = true;
                            btnselectchecked.Visible = true;
                            btnuncheked.Visible = false;
                        }
                        else
                        {
                            btnApprove.Visible = false;
                            btnreject.Visible = false;
                            btnselectchecked.Visible = false;
                            btnuncheked.Visible = false;
                        }
                        dr.Close();
                        con.DisConnect();
                    }

                }
                if (PriorityHRapr == "1")
                {
                    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                    {
                        SqlDataReader odr = con.Profile_Approval_Detail_withdateHR(Session["uid"].ToString(), txtfromDate.Text, txtTodate.Text, Session["Company"].ToString());
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdViewApproval.DataSource = Dt;
                        grdViewApproval.DataBind();
                        odr.Close();
                        con.DisConnect();

                        SqlDataReader dr = con.Profile_Approval_Detail_withdateHR(Session["uid"].ToString(), txtfromDate.Text, txtTodate.Text, Session["Company"].ToString());
                        dr.Read();
                        if (dr.HasRows)
                        {
                            btnApprove.Visible = true;
                            btnreject.Visible = true;
                            btnselectchecked.Visible = true;
                            btnuncheked.Visible = false;
                        }
                        else
                        {
                            btnApprove.Visible = false;
                            btnreject.Visible = false;
                            btnselectchecked.Visible = false;
                            btnuncheked.Visible = false;
                        }
                        dr.Close();
                        con.DisConnect();

                    }
                    else
                    {
                        SqlDataReader odr = con.Profile_Approval_Detail_withdate(Session["uid"].ToString(), txtfromDate.Text, txtTodate.Text, Session["Company"].ToString());
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdViewApproval.DataSource = Dt;
                        grdViewApproval.DataBind();
                        odr.Close();
                        con.DisConnect();

                        SqlDataReader dr = con.Profile_Approval_Detail_withdate(Session["uid"].ToString(), txtfromDate.Text, txtTodate.Text, Session["Company"].ToString());
                        dr.Read();
                        if (dr.HasRows)
                        {
                            btnApprove.Visible = true;
                            btnreject.Visible = true;
                            btnselectchecked.Visible = true;
                            btnuncheked.Visible = false;
                        }
                        else
                        {
                            btnApprove.Visible = false;
                            btnreject.Visible = false;
                            btnselectchecked.Visible = false;
                            btnuncheked.Visible = false;
                        }
                        dr.Close();
                        con.DisConnect();

                    }
                }

           // }
        }

        if (rdEmployeeID.Checked == true)
        {

            //if (Session["UserType"].ToString() == "1")
            //{

            //    SqlDataReader odr = con.Profile_Approval_Detail_ForAdminwithuserid(Session["Company"].ToString(), txtSearchName.Text);
            //    DataTable Dt = new DataTable();
            //    Dt.Load(odr);
            //    grdViewApproval.DataSource = Dt;
            //    grdViewApproval.DataBind();
            //    odr.Close();
            //    con.DisConnect();

            //}

            //else
            //{


                if (Blankapr == "1")
                {
                    SqlDataReader odr = con.Profile_Approval_Detail_withUserIDUser(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdViewApproval.DataSource = Dt;
                    grdViewApproval.DataBind();
                    odr.Close();
                    con.DisConnect();

                    SqlDataReader dr = con.Profile_Approval_Detail_withUserIDUser(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                    dr.Read();
                    if (dr.HasRows)
                    {
                        btnApprove.Visible = true;
                        btnreject.Visible = true;
                        btnselectchecked.Visible = true;
                        btnuncheked.Visible = false;
                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnreject.Visible = false;
                        btnselectchecked.Visible = false;
                        btnuncheked.Visible = false;
                    }
                    dr.Close();
                    con.DisConnect();

                }

                if (PriorityHODapr == "1")
                {
                    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                    {
                        SqlDataReader odr = con.Profile_Approval_Detail_withUserIDHR(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdViewApproval.DataSource = Dt;
                        grdViewApproval.DataBind();
                        odr.Close();
                        con.DisConnect();

                        SqlDataReader dr = con.Profile_Approval_Detail_withUserIDHR(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                        dr.Read();
                        if (dr.HasRows)
                        {
                            btnApprove.Visible = true;
                            btnreject.Visible = true;
                            btnselectchecked.Visible = true;
                            btnuncheked.Visible = false;
                        }
                        else
                        {
                            btnApprove.Visible = false;
                            btnreject.Visible = false;
                            btnselectchecked.Visible = false;
                            btnuncheked.Visible = false;
                        }
                        dr.Close();
                        con.DisConnect();


                    }
                    else
                    {
                        SqlDataReader odr = con.Profile_Approval_Detail_withUserID(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdViewApproval.DataSource = Dt;
                        grdViewApproval.DataBind();
                        odr.Close();
                        con.DisConnect();

                        SqlDataReader dr = con.Profile_Approval_Detail_withUserID(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                        dr.Read();
                        if (dr.HasRows)
                        {
                            btnApprove.Visible = true;
                            btnreject.Visible = true;
                            btnselectchecked.Visible = true;
                            btnuncheked.Visible = false;
                        }
                        else
                        {
                            btnApprove.Visible = false;
                            btnreject.Visible = false;
                            btnselectchecked.Visible = false;
                            btnuncheked.Visible = false;
                        }
                        dr.Close();
                        con.DisConnect();
                    }
                }
                if (PriorityHRapr == "1")
                {
                    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                    {
                        SqlDataReader odr = con.Profile_Approval_Detail_withUserIDHR(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdViewApproval.DataSource = Dt;
                        grdViewApproval.DataBind();
                        odr.Close();
                        con.DisConnect();

                        SqlDataReader dr = con.Profile_Approval_Detail_withUserIDHR(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                        dr.Read();
                        if (dr.HasRows)
                        {
                            btnApprove.Visible = true;
                            btnreject.Visible = true;
                            btnselectchecked.Visible = true;
                            btnuncheked.Visible = false;
                        }
                        else
                        {
                            btnApprove.Visible = false;
                            btnreject.Visible = false;
                            btnselectchecked.Visible = false;
                            btnuncheked.Visible = false;
                        }
                        dr.Close();
                        con.DisConnect();
                    }
                    else
                    {
                        SqlDataReader odr = con.Profile_Approval_Detail_withUserID(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdViewApproval.DataSource = Dt;
                        grdViewApproval.DataBind();
                        odr.Close();
                        con.DisConnect();

                        SqlDataReader dr = con.Profile_Approval_Detail_withUserID(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                        dr.Read();
                        if (dr.HasRows)
                        {
                            btnApprove.Visible = true;
                            btnreject.Visible = true;
                            btnselectchecked.Visible = true;
                            btnuncheked.Visible = false;
                        }
                        else
                        {
                            btnApprove.Visible = false;
                            btnreject.Visible = false;
                            btnselectchecked.Visible = false;
                            btnuncheked.Visible = false;
                        }
                        dr.Close();
                        con.DisConnect();

                    }


                }

            //}

        }

        if (rdEmployeeName.Checked == true)
        {

            //if (Session["UserType"].ToString() == "1")
            //{
            //    SqlDataReader odr = con.Profile_Approval_Detail_for_Admin_UserNameUser( Session["Company"].ToString(), txtSearchName.Text);
            //    DataTable Dt = new DataTable();
            //    Dt.Load(odr);
            //    grdViewApproval.DataSource = Dt;
            //    grdViewApproval.DataBind();
            //    odr.Close();
            //    con.DisConnect();

               
            //}
            //else
            //{

                if (Blankapr == "1")
                {
                    SqlDataReader odr = con.Profile_Approval_Detail_withUserNameUser(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdViewApproval.DataSource = Dt;
                    grdViewApproval.DataBind();
                    odr.Close();
                    con.DisConnect();

                    SqlDataReader dr = con.Profile_Approval_Detail_withUserNameUser(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                    dr.Read();
                    if (dr.HasRows)
                    {
                        btnApprove.Visible = true;
                        btnreject.Visible = true;
                        btnselectchecked.Visible = true;
                        btnuncheked.Visible = false;
                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnreject.Visible = false;
                        btnselectchecked.Visible = false;
                        btnuncheked.Visible = false;
                    }
                    dr.Close();
                    con.DisConnect();

                }
                if (PriorityHODapr == "1")
                {
                    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                    {
                        SqlDataReader odr = con.Profile_Approval_Detail_withUserNameHR(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdViewApproval.DataSource = Dt;
                        grdViewApproval.DataBind();
                        odr.Close();
                        con.DisConnect();

                        SqlDataReader dr = con.Profile_Approval_Detail_withUserNameHR(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                        dr.Read();
                        if (dr.HasRows)
                        {
                            btnApprove.Visible = true;
                            btnreject.Visible = true;
                            btnselectchecked.Visible = true;
                            btnuncheked.Visible = false;
                        }
                        else
                        {
                            btnApprove.Visible = false;
                            btnreject.Visible = false;
                            btnselectchecked.Visible = false;
                            btnuncheked.Visible = false;
                        }
                        dr.Close();
                        con.DisConnect();

                    }
                    else
                    {

                        SqlDataReader odr = con.Profile_Approval_Detail_withUserName(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdViewApproval.DataSource = Dt;
                        grdViewApproval.DataBind();
                        odr.Close();
                        con.DisConnect();

                        SqlDataReader dr = con.Profile_Approval_Detail_withUserName(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                        dr.Read();
                        if (dr.HasRows)
                        {
                            btnApprove.Visible = true;
                            btnreject.Visible = true;
                            btnselectchecked.Visible = true;
                            btnuncheked.Visible = false;
                        }
                        else
                        {
                            btnApprove.Visible = false;
                            btnreject.Visible = false;
                            btnselectchecked.Visible = false;
                            btnuncheked.Visible = false;
                        }
                        dr.Close();
                        con.DisConnect();
                    }
                }
                if (PriorityHRapr == "1")
                {
                    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                    {
                        SqlDataReader odr = con.Profile_Approval_Detail_withUserNameHR(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdViewApproval.DataSource = Dt;
                        grdViewApproval.DataBind();
                        odr.Close();
                        con.DisConnect();

                        SqlDataReader dr = con.Profile_Approval_Detail_withUserNameHR(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                        dr.Read();
                        if (dr.HasRows)
                        {
                            btnApprove.Visible = true;
                            btnreject.Visible = true;
                            btnselectchecked.Visible = true;
                            btnuncheked.Visible = false;
                        }
                        else
                        {
                            btnApprove.Visible = false;
                            btnreject.Visible = false;
                            btnselectchecked.Visible = false;
                            btnuncheked.Visible = false;
                        }
                        dr.Close();
                        con.DisConnect();
                    }
                    else
                    {
                        SqlDataReader odr = con.Profile_Approval_Detail_withUserName(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdViewApproval.DataSource = Dt;
                        grdViewApproval.DataBind();
                        odr.Close();
                        con.DisConnect();

                        SqlDataReader dr = con.Profile_Approval_Detail_withUserName(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
                        dr.Read();
                        if (dr.HasRows)
                        {
                            btnApprove.Visible = true;
                            btnreject.Visible = true;
                            btnselectchecked.Visible = true;
                            btnuncheked.Visible = false;
                        }
                        else
                        {
                            btnApprove.Visible = false;
                            btnreject.Visible = false;
                            btnselectchecked.Visible = false;
                            btnuncheked.Visible = false;
                        }
                        dr.Close();
                        con.DisConnect();

                    }

                }
            //}


        }

        if (CHKAllPending.Checked == true)
        {

            //if (Session["UserType"].ToString() == "1")
            //{
            //    SqlDataReader odr = con.Profile_Approval_Detail_For_Admin_withALLPendingBlank(Session["Company"].ToString());
            //    DataTable Dt = new DataTable();
            //    Dt.Load(odr);
            //    grdViewApproval.DataSource = Dt;
            //    grdViewApproval.DataBind();
            //    odr.Close();
            //    con.DisConnect();

               
            
            //}
            //else
            //{

                if (Blankapr == "1")
                {
                    SqlDataReader odr = con.Profile_Approval_Detail_withALLPendingBlank(Session["uid"].ToString(), Session["Company"].ToString());
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdViewApproval.DataSource = Dt;
                    grdViewApproval.DataBind();
                    odr.Close();
                    con.DisConnect();

                    SqlDataReader dr = con.Profile_Approval_Detail_withALLPendingBlank(Session["uid"].ToString(), Session["Company"].ToString());
                    dr.Read();
                    if (dr.HasRows)
                    {
                        btnApprove.Visible = true;
                        btnreject.Visible = true;
                        btnselectchecked.Visible = true;
                        btnuncheked.Visible = false;
                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnreject.Visible = false;
                        btnselectchecked.Visible = false;
                        btnuncheked.Visible = false;
                    }
                    dr.Close();
                    con.DisConnect();

                }
                if (PriorityHODapr == "1")
                {
                    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                    {
                        SqlDataReader odr = con.Profile_Approval_Detail_withPriorityHRAllPending(Session["uid"].ToString(), Session["Company"].ToString());
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdViewApproval.DataSource = Dt;
                        grdViewApproval.DataBind();
                        odr.Close();
                        con.DisConnect();

                        SqlDataReader dr = con.Profile_Approval_Detail_withPriorityHRAllPending(Session["uid"].ToString(), Session["Company"].ToString());
                        dr.Read();
                        if (dr.HasRows)
                        {
                            btnApprove.Visible = true;
                            btnreject.Visible = true;
                            btnselectchecked.Visible = true;
                            btnuncheked.Visible = false;
                        }
                        else
                        {
                            btnApprove.Visible = false;
                            btnreject.Visible = false;
                            btnselectchecked.Visible = false;
                            btnuncheked.Visible = false;
                        }
                        dr.Close();
                        con.DisConnect();

                    }
                    else
                    {

                        SqlDataReader odr = con.Profile_Approval_Detail_withHODALLPending(Session["uid"].ToString(), Session["Company"].ToString());
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdViewApproval.DataSource = Dt;
                        grdViewApproval.DataBind();
                        odr.Close();
                        con.DisConnect();

                        SqlDataReader dr = con.Profile_Approval_Detail_withHODALLPending(Session["uid"].ToString(), Session["Company"].ToString());
                        dr.Read();
                        if (dr.HasRows)
                        {
                            btnApprove.Visible = true;
                            btnreject.Visible = true;
                            btnselectchecked.Visible = true;
                            btnuncheked.Visible = false;
                        }
                        else
                        {
                            btnApprove.Visible = false;
                            btnreject.Visible = false;
                            btnselectchecked.Visible = false;
                            btnuncheked.Visible = false;
                        }
                        dr.Close();
                        con.DisConnect();
                    }
                }
                if (PriorityHRapr == "1")
                {
                    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                    {
                        SqlDataReader odr = con.Profile_Approval_Detail_withPriorityHRAllPending(Session["uid"].ToString(), Session["Company"].ToString());
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdViewApproval.DataSource = Dt;
                        grdViewApproval.DataBind();
                        odr.Close();
                        con.DisConnect();

                        SqlDataReader dr = con.Profile_Approval_Detail_withPriorityHRAllPending(Session["uid"].ToString(), Session["Company"].ToString());
                        dr.Read();
                        if (dr.HasRows)
                        {
                            btnApprove.Visible = true;
                            btnreject.Visible = true;
                            btnselectchecked.Visible = true;
                            btnuncheked.Visible = false;
                        }
                        else
                        {
                            btnApprove.Visible = false;
                            btnreject.Visible = false;
                            btnselectchecked.Visible = false;
                            btnuncheked.Visible = false;
                        }
                        dr.Close();
                        con.DisConnect();
                    }
                    else
                    {
                        SqlDataReader odr = con.Profile_Approval_Detail_withHODALLPending(Session["uid"].ToString(), Session["Company"].ToString());
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdViewApproval.DataSource = Dt;
                        grdViewApproval.DataBind();
                        odr.Close();
                        con.DisConnect();

                        SqlDataReader dr = con.Profile_Approval_Detail_withHODALLPending(Session["uid"].ToString(), Session["Company"].ToString());
                        dr.Read();
                        if (dr.HasRows)
                        {
                            btnApprove.Visible = true;
                            btnreject.Visible = true;
                            btnselectchecked.Visible = true;
                            btnuncheked.Visible = false;
                        }
                        else
                        {
                            btnApprove.Visible = false;
                            btnreject.Visible = false;
                            btnselectchecked.Visible = false;
                            btnuncheked.Visible = false;
                        }
                        dr.Close();
                        con.DisConnect();

                    }

                }


          //  }
        }


    }

    string mailfrom = ""; string subject1 = ""; string Body1 = ""; string smtpfromportal = ""; int portNo1; string Pass_From = ""; string Mail_Sending_Option = ""; string Leave_Applymail = ""; string CCMail = "";
    public void ShowMailDataTo(string mailTo1)
    {

        SqlDataReader dr = con.Show_tble_MailSetup(Session["Company"].ToString(), "Profile");
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
            Leave_Applymail = dr["Profile_Approval"].ToString();

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

                msg.From = new MailAddress(Session["CompanyEmail"].ToString().Trim(), "  " + Session["Fulname"].ToString().Trim());
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        grdViewApproval.Visible = true;
        profileApproval_Search();
    }
    string chkUSerID = ""; string profilechangedate = ""; string lblnoofchangeu1 = ""; string HRAPRStatus = "";
    string HODAPRStatus = "";
    public void FindCheckData()
    {


        SqlDataReader drall=con.Approvalfor_ProfileData(Session["Company"].ToString(),chkUSerID,profilechangedate,lblnoofchangeu1);
        drall.Read();
        if(drall.HasRows)
        {

        //SqlDataAdapter da = new SqlDataAdapter("select * from tble_ProfileApprovalStatus where UserID='" + chkUSerID + "' and CompanyName='" + Session["Company"].ToString() + "' and ProfileUpdateDate ='" + profilechangedate + "' and id='" + lblnoofchangeu1 + "' ", con.Con);
        //DataSet ds = new DataSet();
        //da.Fill(ds, "tble_ProfileApprovalStatus");
        //for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
        //{
            string tytle1 = ""; string FName1 = ""; string SName1 = ""; string LName1 = ""; string Address1 = ""; string Address21 = ""; string City1 = ""; string STate1 = ""; string Country1 = ""; string Pincode1 = ""; string Emailid1 = ""; string Sex1 = ""; string Status1 = ""; string PhoneNo1 = ""; string MobileNo1 = ""; string DOB1 = ""; string HusBandName1 = ""; string AddressPermanent1 = ""; string AddressPermanent = ""; string CityPermanent1 = ""; string CountryPermanent1 = ""; string statePermanent = ""; string PincodePermanent1 = ""; string PanNo1 = ""; string CompanyMail1 = ""; string ACNO1 = ""; string change_stSMS = ""; string change_status = ""; string path = ""; string fatherName1 = ""; string hodemailid = ""; string hoduserid = ""; string MotherName1 = ""; string CompanyName1 = ""; string imgname = ""; string prfPhoto1 = ""; string HR_Userid = "";
            string HREmailidapr = ""; string HODEmailIDapr = ""; string HRNameapr = ""; string HODNameapr = "";
            tytle1 = drall["title"].ToString();
            FName1 = drall["Fname"].ToString();
            SName1 = drall["SName"].ToString();
            LName1 = drall["LName"].ToString();

            Address1 = drall["Address"].ToString();

            Address21 = drall["Address2"].ToString();
            City1 = drall["City"].ToString();
            STate1 = drall["State"].ToString();
            Country1 = drall["Country"].ToString();
            Pincode1 = drall["Pin_Code"].ToString();
            Emailid1 = drall["Email_ID"].ToString();
            Sex1 = drall["Sex"].ToString();
            Status1 = drall["Marital_Status"].ToString();
            PhoneNo1 = drall["Phone_No"].ToString();
            MobileNo1 = drall["Mobile_No"].ToString();
            DOB1 = drall["DOB"].ToString();
            fatherName1 = drall["FatherName"].ToString();
            HusBandName1 = drall["HusbandName"].ToString();
            MotherName1 = drall["MotherName"].ToString();
            AddressPermanent = drall["PAddress"].ToString();
            AddressPermanent1 = drall["PAddress2"].ToString();
            CityPermanent1 = drall["PCity"].ToString();
            statePermanent = drall["PState"].ToString();
            CountryPermanent1 = drall["PCountry"].ToString();
            PincodePermanent1 = drall["PPinCode"].ToString();
            PanNo1 = drall["PanNo"].ToString();
            ACNO1 = drall["Ac_No"].ToString();
            PanNo1 = drall["PanNo"].ToString();
            HR_Userid = drall["HRUserID"].ToString();
            imgname = drall["Photo"].ToString();
            prfPhoto1 = drall["Photo1"].ToString();

            CompanyName1 = drall["CompanyName"].ToString();


            HREmailidapr = drall["HREmailid"].ToString();
            HODEmailIDapr = drall["HODEmailID"].ToString();
            HRNameapr = drall["HRName"].ToString();
            HODNameapr = drall["HODName"].ToString();
            DateTime dob3 = DateTime.ParseExact(DOB1, "d/M/yyyy", null);
            drall.Close();
            con.DisConnect();
            if (Blankapr == "1")
            {


                Portalcon.Update_Navision_Resolved_Profile(tytle1, FName1, SName1, LName1, Address1, Address21, City1, Country1, Pincode1, Emailid1, Status1, PhoneNo1, MobileNo1, dob3.ToString("yyyy-MM-dd"), fatherName1, HusBandName1, AddressPermanent, AddressPermanent1, CityPermanent1, statePermanent, PanNo1, ACNO1, Session["CompanyTableEmployee"].ToString(), chkUSerID, imgname, prfPhoto1, STate1);
                Portalcon.DisConnect();
                con.Update_tble_ProfileApprovalStatus_Resolved(System.DateTime.Now.ToString("dd/MM/yyyy"), chkUSerID, profilechangedate, lblnoofchangeu1, Session["Company"].ToString());
                con.DisConnect();
                subject1 = "Your Changes Profiles are Approved ";

                Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}", Environment.NewLine, "" + FName1 + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Your Changes Profiles are Approved Please check Profile Detail " , "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                ShowMailDataTo(Emailid1);

                if (EmailHOD == "True")
                {
                    subject1 = "Profile Approved of " +FName1;
                    
                    Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}", Environment.NewLine, "" + HODNameapr + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Profiles are Approved ", "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                    ShowMailDataTo(HODEmailIDapr);

                }

                if (EmailHR == "True")
                {

                    subject1 = "Profile Approved of " + FName1;
                    string hrn = "HR Manager";
                    Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}", Environment.NewLine, "" + hrn + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Profiles are Approved ", "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                    ShowMailDataTo(HREmailidapr);

                }
            }


            if (HRapr == "1" && HODapr == "1")
            {
                if (PriorityHRapr == "1" && Session["UserType"].ToString() == "2" || PriorityHRapr == "1" && Session["UserType"].ToString() == "2" && HRHODsame == "HR" || PriorityHRapr == "1" && Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                {
                    SqlDataReader drap = con.Show_ApprovalStatusHR_HOD(chkUSerID, profilechangedate, lblnoofchangeu1, Session["Company"].ToString());
                    drap.Read();
                    if (drap.HasRows)
                    {
                        HRAPRStatus = drap["CountStatusHR"].ToString();
                        HODAPRStatus = drap["CountStatusHOD"].ToString();
                        drap.Close();
                        con.DisConnect();
                        if (HRAPRStatus == "Pending" && HODAPRStatus == "Pending")
                        {
                            string ApprovalStatus = "Approved By HR and HR Forwarded this record for HOD Approval";
                            string CountStatusHR = "Approved";
                            Portalcon.DisConnect();
                            SqlDataReader dr = Portalcon.SHow_HODIDForApproval(chkUSerID, Session["CompanyTableEmployee"].ToString());
                            dr.Read();
                            if (dr.HasRows)
                            {
                                string hodid = "";
                                hodid = dr["HOD"].ToString();
                                dr.Close();
                                con.DisConnect();
                                if (hodid == "")
                                {
                                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('There is no HOD ID available of this user Please update HOD');", true);
                                }
                                else
                                {
                                    con.Update_tble_ProfileApprovalStatus_ResolvedHR(System.DateTime.Now.ToString("dd/MM/yyyy"), chkUSerID, profilechangedate, lblnoofchangeu1, Session["Company"].ToString(), hodid, ApprovalStatus, CountStatusHR);
                                    con.DisConnect();


                                    subject1 = "Pending Profile Approval of " + FName1;
                                    
                                    Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}", Environment.NewLine, "" + HODNameapr + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Profiles are Approved by HR ", "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                                    ShowMailDataTo(HODEmailIDapr);

                                }
                            }


                        }


                        else
                        {
                            string ApprovalStatus = "Approved";
                            string CountStatusHR = "Approved";
                            Portalcon.DisConnect();
                            SqlDataReader dr = Portalcon.SHow_HODIDForApproval(chkUSerID, Session["CompanyTableEmployee"].ToString());
                            dr.Read();
                            if (dr.HasRows)
                            {
                                string hodid = "";
                                hodid = dr["HOD"].ToString();
                                dr.Close();
                                con.DisConnect();
                                if (hodid == "")
                                {
                                    dr.Close();
                                    con.DisConnect();
                                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('There is no HOD ID available of this user Please update HOD');", true);
                                }
                                else
                                {
                                    Portalcon.Update_Navision_Resolved_Profile(tytle1, FName1, SName1, LName1, Address1, Address21, City1, Country1, Pincode1, Emailid1, Status1, PhoneNo1, MobileNo1, dob3.ToString("yyyy-MM-dd"), fatherName1, HusBandName1, AddressPermanent, AddressPermanent1, CityPermanent1, statePermanent, PanNo1, ACNO1, Session["CompanyTableEmployee"].ToString(), chkUSerID, imgname, prfPhoto1, STate1);
                                    Portalcon.DisConnect();
                                    con.Update_tble_ProfileApprovalStatus_ResolvedHR(System.DateTime.Now.ToString("dd/MM/yyyy"), chkUSerID, profilechangedate, lblnoofchangeu1, Session["Company"].ToString(), hodid, ApprovalStatus, CountStatusHR);
                                    con.DisConnect();

                                    subject1 = "Your Changes Profiles are Approved ";

                                    Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}", Environment.NewLine, "" + FName1 + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Your Changes Profiles are Approved Please check Profile Detail  " +   "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                                    ShowMailDataTo(Emailid1);



                                }
                            }

                        }
                    }


                }

                if (PriorityHODapr == "1" && Session["UserType"].ToString() == "2" || PriorityHODapr == "1" && Session["UserType"].ToString() == "2" && HRHODsame == "HR" || PriorityHODapr == "1" && Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                {
                    string ApprovalStatus = "Approved";
                    string CountStatusHR = "Approved";
                    Portalcon.DisConnect();
                    SqlDataReader dr = Portalcon.SHow_HODIDForApproval(chkUSerID, Session["CompanyTableEmployee"].ToString());
                    dr.Read();
                    if (dr.HasRows)
                    {
                        string hodid = "";
                        hodid = dr["HOD"].ToString();
                        dr.Close();
                        con.DisConnect();
                        if (hodid == "")
                        {
                            dr.Close();
                            con.DisConnect();
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('There is no HOD ID available of this user Please update HOD');", true);
                        }
                        else
                        {
                            Portalcon.Update_Navision_Resolved_Profile(tytle1, FName1, SName1, LName1, Address1, Address21, City1, Country1, Pincode1, Emailid1, Status1, PhoneNo1, MobileNo1, dob3.ToString("yyyy-MM-dd"), fatherName1, HusBandName1, AddressPermanent, AddressPermanent1, CityPermanent1, statePermanent, PanNo1, ACNO1, Session["CompanyTableEmployee"].ToString(), chkUSerID, imgname, prfPhoto1, STate1);
                            Portalcon.DisConnect();
                            con.Update_tble_ProfileApprovalStatus_ResolvedHR(System.DateTime.Now.ToString("dd/MM/yyyy"), chkUSerID, profilechangedate, lblnoofchangeu1, Session["Company"].ToString(), hodid, ApprovalStatus, CountStatusHR);
                            con.DisConnect();
                            subject1 = "Your Changes Profiles are Approved ";

                            Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}", Environment.NewLine, "" + FName1 + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Your Changes Profiles are Approved Please check Profile Detail  " + "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                            ShowMailDataTo(Emailid1);
                        }
                    }

                }


                else if (Session["UserType"].ToString() != "2")
                {

                    SqlDataReader drap = con.Show_ApprovalStatusHR_HOD(chkUSerID, profilechangedate, lblnoofchangeu1, Session["Company"].ToString());
                    drap.Read();
                    if (drap.HasRows)
                    {
                        HRAPRStatus = drap["CountStatusHR"].ToString();
                        HODAPRStatus = drap["CountStatusHOD"].ToString();
                        drap.Close();
                        con.DisConnect();
                        if (HRAPRStatus == "Pending" && HODAPRStatus == "Pending")
                        {

                            string ApprovalStatus = "Approved By HOD it forwared to HR for Approval";
                            string CountStatusHOD = "Approved";
                            Portalcon.DisConnect();


                            SqlDataReader dr = Portalcon.SHow_HODIDForApproval(chkUSerID, Session["CompanyTableEmployee"].ToString());
                            dr.Read();
                            if (dr.HasRows)
                            {
                                string HRIDIND = "";
                                HRIDIND = dr["HR"].ToString();
                                dr.Close();
                                con.DisConnect();
                                if (HRIDIND == "")
                                {
                                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('There is no HR ID available of this user Please update HR ');", true);
                                }
                                else
                                {

                                    con.Update_tble_ProfileApprovalStatus_ResolvedHOD(System.DateTime.Now.ToString("dd/MM/yyyy"), chkUSerID, profilechangedate, lblnoofchangeu1, Session["Company"].ToString(), HRIDIND, ApprovalStatus, CountStatusHOD);
                                    con.DisConnect();

                                    subject1 = "Pending Profile Approval of " + FName1;
                                    string HRTY = "HR Manager";
                                    Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}", Environment.NewLine, "" + HRTY + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Profiles are Approved by HR ", "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                                    ShowMailDataTo(HREmailidapr);
                                }
                            }
                            dr.Close();
                            con.DisConnect();



                           
                        }
                        else
                        {
                            string ApprovalStatus = "Approved";
                            string CountStatusHOD = "Approved";
                            Portalcon.DisConnect();
                            drap.Close();
                            con.DisConnect();
                            Portalcon.Update_Navision_Resolved_Profile(tytle1, FName1, SName1, LName1, Address1, Address21, City1, Country1, Pincode1, Emailid1, Status1, PhoneNo1, MobileNo1, dob3.ToString("yyyy-MM-dd"), fatherName1, HusBandName1, AddressPermanent, AddressPermanent1, CityPermanent1, statePermanent, PanNo1, ACNO1, Session["CompanyTableEmployee"].ToString(), chkUSerID, imgname, prfPhoto1, STate1);
                            Portalcon.DisConnect();
                            con.Update_tble_ProfileApprovalStatus_ResolvedHOD(System.DateTime.Now.ToString("dd/MM/yyyy"), chkUSerID, profilechangedate, lblnoofchangeu1, Session["Company"].ToString(), HR_Userid, ApprovalStatus, CountStatusHOD);
                            con.DisConnect();

                            subject1 = "Your Changes Profiles are Approved ";

                            Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}", Environment.NewLine, "" + FName1 + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Your Changes Profiles are Approved Please check Profile Detail  " +  "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                            ShowMailDataTo(Emailid1);



                        }

                    }




                }

                //if (PriorityHODapr == "1")
                //{
                //    string ApprovalStatus = "Approved By HOD";
                //    string CountStatusHOD="Approved";
                //    Portalcon.DisConnect();
                //    con.Update_tble_ProfileApprovalStatus_ResolvedHOD(System.DateTime.Now.ToString("dd/MM/yyyy"), chkUSerID, profilechangedate, lblnoofchangeu1, Session["Company"].ToString(), Session["HRID"].ToString(), ApprovalStatus, CountStatusHOD);
                //    con.DisConnect();
                //}


            }
            if (HRapr == "1" && HODapr == "0")
            {
                Portalcon.Update_Navision_Resolved_Profile(tytle1, FName1, SName1, LName1, Address1, Address21, City1, Country1, Pincode1, Emailid1, Status1, PhoneNo1, MobileNo1, dob3.ToString("yyyy-MM-dd"), fatherName1, HusBandName1, AddressPermanent, AddressPermanent1, CityPermanent1, statePermanent, PanNo1, ACNO1, Session["CompanyTableEmployee"].ToString(), chkUSerID, imgname, prfPhoto1, STate1);

                Portalcon.DisConnect();

                con.Update_tble_ProfileApprovalStatus_ResolvedHRFinal(System.DateTime.Now.ToString("dd/MM/yyyy"), chkUSerID, profilechangedate, lblnoofchangeu1, Session["Company"].ToString());
                con.DisConnect();
                subject1 = "Your Changes Profiles are Approved ";

                Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}", Environment.NewLine, "" + FName1 + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Your Changes Profiles are Approved Please check Profile Detail " +  "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                ShowMailDataTo(Emailid1);

                if (EmailHOD == "True")
                {
                    subject1 = "Profiles are Approved by HR  of  " + FName1;

                    Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}", Environment.NewLine, "" + HODNameapr + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " Profiles are Approved" + "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                    ShowMailDataTo(HODEmailIDapr);
                
                }


            }

            if (HODapr == "1" && HRapr == "0")
            {


                Portalcon.Update_Navision_Resolved_Profile(tytle1, FName1, SName1, LName1, Address1, Address21, City1, Country1, Pincode1, Emailid1, Status1, PhoneNo1, MobileNo1, dob3.ToString("yyyy-MM-dd"), fatherName1, HusBandName1, AddressPermanent, AddressPermanent1, CityPermanent1, statePermanent, PanNo1, ACNO1, Session["CompanyTableEmployee"].ToString(), chkUSerID, imgname, prfPhoto1, STate1);

                Portalcon.DisConnect();

                con.Update_tble_ProfileApprovalStatus_ResolvedHODFinal(System.DateTime.Now.ToString("dd/MM/yyyy"), chkUSerID, profilechangedate, lblnoofchangeu1, Session["Company"].ToString());
                con.DisConnect();

                subject1 = "Your Changes Profiles are Approved ";

                Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}", Environment.NewLine, "" + FName1 + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Your Changes Profiles are Approved Please check Profile Detail" +  "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                ShowMailDataTo(Emailid1);

                if (EmailHR == "True")
                {
                    subject1 = "Profiles are Approved by HOD  of  " + FName1;
                    string hrt = "HR Manager";
                    Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}", Environment.NewLine, "" + hrt + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Profiles are Approved " + "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                    ShowMailDataTo(HREmailidapr);

                }
            }
        }
        drall.Close();
        con.DisConnect();
    }


    public void FindCheckDataRejected()
    {
        //SqlDataAdapter da = new SqlDataAdapter("select * from tble_ProfileApprovalStatus where UserID='" + chkUSerIDRejected + "' and CompanyName='" + Session["Company"].ToString() + "' and ProfileUpdateDate ='" + profilechangedaterejected + "' and id='" + lblnoofchangeReject1 + "'", con.Con);
        //DataSet ds = new DataSet();
        //da.Fill(ds, "tble_ProfileApprovalStatus");
        //for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
        //{

        SqlDataReader drall=con.Rejectedfor_ProfileData1(Session["Company"].ToString() ,chkUSerIDRejected,profilechangedaterejected,lblnoofchangeReject1);
        drall.Read();
        if (drall.HasRows)
        {
            string CompanyName1 = "";
            string tytle1 = ""; string FName1 = ""; string Emailid1 = ""; string HREmailidrej = ""; string HODEmailIDrej = ""; string HRNamerej = ""; string HODNamerej = "";

            tytle1 = drall["title"].ToString();
            FName1 = drall["Fname"].ToString();

            Emailid1 = drall["Email_ID"].ToString();

            HREmailidrej = drall["HREmailid"].ToString();
            HODEmailIDrej = drall["HODEmailID"].ToString();
            HRNamerej = drall["HRName"].ToString();
            HODNamerej = drall["HODName"].ToString();


            CompanyName1 = drall["CompanyName"].ToString();
            drall.Close();
            con.DisConnect();
            if (Blankapr == "1")
            {
                con.Update_tble_ProfileApprovalStatus_RejectedbyUser(txtRemarksRejected.Text, "Yes", System.DateTime.Now.ToString("dd/MM/yyyy"), chkUSerIDRejected, profilechangedaterejected, lblnoofchangeReject1, Session["Company"].ToString());
                con.DisConnect();
                subject1 = "Your Changes Profiles are Rejected";

                Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}", Environment.NewLine, "" + FName1 + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Your Changes Profiles are Regected Please check Profile Detail ", "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                ShowMailDataTo(Emailid1);

                if (EmailHR == "True")
                {
                    subject1 = "Profiles are Rejected of  " + FName1;
                    string hrt = "HR Manager";
                    Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}", Environment.NewLine, "" + hrt + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Profiles are Regected ", "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                    ShowMailDataTo(HREmailidrej);

                }
                if (EmailHOD == "True")
                {
                    subject1 = "Profiles are Rejected of  " + FName1;
                    
                    Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}", Environment.NewLine, "" +HODNamerej + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Profiles are Regected ", "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                    ShowMailDataTo(HODEmailIDrej);

                }
            }
            if (PriorityHODapr == "1")
            {
                if (Session["UserType"].ToString() == "2")
                {
                    con.Update_tble_ProfileApprovalStatus_RejectedByHR(txtRemarksRejected.Text, "Yes", System.DateTime.Now.ToString("dd/MM/yyyy"), chkUSerIDRejected, profilechangedaterejected, lblnoofchangeReject1, Session["Company"].ToString());
                    con.DisConnect();
                    subject1 = "Your Changes Profiles are Rejected";

                    Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}", Environment.NewLine, "" + FName1 + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Your Changes Profiles are Regected Please check Profile Detail ", "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                    ShowMailDataTo(Emailid1);

                    if (EmailHR == "True")
                    {
                        subject1 = "Profiles are Rejected of  " + FName1;
                        string hrt = "HR Manager";
                        Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}", Environment.NewLine, "" + hrt + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Profiles are Regected ", "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                        ShowMailDataTo(HREmailidrej);

                    }

                }
                else
                {
                    con.Update_tble_ProfileApprovalStatus_Rejected(txtRemarksRejected.Text, "Yes", System.DateTime.Now.ToString("dd/MM/yyyy"), chkUSerIDRejected, profilechangedaterejected, lblnoofchangeReject1, Session["Company"].ToString());
                    con.DisConnect();
                    subject1 = "Your Changes Profiles are Rejected";

                    Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}", Environment.NewLine, "" + FName1 + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Your Changes Profiles are Regected Please check Profile Detail ", "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                    ShowMailDataTo(Emailid1);

                    if (EmailHR == "True")
                    {
                        subject1 = "Profiles are Rejected of  " + FName1;
                        string hrt = "HR Manager";
                        Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}", Environment.NewLine, "" + hrt + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Profiles are Regected ", "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                        ShowMailDataTo(HREmailidrej);

                    }
                }
            }

            if (PriorityHRapr == "1")
            {
                if (Session["UserType"].ToString() == "2")
                {
                    con.Update_tble_ProfileApprovalStatus_RejectedByHR(txtRemarksRejected.Text, "Yes", System.DateTime.Now.ToString("dd/MM/yyyy"), chkUSerIDRejected, profilechangedaterejected, lblnoofchangeReject1, Session["Company"].ToString());
                    con.DisConnect();
                    subject1 = "Your Changes Profiles are Rejected";

                    Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}", Environment.NewLine, "" + FName1 + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Your Changes Profiles are Regected Please check Profile Detail ", "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                    ShowMailDataTo(Emailid1);

                    if (EmailHOD == "True")
                    {
                        subject1 = "Profiles are Rejected of  " + FName1;
                       
                        Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}", Environment.NewLine, "" + HODNamerej + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Profiles are Regected ", "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                        ShowMailDataTo(HODEmailIDrej);

                    }

                }
                else
                {
                    con.Update_tble_ProfileApprovalStatus_Rejected(txtRemarksRejected.Text, "Yes", System.DateTime.Now.ToString("dd/MM/yyyy"), chkUSerIDRejected, profilechangedaterejected, lblnoofchangeReject1, Session["Company"].ToString());
                    con.DisConnect();
                    subject1 = "Your Changes Profiles are Rejected";

                    Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}", Environment.NewLine, "" + FName1 + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Your Changes Profiles are Regected Please check Profile Detail ", "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                    ShowMailDataTo(Emailid1);

                    if (EmailHOD == "True")
                    {
                        subject1 = "Profiles are Rejected of  " + FName1;

                        Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}", Environment.NewLine, "" + HODNamerej + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Profiles are Regected ", "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                        ShowMailDataTo(HODEmailIDrej);

                    }
                }
            }


        }
        drall.Close();
        con.DisConnect();
    }


    protected void btnApprove_Click(object sender, EventArgs e)
    {

        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {

            foreach (GridViewRow row in grdViewApproval.Rows)
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
            profileApproval_Search();
        }
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
        profileApproval_Search();


    }

    public void profileRejected_Search()
    {
        if (rdProfileRectedDatewise.Checked == true)
        {
            //if (Session["UserType"].ToString() == "1")
            //{
            //    SqlDataReader odr = con.Profile_Approval_Reject_withdateBlank_ForAdmin(txtRejectedFromDate.Text, txtRejectedToDate.Text, Session["Company"].ToString());
            //    DataTable Dt = new DataTable();
            //    Dt.Load(odr);
            //    grdRejected.DataSource = Dt;
            //    grdRejected.DataBind();
            //    odr.Close();
            //    con.DisConnect();
            
            //}
            //else
            //{
                if (Blankapr == "1")
                {
                    SqlDataReader odr = con.Profile_Approval_Reject_withdateBlank(Session["uid"].ToString(), txtRejectedFromDate.Text, txtRejectedToDate.Text, Session["Company"].ToString());
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdRejected.DataSource = Dt;
                    grdRejected.DataBind();
                    odr.Close();
                    con.DisConnect();

                }
                if (PriorityHODapr == "1")
                {
                    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                    {
                        SqlDataReader odr = con.Profile_Approval_Reject_withdateHR(Session["uid"].ToString(), txtRejectedFromDate.Text, txtRejectedToDate.Text, Session["Company"].ToString());
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdRejected.DataSource = Dt;
                        grdRejected.DataBind();
                        odr.Close();
                        con.DisConnect();


                    }
                    else
                    {
                        SqlDataReader odr = con.Profile_Approval_Reject_withdate(Session["uid"].ToString(), txtRejectedFromDate.Text, txtRejectedToDate.Text, Session["Company"].ToString());
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdRejected.DataSource = Dt;
                        grdRejected.DataBind();
                        odr.Close();
                        con.DisConnect();

                    }
                }
                if (PriorityHRapr == "1")
                {
                    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                    {
                        SqlDataReader odr = con.Profile_Approval_Reject_withdateHR(Session["uid"].ToString(), txtRejectedFromDate.Text, txtRejectedToDate.Text, Session["Company"].ToString());
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdRejected.DataSource = Dt;
                        grdRejected.DataBind();
                        odr.Close();
                        con.DisConnect();
                    }
                    else
                    {
                        SqlDataReader odr = con.Profile_Approval_Reject_withdate(Session["uid"].ToString(), txtRejectedFromDate.Text, txtRejectedToDate.Text, Session["Company"].ToString());
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdRejected.DataSource = Dt;
                        grdRejected.DataBind();
                        odr.Close();
                        con.DisConnect();


                    }
                }

           // }

        }

        if (rdProfileRectedEMPID.Checked == true)
        {
            //if (Session["UserType"].ToString() == "1")
            //{
            //    SqlDataReader odr = con.Profile_Approval_Rejected_withUserIDforAdmin(Session["Company"].ToString(), txtRejectedSearch.Text);
            //    DataTable Dt = new DataTable();
            //    Dt.Load(odr);
            //    grdRejected.DataSource = Dt;
            //    grdRejected.DataBind();
            //    odr.Close();
            //    con.DisConnect();
            //}
            //else
            //{

                if (Blankapr == "1")
                {
                    SqlDataReader odr = con.Profile_Approval_Rejected_withUserIDBlank(Session["uid"].ToString(), Session["Company"].ToString(), txtRejectedSearch.Text);
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdRejected.DataSource = Dt;
                    grdRejected.DataBind();
                    odr.Close();
                    con.DisConnect();

                }
                if (PriorityHODapr == "1")
                {
                    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                    {
                        SqlDataReader odr = con.Profile_Approval_Rejected_withUserIDHR(Session["uid"].ToString(), Session["Company"].ToString(), txtRejectedSearch.Text);
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdRejected.DataSource = Dt;
                        grdRejected.DataBind();
                        odr.Close();
                        con.DisConnect();
                    }
                    else
                    {
                        SqlDataReader odr = con.Profile_Approval_Rejected_withUserID(Session["uid"].ToString(), Session["Company"].ToString(), txtRejectedSearch.Text);
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdRejected.DataSource = Dt;
                        grdRejected.DataBind();
                        odr.Close();
                        con.DisConnect();

                    }
                }
                if (PriorityHRapr == "1")
                {
                    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                    {
                        SqlDataReader odr = con.Profile_Approval_Rejected_withUserIDHR(Session["uid"].ToString(), Session["Company"].ToString(), txtRejectedSearch.Text);
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdRejected.DataSource = Dt;
                        grdRejected.DataBind();
                        odr.Close();
                        con.DisConnect();
                    }
                    else
                    {
                        SqlDataReader odr = con.Profile_Approval_Rejected_withUserID(Session["uid"].ToString(), Session["Company"].ToString(), txtRejectedSearch.Text);
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdRejected.DataSource = Dt;
                        grdRejected.DataBind();
                        odr.Close();
                        con.DisConnect();
                    }
               // }
            }
        }

        if (rdrdProfileRectedName.Checked == true)
        {

            //if (Session["UserType"].ToString() == "1")
            //{
            //    SqlDataReader odr = con.Profile_Approval_rejected_withUserNameBlank_ForAdmin(Session["Company"].ToString(), txtRejectedSearch.Text);
            //    DataTable Dt = new DataTable();
            //    Dt.Load(odr);
            //    grdRejected.DataSource = Dt;
            //    grdRejected.DataBind();
            //    odr.Close();
            //    con.DisConnect();

            //}
            //else
            //{

                if (Blankapr == "1")
                {
                    SqlDataReader odr = con.Profile_Approval_rejected_withUserNameBlank(Session["uid"].ToString(), Session["Company"].ToString(), txtRejectedSearch.Text);
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdRejected.DataSource = Dt;
                    grdRejected.DataBind();
                    odr.Close();
                    con.DisConnect();
                }
                if (PriorityHODapr == "1")
                {
                    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                    {
                        SqlDataReader odr = con.Profile_Approval_rejected_withUserNameHR(Session["uid"].ToString(), Session["Company"].ToString(), txtRejectedSearch.Text);
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdRejected.DataSource = Dt;
                        grdRejected.DataBind();
                        odr.Close();
                        con.DisConnect();
                    }
                    else
                    {
                        SqlDataReader odr = con.Profile_Approval_rejected_withUserName(Session["uid"].ToString(), Session["Company"].ToString(), txtRejectedSearch.Text);
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdRejected.DataSource = Dt;
                        grdRejected.DataBind();
                        odr.Close();
                        con.DisConnect();
                    }
                }

                if (PriorityHRapr == "1")
                {
                    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                    {
                        SqlDataReader odr = con.Profile_Approval_rejected_withUserNameHR(Session["uid"].ToString(), Session["Company"].ToString(), txtRejectedSearch.Text);
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdRejected.DataSource = Dt;
                        grdRejected.DataBind();
                        odr.Close();
                        con.DisConnect();
                    }
                    else
                    {
                        SqlDataReader odr = con.Profile_Approval_rejected_withUserName(Session["uid"].ToString(), Session["Company"].ToString(), txtRejectedSearch.Text);
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdRejected.DataSource = Dt;
                        grdRejected.DataBind();
                        odr.Close();
                        con.DisConnect();
                    }
               // }

            }

            }
            if (rdAllReject.Checked == true)
            {

                //if (Session["UserType"].ToString() == "1")
                //{
                //    SqlDataReader odr = con.Profile_Approval_rejected_For_Admins( Session["Company"].ToString());
                //    DataTable Dt = new DataTable();
                //    Dt.Load(odr);
                //    grdRejected.DataSource = Dt;
                //    grdRejected.DataBind();
                //    odr.Close();
                //    con.DisConnect();

                //}
                //else
                //{
                    if (Blankapr == "1")
                    {
                        SqlDataReader odr = con.Profile_Approval_rejected_withUserNameBlankALLReject(Session["uid"].ToString(), Session["Company"].ToString());
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdRejected.DataSource = Dt;
                        grdRejected.DataBind();
                        odr.Close();
                        con.DisConnect();
                    }
                    if (PriorityHODapr == "1")
                    {
                        if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                        {
                            SqlDataReader odr = con.Profile_Approval_rejected_withUserNameHRAllReject(Session["uid"].ToString(), Session["Company"].ToString());
                            DataTable Dt = new DataTable();
                            Dt.Load(odr);
                            grdRejected.DataSource = Dt;
                            grdRejected.DataBind();
                            odr.Close();
                            con.DisConnect();
                        }
                        else
                        {
                            SqlDataReader odr = con.Profile_Approval_rejected_withUserNameAllReject(Session["uid"].ToString(), Session["Company"].ToString());
                            DataTable Dt = new DataTable();
                            Dt.Load(odr);
                            grdRejected.DataSource = Dt;
                            grdRejected.DataBind();
                            odr.Close();
                            con.DisConnect();
                        }
                    }

                    if (PriorityHRapr == "1")
                    {
                        if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                        {
                            SqlDataReader odr = con.Profile_Approval_rejected_withUserNameHRAllReject(Session["uid"].ToString(), Session["Company"].ToString());
                            DataTable Dt = new DataTable();
                            Dt.Load(odr);
                            grdRejected.DataSource = Dt;
                            grdRejected.DataBind();
                            odr.Close();
                            con.DisConnect();
                        }
                        else
                        {
                            SqlDataReader odr = con.Profile_Approval_rejected_withUserNameAllReject(Session["uid"].ToString(), Session["Company"].ToString());
                            DataTable Dt = new DataTable();
                            Dt.Load(odr);
                            grdRejected.DataSource = Dt;
                            grdRejected.DataBind();
                            odr.Close();
                            con.DisConnect();
                        }
                    }


                
         //   }
        }


    }


    public void profileResolved_Search()
    {
        if (rdApprovedDatewise.Checked == true)
        {

            //if (Session["UserType"].ToString() == "1")
            //{
            //    SqlDataReader odr = con.Profile_Approval_Resolved_withdateByUser_FOradmin(txtApprovedFromDate.Text, txtApprovedToDate.Text, Session["Company"].ToString());
            //    DataTable Dt = new DataTable();
            //    Dt.Load(odr);
            //    grdResolved.DataSource = Dt;
            //    grdResolved.DataBind();
            //    odr.Close();
            //    con.DisConnect();
            //}
            //else
            //{

                if (Blankapr == "1")
                {
                    SqlDataReader odr = con.Profile_Approval_Resolved_withdateByUser(Session["uid"].ToString(), txtApprovedFromDate.Text, txtApprovedToDate.Text, Session["Company"].ToString());
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdResolved.DataSource = Dt;
                    grdResolved.DataBind();
                    odr.Close();
                    con.DisConnect();
                }
                if (PriorityHODapr == "1")
                {
                    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                    {
                        SqlDataReader odr = con.Profile_Approval_Resolved_withdateByHR(Session["uid"].ToString(), txtApprovedFromDate.Text, txtApprovedToDate.Text, Session["Company"].ToString());
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdResolved.DataSource = Dt;
                        grdResolved.DataBind();
                        odr.Close();
                        con.DisConnect();
                    }
                    else
                    {
                        SqlDataReader odr = con.Profile_Approval_Resolved_withdate(Session["uid"].ToString(), txtApprovedFromDate.Text, txtApprovedToDate.Text, Session["Company"].ToString());
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdResolved.DataSource = Dt;
                        grdResolved.DataBind();
                        odr.Close();
                        con.DisConnect();

                    }
                }
                if (PriorityHRapr == "1")
                {
                    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                    {
                        SqlDataReader odr = con.Profile_Approval_Resolved_withdateByHR(Session["uid"].ToString(), txtApprovedFromDate.Text, txtApprovedToDate.Text, Session["Company"].ToString());
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdResolved.DataSource = Dt;
                        grdResolved.DataBind();
                        odr.Close();
                        con.DisConnect();
                    }
                    else
                    {
                        SqlDataReader odr = con.Profile_Approval_Resolved_withdate(Session["uid"].ToString(), txtApprovedFromDate.Text, txtApprovedToDate.Text, Session["Company"].ToString());
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdResolved.DataSource = Dt;
                        grdResolved.DataBind();
                        odr.Close();
                        con.DisConnect();
                    }
                }

           // }

        }

        if (rdApprovedEmpid.Checked == true)
        {
            //if (Session["UserType"].ToString() == "1")
            //{
            //    SqlDataReader odr = con.Profile_Approval_Resolved_withUserIDByUserforadmin(Session["Company"].ToString(), txtResolvedSearch.Text);
            //    DataTable Dt = new DataTable();
            //    Dt.Load(odr);
            //    grdResolved.DataSource = Dt;
            //    grdResolved.DataBind();
            //    odr.Close();
            //    con.DisConnect();

            //}
            //else
            //{

                if (Blankapr == "1")
                {
                    SqlDataReader odr = con.Profile_Approval_Resolved_withUserIDByUser(Session["uid"].ToString(), Session["Company"].ToString(), txtResolvedSearch.Text);
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdResolved.DataSource = Dt;
                    grdResolved.DataBind();
                    odr.Close();
                    con.DisConnect();
                }
                if (PriorityHODapr == "1")
                {
                    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                    {
                        SqlDataReader odr = con.Profile_Approval_Resolved_withUseIDbyHR(Session["uid"].ToString(), Session["Company"].ToString(), txtResolvedSearch.Text);
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdResolved.DataSource = Dt;
                        grdResolved.DataBind();
                        odr.Close();
                        con.DisConnect();
                    }
                    else
                    {
                        SqlDataReader odr = con.Profile_Approval_Resolved_withUserID(Session["uid"].ToString(), Session["Company"].ToString(), txtResolvedSearch.Text);
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdResolved.DataSource = Dt;
                        grdResolved.DataBind();
                        odr.Close();
                        con.DisConnect();

                    }
                }

                if (PriorityHRapr == "1")
                {
                    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                    {
                        SqlDataReader odr = con.Profile_Approval_Resolved_withUseIDbyHR(Session["uid"].ToString(), Session["Company"].ToString(), txtResolvedSearch.Text);
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdResolved.DataSource = Dt;
                        grdResolved.DataBind();
                        odr.Close();
                        con.DisConnect();
                    }
                    else
                    {
                        SqlDataReader odr = con.Profile_Approval_Resolved_withUserID(Session["uid"].ToString(), Session["Company"].ToString(), txtResolvedSearch.Text);
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdResolved.DataSource = Dt;
                        grdResolved.DataBind();
                        odr.Close();
                        con.DisConnect();

                    }
                }
           // }
        }

        if (rdApprovedEMPName.Checked == true)
        {
            //if (Session["UserType"].ToString() == "1")
            //{
            //    SqlDataReader odr = con.Profile_Approval_Resolved_withUserNameByUserForAdmin(Session["Company"].ToString(), txtResolvedSearch.Text);
            //    DataTable Dt = new DataTable();
            //    Dt.Load(odr);
            //    grdResolved.DataSource = Dt;
            //    grdResolved.DataBind();
            //    odr.Close();
            //    con.DisConnect();

            //}
            //else
            //{
                if (Blankapr == "1")
                {
                    SqlDataReader odr = con.Profile_Approval_Resolved_withUserNameByUser(Session["uid"].ToString(), Session["Company"].ToString(), txtResolvedSearch.Text);
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdResolved.DataSource = Dt;
                    grdResolved.DataBind();
                    odr.Close();
                    con.DisConnect();
                }
                if (PriorityHODapr == "1")
                {
                    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                    {
                        SqlDataReader odr = con.Profile_Approval_Resolved_withUserNameByHR(Session["uid"].ToString(), Session["Company"].ToString(), txtResolvedSearch.Text);
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdResolved.DataSource = Dt;
                        grdResolved.DataBind();
                        odr.Close();
                        con.DisConnect();
                    }
                    else
                    {
                        SqlDataReader odr = con.Profile_Approval_Resolved_withUserName(Session["uid"].ToString(), Session["Company"].ToString(), txtResolvedSearch.Text);
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdResolved.DataSource = Dt;
                        grdResolved.DataBind();
                        odr.Close();
                        con.DisConnect();

                    }
                }

                if (PriorityHRapr == "1")
                {
                    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                    {
                        SqlDataReader odr = con.Profile_Approval_Resolved_withUserNameByHR(Session["uid"].ToString(), Session["Company"].ToString(), txtResolvedSearch.Text);
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdResolved.DataSource = Dt;
                        grdResolved.DataBind();
                        odr.Close();
                        con.DisConnect();
                    }
                    else
                    {
                        SqlDataReader odr = con.Profile_Approval_Resolved_withUserName(Session["uid"].ToString(), Session["Company"].ToString(), txtResolvedSearch.Text);
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdResolved.DataSource = Dt;
                        grdResolved.DataBind();
                        odr.Close();
                        con.DisConnect();
                    }
              //  }
            }
        }
        if (rdAllApprove.Checked == true)
        {
            //if (Session["UserType"].ToString() == "1")
            //{
            //    SqlDataReader odr = con.Profile_Approval_Resolved_withUserNameByUserAllforAdmin(Session["Company"].ToString());
            //    DataTable Dt = new DataTable();
            //    Dt.Load(odr);
            //    grdResolved.DataSource = Dt;
            //    grdResolved.DataBind();
            //    odr.Close();
            //    con.DisConnect();

            //}
            //else
         //   {
                if (Blankapr == "1")
                {
                    SqlDataReader odr = con.Profile_Approval_Resolved_withUserNameByUserAll(Session["uid"].ToString(), Session["Company"].ToString());
                    DataTable Dt = new DataTable();
                    Dt.Load(odr);
                    grdResolved.DataSource = Dt;
                    grdResolved.DataBind();
                    odr.Close();
                    con.DisConnect();
                }
                if (PriorityHODapr == "1")
                {
                    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                    {
                        SqlDataReader odr = con.Profile_Approval_Resolved_withUserNameByHRAll(Session["uid"].ToString(), Session["Company"].ToString());
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdResolved.DataSource = Dt;
                        grdResolved.DataBind();
                        odr.Close();
                        con.DisConnect();
                    }
                    else
                    {
                        SqlDataReader odr = con.Profile_Approval_Resolved_withUserNameAll(Session["uid"].ToString(), Session["Company"].ToString());
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdResolved.DataSource = Dt;
                        grdResolved.DataBind();
                        odr.Close();
                        con.DisConnect();

                    }
                }

                if (PriorityHRapr == "1")
                {
                    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
                    {
                        SqlDataReader odr = con.Profile_Approval_Resolved_withUserNameByHRAll(Session["uid"].ToString(), Session["Company"].ToString());
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdResolved.DataSource = Dt;
                        grdResolved.DataBind();
                        odr.Close();
                        con.DisConnect();
                    }
                    else
                    {
                        SqlDataReader odr = con.Profile_Approval_Resolved_withUserNameAll(Session["uid"].ToString(), Session["Company"].ToString());
                        DataTable Dt = new DataTable();
                        Dt.Load(odr);
                        grdResolved.DataSource = Dt;
                        grdResolved.DataBind();
                        odr.Close();
                        con.DisConnect();
                    }
                }

           // }
        }


    }

    protected void btnrejectedsearch_Click(object sender, EventArgs e)
    {
        grdRejected.Visible = true;
        profileRejected_Search();
    }
    protected void btnApprovedSearch_Click(object sender, EventArgs e)
    {
        grdResolved.Visible = true;
        profileResolved_Search();
    }
    protected void grdViewApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdViewApproval.PageIndex = e.NewPageIndex;
        profileApproval_Search();
    }
    protected void grdResolved_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdResolved.PageIndex = e.NewPageIndex;
        profileResolved_Search();
    }
    protected void grdRejected_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdRejected.PageIndex = e.NewPageIndex;
        profileRejected_Search();
    }
    protected void grdViewApproval_SelectedIndexChanged(object sender, EventArgs e)
    {

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
        profileApproval_Search();

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
        profileRejected_Search();

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
        profileResolved_Search();
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
    public void FindChangeStatusNew()
    {
        SqlDataAdapter da = new SqlDataAdapter("select * from tble_ProfileApprovalStatus where UserID='" + chgsUserid + "' and CompanyName='" + Session["Company"].ToString() + "' and ProfileUpdateDate ='" + chgsProfilechangeDate + "' and Noofchange='" + chgsNoOfchanges.ToString() + "'", con.Con);
        DataSet ds = new DataSet();
        da.Fill(ds, "tble_ProfileApprovalStatus");
        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
        {


            lblNewAddress.Text = ds.Tables[0].Rows[i]["Address"].ToString();

            lblNewAddress1.Text = ds.Tables[0].Rows[i]["Address2"].ToString();
            lblNewCity.Text = ds.Tables[0].Rows[i]["City"].ToString();
            lblNewState.Text = ds.Tables[0].Rows[i]["State"].ToString();
            lblNewCountry.Text = ds.Tables[0].Rows[i]["Country"].ToString();
            lblNewPincode.Text = ds.Tables[0].Rows[i]["Pin_Code"].ToString();
            lblNewEmailID.Text = ds.Tables[0].Rows[i]["Email_ID"].ToString();

            lblNewMaritalStatus.Text = ds.Tables[0].Rows[i]["Marital_Status"].ToString();
            lblNewPhoneNo.Text = ds.Tables[0].Rows[i]["Phone_No"].ToString();
            lblNewContactNo.Text = ds.Tables[0].Rows[i]["Mobile_No"].ToString();
            lblNewDateofBirth.Text = ds.Tables[0].Rows[i]["DOB"].ToString();
            lblNewFatherName.Text = ds.Tables[0].Rows[i]["FatherName"].ToString();
            lblNewHusbandName.Text = ds.Tables[0].Rows[i]["HusbandName"].ToString();
            lblNewMotherName.Text = ds.Tables[0].Rows[i]["MotherName"].ToString();
            lblNewAddressPERMAnent.Text = ds.Tables[0].Rows[i]["PAddress"].ToString();
            lblNewAddressPERMAnent2.Text = ds.Tables[0].Rows[i]["PAddress2"].ToString();
            lblNewCitypermanet.Text = ds.Tables[0].Rows[i]["PCity"].ToString();
            lblNewStatePermanet.Text = ds.Tables[0].Rows[i]["PState"].ToString();
            lblNewCitypermanet.Text = ds.Tables[0].Rows[i]["PCountry"].ToString();
            lblnewPincodepermanet.Text = ds.Tables[0].Rows[i]["PPinCode"].ToString();
            lblNewPanNo.Text = ds.Tables[0].Rows[i]["PanNo"].ToString();
            lblNewACNO.Text = ds.Tables[0].Rows[i]["Ac_No"].ToString();
            lblNewPanNo.Text = ds.Tables[0].Rows[i]["PanNo"].ToString();
            lblchangestatusmodel.Text = ds.Tables[0].Rows[i]["Change_Status"].ToString();



        }
        con.DisConnect();
    }



    public void FindChangeStatusOLd(string compnayName)
    {
        SqlDataAdapter da = new SqlDataAdapter("select * from " + compnayName + "  where No_='" + chgsUserid + "' ", Portalcon.Con);
        DataSet ds = new DataSet();
        da.Fill(ds, "Employee");
        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
        {


            lblOldAddress.Text = ds.Tables[0].Rows[i]["Address"].ToString();

            lblOldAddress1.Text = ds.Tables[0].Rows[i]["Address 2"].ToString();
            lblOldCity.Text = ds.Tables[0].Rows[i]["City"].ToString();
            lblOldState.Text = ds.Tables[0].Rows[i]["State"].ToString();
            lblOldCountry.Text = ds.Tables[0].Rows[i]["County"].ToString();
            lblOldPincode.Text = ds.Tables[0].Rows[i]["Post Code"].ToString();
            lblOldEmailID.Text = ds.Tables[0].Rows[i]["E-Mail"].ToString();

            lblOldMaritalStatus.Text = ds.Tables[0].Rows[i]["Marital Status"].ToString();
            lblOldPhoneNo.Text = ds.Tables[0].Rows[i]["Phone No_"].ToString();
            lblOLDContactNo.Text = ds.Tables[0].Rows[i]["Mobile Phone No_"].ToString();
            lblOldDateOfBirth.Text = ds.Tables[0].Rows[i]["Birth Date"].ToString();
            lblOldFatherName.Text = ds.Tables[0].Rows[i]["Father Name"].ToString();
            lblOldHusbandName.Text = ds.Tables[0].Rows[i]["Husband Name"].ToString();
            // lblOldMotherName.Text = ds.Tables[0].Rows[i]["MotherName"].ToString();
            lblOldAddressPERMAnent.Text = ds.Tables[0].Rows[i]["Permanent Address1"].ToString();
            lblOldAddressPERMAnent2.Text = ds.Tables[0].Rows[i]["Permanent Address2"].ToString();
            lblOldCitypermanet.Text = ds.Tables[0].Rows[i]["Permanent City"].ToString();
            lblOldStatePermanet.Text = ds.Tables[0].Rows[i]["Permanent State"].ToString();
            lblOldCitypermanet.Text = ds.Tables[0].Rows[i]["County"].ToString();
            // lblOldPincodepermanet.Text = ds.Tables[0].Rows[i]["PPinCode"].ToString();
            lblOldPanNo.Text = ds.Tables[0].Rows[i]["PAN No"].ToString();
            lblOldACNo.Text = ds.Tables[0].Rows[i]["Account No"].ToString();




        }
        Portalcon.DisConnect();
    }



    protected void grdViewApproval_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow Row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);


        Label lblProfilechangedate1 = (Label)Row.FindControl("lblProfilechangedate");

         Session["chgsProfilechangeDate"] = lblProfilechangedate1.Text;

        Label lblNoofchange = (Label)Row.FindControl("lblNoofchange");
        Session["chgsNoOfchanges"]= lblNoofchange.Text;
      

        Page.ClientScript.RegisterStartupScript(
  this.GetType(), "OpenWindow", "window.open('changeStatus.aspx','_newtab','toolbar=no, scrollbars=yes, resizable=no');", true);
    }
    protected void btnchangestusrejected_Command(object sender, CommandEventArgs e)
    {
        Session["chgsUserid"] = e.CommandArgument.ToString();
    }
    protected void grdRejected_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow Row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);


        Label lblProfilechangedate1 = (Label)Row.FindControl("lblProfilechangedate");

        Session["chgsProfilechangeDate"] = lblProfilechangedate1.Text;

        Label lblNoofchange = (Label)Row.FindControl("lblNoofchange");
        Session["chgsNoOfchanges"] = lblNoofchange.Text;


        Page.ClientScript.RegisterStartupScript(
  this.GetType(), "OpenWindow", "window.open('changeStatus.aspx','_newtab','toolbar=no, scrollbars=yes, resizable=no');", true);
    }
    protected void btnresolved_Command(object sender, CommandEventArgs e)
    {
        Session["chgsUserid"] = e.CommandArgument.ToString();
    }
    protected void grdResolved_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow Row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);


        Label lblProfilechangedate1 = (Label)Row.FindControl("lblProfilechangedate");

        Session["chgsProfilechangeDate"] = lblProfilechangedate1.Text;

        Label lblNoofchange = (Label)Row.FindControl("lblNoofchange");
        Session["chgsNoOfchanges"] = lblNoofchange.Text;


        Page.ClientScript.RegisterStartupScript(
  this.GetType(), "OpenWindow", "window.open('changeStatus.aspx','_newtab','toolbar=no, scrollbars=yes, resizable=no');", true);
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
}