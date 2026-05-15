using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class Faculty_Indentview : System.Web.UI.Page
{
    Connection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["IndentEntry_DH"].ToString() == "1")
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
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        ShowApprovaldata();
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

                if (lblStatus.Text.Trim() == "Approved" || lblStatus.Text.Trim() == "Rejected" || lblStatus.Text.Trim() == "Released" || lblStatus.Text.Trim() == "Processed for Approval")
                {
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                }


            }
        }
        catch (Exception)
        { }
    }
    protected void btnApprove_Command(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");

        
        string tblIndentHeader = "[" + rccname + "$Indent Header" + "]";
        con = new Connection();
        con.UpdateSendForApprovalHeader(tblIndentHeader, id.Trim(), "1");
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
    //protected void rdDocumentNo_CheckedChanged(object sender, EventArgs e)
    //{
    //    pnlDatewisefilter.Visible = false;      
    //    txtFromDate.Text = "";
    //    txtTillDate.Text = "";
    //    // txtSearch.Text = "";
    //   // txtSearch.Visible = true;
    //}
    //protected void rdDate_CheckedChanged(object sender, EventArgs e)
    //{
    //    pnlDatewisefilter.Visible = true;
    //    txtFromDate.Text = "";
    //    txtTillDate.Text = "";
    //   // txtSearch.Text = "";
    //   // txtSearch.Visible = false;
    //}


    public void ShowApprovaldata()
    {
        con = new Connection();
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");

        string tblIndentHeader = "[" + rccname + "$Indent Header" + "]";
        string tblDimension = "[" + rccname + "$TMU$Dimension Value" + "]";
        if (txtFromDate.Text == "" && txtTillDate.Text == "")
        {
            SqlDataReader dr = con.SHow_IndentHeaderforApprovalbyUserid(tblIndentHeader, Session["uid"].ToString().Trim());
            DataTable dt = new DataTable();
            dt.Load(dr);
            grdApproval.DataSource = dt;
            grdApproval.DataBind();
            dr.Close();
            con.DisConnect();
        }
        else
        {
            SqlDataReader dr = con.SHow_IndentHeaderforApprovalbyUserDateFilter(tblIndentHeader, Session["uid"].ToString().Trim(), txtFromDate.Text.Trim(), txtTillDate.Text.Trim(), ddStatus.SelectedValue.Trim());
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
    protected void lblDoumnentNo_Command(object sender, CommandEventArgs e)
    {
        //int index = 0;
        //if (e.CommandArgument.ToString() == "")
        //{
        //    index = 0;
        //}
        //else
        //{
        //    index = Convert.ToInt32(e.CommandArgument.ToString())+1;
        //}
      
      //  string id = grdApproval.DataKeys[index].Values[0].ToString();
        string id = e.CommandArgument.ToString();

        Session["indentdocumtss"] = "";
        Session["indentdocumtss"] = id.Trim();
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
        ShowApprovaldata();
    }
    protected void grdViewIndentLine_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdViewIndentLine.PageIndex = e.NewPageIndex;
        ShowLinedata(Session["indentdocumtss"].ToString());
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        Response.Redirect("indent.aspx");
    }
    protected void grdApproval_DataBound(object sender, EventArgs e)
    {
            }
    protected void grdApproval_DataBound1(object sender, EventArgs e)
    {
        //try
        //{
        //    GridViewRow row = new GridViewRow(-1, -1, DataControlRowType.Header, DataControlRowState.Normal);
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
        //        else if ( i == 1 || i == 2 || i == 3 || i == 4 || i == 5)
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
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ShowApprovaldata();
    }
}