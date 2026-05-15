using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


public partial class Faculty_StudentTagingReport : System.Web.UI.Page
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

                    BindYear();
                    College();
                    BindCourse();
                    //ddlMonth.SelectedValue = System.DateTime.Now.ToString("MM");
                    //ddlYear.SelectedValue = System.DateTime.Now.ToString("yyyy");
                    //ViewAttendance();
                    binddata();
                }

                // show_Attendence();
            }
        }
        catch (Exception)
        {
            Response.Redirect("../Default.aspx");
        }




    }


    public void BindCourse()
    {

        con1.Open();
        SqlDataAdapter daP = new SqlDataAdapter("select distinct T.[Course Code] from ( select No_,[Course Code],SSC.[Subject Code] Sub1,SOSC.[Subject Code] Sub2,SSC.[Tag Date] tag1,SOSC.[Tag Date] tag2  ,SSC.[Academic Year] AC1,SOSC.[Academic Year] AC2,SC.[Global Dimension 1 Code] from [TMU$Student - COLLEGE] SC left join [TMU$Student Subject - COLLEGE] SSC on SC.No_=SSC.[Student No_] left join [TMU$Student Optional Subject - COL] SOSC on SOSC.[Student No_]=SC.No_  where [Student Status]=1) T where T.tag1!='1753-01-01 00:00:00.000'  and T.tag2!='1753-01-01 00:00:00.000' and [Global Dimension 1 Code]='"+ddlCollege.SelectedValue+"' ", con1);
        DataTable dtMinuteP = new DataTable();

        daP.Fill(dtMinuteP);
        drpCourse.DataSource = dtMinuteP;
        drpCourse.DataTextField = "Course Code";
        drpCourse.DataValueField = "Course Code";
        drpCourse.DataBind();
        con1.Close();
    }

    public void BindYear()
    {

        con1.Open();
        SqlDataAdapter daP = new SqlDataAdapter("select distinct case when T.AC1 =null then T.AC2 else T.AC1 end as 'Academic Year' from ( select No_,[Course Code],SSC.[Subject Code] Sub1,SOSC.[Subject Code] Sub2,SSC.[Tag Date] tag1,SOSC.[Tag Date] tag2  ,SSC.[Academic Year] AC1,SOSC.[Academic Year] AC2,SC.[Global Dimension 1 Code] from [TMU$Student - COLLEGE] SC left join [TMU$Student Subject - COLLEGE] SSC on SC.No_=SSC.[Student No_] left join [TMU$Student Optional Subject - COL] SOSC on SOSC.[Student No_]=SC.No_  where [Student Status]=1) T where T.tag1!='1753-01-01 00:00:00.000'  and T.tag2!='1753-01-01 00:00:00.000' order by T.AC1 desc", con1);
        DataTable dtMinuteP = new DataTable();

        daP.Fill(dtMinuteP);
        ddlAcademicYear.DataSource = dtMinuteP;
        ddlAcademicYear.DataTextField = "Academic Year";
        ddlAcademicYear.DataValueField = "Academic Year";
        ddlAcademicYear.DataBind();
        con1.Close();
    }
    public void College()
    {

        con1.Open();
        SqlDataAdapter daP = new SqlDataAdapter("select distinct T.[Global Dimension 1 Code] from ( select No_,[Course Code],SSC.[Subject Code] Sub1,SOSC.[Subject Code] Sub2,SSC.[Tag Date] tag1,SOSC.[Tag Date] tag2  ,SSC.[Academic Year] AC1,SOSC.[Academic Year] AC2,SC.[Global Dimension 1 Code] from [TMU$Student - COLLEGE] SC left join [TMU$Student Subject - COLLEGE] SSC on SC.No_=SSC.[Student No_] left join [TMU$Student Optional Subject - COL] SOSC on SOSC.[Student No_]=SC.No_  where [Student Status]=1) T where T.tag1!='1753-01-01 00:00:00.000'  and T.tag2!='1753-01-01 00:00:00.000' ", con1);
        DataTable dtMinuteP = new DataTable();

        daP.Fill(dtMinuteP);
        ddlCollege.DataSource = dtMinuteP;
        ddlCollege.DataTextField = "Global Dimension 1 Code";
        ddlCollege.DataValueField = "Global Dimension 1 Code";
        ddlCollege.DataBind();
        con1.Close();
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {

        binddata();



    }


    public void binddata()
    {
        con1.Open();
        SqlDataAdapter daP = new SqlDataAdapter("Select * from (select T.No_,T.[Course Code],case when T.Sub1=null then T.Sub2 else T.Sub1 end 'Subject Code',case when T.tag1 =null then T.tag2 else T.tag1 end as 'Tag Date',case when T.AC1 =null then T.AC2 else T.AC1 end as 'Academic Year',(Select Description from [TMU$Course - COLLEGE] where Code=T.[Course Code])as 'Course Name',T.[Global Dimension 1 Code] from ( select No_,[Course Code],SSC.[Subject Code] Sub1,SOSC.[Subject Code] Sub2,SSC.[Tag Date] tag1,SOSC.[Tag Date] tag2  ,SSC.[Academic Year] AC1,SOSC.[Academic Year] AC2,SC.[Global Dimension 1 Code] from [TMU$Student - COLLEGE] SC left join [TMU$Student Subject - COLLEGE] SSC on SC.No_=SSC.[Student No_] left join [TMU$Student Optional Subject - COL] SOSC on SOSC.[Student No_]=SC.No_  where [Student Status]=1) T where T.tag1!='1753-01-01 00:00:00.000'  and T.tag2!='1753-01-01 00:00:00.000'  and T.[Course Code]='"+drpCourse.SelectedValue+"'  and T.[Global Dimension 1 Code]='"+ddlCollege.SelectedValue+"' ) Y where Y.[Academic Year]='"+ddlAcademicYear.SelectedValue+"' order by Y.[Tag Date] asc", con1);
        DataTable dtMinuteP = new DataTable();

        daP.Fill(dtMinuteP);

        grd_ViewAttendance.DataSource = dtMinuteP;
        grd_ViewAttendance.DataBind();



        con1.Close();
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
        string str = "StudentTaggingData" + ddlCollege.SelectedValue; 
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindCourse();


    }
}