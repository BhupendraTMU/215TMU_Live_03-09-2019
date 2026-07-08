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


public partial class IndexMaster : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    Connection Portalcon;
    string HRHODsame = "";
    string forHRisHOD = "";
    SqlCommand com;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            con.Open();

            com = new SqlCommand("select [Custom System Indicator Text] from [TMU$Company Information]", con);
            SqlDataReader reader = com.ExecuteReader();
            reader.Read();
            ind.Text = reader["Custom System Indicator Text"].ToString();
            reader.Close();
            con.Close();


            lblName.Text = Session["Name"].ToString();
            BINDimage();
            GetLinkData();
            hidepageschoal();
            hideMigrationCertificate();
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }  
    }

    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        if (Session["Passw"].ToString() == txtOldPassword.Text)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("Update [TMU$Student - COLLEGE] set Password='" + txtNewPassword.Text + "' where No_='"+Session["uid"].ToString()+"'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Change Successfully');", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Old Password in not correct');", true);
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
                if (dt.Rows.Count > 0 && dt.Rows[0]["College"].ToString() != "TMMC")
                {
                    StudentNoDues.Visible = true;

                }
                else
                {
                    StudentNoDues.Visible = false;                    
                }

                if (Session["College"].ToString() == "TPHD")
                {
                    liIdStudentNoDuesPhd.Visible = true;
                    IdCRACMeetingPHDStudent.Visible = true;
                }
                else
                {
                    string gdg = Session["College"].ToString();
                    liIdStudentNoDuesPhd.Visible = false;
                    IdCRACMeetingPHDStudent.Visible = false;
                }


            }
        }
    }
    public void hideMigrationCertificate()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        {
            
            string strSQL = ("select top 1 Year([Publish Date]) as 'YEAR' from[TMU$Posted Student Ext_Int Line] with(NOLOCK) where[Enrollement No_] = '" + Session["enroll"].ToString() + "' order by[Publish Date] desc");
            {
                SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0]["YEAR"]) >= 2024)
                    {
                        Assessment.Visible = true;

                    }
                  
                    else
                    {
                        Assessment.Visible = false;
                    }
                }
                else if (Session["College"].ToString() == "TMMC" || Session["College"].ToString() == "TMDC")
                {
                    Assessment.Visible = true;
                }
                else
                {
                    Assessment.Visible = false;
                }

            }
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

        byte[] bytes = GetData("select [Student Image]  from [TMU$Student - COLLEGE] where [No_]='" + id + "'").Rows[0]["Student Image"].ToString() == "" ? null : (byte[])GetData("select [Student Image]  from [TMU$Student - COLLEGE] where [No_]='" + id + "'").Rows[0]["Student Image"];
        if (bytes != null)
        {
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            imgProfile.ImageUrl = "data:image/png;base64," + base64String;

        }
    }

    public void GetLinkData()
    {


        con.Open();
        SqlCommand cmd = new SqlCommand("GetAlumanilink", con); 
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SNo", Session["uid"].ToString());        
        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();     
        da.Fill(dt);
        con.Close();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["PageId"].ToString() == "Registration") { Registration.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "EventList") { EventList.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "RegistraAlumniFeedback") { RegistraAlumniFeedback.Visible = true; }
            //if (dt.Rows[i]["PageId"].ToString() == "Assessment") { Assessment.Visible = true; }
            
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
                            string query = "update [TMU$Student - COLLEGE] set [Student Image]=@Data where No_='" + Session["uid"].ToString() + "'";
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
}




