using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_UpdateProfile : System.Web.UI.Page
{
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getUpdatedata();
        }
    }

    public void getUpdatedata()
    {
        SqlCommand cmd = new SqlCommand("pro_getDataForUpdate", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        if (con1.State == ConnectionState.Closed)
            con1.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdUpdateProfileData.DataSource = dtCL;
        grdUpdateProfileData.DataBind();
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        int i = 0;
        foreach (GridViewRow row in grdUpdateProfileData.Rows)
        {
            CheckBox check = (CheckBox)row.FindControl("Chkemployee");
            Label lblemployeecode = (Label)row.FindControl("lblemployeecode");
            var id = grdUpdateProfileData.DataKeys[row.RowIndex].Value;
            SqlCommand cmd = new SqlCommand("[HRMSPortal].dbo.pro_approveUpdateProfile", con1);
            if (check.Checked == true)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Student_Number", lblemployeecode.Text);
                cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
                con1.Open();
                cmd.ExecuteNonQuery();
                con1.Close();
                i++;
            }
        }
        if (i > 0) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Update Data Approved')", true); }
        else { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Data.. !')", true); }
        getUpdatedata();
    }

    protected void BtnRejected_Click(object sender, EventArgs e)
    {
        int i = 0;
        foreach (GridViewRow row in grdUpdateProfileData.Rows)
        {
            CheckBox check = (CheckBox)row.FindControl("Chkemployee");
            Label lblemployeecode = (Label)row.FindControl("lblemployeecode");
            var id = grdUpdateProfileData.DataKeys[row.RowIndex].Value;
            SqlCommand cmd = new SqlCommand("[HRMSPortal].dbo.pro_RejectedUpdateProfile", con1);
            if (check.Checked == true)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Student_Number", lblemployeecode.Text);
                cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
                con1.Open();
                cmd.ExecuteNonQuery();
                con1.Close();
                i++;
            }
        }
        if (i > 0) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Update Data Rejected')", true); }
        else { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Data.. !')", true); }
        getUpdatedata();
    }

    protected void lnkbutton_Click(object sender, EventArgs e)
    {

        GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
        int index = row.RowIndex;
        Label UserId = (Label)grdUpdateProfileData.Rows[index].FindControl("lblemployeecode");
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        string strSQL = "select * , case when Gender='1' then 'Male' else 'Female' end as Gender1, format([Date of Birth], 'dd-MM-yyyy') as DOB,  (select [Student Name] as StudentName  from [TMU$Student Data H_E] where [Student No_]=sc.No_) as NameH,(select [Student Father Name]  from [TMU$Student Data H_E] where[Student No_] = sc.No_)FNameH,(select [Student Mother Name]  from [TMU$Student Data H_E] where[Student No_] = sc.No_) MNameH   from [TMU$Student - COLLEGE] as sc where [No_]='" + UserId.Text + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            txtRollNo.Text = dt.Rows[0]["Enrollment No_"].ToString();
            txtName.Text = dt.Rows[0]["Student Name"].ToString();
            txtCourse.Text = dt.Rows[0]["Course Code"].ToString();
            txtfathername.Text = dt.Rows[0]["Fathers Name"].ToString();
            txtMothername.Text = dt.Rows[0]["Mothers Name"].ToString();
            txtnameofcollege.Text = dt.Rows[0]["Global Dimension 1 Code"].ToString();
            txtyearofaddmission.Text = dt.Rows[0]["Academic Year"].ToString();
            txtreligion.Text = dt.Rows[0]["Religion"].ToString();
            txtcategory.Text = dt.Rows[0]["Category"].ToString();
            txtgender.Text = dt.Rows[0]["Gender1"].ToString();
            txtnationality.Text = dt.Rows[0]["Nationality"].ToString();
            txtDOB.Text = dt.Rows[0]["DOB"].ToString();
            txtmobileno.Text = dt.Rows[0]["Mobile Number"].ToString();
            txtparentsmob.Text = dt.Rows[0]["Father Mobile No"].ToString();
            txtsudentEmailID.Text = dt.Rows[0]["E-Mail Address"].ToString();
            txtEmailID.Text = dt.Rows[0]["Alternate Email Address"].ToString();
            txtcorrespondence.Text = dt.Rows[0]["Address1"].ToString();
            txtdistrictcorres.Text = dt.Rows[0]["City"].ToString();
            txtstatecorres.Text = dt.Rows[0]["State"].ToString();
            txtpincodecorre.Text = dt.Rows[0]["Post Code"].ToString();
            txtcountrycorre.Text = dt.Rows[0]["Country Code"].ToString();
            txtperaddress.Text = dt.Rows[0]["Address2"].ToString();
            txtperdistrict.Text = dt.Rows[0]["City"].ToString();
            txtperstate.Text = dt.Rows[0]["State"].ToString();
            txtperpincode.Text = dt.Rows[0]["Post Code"].ToString();
            txtpercountry.Text = dt.Rows[0]["Country Code"].ToString();
            txtstudendtnameHindi.Text = dt.Rows[0]["NameH"].ToString();
            txtFathernameHindi.Text = dt.Rows[0]["FNameH"].ToString();
            txtMotherNameHindi.Text = dt.Rows[0]["MNameH"].ToString();
            GridViewDetails.Show();
        }
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        GridViewDetails.Hide();
    }

    protected void grdUpdateProfileData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdUpdateProfileData.PageIndex = e.NewPageIndex;
        getUpdatedata();
    }
}