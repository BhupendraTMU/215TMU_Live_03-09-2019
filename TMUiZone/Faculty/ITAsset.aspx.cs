using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Web.Services;
using System.Web.Script.Services;

public partial class Faculty_ITAsset : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {

                bindGrid();

            }
            catch (Exception ex)
            {

            }
        }
    }
    public void bindGrid()
    {
        SqlCommand cmd = new SqlCommand("proc_getITAssets", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@UserId", Session["uid"].ToString());

        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);

        grdAssestDetails.DataSource = dtCL;
        grdAssestDetails.DataBind();
    }
    protected void grdAssestDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Receive")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdAssestDetails.Rows[rowIndex];

            string indentNo = ((Label)row.FindControl("lblIndent")).Text;
            string assetNo = ((Label)row.FindControl("lblAsset")).Text;

            TextBox txtRoom = (TextBox)row.FindControl("lblRoom");
            string roomNo = txtRoom.Text;

            SqlCommand cmd = new SqlCommand("UpdateItTransfer", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Indent", indentNo);
            cmd.Parameters.AddWithValue("@Asset", assetNo);
            cmd.Parameters.AddWithValue("@Room", roomNo);
            cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            bindGrid();

            // ✅ Success Alert
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                "alert('Asset received successfully!');", true);
        }
    }


}