using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Faculty_FacultyFeedback : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            bindGrid();
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DateTime dt = new DateTime();
        dt = Convert.ToDateTime(txtDateCommited.Text);
        con.Open();
        SqlCommand cmd = new SqlCommand("insert into TMU$Feedback(StudentNo,Date,FeedbackFor,Remarks,Type) values('" + Session["uid"].ToString() + "','" + dt.ToString("MM-dd-yyyy") + "','" + txtFeedbackFor.Text + "','" + txtRemarks.Text + "','FACULTY')", con);
        cmd.ExecuteNonQuery();
        bindGrid();
        con.Close();
        Blank();
    }
    public void Blank()
    {
        txtDateCommited.Text = "";
        txtFeedbackFor.Text = "";
        txtRemarks.Text = "";
    }
    protected void grdFeedbackReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdFeedbackReport.PageIndex = e.NewPageIndex;
        bindGrid();
    }
    public void bindGrid()
    {
        SqlCommand cmd = new SqlCommand("select *,replace(convert(NVARCHAR, Date, 106), ' ', '-') as Date1 from TMU$Feedback where StudentNo='" + Session["uid"].ToString() + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        grdFeedbackReport.DataSource = dt;
        grdFeedbackReport.DataBind();
    }
}