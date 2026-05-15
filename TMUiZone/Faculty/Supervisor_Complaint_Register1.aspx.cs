using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_Supervisor_Complaint_Register1 : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {

                getcomplaint();

            }
            catch
            {
                Response.Redirect("../Default.aspx");
            }

        }

    }

    private void ValidateDate()
    {
        DateTime frodatecom = DateTime.ParseExact(txtdateofcomplaint.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime Todatecom = DateTime.ParseExact(txtfwdofdate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime Otdatecom = DateTime.ParseExact(txtresolveddate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //DateTime frodate1com = DateTime.ParseExact(txtdateofcomplaint.Text.Trim() + " " + txtdateofcomplaint.Text.Trim(), "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
        //DateTime Todate2com = DateTime.ParseExact(txtfwdofdate.Text.Trim() + " " + txtfwdofdate.Text.Trim(), "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
        //DateTime Otdate3com = DateTime.ParseExact(txtresolveddate.Text.Trim() + " " + txtresolveddate.Text.Trim(), "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
        if (frodatecom > Todatecom)
        {
            txtfwdofdate.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('To Date should always be greater than Complaint Date');", true);

        }
        if (Todatecom > Otdatecom)
        {
            txtresolveddate.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('To Date should always be greater than FWD Date');", true);

        }

    }
    public void getcomplaint()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("sp_getcomplaintdetail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlDataAdapter daCL = new SqlDataAdapter(cmd);
            DataTable dtCL = new DataTable(); ;
            daCL.Fill(dtCL);
            grdcomplainregister1.DataSource = dtCL;
            grdcomplainregister1.DataBind();
        }
        catch (Exception ex)
        {

        }
    }

    protected void txtfwdofdate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ValidateDate();
        }
        catch (Exception)
        { }
    }

    protected void txtresolveddate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ValidateDate();
        }
        catch (Exception)
        { }
    }




    protected void btnselect_Click(object sender, EventArgs e)
    {

        LinkButton btn = sender as LinkButton;
        GridViewRow grow = btn.NamingContainer as GridViewRow;
        string pk = grdcomplainregister1.DataKeys[grow.RowIndex].Values[0].ToString();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "Select * from tbl_ComplainRegister where ID='" + pk + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        txtfloorname.Text = dt.Rows[0]["Floor_Name"].ToString();
        txtwardno.Text = dt.Rows[0]["Ward_Name"].ToString();
        txtroomno.Text = dt.Rows[0]["Room_No"].ToString();
        txtcomplaint.Text = dt.Rows[0]["Type_Of_Complaint"].ToString();
        txtdateofcomplaint.Text = dt.Rows[0]["Date_Of_Complaint"].ToString();
        txtactualcomplain.Text = dt.Rows[0]["Actual_Complaint"].ToString();
        txtfwdofdate.Text = dt.Rows[0]["Complaint_FWD_On_Date"].ToString();
        txtpersonsolvecomplain.Text = dt.Rows[0]["Person_Responsible_To_Solve_Complaint"].ToString();
        txtremark.Text = dt.Rows[0]["Remark"].ToString();
        txtresolveddate.Text = dt.Rows[0]["Complaint_Resolved_On_Date"].ToString();
        txtID.Text = dt.Rows[0]["ID"].ToString();
    }


   protected void btnSave_Click(object sender, EventArgs e)
    {

        if (txtfloorname.Text == "")
        {
            string message1 = "Please Select Floor Name.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (txtwardno.Text == "")
        {
            string message1 = "Please Select Ward No.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (txtroomno.Text == "")
        {
            string message1 = "Please Fill Room No.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (txtpersonsolvecomplain.Text == "")
        {
            string message1 = "Please Fill Person Solve Complain.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }

        if (txtfwdofdate.Text == "")
        {
            string message1 = "Please Fill FWD Date.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        //if (txtresolveddate.Text == "")
        //{
        //    string message1 = "Please Fill Resolve Date.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    return;
        //}
        //if (drpsigconfirmation.SelectedIndex == 0)
        //{
        //    string message1 = "Please Select Sig. Confirmation";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    return;
        //}
        //if (txtremark.Text == "")
        //{
        //    string message1 = "Please Fill Remark.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    return;
        //}
        //if (drpremainder.SelectedIndex == 0)
        //{
        //    string message1 = "Please Select Remainder.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    return;
        //}
        

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
                SqlCommand cmd = new SqlCommand("sp_insertcomplainregister", con); 
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Employee_Name", Session["Fulname"].ToString());
                cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@Floor_Name", "");
                cmd.Parameters.AddWithValue("@Ward_Name", "");
                cmd.Parameters.AddWithValue("@Room_No", txtroomno.Text);
                cmd.Parameters.AddWithValue("@Date_Of_Complaint", txtdateofcomplaint.Text);
                cmd.Parameters.AddWithValue("@Type_Of_Complaint", "");
                cmd.Parameters.AddWithValue("@Actual_Complaint", txtactualcomplain.Text);
                cmd.Parameters.AddWithValue("@Person_Responsible_To_Solve_Complaint", txtpersonsolvecomplain.Text);
                cmd.Parameters.AddWithValue("@Complaint_FWD_On_Date", txtfwdofdate.Text);
                cmd.Parameters.AddWithValue("@Complaint_Resolved_On_Date", txtresolveddate.Text);
                cmd.Parameters.AddWithValue("@Signature_Of_Manager_After_Confirmation", drpsigconfirmation.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Remark", txtremark.Text);
                //cmd.Parameters.AddWithValue("@Upload_Photo", "");
                cmd.Parameters.AddWithValue("@ID", txtID.Text);
                cmd.Parameters.AddWithValue("@Reminder", drpremainder.SelectedItem.Text);
                if (con.State == ConnectionState.Open)
                { con.Close(); }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                string message = "Your details have been saved successfully.";
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);

                txtfloorname.Text = "";
                txtwardno.Text = "";
                txtroomno.Text = "";
                txtdateofcomplaint.Text = "";
                txtfwdofdate.Text = "";
                txtactualcomplain.Text = "";
                txtcomplaint.Text = "";
                getcomplaint();

            }
        
    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void btnexporttoexel_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
         grdcomplainregister1.RenderControl(htmlWrite);

        Response.Clear();
        string str = "COMPLAIN REGISTER REPORT" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();
    }

    protected void lnkPhoto_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        GridViewRow row = btn.NamingContainer as GridViewRow;
        string id = grdcomplainregister1.DataKeys[row.RowIndex].Values[0].ToString();
        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select Upload_Photo from tbl_ComplainRegister where ID=" + id + "";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Upload_Photo"];


                    fileName = "Photo";
                }
                con.Close();
            }
        }
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".jpeg");
        Response.ContentType = "image/jpeg"; ;
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }

}








