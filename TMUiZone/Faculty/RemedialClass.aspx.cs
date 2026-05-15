using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;



public partial class Faculty_RemedialClass : System.Web.UI.Page
{
    Connection con;
    ServicePoratal PortalCon;
    string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;

    string constr1 = ConfigurationSettings.AppSettings["strPortal"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
               
                drpExammethod.Enabled = false;
                bindAcademicYear();
                bindDrpCourseList();
               
            }
        }
        catch (Exception)
        { }

    }
    public void SMS(String MobileNo, string Msg)
    {
        //  Website: http://www.universalsmsadvertising.com/     //  Username: 9837016352   //  Password: 9837016352 
        MobileNo = "91" + MobileNo;
        // MobileNo = "91" + 9015762885;
        string url = "http://www.universalsmsadvertising.com/universalsmsapi.php?user_name=9837016352&user_password=9837016352&mobile=" + MobileNo + "&sender_id=TMUniv&type=F&text=" + Msg + "";
        System.Net.HttpWebRequest fr;
        Uri targetURI = new Uri(url);
        fr = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(targetURI);
        if (fr.GetResponse().ContentLength > 0)
        {
            System.IO.StreamReader str = new System.IO.StreamReader(fr.GetResponse().GetResponseStream());
            Response.Write(str.ReadToEnd());
            str.Close();
        }
    }
   
    public void EmpmobilNo()
    {
        string tablenameemployeedata = "";
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        tablenameemployeedata = "[" + rccname + "$Employee" + "]";
        SqlDataReader dr = PortalCon.SHow_EmployeeMobileNo(ddFaculty.SelectedValue, tablenameemployeedata);

        string mobilnoemp = "";
        string facultyName = "";

        dr.Read();
        if (dr.HasRows)
        {
            mobilnoemp = dr["MobilePhoneNo"].ToString();
            facultyName = dr["faculty"].ToString();
            dr.Close();
            PortalCon.DisConnect();
            if (mobilnoemp.Trim() == "")
            { }
            else
            {
                try
                {

                    SMS(mobilnoemp, "Dear " + facultyName + "," + '\n' + "Your remedial class has been assigned on date from " + txtStartdate.Text + " to " + txtEnddate.Text + "");
                }
                catch (Exception)
                {

                }
            }
        }
        else
        {
            dr.Close();
            PortalCon.DisConnect();
        }
    }




    public void bindAcademicYear()
    {
        try
        {
            con = new Connection();
            SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con.Con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            ddAcademicyrs.DataSource = dt1;
            ddAcademicyrs.DataTextField = "Details";
            ddAcademicyrs.DataValueField = "No_";
            ddAcademicyrs.DataBind();
        }
        catch
        {
        }
    }
    string TypeOfCourse = "";




    public void bindDrpCourseList()
    {
        try
        {
            con = new Connection();
            SqlCommand cmd = new SqlCommand("proc_GetRCourse_RoleMatrix", con.Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Con.Open();
            da.Fill(dt);
            con.Con.Close();
            ddCourse.DataSource = dt;
            ddCourse.DataTextField = "Description";
            ddCourse.DataValueField = "Code";
            ddCourse.DataBind();
            ddCourse.Items.Insert(0, new ListItem("--Course--", "--Course--"));
           
        }
        catch
        {
        }
    }
    public void Semyear()
    {
        try
        {
            con = new Connection();
            SqlCommand cmd = new SqlCommand("proc_GetRSemYear_RoleMatrix", con.Con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CourseCode", ddCourse.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Con.Open();
            da.Fill(dt);
            con.Con.Close();
            ddSemester_Year.DataSource = dt;
            ddSemester_Year.DataTextField = "semcode";
            ddSemester_Year.DataValueField = "semcode";
            ddSemester_Year.DataBind();
            ddSemester_Year.Items.Insert(0, new ListItem("--Semester/Year--", "--Semester/Year--"));

        }
        catch
        {
        }

    }

    public void subject()
    {
        try
        {
            con = new Connection();
            SqlCommand cmd = new SqlCommand("proc_GetRSubject", con.Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CourseCode", ddCourse.SelectedValue);
            cmd.Parameters.Add("@SemYear", ddSemester_Year.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Con.Open();
            da.Fill(dt);
            con.Con.Close();
            ddSubject.DataSource = dt;
            ddSubject.DataTextField = "Description";
            ddSubject.DataValueField = "Code";
            ddSubject.DataBind();
            ddSubject.Items.Insert(0, new ListItem("--Subject--", "--Subject--"));
        }
        catch
        {
        }
    }

    public void Section()
    {
        try
        {
            con = new Connection();
            SqlCommand cmd = new SqlCommand("proc_GetRSection", con.Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CourseCode", ddCourse.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Con.Open();
            da.Fill(dt);
            con.Con.Close();
            ddSection.DataSource = dt;
            ddSection.DataTextField = "SectionCode";
            ddSection.DataValueField = "SectionCode";
            ddSection.DataBind();
            ddSection.Items.Insert(0, new ListItem("--Section--", ""));
        }
        catch
        {
        }
    }



    public void bindExamMethod()
    {

        try
        {
            con = new Connection();
            SqlCommand cmd = new SqlCommand("Sp_BindExam_Method", con.Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Acadyear", ddAcademicyrs.SelectedValue);
            cmd.Parameters.AddWithValue("@Subject", ddSubject.SelectedValue);
            cmd.Parameters.AddWithValue("@facultyCode", "");
            cmd.Parameters.AddWithValue("@Course", ddCourse.SelectedValue);
            cmd.Parameters.AddWithValue("@Sem", ddSemester_Year.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            drpExammethod.DataSource = dt;
            drpExammethod.DataTextField = "ExamMethod";
            drpExammethod.DataValueField = "code";
            drpExammethod.DataBind();
            if (chkevalution.Checked == true)
            {
                drpExammethod.Enabled = true;

            }
            else
            {
                drpExammethod.Enabled = false;

            }
        }
        catch
        {
        }
    }

    public void bindfaculty()
    {
        try
        {
            con = new Connection();
            SqlCommand cmd = new SqlCommand("proc_GetRFacultyList", con.Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CourseCode", ddCourse.SelectedValue);
            cmd.Parameters.Add("@SemYear", ddSemester_Year.SelectedValue);
            cmd.Parameters.Add("@Section", ddSection.SelectedValue);
            cmd.Parameters.Add("@Subject", ddSubject.SelectedValue);
            cmd.Parameters.Add("@AcademicYear", ddAcademicyrs.SelectedValue);
            cmd.Parameters.Add("@Exammathed", drpExammethod.SelectedValue);
            cmd.Parameters.Add("@FromDate", txtStartdate.Text);
            cmd.Parameters.Add("@ToDate", txtEnddate.Text);
            cmd.Parameters.Add("@HFrom", ddhournofrom.SelectedValue);
            cmd.Parameters.Add("@HTo", ddhourTillNo.SelectedValue);
            cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Con.Open();
            da.Fill(dt);
            con.Con.Close();
            ddFaculty.DataSource = dt;
            ddFaculty.DataTextField = "Faculty";
            ddFaculty.DataValueField = "Faculty Code";
            ddFaculty.DataBind();
            ddFaculty.Items.Insert(0, new ListItem("--Faculty--", "--Faculty--"));
        }
        catch
        {
        }
    }

  





    public void Show_CourseSubject()
    {
        try
        {
            con = new Connection();
            PortalCon = new ServicePoratal();
            string ccname = Session["Company"].ToString();
            string rccname = ccname.Replace(".", "_");
            string tblCourseCollege = "[" + rccname + "$Course - COLLEGE" + "]";
            string tblCourseSemester = "[" + rccname + "$Course Semester - COL" + "]";
            string tblCourseYear = "[" + rccname + "$Course Year - COL" + "]";
            string tblCourseSectionCOL = "[" + rccname + "$Course Section - COL" + "]";
            string tblSubjectCOLLEGE = "[" + rccname + "$Subject - COLLEGE" + "]";
            //string tblFaculty = "[" + rccname + "$Course Wise Faculty" + "]";
            string tblFaculty = "[" + rccname + "$Course Subject Line - COLLEGE" + "]";
            string tblStudentSubjectCollege = "[" + rccname + "$Student Subject - COLLEGE" + "]";
            string tblStudentOptionalSubject = "[" + rccname + "$Student Optional Subject - COL" + "]";
            string tblroomallocationcollege = "[" + rccname + "$Room Allocation - College" + "]";
            string tblEmployee = "[" + rccname + "$Employee" + "]";
            ddSubClassification.Items.Clear();
            SqlDataReader drclass = con.SHow_SubjectClassification(tblSubjectCOLLEGE, ddSubject.SelectedValue.Trim());
            ddSubClassification.DataSource = drclass;
            ddSubClassification.DataTextField = "SubClassification";
            ddSubClassification.DataBind();
            drclass.Close();
            con.DisConnect();
            if (ddSubject.SelectedItem.Text != "--Subject--")
            {

                txtsubjecttype.Text = "";
                SqlDataReader drSubtype = con.SHow_SubjectClassification1(tblSubjectCOLLEGE, ddSubject.SelectedValue.Trim(), ddCourse.SelectedValue);
                drSubtype.Read();
                if (drSubtype.HasRows)
                {
                    txtsubjecttype.Text = drSubtype["Subject Type"].ToString();
                    drSubtype.Close();
                    con.DisConnect();
                }
                else
                {
                    drSubtype.Close();
                    con.DisConnect();
                }
            }
       
            bindfaculty();
            //veerendra
            if (txtsubjecttype.Text.Trim() == "MAJOR")
            {
                SqlDataReader drstud = con.SHow_CourseDetails29StudentList(tblStudentSubjectCollege, ddCourse.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddSection.SelectedValue.Trim(), ddSubject.SelectedValue.Trim(), ddAcademicyrs.SelectedValue.Trim(), lblSemesateryear.Text.Trim(), drpExammethod.SelectedValue.Trim(),txtStartdate.Text,txtEnddate.Text,ddhournofrom.SelectedValue.Trim(),ddhourTillNo.SelectedValue.Trim());
                DataTable dt = new DataTable();
                dt.Load(drstud);
                grdListofStudent.DataSource = dt;
                grdListofStudent.DataBind();
                drstud.Close();
                con.DisConnect();

                SqlDataReader drRemedial = con.SHow_RemedialStudentData(tblStudentSubjectCollege, ddCourse.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddSection.SelectedValue.Trim(), ddSubject.SelectedValue.Trim());
                drRemedial.Read();
                if (drRemedial.HasRows)
                {
                    drRemedial.Close();
                    con.DisConnect();

                  
                    SqlDataReader drremport = PortalCon.Show_RemedialdataforDuplication(Session["Company"].ToString(), ddCourse.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddSection.SelectedValue.Trim(), ddSubject.SelectedValue.Trim(), Session["GlobalDimension1Code"].ToString().Trim(), txtsubjecttype.Text.Trim(), ddAcademicyrs.SelectedValue.Trim());
                    drremport.Read();
                    if (drremport.HasRows)
                    {



                        drremport.Close();
                        PortalCon.DisConnect();
                    }
                    else
                    {
                        drremport.Close();
                        PortalCon.DisConnect();
                    }

                }
                else
                {
                    drRemedial.Close();
                    con.DisConnect();

                 
                }


                SqlDataReader drChecked = con.SHow_CourseDetails29StudentList(tblStudentSubjectCollege, ddCourse.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddSection.SelectedValue.Trim(), ddSubject.SelectedValue.Trim(), ddAcademicyrs.SelectedValue.Trim(), lblSemesateryear.Text.Trim(), drpExammethod.SelectedValue.Trim(), txtStartdate.Text, txtEnddate.Text, ddhournofrom.SelectedValue.Trim(), ddhourTillNo.SelectedValue.Trim());
                drChecked.Read();
                if (drChecked.HasRows)
                {
                    drChecked.Close();
                    con.DisConnect();
                    btnselectchecked.Visible = true;
                    btnuncheked.Visible = false;
                }
                else
                {
                    drChecked.Close();
                    con.DisConnect();
                    btnselectchecked.Visible = false;
                    btnuncheked.Visible = false;

                }
            }

            //
            if (txtsubjecttype.Text.Trim() == "ELECTIVE")
            {
                SqlDataReader drstud = con.SHow_CourseDetails29StudentList(tblStudentOptionalSubject, ddCourse.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddSection.SelectedValue.Trim(), ddSubject.SelectedValue.Trim(), ddAcademicyrs.SelectedValue.Trim(), lblSemesateryear.Text.Trim(), drpExammethod.SelectedValue.Trim(), txtStartdate.Text, txtEnddate.Text, ddhournofrom.SelectedValue.Trim(), ddhourTillNo.SelectedValue.Trim());
                DataTable dt = new DataTable();
                dt.Load(drstud);
                grdListofStudent.DataSource = dt;
                grdListofStudent.DataBind();
                drstud.Close();
                con.DisConnect();

                SqlDataReader drRemedial = con.SHow_RemedialStudentData(tblStudentOptionalSubject, ddCourse.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddSection.SelectedValue.Trim(), ddSubject.SelectedValue.Trim());
                drRemedial.Read();
                if (drRemedial.HasRows)
                {
                    drRemedial.Close();
                    con.DisConnect();
                    SqlDataReader drremport = PortalCon.Show_RemedialdataforDuplication(Session["Company"].ToString(), ddCourse.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddSection.SelectedValue.Trim(), ddSubject.SelectedValue.Trim(), Session["GlobalDimension1Code"].ToString().Trim(), txtsubjecttype.Text.Trim(), ddAcademicyrs.SelectedValue.Trim());
                    drremport.Read();
                    if (drremport.HasRows)
                    {
                     


                        drremport.Close();
                        PortalCon.DisConnect();
                    }
                    else
                    {
                        drremport.Close();
                        PortalCon.DisConnect();
                    }

                }
                else
                {
                    drRemedial.Close();
                    con.DisConnect();

                 
                }


                SqlDataReader drChecked = con.SHow_CourseDetails29StudentList(tblStudentOptionalSubject, ddCourse.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddSection.SelectedValue.Trim(), ddSubject.SelectedValue.Trim(), ddAcademicyrs.SelectedValue.Trim(), lblSemesateryear.Text.Trim(), drpExammethod.SelectedValue.Trim(), txtStartdate.Text, txtEnddate.Text, ddhournofrom.SelectedValue.Trim(), ddhourTillNo.SelectedValue.Trim());
                drChecked.Read();
                if (drChecked.HasRows)
                {
                    drChecked.Close();
                    con.DisConnect();
                    btnselectchecked.Visible = true;
                    btnuncheked.Visible = false;
                }
                else
                {
                    drChecked.Close();
                    con.DisConnect();
                    btnselectchecked.Visible = false;
                    btnuncheked.Visible = false;

                }
            }
            if (txtsubjecttype.Text.Trim() == "")
            {
                SqlDataReader drstud = con.SHow_CourseDetails29StudentList(tblStudentOptionalSubject, ddCourse.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddSection.SelectedValue.Trim(), ddSubject.SelectedValue.Trim(), ddAcademicyrs.SelectedValue.Trim(), lblSemesateryear.Text.Trim(), drpExammethod.SelectedValue.Trim(), txtStartdate.Text, txtEnddate.Text, ddhournofrom.SelectedValue.Trim(), ddhourTillNo.SelectedValue.Trim());
                DataTable dt = new DataTable();
                dt.Load(drstud);
                grdListofStudent.DataSource = dt;
                grdListofStudent.DataBind();
                drstud.Close();
                con.DisConnect();

                SqlDataReader drRemedial = con.SHow_RemedialStudentData(tblStudentOptionalSubject, ddCourse.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddSection.SelectedValue.Trim(), ddSubject.SelectedValue.Trim());
                drRemedial.Read();
                if (drRemedial.HasRows)
                {
                    drRemedial.Close();
                    con.DisConnect();

    
                    SqlDataReader drremport = PortalCon.Show_RemedialdataforDuplication(Session["Company"].ToString(), ddCourse.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddSection.SelectedValue.Trim(), ddSubject.SelectedValue.Trim(), Session["GlobalDimension1Code"].ToString().Trim(), txtsubjecttype.Text.Trim(), ddAcademicyrs.SelectedValue.Trim());
                    drremport.Read();
                    if (drremport.HasRows)
                    {
                      


                        drremport.Close();
                        PortalCon.DisConnect();
                    }
                    else
                    {
                        drremport.Close();
                        PortalCon.DisConnect();
                    }

                }
                else
                {
                    drRemedial.Close();
                    con.DisConnect();

                
                }


                SqlDataReader drChecked = con.SHow_CourseDetails29StudentList(tblStudentOptionalSubject, ddCourse.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddSection.SelectedValue.Trim(), ddSubject.SelectedValue.Trim(), ddAcademicyrs.SelectedValue.Trim(), lblSemesateryear.Text.Trim(), drpExammethod.SelectedValue.Trim(), txtStartdate.Text, txtEnddate.Text, ddhournofrom.SelectedValue.Trim(), ddhourTillNo.SelectedValue.Trim());
                drChecked.Read();
                if (drChecked.HasRows)
                {
                    drChecked.Close();
                    con.DisConnect();
                    btnselectchecked.Visible = true;
                    btnuncheked.Visible = false;
                }
                else
                {
                    drChecked.Close();
                    con.DisConnect();
                    btnselectchecked.Visible = false;
                    btnuncheked.Visible = false;

                }
            }
        }
        catch
        {
        }
    }



    public void Show_CourseWiseData()
    {
        try
        {
            con = new Connection();
            PortalCon = new ServicePoratal();
            string ccname = Session["Company"].ToString();
            string rccname = ccname.Replace(".", "_");
            string tblCourseCollege = "[" + rccname + "$Course - COLLEGE" + "]";
            string tblCourseSemester = "[" + rccname + "$Course Semester - COL" + "]";
            string tblCourseYear = "[" + rccname + "$Course Year - COL" + "]";
            string tblCourseSectionCOL = "[" + rccname + "$Course Section - COL" + "]";
            string tblSubjectCOLLEGE = "[" + rccname + "$Subject - COLLEGE" + "]";
            //string tblFaculty = "[" + rccname + "$Course Wise Faculty" + "]";
            string tblFaculty = "[" + rccname + "$Course Subject Line - COLLEGE" + "]";
            string tblStudentSubjectCollege = "[" + rccname + "$Student Subject - COLLEGE" + "]";
            string tblStudentOptionalSubject = "[" + rccname + "$Student Optional Subject - COL" + "]";
            string tblroomallocationcollege = "[" + rccname + "$Room Allocation - College" + "]";

            string tblEmployee = "[" + rccname + "$Employee" + "]";
            ddRoomAllocation.Items.Clear();
            SqlDataReader drrom = con.SHow_RoomAllocation(tblroomallocationcollege, ddCourse.SelectedValue.Trim());
            ddRoomAllocation.DataSource = drrom;
            ddRoomAllocation.DataTextField = "RoomNo";

            ddRoomAllocation.DataBind();
            drrom.Close();
            con.DisConnect();
            ddRoomAllocation.Items.Insert(0, new ListItem("--Room--", "--Room--"));

            lblTypeofCourse.Text = "";
            SqlDataReader drc = con.SHow_CourseDetails29mSemYear(tblCourseCollege, ddCourse.SelectedValue.Trim());
            drc.Read();
            if (drc.HasRows)
            {
                TypeOfCourse = drc["Type Of Course"].ToString();
                lblTypeofCourse.Text = drc["Type Of Course"].ToString();
                drc.Read();
                con.DisConnect();
            }
            else
            {
                drc.Read();
                con.DisConnect();
            }
            //Semester 
            if (TypeOfCourse == "1")
            {
                lblSemester.Visible = true;
                lblYear.Visible = false;
                ddSemester_Year.Items.Clear();
                SqlDataReader drsemyer = con.SHow_CourseDetails29semester(tblCourseSemester, ddCourse.SelectedValue.Trim());
                ddSemester_Year.DataSource = drsemyer;
                ddSemester_Year.DataTextField = "semcode";
                ddSemester_Year.DataBind();
                drsemyer.Close();
                con.DisConnect();
                ddSemester_Year.Items.Insert(0, new ListItem("--Semester--", "--Semester--"));
                lblSemesateryear.Text = "Semester";
            }
            //Year
            if (TypeOfCourse == "2")
            {
                lblSemester.Visible = false;
                lblYear.Visible = true;
                ddSemester_Year.Items.Clear();
                SqlDataReader drsemyer = con.SHow_CourseDetails29CourseYear(tblCourseYear, ddCourse.SelectedValue.Trim());
                ddSemester_Year.DataSource = drsemyer;
                ddSemester_Year.DataTextField = "YearCode";
                ddSemester_Year.DataBind();
                drsemyer.Close();
                con.DisConnect();
                ddSemester_Year.Items.Insert(0, new ListItem("--Year--", "--Year--"));
                lblSemesateryear.Text = "Year";
            }
            if (TypeOfCourse == "0")
            {
                lblSemesateryear.Text = "Semester";
                lblSemester.Visible = false;
                lblYear.Visible = true;
                ddSemester_Year.Items.Clear();
                ddSemester_Year.Items.Insert(0, new ListItem("--Semester/Year--", "--Semester/Year--"));
            }
            if (TypeOfCourse == "")
            {
                lblSemesateryear.Text = "Semester";
                lblSemester.Visible = false;
                lblYear.Visible = true;
                ddSemester_Year.Items.Clear();
                ddSemester_Year.Items.Insert(0, new ListItem("--Semester/Year--", "--Semester/Year--"));

            }

            ddSection.Items.Clear();
            SqlDataReader drsec = con.SHow_CourseDetails29CourseSection(tblCourseSectionCOL, ddCourse.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), lblSemesateryear.Text.Trim());
            ddSection.DataSource = drsec;
            ddSection.DataTextField = "SectionCode";
            ddSection.DataValueField = "SectionCode";

            ddSection.DataBind();
            drsec.Close();
            con.DisConnect();
            ddSection.Items.Insert(0, new ListItem("--Section--", ""));
            ddSubject.Items.Clear();
            SqlDataReader drsub = con.SHow_CourseDetails29SubjectCOLLEGE(tblSubjectCOLLEGE, ddCourse.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), lblSemesateryear.Text);
            ddSubject.DataSource = drsub;
            ddSubject.DataTextField = "Description";
            ddSubject.DataValueField = "Code";
            ddSubject.DataBind();
            drsub.Close();
            con.DisConnect();
            ddSubject.Items.Insert(0, new ListItem("--Subject--", "--Subject--"));
            ddSubClassification.Items.Clear();
            SqlDataReader drclass = con.SHow_SubjectClassification(tblSubjectCOLLEGE, ddSubject.SelectedValue.Trim());
            ddSubClassification.DataSource = drclass;
            ddSubClassification.DataTextField = "SubClassification";

            ddSubClassification.DataBind();
            drclass.Close();
            con.DisConnect();

            if (ddSubject.SelectedItem.Text != "--Subject--")
            {
                txtsubjecttype.Text = "";
                SqlDataReader drSubtype = con.SHow_SubjectClassification1(tblSubjectCOLLEGE, ddSubject.SelectedValue.Trim(), ddCourse.SelectedValue);
                drSubtype.Read();
                if (drSubtype.HasRows)
                {
                    txtsubjecttype.Text = drSubtype["Subject Type"].ToString();
                    drSubtype.Close();
                    con.DisConnect();
                }
                else
                {
                    drSubtype.Close();
                    con.DisConnect();
                }
            }
            // ddFaculty.Items.Clear();
            //SqlDataReader drfac = con.SHow_CourseDetails29Faculty_Asu(tblFaculty, tblEmployee, ddCourse.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddSubject.SelectedValue.Trim(), ddSection.SelectedValue.Trim(), ddAcademicyrs.SelectedValue.Trim(), lblSemesateryear.Text.Trim());

            //ddFaculty.DataSource = drfac;
            //ddFaculty.DataTextField = "Faculty";
            //ddFaculty.DataValueField = "Faculty Code";
            //ddFaculty.DataBind();
            //drfac.Close();
            //con.DisConnect();
            //ddFaculty.Items.Insert(0, new ListItem("--Faculty--", "--Faculty--"));
            bindfaculty();

           
        }
        catch 
        {
        }
    }




    protected void ddCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        Show_CourseWiseData();
        Semyear();
        Section();
        clear();
        
    }
    protected void ddSemester_Year_SelectedIndexChanged(object sender, EventArgs e)
    {

        subject();
     
    }
    protected void ddSection_SelectedIndexChanged(object sender, EventArgs e)
    {

       // Show_CourseSection();
        
    }
    protected void ddSubject_SelectedIndexChanged(object sender, EventArgs e)
    {

        Show_CourseSubject();
        bindExamMethod();
        clear();

        
    }
    protected void ddFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
     //   Show_CourseSemesterYear();
       
    }

    protected void ddhournofrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        ValidationTimetablegeneration();
        bindfaculty();
        Show_CourseSubject();
    }
    protected void ddhourTillNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ValidationTimetablegeneration();
        bindfaculty();
        Show_CourseSubject();
    }
    protected void ddAcademicyrs_SelectedIndexChanged(object sender, EventArgs e)
    {
        Show_CourseSubject();
    }
  
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            int fromhour = Convert.ToInt32(ddhournofrom.SelectedValue.Trim());
            int Tohour = Convert.ToInt32(ddhourTillNo.SelectedValue.Trim());
            if (fromhour > Tohour)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please set valid hour no from and till');", true);
            }
            else
            {

                PortalCon = new ServicePoratal();
                con = new Connection();
                string ccname = Session["Company"].ToString();
                string rccname = ccname.Replace(".", "_");
                string tblStudentOptionalSubject = "[" + rccname + "$Student Optional Subject - COL" + "]";
                string tblStudentSubjectCollege = "[" + rccname + "$Student Subject - COLLEGE" + "]";
                string tblTimetablegenerationCol = "[" + rccname + "$Time Table Generation - COL" + "]";
                foreach (GridViewRow row in grdListofStudent.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {

                        CheckBox chkID = (row.Cells[0].FindControl("chkID") as CheckBox);

                        Label lblStudentNo = (row.Cells[0].FindControl("lblStudentNo") as Label);
                        Label lblEnrollMentNo = (row.Cells[0].FindControl("lblEnrollMentNo") as Label);
                        Label lblStudentName = (row.Cells[0].FindControl("lblStudentName") as Label);
                        if (chkID.Checked == true)
                        {
                            string section = "";
                            string coursename = ""; string subjectname = ""; string facultyName = ""; string StudentNo = ""; string EnrollmentNo = ""; string StudentName = "";
                            string fullcoursename = ddCourse.SelectedItem.Text.Trim();
                            string fullcourseCode = ddCourse.SelectedValue.Trim();
                            string coursename1 = fullcoursename.Replace(fullcourseCode, "");


                            string fullSubjectname = ddSubject.SelectedItem.Text.Trim();
                            string fullSubjectCode = ddSubject.SelectedValue.Trim();
                            string subjectname1 = fullSubjectname.Replace(fullSubjectCode, "");

                            string fullFacultyname = ddFaculty.SelectedItem.Text.Trim();
                            string fullFacultyCode = ddFaculty.SelectedValue.Trim();
                            string Facultyname1 = fullFacultyname.Replace(fullFacultyCode, "");
                            coursename = coursename1.Trim(); subjectname = subjectname1.Trim(); facultyName = Facultyname1.Trim(); StudentNo = lblStudentNo.Text.Trim(); EnrollmentNo = lblEnrollMentNo.Text.Trim(); StudentName = lblStudentName.Text.Trim();

                            //veerendra
                            if (chkevalution.Checked == true)
                            {


                                SqlDataReader dr = PortalCon.Show_tbl_RemedialClassRepetioncheck(ddCourse.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(),ddRoomAllocation.SelectedValue,ddhournofrom.SelectedValue,ddhourTillNo.SelectedValue,ddFaculty.SelectedValue.Trim(), ddSection.SelectedValue.Trim(), ddSubject.SelectedValue.Trim(), StudentNo.Trim(), Session["Company"].ToString(), txtsubjecttype.Text.Trim());
                                dr.Read();
                                if (dr.HasRows)
                                {
                                    dr.Close();
                                    PortalCon.DisConnect();
                                    if (txtsubjecttype.Text.Trim() == "MAJOR")
                                    {

                                        con.UpdateStudentSubjectCollegeDel(tblStudentSubjectCollege, "1", ddCourse.SelectedValue.Trim(), ddSection.SelectedValue.Trim(), ddSubject.SelectedValue.Trim(), StudentNo.Trim(), EnrollmentNo.Trim(), ddSemester_Year.SelectedValue.Trim());
                                        con.DisConnect();
                                    }
                                    if (txtsubjecttype.Text.Trim() == "ELECTIVE")
                                    {

                                        con.UpdateStudentSubjectCollegeDel(tblStudentOptionalSubject, "1", ddCourse.SelectedValue.Trim(), ddSection.SelectedValue.Trim(), ddSubject.SelectedValue.Trim(), StudentNo.Trim(), EnrollmentNo.Trim(), ddSemester_Year.SelectedValue.Trim());
                                        con.DisConnect();
                                    }
                                    SqlDataReader drTimetable = con.SHow_TimeTableGeneration(tblTimetablegenerationCol, ddSubject.SelectedValue.Trim(), ddCourse.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddFaculty.SelectedValue.Trim(), ddAcademicyrs.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddRoomAllocation.SelectedValue.Trim(), txtStartdate.Text, txtEnddate.Text, ddhournofrom.SelectedValue.Trim(), ddhourTillNo.SelectedValue.Trim());

                                    drTimetable.Read();
                                    if (drTimetable.HasRows)
                                    {
                                        drTimetable.Close();
                                        con.DisConnect();
                                    }

                                    else
                                    {
                                        drTimetable.Close();
                                        con.DisConnect();
                                        saveTimetablegeneration(subjectname.Trim(), facultyName.Trim());
                                    }

                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Saved Successfully.....');", true);
                                  
                                }
                                else
                                {
                                    dr.Close();
                                    PortalCon.DisConnect();
                                    try
                                    {
                                        section = ddSection.SelectedValue.ToString();
                                    }
                                    catch (Exception)
                                    {
                                        section = "";
                                    }
                                    PortalCon.Connect();
                                    PortalCon.Insert_tbl_RemedialClass(ddCourse.SelectedValue.ToString().Trim(), coursename.Trim(), ddSemester_Year.SelectedItem.Text.Trim().Trim(), section, ddSubject.SelectedValue.ToString().Trim(), subjectname.Trim(), ddFaculty.SelectedValue.Trim(), facultyName.Trim(), txtStartdate.Text.Trim(), txtEnddate.Text.Trim(), ddhournofrom.SelectedValue.Trim(), ddhourTillNo.SelectedValue.Trim(), StudentNo.Trim(), EnrollmentNo.Trim(), StudentName.Trim(), System.DateTime.Now.ToString(), Session["uid"].ToString(), Session["uname"].ToString(), Session["Company"].ToString().Trim(), ddAcademicyrs.SelectedItem.Text.Trim(), txtsubjecttype.Text.Trim(), Session["GlobalDimension1Code"].ToString().Trim(), lblTypeofCourse.Text.Trim(), ddRoomAllocation.SelectedValue.Trim(), ddSubClassification.SelectedValue.Trim(),drpExammethod.SelectedValue.Trim());
                                    PortalCon.DisConnect();


                                    if (txtsubjecttype.Text.Trim() == "MAJOR")
                                    {

                                        con.UpdateStudentSubjectCollegeDel(tblStudentSubjectCollege, "1", ddCourse.SelectedValue.Trim(), ddSection.SelectedValue.Trim(), ddSubject.SelectedValue.Trim(), StudentNo.Trim(), EnrollmentNo.Trim(), ddSemester_Year.SelectedValue.Trim());
                                        con.DisConnect();
                                    }
                                    if (txtsubjecttype.Text.Trim() == "ELECTIVE")
                                    {

                                        con.UpdateStudentSubjectCollegeDel(tblStudentOptionalSubject, "1", ddCourse.SelectedValue.Trim(), ddSection.SelectedValue.Trim(), ddSubject.SelectedValue.Trim(), StudentNo.Trim(), EnrollmentNo.Trim(), ddSemester_Year.SelectedValue.Trim());
                                        con.DisConnect();
                                    }

                                    SqlDataReader drTimetable = con.SHow_TimeTableGeneration(tblTimetablegenerationCol, ddSubject.SelectedValue.Trim(), ddCourse.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddFaculty.SelectedValue.Trim(), ddAcademicyrs.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddRoomAllocation.SelectedValue.Trim(), txtStartdate.Text, txtEnddate.Text, ddhournofrom.SelectedValue.Trim(), ddhourTillNo.SelectedValue.Trim());
                                    drTimetable.Read();
                                    if (drTimetable.HasRows)
                                    {
                                        drTimetable.Close();
                                        con.DisConnect();
                                    }

                                    else
                                    {
                                        drTimetable.Close();
                                        con.DisConnect();
                                        saveTimetablegeneration(subjectname.Trim(), facultyName.Trim());
                                    }

                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Saved Successfully.....');", true);
                                  


                                }


                            }
                            else
                            {// end veerendra

                                SqlDataReader dr = PortalCon.Show_tbl_RemedialClassRepetioncheck(ddCourse.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddRoomAllocation.SelectedValue, ddhournofrom.SelectedValue, ddhourTillNo.SelectedValue, ddFaculty.SelectedValue.Trim(), ddSection.SelectedValue.Trim(), ddSubject.SelectedValue.Trim(), StudentNo.Trim(), Session["Company"].ToString(), txtsubjecttype.Text.Trim());
                                dr.Read();
                                if (dr.HasRows)
                                {
                                    dr.Close();
                                    PortalCon.DisConnect();
                                    if (txtsubjecttype.Text.Trim() == "MAJOR")
                                    {

                                        con.UpdateStudentSubjectCollegeDel(tblStudentSubjectCollege, "1", ddCourse.SelectedValue.Trim(), ddSection.SelectedValue.Trim(), ddSubject.SelectedValue.Trim(), StudentNo.Trim(), EnrollmentNo.Trim(), ddSemester_Year.SelectedValue.Trim());
                                        con.DisConnect();
                                    }
                                    if (txtsubjecttype.Text.Trim() == "ELECTIVE")
                                    {

                                        con.UpdateStudentSubjectCollegeDel(tblStudentOptionalSubject, "1", ddCourse.SelectedValue.Trim(), ddSection.SelectedValue.Trim(), ddSubject.SelectedValue.Trim(), StudentNo.Trim(), EnrollmentNo.Trim(), ddSemester_Year.SelectedValue.Trim());
                                        con.DisConnect();
                                    }
                                    SqlDataReader drTimetable = con.SHow_TimeTableGeneration(tblTimetablegenerationCol, ddSubject.SelectedValue.Trim(), ddCourse.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddFaculty.SelectedValue.Trim(), ddAcademicyrs.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddRoomAllocation.SelectedValue.Trim(), txtStartdate.Text, txtEnddate.Text, ddhournofrom.SelectedValue.Trim(), ddhourTillNo.SelectedValue.Trim());

                                    drTimetable.Read();
                                    if (drTimetable.HasRows)
                                    {
                                        drTimetable.Close();
                                        con.DisConnect();
                                    }

                                    else
                                    {
                                        drTimetable.Close();
                                        con.DisConnect();
                                        saveTimetablegeneration(subjectname.Trim(), facultyName.Trim());
                                    }

                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Saved Successfully.....');", true);
                                   
                                }
                                else
                                {
                                    dr.Close();
                                    PortalCon.DisConnect();
                                    try
                                    {
                                        section = ddSection.SelectedValue.ToString();
                                    }
                                    catch (Exception)
                                    {
                                        section = "";
                                    }
                                    PortalCon.Connect();
                                    PortalCon.Insert_tbl_RemedialClass(ddCourse.SelectedValue.ToString().Trim(), coursename.Trim(), ddSemester_Year.SelectedItem.Text.Trim().Trim(), section, ddSubject.SelectedValue.ToString().Trim(), subjectname.Trim(), ddFaculty.SelectedValue.Trim(), facultyName.Trim(), txtStartdate.Text.Trim(), txtEnddate.Text.Trim(), ddhournofrom.SelectedValue.Trim(), ddhourTillNo.SelectedValue.Trim(), StudentNo.Trim(), EnrollmentNo.Trim(), StudentName.Trim(), System.DateTime.Now.ToString(), Session["uid"].ToString(), Session["uname"].ToString(), Session["Company"].ToString().Trim(), ddAcademicyrs.SelectedItem.Text.Trim(), txtsubjecttype.Text.Trim(), Session["GlobalDimension1Code"].ToString().Trim(), lblTypeofCourse.Text.Trim(), ddRoomAllocation.SelectedValue.Trim(), ddSubClassification.SelectedValue.Trim(), drpExammethod.SelectedValue.Trim());
                                    PortalCon.DisConnect();


                                    if (txtsubjecttype.Text.Trim() == "MAJOR")
                                    {

                                        con.UpdateStudentSubjectCollegeDel(tblStudentSubjectCollege, "1", ddCourse.SelectedValue.Trim(), ddSection.SelectedValue.Trim(), ddSubject.SelectedValue.Trim(), StudentNo.Trim(), EnrollmentNo.Trim(), ddSemester_Year.SelectedValue.Trim());
                                        con.DisConnect();
                                    }
                                    if (txtsubjecttype.Text.Trim() == "ELECTIVE")
                                    {

                                        con.UpdateStudentSubjectCollegeDel(tblStudentOptionalSubject, "1", ddCourse.SelectedValue.Trim(), ddSection.SelectedValue.Trim(), ddSubject.SelectedValue.Trim(), StudentNo.Trim(), EnrollmentNo.Trim(), ddSemester_Year.SelectedValue.Trim());
                                        con.DisConnect();
                                    }

                                    SqlDataReader drTimetable = con.SHow_TimeTableGeneration(tblTimetablegenerationCol, ddSubject.SelectedValue.Trim(), ddCourse.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddFaculty.SelectedValue.Trim(), ddAcademicyrs.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddRoomAllocation.SelectedValue.Trim(), txtStartdate.Text, txtEnddate.Text, ddhournofrom.SelectedValue.Trim(), ddhourTillNo.SelectedValue.Trim());
                                    drTimetable.Read();
                                    if (drTimetable.HasRows)
                                    {
                                        drTimetable.Close();
                                        con.DisConnect();
                                    }

                                    else
                                    {
                                        drTimetable.Close();
                                        con.DisConnect();
                                        saveTimetablegeneration(subjectname.Trim(), facultyName.Trim());
                                    }

                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Saved Successfully.....');", true);



                                }
                            }
                        }
                    }
                }

              
            }
            bindDrpCourseList();
            bindfaculty();
            txtStartdate.Text = "";
            txtEnddate.Text = "";
            drpExammethod.SelectedIndex = -1;
            ddhourTillNo.SelectedIndex = -1;
            ddhournofrom.SelectedIndex = -1;
            ddRoomAllocation.SelectedIndex = -1;
            subject();
            Show_CourseSubject();

            EmpmobilNo();
        }
        catch
        {
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewRemedialClass.aspx");
    }
    private void ToggleCheckState(bool checkState)
    {

        foreach (GridViewRow row in grdListofStudent.Rows)
        {

            CheckBox cb = (CheckBox)row.FindControl("chkID");
            if (cb != null)
                cb.Checked = checkState;
        }
    }

    protected void btnselectchecked_Click(object sender, EventArgs e)
    {
        ToggleCheckState(true);
        btnselectchecked.Visible = false;
        btnuncheked.Visible = true;
    }
    protected void btnuncheked_Click(object sender, EventArgs e)
    {
        ToggleCheckState(false);
        btnselectchecked.Visible = true;
        btnuncheked.Visible = false;
    }
    protected void txtEnddate_TextChanged(object sender, EventArgs e)
    {
        DateTime Stime = System.DateTime.Today;
        if (txtEnddate.Text != "")
        {
            DateTime endDateTime = Convert.ToDateTime(txtEnddate.Text);

            if (Stime > endDateTime)
            {
                txtEnddate.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('End date should not be less than current date');", true);
                return;
            }
        }

        if (txtEnddate.Text != "" && txtStartdate.Text != "")
        {
            DateTime endDateTime = Convert.ToDateTime(txtEnddate.Text);
            DateTime startDateTime = Convert.ToDateTime(txtStartdate.Text);
            if (endDateTime < startDateTime)
            {
                txtEnddate.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('End date should always be greater than from Date');", true);
                return;
            }

            else
            {
            }
        }




        datevalidate();
        ValidationTimetablegeneration();
        bindfaculty();
        Show_CourseSubject();
    }

    public void datevalidate()
    {
        try
        {
            DateTime frodatecom = DateTime.ParseExact(txtStartdate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime Todatecom = DateTime.ParseExact(txtEnddate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
            if (frodatecom > Todatecom)
            {
                txtEnddate.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('To Date should always be greater than From Date');", true);

            }
        }
        catch (Exception)
        { 
        
        }
    }
    protected void txtStartdate_TextChanged(object sender, EventArgs e)
    {
        DateTime Stime = System.DateTime.Today;
        if (txtStartdate.Text != "")
        {
            DateTime startDateTime = Convert.ToDateTime(txtStartdate.Text);

            if (Stime > startDateTime)
            {
                txtStartdate.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Start date should not be less than current date');", true);
                return;
            }
        }





        if (txtStartdate.Text != "" && txtEnddate.Text != "")
        {

            DateTime endDateTime = Convert.ToDateTime(txtEnddate.Text);
            DateTime startDateTime = Convert.ToDateTime(txtStartdate.Text);
            if (endDateTime < startDateTime)
            {
                txtStartdate.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Start Date should not be Less than End Date');", true);
                return;
            }

            else
            {
            }
        }



        datevalidate();
        ValidationTimetablegeneration();
        bindfaculty();
        Show_CourseSubject();
    }

    
    public void GenerateEntryNo()
    {
        try
        {
            string ccname = Session["Company"].ToString();
            string rccname = ccname.Replace(".", "_");
            string tbltimetablegenerationCol = "[" + rccname + "$Time Table Generation - COL" + "]";
            con = new Connection();
            SqlDataReader dr = con.SHow_EntryNoAuto(tbltimetablegenerationCol);
            dr.Read();

            if (dr.HasRows)
            {

                string d = dr["Entry No"].ToString();
                if (d == "")
                {
                    d = "0";
                }
                double rec = Convert.ToInt64(d);
                double c1 = rec + 1;

                lblEntryNo.Text = c1.ToString();
            }

            else
            {

                lblEntryNo.Text = "1";


            }

            dr.Close();
            con.DisConnect();
        }
        catch
        {
        }

    }

   
   

    public void saveTimetablegeneration(string subjectnamedd,string facultyname)
    {

        try
        {
            string ccname = Session["Company"].ToString();
            string rccname = ccname.Replace(".", "_");

            string tblTimetablegenerationCol = "[" + rccname + "$Time Table Generation - COL" + "]";

            int hourfrom = Convert.ToInt32(ddhournofrom.SelectedValue.Trim());
            int hourTo = Convert.ToInt32(ddhourTillNo.SelectedValue.Trim());
            string dayno = ""; string DayOfWeek = "";
            DateTime frodatecom = Convert.ToDateTime(txtStartdate.Text);// DateTime.ParseExact(txtStartdate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime Todatecom = Convert.ToDateTime(txtEnddate.Text); //DateTime.ParseExact(txtEnddate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan difference = Todatecom - frodatecom;
            string totalnoofdyas = difference.TotalDays.ToString();
            int totalnoofdyas1 = Convert.ToInt32(totalnoofdyas) + 1;
            while (hourfrom <= hourTo)
            {


                while (frodatecom <= Todatecom)
                {
                    string frodatecom1 = frodatecom.ToString("dd MMM yyyy");
                    DateTime datefordayofweek = DateTime.ParseExact(frodatecom1.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DayOfWeek = datefordayofweek.DayOfWeek.ToString();
                    if (DayOfWeek == "Sunday")
                    {
                        dayno = "7";
                    }
                    if (DayOfWeek == "Monday")
                    {
                        dayno = "1";
                    }
                    if (DayOfWeek == "Tuesday")
                    {
                        dayno = "2";
                    }
                    if (DayOfWeek == "Wednesday")
                    {
                        dayno = "3";
                    }
                    if (DayOfWeek == "Thursday")
                    {
                        dayno = "4";
                    }
                    if (DayOfWeek == "Friday")
                    {
                        dayno = "5";
                    }
                    if (DayOfWeek == "Saturday")
                    {
                        dayno = "6";
                    }
                    if (dayno == "7")
                    { }

                    if (dayno != "7")
                    {

                        string hourNo = hourfrom.ToString(); string attendancedate = frodatecom1.ToString();
                        con.InsertTimeTableGeneration(tblTimetablegenerationCol, dayno, hourNo, ddSubject.SelectedValue.Trim(), ddCourse.SelectedValue.Trim(), ddSemester_Year.SelectedValue.Trim(), ddFaculty.SelectedValue.Trim(), ddAcademicyrs.SelectedValue.Trim(), ddSection.SelectedValue.Trim(), txtsubjecttype.Text.Trim(), facultyname.ToString().Trim(), subjectnamedd.Trim(), Session["GlobalDimension1Code"].ToString().Trim(), "", lblTypeofCourse.Text.Trim(), ddSemester_Year.SelectedValue.Trim(), "", ddRoomAllocation.SelectedValue.Trim(), ddSubClassification.SelectedValue.Trim(), attendancedate.Trim(), "", "", "", "", "", "", "1");
                        con.DisConnect();
                    }

                    frodatecom = frodatecom.AddDays(1);

                }
                hourfrom = hourfrom + 1;
                frodatecom = frodatecom.AddDays(-totalnoofdyas1);
            }


        }
        catch
        {
        }
    }



    public void ValidationTimetablegeneration()
    {
        try
        {
            con = new Connection();

            string ccname = Session["Company"].ToString();
            string rccname = ccname.Replace(".", "_");

            string tblTimetablegenerationCol = "[" + rccname + "$Time Table Generation - COL" + "]";

            int hourfrom = Convert.ToInt32(ddhournofrom.SelectedValue.Trim());
            int hourTo = Convert.ToInt32(ddhourTillNo.SelectedValue.Trim());
            string dayno = ""; string DayOfWeek = "";
            DateTime frodatecom = DateTime.ParseExact(txtStartdate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime Todatecom = DateTime.ParseExact(txtEnddate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan difference = Todatecom - frodatecom;
            string totalnoofdyas = difference.TotalDays.ToString();
            int totalnoofdyas1 = Convert.ToInt32(totalnoofdyas) + 1;
            while (hourfrom <= hourTo)
            {


                while (frodatecom <= Todatecom)
                {
                    string frodatecom1 = frodatecom.ToString("dd MMM yyyy");
                    DateTime datefordayofweek = DateTime.ParseExact(frodatecom1.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DayOfWeek = datefordayofweek.DayOfWeek.ToString();
                    if (DayOfWeek == "Sunday")
                    {
                        dayno = "7";
                    }
                    if (DayOfWeek == "Monday")
                    {
                        dayno = "1";
                    }
                    if (DayOfWeek == "Tuesday")
                    {
                        dayno = "2";
                    }
                    if (DayOfWeek == "Wednesday")
                    {
                        dayno = "3";
                    }
                    if (DayOfWeek == "Thursday")
                    {
                        dayno = "4";
                    }
                    if (DayOfWeek == "Friday")
                    {
                        dayno = "5";
                    }
                    if (DayOfWeek == "Saturday")
                    {
                        dayno = "6";
                    }
                    if (dayno == "7")
                    { }

                    if (dayno != "7")
                    {

                        SqlDataReader dr = con.SHow_TimeTableGenerationHourValidation(tblTimetablegenerationCol, ddhournofrom.SelectedValue.Trim(), ddhourTillNo.SelectedValue.Trim(), frodatecom1.Trim(), ddRoomAllocation.SelectedValue.Trim());
                        dr.Read();
                        if (dr.HasRows)
                        {
                            dr.Close();
                            con.DisConnect();
                            ddhourTillNo.SelectedValue = "";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Slot is not available');", true);

                        }
                        else
                        {
                            dr.Close();
                            con.DisConnect();
                        }
                   
                    }

                    frodatecom = frodatecom.AddDays(1);



                }
                hourfrom = hourfrom + 1;
                frodatecom = frodatecom.AddDays(-totalnoofdyas1);
            }
        }
        catch (Exception)
        { 
        
        
        }

    }

     
    protected void drpExammethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (chkevalution.Checked == true)
        {

            Show_CourseSubject();// 16-01-2019
            drpExammethod.Enabled = true;
            txtStartdate.Text = "";
            txtEnddate.Text = "";

            ddhourTillNo.SelectedIndex = -1;
            ddhournofrom.SelectedIndex = -1;
            ddRoomAllocation.SelectedIndex = -1;
            bindfaculty();
        }
        else
        {
            drpExammethod.Enabled = false;

        }
    }

    protected void chkevalution_CheckedChanged(object sender, EventArgs e)
    {
        if (chkevalution.Checked == true)
        {

            Show_CourseSubject();// 16-01-2019
            drpExammethod.Enabled = true;

            txtStartdate.Text = "";
            txtEnddate.Text = "";
            drpExammethod.SelectedIndex = -1;
            ddhourTillNo.SelectedIndex = -1;
            ddhournofrom.SelectedIndex = -1;
            ddRoomAllocation.SelectedIndex = -1;
           bindfaculty();
        }
        else
        {
            bindExamMethod();
            Show_CourseSubject();
            drpExammethod.Enabled = false;

        }
    }

    public void clear()
    {
        txtStartdate.Text = "";
        txtEnddate.Text = "";

        ddhourTillNo.SelectedIndex = -1;
        ddhournofrom.SelectedIndex = -1;
        ddRoomAllocation.SelectedIndex = -1;
    }
}