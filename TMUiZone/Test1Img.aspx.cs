using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Test1Img : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        string Id = "ENQ/16-17/0001";
        //Id = "ASG-0000001224";
        Image12.ImageUrl = "image.aspx?ImageID='" + Id + "'";
       // Image12.ImageUrl = "image.aspx?ImageID='" + Session["Gl_UserId"].ToString() + "'";
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {   if (fileuploadImage.HasFile == true)
        {
            int length = fileuploadImage.PostedFile.ContentLength;
            byte[] imgbyte = new byte[length];
            HttpPostedFile img = fileuploadImage.PostedFile;
            img.InputStream.Read(imgbyte, 0, length);
            SaveImage(imgbyte, "ENQ/16-17/0002");
            
        }

        
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {

    }
    public void SaveImage(Byte[] imagedata ,string Id)
    {
       DataSet  ds=new DataSet();
       SqlCommand cmd = new SqlCommand("dbo.[Proc_UploadImageEnquiry]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 999000000;
            cmd.Parameters.AddWithValue("@imagedata", imagedata);            
            cmd.Parameters.AddWithValue("@No", Id);
            con.Open();
            cmd.ExecuteNonQuery();
    }
}