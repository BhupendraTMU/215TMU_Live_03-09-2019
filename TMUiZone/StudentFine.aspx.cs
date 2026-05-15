using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class StudentFine : System.Web.UI.Page
{
    DL.StudentFineDL sdl = new DL.StudentFineDL();
    static string FacultyCode = ""; static string CollegeCode = ""; string StudentNo = "";
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SqlCommand cmd1 = new SqlCommand("select No_ from [TMU$Employee]", con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            
            da1.Fill(dt1);
            drpStaffCode.DataTextField = "No_";
            drpStaffCode.DataValueField = "No_";
            drpStaffCode.DataSource = dt1;
            drpStaffCode.DataBind();
            drpStaffCode.Items.Insert(0, "--Select--");

            SqlCommand cmd2 = new SqlCommand("select Description from [TMU$Action Taken]", con);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            drpActionTaken.DataTextField = "Description";
            drpActionTaken.DataValueField = "Description";
            drpActionTaken.DataSource = dt2;
            drpActionTaken.DataBind();
            drpActionTaken.Items.Insert(0, "-- Select --");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DateTime dt=Convert.ToDateTime("1 - 1 - 1753");
        if (txtDateCommited.Text != "")
        {
            dt = Convert.ToDateTime(txtDateCommited.Text);
        }
        decimal fine=0;
         if(txtAmount.Text!="")
             fine=Convert.ToDecimal(txtAmount.Text);

         string StaffCode = "";
         if (drpStaffCode.SelectedItem.Text != "")
             StaffCode = drpStaffCode.SelectedItem.Text;

         //sdl.insertStudentDeciplineLine(txtStudentNo.Text, lblCourse.Text, lblSemester.Text, lblSection.Text,
         //     lblAcademicYear.Text, dt, drpActionTaken.SelectedItem.Text, StaffCode, fine);
         //sdl.insertFineGL(txtStudentNo.Text, drpActionTaken.SelectedItem.Text, Convert.ToDecimal(txtAmount.Text));
         bindGrid();
         blank();
    }
    public void blank()
    {
        txtDateCommited.Text = "";
        drpActionTaken.SelectedIndex = 0;
        drpStaffCode.SelectedIndex = 0;
        txtAmount.Text = "";
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchCustomers(string prefixText, int count)
 {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select No_ from [TMU$Student - COLLEGE] where  No_ like '" + prefixText.ToUpper() + "%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["No_"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }

    protected void grdFineInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {        
        grdFineInfo.PageIndex = e.NewPageIndex;
        bindGrid();
    }
    protected void txtStudentNo_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = sdl.GetStudentDetails(txtStudentNo.Text,CollegeCode);       
        
        if (dt.Rows.Count > 0)
        {
            lblName.Text = dt.Rows[0]["Student Name"].ToString();
            lblCourse.Text = dt.Rows[0]["Course Code"].ToString();
            lblSection.Text = dt.Rows[0]["Section"].ToString();
            lblSemester.Text = dt.Rows[0]["Semester"].ToString();
            lblAcademicYear.Text = dt.Rows[0]["Academic Year"].ToString();

        }
        else
        {
            lblCourse.Text = "";
            lblName.Text = "";
            lblSemester.Text = "";
            lblSection.Text = "";
            lblAcademicYear.Text = "";
           
        }
        bindGrid();
    }

    public void bindGrid()
    {
        DataTable dt1 = new DataTable();
        dt1 = sdl.GetStudentFineDetails(txtStudentNo.Text);
        if (dt1.Rows.Count > 0)
        {
            grdFineInfo.DataSource = dt1;
            grdFineInfo.DataBind();
        }
        else
        {
            grdFineInfo.DataSource = null;
            grdFineInfo.DataBind();
        }
    }

    protected void grdFineInfo_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
}