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
public partial class Faculty_RoasterPost : System.Web.UI.Page
{
    Connection con;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                con = new Connection();
                BindYear();
                ddlMonth.SelectedValue = System.DateTime.Now.ToString("MM");
                ddlYear.SelectedValue = System.DateTime.Now.ToString("yyyy");

                BindData();








            }
            catch (Exception ex)
            {
            }
        }
    }
    public void BindData()
    {

        SqlCommand cmd = new SqlCommand("GetRoasterFinal", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        if (con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Month", ddlMonth.SelectedValue);
        cmd.Parameters.AddWithValue("@Year", ddlYear.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();


        da.Fill(dt);
        con1.Close();
        if (dt.Rows.Count > 0)
        {
            btnPost.Visible = true;
            grddata.DataSource = dt;
            grddata.DataBind();
        }
        else
        {
            btnPost.Visible = false;
            grddata.DataSource = "";
            grddata.DataBind();
        }


    }
    public void BindYear()
    {

        int Currentyear = System.DateTime.Now.Year;

        for (int i = Currentyear - 1; i <= Currentyear; i++)
            ddlYear.Items.Add(i.ToString());
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("PostRoasterFinal", con1);
        cmd.CommandType = CommandType.StoredProcedure;
       
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Month", ddlMonth.SelectedValue);
        cmd.Parameters.AddWithValue("@Year", ddlYear.SelectedValue);
        if (con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }
        cmd.ExecuteNonQuery();
        con1.Close();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Roaster Post Successfully');", true);

    }
}