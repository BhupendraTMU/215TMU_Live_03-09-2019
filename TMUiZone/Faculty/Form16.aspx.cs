using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_Form16 : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                getForm16();
                //BindGridview();
            }
            catch
            {
                Response.Redirect("../Default.aspx");
            }
        }
    }
    public void getForm16()
    {

        SqlCommand cmd = new SqlCommand("Pro_getform16", con);
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        //cmd.Parameters.Add("@AcademicSession", drpacsession.SelectedValue);
        cmd.CommandType = CommandType.StoredProcedure;
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdmemberapprovallist.DataSource = dtCL;
        grdmemberapprovallist.DataBind();
    }
    
  

    //protected void btnGetFiles_Click(object sender, EventArgs e)
    //{
    //    BindGridview();
    //    getForm16();
    //}
    // Bind Data to Gridview
    protected void BindGridview()
    {
        getForm16();
        string[] filesPath = Directory.GetFiles(Server.MapPath("~/AllForm16/"));
        List<ListItem> files = new List<ListItem>();
        foreach (string path in filesPath)
        {
            files.Add(new ListItem(Path.GetFileName(path)));

        }
        gvDetails.DataSource = files;
        gvDetails.DataBind();

    }


    //protected void btndowload_Click(object sender, EventArgs e)
    //{

    //    Response.ContentType = "Application/pdf";
    //    Response.AppendHeader("Content-Disposition", "attachment; filename=AAEPQ0452E.pdf");
    //    Response.TransmitFile(Server.MapPath("~/AllForm16/AAEPQ0452E.pdf"));
    //    Response.End();
    //}

    protected void LinkButtonDownloadPdf_Click(object sender, EventArgs e)
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("select [PAN No] from [TMU$Employee] where No_='" + Session["uid"].ToString() + "'", con))
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string PanNo = dr["PAN No"].ToString();
                    con.Close();
                    if (PanNo != "")
                    {
                        try
                        {

                            System.Net.WebClient wc = new System.Net.WebClient();
                            wc.OpenRead(Server.MapPath("~/AllForm16/" + PanNo + ".pdf"));
                            Int64 bytes_total = Convert.ToInt64(wc.ResponseHeaders["Content-Length"]);
                            Response.ContentType = "Application/pdf";
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + PanNo + ".pdf");
                            Response.TransmitFile(Server.MapPath("~/AllForm16/" + PanNo + ".pdf"));
                            string Size = Response.OutputStream.Length.ToString();
                            Response.End();
                        }
                        catch (Exception ex)
                        {
                            string message1 = "No Data Found.";
                            string script1 = "window.onload = function(){ alert('";
                            script1 += message1;
                            script1 += "')};";
                            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
                            return;
                        }
                    }
                }
            }
        }
    }
    protected void drpacsession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpacsession.SelectedValue == "1")
        {
            grdmemberapprovallist.Columns[7].Visible = true;
            grdmemberapprovallist.Columns[8].Visible = false;
            grdmemberapprovallist.Columns[9].Visible = false;
        }
        else if (drpacsession.SelectedValue == "2")
        {
            grdmemberapprovallist.Columns[8].Visible = true;
            grdmemberapprovallist.Columns[7].Visible = false;
            grdmemberapprovallist.Columns[9].Visible = false;
        }
        else if (drpacsession.SelectedValue == "3")
        {
            grdmemberapprovallist.Columns[9].Visible = true;
            grdmemberapprovallist.Columns[7].Visible = false;
            grdmemberapprovallist.Columns[8].Visible = false;
        }
        else if (drpacsession.SelectedValue == "4")
        {
            grdmemberapprovallist.Columns[10].Visible = true;
            grdmemberapprovallist.Columns[7].Visible = false;
            grdmemberapprovallist.Columns[8].Visible = false;
            grdmemberapprovallist.Columns[9].Visible = false;
        }
        else
        {
            grdmemberapprovallist.Columns[9].Visible = false;
            grdmemberapprovallist.Columns[7].Visible = false;
            grdmemberapprovallist.Columns[8].Visible = false;
        }
    }
    protected void LinkButtonDownloadPdf23_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("select [PAN No] from [TMU$Employee] where No_='" + Session["uid"].ToString() + "'", con))
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string PanNo = dr["PAN No"].ToString();
                    con.Close();
                    if (PanNo != "")
                    {
                        try
                        {
                            System.Net.WebClient wc = new System.Net.WebClient();
                           

                            wc.OpenRead(Server.MapPath("~/AllForm16/F16FY202223/" + PanNo + ".pdf"));
                            Int64 bytes_total = Convert.ToInt64(wc.ResponseHeaders["Content-Length"]);
                            Response.ContentType = "Application/pdf";
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + PanNo + ".pdf");
                            Response.TransmitFile(Server.MapPath("~/AllForm16/F16FY202223/" + PanNo + ".pdf"));
                            string Size = Response.OutputStream.Length.ToString();
                            Response.End();
                        }
                        catch (Exception ex)
                        {
                            string message1 = "No Data Found.";
                            string script1 = "window.onload = function(){ alert('";
                            script1 += message1;
                            script1 += "')};";
                            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
                            return;
                        }
                    }
                }
            }
        }
    }
    protected void LinkButtonDownloadPdf24_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("select [PAN No] from [TMU$Employee] where No_='" + Session["uid"].ToString() + "'", con))
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string PanNo = dr["PAN No"].ToString();
                    con.Close();
                    if (PanNo != "")
                    {
                        try
                        {
                            System.Net.WebClient wc = new System.Net.WebClient();
                            wc.OpenRead(Server.MapPath("~/AllForm16/F16 FY202324/" + PanNo + ".pdf"));
                            Int64 bytes_total = Convert.ToInt64(wc.ResponseHeaders["Content-Length"]);
                            Response.ContentType = "Application/pdf";
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + PanNo + ".pdf");
                            Response.TransmitFile(Server.MapPath("~/AllForm16/F16 FY202324/" + PanNo + ".pdf"));
                            string Size = Response.OutputStream.Length.ToString();
                            Response.End();
                        }
                        catch (Exception ex)
                        {
                            string message1 = "No Data Found.";
                            string script1 = "window.onload = function(){ alert('";
                            script1 += message1;
                            script1 += "')};";
                            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
                            return;
                        }
                    }
                }
            }
        }
    }

    protected void LinkButtonDownloadPdf25_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("select [PAN No] from [TMU$Employee] where No_='" + Session["uid"].ToString() + "'", con))
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string PanNo = dr["PAN No"].ToString();
                    con.Close();
                    if (PanNo != "")
                    {
                        try
                        {
                            System.Net.WebClient wc = new System.Net.WebClient();
                            wc.OpenRead(Server.MapPath("~/AllForm16/F16FY202425/" + PanNo + ".pdf"));
                            Int64 bytes_total = Convert.ToInt64(wc.ResponseHeaders["Content-Length"]);
                            Response.ContentType = "Application/pdf";
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + PanNo + ".pdf");
                            Response.TransmitFile(Server.MapPath("~/AllForm16/F16FY202425/" + PanNo + ".pdf"));
                            string Size = Response.OutputStream.Length.ToString();
                            Response.End();
                        }
                        catch (Exception ex)
                        {
                            string message1 = "No Data Found.";
                            string script1 = "window.onload = function(){ alert('";
                            script1 += message1;
                            script1 += "')};";
                            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
                            return;
                        }
                    }
                }
            }
        }
    }
}

