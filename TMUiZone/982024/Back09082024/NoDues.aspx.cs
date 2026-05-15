using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_NoDues : System.Web.UI.Page
{
    private string pk;

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {
                resignationdata();
                binddata();
                getdeptlibrary();
                FORCOLLEGE();
                //resignationdata();
                showhr();
                hrdept();
                showfinance();
                submitfrm();
                alldept();
                showNotable();
                //hidehr();
                //if(lblpayrollname.Text=="")
                //{
                //    lblpayrollname.Text = "SANDEEP GUPTA";
                //    lblpayrolldeg.Text= "SENIOR MANAGER";
                    
                //}
               

            }
        }

        catch
        {
            Response.Redirect("../Default.aspx");
        }

    }


    public void binddata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        string strSQL = "select No_,[First Name],[Father Name],[HOD Name],[HOD],[Global Dimension 1 Code],[Leave Date],[Job Title_Grade Desc],[Mobile Phone No_],[E-Mail],[Branch Name],Address,[Employment Date] from TMU$Employee where [No_]='" + Session["uid"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        txtemployeecode.Text = dt.Rows[0]["No_"].ToString();
        txtnameofemployee.Text = dt.Rows[0]["First Name"].ToString();
        Label33.Text = dt.Rows[0]["First Name"].ToString();
        Label50.Text = dt.Rows[0]["First Name"].ToString();
        //Label72.Text = dt.Rows[0]["First Name"].ToString();
        Label88.Text = dt.Rows[0]["First Name"].ToString();
        Label62.Text = dt.Rows[0]["First Name"].ToString();
        //Label65.Text = dt.Rows[0]["First Name"].ToString();
        //Label73.Text = dt.Rows[0]["Job Title_Grade Desc"].ToString();
        Label35.Text = dt.Rows[0]["Job Title_Grade Desc"].ToString();
        txtfathername.Text = dt.Rows[0]["Father Name"].ToString();
        txtcollegedeptsection.Text = dt.Rows[0]["Global Dimension 1 Code"].ToString();
        //Label74.Text = dt.Rows[0]["Global Dimension 1 Code"].ToString();
        Label39.Text = dt.Rows[0]["Global Dimension 1 Code"].ToString();
        txtbranch.Text = dt.Rows[0]["Branch Name"].ToString();
        txtdateofjoining.Text = dt.Rows[0]["Employment Date"].ToString();
        Label37.Text = dt.Rows[0]["Employment Date"].ToString();
       // txtdateofleaving.Text = dt.Rows[0]["Leave Date"].ToString();
        //txtperaddress.Text = dt.Rows[0]["Address"].ToString();
        //txtmailingaddress.Text = dt.Rows[0]["Address"].ToString();
        txtmobileno.Text = dt.Rows[0]["Mobile Phone No_"].ToString();
        txtemailid.Text = dt.Rows[0]["E-Mail"].ToString();
       // Label73.Text = dt.Rows[0]["Job Title_Grade Desc"].ToString();
        Label35.Text = dt.Rows[0]["Job Title_Grade Desc"].ToString();
       // Label74.Text = dt.Rows[0]["Global Dimension 1 Code"].ToString();
        Label37.Text = dt.Rows[0]["Employment Date"].ToString();
        Label39.Text = dt.Rows[0]["Global Dimension 1 Code"].ToString();
        //lbldeptstorename.Text = dt.Rows[0]["HOD Name"].ToString();
        //lblcollegedeptdeg.Text= dt.Rows[0]["Job Title_Grade Desc"].ToString();
        showNotable();
    }
    public void showNotable()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "select * from Tbl_NoDuesDataTable where [Employee_Code]='" + txtemployeecode.Text + "'";
        SqlDataAdapter da1 = new SqlDataAdapter(strSQL, con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        con.Close();
        if (dt1.Rows.Count > 0)
        {
            txtIssuedon.Text = dt1.Rows[0]["Issued_On"].ToString();
            txtdateofrelieving.Text = dt1.Rows[0]["Proposed_Date_of_Relieving"].ToString();
            txtdeputyRegistrar.Text = dt1.Rows[0]["Deputy_Registrar1"].ToString();
            TextBox89.Text = dt1.Rows[0]["Amount1_Finance"].ToString();
            TextBox1.Text = dt1.Rows[0]["Amount2_Finance"].ToString();
            TextBox2.Text = dt1.Rows[0]["Cheque_No_Finance"].ToString();
            TextBox3.Text = dt1.Rows[0]["Date1_Finance"].ToString();
            TextBox4.Text = dt1.Rows[0]["DrawnonFinance"].ToString();
            TextBox6.Text = dt1.Rows[0]["Amount3_Finance"].ToString();
            TextBox8.Text = dt1.Rows[0]["Amount4_Finance"].ToString();
            txtfinance.Text = dt1.Rows[0]["Date2_Finance"].ToString();
            TextBox7.Text = dt1.Rows[0]["Signature_Finance"].ToString();
            TextBox9.Text = dt1.Rows[0]["Name_Finance"].ToString();
            TextBox10.Text = dt1.Rows[0]["Designation_Finance"].ToString();
            lbldeptstoredeg.Text = dt1.Rows[0]["Dir_Pri_Head_Designation"].ToString();
            lblcollegeworkdeg.Text = dt1.Rows[0]["Dir_Pri_Head_Designation"].ToString();
            lblcollegedeptdeg.Text = dt1.Rows[0]["Dir_Pri_Head_Designation"].ToString();
            lbldepartmentdeg.Text = dt1.Rows[0]["Dir_Pri_Head_Designation"].ToString();
            //TextBox11.Text = dt1.Rows[0]["Receive_Amount_Employee"].ToString();
            //TextBox12.Text = dt1.Rows[0]["Cheque_No_Employee"].ToString();
            //TextBox14.Text = dt1.Rows[0]["Drawn_On_Employee"].ToString();
            //TextBox15.Text = dt1.Rows[0]["Amount2_Employee"].ToString();
            //txtsigemployee.Text = dt1.Rows[0]["Signature_Employee"].ToString();
            //txtdesignation.Text = dt1.Rows[0]["Designation_Employee"].ToString();
            //txtdateemployee.Text = dt1.Rows[0]["Date2_Employee"].ToString();
            //TextBox13.Text = dt1.Rows[0]["Date1_Employee"].ToString();

        }

    }
    public void resignationdata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "select  *,(Select Date_Of_Leaving from Tbl_NoDuesDataTable where [Employee_Code]='" + Session["uid"].ToString() + "') 'Date Of leaving' from tble_Exit_Interview_Form where [Employee Code]='" + Session["uid"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();

        if (dt.Rows.Count > 0)
        {
            txtdateofleaving.Text = dt.Rows[0]["Date Of leaving"].ToString();
            txtdateofrelieving.Text= dt.Rows[0]["Date Of Resignation"].ToString();
        }

    }
    public void getdeptlibrary()
    {

        //if (Session["uid"].ToString() == "TMU04426")
        //{
        //    DropDownList1.Enabled = true;
        //    txtremarkdeptlibrary.Enabled = true;
        //    btn_submit1.Enabled = true;
        //    DropDownList2.Enabled = true;
        //    txtremarkcentrallib.Enabled = true;
        //    Button2.Enabled = true;

        //}

        //if (Session["uid"].ToString() == "TMU06070")
        //{
        //    DropDownList9.Enabled = true;
        //    txtremarkhostelmess.Enabled = true;
        //    Button9.Enabled = true;

        //}
        //if (Session["uid"].ToString() == "TMU00202")
        //{
        //    DropDownList10.Enabled = true;
        //    txtremarkhosteloffice.Enabled = true;
        //    Button10.Enabled = true;

        //}
        //if (Session["uid"].ToString() == "TMU00211")
        //{
        //    DropDownList11.Enabled = true;
        //    txtremarktransportoffice.Enabled = true;
        //    Button11.Enabled = true;

        //}
        //if (Session["uid"].ToString() == "TMU00035")
        //{
        //    DropDownList12.Enabled = true;
        //    txtremarkguesthouseic.Enabled = true;
        //    Button12.Enabled = true;

        //}
        //if (Session["uid"].ToString() == "TMU00035")
        //{
        //    DropDownList13.Enabled = true;
        //    txtremarkfacultyhose.Enabled = true;
        //    Button13.Enabled = true;

        //}
        //if (Session["uid"].ToString() == "TMU00784")
        //{
        //    DropDownList14.Enabled = true;
        //    txtremarksportic.Enabled = true;
        //    Button14.Enabled = true;

        //}
        //if (Session["uid"].ToString() == "TMU00864")
        //{
        //    DropDownList15.Enabled = true;
        //    txtremarkmedicalstore.Enabled = true;
        //    Button15.Enabled = true;

        //}
        //if (Session["uid"].ToString() == "TMU00381")
        //{
        //    DropDownList16.Enabled = true;
        //    txtremarkelectrictydepart.Enabled = true;
        //    Button16.Enabled = true;

        //}
        //if (Session["uid"].ToString() == "TMU06286")
        //{
        //    DropDownList17.Enabled = true;
        //    txtremarkicardoffice.Enabled = true;
        //    Button17.Enabled = true;

        //}
        //if (Session["uid"].ToString() == "TMU01979")
        //{
        //    DropDownList18.Enabled = true;
        //    txtremarkit.Enabled = true;
        //    Button18.Enabled = true;

        //}
        //if (Session["uid"].ToString() == "TMU00084")
        //{
        //    DropDownList20.Enabled = true;
        //    txtremarkcashcounter.Enabled = true;
        //    Button20.Enabled = true;

        //}
        //if (Session["uid"].ToString() == "TMU00530")
        //{
        //    DropDownList21.Enabled = true;
        //    txtremarkjtdirector.Enabled = true;
        //    Button21.Enabled = true;

        //}
        //if (Session["uid"].ToString() == "TMU00075")
        //{
        //    DropDownList22.Enabled = true;
        //    txtremarkpayrollsection.Enabled = true;
        //    Button22.Enabled = true;

        //}
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("sp_getlibrarydept1", con);
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        if (dtCL.Rows.Count > 0)
        {
            lbldepartmentlib.Text = dtCL.Select("Particulars='Department Library'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
            lbldepartmentlibdeg.Text = dtCL.Select("Particulars='Department Library'").CopyToDataTable().Rows[0]["Designation"].ToString();
            lblcentlibname.Text = dtCL.Select("Particulars='Central Library'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
            lblcentlibdeg.Text = dtCL.Select("Particulars='Central Library'").CopyToDataTable().Rows[0]["Designation"].ToString();
            //lblbookbankname.Text = dtCL.Select("Particulars='Book Bank'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
            //lblbookbankdeg.Text = dtCL.Select("Particulars='Book Bank'").CopyToDataTable().Rows[0]["Designation"].ToString();
            lblseedmoneyname.Text = dtCL.Select("Particulars='Seed Money'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
            lblseedmoneydeg.Text = dtCL.Select("Particulars='Seed Money'").CopyToDataTable().Rows[0]["Designation"].ToString();
            lbldeptstorename.Text = dtCL.Select("Particulars='College/Department Store'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
            lbldeptstoredeg.Text = dtCL.Select("Particulars='College/Department Store'").CopyToDataTable().Rows[0]["Designation"].ToString();
            lblcentralstorename.Text = dtCL.Select("Particulars='Central Store'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
            lblcentralstoredeg.Text = dtCL.Select("Particulars='Central Store'").CopyToDataTable().Rows[0]["Designation"].ToString();
            lblcollegedeptname.Text = dtCL.Select("Particulars='College/Department Laboratory'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
            lblcollegedeptdeg.Text = dtCL.Select("Particulars='College/Department Laboratory'").CopyToDataTable().Rows[0]["Designation"].ToString();
            lblcollegeworkname.Text = dtCL.Select("Particulars='College/Department Wprkshop'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
            lblcollegeworkdeg.Text = dtCL.Select("Particulars='College/Department Wprkshop'").CopyToDataTable().Rows[0]["Designation"].ToString();
            lblhostelmessname.Text = dtCL.Select("Particulars='Hostel Mess'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
            lblhostelmessdeg.Text = dtCL.Select("Particulars='Hostel Mess'").CopyToDataTable().Rows[0]["Designation"].ToString();
            lblhosteloffficename.Text = dtCL.Select("Particulars='Hostel Office'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
            lblhosteloffficedeg.Text = dtCL.Select("Particulars='Hostel Office'").CopyToDataTable().Rows[0]["Designation"].ToString();
            lbltransportofficename.Text = dtCL.Select("Particulars='Transport Office'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
            lbltransportofficedeg.Text = dtCL.Select("Particulars='Transport Office'").CopyToDataTable().Rows[0]["Designation"].ToString();
            lblguestname.Text = dtCL.Select("Particulars='Guest House I/c'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
            lblguestdeg.Text = dtCL.Select("Particulars='Guest House I/c'").CopyToDataTable().Rows[0]["Designation"].ToString();
            lblfacultyname.Text = dtCL.Select("Particulars='Faculty House I/c'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
            lblfacultydeg.Text = dtCL.Select("Particulars='Faculty House I/c'").CopyToDataTable().Rows[0]["Designation"].ToString();
            lblsportname.Text = dtCL.Select("Particulars='Sport I/c'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
            lblsportdeg.Text = dtCL.Select("Particulars='Sport I/c'").CopyToDataTable().Rows[0]["Designation"].ToString();
            lblmedicalname.Text = dtCL.Select("Particulars='Medical Store'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
            lblmedicaldeg.Text = dtCL.Select("Particulars='Medical Store'").CopyToDataTable().Rows[0]["Designation"].ToString();
            lblelectricityname.Text = dtCL.Select("Particulars='Electricity Department'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
            lblelectricitydeg.Text = dtCL.Select("Particulars='Electricity Department'").CopyToDataTable().Rows[0]["Designation"].ToString();
            lblicardofficename.Text = dtCL.Select("Particulars='I/Card Office'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
            lbliccardofficedeg.Text = dtCL.Select("Particulars='I/Card Office'").CopyToDataTable().Rows[0]["Designation"].ToString();
            lblitname.Text = dtCL.Select("Particulars='Computer Center/IT Dept.'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
            lblitdeg.Text = dtCL.Select("Particulars='Computer Center/IT Dept.'").CopyToDataTable().Rows[0]["Designation"].ToString();
            lbldepartmenthname.Text = dtCL.Select("Particulars='Department Head'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
            lbldepartmentdeg.Text = dtCL.Select("Particulars='Department Head'").CopyToDataTable().Rows[0]["Designation"].ToString();
            lblcashname.Text = dtCL.Select("Particulars='Cash Counter'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
            lblcashdeg.Text = dtCL.Select("Particulars='Cash Counter'").CopyToDataTable().Rows[0]["Designation"].ToString();
            lbljtdirename.Text = dtCL.Select("Particulars='Jt. Director(Security)'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
            lbljtdirdeg.Text = dtCL.Select("Particulars='Jt. Director(Security)'").CopyToDataTable().Rows[0]["Designation"].ToString();
            //lblpayrollname.Text = dtCL.Select("Particulars='Payroll Section'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
            //lblpayrolldeg.Text = dtCL.Select("Particulars='Payroll Section'").CopyToDataTable().Rows[0]["Designation"].ToString();
            lblphddepartmentname.Text = dtCL.Select("Particulars='Ph.D DEPARTMENT'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
            lblphddepartmentdesignation.Text = dtCL.Select("Particulars='Ph.D DEPARTMENT'").CopyToDataTable().Rows[0]["Designation"].ToString();
            //lblpharmacydesignation.Text = dtCL.Select("Particulars='Pharmacy'").CopyToDataTable().Rows[0]["Designation"].ToString();
            //lblpharmacyname.Text = dtCL.Select("Particulars='Pharmacy'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();

        }
    }



    protected void btnsave_Click(object sender, EventArgs e)
    {
        submitfrm();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("proc_InsertNoDuesData", con);
        cmd.CommandType = CommandType.StoredProcedure;   
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Issued_On", txtIssuedon.Text);
        cmd.Parameters.AddWithValue("@Proposed_Date_of_Relieving", txtdateofrelieving.Text);
        cmd.Parameters.AddWithValue("@Deputy_Registrar1",txtdeputyRegistrar.Text);
        cmd.Parameters.AddWithValue("@Name_Of_Employee", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Father_Name", txtfathername.Text);
        cmd.Parameters.AddWithValue("@College_Department_Section", txtcollegedeptsection.Text);
        cmd.Parameters.AddWithValue("@Branch", txtbranch.Text);
        cmd.Parameters.AddWithValue("@Date_Of_Joining", txtdateofjoining.Text);
        cmd.Parameters.AddWithValue("@Date_Of_Leaving", txtdateofleaving.Text);
        //cmd.Parameters.AddWithValue("@Permanent_Address", txtperaddress.Text);
        //cmd.Parameters.AddWithValue("@Mailing_Address", txtmailingaddress.Text);
        cmd.Parameters.AddWithValue("@Mobile_No", txtmobileno.Text);
        cmd.Parameters.AddWithValue("@E_mail_Id", txtemailid.Text);
        cmd.Parameters.AddWithValue("@Dir_Pri_Head_Name", txtdirectorprinciname.Text);
        cmd.Parameters.AddWithValue("@Dir_Pri_Head_Designation", txtdirectorprincipalheaddeg.Text);
        cmd.Parameters.AddWithValue("@Dir_Pri_Head_College_Department_Section", txtdirectorprincipalheadcollegedept.Text);
        cmd.Parameters.AddWithValue("@Dir_Pri_Head_Date", txtdatedirectorprincipaldate.Text);
        //cmd.Parameters.AddWithValue("@Date2_Employee", txtdateemployee.Text);
        //cmd.Parameters.AddWithValue("@Signature_Employee ", txtsigemployee.Text);
        //cmd.Parameters.AddWithValue("@Designation_Employee", txtdesignation.Text);
        cmd.Parameters.AddWithValue("@Status", "Submit");
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Update successfully.'); document.location.href='NoDues.aspx';", true);
    }
    protected void btn_submit1_Click(object sender, EventArgs e)
    {
        if (txtremarkdeptlibrary.Text == "")
        {
            string message1 = "Please Fill Remark.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (DropDownList1.SelectedItem.Text == "Select")
        {
            string message1 = "Please Fill No Dues";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Particulars", LabelR1.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList1.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lbldepartmentlib.Text);
        cmd.Parameters.AddWithValue("@Designation", lbldepartmentlibdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkdeptlibrary.Text);



        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "Your details have been saved successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        DropDownList1.SelectedItem.Text = "Select";
        txtremarkdeptlibrary.Text = "";
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (txtremarkcentrallib.Text == "")
        {
            string message1 = "Please Fill Remark.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (DropDownList2.SelectedItem.Text == "Select")
        {
            string message1 = "Please Fill No Dues";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Particulars", Label5.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList2.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblcentlibname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblcentlibdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkcentrallib.Text);

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "Your details have been saved successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        DropDownList2.SelectedItem.Text = "Select";
        txtremarkcentrallib.Text = "";
    }

    //protected void Button3_Click(object sender, EventArgs e)
    //{

    //    if (txtremarkbookbank.Text == "")
    //    {
    //        string message1 = "Please Fill Remark.";
    //        string script1 = "window.onload = function(){ alert('";
    //        script1 += message1;
    //        script1 += "')};";
    //        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
    //        return;
    //    }
    //    if (DropDownList3.SelectedItem.Text == "Select")
    //    {
    //        string message1 = "Please Fill No Dues";
    //        string script1 = "window.onload = function(){ alert('";
    //        script1 += message1;
    //        script1 += "')};";
    //        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
    //        return;
    //    }
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    //    SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@Particulars", Label6.Text);
    //    cmd.Parameters.AddWithValue("@No_Dues", DropDownList3.SelectedItem.Text);
    //    cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblbookbankname.Text);
    //    cmd.Parameters.AddWithValue("@Designation", lblbookbankdeg.Text);
    //    cmd.Parameters.AddWithValue("@Remark", txtremarkbookbank.Text);
    //    if (con.State == ConnectionState.Open)
    //    { con.Close(); }
    //    con.Open();
    //    cmd.ExecuteNonQuery();
    //    con.Close();
    //    string message = "Your details have been saved successfully.";
    //    string script = "window.onload = function(){ alert('";
    //    script += message;
    //    script += "')};";
    //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
    //    DropDownList3.SelectedItem.Text = "Select";
    //    txtremarkbookbank.Text = "";
    //}

    protected void Button4_Click(object sender, EventArgs e)
    {

        if (txtremarkseedmoney.Text == "")
        {
            string message1 = "Please Fill Remark.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (DropDownList4.SelectedItem.Text == "Select")
        {
            string message1 = "Please Fill No Dues";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Particulars", Label11.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList4.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblseedmoneyname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblseedmoneydeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkseedmoney.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "Your details have been saved successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        DropDownList4.SelectedItem.Text = "Select";
        txtremarkseedmoney.Text = "";
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        if (txtremarkdepartmentstore.Text == "")
        {
            string message1 = "Please Fill Remark.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (DropDownList5.SelectedItem.Text == "Select")
        {
            string message1 = "Please Fill No Dues";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Particulars", Label12.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList5.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lbldeptstorename.Text);
        cmd.Parameters.AddWithValue("@Designation", lbldeptstoredeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkdepartmentstore.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "Your details have been saved successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        DropDownList5.SelectedItem.Text = "Select";
        txtremarkdepartmentstore.Text = "";
    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        if (txtremarkcentralstore.Text == "")
        {
            string message1 = "Please Fill Remark.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (DropDownList6.SelectedItem.Text == "Select")
        {
            string message1 = "Please Fill No Dues";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Particulars", Label13.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList6.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblcentralstorename.Text);
        cmd.Parameters.AddWithValue("@Designation", lblcentralstoredeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkcentralstore.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "Your details have been saved successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        DropDownList6.SelectedItem.Text = "Select";
        txtremarkcentralstore.Text = "";
    }

    protected void txtremarkdepartmentlaborat_Click(object sender, EventArgs e)
    {
        if (txtremarkcollegedeptre.Text == "")
        {
            string message1 = "Please Fill Remark.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (DropDownList7.SelectedItem.Text == "Select")
        {
            string message1 = "Please Fill No Dues";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Particulars", Label14.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList7.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblcollegedeptname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblcollegedeptdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkcollegedeptre.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "Your details have been saved successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        DropDownList7.SelectedItem.Text = "Select";
        txtremarkcollegedeptre.Text = "";
    }

    protected void Button8_Click(object sender, EventArgs e)
    {
        if (txtremarkdepartmentwork.Text == "")
        {
            string message1 = "Please Fill Remark.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (DropDownList8.SelectedItem.Text == "Select")
        {
            string message1 = "Please Fill No Dues";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Particulars", Label15.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList8.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblcollegeworkname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblcollegeworkdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkdepartmentwork.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "Your details have been saved successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        DropDownList8.SelectedItem.Text = "Select";
        txtremarkdepartmentwork.Text = "";
    }

    protected void Button9_Click(object sender, EventArgs e)
    {
        if (txtremarkhostelmess.Text == "")
        {
            string message1 = "Please Fill Remark.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (DropDownList9.SelectedItem.Text == "Select")
        {
            string message1 = "Please Fill No Dues";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Particulars", Label16.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList9.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblhostelmessname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblhostelmessdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkhostelmess.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "Your details have been saved successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        DropDownList9.SelectedItem.Text = "Select";
        txtremarkhostelmess.Text = "";
    }

    protected void Button10_Click(object sender, EventArgs e)
    {
        if (txtremarkhosteloffice.Text == "")
        {
            string message1 = "Please Fill Remark.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (DropDownList10.SelectedItem.Text == "Select")
        {
            string message1 = "Please Fill No Dues";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Particulars", Label17.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList10.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblhosteloffficename.Text);
        cmd.Parameters.AddWithValue("@Designation", lblhosteloffficedeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkhosteloffice.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "Your details have been saved successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        DropDownList10.SelectedItem.Text = "Select";
        txtremarkhosteloffice.Text = "";
    }

    protected void Button11_Click(object sender, EventArgs e)
    {
        if (txtremarktransportoffice.Text == "")
        {
            string message1 = "Please Fill Remark.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (DropDownList11.SelectedItem.Text == "Select")
        {
            string message1 = "Please Fill No Dues";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Particulars", Label18.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList11.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lbltransportofficename.Text);
        cmd.Parameters.AddWithValue("@Designation", lbltransportofficedeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarktransportoffice.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "Your details have been saved successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        DropDownList11.SelectedItem.Text = "Select";
        txtremarktransportoffice.Text = "";
    }

    protected void Button12_Click(object sender, EventArgs e)
    {
        if (txtremarkguesthouseic.Text == "")
        {
            string message1 = "Please Fill Remark.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (DropDownList12.SelectedItem.Text == "Select")
        {
            string message1 = "Please Fill No Dues";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Particulars", Label19.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList12.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblguestname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblguestdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkguesthouseic.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "Your details have been saved successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        DropDownList12.SelectedItem.Text = "Select";
        txtremarkguesthouseic.Text = "";
    }

    protected void Button13_Click(object sender, EventArgs e)
    {
        if (txtremarkfacultyhose.Text == "")
        {
            string message1 = "Please Fill Remark.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (DropDownList13.SelectedItem.Text == "Select")
        {
            string message1 = "Please Fill No Dues";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Particulars", Label20.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList13.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblfacultyname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblfacultydeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkfacultyhose.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "Your details have been saved successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        DropDownList13.SelectedItem.Text = "Select";
        txtremarkfacultyhose.Text = "";

    }

    protected void Button14_Click(object sender, EventArgs e)
    {
        if (txtremarksportic.Text == "")
        {
            string message1 = "Please Fill Remark.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (DropDownList14.SelectedItem.Text == "Select")
        {
            string message1 = "Please Fill No Dues";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Particulars", Label21.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList14.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblsportname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblsportdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarksportic.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "Your details have been saved successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        DropDownList14.SelectedItem.Text = "Select";
        txtremarksportic.Text = "";
    }

    protected void Button15_Click(object sender, EventArgs e)
    {
        if (txtremarkmedicalstore.Text == "")
        {
            string message1 = "Please Fill Remark.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (DropDownList15.SelectedItem.Text == "Select")
        {
            string message1 = "Please Fill No Dues";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Particulars", Label22.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList15.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblmedicalname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblmedicaldeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkmedicalstore.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "Your details have been saved successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        DropDownList15.SelectedItem.Text = "Select";
        txtremarkmedicalstore.Text = "";

    }

    protected void Button16_Click(object sender, EventArgs e)
    {
        if (txtremarkelectrictydepart.Text == "")
        {
            string message1 = "Please Fill Remark.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (DropDownList16.SelectedItem.Text == "Select")
        {
            string message1 = "Please Fill No Dues";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Particulars", Label23.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList16.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblelectricityname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblelectricitydeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkelectrictydepart.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "Your details have been saved successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        DropDownList16.SelectedItem.Text = "";
        txtremarkelectrictydepart.Text = "";
    }

    protected void Button17_Click(object sender, EventArgs e)
    {
        if (txtremarkicardoffice.Text == "")
        {
            string message1 = "Please Fill Remark.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (DropDownList17.SelectedItem.Text == "Select")
        {
            string message1 = "Please Fill No Dues";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Particulars", Label24.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList17.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblicardofficename.Text);
        cmd.Parameters.AddWithValue("@Designation", lbliccardofficedeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkicardoffice.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "Your details have been saved successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        DropDownList17.SelectedItem.Text = "Select";
        txtremarkicardoffice.Text = "";

    }

    protected void Button18_Click(object sender, EventArgs e)
    {
        if (txtremarkit.Text == "")
        {
            string message1 = "Please Fill Remark.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (DropDownList18.SelectedItem.Text == "Select")
        {
            string message1 = "Please Fill No Dues";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Particulars", Label25.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList18.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblitname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblitdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkit.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "Your details have been saved successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        DropDownList18.SelectedItem.Text = "";
        txtremarkit.Text = "";
    }

    protected void Button19_Click(object sender, EventArgs e)
    {
        if (txtremarkheaddept.Text == "")
        {
            string message1 = "Please Fill Remark.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (DropDownList19.SelectedItem.Text == "Select")
        {
            string message1 = "Please Fill No Dues";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Particulars", Label26.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList19.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lbldepartmenthname.Text);
        cmd.Parameters.AddWithValue("@Designation", lbldepartmentdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkheaddept.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "Your details have been saved successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        DropDownList19.SelectedItem.Text = "Select";
        txtremarkheaddept.Text = "";
    }

    protected void Button20_Click(object sender, EventArgs e)
    {
        if (txtremarkcashcounter.Text == "")
        {
            string message1 = "Please Fill Remark.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (DropDownList20.SelectedItem.Text == "Select")
        {
            string message1 = "Please Fill No Dues";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Particulars", Label27.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList20.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblcashname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblcashdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkcashcounter.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "Your details have been saved successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        DropDownList20.SelectedItem.Text = "Select";
        txtremarkcashcounter.Text = "";
    }

    protected void Button21_Click(object sender, EventArgs e)
    {
        if (txtremarkjtdirector.Text == "")
        {
            string message1 = "Please Fill Remark.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (DropDownList21.SelectedItem.Text == "Select")
        {
            string message1 = "Please Fill No Dues";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Particulars", Label28.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList21.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lbljtdirename.Text);
        cmd.Parameters.AddWithValue("@Designation", lbljtdirdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkjtdirector.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "Your details have been saved successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        txtremarkjtdirector.Text = "";
        DropDownList21.SelectedItem.Text = "Select";
    }

    //protected void Button22_Click(object sender, EventArgs e)
    //{
    //    if (txtremarkpayrollsection.Text == "")
    //    {
    //        string message1 = "Please Fill Remark.";
    //        string script1 = "window.onload = function(){ alert('";
    //        script1 += message1;
    //        script1 += "')};";
    //        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
    //        return;
    //    }
    //    if (DropDownList22.SelectedItem.Text == "Select")
    //    {
    //        string message1 = "Please Fill No Dues";
    //        string script1 = "window.onload = function(){ alert('";
    //        script1 += message1;
    //        script1 += "')};";
    //        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
    //        return;
    //    }
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    //    SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@Particulars", Label29.Text);
    //    cmd.Parameters.AddWithValue("@No_Dues", DropDownList22.SelectedItem.Text);
    //    cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblpayrollname.Text);
    //    cmd.Parameters.AddWithValue("@Designation", lblpayrolldeg.Text);
    //    cmd.Parameters.AddWithValue("@Remark", txtremarkpayrollsection.Text);
    //    if (con.State == ConnectionState.Open)
    //    { con.Close(); }
    //    con.Open();
    //    cmd.ExecuteNonQuery();
    //    con.Close();
    //    string message = "Your details have been saved successfully.";
    //    string script = "window.onload = function(){ alert('";
    //    script += message;
    //    script += "')};";
    //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
    //    txtremarkpayrollsection.Text = "";
    //    DropDownList22.SelectedItem.Text = "Select";
    //}
    public void FORCOLLEGE()

    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        //string strSQL = "select No_,[HOD Name],[Father Name],[Global Dimension 1 Code],[HOD Name],[HOD],[Leave Date],[Job Title_Grade Desc],[Mobile Phone No_],[E-Mail],[Branch Name],Address,[Employment Date] from TMU$Employee where [No_]='" + Session["uid"].ToString() + "'";
        string strSQL = "select HOD, (Select [Job Title_Grade Desc] from TMU$Employee where No_=T.HOD) as Title, * from TMU$Employee as T where No_='" + Session["uid"].ToString() + "'";
        
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        txtdirectorprinciname.Text = dt.Rows[0]["HOD Name"].ToString();
        txtdirectorprincipalheaddeg.Text = dt.Rows[0]["Title"].ToString();
        txtdirectorprincipalheadcollegedept.Text = dt.Rows[0]["Branch Name"].ToString();
        lbldeptstorename.Text = dt.Rows[0]["HOD Name"].ToString();
        lblcollegedeptname.Text= dt.Rows[0]["HOD Name"].ToString();
        lblcollegeworkname.Text= dt.Rows[0]["HOD Name"].ToString();
        lbldepartmenthname.Text= dt.Rows[0]["HOD Name"].ToString();
        lbldeptstoredeg.Text = dt.Rows[0]["Title"].ToString();
        lblcollegedeptdeg.Text= dt.Rows[0]["Title"].ToString();
        lblcollegeworkdeg.Text = dt.Rows[0]["Title"].ToString();
        lbldepartmentdeg.Text = dt.Rows[0]["Title"].ToString();
        //txtdatedirectorprincipaldate.Text = dt.Rows[0]["Institution"].ToString();

    }
    public void alldept()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "Select No_Dues,Remark,Particulars,Designation,Dept_Employee_Name from Tbl_OutstandingNoDues where Employee_Code ='" + txtemployeecode.Text + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            DataRow[] dr1 = dt.Select("Particulars = 'Central Library'");
            if (dr1.Length > 0)
            {
                DropDownList2.SelectedItem.Text = dr1[0]["No_Dues"].ToString();
                txtremarkcentrallib.Text = dr1[0]["Remark"].ToString();
            }
            DataRow[] dr2 = dt.Select("Particulars = 'Department Library'");
            if (dr2.Length > 0)
            {
                DropDownList1.SelectedItem.Text = dr2[0]["No_Dues"].ToString();
                txtremarkdeptlibrary.Text = dr2[0]["Remark"].ToString();
            }
            //DataRow[] dr3 = dt.Select("Particulars = 'Book Bank'");
            //if (dr3.Length > 0)
            //{
            //    DropDownList3.SelectedItem.Text = dr3[0]["No_Dues"].ToString();
            //    txtremarkbookbank.Text = dr3[0]["Remark"].ToString();
            //}
            DataRow[] dr4 = dt.Select("Particulars = 'Seed Money'");
            if (dr4.Length > 0)
            {
                DropDownList4.SelectedItem.Text = dr4[0]["No_Dues"].ToString();
                txtremarkseedmoney.Text = dr4[0]["Remark"].ToString();
            }
            DataRow[] dr5 = dt.Select("Particulars = 'College/Department Store'");
            if (dr5.Length > 0)
            {
                DropDownList5.SelectedItem.Text = dr5[0]["No_Dues"].ToString();
                txtremarkdepartmentstore.Text = dr5[0]["Remark"].ToString();
            }
            DataRow[] dr6 = dt.Select("Particulars = 'Central Store (Furniture or any other equipment et'");
            if (dr6.Length > 0)
            {
                DropDownList6.SelectedItem.Text = dr6[0]["No_Dues"].ToString();
                txtremarkcentralstore.Text = dr6[0]["Remark"].ToString();
            }
            DataRow[] dr7 = dt.Select("Particulars = 'College/Department Laboratory'");
            if (dr7.Length > 0)
            {
                DropDownList7.SelectedItem.Text = dr7[0]["No_Dues"].ToString();
                txtremarkcollegedeptre.Text = dr7[0]["Remark"].ToString();
            }
            DataRow[] dr8 = dt.Select("Particulars = 'College/Department Workshop'");
            if (dr8.Length > 0)
            {
                DropDownList8.SelectedItem.Text = dr8[0]["No_Dues"].ToString();
                txtremarkdepartmentwork.Text = dr8[0]["Remark"].ToString();
            }
            DataRow[] dr9 = dt.Select("Particulars = 'Hostel Mess'");
            if (dr9.Length > 0)
            {
                DropDownList9.SelectedItem.Text = dr9[0]["No_Dues"].ToString();
                txtremarkhostelmess.Text = dr9[0]["Remark"].ToString();
            }
            DataRow[] dr10 = dt.Select("Particulars = 'Hostel Office'");
            if (dr10.Length > 0)
            {
                DropDownList10.SelectedItem.Text = dr10[0]["No_Dues"].ToString();
                txtremarkhosteloffice.Text = dr10[0]["Remark"].ToString();
            }
            DataRow[] dr11 = dt.Select("Particulars = 'Transport Office'");
            if (dr11.Length > 0)
            {
                DropDownList11.SelectedItem.Text = dr11[0]["No_Dues"].ToString();
                txtremarktransportoffice.Text = dr11[0]["Remark"].ToString();
            }
            DataRow[] dr12 = dt.Select("Particulars = 'Guest House I/c'");
            if (dr12.Length > 0)
            {
                DropDownList12.SelectedItem.Text = dr12[0]["No_Dues"].ToString();
                txtremarkguesthouseic.Text = dr12[0]["Remark"].ToString();
            }


            DataRow[] dr13 = dt.Select("Particulars = 'Faculty House I/c'");
            if (dr13.Length > 0)
            {
                DropDownList13.SelectedItem.Text = dr13[0]["No_Dues"].ToString();
                txtremarkfacultyhose.Text = dr13[0]["Remark"].ToString();
            }
            DataRow[] dr14 = dt.Select("Particulars = 'Sport I/c'");
            if (dr14.Length > 0)
            {
                DropDownList14.SelectedItem.Text = dr14[0]["No_Dues"].ToString();
                txtremarksportic.Text = dr14[0]["Remark"].ToString();
            }
            DataRow[] dr15 = dt.Select("Particulars = 'Medical Store'");
            if (dr15.Length > 0)
            {
                DropDownList15.SelectedItem.Text = dr15[0]["No_Dues"].ToString();
                txtremarkmedicalstore.Text = dr15[0]["Remark"].ToString();
            }
            DataRow[] dr16 = dt.Select("Particulars = 'Electricty Department'");
            if (dr16.Length > 0)
            {
                DropDownList16.SelectedItem.Text = dr16[0]["No_Dues"].ToString();
                txtremarkelectrictydepart.Text = dr16[0]["Remark"].ToString();
            }
            DataRow[] dr17 = dt.Select("Particulars = 'I/Card Office (for surrendering I/Card)'");
            if (dr17.Length > 0)
            {
                DropDownList17.SelectedItem.Text = dr17[0]["No_Dues"].ToString();
                txtremarkicardoffice.Text = dr17[0]["Remark"].ToString();
            }
            DataRow[] dr18 = dt.Select("Particulars = 'Computer Center/IT Dept.(for surrendering Mobile H'");
            if (dr18.Length > 0)
            {
                DropDownList18.SelectedItem.Text = dr18[0]["No_Dues"].ToString();
                txtremarkit.Text = dr18[0]["Remark"].ToString();
            }
            DataRow[] dr19 = dt.Select("Particulars = 'Department Head'");
            if (dr19.Length > 0)
            {
                DropDownList19.SelectedItem.Text = dr19[0]["No_Dues"].ToString();
                txtremarkheaddept.Text = dr19[0]["Remark"].ToString();
            }
            DataRow[] dr20 = dt.Select("Particulars = 'Cash Counter'");
            if (dr20.Length > 0)
            {
                DropDownList20.SelectedItem.Text = dr20[0]["No_Dues"].ToString();
                txtremarkcashcounter.Text = dr20[0]["Remark"].ToString();
            }
            DataRow[] dr21 = dt.Select("Particulars = 'Jt. Director(Security)'");
            if (dr21.Length > 0)
            {
                DropDownList21.SelectedItem.Text = dr21[0]["No_Dues"].ToString();
                txtremarkjtdirector.Text = dr21[0]["Remark"].ToString();
            }
            //DataRow[] dr22 = dt.Select("Particulars = 'Payroll Section'");
            //if (dr22.Length > 0)
            //{
            //    DropDownList22.SelectedItem.Text = dr22[0]["No_Dues"].ToString();
            //    txtremarkpayrollsection.Text = dr22[0]["Remark"].ToString();
            //    lblpayrollname.Text = dr22[0]["Dept_Employee_Name"].ToString();
            //    lblpayrolldeg.Text = dr22[0]["Designation"].ToString();
            //}
            DataRow[] dr23 = dt.Select("Particulars = 'Ph.D DEPARTMENT'");
            if (dr23.Length > 0)
            {
                drpphddepartment.SelectedItem.Text = dr23[0]["No_Dues"].ToString();
                txtphddepartmentremark.Text = dr23[0]["Remark"].ToString();
            }
            DataRow[] dr24 = dt.Select("Particulars = 'Pharmacy'");
            if (dr24.Length > 0)
            {
                drpphddepartment.SelectedItem.Text = dr24[0]["No_Dues"].ToString();
                txtphddepartmentremark.Text = dr24[0]["Remark"].ToString();
            }


        }

    }
    protected void btn_Savehr_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("Pro_HrAccountDeptartment", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@LeaveTypeAcademic", Label51.Text);
        cmd.Parameters.AddWithValue("@LeaveTypeMedical", Label52.Text);
        cmd.Parameters.AddWithValue("@LeaveTypeCasual", Label53.Text);
        cmd.Parameters.AddWithValue("@LeaveTypeEarned", Label54.Text);
        cmd.Parameters.AddWithValue("@LeaveTypeExtraordinary", Label55.Text);
        cmd.Parameters.AddWithValue("@LeaveTypeSpecial", Label56.Text);
        cmd.Parameters.AddWithValue("@LeaveTypeSabbatical", Label57.Text);
        cmd.Parameters.AddWithValue("@LeaveTypeMaternity", Label58.Text);
        cmd.Parameters.AddWithValue("@Leavetypeany", Label59.Text);
        cmd.Parameters.AddWithValue("@AcademicNoDues", txtdue1.Text);
        cmd.Parameters.AddWithValue("@AcademicAvailed", txtavailed1.Text);
        cmd.Parameters.AddWithValue("@AcademicBalance", drpbalance1.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@AcademicAdjusted", drpadjust1.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@MedicalDue", txtdues2.Text);
        cmd.Parameters.AddWithValue("@MedicalAvailed", txtavailed2.Text);
        cmd.Parameters.AddWithValue("@MedicalBalance", drbalance2.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@MedicalAdjusted", drpadjust2.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@CasualDue", txtdues3.Text);
        cmd.Parameters.AddWithValue("@CasualAvailed", txtavailed3.Text);
        cmd.Parameters.AddWithValue("@CasualBalance", drpbalance3.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@CasualAdjusted", drpadjust3.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@EarnedDue", txtdues4.Text);
        cmd.Parameters.AddWithValue("@EarnAvailed", txtavailed4.Text);
        cmd.Parameters.AddWithValue("@EarnBalance", drpbalance4.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@EarnAdjusted", drpadjust4.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Extradue", txtdue5.Text);
        cmd.Parameters.AddWithValue("@ExtraAvailed", txtavailed5.Text);
        cmd.Parameters.AddWithValue("@ExtraBalance", drpbalance5.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@ExtraAdjusted", drpadjust5.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@SpecialDue", txtdue6.Text);
        cmd.Parameters.AddWithValue("@SpecialAvailed", txtavailed6.Text);
        cmd.Parameters.AddWithValue("@SpecialBalance", drpbalance6.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@SpecialAdjusted", drpadjust6.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@SabbaticalDue", txtdue7.Text);
        cmd.Parameters.AddWithValue("@SabbaticalAvailed", txtavailed7.Text);
        cmd.Parameters.AddWithValue("@SabbaticalBalance", drpbalance7.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@SabbaticalAdjusted", drpadjust7.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@MaterintyDue", txtdue8.Text);
        cmd.Parameters.AddWithValue("@MaterintyAvailed", txtavailed8.Text);
        cmd.Parameters.AddWithValue("@MaterintyBalance", drpbalance8.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@MaterintyAdjusted", drpadjust8.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@LeavetypeDue", txtdue9.Text);
        cmd.Parameters.AddWithValue("@LeavetypeAvailed", txtavailed9.Text);
        cmd.Parameters.AddWithValue("@LeavetypeBalance", drpbalance9.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@LeavetypeAdjusted", drpadjust9.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@SignatureHr", "");
        cmd.Parameters.AddWithValue("@NameHr", "");
        cmd.Parameters.AddWithValue("@Designation", "");
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Update successfully.'); document.location.href='NoDues.aspx';", true);
    }
    public void showhr()
    {
        if (Session["uid"].ToString() == "TMU005721")
        {
            Label42.Enabled = true;
            Label43.Enabled = true;
            Label43.Enabled = true;
            Label44.Enabled = true;
            Label45.Enabled = true;
            Label46.Enabled = true;
            Label47.Enabled = true;
            Label49.Enabled = true;
            Label51.Enabled = true;
            Label52.Enabled = true;
            Label53.Enabled = true;
            Label54.Enabled = true;
            Label55.Enabled = true;
            Label56.Enabled = true;
            Label57.Enabled = true;
            Label58.Enabled = true;
            Label59.Enabled = true;
            txtdue1.Enabled = true;
            txtdues2.Enabled = true;
            txtdues3.Enabled = true;
            txtdues4.Enabled = true;
            txtdue5.Enabled = true;
            txtdue6.Enabled = true;
            txtdue7.Enabled = true;
            txtdue8.Enabled = true;
            txtdue9.Enabled = true;
            txtavailed1.Enabled = true;
            txtavailed2.Enabled = true;
            txtavailed3.Enabled = true;
            txtavailed4.Enabled = true;
            txtavailed5.Enabled = true;
            txtavailed6.Enabled = true;
            txtavailed7.Enabled = true;
            txtavailed8.Enabled = true;
            txtavailed9.Enabled = true;
            drpbalance1.Enabled = true;
            drbalance2.Enabled = true;
            drpbalance3.Enabled = true;
            drpbalance4.Enabled = true;
            drpbalance5.Enabled = true;
            drpbalance6.Enabled = true;
            drpbalance7.Enabled = true;
            drpbalance8.Enabled = true;
            drpbalance9.Enabled = true;
            drpadjust1.Enabled = true;
            drpadjust2.Enabled = true;
            drpadjust3.Enabled = true;
            drpadjust4.Enabled = true;
            drpadjust5.Enabled = true;
            drpadjust6.Enabled = true;
            drpadjust7.Enabled = true;
            drpadjust8.Enabled = true;
            drpadjust9.Enabled = true;
            TextBox81.Enabled = true;
            TextBox82.Enabled = true;
            TextBox83.Enabled = true;
            TextBox84.Enabled = true;
            TextBox85.Enabled = true;
            TextBox86.Enabled = true;
            TextBox87.Enabled = true;
            TextBox88.Enabled = true;
            txttotalamount.Enabled = true;
            txtdatehr.Enabled = true;
            txtdatehr0.Enabled = true;
            //btn_Savehr.Enabled = true;

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("proc_UpdateNoDuesData", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Name_Finance", TextBox9.Text);
        cmd.Parameters.AddWithValue("@Designation_Finance", TextBox10.Text);
        cmd.Parameters.AddWithValue("@Date2_Finance", txtfinance.Text);
        cmd.Parameters.AddWithValue("@Signature_Finance", TextBox7.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "Your details have been saved successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
    }

    public void submitfrm()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("select * from [Tbl_NoDuesDataTable] where [Employee_Code]='" + Session["uid"].ToString() + "'", con))
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string Status = dr["Status"].ToString();
                    con.Close();
                    if (Status == "Submit")

                    {
                        btnsave.Visible = false;
                    }
                    else
                    {
                        btnsave.Visible = true;
                    }


                }
            }
        }
    }
    public void showfinance()
    {
        if (Session["uid"].ToString() == "TMU00049")
        {
            Button1.Visible = true;
            txtfinance.Enabled = true;
            TextBox7.Enabled = true;
            TextBox9.Enabled = true;
            TextBox10.Enabled = true;
        }
    }



    //public void hidehr()
    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    //    {
    //        using (SqlCommand cmd = new SqlCommand("select count(*) No_Dues  from Tbl_OutstandingNoDues where No_Dues =' Not Granted'", con))
    //        {
    //            con.Open();
    //            SqlDataReader dr = cmd.ExecuteReader();
    //            if (dr.Read())
    //            {
    //                string No_Dues = dr["No_Dues"].ToString();
    //                con.Close();
    //                if (No_Dues == "Not Granted")

    //                {
    //                    btn_Savehr.Visible = false;
    //                }


    //            }
    //        }
    //    }
    //}
    public void hrdept()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "Select * from tbl_hraccountdepartment where Employee_Code ='" + txtemployeecode.Text + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            TextBox81.Text = dt.Rows[0]["Description"].ToString();
            TextBox82.Text = dt.Rows[0]["Amount_Rs"].ToString();
            txttotalamount.Text = dt.Rows[0]["TotalAmount"].ToString();
            TextBox16.Text = dt.Rows[0]["AmountHR"].ToString();
            TextBox17.Text = dt.Rows[0]["AmountHr_Rs"].ToString();
            TextBox83.Text = dt.Rows[0]["Signature1Hr"].ToString();
            TextBox84.Text = dt.Rows[0]["Name1Hr"].ToString();
            TextBox85.Text = dt.Rows[0]["Designation1Hr"].ToString();
            TextBox86.Text = dt.Rows[0]["Signature2Hr"].ToString();
            TextBox87.Text = dt.Rows[0]["Name2Hr"].ToString();
            TextBox88.Text = dt.Rows[0]["Designation2Hr"].ToString();
            txtdatehr0.Text = dt.Rows[0]["Date1Hr"].ToString();
            lblapprovedby.Text = dt.Rows[0]["Approvedbyhr"].ToString();
            txtdatehr.Text = dt.Rows[0]["Date2Hr"].ToString();
            //Label72.Text = dt.Rows[0]["Confir_Employee_Name"].ToString();
            //Label73.Text = dt.Rows[0]["Confir_Designation"].ToString();
            //Label74.Text = dt.Rows[0]["Confir_Deptt_College"].ToString();
            //Label75.Text = dt.Rows[0]["Confir_wef1"].ToString();
            //Label76.Text = dt.Rows[0]["Confir_wef2"].ToString();
            //Label77.Text = dt.Rows[0]["Deputy_Registrar"].ToString();
            //Label79.Text = dt.Rows[0]["Approving_aurity"].ToString();

        }
        {
            DataRow[] dr1 = dt.Select("LeaveTypeAcademic = 'Academic'");
            if (dr1.Length > 0)
            {
                txtdue1.Text = dr1[0]["AcademicNoDues"].ToString();
                txtavailed1.Text = dr1[0]["AcademicAvailed"].ToString();
                drpbalance1.SelectedItem.Text = dr1[0]["AcademicBalance"].ToString();
                drpadjust1.SelectedItem.Text = dr1[0]["AcademicAdjusted"].ToString();
            }
            DataRow[] dr2 = dt.Select("LeaveTypeMedical = 'Medical'");
            if (dr2.Length > 0)
            {
                txtdues2.Text = dr2[0]["MedicalDue"].ToString();
                txtavailed2.Text = dr2[0]["MedicalAvailed"].ToString();
                drbalance2.SelectedItem.Text = dr2[0]["MedicalBalance"].ToString();
                drpadjust2.SelectedItem.Text = dr2[0]["MedicalAdjusted"].ToString();
            }
            DataRow[] dr3 = dt.Select("LeaveTypeCasual = 'Casual'");
            if (dr3.Length > 0)
            {
                txtdues3.Text = dr3[0]["CasualDue"].ToString();
                txtavailed3.Text = dr3[0]["CasualAvailed"].ToString();
                drpbalance3.SelectedItem.Text = dr3[0]["CasualBalance"].ToString();
                drpadjust3.SelectedItem.Text = dr3[0]["CasualAdjusted"].ToString();
            }
            DataRow[] dr4 = dt.Select("LeaveTypeEarned = 'Earned'");
            if (dr4.Length > 0)
            {
                txtdues4.Text = dr4[0]["EarnedDue"].ToString();
                txtavailed4.Text = dr4[0]["EarnAvailed"].ToString();
                drpbalance4.SelectedItem.Text = dr4[0]["EarnBalance"].ToString();
                drpadjust4.SelectedItem.Text = dr4[0]["EarnAdjusted"].ToString();
            }
            DataRow[] dr5 = dt.Select("LeaveTypeExtraordinary = 'Extraordinary'");
            if (dr5.Length > 0)
            {
                txtdue5.Text = dr5[0]["Extradue"].ToString();
                txtavailed5.Text = dr5[0]["ExtraAvailed"].ToString();
                drpbalance5.SelectedItem.Text = dr5[0]["ExtraBalance"].ToString();
                drpadjust5.SelectedItem.Text = dr5[0]["ExtraAdjusted"].ToString();
            }
            DataRow[] dr6 = dt.Select("LeaveTypeSpecial = 'Special'");
            if (dr6.Length > 0)
            {
                txtdue6.Text = dr6[0]["SpecialDue"].ToString();
                txtavailed6.Text = dr6[0]["SpecialAvailed"].ToString();
                drpbalance6.SelectedItem.Text = dr6[0]["SpecialBalance"].ToString();
                drpadjust6.SelectedItem.Text = dr6[0]["SpecialAdjusted"].ToString();
            }
            DataRow[] dr7 = dt.Select("LeaveTypeSabbatical = 'Sabbatical'");
            if (dr7.Length > 0)
            {
                txtdue7.Text = dr7[0]["SabbaticalDue"].ToString();
                txtavailed7.Text = dr7[0]["SabbaticalAvailed"].ToString();
                drpbalance7.SelectedItem.Text = dr7[0]["SabbaticalBalance"].ToString();
                drpadjust7.SelectedItem.Text = dr7[0]["SabbaticalAdjusted"].ToString();
            }
            DataRow[] dr8 = dt.Select("LeaveTypeMaternity = 'Maternity'");
            if (dr8.Length > 0)
            {
                txtdue8.Text = dr8[0]["MaterintyDue"].ToString();
                txtavailed8.Text = dr8[0]["MaterintyAvailed"].ToString();
                drpbalance8.SelectedItem.Text = dr8[0]["MaterintyBalance"].ToString();
                drpadjust8.SelectedItem.Text = dr8[0]["MaterintyAdjusted"].ToString();
            }
            DataRow[] dr9 = dt.Select("Leavetypeany = 'Any Other (please specify)'");
            if (dr8.Length > 0)
            {
                txtdue9.Text = dr9[0]["LeavetypeDue"].ToString();
                txtavailed9.Text = dr9[0]["LeavetypeAvailed"].ToString();
                drpbalance9.SelectedItem.Text = dr9[0]["LeavetypeBalance"].ToString();
                drpadjust9.SelectedItem.Text = dr9[0]["LeavetypeAdjusted"].ToString();
            }
        }

    }
}


