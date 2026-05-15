using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_ComplaintForms : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalComplaint"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                bindAsset();
                txtComplaintDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                getComplaintlist();
            }
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }
    }

    private void bindAsset()
    {
        try
        {
            SqlCommand cmd = new SqlCommand(" SELECT [Asset No_] 'No_' , concat(FA.Description,'_',FA.[Serial No_],'_',FA.[Old Asset Code]) [Details]  FROM [TMU Hospital$Alloted Person] AP inner join [TMU Hospital$Fixed Asset] FA on FA.[No_]=AP.[Asset No_] where [Issue ID]='" + Session["uid"].ToString() + "'", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            con.Open();
            da.Fill(dt1);
            con.Close();
            ddlEquipmentNo.DataSource = dt1;
            ddlEquipmentNo.DataTextField = "Details";
            ddlEquipmentNo.DataValueField = "No_";
            ddlEquipmentNo.DataBind();
            ddlEquipmentNo.Items.Insert(0, new ListItem("-- Select Asset --", ""));
        }
        catch
        {

        }
    }
    protected void btnClose_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "ManualPresent")
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            // Read values from GridView row
            string fingerNo = ((Label)row.FindControl("lblComplaintNo")).Text;

            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("Update [TMU Hospital$Job Order Header] set [Status]=2,[Closing Date]='"+ DateTime.Now + "'  where [Complaint No_]='" + fingerNo + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Complaint Close Successfully');", true);
            getComplaintlist();

        }
    }
    protected void btn_Reset_Click(object sender, EventArgs e)
    {
        Response.Redirect("ComplaintForms.aspx");
    }
    private void getComplaintlist()
    {
        try
        {
            using (SqlCommand cmd = new SqlCommand(@"
        SELECT 
        
        [Complaint No_] AS ComplaintNo,
        [Complaint Date] AS ComplaintDate,
        [Complaint Description] AS ComplaintDescription,
        [Equipment  No_] AS EquipmentNo,Make,
Model,
case when [Complaint Status] =0 then 'Open' when [Complaint Status] =1 then 'In Progress' else 'Close' end as 'BMStatus',
[Serial No_] 'SerialNo',
[Employee Code] EmployeeCode,
[Employee Name] EmployeeName

        FROM [TMU Hospital$Job Order Header] with(NOLOCK)
        WHERE [User Id] = @UserId and [Status]!=2", con))
            {
                cmd.Parameters.Add("@UserId", SqlDbType.NVarChar).Value = Session["uid"].ToString();

                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                da.Fill(dt1);

                grdComplaintall.DataSource = dt1;
                grdComplaintall.DataBind();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        string Code = "";
        if (ddlEquipmentNo.SelectedValue == "")
        {
            string message1 = "Please select Equipment No.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (txtComplaintDescription.Text == "")
        {
            string message1 = "Please Fill Complaint Description.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        DateTime complaintDate = DateTime.Parse(txtComplaintDate.Text);
        SqlCommand cmd = new SqlCommand("SP_Insert_TMU_JobOrderHeader", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@ComplaintNo", 0);
        cmd.Parameters.AddWithValue("@ComplaintDate", complaintDate);
        cmd.Parameters.AddWithValue("@EquipmentNo", ddlEquipmentNo.SelectedValue);
        cmd.Parameters.AddWithValue("@ComplaintDescription", txtComplaintDescription.Text);
        cmd.Parameters.AddWithValue("@UserName", txtEmployeeName.Text);

        cmd.Parameters.Add("@OrderNo", SqlDbType.VarChar, 100);
        cmd.Parameters["@OrderNo"].Direction = ParameterDirection.Output;
        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();

        Code = cmd.Parameters["@OrderNo"].Value.ToString();


        con.Close();


       
          
        ScriptManager.RegisterStartupScript(
                   this,
                   this.GetType(),
                   "Key",
                   "alert('Your Complaint "+ Code + " have been generate successfully.'); window.location='ComplaintForms.aspx';",
                   true
               );
    }

  
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    private void BindGrid()
    {
        try
        {
            using (SqlCommand cmd = new SqlCommand(@"
        SELECT 
        ROW_NUMBER() OVER(ORDER BY [Complaint Date] DESC) AS ID,
        [Complaint No_] AS ComplaintNo,
        [Complaint Date] AS ComplaintDate,
        [Complaint Description] AS ComplaintDescription,
        [Equipment  No_] AS EquipmentNo
        FROM [TMU Hospital$Job Order Header] with(NOLOCK)
        WHERE [User Id] = @UserId", con))
            {
                cmd.Parameters.Add("@UserId", SqlDbType.NVarChar).Value = Session["uid"].ToString();

                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                da.Fill(dt1);

                grdComplaintall.DataSource = dt1;
                grdComplaintall.DataBind();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void btn_ExportToexel_Click(object sender, EventArgs e)
    {
        //Disable paging before binding
        grdComplaintall.AllowPaging = false;

        BindGrid();  // fill grid again if needed

        Response.ClearContent();
        Response.Buffer = true;

        string filename = "Complaint_" + Session["uid"].ToString() + ".xls";

        Response.AddHeader("content-disposition", "attachment; filename=" + filename);
        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";

        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        grdComplaintall.GridLines = GridLines.Both;
        grdComplaintall.HeaderStyle.Font.Bold = true;
        grdComplaintall.RenderControl(htw);

        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();

        //Enable paging again
        grdComplaintall.AllowPaging = true;
        BindGrid();
    }




    protected void ddlEquipmentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlCommand cmd = new SqlCommand(" SELECT  Brand,Model,[Serial No_],AP.[Department Name],FA.Description,FA.[Old Asset Code] 'Equipment Tag ID',FA.Description FROM  [TMU Hospital$Fixed Asset] FA with(NOLOCK)  inner join [TMU Hospital$Alloted Person] AP with(NOLOCK) on FA.No_=AP.[Asset No_] where [No_]='" + ddlEquipmentNo.SelectedValue+"'", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            con.Open();
            da.Fill(dt1);
            con.Close();
            if(dt1.Rows.Count>0)
            {
                txtMake.Text = dt1.Rows[0]["Brand"].ToString();
                txtSerialNo.Text = dt1.Rows[0]["Serial No_"].ToString();
                txtModel.Text = dt1.Rows[0]["Model"].ToString();
                txtEmployeeCode.Text = Session["uid"].ToString();
                txtEmployeeName.Text = Session["uname"].ToString();
                txtTagID.Text= dt1.Rows[0]["Equipment Tag ID"].ToString();
                txtDepartment.Text = dt1.Rows[0]["Department Name"].ToString();
                txtEqname.Text = dt1.Rows[0]["Description"].ToString();
            }
            else
            {
                txtMake.Text = "";
                txtSerialNo.Text = "";
                txtModel.Text = "";
                txtEmployeeCode.Text ="";
                txtEmployeeName.Text = "";
                txtTagID.Text = "";
                txtDepartment.Text = "";
                txtEqname.Text = "";
            }
        }
        catch
        {

        }
    }



   
}