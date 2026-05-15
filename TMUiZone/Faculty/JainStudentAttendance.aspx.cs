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
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using OfficeOpenXml;

public partial class Faculty_JainStudentAttendance : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Bindyearlist();
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../Default.aspx");
        }
    }

    private void Bindyearlist()
    {
        int currentYear = DateTime.Now.Year;
        List<int> yearsList = new List<int>();
        for (int i = 0; i <= 5; i++)
        {
            yearsList.Add(currentYear - i);
        }
        ddlYear.DataSource = yearsList;
        ddlYear.DataBind();
    }

    protected void BtnShow_Click(object sender, EventArgs e)
    {
        try

        {
            SqlCommand cmd = new SqlCommand("Get_JainStudentAttendanceDetailsView", con1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Session", ddlSession.SelectedValue.ToString());
            cmd.Parameters.Add("@Month", ddlMonth.SelectedValue.ToString());
            cmd.Parameters.Add("@Year", ddlYear.SelectedValue.ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                DataColumnCollection columns = dt.Columns;
                columns.Cast<DataColumn>().OrderBy(col => col.ColumnName).ToList();
                JainStudentList.DataSource = dt;
                JainStudentList.DataBind();
            }
        }
        catch (Exception ex)
        {
            DataTable dt = new DataTable();
            JainStudentList.DataSource = dt;
            JainStudentList.DataBind();
        }
    }

    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        try

        {
            SqlCommand cmd = new SqlCommand("Get_JainStudentAttendanceDetailsView", con1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Session", ddlSession.SelectedValue.ToString());
            cmd.Parameters.Add("@Month", ddlMonth.SelectedValue.ToString());
            cmd.Parameters.Add("@Year", ddlYear.SelectedValue.ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                DataColumnCollection columns = dt.Columns;
                columns.Cast<DataColumn>().OrderBy(col => col.ColumnName).ToList();
                JainStudentList.DataSource = dt;
                JainStudentList.DataBind();
            }
        }
        catch (Exception ex)
        {
            JainStudentList.DataSource = dt;
            JainStudentList.DataBind();
        }
        using (ExcelPackage pck = new ExcelPackage())
        {
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Jain Student Attendance List");
            ws.Cells["A1"].LoadFromDataTable(dt, true);
            using (var header = ws.Cells[1, 1, 1, dt.Columns.Count])
            {
                header.Style.Font.Bold = true;
                header.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                header.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
            }
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=JainStudentAttendanceList.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }
    }


}