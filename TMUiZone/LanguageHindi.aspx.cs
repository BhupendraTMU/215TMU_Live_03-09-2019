using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Reflection;

public partial class LanguageHindi : System.Web.UI.Page
{
    ResourceManager rm;
    CultureInfo ci;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            ci = Thread.CurrentThread.CurrentCulture;
            LoadString(ci);
        }
        else
        {
            rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            ci = Thread.CurrentThread.CurrentCulture;
            LoadString(ci);
        }
    }

    private void LoadString(CultureInfo ci)
    {

        lblabtme.Text = rm.GetString("AboutMe", ci);
        lbldesc.Text = rm.GetString("Desc", ci);
        Button1.Text = rm.GetString("Eng", ci);
        lblheader.Text = rm.GetString("Header", ci);
        Button2.Text = rm.GetString("Hindi", ci);
        Button3.Text = rm.GetString("Marathi", ci);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("hi-IN");
        LoadString(Thread.CurrentThread.CurrentCulture);

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        LoadString(Thread.CurrentThread.CurrentCulture);
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("mr-IN");
        LoadString(Thread.CurrentThread.CurrentCulture);
    }
    protected void BindGrid()
    { 

    
    }
}
