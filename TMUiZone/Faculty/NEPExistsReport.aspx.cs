using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using OfficeOpenXml;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_NEPExistsReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindGrid(null);
            }
        }
        catch (Exception)
        {
            Response.Redirect("../Default.aspx");
        }
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        string enroll = null;
        if (!string.IsNullOrWhiteSpace(txtEnrollment.Text))
        {
            enroll = txtEnrollment.Text.Trim();
        }
        BindGrid(enroll);
    }

    private void BindGrid(string enrollmentNo)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_GetNEPExistsReport", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (enrollmentNo == null)
                {
                    cmd.Parameters.AddWithValue("@EnrollmentNo", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@EnrollmentNo", enrollmentNo);
                }
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
        }

        gvNEP.DataSource = dt;
        gvNEP.DataBind();

        if (gvNEP.Rows.Count > 0 && gvNEP.HeaderRow != null)
        {
            gvNEP.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void gvNEP_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvNEP.PageIndex = e.NewPageIndex;
        string enroll = null;
        if (!string.IsNullOrWhiteSpace(txtEnrollment.Text))
        {
            enroll = txtEnrollment.Text.Trim();
        }
        BindGrid(enroll);
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_GetNEPExistsReport", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                string enroll = null;
                if (!string.IsNullOrWhiteSpace(txtEnrollment.Text))
                {
                    enroll = txtEnrollment.Text.Trim();
                }
                if (enroll == null)
                    cmd.Parameters.AddWithValue("@EnrollmentNo", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@EnrollmentNo", enroll);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
        }

        if (dt.Rows.Count == 0)
        {
            return;
        }

        // Export following the UI TemplateField order and headers
        var uiHeaders = new string[] {
            "Enrollment No","NEP Student Name","Admission Session","Option Selected","Course Name","Department","College","Semester","Section","Full Name","Father Name","Mother Name","Mobile No","Email Id","Gender","DOB","City","State","Student Status","Session"
        };
        var dataFields = new string[] {
            "EnrollmentNo","StudentName","AdmissionSession","OptionSelected","CourseName","Department","College","Semester","Section","FullName","FatherName","MotherName","MobileNo","EmailId","Gender","DOB","City","State","Student Status","Session"
        };

        // build export datatable in UI order
        DataTable exportDt = new DataTable("NEPExistsReport");
        for (int i = 0; i < uiHeaders.Length; i++)
        {
            exportDt.Columns.Add(uiHeaders[i]);
        }

        foreach (DataRow row in dt.Rows)
        {
            var newRow = exportDt.NewRow();
            for (int i = 0; i < dataFields.Length; i++)
            {
                object val = null;
                if (dt.Columns.Contains(dataFields[i]))
                {
                    val = row[dataFields[i]];
                }
                else
                {
                    // try alternative without spaces if needed
                    string alt = dataFields[i].Replace(" ", "");
                    if (dt.Columns.Contains(alt)) val = row[alt];
                }
                newRow[i] = val ?? string.Empty;
            }
            exportDt.Rows.Add(newRow);
        }

        ExcelPackage package = new ExcelPackage();
        try
        {
            var ws = package.Workbook.Worksheets.Add("NEPExistsReport");
            for (int c = 0; c < exportDt.Columns.Count; c++)
            {
                ws.Cells[1, c + 1].Value = exportDt.Columns[c].ColumnName;
            }
            for (int r = 0; r < exportDt.Rows.Count; r++)
            {
                for (int c = 0; c < exportDt.Columns.Count; c++)
                {
                    ws.Cells[r + 2, c + 1].Value = exportDt.Rows[r][c];
                }
            }

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", "attachment; filename=NEPExistsReport.xlsx");
            Response.BinaryWrite(package.GetAsByteArray());
            Response.End();
        }
        finally
        {
            package.Dispose();
        }
    }
}
