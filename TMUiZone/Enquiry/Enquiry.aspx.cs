using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DL;
using PL;
using System.Data;
using System.Web.UI.HtmlControls;
public partial class Enquiry : System.Web.UI.Page
{
    static string UserName = string.Empty;
    static string EnquiryNo = string.Empty;
    DL.CommonDL commonDl = new CommonDL();
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
                else if (Session["UserGroup"].ToString() != "COUNSELLOR")
                {
                    Response.Redirect("../Default.aspx");
                }
            }
            else
            {
                Response.Redirect("../Default.aspx");
            }
        }
  catch { 
            Response.Redirect("../Default.aspx"); 
        }

            if (!IsPostBack)
            {
                cleEnquiryDate.EndDate = DateTime.Now.AddDays(0);
                cleDOB.EndDate = DateTime.Now.AddDays(0);
                txtEnquiryDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                BindDdlENquiry();
                BindDdlSession();
                BindDdlCourse();
                BindDdlCity();
                BindDdlSource();
                BindDdlGender();
                BindDdlNationality();
                BindDdlGetFeeType();
                BindDdlNameOfMedia();
                BindDdlEnquiryType();
                BindDdlPrequalification();
                BindDdlReligion();      //1          
                BindDdlCategory();      //2
                BindDdlSubReligion();   //3
                GetNextEnquiryNo();
                if (Request.QueryString["EnquiryNo"] != null)
                {
                    rblOldNew.SelectedValue = "Old Enquiry";
                    ddlEnquiryNo.SelectedValue = Request.QueryString["EnquiryNo"];
                    ddlEnquiryNo.Visible = false;
                    lblEnquiryNo.Visible = true;
                    lblEnquiryNo.Text = Request.QueryString["EnquiryNo"]; 
                    GetEnquiryDetails_ByEnquiryID(Request.QueryString["EnquiryNo"]);                   
                    hfOldNew.Value = "Old";
                    lblMsgmandatory.Text = "";
                    DisableForm(Page.Controls);
                }
            }
    }
    public void GetNextEnquiryNo()
    {
        EnquiryDL objEnquiryDL = new EnquiryDL();
        lblEnquiryNo.Text = objEnquiryDL.GetNextEnquiryNo();
    }
    public void BindDdlENquiry()
    {
        EnquiryDL objEnquiryDL = new EnquiryDL();
        List<EnquiryPL> objEnquiryList = new List<EnquiryPL>();
        if (Session["uid"].ToString().ToString() != null)
        {
            string UserId = Session["uid"].ToString().ToString();
            objEnquiryList = objEnquiryDL.GetEnquiryDdl(UserId);
        }
        else
        {
            objEnquiryList = objEnquiryDL.GetEnquiryDdl();
        }  
        ddlEnquiryNo.DataSource = objEnquiryList;
        ddlEnquiryNo.DataTextField = "Details";
        ddlEnquiryNo.DataValueField = "No_";
        ddlEnquiryNo.DataBind();

    }
    public void BindDdlSession()
    {
        DataTable dt = new DataTable();
        EnquiryDL objEnquiryDL = new EnquiryDL();
        dt = objEnquiryDL.GetSessionDdl();
        ddlSession.DataSource = dt;
        ddlSession.DataTextField = "Details";
        ddlSession.DataValueField = "No_";
        ddlSession.DataBind();

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
        ddlCourse.DataSource = dt;
        ddlCourse.DataTextField = "Details";
        ddlCourse.DataValueField = "No_";
        ddlCourse.DataBind();

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
    public void BindDdlCategory()
    {
        DataTable dt = new DataTable();
        EnquiryDL objEnquiryDL = new EnquiryDL();
        dt = objEnquiryDL.GetCategoryDdl(ddlReligion.SelectedValue);
        ddlCategory.DataSource = dt;
        ddlCategory.DataTextField = "Details";
        ddlCategory.DataValueField = "No_";
        ddlCategory.DataBind();

    }
    public void BindDdlSource()
    {
        DataTable dt = new DataTable();
        EnquiryDL objEnquiryDL = new EnquiryDL();
        dt = objEnquiryDL.GetSourceDdl();
        ddlEnquirySource.DataSource = dt;
        ddlEnquirySource.DataTextField = "Details";
        ddlEnquirySource.DataValueField = "No_";
        ddlEnquirySource.DataBind();

    }
    public void BindDdlGender()
    {
        DataTable dt = new DataTable();
        EnquiryDL objEnquiryDL = new EnquiryDL();
        dt = objEnquiryDL.GetGenderDdl();
        ddlGender.DataSource = dt;
        ddlGender.DataTextField = "Details";
        ddlGender.DataValueField = "No_";
        ddlGender.DataBind();

    }
    public void BindDdlNationality()
    {
        DataTable dt = new DataTable();
        EnquiryDL objEnquiryDL = new EnquiryDL();
        dt = objEnquiryDL.GetNationalityDdl();
        ddlNationality.DataSource = dt;
        ddlNationality.DataTextField = "Details";
        ddlNationality.DataValueField = "No_";
        ddlNationality.DataBind();

    }    
    public void BindDdlGetFeeType()
    {
        DataTable dt = new DataTable();
        EnquiryDL objEnquiryDL = new EnquiryDL();
        dt = objEnquiryDL.GetFeeTypeDdl();
        ddlFeeType.DataSource = dt;
        ddlFeeType.DataTextField = "Details";
        ddlFeeType.DataValueField = "No_";
        ddlFeeType.DataBind();

    }
    public void BindDdlNameOfMedia()
    {
        DataTable dt = new DataTable();
        EnquiryDL objEnquiryDL = new EnquiryDL();
        dt = objEnquiryDL.GetNameOfMediaDdl();
        ddlNameOfMedia.DataSource = dt;
        ddlNameOfMedia.DataTextField = "Details";
        ddlNameOfMedia.DataValueField = "No_";
        ddlNameOfMedia.DataBind();

    }
    public void BindDdlPrequalification()
    {
        DataTable dt = new DataTable();
        EnquiryDL objEnquiryDL = new EnquiryDL();
        dt = objEnquiryDL.GetPrequalificationDdl();
        ddlPrequalification.DataSource = dt;
        ddlPrequalification.DataTextField = "Details";
        ddlPrequalification.DataValueField = "No_";
        ddlPrequalification.DataBind();

    }
    public void BindDdlEnquiryType()
    {
        DataTable dt = new DataTable();
        EnquiryDL objEnquiryDL = new EnquiryDL();
        dt = objEnquiryDL.GetEnquiryTypeDdl();
        ddlEnquiryType.DataSource = dt;
        ddlEnquiryType.DataTextField = "Details";
        ddlEnquiryType.DataValueField = "No_";
        ddlEnquiryType.DataBind();

    }
    public void CalculateAge()
    {
        DataTable dt = new DataTable();
        EnquiryDL objEnquiryDL = new EnquiryDL();
        if(txtDOB.Text!=string.Empty)
        {
          dt = objEnquiryDL.GetAge(txtDOB.Text, DateTime.Now.ToString("dd-MM-yyyy"));
            txtYear.Text=dt.Rows[0]["Years"].ToString();
            txtMonth.Text=dt.Rows[0]["Months"].ToString();
        }
        if(Convert.ToInt32(txtYear.Text)<17)
        {
            
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", @"alert('Age is less than 17 years ! ')", true);

            //==========for catch=======
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",  "alert('" + ex.Message + "');", true);

        }
        
    }
    public void GetEnquiryDetails_ByEnquiryID(string ID)
    {
       BindDdlCity();
        EnquiryDL objEnquiryDL = new EnquiryDL();
        List<EnquiryPL> objEnquiryDetailList = new List<EnquiryPL>();
        objEnquiryDetailList = objEnquiryDL.GetEnquiryDetails_ByENquiryId(ID);
        txtEnquiryDate.Text = Convert.ToDateTime(objEnquiryDetailList[0].EnquiryDate).ToString("dd-MM-yyyy");
        txtApplicantName.Text = objEnquiryDetailList[0].ApplicantName;
        ddlCity.SelectedValue = objEnquiryDetailList[0].City;
       
        if (objEnquiryDetailList[0].DateofBirth != "")
        {
            txtDOB.Text = Convert.ToDateTime(objEnquiryDetailList[0].DateofBirth).ToString("dd-MM-yyyy");
        }
        
        if (objEnquiryDetailList[0].AcademicYear != "")
        { 
        ddlSession.SelectedValue = objEnquiryDetailList[0].AcademicYear;
        }
        txtYear.Text = objEnquiryDetailList[0].Age.ToString();
        txtMonth.Text =Convert.ToString(objEnquiryDetailList[0].Months);
        ddlCourse.SelectedValue = objEnquiryDetailList[0].CourseCode;
        ddlReligion.SelectedValue = objEnquiryDetailList[0].Religion.ToString();
        BindDdlCategory();
        ddlCategory.SelectedValue = objEnquiryDetailList[0].Category.ToString();
        ddlSubReligion.SelectedValue = objEnquiryDetailList[0].SubReligion.ToString();
        if (ddlReligion.SelectedValue.ToUpper() == "JAIN")
        {
            ddlSubReligion.Visible = true;
            lblSubReligionNotRequired.Visible = false;
        }
        else
        {
            ddlSubReligion.Visible = false;
            lblSubReligionNotRequired.Visible = true;
        }       
        
        
        ddlGender.SelectedValue = objEnquiryDetailList[0].Gender.ToString();
        ddlNationality.SelectedValue = objEnquiryDetailList[0].ApplicantStatus;
        string MobileNo = "";
        MobileNo = objEnquiryDetailList[0].MobileNumber;
        if (MobileNo.Length >= 10)
        {
            MobileNo = MobileNo.Substring(MobileNo.Length - 10, 10);
        }
        txtContactNo.Text = MobileNo; //objEnquiryDetailList[0].MobileNumber;       
        ddlFeeType.SelectedValue = objEnquiryDetailList[0].FeeType.ToString();
        txtEnquirerName.Text = objEnquiryDetailList[0].EnquirerName;
        txtApplicantRelationShip.Text = objEnquiryDetailList[0].ApplicantRelationship;
        txtFatherName.Text = objEnquiryDetailList[0].Father_sName;
        txtMotherName.Text = objEnquiryDetailList[0].Mother_sName;
        ddlPrequalification.SelectedValue = objEnquiryDetailList[0].Prequalification;
        ddlEnquiryType.SelectedValue = objEnquiryDetailList[0].EnquiryType;
        ddlEnquirySource.SelectedValue = objEnquiryDetailList[0].EnquirySource;
        ddlNameOfMedia.SelectedValue = objEnquiryDetailList[0].NameoftheMedia;
        txtAddress.Text = objEnquiryDetailList[0].Address1;
        txtRemarks.Text = objEnquiryDetailList[0].Remarks_Feedback;

    }

    public void BindDdlReligion()
    {
        DataTable dt = new DataTable();
        EnquiryDL objEnquiryDL = new EnquiryDL();
        dt = objEnquiryDL.GetReligionDdl();
        ddlReligion.DataSource = dt;
        ddlReligion.DataTextField = "Details";
        ddlReligion.DataValueField = "No_";
        ddlReligion.DataBind();

    }
    public void BindDdlSubReligion()
    {
        DataTable dt = new DataTable();
        EnquiryDL objEnquiryDL = new EnquiryDL();
        dt = objEnquiryDL.GetSubReligionDdl();
        ddlSubReligion.DataSource = dt;
        ddlSubReligion.DataTextField = "Details";
        ddlSubReligion.DataValueField = "No_";
        ddlSubReligion.DataBind();

    }
    public Boolean ValidateCourseAge()
    {
        Boolean Val = false;
        EnquiryDL objEnquiryDL = new EnquiryDL();        
        DataTable dtCourseAge = new DataTable();
        dtCourseAge = objEnquiryDL.GetCourseAge(ddlCourse.SelectedValue);
        int MaxAge = 0;
        hfCourseAge.Value = dtCourseAge.Rows[0]["MinAge"].ToString() + "-" + dtCourseAge.Rows[0]["MaxAge"].ToString();
        if (txtYear.Text == "") { txtYear.Text = "0"; }
        if (txtMonth.Text == "") { txtYear.Text = "0"; }
        

        if (txtYear.Text != "0" )        
        {
            if (Convert.ToInt16(txtMonth.Text) > 0)
            {
                MaxAge = Convert.ToInt16(txtYear.Text) + 1;
            }
            if (Convert.ToInt16(txtYear.Text) < Convert.ToInt16(dtCourseAge.Rows[0]["MinAge"].ToString()) || MaxAge > Convert.ToInt16(dtCourseAge.Rows[0]["MaxAge"].ToString()))
            {
                Val = true;
            }
        }
        return Val;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Boolean v = true;
        if (v == Validate())
        {
            return;
        }
        //----------------Duplicate---------Start
        string Result = commonDl.addApplicantInformation(txtContactNo.Text, ddlCourse.SelectedValue.ToString());
        if (Result != "" && rblOldNew.SelectedValue != "Old Enquiry")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", @"alert('Record all ready Exist with Enquiry( " + Result + " ) ! ')", true);
            return;
        }
        //----------------Duplicate-----End
        
        if (v == ValidateCourseAge())
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", @"alert('Age should be between "+hfCourseAge.Value+" !')", true);
            return;
        }
        else
        {
            EnquiryNo = lblEnquiryNo.Text;

            List<EnquiryPL> objEnquiryList = new List<EnquiryPL>();
            if (rblOldNew.SelectedValue == "Old Enquiry")
            {
                hfOldNew.Value = "Old";
            }

            if (hfOldNew.Value == "Old")
            {
                EnquiryNo = ddlEnquiryNo.SelectedValue;
            }
            else
            {
                EnquiryNo = "0";
            }
            if (txtDOB.Text == "") { txtDOB.Text = "01-01-1753"; }
            DateTime dt = Convert.ToDateTime(txtDOB.Text);
            string DOB = dt.ToString("MM-dd-yyyy");
            if (txtMonth.Text == "") { txtMonth.Text = "0"; }
            objEnquiryList.Add(new EnquiryPL(EnquiryNo,
                                               txtEnquiryDate.Text.Trim().ToString(),
                                               ddlEnquiryType.SelectedValue,
                                               ddlEnquirySource.SelectedValue,
                                               ddlNameOfMedia.SelectedValue,
                                               txtEnquirerName.Text,
                                               txtApplicantRelationShip.Text,
                                               txtApplicantName.Text.ToUpper(),
                                               DOB,
                                               txtFatherName.Text.ToUpper(),
                                               txtMotherName.Text.ToUpper(),
                                               ddlNationality.SelectedValue,
                                               ddlSession.SelectedValue,//Acadmic year
                                               ddlCourse.SelectedValue,
                                               "TMU",
                                               0,//hostel accomodation
                                               ddlPrequalification.SelectedValue,
                                               "",
                                               "",
                                               "",
                                               "",
                                               "",
                                               txtAddress.Text,
                                               "",
                                               ddlCity.SelectedValue,
                                               "",
                                               "",
                                               "",
                                               txtContactNo.Text,
                                               "",
                                               Convert.ToInt32(ddlGender.SelectedValue),
                                               "",
                                               "ENQ",//No series
                                               "",
                                               0,//"ConvertedToApplication"
                                              Convert.ToInt32(txtYear.Text),
                                              Convert.ToInt32(txtMonth.Text),
                                              Session["uid"].ToString(),//User ID
                                              "",//PortalID
                                              "",//CollegeIntrest
                                              ddlCategory.SelectedValue,//Category
                                              ddlReligion.SelectedValue,//Relgion
                                              Convert.ToInt32(ddlSubReligion.SelectedValue),//SubReligion,
                                              txtRemarks.Text,//Remarks                                             
                                              Convert.ToInt32(ddlFeeType.SelectedValue)
                                               ));

            EnquiryNo = SaveEnquiryList(objEnquiryList, UserName);
            if (EnquiryNo == "Error")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", @"alert('Error ! ')", true);
                return;
            }
            //----------------Save-message-------
            EmptyControls();
            GetNextEnquiryNo();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            //sb.Append("alert("+EnquiryNo+");");
            if (hfOldNew.Value == "Old")
                //sb.Append("alert('Records Updated Successfully');");
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Records Updated Successfully ! ');", true);
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", @"alert('Records Updated Successfully ! ')", true);
            else
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Records Saved Successfully ! ');", true);
               // ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", @"alert('Records Saved Successfully ! ')", true);
            //    sb.Append("alert('Records Saved Successfully');");
            //sb.Append("$('#editModal').modal('hide');");
            //sb.Append(@"</script>");
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);

        }

    }
    protected void ddlEnquiryNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlEnquiryNo.SelectedValue!="")
        {
        GetEnquiryDetails_ByEnquiryID(ddlEnquiryNo.SelectedValue);
        }
    }
    protected void txtDOB_TextChanged(object sender, EventArgs e)
    {
        //CalculateAge();       
    }
    public Boolean Validate()
    {
        Boolean Val = false;
        if (txtEnquiryDate.Text== "") {  Val = true;   }
        else if (txtApplicantName.Text == "") { Val = true; }
        else if (ddlCourse.SelectedIndex == 0) { Val = true; }
        else if (txtContactNo.Text == "") { Val = true; }
        else if (ddlCity.SelectedIndex == 0) { Val = true; }
        
        //else if (txtDOB.Text == "") {  Val = true;   }
        ////else if (ddlSession.SelectedIndex == 0) { Val = true; }        
        //else if (txtYear.Text == "" && txtMonth.Text=="")   { Val = true;  }
        //else if (ddlGender.SelectedIndex==0) { Val = true;  }
        //else if (ddlNationality.SelectedIndex == 0) { Val = true; }
        //else if (txtEnquirerName.Text == ""){   Val = true;  }
        //else if (txtApplicantRelationShip.Text == "") { Val = true;  }
        //else if (txtFatherName.Text == "") {  Val = true; } 
        //else if (txtMotherName.Text == "") {  Val = true; }
        //else if (ddlPrequalification.SelectedIndex==0) { Val = true;  }
        //else if (ddlEnquiryType.SelectedIndex == 0) { Val = true;  }
        //else if (ddlEnquirySource.SelectedIndex == 0)   {Val = true; }
        //else if (ddlNameOfMedia.SelectedIndex == 0) { Val = true;  }
        //else if (ddlFeeType.SelectedIndex == 0)     { Val = true; }
        //else if (txtAddress.Text == "") { Val = true; } 
         return Val ;
    }
    public String SaveEnquiryList(List<EnquiryPL> objEnquiryList, string UserName)
    {
        String ReturnID;
        EnquiryDL objEnquiryDL = new EnquiryDL();
        ReturnID = objEnquiryDL.SaveEnquiry(objEnquiryList, UserName);
        return ReturnID;
    }
    protected void ddlReligion_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDdlCategory();
        if (ddlReligion.SelectedValue == "JAIN")
        {
            ddlSubReligion.Visible = true;
            lblSubReligionNotRequired.Visible = false;
        }
        else
        {
            ddlSubReligion.Visible = false;
            lblSubReligionNotRequired.Visible = true;
        }
    }

    public void EmptyControls()
    {
        BindDdlENquiry();    
       // ddlEnquiryNo.SelectedIndex= 0;
        txtEnquiryDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        txtApplicantName.Text = "";
        txtDOB.Text = "";
        ddlSession.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
        ddlCity.SelectedIndex =0;
        txtContactNo.Text = "";
        txtYear.Text = "";
        txtMonth.Text = "";
        ddlCourse.SelectedIndex = 0;
        ddlReligion.SelectedIndex = 0;
        ddlCategory.SelectedIndex = 0;
        ddlSubReligion.SelectedIndex = 0;
        ddlGender.SelectedIndex = 0;
        ddlNationality.SelectedIndex = 0;
        txtEnquirerName.Text = "";
        txtApplicantRelationShip.Text = "";
        txtFatherName.Text = "";
        txtMotherName.Text = "";
        ddlPrequalification.SelectedIndex = 0;
        ddlEnquiryType.SelectedIndex = 0;
        ddlEnquirySource.SelectedIndex = 0;
        ddlNameOfMedia.SelectedIndex = 0;
        txtAddress.Text = "";
        txtRemarks.Text = "";
        ddlFeeType.SelectedIndex = 0;
        
    }
    protected void rblOldNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetNextEnquiryNo();
        EmptyControls();
        if (rblOldNew.SelectedValue == "Old Enquiry")
        {
            ddlEnquiryNo.Visible = true;
            lblEnquiryNo.Visible = false;
            hfOldNew.Value = "Old";
        }
        else
        {
            ddlEnquiryNo.Visible = false;
            lblEnquiryNo.Visible = true;
            hfOldNew.Value = "New";
        }
    }

    public void DisableForm(ControlCollection ctrls)
    {
        foreach (Control ctrl in ctrls)
        {
            if (ctrl is TextBox)
                ((TextBox)ctrl).Enabled = false;
            if (ctrl is Button)
                // ((Button)ctrl).Enabled = false;
                ((Button)ctrl).Visible = false;
            else if (ctrl is DropDownList)
                ((DropDownList)ctrl).Enabled = false;
            else if (ctrl is CheckBox)
                ((CheckBox)ctrl).Enabled = false;
            else if (ctrl is RadioButton)
                ((RadioButton)ctrl).Enabled = false;
            else if (ctrl is RadioButtonList)
                ((RadioButtonList)ctrl).Enabled = false;
            else if (ctrl is Calendar)
                ((Calendar)ctrl).Enabled = false;
            else if (ctrl is HtmlInputButton)
                ((HtmlInputButton)ctrl).Disabled = true;
            else if (ctrl is HtmlInputText)
                ((HtmlInputText)ctrl).Disabled = true;
            else if (ctrl is HtmlSelect)
                ((HtmlSelect)ctrl).Disabled = true;
            else if (ctrl is HtmlInputCheckBox)
                ((HtmlInputCheckBox)ctrl).Disabled = true;
            else if (ctrl is HtmlInputRadioButton)
                ((HtmlInputRadioButton)ctrl).Disabled = true;

            DisableForm(ctrl.Controls);

        }
    }
}