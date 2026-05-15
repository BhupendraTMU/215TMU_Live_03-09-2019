using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Web.Services;
using System.Web.Script.Services;
using Utility;

public partial class Faculty_IndentRequest : System.Web.UI.Page
{
   SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    DL.FacultyPortalDL FDL = new DL.FacultyPortalDL();
    DataTable dt1 = new DataTable();
    DataRow row;
    static int p = 0;
    string NextNo = "";
    
    Connection con;  ServicePoratal Portalcon;
    DataTable dtAddItem = new DataTable(); SqlTransaction SqlHeaderTrn, SqlLineTrn; 

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {  
              //  hfDocumentNo.Value = ""; p = 0;   
                BindAppliedIndent();
            }
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    public void bindDdlDepartment()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_fetchFacultyDetailsForGrievance", con1);        
        cmd.CommandType = CommandType.StoredProcedure;        
        cmd.Parameters.Add("@UserID", Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        ddlDepartment.DataSource = dt;
        ddlDepartment.DataTextField = "Details";
        ddlDepartment.DataValueField = "No_";
        ddlDepartment.DataBind();
    }
    protected void txtItemToPurchase_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable(); String ItemCode = "";
        if (txtItemToPurchase.Text.Contains('(') == true && txtItemToPurchase.Text.Contains(')') == true)
        {
            ItemCode = txtItemToPurchase.Text.Split('(', ')')[1].Trim();
            btnSendForApproval.Enabled = true;
        }
        else
        {
            btnSendForApproval.Enabled = false;
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Item Not Found !');", true);
            txtItemToPurchase.Text = "";
            return;
        }

