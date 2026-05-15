using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public class AttendanceMessage: Page
{
    
    public static void DoSomething()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        SqlCommand cmd = new SqlCommand("select [Student No_],[Subject Code] from [TMU$Student Attendance Line - COL] group by [Student No_],[Subject Code]", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //SqlCommand cmd1 = new SqlCommand("select * from [TMU$Student Attendance Line - COL] where convert(varchar(11),Date,103) in (convert(varchar(11),getdate(),103),convert(varchar(11),dateadd(day,-1,getdate()),103), " + @"
                //convert(varchar(11),dateadd(day,-2,getdate()),103)) and [Student No_]='" + dt.Rows[i]["Student No_"].ToString() + "' and [Subject Code]='" + dt.Rows[i]["Subject Code"].ToString() + "' and [Attendance Type]='1'", con);

                SqlCommand cmd1 = new SqlCommand("select top 3 * from [TMU$Student Attendance Line - COL] where [Student No_]='" + dt.Rows[i]["Student No_"].ToString() + "' and [Subject Code]='" + dt.Rows[i]["Subject Code"].ToString() + "' order by Date desc", con);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows[0]["Date"].ToString().Remove(11) == System.DateTime.Now.ToString().Remove(11) && dt1.Rows[0]["Attendance Type"].ToString() == "0" && dt1.Rows[1]["Attendance Type"].ToString() == "0" && dt1.Rows[2]["Attendance Type"].ToString() == "0")
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlCommand cmd2 = new SqlCommand("select [Mobile Number],[Student Name] from [TMU$Student - COLLEGE] where No_='" + dt1.Rows[0]["Student No_"] + "'", con);
                    string s = cmd2.ExecuteScalar().ToString();
                    con.Close();
                   // SMS("","");
                }
            }
        }
        
    }

    public static void SMS(String MobileNo, string Msg)
    {
        //  Website: http://www.universalsmsadvertising.com/     //  Username: 9837016352   //  Password: 9837016352 
        // MobileNo = "91" + MobileNo;
        Msg = "Hii";
        MobileNo = "91" + 8882074374;
        string url = "http://www.universalsmsadvertising.com/universalsmsapi.php?user_name=9837016352&user_password=9837016352&mobile=" + MobileNo + "&sender_id=TMUniv&type=F&text=" + Msg + "";
        System.Net.HttpWebRequest fr;
        Uri targetURI = new Uri(url);
        fr = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(targetURI);
        if (fr.GetResponse().ContentLength > 0)
        {
            System.IO.StreamReader str = new System.IO.StreamReader(fr.GetResponse().GetResponseStream());
            //Response.Write(str.ReadToEnd());
            str.Close();
        }
    }
    
	public AttendanceMessage()
	{
		
	}

}