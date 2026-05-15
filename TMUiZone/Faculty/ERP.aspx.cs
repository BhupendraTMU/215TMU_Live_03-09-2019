using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Configuration;

public partial class recognition : System.Web.UI.Page
{
    Connection con;
    ServicePoratal Portalcon;
    //ashu 27-05-2016-Start
    DataTable dtAddItem = new DataTable();
    SqlTransaction SqlHeaderTrn, SqlLineTrn; 
    //ashu 27-05-2016-END
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            con = new Connection();   Portalcon = new ServicePoratal();
            

            if (!IsPostBack)
            {
                if (Session["HODLoginPage"].ToString() == Session["uid"].ToString())
                {
                    rblDepartment.Enabled = true; 
                }
                else
                {
                    rblDepartment.Enabled = false;  
                }
               // txtCampus.Text = Session["GlobalDimension1Code"].ToString();
               // Show_Store();
               // show_Department();
               // ddDepartment.SelectedValue = Session["Departmentcode"].ToString();
                txtFromDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                txtTodate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
              //  txtDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                txtFromDateApproval.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                txtTodateApproval.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                //showNoSeries();

                //TopNoSeries();
            }
            PendingApprovalCount();
            if (grdAddItem.Rows.Count > 0) btn_sendforAproval.Visible = true;
            else                           btn_sendforAproval.Visible = false;
        }
        catch (Exception)
        {
            Response.Redirect("../Default.aspx");
        }
    }
   
    protected void lnkUserReqest_Click(object sender, EventArgs e)
    {
      //  txtCampus.Text = Session["GlobalDimension1Code"].ToString();
        btnResend.Visible = false;
        btnCancel.Visible = false;
        btn_sendforAproval.Visible = true;
        pnlUserRequest.Visible = true;
        pnlViewRequest.Visible = false;
        pnlPending_Approval.Visible = false;
        pnlApprovalSatus.Visible = false;
    }
    protected void lnkViewRequest_Click(object sender, EventArgs e)
    {
        pnlUserRequest.Visible = false;
        pnlViewRequest.Visible = true;
        pnlPending_Approval.Visible = false;
        pnlApprovalSatus.Visible = false;
    }
    protected void lnkPendingApproval_Click(object sender, EventArgs e)
    {
        pnlUserRequest.Visible = false;
        pnlViewRequest.Visible = false;
        pnlPending_Approval.Visible = true;
        pnlApprovalSatus.Visible = false;
        ShowHODPendingRequestAll("Pending");
    }
    
    string mailfrom = ""; string subject1 = ""; string Body1 = ""; string smtpfromportal = ""; int portNo1; string Pass_From = ""; string Mail_Sending_Option = ""; string Leave_Applymail = ""; string CCMail = "";
    public void ShowMailData(string mailTo1)
    {

        SqlDataReader dr = Portalcon.Show_tble_MailSetup(Session["Company"].ToString(), "Profile");
        dr.Read();
        if (dr.HasRows)
        {
            mailfrom = dr["from_Email"].ToString();
            smtpfromportal = dr["smtp"].ToString();
            Pass_From = dr["Password_From"].ToString();
            //CCMail = dr["CCMail"].ToString();
            string portNo = dr["Port_No"].ToString();
            portNo1 = Convert.ToInt32(portNo);
            //Mail_Sending_Option = dr["Mail_Sending_Option"].ToString();
            //Leave_Applymail = dr["Attendence_Mark"].ToString();

        }

        dr.Close();
        Portalcon.DisConnect();
        //if (Mail_Sending_Option == "1" && Leave_Applymail == "True")
        //{
        //    SendMail(mailTo1);
        //}
        SendMail(mailTo1);
    }


    public void SendMail(string MailTo)
    {

        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
        if (mailfrom == "" && MailTo == "")
        { }

        else
        {
            if (mailfrom == "")
            {
            }

            else
            {

                msg.From = new MailAddress(Session["CompanyEmail"].ToString().Trim(), "  " + Session["Fulname"].ToString());
                if (MailTo == "")
                { }
                else
                {

                    string[] multi = MailTo.Split(',');
                    foreach (string multiTo in multi)
                    {
                        msg.To.Add(multiTo);
                    }
                }
                if (CCMail == "")
                { }
                else
                {
                    string[] ccmulti = CCMail.Split(',');
                    foreach (string ccm in ccmulti)
                    {
                        msg.CC.Add(ccm);
                    }
                }
                msg.Subject = subject1;
                msg.Body = Body1;


                SmtpClient smtp = new SmtpClient();

                smtp.Port = portNo1;
                smtp.Host = smtpfromportal;
                smtp.EnableSsl = true;
                NetworkCredential credential = new NetworkCredential(mailfrom, Pass_From);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = credential;

                try
                {
                    smtp.Send(msg);
                    msg.Dispose();

                }
                catch (Exception)
                {
                    msg.Dispose();
                }
            }
        }
    }

    public void cleardata()
    {
        txtItemToPurchase.Text = "";
        txtRemarks.Text = "";
      //  txtReason.Text = "";
       // txtApprox.Text = "";
       // txtQuantity.Text = "";
       
       // txtVendor.Text = ""; 
        //txtContactPerson.Text = "";
       // txtAddress.Text = "";
    }
    protected void btn_sendforAproval_Click(object sender, EventArgs e)
    {
        Boolean SaveHeader = false; Boolean SaveLine=false; string SequenceNo = "";
        SequenceNo = "";
        if (grdAddItem.Rows.Count < 1)
        { 
        
        }
     
        else
        {
            //if (txtQuantity.Text.Trim() == "")
            //{
            //    txtQuantity.Text = "0";            
            //}

            //Ashu-----------------27-05-2016-------------start
             con = new Connection();
             SqlCommand cmd = new SqlCommand();
             cmd.Connection = con.Con;
             con.Con.Open();
             SqlHeaderTrn = con.Con.BeginTransaction();
              //SqlHeaderTrn = 
             cmd.Transaction = SqlHeaderTrn;
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.CommandText = "sp_SaveItemIndentRequestHeader";
             cmd.CommandTimeout = 999000000;
             int Status=0;
             try
             {
                 if (Session["HODLoginPage"].ToString() == Session["uid"].ToString()) { Status = 2; } else { Status = 1; }
                 cmd.Parameters.AddWithValue("@IndentFor",rblDepartment.SelectedValue);
                 cmd.Parameters.AddWithValue("@UserID",Session["uid"] );
                 cmd.Parameters.AddWithValue("@Status", Status);
                
                 object Result = new object();
                 Result = cmd.ExecuteScalar();
                 SequenceNo = Result.ToString();
                 cmd.Parameters.Clear();
                // cmd.ExecuteReader();
                 SaveHeader = true;
                // con.Con.Close();
             }
            catch
             {
                SqlHeaderTrn.Rollback();
                return;
             }
            // SqlCommand  cmdL=new SqlCommand();
            // cmdL.Connection = con.Con;
             //con.Con.Open();
             //SqlLineTrn = con.Con.BeginTransaction();
             //cmdL.Transaction = SqlLineTrn;            
             //cmdL.CommandType = CommandType.StoredProcedure;                     
             //cmdL.CommandText = "sp_SaveItemIndentRequestLine";   
             //cmdL.CommandTimeout = 999000000;
             cmd.CommandText = "sp_SaveItemIndentRequestLine";
             DataTable dt = (DataTable)ViewState["AddItem"];
             try
             {
                 for (int i = 0; i < dt.Rows.Count; i++)
                 {                     
                     cmd.Parameters.AddWithValue("@DocumentNo", SequenceNo);
                     cmd.Parameters.AddWithValue("@Departmentcode", Session["Departmentcode"]);
                     cmd.Parameters.AddWithValue("@ItemCode", dt.Rows[i]["ItemCode"]);
                     cmd.Parameters.AddWithValue("@ItemDescription", dt.Rows[i]["ItemDescription"]);
                     cmd.Parameters.AddWithValue("@IndentFor", Convert.ToInt32(rblDepartment.SelectedValue));
                     cmd.Parameters.AddWithValue("@VariantCode", dt.Rows[i]["ItemCode"]);
                     cmd.Parameters.AddWithValue("@UOM", dt.Rows[i]["ItemCode"]);
                     cmd.Parameters.AddWithValue("@Qty", Convert.ToDecimal(dt.Rows[i]["Qty"]));
                     cmd.Parameters.AddWithValue("@Remarks", dt.Rows[i]["Remarks"]);
                     cmd.Parameters.AddWithValue("@UserID", Session["uid"]);
                     cmd.ExecuteScalar();
                     cmd.Parameters.Clear();
                     
                 }
                 SaveLine = true;
             }
             catch
             {
                 SqlHeaderTrn.Rollback();                
                 return;
             }
             if (SaveHeader == true && SaveLine == true)
             {
                 SqlHeaderTrn.Commit();               
                 lblRequestError.Text = "Request save with document No :" + SequenceNo + "";
             }

            //Ashu-----------------27-05-2016-------------start


            //Portalcon.Insert_intotbl_Requisition_user_Request(txtItemToPurchase.Text.Trim(), txtReason.Text.Trim(), Convert.ToDecimal(txtApprox.Text.Trim()), Convert.ToDateTime(txtDate.Text), txtVendor.Text, txtRemarks.Text, ddStore.SelectedValue.ToString(), ddStore.SelectedItem.Text.Trim(), Convert.ToDateTime(System.DateTime.Now.ToString()), Session["uid"].ToString(), Session["Fulname"].ToString(), Session["CompanyEmail"].ToString(), Session["HODLoginPage"].ToString(), Session["hod_Name2"].ToString(), Session["hod_email2"].ToString(), Session["Company"].ToString(), ddRequistionType.SelectedItem.Text.Trim(), txtMobileMo.Text.Trim(), txtAddress.Text.Trim(), Convert.ToDecimal(txtQuantity.Text.Trim()), ddRequistionType.SelectedValue.ToString().Trim(),txtContactPerson.Text.Trim(),ddDepartment.SelectedItem.Text,ddDepartment.SelectedValue.ToString(),txtCampus.Text);
            //Portalcon.DisConnect();


            //subject1 = "User Request of " + System.DateTime.Now.ToString("dd-MM-yyyy");

            //Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}",  Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "I have send request for requisition of  " + txtItemToPurchase.Text + " , '" + Environment.NewLine, "'Approxe Cost : " + txtApprox.Text + "   Quantity : " + txtQuantity.Text + " " + Environment.NewLine, "Remarks :  " + txtReason.Text + "", Environment.NewLine, "Kindly approve it.", Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());


            //ShowMailData(Session["hod_email2"].ToString());
            //cleardata();
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Thanks for your interest, Your request send successfully');", true);

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddstatus.SelectedItem.Text == "All")
        {
            ShowRequestDataAll();
        }
        else
        {
            ShowRequestDatawithStatus(ddstatus.SelectedItem.Text.Trim());
        }
    }
    //public void ShowRequestDataAll()
    //{
    //    if (txtFromDate.Text == "" || txtTodate.Text == "")
    //    { }
    //    else
    //    {
    //        SqlDataReader dr = Portalcon.Show_table_Requisition(Session["Company"].ToString(), Session["uid"].ToString(), Convert.ToDateTime(txtFromDate.Text.Trim()), Convert.ToDateTime(txtTodate.Text.Trim()));
    //        DataTable dt = new DataTable();
    //        dt.Load(dr);
    //        grdViewdetail.DataSource = dt;
    //        grdViewdetail.DataBind();
    //        dr.Close();
    //        Portalcon.DisConnect();
    //    }
    //}
    public void ShowRequestDataAll()
    {
        if (txtFromDate.Text == "" || txtTodate.Text == "")
        { }
        else
        {
            SqlDataReader dr = Portalcon.Show_table_Requisition(Session["Company"].ToString(), Session["uid"].ToString(), Convert.ToDateTime(txtFromDate.Text.Trim()), Convert.ToDateTime(txtTodate.Text.Trim()));
            DataTable dt = new DataTable();
            dt.Load(dr);
            grdViewdetail.DataSource = dt;
            grdViewdetail.DataBind();
            dr.Close();
            Portalcon.DisConnect();
        }
    }

    public void ShowRequestDatawithStatus(string Approval_Status)
    {
        if (txtFromDate.Text == "" || txtTodate.Text == "")
        { }
        else
        {
            SqlDataReader dr = Portalcon.Show_table_RequisitionwithStatus(Session["Company"].ToString(), Session["uid"].ToString(), Convert.ToDateTime(txtFromDate.Text.Trim()), Convert.ToDateTime(txtTodate.Text.Trim()), Approval_Status);
            DataTable dt = new DataTable();
            dt.Load(dr);
            grdViewdetail.DataSource = dt;
            grdViewdetail.DataBind();
            dr.Close();
            Portalcon.DisConnect();
        }
    }
    public void PendingApprovalCount()
    {
        SqlDataReader dr = Portalcon.Show_table_RequisitionwithPendingCount(Session["Company"].ToString(), Session["uid"].ToString(),"Pending");
        dr.Read();
        if (dr.HasRows)
        {
            lblpendingappcount.Text = dr["Approval_Status"].ToString();
            if (lblpendingappcount.Text.Trim() == "0")
            {
                lblpendingappcount.Text = "";
            }
        }

        dr.Close();
        con.DisConnect();
    }

    protected void btnShowPendingApproval_Click(object sender, EventArgs e)
    {
        ShowHODPendingRequest("Pending");
    }
    //select * from [Ashoka Testing$Requistion Header]
    public void ShowHODPendingRequest(string Approval_Status)
    {
        if (txtFromDateApproval.Text == "" || txtTodateApproval.Text == "")
        { }
        else
        {
            SqlDataReader dr = Portalcon.Show_table_RequisitionwithPending(Session["Company"].ToString(), Session["uid"].ToString(), Convert.ToDateTime(txtFromDateApproval.Text.Trim()), Convert.ToDateTime(txtTodateApproval.Text.Trim()), Approval_Status);
            DataTable dt = new DataTable();
            dt.Load(dr);
            grdPendingApproval.DataSource = dt;
            grdPendingApproval.DataBind();
            dr.Close();
            Portalcon.DisConnect();
        }
    }


    public void ShowHODPendingRequestAll(string Approval_Status)
    {

        SqlDataReader dr = Portalcon.Show_table_RequisitionwithPendingAll(Session["Company"].ToString(), Session["uid"].ToString(),Approval_Status);
            DataTable dt = new DataTable();
            dt.Load(dr);
            grdPendingApproval.DataSource = dt;
            grdPendingApproval.DataBind();
            dr.Close();
            Portalcon.DisConnect();
        
    }



    public void View_Requition_For_Approval(string id)
    {
        SqlDataReader dr = Portalcon.show_Tble_Requition_View(id);
        DataTable dt = new DataTable();
        dt.Load(dr);
        grdviewApprovaldata.DataSource = dt;
        grdviewApprovaldata.DataBind();
        dr.Close();
        Portalcon.DisConnect();

    }
    protected void btnViewgrid_Command(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        lblViewID.Text = id;
        View_Requition_For_Approval(id);
        ModalPopupExtender1.Show();
        txtRemarksforApproval.Text = "";
        lblerror.Text = "";
    }
    string NoSeriesdata = ""; string FnoSeries = ""; string lineNofromdt = "";
    public void showNoSeries()
    {
        string NoseriesTable="";
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        NoseriesTable = "[" + rccname + "$No_ Series Line" + "]";
        SqlDataReader dr = con.Show_NoSeries(NoseriesTable);
        dr.Read();
        if (dr.HasRows)
        {
            NoSeriesdata = dr["Starting No_"].ToString();
            lineNofromdt = dr["Line No_"].ToString();
        }
        dr.Close();
        con.DisConnect();

        
        //FnoSeries = NoSeriesdata.Substring(NoSeriesdata.IndexOf('/') + 1);

        //FnoSeries = NoSeriesdata.Substring(NoSeriesdata.IndexOf('-') + 1);


    }

    public void TopNoSeries()
    {
        string tablename1 = ""; string topnoseries1 = "";
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        tablename1 = "[" + rccname + "$Requistion Header" + "]";
        SqlDataReader drt = con.Show_TopNoSeries(tablename1);
        drt.Read();
        if (drt.HasRows)
        {
            string TopNofSeries = drt["No"].ToString();
            if (TopNofSeries.Trim() == "")
            {
                TopNofSeries = "0";
            }
            //topnoseries1 = TopNofSeries.Substring(TopNofSeries.IndexOf('-') + 1);
            //topnoseries1 = TopNofSeries.Substring(TopNofSeries.IndexOf('/') + 1);
            int ab = Convert.ToInt32(topnoseries1) + 1;
            RequisitionNo = ab.ToString();

        }

        else
        {
            RequisitionNo = NoSeriesdata.ToString();
        }
        drt.Close();
        con.DisConnect();
    }

    string RequisitionNo = ""; string tablenameLine = "";
    protected void btnApproval_Click(object sender, EventArgs e)
    {
      //  string topnoseries1 = "";
      //  showNoSeries();
      //  string tablename1 = ""; int Line_No=0;string typeu="";
      //  string ccname = Session["Company"].ToString();
      //  string rccname = ccname.Replace(".", "_");
      //  tablename1 = "[" + rccname + "$Requistion Header" + "]";
      //   tablenameLine = "[" + rccname + "$Requistion Line" + "]";
      //  int blankint=0; string blankstring = ""; string Item_To_Purchase = ""; string Reason_For_Purchase = ""; string Approxe_Cost = ""; string Request_Date = ""; string Vendor = "";
      //  string Remarks = ""; string StoreId = ""; string Store = ""; string Current_Date_Time = ""; string Request_Created_UserId = ""; string Request_Created_UserName = "";
      //  string Request_Created_UserEmailid = ""; string HOD_Userid = ""; string HOD_Name = ""; string HOD_EmailID = ""; string Requisition_Type = ""; string Vendor_Mobile_No = "";
      //  string Vendor_Address = ""; string Quantity = ""; string Requisition_Type_id = ""; string departmentCode = ""; string Campus = ""; string Vendor_Contact_Person = "";string NoSeries="REQ";
      //  string UserRequestUserid="";string blankDates="1753-01-01 00:00:00.000";string BlankTime="1753-01-01 00:00:00.000";
      //  string Creationdate = ""; string Creationtime = ""; int onevalue = 1; string reqCreaterName = ""; string requestCreaterEmail = "";
      ////  con.Insert_Requition_data_afterApproval
      //  SqlDataReader dr = Portalcon.show_Tble_Requition_View(lblViewID.Text);
      //  dr.Read();
      //  if (dr.HasRows)
      //  {
      //      Requisition_Type_id = dr["Requisition_Type_id"].ToString();
      //      Item_To_Purchase = dr["Item_To_Purchase"].ToString();
      //      Reason_For_Purchase = dr["Reason_For_Purchase"].ToString();
      //      Approxe_Cost = dr["Approxe_Cost"].ToString();
      //      Quantity = dr["Quantity"].ToString();
      //      departmentCode = dr["Department Code"].ToString();
      //      Campus = dr["Campus"].ToString();
      //     // Request_Date = dr["Request_Date"].ToString();
      //    //  DateTime dt = Convert.ToDateTime(txtDate.Text);
      //    //  Request_Date = dt.ToString("yyyy-MM-dd");
      //      Store = dr["StoreId"].ToString();
      //      Remarks = dr["Remarks"].ToString();
      //      Vendor = dr["Vendor"].ToString();
      //      Vendor_Mobile_No = dr["Vendor_Mobile_No"].ToString();
      //      Vendor_Contact_Person = dr["Vendor_Contact_Person"].ToString();
      //      Vendor_Address = dr["Vendor_Address"].ToString();
      //      UserRequestUserid=dr["Request_Created_UserId"].ToString();
      //      string Current_Date_Time1=dr["Current_Date_Time"].ToString();
      //      DateTime curd=Convert.ToDateTime(Current_Date_Time1);
      //      Creationdate=curd.ToString("yyyy-MM-dd");
      //      Creationtime= curd.ToString("HH:mm:ss");
      //      reqCreaterName = dr["Request_Created_UserName"].ToString();
      //      requestCreaterEmail = dr["Request_Created_UserEmailid"].ToString();
      //  }
      //  dr.Close();
      //  Portalcon.DisConnect();

      //  SqlDataReader drt = con.Show_TopNoSeries(tablename1);
      //  drt.Read();
      //  if (drt.HasRows)
      //  {
      //      string TopNofSeries = drt["No"].ToString();
      //      if (TopNofSeries.Trim() == "")
      //      {
      //          TopNofSeries = "0";
      //      }
      //      //AK/15-16/000001
      //    //REQ/AK/15-16/000001
            
      //      //topnoseries1 = TopNofSeries.Substring(TopNofSeries.IndexOf("REQ/AK/15-16/") + 1);
      //      //topnoseries1 = topnoseries1.Replace("REQ/AK/15-16/", "");
      //      string top1 = TopNofSeries.Replace("REQ/AK/15-16/", "");
          
      //      int ab = Convert.ToInt32(top1) + 1;
      //      RequisitionNo = "REQ/AK/15-16/" + String.Format("{0:D6}", ab);
           
           
           
      //  }

      //  else
      //  {
      //      RequisitionNo = NoSeriesdata.ToString();
      //  }
      //  drt.Close();
      //  con.DisConnect();

      //  con.Insert_Requition_data_afterApproval(tablename1, RequisitionNo, Campus, Convert.ToDateTime(Request_Date), NoSeries, Convert.ToDateTime(Request_Date), UserRequestUserid, blankint, blankint, blankint, blankint, departmentCode, Store, blankint, blankstring, blankstring, blankint, blankint, blankstring,Convert.ToDateTime( blankDates),Convert.ToDateTime( BlankTime),Convert.ToDateTime(Creationdate),Convert.ToDateTime(Creationtime), blankint, blankstring, blankstring, blankstring,Convert.ToDateTime(blankDates), UserRequestUserid, blankstring, Session["uid"].ToString(), blankint, blankint, onevalue, blankint, Convert.ToInt32(2), Convert.ToDateTime(System.DateTime.Now), Convert.ToInt32(Requisition_Type_id));
      //  con.DisConnect();
      //  Line_No = Convert.ToInt32(lineNofromdt);
      //  con.Insert_Requition_data_afterApprovalinLine(tablenameLine, RequisitionNo, Line_No, blankstring, Item_To_Purchase, blankstring, Convert.ToDecimal(Quantity), blankstring, blankstring, blankint, blankint, blankstring, blankstring, "0", Convert.ToDecimal(0), Convert.ToDecimal(0), blankstring, Store, Requisition_Type_id, Convert.ToDecimal(0), Convert.ToDecimal(Quantity), Convert.ToDecimal(0), blankint, "0", blankstring, Remarks, blankstring, Convert.ToDecimal(0), blankstring, blankint, Convert.ToDecimal(0), blankstring, blankstring, Reason_For_Purchase, Convert.ToDecimal(0), blankint, Convert.ToDateTime(blankDates), Convert.ToDecimal(0), "2", blankint, blankint, Convert.ToDecimal(Quantity), blankstring, blankstring, Convert.ToDateTime(Request_Date), Convert.ToDecimal(Approxe_Cost), Convert.ToInt32(Requisition_Type_id));
      //  Portalcon.tble_Requition_ApprovalStatus("Approved", txtRemarksforApproval.Text, Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd")), lblViewID.Text);
      //  Portalcon.DisConnect();
      //  lblerror.Text = "Approved Successfully......";

      //  subject1 = "Approved Requisition Request of " + Request_Date;

      //  Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}", Environment.NewLine, Environment.NewLine, reqCreaterName, Environment.NewLine, Environment.NewLine, "You request for requisition of  " + Item_To_Purchase + " , '" + " Approved ", Environment.NewLine, "Remarks :  " + txtRemarksforApproval.Text + "", Environment.NewLine, "Please check .", Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());



      //  ShowMailData(requestCreaterEmail.ToString());
      //  ModalPopupExtender1.Show();
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        string Request_Date = ""; string reqCreaterName = ""; string requestCreaterEmail = ""; string Item_To_Purchase = "";
        if (txtRemarksforApproval.Text.Trim() == "")
        {

            lblerror.Text = "Please fill remarks.";
        }
        else
        {

            SqlDataReader dr = Portalcon.show_Tble_Requition_View(lblViewID.Text);
            dr.Read();
            if (dr.HasRows)
            {
                
              //  Request_Date = dr["Request_Date"].ToString();
               // DateTime dt = Convert.ToDateTime(txtDate.Text);
               // Request_Date = dt.ToString("yyyy-MM-dd");
                Item_To_Purchase = dr["Item_To_Purchase"].ToString();
                reqCreaterName = dr["Request_Created_UserName"].ToString();
                requestCreaterEmail = dr["Request_Created_UserEmailid"].ToString();
            }
            dr.Close();
            Portalcon.DisConnect();

            Portalcon.tble_Requition_ApprovalStatus("Rejected", txtRemarksforApproval.Text, Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd")), lblViewID.Text);
            Portalcon.DisConnect();
            lblerror.Text = "Rejected Successfully......";

            subject1 = "Rejected Request of " + Request_Date;

            Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}", Environment.NewLine, Environment.NewLine, reqCreaterName, Environment.NewLine, Environment.NewLine, "You request for requisition of  " + Item_To_Purchase + " , '" + " Rejected ", Environment.NewLine, "Remarks :  " + txtRemarksforApproval.Text + "", Environment.NewLine, "Please check .", Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());



            ShowMailData(requestCreaterEmail.ToString());
        }
        ModalPopupExtender1.Show();
    }
    protected void lnkClose_Click(object sender, EventArgs e)
    {
        pnlUserRequest.Visible = false;
        pnlViewRequest.Visible = false;
        pnlPending_Approval.Visible = true;
        ShowHODPendingRequestAll("Pending");
    }
    protected void lnlApprovalStatus_Click(object sender, EventArgs e)
    {
        pnlUserRequest.Visible = false;
        pnlViewRequest.Visible = false;
        pnlPending_Approval.Visible = false;
        pnlApprovalSatus.Visible = true;
    }


    public void Show_Approval_RequestedStatusHOD(string Approval_Status)
    {
        if (txtFromdateApprovalStatus.Text == "" || txtTodateApprovalStatus.Text == "")
        { }

        else
        {
            SqlDataReader dr = Portalcon.Show_tble_Requition_Approval_statusofHOD(Session["Company"].ToString(), Session["uid"].ToString(), Convert.ToDateTime(txtFromdateApprovalStatus.Text.Trim()), Convert.ToDateTime(txtTodateApprovalStatus.Text.Trim()), Approval_Status);
            DataTable dt = new DataTable();
            dt.Load(dr);
            grdApprovalStatus.DataSource = dt;
            grdApprovalStatus.DataBind();
            dr.Close();
            Portalcon.DisConnect();
        }
    }
    protected void btnSearchApproval_Click(object sender, EventArgs e)
    {
        Show_Approval_RequestedStatusHOD(ddApprovalStatus.SelectedItem.Text);
    }
    protected void grdViewdetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdViewdetail.PageIndex = e.NewPageIndex;
        if (ddstatus.SelectedItem.Text == "All")
        {
            ShowRequestDataAll();
        }
        else
        {
            ShowRequestDatawithStatus(ddstatus.SelectedItem.Text.Trim());
        }
    }
    protected void grdPendingApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPendingApproval.PageIndex = e.NewPageIndex;
        ShowHODPendingRequestAll("Pending");
    }
    protected void grdApprovalStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdApprovalStatus.PageIndex = e.NewPageIndex;
        Show_Approval_RequestedStatusHOD(ddApprovalStatus.SelectedItem.Text);
    }

    public void View_Requition_ForUserwithid(string id)
    {
        SqlDataReader dr = Portalcon.show_Tble_Requition_View(id);
        DataTable dt = new DataTable();
        dt.Load(dr);
        grdViewRequestionforuser.DataSource = dt;
        grdViewRequestionforuser.DataBind();
        dr.Close();
        Portalcon.DisConnect();

    }
    protected void btnViewgridRequest_Command(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        View_Requition_ForUserwithid(id);
        SqlDataReader dr = Portalcon.show_Tble_Requition_View(id);
        dr.Read();
        if (dr.HasRows)
        {
            string aprstatus = dr["Approval_Status"].ToString();

            if (aprstatus == "Approved")
            {
                grdViewRequestionforuser.Columns[11].Visible = false;
            }
            else
            {
                grdViewRequestionforuser.Columns[11].Visible = true;
            }
        }
        dr.Close();
        Portalcon.DisConnect();
        ModalPopupExtender2.Show();
    }


    public void showHODApprovalDataview(string id)
    {
        
        SqlDataReader dr = Portalcon.show_Tble_Requition_View(id);
        DataTable dt = new DataTable();
        dt.Load(dr);
        grdApprovallhodview.DataSource = dt;
        grdApprovallhodview.DataBind();
        dr.Close();
        Portalcon.DisConnect();

    
    }
    //public void show_Department()
    //{
    //   // string tble_Name = "[" + Session["Company"] + "$" + "Department Master]";
    //  //SqlDataReader dr = con.Show_Department(tble_Name);
    //    string tble_Name = "[" + Session["Company"] + "$" + "Dimension Value]";        
    //    SqlDataReader dr = con.Show_Department1(tble_Name);
    //    ddDepartment.DataSource = dr;
    //    ddDepartment.DataTextField = "Department Description";
    //    ddDepartment.DataValueField = "Department Code";
    //    ddDepartment.DataBind();
    //    dr.Close();
    //    con.DisConnect();

    //}

    //protected void btnResend_Click(object sender, EventArgs e)
    //{
    //    if (txtApprox.Text.Trim() == "")
    //    {

    //    }

    //    else
    //    {
    //        if (txtQuantity.Text.Trim() == "")
    //        {
    //            txtQuantity.Text = "0";

    //        }

    //        Portalcon.Update_intotbl_Requisition_user_Request(txtItemToPurchase.Text.Trim(), txtReason.Text.Trim(), Convert.ToDecimal(txtApprox.Text), Convert.ToDateTime(txtDate.Text.Trim()), txtVendor.Text.Trim(), txtRemarks.Text, ddStore.SelectedValue.ToString(), ddStore.SelectedItem.Text, Convert.ToDateTime(System.DateTime.Now), ddRequistionType.SelectedItem.Text, txtMobileMo.Text.Trim(), txtVendor.Text.Trim(), Convert.ToDecimal(txtQuantity.Text.Trim()), ddRequistionType.SelectedValue.ToString(), txtContactPerson.Text.Trim(), ddDepartment.SelectedItem.Text.Trim(), ddDepartment.SelectedValue.ToString(), txtCampus.Text, Session["idresend"].ToString());
    //        Portalcon.DisConnect();
    //        btnResend.Visible = false;
    //        btnCancel.Visible = false;
    //        btn_sendforAproval.Visible = true;
    //        subject1 = "Resend User Request of " + System.DateTime.Now.ToString("dd-MM-yyyy");

    //        Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}", Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "I have send request for requisition of  " + txtItemToPurchase.Text + " , '" + Environment.NewLine, "'Approxe Cost : " + txtApprox.Text + "   Quantity : " + txtQuantity.Text + " " + Environment.NewLine, "Remarks :  " + txtReason.Text + "", Environment.NewLine, "Kindly approve it.", Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());



    //        ShowMailData(Session["hod_email2"].ToString());
    //        cleardata();
    //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Thanks for your interest, Your request send successfully');", true);
    //    }
    //    pnlUserRequest.Visible = false;
    //    pnlViewRequest.Visible = true;
    //    pnlPending_Approval.Visible = false;
    //    pnlApprovalSatus.Visible = false;
    //    if (ddstatus.SelectedItem.Text == "All")
    //    {
    //        ShowRequestDataAll();
    //    }
    //    else
    //    {
    //        ShowRequestDatawithStatus(ddstatus.SelectedItem.Text.Trim());
    //    }
    //}

    //protected void lnkEditorResend_Command(object sender, CommandEventArgs e)
    //{

    //    pnlUserRequest.Visible = true;
    //    btnResend.Visible = true;
    //    btnCancel.Visible = true;
    //    btn_sendforAproval.Visible = false;
    //    string id=e.CommandArgument.ToString();
    //    Session["idresend"] = id.ToString();
    //    pnlViewRequest.Visible = false;
    //    SqlDataReader dr = Portalcon.ShowRequitionforResend(id);
    //    dr.Read();
    //    if (dr.HasRows)
    //    {
    //        ddRequistionType.SelectedValue = dr["Requisition_Type_id"].ToString();
    //        txtItemToPurchase.Text = dr["Item_To_Purchase"].ToString();
    //        txtReason.Text = dr["Reason_For_Purchase"].ToString();
    //        txtApprox.Text = dr["Approxe_Cost"].ToString();
    //        txtQuantity.Text = dr["Quantity"].ToString();
    //        ddDepartment.SelectedValue = dr["Department Code"].ToString();
    //        txtCampus.Text = dr["Campus"].ToString();
    //        txtDate.Text = dr["Request_Date"].ToString();
    //        DateTime dt = Convert.ToDateTime(txtDate.Text);
    //        txtDate.Text = dt.ToString("yyyy-MM-dd");
    //        ddStore.Text = dr["StoreId"].ToString();
    //        txtRemarks.Text = dr["Remarks"].ToString();
    //        txtVendor.Text = dr["Vendor"].ToString();
    //        txtMobileMo.Text = dr["Vendor_Mobile_No"].ToString();
    //        txtContactPerson.Text = dr["Vendor_Contact_Person"].ToString();
    //        txtAddress.Text = dr["Vendor_Address"].ToString();
    //    }
    //    dr.Close();
    //    con.DisConnect();
    //}
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnResend.Visible = false;
        btnCancel.Visible = false;
        btn_sendforAproval.Visible = true;
        cleardata();
        pnlUserRequest.Visible = false;
        pnlViewRequest.Visible = true;
        pnlPending_Approval.Visible = false;
        pnlApprovalSatus.Visible = false;
        if (ddstatus.SelectedItem.Text == "All")
        {
            ShowRequestDataAll();
        }
        else
        {
            ShowRequestDatawithStatus(ddstatus.SelectedItem.Text.Trim());
        }
    }
    protected void btnViewApprovaldetails_Command(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        showHODApprovalDataview(id);
        ModalPopupExtender3.Show();
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    
    public static List<string> SearchItems(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            DataTable dt = new DataTable();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "Sp_SerchItem";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ItemSearch", prefixText.ToUpper());               
                cmd.Connection = conn;
                conn.Open();               
                List<string> item = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        item.Add(sdr["Description"].ToString());
                    }
                }
                conn.Close();
                return item;
            }
        }
    }
    protected void txtItemToPurchase_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable(); String ItemCode = "";
        if (txtItemToPurchase.Text.Contains('(') == true && txtItemToPurchase.Text.Contains(')') == true)
        {
            ItemCode = txtItemToPurchase.Text.Split('(', ')')[1].Trim();
            btn_sendforAproval.Enabled = true;
        }
        else
        {
            btn_sendforAproval.Enabled = false;
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Item Not Found !');", true);
            txtItemToPurchase.Text = "";
            return;
        }
        
        if (ItemCode != "")
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "Sp_GetVarianceUOMByItemCode";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ItemCode", ItemCode.Trim());
                    cmd.Connection = conn;
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    dt.Load(sdr);
                    dt.AcceptChanges();
                    conn.Close();
                }
            }
            //dt = sdl.GetStudentDetails(ItemCode);
            if (dt.Rows.Count > 0)
            {
                lblUOM.Text = dt.Rows[0]["Base Unit of Measure"].ToString();
                ddlVariance.DataSource = dt;
                ddlVariance.DataValueField = "Code";
                ddlVariance.DataTextField = "Description";
                ddlVariance.DataBind();
                // btnSave.Enabled = true;
            }
        }
        else
        {
            txtItemToPurchase.Text = "";
            lblUOM.Text = "";
            ddlVariance.DataSource = null;
            ddlVariance.DataBind();

        }
    }
 
    protected void btnADD_Click(object sender, EventArgs e)
    {
        lblRequestError.Text = ""; string ItemCode = ""; String ItemDescription = "";
        try
        {
           Convert.ToDecimal(txtQty.Text);
        }
        catch
        {
            lblRequestError.Text = "Please Enter Correct Quantity";
            return;
        }
        
        if (txtItemToPurchase.Text.Contains('(') == true && txtItemToPurchase.Text.Contains(')') == true)
        {
            ItemDescription = txtItemToPurchase.Text.Split('(', ')')[0].Trim();
            ItemCode = txtItemToPurchase.Text.Split('(', ')')[1].Trim();            
            btnADD.Enabled = true;
        }
        else
        {
            btnADD.Enabled = false;
        }
        if (ddlVariance.SelectedItem.Text == "-- Select --")
        {
            lblRequestError.Text = "please Select Variance.";
            return;
        }
        else
        {
            if (grdAddItem.Rows.Count > 0)
            {
                dtAddItem = (DataTable)ViewState["AddItem"];
                DataView dv = new DataView(dtAddItem);
                dv.RowFilter = "ItemCode='" + ItemCode + "' and Variance='" + ddlVariance.SelectedItem.Text + "'";
                if (dv.Count > 0)
                {
                    lblRequestError.Text = "Item already Added.";
                }
                else
                {
                    dtAddItem.Rows.Add(ItemCode, ItemDescription, ddlVariance.SelectedValue, ddlVariance.SelectedItem.Text, txtQty.Text, lblUOM.Text, txtRemarks.Text);
                    grdAddItem.DataSource = dtAddItem;
                    grdAddItem.DataBind();
                    blankTable();
                }
            }
            else
            {
                dtAddItem.Columns.AddRange(new DataColumn[7]{
                    new DataColumn ("ItemCode",typeof(String)),                    
                    new DataColumn ("ItemDescription",typeof(string)),
                    new DataColumn ("VarianceCode",typeof(string)),
                    new DataColumn ("Variance",typeof(string)),
                    new DataColumn ("Qty",typeof(decimal)),
                    new DataColumn ("UOM",typeof(string))   ,
                     new DataColumn ("Remarks",typeof(string))                    
                    });
                dtAddItem.Rows.Add(ItemCode, ItemDescription,ddlVariance.SelectedValue,ddlVariance.SelectedItem.Text, txtQty.Text, lblUOM.Text,txtRemarks.Text);
                grdAddItem.DataSource = dtAddItem;
                grdAddItem.DataBind();
                ViewState["AddItem"] = dtAddItem;
                rblDepartment.Enabled = false;
                blankTable();
            }


        }
        if (grdAddItem.Rows.Count > 0)
         btn_sendforAproval.Visible = true;
        else btn_sendforAproval.Visible = false; 
    }
    public void blankTable()
    {
        txtItemToPurchase.Text = "";
        ddlVariance.SelectedIndex = -1;
        txtQty.Text = "";
        txtRemarks.Text = "";
        if (grdAddItem.Rows.Count > 0) { btn_sendforAproval.Visible = true; rblDepartment.Enabled = false; }
        else { btn_sendforAproval.Visible = false; rblDepartment.Enabled = true; }
    }
       
    protected void grdAddItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DeleteItem"))
        {
            GridViewRow rowItem = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            int index = rowItem.RowIndex;
            DataTable dt = ViewState["AddItem"] as DataTable;
            dt.Rows[index].Delete();
            grdAddItem.DataSource = dt;
            grdAddItem.DataBind();
            ViewState["AddItem"] = dt;
            blankTable();          
            
        }
    }

}