using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class Faculty_FacultyLoad : System.Web.UI.Page
{
    TMUConnection con; string CollegeCode = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Session["UserGroup"].ToString() == "PRINCIPAL" || Session["UserGroup"].ToString() == "ADMIN")
            {
                try
                {
                    CollegeCode = Session["GlobalDimension1Code"].ToString();
                    if (!IsPostBack)
                    {
                        GetFacultyList();
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
    public void GetAttendanceList()
    {
        con = new TMUConnection();
        SqlCommand cmd = new SqlCommand("SP_GetFacultyLoadBetweenDate", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"]);
        cmd.Parameters.Add("@FromDate", txtFromtDate.Text);
        cmd.Parameters.Add("@ToDate", txtToDate.Text);
        cmd.Parameters.Add("@FacultyCode", ddlFaculty.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            DataRow toInsert = dt.NewRow();
            dt.Rows.InsertAt(toInsert, dt.Rows.Count + 1);
        }
        decimal TC = 0, TA = 0, LC = 0, LA = 0, PC = 0, PA = 0, CC = 0, CA = 0;
        for (int i = 0; i < dt.Rows.Count-1; i++)
        {           
            if (dt.Rows[i][0].ToString() != dt.Rows[i + 1][0].ToString())
            {
                
                TC += Convert.ToDecimal(dt.Rows[i][3]);
                TA+= Convert.ToDecimal(dt.Rows[i][4]);
                LC += Convert.ToDecimal(dt.Rows[i][5]);
                LA+= Convert.ToDecimal(dt.Rows[i][6]);
                PC += Convert.ToDecimal(dt.Rows[i][7]);
                PA+= Convert.ToDecimal(dt.Rows[i][8]);
                CC += Convert.ToDecimal(dt.Rows[i][9]);
                CA+= Convert.ToDecimal(dt.Rows[i][10]);
                DataRow toInsert = dt.NewRow();
                int a = i;                
                dt.Rows.InsertAt(toInsert, i + 1);
                i++;
                dt.Rows[i][2] = "Total";               
                dt.Rows[i][3] = TC;  dt.Rows[i][4] = TA; dt.Rows[i][5] = LC;  dt.Rows[i][6] = LA;  dt.Rows[i][7] = PC;  dt.Rows[i][8] = PA;  dt.Rows[i][9] = CC; dt.Rows[i][10] = CA;
                TC = 0; TA = 0; LC = 0; LA = 0; PC = 0; PA = 0; CC = 0; CA = 0;

            }
            else
            {
                TC += Convert.ToDecimal(dt.Rows[i][3]);
                TA += Convert.ToDecimal(dt.Rows[i][4]);
                LC += Convert.ToDecimal(dt.Rows[i][5]);
                LA += Convert.ToDecimal(dt.Rows[i][6]);
                PC += Convert.ToDecimal(dt.Rows[i][7]);
                PA += Convert.ToDecimal(dt.Rows[i][8]);
                CC += Convert.ToDecimal(dt.Rows[i][9]);
                CA += Convert.ToDecimal(dt.Rows[i][10]);
            }

        }
        con.DisConnect();
        grdLoad.DataSource = dt;
        grdLoad.DataBind();
        for (int i = 0; i < grdLoad.Rows.Count; i++)
        {
            if (grdLoad.Rows[i].Cells[2].Text  == "Total")
            {
                grdLoad.Rows[i].BackColor = System.Drawing.Color.Turquoise;
            }
        }
        decimal STC = 0, STA = 0, SLC = 0,   SLA = 0, SPC = 0, SPA = 0, SCC = 0, SCA = 0;
        STC = Convert.ToDecimal(dt.Compute("Sum(TC)", "").ToString());        
        SLC = Convert.ToDecimal(dt.Compute("Sum(LC)", "").ToString());        
        SPC = Convert.ToDecimal(dt.Compute("Sum(PC)", "").ToString());        
        SCC = Convert.ToDecimal(dt.Compute("Sum(CC)", "").ToString());
        //STA = Convert.ToDecimal(dt.Compute("Sum(TA)", "").ToString());
        //SLA = Convert.ToDecimal(dt.Compute("Sum(LA)", "").ToString());
        //SPA = Convert.ToDecimal(dt.Compute("Sum(PA)", "").ToString());
        //SCA = Convert.ToDecimal(dt.Compute("Sum(CA)", "").ToString());
        for (int i = 0; i < grdLoad.Rows.Count; i++)
        {
            if (STC == 0)
            {
                grdLoad.Rows[i].Cells[3].Visible = false;
                grdLoad.Rows[i].Cells[4].Visible = false;
                grdLoad.HeaderRow.Cells[3].Visible = false;
                grdLoad.HeaderRow.Cells[4].Visible = false;
            }
            if (SLC == 0)
            {
                grdLoad.Rows[i].Cells[5].Visible = false;
                grdLoad.Rows[i].Cells[6].Visible = false;
                grdLoad.HeaderRow.Cells[5].Visible = false;
                grdLoad.HeaderRow.Cells[6].Visible = false;
            } if (SPC == 0)
            {
                grdLoad.Rows[i].Cells[7].Visible = false;
                grdLoad.Rows[i].Cells[8].Visible = false;
                grdLoad.HeaderRow.Cells[7].Visible = false;
                grdLoad.HeaderRow.Cells[8].Visible = false;
            } if (SCC == 0)
            {
                grdLoad.Rows[i].Cells[9].Visible = false;
                grdLoad.Rows[i].Cells[10].Visible = false;
                grdLoad.HeaderRow.Cells[9].Visible = false;
                grdLoad.HeaderRow.Cells[10].Visible = false;
            }
        }
        
        
        
        
        
        //if (STC == 0) { grdLoad.Columns[3].Visible = false; grdLoad.Columns[4].Visible = false; }
        //if (SLC == 0) { grdLoad.Columns[5].Visible = false; grdLoad.Columns[6].Visible = false; }
        //if (SPC == 0) { grdLoad.Columns[7].Visible = false; grdLoad.Columns[8].Visible = false; }



        GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell HeaderCell = new TableCell(); 
        HeaderCell.Text = "Faculty";
        HeaderGridRow.Cells.Add(HeaderCell);
        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        HeaderCell = new TableCell();
        HeaderCell.Text = "Subject";
        HeaderGridRow.Cells.Add(HeaderCell);
        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        HeaderCell = new TableCell();
        HeaderCell.Text = "Classification";
        HeaderGridRow.Cells.Add(HeaderCell);
        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        if(STC != 0)
        {
        HeaderCell = new TableCell();
        HeaderCell.Text = "Theory";
        HeaderCell.ColumnSpan = 2;
        HeaderGridRow.Cells.Add(HeaderCell);
        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        }
        if (SLC != 0)
        {
            HeaderCell = new TableCell();
            HeaderCell.Text = "LAB";
            HeaderCell.ColumnSpan = 2;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        }
        if (SPC != 0)
        {
            HeaderCell = new TableCell();
            HeaderCell.Text = "Practical";
            HeaderCell.ColumnSpan = 2;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;            
        }

        if (SCC != 0)
        {
            HeaderCell = new TableCell();
            HeaderCell.Text = "Clinic";
            HeaderCell.ColumnSpan = 2;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        }
        grdLoad.Controls[0].Controls.AddAt(0, HeaderGridRow);
        grdLoad.HeaderStyle.Font.Bold = true;
        grdLoad.Rows[grdLoad.Rows.Count-1].Visible = false;

    }
    public void GetFacultyList()
    {
        con = new TMUConnection();
        SqlCommand cmd = new SqlCommand("SP_GetFacultyFromTimeTable_CollegeWise", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"]);       
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.DisConnect();
        ddlFaculty.DataSource = dt;
        ddlFaculty.DataTextField = "Name";
        ddlFaculty.DataValueField = "Code";
        ddlFaculty.DataBind();
    }
    protected void grdLoad_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdLoad.PageIndex = e.NewPageIndex;
        GetAttendanceList();
    }
    protected void BtnShow_Click(object sender, EventArgs e)
    {
        if (txtFromtDate.Text == "" || txtToDate.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Date Could Not be Blank');", true);
            return;
        }
        if (Convert.ToDateTime(txtFromtDate.Text) > Convert.ToDateTime(txtToDate.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'From Date Could Not be greater than To Date');", true);                    return;
        }
        else
        {
            GetAttendanceList();
        }
    }
    protected void grdLoad_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int RowSpan = 2;
        for (int i = grdLoad.Rows.Count - 2; i >= 0; i--)
        {
            GridViewRow currRow = grdLoad.Rows[i];
            GridViewRow prevRow = grdLoad.Rows[i + 1];
            if (currRow.Cells[0].Text == prevRow.Cells[0].Text)
            {
                currRow.Cells[0].RowSpan = RowSpan;
                prevRow.Cells[0].Visible = false;
                RowSpan += 1;
            }
            else
            {
                RowSpan = 2;
            }
        }
       
    }
    protected void grdLoad_DataBound(object sender, EventArgs e)
    {      
        grdLoad.HeaderRow.Cells[0].Text = "";
        grdLoad.HeaderRow.Cells[1].Text = "";
        grdLoad.HeaderRow.Cells[2].Text = "";
        grdLoad.HeaderRow.Cells[3].Text = "Contact Hr.";
        grdLoad.HeaderRow.Cells[4].Text = "Actual Hr.";
        grdLoad.HeaderRow.Cells[5].Text = "Contact Hr.";
        grdLoad.HeaderRow.Cells[6].Text = "Actual Hr.";
        grdLoad.HeaderRow.Cells[7].Text = "Contact Hr";
        grdLoad.HeaderRow.Cells[8].Text = "Actual Hr.";
        grdLoad.HeaderRow.Cells[9].Text = "Contact Hr.";
        grdLoad.HeaderRow.Cells[10].Text = "Actual Hr.";

       
    }
    protected void grdLoad_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    GridView HeaderGrid = (GridView)sender;
        //    GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        //    TableCell HeaderCell = new TableCell();
        //    HeaderCell.Text = "Faculty";
        //    HeaderGridRow.Cells.Add(HeaderCell);
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "Subject";
        //    HeaderGridRow.Cells.Add(HeaderCell);
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "Classification";
        //    HeaderGridRow.Cells.Add(HeaderCell);
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "Theory";
        //    HeaderCell.ColumnSpan = 2;
        //    HeaderGridRow.Cells.Add(HeaderCell);
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "LAB";
        //    HeaderCell.ColumnSpan = 2;
        //    HeaderGridRow.Cells.Add(HeaderCell);
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "Practical";
        //    HeaderCell.ColumnSpan = 2;
        //    HeaderGridRow.Cells.Add(HeaderCell);
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "Clinic";
        //    HeaderCell.ColumnSpan = 2;
        //    HeaderGridRow.Cells.Add(HeaderCell);
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    grdLoad.Controls[0].Controls.AddAt(0, HeaderGridRow);
        //    grdLoad.HeaderStyle.Font.Bold = true;

        //}
        //===================================== 
       
    }
}