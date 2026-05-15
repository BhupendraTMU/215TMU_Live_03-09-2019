using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DL;
using System.IO;

public partial class Faculty_MasterPageChanges : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string ChangePassword(string txtOldPassword, string txtNewPassword)
    {
        if (HttpContext.Current.Session["Passw"].ToString() == txtOldPassword)
        {
            string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
            using (SqlConnection con1 = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("update [TMU$Employee] set [Web Portal Password]='" + txtNewPassword + "' where No_='" + HttpContext.Current.Session["uid"].ToString() + "'", con1))
                {
                    if (con1.State == ConnectionState.Closed)
                        con1.Open();
                    cmd.Connection = con1;
                    cmd.ExecuteNonQuery();
                    con1.Close();
                    return "successfully";
                }
            }
        }
        else
        {
            return "Old Password in not correct";
        }
    }

}