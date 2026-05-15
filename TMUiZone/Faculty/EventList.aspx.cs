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
using iTextSharp.text;

public partial class Faculty_EventList : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserGroup"].ToString() =="FACULTY" || Session["UserGroup"].ToString() == "PRINCIPAL" || Session["uid"].ToString() == "TMU01005" || Session["UserGroup"].ToString() == "REGISTRAR" || Session["UserGroup"].ToString() == "STUDENT")
            {
                if (!IsPostBack)
                {
                    txtFromtDate.Text = System.DateTime.Now.ToString("dd MMM yyyy");
                    txtToDate.Text = System.DateTime.Now.AddMonths(1).ToString("dd MMM yyyy");       
                    bindcollegedrp();
                    BinddlEventType();
                   
                    
                    if (Session["UserGroup"].ToString() == "STUDENT")
                    {
                        tblSearch.Visible = false;
                    }
                    if (Session["UserGroup"].ToString() == "REGISTRAR")
                    {
                        ddlcollege.Enabled = true;
                    }
                    else
                    {
                        if (Session["UserGroup"].ToString() == "STUDENT")
                        {
                            ddlcollege.SelectedValue = Session["College"].ToString();
                        }
                        else
                        {
                            ddlcollege.SelectedValue = Session["GlobalDimension1Code"].ToString();
                        }
                    }
                    BindGrid();
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Default.aspx");
        }
        
    }
    public void BinddlEventType()
    {
        string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {

            SqlCommand cmd = new SqlCommand("Sp_getEventType", con); //ok
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddleventl.DataSource = dt;
            ddleventl.DataTextField = "Details";
            ddleventl.DataValueField = "Value";
            ddleventl.DataBind();
        }
    }

    private void BindGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_EventList", con))
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                if (ddlcollege.SelectedIndex == 0) { cmd.Parameters.Add("@College", ""); }
                else { cmd.Parameters.Add("@College", ddlcollege.SelectedValue); }
                cmd.Parameters.Add("@fromDate", txtFromtDate.Text);
                cmd.Parameters.Add("@toDate", txtToDate.Text);
                cmd.Parameters.Add("@EventType", ddleventl.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    if (dt.Rows.Count > 0)
                    {
                        DateTime dt1 = Convert.ToDateTime(dt.Rows[0]["Date"]);
                       // lblEvent.Text = dt.Rows[0]["Event"].ToString() + " ( " + dt1.ToString("dd MMM yyyy") + " )";//comment on 01 march 2017
                        lblEvent.Text = dt.Rows[0]["Event"].ToString() + " ( " + dt.Rows[0]["FromTo"] + " )";
                    }
                }
            }
        }


       
    }


    public void bindcollegedrp()
    {
        using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("select Code, (Code +' - '+ Name) as BranchName from [TMU$Dimension Value] where [Dimension Code]='COLLEGE' and [Active College]='1'", Conn);

            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            ddlcollege.DataSource = dt1;
            ddlcollege.DataTextField = "BranchName";
            ddlcollege.DataValueField = "Code";
            ddlcollege.DataBind();
            ddlcollege.Items.Insert(0,"-- Select --");
            //drpcollege.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));
        }
    }



    protected void BtnShow_Click(object sender, EventArgs e)
    {
        this.BindGrid();
    }


   // for  image view popup

    private void BindGridpop()
    {
        string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Sp_ValidateAndGetImage"); 
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Code", lblpop.Text);
            cmd.Connection = con;
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);
               GridViewpop.DataSource = dt;
                GridViewpop.DataBind();
            }
        }
    }

    
    protected void Btnpop_Click(object sender, EventArgs e)
    {

           LinkButton btndetails = sender as LinkButton;
            GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
            lblpop.Text = GridView1.DataKeys[gvrow.RowIndex].Value.ToString();
            this.ModalPopupExtender1.Show();
            this.BindGridpop();
    }

    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
        ModalPopupExtender1.Show();
    }
    protected void lnkDownload2_Click(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();


        ModalPopupExtender1.Show();

    }
    protected void lnkDownload3_Click(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(Server.MapPath( filePath));
        Response.End();


        ModalPopupExtender1.Show();
    }
    protected void btnExportToExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=EventList.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            // string headerTable = @"<Table><tr><td colspan=10 align=center  bgcolor=gold ><font size=14><h1>Student Fine Report : " + DateTime.Now.ToString("dd-MMM-yyyy") + "</h1></font> </td></tr><tr><td colspan=2 bgcolor=gold><b>Session: " + hfR_Session.Value + "</b></td><td colspan=2 bgcolor=gold><b>By:-" + Session["uname"].ToString() + "</b></td><td colspan=2 bgcolor=gold><b>Course: " + hfR_Course.Value + "</b></td><td colspan=2 bgcolor=gold><b>Semester: " + hfR_SemYear.Value + "</b></td><td colspan=2 bgcolor=gold><b>ActionTaken: " + hfR_Action.Value + "</b></td></tr></Table>";
            // Response.Write(headerTable);
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                //To Export all pages
                GridView1.AllowPaging = false;
                BindGrid();
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
        catch
        {

        }
    }
    protected void BtnExportpdf_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt;
        dt=BindPDF();
        GenerateReport();
      //  Document document = new Document();
      //  //PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
      //  PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);
      //  document.Open();
      //  iTextSharp.text.Font font8 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 8);
      //  iTextSharp.text.Font myFont = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_BOLD, 12,iTextSharp.text.Color.BLACK);
      //  PdfPTable table = new PdfPTable(dt.Columns.Count);
      ////  PdfPRow row = null; int iCol = 0;   string colname = "";        
      //  table.WidthPercentage = 100;
      //  table.SetTotalWidth(new float[] { 4,15,7,10,25,25,7,7 });        
      //  PdfPCell cell = new PdfPCell(new Phrase("Events List"));
      //  Phrase[] header = new Phrase[2];
      //  header[1] = new Phrase("Movie history");
      //  cell.Colspan = dt.Columns.Count;
      //  foreach (DataColumn c in dt.Columns)
      //  {
      //      table.AddCell(new Phrase(c.ColumnName, myFont));
      //  }
      //  int i = dt.Columns.Count;
      //  foreach (DataRow r in dt.Rows)
      //  {            
      //      if (dt.Rows.Count > 0)
      //      {
      //          for (int j = 0; j < i; j++)
      //          {
      //              table.AddCell(new Phrase(r[j].ToString(), font8));
                    
      //          }
                
      //      }
           
      //  }
      //  document.Add(table);
      //  document.Close();
      //  Response.ContentType = "application/pdf";
      //  Response.AddHeader("content-disposition", "attachment; filename= EventList.pdf");
      //  Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }
    private  DataTable BindPDF()
    {
        DataTable dt=new DataTable(); 
        string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_EventListPDF", con))
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                if (ddlcollege.SelectedIndex == 0) { cmd.Parameters.Add("@College", ""); }
                else { cmd.Parameters.Add("@College", ddlcollege.SelectedValue); }
                cmd.Parameters.Add("@fromDate", txtFromtDate.Text);
                cmd.Parameters.Add("@toDate", txtToDate.Text);
                cmd.Parameters.Add("@EventType", ddleventl.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    //DataTable dt = new DataTable();
                    sda.Fill(dt);
                    //GridView1.DataSource = dt;
                    //GridView1.DataBind();
                    if (dt.Rows.Count > 0)
                    {
                        DateTime dt1 = Convert.ToDateTime(dt.Rows[0]["Date"]);
                        lblEvent.Text = dt.Rows[0]["Event"].ToString() + " ( " + dt1.ToString("dd MMM yyyy") + " )";
                    }
                }
            }
        }

        return dt;

    }
    protected void GenerateReport()
    {
        DataTable dt; dt = BindPDF();
        Document document = new Document(PageSize.A4, 10f, 15f, 20f, 10f);
        iTextSharp.text.Font NormalFont = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_BOLD, 12, iTextSharp.text.Color.BLACK);         
        using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
        {
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            Phrase phrase = null;   PdfPCell cell = null;          
            document.Open();            
            PdfPTable table = new PdfPTable(dt.Columns.Count);           
            table.WidthPercentage = 100;
            table.SetTotalWidth(new float[] { 4, 15, 7, 10, 25, 25, 7, 7 });          
            
            //Header
            string EventType = "", CollegeCode = "";
            if(ddleventl.SelectedValue !="0"){EventType="EventType - "+ddleventl.SelectedItem.Text;}
            if (ddlcollege.SelectedValue != "") { CollegeCode = "College - " + ddlcollege.SelectedValue; }
            phrase = new Phrase();
            phrase.Add(new Chunk(EventType, FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 8, iTextSharp.text.Color.BLACK)));
            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
            cell.Colspan = 2;
            table.AddCell(cell);
            //phrase = new Phrase();
            //phrase.Add(new Chunk(txtFromtDate.Text, FontFactory.GetFont(FontFactory.TIMES_BOLD, 8, iTextSharp.text.Color.BLACK)));
            //cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
            //cell.VerticalAlignment = PdfCell.ALIGN_TOP;
            //cell.Colspan = 2;
            //table.AddCell(cell);

            phrase = new Phrase();
            phrase.Add(new Chunk(txtFromtDate.Text+"  -  "+txtToDate.Text, FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 8, iTextSharp.text.Color.BLACK)));
            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
            cell.Colspan = 3;
            table.AddCell(cell);
            phrase = new Phrase();
            phrase.Add(new Chunk(CollegeCode, FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 8, iTextSharp.text.Color.BLACK)));
            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
            cell.Colspan = 3;
            table.AddCell(cell);
            //Company Logo
            //cell = ImageCell("~/images/TMU Logo.jpg", 30f, PdfPCell.ALIGN_CENTER);
            //cell.Colspan = 2;
            //table.AddCell(cell);
            //Separater Line            
           // DrawLine(writer, 15f, document.Top - 44f, document.PageSize.Width - 42f, document.Top - 44f, iTextSharp.text.Color.BLACK);
         //   DrawLine(writer, 25f, document.Top - 31f, document.PageSize.Width - 25f, document.Top - 31f, iTextSharp.text.Color.RED);


            document.Add(table);

            iTextSharp.text.Font font8 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 8);
            iTextSharp.text.Font myFont = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_BOLD, 10, iTextSharp.text.Color.BLACK);
           table = new PdfPTable(dt.Columns.Count);          
           table.WidthPercentage = 100;
           table.SetTotalWidth(new float[] { 4, 15, 7, 10, 25, 25, 7, 7 });
           table.SpacingBefore = 20f;
            cell.Colspan = dt.Columns.Count;
            foreach (DataColumn c in dt.Columns)
            {
                table.AddCell(new Phrase(c.ColumnName, myFont));
            }
            int i = dt.Columns.Count;
            foreach (DataRow r in dt.Rows)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        table.AddCell(new Phrase(r[j].ToString(), font8));

                    }

                }

            }
            document.Add(table);            
            document.Close();
            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=Events List.pdf");
            Response.ContentType = "application/pdf";
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.End();
            Response.Close();
        }
    }

    private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, iTextSharp.text.Color color)
    {
        PdfContentByte contentByte = writer.DirectContent;
        contentByte.SetColorStroke(color);
        contentByte.MoveTo(x1, y1);
        contentByte.LineTo(x2, y2);
        contentByte.Stroke();
    }
    private static PdfPCell PhraseCell(Phrase phrase, int align)
    {
        PdfPCell cell = new PdfPCell(phrase);
        cell.BorderColor = iTextSharp.text.Color.WHITE;
        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 2f;
        cell.PaddingTop = 0f;
        return cell;
    }
    private static PdfPCell ImageCell(string path, float scale, int align)
    {
        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path));
        image.ScalePercent(scale);
        PdfPCell cell = new PdfPCell(image);
        cell.BorderColor = iTextSharp.text.Color.WHITE;
        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 0f;
        cell.PaddingTop = 0f;
        return cell;
    }
}