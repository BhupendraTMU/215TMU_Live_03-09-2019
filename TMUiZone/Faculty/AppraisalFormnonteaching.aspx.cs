using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

public partial class Faculty_AppraisalFormnonteaching : System.Web.UI.Page
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
                Form();
                Checkpms();
                BindEmployee();
            }
            catch
            { }
        }
    }

    public void BindEmployee()
    {
        SqlCommand cmd = new SqlCommand("proc_GetEmployeeList", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EmpCode", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@AcademicYear", lblAcad.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        con1.Open();
        da.Fill(dt1);
        con1.Close();
        ddlEmployeeList.DataSource = dt1;
        ddlEmployeeList.DataValueField = "No_";
        ddlEmployeeList.DataTextField = "Details";
        ddlEmployeeList.DataBind();
        ddlEmployeeList.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));
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

    public void Form()
    {
        SqlCommand cmd = new SqlCommand("proc_GetNonTechPMApplied", con);
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
            grdaddedEmployee.DataSource = dt;
            grdaddedEmployee.DataBind();
            btnSubmit.Visible = true;
            btntest.Visible = true;
        }
        else
        {
             btnSubmit.Visible = false;
            btntest.Visible = false;
        }
    }

    public void Checkpms()
    {
        SqlCommand cmd = new SqlCommand("proc_GetNonPMSubmit", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EmpCode", Session["uid"].ToString());
	    cmd.Parameters.AddWithValue("@AcademicYear",lblAcad.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        con.Open();
        da.Fill(dt1);
        con.Close();
        if (dt1.Rows.Count > 0)
        {
            EnableF();
            btnsave.Visible = false;
            btnSubmit.Visible = false;
            btntest.Visible = true;
        }
        else
        {
            EnableT();
            btnsave.Visible = true;
            btnSubmit.Visible = true;
            btntest.Visible = false;
        }

    }

    public void EnableT()
    {

        txtEmpName.Enabled=true;
        txtDatJob.Enabled=true;
        txtDesignation.Enabled=true;
                txt15.Enabled=true;
                txt16.Enabled=true;
                txt17.Enabled=true;
                txt18.Enabled=true;
                txt19.Enabled=true;


    }
    public void EnableF()
    {

        txtEmpName.Enabled = false;
        txtDatJob.Enabled = false;
        txtDesignation.Enabled = false;
        txt15.Enabled = false;
        txt16.Enabled = false;
        txt17.Enabled = false;
        txt18.Enabled = false;
        txt19.Enabled = false;

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (ddlEmployeeList.SelectedIndex ==0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Employee First')", true);
            return;
        }
        decimal A;//Convert.ToDecimal(txtct1.Text = string.IsNullOrEmpty(txtct1.Text) ? "0" : txtct1.Text)
        A = (Convert.ToDecimal(txt15.Text = string.IsNullOrEmpty(txt15.Text) ? "0" : txt15.Text)
        + Convert.ToDecimal(txt16.Text = string.IsNullOrEmpty(txt16.Text) ? "0" : txt16.Text)
        + Convert.ToDecimal(txt17.Text = string.IsNullOrEmpty(txt17.Text) ? "0" : txt17.Text)
        + Convert.ToDecimal(txt18.Text = string.IsNullOrEmpty(txt18.Text) ? "0" : txt18.Text)
        + Convert.ToDecimal(txt19.Text = string.IsNullOrEmpty(txt19.Text) ? "0" : txt19.Text)

        );
        txtT1.Text = Convert.ToString(A);

         SqlCommand cmd1 = new SqlCommand("proc_GetNonTechPM", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.AddWithValue("@EmpCode", ddlEmployeeList.SelectedValue  );
	    cmd1.Parameters.AddWithValue("@AcademicYear",lblAcad.Text);
	
        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        con.Open();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            update();
            Form();
            clear();
        }
        else
        {

            con.Open();
            SqlCommand cmd = new SqlCommand("PmsInsertNonTech", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmpCode", ddlEmployeeList.SelectedValue);
            cmd.Parameters.AddWithValue("@EmpName", txtEmpName.Text);
            cmd.Parameters.AddWithValue("@DatJob", txtDatJob.Text);
            cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text);
            cmd.Parameters.AddWithValue("@AcademicYear", lblAcad.Text);
            cmd.Parameters.AddWithValue("@txt15", txt15.Text);
            cmd.Parameters.AddWithValue("@txt16", txt16.Text);
            cmd.Parameters.AddWithValue("@txt17", txt17.Text);
            cmd.Parameters.AddWithValue("@txt18", txt18.Text);
            cmd.Parameters.AddWithValue("@txt19", txt19.Text);
            cmd.Parameters.AddWithValue("@txtT1", txtT1.Text);
            cmd.Parameters.AddWithValue("@Status", "1");
            cmd.Parameters.AddWithValue("@HOD", Session["uid"].ToString());
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data saved successfully')", true);
            Form();
            clear();
            BindEmployee();

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("SubmitApplied", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EmpCode", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@AcademicYear", lblAcad.Text);
        cmd.Parameters.AddWithValue("@Status", "3");
        cmd.ExecuteNonQuery();
        con.Close();
        Form();
        clear();
        BindEmployee();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data submitted successfully')", true);

    }

    public void update()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("PmsupdateNonTech", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EmpCode", ddlEmployeeList.SelectedValue);
        cmd.Parameters.AddWithValue("@EmpName", txtEmpName.Text);
        cmd.Parameters.AddWithValue("@DatJob", txtDatJob.Text);
        cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text);
        cmd.Parameters.AddWithValue("@AcademicYear", lblAcad.Text);
        cmd.Parameters.AddWithValue("@txt15", txt15.Text);
        cmd.Parameters.AddWithValue("@txt16", txt16.Text);
        cmd.Parameters.AddWithValue("@txt17", txt17.Text);
        cmd.Parameters.AddWithValue("@txt18", txt18.Text);
        cmd.Parameters.AddWithValue("@txt19", txt19.Text);
        cmd.Parameters.AddWithValue("@txtT1", txtT1.Text);
        cmd.Parameters.AddWithValue("@Status", "1");
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data saved successfully')", true);
    }
    protected void ddlEmployeeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("[proc_GetEmployeeListDOB]", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EmpCode", ddlEmployeeList.SelectedValue);
        cmd.Parameters.AddWithValue("@AcademicYear", lblAcad.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        con1.Open();
        da.Fill(dt1);
        con1.Close();
        if (dt1.Rows.Count > 0)
        {
            txtEmpName.Text = dt1.Rows[0]["Details"].ToString();
            txtDatJob.Text = dt1.Rows[0]["Employment Date"].ToString();
            txtDesignation.Text = dt1.Rows[0]["Designation Code"].ToString();
            txtT1.Text = "";
        }
        else
        {
            txtEmpName.Text = "";
            txtDatJob.Text = "";
            txtDesignation.Text = "";
            txtT1.Text = "";
        }
    }
    protected void clear()
    {
        ddlEmployeeList.SelectedIndex = -1;
        txtEmpName.Text = "";
        txtDatJob.Text = "";
        txtDesignation.Text = "";
        txt15.Text = "";
        txt16.Text = "";
        txt17.Text = "";
        txt18.Text = "";
        txt19.Text = "";
        txtT1.Text = "";
    }
}