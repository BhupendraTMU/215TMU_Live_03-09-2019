using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.ServiceModel;

public partial class Faculty_TeacherDairy : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //if (!IsPostBack)
            //{
            ////Profiledata();
            ////GetResearchProject();
            ////GetPhdGuidance();
            ////GetPatent();
            ////GetAwardDetails();
            ////GetMembershipDetails();
            ////GetResearchpaper();
            ////GetBookPublishedDetails();
            ////GetTechniquesDeveloped();
            ////GetSeminarsAndConferences();
            ////GetAdditionalActivities();
            ////GetAcademicAssignments();
            ////GetStudentProjectsOngoing();


            //  }
            txtacademicsession.Text = drpacademicsession.Text;
            GetTeacherLoad();
          //  BindTable(Session["uid"].ToString(), drpacademicsession.SelectedValue);
            //BindAttendance(Session["uid"].ToString(), drpacademicsession.SelectedValue);
            if (!IsPostBack)
            {
                PopulateSemesters(drpsemester.SelectedValue);
            }


        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }

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
    public void Profiledata()
    {
        try

        {
            byte[] bytes = GetData("select Picture as FacultyImage from [EDUCOLLEGELIVE-R2].dbo.TMU$Employee  WHERE [No_] = '" + Session["uid"].ToString() + "'").Rows[0]["FacultyImage"].ToString() == "" ? null : (byte[])GetData("select Picture as FacultyImage from [EDUCOLLEGELIVE-R2].dbo.TMU$Employee  WHERE [No_] = '" + Session["uid"].ToString() + "'").Rows[0]["FacultyImage"];
            if (bytes != null)
            {
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                ImgPrv.ImageUrl = "data:image/png;base64," + base64String;
            }
        }
        catch (Exception ex)
        {

        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        string strSQL = "select * from TMU$Employee where [No_]='" + Session["uid"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        txtFName.Text = dt.Rows[0]["Full Name"].ToString();
        txtFacultyCode.Text = dt.Rows[0]["No_"].ToString();
        txtDepartment.Text = dt.Rows[0]["Global Dimension 1 Code"].ToString();
        txtdesignaton.Text = dt.Rows[0]["Job Title_Grade Desc"].ToString();
        txtcollegeFaculty.Text = dt.Rows[0]["Branch Name"].ToString();
        txtPrograme.Text = dt.Rows[0]["Academic Qualification"].ToString();
        txtcontactno.Text = dt.Rows[0]["Mobile Phone No_"].ToString();
        txtemailid.Text = dt.Rows[0]["E-Mail"].ToString();
        txtcollegetheory.Text = dt.Rows[0]["Branch Name"].ToString();
        txtlabcollege.Text = dt.Rows[0]["Branch Name"].ToString();
        //txtdateofbirth.Text = dt.Rows[0]["Birth Date"].ToString();
        // Assuming "Birth Date" contains a valid DateTime value
        DateTime birthDate = Convert.ToDateTime(dt.Rows[0]["Birth Date"]);
        txtdateofbirth.Text = birthDate.ToString("dd-MM-yyyy"); // Format as "MM/dd/yyyy" or use another format like "dd/MM/yyyy"

        //txtdateofjoining.Text = dt.Rows[0]["Employment Date"].ToString();
        DateTime joiningdate = Convert.ToDateTime(dt.Rows[0]["Employment Date"]);
        txtdateofjoining.Text = joiningdate.ToString("dd-MM-yyyy"); // Format as "MM/dd/yyyy" or use another format like "dd/MM/yyyy"


        txtAreaofSpecilization.Text = dt.Rows[0]["Specialization"].ToString();
    }



    public void EmpolyeeProfiledata()
    {

        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        string strSQL = "select * from EmployeeProfile where [EmployeeCode]='" + Session["uid"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            txtprofileid.Text = dt.Rows[0]["ID"].ToString();
            txtteachingwithintmu.Text = dt.Rows[0]["TeachingWithinTMU"].ToString();
            txtteachingoutsidetmu.Text = dt.Rows[0]["TeachingOutsideTMU"].ToString();
            txtresearch.Text = dt.Rows[0]["Research"].ToString();
            txtindustry.Text = dt.Rows[0]["Industry"].ToString();
            txtothers.Text = dt.Rows[0]["Others"].ToString();
            //TextBox1.Text = dt.Rows[0]["TotalPublications"].ToString();

            DateTime TotalPublications = Convert.ToDateTime(dt.Rows[0]["TotalPublications"]);
            TextBox1.Text = TotalPublications.ToString("dd-MM-yyyy");

            txthindex.Text = dt.Rows[0]["FacultyIndex"].ToString();
            txti10index.Text = dt.Rows[0]["FacultyIndex1"].ToString();
            txtcitations.Text = dt.Rows[0]["Citations"].ToString();
        }
    }


    public void GetResearchProject()
    {

        SqlCommand cmd = new SqlCommand("Pro_GetResearchProject", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdresearchproject.DataSource = dtCL;
        grdresearchproject.DataBind();

    }

    public void GetPhdGuidance()
    {

        SqlCommand cmd = new SqlCommand("Pro_GetPhdGuidance", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdphdguidence.DataSource = dtCL;
        grdphdguidence.DataBind();

    }

    public void GetPatent()
    {

        SqlCommand cmd = new SqlCommand("Pro_GetPatent", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdpatents.DataSource = dtCL;
        grdpatents.DataBind();

    }
    public void GetAwardDetails()
    {

        SqlCommand cmd = new SqlCommand("Pro_GetAwardDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdaward.DataSource = dtCL;
        grdaward.DataBind();

    }
    public void GetMembershipDetails()
    {

        SqlCommand cmd = new SqlCommand("Pro_GetMembershipDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdmembership.DataSource = dtCL;
        grdmembership.DataBind();

    }
    public void GetResearchpaper()
    {

        SqlCommand cmd = new SqlCommand("Pro_GetResearchpaper", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdresearch.DataSource = dtCL;
        grdresearch.DataBind();

    }

    public void GetBookPublishedDetails()
    {

        SqlCommand cmd = new SqlCommand("Pro_GetBookPublishedDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdbookpublished.DataSource = dtCL;
        grdbookpublished.DataBind();

    }
    public void GetSeminarsAndConferences()
    {

        SqlCommand cmd = new SqlCommand("Pro_GetSeminarsAndConferences", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdSeminar.DataSource = dtCL;
        grdSeminar.DataBind();

    }

    public void GetAdditionalActivities()
    {

        SqlCommand cmd = new SqlCommand("Pro_AdditionalActivities", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdrecordofadditional.DataSource = dtCL;
        grdrecordofadditional.DataBind();

    }
    public void GetTechniquesDeveloped()
    {

        SqlCommand cmd = new SqlCommand("Pro_TechniquesDeveloped", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdtechniques.DataSource = dtCL;
        grdtechniques.DataBind();
    }
    public void GetAcademicAssignments()
    {

        SqlCommand cmd = new SqlCommand("Pro_AcademicAssignments", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdAcademicAdministrative.DataSource = dtCL;
        grdAcademicAdministrative.DataBind();

    }
    public void GetStudentProjectsOngoing()
    {

        SqlCommand cmd = new SqlCommand("Pro_GetStudentProjectsOngoing", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdstudentproject.DataSource = dtCL;
        grdstudentproject.DataBind();

    }
    public void GetTeacherLoad()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Pro_TeacherLoad", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
            cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlDataAdapter daCL = new SqlDataAdapter(cmd);
            DataTable dtCL = new DataTable();
            daCL.Fill(dtCL);
            totalL = 0;
            totalT = 0;
            totalP = 0;
            totalC = 0;
            grdteacherload.DataSource = dtCL;
            grdteacherload.DataBind();
        }
        catch (Exception ex)
        {
            Response.Redirect("../Default.aspx");
        }
    }
    public void LessionPlanSummaryunitwise()
    {
        SqlCommand cmd = new SqlCommand("Pro_LessionPlanSummaryunitwise", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdlessionplanunitwise.DataSource = dtCL;
        grdlessionplanunitwise.DataBind();
    }

    protected void btnResearchproject_Click(object sender, EventArgs e)
    {
        GridViewDetails.Show();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GridViewDetails.Hide();

        pnlGridViewDetails.Style["display"] = "none";
    }

    protected void btnGuidance_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Hide();
    }

    protected void btnPatents_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Show();
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Hide();
    }

    protected void btnAwardsrecognition_Click(object sender, EventArgs e)
    {
        ModalPopupExtender3.Show();
    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        ModalPopupExtender3.Hide();
    }

    protected void btnmembership_Click(object sender, EventArgs e)
    {
        ModalPopupExtender4.Show();
    }

    protected void Button8_Click(object sender, EventArgs e)
    {
        ModalPopupExtender4.Hide();
    }

    protected void btnResearch_Click(object sender, EventArgs e)
    {
        ModalPopupExtender5.Show();
    }

    protected void Button10_Click(object sender, EventArgs e)
    {
        ModalPopupExtender5.Hide();
    }

    protected void btnbookpublished_Click(object sender, EventArgs e)
    {
        ModalPopupExtender6.Show();
    }

    protected void Button12_Click(object sender, EventArgs e)
    {
        ModalPopupExtender6.Hide();
    }

    protected void btnseminars_Click(object sender, EventArgs e)
    {
        ModalPopupExtender7.Show();
    }

    protected void Button14_Click(object sender, EventArgs e)
    {
        ModalPopupExtender7.Hide();
    }

    protected void btntrecord_Click(object sender, EventArgs e)
    {
        ModalPopupExtender8.Show();
    }

    protected void Button16_Click(object sender, EventArgs e)
    {
        ModalPopupExtender8.Hide();
    }

    protected void btnstudentproject_Click(object sender, EventArgs e)
    {
        ModalPopupExtender9.Show();
    }

    protected void Button19_Click(object sender, EventArgs e)
    {

        ModalPopupExtender9.Hide();
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("Pro_InsertResearchProject", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtid.Text);
        cmd.Parameters.AddWithValue("@ProjectTitle", txtprojecttitle.Text);
        cmd.Parameters.AddWithValue("@AmountSanctionedApplied", txtamountsantioned.Text);
        cmd.Parameters.AddWithValue("@Date", txtDate.Text);
        cmd.Parameters.AddWithValue("@FundingAgency", txtfundingagency.Text);
        cmd.Parameters.AddWithValue("@FundingBody", txtfundibodynd.Text);
        cmd.Parameters.AddWithValue("@Status", drpstatus.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@EmployeeCode", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@EmployeeName", Session["uname"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save successfully.'); document.location.href='TeacherDairy.aspx';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been saved successfully.');", true);
        GetResearchProject();
        GridViewDetails.Hide();
        txtprojecttitle.Text = "";
        txtamountsantioned.Text = "";
        txtDate.Text = "";
        txtfundingagency.Text = "";
        txtfundibodynd.Text = "";
        drpstatus.SelectedItem.Text = "";
        txtid.Text = "";
    }

    protected void btnselect_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        GridViewRow row = btn.NamingContainer as GridViewRow;
        string pk = grdresearchproject.DataKeys[row.RowIndex].Values[0].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "Select * from ResearchProject WHERE [ID]='" + pk + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        txtprojecttitle.Text = dt.Rows[0]["ProjectTitle"].ToString();
        txtamountsantioned.Text = dt.Rows[0]["AmountSanctionedApplied"].ToString();
        txtDate.Text = dt.Rows[0]["Date"].ToString();
        txtfundingagency.Text = dt.Rows[0]["FundingAgency"].ToString();
        txtfundibodynd.Text = dt.Rows[0]["FundingBody"].ToString();
        drpstatus.SelectedItem.Text = dt.Rows[0]["Status"].ToString();
        txtid.Text = dt.Rows[0]["ID"].ToString();

        GridViewDetails.Show();
    }

    protected void btnphdguidance_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("Pro_InsertPhdGuidance", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtphdid.Text);
        cmd.Parameters.AddWithValue("@ScholarName", txtnameofscholars.Text);
        cmd.Parameters.AddWithValue("@RegistrationDate", txtdateofregistration.Text);
        cmd.Parameters.AddWithValue("@PhDTopic", txtphdtopic.Text);
        cmd.Parameters.AddWithValue("@CurrentStatus", drpcurrentstatus.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@EmployeeCode", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@EmployeeName", Session["uname"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save successfully.'); document.location.href='TeacherDairy.aspx';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been saved successfully.');", true);
        GetPhdGuidance();
        ModalPopupExtender1.Hide();
        txtnameofscholars.Text = "";
        txtdateofregistration.Text = "";
        txtphdtopic.Text = "";
        txtphdid.Text = "";
    }

    protected void btnselect_Click1(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        GridViewRow row = btn.NamingContainer as GridViewRow;
        string pk = grdphdguidence.DataKeys[row.RowIndex].Values[0].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "Select * from PhdGuidance WHERE [ID]='" + pk + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        txtnameofscholars.Text = dt.Rows[0]["ScholarName"].ToString();
        txtdateofregistration.Text = dt.Rows[0]["RegistrationDate"].ToString();
        txtphdtopic.Text = dt.Rows[0]["PhDTopic"].ToString();
        drpcurrentstatus.SelectedItem.Text = dt.Rows[0]["CurrentStatus"].ToString();
        txtphdid.Text = dt.Rows[0]["ID"].ToString();
        ModalPopupExtender1.Show();
    }

    protected void btnpatent_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("Pro_InsertPatentDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtidpatent.Text);
        cmd.Parameters.AddWithValue("@PatentTitle", txttitleofpatent.Text);
        cmd.Parameters.AddWithValue("@FiledPublishedGrantDate", txtdateoffiled.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarkpatent.Text);
        cmd.Parameters.AddWithValue("@EmployeeCode", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@EmployeeName", Session["uname"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save successfully.'); document.location.href='TeacherDairy.aspx';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been saved successfully.');", true);
        GetPatent();
        ModalPopupExtender2.Hide();
        txttitleofpatent.Text = "";
        txtdateoffiled.Text = "";
        txtremarkpatent.Text = "";
        txtidpatent.Text = "";
    }

    protected void btnPatent_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        GridViewRow row = btn.NamingContainer as GridViewRow;
        string pk = grdpatents.DataKeys[row.RowIndex].Values[0].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "Select * from PatentDetails WHERE [ID]='" + pk + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        txttitleofpatent.Text = dt.Rows[0]["PatentTitle"].ToString();
        txtdateoffiled.Text = dt.Rows[0]["FiledPublishedGrantDate"].ToString();
        txtremarkpatent.Text = dt.Rows[0]["Remark"].ToString();
        txtidpatent.Text = dt.Rows[0]["ID"].ToString();
        ModalPopupExtender2.Show();
    }

    protected void btnaward_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("Pro_InsertAwardDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtidawarded.Text);
        cmd.Parameters.AddWithValue("@AwardDate", txtdateAward.Text);
        cmd.Parameters.AddWithValue("@InternalAwardsTitle", txtinternalaward.Text);
        cmd.Parameters.AddWithValue("@ExternalAwardsTitle", txtexternalaward.Text);
        cmd.Parameters.AddWithValue("@Remark", txtremarksaward.Text);
        cmd.Parameters.AddWithValue("@EmployeeCode", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@EmployeeName", Session["uname"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save successfully.'); document.location.href='TeacherDairy.aspx';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been saved successfully.');", true);
        GetAwardDetails();
        ModalPopupExtender3.Hide();
        txtdateAward.Text = "";
        txtinternalaward.Text = "";
        txtexternalaward.Text = "";
        txtremarksaward.Text = "";
        txtidawarded.Text = "";
    }

    protected void btnaward_Click1(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        GridViewRow row = btn.NamingContainer as GridViewRow;
        string pk = grdaward.DataKeys[row.RowIndex].Values[0].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "Select * from AwardDetails WHERE [ID]='" + pk + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        txtdateAward.Text = dt.Rows[0]["AwardDate"].ToString();
        txtinternalaward.Text = dt.Rows[0]["InternalAwardsTitle"].ToString();
        txtexternalaward.Text = dt.Rows[0]["ExternalAwardsTitle"].ToString();
        txtremarksaward.Text = dt.Rows[0]["Remark"].ToString();
        txtidawarded.Text = dt.Rows[0]["ID"].ToString();
        ModalPopupExtender3.Show();
    }

    protected void btnmemberprofessional_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("Pro_InsertMembershipDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtidmembership.Text);
        cmd.Parameters.AddWithValue("@MembershipDuration", txtmembership.Text);
        cmd.Parameters.AddWithValue("@ProfessionalBodyName", txtnameofprofessional.Text);
        cmd.Parameters.AddWithValue("@MemberType", drptypemember.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@MembershipNumber", txtmembershipnumber.Text);
        cmd.Parameters.AddWithValue("@EmployeeCode", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@EmployeeName", Session["uname"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save successfully.'); document.location.href='TeacherDairy.aspx';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been saved successfully.');", true);
        GetMembershipDetails();
        ModalPopupExtender4.Hide();
        txtmembership.Text = "";
        txtnameofprofessional.Text = "";
        txtmembershipnumber.Text = "";
        txtidmembership.Text = "";
    }

    protected void btnmemberprofessional_Click1(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        GridViewRow row = btn.NamingContainer as GridViewRow;
        string pk = grdmembership.DataKeys[row.RowIndex].Values[0].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "Select * from MembershipDetails WHERE [ID]='" + pk + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        txtmembership.Text = dt.Rows[0]["MembershipDuration"].ToString();
        txtnameofprofessional.Text = dt.Rows[0]["ProfessionalBodyName"].ToString();
        drptypemember.SelectedItem.Text = dt.Rows[0]["MemberType"].ToString();
        txtmembershipnumber.Text = dt.Rows[0]["MembershipNumber"].ToString();
        txtidmembership.Text = dt.Rows[0]["ID"].ToString();
        ModalPopupExtender4.Show();
    }

    protected void btnResearchPaper_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("Pro_InsertResearchPaperBook", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtidResearchPaper.Text);
        cmd.Parameters.AddWithValue("@TitleOfPaperOrBookChapter", txttitleofpaperbook.Text);
        cmd.Parameters.AddWithValue("@DetailsofJournal", txtdetailofjournal.Text);
        cmd.Parameters.AddWithValue("@DetailsofIndexing", txtdetailofindexing.Text);
        cmd.Parameters.AddWithValue("@EmployeeCode", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@EmployeeName", Session["uname"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save successfully.'); document.location.href='TeacherDairy.aspx';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been saved successfully.');", true);
        GetResearchpaper();
        ModalPopupExtender5.Hide();
        txttitleofpaperbook.Text = "";
        txtdetailofjournal.Text = "";
        txtdetailofindexing.Text = "";
        txtidResearchPaper.Text = "";

    }

    protected void btnResearch_Click1(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        GridViewRow row = btn.NamingContainer as GridViewRow;
        string pk = grdresearch.DataKeys[row.RowIndex].Values[0].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "Select * from Researchpaper WHERE [ID]='" + pk + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        txttitleofpaperbook.Text = dt.Rows[0]["TitleOfPaperOrBookChapter"].ToString();
        txtdetailofjournal.Text = dt.Rows[0]["DetailsofJournal"].ToString();
        txtdetailofindexing.Text = dt.Rows[0]["DetailsofIndexing"].ToString();
        txtidResearchPaper.Text = dt.Rows[0]["ID"].ToString();
        ModalPopupExtender5.Show();
    }

    protected void btnbookpublisheddetail_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("Pro_InsertBookPublishedDetail", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtidbookpublished.Text);
        cmd.Parameters.AddWithValue("@TitleOfBook", txttitleofbook.Text);
        cmd.Parameters.AddWithValue("@Authors", txtauthors.Text);
        cmd.Parameters.AddWithValue("@PublishingMonthAndYear", txtpublishingmonthyear.Text);
        cmd.Parameters.AddWithValue("@PublisherDetails", txtdetailofpublisher.Text);
        cmd.Parameters.AddWithValue("@ISBN", txtisbn.Text);
        cmd.Parameters.AddWithValue("@EmployeeCode", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@EmployeeName", Session["uname"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save successfully.'); document.location.href='TeacherDairy.aspx';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been saved successfully.');", true);
        GetBookPublishedDetails();
        ModalPopupExtender6.Hide();
        txttitleofbook.Text = "";
        txtauthors.Text = "";
        txtpublishingmonthyear.Text = "";
        txtdetailofpublisher.Text = "";
        txtisbn.Text = "";
        txtidbookpublished.Text = "";
    }

    protected void btnbookpublisheddetail_Click1(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        GridViewRow row = btn.NamingContainer as GridViewRow;
        string pk = grdbookpublished.DataKeys[row.RowIndex].Values[0].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "Select * from BookPublishedDetails WHERE [ID]='" + pk + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        txttitleofbook.Text = dt.Rows[0]["TitleOfBook"].ToString();
        txtauthors.Text = dt.Rows[0]["Authors"].ToString();
        txtpublishingmonthyear.Text = dt.Rows[0]["PublishingMonthAndYear"].ToString();
        txtdetailofpublisher.Text = dt.Rows[0]["PublisherDetails"].ToString();
        txtisbn.Text = dt.Rows[0]["ISBN"].ToString();
        txtidbookpublished.Text = dt.Rows[0]["ID"].ToString();
        ModalPopupExtender6.Show();
    }

    protected void btnseminar_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("Pro_InsertSeminarsAndConferences", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtidseminar.Text);
        cmd.Parameters.AddWithValue("@EventName", txtnameofevent.Text);
        cmd.Parameters.AddWithValue("@Title", txttitleseminar.Text);
        cmd.Parameters.AddWithValue("@OrganizingInstitute", txtorganizinginstitute.Text);
        cmd.Parameters.AddWithValue("@DurationOfProgramme", txtdurationofprogram.Text);
        cmd.Parameters.AddWithValue("@ExpertOrParticipatedOrPaperPresented", txtexpertparticipated.Text);
        cmd.Parameters.AddWithValue("@Date", txtdateseminar.Text);
        cmd.Parameters.AddWithValue("@EmployeeCode", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@EmployeeName", Session["uname"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save successfully.'); document.location.href='TeacherDairy.aspx';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been saved successfully.');", true);
        GetSeminarsAndConferences();
        ModalPopupExtender7.Hide();

        txtnameofevent.Text = "";
        txttitleseminar.Text = "";
        txtorganizinginstitute.Text = "";
        txtdurationofprogram.Text = "";
        txtexpertparticipated.Text = "";
        txtdateseminar.Text = "";
    }

    protected void btnseminarscon_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        GridViewRow row = btn.NamingContainer as GridViewRow;
        string pk = grdSeminar.DataKeys[row.RowIndex].Values[0].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "Select * from SeminarsAndConferences WHERE [ID]='" + pk + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        txtnameofevent.Text = dt.Rows[0]["EventName"].ToString();
        txttitleseminar.Text = dt.Rows[0]["Title"].ToString();
        txtorganizinginstitute.Text = dt.Rows[0]["OrganizingInstitute"].ToString();
        txtdurationofprogram.Text = dt.Rows[0]["DurationOfProgramme"].ToString();
        txtexpertparticipated.Text = dt.Rows[0]["ExpertOrParticipatedOrPaperPresented"].ToString();
        txtdateseminar.Text = dt.Rows[0]["Date"].ToString();
        txtidseminar.Text = dt.Rows[0]["ID"].ToString();
        ModalPopupExtender7.Show();
    }

    protected void btnadditional_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("Pro_InsertAdditionalActivities", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtidadditional.Text);
        cmd.Parameters.AddWithValue("@DateandDuration", txtdateandduration.Text);
        cmd.Parameters.AddWithValue("@DetailsofActivity", txtdetailsofactivity.Text);
        cmd.Parameters.AddWithValue("@EmployeeCode", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@EmployeeName", Session["uname"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save successfully.'); document.location.href='TeacherDairy.aspx';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been saved successfully.');", true);
        GetAdditionalActivities();
        ModalPopupExtender8.Hide();
        txtdateandduration.Text = "";
        txtdetailsofactivity.Text = "";
        txtidadditional.Text = "";

    }

    protected void btnAdditionalactivity_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        GridViewRow row = btn.NamingContainer as GridViewRow;
        string pk = grdrecordofadditional.DataKeys[row.RowIndex].Values[0].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "Select * from AdditionalActivities WHERE [ID]='" + pk + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        txtdateandduration.Text = dt.Rows[0]["DateandDuration"].ToString();
        txtdetailsofactivity.Text = dt.Rows[0]["DetailsofActivity"].ToString();
        txtidadditional.Text = dt.Rows[0]["ID"].ToString();
        ModalPopupExtender8.Show();
    }

    protected void btntechniques_Click(object sender, EventArgs e)
    {
        ModalPopupExtender10.Show();
    }

    protected void btnanyother_Click(object sender, EventArgs e)
    {
        ModalPopupExtender11.Show();

    }

    protected void Button23_Click(object sender, EventArgs e)
    {
        ModalPopupExtender11.Hide();
    }

    protected void Button18_Click(object sender, EventArgs e)
    {
        ModalPopupExtender10.Hide();
    }

    protected void btntechdevlopment_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("Pro_InsertTechniquesDevelopedDetail", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtidtechnicaldevlopment.Text);
        cmd.Parameters.AddWithValue("@TechniquesDeveloped", txtTechniquesDeveloped.Text);
        cmd.Parameters.AddWithValue("@EmployeeCode", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@EmployeeName", Session["uname"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save successfully.'); document.location.href='TeacherDairy.aspx';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been saved successfully.');", true);
        GetTechniquesDeveloped();
        ModalPopupExtender10.Hide();
        txtTechniquesDeveloped.Text = "";
        txtidtechnicaldevlopment.Text = "";
    }

    protected void btntechniquesdevlopment_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        GridViewRow row = btn.NamingContainer as GridViewRow;
        string pk = grdtechniques.DataKeys[row.RowIndex].Values[0].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "Select * from TechniquesDeveloped WHERE [ID]='" + pk + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        txtTechniquesDeveloped.Text = dt.Rows[0]["TechniquesDeveloped"].ToString();
        txtidtechnicaldevlopment.Text = dt.Rows[0]["ID"].ToString();
        ModalPopupExtender10.Show();
    }

    protected void btnAcademicAdmin_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("Pro_InsertAcademicAssignmentsDetail", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtidAcademicAdmin.Text);
        cmd.Parameters.AddWithValue("@AcademicAdministrativeAssignments", txtAcademicAdministrativeAssignments.Text);
        cmd.Parameters.AddWithValue("@EmployeeCode", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@EmployeeName", Session["uname"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save successfully.'); document.location.href='TeacherDairy.aspx';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been saved successfully.');", true);
        GetAcademicAssignments();
        ModalPopupExtender11.Hide();
        txtAcademicAdministrativeAssignments.Text = "";
        txtidAcademicAdmin.Text = "";
    }

    protected void btnAcademicAdministrative_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        GridViewRow row = btn.NamingContainer as GridViewRow;
        string pk = grdAcademicAdministrative.DataKeys[row.RowIndex].Values[0].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "Select * from AcademicAssignments WHERE [ID]='" + pk + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        txtAcademicAdministrativeAssignments.Text = dt.Rows[0]["AcademicAdministrativeAssignments"].ToString();
        txtidAcademicAdmin.Text = dt.Rows[0]["ID"].ToString();
        ModalPopupExtender11.Show();
    }



    protected void btnTeacherdairy_Click(object sender, EventArgs e)
    {
        divteacherdairy.Visible = true;
        Profiledata();
        EmpolyeeProfiledata();
        GetResearchProject();
        GetPhdGuidance();
        GetPatent();
        GetAwardDetails();
        GetMembershipDetails();
        GetResearchpaper();
        GetBookPublishedDetails();
        GetTechniquesDeveloped();
        GetSeminarsAndConferences();
        GetAdditionalActivities();
        GetAcademicAssignments();
        GetStudentProjectsOngoing();
        //LessionPlanSummaryunitwise();
        bindDrpCourseList();
        //bindDrpCoursecodeList();
        BindLessonPlans();
        LessionPlanLECTUREWISE();
        LaboratoryWorkPlanLABCOURS();
        //bindDrpSemesterList();
        //GetTeacherLoad();
        // drpsemester.SelectedItem.Text = txtsemteacher.Text;
        //txtsemteacher.Text = drpsemester.SelectedItem.Text;
    }
    public void BindAttendance(string FacultyCode, string academicYear)
    {


        SqlCommand cmd = new SqlCommand("Pro_GetAttendance", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Session", academicYear);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdStudentAttendance.DataSource = dtCL;
        grdStudentAttendance.DataBind();
    }
    public void BindTable(string FacultyCode, string academicYear)
    {
        timeTable.Rows.Clear();


        string Command = "";



        Command = "select * from [EDUCOLLEGELIVE-R2].dbo.[TMU$Time Table Generation - COL] with(NOLOCK) where [Faculty Code]='" + FacultyCode + "' and [Day No] between  1 and 6 and [Hour No] between 1 and 7 and [Academic Year]='" + academicYear + "' ";


        SqlCommand cmd = new SqlCommand(Command, con);
        cmd.CommandTimeout = 240;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        SqlCommand cmd1 = new SqlCommand("select * from [EDUCOLLEGELIVE-R2].dbo.[TMU$Time Table Generation - COL] with(NOLOCK) where [Substitute Faculty Code]='" + FacultyCode + "' and [Day No] between  1 and 6 and [Hour No] between 1 and 7 and [Attendance Date] between dateadd(day,-([EDUCOLLEGELIVE-R2].dbo.fun_GetDayNoMon2Sat(convert(date, getdate()))-1),convert(date, getdate())) and " + @"
                             dateadd(day,7,dateadd(day,-([EDUCOLLEGELIVE-R2].dbo.fun_GetDayNoMon2Sat(convert(date, getdate()))),convert(date, getdate())))", con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);

        TableHeaderRow hr = new TableHeaderRow();
        hr.CssClass = "Bold";
        hr.Height = 30;

        TableCell c1 = new TableCell();
        TableCell c2 = new TableCell();
        TableCell c3 = new TableCell();
        TableCell c4 = new TableCell();
        TableCell c5 = new TableCell();
        TableCell c6 = new TableCell();
        TableCell c7 = new TableCell();
        TableCell c8 = new TableCell();
        TableCell c9 = new TableCell();
        c1.Font.Size = 15;
        c2.Font.Size = 15;
        c3.Font.Size = 15;
        c4.Font.Size = 15;
        c5.Font.Size = 15;
        c6.Font.Size = 15;
        c7.Font.Size = 15;
        c8.Font.Size = 15;
        c9.Font.Size = 15;

        // add below ----ashu
        //TableCell c10 = new TableCell();
        //TableCell c11 = new TableCell();
        //TableCell c12 = new TableCell();
        // add below ----ashu
        c1.Text = "Days";
        c2.Text = "I";
        c3.Text = "II";
        c4.Text = "III";
        c5.Text = "IV";
        c6.Text = "";
        c7.Text = "V";
        c8.Text = "VI";
        c9.Text = "VII";
        // add below ----ashu
        //c10.Text = "VIII";
        //c11.Text = "IX";
        //c12.Text = "X";
        // add below ----ashu
        hr.Cells.Add(c1);
        hr.Cells.Add(c2);
        hr.Cells.Add(c3);
        hr.Cells.Add(c4);
        hr.Cells.Add(c5);
        hr.Cells.Add(c6);
        hr.Cells.Add(c7);
        hr.Cells.Add(c8);
        hr.Cells.Add(c9);
        hr.BackColor = Color.LightYellow;

        // add below ----ashu
        //hr.Cells.Add(c10);
        //hr.Cells.Add(c11);
        //hr.Cells.Add(c12);
        // add below ----ashu
        timeTable.Rows.Add(hr);

        for (int i = 0; i < 6; i++)
        {
            TableRow tr = new TableRow();


            for (int j = 0; j < 9; j++)
            {
                TableCell tc = new TableCell();
                Label l = new Label();
                l.Font.Size = 15;
                tc.Controls.Add(l);
                tr.Cells.Add(tc);
            }
            timeTable.Rows.Add(tr);
        }
        int k = dt1.Rows.Count;
        int g = 0;
        string a = timeTable.Rows[1].Cells[3].Text;
        //---------------------------------------------Clear-----------------------
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int r = Convert.ToInt16(dt.Rows[i]["Day No"]);
            int c = Convert.ToInt16(dt.Rows[i]["Hour No"]);
            if (c > 4)
                c++;
            if (dt.Rows[i]["Substitute Faculty Code"].ToString() != "")// "<br/>" + "==>" + 
            {
                timeTable.Rows[r].Cells[c].Text = "";
                //  timeTable.Rows[r].Cells[c].ForeColor = Color.Green;
                //   timeTable.Rows[r].Cells[c].Font.Bold=true;
            }
            else
                timeTable.Rows[r].Cells[c].Text = "";
            if (i <= k - 1)
            {
                int r11 = Convert.ToInt16(dt1.Rows[i]["Day No"]);
                int C11 = Convert.ToInt16(dt1.Rows[i]["Hour No"]);
                if (C11 > 4)
                    C11++;
                timeTable.Rows[r11].Cells[C11].Text = "";
                //   timeTable.Rows[r11].Cells[C11].ForeColor = Color.Green;
                //  timeTable.Rows[r11].Cells[C11].Font.Bold = true;
            }
        }
        //
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int r = Convert.ToInt16(dt.Rows[i]["Day No"]);
            int c = Convert.ToInt16(dt.Rows[i]["Hour No"]);
            if (c > 4)
                c++;
            if (dt.Rows[i]["Substitute Faculty Code"].ToString() != "")// "<br/>" + "==>" + 
            {
                timeTable.Rows[r].Cells[c].Text = timeTable.Rows[r].Cells[c].Text + "<font color=Green>" + "<br/>" + "<B>" + "==>" + "</B>" + dt.Rows[i]["Subject Description"].ToString() + "/" + dt.Rows[i]["Course Code"].ToString() + "/" + dt.Rows[i]["Section Code"].ToString() + "/" + dt.Rows[i]["Room Allocation"].ToString() + "/" + dt.Rows[i]["Group"].ToString() + "/" + dt.Rows[i]["Batch"].ToString() + "(Assigned)" + "</font>";
                //  timeTable.Rows[r].Cells[c].ForeColor = Color.Green;
                timeTable.Rows[r].Cells[c].Font.Bold = true;
                timeTable.Rows[r].Cells[c].Font.Size = 9;
            }
            else
                timeTable.Rows[r].Cells[c].Text += "<br/>" + "<b>" + "==>" + "</b>" + dt.Rows[i]["Subject Description"].ToString() + "/" + dt.Rows[i]["Course Code"].ToString() + "/" + dt.Rows[i]["Section Code"].ToString() + "/" + dt.Rows[i]["Room Allocation"].ToString() + "/" + dt.Rows[i]["Group"].ToString() + "/" + dt.Rows[i]["Batch"].ToString() + "/" + dt.Rows[i]["Attendance Date"].ToString();
            timeTable.Rows[r].Cells[c].Font.Size = 9;
            if (i <= k - 1)
            {
                int r11 = Convert.ToInt16(dt1.Rows[i]["Day No"]);
                int C11 = Convert.ToInt16(dt1.Rows[i]["Hour No"]);
                if (C11 > 4)
                    C11++;
                timeTable.Rows[r11].Cells[C11].Text = timeTable.Rows[r11].Cells[C11].Text + "<br/>" + "==>" + timeTable.Rows[r11].Cells[C11].Text + dt1.Rows[i]["Subject Description"].ToString() + "/" + dt1.Rows[i]["Course Code"].ToString() + "/" + dt1.Rows[i]["Room Allocation"].ToString() + "/" + dt.Rows[i]["Group"].ToString() + "/" + dt.Rows[i]["Batch"].ToString() + "(Assigned)";
                timeTable.Rows[r11].Cells[C11].ForeColor = Color.Green;
                timeTable.Rows[r11].Cells[C11].Font.Bold = true;
            }
            if (dt.Rows[i]["Remedial Portal ID"].ToString() == "1")
            {
                timeTable.Rows[r].Cells[c].ForeColor = Color.Blue;
                timeTable.Rows[r].Cells[c].Font.Bold = true;
            }
            if (dt.Rows[i]["Remedial Portal ID"].ToString() == "3")
            {
                timeTable.Rows[r].Cells[c].ForeColor = Color.Red;
                timeTable.Rows[r].Cells[c].Font.Bold = true;
            }
        }
        if (g == 0)
        {
            string[] weeks = new string[6] { "Mon.", "Tue.", "Wed.", "Thu.", "Fri.", "Sat." };
            string[] lunch = new string[6] { "L", "U", "N", "C", "H", "*" };
            for (int l = 1; l < 7; l++)
            {
                timeTable.Rows[l].Cells[0].Font.Size = 20;
                timeTable.Rows[l].Cells[5].Font.Size = 20;
                timeTable.Rows[l].Cells[0].Text = weeks[l - 1];

                timeTable.Rows[l].Cells[5].Text = lunch[l - 1];
                timeTable.Rows[l].Cells[0].CssClass = "Bold";
                timeTable.Rows[l].Cells[5].CssClass = "Color";
                timeTable.Rows[l].Cells[5].BackColor = Color.Gray;

                timeTable.Rows[l].Cells[5].ForeColor = Color.White;

                Unit width = new Unit(150, UnitType.Pixel);
            }
            g++;
        }
    }
    protected void BtnAbsent_Click(object sender, EventArgs e)
    {

        GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
        int index = row.RowIndex;

        Label lblPresent = (Label)grdStudentAttendance.Rows[index].FindControl("lblStudent");


        Label lblStudentName = (Label)grdStudentAttendance.Rows[index].FindControl("lblStudentName");

        lblStudent.Text = "Student Name:- " + lblStudentName.Text;

        ReviewAttandanceCountDetail(lblPresent.Text, 1);
        ModalPopupExtender12.Show();
    }
    public void ReviewAttandanceCountDetail(string str, int i)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("[Sp_ReviewAttandanceForTeacherDiary]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StudentNo_", str);
            cmd.Parameters.Add("@AcademicYear", "24-25");
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@AttendanceType", i);



            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            grdAttandanceDetails.DataSource = dt;
            grdAttandanceDetails.DataBind();



        }
        catch (Exception ex) { }
    }
    protected void BtnPresent_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
            int index = row.RowIndex;

            Label lblPresent = (Label)grdStudentAttendance.Rows[index].FindControl("lblStudent");

            Label lblStudentName = (Label)grdStudentAttendance.Rows[index].FindControl("lblStudentName");
            HiddenField HfEventType = (HiddenField)grdStudentAttendance.Rows[index].FindControl("HfEventType");
            lblStudent.Text = "Student Name:- " + lblStudentName.Text;

            ReviewAttandanceCountDetail(lblPresent.Text, 0);
            ModalPopupExtender12.Show();
        }
        catch (Exception ex) { }
    }



    protected void btnclose_Click(object sender, EventArgs e)
    {

    }


    protected void grdteacherload_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Accumulate totals for each column
            totalL += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "L"));
            totalT += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "T"));
            totalP += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "P"));
            totalC += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "C"));
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Display totals in footer
            e.Row.Cells[5].Text = "Total Load";
            e.Row.Cells[5].Font.Bold = true;

            e.Row.Cells[6].Text = totalL.ToString();
            e.Row.Cells[7].Text = totalT.ToString();
            e.Row.Cells[8].Text = totalP.ToString();
            e.Row.Cells[9].Text = totalC.ToString();
        }
    }

    // Declare totals as class-level variables
    private int totalL = 0;
    private int totalT = 0;
    private int totalP = 0;
    private int totalC = 0;





    protected void btnTeachingload_Click(object sender, EventArgs e)
    {
        ModalPopupExtender13.Show();
    }

    protected void Button26_Click(object sender, EventArgs e)
    {
        ModalPopupExtender13.Hide();
    }

    protected void Button27_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("Pro_InsertOrUpdateTeacherLoad", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtteachingloadid.Text);
        cmd.Parameters.AddWithValue("@EmployeeCode", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@EmployeeName", Session["uname"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Semester1", drpsemteacher.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Program", Drpprogram.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@StudentStrength", txtstudentstrength.Text);
        cmd.Parameters.AddWithValue("@CourseCode", drpcoursecode.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@CourseName", drpcoursName.Text);
        cmd.Parameters.AddWithValue("@L", txtl.Text);
        cmd.Parameters.AddWithValue("@T", txtt.Text);
        cmd.Parameters.AddWithValue("@P", txtp.Text);
        cmd.Parameters.AddWithValue("@C", txtc.Text);
        cmd.Parameters.AddWithValue("@CourseCategory", drpcorsecategory.SelectedItem.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save successfully.'); document.location.href='TeacherDairy.aspx';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been saved successfully.');", true);
        ModalPopupExtender13.Hide();
        GetTeacherLoad();
        txtstudentstrength.Text = "";
        txtl.Text = "";
        txtt.Text = "";
        txtp.Text = "";
        txtc.Text = "";


    }

    protected void btnteachingloadug_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        GridViewRow row = btn.NamingContainer as GridViewRow;
        string pk = grdteacherload.DataKeys[row.RowIndex].Values[0].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "Select * from TeacherLoad WHERE [ID]='" + pk + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        txtstudentstrength.Text = dt.Rows[0]["StudentStrength"].ToString();
        txtteachingloadid.Text = dt.Rows[0]["ID"].ToString();
        Drpprogram.SelectedItem.Text = dt.Rows[0]["Program"].ToString();
        drpcoursecode.SelectedItem.Text = dt.Rows[0]["CourseCode"].ToString();
        drpcoursName.Text = dt.Rows[0]["CourseName"].ToString();
        txtl.Text = dt.Rows[0]["L"].ToString();
        txtt.Text = dt.Rows[0]["T"].ToString();
        txtp.Text = dt.Rows[0]["P"].ToString();
        txtc.Text = dt.Rows[0]["C"].ToString();
        drpcorsecategory.SelectedItem.Text = dt.Rows[0]["CourseCategory"].ToString();
        ModalPopupExtender13.Show();
    }
    public void GetTheoryData()
    {
        SqlCommand cmd = new SqlCommand("Pro_Theory1", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);


        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            //txtcoursecodetheory.Text = dt.Rows[0]["CourseCode"].ToString();
            //txtcoursenametheory.Text = dt.Rows[0]["CourseName"].ToString();
            //txtsemestertheory.Text = dt.Rows[0]["Semester1"].ToString();
            //txtprogramtheory.Text = dt.Rows[0]["Program"].ToString();

            //txtcoursecodelessionplantheorycourse1.Text = dt.Rows[0]["CourseCode"].ToString();
            // txtcoursenamelessionplantheorycourse1.Text = dt.Rows[0]["CourseName"].ToString();
            // txtsemesterlessionplantheorycourse1.Text = dt.Rows[0]["Semester1"].ToString();
            //drpprogramlessionplantheorycourse1.Text = dt.Rows[0]["Program"].ToString();


            //drpprogramlessionplantheorycourse1.DataSource = dt;
            //drpprogramlessionplantheorycourse1.DataTextField = "Program"; // Display text
            //drpprogramlessionplantheorycourse1.DataValueField = "Program"; // Hidden value
            //drpprogramlessionplantheorycourse1.DataBind();



            //txtcoursecodelecturewisetheorycourse1.Text = dt.Rows[0]["CourseCode"].ToString();
            //txtcoursecodelecturewisetheorycourse1.Text = dt.Rows[0]["CourseName"].ToString();
            //txtsemesterlecturewisetheorycourse1.Text = dt.Rows[0]["Semester1"].ToString();
            //txtprogramlecturewisetheorycourse1.Text = dt.Rows[0]["Program"].ToString();


            txtcoursecodedetailsofclasstext.Text = dt.Rows[0]["CourseCode"].ToString();
            //txtcoursenamedetailsofclasstext.Text = dt.Rows[0]["CourseName"].ToString();
            txtsemesterdetailsofclasstext.Text = dt.Rows[0]["Semester1"].ToString();
            txtprogramdetailsofclasstext.Text = dt.Rows[0]["Program"].ToString();


            //txtcoursenamedetailsofassignments.Text = dt.Rows[0]["CourseCode"].ToString();
            //txtcoursenamedetailsofassignments.Text = dt.Rows[0]["CourseName"].ToString();
            //txtsemesterdetailsofassignments.Text = dt.Rows[0]["Semester1"].ToString();
            //txtprogramdetailsofassignments.Text = dt.Rows[0]["Program"].ToString();

            txtcoursecodeattendancecontinuous.Text = dt.Rows[0]["CourseCode"].ToString();
            //txtcoursenameattendancecontinuous.Text = dt.Rows[0]["CourseName"].ToString();
            txtsemesterattendancecontinuous.Text = dt.Rows[0]["Semester1"].ToString();
            txtprogrammattendancecontinuous.Text = dt.Rows[0]["Program"].ToString();


            txtcoursecodeattendanceslowTheory1.Text = dt.Rows[0]["CourseCode"].ToString();
            txtcoursenameattendanceslowTheory1.Text = dt.Rows[0]["CourseName"].ToString();
            txtsemesterattendanceslowTheory1.Text = dt.Rows[0]["Semester1"].ToString();
            txtprogramattendanceslowTheory1.Text = dt.Rows[0]["Program"].ToString();

            txtcoursecodeAdvancedLearnerstheory1.Text = dt.Rows[0]["CourseCode"].ToString();
            txtcoursenameAdvancedLearnerstheory1.Text = dt.Rows[0]["CourseName"].ToString();
            txtsemesterAdvancedLearnerstheory1.Text = dt.Rows[0]["Semester1"].ToString();
            txtprogramAdvancedLearnerstheory1.Text = dt.Rows[0]["Program"].ToString();
        }
    }

    public void GetLabcourseData()
    {
        SqlCommand cmd = new SqlCommand("Pro_LabCourse1", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);


        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            txtlabcoursecode.Text = dt.Rows[0]["CourseCode"].ToString();
            txtlabcoursename.Text = dt.Rows[0]["CourseName"].ToString();
            txtlabsemester.Text = dt.Rows[0]["Semester1"].ToString();
            txtlabprogram.Text = dt.Rows[0]["Program"].ToString();

            //txtcoursecodeLaboratoryWork.Text = dt.Rows[0]["CourseCode"].ToString();
            //txtcoursecodeLaboratoryWork.Text = dt.Rows[0]["CourseName"].ToString();
            //txtcoursecodeLaboratoryWork.Text = dt.Rows[0]["Semester1"].ToString();
            //txtprogramLaboratoryWork.Text = dt.Rows[0]["Program"].ToString();


            txtcoursecodeAttendancelab1.Text = dt.Rows[0]["CourseCode"].ToString();
            txtcoursenameAttendancelab1.Text = dt.Rows[0]["CourseName"].ToString();
            txtsemesterAttendancelab1.Text = dt.Rows[0]["Semester1"].ToString();
            txtprogramAttendancelab1.Text = dt.Rows[0]["Program"].ToString();
        }

    }

    protected void Button29_Click(object sender, EventArgs e)
    {
        GetTheoryData();
        ModalPopupExtender14.Show();
    }

    protected void Button31_Click(object sender, EventArgs e)
    {
        ModalPopupExtender15.Hide();
    }

    protected void Button34_Click(object sender, EventArgs e)
    {
        GetLabcourseData();
        ModalPopupExtender15.Show();
    }

    protected void Button30_Click(object sender, EventArgs e)
    {
        ModalPopupExtender14.Hide();
    }

    protected void Button37_Click(object sender, EventArgs e)
    {
        GetTheoryData();
        ModalPopupExtender16.Show();
    }

    protected void Button35_Click(object sender, EventArgs e)
    {
        ModalPopupExtender16.Hide();
    }

    protected void Button40_Click(object sender, EventArgs e)
    {
        GetTheoryData();
        //ModalPopupExtender17.Show();
    }

    protected void Button38_Click(object sender, EventArgs e)
    {
        //ModalPopupExtender17.Hide();
    }

    protected void Button41_Click(object sender, EventArgs e)
    {
        GetLabcourseData();
        //ModalPopupExtender18.Show();
    }

    protected void Button42_Click(object sender, EventArgs e)
    {
        //ModalPopupExtender18.Hide();
    }

    protected void Button44_Click(object sender, EventArgs e)
    {
        GetTheoryData();
       // ModalPopupExtender19.Show();
    }

    protected void Button45_Click(object sender, EventArgs e)
    {
       // ModalPopupExtender19.Hide();
    }

    protected void Button47_Click(object sender, EventArgs e)
    {
        GetTheoryData();
        ModalPopupExtender20.Show();
    }

    protected void Button48_Click(object sender, EventArgs e)
    {
        ModalPopupExtender20.Hide();
    }

    protected void Button52_Click(object sender, EventArgs e)
    {
        GetTheoryData();
        ModalPopupExtender21.Show();

    }

    protected void Button50_Click(object sender, EventArgs e)
    {
        ModalPopupExtender21.Hide();
    }

    protected void Button53_Click(object sender, EventArgs e)
    {
        ModalPopupExtender22.Hide();
    }

    protected void Button55_Click(object sender, EventArgs e)
    {
        GetLabcourseData();
        ModalPopupExtender22.Show();
    }

    protected void Button56_Click(object sender, EventArgs e)
    {
        ModalPopupExtender23.Hide();
    }

    protected void Button58_Click(object sender, EventArgs e)
    {
        GetTheoryData();
        ModalPopupExtender23.Show();
    }

    protected void Button59_Click(object sender, EventArgs e)
    {
        GetTheoryData();
        ModalPopupExtender24.Show();
    }

    protected void Button60_Click(object sender, EventArgs e)
    {
        ModalPopupExtender24.Hide();
    }

    protected void Button62_Click(object sender, EventArgs e)
    {
        ModalPopupExtender25.Show();
    }

    protected void Button63_Click(object sender, EventArgs e)
    {
        ModalPopupExtender25.Hide();
    }

    protected void Button65_Click(object sender, EventArgs e)
    {
        ModalPopupExtender26.Hide();
    }

    protected void Button67_Click(object sender, EventArgs e)
    {
        ModalPopupExtender26.Show();
    }

    protected void btnprofileupdate_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("InserUpdateEmployeeProfile", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtprofileid.Text);
        cmd.Parameters.AddWithValue("@EmployeeName", txtFName.Text);
        cmd.Parameters.AddWithValue("@EmployeeCode", txtFacultyCode.Text);
        cmd.Parameters.AddWithValue("@Designation", txtdesignaton.Text);
        cmd.Parameters.AddWithValue("@Department", txtDepartment.Text);
        cmd.Parameters.AddWithValue("@CollegeFaculty", txtcollegeFaculty.Text);
        cmd.Parameters.AddWithValue("@EducationalQualification", txtPrograme.Text);
        cmd.Parameters.AddWithValue("@AreaOfSpecialization", txtAreaofSpecilization.Text);
        cmd.Parameters.AddWithValue("@ContactNo", txtcontactno.Text);
        cmd.Parameters.AddWithValue("@EmailID", txtemailid.Text);
        cmd.Parameters.AddWithValue("@DateOfBirth", txtdateofbirth.Text);
        cmd.Parameters.AddWithValue("@DateOfJoining", txtdateofjoining.Text);
        cmd.Parameters.AddWithValue("@TeachingWithinTMU", txtteachingwithintmu.Text);
        cmd.Parameters.AddWithValue("@TeachingOutsideTMU", txtteachingoutsidetmu.Text);
        cmd.Parameters.AddWithValue("@Research", txtresearch.Text);
        cmd.Parameters.AddWithValue("@Industry", txtindustry.Text);
        cmd.Parameters.AddWithValue("@Others", txtothers.Text);
        cmd.Parameters.AddWithValue("@TotalPublications", TextBox1.Text);
        cmd.Parameters.AddWithValue("@FacultyIndex", txthindex.Text);
        cmd.Parameters.AddWithValue("@FacultyIndex1", txti10index.Text);
        cmd.Parameters.AddWithValue("@Citations", txtcitations.Text);
        cmd.Parameters.AddWithValue("@CreatedBy", Session["uid"].ToString());

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Update successfully.'); document.location.href='TeacherDairy.aspx';", true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your Profile have been Update successfully.');", true);
    }

    private void PopulateSemesters(string semesterType)
    {
        drpsemteacher.Items.Clear();
        drpsemesterlessionplantheorycourse1.Items.Clear();
        ddlSemester.Items.Clear();
        if (semesterType == "0") // Even Semesters
        {

            drpsemteacher.Items.Add(new ListItem("II", "2"));
            drpsemteacher.Items.Add(new ListItem("IV", "4"));
            drpsemteacher.Items.Add(new ListItem("VI", "6"));
            drpsemteacher.Items.Add(new ListItem("VIII", "8"));
            drpsemteacher.Items.Add(new ListItem("X", "10"));


            drpsemesterlessionplantheorycourse1.Items.Add(new ListItem("II", "2"));
            drpsemesterlessionplantheorycourse1.Items.Add(new ListItem("IV", "4"));
            drpsemesterlessionplantheorycourse1.Items.Add(new ListItem("VI", "6"));
            drpsemesterlessionplantheorycourse1.Items.Add(new ListItem("VIII", "8"));
            drpsemesterlessionplantheorycourse1.Items.Add(new ListItem("X", "10"));

            //drpsemesterLaboratoryWork.Items.Add(new ListItem("II", "2"));
            //drpsemesterLaboratoryWork.Items.Add(new ListItem("IV", "4"));
            //drpsemesterLaboratoryWork.Items.Add(new ListItem("VI", "6"));
            //drpsemesterLaboratoryWork.Items.Add(new ListItem("VIII", "8"));
            //drpsemesterLaboratoryWork.Items.Add(new ListItem("X", "10"));



            //drpsemesterlecturewisetheorycourse1.Items.Add(new ListItem("II", "2"));
            //drpsemesterlecturewisetheorycourse1.Items.Add(new ListItem("IV", "4"));
            //drpsemesterlecturewisetheorycourse1.Items.Add(new ListItem("VI", "6"));
            //drpsemesterlecturewisetheorycourse1.Items.Add(new ListItem("VIII", "8"));
            //drpsemesterlecturewisetheorycourse1.Items.Add(new ListItem("X", "10"));

            ddlSemester.Items.Add(new ListItem("II", "2"));
            ddlSemester.Items.Add(new ListItem("IV", "4"));
            ddlSemester.Items.Add(new ListItem("VI", "6"));
            ddlSemester.Items.Add(new ListItem("VIII", "8"));
            ddlSemester.Items.Add(new ListItem("X", "10"));

        }
        else
        {
            drpsemteacher.Items.Add(new ListItem("I", "1"));
            drpsemteacher.Items.Add(new ListItem("III", "3"));
            drpsemteacher.Items.Add(new ListItem("V", "5"));
            drpsemteacher.Items.Add(new ListItem("VII", "7"));
            drpsemteacher.Items.Add(new ListItem("IX", "9"));

            drpsemesterlessionplantheorycourse1.Items.Add(new ListItem("I", "1"));
            drpsemesterlessionplantheorycourse1.Items.Add(new ListItem("III", "3"));
            drpsemesterlessionplantheorycourse1.Items.Add(new ListItem("V", "5"));
            drpsemesterlessionplantheorycourse1.Items.Add(new ListItem("VII", "7"));
            drpsemesterlessionplantheorycourse1.Items.Add(new ListItem("IX", "9"));

            //drpsemesterLaboratoryWork.Items.Add(new ListItem("I", "1"));
            //drpsemesterLaboratoryWork.Items.Add(new ListItem("III", "3"));
            //drpsemesterLaboratoryWork.Items.Add(new ListItem("V", "5"));
            //drpsemesterLaboratoryWork.Items.Add(new ListItem("VII", "7"));
            //drpsemesterLaboratoryWork.Items.Add(new ListItem("IX", "9"));


            //drpsemesterlecturewisetheorycourse1.Items.Add(new ListItem("I", "1"));
            //drpsemesterlecturewisetheorycourse1.Items.Add(new ListItem("III", "3"));
            //drpsemesterlecturewisetheorycourse1.Items.Add(new ListItem("V", "5"));
            //drpsemesterlecturewisetheorycourse1.Items.Add(new ListItem("VII", "7"));
            //drpsemesterlecturewisetheorycourse1.Items.Add(new ListItem("IX", "9"));

            ddlSemester.Items.Add(new ListItem("I", "1"));
            ddlSemester.Items.Add(new ListItem("III", "3"));
            ddlSemester.Items.Add(new ListItem("V", "5"));
            ddlSemester.Items.Add(new ListItem("VII", "7"));
            ddlSemester.Items.Add(new ListItem("IX", "9"));
        }

    }

    protected void drpsemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedValue = drpsemester.SelectedValue;
        PopulateSemesters(selectedValue);
    }

    protected void Button68_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("Pro_InsertOrUpdateLessionPlan", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtidlessionplanunitwise.Text);
        cmd.Parameters.AddWithValue("@EmployeeCode", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@EmployeeName", Session["uname"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Semester1", drpsemesterlessionplantheorycourse1.Text);
        cmd.Parameters.AddWithValue("@Program", drpprogramlessionplantheorycourse1.Text);
        cmd.Parameters.AddWithValue("@CourseCode", drpcoursecodelessionplantheorycourse1.Text);
        cmd.Parameters.AddWithValue("@CourseName", drpcoursenamelessionplantheorycourse1.Text);
        cmd.Parameters.AddWithValue("@NoOfCredits", txtnoofcreditlessionplantheorycourse1.Text);
        cmd.Parameters.AddWithValue("@TotalTeachingHrs", txttotalteachinghours.Text);
        cmd.Parameters.AddWithValue("@UnitNo", drpunitno.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@TopicsCount", txttopiccovered.Text);
        cmd.Parameters.AddWithValue("@PlannedStartDate", txtplanstarteddate.Text);
        cmd.Parameters.AddWithValue("@PlannedEndDate", txtplannedenddate.Text);
        cmd.Parameters.AddWithValue("@PlannedLectures", txtplannedtotallecture.Text);
        cmd.Parameters.AddWithValue("@ActualStartDate", txtactualstartdate.Text);
        cmd.Parameters.AddWithValue("@ActualEndDate", txtactualenddate.Text);
        cmd.Parameters.AddWithValue("@ActualLectures", txtactualtotallecture.Text);

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save successfully.'); document.location.href='TeacherDairy.aspx';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been saved successfully.');", true);
        ModalPopupExtender15.Hide();
        BindLessonPlans();
        //LessionPlanSummaryunitwise();
    }
    public void bindDrpCourseList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetCourseFromCourseWiseFacultyCollege_Role03072025", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        Drpprogram.DataSource = dt;
        Drpprogram.DataTextField = "Details";
        Drpprogram.DataValueField = "No_";
        Drpprogram.DataBind();

        drpprogramlessionplantheorycourse1.DataSource = dt;
        drpprogramlessionplantheorycourse1.DataTextField = "Details";
        drpprogramlessionplantheorycourse1.DataValueField = "No_";
        drpprogramlessionplantheorycourse1.DataBind();

        //drpprogramLaboratoryWork.DataSource = dt;
        //drpprogramLaboratoryWork.DataTextField = "Details";
        //drpprogramLaboratoryWork.DataValueField = "No_";
        //drpprogramLaboratoryWork.DataBind();


        //drpprogramlecturewisetheorycourse1.DataSource = dt;
        //drpprogramlecturewisetheorycourse1.DataTextField = "Details";
        //drpprogramlecturewisetheorycourse1.DataValueField = "No_";
        //drpprogramlecturewisetheorycourse1.DataBind();
        ddlProgram.DataSource = dt;
        ddlProgram.DataTextField = "Details";
        ddlProgram.DataValueField = "No_";
        ddlProgram.DataBind();

    }
    public void bindDrpCoursecodeListTeachLoad()
    {
        SqlCommand cmd = new SqlCommand("proc_GetSubject", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", Drpprogram.SelectedValue);
        cmd.Parameters.Add("@SemesterYear", drpsemteacher.SelectedItem.Text);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if(dt.Rows.Count>0)
        {
            drpcoursecode.DataSource = dt;
            drpcoursecode.DataTextField = "No_";
            drpcoursecode.DataValueField = "No_";
            drpcoursecode.DataBind ();
        }
    }


    public void bindDrpCoursecodeListLessionplansummarytheory()
    {
        SqlCommand cmd = new SqlCommand("proc_GetSubject", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpprogramlessionplantheorycourse1.SelectedValue);
        cmd.Parameters.Add("@SemesterYear", drpsemesterlessionplantheorycourse1.SelectedItem.Text);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            drpcoursecodelessionplantheorycourse1.DataSource = dt;
            drpcoursecodelessionplantheorycourse1.DataTextField = "No_";
            drpcoursecodelessionplantheorycourse1.DataValueField = "No_";
            drpcoursecodelessionplantheorycourse1.DataBind();
        }
    }

    //public void bindLessionPlanLECTUREWISE()
    //{
    //    SqlCommand cmd = new SqlCommand("proc_GetSubject", con1);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.Add("@CourseCode", drpprogramlecturewisetheorycourse1.SelectedValue);
    //    cmd.Parameters.Add("@SemesterYear", drpsemesterlecturewisetheorycourse1.SelectedItem.Text);
    //    cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        //drpcoursecodelecturewisetheorycourse1.DataSource = dt;
    //        //drpcoursecodelecturewisetheorycourse1.DataTextField = "No_";
    //        //drpcoursecodelecturewisetheorycourse1.DataValueField = "No_";
    //        //drpcoursecodelecturewisetheorycourse1.DataBind();
    //    }
    //}

    //public void bindLaboratoryWorkPlan()
    //{
    //    SqlCommand cmd = new SqlCommand("proc_GetSubject", con1);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.Add("@CourseCode", drpprogramLaboratoryWork.SelectedValue);
    //    cmd.Parameters.Add("@SemesterYear", drpsemesterLaboratoryWork.SelectedItem.Text);
    //    cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        drpcoursecodeLaboratoryWork.DataSource = dt;
    //        drpcoursecodeLaboratoryWork.DataTextField = "No_";
    //        drpcoursecodeLaboratoryWork.DataValueField = "No_";
    //        drpcoursecodeLaboratoryWork.DataBind();
    //    }
    //}

    public void bindDrpCoursecodeList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetCourseFromCourseWiseFacultyCollege_Role03072025", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());


        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpcoursecode.DataSource = dt;
        drpcoursecode.DataTextField = "No_";
        drpcoursecode.DataValueField = "No_";
        drpcoursecode.DataBind();

        drpcoursecodelessionplantheorycourse1.DataSource = dt;
        drpcoursecodelessionplantheorycourse1.DataTextField = "No_";
        drpcoursecodelessionplantheorycourse1.DataValueField = "No_";
        drpcoursecodelessionplantheorycourse1.DataBind();

        ////drpcoursecodeLaboratoryWork.DataSource = dt;
        ////drpcoursecodeLaboratoryWork.DataTextField = "No_";
        ////drpcoursecodeLaboratoryWork.DataValueField = "No_";
        ////drpcoursecodeLaboratoryWork.DataBind();

        ////drpcoursecodelecturewisetheorycourse1.DataSource = dt;
        ////drpcoursecodelecturewisetheorycourse1.DataTextField = "No_";
        ////drpcoursecodelecturewisetheorycourse1.DataValueField = "No_";
        ////drpcoursecodelecturewisetheorycourse1.DataBind();


        //drpcoursName.DataSource = dt;
        //drpcoursName.DataTextField = "Exam Program Name";
        //drpcoursName.DataValueField = "Exam Program Name";
        //drpcoursName.DataBind();

        //drpcoursenamelessionplantheorycourse1.DataSource = dt;
        //drpcoursenamelessionplantheorycourse1.DataTextField = "Exam Program Name";
        //drpcoursenamelessionplantheorycourse1.DataValueField = "Exam Program Name";
        //drpcoursenamelessionplantheorycourse1.DataBind();

        //drpcoursenameLaboratoryWork.DataSource = dt;
        //drpcoursenameLaboratoryWork.DataTextField = "Exam Program Name";
        //drpcoursenameLaboratoryWork.DataValueField = "Exam Program Name";
        //drpcoursenameLaboratoryWork.DataBind();

        //drpcoursenamelecturewisetheorycourse1.DataSource = dt;
        //drpcoursenamelecturewisetheorycourse1.DataTextField = "Exam Program Name";
        //drpcoursenamelecturewisetheorycourse1.DataValueField = "Exam Program Name";
        //drpcoursenamelecturewisetheorycourse1.DataBind();
    }
    //public void bindDrpSemesterList()
    //{
    //    SqlCommand cmd = new SqlCommand("proc_GetSemesterFromCourseWiseFaculty_Role", con1);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.Add("@ID1", Session["uid"].ToString());
    //    cmd.Parameters.Add("@ID", Drpprogram.SelectedValue);
    //    DataTable dt = new DataTable();
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    da.Fill(dt);
    //    drpsemteacher.DataSource = dt;
    //    drpsemteacher.DataTextField = "Details";
    //    drpsemteacher.DataValueField = "No_";
    //    drpsemteacher.DataBind();
    //}

    //public void bindGetSubjectList()
    //{
    //    DataTable dt = new DataTable();
    //    SqlCommand cmd = new SqlCommand("proc_GetSubjectFromCourseSubjectLine", con1);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.Add("@AcademicYear", drpacademicsession.SelectedValue);
    //    cmd.Parameters.Add("@CourseCode", Drpprogram.SelectedValue);
    //    cmd.Parameters.Add("@SemesterCode", drpsemester.SelectedValue);
    //    //cmd.Parameters.Add("@Section", drpSection.SelectedValue);
    //    cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());

    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    da.Fill(dt);
    //    drpcoursecode.DataSource = dt;
    //    drpcoursecode.DataTextField = "Details";
    //    drpcoursecode.DataValueField = "No_";
    //    drpcoursecode.DataBind();

    //}

    protected void Drpprogram_SelectedIndexChanged(object sender, EventArgs e)
    {
        //bindDrpCoursecodeList();
        bindDrpCoursecodeListTeachLoad();
        ModalPopupExtender13.Show();
    }

    public void BindLessonPlans()
    {
        string connStr = ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand("Pro_LessionPlanSummaryunitwise", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
                cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                pnlLessonPlans.Controls.Clear();

                if (dt.Rows.Count > 0)
                {
                    var groupedCourseCode = dt.AsEnumerable().GroupBy(row => row["CourseCode"].ToString());

                    foreach (var CourseCodeGroup in groupedCourseCode)
                    {

                        DataRow firstRow = CourseCodeGroup.First();

                        string Program = firstRow["Program"].ToString();
                        string CourseCode = firstRow["CourseCode"].ToString();
                        string CourseName = firstRow["CourseName"].ToString();
                        string Semester1 = firstRow["Semester1"].ToString();
                        string NoOfCredits = firstRow["NoOfCredits"].ToString();
                        string TotalTeachingHrs = firstRow["TotalTeachingHrs"].ToString();


                        Label lblInfo = new Label();
                        lblInfo.Text = "<h3 style='color:#ed7600'>" +
                                       "Program: " + Program + " | " +
                                       "Course: " + CourseCode + " | " +
                                       "Course Name: " + CourseName + " | " +
                                       "Semester: " + Semester1 + " | " +
                                       "No of Credits: " + NoOfCredits + " | " +
                                       "Total Teaching Hrs: " + TotalTeachingHrs +
                                       "</h3>";

                        pnlLessonPlans.Controls.Add(new LiteralControl(lblInfo.Text));


                        DataTable filteredTable = new DataTable();
                        filteredTable.Columns.Add("SrNo", typeof(int));
                        filteredTable.Columns.Add("UnitNo", typeof(string));
                        filteredTable.Columns.Add("TopicsCount", typeof(string));
                        filteredTable.Columns.Add("PlannedStartDate", typeof(string));
                        filteredTable.Columns.Add("PlannedEndDate", typeof(string));
                        filteredTable.Columns.Add("PlannedLectures", typeof(int));
                        filteredTable.Columns.Add("ActualStartDate", typeof(string));
                        filteredTable.Columns.Add("ActualEndDate", typeof(string));
                        filteredTable.Columns.Add("ActualLectures", typeof(int));

                        int srNo = 1;

                        foreach (DataRow row in CourseCodeGroup)
                        {
                            string topicsCount = row["TopicsCount"].ToString();
                            int plannedLectures = 0;
                            int actualLectures = 0;


                            if (!int.TryParse(row["PlannedLectures"].ToString(), out plannedLectures))
                                plannedLectures = 0;

                            if (!int.TryParse(row["ActualLectures"].ToString(), out actualLectures))
                                actualLectures = 0;

                            filteredTable.Rows.Add(
                                srNo++,
                                row["UnitNo"].ToString(),
                                topicsCount,
                                SafeDate(row["PlannedStartDate"]),
                                SafeDate(row["PlannedEndDate"]),
                                plannedLectures,
                                SafeDate(row["ActualStartDate"]),
                                SafeDate(row["ActualEndDate"]),
                                actualLectures
                            );
                        }


                        GridView gv = new GridView
                        {
                            AutoGenerateColumns = false,
                            CssClass = "table table-bordered",
                            BorderStyle = BorderStyle.Solid,
                            BorderWidth = 1
                        };


                        gv.Columns.Add(new BoundField { HeaderText = "Sr.No", DataField = "SrNo" });
                        gv.Columns.Add(new BoundField { HeaderText = "Unit No", DataField = "UnitNo" });
                        gv.Columns.Add(new BoundField { HeaderText = "Topics Count", DataField = "TopicsCount" });
                        gv.Columns.Add(new BoundField { HeaderText = "Planned Start Date", DataField = "PlannedStartDate" });
                        gv.Columns.Add(new BoundField { HeaderText = "Planned End Date", DataField = "PlannedEndDate" });
                        gv.Columns.Add(new BoundField { HeaderText = "Planned Lectures", DataField = "PlannedLectures" });
                        gv.Columns.Add(new BoundField { HeaderText = "Actual Start Date", DataField = "ActualStartDate" });
                        gv.Columns.Add(new BoundField { HeaderText = "Actual End Date", DataField = "ActualEndDate" });
                        gv.Columns.Add(new BoundField { HeaderText = "Actual Lectures", DataField = "ActualLectures" });

                        gv.DataSource = filteredTable;
                        gv.DataBind();

                        pnlLessonPlans.Controls.Add(gv);
                    }
                }
            }
        }
    }


    private string SafeDate(object dateValue)
    {
        DateTime date;
        if (DateTime.TryParse(dateValue.ToString(), out date))
        {
            return date.ToString("dd-MMM-yyyy");
        }
        return "";
    }




    //protected void Button69_Click(object sender, EventArgs e)
    //{
    //    SqlCommand cmd = new SqlCommand("Pro_InsertOrUpdateLessionPlanLectureWisetheorycourse", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@ID", txtidlecturewisetheorycourse1.Text);
    //    cmd.Parameters.AddWithValue("@EmployeeCode", Session["uid"].ToString());
    //    cmd.Parameters.AddWithValue("@EmployeeName", Session["uname"].ToString());
    //    cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
    //    cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
    //    cmd.Parameters.AddWithValue("@Semester1", drpsemesterlecturewisetheorycourse1.SelectedItem.Text);
    //    cmd.Parameters.AddWithValue("@Program", drpprogramlecturewisetheorycourse1.SelectedItem.Text);
    //    cmd.Parameters.AddWithValue("@CourseCode", drpcoursecodelecturewisetheorycourse1.SelectedItem.Text);
    //    cmd.Parameters.AddWithValue("@CourseName", drpcoursenamelecturewisetheorycourse1.Text);
    //    cmd.Parameters.AddWithValue("@NoOfCredits", txtnoofcreditlecturewisetheorycourse1.Text);
    //    cmd.Parameters.AddWithValue("@TotalTeachingHrs", txttotalteachinghrslecturewisetheorycourse1.Text);
    //    cmd.Parameters.AddWithValue("@LectureNo", txtlecturenolecturewisetheorycourse1.Text);
    //    cmd.Parameters.AddWithValue("@UnitNo", txtunitnolecturewisetheorycourse1.Text);
    //    cmd.Parameters.AddWithValue("@PlannedDate", txtplanneddatelecturewisetheorycourse1.Text);
    //    cmd.Parameters.AddWithValue("@DateofActualConduction", txtdateofactualconductionlecturewisetheorycourse1.Text);
    //    cmd.Parameters.AddWithValue("@Planned", txtplannedlecturewisetheorycourse1.Text);
    //    cmd.Parameters.AddWithValue("@ActuallyCovered", txtactuallycoveredlecturewisetheorycourse1.Text);
    //    cmd.Parameters.AddWithValue("@Pedagogy_TeachingMethods", txtpedagogylecturewisetheorycourse1.Text);
    //    cmd.Parameters.AddWithValue("@TextBook_ReferenceBook_WebReferences", txtbookreferencelecturewisetheorycourse1.Text);
    //    cmd.Parameters.AddWithValue("@LectureOutcome_BloomsTaxonomy", txtlectureotcomelecturewisetheorycourse1.Text);
    //    cmd.Parameters.AddWithValue("@CourseOutcome_CO", txtcourseotcomelecturewisetheorycourse1.Text);
    //    if (con.State == ConnectionState.Open)
    //    { con.Close(); }
    //    con.Open();
    //    cmd.ExecuteNonQuery();
    //    con.Close();
    //    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save successfully.'); document.location.href='TeacherDairy.aspx';", true);
    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been saved successfully.');", true);
    //    //ModalPopupExtender17.Hide();

    //}

    //protected void Button70_Click(object sender, EventArgs e)
    //{
    //    SqlCommand cmd = new SqlCommand("Pro_InsertOrUpdateLaboratoryWorkPlan", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@ID", txtidLaboratoryWork.Text);
    //    cmd.Parameters.AddWithValue("@EmployeeCode", Session["uid"].ToString());
    //    cmd.Parameters.AddWithValue("@EmployeeName", Session["uname"].ToString());
    //    cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
    //    cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
    //    cmd.Parameters.AddWithValue("@Semester1", drpsemesterLaboratoryWork.SelectedItem.Text);
    //    cmd.Parameters.AddWithValue("@Program", drpprogramLaboratoryWork.SelectedItem.Text);
    //    cmd.Parameters.AddWithValue("@CourseCode", drpcoursecodeLaboratoryWork.SelectedItem.Text);
    //    cmd.Parameters.AddWithValue("@CourseName", drpcoursenameLaboratoryWork.Text);
    //    cmd.Parameters.AddWithValue("@NoOfCredits", txtnoofcreditsLaboratoryWork.Text);
    //    cmd.Parameters.AddWithValue("@TotalTeachingHrs", txttotalteachingLaboratoryWork.Text);
    //    cmd.Parameters.AddWithValue("@NameOfExperiment", txtnameofexperimentLaboratoryWork.Text);
    //    cmd.Parameters.AddWithValue("@PlannedDate", txtplanneddateLaboratoryWork.Text);
    //    cmd.Parameters.AddWithValue("@DateOfCompletion", txtdateofcompletionLaboratoryWork.Text);
    //    cmd.Parameters.AddWithValue("@Remarks", txtremarkLaboratoryWork.Text);
    //    if (con.State == ConnectionState.Open)
    //    { con.Close(); }
    //    con.Open();
    //    cmd.ExecuteNonQuery();
    //    con.Close();
    //    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save successfully.'); document.location.href='TeacherDairy.aspx';", true);
    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been saved successfully.');", true);
    //    //ModalPopupExtender18.Hide();

    //}

    public void LaboratoryWorkPlanLABCOURS()
    {
        string connStr = ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand("Pro_LaboratoryWorkPlanLABCOURSE", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
                cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                PnlLaboratoryWorkPlan.Controls.Clear();

                if (dt.Rows.Count > 0)
                {
                    var groupedCourseCode = dt.AsEnumerable().GroupBy(row => row["CourseCode"].ToString());

                    foreach (var CourseCodeGroup in groupedCourseCode)
                    {

                        DataRow firstRow = CourseCodeGroup.First();

                        string Program = firstRow["Program"].ToString();
                        string CourseCode = firstRow["CourseCode"].ToString();
                        string CourseName = firstRow["CourseName"].ToString();
                        string Semester1 = firstRow["Semester1"].ToString();
                        string NoOfCredits = firstRow["NoOfCredits"].ToString();
                        string TotalTeachingHrs = firstRow["TotalTeachingHrs"].ToString();


                        Label lblInfo = new Label();
                        lblInfo.Text = "<h3 style='color:#ed7600'>" +
                                       "Program: " + Program + " | " +
                                       "Course: " + CourseCode + " | " +
                                       "Course Name: " + CourseName + " | " +
                                       "Semester: " + Semester1 + " | " +
                                       "No of Credits: " + NoOfCredits + " | " +
                                       "Total Teaching Hrs: " + TotalTeachingHrs +
                                       "</h3>";

                        PnlLaboratoryWorkPlan.Controls.Add(new LiteralControl(lblInfo.Text));


                        DataTable filteredTable = new DataTable();
                        filteredTable.Columns.Add("SrNo", typeof(int));
                        filteredTable.Columns.Add("NameOfExperiment", typeof(string));
                        filteredTable.Columns.Add("PlannedDate", typeof(string));
                        filteredTable.Columns.Add("DateOfCompletion", typeof(string));

                        int srNo = 1;

                        foreach (DataRow row in dt.Rows)
                        {
                            filteredTable.Rows.Add(
                                srNo++,
                                row["NameOfExperiment"].ToString(),
                                SafeDate(row["PlannedDate"]),
                                SafeDate(row["DateOfCompletion"])
                            );
                        }

                        // Create a GridView dynamically
                        GridView gv = new GridView
                        {
                            AutoGenerateColumns = false, // Disable auto column generation
                            CssClass = "table table-bordered",
                            BorderStyle = BorderStyle.Solid,
                            BorderWidth = 1
                        };

                        // Define required columns
                        gv.Columns.Add(new BoundField { HeaderText = "Sr.No", DataField = "SrNo" });
                        gv.Columns.Add(new BoundField { HeaderText = "Name of Experiment", DataField = "NameOfExperiment" });
                        gv.Columns.Add(new BoundField { HeaderText = "Planned Date", DataField = "PlannedDate" });
                        gv.Columns.Add(new BoundField { HeaderText = "Date of Completion", DataField = "DateOfCompletion" });

                        gv.DataSource = filteredTable;
                        gv.DataBind();

                        PnlLaboratoryWorkPlan.Controls.Add(gv);

                    }
                }
            }
        }
    }
    public void LessionPlanLECTUREWISE()
    {
        string connStr = ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand("Pro_LessionPlanLECTUREWISE", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
                cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                PnlLessionPlanLECTUREWISE.Controls.Clear(); // Clear previous data

                if (dt.Rows.Count > 0)
                {
                    var groupedCourseCode = dt.AsEnumerable().GroupBy(row => row["CourseCode"].ToString());

                    foreach (var CourseCodeGroup in groupedCourseCode)
                    {

                        DataRow firstRow = CourseCodeGroup.First();

                        string Program = firstRow["Program"].ToString();
                        string CourseCode = firstRow["CourseCode"].ToString();
                        string CourseName = firstRow["CourseName"].ToString();
                        string Semester1 = firstRow["Semester1"].ToString();
                        string NoOfCredits = firstRow["NoOfCredits"].ToString();
                        string TotalTeachingHrs = firstRow["TotalTeachingHrs"].ToString();


                        Label lblInfo = new Label();
                        lblInfo.Text = "<h3 style='color:#ed7600'>" +
                                       "Program: " + Program + " | " +
                                       "Course: " + CourseCode + " | " +
                                       "Course Name: " + CourseName + " | " +
                                       "Semester: " + Semester1 + " | " +
                                       "No of Credits: " + NoOfCredits + " | " +
                                       "Total Teaching Hrs: " + TotalTeachingHrs +
                                       "</h3>";

                        PnlLessionPlanLECTUREWISE.Controls.Add(new LiteralControl(lblInfo.Text));


                        DataTable filteredTable = new DataTable();
                        filteredTable.Columns.Add("SrNo", typeof(int));
                        filteredTable.Columns.Add("LectureNo", typeof(string));
                        filteredTable.Columns.Add("UnitNo", typeof(string));
                        filteredTable.Columns.Add("PlannedDate", typeof(string));
                        filteredTable.Columns.Add("DateofActualConduction", typeof(string));
                        filteredTable.Columns.Add("Planned", typeof(string));
                        filteredTable.Columns.Add("ActuallyCovered", typeof(string));
                        filteredTable.Columns.Add("Pedagogy_TeachingMethods", typeof(string));
                        filteredTable.Columns.Add("TextBook_ReferenceBook_WebReferences", typeof(string));
                        filteredTable.Columns.Add("LectureOutcome_BloomsTaxonomy", typeof(string));
                        filteredTable.Columns.Add("CourseOutcome_CO", typeof(string));

                        int srNo = 1;

                        foreach (DataRow row in CourseCodeGroup)
                        {
                            filteredTable.Rows.Add(
                                srNo++,
                                row["LectureNo"].ToString(),
                                row["UnitNo"].ToString(),
                                SafeDate(row["PlannedDate"]),
                                SafeDate(row["DateofActualConduction"]),
                                row["Planned"].ToString(),
                                row["ActuallyCovered"].ToString(),
                                row["Pedagogy_TeachingMethods"].ToString(),
                                row["TextBook_ReferenceBook_WebReferences"].ToString(),
                                row["LectureOutcome_BloomsTaxonomy"].ToString(),
                                row["CourseOutcome_CO"].ToString()
                            );
                        }


                        GridView gv = new GridView
                        {
                            AutoGenerateColumns = false,
                            CssClass = "table table-bordered",
                            BorderStyle = BorderStyle.Solid,
                            BorderWidth = 1
                        };

                        // Define required columns
                        gv.Columns.Add(new BoundField { HeaderText = "Sr.No", DataField = "SrNo" });
                        gv.Columns.Add(new BoundField { HeaderText = "Lecture No", DataField = "LectureNo" });
                        gv.Columns.Add(new BoundField { HeaderText = "Unit No", DataField = "UnitNo" });
                        gv.Columns.Add(new BoundField { HeaderText = "Planned Date", DataField = "PlannedDate" });
                        gv.Columns.Add(new BoundField { HeaderText = "Date of Actual Conduction", DataField = "DateofActualConduction" });
                        gv.Columns.Add(new BoundField { HeaderText = "Planned", DataField = "Planned" });
                        gv.Columns.Add(new BoundField { HeaderText = "Actually Covered", DataField = "ActuallyCovered" });
                        gv.Columns.Add(new BoundField { HeaderText = "Pedagogy/Teaching Methods", DataField = "Pedagogy_TeachingMethods" });
                        gv.Columns.Add(new BoundField { HeaderText = "TextBook / Reference Book / Web References", DataField = "TextBook_ReferenceBook_WebReferences" });
                        gv.Columns.Add(new BoundField { HeaderText = "Lecture Outcome / Bloom's Taxonomy", DataField = "LectureOutcome_BloomsTaxonomy" });
                        gv.Columns.Add(new BoundField { HeaderText = "Course Outcome (CO)", DataField = "CourseOutcome_CO" });

                        gv.DataSource = filteredTable;
                        gv.DataBind();

                        PnlLessionPlanLECTUREWISE.Controls.Add(gv);
                    }
                }
            }
        }
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetStudentList();
        ModalPopupExtender9.Show();
    }


    public void GetStudentList()
    {
        SqlCommand cmd = new SqlCommand("Pro_bindStudentList", con1);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@AcademicSession", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", ddlSemester.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Course", ddlProgram.SelectedValue);


        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {

            ddlStudentList.DataSource = dt;
            ddlStudentList.DataTextField = "Student_Name";
            ddlStudentList.DataValueField = "Name";
            ddlStudentList.DataBind();

        }

    }

    protected void Button21_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("Pro_InsertStudentProjectsOngoing", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", txtidstudentongoing.Text);
        cmd.Parameters.AddWithValue("@EmployeeCode", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@EmployeeName", Session["uname"].ToString());
        cmd.Parameters.AddWithValue("@AcademicYear", drpacademicsession.SelectedValue);
        cmd.Parameters.AddWithValue("@Semester", drpsemester.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Semester1", ddlSemester.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Programme", ddlProgram.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@UGPGLevel", ddlUGPG.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@StudentsName", ddlStudentList.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@ProjectTopic", txtProjectTopic.Text);
        cmd.Parameters.AddWithValue("@Month1", txtmonth1.Text);
        cmd.Parameters.AddWithValue("@Month2", txtmonth2.Text);
        cmd.Parameters.AddWithValue("@Month3", txtmonth3.Text);
        cmd.Parameters.AddWithValue("@Month4", txtmonths4.Text);
        cmd.Parameters.AddWithValue("@OverallEvaluation", txtOverallEvaluation.Text);
        cmd.Parameters.AddWithValue("@CreatedDate", "");

        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Save successfully.'); document.location.href='TeacherDairy.aspx';", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been saved successfully.');", true);
        ModalPopupExtender9.Hide();
    }

    protected void drpsemteacher_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpCoursecodeListTeachLoad();
        ModalPopupExtender13.Show();

    }

    protected void drpcoursecode_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        SqlCommand cmd = new SqlCommand("proc_GetSubject", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", Drpprogram.SelectedValue);
        cmd.Parameters.Add("@SemesterYear", drpsemteacher.SelectedItem.Text);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            DataTable dtnew = dt.Select("No_='" + drpcoursecode.Text + "'").CopyToDataTable();
            drpcoursName.Text = dtnew.Rows[0]["Details"].ToString();
           
        }
        ModalPopupExtender13.Show();
    }

    protected void drpprogramlessionplantheorycourse1_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpCoursecodeListLessionplansummarytheory();
        ModalPopupExtender16.Show();

    }

    protected void drpcoursecodelessionplantheorycourse1_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("proc_GetSubject", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpprogramlessionplantheorycourse1.SelectedValue);
        cmd.Parameters.Add("@SemesterYear", drpsemesterlessionplantheorycourse1.SelectedItem.Text);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            DataTable dtnew = dt.Select("No_='" + drpcoursecodelessionplantheorycourse1.Text + "'").CopyToDataTable();
            drpcoursenamelessionplantheorycourse1.Text = dtnew.Rows[0]["Details"].ToString();

        }
        ModalPopupExtender16.Show();
    }

    protected void drpsemesterlessionplantheorycourse1_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpCoursecodeListLessionplansummarytheory();
        ModalPopupExtender16.Show();

    }

    protected void drpprogramlecturewisetheorycourse1_SelectedIndexChanged(object sender, EventArgs e)
    {
       // bindLessionPlanLECTUREWISE();
        //ModalPopupExtender17.Show();
    }

    protected void drpsemesterlecturewisetheorycourse1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //bindLessionPlanLECTUREWISE();
       // ModalPopupExtender17.Show();
    }

    protected void drpcoursecodelecturewisetheorycourse1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //SqlCommand cmd = new SqlCommand("proc_GetSubject", con1);
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.Add("@CourseCode", drpprogramlecturewisetheorycourse1.SelectedValue);
        //cmd.Parameters.Add("@SemesterYear", drpsemesterlecturewisetheorycourse1.SelectedItem.Text);
        //cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //da.Fill(dt);
        //if (dt.Rows.Count > 0)
        //{
            //DataTable dtnew = dt.Select("No_='" + drpcoursecodelecturewisetheorycourse1.Text + "'").CopyToDataTable();
            //drpcoursenamelecturewisetheorycourse1.Text = dtnew.Rows[0]["Details"].ToString();

        }
        //ModalPopupExtender17.Show();

    //}

    //protected void drpprogramLaboratoryWork_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    bindLaboratoryWorkPlan();
    //   // ModalPopupExtender18.Show();
    //}

    protected void drpcoursecodeLaboratoryWork_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("proc_GetSubject", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.Add("@CourseCode", drpprogramLaboratoryWork.SelectedValue);
        //cmd.Parameters.Add("@SemesterYear", drpsemesterLaboratoryWork.SelectedItem.Text);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
           // DataTable dtnew = dt.Select("No_='" + drpcoursecodeLaboratoryWork.Text + "'").CopyToDataTable();
            //drpcoursenameLaboratoryWork.Text = dtnew.Rows[0]["Details"].ToString();

        }
        //ModalPopupExtender18.Show();

    }



    protected void drpsemesterLaboratoryWork_SelectedIndexChanged(object sender, EventArgs e)
    {
       // bindLaboratoryWorkPlan();
       // ModalPopupExtender18.Show();
    }
}















