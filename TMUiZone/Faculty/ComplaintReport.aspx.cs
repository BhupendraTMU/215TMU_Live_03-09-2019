using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text;
using System.Drawing;


public partial class Faculty_ComplaintReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                HostelList();
                ComplaintList(drpHostel.SelectedItem.Text);

            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../Default.aspx");
        }
    }
    public void HostelList()
    {
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("Customer_GetHostelList '" + Session["uid"].ToString() + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpHostel.DataSource = dt;
        drpHostel.DataTextField = "Name";
        drpHostel.DataValueField = "Code";
        drpHostel.DataBind();

    }
    public void ComplaintList(string Hostel)
    {
        if (Request.Browser.IsMobileDevice)
        {
            OrderList1.Width = 380;
        }
        con1.Open();
        SqlDataAdapter da = new SqlDataAdapter("Customer_GetComplaintReport '" + Session["uid"].ToString() + "','" + Hostel + "'", con1);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            OrderList1.DataSource = dt;
            OrderList1.DataBind();
            if (OrderList1.Rows.Count > 0)
            {
                OrderList1.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                OrderList1.HeaderRow.Cells[1].Attributes["data-class"] = "phone";
                OrderList1.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.Cells[5].Attributes["data-class"] = "phone";
                //GridView1.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        else
        {
            OrderList1.DataSource = "";
            OrderList1.DataBind();
        }


    }


    public void bindComplaintDetail(string Department, string Status, string Hostel)
    {
        if (Request.Browser.IsMobileDevice)
        {
            GridView1.Width = 380;
        }
        con1.Open();
        DataTable dt = new DataTable();

        SqlDataAdapter da = new SqlDataAdapter("Customer_GetComplaintListWithDepartmentStatus '" + Department + "','" + Status + "','" + Hostel + "'", con1);

        da.Fill(dt);


        if (dt.Rows.Count > 0)
        {
            Orderlist.Visible = false;
            main.Visible = true;

            GridView1.DataSource = dt;
            GridView1.DataBind();
            if (GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.Cells[0].Attributes["data-class"] = "expand";


                GridView1.HeaderRow.Cells[1].Attributes["data-class"] = "phone";
                GridView1.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[5].Attributes["data-class"] = "phone";
                //GridView1.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[7].Attributes["data-hide"] = "phone";

                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }


    }



    protected void lnkT_Click(object sender, EventArgs e)
    {

        GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
        string Department = clickedRow.Cells[1].Text;


        bindComplaintDetail(Department, "9", drpHostel.SelectedItem.Text);






    }
    protected void lnkPW_Click(object sender, EventArgs e)
    {
        GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
        string Department = clickedRow.Cells[1].Text;


        bindComplaintDetail(Department, "1", drpHostel.SelectedItem.Text);
        //bindComplaintDetail();

    }
    protected void lnkPD_Click(object sender, EventArgs e)
    {
        GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
        string Department = clickedRow.Cells[1].Text;


        bindComplaintDetail(Department, "2", drpHostel.SelectedItem.Text);


    }
    protected void lnkRD_Click(object sender, EventArgs e)
    {
        GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
        string Department = clickedRow.Cells[1].Text;


        bindComplaintDetail(Department, "3", drpHostel.SelectedItem.Text);


    }
    protected void lnkResolved_Click(object sender, EventArgs e)
    {
        GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
        string Department = clickedRow.Cells[1].Text;


        bindComplaintDetail(Department, "4", drpHostel.SelectedItem.Text);


    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

        Response.Redirect("ComplaintReport.aspx");

    }
    protected void lnkClose_Click(object sender, EventArgs e)
    {
        GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
        string Department = clickedRow.Cells[1].Text;


        bindComplaintDetail(Department, "5", drpHostel.SelectedItem.Text);
    }

    protected void drpHostel_SelectedIndexChanged(object sender, EventArgs e)
    {
        Orderlist.Visible = true;
        main.Visible = false;
        ComplaintList(drpHostel.SelectedItem.Text);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {

        if (main.Visible == true)
        {
            GridView1.AllowPaging = false;
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();

            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            GridView1.RenderControl(htmlWrite);

            Response.Clear();
            string str = "StudentComplaintReport_"+drpHostel.SelectedItem.Text+"_"+ DateTime.Now.ToString() + "" ;
            Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

            Response.Charset = "";

            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Response.ContentType = "application/vnd.xls";

            Response.Write(stringWrite.ToString());


            Response.End();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('No Data Found');", true);
            return;
        }
    }
}