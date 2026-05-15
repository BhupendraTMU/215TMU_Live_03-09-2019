using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Faculty_smsStudent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                bindcollegedrp();
                bindcourse();
                bindSemester();
                bindyear();
                bindReligion();
                bindcategory();

            }
        }
        catch
        {
            Response.Redirect("../Default.aspx"); 
        }

    }
    public void bindcollegedrp()
    {
        using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {
            string str = "";

            if (Session["UserGroup"].ToString() == "REGISTRAR")
            {
                str = "select distinct(Code +' - '+ Name) as BranchName from [TMU$Dimension Value] where [Dimension Code]='COLLEGE' and [Active College]=1";
            }
            if (Session["UserGroup"].ToString() == "PRINCIPAL")
            {
                str = "select distinct(Code +' - '+ Name) as BranchName from [TMU$Dimension Value] where [Dimension Code]='COLLEGE' and [Active College]=1 and Code='" + Session["GlobalDimension1Code"].ToString() + "' ";
            }



            SqlCommand cmd = new SqlCommand(str, Conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1); if (Session["UserGroup"].ToString() == "PRINCIPAL")
            {
                DrpCollege.Visible = false;
                lblCollege.Visible = true;
                
                string strCollegecode = dt1.Rows[0]["BranchName"].ToString();
                string[] code = strCollegecode.Split('-');
                lblCollege.Text = code[0].ToString();


            }
            else
            {
                
                lblCollege.Visible = false;
                DrpCollege.Visible = true;
                DrpCollege.DataSource = dt1;
                DrpCollege.DataTextField = "BranchName";
                DrpCollege.DataValueField = "BranchName";
                DrpCollege.DataBind();
                DrpCollege.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));
            }
        }
    }

    public void bindcourse()
    {
        using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {

            string str = "";

            if (Session["UserGroup"].ToString() == "REGISTRAR")
            {
              //  str = "select distinct [Course Code] as coursecode from [TMU$Student - COLLEGE] where len([Course Code])!=0";
                // above query change after discussion between  akanksha mam and ashutosh sir
                str = "select distinct Code as coursecode from [TMU$Course - COLLEGE] where len(Code)!=0";
            }
            if (Session["UserGroup"].ToString() == "PRINCIPAL")
            {
              //  str = "select distinct [Course Code] as coursecode from [TMU$Student - COLLEGE] where len([Course Code])!=0 and [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "' ";
                str = "select distinct Code as coursecode from [TMU$Course - COLLEGE] where [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "'  and len(Code)!=0";
            }









            SqlCommand cmd = new SqlCommand(str, Conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            DrpCourse.DataSource = dt1;
            DrpCourse.DataTextField = "coursecode";
            DrpCourse.DataValueField = "coursecode";
            DrpCourse.DataBind();
            DrpCourse.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));
        }
    }
    public void bindSemester()
    {
        using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {




            string str = "";

            if (Session["UserGroup"].ToString() == "REGISTRAR")
            {
                str = "select distinct [Semester] as sem from [TMU$Student - COLLEGE] where len([Semester])!=0";
            }
            if (Session["UserGroup"].ToString() == "PRINCIPAL")
            {
                str = "select distinct [Semester] as sem from [TMU$Student - COLLEGE] where len([Semester])!=0 and [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "' ";
            }








            SqlCommand cmd = new SqlCommand(str, Conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            DropSemester.DataSource = dt1;
            DropSemester.DataTextField = "sem";
            DropSemester.DataValueField = "sem";
            DropSemester.DataBind();
            DropSemester.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));
        }
    }

    public void bindReligion()
    {
        using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {




            string str = "";

            if (Session["UserGroup"].ToString() == "REGISTRAR")
            {
                str = "select Distinct Religion from [TMU$Student - COLLEGE]  where len(Religion)!=0  order by Religion";
            }
            if (Session["UserGroup"].ToString() == "PRINCIPAL")
            {
                str = "select Distinct Religion from [TMU$Student - COLLEGE]  where len(Religion)!=0   and [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "'order by Religion ";
            }








            SqlCommand cmd = new SqlCommand(str, Conn);//where [Branch Name]='" + DrpCollege.SelectedValue + "' and [Department Name]='" + DrpDepartment.SelectedValue + "' and [Job Title_Grade Desc]='" + DrpGrade.SelectedValue + "'", Conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            DrpReligion.DataSource = dt1;
            DrpReligion.DataTextField = "Religion";
            DrpReligion.DataValueField = "Religion";
            DrpReligion.DataBind();
            DrpReligion.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));
        }
    }
    public void bindyear()
    {
        using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {



            string str = "";

            if (Session["UserGroup"].ToString() == "REGISTRAR")
            {
                str = "select  distinct [Year] as yr from [TMU$Student - COLLEGE] where len([Year])!=0 order by [Year]";
            }
            if (Session["UserGroup"].ToString() == "PRINCIPAL")
            {
                str = "select  distinct [Year] as yr from [TMU$Student - COLLEGE] where len([Year])!=0   and [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "'order by [Year] ";
            }








            SqlCommand cmd = new SqlCommand(str, Conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            DropYear.DataSource = dt1;
            DropYear.DataTextField = "yr";
            DropYear.DataValueField = "yr";
            DropYear.DataBind();
            DropYear.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));
        }
    }
    public void bindcategory()
    {
        using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {



            string str = "";

            if (Session["UserGroup"].ToString() == "REGISTRAR")
            {
                str = "select distinct [Category] as Category from [TMU$Student - COLLEGE] where len([Category])!=0";
            }
            if (Session["UserGroup"].ToString() == "PRINCIPAL")
            {
                str = "select distinct [Category] as Category from [TMU$Student - COLLEGE] where len([Category])!=0  and [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "'";
            }











            SqlCommand cmd = new SqlCommand(str, Conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            DropCategory.DataSource = dt1;
            DropCategory.DataTextField = "Category";
            DropCategory.DataValueField = "Category";
            DropCategory.DataBind();
            DropCategory.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));
        }
    }

    public void sendandbindmobileno( string str)
    {
        using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("select [Mobile Number] from [TMU$Student - COLLEGE] where " + str + " and len([Mobile Number])!=0", Conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                int i = dt1.Rows.Count;
                int j = 0;
                while (i > 0)
                {

                   //Txtsms.Text += "  " + dt1.Rows[j]["Mobile Number"].ToString();
                   SMS(dt1.Rows[i-1]["Mobile Number"].ToString(), Txtsms.Text);
                    j++;
                    i--;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Message Sent !');", true);
                Txtsms.Text = "";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('There are no record Found !');", true);
            }

            // DrpGender.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));
        }
    }

    public void SMS(String MobileNo, string Msg)
    {
        //  Website: http://www.universalsmsadvertising.com/     //  Username: 9837016352   //  Password: 9837016352 
        // MobileNo = "91" + MobileNo;
        // MobileNo = "91" + 7503335183;
        string url = "http://www.universalsmsadvertising.com/universalsmsapi.php?user_name=9837016352&user_password=9837016352&mobile=" + MobileNo + "&sender_id=TMUniv&type=F&text=" + Msg + "";
        System.Net.HttpWebRequest fr;
        Uri targetURI = new Uri(url);
        fr = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(targetURI);
        if (fr.GetResponse().ContentLength > 0)
        {
            System.IO.StreamReader str = new System.IO.StreamReader(fr.GetResponse().GetResponseStream());
            Response.Write(str.ReadToEnd());
            str.Close();
        }
    }


    protected void DrpCollege_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DrpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DrpGrade_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DrpReligion_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnsendsms_Click(object sender, EventArgs e)
    {
        string quer = "";
        //1
        if (Session["UserGroup"].ToString() == "REGISTRAR")
        {

            if (DrpCollege.SelectedValue != "-- Select --")
            {
                string str = DrpCollege.SelectedValue.ToString();
                string[] str1 = str.Split('-');
                quer = "and [Student Status]=1 and [Global Dimension 1 Code]='" + str1[0].Trim() + "'";

            }
        }
        if (Session["UserGroup"].ToString() == "PRINCIPAL")
        {
            quer = "and [Student Status]=1 and [Global Dimension 1 Code]='" +lblCollege.Text + "'";
        }
        //2
        if (DrpCourse.SelectedValue != "-- Select --")
        {
            quer += "and [Student Status]=1 and [Course Code]='" + DrpCourse.SelectedValue + "'";
        }
        //3
        if (DropSemester.SelectedValue != "-- Select --")
        {
            quer += "and [Student Status]=1 and [Semester]='" + DropSemester.SelectedValue + "'";
        }
        //4
        if (DropYear.SelectedValue != "-- Select --")
        {
            quer += "and [Student Status]=1 and [Year]='" + DropYear.SelectedValue + "'";
        }
        //5
        if (DrpReligion.SelectedValue != "-- Select --")
        {
            quer += "and [Student Status]=1 and Religion='" + DrpReligion.SelectedValue + "'";
        }
        //6
        if (DropCategory.SelectedValue != "-- Select --")
        {
            quer += "and [Student Status]=1 and [Category]='" + DropCategory.SelectedValue + "'";
        }
        //7
        if (DrpStudent.SelectedValue != "-- Select --")
        {
            //1-student 3- alumni
            quer += "and [Student Status]='" + DrpStudent.SelectedValue + "'";
        }
        //8
        if (DropFacility.SelectedValue != "-- Select --")
        {
            if (DropFacility.SelectedItem.Text =="Transport")
            {
                quer += "and [Student Status]=1 and [Hostel Acommodation]=" + DropFacility.SelectedValue + "";
            }
            if (DropFacility.SelectedItem.Text == "")
            {
                quer += "and [Student Status]=1 and [Transport Allot]=" + DropFacility.SelectedValue + "";
            }
        }
        //9
        if (DrpGender.SelectedValue != "0")
        {
            quer += "and [Student Status]=1 and [Gender]=" + DrpGender.SelectedValue.ToString() + "";
        }
        if (quer.Length != 0)
        {
            quer = quer.Remove(0, 4);
            if (Txtsms.Text != "")
            {
                sendandbindmobileno(quer);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert(' Please Write Some Text !');", true);
                return;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert(' Please Select Any Option !');", true);
        }
       // SMS("7065991428", "hi test sms");

    }
}