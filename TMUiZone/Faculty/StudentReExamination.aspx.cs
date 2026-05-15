using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Faculty_StudentReExamination : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(
        ConfigurationManager.ConnectionStrings["TMUCON"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindGrid();
        }
    }

    public void bindGrid()
    {
        try
        {
            SqlCommand cmd = new SqlCommand(
                "sp_GetStudentReStatus", con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CollegeCode",
                Session["GlobalDimension1Code"].ToString());

            cmd.Parameters.AddWithValue("@AcademicYear",
                Session["AcademicYear"].ToString());

            cmd.Parameters.AddWithValue("@UserId",
                Session["uid"].ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            grdStudentReport.DataSource = dt;
            grdStudentReport.DataBind();
        }
        catch (Exception ex)
        {

        }
    }

    protected void grdStudentReport_RowDataBound(
        object sender,
        GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = (Label)e.Row.FindControl("lblPaymentStatus");

            if (lbl != null)
            {
                string status = lbl.Text.Trim().ToUpper();

                lbl.ForeColor = System.Drawing.Color.White;
                lbl.Font.Bold = true;

                lbl.Style.Add("padding", "6px 12px");
                lbl.Style.Add("border-radius", "20px");
                lbl.Style.Add("display", "inline-block");

                if (status == "PAID")
                {
                    lbl.BackColor = System.Drawing.Color.Green;
                }
                else
                {
                    lbl.BackColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            grdStudentReport.AllowPaging = false;

            bindGrid();

            Response.Clear();
            Response.Buffer = true;

            Response.AddHeader(
                "content-disposition",
                "attachment;filename=StudentReappearReport_" +
                DateTime.Now.ToString("ddMMyyyyHHmmss") +
                ".xls");

            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            HtmlForm frm = new HtmlForm();

            grdStudentReport.Parent.Controls.Add(frm);

            frm.Attributes["runat"] = "server";

            frm.Controls.Add(grdStudentReport);

            // Header Style
            grdStudentReport.HeaderRow.BackColor =
                System.Drawing.Color.Green;

            foreach (TableCell cell
                in grdStudentReport.HeaderRow.Cells)
            {
                cell.BackColor = System.Drawing.Color.Green;
                cell.ForeColor = System.Drawing.Color.White;
                cell.Font.Bold = true;
            }

            // Rows
            foreach (GridViewRow row
                in grdStudentReport.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    cell.BorderStyle = BorderStyle.Solid;
                    cell.BorderWidth = 1;
                }

                // Payment Status Text
                Label lbl =
                    (Label)row.FindControl("lblPaymentStatus");

                if (lbl != null)
                {
                    row.Cells[11].Controls.Clear();
                    row.Cells[11].Text = lbl.Text;
                }
            }

            frm.RenderControl(hw);

            string style = @"<style>
                            td
                            {
                                mso-number-format:\@;
                            }
                            </style>";

            Response.Write(style);

            Response.Output.Write(sw.ToString());

            Response.Flush();

            Response.SuppressContent = true;

            HttpContext.Current.ApplicationInstance
                .CompleteRequest();
        }
        catch (Exception ex)
        {

        }
    }
}