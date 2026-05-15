using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_EmployeeDetailEditAprove : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                getData();
                //BindGridview();
            }
            catch
            {
                Response.Redirect("../Default.aspx");
            }
        }
    }
    public void getData()
    {
        using (SqlCommand cmd = new SqlCommand(@"SELECT [ID],[EmployeeNo],[Name],case when [Gender]='1' then 'Female' else 'Male' end as Gender,[BranchCode],[BirthDate],[FatherName],[AdharCard],[UANNo],[MobileNo],[Email],[PFNo],[Status],[CreatedBy],[CreatedAt],[UpdatedBy],[UpdatedAt]  
                                             FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$EmployeeUpdateInfo] WITH(NOLOCK)", con))
        {
            cmd.CommandType = CommandType.Text;

            if (con.State == ConnectionState.Closed)
                con.Open();

            SqlDataAdapter daCL = new SqlDataAdapter(cmd);
            DataTable dtCL = new DataTable();
            daCL.Fill(dtCL);

            grdmemberapprovallist.DataSource = dtCL;
            grdmemberapprovallist.DataBind();
        }
    }


     [WebMethod]
    public static object GetEmployeeDetailList(string employeeNo)
    {
        string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ToString();

        using (SqlConnection con = new SqlConnection(constr))
        {
            string query = @"
            SELECT 
                old.[No_] AS Old_EmployeeID,
                newinfo.[EmployeeNo] AS New_EmployeeID,
                old.[Full Name] AS Old_Name,
                newinfo.[Name] AS New_Name,
                old.[Gender] AS Old_Gender,
                old.[Birth Date] AS Old_BirthDate,
                newinfo.[BirthDate] AS New_BirthDate,
                newinfo.[Gender] AS New_Gender,
                old.[Branch Code] AS Old_BranchCode,
                newinfo.[BranchCode] AS New_BranchCode,
               
                old.[Job Title_Grade Desc] AS Old_DesignationName,
                newinfo.[DesignationName] AS New_DesignationName,
                old.[Father Name] AS Old_FatherName,
                newinfo.[FatherName] AS New_FatherName,
                old.[Aadhar Card] AS Old_AdharCard,
                newinfo.[AdharCard] AS New_AdharCard,
                old.[PAN No] AS Old_PANCard,
                newinfo.[PANCard] AS New_PANCard,
                old.[UAN No] AS Old_UANNo,
                newinfo.[UANNo] AS New_UANNo,
              
                old.[Mobile Phone No_] AS Old_MobileNo,
                newinfo.[MobileNo] AS New_MobileNo,
                old.[E-Mail] AS Old_Email,
                newinfo.[Email] AS New_Email,
                old.[PF No] AS Old_PFNo,
                newinfo.[PFNo] AS New_PFNo,
                newinfo.FileUpload,
                newinfo.[Status] AS New_Status,
                newinfo.[CreatedBy],
                newinfo.[CreatedAt],
                newinfo.[UpdatedBy],
                newinfo.[UpdatedAt]

            FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$Employee] AS old with(NOLOCK)
            JOIN [EDUCOLLEGELIVE-R2].[dbo].[TMU$EmployeeUpdateInfo] AS newinfo with(NOLOCK)
                ON old.[No_] = newinfo.CreatedBy
            WHERE 
                old.[No_] = @EmployeeNo";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@EmployeeNo", employeeNo);

                if (con.State == ConnectionState.Closed)
                    con.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {
                    if (Reader != null && Reader.Read())  // ✅ only read if data exists
                    {
                        return new
                        {

                            New_EmployeeNo = Reader["Old_EmployeeID"].ToString(),
                            New_Name = Reader["New_Name"].ToString(),
                            New_Gender = Reader["New_Gender"].ToString() == "2" ? "Male" : Reader["New_Gender"].ToString() == "1" ? "Female" : "Other",
                          
                            New_BirthDate = Convert.ToDateTime(Reader["New_BirthDate"]).ToString("dd-MM-yyyy"),
                            New_FatherName = Reader["New_FatherName"].ToString(),
                            New_AdharCard = Reader["New_AdharCard"].ToString(),
                            empNo= Reader["Old_EmployeeID"].ToString(),
                            New_DesignationName = Reader["New_DesignationName"].ToString(),
                          
                            New_PANCard = Reader["New_PANCard"].ToString(),
                            New_MobileNo = Reader["New_MobileNo"].ToString(),
                            New_Email = Reader["New_Email"].ToString(),
                         
                            New_FileUpload = Reader["FileUpload"].ToString(),
                            New_Status = Reader["New_Status"].ToString() == "0" ? "Pending" : Reader["New_Status"].ToString() == "1" ? "Accepted" : "Other",

                          
                            Old_Name = Reader["Old_Name"].ToString(),
                            Old_Gender = Reader["Old_Gender"].ToString() == "2" ? "Male" : Reader["Old_Gender"].ToString() == "1" ? "Female" : "Other",
                          
                            Old_BirthDate = Convert.ToDateTime(Reader["Old_BirthDate"]).ToString("dd-MM-yyyy"),
                            Old_FatherName = Reader["Old_FatherName"].ToString(),
                           
                            Old_DesignationName = Reader["Old_DesignationName"].ToString(),
                            Old_AdharCard = Reader["Old_AdharCard"].ToString(),
                            Old_PANCard = Reader["Old_PANCard"].ToString(),
                           
                          
                            Old_MobileNo = Reader["Old_MobileNo"].ToString(),
                            Old_Email = Reader["Old_Email"].ToString(),
                           
                            
                            CreatedBy = Reader["CreatedBy"].ToString(),
                            CreatedAt = Reader["CreatedAt"].ToString(),
                            UpdatedBy = Reader["UpdatedBy"].ToString(),
                            UpdatedAt = Reader["UpdatedAt"].ToString()
                        };
                    }
                }
            }
        }
        return null; // ✅ return null if no record
    }

       protected void btnAccept_Click(object sender, EventArgs e)
    {
        // Accept logic
        string employeeNo = hidEmpID.Value;
        updateEmployeeDetail(employeeNo,1);
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        // Reject logic
        string employeeNo = hidEmpID.Value;
        updateEmployeeDetail(employeeNo, 2);
    }
    public void updateEmployeeDetail(string employeeNo, int statusVal)
    {
        try
        {
            using (SqlCommand cmd = new SqlCommand("sp_UpdateEmployeeDetail", con)) // replace with your stored procedure name
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeNo", employeeNo);
                cmd.Parameters.AddWithValue("@StatusVal", statusVal);

                if (con.State == ConnectionState.Closed)
                    con.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    // Success alert
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Employee detail updated successfully'); document.location.href='EmployeeDetailEditAprove.aspx';", true);
                    }
                else
                {
                    // If no row updated
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                        "alert('No record found for the given Employee No.');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                        "alert('Something went wrong.');", true);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void grdmemberapprovallist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdmemberapprovallist.PageIndex = e.NewPageIndex;
        getData();
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        ExportToExcel();
    }

    private void ExportToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition",
            "attachment;filename=EmployeeProfileUpdateReport.xls");

        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            // Disable Paging
            grdmemberapprovallist.AllowPaging = false;
            getData();

            // Hide Action Column (Last Column)
           

            grdmemberapprovallist.RenderControl(hw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
}