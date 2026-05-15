using paytm;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_StudentNoDuesTMMC : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    private string randomnumber;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                binddata();
                numberseries();
                getdepartment();
                alldept();
                binddate();              
                divnodues.Visible = true;
                hidenodues();
                hidesave();
                hidePaymentbutton();
                PaymentVerifyButton();
                if (txtgender.Text == "Male")
                {
                    lblmr.Visible = true;
                    lblson.Visible = true;
                    Label9.Visible = true;
                    Label13.Visible = true;
                }
                else
                {
                    lblms.Visible = true;
                    lbldaug.Visible = true;
                    Label11.Visible = true;
                    Label18.Visible = true;
                }
                txtdatedirectorprincipaldate.Text = String.Format("{0}", DateTime.Now.ToString("dd/MM/yyyy"));
                lblprinteddateandtime.Text = String.Format("{0}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                SqlDataAdapter da1 = new SqlDataAdapter("select No_,[Enrollment No_] ,[Student Name],[Course Name],[Fathers Name],No_,'20' +RIGHT([Academic Year], 2) AS AcademicYear1,[Global Dimension 1 Code],[Course Name],[Admitted Year] ,case when [Student Status]=1 then 'Student' when [Student Status]=2 then 'Inactive' when [Student Status]=3 then 'Alumini' when [Student Status]=4 then 'NR' when [Student Status]=5 then 'Re-Appear' when [Student Status]=6 then 'Intership'  end 'Student Status',[Course Code],[Admitted Year],[Mobile Number],case when [Father Mobile No]='' then [Gaurdian Mobile No] else [Father Mobile No] end as PMobile,Address1 from [TMU$Student - COLLEGE] where No_='" + Session["uid"].ToString() + "'", con1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);

                if (dt1.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("STUDENT NO. : " + dt1.Rows[0]["No_"].ToString() + "");
                    sb.Append(Environment.NewLine);
                    sb.Append("ENROLLMENT NO. : " + dt1.Rows[0]["Enrollment No_"].ToString() + "");
                    sb.Append(Environment.NewLine);
                    sb.Append("STUDENT NAME : " + dt1.Rows[0]["Student Name"].ToString() + "");
                    sb.Append(Environment.NewLine);
                    sb.Append("PROGRAM : " + dt1.Rows[0]["Course Name"].ToString() + "");
                    sb.Append(Environment.NewLine);
                    sb.Append("PASSOUT YEAR : " + dt1.Rows[0]["AcademicYear1"].ToString() + "");
                    sb.Append(Environment.NewLine);
                    sb.Append("Mobile : " + dt1.Rows[0]["Mobile Number"].ToString() + "");
                    //sb.Append(Environment.NewLine);
                    //sb.Append("Address : " + dt1.Rows[0]["Address1"].ToString() + "");
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(sb.ToString(), QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    Bitmap qrCodeImage = qrCode.GetGraphic(30);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] byteImage = ms.ToArray();
                        imgBarcode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                        imgBarcode.Visible = true;
                    }
                }
            }
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }

    }
    public void numberseries()
    {

        SqlDataAdapter sda = new SqlDataAdapter("Select * from Tbl_StudentNodues", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        int i = dt.Rows.Count;
        string Nodues = Convert.ToString(i + 1);
        txtnoduesid.Text += i.ToString("TMU/Exam/NoDues/244001") + Nodues;

    }
    public void binddata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        string strSQL = "select Case when Gender=1 then 'Male' else 'Female' end as Gender, '20' +RIGHT([Academic Year], 2) AS AcademicYear1, (Select [Exam Course Name] from [TMU$Course - COLLEGE] where [Code]=P.[Course Code]) As CourseName, *  from [EDUCOLLEGELIVE-R2].dbo.[TMU$Student - COLLEGE] as P where [Enrollment No_]='" + Session["enroll"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        txtenrollmentno.Text = dt.Rows[0]["Enrollment No_"].ToString();
        txtstudentName.Text = dt.Rows[0]["Student Name"].ToString();
        lblname.Text = dt.Rows[0]["Student Name"].ToString();
        lblenrollment.Text = dt.Rows[0]["Enrollment No_"].ToString();
        lblfathername.Text = dt.Rows[0]["Fathers Name"].ToString();
        txtfathername.Text = dt.Rows[0]["Fathers Name"].ToString();
        //txtmobile.Text = dt.Rows[0]["Mobile Number"].ToString();
        // txtemailid.Text = dt.Rows[0]["E-Mail Address"].ToString();
        txtPrograme.Text = dt.Rows[0]["Course Name"].ToString();
        lblprogram.Text = dt.Rows[0]["CourseName"].ToString();
        txtcollegedept.Text = dt.Rows[0]["Global Dimension 1 Code"].ToString();
        //lblcertifiedname.Text = dt.Rows[0]["Student Name"].ToString();
        //lblYear.Text = dt.Rows[0]["Academic Year"].ToString();      
        txtgender.Text = dt.Rows[0]["Gender"].ToString();
        lblacedmicyear.Text = dt.Rows[0]["AcademicYear1"].ToString();
        lblacedmicyear.Text = dt.Rows[0]["AcademicYear1"].ToString();
        txtstno.Text = dt.Rows[0]["No_"].ToString();
        //lblacedmicyear.Text= dt.Rows[0]["Academic Year"].ToString();
    }
    public void getdepartment()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("sp_getlibrarydeptstudentNodues", con);
        cmd.Parameters.AddWithValue("@enroll", Session["enroll"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
       

        lblcentlibname.Text = dtCL.Select("Particulars='Central Library'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblcentlibdeg.Text = dtCL.Select("Particulars='Central Library'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblcentrallibrarycode.Text= dtCL.Select("Particulars='Central Library'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();
        // lblprincipalcodelab.Text = dtCL.Select("Particulars='Central Library'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblaccountname.Text = dtCL.Select("Particulars='Account Deptt'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblaccountdeg.Text = dtCL.Select("Particulars='Account Deptt'").CopyToDataTable().Rows[0]["Designation"].ToString();
        txtaccountemployeecode.Text = dtCL.Select("Particulars='Account Deptt'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();


        lblhostelsname.Text = dtCL.Select("Particulars='Hostel'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblhosteldeg.Text = dtCL.Select("Particulars='Hostel'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblhostelcode.Text = dtCL.Select("Particulars='Hostel'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();
        ////lbltransportofficenamecode.Text = dtCL.Select("Particulars='Transport Office'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblhostelmasname.Text = dtCL.Select("Particulars='Hostel'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblhostelmescode.Text = dtCL.Select("Particulars='Hostel'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblwashermanname.Text = dtCL.Select("Particulars='Hostel'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblwashermandeg.Text = dtCL.Select("Particulars='Hostel'").CopyToDataTable().Rows[0]["Designation"].ToString();
        ////lbltransportofficenamecode.Text = dtCL.Select("Particulars='Transport Office'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();



        lblsportname.Text = dtCL.Select("Particulars='Sport I/c'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblsportdeg.Text = dtCL.Select("Particulars='Sport I/c'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblsportnamecode.Text = dtCL.Select("Particulars='Sport I/c'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblITname.Text = dtCL.Select("Particulars='IT Department'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblITdeg.Text = dtCL.Select("Particulars='IT Department'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblITnamecode.Text = dtCL.Select("Particulars='IT Department'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();


        lblCommunityMedicinename.Text= dtCL.Select("Particulars='Community Medicine'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblCommunityMedicinecode.Text = dtCL.Select("Particulars='Community Medicine'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblGeneralMedicinename.Text= dtCL.Select("Particulars='General Medicine'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblGeneralMedicicode.Text = dtCL.Select("Particulars='General Medicine'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblPsychiatryname.Text = dtCL.Select("Particulars='Psychiatry'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblPsychiatrycode.Text = dtCL.Select("Particulars='Psychiatry'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblGeneralSurgeryname.Text = dtCL.Select("Particulars='General Surgery'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblGeneralSurgerycode.Text = dtCL.Select("Particulars='General Surgery'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblAnesthisianame.Text = dtCL.Select("Particulars='Anesthisia'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblAnesthisiacode.Text = dtCL.Select("Particulars='Anesthisia'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblObsGyanename.Text = dtCL.Select("Particulars='Obs & Gyane'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblObsGyanecode.Text = dtCL.Select("Particulars='Obs & Gyane'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblPediatricsname.Text = dtCL.Select("Particulars='Pediatrics'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblPediatricscode.Text = dtCL.Select("Particulars='Pediatrics'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblOrthopedicsname.Text = dtCL.Select("Particulars='Orthopedics'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblOrthopedicscode.Text = dtCL.Select("Particulars='Orthopedics'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblEntname.Text = dtCL.Select("Particulars='Ent'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblEntcode.Text = dtCL.Select("Particulars='Ent'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblOphthalmologyname.Text = dtCL.Select("Particulars='Ophthalmology'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblOphthalmologycode.Text = dtCL.Select("Particulars='Ophthalmology'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblCasualtyname.Text = dtCL.Select("Particulars='Casualty'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblCasualtycode.Text = dtCL.Select("Particulars='Casualty'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblDermatologyname.Text = dtCL.Select("Particulars='Dermatology'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblDermatologycode.Text = dtCL.Select("Particulars='Dermatology'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblTbChestname.Text = dtCL.Select("Particulars='Tb & Chest (Pulmonary Medicine)'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblTbChestcode.Text = dtCL.Select("Particulars='Tb & Chest (Pulmonary Medicine)'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblRadiologyname.Text = dtCL.Select("Particulars='Radiology'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblRadiologycode.Text = dtCL.Select("Particulars='Radiology'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblPathlogyname.Text = dtCL.Select("Particulars='Pathlogy(Blood Bank)'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblPathlogycode.Text = dtCL.Select("Particulars='Pathlogy(Blood Bank)'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblForensicMedicinename.Text = dtCL.Select("Particulars='Forensic Medicine'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblForensicMedicinecode.Text = dtCL.Select("Particulars='Forensic Medicine'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();
    }
    public void alldept()
    {
        Decimal Total = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "Select [Status of Dues],Remark,Particular,[Dept Employee Name],[Dept Employee Code],isnull([Pending Amount],0) 'Pending Amount',Designation,ID,isnull((select top 1 [Temp 2] from  [EDUCOLLEGELIVE-R2].dbo.OnlinePaymentLog where UserID='" + Session["uid"].ToString() + "' and OrderID like 'TMUNODUES%' and GatewayStatus=1),-1) as PayStatus from Tbl_StudentOutstanigNoDues where [Enrollment No_] ='" + txtenrollmentno.Text + "'  ";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["PayStatus"].ToString() == "-1")
            {
                PayStatus.Text = "OPEN";
            }
            else if (dt.Rows[0]["PayStatus"].ToString() == "0")
            {
                PayStatus.Text = "PENDING";
                lnkPay.Visible = false;
            }
            else if (dt.Rows[0]["PayStatus"].ToString() == "1")
            {
                PayStatus.Text = "Verified";
                PayStatus.ForeColor = System.Drawing.Color.Green;
                lnkPay.Visible = false;
            }
            else
            {
                PayStatus.Text = "ERROR";
                lnkPay.Visible = false;
            }
            DataRow[] dr1 = dt.Select("Particular = 'Central Library'");
            if (dr1.Length > 0)
            {
                //DropDownList2.SelectedItem.Text = dr1[0]["Status of Dues"].ToString();
                txtremarkcentrallib.Text = dr1[0]["Remark"].ToString();
                TextBox2.Text = dr1[0]["Pending Amount"].ToString();

                if (dr1[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr1[0]["Pending Amount"]);
                }
                // txtCentrallibID.Text = dr1[0]["ID"].ToString();
            }
           
            DataRow[] dr7 = dt.Select("Particular = 'Sports'");
            if (dr7.Length > 0)
            {
                //DrpSport.SelectedItem.Text = dr7[0]["Status of Dues"].ToString();
                txtremarksportic.Text = dr7[0]["Remark"].ToString();
                //txtSportID.Text = dr7[0]["ID"].ToString();
                TextBox8.Text = dr7[0]["Pending Amount"].ToString();
                if (dr7[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr7[0]["Pending Amount"]);
                }
            }

            DataRow[] dr97 = dt.Select("Particular = 'IT Department'");
            if (dr97.Length > 0)
            {
                //DrpSport.SelectedItem.Text = dr7[0]["Status of Dues"].ToString();
                txtremarkIT.Text = dr97[0]["Remark"].ToString();
                //txtSportID.Text = dr7[0]["ID"].ToString();
                TextBox98.Text = dr97[0]["Pending Amount"].ToString();
                if (dr97[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr97[0]["Pending Amount"]);
                }
            }
            DataRow[] dr18 = dt.Select("Particular = 'Other Fee'");
            if (dr18.Length > 0)
            {
                //drpotherfeee.SelectedItem.Text = dr18[0]["Status of Dues"].ToString();
                //lblotherfinename.Text= dr18[0]["Dept Employee Name"].ToString();
                //lblotherfeedeg.Text= dr18[0]["Designation"].ToString();
                TextBox17.Text = dr18[0]["Remark"].ToString();
                TextBox18.Text = dr18[0]["Pending Amount"].ToString();
                if (dr18[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr18[0]["Pending Amount"]);
                }

            }

            DataRow[] dr19 = dt.Select("Particular = 'Hostel'");
            if (dr19.Length > 0)
            {
                txtremarkhostel.Text = dr19[0]["Remark"].ToString();
                TextBox5.Text = dr19[0]["Pending Amount"].ToString();
                if (dr19[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr19[0]["Pending Amount"]);
                }

            }
            DataRow[] dr20 = dt.Select("Particular = 'Hostel Mess'");
            if (dr20.Length > 0)
            {
                TextBox1.Text = dr20[0]["Remark"].ToString();
                TextBox3.Text = dr20[0]["Pending Amount"].ToString();
                if (dr20[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr20[0]["Pending Amount"]);
                }

            }
            DataRow[] dr21 = dt.Select("Particular = 'Washer Man'");
            if (dr21.Length > 0)
            {
                txtremarkwasherman.Text = dr21[0]["Remark"].ToString();
                TextBox6.Text = dr21[0]["Pending Amount"].ToString();
                if (dr21[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr20[0]["Pending Amount"]);
                }
            }
                DataRow[] dr22 = dt.Select("Particular = 'Community Medicine'");
                if (dr22.Length > 0)
                {
                    TextBox4.Text = dr22[0]["Remark"].ToString();
                    TextBox7.Text = dr22[0]["Pending Amount"].ToString();
                    if (dr22[0]["Pending Amount"] == "")
                    {
                        Total = Total + 0;
                    }
                    else
                    {
                        Total = Total + Convert.ToDecimal(dr22[0]["Pending Amount"]);
                    }

                }
            DataRow[] dr23 = dt.Select("Particular = 'General Medicine'");
            if (dr23.Length > 0)
            {
                TextBox9.Text = dr23[0]["Remark"].ToString();
                TextBox10.Text = dr23[0]["Pending Amount"].ToString();
                if (dr23[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr23[0]["Pending Amount"]);
                }

            }
            DataRow[] dr24 = dt.Select("Particular = 'Psychiatry'");
            if (dr24.Length > 0)
            {
                TextBox11.Text = dr24[0]["Remark"].ToString();
                TextBox12.Text = dr24[0]["Pending Amount"].ToString();
                if (dr24[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr24[0]["Pending Amount"]);
                }

            }
            DataRow[] dr25 = dt.Select("Particular = 'General Surgery'");
            if (dr25.Length > 0)
            {
                TextBox13.Text = dr25[0]["Remark"].ToString();
                TextBox14.Text = dr25[0]["Pending Amount"].ToString();
                if (dr25[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr25[0]["Pending Amount"]);
                }

            }

            DataRow[] dr26 = dt.Select("Particular = 'Anesthisia'");
            if (dr26.Length > 0)
            {
                TextBox16.Text = dr26[0]["Remark"].ToString();
                TextBox19.Text = dr26[0]["Pending Amount"].ToString();
                if (dr26[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr26[0]["Pending Amount"]);
                }

            }
            DataRow[] dr27 = dt.Select("Particular = 'Obs & Gyane'");
            if (dr27.Length > 0)
            {
                TextBox20.Text = dr27[0]["Remark"].ToString();
                TextBox21.Text = dr27[0]["Pending Amount"].ToString();
                if (dr27[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr27[0]["Pending Amount"]);
                }

            }
            DataRow[] dr28 = dt.Select("Particular = 'Pediatrics'");
            if (dr28.Length > 0)
            {
                TextBox22.Text = dr28[0]["Remark"].ToString();
                TextBox23.Text = dr28[0]["Pending Amount"].ToString();
                if (dr28[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr28[0]["Pending Amount"]);
                }

            }

            DataRow[] dr29 = dt.Select("Particular = 'Orthopedics'");
            if (dr29.Length > 0)
            {
                TextBox24.Text = dr29[0]["Remark"].ToString();
                TextBox25.Text = dr29[0]["Pending Amount"].ToString();
                if (dr29[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr29[0]["Pending Amount"]);
                }

            }

            DataRow[] dr30 = dt.Select("Particular = 'Ent'");
            if (dr30.Length > 0)
            {
                TextBox26.Text = dr30[0]["Remark"].ToString();
                TextBox27.Text = dr30[0]["Pending Amount"].ToString();
                if (dr30[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr30[0]["Pending Amount"]);
                }

            }

            DataRow[] dr31 = dt.Select("Particular = 'Ophthalmology'");
            if (dr31.Length > 0)
            {
                TextBox28.Text = dr31[0]["Remark"].ToString();
                TextBox29.Text = dr31[0]["Pending Amount"].ToString();
                if (dr31[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr31[0]["Pending Amount"]);
                }

            }

            DataRow[] dr32 = dt.Select("Particular = 'Casualty'");
            if (dr32.Length > 0)
            {
                TextBox30.Text = dr32[0]["Remark"].ToString();
                TextBox31.Text = dr32[0]["Pending Amount"].ToString();
                if (dr32[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr32[0]["Pending Amount"]);
                }

            }

            DataRow[] dr33 = dt.Select("Particular = 'Dermatology'");
            if (dr33.Length > 0)
            {
                TextBox32.Text = dr33[0]["Remark"].ToString();
                TextBox33.Text = dr33[0]["Pending Amount"].ToString();
                if (dr33[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr33[0]["Pending Amount"]);
                }

            }

            DataRow[] dr34 = dt.Select("Particular = 'Tb & Chest (Pulmonary Medicine)'");
            if (dr34.Length > 0)
            {
                TextBox34.Text = dr34[0]["Remark"].ToString();
                TextBox35.Text = dr34[0]["Pending Amount"].ToString();
                if (dr34[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr34[0]["Pending Amount"]);
                }

            }

            DataRow[] dr35 = dt.Select("Particular = 'Radiology'");
            if (dr35.Length > 0)
            {
                TextBox36.Text = dr35[0]["Remark"].ToString();
                TextBox37.Text = dr35[0]["Pending Amount"].ToString();
                if (dr35[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr35[0]["Pending Amount"]);
                }

            }

            DataRow[] dr36 = dt.Select("Particular = 'Pathlogy(Blood Bank)'");
            if (dr36.Length > 0)
            {
                TextBox38.Text = dr36[0]["Remark"].ToString();
                TextBox39.Text = dr36[0]["Pending Amount"].ToString();
                if (dr36[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr36[0]["Pending Amount"]);
                }

            }

            DataRow[] dr37 = dt.Select("Particular = 'Forensic Medicine'");
            if (dr37.Length > 0)
            {
                TextBox40.Text = dr37[0]["Remark"].ToString();
                TextBox41.Text = dr37[0]["Pending Amount"].ToString();
                if (dr37[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr37[0]["Pending Amount"]);
                }

            }



            lblTotal.Text = Total.ToString();

            //(Convert.ToDecimal(dr18[0]["Pending Amount"]) +
            //Convert.ToDecimal(dr17[0]["Pending Amount"]) +
            //Convert.ToDecimal(dr16[0]["Pending Amount"]) +                 
            //Convert.ToDecimal(dr1[0]["Pending Amount"]) +
            //Convert.ToDecimal(dr2[0]["Pending Amount"]) + 
            //Convert.ToDecimal(dr7[0]["Pending Amount"]) +
            //Convert.ToDecimal(dr8[0]["Pending Amount"]) +
            //Convert.ToDecimal(dr9[0]["Pending Amount"]) + 
            //Convert.ToDecimal(dr10[0]["Pending Amount"]) + 
            //Convert.ToDecimal(dr11[0]["Pending Amount"]) +
            //Convert.ToDecimal(dr12[0]["Pending Amount"]) + 
            //Convert.ToDecimal(dr13[0]["Pending Amount"]) +
            //Convert.ToDecimal(dr14[0]["Pending Amount"]) +
            //Convert.ToDecimal(dr15[0]["Pending Amount"])).ToString();
        }

    }

    public void SMS(String MobileNo, string Msg)
    {
        //  Website: http://www.universalsmsadvertising.com/     //  Username: 9837016352   //  Password: 9837016352 
        MobileNo = "91" + txtmobile.Text;
        // MobileNo = "91" + 9015762885;
        string url = "http://www.universalsmsadvertising.com/universalsmsapi.php?user_name=9837016352&user_password=9837016352&mobile=" + MobileNo + "&sender_id=TMUniv&type=F&text=" + Msg + "";
        // string url = "http://www.universalsmsadvertising.com/universalsmsapi.php?user_name=myuserid&user_password=mypassword&mobile=919834567120&sender_id=SWEET&type=F&text= " + Msg + "";
        System.Net.HttpWebRequest fr;
        Uri targetURI = new Uri(url);
        fr = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(targetURI);
        if (fr.GetResponse().ContentLength > 0)
        {
            System.IO.StreamReader str = new System.IO.StreamReader(fr.GetResponse().GetResponseStream());
            Response.Write(str.ReadToEnd());
            str.Close();
        }
    }
    private string GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters)
    {
        string sOTP = String.Empty;

        string sTempChars = String.Empty;

        Random rand = new Random();

        for (int i = 0; i < iOTPLength; i++)
        {

            int p = rand.Next(0, saAllowedCharacters.Length);
            sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
            sOTP += sTempChars;
        }
        return sOTP;
    }
    protected void btnsendOtp_Click(object sender, EventArgs e)
    {
        if (txtmobile.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Mobile No.')", true);
            return;
        }
        pnl1.Visible = false;
        pnl2.Visible = true;
        string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        string sRandomOTP = GenerateRandomOTP(4, saAllowedCharacters);
        SMS("MobileNo", "Dear Student, Your No dues confirmation OTP is " + sRandomOTP + ". Thank you. TEERTHANKER MAHAVEER UNIVERSITY");
        string somestring = txtmobile.Text;
        StringBuilder sb = new StringBuilder(somestring);
        sb[2] = '*';
        sb[3] = '*';
        sb[4] = '*';
        sb[5] = '*';
        somestring = sb.ToString();

        lblMSGOTP.Visible = true;
        //lblMSG.Visible = true;
        lblMSGOTP.Text = "OTP sent successfully for your registered mobile number " + somestring + " .OTP Valid for 15 minutes.";

        Session["OTP"] = sRandomOTP;
    }
    protected void btnverify_Click(object sender, EventArgs e)
    {
        if (txtmobile.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Mobile OTP')", true);
            return;
        }
        if (txtverifyMobileNO.Text == Session["OTP"].ToString())
        {

            pnl2.Visible = false;
            ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "confirm('Your Mobile Number has been verified sccessfully.');", true);
            pnl1.Visible = true;
            txtmobile.Enabled = false;
            btnsendOtp.Visible = false;
            //btn_Save.Visible = true;
            Button2.Visible = true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "confirm('Your OTP is not correct please enter correct OTP');", true);
            pnl2.Visible = true;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (txtemailid.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Email Id.')", true);
            return;
        }
        string userEmail = txtemailid.Text;

        if (!string.IsNullOrEmpty(userEmail))
        {
            //string otp = GenerateOTP(6);
            //bool emailSent = SendEmail(userEmail, "Your OTP Code", $@"Your OTP code is {otp}");
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            string otp = GenerateRandomOTP(6, saAllowedCharacters);
            bool emailSent = SendEmail(userEmail, "Your OTP Code", "Dear Student, Your No dues confirmation OTP is : '" + otp + "'. Thank you. TEERTHANKER MAHAVEER UNIVERSITY");
            if (emailSent)
            {
                lblMSGOTPemail.Text = "OTP has been sent to your Email.";
                Session["OTP"] = otp;
                Panel1.Visible = false;
                Panel2.Visible = true;
            }
            else
            {
                lblMSGOTPemail.Text = "Error sending OTP. Please try again.";
            }
        }
        else
        {
            lblMSGOTPemail.Text = "Please enter a valid Email address.";
        }
    }


    private bool SendEmail(string toEmail, string subject, string body)
    {
        try
        {
            var fromEmail = "naverp@tmu.ac.in";
            var fromPassword = "nwar yzam bcez rqop";

            // Initialize the SMTP client with the server details
            var smtpClient = new SmtpClient("smtp.gmail.com") // Use your SMTP server here
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, fromPassword),
                EnableSsl = true,
            };

            // Create the MailMessage object
            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = "No Dues Form:Enrollment No:" + txtenrollmentno.Text + ",Student Name: " + txtstudentName.Text + "",
                Body = body,

                IsBodyHtml = true, // Set to true if the body contains HTML
            };
            mailMessage.To.Add(txtemailid.Text); // Add recipient

            // Send the email
            smtpClient.Send(mailMessage);
            return true; // Email sent successfully
        }
        catch (Exception ex)
        {
            // Log the exception (ex) if necessary
            return false; // Email sending failed
        }
    }
    private string GenerateOTP(int length)
    {
        var random = new Random();
        string characters = "0123456789";
        string otp = new string(Enumerable.Repeat(characters, length).Select(s => s[random.Next(s.Length)]).ToArray());
        return otp;
    }

    protected void Btn_verifyEmail_Click(object sender, EventArgs e)
    {

        if (txtverifyEmail.Text == Session["OTP"].ToString())
        {
            Panel2.Visible = false;
            ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "confirm('Your Emailid  has been verified sccessfully.');", true);
            Panel1.Visible = true;
            txtemailid.Enabled = false;
            Button2.Visible = false;
            btnsendOtp.Visible = false;
            btnsendOtp.Visible = false;
            btn_Save.Visible = true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "confirm('Your OTP is not correct please enter correct OTP');", true);
            Panel2.Visible = true;
        }
    }
    protected void lnkbutton_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("Pro_ShowStudentFeeLedger", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@customerNo", txtstno.Text);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        GriedviewStudentfeeLedger.DataSource = dtCL;
        GriedviewStudentfeeLedger.DataBind();
        this.BindGrid();
        GridViewdata.Show();
    }
    public void Feestudent()
    {
        SqlCommand cmd = new SqlCommand("Pro_ShowStudentFeeLedger", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@customerNo", txtstno.Text);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        GriedviewStudentfeeLedger.DataSource = dtCL;
        GriedviewStudentfeeLedger.DataBind();
    }
    private void BindGrid()
    {
        String query = "(select * from (Select (Select [Student Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Student - COLLEGE] where No_ = P.[Customer No_]) As StudentName, (Select [Enrollment No_] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Student - COLLEGE] where No_ = P.[Customer No_] ) As EnrollmentNo,(Select  CAST(ROUND(sum([Amount (LCY)]), 2) AS DECIMAL(10, 2))[Amount (LCY)] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Detailed Cust_ Ledg_ Entry] where[Cust_ Ledger Entry No_] = P.[Entry No_]) as PendingAmount, [Customer No_] as customerNo, CONVERT(VARCHAR, [Posting Date], 103) AS PostingDate, * from [EDUCOLLEGELIVE-R2].dbo.[TMU$Cust_ Ledger Entry] As P where [Customer No_] ='" + txtstno.Text + "' and[Open]=1) Y where isnull(Y.PendingAmount,0)!=0)";
        string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        GriedviewStudentfeeLedger.DataSource = dt;
                        GriedviewStudentfeeLedger.DataBind();

                        //Calculate Sum and display in Footer Row
                        if (dt.Rows.Count > 0)
                        {
                            decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingAmount"));
                            GriedviewStudentfeeLedger.FooterRow.Cells[3].Text = "Total Pending Amount";
                            GriedviewStudentfeeLedger.FooterRow.Cells[3].ForeColor = System.Drawing.Color.Red;
                            GriedviewStudentfeeLedger.FooterRow.Cells[3].Font.Bold = true;
                            GriedviewStudentfeeLedger.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                            GriedviewStudentfeeLedger.FooterRow.Cells[4].Text = total.ToString("N4");
                        }
                    }
                }
            }
        }
    }
    protected void GriedviewStudentfeeLedger_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        GriedviewStudentfeeLedger.PageIndex = e.NewPageIndex;
        Feestudent();
        this.BindGrid();
        GridViewdata.Show();
    }
    protected void lnkPay_Click(object sender, EventArgs e)
    {
        int temp = 0;
        string orderid = "TMUNODUES" + DateTime.Now.Ticks.ToString();
        con1.Open();
        SqlCommand cmd = new SqlCommand("INSERT INTO [OnlinePaymentLog]([Payment Date],[Amount],[Status],[UserID],[GATEWAY],[GatewayStatus],[OrderID],[CLE Entry No],[Semester FEE],[Year FEE],[Temp 1],[Temp 2],[Temp 3],[Orgnized Date])VALUES(GETUTCDATE(),1 ,0 ,'" + Session["uid"].ToString() + "' ,'PAYTM'  ,0 ,'" + orderid + "','','','','',0,'', DATEADD(Minute,330,Getdate()))", con1);
        temp = cmd.ExecuteNonQuery();
        con1.Close();
        if (temp == 1)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //-------->Test
            String merchantKey = "7v_qN#jfvvCiLSOB";
            // Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("MID", "Teerth64420690832928");
            parameters.Add("CHANNEL_ID", "WEB");
            parameters.Add("INDUSTRY_TYPE_ID", "Education");
            parameters.Add("WEBSITE", "DEFAULT");
            parameters.Add("EMAIL", "");
            parameters.Add("MOBILE_NO", "");
            //parameters.Add("CUST_ID", Session["uid"].ToString().Replace("/", "").ToString());
            parameters.Add("CUST_ID", Session["uid"].ToString());
            parameters.Add("ORDER_ID", orderid);
            parameters.Add("TXN_AMOUNT", lblTotal.Text);
            //parameters.Add("EXTENDINFO", Session["uid"].ToString());
            // parameters.Add("mercUnqRef", Session["enroll"].ToString());

            // parameters.Add("CALLBACK_URL", "http://172.0.1.105:82/Student/PaYTMSport.aspx");//http://14.139.238.130:82/
            parameters.Add("CALLBACK_URL", "http://172.0.1.105:100/Student/NoDuesResponse.aspx");
            string checksum = CheckSum.generateCheckSum(merchantKey, parameters);
            string paytmURL = "https://securegw.paytm.in/order/process";
            //string paytmURL = "https://securegw-stage.paytm.in/theia/processTransaction?orderid=" + orderid;
            Session["uid"] = null;
            string outputHTML = "<html>";
            outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
            outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
            outputHTML += "<table border='1'>";
            outputHTML += "<tbody>";
            foreach (string key in parameters.Keys)
            {
                outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
            }
            outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            outputHTML += "<script type='text/javascript'>";
            outputHTML += "document.f1.submit();";
            outputHTML += "</script>";
            outputHTML += "</form>";
            outputHTML += "</script>";
            outputHTML += "</body>";
            outputHTML += "</html>";
            Response.Write(outputHTML);
        }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (txtmobile.Text == "")
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Mobile Number')", true);
            return;
        }
        if (txtemailid.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Email_Id')", true);
            return;
        }
        if (txtdatedirectorprincipaldate.Text == "")
        {
            string message1 = "Please Fill Date.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        //if(chkelegebleyesorno.Checked)
        //{
        //    string message1 = "Please Check Elegible.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    return;
        //}
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("Pro_InsertStudentNoduesTMMC", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@CreationTime", "");
        cmd.Parameters.AddWithValue("@EnrollementNo", txtenrollmentno.Text);
        cmd.Parameters.AddWithValue("@STNO_", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
        cmd.Parameters.AddWithValue("@FathersName", txtfathername.Text);
        cmd.Parameters.AddWithValue("@CollegeDept", txtcollegedept.Text);
        cmd.Parameters.AddWithValue("@Programme", txtPrograme.Text);
        cmd.Parameters.AddWithValue("@MobileNo_", txtmobile.Text);
        cmd.Parameters.AddWithValue("@EmailId", txtemailid.Text);
        cmd.Parameters.AddWithValue("@Date", txtdatedirectorprincipaldate.Text);
        cmd.Parameters.AddWithValue("@FormStatus", "Submit");
        cmd.Parameters.AddWithValue("@Gender", txtgender.Text);
        cmd.Parameters.AddWithValue("@Section", txtstno.Text);
        cmd.Parameters.AddWithValue("@HostelId", lblhostelcode.Text);
        cmd.Parameters.AddWithValue("@HostelDesignation", lblhosteldeg.Text);
        cmd.Parameters.AddWithValue("@HostelEmployee_Name", lblhostelsname.Text);
        cmd.Parameters.AddWithValue("@AccountDeptId", txtaccountemployeecode.Text);
        cmd.Parameters.AddWithValue("@AccountDeptDesignation", lblaccountdeg.Text);
        cmd.Parameters.AddWithValue("@AccountEmployeeName", lblaccountname.Text);
        cmd.Parameters.AddWithValue("@No_Dues_Id", txtnoduesid.Text);
        cmd.Parameters.AddWithValue("@PrincipalId","");
        cmd.Parameters.AddWithValue("@SportDeptId", lblsportnamecode.Text);
        cmd.Parameters.AddWithValue("@LibraryDeptId", lblcentrallibrarycode.Text);
        cmd.Parameters.AddWithValue("@ITDeptId", lblITnamecode.Text);
        cmd.Parameters.AddWithValue("@Elegible", "");
        cmd.Parameters.AddWithValue("@COEStatus", "Pending");
        cmd.Parameters.AddWithValue("@COMMUNITYMEDICINEDeptId", lblCommunityMedicinecode.Text);
        cmd.Parameters.AddWithValue("@GENERALMEDICINEDeptId", lblGeneralMedicicode.Text);
        cmd.Parameters.AddWithValue("@PSYCHIATRYDeptId", lblPsychiatrycode.Text);
        cmd.Parameters.AddWithValue("@GENERALSURGERYDeptId", lblGeneralSurgerycode.Text);
        cmd.Parameters.AddWithValue("@ANESTHISIADeptId", lblAnesthisiacode.Text);
        cmd.Parameters.AddWithValue("@OBSGYANEDeptId", lblObsGyanecode.Text);
        cmd.Parameters.AddWithValue("@PEDIATRICSDeptId", lblPediatricscode.Text);
        cmd.Parameters.AddWithValue("@ORTHOPEDICSDeptId", lblOrthopedicscode.Text);
        cmd.Parameters.AddWithValue("@ENTDeptId", lblEntcode.Text);
        cmd.Parameters.AddWithValue("@OPHTHALMOLOGYDeptId", lblOphthalmologycode.Text);
        cmd.Parameters.AddWithValue("@CASUALTYDeptId",  lblCasualtycode.Text);
        cmd.Parameters.AddWithValue("@DERMOTOLOGYDeptId", lblDermatologycode.Text);
        cmd.Parameters.AddWithValue("@TBANDCHESTDeptId", lblTbChestcode.Text);
        cmd.Parameters.AddWithValue("@RADIOLOGYDeptId", lblRadiologycode.Text);
        cmd.Parameters.AddWithValue("@PATHOLOGYDeptId", lblPathlogycode.Text);
        cmd.Parameters.AddWithValue("@FORENSICMEDICINEDeptId", lblForensicMedicinecode.Text);
        cmd.Parameters.AddWithValue("@HostelMessId", lblhostelcode.Text);
        

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');document.location.href='StudentNoDuesTMMC.aspx';", true);
        // Response.Redirect("StudentNoDues.aspx");
    }
    public void hidePaymentbutton()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        string strSQL = "select count(*) c from  HRMSPortal.dbo.Tbl_StudentOutstanigNoDues where Status='Submit' and  [Enrollment No_] ='" + txtenrollmentno.Text + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {

            if (Convert.ToInt32(dt.Rows[0]["c"]) >= 22 && lblTotal.Text != "0")
            {
                lnkPay.Visible = true;
            }
            else
            {
                lnkPay.Visible = false;
                PayStatus.Text = "No Dues";
                //lnkPay.Visible = false;
            }
        }
    }
    public void PaymentVerifyButton()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("Select * from  [EDUCOLLEGELIVE-R2].dbo.OnlinePaymentLog as P  where UserID='" + txtstno.Text + "' and OrderID like 'TMUNODUES%' and GatewayStatus=1", con))
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        string GatewayStatus = dr["GatewayStatus"].ToString();
                        con.Close();
                        if (GatewayStatus == "1")

                        {
                            lnkPay.Visible = false;

                        }
                        else
                        {
                            lnkPay.Visible = true;
                        }
                    }
                }
            }
        }
    }

    public void binddate()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "select *,  FORMAT(CAST([COE Approval Date] AS DATETIME), 'dd/MM/yyyy') AS COEApprovaldate from Tbl_StudentNodues where [Enrollement No]='" + Session["enroll"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            txtdatedirectorprincipaldate.Text = dt.Rows[0]["Date"].ToString();
            txtnoduesid.Text = dt.Rows[0]["No_Dues_Id"].ToString();
            lblrefno.Text = dt.Rows[0]["No_Dues_Id"].ToString();
            txtmobile.Text = dt.Rows[0]["Mobile No_"].ToString();
            txtemailid.Text = dt.Rows[0]["Email Id"].ToString();
            lbldate.Text = dt.Rows[0]["COEApprovaldate"].ToString();
            //if (dt.Rows[0]["Elegible"].ToString() == "Y")
            //{
            //    chkelegebleyesorno.Checked = true;
            //}
            //else
            //{
            //    chkelegebleyesorno.Checked = false;
            //}
        }
    }
    public void hidenodues()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("Select [COE Status] as COEStatus from Tbl_StudentNodues where [Enrollement No]='" + txtenrollmentno.Text + "'", con))
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        string COEStatus = dr["COEStatus"].ToString();
                        con.Close();
                        if (COEStatus == "Submit")

                        {
                            divnodues.Visible = false;
                            divnodues1.Visible = true;
                            Button1.Visible = true;
                        }
                        else
                        {
                            divnodues.Visible = true;
                            divnodues1.Visible = false;
                        }
                    }
                }
            }
        }
    }
    public void hidesave()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("select *, [Form Status] as Status from Tbl_StudentNoDues where [Enrollement No]='" + Session["enroll"].ToString() + "'", con))
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        string Status = dr["Status"].ToString();
                        con.Close();
                        if (Status == "Submit")

                        {
                            btn_Save.Visible = false;
                            btnsendOtp.Visible = false;
                            Button2.Visible = false;
                            txtmobile.Enabled = false;
                            txtemailid.Enabled = false;
                        }
                        else
                        {
                            btn_Save.Visible = true;
                            btnsendOtp.Visible = true;
                            Button2.Visible = true;
                            txtmobile.Enabled = true;
                            txtemailid.Enabled = true;
                        }
                    }
                }
            }
        }
    }

}