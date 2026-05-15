using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class WebService : System.Web.Services.WebService {

    public WebService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }
  //  [System.Web.Script.Services.ScriptMethod()]
   
    public  List<string> SearchItems(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            DataTable dt = new DataTable();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "Sp_SerchItem";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ItemSearch", prefixText.ToUpper());
                cmd.Connection = conn;
                conn.Open();
                List<string> item = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        item.Add(sdr["Description"].ToString());
                    }
                }
                conn.Close();
                return item;
            }
        }
    }
    public class District
    {
        public string No { get; set; }
        public string Description { get; set; }
    }
     [WebMethod]
    public  List<District> BindFacultyCode(string StateCode)
    {
        string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
        using (SqlConnection con1 = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("select distinct upper(District) as [No_], upper(District)   as [Description] from [TMU$Post Code] where [State]='" + StateCode + "'", con1))
            {
                cmd.Connection = con1;
                List<District> District = new List<District>();
                con1.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        District.Add(new District
                        {
                            No = sdr["No_"].ToString(),
                            Description = sdr["Description"].ToString(),
                        });
                    }
                }
                con1.Close();

                return District;
            }
        }
    }

}


