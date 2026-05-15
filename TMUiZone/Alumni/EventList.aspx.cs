using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using System.IO;

public partial class Alumni_EventList : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserGroup"].ToString() == "STUDENT")
            {
                if (!IsPostBack)
                { 
               BindGrid();
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Default.aspx");
        }
        
    }
 

    private void BindGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_EventListAlumani", con))
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@College", Session["College"].ToString());
                cmd.Parameters.AddWithValue("@CourseCode", Session["CourseCode"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    if (dt.Rows.Count > 0 && dt.Rows.Count <2 )
                    {
                        DateTime dt1 = Convert.ToDateTime(dt.Rows[0]["From Date"]);
                        DateTime dt2 = Convert.ToDateTime(dt.Rows[0]["To Date"]);

                        lblEvent.Text = dt.Rows[0]["Event"].ToString() + " (" + dt1.ToString("dd MMM yyyy") + " - " + dt2.ToString("dd MMM yyyy") + ")";
                        
                    }

                    if (dt.Rows.Count > 1)
                    {
                        DateTime dt1 = Convert.ToDateTime(dt.Rows[0]["From Date"]);
                        DateTime dt2 = Convert.ToDateTime(dt.Rows[0]["To Date"]);
                        DateTime dt3 = Convert.ToDateTime(dt.Rows[1]["From Date"]);
                        DateTime dt4 = Convert.ToDateTime(dt.Rows[1]["To Date"]);

                        lblEvent.Text = dt.Rows[0]["Event"].ToString() + " (" + dt1.ToString("dd MMM yyyy") + " - " + dt2.ToString("dd MMM yyyy") + ")";
                        Label1.Text = dt.Rows[1]["Event"].ToString() + " (" + dt3.ToString("dd MMM yyyy") + " - " + dt4.ToString("dd MMM yyyy") + ")";
  
                    }
                }
            }
        }


       
    }



   // for  image view popup




    protected void lblConnected_Click(object sender, EventArgs e)
    {

    }
}