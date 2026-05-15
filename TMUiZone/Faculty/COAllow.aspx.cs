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
using System.Web.UI.HtmlControls;
public partial class Faculty_COAllow : System.Web.UI.Page
{
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
             if (Session["uid"].ToString() == "TMU00628")
            {
                binddata();
            }
            else
            {
                Response.Redirect("../Default.aspx");
            }
        }
    }


    public void binddata()
    {
        con1.Open();
        string sqlStatement = "SELECT [ID],upper([EmployeeCode]) as 'EmployeeCode',(select [First Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=Upper([tbl_CoAllowEmployee].EmployeeCode)) as 'EmployeeName',  case when [Status]=1 then 'Active' else 'Inactive' end  as 'Status' FROM [dbo].[tbl_CoAllowEmployee]";
        SqlCommand sqlCmd = new SqlCommand(sqlStatement, con1);
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataTable dt = new DataTable();
        sqlDa.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            grdApproval.DataSource = dt;
            grdApproval.DataBind();
            
        }
        con1.Close();


    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        con1.Open();
        SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[tbl_CoAllowEmployee]  ([EmployeeCode],[Status])  VALUES    ('" + txtEmployee.Text + "'   ,1)", con1);
        cmd.ExecuteNonQuery();
        con1.Close();
        binddata();
    }
}