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

public partial class Faculty_EmployeeAttendance : System.Web.UI.Page
{
    Connection con;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            con = new Connection();
            if (Session["uname"].ToString() == null)
            {
                Response.Redirect("../Default.aspx");
            }
            else
            {
                if (!IsPostBack)
                {

                    ViewAttendance();

                }

                // show_Attendence();
            }
        }
        catch (Exception)
        {
            Response.Redirect("../Default.aspx");
        }
    }

    SqlDataReader dr;
     public void ViewAttendance()
    {
        con1.Open();
        SqlDataAdapter daP = new SqlDataAdapter("select t.[Employee No],t.[Employee Name],(Select [HOD Name] from [TMU$Employee] where No_=t.[Employee No]) as HOD, CONVERT(varchar(11) ,[Attendance Date],106) as [Attendance Date] ,case when (select COUNT(*) from [TMU$Employee Device Punches] where [Employee Machine Code]=t.[Employee Machine Code] and [Punch Date]=t.[Attendance Date])>0 then 'Present' when Status=4 then 'Leave' when Status=9 then 'Absent' else 'Holiday' end as 'Status',(select [Designation Description] from [TMU$Designation Master] where  [Designation Code]=(Select [Designation Code] from [TMU$Employee] where No_=t.[Employee No])) as 'Designation' from [TMU$Employee Actual Punch Data] t where [Attendance Date] between '" + txtfromDate.Text + "' and '" + txtTodate.Text + "' and [Global Dimension 1 Code]='TMDC' order by [Employee Name],[Attendance Date]", con1);
        DataTable dtMinuteP = new DataTable();

        daP.Fill(dtMinuteP);
        if (dtMinuteP.Rows.Count > 0)
        {
            grd_ViewAttendance.DataSource = dtMinuteP;
            grd_ViewAttendance.DataBind();
        }
        else
        {
            grd_ViewAttendance.DataSource = "";
            grd_ViewAttendance.DataBind();
        }
        con1.Close();

    }




    protected void btnGet_Click(object sender, EventArgs e)
    {
        ViewAttendance();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grd_ViewAttendance.RenderControl(htmlWrite);

        Response.Clear();
        string str = "ActualAttendance" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();
    }
}