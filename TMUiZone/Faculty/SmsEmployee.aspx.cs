using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;


public partial class Faculty_smsEmployee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        if (!IsPostBack)
        {
            bindcollegedrp();
            bindDepatment();
            bindgrade();
            bindReligion();
            //bindgender();
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
            string str="";
           
            if (Session["UserGroup"].ToString() == "REGISTRAR")
            {
                str = "select distinct(Code +' - '+ Name) as BranchName from [TMU$Dimension Value] where [Dimension Code]='COLLEGE' and [Active College]=1";
            }
            if(Session["UserGroup"].ToString() == "PRINCIPAL")
            {
                str = "select distinct(Code +' - '+ Name) as BranchName from [TMU$Dimension Value] where [Dimension Code]='COLLEGE' and [Active College]=1 and Code='" + Session["GlobalDimension1Code"].ToString() + "' ";
            }
            SqlCommand cmd=new SqlCommand(str,Conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            if (Session["UserGroup"].ToString() == "PRINCIPAL")
            {
                DrpCollege.Visible = false;
                lblCollege.Visible = true;
                chkAll.Visible = false;
                string  strCollegecode=dt1.Rows[0]["BranchName"].ToString();
                string []code=strCollegecode.Split('-');
                lblCollege.Text = code[0].ToString();
                    
                    
            }
            else
            {
                chkAll.Visible = true; 
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
    //select Distinct [Department Name] from TMU$Employee where [Branch Name]='TEERTHANKER AADINATH COLLEGE OF EDUCATION'
    public void bindDepatment()
    {
        using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {

            string str = "";

            if (Session["UserGroup"].ToString() == "REGISTRAR")
            {
                str = "select Distinct [Department Name] as Dept from TMU$Employee where len([Department Name])!=0  order by [Department Name]";
            }
            if (Session["UserGroup"].ToString() == "PRINCIPAL")
            {
                str = "select Distinct [Department Name] as Dept from TMU$Employee where [Global Dimension 1 Code]='"+Session["GlobalDimension1Code"].ToString() + "' and len([Department Name])!=0  order by [Department Name] ";
            }





            SqlCommand cmd = new SqlCommand(str, Conn);// where [Branch Name]='" + DrpCollege.SelectedValue + "'", Conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            DrpDepartment.DataSource = dt1;
            DrpDepartment.DataTextField = "Dept";
            DrpDepartment.DataValueField = "Dept";
            DrpDepartment.DataBind();
            DrpDepartment.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));
        }
    }
    //select Distinct [Job Title_Grade Desc],[Job Title_Grade] from TMU$Employee where [Branch Name]='TEERTHANKER AADINATH COLLEGE OF EDUCATION' and [Department Name]='ADINATH COLLEGE OF EDUCATION'
    public void bindgrade()
    {
        using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {
            string str = "";

            if (Session["UserGroup"].ToString() == "REGISTRAR")
            {
                str = "select Distinct [Job Title_Grade Desc] as Grade from TMU$Employee where len([Job Title_Grade Desc])!=0  order by [Job Title_Grade Desc]";
            }
            if (Session["UserGroup"].ToString() == "PRINCIPAL")
            {
                str = " select Distinct [Job Title_Grade Desc] as Grade from TMU$Employee where [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "' and len([Job Title_Grade Desc])!=0  order by [Job Title_Grade Desc]";
            }




            SqlCommand cmd = new SqlCommand(str, Conn);// where [Branch Name]='" + DrpCollege.SelectedValue + "' and [Department Name]='" + DrpDepartment.SelectedValue+ "'", Conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            DrpGrade.DataSource = dt1;
            DrpGrade.DataTextField = "Grade";
            DrpGrade.DataValueField = "Grade";
            DrpGrade.DataBind();
            DrpGrade.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));
        }
    }

    //select Religion from TMU$Employee where [Branch Name]='TEERTHANKER AADINATH COLLEGE OF EDUCATION' and [Department Name]='ADINATH COLLEGE OF EDUCATION' and [Job Title_Grade Desc]=''


    public void bindReligion()
    {
        using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {

            string str = "";

            if (Session["UserGroup"].ToString() == "REGISTRAR")
            {
                str = "select Distinct Religion from TMU$Employee where len(Religion)!=0  order by Religion";
            }
            if (Session["UserGroup"].ToString() == "PRINCIPAL")
            {
                str = " select Distinct Religion from TMU$Employee where  [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "' and len(Religion)!=0  order by Religion";
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

    //select Distinct gender=(case [Gender] when 0 then '--select--' when 1 then 'Female' when 2 then 'Male' end )from TMU$Employee  where [Branch Name]='TEERTHANKER AADINATH COLLEGE OF EDUCATION' and [Department Name]='ADINATH COLLEGE OF EDUCATION' and [Job Title_Grade Desc]='' and Religion=''
    public void bindgender()
    {
        using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {


            string str = "";

            if (Session["UserGroup"].ToString() == "REGISTRAR")
            {
                str = "select Distinct gender=(case [Gender] when 0 then '--Select--' when 1 then 'Female' when 2 then 'Male' end )  from TMU$Employee";
            }
            if (Session["UserGroup"].ToString() == "PRINCIPAL")
            {
                str = " select Distinct gender=(case [Gender] when 0 then '--Select--' when 1 then 'Female' when 2 then 'Male' end )  from TMU$Employee where  [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "'";
            }







            SqlCommand cmd = new SqlCommand(str, Conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            DrpGender.DataSource = dt1;
            DrpGender.DataTextField = "gender";
            DrpGender.DataValueField = "gender";
            DrpGender.DataBind();
           // DrpGender.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));
        }
    }
    protected void DrpCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        //bindDepatment();
    }
    protected void DrpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
       // bindgrade();
    }
    protected void DrpReligion_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void DrpGrade_SelectedIndexChanged(object sender, EventArgs e)
    {
       // bindReligion();
    }

    public void Sendandbindmobileno( string str)
    {
       

        using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("select [Mobile Phone No_] from TMU$Employee where " + str + " and len([Mobile Phone No_])!=0 and [Status]=0", Conn);
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
                    //Txtsms.Text += "  "+dt1.Rows[j]["Mobile Phone No_"].ToString();
                   SMS(dt1.Rows[i-1]["Mobile Phone No_"].ToString(), Txtsms.Text);
                    j++;
                    i--;
                }
               Txtsms.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Message Sent !');", true);
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
    protected void btnsendsms_Click(object sender, EventArgs e)
    
    {
        //Sendandbindmobileno();

        string quer = "";
        if (chkAll.Checked == true)
        {

           

            if (Session["UserGroup"].ToString() == "REGISTRAR")
            {

                quer = "[Status]!=1";
            }
            if (Session["UserGroup"].ToString() == "PRINCIPAL")
            {
                quer = " [Status]!=1 And [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "'";
            }

            
            
            
            
            
            
           // quer = "[Status]!=1";


            if (quer.Length != 0)
            {

                if (Txtsms.Text != "")
                {
                    Sendandbindmobileno(quer);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert(' Please write some text !');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert(' Please Select Any Option !');", true);
            }

        }
        else
        {
            if (Session["UserGroup"].ToString() == "REGISTRAR")
            {

                if (DrpCollege.SelectedValue != "-- Select --")
                {
                    string str = DrpCollege.SelectedValue.ToString();
                    string[] str1 = str.Split('-');
                    quer = "and [Global Dimension 1 Code]='" + str1[0].Trim() + "'";
                }
            }
            if (Session["UserGroup"].ToString() == "PRINCIPAL")
            {
                quer = "and [Global Dimension 1 Code]='" + lblCollege.Text + "'";
            }
            
            if (DrpDepartment.SelectedValue != "-- Select --")
            {
                quer += " and [Department Name]='" + DrpDepartment.SelectedValue.ToString() + "'";
            }
            if (DrpReligion.SelectedValue != "-- Select --")
            {
                quer += " and  Religion='" + DrpReligion.SelectedValue.ToString() + "'";
            }

            if (DrpGrade.SelectedValue != "-- Select --")
            {
                quer += " and [Job Title_Grade Desc]='" + DrpGrade.SelectedValue.ToString() + "'";
            }

            if (DrpGender.SelectedValue != "0")
            {
                quer += " and [Gender]=" + DrpGender.SelectedValue.ToString() + "";
            }
            if (quer.Length != 0)
            {
                quer = quer.Remove(0, 4);
                if (Txtsms.Text != "")
                {
                    Sendandbindmobileno(quer);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert(' Please Write Some Text !');", true);
                }


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert(' Please Select Any Option !');", true);
            }

        }
    }
}