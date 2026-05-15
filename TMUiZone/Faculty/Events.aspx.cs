using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Student_Events : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        bindGrid();
      //  bindLabel();
    }
    protected void grdEvents_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdEvents.PageIndex = e.NewPageIndex;
        bindGrid();
    }
    public void bindGrid()
    {
        con.Open();
        
        //SqlCommand cmd = new SqlCommand("select replace(convert(NVARCHAR, Date, 106), ' ', '-') as Date1,Events,Date from TMU$Events order by Date desc", con);
        SqlCommand cmd = new SqlCommand("select distinct replace(convert(NVARCHAR, Date, 106), ' ', '-') as Date1,Event,E.Code,case E.College when '' then 'University' else 'College' end as Campus from TMU$Events E inner join [TMU$Student - COLLEGE] C on E.College= '"+Session["GlobalDimension1Code"].ToString()+"' or E.University='1' where  [Date] >getdate()   order by Code Asc  ", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        grdEvents.DataSource = dt;
        grdEvents.DataBind();
        //=-----------------------Ashu--------------
        if (dt.Rows.Count > 0)
        {
            DateTime dt1 = Convert.ToDateTime(dt.Rows[0]["Date1"]);
            lblEvent.Text = dt.Rows[0]["Event"].ToString() + " ( " + dt1.ToString("dd MMM yyyy")+" )";
        }
       
        //------------------------Ashu--------------
    }
    //public void bindLabel()
    //{
    //    con.Open();
    //    SqlCommand cmd = new SqlCommand("select top 1 Date,Events  from Events order by Date desc", con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    con.Close();
    //    da.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        DateTime dt1 = Convert.ToDateTime(dt.Rows[0]["Date"]);
    //        lblEvent.Text = dt.Rows[0]["Events"].ToString() + " has organized on " + dt1.ToString("dd MMM yyyy");
    //    }
       
    //}
}