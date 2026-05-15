using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Collections;
using System.Configuration;
using AshokaHRMBudget.DTO;
using AshokaHRMBudget.DAO;
using System.Drawing;

public partial class Budget : System.Web.UI.Page
{
    #region BudgetEntry
    #region Variables
    decimal Jan, Feb, March, April, May, June, July, August, Sept, Oct, Nov, Dec, BudgetedAmount, PYB,YTDB, YTDA, FirstQuater, SecondQuater, ThirdQuater, FourthQuater,
                HeadJan, HeadFeb, HeadMarch, HeadApril, HeadMay, HeadJune, HeadJuly, HeadAugust, HeadSept, HeadOct, HeadNov, HeadDec, HeadBudgetedAmount, HeadPYB, HeadYTDB, HeadYTDA;
    string BudgetStatus, EmployeeId, ReviewedBy, ApprovedBy, gvBudgetUniqueID, QuarterlyCells;
    Int32 gvBudgetEditIndex = -1;
    
    #endregion

    #region UserDefinedFunction
    private void fillgrid(bool CheckForPremission)
    {
        if (CheckForPremission)
        {
            string Remark;
            BudgetStatus = EmployeeId = Remark = ReviewedBy = ApprovedBy = "";
            UtilityUI.FillGrid(GridViewHead, BudgetDAO.GLAccountByBudgetName(Session["NavDBName"].ToString(), Session["Company"].ToString(), ddlDepartment.SelectedValue.ToString(), Session["GlobalDimension1Code"].ToString(), ddlBudget.SelectedValue.ToString(), ddlBudget.Text, out EmployeeId, out BudgetStatus, out Remark, out ReviewedBy, out ApprovedBy,ViewState["GLRange"].ToString()), "GLRange", null);
            lblBudgetStatus.InnerHtml = "Status :- " + (BudgetStatus=="PENDING"?BudgetStatus+"..." : BudgetStatus=="PROCESSING"?BudgetStatus+"..." :BudgetStatus);
            txtRemark.InnerText = Remark;
            setPermission();
        }
        else
        {
            UtilityUI.FillGrid(GridViewHead, BudgetDAO.GLHead(Session["NavDBName"].ToString(), Session["Company"].ToString(), ddlDepartment.SelectedValue.ToString(), Session["GlobalDimension1Code"].ToString(), ddlBudget.SelectedValue.ToString()), "GLRange", null);
        }
    }
    private void fillChildgrid(GridView gv, string GLRange)
    {
        UtilityUI.FillGrid(gv, BudgetDAO.GLAccountByGLRange(Session["NavDBName"].ToString(), Session["Company"].ToString(), ddlDepartment.SelectedValue.ToString(), Session["GlobalDimension1Code"].ToString(), ddlBudget.SelectedValue.ToString(), GLRange), "ID", "");
    }
    void setPermission()
    {
        txtRemark.Disabled = false;
        lblRemark.Visible = txtRemark.Visible = true;
        if (ViewState["BudgetScreen"].ToString() == "ENTRY")
        {
            txtRemark.Disabled = true;
            lblRemark.Visible = txtRemark.Visible = BudgetStatus.ToString().ToUpper() == "PENDING"? false :true;
            btnSendForReview.Enabled = GridViewHead.Enabled = (BudgetStatus.ToString().ToUpper() == "PENDING" || (ApprovedBy == "" && BudgetStatus.ToString().ToUpper() == "SENTBACK")) ? true : false;
        }
        else if (ViewState["BudgetScreen"].ToString() == "REVIEW")
        {
            btnReview.Enabled = btnReject.Enabled = GridViewHead.Enabled = (BudgetStatus.ToString().ToUpper() == "PROCESSING" || (ApprovedBy != "" && BudgetStatus.ToString().ToUpper() == "SENTBACK")) ? true : false;
        }
        else if (ViewState["BudgetScreen"].ToString() == "APPROVE")
        {
            btnApprove.Enabled = btnReject.Enabled = GridViewHead.Enabled = (BudgetStatus.ToString().ToUpper() == "REVIEWED") ? true : false;
        }
    }
    void SendMail()
    {
        string SenderId = Session["CompanyEmail"].ToString().Trim(), SenderName = Session["Fulname"].ToString(),Subject = "",Message = "";
        string ToID = BudgetDAO.GetToMailIds(Session["NavDbName"].ToString() + "[" + Session["Company"].ToString(), Session["Company"].ToString(), ddlBudget.SelectedValue.ToString(), ddlDepartment.SelectedValue.ToString(), Session["GlobalDimension1Code"].ToString(), BudgetStatus, ViewState["BudgetScreen"].ToString());

        if (BudgetStatus == "PROCESSING")
        {
            Subject = "Budget Ready For Review";
            Message = Subject;// "Budget Aa gya Review k lie";
        }
        else if (BudgetStatus == "REVIEWED")
        {
            Subject = "Budget Ready For Approval";
            Message = Subject;// "Budget Aa gya Approval k lie";
        }
        else if (BudgetStatus == "APPROVED")
        {
            Subject = "Budget Approved";
            Message = Subject;// "Budget Aa gya Approve ho k";
        }
        else if (BudgetStatus == "SENTBACK")
        {
            Subject = "Budget Sent Back";
            Message = Subject;// "Budget Aa gya vapis tere pass hoo gai na gadbad";
        }

        UtilityUI.SendMail(SenderId, SenderName, ToID, Subject, Message, Session["Company"].ToString(), "Attendance");
        //subject1 = "User Request of " + System.DateTime.Now.ToString("dd-MM-yyyy");

        //Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}", Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "I have send request for requisition of  " + txtItemToPurchase.Text + " , '" + Environment.NewLine, "'Approxe Cost : " + txtApprox.Text + "   Quantity : " + txtQuantity.Text + " " + Environment.NewLine, "Remarks :  " + txtReason.Text + "", Environment.NewLine, "Kindly approve it.", Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

    }
    void ClickOnReview()
    {
        ViewState["BudgetScreen"] = "REVIEW";
        btnApprove.Visible = btnApprove.Enabled = btnSendForReview.Visible = btnSendForReview.Enabled = false;
        btnReview.Visible = btnReview.Enabled = btnReject.Enabled = btnReject.Visible = true;
        fillgrid(true);
        pnlReport.Visible = false;
        pnlEntry.Visible = true;
        Label2.Text = "Review Budget";
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                ViewState["GLRange"] = "300000000 and 500000000";
                ViewState["DepartmentTable"] = BudgetDAO.DepartmentForDdlByEmployee(Session["NavDBName"].ToString() + "[" + Session["Company"].ToString(), Session["uid"].ToString());
                Int32 BudgetEntry, BudgetReview, BudgetApprove;
                using (DataTable BudgetTable = BudgetDAO.BudgetForDdl(Session["NavDbName"].ToString() + "[" + Session["Company"].ToString()))
                {
                    if (BudgetTable.Rows.Count>0)
                    {
                        UtilityUI.ddlFillByDataTable(ddlBudget, BudgetTable.Copy(), "BudgetId", "BudgetId");
                        UtilityUI.ddlFillByDataTable(ddlDepartment, (DataTable)ViewState["DepartmentTable"], "DepartmentCode", "DepartmentCode");
                        BudgetDAO.EmployeeBudgetPermission(Session["NavDbName"].ToString() + "[" + Session["Company"].ToString(), Session["uid"].ToString(), out BudgetEntry, out BudgetReview, out BudgetApprove);
                        lnkBudgetEntry.Enabled = BudgetEntry == 1 ? true : false;
                        lnkReview.Enabled = BudgetReview == 1 ? true : false;
                        lnkApproveBudget.Enabled = BudgetApprove == 1 ? true : false;
                        ddlDepartment.SelectedValue = Session["DepartmentCode"].ToString();
                        //lnkBudgetEntry.ForeColor = lnkBudgetEntry.Enabled ? lnkBudgetEntry.ForeColor : System.Drawing.Color.WhiteSmoke;
                        //lnkReview.ForeColor = lnkReview.Enabled ? lnkReview.ForeColor : System.Drawing.Color.WhiteSmoke;
                        //lnkApproveBudget.ForeColor = lnkApproveBudget.Enabled ? lnkApproveBudget.ForeColor : System.Drawing.Color.Gray;

                        //BudgetReport
                        UtilityUI.ddlFillByDataTable(ddlrptBudget, BudgetTable, "BudgetId", "BudgetId");
                        UtilityUI.ddlFillByDataReaderWithAll(ddlrptCampus, BudgetDAO.CampusForDdl(Session["NavDBName"].ToString() + "[" + Session["Company"].ToString()), "CampusCode", "CampusCode");
                    }
                    else
                    {
                        lnkBudgetEntry.Enabled = lnkReview.Enabled = lnkApproveBudget.Enabled = lnkBudgetReport.Enabled = false;
                        UtilityUI.ShowAlert(this, "Create Budget Name From Navision. example 16-17 or 2016-2017. Then Try Again.");
                    }
                }
            }
        }
        catch{Response.Redirect("Default.aspx");}
    }

    protected void lnkBudgetEntry_Click(object sender, EventArgs e)
    {
        ViewState["BudgetScreen"] = "ENTRY";
        btnApprove.Visible = btnApprove.Enabled = btnReview.Visible = btnReview.Enabled = btnReject.Enabled = btnReject.Visible = false;
        btnSendForReview.Visible = btnSendForReview.Enabled = true;
        fillgrid(true);
        pnlReport.Visible = pnlReview.Visible = false;
        pnlEntry.Visible = true;
        Label2.Text = "Enter Budget";
    }
    protected void lnkReview_Click(object sender, EventArgs e)
    {
        rbtnReView.Checked = pnlReview.Visible = true;
        ClickOnReview();
    }
    protected void lnkApproveBudget_Click(object sender, EventArgs e)
    {
        ViewState["BudgetScreen"] = "APPROVE";
        btnReview.Visible = btnReview.Enabled = btnSendForReview.Visible = btnSendForReview.Enabled = false;
        btnApprove.Visible = btnApprove.Enabled = btnReject.Enabled = btnReject.Visible = true;
        fillgrid(true);
        pnlReport.Visible = pnlReview.Visible = false;
        pnlEntry.Visible = true;
        Label2.Text = "Approve Budget";
    }

    protected void ddlBudget_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBudget.SelectedIndex >= 0)
            {
                fillgrid(true);
            }
        }
        catch (Exception ex)
        {
            UtilityUI.ShowAlert(this, ex.Message);
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartment.SelectedIndex >= 0)
            {
                fillgrid(true);
            }
        }
        catch (Exception ex)
        {
            UtilityUI.ShowAlert(this, ex.Message);
        }
    }

    protected void GridViewHead_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.BackColor = System.Drawing.Color.White;
            HeaderCell.ColumnSpan = 3;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Previous Year Details";
            HeaderCell.ToolTip = "Previous Year Details";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.ColumnSpan = 3;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.BackColor = System.Drawing.Color.White;
            HeaderCell.ColumnSpan = 14;
            HeaderGridRow.Cells.Add(HeaderCell);

            GridViewHead.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
    }
    protected void GridViewHead_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            e.Row.Cells[1].HorizontalAlign = e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[3].HorizontalAlign = e.Row.Cells[4].HorizontalAlign = e.Row.Cells[5].HorizontalAlign = e.Row.Cells[6].HorizontalAlign =
            e.Row.Cells[7].HorizontalAlign = e.Row.Cells[8].HorizontalAlign = e.Row.Cells[9].HorizontalAlign = e.Row.Cells[10].HorizontalAlign =
            e.Row.Cells[11].HorizontalAlign = e.Row.Cells[12].HorizontalAlign = e.Row.Cells[13].HorizontalAlign = e.Row.Cells[14].HorizontalAlign =
            e.Row.Cells[15].HorizontalAlign = e.Row.Cells[16].HorizontalAlign = e.Row.Cells[17].HorizontalAlign = e.Row.Cells[18].HorizontalAlign = HorizontalAlign.Right;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[3].ToolTip = e.Row.Cells[4].ToolTip = e.Row.Cells[5].ToolTip = "Previous Year Details";
            }

            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HeadPYB += Convert.ToDecimal(e.Row.Cells[3].Text.Trim());
                HeadYTDB += Convert.ToDecimal(e.Row.Cells[4].Text.Trim());
                HeadYTDA += Convert.ToDecimal(e.Row.Cells[5].Text.Trim());
                HeadBudgetedAmount += Convert.ToDecimal(e.Row.Cells[6].Text.Trim());
                HeadApril += Convert.ToDecimal(e.Row.Cells[7].Text.Trim());
                HeadMay += Convert.ToDecimal(e.Row.Cells[8].Text.Trim());
                HeadJune += Convert.ToDecimal(e.Row.Cells[9].Text.Trim());
                HeadJuly += Convert.ToDecimal(e.Row.Cells[10].Text.Trim());
                HeadAugust += Convert.ToDecimal(e.Row.Cells[11].Text.Trim());
                HeadSept += Convert.ToDecimal(e.Row.Cells[12].Text.Trim());
                HeadOct += Convert.ToDecimal(e.Row.Cells[13].Text.Trim());
                HeadNov += Convert.ToDecimal(e.Row.Cells[14].Text.Trim());
                HeadDec += Convert.ToDecimal(e.Row.Cells[15].Text.Trim());
                HeadJan += Convert.ToDecimal(e.Row.Cells[16].Text.Trim());
                HeadFeb += Convert.ToDecimal(e.Row.Cells[17].Text.Trim());
                HeadMarch += Convert.ToDecimal(e.Row.Cells[18].Text.Trim());

                GridView gvBudget = new GridView();
                gvBudget = (GridView)e.Row.FindControl("GridViewBudget");

                //Check if any additional conditions (Paging, Sorting, Editing, etc) to be applied on child GridView
                if (gvBudget.UniqueID == gvBudgetUniqueID)
                {
                    gvBudget.EditIndex = gvBudgetEditIndex;
                    //Expand the Child grid
                    ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>divexpandcollapse('div" + GridViewHead.DataKeys[e.Row.RowIndex].Value.ToString() + "');</script>");
                }
                fillChildgrid(gvBudget, GridViewHead.DataKeys[e.Row.RowIndex].Value.ToString());
                if (gvBudget.UniqueID == gvBudgetUniqueID)
                {
                    ((TextBox)gvBudget.Rows[gvBudgetEditIndex].FindControl("txtApril")).Focus();
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total";
                e.Row.Cells[3].Text = HeadPYB.ToString();
                e.Row.Cells[4].Text = HeadYTDB.ToString();
                e.Row.Cells[5].Text = HeadYTDA.ToString();
                e.Row.Cells[6].Text = HeadBudgetedAmount.ToString();
                e.Row.Cells[7].Text = HeadApril.ToString();
                e.Row.Cells[8].Text = HeadMay.ToString();
                e.Row.Cells[9].Text = HeadJune.ToString();
                e.Row.Cells[10].Text = HeadJuly.ToString();
                e.Row.Cells[11].Text = HeadAugust.ToString();
                e.Row.Cells[12].Text = HeadSept.ToString();
                e.Row.Cells[13].Text = HeadOct.ToString();
                e.Row.Cells[14].Text = HeadNov.ToString();
                e.Row.Cells[15].Text = HeadDec.ToString();
                e.Row.Cells[16].Text = HeadJan.ToString();
                e.Row.Cells[17].Text = HeadFeb.ToString();
                e.Row.Cells[18].Text = HeadMarch.ToString();

                HeadPYB = HeadYTDB = HeadYTDA = HeadBudgetedAmount = HeadJan = HeadFeb = HeadMarch = HeadApril = HeadMay =
                    HeadJune = HeadJuly = HeadAugust = HeadSept = HeadOct = HeadNov = HeadDec = 0;
            }
        }
        catch (Exception ex)
        {
            UtilityUI.ShowAlert(this, ex.Message);
        }
    }
    protected void GridViewBudget_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            e.Row.Cells[0].HorizontalAlign = e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;

            e.Row.Cells[2].HorizontalAlign = e.Row.Cells[3].HorizontalAlign =
            e.Row.Cells[4].HorizontalAlign = e.Row.Cells[5].HorizontalAlign = e.Row.Cells[7].HorizontalAlign =
            e.Row.Cells[8].HorizontalAlign = e.Row.Cells[9].HorizontalAlign = e.Row.Cells[10].HorizontalAlign = e.Row.Cells[11].HorizontalAlign =
            e.Row.Cells[12].HorizontalAlign = e.Row.Cells[13].HorizontalAlign = e.Row.Cells[14].HorizontalAlign = e.Row.Cells[15].HorizontalAlign =
            e.Row.Cells[16].HorizontalAlign = e.Row.Cells[17].HorizontalAlign = e.Row.Cells[18].HorizontalAlign = HorizontalAlign.Right;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PYB += Convert.ToDecimal(e.Row.Cells[2].Text.Trim());
                YTDB += Convert.ToDecimal(e.Row.Cells[3].Text.Trim());
                YTDA += Convert.ToDecimal(e.Row.Cells[4].Text.Trim());
                BudgetedAmount += Convert.ToDecimal(e.Row.Cells[5].Text.Trim());
                April += Convert.ToDecimal(((Label)e.Row.FindControl("lblApril")).Text.Trim());
                May += Convert.ToDecimal(((Label)e.Row.FindControl("lblMay")).Text.Trim());
                June += Convert.ToDecimal(((Label)e.Row.FindControl("lblJune")).Text.Trim());
                July += Convert.ToDecimal(((Label)e.Row.FindControl("lblJuly")).Text.Trim());
                August += Convert.ToDecimal(((Label)e.Row.FindControl("lblAug")).Text.Trim());
                Sept += Convert.ToDecimal(((Label)e.Row.FindControl("lblSept")).Text.Trim());
                Oct += Convert.ToDecimal(((Label)e.Row.FindControl("lblOct")).Text.Trim());
                Nov += Convert.ToDecimal(((Label)e.Row.FindControl("lblNov")).Text.Trim());
                Dec += Convert.ToDecimal(((Label)e.Row.FindControl("lblDec")).Text.Trim());
                Jan += Convert.ToDecimal(((Label)e.Row.FindControl("lblJan")).Text.Trim());
                Feb += Convert.ToDecimal(((Label)e.Row.FindControl("lblFeb")).Text.Trim());
                March += Convert.ToDecimal(((Label)e.Row.FindControl("lblMarch")).Text.Trim());
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Total";
                e.Row.Cells[2].Text = PYB.ToString();
                e.Row.Cells[3].Text = YTDB.ToString();
                e.Row.Cells[4].Text = YTDA.ToString();
                e.Row.Cells[5].Text = BudgetedAmount.ToString();
                e.Row.Cells[7].Text = April.ToString();
                e.Row.Cells[8].Text = May.ToString();
                e.Row.Cells[9].Text = June.ToString();
                e.Row.Cells[10].Text = July.ToString();
                e.Row.Cells[11].Text = August.ToString();
                e.Row.Cells[12].Text = Sept.ToString();
                e.Row.Cells[13].Text = Oct.ToString();
                e.Row.Cells[14].Text = Nov.ToString();
                e.Row.Cells[15].Text = Dec.ToString();
                e.Row.Cells[16].Text = Jan.ToString();
                e.Row.Cells[17].Text = Feb.ToString();
                e.Row.Cells[18].Text = March.ToString();
                PYB = YTDB = YTDA = BudgetedAmount = Jan = Feb = March = April = May = June = July = August = Sept = Oct = Nov = Dec = 0;
            }
        }
        catch { }
    }
    protected void GridViewBudget_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvBudgetUniqueID = ((GridView)sender).UniqueID;
            gvBudgetEditIndex = e.NewEditIndex;
            fillgrid(false);
        }
        catch (Exception ex){ }
    }
    protected void GridViewBudget_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvBudgetUniqueID = ((GridView)sender).UniqueID;
        gvBudgetEditIndex = -1;
        fillgrid(false);
    }
    protected void GridViewBudget_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        BudgetDTO objDTO = null;
        GridView GVBudget = null;
        try
        {
            GVBudget = (GridView)sender;
            objDTO = new BudgetDTO();
            //objDTO.Id = e.Keys.Values.ToString();
            objDTO.Id = GVBudget.DataKeys[e.RowIndex].Value.ToString();
            objDTO.CompanyName = Session["Company"].ToString();
            objDTO.BudgetedAmount = Convert.ToDecimal(GVBudget.Rows[e.RowIndex].Cells[5].Text.Trim());
            objDTO.Jan = UtilityUI.IsValidDecimal(((TextBox)GVBudget.Rows[e.RowIndex].FindControl("txtJan")).Text.Trim()) ? Convert.ToDecimal(((TextBox)GVBudget.Rows[e.RowIndex].FindControl("txtJan")).Text.Trim()) : 0;
            objDTO.Feb = UtilityUI.IsValidDecimal(((TextBox)GVBudget.Rows[e.RowIndex].FindControl("txtFeb")).Text.Trim()) ? Convert.ToDecimal(((TextBox)GVBudget.Rows[e.RowIndex].FindControl("txtFeb")).Text.Trim()) : 0;
            objDTO.March = UtilityUI.IsValidDecimal(((TextBox)GVBudget.Rows[e.RowIndex].FindControl("txtMarch")).Text.Trim()) ? Convert.ToDecimal(((TextBox)GVBudget.Rows[e.RowIndex].FindControl("txtMarch")).Text.Trim()) : 0;
            objDTO.April = UtilityUI.IsValidDecimal(((TextBox)GVBudget.Rows[e.RowIndex].FindControl("txtApril")).Text.Trim()) ? Convert.ToDecimal(((TextBox)GVBudget.Rows[e.RowIndex].FindControl("txtApril")).Text.Trim()) : 0;
            objDTO.May = UtilityUI.IsValidDecimal(((TextBox)GVBudget.Rows[e.RowIndex].FindControl("txtMay")).Text.Trim()) ? Convert.ToDecimal(((TextBox)GVBudget.Rows[e.RowIndex].FindControl("txtMay")).Text.Trim()) : 0;
            objDTO.June = UtilityUI.IsValidDecimal(((TextBox)GVBudget.Rows[e.RowIndex].FindControl("txtJune")).Text.Trim()) ? Convert.ToDecimal(((TextBox)GVBudget.Rows[e.RowIndex].FindControl("txtJune")).Text.Trim()) : 0;
            objDTO.July = UtilityUI.IsValidDecimal(((TextBox)GVBudget.Rows[e.RowIndex].FindControl("txtJuly")).Text.Trim()) ? Convert.ToDecimal(((TextBox)GVBudget.Rows[e.RowIndex].FindControl("txtJuly")).Text.Trim()) : 0;
            objDTO.Aug = UtilityUI.IsValidDecimal(((TextBox)GVBudget.Rows[e.RowIndex].FindControl("txtAug")).Text.Trim()) ? Convert.ToDecimal(((TextBox)GVBudget.Rows[e.RowIndex].FindControl("txtAug")).Text.Trim()) : 0;
            objDTO.Sept = UtilityUI.IsValidDecimal(((TextBox)GVBudget.Rows[e.RowIndex].FindControl("txtSept")).Text.Trim()) ? Convert.ToDecimal(((TextBox)GVBudget.Rows[e.RowIndex].FindControl("txtSept")).Text.Trim()) : 0;
            objDTO.Oct = UtilityUI.IsValidDecimal(((TextBox)GVBudget.Rows[e.RowIndex].FindControl("txtOct")).Text.Trim()) ? Convert.ToDecimal(((TextBox)GVBudget.Rows[e.RowIndex].FindControl("txtOct")).Text.Trim()) : 0;
            objDTO.Nov = UtilityUI.IsValidDecimal(((TextBox)GVBudget.Rows[e.RowIndex].FindControl("txtNov")).Text.Trim()) ? Convert.ToDecimal(((TextBox)GVBudget.Rows[e.RowIndex].FindControl("txtNov")).Text.Trim()) : 0;
            objDTO.Dec = UtilityUI.IsValidDecimal(((TextBox)GVBudget.Rows[e.RowIndex].FindControl("txtDec")).Text.Trim()) ? Convert.ToDecimal(((TextBox)GVBudget.Rows[e.RowIndex].FindControl("txtDec")).Text.Trim()) : 0;
            BudgetDAO.UpdateBudgetDetails(objDTO, Session["uid"].ToString());
            gvBudgetUniqueID = ((GridView)sender).UniqueID;
            gvBudgetEditIndex = -1;
            fillgrid(false);
        }
        catch (Exception ex)
        {
            UtilityUI.ShowAlert(this, ex.Message);
        }
        finally
        {
            if (objDTO != null)
            {
                objDTO = null;
            }
            if (GVBudget != null)
            {
                GVBudget.Dispose();
                GVBudget = null;
            }
        }
    }
    protected void txtJan_TextChanged(object sender, EventArgs e)
    {
        try
        {
            using (GridViewRow CurrentRow = (GridViewRow)((TextBox)sender).Parent.Parent)
            {
                decimal BudgetedAmount = 0;
                BudgetedAmount = (UtilityUI.IsValidDecimal(((TextBox)CurrentRow.FindControl("txtJan")).Text.Trim()) ? Convert.ToDecimal(((TextBox)CurrentRow.FindControl("txtJan")).Text.Trim()) : 0) +
                                (UtilityUI.IsValidDecimal(((TextBox)CurrentRow.FindControl("txtFeb")).Text.Trim()) ? Convert.ToDecimal(((TextBox)CurrentRow.FindControl("txtFeb")).Text.Trim()) : 0) +
                                (UtilityUI.IsValidDecimal(((TextBox)CurrentRow.FindControl("txtMarch")).Text.Trim()) ? Convert.ToDecimal(((TextBox)CurrentRow.FindControl("txtMarch")).Text.Trim()) : 0) +
                                (UtilityUI.IsValidDecimal(((TextBox)CurrentRow.FindControl("txtApril")).Text.Trim()) ? Convert.ToDecimal(((TextBox)CurrentRow.FindControl("txtApril")).Text.Trim()) : 0) +
                                (UtilityUI.IsValidDecimal(((TextBox)CurrentRow.FindControl("txtMay")).Text.Trim()) ? Convert.ToDecimal(((TextBox)CurrentRow.FindControl("txtMay")).Text.Trim()) : 0) +
                                (UtilityUI.IsValidDecimal(((TextBox)CurrentRow.FindControl("txtJune")).Text.Trim()) ? Convert.ToDecimal(((TextBox)CurrentRow.FindControl("txtJune")).Text.Trim()) : 0) +
                                (UtilityUI.IsValidDecimal(((TextBox)CurrentRow.FindControl("txtJuly")).Text.Trim()) ? Convert.ToDecimal(((TextBox)CurrentRow.FindControl("txtJuly")).Text.Trim()) : 0) +
                                (UtilityUI.IsValidDecimal(((TextBox)CurrentRow.FindControl("txtAug")).Text.Trim()) ? Convert.ToDecimal(((TextBox)CurrentRow.FindControl("txtAug")).Text.Trim()) : 0) +
                                (UtilityUI.IsValidDecimal(((TextBox)CurrentRow.FindControl("txtSept")).Text.Trim()) ? Convert.ToDecimal(((TextBox)CurrentRow.FindControl("txtSept")).Text.Trim()) : 0) +
                                (UtilityUI.IsValidDecimal(((TextBox)CurrentRow.FindControl("txtOct")).Text.Trim()) ? Convert.ToDecimal(((TextBox)CurrentRow.FindControl("txtOct")).Text.Trim()) : 0) +
                                (UtilityUI.IsValidDecimal(((TextBox)CurrentRow.FindControl("txtNov")).Text.Trim()) ? Convert.ToDecimal(((TextBox)CurrentRow.FindControl("txtNov")).Text.Trim()) : 0) +
                                (UtilityUI.IsValidDecimal(((TextBox)CurrentRow.FindControl("txtDec")).Text.Trim()) ? Convert.ToDecimal(((TextBox)CurrentRow.FindControl("txtDec")).Text.Trim()) : 0);

                CurrentRow.Cells[5].Text = BudgetedAmount.ToString();
                using (GridViewRow CurrentHeadRow = (GridViewRow)CurrentRow.Parent.Parent.Parent.Parent)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>divexpandcollapse('div" + GridViewHead.DataKeys[CurrentHeadRow.RowIndex].Value.ToString() + "');</script>");
                }
                string ControlId = ((TextBox)sender).ID;
                if (ControlId == "txtMarch")
                {
                    ((LinkButton)CurrentRow.FindControl("lnkUpdate")).Focus();
                }
                else
                {
                    string NextControlId = ControlId == "txtApril" ? "txtMay" : ControlId == "txtMay" ? "txtJune" : ControlId == "txtJune" ? "txtJuly" :
                                        ControlId == "txtJuly" ? "txtAug" : ControlId == "txtAug" ? "txtSept" : ControlId == "txtSept" ? "txtOct" :
                                        ControlId == "txtOct" ? "txtNov" : ControlId == "txtNov" ? "txtDec" : ControlId == "txtDec" ? "txtJan" :
                                        ControlId == "txtJan" ? "txtFeb" : "txtMarch";
                    ((TextBox)CurrentRow.FindControl(NextControlId)).Focus();
                }
            }
        }
        catch (Exception ex)
        {
            UtilityUI.ShowAlert(this, ex.Message);
        }
    }
    protected void lnkCopy_Click(object sender, EventArgs e)
    {
        try
        {
            decimal CopyJan, CopyFeb, CopyMarch, CopyApril, CopyMay, CopyJune, CopyJuly, CopyAugust, CopySept, CopyOct, CopyNov, CopyDec;
            GridViewRow CurrentRow = (GridViewRow)(((LinkButton)sender).Parent.Parent);
            CopyMarch = (UtilityUI.IsValidDecimal(((TextBox)CurrentRow.FindControl("txtMarch")).Text.Trim()) ? Convert.ToDecimal(((TextBox)CurrentRow.FindControl("txtMarch")).Text.Trim()) : 0);
            if (CopyMarch == 0)
            {
                CopyFeb = (UtilityUI.IsValidDecimal(((TextBox)CurrentRow.FindControl("txtFeb")).Text.Trim()) ? Convert.ToDecimal(((TextBox)CurrentRow.FindControl("txtFeb")).Text.Trim()) : 0);
                CopyJan = (UtilityUI.IsValidDecimal(((TextBox)CurrentRow.FindControl("txtJan")).Text.Trim()) ? Convert.ToDecimal(((TextBox)CurrentRow.FindControl("txtJan")).Text.Trim()) : 0);
                CopyDec = (UtilityUI.IsValidDecimal(((TextBox)CurrentRow.FindControl("txtDec")).Text.Trim()) ? Convert.ToDecimal(((TextBox)CurrentRow.FindControl("txtDec")).Text.Trim()) : 0);
                CopyNov = (UtilityUI.IsValidDecimal(((TextBox)CurrentRow.FindControl("txtNov")).Text.Trim()) ? Convert.ToDecimal(((TextBox)CurrentRow.FindControl("txtNov")).Text.Trim()) : 0);
                CopyOct = (UtilityUI.IsValidDecimal(((TextBox)CurrentRow.FindControl("txtOct")).Text.Trim()) ? Convert.ToDecimal(((TextBox)CurrentRow.FindControl("txtOct")).Text.Trim()) : 0);
                CopySept = (UtilityUI.IsValidDecimal(((TextBox)CurrentRow.FindControl("txtSept")).Text.Trim()) ? Convert.ToDecimal(((TextBox)CurrentRow.FindControl("txtSept")).Text.Trim()) : 0);
                CopyAugust = (UtilityUI.IsValidDecimal(((TextBox)CurrentRow.FindControl("txtAug")).Text.Trim()) ? Convert.ToDecimal(((TextBox)CurrentRow.FindControl("txtAug")).Text.Trim()) : 0);
                CopyJuly = (UtilityUI.IsValidDecimal(((TextBox)CurrentRow.FindControl("txtJuly")).Text.Trim()) ? Convert.ToDecimal(((TextBox)CurrentRow.FindControl("txtJuly")).Text.Trim()) : 0);
                CopyJune = (UtilityUI.IsValidDecimal(((TextBox)CurrentRow.FindControl("txtJune")).Text.Trim()) ? Convert.ToDecimal(((TextBox)CurrentRow.FindControl("txtJune")).Text.Trim()) : 0);
                CopyMay = (UtilityUI.IsValidDecimal(((TextBox)CurrentRow.FindControl("txtMay")).Text.Trim()) ? Convert.ToDecimal(((TextBox)CurrentRow.FindControl("txtMay")).Text.Trim()) : 0);
                CopyApril = (UtilityUI.IsValidDecimal(((TextBox)CurrentRow.FindControl("txtApril")).Text.Trim()) ? Convert.ToDecimal(((TextBox)CurrentRow.FindControl("txtApril")).Text.Trim()) : 0);

                string Str = CopyFeb != 0 ? "txtFeb" : CopyJan != 0 ? "txtJan" : CopyDec != 0 ? "txtDec" : CopyNov != 0 ? "txtNov" :
                                CopyOct != 0 ? "txtOct" : CopySept != 0 ? "txtSept" : CopyAugust != 0 ? "txtAug" : CopyJuly != 0 ? "txtJuly" :
                                CopyJune != 0 ? "txtJune" : CopyMay != 0 ? "txtMay" : "txtApril";

                decimal value = CopyFeb != 0 ? CopyFeb : CopyJan != 0 ? CopyJan : CopyDec != 0 ? CopyDec : CopyNov != 0 ? CopyNov :
                                CopyOct != 0 ? CopyOct : CopySept != 0 ? CopySept : CopyAugust != 0 ? CopyAugust : CopyJuly != 0 ? CopyJuly :
                                CopyJune != 0 ? CopyJune : CopyMay != 0 ? CopyMay : CopyApril;

                string[] Month = { "txtApril", "txtMay", "txtJune", "txtJuly", "txtAug", "txtSept", "txtOct", "txtNov", "txtDec", "txtJan", "txtFeb", "txtMarch" };
                bool nextMonth = false;
                decimal BudgetedAmount;
                BudgetedAmount = CopyJan + CopyFeb + CopyMarch + CopyApril + CopyMay + CopyJune + CopyJuly + CopyAugust + CopySept + CopyOct + CopyNov + CopyDec;
                foreach (string s in Month)
                {
                    if (s != Str && !nextMonth)
                    {
                        continue;
                    }
                    ((TextBox)CurrentRow.FindControl(s)).Text = value.ToString();
                    BudgetedAmount += s != Str ? value : 0;
                    nextMonth = true;
                }
                CurrentRow.Cells[5].Text = BudgetedAmount.ToString();
                ((LinkButton)CurrentRow.FindControl("lnkUpdate")).Focus();
            }
            using (GridViewRow CurrentHeadRow = (GridViewRow)CurrentRow.Parent.Parent.Parent.Parent)
            {
                ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>divexpandcollapse('div" + GridViewHead.DataKeys[CurrentHeadRow.RowIndex].Value.ToString() + "');</script>");
            }
        }
        catch (Exception ex)
        {
            UtilityUI.ShowAlert(this, ex.Message);
        }

    }

    protected void btnSendForReview_Click(object sender, EventArgs e)
    {
        try
        {
            btnSendForReview.Enabled = false;
            //if (btnSendForReview.Enabled)
            //{
                BudgetDAO.BudgetSendForReview(Session["Company"].ToString(), ddlBudget.SelectedValue.ToString(), ddlDepartment.SelectedValue.ToString(), Session["GlobalDimension1Code"].ToString(), Session["uid"].ToString());
                fillgrid(true);
                SendMail();
            ///}
        }
        catch (Exception ex)
        {
            UtilityUI.ShowAlert(this, ex.Message);
        }
    }
    protected void btnReview_Click(object sender, EventArgs e)
    {
        try
        {
            //if (btnReview.Enabled)
            //{
                BudgetDAO.BudgetSendForApproval(Session["Company"].ToString(), ddlBudget.SelectedValue.ToString(), ddlDepartment.SelectedValue.ToString(), Session["GlobalDimension1Code"].ToString(), txtRemark.InnerText.Trim(), Session["uid"].ToString());
                fillgrid(true);
                SendMail();
            //}
        }
        catch (Exception ex)
        {
            UtilityUI.ShowAlert(this, ex.Message);
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            //if (btnApprove.Enabled)
            //{
            BudgetDAO.BudgetApprove(Session["NavDBName"].ToString(), Session["Company"].ToString(), ddlBudget.SelectedValue.ToString(), ddlDepartment.SelectedValue.ToString(), Session["GlobalDimension1Code"].ToString(), txtRemark.InnerText.Trim(), Session["uid"].ToString());
            fillgrid(true);
            SendMail();
            //}
        }
        catch (Exception ex)
        {
            UtilityUI.ShowAlert(this, ex.Message);
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            //if (btnReject.Enabled)
            //{
                btnReject.Enabled = false;
                BudgetDAO.BudgetReject(Session["Company"].ToString(), ddlBudget.SelectedValue.ToString(), ddlDepartment.SelectedValue.ToString(), Session["GlobalDimension1Code"].ToString(), txtRemark.InnerText.Trim(), Session["uid"].ToString());
                fillgrid(true);
                SendMail();
            //}
        }
        catch (Exception ex)
        {
            UtilityUI.ShowAlert(this, ex.Message);
        }
    }
    protected void rbtnReView_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtnReView.Checked)
        {
            ClickOnReview();
        }
        else
        {
            ClickOnReport();
        }
    }
    #endregion 
   
    # region BudgetReport
    void FillDataList()
    {
        UtilityUI.FillDatalist(DataListDepartment, rbtnDepartmentWise.Checked ? (DataTable) ViewState["DepartmentTable"] : BudgetDAO.GLForDdl(Session["NavDBName"].ToString() + "[" + Session["Company"].ToString(), ViewState["GLRange"].ToString()), rbtnDepartmentWise.Checked ? "DepartmentCode" : "Id");
    }
    void ClickOnReport()
    {
        pnlEntry.Visible = false;
        pnlReport.Visible = true;
        rbtnHeadWise.Checked = false;
        rbtnDepartmentWise.Checked = chkSelect.Checked = true;
        DataListDepartment.RepeatColumns = 7;
        FillDataList();
        GridViewBudgetrpt.DataBind();
        Label2.Text = "Budget Report";
    }
    protected void lnkBudgetReport_Click(object sender, EventArgs e)
    {
        pnlReview.Visible = false;
        ClickOnReport();
    }
    protected void btnGet_Click(object sender, EventArgs e)
    {
        try
        {
            string DepartmentList ="";
                Int32 Count = 0;
                foreach (DataListItem dli in DataListDepartment.Items)
                {
                    if (((CheckBox)dli.FindControl("Chk")).Checked)
                    {
                        //DepartmentList += "," + ((CheckBox)dli.FindControl("Chk")).Text;
                        DepartmentList += "," + DataListDepartment.DataKeys[Count].ToString();
                    }
                    Count++;
                }
                if (DepartmentList.Length == 0)
                {
                    UtilityUI.ShowAlert(this, rbtnDepartmentWise.Checked ? "Select Atleast One GL." : "Select Atleast One Department.");
                    return;
                }
            string EmployeeDepartment = "";

            foreach(DataRow dr in ((DataTable)ViewState["DepartmentTable"]).Rows)
            {
                EmployeeDepartment += ","+ dr["DepartmentCode"].ToString();
            }

            UtilityUI.FillGrid(GridViewBudgetrpt, BudgetDAO.rptBudgetByDepartment(Session["NavDBName"].ToString(), Session["Company"].ToString(), ddlrptBudget.SelectedValue.ToString(), DepartmentList, ddlrptCampus.SelectedValue.ToString(), rbtnDepartmentWise.Checked, chkQuaterly.Checked, Session["uid"].ToString(), EmployeeDepartment));
        }
        catch (Exception ex)
        {
            UtilityUI.ShowAlert(this, ex.Message);
        }
    }
    protected void GridViewBudgetrpt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewBudgetrpt.Width = chkQuaterly.Checked ? 2600 : 2200;
            }
            for (int i = rbtnDepartmentWise.Checked?2:1; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[i].Width = 100;

                    QuarterlyCells += chkQuaterly.Checked && ((e.Row.Cells[i].Text == "1stQuater") || (e.Row.Cells[i].Text == "2ndQuater") || 
                                      (e.Row.Cells[i].Text == "3rdQuater") || (e.Row.Cells[i].Text == "4thQuater"))  ? ","+i.ToString() : "";
                }
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            }
            if (chkQuaterly.Checked)
            {
                foreach (string s in QuarterlyCells.Split(','))
                {
                    if (s.Trim().Length > 0) e.Row.Cells[Convert.ToInt32(s)].BackColor = System.Drawing.ColorTranslator.FromHtml("#CCCCCC");
                }
            }
            if (rbtnDepartmentWise.Checked && !chkQuaterly.Checked)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    BudgetedAmount += Convert.ToDecimal(e.Row.Cells[2].Text.Trim());
                    PYB += Convert.ToDecimal(e.Row.Cells[3].Text.Trim());
                    YTDB += Convert.ToDecimal(e.Row.Cells[4].Text.Trim());
                    YTDA += Convert.ToDecimal(e.Row.Cells[5].Text.Trim());
                    April += Convert.ToDecimal(e.Row.Cells[6].Text.Trim());
                    May += Convert.ToDecimal(e.Row.Cells[7].Text.Trim());
                    June += Convert.ToDecimal(e.Row.Cells[8].Text.Trim());
                    July += Convert.ToDecimal(e.Row.Cells[9].Text.Trim());
                    August += Convert.ToDecimal(e.Row.Cells[10].Text.Trim());
                    Sept += Convert.ToDecimal(e.Row.Cells[11].Text.Trim());
                    Oct += Convert.ToDecimal(e.Row.Cells[12].Text.Trim());
                    Nov += Convert.ToDecimal(e.Row.Cells[13].Text.Trim());
                    Dec += Convert.ToDecimal(e.Row.Cells[14].Text.Trim());
                    Jan += Convert.ToDecimal(e.Row.Cells[15].Text.Trim());
                    Feb += Convert.ToDecimal(e.Row.Cells[16].Text.Trim());
                    March += Convert.ToDecimal(e.Row.Cells[17].Text.Trim());
                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[1].Text = "Total";
                    e.Row.Cells[2].Text = BudgetedAmount.ToString();
                    e.Row.Cells[3].Text = PYB.ToString();
                    e.Row.Cells[4].Text = YTDB.ToString();
                    e.Row.Cells[5].Text = YTDA.ToString();
                    e.Row.Cells[6].Text = April.ToString();
                    e.Row.Cells[7].Text = May.ToString();
                    e.Row.Cells[8].Text = June.ToString();
                    e.Row.Cells[9].Text = July.ToString();
                    e.Row.Cells[10].Text = August.ToString();
                    e.Row.Cells[11].Text = Sept.ToString();
                    e.Row.Cells[12].Text = Oct.ToString();
                    e.Row.Cells[13].Text = Nov.ToString();
                    e.Row.Cells[14].Text = Dec.ToString();
                    e.Row.Cells[15].Text = Jan.ToString();
                    e.Row.Cells[16].Text = Feb.ToString();
                    e.Row.Cells[17].Text = March.ToString();
                }
            }
            else if (rbtnDepartmentWise.Checked && chkQuaterly.Checked)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    BudgetedAmount += Convert.ToDecimal(e.Row.Cells[2].Text.Trim());
                    PYB += Convert.ToDecimal(e.Row.Cells[3].Text.Trim());
                    YTDB += Convert.ToDecimal(e.Row.Cells[4].Text.Trim());
                    YTDA += Convert.ToDecimal(e.Row.Cells[5].Text.Trim());
                    April += Convert.ToDecimal(e.Row.Cells[6].Text.Trim());
                    May += Convert.ToDecimal(e.Row.Cells[7].Text.Trim());
                    June += Convert.ToDecimal(e.Row.Cells[8].Text.Trim());
                    FirstQuater += Convert.ToDecimal(e.Row.Cells[9].Text.Trim());

                    July += Convert.ToDecimal(e.Row.Cells[10].Text.Trim());
                    August += Convert.ToDecimal(e.Row.Cells[11].Text.Trim());
                    Sept += Convert.ToDecimal(e.Row.Cells[12].Text.Trim());
                    SecondQuater += Convert.ToDecimal(e.Row.Cells[13].Text.Trim());

                    Oct += Convert.ToDecimal(e.Row.Cells[14].Text.Trim());
                    Nov += Convert.ToDecimal(e.Row.Cells[15].Text.Trim());
                    Dec += Convert.ToDecimal(e.Row.Cells[16].Text.Trim());
                    ThirdQuater += Convert.ToDecimal(e.Row.Cells[17].Text.Trim());

                    Jan += Convert.ToDecimal(e.Row.Cells[18].Text.Trim());
                    Feb += Convert.ToDecimal(e.Row.Cells[19].Text.Trim());
                    March += Convert.ToDecimal(e.Row.Cells[20].Text.Trim());
                    FourthQuater += Convert.ToDecimal(e.Row.Cells[21].Text.Trim());

                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[1].Text = "Total";
                    e.Row.Cells[2].Text = BudgetedAmount.ToString();
                    e.Row.Cells[3].Text = PYB.ToString();
                    e.Row.Cells[4].Text = YTDB.ToString();
                    e.Row.Cells[5].Text = YTDA.ToString();
                    e.Row.Cells[6].Text = April.ToString();
                    e.Row.Cells[7].Text = May.ToString();
                    e.Row.Cells[8].Text = June.ToString();
                    e.Row.Cells[9].Text = FirstQuater.ToString();
                    e.Row.Cells[10].Text = July.ToString();
                    e.Row.Cells[11].Text = August.ToString();
                    e.Row.Cells[12].Text = Sept.ToString();
                    e.Row.Cells[13].Text = SecondQuater.ToString();
                    e.Row.Cells[14].Text = Oct.ToString();
                    e.Row.Cells[15].Text = Nov.ToString();
                    e.Row.Cells[16].Text = Dec.ToString();
                    e.Row.Cells[17].Text = ThirdQuater.ToString();
                    e.Row.Cells[18].Text = Jan.ToString();
                    e.Row.Cells[19].Text = Feb.ToString();
                    e.Row.Cells[20].Text = March.ToString();
                    e.Row.Cells[21].Text = FourthQuater.ToString();
                }
            }
            else if (rbtnHeadWise.Checked && !chkQuaterly.Checked)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    BudgetedAmount += Convert.ToDecimal(e.Row.Cells[1].Text.Trim());
                    PYB += Convert.ToDecimal(e.Row.Cells[2].Text.Trim());
                    YTDB += Convert.ToDecimal(e.Row.Cells[3].Text.Trim());
                    YTDA += Convert.ToDecimal(e.Row.Cells[4].Text.Trim());
                    April += Convert.ToDecimal(e.Row.Cells[5].Text.Trim());
                    May += Convert.ToDecimal(e.Row.Cells[6].Text.Trim());
                    June += Convert.ToDecimal(e.Row.Cells[7].Text.Trim());
                    July += Convert.ToDecimal(e.Row.Cells[8].Text.Trim());
                    August += Convert.ToDecimal(e.Row.Cells[9].Text.Trim());
                    Sept += Convert.ToDecimal(e.Row.Cells[10].Text.Trim());
                    Oct += Convert.ToDecimal(e.Row.Cells[11].Text.Trim());
                    Nov += Convert.ToDecimal(e.Row.Cells[12].Text.Trim());
                    Dec += Convert.ToDecimal(e.Row.Cells[13].Text.Trim());
                    Jan += Convert.ToDecimal(e.Row.Cells[14].Text.Trim());
                    Feb += Convert.ToDecimal(e.Row.Cells[15].Text.Trim());
                    March += Convert.ToDecimal(e.Row.Cells[16].Text.Trim());
                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[0].Text = "Total";
                    e.Row.Cells[1].Text = BudgetedAmount.ToString();
                    e.Row.Cells[2].Text = PYB.ToString();
                    e.Row.Cells[3].Text = YTDB.ToString();
                    e.Row.Cells[4].Text = YTDA.ToString();
                    e.Row.Cells[5].Text = April.ToString();
                    e.Row.Cells[6].Text = May.ToString();
                    e.Row.Cells[7].Text = June.ToString();
                    e.Row.Cells[8].Text = July.ToString();
                    e.Row.Cells[9].Text = August.ToString();
                    e.Row.Cells[10].Text = Sept.ToString();
                    e.Row.Cells[11].Text = Oct.ToString();
                    e.Row.Cells[12].Text = Nov.ToString();
                    e.Row.Cells[13].Text = Dec.ToString();
                    e.Row.Cells[14].Text = Jan.ToString();
                    e.Row.Cells[15].Text = Feb.ToString();
                    e.Row.Cells[16].Text = March.ToString();
                }
            }
            else if (rbtnHeadWise.Checked && chkQuaterly.Checked)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    BudgetedAmount += Convert.ToDecimal(e.Row.Cells[1].Text.Trim());
                    PYB += Convert.ToDecimal(e.Row.Cells[2].Text.Trim());
                    YTDB += Convert.ToDecimal(e.Row.Cells[3].Text.Trim());
                    YTDA += Convert.ToDecimal(e.Row.Cells[4].Text.Trim());
                    April += Convert.ToDecimal(e.Row.Cells[5].Text.Trim());
                    May += Convert.ToDecimal(e.Row.Cells[6].Text.Trim());
                    June += Convert.ToDecimal(e.Row.Cells[7].Text.Trim());
                    FirstQuater += Convert.ToDecimal(e.Row.Cells[8].Text.Trim());

                    July += Convert.ToDecimal(e.Row.Cells[9].Text.Trim());
                    August += Convert.ToDecimal(e.Row.Cells[10].Text.Trim());
                    Sept += Convert.ToDecimal(e.Row.Cells[11].Text.Trim());
                    SecondQuater += Convert.ToDecimal(e.Row.Cells[12].Text.Trim());

                    Oct += Convert.ToDecimal(e.Row.Cells[13].Text.Trim());
                    Nov += Convert.ToDecimal(e.Row.Cells[14].Text.Trim());
                    Dec += Convert.ToDecimal(e.Row.Cells[15].Text.Trim());
                    ThirdQuater += Convert.ToDecimal(e.Row.Cells[16].Text.Trim());

                    Jan += Convert.ToDecimal(e.Row.Cells[17].Text.Trim());
                    Feb += Convert.ToDecimal(e.Row.Cells[18].Text.Trim());
                    March += Convert.ToDecimal(e.Row.Cells[19].Text.Trim());
                    FourthQuater += Convert.ToDecimal(e.Row.Cells[20].Text.Trim());

                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[0].Text = "Total";
                    e.Row.Cells[1].Text = BudgetedAmount.ToString();
                    e.Row.Cells[2].Text = PYB.ToString();
                    e.Row.Cells[3].Text = YTDB.ToString();
                    e.Row.Cells[4].Text = YTDA.ToString();
                    e.Row.Cells[5].Text = April.ToString();
                    e.Row.Cells[6].Text = May.ToString();
                    e.Row.Cells[7].Text = June.ToString();
                    e.Row.Cells[8].Text = FirstQuater.ToString();
                    e.Row.Cells[9].Text = July.ToString();
                    e.Row.Cells[10].Text = August.ToString();
                    e.Row.Cells[11].Text = Sept.ToString();
                    e.Row.Cells[12].Text = SecondQuater.ToString();
                    e.Row.Cells[13].Text = Oct.ToString();
                    e.Row.Cells[14].Text = Nov.ToString();
                    e.Row.Cells[15].Text = Dec.ToString();
                    e.Row.Cells[16].Text = ThirdQuater.ToString();
                    e.Row.Cells[17].Text = Jan.ToString();
                    e.Row.Cells[18].Text = Feb.ToString();
                    e.Row.Cells[19].Text = March.ToString();
                    e.Row.Cells[20].Text = FourthQuater.ToString();
                }
            }
        }
        catch (Exception ex)
        { 
            
        }
    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        foreach (DataListItem dli in DataListDepartment.Items)
        {
            ((CheckBox)dli.FindControl("Chk")).Checked = chkSelect.Checked;
        }
        GridViewBudgetrpt.DataBind();
    }
    protected void Chk_CheckedChanged(object sender, EventArgs e)
    {
        chkSelect.Checked = true;
        foreach (DataListItem dli in DataListDepartment.Items)
        {
            if (!((CheckBox)dli.FindControl("Chk")).Checked)
            {
                chkSelect.Checked = false;
                break;
            }
        }
        GridViewBudgetrpt.DataBind();
    }
    protected void rbtnHeadWise_CheckedChanged(object sender, EventArgs e)
    {
        chkSelect.Checked = true;
        DataListDepartment.RepeatColumns = rbtnDepartmentWise.Checked ? 7 : 3;
        FillDataList();
        GridViewBudgetrpt.DataBind();
    }
    protected void chkQuaterly_CheckedChanged(object sender, EventArgs e)
    {
        GridViewBudgetrpt.DataBind();
    }
    protected void ddlrptBudget_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewBudgetrpt.DataBind();
    }
    protected void ddlrptCampus_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewBudgetrpt.DataBind();
    }
    #endregion
   
}