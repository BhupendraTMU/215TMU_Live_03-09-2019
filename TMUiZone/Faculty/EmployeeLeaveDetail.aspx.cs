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

public partial class Faculty_EmployeeLeaveDetail : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal con;
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Session["uid"].ToString() == "TMU00049" || Session["uid"].ToString() == "TMU00075")
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
        showLeaveBlance();
    }
    string tble_Pay_Employee_Leave_Entitled = "";
    public void showLeaveBlance()
    {

        if (txtEmployee.Text == "")
        {

            SqlDataAdapter da = new SqlDataAdapter("select PEL.[Employee Code] ,E.[First Name] as 'Employee_name', PEL.[Leave code],PEL.[Leave Balance],PEL.[Unapproved Leave]  from [TMU$Pay Employee Leave Entitled] PEL inner join [TMU$Employee] E on PEL.[Employee Code]=E.No_ where E.Status=0  order by [Employee Code]", Portalcon.Con);
            DataTable dt = new DataTable();

            da.Fill(dt);
            Portalcon.DisConnect();




            grdleave.DataSource = dt;
            grdleave.DataBind();
        }
        else
        {
            SqlDataAdapter da = new SqlDataAdapter("select PEL.[Employee Code] ,E.[First Name] as 'Employee_name', PEL.[Leave code],PEL.[Leave Balance],PEL.[Unapproved Leave]  from [TMU$Pay Employee Leave Entitled] PEL inner join [TMU$Employee] E on PEL.[Employee Code]=E.No_ where E.Status=0 and PEL.[Employee Code]='"+txtEmployee.Text.ToUpper()+"' order by [Employee Code]", Portalcon.Con);
            DataTable dt = new DataTable();

            da.Fill(dt);
            Portalcon.DisConnect();




            grdleave.DataSource = dt;
            grdleave.DataBind();
        }
       
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        grdleave.AllowPaging = false;

        showLeaveBlance();


        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grdleave.RenderControl(htmlWrite);

        Response.Clear();
        string str = "EmployeeLeaveReport";
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();
    }
    protected void grdleave_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Current leave status";
            HeaderCell.ColumnSpan = 5;
            HeaderGridRow.Cells.Add(HeaderCell);
          


            HeaderCell.CssClass = "HeaderStyleofgrid";
            HeaderGridRow.Cells.Add(HeaderCell);
          

            grdleave.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
}