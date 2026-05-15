using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class AccountSetting : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal con;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Portalcon = new Connection();
            con = new ServicePoratal();
            showProfleDetail();
        }
        catch (Exception)
        {
            Response.Redirect("Default.aspx");
        }
    }
    string Statetx = "";
    public void show_statetx()
    {
        string stable = Session["Company"].ToString();
        string rtable = stable.Replace(".", "_");
        string tblNameEmployee = "[" + rtable + "$State" + "]";
        //string tblNameEmployee = Session["Company"] + "$Employee";
        SqlDataReader dr = Portalcon.Show_StatewithStateCode(tblNameEmployee, Statetx);
        dr.Read();
        if (dr.HasRows)
        {
            txtSTate.Text = dr["Description"].ToString();
        }
        dr.Close();
        Portalcon.DisConnect();

    }

    public void showdetail()
    {

        string tblNameEmployee = Session["CompanyTableEmployee"].ToString();

        SqlDataReader dr = Portalcon.Profile_Detail(tblNameEmployee, Session["uid"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            txttytle.Text = dr["Title"].ToString();
            txtFName.Text = dr["First Name"].ToString();
            txtSName.Text = dr["Middle Name"].ToString();
            txtLName.Text = dr["Last Name"].ToString();
            txtAddress.Text = dr["Address"].ToString();
            txtAddress2.Text = dr["Address 2"].ToString();
            txtCity.Text = dr["City"].ToString();
            Statetx = dr["State"].ToString();
          
            txtCountry.Text = dr["County"].ToString();
            txtPincode.Text = dr["Post Code"].ToString();
            txtEmailid.Text = dr["E-Mail"].ToString();
            string sex1 = dr["Sex"].ToString();
            if (sex1 == "1")
            {
                txtSex.Text = "Female";
            }
            else if (sex1 == "2")
            {
                txtSex.Text = "Male";
            }
          string mrstatus= dr["Marital Status"].ToString();
          if (mrstatus == "0")
          { txtStatus.Text = "Single";
          
          }
          if (mrstatus == "1")
          {
              txtStatus.Text = "Married";

          }
          if (mrstatus == "2")
          {
              txtStatus.Text = "Divorced";

          }
          if (mrstatus == "3")
          {
              txtStatus.Text = "Widow";

          }
            txtPhoneNo.Text = dr["Phone No_"].ToString();
            txtMobileNo.Text = dr["Mobile Phone No_"].ToString();
            txtDOB.Text =Convert.ToDateTime( dr["Birth Date"].ToString()).ToString("dd/MM/yyyy");
            txtFatherName.Text = dr["Father Name"].ToString();
            txtHusBandName.Text = dr["Husband Name"].ToString();
            //  txtMotherName.Text = dr[""].ToString();
            txtAddressPermanent.Text = dr["Permanent Address1"].ToString();
            txtAddress2Permanent.Text = dr["Permanent Address2"].ToString();
            txtCityPermanent.Text = dr["Permanent City"].ToString();
            txtStatePermanent.Text = dr["Permanent State"].ToString();
            
            txtPanNo.Text = dr["PAN No"].ToString();
            txtEsiNo.Text = dr["ESI No"].ToString();
            txtPFNo.Text = dr["PF No"].ToString();
            txtBranch.Text = dr["Location Code Dim_"].ToString();
            txtDepartment.Text = dr["Department Name"].ToString();
            txtCompanyMail.Text = dr["Company E-Mail"].ToString();
            txtACNO.Text = dr["Account No"].ToString();
            txtDOJ.Text = Convert.ToDateTime(dr["Employment Date"].ToString()).ToString("dd/MM/yyyy");
            txtDesignation.Text = dr["Job Title_Grade Desc"].ToString();
            txtIncharge.Text = dr["Reporting Incharge Name"].ToString();
            txtHOD.Text = dr["HOD Name"].ToString();

            string paymethod = dr["Payment Method"].ToString();
            if (paymethod == "1")
            {
                txtPaymethod.Text = "Cash";
            }
            if (paymethod == "2")
            {
                txtPaymethod.Text = "Check";
            }
            if (paymethod == "3")
            {
                txtPaymethod.Text = "Bank Transfer";
            }


        }
        dr.Close();
        Portalcon.DisConnect();
        show_statetx();
    }





    

            

    public void showProfleDetail()
    {


        SqlDataReader drpend = con.SHow_tble_ProfileApprovalStatuswithUserid(Session["uid"].ToString(), Session["Company"].ToString());
        drpend.Read();
        if (drpend.HasRows)
        {
            pnlApprovalDetail.Visible=true;
            lblProfileStatus.Text = drpend["ApprovalStatus"].ToString();
            lblUpdatedDate.Text = drpend["Approval_HOD_Date"].ToString();
            lblHRUpdatedate.Text = drpend["Approval_HR_Date"].ToString();
            drpend.Close();
            con.DisConnect();
            showdetail();

        }
        else
        {
            pnlApprovalDetail.Visible = false;
            drpend.Close();
            con.DisConnect();
            showdetail();
        }
    }
}