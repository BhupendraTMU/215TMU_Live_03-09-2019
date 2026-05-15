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

public partial class Faculty_CovidCertificateUpload : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsPostBack)
            {

                bindGrid();
                bindGridEmployee();
            }
            else
            { Response.Redirect("~/Default.aspx"); }
        }

    }
    public void bindGridEmployee()
    {
        SqlCommand cmd = new SqlCommand("proc_GetCovidEmployee", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add("@ID", Session["uid"].ToString());

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        grdEmployee.DataSource = dt;
        grdEmployee.DataBind();
    }
    public void bindGrid()
    {
        SqlCommand cmd = new SqlCommand("proc_GetCovidCertificate", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", Session["uid"].ToString());


        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        grdCovidCertificate.DataSource = dt;
        grdCovidCertificate.DataBind();
    }
    protected void lnkApproval_Click(object sender, EventArgs e)
    {
        pnlApproval.Visible = true;
        pnlUpload.Visible = false;
    }
    protected void lnkUpload_Click(object sender, EventArgs e)
    {
        pnlApproval.Visible = false;
        pnlUpload.Visible = true;
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (fUpload.HasFile)
        {

            try
            {

                string filename = Path.GetFileName(fUpload.PostedFile.FileName);
                string contentType = fUpload.PostedFile.ContentType;
                using (Stream fs = fUpload.PostedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["str"]))
                        {

                            SqlCommand cmd = new SqlCommand("proc_InsertCovidAttachment", con2);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
                            cmd.Parameters.Add("@FacultyName", Session["Fulname"].ToString());
                            cmd.Parameters.Add("@FirstDate", txtfDate.Text);
                            cmd.Parameters.Add("@SecondDate", txtSDate.Text);
                            cmd.Parameters.Add("@ApprovalStatus", Convert.ToInt32("0"));
                            cmd.Parameters.Add("@AttachmentType", contentType);
                            cmd.Parameters.Add("@Attachment", bytes);
                            cmd.Parameters.Add("@FileName", filename);
                            if (con2.State == ConnectionState.Closed)
                                con2.Open();
                            cmd.ExecuteNonQuery();
                            con2.Close();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success','Upload Successfully');", true);
                        }
                    }
                }



            }



            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error','Some Thing Went Wrong..');", true);
            }
            bindGrid();
        }
    }
    protected void lnkdelete_Click(object sender, EventArgs e)
    {
        string EmployeeCode = (sender as LinkButton).CommandArgument;

        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["str"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "delete from [TMU$Employee Vaccine List] where [Employee Code]='" + EmployeeCode + "' ";
                cmd2.Connection = con2;
                con2.Open();
                cmd2.ExecuteNonQuery();
                con2.Close();
            }
        }
        bindGrid();


    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (grdEmployee.Rows.Count > 0)
        {
            foreach (GridViewRow row in grdEmployee.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkcheck = (CheckBox)row.FindControl("chkMark");
                    if (chkcheck.Checked == true)
                    {
                        var id = grdEmployee.DataKeys[row.RowIndex].Value;
                        CheckBox chkF = (CheckBox)row.FindControl("chkFirst");
                        CheckBox chkS = (CheckBox)row.FindControl("chkSecond");
                        TextBox txtRemark = (TextBox)row.FindControl("txtremark");
                        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["str"]))
                        {
                            using (SqlCommand cmd2 = new SqlCommand())
                            {
                                cmd2.CommandText = "update [TMU$Employee Vaccine List] set Status=1,[First Dose]='" +Convert.ToInt32(chkF.Checked) + "',[Second Dose]='" + Convert.ToInt32(chkS.Checked) + "',[Rejection Remarks]='" + txtRemark.Text + "' where [Employee Code]='" + id + "' ";
                                cmd2.Connection = con2;
                                con2.Open();
                                cmd2.ExecuteNonQuery();
                                con2.Close();
                            }
                        }
                    }
                }
            }
        }


        bindGridEmployee();
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        if (grdEmployee.Rows.Count > 0)
        {
            foreach (GridViewRow row in grdEmployee.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkcheck = (CheckBox)row.FindControl("chkMark");
                  
                    if (chkcheck.Checked == true)
                    {
                        TextBox txtRemark = (TextBox)row.FindControl("txtremark");
                        if (txtRemark.Text == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error','Remark is mandatory for Rejection..');", true);
                            return;
                        }
                    }
                }
            }

            foreach (GridViewRow row in grdEmployee.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkcheck = (CheckBox)row.FindControl("chkMark");
                    if (chkcheck.Checked == true)
                    {
                        var id = grdEmployee.DataKeys[row.RowIndex].Value;
                      
                        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["str"]))
                        {
                            using (SqlCommand cmd2 = new SqlCommand())
                            {
                                cmd2.CommandText = "update [TMU$Employee Vaccine List] set Status=3 where [Employee Code]='" + id + "' ";
                                cmd2.Connection = con2;
                                con2.Open();
                                cmd2.ExecuteNonQuery();
                                con2.Close();
                            }
                        }
                    }
                }
            }
        }


        bindGridEmployee();
    }
    protected void lnkFile_Click(object sender, EventArgs e)
    {
        string EmployeeCode = (sender as LinkButton).CommandArgument;
      
        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["str"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select [Attachment of Certificate] Attachment ,[Content Type] AttachmentType,[File Name] FileName from [TMU$Employee Vaccine List] where [Employee Code]='" + EmployeeCode + "'";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Attachment"];
                    contentType = sdr["AttachmentType"].ToString();
                    fileName = sdr["FileName"].ToString();
                }
                con.Close();
            }
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
}