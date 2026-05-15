using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Formvisibilty : System.Web.UI.Page
{

    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["uid"].ToString() == "TMU02982" || Session["uid"].ToString() == "TMU08617"  || Session["uid"].ToString() == "TMU00588" || Session["UserGroup"].ToString() == "ADMIN" || Session["UserGroup"].ToString() == "REGISTRAR")
                {
                    BindData();
                }
            }
            catch { Response.Redirect("~/Default.aspx"); }

        }
    }


    public void BindData()
    {
        DataSet ds = new DataSet();
        DataTable FromTable = new DataTable();
        try
        {
            con1.Open();
            string stcmd = "Select id,[Academic Year],[Odd Sem],[Even Sem],[Year],Convert(varchar(11),[Open Date],106) as [Open Date],Convert(varchar(11),[Close Date],106) as [Close Date],Active,FormName,SubmissionAllow    from FormVisibilitySetup";
            SqlCommand cmd = new SqlCommand(stcmd, con1);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(ds);
            cmd.ExecuteNonQuery();
            FromTable = ds.Tables[0];
            if (FromTable.Rows.Count > 0)
            {
                grdFormvisibilty.DataSource = FromTable;
                grdFormvisibilty.DataBind();
            }
    
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            ds.Dispose();
            con1.Close();
        }
        }
    protected void grdFormvisibilty_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdFormvisibilty.EditIndex = e.NewEditIndex;
        BindData();
    }
    protected void grdFormvisibilty_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdFormvisibilty.EditIndex = -1;
        BindData();
    }
    protected void grdFormvisibilty_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        Label lblEditid = (Label)grdFormvisibilty.Rows[e.RowIndex].FindControl("lblEditid");
        Label lblEditFormName = (Label)grdFormvisibilty.Rows[e.RowIndex].FindControl("lblEditFormName");
        TextBox lblEditAcademicYear = (TextBox)grdFormvisibilty.Rows[e.RowIndex].FindControl("lblEditAcademicYear");

        CheckBox chkEditOddSem = (CheckBox)grdFormvisibilty.Rows[e.RowIndex].FindControl("chkEditOddSem");

        CheckBox chkEditEvenSem = (CheckBox)grdFormvisibilty.Rows[e.RowIndex].FindControl("chkEditEvenSem");

        CheckBox chkEditYear = (CheckBox)grdFormvisibilty.Rows[e.RowIndex].FindControl("chkEditYear");

        CheckBox chkEditProctor = (CheckBox)grdFormvisibilty.Rows[e.RowIndex].FindControl("chkEditProctor");

        TextBox lblEditOpenDate = (TextBox)grdFormvisibilty.Rows[e.RowIndex].FindControl("lblEditOpenDate");

        TextBox lblEditCloseDate = (TextBox)grdFormvisibilty.Rows[e.RowIndex].FindControl("lblEditCloseDate");

        CheckBox chkEditActive = (CheckBox)grdFormvisibilty.Rows[e.RowIndex].FindControl("chkEditActive");

        CheckBox chkEditSubmissionAllow = (CheckBox)grdFormvisibilty.Rows[e.RowIndex].FindControl("chkEditSubmissionAllow");

        string OddSem = "";
        if (chkEditOddSem.Checked == true)
            OddSem = "1";
        else if (chkEditOddSem.Checked == false)
            OddSem = "0";

        string EvenSem = "";
        if (chkEditEvenSem.Checked == true)
            EvenSem = "1";
        else if (chkEditEvenSem.Checked == false)
            EvenSem = "0";

        string Year = "";
        if (chkEditYear.Checked == true)
            Year = "1";
        else if (chkEditYear.Checked == false)
            Year = "0";


        string Active = "";
        if (chkEditActive.Checked == true)
            Active = "1";
        else if (chkEditActive.Checked == false)
            Active = "0";

        string SubmissionAllow = "";
        if (chkEditSubmissionAllow.Checked == true)
            SubmissionAllow = "1";
        else if (chkEditSubmissionAllow.Checked == false)
            SubmissionAllow = "0";

        string dtCloseDate = Convert.ToDateTime(lblEditCloseDate.Text).ToString("dd MMM yyyy");
        string dtOpenDate = Convert.ToDateTime(lblEditOpenDate.Text).ToString("dd MMM yyyy");

        SqlCommand cmd = new SqlCommand("update FormVisibilitySetup set [Academic Year]='" + lblEditAcademicYear.Text + "',[Odd Sem]='" + OddSem + "',[Even Sem]='" + EvenSem + "',Year='" + Year + "',[Open Date]='" + dtOpenDate + "',[Close Date]='" + dtCloseDate + "',Active='" + Active + "',SubmissionAllow='" + SubmissionAllow + "',UpdatedDate=getdate() where id='" + lblEditid.Text + "'", con1);

        con1.Open();

        cmd.ExecuteNonQuery();
        con1.Close();
        grdFormvisibilty.EditIndex = -1;
        BindData();

    }
}

