using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


public partial class Faculty_summeryOfJoinAndLeave : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
   // string constr1 = ConfigurationSettings.AppSettings["strPortal"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
       // todate.AutoCompleteType = AutoCompleteType.Disabled;
       // todate.Attributes.Add("autocomplete", "off");
    }

    public void bindsumery(DateTime d1,DateTime d2,string str)
    {
        using (SqlConnection con = new SqlConnection(constr))
        {


            SqlCommand cmd = new SqlCommand("SP_ExitFormSummaryCount", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FromDate", d1);
            cmd.Parameters.AddWithValue("@ToDate", d2);
            cmd.Parameters.AddWithValue("@TeachNonTech", str);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            sumeerygrid.DataSource = dt;
            sumeerygrid.DataBind();
        }
    }


    public DateTime FirstDayOfMonth(DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, 1);
    }
    public DateTime LastDayOfMonth(DateTime dateTime)
    {
        DateTime firstDayOfTheMonth = new DateTime(dateTime.Year, dateTime.Month, 1);
        return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DateTime dt=Convert.ToDateTime(frmdate.Text);
       DateTime dt1= FirstDayOfMonth(dt);
       firstdate.Value = dt1.ToString("dd-MMM-yyyy");
        //last date

       DateTime dtt = Convert.ToDateTime(todate.Text);
       DateTime dtt2 = LastDayOfMonth(dtt);
       lastdate.Value = dtt2.ToString("dd-MMM-yyyy");
       bindsumery( Convert.ToDateTime(firstdate.Value),Convert.ToDateTime(lastdate.Value),drptechnontech.SelectedValue);
    }
}