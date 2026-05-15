using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

using System.IO;

public partial class changeStatus : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal con;
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            Portalcon = new Connection();
            con = new ServicePoratal();
            lblID.Text = Session["chgsUserid"].ToString();
            FindChangeStatusNew();
            FindChangeStatusOLd(Session["CompanyTableEmployee"].ToString());
        }
        catch (Exception)
        {
            Response.Redirect("Default.aspx");
        }

       
    }

    public void FindChangeStatusNew()
    {
        SqlDataAdapter da = new SqlDataAdapter("select * from tble_ProfileApprovalStatus where UserID='" + Session["chgsUserid"].ToString() + "' and CompanyName='" + Session["Company"].ToString() + "' and ProfileUpdateDate ='" + Session["chgsProfilechangeDate"].ToString() + "' and id='" + Session["chgsNoOfchanges"].ToString() + "' ", con.Con);
        DataSet ds = new DataSet();
        da.Fill(ds, "tble_ProfileApprovalStatus");
        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
        {


            lblOldAddress.Text = ds.Tables[0].Rows[i]["Address"].ToString();

            lblOldAddress1.Text = ds.Tables[0].Rows[i]["Address2"].ToString();
            lblOldCity.Text = ds.Tables[0].Rows[i]["City"].ToString();
            lblOldState.Text = ds.Tables[0].Rows[i]["State"].ToString();
            lblOldCountry.Text = ds.Tables[0].Rows[i]["Country"].ToString();
            lblOldPincode.Text = ds.Tables[0].Rows[i]["Pin_Code"].ToString();
            lblOldEmailID.Text = ds.Tables[0].Rows[i]["Email_ID"].ToString();
            string martialstatus = ds.Tables[0].Rows[i]["Marital_Status"].ToString();
            if (martialstatus == "0")
            {
                lblOldMaritalStatus.Text = "Single";

            }
            if (martialstatus == "1")
            {
                lblOldMaritalStatus.Text = "Married";

            }
            if (martialstatus == "2")
            {
                lblOldMaritalStatus.Text = "Divorced";

            }
            if (martialstatus == "3")
            {
                lblOldMaritalStatus.Text = "Widow";

            }
            //lblNewMaritalStatus.Text = ds.Tables[0].Rows[i]["Marital_Status"].ToString();
            lblOldPhoneNo.Text = ds.Tables[0].Rows[i]["Phone_No"].ToString();
            lblOLDContactNo.Text = ds.Tables[0].Rows[i]["Mobile_No"].ToString();
            lblOldDateOfBirth.Text = ds.Tables[0].Rows[i]["DOB"].ToString();
            lblOldFatherName.Text = ds.Tables[0].Rows[i]["FatherName"].ToString();
            lblOldHusbandName.Text = ds.Tables[0].Rows[i]["HusbandName"].ToString();
            //lblNewMotherName.Text = ds.Tables[0].Rows[i]["MotherName"].ToString();
            lblOldAddressPERMAnent.Text = ds.Tables[0].Rows[i]["PAddress"].ToString();
            lblOldAddressPERMAnent2.Text = ds.Tables[0].Rows[i]["PAddress2"].ToString();
            lblOldCitypermanet.Text = ds.Tables[0].Rows[i]["PCity"].ToString();
            lblOldStatePermanet.Text = ds.Tables[0].Rows[i]["PState"].ToString();
            //lblNewCitypermanet.Text = ds.Tables[0].Rows[i]["PCountry"].ToString();
            //lblnewPincodepermanet.Text = ds.Tables[0].Rows[i]["PPinCode"].ToString();
            lblOldPanNo.Text = ds.Tables[0].Rows[i]["PanNo"].ToString();
            lblOldACNo.Text = ds.Tables[0].Rows[i]["Ac_No"].ToString();
            lblOldPanNo.Text = ds.Tables[0].Rows[i]["PanNo"].ToString();
            lblchangestatusmodel.Text = ds.Tables[0].Rows[i]["Change_Status"].ToString();
            imgOldprofile.ImageUrl = "~/ProfileImage/" + ds.Tables[0].Rows[i]["Photo1"].ToString();


        }
        con.DisConnect();
    }



    public void FindChangeStatusOLd(string compnayName)
    {
        SqlDataAdapter da = new SqlDataAdapter("select * from " + compnayName + "  where No_='" + Session["chgsUserid"].ToString() + "' ", Portalcon.Con);
        DataSet ds = new DataSet();
        da.Fill(ds, "Employee");
        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
        {


            lblNewAddress.Text = ds.Tables[0].Rows[i]["Address"].ToString();

            lblNewAddress1.Text = ds.Tables[0].Rows[i]["Address 2"].ToString();
            lblNewCity.Text = ds.Tables[0].Rows[i]["City"].ToString();
            lblNewState.Text = ds.Tables[0].Rows[i]["State"].ToString();
            lblNewCountry.Text = ds.Tables[0].Rows[i]["County"].ToString();
            lblNewPincode.Text = ds.Tables[0].Rows[i]["Post Code"].ToString();
            lblNewEmailID.Text = ds.Tables[0].Rows[i]["E-Mail"].ToString();
            string martialstatus = ds.Tables[0].Rows[i]["Marital Status"].ToString();
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

            lblNewPhoneNo.Text = ds.Tables[0].Rows[i]["Phone No_"].ToString();
            lblNewContactNo.Text = ds.Tables[0].Rows[i]["Mobile Phone No_"].ToString();
            lblNewDateofBirth.Text = ds.Tables[0].Rows[i]["Birth Date"].ToString();
            lblNewFatherName.Text = ds.Tables[0].Rows[i]["Father Name"].ToString();
            lblNewHusbandName.Text = ds.Tables[0].Rows[i]["Husband Name"].ToString();
            // lblOldMotherName.Text = ds.Tables[0].Rows[i]["MotherName"].ToString();
            lblNewAddressPERMAnent.Text = ds.Tables[0].Rows[i]["Permanent Address1"].ToString();
            lblNewAddressPERMAnent2.Text = ds.Tables[0].Rows[i]["Permanent Address2"].ToString();
            lblNewCitypermanet.Text = ds.Tables[0].Rows[i]["Permanent City"].ToString();
            lblNewStatePermanet.Text = ds.Tables[0].Rows[i]["Permanent State"].ToString();
            //lblOldCitypermanet.Text = ds.Tables[0].Rows[i]["County"].ToString();
            // lblOldPincodepermanet.Text = ds.Tables[0].Rows[i]["PPinCode"].ToString();
            lblNewPanNo.Text = ds.Tables[0].Rows[i]["PAN No"].ToString();
            lblNewACNO.Text = ds.Tables[0].Rows[i]["Account No"].ToString();
            imgNewProfilePhoto.ImageUrl = "~/ProfileImage/" + ds.Tables[0].Rows[i]["ProfilePhoto"].ToString();




        }
        Portalcon.DisConnect();
    }

}