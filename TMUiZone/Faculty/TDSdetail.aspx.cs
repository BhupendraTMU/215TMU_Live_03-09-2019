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


public partial class Faculty_TDSdetail : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                bindTDSRequestList();

                txtEmployeeNo.Text = Session["uid"].ToString();
                txtEmployeeName.Text = Session["uname"].ToString();
                GetTDSdetail();
            }
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
    }



    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {

        ModalPopupExtender2.Show();
    }


    public void bindTDSRequestList()
    {
        // Replace this with your actual SELECT query
        string query = "SELECT * FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$EmployeeUpdateTDSInfo] WHERE EmployeeNo = @UserId";

        SqlCommand cmd = new SqlCommand(query, con);
        cmd.CommandType = CommandType.Text;

        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());

        if (con.State == ConnectionState.Closed)
            con.Open();

        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        dtCL.Columns.Add("StatusText", typeof(string));

        foreach (DataRow row in dtCL.Rows)
        {
            int status = Convert.ToInt32(row["status"]);
            switch (status)
            {
                case 0:
                    row["StatusText"] = "Pending";
                    break;
                case 2:
                    row["StatusText"] = "Reject";
                    break;
                case 1:
                    row["StatusText"] = "Accept";
                    break;
                default:
                    row["StatusText"] = "Unknown";
                    break;
            }
        }
        getTDSRequestList.DataSource = dtCL;
        getTDSRequestList.DataBind();
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        GetTDSdetail();
    }
    public void GetTDSdetail()
    {
        SqlCommand cmd = new SqlCommand("proc_getTDS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@AcademicSession", drpacsession.SelectedValue);
        cmd.Parameters.Add("@UserId", Session["uid"].ToString());

        if (con.State == ConnectionState.Closed)
            con.Open();

        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);

        grdStudentAttendanceD.DataSource = dtCL;
        grdStudentAttendanceD.DataBind();

        // Check if there are any rows returned
        if (dtCL.Rows.Count > 0)
        {
            // Get the last row
            DataRow lastRow = dtCL.Rows[dtCL.Rows.Count - 1];

            // Retrieve the Actual Amount from the last row
            decimal lastActualAmount = Convert.ToDecimal(lastRow["TDS Amount"]);

            // Now you can use lastActualAmount as needed, e.g., display in a label
            txtLastAmount.Text = lastActualAmount.ToString("N2");
        }
        else
        {
            txtLastAmount.Text = Convert.ToDecimal(0).ToString("N2");
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }


    protected void btnexporttoexel_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grdStudentAttendanceD.RenderControl(htmlWrite);

        Response.Clear();
        string str = "TDSDetail" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (decimal.Parse(txtLastAmount.Text) > decimal.Parse(txtNewAmountNew.Text))
        {
            ModalPopupExtender2.Show();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('New Amount not less then from Actual Amount');", true);
            return;

        }

        string txtEmployeeNo1 = Session["uid"].ToString();
        string txtEmployeeName1 = Session["uname"].ToString();
        //string ddlYear1 = ddlYear.Text;
        //int ddlMonth1 = int.Parse(ddlMonth.Text);

        try
        {
            using (SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
            {
                con1.Open();


                string checkQuery = @"
SELECT COUNT(*) 
FROM [TMU$EmployeeUpdateTDSInfo]
WHERE EmployeeNo = @EmployeeNo
AND (
        (@from_date BETWEEN from_date AND to_date)
     OR (@to_date BETWEEN from_date AND to_date)
     OR (from_date BETWEEN @from_date AND @to_date)
     OR (to_date BETWEEN @from_date AND @to_date)
)";

                using (SqlCommand checkCmd = new SqlCommand(checkQuery, con1))
                {
                    checkCmd.Parameters.AddWithValue("@EmployeeNo", txtEmployeeNo1);
                    checkCmd.Parameters.AddWithValue("@from_date", Convert.ToDateTime(txtfromdate.Text));
                    checkCmd.Parameters.AddWithValue("@to_date", Convert.ToDateTime(txtTodate.Text));

                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('This date range overlaps with an existing record.');", true);
                       
                        return;
                    }
                    
                }
                // If not exists, insert the new record
                string insertQuery = @"
                INSERT INTO [TMU$EmployeeUpdateTDSInfo] 
                ([EmployeeNo]
           ,[EmployeeName]
           ,[Year]
           ,[Month]
           ,[OldAmount]
           ,[NewAmount]
           ,[Status]
           ,[CreatedBy]
           ,[CreatedAt]
           ,[UpdatedBy]
           ,[UpdatedAt]
           ,[from_date]
           ,[to_date]
           ,[remark])
                VALUES
                (@EmployeeNo, @EmployeeName, '', '', @OldAmount, @NewAmount, @Status, @CreatedBy, @CreatedAt, @UpdatedBy, @UpdatedAt,@fromDate,@todate,@remark)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, con1))
                {
                    cmd.Parameters.AddWithValue("@EmployeeNo", txtEmployeeNo1);
                    cmd.Parameters.AddWithValue("@EmployeeName", txtEmployeeName1);
                    cmd.Parameters.AddWithValue("@OldAmount", decimal.Parse(txtLastAmount.Text));
                    cmd.Parameters.AddWithValue("@NewAmount", decimal.Parse(txtNewAmountNew.Text));
                    cmd.Parameters.AddWithValue("@Status", 0);
                    cmd.Parameters.AddWithValue("@CreatedBy", txtEmployeeNo1);
                    cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UpdatedBy", txtEmployeeNo1);
                    cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    cmd.Parameters.AddWithValue("@fromDate", txtfromdate.Text);
                    cmd.Parameters.AddWithValue("@todate", txtTodate.Text);
                    cmd.Parameters.AddWithValue("@remark", txtRemark.Text);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Request Insert Successful'); document.location.href='TDSdetail.aspx';", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Request Insert Failed: No rows affected'); document.location.href='TDSdetail.aspx';", true);
                    }
                }

                con1.Close();
            }
        }
        catch (SqlException sqlEx)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Request Insert Failed: No rows affected'); document.location.href='TDSdetail.aspx';", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Request Insert Failed: No rows affected'); document.location.href='TDSdetail.aspx';", true);
        }
    }


}


