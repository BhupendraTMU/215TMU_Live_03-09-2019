using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;


public partial class Alumni_AlumniFeedback : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString);
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (!IsPostBack)
                {
                    lblSession.Text = Session["AcademicYear"].ToString();
                    SqlCommand cmd = new SqlCommand("select * from [HRMSPortal].[dbo].[tbl_AlumniFeedback] where StudentNo_='" + Session["uid"].ToString() + "' and [Academic Year]='" + Session["AcademicYear"].ToString() + "' and ([Semester]='" + Session["Semester"].ToString() + "' or [Semester]='" + Session["Year"].ToString() + "')", con);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        pnlMain.Visible = false;
                        pnlError.Visible = true;
                    }
                    else
                    {
                        pnlMain.Visible = true;
                        pnlError.Visible = false;

                        SqlCommand cmd1 = new SqlCommand("select * from [TMU$Alumni Registration] where [Student No_]='" + Session["uid"].ToString() + "'", con);
                        DataTable dt1 = new DataTable();
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                        da1.Fill(dt1);
                        if (dt1.Rows.Count > 0)
                        {
                            lblAlumniName.Text = dt1.Rows[0]["Student Name"].ToString();
                            lblCurrDesg.Text = dt1.Rows[0]["Designation"].ToString();
                            Label1.Text = dt1.Rows[0]["Program Code"].ToString();
                            Label2.Text = dt1.Rows[0]["Email ID"].ToString();
                            Label3.Text = dt1.Rows[0]["Passout Year"].ToString();
                            Label4.Text = dt1.Rows[0]["Mobile No_"].ToString();
                        }


                        BindGridView();
                        //getSubject();
                    }
                }
                else
                {
                    //chkrecord();
                }

            }
            catch
            {
                Response.Redirect("~/Default.aspx");
            }

           
            
        }
    }
    public void BindGridView()
    {
        SqlCommand cmd = new SqlCommand("Get_AlumniFeedbackQuestion", con);
        cmd.CommandType = CommandType.StoredProcedure;
      
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        grdCourse.DataSource = dt;
        grdCourse.DataBind();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            foreach (GridViewRow row in grdCourse.Rows)
            {
                CheckBox chk4 = row.FindControl("rbtExcell") as CheckBox;
                CheckBox chk3 = row.FindControl("rbtVGood") as CheckBox;
                CheckBox chk2 = row.FindControl("rbtGood") as CheckBox;
                CheckBox chk1 = row.FindControl("rbtAverage") as CheckBox;

                CheckBox chk0 = row.FindControl("rbtPoor") as CheckBox;

               
              
                bool checkedChk = false;
                if (chk4.Checked)
                {
                    checkedChk = true;
                }
                else if (chk3.Checked)
                {
                    checkedChk = true;
                }
                else if (chk2.Checked)
                {
                    checkedChk = true;
                }
                else if (chk1.Checked)
                {
                    checkedChk = true;
                }
                else if (chk0.Checked)
                {
                    checkedChk = true;
                }
                if (!checkedChk)
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Atleast one checkbox should be selected in row " + (row.RowIndex + 1) + "')", true); return;
                }
                if (txtSugg.Text=="")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Suggestion box can not be blank.')", true); return;
                }
            }

            if (grdCourse.Rows.Count != 0)
            {
                foreach (GridViewRow row in grdCourse.Rows)
                {
                    Label Question = (Label)row.FindControl("lblQuestion");
                    HiddenField questionId = (HiddenField)row.FindControl("hdfID");
                    CheckBox Excell = row.FindControl("rbtExcell") as CheckBox;
                    CheckBox VGood = row.FindControl("rbtVGood") as CheckBox;
                    CheckBox Good = row.FindControl("rbtGood") as CheckBox;
                    CheckBox Average = row.FindControl("rbtAverage") as CheckBox;

                    CheckBox Poor = row.FindControl("rbtPoor") as CheckBox;

                    string StudentNo = Session["uid"].ToString();
                    SqlCommand cmd1 = new SqlCommand("Pro_AlumniFeedback", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@StudentNo", Session["uid"].ToString());
                    cmd1.Parameters.AddWithValue("@Question", Question.Text);
                    cmd1.Parameters.AddWithValue("@QuestionId", questionId.Value);
                    cmd1.Parameters.AddWithValue("@Option1", Excell.Checked);
                    cmd1.Parameters.AddWithValue("@Option2", VGood.Checked);
                    cmd1.Parameters.AddWithValue("@Option3", Good.Checked);
                    cmd1.Parameters.AddWithValue("@Option4", Average.Checked);
                    cmd1.Parameters.AddWithValue("@Option5", Poor.Checked);
                    con.Open();
                    cmd1.ExecuteNonQuery();
                    con.Close();

                }
               

                ScriptManager.RegisterStartupScript(this, this.GetType(), "CropImage", "alert('Data Saved Successfully.');", true);
                BindGridView();
            }
        }
        catch (Exception ex)
        {
        }
    }
}