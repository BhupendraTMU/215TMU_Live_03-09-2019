using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Faculty_TMVF_Approval : System.Web.UI.Page
{
    SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            getFellowshipdata();
            BindYear();
            ddlMonth.SelectedValue = System.DateTime.Now.ToString("MM");
            ddlYear.SelectedValue = System.DateTime.Now.ToString("yyyy");
            if (Session["uid"].ToString() == "TMU05721")
            {
                btnGenerate.Visible = true;
            }



        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    public void getFellowshipdata()
    {

        SqlCommand cmd = new SqlCommand("Pro_getTMVFdata", con2);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Status", drpList.SelectedValue);
        cmd.Parameters.AddWithValue("@Month", ddlMonth.SelectedValue);
        cmd.Parameters.AddWithValue("@Year", ddlYear.SelectedValue);
        if (con2.State == ConnectionState.Closed)
            con2.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdtmvfpdata.DataSource = dtCL;
        grdtmvfpdata.DataBind();


    }
    protected void lnkAmountView_Click(object sender, EventArgs e)
    {


    }
    protected void lnkbutton_Click(object sender, EventArgs e)
    {



        LinkButton btn = (LinkButton)sender;
        GridViewRow grow = (GridViewRow)btn.Parent.Parent;
        Label Month = (Label)grow.FindControl("lblMonth");
        Label Year = (Label)grow.FindControl("lblYear");
        Label EmployeeName = (Label)grow.FindControl("lblemployeename");


        lblName.Text = EmployeeName.Text;

        string No_ = grdtmvfpdata.DataKeys[grow.RowIndex].Values["No_"].ToString();

        hfEmployee.Value = No_;

        SqlCommand cmd = new SqlCommand("Pro_getTMVFdatabyEmployeeCode", con2);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@UserId", No_);
        cmd.Parameters.Add("@Month", Month.Text);
        cmd.Parameters.Add("@Year", Year.Text);




        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {

            grdDetail.DataSource = dt;
            grdDetail.DataBind();

        }
        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal10').modal('show');</script>", false);
    }
    protected void Search_Click(object sender, EventArgs e)
    {

        getFellowshipdata();



    }
    public void BindYear()
    {

        int Currentyear = System.DateTime.Now.Year;

        for (int i = Currentyear - 1; i <= Currentyear; i++)
            ddlYear.Items.Add(i.ToString());
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        int i = 0;
        foreach (GridViewRow row in grdtmvfpdata.Rows)
        {
            CheckBox check = (CheckBox)row.FindControl("chkSelect");
            Label lblMonth = (Label)row.FindControl("lblMonth");
            Label lblYear = (Label)row.FindControl("lblYear");
            string No_ = grdtmvfpdata.DataKeys[row.RowIndex].Values["No_"].ToString();

            if (check.Checked == true)
            {
                SqlCommand cmd = new SqlCommand("[EDUCOLLEGELIVE-R2].dbo.TMVF_approval", con2);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Employee", No_);
                cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@Month", lblMonth.Text);
                cmd.Parameters.AddWithValue("@Year", lblYear.Text);
                cmd.Parameters.AddWithValue("@Status", "1");
                con2.Open();
                cmd.ExecuteNonQuery();
                con2.Close();
                i++;

            }
        }
        if (i > 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Approved Data Successfully');document.location.href='TMVF_Approval.aspx';", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Record.. !')", true);
        }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        int i = 0;
        foreach (GridViewRow row in grdtmvfpdata.Rows)
        {
            CheckBox check = (CheckBox)row.FindControl("chkSelect");
            Label lblMonth = (Label)row.FindControl("lblMonth");
            Label lblYear = (Label)row.FindControl("lblYear");
            string No_ = grdtmvfpdata.DataKeys[row.RowIndex].Values["No_"].ToString();

            if (check.Checked == true)
            {
                SqlCommand cmd = new SqlCommand("[EDUCOLLEGELIVE-R2].dbo.TMVF_approval", con2);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Employee", No_);
                cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@Month", lblMonth.Text);
                cmd.Parameters.AddWithValue("@Year", lblYear.Text);
                cmd.Parameters.AddWithValue("@Status", "2");
                con2.Open();
                cmd.ExecuteNonQuery();
                con2.Close();
                i++;

            }
        }
        if (i > 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Rejected');document.location.href='TMVF_Approval.aspx';", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Record.. !')", true);
        }
    }



    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        pnlMainTable.Visible = false;

        SqlCommand cmd = new SqlCommand("TMVF_getTMVFdataforReport", con2);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Month", ddlMonth.SelectedValue);
        cmd.Parameters.AddWithValue("@Year", ddlYear.SelectedValue);
        con2.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con2.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ReportViewer1.Visible = true;
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.Visible = true;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/TMVF_Report.rdlc");
            ReportDataSource datasource = new ReportDataSource("DataSetTMVF", dt);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {

            foreach (GridViewRow row in grdDetail.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    int Verify = 0;
                    string EmployeeCode = hfEmployee.Value;
                    CheckBox CHKVerify = (row.Cells[0].FindControl("chkVerify") as CheckBox);
                    HiddenField HFID = (HiddenField)row.Cells[0].FindControl("hfID");
                    if (CHKVerify.Checked == true)
                    {
                        Verify = 1;
                    }
                   
                    //if (CHKVerify.Checked == true)
                    //{
                    SqlCommand cmd = new SqlCommand("tmvf_Activity", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", HFID.Value);
                    cmd.Parameters.Add("@EmployeeNo", EmployeeCode);
                    cmd.Parameters.Add("@Verify", Verify);

                    if (con2.State == ConnectionState.Closed)
                        con2.Open();
                    cmd.ExecuteNonQuery();
                    con2.Close();
                    //}
                }
            }

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Data Update Successfully.');", true);

        }








        catch (Exception ex)
        {
            // scope.Dispose();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Some Problem Occured.');", true);
        }






    }
 
    
    protected void btnExport_Click(object sender, EventArgs e)
    {
        grdDetail.AllowPaging = false;
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grdDetail.RenderControl(htmlWrite);

        Response.Clear();
        string str = "Report_" + lblName.Text;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();
        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal10').modal('show');</script>", false);


    }
}