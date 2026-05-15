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
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class StudentFineReport : System.Web.UI.Page
{
    TMUConnection con; string CollegeCode = "";

    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Session["UserGroup"].ToString() == "FINE" || Session["UserGroup"].ToString() == "ADMIN" || Session["Proctor"].ToString() != "")
            {
                try
                {

                    CollegeCode = Session["GlobalDimension1Code"].ToString();
                    if (!IsPostBack)
                    {

                        txtFromtDate.Text = System.DateTime.Now.AddMonths(-12).ToString("dd MMM yyyy"); 
                        txtToDate.Text = System.DateTime.Now.ToString("dd MMM yyyy"); 
                        bindAcademicYear();
                        BindCourse();
                        bindActionTaken();
                        BindGrid();
                       

                    }

                }

                catch (Exception ex)
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        catch
        { Response.Redirect("~/Default.aspx"); }


    }


    public void bindAcademicYear()
    {
        SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da.Fill(dt1);
        ddlAcademicYear.DataSource = dt1;
        ddlAcademicYear.DataTextField = "Details";
        ddlAcademicYear.DataValueField = "No_";
        ddlAcademicYear.DataBind();
        ddlAcademicYear.Items.Insert(ddlAcademicYear.Items.Count, "ALL");

    }

    public void BindCourse()
    {
       
        SqlCommand cmd = new SqlCommand("proc_GetCourseForFine", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpCourse.DataSource = dt;
        drpCourse.DataTextField = "Details";
        drpCourse.DataValueField = "No_";
        drpCourse.DataBind();
        BindSemester();
        
    }

    public void BindSemester()
    {
        string FacultyCode = "";
        //  FacultyCode = Session["uid"].ToString();

        SqlCommand cmd = new SqlCommand("proc_GetSemester", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", FacultyCode);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpSemester.DataSource = dt;
        drpSemester.DataTextField = "Details";
        drpSemester.DataValueField = "No_";
        drpSemester.DataBind();

    }


    public void bindActionTaken()
    {

        SqlCommand cmd2 = new SqlCommand("select Description from [TMU$Action Taken]", con1);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataTable dt2 = new DataTable();
        da2.Fill(dt2);
        drpAction.DataTextField = "Description";
        drpAction.DataValueField = "Description";
        drpAction.DataSource = dt2;
        drpAction.DataBind();
        drpAction.Items.Insert(0, "-- Select --");

        //FacultyCode = Session["uid"].ToString();
        CollegeCode = Session["GlobalDimension1Code"].ToString();



    }



    private void BindGrid()
    {



        string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_StudentDeciplineFineReport"))
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@fromDate", txtFromtDate.Text);
                cmd.Parameters.Add("@toDate", txtToDate.Text);
                cmd.Parameters.Add("@Academic", ddlAcademicYear.SelectedValue);
                cmd.Parameters.Add("@courseId", drpCourse.SelectedValue);
                cmd.Parameters.Add("@semester", drpSemester.SelectedValue);
                cmd.Parameters.Add("@college", Session["GlobalDimension1Code"].ToString());

                if (drpAction.SelectedIndex > 0)
                { cmd.Parameters.Add("@ActionTaken", drpAction.SelectedValue); }
                else
                { cmd.Parameters.Add("@ActionTaken", ""); }

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind(); // for sum
                  


                    if (dt.Rows.Count > 0)
                    {
                        decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("Fine Amount"));
                        GridView1.FooterRow.Cells[10].Text = "Total";
                        GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                        GridView1.FooterRow.Cells[11].Text = total.ToString("N2");

                        btnExportToExcel.Visible = true;
                        Btnpdf.Visible = true;
                       

                        
                        hfR_Session.Value =  ddlAcademicYear.SelectedValue;
                        hfR_Course.Value = (drpCourse.SelectedIndex != 0 ? drpCourse.SelectedItem.Text : "");                       
                        if (drpSemester.SelectedIndex > 0)
                        {
                     hfR_SemYear.Value = (drpSemester.SelectedIndex != 0 ? drpSemester.SelectedItem.Text : "");
                           
                            
                        
                        
                        }
                        else
                        {
                            hfR_SemYear.Value = " ";
                        
                        }



                        hfR_Action.Value =(drpAction.SelectedIndex != 0 ? drpAction.SelectedItem.Text : "");

                        lblpdf.Text = "<Table><tr><td colspan=10 align=center  bgcolor=gold ><font size=14><h1>Student Fine Report : " + DateTime.Now.ToString("dd-MMM-yyyy") + "</h1></font> </td></tr><tr><td colspan=2 bgcolor=gold><b>Session: " + hfR_Session.Value + "</b></td><td colspan=2 bgcolor=gold><b>By:-" + Session["uname"].ToString() + "</b></td><td colspan=2 bgcolor=gold><b>Course: " + hfR_Course.Value + "</b></td><td colspan=2 bgcolor=gold><b>Semester: " + hfR_SemYear.Value + "</b></td><td colspan=2 bgcolor=gold><b>ActionTaken: " + hfR_Action.Value + "</b></td></tr></Table>";
                        
                    }
                    else
                    {
                        btnExportToExcel.Visible = false;
                        Btnpdf.Visible = false;
                        
                    }




                }
            }
        }



    }









    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
        BindGrid();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //hfR_Session.Value = "";
        //hfR_Course.Value = "";
        //hfR_SemYear.Value = "";
        //hfR_Action.Value = "";
        lblpdf.Text = "";
       
        
        this.BindGrid();
    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSemester();


    }
    protected void drpAction_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnExportToExcel_Click(object sender, ImageClickEventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=StudentFineList.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
       // string headerTable = @"<Table><tr><td colspan=10 align=center  bgcolor=gold ><font size=14><h1>Student Fine Report : " + DateTime.Now.ToString("dd-MMM-yyyy") + "</h1></font> </td></tr><tr><td colspan=2 bgcolor=gold><b>Session: " + hfR_Session.Value + "</b></td><td colspan=2 bgcolor=gold><b>By:-" + Session["uname"].ToString() + "</b></td><td colspan=2 bgcolor=gold><b>Course: " + hfR_Course.Value + "</b></td><td colspan=2 bgcolor=gold><b>Semester: " + hfR_SemYear.Value + "</b></td><td colspan=2 bgcolor=gold><b>ActionTaken: " + hfR_Action.Value + "</b></td></tr></Table>";
       // Response.Write(headerTable);
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //To Export all pages
            GridView1.AllowPaging = false;
            this.BindGrid();
            GridView1.HeaderRow.BackColor = System.Drawing.Color.DarkSeaGreen;
            foreach (TableCell cell in GridView1.HeaderRow.Cells)
            {
                cell.BackColor = GridView1.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in GridView1.Rows)
            {
                row.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = GridView1.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }
            GridView1.RenderControl(hw);

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
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }
    protected void Btnpdf_Click(object sender, EventArgs e)
    {
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                
                //To Export all pages
                GridView1.AllowPaging = false;
                this.BindGrid();

                GridView1.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=StudentFineList.pdf");
                //string headerTable = @"<Table><tr><td colspan=10 align=center  bgcolor=gold ><font size=14><h1>Student Fine Report : " + DateTime.Now.ToString("dd-MMM-yyyy") + "</h1></font> </td></tr><tr><td colspan=2 bgcolor=gold><b>Session: " + hfR_Session.Value + "</b></td><td colspan=2 bgcolor=gold><b>By:-" + Session["uname"].ToString() + "</b></td><td colspan=2 bgcolor=gold><b>Course: " + hfR_Course.Value + "</b></td><td colspan=2 bgcolor=gold><b>Semester: " + hfR_SemYear.Value + "</b></td><td colspan=2 bgcolor=gold><b>ActionTaken: " + hfR_Action.Value + "</b></td></tr></Table>";
                //Response.Write(headerTable);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
                //if (btnExportToExcel.Visible == true || Btnpdf.Visible == true)
                //{
                //    System.Threading.Thread.Sleep(120000);
                //}
                Response.End();
            }
        }
    }


    protected void OnDataBound(object sender, EventArgs e)
    {
       // GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        
       // TableHeaderCell cell = new TableHeaderCell();
       // cell.HorizontalAlign = HorizontalAlign.Center;
       // cell.Text = "" + lblpdf.Text + "";


       ////// cell.Text = "<Table><tr><td colspan=10 align=center  bgcolor=gold ><font size=14><h1>Student Fine Report : " + DateTime.Now.ToString("dd-MMM-yyyy") + "</h1></font> </td></tr><tr><td colspan=2 bgcolor=gold><b>Session: " + hfR_Session.Value + "</b></td><td colspan=2 bgcolor=gold><b>By:-" + Session["uname"].ToString() + "</b></td><td colspan=2 bgcolor=gold><b>Course: " + hfR_Course.Value + "</b></td><td colspan=2 bgcolor=gold><b>Semester: " + hfR_SemYear.Value + "</b></td><td colspan=2 bgcolor=gold><b>ActionTaken: " + hfR_Action.Value + "</b></td></tr></Table>";
        

       // cell.ColumnSpan = 10;
       // row.Controls.Add(cell);



       


       // row.BackColor = ColorTranslator.FromHtml("#dde0e8");               
       // row.ForeColor = ColorTranslator.FromHtml("Black");
        
       // GridView1.HeaderRow.Parent.Controls.AddAt(0, row);// by veerendra

    }
 

    
}