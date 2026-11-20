using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using iTextSharp.text.html.simpleparser;
using System.IO;

public partial class Faculty_NaacDashboard : System.Web.UI.Page
{
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["NAAC"].ToString());
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {


            if (!IsPostBack)
            {

                if (Session["uid"].ToString() == "TMU02982" || Session["uid"].ToString() == "TMU08026" || Session["uid"].ToString() == "TMU08617" || Session["uid"].ToString() == "TMU05294" || Session["uid"].ToString() == "TMU06022")
                {
                    BindAcademicYear();
                    BindMetrics();
                    bindetails(ddlAcademicYear.SelectedValue, drpMetric.SelectedValue);

                }
                else
                {
                    Response.Redirect("Error.aspx", false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }



            }



        }
        catch (Exception)
        {
            Response.Redirect("../Default.aspx");
        }
    }
    public void BindAcademicYear()
    {
        try
        {

            SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            con.Open();
            da.Fill(dt1);
            con.Close();
            ddlAcademicYear.DataSource = dt1;
            ddlAcademicYear.DataTextField = "Details";
            ddlAcademicYear.DataValueField = "No_";
            ddlAcademicYear.DataBind();
        }
        catch
        {
        }
    }
    public void bindetails(string academicYear, string metric)
    {
        SqlCommand cmd = new SqlCommand("[GetDataForDashboard_new]", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@AcademicYear", academicYear);
        cmd.Parameters.Add("@metric", metric);
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataTable dt = new DataTable();
        da.Fill(dt);

        JainStudentList.DataSource = dt;
        JainStudentList.DataBind();




    }
    public void BindMetrics()
    {
        try
        {

            SqlCommand cmd = new SqlCommand("SP_GetMetricbyAcademicyear", con1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            con.Open();
            da.Fill(dt1);
            con.Close();
            drpMetric.DataSource = dt1;
            drpMetric.DataTextField = "Details";
            drpMetric.DataValueField = "Code";
            drpMetric.DataBind();
        }
        catch
        {
        }
    }
    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMetrics();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        bindetails(ddlAcademicYear.SelectedValue, drpMetric.SelectedValue);
    }



    protected void JainStudentList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string metric = "";

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow row = e.Row;

                // =========================================================
                // METRIC NO
                // =========================================================

                Label lblMetricNo = (Label)row.FindControl("lblMetricNo");

                if (lblMetricNo != null)
                {
                    metric = lblMetricNo.Text.Trim();
                }


                // =========================================================
                // TARGET
                // =========================================================

                Label lblTarget = (Label)row.FindControl("lblTarget");

                int targetValue = 0;

                if (lblTarget != null)
                {
                    int.TryParse(
                        lblTarget.Text.Trim(),
                        out targetValue
                    );
                }


                // =========================================================
                // CUMULATIVE
                // =========================================================

                Label lblCumulative = (Label)row.FindControl("lblCumulative");

                int cumulativeValue = 0;

                if (lblCumulative != null)
                {
                    int.TryParse(
                        lblCumulative.Text.Trim(),
                        out cumulativeValue
                    );
                }


                // =========================================================
                // MONTHS
                // July -> June
                // =========================================================

                List<Label> months = new List<Label>()
            {
                (Label)row.FindControl("lblJuly"),
                (Label)row.FindControl("lblAugust"),
                (Label)row.FindControl("lblSeptember"),
                (Label)row.FindControl("lblOctober"),
                (Label)row.FindControl("lblNovember"),
                (Label)row.FindControl("lblDecember"),
                (Label)row.FindControl("lblJanuary"),
                (Label)row.FindControl("lblFebruary"),
                (Label)row.FindControl("lblMarch"),
                (Label)row.FindControl("lblApril"),
                (Label)row.FindControl("lblMay"),
                (Label)row.FindControl("lblJune")
            };


                // =========================================================
                // METRIC 4.3.2
                // JANUARY -> JUNE CARRY FORWARD
                // =========================================================

                if (metric == "4.3.2" || metric == "2.2.2")
                {
                    // January index = 6
                    int janIndex = 6;

                    for (int i = janIndex + 1; i < months.Count; i++)
                    {
                        if (months[i] != null && months[i - 1] != null)
                        {
                            int currentValue = 0;

                            int.TryParse(
                                months[i].Text.Trim(),
                                out currentValue
                            );

                            if (currentValue == 0)
                            {
                                months[i].Text = months[i - 1].Text;
                            }
                        }
                    }
                }


                // =========================================================
                // CUMULATIVE CELL COLOR
                // =========================================================

                // GridView Column Index
                // 16 = June
                // 17 = Cumulative

                int cumulativeCellIndex = 17;

                if (lblCumulative != null &&
                    row.Cells.Count > cumulativeCellIndex)
                {
                    cumulativeValue = 0;

                    int.TryParse(
                        lblCumulative.Text.Trim(),
                        out cumulativeValue
                    );


                    // =====================================================
                    // METRIC 4.3.2
                    // Cumulative <= Target = GREEN
                    // Cumulative > Target = RED
                    // =====================================================

                    if (metric == "4.3.2" || metric=="2.2.2")
                    {
                        if (cumulativeValue != 0 &&
                            cumulativeValue <= targetValue)
                        {
                            row.Cells[cumulativeCellIndex].BackColor =
                                System.Drawing.Color.LightGreen;
                        }
                        else
                        {
                            row.Cells[cumulativeCellIndex].BackColor =
                                System.Drawing.Color.LightCoral;
                        }
                    }


                    // =====================================================
                    // OTHER METRICS
                    // Cumulative >= Target = GREEN
                    // Cumulative < Target = RED
                    // =====================================================

                    else
                    {
                        if (cumulativeValue != 0 &&
                            cumulativeValue >= targetValue)
                        {
                            row.Cells[cumulativeCellIndex].BackColor =
                                System.Drawing.Color.LightGreen;
                        }
                        else
                        {
                            row.Cells[cumulativeCellIndex].BackColor =
                                System.Drawing.Color.LightCoral;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(
                this.Page,
                this.GetType(),
                "alert",
                "callFeedbackMessage('Error', '" +
                ex.Message.Replace("'", "") +
                "');",
                true
            );

            return;
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=NAAC_Metric_Report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            JainStudentList.AllowPaging = false;
            BindMetrics();

            // ================= HEADER SECTION =================
            string academicYear = ddlAcademicYear.SelectedValue;   // dynamic kar sakte ho DB se
            string tillDate = DateTime.Now.ToString("dd-MM-yyyy");

            sw.Write("<table width='100%' border='0'>");

            sw.Write("<tr>");
            sw.Write("<td colspan='17' style='font-size:18px;font-weight:bold;text-align:center;'>NAAC METRIC MONTHLY PROGRESS REPORT</td>");
            sw.Write("</tr>");

            sw.Write("<tr>");
            sw.Write("<td colspan='17' style='font-size:14px;font-weight:bold;text-align:center;'>Academic Year: " + academicYear + "</td>");
            sw.Write("</tr>");

            sw.Write("<tr>");
            sw.Write("<td colspan='17' style='font-size:12px;text-align:center;'>Report Date (Till Date): " + tillDate + "</td>");
            sw.Write("</tr>");

            sw.Write("<tr><td colspan='17'>&nbsp;</td></tr>");
            sw.Write("</table>");

            // ================= GRID =================
            JainStudentList.RenderControl(hw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

    }

}