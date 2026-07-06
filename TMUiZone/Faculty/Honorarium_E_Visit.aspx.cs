
using Microsoft.Reporting.WebForms;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
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
                //if (Session["UserGroup"].ToString() != "PRINCIPAL" && Session["UserGroup"].ToString() != "REGISTRAR" && Session["uid"].ToString() != "TMU00283" && Session["uid"].ToString() != "TMU08026" && Session["uid"].ToString() != "TMU00525" && Session["uid"].ToString() != "TMU05294" && Session["uid"].ToString() != "TMU00260" )
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('access denied ?'); document.location.href='FacultyDetails.aspx';", true);
                //}
                calculateAmt(drpDesignation.SelectedValue, drpMode.SelectedValue);
                BindList(Session["uid"].ToString());
                BindTaskList();

                BindEventType();
                if (Session["uid"].ToString() == "TMU08026")
                {
                    txtApproveAMT.Visible = false;
                    txtVCApproveAMT.Visible = true;
                }
                if (Session["uid"].ToString() == "TMU06022")
                {
                    lnkAllData.Visible = true;
                }
                //if (Session["uid"].ToString() == "TMU00525" || Session["uid"].ToString() == "TMU00260" )
                //{
                //    divSupport.Visible = true;



                    //}
                    //else
                    //{

                    //        ListItem removeItem4 = drpDesignation.Items.FindByValue("Others");
                    //        drpDesignation.Items.Remove(removeItem4);



                    //}

            }
            catch (Exception ex)
            {
                Response.Redirect("../Default.aspx");
            }
        }
    }
    void BindEventType()
    {
        SqlDataAdapter da = new SqlDataAdapter("SELECT distinct       [task] EventID,      [task] EventName  FROM [dbo].[tbl_honorarium_auth_process] where status=1", con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        chkEventType.DataSource = dt;
        chkEventType.DataTextField = "EventName";
        chkEventType.DataValueField = "EventID";
        chkEventType.DataBind();
    }

    // 🔷 Get Selected Events
    string GetSelectedEvents()
    {
        string selected = "";

        foreach (ListItem item in chkEventType.Items)
        {
            if (item.Selected)
            {
                selected += item.Value + ",";
            }
        }

        return selected.TrimEnd(',');
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
    public void calculateAmt(string des, string mode)
    {
        if (Session["uid"].ToString() == "TMU00525" || Session["uid"].ToString() == "TMU00260")
        {
            txtHonoAMT.Text = "";
            txtHonoAMT.Enabled = true;
        }
        else
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
        }
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
        Panel1.Visible = false;
       

    }

    protected void lnkApproval_Click(object sender, EventArgs e)
    {
        pnlApplication.Visible = false;
        pnlApplicationList.Visible = false;
        pnlApplicationApproval.Visible = true;
        pnlRepport.Visible = false;
        Panel1.Visible = false;
        BindListForApproval(Session["uid"].ToString());

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string reportType = ddlReportType.SelectedValue;
        string fromDate = txtDateFrom.Text;
        string toDate = txtDateTo.Text;
        string eventIds = GetSelectedEvents();

        LoadReport(reportType, fromDate, toDate, eventIds);
    }

    void LoadReport(string reportType, string fromDate, string toDate, string eventIds)
    {
        SqlCommand cmd = new SqlCommand("proc_HOApplicationReport", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@ReportType", reportType);
        cmd.Parameters.AddWithValue("@FromDate", fromDate);
        cmd.Parameters.AddWithValue("@ToDate", toDate);
        cmd.Parameters.AddWithValue("@EventIDs", eventIds);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ReportViewer1.Visible = true;
        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.Visible = true;

        if (ddlReportType.SelectedValue == "1")
        {
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/HonorReport.rdlc");

        }
        if (ddlReportType.SelectedValue == "2")
        {
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/HonorReportExpert.rdlc");

        }
        if (ddlReportType.SelectedValue == "3")
        {
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/HonorReportExpertAmount.rdlc");

        }
        ReportDataSource datasource = new ReportDataSource("DataSet_Result", dt);
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(datasource);
    }






    protected void lnkReport_Click(object sender, EventArgs e)
    {
        pnlApplication.Visible = false;
        pnlApplicationList.Visible = false;
        pnlApplicationApproval.Visible = false;
      
        pnlRepport.Visible = true;
        Panel1.Visible = false;
        //SqlCommand cmd = new SqlCommand("proc_HOApplicationReport", con);
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        //cmd.Parameters.AddWithValue("@FromDate", txtfe);

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
        //    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/HonorReport.rdlc");
        //    ReportDataSource datasource = new ReportDataSource("DataSet_Result", dt);
        //    ReportViewer1.LocalReport.DataSources.Clear();
        //    ReportViewer1.LocalReport.DataSources.Add(datasource);
        //}




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
        Panel1.Visible = false;




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

        if (drpAppType.SelectedValue == "Others-Specify" && txteventType.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Enter Event Type.');", true);
            return;
        }

        if (FileUpload1.HasFile)
        {
            string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string contentType = FileUpload1.PostedFile.ContentType;
            //if (divSupport.Visible == false)
            //{
            //    Supportfilename = "";
            //    SupportcontentType = "";               
            bytesSupport = new byte[0];
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

            // }
            using (Stream fs = FileUpload1.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
                    {
                        SqlCommand cmd = new SqlCommand("SP_InsertHonorarium", con2);
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
                        cmd.Parameters.Add("@Ann_Attachment", bytes);
                        cmd.Parameters.Add("@Ann_ContentType", contentType);
                        cmd.Parameters.Add("@FileName", filename);

                        cmd.Parameters.Add("@Support_Attachment", bytesSupport);
                        cmd.Parameters.Add("@Support_ContentType", SupportcontentType);
                        cmd.Parameters.Add("@SupportFileName", Supportfilename);

                        cmd.Parameters.Add("@UserId", Session["uid"].ToString());

                        cmd.Parameters.Add("@Mobile", txtMobile.Text);
                        cmd.Parameters.Add("@Email", txtEmail.Text);


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
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select Attachment to Upload.');", true);
            return;
        }





    }




    protected void drpDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        calculateAmt(drpDesignation.SelectedValue, drpMode.SelectedValue);
    }

    protected void drpMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        calculateAmt(drpDesignation.SelectedValue, drpMode.SelectedValue);
    }

    protected void txttravelAll_TextChanged(object sender, EventArgs e)
    {
        txttravelAMT.Text = (Convert.ToDecimal(txtHonoAMT.Text) + Convert.ToDecimal(txttravelAll.Text)).ToString();
    }

    protected void lnkDetail_Click(object sender, EventArgs e)
    {
        string Complaint = (sender as LinkButton).CommandArgument;

        hdfApplicationNo.Value = Complaint;
        SqlCommand cmd = new SqlCommand("[SP_GetHonorariumbyID]", con);
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
            txtApproveAMT.Text = dt.Rows[0]["totalamt"].ToString();
            txtVCApproveAMT.Text = dt.Rows[0]["totalamt"].ToString();
            lblACHName.Text = dt.Rows[0]["acname"].ToString();
            lblACNumber.Text = dt.Rows[0]["acnumber"].ToString();
            lblBank.Text = dt.Rows[0]["bank"].ToString();
            lblBranch.Text = dt.Rows[0]["branch"].ToString();
            lblIFSC.Text = dt.Rows[0]["ifsc"].ToString();
            lblEventtype.Text = dt.Rows[0]["eventType"].ToString();
            txtRecommend.Text= dt.Rows[0]["registrarremark"].ToString();



        }

        btnApprove.Visible = false;
        btnRejectPop.Visible = false;
        divVCRemark.Visible = false;







        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal1').modal('show');</script>", false);
    }

    protected void lnkAction_Click(object sender, EventArgs e)
    {
        string Complaint = (sender as LinkButton).CommandArgument;
        hdfApplicationNo.Value = Complaint;

        SqlCommand cmd = new SqlCommand("[SP_GetHonorariumbyID]", con);
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
            txtApproveAMT.Text = dt.Rows[0]["totalamt"].ToString();
            lblRecommendedAmt.Text = dt.Rows[0]["AprrovedAMT"].ToString();
            txtVCApproveAMT.Text = dt.Rows[0]["totalamt"].ToString();
            lblACHName.Text = dt.Rows[0]["acname"].ToString();
            lblACNumber.Text = dt.Rows[0]["acnumber"].ToString();
            lblBank.Text = dt.Rows[0]["bank"].ToString();
            lblBranch.Text = dt.Rows[0]["branch"].ToString();
            lblIFSC.Text = dt.Rows[0]["ifsc"].ToString();
            lblEventtype.Text = dt.Rows[0]["eventType"].ToString();
            txtRecommend.Text = dt.Rows[0]["registrarremark"].ToString();

        }
        btnApprove.Visible = true;
        btnRejectPop.Visible = true;
        if (Session["uid"].ToString() == "TMU08026")
        {
            divVCRemark.Visible = true;
            txtRecommend.Enabled = false;
        }
        else
        {
            divVCRemark.Visible = false;
        }

        
        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal1').modal('show');</script>", false);

    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (Session["uid"].ToString() != "TMU08026")
        {
            if (txtApproveAMT.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Enter Approved Amount');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal1').modal('show');</script>", false);
                return;
            }
        }
        if (Session["uid"].ToString() == "TMU08026")
        {
            if (txtVCApproveAMT.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Enter Approved Amount');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal1').modal('show');</script>", false);
                return;
            }
        }

        SqlCommand cmd = new SqlCommand("[SP_ApproveBoSStatus]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ComplaintNo", hdfApplicationNo.Value);
        cmd.Parameters.Add("@UseId", Session["uid"].ToString());
        cmd.Parameters.Add("@Remark", txtRemark.Text);
        if (Session["uid"].ToString() == "TMU08026")
        {
            cmd.Parameters.Add("@AprrovedAMT", txtVCApproveAMT.Text);
        }
        else
        {
            cmd.Parameters.Add("@AprrovedAMT", txtApproveAMT.Text);
        }


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

            if (fileName != "")
            {

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
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('No Attachment Found');", true);
            }
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
        if (drpAppType.SelectedValue == "Others-Specify")
        {
            divEvent.Visible = true;



        }
        else
        {
            divEvent.Visible = false;
            txteventType.Text = "";
            //divEvent
        }

    }

    protected void txtHonoAMT_TextChanged(object sender, EventArgs e)
    {
        txttravelAMT.Text = (Convert.ToDecimal(txtHonoAMT.Text) + Convert.ToDecimal(txttravelAll.Text)).ToString();
    }

    protected void lnkAllData_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        pnlApplicationList.Visible = false;
        pnlApplication.Visible = false;
        pnlRepport.Visible = false;
        pnlApplicationApproval.Visible = false;

        BindListALL(Session["uid"].ToString());
    }

    public void BindListALL(string UserId)
    {
        SqlCommand cmd = new SqlCommand("[SP_GetHonorariumALL]", con);
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
    }
}