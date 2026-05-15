using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

using System.IO;

public partial class viewpreviousprofile : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal con;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Portalcon = new Connection();
            con = new ServicePoratal();
            if (!IsPostBack)
            {
                showPreviosProfile();
            }
        }
        catch (Exception)
        {
            Response.Redirect("Default.aspx");
        }

    }

    public void showPreviosProfile()
    {
        SqlDataReader odr = con.Profile_PreviouseDetail(Session["uid"].ToString(), Session["Company"].ToString());
        DataTable Dt = new DataTable();
        Dt.Load(odr);
        grdViewApproval.DataSource = Dt;
        grdViewApproval.DataBind();
        odr.Close();
        con.DisConnect();
    }
    protected void grdViewApproval_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdViewApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdViewApproval.PageIndex = e.NewPageIndex;
        showPreviosProfile();
    }
    string chgsUserid = ""; string chgsProfilechangeDate = ""; string chgsNoOfchanges = "";
    public void FindChangeStatusNew()
    {
        SqlDataAdapter da = new SqlDataAdapter("select * from tble_ProfileApprovalStatus where UserID='" + chgsUserid + "' and CompanyName='" + Session["Company"].ToString() + "' and ProfileUpdateDate ='" + chgsProfilechangeDate + "' and id='" + chgsNoOfchanges.ToString() + "'", con.Con);
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
            string martialstatus = ds.Tables[0].Rows[i]["Marital_Status"].ToString();
            if (martialstatus == "0")
            {
                lblNewMaritalStatus.Text = "Single";

            }
            if (martialstatus == "1")
            {
                lblNewMaritalStatus.Text = "Married";

            }
            if (martialstatus == "2")
            {
                lblNewMaritalStatus.Text = "Divorced";

            }
            if (martialstatus == "3")
            {
                lblNewMaritalStatus.Text = "Widow";

            }

            //lblNewMaritalStatus.Text = ds.Tables[0].Rows[i]["Marital_Status"].ToString();
            lblNewPhoneNo.Text = ds.Tables[0].Rows[i]["Phone_No"].ToString();
            lblNewContactNo.Text = ds.Tables[0].Rows[i]["Mobile_No"].ToString();
            lblNewDateofBirth.Text = ds.Tables[0].Rows[i]["DOB"].ToString();
            lblNewFatherName.Text = ds.Tables[0].Rows[i]["FatherName"].ToString();
            lblNewHusbandName.Text = ds.Tables[0].Rows[i]["HusbandName"].ToString();
           // lblNewMotherName.Text = ds.Tables[0].Rows[i]["MotherName"].ToString();
            lblNewAddressPERMAnent.Text = ds.Tables[0].Rows[i]["PAddress"].ToString();
            lblNewAddressPERMAnent2.Text = ds.Tables[0].Rows[i]["PAddress2"].ToString();
            lblNewCitypermanet.Text = ds.Tables[0].Rows[i]["PCity"].ToString();
            lblNewStatePermanet.Text = ds.Tables[0].Rows[i]["PState"].ToString();
           // lblNewCitypermanet.Text = ds.Tables[0].Rows[i]["PCountry"].ToString();
            //lblnewPincodepermanet.Text = ds.Tables[0].Rows[i]["PPinCode"].ToString();
            lblNewPanNo.Text = ds.Tables[0].Rows[i]["PanNo"].ToString();
            lblNewACNO.Text = ds.Tables[0].Rows[i]["Ac_No"].ToString();
            lblNewPanNo.Text = ds.Tables[0].Rows[i]["PanNo"].ToString();
            lblchangestatusmodel.Text = ds.Tables[0].Rows[i]["Change_Status"].ToString();
            imgNewProfilePhoto.ImageUrl = "~/ProfileImage/" + ds.Tables[0].Rows[i]["Photo1"].ToString();

            lblOldAddress.Text = ds.Tables[0].Rows[i]["OldAddress"].ToString();

            lblOldAddress1.Text = ds.Tables[0].Rows[i]["OldAddress2"].ToString();
            lblOldCity.Text = ds.Tables[0].Rows[i]["OldCity"].ToString();
            lblOldState.Text = ds.Tables[0].Rows[i]["OldState"].ToString();
            lblOldCountry.Text = ds.Tables[0].Rows[i]["OldCounty"].ToString();
            lblOldPincode.Text = ds.Tables[0].Rows[i]["OldPostCode"].ToString();
            lblOldEmailID.Text = ds.Tables[0].Rows[i]["OldEMail"].ToString();
            string oldmartialstatus = ds.Tables[0].Rows[i]["OldMaritalStatus"].ToString();
            if (oldmartialstatus == "0")
            {
                lblOldMaritalStatus.Text = "Single";

            }
            if (oldmartialstatus == "1")
            {
                lblOldMaritalStatus.Text = "Married";

            }
            if (oldmartialstatus == "2")
            {
                lblOldMaritalStatus.Text = "Divorced";

            }
            if (oldmartialstatus == "3")
            {
                lblOldMaritalStatus.Text = "Widow";

            }


            lblOldPhoneNo.Text = ds.Tables[0].Rows[i]["OldPhoneNo"].ToString();
            lblOLDContactNo.Text = ds.Tables[0].Rows[i]["OldMobilePhoneNo"].ToString();
            lblOldDateOfBirth.Text = ds.Tables[0].Rows[i]["OldBirthDate"].ToString();
            lblOldFatherName.Text = ds.Tables[0].Rows[i]["OldFatherName"].ToString();
            lblOldHusbandName.Text = ds.Tables[0].Rows[i]["OldHusbandName"].ToString();
            // lblOldMotherName.Text = ds.Tables[0].Rows[i]["MotherName"].ToString();
            lblOldAddressPERMAnent.Text = ds.Tables[0].Rows[i]["OldPermanentAddress1"].ToString();
            lblOldAddressPERMAnent2.Text = ds.Tables[0].Rows[i]["OldPermanentAddress2"].ToString();
            lblOldCitypermanet.Text = ds.Tables[0].Rows[i]["OldPermanentCity"].ToString();
            lblOldStatePermanet.Text = ds.Tables[0].Rows[i]["OldPermanentState"].ToString();
            //lblOldCitypermanet.Text = ds.Tables[0].Rows[i]["County"].ToString();
            // lblOldPincodepermanet.Text = ds.Tables[0].Rows[i]["PPinCode"].ToString();
            lblOldPanNo.Text = ds.Tables[0].Rows[i]["OldPANNo"].ToString();
            lblOldACNo.Text = ds.Tables[0].Rows[i]["OldAccountNo"].ToString();
            imgOldprofile.ImageUrl = "~/ProfileImage/" + ds.Tables[0].Rows[i]["OldProfilePhoto"].ToString();





        }
        con.DisConnect();
    }



    //public void FindChangeStatusOLd(string compnayName)
    //{
    //    SqlDataAdapter da = new SqlDataAdapter("select * from " + compnayName + "  where No_='" + chgsUserid + "' ", Portalcon.Con);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds, "Employee");
    //    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
    //    {


    //        lblOldAddress.Text = ds.Tables[0].Rows[i]["Address"].ToString();

    //        lblOldAddress1.Text = ds.Tables[0].Rows[i]["Address 2"].ToString();
    //        lblOldCity.Text = ds.Tables[0].Rows[i]["City"].ToString();
    //        lblOldState.Text = ds.Tables[0].Rows[i]["State"].ToString();
    //        lblOldCountry.Text = ds.Tables[0].Rows[i]["County"].ToString();
    //        lblOldPincode.Text = ds.Tables[0].Rows[i]["Post Code"].ToString();
    //        lblOldEmailID.Text = ds.Tables[0].Rows[i]["E-Mail"].ToString();

    //        string martialstatus = ds.Tables[0].Rows[i]["Marital Status"].ToString();
    //        if (martialstatus == "0")
    //        {
    //            lblOldMaritalStatus.Text = "Single";

    //        }
    //        if (martialstatus == "1")
    //        {
    //            lblOldMaritalStatus.Text = "Married";

    //        }
    //        if (martialstatus == "2")
    //        {
    //            lblOldMaritalStatus.Text = "Divorced";

    //        }
    //        if (martialstatus == "3")
    //        {
    //            lblOldMaritalStatus.Text = "Widow";

    //        }


    //       // lblOldMaritalStatus.Text = ds.Tables[0].Rows[i]["Marital Status"].ToString();
    //        lblOldPhoneNo.Text = ds.Tables[0].Rows[i]["Phone No_"].ToString();
    //        lblOLDContactNo.Text = ds.Tables[0].Rows[i]["Mobile Phone No_"].ToString();
    //        lblOldDateOfBirth.Text = ds.Tables[0].Rows[i]["Birth Date"].ToString();
    //        lblOldFatherName.Text = ds.Tables[0].Rows[i]["Father Name"].ToString();
    //        lblOldHusbandName.Text = ds.Tables[0].Rows[i]["Husband Name"].ToString();
    //        // lblOldMotherName.Text = ds.Tables[0].Rows[i]["MotherName"].ToString();
    //        lblOldAddressPERMAnent.Text = ds.Tables[0].Rows[i]["Permanent Address1"].ToString();
    //        lblOldAddressPERMAnent2.Text = ds.Tables[0].Rows[i]["Permanent Address2"].ToString();
    //        lblOldCitypermanet.Text = ds.Tables[0].Rows[i]["Permanent City"].ToString();
    //        lblOldStatePermanet.Text = ds.Tables[0].Rows[i]["Permanent State"].ToString();
    //      //  lblOldCitypermanet.Text = ds.Tables[0].Rows[i]["County"].ToString();
    //        // lblOldPincodepermanet.Text = ds.Tables[0].Rows[i]["PPinCode"].ToString();
    //        lblOldPanNo.Text = ds.Tables[0].Rows[i]["PAN No"].ToString();
    //        lblOldACNo.Text = ds.Tables[0].Rows[i]["Account No"].ToString();
    //        imgOldprofile.ImageUrl = "~/ProfileImage/" + ds.Tables[0].Rows[i]["ProfilePhoto"].ToString();




    //    }
    //    Portalcon.DisConnect();
    //}


    protected void btnchangestatus_Command(object sender, CommandEventArgs e)
    {
        //chgsUserid = e.CommandArgument.ToString();
        //foreach (GridViewRow row in grdViewApproval.Rows)
        //{
        //    if (row.RowType == DataControlRowType.DataRow)
        //    {

        //        Label lblProfilechangedate1 = (row.Cells[0].FindControl("lblProfilechangedate") as Label);
        //        chgsProfilechangeDate = lblProfilechangedate1.Text;
        //        Label lblNoofchange = (row.Cells[0].FindControl("lblNoofchange") as Label);
        //        chgsNoOfchanges = lblNoofchange.Text.ToString();

        //        FindChangeStatusNew();
        //        FindChangeStatusOLd(Session["CompanyTableEmployee"].ToString());
        //       this.ModalPopupExtender1.Show();

        //    }
        //}

      

       chgsUserid = e.CommandArgument.ToString();

        
               
            
        }
       
    
    protected void grdViewApproval_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        GridViewRow Row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);


        Label lblProfilechangedate1 = (Label)Row.FindControl("lblProfilechangedate");

      chgsProfilechangeDate = lblProfilechangedate1.Text;

        Label lblNoofchange = (Label)Row.FindControl("lblNoofchange");
       chgsNoOfchanges = lblNoofchange.Text;
        FindChangeStatusNew();
        //FindChangeStatusOLd(Session["CompanyTableEmployee"].ToString());
        this.ModalPopupExtender1.Show();
  //      Page.ClientScript.RegisterStartupScript(
  //this.GetType(), "OpenWindow", "window.open('changeStatus.aspx','_newtab');", true);

       

    }
}