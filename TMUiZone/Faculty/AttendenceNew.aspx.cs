using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;


public partial class AttendenceNew : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {          
        try
        {
            if (Session["uname"].ToString() == null )
            {
                Response.Redirect("../Default.aspx");
            }
            else
            {                 
                if (!IsPostBack)
                {
                    BindYear();               
                }
               
                // show_Attendence();
            }
        }
        catch (Exception)
        {
            Response.Redirect("../Default.aspx");
        }
    }
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        show_Attendence();
    }
    public void BindYear()
    {

        int Currentyear = System.DateTime.Now.Year;

        for (int i = Currentyear - 1; i <= Currentyear; i++)
            ddlYear.Items.Add(i.ToString());
    }  
    public void show_Attendence()
    {
        SqlCommand cmd = new SqlCommand("[SP_AttendancePunchmain]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@UserID", Session["uid"].ToString());
        cmd.Parameters.Add("@Month", ddlMonth.SelectedValue);        
        cmd.Parameters.Add("@Year", ddlYear.SelectedValue);        
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        grdAttendance.DataSource = dt;
        grdAttendance.DataBind();
        con.Close();
    }
    protected void grdAttendance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAttendance.PageIndex = e.NewPageIndex;
        show_Attendence();        
    }


    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void grdViewdetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}