using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Drawing;
using System.Web.Services;
using System.Web.Script.Services;
using Utility;



public partial class Faculty_PendingStudentForFeedbackEnty_OLD : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["uid"] != null)
            {
                if (Session["UserGroup"].ToString() == "HR" || Session["UserGroup"].ToString() == "REGISTRAR")
                {
                    btnExport.Visible = true;
                    lblcolloege.Visible = true;
                    ddCollege.Visible = true;
                    if (!IsPostBack)
                    {
                        bindcollege();
                    }
                }
                bindresult(Session["GlobalDimension1Code"].ToString());

            }
        }
        catch
        {
            Response.Redirect("../Default.aspx");

        }

    }

    public void bindcollege()
    {
        using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("select Code +' -' +Name as Details,Code from [TMU$Dimension Value] where [Dimension Code]='COLLEGE' and [Active College]='1' order by Details", Conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            ddCollege.DataSource = dt1;
            ddCollege.DataTextField = "Details";
            ddCollege.DataValueField = "Code";
            ddCollege.DataBind();
            ddCollege.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));
        }
    }

    public void bindresult(string str)
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Sp_PendingStudentForFeedbackEntry", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CollegeCode", str);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grdPending.DataSource = dt;
            grdPending.DataBind();
            //Session["grd"] = dt;
        }
    }

    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        Response.Clear(); Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=PendingFeedback.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            if (ddCollege.Visible == true) { bindresult(ddCollege.SelectedValue); }
            //To Export all pages
           // DataTable dtExport = new DataTable();
           // dtExport = (DataTable)Session["grd"];

           // grdPending.DataSource = dtExport;
            //grdPending.DataBind();

            grdPending.HeaderRow.BackColor = Color.YellowGreen;

            foreach (TableCell cell in grdPending.HeaderRow.Cells)
            {
                cell.BackColor = grdPending.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grdPending.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grdPending.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grdPending.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grdPending.RenderControl(hw);
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }

    protected void ddCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindresult(ddCollege.SelectedValue);
    }
}