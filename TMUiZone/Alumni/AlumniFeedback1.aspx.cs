using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Alumni_AlumniFeedback : System.Web.UI.Page
{
    DL.StudentDetailsViewDL sdvDL = new DL.StudentDetailsViewDL();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getfeedbackcount();
            bindData();
            Form();
            //bindRadiobuttondata();
            BindAcademicYear();
            lblfDate.Text = DateTime.Today.ToString("dd MMM yyyy");

        }
    }

    public void BindAcademicYear()
    {
        try
        {

            SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            con.Open();
            da.Fill(dt1);
            con.Close();
            lblAF.Text = dt1.Rows[0]["No_"].ToString();

        }
        catch
        {
        }
    }


    public void bindData()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("spProcFeedbackData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentNo", Session["uid"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {

                lblAlumniName.Text = dt.Rows[0]["Student Name"].ToString();
                lblCurrOrgWthDesg.Text = dt.Rows[0]["Desi"].ToString();
                lblDesg.Text = dt.Rows[0]["Emp"].ToString();
                lblProgWthSpc.Text = dt.Rows[0]["Program Code"].ToString();
                lblEmail.Text = dt.Rows[0]["Email ID"].ToString();

                lblYearPass.Text = dt.Rows[0]["Passout Year"].ToString();
                lblMob.Text = dt.Rows[0]["Mobile No_"].ToString();

            }
            else
            {

            }
        }
        catch { }
    }
    //    public void bindRadiobuttondata()
    //{
    //}

    public void Form()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("spProcFeedbackQuestion", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SNo", Session["uid"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = " Attribute = 'Rating Attribute'";
            DataTable dtNew = dv1.ToTable();
            if (dtNew.Rows.Count > 0)
            {
                hfcount.Value = dtNew.Rows.Count.ToString();
                grdaddedEmployee.DataSource = dtNew;
                grdaddedEmployee.DataBind();
            }
            else
            {
                grdaddedEmployee.DataSource = "";
                grdaddedEmployee.DataBind();
            }
            dv1.RowFilter = " Attribute = 'Yes/No Attribute'";
            DataTable dtNewYesNo = dv1.ToTable();
            if (dtNewYesNo.Rows.Count > 0)
            {
                hfcountYesNo.Value = dtNewYesNo.Rows.Count.ToString();
                rptrYesNo.DataSource = dtNewYesNo;
                rptrYesNo.DataBind();
            }
            else
            {
                rptrYesNo.DataSource = "";
                rptrYesNo.DataBind();
            }
            dv1.RowFilter = " Attribute = 'Comment Attribute'";
            DataTable dtNewrptrCommnt = dv1.ToTable();
            if (dtNewrptrCommnt.Rows.Count > 0)
            {
                rptrCommnt.DataSource = dtNewrptrCommnt;
                rptrCommnt.DataBind();
            }
            else
            {
                rptrCommnt.DataSource = "";
                rptrCommnt.DataBind();
            }
        }
        catch { }
    }
    public void getfeedbackcount()
    {
        try
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("GetAlumaniFeedback", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@SNo", Session["uid"].ToString());
            cmd1.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            con.Close();
            if (dt1.Rows.Count > 0)
                {


                btnSubmit.Visible = false;
            }
            else
            {
                btnSubmit.Visible = true;
            }
        }
        catch (Exception ex) { }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            con.Open();
            SqlCommand cmd1 = new SqlCommand("GetAlumaniFeedback", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@SNo", Session["uid"].ToString());
            cmd1.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            con.Close();
            if (dt1.Rows.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Already fill feedback or feedback date over')", true);
            }
            else
            {




                string studentNo = Session["uid"].ToString();
                foreach (RepeaterItem item in grdaddedEmployee.Items)
                {
                    RadioButtonList ddr = item.FindControl("ddlRating1") as RadioButtonList;
                    Label lblQuestionNo = item.FindControl("lblQuestionNo") as Label;
                    Label lblQuestions = item.FindControl("lblQuestions") as Label;
                    HiddenField hfSequence = item.FindControl("hfSequence") as HiddenField;
                    HiddenField HfQuestionNo = item.FindControl("HfQuestionNo") as HiddenField;
                    SqlCommand cmd = new SqlCommand("spProcFeedbackQuestionInsert", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    RepeaterItem ddlRating1 = (RepeaterItem)grdaddedEmployee.FindControl("ddlRating1");
                    con.Open();
                    cmd.Parameters.AddWithValue("@Questions", Convert.ToInt32(HfQuestionNo.Value));
                    cmd.Parameters.AddWithValue("@hfSequence", Convert.ToInt32(hfSequence.Value));
                    cmd.Parameters.AddWithValue("@StudentNo", Session["uid"].ToString());
                    cmd.Parameters.AddWithValue("@QuestionsDes", lblQuestions.Text);
                    if (HfQuestionNo.Value == "12" && lblQuestionNo.Text=="12")
                    {
                        cmd.Parameters.AddWithValue("@QuestionsOpt", Convert.ToInt32(0));
                    }
                    else if (ddr.SelectedValue == "" && HfQuestionNo.Value == "12")
                        {
                            cmd.Parameters.AddWithValue("@QuestionsOpt", Convert.ToInt32(0)); 
                        }
                    else 
                        { 
                            cmd.Parameters.AddWithValue("@QuestionsOpt", Convert.ToInt32(ddr.SelectedValue)); 
                        }
                    cmd.Parameters.AddWithValue("@QuestionsOptYesNo", 0);
                    cmd.Parameters.AddWithValue("@txtrptrCommnt", "");
                    cmd.Parameters.AddWithValue("@attribute", "Rating Attribute");
                    cmd.ExecuteNonQuery();
                    con.Close();
                    // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data submitted successfully')", true);

                }
                foreach (RepeaterItem item in rptrYesNo.Items)
                {
                    RadioButtonList ddr = item.FindControl("ddrptrYesNo") as RadioButtonList;
                    Label lblQuestionNo = item.FindControl("lblrptrYesNoQuestionNo") as Label;
                    Label lblQuestions = item.FindControl("lblrptrYesNoQuestions") as Label;
                    HiddenField hfSequence = item.FindControl("hfrptrYesNoSequence") as HiddenField;
                    SqlCommand cmd = new SqlCommand("spProcFeedbackQuestionInsert", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    RepeaterItem ddlRating1 = (RepeaterItem)grdaddedEmployee.FindControl("ddlRating1");
                    con.Open();
                    cmd.Parameters.AddWithValue("@Questions", Convert.ToInt32(lblQuestionNo.Text));
                    cmd.Parameters.AddWithValue("@hfSequence", Convert.ToInt32(hfSequence.Value));
                    cmd.Parameters.AddWithValue("@StudentNo", Session["uid"].ToString());
                    cmd.Parameters.AddWithValue("@QuestionsDes", lblQuestions.Text);
                    cmd.Parameters.AddWithValue("@QuestionsOpt", 0);
                    cmd.Parameters.AddWithValue("@QuestionsOptYesNo", Convert.ToInt32(ddr.SelectedValue));
                    cmd.Parameters.AddWithValue("@txtrptrCommnt", "");
                    cmd.Parameters.AddWithValue("@attribute", "Yes/No Attribute");
                    cmd.ExecuteNonQuery();
                    con.Close();
                    // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data submitted successfully')", true);

                }
                foreach (RepeaterItem item in rptrCommnt.Items)
                {
                    Label lblQuestionNo = item.FindControl("lblrptrCommntQuestionNo") as Label;
                    Label lblQuestions = item.FindControl("lblrptrCommntQuestions") as Label;
                    HiddenField hfSequence = item.FindControl("hfrptrCommntSequence") as HiddenField;
                    TextBox txtrptrCommnt = item.FindControl("txtrptrCommnt") as TextBox;
                    SqlCommand cmd = new SqlCommand("spProcFeedbackQuestionInsert", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    RepeaterItem ddlRating1 = (RepeaterItem)grdaddedEmployee.FindControl("ddlRating1");
                    con.Open();
                    cmd.Parameters.AddWithValue("@Questions", Convert.ToInt32(lblQuestionNo.Text));
                    cmd.Parameters.AddWithValue("@hfSequence", Convert.ToInt32(hfSequence.Value));
                    cmd.Parameters.AddWithValue("@StudentNo", Session["uid"].ToString());
                    cmd.Parameters.AddWithValue("@QuestionsDes", lblQuestions.Text);
                    cmd.Parameters.AddWithValue("@QuestionsOpt", 0);
                    cmd.Parameters.AddWithValue("@QuestionsOptYesNo", Convert.ToInt32(0));
                    cmd.Parameters.AddWithValue("@txtrptrCommnt", txtrptrCommnt.Text.Trim());
                    cmd.Parameters.AddWithValue("@attribute", "Comment Attribute");
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data submitted successfully')", true);
                Form();
                getfeedbackcount();

            }
        }
        catch
        { }
    }
    protected void rptrYesNo_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
            {
                RadioButtonList ddrptrYesNo = (RadioButtonList)e.Item.FindControl("ddrptrYesNo");

                SqlCommand cmd = new SqlCommand("spProcGetSavedDataforFeedback", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentNo", Session["uid"].ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                DataView dv1 = dt.DefaultView;
                dv1.RowFilter = " Attribute = 'Yes/No Attribute'";
                DataTable dtNew = dv1.ToTable();
                ddrptrYesNo.SelectedValue = dtNew.Rows[e.Item.ItemIndex]["Attribute Yes_ No"].ToString();
            }

        }
        catch (Exception ex) { }
    }
    protected void grdaddedEmployee_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
            {
                RadioButtonList ddlRating1 = (RadioButtonList)e.Item.FindControl("ddlRating1");

                SqlCommand cmd = new SqlCommand("spProcGetSavedDataforFeedback", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentNo", Session["uid"].ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                DataView dv1 = dt.DefaultView;
                dv1.RowFilter = " Attribute = 'Rating Attribute'";
                DataTable dtNew = dv1.ToTable();
                ddlRating1.SelectedValue = dtNew.Rows[e.Item.ItemIndex]["Rate"].ToString();


            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void rptrCommnt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            TextBox txtrptrCommnt = (TextBox)e.Item.FindControl("txtrptrCommnt");
            SqlCommand cmd = new SqlCommand("spProcGetSavedDataforFeedback", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentNo", Session["uid"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = " Attribute = 'Comment Attribute'";
            DataTable dtNew = dv1.ToTable();
            if (dtNew.Rows.Count > 0)
            {
                txtrptrCommnt.Text = dtNew.Rows[e.Item.ItemIndex]["Attribute Comments"].ToString();
            }
            else { txtrptrCommnt.Text = ""; }
            // txtrptrCommnt.Text = dtNew.Rows[0]["Attribute Comments"].ToString();
        }
    }
}