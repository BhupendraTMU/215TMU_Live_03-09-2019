using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Configuration;//ashuss
public partial class Faculty_CLaimReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnFIlterGet_Approval_Click(object sender, EventArgs e)
    {
        Show_HODData();
    }
    SqlDataReader dr;
    public void Show_HODData()
    {
        ServicePoratal con = new ServicePoratal();

        if (rdEmployeeID.Checked == false && rdEmployeeName.Checked == false && rdDatewise.Checked == false)
        {
           dr = con.Show_CoApplicationforApprovalPendingHR(Session["Company"].ToString().Trim());
        
        }
        if (rdEmployeeID.Checked == true)
        {
           dr = con.Show_COApplicationByHODWithEMPIDHR(txtEmployeeIDNameFilter.Text.Trim(), Session["Company"].ToString().Trim());
          
        }

        if (rdEmployeeName.Checked == true)
        {


           dr = con.Show_COApplicationByHODWithEMPNAmeHR(txtEmployeeIDNameFilter.Text.Trim(), Session["Company"].ToString().Trim());
         
        }

        if (rdDatewise.Checked == true)
        {

            dr = con.Show_COApplicationByHODWithDatewiseHR(txtFromDate_Approval.Text.Trim(), txtTodate_Approval.Text.Trim(), Session["Company"].ToString().Trim());
        }

        DataTable dt = new DataTable();
        dt.Load(dr);
        grdApproval.DataSource = dt;
        grdApproval.DataBind();
        dr.Close();
        con.DisConnect();

    }

    protected void rdDatewise_CheckedChanged(object sender, EventArgs e)
    {
        pnlFilterByIDName.Visible = false;
        pnlFilterDate.Visible = true;
        btnFIlterGet_Approval.Visible = true;
      
    }

    protected void grdApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdApproval.PageIndex = e.NewPageIndex;
        Show_HODData();
    }

    protected void rdEmployeeID_CheckedChanged(object sender, EventArgs e)
    {
        pnlFilterByIDName.Visible = true;
        pnlFilterDate.Visible = false;
        btnFIlterGet_Approval.Visible = true;
        lblEmployeeIDNameText.Text = "Employee ID";
        
    }

    protected void rdEmployeeName_CheckedChanged(object sender, EventArgs e)
    {
        pnlFilterByIDName.Visible = true;
        pnlFilterDate.Visible = false;
        btnFIlterGet_Approval.Visible = true;
        lblEmployeeIDNameText.Text = "Employee Name";
        
    }

    protected void btnApproveExport_Click(object sender, EventArgs e)
    {
        try
        {
            grdApproval.AllowPaging = false;
            Show_HODData();
            //mdStaffStatus.Show();
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=COApproveddata.xls");
            Response.Charset = "";
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter ht = new HtmlTextWriter(sw);


            grdApproval.HeaderRow.Style.Add("background-color", "#FFFFFF");
            foreach (TableCell tablecell in grdApproval.HeaderRow.Cells)
            {
                tablecell.Style["background-color"] = "#507CD1";
            }

            foreach (GridViewRow gridviewrow in grdApproval.Rows)
            {
                gridviewrow.BackColor = System.Drawing.Color.White;
                foreach (TableCell gridViewRowTableCell in gridviewrow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#EFF3FB";
                }
            }

            grdApproval.RenderControl(ht);
            Response.Write(sw.ToString());
            Response.End();
        }
        catch (Exception)
        { }
    }
}