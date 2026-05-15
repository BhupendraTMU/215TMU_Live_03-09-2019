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

public partial class Faculty_ArrearsList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["uid"].ToString() == "TMU00049")
            {
                pnlApproval.Visible = true;
                pnlmsg.Visible = false;
                bindPendingLeave();
            }
            else
            {
                pnlmsg.Visible = true;
                pnlApproval.Visible = false;
            }
        }
    }


    public void bindFinalArrear()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("[HRMSPortal].dbo.proc_GetFinalArrearFinance", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", Session["uid"].ToString());


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                btnPost.Visible = true;
                GridView1.DataSource = dt;
                GridView1.DataBind();
                
            }
            else
            {
                btnPost.Visible = false;
                GridView1.DataSource = "";
                GridView1.DataBind();
              
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../Default.aspx");
        }
    }


    public void bindPendingLeave()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("[HRMSPortal].dbo.proc_GetPendingArrearFinance", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", Session["uid"].ToString());


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                grdData.DataSource = dt;
                grdData.DataBind();
                btnApprove.Visible = true;
                btnReject.Visible = true;
            }
            else
            {
                grdData.DataSource = "";
                grdData.DataBind();
                btnApprove.Visible = false;
                btnReject.Visible = false;
            }
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
                TextBox txtAmount = (row.Cells[0].FindControl("txtCAmount") as TextBox);
                TextBox txtAmountRemarks = (row.Cells[0].FindControl("txtCAmountRemarks") as TextBox);

                if (chkRow.Checked == true)
                {

                    profilechangedate = lblProfilechangedate1.Text;

                    using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
                    {

                        SqlCommand cmd = new SqlCommand("Update_ArrearStatus", con2);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@UserId", Session["uid"].ToString());
                        cmd.Parameters.Add("@Id", profilechangedate);
                        cmd.Parameters.Add("@Status", 3);
                        cmd.Parameters.Add("@FinalAMT", txtAmount.Text);
                        cmd.Parameters.Add("@FinalRemark", txtAmountRemarks.Text);
                        if (con2.State == ConnectionState.Closed)
                            con2.Open();
                        cmd.ExecuteNonQuery();
                        con2.Close();

                    }
                }

            }
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Arrears Paid Successfully.');", true);

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
                        cmd.Parameters.Add("@Status", 4);
                        if (con2.State == ConnectionState.Closed)
                            con2.Open();
                        cmd.ExecuteNonQuery();
                        con2.Close();

                    }
                }

            }
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Arrears Unpaid Successfully.');", true);

        bindPendingLeave();
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if(CheckBox1.Checked==true)
        {
            bindFinalArrear();

            GridView1.Visible = true;
            grdData.Visible = false;
            btnApprove.Visible = false;
            btnReject.Visible = false;
            btnPost.Visible = true;
        }
        else
        {
            GridView1.Visible = false;
            grdData.Visible = true;
            btnApprove.Visible = true;
            btnReject.Visible = true;
            btnPost.Visible = false;
        }
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {

       try
        {
        string profilechangedate = "", SIADocNo1 = ""; 
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {

                CheckBox chkRow = (row.Cells[0].FindControl("chkSelect") as CheckBox);
                Label lblProfilechangedate1 = (row.Cells[0].FindControl("lblId") as Label);
                TextBox txtAmount = (row.Cells[0].FindControl("txtCAmount") as TextBox);
                TextBox txtAmountRemarks = (row.Cells[0].FindControl("txtCAmountRemarks") as TextBox);

                if (chkRow.Checked == true)
                {

                    profilechangedate = lblProfilechangedate1.Text;

                    using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
                    {

                        SqlCommand cmd = new SqlCommand("SP_PostArrears", con2);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@UserId", Session["uid"].ToString());
                        cmd.Parameters.Add("@Id", profilechangedate);
                        cmd.Parameters.Add("@Status", 5);
                        cmd.Parameters.Add("@FinalAMT", txtAmount.Text);
                        cmd.Parameters.Add("@FinalRemark", txtAmountRemarks.Text);
                        cmd.Parameters.Add("@Out", SqlDbType.VarChar, 100);
                        cmd.Parameters["@Out"].Direction = ParameterDirection.Output;
                        if (con2.State == ConnectionState.Closed)
                            con2.Open();
                        int i = cmd.ExecuteNonQuery();

                        SIADocNo1 = Convert.ToString(cmd.Parameters["@Out"].Value);
                        con2.Close();

                    }
                }

            }
        }

        if (con.State == ConnectionState.Closed)
            con.Open();

        SqlCommand cmdnoseries = new SqlCommand("update [dbo].[TMU$No_ Series Line] set [Last No_ Used]='" + @SIADocNo1.TrimEnd() + "' where [Series Code]='ADD/DED'", con);
        //cmdnoseries.CommandType = CommandType.StoredProcedure;
        cmdnoseries.ExecuteNonQuery();

        con.Close();
        // scope.Complete();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Arrears Paid Successfully.');", true);
        bindPendingLeave();

    }
    catch (Exception ex)
        {
        }
        bindPendingLeave();
}
}