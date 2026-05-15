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
                GridViewRow row = e.Row;
                Label lblMetricNo = (Label)e.Row.FindControl("lblMetricNo");
                metric = lblMetricNo.Text;
                if (lblMetricNo.Text == "1.2.1")
                {
                    int currentMonthIndex = DateTime.Now.Month - 1;
                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");
                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text) / 12;

                    // All months in order
                    List<Label> months = new List<Label>()
{
    (Label)e.Row.FindControl("lblJanuary"),
    (Label)e.Row.FindControl("lblFebruary"),
    (Label)e.Row.FindControl("lblMarch"),
    (Label)e.Row.FindControl("lblApril"),
    (Label)e.Row.FindControl("lblMay"),
    (Label)e.Row.FindControl("lblJune"),
    (Label)e.Row.FindControl("lblJuly"),
    (Label)e.Row.FindControl("lblAugust"),
    (Label)e.Row.FindControl("lblSeptember"),
    (Label)e.Row.FindControl("lblOctober"),
    (Label)e.Row.FindControl("lblNovember"),
    (Label)e.Row.FindControl("lblDecember")
};

                    // 🔹 Step 1: Fill previous values dynamically
                    for (int i = 1; i <= currentMonthIndex; i++)
                    {
                        int currentVal = Convert.ToInt32(months[i].Text);

                        if (currentVal == 0)
                        {
                            months[i].Text = months[i - 1].Text; // previous month ki value
                        }
                    }

                    // 🔹 Step 2: Apply color
                    for (int i = 0; i <= currentMonthIndex; i++)
                    {
                        int val = Convert.ToInt32(months[i].Text);

                        if (val >= targetvalMonthly)
                            row.Cells[4 + i].BackColor = System.Drawing.Color.LightGreen;
                        else
                            row.Cells[4 + i].BackColor = System.Drawing.Color.OrangeRed;
                    }

                    // 🔹 Cumulative
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");

                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;
                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;
                    }
                }

                if (lblMetricNo.Text == "1.3.2")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = 1;


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }

                if (lblMetricNo.Text == "1.3.3")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text);


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "1.4.1")
                {
                    int currentMonthIndex = DateTime.Now.Month - 1;
                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");
                    int targetvalMonthly = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    // Month labels
                    Label[] monthLabels = new Label[]
                    {
    (Label)e.Row.FindControl("lblJanuary"),
    (Label)e.Row.FindControl("lblFebruary"),
    (Label)e.Row.FindControl("lblMarch"),
    (Label)e.Row.FindControl("lblApril"),
    (Label)e.Row.FindControl("lblMay"),
    (Label)e.Row.FindControl("lblJune"),
    (Label)e.Row.FindControl("lblJuly"),
    (Label)e.Row.FindControl("lblAugust"),
    (Label)e.Row.FindControl("lblSeptember"),
    (Label)e.Row.FindControl("lblOctober"),
    (Label)e.Row.FindControl("lblNovember"),
    (Label)e.Row.FindControl("lblDecember")
                    };

                    // 🔹 Check if any month = 1
                    bool anyMonthOne = monthLabels.Any(lbl =>
                    {
                        int val = string.IsNullOrEmpty(lbl.Text) ? 0 : Convert.ToInt32(lbl.Text);
                        return val == 1;
                    });

                    // 🔹 Apply color to all months dynamically
                    for (int i = 0; i <= currentMonthIndex; i++)
                    {
                        row.Cells[4 + i].BackColor = anyMonthOne
                            ? System.Drawing.Color.LightGreen
                            : System.Drawing.Color.OrangeRed;
                    }

                    // 🔹 OPTIONAL: jahan se 1 start ho, uske baad sab 1 ho jaye
                    bool found = false;
                    for (int i = 0; i <= currentMonthIndex; i++)
                    {
                        int val = string.IsNullOrEmpty(monthLabels[i].Text) ? 0 : Convert.ToInt32(monthLabels[i].Text);

                        if (val == 1)
                            found = true;

                        if (found)
                        {
                            monthLabels[i].Text = "1";
                            row.Cells[4 + i].Text = "1"; // grid cell bhi update
                        }
                    }

                    // 🔹 Cumulative (last column)
                    row.Cells[4 + monthLabels.Length].BackColor = anyMonthOne
                        ? System.Drawing.Color.GreenYellow
                        : System.Drawing.Color.Red;
                }
                if (lblMetricNo.Text == "2.1.1")
                {
                    int currentMonthIndex = DateTime.Now.Month - 1;
                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");
                    int targetvalMonthly = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    // Month labels array
                    Label[] months = new Label[]
                    {
    (Label)e.Row.FindControl("lblJanuary"),
    (Label)e.Row.FindControl("lblFebruary"),
    (Label)e.Row.FindControl("lblMarch"),
    (Label)e.Row.FindControl("lblApril"),
    (Label)e.Row.FindControl("lblMay"),
    (Label)e.Row.FindControl("lblJune"),
    (Label)e.Row.FindControl("lblJuly"),
    (Label)e.Row.FindControl("lblAugust"),
    (Label)e.Row.FindControl("lblSeptember"),
    (Label)e.Row.FindControl("lblOctober"),
    (Label)e.Row.FindControl("lblNovember"),
    (Label)e.Row.FindControl("lblDecember")
                    };

                    // 🔹 Step 1: Fill zero वाले months (Feb, Mar जैसे cases)
                    for (int i = 1; i <= currentMonthIndex; i++)
                    {
                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        if (val == 0)
                        {
                            months[i].Text = months[i - 1].Text; // previous month value
                        }
                    }

                    // 🔹 Step 2: Apply color dynamically
                    for (int i = 0; i <= currentMonthIndex; i++)
                    {
                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        row.Cells[4 + i].BackColor = (val >= targetvalMonthly)
                            ? System.Drawing.Color.LightGreen
                            : System.Drawing.Color.OrangeRed;
                    }

                    // 🔹 Step 3: Cumulative
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");

                    int cumVal = string.IsNullOrEmpty(lblCum.Text) ? 0 : Convert.ToInt32(lblCum.Text);
                    int targetVal = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    row.Cells[4 + months.Length].BackColor = (cumVal >= targetVal)
                        ? System.Drawing.Color.GreenYellow
                        : System.Drawing.Color.Red;
                }

                if (lblMetricNo.Text == "2.1.2")
                {
                    int currentMonthIndex = DateTime.Now.Month - 1;

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");

                    int targetvalMonthly = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    // Month labels
                    Label[] months = new Label[]
                    {
    (Label)e.Row.FindControl("lblJanuary"),
    (Label)e.Row.FindControl("lblFebruary"),
    (Label)e.Row.FindControl("lblMarch"),
    (Label)e.Row.FindControl("lblApril"),
    (Label)e.Row.FindControl("lblMay"),
    (Label)e.Row.FindControl("lblJune"),
    (Label)e.Row.FindControl("lblJuly"),
    (Label)e.Row.FindControl("lblAugust"),
    (Label)e.Row.FindControl("lblSeptember"),
    (Label)e.Row.FindControl("lblOctober"),
    (Label)e.Row.FindControl("lblNovember"),
    (Label)e.Row.FindControl("lblDecember")
                    };

                    int baseIndex = 4;

                    // 🔹 Step 1: Fill zero वाले months
                    for (int i = 1; i <= currentMonthIndex; i++)
                    {
                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        if (val == 0)
                        {
                            months[i].Text = months[i - 1].Text;
                        }
                    }

                    // 🔹 Step 2: Apply color till current month
                    for (int i = 0; i <= currentMonthIndex; i++)
                    {
                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        row.Cells[baseIndex + i].BackColor =
                            (val >= targetvalMonthly)
                            ? System.Drawing.Color.LightGreen
                            : System.Drawing.Color.OrangeRed;
                    }

                    // 🔹 Step 3: Future months (optional)
                    for (int i = currentMonthIndex + 1; i < months.Length; i++)
                    {
                        row.Cells[baseIndex + i].BackColor = System.Drawing.Color.White;
                    }

                    // 🔹 Step 4: Cumulative
                    int cumVal = string.IsNullOrEmpty(lblCum.Text) ? 0 : Convert.ToInt32(lblCum.Text);
                    int targetVal = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    row.Cells[baseIndex + months.Length].BackColor =
                        (cumVal >= targetVal)
                        ? System.Drawing.Color.GreenYellow
                        : System.Drawing.Color.Red;
                }

                if (lblMetricNo.Text == "2.2.2")
                {
                    int currentMonthIndex = DateTime.Now.Month - 1;

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");

                    int targetvalMonthly = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    // Month labels
                    Label[] months = new Label[]
                    {
    (Label)e.Row.FindControl("lblJanuary"),
    (Label)e.Row.FindControl("lblFebruary"),
    (Label)e.Row.FindControl("lblMarch"),
    (Label)e.Row.FindControl("lblApril"),
    (Label)e.Row.FindControl("lblMay"),
    (Label)e.Row.FindControl("lblJune"),
    (Label)e.Row.FindControl("lblJuly"),
    (Label)e.Row.FindControl("lblAugust"),
    (Label)e.Row.FindControl("lblSeptember"),
    (Label)e.Row.FindControl("lblOctober"),
    (Label)e.Row.FindControl("lblNovember"),
    (Label)e.Row.FindControl("lblDecember")
                    };

                    int baseIndex = 4;

                    // 🔹 Step 1: Fill zero वाले months
                    for (int i = 1; i <= currentMonthIndex; i++)
                    {
                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        if (val == 0)
                        {
                            months[i].Text = months[i - 1].Text;
                        }
                    }

                    // 🔹 Step 2: Apply color till current month
                    for (int i = 0; i <= currentMonthIndex; i++)
                    {
                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        row.Cells[baseIndex + i].BackColor =
                            (val >= targetvalMonthly)
                            ? System.Drawing.Color.LightGreen
                            : System.Drawing.Color.OrangeRed;
                    }

                    // 🔹 Step 3: Future months (optional)
                    for (int i = currentMonthIndex + 1; i < months.Length; i++)
                    {
                        row.Cells[baseIndex + i].BackColor = System.Drawing.Color.White;
                    }

                    // 🔹 Step 4: Cumulative
                    int cumVal = string.IsNullOrEmpty(lblCum.Text) ? 0 : Convert.ToInt32(lblCum.Text);
                    int targetVal = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    row.Cells[baseIndex + months.Length].BackColor =
                        (cumVal >= targetVal)
                        ? System.Drawing.Color.GreenYellow
                        : System.Drawing.Color.Red;
                }





                if (lblMetricNo.Text == "2.4.1")
                {
                    int currentMonthIndex = DateTime.Now.Month - 1;

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");

                    int targetvalMonthly = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    // Month labels
                    Label[] months = new Label[]
                    {
    (Label)e.Row.FindControl("lblJanuary"),
    (Label)e.Row.FindControl("lblFebruary"),
    (Label)e.Row.FindControl("lblMarch"),
    (Label)e.Row.FindControl("lblApril"),
    (Label)e.Row.FindControl("lblMay"),
    (Label)e.Row.FindControl("lblJune"),
    (Label)e.Row.FindControl("lblJuly"),
    (Label)e.Row.FindControl("lblAugust"),
    (Label)e.Row.FindControl("lblSeptember"),
    (Label)e.Row.FindControl("lblOctober"),
    (Label)e.Row.FindControl("lblNovember"),
    (Label)e.Row.FindControl("lblDecember")
                    };

                    int baseIndex = 4;

                    // 🔹 Step 1: ONLY Feb & Mar → Jan se fill (same as your original logic)
                    for (int i = 1;  i <= currentMonthIndex; i++)
                    {
                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        if (val == 0)
                        {
                            months[i].Text = months[0].Text; // Jan se fill
                        }
                    }











                    // 🔹 Step 2: Color apply (only till current month)
                    for (int i = 0; i <= currentMonthIndex; i++)
                    {
                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        row.Cells[baseIndex + i].BackColor =
                            (val >= targetvalMonthly)
                            ? System.Drawing.Color.LightGreen
                            : System.Drawing.Color.OrangeRed;
                    }

                    // 🔹 Step 3: बाकी months (future months - optional)
                    for (int i = currentMonthIndex + 1; i < months.Length; i++)
                    {
                        row.Cells[baseIndex + i].BackColor = System.Drawing.Color.White; // optional
                    }

                    // 🔹 Step 4: Cumulative
                    int cumVal = string.IsNullOrEmpty(lblCum.Text) ? 0 : Convert.ToInt32(lblCum.Text);
                    int targetVal = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    row.Cells[baseIndex + months.Length].BackColor =
                        (cumVal >= targetVal)
                        ? System.Drawing.Color.GreenYellow
                        : System.Drawing.Color.Red;
                }

                if (lblMetricNo.Text == "2.4.2")
                {

                    int currentMonthIndex = DateTime.Now.Month - 1;

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");

                    int targetvalMonthly = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    // Month labels
                    Label[] months = new Label[]
                    {
    (Label)e.Row.FindControl("lblJanuary"),
    (Label)e.Row.FindControl("lblFebruary"),
    (Label)e.Row.FindControl("lblMarch"),
    (Label)e.Row.FindControl("lblApril"),
    (Label)e.Row.FindControl("lblMay"),
    (Label)e.Row.FindControl("lblJune"),
    (Label)e.Row.FindControl("lblJuly"),
    (Label)e.Row.FindControl("lblAugust"),
    (Label)e.Row.FindControl("lblSeptember"),
    (Label)e.Row.FindControl("lblOctober"),
    (Label)e.Row.FindControl("lblNovember"),
    (Label)e.Row.FindControl("lblDecember")
                    };

                    int baseIndex = 4;

                    // 🔹 Step 1: ONLY Feb & Mar → Jan se fill (same as your code)
                    for (int i = 1; i <= currentMonthIndex; i++)
                    {
                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        if (val == 0)
                        {
                            months[i].Text = months[0].Text;
                        }
                    }

                    // 🔹 Step 2: Color apply till current month
                    for (int i = 0; i <= currentMonthIndex; i++)
                    {
                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        row.Cells[baseIndex + i].BackColor =
                            (val >= targetvalMonthly)
                            ? System.Drawing.Color.LightGreen
                            : System.Drawing.Color.OrangeRed;
                    }

                    // 🔹 Step 3: Future months (optional)
                    for (int i = currentMonthIndex + 1; i < months.Length; i++)
                    {
                        row.Cells[baseIndex + i].BackColor = System.Drawing.Color.White;
                    }

                    // 🔹 Step 4: Cumulative
                    int cumVal = string.IsNullOrEmpty(lblCum.Text) ? 0 : Convert.ToInt32(lblCum.Text);
                    int targetVal = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    row.Cells[baseIndex + months.Length].BackColor =
                        (cumVal >= targetVal)
                        ? System.Drawing.Color.GreenYellow
                        : System.Drawing.Color.Red;
                }

                if (lblMetricNo.Text == "2.4.3")
                {

                    int currentMonthIndex = DateTime.Now.Month - 1;

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");

                    int targetvalMonthly = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    // Month labels
                    Label[] months = new Label[]
                    {
    (Label)e.Row.FindControl("lblJanuary"),
    (Label)e.Row.FindControl("lblFebruary"),
    (Label)e.Row.FindControl("lblMarch"),
    (Label)e.Row.FindControl("lblApril"),
    (Label)e.Row.FindControl("lblMay"),
    (Label)e.Row.FindControl("lblJune"),
    (Label)e.Row.FindControl("lblJuly"),
    (Label)e.Row.FindControl("lblAugust"),
    (Label)e.Row.FindControl("lblSeptember"),
    (Label)e.Row.FindControl("lblOctober"),
    (Label)e.Row.FindControl("lblNovember"),
    (Label)e.Row.FindControl("lblDecember")
                    };

                    int baseIndex = 4;

                    // 🔹 Step 1: Feb & Mar → Jan se fill (safe bound check)
                    for (int i = 1; i <= currentMonthIndex; i++)
                    {
                        if (i > currentMonthIndex) break;

                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        if (val == 0)
                        {
                            months[i].Text = months[0].Text;
                        }
                    }

                    // 🔹 Step 2: Color apply (only till current month)
                    for (int i = 0; i <= currentMonthIndex && i < months.Length; i++)
                    {
                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        row.Cells[baseIndex + i].BackColor =
                            (val >= targetvalMonthly)
                            ? System.Drawing.Color.LightGreen
                            : System.Drawing.Color.OrangeRed;
                    }

                    // 🔹 Step 3: Future months (optional UI clarity)
                    for (int i = currentMonthIndex + 1; i < months.Length; i++)
                    {
                        row.Cells[baseIndex + i].BackColor = System.Drawing.Color.White;
                    }

                    // 🔹 Step 4: Cumulative
                    int cumVal = string.IsNullOrEmpty(lblCum.Text) ? 0 : Convert.ToInt32(lblCum.Text);
                    int targetVal = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    row.Cells[baseIndex + months.Length].BackColor =
                        (cumVal >= targetVal)
                        ? System.Drawing.Color.GreenYellow
                        : System.Drawing.Color.Red;
                }
                if (lblMetricNo.Text == "2.5.1")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text);


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "2.5.2")
                {
                    int currentMonthIndex = DateTime.Now.Month - 1;

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");

                    int targetvalMonthly = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    // Month labels
                    Label[] months = new Label[]
                    {
    (Label)e.Row.FindControl("lblJanuary"),
    (Label)e.Row.FindControl("lblFebruary"),
    (Label)e.Row.FindControl("lblMarch"),
    (Label)e.Row.FindControl("lblApril"),
    (Label)e.Row.FindControl("lblMay"),
    (Label)e.Row.FindControl("lblJune"),
    (Label)e.Row.FindControl("lblJuly"),
    (Label)e.Row.FindControl("lblAugust"),
    (Label)e.Row.FindControl("lblSeptember"),
    (Label)e.Row.FindControl("lblOctober"),
    (Label)e.Row.FindControl("lblNovember"),
    (Label)e.Row.FindControl("lblDecember")
                    };

                    int baseIndex = 4;

                    // 🔹 Step 1: Feb & Mar → Jan se fill
                    for (int i = 1; i <= 2; i++)
                    {
                        if (i > currentMonthIndex) break;

                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        if (val == 0)
                        {
                            months[i].Text = months[0].Text;
                        }
                    }

                    // 🔹 Step 2: Color apply (<= condition)
                    for (int i = 0; i <= currentMonthIndex && i < months.Length; i++)
                    {
                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        row.Cells[baseIndex + i].BackColor =
                            (val <= targetvalMonthly)
                            ? System.Drawing.Color.LightGreen
                            : System.Drawing.Color.OrangeRed;
                    }

                    // 🔹 Step 3: Future months → no color (optional remove grey)
                    // (agar chaho to empty chhod do)

                    // 🔹 Step 4: Cumulative
                    int cumVal = string.IsNullOrEmpty(lblCum.Text) ? 0 : Convert.ToInt32(lblCum.Text);
                    int targetVal = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    row.Cells[baseIndex + months.Length].BackColor =
                        (cumVal <= targetVal)
                        ? System.Drawing.Color.GreenYellow
                        : System.Drawing.Color.Red;
                }

                if (lblMetricNo.Text == "2.5.3")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text);


                    Label[] monthLabels = new Label[]
    {
    (Label)e.Row.FindControl("lblJanuary"),
    (Label)e.Row.FindControl("lblFebruary"),
    (Label)e.Row.FindControl("lblMarch"),
    (Label)e.Row.FindControl("lblApril"),
    (Label)e.Row.FindControl("lblMay"),
    (Label)e.Row.FindControl("lblJune"),
    (Label)e.Row.FindControl("lblJuly"),
    (Label)e.Row.FindControl("lblAugust"),
    (Label)e.Row.FindControl("lblSeptember"),
    (Label)e.Row.FindControl("lblOctober"),
    (Label)e.Row.FindControl("lblNovember"),
    (Label)e.Row.FindControl("lblDecember")
    };

                    // Check if any month has value 1
                    bool anyMonthOne = monthLabels.Any(lbl => Convert.ToInt32(lbl.Text) == 1);
                    if (anyMonthOne == true)
                    {
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    }
                    else
                    {
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    }


                    if (anyMonthOne == true)
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }

                if (lblMetricNo.Text == "2.6.2")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text);


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "2.7.1")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text);


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "3.1.2")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text) / 12;


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "3.1.3")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text) / 12;


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }

                if (lblMetricNo.Text == "3.1.4")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text) / 2;


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "3.2.1")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text) / 12;


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "3.2.2")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text) / 12;


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "3.3.2")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text) / 6;


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }

                if (lblMetricNo.Text == "3.4.1")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text);


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "3.4.2")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text) / 2;


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "3.4.3")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text) / 12;


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "3.4.5")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text) / 12;


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }





                }
                if (lblMetricNo.Text == "3.4.4")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text) / 12;


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "3.4.6")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text);


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "3.4.7")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text);


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "3.4.8")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text);


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "3.5.1")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text) / 12;


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "3.6.2")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text) / 12;


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "3.7.1")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text) / 8;


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "4.1.2")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text) / 12;


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "4.2.2")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text) / 12;


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "4.3.3")
                {

                    int currentMonthIndex = DateTime.Now.Month - 1;

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");

                    int targetvalMonthly = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    // Month labels
                    Label[] months = new Label[]
                    {
    (Label)e.Row.FindControl("lblJanuary"),
    (Label)e.Row.FindControl("lblFebruary"),
    (Label)e.Row.FindControl("lblMarch"),
    (Label)e.Row.FindControl("lblApril"),
    (Label)e.Row.FindControl("lblMay"),
    (Label)e.Row.FindControl("lblJune"),
    (Label)e.Row.FindControl("lblJuly"),
    (Label)e.Row.FindControl("lblAugust"),
    (Label)e.Row.FindControl("lblSeptember"),
    (Label)e.Row.FindControl("lblOctober"),
    (Label)e.Row.FindControl("lblNovember"),
    (Label)e.Row.FindControl("lblDecember")
                    };

                    int baseIndex = 4;

                    // 🔹 Step 1: Feb & Mar → Jan se fill
                    for (int i = 1; i <= 2; i++)
                    {
                        if (i > currentMonthIndex) break;

                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        if (val == 0)
                        {
                            months[i].Text = months[0].Text;
                        }
                    }

                    // 🔹 Step 2: Color apply (>= condition)
                    for (int i = 0; i < months.Length; i++)
                    {
                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        row.Cells[baseIndex + i].BackColor =
                            (val >= targetvalMonthly)
                            ? System.Drawing.Color.LightGreen
                            : System.Drawing.Color.OrangeRed;
                    }

                    // 🔹 Step 3: Cumulative
                    int cumVal = string.IsNullOrEmpty(lblCum.Text) ? 0 : Convert.ToInt32(lblCum.Text);
                    int targetVal = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    row.Cells[baseIndex + months.Length].BackColor =
                        (cumVal >= targetVal)
                        ? System.Drawing.Color.GreenYellow
                        : System.Drawing.Color.Red;
                }
                if (lblMetricNo.Text == "4.4.1")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text) / 12;


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "5.1.1")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text);


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "5.1.3")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text);


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "5.1.4")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text);


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "5.2.1")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text) / 12;


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "5.2.2")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text) / 12;


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "5.2.3")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text) / 12;


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "5.3.1")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text);


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "5.3.3")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text);


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "5.4.1")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text);


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "6.2.2")
                {

                    int currentMonthIndex = DateTime.Now.Month - 1;

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");

                    int targetvalMonthly = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    // Month labels
                    Label[] months = new Label[]
                    {
    (Label)e.Row.FindControl("lblJanuary"),
    (Label)e.Row.FindControl("lblFebruary"),
    (Label)e.Row.FindControl("lblMarch"),
    (Label)e.Row.FindControl("lblApril"),
    (Label)e.Row.FindControl("lblMay"),
    (Label)e.Row.FindControl("lblJune"),
    (Label)e.Row.FindControl("lblJuly"),
    (Label)e.Row.FindControl("lblAugust"),
    (Label)e.Row.FindControl("lblSeptember"),
    (Label)e.Row.FindControl("lblOctober"),
    (Label)e.Row.FindControl("lblNovember"),
    (Label)e.Row.FindControl("lblDecember")
                    };

                    int baseIndex = 4;

                    // 🔹 Step 1: Feb & Mar → Jan se fill (only till current month)
                    for (int i = 1; i <= 2; i++)
                    {
                        if (i > currentMonthIndex) break;

                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        if (val == 0)
                        {
                            months[i].Text = months[0].Text;
                        }
                    }

                    // 🔹 Step 2: Color apply ONLY till current month
                    for (int i = 0; i <= currentMonthIndex && i < months.Length; i++)
                    {
                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        row.Cells[baseIndex + i].BackColor =
                            (val >= targetvalMonthly)
                            ? System.Drawing.Color.LightGreen
                            : System.Drawing.Color.OrangeRed;
                    }

                    // 🔹 Step 3: Future months → no color (default छोड़ दो)

                    // 🔹 Step 4: Cumulative
                    int cumVal = string.IsNullOrEmpty(lblCum.Text) ? 0 : Convert.ToInt32(lblCum.Text);
                    int targetVal = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    row.Cells[baseIndex + months.Length].BackColor =
                        (cumVal >= targetVal)
                        ? System.Drawing.Color.GreenYellow
                        : System.Drawing.Color.Red;
                }
                if (lblMetricNo.Text == "6.3.2")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text) / 12;


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "6.3.3")
                {

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");

                    int targetvalMonthly = Convert.ToInt32(lblTarget.Text) / 12;


                    Label lblJan = (Label)e.Row.FindControl("lblJanuary");

                    Label lblFeb = (Label)e.Row.FindControl("lblFebruary");
                    Label lblMar = (Label)e.Row.FindControl("lblMarch");
                    Label lblApr = (Label)e.Row.FindControl("lblApril");
                    Label lblMay = (Label)e.Row.FindControl("lblMay");
                    Label lblJun = (Label)e.Row.FindControl("lblJune");
                    Label lblJul = (Label)e.Row.FindControl("lblJuly");
                    Label lblAug = (Label)e.Row.FindControl("lblAugust");
                    Label lblSep = (Label)e.Row.FindControl("lblSeptember");
                    Label lblOct = (Label)e.Row.FindControl("lblOctober");
                    Label lblNov = (Label)e.Row.FindControl("lblNovember");
                    Label lblDec = (Label)e.Row.FindControl("lblDecember");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");
                    if (Convert.ToInt32(lblJan.Text) >= targetvalMonthly)
                        row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[4].BackColor = System.Drawing.Color.OrangeRed;

                    // February
                    if (Convert.ToInt32(lblFeb.Text) >= targetvalMonthly)
                        row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[5].BackColor = System.Drawing.Color.OrangeRed;

                    // March
                    if (Convert.ToInt32(lblMar.Text) >= targetvalMonthly)
                        row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[6].BackColor = System.Drawing.Color.OrangeRed;

                    // April
                    if (Convert.ToInt32(lblApr.Text) >= targetvalMonthly)
                        row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[7].BackColor = System.Drawing.Color.OrangeRed;

                    // May
                    if (Convert.ToInt32(lblMay.Text) >= targetvalMonthly)
                        row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[8].BackColor = System.Drawing.Color.OrangeRed;

                    // June
                    if (Convert.ToInt32(lblJun.Text) >= targetvalMonthly)
                        row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[9].BackColor = System.Drawing.Color.OrangeRed;

                    // July
                    if (Convert.ToInt32(lblJul.Text) >= targetvalMonthly)
                        row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[10].BackColor = System.Drawing.Color.OrangeRed;

                    // August
                    if (Convert.ToInt32(lblAug.Text) >= targetvalMonthly)
                        row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[11].BackColor = System.Drawing.Color.OrangeRed;

                    // September
                    if (Convert.ToInt32(lblSep.Text) >= targetvalMonthly)
                        row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[12].BackColor = System.Drawing.Color.OrangeRed;

                    // October
                    if (Convert.ToInt32(lblOct.Text) >= targetvalMonthly)
                        row.Cells[13].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[13].BackColor = System.Drawing.Color.OrangeRed;

                    // November
                    if (Convert.ToInt32(lblNov.Text) >= targetvalMonthly)
                        row.Cells[14].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[14].BackColor = System.Drawing.Color.OrangeRed;

                    // December
                    if (Convert.ToInt32(lblDec.Text) >= targetvalMonthly)
                        row.Cells[15].BackColor = System.Drawing.Color.LightGreen;
                    else
                        row.Cells[15].BackColor = System.Drawing.Color.OrangeRed;
                    if (Convert.ToInt32(lblCum.Text) >= Convert.ToInt32(lblTarget.Text))
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;

                    }
                    else
                    {
                        row.Cells[16].BackColor = System.Drawing.Color.Red;

                    }
                }
                if (lblMetricNo.Text == "6.4.2")
                {

                    int currentMonthIndex = DateTime.Now.Month - 1;

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");

                    int targetTotal = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);
                    int targetvalMonthly = targetTotal / 12; // 🔥 monthly target

                    // Month labels
                    Label[] months = new Label[]
                    {
    (Label)e.Row.FindControl("lblJanuary"),
    (Label)e.Row.FindControl("lblFebruary"),
    (Label)e.Row.FindControl("lblMarch"),
    (Label)e.Row.FindControl("lblApril"),
    (Label)e.Row.FindControl("lblMay"),
    (Label)e.Row.FindControl("lblJune"),
    (Label)e.Row.FindControl("lblJuly"),
    (Label)e.Row.FindControl("lblAugust"),
    (Label)e.Row.FindControl("lblSeptember"),
    (Label)e.Row.FindControl("lblOctober"),
    (Label)e.Row.FindControl("lblNovember"),
    (Label)e.Row.FindControl("lblDecember")
                    };

                    int baseIndex = 4;

                    // 🔹 Step 1: Feb & Mar → Jan se fill
                    for (int i = 1; i <= 2; i++)
                    {
                        if (i > currentMonthIndex) break;

                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        if (val == 0)
                        {
                            months[i].Text = months[0].Text;
                        }
                    }

                    // 🔹 Step 2: Color apply (ONLY till current month)
                    for (int i = 0; i <= currentMonthIndex && i < months.Length; i++)
                    {
                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        row.Cells[baseIndex + i].BackColor =
                            (val >= targetvalMonthly)
                            ? System.Drawing.Color.LightGreen
                            : System.Drawing.Color.OrangeRed;
                    }

                    // 🔹 Step 3: Future months → no color (clean UI)

                    // 🔹 Step 4: Cumulative (compare with TOTAL target)
                    int cumVal = string.IsNullOrEmpty(lblCum.Text) ? 0 : Convert.ToInt32(lblCum.Text);

                    row.Cells[baseIndex + months.Length].BackColor =
                        (cumVal >= targetTotal)
                        ? System.Drawing.Color.GreenYellow
                        : System.Drawing.Color.Red;
                }

                if (lblMetricNo.Text == "6.5.2")
                {

                    int currentMonthIndex = DateTime.Now.Month - 1;

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");

                    int targetvalMonthly = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    // Month labels
                    Label[] months = new Label[]
                    {
    (Label)e.Row.FindControl("lblJanuary"),
    (Label)e.Row.FindControl("lblFebruary"),
    (Label)e.Row.FindControl("lblMarch"),
    (Label)e.Row.FindControl("lblApril"),
    (Label)e.Row.FindControl("lblMay"),
    (Label)e.Row.FindControl("lblJune"),
    (Label)e.Row.FindControl("lblJuly"),
    (Label)e.Row.FindControl("lblAugust"),
    (Label)e.Row.FindControl("lblSeptember"),
    (Label)e.Row.FindControl("lblOctober"),
    (Label)e.Row.FindControl("lblNovember"),
    (Label)e.Row.FindControl("lblDecember")
                    };

                    int baseIndex = 4;

                    // 🔹 Step 1: Feb & Mar → Jan se fill
                    for (int i = 1; i <= 2; i++)
                    {
                        if (i > currentMonthIndex) break;

                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        if (val == 0)
                        {
                            months[i].Text = months[0].Text;
                        }
                    }

                    // 🔹 Step 2: Color apply (only till current month)
                    for (int i = 0; i <= currentMonthIndex && i < months.Length; i++)
                    {
                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        row.Cells[baseIndex + i].BackColor =
                            (val >= targetvalMonthly)
                            ? System.Drawing.Color.LightGreen
                            : System.Drawing.Color.OrangeRed;
                    }

                    // 🔹 Step 3: Future months → no color (clean UI)

                    // 🔹 Step 4: Cumulative
                    int cumVal = string.IsNullOrEmpty(lblCum.Text) ? 0 : Convert.ToInt32(lblCum.Text);
                    int targetVal = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    row.Cells[baseIndex + months.Length].BackColor =
                        (cumVal >= targetVal)
                        ? System.Drawing.Color.GreenYellow
                        : System.Drawing.Color.Red;
                }
                if (lblMetricNo.Text == "7.1.10")
                {

                    int currentMonthIndex = DateTime.Now.Month - 1;

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");

                    // Target (monthly ya yearly — jo chahiye use adjust kar lo)
                    int targetvalMonthly = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    // All months in array
                    Label[] months = new Label[]
                    {
    (Label)e.Row.FindControl("lblJanuary"),
    (Label)e.Row.FindControl("lblFebruary"),
    (Label)e.Row.FindControl("lblMarch"),
    (Label)e.Row.FindControl("lblApril"),
    (Label)e.Row.FindControl("lblMay"),
    (Label)e.Row.FindControl("lblJune"),
    (Label)e.Row.FindControl("lblJuly"),
    (Label)e.Row.FindControl("lblAugust"),
    (Label)e.Row.FindControl("lblSeptember"),
    (Label)e.Row.FindControl("lblOctober"),
    (Label)e.Row.FindControl("lblNovember"),
    (Label)e.Row.FindControl("lblDecember")
                    };

                    int baseIndex = 4;

                    // 🔹 Step 1: Feb & Mar ko Jan se fill karo (same as your logic)
                    for (int i = 1; i <= 2 && i <= currentMonthIndex; i++)
                    {
                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        if (val == 0)
                        {
                            months[i].Text = months[0].Text;
                        }
                    }

                    // 🔹 Step 2: Dynamic coloring till current month
                    for (int i = 0; i <= currentMonthIndex; i++)
                    {
                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        row.Cells[baseIndex + i].BackColor =
                            (val >= targetvalMonthly)
                            ? System.Drawing.Color.LightGreen
                            : System.Drawing.Color.OrangeRed;
                    }

                    // 🔹 Step 3: Future months → no color (remove grey)
                    for (int i = currentMonthIndex + 1; i < months.Length; i++)
                    {
                        row.Cells[baseIndex + i].BackColor = System.Drawing.Color.Transparent;
                    }

                    // 🔹 Step 4: Cumulative
                    int cumVal = string.IsNullOrEmpty(lblCum.Text) ? 0 : Convert.ToInt32(lblCum.Text);
                    int targetVal = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    row.Cells[baseIndex + months.Length].BackColor =
                        (cumVal >= targetVal)
                        ? System.Drawing.Color.GreenYellow
                        : System.Drawing.Color.Red;
                }
                if (lblMetricNo.Text == "7.1.2")
                {

                    int currentMonthIndex = DateTime.Now.Month - 1;

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");

                    int targetvalMonthly = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    // All months in array
                    Label[] months = new Label[]
                    {
    (Label)e.Row.FindControl("lblJanuary"),
    (Label)e.Row.FindControl("lblFebruary"),
    (Label)e.Row.FindControl("lblMarch"),
    (Label)e.Row.FindControl("lblApril"),
    (Label)e.Row.FindControl("lblMay"),
    (Label)e.Row.FindControl("lblJune"),
    (Label)e.Row.FindControl("lblJuly"),
    (Label)e.Row.FindControl("lblAugust"),
    (Label)e.Row.FindControl("lblSeptember"),
    (Label)e.Row.FindControl("lblOctober"),
    (Label)e.Row.FindControl("lblNovember"),
    (Label)e.Row.FindControl("lblDecember")
                    };

                    int baseIndex = 4;

                    // 🔹 Step 1: Feb & Mar → Jan se fill (same as your code)
                    for (int i = 1; i <= 2 && i <= currentMonthIndex; i++)
                    {
                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        if (val == 0)
                        {
                            months[i].Text = months[0].Text;
                        }
                    }

                    // 🔹 Step 2: Apply color (ALL months – same as your original behavior)
                    for (int i = 0; i <= currentMonthIndex && i < months.Length; i++)
                    {
                       
                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        row.Cells[baseIndex + i].BackColor =
                            (val >= targetvalMonthly)
                            ? System.Drawing.Color.LightGreen
                            : System.Drawing.Color.OrangeRed;
                    }

                    // 🔹 Step 3: Cumulative
                    int cumVal = string.IsNullOrEmpty(lblCum.Text) ? 0 : Convert.ToInt32(lblCum.Text);
                    int targetVal = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    row.Cells[baseIndex + months.Length].BackColor =
                        (cumVal >= targetVal)
                        ? System.Drawing.Color.GreenYellow
                        : System.Drawing.Color.Red;
                }
                if (lblMetricNo.Text == "7.1.4")
                {

                    int currentMonthIndex = DateTime.Now.Month - 1;

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");

                    int targetvalMonthly = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    // Months array
                    Label[] months = new Label[]
                    {
    (Label)e.Row.FindControl("lblJanuary"),
    (Label)e.Row.FindControl("lblFebruary"),
    (Label)e.Row.FindControl("lblMarch"),
    (Label)e.Row.FindControl("lblApril"),
    (Label)e.Row.FindControl("lblMay"),
    (Label)e.Row.FindControl("lblJune"),
    (Label)e.Row.FindControl("lblJuly"),
    (Label)e.Row.FindControl("lblAugust"),
    (Label)e.Row.FindControl("lblSeptember"),
    (Label)e.Row.FindControl("lblOctober"),
    (Label)e.Row.FindControl("lblNovember"),
    (Label)e.Row.FindControl("lblDecember")
                    };

                    int baseIndex = 4;

                    // 🔹 Step 1: Feb & Mar → Jan se fill (same as your original logic)
                    for (int i = 1; i <= 2 && i <= currentMonthIndex; i++)
                    {
                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        if (val == 0)
                        {
                            months[i].Text = months[0].Text;
                        }
                    }

                    // 🔹 Step 2: Apply color to ALL months (same behavior as your code)
                    for (int i = 0; i <= currentMonthIndex && i < months.Length; i++)
                    {

                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        row.Cells[baseIndex + i].BackColor =
                            (val >= targetvalMonthly)
                            ? System.Drawing.Color.LightGreen
                            : System.Drawing.Color.OrangeRed;
                    }

                    // 🔹 Step 3: Cumulative
                    int cumVal = string.IsNullOrEmpty(lblCum.Text) ? 0 : Convert.ToInt32(lblCum.Text);
                    int targetVal = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    row.Cells[baseIndex + months.Length].BackColor =
                        (cumVal >= targetVal)
                        ? System.Drawing.Color.GreenYellow
                        : System.Drawing.Color.Red;
                }
                if (lblMetricNo.Text == "7.1.6")
                {

                    int currentMonthIndex = DateTime.Now.Month - 1;

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");

                    int targetvalMonthly = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    // Months array
                    Label[] months = new Label[]
                    {
    (Label)e.Row.FindControl("lblJanuary"),
    (Label)e.Row.FindControl("lblFebruary"),
    (Label)e.Row.FindControl("lblMarch"),
    (Label)e.Row.FindControl("lblApril"),
    (Label)e.Row.FindControl("lblMay"),
    (Label)e.Row.FindControl("lblJune"),
    (Label)e.Row.FindControl("lblJuly"),
    (Label)e.Row.FindControl("lblAugust"),
    (Label)e.Row.FindControl("lblSeptember"),
    (Label)e.Row.FindControl("lblOctober"),
    (Label)e.Row.FindControl("lblNovember"),
    (Label)e.Row.FindControl("lblDecember")
                    };

                    int baseIndex = 4;

                    // 🔹 Step 1: Feb & Mar → Jan se fill
                    for (int i = 1; i <= 2 && i <= currentMonthIndex; i++)
                    {
                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        if (val == 0)
                        {
                            months[i].Text = months[0].Text;
                        }
                    }

                    // 🔹 Step 2: ONLY current month tak color (IMPORTANT)
                    for (int i = 0; i <= currentMonthIndex && i < months.Length; i++)
                    {

                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        row.Cells[baseIndex + i].BackColor =
                            (val >= targetvalMonthly)
                            ? System.Drawing.Color.LightGreen
                            : System.Drawing.Color.OrangeRed;
                    }

                    // 🔹 Step 3: Future months → no color
                    for (int i = currentMonthIndex + 1; i < months.Length; i++)
                    {
                        row.Cells[baseIndex + i].BackColor = System.Drawing.Color.Transparent;
                    }

                    // 🔹 Step 4: Cumulative
                    int cumVal = string.IsNullOrEmpty(lblCum.Text) ? 0 : Convert.ToInt32(lblCum.Text);
                    int targetVal = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    row.Cells[baseIndex + months.Length].BackColor =
                        (cumVal >= targetVal)
                        ? System.Drawing.Color.GreenYellow
                        : System.Drawing.Color.Red;
                }
                if (lblMetricNo.Text == "4.3.2")
                {

                    int currentMonthIndex = DateTime.Now.Month - 1;

                    Label lblTarget = (Label)e.Row.FindControl("lblTarget");
                    Label lblCum = (Label)e.Row.FindControl("lblCumulative");

                    int targetvalMonthly = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    // Month labels array
                    Label[] months = new Label[]
                    {
    (Label)e.Row.FindControl("lblJanuary"),
    (Label)e.Row.FindControl("lblFebruary"),
    (Label)e.Row.FindControl("lblMarch"),
    (Label)e.Row.FindControl("lblApril"),
    (Label)e.Row.FindControl("lblMay"),
    (Label)e.Row.FindControl("lblJune"),
    (Label)e.Row.FindControl("lblJuly"),
    (Label)e.Row.FindControl("lblAugust"),
    (Label)e.Row.FindControl("lblSeptember"),
    (Label)e.Row.FindControl("lblOctober"),
    (Label)e.Row.FindControl("lblNovember"),
    (Label)e.Row.FindControl("lblDecember")
                    };

                    int baseIndex = 4;

                    // 🔹 Step 1: Feb & Mar → Jan se fill
                    for (int i = 1; i <= 2 && i <= currentMonthIndex; i++)
                    {
                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        if (val == 0)
                        {
                            months[i].Text = months[0].Text;
                        }
                    }

                    // 🔹 Step 2: Color only till current month (<= condition)
                    for (int i = 0; i <= currentMonthIndex && i < months.Length; i++)
                    {

                        int val = string.IsNullOrEmpty(months[i].Text) ? 0 : Convert.ToInt32(months[i].Text);

                        row.Cells[baseIndex + i].BackColor =
                            (val <= targetvalMonthly)
                            ? System.Drawing.Color.LightGreen
                            : System.Drawing.Color.OrangeRed;
                    }

                    // 🔹 Step 3: Future months → NO COLOR (remove light gray issue)
                    for (int i = currentMonthIndex + 1; i < months.Length; i++)
                    {
                        row.Cells[baseIndex + i].BackColor = System.Drawing.Color.Transparent;
                    }

                    // 🔹 Step 4: Cumulative (<= condition)
                    int cumVal = string.IsNullOrEmpty(lblCum.Text) ? 0 : Convert.ToInt32(lblCum.Text);
                    int targetVal = string.IsNullOrEmpty(lblTarget.Text) ? 0 : Convert.ToInt32(lblTarget.Text);

                    row.Cells[baseIndex + months.Length].BackColor =
                        (cumVal <= targetVal)
                        ? System.Drawing.Color.GreenYellow
                        : System.Drawing.Color.Red;
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
        // Required for GridView PDF export
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        BindMetrics();

        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=NAAC_Metric_Color_Report.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Document doc = new Document(PageSize.A4.Rotate(), 10f, 10f, 20f, 10f);
        PdfWriter.GetInstance(doc, Response.OutputStream);
        doc.Open();

        Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
        Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, Color.WHITE);
        Font bodyFont = FontFactory.GetFont(FontFactory.HELVETICA, 8);

        // ===== TITLE =====
        Paragraph title = new Paragraph("NAAC Metric Monthly Progress Report", titleFont);
        title.Alignment = Element.ALIGN_CENTER;
        title.SpacingAfter = 10;
        doc.Add(title);

        // ===== TABLE (17 Columns) =====
        PdfPTable table = new PdfPTable(17);
        table.WidthPercentage = 100;
        table.HeaderRows = 1;

        table.SetWidths(new float[]
        {
        3,5,18,5,
        4,4,4,4,4,4,4,4,4,4,4,4,
        5
        });

        // ===== HEADER =====
        string[] headers = {
        "S.No","Metric No","Metric","Target",
        "Jan","Feb","Mar","Apr","May","Jun",
        "Jul","Aug","Sep","Oct","Nov","Dec","Cumulative"
    };

        foreach (string h in headers)
            table.AddCell(HeaderCell(h, headerFont));

        // ===== DATA =====
        int sno = 1;
        foreach (GridViewRow row in JainStudentList.Rows)
        {
            bool evenRow = sno % 2 == 0;
            Color rowBg = evenRow ? new Color(240, 248, 255) : Color.WHITE;

            int target = SafeInt(GetText(row, "lblTarget"));
            int monthlyTarget = target > 0 ? target / 12 : 0;

            AddDataCell(table, sno++.ToString(), bodyFont, rowBg, Element.ALIGN_CENTER);
            AddDataCell(table, GetText(row, "lblMetricNo"), bodyFont, rowBg);
            AddDataCell(table, GetText(row, "lblMetric"), bodyFont, rowBg);
            AddDataCell(table, target.ToString(), bodyFont, rowBg, Element.ALIGN_CENTER);

            // Months
            AddMonthCell(table, row, "lblJanuary", monthlyTarget);
            AddMonthCell(table, row, "lblFebruary", monthlyTarget);
            AddMonthCell(table, row, "lblMarch", monthlyTarget);
            AddMonthCell(table, row, "lblApril", monthlyTarget);
            AddMonthCell(table, row, "lblMay", monthlyTarget);
            AddMonthCell(table, row, "lblJune", monthlyTarget);
            AddMonthCell(table, row, "lblJuly", monthlyTarget);
            AddMonthCell(table, row, "lblAugust", monthlyTarget);
            AddMonthCell(table, row, "lblSeptember", monthlyTarget);
            AddMonthCell(table, row, "lblOctober", monthlyTarget);
            AddMonthCell(table, row, "lblNovember", monthlyTarget);
            AddMonthCell(table, row, "lblDecember", monthlyTarget);

            // Cumulative
            int cumulative = SafeInt(GetText(row, "lblCumulative"));
            Color cumColor = cumulative >= target ? new Color(0, 100, 0) : new Color(178, 34, 34);

            PdfPCell cumCell = new PdfPCell(new Phrase(cumulative.ToString(),
                              FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, cumColor)));
            cumCell.HorizontalAlignment = Element.ALIGN_CENTER;
            cumCell.Padding = 4;
            table.AddCell(cumCell);
        }

        doc.Add(table);
        doc.Close();
        Response.End();



    }


    private PdfPCell HeaderCell(string text, Font font)
    {
        PdfPCell cell = new PdfPCell(new Phrase(text, font));
        cell.BackgroundColor = new Color(0, 102, 204);
        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Padding = 5;
        return cell;
    }
    private void AddDataCell(PdfPTable table, string text, Font font, Color bg, int align = Element.ALIGN_LEFT)
    {
        PdfPCell cell = new PdfPCell(new Phrase(text, font));
        cell.BackgroundColor = bg;
        cell.HorizontalAlignment = align;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Padding = 4;
        table.AddCell(cell);
    }

    private void AddMonthCell(PdfPTable table, GridViewRow row, string lblId, int target)
    {
        int val = SafeInt(GetText(row, lblId));
        Color color = val >= target ? new Color(0, 128, 0) : new Color(200, 0, 0);

        PdfPCell cell = new PdfPCell(new Phrase(val.ToString(),
            FontFactory.GetFont(FontFactory.HELVETICA, 8, color)));

        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        cell.Padding = 4;
        table.AddCell(cell);
    }

    private int SafeInt(string val, int defaultValue = 0)
    {
        int result;
        return int.TryParse(val, out result) ? result : defaultValue;
    }

    private string GetText(GridViewRow row, string id)
    {
        Label lbl = row.FindControl(id) as Label;
        return lbl != null ? lbl.Text.Trim() : "0";
    }
}