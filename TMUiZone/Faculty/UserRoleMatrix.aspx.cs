using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Faculty_UserRoleMatrix : System.Web.UI.Page
{
    ServicePoratal con1;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if ((this.Master as IndexMaster).GetLinkYesNo("UserRoleMatrix") == "True")
                {
                    BindCourse();
                    BindData();
                }
                else
                { Response.Redirect("~/Default.aspx"); }
            } 
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Default.aspx");
        }
        
    }
    public void BindCourse()
    {
        SqlCommand cmd = new SqlCommand("SP_GetCourseNotInUserRoleMatrix", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlCourse.DataSource = dt;
        ddlCourse.DataTextField = "Description";
        ddlCourse.DataValueField = "Code";
        ddlCourse.DataBind();
    }
    public void BindData()
    {
        SqlCommand cmd = new SqlCommand("SP_GetDataUserRoleMatrix", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@FcultyCode", Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);  
        grdUserRoleMatrix.DataSource = dt;
        grdUserRoleMatrix.DataBind();

    }


    protected void grdUserRoleMatrix_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdUserRoleMatrix.EditIndex = e.NewEditIndex;
        BindData();

    }
    protected void grdUserRoleMatrix_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = grdUserRoleMatrix.Rows[e.RowIndex];
        int id = Convert.ToInt32(grdUserRoleMatrix.DataKeys[e.RowIndex].Value);        
        TextBox txtEditCourseCode = (TextBox)grdUserRoleMatrix.Rows[e.RowIndex].FindControl("txtEditCourseCode");
        TextBox txtEditPrincipal =  (TextBox)grdUserRoleMatrix.Rows[e.RowIndex].FindControl("txtEditPrincipal");
        TextBox txtEditHOD = (TextBox)grdUserRoleMatrix.Rows[e.RowIndex].FindControl("txtEditHOD");
        TextBox txtEditCourseCoOrdinator = (TextBox)grdUserRoleMatrix.Rows[e.RowIndex].FindControl("txtEditCourseCoOrdinator");
        TextBox txtEditProctor = (TextBox)grdUserRoleMatrix.Rows[e.RowIndex].FindControl("txtEditProctor");
        TextBox txtEditLabIncharge = (TextBox)grdUserRoleMatrix.Rows[e.RowIndex].FindControl("txtEditLabIncharge");
        TextBox txtEditEventCoOrdinator = (TextBox)grdUserRoleMatrix.Rows[e.RowIndex].FindControl("txtEditEventCoOrdinator");
        TextBox txtEditTransport = (TextBox)grdUserRoleMatrix.Rows[e.RowIndex].FindControl("txtEditTransportApproval");
        TextBox txtEditmanagement = (TextBox)grdUserRoleMatrix.Rows[e.RowIndex].FindControl("txtEditManagementapproval");
        TextBox txtEditCo = (TextBox)grdUserRoleMatrix.Rows[e.RowIndex].FindControl("txtEditExamapproval");

        SqlCommand cmd = new SqlCommand("SP_UpdateUserRoleMatrix", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@id", id);
        cmd.Parameters.Add("@Principal", txtEditPrincipal.Text);
        cmd.Parameters.Add("@HOD", txtEditHOD.Text);
        cmd.Parameters.Add("@CourseCoOrdinator", txtEditCourseCoOrdinator.Text);
        cmd.Parameters.Add("@EventCoOrdinator", txtEditEventCoOrdinator.Text);
        cmd.Parameters.Add("@Proctor", txtEditProctor.Text);
        cmd.Parameters.Add("@LabIncharge", txtEditLabIncharge.Text);
        cmd.Parameters.Add("@UserCode", Session["uid"].ToString());
        cmd.Parameters.Add("@Transport", txtEditTransport.Text);
        cmd.Parameters.Add("@Management", txtEditmanagement.Text);
        cmd.Parameters.Add("@ExamCoOrdinator", txtEditCo.Text);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        grdUserRoleMatrix.EditIndex = -1;
        BindData();
    }
    protected void grdUserRoleMatrix_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdUserRoleMatrix.EditIndex = -1;
        BindData();
    }
    protected void grdUserRoleMatrix_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

       // GridViewRow row = grdUserRoleMatrix.Rows[e.RowIndex];
         
        int id = Convert.ToInt32(grdUserRoleMatrix.DataKeys[e.RowIndex].Value);
        SqlCommand cmd = new SqlCommand("SP_DeleteDataUserRoleMatrix", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@id", id);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        BindData();
    }



    protected void btnAdd_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("SP_InsertCourseNotInUserRoleMatrix", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@CourseCode", ddlCourse.SelectedValue);
        cmd.Parameters.Add("@UserCode", Session["uid"].ToString());
        con.Open();
       cmd.ExecuteNonQuery();
        con.Close();
        BindData();
        BindCourse();
    }

    
    protected void grdUserRoleMatrix_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("ADD"))
        {
            DropDownList ddlFootercourse = (DropDownList)grdUserRoleMatrix.FooterRow.FindControl("ddlAddCourse"); 
            TextBox txtAddPrincipal = (TextBox)grdUserRoleMatrix.FooterRow.FindControl("txtAddPrincipal");
            TextBox txtAddHOD = (TextBox)grdUserRoleMatrix.FooterRow.FindControl("txtAddHOD");
            TextBox txtAddCourseCoOrdinator = (TextBox)grdUserRoleMatrix.FooterRow.FindControl("txtAddCourseCoOrdinator");
            TextBox txtAddProctor = (TextBox)grdUserRoleMatrix.FooterRow.FindControl("txtAddProctor");
            TextBox txtAddLabIncharge = (TextBox)grdUserRoleMatrix.FooterRow.FindControl("txtAddLabIncharge");
            TextBox txtAddEventCoOrdinator = (TextBox)grdUserRoleMatrix.FooterRow.FindControl("txtAddEventCoOrdinator");
            TextBox txtAddCountry = (TextBox)grdUserRoleMatrix.FooterRow.FindControl("txtAddCountry");
            TextBox txtExamapproval = (TextBox)grdUserRoleMatrix.FooterRow.FindControl("txtExamapproval");
            //string country = (GridView1.FooterRow.FindControl("ddlCountries") as DropDownList).SelectedItem.Value;

            if (ddlFootercourse.SelectedValue == "" || txtAddPrincipal.Text == "")
            {
                string myStringVariable = "Course Code OR Principal Could Not Be Blank ";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);
                return;
            }


            SqlCommand cmd = new SqlCommand("SP_InsertDataNotInUserRoleMatrix", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            cmd.Parameters.Add("@CourseCode", ddlFootercourse.SelectedValue);            
            cmd.Parameters.Add("@Principal", txtAddPrincipal.Text);
            cmd.Parameters.Add("@HOD", txtAddHOD.Text);
            cmd.Parameters.Add("@CourseCoOrdinator", txtAddCourseCoOrdinator.Text);
            cmd.Parameters.Add("@EventCoOrdinator", txtAddEventCoOrdinator.Text);
            cmd.Parameters.Add("@Proctor", txtAddProctor.Text);
            cmd.Parameters.Add("@LabIncharge", txtAddLabIncharge.Text);
            cmd.Parameters.Add("@UserCode", Session["uid"].ToString());
            cmd.Parameters.Add("@ExamCoOrdinator", txtExamapproval.Text);
            
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            BindData();
            BindCourse();           
        }
    }

    protected void grdUserRoleMatrix_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            DropDownList ddlAddCourse = (DropDownList)e.Row.FindControl("ddlAddCourse");
           // DropDownList ddlAddCourse = grdUserRoleMatrix.FooterRow.FindControl("ddlAddCourse") as DropDownList;
            SqlCommand cmd = new SqlCommand("SP_GetCourseNotInUserRoleMatrix", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddlAddCourse.DataSource = dt;
            // ddlAddCourse.DataSource = GetData("SELECT DISTINCT Country FROM Customers");
            ddlAddCourse.DataTextField = "Description";
            ddlAddCourse.DataValueField = "Code";
            ddlAddCourse.DataBind();
            //ddlAddCourse.Items.Insert(0, new ListItem("Select Country", "0"));
        }
    }
}


