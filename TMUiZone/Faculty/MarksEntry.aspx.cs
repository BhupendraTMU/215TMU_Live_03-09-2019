using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebReference;

public partial class Faculty_MarksEntry : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());

    DL.FacultyPortalDL FDL = new DL.FacultyPortalDL();
    int prentcount = 0;
    int UFMCount = 0;
    int AbCount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('access denied ?'); document.location.href='FacultyDetails.aspx';", true);
                Session["ATT"] = "";
                //bindDrpCourseList();  //added on 24 feb 2017
                bindAcademicYear(); bindDrpCourseList();
                onetime.Visible = false;
                btnview.Visible = false;

            }
        }
        catch (Exception ex)
        {
            // Response.Redirect("~/Default.aspx");
        }
    }
    public void bindAcademicYear()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            con.Open();
            da.Fill(dt1);
            con.Close();
            drpAcademicYear.DataSource = dt1;
            drpAcademicYear.DataTextField = "Details";
            drpAcademicYear.DataValueField = "No_";
            drpAcademicYear.DataBind();
            drpAcademicYear1.DataSource = dt1;
            drpAcademicYear1.DataTextField = "Details";
            drpAcademicYear1.DataValueField = "No_";
            drpAcademicYear1.DataBind();
        }
        catch
        {
        }
    }
    public void bindDrpCourseList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetCourseFromCourseWiseFacultyCollege_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@Academic", drpAcademicYear.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Open();
        da.Fill(dt);
        con.Close();
        drpCourse.DataSource = dt;
        drpCourse.DataTextField = "Details";
        drpCourse.DataValueField = "No_";
        drpCourse.DataBind();
    }
    public void bindDrpSemesterList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetSemesterFromCourseWiseFaculty_RoleTemp", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 1000000;
        cmd.Parameters.Add("@ID1", Session["uid"].ToString());
        cmd.Parameters.Add("@ID", drpCourse.SelectedValue);
        cmd.Parameters.Add("@AcYear", drpAcademicYear.SelectedValue);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        con.Open();
        da.Fill(dt);
        con.Close();
        drpSemester.DataSource = dt;
        drpSemester.DataTextField = "Details";
        drpSemester.DataValueField = "No_";
        drpSemester.DataBind();
    }
    public void bindgrid()
    {
        try
        {
            SqlDataAdapter daS = new SqlDataAdapter("select distinct [Marks Entry Faculty] from [TMU$Course Subject Line - COLLEGE] where [Academic Year]='" + drpAcademicYear.SelectedValue + "' and [Course Code]='" + drpCourse.SelectedValue + "' and (Semester='" + drpSemester.SelectedValue + "' or Year='" + drpSemester.SelectedValue + "') AND [Subject Code]='" + ddlSubject.SelectedValue + "'", con);
            DataTable dtS = new DataTable();
            daS.Fill(dtS);
            int k1 = 0;
            if (dtS.Rows[0]["Marks Entry Faculty"].ToString() != "")
            {
                for (int i = 0; i < dtS.Rows.Count; i++)
                {

                    if (dtS.Rows[i]["Marks Entry Faculty"].ToString() == Session["uid"].ToString())
                    {
                        k1 = 1;
                    }

                }
            }
            else
            {
                k1 = 1;
            }


            if (k1 == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'You are not authorised to enter marks contact Admin');", true);
                return;
                //VisibleFalseTrue(false, false, true, false); LblInstruction.Visible = false;
            }



            string SP = "";
            if (chkPiv.Checked == true && onetime.Visible == true)
            {
                SP = "sp_GetMarkEntryForFaculty_Pivot";
            }
            else
            {
                SP = "sp_GetMarkEntryForFaculty_Calendar_PNC2_TheoryNew";
            }
            SqlCommand cmd = new SqlCommand(SP, con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AcadmicYear", drpAcademicYear.SelectedValue);
            cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            cmd.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.AddWithValue("@Semester", drpSemester.SelectedValue);
            if (drpSection.SelectedIndex > 0)
            { cmd.Parameters.AddWithValue("@Section", drpSection.SelectedValue); }
            else { cmd.Parameters.AddWithValue("@Section", ""); }
            if (ddlGroup.SelectedIndex > 0)
            { cmd.Parameters.AddWithValue("@Group", ddlGroup.SelectedValue); }
            else { cmd.Parameters.AddWithValue("@Group", ""); }
            if (ddlBatch.SelectedIndex > 0)
            { cmd.Parameters.AddWithValue("@Batch", ddlBatch.SelectedValue); }
            else { cmd.Parameters.AddWithValue("@Batch", ""); }
            if (ddlSubject.SelectedIndex > 0)
            { cmd.Parameters.AddWithValue("@Subject", ddlSubject.SelectedValue); }
            else { cmd.Parameters.AddWithValue("@Subject", ""); }
            //if (SP == "sp_GetMarkEntryForFaculty_Pivot")
            //{
            if (rdInternal.Checked == true)
            {
                cmd.Parameters.AddWithValue("@ExamType1", 1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ExamType1", 2);
            }
            cmd.Parameters.AddWithValue("@StudentGroup", ddlGroup.SelectedValue);

            // }

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            if (con.State == ConnectionState.Open) { con.Close(); }
            con.Open();
            da.Fill(ds);





            if (chkPiv.Checked == true)
            {
                if (ds.Tables[3].Rows.Count == 1)
                {
                    btnResend.Visible = true;
                }
            }
            Session["dtPivot"] = ds.Tables[0];
            if (chkPiv.Checked == true)
            {
                try
                {
                    Session["dtMarkEntryForFaculty"] = ds.Tables[2];
                }
                catch (Exception ex) { }
            }
            else { Session["dtMarkEntryForFaculty"] = ds.Tables[0]; }
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                LblInstruction.Visible = true;
                lblMaxmark.Visible = true;
                foreach (DataColumn col in ds.Tables[0].Columns)
                {
                    TemplateField bfield = new TemplateField();
                    bfield.HeaderTemplate = new GridViewTemplate(ListItemType.Header, col.ColumnName);

                    if (col.ColumnName == "SrNo")
                    {
                        bfield.AccessibleHeaderText = col.ColumnName;
                        bfield.ControlStyle.BackColor = System.Drawing.Color.LightBlue;
                        bfield.ControlStyle.Width = 40;
                        bfield.HeaderStyle.Width = 40;
                        bfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Right;


                    }
                    if (col.ColumnName == "txtMarksEnableDesable")
                    {
                        bfield.AccessibleHeaderText = col.ColumnName;
                        bfield.Visible = false;

                    }
                    if (col.ColumnName == "Enrollement_No")
                    {
                        bfield.AccessibleHeaderText = col.ColumnName;
                        bfield.ControlStyle.BackColor = System.Drawing.Color.LightBlue;
                        bfield.ControlStyle.Width = 100;
                        bfield.HeaderStyle.Width = 150;
                        bfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Right;

                    }
                    if (col.ColumnName == "Student Name")
                    {
                        bfield.AccessibleHeaderText = col.ColumnName;
                        bfield.ControlStyle.BackColor = System.Drawing.Color.LightBlue;
                        bfield.ControlStyle.Width = 180;
                        bfield.HeaderStyle.Width = 150;
                    }
                    if (col.ColumnName == "TotalMarks")
                    {
                        bfield.AccessibleHeaderText = col.ColumnName;
                        bfield.ControlStyle.BackColor = System.Drawing.Color.LightBlue;

                    }
                    if (col.ColumnName == "AdmittedYear")
                    {
                        bfield.AccessibleHeaderText = col.ColumnName;
                        bfield.ControlStyle.BackColor = System.Drawing.Color.LightBlue;
                        bfield.ControlStyle.Width = 100;
                        bfield.HeaderStyle.Width = 100;

                    }
                    bfield.ItemTemplate = new GridViewTemplate(ListItemType.Item, col.ColumnName);
                    bfield.AccessibleHeaderText = col.ColumnName;
                    grdpivot.Columns.Add(bfield);


                }

                if (chkPiv.Checked == true && onetime.Visible == true)
                {

                    PivotEntry.Visible = true;
                    MarkEntry.Visible = false;
                    grdpivot.DataSource = ds.Tables[0];
                    grdpivot.DataBind();
                    int i = 0;
                    foreach (TableCell Tc in grdpivot.HeaderRow.Cells)
                    {
                        //if you are not getting value than find childcontrol of TabelCell.
                        string sssb = Tc.Text;
                        if (sssb == "&nbsp;")
                        {
                            grdpivot.Columns[i].Visible = false;

                        }
                        i++;
                    }

                    for (int j = 0; j < grdpivot.Rows.Count; j++)
                    {
                        HiddenField l1 = (HiddenField)grdpivot.Rows[j].FindControl("enabledisable");
                        string s = l1.Value;


                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        StringBuilder maxmark = new StringBuilder();
                        maxmark.Append("MAX MARKS ( ");
                        for (int k = 0; k < ds.Tables[1].Rows.Count; k++)
                        {
                            HiddenField hf = new HiddenField();

                            maxmark.Append(ds.Tables[1].Rows[k]["ExamMethod"].ToString() + ":" + ds.Tables[1].Rows[k]["maxmark"].ToString() + "&nbsp&nbsp&nbsp&nbsp&nbsp");
                            hf.ID = ds.Tables[1].Rows[k]["ExamMethod"].ToString();
                            hf.Value = ds.Tables[1].Rows[k]["maxmark"].ToString();
                            PivotEntry.Controls.Add(hf);
                        }
                        maxmark.Append(")");
                        lblMaxmark.Text = maxmark.ToString();

                        HiddenField hf1 = new HiddenField();
                        hf1.ID = ds.Tables[0].Rows[0]["txtMarksEnableDesable"].ToString();
                        hf1.Value = ds.Tables[0].Rows[0]["txtMarksEnableDesable"].ToString();
                        PivotEntry.Controls.Add(hf1);

                    }
                    VisibleFalseTrue(false, false, true, false);
                    Btnprint.Visible = false;

                }
                else
                {

                    if (ds.Tables[1].Rows.Count <= 0)
                    {
                        btnview.Visible = false;
                        btnview.Visible = false;

                        PivotEntry.Visible = false;
                        MarkEntry.Visible = true;
                        grdmarktable.DataSource = ds.Tables[0];
                        grdmarktable.DataBind();
                    }
                    else
                    {

                        if (Convert.ToInt32(ds.Tables[1].Rows[0]["ExamCountinHeader"]) == Convert.ToInt32(ds.Tables[1].Rows[0]["ExamCountinExGroup"]))
                        {

                            btnview.Visible = true;
                        }

                        else
                        { btnview.Visible = false; }

                        PivotEntry.Visible = false;
                        MarkEntry.Visible = true;
                        grdmarktable.DataSource = ds.Tables[0];
                        grdmarktable.DataBind();
                    }
                }
            }
            else
            {
                LblInstruction.Visible = false;
                lblMaxmark.Visible = false;

                btnview.Visible = false;
                if (chkPiv.Checked == false && onetime.Visible == false)
                {
                    btnPivotSave.Visible = true;
                    btnPivotSubmit.Visible = true;
                    MarkEntry.Visible = true;
                    PivotEntry.Visible = false;
                    grdmarktable.DataSource = ds.Tables[0];
                    grdmarktable.DataBind();
                }
                else
                {
                    btnPivotSave.Visible = false;
                    btnPivotSubmit.Visible = false;
                    MarkEntry.Visible = false;
                    PivotEntry.Visible = true;
                    grdpivot.DataSource = ds.Tables[0];
                    grdpivot.DataBind();
                }
            }

        }

        catch (Exception ex)
        {
            btnview.Visible = false;


        }
    }

    //added by bhupii

    public class GridViewTemplate : ITemplate
    {

        ListItemType _templateType;


        string _columnName;



        public GridViewTemplate(ListItemType type, string colname)
        {


            _templateType = type;


            _columnName = colname;

        }



        void ITemplate.InstantiateIn(System.Web.UI.Control container)
        {

            switch (_templateType)
            {

                case ListItemType.Header:



                    Label lbl = new Label();

                    lbl.Text = _columnName;
                    lbl.ID = _columnName;
                    container.Controls.Add(lbl);

                    break;



                case ListItemType.Item:


                    if (_columnName == "SrNo")
                    {
                        Label lbl1 = new Label();                            //Allocates the new text box object.<o:p>
                        lbl1.ID = _columnName;

                        lbl1.DataBinding += new EventHandler(tb2_DataBinding);   //Attaches the data binding event.<o:p>

                        // lbl1.Columns = 4;                                        //Creates a column with size 4.<o:p>

                        container.Controls.Add(lbl1);                            //Adds the newly created textbox to the container.<o:p>
                    }
                    else if (_columnName == "Enrollement_No")
                    {
                        Label lbl1 = new Label();                            //Allocates the new text box object.<o:p>
                        lbl1.ID = _columnName;
                        lbl1.DataBinding += new EventHandler(tb2_DataBinding);   //Attaches the data binding event.<o:p>

                        // lbl1.Columns = 4;                                        //Creates a column with size 4.<o:p>

                        container.Controls.Add(lbl1);                            //Adds the newly created textbox to the container.<o:p>
                    }
                    else if (_columnName == "Student_No")
                    {
                        Label lbl1 = new Label();                            //Allocates the new text box object.<o:p>
                        lbl1.ID = _columnName;
                        lbl1.DataBinding += new EventHandler(tb2_DataBinding);   //Attaches the data binding event.<o:p>

                        // lbl1.Columns = 4;                                        //Creates a column with size 4.<o:p>

                        container.Controls.Add(lbl1);                            //Adds the newly created textbox to the container.<o:p>
                    }
                    else if (_columnName == "Student Name")
                    {
                        Label lbl1 = new Label();                            //Allocates the new text box object.<o:p>
                        lbl1.ID = _columnName;
                        lbl1.DataBinding += new EventHandler(tb2_DataBinding);   //Attaches the data binding event.<o:p>

                        //lbl1.Columns = 4;                                        //Creates a column with size 4.<o:p>

                        container.Controls.Add(lbl1);                            //Adds the newly created textbox to the container.<o:p>
                    }
                    else if (_columnName == "TotalMarks")
                    {
                        Label lbl1 = new Label();                            //Allocates the new text box object.<o:p>
                        lbl1.ID = _columnName;
                        lbl1.DataBinding += new EventHandler(tb2_DataBinding);   //Attaches the data binding event.<o:p>

                        //lbl1.Columns = 4;                                        //Creates a column with size 4.<o:p>

                        container.Controls.Add(lbl1);                            //Adds the newly created textbox to the container.<o:p>
                    }
                    else if (_columnName == "AdmittedYear")
                    {
                        Label lbl1 = new Label();                            //Allocates the new text box object.<o:p>
                        lbl1.ID = _columnName;
                        lbl1.DataBinding += new EventHandler(tb2_DataBinding);   //Attaches the data binding event.<o:p>

                        //lbl1.Columns = 4;                                        //Creates a column with size 4.<o:p>

                        container.Controls.Add(lbl1);                            //Adds the newly created textbox to the container.<o:p>
                    }

                    else
                    {

                        TextBox tb1 = new TextBox();                            //Allocates the new text box object.<o:p>
                        tb1.ID = _columnName;
                        tb1.Columns = 4;
                        tb1.CssClass = "upCase";
                        tb1.DataBinding += new EventHandler(tb1_DataBinding);   //Attaches the data binding event.<o:p>

                        //Creates a column with size 4.<o:p>

                        container.Controls.Add(tb1);                            //Adds the newly created textbox to the container.<o:p>
                    }
                    break;



                case ListItemType.EditItem:



                    break;

                case ListItemType.Footer:

                    CheckBox chkColumn = new CheckBox();

                    chkColumn.ID = "Chk" + _columnName;

                    container.Controls.Add(chkColumn);

                    break;

            }

        }




        void tb1_DataBinding(object sender, EventArgs e)
        {

            TextBox txtdata = (TextBox)sender;

            GridViewRow container = (GridViewRow)txtdata.NamingContainer;

            object dataValue = DataBinder.Eval(container.DataItem, _columnName);

            if (dataValue != DBNull.Value)
            {
                txtdata.ID = _columnName;
                txtdata.Text = dataValue.ToString();

            }

        }
        void tb2_DataBinding(object sender, EventArgs e)
        {

            Label txtdata = (Label)sender;

            GridViewRow container = (GridViewRow)txtdata.NamingContainer;

            object dataValue = DataBinder.Eval(container.DataItem, _columnName);

            if (dataValue != DBNull.Value)
            {
                txtdata.ID = _columnName;
                txtdata.Text = dataValue.ToString();

            }

        }

    }

    //....................

    protected void btntblmarksshow_Click(object sender, EventArgs e)
    {

        if (drpSection.Items.Count > 1 && drpSection.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Please select section.');", true);

            return;
        }
        if (ddlGroup.Items.Count > 1 && ddlGroup.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Please select group.');", true);

            return;
        }
        hf_DocumentNo.Value = ""; hf_ExamCriteria.Value = ""; hf_ExamGroup.Value = ""; hf_ExamMethod.Value = ""; hf_MaxMarks.Value = "";
        hf_SemYear.Value = ""; hf_Status.Value = ""; hf_SubjectType.Value = ""; hf_Classification.Value = ""; hf_ExamType.Value = "";
        try
        {
            LinkButton btn = sender as LinkButton;
            GridViewRow grow = btn.NamingContainer as GridViewRow;
            HiddenField grdlblDocumentNo = (HiddenField)grow.FindControl("grdlblDocumentNo");
            Label grdlblMethod = (Label)grow.FindControl("grdlblMethod");
            HiddenField grdlblgroup = (HiddenField)grow.FindControl("grdlblgroup");
            Label grdlblSubjectType = (Label)grow.FindControl("grdlblSubjectType");

            HiddenField hfStatus = (HiddenField)grow.FindControl("hfStatus");
            HiddenField hfSubject = (HiddenField)grow.FindControl("hfSubject");
            HiddenField hfMaxMarks = (HiddenField)grow.FindControl("hfMaxMarks");
            HiddenField hfDocumentNo = (HiddenField)grow.FindControl("hfDocumentNo");
            HiddenField grdlblclassication = (HiddenField)grow.FindControl("grdlblclassication");
            HiddenField HFExamType = (HiddenField)grow.FindControl("HFExamType");
            Label lblStatusALStatus = (Label)grow.FindControl("lblStatusALStatus");
            hf_DocumentNo.Value = hfDocumentNo.Value; hf_ExamCriteria.Value = grdlblDocumentNo.Value;
            hf_ExamGroup.Value = grdlblgroup.Value; hf_ExamMethod.Value = grdlblMethod.Text; hf_MaxMarks.Value = hfMaxMarks.Value;
            hf_Status.Value = hfStatus.Value; hf_SubjCode.Value = hfSubject.Value; hf_SubjectType.Value = grdlblSubjectType.Text;
            hf_SemYear.Value = drpSemester.SelectedValue; hf_Classification.Value = grdlblclassication.Value; hf_ExamType.Value = HFExamType.Value;
            HfAwardlistStatus.Value = lblStatusALStatus.Text;
            btnSave.Text = "Save";

            if (grdlblMethod.Text == "CT1" || grdlblMethod.Text == "CT2" || grdlblMethod.Text == "CT3" || grdlblMethod.Text == "ASN")
            {

                CoMaxMark.Visible = true;
                grdViewmarksEntry.Columns[7].Visible = true;
                grdViewmarksEntry.Columns[8].Visible = true;
                grdViewmarksEntry.Columns[9].Visible = true;
                grdViewmarksEntry.Columns[10].Visible = true;
                grdViewmarksEntry.Columns[11].Visible = true;
                grdViewmarksEntry.Columns[12].Visible = true;

            }
            else
            {
                CoMaxMark.Visible = false;
                grdViewmarksEntry.Columns[7].Visible = false;
                grdViewmarksEntry.Columns[8].Visible = false;
                grdViewmarksEntry.Columns[9].Visible = false;
                grdViewmarksEntry.Columns[10].Visible = false;
                grdViewmarksEntry.Columns[11].Visible = false;
                grdViewmarksEntry.Columns[12].Visible = true;

            }




            if (grdlblMethod.Text == "ATT")
            {
                Session["ATT"] = "1";
            }
            else
            {
                Session["ATT"] = "";
            }



            SqlCommand cmd1 = new SqlCommand("sp_GetRoomStatementdetailssectionwise", con);
            cmd1.CommandType = CommandType.StoredProcedure;

            cmd1.Parameters.AddWithValue("@AcadmicYear", drpAcademicYear.SelectedValue);
            cmd1.Parameters.AddWithValue("@CourseCode", drpCourse.SelectedValue);
            cmd1.Parameters.AddWithValue("@Semester", drpSemester.SelectedValue);
            cmd1.Parameters.AddWithValue("@SubjectCode", hfSubject.Value);
            cmd1.Parameters.AddWithValue("@Method", grdlblMethod.Text);

            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();

            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            da1.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Section"].ToString() == "0" && drpSection.SelectedIndex > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Room statement is not created section wise, Please contact Admin.');", true);
                    //VisibleFalseTrue(false, false, true, false); LblInstruction.Visible = false;
                    return;
                }
            }
            con.Close();

            SqlCommand cmd = new SqlCommand("sp_GetStudentForInternalMarksEntry_PNC_Test_Internal2New", con);//sp_GetStudentForInternalMarksEntry usp_GetMarkEntryStudent//sp_GetStudentForInternalMarksEntry
            //sp_GetStudentForInternalMarksEntry_PNC
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            cmd.Parameters.AddWithValue("@AcadmicYear", drpAcademicYear.SelectedValue);
            cmd.Parameters.AddWithValue("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.AddWithValue("@Semester", drpSemester.SelectedValue);
            cmd.Parameters.AddWithValue("@SubjectCode", hfSubject.Value);
            cmd.Parameters.AddWithValue("@SubjectType", grdlblSubjectType.Text);
            cmd.Parameters.AddWithValue("@Status", hfStatus.Value);
            cmd.Parameters.AddWithValue("@Method", grdlblMethod.Text);
            cmd.Parameters.AddWithValue("@Group", grdlblgroup.Value.ToString());
            cmd.Parameters.AddWithValue("@MaxMarks", hfMaxMarks.Value);
            cmd.Parameters.AddWithValue("@DocumentNo", hfDocumentNo.Value);
            cmd.Parameters.AddWithValue("@ExamType", hf_ExamType.Value);
            cmd.Parameters.AddWithValue("@Section", drpSection.SelectedValue);

            if (ddlGroup.SelectedIndex > 0)
            { cmd.Parameters.AddWithValue("@ExamGroup", ddlGroup.SelectedValue); }
            else
            { cmd.Parameters.AddWithValue("@ExamGroup", ""); }


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            da.Fill(ds);
            con.Close();

            if (ds.Tables[4].Rows.Count > 0)
            {
                txtCo1Max.Text = ds.Tables[4].Rows[0]["CO1"].ToString();
                txtCo2Max.Text = ds.Tables[4].Rows[0]["CO2"].ToString();
                txtCo3Max.Text = ds.Tables[4].Rows[0]["CO3"].ToString();
                txtCo4Max.Text = ds.Tables[4].Rows[0]["CO4"].ToString();
                txtCo5Max.Text = ds.Tables[4].Rows[0]["CO5"].ToString();
            }



            grdViewmarksEntry.DataSource = ds.Tables[0];
            grdViewmarksEntry.DataBind();
            if (grdlblMethod.Text == "CT1" || grdlblMethod.Text == "CT2" || grdlblMethod.Text == "CT3" || grdlblMethod.Text == "ASN" || grdlblMethod.Text.Contains("ATT"))
            {
                foreach (GridViewRow row in grdViewmarksEntry.Rows)
                {
                    TextBox txtMarks = (TextBox)row.FindControl("grdtxtMarks");
                    if (txtMarks != null)
                    {
                        txtMarks.Enabled = false;
                    }
                }
            }
            else
            {
                foreach (GridViewRow row in grdViewmarksEntry.Rows)
                {
                    TextBox txtMarks = (TextBox)row.FindControl("grdtxtMarks");
                    if (txtMarks != null)
                    {
                        txtMarks.Enabled = true;
                    }
                }
            }



            if (ds.Tables[0].Rows.Count > 0)
            {
                btnview.Visible = false;
                //   Btnprint.Visible = true;
                if (hfStatus.Value == "0" || hfStatus.Value == "1" || hfStatus.Value == "3" || hfStatus.Value == "5" || hfStatus.Value == "7")
                { VisibleFalseTrue(true, true, false, true); LblInstruction.Visible = true; }
                else
                {
                    if (lblStatusALStatus.Text == "Rejected by HOD" || lblStatusALStatus.Text == "Rejected by Principal")
                    {
                        VisibleFalseTrue(true, true, false, true); LblInstruction.Visible = true;
                    }
                    else
                    {
                        VisibleFalseTrue(false, false, false, true); LblInstruction.Visible = false;
                    }


                }



                if (ds.Tables[1].Rows.Count > 0)
                {
                    lbltotalst.Text = ds.Tables[1].Rows[0]["Total Student"].ToString();
                    lblpresentst.Text = ds.Tables[1].Rows[0]["Present"].ToString();
                    lblabsentst.Text = ds.Tables[1].Rows[0]["Absent"].ToString();
                    LblUFMCount.Text = ds.Tables[1].Rows[0]["UFM"].ToString();
                }
                else
                {
                    lbltotalst.Text = "";
                    lblpresentst.Text = "";
                    lblabsentst.Text = "";
                    LblUFMCount.Text = "";
                }

                if (ds.Tables[2].Rows.Count > 0)
                {


                    LblValue.Text = drpCourse.SelectedValue;
                    LblDesc.Text = ds.Tables[2].Rows[0]["Description"].ToString();
                    LblMethod.Text = hf_ExamMethod.Value;
                    LblOddEven.Text = ds.Tables[2].Rows[0]["ODDEven"].ToString();
                    if (ddlSubject.Visible == true)
                    {
                        LblSubDesc.Text = ddlSubject.SelectedItem.Text;
                    }
                    LblNopresent.Text = lblpresentst.Text;
                    LblNoAbsent.Text = lblabsentst.Text;
                }
                else
                {
                    lbltotalst.Text = "";
                    lblpresentst.Text = "";
                    lblabsentst.Text = "";
                    LblUFMCount.Text = "";
                }
                //if (hf_ExamGroup.Value == "IOS" )
                //{
                if (drpSection.SelectedIndex == 0)
                {
                    if (ds.Tables[3].Rows[0]["Accesed Faculty"].ToString() == "1")
                    {
                        if (hfStatus.Value == "0" || hfStatus.Value == "1" || hfStatus.Value == "3" || hfStatus.Value == "5" || hfStatus.Value == "7" || lblStatusALStatus.Text == "Rejected by HOD" || lblStatusALStatus.Text == "Rejected by Principal")
                        {
                            VisibleFalseTrue(true, true, false, true); LblInstruction.Visible = true;
                        }
                        else
                        {
                            VisibleFalseTrue(false, false, false, true); LblInstruction.Visible = false;
                        }
                    }
                    else
                    {
                        if (ds.Tables[3].Rows[0]["Accesed Faculty"] == null || ds.Tables[3].Rows[0]["Accesed Faculty"].ToString() != Session["uid"].ToString())
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'You are not authorised to enter marks contact Admin');", true);
                            VisibleFalseTrue(false, false, true, false); LblInstruction.Visible = false;
                        }
                        else
                        {

                            if (hfStatus.Value == "0" || hfStatus.Value == "1" || hfStatus.Value == "3" || hfStatus.Value == "5" || hfStatus.Value == "7" || lblStatusALStatus.Text == "Rejected by HOD" || lblStatusALStatus.Text == "Rejected by Principal")
                            {
                                VisibleFalseTrue(true, true, false, true); LblInstruction.Visible = true;
                            }
                            else
                            {
                                VisibleFalseTrue(false, false, false, true); LblInstruction.Visible = false;
                            }
                        }
                    }


                }
                //}

                //else
                //{
                //    if (hf_Classification.Value != "THEORY" && hf_ExamGroup.Value != "IOS" && hf_ExamType.Value == "2" && hfStatus.Value != "6")
                //    {

                //        VisibleFalseTrue(true, true, false, true); LblInstruction.Visible = true;
                //    }
                //    else
                //    {


                //    }
                //}
            }

            else
            {
                //    Btnprint.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'No record Found');", true);
                VisibleFalseTrue(false, false, true, false); LblInstruction.Visible = false;

            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        //  Btnprint.Visible = false;
        //btnShow.Enabled = true;
        if (drpSection.Items.Count > 1 && drpSection.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Please select section.');", true);

            return;
        }
        if (ddlGroup.Items.Count > 1 && ddlGroup.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Please select group.');", true);

            return;
        }
        LblInstruction.Visible = false;
        lblError.Text = "";
        hf_AcademicYear.Value = ""; hf_Course.Value = ""; hf_SemYear.Value = "";
        hf_AcademicYear.Value = drpAcademicYear.SelectedValue; hf_Course.Value = drpCourse.SelectedValue; hf_SemYear.Value = drpSemester.SelectedValue;
        bindgrid();
        VisibleFalseTrue(false, false, true, false);
        if (chkPiv.Checked == true) { grdpivot.Visible = true; }   //veer
        else { grdmarktable.Visible = true; }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (hf_Classification.Value != "THEORY" && hf_ExamGroup.Value != "IOS" && hf_ExamType.Value == "2")
        {
            hf_Status.Value = "6"; //Submit
        }
        else
        {
            hf_Status.Value = "2";

        }
        if (ValidateBlankDataForSaveSubmit() == false) { return; }
        else
        {

            GridView gr = new GridView();
            if (hf_Classification.Value != "THEORY" && hf_ExamGroup.Value != "IOS" && hf_ExamType.Value == "2")
            {
                if (rdOpen.Checked == true)
                {
                    SaveOrSubmitDataOpen(drpAcademicYear1.SelectedValue, hf_Course.Value, "", ddlSubject1.SelectedValue, hf_SubjectType.Value, hf_ExamCriteria.Value, hf_ExamMethod.Value, hf_ExamGroup.Value, Convert.ToDecimal(hf_MaxMarks.Value), 0, gr);
                    //getmethodcountOpen();
                }
                else
                {
                    SaveOrSubmitData(hf_AcademicYear.Value, hf_Course.Value, hf_SemYear.Value, hf_SubjCode.Value, hf_SubjectType.Value, hf_ExamCriteria.Value, hf_ExamMethod.Value, hf_ExamGroup.Value, Convert.ToDecimal(hf_MaxMarks.Value), 0, gr, Convert.ToDecimal(txtCo1Max.Text), Convert.ToDecimal(txtCo2Max.Text), Convert.ToDecimal(txtCo3Max.Text), Convert.ToDecimal(txtCo4Max.Text), Convert.ToDecimal(txtCo5Max.Text));
                    //getmethodcount();
                }
            }
            else
            {
                if (rdOpen.Checked == true)
                {
                    SaveOrSubmitDataOpen(drpAcademicYear1.SelectedValue, hf_Course.Value, "", ddlSubject1.SelectedValue, hf_SubjectType.Value, hf_ExamCriteria.Value, hf_ExamMethod.Value, hf_ExamGroup.Value, Convert.ToDecimal(hf_MaxMarks.Value), 0, gr);
                    //getmethodcountOpen();
                }
                else
                {
                    SaveOrSubmitData(hf_AcademicYear.Value, hf_Course.Value, hf_SemYear.Value, hf_SubjCode.Value, hf_SubjectType.Value, hf_ExamCriteria.Value, hf_ExamMethod.Value, hf_ExamGroup.Value, Convert.ToDecimal(hf_MaxMarks.Value), 1, gr, Convert.ToDecimal(txtCo1Max.Text), Convert.ToDecimal(txtCo2Max.Text), Convert.ToDecimal(txtCo3Max.Text), Convert.ToDecimal(txtCo4Max.Text), Convert.ToDecimal(txtCo5Max.Text));
                    // getmethodcount();
                }
            }
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Data Submitted Successfully');", true);

            LblInstruction.Visible = false;
            if (rdOpen.Checked == true)
            {
                bindGrid1();
            }
            else
            {
                bindgrid();
            }
            VisibleFalseTrue(false, false, true, false);
        }
    }

    public void VisibleFalseTrue(bool bSave, bool bSubmit, bool gMarktable, bool gMarksEntry)
    {

        if (Session["ATT"].ToString() == "1")
        {
            btnSave.Visible = false;
            BtnSaveBottom.Visible = false;
        }
        else
        {
            btnSave.Visible = bSave;
            BtnSaveBottom.Visible = bSave;
        }


        BtnSubmit1.Visible = bSubmit;
        //BtnSave1.Visible = bSave;

        btnSubmit.Visible = bSubmit;

        grdmarktable.Visible = gMarktable;
        grdViewmarksEntry.Visible = gMarksEntry;
    }

    protected void grdatted_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddl.NamingContainer;

        DropDownList grdatted = (DropDownList)row.FindControl("grdatted");
        TextBox txtRemark = (TextBox)row.FindControl("grdtxtMarks");

        if (grdatted.SelectedValue != "1")
        {
            txtRemark.Text = "";
            txtRemark.Enabled = false;
        }
        else
        {
            txtRemark.Enabled = true;
        }

    }

    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpCourseList();
        VisibleFalseTrue(false, false, false, false);
        bindSubject();
        PivotEntry.Visible = false;
        btnview.Visible = false;
        LblInstruction.Visible = false;
        lblMaxmark.Visible = false;

    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleFalseTrue(false, false, false, false);
        bindDrpSemesterList();
        //bindSubject();
        PivotEntry.Visible = false;
        btnview.Visible = false;
        LblInstruction.Visible = false;
        lblMaxmark.Visible = false;

    }
    protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleFalseTrue(false, false, false, false);
        bindSubject();

        PivotEntry.Visible = false;
        btnview.Visible = false;
        LblInstruction.Visible = false;
        lblMaxmark.Visible = false;


    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {





            hf_Status.Value = "1"; //Save
            ValidateBlankDataForSaveSubmit();
            if (lblError.Text == "")
            {
                GridView gr = new GridView();
                SaveOrSubmitData(hf_AcademicYear.Value, hf_Course.Value, hf_SemYear.Value, hf_SubjCode.Value, hf_SubjectType.Value, hf_ExamCriteria.Value, hf_ExamMethod.Value, hf_ExamGroup.Value, Convert.ToDecimal(hf_MaxMarks.Value), 0, gr, Convert.ToDecimal(txtCo1Max.Text), Convert.ToDecimal(txtCo2Max.Text), Convert.ToDecimal(txtCo3Max.Text), Convert.ToDecimal(txtCo4Max.Text), Convert.ToDecimal(txtCo5Max.Text));
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Data saved Successfully');", true);
                bindgrid();
                LblInstruction.Visible = false;
                VisibleFalseTrue(false, false, true, false);

            }
        }
        catch (Exception ex)
        {
        }
    }
    public bool ValidateBlankDataForSaveSubmit()
    {
        lblError.Text = "";
        prentcount = 0;
        UFMCount = 0;
        AbCount = 0;
        foreach (GridViewRow row in grdViewmarksEntry.Rows)
        {
            if (lblError.Text == "")
            {
                TextBox grdtxtMarks = (TextBox)row.FindControl("grdtxtMarks");
                TextBox CO1 = (TextBox)row.FindControl("grdtxtCO1");
                TextBox CO2 = (TextBox)row.FindControl("grdtxtCO2");
                TextBox CO3 = (TextBox)row.FindControl("grdtxtCO3");
                TextBox CO4 = (TextBox)row.FindControl("grdtxtCO4");
                TextBox CO5 = (TextBox)row.FindControl("grdtxtCO5");


                string _markentery = grdtxtMarks.Text;
                string _maxmarks = (row.FindControl("grdlMaximummark") as Label).Text;
                grdtxtMarks.BorderStyle = BorderStyle.None;
                if (grdtxtMarks.Enabled == true)
                {
                    //if (_markentery == "" && hf_Status.Value == "1")  // check Submit _marksentry could not be blank
                    //{
                    //    lblError.Text = "Marks could not be blank";
                    //    grdtxtMarks.Focus();
                    //    grdtxtMarks.BorderStyle = BorderStyle.Solid;
                    //    grdtxtMarks.BorderColor = System.Drawing.Color.Red;

                    //}


                    try
                    {
                        if (grdtxtMarks.Visible == true)
                        {
                            if (_markentery == "AB" || _markentery == "ab" || _markentery == "DT" || _markentery == "dt" || _markentery == "UFM" || _markentery == "ufm" || (_markentery == "" && (hf_Status.Value == "0" || hf_Status.Value == "1")))
                            {
                                lblError.Text = "";
                            }


                            else if (Convert.ToDecimal(_maxmarks) < Convert.ToDecimal(_markentery))
                            {

                                lblError.Text = "You can not enter Greater than maxmuim marks.";
                                grdtxtMarks.Focus();
                                grdtxtMarks.BorderStyle = BorderStyle.Solid;
                                grdtxtMarks.BorderColor = System.Drawing.Color.Red;
                            }
                        }
                        else
                        {
                            if (CO1.Text == "" || CO2.Text == "" || CO3.Text == "" || CO4.Text == "" || CO5.Text == "")
                            {
                                lblError.Text = "Marks could not be ENTER blank.";

                            }
                        }

                    }
                    catch
                    {
                        lblError.Text = "Marks could not be ENTER blank  ONLY Less than Maximuim Marks, AB, DT , UFM and Number only ";
                        grdtxtMarks.Focus();
                        grdtxtMarks.BorderStyle = BorderStyle.Solid;
                        grdtxtMarks.BorderColor = System.Drawing.Color.Red;

                    }

                }
                string Str = grdtxtMarks.Text.Trim();

                double Num;

                bool isNum = double.TryParse(Str, out Num);
                if (Str == "AB" || Str == "ab")
                {
                    AbCount = AbCount + 1;
                }
                else
                {
                    if (isNum || (Str == "UFM" || Str == "ufm"))
                    {
                        prentcount = prentcount + 1;
                        if (Str == "UFM" || Str == "ufm")
                        {
                            UFMCount = UFMCount + 1;
                        }
                    }
                }


            }

        }
        if (lblError.Text == "")
        //prentcount == Convert.ToInt32(lblpresentst.Text) ||
        {
            if (hf_ExamGroup.Value == "IOS")
            {
                if (Session["GlobalDimension1Code"].ToString() == "TMSN" || Session["GlobalDimension1Code"].ToString() == "TMNS")
                {
                    if (hf_Classification.Value == "THEORY" || hf_Classification.Value == "THEORYRACT")
                    {
                        if ((AbCount == Convert.ToInt32(lblabsentst.Text) && UFMCount == Convert.ToInt32(LblUFMCount.Text)))
                        {
                            return true;
                        }
                        else
                        {
                            divmsg.InnerHtml = "Total No. of Absent student (" + lblabsentst.Text + ") is mismatching with Entered No.of Absent student  (" + AbCount + ")  OR Total No. of UFM student (" + LblUFMCount.Text + ") is mismatching with Entered No. of UFM student (" + UFMCount + ") ";
                            ModalPopupMsg.Show();
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    if ((AbCount == Convert.ToInt32(lblabsentst.Text) && UFMCount == Convert.ToInt32(LblUFMCount.Text)))
                    {
                        return true;
                    }
                    else
                    {
                        divmsg.InnerHtml = "Total No. of Absent student (" + lblabsentst.Text + ") is mismatching with Entered No.of Absent student  (" + AbCount + ")  OR Total No. of UFM student (" + LblUFMCount.Text + ") is mismatching with Entered No. of UFM student (" + UFMCount + ") ";
                        ModalPopupMsg.Show();
                        return false;
                    }
                }
            }

            else
            {
                return true;
            }

        }
        else
        {

            return false;
        }
    }
    public bool ValidateBlankDataForSaveSubmit(GridView grdvalidate)
    {
        lblError.Text = "";
        prentcount = 0;
        UFMCount = 0;
        foreach (GridViewRow row in grdvalidate.Rows)
        {
            if (lblError.Text == "")
            {
                TextBox grdtxtMarks = (TextBox)row.FindControl("grdtxtMarks");
                TextBox txtmark = (row.FindControl("grdtxtMarks") as TextBox);
                string _markentery = (row.FindControl("grdtxtMarks") as TextBox).Text;
                _markentery = _markentery.Substring(_markentery.LastIndexOf(',') + 1);
                string _maxmarks = (row.FindControl("grdlMaximummark") as Label).Text;
                grdtxtMarks.BorderStyle = BorderStyle.None;
                if (grdtxtMarks.Enabled == true)
                {



                    try
                    {

                        if (_markentery == "AB" || _markentery == "ab" || _markentery == "DT" || _markentery == "dt" || _markentery == "UFM" || _markentery == "ufm" || (_markentery == "" && (hf_Status.Value == "0" || hf_Status.Value == "1")))
                        {
                            lblError.Text = "";
                        }

                        else if (hf_Status.Value == "2" && _markentery == "")
                        {
                            lblError.Text = "Marks could not be ENTER blank  ONLY Less than Maximuim Marks, AB, DT , UFM and Number only ";
                            bindgrid();
                            grdtxtMarks.Focus();
                            grdtxtMarks.BorderStyle = BorderStyle.Solid;
                            grdtxtMarks.BorderColor = System.Drawing.Color.Red;
                            return false;
                        }
                        else if (Convert.ToDecimal(_maxmarks) < Convert.ToDecimal(_markentery))
                        {

                            lblError.Text = "You can not enter Greater than maxmuim marks.";

                            grdtxtMarks.Focus();
                            grdtxtMarks.BorderStyle = BorderStyle.Solid;
                            grdtxtMarks.BorderColor = System.Drawing.Color.Red;

                        }

                    }
                    catch
                    {
                        lblError.Text = "Marks could not be ENTER blank  ONLY Less than Maximuim Marks, AB, DT , UFM and Number only ";

                        grdtxtMarks.Focus();
                        grdtxtMarks.BorderStyle = BorderStyle.Solid;
                        grdtxtMarks.BorderColor = System.Drawing.Color.Red;

                    }

                }
                string Str = txtmark.Text.Trim();

                double Num;

                bool isNum = double.TryParse(Str, out Num);

                if (isNum || Str == "UFM" || Str == "ufm")
                {
                    prentcount = prentcount + 1;
                    if (Str == "UFM" || Str == "ufm")
                    {
                        UFMCount = UFMCount + 1;
                    }

                }


            }

        }
        if (lblError.Text == "")
        {
            if (prentcount == Convert.ToInt32(lblpresentst.Text) || hf_Classification.Value != "THEORY" || hf_ExamGroup.Value != "IOS")
            {
                return true;
            }
            else
            {
                divmsg.InnerHtml = "Total No. of present student (" + lblpresentst.Text + ") is mismatching with Entered No. (" + prentcount + ") of present mark student and No. of UFM Case is(" + UFMCount + ") .";
                ModalPopupMsg.Show();
                return false;
            }

        }
        else
        {

            return false;
        }

    }
    protected void btncancelpopup_Click(object sender, EventArgs e)
    {
        divmsg.InnerHtml = "";
        hf_Status.Value = "2";
        ModalPopupMsg.Hide();
        LblInstruction.Visible = false;
    }
    public void SaveOrSubmitDataOpen(string AcademicYear, string CourseCode, string SemYear, string SubjectCode, string SubjectType, string ExamCriteria, string Method, string Group, decimal MaxMarks, int Reportstatus, GridView grdSelect)
    {
        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_InsertInternalMarksEntryAndAttendanceHeaderOpen", con);//Insert_proc_MarksEntryHeader1
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());//Session["GlobalDimension1Code"]
                cmd.Parameters.AddWithValue("@AcadmicYear", AcademicYear);
                cmd.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@CourseCode", CourseCode);
                cmd.Parameters.AddWithValue("@SemesterYear", SemYear);
                cmd.Parameters.AddWithValue("@SubjectCode", SubjectCode);
                cmd.Parameters.AddWithValue("@SubjectType", SubjectType);
                cmd.Parameters.AddWithValue("@ExamCriteria", ExamCriteria);
                cmd.Parameters.AddWithValue("@Status", hf_Status.Value);
                cmd.Parameters.AddWithValue("@Method", Method);
                cmd.Parameters.AddWithValue("@Group", Group);
                cmd.Parameters.AddWithValue("@MaxMarks", MaxMarks);
                if (grdSelect.Rows.Count > 0)
                {
                    cmd.Parameters.AddWithValue("@ExamType", hfExamType1.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ExamType", hf_ExamType.Value);
                }
                cmd.Parameters.AddWithValue("@Section", drpSection.SelectedValue);
                cmd.Parameters.AddWithValue("@AwardListStatus", Reportstatus);

                cmd.Parameters.AddWithValue("@Co1", txtCo1Max.Text);
                cmd.Parameters.AddWithValue("@Co2", txtCo2Max.Text);
                cmd.Parameters.AddWithValue("@Co3", txtCo3Max.Text);
                cmd.Parameters.AddWithValue("@Co4", txtCo4Max.Text);
                cmd.Parameters.AddWithValue("@Co5", txtCo5Max.Text);

                if (con.State == ConnectionState.Open)
                { con.Close(); }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (grdSelect.Rows.Count > 0)
                {
                    foreach (GridViewRow row in grdSelect.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            TextBox grdtxtMarks = (TextBox)row.FindControl("grdtxtMarks");
                            TextBox grdtxtMarksCO1 = (TextBox)row.FindControl("grdtxtCO1");
                            TextBox grdtxtMarksCO2 = (TextBox)row.FindControl("grdtxtCO2");
                            TextBox grdtxtMarksCO3 = (TextBox)row.FindControl("grdtxtCO3");
                            TextBox grdtxtMarksCO4 = (TextBox)row.FindControl("grdtxtCO4");
                            TextBox grdtxtMarksCO5 = (TextBox)row.FindControl("grdtxtCO5");
                            grdtxtMarks.Text = grdtxtMarks.Text.ToString().Substring(grdtxtMarks.Text.ToString().LastIndexOf(',') + 1);
                            // DropDownList ddattend = (DropDownList)row.FindControl("grdatted");
                            HiddenField hfStatusL = (HiddenField)row.FindControl("hfStatusL");
                            hfStatusL.Value = hfStatusL.Value.ToString().Substring(hfStatusL.Value.ToString().LastIndexOf(',') + 1);
                            var id = grdSelect.DataKeys[row.RowIndex].Value;
                            int Status = Convert.ToInt16(hf_Status.Value);
                            if (grdtxtMarks.Text == "" && grdtxtMarks.Enabled == true)
                            {
                                Status = 0;
                            }
                            if (hfStatusL.Value == "0" || hfStatusL.Value == "1" || hfStatusL.Value == "3" || hfStatusL.Value == "5" || HfAwardlistStatus.Value == "Rejected by HOD" || HfAwardlistStatus.Value == "Unlocked by Principal" || HfAwardlistStatus.Value == "")   // 0,1,3,5 for Pending else not editable 
                            {
                                SqlCommand cmd1 = new SqlCommand("sp_InsertInternalMarksEntryAndAttendanceLineOpen", con);//Insert_proc_MarksEntryLine1
                                cmd1.CommandType = CommandType.StoredProcedure;
                                cmd1.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                                cmd1.Parameters.AddWithValue("@AcadmicYear", drpAcademicYear1.SelectedValue);

                                //&& grdtxtMarks.Text != "DT" && grdtxtMarks.Text != "UFM"
                                if (grdtxtMarks.Text != "AB" && grdtxtMarks.Text != "DT" && grdtxtMarks.Text != "UFM" && grdtxtMarks.Text != "ab" && grdtxtMarks.Text != "dt" && grdtxtMarks.Text != "ufm")
                                {
                                    if (grdtxtMarks.Text != "")
                                    {

                                        cmd1.Parameters.AddWithValue("@InernalMark", Convert.ToDecimal(grdtxtMarks.Text));
                                        cmd1.Parameters.AddWithValue("@AttendanceType", "1");
                                    }
                                    else
                                    {
                                        cmd1.Parameters.AddWithValue("@InernalMark", Convert.ToDecimal("-1"));
                                        cmd1.Parameters.AddWithValue("@AttendanceType", "1");
                                    }
                                }
                                else
                                {
                                    if (grdtxtMarks.Text == "AB" || grdtxtMarks.Text == "ab")
                                    {
                                        cmd1.Parameters.AddWithValue("@AttendanceType", "2");
                                        cmd1.Parameters.AddWithValue("@InernalMark", Convert.ToDecimal("-1"));
                                    }
                                    if (grdtxtMarks.Text == "DT" || grdtxtMarks.Text == "dt")
                                    {
                                        cmd1.Parameters.AddWithValue("@AttendanceType", "3");
                                        cmd1.Parameters.AddWithValue("@InernalMark", Convert.ToDecimal("-1"));
                                    }
                                    if (grdtxtMarks.Text == "UFM" || grdtxtMarks.Text == "ufm")
                                    {
                                        cmd1.Parameters.AddWithValue("@AttendanceType", "4");
                                        cmd1.Parameters.AddWithValue("@InernalMark", Convert.ToDecimal("-1"));
                                    }


                                }
                                if (grdtxtMarksCO1.Text.ToUpper() != "AB" && grdtxtMarksCO1.Text.ToUpper() != "DT" && grdtxtMarksCO1.Text.ToUpper() != "UFM")
                                {
                                    if (grdtxtMarksCO1.Text != "")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co1", Convert.ToDecimal(grdtxtMarksCO1.Text));

                                    }
                                    else
                                    {
                                        cmd1.Parameters.AddWithValue("@Co1", Convert.ToDecimal("-1"));

                                    }
                                }
                                else
                                {
                                    if (grdtxtMarksCO1.Text.ToUpper() == "AB")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co1", Convert.ToDecimal("-1"));
                                    }
                                    if (grdtxtMarksCO1.Text.ToUpper() == "DT")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co1", Convert.ToDecimal("-1"));
                                    }
                                    if (grdtxtMarksCO1.Text.ToUpper() == "UFM")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co1", Convert.ToDecimal("-1"));
                                    }
                                }
                                if (grdtxtMarksCO2.Text.ToUpper() != "AB" && grdtxtMarksCO2.Text.ToUpper() != "DT" && grdtxtMarksCO2.Text.ToUpper() != "UFM")
                                {
                                    if (grdtxtMarksCO2.Text != "")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co2", Convert.ToDecimal(grdtxtMarksCO2.Text));

                                    }
                                    else
                                    {
                                        cmd1.Parameters.AddWithValue("@Co2", Convert.ToDecimal("-1"));

                                    }
                                }
                                else
                                {
                                    if (grdtxtMarksCO2.Text.ToUpper() == "AB")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co2", Convert.ToDecimal("-1"));
                                    }
                                    if (grdtxtMarksCO2.Text.ToUpper() == "DT")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co2", Convert.ToDecimal("-1"));
                                    }
                                    if (grdtxtMarksCO2.Text.ToUpper() == "UFM")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co2", Convert.ToDecimal("-1"));
                                    }
                                }

                                if (grdtxtMarksCO3.Text.ToUpper() != "AB" && grdtxtMarksCO3.Text.ToUpper() != "DT" && grdtxtMarksCO3.Text.ToUpper() != "UFM")
                                {
                                    if (grdtxtMarksCO3.Text != "")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co3", Convert.ToDecimal(grdtxtMarksCO3.Text));

                                    }
                                    else
                                    {
                                        cmd1.Parameters.AddWithValue("@Co3", Convert.ToDecimal("-1"));

                                    }
                                }
                                else
                                {
                                    if (grdtxtMarksCO3.Text.ToUpper() == "AB")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co3", Convert.ToDecimal("-1"));
                                    }
                                    if (grdtxtMarksCO3.Text.ToUpper() == "DT")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co3", Convert.ToDecimal("-1"));
                                    }
                                    if (grdtxtMarksCO3.Text.ToUpper() == "UFM")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co3", Convert.ToDecimal("-1"));
                                    }
                                }
                                if (grdtxtMarksCO4.Text.ToUpper() != "AB" && grdtxtMarksCO4.Text.ToUpper() != "DT" && grdtxtMarksCO4.Text.ToUpper() != "UFM")
                                {
                                    if (grdtxtMarksCO4.Text != "")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co4", Convert.ToDecimal(grdtxtMarksCO4.Text));

                                    }
                                    else
                                    {
                                        cmd1.Parameters.AddWithValue("@Co4", Convert.ToDecimal("-1"));

                                    }
                                }
                                else
                                {
                                    if (grdtxtMarksCO4.Text.ToUpper() == "AB")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co4", Convert.ToDecimal("-1"));
                                    }
                                    if (grdtxtMarksCO4.Text.ToUpper() == "DT")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co4", Convert.ToDecimal("-1"));
                                    }
                                    if (grdtxtMarksCO4.Text.ToUpper() == "UFM")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co4", Convert.ToDecimal("-1"));
                                    }
                                }
                                if (grdtxtMarksCO5.Text.ToUpper() != "AB" && grdtxtMarksCO5.Text.ToUpper() != "DT" && grdtxtMarksCO5.Text.ToUpper() != "UFM")
                                {
                                    if (grdtxtMarksCO5.Text != "")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co5", Convert.ToDecimal(grdtxtMarksCO5.Text));

                                    }
                                    else
                                    {
                                        cmd1.Parameters.AddWithValue("@Co5", Convert.ToDecimal("-1"));

                                    }
                                }
                                else
                                {
                                    if (grdtxtMarksCO5.Text.ToUpper() == "AB")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co5", Convert.ToDecimal("-1"));
                                    }
                                    if (grdtxtMarksCO5.Text.ToUpper() == "DT")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co5", Convert.ToDecimal("-1"));
                                    }
                                    if (grdtxtMarksCO5.Text.ToUpper() == "UFM")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co5", Convert.ToDecimal("-1"));
                                    }
                                }

                                cmd1.Parameters.AddWithValue("@StudentNo", id);
                                cmd1.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
                                cmd1.Parameters.AddWithValue("@Status", Status);

                                cmd1.Parameters.AddWithValue("@CourseCode", "");
                                cmd1.Parameters.AddWithValue("@SemesterYear", "");
                                cmd1.Parameters.AddWithValue("@SubjectCode", SubjectCode);
                                cmd1.Parameters.AddWithValue("@SubjectType", SubjectType);
                                cmd1.Parameters.AddWithValue("@ExamCriteria", ExamCriteria);
                                cmd1.Parameters.AddWithValue("@Method", Method);
                                cmd1.Parameters.AddWithValue("@Group", Group);
                                cmd1.Parameters.AddWithValue("@ExamType", hfExamType1.Value);
                                cmd1.Parameters.AddWithValue("@Section", drpSection.SelectedValue);
                                cmd1.Parameters.AddWithValue("@AwardListStatus", Reportstatus);



                                if (con.State != ConnectionState.Open)
                                {
                                    con.Close();
                                }
                                con.Open();
                                cmd1.ExecuteNonQuery();

                                con.Close();
                            }
                        }
                    }
                }
                else
                {
                    //  for mark  entry header line           
                    foreach (GridViewRow row in grdViewmarksEntry.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            TextBox grdtxtMarks = (TextBox)row.FindControl("grdtxtMarks");
                            TextBox grdtxtMarksCO1 = (TextBox)row.FindControl("grdtxtCO1");
                            TextBox grdtxtMarksCO2 = (TextBox)row.FindControl("grdtxtCO2");
                            TextBox grdtxtMarksCO3 = (TextBox)row.FindControl("grdtxtCO3");
                            TextBox grdtxtMarksCO4 = (TextBox)row.FindControl("grdtxtCO4");
                            TextBox grdtxtMarksCO5 = (TextBox)row.FindControl("grdtxtCO5");
                            // DropDownList ddattend = (DropDownList)row.FindControl("grdatted");
                            HiddenField hfStatusL = (HiddenField)row.FindControl("hfStatusL");
                            var id = grdViewmarksEntry.DataKeys[row.RowIndex].Value;
                            int Status = Convert.ToInt16(hf_Status.Value);
                            if (grdtxtMarks.Text == "" && grdtxtMarks.Enabled == true)
                            {
                                Status = 0;
                            }
                            if (hfStatusL.Value == "0" || hfStatusL.Value == "1" || hfStatusL.Value == "3" || hfStatusL.Value == "5" || HfAwardlistStatus.Value == "Rejected by HOD" || HfAwardlistStatus.Value == "Unlocked by Principal" || HfAwardlistStatus.Value == "")   // 0,1,3,5 for Pending else not editable 
                            {
                                SqlCommand cmd1 = new SqlCommand("sp_InsertInternalMarksEntryAndAttendanceLineOpen", con);//Insert_proc_MarksEntryLine1
                                cmd1.CommandType = CommandType.StoredProcedure;
                                cmd1.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                                cmd1.Parameters.AddWithValue("@AcadmicYear", drpAcademicYear.SelectedValue);

                                //&& grdtxtMarks.Text != "DT" && grdtxtMarks.Text != "UFM"
                                if (grdtxtMarks.Text != "AB" && grdtxtMarks.Text != "DT" && grdtxtMarks.Text != "UFM" && grdtxtMarks.Text != "ab" && grdtxtMarks.Text != "dt" && grdtxtMarks.Text != "ufm")
                                {
                                    if (grdtxtMarks.Text != "")
                                    {

                                        cmd1.Parameters.AddWithValue("@InernalMark", Convert.ToDecimal(grdtxtMarks.Text));
                                        cmd1.Parameters.AddWithValue("@AttendanceType", "1");
                                    }
                                    else
                                    {
                                        cmd1.Parameters.AddWithValue("@InernalMark", Convert.ToDecimal("-1"));
                                        cmd1.Parameters.AddWithValue("@AttendanceType", "1");
                                    }
                                }
                                else
                                {
                                    if (grdtxtMarks.Text == "AB" || grdtxtMarks.Text == "ab")
                                    {
                                        cmd1.Parameters.AddWithValue("@AttendanceType", "2");
                                        cmd1.Parameters.AddWithValue("@InernalMark", Convert.ToDecimal("-1"));
                                    }
                                    if (grdtxtMarks.Text == "DT" || grdtxtMarks.Text == "dt")
                                    {
                                        cmd1.Parameters.AddWithValue("@AttendanceType", "3");
                                        cmd1.Parameters.AddWithValue("@InernalMark", Convert.ToDecimal("-1"));
                                    }
                                    if (grdtxtMarks.Text == "UFM" || grdtxtMarks.Text == "ufm")
                                    {
                                        cmd1.Parameters.AddWithValue("@AttendanceType", "4");
                                        cmd1.Parameters.AddWithValue("@InernalMark", Convert.ToDecimal("-1"));
                                    }


                                }
                                if (grdtxtMarksCO1.Text.ToUpper() != "AB" && grdtxtMarksCO1.Text.ToUpper() != "DT" && grdtxtMarksCO1.Text.ToUpper() != "UFM")
                                {
                                    if (grdtxtMarksCO1.Text != "")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co1", Convert.ToDecimal(grdtxtMarksCO1.Text));

                                    }
                                    else
                                    {
                                        cmd1.Parameters.AddWithValue("@Co1", Convert.ToDecimal("-1"));

                                    }
                                }
                                else
                                {
                                    if (grdtxtMarksCO1.Text.ToUpper() == "AB")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co1", Convert.ToDecimal("-1"));
                                    }
                                    if (grdtxtMarksCO1.Text.ToUpper() == "DT")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co1", Convert.ToDecimal("-1"));
                                    }
                                    if (grdtxtMarksCO1.Text.ToUpper() == "UFM")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co1", Convert.ToDecimal("-1"));
                                    }
                                }
                                if (grdtxtMarksCO2.Text.ToUpper() != "AB" && grdtxtMarksCO2.Text.ToUpper() != "DT" && grdtxtMarksCO2.Text.ToUpper() != "UFM")
                                {
                                    if (grdtxtMarksCO2.Text != "")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co2", Convert.ToDecimal(grdtxtMarksCO2.Text));

                                    }
                                    else
                                    {
                                        cmd1.Parameters.AddWithValue("@Co2", Convert.ToDecimal("-1"));

                                    }
                                }
                                else
                                {
                                    if (grdtxtMarksCO2.Text.ToUpper() == "AB")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co2", Convert.ToDecimal("-1"));
                                    }
                                    if (grdtxtMarksCO2.Text.ToUpper() == "DT")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co2", Convert.ToDecimal("-1"));
                                    }
                                    if (grdtxtMarksCO2.Text.ToUpper() == "UFM")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co2", Convert.ToDecimal("-1"));
                                    }
                                }

                                if (grdtxtMarksCO3.Text.ToUpper() != "AB" && grdtxtMarksCO3.Text.ToUpper() != "DT" && grdtxtMarksCO3.Text.ToUpper() != "UFM")
                                {
                                    if (grdtxtMarksCO3.Text != "")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co3", Convert.ToDecimal(grdtxtMarksCO3.Text));

                                    }
                                    else
                                    {
                                        cmd1.Parameters.AddWithValue("@Co3", Convert.ToDecimal("-1"));

                                    }
                                }
                                else
                                {
                                    if (grdtxtMarksCO3.Text.ToUpper() == "AB")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co3", Convert.ToDecimal("-1"));
                                    }
                                    if (grdtxtMarksCO3.Text.ToUpper() == "DT")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co3", Convert.ToDecimal("-1"));
                                    }
                                    if (grdtxtMarksCO3.Text.ToUpper() == "UFM")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co3", Convert.ToDecimal("-1"));
                                    }
                                }
                                if (grdtxtMarksCO4.Text.ToUpper() != "AB" && grdtxtMarksCO4.Text.ToUpper() != "DT" && grdtxtMarksCO4.Text.ToUpper() != "UFM")
                                {
                                    if (grdtxtMarksCO4.Text != "")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co4", Convert.ToDecimal(grdtxtMarksCO4.Text));

                                    }
                                    else
                                    {
                                        cmd1.Parameters.AddWithValue("@Co4", Convert.ToDecimal("-1"));

                                    }
                                }
                                else
                                {
                                    if (grdtxtMarksCO4.Text.ToUpper() == "AB")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co4", Convert.ToDecimal("-1"));
                                    }
                                    if (grdtxtMarksCO4.Text.ToUpper() == "DT")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co4", Convert.ToDecimal("-1"));
                                    }
                                    if (grdtxtMarksCO4.Text.ToUpper() == "UFM")
                                    {
                                        cmd1.Parameters.AddWithValue("@AttendanceType", "4");
                                        cmd1.Parameters.AddWithValue("@Co4", Convert.ToDecimal("-1"));
                                    }
                                }
                                if (grdtxtMarksCO5.Text.ToUpper() != "AB" && grdtxtMarksCO5.Text.ToUpper() != "DT" && grdtxtMarksCO5.Text.ToUpper() != "UFM")
                                {
                                    if (grdtxtMarksCO5.Text != "")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co5", Convert.ToDecimal(grdtxtMarksCO5.Text));

                                    }
                                    else
                                    {
                                        cmd1.Parameters.AddWithValue("@Co5", Convert.ToDecimal("-1"));

                                    }
                                }
                                else
                                {
                                    if (grdtxtMarksCO5.Text.ToUpper() == "AB")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co5", Convert.ToDecimal("-1"));
                                    }
                                    if (grdtxtMarksCO5.Text.ToUpper() == "DT")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co5", Convert.ToDecimal("-1"));
                                    }
                                    if (grdtxtMarksCO5.Text.ToUpper() == "UFM")
                                    {

                                        cmd1.Parameters.AddWithValue("@Co5", Convert.ToDecimal("-1"));
                                    }
                                }

                                cmd1.Parameters.AddWithValue("@StudentNo", id);
                                cmd1.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
                                cmd1.Parameters.AddWithValue("@Status", Status);

                                cmd1.Parameters.AddWithValue("@CourseCode", drpCourse.SelectedValue);
                                cmd1.Parameters.AddWithValue("@SemesterYear", drpSemester.SelectedValue);
                                cmd1.Parameters.AddWithValue("@SubjectCode", SubjectCode);
                                cmd1.Parameters.AddWithValue("@SubjectType", SubjectType);
                                cmd1.Parameters.AddWithValue("@ExamCriteria", ExamCriteria);
                                cmd1.Parameters.AddWithValue("@Method", Method);
                                cmd1.Parameters.AddWithValue("@Group", Group);
                                cmd1.Parameters.AddWithValue("@ExamType", hf_ExamType.Value);
                                cmd1.Parameters.AddWithValue("@Section", drpSection.SelectedValue);
                                cmd1.Parameters.AddWithValue("@AwardListStatus", Reportstatus);
                                if (con.State != ConnectionState.Open)
                                {
                                    con.Close();
                                }
                                con.Open();
                                cmd1.ExecuteNonQuery();
                                con.Close();

                            }
                        }
                    }
                }
                scope.Complete();

            }
            catch (Exception ex)
            {
                scope.Dispose();
            }
        }
    }





    public void SaveOrSubmitData(string AcademicYear, string CourseCode, string SemYear, string SubjectCode, string SubjectType, string ExamCriteria, string Method, string Group, decimal MaxMarks, int Reportstatus, GridView grdSelect, decimal CO1Max, decimal CO2Max, decimal CO3Max, decimal CO4Max, decimal CO5Max)
    {
        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        {

            try
            {



                DataTable MarksLine = new DataTable();
                MarksLine.Clear();

                MarksLine.Columns.Add("CollegeCode");
                MarksLine.Columns.Add("AcadmicYear");
                MarksLine.Columns.Add("InernalMark");
                MarksLine.Columns.Add("AttendanceType");
                MarksLine.Columns.Add("StudentNo");
                MarksLine.Columns.Add("facultyCode");
                MarksLine.Columns.Add("Status");
                MarksLine.Columns.Add("CourseCode");
                MarksLine.Columns.Add("SemesterYear");
                MarksLine.Columns.Add("SubjectCode");
                MarksLine.Columns.Add("SubjectType");
                MarksLine.Columns.Add("ExamCriteria");
                MarksLine.Columns.Add("Method");
                MarksLine.Columns.Add("Group");
                MarksLine.Columns.Add("ExamType");
                MarksLine.Columns.Add("Section");
                MarksLine.Columns.Add("StudentGroup");
                MarksLine.Columns.Add("AwardListStatus");
                MarksLine.Columns.Add("CO1");
                MarksLine.Columns.Add("CO2");
                MarksLine.Columns.Add("CO3");
                MarksLine.Columns.Add("CO4");
                MarksLine.Columns.Add("CO5");


                if (grdSelect.Rows.Count > 0)
                {
                    foreach (GridViewRow row in grdSelect.Rows)
                    {
                        DataRow _DataLine = MarksLine.NewRow();
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            TextBox grdtxtMarks = (TextBox)row.FindControl("grdtxtMarks");
                            TextBox grdtxtCO1 = (TextBox)row.FindControl("grdtxtCO1");
                            TextBox grdtxtCO2 = (TextBox)row.FindControl("grdtxtCO2");
                            TextBox grdtxtCO3 = (TextBox)row.FindControl("grdtxtCO3");
                            TextBox grdtxtCO4 = (TextBox)row.FindControl("grdtxtCO4");
                            TextBox grdtxtCO5 = (TextBox)row.FindControl("grdtxtCO5");
                            grdtxtMarks.Text = grdtxtMarks.Text.ToString().Substring(grdtxtMarks.Text.ToString().LastIndexOf(',') + 1);
                            // DropDownList ddattend = (DropDownList)row.FindControl("grdatted");
                            HiddenField hfStatusL = (HiddenField)row.FindControl("hfStatusL");
                            hfStatusL.Value = hfStatusL.Value.ToString().Substring(hfStatusL.Value.ToString().LastIndexOf(',') + 1);
                            var id = grdSelect.DataKeys[row.RowIndex].Value;
                            int Status = Convert.ToInt16(hf_Status.Value);
                            if (grdtxtMarks.Text == "" && grdtxtMarks.Enabled == true)
                            {
                                Status = 0;
                            }
                            if (hfStatusL.Value == "0" || hfStatusL.Value == "1" || hfStatusL.Value == "3" || hfStatusL.Value == "5" || HfAwardlistStatus.Value == "Rejected by HOD" || HfAwardlistStatus.Value == "Unlocked by Principal" || HfAwardlistStatus.Value == "")   // 0,1,3,5 for Pending else not editable 
                            {
                                //SqlCommand cmd1 = new SqlCommand("sp_InsertInternalMarksEntryAndAttendanceLine2", con);
                                //cmd1.CommandType = CommandType.StoredProcedure;
                                _DataLine["CollegeCode"] = Session["GlobalDimension1Code"].ToString();
                                _DataLine["AcadmicYear"] = drpAcademicYear.SelectedValue;


                                //&& grdtxtMarks.Text != "DT" && grdtxtMarks.Text != "UFM"
                                if (grdtxtMarks.Text != "AB" && grdtxtMarks.Text != "DT" && grdtxtMarks.Text != "UFM" && grdtxtMarks.Text != "ab" && grdtxtMarks.Text != "dt" && grdtxtMarks.Text != "ufm")
                                {
                                    if (grdtxtMarks.Text != "")
                                    {

                                        _DataLine["InernalMark"] = Convert.ToDecimal(grdtxtMarks.Text);
                                        _DataLine["AttendanceType"] = "1";
                                    }
                                    else
                                    {
                                        _DataLine["InernalMark"] = Convert.ToDecimal("-1");
                                        _DataLine["AttendanceType"] = "1";
                                    }
                                }
                                else
                                {
                                    if (grdtxtMarks.Text == "AB" || grdtxtMarks.Text == "ab")
                                    {
                                        _DataLine["AttendanceType"] = "2";
                                        _DataLine["InernalMark"] = Convert.ToDecimal("-1");
                                    }
                                    if (grdtxtMarks.Text == "DT" || grdtxtMarks.Text == "dt")
                                    {
                                        _DataLine["AttendanceType"] = "3";
                                        _DataLine["InernalMark"] = Convert.ToDecimal("-1");
                                    }
                                    if (grdtxtMarks.Text == "UFM" || grdtxtMarks.Text == "ufm")
                                    {
                                        _DataLine["AttendanceType"] = "4";
                                        _DataLine["InernalMark"] = Convert.ToDecimal("-1");
                                    }


                                }
                                _DataLine["StudentNo"] = id;
                                _DataLine["facultyCode"] = Session["uid"].ToString();
                                _DataLine["Status"] = Status;

                                _DataLine["CourseCode"] = drpCourse.SelectedValue;
                                _DataLine["SemesterYear"] = drpSemester.SelectedValue;
                                _DataLine["SubjectCode"] = SubjectCode;
                                _DataLine["SubjectType"] = SubjectType;
                                _DataLine["ExamCriteria"] = ExamCriteria;
                                _DataLine["Method"] = Method;
                                _DataLine["Group"] = Group;
                                if (grdSelect.Rows.Count > 0)
                                {
                                    _DataLine["ExamType"] = hfExamType1.Value;
                                }
                                else
                                {
                                    _DataLine["ExamType"] = hf_ExamType.Value;
                                }

                                if (drpSection.SelectedIndex > 0)
                                {
                                    _DataLine["Section"] = drpSection.SelectedValue;
                                }
                                else
                                {
                                    _DataLine["Section"] = "";
                                }
                                if (ddlGroup.SelectedIndex > 0)
                                {

                                    _DataLine["StudentGroup"] = ddlGroup.SelectedValue;
                                }
                                else
                                {
                                    _DataLine["StudentGroup"] = "";
                                }
                                _DataLine["AwardListStatus"] = Reportstatus;
                                _DataLine["CO1"] = Convert.ToDecimal(grdtxtCO1.Text);
                                _DataLine["CO2"] = Convert.ToDecimal(grdtxtCO2.Text);
                                _DataLine["CO3"] = Convert.ToDecimal(grdtxtCO3.Text);
                                _DataLine["CO4"] = Convert.ToDecimal(grdtxtCO4.Text);
                                _DataLine["CO5"] = Convert.ToDecimal(grdtxtCO5.Text);
                                if (grdtxtMarks.Visible == false)
                                {
                                    _DataLine["InernalMark"] = Convert.ToDecimal(grdtxtCO1.Text) + Convert.ToDecimal(grdtxtCO2.Text) + Convert.ToDecimal(grdtxtCO3.Text) + Convert.ToDecimal(grdtxtCO4.Text) + Convert.ToDecimal(grdtxtCO5.Text);
                                }
                                MarksLine.Rows.Add(_DataLine);
                                //if (con.State != ConnectionState.Open)
                                //{
                                //    con.Close();
                                //}
                                //con.Open();
                                //cmd1.ExecuteNonQuery();

                                //con.Close();
                            }
                        }
                    }
                }
                else
                {
                    //  for mark  entry header line           
                    foreach (GridViewRow row in grdViewmarksEntry.Rows)
                    {
                        DataRow _DataLine = MarksLine.NewRow();
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            TextBox grdtxtMarks = (TextBox)row.FindControl("grdtxtMarks");
                            TextBox grdtxtCO1 = (TextBox)row.FindControl("grdtxtCO1");
                            TextBox grdtxtCO2 = (TextBox)row.FindControl("grdtxtCO2");
                            TextBox grdtxtCO3 = (TextBox)row.FindControl("grdtxtCO3");
                            TextBox grdtxtCO4 = (TextBox)row.FindControl("grdtxtCO4");
                            TextBox grdtxtCO5 = (TextBox)row.FindControl("grdtxtCO5");
                            HiddenField hfStatusL = (HiddenField)row.FindControl("hfStatusL");
                            var id = grdViewmarksEntry.DataKeys[row.RowIndex].Value;
                            int Status = Convert.ToInt16(hf_Status.Value);
                            if (grdtxtMarks.Text == "" && grdtxtMarks.Enabled == true)
                            {
                                Status = 0;
                            }
                            if (hfStatusL.Value == "0" || hfStatusL.Value == "1" || hfStatusL.Value == "3" || hfStatusL.Value == "5" || HfAwardlistStatus.Value == "Rejected by HOD" || HfAwardlistStatus.Value == "Unlocked by Principal" || HfAwardlistStatus.Value == "")   // 0,1,3,5 for Pending else not editable 
                            {
                                //SqlCommand cmd1 = new SqlCommand("sp_InsertInternalMarksEntryAndAttendanceLine2", con);
                                //cmd1.CommandType = CommandType.StoredProcedure;
                                _DataLine["CollegeCode"] = Session["GlobalDimension1Code"].ToString();
                                _DataLine["AcadmicYear"] = drpAcademicYear.SelectedValue;

                                //&& grdtxtMarks.Text != "DT" && grdtxtMarks.Text != "UFM"
                                if (grdtxtMarks.Text != "AB" && grdtxtMarks.Text != "DT" && grdtxtMarks.Text != "UFM" && grdtxtMarks.Text != "ab" && grdtxtMarks.Text != "dt" && grdtxtMarks.Text != "ufm")
                                {
                                    if (grdtxtMarks.Text != "")
                                    {

                                        _DataLine["InernalMark"] = Convert.ToDecimal(grdtxtMarks.Text);
                                        _DataLine["AttendanceType"] = "1";
                                    }
                                    else
                                    {
                                        _DataLine["InernalMark"] = Convert.ToDecimal("-1");
                                        _DataLine["AttendanceType"] = "1";
                                    }
                                }
                                else
                                {
                                    if (grdtxtMarks.Text == "AB" || grdtxtMarks.Text == "ab")
                                    {
                                        _DataLine["AttendanceType"] = "2";
                                        _DataLine["InernalMark"] = Convert.ToDecimal("-1");
                                    }
                                    if (grdtxtMarks.Text == "DT" || grdtxtMarks.Text == "dt")
                                    {
                                        _DataLine["AttendanceType"] = "3";
                                        _DataLine["InernalMark"] = Convert.ToDecimal("-1");
                                    }
                                    if (grdtxtMarks.Text == "UFM" || grdtxtMarks.Text == "ufm")
                                    {
                                        _DataLine["AttendanceType"] = "4";
                                        _DataLine["InernalMark"] = Convert.ToDecimal("-1");
                                    }


                                }
                                _DataLine["StudentNo"] = id;
                                _DataLine["facultyCode"] = Session["uid"].ToString();
                                _DataLine["Status"] = Status;

                                _DataLine["CourseCode"] = drpCourse.SelectedValue;
                                _DataLine["SemesterYear"] = drpSemester.SelectedValue;
                                _DataLine["SubjectCode"] = SubjectCode;
                                _DataLine["SubjectType"] = SubjectType;
                                _DataLine["ExamCriteria"] = ExamCriteria;
                                _DataLine["Method"] = Method;
                                _DataLine["Group"] = Group;

                                if (grdtxtCO1.Text == "AB" || grdtxtCO1.Text == "DT" || grdtxtCO1.Text == "UFM" || grdtxtCO1.Text == "ab" || grdtxtCO1.Text == "dt" || grdtxtCO1.Text == "ufm")
                                {
                                    if (grdtxtCO1.Text == "AB" || grdtxtCO1.Text == "ab")
                                    {
                                        _DataLine["AttendanceType"] = "2";
                                        _DataLine["CO1"] = Convert.ToDecimal("-1");
                                        grdtxtMarks.Text = "AB";
                                    }
                                    if (grdtxtCO1.Text == "DT" || grdtxtCO1.Text == "dt")
                                    {
                                        _DataLine["AttendanceType"] = "3";
                                        _DataLine["CO1"] = Convert.ToDecimal("-1");
                                        grdtxtMarks.Text = "DT";
                                    }
                                    if (grdtxtCO1.Text == "UFM" || grdtxtCO1.Text == "ufm")
                                    {
                                        _DataLine["AttendanceType"] = "4";
                                        _DataLine["CO1"] = Convert.ToDecimal("-1");
                                        grdtxtMarks.Text = "UFM";
                                    }
                                }
                                else
                                {
                                    if (grdtxtCO1.Text != "")
                                    {

                                        _DataLine["CO1"] = Convert.ToDecimal(grdtxtCO1.Text);
                                        _DataLine["AttendanceType"] = "1";

                                    }
                                    else
                                    {
                                        _DataLine["CO1"] = Convert.ToDecimal("-1");
                                        _DataLine["AttendanceType"] = "1";
                                    }
                                }

                                if (grdtxtCO2.Text == "AB" || grdtxtCO2.Text == "DT" || grdtxtCO2.Text == "UFM" || grdtxtCO2.Text == "ab" || grdtxtCO2.Text == "dt" || grdtxtCO2.Text == "ufm")
                                {
                                    if (grdtxtCO2.Text == "AB" || grdtxtCO2.Text == "ab")
                                    {
                                        _DataLine["AttendanceType"] = "2";
                                        _DataLine["CO2"] = Convert.ToDecimal("-1");
                                        grdtxtMarks.Text = "AB";
                                    }
                                    if (grdtxtCO2.Text == "DT" || grdtxtCO2.Text == "dt")
                                    {
                                        _DataLine["AttendanceType"] = "3";
                                        _DataLine["CO2"] = Convert.ToDecimal("-1");
                                        grdtxtMarks.Text = "DT";
                                    }
                                    if (grdtxtCO2.Text == "UFM" || grdtxtCO2.Text == "ufm")
                                    {
                                        _DataLine["AttendanceType"] = "4";
                                        _DataLine["CO2"] = Convert.ToDecimal("-1");
                                        grdtxtMarks.Text = "UFM";
                                    }
                                }
                                else
                                {
                                    if (grdtxtCO2.Text != "")
                                    {

                                        _DataLine["CO2"] = Convert.ToDecimal(grdtxtCO2.Text);
                                        _DataLine["AttendanceType"] = "1";

                                    }
                                    else
                                    {
                                        _DataLine["CO2"] = Convert.ToDecimal("-1");
                                        _DataLine["AttendanceType"] = "1";
                                    }
                                }

                                if (grdtxtCO3.Text == "AB" || grdtxtCO3.Text == "DT" || grdtxtCO3.Text == "UFM" || grdtxtCO3.Text == "ab" || grdtxtCO3.Text == "dt" || grdtxtCO3.Text == "ufm")
                                {
                                    if (grdtxtCO3.Text == "AB" || grdtxtCO3.Text == "ab")
                                    {
                                        _DataLine["AttendanceType"] = "2";
                                        _DataLine["CO3"] = Convert.ToDecimal("-1");
                                        grdtxtMarks.Text = "AB";
                                    }
                                    if (grdtxtCO3.Text == "DT" || grdtxtCO3.Text == "dt")
                                    {
                                        _DataLine["AttendanceType"] = "3";
                                        _DataLine["CO3"] = Convert.ToDecimal("-1");
                                        grdtxtMarks.Text = "DT";
                                    }
                                    if (grdtxtCO3.Text == "UFM" || grdtxtCO3.Text == "ufm")
                                    {
                                        _DataLine["AttendanceType"] = "4";
                                        _DataLine["CO3"] = Convert.ToDecimal("-1");
                                        grdtxtMarks.Text = "UFM";
                                    }
                                }
                                else
                                {
                                    if (grdtxtCO3.Text != "")
                                    {

                                        _DataLine["CO3"] = Convert.ToDecimal(grdtxtCO3.Text);
                                        _DataLine["AttendanceType"] = "1";

                                    }
                                    else
                                    {
                                        _DataLine["CO3"] = Convert.ToDecimal("-1");
                                        _DataLine["AttendanceType"] = "1";
                                    }
                                }
                                if (grdtxtCO4.Text == "AB" || grdtxtCO4.Text == "DT" || grdtxtCO4.Text == "UFM" || grdtxtCO4.Text == "ab" || grdtxtCO4.Text == "dt" || grdtxtCO4.Text == "ufm")
                                {
                                    if (grdtxtCO4.Text == "AB" || grdtxtCO4.Text == "ab")
                                    {
                                        _DataLine["AttendanceType"] = "2";
                                        _DataLine["CO4"] = Convert.ToDecimal("-1");
                                        grdtxtMarks.Text = "AB";
                                    }
                                    if (grdtxtCO4.Text == "DT" || grdtxtCO4.Text == "dt")
                                    {
                                        _DataLine["AttendanceType"] = "3";
                                        _DataLine["CO4"] = Convert.ToDecimal("-1");
                                        grdtxtMarks.Text = "DT";
                                    }
                                    if (grdtxtCO4.Text == "UFM" || grdtxtCO4.Text == "ufm")
                                    {
                                        _DataLine["AttendanceType"] = "4";
                                        _DataLine["CO4"] = Convert.ToDecimal("-1");
                                        grdtxtMarks.Text = "UFM";
                                    }
                                }
                                else
                                {
                                    if (grdtxtCO4.Text != "")
                                    {

                                        _DataLine["CO4"] = Convert.ToDecimal(grdtxtCO4.Text);
                                        _DataLine["AttendanceType"] = "1";

                                    }
                                    else
                                    {
                                        _DataLine["CO4"] = Convert.ToDecimal("-1");
                                        _DataLine["AttendanceType"] = "1";
                                    }
                                }

                                if (grdtxtCO5.Text == "AB" || grdtxtCO5.Text == "DT" || grdtxtCO5.Text == "UFM" || grdtxtCO5.Text == "ab" || grdtxtCO5.Text == "dt" || grdtxtCO5.Text == "ufm")
                                {
                                    if (grdtxtCO5.Text == "AB" || grdtxtCO5.Text == "ab")
                                    {
                                        _DataLine["AttendanceType"] = "2";
                                        _DataLine["CO5"] = Convert.ToDecimal("-1");
                                        grdtxtMarks.Text = "AB";
                                    }
                                    if (grdtxtCO5.Text == "DT" || grdtxtCO5.Text == "dt")
                                    {
                                        _DataLine["AttendanceType"] = "3";
                                        _DataLine["CO5"] = Convert.ToDecimal("-1");
                                        grdtxtMarks.Text = "DT";
                                    }
                                    if (grdtxtCO5.Text == "UFM" || grdtxtCO5.Text == "ufm")
                                    {
                                        _DataLine["AttendanceType"] = "4";
                                        _DataLine["CO5"] = Convert.ToDecimal("-1");
                                        grdtxtMarks.Text = "UFM";
                                    }
                                }
                                else
                                {
                                    if (grdtxtCO5.Text != "")
                                    {

                                        _DataLine["CO5"] = Convert.ToDecimal(grdtxtCO5.Text);
                                        _DataLine["AttendanceType"] = "1";

                                    }
                                    else
                                    {
                                        _DataLine["CO5"] = Convert.ToDecimal("-1");
                                        _DataLine["AttendanceType"] = "1";
                                    }
                                }


                                if (grdtxtMarks.Text == "AB" || grdtxtMarks.Text == "DT" || grdtxtMarks.Text == "UFM" || grdtxtMarks.Text == "ab" || grdtxtMarks.Text == "dt" || grdtxtMarks.Text == "ufm")
                                {

                                    if (grdtxtMarks.Text == "AB" || grdtxtMarks.Text == "ab")
                                    {
                                        _DataLine["AttendanceType"] = "2";
                                        _DataLine["InernalMark"] = Convert.ToDecimal("-1");
                                    }
                                    if (grdtxtMarks.Text == "DT" || grdtxtMarks.Text == "dt")
                                    {
                                        _DataLine["AttendanceType"] = "3";
                                        _DataLine["InernalMark"] = Convert.ToDecimal("-1");
                                    }
                                    if (grdtxtMarks.Text == "UFM" || grdtxtMarks.Text == "ufm")
                                    {
                                        _DataLine["AttendanceType"] = "4";
                                        _DataLine["InernalMark"] = Convert.ToDecimal("-1");
                                    }
                                }

                                else
                                {
                                    if (grdtxtMarks.Text != "")
                                    {

                                        _DataLine["InernalMark"] = Convert.ToDecimal(grdtxtMarks.Text);
                                        _DataLine["AttendanceType"] = "1";
                                    }

                                    else
                                    {
                                        _DataLine["InernalMark"] = Convert.ToDecimal("-1");
                                        _DataLine["AttendanceType"] = "1";
                                    }
                                }
                                if (grdtxtMarks.Visible == false)
                                {
                                    _DataLine["InernalMark"] = Convert.ToDecimal(_DataLine["CO1"]) + Convert.ToDecimal(_DataLine["CO2"]) + Convert.ToDecimal(_DataLine["CO3"]) + Convert.ToDecimal(_DataLine["CO4"]) + Convert.ToDecimal(_DataLine["CO5"]);
                                }

                                if (Convert.ToDecimal(_DataLine["InernalMark"]) < 0)
                                {
                                    _DataLine["InernalMark"] = Convert.ToDecimal("-1");
                                }

                                if (grdSelect.Rows.Count > 0)
                                {
                                    _DataLine["ExamType"] = hfExamType1.Value;
                                }
                                else
                                {
                                    _DataLine["ExamType"] = hf_ExamType.Value;
                                }
                                if (drpSection.SelectedIndex > 0)
                                {
                                    _DataLine["Section"] = drpSection.SelectedValue;
                                }
                                else
                                {
                                    _DataLine["Section"] = "";
                                }
                                if (ddlGroup.SelectedIndex > 0)
                                {

                                    _DataLine["StudentGroup"] = ddlGroup.SelectedValue;
                                }
                                else
                                {
                                    _DataLine["StudentGroup"] = "";
                                }
                                _DataLine["AwardListStatus"] = Reportstatus;
                                _DataLine["AwardListStatus"] = Reportstatus;
                                MarksLine.Rows.Add(_DataLine);



                            }
                        }
                    }
                }


                SqlCommand cmd = new SqlCommand("sp_InsertInternalMarksEntryHeaderLine", con);//Insert_proc_MarksEntryHeader1
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());//Session["GlobalDimension1Code"]
                cmd.Parameters.AddWithValue("@AcadmicYear", AcademicYear);
                cmd.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@CourseCode", CourseCode);
                cmd.Parameters.AddWithValue("@SemesterYear", SemYear);
                cmd.Parameters.AddWithValue("@SubjectCode", SubjectCode);
                cmd.Parameters.AddWithValue("@SubjectType", SubjectType);
                cmd.Parameters.AddWithValue("@ExamCriteria", ExamCriteria);
                cmd.Parameters.AddWithValue("@Status", hf_Status.Value);
                cmd.Parameters.AddWithValue("@Method", Method);
                cmd.Parameters.AddWithValue("@Group", Group);
                cmd.Parameters.AddWithValue("@MaxMarks", MaxMarks);
                cmd.Parameters.AddWithValue("@CO1Max", CO1Max);
                cmd.Parameters.AddWithValue("@CO2Max", CO2Max);
                cmd.Parameters.AddWithValue("@CO3Max", CO3Max);
                cmd.Parameters.AddWithValue("@CO4Max", CO4Max);
                cmd.Parameters.AddWithValue("@CO5Max", CO5Max);

                if (grdSelect.Rows.Count > 0)
                {
                    cmd.Parameters.AddWithValue("@ExamType", hfExamType1.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ExamType", hf_ExamType.Value);

                }
                if (drpSection.SelectedIndex > 0)
                {
                    cmd.Parameters.AddWithValue("@Section", drpSection.SelectedValue);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Section", "");
                }
                if (ddlGroup.SelectedIndex > 0)
                {

                    cmd.Parameters.AddWithValue("@StudentGroup", ddlGroup.SelectedValue);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@StudentGroup", "");
                }
                cmd.Parameters.AddWithValue("@AwardListStatus", Reportstatus);
                cmd.Parameters.AddWithValue("@dtfinal", MarksLine);
                if (con.State == ConnectionState.Open)
                { con.Close(); }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();


                scope.Complete();

            }
            catch (Exception ex)
            {
                scope.Dispose();
            }
        }
    }
    public void bindSubject()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("sp_getSubjrctCodeFacultyWise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedItem.Value.ToString());
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedItem.Value.ToString());
            cmd.Parameters.Add("@Semester", drpSemester.SelectedItem.Value.ToString());
            if (rdInternal.Checked == true)
            {
                cmd.Parameters.Add("@ExamType", "Internal");
            }
            else
            {
                cmd.Parameters.Add("@ExamType", "External");
            }


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddlSubject.DataSource = dt;
            ddlSubject.DataTextField = "Description";
            ddlSubject.DataValueField = "Subject Code";
            ddlSubject.DataBind();
            ddlSubject.Items.Insert(0, "--Course--");
        }
        catch (Exception ex) { }
    }
    public void bindSectionList()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetSectionFromCourseWiseFaculty_RoleNew", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@Subject", ddlSubject.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSection.DataSource = dt;
        drpSection.DataTextField = "Details";
        drpSection.DataValueField = "No_";
        drpSection.DataBind();
    }
    public void bindGroupList()
    {
        SqlCommand cmd = new SqlCommand("sp_GetGroup_RoleNew", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@SubjectCode", ddlSubject.SelectedValue);
        cmd.Parameters.Add("@SectionCode", drpSection.SelectedValue);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        ddlGroup.DataSource = dt;
        ddlGroup.DataTextField = "Details";
        ddlGroup.DataValueField = "No_";
        ddlGroup.DataBind();
    }
    public void bindBatchList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetBatchFromCourseSemester_RoleNew", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@SubjectCode", ddlSubject.SelectedValue);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        ddlBatch.DataSource = dt;
        ddlBatch.DataTextField = "Details";
        ddlBatch.DataValueField = "No_";
        ddlBatch.DataBind();
    }

    protected void BtnSubmit1_Click(object sender, EventArgs e)
    {
        if (hf_Classification.Value != "THEORY" && hf_ExamGroup.Value != "IOS" && hf_ExamType.Value == "2")
        {
            hf_Status.Value = "6"; //Submit
        }
        else
        {
            hf_Status.Value = "2";

        }


        if (ValidateBlankDataForSaveSubmit() == false) { return; }
        else
        {
            GridView gr = new GridView();

            SaveOrSubmitData(hf_AcademicYear.Value, hf_Course.Value, hf_SemYear.Value, hf_SubjCode.Value, hf_SubjectType.Value, hf_ExamCriteria.Value, hf_ExamMethod.Value, hf_ExamGroup.Value, Convert.ToDecimal(hf_MaxMarks.Value), 1, gr, Convert.ToDecimal(txtCo1Max.Text), Convert.ToDecimal(txtCo2Max.Text), Convert.ToDecimal(txtCo3Max.Text), Convert.ToDecimal(txtCo4Max.Text), Convert.ToDecimal(txtCo5Max.Text));
            //getmethodcount();

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Data Submitted Successfully');", true);

            LblInstruction.Visible = false;
            bindgrid();
            VisibleFalseTrue(false, false, true, false);
        }
    }
    protected void grdmarktable_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string SubjectCode = (string)this.grdmarktable.DataKeys[e.Row.RowIndex]["Subject Code"];
                string SubjectType = (string)this.grdmarktable.DataKeys[e.Row.RowIndex]["Subject Type"];
                int Status = (int)this.grdmarktable.DataKeys[e.Row.RowIndex]["Status"];
                string ExamMethod = (string)this.grdmarktable.DataKeys[e.Row.RowIndex]["ExamMethod"];
                string ExamGroup = (string)this.grdmarktable.DataKeys[e.Row.RowIndex]["ExamGroup"];
                decimal MaxMarks = (decimal)this.grdmarktable.DataKeys[e.Row.RowIndex]["MaxMarks"];
                string DocumentNo = (string)this.grdmarktable.DataKeys[e.Row.RowIndex]["DocumentNo"];
                int ExamType = (int)this.grdmarktable.DataKeys[e.Row.RowIndex]["Exam Type"];
                //hfExamType1.Value = ExamGroup;
                //string customerId = grdmarktable.DataKeys[e.Row.RowIndex].Value.ToString();
                SqlCommand cmd = new SqlCommand("sp_GetStudentForInternalMarksEntry_PNC_Test_Internal2New", con);//sp_GetStudentForInternalMarksEntry usp_GetMarkEntryStudent//sp_GetStudentForInternalMarksEntry
                //sp_GetStudentForInternalMarksEntry_PNC
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                cmd.Parameters.AddWithValue("@AcadmicYear", drpAcademicYear.SelectedValue);
                cmd.Parameters.AddWithValue("@CourseCode", drpCourse.SelectedValue);
                cmd.Parameters.AddWithValue("@Semester", drpSemester.SelectedValue);
                cmd.Parameters.AddWithValue("@SubjectCode", SubjectCode);
                cmd.Parameters.AddWithValue("@SubjectType", SubjectType);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@Method", ExamMethod);
                cmd.Parameters.AddWithValue("@Group", ExamGroup);
                cmd.Parameters.AddWithValue("@MaxMarks", MaxMarks);
                cmd.Parameters.AddWithValue("@DocumentNo", DocumentNo);
                cmd.Parameters.AddWithValue("@ExamType", ExamType);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                con.Open();
                da.Fill(ds);
                con.Close();
                GridView gvOrders = e.Row.FindControl("grdViewmarksEntry1") as GridView;
                gvOrders.DataSource = ds.Tables[0];
                gvOrders.DataBind();
                try
                {
                    Button Save = ((Button)gvOrders.FooterRow.FindControl("btnSavedata"));
                    Button Submit = ((Button)gvOrders.FooterRow.FindControl("BtnSubmitdata"));

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Btnprint.Visible = true;

                        if (Status == 0 || Status == 1 || Status == 3 || Status == 5 || Status == 7)
                        { VisibleFalseTrue(true, true, false, true); }
                        else
                        {
                            Save.Visible = false;
                            Submit.Visible = false;
                            VisibleFalseTrue(false, false, false, true);
                        }
                    }

                    else
                    {
                        Btnprint.Visible = false;
                    }
                }
                catch (Exception ex)
                {

                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    lbltotalst.Text = ds.Tables[1].Rows[0]["Total Student"].ToString();
                    lblpresentst.Text = ds.Tables[1].Rows[0]["Present"].ToString();
                    lblabsentst.Text = ds.Tables[1].Rows[0]["Absent"].ToString();
                }
                else
                {
                    lbltotalst.Text = "";
                    lblpresentst.Text = "";
                    lblabsentst.Text = "";
                }

                if (ds.Tables[2].Rows.Count > 0)
                {


                    LblValue.Text = drpCourse.SelectedValue;
                    LblDesc.Text = ds.Tables[2].Rows[0]["Description"].ToString();
                    LblMethod.Text = hf_ExamMethod.Value;
                    LblOddEven.Text = ds.Tables[2].Rows[0]["ODDEven"].ToString();
                    LblSubDesc.Text = ddlSubject.SelectedItem.Text;
                    LblNopresent.Text = lblpresentst.Text;
                    LblNoAbsent.Text = lblabsentst.Text;
                }
                else
                {
                    lbltotalst.Text = "";
                    lblpresentst.Text = "";
                    lblabsentst.Text = "";
                }

            }

        }
        catch (Exception ex)
        {
        }

    }

    protected void btnSavedata_Click(object sender, EventArgs e)
    {
        GridViewRow Gv2Row = (GridViewRow)((Button)sender).NamingContainer;
        GridView Childgrid = (GridView)(Gv2Row.Parent.Parent);
        GridViewRow Gv1Row = (GridViewRow)(Childgrid.NamingContainer);
        int GridviewRowIndex = Gv1Row.RowIndex;
        Button btn = (Button)sender;
        GridView gvR = (GridView)grdmarktable.Rows[GridviewRowIndex].FindControl("grdViewmarksEntry1");
        HiddenField grdlblDocumentNo = (HiddenField)grdmarktable.Rows[GridviewRowIndex].FindControl("grdlblDocumentNo");
        string SubjectCode = (string)this.grdmarktable.DataKeys[GridviewRowIndex]["Subject Code"];
        string SubjectType = (string)this.grdmarktable.DataKeys[GridviewRowIndex]["Subject Type"];
        int Status = (int)this.grdmarktable.DataKeys[GridviewRowIndex]["Status"];
        string ExamMethod = (string)this.grdmarktable.DataKeys[GridviewRowIndex]["ExamMethod"];
        string ExamGroup = (string)this.grdmarktable.DataKeys[GridviewRowIndex]["ExamGroup"];
        decimal MaxMarks = (decimal)this.grdmarktable.DataKeys[GridviewRowIndex]["MaxMarks"];
        string DocumentNo = grdlblDocumentNo.Value;
        int ExamType1 = (int)this.grdmarktable.DataKeys[GridviewRowIndex]["Exam Type"];
        hfExamType1.Value = ExamType1.ToString();
        hf_Status.Value = "1"; //Save
        ValidateBlankDataForSaveSubmit(gvR);
        if (lblError.Text == "")
        {
            SaveOrSubmitData(hf_AcademicYear.Value, hf_Course.Value, hf_SemYear.Value, SubjectCode, SubjectType, DocumentNo, ExamMethod, ExamGroup, Convert.ToDecimal(MaxMarks), 0, gvR, Convert.ToDecimal(txtCo1Max.Text), Convert.ToDecimal(txtCo2Max.Text), Convert.ToDecimal(txtCo3Max.Text), Convert.ToDecimal(txtCo4Max.Text), Convert.ToDecimal(txtCo5Max.Text));
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Data saved Successfully');", true);
            bindgrid();

            VisibleFalseTrue(false, false, true, false);
        }
        bindgrid();

    }
    protected void BtnSubmitdata_Click(object sender, EventArgs e)
    {
        GridViewRow Gv2Row = (GridViewRow)((Button)sender).NamingContainer;
        GridView Childgrid = (GridView)(Gv2Row.Parent.Parent);
        GridViewRow Gv1Row = (GridViewRow)(Childgrid.NamingContainer);
        int GridviewRowIndex = Gv1Row.RowIndex;
        Button btn = (Button)sender;
        GridView gvR = (GridView)grdmarktable.Rows[GridviewRowIndex].FindControl("grdViewmarksEntry1");
        HiddenField grdlblDocumentNo = (HiddenField)grdmarktable.Rows[GridviewRowIndex].FindControl("grdlblDocumentNo");
        string SubjectCode = (string)this.grdmarktable.DataKeys[GridviewRowIndex]["Subject Code"];
        string SubjectType = (string)this.grdmarktable.DataKeys[GridviewRowIndex]["Subject Type"];
        int Status = (int)this.grdmarktable.DataKeys[GridviewRowIndex]["Status"];
        string ExamMethod = (string)this.grdmarktable.DataKeys[GridviewRowIndex]["ExamMethod"];
        string ExamGroup = (string)this.grdmarktable.DataKeys[GridviewRowIndex]["ExamGroup"];
        decimal MaxMarks = (decimal)this.grdmarktable.DataKeys[GridviewRowIndex]["MaxMarks"];
        string DocumentNo = grdlblDocumentNo.Value;
        int ExamType1 = (int)this.grdmarktable.DataKeys[GridviewRowIndex]["Exam Type"];
        hfExamType1.Value = ExamType1.ToString();
        hf_Status.Value = "2"; //Submit
        if (ValidateBlankDataForSaveSubmit(gvR) == false) { return; }
        else
        {

            SaveOrSubmitData(hf_AcademicYear.Value, hf_Course.Value, hf_SemYear.Value, SubjectCode, SubjectType, DocumentNo, ExamMethod, ExamGroup, Convert.ToDecimal(MaxMarks), 1, gvR, Convert.ToDecimal(txtCo1Max.Text), Convert.ToDecimal(txtCo2Max.Text), Convert.ToDecimal(txtCo3Max.Text), Convert.ToDecimal(txtCo4Max.Text), Convert.ToDecimal(txtCo5Max.Text));
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Data Submitted Successfully');", true);
            bindgrid();
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        GridViewRow grow = btn.NamingContainer as GridViewRow;
        Label grdlblMethod = (Label)grow.FindControl("grdlblMethod");
        HiddenField hfsubject = (HiddenField)grow.FindControl("hfSubject");
        Session["Subject"] = hfsubject.Value;
        Session["AcademicYear"] = drpAcademicYear.SelectedValue;
        Session["drpCourse"] = drpCourse.SelectedValue;
        Session["drpSemester"] = drpSemester.SelectedValue;
        Session["ExamMethod"] = grdlblMethod.Text;
        Session["FacultyCode1"] = Session["uid"].ToString();
        Session["Section"] = drpSection.SelectedValue;
        Session["StudGroup"] = ddlGroup.SelectedValue;


        ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "OpenNewWindow()", true);

    }


    protected void BtnSaveBottom_Click(object sender, EventArgs e)
    {
        try
        {
            hf_Status.Value = "1"; //Save
            ValidateBlankDataForSaveSubmit();
            if (lblError.Text == "")
            {
                GridView gr = new GridView();
                if (rdOpen.Checked == true)
                {
                    SaveOrSubmitDataOpen(drpAcademicYear1.SelectedValue, hf_Course.Value, "", ddlSubject1.SelectedValue, hf_SubjectType.Value, hf_ExamCriteria.Value, hf_ExamMethod.Value, hf_ExamGroup.Value, Convert.ToDecimal(hf_MaxMarks.Value), 0, gr);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Data saved Successfully');", true);
                    bindGrid1();

                    VisibleFalseTrue(false, false, true, false);
                }
                else
                {
                    SaveOrSubmitData(hf_AcademicYear.Value, hf_Course.Value, hf_SemYear.Value, hf_SubjCode.Value, hf_SubjectType.Value, hf_ExamCriteria.Value, hf_ExamMethod.Value, hf_ExamGroup.Value, Convert.ToDecimal(hf_MaxMarks.Value), 0, gr, Convert.ToDecimal(txtCo1Max.Text), Convert.ToDecimal(txtCo2Max.Text), Convert.ToDecimal(txtCo3Max.Text), Convert.ToDecimal(txtCo4Max.Text), Convert.ToDecimal(txtCo5Max.Text));
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Data saved Successfully');", true);
                    bindgrid();

                    VisibleFalseTrue(false, false, true, false);
                }
            }
        }
        catch (Exception ex)
        {
        }
    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string SavePivotData(string string_rowdata)
    {
        try
        {
            DataTable JSONTABLE = new DataTable();
            DataTable dtpivot = new DataTable();
            Faculty_MarksEntry obj = new Faculty_MarksEntry();
            DataTable dtgrid = (DataTable)obj.Session["dtPivot"];
            JSONTABLE = obj.JsonStringToDataTable(string_rowdata);
            dtpivot = obj.RemoveDuplicatesRecords(JSONTABLE);
            string result = obj.SavePivotDatarowwise(1, dtpivot);
            if (result == "Success")
            {
                return "All Success";
            }
            else
            {
                return "Error";
            }
        }
        catch (Exception ex)
        {
            return "Error";

        }

    }
    private DataTable RemoveDuplicatesRecords(DataTable dt)
    {
        //Returns just 5 unique rows
        var UniqueRows = dt.AsEnumerable().Distinct(DataRowComparer.Default);
        DataTable dt2 = UniqueRows.CopyToDataTable();
        return dt2;
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string SubmitPivotData(string string_rowdata)
    {

        try
        {
            DataTable JSONTABLE = new DataTable();
            DataTable dtpivot = new DataTable();

            Faculty_MarksEntry obj = new Faculty_MarksEntry();
            DataTable dtgrid = (DataTable)obj.Session["dtPivot"];
            JSONTABLE = obj.JsonStringToDataTable(string_rowdata);
            dtpivot = obj.RemoveDuplicatesRecords(JSONTABLE);
            string result = obj.SavePivotDatarowwise(2, dtpivot);

            if (result == "Success")
            {
                return "All Success";
            }
            else
            {
                return "Error";
            }
        }
        catch (Exception ex)
        {
            return "Error";

        }
    }

    public DataTable JsonStringToDataTable(string jsonString)
    {
        DataTable dt = new DataTable();
        string[] jsonStringArray = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");
        List<string> ColumnsName = new List<string>();
        foreach (string jSA in jsonStringArray)
        {
            string[] jsonStringData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
            foreach (string ColumnsNameData in jsonStringData)
            {
                try
                {
                    int idx = ColumnsNameData.IndexOf(":");
                    string ColumnsNameString = ColumnsNameData.Substring(0, idx);
                    if (!ColumnsName.Contains(ColumnsNameString))
                    {
                        ColumnsName.Add(ColumnsNameString);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                }
            }
            break;
        }
        foreach (string AddColumnName in ColumnsName)
        {
            dt.Columns.Add(AddColumnName);
        }
        foreach (string jSA in jsonStringArray)
        {
            string[] RowData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
            DataRow nr = dt.NewRow();
            foreach (string rowData in RowData)
            {
                try
                {
                    int idx = rowData.IndexOf(":");
                    string RowColumns = rowData.Substring(0, idx);
                    string RowDataString = rowData.Substring(idx + 1).Replace("\"", "");
                    nr[RowColumns] = RowDataString;
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            dt.Rows.Add(nr);
        }
        return dt;
    }
    protected void btnPivotSave_Click(object sender, EventArgs e)
    {


        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Data saved Successfully');", true);

        //bindgrid();
        // LblInstruction.Visible = false;
        //VisibleFalseTrue(false, false, true, false);
        grdpivot.Visible = false;
        btnPivotSave.Visible = false;
        btnPivotSubmit.Visible = false;
        LblInstruction.Visible = false;
        lblMaxmark.Visible = false;
        //veer

    }

    protected void grdpivot_RowDataBound(object sender, GridViewRowEventArgs e)
    {





        DataTable dt = (DataTable)Session["dtPivot"];

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            HiddenField enabledisable = (HiddenField)e.Row.FindControl("enabledisable");
            enabledisable.Visible = false;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                try
                {
                    string cabecera = dt.Columns[i].ColumnName;
                    if (cabecera == "Enrollement_No" || cabecera == "Student Name" || cabecera == "AdmittedYear" || cabecera == "TotalMarks" || cabecera == "SrNo")
                    {
                        Label lblnothing = (Label)e.Row.FindControl(cabecera);
                    }
                    else
                    {
                        TextBox lblnothing = (TextBox)e.Row.FindControl(cabecera);
                        TableCell Tc = grdpivot.HeaderRow.Cells[i];
                        string sssb = Tc.Text;
                        e.Row.Cells[i].Attributes.Clear();
                        lblnothing.Attributes.Add("onchange", "callFunctions('" + lblnothing.ClientID + "')");
                        lblnothing.Attributes.Add("runat", "server");
                        if (lblnothing.ClientID.Contains("ATT"))
                        {
                            //comment by Nandu on 01-Sep-2020
                            lblnothing.Enabled = false;
                        }

                        if (enabledisable.Value == "False")
                        {
                            btnview.Visible = true;
                            lblnothing.Enabled = false;
                            btnPivotSave.Visible = false;
                            btnPivotSubmit.Visible = false;
                        }
                        else
                        {
                            btnview.Visible = false;
                            btnPivotSave.Visible = true;
                            btnPivotSubmit.Visible = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
    }

    public string SavePivotDatarowwise(int status, DataTable dtpivot)
    {
        Faculty_MarksEntry obj = new Faculty_MarksEntry();
        string returnResult = "";
        DataTable dt = (DataTable)obj.Session["dtMarkEntryForFaculty"];
        string result;
        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        {

            try
            {



                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string id = "";
                        SqlCommand cmd = new SqlCommand("sp_InsertInternalMarksEntryAndAttendanceHeader1", con);//Insert_proc_MarksEntryHeader1
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CollegeCode", obj.Session["GlobalDimension1Code"].ToString());//Session["GlobalDimension1Code"]
                        cmd.Parameters.AddWithValue("@AcadmicYear", dt.Rows[i]["academicyear"].ToString());
                        cmd.Parameters.AddWithValue("@facultyCode", obj.Session["uid"].ToString());
                        cmd.Parameters.AddWithValue("@CourseCode", dt.Rows[i]["Course Code"].ToString());
                        cmd.Parameters.AddWithValue("@SemesterYear", dt.Rows[i]["Semester"].ToString());
                        cmd.Parameters.AddWithValue("@SubjectCode", dt.Rows[i]["Subject Code"].ToString());
                        cmd.Parameters.AddWithValue("@SubjectType", dt.Rows[i]["Subject Type"].ToString());
                        cmd.Parameters.AddWithValue("@ExamCriteria", dt.Rows[i]["Group Document No_"].ToString().Trim());
                        if (dt.Rows[i]["Exam Type"].ToString() == "2" && Session["SubjectClassification"].ToString().ToUpper() != "THEORY" && status == 2)
                        {
                            cmd.Parameters.AddWithValue("@Status", 6);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Status", status);
                        }


                        cmd.Parameters.AddWithValue("@Method", dt.Rows[i]["ExamMethod"].ToString());
                        cmd.Parameters.AddWithValue("@Group", dt.Rows[i]["ExamGroup"].ToString());
                        cmd.Parameters.AddWithValue("@MaxMarks", Convert.ToDecimal(dt.Rows[i]["MaxMarks"].ToString()));
                        cmd.Parameters.AddWithValue("@ExamType", dt.Rows[i]["Exam Type"].ToString());
                        cmd.Parameters.AddWithValue("@Section", dt.Rows[i]["section"].ToString());

                        if (status == 1)
                        {
                            cmd.Parameters.AddWithValue("@AwardListStatus", 0);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@AwardListStatus", 1);
                        }
                        cmd.Parameters.AddWithValue("@StudentGroup", dt.Rows[i]["StudentGroup"].ToString());
                        if (con.State == ConnectionState.Open)
                        { con.Close(); }
                        con.Open();

                        cmd.ExecuteNonQuery();
                        con.Close();

                        if (dtpivot.Rows.Count > 0)
                        {
                            for (int k = 0; k < dtpivot.Rows.Count; k++)
                            {
                                try
                                {

                                    string txtmarks = dtpivot.Rows[k][dt.Rows[i]["ExamMethod"].ToString()].ToString();
                                    id = dtpivot.Rows[k]["Student_No"].ToString();
                                    SqlCommand cmd1 = new SqlCommand("sp_InsertInternalMarksEntryAndAttendanceLine2", con);//Insert_proc_MarksEntryLine1
                                    cmd1.CommandType = CommandType.StoredProcedure;
                                    cmd1.Parameters.AddWithValue("@CollegeCode", obj.Session["GlobalDimension1Code"].ToString());
                                    cmd1.Parameters.AddWithValue("@AcadmicYear", dt.Rows[i]["academicyear"].ToString());


                                    if (txtmarks.ToUpper().Trim() != "AB" && txtmarks.ToUpper().Trim() != "DT" && txtmarks.ToUpper().Trim() != "UFM")
                                    {
                                        if (txtmarks != "")
                                        {

                                            cmd1.Parameters.AddWithValue("@InernalMark", Convert.ToDecimal(txtmarks));
                                            cmd1.Parameters.AddWithValue("@AttendanceType", "1");
                                        }
                                        else
                                        {
                                            cmd1.Parameters.AddWithValue("@InernalMark", Convert.ToDecimal("-1"));
                                            cmd1.Parameters.AddWithValue("@AttendanceType", "1");
                                        }
                                    }
                                    else
                                    {
                                        if (txtmarks.ToUpper().Trim() == "AB")
                                        {
                                            cmd1.Parameters.AddWithValue("@AttendanceType", "2");
                                            cmd1.Parameters.AddWithValue("@InernalMark", Convert.ToDecimal("-1"));
                                        }
                                        if (txtmarks.ToUpper().Trim() == "DT")
                                        {
                                            cmd1.Parameters.AddWithValue("@AttendanceType", "3");
                                            cmd1.Parameters.AddWithValue("@InernalMark", Convert.ToDecimal("-1"));
                                        }
                                        if (txtmarks.ToUpper().Trim() == "UFM")
                                        {
                                            cmd1.Parameters.AddWithValue("@AttendanceType", "4");
                                            cmd1.Parameters.AddWithValue("@InernalMark", Convert.ToDecimal("-1"));
                                        }
                                    }
                                    cmd1.Parameters.AddWithValue("@StudentNo", id);
                                    cmd1.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
                                    if (dt.Rows[i]["Exam Type"].ToString() == "2" && Session["SubjectClassification"].ToString().ToUpper() != "THEORY" && status == 2)
                                    {
                                        cmd1.Parameters.AddWithValue("@Status", 6);
                                    }
                                    else
                                    {
                                        cmd1.Parameters.AddWithValue("@Status", status);
                                    }
                                    cmd1.Parameters.AddWithValue("@CourseCode", dt.Rows[i]["Course Code"].ToString());
                                    cmd1.Parameters.AddWithValue("@SemesterYear", dt.Rows[i]["Semester"].ToString());
                                    cmd1.Parameters.AddWithValue("@ExamType", dt.Rows[i]["Exam Type"].ToString());
                                    cmd1.Parameters.AddWithValue("@Section", dt.Rows[i]["section"].ToString());
                                    cmd1.Parameters.AddWithValue("@SubjectCode", dt.Rows[i]["Subject Code"].ToString());
                                    cmd1.Parameters.AddWithValue("@SubjectType", dt.Rows[i]["Subject Type"].ToString());
                                    cmd1.Parameters.AddWithValue("@ExamCriteria", dt.Rows[i]["Group Document No_"].ToString().Trim());
                                    cmd1.Parameters.AddWithValue("@Method", dt.Rows[i]["ExamMethod"].ToString());
                                    cmd1.Parameters.AddWithValue("@Group", dt.Rows[i]["ExamGroup"].ToString());
                                    if (status == 1)
                                    {
                                        cmd1.Parameters.AddWithValue("@AwardListStatus", 0);
                                    }
                                    else
                                    {
                                        cmd1.Parameters.AddWithValue("@AwardListStatus", 1);
                                    }
                                    cmd1.Parameters.AddWithValue("@StudentGroup", dt.Rows[i]["StudentGroup"].ToString());
                                    con.Open();
                                    cmd1.ExecuteNonQuery();
                                    //Update Student weitghage Marks

                                    con.Close();

                                }
                                catch (Exception ex)
                                {
                                }
                            }

                        }

                    }
                    scope.Complete();
                    returnResult = "Success";
                }
            }
            catch (Exception ex)
            {
                scope.Dispose();
                return "Error";
            }
        }
        if (status == 2)
        {
            for (int k = 0; k < dtpivot.Rows.Count; k++)
            {
                string id = "";
                for (int l = 0; l < dt.Rows.Count; l++)
                {
                    id = dtpivot.Rows[k]["Student_No"].ToString();
                    SqlCommand cmdDocument = new SqlCommand("GetDocumentNo", con);//sp_GetMarkEntryForFaculty //sp_GetMarkEntryForFaculty_Calendar //comment on 02-05-2018

                    cmdDocument.CommandType = CommandType.StoredProcedure;
                    cmdDocument.Parameters.AddWithValue("@AcadmicYear", dt.Rows[l]["academicyear"].ToString());
                    cmdDocument.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                    cmdDocument.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
                    cmdDocument.Parameters.AddWithValue("@CourseCode", dt.Rows[l]["Course Code"].ToString());
                    cmdDocument.Parameters.AddWithValue("@Semester", dt.Rows[l]["Semester"].ToString());
                    cmdDocument.Parameters.AddWithValue("@Exammethod", dt.Rows[l]["ExamMethod"].ToString());
                    cmdDocument.Parameters.AddWithValue("@SubjectCode", dt.Rows[l]["Subject Code"].ToString());
                    SqlDataAdapter da = new SqlDataAdapter(cmdDocument);
                    DataTable datadoc = new DataTable();
                    if (con.State == ConnectionState.Open) { con.Close(); }
                    con.Open();
                    da.Fill(datadoc);

                    if (datadoc.Rows.Count > 0)
                    {

                        DataTable dtNAV = new DataTable();
                        SqlCommand cmdNAV = new SqlCommand("Proc_GetNAVCreditionalLive", con);
                        cmdNAV.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter daNAV = new SqlDataAdapter(cmdNAV);
                        daNAV.Fill(dtNAV);
                        VoucherPosting nvp = new VoucherPosting();
                        nvp.UseDefaultCredentials = true;
                        nvp.Url = dtNAV.Rows[0]["URL"].ToString();
                        nvp.Credentials = new NetworkCredential(dtNAV.Rows[0]["UserID"].ToString(), dtNAV.Rows[0]["Password"].ToString());
                        result = nvp.InternalTotalMarks(datadoc.Rows[0]["Subject Exam Craiteria"].ToString().Trim(), obj.Session["GlobalDimension1Code"].ToString(), dt.Rows[l]["academicyear"].ToString(), dt.Rows[l]["Subject Code"].ToString(), dt.Rows[l]["Course Code"].ToString(), id);


                    }
                }
            }
        }

        return returnResult;


    }

    //public void getmethodcountOpen()
    //{
    //    SqlCommand cmd2 = new SqlCommand("Pro_GetMethodCountOpen", con);
    //    cmd2.CommandType = CommandType.StoredProcedure;
    //    cmd2.Parameters.AddWithValue("@Acadyear", drpAcademicYear.SelectedValue);

    //    cmd2.Parameters.AddWithValue("@Subject", ddlSubject1.SelectedValue);

    //    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
    //    DataTable dt2 = new DataTable();
    //    con.Open();
    //    da2.Fill(dt2);
    //    if (dt2.Rows.Count > 0)
    //    {
    //        if (Convert.ToInt32(dt2.Rows[0]["ExamCountinHeader"]) == Convert.ToInt32(dt2.Rows[0]["ExamCountinExGroup"]))
    //        {
    //            try
    //            {
    //                string result = "";
    //                foreach (GridViewRow row in grdViewmarksEntry.Rows)
    //                {
    //                    string documentno = hf_ExamCriteria.Value;
    //                    string collegeCode = Session["GlobalDimension1Code"].ToString();
    //                    string academicyear = drpAcademicYear.SelectedValue;
    //                    string subjectcode = ddlSubject1.SelectedValue;
    //                    string coursecode = drpCourse.SelectedValue;
    //                    HiddenField hfStudentNo = (HiddenField)row.FindControl("hfStudentNo");

    //                    DataTable dtNAV = new DataTable();
    //                    SqlCommand cmdNAV = new SqlCommand("Proc_GetNAVCreditionalLive", con);
    //                    cmdNAV.CommandType = CommandType.StoredProcedure;
    //                    SqlDataAdapter daNAV = new SqlDataAdapter(cmdNAV);
    //                    daNAV.Fill(dtNAV);
    //                    VoucherPosting nvp = new VoucherPosting();
    //                    nvp.UseDefaultCredentials = true;
    //                    nvp.Url = dtNAV.Rows[0]["URL"].ToString();
    //                    nvp.Credentials = new NetworkCredential(dtNAV.Rows[0]["UserID"].ToString(), dtNAV.Rows[0]["Password"].ToString());
    //                    //result = nvp.InternalTotalMarksOE(documentno, academicyear, subjectcode, hfStudentNo.Value);

    //                }
    //                con.Close();
    //                bindGrid1();
    //            }
    //            catch (Exception ex) { }
    //        }
    //        else { con.Close(); }
    //    }
    //    else { con.Close(); }
    //}


    //public void getmethodcount()
    //{
    //    SqlCommand cmd2 = new SqlCommand("Pro_GetMethodCount", con);
    //    cmd2.CommandType = CommandType.StoredProcedure;
    //    cmd2.Parameters.AddWithValue("@Acadyear", drpAcademicYear.SelectedValue);
    //    cmd2.Parameters.AddWithValue("@Course", drpCourse.SelectedValue);
    //    cmd2.Parameters.AddWithValue("@Sem", drpSemester.SelectedValue);
    //    cmd2.Parameters.AddWithValue("@Subject", ddlSubject.SelectedValue);

    //    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
    //    DataTable dt2 = new DataTable();
    //    con.Open();
    //    da2.Fill(dt2);
    //    if (dt2.Rows.Count > 0)
    //    {
    //        if (Convert.ToInt32(dt2.Rows[0]["ExamCountinHeader"]) == Convert.ToInt32(dt2.Rows[0]["ExamCountinExGroup"]))
    //        {
    //            try
    //            {
    //                string result = "";
    //                foreach (GridViewRow row in grdViewmarksEntry.Rows)
    //                {
    //                    string documentno = hf_ExamCriteria.Value;
    //                    string collegeCode = Session["GlobalDimension1Code"].ToString();
    //                    string academicyear = drpAcademicYear.SelectedValue;
    //                    string subjectcode = ddlSubject.SelectedValue;
    //                    string coursecode = drpCourse.SelectedValue;
    //                    HiddenField hfStudentNo = (HiddenField)row.FindControl("hfStudentNo");

    //                    DataTable dtNAV = new DataTable();
    //                    SqlCommand cmdNAV = new SqlCommand("Proc_GetNAVCreditionalLive", con);
    //                    cmdNAV.CommandType = CommandType.StoredProcedure;
    //                    SqlDataAdapter daNAV = new SqlDataAdapter(cmdNAV);
    //                    daNAV.Fill(dtNAV);
    //                    VoucherPosting nvp = new VoucherPosting();
    //                    nvp.UseDefaultCredentials = true;
    //                    nvp.Url = dtNAV.Rows[0]["URL"].ToString();
    //                    nvp.Credentials = new NetworkCredential(dtNAV.Rows[0]["UserID"].ToString(), dtNAV.Rows[0]["Password"].ToString());
    //                    result = nvp.InternalTotalMarks(documentno, collegeCode, academicyear, subjectcode, coursecode, hfStudentNo.Value);

    //                }
    //                con.Close();
    //                bindgrid();
    //            }
    //            catch (Exception ex) { }
    //        }
    //        else { con.Close(); }
    //    }
    //    else { con.Close(); }
    //}

    protected void btnPivotSubmit_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Data Summitted Successfully');", true);
        //bindgrid();
        //LblInstruction.Visible = false;
        //VisibleFalseTrue(false, false, true, false);
        grdpivot.Visible = false;
        btnPivotSave.Visible = false;
        btnPivotSubmit.Visible = false;
        LblInstruction.Visible = false;
        lblMaxmark.Visible = false;
        //veer

    }


    protected void btnview_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = sender as LinkButton;
            GridViewRow grow = btn.NamingContainer as GridViewRow;
            //HiddenField hfsubject = (HiddenField)grow.FindControl("hfSubject");
            Session["Subject"] = ddlSubject.SelectedValue;
            Session["AcademicYear"] = drpAcademicYear.SelectedValue;
            Session["drpCourse"] = drpCourse.SelectedValue;
            Session["drpSemester"] = drpSemester.SelectedValue;
            Session["FacultyCode"] = Session["uid"].ToString();
            if (rdInternal.Checked == true)
            {
                Session["examType1"] = 1;
            }
            else
            {
                Session["examType1"] = 2;
            }

            Session["Section"] = drpSection.SelectedValue;
            Session["StudGroup"] = ddlGroup.SelectedValue;

            DataTable dtNAV = new DataTable();
            SqlCommand cmdNAV = new SqlCommand("Proc_GetNAVCreditionalLive", con);
            cmdNAV.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter daNAV = new SqlDataAdapter(cmdNAV);
            daNAV.Fill(dtNAV);
            VoucherPosting nvp = new VoucherPosting();
            nvp.UseDefaultCredentials = true;
            nvp.Url = dtNAV.Rows[0]["URL"].ToString();
            nvp.Credentials = new NetworkCredential(dtNAV.Rows[0]["UserID"].ToString(), dtNAV.Rows[0]["Password"].ToString());

            if (drpSemester.SelectedValue.Contains("YEAR"))
            {
                nvp.UpdateTotalValues_New("NORMAL", drpCourse.SelectedValue, drpAcademicYear.SelectedValue, Session["GlobalDimension1Code"].ToString(), "", drpSemester.SelectedValue, ddlSubject.SelectedValue, "", drpSection.SelectedValue, ddlGroup.SelectedValue);
            }
            else
            {
                nvp.UpdateTotalValues_New("NORMAL", drpCourse.SelectedValue, drpAcademicYear.SelectedValue, Session["GlobalDimension1Code"].ToString(), drpSemester.SelectedValue, "", ddlSubject.SelectedValue, "", drpSection.SelectedValue, ddlGroup.SelectedValue);
            }



            ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "OpenNewWindow1()", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Some thing went wrong,Please try after some time');", true);
            return;
        }
    }
    protected void ddlSubject_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetSubjectClassificationformarkEntry", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@AcadmicYear", drpAcademicYear.SelectedValue);
        cmd.Parameters.AddWithValue("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.AddWithValue("@SubjectCode", ddlSubject.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Session["SubjectClassification"] = dt.Rows[0]["Subject Classification"].ToString();
            if (dt.Rows[0]["Subject Classification"].ToString() == "THEORY" || Session["GlobalDimension1Code"].ToString() == "TMNS" || Session["GlobalDimension1Code"].ToString() == "TMSN")
            {
                onetime.Visible = false;
            }
            else
            {
                onetime.Visible = true;
                chkPiv.Checked = true;
                chkPiv.Enabled = false;
            }
        }
        bindSectionList();

        bindGroupList();


        bindBatchList();
        grdpivot.DataSource = "";
        grdpivot.DataBind();

    }

    protected void rdOpen_CheckedChanged(object sender, EventArgs e)
    {


        if (rdOpen.Checked == true)
        {
            pnlMain.Visible = false;
            pnlOpenElective.Visible = true;
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("proc_GetSubjectFromCourseSubjectLine1", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@AcademicYear", drpAcademicYear1.SelectedValue);
            cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            ddlSubject1.DataSource = dt;
            ddlSubject1.DataTextField = "Details";
            ddlSubject1.DataValueField = "No_";
            ddlSubject1.DataBind();
        }
        else
        {
            pnlMain.Visible = true;
            pnlOpenElective.Visible = false;
        }







    }
    protected void btnShow1_Click(object sender, EventArgs e)
    {
        bindGrid1();
    }
    public void bindGrid1()
    {
        try
        {
            string SP = "";

            SP = "sp_GetMarkEntryForFaculty_Calendar_PNC2_TheoryOpen";

            SqlCommand cmd = new SqlCommand(SP, con);
            cmd.CommandTimeout = 1000;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AcadmicYear", drpAcademicYear1.SelectedValue);

            cmd.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());

            if (ddlSubject1.SelectedIndex > 0)
            { cmd.Parameters.AddWithValue("@Subject", ddlSubject1.SelectedValue); }
            else { cmd.Parameters.AddWithValue("@Subject", ""); }
            //if (SP == "sp_GetMarkEntryForFaculty_Pivot")
            //{
            if (rdInternal.Checked == true)
            {
                cmd.Parameters.AddWithValue("@ExamType1", 1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ExamType1", 2);
            }

            // }

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            if (con.State == ConnectionState.Open) { con.Close(); }
            con.Open();
            da.Fill(ds);
            MarkEntry.Visible = true;
            grdmarktable.Visible = true;
            grdViewmarksEntry.Visible = false;
            grdmarktable.DataSource = ds.Tables[0];
            grdmarktable.DataBind();
            SqlCommand cmd2 = new SqlCommand("Pro_GetMethodCountOpen", con);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@Acadyear", drpAcademicYear.SelectedValue);

            cmd2.Parameters.AddWithValue("@Subject", ddlSubject1.SelectedValue);

            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataTable dt2 = new DataTable();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            da2.Fill(dt2);
            if (dt2.Rows.Count > 0)
            {
                if (Convert.ToInt32(dt2.Rows[0]["ExamCountinHeader"]) == Convert.ToInt32(dt2.Rows[0]["ExamCountinExGroup"]))
                {
                    pnlMain.Visible = false;
                    pnlOpenElective.Visible = true;
                    DataTable dt3 = new DataTable();
                    SqlCommand cmd3 = new SqlCommand("proc_GetProgramOpenElective", con);
                    cmd3.CommandType = CommandType.StoredProcedure;
                    cmd3.Parameters.Add("@FacultyCode", Session["uid"].ToString());
                    cmd3.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
                    SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                    da3.Fill(dt3);
                    drpCourseOpen.DataSource = dt3;
                    drpCourseOpen.DataTextField = "Details";
                    drpCourseOpen.DataValueField = "No_";
                    drpCourseOpen.DataBind();
                    lnkview.Visible = true;
                    drpCourseOpen.Visible = true;
                }
                else
                {
                    lnkview.Visible = false;
                    drpCourseOpen.Visible = false;
                    pnlMain.Visible = false;
                    pnlOpenElective.Visible = true;

                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void lnkview_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = sender as LinkButton;
            GridViewRow grow = btn.NamingContainer as GridViewRow;
            //HiddenField hfsubject = (HiddenField)grow.FindControl("hfSubject");
            Session["Subject"] = ddlSubject1.SelectedValue;
            Session["AcademicYear"] = drpAcademicYear1.SelectedValue;
            Session["drpCourse"] = drpCourseOpen.SelectedValue;
            Session["drpSemester"] = drpSemester.SelectedValue;

            Session["FacultyCode"] = Session["uid"].ToString();
            if (rdInternal.Checked == true)
            {
                Session["examType1"] = 1;

            }
            else
            {
                Session["examType1"] = 2;

            }

            DataTable dtNAV = new DataTable();
            SqlCommand cmdNAV = new SqlCommand("Proc_GetNAVCreditionalLive", con);
            cmdNAV.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter daNAV = new SqlDataAdapter(cmdNAV);
            daNAV.Fill(dtNAV);
            VoucherPosting nvp = new VoucherPosting();
            nvp.UseDefaultCredentials = true;
            nvp.Url = dtNAV.Rows[0]["URL"].ToString();
            nvp.Credentials = new NetworkCredential(dtNAV.Rows[0]["UserID"].ToString(), dtNAV.Rows[0]["Password"].ToString());

            nvp.UpdateTotalValues_New("OPENELEC", "", drpAcademicYear.SelectedValue, "", "", "", ddlSubject.SelectedValue, "", "", "");
            ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "OpenNewWindow1()", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Some thing went wrong,Please try after some time');", true);
            return;
        }
    }
    protected void drpAcademicYear1_TextChanged(object sender, EventArgs e)
    {
        if (rdOpen.Checked == true)
        {
            pnlMain.Visible = false;
            pnlOpenElective.Visible = true;
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("proc_GetSubjectFromCourseSubjectLine1", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@AcademicYear", drpAcademicYear1.SelectedValue);
            cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ddlSubject1.DataSource = dt;
                ddlSubject1.DataTextField = "Details";
                ddlSubject1.DataValueField = "No_";
                ddlSubject1.DataBind();
            }
            else
            {
                ddlSubject1.DataSource = "";

                ddlSubject1.DataBind();
            }
        }
        else
        {
            pnlMain.Visible = true;
            pnlOpenElective.Visible = false;
        }

    }

    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        grdpivot.DataSource = "";
        grdpivot.DataBind();
        bindGroupList();
    }

    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        grdpivot.DataSource = "";
        grdpivot.DataBind();
    }
}