using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Faculty_FacultyGrievances : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["uid"].ToString() != "" && Session["UserGroup"].ToString()!="STUDENT"  )
            {
                BindFacultyDetails();
            }
            else
            {
                Response.Redirect("../Default.aspx");
            }
        }
        catch 
            {
                Response.Redirect("../Default.aspx");
            } 
       
       // bindListGrievable();
    }
   
    //public void bindListGrievable()
    //{
    //     SqlCommand cmd = new SqlCommand("select id,[Grievable Matters] from GrievableMatters where [Type]='FACULTY'", con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    lstGriv.DataSource = dt;
    //    lstGriv.DataTextField = "Grievable Matters";
    //    lstGriv.DataValueField = "id";
    //    lstGriv.DataBind();
    //}
    public void BindFacultyDetails()
    {
        SqlCommand cmd = new SqlCommand("proc_fetchFacultyDetailsForGrievance", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {            
            txtEmpCode.Text = dt.Rows[0]["No_"].ToString();
            txtName.Text = dt.Rows[0]["Search Name"].ToString();
            txtDesignation.Text = dt.Rows[0]["Job Title_Grade Desc"].ToString();           
            txtCollegeCode.Text = dt.Rows[0]["Global Dimension 1 Code"].ToString();
            txtCollegeName.Text = dt.Rows[0]["Branch Name"].ToString();
            txtDOB.Text = dt.Rows[0]["Birth Date"].ToString();
            txtDOJ.Text = dt.Rows[0]["Employment Date"].ToString(); 
            txtContactNo.Text = dt.Rows[0]["Mobile Phone No_"].ToString();
            txtEmail.Text = dt.Rows[0]["E-Mail"].ToString();
        }
    }
    public void Clear()
    {
        txtGrievableMatters.Text = "";
        txtOther.Text = "";
        txtDateOfEvent.Text = "";
        txtFacultySpecificDetails.Text="";
        txtSpecificRemedy.Text="";
        chkOther.Checked = false;
        lstGriv.SelectedIndex = -1;

    }
    protected void btnGrievableMatters_Click(object sender, EventArgs e)
    {
        string message = "";
        foreach (ListItem item in lstGriv.Items)
        {
            if (item.Selected)
            {
               
               // message += item.Text + " " +"\n";
                message += item.Text + " " + " \\ ";
            }
        }
        txtGrievableMatters.Text = message;
        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('" + message + "');", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        byte[] bytes = null;

        try
        {
            //if (chkboxAttechment.Checked == true)
            //{
            //    if (UploadAttecment.HasFile)
            //    {

            //        string filename = Path.GetFileName(UploadAttecment.PostedFile.FileName);
            //        string contentType = UploadAttecment.PostedFile.ContentType;
            //        using (Stream fs = UploadAttecment.PostedFile.InputStream)
            //        {
            //            using (BinaryReader br = new BinaryReader(fs))
            //            {
            //                bytes = br.ReadBytes((Int32)fs.Length);
            //            }
            //        }
            //    }
            //}
            int co=0;
            if (chkOther.Checked == true) { co = 1; }

            SqlCommand cmd = new SqlCommand("proc_insertIntoFacultyGrievance", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Code", txtEmpCode.Text);
            cmd.Parameters.Add("@Name", txtName.Text);
            cmd.Parameters.Add("@Appeal1",txtFacultySpecificDetails.Text);
            cmd.Parameters.Add("@Appeal2", txtSpecificRemedy.Text);
            cmd.Parameters.Add("@Appeal3",txtGrievableMatters.Text);            
            cmd.Parameters.Add("@Other", co);
            cmd.Parameters.Add("@OtherMatters", txtOther.Text);
            cmd.Parameters.Add("@Attachment", bytes);
            cmd.Parameters.Add("@GrievancesDate",txtDateOfEvent.Text);
            cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString()); 
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Clear();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Save Successfully');", true);
            }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', '" + ex + "');", true);
            //Response.Redirect("~/Default.aspx");
        }
    }
    protected void chkOther_CheckedChanged(object sender, EventArgs e)
    {
        if (chkOther.Checked == true)
        {
            txtOther.Enabled = true;
            rfvOther.Enabled = true;
        }
        else
        {
            txtOther.Enabled = false;
            rfvOther.Enabled = false;
        }
    }
}