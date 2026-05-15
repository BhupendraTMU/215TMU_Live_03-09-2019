using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using Utility;
using System.Configuration;
using System;

public partial class Student_ExamFormStatus : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal conn;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Portalcon = new Connection();
        conn = new ServicePoratal();
        if (!IsPostBack)
        {
            bindStudentExamDetails();
        }
    }
   
 
   



    protected void Rblist_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
      
    }
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {

    }
    protected void BtnRejected_Click(object sender, EventArgs e)
    {

    }
    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Chekap_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void GrdExamList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdExamList.PageIndex = e.NewPageIndex;
        bindStudentExamDetails();
    }
    public void bindStudentExamDetails()
    {
        try
        {

            SqlCommand cmd = new SqlCommand("Sp_GetStudentExaminationDetailsforStatusforstudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
         
            cmd.Parameters.AddWithValue("@Appear", ddlReaapear.SelectedValue);
            cmd.Parameters.AddWithValue("@LoginId", Session["uid"].ToString());
            

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            GrdExamList.DataSource = dt;
            GrdExamList.DataBind();
            if (dt.Rows.Count > 0)
            {
                GrdExamList.Visible = true;

            }

            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        GrdExamList.AllowPaging = false;

        bindStudentExamDetails();






        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        GrdExamList.RenderControl(htmlWrite);

        Response.Clear();
        string str = "ExamFormStatusReport";
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();
    }
    protected void BtnShow_Click(object sender, EventArgs e)
    {

        bindStudentExamDetails();

    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void chkStudent_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void ddlReaapear_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindStudentExamDetails();
    }
}