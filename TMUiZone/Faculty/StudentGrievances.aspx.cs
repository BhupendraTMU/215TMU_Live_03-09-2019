using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Student_StudentGrievances : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Bindgrid();
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }
    }
    protected void grdActionTakenReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdActionTakenReport.PageIndex = e.NewPageIndex;
        Bindgrid();
    }
    public void Bindgrid()
    {
        SqlCommand cmd = new SqlCommand("select [Date Commited],[Staff Code],[Action Taken],[Fine Amount] from [TMU$Student Discipline Line] where [Student No_]='" + Session["uid"].ToString() + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        grdActionTakenReport.DataSource = dt;
        grdActionTakenReport.DataBind();
    }
}