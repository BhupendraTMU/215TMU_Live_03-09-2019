using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class translation : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
       // btnConvert_Click(btnConvert, null);
    }
    protected void btnConvert_Click(object sender, EventArgs e)
    {
        SaveData();
        GetData();
    }
    public void SaveData()
    {
        SqlCommand cmd = new SqlCommand("sp_saveTestEmp", con);
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", TextBox1.Text);                    
                    cmd.ExecuteNonQuery();
                    con.Close();
    }
    public void GetData()
    {
        DataTable dt = new DataTable();
        con.Open();
        SqlCommand cmd = new SqlCommand("sp_GetTestEmp", con);
        cmd.CommandType = CommandType.StoredProcedure;        
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        con.Close();
        da.Fill(dt);
        grd.DataSource = dt;
        grd.DataBind();
    }
    
}