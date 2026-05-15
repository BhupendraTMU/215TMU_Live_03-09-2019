using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Utility;

namespace DL
{

    public class CommonDL
    {
        string str;
        private SqlConnection gl_con;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        public void New()
        {
            AppSettingsReader objCnf = new AppSettingsReader();
            string strOutput = string.Empty;       //new string("");
            //strOutput =
            objCnf.GetValue("ConnectionString", strOutput.GetType());
            if (string.IsNullOrEmpty(strOutput))
            {
                throw new Exception("Connection string not found in application configuration file.");
            }
            else
            {
                gl_con = new SqlConnection(strOutput);
                gl_con.Open();
            }
        }
        public void New(String ConnectionString)
        {
            gl_con = new SqlConnection(ConnectionString);
            gl_con.Open();
        }
        public SqlConnection Connection
        {
            get { return gl_con; }
        }
        public void addApplicantInformation(string No, string name, string address, string contactNo, string course, string source,
           string category, string religion, int subreligion, string remarks,string Place,string UserID) 
        {
            SqlCommand cmd = new SqlCommand("[dbo].[proc_insertApllicantEnquiry]", con);
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@No", No);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@ContactNo", contactNo);
            cmd.Parameters.AddWithValue("@Course", course);
            cmd.Parameters.AddWithValue("@Source", source);
            cmd.Parameters.AddWithValue("@category", category);
            cmd.Parameters.AddWithValue("@religion", religion);
            cmd.Parameters.AddWithValue("@subreligion", subreligion);
            cmd.Parameters.AddWithValue("@remarks", remarks);
            cmd.Parameters.AddWithValue("@City", Place);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void addRegistrationInformation(string No, DateTime dateofSale, string enquiryNo, string Name, string Course, int category,
             int gender, DateTime DOB, int Age, int months, string fathersName, string mothersNmae, int applicationStatus,
             string citizen, string preQualification, string nameOfPreviousInstitute, string medium, int hostelAcommodation,
            string quota, decimal applicationCost, string paymentMode, DateTime chequeDate, string chequeNo, string bankName)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("[dbo].[proc_insertRegistrationInformation]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@No", No);
            cmd.Parameters.AddWithValue("@dateofSale", dateofSale);
            cmd.Parameters.AddWithValue("@enquiryNo", enquiryNo);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Course", Course);
            cmd.Parameters.AddWithValue("@category", category);
            cmd.Parameters.AddWithValue("@gender", gender);
            cmd.Parameters.AddWithValue("@DOB", DOB);
            cmd.Parameters.AddWithValue("@Age", Age);
            cmd.Parameters.AddWithValue("@months", months);
            cmd.Parameters.AddWithValue("@fathersName", fathersName);
            cmd.Parameters.AddWithValue("@mothersNmae", mothersNmae);
            cmd.Parameters.AddWithValue("@applicationStatus", applicationStatus);
            cmd.Parameters.AddWithValue("@citizen", citizen);
            cmd.Parameters.AddWithValue("@preQualification", preQualification);
            cmd.Parameters.AddWithValue("@nameOfPreviousInstitute", nameOfPreviousInstitute);
            cmd.Parameters.AddWithValue("@medium", medium);
            cmd.Parameters.AddWithValue("@hostelAcommodation", hostelAcommodation);
            cmd.Parameters.AddWithValue("@quota", quota);
            cmd.Parameters.AddWithValue("@applicationCost", applicationCost);
            cmd.Parameters.AddWithValue("@paymentMode", paymentMode);
            cmd.Parameters.AddWithValue("@chequeDate", chequeDate);
            cmd.Parameters.AddWithValue("@chequeNo", chequeNo);
            cmd.Parameters.AddWithValue("@bankName", bankName);
            cmd.ExecuteNonQuery();
            con.Close();
        }


        public DataSet getApplicationCost(string Category, string CourseCode)
        {
            string procName = "proc_ApplicationCost";
            DataUtility objDut = new DataUtility();
            DataSet s = objDut.GetDataSetProc(procName, Category, CourseCode);
            return s;
        }

        public DataTable GetStateDdl()
        {
            string procName = "proc_GetStateDdl";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName);
            return dt;
        }
        public DataTable GetDistrictDdl()
        {
            string procName = "proc_GetDistrictDdl";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName);
            return dt;
        }
        public DataTable GetDistrictDdl(String StateCode)
        {
            string procName = "proc_GetDistrictDdlByState";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName,StateCode);
            return dt;
        }
        public DataTable GetCityDdl()
        {
            string procName = "proc_GetCityDdl";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName);
            return dt;
        }
        public DataTable GetCityDdl(string StateCode,string DistrictCode)
        {
            string procName = "proc_GetCityDdlByStateDistrict";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName,StateCode,DistrictCode);
            return dt;
        }
        public string  addApplicantInformation(string ContactNo, string Course)
           
        {
            SqlParameter outResult = new SqlParameter("@Result", SqlDbType.VarChar, 2500) { Direction = ParameterDirection.Output };              
            SqlCommand cmd = new SqlCommand("[dbo].[proc_DuplicateEnquiry]", con);
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ContactNo", ContactNo);
            cmd.Parameters.AddWithValue("@Course", Course);
            cmd.Parameters.Add(outResult);
            cmd.ExecuteScalar();
            con.Close();
            string Result = outResult.Value.ToString() != null ? outResult.Value.ToString() : "";
            return Result;
           
        }

        public string GetPassword(string UserId, string MobileNo)
        {
            SqlParameter outResult = new SqlParameter("@Result", SqlDbType.VarChar, 2500) { Direction = ParameterDirection.Output };
            SqlCommand cmd = new SqlCommand("[dbo].[proc_GetPassword]", con);
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
            cmd.Parameters.Add(outResult);
            cmd.ExecuteScalar();
            con.Close();
            string Result = outResult.Value.ToString() != null ? outResult.Value.ToString() : "";
            return Result;

        }

        public void RegisterYourInterest(string No, string name, string address, string contactNo, string course, string source,
          string category, string religion, int subreligion, string remarks, string Place, string UserID, string EmailID) //Add ->EmailID-06-06-2016 for EnquiryOnline
        {
            SqlCommand cmd = new SqlCommand("[dbo].[proc_insertRegisterYourInterest]", con);
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@No", No);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@ContactNo", contactNo);
            cmd.Parameters.AddWithValue("@Course", course);
            cmd.Parameters.AddWithValue("@Source", source);
            cmd.Parameters.AddWithValue("@category", category);
            cmd.Parameters.AddWithValue("@religion", religion);
            cmd.Parameters.AddWithValue("@subreligion", subreligion);
            cmd.Parameters.AddWithValue("@remarks", remarks);
            cmd.Parameters.AddWithValue("@City", Place);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@EmailID", EmailID);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}