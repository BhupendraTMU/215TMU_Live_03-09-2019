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
public partial class EditProfile : System.Web.UI.Page
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
           
            hrID();
        }
        catch (Exception)
        {
            Response.Redirect("Default.aspx");
        }
    }


    string mailfrom = ""; string subject1 = ""; string Body1 = ""; string smtpfromportal = ""; int portNo1; string Pass_From = ""; string Mail_Sending_Option = ""; string Leave_Applymail = ""; string CCMail = "";
    public void ShowMailData(string mailTo1)
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
            Leave_Applymail = dr["Profile_Change"].ToString();

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


    public void hrID()
    {
        string stable = Session["Company"].ToString();
        string rtable = stable.Replace(".", "_");
        string tblNameEmployee = "[" + rtable + "$Employee" + "]";
        SqlDataReader dr = Portalcon.Show_HRID(tblNameEmployee, Session["HRID"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            lblHRUserId.Text = Session["HRID"].ToString();
        

        }
        dr.Close();
        Portalcon.DisConnect();
    }

   
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
    public void show_state()
    {
        string stable = Session["Company"].ToString();
        string rtable = stable.Replace(".", "_");
        string tblNameEmployee = "[" + rtable + "$State" + "]";
        //string tblNameEmployee = Session["Company"] + "$Employee";
        SqlDataReader dr = Portalcon.Show_State(tblNameEmployee);
        txtSTate.DataSource = dr;
      
        txtSTate.DataValueField = "Code";
        txtSTate.DataTextField = "Description";
        txtSTate.DataBind();
        dr.Close();
        Portalcon.DisConnect();
    
    }

    public void Show_Pincode()
    {

        string stable = Session["Company"].ToString();
        string rtable = stable.Replace(".", "_");
        string tblNameEmployee = "[" + rtable + "$Post Code" + "]";
        //string tblNameEmployee = Session["Company"] + "$Employee";
        SqlDataReader dr = Portalcon.Show_Pincode(tblNameEmployee);
        txtPincode.DataSource = dr;

        txtPincode.DataValueField = "Code";

        txtPincode.DataBind();
        dr.Close();
        Portalcon.DisConnect();
    }

    public void showProfleDetail()
    {
        show_state();
        Show_Pincode();
        string stable = Session["Company"].ToString();
        string rtable = stable.Replace(".", "_");
        string tblNameEmployee = "[" + rtable + "$Employee" + "]";
        //string tblNameEmployee = Session["Company"] + "$Employee";
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
            txtSTate.SelectedValue = dr["State"].ToString();
            txtCountry.Text = dr["County"].ToString();
            txtPincode.SelectedValue = dr["Post Code"].ToString();
            txtEmailid.Text = dr["E-Mail"].ToString();
            //txtSex.Text = dr["Sex"].ToString();
            //txtStatus.Text = dr["Marital Status"].ToString();
            string sex1 = dr["Gender"].ToString();
            if (sex1 == "1")
            {
                txtSex.Text = "Female";
            }
            else if (sex1 == "2")
            {
                txtSex.Text = "Male";
            }
            string mrstatus = dr["Marital Status"].ToString();
            if (mrstatus == "0")
            {
               // txtStatus.SelectedValue= "Single";
                txtStatus.SelectedValue = "0";
            }
            if (mrstatus == "1")
            {
               // txtStatus.SelectedItem.Text = "Married";
                txtStatus.SelectedValue = "1";
            }
            if (mrstatus == "2")
            {
               // txtStatus.SelectedItem.Text = "Divorced";
                txtStatus.SelectedValue = "2";
            }
            if (mrstatus == "3")
            {
               // txtStatus.SelectedItem.Text = "Widow";
                txtStatus.SelectedValue = "3";
            }
            //9876771135 Papa
          //  Mahesh Sharma Shimla
           
            txtPhoneNo.Text = dr["Phone No_"].ToString();
            txtMobileNo.Text = dr["Mobile Phone No_"].ToString();
            txtDOB.Text = Convert.ToDateTime(dr["Birth Date"].ToString()).ToString("dd/MM/yyyy");
            txtFatherName.Text = dr["Father Name"].ToString();
            txtHusBandName.Text = dr["Husband Name"].ToString();
            //  txtMotherName.Text = dr[""].ToString();
            txtAddressPermanent.Text = dr["Permanent Address1"].ToString();
            txtAddress2Permanent.Text = dr["Permanent Address2"].ToString();
            txtCityPermanent.Text = dr["Permanent City"].ToString();
            txtStatePermanent.Text = dr["Permanent State"].ToString();
            //txtCountryPermanent.Text = dr["County"].ToString();
            // txtPincodePermanent.Text = dr["PPost Code"].ToString();
            txtPanNo.Text = dr["PAN No"].ToString();
            txtEsiNo.Text = dr["ESI No"].ToString();
            txtPFNo.Text = dr["PF No"].ToString();
            txtBranch.Text = dr["Location Code"].ToString();
            txtDepartment.Text = dr["Department Name"].ToString();
            txtCompanyMail.Text = dr["Company E-Mail"].ToString();
            txtACNO.Text = dr["Account No"].ToString();
            txtDOJ.Text = Convert.ToDateTime(dr["Employment Date"].ToString()).ToString("dd/MM/yyyy");
            txtDesignation.Text = dr["Designation Code"].ToString();
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
    }
   

    string lblnofchange = "";
    public void lastrec()
    {
         string updatedate=System.DateTime.Now.ToString("yyyy-MM-dd");
         SqlDataReader dr = con.SHow_NoofchangePerday(Session["uid"].ToString(), updatedate, Session["Company"].ToString());

        dr.Read();
        if (dr.HasRows)
        {

            string d = dr["Noofchange"].ToString();
            double rec = Convert.ToInt64(d);
            double c1 = rec + 1;
            lblnofchange = c1.ToString();
        }

        else
        {
            lblnofchange = "1";

        }

        dr.Close();
        con.DisConnect();



    }



    //byte[] imgname; 
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Showpermission();
        string Address1 = ""; string Address21 = ""; string City1 = ""; string STate1 = ""; string Country1 = ""; string Pincode1 = ""; string Emailid1 = ""; string Status1 = ""; string PhoneNo1 = ""; string MobileNo1 = ""; string DOB1 = ""; string HusBandName1 = ""; string AddressPermanent1 = ""; string AddressPermanent2 = ""; string CityPermanent1 = ""; string CountryPermanent1 = ""; string statePermanet12 = ""; string PincodePermanent1 = ""; string PanNo1 = ""; string ACNO1 = ""; string change_stSMS = ""; string change_status = ""; string path = ""; string fatherName1 = ""; string hoduserid = "";
        string profilephoto = "";

        string imgname1 = "";
        string simgname = "NULL";
        //if (FileUpload1.HasFile)
        //{
        //    Random rd = new Random();
        //    int idno = rd.Next(1, 1000000);
        //    string filen = Path.GetFileName(FileUpload1.FileName);
        //    imgname1 = Session["uid"].ToString() + idno + filen;

        //    FileUpload1.SaveAs(Server.MapPath("~/ProfileImage/") + Session["uid"].ToString() + idno + filen);


        //}
        //else
        //{

           
        //    imgname1 = Session["ph1"].ToString();
        //}


        //FileUpload img = (FileUpload)FileUpload1;
        //Byte[] simgname = null;

        //HttpPostedFile File = FileUpload1.PostedFile;

        //simgname = new Byte[File.ContentLength];

        //File.InputStream.Read(simgname, 0, File.ContentLength);

       

   string approvalstatus="Pending";
        string updatedate=System.DateTime.Now.ToString("dd/MM/yyyy");
        string stable = Session["Company"].ToString();
        string rtable = stable.Replace(".", "_");
        string cmpName = "[" + rtable + "$Employee" + "]";
        //string cmpName=Session["Company"].ToString()+"$Employee";
        SqlDataReader dr = Portalcon.Profile_Detail(cmpName, Session["uid"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            profilephoto = dr["ProfilePhoto"].ToString().ToUpper();
            string Address = dr["Address"].ToString().ToUpper();
            if (Address == txtAddress.Text.ToUpper())
            { }
            else
            {
                Address1 = "Address";
            }

            string Address2 = dr["Address 2"].ToString().ToUpper();
            if (Address2 == txtAddress2.Text.ToUpper())
            { }
            else
            {
                Address21 = "Address 2";
            }
            string City = dr["City"].ToString().ToUpper();
            if (City == txtCity.Text.ToUpper())
            { }
            else
            {
                City1 = "City";
            }
            string STate = dr["State"].ToString().ToUpper();
            if (STate == txtSTate.Text)
            {

            }
            else
            {
                STate1 = "State";
            }
            string Country = dr["County"].ToString().ToUpper();
            if (Country == txtCountry.Text.ToUpper())
            { }
            else
            {
                Country1 = "Country";
            }
            string Pincode = dr["Post Code"].ToString().ToUpper();
            if (Pincode == txtPincode.Text.ToUpper())
            { }
            else
            {
                Pincode1 = "Post Code";
            }
            string Emailid = dr["E-Mail"].ToString().ToUpper();
            if (Emailid == txtEmailid.Text.ToUpper())
            {

            }
            else
            {
                Emailid1 = "Email Id";
            }
           

            string Status = dr["Marital Status"].ToString();


            if (Status == txtStatus.SelectedValue.ToString())
            { }
            else
            {
                Status1 = "Marital Status";
            }
            string PhoneNo = dr["Phone No_"].ToString();
            if (PhoneNo == txtPhoneNo.Text)
            { }
            else
            {
                PhoneNo1 = "Phone No";
            }
            string MobileNo = dr["Mobile Phone No_"].ToString();
            if (MobileNo == txtMobileNo.Text)
            { }
            else
            {
                MobileNo1 = "Mobile No";
            }
            string DOB = Convert.ToDateTime(dr["Birth Date"].ToString()).ToString("dd/MM/yyyy"); ;
            if (DOB == txtDOB.Text)
            { }
            else
            {
                DOB1 = "Date Of Birth";
            }
            string HusBandName = dr["Husband Name"].ToString().ToUpper();
            if (HusBandName == txtHusBandName.Text.ToUpper())
            { }
            else
            {
                HusBandName1 = "Husband Name";
            }
            string AddressPermanent = dr["Permanent Address1"].ToString().ToUpper();
            if (AddressPermanent == txtAddressPermanent.Text.ToUpper())
            { }
            else
            {
                AddressPermanent1 = "Permanent Address";
            }

            string AddressPermanent3 = dr["Permanent Address2"].ToString().ToUpper();
            if (AddressPermanent3 == txtAddress2Permanent.Text.ToUpper())
            { }
            else
            {
                AddressPermanent2 = "Permanent Address2";
            }

            string CityPermanent = dr["Permanent City"].ToString().ToUpper();
            if (CityPermanent == txtCityPermanent.Text.ToUpper())
            { }
            else
            {
                CityPermanent1 = "Permanent City";
            }
           
            string statePermanet = dr["Permanent State"].ToString().ToUpper();
            if (statePermanet == txtStatePermanent.Text.ToUpper())
            { }
            else
            {
                statePermanet12 = "Permanent State";
            }

            string fatherName = dr["Father Name"].ToString().ToUpper();
            if (fatherName == txtFatherName.Text.ToUpper())
            { }
            else
            {
                fatherName1 = "Father Name";
            }

            string PanNo = dr["PAN No"].ToString();
            if (PanNo == txtPanNo.Text)
            { }
            else
            {
                PanNo1 = "Pan No";
            }
           
            hoduserid = dr["HOD"].ToString();
          
            string ACNO = dr["Account No"].ToString().ToUpper();
            if (ACNO == txtACNO.Text.ToUpper())
            {

            }
            else
            {
                ACNO1 = "Account No";
            }
            dr.Close();
            Portalcon.DisConnect();
            change_stSMS = Address1 + "," + "," + Address21 + " " + "," + City1 + " " + ", " + STate1 + " " + ", " + Country1 + " " + ", " + Pincode1 + " " + "," + Emailid1 + " " + "," + "," + Status1 + " " + "," + PhoneNo1 + " " + "," + MobileNo1 + " " + "," + DOB1 + " " + "," + fatherName1 + " " + "," + HusBandName1 + " " + "," + AddressPermanent1 + " " + "," + AddressPermanent2 + " " + "  ," + CityPermanent1 + " " + "," + CountryPermanent1 + " " + "," + PincodePermanent1 + " " + "," + statePermanet12 + " " + " " + "," + PanNo1 + " " + "," + " " + "," + ACNO1 ;

            string remSTATUs = change_stSMS.Replace(",","");
            if (remSTATUs.Trim() == "" )
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('You have not changed any information');", true);
            }
            else
            {
                string rejectsttusapr = "";
                change_status = change_stSMS + "This information want to change";
                SqlDataReader drPAS = con.SHow_tble_ProfileApprovalStatus(Session["uid"].ToString(), updatedate,Session["Company"].ToString());
                drPAS.Read();
                if (drPAS.HasRows)
                {
                    rejectsttusapr = drPAS["ApprovalStatus"].ToString();
                    drPAS.Close();
                    con.DisConnect();
                    if (Blankapr == "1")
                    {
                        if (rejectsttusapr == "Rejected")
                        {
                            string hodaprid = ""; string hruid = "";
                            drPAS.Close();
                            con.DisConnect();
                            lastrec();
                            con.Insert_tble_ProfileApprovalStatus(txttytle.Text, txtFName.Text, txtSName.Text, txtLName.Text, txtAddress.Text, txtAddress2.Text, txtCity.Text, txtSTate.Text, txtCountry.Text, txtPincode.Text, txtEmailid.Text, txtStatus.Text, txtPhoneNo.Text, txtMobileNo.Text, txtDOB.Text, txtFatherName.Text, txtHusBandName.Text, txtAddressPermanent.Text, txtAddress2Permanent.Text, txtCityPermanent.Text, txtStatePermanent.Text, txtPanNo.Text, txtACNO.Text, simgname.ToString(), approvalstatus, updatedate, Session["uid"].ToString(), Session["uid"].ToString(), Session["Company"].ToString(), change_status, hodaprid, lblnofchange, hruid, imgname1, Address, Address2, City, STate, Country, Pincode, Emailid, Status, PhoneNo, MobileNo, DOB, fatherName, HusBandName, AddressPermanent, AddressPermanent3, CityPermanent, statePermanet, PanNo, ACNO, profilephoto, Session["hr_email2"].ToString(), Session["hod_email2"].ToString(), Session["HRName"].ToString(), Session["hod_Name2"].ToString(), simgname.ToString());
                            con.DisConnect();
                            DateTime dob3 = DateTime.ParseExact(txtDOB.Text, "d/M/yyyy", null);
                            Portalcon.Update_Navision_Resolved_Profile(txttytle.Text, txtFName.Text, txtSName.Text, txtLName.Text, txtAddress.Text, txtAddress2.Text, txtCity.Text, txtCountry.Text, txtPincode.Text, txtEmailid.Text, txtStatus.Text, txtPhoneNo.Text, txtMobileNo.Text, dob3.ToString("yyyy-MM-dd"), txtFatherName.Text, txtHusBandName.Text, txtAddressPermanent.Text, txtAddress2Permanent.Text, txtCityPermanent.Text, txtStatePermanent.Text, txtPanNo.Text, txtACNO.Text, Session["CompanyTableEmployee"].ToString(), Session["uid"].ToString(), simgname.ToString(), imgname1, txtSTate.Text);
                            Portalcon.DisConnect();
                            con.Update_tble_ProfileApprovalStatus_ResolvedBlankcase(System.DateTime.Now.ToString("dd/MM/yyyy"), Session["uid"].ToString(), updatedate, lblnofchange, Session["Company"].ToString());
                            con.DisConnect();

                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Profile updated successfully');", true);

                            if (EmailHR == "True")
                            {
                                subject1 = "Application For Profile Changes ";

                                Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have change my Profile Detail " + change_status + " , I need to update this detail ", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                                ShowMailData(Session["hr_email2"].ToString());
                            }
                            if (EmailHOD == "True")
                            {
                                subject1 = "Application For Profile Changes";

                                Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "" + Session["hod_Name2"].ToString() + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "I have change my Profile Detail " + change_status + " , I need to update this detail ", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                                ShowMailData(Session["hod_email2"].ToString());
                            
                            }
                        }
                        else
                        {
                            string hodaprid = ""; string hruid = "";
                            con.update_tble_ProfileApprovalStatus(txttytle.Text, txtFName.Text, txtSName.Text, txtLName.Text, txtAddress.Text, txtAddress2.Text, txtCity.Text, txtSTate.Text, txtCountry.Text, txtPincode.Text, txtEmailid.Text, txtStatus.SelectedValue.ToString(), txtPhoneNo.Text, txtMobileNo.Text, txtDOB.Text, txtFatherName.Text, txtHusBandName.Text, txtAddressPermanent.Text, txtAddress2Permanent.Text, txtCityPermanent.Text, txtStatePermanent.Text, txtPanNo.Text, txtACNO.Text, simgname.ToString(), approvalstatus, updatedate, Session["uid"].ToString(), Session["uid"].ToString(), Session["Company"].ToString(), change_status, hodaprid, txtHOD.Text, hruid, imgname1, Address, Address2, City, STate, Country, Pincode, Emailid, Status, PhoneNo, MobileNo, DOB, fatherName, HusBandName, AddressPermanent, AddressPermanent3, CityPermanent, statePermanet, PanNo, ACNO, profilephoto, Session["hr_email2"].ToString(), Session["hod_email2"].ToString(), Session["HRName"].ToString(), Session["hod_Name2"].ToString());
                            DateTime dob3 = DateTime.ParseExact(txtDOB.Text, "d/M/yyyy", null);
                            Portalcon.Update_Navision_Resolved_Profile(txttytle.Text, txtFName.Text, txtSName.Text, txtLName.Text, txtAddress.Text, txtAddress2.Text, txtCity.Text, txtCountry.Text, txtPincode.Text, txtEmailid.Text, txtStatus.Text, txtPhoneNo.Text, txtMobileNo.Text, dob3.ToString("yyyy-MM-dd"), txtFatherName.Text, txtHusBandName.Text, txtAddressPermanent.Text, txtAddress2Permanent.Text, txtCityPermanent.Text, txtStatePermanent.Text, txtPanNo.Text, txtACNO.Text, Session["CompanyTableEmployee"].ToString(), Session["uid"].ToString(), simgname.ToString(), imgname1, txtSTate.Text);
                            Portalcon.DisConnect();
                            con.Update_tble_ProfileApprovalStatus_ResolvedBlankcase(System.DateTime.Now.ToString("dd/MM/yyyy"), Session["uid"].ToString(), updatedate, lblnofchange, Session["Company"].ToString());
                            con.DisConnect();

                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Profile updated successfully');", true);

                            if (EmailHR == "True")
                            {
                                subject1 = "Application For Profile Changes Updated";

                                Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have change my Profile Detail " + change_status + " , I need to update this detail ", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                                ShowMailData(Session["hr_email2"].ToString());
                            }
                            if (EmailHOD == "True")
                            {
                                subject1 = "Application For Profile Changes";

                                Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "" + Session["hod_Name2"].ToString() + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "I have change my Profile Detail " + change_status + " , I need to update this detail ", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                                ShowMailData(Session["hod_email2"].ToString());
                            }
                        }
                    }
                    if (PriorityHODapr == "1")
                    {
                        if (rejectsttusapr == "Rejected")
                        {
                            string hruid = "";
                            drPAS.Close();
                            con.DisConnect();
                            lastrec();
                            con.Insert_tble_ProfileApprovalStatus(txttytle.Text, txtFName.Text, txtSName.Text, txtLName.Text, txtAddress.Text, txtAddress2.Text, txtCity.Text, txtSTate.Text, txtCountry.Text, txtPincode.Text, txtEmailid.Text, txtStatus.Text, txtPhoneNo.Text, txtMobileNo.Text, txtDOB.Text, txtFatherName.Text, txtHusBandName.Text, txtAddressPermanent.Text, txtAddress2Permanent.Text, txtCityPermanent.Text, txtStatePermanent.Text, txtPanNo.Text, txtACNO.Text, simgname.ToString(), approvalstatus, updatedate, Session["uid"].ToString(), Session["uid"].ToString(), Session["Company"].ToString(), change_status, hoduserid, lblnofchange, hruid, imgname1, Address, Address2, City, STate, Country, Pincode, Emailid, Status, PhoneNo, MobileNo, DOB, fatherName, HusBandName, AddressPermanent, AddressPermanent3, CityPermanent, statePermanet, PanNo, ACNO, profilephoto, Session["hr_email2"].ToString(), Session["hod_email2"].ToString(), Session["HRName"].ToString(), Session["hod_Name2"].ToString(), simgname.ToString());
                            con.DisConnect();
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Profile changes will be consider after approval');", true);
                            subject1 = "Application For Profile Changes";

                            Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "" + Session["hod_Name2"].ToString() + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "I have change my Profile Detail " + change_status + " , I need to update this detail ", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                            ShowMailData(Session["hod_email2"].ToString());
                            if (EmailHR == "True")
                            {
                                subject1 = "Application For Profile Changes ";

                                Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have change my Profile Detail " + change_status + " , I need to update this detail ", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                                ShowMailData(Session["hr_email2"].ToString());


                            }
                        }
                        else
                        {
                            string hruid = "";
                            con.update_tble_ProfileApprovalStatus(txttytle.Text, txtFName.Text, txtSName.Text, txtLName.Text, txtAddress.Text, txtAddress2.Text, txtCity.Text, txtSTate.Text, txtCountry.Text, txtPincode.Text, txtEmailid.Text, txtStatus.SelectedValue.ToString(), txtPhoneNo.Text, txtMobileNo.Text, txtDOB.Text, txtFatherName.Text, txtHusBandName.Text, txtAddressPermanent.Text, txtAddress2Permanent.Text, txtCityPermanent.Text, txtStatePermanent.Text, txtPanNo.Text, txtACNO.Text, simgname.ToString(), approvalstatus, updatedate, Session["uid"].ToString(), Session["uid"].ToString(), Session["Company"].ToString(), change_status, hoduserid, txtHOD.Text, hruid, imgname1, Address, Address2, City, STate, Country, Pincode, Emailid, Status, PhoneNo, MobileNo, DOB, fatherName, HusBandName, AddressPermanent, AddressPermanent3, CityPermanent, statePermanet, PanNo, ACNO, profilephoto, Session["hr_email2"].ToString(), Session["hod_email2"].ToString(), Session["HRName"].ToString(), Session["hod_Name2"].ToString());
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Profile changes will be consider after approval');", true);
                            subject1 = "Application For Profile Changes";

                            Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "" + Session["hod_Name2"].ToString() + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "I have change my Profile Detail " + change_status + " , I need to update this detail ", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                            ShowMailData(Session["hod_email2"].ToString());

                            if (EmailHR == "True")
                            {
                                subject1 = "Application For Profile Changes ";

                                Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have change my Profile Detail " + change_status + " , I need to update this detail ", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                                ShowMailData(Session["hr_email2"].ToString());
                            }

                        }
                    }
                    if(PriorityHRapr=="1")
                    {
                        if (rejectsttusapr == "Rejected")
                        {
                            string hodaprid = "";

                            drPAS.Close();
                            con.DisConnect();
                            lastrec();
                            con.Insert_tble_ProfileApprovalStatus(txttytle.Text, txtFName.Text, txtSName.Text, txtLName.Text, txtAddress.Text, txtAddress2.Text, txtCity.Text, txtSTate.Text, txtCountry.Text, txtPincode.Text, txtEmailid.Text, txtStatus.Text, txtPhoneNo.Text, txtMobileNo.Text, txtDOB.Text, txtFatherName.Text, txtHusBandName.Text, txtAddressPermanent.Text, txtAddress2Permanent.Text, txtCityPermanent.Text, txtStatePermanent.Text, txtPanNo.Text, txtACNO.Text, simgname.ToString(), approvalstatus, updatedate, Session["uid"].ToString(), Session["uid"].ToString(), Session["Company"].ToString(), change_status, hodaprid, lblnofchange, lblHRUserId.Text, imgname1, Address, Address2, City, STate, Country, Pincode, Emailid, Status, PhoneNo, MobileNo, DOB, fatherName, HusBandName, AddressPermanent, AddressPermanent3, CityPermanent, statePermanet, PanNo, ACNO, profilephoto, Session["hr_email2"].ToString(), Session["hod_email2"].ToString(), Session["HRName"].ToString(), Session["hod_Name2"].ToString(), simgname.ToString());
                            con.DisConnect();
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Profile changes will be consider after approval');", true);
                            subject1 = "Application For Profile Changes ";

                            Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have change my Profile Detail " + change_status + " , I need to update this detail ", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                            ShowMailData(Session["hr_email2"].ToString());
                            if (EmailHOD == "True")
                            {
                                subject1 = "Application For Profile Changes";

                                Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "" + Session["hod_Name2"].ToString() + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "I have change my Profile Detail " + change_status + " , I need to update this detail ", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                                ShowMailData(Session["hod_email2"].ToString());
                            }

                        }
                        else
                        {
                            string hodaprid = "";
                            con.update_tble_ProfileApprovalStatus(txttytle.Text, txtFName.Text, txtSName.Text, txtLName.Text, txtAddress.Text, txtAddress2.Text, txtCity.Text, txtSTate.Text, txtCountry.Text, txtPincode.Text, txtEmailid.Text, txtStatus.SelectedValue.ToString(), txtPhoneNo.Text, txtMobileNo.Text, txtDOB.Text, txtFatherName.Text, txtHusBandName.Text, txtAddressPermanent.Text, txtAddress2Permanent.Text, txtCityPermanent.Text, txtStatePermanent.Text, txtPanNo.Text, txtACNO.Text, simgname.ToString(), approvalstatus, updatedate, Session["uid"].ToString(), Session["uid"].ToString(), Session["Company"].ToString(), change_status, hodaprid, txtHOD.Text, lblHRUserId.Text, imgname1, Address, Address2, City, STate, Country, Pincode, Emailid, Status, PhoneNo, MobileNo, DOB, fatherName, HusBandName, AddressPermanent, AddressPermanent3, CityPermanent, statePermanet, PanNo, ACNO, profilephoto, Session["hr_email2"].ToString(), Session["hod_email2"].ToString(), Session["HRName"].ToString(), Session["hod_Name2"].ToString());
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Profile changes will be consider after approval');", true);
                            subject1 = "Application For Profile Changes ";

                            Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have change my Profile Detail " + change_status + " , I need to update this detail ", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                            ShowMailData(Session["hr_email2"].ToString());
                            if (EmailHOD == "True")
                            {
                                subject1 = "Application For Profile Changes";

                                Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "" + Session["hod_Name2"].ToString() + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "I have change my Profile Detail " + change_status + " , I need to update this detail ", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                                ShowMailData(Session["hod_email2"].ToString());
                            }

                        }
                    }
                   
                
                }

                else
                {
                    if (Blankapr == "1")
                    {
                        string hodaprid = ""; string hruid = "";
                        drPAS.Close();
                        con.DisConnect();
                        lastrec();
                        con.Insert_tble_ProfileApprovalStatus(txttytle.Text, txtFName.Text, txtSName.Text, txtLName.Text, txtAddress.Text, txtAddress2.Text, txtCity.Text, txtSTate.Text, txtCountry.Text, txtPincode.Text, txtEmailid.Text, txtStatus.Text, txtPhoneNo.Text, txtMobileNo.Text, txtDOB.Text, txtFatherName.Text, txtHusBandName.Text, txtAddressPermanent.Text, txtAddress2Permanent.Text, txtCityPermanent.Text, txtStatePermanent.Text, txtPanNo.Text, txtACNO.Text, simgname.ToString(), approvalstatus, updatedate, Session["uid"].ToString(), Session["uid"].ToString(), Session["Company"].ToString(), change_status, hodaprid, lblnofchange, hruid, imgname1, Address, Address2, City, STate, Country, Pincode, Emailid, Status, PhoneNo, MobileNo, DOB, fatherName, HusBandName, AddressPermanent, AddressPermanent3, CityPermanent, statePermanet, PanNo, ACNO, profilephoto, Session["hr_email2"].ToString(), Session["hod_email2"].ToString(), Session["HRName"].ToString(), Session["hod_Name2"].ToString(), simgname.ToString());
                        con.DisConnect();
                        DateTime dob3 = DateTime.ParseExact(txtDOB.Text, "d/M/yyyy", null);
                        Portalcon.Update_Navision_Resolved_Profile(txttytle.Text, txtFName.Text, txtSName.Text, txtLName.Text, txtAddress.Text, txtAddress2.Text, txtCity.Text, txtCountry.Text, txtPincode.Text, txtEmailid.Text, txtStatus.Text, txtPhoneNo.Text, txtMobileNo.Text, dob3.ToString("yyyy-MM-dd"), txtFatherName.Text, txtHusBandName.Text, txtAddressPermanent.Text, txtAddress2Permanent.Text, txtCityPermanent.Text, txtStatePermanent.Text, txtPanNo.Text, txtACNO.Text, Session["CompanyTableEmployee"].ToString(), Session["uid"].ToString(), simgname.ToString(), imgname1, txtSTate.Text);
                        Portalcon.DisConnect();
                        con.Update_tble_ProfileApprovalStatus_ResolvedBlankcase(System.DateTime.Now.ToString("dd/MM/yyyy"), Session["uid"].ToString(), updatedate, lblnofchange, Session["Company"].ToString());
                        con.DisConnect();

                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Profile updated successfully');", true);
                        if (EmailHR == "True")
                        {
                            subject1 = "Application For Profile Changes";

                            Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have change my Profile Detail " + change_status + " , I need to update this detail ", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                            ShowMailData(Session["hr_email2"].ToString());
                        }
                        if (EmailHOD == "True")
                        {
                            subject1 = "Application For Profile Changes";

                            Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "" + Session["hod_Name2"].ToString() + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "I have change my Profile Detail " + change_status + " , I need to update this detail ", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                            ShowMailData(Session["hod_email2"].ToString());

                        }
                    }
                    if (PriorityHODapr == "1")
                    {
                        string hruid = "";
                        drPAS.Close();
                        con.DisConnect();
                        lastrec();
                        con.Insert_tble_ProfileApprovalStatus(txttytle.Text, txtFName.Text, txtSName.Text, txtLName.Text, txtAddress.Text, txtAddress2.Text, txtCity.Text, txtSTate.Text, txtCountry.Text, txtPincode.Text, txtEmailid.Text, txtStatus.Text, txtPhoneNo.Text, txtMobileNo.Text, txtDOB.Text, txtFatherName.Text, txtHusBandName.Text, txtAddressPermanent.Text, txtAddress2Permanent.Text, txtCityPermanent.Text, txtStatePermanent.Text, txtPanNo.Text, txtACNO.Text, simgname.ToString(), approvalstatus, updatedate, Session["uid"].ToString(), Session["uid"].ToString(), Session["Company"].ToString(), change_status, hoduserid, lblnofchange, hruid, imgname1, Address, Address2, City, STate, Country, Pincode, Emailid, Status, PhoneNo, MobileNo, DOB, fatherName, HusBandName, AddressPermanent, AddressPermanent3, CityPermanent, statePermanet, PanNo, ACNO, profilephoto, Session["hr_email2"].ToString(), Session["hod_email2"].ToString(), Session["HRName"].ToString(), Session["hod_Name2"].ToString(), simgname.ToString());
                        con.DisConnect();
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Profile changes will be consider after approval');", true);
                        if (EmailHR == "True")
                        {

                            subject1 = "Application For Profile Changes";

                            Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have change my Profile Detail " + change_status + " , I need to update this detail ", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                            ShowMailData(Session["hr_email2"].ToString());
                        }

                        subject1 = "Application For Profile Changes";

                        Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "" + Session["hod_Name2"].ToString() + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "I have change my Profile Detail " + change_status + " , I need to update this detail ", Environment.NewLine,  Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                        ShowMailData(Session["hod_email2"].ToString());

                    }
                    if (PriorityHRapr == "1")
                    {
                        string hodaprid = "";
                       
                        drPAS.Close();
                        con.DisConnect();
                        lastrec();
                        con.Insert_tble_ProfileApprovalStatus(txttytle.Text, txtFName.Text, txtSName.Text, txtLName.Text, txtAddress.Text, txtAddress2.Text, txtCity.Text, txtSTate.Text, txtCountry.Text, txtPincode.Text, txtEmailid.Text, txtStatus.Text, txtPhoneNo.Text, txtMobileNo.Text, txtDOB.Text, txtFatherName.Text, txtHusBandName.Text, txtAddressPermanent.Text, txtAddress2Permanent.Text, txtCityPermanent.Text, txtStatePermanent.Text, txtPanNo.Text, txtACNO.Text, simgname.ToString(), approvalstatus, updatedate, Session["uid"].ToString(), Session["uid"].ToString(), Session["Company"].ToString(), change_status, hodaprid, lblnofchange, lblHRUserId.Text, imgname1, Address, Address2, City, STate, Country, Pincode, Emailid, Status, PhoneNo, MobileNo, DOB, fatherName, HusBandName, AddressPermanent, AddressPermanent3, CityPermanent, statePermanet, PanNo, ACNO, profilephoto, Session["hr_email2"].ToString(), Session["hod_email2"].ToString(), Session["HRName"].ToString(), Session["hod_Name2"].ToString(), simgname.ToString());
                        con.DisConnect();
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Profile changes will be consider after approval');", true);
                        if (EmailHOD=="")
                        {
                            subject1 = "Application For Profile Changes";

                            Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "" + Session["hod_Name2"].ToString() + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "I have change my Profile Detail " + change_status + " , I need to update this detail ", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                            ShowMailData(Session["hod_email2"].ToString());
                        }
                        subject1 = "Application For Profile Changes";

                        Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have change my Profile Detail " + change_status + " , I need to update this detail ", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                        ShowMailData(Session["hr_email2"].ToString());

                    }
                    //drPAS.Close();
                    //con.DisConnect();
                    //lastrec();
                    //con.Insert_tble_ProfileApprovalStatus(txtAddress.Text, txtAddress2.Text, txtCity.Text, txtSTate.Text, txtCountry.Text, txtPincode.Text, txtEmailid.Text, txtStatus.Text, txtPhoneNo.Text, txtMobileNo.Text, txtDOB.Text, txtFatherName.Text, txtHusBandName.Text, txtMotherName.Text, txtAddressPermanent.Text, txtAddress2Permanent.Text, txtCityPermanent.Text, txtStatePermanent.Text, txtCountryPermanent.Text, txtPincodePermanent.Text, txtPanNo.Text, txtACNO.Text, imgname, approvalstatus, updatedate, Session["uid"].ToString(), Session["uid"].ToString(), Session["Company"].ToString(), change_status, hoduserid, lblnofchange, txtHOD.Text, lblHRUserId.Text);
                   
                }
            }
        }
        else
        {
            dr.Close();
            Portalcon.DisConnect();
           // con.DisConnect();
        }
       
    }

    public void Show_PincodewithCity()
    {

        string stable = Session["Company"].ToString();
        string rtable = stable.Replace(".", "_");
        string tblNameEmployee = "[" + rtable + "$Post Code" + "]";
      
        SqlDataReader dr = Portalcon.Show_PincodewithCity(tblNameEmployee,txtPincode.Text);
        dr.Read();
        if (dr.HasRows)
        {
            txtCity.Text = dr["City"].ToString();
        }
        dr.Close();
                
        Portalcon.DisConnect();
    }

    public void Show_PincodewithCode()
    {

        string stable = Session["Company"].ToString();
        string rtable = stable.Replace(".", "_");
        string tblNameEmployee = "[" + rtable + "$Post Code" + "]";

        SqlDataReader dr = Portalcon.Show_PincodewithPincode(tblNameEmployee, txtCity.Text);
        dr.Read();
        if (dr.HasRows)
        {
            txtPincode.Text = dr["Code"].ToString();
        }
        dr.Close();

        Portalcon.DisConnect();
    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCompletionList(string prefixText, int count)
    {
        //using (SqlConnection con = new SqlConnection())
        //{
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["str"].ConnectionString;
            SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["str"].ToString());
       
            using (SqlCommand com = new SqlCommand())
            {
                string cname1 = HttpContext.Current.Session["Company"].ToString();
                string stable = cname1;
                string rtable = stable.Replace(".", "_");
                string tblNameEmployee = "[" + rtable + "$Post Code" + "]";
                com.CommandText = "select City from " + tblNameEmployee + " where City like @City+'%'";

                //com.CommandText = "select City from " + tblNameEmployee + " where (UPPER([Userid]) LIKE UPPER('%" @City "%'))";
              
                com.Parameters.AddWithValue("@City", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["City"].ToString());
                    }
                }
                con.Close();
                return countryNames;


            }

        //}

    }  


    protected void txtCity_TextChanged(object sender, EventArgs e)
    {
        Show_PincodewithCode();
    }
    protected void txtPincode_SelectedIndexChanged(object sender, EventArgs e)
    {
        Show_PincodewithCity();
    }
}