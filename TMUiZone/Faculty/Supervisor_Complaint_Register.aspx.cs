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

public partial class Faculty_Supervisor_Complaint_Register : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {

                //txtcomplainby.Text = Session["Fulname"].ToString();
            getcomplaint();
            getfloor();
            getward();

        }
            catch
            {
                Response.Redirect("../Default.aspx");
            }
        }
    }

    public void getward()
    {
        SqlCommand cmd = new SqlCommand("sp_getwarddata", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        cmd.Parameters.Add("@floor_Name", drpfloorname.SelectedValue);
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        drpwardname.DataSource = dtCL;
        drpwardname.DataTextField = "Ward_Name";
        drpwardname.DataValueField = "Ward_Id";
        drpwardname.DataBind();
    }
    public void getfloor()
    {

        SqlCommand cmd = new SqlCommand("sp_getFLLOR", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);

        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        drpfloorname.DataSource = dtCL;
        drpfloorname.DataTextField = "Floor_Name";
        drpfloorname.DataValueField = "Floor_Id";
        drpfloorname.DataBind();
    }

    protected void drpfloorname_SelectedIndexChanged(object sender, EventArgs e)
    {
        getward();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        string contentType1 = "";
        byte[] Photo = new byte[720];
        if (txtPhotoUploader.HasFile)
        {
            contentType1 = txtPhotoUploader.PostedFile.ContentType;
            string filename = Path.GetFileName(txtPhotoUploader.PostedFile.FileName);
            using (Stream fs = txtPhotoUploader.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    Photo = br.ReadBytes((Int32)fs.Length);
                }
            }
        }
        else
        {
            contentType1 ="";
            string filename = "";
            using (Stream fs = txtPhotoUploader.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    Photo = br.ReadBytes((Int32)0);
                }
            }
        }
        if (drpfloorname.SelectedIndex == 0)
        {
            string message1 = "Please Fill Floor Name.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (drpwardname.SelectedIndex == 0)
        {
            string message1 = "Please Fill Ward Name.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        //if (drptypeofcomplaint.SelectedIndex == 0)
        //{
        //    string message1 = "Please Fill Complaint.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    return;
        //}
        //if (drpsigconfirmation.SelectedIndex == 0)
        //{
        //    string message1 = "Please Select Confir.";

        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    return;
        //}
        if (txtroomno.Text == "")
        {
            string message1 = "Please Fill Room No.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (txtactualcomplain.Text == "")
        {
            string message1 = "Please Fill Actual Complain.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("sp_insertcomplainregister", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Employee_Name", Session["Fulname"].ToString());
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Floor_Name", drpfloorname.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Ward_Name", drpwardname.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Room_No", txtroomno.Text);
        cmd.Parameters.AddWithValue("@Date_Of_Complaint", txtdateofcomplaint.Text);
        cmd.Parameters.AddWithValue("@Type_Of_Complaint", drptypeofcomplaint.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Actual_Complaint", txtactualcomplain.Text);
        cmd.Parameters.AddWithValue("@Person_Responsible_To_Solve_Complaint", "");
        cmd.Parameters.AddWithValue("@Complaint_FWD_On_Date", "");
        cmd.Parameters.AddWithValue("@Complaint_Resolved_On_Date", "");
        cmd.Parameters.AddWithValue("@Signature_Of_Manager_After_Confirmation", "");
        cmd.Parameters.AddWithValue("@Remark", "");
        cmd.Parameters.AddWithValue("@Upload_Photo", Photo);

        cmd.Parameters.AddWithValue("@ID", "");
        cmd.Parameters.AddWithValue("@Reminder", "");
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

        drpfloorname.SelectedIndex = 0;
        drpwardname.SelectedIndex = 0;
        txtroomno.Text = "";
        txtdateofcomplaint.Text = "";
        //txtfwdofdate.Text = "";
        //txtactualcomplain.Text = "";
        //drptypeofcomplaint.SelectedIndex = 0;
        getcomplaint();


    
}
public void getcomplaint()
{

    SqlCommand cmd = new SqlCommand("sp_getcomplaintdetail", con);
    cmd.Parameters.Add("@UserID", Session["uid"].ToString());
    cmd.CommandType = CommandType.StoredProcedure;
    if (con.State == ConnectionState.Closed)
        con.Open();
    SqlDataAdapter daCL = new SqlDataAdapter(cmd);
    DataTable dtCL = new DataTable(); ;
    daCL.Fill(dtCL);
    grdcomplainregister.DataSource = dtCL;
    grdcomplainregister.DataBind();
}
public override void VerifyRenderingInServerForm(Control control)
{

}
protected void btnexporttoexel_Click(object sender, EventArgs e)
{
    System.IO.StringWriter stringWrite = new System.IO.StringWriter();

    System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

    grdcomplainregister.RenderControl(htmlWrite);

    Response.Clear();
    string str = "COMPLAIN REGISTER REPORT" + Session["uid"].ToString(); ;
    Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

    Response.Charset = "";

    Response.Cache.SetCacheability(HttpCacheability.NoCache);

    Response.ContentType = "application/vnd.xls";

    Response.Write(stringWrite.ToString());


    Response.End();
}

public void ValidateDate()
{

    DateTime frodatecom = DateTime.ParseExact(txtdateofcomplaint.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
    //DateTime Todatecom = DateTime.ParseExact(txtfwdofdate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
    //DateTime Otdatecom = DateTime.ParseExact(txtresolveddate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
    //DateTime frodatecom = DateTime.ParseExact(txtFromDate.Text.Trim() + " " + txtFromTime.Text.Trim(), "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
    //DateTime Todatecom = DateTime.ParseExact(txtTodate.Text.Trim() + " " + txtToTime.Text.Trim(), "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
    //DateTime Todatecom = DateTime.ParseExact(txtresolveddate.Text.Trim() + " " + txtresolveddate.Text.Trim(), "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
    //if (frodatecom > Todatecom)
    //{
    //    txtfwdofdate.Text = "";
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('To Date should always be greater than Complaint Date');", true);

    //}
    //if (Todatecom > Otdatecom)
    //{
    //    txtresolveddate.Text = "";
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('To Date should always be greater than FWD Date');", true);

    //}


}

protected void txtdateofcomplaint_TextChanged(object sender, EventArgs e)
{
    try
    {
        ValidateDate();
    }
    catch (Exception)
    { }
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

public Stream DisplayImage(string theID)
{
    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());

    string sql = "select Upload_Photo from  tbl_ComplainRegister  where ID=" + theID + "";
    SqlCommand cmd = new SqlCommand(sql, connection);
    cmd.CommandType = CommandType.Text;
    cmd.Parameters.AddWithValue("@ID", theID);
    connection.Open();
    object theImg = cmd.ExecuteScalar();
    try
    {
        return new MemoryStream((byte[])theImg);
    }
    catch
    {
        return null;
    }
    finally
    {
        connection.Close();
    }
}

public bool IsReusable
{
    get
    {
        return false;
    }
}


protected void lnkPhoto_Click(object sender, EventArgs e)
{
    LinkButton btn = sender as LinkButton;
    GridViewRow row = btn.NamingContainer as GridViewRow;
    string id = grdcomplainregister.DataKeys[row.RowIndex].Values[0].ToString();
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

