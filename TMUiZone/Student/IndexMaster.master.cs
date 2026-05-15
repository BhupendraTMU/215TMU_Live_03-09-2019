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
using paytm;


public partial class IndexMaster : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    Connection Portalcon;
    SqlCommand com, com1;
    string HRHODsame = "";
    string forHRisHOD = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            string s1 = this.Page.Request.FilePath;
            con.Open();
            SqlCommand cmd1 = new SqlCommand("select count(*) 'C' from [tbl_StudentUpdatedRecord]  where studentno='" + Session["uid"].ToString() + "'", con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt2 = new DataTable();
            da1.Fill(dt2);
            con.Close();
            if (dt2.Rows[0]["C"].ToString() == "0")
            {
                //LOCAL
                //if (s1 != "/Student/StudentDetailsView1.aspx")
                //{
                //    Response.Redirect("/Student/StudentDetailsView1.aspx", false);
                //    HttpContext.Current.ApplicationInstance.CompleteRequest();

                //}
                //LIVE
                if (s1 != "/Student/StudentDetailsView1.aspx" && s1 != "/Student/SemRegistration.aspx")
                {
                    Response.Redirect("/Student/StudentDetailsView1.aspx", false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }



            }
            con.Open();
            SqlCommand cmd = new SqlCommand("select case when s.Year='' then s.[Semester Registration] else 1 end as 'Semester Registration',Year,[Hostel Alloted], [Global Dimension 1 Code] from  [TMU$Student - COLLEGE] s where s.[No_]='" + Session["uid"].ToString() + "' and s.[Student Status] in (1,3) ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Hostel Alloted"].ToString() == "1")
                {
                    liJobWork.Visible = true;
                    OutPass.Visible = true;
                }
                else
                {
                    liJobWork.Visible = false;
                    OutPass.Visible = false;

                }

                if (dt.Rows[0]["Semester Registration"].ToString() != null && dt.Rows[0]["Global Dimension 1 Code"].ToString() != "TMMC")
                {
                    string s = this.Page.Request.FilePath;
                    //Local Process
                    //if (dt.Rows[0]["Semester Registration"].ToString() == "0" && s != "/TMUiZone/Student/SemRegistration.aspx")
                    //{
                     //   Response.Redirect("~/Student/SemRegistration.aspx", false);
                      //  HttpContext.Current.ApplicationInstance.CompleteRequest();

                   // }


                    //Live Process
                    if (dt.Rows[0]["Semester Registration"].ToString() == "0" && s != "/Student/SemRegistration.aspx")
                    {
                        Response.Redirect("/Student/SemRegistration.aspx", false);
                       HttpContext.Current.ApplicationInstance.CompleteRequest();

                    }
                }
                if (dt.Rows[0]["Year"].ToString() != "")
                {
                    SemReg.Visible = false;
                }
            }
            
            con.Close();

            con.Open();
            com = new SqlCommand("select [Custom System Indicator Text] from [TMU$Company Information] ", con);
            SqlDataReader reader = com.ExecuteReader();
            reader.Read();
            ind.Text = reader["Custom System Indicator Text"].ToString();
            reader.Close();
            con.Close();
            //------------------------

            lblName.Text = Session["Name"].ToString();
            BINDimage();
            hidepageschoal();
            

        }
        catch (Exception ex)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
 
  
    public void hidepageschoal()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        {
            string strSQL = ("Select *,(select [Global Dimension 1 Code] from [TMU$Student - COLLEGE] where [Enrollment No_]=ML.[Enrollment No_]) as 'College' from [TMU$Cons_ Marksheet Ledger] ML where   [Enrollment No_]='" + Session["enroll"].ToString() + "' and Status=1");
            {
                SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0 && dt.Rows[0]["College"].ToString()!="TMMC")
                {
                    StudentNoDues.Visible = true;
                }
                else
                {
                    StudentNoDues.Visible = false;
                }

            }
        }

        if ((Session["CourseCode"].ToString() == "BCA-001" || Session["CourseCode"].ToString() == "BCA-002" || Session["CourseCode"].ToString() == "BCA-004" || Session["CourseCode"].ToString() == "BSC-001" || Session["CourseCode"].ToString() == "BBA-001" || Session["CourseCode"].ToString() == "BCOM-001" || Session["CourseCode"].ToString() == "BPES-001") && Session["Semester"].ToString() == "VI")
        {
            NEPUndertaking.Visible = true;
        }
    }
    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        if (Session["Passw"].ToString() == txtOldPassword.Text)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("Update [TMU$Student - COLLEGE] set Password='" + txtNewPassword.Text + "' where No_='" + Session["uid"].ToString() + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Change Successfully');", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Old Password in not correct');", true);
        }
    }
    protected void lnkLogOut_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("~/Default.aspx");
    }
    public void BINDimage()
    {
        string id = Session["uid"].ToString();

        byte[] bytes = GetData("select  top 1 [Student Image]  from [Portal Users] where [Login ID]=(select [Enrollment No_] from [TMU$Student - COLLEGE] where No_='" + id + "')").Rows[0]["Student Image"].ToString() == "" ? null : (byte[])GetData("select  top 1 [Student Image]  from [Portal Users] where [Login ID]=(select [Enrollment No_] from [TMU$Student - COLLEGE] where No_='" + id + "')").Rows[0]["Student Image"];
        if (bytes != null)
        {
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            imgProfile.ImageUrl = "data:image/png;base64," + base64String;

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
    protected void btnChangeImage_Click(object sender, EventArgs e)
    {
       if (imgUpload.HasFile)
        {
            try
            {
                string filename = Path.GetFileName(imgUpload.PostedFile.FileName);
                string contentType = imgUpload.PostedFile.ContentType;
                using (Stream fs = imgUpload.PostedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        System.Drawing.Image img = System.Drawing.Image.FromStream(imgUpload.PostedFile.InputStream);
                        int height = img.Height;
                        int width = img.Width;
                        decimal size = Math.Round(((decimal)imgUpload.PostedFile.ContentLength / (decimal)1024), 2);
                        int fs1 = 0;
                        fs1 = Convert.ToInt32(size);
                        if (fs1 > 50)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Image Size Could Not be Greater than 50 KB !');", true);

                            return;
                        }
                        if (fs1 < 40)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Image Size Could Not be Less than 40 KB !');", true);

                            return;
                        }

                        using (SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
                        {
                            string query = "update [Portal Users] set [Student Image]=@Data where [Login ID]=(select [Enrollment No_] from [TMU$Student - COLLEGE] where No_='" + Session["uid"].ToString() + "')";
                            using (SqlCommand cmd1 = new SqlCommand(query))
                            {
                                cmd1.Connection = con1;
                                cmd1.Parameters.AddWithValue("@Data", bytes);
                                con1.Open();
                                cmd1.ExecuteNonQuery();
                                con1.Close();
                            }
                        }
                    }
                }
                BINDimage();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Default.aspx");
            }
        }
    }
    //protected void lblPayTM_Click(object sender, EventArgs e)
    //{

    //    int temp = 0;
    //    string orderid = "TMUSPORT" + DateTime.Now.Ticks.ToString();


    //    con.Open();
    //    SqlCommand cmd = new SqlCommand("INSERT INTO [OnlinePaymentLog]([Payment Date],[Amount],[Status],[UserID],[GATEWAY],[GatewayStatus],[OrderID],[CLE Entry No],[Semester FEE],[Year FEE],[Temp 1],[Temp 2],[Temp 3],[Orgnized Date])VALUES(GETUTCDATE(),1 ,0 ,'" + Session["uid"].ToString() + "' ,'PAYTM'  ,0 ,'" + orderid + "','',(select Semester from [TMU$Student - COLLEGE] where [No_]='" + Session["uid"].ToString() + "'),(select Year from [TMU$Student - COLLEGE] where [No_]='" + Session["uid"].ToString() + "'),'',0,'', DATEADD(Minute,330,Getdate()))", con);
    //    temp = cmd.ExecuteNonQuery();
    //    con.Close();
    //    if (temp == 1)
    //    {




    //        Dictionary<string, string> parameters = new Dictionary<string, string>();



    //        //-------->Test
    //        String merchantKey = "7v_qN#jfvvCiLSOB";
    //        // Dictionary<string, string> parameters = new Dictionary<string, string>();
    //        parameters.Add("MID", "Teerth64420690832928");
    //        parameters.Add("CHANNEL_ID", "WEB");
    //        parameters.Add("INDUSTRY_TYPE_ID", "Education");
    //        parameters.Add("WEBSITE", "DEFAULT");
    //        parameters.Add("EMAIL", "");
    //        parameters.Add("MOBILE_NO", "");
    //        //parameters.Add("CUST_ID", Session["uid"].ToString().Replace("/", "").ToString());
    //        parameters.Add("CUST_ID", Session["enroll"].ToString());
    //        parameters.Add("ORDER_ID", orderid);
    //        parameters.Add("TXN_AMOUNT", "1");
    //        //parameters.Add("EXTENDINFO", Session["uid"].ToString());
    //        // parameters.Add("mercUnqRef", Session["enroll"].ToString());

    //       // parameters.Add("CALLBACK_URL", "http://172.0.1.105:82/Student/PaYTMSports.aspx");//http://14.139.238.130:82/
    //        parameters.Add("CALLBACK_URL", "http://localhost:1049/TMUiZone/Student/PaYTMSports.aspx");
    //        string checksum = CheckSum.generateCheckSum(merchantKey, parameters);
    //        string paytmURL = "https://securegw.paytm.in/order/process";
    //        //string paytmURL = "https://securegw-stage.paytm.in/theia/processTransaction?orderid=" + orderid;
    //        Session["uid"] = null;

    //        string outputHTML = "<html>";






    //        outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
    //        outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
    //        outputHTML += "<table border='1'>";
    //        outputHTML += "<tbody>";
    //        foreach (string key in parameters.Keys)
    //        {
    //            outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
    //        }
    //        outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
    //        outputHTML += "</tbody>";
    //        outputHTML += "</table>";
    //        outputHTML += "<script type='text/javascript'>";
    //        outputHTML += "document.f1.submit();";
    //        outputHTML += "</script>";
    //        outputHTML += "</form>";
    //        outputHTML += "</script>";
    //        outputHTML += "</body>";
    //        outputHTML += "</html>";
    //        Response.Write(outputHTML);
    //    }
    //}

}




