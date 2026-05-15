using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public partial class Faculty_EducalenderEntry : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
    string constr1 = ConfigurationSettings.AppSettings["strPortal"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                using (SqlConnection con = new SqlConnection(constr))
                {

                    SqlCommand cmd = new SqlCommand("select [Job Title_Grade Desc],No_,[Global Dimension 1 Code] as clgname from TMU$Employee where [Job Title_Grade Desc]='REGISTRAR' and [No_]='" + Session["uid"].ToString() + "'", con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        con.Close();
                        bindforREGISTRAR();
                        bindAcademicYear();

                    }
                    else
                    {
                        con.Close();
                         bindcode();
                         bindAcademicYear();
                    }
                    con.Close();
                }







               ;
                // bidgrid();
            }
        }
        catch (Exception)
        {
            Response.Redirect("../Default.aspx");
        }

    }
    public void bindforREGISTRAR()
    {
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {
            //using (SqlDataAdapter a = new SqlDataAdapter("select distinct(Code) as Code from [TMU$Edu Calendar Entry]", Conn))
            using (SqlDataAdapter a = new SqlDataAdapter(" select Distinct [Global Dimension 1 Code] as Code from [TMU$Employee]", conn))
            {
                DataTable dt = new DataTable();
                a.Fill(dt); 
                if (dt.Rows.Count > 0)
                {
                    CodeDropDownList.DataSource = dt;
                    CodeDropDownList.DataTextField = "Code";
                    CodeDropDownList.DataValueField = "Code";
                    CodeDropDownList.DataBind();
                    CodeDropDownList.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));
                }
            }
           
        }
    }
    public void bidgrid()
    {
        using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {
            //[Global Dimension 1 Code]
           // SqlCommand cmd = new SqlCommand("select convert(date,[Date],103) as [Date],FORMAT([Date],'dddd')AS  [Day],[Description] from [TMU$Edu Calendar Entry] where Code='" + CodeDropDownList.SelectedValue.Trim() + "' and [Academic Year]='" + drpAcademicYear.SelectedValue + "'", Conn);
            //SqlCommand cmd = new SqlCommand("select convert(date,[Date],103) as [Date],FORMAT([Date],'dddd')AS  [Day],[Description] from [TMU$Edu Calendar Entry] where len([Description])!=0 and  Code='" + CodeDropDownList.SelectedValue.Trim() + "' and [Academic Year]='" + drpAcademicYear.SelectedValue + "'  and [Date] >='" + txtFromDate.Text + "' and  [Date] <'" + txtToDate.Text + "'", Conn);
            //SqlCommand cmd = new SqlCommand("select convert(date,[Date],103) as [Date],FORMAT([Date],'dddd')AS  [Day],[Description] from [TMU$Edu Calendar Entry] where Code='" + CodeDropDownList.SelectedValue.Trim() + "' and [Description]!='' and [Academic Year]='" + drpAcademicYear.SelectedValue + "'  and [Date] >=CONVERT(datetime,'" + txtFromDate.Text + "',20) and  [Date] <CONVERT(datetime,'" + txtToDate.Text + "',20)", Conn);
            SqlCommand cmd = new SqlCommand("select convert(date,[Date],103) as [Date],FORMAT([Date],'dddd')AS  [Day],[Description] from [TMU$Edu Calendar Entry] where len([Description])!=0 and  [Global Dimension 1 Code]='" + CodeDropDownList.SelectedValue.Trim() + "' and [Academic Year]='" + drpAcademicYear.SelectedValue + "'  and [Date] >='" + txtFromDate.Text + "' and  [Date] <='" + txtToDate.Text + "'", Conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            EduGridView.DataSource = dt1;
            EduGridView.DataBind();
        }
    }

    public void bindAcademicYear()
    {
        using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            drpAcademicYear.DataSource = dt1;
            drpAcademicYear.DataTextField = "Details";
            drpAcademicYear.DataValueField = "No_";
            drpAcademicYear.DataBind();
        }
    }
    //public void bindcode()
    //{
    //    DataTable dt = new DataTable();

    //    using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
    //    {
    //        Conn.Open();
          
    //        //using (SqlDataAdapter a = new SqlDataAdapter("select distinct(Code) as Code from [TMU$Edu Calendar Entry]", Conn))
    //        using (SqlDataAdapter a = new SqlDataAdapter(" select Distinct [Global Dimension 1 Code] as Code from [TMU$Employee] where  No_='"+Session["uid"].ToString()+"' ", Conn))
    //        {
    //            a.Fill(dt); ;
    //            if (dt.Rows.Count > 0)
    //            {
    //                CodeDropDownList.DataSource = dt;
    //                CodeDropDownList.DataTextField = "Code";
    //                CodeDropDownList.DataValueField = "Code";
    //                CodeDropDownList.DataBind();
    //                CodeDropDownList.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));
    //            }
    //        }
    //    }
    //}
    public void bindcode()
    {
        //var CollegeCode = Session["GlobalDimension1Code"].ToString();
        //CodeDropDownList.DataSource = CollegeCode;
        //CodeDropDownList.DataTextField = "Code";
        //CodeDropDownList.DataValueField = "Code";
        //CodeDropDownList.DataBind();
        CodeDropDownList.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));
        CodeDropDownList.Items.Insert(1, new ListItem(Session["GlobalDimension1Code"].ToString(),Session["GlobalDimension1Code"].ToString()));

    }
    protected void CodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        //bindcode();
        //bindAcademicYear();
        //bidgrid();
    }
    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
       // bindcode();
        //bindAcademicYear();
        //bidgrid();
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
       // bidgrid();
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        bidgrid();
    }
}