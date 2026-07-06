using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_ITEmployeePunchRecord : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["uid"].ToString() == "TMU07001" || Session["uid"].ToString() == "TMU06106" || Session["uid"].ToString() == "TMU07417" || Session["uid"].ToString() == "TMU07473")
            {
                txtDate.Text =
                DateTime.Now.ToString("yyyy-MM-dd");
                BindPunch();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('Access denied.'); document.location.href='FacultyDetails.aspx';", true);

            }
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindPunch();
    }

    private void BindPunch()
    {
        SqlCommand cmd = new SqlCommand(
        "USP_GetEmployeePunchDetails", con);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@EmployeeID",
        Session["uid"].ToString());

        cmd.Parameters.AddWithValue("@PunchDate",
        txtDate.Text);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataTable dt = new DataTable();

        da.Fill(dt);

        gvPunch.DataSource = dt;

        gvPunch.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;

        GridViewRow row =
        (GridViewRow)btn.NamingContainer;

        TextBox txtReason =
        (TextBox)row.FindControl("txtReason");

        HiddenField hdnMachineID =
        (HiddenField)row.FindControl("hdnMachineID");

        string PunchTime =
        btn.CommandArgument;

        SqlCommand cmd = new SqlCommand(
        "USP_UpdatePunchReason", con);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@EmployeeID",
        Session["uid"].ToString());

        cmd.Parameters.AddWithValue("@PunchDate",
        txtDate.Text);

        cmd.Parameters.AddWithValue("@PunchTime",
        Convert.ToDateTime(PunchTime));

        cmd.Parameters.AddWithValue("@MachineID",
        hdnMachineID.Value);

        cmd.Parameters.AddWithValue("@Reason",
        txtReason.Text);

        con.Open();

        cmd.ExecuteNonQuery();

        con.Close();

        BindPunch();
    }


    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=PunchReport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            // Action Column Remove
            gvPunch.Columns[gvPunch.Columns.Count - 1].Visible = false;

            foreach (GridViewRow row in gvPunch.Rows)
            {
                // Reason TextBox -> Label/Text
                TextBox txtReason = (TextBox)row.FindControl("txtReason");

                if (txtReason != null)
                {
                    row.Cells[5].Text = txtReason.Text;
                }
            }

            gvPunch.RenderControl(hw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            // Again Show Action Column
            gvPunch.Columns[gvPunch.Columns.Count - 1].Visible = true;
        }
    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        // Required for Export to Excel
    }
}