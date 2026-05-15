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
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string s = this.Page.Request.FilePath;
            con.Open();
            SqlCommand cmd1 = new SqlCommand("select count(*) 'C' from [tbl_StudentUpdatedRecord]  where studentno='" + Session["uid"].ToString() + "'", con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt2 = new DataTable();
            da1.Fill(dt2);
            con.Close();
            if (dt2.Rows[0]["C"].ToString() == "0")
            {
                if (s != "/Application/StudentDetailsView.aspx")
                {
                    Response.Redirect("/Application/StudentDetailsView.aspx", false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();

                }

            }
            lblName.Text = Session["Name"].ToString();
            BINDimage();
            GetLinkData();
            hideUpdate();
            GetSpecializationRule();


        }
        catch
        {
            Response.Redirect("~/Default.aspx");
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
    public void hideUpdate()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("select [Profile Updated] as ProfileUpdated,[Hostel Alloted] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Student - COLLEGE] where No_='" + Session["uid"].ToString() + "'", con))
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string ProfileUpdated = dr["ProfileUpdated"].ToString();

                    string HostelAlloted = dr["Hostel Alloted"].ToString();
                    if(HostelAlloted=="1")
                    {
                        OutPass.Visible = true;
                    }
                    else
                    {
                        OutPass.Visible = false;
                    }
                    if (ProfileUpdated == "3")

                    {
                        Registration.Visible = true;
                        //ScholarshipUploadDocument.Visible = true;
                    }
                    else
                    {
                        Registration.Visible = false;
                        //ScholarshipUploadDocument.Visible = false;
                    }


                }
            }
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
            if (dt.Rows[i]["PageId"].ToString() == "Registration") 
            
            { Registration.Visible = true; }
           
            
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


    public void GetSpecializationRule()
    {


        con.Open();
        SqlCommand cmd = new SqlCommand("GetSpecializationRule", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SNo", Session["uid"].ToString());
        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();

        if (dt.Rows[0]["NEP Based Program"].ToString() == "1")
        {
            CourseSelection.Visible = true;
        }





    }


}




