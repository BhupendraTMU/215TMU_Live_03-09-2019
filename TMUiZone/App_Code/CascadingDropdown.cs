using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using AjaxControlToolkit;

/// <summary>
/// Summary description for CascadingDropdown
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
 [System.Web.Script.Services.ScriptService]
//[System.Web.Script.Services.ScriptService()] 
public class CascadingDropdown : System.Web.Services.WebService
{
    private static string strconnection = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
    SqlConnection concountry = new SqlConnection(strconnection);
    public CascadingDropdown () {

        
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }
    [WebMethod]
    public CascadingDropDownNameValue[] BindStateDetails(string knownCategoryValues, string category)
    {
        concountry.Open();
        SqlCommand cmdcountry = new SqlCommand("select Code as No_,[Description]+' '++' - '+ [Code]  as Details from [TMU$State]", concountry);
        cmdcountry.ExecuteNonQuery();
        SqlDataAdapter dacountry = new SqlDataAdapter(cmdcountry);
        DataSet dscountry = new DataSet();
        dacountry.Fill(dscountry);
        concountry.Close();
        //create list and add items in it by looping through dataset table
        List<CascadingDropDownNameValue> countrydetails = new List<CascadingDropDownNameValue>();
        foreach (DataRow dtrow in dscountry.Tables[0].Rows)
        {
            string No_ = dtrow["No_"].ToString();
            string Details = dtrow["Details"].ToString();
            countrydetails.Add(new CascadingDropDownNameValue(Details, No_));
        }
        return countrydetails.ToArray();
    }
    /// <summary>
    /// WebMethod to Populate State Dropdown
    /// </summary>
    [WebMethod]
    public CascadingDropDownNameValue[] BindDistrictDetails(string knownCategoryValues, string category)
    {
        string countryID;


        //This method will return a StringDictionary containing the name/value pairs of the currently selected values
        StringDictionary countrydetails = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        countryID =countrydetails["No_"];
        concountry.Open();
        SqlCommand cmdstate = new SqlCommand("select distinct upper(District) as No_, upper(District)   as Details from [TMU$Post Code]  where [State]=@CountryID", concountry);
        cmdstate.Parameters.AddWithValue("@CountryID", countryID);
        cmdstate.ExecuteNonQuery();
        SqlDataAdapter dastate = new SqlDataAdapter(cmdstate);
        DataSet dsstate = new DataSet();
        dastate.Fill(dsstate);
        concountry.Close();
        //create list and add items in it by looping through dataset table
        List<CascadingDropDownNameValue> statedetails = new List<CascadingDropDownNameValue>();
        foreach (DataRow dtrow in dsstate.Tables[0].Rows)
        {
            string StateID = dtrow["No_"].ToString();
            string StateName = dtrow["Details"].ToString();
            statedetails.Add(new CascadingDropDownNameValue(StateName, StateID));
        }
        return statedetails.ToArray();
    }
    /// <summary>
    /// WebMethod to Populate Region Dropdown
    /// </summary>
    [WebMethod]
    public CascadingDropDownNameValue[] BindRegionDetails(string knownCategoryValues, string category)
    {
        int stateID;
        //This method will return a StringDictionary containing the name/value pairs of the currently selected values
        StringDictionary statedetails = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        stateID = Convert.ToInt32(statedetails["State"]);
        concountry.Open();
        SqlCommand cmdregion = new SqlCommand("select * from RegionTable where StateID=@StateID", concountry);
        cmdregion.Parameters.AddWithValue("@StateID", stateID);
        cmdregion.ExecuteNonQuery();
        SqlDataAdapter daregion = new SqlDataAdapter(cmdregion);
        DataSet dsregion = new DataSet();
        daregion.Fill(dsregion);
        concountry.Close();
        //create list and add items in it by looping through dataset table
        List<CascadingDropDownNameValue> regiondetails = new List<CascadingDropDownNameValue>();
        foreach (DataRow dtrow in dsregion.Tables[0].Rows)
        {
            string RegionID = dtrow["RegionID"].ToString();
            string RegionName = dtrow["RegionName"].ToString();
            regiondetails.Add(new CascadingDropDownNameValue(RegionName, RegionID));
        }
        return regiondetails.ToArray();
    }
}
