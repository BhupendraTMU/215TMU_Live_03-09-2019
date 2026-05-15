using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class Master : System.Web.UI.MasterPage
{
    TMUConnection con;
    string Companyname = "[Ashoka University$Student - COLLEGE]";

    protected void Page_Load(object sender, EventArgs e)
    {
        trProfile.Visible = false;
        try
        {
            con = new TMUConnection();
            ProfileName.InnerText = "";// Session["Name"].ToString();
            Currentdatetime.InnerText = System.DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");
           // getimage();
        }
        catch(Exception)
        {
            Response.Redirect("Default.aspx");
        }
    }

    private void getimage()
    {
        try
        {
            string sQuery = "select Photo as Photo  from " + Companyname + " where [No_]='" + Session["uid"].ToString() + "'";
            SqlDataAdapter da = new SqlDataAdapter(sQuery, con.Con);
            DataSet ds = new DataSet();
            da.Fill(ds, "tbl_Registration");
            imgProfile.ImageUrl = "data:image/bmp;base64," + Convert.ToBase64String((byte[])ds.Tables[0].Rows[0]["Photo"]);
        }
        catch (Exception)
        {
            imgProfile.ImageUrl = "images/index.jpg";
        }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Session.Abandon();
        Response.Redirect("Default.aspx");
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
    }

    public void UpdateProfilePhoto()
    {
        if (FileUpload1.HasFile)
        {
            int length = FileUpload1.PostedFile.ContentLength;
            byte[] pic = new byte[length];


            FileUpload1.PostedFile.InputStream.Read(pic, 0, length);

            SqlCommand com = new SqlCommand("update " + Companyname + " set [Photo]=@Photo where [No_]='" + Session["uid"].ToString() + "'", con.Con);
            con.Connect();
            com.Parameters.AddWithValue("@Photo", pic);

            com.ExecuteNonQuery();
            con.DisConnect();
        }
    }
    protected void btnSavephoto_Click(object sender, EventArgs e)
    {
        UpdateProfilePhoto();
        getimage();
        Panel1.Visible = false;
    }
}
