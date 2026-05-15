using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_Arrears : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("select UserId,(select count(*) from [HRMSPortal].dbo.tbl_ArrearMonthWiseCloserDetails where [Month]=Month('" + txtfrom.Text + "') and [YEAR]=Year('" + txtfrom.Text + "') and [Open]=0) as 'open' from [HRMSPortal].dbo.tbl_EmployeeArrear where ((convert(date,From_Date,111) =  '" + txtfrom.Text + "') or (convert(date,To_Date,111) =  '" + txtfrom.Text + "' )) and Status!=2 and  UserId='" + Session["uid"].ToString() + "' ", con);
            DataTable dt = new DataTable();

            da.Fill(dt);

            con.Close();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["open"].ToString() == "0")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'You can not apply Arrears for this date.');", true);

                    return;
                }
            }
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'You have already applied Arrears for this date.');", true);

                return;
            }
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {

                SqlCommand cmd = new SqlCommand("Insert_EmployeeArrear", con2);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@FromDate", txtfrom.Text);
                cmd.Parameters.Add("@ToDate", txtfrom.Text);
                cmd.Parameters.Add("@EmployeeCode", Session["uid"].ToString());
                cmd.Parameters.Add("@Reason", TextBox1.Text);
                cmd.Parameters.Add("@Status", "0");
                cmd.Parameters.Add("@Amount", txtamount.Text);
                cmd.Parameters.Add("@No_Of_Days", txtNoOfLeavePriod.Text);


                if (con2.State == ConnectionState.Closed)
                    con2.Open();
                cmd.ExecuteNonQuery();
                con2.Close();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Arrears Request Generate Successfully.');", true);

            }
        }


        catch (Exception ex)
        {
            Response.Redirect("../Default.aspx");
        }


    }
    //protected void txtfrom_TextChanged(object sender, EventArgs e)
    //{

    //    txtto.Text = txtfrom.Text;

    //    if (txtfrom.Text == "" || txtto.Text == "")
    //    {


    //    }
    //    else
    //    {
    //        DateTime frodatecom = DateTime.ParseExact(txtfrom.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
    //        DateTime Todatecom = DateTime.ParseExact(txtto.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);

    //        if (frodatecom > Todatecom)
    //        {
    //            txtto.Text = "";
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('To Date should always be greater than From Date');", true);

    //        }
    //        else
    //        {


    //            string sdFom = txtfrom.Text.Trim();
    //            string sdTo = txtto.Text.Trim();
    //            DateTime startDateTime1 = DateTime.ParseExact(sdFom, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
    //            DateTime endDateTime1 = DateTime.ParseExact(sdTo, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
    //            TimeSpan difference = endDateTime1 - startDateTime1;
    //            string differenceString = difference.ToString();
    //            string s = differenceString.ToString();
    //            string h1 = difference.TotalDays.ToString();
    //            double sh4 = Convert.ToDouble(h1);
    //            double fl2 = Math.Floor(sh4);
    //            int no1 = 1;
    //            double no2 = Convert.ToDouble(no1);
    //            double f13 = Convert.ToDouble(fl2 + no2);
    //            //lblTotalLeave.Text = f13.ToString();

    //            //int hcount = Convert.ToInt32(lblcountholiday.Text);
    //            //int offcount = Convert.ToInt32(lblCountOffDay.Text);
    //            //int hcoffcount = hcount + offcount;
    //            //int toleav = Convert.ToInt32(lblTotalLeave.Text);
    //            txtNoOfLeavePriod.Text = (f13).ToString();




    //        }

    //    }
    //}
    //protected void txtto_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtfrom.Text == "" || txtto.Text == "")
    //    {


    //    }
    //    else
    //    {
    //        DateTime frodatecom = DateTime.ParseExact(txtfrom.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
    //        DateTime Todatecom = DateTime.ParseExact(txtto.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);

    //        if (frodatecom > Todatecom)
    //        {
    //            txtto.Text = "";
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('To Date should always be greater than From Date');", true);

    //        }
    //        else
    //        {


    //            string sdFom = txtfrom.Text.Trim();
    //            string sdTo = txtto.Text.Trim();
    //            DateTime startDateTime1 = DateTime.ParseExact(sdFom, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
    //            DateTime endDateTime1 = DateTime.ParseExact(sdTo, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
    //            TimeSpan difference = endDateTime1 - startDateTime1;
    //            string differenceString = difference.ToString();
    //            string s = differenceString.ToString();
    //            string h1 = difference.TotalDays.ToString();
    //            double sh4 = Convert.ToDouble(h1);
    //            double fl2 = Math.Floor(sh4);
    //            int no1 = 1;
    //            double no2 = Convert.ToDouble(no1);
    //            double f13 = Convert.ToDouble(fl2 + no2);
    //            //lblTotalLeave.Text = f13.ToString();

    //            //int hcount = Convert.ToInt32(lblcountholiday.Text);
    //            //int offcount = Convert.ToInt32(lblCountOffDay.Text);
    //            //int hcoffcount = hcount + offcount;
    //            //int toleav = Convert.ToInt32(lblTotalLeave.Text);
    //            txtNoOfLeavePriod.Text = (f13).ToString();




    //        }

    //    }
    //}
    protected void lnkProfileview_Click(object sender, EventArgs e)
    {
        pnlLeaveApplication.Visible = true;
        pnlApproval.Visible = false;
    }
    protected void lnkRejectProfileDetail_Click(object sender, EventArgs e)
    {

    }
    protected void lnkApproval_Click(object sender, EventArgs e)
    {
        pnlLeaveApplication.Visible = false;
        pnlApproval.Visible = true;
        bindPendingLeave();
    }
    public void bindPendingLeave()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("[HRMSPortal].dbo.proc_GetPendingArrear", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", Session["uid"].ToString());


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            grdData.DataSource = dt;
            grdData.DataBind();
        }
        catch (Exception ex)
        {
            Response.Redirect("../Default.aspx");
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {

        string profilechangedate = "";
        foreach (GridViewRow row in grdData.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {

                CheckBox chkRow = (row.Cells[0].FindControl("chkSelect") as CheckBox);
                Label lblProfilechangedate1 = (row.Cells[0].FindControl("lblId") as Label);


                if (chkRow.Checked == true)
                {

                    profilechangedate = lblProfilechangedate1.Text;

                    using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
                    {

                        SqlCommand cmd = new SqlCommand("Update_ArrearStatus", con2);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@UserId", Session["uid"].ToString());
                        cmd.Parameters.Add("@Id", profilechangedate);
                        cmd.Parameters.Add("@Status", 1);
                       
                        if (con2.State == ConnectionState.Closed)
                            con2.Open();
                        cmd.ExecuteNonQuery();
                        con2.Close();

                    }
                }

            }
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Arrears Approved Successfully.');", true);

        bindPendingLeave();
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        string profilechangedate = "";
        foreach (GridViewRow row in grdData.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {

                CheckBox chkRow = (row.Cells[0].FindControl("chkSelect") as CheckBox);
                Label lblProfilechangedate1 = (row.Cells[0].FindControl("lblId") as Label);


                if (chkRow.Checked == true)
                {

                    profilechangedate = lblProfilechangedate1.Text;

                    using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
                    {

                        SqlCommand cmd = new SqlCommand("Update_ArrearStatus", con2);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@UserId", Session["uid"].ToString());
                        cmd.Parameters.Add("@Id", profilechangedate);
                        cmd.Parameters.Add("@Status", 2);
                        if (con2.State == ConnectionState.Closed)
                            con2.Open();
                        cmd.ExecuteNonQuery();
                        con2.Close();

                    }
                }

            }
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Arrears Rejected Successfully.');", true);

        bindPendingLeave();
    }
}