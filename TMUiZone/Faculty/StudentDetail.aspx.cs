using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Faculty_StudentDetail : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        bindetails("");
    }

    public void bindetails(string Code)
    {
        SqlCommand cmd = new SqlCommand("[sp_GetStudentByCode]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cmd.Parameters.Add("@Code", Code);


        DataTable dt = new DataTable();
        da.Fill(dt);
        grdTimetable.DataSource = dt;
        grdTimetable.DataBind();
    }
    public string GetImage(object img)
    {
        return "data:image/jpg;base64," + Convert.ToBase64String((byte[])img);
    }
}