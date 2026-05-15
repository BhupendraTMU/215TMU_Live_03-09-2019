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

public partial class Student_NAACForm : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           

                if (!IsPostBack)
                {

                    getQuestion();

                }

           

        }
        catch (Exception ex)
        {
            Response.Redirect("../Default.aspx");

        }
    }

    public void getQuestion()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Get_NAACQuestion", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentNo", Session["uid"].ToString());
         
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            con.Open();
            da.Fill(ds);
            con.Close();
            if (ds.Tables[1].Rows[0]["Yes"].ToString() == "0")
            {
                logoDiv.Visible = true;
                lblMsg.Visible = false;
                grddata.DataSource = ds.Tables[0];
                grddata.DataBind();
            }
            else
            {
                logoDiv.Visible = false;
                lblMsg.Visible = true;
            }


        }
        catch (Exception ex)
        {

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            foreach (GridViewRow row in grddata.Rows)
            {
                CheckBox chk4 = row.FindControl("chk4") as CheckBox;
                CheckBox chk3 = row.FindControl("chk3") as CheckBox;
                CheckBox chk2 = row.FindControl("chk2") as CheckBox;
                CheckBox chk1 = row.FindControl("chk1") as CheckBox;
              
                CheckBox chk0 = row.FindControl("chk0") as CheckBox;
            
                TextBox option1 = (TextBox)grddata.FooterRow.FindControl("txtoption1");
                TextBox option2 = (TextBox)grddata.FooterRow.FindControl("txtoption2");
                TextBox option3 = (TextBox)grddata.FooterRow.FindControl("txtoption3");
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
                if (option1.Text == "" || option2.Text == "" || option3.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Suggestion box can not blank.')", true); return;
                }
            }

            if (grddata.Rows.Count != 0)
            {
                foreach (GridViewRow row in grddata.Rows)
                {
                    Label question = row.FindControl("lblQuestion") as Label;
                    HiddenField questionId = (HiddenField)row.FindControl("hdfID");
                   
                        CheckBox chk4 = row.FindControl("chk4") as CheckBox;
                        CheckBox chk3 = row.FindControl("chk3") as CheckBox;
                        CheckBox chk2 = row.FindControl("chk2") as CheckBox;
                        CheckBox chk1 = row.FindControl("chk1") as CheckBox;
                        CheckBox chk0 = row.FindControl("chk0") as CheckBox;
                   
                    string StudentNo = Session["uid"].ToString();
                    SqlCommand cmd1 = new SqlCommand("Pro_NAACSurveyInput", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@StudentNo", Session["uid"].ToString());
                    cmd1.Parameters.AddWithValue("@Question", question.Text);
                    cmd1.Parameters.AddWithValue("@QuestionId", questionId.Value);
                    cmd1.Parameters.AddWithValue("@Option1", chk0.Checked);
                    cmd1.Parameters.AddWithValue("@Option2", chk1.Checked);
                    cmd1.Parameters.AddWithValue("@Option3", chk2.Checked);
                    cmd1.Parameters.AddWithValue("@Option4", chk3.Checked);
                    cmd1.Parameters.AddWithValue("@Option5", chk4.Checked);
                    con.Open();
                    cmd1.ExecuteNonQuery();
                    con.Close();

                }
                Label question1 = (Label)grddata.FooterRow.FindControl("txtQustion");
                TextBox option1 = (TextBox)grddata.FooterRow.FindControl("txtoption1");
                TextBox option2 = (TextBox)grddata.FooterRow.FindControl("txtoption1");
                TextBox option3 = (TextBox)grddata.FooterRow.FindControl("txtoption2");
               
                SqlCommand cmd2 = new SqlCommand("Pro_NAACSurveyInput", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@StudentNo", Session["uid"].ToString());
                cmd2.Parameters.AddWithValue("@Question", question1.Text);
                cmd2.Parameters.AddWithValue("@QuestionId", 21);
                cmd2.Parameters.AddWithValue("@Option1", option1.Text);
                cmd2.Parameters.AddWithValue("@Option2", option2.Text);
                cmd2.Parameters.AddWithValue("@Option3", option3.Text);
                cmd2.Parameters.AddWithValue("@Option4", "");
                cmd2.Parameters.AddWithValue("@Option5", "");
                con.Open();
                cmd2.ExecuteNonQuery();
                con.Close();
                   
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CropImage", "alert('Data Saved Successfully.');", true);
                getQuestion();
            }
        }
        catch (Exception ex)
        {
        }



    }
}