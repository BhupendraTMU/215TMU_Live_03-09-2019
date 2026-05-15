using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Faculty_NoDuesApprovalList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                getdeptlibrary();
                //getnodueslist();
                hideSubmitbutton();
                //binddata();

                if (Session["uid"].ToString() == "TMU001174")
                {
                    getnodueslistTMMC();
                    Button31.Visible = true;
                    Button32.Visible = true;
                }
                else
                {
                    getnodueslistTMMC();
                    Button31.Visible = false;
                    Button32.Visible = false;
                }

            }
            if (Session["uid"].ToString() == "TMU00140")
            {
                // VisibleFormCOE();

                //btn_Save.Visible = true;
                Button33.Visible = true;
            }
            else
            {
                //btn_Save.Visible = false;
                Button33.Visible = false;
            }

        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }
    }
    public void lnkbutton_Click1xx(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(searchvalue.Text) && searchvalue.Text != null)
        {
            GriedviewStudent.DataSource = null;
            GriedviewStudent.DataBind();
            SqlCommand cmd = new SqlCommand("pro_StudentnodueslistTMMCNewListBySTUID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@DeptName", txtcollegedept.Text);
            cmd.Parameters.AddWithValue("@EnrollementNo", searchvalue.Text);
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlDataAdapter daCL = new SqlDataAdapter(cmd);
            DataTable dtCL = new DataTable();
            daCL.Fill(dtCL);
            GriedviewStudent.DataSource = dtCL;
            GriedviewStudent.DataBind();
        }
        else
        {
            getnodueslistTMMC();
        }


    }

    public void alldept()
    {
        Decimal Total = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "Select [Status of Dues],Remark,Particular,[Creation Time],[Dept Employee Name],[Check NoDues Status],[Dept Employee Code],[Total Amount],[Pending Amount],Designation,ID,isnull((select top 1 [Temp 2] from  [EDUCOLLEGELIVE-R2].dbo.OnlinePaymentLog where UserID='" + hdfSTNO.Value + "' and OrderID like 'TMUNODUES%' and GatewayStatus=1),-1) as PayStatus from Tbl_StudentOutstanigNoDues where [Enrollment No_] ='" + UserId.Text + "'  ";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {

            if (dt.Rows[0]["PayStatus"].ToString() == "-1")
            {
                Button2.Visible = false;


            }
            else if (dt.Rows[0]["PayStatus"].ToString() == "0")
            {
                Button2.Visible = true;
            }
            else if (dt.Rows[0]["PayStatus"].ToString() == "1")
            {
                Button2.Enabled = false;
                Button2.ForeColor = System.Drawing.Color.White;
                Button2.BackColor = System.Drawing.Color.Green;

            }
            else
            {

            }
            DataRow[] dr1 = dt.Select("Particular = 'Central Library'");
            if (dr1.Length > 0)
            {
                TextBox8.Text = dr1[0]["Status of Dues"].ToString();
                txtremarkcentrallib.Text = dr1[0]["Remark"].ToString();
                // Sanjay Jain
                if (dr1[0]["Creation Time"] != null && dr1[0]["Creation Time"].ToString() != "")
                {

                    txtdateCentralLibrary.Text = DateTime.Parse(dr1[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    txtdateCentralLibrary.Text = "";
                }
                // Sanjay Jain
                txtpendingamountcentrallibrary.Text = dr1[0]["Pending Amount"].ToString();
                //TextBox12.Text = dr1[0]["Total Amount"].ToString();               
                txtCentrallibID.Text = dr1[0]["ID"].ToString();
                if (dr1[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox3.Checked = true;

                }
                else
                {
                    CheckBox3.Checked = false;

                }
                if (dr1[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr1[0]["Pending Amount"]);
                }
                if (TextBox8.Text == "NoDues")
                {
                    CheckBox3.Enabled = false;
                    txtremarkcentrallib.Enabled = false;
                    txtpendingamountcentrallibrary.Enabled = false;
                    btn_Centrallib.ForeColor = System.Drawing.Color.White;
                    btn_Centrallib.BackColor = System.Drawing.Color.Green;
                    btn_Centrallib.Enabled = false;
                }
                else
                {
                    btn_Centrallib.ForeColor = System.Drawing.Color.White;
                    btn_Centrallib.BackColor = System.Drawing.Color.Red;
                }
            }
            DataRow[] dr2 = dt.Select("Particular = 'Department Library'");
            if (dr2.Length > 0)
            {
                TextBox6.Text = dr2[0]["Status of Dues"].ToString();
                txtremarkdeptlibrary.Text = dr2[0]["Remark"].ToString();
                // Sanjay Jain
                if (dr2[0]["Creation Time"] != null && dr2[0]["Creation Time"].ToString() != "")
                {

                    txtdatedeptlibrary.Text = DateTime.Parse(dr2[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    txtdatedeptlibrary.Text = "";
                }
                // Sanjay Jain
                //TextBox12.Text = dt.Rows[0]["Total Amount"].ToString();
                txtpendingamountdeptlibrary.Text = dr2[0]["Pending Amount"].ToString();
                txtdepartmentlibID.Text = dr2[0]["ID"].ToString();
                if (dr2[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox1.Checked = true;
                }
                else
                {
                    CheckBox1.Checked = false;
                }
                if (dr2[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr2[0]["Pending Amount"]);
                }
                if (TextBox6.Text == "NoDues")
                {
                    CheckBox1.Enabled = false;
                    txtremarkdeptlibrary.Enabled = false;
                    txtpendingamountdeptlibrary.Enabled = false;
                    btn_DepartmentLib.ForeColor = System.Drawing.Color.White;
                    btn_DepartmentLib.BackColor = System.Drawing.Color.Green;
                    btn_DepartmentLib.Enabled = false;
                }
                else
                {
                    btn_DepartmentLib.ForeColor = System.Drawing.Color.White;
                    btn_DepartmentLib.BackColor = System.Drawing.Color.Red;
                }
            }
            DataRow[] dr7 = dt.Select("Particular = 'Sports'");
            if (dr7.Length > 0)
            {
                TextBox15.Text = dr7[0]["Status of Dues"].ToString();
                txtremarksportic.Text = dr7[0]["Remark"].ToString();
                // Sanjay Jain
                if (dr7[0]["Creation Time"] != null && dr7[0]["Creation Time"].ToString() != "")
                {

                    txtdateSports.Text = DateTime.Parse(dr7[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    txtdateSports.Text = "";
                }
                // Sanjay Jain
                TextBox5.Text = dr7[0]["Pending Amount"].ToString();
                txtSportID.Text = dr7[0]["ID"].ToString();
                if (dr7[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox8.Checked = true;
                }
                else
                {
                    CheckBox8.Checked = false;
                }
                if (dr7[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr7[0]["Pending Amount"]);
                }
                if (TextBox15.Text == "NoDues")
                {
                    txtremarksportic.Enabled = false;
                    TextBox5.Enabled = false;
                    Button14.ForeColor = System.Drawing.Color.White;
                    Button14.BackColor = System.Drawing.Color.Green;
                    Button14.Enabled = false;
                    CheckBox8.Enabled = false;
                }
                else
                {

                    Button14.ForeColor = System.Drawing.Color.White;
                    Button14.BackColor = System.Drawing.Color.Red;

                }
            }
            DataRow[] dr87 = dt.Select("Particular = 'IT Department'");
            if (dr87.Length > 0)
            {
                TextBox815.Text = dr87[0]["Status of Dues"].ToString();
                txtremarkIT.Text = dr87[0]["Remark"].ToString();
                // Sanjay Jain
                if (dr87[0]["Creation Time"] != null && dr87[0]["Creation Time"].ToString() != "")
                {

                    txtdateITs.Text = DateTime.Parse(dr87[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    txtdateITs.Text = "";
                }
                // Sanjay Jain
                TextBox858.Text = dr87[0]["Pending Amount"].ToString();
                txtITID.Text = dr87[0]["ID"].ToString();
                if (dr87[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox88.Checked = true;
                }
                else
                {
                    CheckBox88.Checked = false;
                }
                if (dr87[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr87[0]["Pending Amount"]);
                }
                if (TextBox815.Text == "NoDues")
                {
                    txtremarkIT.Enabled = false;
                    TextBox858.Enabled = false;
                    Button814.ForeColor = System.Drawing.Color.White;
                    Button814.BackColor = System.Drawing.Color.Green;
                    Button814.Enabled = false;
                    CheckBox88.Enabled = false;
                }
                else
                {

                    Button814.ForeColor = System.Drawing.Color.White;
                    Button814.BackColor = System.Drawing.Color.Red;

                }
            }

            //fgd
            DataRow[] drSecurity87 = dt.Select("Particular = 'Security Department'");
            if (drSecurity87.Length > 0)
            {
                TextBox815Security.Text = drSecurity87[0]["Status of Dues"].ToString();
                txtremarkSecurity.Text = drSecurity87[0]["Remark"].ToString();
                // Sanjay Jain
                if (drSecurity87[0]["Creation Time"] != null && drSecurity87[0]["Creation Time"].ToString() != "")
                {

                    txtdateSecurity.Text = DateTime.Parse(drSecurity87[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    txtdateSecurity.Text = "";
                }
                // Sanjay Jain
                TextBox858Security.Text = drSecurity87[0]["Pending Amount"].ToString();
                txtSecurityID.Text = drSecurity87[0]["ID"].ToString();
                if (drSecurity87[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox88Security.Checked = true;
                }
                else
                {
                    CheckBox88Security.Checked = false;
                }
                if (drSecurity87[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(drSecurity87[0]["Pending Amount"]);
                }
                if (TextBox815Security.Text == "NoDues")
                {
                    txtremarkSecurity.Enabled = false;
                    TextBox858Security.Enabled = false;
                    Button814Security.ForeColor = System.Drawing.Color.White;
                    Button814Security.BackColor = System.Drawing.Color.Green;
                    Button814Security.Enabled = false;
                    CheckBox88Security.Enabled = false;
                }
                else
                {

                    Button814Security.ForeColor = System.Drawing.Color.White;
                    Button814Security.BackColor = System.Drawing.Color.Red;

                }
            }
            //fdgdfg
            DataRow[] drExamination87 = dt.Select("Particular = 'Examination Department'");
            if (drExamination87.Length > 0)
            {
                TextBox815Examination.Text = drExamination87[0]["Status of Dues"].ToString();
                txtremarkExamination.Text = drExamination87[0]["Remark"].ToString();
                // Sanjay Jain
                if (drExamination87[0]["Creation Time"] != null && drExamination87[0]["Creation Time"].ToString() != "")
                {

                    txtdateExamination.Text = DateTime.Parse(drExamination87[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    txtdateExamination.Text = "";
                }
                // Sanjay Jain
                TextBox858Examination.Text = drExamination87[0]["Pending Amount"].ToString();
                txtExaminationID.Text = drExamination87[0]["ID"].ToString();
                if (drExamination87[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox88Examination.Checked = true;
                }
                else
                {
                    CheckBox88Examination.Checked = false;
                }
                if (drExamination87[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(drExamination87[0]["Pending Amount"]);
                }
                if (TextBox815Examination.Text == "NoDues")
                {
                    txtremarkExamination.Enabled = false;
                    TextBox858Examination.Enabled = false;
                    Button814Examination.ForeColor = System.Drawing.Color.White;
                    Button814Examination.BackColor = System.Drawing.Color.Green;
                    Button814Examination.Enabled = false;
                    CheckBox88Examination.Enabled = false;
                }
                else
                {

                    Button814Examination.ForeColor = System.Drawing.Color.White;
                    Button814Examination.BackColor = System.Drawing.Color.Red;

                }
            }
            //test
            DataRow[] dr19 = dt.Select("Particular = 'Other Fee'");
            if (dr19.Length > 0)
            {
                TextBox16.Text = dr19[0]["Status of Dues"].ToString();
                txtotherfee.Text = dr19[0]["Remark"].ToString();
                // Sanjay Jain
                if (dr19[0]["Creation Time"] != null && dr19[0]["Creation Time"].ToString() != "")
                {

                    txtdateOtherFee.Text = DateTime.Parse(dr19[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    txtdateOtherFee.Text = "";
                }
                // Sanjay Jain
                txtifotherfeeid.Text = dr19[0]["ID"].ToString();
                //lblotherfinename.Text = dr19[0]["Dept Employee Name"].ToString();
                lblaccountdeg.Text = dr19[0]["Designation"].ToString();
                TextBox7.Text = dr19[0]["Pending Amount"].ToString();
                if (dr19[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox9.Checked = true;
                }
                else
                {
                    CheckBox9.Checked = false;
                }
                if (dr19[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr19[0]["Pending Amount"]);
                }
                if (TextBox16.Text == "NoDues")
                {
                    CheckBox9.Enabled = false;
                    txtotherfee.Enabled = false;
                    TextBox7.Enabled = false;
                    Button3.ForeColor = System.Drawing.Color.White;
                    Button3.BackColor = System.Drawing.Color.Green;
                    Button3.Enabled = false;
                }
                else
                {
                    Button3.ForeColor = System.Drawing.Color.White;
                    Button3.BackColor = System.Drawing.Color.Red;
                }
            }

            DataRow[] dr18 = dt.Select("Particular = 'Electricty Department'");
            if (dr18.Length > 0)
            {
                TextBox14.Text = dr18[0]["Status of Dues"].ToString();
                txtelectrictydeptremark.Text = dr18[0]["Remark"].ToString();
                txtelectrictid.Text = dr18[0]["ID"].ToString();
                // Sanjay Jain
                if (dr18[0]["Creation Time"] != null && dr18[0]["Creation Time"].ToString() != "")
                {

                    txtdateElectrictyDepartment.Text = DateTime.Parse(dr18[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    txtdateElectrictyDepartment.Text = "";
                }
                // Sanjay Jain
                lblelectrictydeptname.Text = dr18[0]["Dept Employee Name"].ToString();
                lblelectrictydeptdeg.Text = dr18[0]["Designation"].ToString();
                TextBox4.Text = dr18[0]["Pending Amount"].ToString();
                if (dr18[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox7.Checked = true;
                }
                else
                {
                    CheckBox7.Checked = false;
                }
                if (dr18[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr18[0]["Pending Amount"]);
                }
                if (TextBox14.Text == "NoDues")
                {
                    txtelectrictydeptremark.Enabled = false;
                    TextBox4.Enabled = false;
                    Button1.BackColor = System.Drawing.Color.Green;
                    Button1.ForeColor = System.Drawing.Color.White;
                    Button1.Enabled = false;
                    CheckBox7.Enabled = false;
                }
                else
                {

                    Button1.BackColor = System.Drawing.Color.Red;
                    Button1.ForeColor = System.Drawing.Color.White;

                }
            }

            DataRow[] dr14 = dt.Select("Particular = 'College/Department Laboratory'");
            if (dr14.Length > 0)
            {
                TextBox9.Text = dr14[0]["Status of Dues"].ToString();
                txtremarkdepartmentLaboratory.Text = dr14[0]["Remark"].ToString();
                // Sanjay Jain
                if (dr14[0]["Creation Time"] != null && dr14[0]["Creation Time"].ToString() != "")
                {

                    txtdateCollegeDepartmentLaboratory.Text = DateTime.Parse(dr14[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    txtdateCollegeDepartmentLaboratory.Text = "";
                }
                // Sanjay Jain
                txtidDeptLaboratory.Text = dr14[0]["ID"].ToString();
                txtpandingamountdeptlaboratory.Text = dr14[0]["Pending Amount"].ToString();
                if (dr14[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox2.Checked = true;
                }
                else
                {
                    CheckBox2.Checked = false;
                }
                if (dr14[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr14[0]["Pending Amount"]);
                }
                if (TextBox9.Text == "NoDues")
                {
                    txtremarkdepartmentLaboratory.Enabled = false;
                    txtpandingamountdeptlaboratory.Enabled = false;
                    btn_DeptLaboratory.BackColor = System.Drawing.Color.Green;
                    btn_DeptLaboratory.ForeColor = System.Drawing.Color.White;
                    btn_DeptLaboratory.Enabled = false;
                    CheckBox2.Enabled = false;
                }
                else
                {

                    btn_DeptLaboratory.BackColor = System.Drawing.Color.Red;
                    btn_DeptLaboratory.ForeColor = System.Drawing.Color.White;

                }
            }

            DataRow[] dr15 = dt.Select("Particular = 'College/Department Workshop'");
            if (dr15.Length > 0)
            {
                TextBox10.Text = dr15[0]["Status of Dues"].ToString();
                txtremarkdepartmentwork.Text = dr15[0]["Remark"].ToString();
                // Sanjay Jain
                if (dr15[0]["Creation Time"] != null && dr15[0]["Creation Time"].ToString() != "")
                {

                    txtdateCollegeDepartmentWorkshop.Text = DateTime.Parse(dr15[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    txtdateCollegeDepartmentWorkshop.Text = "";
                }
                // Sanjay Jain
                txtidcollegework.Text = dr15[0]["ID"].ToString();
                TextBox1.Text = dr15[0]["Pending Amount"].ToString();
                if (dr15[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox4.Checked = true;
                }
                else
                {
                    CheckBox4.Checked = false;
                }
                if (dr15[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr15[0]["Pending Amount"]);
                }
                if (TextBox10.Text == "NoDues")
                {
                    txtremarkdepartmentwork.Enabled = false;
                    TextBox1.Enabled = false;
                    Button8.ForeColor = System.Drawing.Color.White;
                    Button8.BackColor = System.Drawing.Color.Green;
                    Button8.Enabled = false;
                    CheckBox4.Enabled = false;
                }
                else
                {

                    Button8.ForeColor = System.Drawing.Color.White;
                    Button8.BackColor = System.Drawing.Color.Red;

                }
            }
            DataRow[] dr16 = dt.Select("Particular = 'Hostel'");
            if (dr16.Length > 0)
            {
                TextBox11.Text = dr16[0]["Status of Dues"].ToString();
                txtremarkhostel.Text = dr16[0]["Remark"].ToString();
                // Sanjay Jain
                if (dr16[0]["Creation Time"] != null && dr16[0]["Creation Time"].ToString() != "")
                {

                    txtdateHostel.Text = DateTime.Parse(dr16[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    txtdateHostel.Text = "";
                }
                // Sanjay Jain
                txtidhostel.Text = dr16[0]["ID"].ToString();
                TextBox2.Text = dr16[0]["Pending Amount"].ToString();
                if (dr16[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox5.Checked = true;
                }
                else
                {
                    CheckBox5.Checked = false;
                }
                if (dr16[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr16[0]["Pending Amount"]);
                }
                if (TextBox11.Text == "NoDues")
                {
                    txtremarkhostel.Enabled = false;
                    TextBox2.Enabled = false;
                    Btn_Hostel.ForeColor = System.Drawing.Color.White;
                    Btn_Hostel.BackColor = System.Drawing.Color.Green;
                    Btn_Hostel.Enabled = false;
                    CheckBox5.Enabled = false;
                }
                else
                {

                    Btn_Hostel.ForeColor = System.Drawing.Color.White;
                    Btn_Hostel.BackColor = System.Drawing.Color.Red;

                }
            }
            TextBox12.Text = Total.ToString();
            //(
            //    Convert.ToDecimal(dr19[0]["Pending Amount"]) +
            //    Convert.ToDecimal(dr17[0]["Pending Amount"]) +
            //    Convert.ToDecimal(dr16[0]["Pending Amount"]) +
            //    Convert.ToDecimal(dr1[0]["Pending Amount"]) +
            //    Convert.ToDecimal(dr2[0]["Pending Amount"]) +
            //    Convert.ToDecimal(dr7[0]["Pending Amount"]) +
            //    Convert.ToDecimal(dr18[0]["Pending Amount"]) +
            //    Convert.ToDecimal(dr9[0]["Pending Amount"]) +
            //    Convert.ToDecimal(dr10[0]["Pending Amount"]) +
            //    Convert.ToDecimal(dr11[0]["Pending Amount"]) +
            //    Convert.ToDecimal(dr12[0]["Pending Amount"]) +
            //    Convert.ToDecimal(dr13[0]["Pending Amount"]) +
            //    Convert.ToDecimal(dr14[0]["Pending Amount"]) +
            //    Convert.ToDecimal(dr15[0]["Pending Amount"])).ToString();
        }
    }


    public void alldept1()
    {
        Decimal Total = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "Select [Status of Dues],Remark,[Creation Time],Particular,[Dept Employee Name],[Check NoDues Status],[Dept Employee Code],[Total Amount],[Pending Amount],Designation,ID,isnull((select top 1 [Temp 2] from  [EDUCOLLEGELIVE-R2].dbo.OnlinePaymentLog where UserID='" + hdfSTNO.Value + "' and OrderID like 'TMUNODUES%' and GatewayStatus=1),-1) as PayStatus from Tbl_StudentOutstanigNoDues where [Enrollment No_] ='" + UserId.Text + "'  ";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["PayStatus"].ToString() == "-1")
            {
                //Button2.Visible = false;
                Button30.Visible = false;

            }
            else if (dt.Rows[0]["PayStatus"].ToString() == "0")
            {
                //Button2.Visible = true;
                Button30.Visible = true;
            }
            else if (dt.Rows[0]["PayStatus"].ToString() == "1")
            {
                //Button2.Enabled = false;
                //Button2.ForeColor = System.Drawing.Color.White;
                //Button2.BackColor = System.Drawing.Color.Green;

                Button30.Enabled = false;
                Button30.ForeColor = System.Drawing.Color.White;
                Button30.BackColor = System.Drawing.Color.Green;
            }
            else
            {

            }
            DataRow[] dr1 = dt.Select("Particular = 'Central Library'");
            if (dr1.Length > 0)
            {
                String value = dr1[0]["Creation Time"].ToString();
                TextBox27.Text = dr1[0]["Status of Dues"].ToString();
                TextBox29.Text = dr1[0]["Remark"].ToString();
                // Sanjay Jain
                if (dr1[0]["Creation Time"] != null && dr1[0]["Creation Time"].ToString() != "")
                {

                    Label67.Text = DateTime.Parse(dr1[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    Label67.Text = "";
                }
                // Sanjay Jain
                TextBox30.Text = dr1[0]["Pending Amount"].ToString();
                //TextBox12.Text = dr1[0]["Total Amount"].ToString();               
                TextBox31.Text = dr1[0]["ID"].ToString();
                if (dr1[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox10.Checked = true;

                }
                else
                {
                    CheckBox10.Checked = false;

                }
                if (dr1[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr1[0]["Pending Amount"]);
                }
                if (TextBox27.Text == "NoDues")
                {
                    CheckBox10.Enabled = false;
                    TextBox29.Enabled = false;
                    TextBox30.Enabled = false;
                    Button6.ForeColor = System.Drawing.Color.White;
                    Button6.BackColor = System.Drawing.Color.Green;
                    Button6.Enabled = false;
                }
                else
                {

                    Button6.ForeColor = System.Drawing.Color.White;
                    Button6.BackColor = System.Drawing.Color.Red;

                }

            }

            DataRow[] dr2 = dt.Select("Particular = 'Hostel'");
            if (dr2.Length > 0)
            {
                TextBox32.Text = dr2[0]["Status of Dues"].ToString();
                TextBox37.Text = dr2[0]["Remark"].ToString();
                TextBox38.Text = dr2[0]["Pending Amount"].ToString();

                // Sanjay Jain
                if (dr2[0]["Creation Time"] != null && dr2[0]["Creation Time"].ToString() != "")
                {

                    Label68.Text = DateTime.Parse(dr2[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    Label68.Text = "";
                }
                // Sanjay Jain
                //TextBox12.Text = dr1[0]["Total Amount"].ToString();               
                TextBox39.Text = dr2[0]["ID"].ToString();
                if (dr2[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox12.Checked = true;

                }
                else
                {
                    CheckBox12.Checked = false;

                }
                if (dr2[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr2[0]["Pending Amount"]);
                }
                if (TextBox32.Text == "NoDues")
                {
                    CheckBox12.Enabled = false;
                    TextBox37.Enabled = false;
                    TextBox38.Enabled = false;
                    Button9.ForeColor = System.Drawing.Color.White;
                    Button9.BackColor = System.Drawing.Color.Green;
                    Button9.Enabled = false;
                }
                else
                {

                    Button9.ForeColor = System.Drawing.Color.White;
                    Button9.BackColor = System.Drawing.Color.Red;

                }

            }

            DataRow[] dr3 = dt.Select("Particular = 'Hostel Mess'");
            if (dr3.Length > 0)
            {
                TextBox40.Text = dr3[0]["Status of Dues"].ToString();
                TextBox41.Text = dr3[0]["Remark"].ToString();
                // Sanjay Jain
                if (dr3[0]["Creation Time"] != null && dr3[0]["Creation Time"].ToString() != "")
                {

                    Label69.Text = DateTime.Parse(dr3[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    Label69.Text = "";
                }
                // Sanjay Jain
                TextBox43.Text = dr3[0]["ID"].ToString();
                TextBox42.Text = dr3[0]["Pending Amount"].ToString();
                if (dr3[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox13.Checked = true;
                }
                else
                {
                    CheckBox13.Checked = false;
                }
                if (dr3[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr3[0]["Pending Amount"]);
                }
                if (TextBox40.Text == "NoDues")
                {
                    TextBox41.Enabled = false;
                    TextBox42.Enabled = false;
                    Button10.ForeColor = System.Drawing.Color.White;
                    Button10.BackColor = System.Drawing.Color.Green;
                    Button10.Enabled = false;
                    CheckBox13.Enabled = false;
                }
                else
                {

                    Button10.ForeColor = System.Drawing.Color.White;
                    Button10.BackColor = System.Drawing.Color.Red;

                }
            }

            DataRow[] dr4 = dt.Select("Particular = 'Other Fee'");
            if (dr4.Length > 0)
            {
                TextBox33.Text = dr4[0]["Status of Dues"].ToString();
                TextBox34.Text = dr4[0]["Remark"].ToString();
                // Sanjay Jain
                if (dr4[0]["Creation Time"] != null && dr4[0]["Creation Time"].ToString() != "")
                {

                    Label66.Text = DateTime.Parse(dr4[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    Label66.Text = "";
                }
                // Sanjay Jain
                TextBox36.Text = dr4[0]["ID"].ToString();
                TextBox35.Text = dr4[0]["Pending Amount"].ToString();
                if (dr4[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox11.Checked = true;
                }
                else
                {
                    CheckBox11.Checked = false;
                }
                if (dr4[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr4[0]["Pending Amount"]);
                }
                if (TextBox33.Text == "NoDues")
                {
                    TextBox34.Enabled = false;
                    TextBox35.Enabled = false;
                    Button7.ForeColor = System.Drawing.Color.White;
                    Button7.BackColor = System.Drawing.Color.Green;
                    Button7.Enabled = false;
                    CheckBox11.Enabled = false;
                }
                else
                {

                    Button7.ForeColor = System.Drawing.Color.White;
                    Button7.BackColor = System.Drawing.Color.Red;

                }
            }

            DataRow[] dr6 = dt.Select("Particular = 'Sports'");
            if (dr6.Length > 0)
            {
                TextBox48.Text = dr6[0]["Status of Dues"].ToString();
                TextBox49.Text = dr6[0]["Remark"].ToString();
                // Sanjay Jain
                if (dr6[0]["Creation Time"] != null && dr6[0]["Creation Time"].ToString() != "")
                {

                    Label71.Text = DateTime.Parse(dr6[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    Label71.Text = "";
                }
                // Sanjay Jain
                TextBox51.Text = dr6[0]["ID"].ToString();
                TextBox50.Text = dr6[0]["Pending Amount"].ToString();
                if (dr6[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox15.Checked = true;
                }
                else
                {
                    CheckBox15.Checked = false;
                }
                if (dr6[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr6[0]["Pending Amount"]);
                }
                if (TextBox48.Text == "NoDues")
                {
                    TextBox49.Enabled = false;
                    TextBox50.Enabled = false;
                    Button12.ForeColor = System.Drawing.Color.White;
                    Button12.BackColor = System.Drawing.Color.Green;
                    Button12.Enabled = false;
                    CheckBox15.Enabled = false;
                }
                else
                {

                    Button12.ForeColor = System.Drawing.Color.White;
                    Button12.BackColor = System.Drawing.Color.Red;

                }
            }
            DataRow[] dr87 = dt.Select("Particular = 'IT Department'");
            if (dr87.Length > 0)
            {
                TextBox948.Text = dr87[0]["Status of Dues"].ToString();
                TextBox949.Text = dr87[0]["Remark"].ToString();
                // Sanjay Jain
                if (dr87[0]["Creation Time"] != null && dr87[0]["Creation Time"].ToString() != "")
                {

                    Label971.Text = DateTime.Parse(dr87[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    Label971.Text = "";
                }
                // Sanjay Jain
                TextBox951.Text = dr87[0]["ID"].ToString();
                TextBox950.Text = dr87[0]["Pending Amount"].ToString();
                if (dr87[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox915.Checked = true;
                }
                else
                {
                    CheckBox915.Checked = false;
                }
                if (dr87[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr87[0]["Pending Amount"]);
                }
                if (TextBox948.Text == "NoDues")
                {
                    TextBox949.Enabled = false;
                    TextBox950.Enabled = false;
                    Button912.ForeColor = System.Drawing.Color.White;
                    Button912.BackColor = System.Drawing.Color.Green;
                    Button912.Enabled = false;
                    CheckBox915.Enabled = false;
                }
                else
                {

                    Button912.ForeColor = System.Drawing.Color.White;
                    Button912.BackColor = System.Drawing.Color.Red;

                }
            }
            DataRow[] dr7 = dt.Select("Particular = 'Community Medicine'");
            if (dr7.Length > 0)
            {
                TextBox52.Text = dr7[0]["Status of Dues"].ToString();
                TextBox53.Text = dr7[0]["Remark"].ToString();
                // Sanjay Jain
                if (dr7[0]["Creation Time"] != null && dr7[0]["Creation Time"].ToString() != "")
                {

                    Label72.Text = DateTime.Parse(dr7[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    Label72.Text = "";
                }
                // Sanjay Jain
                TextBox55.Text = dr7[0]["ID"].ToString();
                TextBox54.Text = dr7[0]["Pending Amount"].ToString();
                if (dr7[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox16.Checked = true;
                }
                else
                {
                    CheckBox16.Checked = false;
                }
                if (dr7[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr7[0]["Pending Amount"]);
                }
                if (TextBox52.Text == "NoDues")
                {
                    TextBox53.Enabled = false;
                    TextBox54.Enabled = false;
                    Button13.ForeColor = System.Drawing.Color.White;
                    Button13.BackColor = System.Drawing.Color.Green;
                    Button13.Enabled = false;
                    CheckBox16.Enabled = false;
                }
                else
                {

                    Button13.ForeColor = System.Drawing.Color.White;
                    Button13.BackColor = System.Drawing.Color.Red;

                }
            }

            DataRow[] dr8 = dt.Select("Particular = 'General Medicine'");
            if (dr8.Length > 0)
            {
                TextBox56.Text = dr8[0]["Status of Dues"].ToString();
                TextBox57.Text = dr8[0]["Remark"].ToString();
                // Sanjay Jain
                if (dr8[0]["Creation Time"] != null && dr8[0]["Creation Time"].ToString() != "")
                {

                    Label73.Text = DateTime.Parse(dr8[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    Label73.Text = "";
                }
                // Sanjay Jain
                TextBox59.Text = dr8[0]["ID"].ToString();
                TextBox58.Text = dr8[0]["Pending Amount"].ToString();
                if (dr8[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox17.Checked = true;
                }
                else
                {
                    CheckBox17.Checked = false;
                }
                if (dr8[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr8[0]["Pending Amount"]);
                }
                if (TextBox56.Text == "NoDues")
                {
                    TextBox57.Enabled = false;
                    TextBox58.Enabled = false;
                    Button15.ForeColor = System.Drawing.Color.White;
                    Button15.BackColor = System.Drawing.Color.Green;
                    Button15.Enabled = false;
                    CheckBox17.Enabled = false;
                }
                else
                {

                    Button15.ForeColor = System.Drawing.Color.White;
                    Button15.BackColor = System.Drawing.Color.Red;

                }

            }
            DataRow[] dr9 = dt.Select("Particular = 'Psychiatry'");
            if (dr9.Length > 0)
            {
                TextBox60.Text = dr9[0]["Status of Dues"].ToString();
                TextBox61.Text = dr9[0]["Remark"].ToString();
                // Sanjay Jain
                if (dr9[0]["Creation Time"] != null && dr9[0]["Creation Time"].ToString() != "")
                {

                    Label74.Text = DateTime.Parse(dr9[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    Label74.Text = "";
                }
                // Sanjay Jain
                TextBox63.Text = dr9[0]["ID"].ToString();
                TextBox62.Text = dr9[0]["Pending Amount"].ToString();
                if (dr9[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox18.Checked = true;
                }
                else
                {
                    CheckBox18.Checked = false;
                }
                if (dr9[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr9[0]["Pending Amount"]);
                }
                if (TextBox60.Text == "NoDues")
                {
                    TextBox61.Enabled = false;
                    TextBox62.Enabled = false;
                    Button16.ForeColor = System.Drawing.Color.White;
                    Button16.BackColor = System.Drawing.Color.Green;
                    Button16.Enabled = false;
                    CheckBox18.Enabled = false;
                }
                else
                {
                    Button16.ForeColor = System.Drawing.Color.White;
                    Button16.BackColor = System.Drawing.Color.Red;
                }
            }
            DataRow[] dr10 = dt.Select("Particular = 'General Surgery'");
            if (dr10.Length > 0)
            {
                TextBox64.Text = dr10[0]["Status of Dues"].ToString();
                TextBox65.Text = dr10[0]["Remark"].ToString();
                // Sanjay Jain
                if (dr10[0]["Creation Time"] != null && dr10[0]["Creation Time"].ToString() != "")
                {

                    Label75.Text = DateTime.Parse(dr10[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    Label75.Text = "";
                }
                // Sanjay Jain
                TextBox67.Text = dr10[0]["ID"].ToString();
                TextBox66.Text = dr10[0]["Pending Amount"].ToString();
                if (dr10[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox19.Checked = true;
                }
                else
                {
                    CheckBox19.Checked = false;
                }
                if (dr10[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr10[0]["Pending Amount"]);
                }
                if (TextBox64.Text == "NoDues")
                {
                    TextBox65.Enabled = false;
                    TextBox66.Enabled = false;
                    Button17.ForeColor = System.Drawing.Color.White;
                    Button17.BackColor = System.Drawing.Color.Green;
                    Button17.Enabled = false;
                    CheckBox19.Enabled = false;
                }
                else
                {
                    Button17.ForeColor = System.Drawing.Color.White;
                    Button17.BackColor = System.Drawing.Color.Red;
                }
            }
            DataRow[] dr11 = dt.Select("Particular = 'Anesthisia'");
            if (dr11.Length > 0)
            {
                TextBox68.Text = dr11[0]["Status of Dues"].ToString();
                TextBox69.Text = dr11[0]["Remark"].ToString();
                // Sanjay Jain
                if (dr11[0]["Creation Time"] != null && dr11[0]["Creation Time"].ToString() != "")
                {

                    Label76.Text = DateTime.Parse(dr11[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    Label76.Text = "";
                }
                // Sanjay Jain
                TextBox71.Text = dr11[0]["ID"].ToString();
                TextBox70.Text = dr11[0]["Pending Amount"].ToString();
                if (dr11[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox20.Checked = true;
                }
                else
                {
                    CheckBox20.Checked = false;
                }
                if (dr11[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr11[0]["Pending Amount"]);
                }
                if (TextBox68.Text == "NoDues")
                {
                    TextBox69.Enabled = false;
                    TextBox70.Enabled = false;
                    Button18.ForeColor = System.Drawing.Color.White;
                    Button18.BackColor = System.Drawing.Color.Green;
                    Button18.Enabled = false;
                    CheckBox20.Enabled = false;
                }
                else
                {
                    Button18.ForeColor = System.Drawing.Color.White;
                    Button18.BackColor = System.Drawing.Color.Red;
                }
            }
            DataRow[] dr12 = dt.Select("Particular = 'Obs & Gyane'");
            if (dr12.Length > 0)
            {
                TextBox72.Text = dr12[0]["Status of Dues"].ToString();
                TextBox73.Text = dr12[0]["Remark"].ToString();
                // Sanjay Jain
                if (dr12[0]["Creation Time"] != null && dr12[0]["Creation Time"].ToString() != "")
                {

                    Label77.Text = DateTime.Parse(dr12[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    Label77.Text = "";
                }
                // Sanjay Jain
                TextBox75.Text = dr12[0]["ID"].ToString();
                TextBox74.Text = dr12[0]["Pending Amount"].ToString();
                if (dr12[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox21.Checked = true;
                }
                else
                {
                    CheckBox21.Checked = false;
                }
                if (dr12[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr12[0]["Pending Amount"]);
                }
                if (TextBox72.Text == "NoDues")
                {
                    TextBox73.Enabled = false;
                    TextBox74.Enabled = false;
                    Button19.ForeColor = System.Drawing.Color.White;
                    Button19.BackColor = System.Drawing.Color.Green;
                    Button19.Enabled = false;
                    CheckBox21.Enabled = false;
                }
                else
                {
                    Button19.ForeColor = System.Drawing.Color.White;
                    Button19.BackColor = System.Drawing.Color.Red;
                }
            }
            DataRow[] dr13 = dt.Select("Particular = 'Pediatrics'");
            if (dr13.Length > 0)
            {
                TextBox76.Text = dr13[0]["Status of Dues"].ToString();
                TextBox77.Text = dr13[0]["Remark"].ToString();
                // Sanjay Jain
                if (dr13[0]["Creation Time"] != null && dr13[0]["Creation Time"].ToString() != "")
                {

                    Label78.Text = DateTime.Parse(dr13[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    Label78.Text = "";
                }
                // Sanjay Jain
                TextBox79.Text = dr13[0]["ID"].ToString();
                TextBox78.Text = dr13[0]["Pending Amount"].ToString();
                if (dr13[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox22.Checked = true;
                }
                else
                {
                    CheckBox22.Checked = false;
                }
                if (dr13[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr13[0]["Pending Amount"]);
                }
                if (TextBox76.Text == "NoDues")
                {
                    TextBox77.Enabled = false;
                    TextBox78.Enabled = false;
                    Button20.ForeColor = System.Drawing.Color.White;
                    Button20.BackColor = System.Drawing.Color.Green;
                    Button20.Enabled = false;
                    CheckBox22.Enabled = false;
                }
                else
                {
                    Button20.ForeColor = System.Drawing.Color.White;
                    Button20.BackColor = System.Drawing.Color.Red;
                }
            }
            DataRow[] dr14 = dt.Select("Particular = 'Orthopedics'");
            if (dr14.Length > 0)
            {
                TextBox80.Text = dr14[0]["Status of Dues"].ToString();
                TextBox81.Text = dr14[0]["Remark"].ToString();
                // Sanjay Jain
                if (dr14[0]["Creation Time"] != null && dr14[0]["Creation Time"].ToString() != "")
                {

                    Label79.Text = DateTime.Parse(dr14[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    Label79.Text = "";
                }
                // Sanjay Jain
                TextBox83.Text = dr14[0]["ID"].ToString();
                TextBox82.Text = dr14[0]["Pending Amount"].ToString();
                if (dr14[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox23.Checked = true;
                }
                else
                {
                    CheckBox23.Checked = false;
                }
                if (dr14[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr14[0]["Pending Amount"]);
                }
                if (TextBox80.Text == "NoDues")
                {
                    TextBox81.Enabled = false;
                    TextBox82.Enabled = false;
                    Button21.ForeColor = System.Drawing.Color.White;
                    Button21.BackColor = System.Drawing.Color.Green;
                    Button21.Enabled = false;
                    CheckBox23.Enabled = false;
                }
                else
                {
                    Button21.ForeColor = System.Drawing.Color.White;
                    Button21.BackColor = System.Drawing.Color.Red;
                }
            }
            DataRow[] dr15 = dt.Select("Particular = 'Ent'");
            if (dr15.Length > 0)
            {
                TextBox84.Text = dr15[0]["Status of Dues"].ToString();
                TextBox85.Text = dr15[0]["Remark"].ToString();
                // Sanjay Jain
                if (dr15[0]["Creation Time"] != null && dr15[0]["Creation Time"].ToString() != "")
                {

                    Label80.Text = DateTime.Parse(dr15[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    Label80.Text = "";
                }
                // Sanjay Jain
                TextBox87.Text = dr15[0]["ID"].ToString();
                TextBox86.Text = dr15[0]["Pending Amount"].ToString();
                if (dr15[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox24.Checked = true;
                }
                else
                {
                    CheckBox24.Checked = false;
                }
                if (dr15[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr15[0]["Pending Amount"]);
                }
                if (TextBox84.Text == "NoDues")
                {
                    TextBox85.Enabled = false;
                    TextBox86.Enabled = false;
                    Button22.ForeColor = System.Drawing.Color.White;
                    Button22.BackColor = System.Drawing.Color.Green;
                    Button22.Enabled = false;
                    CheckBox24.Enabled = false;
                }
                else
                {
                    Button22.ForeColor = System.Drawing.Color.White;
                    Button22.BackColor = System.Drawing.Color.Red;
                }
            }
            DataRow[] dr16 = dt.Select("Particular = 'Ophthalmology'");
            if (dr16.Length > 0)
            {
                TextBox88.Text = dr16[0]["Status of Dues"].ToString();
                TextBox89.Text = dr16[0]["Remark"].ToString();
                // Sanjay Jain
                if (dr16[0]["Creation Time"] != null && dr16[0]["Creation Time"].ToString() != "")
                {

                    Label81.Text = DateTime.Parse(dr16[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    Label81.Text = "";
                }
                // Sanjay Jain
                TextBox91.Text = dr16[0]["ID"].ToString();
                TextBox90.Text = dr16[0]["Pending Amount"].ToString();
                if (dr16[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox25.Checked = true;
                }
                else
                {
                    CheckBox25.Checked = false;
                }
                if (dr16[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr16[0]["Pending Amount"]);
                }
                if (TextBox88.Text == "NoDues")
                {
                    TextBox89.Enabled = false;
                    TextBox90.Enabled = false;
                    Button23.ForeColor = System.Drawing.Color.White;
                    Button23.BackColor = System.Drawing.Color.Green;
                    Button23.Enabled = false;
                    CheckBox25.Enabled = false;
                }
                else
                {
                    Button23.ForeColor = System.Drawing.Color.White;
                    Button23.BackColor = System.Drawing.Color.Red;
                }
            }
            DataRow[] dr17 = dt.Select("Particular = 'Casualty'");
            if (dr17.Length > 0)
            {
                TextBox92.Text = dr17[0]["Status of Dues"].ToString();
                TextBox93.Text = dr17[0]["Remark"].ToString();
                TextBox95.Text = dr17[0]["ID"].ToString();
                // Sanjay Jain
                if (dr17[0]["Creation Time"] != null && dr17[0]["Creation Time"].ToString() != "")
                {

                    Label82.Text = DateTime.Parse(dr17[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    Label82.Text = "";
                }
                // Sanjay Jain
                TextBox94.Text = dr17[0]["Pending Amount"].ToString();
                if (dr17[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox26.Checked = true;
                }
                else
                {
                    CheckBox26.Checked = false;
                }
                if (dr17[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr17[0]["Pending Amount"]);
                }
                if (TextBox92.Text == "NoDues")
                {
                    TextBox93.Enabled = false;
                    TextBox94.Enabled = false;
                    Button24.ForeColor = System.Drawing.Color.White;
                    Button24.BackColor = System.Drawing.Color.Green;
                    Button24.Enabled = false;
                    CheckBox26.Enabled = false;
                }
                else
                {
                    Button24.ForeColor = System.Drawing.Color.White;
                    Button24.BackColor = System.Drawing.Color.Red;
                }
            }
            DataRow[] dr18 = dt.Select("Particular = 'Dermatology'");
            if (dr18.Length > 0)
            {
                TextBox96.Text = dr18[0]["Status of Dues"].ToString();
                TextBox97.Text = dr18[0]["Remark"].ToString();
                TextBox99.Text = dr18[0]["ID"].ToString();
                // Sanjay Jain
                if (dr18[0]["Creation Time"] != null && dr18[0]["Creation Time"].ToString() != "")
                {

                    Label83.Text = DateTime.Parse(dr18[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    Label83.Text = "";
                }
                // Sanjay Jain
                TextBox98.Text = dr18[0]["Pending Amount"].ToString();
                if (dr18[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox27.Checked = true;
                }
                else
                {
                    CheckBox27.Checked = false;
                }
                if (dr18[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr18[0]["Pending Amount"]);
                }
                if (TextBox96.Text == "NoDues")
                {
                    TextBox97.Enabled = false;
                    TextBox98.Enabled = false;
                    Button25.ForeColor = System.Drawing.Color.White;
                    Button25.BackColor = System.Drawing.Color.Green;
                    Button25.Enabled = false;
                    CheckBox27.Enabled = false;
                }
                else
                {
                    Button25.ForeColor = System.Drawing.Color.White;
                    Button25.BackColor = System.Drawing.Color.Red;
                }
            }

            DataRow[] dr19 = dt.Select("Particular = 'Tb & Chest (Pulmonary Medicine)'");
            if (dr19.Length > 0)
            {
                TextBox100.Text = dr19[0]["Status of Dues"].ToString();
                TextBox101.Text = dr19[0]["Remark"].ToString();
                // Sanjay Jain
                if (dr19[0]["Creation Time"] != null && dr19[0]["Creation Time"].ToString() != "")
                {

                    Label84.Text = DateTime.Parse(dr19[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    Label84.Text = "";
                }
                // Sanjay Jain
                TextBox103.Text = dr19[0]["ID"].ToString();
                TextBox102.Text = dr19[0]["Pending Amount"].ToString();
                if (dr19[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox28.Checked = true;
                }
                else
                {
                    CheckBox28.Checked = false;
                }
                if (dr19[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr19[0]["Pending Amount"]);
                }
                if (TextBox100.Text == "NoDues")
                {
                    TextBox101.Enabled = false;
                    TextBox102.Enabled = false;
                    Button26.ForeColor = System.Drawing.Color.White;
                    Button26.BackColor = System.Drawing.Color.Green;
                    Button26.Enabled = false;
                    CheckBox28.Enabled = false;
                }
                else
                {
                    Button26.ForeColor = System.Drawing.Color.White;
                    Button26.BackColor = System.Drawing.Color.Red;
                }
            }
            DataRow[] dr20 = dt.Select("Particular = 'Radiology'");
            if (dr20.Length > 0)
            {
                TextBox104.Text = dr20[0]["Status of Dues"].ToString();
                TextBox105.Text = dr20[0]["Remark"].ToString();
                // Sanjay Jain
                if (dr20[0]["Creation Time"] != null && dr20[0]["Creation Time"].ToString() != "")
                {

                    Label85.Text = DateTime.Parse(dr20[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    Label85.Text = "";
                }
                // Sanjay Jain
                TextBox107.Text = dr20[0]["ID"].ToString();
                TextBox106.Text = dr20[0]["Pending Amount"].ToString();
                if (dr20[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox29.Checked = true;
                }
                else
                {
                    CheckBox29.Checked = false;
                }
                if (dr20[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr20[0]["Pending Amount"]);
                }
                if (TextBox104.Text == "NoDues")
                {
                    TextBox105.Enabled = false;
                    TextBox106.Enabled = false;
                    Button27.ForeColor = System.Drawing.Color.White;
                    Button27.BackColor = System.Drawing.Color.Green;
                    Button27.Enabled = false;
                    CheckBox29.Enabled = false;
                }
                else
                {
                    Button27.ForeColor = System.Drawing.Color.White;
                    Button27.BackColor = System.Drawing.Color.Red;
                }
            }
            DataRow[] dr21 = dt.Select("Particular = 'Pathlogy(Blood Bank)'");
            if (dr21.Length > 0)
            {
                TextBox108.Text = dr21[0]["Status of Dues"].ToString();
                TextBox109.Text = dr21[0]["Remark"].ToString();
                // Sanjay Jain
                if (dr21[0]["Creation Time"] != null && dr21[0]["Creation Time"].ToString() != "")
                {

                    Label86.Text = DateTime.Parse(dr21[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    Label86.Text = "";
                }
                // Sanjay Jain
                TextBox111.Text = dr21[0]["ID"].ToString();
                TextBox110.Text = dr21[0]["Pending Amount"].ToString();
                if (dr21[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox30.Checked = true;
                }
                else
                {
                    CheckBox30.Checked = false;
                }
                if (dr21[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr21[0]["Pending Amount"]);
                }
                if (TextBox108.Text == "NoDues")
                {
                    TextBox109.Enabled = false;
                    TextBox110.Enabled = false;
                    Button28.ForeColor = System.Drawing.Color.White;
                    Button28.BackColor = System.Drawing.Color.Green;
                    Button28.Enabled = false;
                    CheckBox30.Enabled = false;
                }
                else
                {
                    Button28.ForeColor = System.Drawing.Color.White;
                    Button28.BackColor = System.Drawing.Color.Red;
                }
            }

            DataRow[] dr22 = dt.Select("Particular = 'Forensic Medicine'");
            if (dr22.Length > 0)
            {
                TextBox112.Text = dr22[0]["Status of Dues"].ToString();
                TextBox113.Text = dr22[0]["Remark"].ToString();
                // Sanjay Jain
                if (dr22[0]["Creation Time"] != null && dr22[0]["Creation Time"].ToString() != "")
                {

                    Label87.Text = DateTime.Parse(dr22[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    Label87.Text = "";
                }
                // Sanjay Jain
                TextBox115.Text = dr22[0]["ID"].ToString();
                TextBox114.Text = dr22[0]["Pending Amount"].ToString();
                if (dr22[0]["Check NoDues Status"].ToString() == "Y")
                {
                    CheckBox31.Checked = true;
                }
                else
                {
                    CheckBox31.Checked = false;
                }
                if (dr22[0]["Pending Amount"] == "")
                {
                    Total = Total + 0;
                }
                else
                {
                    Total = Total + Convert.ToDecimal(dr22[0]["Pending Amount"]);
                }
                if (TextBox112.Text == "NoDues")
                {
                    TextBox113.Enabled = false;
                    TextBox114.Enabled = false;
                    Button29.ForeColor = System.Drawing.Color.White;
                    Button29.BackColor = System.Drawing.Color.Green;
                    Button29.Enabled = false;
                    CheckBox31.Enabled = false;
                }
                else
                {
                    Button29.ForeColor = System.Drawing.Color.White;
                    Button29.BackColor = System.Drawing.Color.Red;

                }
            }
            TextBox116.Text = Total.ToString();
        }
    }

    public void Button11_Click(object sender, EventArgs e)
    {
        ExportToExcel();
    }
    public void ExportToExcel()
    {
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString()))
        {
            SqlCommand cmd = new SqlCommand("pro_StudentnodueslistTMMCNewList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@DeptName", Session["GlobalDimension1Code"].ToString());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Columns.Contains("Status"))
            {
                dt.Columns.Remove("Status");
            }
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("NoDuesApproveStatus");
                worksheet.Cells["A1"].LoadFromDataTable(dt, true);
                var headerRange = worksheet.Cells[1, 1, 1, dt.Columns.Count];
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                headerRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Gray);
                headerRange.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                using (var memoryStream = new MemoryStream())
                {
                    package.SaveAs(memoryStream);
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=StudentNoDues.xlsx");
                    HttpContext.Current.Response.BinaryWrite(memoryStream.ToArray());
                    HttpContext.Current.Response.End();
                }
            }
        }
    }

    public void getdeptlibrary()
    {
        if (Session["uid"].ToString() == "TMU00034" || Session["uid"].ToString() == "TMU00027")
        {

            //DropDownList1.Enabled = true;
            txtremarkdeptlibrary.Enabled = true;
            btn_DepartmentLib.Enabled = true;
            //DropDownList2.Enabled = true;
            btn_Centrallib.Enabled = true;
            txtremarkcentrallib.Enabled = true;
            txtpendingamountdeptlibrary.Enabled = true;
            txtpendingamountcentrallibrary.Enabled = true;
            CheckBox1.Enabled = true;
            CheckBox3.Enabled = true;
            CheckBox10.Enabled = true;
            TextBox29.Enabled = true;
            TextBox30.Enabled = true;
            Button6.Enabled = true;

        }
        else
        {
            // DropDownList1.Enabled = false;
            txtremarkdeptlibrary.Enabled = false;
            btn_DepartmentLib.Enabled = false;
            //DropDownList2.Enabled = false;
            btn_Centrallib.Enabled = false;
            txtremarkcentrallib.Enabled = false;
            txtpendingamountdeptlibrary.Enabled = false;
            txtpendingamountcentrallibrary.Enabled = false;
            CheckBox1.Enabled = false;
            CheckBox3.Enabled = false;
            CheckBox10.Enabled = false;
            TextBox29.Enabled = false;
            TextBox30.Enabled = false;
            Button6.Enabled = false;


        }

        if (Session["uid"].ToString() == "TMU00784")
        {
            // DrpSport.Enabled = true;
            txtremarksportic.Enabled = true;
            Button14.Enabled = true;
            TextBox5.Enabled = true;
            CheckBox8.Enabled = true;
            CheckBox15.Enabled = true;
            TextBox49.Enabled = true;
            TextBox50.Enabled = true;
            Button12.Enabled = true;
        }
        else
        {
            //DrpSport.Enabled = false;
            txtremarksportic.Enabled = false;
            Button14.Enabled = false;
            TextBox5.Enabled = false;
            CheckBox8.Enabled = false;
            CheckBox15.Enabled = false;
            TextBox49.Enabled = false;
            TextBox50.Enabled = false;
            Button12.Enabled = false;
        }
        if (Session["uid"].ToString() == "TMU01979")
        {
            // DrpSport.Enabled = true;
            txtremarkIT.Enabled = true;
            Button814.Enabled = true;
            TextBox858.Enabled = true;
            CheckBox88.Enabled = true;
            CheckBox915.Enabled = true;
            TextBox949.Enabled = true;
            TextBox950.Enabled = true;
            Button912.Enabled = true;
        }
        else
        {
            //DrpSport.Enabled = false;
            txtremarkIT.Enabled = false;
            Button814.Enabled = false;
            TextBox858.Enabled = false;
            CheckBox88.Enabled = false;
            CheckBox915.Enabled = false;
            TextBox949.Enabled = false;
            TextBox950.Enabled = false;
            Button912.Enabled = false;
        }
        if (Session["uid"].ToString() == "TMU00378")
        {
            // DrpSport.Enabled = true;
            txtremarkSecurity.Enabled = true;
            Button814Security.Enabled = true;
            TextBox858Security.Enabled = true;
            CheckBox88Security.Enabled = true;
            //CheckBox915Security.Enabled = true;
            //TextBox949Security.Enabled = true;
            //TextBox950Security.Enabled = true;
            //Button912Security.Enabled = true;
        }
        else
        {
            //DrpSport.Enabled = false;
            txtremarkSecurity.Enabled = false;
            Button814Security.Enabled = false;
            TextBox858Security.Enabled = false;
            CheckBox88Security.Enabled = false;
            //CheckBox915Security.Enabled = false;
            //TextBox949Security.Enabled = false;
            //TextBox950Security.Enabled = false;
            //Button912Security.Enabled = false;
        }
        if (Session["uid"].ToString() == "TMU00140")
        {
            // DrpSport.Enabled = true;
            txtremarkExamination.Enabled = true;
            Button814Examination.Enabled = true;
            TextBox858Examination.Enabled = true;
            CheckBox88Examination.Enabled = true;
            //CheckBox915Examination.Enabled = true;
            //TextBox949Examination.Enabled = true;
            //TextBox950Examination.Enabled = true;
            //Button912Examination.Enabled = true;
        }
        else
        {
            //DrpSport.Enabled = false;
            txtremarkExamination.Enabled = false;
            Button814Examination.Enabled = false;
            TextBox858Examination.Enabled = false;
            CheckBox88Examination.Enabled = false;
            //CheckBox915Examination.Enabled = false;
            //TextBox949Examination.Enabled = false;
            //TextBox950Examination.Enabled = false;
            //Button912Examination.Enabled = false;
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("sp_getlibrarydept4", con);
        cmd.Parameters.AddWithValue("@dept", txtcollegedept.Text);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);

        lbldepartmentlib.Text = dtCL.Select("Particulars='Department Library'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lbldepartmentlibdeg.Text = dtCL.Select("Particulars='Department Library'").CopyToDataTable().Rows[0]["Designation"].ToString();
        // txtdepartmentlibemployeecode.Text = dtCL.Select("Particulars='Department Library'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblcentlibname.Text = dtCL.Select("Particulars='Central Library'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblcentlibdeg.Text = dtCL.Select("Particulars='Central Library'").CopyToDataTable().Rows[0]["Designation"].ToString();
        Label32.Text = dtCL.Select("Particulars='Central Library'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        Label33.Text = dtCL.Select("Particulars='Central Library'").CopyToDataTable().Rows[0]["Designation"].ToString();
        // lblcentlibnameemployeecode.Text = dtCL.Select("Particulars='Central Library'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lbldeptLaboratory.Text = dtCL.Select("Particulars='College/Department Laboratory'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lbldeptLaboratorydeg.Text = dtCL.Select("Particulars='College/Department Laboratory'").CopyToDataTable().Rows[0]["Designation"].ToString();

        Label17InternshipStatus.Text = dtCL.Select("Particulars='College/Department Laboratory'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        Label42InternshipStatus.Text = dtCL.Select("Particulars='College/Department Laboratory'").CopyToDataTable().Rows[0]["Designation"].ToString();


        lblcollegeworkname.Text = dtCL.Select("Particulars='College/Department Wprkshop'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblcollegeworkdeg.Text = dtCL.Select("Particulars='College/Department Wprkshop'").CopyToDataTable().Rows[0]["Designation"].ToString();

        //lblAlumnifeename.Text = dtCL.Select("Particulars='Alumni Fee'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        //lblAlumnifeedeg.Text = dtCL.Select("Particulars='Alumni Fee'").CopyToDataTable().Rows[0]["Designation"].ToString();
        ////lblhostelmessnamecode.Text = dtCL.Select("Particulars='Hostel Mess'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblaccountname.Text = dtCL.Select("Particulars='Account Deptt'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblaccountdeg.Text = dtCL.Select("Particulars='Account Deptt'").CopyToDataTable().Rows[0]["Designation"].ToString();

        ////lblhosteloffficenamecode.Text = dtCL.Select("Particulars='Hostel Office'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblsportname.Text = dtCL.Select("Particulars='Sport I/c'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblsportdeg.Text = dtCL.Select("Particulars='Sport I/c'").CopyToDataTable().Rows[0]["Designation"].ToString();

        lblsportcode.Text = dtCL.Select("Particulars='Sport I/c'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();
        lblsportsname.Text = dtCL.Select("Particulars='Sport I/c'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblsportsdesi.Text = dtCL.Select("Particulars='Sport I/c'").CopyToDataTable().Rows[0]["Designation"].ToString();

        lblITname.Text = dtCL.Select("Particulars='IT Department'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblITdeg.Text = dtCL.Select("Particulars='IT Department'").CopyToDataTable().Rows[0]["Designation"].ToString();

        lblITcode.Text = dtCL.Select("Particulars='IT Department'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();
        lblITsname.Text = dtCL.Select("Particulars='IT Department'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblITsdesi.Text = dtCL.Select("Particulars='IT Department'").CopyToDataTable().Rows[0]["Designation"].ToString();

        lblExaminationname.Text = dtCL.Select("Particulars='Examination Department'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblExaminationdeg.Text = dtCL.Select("Particulars='Examination Department'").CopyToDataTable().Rows[0]["Designation"].ToString();
        //lblExaminationcode.Text = dtCL.Select("Particulars='Examination Department'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();
        // lblExaminationname.Text = dtCL.Select("Particulars='Examination Department'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        // lblExaminationdesi.Text = dtCL.Select("Particulars='Examination Department'").CopyToDataTable().Rows[0]["Designation"].ToString();

        lblSecurityname.Text = dtCL.Select("Particulars='Security Department'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblSecuritydeg.Text = dtCL.Select("Particulars='Security Department'").CopyToDataTable().Rows[0]["Designation"].ToString();
        //  lblITcode.Text = dtCL.Select("Particulars='IT Department'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();
        //  lblITsname.Text = dtCL.Select("Particulars='IT Department'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        //  lblITsdesi.Text = dtCL.Select("Particulars='IT Department'").CopyToDataTable().Rows[0]["Designation"].ToString();


        //lblsportnamecode.Text = dtCL.Select("Particulars='Sport I/c'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblaccountname.Text = dtCL.Select("Particulars='Account Deptt'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        txtaccountemployeecode.Text = dtCL.Select("Particulars='Account Deptt'").CopyToDataTable().Rows[0]["Designation"].ToString();

        lblCommunityMedicinename.Text = dtCL.Select("Particulars='Community Medicine'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblCommunityMedicinedesig.Text = dtCL.Select("Particulars='Community Medicine'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblCommunityMedicinecode.Text = dtCL.Select("Particulars='Community Medicine'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblGeneralMedicineName.Text = dtCL.Select("Particulars='General Medicine'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblGeneralMedicinedegis.Text = dtCL.Select("Particulars='General Medicine'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblGeneralMedicineCode.Text = dtCL.Select("Particulars='General Medicine'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblPsychiatryname.Text = dtCL.Select("Particulars='Psychiatry'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblPsychiatrydesig.Text = dtCL.Select("Particulars='Psychiatry'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblPsychiatrycode.Text = dtCL.Select("Particulars='Psychiatry'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblGeneralSurgeryname.Text = dtCL.Select("Particulars='General Surgery'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblGeneralSurgerydesi.Text = dtCL.Select("Particulars='General Surgery'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblGeneralSurgerycode.Text = dtCL.Select("Particulars='General Surgery'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblAnesthisiaName.Text = dtCL.Select("Particulars='Anesthisia'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblAnesthisiadesi.Text = dtCL.Select("Particulars='Anesthisia'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblAnesthisiaCode.Text = dtCL.Select("Particulars='Anesthisia'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblObsGyaneName.Text = dtCL.Select("Particulars='Obs & Gyane'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblObsGyanedesi.Text = dtCL.Select("Particulars='Obs & Gyane'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblObsGyaneCode.Text = dtCL.Select("Particulars='Obs & Gyane'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();


        lblPediatricsname.Text = dtCL.Select("Particulars='Pediatrics'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblPediatricsdesi.Text = dtCL.Select("Particulars='Pediatrics'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblPediatricscode.Text = dtCL.Select("Particulars='Pediatrics'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblOrthopedicsname.Text = dtCL.Select("Particulars='Orthopedics'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblOrthopedicsdesi.Text = dtCL.Select("Particulars='Orthopedics'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblOrthopedicscode.Text = dtCL.Select("Particulars='Orthopedics'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblentname.Text = dtCL.Select("Particulars='Ent'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblentdesi.Text = dtCL.Select("Particulars='Ent'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblEntcode.Text = dtCL.Select("Particulars='Ent'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblOphthalmologyname.Text = dtCL.Select("Particulars='Ophthalmology'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblOphthalmologydesi.Text = dtCL.Select("Particulars='Ophthalmology'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblOphthalmologycode.Text = dtCL.Select("Particulars='Ophthalmology'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblCasualtyname.Text = dtCL.Select("Particulars='Casualty'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblCasualtydesi.Text = dtCL.Select("Particulars='Casualty'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblCasualtycode.Text = dtCL.Select("Particulars='Casualty'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();


        lblDermatologyname.Text = dtCL.Select("Particulars='Dermatology'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblDermatologydesi.Text = dtCL.Select("Particulars='Dermatology'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblDermatologycode.Text = dtCL.Select("Particulars='Dermatology'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();


        lbltbchestname.Text = dtCL.Select("Particulars='Tb & Chest (Pulmonary Medicine)'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lbltbchestdesi.Text = dtCL.Select("Particulars='Tb & Chest (Pulmonary Medicine)'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lbltbchestcode.Text = dtCL.Select("Particulars='Tb & Chest (Pulmonary Medicine)'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblRadiologyname.Text = dtCL.Select("Particulars='Radiology'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblRadiologydesi.Text = dtCL.Select("Particulars='Radiology'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblRadiologycode.Text = dtCL.Select("Particulars='Radiology'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblPathlogyname.Text = dtCL.Select("Particulars='Pathlogy(Blood Bank)'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblPathlogydesi.Text = dtCL.Select("Particulars='Pathlogy(Blood Bank)'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblPathlogycode.Text = dtCL.Select("Particulars='Pathlogy(Blood Bank)'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblForensicMedicinename.Text = dtCL.Select("Particulars='Forensic Medicine'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblForensicMedicinedesi.Text = dtCL.Select("Particulars='Forensic Medicine'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblForensicMedicinecode.Text = dtCL.Select("Particulars='Forensic Medicine'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        //lblhostelsname.Text = dtCL.Select("Particulars='Hostel'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        //lblhosteldeg.Text = dtCL.Select("Particulars='Hostel'").CopyToDataTable().Rows[0]["Designation"].ToString();
        ////lbltransportofficenamecode.Text = dtCL.Select("Particulars='Transport Office'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        //lblwashermanname.Text = dtCL.Select("Particulars='Hostel'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        //lblwashermandeg.Text = dtCL.Select("Particulars='Hostel'").CopyToDataTable().Rows[0]["Designation"].ToString();
        ////lbltransportofficenamecode.Text = dtCL.Select("Particulars='Transport Office'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();
    }
    //public void getnodueslist()
    //{
    //    SqlCommand cmd = new SqlCommand("pro_Studentnodueslist", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());

    //    if (con.State == ConnectionState.Closed)
    //        con.Open();
    //    SqlDataAdapter daCL = new SqlDataAdapter(cmd);
    //    DataTable dtCL = new DataTable();
    //    daCL.Fill(dtCL);
    //    GriedviewStudent.DataSource = dtCL;
    //    GriedviewStudent.DataBind();
    //}
    public void getnodueslistTMMC()
    {
        SqlCommand cmd = new SqlCommand("pro_StudentnodueslistTMMCNewList", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@DeptName", Session["GlobalDimension1Code"].ToString());
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        DataView dv = new DataView(dtCL);
        dv.RowFilter = "NOT ([Enrollement No] IS NULL OR [Enrollement No] = '')"; // Adjust Column1, Column2 as needed
        GriedviewStudent.DataSource = dv.ToTable();
        GriedviewStudent.DataBind();
    }
    public void binddata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        string strSQL = "select Case when Gender=1 then 'Male' else 'Female' end as Gender,*  from [EDUCOLLEGELIVE-R2].dbo.[TMU$Student - COLLEGE] where [Enrollment No_]='" + UserId.Text + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            UserId.Text = dt.Rows[0]["Enrollment No_"].ToString();
            txtstudentName.Text = dt.Rows[0]["Student Name"].ToString();
            lblcertifiedname.Text = dt.Rows[0]["Student Name"].ToString();
            txtfathername.Text = dt.Rows[0]["Fathers Name"].ToString();
            txtmobile.Text = dt.Rows[0]["Mobile Number"].ToString();
            txtemailid.Text = dt.Rows[0]["E-Mail Address"].ToString();
            txtPrograme.Text = dt.Rows[0]["Course Name"].ToString();
            txtcollegedept.Text = dt.Rows[0]["Global Dimension 1 Code"].ToString();
            lblcertifiedname.Text = dt.Rows[0]["Student Name"].ToString();
            lblYear.Text = dt.Rows[0]["Academic Year"].ToString();
            txtgender.Text = dt.Rows[0]["Gender"].ToString();
        }
    }
    public void binddataHOD()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        string strSQL = "select Distinct Principal,(select [First Name] from TMU$Employee where No_=T.Principal) as P,(select [Job Title_Grade Desc] from TMU$Employee where No_=T.Principal) as Title from [TMU$User Role Matrix]  as T where Status=1 and [Global Dimenison 1 Code] ='" + txtcollegedept.Text + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            if (txtcollegedept.Text.ToString() == "TMDC")
            {
                txtdeptlaboratorydegcode.Text = "TMU00974";
                txtCollegeDepartmentWorkshopcode.Text = "TMU00974";
                lbldeptLaboratory.Text = "DR ANKITA JAIN";
                Label17InternshipStatus.Text = "DR ANKITA JAIN";
                Label42InternshipStatus.Text = "VICE PRINCIPAL";
                lblcollegeworkname.Text = "DR ANKITA JAIN";
                lbldeptLaboratorydeg.Text = "VICE PRINCIPAL";
                lblcollegeworkdeg.Text = "VICE PRINCIPAL";
            }
            else
            {
                txtdeptlaboratorydegcode.Text = dt.Rows[0]["Principal"].ToString();
                txtCollegeDepartmentWorkshopcode.Text = dt.Rows[0]["Principal"].ToString();
                lbldeptLaboratory.Text = dt.Rows[0]["P"].ToString();
                Label17InternshipStatus.Text = dt.Rows[0]["P"].ToString();
                Label42InternshipStatus.Text = dt.Rows[0]["Title"].ToString();
                lblcollegeworkname.Text = dt.Rows[0]["P"].ToString();
                lbldeptLaboratorydeg.Text = dt.Rows[0]["Title"].ToString();
                lblcollegeworkdeg.Text = dt.Rows[0]["Title"].ToString();

            }
        }
        if (Session["uid"].ToString() == txtdeptlaboratorydegcode.Text || Session["uid"].ToString() == txtCollegeDepartmentWorkshopcode.Text)
        {


            txtremarkdepartmentLaboratory.Enabled = true;
            btn_DeptLaboratory.Enabled = true;
            txtremarkdepartmentwork.Enabled = true;

            Button8.Enabled = true;
            txtpandingamountdeptlaboratory.Enabled = true;
            TextBox1.Enabled = true;
            CheckBox2.Enabled = true;
            CheckBox4.Enabled = true;

        }
        else
        {


            txtremarkdepartmentLaboratory.Enabled = false;
            btn_DeptLaboratory.Enabled = false;
            txtremarkdepartmentwork.Enabled = false;
            //DropDownList8.Enabled = false;
            Button8.Enabled = false;
            txtpandingamountdeptlaboratory.Enabled = false;
            TextBox1.Enabled = false;
            CheckBox2.Enabled = false;
            CheckBox4.Enabled = false;

        }
        if (Session["uid"].ToString() == txtaccountemployeecode.Text)
        {

            txtotherfee.Enabled = true;
            TextBox34.Enabled = true;
            TextBox7.Enabled = false;
            TextBox35.Enabled = false;
            Button3.Enabled = true;
            CheckBox9.Enabled = true;
            CheckBox11.Enabled = true;
            Button2.Visible = true;
            Button7.Enabled = true;
            if (txtcollegedept.Text == "TMMC")
            {
                hidePaymentbuttonTMMC();
                VerifyButtonTMMC();
            }
            {
                hidePaymentbutton();
                VerifyButton();
            }


        }
        else
        {
            TextBox34.Enabled = false;
            txtotherfee.Enabled = false;
            TextBox7.Enabled = false;
            TextBox35.Enabled = false;
            Button3.Enabled = false;
            CheckBox9.Enabled = false;
            CheckBox11.Enabled = false;
            Button2.Visible = false;
            Button7.Enabled = false;

        }
        if (Session["uid"].ToString() == lblCommunityMedicinecode.Text)
        {
            CheckBox16.Enabled = true;
            TextBox53.Enabled = true;
            TextBox54.Enabled = true;
            Button13.Enabled = true;
        }
        else
        {
            CheckBox16.Enabled = false;
            TextBox53.Enabled = false;
            TextBox54.Enabled = false;
            Button13.Enabled = false;
        }

        if (Session["uid"].ToString() == lblGeneralMedicineCode.Text)
        {
            CheckBox17.Enabled = true;
            TextBox57.Enabled = true;
            TextBox58.Enabled = true;
            Button15.Enabled = true;
        }
        else
        {
            CheckBox17.Enabled = false;
            TextBox57.Enabled = false;
            TextBox58.Enabled = false;
            Button15.Enabled = false;
        }
        if (Session["uid"].ToString() == lblPsychiatrycode.Text)
        {
            CheckBox18.Enabled = true;
            TextBox61.Enabled = true;
            TextBox62.Enabled = true;
            Button16.Enabled = true;
        }
        else
        {
            CheckBox18.Enabled = false;
            TextBox61.Enabled = false;
            TextBox62.Enabled = false;
            Button16.Enabled = false;
        }
        if (Session["uid"].ToString() == lblGeneralSurgerycode.Text)
        {
            CheckBox19.Enabled = true;
            TextBox65.Enabled = true;
            TextBox66.Enabled = true;
            Button17.Enabled = true;
        }
        else
        {
            CheckBox19.Enabled = false;
            TextBox65.Enabled = false;
            TextBox66.Enabled = false;
            Button17.Enabled = false;
        }
        if (Session["uid"].ToString() == lblAnesthisiaCode.Text)
        {
            CheckBox20.Enabled = true;
            TextBox69.Enabled = true;
            TextBox70.Enabled = true;
            Button18.Enabled = true;
        }
        else
        {
            CheckBox20.Enabled = false;
            TextBox69.Enabled = false;
            TextBox70.Enabled = false;
            Button18.Enabled = false;
        }
        if (Session["uid"].ToString() == lblObsGyaneCode.Text)
        {
            CheckBox21.Enabled = true;
            TextBox73.Enabled = true;
            TextBox74.Enabled = true;
            Button19.Enabled = true;
        }
        else
        {
            CheckBox21.Enabled = false;
            TextBox73.Enabled = false;
            TextBox74.Enabled = false;
            Button19.Enabled = false;
        }
        if (Session["uid"].ToString() == lblPediatricscode.Text)
        {
            CheckBox22.Enabled = true;
            TextBox77.Enabled = true;
            TextBox78.Enabled = true;
            Button20.Enabled = true;
        }
        else
        {
            CheckBox22.Enabled = false;
            TextBox77.Enabled = false;
            TextBox78.Enabled = false;
            Button20.Enabled = false;
        }
        if (Session["uid"].ToString() == lblOrthopedicscode.Text)
        {
            CheckBox23.Enabled = true;
            TextBox81.Enabled = true;
            TextBox82.Enabled = true;
            Button21.Enabled = true;
        }
        else
        {
            CheckBox23.Enabled = false;
            TextBox81.Enabled = false;
            TextBox82.Enabled = false;
            Button21.Enabled = false;
        }
        if (Session["uid"].ToString() == lblEntcode.Text)
        {
            CheckBox24.Enabled = true;
            TextBox85.Enabled = true;
            TextBox86.Enabled = true;
            Button22.Enabled = true;
        }
        else
        {
            CheckBox24.Enabled = false;
            TextBox85.Enabled = false;
            TextBox86.Enabled = false;
            Button22.Enabled = false;
        }
        if (Session["uid"].ToString() == lblOphthalmologycode.Text)
        {
            CheckBox25.Enabled = true;
            TextBox89.Enabled = true;
            TextBox90.Enabled = true;
            Button23.Enabled = true;
        }
        else
        {
            CheckBox25.Enabled = false;
            TextBox89.Enabled = false;
            TextBox90.Enabled = false;
            Button23.Enabled = false;
        }
        if (Session["uid"].ToString() == lblCasualtycode.Text)
        {
            CheckBox26.Enabled = true;
            TextBox93.Enabled = true;
            TextBox94.Enabled = true;
            Button24.Enabled = true;
        }
        else
        {
            CheckBox26.Enabled = false;
            TextBox93.Enabled = false;
            TextBox94.Enabled = false;
            Button24.Enabled = false;
        }
        if (Session["uid"].ToString() == lblDermatologycode.Text)
        {
            CheckBox27.Enabled = true;
            TextBox97.Enabled = true;
            TextBox98.Enabled = true;
            Button25.Enabled = true;
        }
        else
        {
            CheckBox27.Enabled = false;
            TextBox97.Enabled = false;
            TextBox98.Enabled = false;
            Button25.Enabled = false;
        }
        if (Session["uid"].ToString() == lbltbchestcode.Text)
        {
            CheckBox28.Enabled = true;
            TextBox101.Enabled = true;
            TextBox102.Enabled = true;
            Button26.Enabled = true;
        }
        else
        {
            CheckBox28.Enabled = false;
            TextBox101.Enabled = false;
            TextBox102.Enabled = false;
            Button26.Enabled = false;
        }
        if (Session["uid"].ToString() == lblRadiologycode.Text)
        {
            CheckBox29.Enabled = true;
            TextBox105.Enabled = true;
            TextBox106.Enabled = true;
            Button27.Enabled = true;
        }
        else
        {
            CheckBox29.Enabled = false;
            TextBox105.Enabled = false;
            TextBox106.Enabled = false;
            Button27.Enabled = false;
        }
        if (Session["uid"].ToString() == lblPathlogycode.Text)
        {
            CheckBox30.Enabled = true;
            TextBox109.Enabled = true;
            TextBox110.Enabled = true;
            Button28.Enabled = true;
        }
        else
        {
            CheckBox30.Enabled = false;
            TextBox109.Enabled = false;
            TextBox110.Enabled = false;
            Button28.Enabled = false;
        }
        if (Session["uid"].ToString() == lblForensicMedicinecode.Text)
        {
            CheckBox31.Enabled = true;
            TextBox113.Enabled = true;
            TextBox114.Enabled = true;
            Button29.Enabled = true;
        }
        else
        {
            CheckBox31.Enabled = false;
            TextBox113.Enabled = false;
            TextBox114.Enabled = false;
            Button29.Enabled = false;
        }
    }
    protected void lnkbutton_Click(object sender, EventArgs e)
    {


        GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
        int index = row.RowIndex;
        Label UserId = (Label)GriedviewStudent.Rows[index].FindControl("lblemployeecode");
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "select *,(Select [First Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=[Principal Id]) as 'Principal Name' from Tbl_StudentNodues WHERE [ST NO_]='" + UserId.Text + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["Hostel Id"].ToString() == Session["uid"].ToString())
            {

                txtremarkhostel.Enabled = true;
                Btn_Hostel.Enabled = true;
                txtelectrictydeptremark.Enabled = true;
                Button1.Enabled = true;
                TextBox4.Enabled = true;
                TextBox2.Enabled = true;
                CheckBox5.Enabled = true;
                CheckBox7.Enabled = true;
                CheckBox12.Enabled = true;
                CheckBox13.Enabled = true;
                TextBox37.Enabled = true;
                TextBox38.Enabled = true;
                Button9.Enabled = true;
                TextBox41.Enabled = true;
                TextBox42.Enabled = true;
                Button10.Enabled = true;
            }
            else
            {

                txtremarkhostel.Enabled = false;
                Btn_Hostel.Enabled = false;
                txtelectrictydeptremark.Enabled = false;
                Button1.Enabled = false;
                TextBox4.Enabled = false;
                TextBox2.Enabled = false;
                CheckBox5.Enabled = false;
                CheckBox7.Enabled = false;
                CheckBox12.Enabled = false;
                CheckBox13.Enabled = false;
                TextBox37.Enabled = false;
                TextBox38.Enabled = false;
                Button9.Enabled = false;
                TextBox41.Enabled = false;
                TextBox42.Enabled = false;
                Button10.Enabled = false;
            }
            this.UserId.Text = dt.Rows[0]["Enrollement No"].ToString();
            TextBox17.Text = dt.Rows[0]["Enrollement No"].ToString();
            txtstudentName.Text = dt.Rows[0]["Student Name"].ToString();
            TextBox19.Text = dt.Rows[0]["Student Name"].ToString();
            txtfathername.Text = dt.Rows[0]["Father's Name"].ToString();
            TextBox20.Text = dt.Rows[0]["Father's Name"].ToString();
            txtcollegedept.Text = dt.Rows[0]["College/Dept"].ToString();
            TextBox21.Text = dt.Rows[0]["College/Dept"].ToString();
            txtPrograme.Text = dt.Rows[0]["Programme"].ToString();
            TextBox22.Text = dt.Rows[0]["Programme"].ToString();
            txtmobile.Text = dt.Rows[0]["Mobile No_"].ToString();
            TextBox23.Text = dt.Rows[0]["Mobile No_"].ToString();
            txtemailid.Text = dt.Rows[0]["Email Id"].ToString();
            TextBox24.Text = dt.Rows[0]["Email Id"].ToString();
            txtgender.Text = dt.Rows[0]["Gender"].ToString();
            TextBox25.Text = dt.Rows[0]["Gender"].ToString();
            txtSection.Text = dt.Rows[0]["Section"].ToString();
            txtdatedirectorprincipaldate.Text = dt.Rows[0]["Date"].ToString();
            lblhosteldeg.Text = dt.Rows[0]["Hostel Designation"].ToString();
            lblhostelsname.Text = dt.Rows[0]["Hostel Employee_Name"].ToString();
            lblHostalDesignation.Text = dt.Rows[0]["Hostel Designation"].ToString();
            lblHostalname.Text = dt.Rows[0]["Hostel Employee_Name"].ToString();
            lblhostelemployeecode.Text = dt.Rows[0]["Hostel Id"].ToString();
            Label39.Text = dt.Rows[0]["Hostel Id"].ToString();
            Label40.Text = dt.Rows[0]["Hostel Employee_Name"].ToString();
            Label41.Text = dt.Rows[0]["Hostel Designation"].ToString();
            lblelectrictydeptname.Text = dt.Rows[0]["Hostel Employee_Name"].ToString();
            lblelectrictydeptdeg.Text = dt.Rows[0]["Hostel Designation"].ToString();
            lblaccountname.Text = dt.Rows[0]["Account Employee Name"].ToString();
            Label35.Text = dt.Rows[0]["Account Employee Name"].ToString();
            lblaccountdeg.Text = dt.Rows[0]["Account Dept Designation"].ToString();
            Label36.Text = dt.Rows[0]["Account Dept Designation"].ToString();
            txtaccountemployeecode.Text = dt.Rows[0]["Account Dept Id"].ToString();
            lblaccountcode.Text = dt.Rows[0]["Account Dept Id"].ToString();
            txtnoduesid.Text = dt.Rows[0]["No_Dues_Id"].ToString();
            lbldeptLaboratory.Text= dt.Rows[0]["Principal Name"].ToString();
            lbldeptLaboratorydeg.Text = "Principal";
            if (dt.Rows[0]["Principal Id"].ToString().Equals(Session["uid"].ToString()))
            {
                if ((dt.Rows[0]["Internship Status"].ToString().Equals("") || dt.Rows[0]["Internship Status"].ToString().Equals(null)))
                {
                    txtInternshipStatus.Enabled = true;
                    InternshipStatus.Enabled = true;
                }
                else
                {
                    txtInternshipStatus.Enabled = false;
                    InternshipStatus.Enabled = false;
                }
            }
            else
            {
                txtInternshipStatus.Enabled = false;
                InternshipStatus.Enabled = false;
            }
            if ((!dt.Rows[0]["Internship Status"].ToString().Equals("") && !dt.Rows[0]["Internship Status"].ToString().Equals(null)))
            {
                txtInternshipStatus.SelectedValue = dt.Rows[0]["Internship Status"].ToString();
                txtInternshipStatusDate.Text = DateTime.Parse(dt.Rows[0]["Internship Approval Date"].ToString()).ToString("dd/MM/yyyy");
                InternshipStatus.ForeColor = System.Drawing.Color.White;
                InternshipStatus.BackColor = System.Drawing.Color.Green;

            }
            String LibraryDeptId = dt.Rows[0]["Library Dept Id"].ToString();
            if (LibraryDeptId.Equals("TMU00034"))
            {
                lbldepartmentlib.Text = "ALOK KUMAR GUPTA";
                lbldepartmentlibdeg.Text = "DEPUTY LIBRARIAN";
                lblcentlibname.Text = "ALOK KUMAR GUPTA";
                lblcentlibdeg.Text = "DEPUTY LIBRARIAN";
                Label32.Text = "ALOK KUMAR GUPTA";
                Label33.Text = "DEPUTY LIBRARIAN";
            }
            if (LibraryDeptId.Equals("TMU00027"))
            {
                lbldepartmentlib.Text = "SANJEEV KUMAR";
                lbldepartmentlibdeg.Text = "DEPUTY LIBRARIAN";
                lblcentlibname.Text = "SANJEEV KUMAR";
                lblcentlibdeg.Text = "DEPUTY LIBRARIAN";
                Label32.Text = "SANJEEV KUMAR";
                Label33.Text = "DEPUTY LIBRARIAN";
            }
            //Sanjay Jain
            if (dt.Rows[0]["Creation Time"] != null && dt.Rows[0]["Creation Time"].ToString() != "")
            {

                txtnoduesApplyDate.Text = DateTime.Parse(dt.Rows[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
                TextBox117.Text = DateTime.Parse(dt.Rows[0]["Creation Time"].ToString()).ToString("dd/MM/yyyy");
            }
            else
            {
                txtnoduesApplyDate.Text = "";
                TextBox117.Text = "";
            }
            // Sanjay Jain
            TextBox26.Text = dt.Rows[0]["No_Dues_Id"].ToString();
            hdfSTNO.Value = dt.Rows[0]["ST NO_"].ToString();
            TextBox18.Text = dt.Rows[0]["ST NO_"].ToString();
            if (txtcollegedept.Text == "TMMC")
            {
                ModalPopupExtender1.Show();
                alldept1();
                hidePaymentbuttonTMMC();
                getnodueslistTMMC();
            }
            else
            {
                GridViewdata.Show();

            }
            if (Session["uid"].ToString() == "TMU01174")
            {
                Button31.Visible = true;
                Button32.Visible = true;
                hideSubmitbuttonPrincipal();
            }
            else
            {
                Button31.Visible = false;
                Button32.Visible = false;
            }
            binddataHOD();
            alldept1();
            alldept();
            hidePaymentbutton();

            hideSubmitbutton();
            UpdateAndDisplayAmount();
        }
    }

    protected void btn_DepartmentLib_Click(object sender, EventArgs e)
    {

        if (CheckBox1.Checked)
        {
            if (txtremarkdeptlibrary.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (txtpendingamountdeptlibrary.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox1.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", LabelR1.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lbldepartmentlib.Text);
            cmd.Parameters.AddWithValue("@Designation", lbldepartmentlibdeg.Text);
            cmd.Parameters.AddWithValue("@Remark", txtremarkdeptlibrary.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", txtpendingamountdeptlibrary.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", txtdepartmentlibID.Text);

            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept();
        }
        else
        {

            if (txtremarkdeptlibrary.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }

            string Declaration = CheckBox1.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", LabelR1.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lbldepartmentlib.Text);
            cmd.Parameters.AddWithValue("@Designation", lbldepartmentlibdeg.Text);
            cmd.Parameters.AddWithValue("@Remark", txtremarkdeptlibrary.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Pending");
            cmd.Parameters.AddWithValue("@PendingAmount", txtpendingamountdeptlibrary.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", txtdepartmentlibID.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept();
        }
    }

    protected void InternshipStatus_Click(object sender, EventArgs e)
    {
        string value = txtInternshipStatus.SelectedValue;
        if (!String.IsNullOrEmpty(value) && value != null)
        {
            string query = "UPDATE [HRMSPortal].[dbo].[Tbl_StudentNodues] SET [Internship Status] = @value,[Internship Approval Date]=@currentDate WHERE [Enrollement No] = @UserId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@value", value);
            cmd.Parameters.AddWithValue("@UserId", UserId.Text);
            cmd.Parameters.AddWithValue("@currentDate", DateTime.Now);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept();
            txtInternshipStatus.Enabled = false;
            InternshipStatus.Enabled = false;
            InternshipStatus.ForeColor = System.Drawing.Color.White;
            InternshipStatus.BackColor = System.Drawing.Color.Green;
        }
        else
        {
        }
    }

    protected void btn_Centrallib_Click(object sender, EventArgs e)
    {

        if (CheckBox3.Checked)
        {
            if (txtremarkcentrallib.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (txtpendingamountcentrallibrary.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox3.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label5.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblcentlibname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblcentlibdeg.Text);
            cmd.Parameters.AddWithValue("@Remark", txtremarkcentrallib.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", txtpendingamountcentrallibrary.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", txtCentrallibID.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept();
        }
        else
        {


            if (txtremarkcentrallib.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }

            string Declaration = CheckBox3.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label5.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblcentlibname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblcentlibdeg.Text);
            cmd.Parameters.AddWithValue("@Remark", txtremarkcentrallib.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Pending");
            cmd.Parameters.AddWithValue("@PendingAmount", txtpendingamountcentrallibrary.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", txtCentrallibID.Text);

            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept();
        }
    }
    protected void Button14_Click(object sender, EventArgs e)
    {

        if (CheckBox8.Checked)
        {

            if (txtremarksportic.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox5.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox8.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label21.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblsportname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblsportdeg.Text);
            cmd.Parameters.AddWithValue("@Remark", txtremarksportic.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox5.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", txtSportID.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept();
        }
        else
        {

            if (txtremarksportic.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            string Declaration = CheckBox8.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label21.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblsportname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblsportdeg.Text);
            cmd.Parameters.AddWithValue("@Remark", txtremarksportic.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Pending");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox5.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", txtSportID.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept();
        }
    }

    protected void Button814_Click(object sender, EventArgs e)
    {

        if (CheckBox88.Checked)
        {

            if (txtremarkIT.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox858.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox88.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label821.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblITname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblITdeg.Text);
            cmd.Parameters.AddWithValue("@Remark", txtremarkIT.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox858.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", txtITID.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept();
        }
        else
        {

            if (txtremarkIT.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            string Declaration = CheckBox88.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label821.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblITname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblITdeg.Text);
            cmd.Parameters.AddWithValue("@Remark", txtremarkIT.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Pending");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox858.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", txtITID.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept();
        }
    }

    protected void Button814Security_Click(object sender, EventArgs e)
    {

        if (CheckBox88Security.Checked)
        {

            if (txtremarkSecurity.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox858Security.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox88Security.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label821Security.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblSecurityname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblSecuritydeg.Text);
            cmd.Parameters.AddWithValue("@Remark", txtremarkSecurity.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox858Security.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", txtSecurityID.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept();
        }
        else
        {

            if (txtremarkSecurity.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            string Declaration = CheckBox88Security.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label821Security.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblSecurityname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblSecuritydeg.Text);
            cmd.Parameters.AddWithValue("@Remark", txtremarkSecurity.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Pending");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox858Security.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", txtSecurityID.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept();
        }
    }

    protected void Button814Examination_Click(object sender, EventArgs e)
    {

        if (CheckBox88Examination.Checked)
        {

            if (txtremarkExamination.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox858Examination.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox88Examination.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label821Examination.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblExaminationname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblExaminationdeg.Text);
            cmd.Parameters.AddWithValue("@Remark", txtremarkExamination.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox858Examination.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", txtExaminationID.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept();
        }
        else
        {

            if (txtremarkExamination.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            string Declaration = CheckBox88Examination.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label821Examination.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblExaminationname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblExaminationdeg.Text);
            cmd.Parameters.AddWithValue("@Remark", txtremarkExamination.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Pending");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox858Examination.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", txtExaminationID.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept();
        }
    }
    protected void btn_DeptLaboratory_Click(object sender, EventArgs e)
    {

        if (CheckBox2.Checked)
        {

            if (txtremarkdepartmentLaboratory.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (txtpandingamountdeptlaboratory.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox2.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label12.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lbldeptLaboratory.Text);
            cmd.Parameters.AddWithValue("@Designation", lbldeptLaboratorydeg.Text);
            cmd.Parameters.AddWithValue("@Remark", txtremarkdepartmentLaboratory.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", txtpandingamountdeptlaboratory.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", txtidDeptLaboratory.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept();
        }
        else
        {

            if (txtremarkdepartmentLaboratory.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            string Declaration = CheckBox2.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label12.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lbldeptLaboratory.Text);
            cmd.Parameters.AddWithValue("@Designation", lbldeptLaboratorydeg.Text);
            cmd.Parameters.AddWithValue("@Remark", txtremarkdepartmentLaboratory.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Pending");
            cmd.Parameters.AddWithValue("@PendingAmount", txtpandingamountdeptlaboratory.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", txtidDeptLaboratory.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept();
        }
    }

    protected void Button8_Click(object sender, EventArgs e)
    {

        if (CheckBox4.Checked)
        {

            if (txtremarkdepartmentwork.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox1.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox4.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label15.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblcollegeworkname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblcollegeworkdeg.Text);
            cmd.Parameters.AddWithValue("@Remark", txtremarkdepartmentwork.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox1.Text);
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", txtidcollegework.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept();

        }
        else
        {

            if (txtremarkdepartmentwork.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            string Declaration = CheckBox4.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label15.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblcollegeworkname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblcollegeworkdeg.Text);
            cmd.Parameters.AddWithValue("@Remark", txtremarkdepartmentwork.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox1.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@Status", "Pending");
            cmd.Parameters.AddWithValue("@ID", txtidcollegework.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept();
        }
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        GridViewdata.Hide();
        Response.Redirect("NoDuesApprovalList.aspx");
    }

    protected void Btn_Hostel_Click(object sender, EventArgs e)
    {

        if (CheckBox5.Checked)
        {
            if (txtremarkhostel.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox2.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox5.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label16.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblhostelsname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblhosteldeg.Text);
            cmd.Parameters.AddWithValue("@Remark", txtremarkhostel.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox2.Text);
            cmd.Parameters.AddWithValue("@ID", txtidhostel.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept();
        }
        else
        {

            if (txtremarkhostel.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            string Declaration = CheckBox5.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label16.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblhostelsname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblhosteldeg.Text);
            cmd.Parameters.AddWithValue("@Remark", txtremarkhostel.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Pending");
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox2.Text);
            cmd.Parameters.AddWithValue("@ID", txtidhostel.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept();
        }
    }

    //public void ShowDept()
    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    //    {
    //        using (SqlCommand cmd = new SqlCommand("select *, [Elegible] from Tbl_StudentNoDues where [Enrollement No]='" + UserId.Text + "'", con))
    //        {
    //            con.Open();
    //            SqlDataReader dr = cmd.ExecuteReader();
    //            if (dr.HasRows)
    //            {
    //                if (dr.Read())
    //                {
    //                    string Elegible = dr["Elegible"].ToString();
    //                    con.Close();
    //                    if (Elegible == "Y" || Elegible == "N")

    //                    {
    //                        divverifydat.Visible = true;
    //                        btn_Save.Visible = false;

    //                    }
    //                    else
    //                    {
    //                        divverifydat.Visible = false;
    //                        btn_Save.Visible = true;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}


    //protected void btn_Save_Click(object sender, EventArgs e)
    //{
    //    SqlCommand cmd = new SqlCommand("Pro_UpdateNodues", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    //cmd.Parameters.AddWithValue("@Student_Number", Session["enroll"].ToString());     
    //    cmd.Parameters.AddWithValue("@EnrollementNo", UserId.Text);
    //    if (chkelegebleyesorno.Checked)
    //    {
    //        cmd.Parameters.AddWithValue("@Elegible", "Y");
    //    }
    //    else
    //    {
    //        cmd.Parameters.AddWithValue("@Elegible", "N");
    //    }

    //    if (con.State == ConnectionState.Open)
    //    { con.Close(); }
    //    con.Open();
    //    cmd.ExecuteNonQuery();
    //    con.Close();

    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' Your details have been saved successfully')", true);
    //    getnodueslist();
    //}

    protected void GriedviewStudent_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GriedviewStudent.PageIndex = e.NewPageIndex;
        getnodueslistTMMC();
    }

    public void hidePaymentbutton()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        string strSQL = "select count(*) c from  HRMSPortal.dbo.Tbl_StudentOutstanigNoDues where Status='Submit' and  [Enrollment No_] ='" + UserId.Text + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            if (Convert.ToInt32(dt.Rows[0]["c"]) >= 9)
            {
                VerifyButton();
            }
        }
    }

    public void hidePaymentbuttonTMMC()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        string strSQL = "select count(*) c from  HRMSPortal.dbo.Tbl_StudentOutstanigNoDues where Status='Submit' and  [Enrollment No_] ='" + UserId.Text + "' ";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            if (Convert.ToInt32(dt.Rows[0]["c"]) >= 22)
            {
                VerifyButtonTMMC();
            }
        }
    }


    public void VerifyButtonTMMC()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("Select * from  [EDUCOLLEGELIVE-R2].dbo.OnlinePaymentLog as P  where UserID='" + txtSection.Text + "' and OrderID like 'TMUNODUES%' and GatewayStatus=1", con))
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
                            Button30.Enabled = true;
                        }
                        else
                        {
                            Button30.Enabled = false;
                        }
                    }
                }
            }
        }
    }

    public void VerifyButton()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("Select * from  [EDUCOLLEGELIVE-R2].dbo.OnlinePaymentLog as P  where UserID='" + txtSection.Text + "' and OrderID like 'TMUNODUES%' and GatewayStatus=1", con))
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
                            Button2.Enabled = true;
                        }
                        else
                        {
                            Button2.Enabled = false;
                        }
                    }
                }
            }
        }
    }

    protected void Btn_otherfee_Click(object sender, EventArgs e)
    {


        if (CheckBox9.Checked)
        {


            if (txtotherfee.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox7.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox9.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label19.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblaccountname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblaccountdeg.Text);
            cmd.Parameters.AddWithValue("@Remark", txtotherfee.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox7.Text);
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", txtifotherfeeid.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept();
        }
        else
        {
            if (txtotherfee.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            string Declaration = CheckBox9.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label19.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblaccountname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblaccountdeg.Text);
            cmd.Parameters.AddWithValue("@Remark", txtotherfee.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox7.Text);
            cmd.Parameters.AddWithValue("@Status", "Pending");
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", txtifotherfeeid.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (CheckBox7.Checked)
        {

            if (txtelectrictydeptremark.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox4.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox7.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label22.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblelectrictydeptname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblelectrictydeptdeg.Text);
            cmd.Parameters.AddWithValue("@Remark", txtelectrictydeptremark.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox4.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@ID", txtelectrictid.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept();

        }
        else
        {
            if (txtelectrictydeptremark.Text == "")
            {
                GridViewdata.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            string Declaration = CheckBox7.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label22.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblelectrictydeptname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblelectrictydeptdeg.Text);
            cmd.Parameters.AddWithValue("@Remark", txtelectrictydeptremark.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox4.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@Status", "Pending");
            cmd.Parameters.AddWithValue("@ID", txtelectrictid.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        SqlCommand command = new SqlCommand("update  [EDUCOLLEGELIVE-R2].dbo.OnlinePaymentLog set [Temp 2]=1 where UserID='" + hdfSTNO.Value + "' and OrderID like 'TMUNODUES%' and GatewayStatus=1", con);

        command.ExecuteNonQuery();
        con.Close();
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked)
        {
            txtremarkdeptlibrary.Text = "OK";
            txtremarkdeptlibrary.Enabled = false;
            txtpendingamountdeptlibrary.Enabled = true;
        }
        else
        {
            txtremarkdeptlibrary.Enabled = true;
            txtpendingamountdeptlibrary.Enabled = true;
            txtremarkdeptlibrary.Text = "";
        }

    }
    protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox3.Checked)
        {
            txtremarkcentrallib.Text = "OK";
            txtremarkcentrallib.Enabled = false;
            txtpendingamountcentrallibrary.Enabled = true;
        }
        else
        {
            txtremarkcentrallib.Enabled = true;
            txtpendingamountcentrallibrary.Enabled = true;
            txtremarkcentrallib.Text = "";
        }
    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox2.Checked)
        {
            txtremarkdepartmentLaboratory.Text = "OK";
            txtremarkdepartmentLaboratory.Enabled = false;
            txtpandingamountdeptlaboratory.Enabled = true;
        }
        else
        {
            txtremarkdepartmentLaboratory.Enabled = true;
            txtpandingamountdeptlaboratory.Enabled = true;
            txtremarkdepartmentLaboratory.Text = "";
        }

    }
    protected void CheckBox4_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox4.Checked)
        {
            txtremarkdepartmentwork.Text = "OK";
            txtremarkdepartmentwork.Enabled = false;
            TextBox1.Enabled = true;
        }
        else
        {
            txtremarkdepartmentwork.Enabled = true;
            TextBox1.Enabled = true;
            txtremarkdepartmentwork.Text = "";
        }
    }
    protected void CheckBox5_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox5.Checked)
        {
            txtremarkhostel.Text = "OK";
            txtremarkhostel.Enabled = false;
            TextBox2.Enabled = true;
        }
        else
        {
            txtremarkhostel.Enabled = true;
            TextBox2.Enabled = true;
            txtremarkhostel.Text = "";
        }

    }

    protected void CheckBox7_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox7.Checked)
        {
            txtelectrictydeptremark.Text = "OK";
            txtelectrictydeptremark.Enabled = false;
            TextBox4.Enabled = true;
        }
        else
        {
            txtelectrictydeptremark.Enabled = true;
            TextBox4.Enabled = true;
            txtelectrictydeptremark.Text = "";
        }

    }
    protected void CheckBox8_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox8.Checked)
        {
            txtremarksportic.Text = "OK";
            txtremarksportic.Enabled = false;
            TextBox5.Enabled = true;
        }
        else
        {
            txtremarksportic.Enabled = true;
            TextBox5.Enabled = true;
            txtremarksportic.Text = "";
        }
    }

    protected void CheckBox88_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox88.Checked)
        {
            txtremarkIT.Text = "OK";
            txtremarkIT.Enabled = false;
            TextBox858.Enabled = true;
        }
        else
        {
            txtremarkIT.Enabled = true;
            TextBox858.Enabled = true;
            txtremarkIT.Text = "";
        }
    }
    protected void CheckBox88Security_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox88Security.Checked)
        {
            txtremarkSecurity.Text = "OK";
            txtremarkSecurity.Enabled = false;
            TextBox858Security.Enabled = true;
        }
        else
        {
            txtremarkSecurity.Enabled = true;
            TextBox858Security.Enabled = true;
            txtremarkSecurity.Text = "";
        }
    }
    protected void CheckBox88Examination_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox88Examination.Checked)
        {
            txtremarkExamination.Text = "OK";
            txtremarkExamination.Enabled = false;
            TextBox858Examination.Enabled = true;
        }
        else
        {
            txtremarkExamination.Enabled = true;
            TextBox858Examination.Enabled = true;
            txtremarkExamination.Text = "";
        }
    }
    protected void CheckBox9_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox9.Checked)
        {
            txtotherfee.Text = "OK";
            txtotherfee.Enabled = false;
            TextBox7.Enabled = false;
        }
        else
        {
            txtotherfee.Enabled = true;
            TextBox7.Enabled = false;
            txtotherfee.Text = "";
        }
    }



    protected void btn_Save_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("Pro_UpdateCOEStatus", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EnrollmentNo", UserId.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();

        SMS("MobileNo", "Dear Student, \r\n\r\nYour No Dues process has \r\nbeen completed. You can now download or print the No Dues Certificate\r\nthrough your ERP Portal.\r\n\r\nTMU");
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');document.location.href='NoDuesApprovalList.aspx';", true);
        GridViewdata.Show();
        alldept();
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
    public void hideSubmitbutton()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("Select [COE Status] as COEStatus from Tbl_StudentNodues where [Enrollement No]='" + UserId.Text + "'", con))
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
                            btn_Save.Visible = false;
                            Button33.Visible = false;
                        }
                        else
                        {
                            //btn_Save.Visible = true;
                        }
                    }
                }
            }
        }
    }


    public void VisibleFormCOE()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        string strSQL = "select count(*) c from  HRMSPortal.dbo.Tbl_StudentOutstanigNoDues where Status='Submit' and  [Enrollment No_] ='" + UserId.Text + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            if (Convert.ToInt32(dt.Rows[0]["c"]) >= 9 || TextBox12.Text == "0")
            {

            }
        }
    }

    public void UpdateAndDisplayAmount()
    {
        String query = "(select * from (Select (Select [Student Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Student - COLLEGE] where No_ = P.[Customer No_]) As StudentName, (Select [Enrollment No_] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Student - COLLEGE] where No_ = P.[Customer No_] ) As EnrollmentNo, (Select  CAST(ROUND(sum([Amount (LCY)]), 2) AS DECIMAL(10, 0))[Amount (LCY)] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Detailed Cust_ Ledg_ Entry] where [Cust_ Ledger Entry No_]= P.[Entry No_]) as PendingAmount, [Customer No_] as customerNo, CONVERT(VARCHAR, [Posting Date], 103) AS PostingDate, * from [EDUCOLLEGELIVE-R2].dbo.[TMU$Cust_ Ledger Entry] As P where [Customer No_] ='" + txtSection.Text + "' and[Open]=1) Y where isnull(Y.PendingAmount,0)!=0)";
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

                        //Calculate Sum and display in Footer Row
                        if (dt.Rows.Count > 0)
                        {
                            decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingAmount"));
                            if (total>0)
                            {
                                TextBox7.Text = total.ToString();
                                TextBox35.Text = total.ToString();
                            }
                            else
                            {
                                TextBox7.Text = "0";
                                TextBox35.Text = "0";
                            }
                        }
                        else
                        {
                            TextBox7.Text = "0";
                            TextBox35.Text = "0";
                        }
                    }
                }
            }
        }
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Hide();
        Response.Redirect("NoDuesApprovalList.aspx");
    }

    protected void CheckBox11_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox11.Checked)
        {
            TextBox34.Text = "OK";
            TextBox34.Enabled = false;
            TextBox35.Enabled = false;
        }
        else
        {
            TextBox34.Enabled = true;
            TextBox35.Enabled = true;
            TextBox34.Text = "";
        }
    }

    protected void CheckBox10_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox10.Checked)
        {
            TextBox29.Text = "OK";
            TextBox29.Enabled = false;
            TextBox30.Enabled = true;
        }
        else
        {
            TextBox29.Enabled = true;
            TextBox30.Enabled = true;
            TextBox29.Text = "";
        }
    }

    protected void CheckBox12_CheckedChanged(object sender, EventArgs e)
    {

        if (CheckBox12.Checked)
        {
            TextBox37.Text = "OK";
            TextBox37.Enabled = false;
            TextBox38.Enabled = true;
        }
        else
        {
            TextBox37.Enabled = true;
            TextBox38.Enabled = true;
            TextBox37.Text = "";
        }

    }

    protected void CheckBox13_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox13.Checked)
        {
            TextBox41.Text = "OK";
            TextBox41.Enabled = false;
            TextBox42.Enabled = true;
        }
        else
        {
            TextBox41.Enabled = true;
            TextBox42.Enabled = true;
            TextBox41.Text = "";
        }

    }



    protected void CheckBox15_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox15.Checked)
        {
            TextBox49.Text = "OK";
            TextBox49.Enabled = false;
            TextBox50.Enabled = true;
        }
        else
        {
            TextBox49.Enabled = true;
            TextBox50.Enabled = true;
            TextBox49.Text = "";
        }

    }

    protected void CheckBox915_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox915.Checked)
        {
            TextBox949.Text = "OK";
            TextBox949.Enabled = false;
            TextBox950.Enabled = true;
        }
        else
        {
            TextBox949.Enabled = true;
            TextBox950.Enabled = true;
            TextBox949.Text = "";
        }

    }

    protected void CheckBox16_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox16.Checked)
        {
            TextBox53.Text = "OK";
            TextBox53.Enabled = false;
            TextBox54.Enabled = true;
        }
        else
        {
            TextBox53.Enabled = true;
            TextBox54.Enabled = true;
            TextBox53.Text = "";
        }

    }

    protected void CheckBox17_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox17.Checked)
        {
            TextBox57.Text = "OK";
            TextBox57.Enabled = false;
            TextBox58.Enabled = true;
        }
        else
        {
            TextBox57.Enabled = true;
            TextBox58.Enabled = true;
            TextBox57.Text = "";
        }
    }

    protected void CheckBox18_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox18.Checked)
        {
            TextBox61.Text = "OK";
            TextBox61.Enabled = false;
            TextBox62.Enabled = true;
        }
        else
        {
            TextBox61.Enabled = true;
            TextBox62.Enabled = true;
            TextBox61.Text = "";
        }

    }

    protected void CheckBox19_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox19.Checked)
        {
            TextBox65.Text = "OK";
            TextBox65.Enabled = false;
            TextBox66.Enabled = true;
        }
        else
        {
            TextBox65.Enabled = true;
            TextBox66.Enabled = true;
            TextBox65.Text = "";
        }
    }

    protected void CheckBox20_CheckedChanged(object sender, EventArgs e)
    {

        if (CheckBox20.Checked)
        {
            TextBox69.Text = "OK";
            TextBox69.Enabled = false;
            TextBox70.Enabled = true;
        }
        else
        {
            TextBox69.Enabled = true;
            TextBox70.Enabled = true;
            TextBox69.Text = "";
        }
    }

    protected void CheckBox21_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox21.Checked)
        {
            TextBox73.Text = "OK";
            TextBox73.Enabled = false;
            TextBox74.Enabled = true;
        }
        else
        {
            TextBox73.Enabled = true;
            TextBox74.Enabled = true;
            TextBox73.Text = "";
        }
    }

    protected void CheckBox22_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox22.Checked)
        {
            TextBox77.Text = "OK";
            TextBox77.Enabled = false;
            TextBox78.Enabled = true;
        }
        else
        {
            TextBox77.Enabled = true;
            TextBox78.Enabled = true;
            TextBox77.Text = "";
        }
    }

    protected void CheckBox23_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox23.Checked)
        {
            TextBox81.Text = "OK";
            TextBox81.Enabled = false;
            TextBox82.Enabled = true;
        }
        else
        {
            TextBox81.Enabled = true;
            TextBox82.Enabled = true;
            TextBox81.Text = "";
        }
    }

    protected void CheckBox24_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox24.Checked)
        {
            TextBox85.Text = "OK";
            TextBox85.Enabled = false;
            TextBox86.Enabled = true;
        }
        else
        {
            TextBox85.Enabled = true;
            TextBox86.Enabled = true;
            TextBox85.Text = "";
        }
    }

    protected void CheckBox25_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox25.Checked)
        {
            TextBox89.Text = "OK";
            TextBox89.Enabled = false;
            TextBox90.Enabled = true;
        }
        else
        {
            TextBox89.Enabled = true;
            TextBox90.Enabled = true;
            TextBox89.Text = "";
        }
    }

    protected void CheckBox26_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox26.Checked)
        {
            TextBox93.Text = "OK";
            TextBox93.Enabled = false;
            TextBox94.Enabled = true;
        }
        else
        {
            TextBox93.Enabled = true;
            TextBox94.Enabled = true;
            TextBox93.Text = "";
        }
    }

    protected void CheckBox27_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox27.Checked)
        {
            TextBox97.Text = "OK";
            TextBox97.Enabled = false;
            TextBox98.Enabled = true;
        }
        else
        {
            TextBox97.Enabled = true;
            TextBox98.Enabled = true;
            TextBox97.Text = "";
        }
    }

    protected void CheckBox28_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox28.Checked)
        {
            TextBox101.Text = "OK";
            TextBox101.Enabled = false;
            TextBox102.Enabled = true;
        }
        else
        {
            TextBox101.Enabled = true;
            TextBox102.Enabled = true;
            TextBox101.Text = "";
        }
    }

    protected void CheckBox29_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox29.Checked)
        {
            TextBox105.Text = "OK";
            TextBox105.Enabled = false;
            TextBox106.Enabled = true;
        }
        else
        {
            TextBox105.Enabled = true;
            TextBox106.Enabled = true;
            TextBox105.Text = "";
        }
    }

    protected void CheckBox30_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox30.Checked)
        {
            TextBox109.Text = "OK";
            TextBox109.Enabled = false;
            TextBox110.Enabled = true;
        }
        else
        {
            TextBox109.Enabled = true;
            TextBox110.Enabled = true;
            TextBox109.Text = "";
        }
    }

    protected void CheckBox31_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox31.Checked)
        {
            TextBox113.Text = "OK";
            TextBox113.Enabled = false;
            TextBox114.Enabled = true;
        }
        else
        {
            TextBox113.Enabled = true;
            TextBox114.Enabled = true;
            TextBox113.Text = "";
        }
    }

    protected void Button6_Click(object sender, EventArgs e)
    {

        if (CheckBox10.Checked)
        {
            if (TextBox29.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox30.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox10.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label27.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", Label32.Text);
            cmd.Parameters.AddWithValue("@Designation", Label33.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox29.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox30.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox31.Text);

            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
        else
        {

            if (TextBox29.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }

            string Declaration = CheckBox10.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label27.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", Label32.Text);
            cmd.Parameters.AddWithValue("@Designation", Label33.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox29.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox30.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox31.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
    }

    protected void Button9_Click(object sender, EventArgs e)
    {
        if (CheckBox12.Checked)
        {
            if (TextBox37.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox38.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox12.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label37.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblHostalname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblHostalDesignation.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox37.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox38.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox39.Text);

            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
        else
        {

            if (TextBox37.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }

            string Declaration = CheckBox12.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label37.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblHostalname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblHostalDesignation.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox37.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox38.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox39.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }

    }

    protected void Button10_Click(object sender, EventArgs e)
    {
        if (CheckBox13.Checked)
        {
            if (TextBox41.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox42.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox13.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label38.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", Label40.Text);
            cmd.Parameters.AddWithValue("@Designation", Label41.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox41.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox42.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox43.Text);

            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
        else
        {

            if (TextBox41.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }

            string Declaration = CheckBox13.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label38.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", Label40.Text);
            cmd.Parameters.AddWithValue("@Designation", Label41.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox41.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox42.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox43.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
    }

    protected void Button7_Click(object sender, EventArgs e)
    {
        if (CheckBox11.Checked)
        {
            if (TextBox34.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox35.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox11.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label34.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", Label35.Text);
            cmd.Parameters.AddWithValue("@Designation", Label36.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox34.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox35.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox36.Text);

            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            //GridViewdata.Show();
            alldept1();
        }
        else
        {

            if (TextBox34.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }

            string Declaration = CheckBox11.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label34.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", Label35.Text);
            cmd.Parameters.AddWithValue("@Designation", Label36.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox34.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox35.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox36.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            //GridViewdata.Show();
            alldept1();
        }
    }

    protected void Button12_Click(object sender, EventArgs e)
    {
        if (CheckBox15.Checked)
        {
            if (TextBox49.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox50.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox15.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label46.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblsportsname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblsportsdesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox49.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox50.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox51.Text);

            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
        else
        {

            if (TextBox49.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }

            string Declaration = CheckBox15.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label46.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblsportsname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblsportsdesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox49.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox50.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox51.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
    }
    protected void Button912_Click(object sender, EventArgs e)
    {
        if (CheckBox915.Checked)
        {
            if (TextBox949.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox950.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox915.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label946.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblITsname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblITsdesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox949.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox950.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox951.Text);

            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
        else
        {

            if (TextBox49.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }

            string Declaration = CheckBox15.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label946.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblITsname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblITsdesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox949.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox950.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox951.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
    }

    protected void Button13_Click(object sender, EventArgs e)
    {
        if (CheckBox16.Checked)
        {
            if (TextBox53.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox54.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox16.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label47.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblCommunityMedicinename.Text);
            cmd.Parameters.AddWithValue("@Designation", lblCommunityMedicinedesig.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox53.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox54.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox55.Text);

            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
        else
        {

            if (TextBox53.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }

            string Declaration = CheckBox16.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label47.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblCommunityMedicinename.Text);
            cmd.Parameters.AddWithValue("@Designation", lblCommunityMedicinedesig.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox53.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox54.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox55.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }

    }

    protected void Button15_Click(object sender, EventArgs e)
    {
        if (CheckBox17.Checked)
        {
            if (TextBox57.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox58.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox17.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label48.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblGeneralMedicineName.Text);
            cmd.Parameters.AddWithValue("@Designation", lblGeneralMedicinedegis.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox57.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox58.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox59.Text);

            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
        else
        {

            if (TextBox57.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }

            string Declaration = CheckBox17.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label48.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblGeneralMedicineName.Text);
            cmd.Parameters.AddWithValue("@Designation", lblGeneralMedicinedegis.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox57.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox58.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox59.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }

    }

    protected void Button16_Click(object sender, EventArgs e)
    {
        if (CheckBox18.Checked)
        {
            if (TextBox61.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox62.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox18.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label49.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblPsychiatryname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblPsychiatrydesig.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox61.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox62.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox63.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
        else
        {

            if (TextBox57.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }

            string Declaration = CheckBox18.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label49.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblPsychiatryname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblPsychiatrydesig.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox61.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox62.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox63.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }

    }


    protected void Button17_Click(object sender, EventArgs e)
    {
        if (CheckBox19.Checked)
        {
            if (TextBox65.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox66.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox19.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label50.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblGeneralSurgeryname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblGeneralSurgerydesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox65.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox66.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox67.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
        else
        {

            if (TextBox66.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }

            string Declaration = CheckBox19.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label50.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblGeneralSurgeryname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblGeneralSurgerydesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox65.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox66.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox67.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
    }

    protected void Button18_Click(object sender, EventArgs e)
    {
        if (CheckBox20.Checked)
        {
            if (TextBox69.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox70.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox20.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label51.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblAnesthisiaName.Text);
            cmd.Parameters.AddWithValue("@Designation", lblAnesthisiadesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox69.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox70.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox71.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
        else
        {

            if (TextBox69.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }

            string Declaration = CheckBox20.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label51.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblAnesthisiaName.Text);
            cmd.Parameters.AddWithValue("@Designation", lblAnesthisiadesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox69.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox70.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox71.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
    }

    protected void Button19_Click(object sender, EventArgs e)
    {
        if (CheckBox21.Checked)
        {
            if (TextBox73.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox74.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox21.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label52.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblObsGyaneName.Text);
            cmd.Parameters.AddWithValue("@Designation", lblObsGyanedesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox73.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox74.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox75.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
        else
        {

            if (TextBox73.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }

            string Declaration = CheckBox21.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label52.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblObsGyaneName.Text);
            cmd.Parameters.AddWithValue("@Designation", lblObsGyanedesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox73.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox74.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox75.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
    }

    protected void Button20_Click(object sender, EventArgs e)
    {
        if (CheckBox22.Checked)
        {
            if (TextBox77.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox78.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox22.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label53.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblPediatricsname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblPediatricsdesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox77.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox78.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox79.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
        else
        {

            if (TextBox77.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }

            string Declaration = CheckBox22.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label53.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblPediatricsname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblPediatricsdesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox77.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox78.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox79.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }

    }

    protected void Button21_Click(object sender, EventArgs e)
    {
        if (CheckBox23.Checked)
        {
            if (TextBox81.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox82.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox23.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label54.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblOrthopedicsname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblOrthopedicsdesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox81.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox82.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox83.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
        else
        {

            if (TextBox81.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }

            string Declaration = CheckBox23.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label54.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblOrthopedicsname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblOrthopedicsdesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox81.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox82.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox83.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }

    }


    protected void Button22_Click(object sender, EventArgs e)
    {
        if (CheckBox24.Checked)
        {
            if (TextBox85.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox86.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox24.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label55.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblentname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblentdesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox85.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox86.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox87.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
        else
        {

            if (TextBox85.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }

            string Declaration = CheckBox24.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label55.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblentname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblentdesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox85.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox86.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox87.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }

    }

    protected void Button23_Click(object sender, EventArgs e)
    {
        if (CheckBox25.Checked)
        {
            if (TextBox89.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox90.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox25.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label56.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblOphthalmologyname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblOphthalmologydesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox89.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox90.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox91.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
        else
        {

            if (TextBox89.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }

            string Declaration = CheckBox25.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label56.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblOphthalmologyname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblOphthalmologydesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox89.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox90.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox91.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }

    }

    protected void Button24_Click(object sender, EventArgs e)
    {
        if (CheckBox26.Checked)
        {
            if (TextBox93.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox94.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox26.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label57.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblCasualtyname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblCasualtydesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox93.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox94.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox95.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
        else
        {

            if (TextBox93.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }

            string Declaration = CheckBox26.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label57.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblCasualtyname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblCasualtydesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox93.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox94.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox95.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }

    }

    protected void Button25_Click(object sender, EventArgs e)
    {
        if (CheckBox27.Checked)
        {
            if (TextBox97.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox98.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox27.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label58.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblDermatologyname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblDermatologydesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox97.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox98.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox99.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
        else
        {

            if (TextBox97.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }

            string Declaration = CheckBox27.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label58.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblDermatologyname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblDermatologydesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox97.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox98.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox99.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
    }

    protected void Button26_Click(object sender, EventArgs e)
    {
        if (CheckBox28.Checked)
        {
            if (TextBox101.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox102.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox28.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label59.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lbltbchestname.Text);
            cmd.Parameters.AddWithValue("@Designation", lbltbchestdesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox101.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox102.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox103.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
        else
        {

            if (TextBox101.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }

            string Declaration = CheckBox28.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label59.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lbltbchestname.Text);
            cmd.Parameters.AddWithValue("@Designation", lbltbchestdesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox101.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox102.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox103.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
    }

    protected void Button27_Click(object sender, EventArgs e)
    {
        if (CheckBox29.Checked)
        {
            if (TextBox105.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox106.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox29.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label60.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblRadiologyname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblRadiologydesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox105.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox106.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox107.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
        else
        {

            if (TextBox105.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }

            string Declaration = CheckBox29.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label60.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblRadiologyname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblRadiologydesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox105.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox106.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox107.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
    }

    protected void Button28_Click(object sender, EventArgs e)
    {
        if (CheckBox30.Checked)
        {
            if (TextBox109.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox110.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox30.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label61.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblPathlogyname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblPathlogydesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox109.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox110.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox111.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
        else
        {

            if (TextBox109.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }

            string Declaration = CheckBox30.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label61.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblPathlogyname.Text);
            cmd.Parameters.AddWithValue("@Designation", lblPathlogydesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox109.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox110.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox111.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
    }

    protected void Button29_Click(object sender, EventArgs e)
    {
        if (CheckBox31.Checked)
        {
            if (TextBox113.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            if (TextBox114.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Pending Amount')", true);
                return;
            }
            string Declaration = CheckBox31.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label62.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "NoDues");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblForensicMedicinename.Text);
            cmd.Parameters.AddWithValue("@Designation", lblForensicMedicinedesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox113.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox114.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox115.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }
        else
        {
            if (TextBox113.Text == "")
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
                return;
            }
            string Declaration = CheckBox31.Checked ? "Y" : "N";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOutstandingNoDues", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Particular", Label62.Text);
            cmd.Parameters.AddWithValue("@StatusofDues", "Pending");
            cmd.Parameters.AddWithValue("@DeptEmployeeName", lblForensicMedicinename.Text);
            cmd.Parameters.AddWithValue("@Designation", lblForensicMedicinedesi.Text);
            cmd.Parameters.AddWithValue("@Remark", TextBox113.Text);
            cmd.Parameters.AddWithValue("@EnrollmentNo_", UserId.Text);
            cmd.Parameters.AddWithValue("@StudentName", txtstudentName.Text);
            cmd.Parameters.AddWithValue("@DeptEmployeeCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Submit");
            cmd.Parameters.AddWithValue("@PendingAmount", TextBox114.Text);
            cmd.Parameters.AddWithValue("@CheckNoDuesStatus", Declaration);
            cmd.Parameters.AddWithValue("@ID", TextBox115.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');", true);
            GridViewdata.Show();
            alldept1();
        }

    }

    protected void Button30_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        SqlCommand command = new SqlCommand("update  [EDUCOLLEGELIVE-R2].dbo.OnlinePaymentLog set [Temp 2]=1 where UserID='" + hdfSTNO.Value + "' and OrderID like 'TMUNODUES%' and GatewayStatus=1", con);

        command.ExecuteNonQuery();
        con.Close();
    }

    protected void Button31_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("Pro_UpdatePrincipalStatus", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EnrollmentNo", UserId.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Approved Successfully.');document.location.href='NoDuesApprovalList.aspx';", true);
        ModalPopupExtender1.Show();
        alldept1();
    }

    protected void Button32_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("Pro_UpdatePrincipalStatusReject", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EnrollmentNo", UserId.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Reject Successfully.');document.location.href='NoDuesApprovalList.aspx';", true);
        ModalPopupExtender1.Show();
        alldept1();
    }
    public void hideSubmitbuttonPrincipal()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("Select [Principal Approval Status] as PrincipalStatus from Tbl_StudentNodues where [Enrollement No]='" + UserId.Text + "'", con))
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        string PrincipalStatus = dr["PrincipalStatus"].ToString();
                        con.Close();
                        if (PrincipalStatus == "Submit")

                        {
                            Button31.Visible = false;
                            Button32.Visible = false;
                        }
                        else
                        {
                            Button31.Visible = true;
                            Button32.Visible = true;
                        }
                    }
                }
            }
        }
    }

    protected void Button33_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("Pro_UpdateCOEStatus", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EnrollmentNo", UserId.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Saved Successfully.');document.location.href='NoDuesApprovalList.aspx';", true);
        ModalPopupExtender1.Show();
        alldept1();
    }
}








