using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;


public partial class Faculty_Super_checkList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {

                txtsupervisorname.Text = Session["Fulname"].ToString();
            } 
            catch
            {
                Response.Redirect("../Default.aspx");
            }

        }

}
   
   

    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        int RowCount = tblData.Rows.Count;
        for (int i = 1; i < tblData.Rows.Count; i++)
        {
            Label Question = (Label)tblData.Rows[i].Cells[1].FindControl("LabelR" + i.ToString());

            DropDownList Answer = (DropDownList)tblData.Rows[i].Cells[1].FindControl("DropDownList" + i.ToString());
            TextBox txtRemark = (TextBox)tblData.Rows[i].Cells[1].FindControl("TextBoxR" + i.ToString());
            SqlCommand cmd = new SqlCommand("sp_Insertsuperdetail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SupervisorName", txtsupervisorname.Text);
            cmd.Parameters.AddWithValue("@PatientDetail", txtpatientdetail.Text);
            cmd.Parameters.AddWithValue("@ReportDate", txtreportdate.Text);
            cmd.Parameters.AddWithValue("@HelperDetail", txthelperdetail.Text);
            cmd.Parameters.AddWithValue("@WardName", txtwardname.Text);
            cmd.Parameters.AddWithValue("@Question", Question.Text);
            cmd.Parameters.AddWithValue("@Status", Answer.Text);
            cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);
            cmd.Parameters.AddWithValue("@WardRoom", txtwardroomno.Text);
            cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
           }
        string message = "Your details have been saved successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        
        txtpatientdetail.Text = "";
        txtreportdate.Text = "";
        txthelperdetail.Text = "";
        txtwardname.Text = "";
        txtwardroomno.Text ="";
        
    }
     
}

