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
using System.Net.Mail;
using System.IO;
using System.Net.Configuration;



public partial class Faculty_NoDuesApproval : System.Web.UI.Page
{
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                getnodueslist();
               // showhr();
               //// Showfinancehr();
               // //submithrm();
               // finance();
               // //showfinance();
               // //submitalldept();
               // //alldept();
               
               // hrdept();
               // hidehr();

               // //resignationdata();
               // //hidehr();
            }
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }

    }

    protected void grdnodueslist_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        grdnodueslist.PageIndex = e.NewPageIndex;
        getnodueslist();
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("NoDuesApproval.aspx");
        GridViewdata.Hide();
    }


    public void getnodueslist()
    {

        SqlCommand cmd = new SqlCommand("pro_nodueslist", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        if (con1.State == ConnectionState.Closed)
            con1.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdnodueslist.DataSource = dtCL;
        grdnodueslist.DataBind();
    }
    public void resignationdata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "select  * from tble_Exit_Interview_Form where [Employee Code]='" + Session["uid"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();

        if (dt.Rows.Count >0)
        {
            //txtdateofleaving.Text = dt.Rows[0]["Date Of Resignation"].ToString();
            txtdateofrelieving.Text = dt.Rows[0]["Date Of Resignation"].ToString();
        }

    }


    public void FORCOLLEGE()

    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        //string strSQL = "select No_,[HOD Name],[Father Name],[Global Dimension 1 Code],[HOD Name],[HOD],[Leave Date],[Job Title_Grade Desc],[Mobile Phone No_],[E-Mail],[Branch Name],Address,[Employment Date] from TMU$Employee where [No_]='" + Session["uid"].ToString() + "'";
        string strSQL = "select HOD, (Select [Job Title_Grade Desc] from [EDUCOLLEGELIVE-R2].dbo.TMU$Employee where No_=T.HOD) as Title, * from [EDUCOLLEGELIVE-R2].dbo.TMU$Employee as T where No_='" + txtemployeecode.Text + "'";

        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        txtdirectorprinciname.Text = dt.Rows[0]["HOD Name"].ToString();
        txtdirectorprincipalheaddeg.Text = dt.Rows[0]["Title"].ToString();
        txtdirectorprincipalheadcollegedept.Text = dt.Rows[0]["Branch Name"].ToString();
        lbldeptstorename.Text = dt.Rows[0]["HOD Name"].ToString();
        lblcollegedeptname.Text = dt.Rows[0]["HOD Name"].ToString();
        lblcollegeworkname.Text = dt.Rows[0]["HOD Name"].ToString();
        lbldepartmenthname.Text = dt.Rows[0]["HOD Name"].ToString();
        lbldeptstoredeg.Text = dt.Rows[0]["Title"].ToString();
        lblcollegedeptdeg.Text = dt.Rows[0]["Title"].ToString();
        lblcollegeworkdeg.Text = dt.Rows[0]["Title"].ToString();
        lbldepartmentdeg.Text = dt.Rows[0]["Title"].ToString();
        //txtdatedirectorprincipaldate.Text = dt.Rows[0]["Institution"].ToString();

    }
    protected void lnkbutton_Click(object sender, EventArgs e)
    {

        GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
        int index = row.RowIndex;
        Label UserId = (Label)grdnodueslist.Rows[index].FindControl("lblemployeecode");
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
       string strSQL = "select *,( select [Job Title_Grade Desc] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_CI_AI=Employee_Code) 'Designation',convert(varchar,convert(date,Date_Of_Joining),106) DOJ from Tbl_NoDuesDataTable WHERE [Employee_Code]='" + UserId.Text + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        txtemployeecode.Text = dt.Rows[0]["Employee_Code"].ToString();
        txtnameofemployee.Text = dt.Rows[0]["Name_Of_Employee"].ToString();
        Label33.Text = dt.Rows[0]["Name_Of_Employee"].ToString();
       //Label6.Text= dt.Rows[0]["Employee_Code"].ToString();
       //Label29.Text= dt.Rows[0]["Name_Of_Employee"].ToString();
        Label50.Text = dt.Rows[0]["Name_Of_Employee"].ToString();
        //Label72.Text = dt.Rows[0]["Name_Of_Employee"].ToString();
        Label88.Text = dt.Rows[0]["Name_Of_Employee"].ToString();
        Label62.Text = dt.Rows[0]["Name_Of_Employee"].ToString();
        txtfathername.Text = dt.Rows[0]["Father_Name"].ToString();
        txtcollegedeptsection.Text = dt.Rows[0]["College_Department_Section"].ToString();
        txtbranch.Text = dt.Rows[0]["Branch"].ToString();
        txtdateofjoining.Text = dt.Rows[0]["DOJ"].ToString();
        txtdateofleaving.Text = dt.Rows[0]["Date_Of_Leaving"].ToString();
        //txtperaddress.Text = dt.Rows[0]["Permanent_Address"].ToString();
        //txtmailingaddress.Text = dt.Rows[0]["Mailing_Address"].ToString();
        txtmobileno.Text = dt.Rows[0]["Mobile_No"].ToString();
        txtemailid.Text = dt.Rows[0]["E_mail_Id"].ToString();
        txtdirectorprinciname.Text = dt.Rows[0]["Dir_Pri_Head_Name"].ToString();
        lbldeptstorename.Text = dt.Rows[0]["Dir_Pri_Head_Name"].ToString();
        lbldeptstoredeg.Text = dt.Rows[0]["Dir_Pri_Head_Designation"].ToString();
        lblcollegedeptdeg.Text = dt.Rows[0]["Dir_Pri_Head_Designation"].ToString();
        lblcollegeworkname.Text = dt.Rows[0]["Dir_Pri_Head_Name"].ToString();
        lblcollegeworkdeg.Text = dt.Rows[0]["Dir_Pri_Head_Designation"].ToString();
        lblcollegedeptname.Text = dt.Rows[0]["Dir_Pri_Head_Name"].ToString();
        lbldepartmentdeg.Text = dt.Rows[0]["Dir_Pri_Head_Designation"].ToString();
        txtdirectorprincipalheaddeg.Text = dt.Rows[0]["Dir_Pri_Head_Designation"].ToString();
        txtdirectorprincipalheadcollegedept.Text = dt.Rows[0]["Dir_Pri_Head_College_Department_Section"].ToString();
        TextBox89.Text = dt.Rows[0]["Amount1_Finance"].ToString();
        TextBox1.Text = dt.Rows[0]["Amount2_Finance"].ToString();
        TextBox2.Text = dt.Rows[0]["Cheque_No_Finance"].ToString();
        TextBox3.Text = dt.Rows[0]["Date1_Finance"].ToString();
        TextBox4.Text = dt.Rows[0]["DrawnonFinance"].ToString();
        TextBox6.Text = dt.Rows[0]["Amount3_Finance"].ToString();
        TextBox8.Text = dt.Rows[0]["Amount4_Finance"].ToString();
        txtfinance.Text = dt.Rows[0]["Date2_Finance"].ToString();
        TextBox7.Text = dt.Rows[0]["Signature_Finance"].ToString();
        TextBox9.Text = dt.Rows[0]["Name_Finance"].ToString();
        TextBox10.Text = dt.Rows[0]["Designation_Finance"].ToString();
        hdfDesignation.Value = dt.Rows[0]["Designation"].ToString();
        binddata();
        //submithrm();
        
        //submitalldept();
        hrdept();
       
        getdeptlibrary();
        alldept();
        FORCOLLEGE();
        resignationdata();
        txtdatehr.Text = String.Format("{0}", DateTime.Now.ToString("dd/MM/yyyy"));
        txtdatehr0.Text = String.Format("{0}", DateTime.Now.ToString("dd/MM/yyyy"));
        txtfinance.Text = String.Format("{0}", DateTime.Now.ToString("dd/MM/yyyy"));
        TextBox3.Text = String.Format("{0}", DateTime.Now.ToString("dd/MM/yyyy"));
        
        showhr();
        finance();
       // hidehr();
        if (Session["uid"].ToString() == "TMU05721")
        {
            btn_Hrstatus.Visible = true;
            Showfinancehr();
        }
        else
        {
            btn_Hrstatus.Visible = false;
        }
        ShowDept();
        GridViewdata.Show();

    }

    public void getdeptlibrary()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("sp_getlibrarydept1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", txtemployeecode.Text);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        lbldepartmentlib.Text = dtCL.Select("Particulars='Department Library'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lbldepartmentlibdeg.Text = dtCL.Select("Particulars='Department Library'").CopyToDataTable().Rows[0]["Designation"].ToString();
        txtdepartmentlibemployeecode.Text = dtCL.Select("Particulars='Department Library'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblcentlibname.Text = dtCL.Select("Particulars='Central Library'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblcentlibdeg.Text = dtCL.Select("Particulars='Central Library'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblcentlibnameemployeecode.Text = dtCL.Select("Particulars='Central Library'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();



        lblseedmoneyname.Text = dtCL.Select("Particulars='Seed Money'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblseedmoneydeg.Text = dtCL.Select("Particulars='Seed Money'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblseedmoneynamecode.Text = dtCL.Select("Particulars='Seed Money'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lbldeptstorename.Text = dtCL.Select("Particulars='College/Department Store'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lbldeptstoredeg.Text = dtCL.Select("Particulars='College/Department Store'").CopyToDataTable().Rows[0]["Designation"].ToString();
        // lbldeptstorenamecode.Text = dtCL.Select("Particulars='College/Department Store'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblcentralstorename.Text = dtCL.Select("Particulars='Central Store'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblcentralstoredeg.Text = dtCL.Select("Particulars='Central Store'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblcentralstorenamecode.Text = dtCL.Select("Particulars='Central Store'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblcollegedeptname.Text = dtCL.Select("Particulars='College/Department Laboratory'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblcollegedeptdeg.Text = dtCL.Select("Particulars='College/Department Laboratory'").CopyToDataTable().Rows[0]["Designation"].ToString();
        //lblcollegedeptnamecode.Text = dtCL.Select("Particulars='College/Department Laboratory'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblcollegeworkname.Text = dtCL.Select("Particulars='College/Department Wprkshop'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblcollegeworkdeg.Text = dtCL.Select("Particulars='College/Department Wprkshop'").CopyToDataTable().Rows[0]["Designation"].ToString();
        // lblcollegeworknamecode.Text = dtCL.Select("Particulars='College/Department Wprkshop'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblhostelmessname.Text = dtCL.Select("Particulars='Hostel Mess'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblhostelmessdeg.Text = dtCL.Select("Particulars='Hostel Mess'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblhostelmessnamecode.Text = dtCL.Select("Particulars='Hostel Mess'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblhosteloffficename.Text = dtCL.Select("Particulars='Hostel Office'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblhosteloffficedeg.Text = dtCL.Select("Particulars='Hostel Office'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblhosteloffficenamecode.Text = dtCL.Select("Particulars='Hostel Office'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lbltransportofficename.Text = dtCL.Select("Particulars='Transport Office'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lbltransportofficedeg.Text = dtCL.Select("Particulars='Transport Office'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lbltransportofficenamecode.Text = dtCL.Select("Particulars='Transport Office'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();


        lblguestname.Text = dtCL.Select("Particulars='Guest House I/c'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblguestdeg.Text = dtCL.Select("Particulars='Guest House I/c'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblguestnamecode.Text = dtCL.Select("Particulars='Guest House I/c'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblfacultyname.Text = dtCL.Select("Particulars='Faculty House I/c'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblfacultydeg.Text = dtCL.Select("Particulars='Faculty House I/c'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblfacultynamecode.Text = dtCL.Select("Particulars='Faculty House I/c'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblsportname.Text = dtCL.Select("Particulars='Sport I/c'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblsportdeg.Text = dtCL.Select("Particulars='Sport I/c'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblsportnamecode.Text = dtCL.Select("Particulars='Sport I/c'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();


        lblmedicalname.Text = dtCL.Select("Particulars='Medical Store'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblmedicaldeg.Text = dtCL.Select("Particulars='Medical Store'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblmedicalnamecode.Text = dtCL.Select("Particulars='Medical Store'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblelectricityname.Text = dtCL.Select("Particulars='Electricity Department'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblelectricitydeg.Text = dtCL.Select("Particulars='Electricity Department'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblelectricitynamecode.Text = dtCL.Select("Particulars='Electricity Department'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblicardofficename.Text = dtCL.Select("Particulars='I/Card Office'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lbliccardofficedeg.Text = dtCL.Select("Particulars='I/Card Office'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblicardofficenamecode.Text = dtCL.Select("Particulars='I/Card Office'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblitname.Text = dtCL.Select("Particulars='Computer Center/IT Dept.'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblitdeg.Text = dtCL.Select("Particulars='Computer Center/IT Dept.'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblitnamecode.Text = dtCL.Select("Particulars='Computer Center/IT Dept.'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lbldepartmenthname.Text = dtCL.Select("Particulars='Department Head'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lbldepartmentdeg.Text = dtCL.Select("Particulars='Department Head'").CopyToDataTable().Rows[0]["Designation"].ToString();
        // lbldepartmenthname.Text = dtCL.Select("Particulars='Department Head'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblcashname.Text = dtCL.Select("Particulars='Cash Counter'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblcashdeg.Text = dtCL.Select("Particulars='Cash Counter'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblcashnamecode.Text = dtCL.Select("Particulars='Cash Counter'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lbljtdirename.Text = dtCL.Select("Particulars='Jt. Director(Security)'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lbljtdirdeg.Text = dtCL.Select("Particulars='Jt. Director(Security)'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lbljtdirenamecode.Text = dtCL.Select("Particulars='Jt. Director(Security)'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        lblphddepartmentname.Text = dtCL.Select("Particulars='Ph.D DEPARTMENT'").CopyToDataTable().Rows[0]["Employee_Name"].ToString();
        lblphddepartmentdesignation.Text = dtCL.Select("Particulars='Ph.D DEPARTMENT'").CopyToDataTable().Rows[0]["Designation"].ToString();
        lblphddepartmentnamecode.Text = dtCL.Select("Particulars='Ph.D DEPARTMENT'").CopyToDataTable().Rows[0]["Employee_Code"].ToString();

        if ( txtdepartmentlibemployeecode.Text == Session["uid"].ToString())
        {

            DropDownList1.Enabled = true;
            txtremarkdeptlibrary.Enabled = true; 
            btn_submit1.Enabled = true;
            btnrejdeptlibar.Enabled = true;
            
        }
        else
        {

            DropDownList1.Enabled = false;
            txtremarkdeptlibrary.Enabled = false;
            btn_submit1.Enabled = false;
            btnrejdeptlibar.Enabled = false;
           
        }
        if (lblcentlibnameemployeecode.Text == Session["uid"].ToString())
        {
            DropDownList2.Enabled = true;
            txtremarkcentrallib.Enabled = true;
            btnrejectcentrallib.Enabled = true;
            Button2.Enabled = true;
        }
        else
        {
            DropDownList2.Enabled = false;
            Button2.Enabled = false;
            btnrejectcentrallib.Enabled = false;
            txtremarkcentrallib.Enabled = false;
        }
        if (Session["uid"].ToString() == "TMU05328")
        {

            DropDownList9.Enabled = true;
            txtremarkhostelmess.Enabled = true;
            Button9.Enabled = true;
            txthostelmessID.Enabled = true;
            btnrejectmess.Enabled = true;
            

        }
        if (Session["uid"].ToString() == "TMU00619")
        {

            DropDownList10.Enabled = true;
            txtremarkhosteloffice.Enabled = true;
            Button10.Enabled = true;
            txthostelofficeID.Enabled = true;
            btnrejecthosteloffice.Enabled = true;
            


        }
        if (Session["uid"].ToString() == "TMU00211")
        {

            DropDownList11.Enabled = true;
            txtremarktransportoffice.Enabled = true;
            Button11.Enabled = true;
            btnrejecttransport.Enabled = true;
           

        }
        if (Session["uid"].ToString() == "TMU00035")
        {

            DropDownList12.Enabled = true;
            txtremarkguesthouseic.Enabled = true;
            Button12.Enabled = true;
            btnrejcetgesthouse.Enabled = true;
           

        }
        if (Session["uid"].ToString() == "TMU06287")
        {

            DropDownList13.Enabled = true;
            txtremarkfacultyhose.Enabled = true;
            Button13.Enabled = true;
            btnrejectfaculthou.Enabled = true;
            

        }
        if (Session["uid"].ToString() == "TMU00784")
        {

            DropDownList14.Enabled = true;
            txtremarksportic.Enabled = true;
            Button14.Enabled = true;
            btnrejsport.Enabled = true;
            

        }
        if (Session["uid"].ToString() == "TMU04173")
        {

            DropDownList15.Enabled = true;
            txtremarkmedicalstore.Enabled = true;
            Button15.Enabled = true;
            btnrejmedicalstore.Enabled = true;
            

        }
        if (Session["uid"].ToString() == "TMU00381")
        {

            DropDownList16.Enabled = true;
            txtremarkelectrictydepart.Enabled = true;
            Button16.Enabled = true;
            btnrejelectric.Enabled = true;
            

        }
        if (Session["uid"].ToString() == "TMU00166")
        {

            DropDownList17.Enabled = true;
            txtremarkicardoffice.Enabled = true;
            Button17.Enabled = true;
            btniccardreject.Enabled = true;
           

        }
        if (Session["uid"].ToString() == "TMU01979")
        {

            DropDownList18.Enabled = true;
            txtremarkit.Enabled = true;
            Button18.Enabled = true;
            btnrejectit.Enabled = true;
          

        }
        if (Session["uid"].ToString() == "TMU00084")
        {

            DropDownList20.Enabled = true;
            txtremarkcashcounter.Enabled = true;
            Button20.Enabled = true;
            btnrejectcounter.Enabled = true;
           
        }
        if (Session["uid"].ToString() == "TMU00530")
        {

            DropDownList21.Enabled = true;
            txtremarkjtdirector.Enabled = true;
            btnrejectdirector.Enabled = true;
            Button21.Enabled = true;
           

        }


        if (Session["uid"].ToString() == "TMU00283")
        {

            drpphddepartment.Enabled = true;
            txtphddepartmentremark.Enabled = true;
            btnphd.Enabled = true;
            btnphdreject.Enabled = true;
          
         


        }
        if (Session["uid"].ToString() == "TMU05293")
        {
                                             
            DropDownList4.Enabled = true;
            txtremarkseedmoney.Enabled = true;
            Button4.Enabled = true;
            txtrejseedmoney.Enabled = true;
        }


        if (Session["uid"].ToString() == "TMU05824")
        {
            DropDownList6.Enabled = true;
            txtremarkcentralstore.Enabled = true;
            Button6.Enabled = true;
            btnrejcentralstore.Enabled = true;
           
        }



    }

    protected void btn_submit1_Click(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkdeptlibrary.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
            return;
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtdepartmentlibID.Text);
        cmd.Parameters.AddWithValue("@Particulars", LabelR1.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList1.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lbldepartmentlib.Text);
        cmd.Parameters.AddWithValue("@Designation", lbldepartmentlibdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkdeptlibrary.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Submit");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save Successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {

        if (DropDownList2.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkcentrallib.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtCentrallibID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label5.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList2.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblcentlibname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblcentlibdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkcentrallib.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Submit");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save Successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();


    }



    protected void Button4_Click(object sender, EventArgs e)
    {

        if (DropDownList4.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkseedmoney.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtseedmoneyID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label11.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList4.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblseedmoneyname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblseedmoneydeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkseedmoney.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Status");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save Successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void Button5_Click(object sender, EventArgs e)
    {

        if (DropDownList5.SelectedItem.Text == "Select")
        {

            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkdepartmentstore.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtdepartmentStoreID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label12.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList5.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lbldeptstorename.Text);
        cmd.Parameters.AddWithValue("@Designation", lbldeptstoredeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkdepartmentstore.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Submit");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save Successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void Button6_Click(object sender, EventArgs e)
    {

        if (DropDownList6.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkcentralstore.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtCentralstoreID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label13.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList6.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblcentralstorename.Text);
        cmd.Parameters.AddWithValue("@Designation", lblcentralstoredeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkcentralstore.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Submit");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save Successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void txtremarkdepartmentlaborat_Click(object sender, EventArgs e)
    {

        if (DropDownList7.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkcollegedeptre.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtlaborateID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label14.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList7.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblcollegedeptname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblcollegedeptdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkcollegedeptre.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Submit");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save Successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void Button8_Click(object sender, EventArgs e)
    {

        if (DropDownList8.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkdepartmentwork.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtcollegeID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label15.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList8.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblcollegeworkname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblcollegeworkdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkdepartmentwork.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Submit");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save Successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void Button9_Click(object sender, EventArgs e)
    {
        if (DropDownList9.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }

        if (txtremarkhostelmess.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txthostelmessID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label16.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList9.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblhostelmessname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblhostelmessdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkhostelmess.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Submit");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save Successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void Button10_Click(object sender, EventArgs e)
    {

        if (DropDownList10.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkhosteloffice.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txthostelofficeID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label17.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList10.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblhosteloffficename.Text);
        cmd.Parameters.AddWithValue("@Designation", lblhosteloffficedeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkhosteloffice.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Submit");


        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save Successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void Button11_Click(object sender, EventArgs e)
    {
        if (DropDownList11.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }

        if (txtremarktransportoffice.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txttransportID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label18.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList11.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lbltransportofficename.Text);
        cmd.Parameters.AddWithValue("@Designation", lbltransportofficedeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarktransportoffice.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Submit");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save Successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }
    protected void Button12_Click(object sender, EventArgs e)
    {

        if (DropDownList12.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkguesthouseic.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtguesthouseID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label19.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList12.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblguestname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblguestdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkguesthouseic.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Submit");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save Successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void Button13_Click(object sender, EventArgs e)
    {

        if (DropDownList13.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkfacultyhose.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtfacultyhouseID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label20.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList13.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblfacultyname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblfacultydeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkfacultyhose.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Submit");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save Successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();

    }

    protected void Button14_Click(object sender, EventArgs e)
    {

        if (DropDownList14.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarksportic.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtsportID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label21.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList14.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblsportname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblsportdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarksportic.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Submit");
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save Successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void Button15_Click(object sender, EventArgs e)
    {

        if (DropDownList15.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkmedicalstore.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtmedicalstoreID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label22.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList15.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblmedicalname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblmedicaldeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkmedicalstore.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Submit");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save Successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();

    }

    protected void Button16_Click(object sender, EventArgs e)
    {

        if (DropDownList16.SelectedItem.Text == "Select")
        {

            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkelectrictydepart.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtelectricsID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label23.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList16.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblelectricityname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblelectricitydeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkelectrictydepart.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Submit");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save Successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void Button17_Click(object sender, EventArgs e)
    {

        if (DropDownList17.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkicardoffice.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtIcardID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label24.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList17.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblicardofficename.Text);
        cmd.Parameters.AddWithValue("@Designation", lbliccardofficedeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkicardoffice.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Submit");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save Successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();

    }

    protected void Button18_Click(object sender, EventArgs e)
    {

        if (DropDownList18.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkit.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtitID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label25.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList18.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblitname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblitdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkit.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Submit");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save Successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void Button19_Click(object sender, EventArgs e)
    {

        if (DropDownList19.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkheaddept.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtheaddeptID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label26.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList19.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lbldepartmenthname.Text);
        cmd.Parameters.AddWithValue("@Designation", lbldepartmentdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkheaddept.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Submit");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save Successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void Button20_Click(object sender, EventArgs e)
    {

        if (DropDownList20.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkcashcounter.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtCounterID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label27.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList20.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblcashname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblcashdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkcashcounter.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Submit");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save Successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void Button21_Click(object sender, EventArgs e)
    {

        if (DropDownList21.SelectedItem.Text == "Select")
        {

            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkjtdirector.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label28.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList21.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lbljtdirename.Text);
        cmd.Parameters.AddWithValue("@Designation", lbljtdirdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkjtdirector.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Submit");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save Successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }


    //public void FORCOLLEGE()

    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    //    string strSQL = "select [Dir_Pri_Head_Name],[Dir_Pri_Head_Designation],[Dir_Pri_Head_College_Department_Section] from Tbl_NoDuesDataTable where [Employee_Code]='" + txtemployeecode.Text + "'";
    //    SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    con.Close();
    //    txtdirectorprinciname.Text = dt.Rows[0]["Dir_Pri_Head_Name"].ToString();
    //    txtdirectorprincipalheaddeg.Text = dt.Rows[0]["Dir_Pri_Head_Designation"].ToString();
    //    txtdirectorprincipalheadcollegedept.Text = dt.Rows[0]["Dir_Pri_Head_College_Department_Section"].ToString();
    //    txtdatedirectorprincipaldate.Text = dt.Rows[0]["Institution"].ToString();


    public void binddata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        string strSQL = "select No_, [Job Title_Grade Desc],[HOD],[Employment Date],[HOD Name],[Global Dimension 1 Code] from TMU$Employee where [No_]='" + txtemployeecode.Text + "'";
        // string strSQL = "select No_, [Job Title_Grade Desc],[HOD],[Employment Date],[HOD Name],[Global Dimension 1 Code] from TMU$Employee where [No_]='" + txtemployeecode.Text + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            // Label73.Text = dt.Rows[0]["Job Title_Grade Desc"].ToString();
            Label35.Text = dt.Rows[0]["Job Title_Grade Desc"].ToString();
            //Label74.Text = dt.Rows[0]["Global Dimension 1 Code"].ToString();
            Label37.Text = dt.Rows[0]["Employment Date"].ToString();
            Label39.Text = dt.Rows[0]["Global Dimension 1 Code"].ToString();
            lbldepartmenthname.Text = dt.Rows[0]["HOD Name"].ToString();
            lbldepartmenthnamecode.Text = dt.Rows[0]["HOD"].ToString();
            lblcollegedeptnamecode.Text = dt.Rows[0]["HOD"].ToString();
            lblcollegeworknamecode.Text = dt.Rows[0]["HOD"].ToString();
            lbldeptstorenamecode.Text = dt.Rows[0]["HOD"].ToString();
        }
        showNotable();

        if (Session["uid"].ToString() == lbldeptstorenamecode.Text || Session["uid"].ToString() == lblcollegedeptnamecode.Text || Session["uid"].ToString() == lblcollegeworknamecode.Text || Session["uid"].ToString() == lbldepartmenthnamecode.Text)
        {
            DropDownList5.Enabled = true;
            DropDownList19.Enabled = true;
            DropDownList8.Enabled = true;
            DropDownList7.Enabled = true;
            Button5.Enabled = true;
            Button19.Enabled = true;
            Button8.Enabled = true;
            txtremarkdepartmentlaborat.Enabled = true;
            btnrejlaborate.Enabled = true;
            btnrejdepartmentstore.Enabled = true;
            btnrejectdepartment.Enabled = true;
            btnrejdept.Enabled = true;
            txtremarkheaddept.Enabled = true;
            txtremarkdepartmentwork.Enabled = true;
            txtremarkdepartmentlaborat.Enabled = true;
            txtremarkdepartmentstore.Enabled = true;
            txtremarkcollegedeptre.Enabled = true;
        }

    }


    //public void resignationdata()
    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    //    string strSQL = "select  [Date Of Resignation] from tble_Exit_Interview_Form where [Employee Code]='" + Session["uid"].ToString() + "'";
    //    SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    con.Close();
    //    if (dt.Rows.Count > 0)
    //    {
    //        txtdateofleaving.Text = dt.Rows[0]["Date Of Resignation"].ToString();
    //    }


    //}

    protected void btn_Savehr_Click(object sender, EventArgs e)
    {
        //submithrm();
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
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@SignatureHr", "");
        cmd.Parameters.AddWithValue("@NameHr", "");
        cmd.Parameters.AddWithValue("@Employee_name", Label50.Text);
        cmd.Parameters.AddWithValue("@Description", TextBox81.Text);
        cmd.Parameters.AddWithValue("@Amount_Rs", TextBox82.Text);
        cmd.Parameters.AddWithValue("@TotalAmount", txttotalamount.Text);
        cmd.Parameters.AddWithValue("@AmountHR", TextBox16.Text);
        cmd.Parameters.AddWithValue("@AmountHr_Rs", TextBox17.Text);
        cmd.Parameters.AddWithValue("@Signature1Hr", TextBox83.Text);
        cmd.Parameters.AddWithValue("@Name1Hr", TextBox84.Text);
        cmd.Parameters.AddWithValue("@Designation1Hr", TextBox85.Text);
        cmd.Parameters.AddWithValue("@Signature2Hr", TextBox86.Text);
        cmd.Parameters.AddWithValue("@Name2Hr", TextBox87.Text);
        cmd.Parameters.AddWithValue("@Designation2Hr", TextBox88.Text);
        cmd.Parameters.AddWithValue("@Date1Hr", txtdatehr0.Text);
        cmd.Parameters.AddWithValue("@Approvedbyhr", lblapprovedby.Text);
        cmd.Parameters.AddWithValue("@Date2Hr", txtdatehr.Text);
        //cmd.Parameters.AddWithValue("@Confir_Employee_Name", Label72.Text);
        //cmd.Parameters.AddWithValue("@Confir_Designation", Label73.Text);
        //cmd.Parameters.AddWithValue("@Confir_Deptt_College", Label74.Text);
        //cmd.Parameters.AddWithValue("@Confir_wef1", Label75.Text);
        //cmd.Parameters.AddWithValue("@Confir_wef2", Label76.Text);
        //cmd.Parameters.AddWithValue("@Deputy_Registrar", Label77.Text);
        //cmd.Parameters.AddWithValue("@Approving_aurity", Label79.Text);
        cmd.Parameters.AddWithValue("@Status", "Submit");
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save successfully.');", true);
        GridViewdata.Show();
    }

    public void showhr()
    {
        if (Session["uid"].ToString() == "TMU00049")
        {
            hidehr();
           
            Button1.Visible = true;
            Showfinancehr();
            txtfinance.Enabled = true;
            TextBox7.Enabled = true;
            TextBox9.Enabled = true;
            TextBox10.Enabled = true;
            hr.Visible = true;
            Table1.Visible = true;
            Label42.Enabled = true;
            Label43.Enabled = true;
            Label43.Enabled = true;
            Label44.Enabled = true;
            Label45.Enabled = true;
            Label46.Enabled = true;
            Label47.Enabled = true;
            Label48.Enabled = true;
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
            //btn_Savehr.Visible = true;
        }
        else
        {
            Button1.Visible = false;
            divfinance.Visible = false;


        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("proc_InsertNoDuesData", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        //cmd.Parameters.AddWithValue("@Issued_On", txtIssuedon.Text);
        cmd.Parameters.AddWithValue("@Proposed_Date_of_Relieving", txtdateofrelieving.Text);
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

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been saved successfully.');", true);
        GridViewdata.Show();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //finance();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("proc_UpdateNoDuesDataFinance", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        //cmd.Parameters.AddWithValue("@Issued_On", txtIssuedon.Text);
        cmd.Parameters.AddWithValue("@Proposed_Date_of_Relieving", txtdateofrelieving.Text);
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
        cmd.Parameters.AddWithValue("@Name_Finance", TextBox9.Text);
        cmd.Parameters.AddWithValue("@Designation_Finance", TextBox10.Text);
        cmd.Parameters.AddWithValue("@Date2_Finance", txtfinance.Text);
        cmd.Parameters.AddWithValue("@Signature_Finance", TextBox7.Text);
        cmd.Parameters.AddWithValue("@Date1_Finance", TextBox3.Text);
        cmd.Parameters.AddWithValue("@DrawnonFinance", TextBox4.Text);
        cmd.Parameters.AddWithValue("@Amount1_Finance", TextBox89.Text);
        cmd.Parameters.AddWithValue("@Amount2_Finance", TextBox1.Text);
        cmd.Parameters.AddWithValue("@Amount3_Finance", TextBox6.Text);
        cmd.Parameters.AddWithValue("@Amount4_Finance", TextBox8.Text);
        cmd.Parameters.AddWithValue("@Cheque_No_Finance", TextBox2.Text);
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
        cmd.Parameters.AddWithValue("@Employee_name", Label50.Text);
        cmd.Parameters.AddWithValue("@Description", TextBox81.Text);
        cmd.Parameters.AddWithValue("@Amount_Rs", TextBox82.Text);
        cmd.Parameters.AddWithValue("@TotalAmount", txttotalamount.Text);
        cmd.Parameters.AddWithValue("@AmountHR", TextBox16.Text);
        cmd.Parameters.AddWithValue("@AmountHr_Rs", TextBox17.Text);
        cmd.Parameters.AddWithValue("@Signature1Hr", TextBox83.Text);
        cmd.Parameters.AddWithValue("@Name1Hr", TextBox84.Text);
        cmd.Parameters.AddWithValue("@Designation1Hr", TextBox85.Text);
        cmd.Parameters.AddWithValue("@Signature2Hr", TextBox86.Text);
        cmd.Parameters.AddWithValue("@Name2Hr", TextBox87.Text);
        cmd.Parameters.AddWithValue("@Designation2Hr", TextBox88.Text);
        cmd.Parameters.AddWithValue("@Date1Hr", txtdatehr0.Text);
        cmd.Parameters.AddWithValue("@Approvedbyhr", lblapprovedby.Text);
        cmd.Parameters.AddWithValue("@Date2Hr", txtdatehr.Text);
        cmd.Parameters.AddWithValue("@Status", "Submit");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been saved successfully.');", true);
        GridViewdata.Show();
    }

    public void hidehr()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        string strSQL = "select count(*) c from HRMSPortal.dbo.Tbl_OutstandingNoDues where No_Dues='Granted' and  Employee_Code ='" + txtemployeecode.Text + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            if (Convert.ToInt32(dt.Rows[0]["c"]) >= 21)
            {
                divfinance.Visible = true;
                Button1.Visible = true;

            }
            else

            {

                divfinance.Visible = false;
                Button1.Visible = false;
            }
        }
    }
    public void showNotable()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "select [Issued_On],[Proposed_Date_of_Relieving],[Deputy_Registrar1] from Tbl_NoDuesDataTable where [Employee_Code]='" + txtemployeecode.Text + "'";
        SqlDataAdapter da1 = new SqlDataAdapter(strSQL, con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        con.Close();

       // txtIssuedon.Text = dt1.Rows[0]["Issued_On"].ToString();
        txtdateofrelieving.Text = dt1.Rows[0]["Proposed_Date_of_Relieving"].ToString();
        //txtdeputyRegistrar.Text = dt1.Rows[0]["Deputy_Registrar1"].ToString();

    }
    protected void btnphd_Click(object sender, EventArgs e)
    {

        if (drpphddepartment.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtphddepartmentremark.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remark ')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtidphd.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label48.Text);
        cmd.Parameters.AddWithValue("@No_Dues", drpphddepartment.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblphddepartmentname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblphddepartmentdesignation.Text);
        cmd.Parameters.AddWithValue("@Remark", txtphddepartmentremark.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Submit");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been saved successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    //public void submithrm()
    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    //    {
    //        using (SqlCommand cmd = new SqlCommand("select * from [tbl_hraccountdepartment] where [Employee_Code]='" + txtemployeecode.Text + "'", con))
    //        {
    //            con.Open();
    //            SqlDataReader dr = cmd.ExecuteReader();
    //            if (dr.Read())
    //            {
    //                string Status = dr["Status"].ToString();
    //                con.Close();
    //                if (Status == "Submit")

    //                {
    //                    btn_Savehr.Visible = false;
    //                }
    //                else
    //                {
    //                    btn_Savehr.Visible = true;
    //                }


    //            }
    //        }
    //    }
    //}
    public void finance()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("Select * from  tbl_hraccountdepartment where Employee_Code ='" + txtemployeecode.Text + "'", con))
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string Status = dr["Status"].ToString();
                    con.Close();
                    if (Status == "Submit")

                    {
                        Button1.Visible = false;
                    }
                    else
                    {
                        Button1.Visible = true;
                    }
                }
            }
        }
    }





    //public void submitalldept()
    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    //    {
    //        using (SqlCommand cmd = new SqlCommand("select * from [Tbl_OutstandingNoDues] where [Dept_Employee_Code ]='" + Session["uid"].ToString() + "'", con))
    //        {
    //            con.Open();
    //            SqlDataReader dr = cmd.ExecuteReader();
    //            if (dr.Read())
    //            {
    //                string Status = dr["Status"].ToString();
    //                con.Close();
    //                if (Status == "Submit")

    //                {
    //                    btn_submit1.Enabled = false;
    //                    Button2.Enabled = false;
    //                    Button3.Enabled= false;
    //                    Button4.Enabled = false;
    //                    Button5.Enabled = false;
    //                    Button6.Enabled = false;
    //                    txtremarkdepartmentlaborat.Enabled = false;
    //                    Button8.Enabled = false;
    //                    Button9.Enabled = false;
    //                    Button10.Enabled = false;
    //                    Button11.Enabled = false;
    //                    Button12.Enabled = false;
    //                    Button13.Enabled = false;
    //                    Button14.Enabled = false;
    //                    Button15.Enabled = false;
    //                    Button16.Enabled = false;
    //                    Button17.Enabled = false;
    //                    Button18.Enabled = false;
    //                    Button19.Enabled = false;
    //                    Button20.Enabled = false;
    //                    Button21.Enabled = false;
    //                    Button22.Enabled = false;
    //                    btnphd.Enabled = false;




    //                }
    //                else
    //                {
    //                    btn_submit1.Enabled = true;
    //                    Button2.Enabled = true;
    //                    Button3.Enabled = true;
    //                    Button4.Enabled = true;
    //                    Button5.Enabled = true;
    //                    Button6.Enabled = true;
    //                    txtremarkdepartmentlaborat.Enabled = true;
    //                    Button8.Enabled = true;
    //                    Button9.Enabled = true;
    //                    Button10.Enabled = true;
    //                    Button11.Enabled = true;
    //                    Button12.Enabled = true;
    //                    Button13.Enabled = true;
    //                    Button14.Enabled = true;
    //                    Button15.Enabled = true;
    //                    Button16.Enabled = true;
    //                    Button17.Enabled = true;
    //                    Button18.Enabled = true;
    //                    Button19.Enabled = true;
    //                    Button20.Enabled = true;
    //                    Button21.Enabled = true;
    //                    Button22.Enabled = true;
    //                    btnphd.Enabled = true;



    //                }


    //            }
    //        }
    //    }
    //}


    public void alldept()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "Select No_Dues,Remark,Particulars,ID from Tbl_OutstandingNoDues where Employee_Code ='" + txtemployeecode.Text + "'";
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
                txtCentrallibID.Text = dr1[0]["ID"].ToString();
                if (DropDownList2.SelectedItem.Text == "Granted")
                {
                    Button2.ForeColor = System.Drawing.Color.White;
                    Button2.BackColor = System.Drawing.Color.Green;
                    txtremarkcentrallib.Enabled = false;
                    Button2.Enabled = false;
                    DropDownList2.Enabled = false;
                    btnrejectcentrallib.Enabled = false;
                }
                else
                {

                    Button2.ForeColor = System.Drawing.Color.White;
                    Button2.BackColor = System.Drawing.Color.Red;
                    txtremarkcentrallib.Enabled = true;
                    Button2.Enabled = true;
                    btnrejectcentrallib.Enabled = true;

                }
            }
            DataRow[] dr2 = dt.Select("Particulars = 'Department Library'");
            if (dr2.Length > 0)
            {
                DropDownList1.SelectedItem.Text = dr2[0]["No_Dues"].ToString();
                txtremarkdeptlibrary.Text = dr2[0]["Remark"].ToString();
                txtdepartmentlibID.Text = dr2[0]["ID"].ToString();
                if (DropDownList1.SelectedItem.Text == "Granted")
                {
                    btn_submit1.ForeColor = System.Drawing.Color.White;
                    btn_submit1.BackColor = System.Drawing.Color.Green;
                    btn_submit1.Enabled = false;
                    DropDownList1.Enabled = false;
                    txtremarkdeptlibrary.Enabled = false;
                    btnrejdeptlibar.Enabled = false;
                }
                else
                {

                    btn_submit1.ForeColor = System.Drawing.Color.White;
                    btn_submit1.BackColor = System.Drawing.Color.Red;
                    btn_submit1.Enabled = true;
                    btnrejdeptlibar.Enabled = true;

                }
            }
            //DataRow[] dr3 = dt.Select("Particulars = 'Book Bank'");
            //if (dr3.Length > 0)
            //{
            //    DropDownList3.SelectedItem.Text = dr3[0]["No_Dues"].ToString();
            //    txtremarkbookbank.Text = dr3[0]["Remark"].ToString();
            //    txtBookID.Text = dr3[0]["ID"].ToString();
            //}
            DataRow[] dr4 = dt.Select("Particulars = 'Seed Money'");
            if (dr4.Length > 0)
            {
                DropDownList4.SelectedItem.Text = dr4[0]["No_Dues"].ToString();
                txtremarkseedmoney.Text = dr4[0]["Remark"].ToString();
                txtseedmoneyID.Text = dr4[0]["ID"].ToString();
                if(DropDownList4.SelectedItem.Text == "Granted")
                {
                    Button4.ForeColor = System.Drawing.Color.White;
                    Button4.BackColor = System.Drawing.Color.Green;
                    DropDownList4.Enabled = false;
                    txtremarkseedmoney.Enabled = false;
                    txtrejseedmoney.Enabled = false;
                    Button4.Enabled = false;
                }
                else
                {
                    Button4.ForeColor = System.Drawing.Color.White;
                    Button4.BackColor = System.Drawing.Color.Red;
                    DropDownList4.Enabled = true;
                    txtremarkseedmoney.Enabled = true;
                    txtrejseedmoney.Enabled = true;
                    Button4.Enabled = true;
                }
            }
            DataRow[] dr5 = dt.Select("Particulars = 'College/Department Store'");
            if (dr5.Length > 0)
            {
                DropDownList5.SelectedItem.Text = dr5[0]["No_Dues"].ToString();
                txtremarkdepartmentstore.Text = dr5[0]["Remark"].ToString();
                txtdepartmentStoreID.Text = dr5[0]["ID"].ToString();

                if (DropDownList5.SelectedItem.Text == "Granted")
                {
                    Button5.ForeColor = System.Drawing.Color.White;
                    Button5.BackColor = System.Drawing.Color.Green;
                    DropDownList5.Enabled = false;
                    txtremarkdepartmentstore.Enabled = false;
                    btnrejcentralstore.Enabled = false;
                    Button5.Enabled = false;
                }
                else
                {
                    Button5.ForeColor = System.Drawing.Color.White;
                    Button5.BackColor = System.Drawing.Color.Red;
                    DropDownList5.Enabled = true;
                    txtremarkdepartmentstore.Enabled = true;
                    btnrejcentralstore.Enabled = true;
                    Button5.Enabled = true;
                }
            }
            DataRow[] dr6 = dt.Select("Particulars = 'Central Store (Furniture or any other equipment et'");
            if (dr6.Length > 0)
            {
                DropDownList6.SelectedItem.Text = dr6[0]["No_Dues"].ToString();
                txtremarkcentralstore.Text = dr6[0]["Remark"].ToString();
                txtCentralstoreID.Text = dr6[0]["ID"].ToString();
                if (DropDownList6.SelectedItem.Text == "Granted")
                {
                    Button6.ForeColor = System.Drawing.Color.White;
                    Button6.BackColor = System.Drawing.Color.Green;
                    DropDownList6.Enabled = false;
                    txtremarkcentralstore.Enabled = false;
                    btnrejcentralstore.Enabled = false;
                    Button6.Enabled = false;
                }
                else
                {
                    Button6.ForeColor = System.Drawing.Color.White;
                    Button6.BackColor = System.Drawing.Color.Red;
                    DropDownList6.Enabled = true;
                    txtremarkcentralstore.Enabled = true;
                    btnrejcentralstore.Enabled = true;
                    Button6.Enabled = true;
                }
            }
            DataRow[] dr7 = dt.Select("Particulars = 'College/Department Laboratory'");
            if (dr7.Length > 0)
            {
                DropDownList7.SelectedItem.Text = dr7[0]["No_Dues"].ToString();
                txtremarkcollegedeptre.Text = dr7[0]["Remark"].ToString();
                txtlaborateID.Text = dr7[0]["ID"].ToString();
                if (DropDownList7.SelectedItem.Text == "Granted")
                {
                    txtremarkdepartmentlaborat.ForeColor = System.Drawing.Color.White;
                    txtremarkdepartmentlaborat.BackColor = System.Drawing.Color.Green;
                    DropDownList7.Enabled = false;
                    txtremarkcollegedeptre.Enabled = false;
                    btnrejlaborate.Enabled = false;
                    txtremarkdepartmentlaborat.Enabled = false;
                }
                else
                {
                    txtremarkdepartmentlaborat.ForeColor = System.Drawing.Color.White;
                    txtremarkdepartmentlaborat.BackColor = System.Drawing.Color.Red;
                    DropDownList7.Enabled = true;
                    txtremarkcollegedeptre.Enabled = true;
                    btnrejlaborate.Enabled = true;
                    txtremarkdepartmentlaborat.Enabled = true;
                }
            }
            DataRow[] dr8 = dt.Select("Particulars = 'College/Department Workshop'");
            if (dr8.Length > 0)
            {
                DropDownList8.SelectedItem.Text = dr8[0]["No_Dues"].ToString();
                txtremarkdepartmentwork.Text = dr8[0]["Remark"].ToString();
                txtcollegeID.Text = dr8[0]["ID"].ToString();
                if (DropDownList8.SelectedItem.Text == "Granted")
                {
                    Button8.ForeColor = System.Drawing.Color.White;
                    Button8.BackColor = System.Drawing.Color.Green;
                    DropDownList8.Enabled = false;
                    txtremarkdepartmentwork.Enabled = false;
                    btnrejectdepartment.Enabled = false;
                    Button8.Enabled = false;
                }
                else
                {
                    Button8.ForeColor = System.Drawing.Color.White;
                    Button8.BackColor = System.Drawing.Color.Red;
                    DropDownList8.Enabled = true;
                    txtremarkdepartmentwork.Enabled = true;
                    btnrejectdepartment.Enabled = true;
                    Button8.Enabled = true;
                }
            }
            DataRow[] dr9 = dt.Select("Particulars = 'Hostel Mess'");
            if (dr9.Length > 0)
            {
                DropDownList9.SelectedItem.Text = dr9[0]["No_Dues"].ToString();
                txtremarkhostelmess.Text = dr9[0]["Remark"].ToString();
                txthostelmessID.Text = dr9[0]["ID"].ToString();
                if (DropDownList9.SelectedItem.Text == "Granted")
                {
                    Button9.ForeColor = System.Drawing.Color.White;
                    Button9.BackColor = System.Drawing.Color.Green;
                    DropDownList9.Enabled = false;
                    txtremarkhostelmess.Enabled = false;
                    btnrejectmess.Enabled = false;
                    Button9.Enabled = false;
                }
                else
                {
                    Button9.ForeColor = System.Drawing.Color.White;
                    Button9.BackColor = System.Drawing.Color.Red;
                    DropDownList9.Enabled = true;
                    txtremarkhostelmess.Enabled = true;
                    btnrejectmess.Enabled = true;
                    Button9.Enabled = true;
                }
            }
            DataRow[] dr10 = dt.Select("Particulars = 'Hostel Office'");
            if (dr10.Length > 0)
            {
                DropDownList10.SelectedItem.Text = dr10[0]["No_Dues"].ToString();
                txtremarkhosteloffice.Text = dr10[0]["Remark"].ToString();
                txthostelofficeID.Text = dr10[0]["ID"].ToString();
                if (DropDownList10.SelectedItem.Text == "Granted")
                {
                    Button10.ForeColor = System.Drawing.Color.White;
                    Button10.BackColor = System.Drawing.Color.Green;
                    DropDownList10.Enabled = false;
                    txtremarkhosteloffice.Enabled = false;
                    btnrejecthosteloffice.Enabled = false;
                    Button10.Enabled = false;
                }
                else
                {
                    Button10.ForeColor = System.Drawing.Color.White;
                    Button10.BackColor = System.Drawing.Color.Red;
                    DropDownList10.Enabled = true;
                    txtremarkhosteloffice.Enabled = true;
                    btnrejecthosteloffice.Enabled = true;
                    Button10.Enabled = true;
                }
            }
            DataRow[] dr11 = dt.Select("Particulars = 'Transport Office'");
            if (dr11.Length > 0)
            {
                DropDownList11.SelectedItem.Text = dr11[0]["No_Dues"].ToString();
                txtremarktransportoffice.Text = dr11[0]["Remark"].ToString();
                txttransportID.Text = dr11[0]["ID"].ToString();
                if (DropDownList11.SelectedItem.Text == "Granted")
                {
                    Button11.ForeColor = System.Drawing.Color.White;
                    Button11.BackColor = System.Drawing.Color.Green;
                    DropDownList11.Enabled = false;
                    txtremarktransportoffice.Enabled = false;
                    btnrejecttransport.Enabled = false;
                    Button11.Enabled = false;
                }
                else
                {
                    Button11.ForeColor = System.Drawing.Color.White;
                    Button11.BackColor = System.Drawing.Color.Red;
                    DropDownList11.Enabled = true;
                    txtremarktransportoffice.Enabled = true;
                    btnrejecttransport.Enabled = true;
                    Button11.Enabled = true;
                }
            }
            DataRow[] dr12 = dt.Select("Particulars = 'Guest House I/c'");
            if (dr12.Length > 0)
            {
                DropDownList12.SelectedItem.Text = dr12[0]["No_Dues"].ToString();
                txtremarkguesthouseic.Text = dr12[0]["Remark"].ToString();
                txtguesthouseID.Text = dr12[0]["ID"].ToString();
                if (DropDownList12.SelectedItem.Text == "Granted")
                {
                    Button12.ForeColor = System.Drawing.Color.White;
                    Button12.BackColor = System.Drawing.Color.Green;
                    DropDownList12.Enabled = false;
                    txtremarkguesthouseic.Enabled = false;
                    btnrejcetgesthouse.Enabled = false;
                    Button12.Enabled = false;
                }
                else
                {
                    Button12.ForeColor = System.Drawing.Color.White;
                    Button12.BackColor = System.Drawing.Color.Red;
                    DropDownList12.Enabled = true;
                    txtremarkguesthouseic.Enabled = true;
                    btnrejcetgesthouse.Enabled = true;
                    Button12.Enabled = true;
                }
            }

            DataRow[] dr13 = dt.Select("Particulars = 'Faculty House I/c'");
            if (dr13.Length > 0)
            {
                DropDownList13.SelectedItem.Text = dr13[0]["No_Dues"].ToString();
                txtremarkfacultyhose.Text = dr13[0]["Remark"].ToString();
                txtfacultyhouseID.Text = dr13[0]["ID"].ToString();
                if (DropDownList13.SelectedItem.Text == "Granted")
                {
                    Button13.ForeColor = System.Drawing.Color.White;
                    Button13.BackColor = System.Drawing.Color.Green;
                    DropDownList13.Enabled = false;
                    txtremarkfacultyhose.Enabled = false;
                    btnrejectfaculthou.Enabled = false;
                    Button13.Enabled = false;
                }
                else
                {
                    Button13.ForeColor = System.Drawing.Color.White;
                    Button13.BackColor = System.Drawing.Color.Red;
                    DropDownList13.Enabled = true;
                    txtremarkfacultyhose.Enabled = true;
                    btnrejectfaculthou.Enabled = true;
                    Button13.Enabled = true;
                }
            }
            DataRow[] dr14 = dt.Select("Particulars = 'Sport I/c'");
            if (dr14.Length > 0)
            {
                DropDownList14.SelectedItem.Text = dr14[0]["No_Dues"].ToString();
                txtremarksportic.Text = dr14[0]["Remark"].ToString();
                txtsportID.Text = dr14[0]["ID"].ToString();
                if (DropDownList14.SelectedItem.Text == "Granted")
                {
                    Button14.ForeColor = System.Drawing.Color.White;
                    Button14.BackColor = System.Drawing.Color.Green;
                    DropDownList14.Enabled = false;
                    txtremarksportic.Enabled = false;
                    btnrejsport.Enabled = false;
                    Button14.Enabled = false;
                }
                else
                {
                    Button14.ForeColor = System.Drawing.Color.White;
                    Button14.BackColor = System.Drawing.Color.Red;
                    DropDownList14.Enabled = true;
                    txtremarksportic.Enabled = true;
                    btnrejsport.Enabled = true;
                    Button14.Enabled = true;
                }
            }
            DataRow[] dr15 = dt.Select("Particulars = 'Medical Store'");
            if (dr15.Length > 0)
            {
                DropDownList15.SelectedItem.Text = dr15[0]["No_Dues"].ToString();
                txtremarkmedicalstore.Text = dr15[0]["Remark"].ToString();
                txtmedicalstoreID.Text = dr15[0]["ID"].ToString();
                if (DropDownList15.SelectedItem.Text == "Granted")
                {
                    Button15.ForeColor = System.Drawing.Color.White;
                    Button15.BackColor = System.Drawing.Color.Green;
                    DropDownList15.Enabled = false;
                    txtremarkmedicalstore.Enabled = false;
                    btnrejmedicalstore.Enabled = false;
                    Button15.Enabled = false;
                }
                else
                {
                    Button15.ForeColor = System.Drawing.Color.White;
                    Button15.BackColor = System.Drawing.Color.Red;
                    DropDownList15.Enabled = true;
                    txtremarkmedicalstore.Enabled = true;
                    btnrejmedicalstore.Enabled = true;
                    Button15.Enabled = true;
                }
            }
            DataRow[] dr16 = dt.Select("Particulars = 'Electricty Department'");
            if (dr16.Length > 0)
            {
                DropDownList16.SelectedItem.Text = dr16[0]["No_Dues"].ToString();
                txtremarkelectrictydepart.Text = dr16[0]["Remark"].ToString();
                txtelectricsID.Text = dr16[0]["ID"].ToString();
                if (DropDownList16.SelectedItem.Text == "Granted")
                {
                    Button16.ForeColor = System.Drawing.Color.White;
                    Button16.BackColor = System.Drawing.Color.Green;
                    DropDownList16.Enabled = false;
                    txtremarkelectrictydepart.Enabled = false;
                    btnrejelectric.Enabled = false;
                    Button16.Enabled = false;
                }
                else
                {
                    Button16.ForeColor = System.Drawing.Color.White;
                    Button16.BackColor = System.Drawing.Color.Red;
                    DropDownList16.Enabled = true;
                    txtremarkelectrictydepart.Enabled = true;
                    btnrejelectric.Enabled = true;
                    Button16.Enabled = true;
                }
            }
            DataRow[] dr17 = dt.Select("Particulars = 'I/Card Office (for surrendering I/Card)'");
            if (dr17.Length > 0)
            {
                DropDownList17.SelectedItem.Text = dr17[0]["No_Dues"].ToString();
                txtremarkicardoffice.Text = dr17[0]["Remark"].ToString();
                txtIcardID.Text = dr17[0]["ID"].ToString();
                if (DropDownList17.SelectedItem.Text == "Granted")
                {
                    Button17.ForeColor = System.Drawing.Color.White;
                    Button17.BackColor = System.Drawing.Color.Green;
                    DropDownList17.Enabled = false;
                    txtremarkicardoffice.Enabled = false;
                    btniccardreject.Enabled = false;
                    Button17.Enabled = false;
                }
                else
                {
                    Button17.ForeColor = System.Drawing.Color.White;
                    Button17.BackColor = System.Drawing.Color.Red;
                    DropDownList17.Enabled = true;
                    txtremarkicardoffice.Enabled = true;
                    btniccardreject.Enabled = true;
                    Button17.Enabled = true;
                }
            }
            DataRow[] dr18 = dt.Select("Particulars = 'Computer Center/IT Dept.(for surrendering Mobile H'");
            if (dr18.Length > 0)
            {
                DropDownList18.SelectedItem.Text = dr18[0]["No_Dues"].ToString();
                txtremarkit.Text = dr18[0]["Remark"].ToString();
                txtitID.Text = dr18[0]["ID"].ToString();
                if (DropDownList18.SelectedItem.Text == "Granted")
                {
                    Button18.ForeColor = System.Drawing.Color.White;
                    Button18.BackColor = System.Drawing.Color.Green;
                    DropDownList18.Enabled = false;
                    txtremarkit.Enabled = false;
                    btnrejectit.Enabled = false;
                    Button18.Enabled = false;
                }
                else
                {
                    Button18.ForeColor = System.Drawing.Color.White;
                    Button18.BackColor = System.Drawing.Color.Red;
                    DropDownList18.Enabled = true;
                    txtremarkit.Enabled = true;
                    btnrejectit.Enabled = true;
                    Button18.Enabled = true;
                }

            }
            DataRow[] dr19 = dt.Select("Particulars = 'Department Head'");
            if (dr19.Length > 0)
            {
                DropDownList19.SelectedItem.Text = dr19[0]["No_Dues"].ToString();
                txtremarkheaddept.Text = dr19[0]["Remark"].ToString();
                txtheaddeptID.Text = dr19[0]["ID"].ToString();
                if (DropDownList19.SelectedItem.Text == "Granted")
                {
                    Button19.ForeColor = System.Drawing.Color.White;
                    Button19.BackColor = System.Drawing.Color.Green;
                    DropDownList19.Enabled = false;
                    txtremarkheaddept.Enabled = false;
                    btnrejdept.Enabled = false;
                    Button19.Enabled = false;
                }
                else
                {
                    Button19.ForeColor = System.Drawing.Color.White;
                    Button19.BackColor = System.Drawing.Color.Red;
                    DropDownList19.Enabled = true;
                    txtremarkheaddept.Enabled = true;
                    btnrejdept.Enabled = true;
                    Button19.Enabled = true;
                }
            }
            DataRow[] dr20 = dt.Select("Particulars = 'Cash Counter'");
            if (dr20.Length > 0)
            {
                DropDownList20.SelectedItem.Text = dr20[0]["No_Dues"].ToString();
                txtremarkcashcounter.Text = dr20[0]["Remark"].ToString();
                txtCounterID.Text = dr20[0]["ID"].ToString();
                if (DropDownList20.SelectedItem.Text == "Granted")
                {
                    Button20.ForeColor = System.Drawing.Color.White;
                    Button20.BackColor = System.Drawing.Color.Green;
                    DropDownList20.Enabled = false;
                    txtremarkcashcounter.Enabled = false;
                    btnrejectcounter.Enabled = false;
                    Button20.Enabled = false;
                }
                else
                {
                    Button20.ForeColor = System.Drawing.Color.White;
                    Button20.BackColor = System.Drawing.Color.Red;
                    DropDownList20.Enabled = true;
                    txtremarkcashcounter.Enabled = true;
                    btnrejectcounter.Enabled = true;
                    Button20.Enabled = true;
                }

            }
            DataRow[] dr21 = dt.Select("Particulars = 'Jt. Director(Security)'");
            if (dr21.Length > 0)
            {
                DropDownList21.SelectedItem.Text = dr21[0]["No_Dues"].ToString();
                txtremarkjtdirector.Text = dr21[0]["Remark"].ToString();
                txtID.Text = dr21[0]["ID"].ToString();

                if (DropDownList21.SelectedItem.Text == "Granted")
                {
                    Button21.ForeColor = System.Drawing.Color.White;
                    Button21.BackColor = System.Drawing.Color.Green;
                    DropDownList21.Enabled = false;
                    txtremarkjtdirector.Enabled = false;
                    btnrejectdirector.Enabled = false;
                    Button21.Enabled = false;
                }
                else
                {
                    Button21.ForeColor = System.Drawing.Color.White;
                    Button21.BackColor = System.Drawing.Color.Red;
                    DropDownList21.Enabled = true;
                    txtremarkjtdirector.Enabled = true;
                    btnrejectdirector.Enabled = true;
                    Button21.Enabled = true;
                }


            }

            DataRow[] dr23 = dt.Select("Particulars = 'Ph.D DEPARTMENT'");
            if (dr23.Length > 0)
            {
                drpphddepartment.SelectedItem.Text = dr23[0]["No_Dues"].ToString();
                txtphddepartmentremark.Text = dr23[0]["Remark"].ToString();
                txtidphd.Text = dr23[0]["ID"].ToString();

                if (drpphddepartment.SelectedItem.Text == "Granted")
                {
                    btnphd.ForeColor = System.Drawing.Color.White;
                    btnphd.BackColor = System.Drawing.Color.Green;
                    drpphddepartment.Enabled = false;
                    txtphddepartmentremark.Enabled = false;
                    btnphdreject.Enabled = false;
                    btnphd.Enabled = false;
                }
                else
                {
                    btnphd.ForeColor = System.Drawing.Color.White;
                    btnphd.BackColor = System.Drawing.Color.Red;
                    drpphddepartment.Enabled = true;
                    txtphddepartmentremark.Enabled = true;
                    btnphdreject.Enabled = true;
                    btnphd.Enabled = true;
                }
            }


        }

    }

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

    protected void btnrejectdirector_Click(object sender, EventArgs e)
    {

        if (DropDownList21.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkjtdirector.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remarks ')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label28.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList21.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lbljtdirename.Text);
        cmd.Parameters.AddWithValue("@Designation", lbljtdirdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkjtdirector.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Reject");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Reject successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void btnrejdeptlibar_Click(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkdeptlibrary.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remarks ')", true);
            return;
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtdepartmentlibID.Text);
        cmd.Parameters.AddWithValue("@Particulars", LabelR1.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList1.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lbldepartmentlib.Text);
        cmd.Parameters.AddWithValue("@Designation", lbldepartmentlibdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkdeptlibrary.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Reject");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Reject successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void btnrejectcentrallib_Click(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkcentrallib.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remarks ')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtCentrallibID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label5.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList2.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblcentlibname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblcentlibdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkcentrallib.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Reject");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Reject successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }


    protected void txtrejseedmoney_Click(object sender, EventArgs e)
    {

        if (DropDownList4.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkseedmoney.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remarks ')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtseedmoneyID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label11.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList4.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblseedmoneyname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblseedmoneydeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkseedmoney.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Reject");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Reject successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void btnrejdepartmentstore_Click(object sender, EventArgs e)
    {

        if (DropDownList5.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkdepartmentstore.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remarks ')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtdepartmentStoreID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label12.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList5.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lbldeptstorename.Text);
        cmd.Parameters.AddWithValue("@Designation", lbldeptstoredeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkdepartmentstore.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Reject");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Reject successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void btnrejcentralstore_Click(object sender, EventArgs e)
    {

        if (DropDownList6.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkcentralstore.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remarks ')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtCentralstoreID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label13.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList6.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblcentralstorename.Text);
        cmd.Parameters.AddWithValue("@Designation", lblcentralstoredeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkcentralstore.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Reject");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Reject successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void btnrejlaborate_Click(object sender, EventArgs e)
    {

        if (DropDownList7.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkcollegedeptre.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remarks ')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtlaborateID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label14.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList7.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblcollegedeptname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblcollegedeptdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkcollegedeptre.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Reject");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Reject successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void btnrejectdepartment_Click(object sender, EventArgs e)
    {
        if (DropDownList8.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkdepartmentwork.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remarks ')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtcollegeID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label15.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList8.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblcollegeworkname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblcollegeworkdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkdepartmentwork.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Reject");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Reject successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void btnrejectmess_Click(object sender, EventArgs e)
    {

        if (DropDownList9.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkhostelmess.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remarks ')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txthostelmessID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label16.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList9.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblhostelmessname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblhostelmessdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkhostelmess.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Reject");
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Reject successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void btnrejecthosteloffice_Click(object sender, EventArgs e)
    {

        if (DropDownList10.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkhosteloffice.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remarks ')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txthostelofficeID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label17.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList10.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblhosteloffficename.Text);
        cmd.Parameters.AddWithValue("@Designation", lblhosteloffficedeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkhosteloffice.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Reject");


        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Reject successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void btnrejecttransport_Click(object sender, EventArgs e)
    {

        if (DropDownList11.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarktransportoffice.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remarks ')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txttransportID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label18.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList11.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lbltransportofficename.Text);
        cmd.Parameters.AddWithValue("@Designation", lbltransportofficedeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarktransportoffice.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Reject");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Reject successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void btnrejcetgesthouse_Click(object sender, EventArgs e)
    {

        if (DropDownList12.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkguesthouseic.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remarks ')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtguesthouseID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label19.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList12.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblguestname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblguestdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkguesthouseic.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Reject");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Reject successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void btnrejectfaculthou_Click(object sender, EventArgs e)
    {

        if (DropDownList13.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkfacultyhose.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remarks ')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtfacultyhouseID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label20.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList13.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblfacultyname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblfacultydeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkfacultyhose.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Reject");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Reject successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void btnrejsport_Click(object sender, EventArgs e)
    {

        if (DropDownList14.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarksportic.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remarks ')", true);
            return;
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtsportID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label21.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList14.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblsportname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblsportdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarksportic.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Reject");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Reject successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void btnrejmedicalstore_Click(object sender, EventArgs e)
    {

        if (DropDownList15.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkmedicalstore.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remarks ')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtmedicalstoreID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label22.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList15.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblmedicalname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblmedicaldeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkmedicalstore.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Reject");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Reject successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();


    }

    protected void btnrejelectric_Click(object sender, EventArgs e)
    {

        if (DropDownList16.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkelectrictydepart.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remarks ')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtelectricsID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label23.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList16.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblelectricityname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblelectricitydeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkelectrictydepart.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Reject");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Reject successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void btniccardreject_Click(object sender, EventArgs e)
    {
        if (txtremarkicardoffice.Text == "")
        {

            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remarks ')", true);
            return;

        }
        if (DropDownList17.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtIcardID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label24.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList17.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblicardofficename.Text);
        cmd.Parameters.AddWithValue("@Designation", lbliccardofficedeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkicardoffice.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Reject");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Reject successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void btnrejectit_Click(object sender, EventArgs e)
    {

        if (DropDownList18.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkit.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remarks ')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtitID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label25.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList18.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblitname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblitdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkit.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Reject");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Reject successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void btnrejdept_Click(object sender, EventArgs e)
    {


        if (DropDownList19.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkheaddept.Text == "")
        {

            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remarks ')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtheaddeptID.Text);
        cmd.Parameters.AddWithValue("@Particulars", Label26.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList19.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lbldepartmenthname.Text);
        cmd.Parameters.AddWithValue("@Designation", lbldepartmentdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkheaddept.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Reject");
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Reject successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void btnrejectcounter_Click(object sender, EventArgs e)
    {

        if (DropDownList20.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Nodues ')", true);
            return;
        }
        if (txtremarkcashcounter.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Remarks ')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Particulars", Label27.Text);
        cmd.Parameters.AddWithValue("@No_Dues", DropDownList20.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblcashname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblcashdeg.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkcashcounter.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Reject");
        cmd.Parameters.AddWithValue("@ID", txtCounterID.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Reject successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }



    protected void btnphdreject_Click(object sender, EventArgs e)
    {

        if (drpphddepartment.SelectedItem.Text == "Select")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Phd Department Nodues ')", true);
            return;
        }
        if (txtphddepartmentremark.Text == "")
        {
            GridViewdata.Show();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Phd Department Remark')", true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_OutstandingNoDues1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Particulars", Label48.Text);
        cmd.Parameters.AddWithValue("@No_Dues", drpphddepartment.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Name", lblphddepartmentname.Text);
        cmd.Parameters.AddWithValue("@Designation", lblphddepartmentdesignation.Text);
        cmd.Parameters.AddWithValue("@Remark", txtphddepartmentremark.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Employee_Name", txtnameofemployee.Text);
        cmd.Parameters.AddWithValue("@Dept_Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", "Reject");
        cmd.Parameters.AddWithValue("@ID", txtidphd.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Reject successfully.');document.location.href='NoDuesApproval.aspx';", true);
        GridViewdata.Show();
    }

    protected void btn_Hrstatus_Click(object sender, EventArgs e)
    {
        btn_Hrstatus.Visible = false;
        if (txtdateofleaving.Text == "")
        {
            btn_Hrstatus.Visible = true;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Leaving Date')", true);
          
            return;
        }

        SqlCommand cmd = new SqlCommand("Pro_HrSubmitNodues", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@dDate_Of_Leaving", txtdateofleaving.Text);
        cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
        if (con1.State == ConnectionState.Open)
        { con1.Close(); }
        con1.Open();
        cmd.ExecuteNonQuery();
        con1.Close();
        sendmail();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Your details have been saved successfully');document.location.href='NoDuesApproval.aspx';", true);
        
    }
    public void ShowDept()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        {
            if (Session["uid"].ToString() == "TMU00477")
            {
                using (SqlCommand cmd = new SqlCommand("select [HospitalHRStatus] as Hr_status from Tbl_NoDuesDataTable where [Employee_Code] = '" + txtemployeecode.Text + "'", con))
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        if (dr.Read())
                        {
                            string Hr_status = dr["Hr_status"].ToString();
                            con.Close();
                            if (Hr_status == "Submit")
                            {
                                divNoduesalldept.Visible = true;
                                btn_Hrstatus.Visible = false;
                            }
                            else
                            {
                                divNoduesalldept.Visible = false;
                                btn_Hrstatus.Visible = true;
                            }
                        }
                    }
                }
            
            
            }
            else
            {


                using (SqlCommand cmd = new SqlCommand("select [HR Status] as Hr_status,[HospitalHRStatus] as Hosp_Hr_status from Tbl_NoDuesDataTable where [Employee_Code] = '" + txtemployeecode.Text + "'", con))
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        if (dr.Read())
                        {
                            string Hr_status = dr["Hr_status"].ToString();
                            string Hosp_Hr_status = dr["Hosp_Hr_status"].ToString();
                            con.Close();
                            if (Hr_status == "Submit" || Hosp_Hr_status == "Submit")
                            {
                                divNoduesalldept.Visible = true;
                                btn_Hrstatus.Visible = false;
                            }
                            else
                            {
                                divNoduesalldept.Visible = false;
                                btn_Hrstatus.Visible = true;
                            }
                        }
                    }
                }
            }
        }
    }



    public void Showfinancehr()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("Select [Status],* from  tbl_hraccountdepartment where Employee_Code = '" + txtemployeecode.Text + "'", con))
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
                            divfinance.Visible = true;
                            Button1.Visible = false;                           
                        }
                        else
                        {

                            divfinance.Visible = true;
                        }
                    }
                }
            }
        }
    }


  public void sendmail()
    {
        SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
        using (MailMessage mm = new MailMessage("naverp@tmu.ac.in", "dy.registrar.hr@tmu.ac.in,jt.registrar@tmu.ac.in, university.librarian@tmu.ac.in  ,joint.registrar@tmu.ac.in ,anshulj.erp@tmu.ac.in ,incharge.store@tmu.ac.in,dharmikjain38@gmail.com ,chiefwarden@tmu.ac.in ,transport@tmu.ac.in ,asstt.registrar@tmu.ac.in ,hospitality@tmu.ac.in ,principal.physicaleducation@tmu.ac.in ,dy.directorfinance.hospital@tmu.ac.in ,er.maintenance@tmu.ac.in,asstt.registrar@tmu.ac.in,head.it@tmu.ac.in ,nitinjindel@gmail.com,dy.dir.ssw@tmu.ac.in,payroll@tmu.ac.in,joint.registrar@tmu.ac.in,hr@tmu.ac.in"))
        {
        //using (MailMessage mm = new MailMessage("bhupendras.erp@tmu.ac.in", "bhupendras.erp@tmu.ac.in")) jt.registrar@tmu.ac.in, university.librarian@tmu.ac.in  ,joint.registrar@tmu.ac.in ,anshulj.erp@tmu.ac.in ,incharge.store@tmu.ac.in,dharmikjain38@gmail.com ,chiefwarden@tmu.ac.in ,transport@tmu.ac.in ,asstt.registrar@tmu.ac.in ,hospitality@tmu.ac.in ,principal.physicaleducation@tmu.ac.in ,dy.directorfinance.hospital@tmu.ac.in ,er.maintenance@tmu.ac.in,asstt.registrar@tmu.ac.in,head.it@tmu.ac.in ,nitinjindel@gmail.com,dy.dir.ssw@tmu.ac.in,payroll@tmu.ac.in,joint.registrar@tmu.ac.in,,hr@tmu.ac.in
        //{
            mm.Subject = "No Dues Form Status: Active,Employee Code:" + txtemployeecode.Text + ",Employee Name: " + txtnameofemployee.Text + "";
              mm.Body = "<HTML><div>Sir/ Madam,</div>";
            mm.Body += "<div>&nbsp;</div>";
            mm.Body += "<div>";
            mm.Body += "<p><span style='font-family: verdana, sans-serif;'>No&nbsp;Dues&nbsp;Form Status is now active for the following employee:</span></p>";
            mm.Body += "<p><span style='font-family: verdana, sans-serif;'>1. Employee Code: "+ txtemployeecode.Text + "</span></p>";
            mm.Body += "<p><span style='font-family: verdana, sans-serif;'>2. Employee Name: " + txtnameofemployee.Text + "</span></p>";
            mm.Body += "<p><span style='font-family: verdana, sans-serif;'>3. Designation:" + hdfDesignation.Value + "</span></p>";
            mm.Body += "<p><span style='font-family: verdana, sans-serif;'>4. Department: " + txtcollegedeptsection.Text + "</span></p>";
            mm.Body += "<p><span style='font-family: verdana, sans-serif;'>5. Date of Joining: " + txtdateofjoining.Text + "</span></p>";
            mm.Body += "<p><span style='font-family: verdana, sans-serif;'>6. Date of Leaving: " + txtdateofleaving.Text + "</span></p>";
            mm.Body += "<p><span style='font-family: verdana, sans-serif;'>&nbsp;</span></p>";
            mm.Body += "<p>Kindly verify that nothing is Outstanding (No Dues Granted/ Pending/ Contact to concerned department from which No Dues Status is pending).</p>";
            mm.Body += "<p>&nbsp;</p>";
            mm.Body += "<p>**** This is a system generated mail, please do not reply****</p>";
            mm.Body += "<p>&nbsp;</p>";
            mm.Body += "<p>By User ID: TMUNAVERP/PRASHANT</p>";
            mm.Body += "<p>Regards,</p>";
            mm.Body += "<p>TEAM- HR</p>";
            mm.Body += "</div></HTML>";

            mm.IsBodyHtml = true;
            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Host = smtpSection.Network.Host;
                smtp.EnableSsl = smtpSection.Network.EnableSsl;
                NetworkCredential networkCred = new NetworkCredential("naverp@tmu.ac.in", "nwar yzam bcez rqop");
                smtp.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                smtp.Credentials = networkCred;
                smtp.Port = smtpSection.Network.Port;
                smtp.Send(mm);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Email sent.');", true);
            }
        }
    }

  
}
