using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using Utility;
using System.Configuration;


public partial class Faculty_EmployeeCountHODWise : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal con;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["uid"].ToString() == "TMU00049" || Session["uid"].ToString() == "TMU00075" || Session["uid"].ToString() == "TMU08719")
            {
                Portalcon = new Connection();
                con = new ServicePoratal();
                showLeaveBlance();
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Default.aspx");
        }

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {

    }
   
    string tble_Pay_Employee_Leave_Entitled = "";
    public void showLeaveBlance()
    {

      
            SqlDataAdapter da = new SqlDataAdapter("select [HOD Name],HOD,[Employee Count] from (select count(No_) 'Employee Count',HOD,(Select distinct [First Name] from [TMU$Employee] where No_=T.HOD) 'HOD Name' from [TMU$Employee] T where Status=0 and HOD!='' group by HOD ) T group by T.[HOD Name],T.HOD,T.[Employee Count]", Portalcon.Con);
            DataTable dt = new DataTable();

            da.Fill(dt);
            Portalcon.DisConnect();




            grdData.DataSource = dt;
            grdData.DataBind();
        

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        grdData.AllowPaging = false;

        //showLeaveBlance();


        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grdData.RenderControl(htmlWrite);

        Response.Clear();
        string str = "EmployeeCountHODWise";
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();
    }
    int qtyTotal = 0;
    protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label qty = e.Row.FindControl("lblqty") as Label;

            int tmpTotal =Convert.ToInt32 (qty.Text);
            qtyTotal += tmpTotal;
           
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalqty = (Label)e.Row.FindControl("lblTotalqty");
            lblTotalqty.Text = "TOTAL :"+ qtyTotal.ToString();
        }
    }
}