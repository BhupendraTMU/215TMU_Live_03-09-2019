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


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //  lblName.Text = Session["Name"].ToString();
        }
        catch
        {
            // Response.Redirect("~/Default.aspx");
        }
    }
    
    protected void ImageButton_Click(object sender, ImageClickEventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("~/Default.aspx");
    }
}




