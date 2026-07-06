using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI;
using System.Drawing;


public partial class Faculty_NAACSurveyReport : System.Web.UI.Page
{
    string ConStr = ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["uid"].ToString() != "TMU02982" &&
    Session["uid"].ToString() != "TMU08617")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key",
                    "alert('Access denied'); document.location.href='FacultyDetails.aspx';", true);
            }
           
            bindAcademicYear();
            BindQuestions();
            string ay = ddlAcademicYear.SelectedValue; // 25-26

            string fullAY = "20" + ay.Substring(0, 2) + "-" + ay.Substring(3, 2);

            lblAcademicYear.Text = "(A.Y. " + fullAY + ")";
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        string ay = ddlAcademicYear.SelectedValue; // 25-26

        string fullAY = "20" + ay.Substring(0, 2) + "-" + ay.Substring(3, 2);

        lblAcademicYear.Text = "(A.Y. " + fullAY + ")";
        BindQuestions();
    }
    public void bindAcademicYear()
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
    private void BindQuestions()
    {
        using (SqlConnection con = new SqlConnection(ConStr))
        {
            string qry = @"
        SELECT DISTINCT
               QuestionID,
               Question
        FROM tbl_NAACSurveyInput
        WHERE QuestionID < 21 ";

            if (!string.IsNullOrEmpty(ddlAcademicYear.SelectedValue))
            {
                qry += " AND [Academic Year] = @AcademicYear ";
            }

            qry += " ORDER BY QuestionID ";

            SqlCommand cmd = new SqlCommand(qry, con);

            if (!string.IsNullOrEmpty(ddlAcademicYear.SelectedValue))
            {
                cmd.Parameters.AddWithValue("@AcademicYear",
                    ddlAcademicYear.SelectedValue);
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            rptQuestions.DataSource = dt;
            rptQuestions.DataBind();
        }
    }

    protected void rptQuestions_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
        e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;

            int QuestionID = Convert.ToInt32(drv["QuestionID"]);

            Repeater rptOptions =
                (Repeater)e.Item.FindControl("rptOptions");

            BindQuestionOptions(QuestionID, rptOptions);

            GridView gvSummary =
                (GridView)e.Item.FindControl("gvSummary");

            Chart chart1 =
                (Chart)e.Item.FindControl("Chart1");

            BindQuestionSummary(QuestionID, gvSummary, chart1);
        }
    }
    private void BindQuestionOptions(int QuestionID, Repeater rpt)
    {
        using (SqlConnection con = new SqlConnection(ConStr))
        {
            string qry = @"
        SELECT
            OptionValue,
            OptionDescription
        FROM tbl_NAACSurveyQuestionOptions
        WHERE QuestionID = @QuestionID
        ORDER BY OptionValue DESC";

            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@QuestionID", QuestionID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            rpt.DataSource = dt;
            rpt.DataBind();
        }
    }

    private void BindQuestionSummary(
        int QuestionID,
        GridView gv,
        Chart chart)
    {
        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(ConStr))
        {
            string qry = @"
                SELECT
                    SUM(ISNULL(TRY_CAST(Option5 AS INT),0)) AS Excellent,
                    SUM(ISNULL(TRY_CAST(Option4 AS INT),0)) AS Better,
                    SUM(ISNULL(TRY_CAST(Option3 AS INT),0)) AS Good,
                    SUM(ISNULL(TRY_CAST(Option2 AS INT),0)) AS Satisfactory,
                    SUM(ISNULL(TRY_CAST(Option1 AS INT),0)) AS Poor
                FROM tbl_NAACSurveyInput 
                WHERE QuestionID=@QuestionID";
            if (!string.IsNullOrEmpty(ddlAcademicYear.SelectedValue))
            {
                qry += " AND [Academic Year] = @AcademicYear ";
            }

            qry += " GROUP BY QuestionID ORDER BY QuestionID ";

            SqlCommand cmd = new SqlCommand(qry, con);

            if (!string.IsNullOrEmpty(ddlAcademicYear.SelectedValue))
            {
                cmd.Parameters.AddWithValue("@AcademicYear",
                    ddlAcademicYear.SelectedValue);
            }






            cmd.Parameters.AddWithValue("@QuestionID", QuestionID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        }

        if (dt.Rows.Count == 0)
            return;

        DataTable summary = new DataTable();

        summary.Columns.Add("Choice");
        summary.Columns.Add("Responses");

        summary.Rows.Add("4- Excellent", dt.Rows[0]["Excellent"]);
        summary.Rows.Add("3- Better", dt.Rows[0]["Better"]);
        summary.Rows.Add("2- Good", dt.Rows[0]["Good"]);
        summary.Rows.Add("1- Satisfactory", dt.Rows[0]["Satisfactory"]);
        summary.Rows.Add("0- Poor", dt.Rows[0]["Poor"]);

        gv.DataSource = summary;
        gv.DataBind();

        chart.Series.Clear();

        chart.Width = 900;
        chart.Height = 450;
        chart.BackColor = Color.White;

        ChartArea ca = chart.ChartAreas[0];

        ca.Area3DStyle.Enable3D = true;
        ca.Area3DStyle.Inclination = 25;
        ca.Area3DStyle.Rotation = 15;
        ca.Area3DStyle.PointDepth = 80;
        ca.Area3DStyle.PointGapDepth = 0;
        ca.Area3DStyle.IsClustered = true;
        ca.Area3DStyle.WallWidth = 0;

        // Pie ko bada dikhao lekin chart control nahi
        ca.Position.X = 2;
        ca.Position.Y = 2;
        ca.Position.Width = 78;
        ca.Position.Height = 92;

        ca.InnerPlotPosition.X = 5;
        ca.InnerPlotPosition.Y = 5;
        ca.InnerPlotPosition.Width = 88;
        ca.InnerPlotPosition.Height = 85;

        Legend lg = chart.Legends[0];
        lg.Docking = Docking.Right;
        lg.Alignment = StringAlignment.Center;
        lg.Title = "Option Value / Percentage";
        lg.Font = new Font("Arial", 10, FontStyle.Bold);
        lg.IsTextAutoFit = false;

        Series s = new Series("Feedback");
        s.ChartType = SeriesChartType.Pie;

        s.Points.AddXY("85 to 100%", Convert.ToInt32(dt.Rows[0]["Excellent"]));
        s.Points.AddXY("70 to 84%", Convert.ToInt32(dt.Rows[0]["Better"]));
        s.Points.AddXY("55 to 69%", Convert.ToInt32(dt.Rows[0]["Good"]));
        s.Points.AddXY("30 to 54%", Convert.ToInt32(dt.Rows[0]["Satisfactory"]));
        s.Points.AddXY("Below 30%", Convert.ToInt32(dt.Rows[0]["Poor"]));

        foreach (DataPoint p in s.Points)
        {
            p.Label = "#PERCENT{P1}";
            p.LegendText = p.AxisLabel;
            p.Font = new Font("Arial", 10, FontStyle.Bold);
        }

        chart.Series.Add(s);
    }
}