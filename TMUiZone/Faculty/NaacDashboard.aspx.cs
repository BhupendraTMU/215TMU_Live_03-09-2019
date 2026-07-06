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
        SqlCommand cmd = new SqlCommand("[GetDataForDashboard]", con1);
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

                Label lblMetricNo = (Label)e.Row.FindControl("lblMetricNo");
                if (lblMetricNo.Text == "4.3.2")
                {

                    GridViewRow row = e.Row;

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");
                    Label lblCumulative = (Label)e.Row.FindControl("lblCumulative");

                    int targetValue = 0;
                    int.TryParse(lblTarget.Text, out targetValue);

                    List<Label> months = new List<Label>()
                {
                    (Label)e.Row.FindControl("lblJuly"),
                    (Label)e.Row.FindControl("lblAugust"),
                    (Label)e.Row.FindControl("lblSeptember"),
                    (Label)e.Row.FindControl("lblOctober"),
                    (Label)e.Row.FindControl("lblNovember"),
                    (Label)e.Row.FindControl("lblDecember"),
                    (Label)e.Row.FindControl("lblJanuary"),
                    (Label)e.Row.FindControl("lblFebruary"),
                    (Label)e.Row.FindControl("lblMarch"),
                    (Label)e.Row.FindControl("lblApril"),
                    (Label)e.Row.FindControl("lblMay"),
                    (Label)e.Row.FindControl("lblJune")
                };

                    // January Index
                    int janIndex = 6;

                    // Carry Forward (Jan -> Jun)
                    for (int i = janIndex + 1; i < months.Count; i++)
                    {
                        int currentValue = 0;
                        int.TryParse(months[i].Text, out currentValue);

                        if (currentValue == 0)
                        {
                            months[i].Text = months[i - 1].Text;
                        }
                    }



                    int monthStartCellIndex = 4;

                    // July -> June Coloring
                    for (int i = 0; i < months.Count; i++)
                    {
                        int monthValue = 0;
                        int.TryParse(months[i].Text, out monthValue);

                        if (monthValue <= targetValue && monthValue != 0)
                        {
                            row.Cells[monthStartCellIndex + i].BackColor =
                                System.Drawing.Color.LightGreen;
                        }
                        else
                        {
                            row.Cells[monthStartCellIndex + i].BackColor =
                                System.Drawing.Color.LightPink;
                        }
                    }

                    // Cumulative Coloring
                    if (lblCumulative != null)
                    {
                        int cumulativeValue = 0;
                        int.TryParse(lblCumulative.Text, out cumulativeValue);

                        int cumulativeCellIndex = 16;

                        if (cumulativeValue <= targetValue && cumulativeValue!=0)
                        {
                            row.Cells[cumulativeCellIndex].BackColor =
                                System.Drawing.Color.LightGreen;
                        }
                        else
                        {
                            row.Cells[cumulativeCellIndex].BackColor =
                                System.Drawing.Color.LightPink;
                        }
                    }
                }
                else
                {


                    GridViewRow row = e.Row;

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");
                    Label lblCumulative = (Label)e.Row.FindControl("lblCumulative");

                    int targetValue = 0;
                    int.TryParse(lblTarget.Text, out targetValue);

                    List<Label> months = new List<Label>()
                {
                    (Label)e.Row.FindControl("lblJuly"),
                    (Label)e.Row.FindControl("lblAugust"),
                    (Label)e.Row.FindControl("lblSeptember"),
                    (Label)e.Row.FindControl("lblOctober"),
                    (Label)e.Row.FindControl("lblNovember"),
                    (Label)e.Row.FindControl("lblDecember"),
                    (Label)e.Row.FindControl("lblJanuary"),
                    (Label)e.Row.FindControl("lblFebruary"),
                    (Label)e.Row.FindControl("lblMarch"),
                    (Label)e.Row.FindControl("lblApril"),
                    (Label)e.Row.FindControl("lblMay"),
                    (Label)e.Row.FindControl("lblJune")
                };

                    // January Index
                    int janIndex = 6;

                    // Carry Forward (Jan -> Jun)
                    for (int i = janIndex + 1; i < months.Count; i++)
                    {
                        int currentValue = 0;
                        int.TryParse(months[i].Text, out currentValue);

                        if (currentValue == 0)
                        {
                            months[i].Text = months[i - 1].Text;
                        }
                    }



                    int monthStartCellIndex = 4;

                    // July -> June Coloring
                    for (int i = 0; i < months.Count; i++)
                    {
                        int monthValue = 0;
                        int.TryParse(months[i].Text, out monthValue);

                        if (monthValue >= targetValue)
                        {
                            row.Cells[monthStartCellIndex + i].BackColor =
                                System.Drawing.Color.LightGreen;
                        }
                        else
                        {
                            row.Cells[monthStartCellIndex + i].BackColor =
                                System.Drawing.Color.LightPink;
                        }
                    }

                    // Cumulative Coloring
                    if (lblCumulative != null)
                    {
                        int cumulativeValue = 0;
                        int.TryParse(lblCumulative.Text, out cumulativeValue);

                        int cumulativeCellIndex = 16;

                        if (cumulativeValue >= targetValue)
                        {
                            row.Cells[cumulativeCellIndex].BackColor =
                                System.Drawing.Color.LightGreen;
                        }
                        else
                        {
                            row.Cells[cumulativeCellIndex].BackColor =
                                System.Drawing.Color.LightPink;
                        }
                    }
                }
            }          
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', " + metric + ");", true);
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