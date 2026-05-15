
using Microsoft.Reporting.WebForms;
using System;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_Honorarium_E_Visit : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                
                calculateAmt(drpDesignation.SelectedValue, drpMode.SelectedValue);
                BindList(Session["uid"].ToString());
                BindTaskList();
                BindCollege();               

            }
            catch (Exception ex)
            {
                Response.Redirect("../Default.aspx");
            }
        }
    }
    public void BindList(string UserId)
    {
        SqlCommand cmd = new SqlCommand("[SP_GetHonorarium]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", UserId);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            grdApplicationList.DataSource = dt;
            grdApplicationList.DataBind();
        }
    }

    public void BindTaskList()
    {
        SqlCommand cmd = new SqlCommand("[SP_GetTaskListHonorarium]", con);
        cmd.CommandType = CommandType.StoredProcedure;

        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            drpAppType.DataSource = dt;
            drpAppType.DataTextField = "Desc";
            drpAppType.DataValueField = "ID";
            drpAppType.DataBind();
        }
    }

    public void BindCollege()
    {
        SqlCommand cmd = new SqlCommand("select 'zz' Code, 'Other Specify' as Name union select Code,Name from [EDUCOLLEGELIVE-R2].dbo.[TMU$Dimension Value]  where College = 1", con);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            drpcollege.DataSource = dt;
            drpcollege.DataTextField = "Name";
            drpcollege.DataValueField = "Code";
            drpcollege.DataBind();
        }
    }







    public void BindListForApproval(string UserId)
    {
        SqlCommand cmd = new SqlCommand("[SP_GetHonorariumforApptoval]", con);
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
    }
    public void enabledisabe()
    {
        if (chkHonorarium.Checked == true)
        {
            txtHonoAMT.Enabled = true;
            txttravelAll.Enabled = true;

            txtACHolderName.Enabled = true;
            txtACCNumber.Enabled = true;
            txtBankName.Enabled = true;
            txtBranch.Enabled = true;
            txtIFSC.Enabled = true;
            flSupportDoc.Enabled = true;
            ddlAttachtype.Enabled = true;
            FileUpload1.Enabled = true;
        }
        else
        {
            txtHonoAMT.Enabled = false;
            txttravelAll.Enabled = false;

            txtACHolderName.Enabled = false;
            txtACCNumber.Enabled = false;
            txtBankName.Enabled = false;
            txtBranch.Enabled = false;
            txtIFSC.Enabled = false;
            flSupportDoc.Enabled = false;
            ddlAttachtype.Enabled = false;
            FileUpload1.Enabled = false;
        }
        if (chkOther.Checked == true)
        {
            txtOAmount.Enabled = true;
            txtOAccountHolder.Enabled = true;
            txtOAcNo.Enabled = true;
            txtOBankName.Enabled = true;
            txtOBranch.Enabled = true;
            txtOIfsc.Enabled = true;
            flApprovalCopy.Enabled = true;
        }
        else
        {
            txtOAmount.Enabled = false;
            txtOAccountHolder.Enabled = false;
            txtOAcNo.Enabled = false;
            txtOBankName.Enabled = false;
            txtOBranch.Enabled = false;
            txtOIfsc.Enabled = false;
            flApprovalCopy.Enabled = false;
        }
    }
    public void calculateAmt(string des, string mode)
    {


        SqlCommand cmd = new SqlCommand("proc_CRCAMTCalculate", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@des", des);
        cmd.Parameters.AddWithValue("@mode", mode);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtHonoAMT.Text = dt.Rows[0]["Amount"].ToString();
        }
        enabledisabe();
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("Honorarium_E_Visit.aspx");
    }

    protected void lnkApplication_Click(object sender, EventArgs e)
    {
        pnlApplicationList.Visible = true;
        pnlApplicationApproval.Visible = false;
        pnlRepport.Visible = false;

    }

    protected void lnkApproval_Click(object sender, EventArgs e)
    {
        pnlApplication.Visible = false;
        pnlApplicationList.Visible = false;
        pnlApplicationApproval.Visible = true;
        pnlRepport.Visible = false;
        BindListForApproval(Session["uid"].ToString());

    }
    protected void lnkReport_Click(object sender, EventArgs e)
    {
        pnlApplication.Visible = false;
        pnlApplicationList.Visible = false;
        pnlApplicationApproval.Visible = false;
        grdApplicationApproval.Visible = false;
        pnlRepport.Visible = true;
        SqlCommand cmd = new SqlCommand("proc_HOApplicationReport", con);
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
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/HonorReport.rdlc");
            ReportDataSource datasource = new ReportDataSource("DataSet_Result", dt);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
        }




    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        //if (Session["uid"].ToString() == "TMU03651")
        //{
        //    //drpAppType.SelectedValue = "AC";
        //    //drpAppType.Enabled = false;
        //    ListItem removeItem1 = drpAppType.Items.FindByValue("CRC");
        //    drpAppType.Items.Remove(removeItem1);
        //    ListItem removeItem2 = drpAppType.Items.FindByValue("BoS");
        //    drpAppType.Items.Remove(removeItem2);
        //    ListItem removeItem3 = drpAppType.Items.FindByValue("LTS");
        //    drpAppType.Items.Remove(removeItem3);
        //    ListItem removeItem4 = drpAppType.Items.FindByValue("IIC");
        //    drpAppType.Items.Remove(removeItem4);
        //}
        //else if (Session["uid"].ToString() == "TMU00525")
        //{
        //    ListItem removeItem1 = drpAppType.Items.FindByValue("CRC");
        //    drpAppType.Items.Remove(removeItem1);
        //    ListItem removeItem2 = drpAppType.Items.FindByValue("BoS");
        //    drpAppType.Items.Remove(removeItem2);
        //    ListItem removeItem3 = drpAppType.Items.FindByValue("AC");
        //    drpAppType.Items.Remove(removeItem3);
        //    ListItem removeItem4 = drpAppType.Items.FindByValue("EC");
        //    drpAppType.Items.Remove(removeItem4);
        //    ListItem removeItem5 = drpAppType.Items.FindByValue("IIC");
        //    drpAppType.Items.Remove(removeItem5);

        //}

        //else if (Session["uid"].ToString() == "TMU00260")
        //{
        //    ListItem removeItem1 = drpAppType.Items.FindByValue("CRC");
        //    drpAppType.Items.Remove(removeItem1);
        //    ListItem removeItem2 = drpAppType.Items.FindByValue("BoS");
        //    drpAppType.Items.Remove(removeItem2);
        //    ListItem removeItem3 = drpAppType.Items.FindByValue("AC");
        //    drpAppType.Items.Remove(removeItem3);
        //    ListItem removeItem4 = drpAppType.Items.FindByValue("EC");
        //    drpAppType.Items.Remove(removeItem4);
        //    ListItem removeItem5 = drpAppType.Items.FindByValue("LTS");
        //    drpAppType.Items.Remove(removeItem5);

        //}


        //else if (Session["uid"].ToString() == "TMU05294")
        //{
        //    ListItem removeItem1 = drpAppType.Items.FindByValue("CRC");
        //    drpAppType.Items.Remove(removeItem1);
        //    ListItem removeItem2 = drpAppType.Items.FindByValue("BoS");
        //    drpAppType.Items.Remove(removeItem2);
        //    ListItem removeItem3 = drpAppType.Items.FindByValue("AC");
        //    drpAppType.Items.Remove(removeItem3);
        //    ListItem removeItem4 = drpAppType.Items.FindByValue("EC");
        //    drpAppType.Items.Remove(removeItem4);
        //    ListItem removeItem5 = drpAppType.Items.FindByValue("LTS");
        //    drpAppType.Items.Remove(removeItem5);
        //    ListItem removeItem6 = drpAppType.Items.FindByValue("IIC");
        //    drpAppType.Items.Remove(removeItem6);

        //}
        //else
        //{
        //    ListItem removeItem1 = drpAppType.Items.FindByValue("AC");
        //    drpAppType.Items.Remove(removeItem1);
        //    ListItem removeItem2 = drpAppType.Items.FindByValue("EC");
        //    drpAppType.Items.Remove(removeItem2);
        //    ListItem removeItem3 = drpAppType.Items.FindByValue("LTS");
        //    drpAppType.Items.Remove(removeItem3);
        //    ListItem removeItem6 = drpAppType.Items.FindByValue("IIC");
        //    drpAppType.Items.Remove(removeItem6);

        //}
        pnlApplicationList.Visible = false;
        pnlApplication.Visible = true;
        pnlRepport.Visible = false;




    }

    protected void lnkAnnexure_Click(object sender, EventArgs e)
    {

    }

    protected void btnSubmitApplication_Click(object sender, EventArgs e)
    {
        string Code = "";
        string Supportfilename = "";
        string SupportcontentType = "";
        byte[] bytesSupport = new byte[0];

        string OSupportfilename = "";
        string OSupportcontentType = "";
        byte[] ObytesSupport = new byte[0];

        string FileUpload1name = "";
        string FileUpload1SupportcontentType = "";
        byte[] FileUpload1bytesSupport = new byte[0];


        if (drpcollege.SelectedValue == "zz" && txtUnitName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Fill Unit Name.');", true);
            return;
        }

        if (drpAppType.SelectedValue == "Others-Specify" && txteventType.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Enter Event Type.');", true);
            return;
        }



        if (chkHonorarium.Checked == true)
        {
            if (txtHonoAMT.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Fill Honorarium Amount.');", true);
                enabledisabe();
                return;
            }
            if (txttravelAll.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Fill Travel Allowance.');", true);
                enabledisabe();
                return;
            }
            if (txtACHolderName.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Fill Account Holder Name.');", true);
                enabledisabe();
                return;
            }
            if (txtACCNumber.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Fill Account Number.');", true);
                enabledisabe();
                return;
            }
            if (txtBankName.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Fill Bank Name.');", true);
                enabledisabe();
                return;
            }
            if (txtBranch.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Fill  Branch.');", true);
                enabledisabe();
                return;
            }
            if (txtIFSC.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Fill IFSC Code.');", true);
                enabledisabe();
                return;
            }
            if (!FileUpload1.HasFile)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select Attachment to Upload.');", true);
                enabledisabe();
                return;
            }
            else
            {
                if (FileUpload1.HasFile)
                {
                    string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string contentType = FileUpload1.PostedFile.ContentType;
                    FileUpload1bytesSupport = new byte[0];
                    using (Stream fs = FileUpload1.PostedFile.InputStream)
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            FileUpload1name = Path.GetFileName(FileUpload1.PostedFile.FileName);
                            FileUpload1SupportcontentType = FileUpload1.PostedFile.ContentType;
                            Stream fsSupport = FileUpload1.PostedFile.InputStream;
                            BinaryReader brsupport = new BinaryReader(fsSupport);
                            FileUpload1bytesSupport = brsupport.ReadBytes((Int32)fsSupport.Length);
                        }
                    }
                }

            }
            if (flSupportDoc.HasFile)
            {
                string filename = Path.GetFileName(flSupportDoc.PostedFile.FileName);
                string contentType = flSupportDoc.PostedFile.ContentType;
                FileUpload1bytesSupport = new byte[0];
                using (Stream fs = flSupportDoc.PostedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        Supportfilename = Path.GetFileName(flSupportDoc.PostedFile.FileName);
                        SupportcontentType = flSupportDoc.PostedFile.ContentType;
                        Stream fsSupport = flSupportDoc.PostedFile.InputStream;
                        BinaryReader brsupport = new BinaryReader(fsSupport);
                        bytesSupport = brsupport.ReadBytes((Int32)fsSupport.Length);
                    }
                }
            }
            


        }
        if (chkOther.Checked == true)
        {
            if (txtOAmount.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter other expense amount.');", true);
                enabledisabe();
                return;
            }
            if (txtOAccountHolder.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter other expense account holder name.');", true);
                enabledisabe();
                return;
            }
            if (txtOAcNo.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter other expense account no.');", true);
                enabledisabe();
                return;
            }
            if (txtOBankName.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter other expense bank name.');", true);
                enabledisabe();
                return;
            }
            if (txtOBranch.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter other expense branch.');", true);
                enabledisabe();
                return;
            }
            if (txtOIfsc.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter other expense ifsc code.');", true);
                enabledisabe();
                return;
            }
            if (!flApprovalCopy.HasFile)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload Expense Copy.');", true);
                enabledisabe();
                return;
            }
            else
            {
                if (flApprovalCopy.HasFile)
                {
                    string filename = Path.GetFileName(flApprovalCopy.PostedFile.FileName);
                    string contentType = flApprovalCopy.PostedFile.ContentType;
                    ObytesSupport = new byte[0];
                    using (Stream fs = flApprovalCopy.PostedFile.InputStream)
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            OSupportfilename = Path.GetFileName(flApprovalCopy.PostedFile.FileName);
                            OSupportcontentType = flApprovalCopy.PostedFile.ContentType;
                            Stream fsSupport = flApprovalCopy.PostedFile.InputStream;
                            BinaryReader brsupport = new BinaryReader(fsSupport);
                            ObytesSupport = brsupport.ReadBytes((Int32)fsSupport.Length);
                        }
                    }
                }
            }
        }


        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            SqlCommand cmd = new SqlCommand("SP_InsertHonorariumV2", con2);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@App_For", drpAppType.SelectedValue);
            cmd.Parameters.Add("@Name", txtApplicant.Text);
            cmd.Parameters.Add("@Designation", drpDesignation.SelectedValue);
            cmd.Parameters.Add("@Organisation", txtOrganization.Text);
            cmd.Parameters.Add("@Date", txtDate.Text);
            cmd.Parameters.Add("@MOV", drpMode.SelectedValue);
            cmd.Parameters.Add("@HAAmount", txtHonoAMT.Text);
            cmd.Parameters.Add("@T_Allow", txttravelAll.Text);
            cmd.Parameters.Add("@T_Amount", txttravelAMT.Text);
            cmd.Parameters.Add("@ACC_Name", txtACHolderName.Text);
            cmd.Parameters.Add("@ACC_No", txtACCNumber.Text);
            cmd.Parameters.Add("@Bank", txtBankName.Text);
            cmd.Parameters.Add("@Branch", txtBranch.Text);
            cmd.Parameters.Add("@IFSC", txtIFSC.Text);
            cmd.Parameters.Add("@eventType", txteventType.Text);
            cmd.Parameters.Add("@Ann_Attachment", FileUpload1bytesSupport);
            cmd.Parameters.Add("@Ann_ContentType", FileUpload1SupportcontentType);
            cmd.Parameters.Add("@FileName", FileUpload1name);
            cmd.Parameters.Add("@College", drpcollege.SelectedValue);
            cmd.Parameters.Add("@Support_Attachment", bytesSupport);
            cmd.Parameters.Add("@Support_ContentType", SupportcontentType);
            cmd.Parameters.Add("@SupportFileName", Supportfilename);
            cmd.Parameters.Add("@OHAAmount", txtOAmount.Text);
            cmd.Parameters.Add("@OACC_Name", txtOAccountHolder.Text);
            cmd.Parameters.Add("@OACC_No", txtOAcNo.Text);
            cmd.Parameters.Add("@OBank", txtOBankName.Text);
            cmd.Parameters.Add("@OBranch", txtOBranch.Text);
            cmd.Parameters.Add("@OIFSC", txtOIfsc.Text);
            cmd.Parameters.Add("@OAnn_Attachment", ObytesSupport);
            cmd.Parameters.Add("@OAnn_ContentType", OSupportcontentType);
            cmd.Parameters.Add("@OFileName", OSupportfilename);
            cmd.Parameters.Add("@UserId", Session["uid"].ToString());
            cmd.Parameters.Add("@OrderNo", SqlDbType.VarChar, 100);
            cmd.Parameters["@OrderNo"].Direction = ParameterDirection.Output;
            if (con2.State == ConnectionState.Closed)
                con2.Open();
            cmd.ExecuteNonQuery();
            Code = cmd.Parameters["@OrderNo"].Value.ToString();
            con2.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Your Application No. " + Code + " is Save Successfully'); document.location.href='Honorarium_E_Visit.aspx';", true);

        }
    }









    protected void drpDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        calculateAmt(drpDesignation.SelectedValue, drpMode.SelectedValue);
    }

    protected void drpMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        calculateAmt(drpDesignation.SelectedValue, drpMode.SelectedValue);
        enabledisabe();
    }

    protected void txttravelAll_TextChanged(object sender, EventArgs e)
    {
        txttravelAMT.Text = (Convert.ToDecimal(txtHonoAMT.Text) + Convert.ToDecimal(txttravelAll.Text)).ToString();
        enabledisabe();
    }

    protected void lnkDetail_Click(object sender, EventArgs e)
    {
        string Complaint = (sender as LinkButton).CommandArgument;

        hdfApplicationNo.Value = Complaint;
        SqlCommand cmd = new SqlCommand("[SP_GetHonorariumbyIDV2]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", Complaint);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lbltype.Text = dt.Rows[0]["applicationfor"].ToString();
            lblExpert.Text = dt.Rows[0]["name"].ToString();
            lblDesig.Text = dt.Rows[0]["designation"].ToString();
            lblOrganisation.Text = dt.Rows[0]["organization"].ToString();
            lblDate.Text = dt.Rows[0]["visitingdate"].ToString();
            lblMOV.Text = dt.Rows[0]["modeofvisiting"].ToString();
            lblHoAmt.Text = dt.Rows[0]["Amount"].ToString();
            lblTravelAllow.Text = dt.Rows[0]["travelallow"].ToString();
            lblTotalAMT.Text = dt.Rows[0]["totalamt"].ToString();
            lblACHName.Text = dt.Rows[0]["acname"].ToString();
            lblACNumber.Text = dt.Rows[0]["acnumber"].ToString();
            lblBank.Text = dt.Rows[0]["bank"].ToString();
            lblBranch.Text = dt.Rows[0]["branch"].ToString();
            lblIFSC.Text = dt.Rows[0]["ifsc"].ToString();
            lblEventtype.Text = dt.Rows[0]["eventType"].ToString();
            lblOAmount.Text = dt.Rows[0]["OAmount"].ToString();
            lblOACCHolderName.Text = dt.Rows[0]["Oacname"].ToString();

            lblOAccNumber.Text = dt.Rows[0]["Oacnumber"].ToString();
            lblOBankName.Text = dt.Rows[0]["Obank"].ToString();
            lblOBranch.Text = dt.Rows[0]["Obranch"].ToString();
            lblOIFSC.Text = dt.Rows[0]["Oifsc"].ToString();
        }

        btnApprove.Visible = false;
        btnRejectPop.Visible = false;
        txtRemark.Visible = false;







        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal1').modal('show');</script>", false);
    }

    protected void lnkAction_Click(object sender, EventArgs e)
    {
        string Complaint = (sender as LinkButton).CommandArgument;
        hdfApplicationNo.Value = Complaint;

        SqlCommand cmd = new SqlCommand("[SP_GetHonorariumbyIDV2]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", Complaint);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lbltype.Text = dt.Rows[0]["applicationfor"].ToString();
            lblExpert.Text = dt.Rows[0]["name"].ToString();
            lblDesig.Text = dt.Rows[0]["designation"].ToString();
            lblOrganisation.Text = dt.Rows[0]["organization"].ToString();
            lblDate.Text = dt.Rows[0]["visitingdate"].ToString();
            lblMOV.Text = dt.Rows[0]["modeofvisiting"].ToString();
            lblHoAmt.Text = dt.Rows[0]["Amount"].ToString();
            lblTravelAllow.Text = dt.Rows[0]["travelallow"].ToString();
            lblTotalAMT.Text = dt.Rows[0]["totalamt"].ToString();
            lblACHName.Text = dt.Rows[0]["acname"].ToString();
            lblACNumber.Text = dt.Rows[0]["acnumber"].ToString();
            lblBank.Text = dt.Rows[0]["bank"].ToString();
            lblBranch.Text = dt.Rows[0]["branch"].ToString();
            lblIFSC.Text = dt.Rows[0]["ifsc"].ToString();
            lblEventtype.Text = dt.Rows[0]["eventType"].ToString();
            lblOAmount.Text = dt.Rows[0]["OAmount"].ToString();
            lblOACCHolderName.Text = dt.Rows[0]["Oacname"].ToString();

            lblOAccNumber.Text = dt.Rows[0]["Oacnumber"].ToString();
            lblOBankName.Text = dt.Rows[0]["Obank"].ToString();
            lblOBranch.Text = dt.Rows[0]["Obranch"].ToString();
            lblOIFSC.Text = dt.Rows[0]["Oifsc"].ToString();

        }
        btnApprove.Visible = true;
        btnRejectPop.Visible = true;
        txtRemark.Visible = true;
        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal1').modal('show');</script>", false);

    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("[SP_ApproveBoSStatus]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ComplaintNo", hdfApplicationNo.Value);
        cmd.Parameters.Add("@UseId", Session["uid"].ToString());
        cmd.Parameters.Add("@Remark", txtRemark.Text);


        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('Application Approved Successfully'); document.location.href='Honorarium_E_Visit.aspx';", true);

    }

    protected void btnRejectPop_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("[SP_RejectBoSStatus]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ComplaintNo", hdfApplicationNo.Value);
        cmd.Parameters.Add("@UseId", Session["uid"].ToString());
        cmd.Parameters.Add("@Remark", txtRemark.Text);


        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('Application Reject Successfully'); document.location.href='Honorarium_E_Visit.aspx';", true);

    }

    protected void lnkSupport_Click(object sender, EventArgs e)
    {
        try
        {
            string AssignmentNo = hdfApplicationNo.Value;

            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "select supportattachment Attachment, supportcontenttype  [Content Type], supportfilename   [File Name] from tbl_Honorarium_ApplicationData where ApplicationNo='" + AssignmentNo + "' ";
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
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal1').modal('show');</script>", false);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('No Attachment Found');", true);
        }
    }

    protected void lnkDoc_Click(object sender, EventArgs e)
    {
        string AssignmentNo = hdfApplicationNo.Value;

        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select attachment Attachment, contenttype  [Content Type], attfilename   [File Name] from tbl_Honorarium_ApplicationData where ApplicationNo='" + AssignmentNo + "' ";
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
        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal1').modal('show');</script>", false);
    }



    protected void drpAppType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpAppType.SelectedValue == "Others-Specify" || drpAppType.SelectedValue == "IIC")
        {
            divEvent.Visible = true;



        }
        else
        {
            divEvent.Visible = false;
            txteventType.Text = "";
            //divEvent
        }
        enabledisabe();
    }

    protected void lnkOAttach_Click(object sender, EventArgs e)
    {
        string AssignmentNo = hdfApplicationNo.Value;

        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select attachment Attachment, contenttype  [Content Type], attfilename   [File Name] from tbl_Honorarium_Other_Expense where ApplicationNo='" + AssignmentNo + "' ";
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
        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal1').modal('show');</script>", false);
    }

    protected void drpcollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpcollege.SelectedValue == "zz")
        {
            unit.Visible = true;
        }
        else
        {
            unit.Visible = false;
        }
        enabledisabe();
    }
    protected void txtHonoAMT_TextChanged(object sender, EventArgs e)
    {
        decimal honoAmt = 0;
        decimal travelAll = 0;

        decimal.TryParse(txtHonoAMT.Text, out honoAmt);
        decimal.TryParse(txttravelAll.Text, out travelAll);

        txttravelAMT.Text = (honoAmt + travelAll).ToString("F2");
    }
}