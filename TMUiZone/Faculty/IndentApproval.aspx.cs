using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Configuration;


public partial class Faculty_IndentApproval : System.Web.UI.Page
{
    SqlConnection con1 = new SqlConnection(ConfigurationSettings.AppSettings["str"].ToString());
    Connection con; String IndentStatus = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["IndentApprovalID"].ToString() == "")
            {
                Response.Redirect("../Default.aspx");
            }
            if (!IsPostBack)
            {
                ShowApprovaldata();
            }

        }
        catch (Exception)
        {
            Response.Redirect("../Default.aspx");
        }
    }

    public void ShowApprovaldata()
    {
        con = new Connection();
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");

        string tblIndentHeader = "[" + rccname + "$Indent Header" + "]";
        con.Update_Indentheaderhoddata(tblIndentHeader, Session["IndentApproval_DH"].ToString().Trim(), Session["uid"].ToString().Trim());
        con.DisConnect();
        if (txtFromDate.Text == "" && txtTillDate.Text == "")
        {

            SqlDataReader dr = con.SHow_IndentHeaderforApprovalbyhod(tblIndentHeader, Session["uid"].ToString().Trim());
            string s = "select No_ as DocumentNo ,[Issue Date] , [Issue For] = CASE WHEN [Issue For] =null THEN '' WHEN [Issue For] ='1' THEN 'Department' WHEN [Issue For] ='2' THEN 'Employee' ELSE '' END, [Status] = CASE WHEN [Status] =null THEN '' WHEN [Status] ='0' THEN 'Open' WHEN [Status] ='1' THEN 'Processed for Approval' WHEN [Status] ='2' THEN 'Approved' WHEN [Status] ='3' THEN 'Released' WHEN [Status] ='4' THEN 'Rejected' ELSE '' END  ,[Issue Id],(select [Department Name] from [TMU$Employee] where [No_]=IH.[Employee ID]) as [Issue Name],(Select count(*) from [TMU$Indent Line] where [Document No]=IH.[No_] and[Rem_Qty] != 0) as 'AllIssue' from " + tblIndentHeader + " IH where [Approval ID]='" + Session["uid"].ToString().Trim() + "' and Status=1";

            SqlCommand cmd = new SqlCommand();
            if (con1.State == ConnectionState.Closed)
                con1.Open();
            cmd = new SqlCommand(s, con1);
            SqlDataReader drR = cmd.ExecuteReader();


            DataTable dt = new DataTable();
            dt.Load(drR);
            grdApproval.DataSource = dt;
            grdApproval.DataBind();
            drR.Close();
            con.DisConnect();
        }

        //if (rdDocumentNo.Checked == true)
        //{
        //    SqlDataReader dr = con.SHow_IndentHeaderforApprovalbyhodNOFilter(tblIndentHeader, Session["uid"].ToString().Trim(),txtSearch.Text.Trim());
        //    DataTable dt = new DataTable();
        //    dt.Load(dr);
        //    grdApproval.DataSource = dt;
        //    grdApproval.DataBind();
        //    dr.Close();
        //    con.DisConnect();
        //}

        else
        {

            
            string s = "select No_ as DocumentNo ,[Issue Date] , [Issue For] = CASE WHEN [Issue For] =null THEN '' WHEN [Issue For] ='1' THEN 'Department' WHEN [Issue For] ='2' THEN 'Employee' ELSE '' END, [Status] = CASE WHEN [Status] =null THEN '' WHEN [Status] ='0' THEN 'Open' WHEN [Status] ='1' THEN 'Processed for Approval' WHEN [Status] ='2' THEN 'Approved' WHEN [Status] ='3' THEN 'Released' WHEN [Status] ='4' THEN 'Rejected' ELSE '' END  ,[Issue Id],(select [Department Name] from [TMU$Employee] where [No_]=IH.[Employee ID]) as [Issue Name],(Select count(*) from [TMU$Indent Line] where [Document No]=IH.[No_] and[Rem_Qty] != 0) as 'AllIssue' from " + tblIndentHeader + " IH where [Approval ID]='" + Session["uid"].ToString().Trim() + "' and convert(date,[Issue Date],103)>='" + txtFromDate.Text.Trim() + "' and convert(date,[Issue Date],103)<='" + txtTillDate.Text.Trim() + "' and [Status]='" + ddStatus.SelectedValue.Trim() + "'";

            SqlCommand cmd = new SqlCommand();
            if (con1.State == ConnectionState.Closed)
                con1.Open();
            cmd = new SqlCommand(s, con1);
            SqlDataReader drR = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(drR);
            grdApproval.DataSource = dt;
            grdApproval.DataBind();
            drR.Close();
            con.DisConnect();
            drR.Close();
            con.DisConnect();
        }
    }
    protected void btnApprove_Command(object sender, CommandEventArgs e)
    {
        string documentno = e.CommandArgument.ToString();
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");


        string tblIndentHeader = "[" + rccname + "$Indent Header" + "]";
        con = new Connection();
        con.UpdateSendForApprovalHeader(tblIndentHeader, documentno.Trim(), "2");
        con.DisConnect();
        ShowApprovaldata();
    }
    protected void btnReject_Command(object sender, CommandEventArgs e)
    {
        string documentno = e.CommandArgument.ToString();

        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");


        string tblIndentHeader = "[" + rccname + "$Indent Header" + "]";
        con = new Connection();
        con.UpdateSendForApprovalHeader(tblIndentHeader, documentno.Trim(), "4");
        con.DisConnect();
        ShowApprovaldata();
    }
    protected void grdApproval_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField AllApprove= (HiddenField)e.Row.FindControl("AllApprove");
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                Button btnApprove = (Button)e.Row.FindControl("btnApprove");
                Button btnReject = (Button)e.Row.FindControl("btnReject");
                if (lblStatus.Text.Trim() == "Processed for Approval")
                {
                    btnApprove.Visible = true;
                    btnReject.Visible = true;
                }

                if (lblStatus.Text.Trim() == "Approved" || lblStatus.Text.Trim() == "Rejected" || lblStatus.Text.Trim() == "Released")
                {
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                }
                if(AllApprove.Value=="0")
                {
                    e.Row.BackColor = System.Drawing.Color.YellowGreen;
                }

            }
        }
        catch (Exception)
        { }
    }
    protected void rdDate_CheckedChanged(object sender, EventArgs e)
    {
        pnlDatewisefilter.Visible = true;
        txtFromDate.Text = "";
        txtTillDate.Text = "";
        //txtSearch.Text = "";
        //txtSearch.Visible = false;
    }
    protected void rdDocumentNo_CheckedChanged(object sender, EventArgs e)
    {
        pnlDatewisefilter.Visible = true;
        txtFromDate.Text = "";
        txtTillDate.Text = "";
        //txtSearch.Text = ""; 
        //txtSearch.Visible = true;

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ShowApprovaldata();
    }
    protected void grdApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdApproval.PageIndex = e.NewPageIndex;
        ShowApprovaldata();
    }
    protected void lblDoumnentNo_Command(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        Session["indentdocumt"] = "";
        Session["indentdocumt"] = id.Trim();
        LinkButton btn = (LinkButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        int RowIndex = gvr.RowIndex;
        Label lbStatus = (Label)grdApproval.Rows[RowIndex].FindControl("lblStatus");
        IndentStatus = lbStatus.Text;
        ShowLinedata(id);

    }
    public void ShowLinedata(string documentno)
    {

        con = new Connection();
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        string tblIndentLine = "[" + rccname + "$Indent Line" + "]";
        SqlDataReader dr = con.SHow_IndentLinedataforduplicaet(tblIndentLine, documentno.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        grdViewIndentLine.DataSource = dt;
        grdViewIndentLine.DataBind();
        dr.Close();
        con.DisConnect();
        mdIndentLine.Show();
        if (IndentStatus != "Processed for Approval")
        {
            for (int i = 0; i < grdViewIndentLine.Rows.Count; i++)
            {
                TextBox type = (TextBox)grdViewIndentLine.Rows[i].FindControl("lblQuantity_Grid");
                type.Enabled = false;
            }
        }

    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        ShowApprovaldata();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        con = new Connection();
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        string tblIndentLine = "[" + rccname + "$Indent Line" + "]";

        int a = grdViewIndentLine.Rows.Count;
        for (int i = 0; i < grdViewIndentLine.Rows.Count; i++)
        {
            string LineNo = grdViewIndentLine.DataKeys[i].Values[0].ToString();
            HiddenField hfQuantity = (HiddenField)grdViewIndentLine.Rows[i].FindControl("hfQuantity");
            TextBox txtQty = (TextBox)grdViewIndentLine.Rows[i].FindControl("lblQuantity_Grid");


            if (txtQty.Text != hfQuantity.Value)
            {
                con.UpdateItemQuantity(tblIndentLine, txtQty.Text, LineNo);  //for code



            }
        }
        con.DisConnect();
    }
    protected void grdApproval_DataBound(object sender, EventArgs e)
    {
        //try
        //{
        //    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        //    for (int i = 0; i < grdApproval.Columns.Count; i++)
        //    {
        //        TableHeaderCell cell = new TableHeaderCell();
        //        TextBox txtSearch = new TextBox();
        //        Label txtSearch1 = new Label();

        //        if (i == 0 || i == 6 || i == 7 || i == 8)
        //        {
        //            txtSearch1.Attributes["placeholder"] = "";
        //            txtSearch1.Width = 70;
        //            txtSearch1.CssClass = "search_textbox";
        //            cell.Controls.Add(txtSearch1);
        //            row.Controls.Add(cell);

        //        }
        //        else if (i == 1 || i == 2 || i == 3 || i == 4 || i == 5)
        //        {
        //            txtSearch.Attributes["placeholder"] = "";
        //            txtSearch.Width = 140;
        //            txtSearch.Height = 24;
        //            txtSearch.CssClass = "search_textbox";
        //            cell.Controls.Add(txtSearch);
        //            row.Controls.Add(cell);
        //        }

        //    }
        //    grdApproval.HeaderRow.Parent.Controls.AddAt(1, row);
        //}

        //catch (Exception e3) { }

    }
}