using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_itindent : System.Web.UI.Page
{
    Connection con;
    ServicePoratal PortalCon;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    //SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["IndentEntry_DHIT"].ToString() == "1")
            {





                txtIssueDate.Text = System.DateTime.Now.ToString("dd MMM yyyy");
                txtIssueUserid.Text = Session["uname"].ToString();
                hfIssueid.Value = Session["uid"].ToString();

                if (con1 != null && con1.State == ConnectionState.Closed)
                {
                    con1.Open();
                }

                SqlCommand sqlCmd = new SqlCommand("select No_,[First Name] from  [TMU$Employee] where [No_]='" + Session["IndentApproval_DHIT"] + "'", con1);
                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
                DataTable dt1 = new DataTable();
                sqlDa.Fill(dt1);





                txtapprovalid.Text = dt1.Rows[0]["First Name"].ToString();
                hfApprovalId.Value = Session["IndentApproval_DHIT"].ToString();




                if (!IsPostBack)
                {

                    txtIndentno.Text = "";
                    if (Session["uid"].ToString() == "TMU07311")
                    {
                        ddIssueFor.Items.Add(new ListItem("--SELECT--", "0"));
                        ddIssueFor.Items.Add(new ListItem("Department", "1"));
                        ddIssueFor.Items.Add(new ListItem("Employee", "2"));
                        ddIssueFor.Items.Add(new ListItem("PTS", "3"));
                    }
                    else
                    {
                        ddIssueFor.Items.Add(new ListItem("--SELECT--", "0"));
                        ddIssueFor.Items.Add(new ListItem("Department", "1"));
                        ddIssueFor.Items.Add(new ListItem("Employee", "2"));
                        
                    }
                    // ShowIndentNo();  //by ashu 20-12-2016
                    showitem();
                }


                //else
                //{

                //    Response.Redirect("../Default.aspx");
                //}
            }
        }



        catch (Exception)
        {
            Response.Redirect("../Default.aspx");
        }
    }


    protected void ddIssueFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        showDepartmentoremployee();
    }

    public SqlDataReader SHow_indentDepartment(string companyName)
    {
        if (con1 != null && con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }

        string s = "select  Name as Description, [Code] , Name  from " + companyName + " where Name!='' and [Dimension Code]='DEPARTMENT' order by Name ";
        SqlCommand cmd = new SqlCommand(s, con1);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public SqlDataReader Show_Departmentdata(string Employeeid)
    {
        if (con1 != null && con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }

        SqlCommand cmd = new SqlCommand("select  Description, [Code]  from HRMSPortal.dbo.tbl_MultipleDepartmentforIndent where Employeeid='" + Employeeid + "' order by [Code]", con1);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public void showDepartmentoremployee()
    {
        con = new Connection();
        PortalCon = new ServicePoratal();
        string ccname = "TMU Advertisement";
        string rccname = ccname.Replace(".", "_");

        string tblEmployee = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Employee" + "]";
        string tblDepartment = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Dimension Value" + "]";
        if (ddIssueFor.SelectedValue.Trim() == "1")
        {


            if (Session["IndentApproval_Dept_DH"].ToString() == "")
            {

                ddIssueid.Items.Clear();
                SqlDataReader dr = SHow_indentDepartment(tblDepartment);
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
                SqlDataReader dr = Show_Departmentdata(Session["uid"].ToString());
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

        if (ddIssueFor.SelectedValue.Trim() == "2")
        {

            ddIssueid.Items.Clear();
            SqlDataReader dr = SHow_indentEmployee1(tblEmployee, Session["uid"].ToString());
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

        if (ddIssueFor.SelectedValue.Trim() == "3")
        {

            ddIssueid.Items.Clear();
            SqlDataReader dr = SHow_indentEmployeePTS(tblEmployee, Session["uid"].ToString());
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


        if (ddIssueFor.SelectedValue.Trim() == "")
        {
            ddIssueid.Items.Clear();
            txtIssueName.Text = "";
            txtDepartmentCode.Text = "";
            txtDepartmentName.Text = "";
        }


    }

    protected void ddIssueid_SelectedIndexChanged(object sender, EventArgs e)
    {
        string txtIssueName1 = ddIssueid.SelectedItem.Text.Replace(ddIssueid.SelectedValue, "");
        txtIssueName.Text = txtIssueName1.Trim();
        txtDepartmentCode.Text = ddIssueid.SelectedValue.Trim();
        txtDepartmentName.Text = txtIssueName1.Trim();
    }
    public void UpdatenoSeries()
    {
        ShowIndentNoUpdate();
        string ccname = "TMU Advertisement";
        string rccname = ccname.Replace(".", "_");

        string tblnoseriesline = "[NAAC_ADV_TEST].dbo.[" + rccname + "$No_ Series Line" + "]";

        string years = DateTime.Now.ToString("yyyy");
        int yearsi = Convert.ToInt32(years);
        string nextyear = (yearsi + 1).ToString();
        string startdate = years + "-04-01";
        string enddate = nextyear + "-04-01";
        if (Convert.ToInt32((DateTime.Now.ToString("MM"))) <= 3)
        {
            startdate = Convert.ToInt16(years) - 1 + "-04-01";
            enddate = Convert.ToInt16(nextyear) - 1 + "-04-01";
        }



        con = new Connection();
        con.UpdateINoSeriesAuto(tblnoseriesline, txtOldIndent.Text.Trim(), startdate, enddate);
        con.DisConnect();
    }

    protected void ddItemNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowItemdetails(ddItemNo.SelectedValue.Trim());
    }
    public void ShowItemdetails(string Itemcode)
    {
        con = new Connection();
        string ccname = "TMU Advertisement";
        string rccname = ccname.Replace(".", "_");

        string tblItem = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Item" + "]";
        SqlDataReader dr = SHow_indentItemCode1(tblItem, Itemcode.Trim());
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

    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("IndentForIt.aspx");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtQuantityforRequistion.Text == "0")
        {
            string message1 = "Please Enter Quantity Min 1.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "No")
        {
        }
        else
        {

            string ccname = "TMU Advertisement";
            string rccname = ccname.Replace(".", "_");
            string tblIndentHeader = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Indent Header" + "]";
            string tblIndentLine = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Indent Line" + "]";
            con = new Connection();
            string issuename = txtIssueName.Text.Trim();
            SqlDataReader dr = SHow_IndentHeaderdataforduplicaet1(tblIndentHeader, txtIndentno.Text.Trim());
            dr.Read();

            dr.Close();
            con.DisConnect();
            if (txtIndentno.Text == "")
            {
                ShowIndentNo();
                Insert_IndentHeader1(tblIndentHeader, txtIndentno.Text.Trim(), ddIssueFor.SelectedValue.Trim(), "", "", "", lblAcademicyrs.Text.Trim(), txtIssueDate.Text.Trim(), Session["uid"].ToString().Trim(), "ITMIND", "", "", "0", "0", "0", ddIssueid.SelectedValue.Trim(), issuename, "0", "0", Session["uid"].ToString().Trim(), Session["hod_ID_Leave1"].ToString(), "", Session["GlobalDimension1Coded"].ToString());
                con.DisConnect();
                if (con.ValidateIndentHeaderNo_Exist_Or_Not(tblIndentHeader, txtIndentno.Text.Trim(), Session["uid"].ToString().Trim()) != "")//24-May-2017 Ashu
                {
                    UpdatenoSeries();
                }
            }
            else
            {
                if (con.ValidateIndentHeaderNo_Exist_Or_Not(tblIndentHeader, txtIndentno.Text.Trim(), Session["uid"].ToString().Trim()) == "")//24-May-2017 Ashu
                {
                    Response.Redirect("../Default.aspx");
                }
            }





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
                    Insert_IndentLine1(tblIndentLine, txtIndentno.Text.Trim(), ddIssueid.SelectedValue.Trim(), ddIssueFor.SelectedValue.Trim(), ddItemNo.SelectedValue.Trim(), txtIssueName.Text.Trim().Replace("'", " "), txtDescription.Text.Trim().Replace("'", " "), "0.00", txtQuantityforRequistion.Text.Trim(), "0", "0", "1753-01-01 00:00:00.000", "0", release, selectedd.Trim(), "0", "0", "", txtUnitofMeasure.Text.Trim(), "2", "", txtQuantityforRequistion.Text.Trim(), Session["uid"].ToString().Trim(), "", "1753-01-01 00:00:00.000", genPostinggroup, txtQuantityforRequistion.Text.Trim(), "", genPostinggroup, GenBusPostingGroup, "0", "0", Session["GlobalDimension1Coded"].ToString(), txtUserRemark.Text);
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


            ShowIndentLinedata();
        }

    }
    public void ShowIndentNo()
    {

        ShowAcademicyrsNew();       // ShowAcademicyrs();
        con = new Connection();

        string ccname = "TMU Advertisement";
        string rccname = ccname.Replace(".", "_");
        string tblnoseriesline = "[NAAC_ADV_TEST].dbo.[" + rccname + "$No_ Series Line" + "]";
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

            string indetnoser = "ITMINID" + "/" + lblAcademicyrs.Text.Trim() + "/";
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
    public void showitem()
    {
        //SHow_indentItem();
        con = new Connection();
        string ccname = "TMU Advertisement";
        string rccname = ccname.Replace(".", "_");

        string tblItem = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Item" + "]";
        ddItemNo.Items.Clear();
        SqlDataReader dr = SHow_indentItem1(tblItem);
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




    public void ShowIndentLinedata()
    {
        con = new Connection();
        string ccname = "TMU Advertisement";
        string rccname = ccname.Replace(".", "_");

        string tblIndentLine = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Indent Line" + "]";
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
    public void ShowAcademicyrsNew()// added on 03 april 2018 by ashu
    {
        con = new Connection();
        string ccname = "TMU Advertisement";
        string rccname = ccname.Replace(".", "_");
        string tblNoSeriesLine = "[NAAC_ADV_TEST].dbo.[" + rccname + "$No_ Series Line" + "]";
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
    public void ShowIndentNoUpdate()
    {
        ShowAcademicyrsNew();// ShowAcademicyrs();
        con = new Connection();

        string ccname = "TMU Advertisement";
        string rccname = ccname.Replace(".", "_");


        string tblnoseriesline = "[NAAC_ADV_TEST].dbo.[" + rccname + "$No_ Series Line" + "]";

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

    protected void btnDeleted_Command(object sender, CommandEventArgs e)
    {
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "No")
        {
        }
        else
        {
            string lineno = e.CommandArgument.ToString();
            string ccname = "TMU Advertisement";
            string rccname = ccname.Replace(".", "_");

            string tblIndentLine = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Indent Line" + "]";
            string tblIndentHeader = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Indent Header" + "]";
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
    }

    protected void btnSendforApproval_Click(object sender, EventArgs e)
    {
        string ccname = "TMU Advertisement";
        string rccname = ccname.Replace(".", "_");
        string tblIndentLine = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Indent Line" + "]";
        string tblIndentHeader = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Indent Header" + "]";
        con = new Connection();
        con.UpdateSendForApprovalHeader(tblIndentHeader, txtIndentno.Text.Trim(), "1");
        con.DisConnect();
        //con.UpdateSendForApprovalLine(tblIndentLine, txtIndentno.Text.Trim(), "1");
        //con.DisConnect();
        ddIssueFor.Enabled = true;
        ddIssueid.Enabled = true;
        showitem();
        txtQuantityforRequistion.Text = "0";
        //txtInventory.Text = "0";// comment by ashu on 16-12-2016
        ddIssueFor.SelectedValue = "0";
        ddIssueid.SelectedValue = "";
        txtIssueName.Text = "";
        txtDepartmentCode.Text = "";
        txtDepartmentName.Text = "";
        txtIndentno.Text = "";
        // ShowIndentNo();
        ShowIndentLinedata();
    }
    public SqlDataReader SHow_IndentHeaderdataforduplicaet1(string companyName, string No_)
    {
        if (con1 != null && con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }

        string s = "select * from  [NAAC_ADV_TEST].dbo.[TMU Advertisement$Item] where [No_]='" + No_ + "'";
        SqlCommand cmd = new SqlCommand(s, con1);

        SqlDataReader dr = cmd.ExecuteReader();

        return dr;

    }
    public SqlDataReader SHow_indentItemCode1(string companyName, string Code)
    {
        if (con1 != null && con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }
        string s = "select Description ,[No_],[Base Unit of Measure],[Gen_ Prod_ Posting Group] from [NAAC_ADV_TEST].dbo.[TMU Advertisement$Item] where [No_]='" + Code + "'";

        SqlCommand cmd = new SqlCommand(s, con1);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public SqlDataReader SHow_indentEmployee1(string companyName, string LoginId)
    {
        if (con1 != null && con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }


        string s = "select ([No_] + ' ' +[First Name] + ' ' + [Middle Name] + ' ' + [Last Name]) as Name, [No_] as Employeeid,[First Name]  from [NAAC_ADV_TEST].dbo.[TMU Advertisement$Employee]  where [Global Dimension 2 Code] in (SELECT Item from dbo.SplitString(replace(((SELECT  distinct STUFF((SELECT ', ' + t2.[Field Filter] FROM [TMU$Table Filter] t2 WHERE t.[Table Name] = t2.[Table Name] FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') as Val FROM (SELECT DISTINCT [Field Filter],[Table Name] FROM [TMU$Table Filter]) t where [Table Name]='" + LoginId + "')),',','|'),'|') union select  [Global Dimension 2 Code] from [NAAC_ADV_TEST].dbo.[TMU Advertisement$Employee] where No_='" + LoginId + "') and Status=0";

        SqlCommand cmd = new SqlCommand(s, con1);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader SHow_indentEmployeePTS(string companyName, string LoginId)
    {
        if (con1 != null && con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }


        string s = "select ([No_] + ' ' +[First Name] + ' ' + [Middle Name] + ' ' + [Last Name]) as Name, [No_] as Employeeid,[First Name]  from [NAAC_ADV_TEST].dbo.[TMU Advertisement$Employee PTS]  where Status=0";

        SqlCommand cmd = new SqlCommand(s, con1);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public SqlDataReader SHow_indentItem1(string companyName)
    {
        if (con1 != null && con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }

        string s = "select Description as Name ,[No_] as Itemcode ,[Inventory Posting Group] as [Inventory Posting Group]  from [NAAC_ADV_TEST].dbo.[TMU Advertisement$Item] where[Inventory Posting Group] = 'IT ASSETS'";

        SqlCommand cmd = new SqlCommand(s, con1);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public void Insert_IndentHeader1(string tbleName, string No, string IssueFor, string Course, string Semister, string Section, string AcademicYear, string IssueDate, string UserId, string NoSeries, string ItemNo, string Description, string SameItem, string Status, string issuedall, string IssueId, string IssueName, string PostedIndent, string IssueAllItem, string EmployeeID, string ApprovalID, string Remarks, string collegecode)
    {
        if (con1 != null && con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }

        string sqlq = "insert into [NAAC_ADV_TEST].dbo.[TMU Advertisement$Indent Header](No_,[Issue For],[Academic Year],[Issue Date],[User Id],[No_ Series], Status,[Issued All],[Issue Id],[Issue Name],[Posted Indent],[Issue All Item],[Employee ID],[Approval ID],[Remarks- User],[College Code],[Reject Remarks],[User Id Name],[Approval ID Name],[Indent Close Date Time],[Remarks- HOD],[Remarks- Management],[Remarks- IT],[Approved Date Time- IT],[Approved Date Time- Management],[Approved Date Time- HOD],[Remark Date-IT],[Remark USERID-IT])values('" + No + "', '" + IssueFor + "', '" + AcademicYear + "','" + IssueDate + "', '" + UserId + "', '" + NoSeries + "', '" + Status + "','" + issuedall + "', '" + IssueId + "' , '" + IssueName + "', '" + PostedIndent + "', '" + IssueAllItem + "', '" + EmployeeID + "', '" + hfApprovalId.Value + "', '','" + collegecode + "', '','" + txtIssueUserid.Text + "', '" + txtapprovalid.Text + "',   '', '', '', '', '', '','','','')";



        SqlCommand cmd = new SqlCommand(sqlq, con1);
        cmd.ExecuteNonQuery();

    }
    public void Insert_IndentLine1(string tbleName, string DocumentNo, string No, string IndentFor, string ItemNo, string Name, string Description, string UnitPrice, string Quantity, string SerialNo, string LineAmount, string IssueDate, string IssueIndent, string Release, string Selects, string Cancel, string IndentStatus, string Location, string UnitofMeasure, string Types, string VarientCode, string Rem_Qty, string Userid, string Purpose, string PostingDate, string Gen_Prod_PostingGroup, string IssueQty, string Remarks, string ProductSubGroupCode, string Gen_BusPostingGroup, string PostedIndent, string IssuedQty, string CollegeCode, string UserRemark)
    {
        if (con1 != null && con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }

        string sqlq = "insert into " + tbleName + " ([Document No],[No_],[Indent For],[Item No],Name,Description,[Unit Price],Quantity,[Serial No],[Line Amount],[Issue Date],[Issue Indent],Release,[Select],Cancel,[Indent Status],Location,[Unit of Measure],Type,[Varient Code],Rem_Qty,[User id],Purpose,[Posting Date],[Gen_ Prod_ Posting Group],[Issue Qty],Remarks,[Product Sub Group Code],[Gen_Bus Posting Group],[Posted Indent],[Issued Qty],[Total Amount],[Chart of Accounts],[Update Chart of Accounts],[College Code],[ITEM TYPE],[FA Close Issued Qty],[FA Issued Qty],[User Remark],[Location_Room no_],Status,	[Old Fixed Asset No],	[Old FA Serial No],	[Old FA Purchase Date],[Management Appr_ QTY],[HOD Appr_ QTY],[Working Inventory]) values('" + DocumentNo + "','" + No + "','" + IndentFor + "','" + ItemNo + "' ,'" + Name + "','" + Description + "','" + UnitPrice + "','" + Quantity + "', '" + SerialNo + "','" + LineAmount + "', '" + IssueDate + "','" + IssueIndent + "','" + Release + "','" + Selects + "','" + Cancel + "','" + IndentStatus + "','STORE-IT','" + UnitofMeasure + "','" + Types + "','" + VarientCode + "',0,'" + Userid + "','" + Purpose + "', '" + PostingDate + "','" + Gen_Prod_PostingGroup + "',0,'" + Remarks + "','" + ProductSubGroupCode + "','" + Gen_BusPostingGroup + "','" + PostedIndent + "','" + IssuedQty + "',0,0,0,'" + CollegeCode + "',((select [Item Type] from [NAAC_ADV_TEST].dbo.[TMU Advertisement$Item] where No_='" + ItemNo + "')),'','','" + UserRemark + "','',0,'','','',0,0,0)";

        SqlCommand cmd = new SqlCommand(sqlq, con1);
        cmd.ExecuteNonQuery();

    }

}