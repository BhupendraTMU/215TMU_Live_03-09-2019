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


public partial class Enquiry_Phone : System.Web.UI.Page
{
    DL.CommonDL commonDl = new CommonDL();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {            

            if (Session["uid"].ToString() != null)
            {
                if (Session["UserGroup"].ToString() == "ADMIN" || Session["UserGroup"].ToString() == "REGISTRAR")
                {
                    Session["UserGroup"] = Session["UserGroup"].ToString();
                }
                else if (Session["UserGroup"].ToString() != "COUNSELLOR" )
                {
                    Response.Redirect("../Default.aspx");
                }
            }
            else
            {
                Response.Redirect("../Default.aspx");
            }
        }
        catch { Response.Redirect("../Default.aspx"); }
        if (!IsPostBack)
        {
            BindDdlSource();
            BindDdlCourse();
            BindDdlCity();
            BindDdlReligion();
            BindDdlCategory();
            BindDdlSubReligion();
            
        }
    }
    public Boolean Validate()
    {
        Boolean Val = false;
        if (txtName.Text == "") { Val = true; }               
        else if (drpCourse.SelectedIndex == 0) { Val = true; }               
        else if (txtContactNo.Text == "") { Val = true; }
        else if (ddlCity.SelectedIndex == 0) { Val = true; }               
        return Val;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Boolean v = true; lblMandatoryMsg.Text = "";
        if (v == Validate())
        {
            lblMandatoryMsg.Text = "Please Fill Atleast Highlighted fields !";
            return;
        }
        //----------------Duplicate---------Start
        string Result = commonDl.addApplicantInformation(txtContactNo.Text, drpCourse.SelectedValue.ToString());
        if (Result != "")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", @"alert('Record all ready Exist with Enquiry( " + Result + " ) ! ')", true);
            return;
        }
        //----------------Duplicate-----End
        string No = "";        
        EnquiryDL objEnquiryDL = new EnquiryDL();
        No = objEnquiryDL.GetNextEnquiryNo();
        txtContactNo.Text = "+91" + txtContactNo.Text;
        commonDl.addApplicantInformation(No, txtName.Text.ToUpper(), txtAddress.Text, txtContactNo.Text, drpCourse.SelectedValue.ToString(), drpSource.SelectedValue.ToString(),
            drpCategory.SelectedValue.ToString(), drpReligion.SelectedValue.ToString(), Convert.ToInt16(drpSubReligion.SelectedValue.ToString()), txtRemarks.Text, ddlCity.SelectedValue.ToString(),
            Session["uid"].ToString());
        Blank();
       ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", @"alert('Enquiry( "+No+" ) Saved Successfully ! ')", true);
       
    }

   

    public void Blank()
    {
        txtName.Text = "";
        txtContactNo.Text = "";
        txtAddress.Text = "";
        txtRemarks.Text = "";
        drpSource.SelectedIndex = 0;
        drpCourse.SelectedIndex = 0;
        ddlCity.SelectedIndex = 0;
        drpCategory.SelectedIndex = 0;
        drpReligion.SelectedIndex = 0;
        drpSubReligion.SelectedIndex = 0;
    }
    public void BindDdlSource()
    {
        DataTable dt = new DataTable();
        EnquiryDL objEnquiryDL = new EnquiryDL();
        dt = objEnquiryDL.GetSourceDdl();
        drpSource.DataSource = dt;
        drpSource.DataTextField = "Details";
        drpSource.DataValueField = "No_";
        drpSource.DataBind();

    }
    public void BindDdlCourse()
    {
        DataTable dt = new DataTable();
        EnquiryDL objEnquiryDL = new EnquiryDL();
        if (Session["uid"].ToString().ToString() != null)
        {
            string UserId = Session["uid"].ToString().ToString();
            dt = objEnquiryDL.GetCourseDdl(UserId);
        }
        else
        {
            dt = objEnquiryDL.GetCourseDdl(); 
        }
        
        drpCourse.DataSource = dt;
        drpCourse.DataTextField = "Details";
        drpCourse.DataValueField = "No_";
        drpCourse.DataBind();

    }
    public void BindDdlReligion()
    {
        DataTable dt = new DataTable();
        EnquiryDL objEnquiryDL = new EnquiryDL();
        dt = objEnquiryDL.GetReligionDdl();
        drpReligion.DataSource = dt;
        drpReligion.DataTextField = "Details";
        drpReligion.DataValueField = "No_";
        drpReligion.DataBind();

    }
    public void BindDdlSubReligion()
    {
        DataTable dt = new DataTable();
        EnquiryDL objEnquiryDL = new EnquiryDL();
        dt = objEnquiryDL.GetSubReligionDdl();
        drpSubReligion.DataSource = dt;
        drpSubReligion.DataTextField = "Details";
        drpSubReligion.DataValueField = "No_";
        drpSubReligion.DataBind();

    }
    public void BindDdlCategory()
    {
        DataTable dt = new DataTable();
        EnquiryDL objEnquiryDL = new EnquiryDL();
        dt = objEnquiryDL.GetCategoryDdl(drpReligion.SelectedValue);
        drpCategory.DataSource = dt;
        drpCategory.DataTextField = "Details";
        drpCategory.DataValueField = "No_";
        drpCategory.DataBind();

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
    protected void drpReligion_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDdlCategory();
        if (drpReligion.SelectedValue == "JAIN")
        {
            drpSubReligion.Visible = true;
            lblSubReligionNotRequired.Visible = false;
        }
        else
        {
            drpSubReligion.Visible = false;
            lblSubReligionNotRequired.Visible = true;
        }
    }

    
}