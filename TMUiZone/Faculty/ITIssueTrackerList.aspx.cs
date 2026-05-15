
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

public partial class Faculty_IssueTrackerList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getIssuetracker();
            IT();
            
        }

    }
    public void getIssuetracker()
    {

        SqlCommand cmd = new SqlCommand("pro_getIssueTracker1", con);
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
    public void IT()
    {
        SqlCommand cmd = new SqlCommand("sp_geterpemployee", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        drpassignto.DataSource = dtCL;
        drpassignto.DataTextField = "Employee_Name";
        drpassignto.DataValueField = "P";
        drpassignto.DataBind();

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtemployeeceode.Text == "")
        {
            string message1 = "Please Fill Employee Code.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (txtemployeename.Text == "")
        {
            string message1 = "Please Fill Employee Name.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (txtemployeedept.Text == "")
        {
            string message1 = "Please Fill Employee Deptt.";
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
        //string contentType1 = "";
        //byte[] Photo = new byte[720];
        //if (txtuploadPhoto.HasFile)
        //{
        //    contentType1 = txtuploadPhoto.PostedFile.ContentType;
        //    string filename = Path.GetFileName(txtuploadPhoto.PostedFile.FileName);
        //    using (Stream fs = txtuploadPhoto.PostedFile.InputStream)
        //    {
        //        using (BinaryReader br = new BinaryReader(fs))
        //        {
        //            Photo = br.ReadBytes((Int32)fs.Length);
        //        }
        //    }
        //}
        //else
        //{
        //    contentType1 = "";
        //    string filename = "";
        //    using (Stream fs = txtuploadPhoto.PostedFile.InputStream)
        //    {
        //        using (BinaryReader br = new BinaryReader(fs))
        //        {
        //            Photo = br.ReadBytes((Int32)0);
        //        }
        //    }
        //}
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_InsertIssue", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Create_Date", "");
        cmd.Parameters.AddWithValue("@Employee_Name", txtemployeename.Text);
        cmd.Parameters.AddWithValue("@Employee_Code", txtemployeeceode.Text);
        cmd.Parameters.AddWithValue("@Employee_Dept", txtemployeedept.Text);
        //cmd.Parameters.AddWithValue("@Priority", txtpriority.Text);
        cmd.Parameters.AddWithValue("@Query", TextBox1.Text);
        cmd.Parameters.AddWithValue("@Involve_Dept", txtinvolvedept.Text);
        cmd.Parameters.AddWithValue("@Email_ID", txtemail.Text);
        cmd.Parameters.AddWithValue("@ComplaintType", txtcomplaintype.Text);
        cmd.Parameters.AddWithValue("@Mobile_No", txtmobile.Text);
        cmd.Parameters.AddWithValue("@IssueDateTime", "");
        //cmd.Parameters.AddWithValue("@Attachment_1", Photo);
        cmd.Parameters.AddWithValue("@IssueStatus", drpstatus.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@AssignTo", drpassignto.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Remarks", txtremark.Text);
        cmd.Parameters.AddWithValue("@AssignToID", drpassignto.SelectedValue);
        cmd.Parameters.AddWithValue("@AssignBy", Session["uid"].ToString());
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
        //Response.Redirect("IssueTrackerList.aspx");
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
        txtemployeename.Text = dt.Rows[0]["Employee_Name"].ToString();
        txtemployeeceode.Text = dt.Rows[0]["Employee_Code"].ToString();
        txtemployeedept.Text = dt.Rows[0]["Employee_Dept"].ToString();
        txtinvolvedept.Text = dt.Rows[0]["Involve_Dept"].ToString();
        txtcollege.Text = dt.Rows[0]["College"].ToString();
        TextBox1.Text = dt.Rows[0]["Query"].ToString();
        txtcomplaintype.Text = dt.Rows[0]["ComplaintType"].ToString();
        txtemail.Text = dt.Rows[0]["Email_ID"].ToString();
        txtmobile.Text = dt.Rows[0]["Mobile_No"].ToString();
        txtID.Text = dt.Rows[0]["ID"].ToString();
        Label2.Text = dt.Rows[0]["ID"].ToString();
        Label4.Text = dt.Rows[0]["Create_Date"].ToString();
        drpstatus.SelectedItem.Text = dt.Rows[0]["IssueStatus"].ToString();
        drpassignto.SelectedItem.Text = dt.Rows[0]["AssignTo"].ToString();
        if (dt.Rows[0]["AssignToID"].ToString() == "")
        {
            drpassignto.SelectedValue = "0";
        }
        else
        {
            drpassignto.SelectedValue = dt.Rows[0]["AssignToID"].ToString();
        }
        Label6.Text = dt.Rows[0]["IssueStatus"].ToString();
        Label8.Text = dt.Rows[0]["AssignTo"].ToString();
        txtremark.Text = dt.Rows[0]["Remarks"].ToString();
        hide();
       // open();

    }

    protected void btn_Reset_Click(object sender, EventArgs e)
    {
        Response.Redirect("IssueTrackerList.aspx");
    }

    protected void grdissuetracker_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdissuetracker.PageIndex = e.NewPageIndex;
        getIssuetracker();
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
    
public override void VerifyRenderingInServerForm(Control control)
{

}
protected void Button1_Click(object sender, EventArgs e)
{
    System.IO.StringWriter stringWrite = new System.IO.StringWriter();

    System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

    grdissuetracker.RenderControl(htmlWrite);

    Response.Clear();
    string str = "IssueTracker" + Session["uid"].ToString(); ;
    Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

    Response.Charset = "";

    Response.Cache.SetCacheability(HttpCacheability.NoCache);

    Response.ContentType = "application/vnd.xls";

    Response.Write(stringWrite.ToString());


    Response.End();
    }

    public void hide()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("select * from tbl_IssueTracker where [Id]='" + txtID.Text + "'", con))
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string IssueStatus = dr["IssueStatus"].ToString();
                    string AssignBy = dr["AssignBy"].ToString();
                    string AssignToID = dr["AssignToID"].ToString();
                    con.Close();
                    if (AssignBy == "TMU01979" || IssueStatus == "Assign To")

                    {
                        drpstatus.Enabled = false;
                        drpassignto.Enabled = false;
                    }
                    else
                    {
                        drpstatus.Enabled = true;
                        drpassignto.Enabled = true;
                    }

                    if (AssignBy == "TMU06106" || IssueStatus == "Assign To")
                    {
                        drpstatus.Enabled = false;
                        drpassignto.Enabled = false;
                    }
                    else
                    {
                        drpstatus.Enabled = true;
                        drpassignto.Enabled = true;
                    }
                    if (AssignBy == "TMU07001" || IssueStatus == "Assign To")
                    {
                        drpstatus.Enabled = false;
                        drpassignto.Enabled = false;
                    }
                    else
                    {
                        drpstatus.Enabled = true;
                        drpassignto.Enabled = true;
                    }
                    if (AssignToID == Session["uid"].ToString())
                    {
                        drpstatus.Enabled = true;
                        drpassignto.Enabled = true;
                    }
                    //else
                    //{
                    //    drpstatus.Enabled = false;
                    //    drpassignto.Enabled = false;
                    //}
                }
            }
        }
    }
   

    //public void open()
    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    //    {
    //        using (SqlCommand cmd = new SqlCommand("select * from tbl_IssueTracker where [Id]='" + txtID.Text + "'", con))
    //        {
    //            con.Open();
    //            SqlDataReader dr = cmd.ExecuteReader();
    //            if (dr.Read())
    //            {
    //                //string Status = dr["drpstatus"].ToString();
    //                string AssignToID = dr["AssignToID"].ToString();
    //                con.Close();
    //                if (AssignToID == Session["uid"].ToString())
    //                //if(AssignToID =='"+ Session["uid"].ToString()+ "'))
    //                {
    //                    drpstatus.Enabled = true;
    //                    drpassignto.Enabled = true;
    //                }                  
    //                else
    //                {
    //                    drpstatus.Enabled = false;
    //                    drpassignto.Enabled = false;
    //                }
    //            }
    //        }
    //    }
    //}
}
