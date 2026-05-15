using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Drawing;
using System.Web.SessionState;

/// <summary>
/// Summary description for ConnectionAttend
/// </summary>
public class ConnectionAttend
{
	 SqlConnection Conn = null;
    SqlCommand cmd;


    public ConnectionAttend()
    {
             

        //string str = val.ToString();
        Conn = new SqlConnection(ConfigurationSettings.AppSettings["strPunchingAttd"].ToString());
       
    }

    public SqlConnection Con
    {
        get
        {
            return Conn;
        }
    }

    public void Connect()
    {

        if (Conn.State == ConnectionState.Closed)
            Conn.Open();
    }
    public void DisConnect()
    {
        Conn.Close();
    }


    public SqlDataReader Show_CardNumber(string cardNo)
    {
        Connect();

        string s = "select * from AttendanceMaster where x_badge_number='" + cardNo + "'";
        cmd = new SqlCommand(s, Conn);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

}