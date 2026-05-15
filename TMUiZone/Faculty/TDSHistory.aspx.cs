using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_TDSHistory : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal con;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            Portalcon = new Connection();
            con = new ServicePoratal();
            //getgriddata();
        }



    }
    //public void getgriddata()
    //{

    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand("Sp_getExamTimesheetdataInternalApp", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@AcademicYear", ddlAcademicYear.SelectedValue);
    //        cmd.Parameters.AddWithValue("@CT", ddlct.SelectedValue);
    //        cmd.Parameters.AddWithValue("@CourseCode", ddlCourse.SelectedValue);
    //        cmd.Parameters.AddWithValue("@CollegeCode", DdlCollege.SelectedValue);
    //        cmd.Parameters.AddWithValue("@Sem", ddlSem.SelectedValue);
    //        cmd.Parameters.AddWithValue("@Status", Rblist.SelectedValue);
    //        cmd.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
    //        if (ChkOpen.Checked == true)
    //        {
    //            cmd.Parameters.AddWithValue("@Open", 1);
    //        }
    //        else
    //        {
    //            cmd.Parameters.AddWithValue("@Open", 0);
    //        }


    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        DataTable dt = new DataTable();
    //        con.Open();
    //        da.Fill(dt);
    //        con.Close();
    //        GrdExamTimeSheet.DataSource = dt;
    //        GrdExamTimeSheet.DataBind();
    //        if (dt.Rows.Count > 0)
    //        {
    //            GrdExamTimeSheet.Visible = true;

    //            if (Rblist.SelectedValue == "1")
    //            {
    //                BtnSubmit.Visible = true;
    //                BtnReject.Visible = true;
    //            }
    //            else if (Rblist.SelectedValue == "2")
    //            {
    //                BtnSubmit.Visible = false;
    //                BtnReject.Visible = true;
    //            }

    //            else if (Rblist.SelectedValue == "3")
    //            {
    //                BtnSubmit.Visible = true;
    //                BtnReject.Visible = true;
    //            }
    //            else if (Rblist.SelectedValue == "4")
    //            {
    //                BtnSubmit.Visible = false;
    //                BtnReject.Visible = false;
    //            }
    //            else if (Rblist.SelectedValue == "5")
    //            {
    //                BtnSubmit.Visible = true;
    //                BtnReject.Visible = true;
    //            }
    //        }

    //        else
    //        {
    //            BtnSubmit.Visible = false;
    //            BtnReject.Visible = false;
    //        }



    //    }
    //    catch (Exception)
    //    {
    //    }
    //}
}