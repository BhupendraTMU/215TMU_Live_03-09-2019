using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class Faculty_CoStatus : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal con;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["uid"].ToString() == "TMU00049")
        {

            Portalcon = new Connection();
            con = new ServicePoratal();
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }



    }

    public void LeaveData()
    {
        SqlCommand sqlCmd = new SqlCommand("proc_GetCoStatus", con.Con);
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.AddWithValue("@EmployeeNo", txtEmployee.Text.TrimEnd());
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataTable dt = new DataTable();
        sqlDa.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            grdData.DataSource = dt;
            grdData.DataBind();
          
        }
        else
        {
           
        }
    }
    protected void btnSeasrch_Click(object sender, EventArgs e)
    {
        LeaveData();
    }
}