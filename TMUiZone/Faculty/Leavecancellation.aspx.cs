using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class Faculty_Leavecancellation : System.Web.UI.Page
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
        SqlCommand sqlCmd = new SqlCommand("sp_GetEmployeeLeave", con.Con);
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.AddWithValue("@EmployeeNo", txtEmployee.Text.TrimEnd());
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataTable dt = new DataTable();
        sqlDa.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            grdData.DataSource = dt;
            grdData.DataBind();
            btnSubmit.Visible = true;
        }
        else
        {
            btnSubmit.Visible = false;
        }
    }
    protected void btnSeasrch_Click(object sender, EventArgs e)
    {
        LeaveData();        
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string profilechangedate = "";
        foreach (GridViewRow row in grdData.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {

                CheckBox chkRow = (row.Cells[0].FindControl("chkSelect") as CheckBox);
                Label lblProfilechangedate1 = (row.Cells[0].FindControl("lblId") as Label);
               

                if (chkRow.Checked == true)
                {

                    profilechangedate = lblProfilechangedate1.Text;

                    using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
                    {

                        SqlCommand cmd = new SqlCommand("SP_LeaveCancelled", con2);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@LeaveId", lblProfilechangedate1.Text);
                        cmd.Parameters.Add("@UserId", Session["uid"].ToString());     
                        if (con2.State == ConnectionState.Closed)
                            con2.Open();
                       cmd.ExecuteNonQuery();
                        con2.Close();

                    }
                }

            }
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Leave Cancelled Successfully.');", true);

        LeaveData();        
    }
}