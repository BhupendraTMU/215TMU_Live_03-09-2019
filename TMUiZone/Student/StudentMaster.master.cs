using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_StudentMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "Hii();", true);
        try
        {
            Session["Name"].ToString();
            ProfileName.InnerText = Session["Name"].ToString();
            Currentdatetime.InnerText = System.DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");
        }
        catch (Exception)
        {
            Response.Redirect("../Default.aspx");
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {

    }
    protected void btnSavephoto_Click(object sender, EventArgs e)
    {

    }
}
