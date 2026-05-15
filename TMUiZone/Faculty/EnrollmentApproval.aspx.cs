using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_EnrollmentApproval : System.Web.UI.Page
{
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                getenrollment();
                //bindAttachment();
            }
        }
        catch
        {
           
        }
    }
    //public void bindAttachment(object sender, EventArgs e)

    //{
    //    GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
    //    int index = row.RowIndex;
    //    Label UserId = (Label)grdAttachment.Rows[index].FindControl("lblemployeecode");
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    //    DataTable dt = GetData("select datalength([HighSchoolMarksheet]) HighSchoolMarksheet,datalength([InterMarksheet]) InterMarksheet,datalength([Diploma_final_Year]) Diploma_final_Year,datalength([UG_Final_Year]) UG_Final_Year,datalength([Transfer_Certificate]) as 'Transfer_Certificate',datalength([Character_Certificate]) 'Character_Certificate',datalength([Migration]) Migration,datalength([Gap_Affidavit]) Gap_Affidavit,datalength([Domicile]) Domicile,datalength([Student_Aadhar]) Student_Aadhar,datalength([Guardian_Aadhar]) Guardian_Aadhar,datalength([Addmission_Form]) Addmission_Form from [HRMSPortal].dbo.Tbl_EnrollmentTable where   [Student_Number]='" + UserId.Text + "'");

    //    if (dt.Rows.Count > 0)
    //    {

    //        grdAttachment.DataSource = dt;
    //        grdAttachment.DataBind();
    //    }
    //    else
    //    {
    //        grdAttachment.DataSource = "";
    //        grdAttachment.DataBind();
    //    }
    //}
    private DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                }
            }
            return dt;
        }
    }
    public void getenrollment()
    {
        SqlCommand cmd = new SqlCommand("pro_getEnrollmentData", con1);
        cmd.Parameters.AddWithValue("@HOD", Session["uid"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        if (con1.State == ConnectionState.Closed)
            con1.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdenrollmentapprovallist.DataSource = dtCL;
        grdenrollmentapprovallist.DataBind();
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        int i = 0;
        foreach (GridViewRow row in grdenrollmentapprovallist.Rows)
        {
            CheckBox check = (CheckBox)row.FindControl("Chkemployee");
            Label lblemployeecode = (Label)row.FindControl("lblemployeecode");
            var id = grdenrollmentapprovallist.DataKeys[row.RowIndex].Value;
            SqlCommand cmd = new SqlCommand("[HRMSPortal].dbo.pro_approveEnrollment", con1);
            if (check.Checked == true)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Student_Number", lblemployeecode.Text);
                cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@Pri_Approval", "Approved");                
                cmd.Parameters.AddWithValue("@Director_Approval", "Approved");
                cmd.Parameters.AddWithValue("@EnrollmentDept_Approval", "Approved");
                con1.Open();
                cmd.ExecuteNonQuery();
                con1.Close();


                i++;
            }
        }
        if (i > 0) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Enrollment Form Approved')", true); }
        else { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Data.. !')", true); }
        getenrollment();
    }

    protected void BtnRejected_Click(object sender, EventArgs e)
    {
        int i = 0;
        foreach (GridViewRow row in grdenrollmentapprovallist.Rows)
        {
            CheckBox check = (CheckBox)row.FindControl("Chkemployee");
            Label lblemployeecode = (Label)row.FindControl("lblemployeecode");
            TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
            var id = grdenrollmentapprovallist.DataKeys[row.RowIndex].Value;
            SqlCommand cmd = new SqlCommand("[HRMSPortal].dbo.pro_RejectedEnrollment", con1);
            if (check.Checked == true)
            {
                if (txtRemark.Text == "")
                {
                    string message1 = "Please Fill Remark";
                    string script1 = "window.onload = function(){ alert('";
                    script1 += message1;
                    script1 += "')};";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
                    return;
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Student_Number", lblemployeecode.Text);
                cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@RejectRemarks", txtRemark.Text);
                cmd.Parameters.AddWithValue("@Pri_Approval", "Rejected");
                cmd.Parameters.AddWithValue("@Director_Approval", "Rejected");
                cmd.Parameters.AddWithValue("@EnrollmentDept_Approval", "Rejected");
                cmd.Parameters.AddWithValue("@Status", "Pending");
                con1.Open();
                cmd.ExecuteNonQuery();
                con1.Close();
                i++;

            }
        }
        if (i > 0) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Enrollment Form Rejected')", true); }
        else { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Data.. !')", true); }

        getenrollment();
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        GridViewDetails.Hide();
    }

    protected void lnkbutton_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
        int index = row.RowIndex;
        Label UserId = (Label)grdenrollmentapprovallist.Rows[index].FindControl("lblemployeecode");
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "select * from Tbl_EnrollmentTable where [Student_Number]='" + UserId.Text + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
 
 
 string str = "select convert(decimal(16,2),[Credit Amount]) 'Credit Amount',[Document No_],convert(varchar(12),[Posting Date],113) 'Posting Date' FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$Detailed Cust_ Ledg_ Entry] where [Cust_ Ledger Entry No_]=  ( SELECT [Entry No_] FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$Cust_ Ledger Entry] where [Customer No_]='" + UserId.Text + "' and [Fee Code]='APF' and [Document Type]=2)  and [Entry Type]=2 and [Document Type]=1";
 SqlDataAdapter daP = new SqlDataAdapter(str, con);
 DataTable dtP = new DataTable();
 daP.Fill(dtP);
 con.Close();
 if(dtP.Rows.Count>0)
 {
     txtrs.Text = "Enrollment Fee";
     txtreceipt.Text = "Paid";
     txtdateenrollmentfee.Text = dtP.Rows[0]["Posting Date"].ToString();
 }
 else
 {
     txtrs.Text = "Enrollment Fee";
     txtreceipt.Text = "Unpaid";
     txtdateenrollmentfee.Text = "";
 }


        con.Close();
        txtstudentname.Text = dt.Rows[0]["Student_Name"].ToString();
        txtprogrambranch.Text = dt.Rows[0]["Programee_Branch"].ToString();
        txtyearofaddmission.Text = dt.Rows[0]["Year_Of_Admission"].ToString();
        txtnameofcollege.Text = dt.Rows[0]["Name_Of_College"].ToString();
        txtdateofbirth.Text = dt.Rows[0]["Date_Of_Birth"].ToString();
        txtmothername.Text = dt.Rows[0]["Mother_Name"].ToString();
        txtfathername.Text = dt.Rows[0]["Father_Name"].ToString();
        txtgender.Text = dt.Rows[0]["Gender"].ToString();
        txtnationality.Text = dt.Rows[0]["Nationality"].ToString();
        txtreligion.Text = dt.Rows[0]["Religion"].ToString();
        txtcategory.Text = dt.Rows[0]["Category"].ToString();
        txtaddress.Text = dt.Rows[0]["Correspondence_Address"].ToString();
        txtperaddress.Text = dt.Rows[0]["Permanent_Address"].ToString();
        txtdistrict.Text = dt.Rows[0]["District"].ToString();
        txtperdistrict.Text = dt.Rows[0]["District"].ToString();
        txtstate.Text = dt.Rows[0]["State"].ToString();
        txtperstate.Text = dt.Rows[0]["State"].ToString();
        txtcountry.Text = dt.Rows[0]["Country"].ToString();
        txtpercountry.Text = dt.Rows[0]["Country"].ToString();
        txtstudentmob.Text = dt.Rows[0]["Student_Mobile"].ToString();
        txtparentsmob.Text = dt.Rows[0]["Parents_Mobile"].ToString();
        txtemailid.Text = dt.Rows[0]["Email_Id"].ToString();
        lblunderteking.Text = dt.Rows[0]["Undertaking_By"].ToString();
        txtID.Text = dt.Rows[0]["ID"].ToString();
        txtboard10.Text = dt.Rows[0]["10th_Board"].ToString();
        txtyearofpassing10.Text = dt.Rows[0]["10th_Passing_Year"].ToString();
        txtnameofcollege10.Text = dt.Rows[0]["10th_Name_college"].ToString();
        txtboard12.Text = dt.Rows[0]["12th_Board"].ToString();
        txtyearofpassing12.Text = dt.Rows[0]["12th_Passing_Year"].ToString();
        txtnameofcollege12.Text = dt.Rows[0]["12th_Name_College"].ToString();
        txtboardgraduation.Text = dt.Rows[0]["Graduation_Board"].ToString();
        txtyearofpassinggraduation.Text = dt.Rows[0]["Graduation_Passing_Year"].ToString();
        txtnameofcollegegraduation.Text = dt.Rows[0]["Graduation_Passing_Year"].ToString();
        txtboardpost.Text = dt.Rows[0]["PostGraduate_Board"].ToString();
        txtyearofpassingpost.Text = dt.Rows[0]["PostGraduate_Passing_Year"].ToString();
        txtnameofcollegepost.Text = dt.Rows[0]["PostGraduate_Name_College"].ToString();
        txtboardany.Text = dt.Rows[0]["Any_Board"].ToString();
        txtyearofpassingany.Text = dt.Rows[0]["Any_Passing_Year"].ToString();
        txtnameofcollegeany.Text = dt.Rows[0]["Any_Name_College"].ToString();
        txtstudentnumber.Text= dt.Rows[0]["Student_Number"].ToString();
        DataTable dt1 = GetData("select datalength([HighSchoolMarksheet]) HighSchoolMarksheet,datalength([InterMarksheet]) InterMarksheet,datalength([Diploma_final_Year]) Diploma_final_Year,datalength([UG_Final_Year]) UG_Final_Year,datalength([Transfer_Certificate]) as 'Transfer_Certificate',datalength([Character_Certificate]) 'Character_Certificate',datalength([Migration]) Migration,datalength([Gap_Affidavit]) Gap_Affidavit,datalength([Domicile]) Domicile,datalength([Student_Aadhar]) Student_Aadhar,datalength([Guardian_Aadhar]) Guardian_Aadhar,datalength([Addmission_Form]) Addmission_Form from [HRMSPortal].dbo.Tbl_EnrollmentTable where   [Student_Number]='" + UserId.Text + "'");

        if (dt1.Rows.Count > 0)
        {

            grdAttachment.DataSource = dt;
            grdAttachment.DataBind();
        }
        else
        {
            grdAttachment.DataSource = "";
            grdAttachment.DataBind();
        }
            GridViewDetails.Show();
    }

    protected void lbl10th_Click(object sender, EventArgs e)
    {
        //try
        //{

            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "select [HighSchoolMarksheet] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + txtstudentnumber.Text + "'  ";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["HighSchoolMarksheet"];
                        contentType = "application/pdf";
                        fileName = "HighSchoolMarksheet";
                    }
                    con1.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        //catch
        //{
        //    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
        //}
    


    protected void lbl12th_Click(object sender, EventArgs e)
    {
        try
        {

            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "select [InterMarksheet] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + txtstudentnumber.Text + "'  ";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["InterMarksheet"];
                        contentType = "application/pdf";
                        fileName = "InterMarksheet";
                    }
                    con1.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
        }
    }

    protected void lbldipthe_Click(object sender, EventArgs e)
    {
        try
        {

            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "select [Diploma_final_Year] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + txtstudentnumber.Text + "'  ";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Diploma_final_Year"];
                        contentType = "application/pdf";
                        fileName = "Diploma_final_Year";
                    }
                    con1.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
        }
    }

    protected void lblUG_Click(object sender, EventArgs e)
    {
        try
        {

            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "select [UG_Final_Year] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + txtstudentnumber.Text + "'  ";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["UG_Final_Year"];
                        contentType = "application/pdf";
                        fileName = "UG_Final_Year";
                    }
                    con1.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
        }
    }

    protected void lblTran_Click(object sender, EventArgs e)
    {
        try
        {

            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "select [Transfer_Certificate] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + txtstudentnumber.Text + "'";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Transfer_Certificate"];
                        contentType = "application/pdf";
                        fileName = "Transfer_Certificate";
                    }
                    con1.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
        }
    }

    protected void lblCharacter_Click(object sender, EventArgs e)
    {
        try
        {

            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "select [Character_Certificate] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + txtstudentnumber.Text + "'";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Character_Certificate"];
                        contentType = "application/pdf";
                        fileName = "Character_Certificate";
                    }
                    con1.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
        }

    }

    protected void lblMigration_Click(object sender, EventArgs e)
    {

        try
        {

            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "select [Migration] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + txtstudentnumber.Text + "'";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Migration"];
                        contentType = "application/pdf";
                        fileName = "Migration";
                    }
                    con1.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
        }
    }

    protected void lblGap_Click(object sender, EventArgs e)
    {
        try
        {

            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "select [Gap_Affidavit] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + txtstudentnumber.Text + "'";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Gap_Affidavit"];
                        contentType = "application/pdf";
                        fileName = "Gap_Affidavit";
                    }
                    con1.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
        }

    }

    protected void lblDomicile_Click(object sender, EventArgs e)
    {
        try
        {

            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "select [Domicile] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + txtstudentnumber.Text + "'";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Domicile"];
                        contentType = "application/pdf";
                        fileName = "Domicile";
                    }
                    con1.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
        }
    }

    protected void lblAadhar_Click(object sender, EventArgs e)
    {
        try
        {

            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "select [Student_Aadhar] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + txtstudentnumber.Text + "'";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Student_Aadhar"];
                        contentType = "application/pdf";
                        fileName = "Student_Aadhar";
                    }
                    con1.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch

        {
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
        }
    }

    protected void lblGuardian_Click(object sender, EventArgs e)
    {
        try
        {

            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "select [Guardian_Aadhar] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + txtstudentnumber.Text + "'";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Guardian_Aadhar"];
                        contentType = "application/pdf";
                        fileName = "Guardian_Aadhar";
                    }
                    con1.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
        }

    }

    //protected void lblAdmission_Click(object sender, EventArgs e)
    //{

    //    try
    //    {

    //        byte[] bytes;
    //        string fileName, contentType;
    //        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
    //        {
    //            using (SqlCommand cmd2 = new SqlCommand())
    //            {
    //                cmd2.CommandText = "select [Addmission_Form] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + txtstudentnumber.Text + "'";
    //                cmd2.Connection = con2;
    //                con2.Open();
    //                using (SqlDataReader sdr = cmd2.ExecuteReader())
    //                {
    //                    sdr.Read();
    //                    bytes = (byte[])sdr["Addmission_Form"];
    //                    contentType = "application/pdf";
    //                    fileName = "Addmission_Form";
    //                }
    //                con1.Close();
    //            }
    //        }
    //        Response.Clear();
    //        Response.Buffer = true;
    //        Response.Charset = "";
    //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //        Response.ContentType = contentType;
    //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
    //        Response.BinaryWrite(bytes);
    //        Response.Flush();
    //        Response.End();
    //    }
    //    catch
    //    {
    //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + ex + "')", true);
    //    }

    //}
    protected void grdenrollmentapprovallist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdenrollmentapprovallist.PageIndex = e.NewPageIndex;
        getenrollment();
    }
}