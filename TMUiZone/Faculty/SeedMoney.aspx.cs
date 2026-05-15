
using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_SeedMoney : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                BindList(Session["uid"].ToString());

                Fillform(Session["uid"].ToString());


                //if (Session["uid"].ToString() == "TMU04282")
                //{
                //    lnkReport.Visible = true;
                //}
                //else
                //{
                //    lnkReport.Visible = false;
                //}

            }
            catch (Exception ex)
            {
                Response.Redirect("../Default.aspx");
            }



        }
    }

    protected void btnUploadDoc_Click(object sender, EventArgs e)
    {
        string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
        string contentType = FileUpload1.PostedFile.ContentType;
        using (Stream fs = FileUpload1.PostedFile.InputStream)
        {
            using (BinaryReader br = new BinaryReader(fs))
            {
                byte[] bytes = br.ReadBytes((Int32)fs.Length);
                using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
                {
                    SqlCommand cmd = new SqlCommand("proc_InsertRIAttachment", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ApplicationNo", hdfApplicationNo.Value);
                    cmd.Parameters.Add("@UserId", Session["uid"].ToString());
                    cmd.Parameters.Add("@Attachment", bytes);
                    cmd.Parameters.Add("@AttachmentType", contentType);
                    cmd.Parameters.Add("@FileName", filename);
                    cmd.Parameters.Add("@DocName", ddlAttachtype.SelectedItem.Text);
                    if (con2.State == ConnectionState.Closed)
                        con2.Open();
                    cmd.ExecuteNonQuery();
                    con2.Close();



                }

            }
        }
        bindAttachment();

    }
    public void bindAttachment()
    {
        SqlCommand cmd = new SqlCommand("proc_GetRIAttachment", con);
        cmd.Parameters.AddWithValue("@Fid", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@ApplicationNo", hdfApplicationNo.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        if (con.State != ConnectionState.Closed)
            con.Close();
        con.Open();
        da.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            if (Convert.ToInt32(dt1.Rows[0]["Approval"]) > 1)
            {
                grdDocument.Columns[6].Visible = false;
            }

            divAttachmentGrid.Visible = true;
            grdDocument.DataSource = dt1;
            grdDocument.DataBind();
        }
        else
        {
            divAttachmentGrid.Visible = false;
            grdDocument.DataSource = "";
            grdDocument.DataBind();
        }
    }
    protected void DownloadInboxFile(object sender, EventArgs e)
    {
        string Eval = (sender as LinkButton).CommandArgument;
        string AssignmentNo = (sender as LinkButton).CommandArgument;

        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select Attachment,[Content Type],[File Name] from tbl_RIAttachment where ID='" + AssignmentNo + "' ";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Attachment"];
                    contentType = sdr["Content Type"].ToString();
                    fileName = sdr["File Name"].ToString();
                }
                con.Close();
            }
        }
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = contentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();

    }
    protected void DeleteFile(object sender, EventArgs e)
    {
        string Eval = (sender as LinkButton).CommandArgument;
        string AssignmentNo = (sender as LinkButton).CommandArgument;


        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "delete from tbl_RIAttachment where ID='" + AssignmentNo + "' ";
                cmd2.Connection = con2;
                con2.Open();
                cmd2.ExecuteNonQuery();
                con2.Close();
            }
        }
        bindAttachment();
    }

    public void BindClaimList(string UserId)
    {
        SqlCommand cmd = new SqlCommand("proc_SeedMoneyClaimList", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", UserId);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            grdClaim.DataSource = dt;
            grdClaim.DataBind();
        }
    }
    public void BindList(string UserId)
    {
        SqlCommand cmd = new SqlCommand("proc_SeedMoneyList", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", UserId);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            grdTitleList.DataSource = dt;
            grdTitleList.DataBind();
        }
    }
    protected void lnkSeedMoneyApplication_Click(object sender, EventArgs e)
    {
        pnlTitleApplication.Visible = true;
        pnlTitleList.Visible = false;
        pnlApplicationApproval.Visible = false;
        pnlClaimApplication.Visible = false;
        pnlClaimList.Visible = false;

    }

    protected void lnkSeedMoneyApproval_Click(object sender, EventArgs e)
    {
        pnlApplicationApproval.Visible = true;
        pnlTitleList.Visible = false;
        pnlTitleApplication.Visible = false;
        pnlClaimApplication.Visible = false;
        pnlClaimList.Visible = false;

        BindListforApproval(Session["uid"].ToString());

    }

    public void BindListforApproval(string UserId)
    {
        try
        {

            SqlCommand cmd = new SqlCommand("proc_SeedMoneyListForApproval", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Close();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                grdApplicationApproval.DataSource = dt;
                grdApplicationApproval.DataBind();
            }
            else
            {
                grdApplicationApproval.DataSource = "";
                grdApplicationApproval.DataBind();
            }

        }
        catch (Exception ex)
        {

        }


    }

    public void BindClaimListforApproval(string UserId)
    {
        try
        {

            SqlCommand cmd = new SqlCommand("proc_SeedMoneyListForClaimApproval", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Close();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                grdClaimApproval.DataSource = dt;
                grdClaimApproval.DataBind();
            }
            else
            {
                grdClaimApproval.DataSource = "";
                grdClaimApproval.DataBind();
            }

        }
        catch (Exception ex)
        {

        }


    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        pnlTitleApplication.Visible = true;
        pnlApplicationApproval.Visible = false;
        pnlTitleList.Visible = false;

        pnlClaimApplication.Visible = false;
        pnlClaimList.Visible = true;
        pnlClaimApproval.Visible = false;

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }
    public void Fillform(string UserID)
    {
        SqlCommand cmd = new SqlCommand("proc_RecordforRI", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Fid", Session["uid"].ToString());
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtApplicant.Text = dt.Rows[0]["First Name"].ToString();
            txtDesignation.Text = dt.Rows[0]["Designation Code"].ToString();
            txtCollege.Text = dt.Rows[0]["CollegeName"].ToString();
            txtDep.Text = dt.Rows[0]["Department Name"].ToString();
        }
    }
    protected void lnkAnnexure_Click(object sender, EventArgs e)
    {

        try
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            wc.OpenRead(Server.MapPath("~/Faculty/Seed_Money_Proposal_Form-AnnexureI.docx"));
            Int64 bytes_total = Convert.ToInt64(wc.ResponseHeaders["Content-Length"]);
            Response.ContentType = "Application/docx";
            Response.AppendHeader("Content-Disposition", "attachment; filename=Seed_Money_Proposal_Form-AnnexureI.docx");
            Response.TransmitFile(Server.MapPath("~/Faculty/Seed_Money_Proposal_Form-AnnexureI.docx"));
            string Size = Response.OutputStream.Length.ToString();
            Response.End();
        }
        catch (Exception ex)
        {
            string message1 = "No Data Found.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }

    }

    protected void btnbackTitle_Click(object sender, EventArgs e)
    {
        Response.Redirect("SeedMoney.aspx");
    }

    protected void btnSubmitTitleApplication_Click(object sender, EventArgs e)
    {
        string Code = "";
        if (FileUpload1.HasFile)
        {
            string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string contentType = FileUpload1.PostedFile.ContentType;
            using (Stream fs = FileUpload1.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
                    {
                        SqlCommand cmd = new SqlCommand("SP_InsertSeedMoney", con2);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@UserId", Session["uid"].ToString());
                        cmd.Parameters.Add("@UserName", txtApplicant.Text);
                        cmd.Parameters.Add("@Designation", txtDesignation.Text);
                        cmd.Parameters.Add("@NOC", txtCollege.Text);
                        cmd.Parameters.Add("@Dept", txtDep.Text);
                        cmd.Parameters.Add("@Amount", txtAmount.Text);
                        cmd.Parameters.Add("@Title", txtTitle.Text);
                        cmd.Parameters.Add("@Ann_Attachment", bytes);
                        cmd.Parameters.Add("@Ann_ContentType", contentType);
                        cmd.Parameters.Add("@FileName", filename);
                        cmd.Parameters.Add("@PrincipalID", Session["hod_ID_Leave1"].ToString());
                        cmd.Parameters.Add("@OrderNo", SqlDbType.VarChar, 100);
                        cmd.Parameters["@OrderNo"].Direction = ParameterDirection.Output;
                        if (con2.State == ConnectionState.Closed)
                            con2.Open();
                        cmd.ExecuteNonQuery();
                        Code = cmd.Parameters["@OrderNo"].Value.ToString();
                        con2.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Your Application No. " + Code + " is Generate Successfully'); document.location.href='Seedmoney.aspx';", true);

                    }

                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select Attachment to Upload.');", true);
            return;
        }
    }

    protected void lnkAnnexure_Click1(object sender, EventArgs e)
    {
        string Eval = (sender as LinkButton).CommandArgument;
        string AssignmentNo = (sender as LinkButton).CommandArgument;
        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select [Ann_Attachment],[Ann_Content Type],Ann_FileName from [tbl_SeedMoney] where ID='" + AssignmentNo + "' ";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Ann_Attachment"];
                    contentType = sdr["Ann_Content Type"].ToString();
                    fileName = sdr["Ann_FileName"].ToString(); ;
                }
                con.Close();
            }
        }
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = contentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }

    protected void lnkMinutes_Click(object sender, EventArgs e)
    {
        string Eval = (sender as LinkButton).CommandArgument;
        string AssignmentNo = (sender as LinkButton).CommandArgument;
        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select [Minute_Attachment],[Minute_Content Type],Minute_FileName from [tbl_SeedMoney] where ID='" + AssignmentNo + "' ";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Minute_Attachment"];
                    contentType = sdr["Minute_Content Type"].ToString();
                    fileName = sdr["Minute_FileName"].ToString(); ;
                }
                con.Close();
            }
        }
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = contentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }

    protected void lnkGrandLetter_Click(object sender, EventArgs e)
    {
        string Eval = (sender as LinkButton).CommandArgument;
        string AssignmentNo = (sender as LinkButton).CommandArgument;
        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select [SM_G_Letter_Attachment],[SM_G_Letter_Content Type],SM_G_Letter_FileName from [tbl_SeedMoney] where ID='" + AssignmentNo + "' ";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["SM_G_Letter_Attachment"];
                    contentType = sdr["SM_G_Letter_Content Type"].ToString();
                    fileName = sdr["SM_G_Letter_FileName"].ToString(); ;
                }
                con.Close();
            }
        }
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = contentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }

    protected void btnApproveP_Click(object sender, EventArgs e)
    {
        string Complaint = (sender as LinkButton).CommandArgument;
        hdfApplicationNo.Value = Complaint;


        if (Session["uid"].ToString() == "TMU04127")
        {
            btnSApproval.Text = "Recommend";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal7').modal('show');</script>", false);
        }
        else if (Session["uid"].ToString() == "TMU05293")
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal8').modal('show');</script>", false);
        }
        else if (Session["uid"].ToString() == "TMU08026")
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal7').modal('show');</script>", false);
        }
        else

            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal6').modal('show');</script>", false);
    }





    protected void btnRejectPop_Click(object sender, EventArgs e)
    {

        string filename = Path.GetFileName(flUploadMinute.PostedFile.FileName);
        string contentType = flUploadMinute.PostedFile.ContentType;
        using (Stream fs = flUploadMinute.PostedFile.InputStream)
        {
            using (BinaryReader br = new BinaryReader(fs))
            {
                byte[] bytes = br.ReadBytes((Int32)fs.Length);


                SqlCommand cmd = new SqlCommand("[SP_UpdateSeedMoneyStatus]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ComplaintNo", hdfApplicationNo.Value);
                cmd.Parameters.Add("@UseId", Session["uid"].ToString());
                cmd.Parameters.Add("@Remark", txtRemark.Text);
                cmd.Parameters.Add("@Status", 3);
                cmd.Parameters.Add("@Ann_Attachment", bytes);
                cmd.Parameters.Add("@Ann_ContentType", contentType);
                cmd.Parameters.Add("@FileName", filename);

                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('Application Reject Successfully'); document.location.href='Seedmoney.aspx';", true);





    }



    protected void btnApprove_Click(object sender, EventArgs e)
    {
        string filename = Path.GetFileName(flUploadMinute.PostedFile.FileName);
        string contentType = flUploadMinute.PostedFile.ContentType;
        using (Stream fs = flUploadMinute.PostedFile.InputStream)
        {
            using (BinaryReader br = new BinaryReader(fs))
            {
                byte[] bytes = br.ReadBytes((Int32)fs.Length);


                SqlCommand cmd = new SqlCommand("[SP_UpdateSeedMoneyStatus]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ComplaintNo", hdfApplicationNo.Value);
                cmd.Parameters.Add("@UseId", Session["uid"].ToString());
                cmd.Parameters.Add("@Remark", txtRemark.Text);
                cmd.Parameters.Add("@Status", 2);
                cmd.Parameters.Add("@Minute_Attachment", bytes);
                cmd.Parameters.Add("@Minute_ContentType", contentType);
                cmd.Parameters.Add("@FileName", filename);

                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('Application Approved Successfully'); document.location.href='Seedmoney.aspx';", true);



    }

    protected void btnSApproval_Click(object sender, EventArgs e)
    {

        SqlCommand cmd = new SqlCommand("[SP_UpdateSeedMoneyStatus]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ComplaintNo", hdfApplicationNo.Value);
        cmd.Parameters.Add("@UseId", Session["uid"].ToString());
        cmd.Parameters.Add("@Remark", TextBox1.Text);
        cmd.Parameters.Add("@Status", 1);

        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('Application Approved Successfully'); document.location.href='Seedmoney.aspx';", true);

    }

    protected void btnSReject_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("[SP_UpdateSeedMoneyStatus]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ComplaintNo", hdfApplicationNo.Value);
        cmd.Parameters.Add("@UseId", Session["uid"].ToString());
        cmd.Parameters.Add("@Remark", TextBox1.Text);
        cmd.Parameters.Add("@Status", 2);

        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('Application Reject Successfully'); document.location.href='Seedmoney.aspx';", true);
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        string filename = Path.GetFileName(FileUpload2.PostedFile.FileName);
        string contentType = FileUpload2.PostedFile.ContentType;
        using (Stream fs = FileUpload2.PostedFile.InputStream)
        {
            using (BinaryReader br = new BinaryReader(fs))
            {
                byte[] bytes = br.ReadBytes((Int32)fs.Length);


                SqlCommand cmd = new SqlCommand("[SP_UpdateSeedMoneyStatus]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ComplaintNo", hdfApplicationNo.Value);
                cmd.Parameters.Add("@UseId", Session["uid"].ToString());
                cmd.Parameters.Add("@Remark", TextBox2.Text);
                cmd.Parameters.Add("@Status", 1);
                cmd.Parameters.Add("@Minute_Attachment", bytes);
                cmd.Parameters.Add("@Minute_ContentType", contentType);
                cmd.Parameters.Add("@FileName", filename);

                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('Application Approved Successfully'); document.location.href='Seedmoney.aspx';", true);


    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("[SP_UpdateSeedMoneyStatus]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ComplaintNo", hdfApplicationNo.Value);
        cmd.Parameters.Add("@UseId", Session["uid"].ToString());
        cmd.Parameters.Add("@Remark", TextBox2.Text);
        cmd.Parameters.Add("@Status", 2);

        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('Application Reject Successfully'); document.location.href='Seedmoney.aspx';", true);

    }

    protected void lnkClaimApplication_Click(object sender, EventArgs e)
    {
        pnlApplicationApproval.Visible = false;
        pnlTitleList.Visible = false;
        pnlTitleApplication.Visible = false;
        pnlClaimApplication.Visible = false;
        pnlClaimList.Visible = true;
        pnlClaimApproval.Visible = false;
        BindClaimList(Session["uid"].ToString());



    }

    protected void lnkClaimApproval_Click(object sender, EventArgs e)
    {
        pnlClaimApplication.Visible = false;
        pnlApplicationApproval.Visible = false;
        pnlTitleList.Visible = false;
        pnlTitleApplication.Visible = false;
        pnlClaimApproval.Visible = true;
        pnlClaimList.Visible = false;
        BindClaimListforApproval(Session["uid"].ToString());




    }

    protected void btnClaimNew_Click(object sender, EventArgs e)
    {

        pnlApplicationApproval.Visible = false;
        pnlTitleList.Visible = false;
        pnlTitleApplication.Visible = false;
        pnlClaimApplication.Visible = true;
        pnlClaimList.Visible = false;
        BindApplication(Session["uid"].ToString());




    }

    protected void lnkLetter_Click(object sender, EventArgs e)
    {
        string Eval = (sender as LinkButton).CommandArgument;
        string AssignmentNo = (sender as LinkButton).CommandArgument;
        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select [Letter_Attachment],[Letter_Content Type],Letter_Name from [tbl_SeedMoneyClaim] where ID='" + AssignmentNo + "' ";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Letter_Attachment"];
                    contentType = sdr["Letter_Content Type"].ToString();
                    fileName = sdr["Letter_Name"].ToString(); ;
                }
                con.Close();
            }
        }
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = contentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();





    }

    protected void lnkClaimBack_Click(object sender, EventArgs e)
    {
        pnlApplicationApproval.Visible = false;
        pnlTitleList.Visible = false;
        pnlTitleApplication.Visible = false;
        pnlClaimApplication.Visible = false;
        pnlClaimList.Visible = true;


    }

    public void BindApplication(string UserId)
    {
        try
        {

            SqlCommand cmd = new SqlCommand("proc_SeedMoneyApplication", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Close();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                drpApplicationNo.DataSource = dt;
                drpApplicationNo.DataTextField = "ApplicationNo";
                drpApplicationNo.DataValueField = "ID";
                drpApplicationNo.DataBind();
            }


        }
        catch (Exception ex)
        {

        }
    }
    protected void btnSubmitApplication_Click(object sender, EventArgs e)
    {
        string Code = "";
        if (flApplicationLetter.HasFile)
        {
            string filename = Path.GetFileName(flApplicationLetter.PostedFile.FileName);
            string contentType = flApplicationLetter.PostedFile.ContentType;
            using (Stream fs = flApplicationLetter.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
                    {
                        SqlCommand cmd = new SqlCommand("SP_InsertSeedMoneyClaim", con2);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@UserId", Session["uid"].ToString());
                        cmd.Parameters.Add("@ApplicationNoC", drpApplicationNo.SelectedValue);
                        cmd.Parameters.Add("@Amount", txtAmountC.Text);
                        cmd.Parameters.Add("@Remark", txtRemarkC.Text);
                        cmd.Parameters.Add("@Ann_Attachment", bytes);
                        cmd.Parameters.Add("@Ann_ContentType", contentType);
                        cmd.Parameters.Add("@FileName", filename);
                        cmd.Parameters.Add("@PrincipalID", Session["hod_ID_Leave1"].ToString());
                        cmd.Parameters.Add("@OrderNo", SqlDbType.VarChar, 100);
                        cmd.Parameters["@OrderNo"].Direction = ParameterDirection.Output;
                        if (con2.State == ConnectionState.Closed)
                            con2.Open();
                        cmd.ExecuteNonQuery();
                        Code = cmd.Parameters["@OrderNo"].Value.ToString();
                        con2.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Your Application No. " + Code + " is Generate Successfully'); document.location.href='Seedmoney.aspx';", true);

                    }

                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select Attachment to Upload.');", true);
            return;
        }



    }

    protected void lnkAppLetter_Click(object sender, EventArgs e)
    {
        string Eval = (sender as LinkButton).CommandArgument;
        string AssignmentNo = (sender as LinkButton).CommandArgument;
        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select [Letter_Attachment],[Letter_Content Type],Letter_Name from [tbl_SeedMoneyClaim] where ID='" + AssignmentNo + "' ";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Letter_Attachment"];
                    contentType = sdr["Letter_Content Type"].ToString();
                    fileName = sdr["Letter_Name"].ToString(); ;
                }
                con.Close();
            }
        }
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = contentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();

    }

    protected void btnApproveClaim_Click(object sender, EventArgs e)
    {
        string Complaint = (sender as LinkButton).CommandArgument;
        hdfApplicationNo.Value = Complaint;
        if (Session["uid"].ToString() == "TMU08026")
        {
            btnClaimApprove.Text = "Approve";
        }
        else
        {
            btnClaimApprove.Text = "Recommend";
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal9').modal('show');</script>", false);
    }

    protected void btnClaimApprove_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("[SP_UpdateSeedMoneyClaimStatus]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ComplaintNo", hdfApplicationNo.Value);
        cmd.Parameters.Add("@UseId", Session["uid"].ToString());
        cmd.Parameters.Add("@Remark", txtClaimRemark.Text);
       

        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('Application Approved Successfully'); document.location.href='Seedmoney.aspx';", true);

    }

    protected void btnClaimReject_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("[SP_UpdateSeedMoneyClaimStatus]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ComplaintNo", hdfApplicationNo.Value);
        cmd.Parameters.Add("@UseId", Session["uid"].ToString());
        cmd.Parameters.Add("@Remark", txtClaimRemark.Text);
        if (Session["uid"].ToString() == "TMU00002")
        {
            cmd.Parameters.Add("@Status", 3);
        }
        else
        {
            cmd.Parameters.Add("@Status", 2);
        }

        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('Application Approved Successfully'); document.location.href='Seedmoney.aspx';", true);

    }

    protected void lnkReport_Click(object sender, EventArgs e)
    {
        pnlClaimApplication.Visible = false;
        pnlApplicationApproval.Visible = false;
        pnlTitleList.Visible = false;
        pnlTitleApplication.Visible = false;
        pnlClaimApproval.Visible = false;
        pnlClaimList.Visible = false;
        pnlRepport.Visible = true;
        SqlCommand cmd = new SqlCommand("proc_SeedApplicationReport", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ReportViewer1.Visible = true;
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.Visible = true;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/SeedReport.rdlc");
            ReportDataSource datasource = new ReportDataSource("DataSetFA", dt);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
        }
    }
}