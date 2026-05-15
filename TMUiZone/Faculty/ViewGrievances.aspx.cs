using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class Faculty_ViewGrievances : System.Web.UI.Page
{
    TMUConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
      if( ! IsPostBack )
        {
            GetGrievancesList();
          
        }
        
    }
    public void GetGrievancesList()
    {
        con = new TMUConnection();
        SqlCommand cmd = new SqlCommand("Sp_GetGrievancesListSerch", con.Con);
        
        if (rblType.SelectedValue == "STUDENT")
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Type", rblType.SelectedValue);
            cmd.Parameters.AddWithValue("@CollegeCode", txtCollege.Text.ToUpper());
            cmd.Parameters.AddWithValue("@UserGroup", Session["UserGroup"].ToString());
            cmd.Parameters.AddWithValue("@FromDate", txtFromtDate.Text);
            cmd.Parameters.AddWithValue("@ToDate", txtToDate.Text);
            cmd.Parameters.AddWithValue("@CourseCode", txtCourse.Text.ToUpper());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.DisConnect();
            grdGrievances.Visible = true;
            GrdFacultyGrievances.Visible = false;
            grdGrievances.DataSource = dt;
            grdGrievances.DataBind();
        }
        else
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Type", rblType.SelectedValue);
            cmd.Parameters.AddWithValue("@CollegeCode", txtCollege.Text.ToUpper());
            cmd.Parameters.AddWithValue("@UserGroup", Session["UserGroup"].ToString());
            cmd.Parameters.AddWithValue("@FromDate", txtFromtDate.Text);
            cmd.Parameters.AddWithValue("@ToDate", txtToDate.Text);

            cmd.Parameters.AddWithValue("@FaculyCode", txtFacultyCode.Text.ToUpper());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.DisConnect();
            GrdFacultyGrievances.Visible = true;
            grdGrievances.Visible = false;
            GrdFacultyGrievances.DataSource = dt;
            GrdFacultyGrievances.DataBind();
        }
        
    }
    protected void grdGrievances_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdGrievances.PageIndex = e.NewPageIndex;
        GetGrievancesList();
    }
    protected void grdFollowUp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdGrievances_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
             
            string ButtonText =((LinkButton)e.Row.FindControl("btnDownload")).Text;
                //((Button)grdGrievances.Rows[i].FindControl("btnDownload")).Text;
            if (ButtonText.ToUpper() != "DOWNLOAD")
            {
                LinkButton lb = (LinkButton)e.Row.FindControl("btnDownload");
                lb.Visible = false;
            }
            

        }
    }
    protected void grdGrievances_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
        
        //sandeep
        //if (e.CommandName == "updateremarks")
        //{
        //    con = new TMUConnection();
        //    SqlCommand cmd = new SqlCommand("updateremarkGrievance", con.Con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@Type", rblType.SelectedValue);
        //}
        
        if (e.CommandName == "Download")
        {            
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdGrievances.Rows[index];
            string AutoNo = grdGrievances.DataKeys[row.RowIndex].Values["SerialNo"].ToString();
            ShowAttachment( AutoNo);
            
        }
    }
    public void ShowAttachment(string AutoNo)
    {
        SqlDataReader dr = Show_AttachmentNo(AutoNo);
        DataTable dt = new DataTable();
        string Extension = ".jpg"; string FileName = ""; string PrintFileName; string attachment;
        dt.Load(dr);
        byte[] buffer = null;      //--assign null  
        if (dt.Rows.Count > 0)
        {

            buffer = (Byte[])dt.Rows[0]["Attachment"];
            // dt.Rows[0]["AttachmentType"].ToString();
            FileName = dt.Rows[0]["AttachmentName"].ToString();


            if (FileName == "") { PrintFileName = ""; }
            else { PrintFileName = FileName; }
            attachment = "attachment; filename=Attachment" + " " + PrintFileName;
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            //Response.ContentType = ""; //ReturnExtension(Extension);
            Response.ContentType = "";
            StringWriter stringWrite = new System.IO.StringWriter();
            Response.BinaryWrite(buffer);
            Response.End();
        }
        dr.Close();
        con.DisConnect();

    }
    public SqlDataReader Show_AttachmentNo(string AutoNo)
    {
        con.Connect();
        string s = "select Name as AttachmentName , Attachment from [TMU$StudentGrievances]  where SerialNo='" + AutoNo + "' ";
        SqlCommand cmd = new SqlCommand(s, con.Con);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void rblType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtCourse.Text = "";
        txtFromtDate.Text = "";
        txtToDate.Text = "";
        txtCollege.Text = "";
        txtFacultyCode.Text = "";
        if (rblType.SelectedValue.ToString() == "FACULTY")
        {
            txtFacultyCode.Visible = true;
            txtCourse.Visible = false;
        }
        else
        {
            txtFacultyCode.Visible = false;
            txtCourse.Visible = true;

        }
        GetGrievancesList();
    }
    protected void GrdFacultyGrievances_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdFacultyGrievances.PageIndex = e.NewPageIndex;
        GetGrievancesList();

    }
    protected void GrdFacultyGrievances_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GrdFacultyGrievances_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void grdGrievances_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        con = new TMUConnection();
        
       // Label id = grdGrievances.Rows[e.RowIndex].FindControl("lbl_ID") as Label;
        TextBox remarks = grdGrievances.Rows[e.RowIndex].FindControl("txtRemarks") as TextBox;
        CheckBox resolve = grdGrievances.Rows[e.RowIndex].FindControl("chkResolve") as CheckBox;
        HiddenField sno = grdGrievances.Rows[e.RowIndex].FindControl("snoHidden") as HiddenField;
        SqlCommand cmd = new SqlCommand("updateremarkGrievance", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SerialNo", sno.Value);
        cmd.Parameters.Add("@ActionRemarks", remarks.Text);
        cmd.Parameters.Add("@CloseGrievance", resolve.Checked);
        con.Con.Open();
        cmd.ExecuteNonQuery();
        con.Con.Close();
        grdGrievances.EditIndex = -1;
        GetGrievancesList();
        

    }
    protected void grdGrievances_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdGrievances.EditIndex = e.NewEditIndex;
        GetGrievancesList();  
    }
    protected void grdGrievances_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdGrievances.EditIndex = -1;
        GetGrievancesList();
    }
    protected void GrdFacultyGrievances_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GrdFacultyGrievances.EditIndex = e.NewEditIndex;
        GetGrievancesList();  
    }
    protected void GrdFacultyGrievances_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdFacultyGrievances.EditIndex = -1;
        GetGrievancesList();
    }
    protected void GrdFacultyGrievances_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        con = new TMUConnection();

        // Label id = grdGrievances.Rows[e.RowIndex].FindControl("lbl_ID") as Label;
        TextBox remarks = GrdFacultyGrievances.Rows[e.RowIndex].FindControl("txtRemarks") as TextBox;
        CheckBox resolve = GrdFacultyGrievances.Rows[e.RowIndex].FindControl("chkResolve") as CheckBox;
        HiddenField sno = GrdFacultyGrievances.Rows[e.RowIndex].FindControl("snoHidden") as HiddenField;
        SqlCommand cmd = new SqlCommand("updateremarkGrievance", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SerialNo", sno.Value);
        cmd.Parameters.Add("@ActionRemarks", remarks.Text);
        cmd.Parameters.Add("@CloseGrievance", resolve.Checked);
        con.Con.Open();
        cmd.ExecuteNonQuery();
        con.Con.Close();
        GrdFacultyGrievances.EditIndex = -1;
        GetGrievancesList();
    }
    protected void BtnShow_Click(object sender, EventArgs e)
    {
        GetGrievancesList();
    }
}