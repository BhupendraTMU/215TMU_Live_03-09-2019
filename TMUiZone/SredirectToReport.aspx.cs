using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Net.Mail;
using System.Net;

public partial class SredirectToReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Session["MStudentId"] = Request["search"];
        try
        {
            string UserGroup;
            UserGroup=Session["UserGroup"].ToString();
            if (UserGroup == "FACULTY" ||  UserGroup == "PRINCIPAL")
            {
                Response.Redirect("~/StudentReport.aspx");
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        catch
        {
           // Response.Redirect("~/Default.aspx");
        }
    }
  

}