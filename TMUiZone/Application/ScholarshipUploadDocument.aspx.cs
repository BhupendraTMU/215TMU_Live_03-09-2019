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

public partial class Application_ScholarshipUploadDocument : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                bindAttachment();
                hidesubmit();
                Uploaddocument();
                if (Chkemployee.Checked == true)
                {
                    btn_submit.Enabled = true;

                }
                else
                {
                    btn_submit.Enabled = false;
                }
            }
            catch
            {
                Response.Redirect("../Default.aspx");
            }


        }
    }

    public void bindAttachment()
    {
        DataTable dt = GetData("select  datalength([HighSchoolMarksheet]) HighSchoolMarksheet,datalength([InterMarksheet]) InterMarksheet,datalength([Diploma_final_Year]) Diploma_final_Year,datalength([UG_Final_Year]) UG_Final_Year,datalength([Transfer_Certificate]) as 'Transfer_Certificate',datalength([Character_Certificate]) 'Character_Certificate',datalength([Migration]) Migration,datalength([Anti Ragging]) [Anti Ragging],datalength([Domicile]) Domicile,datalength([Student_Aadhar]) Student_Aadhar,datalength([Guardian_Aadhar]) Guardian_Aadhar,datalength([Addmission_Form]) Addmission_Form,datalength([ABC_ID]) ABC_ID from [HRMSPortal].dbo.Tbl_EnrollmentTable where  Student_Number='" + Session["uid"].ToString() + "'");

        if (dt.Rows.Count > 0)
        {

            grdAttachment.DataSource = dt;
            grdAttachment.DataBind();
        }
        else
        {
            grdAttachment.DataSource = "";
            grdAttachment.DataBind();
        }
    }
    public void Uploaddocument()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "Select * from Tbl_EnrollmentTable where Student_Number= '" + Session["enroll"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {

            lblDocumentUploadStatu.Text = dt.Rows[0]["Doc_Upload_Status"].ToString();
            lbldocumentverifystatus.Text = dt.Rows[0]["Doc_Verify_Status"].ToString();          
            lblRejectionReasons.Text = dt.Rows[0]["Document Reject Remark"].ToString();
            
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

    protected void UploadBtn_Click(object sender, EventArgs e)
    {
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
                    //System.Drawing.Image Photo = System.Drawing.Image.FromStream(FileUpload2.PostedFile.InputStream);
                    //int height = Photo.Height;
                    //int width = img.Width;
                    decimal size = Math.Round(((decimal)FileUpload2.PostedFile.ContentLength / (decimal) 1024), 2);
                    int fs1 = 0;
                    fs1 = Convert.ToInt32(size);
                    if (fs1 > 200)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Image Size Should Not be Greater than 200 KB !')", true);
                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Image Size Could Not be Greater than 500 KB !');", true);
                      //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Image Size: " + size + "KB\\nHeight: " + height + "\\nWidth: " + width + "');", true);

                        return;
                    }

                }
            }

        
        if (contentType1 != "application/pdf")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Upload  PDF File Only');", true);

            return;
        }

        SqlCommand cmd = new SqlCommand("pro_InsertImageData", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
        cmd.Parameters.AddWithValue("@Attachment", Photo);
        cmd.Parameters.AddWithValue("@AttachmentName", drpuploaddpocument.SelectedItem.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "File Upload Successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        bindAttachment();
    }
}

protected void lbl10th_Click(object sender, EventArgs e)
{
    try
    {

        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select [HighSchoolMarksheet] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + Session["enroll"].ToString() + "'  ";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["HighSchoolMarksheet"];
                    contentType = "application/pdf";
                    fileName = "HighSchoolMarksheet";
                }
                con.Close();
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
        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
    }
}

protected void lbl12th_Click(object sender, EventArgs e)
{
    try
    {

        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select [InterMarksheet] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + Session["enroll"].ToString() + "'  ";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["InterMarksheet"];
                    contentType = "application/pdf";
                    fileName = "InterMarksheet";
                }
                con.Close();
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
        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
    }
}

protected void lbldipthe_Click(object sender, EventArgs e)
{
    try
    {

        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select [Diploma_final_Year] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + Session["enroll"].ToString() + "'  ";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Diploma_final_Year"];
                    contentType = "application/pdf";
                    fileName = "Diploma_final_Year";
                }
                con.Close();
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
        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
    }
}

