
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Web.Services;
using System.Web.Script.Services;
using System.Net;
using System.Text;
using WebReference;

public partial class Faculty_CertificateApprovalHOD : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

               
                bindApprovalList();
                
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    protected void SChlAll1_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkrow1 = (CheckBox)grdCertificateApproval.HeaderRow.FindControl("SChlAll1");
        if (chkrow1.Checked)
        {

            foreach (GridViewRow row in grdCertificateApproval.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkrow = (CheckBox)row.FindControl("SChkD1");
                    if (chkrow.Enabled == true)
                    {
                        chkrow.Checked = true;
                    }
                }
            }
        }
        else
        {
            foreach (GridViewRow row in grdCertificateApproval.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkrow = (CheckBox)row.FindControl("SChkD1");
                    if (chkrow.Enabled == true)
                    {
                        chkrow.Checked = false;
                    }
                }
            }
        }
    }
    public void bindApprovalList()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("[Pro_GetListForCertificateApprovalHOD]", con); //coment on usp_GetMarkEntrytable1 27-12-2017 by ashu sp_GetInternalMarksDataForApprovalByHOD
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UID", Session["uid"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            grdCertificateApproval.DataSource = dt;
            grdCertificateApproval.DataBind();
        }
        catch
        {
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (grdCertificateApproval.Rows.Count > 0)
        {
            for (int i = 1; i <= grdCertificateApproval.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grdCertificateApproval.Rows[i - 1].FindControl("SChkD1");
                TextBox txtremark = (TextBox)grdCertificateApproval.Rows[i - 1].FindControl("txtRemark");
                if (txtremark.Text == "" && chk.Checked == true)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('Remarks is mandotary....')", true);
                    return;
                }
            }
        }
        int result = 0;
        if (grdCertificateApproval.Rows.Count > 0)
        {
            for (int i = 1; i <= grdCertificateApproval.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grdCertificateApproval.Rows[i - 1].FindControl("SChkD1");
                TextBox txtremark = (TextBox)grdCertificateApproval.Rows[i - 1].FindControl("txtRemark");
                Label lblStudentNo = (Label)grdCertificateApproval.Rows[i - 1].FindControl("lblEnroll");
                if (chk.Checked == true)
                {
                    SqlCommand cmd;
                    cmd = new SqlCommand("[Sp_UpdateYearBack]", con);
                    cmd.Parameters.AddWithValue("@StudentNo", lblStudentNo.Text);
                    cmd.Parameters.AddWithValue("@Remark", txtremark.Text);
                    cmd.Parameters.AddWithValue("@Status", 1);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            if (result > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Message", "alert('Approved Successfully')", true);
                bindApprovalList();
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (grdCertificateApproval.Rows.Count > 0)
        {
            for (int i = 1; i <= grdCertificateApproval.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grdCertificateApproval.Rows[i - 1].FindControl("SChkD1");
                TextBox txtremark = (TextBox)grdCertificateApproval.Rows[i - 1].FindControl("txtRemark");
                if (txtremark.Text == ""  && chk.Checked==true)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('Remarks is mandotary....')", true);
                    return;
                }
            }
        }

        int result = 0;
        if (grdCertificateApproval.Rows.Count > 0)
        {
            for (int i = 1; i <= grdCertificateApproval.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grdCertificateApproval.Rows[i - 1].FindControl("SChkD1");
                TextBox txtremark = (TextBox)grdCertificateApproval.Rows[i - 1].FindControl("txtRemark");
                Label lblStudentNo = (Label)grdCertificateApproval.Rows[i - 1].FindControl("lblEnroll");
                if (chk.Checked == true)
                {
                    SqlCommand cmd;
                    cmd = new SqlCommand("[Sp_UpdateYearBack]", con);
                    cmd.Parameters.AddWithValue("@StudentNo", lblStudentNo.Text);
                    cmd.Parameters.AddWithValue("@Remark", txtremark.Text);
                    cmd.Parameters.AddWithValue("@Status", 2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    result= cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            if (result > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Message", "alert('Rejected Successfully')", true);
                bindApprovalList();
            }
        }


      
    }
}