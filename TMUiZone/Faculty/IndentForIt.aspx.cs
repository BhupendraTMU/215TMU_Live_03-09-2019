using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
public partial class Faculty_IndentForIt : System.Web.UI.Page
{
    Connection con;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Session["IndentEntry_DHIT"].ToString() == "1")
            {

                if (!IsPostBack)
                {
                    ShowApprovaldata();
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
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        Response.Redirect("itindent.aspx");
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

                if (lblStatus.Text.Trim() == "Approved" || lblStatus.Text.Trim() == "Rejected" || lblStatus.Text.Trim() == "Released" || lblStatus.Text.Trim() == "Pending on HOD" || lblStatus.Text.Trim() == "Issued" || lblStatus.Text.Trim() == "Approved by Management" || lblStatus.Text.Trim() == "Item Received")
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
            string ccname = "TMU Advertisement";
            string rccname = ccname.Replace(".", "_");

            string tblIndentHeader = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Indent Header" + "]";
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
             s = "select  [Remarks- HOD] as 'HodRemark',[Remarks- Management] Management_remark, No_ as DocumentNo ,[Issue Date] , [Issue For] = CASE WHEN [Issue For] =null THEN '' WHEN [Issue For] ='1' THEN 'Department' WHEN [Issue For] ='2' THEN 'Employee' ELSE '' END, [Status] = CASE WHEN [Status] =null THEN '' WHEN [Status] ='0' THEN 'Open' WHEN [Status] ='1' THEN 'Pending on HOD' WHEN [Status] ='2' THEN 'Approved' WHEN [Status] ='3' THEN 'Released' WHEN [Status] ='4' THEN 'Rejected' WHEN [Status] ='5' THEN 'Issued' WHEN [Status] ='6' THEN 'Partially Issues' WHEN [Status] ='7' THEN 'Reject From It' WHEN [Status] ='8' THEN 'Approved by Management' WHEN [Status] ='9' THEN 'Rejected from Management' WHEN [Status] ='10' THEN 'Item Received' ELSE '' END  ,[Issue Id],[Issue Name],[Remarks- User] Remarks from " + companyName + " where [Employee ID]='" + EmployeeidID + "' and convert(date,[Issue Date],103)>='" + fromdate + "' and convert(date,[Issue Date],103)<='" + tilldate + "' ";
        }
        else
        {

            s = "select [Remarks- HOD] as 'HodRemark',[Remarks- Management] Management_remark, No_ as DocumentNo ,[Issue Date] , [Issue For] = CASE WHEN [Issue For] =null THEN '' WHEN [Issue For] ='1' THEN 'Department' WHEN [Issue For] ='2' THEN 'Employee' ELSE '' END, [Status] = CASE WHEN [Status] =null THEN '' WHEN [Status] ='0' THEN 'Open' WHEN [Status] ='1' THEN 'Pending on HOD' WHEN [Status] ='2' THEN 'Approved' WHEN [Status] ='3' THEN 'Released' WHEN [Status] ='4' THEN 'Rejected' WHEN [Status] ='5' THEN 'Issued' WHEN [Status] ='6' THEN 'Partially Issues' WHEN [Status] ='7' THEN 'Reject From It' WHEN [Status] ='8' THEN 'Approved by Management' WHEN [Status] ='9' THEN 'Rejected from Management' WHEN [Status] ='10' THEN 'Item Received' ELSE '' END  ,[Issue Id],[Issue Name],[Remarks- User] Remarks from " + companyName + " where [Employee ID]='" + EmployeeidID + "' and convert(date,[Issue Date],103)>='" + fromdate + "' and convert(date,[Issue Date],103)<='" + tilldate + "' and [Status]='" + status + "'";
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
        grdViewIndentLine.PageIndex = e.NewPageIndex;
        ShowLinedata(Session["indentdocumtss"].ToString());
    }
    public void ShowLinedata(string documentno)
    {

        con = new Connection();
        string ccname = "TMU Advertisement";
        string rccname = ccname.Replace(".", "_");

        string tblIndentLine = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Indent Line" + "]";
        SqlDataReader dr = con.SHow_IndentLinedataforduplicaet(tblIndentLine, documentno.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        grdViewIndentLine.DataSource = dt;
        grdViewIndentLine.DataBind();
        dr.Close();
        con.DisConnect();
        mdIndentLine.Show();
        ShowApprovaldata();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ShowApprovaldata();
    }

    protected void btnApprove_Command(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        string ccname = "TMU Advertisement";
        string rccname = ccname.Replace(".", "_");


        string tblIndentHeader = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Indent Header" + "]";
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

            string ccname = "TMU Advertisement";
            string rccname = ccname.Replace(".", "_");


            string tblIndentHeader = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Indent Header" + "]";
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