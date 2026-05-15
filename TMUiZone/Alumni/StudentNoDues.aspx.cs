using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Web.ClientServices;
using System.Collections.Specialized;
using paytm;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Text;
using QRCoder;
using System.Net.Mail;
using System.Net.Configuration;

public partial class StudentNoDues : System.Web.UI.Page
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
                //getdeptlibrary();
                getdepartment();
                binddata();
                alldept();
                binddataHOD();
                hidesave();
                numberseries();
                binddate();
                lblprinteddateandtime.Text = String.Format("{0}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                txtdatedirectorprincipaldate.Text = String.Format("{0}", DateTime.Now.ToString("dd/MM/yyyy"));
                hidePaymentbutton();
                PaymentVerifyButton();
                divnodues.Visible = true;
                Button2.Visible = false;
                hidenodues();
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

    public void binddataHOD()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        string strSQL = "select Distinct Principal,(select [First Name] from TMU$Employee where No_=T.Principal) as P,(select [Job Title_Grade Desc] from TMU$Employee where No_=T.Principal) as Title from [TMU$User Role Matrix]  as T where [Global Dimenison 1 Code] ='" + txtcollegedept.Text + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        //lbldepartmenthnamecode.Text = dt.Rows[0]["Principal"].ToString();

        if (txtcollegedept.Text.ToString() == "TMDC")
        {
            lbldeptLaboratorycode.Text = "TMU00974";
            lbldeptLaboratory.Text = "DR ANKITA JAIN";
            lblcollegeworkname.Text = "DR ANKITA JAIN";
            lbldeptLaboratorydeg.Text = "VICE PRINCIPAL";
            lblcollegeworkdeg.Text = "VICE PRINCIPAL";
        }
        else
        {
            lbldeptLaboratorycode.Text = dt.Rows[0]["Principal"].ToString();
            lbldeptLaboratory.Text = dt.Rows[0]["P"].ToString();
            lblcollegeworkname.Text = dt.Rows[0]["P"].ToString();
            lbldeptLaboratorydeg.Text = dt.Rows[0]["Title"].ToString();
            lblcollegeworkdeg.Text = dt.Rows[0]["Title"].ToString();
        }

    }
    public void alldept()
    {
        Decimal Total = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "Select [Status of Dues],Remark,Particular,[Dept Employee Name],[Dept Employee Code],isnull([Pending Amount],0) 'Pending Amount',Designation,ID,isnull((select top 1 [Temp 2] from  [EDUCOLLEGELIVE-R2].dbo.OnlinePaymentLog where UserID='" + Session["uid"].ToString() + "' and OrderID like 'TMUNODUES%' and GatewayStatus=1),-1) as PayStatus,Status from Tbl_StudentOutstanigNoDues where [Enrollment No_] ='" + txtenrollmentno.Text + "'  ";
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
                txtstatuscentrallib.Text = dr1[0]["Status"].ToString();
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
            DataRow[] dr2 = dt.Select("Particular = 'Department Library'");
            if (dr2.Length > 0)
            {
                // DropDownList1.SelectedItem.Text = dr2[0]["Status of Dues"].ToString();
                txtremarkdeptlibrary.Text = dr2[0]["Remark"].ToString();
                txtstatusdeptlibrary.Text = dr2[0]["Status"].ToString();
                TextBox1.Text = dr2[0]["Pending Amount"].ToString();
                if (dr2[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr2[0]["Pending Amount"]);
                }
                //txtdepartmentlibID.Text = dr2[0]["ID"].ToString();
            }
            DataRow[] dr7 = dt.Select("Particular = 'Sports'");
            if (dr7.Length > 0)
            {
                //DrpSport.SelectedItem.Text = dr7[0]["Status of Dues"].ToString();
                txtremarksportic.Text = dr7[0]["Remark"].ToString();
                txtstatussportic.Text = dr7[0]["Status"].ToString();
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
            DataRow[] dr10 = dt.Select("Particular = 'IT Department'");
            if (dr10.Length > 0)
            {
                //DrpSport.SelectedItem.Text = dr7[0]["Status of Dues"].ToString();
                txtremarkit.Text = dr10[0]["Remark"].ToString();
                txtstatusit.Text = dr10[0]["Status"].ToString();
                //txtSportID.Text = dr7[0]["ID"].ToString();
                TextBox38.Text = dr10[0]["Pending Amount"].ToString();
                if (dr10[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr10[0]["Pending Amount"]);
                }
            }
            DataRow[] Security10 = dt.Select("Particular = 'Security Department'");
            if (Security10.Length > 0)
            {
                //DrpSport.SelectedItem.Text = dr7[0]["Status of Dues"].ToString();
                txtremarkSecurity.Text = Security10[0]["Remark"].ToString();
                txtstatusSecurity.Text = Security10[0]["Status"].ToString();
                //txtSportID.Text = dr7[0]["ID"].ToString();
                TextBoxSecurity38.Text = Security10[0]["Pending Amount"].ToString();
                if (Security10[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(Security10[0]["Pending Amount"]);
                }
            }

            DataRow[] Examination10 = dt.Select("Particular = 'Examination Department'");
            if (Examination10.Length > 0)
            {
                //DrpSport.SelectedItem.Text = dr7[0]["Status of Dues"].ToString();
                txtremarkExamination.Text = Examination10[0]["Remark"].ToString();
                txtstatusExamination.Text = Examination10[0]["Status"].ToString();
                //txtSportID.Text = dr7[0]["ID"].ToString();
                TextBoxExamination38.Text = Examination10[0]["Pending Amount"].ToString();
                if (Examination10[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(Examination10[0]["Pending Amount"]);
                }
            }
            DataRow[] dr9 = dt.Select("Particular = 'Electricty Department'");
            if (dr9.Length > 0)
            {
                //drpelectrictydepartment.SelectedItem.Text = dr9[0]["Status of Dues"].ToString();
                txtelectrictydeptremark.Text = dr9[0]["Remark"].ToString();
                txtelectrictydeptstatus.Text = dr9[0]["Status"].ToString();
                TextBox7.Text = dr9[0]["Pending Amount"].ToString();
                if (dr9[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr9[0]["Pending Amount"]);
                }
            }
            DataRow[] dr14 = dt.Select("Particular = 'College/Department Laboratory'");
            if (dr14.Length > 0)
            {
                // DropDownList5.SelectedItem.Text = dr14[0]["Status of Dues"].ToString();
                txtremarkdepartmentLaboratory.Text = dr14[0]["Remark"].ToString();
                txtstatusdepartmentLaboratory.Text = dr14[0]["Status"].ToString();
                TextBox3.Text = dr14[0]["Pending Amount"].ToString();
                if (dr14[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr14[0]["Pending Amount"]);
                }
                // txtidDeptLaboratory.Text = dr14[0]["ID"].ToString();
            }

            DataRow[] dr15 = dt.Select("Particular = 'College/Department Workshop'");
            if (dr15.Length > 0)
            {
                //DropDownList8.SelectedItem.Text = dr15[0]["Status of Dues"].ToString();
                txtremarkdepartmentwork.Text = dr15[0]["Remark"].ToString();
                txtstatusdepartmentwork.Text = dr15[0]["Status"].ToString();
                TextBox4.Text = dr15[0]["Pending Amount"].ToString();
                if (dr15[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr15[0]["Pending Amount"]);
                }
                //txtidcollegework.Text = dr15[0]["ID"].ToString();
            }
            DataRow[] dr16 = dt.Select("Particular = 'Hostel'");
            if (dr16.Length > 0)
            {
                //DropDownList9.SelectedItem.Text = dr16[0]["Status of Dues"].ToString();
                txtremarkhostel.Text = dr16[0]["Remark"].ToString();
                txtstatushostel.Text = dr16[0]["Status"].ToString();
                TextBox5.Text = dr16[0]["Pending Amount"].ToString();
                if (dr16[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr16[0]["Pending Amount"]);
                }

            }
            DataRow[] dr18 = dt.Select("Particular = 'Other Fee'");
            if (dr18.Length > 0)
            {
                //drpotherfeee.SelectedItem.Text = dr18[0]["Status of Dues"].ToString();
                //lblotherfinename.Text= dr18[0]["Dept Employee Name"].ToString();
                //lblotherfeedeg.Text= dr18[0]["Designation"].ToString();
                TextBox17.Text = dr18[0]["Remark"].ToString();
                txtStatusPendingFee.Text = dr18[0]["Status"].ToString();
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
    public void getdepartment()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("sp_getlibrarydeptstudentNoduesNew", con);
        cmd.Parameters.AddWithValue("@enroll", Session["enroll"].ToString());
        cmd.Parameters.AddWithValue("@dept", Session["College"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        lbldepartmentlib.Text = dtCL.Select("Particulars='Department Library'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lbldepartmentlibdeg.Text = dtCL.Select("Particulars='Department Library'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lbldepartmentlibcode.Text = dtCL.Select("Particulars='Department Library'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblcentlibname.Text = dtCL.Select("Particulars='Central Library'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblcentlibdeg.Text = dtCL.Select("Particulars='Central Library'").CopyToDataTable().Rows[0]["Designation"].ToString();
        // lblprincipalcodelab.Text = dtCL.Select("Particulars='Central Library'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lbldeptLaboratory.Text = dtCL.Select("Particulars='College/Department Laboratory'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lbldeptLaboratorydeg.Text = dtCL.Select("Particulars='College/Department Laboratory'").CopyToDataTable().Rows[0]["Designation"].ToString();


        lblcollegeworkname.Text = dtCL.Select("Particulars='College/Department Wprkshop'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblcollegeworkdeg.Text = dtCL.Select("Particulars='College/Department Wprkshop'").CopyToDataTable().Rows[0]["Designation"].ToString();


        //lblAlumnifeename.Text = dtCL.Select("Particulars='Alumni Fee'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        //lblAlumnifeedeg.Text = dtCL.Select("Particulars='Alumni Fee'").CopyToDataTable().Rows[0]["Designation"].ToString();
        ////lblhostelmessnamecode.Text = dtCL.Select("Particulars='Hostel Mess'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblaccountname.Text = dtCL.Select("Particulars='Account Deptt'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblaccountdeg.Text = dtCL.Select("Particulars='Account Deptt'").CopyToDataTable().Rows[0]["Designation"].ToString();
        txtaccountemployeecode.Text = dtCL.Select("Particulars='Account Deptt'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();


        lblhostelsname.Text = dtCL.Select("Particulars='Hostel'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblhosteldeg.Text = dtCL.Select("Particulars='Hostel'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblhostelcode.Text = dtCL.Select("Particulars='Hostel'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();
        ////lbltransportofficenamecode.Text = dtCL.Select("Particulars='Transport Office'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        //  lblwashermanname.Text = dtCL.Select("Particulars='Hostel'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        //  lblwashermandeg.Text = dtCL.Select("Particulars='Hostel'").CopyToDataTable().Rows[0]["Designation"].ToString();
        ////lbltransportofficenamecode.Text = dtCL.Select("Particulars='Transport Office'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblsportname.Text = dtCL.Select("Particulars='Sport I/c'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblsportdeg.Text = dtCL.Select("Particulars='Sport I/c'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblsportnamecode.Text = dtCL.Select("Particulars='Sport I/c'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblitname.Text = dtCL.Select("Particulars='IT Department'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblitdeg.Text = dtCL.Select("Particulars='IT Department'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblitnamecode.Text = dtCL.Select("Particulars='IT Department'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblSecurityname.Text = dtCL.Select("Particulars='Security Department'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblSecuritydeg.Text = dtCL.Select("Particulars='Security Department'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblSecuritynamecode.Text = dtCL.Select("Particulars='Security Department'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblExaminationname.Text = dtCL.Select("Particulars='Examination Department'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblExaminationdeg.Text = dtCL.Select("Particulars='Examination Department'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblExaminationnamecode.Text = dtCL.Select("Particulars='Examination Department'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblelectrictydeptname.Text = dtCL.Select("Particulars='Electricty Department'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblelectrictydeptdeg.Text = dtCL.Select("Particulars='Electricty Department'").CopyToDataTable().Rows[0]["Designation"].ToString();
        //lblhostelcode.Text = dtCL.Select("Particulars='Electricty Department'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();
    }
    public void binddata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        string strSQL = "select Case when Gender=1 then 'Male' else 'Female' end as Gender,'20' +RIGHT([Academic Year], 2) AS AcademicYear1, (Select [Exam Course Name] from [TMU$Course - COLLEGE] where [Code]=P.[Course Code]) As CourseName, *  from [EDUCOLLEGELIVE-R2].dbo.[TMU$Student - COLLEGE] as P where [Enrollment No_]='" + Session["enroll"].ToString() + "'";
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
        // txtmobile.Text = dt.Rows[0]["Mobile Number"].ToString();
        // txtemailid.Text = dt.Rows[0]["E-Mail Address"].ToString();
        txtPrograme.Text = dt.Rows[0]["Course Name"].ToString();
        lblprogram.Text = dt.Rows[0]["CourseName"].ToString();
        txtcollegedept.Text = dt.Rows[0]["Global Dimension 1 Code"].ToString();
        //lblcertifiedname.Text = dt.Rows[0]["Student Name"].ToString();
        //lblYear.Text = dt.Rows[0]["Academic Year"].ToString();      
        txtgender.Text = dt.Rows[0]["Gender"].ToString();
        lblacedmicyear.Text = dt.Rows[0]["AcademicYear1"].ToString();
        //lblYear.Text = dt.Rows[0]["AcademicYear1"].ToString();
        txtstno.Text = dt.Rows[0]["No_"].ToString();
        // lblacedmicyear.Text= dt.Rows[0]["Academic Year"].ToString();
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
            txtstatusInternship.Text = dt.Rows[0]["Internship Status"].ToString();
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
        SqlCommand cmd = new SqlCommand("Pro_InsertStudentNodues07112024", con);
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
        cmd.Parameters.AddWithValue("@PrincipalId", lbldeptLaboratorycode.Text);
        cmd.Parameters.AddWithValue("@SportDeptId", lblsportnamecode.Text);
        cmd.Parameters.AddWithValue("@ITDeptId", lblitnamecode.Text);
        cmd.Parameters.AddWithValue("@SecurityId", lblSecuritynamecode.Text);
        cmd.Parameters.AddWithValue("@ExaminationId", lblExaminationnamecode.Text);
        cmd.Parameters.AddWithValue("@LibraryDeptId", lbldepartmentlibcode.Text);
        cmd.Parameters.AddWithValue("@Elegible", "");
        cmd.Parameters.AddWithValue("@COEStatus", "Pending");
        cmd.Parameters.AddWithValue("@DepartmentLibraryStatus", "Pending");
        cmd.Parameters.AddWithValue("@CentralLibraryStatus", "Pending");
        cmd.Parameters.AddWithValue("@CollegeDepartmentLaboratoryStatus", "Pending");
        cmd.Parameters.AddWithValue("@CollegeDepartmentWprkshopStatus", "Pending");
        cmd.Parameters.AddWithValue("@HostelStatus", "Pending");
        cmd.Parameters.AddWithValue("@ElectricityDepartmentStatus", "Pending");
        cmd.Parameters.AddWithValue("@SportStatus", "Pending");
        cmd.Parameters.AddWithValue("@WasherManStatus", "Pending");
        cmd.Parameters.AddWithValue("@OtherFeeStatus", "Pending");
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');document.location.href='StudentNoDues.aspx';", true);
        // Response.Redirect("StudentNoDues.aspx");
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
    public void hidePaymentbutton()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        string strSQL = "select count(*) c from  HRMSPortal.dbo.Tbl_StudentOutstanigNoDues where Status='Submit' and  [Enrollment No_] ='" + txtenrollmentno.Text + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {

            if (Convert.ToInt32(dt.Rows[0]["c"]) >= 9 && lblTotal.Text != "0")
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
    public void hidenodues()
    {
        string COEStatus = "";
        string InternshipStatus = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        {

            SqlCommand cmd1 = new SqlCommand("Select [COE Status] as COEStatus,[Internship Status] as InternshipStatus from Tbl_StudentNodues where [Enrollement No]='" + txtenrollmentno.Text + "'", con);
            con.Open();
            SqlDataReader dr = cmd1.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    COEStatus = dr["COEStatus"].ToString();
                    InternshipStatus = dr["InternshipStatus"].ToString();
                }
            }
            con.Close();
            SqlCommand cmd = new SqlCommand("SP_NoDuesStatus", con);
            con.Open();
            cmd.Parameters.AddWithValue("@enrNo", Session["enroll"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            object result = cmd.ExecuteScalar();
            int value = 0;
            if (result != null)
            {
                value = Convert.ToInt32(result.ToString());
            }
            con.Close();
            if (value == 11 && (InternshipStatus == "Internship Done"  || InternshipStatus == "Not Applicable"))
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
            parameters.Add("CALLBACK_URL", "http://172.0.1.105:82/Student/NoDuesResponse.aspx");
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
    protected void btnclose_Click(object sender, EventArgs e)
    {
        GridViewdata.Hide();
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
        String query = "(select * from (Select (Select [Student Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Student - COLLEGE] where No_ = P.[Customer No_]) As StudentName, (Select [Enrollment No_] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Student - COLLEGE] where No_ = P.[Customer No_] ) As EnrollmentNo,(Select  CAST(ROUND(sum([Amount (LCY)]), 2) AS DECIMAL(10, 2))[Amount (LCY)] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Detailed Cust_ Ledg_ Entry] where[Cust_ Ledger Entry No_] = P.[Entry No_]) as PendingAmount, [Customer No_] as customerNo, CONVERT(VARCHAR, [Posting Date], 103) AS PostingDate, * from [EDUCOLLEGELIVE-R2].dbo.[TMU$Cust_ Ledger Entry] As P where [Customer No_] ='" + txtstno.Text + "' and[Open]=1) Y where isnull(Y.PendingAmount,0)!=0 and Y.PendingAmount>0)";
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
                            GriedviewStudentfeeLedger.FooterRow.Cells[4].Text = total.ToString("N2");
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

}

