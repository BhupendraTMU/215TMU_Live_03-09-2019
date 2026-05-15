using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Drawing;
using System.IO;

public partial class Faculty_courier : System.Web.UI.Page
{
    Connection con1;
    ServicePoratal PortalCon;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Session["uid"].ToString() != "TMU04442")
            {
                Response.Redirect("Error.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }

            ShowDepartment();
            bindGrid();
        }
    }

    public void bindGrid()
    {
        SqlCommand cmd = new SqlCommand("SP_GetCourierRecords", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        con.Open();
        da.Fill(dt);
        con.Close();
        grddata.DataSource = dt;
        grddata.DataBind();
    }

   
    public void ShowDepartment()
    {
        con1 = new Connection();
        PortalCon = new ServicePoratal();
        SqlDataReader dr = con1.SHow_indentDepartment("[TMU$Dimension Value]");
        ddIssueid.DataSource = dr;
        ddIssueid.DataTextField = "Description";
        ddIssueid.DataValueField = "Code";
        ddIssueid.DataBind();
    }
    protected void txtPin_TextChanged(object sender, EventArgs e)
    {


           SqlCommand cmd = new SqlCommand("getCitybyPinCode", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.Parameters.AddWithValue("@PinCode", txtPin.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Close();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtCity.Text = dt.Rows[0]["Districtname"].ToString();
            }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtMail.Text != "")
        {
            string email = txtMail.Text;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Email Address');", true);
                return ;
            }
               
        }

        SqlCommand cmd = new SqlCommand("SP_InsertCourierData", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@txtBarcode", txtBarcode.Text);
        cmd.Parameters.AddWithValue("@txtRef", txtRef.Text);
        cmd.Parameters.AddWithValue("@txtPin", txtPin.Text);
        cmd.Parameters.AddWithValue("@txtCity", txtCity.Text);
        cmd.Parameters.AddWithValue("@txtName", txtName.Text);
        cmd.Parameters.AddWithValue("@txtADD1", txtADD1.Text);
        cmd.Parameters.AddWithValue("@txtAdd2", txtAdd2.Text);
        cmd.Parameters.AddWithValue("@txtAdd3", txtAdd3.Text);
        cmd.Parameters.AddWithValue("@txtMail", txtMail.Text);
        cmd.Parameters.AddWithValue("@TextAddMobile", TextAddMobile.Text);
        cmd.Parameters.AddWithValue("@TextSenMobile", TextSenMobile.Text);
        cmd.Parameters.AddWithValue("@TextWeight", TextWeight.Text);
        cmd.Parameters.AddWithValue("@TextCOD", TextCOD.Text);
        cmd.Parameters.AddWithValue("@TextINSVAL", TextINSVAL.Text);
        cmd.Parameters.AddWithValue("@TextVPP", TextVPP.Text);
        cmd.Parameters.AddWithValue("@TextL", TextL.Text);
        cmd.Parameters.AddWithValue("@TextB", TextB.Text);
        cmd.Parameters.AddWithValue("@TextH", TextH.Text);
        cmd.Parameters.AddWithValue("@TextContType", TextContType.Text);
        cmd.Parameters.AddWithValue("@Department", ddIssueid.SelectedValue);
        cmd.Parameters.AddWithValue("@EnteredBy", Session["uid"].ToString());
        con.Open();
        cmd.ExecuteNonQuery();
        //Update Student weitghage Marks

        con.Close();

        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data Save Successfully.');", true);
        bindGrid();

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Sale_Purchase_Report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            grddata.AllowPaging = false;

            bindGrid();

            grddata.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grddata.HeaderRow.Cells)
            {
                cell.BackColor = grddata.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grddata.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grddata.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grddata.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grddata.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
}