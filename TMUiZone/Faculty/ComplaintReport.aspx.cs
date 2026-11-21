
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_ComplaintReport : System.Web.UI.Page
{
    private string TMUConnectionString
    {
        get
        {
            return ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
        }
    }

    private string HRMSConnectionString
    {
        get
        {
            return ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ConnectionString;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                HostelList();

                if (drpHostel.Items.Count > 0)
                {
                    ComplaintList(drpHostel.SelectedItem.Text);
                }
            }
            catch (Exception)
            {
                Response.Redirect("../Default.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
    }

    private string CurrentUserId
    {
        get
        {
            if (Session["uid"] == null)
                throw new InvalidOperationException("User session has expired.");

            return Session["uid"].ToString();
        }
    }

    public void HostelList()
    {
        using (SqlConnection con = new SqlConnection(TMUConnectionString))
        using (SqlCommand cmd = new SqlCommand("Customer_GetHostelList", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 50).Value = CurrentUserId;

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);

                drpHostel.DataSource = dt;
                drpHostel.DataTextField = "Name";
                drpHostel.DataValueField = "Code";
                drpHostel.DataBind();
            }
        }
    }

    public void ComplaintList(string hostel)
    {
        if (Request.Browser.IsMobileDevice)
        {
            OrderList1.Width = Unit.Pixel(380);
        }

        using (SqlConnection con = new SqlConnection(HRMSConnectionString))
        using (SqlCommand cmd = new SqlCommand("Customer_GetComplaintReport", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 50).Value = CurrentUserId;
            cmd.Parameters.Add("@Hostel", SqlDbType.NVarChar, 100).Value = hostel;

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);

                OrderList1.DataSource = dt;
                OrderList1.DataBind();

                if (OrderList1.HeaderRow != null)
                {
                    OrderList1.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }
    }

    public void bindComplaintDetail(string department, string status, string hostel)
    {
        if (Request.Browser.IsMobileDevice)
        {
            GridView1.Width = Unit.Pixel(380);
        }

        using (SqlConnection con = new SqlConnection(HRMSConnectionString))
        using (SqlCommand cmd = new SqlCommand(
            "Customer_GetComplaintListWithDepartmentStatus", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Department", SqlDbType.NVarChar, 100).Value = department;
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 20).Value = status;
            cmd.Parameters.Add("@Hostel", SqlDbType.NVarChar, 100).Value = hostel;

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();

                if (GridView1.HeaderRow != null)
                {
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        // Refresh the details UpdatePanel and open the Bootstrap popup.
        Panel.Update();
        ShowComplaintPopup();
    }

    private void ShowComplaintPopup()
    {
        ScriptManager.RegisterStartupScript(
            Panel,
            Panel.GetType(),
            "ShowComplaintPopup",
            "showComplaintModal();",
            true
        );
    }

    private string GetDepartmentFromClickedLink(object sender)
    {
        LinkButton link = sender as LinkButton;

        if (link == null)
            return string.Empty;

        GridViewRow row = link.NamingContainer as GridViewRow;

        if (row == null || row.Cells.Count < 2)
            return string.Empty;

        // Department is the second column in OrderList1.
        return HttpUtility.HtmlDecode(row.Cells[1].Text).Trim();
    }

    protected void lnkT_Click(object sender, EventArgs e)
    {
        string department = GetDepartmentFromClickedLink(sender);

        bindComplaintDetail(
            department,
            "9",
            drpHostel.SelectedItem.Text
        );
    }

    protected void lnkPW_Click(object sender, EventArgs e)
    {
        string department = GetDepartmentFromClickedLink(sender);

        bindComplaintDetail(
            department,
            "1",
            drpHostel.SelectedItem.Text
        );
    }

    protected void lnkPD_Click(object sender, EventArgs e)
    {
        string department = GetDepartmentFromClickedLink(sender);

        bindComplaintDetail(
            department,
            "2",
            drpHostel.SelectedItem.Text
        );
    }

    protected void lnkRD_Click(object sender, EventArgs e)
    {
        string department = GetDepartmentFromClickedLink(sender);

        bindComplaintDetail(
            department,
            "3",
            drpHostel.SelectedItem.Text
        );
    }

    protected void lnkResolved_Click(object sender, EventArgs e)
    {
        string department = GetDepartmentFromClickedLink(sender);

        bindComplaintDetail(
            department,
            "4",
            drpHostel.SelectedItem.Text
        );
    }

    protected void lnkClose_Click(object sender, EventArgs e)
    {
        string department = GetDepartmentFromClickedLink(sender);

        bindComplaintDetail(
            department,
            "5",
            drpHostel.SelectedItem.Text
        );
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ComplaintReport.aspx", false);
        Context.ApplicationInstance.CompleteRequest();
    }

    protected void drpHostel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpHostel.SelectedItem != null)
        {
            ComplaintList(drpHostel.SelectedItem.Text);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Required for GridView Excel export.
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "NoDataFound",
                "alert('No complaint details available to export.');",
                true
            );
            return;
        }

        GridView exportGrid = new GridView();
        exportGrid.DataSource = GetGridViewDataForExport();
        exportGrid.DataBind();

        if (exportGrid.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "NoExportRows",
                "alert('No data found for export.');",
                true
            );
            return;
        }

        exportGrid.HeaderStyle.BackColor =
            System.Drawing.Color.LightGray;

        exportGrid.GridLines = GridLines.Both;

        using (StringWriter stringWrite = new StringWriter())
        using (HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite))
        {
            exportGrid.RenderControl(htmlWrite);

            string fileName = "StudentComplaintReport_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Charset = "";

            Response.AddHeader(
                "Content-Disposition",
                "attachment;filename=" + fileName
            );

            Response.Write("<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />");
            Response.Write(stringWrite.ToString());
            Response.Flush();

            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }

    private DataTable GetGridViewDataForExport()
    {
        // Export the same records currently displayed in the popup.
        DataTable dt = new DataTable();

        foreach (DataControlField column in GridView1.Columns)
        {
            string columnName = column.HeaderText;

            if (string.IsNullOrEmpty(columnName))
                columnName = "Column" + dt.Columns.Count.ToString();

            if (!dt.Columns.Contains(columnName))
                dt.Columns.Add(columnName);
        }

        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType != DataControlRowType.DataRow)
                continue;

            DataRow dataRow = dt.NewRow();

            for (int i = 0; i < row.Cells.Count && i < dt.Columns.Count; i++)
            {
                dataRow[i] = HttpUtility.HtmlDecode(
                    row.Cells[i].Text.Replace("&nbsp;", "").Trim()
                );
            }

            dt.Rows.Add(dataRow);
        }

        return dt;
    }
}
