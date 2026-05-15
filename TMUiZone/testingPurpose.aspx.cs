using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DL;
using PL;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Web.Mail;
using paytm;
using System.Web.Script.Serialization;

public partial class testingPurpose : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public class Customer
    {
        public string FacultyCode { get; set; }
        public string FacultyName { get; set; }
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public List<Customer> GetDistrict(string No)
    {
        //DataTable dt = new DataTable();
        //CommonDL objCommonDL = new CommonDL();
        //dt = objCommonDL.GetDistrictDdl(No_);
        //List<Customer> District = new List<Customer>();
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    District.Add(dt.Rows[i][1].ToString());
        //}
        //return District;
        string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
        using (SqlConnection con1 = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("select distinct upper(District) as [Faculty Code], upper(District)   as [Faculty Name] from [TMU$Post Code]  where [State]=@StateCode='" + No + "'", con1))
            {
                cmd.Connection = con1;
                List<Customer> customers = new List<Customer>();
                con1.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(new Customer
                        {
                            FacultyCode = sdr["Faculty Code"].ToString(),
                            FacultyName = sdr["Faculty Name"].ToString(),
                        });
                    }
                }
                con1.Close();
                return customers;
            }
        }
    }

}