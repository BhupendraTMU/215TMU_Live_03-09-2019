using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Faculty_ApplicationDetails : System.Web.UI.Page
{
    ServicePoratal con;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            con = new ServicePoratal();
            if (!IsPostBack)
            {
                if (Session["uid"].ToString() == "TMU03651")
                {

                   // show_Report();
                    GridView1.DataSource = "";
                    GridView1.DataBind();

                }
                else
                {

                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../Default.aspx");
        }
    }
    public void show_Report()
    {
        string query = "";

        query = "Select [Program Code],[Program Description],[Admitted Year],[Application Received],case when  [Application Received]<[Admission Made] then [Application Received] else [Admission Made] end as 'Admission Made',(Select Capacity from [EDUCOLLEGELIVE-R2].dbo.[TMU$Course - COLLEGE] where Code=T.[Program Code] and [Academic Year]=T.[Admitted Year]) as 'Intake' from (SELECT  [Course Code] as 'Program Code',[Course Name] as 'Program Description',[Academic Year] as 'Admitted Year' ,Count(No_) as 'Application Received',(Select Count(No_) from [EDUCOLLEGELIVE-R2].dbo.[TMU$Student - COLLEGE] where [Admitted Year]=AC.[Academic Year] and [Course Code]=AC.[Course Code]) as 'Admission Made' FROM [EDUCOLLEGELIVE-R2].dbo.[TMU$Application - COLLEGE] AC where [Course Code]!='' and [Academic Year]!='' and [Academic Year]='21-22' group by [Course Code],[Course Name], [Academic Year] ) T where T.[Program Code] in ('BBA-001','BSC-006','MBA-001','NUR-005','PT-001') order by T.[Admitted Year] desc";


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
    protected void btnExport_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        GridView1.RenderControl(htmlWrite);

        Response.Clear();
        string str = "ApplicationDetail_" + Session["AcademicYear"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (drpProgram.SelectedValue == "")
        {
            GridView1.DataSource = "";
            GridView1.DataBind();
        }

        else if (drpProgram.SelectedValue == "All")
        {

            string query = "";

            query = "Select [Program Code],[Program Description],[Admitted Year],[Application Received],case when  [Application Received]<[Admission Made] then [Application Received] when (Select Capacity from [EDUCOLLEGELIVE-R2].dbo.[TMU$Course - COLLEGE] where Code=T.[Program Code] and [Academic Year]=T.[Admitted Year]) <[Admission Made] then ((Select Capacity from [EDUCOLLEGELIVE-R2].dbo.[TMU$Course - COLLEGE] where Code=T.[Program Code] and [Academic Year]=T.[Admitted Year])) else [Admission Made] end as 'Admission Made',(Select Capacity from [EDUCOLLEGELIVE-R2].dbo.[TMU$Course - COLLEGE] where Code=T.[Program Code] and [Academic Year]=T.[Admitted Year]) as 'Intake' from (SELECT  [Course Code] as 'Program Code',[Course Name] as 'Program Description',[Academic Year] as 'Admitted Year' ,Count(No_) as 'Application Received',(Select Count(No_) from [EDUCOLLEGELIVE-R2].dbo.[TMU$Student - COLLEGE] where [Admitted Year]=AC.[Academic Year] and [Course Code]=AC.[Course Code]) as 'Admission Made' FROM [EDUCOLLEGELIVE-R2].dbo.[TMU$Application - COLLEGE] AC where [Course Code]!='' and [Academic Year]!='' and [Academic Year]='21-22' group by [Course Code],[Course Name], [Academic Year] ) T where T.[Program Code] in ('BBA-001','BSC-006','MBA-001','NUR-005','PT-001') order by T.[Admitted Year] desc";


            SqlDataAdapter da = new SqlDataAdapter(query, con.Con);
            DataTable dt = new DataTable();

            da.Fill(dt);
            con.DisConnect();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            string query = "";

            query = "Select [Program Code],[Program Description],[Admitted Year],[Application Received],case when  [Application Received]<[Admission Made] then [Application Received] when (Select Capacity from [EDUCOLLEGELIVE-R2].dbo.[TMU$Course - COLLEGE] where Code=T.[Program Code] and [Academic Year]=T.[Admitted Year]) <[Admission Made] then ((Select Capacity from [EDUCOLLEGELIVE-R2].dbo.[TMU$Course - COLLEGE] where Code=T.[Program Code] and [Academic Year]=T.[Admitted Year])) else [Admission Made] end as 'Admission Made',(Select Capacity from [EDUCOLLEGELIVE-R2].dbo.[TMU$Course - COLLEGE] where Code=T.[Program Code] and [Academic Year]=T.[Admitted Year]) as 'Intake' from (SELECT  [Course Code] as 'Program Code',[Course Name] as 'Program Description',[Academic Year] as 'Admitted Year' ,Count(No_) as 'Application Received',(Select Count(No_) from [EDUCOLLEGELIVE-R2].dbo.[TMU$Student - COLLEGE] where [Admitted Year]=AC.[Academic Year] and [Course Code]=AC.[Course Code]) as 'Admission Made' FROM [EDUCOLLEGELIVE-R2].dbo.[TMU$Application - COLLEGE] AC where [Course Code]!='' and [Academic Year]!='' and [Academic Year]='21-22' group by [Course Code],[Course Name], [Academic Year] ) T where T.[Program Code] ='" + drpProgram.SelectedValue + "' order by T.[Admitted Year] desc";


            SqlDataAdapter da = new SqlDataAdapter(query, con.Con);
            DataTable dt = new DataTable();

            da.Fill(dt);
            con.DisConnect();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}