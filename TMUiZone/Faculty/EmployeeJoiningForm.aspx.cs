using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

public partial class Faculty_EmployeeJoiningForm : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getstate();
            getcategory();
            getemployeedetail();
            getunitcollege();
            loginpage();

        }
    }

    public void getemployeedetail()
    {

        SqlCommand cmd = new SqlCommand("pro_getemployee", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdnewjoininglist.DataSource = dtCL;
        grdnewjoininglist.DataBind();
    }
    public void getstate()
    {

        SqlCommand cmd = new SqlCommand("pro_getstate", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);

        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        drpState.DataSource = dtCL;
        drpState.DataTextField = "Description";
        drpState.DataBind();
    }
    public void getcategory()
    {

        SqlCommand cmd = new SqlCommand("pro_getcategory", con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter adpt = new SqlDataAdapter(cmd);

        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        drpreligion.DataSource = dtCL;
        drpreligion.DataTextField = "Description";
        drpreligion.DataBind();
    }
    public void getunitcollege()
    {

        SqlCommand cmd = new SqlCommand("pro_getunitcollege", con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter adpt = new SqlDataAdapter(cmd);

        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        drpunitcollege.DataSource = dtCL;
        drpunitcollege.DataTextField = "Name";
        drpunitcollege.DataBind();
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {

        if (CheckBox1.Checked)
        {

            txtpermentadd.Text = txtpreaddress.Text;
            txtpercity.Text = txtprecity.Text;
            txtperdistrict.Text = txtpredisatrict.Text;
            txtperpostcode.Text = txtprepostcode.Text;


            GridViewdata.Show();
        }
        else
        {
            txtpermentadd.Text = "";
            txtpercity.Text = "";
            txtperdistrict.Text = "";
            txtperpostcode.Text = "";

            GridViewdata.Show();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        //if (drptitle.SelectedItem.Text == "--Select Employee Title--")
        //{

        //    string message1 = "Please Fill Employee Title.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();

        //    return;
        //}
        //if (txtemployeename.Text == "")
        //{
        //    string message1 = "Please Fill Employee Name.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        //if (drpemployeetype.SelectedItem.Text == "--Select Employee Type--")
        //{
        //    string message1 = "Please Fill Employee Type.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        //if (drpnatureofappoint.SelectedItem.Text == "--Select Employee Nature Of Appointment--")
        //{
        //    string message1 = "Please Fill Employee Nature of Appointment.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        //if (drpunitcollege.SelectedItem.Text == "--Select Unit/College--")
        //{
        //    string message1 = "Please Fill Unit(College/Department).";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        //if (txtdateofjoining.Text == "")
        //{
        //    string message1 = "Please Fill Joining Date.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;

        //}
        //if (txtdesignation.Text == "")
        //{
        //    string message1 = "Please Fill Employee Designation.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        //if (txtdateofbirth.Text == "")
        //{
        //    string message1 = "Please Fill Employee Date of Birth.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        //if (txtnationality.Text == "")
        //{
        //    string message1 = "Please Fill Employee Nationality.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        //if (drpgender.Text == "--Select Employee Gender--")
        //{
        //    string message1 = "Please Fill Employee Gender.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}


        //if (txtmobileno.Text == "")
        //{
        //    string message1 = "Please Fill Employee Mobile.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        //if (txtaltmobileno.Text == "")
        //{
        //    string message1 = "Please Fill Employee Alt. Mobile.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        //if (txtemailid.Text == "")
        //{
        //    string message1 = "Please Fill Employee Email ID.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        //if (drpreligion.SelectedItem.Text == "--Select Employee Religion--")
        //{
        //    string message1 = "Please Fill Employee Religion.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        //if (drpcategory.SelectedItem.Text == "--Select Employee Category--")
        //{
        //    string message1 = "Please Fill Employee Category.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        //if (drpphysicallychallenged.SelectedItem.Text == "--Select Employee Physically Challenged--")
        //{
        //    string message1 = "Please Fill Employee Physically.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        //if (drpState.SelectedItem.Text == "--Select Employee State--")
        //{
        //    string message1 = "Please Fill Employee State.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        //if (txtpanno.Text == "")
        //{
        //    string message1 = "Please Fill Employee Pan No.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        //if (txtaadharno.Text == "")
        //{
        //    string message1 = "Please Fill Employee Aadhar No.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        //if (txtemergencyname.Text == "")
        //{
        //    string message1 = "Please Fill Emengency Contact Person Name.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        //if (txtemergenmobile.Text == "")
        //{
        //    string message1 = "Please Fill Emengency Contact Person Mobile No.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        //if (txtemergencymail.Text == "")
        //{
        //    string message1 = "Please Fill Emengency Contact Person Email ID.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        //if (drprelationship.SelectedItem.Text == "--Select Employee Relaion--")
        //{
        //    string message1 = "Please Fill Employee Relaionship.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        //if (txtpreaddress.Text == "")
        //{
        //    string message1 = "Please Fill Present Address.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        //if (txtprecity.Text == "")
        //{
        //    string message1 = "Please Fill Present city.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        //if (txtpredisatrict.Text == "")
        //{
        //    string message1 = "Please Fill Present District.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}

        //if (txtprepostcode.Text == "")
        //{
        //    string message1 = "Please Fill Present Post Code.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        //if (txtpermentadd.Text == "")
        //{
        //    string message1 = "Please Fill Permanent Address.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        //if (txtpercity.Text == "")
        //{
        //    string message1 = "Please Fill Permanent City.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        //if (txtperdistrict.Text == "")
        //{
        //    string message1 = "Please Fill Permanent District.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        //if (txtperpostcode.Text == "")
        //{
        //    string message1 = "Please Fill Permanent Post Code.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}

        //string email = txtemailid.Text;

        //Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        //Match match = regex.Match(email);
        //if (!match.Success)
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'You have entered wrong email id.');", true);
        //    GridViewdata.Show();
        //    return;

        //}
        //string emailAlter = txtemergencymail.Text;
        //Regex regex1 = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        //Match match1 = regex1.Match(email);
        //if (!match1.Success)
        //{

        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'You have entered wrong Emergency email id.');", true);
        //    GridViewdata.Show();
        //    return;

        //}
        string contentType1 = "";
        byte[] Photo = new byte[720];
        //if (FileUpload2.HasFile)
        //{
        contentType1 = FileUpload2.PostedFile.ContentType;
        string filename = Path.GetFileName(FileUpload2.PostedFile.FileName);

        using (Stream fs = FileUpload2.PostedFile.InputStream)
        {
            using (BinaryReader br = new BinaryReader(fs))
            {
                Photo = br.ReadBytes((Int32)fs.Length);
            }
        }
        SqlCommand cmd = new SqlCommand("pro_insertjoiningdata", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@TITLE", drptitle.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@FULL_NAME", txtemployeename.Text);
        cmd.Parameters.AddWithValue("@EMPLOYEE_TYPE", drpemployeetype.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@NATURE_OF_APPOINTMENT", drpnatureofappoint.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@UNIT_COLLEGE_DEPARTMENT", drpunitcollege.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@DEPT_IF_ANY", txtdept.Text);
        cmd.Parameters.AddWithValue("@SUB_DEPT_IF_ANY", txtsubdept.Text);
        cmd.Parameters.AddWithValue("@DATE_OF_JOINING", txtdateofjoining.Text);
        cmd.Parameters.AddWithValue("@DESIGNATION", txtdesignation.Text);
        cmd.Parameters.AddWithValue("@DATE_OF_BIRTH", txtdateofbirth.Text);
        cmd.Parameters.AddWithValue("@FATHER_NAME", txtfathername.Text);
        cmd.Parameters.AddWithValue("@MOTHER_NAME", txtmothername.Text);
        cmd.Parameters.AddWithValue("@GENDER", drpgender.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@MARITAL_STATUS", drpmaritalstatus.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@SPOUSE_PROFESSION", drpspouseprofession.SelectedItem.Text);
        cmd.Parameters.AddWithValue("Spous_Name", txtspousename.Text);
        cmd.Parameters.AddWithValue("@MOBILE_NO", txtmobileno.Text);
        cmd.Parameters.AddWithValue("@ALTER_MOBILE_NO", txtaltmobileno.Text);
        cmd.Parameters.AddWithValue("@E_MAIL_ID", txtemailid.Text);
        cmd.Parameters.AddWithValue("@RELIGION", drpreligion.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@CATEGORY", drpcategory.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@BOOLD_GROUP", txtbloodgroup.Text);
        cmd.Parameters.AddWithValue("@PHYSICALLY_CHALLENGED", drpphysicallychallenged.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@NATIONALITY", txtnationality.Text);
        cmd.Parameters.AddWithValue("@DOMICILE_STATE", drpState.SelectedValue);
        cmd.Parameters.AddWithValue("@VOTER_ID_NO", txtvoterid.Text);
        cmd.Parameters.AddWithValue("@DRIVING_LICENSE_NO", txtdrivinglice.Text);
        cmd.Parameters.AddWithValue("@PASSPORT_NO", txtpassport.Text);
        cmd.Parameters.AddWithValue("@VALID_UP_TO", txtvalidupto.Text);
        cmd.Parameters.AddWithValue("@PAN", txtpanno.Text);
        cmd.Parameters.AddWithValue("@AADHAR_NO", txtaadharno.Text);
        cmd.Parameters.AddWithValue("@EMER_CON_PERSON_NAME", txtemergencyname.Text);
        cmd.Parameters.AddWithValue("@EMER_MOBILE_NO", txtemergenmobile.Text);
        cmd.Parameters.AddWithValue("@EMER_E_MAIL_ID", txtemergencymail.Text);
        cmd.Parameters.AddWithValue("@EMER_RELATIONSHIP", drprelationship.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@PRESENT_ADDRESS", txtpreaddress.Text);
        cmd.Parameters.AddWithValue("@PRE_CITY", txtprecity.Text);
        cmd.Parameters.AddWithValue("@PRE_DISTRICT", txtpredisatrict.Text);
        cmd.Parameters.AddWithValue("@PRE_POST_CODE", txtprepostcode.Text);
        cmd.Parameters.AddWithValue("@PER_ADDRESS", txtpermentadd.Text);
        cmd.Parameters.AddWithValue("@PER_CITY", txtpercity.Text);
        cmd.Parameters.AddWithValue("@PER_DISTRICT", txtperdistrict.Text);
        cmd.Parameters.AddWithValue("@PER_POST_CODE", txtperpostcode.Text);
        cmd.Parameters.AddWithValue("@Employee_Photo", Photo);
        cmd.Parameters.AddWithValue("@ID", txtID.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "Your details have been saved successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        GridViewdata.Show();


        //}
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void btnexporttoexel_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        grdnewjoininglist.RenderControl(htmlWrite);
        grdnewjoininglist.AllowPaging = false;
        Response.Clear();
        string str = "Employeejoiningdata" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        Response.Write(stringWrite.ToString());
        Response.End();

    }

    protected void Search_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("pro_getemployeedata", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@FULL_NAME", txtempname.Text));
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable(); ;
        daCL.Fill(dtCL);
        grdnewjoininglist.DataSource = dtCL;
        grdnewjoininglist.DataBind();
    }

    private DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString()))
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
    protected void lnkbutton_Click(object sender, EventArgs e)
    {

        GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
        int index = row.RowIndex;
        Label UserId = (Label)grdnewjoininglist.Rows[index].FindControl("lblemployeecode");


        byte[] bytes = GetData("select [Employee_Photo] as FacultyImage from [HRMSPortal].[dbo].[Employee_Joining_Table] where [ID]='" + UserId.Text + "'").Rows[0]["FacultyImage"].ToString() == "" ? null : (byte[])GetData("select Employee_Photo as FacultyImage from [HRMSPortal].[dbo].[Employee_Joining_Table] where [ID]='" + UserId.Text + "'").Rows[0]["FacultyImage"];
        if (bytes != null)
        {
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            ImgPrv.ImageUrl = "data:image/png;base64," + base64String;
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "select * from [HRMSPortal].[dbo].Employee_Joining_Table WHERE [ID]='" + UserId.Text + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();

        drptitle.SelectedItem.Text = dt.Rows[0]["TITLE"].ToString();
        txtemployeename.Text = dt.Rows[0]["FULL_NAME"].ToString();
        drpemployeetype.SelectedItem.Text = dt.Rows[0]["EMPLOYEE_TYPE"].ToString();
        drpnatureofappoint.SelectedItem.Text = dt.Rows[0]["NATURE_OF_APPOINTMENT"].ToString();
        drpunitcollege.SelectedItem.Text = dt.Rows[0]["UNIT_COLLEGE_DEPARTMENT"].ToString();
        txtdept.Text = dt.Rows[0]["DEPT_IF_ANY"].ToString();
        txtsubdept.Text = dt.Rows[0]["SUB_DEPT_IF_ANY"].ToString();
        txtdateofjoining.Text = dt.Rows[0]["DATE_OF_JOINING"].ToString();
        txtdesignation.Text = dt.Rows[0]["DESIGNATION"].ToString();
        txtdateofbirth.Text = dt.Rows[0]["DATE_OF_BIRTH"].ToString();
        txtfathername.Text = dt.Rows[0]["FATHER_NAME"].ToString();
        txtmothername.Text = dt.Rows[0]["MOTHER_NAME"].ToString();
        drpgender.SelectedItem.Text = dt.Rows[0]["GENDER"].ToString();
        drpmaritalstatus.SelectedItem.Text = dt.Rows[0]["MARITAL_STATUS"].ToString();
        txtspousename.Text = dt.Rows[0]["Spous_Name"].ToString();
        drpspouseprofession.SelectedItem.Text = dt.Rows[0]["SPOUSE_PROFESSION"].ToString();
        txtmobileno.Text = dt.Rows[0]["MOBILE_NO"].ToString();
        txtaltmobileno.Text = dt.Rows[0]["ALTER_MOBILE_NO"].ToString();
        txtemailid.Text = dt.Rows[0]["E_MAIL_ID"].ToString();
        drpreligion.SelectedItem.Text = dt.Rows[0]["RELIGION"].ToString();
        drpcategory.SelectedItem.Text = dt.Rows[0]["CATEGORY"].ToString();
        txtbloodgroup.Text = dt.Rows[0]["BOOLD_GROUP"].ToString();
        drpphysicallychallenged.SelectedItem.Text = dt.Rows[0]["PHYSICALLY_CHALLENGED"].ToString();
        txtnationality.Text = dt.Rows[0]["NATIONALITY"].ToString();
        drpState.SelectedItem.Text = dt.Rows[0]["DOMICILE_STATE"].ToString();
        txtvoterid.Text = dt.Rows[0]["VOTER_ID_NO"].ToString();
        txtdrivinglice.Text = dt.Rows[0]["DRIVING_LICENSE_NO"].ToString();
        txtpassport.Text = dt.Rows[0]["PASSPORT_NO"].ToString();
        txtvalidupto.Text = dt.Rows[0]["VALID_UP_TO"].ToString();
        txtpanno.Text = dt.Rows[0]["PAN"].ToString();
        txtaadharno.Text = dt.Rows[0]["AADHAR_NO"].ToString();
        txtemergencyname.Text = dt.Rows[0]["EMER_CON_PERSON_NAME"].ToString();
        txtemergenmobile.Text = dt.Rows[0]["EMER_MOBILE_NO"].ToString();
        txtemergencymail.Text = dt.Rows[0]["EMER_E_MAIL_ID"].ToString();
        drprelationship.SelectedItem.Text = dt.Rows[0]["EMER_RELATIONSHIP"].ToString();
        txtpreaddress.Text = dt.Rows[0]["PRESENT_ADDRESS"].ToString();
        txtprecity.Text = dt.Rows[0]["PRE_CITY"].ToString();
        txtpredisatrict.Text = dt.Rows[0]["PRE_DISTRICT"].ToString();
        txtprepostcode.Text = dt.Rows[0]["PRE_POST_CODE"].ToString();
        txtpermentadd.Text = dt.Rows[0]["PER_ADDRESS"].ToString();
        txtpercity.Text = dt.Rows[0]["PER_CITY"].ToString();
        txtperdistrict.Text = dt.Rows[0]["PER_DISTRICT"].ToString();
        txtperpostcode.Text = dt.Rows[0]["PER_POST_CODE"].ToString();
        txtID.Text = dt.Rows[0]["ID"].ToString();
        GridViewdata.Show();
        Button1.Visible = false;
        Button2.Visible = true;

    }

    public void ValidateDate()
    {

        DateTime joindatecom = DateTime.ParseExact(txtdateofjoining.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime birthdatecom = DateTime.ParseExact(txtdateofbirth.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //DateTime frodatecom = DateTime.ParseExact(txtFromDate.Text.Trim() + " " + txtFromTime.Text.Trim(), "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
        //DateTime Todatecom = DateTime.ParseExact(txtTodate.Text.Trim() + " " + txtToTime.Text.Trim(), "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
        if (birthdatecom > joindatecom)
        {
            txtdateofbirth.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert(' Joining Date should always be greater than Date Of Birth');", true);

        }
        else
        {


        }

    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        GridViewdata.Hide();
    }

    protected void btn_addnew_Click(object sender, EventArgs e)
    {
        GridViewdata.Show();
        Button2.Visible = false;
        Button1.Visible = true;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        //if (drpState.SelectedItem.Text == "--Select Employee State--")
        //{
        //    string message1 = "Please Fill Employee State.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        //if (drprelationship.SelectedItem.Text == "--Select Employee Relaion--")
        //{
        //    string message1 = "Please Fill Employee Relaionship.";
        //    string script1 = "window.onload = function(){ alert('";
        //    script1 += message1;
        //    script1 += "')};";
        //    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //    GridViewdata.Show();
        //    return;
        //}
        string contentType1 = "";
        byte[] Photo = new byte[0];
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
        }
        SqlCommand cmd = new SqlCommand("pro_insertjoiningdata", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@TITLE", drptitle.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@FULL_NAME", txtemployeename.Text);
        cmd.Parameters.AddWithValue("@EMPLOYEE_TYPE", drpemployeetype.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@NATURE_OF_APPOINTMENT", drpnatureofappoint.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@UNIT_COLLEGE_DEPARTMENT", drpunitcollege.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@DEPT_IF_ANY", txtdept.Text);
        cmd.Parameters.AddWithValue("@SUB_DEPT_IF_ANY", txtsubdept.Text);
        cmd.Parameters.AddWithValue("@DATE_OF_JOINING", txtdateofjoining.Text);
        cmd.Parameters.AddWithValue("@DESIGNATION", txtdesignation.Text);
        cmd.Parameters.AddWithValue("@DATE_OF_BIRTH", txtdateofbirth.Text);
        cmd.Parameters.AddWithValue("@FATHER_NAME", txtfathername.Text);
        cmd.Parameters.AddWithValue("@MOTHER_NAME", txtmothername.Text);
        cmd.Parameters.AddWithValue("@GENDER", drpgender.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@MARITAL_STATUS", drpmaritalstatus.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@SPOUSE_PROFESSION", drpspouseprofession.SelectedItem.Text);
        cmd.Parameters.AddWithValue("Spous_Name", txtspousename.Text);
        cmd.Parameters.AddWithValue("@MOBILE_NO", txtmobileno.Text);
        cmd.Parameters.AddWithValue("@ALTER_MOBILE_NO", txtaltmobileno.Text);
        cmd.Parameters.AddWithValue("@E_MAIL_ID", txtemailid.Text);
        cmd.Parameters.AddWithValue("@RELIGION", drpreligion.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@CATEGORY", drpcategory.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@BOOLD_GROUP", txtbloodgroup.Text);
        cmd.Parameters.AddWithValue("@PHYSICALLY_CHALLENGED", drpphysicallychallenged.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@NATIONALITY", txtnationality.Text);
        cmd.Parameters.AddWithValue("@DOMICILE_STATE", drpState.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@VOTER_ID_NO", txtvoterid.Text);
        cmd.Parameters.AddWithValue("@DRIVING_LICENSE_NO", txtdrivinglice.Text);
        cmd.Parameters.AddWithValue("@PASSPORT_NO", txtpassport.Text);
        cmd.Parameters.AddWithValue("@VALID_UP_TO", txtvalidupto.Text);
        cmd.Parameters.AddWithValue("@PAN", txtpanno.Text);
        cmd.Parameters.AddWithValue("@AADHAR_NO", txtaadharno.Text);
        cmd.Parameters.AddWithValue("@EMER_CON_PERSON_NAME", txtemergencyname.Text);
        cmd.Parameters.AddWithValue("@EMER_MOBILE_NO", txtemergenmobile.Text);
        cmd.Parameters.AddWithValue("@EMER_E_MAIL_ID", txtemergencymail.Text);
        cmd.Parameters.AddWithValue("@EMER_RELATIONSHIP", drprelationship.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@PRESENT_ADDRESS", txtpreaddress.Text);
        cmd.Parameters.AddWithValue("@PRE_CITY", txtprecity.Text);
        cmd.Parameters.AddWithValue("@PRE_DISTRICT", txtpredisatrict.Text);
        cmd.Parameters.AddWithValue("@PRE_POST_CODE", txtprepostcode.Text);
        cmd.Parameters.AddWithValue("@PER_ADDRESS", txtpermentadd.Text);
        cmd.Parameters.AddWithValue("@PER_CITY", txtpercity.Text);
        cmd.Parameters.AddWithValue("@PER_DISTRICT", txtperdistrict.Text);
        cmd.Parameters.AddWithValue("@PER_POST_CODE", txtperpostcode.Text);
        cmd.Parameters.AddWithValue("@Employee_Photo", Photo);
        cmd.Parameters.AddWithValue("@ID", txtID.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "Your details have been Update successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        GridViewdata.Show();
    }
  public void loginpage()
    {
        if (Session["uid"].ToString() == "TMU00310")
        {
            btn_addnew.Visible = true;
            BtnRejected.Visible = true;
            grdnewjoininglist.Columns[16].Visible = true;
            grdnewjoininglist.Columns[3].Visible = true;
        }
        if (Session["uid"].ToString() == "TMU00075" )
        {
            btn_addnew.Visible = false;
            BtnRejected.Visible = false;
            grdnewjoininglist.Columns[16].Visible = false;
            grdnewjoininglist.Columns[3].Visible = true;
        }
        if (Session["uid"].ToString() == "TMU00049")
        {
            btn_addnew.Visible = false;
            BtnRejected.Visible = false;
            grdnewjoininglist.Columns[16].Visible = false;
            grdnewjoininglist.Columns[3].Visible = false; 
        }
    }


    protected void drpmaritalstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpmaritalstatus.SelectedItem.Text == "Single")
        {
            txtspousename.Enabled = false;
            drpspouseprofession.Enabled = false;
            GridViewdata.Show();
        }
        else
        {
            txtspousename.Enabled = true;
            drpspouseprofession.Enabled = true;
            GridViewdata.Show();
        }
    }

    protected void txtdateofjoining_TextChanged(object sender, EventArgs e)
    {
        GridViewdata.Show();
        try
        {
            ValidateDate();
            GridViewdata.Show();
        }
        catch (Exception)
        { }
    }

    protected void txtdateofbirth_TextChanged(object sender, EventArgs e)
    {
        GridViewdata.Show();
        try
        {
            ValidateDate();
            GridViewdata.Show();
        }
        catch (Exception)
        { }

    }
  protected void BtnRejected_Click(object sender, EventArgs e)
    {
        try
        {
            int i = 0;
            foreach (GridViewRow row in grdnewjoininglist.Rows)
            {
                CheckBox check = (CheckBox)row.FindControl("Chkemployee");
                Label lblemployeecode = (Label)row.FindControl("lblemployeecode");
                var id = grdnewjoininglist.DataKeys[row.RowIndex].Value;
                SqlCommand cmd = new SqlCommand("pro_DeleteEmployeeform", con);
                if (check.Checked == true)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", lblemployeecode.Text);
                    cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    i++;
                }
            }
            if (i > 0) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true); }
            else { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Data.. !')", true); }
            getemployeedetail();

        }
        catch (Exception ex)
        {
        }
    }

    //    protected void btnUpload_Click(object sender, EventArgs e)
    //    {

    //        string folderPath = Server.MapPath("~/Files/");

    //        //Check whether Directory (Folder) exists.
    //        if (!Directory.Exists(folderPath))
    //        {
    //            //If Directory (Folder) does not exists Create it.
    //            Directory.CreateDirectory(folderPath);
    //        }

    //        //Save the File to the Directory (Folder).
    //        FileUpload2.SaveAs(folderPath + Path.GetFileName(FileUpload2.FileName));

    //        //Display the Picture in Image control.
    //        Image3.ImageUrl = "~/Files/" + Path.GetFileName(FileUpload2.FileName);
    //    }

    //protected void btnexporttopdf_Click(object sender, EventArgs e)
    //{
    //    ExportGridToPDF();
    //}

    //private void ExportGridToPDF()
    //{

    //    Response.ContentType = "application/pdf";
    //    Response.AddHeader("content-disposition", "attachment;filename=Employee_New_Joining.pdf");
    //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //    StringWriter sw = new StringWriter();
    //    HtmlTextWriter hw = new HtmlTextWriter(sw);
    //    grdnewjoininglist.RenderControl(hw);
    //    StringReader sr = new StringReader(sw.ToString());
    //    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
    //    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
    //    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
    //    pdfDoc.Open();
    //    htmlparser.Parse(sr);
    //    pdfDoc.Close();
    //    Response.Write(pdfDoc);
    //    Response.End();
    //    grdnewjoininglist.AllowPaging = true;
    //    grdnewjoininglist.DataBind();
    //}
  protected void grdnewjoininglist_PageIndexChanging(object sender, GridViewPageEventArgs e)
  {
      grdnewjoininglist.PageIndex = e.NewPageIndex;
      getemployeedetail();
  }
 
}


