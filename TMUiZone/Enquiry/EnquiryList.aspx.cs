using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PL;
using DL;
using Utility;
using System.Data;
using System.Reflection;
using System.Drawing;
using System.IO;

public partial class EnquiryList : System.Web.UI.Page
{
    string UserName = string.Empty; static string Search;   

    protected void Page_Load(object sender, EventArgs e)
    {


        if (Session["uid"] != null)
        {
            if (Session["UserGroup"].ToString() == "ADMIN" || Session["UserGroup"].ToString() == "REGISTRAR")
            {
                Session["UserGroup"] = Session["UserGroup"].ToString();
            }
            else if (Session["UserGroup"].ToString() != "COUNSELLOR")
            {
                Response.Redirect("../Default.aspx");
            }

            if (!IsPostBack)
            {
                cleFollowUpDate.StartDate = DateTime.Now.AddDays(0);
                BindEnquiryListGrid();
                BindDdlSearch();
                Search = "";
            }

        }
        else
        {
            Response.Redirect("../Default.aspx");
        }

    }
    public void BindDdlFollowUpStatus()
    {
        DataTable dt = new DataTable();
        EnquiryDL objEnquiryDL = new EnquiryDL();
        dt = objEnquiryDL.GetFollowUpStatusDdl();
        ddlFollowUpStatus.DataSource = dt;
        ddlFollowUpStatus.DataTextField = "Details";
        ddlFollowUpStatus.DataValueField = "No_";
        ddlFollowUpStatus.DataBind();

    }
    public void BindEnquiryListGrid()
    {
        EnquiryDL objEnquiryDL = new EnquiryDL();
        List<EnquiryPL> objEnquiryList = new List<EnquiryPL>();
        if (Session["uid"].ToString().ToString() != null)
        {
            string UserId = Session["uid"].ToString().ToString();
            objEnquiryList = objEnquiryDL.GetEnquiryList(UserId);
        }
        else
        {
            objEnquiryList = objEnquiryDL.GetEnquiryList("");
        }
        grdEnquiry.DataSource = objEnquiryList;
        grdEnquiry.DataBind();
        Session["EnqList"] = null;
        Session["EnqList"] = objEnquiryList;
        if (grdEnquiry.Rows.Count > 0)
        {
            btnExport.Visible = true;
        }
        else
        {
            btnExport.Visible = false;
        }
    }
    public void BindFollowUpListGrid(String ID)
    {
        EnquiryDL objFollowUpDL = new EnquiryDL();
        List<EnquiryPL> objFollowUpList = new List<EnquiryPL>();
        objFollowUpList = objFollowUpDL.GetFollowUpList_ByENquiryId(ID);
        grdFollowUp.DataSource = objFollowUpList;
        grdFollowUp.DataBind();
    }

