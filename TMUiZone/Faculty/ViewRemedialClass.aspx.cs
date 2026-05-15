using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Configuration;
using System.IO;



public partial class Faculty_ViewRemedialClass : System.Web.UI.Page
{
    ServicePoratal PortalCon;
    Connection con;
    string constr1 = ConfigurationSettings.AppSettings["strPortal"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ShowData();
                binddata();
            }
        }
        catch (Exception)
        {
            Response.Redirect("../Default.aspx");
        }
    }

    public void binddata()
    {
        try
        {
            con = new Connection();
            SqlCommand cmd = new SqlCommand("GetRemidialData", con.Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Collegecode", Session["GlobalDimension1Code"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            con.Con.Open();
            da.Fill(dt1);
           
            grdListofStudent.DataSource = dt1;
            grdListofStudent.DataBind();
            ViewState["SubjectList"] = dt1;
            con.Con.Close();
       
        }
        catch
        {
        }
    }
    public void ShowData()
    {
        //PortalCon = new ServicePoratal();
        //SqlDataReader dr1 = PortalCon.Show_tbl_RemedialClassRepetionView(Session["Company"].ToString(), Session["GlobalDimension1Code"].ToString().Trim());
      
        //DataTable dt = new DataTable();
        //dt.Load(dr1);
        //grdListofStudent.DataSource = dt;
        //grdListofStudent.DataBind();
        //dr1.Close();
        //PortalCon.DisConnect();
        //ViewState["SubjectList"] = dt;
    }
    protected void grdListofStudent_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdListofStudent.PageIndex = e.NewPageIndex;
        filterGrid();

    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        Response.Redirect("RemedialClass.aspx");
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        string tblStudentSubjectCollege = "[" + rccname + "$Student Subject - COLLEGE" + "]";
        string tblStudentOptionalSubject = "[" + rccname + "$Student Optional Subject - COL" + "]";

        string tblStudentAttendaceHeader = "[" + rccname + "$Student Attendance Header -COL" + "]";
        string tblStudentAttendaceLine = "[" + rccname + "$Student Attendance Line - COL" + "]";

        string id = e.CommandArgument.ToString();
        PortalCon = new ServicePoratal();
        con = new Connection();
        SqlDataReader dr2 = PortalCon.Show_tbl_RemedialClassRepetionViewID(id);
        dr2.Read();
        if (dr2.HasRows)
        {

            string Course = dr2["Course"].ToString();
            string Semester = dr2["Semester"].ToString();
            string SubjectType = dr2["Subject Type"].ToString();
            string Section = dr2["Section"].ToString();
            string SubjectCode = dr2["Subject"].ToString();
            string StudentNo = dr2["Student No"].ToString();
            string EnrollmentNo = dr2["Enrollment No"].ToString();
            string Subjecttype = dr2["Subject Type"].ToString();
            string FacultyCode = dr2["Faculty Code"].ToString();
            string AcademicYear = dr2["Academic Year"].ToString();
            string RoomAllocation = dr2["Room Allocation"].ToString();
            string GlobalDimension1 = dr2["Global Dimension 1"].ToString();

            dr2.Close();
            PortalCon.DisConnect();
            PortalCon.delete_tbl_RemedialClass(id);
            PortalCon.DisConnect();
            if (SubjectType == "MAJOR")
            {
                con.UpdateStudentSubjectCollegeDel(tblStudentSubjectCollege, "0", Course.Trim(), Section.Trim(), SubjectCode.Trim(), StudentNo.Trim(), EnrollmentNo.Trim(), Semester.Trim());
                con.DisConnect();
            }
            if (SubjectType == "ELECTIVE")
            {
                con.UpdateStudentSubjectCollegeDel(tblStudentOptionalSubject, "0", Course.Trim(), Section.Trim(), SubjectCode.Trim(), StudentNo.Trim(), EnrollmentNo.Trim(), Semester.Trim());
                con.DisConnect();
            }

            deleteFromtimetablegeneration(Course, Semester, Section, SubjectCode, Subjecttype, FacultyCode, AcademicYear, RoomAllocation);

        }
        else
        {
            dr2.Close();
            PortalCon.DisConnect();
        }

        filterGrid();
    }

    public void deleteFromtimetablegeneration(string course,string Semester,string Section,string Subject, string SubjectType,string faculty,string academicyrs,string rooallocation)
    {
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        string tblTimetablegenerationCol = "[" + rccname + "$Time Table Generation - COL" + "]";
      
        con = new Connection();
        PortalCon = new ServicePoratal();
        SqlDataReader dr = PortalCon.Show_RemedialdataforDuplication(Session["Company"].ToString().Trim(), course.Trim(), Semester.Trim(), Section.Trim(), Subject.Trim(), Session["GlobalDimension1Code"].ToString().Trim(), SubjectType.Trim(), academicyrs.Trim());
        dr.Read();
        if (dr.HasRows)
        {
            dr.Close();
            PortalCon.DisConnect();
        }
        else
        {
            dr.Close();
            PortalCon.DisConnect();

            con.Delete_TimeTableGeneration(tblTimetablegenerationCol, Subject, course, Semester, faculty, academicyrs, Semester, rooallocation);
            con.DisConnect();
        }
    }

    protected void rdCourse_CheckedChanged(object sender, EventArgs e)
    {
        ddFilterdata.Visible = true;
        Course();
        filterGrid();
        
    }
   
    protected void rdStudentNo_CheckedChanged(object sender, EventArgs e)
    {
        ddFilterdata.Visible = true;
        StudentNo();
        filterGrid();
    }
    protected void rdEnrollmentno_CheckedChanged(object sender, EventArgs e)
    {
        ddFilterdata.Visible = true;
        EnrollmentNo();
        filterGrid();
    }
    public void Course()
    {
        PortalCon = new ServicePoratal();

        ddFilterdata.Items.Clear();
        SqlDataReader dr3 = PortalCon.Show_tbl_RemedialClassRepetionView_Course(Session["Company"].ToString(), Session["GlobalDimension1Code"].ToString().Trim());


        ddFilterdata.DataSource = dr3;
        ddFilterdata.DataTextField = "CourseName";
        ddFilterdata.DataValueField = "Course";
        ddFilterdata.DataBind();
        dr3.Close();
        PortalCon.DisConnect();
    }



    public void Subject()
    {

        PortalCon = new ServicePoratal();
        ddFilterdata.Items.Clear();
        SqlDataReader dr4 = PortalCon.Show_tbl_RemedialClassRepetionView_Subject(Session["Company"].ToString(), Session["GlobalDimension1Code"].ToString().Trim());


        ddFilterdata.DataSource = dr4;
        ddFilterdata.DataTextField = "SubjectName";
        ddFilterdata.DataValueField = "Subject";
        ddFilterdata.DataBind();
        dr4.Close();
        PortalCon.DisConnect();
    }

    public void StudentNo()
    {

        PortalCon = new ServicePoratal();
        ddFilterdata.Items.Clear();
        SqlDataReader dr5 = PortalCon.Show_tbl_RemedialClassRepetionView_StudentNo(Session["Company"].ToString(), Session["GlobalDimension1Code"].ToString().Trim());


        ddFilterdata.DataSource = dr5;
        ddFilterdata.DataTextField = "StudentName";
        ddFilterdata.DataValueField = "Student No";
        ddFilterdata.DataBind();
        dr5.Close();
        PortalCon.DisConnect();
    }

    public void EnrollmentNo()
    {

        PortalCon = new ServicePoratal();
        ddFilterdata.Items.Clear();
        SqlDataReader dr6 = PortalCon.Show_tbl_RemedialClassRepetionView_EnrollmentNo(Session["Company"].ToString(), Session["GlobalDimension1Code"].ToString().Trim());


        ddFilterdata.DataSource = dr6;
        ddFilterdata.DataTextField = "StudentName";
        ddFilterdata.DataValueField = "Enrollment No";
        ddFilterdata.DataBind();
        dr6.Close();
        PortalCon.DisConnect();
    }

    protected void rdSubject_CheckedChanged(object sender, EventArgs e)
    {
        ddFilterdata.Visible = true;
        Subject();
        filterGrid();
    }
   
    protected void ddFilterdata_SelectedIndexChanged(object sender, EventArgs e)
    {
        filterGrid();
    }
    SqlDataReader dr;
    public void filterGrid()
    {
        try
        {
            PortalCon = new ServicePoratal();
            if (rdCourse.Checked == true)
            {
              
                dr = PortalCon.Show_tbl_RemedialClassRepetionView_Course_Grid(Session["Company"].ToString().Trim(), ddFilterdata.SelectedValue.ToString().Trim(), Session["GlobalDimension1Code"].ToString().Trim());

            }
            if (rdSubject.Checked == true)
            {
                
                dr = PortalCon.Show_tbl_RemedialClassRepetionView_Subject_Grid(Session["Company"].ToString().Trim(), ddFilterdata.SelectedValue.ToString().Trim(), Session["GlobalDimension1Code"].ToString().Trim());

            }
            if (rdStudentNo.Checked == true)
            {
                dr = PortalCon.Show_tbl_RemedialClassRepetionView_Student_Grid(Session["Company"].ToString().Trim(), ddFilterdata.SelectedValue.ToString().Trim(), Session["GlobalDimension1Code"].ToString().Trim());

            }
            if (rdEnrollmentno.Checked == true)
            {
                dr = PortalCon.Show_tbl_RemedialClassRepetionView_EnrollmentNo_Grid(Session["Company"].ToString(), ddFilterdata.SelectedValue.ToString().Trim(), Session["GlobalDimension1Code"].ToString().Trim());

            }
            if (rdFaculty.Checked == true)
            {
                dr = PortalCon.Show_tbl_RemedialClassRepetionView_Faculty_Grid(Session["Company"].ToString().Trim(), ddFilterdata.SelectedValue.ToString().Trim(), Session["GlobalDimension1Code"].ToString().Trim());

            }
            DataTable dt = new DataTable();
            dt.Load(dr);
            grdListofStudent.DataSource = dt;
            grdListofStudent.DataBind();
            dr.Close();
            PortalCon.DisConnect();
        }
        catch (Exception)
        {
            //dr.Close();
            //PortalCon.DisConnect();

        }
        if (rdCourse.Checked == false && rdSubject.Checked == false && rdStudentNo.Checked == false && rdEnrollmentno.Checked == false && rdFaculty.Checked == false)
        {
            ShowData();
        }

    }
    protected void rdFaculty_CheckedChanged(object sender, EventArgs e)
    {
        ddFilterdata.Visible = true;
        Faculty();
        filterGrid();
    }

    public void Faculty()
    {

        PortalCon = new ServicePoratal();
        ddFilterdata.Items.Clear();
        SqlDataReader dr7 = PortalCon.Show_tbl_RemedialClassRepetionView_Faculty(Session["Company"].ToString(),Session["GlobalDimension1Code"].ToString().Trim());


        ddFilterdata.DataSource = dr7;
        ddFilterdata.DataTextField = "Facultyname";
        ddFilterdata.DataValueField = "Faculty Code";
        ddFilterdata.DataBind();
        dr7.Close();
        PortalCon.DisConnect();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExportToexcel_Click(object sender, EventArgs e)
    {
        grdListofStudent.AllowPaging = false;
        try
        {
            PortalCon = new ServicePoratal();
            if (rdCourse.Checked == true)
            {
                dr = PortalCon.Show_tbl_RemedialClassRepetionView_Course_Grid(Session["Company"].ToString().Trim(), ddFilterdata.SelectedValue.ToString().Trim(),Session["GlobalDimension1Code"].ToString().Trim());

            }
            if (rdSubject.Checked == true)
            {
                dr = PortalCon.Show_tbl_RemedialClassRepetionView_Subject_Grid(Session["Company"].ToString().Trim(), ddFilterdata.SelectedValue.ToString().Trim(), Session["GlobalDimension1Code"].ToString().Trim());

            }
            if (rdStudentNo.Checked == true)
            {
                dr = PortalCon.Show_tbl_RemedialClassRepetionView_Student_Grid(Session["Company"].ToString().Trim(), ddFilterdata.SelectedValue.ToString().Trim(), Session["GlobalDimension1Code"].ToString().Trim());

            }
            if (rdEnrollmentno.Checked == true)
            {
                dr = PortalCon.Show_tbl_RemedialClassRepetionView_EnrollmentNo_Grid(Session["Company"].ToString(), ddFilterdata.SelectedValue.ToString().Trim(), Session["GlobalDimension1Code"].ToString().Trim());

            }
            if (rdFaculty.Checked == true)
            {
                dr = PortalCon.Show_tbl_RemedialClassRepetionView_Faculty_Grid(Session["Company"].ToString().Trim(), ddFilterdata.SelectedValue.ToString().Trim(), Session["GlobalDimension1Code"].ToString().Trim());

            }
            DataTable dt = new DataTable();
            dt.Load(dr);
            grdListofStudent.DataSource = dt;
            grdListofStudent.DataBind();
            dr.Close();
            PortalCon.DisConnect();

        }
        catch (Exception)
        {
            //dr.Close();
            //PortalCon.DisConnect();

        }
        if (rdCourse.Checked == false && rdSubject.Checked == false && rdStudentNo.Checked == false && rdEnrollmentno.Checked == false && rdFaculty.Checked == false)
        {
            ShowData();
        }

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grdListofStudent.RenderControl(htmlWrite);

        Response.Clear();
        string str = "Remedialdata";
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();
    }
    protected void grdListofStudent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblAcademicyrs = (Label)e.Row.FindControl("lblAcademicyrs");
                Label lblgldimension = (Label)e.Row.FindControl("lblgldimension");

                Label lblCourse = (Label)e.Row.FindControl("lblCourse");
                Label lblSemester = (Label)e.Row.FindControl("lblSemester");
                Label lblSection = (Label)e.Row.FindControl("lblSection");
                //Label lblleaveAttachmentFilename = (Label)e.Row.FindControl("lblleaveAttachmentFilenamView");
                Label lblSubject = (Label)e.Row.FindControl("lblSubject");
                Label lblFacultyCode = (Label)e.Row.FindControl("lblFacultyCode");
                Label lblStartDate = (Label)e.Row.FindControl("lblStartDate");
                Label lblEndDate = (Label)e.Row.FindControl("lblEndDate");
                Label lblStudentNo = (Label)e.Row.FindControl("lblStudentNo");
                Label lblEnrollmentNo = (Label)e.Row.FindControl("lblEnrollmentNo");
                Button btnDelete = (Button)e.Row.FindControl("btnDelete");
                string ccname = Session["Company"].ToString();
                string rccname = ccname.Replace(".", "_");
                string tblStudentAttendanceLine = "[" + rccname + "$Student Attendance Line - COL" + "]";
                con = new Connection();
                SqlDataReader dr = con.SHow_DeleteStudentAttendanceLine(tblStudentAttendanceLine, lblCourse.Text.Trim(), lblSemester.Text.Trim(), lblSemester.Text.Trim(), lblSubject.Text.Trim(), lblStudentNo.Text.Trim(), lblAcademicyrs.Text.Trim(), lblgldimension.Text.Trim(), lblStartDate.Text.Trim(), lblEndDate.Text.Trim());
                dr.Read();
                if (dr.HasRows)
                {
                    dr.Close();
                    con.DisConnect();
                    btnDelete.Visible = false;
                }
                else
                {
                    dr.Close();
                    con.DisConnect();
                  //  btnDelete.Visible = true;

                }

            }
        }
        catch (Exception)
        { 
        
        }
    }

    // export PDF
    public string filterchk()
    {
        string str="";
        if (rdCourse.Checked == true && ddFilterdata.Visible==true)
        {
            str = rdCourse.Text+" : "+ddFilterdata.SelectedItem.Text;
        }
        else if (rdFaculty.Checked == true && ddFilterdata.Visible == true)
        {
            str = rdFaculty.Text + " : " + ddFilterdata.SelectedItem.Text; ;
        }
        else if (rdStudentNo.Checked == true && ddFilterdata.Visible == true)
        {
            str = rdStudentNo.Text + " : " + ddFilterdata.SelectedItem.Text; ;
        }
        else if (rdSubject.Checked == true && ddFilterdata.Visible == true)
        {
            str = rdSubject.Text + " : " + ddFilterdata.SelectedItem.Text; ;
        }
        else if (rdEnrollmentno.Checked == true && ddFilterdata.Visible == true)
        {
            str = rdEnrollmentno.Text + " : " + ddFilterdata.SelectedItem.Text; ;
        }

        return  " <table width=100%><tr><td>"+str +"</td><td align=right>Created By: " + Session["uid"].ToString() + " Date:" + System.DateTime.Now.ToString("dd/MM/yyyy") + "</td></tr></table>";
    }

    protected void GenerateReport()
    {
        string ChkOption = filterchk();
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=RemedialClass.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        string str = "<h4>" + ChkOption + "</h4><br/>";
        Response.Write(str);
        StringWriter sw = new StringWriter();

        HtmlTextWriter hw = new HtmlTextWriter(sw);
        grdListofStudent.RenderControl(hw);

        StringReader sr = new StringReader(str+sw.ToString());
        Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
        
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
        grdListofStudent.AllowPaging = true;
        grdListofStudent.DataBind();  
    }

    // end PFD

   
    protected void btnpdf_Click(object sender, EventArgs e)
    {
        GenerateReport();
    }
}