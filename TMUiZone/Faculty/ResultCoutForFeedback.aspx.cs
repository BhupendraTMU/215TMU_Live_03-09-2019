using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Faculty_ResultCoutForFeedback : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           if (Session["uid"].ToString() == "TMU02982" || Session["uid"].ToString() == "TMU08617" || Session["uid"].ToString() == "TMU00588" || Session["UserGroup"].ToString() == "REGISTRAR")
            {
                lblTime.Text = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm"); //System.DateTime.Now.ToLongTimeString();

                if (!IsPostBack)
                {
                    bindresult();
                    bindAcademicYear();
                }
            }
            else
            {
                Response.Redirect("../Default.aspx");
            }
        }
        catch 
        
        { Response.Redirect("../Default.aspx"); 
        
        
        }
    }


    public void bindresult()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
           // SqlCommand cmd = new SqlCommand("Sp_FeedbackCountRealTime", con);
            SqlCommand cmd = new SqlCommand("Sp_FeedbackCountRealTime_OddEvenYear", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OddEvenYear", ddsem.SelectedValue);
            cmd.Parameters.AddWithValue("@AcademicYear", ddlAcademicYear.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grdResult.DataSource = dt;
            grdResult.DataBind();
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
       // bindresult();
    }
    protected void TimerRef_Tick(object sender, EventArgs e)
    {
        //bindresult();
        lblTime.Text = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm");

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
            ddlAcademicYear.DataSource = dt1;
            ddlAcademicYear.DataTextField = "Details";
            ddlAcademicYear.DataValueField = "No_";
            ddlAcademicYear.DataBind();
        }
    }


    protected void btnshow_Click(object sender, EventArgs e)
    {
        bindresult();
    }
}