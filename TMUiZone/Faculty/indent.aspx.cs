using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class Faculty_Requisition : System.Web.UI.Page
{
    Connection con;
    ServicePoratal PortalCon;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["IndentEntry_DH"].ToString() == "1")
            {

                txtIssueDate.Text = System.DateTime.Now.ToString("dd MMM yyyy");
                txtIssueUserid.Text = Session["uid"].ToString();
                if (!IsPostBack)
                {
                    txtIndentno.Text = "";
                  // ShowIndentNo();  //by ashu 20-12-2016
                    showitem();
                }
            }
            else
            {

                Response.Redirect("../Default.aspx");
            }
        }
        catch (Exception)
        {
            Response.Redirect("../Default.aspx");
        }
    }
    public void ShowIndentNo()
    {

         ShowAcademicyrsNew();       // ShowAcademicyrs();
        con = new Connection();
       
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");       
         string tblnoseriesline = "[" + rccname + "$No_ Series Line" + "]";
       //string years=DateTime.Now.ToString("yyyy");
       // int yearsi=Convert.ToInt32(years);
       // string nextyear=(yearsi+1).ToString();

       // string startdate = years + "-04-01";
        


       // string enddate=nextyear + "-04-01";

        SqlDataReader dr = con.SHow_NoseriesLast(tblnoseriesline);
        dr.Read();
        if (dr.HasRows)
        {
            txtIndentno.Text = dr["Last No_ Used"].ToString();
            
            string indetnoser="ITMINID"+"/"+lblAcademicyrs.Text.Trim()+"/";
            string removeindetno = txtIndentno.Text.Replace(indetnoser, "");
            int removeindetno1 = Convert.ToInt32(removeindetno.Trim());
            string nogser = (removeindetno1 + 1).ToString();
            if (nogser.Length == 1)
            {
                nogser = "0000" + nogser;
            }
            if (nogser.Length == 2)
            {
                nogser = "000" + nogser;
            }
            if (nogser.Length == 3)
            {
                nogser = "00" + nogser;
            }
            if (nogser.Length == 4)
            {
                nogser = "0" + nogser;
            }
            if (nogser.Length == 5)
            {
                nogser = nogser.ToString();
            }
            txtIndentno.Text = "ITMINID" + "/" + lblAcademicyrs.Text.Trim() + "/" + nogser;

            dr.Close();
            con.DisConnect();
        }
        else
        {
            dr.Close();
            con.DisConnect();



            SqlDataReader drw = con.SHow_NoseriesLast(tblnoseriesline);
            drw.Read();
            if (drw.HasRows)
            {
                txtIndentno.Text = drw["Last No_ Used"].ToString();
                drw.Close();
                con.DisConnect();
            }
            else
            {
                txtIndentno.Text = "";
                drw.Close();
                con.DisConnect();
            }


        }


    }


    public void ShowIndentNoUpdate()
    {
        ShowAcademicyrsNew();// ShowAcademicyrs();
        con = new Connection();

        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");


        string tblnoseriesline = "[" + rccname + "$No_ Series Line" + "]";

        //string years = DateTime.Now.ToString("yyyy");
        //int yearsi = Convert.ToInt32(years);
        //string nextyear = (yearsi + 1).ToString();

        //string startdate = years + "-04-01";
        //string enddate = nextyear + "-04-01";

        SqlDataReader dr = con.SHow_NoseriesLast(tblnoseriesline);
        dr.Read();
        if (dr.HasRows)
        {
            txtOldIndent.Text = dr["Last No_ Used"].ToString();
            string indetnoser = "ITMINID" + "/" + lblAcademicyrs.Text.Trim() + "/";
            string removeindetno = txtOldIndent.Text.Replace(indetnoser, "");
            int removeindetno1 = Convert.ToInt32(removeindetno.Trim());
            string nogser = (removeindetno1 + 1).ToString();
            if (nogser.Length == 1)
            {
                nogser = "0000" + nogser;
            }
            if (nogser.Length == 2)
            {
                nogser = "000" + nogser;
            }
            if (nogser.Length == 3)
            {
                nogser = "00" + nogser;
            }
            if (nogser.Length == 4)
            {
                nogser = "0" + nogser;
            }
            if (nogser.Length == 5)
            {
                nogser = nogser.ToString();
            }
            txtOldIndent.Text = "ITMINID" + "/" + lblAcademicyrs.Text.Trim() + "/" + nogser;
            dr.Close();
            con.DisConnect();
        }
        else
        {
            dr.Close();
            con.DisConnect();



            SqlDataReader drw = con.SHow_NoseriesLast(tblnoseriesline);
            drw.Read();
            if (drw.HasRows)
            {
                txtOldIndent.Text = drw["Last No_ Used"].ToString();
                drw.Close();
                con.DisConnect();
            }
            else
            {
                txtOldIndent.Text = "";
                drw.Close();
                con.DisConnect();
            }


        }


    }

    public void showDepartmentoremployee()
    {
        con = new Connection();
        PortalCon = new ServicePoratal();
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
       
        string tblEmployee = "[" + rccname + "$Employee" + "]";
        string tblDepartment = "[" + rccname + "$Dimension Value" + "]";
        if (ddIssueFor.SelectedValue.Trim() == "1")
        {


            if (Session["IndentApproval_Dept_DH"].ToString() == "")
            {

                ddIssueid.Items.Clear();
                SqlDataReader dr = con.SHow_indentDepartment(tblDepartment);
                ddIssueid.DataSource = dr;
                ddIssueid.DataTextField = "Description";
                ddIssueid.DataValueField = "Code";
                ddIssueid.DataBind();
                dr.Close();
                PortalCon.DisConnect();
                ddIssueid.Items.Insert(0, new ListItem("", ""));
                txtIssueName.Text = "";
                txtDepartmentCode.Text = "";
                txtDepartmentName.Text = "";
            }
            else
            {
                PortalCon.delete_tbl_MultipleDepartmentforIndent(Session["uid"].ToString());
                PortalCon.DisConnect();
                string[] multi = Session["IndentApproval_Dept_DH"].ToString().Split('|');
                foreach (string multiTo in multi)
                {
                    string multiTo1 = multiTo;

                    SqlDataReader drs = con.SHow_DepartmentNamess(tblDepartment, multiTo1);
                    drs.Read();
                    if (drs.HasRows)
                    {

                        string Descriptionss = drs["Name"].ToString();
                        drs.Close();
                        con.DisConnect();
                        PortalCon.Insert_tbl_MultipleDepartmentforIndent(multiTo1, Descriptionss, Session["uid"].ToString());
                        con.DisConnect();
                    }
                    else
                    {
                        drs.Close();
                        con.DisConnect();
                    }


                }

                ddIssueid.Items.Clear();
                SqlDataReader dr = PortalCon.Show_Departmentdata(Session["uid"].ToString());
                ddIssueid.DataSource = dr;
                ddIssueid.DataTextField = "Description";
                ddIssueid.DataValueField = "Code";
                ddIssueid.DataBind();
                dr.Close();
                PortalCon.DisConnect();
                ddIssueid.Items.Insert(0, new ListItem("", ""));
                txtIssueName.Text = "";
                txtDepartmentCode.Text = "";
                txtDepartmentName.Text = "";
            }
        }
         if(ddIssueFor.SelectedValue.Trim()=="2")
        {

            ddIssueid.Items.Clear();
            SqlDataReader dr = con.SHow_indentEmployee(tblEmployee, Session["uid"].ToString());
            ddIssueid.DataSource = dr;
            ddIssueid.DataTextField = "Name";
            ddIssueid.DataValueField = "Employeeid";
            ddIssueid.DataBind();
            dr.Close();
            con.DisConnect();
            ddIssueid.Items.Insert(0, new ListItem("", ""));

            txtIssueName.Text = "";
            txtDepartmentCode.Text = "";
            txtDepartmentName.Text = "";
         }
        if(ddIssueFor.SelectedValue.Trim()=="")
        {
            ddIssueid.Items.Clear();
            txtIssueName.Text = "";
            txtDepartmentCode.Text = "";
            txtDepartmentName.Text = "";
        }

       
    }

    public void showitem()
    {
        con = new Connection();
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");

        string tblItem = "[" + rccname + "$Item" + "]";
        ddItemNo.Items.Clear();
        SqlDataReader dr = con.SHow_indentItem(tblItem);
        ddItemNo.DataSource = dr;
        ddItemNo.DataTextField = "Name";
        ddItemNo.DataValueField = "Itemcode";
        ddItemNo.DataBind();
        dr.Close();
        con.DisConnect();
        ddItemNo.Items.Insert(0, new ListItem("", ""));

        txtDescription.Text = "";
        txtUnitofMeasure.Text = "";
        
    }


    protected void ddIssueFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        showDepartmentoremployee();
    }
    protected void ddIssueid_SelectedIndexChanged(object sender, EventArgs e)
    {

        string txtIssueName1 = ddIssueid.SelectedItem.Text.Replace(ddIssueid.SelectedValue, "");
        txtIssueName.Text = txtIssueName1.Trim();
        txtDepartmentCode.Text = ddIssueid.SelectedValue.Trim();
        txtDepartmentName.Text = txtIssueName1.Trim();
    }

    public void ShowAcademicyrs()
    { 
        con= new Connection();
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        string tblEducationSetup = "[" + rccname + "$Education Setup" + "]";
    SqlDataReader dr=con.SHow_CurrentAcademicyrs(tblEducationSetup,System.DateTime.Now.ToString("yyyy-MM-dd"));
    dr.Read();
    if (dr.HasRows)
    {
       lblAcademicyrs.Text = dr["Academic Year"].ToString();

        lblAcademicyrs.Text = "17-18";
    }
    dr.Close();
    con.DisConnect();
    }

    public void ShowAcademicyrsNew()// added on 03 april 2018 by ashu
    {
        con = new Connection();
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        string tblNoSeriesLine = "[" + rccname + "$No_ Series Line" + "]";
        SqlDataReader dr = con.SHow_NoseriesLast(tblNoSeriesLine);
        dr.Read();
        if (dr.HasRows)
        {
            string Noseries = dr["Starting No_"].ToString();
            string[] lines = Regex.Split(Noseries, "/");
            lblAcademicyrs.Text = lines[1];
           // lblAcademicyrs.Text = dr["Academic Year"].ToString();

           // lblAcademicyrs.Text = "17-18";
        }
        dr.Close();
        con.DisConnect();
    }

    protected void ddItemNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowItemdetails(ddItemNo.SelectedValue.Trim());
    }

    public void ShowItemdetails(string Itemcode)
    {
        con = new Connection();
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");

        string tblItem = "[" + rccname + "$Item" + "]";
        SqlDataReader dr = con.SHow_indentItemCode(tblItem, Itemcode.Trim());
        dr.Read();
        if (dr.HasRows)
        {
            txtDescription.Text = dr["Description"].ToString().Trim();
            txtUnitofMeasure.Text = dr["Base Unit of Measure"].ToString().Trim();
            lblGenpostingGroup.Text = dr["Gen_ Prod_ Posting Group"].ToString();

            //txtVariantCode.Text = dr[""].ToString();
        }
        dr.Close();
        con.DisConnect();
        // SumofInventory(Itemcode); comment by ashu on 16-012-2016
    }
    //public void ValidateQuantity()// comment by ashu on 16-012-2016
    //{
    //    decimal inventorys = Convert.ToDecimal(txtInventory.Text.Trim());
    //    decimal Quantitys = Convert.ToDecimal(txtQuantityforRequistion.Text.Trim());

    //    if (Quantitys > inventorys)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Quantity must be less than or equal from inventory');", true);
    //        txtQuantityforRequistion.Text = "";
    //    }
    //}

    public void UpdatenoSeries()
    {
        ShowIndentNoUpdate();
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        string tblnoseriesline = "[" + rccname + "$No_ Series Line" + "]";

        string years = DateTime.Now.ToString("yyyy");
        int yearsi = Convert.ToInt32(years);
        string nextyear = (yearsi + 1).ToString();
        string startdate = years + "-04-01";
        string enddate = nextyear + "-04-01";
        if (Convert.ToInt32((DateTime.Now.ToString("MM"))) <= 3)
        {
            startdate = Convert.ToInt16(years)-1 + "-04-01";
            enddate =Convert.ToInt16(nextyear)-1 + "-04-01";
        }
       

        
        con = new Connection();
        con.UpdateINoSeriesAuto(tblnoseriesline, txtOldIndent.Text.Trim(), startdate, enddate);
        con.DisConnect();
    }
    public void ValidatenoSeries()
    {
        
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {      
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        string tblIndentHeader = "[" + rccname + "$Indent Header" + "]";
        string tblIndentLine = "[" + rccname + "$Indent Line" + "]";           
        con = new Connection();     
        string issuename=txtIssueName.Text.Trim();
        SqlDataReader dr = con.SHow_IndentHeaderdataforduplicaet(tblIndentHeader,txtIndentno.Text.Trim());
        dr.Read();
        //if (dr.HasRows)
        //{
        //    dr.Close();
        //    con.DisConnect();

        //}
        //else
        //{
            dr.Close();
            con.DisConnect();
            if (txtIndentno.Text == "")
            {
                ShowIndentNo();
                con.Insert_IndentHeader(tblIndentHeader, txtIndentno.Text.Trim(), ddIssueFor.SelectedValue.Trim(), "", "", "", lblAcademicyrs.Text.Trim(), txtIssueDate.Text.Trim(), "", "ITMIND", "", "", "0", "0", "0", ddIssueid.SelectedValue.Trim(), issuename, "0", "0", Session["uid"].ToString().Trim(), Session["IndentApproval_DH"].ToString().Trim(), "");
                con.DisConnect();
                if (con.ValidateIndentHeaderNo_Exist_Or_Not(tblIndentHeader, txtIndentno.Text.Trim(), Session["uid"].ToString().Trim()) != "")//24-May-2017 Ashu
                {
                    UpdatenoSeries();
                }
            }
            else// else condition added by ashu on 24-05-2017
            {
                if (con.ValidateIndentHeaderNo_Exist_Or_Not(tblIndentHeader, txtIndentno.Text.Trim(), Session["uid"].ToString().Trim()) == "")//24-May-2017 Ashu
                {
                    Response.Redirect("../Default.aspx");
                }
            }
       // }


      //  Lineno();

        string release = "0"; string selectedd = "0"; string genPostinggroup = lblGenpostingGroup.Text.Trim().Replace("'", " "); string GenBusPostingGroup = "";

        SqlDataReader drin = con.SHow_IndentLineDuplicatedata(tblIndentLine, txtIndentno.Text.Trim(), ddIssueid.SelectedValue.Trim(), ddIssueFor.SelectedValue.Trim(), ddItemNo.SelectedValue.Trim());
        drin.Read();
        if (drin.HasRows)
        {
            drin.Close();
            con.DisConnect();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please choose different item');", true);
        }
        else
        {
            drin.Close();
            con.DisConnect();
            if (con.ValidateIndentHeaderNo_Exist_Or_Not(tblIndentHeader, txtIndentno.Text.Trim(), Session["uid"].ToString().Trim()) != "")//24-May-2017 Ashu
            {
                con.Insert_IndentLine(tblIndentLine, txtIndentno.Text.Trim(), ddIssueid.SelectedValue.Trim(), ddIssueFor.SelectedValue.Trim(), ddItemNo.SelectedValue.Trim(), txtIssueName.Text.Trim().Replace("'", " "), txtDescription.Text.Trim().Replace("'", " "), "0.00", txtQuantityforRequistion.Text.Trim(), "0", "0", "1753-01-01 00:00:00.000", "0", release, selectedd.Trim(), "0", "0", "", txtUnitofMeasure.Text.Trim(), "2", "", txtQuantityforRequistion.Text.Trim(), "", "", "1753-01-01 00:00:00.000", genPostinggroup, txtQuantityforRequistion.Text.Trim(), "", genPostinggroup, GenBusPostingGroup, "0", "0");
            }
            else
            { 
            
            }
            con.DisConnect();
        }
            ddIssueFor.Enabled = false;
            ddIssueid.Enabled = false;
            showitem();
            txtQuantityforRequistion.Text = "0";
            // txtInventory.Text = "0"; // comment by ashu on 16-12-2016

            ShowIndentLinedata();
        
    }
    string lineno = "";
    public void Lineno()
    {
        con = new Connection();
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");

      
        string tblIndentLine = "[" + rccname + "$Indent Line" + "]";
        SqlDataReader dr = con.SHow_Top1lineno(tblIndentLine);
        dr.Read();
        if (dr.HasRows)
        {
            lineno = dr["Line No_"].ToString();
            int linenoqq = Convert.ToInt32(lineno);
            lineno = (linenoqq + 1).ToString();
        }
        else
        {
            lineno = "1";
        }
        dr.Close();
        con.DisConnect();
    }

    public void ShowIndentLinedata()
    {
        con = new Connection();
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
              
        string tblIndentLine = "[" + rccname + "$Indent Line" + "]";
        SqlDataReader dr = con.SHow_IndentLinedataforduplicaet(tblIndentLine, txtIndentno.Text.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        grdViewIndentLine.DataSource = dt;
        grdViewIndentLine.DataBind();
        dr.Close();
        con.DisConnect();
        SqlDataReader dr1 = con.SHow_IndentLinedataforduplicaet(tblIndentLine, txtIndentno.Text.Trim());
        dr1.Read();
        if (dr1.HasRows)
        {
            dr1.Close();
            con.DisConnect();
            btnSendforApproval.Visible = true;
        }
        else
        {
            dr1.Close();
            con.DisConnect();
            btnSendforApproval.Visible = false;
        }
    }

    //public void SumofInventory(string itemcode) //comment by ashu on 16-012-2016
    //{
    //    con = new Connection();
    //    string ccname = Session["Company"].ToString();
    //    string rccname = ccname.Replace(".", "_");

    //    string tblItemledgerEntry = "[" + rccname + "$Item Ledger Entry" + "]";
    //    SqlDataReader dr = con.SHow_SumOfQuantity(tblItemledgerEntry, itemcode);
    //    dr.Read();
    //    if (dr.HasRows)
    //    {
    //        txtInventory.Text =Convert.ToDecimal( dr["Inventory"].ToString()).ToString("00.00");

    //    }
    //    dr.Close();
    //    con.DisConnect();
    //}
    //protected void txtQuantityforRequistion_TextChanged(object sender, EventArgs e)  // by ashu on 16-12-2016
    //{
    //    ValidateQuantity();
    //}
    protected void btnDeleted_Command(object sender, CommandEventArgs e)
    {
        string lineno = e.CommandArgument.ToString();
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");

        string tblIndentLine = "[" + rccname + "$Indent Line" + "]";
        string tblIndentHeader = "[" + rccname + "$Indent Header" + "]";
        con = new Connection();

        string documentno = "";
        SqlDataReader dr = con.SHow_DocumentNowithLine(tblIndentLine, lineno);
        dr.Read();
        if (dr.HasRows)
        {
            documentno = dr["Document No"].ToString();
            dr.Close();
            con.DisConnect();
        }

        else
        {
            dr.Close();
            con.DisConnect();

        }

        con.DeleteIndentline(tblIndentLine, lineno);
        con.DisConnect();

        SqlDataReader dr1 = con.SHow_DocumentNowithLineofDocument(tblIndentLine, documentno);
        dr1.Read();
        if (dr1.HasRows)
        {
            dr1.Close();
            con.DisConnect();
        }
        else
        {
            dr1.Close();
            con.DisConnect();

            con.DeleteIndentHeader(tblIndentHeader, documentno);
            con.DisConnect();
            ddIssueFor.Enabled = true;
            ddIssueid.Enabled = true;
            showitem();
            txtQuantityforRequistion.Text = "0";
            //  txtInventory.Text = "0"; //Comment by ashu on 16-12-2016
            ddIssueFor.SelectedValue = "";
            ddIssueid.SelectedValue = "";
           
        }

        ShowIndentLinedata();


    }
    protected void btnSendforApproval_Click(object sender, EventArgs e)
    {
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        string tblIndentLine = "[" + rccname + "$Indent Line" + "]";
        string tblIndentHeader = "[" + rccname + "$Indent Header" + "]";
        con = new Connection();
        con.UpdateSendForApprovalHeader(tblIndentHeader,txtIndentno.Text.Trim(),"1");
        con.DisConnect();
        //con.UpdateSendForApprovalLine(tblIndentLine, txtIndentno.Text.Trim(), "1");
        //con.DisConnect();
        ddIssueFor.Enabled = true;
        ddIssueid.Enabled = true;
        showitem();
        txtQuantityforRequistion.Text = "0";
        //txtInventory.Text = "0";// comment by ashu on 16-12-2016
        ddIssueFor.SelectedValue = "";
        ddIssueid.SelectedValue = "";
        txtIssueName.Text = "";
        txtDepartmentCode.Text = "";
        txtDepartmentName.Text = "";
        txtIndentno.Text = "";
       // ShowIndentNo();
        ShowIndentLinedata();
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("Indentview.aspx");
    }
}