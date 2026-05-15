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
using System.Text;
using System.Drawing;

public partial class Student_JobWork : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {


                BindDepartment();
                bindOrderList();
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../Default.aspx");
        }
    }

    public void BindInformation()
    {
        SqlCommand cmd = new SqlCommand("proc_StudentInformation", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@StudentNo", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@DeptName", ddlDepartment.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            hfWardenMobile.Value = dt.Rows[0]["Warden Mobile No_"].ToString();
            hfWardenEmployee.Value = dt.Rows[0]["Warden Emp Code"].ToString();
            txtArea.Text = dt.Rows[0]["Buiding  Name"].ToString();
            txtRoomNo.Text = dt.Rows[0]["Room No_"].ToString();
            txtWarden.Text = dt.Rows[0]["Warden Emp Name"].ToString();
        }
    }


    public void bindOrderList()
    {
        if (Request.Browser.IsMobileDevice)
        {
            OrderList1.Width = 380;
        }
        con1.Open();
        SqlDataAdapter da = new SqlDataAdapter("SELECT [Complaint_No],[Department],[Area] ,Case when [Status]=1 then 'Pending at Warden' when [Status]=2 then 'Pending at Department' when [Status]=3 then 'Rejected by Warden' when [Status]=4 then 'Resolved from Department' when [Status]=5 then 'Close' end as 'Status'  ,[CreateDate],[Remarks],[CloseDate],[AttachmentFilename],[Reopen],[CustomerMobile],[AttachmentFilename1]  FROM [dbo].[tbl_Complaint] WHERE  [Customer Code]='" + Session["uid"].ToString() + "'", con1);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            OrderList1.DataSource = dt;
            OrderList1.DataBind();
            if (OrderList1.Rows.Count > 0)
            {
                OrderList1.HeaderRow.Cells[0].Attributes["data-class"] = "expand";


                OrderList1.HeaderRow.Cells[1].Attributes["data-class"] = "phone";
                OrderList1.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.Cells[5].Attributes["data-class"] = "phone";
                //GridView1.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.Cells[7].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.Cells[8].Attributes["data-hide"] = "phone";

                OrderList1.HeaderRow.Cells[9].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.Cells[10].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }


    }



    public void BindDepartment()
    {
        SqlCommand cmd = new SqlCommand("proc_fatchDepartment", con1);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataTextField = "ID";
            ddlDepartment.DataValueField = "ID";
            ddlDepartment.DataBind();


        }
    }




    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Session["FileUpload1"] == null && flUpload.HasFile)
        {
            Session["FileUpload1"] = flUpload;
            fileName.Text = flUpload.FileName;
        }
        // Next time submit and Session has values but FileUpload is Blank
        // Return the values from session to FileUpload
        else if (Session["FileUpload1"] != null && (!flUpload.HasFile))
        {
            flUpload = (FileUpload)Session["FileUpload1"];
            fileName.Text = flUpload.FileName;
        }
        // Now there could be another sictution when Session has File but user want to change the file
        // In this case we have to change the file in session object
        else if (flUpload.HasFile)
        {
            Session["FileUpload1"] = flUpload;
            fileName.Text = flUpload.FileName;
        }
        string result = "";
        con1.Open();
        SqlDataAdapter da = new SqlDataAdapter("SELECT [OTP],case when DATEADD(minute,15,createdate)<GETDATE() then '0' else '1' end valid  FROM HRMSPortal.[dbo].[tbl_OTPforComplaint] WHERE  [StudentNo]='" + Session["uid"].ToString() + "'", con1);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["valid"].ToString() == "0")
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'OTP time expired !');", true);


                return;
            }

            if (txtOTP.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Please Enter OTP Number.');", true);

                return;
            }
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["OTP"].ToString().TrimEnd() != txtOTP.Text)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'You have entered wrong OTP.');", true);

                    return;
                }
            }
        }

        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You have entered wrong OTP.');", true);
            return;
        }
        byte[] bytes = null;
        byte[] imgtype = { 0 };
        string filename = "";
        string contentType = "";
        string Code = "";
        if (flUpload.HasFile)
        {

            filename = Path.GetFileName(flUpload.PostedFile.FileName);
            contentType = flUpload.PostedFile.ContentType;
            using (Stream fs = flUpload.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    bytes = br.ReadBytes((Int32)fs.Length);
                }
            }
        }

        SqlCommand cmd = new SqlCommand("Customer_InsertComplaint", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Department", ddlDepartment.SelectedValue);
        cmd.Parameters.Add("@Area", txtArea.Text);
        cmd.Parameters.Add("@Room", txtRoomNo.Text);
        cmd.Parameters.Add("@WardenName", txtWarden.Text);
        cmd.Parameters.Add("@WardenCode", hfWardenEmployee.Value);
        cmd.Parameters.Add("@CustomerCode", Session["uid"].ToString());
        cmd.Parameters.Add("@Remark", txtRemark.Text);
        cmd.Parameters.Add("@FileName", filename);
        cmd.Parameters.Add("@contentType", contentType);
        if (bytes == null)
        {

            cmd.Parameters.Add("@Attachment", imgtype);
        }
        else
        {
            cmd.Parameters.Add("@Attachment", bytes);
        }
        cmd.Parameters.Add("@Status", 1);
        cmd.Parameters.Add("@OrderNo", SqlDbType.VarChar, 100);
        cmd.Parameters["@OrderNo"].Direction = ParameterDirection.Output;
        if (con1.State == ConnectionState.Closed)
            con1.Open();
        cmd.ExecuteNonQuery();

        Code = cmd.Parameters["@OrderNo"].Value.ToString();


        con1.Close();
        string WardenMobile = hfWardenMobile.Value;
        SMS(WardenMobile, "Dear " + txtWarden.Text + ", Maintenance Complaint No. " + Code + " is raised by Student " + Session["Name"].ToString() + ". Please login at portal.tmu.ac.in to view the complaint.Thank you.TMU");
        ddlDepartment.SelectedIndex = 0;
        txtArea.Text = "";
        txtRemark.Text = "";
        fileName.Text = "";
        txtOTP.Text = "";
        txtRoomNo.Text = "";
        txtWarden.Text = "";


        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Key", "callFeedbackMessage('Success', 'Your Complaint " + Code + " Generate Successfully');", true);



    }

    private string GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters)
    {

        string sOTP = String.Empty;

        string sTempChars = String.Empty;

        Random rand = new Random();

        for (int i = 0; i < iOTPLength; i++)
        {

            int p = rand.Next(0, saAllowedCharacters.Length);

            sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];

            sOTP += sTempChars;

        }

        return sOTP;

    }
    public void SMS(String MobileNo, string Msg)
    {
        //  Website: http://www.universalsmsadvertising.com/     //  Username: 9837016352   //  Password: 9837016352 
        MobileNo = "91" + MobileNo;
        // MobileNo = "91" + 9015762885;
        string url = "http://www.universalsmsadvertising.com/universalsmsapi.php?user_name=9837016352&user_password=9837016352&mobile=" + MobileNo + "&sender_id=TMUniv&type=F&text=" + Msg + "";

        // string url = "http://www.universalsmsadvertising.com/universalsmsapi.php?user_name=myuserid&user_password=mypassword&mobile=919834567120&sender_id=SWEET&type=F&text= " + Msg + "";



        System.Net.HttpWebRequest fr;
        Uri targetURI = new Uri(url);
        fr = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(targetURI);
        if (fr.GetResponse().ContentLength > 0)
        {
            System.IO.StreamReader str = new System.IO.StreamReader(fr.GetResponse().GetResponseStream());
            Response.Write(str.ReadToEnd());
            str.Close();
        }
    }
    protected void lnkGenerateOTP_Click(object sender, EventArgs e)
    {

        if (Session["FileUpload1"] == null && flUpload.HasFile)
        {
            Session["FileUpload1"] = flUpload;
            fileName.Text = flUpload.FileName;
        }

        else if (Session["FileUpload1"] != null && (!flUpload.HasFile))
        {
            flUpload = (FileUpload)Session["FileUpload1"];
            fileName.Text = flUpload.FileName;
        }
        // Now there could be another sictution when Session has File but user want to change the file
        // In this case we have to change the file in session object
        else if (flUpload.HasFile)
        {
            Session["FileUpload1"] = flUpload;
            fileName.Text = flUpload.FileName;
        }





        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("select case when [Mobile Number]='' then [Phone Number] else [Mobile Number] end 'Phone' from [TMU$Student - COLLEGE] where [No_]='" + Session["uid"].ToString() + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);



        if (dt.Rows[0]["Phone"] != "")
        {
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string sRandomOTP = GenerateRandomOTP(4, saAllowedCharacters);

            string sqlq = "SP_InsertOTPforComplaint '" + Session["uid"].ToString() + "'," + sRandomOTP + "";


            if (con1.State != ConnectionState.Open)
            {
                con1.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(sqlq, con1);
            cmd.ExecuteNonQuery();
            con1.Close();
            string Mobile = dt.Rows[0]["Phone"].ToString(); 
            //
            SMS(Mobile, "Dear Student,Your OTP is " + sRandomOTP + " for Registering complaint. Thank you. Teerthanker Mahaveer University");
            string somestring = Mobile;
            StringBuilder sb = new StringBuilder(somestring);
            sb[2] = '*';
            sb[3] = '*';
            sb[4] = '*';
            sb[5] = '*';
            somestring = sb.ToString();

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'OTP sent successfully for your mobile number " + somestring + " .OTP Valid for 15 minutes.');", true);


        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Mobile No. is not updated in ERP.');", true);


        }
        con1.Close();
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Orderlist.Visible = false;
        main.Visible = true;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("JobWork.aspx");
    }
    protected void lnkAttachment_Click(object sender, EventArgs e)
    {
        string AssignmentNo = (sender as LinkButton).CommandArgument;

        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strportal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {

                cmd2.CommandText = "select Attachment,AttachmentFilename,AttachmentFileType from tbl_Complaint where [Complaint_No]='" + AssignmentNo + "' ";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Attachment"];
                    contentType = sdr["AttachmentFilename"].ToString();
                    fileName = sdr["AttachmentFileType"].ToString();
                }
                con2.Close();
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
    protected void lnkAttachment1_Click(object sender, EventArgs e)
    {
        string AssignmentNo = (sender as LinkButton).CommandArgument;

        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strportal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {

                cmd2.CommandText = "select Attachment1,AttachmentFilename1,AttachmentFileType1 from tbl_Complaint where [Complaint_No]='" + AssignmentNo + "' ";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Attachment1"];
                    contentType = sdr["AttachmentFilename1"].ToString();
                    fileName = sdr["AttachmentFileType1"].ToString();
                }
                con2.Close();
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
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindInformation();
    }


    protected void OrderList1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string Status  = e.Row.Cells[5].Text;



            if (Status == "Close")
            {
                e.Row.BackColor = Color.LightGreen;
            }
            if (Status == "Pending at Department")
            {
                e.Row.BackColor = Color.Red;
            }
            if (Status == "Pending at Warden")
            {
                e.Row.BackColor = Color.Orange;
            }
            
        }
    }
}