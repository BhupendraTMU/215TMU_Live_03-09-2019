using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
public partial class Faculty_EmployeeFine : System.Web.UI.Page
{
    ServicePoratal con1;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            con1 = new ServicePoratal();
            if (Session["uname"].ToString() == null)
            {
                Response.Redirect("../Default.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    //String sDate = DateTime.Now.ToString();
                   // DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));

                   // int day = datevalue.Day;
                   // int mn = datevalue.Month;
                   // int yy = datevalue.Year;
                   // DateTime now = DateTime.Now;
                    //DateTime lastDayOfLastMonth = now.Date.AddDays(-now.Day);
                    //int lastMonthLastDay = lastDayOfLastMonth.Day;


                    
                   // CalendarExtender3.StartDate = new DateTime(yy, mn , 01);
              
                    
                    ShowEMployeeDetails();

                }

                // show_Attendence();
            }
        }
        catch (Exception)
        {
            Response.Redirect("../Default.aspx");
        }
    }
   
    protected void btnGet_Click(object sender, EventArgs e)
    {

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


        string sqlStatement = "SELECT No_,[First Name],[Middle Name],[Last Name] FROM [TMU$Employee]  where [Status]='0' and ([Global Dimension 1 Code]='TMHS' or [Global Dimension 2 Code]='D036')";
        SqlCommand sqlCmd = new SqlCommand(sqlStatement, con);
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);

        sqlDa.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                id = dt.Rows[i]["No_"].ToString();

                fname = dt.Rows[i]["First Name"].ToString();
                sname = dt.Rows[i]["Middle Name"].ToString();
                lname = dt.Rows[i]["Last Name"].ToString();
                newName = id + "  " + fname + "    " + lname + " ";

                txtUserid.Items.Add(new ListItem(newName, id));

                txtUserid.SelectedValue = id;
            }
        }
        else
        {
            pnlError.Visible = true;
            pnlLeaveApplication.Visible = false;
            pnlReport.Visible = false;
        }

    }
  
   

    protected void lnkApplication_Click(object sender, EventArgs e)
    {
        pnlLeaveApplication.Visible = true;
        pnlReport.Visible = false;
        pnlError.Visible = false;
    }
    protected void lnkReport_Click(object sender, EventArgs e)
    {
        pnlLeaveApplication.Visible = false;
        pnlReport.Visible = true;
        pnlError.Visible = false;
        show_Report();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            SqlDataAdapter da = new SqlDataAdapter("select UserId from [HRMSPortal].dbo.tbl_EmployeeFine where convert(date,Fine_Date,111) =  '" + txtfrom.Text + "' and Status!=2 and  UserId='" + txtUserid.SelectedValue + "'   select  [open] from [HRMSPortal].dbo.tbl_FineMonthWiseCloserDetails where [Month]=(select month([Payroll Processing Month Date]) from [EDUCOLLEGELIVE-R2].dbo.[TMU$Pay Company Policy]) and [YEAR]=(select Year([Payroll Processing Month Date]) from [EDUCOLLEGELIVE-R2].dbo.[TMU$Pay Company Policy]) ", con);
            DataSet ds = new DataSet();

            da.Fill(ds);

            if (ds.Tables[1].Rows.Count > 0)
            {
                if (ds.Tables[1].Rows[0]["open"].ToString() == "1")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'You can not apply Fine for this date.');", true);

                    return;
                }
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'You have already applied Fine for this dates.');", true);

                return;
            }
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {

                SqlCommand cmd = new SqlCommand("Insert_EmployeeFine", con2);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@FineDate", txtfrom.Text);

                cmd.Parameters.Add("@EmployeeCode", txtUserid.SelectedValue);
                cmd.Parameters.Add("@Reason", txtremarks.Text);
                cmd.Parameters.Add("@Status", "0");
                cmd.Parameters.Add("@Amount", txtAmount.Text);
                if (con2.State == ConnectionState.Closed)
                    con2.Open();
                cmd.ExecuteNonQuery();
                con2.Close();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Entry Create Successfully.');", true);

            }
        }


        catch (Exception ex)
        {
            Response.Redirect("../Default.aspx");
        }


    }
    protected void btnLeaveViewSearch_Click(object sender, EventArgs e)
    {
        show_Report();
    }
    protected void grdViewLeaveStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    public void show_Report()
    {
        if (ddstatus.Text == "All")
        {
            SqlDataAdapter da = new SqlDataAdapter("select convert(varchar(11),(convert(date,Fine_Date )),113) as FineDate,case when Status=0 then 'Pending' when Status=1 then 'Approved' when Status=2 then 'Reject'  end Status ,[UserId] as 'Employee_ID',(select [First Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_CI_AI=tbl_EmployeeFine.UserId) as 'Employee_name',Amount,CreateDate,Reason from tbl_EmployeeFine order by convert(date,Fine_Date,111) desc", con1.Con);
            DataTable dt = new DataTable();

            da.Fill(dt);
            con1.DisConnect();
            grdViewLeaveStatus.DataSource = dt;
            grdViewLeaveStatus.DataBind();
           
           
        }
        else
        {

            SqlDataAdapter da = new SqlDataAdapter("select convert(varchar(11),(convert(date,Fine_Date )),113) as FineDate,case when Status=0 then 'Pending' when Status=1 then 'Approved' when Status=2 then 'Reject'  end Status ,[UserId] as 'Employee_ID',(select [First Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_CI_AI=tbl_EmployeeFine.UserId) as 'Employee_name',Amount,CreateDate,Reason from tbl_EmployeeFine where Status='"+ddstatus.SelectedValue+"' order by convert(date,Fine_Date,111) desc", con1.Con);
            DataTable dt = new DataTable();

            da.Fill(dt);
            con1.DisConnect();
            
            grdViewLeaveStatus.DataSource = dt;
            grdViewLeaveStatus.DataBind();

        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        grdViewLeaveStatus.AllowPaging = false;

        show_Report();


        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grdViewLeaveStatus.RenderControl(htmlWrite);

        Response.Clear();
        string str = "FineReport";
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();
    }

}