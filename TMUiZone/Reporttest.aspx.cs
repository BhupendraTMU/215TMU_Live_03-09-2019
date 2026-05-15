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
using System.Drawing;
using System.Web.Services;
using System.Web.Script.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Text.RegularExpressions;
using System.Net;
using System.Text;
using WebReference;


public partial class Reporttest : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["No"] != null && Request.QueryString["No"] != string.Empty)
                {
                    printarea.Visible = true;
                    divprint.Visible = false;
                    MSG.Visible = true;
                    Session["No"] = Request.QueryString["No"];
                    imglogo.Visible = true;
                    fetchReport(Session["No"].ToString());
                    generatebarcode(Session["No"].ToString());
                    bindetails(Session["No"].ToString());
                }
                else
                {
                    MSG.Visible = false;
                    printarea.Visible = false;
                    divprint.Visible = true;
                    imglogo.Visible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }
    }

    public void fetchReport(string Code)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("[Master].[dbo].[GetCovidDataReportFromHospital_work]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cmd.Parameters.Add("@RegistrationNo", (Code.Trim()));

        cmd.ExecuteNonQuery();
        con.Close();

    }
    public void generatebarcode(string Code)
    {
        DataTable dtNAV = new DataTable();
        SqlCommand cmdNAV = new SqlCommand("Proc_GetNAVCreditionalLive", con);
        cmdNAV.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter daNAV = new SqlDataAdapter(cmdNAV);
        daNAV.Fill(dtNAV);
        VoucherPosting nvp = new VoucherPosting();
        nvp.UseDefaultCredentials = true;
        nvp.Url = dtNAV.Rows[0]["URL"].ToString();
        nvp.Credentials = new NetworkCredential(dtNAV.Rows[0]["UserID"].ToString(), dtNAV.Rows[0]["Password"].ToString());
        nvp.BarcodeGeneration(Code);

        //nvp.BarcodeGeneration(Code);

    }

    public void bindetails(string Code)
    {
        SqlCommand cmd = new SqlCommand("[GetCovidDataReport]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cmd.Parameters.Add("@RegistrationNo", Code);


        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            ViewState["OrderTable"] = dt;
            if (dt.Rows[0]["Report Header Text1"].ToString().Contains("MICROBIOLOGY"))
            {

                if (dt.Rows[0]["Service Name"].ToString().Contains("COVID") || dt.Rows[0]["Service Name"].ToString().Contains("RT-PCR"))
                {

                    pnlRemark.Visible = true;
                    Label16.Text = "MICROBIOLOGY";
                    if (chkHeader.Checked == true || (Request.QueryString["No"] != null && Request.QueryString["No"] != string.Empty))
                    {
                        imglogo.Visible = true;
                        imglogo1.Visible = false;
                        imglogo2.Visible = false;
                        imglogo3.Visible = false;
                    }

                    else
                    {
                        imglogo.Visible = false;
                        imglogo1.Visible = false;
                        imglogo2.Visible = false;
                        imglogo3.Visible = false;
                    }
                }
                else
                {
                    pnlRemark.Visible = false;
                    Label16.Text = "MICROBIOLOGY";
                    if (chkHeader.Checked == true || (Request.QueryString["No"] != null && Request.QueryString["No"] != string.Empty))
                    {
                        imglogo.Visible = false;
                        imglogo1.Visible = false;
                        imglogo2.Visible = false;
                        imglogo3.Visible = true;
                    }

                    else
                    {
                        imglogo.Visible = false;
                        imglogo1.Visible = false;
                        imglogo2.Visible = false;
                        imglogo3.Visible = false;
                    }
                }



            }
            if (dt.Rows[0]["Report Header Text1"].ToString().Contains("PATHOLOGY"))
            {
                pnlRemark.Visible = false;
                Label16.Text = "PATHOLOGY";
                if (chkHeader.Checked == true || (Request.QueryString["No"] != null && Request.QueryString["No"] != string.Empty))
                {
                    imglogo.Visible = false;
                    imglogo1.Visible = false;
                    imglogo2.Visible = true;
                    imglogo3.Visible = false;
                }

                else
                {
                    imglogo.Visible = false;
                    imglogo1.Visible = false;
                    imglogo2.Visible = false;
                    imglogo3.Visible = false;
                }



            }
            if (dt.Rows[0]["Report Header Text1"].ToString().Contains("BIOCHEMISTRY"))
            {
                pnlRemark.Visible = false;
                Label16.Text = "BIOCHEMISTRY";
                if (chkHeader.Checked == true || (Request.QueryString["No"] != null && Request.QueryString["No"] != string.Empty))
                {
                    imglogo.Visible = false;
                    imglogo1.Visible = true;
                    imglogo2.Visible = false;
                    imglogo3.Visible = false;
                }
                else
                {
                    imglogo.Visible = false;
                    imglogo1.Visible = false;
                    imglogo2.Visible = false;
                    imglogo3.Visible = false;
                }


                pnlRemark.Visible = false;
            }
            MSG.Visible = false;
            printarea.Visible = true;
            lblPatient.Text = dt.Rows[0]["Patient Name"].ToString();
            lblAge.Text = dt.Rows[0]["Age"].ToString();
            //lblSampleDate.Text = dt.Rows[0]["Sample Collection Date"].ToString();
            lblIPNo.Text = dt.Rows[0]["Encounter No_"].ToString();
            lblBed.Text = dt.Rows[0]["Source"].ToString();
            lblCR.Text = dt.Rows[0]["Registration No_"].ToString();
            lblReferred.Text = dt.Rows[0]["Doctor Department Name"].ToString();
            lblStatus.Text = dt.Rows[0]["Display Report Status"].ToString();
            Label26.Text = dt.Rows[0]["Local Address"].ToString();
            lblLab.Text = dt.Rows[0]["Lab No_"].ToString();
            lblSampleDate.Text = dt.Rows[0]["Sample Collected Date"].ToString();
            lblAck.Text = dt.Rows[0]["Sample Acknowledged Date"].ToString();
            lblReportDate.Text = dt.Rows[0]["Result Date"].ToString();
            lbldoc.Text = dt.Rows[0]["Doctor Name"].ToString();
            BINDimage();
            DataTable dt1 = dt.DefaultView.ToTable(true, "Service Name");
            gvService.Visible = true;
            gvService.DataSource = dt1;
            gvService.DataBind();
        }
        else
        {
            gvService.Visible = false;
            MSG.Visible = true;
            printarea.Visible = false;
        }


    }
    public void BINDimage()
    {




        byte[] bytes = GetData("select [QR Code]  from [TMU$Hospital Report] where ([Lab No_]='" + Session["No"].ToString() + "' or [Registration No_]='" + Session["No"].ToString() + "')").Rows[0]["QR Code"].ToString() == "" ? null : (byte[])GetData("select [QR Code]  from [TMU$Hospital Report] where ([Lab No_]='" + Session["No"].ToString() + "' or [Registration No_]='" + Session["No"].ToString() + "')").Rows[0]["QR Code"];
        if (bytes != null)
        {
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            imgQrCode.ImageUrl = "data:image/jpg;base64," + base64String;
        }
        byte[] bytes1 = GetData("select [Signature]  from [TMU$Hospital Report] where ([Lab No_]='" + Session["No"].ToString() + "' or [Registration No_]='" + Session["No"].ToString() + "')").Rows[0]["Signature"].ToString() == "" ? null : (byte[])GetData("select [Signature]  from [TMU$Hospital Report] where ([Lab No_]='" + Session["No"].ToString() + "' or [Registration No_]='" + Session["No"].ToString() + "')").Rows[0]["Signature"];
        if (bytes1 != null)
        {
            string base64String = Convert.ToBase64String(bytes1, 0, bytes1.Length);
            Image1.ImageUrl = "data:image/jpg;base64," + base64String;
        }
    }
    private DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                }
            }
            return dt;
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        Session["No"] = txtCrNo.Text;
        if (chkHeader.Checked == true)
        {
            imglogo.Visible = true;
        }
        else
        {
            imglogo.Visible = false;
        }
        fetchReport(txtCrNo.Text);
        generatebarcode(txtCrNo.Text);
        bindetails(txtCrNo.Text);
    }
    protected void gvService_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblService = e.Row.FindControl("lblService") as Label;

            if (lblService.Text == "FEVER PROFILE" || lblService.Text == "PRL(PROLACTIN)")
            {

                GridView grdtest = e.Row.FindControl("grdtest") as GridView;
                GridView GridView1 = e.Row.FindControl("GridView1") as GridView;
                grdtest.Visible = false;
                GridView1.Visible = true;
                DataTable dt = (DataTable)ViewState["OrderTable"];

                GridView1.DataSource = dt.Select("[Service Name] = '" + lblService.Text + "'");
                GridView1.DataBind();
            }
            else
            {
                GridView grdtest = e.Row.FindControl("grdtest") as GridView;
                GridView GridView1 = e.Row.FindControl("GridView1") as GridView;
                GridView1.Visible = false;
                grdtest.Visible = true;
                DataTable dt = (DataTable)ViewState["OrderTable"];

                grdtest.DataSource = dt.Select("[Service Name] = '" + lblService.Text + "'");
                grdtest.DataBind();
            }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbltest = e.Row.FindControl("lbltest") as Label;
            Label lblService = e.Row.FindControl("lblResult") as Label;
            if (lblService.Text == "")
            {

                lbltest.Font.Bold = true;
            }
        }
    }
}