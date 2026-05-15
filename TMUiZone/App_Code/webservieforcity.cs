using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;


/// <summary>
/// Summary description for webservieforcity
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class webservieforcity : System.Web.Services.WebService {

    public webservieforcity () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
 
    public List<string> GetCountries(string prefixText)
    {
        //string  s = Session["locations"].ToString();
        Connection con = new Connection();

        string stable = "B_L Lifescience";
        string rtable = stable.Replace(".", "_");
        string tblNameEmployee = "[" + rtable + "$Post Code" + "]";
        SqlCommand cmd = new SqlCommand("select * from " + tblNameEmployee + " where City like @City+'%'", con.Con);
        cmd.Parameters.AddWithValue("@City", prefixText);
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataTable dt = new DataTable();
        da.Fill(dt);
        List<string> CountryNames = new List<string>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            CountryNames.Add(dt.Rows[i][1].ToString());
        }
        return CountryNames;
    }
}
