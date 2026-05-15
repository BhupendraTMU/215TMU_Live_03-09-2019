using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Faculty_Hostel : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                getData("NBH-A", DateTime.ParseExact("23-12-2025", "dd-MM-yyyy", null),TimeSpan.Parse("19:00"),TimeSpan.Parse("23:00"));
            }
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    private void getData(string Hostel, DateTime date, TimeSpan fromtime, TimeSpan totime)
    {
        try
        { 
        SqlCommand cmd = new SqlCommand("GetAttendanceNewDevBhupii", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add("@HostelCode", SqlDbType.VarChar).Value = Hostel;
        cmd.Parameters.Add("@AttDate", SqlDbType.Date).Value = date;
        cmd.Parameters.Add("@fromtime", SqlDbType.Time).Value = fromtime;
        cmd.Parameters.Add("@totime", SqlDbType.Time).Value = totime;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();

        con.Open();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
            {
                grdHostel.DataSource = dt;
                grdHostel.DataBind();
            }
            else
            {
                grdHostel.DataSource = "";
                grdHostel.DataBind();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}