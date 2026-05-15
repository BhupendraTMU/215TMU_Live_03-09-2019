using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
 //Test
//using WEBStudentFine;//Test
using WSNAVLIVE_VP;

public partial class StudentFine : System.Web.UI.Page
{
    DL.StudentFineDL sdl = new DL.StudentFineDL();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    static string FacultyCode = ""; static string CollegeCode = "";  string StudentNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FINE"].ToString().Trim() == "FINE") { } else { Response.Redirect("~/Default.aspx"); }
        if (!IsPostBack)
        {

            try
            {
                SqlCommand cmd2 = new SqlCommand("select Description from [TMU$Action Taken]", con);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                drpActionTaken.DataTextField = "Description";
                drpActionTaken.DataValueField = "Description";
                drpActionTaken.DataSource = dt2;
                drpActionTaken.DataBind();
                drpActionTaken.Items.Insert(0, "-- Select --");

                FacultyCode = Session["uid"].ToString();
                CollegeCode = Session["GlobalDimension1Code"].ToString();
            }
            catch
            {
                Response.Redirect("~/Default.aspx");
            }
        }
       
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        StudentNo = hfStudentId.Value;
        btnSave.Enabled = false;
        DateTime dt = new DateTime();
        dt = Convert.ToDateTime(txtDateCommited.Text);
        dt.ToShortDateString();
            if (txtStudentNo.Text.Contains("(") && txtStudentNo.Text.Contains(")")) 
             StudentNo = txtStudentNo.Text.Split('(', ')')[1];

            sdl.insertStudentDeciplineLine(StudentNo, txtCourse.Text, txtSemester.Text, txtSection.Text,
                  txtAcademicYear.Text, Convert.ToDateTime(dt.ToShortDateString()), drpActionTaken.SelectedItem.Text, Session["uid"].ToString(), Convert.ToDecimal(txtAmount.Text), txtRemarks.Text);
       // if (drpActionTaken.SelectedItem.ToString() == "Fine")
           // sdl.insertFineGL(StudentNo, Convert.ToDecimal(txtAmount.Text), txtSection.Text, txtAcademicYear.Text, txtCourse.Text, txtSemester.Text,Session["GlobalDimension1Code"].ToString());
        if (drpActionTaken.SelectedItem.ToString() == "Fine")
        {
          
            string datasourse = con.DataSource.ToString();
            if (datasourse == "172.0.1.108")
            {
                DataTable dtNAV = new DataTable();
                SqlCommand cmdNAV = new SqlCommand("Proc_GetNAVCreditionalLive", con);
                cmdNAV.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter daNAV = new SqlDataAdapter(cmdNAV);
                daNAV.Fill(dtNAV);
                VoucherPosting vpWeb = new VoucherPosting();
                vpWeb.UseDefaultCredentials = true;
                vpWeb.Url = dtNAV.Rows[0]["URL"].ToString();
                vpWeb.Credentials = new NetworkCredential(dtNAV.Rows[0]["UserID"].ToString(), dtNAV.Rows[0]["Password"].ToString());           
                vpWeb.StudentFine(StudentNo, Convert.ToDecimal(txtAmount.Text), txtRemarks.Text.Trim(), Convert.ToDateTime(txtDateCommited.Text));//Live
            }
            else
            {
                NAVWeb.VoucherPosting vpWeb = new NAVWeb.VoucherPosting();
                vpWeb.UseDefaultCredentials = true;
                //  vpWeb.Url = "http://172.14.7.107:7047/DynamicsNAV71/WS/TMU/Codeunit/VoucherPosting";//test
               vpWeb.Url = "http://172.14.7.107:6047/TEST/WS/TMU/Codeunit/VoucherPosting";//Test
               vpWeb.Credentials = new NetworkCredential("shubham\\administrator", "abcd@1234");//Test
               vpWeb.StudentFine(StudentNo, Convert.ToDecimal(txtAmount.Text), txtRemarks.Text.Trim(), Convert.ToDateTime(txtDateCommited.Text));//Test
            }
            

        }
        bindGrid();
        blank();
        txtAmount.Text = "0";
        txtAmount.Enabled = false;
        btnSave.Enabled = true;
    }
    public void blank()
    {
        txtDateCommited.Text = "";
        drpActionTaken.SelectedIndex = 0;
        txtAmount.Text = "";
        txtRemarks.Text = "";
        bindGrid();
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
//                    cmd.CommandText = "select [Student Name],[Enrollment No_] from [TMU$Student - COLLEGE] where  Upper([Enrollment No_]) like '" + prefixText.ToUpper() + "%'" + @"
//                    and [Course Code] in (select distinct([Course Code]) from [TMU$Course Wise Faculty] where [Global Dimension 1 Code]=(select [Global Dimension 1 Code] from [TMU$Employee] where [No_]='" +FacultyCode+"'))";//17-10-2016
                    cmd.CommandText = "select [Student Name],[Enrollment No_] from [TMU$Student - COLLEGE] where  Upper([Enrollment No_]) like '" + prefixText.ToUpper() + "%'" + @"
                    and [Global Dimension 1 Code]='" + CollegeCode + "' and [Student Status]=1 ";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> customers = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                           // customers.Add(sdr["Enrollment No_"].ToString() + " (" + sdr["Student Name"].ToString() + ")");
                            customers.Add(sdr["Enrollment No_"].ToString()) ;
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
        //if (txtStudentNo.Text.Contains("(") && txtStudentNo.Text.Contains(")")) 
        //StudentNo=txtStudentNo.Text.Split('(', ')')[1];  
        
        DataTable dt = new DataTable();
        //dt = sdl.GetStudentDetails(StudentNo); 
        if (txtStudentNo.Text != "")
        {
            dt = sdl.GetStudentDetails(txtStudentNo.Text, CollegeCode);
        }       
        
        if (dt.Rows.Count > 0)
        {           
            hfStudentId.Value = dt.Rows[0]["No_"].ToString();// 15-10-2016
            StudentNo = hfStudentId.Value;  // 15-10-2016
            CalendarExtender2.StartDate =Convert.ToDateTime(dt.Rows[0]["Date of Joining"]);
            txtName.Text = dt.Rows[0]["Student Name"].ToString();
            txtCourse.Text = dt.Rows[0]["Course Code"].ToString();
            txtSection.Text = dt.Rows[0]["Section"].ToString();
            txtSemester.Text = dt.Rows[0]["Semester"].ToString();
            if (txtSemester.Text == "")
            { txtSemester.Text=dt.Rows[0]["Year"].ToString(); }
            txtAcademicYear.Text = dt.Rows[0]["Academic Year"].ToString();
            btnSave.Enabled = true;
            bindGrid();
        }
        else
        {
            hfStudentId.Value = "";
            txtCourse.Text = "";
            txtName.Text = "";
            txtSemester.Text = "";
            txtSection.Text = "";
            txtAcademicYear.Text = "";
            btnSave.Enabled = false;
        }
        
    }

    public void bindGrid()
    {
        DataTable dt1 = new DataTable();
        dt1 = sdl.GetStudentFineDetails(StudentNo);
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
    protected void drpActionTaken_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpActionTaken.SelectedItem.ToString() == "Fine")
            txtAmount.Enabled = true;
        else
            txtAmount.Enabled = false;
    }
}