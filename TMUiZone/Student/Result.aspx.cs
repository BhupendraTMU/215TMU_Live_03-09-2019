using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_Result : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            
                string FilePath = Server.MapPath(@"Result\" + Session["uid"].ToString().Replace(@"/", "") + ".pdf");
               

                WebClient User = new WebClient();

                Byte[] FileBuffer = User.DownloadData(FilePath);

                if (FileBuffer != null)
                {

                    Response.ContentType = "application/pdf";

                    Response.AddHeader("content-length", FileBuffer.Length.ToString());

                    Response.BinaryWrite(FileBuffer);
                   

                }
           
        }
        catch(Exception ex)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}