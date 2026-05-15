using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

using System.IO;


public partial class Dashboard : System.Web.UI.Page
{
    ConnectionAttend conAttend;
    protected void Page_Load(object sender, EventArgs e)
    {
        //conAttend = new ConnectionAttend();

    }
    public void SaveAttendance()
    {
        SqlDataReader dr = conAttend.Show_CardNumber(Session["CardNo"].ToString().Trim());
        dr.Read();
        if (dr.HasRows)
        { 
        
        }


    }

}