    protected void grdEnquiry_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdEnquiry.PageIndex = e.NewPageIndex;
        //if (Search == "")
        //    BindEnquiryListGrid();
        //else
        //    BindSearchGrid();       
        grdEnquiry.DataSource = Session["EnqList"] as List<EnquiryPL>;// objEnquiryNewList;
        grdEnquiry.DataBind();
        if (grdEnquiry.Rows.Count > 0)
        {
            btnExport.Visible = true;
        }
        else
        {
            btnExport.Visible = false;
        }


    }
    protected void grdFollowUp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdFollowUp.PageIndex = e.NewPageIndex;
        BindFollowUpListGrid(lblFollowEnquiryNo.Text);
        mpe.Show();
    }
    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindDdlFollowUpStatus();
        String No_;
        No_ = grdEnquiry.SelectedDataKey.Value.ToString();
        BindFollowUpListGrid(No_);
        Refresh();
        lblFollowEnquiryNo.Text = No_;
        mpe.Show();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        mpe.Show();
        Boolean v = true;
        if (v == Validate())
        {
            return;
        }
        else
        {
            List<EnquiryPL> objFollowUpList = new List<EnquiryPL>();
            objFollowUpList.Add(new EnquiryPL(lblFollowEnquiryNo.Text, 0, Convert.ToInt32(ddlFollowUpStatus.SelectedValue), txtFollowUpDate.Text, txtRemarks.Text, ""));
            lblMsg.Text = SaveFollowUp(objFollowUpList, UserName);
            if (lblMsg.Text != "Error")
            {
                Refresh();
                BindFollowUpListGrid(lblFollowEnquiryNo.Text);
            }
            else
            {
                mpe.Show();
            }

        }
    }
    public Boolean Validate()
    {
        lblMsg.Text = "";
        Boolean Val = false;
        if (ddlFollowUpStatus.SelectedValue == "0") { ddlFollowUpStatus.Focus(); Val = true; lblMsg.Text = "Select Follow up Status !"; }
        else if (txtFollowUpDate.Text == "") { cleFollowUpDate.Focus(); Val = true; lblMsg.Text = "Select Next Follow up Date !"; }
        return Val;
    }
    public void Refresh()
    {
        ddlFollowUpStatus.SelectedValue = "0";
        txtFollowUpDate.Text = "";
        txtRemarks.Text = "";

    }
    public String SaveFollowUp(List<EnquiryPL> objFollowUpList, string UserName)
    {
        String Message;
        EnquiryDL objEnquiryDL = new EnquiryDL();
        Message = objEnquiryDL.SaveFollowUp(objFollowUpList, UserName);
        return Message;
    }
    public void BindDdlSearch()
    {
        DataTable dt = new DataTable();
        EnquiryDL objEnquiryDL = new EnquiryDL();
        dt = objEnquiryDL.GetSearchEnquiryDdl();
        ddlSearch.DataSource = dt;
        ddlSearch.DataTextField = "Details";
        ddlSearch.DataValueField = "No_";
        ddlSearch.DataBind();

    }
    public void BindSearchGrid()
    {
        EnquiryDL objEnquiryDL = new EnquiryDL();
        List<EnquiryPL> objEnquiryList = new List<EnquiryPL>();
        if (Session["uid"].ToString().ToString() != null)
        {
            string UserId = Session["uid"].ToString().ToString();
            objEnquiryList = objEnquiryDL.GetEnquiryList(UserId);
        }
        else
        {
            objEnquiryList = objEnquiryDL.GetEnquiryList("");
        }

        List<EnquiryPL> objEnquiryNewList = new List<EnquiryPL>();
        if (ddlSearch.SelectedValue == "1")
        {
            objEnquiryNewList = objEnquiryList.FindAll(r => r.No_.StartsWith(txtSearch.Text.Trim().ToString(), true, null));
        }

        else if (ddlSearch.SelectedValue == "2")
        {
            objEnquiryNewList = objEnquiryList.FindAll(r => r.ApplicantName.Trim().StartsWith(txtSearch.Text.Trim().ToString(), true, null));
        }
        else if (ddlSearch.SelectedValue == "3")
        {
            objEnquiryNewList = objEnquiryList.FindAll(r => r.CourseCode.StartsWith(txtSearch.Text.Trim().ToString(), true, null));
        }
        else if (ddlSearch.SelectedValue == "4")
        {
            //College
            objEnquiryNewList = objEnquiryList.FindAll(r => r.CollegeInterested.StartsWith(txtSearch.Text.Trim().ToString(), true, null));
        }
        else if (ddlSearch.SelectedValue == "5")
        {
            objEnquiryNewList = objEnquiryList.FindAll(r => r.City.StartsWith(txtSearch.Text.Trim().ToString(), true, null));
        }
        else if (ddlSearch.SelectedValue == "6")
        {
            objEnquiryNewList = objEnquiryList.FindAll(r => r.EnquiryDate.StartsWith(txtSearch.Text.Trim().ToString(), true, null));
        }
        else
        {
            objEnquiryNewList = objEnquiryList;
        }


        grdEnquiry.DataSource = objEnquiryNewList;
        grdEnquiry.DataBind();
        Session["EnqList"] = objEnquiryNewList;


    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search = "Search";
        BindSearchGrid();
        
        if (grdEnquiry.Rows.Count > 0)
        {
            btnExport.Visible = true;
        }
        else
        {
            btnExport.Visible = false;
        }

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Search = "";
        Response.Redirect("EnquiryList.aspx");
    }

    protected void grdEnquiry_Sorting(object sender, GridViewSortEventArgs e)
    {
       
        List<EnquiryPL> objEnquiryList = new List<EnquiryPL>();
        objEnquiryList = Session["EnqList"] as List<EnquiryPL>;
        string Sortdir = GetSortDirection(e.SortExpression);
        string SortExp = e.SortExpression;
        var list = objEnquiryList;
        if (Sortdir == "ASC")
        {
            list = Sort<EnquiryPL>(list, SortExp, SortDirection.Ascending);
        }
        else
        {
            list = Sort<EnquiryPL>(list, SortExp, SortDirection.Descending);
        }
        this.grdEnquiry.DataSource = list;
        Session["EnqList"] = list;
        this.grdEnquiry.DataBind();
    }
   
    private string GetSortDirection(string column)
    {
        string sortDirection = "ASC";
        string sortExpression = ViewState["SortExpression"] as string;
        if (sortExpression != null)
        {
            if (sortExpression == column)
            {
                string lastDirection = ViewState["SortDirection"] as string;
                if ((lastDirection != null) && (lastDirection == "ASC"))
                {
                    sortDirection = "DESC";
                }
            }
        }
        ViewState["SortDirection"] = sortDirection;
        ViewState["SortExpression"] = column;
        return sortDirection;
    }
    public List<EnquiryPL> Sort<TKey>(List<EnquiryPL> list, string sortBy, SortDirection direction)
    {
        PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
        if (direction == SortDirection.Ascending)
        {
            return list.OrderBy(e => property.GetValue(e, null)).ToList<EnquiryPL>();
        }
        else
        {
            return list.OrderByDescending(e => property.GetValue(e, null)).ToList<EnquiryPL>();
        }

    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (grdEnquiry.Rows.Count <= 0)
        {
            return;
        }
        Response.Clear();   Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=EnquiryList.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //To Export all pages
            grdEnquiry.AllowPaging = false;
            List<EnquiryPL> objEnquiryList = new List<EnquiryPL>();
            objEnquiryList = Session["EnqList"] as List<EnquiryPL>;
            grdEnquiry.DataSource = objEnquiryList;
            grdEnquiry.DataBind();            
            
            grdEnquiry.HeaderRow.BackColor = Color.YellowGreen;
            
            foreach (TableCell cell in grdEnquiry.HeaderRow.Cells)
            {
                cell.BackColor = grdEnquiry.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grdEnquiry.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grdEnquiry.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grdEnquiry.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }
            grdEnquiry.Columns[0].Visible = false;
            grdEnquiry.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }
    
}