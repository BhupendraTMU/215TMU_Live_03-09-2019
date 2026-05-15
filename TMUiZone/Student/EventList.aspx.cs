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

public partial class Faculty_EventList : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserGroup"].ToString() == "PRINCIPAL" || Session["uid"].ToString() == "TMU01005" || Session["UserGroup"].ToString() == "REGISTRAR" || Session["UserGroup"].ToString() == "STUDENT")
            {
                if (!IsPostBack)
                {
                    txtFromtDate.Text = System.DateTime.Now.ToString("dd MMM yyyy");
                    txtToDate.Text = System.DateTime.Now.AddYears(1).ToString("dd MMM yyyy");       
                    bindcollegedrp();
                    BinddlEventType();
                    if (Session["UserGroup"].ToString() == "STUDENT")
                    {
                        tblSearch.Visible = false;
                    }
                    if (Session["UserGroup"].ToString() == "REGISTRAR")
                    {
                        ddlcollege.Enabled = true;
                    }
                    else
                    {
                        if (Session["UserGroup"].ToString() == "STUDENT")
                        {
                            ddlcollege.SelectedValue = Session["College"].ToString();
                        }
                        else
                        {
                            ddlcollege.SelectedValue = Session["GlobalDimension1Code"].ToString();
                        }
                    }
                    BindGrid();
                }
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
    public void BinddlEventType()
    {
        string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {

            SqlCommand cmd = new SqlCommand("Sp_getEventType", con); //ok
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddleventl.DataSource = dt;
            ddleventl.DataTextField = "Details";
            ddleventl.DataValueField = "Value";
            ddleventl.DataBind();
        }
    }

    private void BindGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_EventList", con))
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                if (ddlcollege.SelectedIndex == 0) { cmd.Parameters.Add("@College", ""); }
                else { cmd.Parameters.Add("@College", ddlcollege.SelectedValue); }
                cmd.Parameters.Add("@fromDate", txtFromtDate.Text);
                cmd.Parameters.Add("@toDate", txtToDate.Text);
                cmd.Parameters.Add("@EventType", ddleventl.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    if (dt.Rows.Count > 0)
                    {
                        DateTime dt1 = Convert.ToDateTime(dt.Rows[0]["Date"]);
                        lblEvent.Text = dt.Rows[0]["Event"].ToString() + " ( " + dt1.ToString("dd MMM yyyy") + " )";
                    }
                }
            }
        }


       
    }


    public void bindcollegedrp()
    {
        using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("select Code, (Code +' - '+ Name) as BranchName from [TMU$Dimension Value] where [Dimension Code]='COLLEGE' and [Active College]='1'", Conn);

            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            ddlcollege.DataSource = dt1;
            ddlcollege.DataTextField = "BranchName";
            ddlcollege.DataValueField = "Code";
            ddlcollege.DataBind();
            ddlcollege.Items.Insert(0,"-- Select --");
            //drpcollege.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));
        }
    }



    protected void BtnShow_Click(object sender, EventArgs e)
    {
        this.BindGrid();
    }


   // for  image view popup

    private void BindGridpop()
    {
        string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Sp_ValidateAndGetImage"); 
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Code", lblpop.Text);
            cmd.Connection = con;
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);
               GridViewpop.DataSource = dt;
                GridViewpop.DataBind();
            }
        }
    }

    
    protected void Btnpop_Click(object sender, EventArgs e)
    {

           LinkButton btndetails = sender as LinkButton;
            GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
            lblpop.Text = GridView1.DataKeys[gvrow.RowIndex].Value.ToString();
            this.ModalPopupExtender1.Show();
            this.BindGridpop();
    }

    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
        ModalPopupExtender1.Show();
    }
    protected void lnkDownload2_Click(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();


        ModalPopupExtender1.Show();

    }
    protected void lnkDownload3_Click(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(Server.MapPath( filePath));
        Response.End();


        ModalPopupExtender1.Show();
    }
}