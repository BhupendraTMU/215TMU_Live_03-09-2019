using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_ScholarshipApproval : System.Web.UI.Page
{
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                getscholarship();
                HideSave();
            }
            catch
            {
                Response.Redirect("../Default.aspx");
            }
        }

    }
    public void getscholarship()
    {
        SqlCommand cmd = new SqlCommand("pro_getScholarshipdata", con1);
        cmd.Parameters.AddWithValue("@HOD", Session["uid"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        if (con1.State == ConnectionState.Closed)
            con1.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdscholarshipapprovallist.DataSource = dtCL;
        grdscholarshipapprovallist.DataBind();
    }

    protected void lnkbutton_Click(object sender, EventArgs e)
    {
     
        GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
        int index = row.RowIndex;
        Label UserId = (Label)grdscholarshipapprovallist.Rows[index].FindControl("lblemployeecode");
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "select * from Tbl_ScholarshipDeclaration where Enrollment_No ='" + UserId.Text + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            txtreligion1.Text= dt.Rows[0]["Religion"].ToString();
            txtstudentname.Text = dt.Rows[0]["Student_Name"].ToString();
            txtstudentnamejain.Text = dt.Rows[0]["Student_Name"].ToString();
            txtStudentNo.Text = dt.Rows[0]["No_"].ToString();
            txtprogramme.Text = dt.Rows[0]["Programme"].ToString();
            txtfathername.Text = dt.Rows[0]["Father_Name"].ToString();
            //txtstudentCategory.Text = dt.Rows[0]["Student_category"].ToString();
            txtscholarshipamount.Text = dt.Rows[0]["Scholarship_amount"].ToString();
            //txtpercentagerank.Text = dt.Rows[0]["Rank_In_Exam"].ToString();
            // drpapplicablescholarship.SelectedItem.Text = dt.Rows[0]["Applicable_Scholarship"].ToString();
            //drpexamclaimed.SelectedItem.Text = dt.Rows[0]["Scholarship_Claimed"].ToString();
            txtenrollmentnumber.Text = dt.Rows[0]["Enrollment_No"].ToString();
            txtnatureofscholarship.Text = dt.Rows[0]["Nature of Schaolarship"].ToString();
            txtsubject1.Text = dt.Rows[0]["Sub1Name"].ToString();
            txtsubject2.Text = dt.Rows[0]["Sub2Name"].ToString();
            txtsubject3.Text = dt.Rows[0]["Sub3Name"].ToString();
            txtsubject4.Text = dt.Rows[0]["Sub4Name"].ToString();
            txtsubject5.Text = dt.Rows[0]["Sub5Name"].ToString();
            txtsubject6.Text = dt.Rows[0]["Sub6Name"].ToString();
            txtobtsubject1.Text = dt.Rows[0]["Sub1"].ToString();
            txtobtsubject2.Text = dt.Rows[0]["Sub2"].ToString();
            txtobtsubject3.Text = dt.Rows[0]["Sub3"].ToString();
            txtobtsubject4.Text = dt.Rows[0]["Sub4"].ToString();
            txtobtsubject5.Text = dt.Rows[0]["Sub5"].ToString();
            txtobtsubject6.Text = dt.Rows[0]["Sub6"].ToString();
            txtmaxsubject1.Text = dt.Rows[0]["Max1"].ToString();
            txtmaxsubject2.Text = dt.Rows[0]["Max2"].ToString();
            txtmaxsubject3.Text = dt.Rows[0]["Max3"].ToString();
            txtmaxsubject4.Text = dt.Rows[0]["Max4"].ToString();
            txtmaxsubject5.Text = dt.Rows[0]["Max5"].ToString();
            txtmaxsubject6.Text = dt.Rows[0]["Max6"].ToString();
            txtobtainedmarks.Text = dt.Rows[0]["Obtain Marks"].ToString();
            txttotalmarks.Text = dt.Rows[0]["Max Marks"].ToString();
            txtpercent.Text = dt.Rows[0]["Percentage"].ToString();
            txtpersubject1.Text = dt.Rows[0]["Per1"].ToString();
            txtpersubject2.Text = dt.Rows[0]["Per2"].ToString();
            txtpersubject3.Text = dt.Rows[0]["Per3"].ToString();
            txtpersubject4.Text = dt.Rows[0]["Per4"].ToString();
            txtpersubject5.Text = dt.Rows[0]["Per5"].ToString();
            txtpersubject6.Text = dt.Rows[0]["Per6"].ToString();
            txtid.Text = dt.Rows[0]["Id"].ToString();
            TextBox1.Text = dt.Rows[0]["Mainsub1"].ToString();
            TextBox5.Text = dt.Rows[0]["Mainsub2"].ToString();
            TextBox9.Text = dt.Rows[0]["Mainsub3"].ToString();
            TextBox13.Text = dt.Rows[0]["Mainsub4"].ToString();
            TextBox2.Text = dt.Rows[0]["Main obt Mark1"].ToString();
            TextBox6.Text = dt.Rows[0]["Main obtMark2"].ToString();
            TextBox10.Text = dt.Rows[0]["Main obtMark3"].ToString();
            TextBox14.Text = dt.Rows[0]["Main obtMark4"].ToString();
            TextBox3.Text = dt.Rows[0]["Main Maxmarks1"].ToString();
            TextBox7.Text = dt.Rows[0]["Main Maxmarks2"].ToString();
            TextBox11.Text = dt.Rows[0]["Main Maxmarks3"].ToString();
            TextBox15.Text = dt.Rows[0]["Main Maxmarks4"].ToString();
            TextBox4.Text = dt.Rows[0]["Main Obtain Marks"].ToString();
            TextBox8.Text = dt.Rows[0]["Main MaxMarks"].ToString();
            TextBox12.Text = dt.Rows[0]["Main Per"].ToString();
            drpapplicablescholarship.SelectedItem.Text = dt.Rows[0]["Applicable_Scholarship"].ToString();
            if (drpapplicablescholarship.SelectedItem.Text == "YES")
            {

                txtfee.Visible = true;
                txtdiscountper.Visible = true;
                txtscholarshipamount.Visible = true;
                lblfee.Visible = true;
                lbldis.Visible = true;
                lblscamount.Visible = true;
                txtfee.Text = dt.Rows[0]["Tution Fee"].ToString();
                txtdiscountper.Text = dt.Rows[0]["Discount Per(%)"].ToString();
                txtscholarshipamount.Text = dt.Rows[0]["Scholarship_amount"].ToString();
            }
            if (txtnatureofscholarship.Text == "Jain Scholarship" && txtstudentnamejain.Text.Contains("JAIN") == false)
            {
                divjainscholarship.Visible = true;
                lnkCertificate.Visible = true;
                lblreligion.Visible = true;
                txtreligion1.Visible = true;
            }
            else
            {
                divjainscholarship.Visible = false;
                lnkCertificate.Visible = false;
                lblreligion.Visible = true;
                txtreligion1.Visible = true;
            }
            if (txtnatureofscholarship.Text == "Entry Level Scholarship" || txtnatureofscholarship.Text == "Pass Out Student of MadanSwarup Inter College, Hariana (U.P)" || txtnatureofscholarship.Text == "Pass Out Student of Teerthanker Mahaveer University")
            {
                maxmarks.Visible = true;

            }
            else
            {
                maxmarks.Visible = false;
            }
            if (txtnatureofscholarship.Text == "Chancellor's Scholarship (University Staff Ward)")
            {
                divchancellorscholarship.Visible = true;
                txtemployeename.Text = dt.Rows[0]["Employee Name"].ToString();
                txtemployeecode.Text = dt.Rows[0]["Employee Code"].ToString();
                txtdesignation.Text = dt.Rows[0]["Designation"].ToString();
                txtaadharno.Text = dt.Rows[0]["Aadhar No"].ToString();

            }
            else
            {
                divchancellorscholarship.Visible = false;
            }
            //if (txtreligion.Text == "JAIN" && txtstudentnamejain.Text.Contains("JAIN") == false)
            //{
            //    lbljaincertificate.Visible = true;
            //    FileUpload1.Visible = true;
            //    lnkCertificate.Visible = true;
            //}
            //else
            //{
            //    lbljaincertificate.Visible = false;
            //    FileUpload1.Visible = false;
            //    lnkCertificate.Visible = false;
            //}
            if (txtnatureofscholarship.Text == "Competitive Exam")
            {
                maxmarks.Visible = true;
                txttypeofexam.Visible = true;
                subject6.Style["visibility"] = "hidden";
                txttypeofexam.Text = dt.Rows[0]["Scholarship_Claimed"].ToString();
            }
            else
            {

                subject6.Style["visibility"] = "visible";
            }
           
            GridViewDetails.Show();
            HideSave();

        }

    }


    protected void btnclose_Click(object sender, EventArgs e)
    {
        GridViewDetails.Hide();
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        //try
        //{
        int i = 0;
        foreach (GridViewRow row in grdscholarshipapprovallist.Rows)
        {
            CheckBox check = (CheckBox)row.FindControl("Chkemployee");
            Label lblemployeecode = (Label)row.FindControl("lblemployeecode");
            var id = grdscholarshipapprovallist.DataKeys[row.RowIndex].Value;
            SqlCommand cmd = new SqlCommand("[HRMSPortal].dbo.pro_approveScholarship", con1);
            if (check.Checked == true)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Enrollment_No", lblemployeecode.Text);
                cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@Pri_Approval", "Approved");
                cmd.Parameters.AddWithValue("@Registrar_Approval", "Approved");
                cmd.Parameters.AddWithValue("@Director_Approval", "Approved");
                cmd.Parameters.AddWithValue("@Account_Approval", "Approved");
                con1.Open();
                cmd.ExecuteNonQuery();
                con1.Close();


                i++;
            }
        }
        if (i > 0) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Scholarship Form Approved')", true); }
        else { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Data.. !')", true); }
        //BtnSubmit.Visible = false;
        getscholarship();
    }

    protected void BtnRejected_Click(object sender, EventArgs e)
    {
        try
        {
            int i = 0;
            foreach (GridViewRow row in grdscholarshipapprovallist.Rows)
            {

                CheckBox check = (CheckBox)row.FindControl("Chkemployee");
                Label lblemployeecode = (Label)row.FindControl("lblemployeecode");
                TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
                var id = grdscholarshipapprovallist.DataKeys[row.RowIndex].Value;
                SqlCommand cmd = new SqlCommand("[HRMSPortal].dbo.pro_RejectedScholarship", con1);
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
                    cmd.Parameters.AddWithValue("@Enrollment_No", lblemployeecode.Text);
                    cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
                    cmd.Parameters.AddWithValue("@Pri_Approval", "Rejected");
                    cmd.Parameters.AddWithValue("@Reject_Remarks", txtRemark.Text);
                    cmd.Parameters.AddWithValue("@Registrar_Approval", "Rejected");
                    cmd.Parameters.AddWithValue("@Director_Approval", "Rejected");
                    cmd.Parameters.AddWithValue("@Account_Approval", "Rejected");
                    cmd.Parameters.AddWithValue("@Status", "Pending");
                    con1.Open();
                    cmd.ExecuteNonQuery();
                    con1.Close();
                    i++;

                }
            }
            if (i > 0) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Scholarship Form Rejected')", true); }
            else { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Data.. !')", true); }
            //BtnSubmit.Visible = false;
            getscholarship();
        }
        catch (Exception ex)
        {
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
                    cmd2.CommandText = "select [Pre_Education] from [HRMSPortal].dbo.Tbl_ScholarshipDeclaration where Enrollment_No ='" + txtenrollmentnumber.Text + "'";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Pre_Education"];
                        contentType = "application/pdf";
                        fileName = "Pre_Qualification";
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

    protected void txtnatureofscholarship_TextChanged(object sender, EventArgs e)
    {
        if (txtnatureofscholarship.Text == "Jain Scholarship")
        {
            divjainscholarship.Visible = true;
        }
        else
        {
            divjainscholarship.Visible = false;
        }
    }



    protected void btn_Changesubject_Click(object sender, EventArgs e)
    {
        Mainfoursubject.Visible = true;
        GridViewDetails.Show();

        
    }


    protected void drpapplicablescholarship_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpapplicablescholarship.SelectedItem.Text == "SELECT" || drpapplicablescholarship.SelectedItem.Text == "NO")
        {
            txtfee.Visible = false;
            txtdiscountper.Visible = false;
            txtscholarshipamount.Visible = false;
            lblfee.Visible = false;
            lbldis.Visible = false;
            lblscamount.Visible = false;
        }
        else
        {
            txtfee.Visible = true;
            txtdiscountper.Visible = true;
            txtscholarshipamount.Visible = true;
            lblfee.Visible = true;
            lbldis.Visible = true;
            lblscamount.Visible = true;
        }

    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("pro_InsertScholarship1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (drpapplicablescholarship.SelectedItem.Text == "")
        {
            GridViewDetails.Show();
            string message1 = "Please Select Applicable Scholarship  Yes or No .";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;

        }
        //cmd.Parameters.AddWithValue("@Creation_date", "");
        if (txtnatureofscholarship.Text == "Entry Level Scholarship" || txtnatureofscholarship.Text == "Pass Out Student of MadanSwarup Inter College, Hariana (U.P)" || txtnatureofscholarship.Text == "Pass Out Student of Teerthanker Mahaveer University" || txtnatureofscholarship.Text== "Competitive Exam")
        {
            if (TextBox1.Text == "" || TextBox5.Text == "" || TextBox9.Text == "" || TextBox13.Text == "")
            {
                GridViewDetails.Show();
                string message1 = "Please Fill Main Four Subject Name .";
                string script1 = "window.onload = function(){ alert('";
                script1 += message1;
                script1 += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
                return;

            }
            if (TextBox2.Text == "" || TextBox6.Text == "" || TextBox10.Text == "" || TextBox14.Text == "")
            {
                GridViewDetails.Show();
                string message1 = "Please Fill Main Four Subject Obtain Marks .";
                string script1 = "window.onload = function(){ alert('";
                script1 += message1;
                script1 += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
                return;

            }
            if (TextBox3.Text == "" || TextBox7.Text == "" || TextBox11.Text == "" || TextBox15.Text == "")
            {
                GridViewDetails.Show();
                string message1 = "Please Fill Main Four Subject MAX Marks .";
                string script1 = "window.onload = function(){ alert('";
                script1 += message1;
                script1 += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
                return;

            }
            if (drpapplicablescholarship.SelectedItem.Text == "")
            {
                GridViewDetails.Show();
                string message1 = "Please Select Applicable Scholarship  Yes or No .";
                string script1 = "window.onload = function(){ alert('";
                script1 += message1;
                script1 += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
                return;

            }
            cmd.Parameters.AddWithValue("@Id", txtid.Text);
            cmd.Parameters.AddWithValue("@Mainsub1", TextBox1.Text);
            cmd.Parameters.AddWithValue("@Mainsub2", TextBox5.Text);
            cmd.Parameters.AddWithValue("@Mainsub3", TextBox9.Text);
            cmd.Parameters.AddWithValue("@Mainsub4", TextBox13.Text);
            cmd.Parameters.AddWithValue("@MainobtMark1", TextBox2.Text);
            cmd.Parameters.AddWithValue("@MainobtMark2", TextBox6.Text);
            cmd.Parameters.AddWithValue("@MainobtMark3", TextBox10.Text);
            cmd.Parameters.AddWithValue("@MainobtMark4", TextBox14.Text);
            cmd.Parameters.AddWithValue("@MainMaxmarks1", TextBox3.Text);
            cmd.Parameters.AddWithValue("@MainMaxmarks2", TextBox7.Text);
            cmd.Parameters.AddWithValue("@MainMaxmarks3", TextBox11.Text);
            cmd.Parameters.AddWithValue("@MainMaxmarks4", TextBox15.Text);
            cmd.Parameters.AddWithValue("@MainObtainMarks", TextBox4.Text);
            cmd.Parameters.AddWithValue("@MainMaxMarks", TextBox8.Text);
            cmd.Parameters.AddWithValue("@MainPer", TextBox12.Text);
                                           
        }
        else
        {

        }
        //cmd.Parameters.AddWithValue("@Id", txtid.Text);
        cmd.Parameters.AddWithValue("@TutionFee", txtfee.Text);
        cmd.Parameters.AddWithValue("@DiscountPer", txtdiscountper.Text);
        cmd.Parameters.AddWithValue("@PrincipalStatus", "Submit");
        cmd.Parameters.AddWithValue("@Applicable_Scholarship", drpapplicablescholarship.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Scholarship_amount", txtscholarshipamount.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        //string message = "Your details have been saved successfully.";
        //string script = "window.onload = function(){ alert('";
        //script += message;
        //script += "')};";
        //ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
          ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Your details have been saved successfully');document.location.href='ScholarshipApproval.aspx';", true);
        GridViewDetails.Show();
        


    }
    public void HideSave()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("select *, [Principal Status] as Status1 from Tbl_ScholarshipDeclaration where Enrollment_No='" + txtenrollmentnumber.Text + "'", con))
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string Status1 = dr["Status1"].ToString();
                    con.Close();
                    if (Status1 == "Submit")

                    {
                        btn_Save.Visible = false;
                        btn_reset.Visible = false;
                    }
                    else
                    {
                        btn_Save.Visible = true;
                        btn_reset.Visible = true;
                    }


                }
            }
        }
    }

    protected void lnkCertificate_Click(object sender, EventArgs e)
    {
        try
        {

            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "select [Jain Certificate] from [HRMSPortal].dbo.Tbl_ScholarshipDeclaration where Enrollment_No ='" + txtenrollmentnumber.Text + "'";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Jain Certificate"];
                        contentType = "application/pdf";
                        fileName = "Jain Certificate";
                    }
                    con2.Close();
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
}




