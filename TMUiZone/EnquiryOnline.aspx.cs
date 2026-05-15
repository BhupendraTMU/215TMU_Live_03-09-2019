using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DL;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class EnquiryOnline : System.Web.UI.Page
{
    DL.CommonDL commonDl = new CommonDL();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        { 
            BindDdlCourse();
            BindDdlCity();
        }
    }
    
    public void BindDdlCourse()
    {
        DataTable dt = new DataTable();
        EnquiryDL objEnquiryDL = new EnquiryDL();
        dt = objEnquiryDL.GetCourseDdl();
        drpCourse.DataSource = dt;
       // DataView dv = new DataView(dt);
        //dv.RowFilter = "No_ NOT IN ('BTC-002', 'BTC-003', 'BTC-004','BTC-005','BTC-006')";
        //drpCourse.DataSource = dv;        
        drpCourse.DataTextField = "Details";
        drpCourse.DataValueField = "No_";
        drpCourse.DataBind();

    }
    public void BindDdlCity()
    {
        DataTable dt = new DataTable();
        EnquiryDL objEnquiryDL = new EnquiryDL();
        dt = objEnquiryDL.GetCityDdl();
        ddlCity.DataSource = dt;
        ddlCity.DataTextField = "Details";
        ddlCity.DataValueField = "No_";
        ddlCity.DataBind();
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string Save(string ContactNo, string Course, string Name, string City, string Email)
    {
        DL.CommonDL commonDl = new CommonDL();
        //----------------Duplicate---------Start
        string Result = commonDl.addApplicantInformation(ContactNo, Course);
        if (Result != "")
        {
            return "Error";
        }
        //----------------Duplicate-----End
        string No = "";
        EnquiryDL objEnquiryDL = new EnquiryDL();
        No = objEnquiryDL.GetNextEnquiryNo();
        ContactNo = "+91" + ContactNo;
        //---changes on 05-06-2016--by Saurabh-Email ID- add email ID
        commonDl.RegisterYourInterest(No, Name.ToUpper(), "", ContactNo, Course, "", "", "", 0, "WEBSITE", City, "", Email);
        return "You Have Registered Successfully with Enquiry No : " + No + "  ! ";       
    }
}