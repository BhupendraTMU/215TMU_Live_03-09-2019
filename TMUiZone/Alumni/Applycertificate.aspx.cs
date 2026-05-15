
using paytm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Alumni_Applycertificate : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    static int Updatecount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string strSQL = ("select top 1 Year([Publish Date]) as 'YEAR' from [TMU$Posted Student Ext_Int Line] where[Enrollement No_] = '" + Session["enroll"].ToString() + "' order by [Publish Date] desc");

                SqlDataAdapter da = new SqlDataAdapter(strSQL, con1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0]["YEAR"]) >= 2024)
                    {
                        binddata();
                        bindGrid();
                        bindMigrationDetail(Session["enroll"].ToString());
                        bindCountry();
                        bindstate(drpCountry.SelectedValue);
                        bindcity(drpState.SelectedValue);
                        bindpostcode(drpCity.SelectedValue);
                    }
                    else
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                }
                else if (Session["College"].ToString() == "TMMC" || Session["College"].ToString() == "TMDC")
                {
                    binddata();
                    bindGrid();
                    bindMigrationDetail(Session["enroll"].ToString());
                    bindCountry();
                    bindstate(drpCountry.SelectedValue);
                    bindcity(drpState.SelectedValue);
                    bindpostcode(drpCity.SelectedValue);
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }

            }
        }
        catch (Exception ex)
        {
            // Response.Redirect("~/Default.aspx");
        }

    }

    public void bindMigrationDetail(string Enroll)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetCertificateDetail", con);
            cmd.Parameters.Add("@EnrollmentNo_", txtenrollmentno.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            GrdMigrationStudent.DataSource = dt;
            GrdMigrationStudent.DataBind();
        }
        catch
        {
        }

    }


    public void bindCountry()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetCountry", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            drpCountry.DataSource = dt;
            drpCountry.DataTextField = "name";
            drpCountry.DataValueField = "country_id";
            drpCountry.DataBind();
            drpCountry.SelectedValue = "94";
        }
        catch
        {
        }

    }

    public void bindstate(string countryid)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetState", con);
            cmd.Parameters.Add("@countryId", countryid);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                drpState.DataSource = dt;
                drpState.DataTextField = "name";
                drpState.DataValueField = "state_id";
                drpState.DataBind();
            }
        }
        catch
        {
        }
    }


    public void bindpostcode(string cityid)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetPostCode", con);
            cmd.Parameters.Add("@CityId", cityid);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                txtPost.Text = dt.Rows[0]["postcode"].ToString();

            }
        }
        catch
        {
        }
    }

    public void bindcity(string stateid)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetCity", con);
            cmd.Parameters.Add("@stateId", stateid);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                drpCity.DataSource = dt;
                drpCity.DataTextField = "name";
                drpCity.DataValueField = "district_id";
                drpCity.DataBind();
            }
        }
        catch
        {
        }
    }




    public void binddata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        string strSQL = "select Case when Gender=1 then 'Male' else 'Female' end as Gender,'20' +RIGHT([Academic Year], 2) AS AcademicYear1, (Select [Exam Course Name] from [TMU$Course - COLLEGE] where [Code]=P.[Course Code]) As CourseName, *  from [EDUCOLLEGELIVE-R2].dbo.[TMU$Student - COLLEGE] as P where [Enrollment No_]='" + Session["enroll"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        txtenrollmentno.Text = dt.Rows[0]["Enrollment No_"].ToString();
        txtstudentName.Text = dt.Rows[0]["Student Name"].ToString();

        txtfathername.Text = dt.Rows[0]["Fathers Name"].ToString();
        // txtmobile.Text = dt.Rows[0]["Mobile Number"].ToString();
        // txtemailid.Text = dt.Rows[0]["E-Mail Address"].ToString();
        txtPrograme.Text = dt.Rows[0]["Course Name"].ToString();

        txtcollegedept.Text = dt.Rows[0]["Global Dimension 1 Code"].ToString();
        //lblcertifiedname.Text = dt.Rows[0]["Student Name"].ToString();
        //lblYear.Text = dt.Rows[0]["Academic Year"].ToString();
        txtstno.Text = dt.Rows[0]["No_"].ToString();

    }
    public void bindGrid()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetCertificate", con);
            cmd.Parameters.Add("@Type", drpCertificatetype.SelectedValue);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            ddlCertificate.DataSource = dt;
            ddlCertificate.DataTextField = "Description";
            ddlCertificate.DataValueField = "Code";
            ddlCertificate.DataBind();
        }
        catch
        {
        }
    }





    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string Supportfilename = "";
        string SupportcontentType = "";
        byte[] bytesSupport = new byte[0];
        decimal amount = 0;
        string COEStatus = "";
        string InternshipStatus = "";

        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        string strSQL1 = "select (select Graduation from [TMU$Course - COLLEGE] where Code=[Course Code]) 'CourseType',[Global Dimension 1 Code] as 'College',(select [Internship Completed] from [TMU$Student Hold_Unhold Marking] where [Enrollment No]=SC.[Enrollment No_]) as 'Intership' ,(select [PDC Issued] from [TMU$Student Hold_Unhold Marking] where [Enrollment No]=SC.[Enrollment No_]) as 'PDC',(select [Degree Issued] from [TMU$Student Hold_Unhold Marking] where [Enrollment No]=SC.[Enrollment No_]) as 'Degree' from [EDUCOLLEGELIVE-R2].dbo.[TMU$Student - COLLEGE] SC where [Enrollment No_]='" + Session["enroll"].ToString() + "'";
        SqlDataAdapter da1 = new SqlDataAdapter(strSQL1, con1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        con1.Close();
        if (dt1.Rows[0]["College"].ToString() == "TMMC" || dt1.Rows[0]["College"].ToString() == "TMDC")
        {
            if ((dt1.Rows[0]["College"].ToString() == "TMMC" || dt1.Rows[0]["College"].ToString() == "TMDC") && dt1.Rows[0]["CourseType"].ToString() == "UG")
            {
                if (dt1.Rows[0]["Intership"].ToString() != "1")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('you are not eligible to apply migration certificate.');", true);
                    return;
                }
            }
            if ((dt1.Rows[0]["College"].ToString() == "TMMC" || dt1.Rows[0]["College"].ToString() == "TMDC") && dt1.Rows[0]["CourseType"].ToString() == "PG")
            {
                if (dt1.Rows[0]["Degree"].ToString() != "1" || dt1.Rows[0]["PDC"].ToString() != "1")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('you are not eligible to apply migration certificate.');", true);
                    return;
                }
            }
            if (ddlCertificate.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Please Select Certificate');", true);
                return;
            }
            if (drpCollectType.SelectedValue == "0")
            {
                if (drpState.SelectedValue == "1")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Please Select state');", true);
                    return;
                }
                if (drpCity.SelectedValue == "1")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Please Select city');", true);
                    return;
                }
                if (txtPost.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Please Select post code');", true);
                    return;
                }
                if (txtaddress.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Please Select address');", true);
                    return;
                }
            }
            if (flUpload.HasFile)
            {
                string filename = Path.GetFileName(flUpload.PostedFile.FileName);
                string contentType = flUpload.PostedFile.ContentType;
                bytesSupport = new byte[0];
                using (Stream fs = flUpload.PostedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        Supportfilename = Path.GetFileName(flUpload.PostedFile.FileName);
                        SupportcontentType = flUpload.PostedFile.ContentType;
                        Stream fsSupport = flUpload.PostedFile.InputStream;
                        BinaryReader brsupport = new BinaryReader(fsSupport);
                        bytesSupport = brsupport.ReadBytes((Int32)fsSupport.Length);
                    }
                }
            }
            if (drpCertificatetype.SelectedValue == "1" && Supportfilename == "")
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Please upload Affidavit');", true);
                return;
            }
            if (drpCollectType.SelectedValue == "0")
            {
                if (drpCountry.SelectedValue == "94")
                {
                    amount = 1200;
                }
                else
                {
                    amount = 2500;
                }
            }
            else
            {
                amount = 1000;
            }
            SqlCommand cmd1 = new SqlCommand("HRMSPortal.dbo.proc_GetMIGRApplicationCount", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@DocType", ddlCertificate.SelectedValue);
            cmd1.Parameters.AddWithValue("@UserID", txtenrollmentno.Text);
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            System.Data.DataSet ds = new System.Data.DataSet();
            da.Fill(ds);
            con.Close();
            if (Convert.ToInt32(ds.Tables[0].Rows[0]["C"]) > 0)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Your Already applied for this title'); document.location.href='Applycertificate.aspx';", true);
                return;


            }
            string Code = "";
            SqlCommand cmd = new SqlCommand("HRMSPortal.dbo.MI_InsertApplication", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Enrollment_No", txtenrollmentno.Text);
            cmd.Parameters.Add("@Student_Name", txtstudentName.Text);
            cmd.Parameters.Add("@Father_Name", txtfathername.Text);
            cmd.Parameters.Add("@College", txtcollegedept.Text);
            cmd.Parameters.Add("@Program", txtPrograme.Text);
            cmd.Parameters.Add("@Certificate_Type", drpCertificatetype.SelectedValue);
            cmd.Parameters.Add("@Certificate", ddlCertificate.SelectedValue);
            cmd.Parameters.Add("@Student_Remark", "");
            cmd.Parameters.Add("@Examination_Remark", "");
            cmd.Parameters.Add("@Dept_Remark", "");
            cmd.Parameters.Add("@Attachment", bytesSupport);
            cmd.Parameters.Add("@ContentType", SupportcontentType);
            cmd.Parameters.Add("@FileName", Supportfilename);
            cmd.Parameters.Add("@App_Status", 1);
            cmd.Parameters.Add("@Payment_Status", "0");
            cmd.Parameters.Add("@Receive_Mode", drpCollectType.SelectedValue);
            cmd.Parameters.Add("@country", drpCountry.SelectedValue);
            cmd.Parameters.Add("@state", drpState.SelectedValue);
            cmd.Parameters.Add("@city", drpCity.SelectedValue);
            cmd.Parameters.Add("@postCode", txtPost.Text);
            cmd.Parameters.Add("@Adress", txtaddress.Text);
            cmd.Parameters.Add("@amount", amount);
            cmd.Parameters.Add("@OrderNo", SqlDbType.VarChar, 100);
            cmd.Parameters["@OrderNo"].Direction = ParameterDirection.Output;
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd.ExecuteNonQuery();

            Code = cmd.Parameters["@OrderNo"].Value.ToString();


            con.Close();


            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Your Application No. " + Code + " is Save Successfully'); document.location.href='Applycertificate.aspx';", true);


        }
        else
        { 

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            {
                SqlCommand cmd12 = new SqlCommand("Select [COE Status] as COEStatus,[Internship Status] as InternshipStatus from Tbl_StudentNodues where [Enrollement No]='" + txtenrollmentno.Text + "'", con);
                con.Open();
                SqlDataReader dr = cmd12.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        COEStatus = dr["COEStatus"].ToString();
                        InternshipStatus = dr["InternshipStatus"].ToString();
                    }
                }
                con.Close();
                SqlCommand cmd10 = new SqlCommand("SP_NoDuesStatus", con);
                con.Open();
                cmd10.Parameters.AddWithValue("@enrNo", Session["enroll"].ToString());
                cmd10.CommandType = CommandType.StoredProcedure;
                object result = cmd10.ExecuteScalar();
                int value = 0;
                if (result != null)
                {
                    value = Convert.ToInt32(result.ToString());
                }
                con.Close();
                if (value == 11 && (InternshipStatus == "Internship Done" || InternshipStatus == "Not Applicable"))
                {
                    if (ddlCertificate.SelectedValue == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Please Select Certificate');", true);
                        return;
                    }
                    if (drpCollectType.SelectedValue == "0")
                    {
                        if (drpState.SelectedValue == "1")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Please Select state');", true);
                            return;
                        }
                        if (drpCity.SelectedValue == "1")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Please Select city');", true);
                            return;
                        }
                        if (txtPost.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Please Select post code');", true);
                            return;
                        }
                        if (txtaddress.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Please Select address');", true);
                            return;
                        }
                    }
                    if (flUpload.HasFile)
                    {
                        string filename = Path.GetFileName(flUpload.PostedFile.FileName);
                        string contentType = flUpload.PostedFile.ContentType;
                        bytesSupport = new byte[0];
                        using (Stream fs = flUpload.PostedFile.InputStream)
                        {
                            using (BinaryReader br = new BinaryReader(fs))
                            {
                                Supportfilename = Path.GetFileName(flUpload.PostedFile.FileName);
                                SupportcontentType = flUpload.PostedFile.ContentType;
                                Stream fsSupport = flUpload.PostedFile.InputStream;
                                BinaryReader brsupport = new BinaryReader(fsSupport);
                                bytesSupport = brsupport.ReadBytes((Int32)fsSupport.Length);
                            }
                        }
                    }
                    if (drpCertificatetype.SelectedValue == "1" && Supportfilename == "")
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Please upload Affidavit');", true);
                        return;
                    }
                    if (drpCollectType.SelectedValue == "0")
                    {
                        if (drpCountry.SelectedValue == "94")
                        {
                            amount = 1200;
                        }
                        else
                        {
                            amount = 2500;
                        }
                    }
                    else
                    {
                        amount = 1000;
                    }
                    SqlCommand cmd1 = new SqlCommand("HRMSPortal.dbo.proc_GetMIGRApplicationCount", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@DocType", ddlCertificate.SelectedValue);
                    cmd1.Parameters.AddWithValue("@UserID", txtenrollmentno.Text);
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd1);
                    System.Data.DataSet ds = new System.Data.DataSet();
                    da.Fill(ds);
                    con.Close();
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["C"]) > 0)
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Your Already applied for this title'); document.location.href='Applycertificate.aspx';", true);
                        return;


                    }
                    string Code = "";
                    SqlCommand cmd = new SqlCommand("HRMSPortal.dbo.MI_InsertApplication", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Enrollment_No", txtenrollmentno.Text);
                    cmd.Parameters.Add("@Student_Name", txtstudentName.Text);
                    cmd.Parameters.Add("@Father_Name", txtfathername.Text);
                    cmd.Parameters.Add("@College", txtcollegedept.Text);
                    cmd.Parameters.Add("@Program", txtPrograme.Text);
                    cmd.Parameters.Add("@Certificate_Type", drpCertificatetype.SelectedValue);
                    cmd.Parameters.Add("@Certificate", ddlCertificate.SelectedValue);
                    cmd.Parameters.Add("@Student_Remark", "");
                    cmd.Parameters.Add("@Examination_Remark", "");
                    cmd.Parameters.Add("@Dept_Remark", "");
                    cmd.Parameters.Add("@Attachment", bytesSupport);
                    cmd.Parameters.Add("@ContentType", SupportcontentType);
                    cmd.Parameters.Add("@FileName", Supportfilename);
                    cmd.Parameters.Add("@App_Status", 1);
                    cmd.Parameters.Add("@Payment_Status", "0");
                    cmd.Parameters.Add("@Receive_Mode", drpCollectType.SelectedValue);
                    cmd.Parameters.Add("@country", drpCountry.SelectedValue);
                    cmd.Parameters.Add("@state", drpState.SelectedValue);
                    cmd.Parameters.Add("@city", drpCity.SelectedValue);
                    cmd.Parameters.Add("@postCode", txtPost.Text);
                    cmd.Parameters.Add("@Adress", txtaddress.Text);
                    cmd.Parameters.Add("@amount", amount);
                    cmd.Parameters.Add("@OrderNo", SqlDbType.VarChar, 100);
                    cmd.Parameters["@OrderNo"].Direction = ParameterDirection.Output;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    cmd.ExecuteNonQuery();

                    Code = cmd.Parameters["@OrderNo"].Value.ToString();


                    con.Close();


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Your Application No. " + Code + " is Save Successfully'); document.location.href='Applycertificate.aspx';", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Please Clear Your No Dues First');", true);
                    return;
                }
            }
        }
    }
    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "OpenNewWindow()", true);
    }
    protected void drpCertificatetype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpCertificatetype.SelectedValue == "1")
        {
            divCer.Visible = true;

        }
        else
        {
            divCer.Visible = false;
        }

        bindGrid();
    }



    protected void lnkDownloadSample_Click(object sender, EventArgs e)
    {
        try
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            wc.OpenRead(Server.MapPath("~/Alumni/Affidavit/Affidavit.pdf"));
            Int64 bytes_total = Convert.ToInt64(wc.ResponseHeaders["Content-Length"]);
            Response.ContentType = "Application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=Affidavit.pdf");
            Response.TransmitFile(Server.MapPath("~/Alumni/Affidavit/Affidavit.pdf"));
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

    protected void drpCollectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpCollectType.SelectedValue == "1")
        {
            drpCountry.Enabled = false;
            drpState.Enabled = false;
            drpCity.Enabled = false;
            txtPost.Enabled = false;
            txtaddress.Enabled = false;
        }
        else
        {
            drpCountry.Enabled = true;
            drpState.Enabled = true;
            drpCity.Enabled = true;
            txtPost.Enabled = true;
            txtaddress.Enabled = true;
        }


    }

    protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindstate(drpCountry.SelectedValue);
    }

    protected void drpState_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindcity(drpState.SelectedValue);

    }

    protected void drpCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindpostcode(drpCity.SelectedValue);
    }

    protected void lblView_Click(object sender, EventArgs e)
    {
        string Complaint = (sender as LinkButton).CommandArgument;

        hdfApplicationNo.Value = Complaint;
        SqlCommand cmd = new SqlCommand("[proc_GetMIGRApplicationDetail]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@AppNo", Complaint);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblAppNo.Text = dt.Rows[0]["App_No"].ToString();
            lblEnrollNo.Text = dt.Rows[0]["Enrollment_No"].ToString();
            lblStudentName.Text = dt.Rows[0]["Student_Name"].ToString();
            lblCollege.Text = dt.Rows[0]["College"].ToString();
            lblProgram.Text = dt.Rows[0]["Program"].ToString();
            lblDesc.Text = dt.Rows[0]["Description"].ToString();
            lblCertificatetype.Text = dt.Rows[0]["Certificate_Type"].ToString();
            lblExamRemark.Text = dt.Rows[0]["Examination_Remark"].ToString();
            lbldeptRemark.Text = dt.Rows[0]["Dept_Remark"].ToString();
            lblCountry.Text = dt.Rows[0]["country"].ToString();
            lblState.Text = dt.Rows[0]["state"].ToString();
            lblcity.Text = dt.Rows[0]["city"].ToString();
            lbladdress.Text = dt.Rows[0]["address"].ToString();
            lblpostcode.Text = dt.Rows[0]["postcode"].ToString();




        }

        //btnApprove.Visible = false;
        //btnRejectPop.Visible = false;
        //txtRemark.Visible = false;







        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal1').modal('show');</script>", false);
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
                    cmd2.CommandText = "select  Attachment,   [Content Type], [File Name]   from [tbl_MigrationRequest] where App_No='" + AssignmentNo + "' ";
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

    protected void btnApprove_Click(object sender, EventArgs e)
    {

    }

    protected void btnRejectPop_Click(object sender, EventArgs e)
    {

    }

    protected void lblmake_Payment_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int i = Convert.ToInt32(row.RowIndex);
        Label Amount = (Label)row.FindControl("lblPay_amount");

        string Complaint = (sender as LinkButton).CommandArgument;
        //lblPay_amount
        int temp = 0;
        string orderid = Session["enroll"].ToString() + "MIGR" + DateTime.Now.Ticks.ToString();
        con1.Open();
        SqlCommand cmd = new SqlCommand("INSERT INTO [OnlinePaymentLog]([Payment Date],[Amount],[Status],[UserID],[GATEWAY],[GatewayStatus],[OrderID],[CLE Entry No],[Semester FEE],[Year FEE],[Temp 1],[Temp 2],[Temp 3],[Orgnized Date])VALUES(GETUTCDATE(),1 ,0 ,'" + Session["uid"].ToString() + "' ,'PAYTM'  ,0 ,'" + orderid + "','','','','" + Complaint + "',0,'', DATEADD(Minute,330,Getdate()))", con1);
        temp = cmd.ExecuteNonQuery();
        con1.Close();
        if (temp == 1)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //-------->Test
            String merchantKey = "7v_qN#jfvvCiLSOB";
            // Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("MID", "Teerth64420690832928");
            parameters.Add("CHANNEL_ID", "WEB");
            parameters.Add("INDUSTRY_TYPE_ID", "Education");
            parameters.Add("WEBSITE", "DEFAULT");
            parameters.Add("EMAIL", "");
            parameters.Add("MOBILE_NO", "");
            //parameters.Add("CUST_ID", Session["uid"].ToString().Replace("/", "").ToString());
            parameters.Add("CUST_ID", Session["uid"].ToString());
            parameters.Add("ORDER_ID", orderid);
            parameters.Add("TXN_AMOUNT", Amount.Text);
            var baseUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            var callbackUrl = new Uri(new Uri(baseUrl), "/Alumni/NoDuesResponse.aspx").ToString();
            parameters.Add("CALLBACK_URL", callbackUrl);
            // parameters.Add("CALLBACK_URL", "https://portal.tmu.ac.in/Student/NoDuesResponse.aspx");
            //parameters.Add("CALLBACK_URL", "http://localhost:55941/Alumni/NoDuesResponse.aspx");

            string checksum = CheckSum.generateCheckSum(merchantKey, parameters);
            string paytmURL = "https://securegw.paytm.in/order/process";
            //string paytmURL = "https://securegw-stage.paytm.in/theia/processTransaction?orderid=" + orderid;
            Session["uid"] = null;
            string outputHTML = "<html>";
            outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
            outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
            outputHTML += "<table border='1'>";
            outputHTML += "<tbody>";
            foreach (string key in parameters.Keys)
            {
                outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
            }
            outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            outputHTML += "<script type='text/javascript'>";
            outputHTML += "document.f1.submit();";
            outputHTML += "</script>";
            outputHTML += "</form>";
            outputHTML += "</script>";
            outputHTML += "</body>";
            outputHTML += "</html>";
            Response.Write(outputHTML);
        }
    }
}