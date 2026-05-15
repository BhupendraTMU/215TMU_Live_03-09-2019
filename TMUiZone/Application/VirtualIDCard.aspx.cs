using System;

using System.Linq;
using System.Web;
using System.Web.UI;

using System.Data;
using System.Data.SqlClient;
using System.IO;

using System.Text;

using System.Configuration;
using System.Drawing;
using QRCoder;
using iTextSharp.text;

using iTextSharp.text.pdf;


public partial class Application_VirtualIDCard : System.Web.UI.Page
{
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString);
    SqlConnection Conn = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                con1.Open();
            SqlDataAdapter da = new SqlDataAdapter("select case when [Father’s_Guardian Mobile No_]='' then [Gaurdian Mobile No] else [Father’s_Guardian Mobile No_] end 'Phone',(SELECT [verified]  FROM HRMSPortal.[dbo].[tbl_OTPforVirtualID] WHERE  [StudentNo]='" + Session["uid"].ToString() + "') as 'OTPVerified' from [TMU$Student - COLLEGE] where [No_]='" + Session["uid"].ToString() + "'", con1);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows[0]["OTPVerified"].ToString() == "1")
            {
                fsParent.Disabled = true;
                LinkButton1.Enabled = false;
                fsStudent.Disabled = false;
                Button2.Enabled = false;
                LinkButton1.Text = "Verified";
            }
            else
            {
                btnSubmit.Enabled = false;
                fsStudent.Disabled = true;
                Label2.Enabled = false;
                txtOTP.Enabled = false;
            }
            con1.Close();
            }
            catch
            {
                Response.Redirect("../Default.aspx");
            }
        }
    }


    protected void Label2_Click(object sender, EventArgs e)
    {
        con1.Open();
        SqlDataAdapter da = new SqlDataAdapter("select case when [Mobile Number]='' then [Phone Number] else [Mobile Number] end 'Phone',(SELECT [OTPCount]  FROM HRMSPortal.[dbo].[tbl_OTPforVirtualID] WHERE  [StudentNo]='" + Session["uid"].ToString() + "') as 'OTPCount' from [TMU$Student - COLLEGE] where [No_]='" + Session["uid"].ToString() + "'", con1);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows[0]["OTPCount"].ToString() == "5")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You have exceed the OTP limit ,Please contact to admission Cell.');", true);
            return;
        }

        if (dt.Rows[0]["Phone"] != "")
        {
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string sRandomOTP = GenerateRandomOTP(4, saAllowedCharacters);

            string sqlq = "SP_InsertOTP '" + Session["uid"].ToString() + "'," + sRandomOTP + "";


            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(sqlq, Conn);
            cmd.ExecuteNonQuery();
            Conn.Close();
            string Mobile = dt.Rows[0]["Phone"].ToString();
            SMS(Mobile, "Dear Student,Your OTP is " + sRandomOTP + " for Virtual Id confirmation. Thank you. TMU");
            string somestring = Mobile;
            StringBuilder sb = new StringBuilder(somestring);
            sb[2] = '*';
            sb[3] = '*';
            sb[4] = '*';
            sb[5] = '*';
            somestring = sb.ToString();
            lblMSGOTP.Visible = true;
            //lblMSG.Visible = true;
            lblMSGOTP.Text = "OTP sent successfully for your registered mobile number " + somestring + " .OTP Valid for 15 minutes.";
            //lblMSG.Text = "OTP will expire in : "++"";
        }
        else
        {
            lblMSGOTP.Text = "Mobile No. is not updated in ERP.";
        }
        con1.Close();
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string result = "";
        con1.Open();
        SqlDataAdapter da = new SqlDataAdapter("SELECT [OTP],case when DATEADD(minute,30,createdate)<GETDATE() then '0' else '1' end valid  FROM HRMSPortal.[dbo].[tbl_OTPforVirtualID] WHERE  [StudentNo]='" + Session["uid"].ToString() + "'", con1);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["valid"].ToString() == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('OTP time expired !');", true);
                return;
            }

            if (txtOTP.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Enter OTP Number.');", true);
                return;
            }
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["OTP"].ToString().TrimEnd() != txtOTP.Text)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You have entered wrong OTP.');", true);
                    return;
                }
            }
        }

        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You have entered wrong OTP.');", true);
            return;
        }

        SqlDataAdapter da1 = new SqlDataAdapter(" select No_,[Student Name],[Fathers Name],No_,[Global Dimension 1 Code],[Course Name],[Admitted Year] ,case when [Student Status]=1 then 'Student' when [Student Status]=2 then 'Inactive' when [Student Status]=3 then 'Alumini' when [Student Status]=4 then 'NR' when [Student Status]=5 then 'Re-Appear' when [Student Status]=6 then 'Intership'  end 'Student Status',[Course Code],[Admitted Year],[Mobile Number],case when [Father’s_Guardian Mobile No_]='' then [Gaurdian Mobile No] else [Father’s_Guardian Mobile No_] end as PMobile,Address1 from [TMU$Student - COLLEGE] where No_='" + Session["uid"].ToString() + "'", con1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);

        if (dt1.Rows.Count > 0)
        {
            PanelQuotation.Visible = true;
            lblStuName.Text = ": " + dt1.Rows[0]["Student Name"].ToString();
            lblFather.Text = ": " + dt1.Rows[0]["Fathers Name"].ToString();
            lblStudent.Text = ": " + dt1.Rows[0]["No_"].ToString();
            lblCollege.Text = ": " + dt1.Rows[0]["Global Dimension 1 Code"].ToString();
            lblCourse.Text = ": " + dt1.Rows[0]["Course Name"].ToString();
            lblAdmittedYear.Text = ": " + dt1.Rows[0]["Admitted Year"].ToString();
            lblM.Text = "Mo." + dt1.Rows[0]["Mobile Number"].ToString();
            //lblStMobile.Text = ": " + dt1.Rows[0]["Mobile Number"].ToString();
            lblPMobile.Text = ": " + dt1.Rows[0]["PMobile"].ToString();
            //string

            StringBuilder sb = new StringBuilder();
            sb.Append("Student Name : " + dt1.Rows[0]["Student Name"].ToString() + "");
            sb.Append(Environment.NewLine);
            sb.Append("Program : " + dt1.Rows[0]["Course Code"].ToString() + "");
            sb.Append(Environment.NewLine);
            sb.Append("Status : " + dt1.Rows[0]["Student Status"].ToString() + "");
            sb.Append(Environment.NewLine);
            sb.Append("Mobile : " + dt1.Rows[0]["Mobile Number"].ToString() + "");
            sb.Append(Environment.NewLine);
            sb.Append("Address : " + dt1.Rows[0]["Address1"].ToString() + "");
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(sb.ToString(), QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(30);
            using (MemoryStream ms = new MemoryStream())
            {
                qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();
                Image1.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            }
            byte[] bytes = GetData("select [Student Image] as FacultyImage from [EDUCOLLEGELIVE-R2].dbo.[TMU$Student - COLLEGE] where [No_] = '" + Session["uid"].ToString() + "'").Rows[0]["FacultyImage"].ToString() == "" ? null : (byte[])GetData("select [Student Image] as FacultyImage  from[TMU$Student - COLLEGE] where [No_] = '" + Session["uid"].ToString() + "'").Rows[0]["FacultyImage"];

            if (bytes != null)
            {
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                ImgPrv.ImageUrl = "data:image/png;base64," + base64String;

            }
            //ModalPopupMsg.Show();
        }
    }
    private DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
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
    protected void btncancelpopup_Click(object sender, EventArgs e)
    {
        Response.Redirect("VirtualIDCard.aspx");
        //divmsg.InnerHtml = "";

        //ModalPopupMsg.Hide();

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        con1.Open();
        SqlDataAdapter da = new SqlDataAdapter("select case when [Father’s_Guardian Mobile No_]='' then [Gaurdian Mobile No] else [Father’s_Guardian Mobile No_] end 'Phone',(SELECT [verified]  FROM HRMSPortal.[dbo].[tbl_OTPforVirtualID] WHERE  [StudentNo]='" + Session["uid"].ToString() + "') as 'OTPVerified' from [TMU$Student - COLLEGE] where [No_]='" + Session["uid"].ToString() + "'", con1);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows[0]["OTPVerified"].ToString() == "1")
        {
            fsParent.Disabled = true;
            LinkButton1.Enabled = false;
            fsStudent.Disabled = false;
            Button2.Enabled = false;
            LinkButton1.Text = "Verified";
        }
        else
        {
            btnSubmit.Enabled = false;
            fsStudent.Disabled = true;
            Label2.Enabled = false;
            txtOTP.Enabled = false;

        }

        if (dt.Rows[0]["Phone"] != "")
        {
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string sRandomOTP = GenerateRandomOTP(4, saAllowedCharacters);

            string sqlq = "SP_InsertOTPFather '" + Session["uid"].ToString() + "'," + sRandomOTP + "";


            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(sqlq, Conn);
            cmd.ExecuteNonQuery();
            Conn.Close();
            string Mobile = dt.Rows[0]["Phone"].ToString();
            //dt.Rows[0]["Phone"].ToString();
            SMS(Mobile , "Dear Student,Your OTP is " + sRandomOTP + " for Virtual Id confirmation. Thank you. TMU");
            string somestring = Mobile;
            StringBuilder sb = new StringBuilder(somestring);
            sb[2] = '*';
            sb[3] = '*';
            sb[4] = '*';
            sb[5] = '*';
            somestring = sb.ToString();
            Label13.Visible = true;
            //lblMSG.Visible = true;
            Label13.Text = "OTP sent successfully for your father registered mobile number " + somestring + " .OTP Valid for 15 minutes.";
            //lblMSG.Text = "OTP will expire in : "++"";
        }
        else
        {
            Label13.Text = "Mobile No. is not updated in ERP.";
        }
        con1.Close();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string result = "";
        con1.Open();
        SqlDataAdapter da = new SqlDataAdapter("SELECT [FatherOTP],case when DATEADD(minute,30,FathercreateDate)<GETDATE() then '0' else '1' end valid  FROM HRMSPortal.[dbo].[tbl_OTPforVirtualID] WHERE  [StudentNo]='" + Session["uid"].ToString() + "'", con1);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["valid"].ToString() == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('OTP time expired !');", true);
                return;
            }

            if (TextBox1.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Enter OTP Number.');", true);
                return;
            }
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["FatherOTP"].ToString().TrimEnd() != TextBox1.Text)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You have entered wrong OTP.');", true);
                    return;
                }
            }
        }

        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You have entered wrong OTP.');", true);
            return;
        }

        string sqlq = "SP_UpdateFatherStatus '" + Session["uid"].ToString() + "'";


        if (Conn.State != ConnectionState.Open)
        {
            Conn.Open();
        }
        SqlCommand cmd = new SqlCommand();
        cmd = new SqlCommand(sqlq, Conn);
        cmd.ExecuteNonQuery();
        Conn.Close();

        Response.Redirect("VirtualIDCard.aspx");

    }



    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    using (MemoryStream stream = new MemoryStream())
    //    {
    //        using (XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8))
    //        {
    //           writer.
    //        }
    //        byte[] content = stream.ToArray();
    //        Response.AddHeader("Content-disposition", "attachment; filename=AuthenticatedLicense.pdf");
    //        Response.ContentType = "application/octet-stream";
    //        Response.BinaryWrite(content);
    //        Response.End();
    //    }
    //}

    protected void ExportToImage(object sender, EventArgs e)
    {
        string imagedata = hfImageData.Value;
        string convert = imagedata.Replace("data:image/png;base64,", String.Empty);
        byte[] imagebytes = Convert.FromBase64String(convert);

        //Initialize the PDF document object.
        Document pdfDoc = new Document();
        
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();

            //Add the Image file to the PDF document object.
            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagebytes);
            pdfDoc.Add(img);
            pdfDoc.Close();

            //Download the PDF file.
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=VirtualIDCard.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();
        
    }
}