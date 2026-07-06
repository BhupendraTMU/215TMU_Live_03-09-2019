using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_Employee_ITAssest : System.Web.UI.Page
{
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
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

                    ShowEMployeeDetails();

                }
            }
        }
        catch (Exception ex)
        {

        }





    }
    private void ShowEMployeeDetails()
    {

        DataTable dt = new DataTable();
        string id = string.Empty;
        string fname = string.Empty;
        string sname = string.Empty;
        string lname = string.Empty;
        string newName = string.Empty;
        string ccnameUN = Session["Company"].ToString();
        string rccname = ccnameUN.Replace(".", "_");
        string tbl_EmployeeActualpunchData = "[" + rccname + "$Employee" + "]";

        if (Session["GlobalDimension1Code"].ToString() == "TMMC" || Session["GlobalDimension1Code"].ToString() == "TMHS")
        {
            string sqlStatement = "";

            sqlStatement = "select *,'Approved' PMSStatus from (select *,(Select Name from [TMU$Dimension Value]   where [Code]=[TMU$Employee].[Global Dimension 2 Code]) 'Dept',  [Reporting Incharge Name] as 'Reporting',[Reporting Incharge Name] as 'Deputed' ,  isnull((select top 1 Remark from HRMSPortal.[dbo].[tbl_EmployeeSalaryHoldDetail]  where EmployeeCode collate Latin1_General_100_CS_AS =[TMU$Employee].No_ and   [month] =(select Month([Payroll Processing Month Date])  from [TMU$Pay Company Policy])   and [Year] =(select Year([Payroll Processing Month Date])  from [TMU$Pay Company Policy])   order by Id desc),'') as 'Remark' ,  isnull((select top 1 [Hold the Salary]   from [TMU$Pay Monthly Attendence Detail] where [Employee No]   collate Latin1_General_100_CS_AS =[TMU$Employee].No_ and [Month] =  (select Month([Payroll Processing Month Date])  from [TMU$Pay Company Policy]) and [Year]=(select Year([Payroll Processing Month Date])  from [TMU$Pay Company Policy])),'')     as 'Salary Hold',(Select top 1 [Designation Description] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Designation Master]   where [Designation Code]=[TMU$Employee].[Designation Code])   as Designation,case when [Sanctioning Incharge]='' then HOD else [Sanctioning Incharge] end as 'AuthorisedID' from [TMU$Employee] where [Payroll]!=1) T where AuthorisedID='" + Session["uid"].ToString() + "' and No_ not like 'TMVF%'";




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
        else
        {
            string sqlStatement = "";




            sqlStatement = "select [First Name],[No_],(Select Name from [TMU$Dimension Value] where [Code]=[TMU$Employee].[Global Dimension 2 Code]) 'Dept', [Reporting Incharge Name] as 'Reporting',[Reporting Incharge Name] as 'Deputed' ,isnull((select top 1 Remark from HRMSPortal.[dbo].[tbl_EmployeeSalaryHoldDetail] with (NOLOCK) where EmployeeCode collate Latin1_General_100_CS_AS =[TMU$Employee].No_ and [month] =(select Month([Payroll Processing Month Date]) from [TMU$Pay Company Policy]) and [Year]=(select Year([Payroll Processing Month Date])  from [TMU$Pay Company Policy]) order by Id desc),'') as 'Remark' ,(Select top 1 [Designation Description] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Designation Master] with (NOLOCK) where [Designation Code]=[TMU$Employee].[Designation Code])   as Designation  from [TMU$Employee] with (NOLOCK) where   HOD='" + Session["uid"].ToString() + "' and No_ in  (select  [Issue ID] FROM [NAAC_ADV_TEST].[dbo].[TMU Advertisement$Alloted Person] with (NOLOCK)  where [Inactive]=0) union   " +
                "select [First Name],[No_],(Select Name from [TMU$Dimension Value] where [Code]=[TMU$Employee].[Global Dimension 2 Code]) 'Dept', [Reporting Incharge Name] as 'Reporting',[Reporting Incharge Name] as 'Deputed' ,isnull((select top 1 Remark from HRMSPortal.[dbo].[tbl_EmployeeSalaryHoldDetail] with (NOLOCK) where EmployeeCode collate Latin1_General_100_CS_AS =[TMU$Employee].No_ and [month] =(select Month([Payroll Processing Month Date]) from [TMU$Pay Company Policy]) and [Year]=(select Year([Payroll Processing Month Date])  from [TMU$Pay Company Policy]) order by Id desc),'') as 'Remark' ,(Select top 1 [Designation Description] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Designation Master] with (NOLOCK) where [Designation Code]=[TMU$Employee].[Designation Code])   as Designation  from [TMU$Employee] with (NOLOCK) where   No_='" + Session["uid"].ToString() + "' and No_ in  (select  [Issue ID] FROM [NAAC_ADV_TEST].[dbo].[TMU Advertisement$Alloted Person] with (NOLOCK)  where [Inactive]=0)";




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

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Controls.Clear();
        grdAssestDetails.Visible = false;
        string ID = (sender as LinkButton).CommandArgument;
        lblNotification.Text = "Employee No :-" + ID;
        Session["EmployeeCodeV"] = ID;
        ViewAttendance(ID);
        ModalPopupExtender1.Show();

    }
    protected void btntransfer_Click(object sender, EventArgs e)
    {
        string ID = (sender as LinkButton).CommandArgument;
        lblNotification.Text = "Employee No :- " + ID;
        Session["EmployeeCodeV"] = ID;
        LinkButton btn = (LinkButton)sender;


        GridViewRow row1 = (GridViewRow)btn.NamingContainer;

        Label lblEmpName = (Label)row1.FindControl("EmpName");

        string empName = lblEmpName.Text;


        Label lblEmpCode = (Label)row1.FindControl("EmpCode");
        string empCode = lblEmpCode.Text;
       

        
        Label2.Text = lblEmpName.Text;
        Label1.Text = lblEmpCode.Text;

        GetAssetsForEmployee(ID);
        string sqlStatement = "select No_ EmployeeID,[First Name] EmployeeName,(select [First Name] from [TMU$Employee] where No_=EMP.No_) as EMP_Name  from [TMU$Employee] EMP with (NOLOCK) where [Status]=0 and  HOD='" + Session["uid"].ToString() + "'  ";


        DataTable dt = new DataTable();

        SqlCommand sqlCmd = new SqlCommand(sqlStatement, Portalcon.Con);
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        sqlDa.Fill(dt);
        if (dt.Rows.Count > 0)
        {



            foreach (GridViewRow row in GridView1.Rows)
            {
                DropDownList drpEmp = row.FindControl("drpEmployeee") as DropDownList;
                if (drpEmp != null)
                {
                    drpEmp.DataSource = dt;
                    drpEmp.DataTextField = "EmployeeName";
                    drpEmp.DataValueField = "EmployeeID";
                    drpEmp.DataBind();
                    drpEmp.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
        }

        // 3️⃣ Show Modal
        ModalPopupExtender2.Show();




    }
    public void GetAssetsForEmployee(string ID)
    {

        SqlCommand cmd = new SqlCommand("proc_getITAssets", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@UserId", ID);

        if (con1.State == ConnectionState.Closed)
            con1.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        if (dtCL.Rows.Count > 0)
        {
            GridView1.Visible = true;
            GridView1.DataSource = dtCL;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = "";
            GridView1.DataBind();
            GridView1.Visible = false;
        }

    }
    public void ViewAsset(string ID)
    {

        SqlCommand cmd = new SqlCommand("proc_getITAssets", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@UserId", ID);

        if (con1.State == ConnectionState.Closed)
            con1.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        if (dtCL.Rows.Count > 0)
        {
            GridView1.Visible = true;
            GridView1.DataSource = dtCL;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = "";
            GridView1.DataBind();
            GridView1.Visible = false;
        }

    }
    public void ViewAttendance(string ID)
    {

        SqlCommand cmd = new SqlCommand("proc_getITAssets", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@UserId", ID);

        if (con1.State == ConnectionState.Closed)
            con1.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        if (dtCL.Rows.Count > 0)
        {
            grdAssestDetails.Visible = true;
            grdAssestDetails.DataSource = dtCL;
            grdAssestDetails.DataBind();
        }
        else
        {
            grdAssestDetails.DataSource = "";
            grdAssestDetails.DataBind();
            grdAssestDetails.Visible = false;
        }

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Trans")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = GridView1.Rows[rowIndex];

            DropDownList drpEmployeee = (DropDownList)row.FindControl("drpEmployeee");

            string selectedValue = drpEmployeee.SelectedValue;
            string selectedText = drpEmployeee.SelectedItem.Text;

            Label lblAssetNo = (Label)row.FindControl("lblAssetNoTransfer");
            string assetNo = lblAssetNo.Text;

            Label lblIndentNo = (Label)row.FindControl("lblIndentNo");
            string IndentNo = lblIndentNo.Text;
            Label lblTransferItem = (Label)row.FindControl("lblTransferItem");
            string ItemCode = lblTransferItem.Text;

            SqlCommand cmd = new SqlCommand("Proc_ItTransfer", con1);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@HODId", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@OldEmpId", Label1.Text);
            cmd.Parameters.AddWithValue("@NewEmpId", selectedValue);
            cmd.Parameters.AddWithValue("@assetNo", assetNo);
            cmd.Parameters.AddWithValue("@IndentNo", IndentNo);
            cmd.Parameters.AddWithValue("@ItemCode", ItemCode);

            if (con1.State == ConnectionState.Closed)
                con1.Open();

            cmd.ExecuteNonQuery();
            con1.Close();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg",
                "alert('Asset transferred successfully'); ", true);
        }







    }
}