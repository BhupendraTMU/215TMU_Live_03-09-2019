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

public partial class Faculty_Librarymembershipform : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
               try
               {
            binddata();
            bindlibrarymembership();
            //submitfrm();
            submitfrm1();
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
        string strSQL = "select [PAN No],[Address 2] AS PerAddress,case when [Global Dimension 1 Code]='TMMC' then case when [Sanctioning Incharge]='' then HOD else [Sanctioning Incharge] end else HOD end HOD,Address,case when Gender='1' then 'Female' else 'Male' end as Gender, No_,[First Name],[Father Name],convert(date,[Birth Date]) as DOB,[Mobile Phone No_],[Job Title_Grade Desc],[Account No],[Branch Name] from TMU$Employee where [No_]='" + Session["uid"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            txtemployeecode.Text = dt.Rows[0]["No_"].ToString();
            txtemployeename.Text = dt.Rows[0]["First Name"].ToString();
            txtdateofbirth.Text = dt.Rows[0]["DOB"].ToString();
            txtdepartment.Text = dt.Rows[0]["Branch Name"].ToString();
            txtmobile.Text = dt.Rows[0]["Mobile Phone No_"].ToString();
            txtdesignation.Text = dt.Rows[0]["Job Title_Grade Desc"].ToString();
            txtlocaladdress.Text = dt.Rows[0]["Address"].ToString();
            txtpermentaddress.Text = dt.Rows[0]["PerAddress"].ToString();
            txthod.Text = dt.Rows[0]["HOD"].ToString();
        }
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
    public void bindlibrarymembership()
    {
        try

        {
            byte[] bytes = GetData("select Upload_Photo as FacultyImage from Tbl_LibraryMembership  WHERE [Employee_Code] = '" + Session["uid"].ToString() + "'").Rows[0]["FacultyImage"].ToString() == "" ? null : (byte[])GetData("select Upload_Photo as FacultyImage from Tbl_LibraryMembership  WHERE [Employee_Code] = '" + Session["uid"].ToString() + "'").Rows[0]["FacultyImage"];
            if (bytes != null)
            {
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                ImgPrv.ImageUrl = "data:image/png;base64," + base64String;
            }

        }
        catch (Exception ex)
        {
        }


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "select * from Tbl_LibraryMembership  WHERE [Employee_Code] = '" + Session["uid"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {

            txtemployeecode.Text = dt.Rows[0]["Employee_code"].ToString();
            txtemployeename.Text = dt.Rows[0]["Name"].ToString();
            txtdesignation.Text = dt.Rows[0]["Designation"].ToString();
            txtdepartment.Text = dt.Rows[0]["Department"].ToString();
            txtdateofbirth.Text = dt.Rows[0]["DOB"].ToString();
            txtlocaladdress.Text = dt.Rows[0]["Local_address"].ToString();
            txtpermentaddress.Text = dt.Rows[0]["Per_address"].ToString();
            txtmobile.Text = dt.Rows[0]["Contact_no"].ToString();
            txtemail.Text = dt.Rows[0]["Email"].ToString();
            txtID.Text = dt.Rows[0]["ID"].ToString();
            lblhodstatus1.Text= dt.Rows[0]["Status"].ToString();
            lbldeputylibraianstatus.Text=dt.Rows[0]["Approval_status"].ToString();



        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (txtmobile.Text == "") 
        {
            string message1 = "Please Fill Mobile No.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (txtlocaladdress.Text == "")
        {
            string message1 = "Please Fill Local Address";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (txtpermentaddress.Text == "")
        {
            string message1 = "Please Fill Permenent Address";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (txtemail.Text == "")
        {
            string message1 = "Please Fill Email Id";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if(FileUpload2.HasFile)
        {
           
        }
        else
        {
            string message1 = "Please Upload Photo";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }


        if (chkaccept.Checked)
        {
            // if (!Page.IsPostBack)
            string status = "Y";
        }
        else
        {

            string message1 = " PLEASE READ AND CHECK RULES AND REGULATIONS OF LIBRARY";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;

        }

            string contentType1 = "";
        byte[] Photo = new byte[720];
        if (FileUpload2.HasFile)
        {
            contentType1 = FileUpload2.PostedFile.ContentType;
            string filename = Path.GetFileName(FileUpload2.PostedFile.FileName);
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
            string filename = "";
            using (Stream fs = FileUpload2.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    Photo = br.ReadBytes((Int32)0);
                }
            }
        }
        string Declaration = chkaccept.Checked ? "Y" : "N";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_InsertMemshipdetail", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Creation_date", "");
        cmd.Parameters.AddWithValue("@Employee_code", txtemployeecode.Text);
        cmd.Parameters.AddWithValue("@Name", txtemployeename.Text);
        cmd.Parameters.AddWithValue("@Designation", txtdesignation.Text);
        cmd.Parameters.AddWithValue("@Department", txtdepartment.Text);
        cmd.Parameters.AddWithValue("@Local_address", txtlocaladdress.Text);
        cmd.Parameters.AddWithValue("@Per_address", txtpermentaddress.Text);
        cmd.Parameters.AddWithValue("@Contact_no", txtmobile.Text);
        cmd.Parameters.AddWithValue("@Email", txtemail.Text);
        cmd.Parameters.AddWithValue("@DOB", txtdateofbirth.Text);
        cmd.Parameters.AddWithValue("@Upload_Photo", Photo);
        cmd.Parameters.AddWithValue("@Form_Status", "Submit");
        //cmd.Parameters.AddWithValue("@Employee_code_ICard", txtemployeecodecode1.Text);
        //cmd.Parameters.AddWithValue("@Prepared_by_Icard", txtpreparedby1.Text);
        //cmd.Parameters.AddWithValue("@Date_of_Issue_Icard", txtdateofissue.Text);
        //cmd.Parameters.AddWithValue("@Received_byICard", txtreceivefaulty1.Text);
        //cmd.Parameters.AddWithValue("@Date_dues", date1.Text);
        //cmd.Parameters.AddWithValue("@Faculty_Dues", txtfacultystat1.Text);
        //cmd.Parameters.AddWithValue("@RecbyDues", txtreceivedby.Text);
        cmd.Parameters.AddWithValue("@Status ", "Pending");
        cmd.Parameters.AddWithValue("@Approval_status", "Pending");
        cmd.Parameters.AddWithValue("@Declaration", Declaration);
        cmd.Parameters.AddWithValue("@HOD", txthod.Text);
        cmd.Parameters.AddWithValue("Deputy_librarin", "TMU00478");
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
        Response.Redirect("Librarymembershipform.aspx");
    }
    public void submitfrm1()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("select * from Tbl_LibraryMembership where [Employee_code]='" + Session["uid"].ToString() + "'", con))
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string Form_Status = dr["Form_Status"].ToString();
                    string Approval_status = dr["Approval_status"].ToString();
                    
                    con.Close();
                    if (Form_Status =="Submit")

                    {
                        btnsave.Visible = false;
                        txtlocaladdress.Enabled = false;
                        txtpermentaddress.Enabled = false;
                        txtemail.Enabled = false;
                        txtmobile.Enabled = false;
                        FileUpload2.Enabled = false;
                        chkaccept.Enabled = false;
                        lblhodstatus.Visible = true;
                        lblhodstatus1.Visible = true;
                        Label2.Visible = true;
                        lbldeputylibraianstatus.Visible = true;
                        Button1.Visible = true;

                    }
                    else
                    {
                        btnsave.Visible = true;
                        txtlocaladdress.Enabled = true;
                        txtpermentaddress.Enabled = true;
                        txtemail.Enabled = true;
                        txtmobile.Enabled = true;
                        FileUpload2.Enabled = true;
                        chkaccept.Enabled = true;
                        lblhodstatus.Visible = true;
                        lblhodstatus1.Visible = true;
                        Label2.Visible = true;
                        lbldeputylibraianstatus.Visible = true;
                        Button1.Visible = false;
                    }
                }
            }
        }
    }
    //public void submitfrm()
    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    //    {
    //        using (SqlCommand cmd = new SqlCommand("select * from Tbl_LibraryMembership where [Employee_code]='" + Session["uid"].ToString() + "'", con))
    //        {
    //            con.Open();
    //            SqlDataReader dr = cmd.ExecuteReader();
    //            if (dr.Read())
    //            {
    //                string Status = dr["Status"].ToString();
    //                string Approval_status = dr["Approval_status"].ToString();
    //                con.Close();
    //                if (Status == "Rejected" || Approval_status=="Blocked") 

    //                {
    //                    btnsave.Visible = true;
    //                    txtlocaladdress.Enabled = true;
    //                    txtpermentaddress.Enabled = true;
    //                    txtemail.Enabled = true;
    //                    txtmobile.Enabled = true;
    //                    FileUpload2.Enabled = true;
    //                    chkaccept.Enabled = true;
    //                    lblhodstatus.Visible = true;
    //                    lblhodstatus1.Visible = true;
    //                    Label2.Visible = true;
    //                    lbldeputylibraianstatus.Visible = true;
    //                    Button1.Visible = false;
    //                }
    //                else 
    //                {
    //                    //btnsave.Enabled = true;
    //                    //txtlocaladdress.Enabled = true;
    //                    //txtpermentaddress.Enabled = true;
    //                    //txtemail.Enabled = true;
    //                    //txtmobile.Enabled = true;
    //                    //FileUpload2.Enabled = true;
    //                    //chkaccept.Enabled = true;
    //                    //lblhodstatus.Visible = true;
    //                    //lblhodstatus1.Visible = true;
    //                    //Label2.Visible = true;
    //                    //lbldeputylibraianstatus.Visible = true;
    //                }
    //            }
    //        }
    //    }
    //}
}