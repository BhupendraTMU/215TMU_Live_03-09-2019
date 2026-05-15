using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
//using Ionic.Zip;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Collections;
using System.Configuration;

public partial class reimbursement : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal con;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Portalcon = new Connection();
            con = new ServicePoratal();
            Showpermission();
            Areuexhist();
            if (!IsPostBack)
            {
                Typeof_Reimbursment();
                showClaimTypefinal();
                ShowBasicPay();
                //279
                ReimLimit();
                Show_Minus_Limit();
                con.delete_Claim_ApplyAll(Session["uid"].ToString(), Session["Company"].ToString(), "Created");
                con.DisConnect();
                con.delete_All_Attachment(Session["Company"].ToString(), Session["uid"].ToString(), "Created");
                con.DisConnect();
                ShowClaim();
                showdataattachement();
                lastrecDocumentNo();
               
            }
           
        }
        catch (Exception)
        {
            Response.Redirect("Default.aspx");
        }
    }



    public void Areuexhist()
    {
       
        SqlDataReader dr = Portalcon.SHow_showHODExhist(Session["uid"].ToString(), Session["CompanyTableEmployee"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            dr.Close();
            con.DisConnect();
            lnkClaim_Approval.Visible = true;
            lnkAprovalstatus.Visible = true;
          imgClaim_Approval.Visible = true;
          imgAprovalstatus.Visible = true;
        }
        else
        {
            dr.Close();
            con.DisConnect();
            lnkClaim_Approval.Visible = false;
            lnkAprovalstatus.Visible = false;
            imgClaim_Approval.Visible = false;
            imgAprovalstatus.Visible = false;
        }
    }


    string lblslno = "";
    public void lastrec()
    {
        string updatedate = System.DateTime.Now.ToString("yyyy-MM-dd");
        SqlDataReader dr = con.SHow_SerialNo(Session["uid"].ToString(), updatedate, Session["Company"].ToString());

        dr.Read();
        if (dr.HasRows)
        {

            string d = dr["SerialNo"].ToString();
            double rec = Convert.ToInt64(d);
            double c1 = rec + 1;
            lblslno = c1.ToString();
        }

        else
        {
            lblslno = "1";

        }

        dr.Close();
        con.DisConnect();



    }

    protected void lnkreimbursmentApply_Click(object sender, EventArgs e)
    {
        rdCSVFile.Checked = false;
        pnlClaimAprovalstatus.Visible = false;
        rdEntryForm.Checked = false;
        pnlPendingClaimApproval.Visible = false;
        pnlreimbursmentregister.Visible = false;
        pnlviewReimbursment.Visible = false;
        pnlCSVFile.Visible = false;
        pnlMain.Visible = true;
        Typeof_Reimbursment();
        showClaimTypefinal();

       
        ReimLimit();
        Show_Minus_Limit();

        clear();

       


        con.delete_Claim_ApplyAll(Session["uid"].ToString(), Session["Company"].ToString(), "Created");
        con.DisConnect();
        con.delete_All_Attachment(Session["Company"].ToString(), Session["uid"].ToString(), "Created");
        con.DisConnect();
        ShowClaim();
        showdataattachement();
        lastrecDocumentNo();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        pnlClaimAprovalstatus.Visible = false;
        pnlreimbursmentregister.Visible = false;
        pnlviewReimbursment.Visible = true;
        pnlMain.Visible = false;
        pnlCSVFile.Visible = false;
        pnlPendingClaimApproval.Visible = false;
        
    }



    public void show_ReimbursmentdetailWithSearchCondition()
    {
        if (ddStatus.Text == "All")
        {
            SqlDataReader odr = con.Show_ReimbursmentDetailwithcom_UseridDateALL(Session["Company"].ToString(), Session["uid"].ToString(), txtFromdate.Text, txtToDate.Text);
            DataTable Dt = new DataTable();
            Dt.Load(odr);
            GRDSearchDetail.DataSource = Dt;
            GRDSearchDetail.DataBind();
            odr.Close();
            con.DisConnect();

        }
        else
        {
            //SqlDataReader odr = con.Show_ReimbursmentDetailwithcom_UseridDate(Session["Company"].ToString(), Session["uid"].ToString());
            SqlDataReader odr = con.Show_ReimbursmentDetailwithcom_UseridDate(Session["Company"].ToString(), Session["uid"].ToString(), txtFromdate.Text, txtToDate.Text, ddStatus.Text);
            DataTable Dt = new DataTable();
            Dt.Load(odr);
            GRDSearchDetail.DataSource = Dt;
            GRDSearchDetail.DataBind();
            odr.Close();
            con.DisConnect();
        }

    }


    string mailfrom = ""; string subject1 = ""; string Body1 = ""; string smtpfromportal = ""; int portNo1; string Pass_From = ""; string Mail_Sending_Option = ""; string Leave_Applymail = ""; string CCMail = "";
    public void ShowMailData(string mailTo1)
    {

        SqlDataReader dr = con.Show_tble_MailSetup(Session["Company"].ToString(), "Reimbursement");
        dr.Read();
        if (dr.HasRows)
        {
            mailfrom = dr["from_Email"].ToString();
            smtpfromportal = dr["smtp"].ToString();
            Pass_From = dr["Password_From"].ToString();
            CCMail = dr["CCMail"].ToString();
            string portNo = dr["Port_No"].ToString();
            portNo1 = Convert.ToInt32(portNo);
            Mail_Sending_Option = dr["Mail_Sending_Option"].ToString();
            Leave_Applymail = dr["Reimbursment_Apply"].ToString();

        }

        dr.Close();
        con.DisConnect();
        if (Mail_Sending_Option == "1" && Leave_Applymail == "True")
        {
            SendMail(mailTo1);
        }

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

                msg.From = new MailAddress(Session["CompanyEmail"].ToString().Trim(), "  " + Session["Fulname"].ToString().Trim());
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
                //if (FileUpload1.HasFile)
                //{
                //    Attachment Attachment = new System.Net.Mail.Attachment(Server.MapPath("~/Reimbursment/" + bill1));
                //    msg.Attachments.Add(Attachment);
                //}

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


    public void clear()
    {
        txtNoofBills.Text = "0";
        txtHardCopies.Text = "0";
        txtAttached.Text = "0";
        txtDate.Text = "";
        txtAmount.Text = "";
        txtRemarks.Text = "";
        txtNoofBills.Text = "";
        btnAdd.Visible = true;
        btncancel.Visible = false;
        btnUpdate.Visible = false;
    }
    protected void grdReimbursment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdReimbursment.PageIndex = e.NewPageIndex;
        ShowClaim();
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        show_ReimbursmentdetailWithSearchCondition();
    }
    protected void btnDelete1_Command(object sender, CommandEventArgs e)
    {
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {
            string id = e.CommandArgument.ToString();
            con.Delete_tbl_reimbursmentDetail(id);
            con.DisConnect();
            show_ReimbursmentdetailWithSearchCondition();
        }

    }

    protected void lnkView_Command(object sender, CommandEventArgs e)
    {
        string valuedata = e.CommandArgument.ToString();
        // Response.Redirect(valuedata);
        Page.ClientScript.RegisterStartupScript(
    this.GetType(), "OpenWindow", "window.open('" + valuedata + "','_newtab');", true);


    }

    protected void lnkviewsearch_Command(object sender, CommandEventArgs e)
    {
        string valuedata = e.CommandArgument.ToString();
        // Response.Redirect(valuedata);
        Page.ClientScript.RegisterStartupScript(
    this.GetType(), "OpenWindow", "window.open('" + valuedata + "','_newtab');", true);


    }


    string HODapr = "";
    string HRapr = "";
    string Blankapr = "";
    string PriorityHRapr = "";
    string PriorityHODapr = "";
    public void Showpermission()
    {
        string type = "For Reimbursment";
        SqlDataReader dr = con.SHow_tblesetupforpriorityforApproval(type, Session["Company"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            HODapr = dr["HOD"].ToString();

            HRapr = dr["HR"].ToString();

            Blankapr = dr["Blank"].ToString();

            PriorityHRapr = dr["PriorityHR"].ToString();

            PriorityHODapr = dr["PriorityHOD"].ToString();

        }
        dr.Close();
        con.DisConnect();
    }


    public void lastrecDocumentNo()
    {

        SqlDataReader dr = con.New_DocumentNo(Session["Company"].ToString());

        dr.Read();
        if (dr.HasRows)
        {

            string d = dr["Document_No"].ToString();
            double rec = Convert.ToInt64(d);
            double c1 = rec + 1;
            txtDocumentno.Text = c1.ToString();
        }

        else
        {
            txtDocumentno.Text = "1";

        }

        dr.Close();
        con.DisConnect();



    }

    protected void GRDSearchDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblBill_Detailsearch1 = (e.Row.Cells[0].FindControl("lblBill_Detailsearch") as Label);

            string textdata = lblBill_Detailsearch1.Text;
            string output = textdata.Substring(textdata.IndexOf('$') + 1);
            lblBill_Detailsearch1.Text = output;
        }
    }
    protected void txtEXAmount_TextChanged(object sender, EventArgs e)
    {
        //calcreditminus();
    }

    public void ShowClaim()
    {

        SqlDataReader dr1 = con.SHow_ClaimTypeOFUser(Session["Company"].ToString(), Session["uid"].ToString(), "Created");
        dr1.Read();
        if (dr1.HasRows)
        {
            dr1.Close();
            con.DisConnect();
            btnsend_for_approval.Visible = true;
            flp_file_attachement.Visible = true;
            btnuploadFile.Visible = true;
            lblBillaAttach.Visible = true;
            pnlAttachEntry.Visible = true;
            btnclear.Visible = true;
            SqlDataReader dr = con.SHow_ClaimTypeOFUser(Session["Company"].ToString(), Session["uid"].ToString(), "Created");
            DataTable Dt = new DataTable();
            Dt.Load(dr);
            grdReimbursment.DataSource = Dt;
            grdReimbursment.DataBind();
            dr.Close();
            con.DisConnect();
        }
        else
        {
            dr1.Close();
            con.DisConnect();
            btnsend_for_approval.Visible = false;
            flp_file_attachement.Visible = false;
            btnclear.Visible = false;
            btnuploadFile.Visible = false;
            lblBillaAttach.Visible = false;
            pnlAttachEntry.Visible = false;
            con.delete_All_Attachment(Session["Company"].ToString(), Session["uid"].ToString(), "Created");
            con.DisConnect();
           
            showdataattachement();
            SqlDataReader dr = con.SHow_ClaimTypeOFUser(Session["Company"].ToString(), Session["uid"].ToString(), "Created");
            DataTable Dt = new DataTable();
            Dt.Load(dr);
            grdReimbursment.DataSource = Dt;
            grdReimbursment.DataBind();
            dr.Close();
            con.DisConnect();
        }

    }
    public void showhodofHOD()
    {
        string stable = Session["Company"].ToString();
        string rtable = stable.Replace(".", "_");
        string tblNameEmployee = "[" + rtable + "$Employee" + "]";
        SqlDataReader dr = Portalcon.Show_HODEmail(tblNameEmployee,  Session["hodofhod"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            Session["HODofHODEmail"] = dr["Company E-Mail"].ToString();
            Session["HODofHODName"] = dr["First Name"].ToString() + "  " + dr["Middle Name"].ToString() + " " + dr["Last Name"].ToString();
          

        }
        dr.Close();
        con.DisConnect();
    }

    public void sendfor_Approval()
    {

        SqlDataAdapter da = new SqlDataAdapter("select * from tble_claim_Apply where Company_Name='" + Session["Company"].ToString() + "' and Userid = '" + Session["uid"].ToString() + "' and Approval_Status='Created'", con.Con);
        DataSet ds = new DataSet();
        da.Fill(ds, "tble_claim_Apply");
        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
        {

            string selfApproval = ""; string id = ""; string RM1 = ""; string RM2 = ""; string RM1_Email = ""; string RM2_Email = ""; string Approval_Status = "Pending"; string Claim_created_Date = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm"); string RM1_Name = ""; string RM2_Name = ""; string ApprovalTypesend = ""; string Self_Approvalopt = ""; decimal Self_Limit; string RM1_Approval = ""; decimal RM1_Limit; string RM2_Approval = ""; decimal RM2_Limit; string ClaimAmt = ""; string fr = "";
            string RM2_Approval_Required = "No";
            try
            {


                id = ds.Tables[0].Rows[i]["id"].ToString();
                ApprovalTypesend = ds.Tables[0].Rows[i]["Approval_type"].ToString();
                  ClaimAmt = ds.Tables[0].Rows[i]["Claim_Amount"].ToString();
                decimal ClaimAmtf=Convert.ToDecimal(ClaimAmt);
                SqlDataReader dr = con.SHow_LimitToApprovalType(Session["Company"].ToString(), ApprovalTypesend);
                dr.Read();
                if (dr.HasRows)
                {
                    Self_Approvalopt = dr["Self_Approval"].ToString();
                  string  Self_Limit1 = dr["Self_Limit"].ToString();
                  Self_Limit = Convert.ToDecimal(Self_Limit1);
                    RM1_Approval = dr["RM1_Approval"].ToString();
                   string  RM1_Limit1 = dr["RM1_Limit"].ToString();
                    RM1_Limit=Convert.ToDecimal(RM1_Limit1);
                    RM2_Approval = dr["RM2_Approval"].ToString();
                 string RM2_Limit1 = dr["RM2_Limit"].ToString();
                 RM2_Limit = Convert.ToDecimal(RM2_Limit1);
                    dr.Close();
                    if (Self_Approvalopt == "Yes" && ClaimAmtf <= Self_Limit)
                    {
                       string First_ApprovalUserid = "";
                        Approval_Status = "Approved";
                        con.update_Claim_Apply(Session["uid"].ToString(), RM1, RM2, RM1_Email, RM2_Email, Approval_Status, RM1_Name, RM2_Name, id, RM2_Approval_Required, RM1_Limit, RM2_Limit, Self_Limit, ClaimAmtf, System.DateTime.Now.ToString(), First_ApprovalUserid);
                        con.DisConnect();
                        //fr = "self";
                    }

                    if (RM1_Approval == "Yes")
                    {
                        if (ClaimAmtf > Self_Limit && ClaimAmtf <= RM1_Limit)
                        {
                            string First_ApprovalUserid = Session["HODLoginPage"].ToString();
                            decimal claimapr = Convert.ToDecimal("00.00");
                            string aprdate = "";
                            con.update_Claim_Apply(selfApproval, Session["HODLoginPage"].ToString(), RM2, Session["hod_email2"].ToString(), RM2_Email, Approval_Status, Session["hod_Name2"].ToString(), RM2_Name, id, RM2_Approval_Required, RM1_Limit, RM2_Limit, Self_Limit, claimapr, aprdate, First_ApprovalUserid);
                            con.DisConnect();
                        
                        }
                    }
                    if (RM2_Approval == "Yes")
                    {
                        if (ClaimAmtf > RM1_Limit && ClaimAmtf <= RM2_Limit)
                        {
                            string First_ApprovalUserid = Session["HODLoginPage"].ToString();
                            RM2_Approval_Required = "Yes";
                          //  showhodofHOD();
                            //con.update_Claim_Apply(selfApproval, RM1, Session["HODLoginPage"].ToString(), RM1_Email, Session["HODofHODEmail"].ToString(), Approval_Status, RM1_Name, Session["HODofHODName"].ToString(), id, RM2_Approval_Required);
                            //con.DisConnect();
                            decimal claimapr = Convert.ToDecimal("00.00");
                            string aprdate = "";
                            con.update_Claim_Apply(selfApproval, Session["HODLoginPage"].ToString(), RM2, Session["hod_email2"].ToString(), RM2_Email, Approval_Status, Session["hod_Name2"].ToString(), RM2_Name, id, RM2_Approval_Required, RM1_Limit, RM2_Limit, Self_Limit, claimapr, aprdate, First_ApprovalUserid);
                            con.DisConnect();
                        
                        }
                    }
                    if (RM1_Approval == "No")
                    {
                        if (ClaimAmtf > Self_Limit)
                        {
                            string First_ApprovalUserid = Session["HODLoginPage"].ToString();
                            decimal claimapr = Convert.ToDecimal("00.00");
                            string aprdate = "";
                            con.update_Claim_Apply(selfApproval, Session["HODLoginPage"].ToString(), RM2, Session["hod_email2"].ToString(), RM2_Email, Approval_Status, Session["hod_Name2"].ToString(), RM2_Name, id, RM2_Approval_Required, RM1_Limit, RM2_Limit, Self_Limit, claimapr, aprdate, First_ApprovalUserid);
                            con.DisConnect();
                        }
                    }
                    

                }

               


            }
            catch (Exception)
            { }
        }
        con.DisConnect();
        
        ShowClaim();
       
    }




  






    public void Typeof_Reimbursment()
    {

        SqlDataAdapter daj = new SqlDataAdapter("delete from tble_temp_Rem_type where Userid='" + Session["uid"].ToString() + "' and Company_Name='" + Session["Company"].ToString() + "'  select * from tble_ReimBursementtype_Master where Applicable_for='Individual' and Company_Name='" + Session["Company"].ToString() + "' and Active='Yes'", con.Con);
        DataSet dsj = new DataSet();
        daj.Fill(dsj, "tble_ReimBursementtype_Master");

        for (int j = 0; j <= dsj.Tables[0].Rows.Count - 1; j++)
        {
            string rem_type = ""; string Approval_typeid = "";
            string Approval_typeind = ""; string Indvidual_useridind = ""; string Effective_datef = "";
            rem_type = dsj.Tables[0].Rows[j]["Reim_Type"].ToString();
            Approval_typeind = dsj.Tables[0].Rows[j]["Approval_type"].ToString();
            Indvidual_useridind = dsj.Tables[0].Rows[j]["Indvidual_userid"].ToString();
            Approval_typeid = dsj.Tables[0].Rows[j]["Approval_typeid"].ToString();


            string Effective_date = dsj.Tables[0].Rows[j]["Effective_date"].ToString();
            DateTime effectdate = Convert.ToDateTime(Effective_date);
            Effective_datef = effectdate.ToString("yyyy-MM-dd");
            string curdatetime = System.DateTime.Now.ToString("yyyy-MM-dd");
            DateTime statartdate1 = DateTime.ParseExact(Effective_datef.Trim(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            DateTime enddate1 = DateTime.ParseExact(curdatetime, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            if (statartdate1 > enddate1)
            {



            }
            else
            {



                if (Indvidual_useridind == Session["uid"].ToString())
                {

                    SqlDataReader dr = con.SHow_rem_typeRep(rem_type, Session["uid"].ToString(), Session["Company"].ToString());
                    dr.Read();
                    if (dr.HasRows)
                    {
                        dr.Close();

                    }
                    else
                    {
                        dr.Close();
                        con.insert_tble_temp_Rem_type(Session["uid"].ToString(), Session["Company"].ToString(), rem_type, Approval_typeind, "Individual", Approval_typeid);
                    }
                }
            }
        }
        con.DisConnect();


        SqlDataAdapter dad = new SqlDataAdapter("select * from tble_ReimBursementtype_Master where Applicable_for='Department' and Company_Name='" + Session["Company"].ToString() + "' and Active='Yes'", con.Con);
        DataSet dsd = new DataSet();
        dad.Fill(dsd, "tble_ReimBursementtype_Master");

        for (int j = 0; j <= dsd.Tables[0].Rows.Count - 1; j++)
        {
            string rem_type = ""; string Approval_typeid = "";
            string Approval_typeind = ""; string Department = ""; string Effective_datef = "";
            rem_type = dsd.Tables[0].Rows[j]["Reim_Type"].ToString();
            Approval_typeind = dsd.Tables[0].Rows[j]["Approval_type"].ToString();
            Department = dsd.Tables[0].Rows[j]["Department"].ToString();
            Approval_typeid = dsd.Tables[0].Rows[j]["Approval_typeid"].ToString();

            string Effective_date = dsd.Tables[0].Rows[j]["Effective_date"].ToString();
            DateTime effectdate = Convert.ToDateTime(Effective_date);
            Effective_datef = effectdate.ToString("yyyy-MM-dd");
            string curdatetime = System.DateTime.Now.ToString("yyyy-MM-dd");
            DateTime statartdate1 = DateTime.ParseExact(Effective_datef.Trim(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            DateTime enddate1 = DateTime.ParseExact(curdatetime, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            if (statartdate1 > enddate1)
            {



            }
            else
            {


                if (Department == Session["Departmentcode"].ToString())
                {

                    SqlDataReader dr = con.SHow_rem_typeRep(rem_type, Session["uid"].ToString(), Session["Company"].ToString());
                    dr.Read();
                    if (dr.HasRows)
                    {
                        dr.Close();

                    }
                    else
                    {
                        dr.Close();
                        con.insert_tble_temp_Rem_type(Session["uid"].ToString(), Session["Company"].ToString(), rem_type, Approval_typeind, "Department", Approval_typeid);
                    }
                }
            }
        }
        con.DisConnect();

        SqlDataAdapter daa = new SqlDataAdapter("select * from tble_ReimBursementtype_Master where Applicable_for='All' and Company_Name='" + Session["Company"].ToString() + "' and Active='Yes'", con.Con);
        DataSet dsa = new DataSet();
        daa.Fill(dsa, "tble_ReimBursementtype_Master");

        for (int j = 0; j <= dsa.Tables[0].Rows.Count - 1; j++)
        {
            string rem_type = "";
            string Approval_typeind = ""; string Approval_typeid = "";
            rem_type = dsa.Tables[0].Rows[j]["Reim_Type"].ToString(); string Effective_datef = "";
            Approval_typeind = dsa.Tables[0].Rows[j]["Approval_type"].ToString();
            Approval_typeid = dsa.Tables[0].Rows[j]["Approval_typeid"].ToString();
            string Effective_date = dsa.Tables[0].Rows[j]["Effective_date"].ToString();
            DateTime effectdate = Convert.ToDateTime(Effective_date);
            Effective_datef = effectdate.ToString("yyyy-MM-dd");
            string curdatetime = System.DateTime.Now.ToString("yyyy-MM-dd");
            DateTime statartdate1 = DateTime.ParseExact(Effective_datef.Trim(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            DateTime enddate1 = DateTime.ParseExact(curdatetime, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            if (statartdate1 > enddate1)
            {



            }
            else
            {

                SqlDataReader dr = con.SHow_rem_typeRep(rem_type, Session["uid"].ToString(), Session["Company"].ToString());
                dr.Read();
                if (dr.HasRows)
                {
                    dr.Close();

                }
                else
                {
                    dr.Close();
                    con.insert_tble_temp_Rem_type(Session["uid"].ToString(), Session["Company"].ToString(), rem_type, Approval_typeind, "All", Approval_typeid);
                }

            }
        }
        con.DisConnect();

    }

    public void Typeof_ReimbursmentAdvance()
    {

        SqlDataAdapter daj = new SqlDataAdapter("delete from tble_temp_Rem_type where Userid='" + Session["uid"].ToString() + "' and Company_Name='" + Session["Company"].ToString() + "'  select * from tble_ReimBursementtype_Master where Applicable_for='Individual' and Company_Name='" + Session["Company"].ToString() + "' and Active='Yes' and Reim_Type=''", con.Con);
        DataSet dsj = new DataSet();
        daj.Fill(dsj, "tble_ReimBursementtype_Master");

        for (int j = 0; j <= dsj.Tables[0].Rows.Count - 1; j++)
        {
            string rem_type = ""; string Approval_typeid = "";
            string Approval_typeind = ""; string Indvidual_useridind = ""; string Effective_datef = "";
            rem_type = dsj.Tables[0].Rows[j]["Reim_Type"].ToString();
            Approval_typeind = dsj.Tables[0].Rows[j]["Approval_type"].ToString();
            Indvidual_useridind = dsj.Tables[0].Rows[j]["Indvidual_userid"].ToString();
            Approval_typeid = dsj.Tables[0].Rows[j]["Approval_typeid"].ToString();


            string Effective_date = dsj.Tables[0].Rows[j]["Effective_date"].ToString();
            DateTime effectdate = Convert.ToDateTime(Effective_date);
            Effective_datef = effectdate.ToString("yyyy-MM-dd");
            string curdatetime = System.DateTime.Now.ToString("yyyy-MM-dd");
            DateTime statartdate1 = DateTime.ParseExact(Effective_datef.Trim(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            DateTime enddate1 = DateTime.ParseExact(curdatetime, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            if (statartdate1 > enddate1)
            {



            }
            else
            {



                if (Indvidual_useridind == Session["uid"].ToString())
                {

                    SqlDataReader dr = con.SHow_rem_typeRep(rem_type, Session["uid"].ToString(), Session["Company"].ToString());
                    dr.Read();
                    if (dr.HasRows)
                    {
                        dr.Close();

                    }
                    else
                    {
                        dr.Close();
                        con.insert_tble_temp_Rem_type(Session["uid"].ToString(), Session["Company"].ToString(), rem_type, Approval_typeind, "Individual", Approval_typeid);
                    }
                }
            }
        }
        con.DisConnect();


        SqlDataAdapter dad = new SqlDataAdapter("select * from tble_ReimBursementtype_Master where Applicable_for='Department' and Company_Name='" + Session["Company"].ToString() + "' and Active='Yes'", con.Con);
        DataSet dsd = new DataSet();
        dad.Fill(dsd, "tble_ReimBursementtype_Master");

        for (int j = 0; j <= dsd.Tables[0].Rows.Count - 1; j++)
        {
            string rem_type = ""; string Approval_typeid = "";
            string Approval_typeind = ""; string Department = ""; string Effective_datef = "";
            rem_type = dsd.Tables[0].Rows[j]["Reim_Type"].ToString();
            Approval_typeind = dsd.Tables[0].Rows[j]["Approval_type"].ToString();
            Department = dsd.Tables[0].Rows[j]["Department"].ToString();
            Approval_typeid = dsd.Tables[0].Rows[j]["Approval_typeid"].ToString();

            string Effective_date = dsd.Tables[0].Rows[j]["Effective_date"].ToString();
            DateTime effectdate = Convert.ToDateTime(Effective_date);
            Effective_datef = effectdate.ToString("yyyy-MM-dd");
            string curdatetime = System.DateTime.Now.ToString("yyyy-MM-dd");
            DateTime statartdate1 = DateTime.ParseExact(Effective_datef.Trim(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            DateTime enddate1 = DateTime.ParseExact(curdatetime, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            if (statartdate1 > enddate1)
            {



            }
            else
            {


                if (Department == Session["Departmentcode"].ToString())
                {

                    SqlDataReader dr = con.SHow_rem_typeRep(rem_type, Session["uid"].ToString(), Session["Company"].ToString());
                    dr.Read();
                    if (dr.HasRows)
                    {
                        dr.Close();

                    }
                    else
                    {
                        dr.Close();
                        con.insert_tble_temp_Rem_type(Session["uid"].ToString(), Session["Company"].ToString(), rem_type, Approval_typeind, "Department", Approval_typeid);
                    }
                }
            }
        }
        con.DisConnect();

        SqlDataAdapter daa = new SqlDataAdapter("select * from tble_ReimBursementtype_Master where Applicable_for='All' and Company_Name='" + Session["Company"].ToString() + "' and Active='Yes'", con.Con);
        DataSet dsa = new DataSet();
        daa.Fill(dsa, "tble_ReimBursementtype_Master");

        for (int j = 0; j <= dsa.Tables[0].Rows.Count - 1; j++)
        {
            string rem_type = "";
            string Approval_typeind = ""; string Approval_typeid = "";
            rem_type = dsa.Tables[0].Rows[j]["Reim_Type"].ToString(); string Effective_datef = "";
            Approval_typeind = dsa.Tables[0].Rows[j]["Approval_type"].ToString();
            Approval_typeid = dsa.Tables[0].Rows[j]["Approval_typeid"].ToString();
            string Effective_date = dsa.Tables[0].Rows[j]["Effective_date"].ToString();
            DateTime effectdate = Convert.ToDateTime(Effective_date);
            Effective_datef = effectdate.ToString("yyyy-MM-dd");
            string curdatetime = System.DateTime.Now.ToString("yyyy-MM-dd");
            DateTime statartdate1 = DateTime.ParseExact(Effective_datef.Trim(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            DateTime enddate1 = DateTime.ParseExact(curdatetime, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            if (statartdate1 > enddate1)
            {



            }
            else
            {

                SqlDataReader dr = con.SHow_rem_typeRep(rem_type, Session["uid"].ToString(), Session["Company"].ToString());
                dr.Read();
                if (dr.HasRows)
                {
                    dr.Close();

                }
                else
                {
                    dr.Close();
                    con.insert_tble_temp_Rem_type(Session["uid"].ToString(), Session["Company"].ToString(), rem_type, Approval_typeind, "All", Approval_typeid);
                }

            }
        }
        con.DisConnect();

    }









    DateTime MaxDate;
    public void ShowBasicPay()
    {
       
          string stable =Session["Company"].ToString();
        string rtable = stable.Replace(".", "_");
        string comanyname = "[" + rtable + "$Pay Employee Elements" + "]";

        SqlDataReader dr1 = Portalcon.SHow_Employee_Basic_MaxDate(Session["uid"].ToString(), comanyname);
        dr1.Read();
        if (dr1.HasRows)
        {
            
         string  MaxDate1 = dr1["PayDate"].ToString();
         MaxDate =Convert.ToDateTime(MaxDate1); 
        }
        dr1.Close();
        Portalcon.DisConnect();
        SqlDataReader dr = Portalcon.SHow_Employee_Basic_Pay(Session["uid"].ToString(), comanyname, MaxDate);
        dr.Read();
        if (dr.HasRows)
        {
            string lblBasicPay1 = dr["Amount (LCY)"].ToString();
            decimal lblBasicPay2 = Convert.ToDecimal(lblBasicPay1);
            lblBasicPay.Text = lblBasicPay2.ToString("00.00");
        }
        dr.Close();
        Portalcon.DisConnect();
    
    }



    //public void ShowBasicPayCSV()
    //{

    //    string stable = Session["Company"].ToString();
    //    string rtable = stable.Replace(".", "_");
    //    string comanyname = "[" + rtable + "$Pay Employee Elements" + "]";

    //    SqlDataReader dr1 = Portalcon.SHow_Employee_Basic_MaxDate(Session["uid"].ToString(), comanyname);
    //    dr1.Read();
    //    if (dr1.HasRows)
    //    {

    //        string MaxDate1 = dr1["PayDate"].ToString();
    //        MaxDate = Convert.ToDateTime(MaxDate1);
    //    }
    //    dr1.Close();
    //    Portalcon.DisConnect();
    //    SqlDataReader dr = Portalcon.SHow_Employee_Basic_Pay(Session["uid"].ToString(), comanyname, MaxDate);
    //    dr.Read();
    //    if (dr.HasRows)
    //    {
    //        lblCSVLimit.Text = dr["Amount (LCY)"].ToString();
    //    }
    //    dr.Close();
    //    Portalcon.DisConnect();

    //}

    public void ReimLimit()
    {
        if (ddExpenseType.SelectedValue == "")
        { }

        else
        {
            SqlDataReader dr = con.SHow_Reim_Approvalid(ddExpenseType.SelectedValue);
            dr.Read();
            if (dr.HasRows)
            {
                string Approval_type = dr["Approval_type"].ToString();
                lblApproval_type.Text = Approval_type;

                dr.Close();
                SqlDataReader drapr = con.SHow_Reim_Approvalidwithlimit(Approval_type);
                drapr.Read();
                string Fixed_Amount = drapr["Fixed_Amount"].ToString();
                string Percentage_Of_Basic_Pay = drapr["Percentage_Of_Basic_Pay"].ToString();
                lbllimit_text.Text = drapr["Limit"].ToString();

                string Display_limit = drapr["Display_limit"].ToString();
                string Display_Balance = drapr["Display_Balance"].ToString();
                if (Fixed_Amount == "Yes")
                {
                    lblMaxLimit.Text = drapr["Amount"].ToString();
                }
                if (Percentage_Of_Basic_Pay == "Yes")
                {
                    //lblMaxLimit.Text = drapr["Percentage"].ToString();
                    string percentagelimit = drapr["Percentage"].ToString();
                    decimal percentagel = Convert.ToDecimal(percentagelimit);
                    decimal basicpay = Convert.ToDecimal(lblBasicPay.Text);

                    decimal basicpay1 = basicpay * percentagel / 100;
                    lblMaxLimit.Text = basicpay1.ToString("00.00");

                }
                if (Display_limit == "Yes")
                {
                    pnllimit.Visible = true;
                }
                if (Display_limit == "No")
                {
                    pnllimit.Visible = false;
                }
                if (Display_Balance == "Yes")
                {
                    pnlbalancelimit.Visible = true;
                }
                if (Display_Balance == "No")
                {
                    pnlbalancelimit.Visible = false;
                }

                drapr.Close();

            }
            dr.Close();
            con.DisConnect();
        }
    }






    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string First_ApprovalUserid = "";
        string damt = "00.00";
        decimal AprMT = Convert.ToDecimal(damt);
        string appdat = "";
               string RM2_Approval_Required = "No";
        if (!IsPostBack)
        {
            lastrecDocumentNo();
        }
        
        if (txtAmount.Text.Trim() == "" || txtAmount.Text.Trim() == "0.00" || txtAmount.Text.Trim() == "0" || txtAmount.Text.Trim() == "00.00")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please Fill Correct Claim Amount');", true); 
        }
        else
        {
            if (ddExpenseType.SelectedItem.Text == "Salary Advance")
            {
                if (txtNoofMonth.Text == "" || txtNoofMonth.Text == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter No of month.');", true);
                
                }
                else
                {
                    string DocumentNoAlphaNumeric = "CMP-ASK-" + txtDocumentno.Text.Trim();
                    Show_Minus_Limit();
                   
                    string selfAppro = ""; string RM1 = ""; string RM2 = ""; string RM1_Email = ""; string RM2_Email = ""; string Approval_Status = "Created"; string Claim_created_Date = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm"); string RM1_Name = ""; string RM2_Name = "";
                    con.Claim_Apply_tble_claim_Apply(Session["Fulname"].ToString(), Session["uid"].ToString(), Session["CompanyEmail"].ToString(), ddExpenseType.SelectedItem.Text.ToString(), txtDate.Text, txtAmount.Text, txtRemarks.Text, selfAppro, RM1, RM2, RM1_Email, RM2_Email, Approval_Status, Claim_created_Date, RM1_Name, RM2_Name, Session["Company"].ToString(), ddExpenseType.SelectedValue, Amountexceed, excAmount.ToString(), lblApproval_type.Text, ddBillAttach.Text, lblbalanceAmount.Text, txtNoofBills.Text, txtDocumentno.Text.Trim(), Convert.ToInt32(txtAttached.Text.Trim()), Convert.ToInt32(txtHardCopies.Text.Trim()), DocumentNoAlphaNumeric, Convert.ToInt32(txtNoofMonth.Text), RM2_Approval_Required, AprMT, AprMT, AprMT, AprMT, appdat, First_ApprovalUserid);
                    con.DisConnect();

                    clear();
                    ShowClaim();
                    ShowNoOfattach();
                }
            }
            else
            {
                string DocumentNoAlphaNumeric = "CMP-ASK-" + txtDocumentno.Text.Trim();
                Show_Minus_Limit();

                string selfAppro = ""; string RM1 = ""; string RM2 = ""; string RM1_Email = ""; string RM2_Email = ""; string Approval_Status = "Created"; string Claim_created_Date = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm"); string RM1_Name = ""; string RM2_Name = "";
                con.Claim_Apply_tble_claim_Apply(Session["Fulname"].ToString(), Session["uid"].ToString(), Session["CompanyEmail"].ToString(), ddExpenseType.SelectedItem.Text.ToString(), txtDate.Text, txtAmount.Text, txtRemarks.Text, selfAppro, RM1, RM2, RM1_Email, RM2_Email, Approval_Status, Claim_created_Date, RM1_Name, RM2_Name, Session["Company"].ToString(), ddExpenseType.SelectedValue, Amountexceed, excAmount.ToString(), lblApproval_type.Text, ddBillAttach.Text, lblbalanceAmount.Text, txtNoofBills.Text, txtDocumentno.Text.Trim(), Convert.ToInt32(txtAttached.Text.Trim()), Convert.ToInt32(txtHardCopies.Text.Trim()), DocumentNoAlphaNumeric, Convert.ToInt32(txtNoofMonth.Text), RM2_Approval_Required, AprMT, AprMT, AprMT, AprMT, appdat, First_ApprovalUserid);
                con.DisConnect();

                clear();
                ShowClaim();
                ShowNoOfattach();
            }
        }
        //Label8.Text = ddExpenseType.SelectedValue;
    }
    protected void btnDeleteclaim_Command(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();

        con.delete_Claim_Apply(id);
        con.DisConnect();
        ShowClaim();

    }

    //int fileCount = 0;
    //public void ProcessRequest(HttpContext context)
    //{
    //    if (context.Request.Files.Count > 0)
    //    {
    //        HttpFileCollection files = context.Request.Files;
    //        for (int i = 0; i < files.Count; i++)
    //        {
    //            HttpPostedFile file = files[i];
    //            string fname = context.Server.MapPath("~/uploads/" + file.FileName);
    //            file.SaveAs(fname);
    //            fileCount++;
    //        }
    //        context.Response.ContentType = "text/plain";
    //        context.Response.Write(fileCount + " File Uploaded Successfully!");
    //    }

    //}

    //public bool IsReusable
    //{
    //    get
    //    {
    //        return false;
    //    }
    //}
 
   
    protected void btnsend_for_approval_Click(object sender, EventArgs e)
    {
              

        sendfor_Approval();
        con.update_Attachement(txtDocumentno.Text, Session["Company"].ToString(), "Pending", Session["uid"].ToString());
        con.DisConnect();
        lastrecDocumentNo();
        showdataattachement();
    }

    protected void DownloadFile(object sender, EventArgs e)
    {
        string id = ((sender as LinkButton).CommandArgument);
        byte[] bytes;
        string fileName, contentType;
        //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        //using (SqlConnection con = new SqlConnection())
        //{
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "select File_Attachment_Name, Data, ContentType from tble_Attachment where id=@Id";
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Connection = con.Con;
                con.Connect();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Data"];
                    contentType = sdr["ContentType"].ToString();
                    fileName = sdr["File_Attachment_Name"].ToString();
                }
                con.DisConnect();
            }
      //  }
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
    public void showdataattachement()
    {
        SqlDataReader dr = con.SHow_Attachement(txtDocumentno.Text,Session["Company"].ToString(),Session["uid"].ToString());
        DataTable Dt = new DataTable();
        Dt.Load(dr);
        grdAttachment.DataSource = Dt;
        grdAttachment.DataBind();
        dr.Close();
        con.DisConnect();

    }

    public void showdataattachementCSV()
    {
        SqlDataReader dr = con.SHow_Attachement(txtDocumentno.Text, Session["Company"].ToString(), Session["uid"].ToString());
        DataTable Dt = new DataTable();
        Dt.Load(dr);
        grdAttachmentcsv.DataSource = Dt;
        grdAttachmentcsv.DataBind();
        dr.Close();
        con.DisConnect();

    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        Session["idedReims"] = id.ToString();
        SqlDataReader dr = con.SHow_ClaimDetailid(id);
        dr.Read();
        if (dr.HasRows)
        {
            btnUpdate.Visible = true;
            btncancel.Visible = true;
            btnAdd.Visible = false;
      
         ddExpenseType.SelectedItem.Text = dr["Claim_type"].ToString();
         Disablenoofmonth();  
          ddBillAttach.SelectedValue = dr["Bill_Attach"].ToString();
            string claimd = dr["Claim_Date"].ToString();
            DateTime Claim_Date = Convert.ToDateTime(claimd);
            txtDate.Text = Claim_Date.ToString("yyyy-MM-dd");
            txtAmount.Text = dr["Claim_Amount"].ToString();
            txtRemarks.Text = dr["Remarks"].ToString();
            txtNoofBills.Text = dr["No_of_Attachment"].ToString();
            txtAttached.Text = dr["Attached"].ToString();
            txtHardCopies.Text = dr["HardCopies"].ToString();
            txtNoofMonth.Text = dr["Noof_Month"].ToString();
            dr.Close();
            con.DisConnect();

          

        }
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        con.delete_Claim_ApplyAll(Session["uid"].ToString(), Session["Company"].ToString(), "Created");
        con.DisConnect();
        con.delete_All_Attachment( Session["Company"].ToString(), Session["uid"].ToString(), "Created");
        con.DisConnect();
        ShowClaim();
        showdataattachement();
        ReimLimit();
        Show_Minus_Limit();
        
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (txtAmount.Text.Trim() == "" || txtAmount.Text.Trim() == "0.00" || txtAmount.Text.Trim() == "0" ||txtAmount.Text.Trim() == "00.00")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please Fill Correct Claim Amount');", true);
        }
        else
        {

            if (ddExpenseType.SelectedItem.Text == "Salary Advance")
            {
                if (txtNoofMonth.Text == "" || txtNoofMonth.Text == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter No of month.');", true);

                }
                else
                {
                    ReimLimit();
                    Show_Minus_Limit();
                    string billDetails = "";
                    con.Update_Claim_ApplyAll(ddExpenseType.SelectedItem.Text.ToString(), txtDate.Text, txtAmount.Text, txtRemarks.Text, billDetails, Session["idedReims"].ToString(), ddExpenseType.SelectedValue, Amountexceed, excAmount.ToString(), lblApproval_type.Text, ddBillAttach.Text, lblbalanceAmount.Text, Convert.ToInt32(txtNoofBills.Text), Convert.ToInt32(txtAttached.Text.Trim()), Convert.ToInt32(txtHardCopies.Text.Trim()), Convert.ToInt32(txtNoofMonth.Text));
                    con.DisConnect();
                    clear();
                    ShowClaim();
                    ShowNoOfattach();
                }
            }
            else
            {

                ReimLimit();
                Show_Minus_Limit();
                string billDetails = "";
                con.Update_Claim_ApplyAll(ddExpenseType.SelectedItem.Text.ToString(), txtDate.Text, txtAmount.Text, txtRemarks.Text, billDetails, Session["idedReims"].ToString(), ddExpenseType.SelectedValue, Amountexceed, excAmount.ToString(), lblApproval_type.Text, ddBillAttach.Text, lblbalanceAmount.Text, Convert.ToInt32(txtNoofBills.Text), Convert.ToInt32(txtAttached.Text.Trim()), Convert.ToInt32(txtHardCopies.Text.Trim()), Convert.ToInt32(txtNoofMonth.Text));
                con.DisConnect();
                clear();
                ShowClaim();
                ShowNoOfattach();
            }
        }

    }

    //public void showdataty()
    //{
    //    Label5.Text = ddExpenseType.SelectedValue.ToString();
    //}


    public void showClaimTypefinal()
    {

        SqlDataReader dr = con.SHow_rem_typeFinal(Session["uid"].ToString(), Session["Company"].ToString());
        ddExpenseType.DataSource = dr;
        ddExpenseType.DataTextField = "Rem_Type";
        ddExpenseType.DataValueField = "id";
        ddExpenseType.DataBind();
        dr.Close();
        con.DisConnect();

    }

    protected void ddExpenseType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //protected void ddExpenseType_SelectedIndexChanged1(object sender, EventArgs e)
    //{
    //    Label5.Text = ddExpenseType.SelectedItem.Text.ToString();
    //}

 
    public void Disablenoofmonth()
    {
        try
        {
            if (ddExpenseType.SelectedItem.Text == "Salary Advance")
            {
                //Salary Advance
                //Travelling Advance
                //Other Advances

                txtNoofMonth.Text = "0";
                txtNoofMonth.Enabled = true;
                txtHardCopies.Text = "0";
                txtAttached.Text = "0";
                txtNoofMonth.Text = "0";
                txtNoofBills.Text = "0";
                txtHardCopies.Enabled = false;
                txtAttached.Enabled = false;
                txtNoofBills.Enabled = false;
                ddBillAttach.Text = "";
                ddBillAttach.Enabled = false;
            }
            else if (ddExpenseType.SelectedItem.Text == "Other Advances")
            {
                //Salary Advance
                //Travelling Advance
                //Other Advances

                txtNoofMonth.Text = "0";
                txtNoofMonth.Enabled = false;
                txtHardCopies.Text = "0";
                txtAttached.Text = "0";
                txtNoofMonth.Text = "0";
                txtNoofBills.Text = "0";
                txtHardCopies.Enabled = false;
                txtAttached.Enabled = false;
                txtNoofBills.Enabled = false;
                ddBillAttach.Text = "";
                ddBillAttach.Enabled = false;
            }

            else if (ddExpenseType.SelectedItem.Text == "Travelling Advance")
            {
                //Salary Advance
                //Travelling Advance
                //Other Advances

                txtNoofMonth.Text = "0";
                txtNoofMonth.Enabled = false;
                txtHardCopies.Text = "0";
                txtAttached.Text = "0";
                txtNoofMonth.Text = "0";
                txtNoofBills.Text = "0";
                txtHardCopies.Enabled = false;
                txtAttached.Enabled = false;
                txtNoofBills.Enabled = false;
                ddBillAttach.Text = "";
                ddBillAttach.Enabled = false;
            }
            else
            {
                txtHardCopies.Text = "0";
                txtAttached.Text = "0";
                txtNoofMonth.Text = "0";
                txtNoofBills.Text = "0";
                txtHardCopies.Enabled = true;
                txtAttached.Enabled = true;
                txtNoofMonth.Enabled = true;
                txtNoofBills.Enabled = true;
                txtNoofMonth.Text = "0";
                txtNoofMonth.Enabled = false;
                ddBillAttach.Text = "";
                ddBillAttach.Enabled = true;
            }
        }
        catch (Exception)
        { }
    }
    protected void ddExpenseType_SelectedIndexChanged1(object sender, EventArgs e)
    {
        ReimLimit();
        Show_Minus_Limit();
        Disablenoofmonth();  
    }
    string Amountexceed = ""; decimal excAmount ;
    public void Show_Minus_Limit()
    {
        string takenAmountprev = "";
        try
        {

            //if (txtAmount.Text == "")
            //{
                
            //}

            //else
            //{
            if (txtAmount.Text == "")
            {
                txtAmount.Text = "0";
            }

                decimal maxl = Convert.ToDecimal(lblMaxLimit.Text);
                decimal inputAmt = Convert.ToDecimal(txtAmount.Text);
                //decimal limittext = Convert.ToDecimal(lbllimit_text.Text);

                if (lbllimit_text.Text == "Yearly")
                {
                    SqlDataReader drclaimsum = con.Sum_Claim_Amount_tble_claim_Apply(Session["Company"].ToString(), Session["uid"].ToString(), ddExpenseType.SelectedItem.Text);
                    drclaimsum.Read();
                    string takenAmount = drclaimsum["Claim_Amount"].ToString();
                    if (takenAmount == "")
                    {
                        takenAmountprev = "0";
                        decimal takemamt = Convert.ToDecimal(takenAmountprev);

                        takenAmountprev = (takemamt + inputAmt).ToString();
                        takenAmount2 = Convert.ToDecimal((takemamt + inputAmt));
                    }
                    else
                    {
                        decimal takemamt = Convert.ToDecimal(takenAmount);

                        takenAmountprev = (takemamt + inputAmt).ToString();
                        takenAmount2 = Convert.ToDecimal((takemamt + inputAmt));
                    }

                    drclaimsum.Close();



                    if (takenAmount2 > maxl)
                    {
                        
                        lblError.Text = "Amount Exceed ";
                        Amountexceed = "Yes";
                        decimal actualAmt = Convert.ToDecimal(takenAmountprev);
                        excAmount = actualAmt - maxl;
                        //excAmount = inputAmt - maxl;
                        lblbalanceAmount.Text = "0";
                        
                    }


                    else
                    {
                        decimal actualAmt = Convert.ToDecimal(takenAmountprev);
                        lblError.Text = "";
                        Amountexceed = "No";
                        excAmount = 0;
                        lblbalanceAmount.Text = (maxl - actualAmt).ToString();
                        

                    }
                
                }
                else
                {
                    if (inputAmt > maxl)
                    {
                        lblError.Text = "Amount Exceed ";
                        Amountexceed = "Yes";
                        excAmount = inputAmt - maxl;
                        lblbalanceAmount.Text = "0";

                    }


                    else
                    {
                        lblError.Text = "";
                        Amountexceed = "No";
                        excAmount = 0;
                        lblbalanceAmount.Text = "0";
                    }
                }
            }
        //}
        catch (Exception)
        {
           // ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please Fill Correct Claim Amount');", true); 

        }
    }
    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        Show_Minus_Limit();
    }
    protected void rdEntryForm_CheckedChanged(object sender, EventArgs e)
    {
        pnlreimbursmentregister.Visible = true;
        pnlCSVFile.Visible = false;
        con.delete_Claim_ApplyAll(Session["uid"].ToString(), Session["Company"].ToString(), "Created");
        con.DisConnect();
        con.delete_All_Attachment(Session["Company"].ToString(), Session["uid"].ToString(), "Created");
        con.DisConnect();
        ShowClaim();
        showdataattachement();
        Disablenoofmonth();
    }
    protected void rdCSVFile_CheckedChanged(object sender, EventArgs e)
    {
        pnlreimbursmentregister.Visible = false;
        pnlCSVFile.Visible = true;
        pnlCSvFileuploadattached.Visible = false;
        con.delete_tble_Claim_CSV_Data_notsend_Approve(Session["Company"].ToString(), Session["uid"].ToString());
        con.DisConnect();
        con.delete_All_Attachment(Session["Company"].ToString(), Session["uid"].ToString(), "Created");
        con.DisConnect();
        Show_CSVdata_send();
        showdataattachementCSV();
       
    }
    public void ImportCSV()
    {
        con.delete_tble_Claim_CSV_Data_Temp(Session["Company"].ToString(), Session["uid"].ToString());
        con.DisConnect();
        DataTable tblcsv = new DataTable();

        tblcsv.Columns.Add("Sl_No");
        tblcsv.Columns.Add("Claim_Type");
        tblcsv.Columns.Add("Date");
        tblcsv.Columns.Add("Amount");
        tblcsv.Columns.Add("Bill_Attach");
        tblcsv.Columns.Add("HardCopies");
        tblcsv.Columns.Add("Attached");
        tblcsv.Columns.Add("No_Of_Bills");
        tblcsv.Columns.Add("Noof_Month");
        tblcsv.Columns.Add("Remarks");
       
        tblcsv.Columns.Add("Company_name");
        tblcsv.Columns.Add("Userid");

        if (flPCSV.HasFile)
        {

            string CSVFilePath = Session["Company"].ToString() + Session["uid"].ToString() + flPCSV.PostedFile.FileName;

            string spath = "~/IMPORTCSVFile/" + CSVFilePath;

            flPCSV.SaveAs(Server.MapPath("~/IMPORTCSVFile/") + CSVFilePath);

            string ReadCSV = File.ReadAllText(Server.MapPath("~/IMPORTCSVFile/") + CSVFilePath);

            foreach (string csvRow in ReadCSV.Split('\n'))
            {
                if (!string.IsNullOrEmpty(csvRow))
                {

                    tblcsv.Rows.Add();
                    int count = 0;
                    foreach (string FileRec in csvRow.Split(','))
                    {
                        tblcsv.Rows[tblcsv.Rows.Count - 1][count] = FileRec;
                        count++;
                    }
                }


            }


            InsertCSVRecords(tblcsv);
        }
    }
    private void InsertCSVRecords(DataTable csvdt)
    {
        try
        {


            //tblcsv.Columns.Add("Sl_No");
            //tblcsv.Columns.Add("Claim_Type");
            //tblcsv.Columns.Add("Date");
            //tblcsv.Columns.Add("Amount");
            //tblcsv.Columns.Add("Bill_Attach");
            //tblcsv.Columns.Add("HardCopies");
            //tblcsv.Columns.Add("Attached");
            //tblcsv.Columns.Add("No_Of_Bills");
            //tblcsv.Columns.Add("Remarks");
            //tblcsv.Columns.Add("Company_name");
            //tblcsv.Columns.Add("Userid");



            SqlBulkCopy objbulk = new SqlBulkCopy(con.Con);

            objbulk.DestinationTableName = "tble_Claim_CSV_Data_Temp";

            objbulk.ColumnMappings.Add("Sl_No", "S_No");
            objbulk.ColumnMappings.Add("Claim_Type", "Claim_type");
            objbulk.ColumnMappings.Add("Date", "Claim_Date");
            objbulk.ColumnMappings.Add("Amount", "Claim_Amount");
            objbulk.ColumnMappings.Add("Bill_Attach", "Bill_Detail");
            objbulk.ColumnMappings.Add("HardCopies", "HardCopies");
            objbulk.ColumnMappings.Add("Attached", "Attached");
            objbulk.ColumnMappings.Add("No_Of_Bills", "No_Of_Bills");
            objbulk.ColumnMappings.Add("Noof_Month", "Noof_Month");
            objbulk.ColumnMappings.Add("Remarks", "Remarks");
            objbulk.ColumnMappings.Add("Company_name", "Company_name");
            objbulk.ColumnMappings.Add("Userid", "Userid");

            con.Connect();
            objbulk.WriteToServer(csvdt);

            con.Update_tble_Claim_CSV_Data_Temp(Session["Company"].ToString(), Session["uid"].ToString());

            con.delete_tble_Claim_CSV_Data_notsend_Approve(Session["Company"].ToString(), Session["uid"].ToString());

            SqlDataAdapter daa = new SqlDataAdapter("select * from tble_Claim_CSV_Data_Temp where  Company_name='" + Session["Company"].ToString() + "' and Userid ='" + Session["uid"].ToString() + "'", con.Con);
            DataSet dsa = new DataSet();
            daa.Fill(dsa, "tble_Claim_CSV_Data_Temp");

            for (int j = 0; j <= dsa.Tables[0].Rows.Count - 1; j++)
            {
                string Claim_type = ""; string Claim_Date = ""; string Claim_Amount = ""; string Bill_Detail = ""; string Remarks = ""; string Noof_Month = "";
                string Error_Remarks = ""; string Approval_type = ""; string Applicable_For = ""; string Approval_typeid = ""; string Effective_datef = ""; string Applicable = "No"; string takenAmountprev = ""; string No_Of_Bills = ""; string Attached = ""; string HardCopies = "";
                Claim_type = dsa.Tables[0].Rows[j]["Claim_type"].ToString();
                Claim_Date = dsa.Tables[0].Rows[j]["Claim_Date"].ToString();
                Claim_Amount = dsa.Tables[0].Rows[j]["Claim_Amount"].ToString();
                Bill_Detail = dsa.Tables[0].Rows[j]["Bill_Detail"].ToString();
                Remarks = dsa.Tables[0].Rows[j]["Remarks"].ToString();
                No_Of_Bills = dsa.Tables[0].Rows[j]["No_Of_Bills"].ToString();
                Attached = dsa.Tables[0].Rows[j]["Attached"].ToString();
                HardCopies = dsa.Tables[0].Rows[j]["HardCopies"].ToString();
                Noof_Month = dsa.Tables[0].Rows[j]["Noof_Month"].ToString();
                SqlDataReader dr = con.SHow_Reim_FromCSV(Session["uid"].ToString(), Session["Company"].ToString(), Claim_type);
                dr.Read();
                if (dr.HasRows)
                {
                    Applicable = "Yes";
                    Approval_type = dr["Approval_type"].ToString();
                    Applicable_For = dr["Applicable_For"].ToString();
                    Approval_typeid = dr["Approval_typeid"].ToString();
                    dr.Close();

                    SqlDataReader drapr = con.SHow_Reim_Approvalidwithlimit(Approval_type);
                    drapr.Read();
                    string Fixed_Amount = drapr["Fixed_Amount"].ToString();
                    string Percentage_Of_Basic_Pay = drapr["Percentage_Of_Basic_Pay"].ToString();
                    string Limitcsv = drapr["Limit"].ToString();

                    
                        if (Fixed_Amount == "Yes")
                        {
                            lblCSVLimit.Text = drapr["Amount"].ToString();
                        }
                        if (Percentage_Of_Basic_Pay == "Yes")
                        {
                            //lblCSVLimit.Text = drapr["Percentage"].ToString();
                            string percentagelimit = drapr["Percentage"].ToString();
                            decimal percentagel = Convert.ToDecimal(percentagelimit);
                            decimal basicpay = Convert.ToDecimal(lblBasicPay.Text);

                            decimal basicpay1 = basicpay * percentagel / 100;
                            lblCSVLimit.Text = basicpay1.ToString();

                           
                        }
                   
                    drapr.Close();

                    if (Claim_Amount == "")
                    { }

                    else
                    {
                        decimal maxl = Convert.ToDecimal(lblCSVLimit.Text);
                        decimal inputAmt = Convert.ToDecimal(Claim_Amount);
                        if (Limitcsv == "Yearly")
                        {

                            SqlDataReader drclaimsum = con.Sum_Claim_Amount_tble_claim_Apply(Session["Company"].ToString(), Session["uid"].ToString(), Claim_type);
                            drclaimsum.Read();
                            string takenAmount = drclaimsum["Claim_Amount"].ToString();
                            if (takenAmount == "")
                            {
                                takenAmountprev = "0";
                                decimal takemamt = Convert.ToDecimal(takenAmountprev);
                               
                                takenAmountprev = (takemamt + inputAmt).ToString();
                                takenAmount2 = Convert.ToDecimal((takemamt + inputAmt));
                            }
                            else
                            {
                                decimal takemamt = Convert.ToDecimal(takenAmount);

                                takenAmountprev = (takemamt + inputAmt).ToString();
                                takenAmount2 = Convert.ToDecimal((takemamt + inputAmt));
                            }

                            drclaimsum.Close();

                            if (takenAmount2 > maxl)
                            {

                                decimal actualAmt = Convert.ToDecimal(takenAmountprev);
                              
                                //decimal examt = inputAmt - maxl;
                             decimal examt = actualAmt - maxl;
                                Error_Remarks = "Amount Exceed";
                                lblBalanceAmountcsv.Text = "0";
                                con.insert_tble_Claim_CSV_Data(Claim_type, Claim_Date, Claim_Amount, Bill_Detail, Remarks, Session["Company"].ToString(), Session["uid"].ToString(), Error_Remarks, Approval_type, Applicable_For, Applicable, "Yes", examt.ToString(), lblBalanceAmountcsv.Text, No_Of_Bills, Attached, HardCopies, Convert.ToInt32(Noof_Month));


                            }


                            else
                            {
                                decimal actualAmt = Convert.ToDecimal(takenAmountprev);
                                lblBalanceAmountcsv.Text = (maxl - actualAmt).ToString();

                                con.insert_tble_Claim_CSV_Data(Claim_type, Claim_Date, Claim_Amount, Bill_Detail, Remarks, Session["Company"].ToString(), Session["uid"].ToString(), Error_Remarks, Approval_type, Applicable_For, Applicable, "No", "0.00", lblBalanceAmountcsv.Text, No_Of_Bills, Attached, HardCopies, Convert.ToInt32(Noof_Month));

                            }
                        
                        }
                        else
                        {
                            if (inputAmt > maxl)
                            {
                                decimal examt = inputAmt - maxl;
                                Error_Remarks = "Amount Exceed";
                                lblBalanceAmountcsv.Text = "0";
                                con.insert_tble_Claim_CSV_Data(Claim_type, Claim_Date, Claim_Amount, Bill_Detail, Remarks, Session["Company"].ToString(), Session["uid"].ToString(), Error_Remarks, Approval_type, Applicable_For, Applicable, "Yes", examt.ToString(), lblBalanceAmountcsv.Text, No_Of_Bills, Attached, HardCopies, Convert.ToInt32(Noof_Month));


                            }


                            else
                            {
                                lblBalanceAmountcsv.Text = "0";

                                con.insert_tble_Claim_CSV_Data(Claim_type, Claim_Date, Claim_Amount, Bill_Detail, Remarks, Session["Company"].ToString(), Session["uid"].ToString(), Error_Remarks, Approval_type, Applicable_For, Applicable, "No", "0.00", lblBalanceAmountcsv.Text, No_Of_Bills, Attached, HardCopies,Convert.ToInt32(Noof_Month));

                            }
                        }
                    }




                 


                }
                else
                {
                    Error_Remarks = "Not Applicable";
                    dr.Close();
                    lblBalanceAmountcsv.Text = "0";
                    con.insert_tble_Claim_CSV_Data(Claim_type, Claim_Date, Claim_Amount, Bill_Detail, Remarks, Session["Company"].ToString(), Session["uid"].ToString(), Error_Remarks, Approval_type, Applicable_For, Applicable, "No", "0.00", lblBalanceAmountcsv.Text, No_Of_Bills, Attached, HardCopies, Convert.ToInt32(Noof_Month));

                }

            }
            con.DisConnect();
            con.delete_tble_Claim_CSV_Data_Temp(Session["Company"].ToString(), Session["uid"].ToString());
            Show_CSVdata_send();
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please enter Claim date in correct format i.e yyyy-MM-dd and do not use comma seperator in Column');", true);
        }
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        ImportCSV();
        ShowNoOfattachCSV();
        lastrecDocumentNo();
    }

    public void Show_CSVdata_send()
    {

        SqlDataReader dr1 = con.SHow_CSVdataforsendApproval(Session["Company"].ToString(), Session["uid"].ToString());
        dr1.Read();
        if (dr1.HasRows)
        {
            dr1.Close();
            con.DisConnect();
            btnsendforapprovalcsv.Visible = true;
            btnClearCSV.Visible = true;
          
            SqlDataReader dr = con.SHow_CSVdataforsendApproval(Session["Company"].ToString(), Session["uid"].ToString());
            DataTable Dt = new DataTable();
            Dt.Load(dr);
            grdcsvData.DataSource = Dt;
            grdcsvData.DataBind();
            dr.Close();
            con.DisConnect();
        }
        else
        {
            dr1.Close();
            con.DisConnect();
            btnsendforapprovalcsv.Visible = false;
            btnClearCSV.Visible = false;
           
            con.delete_All_Attachment(Session["Company"].ToString(), Session["uid"].ToString(), "Created");
            con.DisConnect();
            showdataattachementCSV();

            SqlDataReader dr = con.SHow_CSVdataforsendApproval(Session["Company"].ToString(), Session["uid"].ToString());
            DataTable Dt = new DataTable();
            Dt.Load(dr);
            grdcsvData.DataSource = Dt;
            grdcsvData.DataBind();
            dr.Close();
            con.DisConnect();
        }

    }



    protected void btnClearCSV_Click(object sender, EventArgs e)
    {
        con.delete_tble_Claim_CSV_Data_notsend_Approve(Session["Company"].ToString(), Session["uid"].ToString());
        con.DisConnect();
        con.delete_All_Attachment(Session["Company"].ToString(), Session["uid"].ToString(), "Created");
        con.DisConnect();

       
        showdataattachementCSV();
        Show_CSVdata_send();
        ReimLimit();
        Show_Minus_Limit();
        pnlCSvFileuploadattached.Visible = false;
       
    }
    protected void grdcsvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdcsvData.PageIndex = e.NewPageIndex;
        Show_CSVdata_send();
    }
    protected void btnDeletecsv_Command(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        con.delete_tble_Claim_CSV_Data_notsend_Approvewithid(id);
        con.DisConnect();
        Show_CSVdata_send();
    }

    //public void Remarks_ColumnHide()
    //{

    //        foreach (GridViewRow r in grdcsvData.Rows)
    //        {
    //            foreach (TableCell c in r.Cells)
    //            {
    //                c.Visible = string.IsNullOrEmpty(c.Text);

    //            }
    //        }

    //}
    protected void grdcsvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Button btnDeletecsv = (Button)e.Row.FindControl("btnDeletecsv");

           
            Label lblApplicable = (Label)e.Row.FindControl("lblApplicable");
            Label lblAmountExceed = (Label)e.Row.FindControl("lblAmountExceed");
            if (lblApplicable.Text == "No")
            {
                e.Row.BackColor = System.Drawing.Color.PaleGreen;
                Button cmdField = (Button)e.Row.Cells[12].Controls[0];
                cmdField.Visible = false;
              
            }
            if (lblApplicable.Text == "Yes")
            {
                btnDeletecsv.Enabled = true;
                Button cmdField = (Button)e.Row.Cells[12].Controls[0];
                cmdField.Visible = true;
            }

            if (lblAmountExceed.Text == "Yes")
            {
                e.Row.BackColor = System.Drawing.Color.LightCoral;
                e.Row.ForeColor = System.Drawing.Color.White;
            }
            if (lblAmountExceed.Text == "No")
            {
               
            }

        }
    }
    protected void grdcsvData_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdcsvData.EditIndex = e.NewEditIndex;
        Show_CSVdata_send();
    }
    protected void grdcsvData_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdcsvData.EditIndex = -1;
        Show_CSVdata_send();
    }
    decimal takenAmount2;
    protected void grdcsvData_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
       
        string lblCSVLimit = ""; string error_remarks = ""; string takenAmountprev = ""; 
        Label lblid = grdcsvData.Rows[e.RowIndex].FindControl("lblid") as Label;
          Label lblclaimtypecsvd = grdcsvData.Rows[e.RowIndex].FindControl("lblclaimtypecsvd") as Label;
        Label lblAppr_type = grdcsvData.Rows[e.RowIndex].FindControl("lblAppr_type") as Label;
        TextBox txtClaim_Amount = grdcsvData.Rows[e.RowIndex].FindControl("txtClaim_Amount") as TextBox;
        TextBox txtBill_Detail = grdcsvData.Rows[e.RowIndex].FindControl("txtBill_Detail") as TextBox;
        TextBox txtdatecsvgrid = grdcsvData.Rows[e.RowIndex].FindControl("txtdatecsvgrid") as TextBox;
        TextBox txtNoOfBillscsv = grdcsvData.Rows[e.RowIndex].FindControl("txtNoOfBillscsv") as TextBox;
        TextBox txtRemarkscsv = grdcsvData.Rows[e.RowIndex].FindControl("txtRemarkscsv") as TextBox;

        TextBox txtAttachcsv = grdcsvData.Rows[e.RowIndex].FindControl("txtAttachedcsv") as TextBox;
        TextBox txtHardCopiescsv = grdcsvData.Rows[e.RowIndex].FindControl("txtHardCopiesCSV") as TextBox;

        TextBox txtNoof_Monthcsv = grdcsvData.Rows[e.RowIndex].FindControl("txtNoof_Monthcsv") as TextBox;
        if (txtHardCopiescsv.Text == "")
        {
            txtHardCopiescsv.Text = "0";
        }
        if (txtAttachcsv.Text == "")
        {
            txtAttachcsv.Text = "0";
        }
        if (txtNoof_Monthcsv.Text == "0" || txtNoof_Monthcsv.Text == "")
        {
            txtHardCopiescsv.Text = "1";
        }
        int hardcop = Convert.ToInt32(txtHardCopiescsv.Text.Trim());
        int Attac = Convert.ToInt32(txtAttachcsv.Text.Trim());

        txtNoOfBillscsv.Text = (hardcop + Attac).ToString();
       
        SqlDataReader drapr = con.SHow_Reim_Approvalidwithlimit(lblAppr_type.Text);
                    drapr.Read();
                    string Fixed_Amount = drapr["Fixed_Amount"].ToString();
                    string Percentage_Of_Basic_Pay = drapr["Percentage_Of_Basic_Pay"].ToString();
                    string Limitcsv1 = drapr["Limit"].ToString();
                    if (Fixed_Amount == "Yes")
                    {
                        lblCSVLimit = drapr["Amount"].ToString();
                    }
                    if (Percentage_Of_Basic_Pay == "Yes")
                    {

                        string percentagelimit = drapr["Percentage"].ToString();
                        decimal percentagel = Convert.ToDecimal(percentagelimit);
                        decimal basicpay = Convert.ToDecimal(lblBasicPay.Text);

                        decimal basicpay1 = basicpay * percentagel / 100;
                        lblCSVLimit = basicpay1.ToString();

                        //lblCSVLimit = drapr["Percentage"].ToString();
                    }

                    drapr.Close();

                    if (txtClaim_Amount.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please Fill Claim Amount');", true);
                    
                    }


                    else
                    {
                        decimal maxl = Convert.ToDecimal(lblCSVLimit);
                        decimal inputAmt = Convert.ToDecimal(txtClaim_Amount.Text);
                       
                        if (Limitcsv1 == "Yearly")
                        {

                            SqlDataReader drclaimsum = con.Sum_Claim_Amount_tble_claim_Apply(Session["Company"].ToString(), Session["uid"].ToString(), lblclaimtypecsvd.Text);
                            drclaimsum.Read();
                            string takenAmount = drclaimsum["Claim_Amount"].ToString();
                            if (takenAmount == "")
                            {
                                takenAmountprev = "0";
                                decimal takemamt = Convert.ToDecimal(takenAmountprev);

                                takenAmountprev = (takemamt + inputAmt).ToString();
                                takenAmount2 = Convert.ToDecimal((takemamt + inputAmt));
                            }
                            else
                            {
                                decimal takemamt=Convert.ToDecimal(takenAmount);

                                takenAmountprev = (takemamt + inputAmt).ToString();
                              takenAmount2=Convert.ToDecimal( (takemamt + inputAmt));
                            }

                            drclaimsum.Close();
                            if (takenAmount2 > maxl)
                            {
                                decimal actualAmt = Convert.ToDecimal(takenAmountprev);

                                decimal examt = actualAmt - maxl;
                                //decimal examt = inputAmt - maxl;
                                error_remarks = "Amount Exceed";
                                lblBalanceAmountcsv.Text = "0";

                                con.Update_tble_Claim_CSV_Data_notsend_Approvewithidgrd(txtClaim_Amount.Text, txtBill_Detail.Text, lblid.Text, error_remarks, "Yes", examt.ToString(), lblBalanceAmountcsv.Text, Convert.ToDateTime(txtdatecsvgrid.Text), txtRemarkscsv.Text, txtNoOfBillscsv.Text, txtAttachcsv.Text, txtHardCopiescsv.Text, Convert.ToInt32(txtNoof_Monthcsv.Text));
                                con.DisConnect();
                                grdcsvData.EditIndex = -1;
                                Show_CSVdata_send();
                            }
                            else
                            {
                                decimal actualAmt =Convert.ToDecimal(takenAmountprev);
                                error_remarks = "";
                                lblBalanceAmountcsv.Text = (maxl - actualAmt).ToString();

                                con.Update_tble_Claim_CSV_Data_notsend_Approvewithidgrd(txtClaim_Amount.Text, txtBill_Detail.Text, lblid.Text, error_remarks, "No", "0.00", lblBalanceAmountcsv.Text, Convert.ToDateTime(txtdatecsvgrid.Text), txtRemarkscsv.Text, txtNoOfBillscsv.Text, txtAttachcsv.Text, txtHardCopiescsv.Text, Convert.ToInt32(txtNoof_Monthcsv.Text));
                                con.DisConnect();
                                grdcsvData.EditIndex = -1;
                                Show_CSVdata_send();
                            }
                        
                        
                        }
                        else
                        {
                            if (inputAmt > maxl)
                            {
                                decimal examt = inputAmt - maxl;
                                error_remarks = "Amount Exceed";
                                lblBalanceAmountcsv.Text = "0";
                                con.Update_tble_Claim_CSV_Data_notsend_Approvewithidgrd(txtClaim_Amount.Text, txtBill_Detail.Text, lblid.Text, error_remarks, "Yes", examt.ToString(), lblBalanceAmountcsv.Text, Convert.ToDateTime(txtdatecsvgrid.Text), txtRemarkscsv.Text, txtNoOfBillscsv.Text, txtAttachcsv.Text, txtHardCopiescsv.Text, Convert.ToInt32(txtNoof_Monthcsv.Text));
                                con.DisConnect();
                                grdcsvData.EditIndex = -1;
                                Show_CSVdata_send();
                            }
                            else
                            {
                                lblBalanceAmountcsv.Text = "0";
                                error_remarks = "";
                                con.Update_tble_Claim_CSV_Data_notsend_Approvewithidgrd(txtClaim_Amount.Text, txtBill_Detail.Text, lblid.Text, error_remarks, "No", "0.00", lblBalanceAmountcsv.Text, Convert.ToDateTime(txtdatecsvgrid.Text), txtRemarkscsv.Text, txtNoOfBillscsv.Text, txtAttachcsv.Text, txtHardCopiescsv.Text, Convert.ToInt32(txtNoof_Monthcsv.Text));
                                con.DisConnect();
                                grdcsvData.EditIndex = -1;
                                Show_CSVdata_send();
                            }
                        }
                    }


                    ShowNoOfattachCSV();
        
        
       
    }

    public void Total_NoofBills()
    {

        if (txtHardCopies.Text == "")
        {
            txtHardCopies.Text = "0";
        }
        if (txtAttached.Text == "")
        {
            txtAttached.Text = "0";
        }

            int hardcop = Convert.ToInt32(txtHardCopies.Text.Trim());
            int Attac = Convert.ToInt32(txtAttached.Text.Trim());

            txtNoofBills.Text =( hardcop + Attac).ToString();
       

    }


    protected void grdReimbursment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //Button btnDeletecsv = (Button)e.Row.FindControl("btnDeletecsv");


            Label lblAmountExceedentrylevel = (Label)e.Row.FindControl("lblAmountExceedentrylevel");
            if (lblAmountExceedentrylevel.Text == "Yes")
            {
                e.Row.BackColor = System.Drawing.Color.LightCoral;
                e.Row.ForeColor = System.Drawing.Color.White;
            }
            if (lblAmountExceedentrylevel.Text == "No")
            {

            }
        }
    }

    
    public void sendfor_ApprovalCSVData()
    {
       
        SqlDataAdapter da = new SqlDataAdapter("select * from tble_Claim_CSV_Data where Company_name='" + Session["Company"].ToString() + "' and Userid = '" + Session["uid"].ToString() + "' and Applicable ='Yes'", con.Con);
        DataSet ds = new DataSet();
        da.Fill(ds, "tble_Claim_CSV_Data");
        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
        {
            string RM2_Approval_Required = "No";
            string Claim_type = ""; string Claim_Date = ""; string Claim_Amount = ""; string Bill_Detail = ""; string Remarks = ""; string Approval_type = "";
            string AmountExceed = ""; string ExceedAmount = ""; string Balance_Amount = ""; string No_Of_Bills = "";string Self_Approvalopt="";decimal Self_Limit;
            string RM1_Approval = ""; decimal RM1_Limit; string RM2_Approval = ""; decimal RM2_Limit; string selfaprblank = ""; string Noof_Month = "";
            string fr = ""; decimal ClaimAmtfcsv; string RM1 = ""; string RM2 = ""; string RM1_Email = ""; string RM2_Email = ""; string Approval_Status = "Pending"; string RM1_Name = ""; string RM2_Name = ""; string iddata = "";  string DocumentNoAlphaNumeric="CMP-ASK-" + txtDocumentno.Text.Trim();;
            iddata = ds.Tables[0].Rows[i]["id"].ToString();
            Claim_type = ds.Tables[0].Rows[i]["Claim_type"].ToString();
            Claim_Date = ds.Tables[0].Rows[i]["Claim_Date"].ToString();
            Claim_Amount = ds.Tables[0].Rows[i]["Claim_Amount"].ToString();
            ClaimAmtfcsv = Convert.ToDecimal(Claim_Amount);
            Bill_Detail = ds.Tables[0].Rows[i]["Bill_Detail"].ToString();
            Remarks = ds.Tables[0].Rows[i]["Remarks"].ToString();

            Approval_type = ds.Tables[0].Rows[i]["Approval_type"].ToString();
            AmountExceed = ds.Tables[0].Rows[i]["AmountExceed"].ToString();

            ExceedAmount = ds.Tables[0].Rows[i]["ExceedAmount"].ToString();
            Balance_Amount = ds.Tables[0].Rows[i]["Balance_Amount"].ToString();
            No_Of_Bills = ds.Tables[0].Rows[i]["No_Of_Bills"].ToString();
            Noof_Month = ds.Tables[0].Rows[i]["Noof_Month"].ToString();
            if (Noof_Month == "")
            {
                Noof_Month = "0";
            }

            SqlDataReader dr = con.SHow_LimitToApprovalType(Session["Company"].ToString(), Approval_type);
                dr.Read();
                if (dr.HasRows)
                {
                    Self_Approvalopt = dr["Self_Approval"].ToString();
                    string Self_Limit1 = dr["Self_Limit"].ToString();
                    Self_Limit = Convert.ToDecimal(Self_Limit1);
                    RM1_Approval = dr["RM1_Approval"].ToString();
                    string RM1_Limit1 = dr["RM1_Limit"].ToString();
                    RM1_Limit = Convert.ToDecimal(RM1_Limit1);
                    RM2_Approval = dr["RM2_Approval"].ToString();
                    string RM2_Limit1 = dr["RM2_Limit"].ToString();
                    RM2_Limit = Convert.ToDecimal(RM2_Limit1);
                    dr.Close();
                    if (Self_Approvalopt == "Yes" && ClaimAmtfcsv <= Self_Limit)
                    {
                        string First_ApprovalUserid = "";
                        Approval_type = "Approved";
                        con.Claim_Apply_tble_claim_Apply(Session["Fulname"].ToString(), Session["uid"].ToString(), Session["CompanyEmail"].ToString(), Claim_type, Claim_Date, Claim_Amount, Remarks, Session["uid"].ToString(), RM1, RM2, RM1_Email, RM2_Email, Approval_Status, System.DateTime.Now.ToString(), RM1_Name, RM2_Name, Session["Company"].ToString(), Claim_type, AmountExceed, ExceedAmount, Approval_type, Bill_Detail, Balance_Amount, No_Of_Bills.Trim(), txtDocumentno.Text.Trim(), Convert.ToInt32(txtAttached.Text.Trim()), Convert.ToInt32(txtHardCopies.Text.Trim()), DocumentNoAlphaNumeric, Convert.ToInt32(Noof_Month), RM2_Approval_Required, RM1_Limit, RM2_Limit, Self_Limit, ClaimAmtfcsv, System.DateTime.Now.ToString(), First_ApprovalUserid);
                        con.delete_tble_Claim_CSV_Data_withid(iddata);
                        con.DisConnect();

                        string stable = Session["Company"].ToString();
                        string rtable = stable.Replace(".", "_");
                        string comanyname = "[" + rtable + "$Item Journal Line" + "]";
                        Session["CompanyTableEmployee"] = comanyname.ToString();


                    }
                    if (RM1_Approval == "Yes")
                    {
                        if (ClaimAmtfcsv > Self_Limit && ClaimAmtfcsv <= RM1_Limit)
                        {
                            string First_ApprovalUserid = Session["HODLoginPage"].ToString();
                            string claminAppro1 = "00.00";
                            decimal claminAppro = Convert.ToDecimal(claminAppro1);
                            string approvaldat = "";
                            con.Claim_Apply_tble_claim_Apply(Session["Fulname"].ToString(), Session["uid"].ToString(), Session["CompanyEmail"].ToString(), Claim_type, Claim_Date, Claim_Amount, Remarks, selfaprblank, Session["HODLoginPage"].ToString(), RM2, Session["hod_email2"].ToString(), RM2_Email, Approval_Status, System.DateTime.Now.ToString(), Session["hod_Name2"].ToString(), RM2_Name, Session["Company"].ToString(), Claim_type, AmountExceed, ExceedAmount, Approval_type, Bill_Detail, Balance_Amount, No_Of_Bills.Trim(), txtDocumentno.Text.Trim(), Convert.ToInt32(txtAttached.Text.Trim()), Convert.ToInt32(txtHardCopies.Text.Trim()), DocumentNoAlphaNumeric, Convert.ToInt32(Noof_Month), RM2_Approval_Required, RM1_Limit, RM2_Limit, Self_Limit, claminAppro, approvaldat, First_ApprovalUserid);
                            con.delete_tble_Claim_CSV_Data_withid(iddata);
                        }
                    }
                    if (RM2_Approval == "Yes")
                    {
                        if (ClaimAmtfcsv > RM1_Limit && ClaimAmtfcsv <= RM2_Limit)
                        {
                            string First_ApprovalUserid = Session["HODLoginPage"].ToString();
                            string claminAppro1 = "00.00";
                            decimal claminAppro = Convert.ToDecimal(claminAppro1);
                            RM2_Approval_Required = "Yes";
                            string approvaldat = "";
                            //showhodofHOD();
                            //con.Claim_Apply_tble_claim_Apply(Session["Fulname"].ToString(), Session["uid"].ToString(), Session["CompanyEmail"].ToString(), Claim_type, Claim_Date, Claim_Amount, Remarks, selfaprblank, RM1, Session["hodofhod"].ToString(), RM1_Email, Session["HODofHODEmail"].ToString(), Approval_Status, System.DateTime.Now.ToString(), RM1_Name, Session["HODofHODName"].ToString(), Session["Company"].ToString(), Claim_type, AmountExceed, ExceedAmount, Approval_type, Bill_Detail, Balance_Amount, No_Of_Bills.Trim(), txtDocumentno.Text.Trim(), Convert.ToInt32(txtAttached.Text.Trim()), Convert.ToInt32(txtHardCopies.Text.Trim()), DocumentNoAlphaNumeric, Convert.ToInt32(Noof_Month), RM2_Approval_Required);
                            //con.delete_tble_Claim_CSV_Data_withid(iddata);
                            con.Claim_Apply_tble_claim_Apply(Session["Fulname"].ToString(), Session["uid"].ToString(), Session["CompanyEmail"].ToString(), Claim_type, Claim_Date, Claim_Amount, Remarks, selfaprblank, Session["HODLoginPage"].ToString(), RM2, Session["hod_email2"].ToString(), RM2_Email, Approval_Status, System.DateTime.Now.ToString(), Session["hod_Name2"].ToString(), RM2_Name, Session["Company"].ToString(), Claim_type, AmountExceed, ExceedAmount, Approval_type, Bill_Detail, Balance_Amount, No_Of_Bills.Trim(), txtDocumentno.Text.Trim(), Convert.ToInt32(txtAttached.Text.Trim()), Convert.ToInt32(txtHardCopies.Text.Trim()), DocumentNoAlphaNumeric, Convert.ToInt32(Noof_Month), RM2_Approval_Required, RM1_Limit, RM2_Limit, Self_Limit, claminAppro, approvaldat, First_ApprovalUserid);
                            con.delete_tble_Claim_CSV_Data_withid(iddata);

                        }
                    }
                    if (RM1_Approval == "No")
                    {
                        if (ClaimAmtfcsv > Self_Limit)
                        {
                            string First_ApprovalUserid = Session["HODLoginPage"].ToString();
                            string claminAppro1 = "00.00";
                            decimal claminAppro = Convert.ToDecimal(claminAppro1);
                            string approvaldat = "";
                            con.Claim_Apply_tble_claim_Apply(Session["Fulname"].ToString(), Session["uid"].ToString(), Session["CompanyEmail"].ToString(), Claim_type, Claim_Date, Claim_Amount, Remarks, selfaprblank, Session["HODLoginPage"].ToString(), RM2, Session["hod_email2"].ToString(), RM2_Email, Approval_Status, System.DateTime.Now.ToString(), Session["hod_Name2"].ToString(), RM2_Name, Session["Company"].ToString(), Claim_type, AmountExceed, ExceedAmount, Approval_type, Bill_Detail, Balance_Amount, No_Of_Bills.Trim(), txtDocumentno.Text.Trim(), Convert.ToInt32(txtAttached.Text.Trim()), Convert.ToInt32(txtHardCopies.Text.Trim()), DocumentNoAlphaNumeric, Convert.ToInt32(Noof_Month), RM2_Approval_Required, RM1_Limit, RM2_Limit, Self_Limit, claminAppro, approvaldat, First_ApprovalUserid);
                            con.delete_tble_Claim_CSV_Data_withid(iddata);
                        }
                    }
                   
                }
        }

        con.DisConnect();
            
        Show_CSVdata_send();
       

    }




               
    protected void btnsendforapprovalcsv_Click(object sender, EventArgs e)
    {
        sendfor_ApprovalCSVData();
        con.update_Attachement(txtDocumentno.Text, Session["Company"].ToString(), "Pending", Session["uid"].ToString());
        con.DisConnect();
        lastrecDocumentNo();
        showdataattachementCSV();
    }
    protected void ddBillAttach_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddBillAttach.SelectedValue == "Yes")
        {
            txtNoofBills.Enabled = false;
            txtAttached.Enabled = true;
        }

        if (ddBillAttach.SelectedValue == "No")
        {
            txtNoofBills.Text = "";
            txtNoofBills.Enabled = false;
            txtAttached.Enabled = false;
            txtAttached.Text = "0";
        }
        Total_NoofBills();
    }

    public void UploadFileentry()
    {

        string contenttype = String.Empty;
        string ext = "";


        if (contenttype == String.Empty)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFile PostedFile = Request.Files[i];
                if (PostedFile.ContentLength > 0)
                {
                    string filePath = System.IO.Path.GetFileName(PostedFile.FileName);


                    string filename = Path.GetFileName(filePath);
                    ext = Path.GetExtension(filename);

                    switch (ext)
                    {
                        case ".doc":
                            contenttype = "application/vnd.ms-word";
                            break;
                        case ".docx":
                            contenttype = "application/vnd.ms-word";
                            break;
                        case ".xls":
                            contenttype = "application/vnd.ms-excel";
                            break;
                        case ".xlsx":
                            contenttype = "application/vnd.ms-excel";
                            break;
                        case ".jpg":
                            contenttype = "image/jpg";
                            break;
                        case ".png":
                            contenttype = "image/png";
                            break;
                        case ".gif":
                            contenttype = "image/gif";
                            break;
                        case ".pdf":
                            contenttype = "application/pdf";
                            break;
                    }

                    Stream fs = PostedFile.InputStream;

                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);

                    string strQuery = "insert into tble_Attachment(File_Attachment_Name, ContentType, Data,Documentid,CompanyName,userid)" + " values (@File_Attachment_Name, @ContentType, @Data,@Documentid,@CompanyName ,@userid)";
                    SqlCommand cmd = new SqlCommand(strQuery, con.Con);
                    con.Connect();
                    cmd.Parameters.Add("@File_Attachment_Name", SqlDbType.VarChar).Value = filename;
                    cmd.Parameters.Add("@ContentType", SqlDbType.VarChar).Value = contenttype;
                    cmd.Parameters.Add("@Data", SqlDbType.Binary).Value = bytes;
                    cmd.Parameters.Add("@Documentid", SqlDbType.VarChar).Value = txtDocumentno.Text;
                    cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Session["Company"].ToString();
                    cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = Session["uid"].ToString();
                    cmd.ExecuteNonQuery();
                    con.DisConnect();

                }
            }
            showdataattachement();
        }
        else
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "File format not recognised." +
              " Upload Image/Word/PDF/Excel formats";
        }
       
    }

    protected void btnuploadFile_Click(object sender, EventArgs e)
    {
        UploadFileentry();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAttachment.PageIndex = e.NewPageIndex;
        showdataattachement();
    }
    protected void lnkDeketeattachement_Command(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        con.delete_tble_Attachment(id);
        con.DisConnect();
        showdataattachement();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string contenttype = String.Empty;
        string ext = "";


        if (contenttype == String.Empty)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFile PostedFile = Request.Files[i];
                if (PostedFile.ContentLength > 0)
                {
                    string filePath = System.IO.Path.GetFileName(PostedFile.FileName);


                    string filename = Path.GetFileName(filePath);
                    ext = Path.GetExtension(filename);

                    switch (ext)
                    {
                        case ".doc":
                            contenttype = "application/vnd.ms-word";
                            break;
                        case ".docx":
                            contenttype = "application/vnd.ms-word";
                            break;
                        case ".xls":
                            contenttype = "application/vnd.ms-excel";
                            break;
                        case ".xlsx":
                            contenttype = "application/vnd.ms-excel";
                            break;
                        case ".jpg":
                            contenttype = "image/jpg";
                            break;
                        case ".png":
                            contenttype = "image/png";
                            break;
                        case ".gif":
                            contenttype = "image/gif";
                            break;
                        case ".pdf":
                            contenttype = "application/pdf";
                            break;
                    }

                    Stream fs = PostedFile.InputStream;

                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);

                    string strQuery = "insert into tble_Attachment(File_Attachment_Name, ContentType, Data,Documentid,CompanyName,userid)" + " values (@File_Attachment_Name, @ContentType, @Data,@Documentid,@CompanyName ,@userid)";
                    SqlCommand cmd = new SqlCommand(strQuery, con.Con);
                    con.Connect();
                    cmd.Parameters.Add("@File_Attachment_Name", SqlDbType.VarChar).Value = filename;
                    cmd.Parameters.Add("@ContentType", SqlDbType.VarChar).Value = contenttype;
                    cmd.Parameters.Add("@Data", SqlDbType.Binary).Value = bytes;
                    cmd.Parameters.Add("@Documentid", SqlDbType.VarChar).Value = txtDocumentno.Text;
                    cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Session["Company"].ToString();
                    cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = Session["uid"].ToString();
                    cmd.ExecuteNonQuery();
                    con.DisConnect();

                }
            }
            showdataattachementCSV();
        }
        else
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "File format not recognised." +
              " Upload Image/Word/PDF/Excel formats";
        }
    }
    protected void lnkDeketeattachementcsv_Command(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        con.delete_tble_Attachment(id);
        con.DisConnect();
        showdataattachementCSV();
    }
    protected void grdAttachmentcsv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAttachmentcsv.PageIndex = e.NewPageIndex;
        showdataattachementCSV();
    }

    public void ShowDataofPending_ApprovalwithUserid()
    {
        SqlDataReader dr = con.ShowClaimApprovalForRM1withUserid(txtSearchName.Text,Session["Company"].ToString(),Session["uid"].ToString());
        DataTable dt = new DataTable();
        dt.Load(dr);
        grdPendingClaim.DataSource = dt;
        grdPendingClaim.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void ShowDataofPending_ApprovalwithName()
    {
        SqlDataReader dr = con.ShowClaimApprovalForRM1withName(txtSearchName.Text, Session["Company"].ToString(), Session["uid"].ToString());
        DataTable dt = new DataTable();
        dt.Load(dr);
        grdPendingClaim.DataSource = dt;
        grdPendingClaim.DataBind();
        dr.Close();
        con.DisConnect();
    }

    public void ShowDataofPending_ApprovalwithDate()
    {
        SqlDataReader dr = con.ShowClaimApprovalForRM1withDate(Session["Company"].ToString(), Session["uid"].ToString(), Convert.ToDateTime(txtFromDate_Claim.Text), Convert.ToDateTime(txtTodate_claim.Text));
        DataTable dt = new DataTable();
        dt.Load(dr);
        grdPendingClaim.DataSource = dt;
        grdPendingClaim.DataBind();
        dr.Close();
        con.DisConnect();
    }

    public void ShowDataofPending_ApprovalwithAll()
    {
        SqlDataReader dr = con.ShowClaimApprovalForRM1withAll(Session["Company"].ToString(), Session["uid"].ToString());
        DataTable dt = new DataTable();
        dt.Load(dr);
        grdPendingClaim.DataSource = dt;
        grdPendingClaim.DataBind();
        dr.Close();
        con.DisConnect();
    }


    protected void btnPendingApprovalSearch_Click(object sender, EventArgs e)
    {
        if (rdEmployeeID.Checked == true)
        {
            ShowDataofPending_ApprovalwithUserid();
        }
        if (rdEmployeeName.Checked == true)
        {
            ShowDataofPending_ApprovalwithName();
        }
        if (rdDatewise.Checked == true)
        {
            if (txtFromDate_Claim.Text == "" || txtTodate_claim.Text == "")
            {

            }
            else
            {
                ShowDataofPending_ApprovalwithDate();
            }
        }
      
    }
    protected void rdEmployeeID_CheckedChanged(object sender, EventArgs e)
    {
        pnlEmployeeidName.Visible = true;
        pnlDate.Visible = false;
        btnPendingApprovalSearch.Visible = true;
    }
    protected void rdEmployeeName_CheckedChanged(object sender, EventArgs e)
    {
        pnlEmployeeidName.Visible = true;
        pnlDate.Visible = false;
        btnPendingApprovalSearch.Visible = true;
    }
    protected void rdDatewise_CheckedChanged(object sender, EventArgs e)
    {
        pnlEmployeeidName.Visible = false;
        pnlDate.Visible = true;
        btnPendingApprovalSearch.Visible = true;
    }
    protected void CHKAllPending_CheckedChanged(object sender, EventArgs e)
    {
        pnlEmployeeidName.Visible = false;
        pnlDate.Visible = false;
        btnPendingApprovalSearch.Visible = false;
        if (CHKAllPending.Checked == true)
        {
            ShowDataofPending_ApprovalwithAll();
        }
    }


    protected void lnkClaim_Approval_Click(object sender, EventArgs e)
    {
       
        rdCSVFile.Checked = false;
        rdEntryForm.Checked = false;
        pnlreimbursmentregister.Visible = false;
        pnlviewReimbursment.Visible = false;
        pnlPendingClaimApproval.Visible = true;
        pnlClaimAprovalstatus.Visible = false;
        pnlMain.Visible = false;
        pnlCSVFile.Visible = false;
    }

    public void ShowNoOfattach()
    { 
    
    SqlDataReader dr=con.SHow_AttachedData(Session["Company"].ToString(),Session["uid"].ToString(),txtDocumentno.Text.Trim());
    dr.Read();
    if (dr.HasRows)
    {
        string attt = dr["Attached"].ToString();
        int attt1=Convert.ToInt32(attt);
        if (attt1 > 0)
        {
            flp_file_attachement.Visible = true;
            btnuploadFile.Visible = true;
            lblBillaAttach.Visible = true;
            pnlAttachEntry.Visible = true;
        }
        else
        {
            flp_file_attachement.Visible = false;
            btnuploadFile.Visible = false;
            lblBillaAttach.Visible = false;
            pnlAttachEntry.Visible = false;
        }
    
    }
    dr.Close();
    con.DisConnect();
    }


    protected void lnkDocumentNon_Command(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        Session["Docid"] = id.ToString();

        Page.ClientScript.RegisterStartupScript(
   this.GetType(), "OpenWindow", "window.open('Attachmentdetail.aspx','_newtab');", true);
    }
    protected void txtHardCopies_TextChanged(object sender, EventArgs e)
    {
        Total_NoofBills();
    }
    protected void txtAttached_TextChanged(object sender, EventArgs e)
    {
        Total_NoofBills();
    }
    public void ShowNoOfattachCSV()
    {

        SqlDataReader dr = con.SHow_AttachedDataCSV(Session["Company"].ToString(), Session["uid"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            string attt = dr["Attached"].ToString();
            if (attt == "")
            {
                attt = "0";
            }
            int attt1 = Convert.ToInt32(attt);
            if (attt1 > 0)
            {
                pnlCSvFileuploadattached.Visible = true;
            }
            else
            {
                pnlCSvFileuploadattached.Visible = false;
            }

        }
        
        dr.Close();
        con.DisConnect();
    }
    protected void txtClaimDateAdvance_TextChanged(object sender, EventArgs e)
    {

    }




    protected void grdPendingClaim_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

          

            Label lblAmountExceedentrylevel = (Label)e.Row.FindControl("lblAmountExceedentrylevelpending");
            Button btnSendRM2 = (Button)e.Row.FindControl("btnSendRM2");
            Button btnApprovedRM1 = (Button)e.Row.FindControl("btnApprovedRM1");
            Button btnRejecta = (Button)e.Row.FindControl("btnRejecta");
            Label lblClaminAmountPending = (Label)e.Row.FindControl("lblClaminAmountPending");
            Label lblRM1Limit = (Label)e.Row.FindControl("lblRM1Limit");
            Label lblRM2Limit = (Label)e.Row.FindControl("lblRM2Limit");
            TextBox txtApprovaAmounts = (TextBox)e.Row.FindControl("txtApprovaAmounts");

            Label lblRM1Approved = (Label)e.Row.FindControl("lblRM1Approved");

            if (lblAmountExceedentrylevel.Text == "Yes")
            {
                e.Row.BackColor = System.Drawing.Color.LightCoral;
                e.Row.ForeColor = System.Drawing.Color.White;
            }
            if (lblAmountExceedentrylevel.Text == "No")
            {

            }
            decimal lblRM1Limit1=Convert.ToDecimal(lblRM1Limit.Text);
              decimal lblRM2Limit1=Convert.ToDecimal(lblRM2Limit.Text);
             decimal lblClaminAmountPending1=Convert.ToDecimal(lblClaminAmountPending.Text);

             if (lblClaminAmountPending1 > lblRM1Limit1 && lblRM1Approved.Text=="No")
             {
                 btnSendRM2.Visible = true;
                 btnApprovedRM1.Visible = false;
             }
             if (lblClaminAmountPending1 <= lblRM1Limit1 && lblRM1Approved.Text == "No")
             {
                 btnSendRM2.Visible = false;
                 btnApprovedRM1.Visible = true;
             }

             if (lblClaminAmountPending1 >= lblRM1Limit1 && lblRM1Approved.Text == "Yes")
             {
                 btnSendRM2.Visible = false;
                 btnApprovedRM1.Visible = true;
             }
        }
    }
    protected void btnRejecta_Command(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        lblIdofClaim.Text = id.ToString();
        ModalPopupExtender1.Show();
    }
    protected void lnkClose_Click(object sender, EventArgs e)
    {
        if (rdEmployeeID.Checked == true)
        {
            ShowDataofPending_ApprovalwithUserid();
        }
        if (rdEmployeeName.Checked == true)
        {
            ShowDataofPending_ApprovalwithName();
        }
        if (rdDatewise.Checked == true)
        {
            if (txtFromDate_Claim.Text == "" || txtTodate_claim.Text == "")
            {

            }
            else
            {
                ShowDataofPending_ApprovalwithDate();
            }
        }
        if (CHKAllPending.Checked == true)
        {
            ShowDataofPending_ApprovalwithAll();
        }
    }
    protected void btnsaverejected_Click(object sender, EventArgs e)
    {
        con.updateclaimdatawithRejectedRemarks(txtRemarksRejected.Text, lblIdofClaim.Text, Convert.ToDateTime(System.DateTime.Now));
        con.DisConnect();
        if (rdEmployeeID.Checked == true)
        {
            ShowDataofPending_ApprovalwithUserid();
        }
        if (rdEmployeeName.Checked == true)
        {
            ShowDataofPending_ApprovalwithName();
        }
        if (rdDatewise.Checked == true)
        {
            if (txtFromDate_Claim.Text == "" || txtTodate_claim.Text == "")
            {

            }
            else
            {
                ShowDataofPending_ApprovalwithDate();
            }
        }
        if (CHKAllPending.Checked == true)
        {
            ShowDataofPending_ApprovalwithAll();
        }
    }
    protected void btnApprovedRM1_Command(object sender, CommandEventArgs e)
    {
        //string id, DateTime Approval_Date ,decimal ApprovalAmt
        string id = e.CommandArgument.ToString();
        GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
        TextBox txtApprovaAmounts = row.FindControl("txtApprovaAmounts") as TextBox;
        con.ClaimApprovebyRM1(id, Convert.ToDateTime(System.DateTime.Now), Convert.ToDecimal(txtApprovaAmounts.Text));
        con.DisConnect();
        if (rdEmployeeID.Checked == true)
        {
            ShowDataofPending_ApprovalwithUserid();
        }
        if (rdEmployeeName.Checked == true)
        {
            ShowDataofPending_ApprovalwithName();
        }
        if (rdDatewise.Checked == true)
        {
            if (txtFromDate_Claim.Text == "" || txtTodate_claim.Text == "")
            {

            }
            else
            {
                ShowDataofPending_ApprovalwithDate();
            }
        }
        if (CHKAllPending.Checked == true)
        {
            ShowDataofPending_ApprovalwithAll();
        }
       //con.ClaimApprovebyRM1(
    }
    protected void btnSendRM2_Command(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        lblSendforRM2Approval2.Text = id.ToString();
        ModalPopupExtender2.Show();

    }
    protected void btnsaveRM2Save_Click(object sender, EventArgs e)
    {
        con.ClaimApprovebForRM2(lblSendforRM2Approval2.Text, Convert.ToDateTime(System.DateTime.Now.ToString()), Session["HODLoginPage"].ToString(), Session["hod_email2"].ToString(), Session["hod_Name2"].ToString(), txtRemarksSendRM2aproval.Text, "Yes", Session["HODLoginPage"].ToString());
        con.DisConnect();

        if (rdEmployeeID.Checked == true)
        {
            ShowDataofPending_ApprovalwithUserid();
        }
        if (rdEmployeeName.Checked == true)
        {
            ShowDataofPending_ApprovalwithName();
        }
        if (rdDatewise.Checked == true)
        {
            if (txtFromDate_Claim.Text == "" || txtTodate_claim.Text == "")
            {

            }
            else
            {
                ShowDataofPending_ApprovalwithDate();
            }
        }
        if (CHKAllPending.Checked == true)
        {
            ShowDataofPending_ApprovalwithAll();
        }
    }
    protected void LinkButton2_Click1(object sender, EventArgs e)
    {
        rdCSVFile.Checked = false;
        rdEntryForm.Checked = false;
        pnlreimbursmentregister.Visible = false;
        pnlviewReimbursment.Visible = false;
        pnlPendingClaimApproval.Visible = false;

        pnlMain.Visible = false;
        pnlCSVFile.Visible = false;
        pnlClaimAprovalstatus.Visible = true;
    }

    protected void btnShowClaimAprStatus_Click(object sender, EventArgs e)
    {
        ShowClaimReimAprStatus();
    }
    public void ShowClaimReimAprStatus()
    {
        if (ddClaimAprStatus.Text == "All")
        {
            SqlDataReader odr = con.ShowClaimAprStatusAll3(Session["Company"].ToString(), Session["uid"].ToString(), txtClaimStatusFromDate.Text, txtClaimStatusToDate.Text);
            DataTable Dt = new DataTable();
            Dt.Load(odr);
            grdClaimApprovalStatus.DataSource = Dt;
            grdClaimApprovalStatus.DataBind();
            odr.Close();
            con.DisConnect();

        }
        else
        {

            SqlDataReader odr = con.ShowClaimAprstatuswithStatus(Session["Company"].ToString(), Session["uid"].ToString(), txtClaimStatusFromDate.Text, txtClaimStatusToDate.Text, ddClaimAprStatus.Text);
            DataTable Dt = new DataTable();
            Dt.Load(odr);
            grdClaimApprovalStatus.DataSource = Dt;
            grdClaimApprovalStatus.DataBind();
            odr.Close();
            con.DisConnect();
        }

    }

    protected void grdClaimApprovalStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdClaimApprovalStatus.PageIndex = e.NewPageIndex;
        ShowClaimReimAprStatus();
    }
}