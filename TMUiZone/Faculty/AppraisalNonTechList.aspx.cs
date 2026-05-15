using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_AppraisalNonTechList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                AcadmicYear();
               // Form();
                facultylist();
            }
            catch
            { }
        }
    }
    public void AcadmicYear()
    {
        SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        con1.Open();
        da.Fill(dt1);
        con1.Close();
        lblAcad.Text = dt1.Rows[0]["Details"].ToString();
        lblDepart.Text = Session["GlobalDimension1Code"].ToString();
    }

    private void facultylist(string sortExpression = null)
    {
        SqlCommand cmd = new SqlCommand("proc_Gettbl_NoNTeachAP", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Fid", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@AcadmicYear", lblAcad.Text);
        cmd.Parameters.AddWithValue("@UserGroup", Session["UserGroup"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        using (DataTable dt = new DataTable())
        {
            con.Open();
            da.Fill(dt);
            con.Close();
            if (sortExpression != null)
            {
                DataView dv = dt.AsDataView();
                this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                dv.Sort = sortExpression + " " + this.SortDirection;
                GrdExamList.DataSource = dv;
            }
            else
            {
                GrdExamList.DataSource = dt;
            }
            GrdExamList.DataBind();
        }
        //GrdExamList.DataSource = dt;
        //GrdExamList.DataBind();
    }

    public void Form()
    {
        SqlCommand cmd = new SqlCommand("proc_GetNonTechPMList", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EmpCode", Session["uid"].ToString());
	    cmd.Parameters.AddWithValue("@AcademicYear",lblAcad.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Open();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            btnsave.Visible = true; btnSubmit.Visible = true;
            btntest.Visible = true;
            grdaddedEmployee.DataSource = dt;
            grdaddedEmployee.DataBind();
        }
        else
        {
            btnsave.Visible = false; btnSubmit.Visible = false;
            btntest.Visible = false;
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("SubmitAppliedAppr", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EmpCode", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@AcademicYear", lblAcad.Text);
        cmd.Parameters.AddWithValue("@Status", "3");
        cmd.Parameters.AddWithValue("@HID", FF.Value);
        cmd.ExecuteNonQuery();
        con.Close();
        Form();
        facultylist();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data submitted successfully')", true);
        DivForm.Visible = false;
        btnback.Visible = false;
        GrdExamList.Visible = true;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("SubmitAppliedAppr", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EmpCode", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@AcademicYear", lblAcad.Text);
        cmd.Parameters.AddWithValue("@Status", "4");
        cmd.Parameters.AddWithValue("@HID", FF.Value);
        cmd.ExecuteNonQuery();
        con.Close();
        Form();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Rejected successfully')", true);
        facultylist();
        DivForm.Visible = false;
        btnback.Visible = false;
        GrdExamList.Visible = true;
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        DivForm.Visible = false;
        btnback.Visible = false;
        GrdExamList.Visible = true;

    }
    protected void lblview_Click(object sender, EventArgs e)
    {
        FF.Value = "";

        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int i = Convert.ToInt32(row.RowIndex);
        HiddenField HfStaffcode = (HiddenField)row.FindControl("HfStudentNo");
        HiddenField HfSName = (HiddenField)row.FindControl("hfFName");
        FF.Value = HfStaffcode.Value;
        SqlCommand cmd = new SqlCommand("proc_GetNonTechPMList", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EmpCode", FF.Value);
        cmd.Parameters.AddWithValue("@AcademicYear", lblAcad.Text);
        cmd.Parameters.AddWithValue("@UserGroup", Session["UserGroup"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Open();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            btnsave.Visible = true; btnSubmit.Visible = true;
            btntest.Visible = true;
            grdaddedEmployee.DataSource = dt;
            grdaddedEmployee.DataBind();
            lblEmpName.Text = "HOD Code" + ": " + HfSName.Value + "(" + HfStaffcode.Value + ")";
        }
        else
        {
            btnsave.Visible = false; btnSubmit.Visible = false;
            btntest.Visible = false;
        }


        DivForm.Visible = true;
        btnback.Visible = true;
        GrdExamList.Visible = false;
    }
    private string SortDirection
    {
        get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
        set { ViewState["SortDirection"] = value; }
    }
    protected void GrdExamList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdExamList.PageIndex = e.NewPageIndex;
        this.facultylist();
    }
    protected void GrdExamList_Sorting(object sender, GridViewSortEventArgs e)
    {
        this.facultylist(e.SortExpression);
    }
}