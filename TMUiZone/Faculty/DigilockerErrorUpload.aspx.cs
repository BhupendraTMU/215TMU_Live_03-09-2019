using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class Faculty_DigilockerErrorUpload : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string uid = Session["uid"].ToString();

            if (uid == "TMU09294" || uid == "TMU09412" || uid == "TMU00265")
            {
                btnPost.Visible = true;
                
            }
            else if (uid == "TMU00140")
            {
                btnPosted.Visible = true;
                btnCollectionReport.Visible = true;
                btnPreview.Visible = false;
                FileUpload1.Visible = false;
                tdExcel.Visible = false;
                btnClear.Visible = false;
                BindUploadedData();
                pnlReport.Visible = true;
                btnReport.Text = "Pending Data For Post";
                btnReport.CssClass = "btn btnPost btnSelected";
                btnPosted.CssClass = "btn btnPost";
                btnCollectionReport.CssClass = "btn btnPost";

               

            }
        }
    }

    
    protected void btnReport_Click(object sender, EventArgs e)
    {
        if(Session["uid"].ToString()!="TMU00140")
        {
            btnFPost.Visible = false;
            gvData.Columns[7].Visible = false;
            gvData.Columns[0].Visible = false;
        }
        
        

        btnReport.CssClass = "btn btnPost btnSelected";
        btnPosted.CssClass = "btn btnPost";
        btnCollectionReport.CssClass = "btn btnPost";
        pnlReport.Visible = true;
        BindUploadedData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if(btnPosted.CssClass == "btn btnPost btnSelected")
        {
            SearchDataIsPost();
        }
        else
        {
            SearchData();
        }
    }

    protected void btnShowAll_Click(object sender, EventArgs e)
    {
        txtEnrollmentNo.Text = "";

        BindUploadedData();

    }

    private void SearchData()
    {
        try
        {
            string sql = "";
            if (Session["uid"].ToString() == "TMU00140")
            {
                sql = @"SELECT *
                               FROM StudentABCErrorData
                               WHERE (REGN_NO=@REGN_NO or collegeCode=@REGN_NO)
and ispost=0
                               ORDER BY ID DESC";
            }
            else
            {

                sql = @"SELECT *
                               FROM StudentABCErrorData
                               WHERE (REGN_NO=@REGN_NO or collegeCode=@REGN_NO) and  UploadBy = @UploadBy
                               ORDER BY ID DESC";
            }

            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@REGN_NO",
                txtEnrollmentNo.Text.Trim());
            cmd.Parameters.AddWithValue("@UploadBy", Session["uid"].ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            gvData.DataSource = dt;
            gvData.DataBind();

            lblTotalRecords.Text =
                "Total Records : " + dt.Rows.Count;

        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    private void BindUploadedData()
    {
        try
        {
            string sql = "";
            if (Session["uid"].ToString() == "TMU00140")
            {
                if (btnPosted.CssClass == "btn btnPost btnSelected")
                {
                    sql = @"SELECT *
               FROM StudentABCErrorData
               where ispost=1
               ORDER BY ID DESC";
                }
                else
                {

                    sql = @"SELECT *
               FROM StudentABCErrorData
               where ispost=0
               ORDER BY ID DESC";
                }
            }
            else
            {
                sql = @"SELECT *
               FROM StudentABCErrorData
               WHERE UploadBy = @UploadBy
               ORDER BY ID DESC";
            }

            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.SelectCommand.Parameters.AddWithValue("@UploadBy", Session["uid"].ToString());

            DataTable dt = new DataTable();
            da.Fill(dt);
            gvData.DataSource = dt;
            gvData.DataBind();

            lblTotalRecords.Text =
                "Total Records : " + dt.Rows.Count;
        }

        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    protected void btnPreview_Click(object sender, EventArgs e)
    {


        try
        {
            gvData.Columns[7].Visible = false;
            gvData.Columns[0].Visible = false;

            if (FileUpload1.PostedFiles.Count == 0)
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Please select CSV file(s).";
                return;
            }

            string[] requiredColumns =
            {
            "INVALID_COLUMNS",
            "ORG_NAME",
            "ACADEMIC_COURSE_ID",
            "COURSE_NAME",
            "STREAM",
            "SESSION",
            "REGN_NO",
            "RROLL",
            "CNAME",
            "GENDER",
            "DOB",
            "FNAME",
            "MNAME"
        };

            DataTable dtFinal = CreateFinalTable();

            foreach (HttpPostedFile file in FileUpload1.PostedFiles)
            {
                string ext = Path.GetExtension(file.FileName).ToLower();

                if (ext != ".csv")
                    continue;

                DataTable dtvs = new DataTable();

                using (StreamReader sr = new StreamReader(file.InputStream))
                {
                    bool headerRow = true;

                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();

                        if (string.IsNullOrWhiteSpace(line))
                            continue;

                        List<string> values = new List<string>();
                        bool inQuotes = false;
                        string current = "";

                        foreach (char c in line)
                        {
                            if (c == '"')
                            {
                                inQuotes = !inQuotes;
                                continue;
                            }

                            if (c == ',' && !inQuotes)
                            {
                                values.Add(current.Trim());
                                current = "";
                            }
                            else
                            {
                                current += c;
                            }
                        }

                        values.Add(current.Trim());

                        if (headerRow)
                        {
                            foreach (string col in values)
                            {
                                dtvs.Columns.Add(col.Trim());
                            }

                            headerRow = false;
                        }
                        else
                        {
                            DataRow dr = dtvs.NewRow();

                            for (int i = 0; i < dtvs.Columns.Count; i++)
                            {
                                dr[i] = i < values.Count ? values[i].Trim() : "";
                            }

                            dtvs.Rows.Add(dr);
                        }
                    }
                }

                if (dtvs.Rows.Count == 0)
                    continue;

                foreach (string col in requiredColumns)
                {
                    if (!dtvs.Columns.Contains(col))
                    {
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        lblMsg.Text = file.FileName + " : Column not found -> " + col;
                        return;
                    }
                }

                foreach (DataRow row in dtvs.Rows)
                {
                    DataRow dr = dtFinal.NewRow();

                    dr["INVALID_COLUMNS"] = row["INVALID_COLUMNS"].ToString().Trim();
                    dr["ORG_NAME"] = row["ORG_NAME"].ToString().Trim();
                    dr["ACADEMIC_COURSE_ID"] = row["ACADEMIC_COURSE_ID"].ToString().Trim();
                    dr["COURSE_NAME"] = row["COURSE_NAME"].ToString().Trim();
                    dr["STREAM"] = row["STREAM"].ToString().Trim();
                    dr["SESSION"] = row["SESSION"].ToString().Trim();
                    dr["REGN_NO"] = row["REGN_NO"].ToString().Trim();
                    dr["RROLL"] = row["RROLL"].ToString().Trim();
                    dr["CNAME"] = row["CNAME"].ToString().Trim();
                    dr["GENDER"] = row["GENDER"].ToString().Trim();

                    DateTime dob;
                    if (DateTime.TryParseExact(
                        row["DOB"].ToString().Trim(),
                        "dd/MM/yyyy",
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None,
                        out dob))
                    {
                        dr["DOB"] = dob;
                    }
                    else
                    {
                        dr["DOB"] = DBNull.Value;
                    }

                    dr["FNAME"] = row["FNAME"].ToString().Trim();
                    dr["MNAME"] = row["MNAME"].ToString().Trim();

                    dtFinal.Rows.Add(dr);
                }
            }

            if (dtFinal.Rows.Count == 0)
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "No valid CSV data found.";
                return;
            }

            Session["ExcelData"] = dtFinal;

            gvData.DataSource = dtFinal;
            gvData.DataBind();

            lblTotalRecords.Text = "Total Records : " + dtFinal.Rows.Count;

            if (Session["uid"].ToString() == "TMU00265" || Session["uid"].ToString() == "TMU09294" || Session["uid"].ToString() == "TMU09412")
            {
                btnPost.Visible = true;
            }

            lblMsg.ForeColor = System.Drawing.Color.Green;
            lblMsg.Text = "All CSV files loaded successfully.";
        }
        catch (Exception ex)
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = ex.Message;
        }
    }
    private DataTable CreateFinalTable()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("INVALID_COLUMNS");
        dt.Columns.Add("ORG_NAME");
        dt.Columns.Add("ACADEMIC_COURSE_ID");
        dt.Columns.Add("COURSE_NAME");
        dt.Columns.Add("STREAM");
        dt.Columns.Add("SESSION");
        dt.Columns.Add("REGN_NO");
        dt.Columns.Add("RROLL");
        dt.Columns.Add("CNAME");
        dt.Columns.Add("GENDER");
        dt.Columns.Add("DOB");
        dt.Columns.Add("FNAME");
        dt.Columns.Add("MNAME");

        return dt;
    }


    protected void btnPost_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = Session["ExcelData"] as DataTable;

            if (dt == null || dt.Rows.Count == 0)
            {
                lblMsg.Text = "No data available to post.";
                return;
            }

            string conStr = ConfigurationManager
                .ConnectionStrings["HRMSPortalConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.InsertStudentABCErrorData", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param = cmd.Parameters.AddWithValue("@StudentData", dt);
                    param.SqlDbType = SqlDbType.Structured;
                    param.TypeName = "dbo.StudentABCErrorDataType";

                    cmd.Parameters.AddWithValue("@UID", Session["uid"].ToString());

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            lblMsg.ForeColor = System.Drawing.Color.Green;
            lblMsg.Text = "Data Posted Successfully.";

            if (Session["uid"].ToString() == "TMU00265" || Session["uid"].ToString() == "TMU09294" || Session["uid"].ToString() == "TMU09412")
            {
                btnPost.Visible = true;
            }
            Session.Remove("ExcelData");
        }
        catch (Exception ex)
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = ex.Message;
        }
    }

    protected void btnFPost_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvData.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkSelect");

            if (chk != null && chk.Checked)
            {
                string regNo = gvData.DataKeys[row.RowIndex].Value.ToString();

                UpdateIsPost(regNo);
            }
        }

        ScriptManager.RegisterStartupScript(this, GetType(),
            "msg", "alert('Selected records posted successfully.');", true);
    }
    private void UpdateIsPost(string regNo)
    {

        using (SqlCommand cmd = new SqlCommand(
            "UPDATE StudentABCErrorData SET IsPost = 1 WHERE REGN_NO = @REGN_NO", con))
        {
            cmd.Parameters.AddWithValue("@REGN_NO", regNo);

            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("DigilockerErrorUpload.aspx");
    }

    protected void btnPosted_Click(object sender, EventArgs e)
    {
        gvData.Columns[0].Visible = false;
        btnFPost.Visible = false;
        SearchDataIsPost();
    }

    protected void btnCollectionReport_Click(object sender, EventArgs e)
    {
        btnFPost.Visible = false;
        btnReport.CssClass = "btn btnPost";
        btnPosted.CssClass = "btn btnPost";
        btnCollectionReport.CssClass = "btn btnPost btnSelected";
        SearchDataIsCorrect();

    }


    private void SearchDataIsPost()
    {
        try
        {
            btnReport.CssClass = "btn btnPost";
            btnPosted.CssClass = "btn btnPost btnSelected";
            btnCollectionReport.CssClass = "btn btnPost";
            string sql = "";
            if (Session["uid"].ToString() == "TMU00140")
            {
                if (txtEnrollmentNo.Text == "")
                {
                    sql = @"SELECT *
               FROM StudentABCErrorData
               where ispost=1
               ORDER BY ID DESC";
                }
                else
                {
                    sql = @"SELECT *
                               FROM StudentABCErrorData
                               WHERE (REGN_NO=@REGN_NO or collegeCode=@REGN_NO)
and ispost=1
                               ORDER BY ID DESC";
                }
            }
            else
            {
                sql = @"SELECT *
               FROM StudentABCErrorData
               WHERE UploadBy = @UploadBy
               ORDER BY ID DESC";
            }



            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.SelectCommand.Parameters.AddWithValue("@UploadBy", Session["uid"].ToString());
            da.SelectCommand.Parameters.AddWithValue("@REGN_NO",
               txtEnrollmentNo.Text.Trim());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvData.DataSource = dt;
            gvData.DataBind();

            lblTotalRecords.Text =
                "Total Records : " + dt.Rows.Count;
        }

        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }
    private void SearchDataIsCorrect()
    {
        try
        {
            btnReport.CssClass = "btn btnPost";
            btnPosted.CssClass = "btn btnPost";
            btnCollectionReport.CssClass = "btn btnPost btnSelected";
            string sql = "";
            if (Session["uid"].ToString() == "TMU00140")
            {
                if (txtEnrollmentNo.Text == "")
                {
                    sql = @"SELECT *
               FROM StudentABCErrorData
               where IsCorrected=1
               ORDER BY ID DESC";
                }
                else
                {
                    sql = @"SELECT *
                               FROM StudentABCErrorData
                               WHERE (REGN_NO=@REGN_NO or collegeCode=@REGN_NO)
and IsCorrected=1
                               ORDER BY ID DESC";
                }
            }
            else
            {
                sql = @"SELECT *
               FROM StudentABCErrorData
               WHERE UploadBy = @UploadBy
               ORDER BY ID DESC";
            }



            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.SelectCommand.Parameters.AddWithValue("@UploadBy", Session["uid"].ToString());
            da.SelectCommand.Parameters.AddWithValue("@REGN_NO",
               txtEnrollmentNo.Text.Trim());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvData.DataSource = dt;
            gvData.DataBind();

            lblTotalRecords.Text =
                "Total Records : " + dt.Rows.Count;
        }

        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }
}