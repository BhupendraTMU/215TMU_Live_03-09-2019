using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;


public partial class IndexMasterM : System.Web.UI.MasterPage
{
   
    string HRHODsame = "";
    string forHRisHOD = "";
    protected void Page_Load(object sender, EventArgs e)
    {
    //    try
    //    {
    //        if (Session["uname"].ToString() == null || Session["Company"].ToString() == null)
    //        {

    //            Response.Redirect("Default.aspx");
    //        }
    //        else
    //        {
               
    //            lbluserid666.Text = Session["uid"].ToString();
    //            lblName.Text = Session["uname"].ToString();
    //            //imgProfile.ImageUrl = Session["ProfilePhoto"].ToString();
    //          //  imgProfile.ImageUrl = Session["prfileimgeGmail"].ToString();
    //            //imgProfile.ImageUrl = Session["pictureimg"].ToString();
    //           // showHRHODisexhist();
    //            if (Session["uid"].ToString() == Session["HRID"].ToString())
    //            {
    //                HRHODsame = "HRHOD";
                  
    //            }
    //            if (Session["uid"].ToString() != Session["HRID"].ToString())
    //            {

    //                HRHODsame = "HR";
                  
    //            }
    //            if (Session["uid"].ToString() == Session["HODLoginPage"].ToString())
    //            {
    //                forHRisHOD = "HRHOD";
                   
    //            }
    //            if (Session["uid"].ToString() != Session["HODLoginPage"].ToString())
    //            {

    //                forHRisHOD = "HR";
                 
    //            }
               
    //            //if (Session["UserType"].ToString() == "1")
    //            //{
    //            //    profile_Pending_ApprovalForAdmin_Count();
    //            //    Attend_Pending_ApprovalUser_CountFor_Admin();
    //            //    Leave_Pending_ApprovalUser_Count_ForAdmin();
    //            //    Reimbursment_Pending_ApprovalUser_Count_ForAdmin();
    //            //}
    //            //else
    //            //{
    //                if (Session["UserType"].ToString() == "1")
    //                {
    //                    lnkSetup.Visible = true;
    //                }
    //                if (Session["UserType"].ToString() == "0" || Session["UserType"].ToString() == "2")
    //                {
    //                    lnkSetup.Visible = false;
    //                }
    //                lblCompanyName.Text = Session["Company"].ToString();
    //                Showpermission();
    //                ShowpermissionLeave();
    //                ShowpermissionReimb();
    //                ShowpermissionAttend();
    //                if (HODapr == "1" && HRapr == "0")
    //                {
    //                    profile_Pending_ApprovalHOD_Count();
    //                    Reimbursment_Pending_ApprovalHOD_Count();
    //                }
    //                if (HRapr == "1" && HODapr == "0")
    //                {
    //                    profile_Pending_ApprovalHR_Count();
    //                    Reimbursment_Pending_ApprovalHR_Count();
    //                }
    //                if (Blankapr == "1")
    //                {
    //                    profile_Pending_ApprovalUser_Count();
    //                    Reimbursment_Pending_ApprovalUser_Count();
    //                }
    //                if (HODapr == "1" && HRapr == "1")
    //                {
    //                    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
    //                    {
    //                        profile_Pending_ApprovalHR_Count();
    //                        Reimbursment_Pending_ApprovalHR_Count();
    //                    }
    //                    else
    //                    {
    //                        profile_Pending_ApprovalHOD_Count();
    //                        Reimbursment_Pending_ApprovalHOD_Count();
    //                    }
    //                }



    //                if (HODaprAttend == "1" && HRaprAttend == "0")
    //                {
    //                    Attendence_Pending_ApprovalHOD_Count();

    //                }
    //                if (HRaprAttend == "1" && HODaprAttend == "0")
    //                {
    //                    Attendece_Pending_ApprovalHR_Count();

    //                }
    //                if (BlankaprAttend == "1")
    //                {
    //                    Attend_Pending_ApprovalUser_Count();
    //                    //lnkpostAttendance.Visible = true;
    //                    lnkAttendanceApprovalHODHR.Visible = false;
    //                    lblAttendence.Visible = false;
    //                }
    //                if (PriorityHRaprAttend == "1" || PriorityHODaprAttend == "1")
    //                {
    //                    //lnkpostAttendance.Visible = false;
    //                    lnkAttendanceApprovalHODHR.Visible = true;
    //                    lblAttendence.Visible = true;
    //                }

    //                if (HODaprAttend == "1" && HRaprAttend == "1")
    //                {
                        
    //                    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR" )
    //                    {
    //                        Attendece_Pending_ApprovalHR_Count();
    //                    }
                      
    //                    else
    //                    {
    //                        Attendence_Pending_ApprovalHOD_Count();
    //                    }
    //                }





    //                if (HODaprReimb == "1" && HRaprReimb == "0")
    //                {

    //                    Reimbursment_Pending_ApprovalHOD_Count();
    //                }
    //                if (HRaprReimb == "1" && HODaprReimb == "0")
    //                {

    //                    Reimbursment_Pending_ApprovalHR_Count();
    //                }
    //                if (BlankaprReimb == "1")
    //                {

    //                    Reimbursment_Pending_ApprovalUser_Count();
    //                }
    //                if (HODaprReimb == "1" && HRaprReimb == "1")
    //                {
    //                    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
    //                    {

    //                        Reimbursment_Pending_ApprovalHR_Count();
    //                    }
    //                    else
    //                    {

    //                        Reimbursment_Pending_ApprovalHOD_Count();
    //                    }
    //                }



    //                if (HODaprLeave == "1" && HRaprLeave == "0")
    //                {
    //                    Leave_Pending_ApprovalHOD_Count();
    //                }



    //                if (HRaprLeave == "1" && HODaprLeave == "0")
    //                {
    //                    Leave_Pending_ApprovalHR_Count();

    //                }




    //                if (BlankaprLeave == "1")
    //                {
    //                    Leave_Pending_ApprovalUser_Count();

    //                }


    //                if (HODaprLeave == "1" && HRaprLeave == "1")
    //                {
    //                    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
    //                    {
    //                        Leave_Pending_ApprovalHR_Count();

    //                    }
    //                    else
    //                    {
    //                        Leave_Pending_ApprovalHOD_Count();

    //                    }
    //                }

    //            //}
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        Response.Redirect("Default.aspx");
    //    }

    }

   
}




