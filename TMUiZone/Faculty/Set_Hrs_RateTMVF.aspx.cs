using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;

public partial class Faculty_Set_Hrs_RateTMVF : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal con;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Portalcon = new Connection();
            con = new ServicePoratal();
            if (!IsPostBack)
            {


                if (Session["uname"].ToString() == null)
                {
                    Response.Redirect("../Default.aspx");
                }
                else
                {
                    bindDesignation();


                    ShowEMployeeDetails();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void bindDesignation()
    {
        string query = "";
        query = "Select No_,[First Name] FROM [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where [Status]='0' and  No_ like 'TMVF%'";
        SqlDataAdapter da = new SqlDataAdapter(query, con.Con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.DisConnect();
        txtDesig.DataSource = dt;
        txtDesig.DataTextField = "First Name";
        txtDesig.DataValueField = "No_";
        txtDesig.DataBind();
        txtDesig.Items.Insert(0, new ListItem("--Select--", "0"));

    }
   private void ShowEMployeeDetails()
{


    string sqlStatement = "";
    DataTable dt = new DataTable();
    if (txtDesig.SelectedValue != "0")
    {
        sqlStatement = "select No_,[First Name],convert(nvarchar(20),[Employment Date],105) 'Employment Date',[Global Dimension 1 Code],Dept,Reporting,Deputed,Designation,isnull((select top 1 [Rate] from HRMSPortal.dbo.tbl_tmvf_facultyRate where [tmvfid] collate Latin1_General_100_CS_AS = T.No_  order by convert(date,date,102) desc  ),0) as 'Amount',isnull((select top 1[Remark] from HRMSPortal.dbo.tbl_tmvf_facultyRate where [tmvfid] collate Latin1_General_100_CS_AS = T.No_ order by convert(date,date,102) desc),'') as 'Remark',isnull((select top 1 [date] from HRMSPortal.dbo.tbl_tmvf_facultyRate where [tmvfid] collate Latin1_General_100_CS_AS=T.No_ order by convert(date,date,102) desc ),'') as 'date' from(select No_,[First Name],[Employment Date],[Global Dimension 1 Code] ,(Select Name from [TMU$Dimension Value] where [Code]=[TMU$Employee].[Global Dimension 2 Code]) 'Dept',[Reporting Incharge Name] as 'Reporting',[Reporting Incharge Name] as 'Deputed',(Select top 1 [Designation Description] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Designation Master] where [Designation Code]=[TMU$Employee].[Designation Code])   as Designation,case when [Sanctioning Incharge]='' then HOD else [Sanctioning Incharge] end as 'AuthorisedID','' Remark,DateName( month , DateAdd( month ,MONTH(getdate()), -1 )) 'Month',YEAR(getdate()) 'Year' from [TMU$Employee] where [Status]=0) T where T.No_ like 'TMVF%' and   T.No_='" + txtDesig.SelectedValue + "' order by T.[Employment Date] desc";
    }
    else
    {
        sqlStatement = "select No_,[First Name],convert(nvarchar(20),[Employment Date],105) 'Employment Date',[Global Dimension 1 Code],Dept,Reporting,Deputed,Designation,isnull((select top 1 [Rate] from HRMSPortal.dbo.tbl_tmvf_facultyRate where [tmvfid] collate Latin1_General_100_CS_AS = T.No_ order by convert(date,date,102) desc ),0) as 'Amount',isnull((select top 1[Remark] from HRMSPortal.dbo.tbl_tmvf_facultyRate where [tmvfid] collate Latin1_General_100_CS_AS = T.No_ order by convert(date,date,102) desc),'') as 'Remark',isnull((select top 1 [date] from HRMSPortal.dbo.tbl_tmvf_facultyRate where [tmvfid] collate Latin1_General_100_CS_AS=T.No_ order by convert(date,date,102) desc ),'') as 'date' from(select No_,[First Name],[Employment Date],[Global Dimension 1 Code] ,(Select Name from [TMU$Dimension Value] where [Code]=[TMU$Employee].[Global Dimension 2 Code]) 'Dept',[Reporting Incharge Name] as 'Reporting',[Reporting Incharge Name] as 'Deputed',(Select top 1 [Designation Description] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Designation Master] where [Designation Code]=[TMU$Employee].[Designation Code])   as Designation,case when [Sanctioning Incharge]='' then HOD else [Sanctioning Incharge] end as 'AuthorisedID','' Remark,DateName( month , DateAdd( month ,MONTH(getdate()), -1 )) 'Month',YEAR(getdate()) 'Year' from [TMU$Employee] where [Status]=0) T where T.No_ like 'TMVF%' order by T.[Employment Date] desc";
    }


    SqlCommand sqlCmd = new SqlCommand(sqlStatement, Portalcon.Con);
    SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
    sqlDa.Fill(dt);
    if (dt.Rows.Count > 0)
    {
        grdEmployee.DataSource = dt;
        grdEmployee.DataBind();

    }
    else
    {
        grdEmployee.DataSource = "";
        grdEmployee.DataBind();

    }

}
    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        ShowEMployeeDetails();
    }

    protected void grdEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdEmployee.PageIndex = e.NewPageIndex;
        ShowEMployeeDetails();
    }
   
    protected void grdEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditEE")
        {
           
            int rowIndex = Convert.ToInt32(e.CommandArgument);


            GridViewRow row = grdEmployee.Rows[rowIndex];

            string EmpCode = (string)this.grdEmployee.DataKeys[rowIndex]["No_"];

            string Amount = (row.FindControl("txtAmount") as TextBox).Text;

            string Remarks = (row.FindControl("txtRemark") as TextBox).Text;

            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                SqlCommand cmd = new SqlCommand("proc_InsertTMVFRates", con2);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@No_", EmpCode);
                cmd.Parameters.Add("@Amount", Amount);
                cmd.Parameters.Add("@Remark", Remarks);
                cmd.Parameters.Add("@UserId", Session["uid"].ToString());

                if (con2.State == ConnectionState.Closed)
                    con2.Open();
                cmd.ExecuteNonQuery();
                con2.Close();
               
            } 
ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Amount Updated.')", true);      

        }
        

    }
}