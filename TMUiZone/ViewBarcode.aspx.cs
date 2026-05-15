using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewBarcode : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["Code"] != null && Request.QueryString["Code"] != string.Empty)
                    Session["Code"] = Request.QueryString["Code"];
                bindetails(Session["Code"].ToString());
            }
            catch (Exception ex)
            {
            }
        }
    }
    public void bindetails(string Code)
    {
        SqlCommand cmd = new SqlCommand("[sp_GetDetailsByCode]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cmd.Parameters.Add("@Code", Code);
       

        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtName.Text = dt.Rows[0]["Name"].ToString();
            txtOutSource.Text = dt.Rows[0]["Outsourse Agency"].ToString();
            txtCategory.Text = dt.Rows[0]["Category"].ToString();
            txtPatch.Text = dt.Rows[0]["Date"].ToString();
            txtQTY.Text = dt.Rows[0]["Quantity"].ToString();
            txtCode.Text = Code;
            if (dt.Rows[0]["Date"].ToString() == "01-01-1900 00:00:00")
            {
                txtPatch.Text = "";
            }
            if (dt.Rows[0]["Quantity"].ToString() == "0.00")
            {
                txtQTY.Text = "";
            }
            if (dt.Rows[0]["Locked"].ToString() == "1")
            {
                btnSubmit.Visible = false;
                txtQTY.Enabled = false;
            }
            else
            {
                btnSubmit.Visible = true;
                txtQTY.Enabled = true;
            }

        }

      
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        using (SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {
            string query = "  update [TMU$Ministry] set [Patch Date]=Convert(date,getdate()),Quantity="+txtQTY.Text+",Locked=1 where Code='" + Session["Code"].ToString() + "'";
            using (SqlCommand cmd1 = new SqlCommand(query))
            {
                cmd1.Connection = con1;
                con1.Open();
                cmd1.ExecuteNonQuery();
                con1.Close();
            }
            bindetails(Session["Code"].ToString());
        }

    }
}