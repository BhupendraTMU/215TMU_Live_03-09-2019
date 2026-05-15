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
public partial class LeaveReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Departmentcode"].ToString().Trim() == "D213")
            { }
            else
            {
                Response.Redirect("../Default.aspx");
            }
        }
        catch (Exception)
        { 
        
        }
    }
    protected void rdApprovedEmpid_CheckedChanged(object sender, EventArgs e)
    {
        pnlLeaveApprovedDetailEmpid.Visible = true;
        pnlLeaveApprovedDetailDatewise.Visible = false;
        btnApprovedSearch.Visible = true;
        txtResolvedSearch.Text = "";
        txtApprovedFromDate.Text = "";
        txtApprovedToDate.Text = "";
        grdResolved.Visible = false;
    }
    protected void rdApprovedEMPName_CheckedChanged(object sender, EventArgs e)
    {
        pnlLeaveApprovedDetailEmpid.Visible = true;
        pnlLeaveApprovedDetailDatewise.Visible = false;
        btnApprovedSearch.Visible = true;
       
        txtResolvedSearch.Text = "";
        txtApprovedFromDate.Text = "";
        txtApprovedToDate.Text = "";
       
        grdResolved.Visible = false;
    }
    protected void rdApprovedDatewise_CheckedChanged(object sender, EventArgs e)
    {
        pnlLeaveApprovedDetailEmpid.Visible = false;
        pnlLeaveApprovedDetailDatewise.Visible = true;
        btnApprovedSearch.Visible = true;
        
        txtResolvedSearch.Text = "";
        txtApprovedFromDate.Text = "";
        txtApprovedToDate.Text = "";
      
        grdResolved.Visible = false;
    }
    protected void rdAllApprove_CheckedChanged(object sender, EventArgs e)
    {
        pnlLeaveApprovedDetailEmpid.Visible = false;
        pnlLeaveApprovedDetailDatewise.Visible = false;
        btnApprovedSearch.Visible = false;
        
        txtResolvedSearch.Text = "";
        txtApprovedFromDate.Text = "";
        txtApprovedToDate.Text = "";
        LeaveResolved_Search();
        grdResolved.Visible = true;
        
    }
    protected void btnApprovedSearch_Click(object sender, EventArgs e)
    {
        grdResolved.Visible = true;
        LeaveResolved_Search();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExportApprove_Click(object sender, EventArgs e)
    {
        try
        {
        grdResolved.AllowPaging = false;
        LeaveResolved_Search();
            //mdStaffStatus.Show();
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Leavedata.xls");
            Response.Charset = "";
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter ht = new HtmlTextWriter(sw);


            grdResolved.HeaderRow.Style.Add("background-color", "#FFFFFF");
            foreach (TableCell tablecell in grdResolved.HeaderRow.Cells)
            {
                tablecell.Style["background-color"] = "#507CD1";
            }

            foreach (GridViewRow gridviewrow in grdResolved.Rows)
            {
                gridviewrow.BackColor = System.Drawing.Color.White;
                foreach (TableCell gridViewRowTableCell in gridviewrow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#EFF3FB";
                }
            }

            grdResolved.RenderControl(ht);
            Response.Write(sw.ToString());
            Response.End();
        }
        catch (Exception)
        { }
    }
    protected void grdResolved_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdResolved.PageIndex = e.NewPageIndex;
        LeaveResolved_Search();
    }
    SqlDataReader odr;
    public void LeaveResolved_Search()
    {

        ServicePoratal con = new ServicePoratal();
        if (rdApprovedDatewise.Checked == true)
        {

            odr = con.Leave_Approval_Resolved_withdateHR24(txtApprovedFromDate.Text.Trim(), txtApprovedToDate.Text.Trim(), Session["Company"].ToString().Trim());
            
        }

        if (rdApprovedEmpid.Checked == true)
        {

            odr = con.Leave_Approval_Resolved_withUserIDHR24( Session["Company"].ToString().Trim(), txtResolvedSearch.Text.Trim());
            
        }

        if (rdApprovedEMPName.Checked == true)
        {

            odr = con.Leave_Approval_Resolved_withUserNameHR24( Session["Company"].ToString().Trim(), txtResolvedSearch.Text.Trim());
                   
        }
        if (rdAllApprove.Checked == true)
        {

            odr = con.Leave_Approval_Resolved_withUserNameAllHR24(Session["Company"].ToString().Trim(), txtResolvedSearch.Text.Trim());
            
        }
        DataTable Dt = new DataTable();
        Dt.Load(odr);
        grdResolved.DataSource = Dt;
        grdResolved.DataBind();
        odr.Close();
        con.DisConnect();

    }

    protected void grdResolved_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Button btnworkingDetailsApproved = (Button)e.Row.FindControl("btnworkingDetailsApproved");
            Label lblLeavetypeAppliedApproved = (Label)e.Row.FindControl("lblLeavetypeAppliedApproved");

            Label lblleaveAttachmentFilename = (Label)e.Row.FindControl("lblleaveAttachmentFilenameApprove");
            LinkButton lnkDownloadgrid = (LinkButton)e.Row.FindControl("lnkDownloadgridApprove");
            if (lblleaveAttachmentFilename.Text.Trim() == "")
            {
                lnkDownloadgrid.Visible = false;
            }

            if (lblleaveAttachmentFilename.Text.Trim() != "")
            {
                lnkDownloadgrid.Visible = true;
            }

            if (lblLeavetypeAppliedApproved.Text == "CO")
            {
                btnworkingDetailsApproved.Visible = true;
            }
            if (lblLeavetypeAppliedApproved.Text != "CO")
            {
                btnworkingDetailsApproved.Visible = false;
            }
        }
    }

    protected void btnworkingDetailsApproved_Command(object sender, CommandEventArgs e)
    {
        
        string AutoNo = e.CommandArgument.ToString();

        ShowGridworkingdetails(AutoNo);
        ModalPopupExtender2.Show();

    }
    public void ShowGridworkingdetails(string AutoNo)
    {
        ServicePoratal con = new ServicePoratal();
        SqlDataReader dr = con.Show_tble_cowithAutono(AutoNo);

        DataTable dt = new DataTable();
        dt.Load(dr);
        grdworkingdetails.DataSource = dt;
        grdworkingdetails.DataBind();
        dr.Close();
        con.DisConnect();

    }

    protected void DownloadFile(object sender, EventArgs e)
    {
        try
        {
            ServicePoratal con = new ServicePoratal();
            int id = int.Parse((sender as LinkButton).CommandArgument);
            byte[] bytes;
            string fileName, contentType;
            SqlConnection Conn = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]);
            Conn.Open();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select AttachmentFilename, Attachmentdata, AttachmentFileType from tble_Leave_Approval where AutoNo=@AutoNo";
                cmd.Parameters.AddWithValue("@AutoNo", id);
                cmd.Connection = Conn;
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Attachmentdata"];
                    contentType = sdr["AttachmentFileType"].ToString();
                    fileName = sdr["AttachmentFilename"].ToString();
                }
                con.DisConnect();

            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Attachment not found ');", true);

        }
    }

}