protected void lblUG_Click(object sender, EventArgs e)
{
    try
    {

        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select [UG_Final_Year] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + Session["enroll"].ToString() + "'  ";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["UG_Final_Year"];
                    contentType = "application/pdf";
                    fileName = "UG_Final_Year";
                }
                con.Close();
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
        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
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
                cmd2.CommandText = "select [Transfer_Certificate] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + Session["enroll"].ToString() + "'  ";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Transfer_Certificate"];
                    contentType = "application/pdf";
                    fileName = "Transfer_Certificate";
                }
                con.Close();
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
        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
    }
}

protected void lblCharacter_Click(object sender, EventArgs e)
{
    try
    {

        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select [Character_Certificate] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + Session["enroll"].ToString() + "'  ";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Character_Certificate"];
                    contentType = "application/pdf";
                    fileName = "Character_Certificate";
                }
                con.Close();
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
        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
    }
}

protected void lblMigration_Click(object sender, EventArgs e)
{
    try
    {

        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select [Migration] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + Session["enroll"].ToString() + "'  ";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Migration"];
                    contentType = "application/pdf";
                    fileName = "Migration";
                }
                con.Close();
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
        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
    }
}

protected void lblGap_Click(object sender, EventArgs e)
{
    try
    {

        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select [Anti Ragging] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + Session["enroll"].ToString() + "'  ";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Anti Ragging"];
                    contentType = "application/pdf";
                    fileName = "Anti Ragging";
                }
                con.Close();
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
        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
    }
}

protected void lblDomicile_Click(object sender, EventArgs e)
{

    try
    {

        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select [Domicile] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + Session["enroll"].ToString() + "'  ";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Domicile"];
                    contentType = "application/pdf";
                    fileName = "Domicile";
                }
                con.Close();
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
        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
    }
}

protected void lblAadhar_Click(object sender, EventArgs e)
{
    try
    {

        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select [Student_Aadhar] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + Session["enroll"].ToString() + "'  ";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Student_Aadhar"];
                    contentType = "application/pdf";
                    fileName = "Student_Aadhar";
                }
                con.Close();
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
        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
    }
}

protected void lblGuardian_Click(object sender, EventArgs e)
{
    try
    {

        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select [Guardian_Aadhar] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + Session["enroll"].ToString() + "'  ";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Guardian_Aadhar"];
                    contentType = "application/pdf";
                    fileName = "Guardian_Aadhar";
                }
                con.Close();
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
        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
    }
}

protected void lblAdmission_Click(object sender, EventArgs e)
{
    try
    {

        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select [ABC_ID] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + Session["enroll"].ToString() + "'  ";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["ABC_ID"];
                    contentType = "application/pdf";
                    fileName = "ABC_ID";
                }
                con.Close();
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
        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
    }
}

protected void btn_submit_Click(object sender, EventArgs e)
{
     
    {
        SqlCommand cmd = new SqlCommand("Pro_Update", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Student_Number", Session["enroll"].ToString());
        cmd.Parameters.AddWithValue("@Student_Name", Session["Name"].ToString());
        //cmd.Parameters.AddWithValue("@Doc_Verify_Status", "Pending");
        cmd.Parameters.AddWithValue("@Doc_Upload_Status", "Submit");
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
    }
    Response.Redirect("ScholarshipUploadDocument.aspx");
}


public void hidesubmit()
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    {
        using (SqlCommand cmd = new SqlCommand("select * from Tbl_EnrollmentTable where Student_Number='" + Session["uid"].ToString() + "'", con))
        {
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string Doc_Upload_Status = dr["Doc_Upload_Status"].ToString();
                con.Close();
                if (Doc_Upload_Status == "Submit")

                {
                    btn_submit.Enabled = false;
                    UploadBtn.Enabled = false;
                    Chkemployee.Enabled = false;
                }
                else
                {
                    btn_submit.Enabled = true;
                    UploadBtn.Enabled = true;
                    Chkemployee.Enabled = true;
                    }


            }
        }
    }
}

    protected void Chkemployee_CheckedChanged(object sender, EventArgs e)
    {
        if(Chkemployee.Checked==true)
        {
            btn_submit.Enabled = true;
        
        }
        else
        {
            btn_submit.Enabled = false;
        }
    }
}