using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;


using System.Globalization;
using OfficeOpenXml;

public partial class Faculty_TimeTableUpload : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    DL.FacultyPortalDL FDL = new DL.FacultyPortalDL();
   
    protected void Page_Load(object sender, EventArgs e)
    {
         try
        {
            if (Session["uid"].ToString() != null)
            {
                if (Session["UserGroup"].ToString() == "FACULTY" || Session["UserGroup"].ToString() == "PRINCIPAL")
                {
                    if (!IsPostBack)
                    {
                      
                    }
                }
                else 
                {
                    Response.Redirect("../Default.aspx");
                }
            }
            else
            {
                Response.Redirect("../Default.aspx");
            }
        }
  catch { 
            Response.Redirect("../Default.aspx"); 
        }
    }   
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        ExcelUpload();      
        
    }
    public void ExcelUpload()// Sandeep
    {
        if (FileUpload1.HasFile)
        {
            if (Path.GetExtension(FileUpload1.FileName) == ".xlsx" || Path.GetExtension(FileUpload1.FileName) == ".xls")
            {
                DataTable dtvs = new DataTable();
               // MemoryStream stream = new MemoryStream();
                ExcelPackage package = new ExcelPackage(FileUpload1.FileContent);
                grdUpload.DataSource = package.ToDataTable();
                grdUpload.DataBind();
                dtvs = package.ToDataTable();
                if (dtvs.Rows.Count > 0)
                { 
                    ValidateData(dtvs);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Please Check Excell Sheeet');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Upload Excell Sheeet Only !');", true);
                return;
            }

        }

    }
    public DataTable GetDataTable(GridView dtg)//Sandeep
    {
        DataTable dt = new DataTable();

        // add the columns to the datatable            
        if (dtg.HeaderRow != null)
        {

            for (int i = 0; i < dtg.HeaderRow.Cells.Count; i++)
            {
                dt.Columns.Add(dtg.HeaderRow.Cells[i].Text);
            }
        }

        //  add each of the data rows to the table
        foreach (GridViewRow row in dtg.Rows)
        {
            DataRow dr;
            dr = dt.NewRow();

            for (int i = 0; i < row.Cells.Count; i++)
            {
                dr[i] = row.Cells[i].Text.Replace(" ", " ");
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable tbl = GetDataTable(grdUpload);
        Save(tbl);
       // hfUploadSave.Value = "SAVE";
        //Import_To_Grid(hfFilePath.Value, hfExtension.Value, rbHDR.SelectedItem.Text);
    }
   
    public void Save(DataTable dt)
    {
     DataTable dtRecord = new DataTable();
     dtRecord = dt;
     if (con.State == ConnectionState.Closed) { con.Open(); }
        for (int i = 0; i < dtRecord.Rows.Count; i++)
        {
            SqlCommand cmd1 = new SqlCommand("proc_GetSubjectDetailsForUploadTimeTable", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@CourseCode", dtRecord.Rows[i]["Course Code"].ToString());
            cmd1.Parameters.Add("@Subject", dtRecord.Rows[i]["Subject Code"].ToString());
            cmd1.Parameters.Add("@AcademicYear", dtRecord.Rows[i]["Academic Year"].ToString());
           // if (con.State == ConnectionState.Closed) { con.Open(); }
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            cmd1.ExecuteNonQuery();
           // con.Close();
            SqlCommand cmd = new SqlCommand("proc_CreateTimeTableGeneration", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 130;
            cmd.Parameters.Add("@DayNo", dtRecord.Rows[i]["Day No"].ToString());
            cmd.Parameters.Add("@CourseCode", dtRecord.Rows[i]["Course Code"].ToString());
            cmd.Parameters.Add("@SemesterYear", dtRecord.Rows[i]["Semester/Year"].ToString());
            cmd.Parameters.Add("@HouNo", dtRecord.Rows[i]["From Hour No"].ToString());
            cmd.Parameters.Add("@SubjectCode", dtRecord.Rows[i]["Subject Code"].ToString());
            cmd.Parameters.Add("@FacultyCode", dtRecord.Rows[i]["Faculty Code"].ToString());//Session["uid"].ToString());
            cmd.Parameters.Add("@AcademicYear", dtRecord.Rows[i]["Academic Year"].ToString());
            if (dtRecord.Rows[i]["Section"].ToString() == "&nbsp;")
            {
                cmd.Parameters.Add("@Section", "");
            }
            else
            {
                cmd.Parameters.Add("@Section", dtRecord.Rows[i]["Section"].ToString());
            }
            cmd.Parameters.Add("@SubjectType", dt1.Rows[0]["Subject Type"].ToString());
            cmd.Parameters.Add("@FacultyName", Session["Fulname"].ToString());
            cmd.Parameters.Add("@SubjectDescription", dt1.Rows[0]["Description"].ToString());
            cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            cmd.Parameters.Add("@RoomAllocation", dtRecord.Rows[i]["Room No"].ToString());
            cmd.Parameters.Add("@SubjectClassification", dt1.Rows[0]["Subject Classification"].ToString());
            cmd.Parameters.Add("@FromDate", Convert.ToDateTime(dtRecord.Rows[i]["From Date"].ToString()).ToString("dd/MMM/yyyy"));
            cmd.Parameters.Add("@ToDate", Convert.ToDateTime( dtRecord.Rows[i]["To Date"].ToString()).ToString("dd/MMM/yyyy"));
            if(dtRecord.Rows[i]["Group"].ToString()=="&nbsp;")
            { 
                cmd.Parameters.Add("@Group", ""); 
            }
            else
            {
            cmd.Parameters.Add("@Group", dtRecord.Rows[i]["Group"].ToString());
            }
            if (dtRecord.Rows[i]["Batch"].ToString() == "&nbsp;")
            {
                cmd.Parameters.Add("@Batch", "");
            }
            else
            {
                cmd.Parameters.Add("@Batch", dtRecord.Rows[i]["Batch"].ToString());
            }            
            cmd.Parameters.Add("@HouNoTo", dtRecord.Rows[i]["To Hour No"].ToString());
          //  if (con.State == ConnectionState.Closed)
           //     con.Open();
            int a=cmd.ExecuteNonQuery();
           // con.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Save Successfully');", true);
            grdUpload.DataSource = null;
            grdUpload.DataBind();
            btnSave.Visible = false;
           
        }
        if (con.State == ConnectionState.Open) { con.Close(); }
    }
    public void ValidateData(DataTable dtRecord )
    {
        String Error = "";

        if (dtRecord.Columns.Count != 15)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Check Your Excel Sheet Format !');", true);
            //Error = "Error";
            return;
        }

        if (dtRecord.Columns[0].ColumnName == "Course Code" && dtRecord.Columns[1].ColumnName == "Subject Code" && dtRecord.Columns[2].ColumnName == "Classification" && dtRecord.Columns[3].ColumnName == "Semester/Year" && dtRecord.Columns[4].ColumnName == "Day No" && dtRecord.Columns[5].ColumnName == "From Date" && dtRecord.Columns[6].ColumnName == "To Date" && dtRecord.Columns[7].ColumnName == "From Hour No" && dtRecord.Columns[8].ColumnName == "To Hour No" && dtRecord.Columns[9].ColumnName == "Room No" && dtRecord.Columns[10].ColumnName == "Section" && dtRecord.Columns[11].ColumnName == "Group" && dtRecord.Columns[12].ColumnName == "Batch" && dtRecord.Columns[13].ColumnName == "Academic Year" && dtRecord.Columns[14].ColumnName == "Faculty Code")
        {

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Check Your Excel Sheet Format !');", true);
            Error = "Error";
            return;
        }
        grdUpload.DataSource = dtRecord;    grdUpload.DataBind();
        DataTable dtValidate=new DataTable();                       dtValidate = dtRecord;        
       for(int i=0;i<dtRecord.Rows.Count;i++)  
        {

            for (int j = 0; j < dtRecord.Columns.Count; j++)
            {
                if ((dtRecord.Rows[i]["Course Code"].ToString() == "" && dtRecord.Columns[j].ColumnName == "Course Code") || (dtRecord.Rows[i]["Subject Code"].ToString() == "" && dtRecord.Columns[j].ColumnName == "Subject Code") || (dtRecord.Rows[i]["Classification"].ToString() == "" && dtRecord.Columns[j].ColumnName == "Classification") || (dtRecord.Rows[i]["Semester/Year"].ToString() == "" && dtRecord.Columns[j].ColumnName == "Semester/Year") || (dtRecord.Rows[i]["Day No"].ToString() == "" && dtRecord.Columns[j].ColumnName == "Day No") || (dtRecord.Rows[i]["From Date"].ToString() == "" && dtRecord.Columns[j].ColumnName == "From Date") || (dtRecord.Rows[i]["To Date"].ToString() == "" && dtRecord.Columns[j].ColumnName == "To Date") || (dtRecord.Rows[i]["From Hour No"].ToString() == "" && dtRecord.Columns[j].ColumnName == "From Hour No") || (dtRecord.Rows[i]["To Hour No"].ToString() == "" && dtRecord.Columns[j].ColumnName == "To Hour No") || (dtRecord.Rows[i]["Room No"].ToString() == "" && dtRecord.Columns[j].ColumnName == "Room No") || (dtRecord.Rows[i]["Academic Year"].ToString() == "" && dtRecord.Columns[j].ColumnName == "Academic Year") || (dtRecord.Rows[i]["Faculty Code"].ToString() == "" && dtRecord.Columns[j].ColumnName == "Faculty Code"))
                {
                   // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Check Your Data !');", true);
                    Error = "Error";
                    grdUpload.Rows[i].Cells[j].BackColor = System.Drawing.Color.Yellow;//column no
                   // return;
                }
                else
                {
                    SqlCommand cmdClassification = new SqlCommand("Sp_ValidateFacultyForTimeTableUpload", con);
                    cmdClassification.CommandType = CommandType.StoredProcedure;

                    cmdClassification.Parameters.Add("@FacultyCode", dtRecord.Rows[i]["Faculty Code"].ToString());
                    cmdClassification.Parameters.Add("@UserId", Session["uid"].ToString());
                    cmdClassification.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                    cmdClassification.Parameters.Add("@CourseCode", dtRecord.Rows[i]["Course Code"].ToString());
                    cmdClassification.Parameters.Add("@SubjectCode", dtRecord.Rows[i]["Subject Code"].ToString());
                    cmdClassification.Parameters.Add("@SemesterYear", dtRecord.Rows[i]["Semester/Year"].ToString());
                    
                    
                    con.Open();
                    SqlDataReader dr = cmdClassification.ExecuteReader();
                    dr.Read();
                    string str=dr["Result"].ToString();
                    if (str == "Success")
                    {
                        con.Close();
                    }
                    else
                    {
                        con.Close();
                        Error = "Error";
                        grdUpload.Rows[i].Cells[j].BackColor = System.Drawing.Color.Yellow;//column no
                        //return;
                    }
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    //old code strat
                    
                  /*  
                    string str1 = dtRecord.Rows[i]["Faculty Code"].ToString();
                    if (Session["uid"].ToString() != dtRecord.Rows[i]["Faculty Code"].ToString()) // check for faculty uplaod
                    {

                        if ((dtRecord.Rows[i]["Faculty Code"].ToString() != "" && dtRecord.Columns[j].ColumnName == "Faculty Code"))
                        {
                            string str = dtRecord.Rows[i]["Faculty Code"].ToString();
                            SqlCommand cmdClassification = new SqlCommand("Sp_ChkHOD", con);
                            cmdClassification.CommandType = CommandType.StoredProcedure;
                            cmdClassification.Parameters.Add("@ID", dtRecord.Rows[i]["Faculty Code"].ToString());
                            con.Open();
                            SqlDataReader dr = cmdClassification.ExecuteReader();
                            dr.Read();
                            if (Session["uid"].ToString() == dr["HOD"].ToString())
                            {
                                con.Close();
                            }
                            else
                            {
                                con.Close();
                                Error = "Error";
                                grdUpload.Rows[i].Cells[j].BackColor = System.Drawing.Color.Yellow;//column no
                                return;
                            }
                        }
                    }
                    */
                    //old code end















                }
            }
            int HourNo=0; int HourNoTo=0;
            try
            {
                HourNo= Convert.ToInt16(dtRecord.Rows[i]["From Hour No"].ToString());
                HourNoTo = Convert.ToInt16(dtRecord.Rows[i]["To Hour No"].ToString());
                if (HourNo > HourNoTo || (HourNo==0 ||  HourNoTo==0) )
                {
                    // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Check Hour No.');", true);
                    Error = "Error";
                    grdUpload.Rows[i].Cells[7].BackColor = System.Drawing.Color.Yellow; ;//column no
                    grdUpload.Rows[i].Cells[8].BackColor = System.Drawing.Color.Yellow; ;//column no
                    // return;
                }
                DateTimeFormatInfo dateFormat = new DateTimeFormatInfo();
                dateFormat.DateSeparator = "/";
                string input1 = dtRecord.Rows[i]["From Date"].ToString();//"18/10/2012";
                string input2 = dtRecord.Rows[i]["To Date"].ToString();// "17/10/2012";
                DateTime date1 = DateTime.MinValue;
                DateTime date2 = DateTime.MinValue;
//                DateTime.Parse(Convert.ToDateTime(dtRecord.Rows[i]["From Date"].ToString()).ToString("yyyy-MM-dd")) <= DateTime.Parse(Convert.ToDateTime(dtRecord.Rows[i]["To Date"].ToString()).ToString("yyyy-MM-dd")))
                if (DateTime.TryParseExact(input1, "dd/MM/yyyy", dateFormat, DateTimeStyles.AllowWhiteSpaces, out date1))
                {
                    // Date 1 is a valid date
                }
                else
                {
                    //throw new Exception("Input 1 is not a valid recognised date");
                    grdUpload.Rows[i].Cells[5].BackColor = System.Drawing.Color.Yellow; ;//column no
                    return;

                }

                if (DateTime.TryParseExact(input2, "dd/MM/yyyy", dateFormat, DateTimeStyles.AllowWhiteSpaces, out date2))
                {
                    // Date 2 is a valid date
                }
                else
                {
                    grdUpload.Rows[i].Cells[6].BackColor = System.Drawing.Color.Yellow; ;//column no
                    //throw new Exception("Input 2 is not a valid recognised date");
                    return;
                }

                if (date1 > date2)
                {
                    // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Check Hour No.');", true);
                    Error = "Error";
                    grdUpload.Rows[i].Cells[5].BackColor = System.Drawing.Color.Yellow; ;//column no
                    grdUpload.Rows[i].Cells[6].BackColor = System.Drawing.Color.Yellow; ;//column no
                    // return;
                }
            }
            catch (Exception e)
            {
                
                grdUpload.Rows[i].Cells[7].BackColor = System.Drawing.Color.Yellow; ;//column no
            }
            DataTable dtClassification = new DataTable();
            if (Error != "Error")
            {
                SqlCommand cmdClassification = new SqlCommand("proc_GetSubjectDetailsForUploadTimeTable", con);
                cmdClassification.CommandType = CommandType.StoredProcedure;
                cmdClassification.Parameters.Add("@CourseCode", dtRecord.Rows[i]["Course Code"].ToString());
                cmdClassification.Parameters.Add("@Subject", dtRecord.Rows[i]["Subject Code"].ToString());
                cmdClassification.Parameters.Add("@AcademicYear", dtRecord.Rows[i]["Academic Year"].ToString());
                if (con.State == ConnectionState.Closed) { con.Open(); }
                SqlDataAdapter daClassification = new SqlDataAdapter(cmdClassification);
                daClassification.Fill(dtClassification);
                cmdClassification.ExecuteNonQuery();
                con.Close();
            
            
                if (dtClassification.Rows.Count > 0)
                {
                    if (dtClassification.Rows[0]["Subject Classification"].ToString() != dtRecord.Rows[i]["Classification"].ToString())
                    {
                        //  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Check Classification !');", true);
                        grdUpload.Rows[i].BackColor = System.Drawing.Color.DarkSalmon;
                        Error = "Error";
                        //  return;
                    }
                }
                else
                {

                    // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Check Course/Subject/Classification !');", true);
                    grdUpload.Rows[i].BackColor = System.Drawing.Color.DarkSalmon;
                    Error = "Error";
                    // return;
                }
            }
            if (Error != "Error")
            {
                SqlCommand cmdCL = new SqlCommand("proc_GetSubjectClassification", con);
                cmdCL.CommandType = CommandType.StoredProcedure;
                cmdCL.Parameters.Add("@CourseCode", dtRecord.Rows[i]["Course Code"].ToString());
                cmdCL.Parameters.Add("@Subject", dtRecord.Rows[i]["Subject Code"].ToString());
                if (con.State == ConnectionState.Closed) { con.Open(); }
                SqlDataAdapter daCL = new SqlDataAdapter(cmdCL);
                DataTable dtCL = new DataTable();
                daCL.Fill(dtCL);
                int Theory_count = 0;
                Theory_count = Convert.ToInt16(cmdCL.ExecuteScalar().ToString());
                con.Close();

                SqlCommand cmd1 = new SqlCommand("proc_CheckTimeSheetAvailabilityForExcelUpload", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@CourseCode", dtRecord.Rows[i]["Course Code"].ToString());
                cmd1.Parameters.Add("@SemesterYear", dtRecord.Rows[i]["Semester/Year"].ToString());
                cmd1.Parameters.Add("@Section", dtRecord.Rows[i]["Section"].ToString());
                cmd1.Parameters.Add("@DayNo", dtRecord.Rows[i]["Day No"].ToString());
                cmd1.Parameters.Add("@HourNo", HourNo);
                cmd1.Parameters.Add("@AcademicYear", dtRecord.Rows[i]["Academic Year"].ToString());
                cmd1.Parameters.Add("@DateFrom", dtRecord.Rows[i]["From Date"].ToString());
                cmd1.Parameters.Add("@DateTo",dtRecord.Rows[i]["To Date"].ToString());
                cmd1.Parameters.Add("@Group", dtRecord.Rows[i]["Group"].ToString());
                cmd1.Parameters.Add("@Batch", dtRecord.Rows[i]["Batch"].ToString());
                cmd1.Parameters.Add("@Subject", dtRecord.Rows[i]["Subject Code"].ToString());
                cmd1.Parameters.Add("@FacultyCode", dtRecord.Rows[i]["Faculty Code"].ToString());//Session["uid"].ToString());
                cmd1.Parameters.Add("@HourNoTo", HourNoTo);

                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                for (int a = 1; a <= dt.Rows.Count; a++)
                {
                    if (dt.Rows[a - 1]["Faculty Code"].ToString() == dtRecord.Rows[i]["Faculty Code"].ToString() && Theory_count == 0)//Session["uid"].ToString() && Theory_count == 0)
                    {
                        //  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'This lecture has been already assigned');", true);
                        grdUpload.Rows[i].BackColor = System.Drawing.Color.DarkSalmon;
                        Error = "Error";
                        //    return;
                    }
                }
                if (dt.Rows.Count > 0 && Theory_count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'This lecture has been already assigned');", true);
                    grdUpload.Rows[i].BackColor = System.Drawing.Color.DarkSalmon;
                    Error = "Error";

                }
            }
       }
       if (Error == "")
       {
           ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Validation is OK !');", true);
           btnSave.Visible = true;
       }
       else
       {
           ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Please check your subject master data ! ');", true);
           btnSave.Visible = false;
       }
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {

    }

    //protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
    //    string FileName = grdUpload.Caption;
    //    string Extension = Path.GetExtension(FileName);
    //    string FilePath = Server.MapPath(FolderPath + FileName);
    //    Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text);
    //    grdUpload.PageIndex = e.NewPageIndex;
    //    grdUpload.DataBind();

    //}
    //public void GarbageExcelCode()
    //{
    //    //DeleteFile();
    //    grdUpload.DataSource = null;
    //    grdUpload.DataBind();
    //    hfUploadSave.Value = "UPLOAD";
    //    if (FileUpload1.HasFile)
    //    {
    //        string FileName = Path.GetFileName(FileUpload1.FileName);
    //        FileName = Session["uid"].ToString() + FileName;
    //        //----------------comment-----start--------

    //        string folderName = @"C:\fileupload\Event\";
    //        string pathString = "";
    //        pathString = System.IO.Path.Combine(folderName, Label1.Text);
    //        System.IO.Directory.CreateDirectory(pathString);
    //        pathString = System.IO.Path.Combine(pathString, FileName);
    //        hfFilePath.Value = pathString;
    //        FileUpload1.SaveAs(pathString);
    //        //---------------- comment end------------           
    //        hfExtension.Value = Path.GetExtension(FileUpload1.PostedFile.FileName);
    //        // string FolderPath =ConfigurationManager.AppSettings["FolderPath"];
    //        //hfFilePath.Value = Server.MapPath(FolderPath + FileName);
    //        //FileUpload1.SaveAs(hfFilePath.Value);
    //        Import_To_Grid(hfFilePath.Value, hfExtension.Value, rbHDR.SelectedItem.Text);

    //    }

    //}

    //public void UploadExcel()
    //{
    //    //try
    //    //{

    //    //  lblmsg.Text = "";
    //    string ConStr = "";
    //    string ext = Path.GetExtension(FileUpload1.FileName).ToLower();
    //    string path = Server.MapPath(FileUpload1.PostedFile.FileName);
    //    if ((FileUpload1.FileName == "") || (FileUpload1.FileName == null))
    //    {
    //        // lblmsg.Text = "..Please Choose Excel file to Upload";
    //        return;
    //    }
    //    else
    //    {
    //        FileUpload1.SaveAs(path);
    //        grdUpload.Caption = FileUpload1.FileName + "\'s Data showing into the GridView";
    //        if (ext.Trim() == ".xls")
    //        {
    //            ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
    //        }
    //        else if (ext.Trim() == ".xlsx")
    //        {
    //            ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
    //        }

    //        OleDbConnection connExcel = new OleDbConnection(ConStr);
    //        Label1.Text = "excel ok";
    //        connExcel.Open();
    //        Label1.Text = "excel connnection open";
    //        DataTable dtExcelSchema;
    //        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
    //        string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
    //        connExcel.Close();
    //        string query = "SELECT * FROM [" + SheetName + "]";
    //        OleDbConnection conn = new OleDbConnection(ConStr);
    //        if (conn.State == ConnectionState.Closed)
    //        {
    //            conn.Open();
    //        }
    //        OleDbCommand cmd = new OleDbCommand(query, conn);
    //        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        grdUpload.DataSource = ds.Tables[0];
    //        grdUpload.DataBind();
    //        conn.Close();

    //    }
    //    //}
    //    //catch{
    //    //    //Label1.Text = "error";
    //    //}
    //}

    //private void Import_To_Grid(string FilePath, string Extension, string isHDR)
    //{
    //    string conStr = "";
    //    switch (Extension)
    //    {
    //        case ".xls": //Excel 97-03
    //            conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
    //            break;
    //        case ".xlsx": //Excel 07
    //            conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
    //            break;
    //    }
    //    if (conStr == "")
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Upload Excell Sheeet Only !');", true);
    //        return;
    //    }
    //    conStr = String.Format(conStr, FilePath, isHDR);

    //    OleDbConnection connExcel = new OleDbConnection(conStr);

    //    OleDbCommand cmdExcel = new OleDbCommand();
    //    OleDbDataAdapter oda = new OleDbDataAdapter();
    //    DataTable dt = new DataTable();
    //    cmdExcel.Connection = connExcel;
    //    Label1.Text = connExcel.ConnectionString; //return;
    //    //Get the name of First Sheet
    //    connExcel.Open();
    //    DataTable dtExcelSchema;
    //    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
    //    string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
    //    connExcel.Close();
    //    //Read Data from First Sheet
    //    connExcel.Open();
    //    cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
    //    oda.SelectCommand = cmdExcel;
    //    oda.Fill(dt);
    //    connExcel.Close();
    //    //Bind Data to GridView
    //    if (hfUploadSave.Value == "UPLOAD")
    //    {
    //        grdUpload.Caption = Path.GetFileName(FilePath);
    //        if (dt.Rows.Count > 0)
    //            ValidateData(dt);
    //        else
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Please Check Excell Sheeet');", true);
    //    }
    //    else
    //    {
    //        Save(dt);
    //    }
    //}
    //public void DeleteFile()
    //{
    //    string sourceDir = Server.MapPath("../Files/");
    //    string backupDir = @"c:\fileupload\archives\";
    //    try
    //    {
    //        string[] picList = Directory.GetFiles(sourceDir, Session["uid"].ToString() + "*.xlsx");
    //        string[] txtList = Directory.GetFiles(sourceDir, "*.txt");
    //        // Copy picture files.
    //        foreach (string f in picList)
    //        {
    //            // Remove path from the file name.
    //            string fName = f.Substring(sourceDir.Length);

    //            // Use the Path.Combine method to safely append the file name to the path.
    //            // Will overwrite if the destination file already exists.
    //            File.Copy(Path.Combine(sourceDir, fName), Path.Combine(backupDir, fName), true);
    //        }
    //        foreach (string f in picList)
    //        {
    //            File.Delete(f);
    //        }

    //    }
    //    catch { }
    //}
}