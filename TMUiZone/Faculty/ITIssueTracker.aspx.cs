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
using System.Net;
using System.Net.Mail;
using System.Net.Configuration;
using System.Web.Mail;


public partial class Faculty_IssueTracker : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                binddata();
                getIssuetracker();
                //complainStatusfrm();
                txtdateandtime.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
            }
            catch
            {
                Response.Redirect("../Default.aspx");
            }

        }
    }

    public void binddata()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        string strSQL = "select No_,[First Name],[Branch Name],[Mobile Phone No_],[E-Mail],[Department Name] from TMU$Employee where [No_]='" + Session["uid"].ToString() + "' select * from [TMU$Dimension Value] where [Dimension Code]='DEPARTMENT' and [Name]!=''";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        if (ds.Tables.Count > 0)
        {
            txtemployeeceode.Text = ds.Tables[0].Rows[0]["No_"].ToString();
            txtemployeename.Text = ds.Tables[0].Rows[0]["First Name"].ToString();
            txtemployeedept.Text = ds.Tables[0].Rows[0]["Department Name"].ToString();
            txtcollege.Text = ds.Tables[0].Rows[0]["Branch Name"].ToString();
            txtmobile.Text = ds.Tables[0].Rows[0]["Mobile Phone No_"].ToString();
            txtemail.Text = ds.Tables[0].Rows[0]["E-Mail"].ToString();



        }
    }



    public void getIssuetracker()
    {

        SqlCommand cmd = new SqlCommand("pro_getIssueTracker", con);
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdissuetracker.DataSource = dtCL;
        grdissuetracker.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {


        if (drpcomplainttype.SelectedItem.Text == "Select")
        {
            string message1 = "Please Select Complaint Type.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        //if (txtQuery.Text == "")
        //{
        //    string message1 = "Please Fill Query.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    return;
        //}
        //if (txtemail.Text == "")
        //{
        //    string message1 = "Please Fill Email";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    return;
        //}

        //if (drpinvolvedept.SelectedItem.Text == "Select")
        //{
        //    string message1 = "Please Select Dept.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    return;
        //}
        string contentType1 = ""; string filename = "";
        byte[] Photo = new byte[720];
        if (txtuploadPhoto.HasFile)
        {

            contentType1 = txtuploadPhoto.PostedFile.ContentType;
            filename = Path.GetFileName(txtuploadPhoto.PostedFile.FileName);
            using (Stream fs = txtuploadPhoto.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    Photo = br.ReadBytes((Int32)fs.Length);
                }
            }
        }
        else
        {
            contentType1 = "";

            using (Stream fs = txtuploadPhoto.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    Photo = br.ReadBytes((Int32)0);
                }
            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_InsertIssue", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Create_Date", "");
        cmd.Parameters.AddWithValue("@Employee_Name", txtemployeename.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeeceode.Text);
        cmd.Parameters.AddWithValue("@Employee_Dept", txtemployeedept.Text);
        //cmd.Parameters.AddWithValue("@Priority", drppriority.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Query", txtcomplaintdetail.Text);
        cmd.Parameters.AddWithValue("@Involve_Dept", drpinvolvedept.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Email_ID", txtemail.Text);
        cmd.Parameters.AddWithValue("@College", txtcollege.Text);
        cmd.Parameters.AddWithValue("@Mobile_No", txtmobile.Text);
        cmd.Parameters.AddWithValue("@IssueDateTime", "");
        cmd.Parameters.AddWithValue("@ComplaintType", drpcomplainttype.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Attachment_1", Photo);
        cmd.Parameters.AddWithValue("@contentType", contentType1);
        cmd.Parameters.AddWithValue("@fileName", filename);
        cmd.Parameters.AddWithValue("@IssueStatus", "Open");
        cmd.Parameters.AddWithValue("@AssignToID", "");
        cmd.Parameters.AddWithValue("@UserStatus", Drpstatus.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@ID", txtID.Text);
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
        getIssuetracker();
        //Response.Redirect("IssueTracker.aspx");

    }
    protected void lnkPhoto_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        GridViewRow row = btn.NamingContainer as GridViewRow;
        string id = grdissuetracker.DataKeys[row.RowIndex].Values[0].ToString();
        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select Attachment_1,[contentType],[fileName] from tbl_IssueTracker where ID=" + id + "";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {


                    sdr.Read();
                    bytes = (byte[])sdr["Attachment_1"];
                    contentType = sdr["contentType"].ToString();
                    fileName = sdr["fileName"].ToString();
                }
                con.Close();
            }
        }
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + "");
        Response.ContentType = "image/jpeg"; ;
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }

    protected void btnselect_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        GridViewRow grow = btn.NamingContainer as GridViewRow;
        string pk = grdissuetracker.DataKeys[grow.RowIndex].Values[0].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "Select * from tbl_IssueTracker where ID='" + pk + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);        
        //drppriority.SelectedItem.Text = dt.Rows[0]["Priority"].ToString();
        //txtQuery.Text = dt.Rows[0]["Query"].ToString();
        txtemail.Text = dt.Rows[0]["Email_ID"].ToString();
        //txtSearch.Text= dt.Rows[0]["Involve_Dept"].ToString();
        txtID.Text = dt.Rows[0]["ID"].ToString();
        Label4.Text = dt.Rows[0]["ID"].ToString();
        Label6.Text = dt.Rows[0]["Create_Date"].ToString();
        Label8.Text = dt.Rows[0]["IssueStatus"].ToString();
        Label10.Text = dt.Rows[0]["AssignTo"].ToString();       
        txtcollege.Text= dt.Rows[0]["College"].ToString();
        drpcomplainttype.SelectedItem.Text= dt.Rows[0]["ComplaintType"].ToString();
        txtcomplaintdetail.Text= dt.Rows[0]["Query"].ToString();
        drpinvolvedept.SelectedItem.Text = dt.Rows[0]["Involve_Dept"].ToString();
        complainStatusfrm();
        UpdateButton();

    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void btn_exporttoexel_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grdissuetracker.RenderControl(htmlWrite);

        Response.Clear();
        string str = "ITIssueTracker" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();
    }

    protected void btn_Reset_Click(object sender, EventArgs e)
    {
        Response.Redirect("ITIssueTracker.aspx");

    }
    public void complainStatusfrm()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("select * from tbl_IssueTracker where [ID]='" + txtID.Text + "'", con))
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    //string Status = dr["drpstatus"].ToString();
                    string IssueStatus = dr["IssueStatus"].ToString();
                    con.Close();
                    if (IssueStatus == "Closed")

                    {
                        Drpstatus.Enabled = true;
                       // btnSave.Visible = true;
                        //btn_Update.Visible = true;
                    }
                    else
                    {
                        Drpstatus.Enabled = false;
                       // btnSave.Visible = true;
                        //btn_Update.Visible = false;
                    }
                }
            }
        }
    }
    public void UpdateButton()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("select * from tbl_IssueTracker where [ID]='" + txtID.Text + "'", con))
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    //string Status = dr["IssueStatus"].ToString();
                    string IssueStatus = dr["IssueStatus"].ToString();
                    con.Close();
                    if (IssueStatus == "Closed")

                    {      
                        btn_Update.Visible = true;
                        btnSave.Visible = false;
                    }
                    else
                    {                      
                        btn_Update.Visible = false;
                        btnSave.Visible = true;
                    }
                }
            }
        }
    }


    protected void btn_Update_Click(object sender, EventArgs e)
    {
        string contentType1 = ""; string filename = "";
        byte[] Photo = new byte[720];
        if (txtuploadPhoto.HasFile)
        {

            contentType1 = txtuploadPhoto.PostedFile.ContentType;
            filename = Path.GetFileName(txtuploadPhoto.PostedFile.FileName);
            using (Stream fs = txtuploadPhoto.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    Photo = br.ReadBytes((Int32)fs.Length);
                }
            }
        }
        else
        {
            contentType1 = "";

            using (Stream fs = txtuploadPhoto.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    Photo = br.ReadBytes((Int32)0);
                }
            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_InsertIssue", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Create_Date", "");
        cmd.Parameters.AddWithValue("@Employee_Name", txtemployeename.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeeceode.Text);
        cmd.Parameters.AddWithValue("@Employee_Dept", txtemployeedept.Text);
        //cmd.Parameters.AddWithValue("@Priority", drppriority.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Query", txtcomplaintdetail.Text);
        cmd.Parameters.AddWithValue("@Involve_Dept", drpinvolvedept.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Email_ID", txtemail.Text);
        cmd.Parameters.AddWithValue("@College", txtcollege.Text);
        cmd.Parameters.AddWithValue("@Mobile_No", txtmobile.Text);
        cmd.Parameters.AddWithValue("@IssueDateTime", "");
        cmd.Parameters.AddWithValue("@ComplaintType", drpcomplainttype.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Attachment_1", Photo);
        cmd.Parameters.AddWithValue("@contentType", contentType1);
        cmd.Parameters.AddWithValue("@fileName", filename);
        cmd.Parameters.AddWithValue("@IssueStatus", "Closed");
        cmd.Parameters.AddWithValue("@AssignToID", "");
        cmd.Parameters.AddWithValue("@UserStatus", Drpstatus.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@ID", txtID.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "Your details have been Update successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        getIssuetracker();
    }

    
}



