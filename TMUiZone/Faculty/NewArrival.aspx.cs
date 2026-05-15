using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class Faculty_NewArrival : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            try
            {
                getcollegecode();
                getdeptlibrary();
                if (Session["uid"].ToString() == "TMU00478" || Session["uid"].ToString() == "TMU04426")
                {
                    getnewarrivallistM();
                }
                else

                {

                    getnewarrivallist();
                }
                //getcollegecode1();


            }
            catch
            {
                Response.Redirect("../Default.aspx");
            }

        }
    }
    public void getnewarrivallistM()
    {

        SqlCommand cmd = new SqlCommand("pro_getNewArrivalDataM", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@College_code", Session["GlobalDimension1Coded"].ToString());
        //cmd.Parameters.AddWithValue("@Uploaded_by", Session["uid"].ToString());
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdnewarrivallist.DataSource = dtCL;
        grdnewarrivallist.DataBind();
        DropDownList1.DataSource = dtCL;
        DropDownList1.DataTextField = "College_Code";
        DropDownList1.DataValueField = "Id";
        DropDownList1.DataBind();
        DropDownList2.DataSource = dtCL;
        DropDownList2.DataTextField = "College_Code";
        DropDownList2.DataValueField = "Id";
        DropDownList2.DataBind();

    }
    public void getnewarrivallist()
    {

        SqlCommand cmd = new SqlCommand("pro_getNewArrivalData", con);       
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@College_code", Session["GlobalDimension1Coded"].ToString());
        //cmd.Parameters.AddWithValue("@Uploaded_by", Session["uid"].ToString());
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);       
        grdnewarrivallist.DataSource = dtCL;
        grdnewarrivallist.DataBind();
        DropDownList1.DataSource = dtCL;
        DropDownList1.DataTextField = "College_Code";
        DropDownList1.DataValueField = "Id";
        DropDownList1.DataBind();
        DropDownList2.DataSource = dtCL;
        DropDownList2.DataTextField = "College_Code";
        DropDownList2.DataValueField = "Id";
        DropDownList2.DataBind();

    }

    //public void getcollegecode1()
    //{

    //    SqlCommand cmd = new SqlCommand("pro_getNewArrivalData", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@College_code", Session["GlobalDimension1Coded"].ToString());
    //    cmd.Parameters.AddWithValue("@Uploaded_by", Session["uid"].ToString());
    //    if (con.State == ConnectionState.Closed)
    //        con.Open();
    //    SqlDataAdapter daCL = new SqlDataAdapter(cmd);
    //    DataTable dtCL = new DataTable();
    //    daCL.Fill(dtCL);
        


    //}
    public void getcollegecode()
    {

        SqlCommand cmd = new SqlCommand("pro_getcollegecode", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);

        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        drpcollegecode.DataSource = dtCL;        
        drpcollegecode.DataValueField = "Code";
        drpcollegecode.DataBind();
    }
    public void getdeptlibrary()
    {

        if (Session["uid"].ToString() == "TMU00478" || Session["uid"].ToString() == "TMU04426")
       {

            divGeneralBody.Visible = true;
            Label1.Visible = true;
            fieldset1.Visible = true;
            //fieldset2.Visible = true;
            Label2.Visible = true;
            grdnewarrivallist.Visible = true;
            BtnRejected.Visible = true;
            Btnexporttoexel.Visible = true;
        }
        else
        {

            divGeneralBody.Visible = false;
           Label1.Visible = false;
           fieldset1.Visible = false;
            //fieldset2.Visible = false;
            Label2.Visible = true;
            grdnewarrivallist.Visible = true;
            BtnRejected.Visible = false;
            Btnexporttoexel.Visible = false;
        }

       }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        string contentType1 = ""; string filename = "";
        byte[] Photo = new byte[720];
        if (FileUpload2.HasFile)
        {

            contentType1 = FileUpload2.PostedFile.ContentType;
           filename = Path.GetFileName(FileUpload2.PostedFile.FileName);
            using (Stream fs = FileUpload2.PostedFile.InputStream)
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
         
            using (Stream fs = FileUpload2.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    Photo = br.ReadBytes((Int32)0);
                }
            }
        }
        if (drpcas.SelectedItem.Text == "Select")
        {
            string message1 = "Please Fill CAS.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }

        if (drpacademicyear.SelectedItem.Text == "Select")
        {
            string message1 = "Please Fill Academic Year.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }

        if (txtremarks.Text == "")
        {
            string message1 = "Please Fill Remarks.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_insertnewarrival", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Creation_date", "");
        cmd.Parameters.AddWithValue("@College_Code", drpcollegecode.SelectedValue);
        cmd.Parameters.AddWithValue("@Cas", drpcas.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Academic_Year", drpacademicyear.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarks.Text);
        cmd.Parameters.AddWithValue("@Attachment", Photo);
        cmd.Parameters.AddWithValue("@Uploaded_by", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@contentType", contentType1);
        cmd.Parameters.AddWithValue("@fileName", filename);
        cmd.Parameters.AddWithValue("@Status", "Submit");
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
        Response.Redirect("NewArrival.aspx");
    }
   

    protected void lnkPhoto_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        GridViewRow row = btn.NamingContainer as GridViewRow;
        string id = grdnewarrivallist.DataKeys[row.RowIndex].Values[0].ToString();
        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select * from Tbl_NewArrivalTable where ID=" + id + "";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    //sdr.Read();
                    //bytes = (byte[])sdr["Attachment"];


                    //fileName = "Photo";


                    sdr.Read();
                    bytes = (byte[])sdr["Attachment"];

                    contentType = sdr["Content Type"].ToString();
                    fileName = sdr["FileName"].ToString();
                }
                con.Close();
            }
        }
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + "");
        Response.ContentType = contentType;
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }

    //protected void lnkview_Click(object sender, EventArgs e)
    //{
    //    GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
    //    int index = row.RowIndex;
    //    Label UserId = (Label)grdnewarrivallist.Rows[index].FindControl("lblemployeecode");
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    //    string strSQL = "select * from Tbl_NewArrivalTable WHERE [Id]='" + UserId.Text + "'" ;
    //    SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    con.Close();
    //    DropDownList1.SelectedItem.Text = dt.Rows[0]["College_Code"].ToString();
    //    DropDownList2.SelectedItem.Text = dt.Rows[0]["Academic_Year"].ToString();
    //    DropDownList3.SelectedItem.Text = dt.Rows[0]["Cas"].ToString();
    //    TextBox1.Text = dt.Rows[0]["Remark"].ToString();


    //    GridViewDetails.Show();
        
    //}

    

    protected void btnclose_Click(object sender, EventArgs e)
    {
        GridViewDetails.Hide();
    }

    protected void grdnewarrivallist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdnewarrivallist.PageIndex = e.NewPageIndex;
        getnewarrivallistM();
    }
    protected void BtnRejected_Click(object sender, EventArgs e)
    {
        try
        {
            int i = 0;
            foreach (GridViewRow row in grdnewarrivallist.Rows)
            {
                CheckBox check = (CheckBox)row.FindControl("Chkemployee");
                Label lblemployeecode = (Label)row.FindControl("lblemployeecode");
                var id = grdnewarrivallist.DataKeys[row.RowIndex].Value;
                SqlCommand cmd = new SqlCommand("pro_Delete", con);
                if (check.Checked == true)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", lblemployeecode.Text);
                    cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    i++;
                }
            }
            if (i > 0) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' Record Delete')", true); }
            else { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Data.. !')", true); }
            Response.Redirect("NewArrival.aspx");

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
        grdnewarrivallist.RenderControl(htmlWrite);
        Response.Clear();
        string str = "NEWARRIVALREPORT" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        Response.Write(stringWrite.ToString());
        Response.End();
    }
}
