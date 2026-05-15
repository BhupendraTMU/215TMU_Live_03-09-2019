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
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

public partial class Faculty_MigrationApproval : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                bindMigrationDetail(Session["uid"].ToString());
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=MigrationReport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        // Hide unwanted columns (View buttons etc.)
        GrdMigrationStudent.Columns[11].Visible = false; // View Details
        GrdMigrationStudent.Columns[12].Visible = false; // Application Form
        GrdMigrationStudent.Columns[13].Visible = false; // Certificate

        GrdMigrationStudent.AllowPaging = false;

        bindMigrationDetail(Session["uid"].ToString());

        GrdMigrationStudent.RenderControl(hw);

        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Required for Export
    }

    public void bindMigrationDetail(string Enroll)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetCertificateDetail", con);
            cmd.Parameters.Add("@EnrollmentNo_", Enroll);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            GrdMigrationStudent.DataSource = dt;
            GrdMigrationStudent.DataBind();
        }
        catch
        {
        }

    }

    protected void lblView_Click(object sender, EventArgs e)
    {
        string Complaint = (sender as LinkButton).CommandArgument;

        hdfApplicationNo.Value = Complaint;
        SqlCommand cmd = new SqlCommand("[proc_GetMIGRApplicationDetail]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@AppNo", Complaint);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblAppNo.Text = dt.Rows[0]["App_No"].ToString();
            lblEnrollNo.Text = dt.Rows[0]["Enrollment_No"].ToString();
            lblStudentName.Text = dt.Rows[0]["Student_Name"].ToString();
            lblCollege.Text = dt.Rows[0]["College"].ToString();
            lblProgram.Text = dt.Rows[0]["Program"].ToString();
            lblDesc.Text = dt.Rows[0]["Description"].ToString();
            lblCertificatetype.Text = dt.Rows[0]["Certificate_Type"].ToString();
            lblExamRemark.Text = dt.Rows[0]["Examination_Remark"].ToString();
            lbldeptRemark.Text = dt.Rows[0]["Dept_Remark"].ToString();
            lblCountry.Text = dt.Rows[0]["country"].ToString();
            lblState.Text = dt.Rows[0]["state"].ToString();
            lblcity.Text = dt.Rows[0]["city"].ToString();
            lbladdress.Text = dt.Rows[0]["address"].ToString();
            lblpostcode.Text = dt.Rows[0]["postcode"].ToString();
            txtPayOrderNo.Text = dt.Rows[0]["OrderID"].ToString();
            txtPaymentDate.Text = dt.Rows[0]["Payment Date"].ToString();
            btnApprove.Enabled = true;
            btnRejectPop.Enabled = true;
            if (dt.Rows[0]["Payment_Status"].ToString() != "2" && Session["uid"].ToString() == "TMU00035")
            {
                btnApprove.Enabled = false;
                btnRejectPop.Enabled = false;
            }

        }

        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal1').modal('show');</script>", false);
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("[SP_ApproveMigration]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ComplaintNo", hdfApplicationNo.Value);
        cmd.Parameters.Add("@UseId", Session["uid"].ToString());
        cmd.Parameters.Add("@Remark", txtRemark.Text);

        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('Application Approved Successfully'); document.location.href='MigrationApproval.aspx';", true);

    }

    protected void btnRejectPop_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("[SP_RejectMigration]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ComplaintNo", hdfApplicationNo.Value);
        cmd.Parameters.Add("@UseId", Session["uid"].ToString());
        cmd.Parameters.Add("@Remark", txtRemark.Text);
        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('Application Reject Successfully'); document.location.href='MigrationApproval.aspx';", true);

    }
    protected void txtRemark_Click(object sender, EventArgs e)
    {


    }
    protected void lnkSupport_Click(object sender, EventArgs e)
    {



        try
        {
            string AssignmentNo = hdfApplicationNo.Value;

            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "select  Attachment,   [Content Type], [File Name]   from [tbl_MigrationRequest] where App_No='" + AssignmentNo + "' ";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Attachment"];
                        contentType = sdr["Content Type"].ToString();
                        fileName = sdr["File Name"].ToString();
                    }
                    con.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal1').modal('show');</script>", false);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('No Attachment Found');", true);
        }

    }

    protected void lblApplication_Click(object sender, EventArgs e)
    {
        string Complaint = (sender as LinkButton).CommandArgument;

        hdfApplicationNo.Value = Complaint;
        SqlCommand cmd = new SqlCommand("[proc_GetMIGRApplicationdataApplication]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@AppNo", Complaint);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblName.Text = dt.Rows[0]["Student_Name"].ToString();
            Label6.Text= dt.Rows[0]["Father_Name"].ToString();
            Label8.Text = dt.Rows[0]["Mothers Name"].ToString();
            Label10.Text = dt.Rows[0]["Enrollment No_"].ToString();
            Label12.Text = dt.Rows[0]["Enrollment No_"].ToString();
            Label15.Text = dt.Rows[0]["Course Code"].ToString();
            Label17.Text = dt.Rows[0]["sem"].ToString();
            Label19.Text = dt.Rows[0]["Enrollment No_"].ToString();
            Label23.Text = dt.Rows[0]["Global Dimension 1 Code"].ToString();
            Label63.Text = dt.Rows[0]["Global Dimension 1 Code"].ToString();
            Label25.Text= dt.Rows[0]["Academic Year"].ToString();
            Label65.Text = dt.Rows[0]["Enrollment No_"].ToString();
            Label32.Text = dt.Rows[0]["Course Code"].ToString();
            Label34.Text = dt.Rows[0]["sem"].ToString();
            Label36.Text = dt.Rows[0]["Enrollment No_"].ToString();
            Label39.Text = dt.Rows[0]["Course Code"].ToString();
            Label41.Text = "";
            Label42.Text = dt.Rows[0]["Academic Year"].ToString();
            Label43.Text = dt.Rows[0]["Enrollment No_"].ToString();
            Label46.Text = dt.Rows[0]["Course Code"].ToString();
            Label48.Text = dt.Rows[0]["sem"].ToString();
            Label50.Text = "";
            Label53.Text = dt.Rows[0]["Enrollment No_"].ToString();
          

            try

            {

                byte[] bytes = GetData("select Upload_Photo as FacultyImage from Tbl_LibraryMembership  WHERE [Employee_code] = 'TMU00035'").Rows[0]["FacultyImage"].ToString() == "" ? null : (byte[])GetData("select Upload_Photo as FacultyImage from Tbl_LibraryMembership  WHERE [Employee_code] = 'TMU00035'").Rows[0]["FacultyImage"];
                if (bytes != null)
                {
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    ImgPrv.ImageUrl = "data:image/png;base64," + base64String;
                }

            }
            catch (Exception ex)
            {
            }


        }

        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#ApplicationModel').modal('show');</script>", false);
    }
    private DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString()))
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
    protected void lblCertificate_Click(object sender, EventArgs e)
    {
        string Complaint = (sender as LinkButton).CommandArgument;

        hdfApplicationNo.Value = Complaint;
        SqlCommand cmd = new SqlCommand("[proc_GetMIGRApplicationdata]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@AppNo", Complaint);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            try
            {
                slNo.Text = dt.Rows[0]["App_No"].ToString().Replace("MIGR-", "");

                lblStudent.Text = dt.Rows[0]["Student_Name"].ToString();
                lblGaurdiation.Text = dt.Rows[0]["Father_Name"].ToString();
                lblenrollment.Text = dt.Rows[0]["Enrollment_No"].ToString();
                lblProgramCertificate.Text = dt.Rows[0]["Program"].ToString();
                lblacedmicyear.Text = dt.Rows[0]["Academic Year"].ToString();
                //lblRollNo.Text = dt.Rows[0]["Enrollment_No"].ToString();
                
            }
            catch (Exception ex)
            {
            }


        }

        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#CertificateModel').modal('show');</script>", false);

    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        //GridViewDetails.Hide();

    }
}