using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Faculty_exitinterviewformDetails : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
    string constr1 = ConfigurationSettings.AppSettings["strPortal"].ToString();
    
    protected void Page_Load(object sender, EventArgs e)
    {
         try
        {
            
        if (!IsPostBack)
        {            
            bindcollege(); HOD();
        }
        }
         catch
         {
//             Response.Redirect("../Default.aspx");
         }

    }
    public void bindcollege()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("select distinct [Code]+'-'+[Name] as cname,[Code] as code from [TMU$Dimension Value] where [Dimension Code]='COLLEGE'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            DrpCollege.DataSource = dt1;
            DrpCollege.DataTextField = "cname";
            DrpCollege.DataValueField = "code";
            DrpCollege.DataBind();
            DrpCollege.Items.Insert(0, new ListItem("-- Select College --", ""));

        }
    }
    public void HOD()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("SP_ValidateHOD_FromEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.Add("@UserID", Session["uid"].ToString());
            int rcordcount=Convert.ToInt16(cmd.ExecuteScalar());
            con.Close();
            if (rcordcount > 0 && Session["UserGroup"].ToString() !="HR")
            {
                tdCollege.Visible = false; BindExitFormGrid("HOD");
            }
            else if (Session["UserGroup"].ToString() == "HR")
            {
                tdCollege.Visible = true;// DrpCollege.Visible = true;
                BindExitFormGrid("HR");
            }
            else
            {
                //Response.Redirect("http://14.139.238.130:82/TMUiZone/Faculty/exitinterviewform.aspx");
               Response.Redirect("~/Faculty/exitinterviewform.aspx");
            }
        }
    }
    public void BindExitFormGrid(String HOD_HR)
    {       
            using (SqlConnection con = new SqlConnection(constr1))
            {
              try
                 {       
                SqlCommand cmd = new SqlCommand("SP_GetExitFormList", con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Role", HOD_HR);
                if (HOD_HR == "HOD") { cmd.Parameters.Add("@CollegeCode", ""); }
                else { cmd.Parameters.Add("@CollegeCode", DrpCollege.SelectedValue); }
                cmd.Parameters.Add("@Name", txtemp.Text.Trim());
                cmd.Parameters.Add("@UserID", Session["uid"].ToString());                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                da.Fill(dt1);
                exitformDetails.DataSource = dt1;
                exitformDetails.DataBind();
            }
            catch { con.Close(); }
        }
        

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Session["UserGroup"].ToString() == "HR")
        {
            BindExitFormGrid("HR"); ;
        }
        else
        {
            BindExitFormGrid("HOD");
        }

    }   
    protected void exitformDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "printfrm")
        {
            Session["mysession"] = e.CommandArgument;
            //Label2.Text="../SredirectToReport.aspx?search='" + e.CommandArgument + "'";
              Response.Redirect("./ShowReport.aspx");
        }
    }
  
   
}