using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_MembershipAprrovalForm : System.Web.UI.Page
{
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
            getmembership();
            }
            catch
            {
                Response.Redirect("../Default.aspx");
            }
        }

    }

    public void getmembership()
    {

        SqlCommand cmd = new SqlCommand("pro_getmembershipdata", con1);
        cmd.Parameters.AddWithValue("@HOD", Session["uid"].ToString());
        
        cmd.CommandType = CommandType.StoredProcedure;
        if (con1.State == ConnectionState.Closed)
            con1.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdmemberapprovallist.DataSource = dtCL;
        grdmemberapprovallist.DataBind();
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        GridViewDetails.Hide();

    }
    private DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                }
            }
            return dt;
        }
    }

    protected void lnkbutton_Click(object sender, EventArgs e)
    {
       
        GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
        int index = row.RowIndex;
        Label UserId = (Label)grdmemberapprovallist.Rows[index].FindControl("lblemployeecode");

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "select * from Tbl_LibraryMembership WHERE [Employee_code]='" + UserId.Text + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        txtemployeecode.Text = dt.Rows[0]["Employee_code"].ToString();
        txtemployeename.Text = dt.Rows[0]["Name"].ToString();
        txtdepartment.Text = dt.Rows[0]["Department"].ToString();
        txtdesignation.Text = dt.Rows[0]["Designation"].ToString();
        txtemail.Text = dt.Rows[0]["Email"].ToString();
        txtdateofbirth.Text = dt.Rows[0]["DOB"].ToString();
        txtlocaladdress.Text = dt.Rows[0]["Local_address"].ToString();
        txtpermentaddress.Text = dt.Rows[0]["Per_address"].ToString();
        txtmobile.Text= dt.Rows[0]["Contact_no"].ToString();
        //chkaccept.Checked= dt.Rows[0]["Declaration"].ToString();
        try

        {

            byte[] bytes = GetData("select Upload_Photo as FacultyImage from Tbl_LibraryMembership  WHERE [Employee_code] = '" + UserId.Text + "'").Rows[0]["FacultyImage"].ToString() == "" ? null : (byte[])GetData("select Upload_Photo as FacultyImage from Tbl_LibraryMembership  WHERE [Employee_code] = '" + UserId.Text + "'").Rows[0]["FacultyImage"];
            if (bytes != null)
            {
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                ImgPrv.ImageUrl = "data:image/png;base64," + base64String;
            }

        }
        catch (Exception ex)
        {
        }


        GridViewDetails.Show();
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            int i = 0;
            foreach (GridViewRow row in grdmemberapprovallist.Rows)
            {
                CheckBox check = (CheckBox)row.FindControl("Chkemployee");
                Label lblemployeecode = (Label)row.FindControl("lblemployeecode");
                var id = grdmemberapprovallist.DataKeys[row.RowIndex].Value;
                SqlCommand cmd = new SqlCommand("pro_approvelib", con1);
                if (check.Checked == true)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeCode", lblemployeecode.Text);
                    cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
                    cmd.Parameters.AddWithValue("Status", "Approved");
                    cmd.Parameters.AddWithValue("@Approval_status", "Approved");
                    con1.Open();
                    cmd.ExecuteNonQuery();
                    con1.Close();
                    i++;
                }




            }
            if (i > 0) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' Form Approved')", true); }
            else { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Data.. !')", true); }
            //BtnSubmit.Visible = false;
            getmembership();
        }
        catch (Exception ex)
        {
        }
    }



    protected void BtnRejected_Click(object sender, EventArgs e)
    {
        try
        {
            int i = 0;
            foreach (GridViewRow row in grdmemberapprovallist.Rows)
            {
                CheckBox check = (CheckBox)row.FindControl("Chkemployee");
                Label lblemployeecode = (Label)row.FindControl("lblemployeecode");
                var id = grdmemberapprovallist.DataKeys[row.RowIndex].Value;
                SqlCommand cmd = new SqlCommand("pro_Rejectlib", con1);
                if (check.Checked == true)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeCode", lblemployeecode.Text);
                    cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
                    cmd.Parameters.AddWithValue("Status", "Rejected");                        
                    cmd.Parameters.AddWithValue("@Approval_status", "Rejected");
                    cmd.Parameters.AddWithValue("@Form_Status", "Pending");
                    con1.Open();
                    cmd.ExecuteNonQuery();
                    con1.Close();
                    i++;
                }




            }
            if (i > 0) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' Form Rejected')", true); }
            else { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Data.. !')", true); }
            //BtnSubmit.Visible = false;
            getmembership();
        }
        catch (Exception ex)
        {
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }


    protected void Btnexporttoexel_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        grdmemberapprovallist.RenderControl(htmlWrite);
        Response.Clear();
        string str = "LIBRARYMEMBERREPORT" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        Response.Write(stringWrite.ToString());
        Response.End();
    }
    protected void Search_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("sp_searchEmployee", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@Employee_code", txtemployeecodese.Text));
        cmd.Parameters.AddWithValue("@Deputy_librarin", Session["uid"].ToString());
        if (con1.State == ConnectionState.Closed)
            con1.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable(); ;
        daCL.Fill(dtCL);
        grdmemberapprovallist.DataSource = dtCL;
        grdmemberapprovallist.DataBind();
    }
    protected void grdmemberapprovallist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdmemberapprovallist.PageIndex = e.NewPageIndex;
        getmembership();
    }
 protected void grdmemberapprovallist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblprincipalapproval = (Label)e.Row.FindControl("lblprincipalapproval");
            Label lbldeputylibrarian = (Label)e.Row.FindControl("lbldeputylibrarian");
            if (lblprincipalapproval.Text == "Rejected" || lbldeputylibrarian.Text=="Blocked")
            {
                e.Row.BackColor = System.Drawing.Color.MistyRose;
            }

        }
        
    }
}


