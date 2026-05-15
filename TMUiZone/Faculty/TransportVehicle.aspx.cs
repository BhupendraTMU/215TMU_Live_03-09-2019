using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using Utility;
using System.Configuration;
public partial class Faculty_TransportVehicle : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal con;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Portalcon = new Connection();
            con = new ServicePoratal();
            if (!IsPostBack)
            {
                lblfirstApproval.Text = Session["hod_Name_Leave1"].ToString() + "(" + Session["hod_ID_Leave1"].ToString() + ")";
                lblSecondApproval.Text = Session["hod_Name_Leave2"].ToString() + "(" + Session["hod_ID_Leave2"].ToString() + ")";
                if (Session["hod_ID_Leave1"].ToString() == "")
                {
                    lblApprovalAuthority1.Visible = true;
                    btnSave.Enabled = false;
                }

                if (Session["hod_ID_Leave1"].ToString() != "")
                {
                    lblApprovalAuthority1.Visible = false;
                    btnSave.Enabled = true;
                }

                lbldate.Text = DateTime.Today.ToString("dd-MM-yyyy");
                lblDepartment.Text = Session["GlobalDimension1Code"].ToString();
                txtIndentedby.Text = Session["uid"].ToString();
                EmpmobilNo();
                Detination();
                getgriddata();
            }

        }
        catch
        {
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
    public void SendSMSHODExam(string FromDate, string Ftime, string Todate)
    {
        string tablenameemployeedata = "";
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");

        tablenameemployeedata = "[" + rccname + "$Employee" + "]";
        string mobilnoemp = "";
        string smsdata = "A " +ddltypVehicle.SelectedItem.Text+ " Reqd. " +txtPlace.Text+ " "+ Ftime + ", " + FromDate + " to " + Todate + " "+ Session["uid"].ToString()+"";
      
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



  
    



    public void EmpmobilNo()
    {
        string tablenameemployeedata = "";
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        tablenameemployeedata = "[" + rccname + "$Employee" + "]";
        SqlDataReader dr = Portalcon.SHow_EmployeeMobileNo(Session["uid"].ToString(), tablenameemployeedata);
        string mobilnoemp = "";
        dr.Read();
        if (dr.HasRows)
        {
            mobilnoemp = dr["MobilePhoneNo"].ToString();
            dr.Close();
            Portalcon.DisConnect();
            if (mobilnoemp.Trim() == "")
            { }
            else
            {
                try
                {
                    lblmoNo.Text = mobilnoemp;
                    //SMS(mobilnoemp, smsdata);
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



    public void Detination()
    {
        SqlCommand cmd = new SqlCommand("Sp_getTransportDestination", con1);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con1.Open();
        da.Fill(dt);
        con1.Close();
        ddlDestination.DataSource = dt;
        ddlDestination.DataTextField = "Dest";
        ddlDestination.DataValueField = "No_";
        ddlDestination.DataBind();
    }


    public void getgriddata()
    {
        SqlCommand cmd = new SqlCommand("Sp_getTransportVehicle", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@FacultyCode", Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con1.Open();
        da.Fill(dt);
        con1.Close();
        GrdTransport.DataSource = dt;
        GrdTransport.DataBind();
    }

    public void Clear()
    {
        txtFromtDate.Text = "";
        txtToDate.Text = "";
        txtNop.Text = "";
        txtPlace.Text = "";
        txtPurpose.Text = "";
        ddltypVehicle.SelectedIndex = -1;
        ddFAM.SelectedIndex = -1;
        DdFhour.SelectedIndex = -1;
        DDFMinit.SelectedIndex = -1;
        ddTA.SelectedIndex = -1;
        ddThour.SelectedIndex = -1;
        ddTminit.SelectedIndex = -1;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            string TTTime = "" + ddThour.Text + ":" + ddTminit.Text + ":00 " + ddTA.Text + "";
            string TFTime = "" + DdFhour.Text + ":" + DDFMinit.Text + ":00 " + ddFAM.Text + "";


            DateTime startDateTime = Convert.ToDateTime(txtFromtDate.Text.Trim() + " " + TFTime);//, "dd MMM yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            DateTime endDateTime = Convert.ToDateTime(txtToDate.Text.Trim() + " " + TTTime);//, "dd MMM yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);



            if (endDateTime < startDateTime)
            {
                //txtFromTime.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('From time should always be greater than Till Time');", true);
                return;
            }

            else
            {
              int NoPesenger = 0;
        NoPesenger = Convert.ToInt32(txtNop.Text);

            if (NoPesenger > 2)
                 {
                     if (FileUpload1.HasFile)
                     {
                         //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select Upload file');", true);
                        // return;
                     }
                     else
                     {
                         ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload file');", true);
                         return;
                     }
                }



                if (FileUpload1.HasFile)
                {


                    decimal size = Math.Round(((decimal)FileUpload1.PostedFile.ContentLength / (decimal)1024), 2);

                    int fs = 0;
                    fs = Convert.ToInt32(size);

                    if (fs > 700)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'File Size Could Not be Greater than 700 KB !');", true);

                        return;
                    }
                    else
                    {

                               con1.Open();
                        SqlCommand cmd = new SqlCommand("sp_InsertTransportRequistion", con1);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CreateBy", Session["uid"].ToString());
                        cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(lbldate.Text));
                        cmd.Parameters.AddWithValue("@IntendtBy", txtIndentedby.Text);
                        cmd.Parameters.AddWithValue("@MobileNo", lblmoNo.Text);
                        cmd.Parameters.AddWithValue("@Purpose", txtPurpose.Text);
                        cmd.Parameters.AddWithValue("@TypeVehicle", ddltypVehicle.SelectedValue);
                        cmd.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(txtFromtDate.Text));
                        cmd.Parameters.AddWithValue("@ToDate", Convert.ToDateTime(txtToDate.Text));
                        cmd.Parameters.AddWithValue("@Replace", txtPlace.Text);
                        cmd.Parameters.AddWithValue("@DesNo", ddlDestination.SelectedValue);
                        cmd.Parameters.AddWithValue("@FromTime", TFTime);
                        cmd.Parameters.AddWithValue("@ToTime", TTTime);
                        cmd.Parameters.AddWithValue("@HOD", Session["hod_ID_Leave1"].ToString());
                        cmd.Parameters.AddWithValue("@txtNop", txtNop.Text);

                        cmd.ExecuteNonQuery();
                        con1.Close();
                
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Approval  Form Submitted')", true);
                SendSMSHODExam(txtToDate.Text, TFTime, txtToDate.Text);// for sms
                UploadFileAttachmentss();
                Clear();
                getgriddata();

                    }

                }    
                else
                    {
                        con1.Open();
                        SqlCommand cmd = new SqlCommand("sp_InsertTransportRequistion", con1);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CreateBy", Session["uid"].ToString());
                        cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(lbldate.Text));
                        cmd.Parameters.AddWithValue("@IntendtBy", txtIndentedby.Text);
                        cmd.Parameters.AddWithValue("@MobileNo", lblmoNo.Text);
                        cmd.Parameters.AddWithValue("@Purpose", txtPurpose.Text);
                        cmd.Parameters.AddWithValue("@TypeVehicle", ddltypVehicle.SelectedValue);
                        cmd.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(txtFromtDate.Text));
                        cmd.Parameters.AddWithValue("@ToDate", Convert.ToDateTime(txtToDate.Text));
                        cmd.Parameters.AddWithValue("@Replace", txtPlace.Text);
                        cmd.Parameters.AddWithValue("@DesNo", ddlDestination.SelectedValue);
                        cmd.Parameters.AddWithValue("@FromTime", TFTime);
                        cmd.Parameters.AddWithValue("@ToTime", TTTime);
                        cmd.Parameters.AddWithValue("@HOD", Session["hod_ID_Leave1"].ToString());
                        cmd.Parameters.AddWithValue("@txtNop", txtNop.Text);
                        cmd.ExecuteNonQuery();
                        con1.Close();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Approval  Form Submitted')", true);
                        SendSMSHODExam(txtToDate.Text, TFTime, txtToDate.Text);// for sms
                        Clear();
                        getgriddata();

                    }

                
            }
        }
        catch (Exception)
        {


        }
      
              
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        
    }
    public void UploadFileAttachmentss()
    {
        string AutoNo = "";
        SqlDataReader dr = con.Show_TrasportMaxid();
        dr.Read();
        if (dr.HasRows)
        {
            AutoNo = dr["AutoNo"].ToString();
        }
        else
        {
            AutoNo = "0";
        }
        

        string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
        string contentType = FileUpload1.PostedFile.ContentType;
        using (Stream fs = FileUpload1.PostedFile.InputStream)
        {
            using (BinaryReader br = new BinaryReader(fs))
            {
                byte[] bytes = br.ReadBytes((Int32)fs.Length);



                SqlConnection Conn = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]);
                Conn.Open();
             
                //  string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                string query = "update tbleTransportApp set AttachmentFilename=@AttachmentFilename ,AttachmentFileType=@AttachmentFileType,Attachmentdata=@Attachmentdata where AutoNo='" + AutoNo + "'";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = Conn;
                    cmd.Parameters.AddWithValue("@AttachmentFilename", filename);
                    cmd.Parameters.AddWithValue("@AttachmentFileType", contentType);
                    cmd.Parameters.AddWithValue("@Attachmentdata", bytes);

                    cmd.ExecuteNonQuery();
                    con.DisConnect();
                }
            }
        }

    }

    protected void txtFromtDate_TextChanged(object sender, EventArgs e)
    {


        DateTime Stime = System.DateTime.Today;
        if (txtFromtDate.Text != "")
        {
            DateTime startDateTime = Convert.ToDateTime(txtFromtDate.Text);

            if (Stime > startDateTime)
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('From Date should always be Less than End Date');", true);
                
            }

            else
            {
            }
            }
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {

       DateTime Stime=  System.DateTime.Today;
       if (txtToDate.Text != "")
       {
           DateTime endDateTime = Convert.ToDateTime(txtToDate.Text);

           if (Stime > endDateTime)
           {
               txtToDate.Text = "";
               ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('End Date should always be Less than Current Date');", true);
              
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
    protected void txtNop_TextChanged(object sender, EventArgs e)
    {
        int NoPesenger = 0;
        NoPesenger = Convert.ToInt32(txtNop.Text);

        if (NoPesenger > 2)
        {
            rfvflUpload.Visible = true;
            lblFS.Visible = true;
            FileUpload1.Visible = true;
        }
        else
        {
            FileUpload1.Visible = false;
            lblFS.Visible = false;
            rfvflUpload.Visible = false;
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