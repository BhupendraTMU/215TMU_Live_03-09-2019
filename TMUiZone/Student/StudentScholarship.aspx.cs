using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_StudentScholarship : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
            binddata();
            submitfrm1();
            NatureOfScholarship();
            Scholarship();
            GetEmployeeList();
            GetEmloyeerdetail();
            //percent();
            if (drpnatureofscholarship.SelectedItem.Text == "Entry Level Scholarship" || drpnatureofscholarship.SelectedItem.Text == "Pass Out Student of MadanSwarup Inter College, Hariana (U.P)" || drpnatureofscholarship.SelectedItem.Text == "Pass Out Student of Teerthanker Mahaveer University")
            {
                maxmarks.Visible = true;

            }
            else
            {
                maxmarks.Visible = false;
            }

            if (drpnatureofscholarship.SelectedItem.Text == "Chancellor's Scholarship (University Staff Ward)")
            {

                divchancellorscholarship.Visible = true;
            }
            else
            {

                divchancellorscholarship.Visible = false;
            }
            if (drpnatureofscholarship.SelectedItem.Text == "Jain Scholarship")
            {
                divjainscholarship.Visible = true;

            }
            else
            {
                divjainscholarship.Visible = false;

            }
            if (txtreligion.Text == "JAIN" && txtstudentnamejain.Text.Contains("JAIN") == false)
            {
                lbljaincertificate.Visible = true;
                FileUpload1.Visible = true;
                lnkCertificate.Visible = true;
                lblreligion.Visible = true;
                txtreligion1.Visible = true;
            }
            else
            {
                lbljaincertificate.Visible = false;
                FileUpload1.Visible = false;
                lnkCertificate.Visible = false;
                lblreligion.Visible = true;
                txtreligion1.Visible = true;
            }
            if (drpnatureofscholarship.SelectedItem.Text == "Competitive Exam")
            {

                maxmarks.Visible = true;
                txttypeofexam.Visible = true;
                subject6.Style["visibility"] = "hidden";


            }
            else
            {
                txttypeofexam.Visible = false;
                subject6.Style["visibility"] = "visible";
            }


        
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }
    }
}
    public void GetEmployeeList()
    {

        SqlCommand cmd = new SqlCommand("Pro_GetEmployeelistResignation", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        drpemployeelist.DataSource = dtCL;
        drpemployeelist.DataTextField = "No_";

        drpemployeelist.DataBind();
    }
    public void GetEmloyeerdetail()
    {

        SqlCommand cmd = new SqlCommand("Pro_GetEmployeelist", con);
        cmd.Parameters.AddWithValue("@No_", drpemployeelist.SelectedItem.Text);
        cmd.CommandType = CommandType.StoredProcedure;
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        if (dtCL.Rows.Count > 0)
        {
            txtemployeename.Text = dtCL.Rows[0]["First Name"].ToString();
            txtemployeename.DataBind();
            txtdesignation.Text = dtCL.Rows[0]["Job Title_Grade Desc"].ToString();
            txtdesignation.DataBind();
            txtaadharno.Text = dtCL.Rows[0]["Aadhar Card"].ToString();
            txtaadharno.DataBind();
        }
    }
    public void NatureOfScholarship()
    {
        SqlCommand cmd = new SqlCommand("sp_getNatureofScholarship", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Religion", TextBox16.Text);
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        drpnatureofscholarship.DataSource = dtCL;
        drpnatureofscholarship.DataTextField = "NatureofScholarship";
        drpnatureofscholarship.DataValueField = "ID";
        drpnatureofscholarship.DataBind();

    }

    public void percent()
    {
        double txt11Value, txt12Value, per;
        double.TryParse(txtobtainedmarks.Text, out txt11Value);
        double.TryParse(txttotalmarks.Text, out txt12Value);
        per = (txt11Value / txt12Value) * 100;
        txtpercent.Text = per.ToString();

    }
   
    public void binddata()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        string strSQL = "select [Student Name],[Fathers Name],No_,[Course Name],[Religion], * from[TMU$Student - COLLEGE] where [Enrollment No_] = '" + Session["enroll"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            txtstudentname.Text = dt.Rows[0]["Student Name"].ToString();
            txtstudentnamejain.Text = dt.Rows[0]["Student Name"].ToString();
            txtprogramme.Text = dt.Rows[0]["Course Name"].ToString();
            txtfathername.Text = dt.Rows[0]["Fathers Name"].ToString();
            txtStudentNo.Text = dt.Rows[0]["No_"].ToString();
            txtreligion.Text = dt.Rows[0]["Religion"].ToString();
            txtreligion1.Text = dt.Rows[0]["Religion"].ToString();
            TextBox16.Text = dt.Rows[0]["Religion"].ToString();

        }
    }



    public void Scholarship()
    {
        NatureOfScholarship();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "select *  from   Tbl_ScholarshipDeclaration where Enrollment_No = '" + Session["enroll"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {

            txtscholarshipamount.Text = dt.Rows[0]["Scholarship_amount"].ToString();
            txtapplicablescholarship.Text = dt.Rows[0]["Applicable_Scholarship"].ToString();
            //drpexamclaimed.SelectedItem.Text = dt.Rows[0]["Scholarship_Claimed"].ToString();
            lblhodstatus1.Text = dt.Rows[0]["Pri_Approval"].ToString();
            lblAddmissionDirector.Text = dt.Rows[0]["Director_Approval"].ToString();
            lblregistrar.Text = dt.Rows[0]["Registrar_Approval"].ToString();
            lblAccountdept.Text = dt.Rows[0]["Account_Approval"].ToString();
            lblRejectionReasons.Text = dt.Rows[0]["Reject_Remarks"].ToString();
            drpnatureofscholarship.SelectedItem.Text = dt.Rows[0]["Nature of Schaolarship"].ToString();
            if (drpnatureofscholarship.SelectedItem.Text == "Entry Level Scholarship" || drpnatureofscholarship.SelectedItem.Text == "Pass Out Student of MadanSwarup Inter College, Hariana (U.P)" || drpnatureofscholarship.SelectedItem.Text == "Pass Out Student of Teerthanker Mahaveer University")
            {
                maxmarks.Visible = true;

            }
            else
            {
                maxmarks.Visible = false;
            }

            if (drpnatureofscholarship.SelectedItem.Text == "Chancellor's Scholarship (University Staff Ward)")
            {
                //GetEmployeeList();
                divchancellorscholarship.Visible = true;
                //txttypeofexam.Visible = true;
                txtemployeename.Text = dt.Rows[0]["Employee Name"].ToString();
                drpemployeelist.SelectedValue = dt.Rows[0]["Employee Code"].ToString();
                txtdesignation.Text = dt.Rows[0]["Designation"].ToString();
                txtaadharno.Text = dt.Rows[0]["Aadhar No"].ToString();

            }
            else
            {
                divchancellorscholarship.Visible = false;
                // txttypeofexam.Visible = false;
            }


            if (drpnatureofscholarship.SelectedItem.Text == "Jain Scholarship")
            {
                divjainscholarship.Visible = true;

            }
            else
            {
                divjainscholarship.Visible = false;

            }
            if (txtreligion.Text == "JAIN" && txtstudentnamejain.Text.Contains("JAIN") == false)
            {
                lbljaincertificate.Visible = true;
                FileUpload1.Visible = true;
                lnkCertificate.Visible = true;
            }
            else
            {
                lbljaincertificate.Visible = false;
                FileUpload1.Visible = false;
                lnkCertificate.Visible = false;
            }
            if (drpnatureofscholarship.SelectedItem.Text == "Competitive Exam")
            {

                maxmarks.Visible = true;
                txttypeofexam.Visible = true;
                subject6.Style["visibility"] = "hidden";


            }
            else
            {
                txttypeofexam.Visible = false;
                subject6.Style["visibility"] = "visible";
            }
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
            // txtapplicablescholarship.Text= dt.Rows[0]["Applicable_Scholarship"].ToString();
            txtfee.Text = dt.Rows[0]["Tution Fee"].ToString();
            txtdiscountper.Text = dt.Rows[0]["Discount Per(%)"].ToString();
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
            txttypeofexam.Text = dt.Rows[0]["Scholarship_Claimed"].ToString();
            //txtscholarshipamount.Text= dt.Rows[0]["Scholarship_amount"].ToString();
            // NatureOfScholarship();


        }
    }

    protected void btn_reset_Click(object sender, EventArgs e)
    {
        Response.Redirect("StudentScholarship.aspx");
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (FileUpload2.HasFile)
        {

        }
        else
        {
            string message1 = "Please Upload Pre_Qualification Marksheet ";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }

        string contentType1 = "";
        byte[] Photo = new byte[720];
        if (FileUpload2.HasFile)
        {
            contentType1 = FileUpload2.PostedFile.ContentType;

            string filename = Path.GetFileName(FileUpload2.PostedFile.FileName);


            using (Stream fs = FileUpload2.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    Photo = br.ReadBytes((Int32)fs.Length);

                }

            }
            if (contentType1 != "application/pdf")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Upload  PDF File Only');", true);

                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_InsertScholarship", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Creation_date", "");
            cmd.Parameters.AddWithValue("@Enrollment_No", Session["enroll"].ToString());
            cmd.Parameters.AddWithValue("@Student_Name", txtstudentname.Text);
            cmd.Parameters.AddWithValue("@Father_Name", txtfathername.Text);
            cmd.Parameters.AddWithValue("@Programme", txtprogramme.Text);
            cmd.Parameters.AddWithValue("@Status ", "Submit");
            //cmd.Parameters.AddWithValue("@Scholarship_amount", txtscholarshipamount.Text);
            cmd.Parameters.AddWithValue("@Director_Admission", "TMU04870");
            cmd.Parameters.AddWithValue("@Registrar", "TMU03651");
            cmd.Parameters.AddWithValue("@Account", "TMU01023");
            cmd.Parameters.AddWithValue("@Pri_Approval", "Pending");
            cmd.Parameters.AddWithValue("@Director_Approval", "Pending");
            cmd.Parameters.AddWithValue("@Registrar_Approval", "Pending");
            cmd.Parameters.AddWithValue("@Account_Approval", "Pending");
            cmd.Parameters.AddWithValue("@Pre_Education", Photo);
            cmd.Parameters.AddWithValue("@No_", txtStudentNo.Text);
            cmd.Parameters.AddWithValue("@NatureofSchaolarship", drpnatureofscholarship.SelectedItem.Text);
            if (drpnatureofscholarship.SelectedItem.Text == "Entry Level Scholarship" || drpnatureofscholarship.SelectedItem.Text == "Pass Out Student of MadanSwarup Inter College, Hariana (U.P)" || drpnatureofscholarship.SelectedItem.Text == "Pass Out Student of Teerthanker Mahaveer University")
            {
                if (txtsubject1.Text == "" || txtsubject2.Text == "" || txtsubject3.Text == "" || txtsubject4.Text == "" || txtsubject5.Text == "")
                {
                    string message1 = "Please Fill Subject Name .";
                    string script1 = "window.onload = function(){ alert('";
                    script1 += message1;
                    script1 += "')};";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
                    return;
                }
                if (txtobtsubject1.Text == "" || txtobtsubject2.Text == "" || txtobtsubject3.Text == "" || txtobtsubject4.Text == "" || txtobtsubject5.Text == "")
                {
                    string message1 = "Please Fill Obtain Marks  .";
                    string script1 = "window.onload = function(){ alert('";
                    script1 += message1;
                    script1 += "')};";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
                    return;
                }
                if (txtmaxsubject1.Text == "" || txtmaxsubject2.Text == "" || txtmaxsubject3.Text == "" || txtmaxsubject4.Text == "" || txtmaxsubject5.Text == "")
                {
                    string message1 = "Please Fill Max Marks  .";
                    string script1 = "window.onload = function(){ alert('";
                    script1 += message1;
                    script1 += "')};";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
                    return;
                }
                cmd.Parameters.AddWithValue("@Sub1", txtobtsubject1.Text);
                cmd.Parameters.AddWithValue("@Sub2", txtobtsubject2.Text);
                cmd.Parameters.AddWithValue("@Sub3", txtobtsubject3.Text);
                cmd.Parameters.AddWithValue("@Sub4", txtobtsubject4.Text);
                cmd.Parameters.AddWithValue("@Sub5", txtobtsubject5.Text);
                cmd.Parameters.AddWithValue("@Sub6", txtobtsubject6.Text);
                cmd.Parameters.AddWithValue("@Max1", txtmaxsubject1.Text);
                cmd.Parameters.AddWithValue("@Max2", txtmaxsubject2.Text);
                cmd.Parameters.AddWithValue("@Max3", txtmaxsubject3.Text);
                cmd.Parameters.AddWithValue("@Max4", txtmaxsubject4.Text);
                cmd.Parameters.AddWithValue("@Max5", txtmaxsubject5.Text);
                cmd.Parameters.AddWithValue("@Max6", txtmaxsubject6.Text);
                cmd.Parameters.AddWithValue("@ObtainMarks", txtobtainedmarks.Text);
                cmd.Parameters.AddWithValue("@MaxMarks", txttotalmarks.Text);
                cmd.Parameters.AddWithValue("@Percentage", txtpercent.Text);
                cmd.Parameters.AddWithValue("@Sub1Name", txtsubject1.Text);
                cmd.Parameters.AddWithValue("@Sub2Name", txtsubject2.Text);
                cmd.Parameters.AddWithValue("@Sub3Name", txtsubject3.Text);
                cmd.Parameters.AddWithValue("@Sub4Name", txtsubject4.Text);
                cmd.Parameters.AddWithValue("@Sub5Name", txtsubject5.Text);
                cmd.Parameters.AddWithValue("@Sub6Name", txtsubject6.Text);
                cmd.Parameters.AddWithValue("@Per1", txtpersubject1.Text);
                cmd.Parameters.AddWithValue("@Per2", txtpersubject2.Text);
                cmd.Parameters.AddWithValue("@Per3", txtpersubject3.Text);
                cmd.Parameters.AddWithValue("@Per4", txtpersubject4.Text);
                cmd.Parameters.AddWithValue("@Per5", txtpersubject5.Text);
                cmd.Parameters.AddWithValue("@Per6", txtpersubject6.Text);
                cmd.Parameters.AddWithValue("@Id", txtid.Text);
            }
            if (drpnatureofscholarship.SelectedItem.Text == "Competitive Exam")
            {
                if (txttypeofexam.Text == "")
                {
                    string message1 = "Please Fill Exam Type .";
                    string script1 = "window.onload = function(){ alert('";
                    script1 += message1;
                    script1 += "')};";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
                    return;
                }
                if (txtsubject1.Text == "" || txtsubject2.Text == "" || txtsubject3.Text == "" || txtsubject4.Text == "" || txtsubject5.Text == "")
                {
                    string message1 = "Please Fill Subject Name .";
                    string script1 = "window.onload = function(){ alert('";
                    script1 += message1;
                    script1 += "')};";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
                    return;
                }
                if (txtobtsubject1.Text == "" || txtobtsubject2.Text == "" || txtobtsubject3.Text == "" || txtobtsubject4.Text == "" || txtobtsubject5.Text == "")
                {
                    string message1 = "Please Fill Obtain Marks  .";
                    string script1 = "window.onload = function(){ alert('";
                    script1 += message1;
                    script1 += "')};";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
                    return;
                }
                if (txtmaxsubject1.Text == "" || txtmaxsubject2.Text == "" || txtmaxsubject3.Text == "" || txtmaxsubject4.Text == "" || txtmaxsubject5.Text == "")
                {
                    string message1 = "Please Fill Max Marks  .";
                    string script1 = "window.onload = function(){ alert('";
                    script1 += message1;
                    script1 += "')};";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
                    return;
                }

                cmd.Parameters.AddWithValue("@Sub1", txtobtsubject1.Text);
                cmd.Parameters.AddWithValue("@Sub2", txtobtsubject2.Text);
                cmd.Parameters.AddWithValue("@Sub3", txtobtsubject3.Text);
                cmd.Parameters.AddWithValue("@Sub4", txtobtsubject4.Text);
                cmd.Parameters.AddWithValue("@Sub5", txtobtsubject5.Text);
                cmd.Parameters.AddWithValue("@Max1", txtmaxsubject1.Text);
                cmd.Parameters.AddWithValue("@Max2", txtmaxsubject2.Text);
                cmd.Parameters.AddWithValue("@Max3", txtmaxsubject3.Text);
                cmd.Parameters.AddWithValue("@Max4", txtmaxsubject4.Text);
                cmd.Parameters.AddWithValue("@Max5", txtmaxsubject5.Text);
                cmd.Parameters.AddWithValue("@ObtainMarks", txtobtainedmarks.Text);
                cmd.Parameters.AddWithValue("@MaxMarks", txttotalmarks.Text);
                cmd.Parameters.AddWithValue("@Percentage", txtpercent.Text);
                cmd.Parameters.AddWithValue("@Sub1Name", txtsubject1.Text);
                cmd.Parameters.AddWithValue("@Sub2Name", txtsubject2.Text);
                cmd.Parameters.AddWithValue("@Sub3Name", txtsubject3.Text);
                cmd.Parameters.AddWithValue("@Sub4Name", txtsubject4.Text);
                cmd.Parameters.AddWithValue("@Sub5Name", txtsubject5.Text);
                cmd.Parameters.AddWithValue("@Per1", txtpersubject1.Text);
                cmd.Parameters.AddWithValue("@Per2", txtpersubject2.Text);
                cmd.Parameters.AddWithValue("@Per3", txtpersubject3.Text);
                cmd.Parameters.AddWithValue("@Per4", txtpersubject4.Text);
                cmd.Parameters.AddWithValue("@Per5", txtpersubject5.Text);
                cmd.Parameters.AddWithValue("@Per6", txtpersubject6.Text);
                cmd.Parameters.AddWithValue("@Id", txtid.Text);
                cmd.Parameters.AddWithValue("@Scholarship_Claimed", txttypeofexam.Text);


            }
            if (drpnatureofscholarship.SelectedItem.Text == "Chancellor's Scholarship (University Staff Ward)")
            {
                if (txtemployeename.Text == "")
                {
                    string message1 = "Please Fill Employee Name .";
                    string script1 = "window.onload = function(){ alert('";
                    script1 += message1;
                    script1 += "')};";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
                    return;
                }
                if (drpemployeelist.SelectedItem.Text == "SELECT")
                {
                    string message1 = "Please Fill Employee Code.";
                    string script1 = "window.onload = function(){ alert('";
                    script1 += message1;
                    script1 += "')};";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
                    return;
                }
                if (txtdesignation.Text == "")
                {
                    string message1 = "Please Fill Designation.";
                    string script1 = "window.onload = function(){ alert('";
                    script1 += message1;
                    script1 += "')};";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
                    return;
                }
                if (txtaadharno.Text == "")
                {
                    string message1 = "Please Fill Aadhar No.";
                    string script1 = "window.onload = function(){ alert('";
                    script1 += message1;
                    script1 += "')};";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
                    return;
                }
                cmd.Parameters.AddWithValue("@EmployeeName", txtemployeename.Text);
                cmd.Parameters.AddWithValue("@EmployeeCode", drpemployeelist.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Designation", txtdesignation.Text);
                cmd.Parameters.AddWithValue("@AadharNo", txtaadharno.Text);
                cmd.Parameters.AddWithValue("@Id", txtid.Text);
            }
            if (drpnatureofscholarship.SelectedItem.Text == "Jain Scholarship")
            {
                if (txtreligion.Text == "JAIN" && txtstudentnamejain.Text.Contains("JAIN") == false)
                {
                    if (FileUpload1.HasFile)
                    {

                    }
                    else
                    {
                        string message1 = "Please Upload Jain Certificate ";
                        string script1 = "window.onload = function(){ alert('";
                        script1 += message1;
                        script1 += "')};";
                        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
                        return;
                    }
                    string contentType2 = "";
                    byte[] Photo1 = new byte[720];
                    if (FileUpload1.HasFile)
                    {
                        contentType2 = FileUpload1.PostedFile.ContentType;

                        string filename1 = Path.GetFileName(FileUpload1.PostedFile.FileName);


                        using (Stream fs = FileUpload1.PostedFile.InputStream)
                        {
                            using (BinaryReader br = new BinaryReader(fs))
                            {
                                Photo1 = br.ReadBytes((Int32)fs.Length);

                            }
                        }
                        if (contentType2 != "application/pdf")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Upload  PDF File Only');", true);
                            return;
                        }

                        {
                            cmd.Parameters.AddWithValue("@Religion", txtreligion.Text);
                            cmd.Parameters.AddWithValue("@JainCertificate", Photo1);
                            cmd.Parameters.AddWithValue("@Id", txtid.Text);
                        }
                    }


                }
                else

                {
                    cmd.Parameters.AddWithValue("@Religion", txtreligion.Text);
                    cmd.Parameters.AddWithValue("@Id", txtid.Text);
                    //cmd.Parameters.AddWithValue("@JainCertificate", Photo1);

                }
            }

            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save successfully.'); document.location.href='StudentScholarship.aspx';", true);

        }
    }


    public void submitfrm1()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("select *  from  Tbl_ScholarshipDeclaration where Enrollment_No='" + Session["enroll"].ToString() + "'", con))
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string Status = dr["Status"].ToString();
                    con.Close();
                    if (Status == "Submit")

                    {
                        btn_Save.Visible = false;
                        btn_reset.Visible = false;
                        btn_Changesubject.Visible = true;
                        //txtapplicablescholarship.Enabled = false;
                        //txtpercentagerank.Enabled = false;
                        //txtscholarshipamount.Enabled = false;
                        //drpexamclaimed.Enabled = false;
                        //chkentrylevel.Enabled = false;
                        //CheckBox1.Enabled = false;
                        //CheckBox2.Enabled = false;
                        //CheckBox3.Enabled = false;
                        //CheckBox4.Enabled = false;
                        //CheckBox5.Enabled = false;
                        //CheckBox6.Enabled = false;
                    }
                    else
                    {
                        btn_Save.Visible = true;
                        btn_reset.Visible = true;
                        btn_Changesubject.Visible = false;
                        //txtapplicablescholarship.Enabled = true;
                        //txtpercentagerank.Enabled = true;
                        //txtscholarshipamount.Enabled = true;
                        //drpexamclaimed.Enabled = true;
                        //chkentrylevel.Enabled = true;
                        //CheckBox1.Enabled = true;
                        //CheckBox2.Enabled = true;
                        //CheckBox3.Enabled = true;
                        //CheckBox4.Enabled = true;
                        //CheckBox5.Enabled = true;
                        //CheckBox6.Enabled = true;
                    }
                }
            }
        }
    }
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
                    cmd2.CommandText = "select [Pre_Education] from [HRMSPortal].dbo.Tbl_ScholarshipDeclaration where Enrollment_No ='" + Session["enroll"].ToString() + "'  ";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Pre_Education"];
                        contentType = "application/pdf";
                        fileName = "Pre_Qualification";
                    }
                    con.Close();
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




    protected void drpnatureofscholarship_SelectedIndexChanged(object sender, EventArgs e)
    {
        
      

        if (drpnatureofscholarship.SelectedItem.Text == "Entry Level Scholarship" || drpnatureofscholarship.SelectedItem.Text == "Pass Out Student of MadanSwarup Inter College, Hariana (U.P)" || drpnatureofscholarship.SelectedItem.Text == "Pass Out Student of Teerthanker Mahaveer University")
        {
            maxmarks.Visible = true;

        }
        else
        {
            maxmarks.Visible = false;
        }

        if (drpnatureofscholarship.SelectedItem.Text == "Chancellor's Scholarship (University Staff Ward)")
        {

            divchancellorscholarship.Visible = true;
        }
        else
        {

            divchancellorscholarship.Visible = false;
        }
        if (drpnatureofscholarship.SelectedItem.Text == "Jain Scholarship")
        {
            divjainscholarship.Visible = true;

        }
        else
        {
            divjainscholarship.Visible = false;

        }
        if (txtreligion.Text == "JAIN" && txtstudentnamejain.Text.Contains("JAIN") == false)
        {
            lbljaincertificate.Visible = true;
            FileUpload1.Visible = true;
            lnkCertificate.Visible = true;
            lblreligion.Visible = true;
            txtreligion1.Visible = true;
        }
        else
        {
            lbljaincertificate.Visible = false;
            FileUpload1.Visible = false;
            lnkCertificate.Visible = false;
            lblreligion.Visible = true;
            txtreligion1.Visible = true;
        }
        if (drpnatureofscholarship.SelectedItem.Text == "Competitive Exam")
        {

            maxmarks.Visible = true;
            txttypeofexam.Visible = true;
            subject6.Style["visibility"] = "hidden";


        }
        else
        {
            txttypeofexam.Visible = false;
            subject6.Style["visibility"] = "visible";
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
                    cmd2.CommandText = "select [Jain Certificate] from [HRMSPortal].dbo.Tbl_ScholarshipDeclaration where Enrollment_No ='" + Session["enroll"].ToString() + "'  ";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Jain Certificate"];
                        contentType = "application/pdf";
                        fileName = "Jain Certificate";
                    }
                    con.Close();
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

    protected void btn_Changesubject_Click(object sender, EventArgs e)
    {
        Mainfoursubject.Visible = true;
    }

    protected void drpemployeelist_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetEmloyeerdetail();

    }
}



