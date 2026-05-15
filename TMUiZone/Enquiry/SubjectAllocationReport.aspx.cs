using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Drawing;
using System.Web.Services;
using System.Web.Script.Services;
public partial class Enquiry_SubjectAllocationReport : System.Web.UI.Page
{

    string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
    //string constr1 = ConfigurationSettings.AppSettings["strPortal"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["uid"].ToString() != null && Session["UserGroup"].ToString() == "PRINCIPAL")
            {

                Session["UserGroup"] = Session["UserGroup"].ToString();
                bindsubjectAllocationReport();

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
    public void bindsubjectAllocationReport()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            string str = ""; string Proc = "";
            if(RbtnTHEORY.Checked==true)
            {
                if (RbtnTHEORY.Checked == true && chkassign.Checked == true)
                {
                    Proc = "SP_ChoiceSubjectReport_Assign";
                    str = "THEORY";
                }
                else
                {



                    str = "THEORY";
                    Proc = "SP_ChoiceSubjectReport";
                }
            }
            if (RbtnLAB.Checked == true)
            {

                if (RbtnLAB.Checked == true && chkassign.Checked == true)
                {
                    Proc = "SP_ChoiceSubjectReport_LAB_Assign";
                    str = "LAB";
                }
                else
                {

                    Proc = "SP_ChoiceSubjectReport_LAB";
                    str = "LAB";
                }
            
            }
           
          

            SqlCommand cmd = new SqlCommand(Proc, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                cmd.Parameters.AddWithValue("@SubjectType", str);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    btnExport.Visible = true;
                }
                else
                {
                    btnExport.Visible = false;
                }
                grdSubjectAllocation.DataSource = dt;
                grdSubjectAllocation.DataBind();
                
           
        }
    }
    //protected void DrpSubjectType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //bindsubjectAllocationReport();
    //}
    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        string str="";
        if (chkassign.Checked == true) { str = "AssignSubjectList.xls"; } else { str = "ChoiceSubjectList.xls"; }
        
        Response.Clear(); Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename="+str+"");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            foreach (TableCell cell in grdSubjectAllocation.HeaderRow.Cells)
            {
                cell.BackColor = grdSubjectAllocation.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grdSubjectAllocation.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grdSubjectAllocation.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grdSubjectAllocation.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grdSubjectAllocation.RenderControl(hw);
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
}