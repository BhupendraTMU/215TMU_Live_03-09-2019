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
public partial class PaySlip : System.Web.UI.Page
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
                showProfleDetail();

            }

           
        }
        catch (Exception)
        {
            Response.Redirect("Default.aspx");
        }
    }

    public void showProfleDetail()
    {
        
        string stable = Session["Company"].ToString();
        string rtable = stable.Replace(".", "_");
        string tblNameEmployee = "[" + rtable + "$Employee" + "]";
       
        SqlDataReader dr = Portalcon.Profile_Detail(tblNameEmployee, Session["uid"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            lblEmployeeid.Text = Session["uid"].ToString();
        
           string fname = dr["First Name"].ToString();
            string sname = dr["Middle Name"].ToString();
           string lname = dr["Last Name"].ToString();
           lblEmployeeName.Text = fname + " " + sname + " " + lname;
            lblDesignation.Text = dr["Job Title_Grade Desc"].ToString();
            lblDesignation.Text = dr["Job Title_Grade Desc"].ToString();
              lblDepartment.Text = dr["Department Name"].ToString();
               lblPFNo.Text = dr["PF No"].ToString();


            lblPanNo.Text = dr["PAN No"].ToString();
           lblESINo.Text = dr["ESI No"].ToString();
            lblLocation.Text = dr["Location Code Dim_"].ToString();
            string paymethod = dr["Payment Method"].ToString();
            if (paymethod == "1")
            {
                lblPayMode.Text = "Cash";
            }
            if (paymethod == "2")
            {
                lblPayMode.Text  = "Check";
            }
            if (paymethod == "3")
            {
                 lblPayMode.Text  = "Bank Transfer";
            }
            lblDOJ.Text = Convert.ToDateTime(dr["Employment Date"].ToString()).ToString("dd-MMM-yy");
                   
            lblAcoountNo.Text = dr["Account No"].ToString();
           
            
            

            


        }
        dr.Close();
        Portalcon.DisConnect();

        string sCompanyn = Session["Company"].ToString();
        string rsCompanyn = stable.Replace(".", "_");
        string tblsCompanyn = "[" + rtable + "$Company Information" + "]";
        SqlDataReader dr1 = Portalcon.SHow_CompanyInformation(tblsCompanyn);
        dr1.Read();

        if (dr1.HasRows)
        {
            lblCompanyAddress.Text = dr1["Address"].ToString();

            byte[] img = (byte[])(dr1["Picture"]);

            string base64String = Convert.ToBase64String(img, 0, img.Length);
            imgPhoto.ImageUrl = "data:image/png;base64," + base64String;
            imgPhoto.Visible = true;
        }
        dr1.Close();
        Portalcon.DisConnect();



    }

}