        if (ItemCode != "")
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "Sp_GetVarianceUOMByItemCode";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ItemCode", ItemCode.Trim());
                    cmd.Connection = conn;
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    dt.Load(sdr);
                    dt.AcceptChanges();
                    conn.Close();
                }
            }            
            if (dt.Rows.Count > 0)
            {
                lblUOM.Text = dt.Rows[0]["Base Unit of Measure"].ToString();
                ddlVariance.DataSource = dt;
                ddlVariance.DataValueField = "Code";
                ddlVariance.DataTextField = "Description";
                ddlVariance.DataBind();               
            }
        }
        else
        {
            txtItemToPurchase.Text = "";
            lblUOM.Text = "";
            ddlVariance.DataSource = null;
            ddlVariance.DataBind();

        }
    }
    protected void btnADD_Click(object sender, EventArgs e)
    {
        lblRequestError.Text = ""; string ItemCode = ""; String ItemDescription = "";
        try
        {
            Convert.ToDecimal(txtQty.Text);
        }
        catch
        {
            lblRequestError.Text = "Please Enter Correct Quantity";
            return;
        }

        if (txtItemToPurchase.Text.Contains('(') == true && txtItemToPurchase.Text.Contains(')') == true)
        {
            ItemDescription = txtItemToPurchase.Text.Split('(', ')')[0].Trim();
            ItemCode = txtItemToPurchase.Text.Split('(', ')')[1].Trim();
            btnADD.Enabled = true;
        }
        else
        {
            btnADD.Enabled = false;
        }
        if (ddlVariance.SelectedItem.Text == "-- Select --")
        {
            lblRequestError.Text = "please Select Variance.";
            return;
        }
        else
        {
            if (grdAddItem.Rows.Count > 0)
            {
                dtAddItem = (DataTable)ViewState["AddItem"];
                DataView dv = new DataView(dtAddItem);
                dv.RowFilter = "ItemCode='" + ItemCode + "' and Variance='" + ddlVariance.SelectedItem.Text + "'";
                if (dv.Count > 0)
                {
                    lblRequestError.Text = "Item already Added.";
                }
                else
                {
                    dtAddItem.Rows.Add(ItemCode, ItemDescription, ddlVariance.SelectedValue, ddlVariance.SelectedItem.Text, txtQty.Text, lblUOM.Text, txtRemarks.Text);
                    grdAddItem.DataSource = dtAddItem;
                    grdAddItem.DataBind();
                    blankTable();
                }
            }
            else
            {
                dtAddItem.Columns.AddRange(new DataColumn[7]{
                    new DataColumn ("ItemCode",typeof(String)),                    
                    new DataColumn ("ItemDescription",typeof(string)),
                    new DataColumn ("VarianceCode",typeof(string)),
                    new DataColumn ("Variance",typeof(string)),
                    new DataColumn ("Qty",typeof(decimal)),
                    new DataColumn ("UOM",typeof(string))   ,
                     new DataColumn ("Remarks",typeof(string))                    
                    });
                dtAddItem.Rows.Add(ItemCode, ItemDescription, ddlVariance.SelectedValue, ddlVariance.SelectedItem.Text, txtQty.Text, lblUOM.Text, txtRemarks.Text);
                grdAddItem.DataSource = dtAddItem;
                grdAddItem.DataBind();
                ViewState["AddItem"] = dtAddItem;
                rblDepartment.Enabled = false;
                blankTable();
            }


        }
        if (grdAddItem.Rows.Count > 0)
            btnSendForApproval.Visible = true;
        else btnSendForApproval.Visible = false;
    }
    protected void grdAddItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DeleteItem"))
        {
            GridViewRow rowItem = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            int index = rowItem.RowIndex;
            DataTable dt = ViewState["AddItem"] as DataTable;
            dt.Rows[index].Delete();
            grdAddItem.DataSource = dt;
            grdAddItem.DataBind();
            ViewState["AddItem"] = dt;
            blankTable();

        }
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchItems(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            DataTable dt = new DataTable();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "Sp_SerchItem";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ItemSearch", prefixText.ToUpper());
                cmd.Connection = conn;
                conn.Open();
                List<string> item = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        item.Add(sdr["Description"].ToString());
                    }
                }
                conn.Close();
                return item;
            }
        }
    }
   
    public void blankTable()
    {
        txtItemToPurchase.Text = "";
        ddlVariance.SelectedIndex = -1;
        txtQty.Text = "";
        txtRemarks.Text = "";
        if (grdAddItem.Rows.Count > 0) { btnSendForApproval.Visible = true; rblDepartment.Enabled = false; }
        else { btnSendForApproval.Visible = false; rblDepartment.Enabled = true; }
    }
       
    public void BindAppliedIndent()
    {
        SqlCommand cmd = new SqlCommand("sp_GetItemIndentList", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@IndentFor","");
        cmd.Parameters.Add("@UserID", Session["uid"].ToString());
        cmd.Parameters.Add("@Status",1 );        
        SqlDataAdapter da=new SqlDataAdapter (cmd);
        DataTable dt=new DataTable ();
        da.Fill(dt);
        grdAppliedIndent.DataSource = dt;
        grdAppliedIndent.DataBind();
    }
    public void BindAppliedCoursePlan1()
    {
        SqlCommand cmd = new SqlCommand("proc_GetAppliedCoursePlanFacultyCourseSubjectwise", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@AcademicYear", "");
        cmd.Parameters.Add("@CourseCode", "");
        cmd.Parameters.Add("@SubjectCode", "");
        cmd.Parameters.Add("@SemesterCode", "");
        cmd.Parameters.Add("@Section", "");
        cmd.Parameters.Add("@Group", "");
        cmd.Parameters.Add("@Batch", "");

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
       // grdAppliedCoursePlan.DataSource = dt;
       // grdAppliedCoursePlan.DataBind();
    }
    public void HeaderEnable(Boolean TF)
    {
        
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        
        

    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Response.Redirect("FacultyCoursePlan.aspx");
    }
   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        
    }
    protected void btnSendForApproval_Click(object sender, EventArgs e)
    {
        Boolean SaveHeader = false; Boolean SaveLine = false; string SequenceNo = "";
        SequenceNo = "";
        if (grdAddItem.Rows.Count < 1)
        {

        }

        else
        {           
            con = new Connection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con.Con;
            con.Con.Open();
            SqlHeaderTrn = con.Con.BeginTransaction();            
            cmd.Transaction = SqlHeaderTrn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_SaveItemIndentRequestHeader";
            cmd.CommandTimeout = 999000000;
            int Status = 0;
            try
            {
                if (Session["HODLoginPage"].ToString() == Session["uid"].ToString()) { Status = 2; } else { Status = 1; }
                cmd.Parameters.AddWithValue("@IndentFor", rblDepartment.SelectedValue);
                cmd.Parameters.AddWithValue("@UserID", Session["uid"]);
                cmd.Parameters.AddWithValue("@Status", Status);

                object Result = new object();
                Result = cmd.ExecuteScalar();
                SequenceNo = Result.ToString();
                cmd.Parameters.Clear();
                // cmd.ExecuteReader();
                if (Result != "")
                {
                    SaveHeader = true;
                }
                
                // con.Con.Close();
            }
            catch
            {
                SqlHeaderTrn.Rollback();
                return;
            }
            
            cmd.CommandText = "sp_SaveItemIndentRequestLine";
            DataTable dt = (DataTable)ViewState["AddItem"];
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmd.Parameters.AddWithValue("@DocumentNo", SequenceNo);
                    cmd.Parameters.AddWithValue("@Departmentcode", Session["Departmentcode"]);
                    cmd.Parameters.AddWithValue("@ItemCode", dt.Rows[i]["ItemCode"]);
                    cmd.Parameters.AddWithValue("@ItemDescription", dt.Rows[i]["ItemDescription"]);
                    cmd.Parameters.AddWithValue("@IndentFor", Convert.ToInt32(rblDepartment.SelectedValue));
                    cmd.Parameters.AddWithValue("@VariantCode", dt.Rows[i]["ItemCode"]);
                    cmd.Parameters.AddWithValue("@UOM", dt.Rows[i]["ItemCode"]);
                    cmd.Parameters.AddWithValue("@Qty", Convert.ToDecimal(dt.Rows[i]["Qty"]));
                    cmd.Parameters.AddWithValue("@Remarks", dt.Rows[i]["Remarks"]);
                    cmd.Parameters.AddWithValue("@UserID", Session["uid"]);
                    cmd.ExecuteScalar();
                    cmd.Parameters.Clear();

                }
                SaveLine = true;
            }
            catch
            {
                SqlHeaderTrn.Rollback();
                return;
            }
            if (SaveHeader == true && SaveLine == true)
            {
                SqlHeaderTrn.Commit();
               // lblRequestError.Text = "Request save with document No :" + SequenceNo + "";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Request Send Successfully');", true);
                ViewState["AddItem"] = null;                
                grdAddItem.DataSource=null ;
                grdAddItem.DataBind();
                btnSendForApproval.Visible = false;
            }

            

        }
        BindAppliedIndent();
    }
     
    protected void grdAppliedCoursePlan_SelectedIndexChanged(object sender, EventArgs e)
    {        
        

    }


    protected void grdAppliedIndent_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {        
        grdAppliedIndent.PageIndex = e.NewPageIndex;
        BindAppliedIndent();
    }
}