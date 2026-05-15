using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Web.Services;
using System.Web.Script.Services;
using Utility;

public partial class Faculty_StudentFeedBackList : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["uid"].ToString() == "TMU02982" || Session["uid"].ToString() == "TMU08617" || Session["uid"].ToString() == "TMU00588" || Session["UserGroup"].ToString() == "REGISTRAR" || Session["UserGroup"].ToString() == "PRINCIPAL")
            {
                try
                {
                    if (!IsPostBack)
                    {
                        bindAcademicYear(); bindcollege();
                        if (Session["UserGroup"].ToString() != "PRINCIPAL") { }
                        else { tdCollege.Visible = false; }

                    }
                }
                catch
                {
                    Response.Redirect("../Default.aspx");
                }
            }
            else
            {
                Response.Redirect("../Default.aspx");
            }
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }
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
    public void bindcollege()
    {
        using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("select Code +' -' +Name as Details,Code from [TMU$Dimension Value] where [Dimension Code]='COLLEGE' and  [Active College]='1' order by Details", Conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            ddcollege.DataSource = dt1;
            ddcollege.DataTextField = "Details";
            ddcollege.DataValueField = "Code";
            ddcollege.DataBind();
            ddcollege.Items.Insert(0, new ListItem("-- Select College --", "-- Select --"));
        }

    }
    public void bindgrid()
    {
        using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("SP_GetFacultyPerformanceAppraisalReport", Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OddEvenYear", ddsem.SelectedValue);
            if (Session["UserGroup"].ToString() != "PRINCIPAL")
            { cmd.Parameters.AddWithValue("@collegecode", ddcollege.SelectedValue); }
            else { cmd.Parameters.AddWithValue("@collegecode", Session["GlobalDimension1Code"].ToString()); }
            cmd.Parameters.AddWithValue("@AcademicYear", ddlAcademicYear.SelectedValue);
            cmd.CommandTimeout = 1000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            EduGridView.DataSource = dt1;
            EduGridView.DataBind();
        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddsem.SelectedItem.Text != "--- Select Semester/Year----")
            {
                if (Session["UserGroup"].ToString() == "PRINCIPAL") { bindgrid(); }
                else if (ddcollege.SelectedValue == "-- Select --") { EduGridView.DataSource = ""; EduGridView.DataBind(); }
                else { bindgrid(); }

            }
            else
            {
                EduGridView.DataSource = "";
                EduGridView.DataBind();
            }
        }
        catch
        {

        }
    }

    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        Response.Clear(); Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Feedback.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        string headerTable = @"<Table bgcolor=gray><tr><td colspan=17 align=center bgcolor=gold ><font size=16><h1>Student's Feedback Based Faculty Performance Appraisal Report (" + ddsem.SelectedItem.Text + "->" + ddlAcademicYear.SelectedItem.Text + ")</h1></font> </td></tr></td></tr></Table>";
        Response.Write(headerTable);
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            EduGridView.HeaderRow.BackColor = Color.YellowGreen;

            foreach (TableCell cell in EduGridView.HeaderRow.Cells)
            {
                cell.BackColor = EduGridView.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in EduGridView.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = EduGridView.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = EduGridView.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            EduGridView.RenderControl(hw);
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

    public void grdpop(string lblEmpCode)
    {
        SqlCommand cmd = new SqlCommand("HRMSPortal.dbo.Get_StudentFeedbackdetail", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@FacultyCode", lblEmpCode);
        cmd.Parameters.AddWithValue("@AcademicYear", ddlAcademicYear.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();


        conn.Close();
        da.Fill(dt);

        DataTable transformedTable = new DataTable();

        //for (int i = 0; i < dt.Columns.Count; i++)
        //{
        //    transformedTable.Columns.Add("Column" + i);

        //    splitHeaders = dt.Columns[i].ColumnName.Split(',');


        //    foreach (string header in splitHeaders)
        //    {
        //        transformedTable.Rows.Add(header.Trim());
        //    }


        //    // Step 4: Add original data rows as-is
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        transformedTable.Rows.Add(row[i].ToString());
        //    }
        //}
        //DataTable dtnew = transformedTable;


        for (int i = 0; i < dt.Columns.Count; i++)
        {
            transformedTable.Columns.Add("Column" + i);
        }

        // Step 2: Create one row for the split headers (assuming one value per column header after split)
        // If header splits into multiple parts, you can decide how to handle it; here we'll just take the first part.

        for (int k = 0; k < 3; k++)
        {
            DataRow row = transformedTable.NewRow();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                string[] splitHeaders = dt.Columns[i].ColumnName.Split(',');



                row["Column" + i] = splitHeaders[k].Trim();



                //foreach (string part in splitHeaders)
                //{
                //    DataRow row = transformedTable.NewRow();
                //    row["Column" + i] = part.Trim();  

                //}
            }
            transformedTable.Rows.Add(row);
        }



        // Step 3: Add all data rows from original dt, one by one
        foreach (DataRow originalRow in dt.Rows)
        {
            DataRow newRow = transformedTable.NewRow();

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                newRow["Column" + i] = originalRow[i].ToString();
            }

            transformedTable.Rows.Add(newRow);
        }
        dt = transformedTable;




        if (dt.Rows.Count > 0)
        {
            int colcount = dt.Columns.Count;
            ViewState["colcount"] = colcount;
            grdParameters.DataSource = dt;
            grdParameters.DataBind();

        }
        else
        {
            grdParameters.DataSource = "";
            grdParameters.DataBind();
        }

    }
    protected void btnPopupExcel_Click(object sender, ImageClickEventArgs e)
    {
        Response.Clear(); Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Feedback.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        string headerTable = @"<Table bgcolor=gray><tr><td colspan=15 align=center bgcolor=gold ><font size=14><h1>Student's Feedback Based Faculty("") Performance Appraisal Report (" + ddsem.SelectedItem.Text + "->" + ddlAcademicYear.SelectedItem.Text + ") </h1></font> </td></tr></td></tr></Table>";
        Response.Write(headerTable);
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    protected void lblEmpCode_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
        int index = row.RowIndex;
        Label lblEmpCode = (Label)EduGridView.Rows[index].FindControl("lblEmpCode");
        Label lblEmpname = (Label)EduGridView.Rows[index].FindControl("txtname");
        //lblFacultyCode.Text = lblEmpCode.Text;
        empName.Text = lblEmpname.Text;
        lblEmplName.Text = "Dear " + lblEmpname.Text;
        lblfeedback.Text = "Please find the feedback of students taken in " + ddlAcademicYear.SelectedValue + " ( " + ddsem.SelectedItem.Text + ")";
        lblDep.Text = ddcollege.SelectedValue;
        grdpop(lblEmpCode.Text);



        MpaDetails.Show();
    }

    protected void grdParameters_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            foreach (TableCell cell in e.Row.Cells)
            {
                cell.Width = Unit.Pixel(15);
            }
        }

    }

    protected void grdParameters_DataBound(object sender, EventArgs e)
    {
        if (grdParameters.Rows.Count == 0)
            return;

        int rowCount = grdParameters.Rows.Count;
        int colCount = Convert.ToInt32(ViewState["colcount"]);

        // Arrays to hold sums and counts for each column
        double[] sums = new double[colCount];
        int[] counts = new int[colCount];

        // Loop through each row and cell, sum numeric values
        foreach (GridViewRow row in grdParameters.Rows)
        {
            for (int i = 0; i < colCount; i++)
            {
                double val;
                if (row.Cells[i].Text.Contains("."))
                {
                    if (double.TryParse(row.Cells[i].Text, out val))
                    {

                        sums[i] += val;
                        counts[i]++;
                    }

                }
            }
        }

        // Set footer row text
        GridViewRow footer = grdParameters.FooterRow;
        for (int i = 0; i < colCount; i++)
        {
            if (counts[i] > 0)
            {
                double avg = sums[i] / counts[i];
                footer.Cells[i].Text = avg.ToString("F2");
            }
            else
            {
                footer.Cells[i].Text = ""; // or "N/A"
            }
        }

        // Optionally, set the first footer cell to label "Average"
        if (footer.Cells.Count > 0)
        {
            footer.Cells[0].Text = "Average";
        }
    }
}