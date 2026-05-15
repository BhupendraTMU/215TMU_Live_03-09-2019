using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_StudentdocumentVerify : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                getDocumentverify();
                getCollageCode();
                if (Session["uid"].ToString() == "TMU00035")
                {
                    BtnSubmit.Visible = false;
                    BtnRejected.Visible = false;
                }
            }
            catch(Exception ex)
            {
                Response.Redirect("../Default.aspx");
            }

        }
    }

    public void getDocumentverify()
    {
        SqlCommand cmd = new SqlCommand("pro_getdocumentverifyData", con);
        //cmd.Parameters.AddWithValue("@Student_Number", grddocumentverificatiolist.DataKeys.ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grddocumentverificatiolist.DataSource = dtCL;
        grddocumentverify1.DataSource = dtCL;
        grddocumentverificatiolist.DataBind();
        grddocumentverify1.DataBind();
    }
    public void getCollageCode()
    {
        SqlCommand cmd = new SqlCommand("pro_DocumentCollageCode", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        drpCollageCode.DataSource = dtCL;
        drpCollageCode.DataTextField = "Details";
        drpCollageCode.DataValueField = "id";
        drpCollageCode.DataBind();
    }
    protected void drpCollage_SelectedIndexChanged(object sender, EventArgs e)
    {
        getCouserCode();
        Documentverifypendingddata();
        
    }
    public void getCouserCode()
    {
        SqlCommand cmd = new SqlCommand("pro_DocumentCourseCode", con);
        cmd.Parameters.AddWithValue("@CollageCode", drpCollageCode.SelectedItem.Text);
        cmd.CommandType = CommandType.StoredProcedure;
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        drpCourse.DataSource = dtCL;
        drpCourse.DataTextField = "DetailText";
        drpCourse.DataValueField = "Details";
        drpCourse.DataBind();
    }
    public void Documentverifypendingddata()
    {

        SqlCommand cmd = new SqlCommand("pro_Documentverifypendingddata", con);
        cmd.Parameters.AddWithValue("@Status", drpstatus.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@College", drpCollageCode.SelectedValue);
        cmd.Parameters.AddWithValue("@Course", drpCourse.SelectedValue);
        cmd.Parameters.AddWithValue("@STNo", txtstudentNo.Text);
        cmd.CommandType = CommandType.StoredProcedure;
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grddocumentverificatiolist.DataSource = dtCL;
        grddocumentverify1.DataSource = dtCL;
        grddocumentverificatiolist.DataBind();
        grddocumentverify1.DataBind();
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        int i = 0;
        foreach (GridViewRow row in grddocumentverificatiolist.Rows)
        {
            CheckBox check = (CheckBox)row.FindControl("Chkemployee");
            Label lblemployeecode = (Label)row.FindControl("lblemployeecode");
            TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
            var id = grddocumentverificatiolist.DataKeys[row.RowIndex].Value;
            SqlCommand cmd = new SqlCommand("pro_approveDocumentverify", con);
            if (check.Checked == true)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Student_Number", lblemployeecode.Text);
                cmd.Parameters.AddWithValue("@DocumentApprovedRemark", txtRemark.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();


                i++;
            }
        }
        if (i > 0) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Document Approved')", true); }
        else { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Data.. !')", true); }
        getDocumentverify();
    }

    protected void BtnRejected_Click(object sender, EventArgs e)
    {

        int i = 0;
        foreach (GridViewRow row in grddocumentverificatiolist.Rows)
        {
            CheckBox check = (CheckBox)row.FindControl("Chkemployee");
            Label lblemployeecode = (Label)row.FindControl("lblemployeecode");
            TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
            var id = grddocumentverificatiolist.DataKeys[row.RowIndex].Value;
            SqlCommand cmd = new SqlCommand("pro_RejectDocumentverify", con);
            if (check.Checked == true)
            {
                if (txtRemark.Text == "")
                {
                    string message1 = "Please Fil Remarks ";
                    string script1 = "window.onload = function(){ alert('";
                    script1 += message1;
                    script1 += "')};";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
                    return;
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Student_Number", lblemployeecode.Text);
                cmd.Parameters.AddWithValue("@DocumentRejectRemark", txtRemark.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();


                i++;
            }
        }
        if (i > 0) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Document Verification Rejected')", true); }
        else { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Data.. !')", true); }
        getDocumentverify();
    }
    protected void btnDocumentDownload_Click(object sender, EventArgs e)
    {
        if (drpCollageCode.SelectedValue!=null && drpCollageCode.SelectedValue.ToString()== "-- Collage Code --")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Collage')", true);
        }
        else if (drpCourse.SelectedValue != null && drpCourse.SelectedValue.ToString() == "-- Course --")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Course')", true);
        }
        else {
            SqlCommand cmd = null;
            string val = drpstatus.SelectedItem.Text;
            byte[] bytes;
            string contentType;
            if (val.Equals("Select"))
            {
                cmd = new SqlCommand("pro_getdocumentverifyDataNew", con);
                cmd.Parameters.AddWithValue("@CollageCode", drpCollageCode.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@CourseCode", drpCourse.SelectedItem.Text);
                cmd.CommandType = CommandType.StoredProcedure;
            }
            else
            {
                cmd = new SqlCommand("pro_DocumentverifypendingddataNew", con);
                cmd.Parameters.AddWithValue("@Status", drpstatus.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@CollageCode", drpCollageCode.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@CourseCode", drpCourse.SelectedItem.Text);
                cmd.CommandType = CommandType.StoredProcedure;
            }
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlDataAdapter daCL = new SqlDataAdapter(cmd);
            DataTable dtCL = new DataTable();
            daCL.Fill(dtCL);
            using (MemoryStream zipMemoryStream = new MemoryStream())
            {
                using (ZipArchive zip = new ZipArchive(zipMemoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (DataRow row in dtCL.Rows)
                    {
                        string collegeCode = row["Collegecode"].ToString();
                        string courseCode = row["Course Code"].ToString();
                        string studentNumber = row["Student_Number"].ToString().Replace("/", "_");
                        //HighSchoolMarksheet                      
                        if (row["HighSchoolMarksheet"]!= DBNull.Value) {
                            bytes = (byte[])row["HighSchoolMarksheet"];
                            contentType = "application/pdf";
                            if (bytes != null && bytes.Length > 0)
                        {
                                string userFolder = Path.Combine(collegeCode, courseCode, studentNumber, "HighSchoolMarksheet");
                                var zipFileEntry = zip.CreateEntry(userFolder + "/HighSchoolMarksheet.pdf"); // file name
                                using (Stream zipStream = zipFileEntry.Open())
                                {
                                    zipStream.Write(bytes, 0, bytes.Length);
                                }
                        } 
                        }
                        //InterMarksheet
                        if (row["InterMarksheet"] != DBNull.Value)
                        {
                            bytes = (byte[])row["InterMarksheet"];
                            contentType = "application/pdf";
                            if (bytes != null && bytes.Length > 0)
                            {
                                string userFolder = Path.Combine(collegeCode, courseCode, studentNumber, "InterMarksheet");
                                var zipFileEntry = zip.CreateEntry(userFolder + "/InterMarksheet.pdf"); // file name
                                using (Stream zipStream = zipFileEntry.Open())
                                {
                                    zipStream.Write(bytes, 0, bytes.Length);
                                }
                            }
                        }
                        //DiplomaFinalYear
                        if (row["Diploma_final_Year"] != DBNull.Value)
                        {
                            bytes = (byte[])row["Diploma_final_Year"];
                            contentType = "application/pdf";
                            if (bytes != null && bytes.Length > 0)
                            {
                                string userFolder = Path.Combine(collegeCode, courseCode, studentNumber, "DiplomaFinalYear");
                                var zipFileEntry = zip.CreateEntry(userFolder + "/DiplomaFinalYear.pdf"); // file name
                                using (Stream zipStream = zipFileEntry.Open())
                                {
                                    zipStream.Write(bytes, 0, bytes.Length);
                                }
                            }
                        }
                        //UGFinalYear
                        if (row["UG_Final_Year"] != DBNull.Value)
                        {
                            bytes = (byte[])row["UG_Final_Year"];
                            contentType = "application/pdf";
                            if (bytes != null && bytes.Length > 0)
                            {
                                string userFolder = Path.Combine(collegeCode, courseCode, studentNumber, "UGFinalYear");
                                var zipFileEntry = zip.CreateEntry(userFolder + "/UGFinalYear.pdf"); // file name
                                using (Stream zipStream = zipFileEntry.Open())
                                {
                                    zipStream.Write(bytes, 0, bytes.Length);
                                }
                            }
                        }
                        //TransferCertificate
                        if (row["Transfer_Certificate"] != DBNull.Value)
                        {
                            bytes = (byte[])row["Transfer_Certificate"];
                            contentType = "application/pdf";
                            if (bytes != null && bytes.Length > 0)
                            {
                                string userFolder = Path.Combine(collegeCode, courseCode, studentNumber, "TransferCertificate");
                                var zipFileEntry = zip.CreateEntry(userFolder + "/TransferCertificate.pdf"); // file name
                                using (Stream zipStream = zipFileEntry.Open())
                                {
                                    zipStream.Write(bytes, 0, bytes.Length);
                                }
                            }
                        }
                        //CharacterCertificate
                        if (row["Character_Certificate"] != DBNull.Value)
                        {
                            bytes = (byte[])row["Character_Certificate"];
                            contentType = "application/pdf";
                            if (bytes != null && bytes.Length > 0)
                            {
                                string userFolder = Path.Combine(collegeCode, courseCode, studentNumber, "CharacterCertificate");
                                var zipFileEntry = zip.CreateEntry(userFolder + "/CharacterCertificate.pdf"); // file name
                                using (Stream zipStream = zipFileEntry.Open())
                                {
                                    zipStream.Write(bytes, 0, bytes.Length);
                                }
                            }
                        }
                        //Migration
                        if (row["Migration"] != DBNull.Value)
                        {
                            bytes = (byte[])row["Migration"];
                            contentType = "application/pdf";
                            if (bytes != null && bytes.Length > 0)
                            {
                                string userFolder = Path.Combine(collegeCode, courseCode, studentNumber, "Migration");
                                var zipFileEntry = zip.CreateEntry(userFolder + "/Migration.pdf"); // file name
                                using (Stream zipStream = zipFileEntry.Open())
                                {
                                    zipStream.Write(bytes, 0, bytes.Length);
                                }
                            }
                        }
                        //GapCertificate
                        if (row["Gap_Affidavit"] != DBNull.Value)
                        {
                            bytes = (byte[])row["Gap_Affidavit"];
                            contentType = "application/pdf";
                            if (bytes != null && bytes.Length > 0)
                            {
                                string userFolder = Path.Combine(collegeCode, courseCode, studentNumber, "GapCertificate");
                                var zipFileEntry = zip.CreateEntry(userFolder + "/GapCertificate.pdf"); // file name
                                using (Stream zipStream = zipFileEntry.Open())
                                {
                                    zipStream.Write(bytes, 0, bytes.Length);
                                }
                            }
                        }
                        //Domocile
                        if (row["Domicile"] != DBNull.Value)
                        {
                            bytes = (byte[])row["Domicile"];
                            contentType = "application/pdf";
                            if (bytes != null && bytes.Length > 0)
                            {
                                string userFolder = Path.Combine(collegeCode, courseCode, studentNumber, "Domocile");
                                var zipFileEntry = zip.CreateEntry(userFolder + "/Domocile.pdf"); // file name
                                using (Stream zipStream = zipFileEntry.Open())
                                {
                                    zipStream.Write(bytes, 0, bytes.Length);
                                }
                            }
                        }
                        //StudentAadhar
                        if (row["Student_Aadhar"] != DBNull.Value)
                        {
                            bytes = (byte[])row["Student_Aadhar"];
                            contentType = "application/pdf";
                            if (bytes != null && bytes.Length > 0)
                            {
                                string userFolder = Path.Combine(collegeCode, courseCode, studentNumber, "StudentAadhar");
                                var zipFileEntry = zip.CreateEntry(userFolder + "/StudentAadhar.pdf"); // file name
                                using (Stream zipStream = zipFileEntry.Open())
                                {
                                    zipStream.Write(bytes, 0, bytes.Length);
                                }
                            }
                        }
                        //GuardianAadhar
                        if (row["Guardian_Aadhar"] != DBNull.Value)
                        {
                            bytes = (byte[])row["Guardian_Aadhar"];
                            contentType = "application/pdf";
                            if (bytes != null && bytes.Length > 0)
                            {
                                string userFolder = Path.Combine(collegeCode, courseCode, studentNumber, "GuardianAadhar");
                                var zipFileEntry = zip.CreateEntry(userFolder + "/GuardianAadhar.pdf"); // file name
                                using (Stream zipStream = zipFileEntry.Open())
                                {
                                    zipStream.Write(bytes, 0, bytes.Length);
                                }
                            }
                        }
                    }
                }
                Response.Clear();
                Response.ContentType = "application/zip";
                Response.AddHeader("Content-Disposition", "attachment; filename=Documents.zip");
                zipMemoryStream.Seek(0, SeekOrigin.Begin);
                zipMemoryStream.CopyTo(Response.OutputStream);
                Response.End();
            }

        }
    }
        protected void lnkbuttonhigh_Click(object sender, EventArgs e)
    {
        try
        {

            LinkButton btn = sender as LinkButton;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            string id = grddocumentverificatiolist.DataKeys[row.RowIndex].Values[0].ToString();
            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "Select HighSchoolMarksheet from Tbl_EnrollmentTable where ID =" + id + "";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["HighSchoolMarksheet"];
                        contentType = "application/pdf";
                        fileName = "HighSchoolMarksheet";
                    }
                    con2.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.AddHeader("Content-Disposition", string.Format("inline; filename={0}", "attachment"));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Record Found')", true);
        }
    }

    protected void lnkbuttonInter_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = sender as LinkButton;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            string id = grddocumentverificatiolist.DataKeys[row.RowIndex].Values[0].ToString();
            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "Select InterMarksheet from Tbl_EnrollmentTable where ID =" + id + "";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["InterMarksheet"];
                        contentType = "application/pdf";
                        fileName = "InterMarksheet";
                    }
                    con2.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.AddHeader("Content-Disposition", string.Format("inline; filename={0}", "attachment"));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Record Found')", true);
        }


    }

    protected void lnkbuttondiplomafinal_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = sender as LinkButton;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            string id = grddocumentverificatiolist.DataKeys[row.RowIndex].Values[0].ToString();
            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "Select Diploma_final_Year from Tbl_EnrollmentTable where ID =" + id + "";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Diploma_final_Year"];
                        contentType = "application/pdf";
                        fileName = "Diploma_final_Year";
                    }
                    con2.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
           // Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.AddHeader("Content-Disposition", string.Format("inline; filename={0}", "attachment"));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Record Found')", true);
        }
    }

    protected void lnkbuttonugfinal_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = sender as LinkButton;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            string id = grddocumentverificatiolist.DataKeys[row.RowIndex].Values[0].ToString();
            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "Select UG_Final_Year from Tbl_EnrollmentTable where ID =" + id + "";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["UG_Final_Year"];
                        contentType = "application/pdf";
                        fileName = "UG_Final_Year";
                    }
                    con2.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
           // Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.AddHeader("Content-Disposition", string.Format("inline; filename={0}", "attachment"));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Record Found')", true);
        }
    }

    protected void lnkbuttontc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = sender as LinkButton;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            string id = grddocumentverificatiolist.DataKeys[row.RowIndex].Values[0].ToString();
            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "Select Transfer_Certificate from Tbl_EnrollmentTable where ID =" + id + "";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Transfer_Certificate"];
                        contentType = "application/pdf";
                        fileName = "Transfer_Certificate";
                    }
                    con2.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
           // Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.AddHeader("Content-Disposition", string.Format("inline; filename={0}", "attachment"));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Record Found')", true);
        }
    }

    protected void lnkbuttoncc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = sender as LinkButton;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            string id = grddocumentverificatiolist.DataKeys[row.RowIndex].Values[0].ToString();
            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "Select [Anti Ragging] from Tbl_EnrollmentTable where ID =" + id + "";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Anti Ragging"];
                        contentType = "application/pdf";
                        fileName = "Anti_Ragging";
                    }
                    con2.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
           // Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.AddHeader("Content-Disposition", string.Format("inline; filename={0}", "attachment"));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Record Found')", true);
        }
    }

    protected void lnkbuttonmigration_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = sender as LinkButton;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            string id = grddocumentverificatiolist.DataKeys[row.RowIndex].Values[0].ToString();
            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "Select Migration from Tbl_EnrollmentTable where ID =" + id + "";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Migration"];
                        contentType = "application/pdf";
                        fileName = "Migration";
                    }
                    con2.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.AddHeader("Content-Disposition", string.Format("inline; filename={0}", "attachment"));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Record Found')", true);
        }
    }

    protected void lnkbuttongapcertificate_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = sender as LinkButton;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            string id = grddocumentverificatiolist.DataKeys[row.RowIndex].Values[0].ToString();
            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "Select Gap_Affidavit from Tbl_EnrollmentTable where ID =" + id + "";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Gap_Affidavit"];
                        contentType = "application/pdf";
                        fileName = "Gap_Affidavit";
                    }
                    con2.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.AddHeader("Content-Disposition", string.Format("inline; filename={0}", "attachment"));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Record Found')", true);
        }
    }

    protected void lnkbuttondomicile_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = sender as LinkButton;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            string id = grddocumentverificatiolist.DataKeys[row.RowIndex].Values[0].ToString();
            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "Select Domicile from Tbl_EnrollmentTable where ID =" + id + "";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Domicile"];
                        contentType = "application/pdf";
                        fileName = "Domicile";
                    }
                    con2.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.AddHeader("Content-Disposition", string.Format("inline; filename={0}", "attachment"));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Record Found')", true);
        }
    }
    protected void lnkbuttonstudentaadhar_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = sender as LinkButton;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            string id = grddocumentverificatiolist.DataKeys[row.RowIndex].Values[0].ToString();
            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "Select Student_Aadhar from Tbl_EnrollmentTable where ID =" + id + "";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Student_Aadhar"];
                        contentType = "application/pdf";
                        fileName = "Student_Aadhar";
                    }
                    con2.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
           // Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.AddHeader("Content-Disposition", string.Format("inline; filename={0}", "attachment"));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Record Found')", true);
        }
    }

    protected void lnkbuttonguardianaadhar_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = sender as LinkButton;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            string id = grddocumentverificatiolist.DataKeys[row.RowIndex].Values[0].ToString();
            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "Select Guardian_Aadhar from Tbl_EnrollmentTable where ID =" + id + "";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Guardian_Aadhar"];
                        contentType = "application/pdf";
                        fileName = "Guardian_Aadhar";
                    }
                    con2.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.AddHeader("Content-Disposition", string.Format("inline; filename={0}", "attachment"));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Record Found')", true);
        }
    }

    protected void grddocumentverificatiolist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grddocumentverificatiolist.PageIndex = e.NewPageIndex;
        getDocumentverify();
        Documentverifypendingddata();

    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void BtnExporttoExel_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        grddocumentverify1.RenderControl(htmlWrite);
        Response.Clear();
        string str = "StudentDataUploadList" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        Response.Write(stringWrite.ToString());
        Response.End();
    }

    protected void drpstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Documentverifypendingddata();
    }

    protected void lnkABCID_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = sender as LinkButton;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            string id = grddocumentverificatiolist.DataKeys[row.RowIndex].Values[0].ToString();
            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "Select ABC_ID from Tbl_EnrollmentTable where ID =" + id + "";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["ABC_ID"];
                        contentType = "application/pdf";
                        fileName = "ABC_ID";
                    }
                    con2.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.AddHeader("Content-Disposition", string.Format("inline; filename={0}", "attachment"));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Record Found')", true);
        }
    }

    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {

       
        Documentverifypendingddata();
    }

    protected void txtstudentNo_TextChanged(object sender, EventArgs e)
    {
        Documentverifypendingddata();
    }
}

   