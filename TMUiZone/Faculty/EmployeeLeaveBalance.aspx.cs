using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_EmployeeLeaveBalance : System.Web.UI.Page
{
    //SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["uid"].ToString() == "TMU00133" || Session["uid"].ToString() == "TMU00141" || Session["uid"].ToString() == "TMU00074")
            {
                getleavebalance();
                pnlReport.Visible = true;
            }
            else
            {
                pnlmsg.Visible = true;
                pnlReport.Visible = false;
            }
        }
    }
    public void getleavebalance()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        SqlCommand cmd = new SqlCommand("pro_LeaveBalance", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdleavebalancelist.DataSource = dtCL;
        grdleavebalancelist.DataBind();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void btnexporttoexel_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        grdleavebalancelist.RenderControl(htmlWrite);
        Response.Clear();
        string str = "EmployeeLeaveBalance" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        Response.Write(stringWrite.ToString());
        Response.End();
    }




    protected void Search_Click(object sender, EventArgs e)
    {
        if (txtemployeecode.Text == "")
        {
            string message1 = "Please Fill Employee Code.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;

        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        SqlCommand cmd = new SqlCommand("pro_getleavesearch", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeCode", txtemployeecode.Text));
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable(); ;
        daCL.Fill(dtCL);
        grdleavebalancelist.DataSource = dtCL;
        grdleavebalancelist.DataBind();
        
    }

    protected void grdleavebalancelist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdleavebalancelist.PageIndex = e.NewPageIndex;
        getleavebalance();
    }
}
