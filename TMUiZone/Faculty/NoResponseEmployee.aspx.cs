using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class Faculty_NoResponseEmployee : System.Web.UI.Page
{
    ServicePoratal con;
    Connection Portalcon;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            con = new ServicePoratal();
            Portalcon = new Connection();
            if (!IsPostBack)
            {
                if (Session["uid"].ToString() == "TMU00049" || Session["uid"].ToString() == "TMU00075" || Session["uid"].ToString() == "TMU06618" || Session["uid"].ToString() == "TMU07987" || Session["uid"].ToString() == "TMU00142" || Session["uid"].ToString() == "TMU08719" || Session["uid"].ToString() == "TMU04490")
                {
                    pnlReport.Visible = true;
                    pnlmsg.Visible = false;
                    BindYear();
                    bindDesignation();
                    bindDepartment();
                    DropDownList1.SelectedValue = System.DateTime.Now.ToString("MM");
                    ddlYear1.SelectedValue = System.DateTime.Now.ToString("yyyy");
                    DropDownList1.SelectedValue = System.DateTime.Now.ToString("MM");
                    show_Report();

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
    public void bindDesignation()
    {
        string query = "Select distinct [Designation Code], [Designation Description] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Designation Master] where [Designation Description]!=''";
        SqlDataAdapter da = new SqlDataAdapter(query, con.Con);
        DataTable dt = new DataTable();

        da.Fill(dt);
        con.DisConnect();
        drpDesignation.DataSource = dt;
        drpDesignation.DataTextField = "Designation Description";
        drpDesignation.DataValueField = "Designation Code";
        drpDesignation.DataBind();
        drpDesignation.Items.Insert(0, new ListItem("--Select--", "0"));

    }
    public void bindDepartment()
    {

        string query = "select distinct [Department Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where [Status]=0";
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
        if (drpDesignation.SelectedValue != "0")
        {
            if (txtHOD.Text == "")
            {
                query = "select * from (select No_ EmployeeCode,[First Name] as 'Employee Name',[Global Dimension 1 Code] College,(Select top 1 [Designation Description] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Designation Master] where [Designation Code]=EMP.[Designation Code]) as Designation,HOD as  'HOD ID' ,isnull((Select [First Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=EMP.HOD),'') 'HOD Name', [Department Name]  as 'Department',isnull((Select [E-Mail] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=EMP.HOD),'') 'HODEmail',isnull((Select [Mobile Phone No_] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=EMP.HOD),'') 'HODMobile' from [TMU$Employee] EMP where (Status=0 or (month([Leave Date]) =REPLACE(LTRIM(REPLACE('" + DropDownList1.SelectedValue + "', '0', ' ')), ' ', '0') and YEAR([Leave Date])  ='" + ddlYear1.SelectedValue + "')) and No_ collate Latin1_General_CI_AI not in (Select EmployeeCode from HRMSPortal.dbo.tbl_EmployeeSalaryHoldDetail where month collate Latin1_General_CI_AI=REPLACE(LTRIM(REPLACE('" + DropDownList1.SelectedValue + "', '0', ' ')), ' ', '0') and Year collate Latin1_General_CI_AI ='" + ddlYear1.SelectedValue + "') ) T where T.Designation='" + drpDesignation.SelectedItem.Text + "' and   HOD=upper('" + txtHOD.Text + "') and No_ like 'TMU%'";

            }
            else
            {
                query = "select * from (select No_ EmployeeCode,[First Name] as 'Employee Name',[Global Dimension 1 Code] College,(Select top 1 [Designation Description] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Designation Master] where [Designation Code]=EMP.[Designation Code]) as Designation,HOD as  'HOD ID' ,isnull((Select [First Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=EMP.HOD),'') 'HOD Name', [Department Name]  as 'Department',isnull((Select [E-Mail] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=EMP.HOD),'') 'HODEmail',isnull((Select [Mobile Phone No_] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=EMP.HOD),'') 'HODMobile' from [TMU$Employee] EMP where (Status=0 or (month([Leave Date]) =REPLACE(LTRIM(REPLACE('" + DropDownList1.SelectedValue + "', '0', ' ')), ' ', '0') and YEAR([Leave Date])  ='" + ddlYear1.SelectedValue + "')) and No_ collate Latin1_General_CI_AI not in (Select EmployeeCode from HRMSPortal.dbo.tbl_EmployeeSalaryHoldDetail where month collate Latin1_General_CI_AI=REPLACE(LTRIM(REPLACE('" + DropDownList1.SelectedValue + "', '0', ' ')), ' ', '0') and Year collate Latin1_General_CI_AI ='" + ddlYear1.SelectedValue + "') ) T where T.Designation='" + drpDesignation.SelectedItem.Text + "' and No_ like 'TMU%' ";

            }
        }
        if (drpDepartment.SelectedValue != "0")
        {
            if (txtHOD.Text != "")
            {
                query = "select * from (select No_ EmployeeCode,[First Name] as 'Employee Name',[Global Dimension 1 Code] College,(Select top 1 [Designation Description] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Designation Master] where [Designation Code]=EMP.[Designation Code]) as Designation,HOD as  'HOD ID' ,isnull((Select [First Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=EMP.HOD),'') 'HOD Name', [Department Name]  as 'Department',isnull((Select [E-Mail] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=EMP.HOD),'') 'HODEmail',isnull((Select [Mobile Phone No_] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=EMP.HOD),'') 'HODMobile' from [TMU$Employee] EMP where (Status=0 or (month([Leave Date]) =REPLACE(LTRIM(REPLACE('" + DropDownList1.SelectedValue + "', '0', ' ')), ' ', '0') and YEAR([Leave Date])  ='" + ddlYear1.SelectedValue + "')) and No_ collate Latin1_General_CI_AI not in (Select EmployeeCode from HRMSPortal.dbo.tbl_EmployeeSalaryHoldDetail where month collate Latin1_General_CI_AI=REPLACE(LTRIM(REPLACE('" + DropDownList1.SelectedValue + "', '0', ' ')), ' ', '0') and Year collate Latin1_General_CI_AI ='" + ddlYear1.SelectedValue + "') ) T where T.Department='" + drpDepartment.SelectedValue + "'  and   HOD=upper('" + txtHOD.Text + "') and No_ like 'TMU%'";
            }
            else
            {
                query = "select * from (select No_ EmployeeCode,[First Name] as 'Employee Name',[Global Dimension 1 Code] College,(Select top 1 [Designation Description] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Designation Master] where [Designation Code]=EMP.[Designation Code]) as Designation,HOD as  'HOD ID' ,isnull((Select [First Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=EMP.HOD),'') 'HOD Name', [Department Name]  as 'Department',isnull((Select [E-Mail] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=EMP.HOD),'') 'HODEmail',isnull((Select [Mobile Phone No_] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=EMP.HOD),'') 'HODMobile' from [TMU$Employee] EMP where (Status=0 or (month([Leave Date]) =REPLACE(LTRIM(REPLACE('" + DropDownList1.SelectedValue + "', '0', ' ')), ' ', '0') and YEAR([Leave Date])  ='" + ddlYear1.SelectedValue + "')) and No_ collate Latin1_General_CI_AI not in (Select EmployeeCode from HRMSPortal.dbo.tbl_EmployeeSalaryHoldDetail where month collate Latin1_General_CI_AI=REPLACE(LTRIM(REPLACE('" + DropDownList1.SelectedValue + "', '0', ' ')), ' ', '0') and Year collate Latin1_General_CI_AI ='" + ddlYear1.SelectedValue + "') ) T where T.Department='" + drpDepartment.SelectedValue + "' and No_ like 'TMU%'";
            }
        }
        if (drpDepartment.SelectedValue != "0" && drpDesignation.SelectedValue != "0")
        {
            if (txtHOD.Text != "")
            {
                query = "select * from (select No_ EmployeeCode,[First Name] as 'Employee Name',[Global Dimension 1 Code] College,(Select top 1 [Designation Description] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Designation Master] where [Designation Code]=EMP.[Designation Code]) as Designation,HOD as  'HOD ID' ,isnull((Select [First Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=EMP.HOD),'') 'HOD Name', [Department Name]  as 'Department',isnull((Select [E-Mail] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=EMP.HOD),'') 'HODEmail',isnull((Select [Mobile Phone No_] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=EMP.HOD),'') 'HODMobile' from [TMU$Employee] EMP where (Status=0 or (month([Leave Date]) =REPLACE(LTRIM(REPLACE('" + DropDownList1.SelectedValue + "', '0', ' ')), ' ', '0') and YEAR([Leave Date])  ='" + ddlYear1.SelectedValue + "')) and No_ collate Latin1_General_CI_AI not in (Select EmployeeCode from HRMSPortal.dbo.tbl_EmployeeSalaryHoldDetail where month collate Latin1_General_CI_AI=REPLACE(LTRIM(REPLACE('" + DropDownList1.SelectedValue + "', '0', ' ')), ' ', '0') and Year collate Latin1_General_CI_AI ='" + ddlYear1.SelectedValue + "') ) T where T.Department='" + drpDepartment.SelectedValue + "' and T.Designation='" + drpDesignation.SelectedValue + "'  and   HOD=upper('" + txtHOD.Text + "') and No_ like 'TMU%'";
            }
            else
            {
                query = "select * from (select No_ EmployeeCode,[First Name] as 'Employee Name',[Global Dimension 1 Code] College,(Select top 1 [Designation Description] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Designation Master] where [Designation Code]=EMP.[Designation Code]) as Designation,HOD as  'HOD ID' ,isnull((Select [First Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=EMP.HOD),'') 'HOD Name', [Department Name]  as 'Department',isnull((Select [E-Mail] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=EMP.HOD),'') 'HODEmail',isnull((Select [Mobile Phone No_] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=EMP.HOD),'') 'HODMobile' from [TMU$Employee] EMP where (Status=0 or (month([Leave Date]) =REPLACE(LTRIM(REPLACE('" + DropDownList1.SelectedValue + "', '0', ' ')), ' ', '0') and YEAR([Leave Date])  ='" + ddlYear1.SelectedValue + "')) and No_ collate Latin1_General_CI_AI not in (Select EmployeeCode from HRMSPortal.dbo.tbl_EmployeeSalaryHoldDetail where month collate Latin1_General_CI_AI=REPLACE(LTRIM(REPLACE('" + DropDownList1.SelectedValue + "', '0', ' ')), ' ', '0') and Year collate Latin1_General_CI_AI ='" + ddlYear1.SelectedValue + "') ) T where T.Department='" + drpDepartment.SelectedValue + "' and T.Designation='" + drpDesignation.SelectedValue + "' and No_ like 'TMU%'";
            }
        }
        if (drpDepartment.SelectedValue == "0" && drpDesignation.SelectedValue == "0")
        {
            if (txtHOD.Text != "")
            {
                query = "select No_ EmployeeCode,[First Name] as 'Employee Name',[Global Dimension 1 Code] College,(Select top 1 [Designation Description] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Designation Master] where [Designation Code]=EMP.[Designation Code]) as Designation,HOD as  'HOD ID' ,isnull((Select [First Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=EMP.HOD),'') 'HOD Name', [Department Name]  as 'Department',isnull((Select [E-Mail] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=EMP.HOD),'') 'HODEmail',isnull((Select [Mobile Phone No_] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=EMP.HOD),'') 'HODMobile' from [TMU$Employee] EMP where (Status=0 or (month([Leave Date]) =REPLACE(LTRIM(REPLACE('" + DropDownList1.SelectedValue + "', '0', ' ')), ' ', '0') and YEAR([Leave Date])  ='" + ddlYear1.SelectedValue + "')) and No_ collate Latin1_General_CI_AI not in (Select EmployeeCode from HRMSPortal.dbo.tbl_EmployeeSalaryHoldDetail where month collate Latin1_General_CI_AI=REPLACE(LTRIM(REPLACE('" + DropDownList1.SelectedValue + "', '0', ' ')), ' ', '0') and Year collate Latin1_General_CI_AI ='" + ddlYear1.SelectedValue + "')  and   HOD=upper('" + txtHOD.Text + "') and No_ like 'TMU%'";
            }
            else
            {
                query = "select No_ EmployeeCode,[First Name] as 'Employee Name',[Global Dimension 1 Code] College,(Select top 1 [Designation Description] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Designation Master] where [Designation Code]=EMP.[Designation Code]) as Designation,HOD as  'HOD ID' ,isnull((Select [First Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=EMP.HOD),'') 'HOD Name', [Department Name]  as 'Department',isnull((Select [E-Mail] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=EMP.HOD),'') 'HODEmail',isnull((Select [Mobile Phone No_] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=EMP.HOD),'') 'HODMobile' from [TMU$Employee] EMP where (Status=0 or (month([Leave Date]) =REPLACE(LTRIM(REPLACE('" + DropDownList1.SelectedValue + "', '0', ' ')), ' ', '0') and YEAR([Leave Date])  ='" + ddlYear1.SelectedValue + "')) and No_ collate Latin1_General_CI_AI not in (Select EmployeeCode from HRMSPortal.dbo.tbl_EmployeeSalaryHoldDetail where month collate Latin1_General_CI_AI=REPLACE(LTRIM(REPLACE('" + DropDownList1.SelectedValue + "', '0', ' ')), ' ', '0') and Year collate Latin1_General_CI_AI ='" + ddlYear1.SelectedValue + "') and No_ like 'TMU%' ";
            }
        }
        SqlDataAdapter da = new SqlDataAdapter(query, Portalcon.Con);
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
        string str = "No_Response_Employee_Report";
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