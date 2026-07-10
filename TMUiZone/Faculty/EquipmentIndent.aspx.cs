
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Faculty_EquipmentIndent : System.Web.UI.Page
{
    ServicePoratal PortalCon;
    Connection con;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Session["uid"].ToString() != "TMU03798")
            {
                Response.Redirect("Error.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }





            if (Session["IndentEntry_DH"].ToString() == "1")
            {

                if (!IsPostBack)
                {
                    ShowApprovaldata();
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
    protected void ddIssueid_SelectedIndexChanged(object sender, EventArgs e)
    {
        mdlCreateIndent.Show();
        pnlCreateIndent.Visible = true;
        string txtIssueName1 = ddIssueid.SelectedItem.Text.Replace(ddIssueid.SelectedValue, "");
        txtIssueName.Text = txtIssueName1.Trim();
        txtDepartmentCode.Text = ddIssueid.SelectedValue.Trim();
        txtDepartmentName.Text = txtIssueName1.Trim();

    }
    protected void ddIssueFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        mdlCreateIndent.Show();
        pnlCreateIndent.Visible = true;
        showDepartmentoremployee();


    }
    protected void ddItemNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowItemdetails(ddItemNo.SelectedValue.Trim());
        mdlCreateIndent.Show();
        pnlCreateIndent.Visible = true;
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

            decimal value = Convert.ToDecimal(dr["RemainingQty"]);

            txtRemaing.Text = value.ToString("F2"); // ✅ 2 decimal
            //txtVariantCode.Text = dr[""].ToString();
        }
        dr.Close();
        con.DisConnect();
        // SumofInventory(Itemcode); comment by ashu on 16-012-2016
    }
    public SqlDataReader SHow_IndentHeaderdataforduplicaet1(string companyName, string No_)
    {
        if (con1 != null && con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }

        string s = "select * from  [NAAC_ADV_TEST].dbo.[TMU Hospital$Item] where [No_]='" + No_ + "'";
        SqlCommand cmd = new SqlCommand(s, con1);

        SqlDataReader dr = cmd.ExecuteReader();

        return dr;

    }
    public void ShowAcademicyrsNew()// added on 03 april 2018 by ashu
    {
        con = new Connection();
        string ccname = "TMU Hospital";
        string rccname = ccname.Replace(".", "_");
        string tblNoSeriesLine = "[NAAC_ADV_TEST].dbo.[" + rccname + "$No_ Series Line" + "]";
        SqlDataReader dr = SHow_NoseriesLast(tblNoSeriesLine);
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

    public SqlDataReader SHow_NoseriesLast(string companyName)
    {
        if (con1 != null && con1.State != ConnectionState.Open)
        {
            con1.Open();
        }
        string s = "select Top(1)([Starting Date]),* from " + companyName + " where [Series Code]='EQPINDT' order by CONVERT(date,[Starting Date],103) desc";

        SqlCommand cmd = new SqlCommand(s, con1);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public void ShowIndentNo()
    {

        ShowAcademicyrsNew();       // ShowAcademicyrs();
        con = new Connection();

        string ccname = "TMU Hospital";
        string rccname = ccname.Replace(".", "_");
        string tblnoseriesline = "[NAAC_ADV_TEST].dbo.[" + rccname + "$No_ Series Line" + "]";
        //string years=DateTime.Now.ToString("yyyy");
        // int yearsi=Convert.ToInt32(years);
        // string nextyear=(yearsi+1).ToString();

        // string startdate = years + "-04-01";



        // string enddate=nextyear + "-04-01";

        SqlDataReader dr = SHow_NoseriesLast(tblnoseriesline);
        dr.Read();
        if (dr.HasRows)
        {
            txtIndentno.Text = dr["Last No_ Used"].ToString();

            string indetnoser = "EQPINDT" + "/" + lblAcademicyrs.Text.Trim() + "/";
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
            txtIndentno.Text = "EQPINDT" + "/" + lblAcademicyrs.Text.Trim() + "/" + nogser;

            dr.Close();
            con.DisConnect();
        }
        else
        {
            dr.Close();
            con.DisConnect();



            SqlDataReader drw = SHow_NoseriesLast(tblnoseriesline);
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
    public void Insert_IndentHeader1(string tbleName, string No, string IssueFor, string Course, string Semister, string Section, string AcademicYear, string IssueDate, string UserId, string NoSeries, string ItemNo, string Description, string SameItem, string Status, string issuedall, string IssueId, string IssueName, string PostedIndent, string IssueAllItem, string EmployeeID, string ApprovalID, string Remarks, string collegecode)
    {
        if (con1 != null && con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }

        string sqlq = "insert into [NAAC_ADV_TEST].dbo.[TMU Hospital$Equipment Indent Header](No_,[Issue For],[Academic Year],[Issue Date],[User Id],[No_ Series], Status,[Issued All],[Issue Id],[Issue Name],[Posted Indent],[Issue All Item],[Employee ID],[Approval ID],[Remarks- User],[College Code],[Reject Remarks],[User Id Name],[Approval ID Name],[Indent Close Date Time],[Remarks- HOD],[Remarks- Management],[Remarks- IT],[Approved Date Time- IT],[Approved Date Time- Management],[Approved Date Time- HOD],[Remark Date-IT],[Remark USERID-IT])values('" + No + "', '" + IssueFor + "', '" + AcademicYear + "','" + IssueDate + "', '" + UserId + "', '" + NoSeries + "', '" + Status + "','" + issuedall + "', '" + IssueId + "' , '" + IssueName + "', '" + PostedIndent + "', '" + IssueAllItem + "', '" + EmployeeID + "', '" + hfApprovalId.Value + "', '','" + collegecode + "', '','" + txtIssueUserid.Text + "', '" + txtapprovalid.Text + "',   '', '', '', '', '', '','','','')";



        SqlCommand cmd = new SqlCommand(sqlq, con1);
        cmd.ExecuteNonQuery();

    }
    public void ShowIndentNoUpdate()
    {
        ShowAcademicyrsNew();// ShowAcademicyrs();
        con = new Connection();

        string ccname = "TMU Hospital";
        string rccname = ccname.Replace(".", "_");


        string tblnoseriesline = "[NAAC_ADV_TEST].dbo.[" + rccname + "$No_ Series Line" + "]";

        //string years = DateTime.Now.ToString("yyyy");
        //int yearsi = Convert.ToInt32(years);
        //string nextyear = (yearsi + 1).ToString();

        //string startdate = years + "-04-01";
        //string enddate = nextyear + "-04-01";

        SqlDataReader dr = SHow_NoseriesLast(tblnoseriesline);
        dr.Read();
        if (dr.HasRows)
        {
            txtOldIndent.Text = dr["Last No_ Used"].ToString();
            string indetnoser = "EQPINDT" + "/" + lblAcademicyrs.Text.Trim() + "/";
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
            txtOldIndent.Text = "EQPINDT" + "/" + lblAcademicyrs.Text.Trim() + "/" + nogser;
            dr.Close();
            con.DisConnect();
        }
        else
        {
            dr.Close();
            con.DisConnect();



            SqlDataReader drw = SHow_NoseriesLast(tblnoseriesline);
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

    public void UpdateINoSeriesAuto(string tbleName, string LastNo_Used, string fromdate, string todate)
    {
        if (con1 != null && con1.State != ConnectionState.Open)
        {
            con1.Open();
        }
        string sqlq = "update " + tbleName + " set [Last No_ Used]='" + LastNo_Used + "' where [Series Code]='EQPINDT' and CONVERT(date,[Starting Date],103)>='" + fromdate + "' and CONVERT(date,[Starting Date],103)<'" + todate + "' ";

        SqlCommand cmd = new SqlCommand(sqlq, con1);
        cmd.ExecuteNonQuery();

    }

    public void UpdatenoSeries()
    {
        ShowIndentNoUpdate();
        string ccname = "TMU Hospital";
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




        UpdateINoSeriesAuto(tblnoseriesline, txtOldIndent.Text.Trim(), startdate, enddate);

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        decimal remaining = Convert.ToDecimal(txtRemaing.Text);
        decimal reqQty = Convert.ToDecimal(txtQuantityforRequistion.Text);      
        if (remaining < reqQty)
        {
            string message1 = "Quantity can not greater than remaining Qty";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            ShowIndentLinedataforCreate();
            mdlCreateIndent.Show();
            pnlCreateIndent.Visible = true;
            return;
        }


        if (txtQuantityforRequistion.Text == "0")
        {
            string message1 = "Please Enter Quantity Min 1.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            ShowIndentLinedataforCreate();
            mdlCreateIndent.Show();
            pnlCreateIndent.Visible = true;
            return;
        }
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "No")
        {
        }
        else
        {

            string ccname = "TMU Hospital";
            string rccname = ccname.Replace(".", "_");
            string tblIndentHeader = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Equipment Indent Header" + "]";
            string tblIndentLine = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Equipment Indent Line" + "]";
            con = new Connection();
            string issuename = txtIssueName.Text.Trim();
            SqlDataReader dr = SHow_IndentHeaderdataforduplicaet1(tblIndentHeader, txtIndentno.Text.Trim());
            dr.Read();

            dr.Close();
            con.DisConnect();
            if (txtIndentno.Text == "")
            {
                ShowIndentNo();
                Insert_IndentHeader1(tblIndentHeader, txtIndentno.Text.Trim(), ddIssueFor.SelectedValue.Trim(), "", "", "", lblAcademicyrs.Text.Trim(), txtIssueDate.Text.Trim(), Session["uid"].ToString().Trim(), "EQPINDT", "", "", "0", "0", "0", ddIssueid.SelectedValue.Trim(), issuename, "0", "0", Session["uid"].ToString().Trim(), Session["hod_ID_Leave1"].ToString(), "", Session["GlobalDimension1Coded"].ToString());
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

            SqlDataReader drin = SHow_IndentLineDuplicatedata(tblIndentLine, txtIndentno.Text.Trim(), ddIssueid.SelectedValue.Trim(), ddIssueFor.SelectedValue.Trim(), ddItemNo.SelectedValue.Trim());
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


            ShowIndentLinedataforCreate();
            mdlCreateIndent.Show();
            pnlCreateIndent.Visible = true;
        }

    }
    public SqlDataReader SHow_IndentLineDuplicatedata(string companyName, string DocumentNo, string no, string indentfor, string itemno)
    {
        if (con1 != null && con1.State != ConnectionState.Open)
        {
            con1.Open();
        }
        string s = "select * from " + companyName + " where  [Document No]='" + DocumentNo + "' and [No_]='" + no + "' and  [Indent For]='" + indentfor + "' and [Item No]='" + itemno + "'";

        SqlCommand cmd = new SqlCommand(s, con1);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public void ShowIndentLinedataforCreate()
    {
        con = new Connection();
        string ccname = "TMU Hospital";
        string rccname = ccname.Replace(".", "_");

        string tblIndentLine = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Equipment Indent Line" + "]";
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
    public void ShowIndentLinedata()
    {
        con = new Connection();
        string ccname = "TMU Hospital";
        string rccname = ccname.Replace(".", "_");

        string tblIndentLine = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Equipment Indent Line" + "]";
        SqlDataReader dr = con.SHow_IndentLinedataforduplicaet(tblIndentLine, txtIndentno.Text.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);

        grdViewIndentLine_1.DataSource = dt;
        grdViewIndentLine_1.DataBind();
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
    protected void btnSendforApproval_Click(object sender, EventArgs e)
    {
        string ccname = "TMU Hospital";
        string rccname = ccname.Replace(".", "_");
        string tblIndentLine = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Equipment Indent Line" + "]";
        string tblIndentHeader = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Equipment Indent Header" + "]";
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
        ddIssueFor.SelectedValue = "";
        ddIssueid.SelectedValue = "";
        txtIssueName.Text = "";
        txtDepartmentCode.Text = "";
        txtDepartmentName.Text = "";
        txtIndentno.Text = "";
        // ShowIndentNo();
        ShowIndentLinedata();
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
            string ccname = "TMU Hospital";
            string rccname = ccname.Replace(".", "_");

            string tblIndentLine = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Equipment Indent Line" + "]";
            string tblIndentHeader = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Equipment Indent Header" + "]";
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


            ShowLinedata(documentno);
            pnlCreateIndent.Visible = true;
            mdIndentLine.Show();

        }
    }
    public SqlDataReader SHow_indentItem1(string companyName)
    {
        if (con1 != null && con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }

        string s = "SELECT  I.Description AS Name,  I.[No_] AS Itemcode,  I.[Inventory Posting Group],  SUM(L.[Remaining Quantity]) AS RemainingQty FROM  [NAAC_ADV_TEST].dbo.[TMU Hospital$Item] I INNER JOIN  [NAAC_ADV_TEST].dbo.[TMU Hospital$Item Ledger Entry] L  ON I.[No_] = L.[Item No_]   GROUP BY  I.Description, I.[No_], I.[Inventory Posting Group] HAVING SUM(L.[Remaining Quantity]) > 0";

        SqlCommand cmd = new SqlCommand(s, con1);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public void showitem()
    {
        //SHow_indentItem();
        con = new Connection();
        string ccname = "TMU Hospital";
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
    public void Insert_IndentLine1(string tbleName, string DocumentNo, string No, string IndentFor, string ItemNo, string Name, string Description, string UnitPrice, string Quantity, string SerialNo, string LineAmount, string IssueDate, string IssueIndent, string Release, string Selects, string Cancel, string IndentStatus, string Location, string UnitofMeasure, string Types, string VarientCode, string Rem_Qty, string Userid, string Purpose, string PostingDate, string Gen_Prod_PostingGroup, string IssueQty, string Remarks, string ProductSubGroupCode, string Gen_BusPostingGroup, string PostedIndent, string IssuedQty, string CollegeCode, string UserRemark)
    {
        if (con1 != null && con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }

        string sqlq = "insert into " + tbleName + " ([Document No],[No_],[Indent For],[Item No],Name,Description,[Unit Price],Quantity,[Serial No],[Line Amount],[Issue Date],[Issue Indent],Release,[Select],Cancel,[Indent Status],Location,[Unit of Measure],Type,[Varient Code],Rem_Qty,[User id],Purpose,[Posting Date],[Gen_ Prod_ Posting Group],[Issue Qty],Remarks,[Product Sub Group Code],[Gen_Bus Posting Group],[Posted Indent],[Issued Qty],[Total Amount],[Chart of Accounts],[Update Chart of Accounts],[College Code],[ITEM TYPE],[FA Close Issued Qty],[FA Issued Qty],[User Remark],[Location_Room no_],Status,	[Old Fixed Asset No],	[Old FA Serial No],	[Old FA Purchase Date],[Management Appr_ QTY],[HOD Appr_ QTY],[Working Inventory]) values('" + DocumentNo + "','" + No + "','" + IndentFor + "','" + ItemNo + "' ,'" + Name + "','" + Description + "','" + UnitPrice + "','" + Quantity + "', '" + SerialNo + "','" + LineAmount + "', '" + IssueDate + "','" + IssueIndent + "','" + Release + "','" + Selects + "','" + Cancel + "','" + IndentStatus + "','STORE-IT','" + UnitofMeasure + "','" + Types + "','" + VarientCode + "',0,'" + Userid + "','" + Purpose + "', '" + PostingDate + "','" + Gen_Prod_PostingGroup + "',0,'" + Remarks + "','" + ProductSubGroupCode + "','" + Gen_BusPostingGroup + "','" + PostedIndent + "','" + IssuedQty + "',0,0,0,'" + CollegeCode + "',((select [Item Type] from [NAAC_ADV_TEST].dbo.[TMU Hospital$Item] where No_='" + ItemNo + "')),'','','" + UserRemark + "','',0,'','','',0,'" + Quantity + "',0)";

        SqlCommand cmd = new SqlCommand(sqlq, con1);
        cmd.ExecuteNonQuery();

    }

    public SqlDataReader SHow_indentItemCode1(string companyName, string Code)
    {
        if (con1 != null && con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }
        string s = "SELECT  I.Description ,[No_],[Base Unit of Measure],[Gen_ Prod_ Posting Group],  SUM(L.[Remaining Quantity]) AS RemainingQty FROM  [NAAC_ADV_TEST].dbo.[TMU Hospital$Item] I INNER JOIN  [NAAC_ADV_TEST].dbo.[TMU Hospital$Item Ledger Entry] L  ON I.[No_] = L.[Item No_] WHERE    [Item Category]!='ACCESSORIES' and [No_]='" + Code + "' GROUP BY  I.Description, I.[No_], I.[Base Unit of Measure],I.[Gen_ Prod_ Posting Group]";

        SqlCommand cmd = new SqlCommand(s, con1);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
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

        SqlCommand cmd = new SqlCommand("SELECT distinct [Department Name] Description,[Department Name] Code  FROM NAAC_ADV_TEST.[dbo].[TMU Hospital$Fixed Asset]   where [Department Name]!=''   ", con1);
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
                //foreach (string multiTo in multi)
                //{
                //    string multiTo1 = multiTo;

                //    SqlDataReader drs = SHow_DepartmentNamess(tblDepartment, multiTo1);
                //    drs.Read();
                //    if (drs.HasRows)
                //    {

                //        string Descriptionss = drs["Name"].ToString();
                //        drs.Close();
                //        con.DisConnect();
                //        PortalCon.Insert_tbl_MultipleDepartmentforIndent(multiTo1, Descriptionss, Session["uid"].ToString());
                //        con.DisConnect();
                //    }
                //    else
                //    {
                //        drs.Close();
                //        con.DisConnect();
                //    }


                //}

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
        if (ddIssueFor.SelectedValue.Trim() == "")
        {
            ddIssueid.Items.Clear();
            txtIssueName.Text = "";
            txtDepartmentCode.Text = "";
            txtDepartmentName.Text = "";
        }


    }

    public SqlDataReader SHow_DepartmentNamess(string companyName, string DepartmentCOde)
    {
        if (con1 != null && con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }
        string s = "SELECT distinct [Department Name] Description     ,[Department Name] Code  FROM [dbo].[TMU Hospital$Fixed Asset] where[Department Code] != ''";


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
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["IndentEntry_DH"].ToString() == "1")
            {

                txtIssueDate.Text = System.DateTime.Now.ToString("dd MMM yyyy");
                txtIssueUserid.Text = Session["uname"].ToString();
                hfIssueid.Value = Session["uid"].ToString();

                if (con1 != null && con1.State == ConnectionState.Closed)
                {
                    con1.Open();
                }

                SqlCommand sqlCmd = new SqlCommand("select No_,[First Name] from  [TMU$Employee] where [No_]='" + Session["IndentApproval_DH"] + "'", con1);
                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
                DataTable dt1 = new DataTable();
                sqlDa.Fill(dt1);





                txtapprovalid.Text = dt1.Rows[0]["First Name"].ToString();
                hfApprovalId.Value = dt1.Rows[0]["No_"].ToString();




                if (!IsPostBack)
                {

                    txtIndentno.Text = "";

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
        mdlCreateIndent.Show();
        pnlCreateIndent.Visible = true;
    }
    protected void grdApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdApproval.PageIndex = e.NewPageIndex;
        ShowApprovaldata();
    }

    protected void grdApproval_DataBound(object sender, EventArgs e)
    {

    }

    protected void grdApproval_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                Button btnApprove = (Button)e.Row.FindControl("btnApprove");
                Button btnReject = (Button)e.Row.FindControl("btnReject");
                if (lblStatus.Text.Trim() == "Open")
                {
                    btnApprove.Visible = true;
                    btnReject.Visible = true;
                }

                if (lblStatus.Text.Trim() == "Approved" || lblStatus.Text.Trim() == "Rejected" || lblStatus.Text.Trim() == "Released" || lblStatus.Text.Trim() == "Pending on HOD" || lblStatus.Text.Trim() == "Issued")
                {
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                }


            }
        }
        catch (Exception)
        { }
    }

    public void ShowApprovaldata()
    {
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "No")
        {
        }
        else
        {
            con = new Connection();
            string ccname = "TMU Hospital";
            string rccname = ccname.Replace(".", "_");

            string tblIndentHeader = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Equipment Indent Header" + "]";
            string tblDimension = "[" + rccname + "$TMU$Dimension Value" + "]";
            if (txtFromDate.Text == "" && txtTillDate.Text == "")
            {
                SqlDataReader dr = SHow_IndentHeaderforApprovalbyUserid(tblIndentHeader, Session["uid"].ToString().Trim());
                DataTable dt = new DataTable();
                dt.Load(dr);
                grdApproval.DataSource = dt;
                grdApproval.DataBind();
                dr.Close();
                con.DisConnect();
            }
            else
            {
                SqlDataReader dr = SHow_IndentHeaderforApprovalbyUserDateFilter(tblIndentHeader, Session["uid"].ToString().Trim(), txtFromDate.Text.Trim(), txtTillDate.Text.Trim(), ddStatus.SelectedValue.Trim());
                DataTable dt = new DataTable();
                dt.Load(dr);
                grdApproval.DataSource = dt;
                grdApproval.DataBind();
                dr.Close();
                con.DisConnect();
            }

            //if (rdDocumentNo.Checked == true)
            //{
            //    SqlDataReader dr = con.SHow_IndentHeaderforApprovalbyUSerNOFilter(tblIndentHeader, Session["uid"].ToString().Trim(), txtSearch.Text.Trim());
            //    DataTable dt = new DataTable();
            //    dt.Load(dr);
            //    grdApproval.DataSource = dt;
            //    grdApproval.DataBind();
            //    dr.Close();
            //    con.DisConnect();
            //}


        }
    }


    public SqlDataReader SHow_IndentHeaderforApprovalbyUserid(string companyName, string Employeeid)
    {

        string s = "select  [Remarks- HOD] as 'HodRemark',[Remarks- Management] Management_remark,No_ as DocumentNo ,[Issue Date] , [Issue For] = CASE WHEN [Issue For] =null THEN '' WHEN [Issue For] ='1' THEN 'Department' WHEN [Issue For] ='2' THEN 'Employee' ELSE '' END, [Status] = CASE WHEN [Status] =null THEN '' WHEN [Status] ='0' THEN 'Open' WHEN [Status] ='1' THEN 'Processed for Approval' WHEN [Status] ='2' THEN 'Pending on Management' WHEN [Status] ='3' THEN 'Released' WHEN [Status] ='4' THEN 'Rejected' WHEN [Status] ='5' THEN 'Issued' WHEN [Status] ='8' THEN 'Approved by Management' WHEN [Status] ='9' THEN 'Rejected by Management' WHEN [Status] ='10' THEN 'Item Received'  ELSE '' END   ,[Issue Id],[Issue Name],[Remarks- User] 'Remarks' from " + companyName + " where [Employee ID]='" + Employeeid + "' and Status=0";
        if (con1.State == ConnectionState.Closed)
        {

            con1.Open();
        }

        SqlCommand cmd = new SqlCommand(s, con1);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }






    public SqlDataReader SHow_IndentHeaderforApprovalbyUserDateFilter(string companyName, string EmployeeidID, string fromdate, string tilldate, string status)
    {
        string s = "";
        if (status == "7")
        {
            s = "select  [Remarks- HOD] as 'HodRemark',[Remarks- Management] Management_remark, No_ as DocumentNo ,[Issue Date] , [Issue For] = CASE WHEN [Issue For] =null THEN '' WHEN [Issue For] ='1' THEN 'Department' WHEN [Issue For] ='2' THEN 'Employee' ELSE '' END, [Status] = CASE WHEN [Status] =null THEN '' WHEN [Status] ='0' THEN 'Open' WHEN [Status] ='1' THEN 'Pending on HOD' WHEN [Status] ='2' THEN 'Approved' WHEN [Status] ='3' THEN 'Released' WHEN [Status] ='4' THEN 'Rejected' WHEN [Status] ='5' THEN 'Issued' ELSE '' END  ,[Issue Id],[Issue Name],[Remarks- User] Remarks from " + companyName + " where [Employee ID]='" + EmployeeidID + "' and convert(date,[Issue Date],103)>='" + fromdate + "' and convert(date,[Issue Date],103)<='" + tilldate + "' ";
        }
        else
        {

            s = "select [Remarks- HOD] as 'HodRemark',[Remarks- Management] Management_remark, No_ as DocumentNo ,[Issue Date] , [Issue For] = CASE WHEN [Issue For] =null THEN '' WHEN [Issue For] ='1' THEN 'Department' WHEN [Issue For] ='2' THEN 'Employee' ELSE '' END, [Status] = CASE WHEN [Status] =null THEN '' WHEN [Status] ='0' THEN 'Open' WHEN [Status] ='1' THEN 'Pending on HOD' WHEN [Status] ='2' THEN 'Approved' WHEN [Status] ='3' THEN 'Released' WHEN [Status] ='4' THEN 'Rejected' WHEN [Status] ='5' THEN 'Issued' ELSE '' END  ,[Issue Id],[Issue Name],[Remarks- User] Remarks from " + companyName + " where [Employee ID]='" + EmployeeidID + "' and convert(date,[Issue Date],103)>='" + fromdate + "' and convert(date,[Issue Date],103)<='" + tilldate + "' and [Status]='" + status + "'";
        }
        if (con1.State != ConnectionState.Closed)
        {

            con1.Open();
        }

        con1.Open();
        SqlCommand cmd = new SqlCommand(s, con1);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    protected void grdViewIndentLine_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdViewIndentLine_1.PageIndex = e.NewPageIndex;
        ShowLinedata(Session["indentdocumtss"].ToString());
    }
    public void ShowLinedata(string documentno)
    {

        con = new Connection();
        string ccname = "TMU Hospital";
        string rccname = ccname.Replace(".", "_");

        string tblIndentLine = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Equipment Indent Line" + "]";
        SqlDataReader dr = con.SHow_IndentLinedataforduplicaet(tblIndentLine, documentno.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        grdViewIndentLine_1.DataSource = dt;
        grdViewIndentLine_1.DataBind();
        dr.Close();
        con.DisConnect();
        mdIndentLine.Show();
        //ShowApprovaldata();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtFromDate.Text == "" || txtTillDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please choose date range');", true);
        }



        ShowApprovaldata();
    }

    protected void btnApprove_Command(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        string ccname = "TMU Hospital";
        string rccname = ccname.Replace(".", "_");


        string tblIndentHeader = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Equipment Indent Header" + "]";
        con = new Connection();
        con.UpdateSendForApprovalHeader(tblIndentHeader, id.Trim(), "1");
        con.DisConnect();
        ShowApprovaldata();
    }

    protected void btnReject_Command(object sender, CommandEventArgs e)
    {
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "No")
        {
        }
        else
        {
            string documentno = e.CommandArgument.ToString();

            string ccname = "TMU Hospital";
            string rccname = ccname.Replace(".", "_");


            string tblIndentHeader = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Equipment Indent Header" + "]";
            con = new Connection();
            con.UpdateSendForApprovalHeader(tblIndentHeader, documentno.Trim(), "4");
            con.DisConnect();
            ShowApprovaldata();
        }
    }


    protected void lblDoumnentNo_Command(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();

        Session["indentdocumtss"] = "";
        Session["indentdocumtss"] = id.Trim();
        pnlCreateIndent.Visible = true;
        ShowLinedata(id);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grdApproval.RenderControl(htmlWrite);

        Response.Clear();
        string str = "IndentReport" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();

    }
}