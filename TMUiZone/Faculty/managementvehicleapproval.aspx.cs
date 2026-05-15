using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Faculty_managementvehicleapproval : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal con;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {

        Portalcon = new Connection();
        con = new ServicePoratal();
        if (!IsPostBack)
        {
            getgriddata();
        }
    }

     public void SMS(String MobileNo, string Msg)
    {
        //  Website: http://www.universalsmsadvertising.com/     //  Username: 9837016352   //  Password: 9837016352 
        MobileNo = "91" + MobileNo;
        // MobileNo = "91" + 9015762885;
        string url = "http://www.universalsmsadvertising.com/universalsmsapi.php?user_name=9837016352&user_password=9837016352&mobile=" + MobileNo + "&sender_id=TMUniv&type=F&text=" + Msg + "";
        System.Net.HttpWebRequest fr;
        Uri targetURI = new Uri(url);
        fr = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(targetURI);
        if (fr.GetResponse().ContentLength > 0)
        {
            System.IO.StreamReader str = new System.IO.StreamReader(fr.GetResponse().GetResponseStream());
            Response.Write(str.ReadToEnd());
            str.Close();
        }
    }
    public void SendSMSHODExam(string uname, string userid, string FromDate)
    {
        string tablenameemployeedata = "";
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");

        tablenameemployeedata = "[" + rccname + "$Employee" + "]";
        string mobilnoemp = "";
        string smsdata = "A Car Reqd. at Nursing College 09:30, to 05 Jan 2019";
        //string smsdata = "Dear sir your faculty  " + userid + ", CO  from " + FromDate + " to " + FromDate + " has been applied. " + "Thanks ";

        // As per Subham Gupta 29-12-2018

        SqlDataReader dr = Portalcon.Show_AthorityNo(Session["uid"].ToString(), tablenameemployeedata);
        dr.Read();
        if (dr.HasRows)
        {
            mobilnoemp = dr["MobilePhoneNo"].ToString();
            dr.Close();
            Portalcon.DisConnect();
            if (mobilnoemp.Trim() == "")
            {
              
            }
            else
            {
                try
                {
                    SMS(mobilnoemp, smsdata);
                  
                }
                catch (Exception)
                {

                }
            }
        }
        else
        {
            dr.Close();
            Portalcon.DisConnect();
        }
    }


    public void SendSMSUser(string FromDate, string Destination, string usemobile, string requisno)
    {

        string mobilnoemp = usemobile;

        string smsdata1 = "Your Transport Requisition " + requisno + " Journey Date " + FromDate + "Destination " + Destination + " has been rejected by " + Session["Fulname"].ToString() + " (Management) Remarks " + txtRemarks.Text + ".";

        try
        {
            SMS(mobilnoemp, smsdata1);


        }
        catch (Exception)
        {

        }

    }
   

    public void getgriddata()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_getTransportVehicleAppM", con1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Status", Rblist.SelectedValue);
            cmd.Parameters.AddWithValue("@FromDate", txtFromtDate.Text);
            cmd.Parameters.AddWithValue("@Todate", txtToDate.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con1.Open();
            da.Fill(dt);
            con1.Close();
            GrdTransport.DataSource = dt;
            GrdTransport.DataBind();
            if (dt.Rows.Count > 0)
            {
                if (Rblist.SelectedValue == "9")
                {
                    BtnSubmit.Visible = true;
                    BtnReject.Visible = true;

                }
                else if (Rblist.SelectedValue == "5")
                {
                    BtnSubmit.Visible = false;
                    BtnReject.Visible = true;

                }
                else if (Rblist.SelectedValue == "6")
                {
                    BtnSubmit.Visible = true;
                    BtnReject.Visible = false;

                }
            }
            else
            {
                BtnSubmit.Visible = false;
                BtnReject.Visible = false;

            }
        }
        catch
        {
        }
    }
    protected void Rblist_SelectedIndexChanged(object sender, EventArgs e)
    {
        getgriddata();
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModalB').modal('show');</script>", false);
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkBoxheader = (CheckBox)GrdTransport.HeaderRow.FindControl("chkAll");
        foreach (GridViewRow Row in GrdTransport.Rows)
        {
            CheckBox checkRows = (CheckBox)Row.FindControl("chkDate");
            if (checkBoxheader.Checked == true)
            {
                checkRows.Checked = true;

            }
            else
            {
                checkRows.Checked = false;
            }

        }
    }
    protected void chkDate_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkBoxheader = (CheckBox)GrdTransport.HeaderRow.FindControl("chkAll");
        foreach (GridViewRow Row in GrdTransport.Rows)
        {
            CheckBox checkRows = (CheckBox)Row.FindControl("chkDate");
            if (checkRows.Checked == false)
            {
                checkBoxheader.Checked = false;

            }
            else
            {
                // checkBoxheader.Checked = true;
            }

        }
    }
    protected void BtnYes_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow Row in GrdTransport.Rows)
        {

            HiddenField linNo = (HiddenField)Row.FindControl("hdnStatus");


            CheckBox checkRows = (CheckBox)Row.FindControl("chkDate");
            if (checkRows.Checked == true)
            {
                SqlCommand cmd = new SqlCommand("sp_updateTransport", con1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Status", "5");
                cmd.Parameters.AddWithValue("@Linno", linNo.Value);
                cmd.Parameters.AddWithValue("@Remarks", "");
                con1.Open();
                 cmd.ExecuteNonQuery();
               // SendSMSHODExam(Session["uname"].ToString(), Session["uid"].ToString(), "05 Jan 2019");// for sms
                con1.Close();
            }
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Submitted Successfully')", true);
        getgriddata();
    }
    protected void BtnRYes_Click(object sender, EventArgs e)
    {

        if (txtRemarks.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Remarks is Necessary !')", true);
            txtRemarks.Focus();
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModalR').modal('show');</script>", false);
        }
        else
        {

            foreach (GridViewRow Row in GrdTransport.Rows)
            {

                HiddenField linNo = (HiddenField)Row.FindControl("hdnStatus");
                HiddenField fromD = (HiddenField)Row.FindControl("JDate");
                HiddenField ToD = (HiddenField)Row.FindControl("TDate");
                HiddenField Fromtime = (HiddenField)Row.FindControl("FTime");
                HiddenField Reqn = (HiddenField)Row.FindControl("hdReqNo");
                HiddenField UMoblie = (HiddenField)Row.FindControl("hdMoble");
                HiddenField Dest = (HiddenField)Row.FindControl("hdDest");

                CheckBox checkRows = (CheckBox)Row.FindControl("chkDate");
                if (checkRows.Checked == true)
                {
                    SqlCommand cmd = new SqlCommand("sp_updateTransport", con1);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Status", "6");
                    cmd.Parameters.AddWithValue("@Linno", linNo.Value);
                    cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text);
                    con1.Open();
                    cmd.ExecuteNonQuery();
                    con1.Close();
                    SendSMSUser(fromD.Value, Dest.Value, UMoblie.Value, Reqn.Value.Trim());
                }
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Rejected Successfully')", true);
            getgriddata();
        }
    }
    protected void BtnReject_Click(object sender, EventArgs e)
    {
        bool CheckSelect = false;
        foreach (GridViewRow Row in GrdTransport.Rows)
        {

            CheckBox checkRows = (CheckBox)Row.FindControl("chkDate");
            if (checkRows.Checked == true)
            {
                CheckSelect = true;
            }
        }
        if (CheckSelect == false)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Selection required.')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModalR').modal('show');</script>", false);
        }
    }
    protected void txtFromtDate_TextChanged(object sender, EventArgs e)
    {
        DateTime Stime = System.DateTime.Today;
        if (txtFromtDate.Text != "")
        {
            DateTime startDateTime = Convert.ToDateTime(txtFromtDate.Text);

            if (Stime < startDateTime)
            {
                txtFromtDate.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('From Date should always be Less than Current Date');", true);

            }
        }

        if (txtToDate.Text != "" && txtFromtDate.Text != "")
        {

            DateTime endDateTime = Convert.ToDateTime(txtToDate.Text);
            DateTime startDateTime = Convert.ToDateTime(txtFromtDate.Text);
            if (endDateTime < startDateTime)
            {
                txtFromtDate.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('From Date should always be Less than To Date');", true);

            }

            else
            {
            }
        }
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        DateTime Stime = System.DateTime.Today;
        if (txtToDate.Text != "")
        {
            DateTime endDateTime = Convert.ToDateTime(txtToDate.Text);

            if (Stime > endDateTime)
            {
                txtToDate.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('To Date should always be Less than Current Date');", true);

            }
        }

        if (txtToDate.Text != "" && txtFromtDate.Text != "")
        {
            DateTime endDateTime = Convert.ToDateTime(txtToDate.Text);
            DateTime startDateTime = Convert.ToDateTime(txtFromtDate.Text);
            if (endDateTime < startDateTime)
            {
                txtToDate.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('End Date should always be greater than From Date');", true);

            }

            else
            {
            }
        }
    }
    protected void lnkDownloadgrid_Click(object sender, EventArgs e)
    {
        try
        {
            int id = int.Parse((sender as LinkButton).CommandArgument);
            byte[] bytes;
            string fileName, contentType;
            SqlConnection Conn = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]);
            Conn.Open();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select AttachmentFilename, Attachmentdata, AttachmentFileType from tbleTransportApp where AutoNo=@AutoNo";
                cmd.Parameters.AddWithValue("@AutoNo", id);
                cmd.Connection = Conn;
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Attachmentdata"];
                    contentType = sdr["AttachmentFileType"].ToString();
                    fileName = sdr["AttachmentFilename"].ToString();
                }
                con.DisConnect();

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
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Attachment not found ');", true);

        }
    }
}