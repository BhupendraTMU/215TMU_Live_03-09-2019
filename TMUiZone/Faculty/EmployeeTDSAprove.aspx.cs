using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_EmployeeTDSAprove : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                getData();
                //BindGridview();
            }
            catch (Exception ex)
            {
                Response.Redirect("../Default.aspx");
            }
        }
    }
    public void getData()
    {
        using (SqlCommand cmd = new SqlCommand(@"SELECT [ID],[EmployeeNo],[EmployeeName],[Year],[Month],[OldAmount],[NewAmount],[Status],[CreatedBy],[CreatedAt],[UpdatedBy],[UpdatedAt],from_date,	to_date	,remark FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$EmployeeUpdateTDSInfo] where EmployeeNo NOT LIKE '%\_' ESCAPE '\'  order by Status", con))
        {
            cmd.CommandType = CommandType.Text;

            if (con.State == ConnectionState.Closed)
                con.Open();

            SqlDataAdapter daCL = new SqlDataAdapter(cmd);
            DataTable dtCL = new DataTable();
            daCL.Fill(dtCL);
            dtCL.Columns.Add("StatusText", typeof(string));

            foreach (DataRow row in dtCL.Rows)
            {
                int status = Convert.ToInt32(row["Status"]);
                switch (status)
                {
                    case 0:
                        row["StatusText"] = "Pending";
                        break;
                    case 1:
                        row["StatusText"] = "Accept";
                        break;
                    case 2:
                        row["StatusText"] = "Reject";
                        break;
                    default:
                        row["StatusText"] = "Unknown";
                        break;
                }
            }
            grdmemberapprovallist.DataSource = dtCL;
            grdmemberapprovallist.DataBind();
        }
    }

    public void updateEmployeeDetail(string employeeNo, int statusVal, string fromdate, string todate)
    {
        try
        {
            using (SqlCommand cmd = new SqlCommand("sp_UpdateTDSDetail", con)) // replace with your stored procedure name
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeNo", employeeNo);
                cmd.Parameters.AddWithValue("@StatusVal", statusVal);
                cmd.Parameters.AddWithValue("@fromdate", fromdate);
                cmd.Parameters.AddWithValue("@todate", todate);

                if (con.State == ConnectionState.Closed)
                    con.Open();

                int rows = 1;
                cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Success alert
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Record updated successfully'); document.location.href='EmployeeTDSAprove.aspx';", true);
                }
                else
                {
                    // If no row updated
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                        "alert('No Record Updated');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                        "alert('Something went wrong.');", true);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }
    protected void grdmemberapprovallist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ActionCommand")
        {
            string employeeNo = e.CommandArgument.ToString();
            Button btn = e.CommandSource as Button;
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            HiddenField EmployeeNo = (grdmemberapprovallist.Rows[rowIndex].Cells[0].FindControl("Hfemployeecode") as HiddenField);
            Label from_date = (grdmemberapprovallist.Rows[rowIndex].Cells[2].FindControl("lblfrom_date") as Label);
            Label to_date = (grdmemberapprovallist.Rows[rowIndex].Cells[3].FindControl("lblTp_date") as Label);

            if (btn != null)
            {
                if (btn.Text == "Accept")
                {
                    updateEmployeeDetail(EmployeeNo.Value, 1, from_date.Text, to_date.Text);
                }
                else if (btn.Text == "Reject")
                {
                    updateEmployeeDetail(EmployeeNo.Value, 2, from_date.Text, to_date.Text);
                }
            }
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        ExportToExcel();
    }

    private void ExportToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition",
            "attachment;filename=EmployeeTDSApproval.xls");

        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            // Disable Paging
            grdmemberapprovallist.AllowPaging = false;
            getData();

            // Hide Action Column (Last Column)
            grdmemberapprovallist.Columns[9].Visible = false;

            grdmemberapprovallist.RenderControl(hw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    // Required for Export
    

}