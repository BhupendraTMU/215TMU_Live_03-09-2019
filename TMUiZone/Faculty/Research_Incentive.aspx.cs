using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net.Configuration;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;




public partial class Faculty_Research_Incentive : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                notshow.Visible = false;
                BindList(Session["uid"].ToString());
                bindTypeOfReseach();
                Fillform(Session["uid"].ToString());
                bindData();

                if (Session["uid"].ToString() == "TMU05293" || Session["uid"].ToString() == "TMU04127" || Session["uid"].ToString() == "TMU08026")
                {
                    lnkReport.Visible = true;
                }
                else
                {
                    lnkReport.Visible = false;
                }
                if (Session["uid"].ToString() == "TMU05293")
                {
                    lnkRecovery.Visible = true;
                    lnkRecoveryReport.Visible = true;
                    lnkRecoveryApproval.Visible = false;
                }
                if (Session["uid"].ToString() == "TMU08026")
                {
                    lnkRecovery.Visible = false;
                    lnkRecoveryReport.Visible = true;
                    lnkRecoveryApproval.Visible = true;
                }
                if (Session["linkid"] != null)
                {

                    if (Session["linkid"].ToString() == "lnkResearchTitle")
                    {
                        lnkResearchTitle_Click(EventArgs.Empty, EventArgs.Empty);
                    }
                    if (Session["linkid"].ToString() == "lnkResearchTitleApproval")
                    {
                        lnkResearchTitleApproval_Click(EventArgs.Empty, EventArgs.Empty);
                    }
                    if (Session["linkid"].ToString() == "lnkApplication")
                    {
                        lnkApplication_Click(EventArgs.Empty, EventArgs.Empty);
                    }
                    if (Session["linkid"].ToString() == "lnkReApproval")
                    {
                        lnkReApproval_Click(EventArgs.Empty, EventArgs.Empty);
                    }
                    if (Session["linkid"].ToString() == "lnkReport")
                    {
                        lnkReport_Click(EventArgs.Empty, EventArgs.Empty);
                    }
                    if (Session["linkid"].ToString() == "lnkRecovery")
                    {
                        lnkRecovery_Click(EventArgs.Empty, EventArgs.Empty);
                    }
                    if (Session["linkid"].ToString() == "lnkRecoveryApproval")
                    {
                        lnkRecoveryApproval_Click(EventArgs.Empty, EventArgs.Empty);
                    }
                    if (Session["linkid"].ToString() == "lnkRecoveryReport")
                    {
                        lnkRecoveryReport_Click(EventArgs.Empty, EventArgs.Empty);
                    }
                }


            }
            catch (Exception ex)
            {
                Response.Redirect("../Default.aspx");
            }
        }



    }

    public void bindData()
    {
        SqlCommand cmd = new SqlCommand("proc_fetchRIMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;

        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlAttachtype.DataSource = dt;
            ddlAttachtype.DataTextField = "description";
            ddlAttachtype.DataValueField = "id";

            ddlAttachtype.DataBind();
        }
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
    public void BindList(string UserId)
    {
        SqlCommand cmd = new SqlCommand("proc_RITitleList", con);
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
    public void bindTypeOfReseach()
    {
        SqlCommand cmd = new SqlCommand("proc_fetchRIDocumentType", con);
        cmd.CommandType = CommandType.StoredProcedure;

        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            drpResearchIncentive.DataSource = dt;
            drpResearchIncentive.DataTextField = "RI_Type";
            drpResearchIncentive.DataValueField = "ID";

            drpResearchIncentive.DataBind();
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        pnlTitleApplication.Visible = true;
        pnlTitleList.Visible = false;
        pnlRIApproval.Visible = false;
        pnlRecoveryReq.Visible = false;
        pnlRecoveryReport.Visible = false;
        pnlRecoveryApproval.Visible = false;

    }

    protected void lnkSelectRecovery_Click(object sender, EventArgs e)
    {
        string Complaint = (sender as LinkButton).CommandArgument;
        hdfApplicationNo.Value = Complaint;
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("RI_GetApplicationByNo '" + Complaint + "','" + Session["uid"].ToString() + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            bindTitlebyApp(hdfApplicationNo.Value);
            //bindTitle(Session["uid"].ToString());
            drpAppTitle.Enabled = false;

            btnSaveTitleApplication.Visible = false;
            divUploadDoc.Visible = true;
            pnlApplicationList.Visible = false;
            pnlApplication.Visible = true;
            pnlRecoveryReq.Visible = false;
            pnlRecoveryApproval.Visible = false;
            txtApproveAmount.Text = dt.Rows[0]["ApprovedAmount"].ToString();
            txtNameofJournal1.Text = dt.Rows[0]["Name_of_journal"].ToString();
            txtVolume1.Text = dt.Rows[0]["Volume"].ToString();
            txtNoIssue1.Text = dt.Rows[0]["No_Issue"].ToString();
            txtPageno1.Text = dt.Rows[0]["Page_No"].ToString();
            drpAppTitle.SelectedValue = dt.Rows[0]["TitleID"].ToString();
            txtWeblink.Text = dt.Rows[0]["WebLink"].ToString();
            txtDOPub.Text = dt.Rows[0]["DOP"].ToString();
            txtISSN.Text = dt.Rows[0]["ISSN"].ToString();
            txtAuthposition.Text = dt.Rows[0]["AuthPosition"].ToString();
            txtNameofJournal1.Text = dt.Rows[0]["Name_of_journal"].ToString();
            txtVolume1.Text = dt.Rows[0]["Volume"].ToString();
            txtNoIssue1.Text = dt.Rows[0]["No_Issue"].ToString();
            txtPageno1.Text = dt.Rows[0]["Page_No"].ToString();
            hdfAuth.Value = dt.Rows[0]["No_of_author"].ToString();
            chkBookSubmit.Checked = Convert.ToBoolean(dt.Rows[0]["Book_Submit_RD"]);

            chkScopus1.Checked = Convert.ToBoolean(dt.Rows[0]["SCOPUSQ1"]);
            chkScopus2.Checked = Convert.ToBoolean(dt.Rows[0]["SCOPUSQ2"]);
            chkScopus3.Checked = Convert.ToBoolean(dt.Rows[0]["SCOPUSQ3"]);
            chkScopus4.Checked = Convert.ToBoolean(dt.Rows[0]["SCOPUSQ4"]);
            chkwos.Checked = Convert.ToBoolean(dt.Rows[0]["WOS"]);

            chkSCI.Checked = Convert.ToBoolean(dt.Rows[0]["SCI"]);

            chkSSCI.Checked = Convert.ToBoolean(dt.Rows[0]["SSCI"]);

            chkUGC.Checked = Convert.ToBoolean(dt.Rows[0]["UGC_Core"]);

            chkICI.Checked = Convert.ToBoolean(dt.Rows[0]["ICI"]);

            chkabdc.Checked = Convert.ToBoolean(dt.Rows[0]["ABDC"]);
            chkNAAS.Checked = Convert.ToBoolean(dt.Rows[0]["NAAS"]);


            chkWOSSSCI.Checked = Convert.ToBoolean(dt.Rows[0]["WOS-SSCI"]);
            chkWOSSCIE.Checked = Convert.ToBoolean(dt.Rows[0]["WOS-SCIE"]);
            chkWOSESCI.Checked = Convert.ToBoolean(dt.Rows[0]["WOS-ESCI"]);
            chkWOSAHCI.Checked = Convert.ToBoolean(dt.Rows[0]["WOS-AHCI"]);
            chkNATIONALPublisher.Checked = Convert.ToBoolean(dt.Rows[0]["NATIONAL Publisher"]);
            chkInternational.Checked = Convert.ToBoolean(dt.Rows[0]["International Publisher"]);
            chkOthers.Checked = Convert.ToBoolean(dt.Rows[0]["OTHERS"]);

            bindAttachment();

        }
    }

    protected void lnkSelect_Click(object sender, EventArgs e)
    {

        string Complaint = (sender as LinkButton).CommandArgument;
        hdfApplicationNo.Value = Complaint;
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("RI_GetApplicationByNo '" + Complaint + "','" + Session["uid"].ToString() + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {


            if (dt.Rows[0]["PrincipalStatus"].ToString() == "" || dt.Rows[0]["Hold"].ToString() == "1")
            {
                btnSubmitTitleApplication.Visible = true;
                btnUploadDoc.Visible = true;



            }
            else
            {
                btnSubmitTitleApplication.Visible = false;
                btnUploadDoc.Visible = false;
            }
            bindTitlebyApp(hdfApplicationNo.Value);
            //bindTitle(Session["uid"].ToString());
            drpAppTitle.Enabled = false;

            btnSaveTitleApplication.Visible = false;
            divUploadDoc.Visible = true;
            pnlApplicationList.Visible = false;
            pnlApplication.Visible = true;
            txtApproveAmount.Text = dt.Rows[0]["ApprovedAmount"].ToString();
            txtNameofJournal1.Text = dt.Rows[0]["Name_of_journal"].ToString();
            txtVolume1.Text = dt.Rows[0]["Volume"].ToString();
            txtNoIssue1.Text = dt.Rows[0]["No_Issue"].ToString();
            txtPageno1.Text = dt.Rows[0]["Page_No"].ToString();
            drpAppTitle.SelectedValue = dt.Rows[0]["TitleID"].ToString();
            txtWeblink.Text = dt.Rows[0]["WebLink"].ToString();
            txtDOPub.Text = dt.Rows[0]["DOP"].ToString();
            txtISSN.Text = dt.Rows[0]["ISSN"].ToString();
            txtAuthposition.Text = dt.Rows[0]["AuthPosition"].ToString();
            txtNameofJournal1.Text = dt.Rows[0]["Name_of_journal"].ToString();
            txtVolume1.Text = dt.Rows[0]["Volume"].ToString();
            txtNoIssue1.Text = dt.Rows[0]["No_Issue"].ToString();
            txtPageno1.Text = dt.Rows[0]["Page_No"].ToString();
            hdfAuth.Value = dt.Rows[0]["No_of_author"].ToString();
            chkBookSubmit.Checked = Convert.ToBoolean(dt.Rows[0]["Book_Submit_RD"]);

            chkScopus1.Checked = Convert.ToBoolean(dt.Rows[0]["SCOPUSQ1"]);
            chkScopus2.Checked = Convert.ToBoolean(dt.Rows[0]["SCOPUSQ2"]);
            chkScopus3.Checked = Convert.ToBoolean(dt.Rows[0]["SCOPUSQ3"]);
            chkScopus4.Checked = Convert.ToBoolean(dt.Rows[0]["SCOPUSQ4"]);
            chkwos.Checked = Convert.ToBoolean(dt.Rows[0]["WOS"]);

            chkSCI.Checked = Convert.ToBoolean(dt.Rows[0]["SCI"]);

            chkSSCI.Checked = Convert.ToBoolean(dt.Rows[0]["SSCI"]);

            chkUGC.Checked = Convert.ToBoolean(dt.Rows[0]["UGC_Core"]);

            chkICI.Checked = Convert.ToBoolean(dt.Rows[0]["ICI"]);

            chkabdc.Checked = Convert.ToBoolean(dt.Rows[0]["ABDC"]);
            chkNAAS.Checked = Convert.ToBoolean(dt.Rows[0]["NAAS"]);


            chkWOSSSCI.Checked = Convert.ToBoolean(dt.Rows[0]["WOS-SSCI"]);
            chkWOSSCIE.Checked = Convert.ToBoolean(dt.Rows[0]["WOS-SCIE"]);
            chkWOSESCI.Checked = Convert.ToBoolean(dt.Rows[0]["WOS-ESCI"]);
            chkWOSAHCI.Checked = Convert.ToBoolean(dt.Rows[0]["WOS-AHCI"]);
            chkNATIONALPublisher.Checked = Convert.ToBoolean(dt.Rows[0]["NATIONAL Publisher"]);
            chkInternational.Checked = Convert.ToBoolean(dt.Rows[0]["International Publisher"]);
            chkOthers.Checked = Convert.ToBoolean(dt.Rows[0]["OTHERS"]);

            //for (int i = 0; i < Convert.ToInt32(dt.Rows[0]["No_of_author"]); i++)
            //{
            //    HtmlGenericControl div = new HtmlGenericControl("div");
            //    div.ID = "div" + i;
            //    if (i == 3)
            //    {
            //        div.Attributes.Add("class", "col-sm-2 p-0");
            //    }
            //    else
            //    {
            //        div.Attributes.Add("class", "col-sm-3 p-0");
            //    }
            //    int auth = i + 1;
            //    Label l = new Label();
            //    l.Attributes.Add("class", "col-form-label");
            //    l.Text = "Author " + auth;
            //    TextBox t = new TextBox();
            //    t.Attributes.Add("CssClass", "form-control");
            //    t.Attributes.Add("runat", "server");
            //    t.Width = 200;
            //    t.ID = "txtAuthor" + i;
            //    t.Text = dt.Rows[0]["Auth" + auth].ToString();
            //    RequiredFieldValidator reqFieldVal = new RequiredFieldValidator();
            //    reqFieldVal.ID = "validator_" + i;
            //    reqFieldVal.ControlToValidate = t.ID;
            //    reqFieldVal.ForeColor = System.Drawing.Color.Red;
            //    reqFieldVal.ErrorMessage = "**";
            //    reqFieldVal.Display = ValidatorDisplay.Dynamic;
            //    reqFieldVal.ValidationGroup = "R1";
            //    div.Controls.Add(l);
            //    div.Controls.Add(reqFieldVal);
            //    div.Controls.Add(t);
            //    divAuthor.Controls.Add(div);
            //}
            bindAttachment();
        }
        else
        {
            //btnSubmit1.Visible = false;
            //btnSave1.Visible = true;
            //divUploadDoc.Visible = false;

            btnSubmitTitleApplication.Visible = false;
            btnSaveTitleApplication.Visible = true;
            divUploadDoc.Visible = false;

        }
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {


        if (drpResearchIncentive.SelectedValue == "1")
        {
            if (txttitlePaper.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Title of the Paper')", true);
                return;
            }
            if (txtNameofJournal.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Name of the Journal')", true);
                return;
            }
            if (txtISSNno.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter ISSN No.')", true);
                return;
            }
            if (txtLOA.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Link of Article')", true);
                return;
            }


        }
        if (drpResearchIncentive.SelectedValue == "2")
        {
            if (txttitlePaper.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Title')", true);
                return;
            }
            if (txtNameofJournal.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Application No.')", true);
                return;
            }

            if (txtDOP.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Date of Publication')", true);
                return;
            }
        }
        if (Convert.ToInt32(drpResearchIncentive.SelectedValue) > 2)
        {
            if (txttitlePaper.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Title')", true);
                return;
            }
            if (txtNameofJournal.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Name of the Book/Publisher')", true);
                return;
            }
            if (txtDOP.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Date of Publication')", true);
                return;
            }
            if (txtISSNno.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter ISBN No.')", true);
                return;
            }

        }



        SqlCommand cmd1 = new SqlCommand("proc_fetchTitlebyName", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.AddWithValue("@TitleName", txttitlePaper.Text);

        if (con.State == ConnectionState.Closed)
            con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (Convert.ToInt32(dt.Rows[0]["c"]) > 0)
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Incentive was given on the Title')", true);
            return;
        }
        SqlCommand cmd = new SqlCommand("FA_InsertRITitle", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@UserID", Session["uid"].ToString());
        cmd.Parameters.Add("@title_of_paper", txttitlePaper.Text);
        cmd.Parameters.Add("@Name_of_journal", txtNameofJournal.Text);
        cmd.Parameters.Add("@Volume", txtVolume.Text);
        cmd.Parameters.Add("@PageNo", txtPageNop.Text.ToString());
        cmd.Parameters.Add("@Date_Of_Publication", txtDOP.Text);
        cmd.Parameters.Add("@No_of_author", txtNumberOfAuthor.Text);
        cmd.Parameters.Add("@Link_of_Author", txtLOA.Text);
        cmd.Parameters.Add("@Type_of_RI", drpResearchIncentive.SelectedValue);
        cmd.Parameters.Add("@Type_of_RI_Name", drpResearchIncentive.SelectedItem.Text);
        cmd.Parameters.Add("@No_Issue", txtNoIssue.Text.ToString());
        cmd.Parameters.Add("@ISSNno", txtISSNno.Text.ToString());
        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Your Title Rquest Submit Successfully'); document.location.href='Research_Incentive.aspx';", true);
    }

    protected void lnkResearchTitle_Click(object sender, EventArgs e)
    {
        Response.Redirect("Research_Incentive.aspx");
        Session["linkid"] = "lnkResearchTitle";
    }

    protected void lnkResearchTitleApproval_Click(object sender, EventArgs e)
    {
        Session["linkid"] = "lnkResearchTitleApproval";
        pnlRIApproval.Visible = true;
        pnlTitleApplication.Visible = false;
        pnlTitleList.Visible = false;
        pnlTitleApplication.Visible = false;
        pnlRIApplicationApproval.Visible = false;
        pnlApplicationList.Visible = false;
        pnlRecoveryReq.Visible = false;
        pnlRecoveryReport.Visible = false;
        pnlRecoveryApproval.Visible = false;

        BindApprovalList(Session["uid"].ToString());

    }

    public void BindApprovalList(string UserId)
    {
        SqlCommand cmd = new SqlCommand("proc_RIApprovalTitleList", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", UserId);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {

            grdApproval.DataSource = dt;
            grdApproval.DataBind();
        }
        else
        {
            grdApproval.DataSource = "";
            grdApproval.DataBind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

    }

    protected void lnSelect_Click(object sender, EventArgs e)
    {
        string AssignmentNo = (sender as LinkButton).CommandArgument;
        SqlCommand cmd = new SqlCommand("proc_RIApprovalTitleListbyID", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Id", AssignmentNo);
        hdfTitleofPaper.Value = AssignmentNo;
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblTitleofPaper.Text = dt.Rows[0]["Title_of_paper"].ToString();
            lblNameofJournal.Text = dt.Rows[0]["Name_of_journal"].ToString();
            lblVolume.Text = dt.Rows[0]["Volume"].ToString();
            lblPageNo.Text = dt.Rows[0]["Page_No"].ToString();
            lblDOP.Text = dt.Rows[0]["Date_Of_Publication"].ToString();
            lblNoOFAuther.Text = dt.Rows[0]["No_of_author"].ToString();
            hpr.NavigateUrl = dt.Rows[0]["Link_of_Author"].ToString();
            hpr.Text = dt.Rows[0]["Link_of_Author"].ToString();
            lblTypeOfResearch.Text = dt.Rows[0]["Type_of_RI_Name"].ToString();
            lblISBNNo.Text = dt.Rows[0]["ISSNno"].ToString();


        }

        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal10').modal('show');</script>", false);
    }

    protected void lnkApplication_Click(object sender, EventArgs e)
    {
        Session["linkid"] = "lnkApplication";

        pnlRIApplicationApproval.Visible = false;
        pnlApplicationList.Visible = true;
        pnlRIApproval.Visible = false;
        pnlApplication.Visible = false;
        pnlTitleApplication.Visible = false;
        pnlTitleList.Visible = false;
        pnlTitleApplication.Visible = false;
        divUploadDoc.Visible = false;
        divAttachmentGrid.Visible = false;
        pnlRecoveryReq.Visible = false;
        pnlRecoveryReport.Visible = false;
        pnlRecoveryApproval.Visible = false;

        BindApplicationList(Session["uid"].ToString());



    }

    protected void btnbackTitle_Click(object sender, EventArgs e)
    {
        if (Session["linkid"].ToString() == "lnkResearchTitle")
        {
            lnkResearchTitle_Click(EventArgs.Empty, EventArgs.Empty);
        }
        if (Session["linkid"].ToString() == "lnkResearchTitleApproval")
        {
            lnkResearchTitleApproval_Click(EventArgs.Empty, EventArgs.Empty);
        }
        if (Session["linkid"].ToString() == "lnkApplication")
        {
            lnkApplication_Click(EventArgs.Empty, EventArgs.Empty);
        }
        if (Session["linkid"].ToString() == "lnkReApproval")
        {
            lnkReApproval_Click(EventArgs.Empty, EventArgs.Empty);
        }
        if (Session["linkid"].ToString() == "lnkReport")
        {
            lnkReport_Click(EventArgs.Empty, EventArgs.Empty);
        }
        if (Session["linkid"].ToString() == "lnkRecovery")
        {
            lnkRecovery_Click(EventArgs.Empty, EventArgs.Empty);
        }
        if (Session["linkid"].ToString() == "lnkRecoveryApproval")
        {
            lnkRecoveryApproval_Click(EventArgs.Empty, EventArgs.Empty);
        }
        if (Session["linkid"].ToString() == "lnkRecoveryReport")
        {
            lnkRecoveryReport_Click(EventArgs.Empty, EventArgs.Empty);
        }


    }

    protected void btnSubmitTitleApplication_Click(object sender, EventArgs e)
    {

        int Attcount = 0;
        for (int i = 0; i < grdDocument.Rows.Count; i++)
        {
            string s = grdDocument.Rows[i].Cells[1].Text.ToString();

            if (s == "PDF copy of Article/book/book chapter/conference proceeding/Patent" || s == "Supporting document in support of indexing" || s == "Vidwan Profile Updated Document" || s == "Google Scholar Profile Document")
            {
                Attcount++;
            }
        }


        if (Attcount < 4)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload all documents')", true);
            return;
        }

        string Code = "";
        SqlCommand cmd = new SqlCommand("RI_SubmitApplication", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add("@ApplicationNo", hdfApplicationNo.Value);
        cmd.Parameters.Add("@applicant_code", Session["uid"].ToString());
        cmd.Parameters.Add("@applicant_name", Session["uname"].ToString());
        cmd.Parameters.Add("@designation", Session["DesignationCode"].ToString());
        cmd.Parameters.Add("@Application_Title", drpAppTitle.SelectedValue);
        cmd.Parameters.Add("@WebLink", txtWeblink.Text);
        cmd.Parameters.Add("@DOP", txtDOPub.Text);
        cmd.Parameters.Add("@ISSN", txtISSN.Text);



        if (chkBookSubmit.Checked == true)
        {
            cmd.Parameters.Add("@Book_Submit_RD", 1);
        }
        else
        {
            cmd.Parameters.Add("@Book_Submit_RD", 0);
        }
        if (chkScopus1.Checked == true)
        {
            cmd.Parameters.Add("@SCOPUS1", 1);
        }
        else
        {
            cmd.Parameters.Add("@SCOPUS1", 0);
        }
        if (chkScopus2.Checked == true)
        {
            cmd.Parameters.Add("@SCOPUS2", 1);
        }
        else
        {
            cmd.Parameters.Add("@SCOPUS2", 0);
        }
        if (chkScopus3.Checked == true)
        {
            cmd.Parameters.Add("@SCOPUS3", 1);
        }
        else
        {
            cmd.Parameters.Add("@SCOPUS3", 0);
        }
        if (chkScopus4.Checked == true)
        {
            cmd.Parameters.Add("@SCOPUS4", 1);
        }
        else
        {
            cmd.Parameters.Add("@SCOPUS4", 0);
        }
        if (chkwos.Checked == true)
        {
            cmd.Parameters.Add("@WOS", 1);
        }
        else
        {
            cmd.Parameters.Add("@WOS", 0);
        }
        if (chkSCI.Checked == true)
        {
            cmd.Parameters.Add("@SCI", 1);
        }
        else
        {
            cmd.Parameters.Add("@SCI", 0);
        }
        if (chkSSCI.Checked == true)
        {
            cmd.Parameters.Add("@SSCI", 1);
        }
        else
        {
            cmd.Parameters.Add("@SSCI", 0);
        }
        if (chkUGC.Checked == true)
        {
            cmd.Parameters.Add("@UGC_Core", 1);
        }
        else
        {
            cmd.Parameters.Add("@UGC_Core", 0);
        }
        if (chkICI.Checked == true)
        {
            cmd.Parameters.Add("@ICI", 1);
        }
        else
        {
            cmd.Parameters.Add("@ICI", 0);
        }
        if (chkabdc.Checked == true)
        {
            cmd.Parameters.Add("@ABDC", 1);
        }
        else
        {
            cmd.Parameters.Add("@ABDC", 0);
        }
        if (chkNAAS.Checked == true)
        {
            cmd.Parameters.Add("@NAAS", 1);
        }
        else
        {
            cmd.Parameters.Add("@NAAS", 0);
        }
        //for (int i = 0; i < Convert.ToInt32(hdfAuth.Value); i++)
        //{

        //    var id = "ctl00$ContentPlaceHolder1$txtAuthor" + i.ToString();

        //    cmd.Parameters.Add("@Auth" + i + "", Request.Form[id]);
        //}
        cmd.Parameters.Add("@AuthPosition", txtAuthposition.Text.ToString());
        cmd.Parameters.Add("@PrincipalID", Session["hod_ID_Leave1"].ToString());
        cmd.Parameters.Add("@SecondApprovalID", "TMU04127");
        cmd.Parameters.Add("@ThirdApprovalID", "TMU05293");
        cmd.Parameters.Add("@VCID", "TMU08026");
        cmd.Parameters.Add("@Status", 2);



        cmd.Parameters.Add("@OrderNo", SqlDbType.VarChar, 100);
        cmd.Parameters["@OrderNo"].Direction = ParameterDirection.Output;
        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();

        Code = cmd.Parameters["@OrderNo"].Value.ToString();


        con.Close();



        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Your Application No. " + Code + " is Generate Successfully'); document.location.href='Research_Incentive.aspx';", true);











    }

    protected void btnAddNewApplication_Click(object sender, EventArgs e)
    {
        pnlApplicationList.Visible = false;
        pnlApplication.Visible = true;
        //btnSaveTitleApplication.Visible = true;
        //btnSubmitTitleApplication.Visible = false;
        pnlTitleApplication.Visible = false;
        pnlTitleList.Visible = false;
        pnlTitleApplication.Visible = false;
        pnlRecoveryReq.Visible = false;
        pnlRecoveryReport.Visible = false;
        pnlRecoveryApproval.Visible = false;
        bindTitle(Session["uid"].ToString());
    }
    public void bindTitle(string UserId)
    {


        SqlCommand cmd = new SqlCommand("proc_fetchTitle", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", UserId);

        if (con.State == ConnectionState.Closed)
            con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            drpAppTitle.DataSource = dt;
            drpAppTitle.DataTextField = "Title_of_paper";
            drpAppTitle.DataValueField = "id";
            drpAppTitle.DataBind();
        }






    }
    public void BindApplicationList(string UserId)
    {
        SqlCommand cmd = new SqlCommand("proc_RIApplicationList", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", UserId);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            grdApplication.DataSource = dt;
            grdApplication.DataBind();
        }
        else
        {
            grdApplication.DataSource = "";
            grdApplication.DataBind();
        }
    }

    protected void drpAppTitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("proc_GetAuthorfromtitle", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@TitleID", drpAppTitle.SelectedValue);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            txtDOPub.Text = dt.Rows[0]["Date_Of_Publication"].ToString();
            hdfAuth.Value = dt.Rows[0]["No_of_author"].ToString();
            txtWeblink.Text = dt.Rows[0]["Link_of_Author"].ToString();
            txtNameofJournal1.Text = dt.Rows[0]["Name_of_journal"].ToString();
            txtVolume1.Text = dt.Rows[0]["Volume"].ToString();
            txtPageno1.Text = dt.Rows[0]["Page_No"].ToString();
            txtNoIssue1.Text = dt.Rows[0]["No_Issue"].ToString();
            txtISSN.Text = dt.Rows[0]["ISSNno"].ToString();

            //for (int i = 0; i < Convert.ToInt32(dt.Rows[0]["No_of_author"]); i++)
            //{
            //    HtmlGenericControl div = new HtmlGenericControl("div");
            //    div.ID = "div" + i;
            //    if (i == 3)
            //    {
            //        div.Attributes.Add("class", "col-sm-2 p-0");
            //    }
            //    else
            //    {
            //        div.Attributes.Add("class", "col-sm-3 p-0");
            //    }
            //    int auth = i + 1;
            //    Label l = new Label();
            //    l.Attributes.Add("class", "col-form-label");
            //    l.Text = "Author " + auth;
            //    TextBox t = new TextBox();
            //    t.Attributes.Add("CssClass", "form-control");
            //    t.Attributes.Add("runat", "server");
            //    t.Width = 200;
            //    t.ID = "txtAuthor" + i;

            //    RequiredFieldValidator reqFieldVal = new RequiredFieldValidator();
            //    reqFieldVal.ID = "validator_" + i;
            //    reqFieldVal.ControlToValidate = t.ID;
            //    reqFieldVal.ForeColor = System.Drawing.Color.Red;
            //    reqFieldVal.ErrorMessage = "**";
            //    reqFieldVal.Display = ValidatorDisplay.Dynamic;
            //    reqFieldVal.ValidationGroup = "R1";
            //    div.Controls.Add(l);
            //    div.Controls.Add(reqFieldVal);
            //    div.Controls.Add(t);
            //    divAuthor.Controls.Add(div);
            //}
        }
    }

    protected void btnSaveTitleApplication_Click(object sender, EventArgs e)
    {
        SqlCommand cmd1 = new SqlCommand("proc_GetRIApplicationCount", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.AddWithValue("@TitleID", drpAppTitle.SelectedValue);
        cmd1.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        System.Data.DataSet ds = new System.Data.DataSet();
        da.Fill(ds);
        con.Close();
        if (Convert.ToInt32(ds.Tables[0].Rows[0]["C"]) > 0)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Your Already applied for this title'); document.location.href='Research_Incentive.aspx';", true);
            return;


        }



        if (Convert.ToInt32(ds.Tables[1].Rows.Count) > 0)
        {
            if (Convert.ToInt32(ds.Tables[1].Rows[0]["C"]) == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Your Already applied for this title'); document.location.href='Research_Incentive.aspx';", true);
                return;

            }
        }


        if (chkBookSubmit.Checked != true)
        {
            if (txtWeblink.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('Please fill the Web Link.'); ", true);
                return;
            }
        }
        string Code = "";
        SqlCommand cmd = new SqlCommand("RI_InsertApplication", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@applicant_code", Session["uid"].ToString());
        cmd.Parameters.Add("@applicant_name", Session["uname"].ToString());
        cmd.Parameters.Add("@designation", Session["DesignationCode"].ToString());
        cmd.Parameters.Add("@Application_Title", drpAppTitle.SelectedValue);
        cmd.Parameters.Add("@WebLink", txtWeblink.Text);
        cmd.Parameters.Add("@DOP", txtDOPub.Text);
        cmd.Parameters.Add("@ISSN", txtISSN.Text);

        if (chkBookSubmit.Checked == true)
        {
            cmd.Parameters.Add("@Book_Submit_RD", 1);
        }
        else
        {
            cmd.Parameters.Add("@Book_Submit_RD", 0);
        }
        if (chkScopus1.Checked == true)
        {
            cmd.Parameters.Add("@SCOPUS1", 1);
        }
        else
        {
            cmd.Parameters.Add("@SCOPUS1", 0);
        }
        if (chkScopus2.Checked == true)
        {
            cmd.Parameters.Add("@SCOPUS2", 1);
        }
        else
        {
            cmd.Parameters.Add("@SCOPUS2", 0);
        }
        if (chkScopus3.Checked == true)
        {
            cmd.Parameters.Add("@SCOPUS3", 1);
        }
        else
        {
            cmd.Parameters.Add("@SCOPUS3", 0);
        }
        if (chkScopus4.Checked == true)
        {
            cmd.Parameters.Add("@SCOPUS4", 1);
        }
        else
        {
            cmd.Parameters.Add("@SCOPUS4", 0);
        }
        if (chkWOSSSCI.Checked == true)
        {
            cmd.Parameters.Add("@WOSSSCI", 1);
        }
        else
        {
            cmd.Parameters.Add("@WOSSSCI", 0);
        }
        if (chkWOSSCIE.Checked == true)
        {
            cmd.Parameters.Add("@WOSSCIE", 1);
        }
        else
        {
            cmd.Parameters.Add("@WOSSCIE", 0);
        }
        if (chkWOSESCI.Checked == true)
        {
            cmd.Parameters.Add("@WOSESCI", 1);
        }
        else
        {
            cmd.Parameters.Add("@WOSESCI", 0);
        }
        if (chkWOSAHCI.Checked == true)
        {
            cmd.Parameters.Add("@WOSAHCI", 1);
        }
        else
        {
            cmd.Parameters.Add("@WOSAHCI", 0);
        }

        if (chkSSCI.Checked == true)
        {
            cmd.Parameters.Add("@SSCI", 1);
        }
        else
        {
            cmd.Parameters.Add("@SSCI", 0);
        }
        if (chkabdc.Checked == true)
        {
            cmd.Parameters.Add("@ABDC", 1);
        }
        else
        {
            cmd.Parameters.Add("@ABDC", 0);
        }
        if (chkICI.Checked == true)
        {
            cmd.Parameters.Add("@ICI", 1);
        }
        else
        {
            cmd.Parameters.Add("@ICI", 0);
        }



        if (chkNATIONALPublisher.Checked == true)
        {
            cmd.Parameters.Add("@NATIONALPublisher", 1);
        }
        else
        {
            cmd.Parameters.Add("@NATIONALPublisher", 0);
        }

        if (chkInternational.Checked == true)
        {
            cmd.Parameters.Add("@International", 1);
        }
        else
        {
            cmd.Parameters.Add("@International", 0);
        }
        if (chkOthers.Checked == true)
        {
            cmd.Parameters.Add("@Others", 1);
        }
        else
        {
            cmd.Parameters.Add("@Others", 0);
        }
        cmd.Parameters.Add("@AuthPosition", txtAuthposition.Text.ToString());
        cmd.Parameters.Add("@PrincipalID", Session["hod_ID_Leave1"].ToString());
        cmd.Parameters.Add("@SecondApprovalID", "TMU04127");
        cmd.Parameters.Add("@ThirdApprovalID", "TMU05293");
        cmd.Parameters.Add("@VCID", "TMU08026");
        cmd.Parameters.Add("@Status", 1);
        cmd.Parameters.Add("@OrderNo", SqlDbType.VarChar, 100);
        cmd.Parameters["@OrderNo"].Direction = ParameterDirection.Output;
        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();

        Code = cmd.Parameters["@OrderNo"].Value.ToString();


        con.Close();


        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Your Application No. " + Code + " is Save Successfully'); document.location.href='Research_Incentive.aspx';", true);



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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        String toDate = txtDateTo.Text;
        DateTime date = DateTime.ParseExact(toDate, "dd MMM yyyy", CultureInfo.InvariantCulture);
        string outputDate = date.ToString("yyyy-MM-dd");

        String FromDate = txtDateFrom.Text;
        DateTime date1 = DateTime.ParseExact(FromDate, "dd MMM yyyy", CultureInfo.InvariantCulture);
        string outputDate1 = date1.ToString("yyyy-MM-dd");


        pnlRIApplicationApproval.Visible = false;
        pnlApplicationList.Visible = false;
        pnlRIApproval.Visible = false;
        pnlApplication.Visible = false;
        pnlTitleApplication.Visible = false;
        pnlTitleList.Visible = false;
        pnlTitleApplication.Visible = false;
        divUploadDoc.Visible = false;
        divAttachmentGrid.Visible = false;
        pnlRepport.Visible = true;
        pnlRecoveryReq.Visible = false;
        pnlRecoveryReport.Visible = false;
        pnlRecoveryApproval.Visible = false;
        SqlCommand cmd = new SqlCommand("proc_RIApplicationReportByDate", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@toDate", txtDateTo.Text);
        cmd.Parameters.AddWithValue("@FromDate", txtDateFrom.Text);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            notshow.Visible = false;
            ReportViewer1.Visible = true;
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.Visible = true;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/RIReport.rdlc");
            ReportDataSource datasource = new ReportDataSource("DataSetRI", dt);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
        }
        else
        {
            ReportViewer1.Visible = false;
            Label3.Visible = false;
            notshow.Visible = true;
            notshow.Text = "No data available to display.";
        }
    }
    protected void lnkReApproval_Click(object sender, EventArgs e)
    {
        Session["linkid"] = "lnkReApproval";
        pnlRIApplicationApproval.Visible = true;
        pnlApplicationList.Visible = false;
        pnlRIApproval.Visible = false;
        pnlApplication.Visible = false;
        pnlTitleApplication.Visible = false;
        pnlTitleList.Visible = false;
        pnlTitleApplication.Visible = false;
        divUploadDoc.Visible = false;
        divAttachmentGrid.Visible = false;
        pnlRecoveryReq.Visible = false;
        pnlRecoveryReport.Visible = false;
        pnlRecoveryApproval.Visible = false;
        BindListforReApproval(Session["uid"].ToString());

    }
    public void BindListforReApproval(string UserId)
    {
        SqlCommand cmd = new SqlCommand("proc_RIListforprincipal", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", UserId);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = "";
            GridView1.DataBind();
        }
    }

    protected void lnkAppSelect_Click(object sender, EventArgs e)
    {
        if (Session["uid"].ToString() == "TMU04127")
        {
            txtApproveAmount.Enabled = true;
        }
        else
        {
            txtApproveAmount.Enabled = false;
        }
        string Complaint = (sender as LinkButton).CommandArgument;
        hdfApplicationNo.Value = Complaint;
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("RI_GetApplicationByNo '" + Complaint + "','" + Session["uid"].ToString() + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {


            if (dt.Rows[0]["PrincipalStatus"].ToString() == "")
            {
                btnSubmitTitleApplication.Visible = true;
                btnUploadDoc.Visible = true;

            }
            else
            {
                btnSubmitTitleApplication.Visible = false;
                btnUploadDoc.Visible = false;
            }
            if (Session["uid"].ToString() == dt.Rows[0]["SecondApprovalID"].ToString() && dt.Rows[0]["SecondApprovalStatus"].ToString() == "0")
            {

                btnHold.Visible = true;

                btnApproveTitleApplication.Visible = true;
                btnRejectTitleApplication.Visible = true;

            }
            else if (Session["uid"].ToString() == dt.Rows[0]["ThirdApprovalID"].ToString() && dt.Rows[0]["ThirdApprovalStatus"].ToString() == "0")
            {
                btnApproveTitleApplication.Visible = true;
                btnRejectTitleApplication.Visible = true;

            }
            else if (Session["uid"].ToString() == dt.Rows[0]["VCID"].ToString() && dt.Rows[0]["VCApprovalStatus"].ToString() == "0")
            {
                btnApproveTitleApplication.Visible = true;
                btnRejectTitleApplication.Visible = true;
            }
            else if (Session["uid"].ToString() == dt.Rows[0]["PrincipalID"].ToString() && dt.Rows[0]["PrincipalStatus"].ToString() == "Pending")
            {
                btnApproveTitleApplication.Visible = true;
                btnRejectTitleApplication.Visible = true;
            }
            else
            {

                btnApproveTitleApplication.Visible = false;
                btnRejectTitleApplication.Visible = false;
                btnHold.Visible = false;
            }






            //if (dt.Rows[0]["PrincipalStatus"].ToString() == "Approve" && Session["uid"].ToString() != dt.Rows[0]["VCID"].ToString())
            //{
            //    btnApproveTitleApplication.Visible = false;
            //    btnRejectTitleApplication.Visible = false;
            //}
            //else
            //{
            //    btnApproveTitleApplication.Visible = true;
            //    btnRejectTitleApplication.Visible = true;

            //}


            txtApplicant.Text = dt.Rows[0]["applicant_name"].ToString();
            txtDesignation.Text = dt.Rows[0]["designation"].ToString();
            txtCollege.Text = dt.Rows[0]["CollegeName"].ToString();
            bindTitlebyApp(hdfApplicationNo.Value);
            drpAppTitle.Enabled = false;
            txtApproveAmount.Text = dt.Rows[0]["ApprovedAmount"].ToString();
            pnlRIApplicationApproval.Visible = false;
            btnSaveTitleApplication.Visible = false;
            divUploadDoc.Visible = true;
            pnlApplicationList.Visible = false;
            pnlApplication.Visible = true;
            drpAppTitle.SelectedValue = dt.Rows[0]["TitleID"].ToString();
            txtWeblink.Text = dt.Rows[0]["WebLink"].ToString();
            txtWeblink.Enabled = false;
            txtDOPub.Text = dt.Rows[0]["DOP"].ToString();
            txtDep.Text = dt.Rows[0]["Department Name"].ToString();
            txtISSN.Text = dt.Rows[0]["ISSN"].ToString();
            txtAuthposition.Text = dt.Rows[0]["AuthPosition"].ToString();
            txtNameofJournal1.Text = dt.Rows[0]["Name_of_journal"].ToString();
            txtVolume1.Text = dt.Rows[0]["Volume"].ToString();
            txtNoIssue1.Text = dt.Rows[0]["No_Issue"].ToString();
            txtPageno1.Text = dt.Rows[0]["Page_No"].ToString();
            txtAuthposition.Enabled = false;
            txtISSN.Enabled = false;
            hdfAuth.Value = dt.Rows[0]["No_of_author"].ToString();
            chkBookSubmit.Checked = Convert.ToBoolean(dt.Rows[0]["Book_Submit_RD"]);
            chkBookSubmit.Enabled = false;

            chkScopus1.Checked = Convert.ToBoolean(dt.Rows[0]["SCOPUSQ1"]);
            chkScopus1.Enabled = false;
            chkScopus2.Checked = Convert.ToBoolean(dt.Rows[0]["SCOPUSQ2"]);
            chkScopus2.Enabled = false;
            chkScopus3.Checked = Convert.ToBoolean(dt.Rows[0]["SCOPUSQ3"]);
            chkScopus3.Enabled = false;
            chkScopus4.Checked = Convert.ToBoolean(dt.Rows[0]["SCOPUSQ4"]);
            chkScopus4.Enabled = false;
            chkwos.Checked = Convert.ToBoolean(dt.Rows[0]["WOS"]);
            chkwos.Enabled = false;
            chkSCI.Checked = Convert.ToBoolean(dt.Rows[0]["SCI"]);
            chkSCI.Enabled = false;
            chkSSCI.Checked = Convert.ToBoolean(dt.Rows[0]["SSCI"]);
            chkSSCI.Enabled = false;
            chkUGC.Checked = Convert.ToBoolean(dt.Rows[0]["UGC_Core"]);
            chkUGC.Enabled = false;
            chkICI.Checked = Convert.ToBoolean(dt.Rows[0]["ICI"]);
            chkICI.Enabled = false;
            chkabdc.Checked = Convert.ToBoolean(dt.Rows[0]["ABDC"]);
            chkabdc.Enabled = false;
            chkNAAS.Checked = Convert.ToBoolean(dt.Rows[0]["NAAS"]);
            chkNAAS.Enabled = false;

            chkWOSSSCI.Checked = Convert.ToBoolean(dt.Rows[0]["WOS-SSCI"]);
            chkWOSSSCI.Enabled = false;
            chkWOSSCIE.Checked = Convert.ToBoolean(dt.Rows[0]["WOS-SCIE"]);
            chkWOSSCIE.Enabled = false;
            chkWOSESCI.Checked = Convert.ToBoolean(dt.Rows[0]["WOS-ESCI"]);
            chkWOSESCI.Enabled = false;
            chkWOSAHCI.Checked = Convert.ToBoolean(dt.Rows[0]["WOS-AHCI"]);
            chkWOSAHCI.Enabled = false;
            chkNATIONALPublisher.Checked = Convert.ToBoolean(dt.Rows[0]["NATIONAL Publisher"]);
            chkNATIONALPublisher.Enabled = false;
            chkInternational.Checked = Convert.ToBoolean(dt.Rows[0]["International Publisher"]);
            chkInternational.Enabled = false;
            chkOthers.Checked = Convert.ToBoolean(dt.Rows[0]["OTHERS"]);
            chkOthers.Enabled = false;

            //for (int i = 0; i < Convert.ToInt32(dt.Rows[0]["No_of_author"]); i++)
            //{
            //    HtmlGenericControl div = new HtmlGenericControl("div");
            //    div.ID = "div" + i;
            //    if (i == 3)
            //    {
            //        div.Attributes.Add("class", "col-sm-2 p-0");
            //    }
            //    else
            //    {
            //        div.Attributes.Add("class", "col-sm-3 p-0");
            //    }
            //    int auth = i + 1;
            //    Label l = new Label();
            //    l.Attributes.Add("class", "col-form-label");
            //    l.Text = "Author " + auth;
            //    TextBox t = new TextBox();
            //    t.Attributes.Add("CssClass", "form-control");
            //    t.Attributes.Add("runat", "server");
            //    t.Width = 200;
            //    t.ID = "txtAuthor" + i;
            //    t.Text = dt.Rows[0]["Auth" + auth].ToString();
            //    t.Enabled = false;
            //    RequiredFieldValidator reqFieldVal = new RequiredFieldValidator();
            //    reqFieldVal.ID = "validator_" + i;
            //    reqFieldVal.ControlToValidate = t.ID;
            //    reqFieldVal.ForeColor = System.Drawing.Color.Red;
            //    reqFieldVal.ErrorMessage = "**";
            //    reqFieldVal.Display = ValidatorDisplay.Dynamic;
            //    reqFieldVal.ValidationGroup = "R1";
            //    div.Controls.Add(l);
            //    div.Controls.Add(reqFieldVal);
            //    div.Controls.Add(t);
            //    divAuthor.Controls.Add(div);
            //}
            bindAttachment();
        }
        else
        {
            //btnSubmit1.Visible = false;
            //btnSave1.Visible = true;
            //divUploadDoc.Visible = false;

            btnSubmitTitleApplication.Visible = false;
            btnSaveTitleApplication.Visible = true;
            divUploadDoc.Visible = false;

        }
    }
    public void bindTitlebyApp(string AppNo)
    {


        SqlCommand cmd = new SqlCommand("proc_fetchTitlebyApp", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@AppNo", AppNo);

        if (con.State == ConnectionState.Closed)
            con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            drpAppTitle.DataSource = dt;
            drpAppTitle.DataTextField = "Title_of_paper";
            drpAppTitle.DataValueField = "id";
            drpAppTitle.DataBind();
        }






    }

    protected void btnApproveTitleApplication_Click(object sender, EventArgs e)
    {
        SqlCommand cmd1 = new SqlCommand("proc_fetchHoldStatus", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.AddWithValue("@AppNo", hdfApplicationNo.Value);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["Hold"].ToString() == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('Application is Hold,Please Unhold First.'); ", true);
                return;
            }
        }
        SqlCommand cmd = new SqlCommand("[SP_RIApproval]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ComplaintNo", hdfApplicationNo.Value);
        cmd.Parameters.Add("@UseId", Session["uid"].ToString());
        cmd.Parameters.Add("@ApprovedAmount", txtApproveAmount.Text);


        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();
        con.Close();


        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Application No. " + hdfApplicationNo.Value + " is Update Successfully'); document.location.href='Research_Incentive.aspx';", true);







    }

    protected void btnRejectPop_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("[SP_RIReject]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ComplaintNo", hdfApplicationNo.Value);
        cmd.Parameters.Add("@UseId", Session["uid"].ToString());
        cmd.Parameters.Add("@Remark", txtRejectRemark.Text);
        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        //lblmsg.Text= "Application Reject Successfully";
        //lblmsg.ForeColor = Color.Red;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('Application Reject Successfully');", true);

    }



    protected void btnApproveTitle_Click(object sender, EventArgs e)
    {

        SqlCommand cmd = new SqlCommand("[SP_RITitleApproval]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ComplaintNo", hdfTitleofPaper.Value);
        cmd.Parameters.Add("@UseId", Session["uid"].ToString());

        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();
        con.Close();


        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Title Approve Successfully'); document.location.href='Research_Incentive.aspx';", true);






    }

    protected void btnRejectTitle_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("[SP_RITitleReject]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ComplaintNo", hdfTitleofPaper.Value);
        cmd.Parameters.Add("@UseId", Session["uid"].ToString());
        cmd.Parameters.Add("@RejectionRemark", txtTitleRejectMark.Text);


        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();
        con.Close();


        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Title Reject Successfully'); document.location.href='Research_Incentive.aspx';", true);



    }
    protected void lnkReport_Click(object sender, EventArgs e)
    {
        Session["linkid"] = "lnkReport";
        pnlRIApplicationApproval.Visible = false;
        pnlApplicationList.Visible = false;
        pnlRIApproval.Visible = false;
        pnlApplication.Visible = false;
        pnlTitleApplication.Visible = false;
        pnlTitleList.Visible = false;
        pnlTitleApplication.Visible = false;
        divUploadDoc.Visible = false;
        divAttachmentGrid.Visible = false;
        pnlRepport.Visible = true;
        pnlRecoveryReq.Visible = false;
        pnlRecoveryReport.Visible = false;
        pnlRecoveryApproval.Visible = false;
        //SqlCommand cmd = new SqlCommand("proc_RIApplicationReport", con);
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        //con.Open();

        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //con.Close();
        //da.Fill(dt);
        //if (dt.Rows.Count > 0)
        //{
        //    ReportViewer1.Visible = true;
        //    ReportViewer1.ProcessingMode = ProcessingMode.Local;
        //    ReportViewer1.Visible = true;
        //    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/RIReport.rdlc");
        //    ReportDataSource datasource = new ReportDataSource("DataSetRI", dt);
        //    ReportViewer1.LocalReport.DataSources.Clear();
        //    ReportViewer1.LocalReport.DataSources.Add(datasource);
        //}

    }

    protected void lnkAnnexure_Click(object sender, EventArgs e)
    {

        try
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            wc.OpenRead(Server.MapPath("~/AllForm16/TMU_Undertaking_Annexure-II.pdf"));
            Int64 bytes_total = Convert.ToInt64(wc.ResponseHeaders["Content-Length"]);
            Response.ContentType = "Application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=TMU_Undertaking_Annexure-II.pdf");
            Response.TransmitFile(Server.MapPath("~/AllForm16/TMU_Undertaking_Annexure-II.pdf"));
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



    protected void drpResearchIncentive_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpResearchIncentive.SelectedValue == "1")
        {
            lNameOfJournal.Text = "Name of the Journal";
            lblTitlepaper.Text = "Title of the Paper";
            txtNoIssue.Enabled = true;
            txtISSNno.Enabled = true;
            lblISBN.Text = " ISSN no.";

        }
        if (drpResearchIncentive.SelectedValue == "2")
        {

            lNameOfJournal.Text = "Application No";
            //lblTitlepaper.Text = "Name of the Book/ Publisher";
            lblTitlepaper.Text = "Title";
            txtNoIssue.Text = "";
            txtISSNno.Text = "";
            txtNoIssue.Enabled = false;
            txtISSNno.Enabled = false;

        }
        if (Convert.ToInt32(drpResearchIncentive.SelectedValue) > 2)
        {
            lNameOfJournal.Text = "Name of the Book/Publisher";
            lblTitlepaper.Text = "Title";

            lblISBN.Text = "ISBN no.";
            //lblTitlepaper.Text = "Application No";
            txtNoIssue.Enabled = true;
            txtISSNno.Enabled = true;
        }
    }

    protected void btnHold1_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("[SP_RIHold]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ComplaintNo", hdfApplicationNo.Value);
        cmd.Parameters.Add("@UseId", Session["uid"].ToString());
        cmd.Parameters.Add("@Remark", txtHoldRemark.Text);
        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('Application Hold Successfully');", true);
    }



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string dataKeyValue = GridView1.DataKeys[e.Row.RowIndex].Values[1].ToString();

            if (dataKeyValue == "1")
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string dataKeyValue = GridView2.DataKeys[e.Row.RowIndex].Values[1].ToString();

            if (dataKeyValue == "1")
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }
        }
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string dataKeyValue = GridView3.DataKeys[e.Row.RowIndex].Values[1].ToString();

            if (dataKeyValue == "1")
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }
        }
    }
    protected void btnUnhold_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("[SP_UnRIHold]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ComplaintNo", hdfApplicationNo.Value);
        cmd.Parameters.Add("@UseId", Session["uid"].ToString());
        cmd.Parameters.Add("@Remark", txtHoldRemark.Text);
        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('Application UnHold Successfully');", true);
    }
    public void BindListforRecovery(string UserId)
    {
        SqlCommand cmd = new SqlCommand("proc_RIforRecovery", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", UserId);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        
        if (dt.Rows.Count > 0)
        {
            if (UserId == "TMU05293")
            {
                GridView2.DataSource = dt;
                GridView2.DataBind();
            }
            else
            {
                GridView3.DataSource = dt;
                GridView3.DataBind();
            }
        }
        else
        {
            if (UserId == "TMU05293")
            {
                GridView2.DataSource = "";
                GridView2.DataBind();
            }
            else
            {
                GridView3.DataSource = "";
                GridView3.DataBind();
            }
        }
    }
    protected void lnkRecovery_Click(object sender, EventArgs e)
    {
        Session["linkid"] = "lnkRecovery";
        pnlRIApplicationApproval.Visible = false;
        pnlApplicationList.Visible = false;
        pnlRIApproval.Visible = false;
        pnlApplication.Visible = false;
        pnlTitleApplication.Visible = false;
        pnlTitleList.Visible = false;
        pnlTitleApplication.Visible = false;
        divUploadDoc.Visible = false;
        divAttachmentGrid.Visible = false;
        pnlRepport.Visible = false;
        pnlRecoveryReq.Visible = true;
        pnlRecoveryReport.Visible = false;
        pnlRecoveryApproval.Visible = false;
        BindListforRecovery(Session["uid"].ToString());





    }

    protected void lnkRecoveryReport_Click(object sender, EventArgs e)
    {
        Session["linkid"] = "lnkRecoveryReport";
        pnlRIApplicationApproval.Visible = false;
        pnlApplicationList.Visible = false;
        pnlRIApproval.Visible = false;
        pnlApplication.Visible = false;
        pnlTitleApplication.Visible = false;
        pnlTitleList.Visible = false;
        pnlTitleApplication.Visible = false;
        divUploadDoc.Visible = false;
        divAttachmentGrid.Visible = false;
        pnlRepport.Visible = false;
        pnlRecoveryReq.Visible = false;
        pnlRecoveryReport.Visible = true;
        pnlRecoveryApproval.Visible = false;


    }

    protected void lnkRecoveryRequest_Click(object sender, EventArgs e)
    {
        string AssignmentNo = (sender as LinkButton).CommandArgument;
        SqlCommand cmd = new SqlCommand("[proc_CalculateAmountforRecovery]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Id", AssignmentNo);
        hdfTitleofPaper.Value = AssignmentNo;
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtAppNo.Text = AssignmentNo;
            lblAmount.Text = dt.Rows[0]["Amount"].ToString();
            txtRemark.Text = "";
        }

        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#RecoveryModel1').modal('show');</script>", false);
    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        try
        {
            string filename = Path.GetFileName(flrecovery.PostedFile.FileName);
            string contentType = flrecovery.PostedFile.ContentType;

            using (Stream fs = flrecovery.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
                    {
                        SqlCommand cmd = new SqlCommand("proc_InsertRIRecoveryRequest", con2);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ApplicationNo", txtAppNo.Text);
                        cmd.Parameters.Add("@Amount", lblAmount.Text);
                        cmd.Parameters.Add("@Remark", txtRemark.Text);
                        cmd.Parameters.Add("@UserId", Session["uid"].ToString());
                        cmd.Parameters.Add("@Attachment", bytes);
                        cmd.Parameters.Add("@AttachmentType", contentType);
                        cmd.Parameters.Add("@FileName", filename);

                        if (con2.State == ConnectionState.Closed)
                            con2.Open();
                        cmd.ExecuteNonQuery();
                        con2.Close();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("UNIQUE KEY"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('You have already submitted a request for this application.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('Some thing went wrong.');", true);
            }
        }


        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#RecoveryModel1').modal('show');</script>", false);

    }

    protected void lnkRecoveryApproval_Click(object sender, EventArgs e)
    {
        Session["linkid"] = "lnkRecoveryApproval";
        pnlRIApplicationApproval.Visible = false;
        pnlApplicationList.Visible = false;
        pnlRIApproval.Visible = false;
        pnlApplication.Visible = false;
        pnlTitleApplication.Visible = false;
        pnlTitleList.Visible = false;
        pnlTitleApplication.Visible = false;
        divUploadDoc.Visible = false;
        divAttachmentGrid.Visible = false;
        pnlRepport.Visible = false;
        pnlRecoveryReq.Visible = false;
        pnlRecoveryReport.Visible = false;
        pnlRecoveryApproval.Visible = true;
        BindListforRecovery(Session["uid"].ToString());
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("proc_RIRecoveryReport", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@toDate", TextBox1.Text);
        cmd.Parameters.AddWithValue("@FromDate", TextBox2.Text);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            notshow.Visible = false;
            ReportViewer2.Visible = true;
            ReportViewer2.ProcessingMode = ProcessingMode.Local;
            ReportViewer2.Visible = true;
            ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/Report/RIRECOVERYReport.rdlc");
            ReportDataSource datasource = new ReportDataSource("DataSetRI", dt);
            ReportViewer2.LocalReport.DataSources.Clear();
            ReportViewer2.LocalReport.DataSources.Add(datasource);
        }
        else
        {
            ReportViewer2.Visible = false;
            Label3.Visible = false;
            notshow.Visible = true;
            notshow.Text = "No data available to display.";
        }
    }

    protected void lnkRecoveryRequestApproval_Click(object sender, EventArgs e)
    {
        string AssignmentNo = (sender as LinkButton).CommandArgument;
        SqlCommand cmd = new SqlCommand("[proc_RecoveryRequest]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Id", AssignmentNo);
        hdfTitleofPaper.Value = AssignmentNo;
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            TextBox3.Text = AssignmentNo;
            Label14.Text = dt.Rows[0]["Amount"].ToString();
            TextBox4.Text = dt.Rows[0]["Remark"].ToString();
        }

        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#RecoveryModel2').modal('show');</script>", false);
    }

    protected void lnkAttachment_Click(object sender, EventArgs e)
    {
        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select Attachment,[Content Type],[File Name] from tbl_RIRecoveryRequest where ApplicationNo='" + TextBox3.Text + "' ";
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

    protected void Button8_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("SP_RIRecoveryUpdate", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@RIID", TextBox3.Text);
        cmd.Parameters.Add("@Remarks", TextBox5.Text);
        cmd.Parameters.Add("@Status", 2);
        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        SqlDataAdapter da1 = new SqlDataAdapter("select applicant_name,(Select Title_of_paper from tbl_ResearchIncentive_Title where id=Application_Title) Title,(Select Name_of_journal from tbl_ResearchIncentive_Title where id=Application_Title) Journal,YEAR(convert(date,DOP,105)) 'Year',ApprovedAmount,(Select [Company E-Mail] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=applicant_code)  CompanyEmail from tbl_Research_Incentive where Application_No_='" + TextBox3.Text + "'", con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);

        if (dt1.Rows.Count > 0)
        {

            string applicantName = dt1.Rows[0]["applicant_name"].ToString();

            string htmlBody = "<html><body style='font-family:Arial; font-size:14px; color:#333;'>    <p>Dear " + applicantName + ",</p>    <p>Your research paper titled <strong>"+ dt1.Rows[0]["Title"].ToString() + "</strong>, published in the Journal <strong>"+ dt1.Rows[0]["Journal"].ToString() + "</strong> in the year <strong>"+ dt1.Rows[0]["Year"].ToString() + "</strong>, had received a research incentive of Rs. <strong>"+ dt1.Rows[0]["ApprovedAmount"].ToString() + "</strong>.    </p>    <p> As this journal is no longer indexed in the specified database, and in accordance with <strong>Circular No TMU/R.O./2025-26/ADM/44</strong>, you are requested to refund <strong>60%</strong> of the incentive amount paid to you, i.e., Rs. <strong>"+ Label14.Text + "</strong>, to the University.    </p>    <br/>    <p>Regards,<br/>    Teerthanker Mahaveer University</p></body></html>";

            bool emailSent = SendEmail(dt1.Rows[0]["CompanyEmail"].ToString(), "Reserach Incentive Recovery", htmlBody);
            if (emailSent)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('Application approve successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('Error sending Mail. Please try again..');", true);

            }

        }
    }

    protected void Button9_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("SP_RIRecoveryUpdate", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@RIID", TextBox3.Text);
        cmd.Parameters.Add("@Remarks", TextBox5.Text);
        cmd.Parameters.Add("@Status", 3);
        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('Application reject successfully.');", true);
    }

    private bool SendEmail(string toEmail, string subject, string body)
    {
        try
        {
            var fromEmail = "naverp@tmu.ac.in";
            var fromPassword = "nwar yzam bcez rqop";

            // Initialize the SMTP client with the server details
            var smtpClient = new SmtpClient("smtp.gmail.com") // Use your SMTP server here
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, fromPassword),
                EnableSsl = true,
            };

            // Create the MailMessage object
            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = body,

                IsBodyHtml = true, // Set to true if the body contains HTML
            };
            mailMessage.To.Add(toEmail); // Add recipient
            mailMessage.CC.Add("vicechancellor@tmu.ac.in");
            mailMessage.CC.Add("hr@tmu.ac.in");
            mailMessage.CC.Add("dir.acct@tmu.ac.in");
            mailMessage.Bcc.Add("osd.vc@tmu.ac.in");
            // Send the email
            smtpClient.Send(mailMessage);
            return true; // Email sent successfully
        }
        catch (Exception ex)
        {
            // Log the exception (ex) if necessary
            return false; // Email sending failed
        }
    }

}