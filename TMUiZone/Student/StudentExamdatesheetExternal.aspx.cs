using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Student_StudentExamdatesheetExternal : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getgriddata();
        }
    }
    public void getgriddata()
    {
        SqlCommand cmd = new SqlCommand("Sp_getExamTimesheetStudentExternal", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString().Trim());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Open();
        da.Fill(dt);
        con.Close();
        GrdExamTimeSheet.DataSource = dt;
        GrdExamTimeSheet.DataBind();
    }

}