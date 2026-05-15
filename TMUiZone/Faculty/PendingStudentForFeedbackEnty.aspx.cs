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



public partial class Faculty_PendingStudentForFeedbackEnty : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["uid"] != null)
            {
                if (Session["uid"].ToString() == "TMU02982" || Session["uid"].ToString() == "TMU08617" || Session["UserGroup"].ToString() == "REGISTRAR" || Session["UserGroup"].ToString() == "PRINCIPAL" || Session["UserGroup"].ToString() == "HOD" || Session["UserGroup"].ToString() == "FACULTY")
                {
                    btnExport.Visible = true;
                    if (!IsPostBack)
                    {
                        if (Session["UserGroup"].ToString() == "PRINCIPAL" || Session["UserGroup"].ToString() == "HOD" || Session["UserGroup"].ToString() == "FACULTY")
                        {

                            bindAcademicYear();
                            bindcollege();
                        }

                        else
                        {
                            lblcolloege.Visible = true; ddCollege.Visible = true;
                            bindAcademicYear();
                            bindcollege();
                        }

                    }
                }
                else
                { Response.Redirect("../Default.aspx"); }
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
            //SqlCommand cmd = new SqlCommand("Sp_PendingStudentForFeedbackEntry", con);[Sp_PendingStudentForFeedbackEntry_OddEvenYear]
            SqlCommand cmd = new SqlCommand("Sp_PendingStudentForFeedbackEntry_OddEvenYear", con);           
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CollegeCode", str);
            cmd.Parameters.AddWithValue("@OddEvenYear", ddsem.SelectedValue);
            cmd.Parameters.AddWithValue("@AcademicYear", ddlAcademicYear.SelectedValue);
            cmd.Parameters.AddWithValue("@SemYear", txtSearch.Text.Trim());

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
        
    }
    public void bindAcademicYear()
    {
        using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            ddlAcademicYear.DataSource = dt1;
            ddlAcademicYear.DataTextField = "Details";
            ddlAcademicYear.DataValueField = "No_";
            ddlAcademicYear.DataBind();
        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        if (Session["UserGroup"].ToString() == "PRINCIPAL" || Session["UserGroup"].ToString() == "HOD" || Session["UserGroup"].ToString() == "FACULTY") 
        {
            bindresult(Session["GlobalDimension1Code"].ToString());
             
        }
        else
        {
            bindresult(ddCollege.SelectedValue);
        }
    }
}