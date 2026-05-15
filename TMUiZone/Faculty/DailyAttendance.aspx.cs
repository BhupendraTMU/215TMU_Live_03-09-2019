using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class DailyAttendance : System.Web.UI.Page
{
    TMUConnection con; string CollegeCode="";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {            
            CollegeCode = Session["GlobalDimension1Code"].ToString();
            if (!IsPostBack)
            {            
            cleDate.SelectedDate = DateTime.Today.AddDays(-18);           
            }
            GetAttendanceList();
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Default.aspx");
        }
        
    }
    public void GetAttendanceList()
    {
        con = new TMUConnection();
        SqlCommand cmd = new SqlCommand("SP_GetDailyAttendanceReport_CollegeWise", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"]);
        cmd.Parameters.Add("@Date", txtDateOfAttendance.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);       
        con.DisConnect();          
            grdAttendance.DataSource = dt;
            grdAttendance.DataBind();
        
        
    }
    protected void txtDateOfAttendance_TextChanged(object sender, EventArgs e)
    {
      // GetAttendanceList();
    }
    protected void grdAttendance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAttendance.PageIndex = e.NewPageIndex;
        GetAttendanceList();
    }
    protected void grdFollowUp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }
    protected void grdAttendance_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }
   
   

    //public override void VerifyRenderingInServerForm(Control control)
    //{

    //}

    protected void Button1_Click(object sender, EventArgs e)
    {
        GetAttendanceList();
    }
}