using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Faculty_VerifyEmployeeList : System.Web.UI.Page
{

    ServicePoratal con;
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            con = new ServicePoratal();
            if (!IsPostBack)
            {
                if (Session["uid"].ToString() == "TMU00049" || Session["uid"].ToString() == "TMU00075" || Session["uid"].ToString() == "TMU06618" || Session["uid"].ToString() == "TMU07987" || Session["uid"].ToString() == "TMU00142" || Session["uid"].ToString() == "TMU08719" || Session["uid"].ToString() == "TMU04490")
                {
                    pnlReport.Visible = true;
                    pnlmsg.Visible = false;
                    BindYear();
                    //bindDesignation();
                    bindDepartment();
                    show_Report();
                    DropDownList1.SelectedValue = System.DateTime.Now.ToString("MM");
                    ddlYear1.SelectedValue = System.DateTime.Now.ToString("yyyy");
                    DropDownList1.SelectedValue = System.DateTime.Now.ToString("MM");
                }
                else
                {
                    pnlmsg.Visible = true;
                    pnlReport.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../Default.aspx");
        }


    }

    public void BindYear()
    {

        int Currentyear = System.DateTime.Now.Year;

        for (int i = Currentyear - 1; i <= Currentyear; i++)
            ddlYear1.Items.Add(i.ToString());
    }
    //public void bindDesignation()
    //{
    //    string query = "Select distinct [Designation Code], [Designation Description] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Designation Master] where [Designation Description]!=''";
    //    SqlDataAdapter da = new SqlDataAdapter(query, con.Con);
    //    DataTable dt = new DataTable();

    //    da.Fill(dt);
    //    con.DisConnect();
    //    drpDesignation.DataSource = dt;
    //    drpDesignation.DataTextField = "Designation Description";
    //    drpDesignation.DataValueField = "Designation Code";
    //    drpDesignation.DataBind();
    //    drpDesignation.Items.Insert(0, new ListItem("--Select--", "0"));

    //}
    public void bindDepartment()
    {

        string query = "select distinct [Department Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where [Status]=0 order by [Department Name]";
        SqlDataAdapter da = new SqlDataAdapter(query, con.Con);
        DataTable dt = new DataTable();

        da.Fill(dt);
        con.DisConnect();
        drpDepartment.DataSource = dt;
        drpDepartment.DataTextField = "Department Name";
        drpDepartment.DataValueField = "Department Name";
        drpDepartment.DataBind();
        drpDepartment.Items.Insert(0, new ListItem("--Select--", "0"));


    }
    public void show_Report()
    {
        string query = "";
        if (txtEmployeeCode.Text != "")
        {
            query = "Select * from (select EmployeeCode,isnull((Select [Global Dimension 1 Code] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.HOD),'') College,isnull((Select [First Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.HOD),'') ApprovedBY,(Select [Department Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.EmployeeCode) as 'Department',(Select [First Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.EmployeeCode) as 'Employee Name',Year,month,Date,case when Hold=1 then 'YES' else 'NO' end 'HOLD',case when verify=1 then 'YES' else 'NO' end as 'Verify',Remark,(Select top 1 [Designation Description] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Designation Master] where [Designation Code]=(Select [Designation Code] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.EmployeeCode)) as Designation,HOD,(isnull((Select distinct [E-Mail] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.HOD),'')) 'HODMail',(isnull((Select distinct [Mobile Phone No_] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.HOD),'')) 'HODMobile' from HRMSPortal.[dbo].[tbl_EmployeeSalaryHoldDetail] tblhold where  month= REPLACE(LTRIM(REPLACE('" + DropDownList1.SelectedValue + "', '0', ' ')), ' ', '0')  and Year='" + ddlYear1.SelectedValue + "') T where T.EmployeeCode='" + txtEmployeeCode.Text + "'";

        }
        if (drpDepartment.SelectedValue != "0")
        {
            query = "Select * from (select EmployeeCode,isnull((Select [Global Dimension 1 Code] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.HOD),'') College,isnull((Select [First Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.HOD),'') ApprovedBY,(Select [Department Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.EmployeeCode) as 'Department',(Select [First Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.EmployeeCode) as 'Employee Name',Year,month,Date,case when Hold=1 then 'YES' else 'NO' end 'HOLD',case when verify=1 then 'YES' else 'NO' end as 'Verify',Remark,(Select top 1 [Designation Description] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Designation Master] where [Designation Code]=(Select [Designation Code] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.EmployeeCode)) as Designation,HOD,(isnull((Select distinct [E-Mail] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.HOD),'')) 'HODMail',(isnull((Select distinct [Mobile Phone No_] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.HOD),'')) 'HODMobile' from HRMSPortal.[dbo].[tbl_EmployeeSalaryHoldDetail] tblhold where  month= REPLACE(LTRIM(REPLACE('" + DropDownList1.SelectedValue + "', '0', ' ')), ' ', '0')  and Year='" + ddlYear1.SelectedValue + "') T where T.Department='" + drpDepartment.SelectedValue + "'";

        }
        if (drpDepartment.SelectedValue != "0" && txtEmployeeCode.Text != "")
        {
            query = "Select * from (select EmployeeCode,isnull((Select [Global Dimension 1 Code] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.HOD),'') College,isnull((Select [First Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.HOD),'') ApprovedBY,(Select [Department Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.EmployeeCode) as 'Department',(Select [First Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.EmployeeCode) as 'Employee Name',Year,month,Date,case when Hold=1 then 'YES' else 'NO' end 'HOLD',case when verify=1 then 'YES' else 'NO' end as 'Verify',Remark,(Select top 1 [Designation Description] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Designation Master] where [Designation Code]=(Select [Designation Code] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.EmployeeCode)) as Designation,HOD,(isnull((Select distinct [E-Mail] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.HOD),'')) 'HODMail',(isnull((Select distinct [Mobile Phone No_] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.HOD),'')) 'HODMobile' from HRMSPortal.[dbo].[tbl_EmployeeSalaryHoldDetail] tblhold where  month= REPLACE(LTRIM(REPLACE('" + DropDownList1.SelectedValue + "', '0', ' ')), ' ', '0')  and Year='" + ddlYear1.SelectedValue + "') T where T.Department='" + drpDepartment.SelectedValue + "' and T.EmployeeCode='" + txtEmployeeCode.Text + "'";

        }
        if (drpDepartment.SelectedValue == "0" && txtEmployeeCode.Text == "")
        {
            query = "select EmployeeCode,isnull((Select [Global Dimension 1 Code] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.HOD),'') College,isnull((Select [First Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.HOD),'') ApprovedBY,(Select [Department Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.EmployeeCode) as 'Department',(Select [First Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.EmployeeCode) as 'Employee Name',Year,month,Date,case when Hold=1 then 'YES' else 'NO' end 'HOLD',case when verify=1 then 'YES' else 'NO' end as 'Verify',Remark,(Select top 1 [Designation Description] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Designation Master] where [Designation Code]=(Select [Designation Code] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.EmployeeCode)) as Designation,HOD,(isnull((Select distinct [E-Mail] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.HOD),'')) 'HODMail',(isnull((Select distinct [Mobile Phone No_] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.HOD),'')) 'HODMobile' from HRMSPortal.[dbo].[tbl_EmployeeSalaryHoldDetail] tblhold where  month= REPLACE(LTRIM(REPLACE('" + DropDownList1.SelectedValue + "', '0', ' ')), ' ', '0')  and Year='" + ddlYear1.SelectedValue + "'";

        }
        SqlDataAdapter da = new SqlDataAdapter(query, con.Con);
        DataTable dt = new DataTable();

        da.Fill(dt);
        con.DisConnect();
        GridView1.DataSource = dt;
        GridView1.DataBind();




    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        GridView1.AllowPaging = false;

        show_Report();






        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        GridView1.RenderControl(htmlWrite);

        Response.Clear();
        string str = "Verified_Employee_Report";
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();
    }
    protected void btnGet_Click(object sender, EventArgs e)
    {
        show_Report();

    }
}