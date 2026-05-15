using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class Student_StudentAdmitCard : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //SqlDataAdapter da = new SqlDataAdapter("select count(*) C from [tbl_StudentUpdatedMobileNo] where  [studentno]='" + Session["enroll"].ToString() + "' and [semester] ='" + Session["Semester"].ToString() + "' and [status]=1 ", con);
                //DataTable dt = new DataTable();
                //da.Fill(dt);
                //if (dt.Rows[0]["C"].ToString() != "0")
                //{
                //    pnl1.Visible = false;
                //}
                SqlDataAdapter da1 = new SqlDataAdapter("select case when (case when [Final Semester Code]='' then [Final Years Course] else [Final Semester Code] end) =(Select case when Semester='' then Year else Semester end from [TMU$Student - COLLEGE] where [Enrollment No_]='" + Session["enroll"].ToString() + "') and (select count(*) from [AlumniRegistrationTemp] where [Enrollment No_]='" + Session["enroll"].ToString() + "' )=0 then 1 else 0 end as access  from [TMU$Course - COLLEGE] where Code=(select [Course Code] from [TMU$Student - COLLEGE] where [Enrollment No_]='" + Session["enroll"].ToString() + "') ", con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows[0]["access"].ToString() == "1")
                {
                    //Local Process
                    //Response.Redirect("~/Student/GoogleForm.aspx", false);
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();
                   //Live
                    Response.Redirect("GoogleForm.aspx", false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    //bindAdmitcard();
                    SemesterDropdown();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("../Default.aspx");
            }
        }
        
    }




    //developed by : Bhupendra Yadav
    //Purpose by   : Grid view Selected Row Count
   

    public void ReappearSemesterDropdown()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("sp_FetchReappearSemesterFromSubjectcollegeAdmitForST", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            //con.Open();
            da.Fill(dt);
            con.Close();
            ddlSem.DataSource = dt;
            ddlSem.DataTextField = "Semester";
            ddlSem.DataValueField = "Semester";
            ddlSem.DataBind();
            if (dt.Rows.Count > 0)
            {
                DivREp.Visible = true;
            }
            else
            {
                RepAugust.Visible = false;
                DivREp.Visible = true;
            }
           

        }
        catch (Exception e)
        {

        }

    }

    public void bindAdmitcard()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_AdmitCardDataFetchMainREpAdmitForST", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
            cmd.Parameters.AddWithValue("@Semest", ddlSem.SelectedValue);
            cmd.Parameters.AddWithValue("@Apear", radRace.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                btnprint.Visible = true;
                printarea.Visible = true;
                if (radRace.Text == "1")
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string ExaminationType = dt.Rows[i]["ExaminationType"].ToString();
                        if (!ExaminationType.Contains("Re-appear / Supplementary"))
                        {
                            ExaminationType = ExaminationType + " (Re-appear / Supplementary)";
                            dt.Rows[i]["ExaminationType"] = ExaminationType;
                        }

                    }

                }
            }
            else
            {
                btnprint.Visible = false;
                printarea.Visible = false;
            }
            RepAugust.DataSource = dt;
            RepAugust.DataBind();
          
           
        }
        catch
        {
        }
    }
    protected void RepAugust_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                string customerId = (e.Item.FindControl("hfCustomerId") as HiddenField).Value;
                GridView GrdSubject = e.Item.FindControl("GrdSubjectDetail") as GridView;
                GridView GrdSubject1 = e.Item.FindControl("GrdSubjectDetail1") as GridView;
                HtmlGenericControl dvYN = (HtmlGenericControl)e.Item.FindControl("Extrasubject");

                SqlCommand cmd = new SqlCommand("Sp_FetchAdmitCardExaminationDetailsAAdmitForST", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@EnrollmentNo", customerId);
                cmd.Parameters.AddWithValue("@Appear", radRace.SelectedValue);// 0 for main 1 for repair
                cmd.Parameters.AddWithValue("@Semester", ddlSem.SelectedValue);
                //if (radRace.SelectedValue == "0")
                //{
                //    cmd.Parameters.AddWithValue("@Semester", Session["Semester"].ToString());
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@Semester", ddlSem.SelectedValue);
                //}
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GrdSubject.Visible = true;
                    GrdSubject.DataSource = ds.Tables[0];
                    GrdSubject.DataBind();
                }
                else
                {
                    GrdSubject.Visible = false;
                }


                if (ds.Tables.Count > 1)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        GrdSubject1.Visible = true;
                        GrdSubject1.DataSource = ds.Tables[1];
                        GrdSubject1.DataBind();
                    }
                    else
                    {
                        GrdSubject1.Visible = false;
                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {

                        dvYN.Visible = true;

                    }
                    else
                    {

                        dvYN.Visible = false;
                    }
                }
                else
                {

                    dvYN.Visible = false;
                }


            }

        }
        catch (Exception ex)
        {
        }
    }
    protected void radRace_SelectedIndexChanged(object sender, EventArgs e)
    {

        RepAugust.Visible = false;
        btnprint.Visible = false;
        if (radRace.SelectedValue == "0")
        {
            SemesterDropdown();
            //bindAdmitcard();
            RepAugust.Visible = true;
            DivREp.Visible = true;
        }
        else
        {
            ReappearSemesterDropdown();
           
            
        }

    }
    public void SemesterDropdown()
    {
        try
        {
            //divSem.Visible = true;
            SqlCommand cmd = new SqlCommand("sp_FetchSemesterforExamForm", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            DataRow newRow = dt.NewRow();
            newRow[0] = "--Select--";

            dt.Rows.InsertAt(newRow, 0);
            con.Close();
            ddlSem.DataSource = dt;
            ddlSem.DataTextField = "Sem";
            ddlSem.DataValueField = "Semester";
            ddlSem.DataBind();




        }
        catch (Exception e)
        {

        }

    }
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        RepAugust.Visible = false;
        btnprint.Visible = false;
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        if (ddlSem.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Sem/Year.')", true);
            return;
        }
        //SqlDataAdapter da1 = new SqlDataAdapter("select count(*) C from [tbl_StudentUpdatedMobileNo] where  [studentno]='" + Session["enroll"].ToString() + "' and [semester] ='" + ddlSem.SelectedValue + "' and [status]=1 ", con);
        //DataTable dt1 = new DataTable();
        //da1.Fill(dt1);
        //if (dt1.Rows[0]["C"].ToString() == "0")
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Verify your Mobile No.')", true);
        //    pnl1.Visible = true;
        //    return;
        //}
        //else
        //{

            bindAdmitcard();
            //pnl1.Visible = false;
            RepAugust.Visible = true;
        //}
    }

    //protected void btnsendOtp_Click(object sender, EventArgs e)
    //{
    //    //if (txtmobile.Text == "")
    //    //{
    //    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Mobile No.')", true);
    //    //    return;
    //    //}
    //    if (ddlSem.SelectedItem.Text == "--Select--")
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Semester/Year.')", true);
    //        return;
    //    }
    //    //pnl1.Visible = false;
    //    pnl2.Visible = true;
    //    string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
    //    string sRandomOTP = GenerateRandomOTP(4, saAllowedCharacters);

    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand("InsertUpdatedMobileNo", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@studentno", Session["enroll"].ToString());
    //        cmd.Parameters.AddWithValue("@studentname", Session["Name"].ToString());
    //        cmd.Parameters.AddWithValue("@studentMobile", txtmobile.Text.ToString());
    //        cmd.Parameters.AddWithValue("@semester", ddlSem.SelectedValue);
    //        cmd.Parameters.AddWithValue("@OTP", sRandomOTP);
    //        cmd.Parameters.AddWithValue("@status", 0);            
    //        con.Open();
    //        int result = cmd.ExecuteNonQuery();
    //        con.Close();
    //        if (result > 0)
    //        {

    //            SMS("MobileNo", "Dear Student, Your No dues confirmation OTP is " + sRandomOTP + ". Thank you. TEERTHANKER MAHAVEER UNIVERSITY");
    //            string somestring = txtmobile.Text;
    //            StringBuilder sb = new StringBuilder(somestring);
    //            sb[2] = '*';
    //            sb[3] = '*';
    //            sb[4] = '*';
    //            sb[5] = '*';
    //            somestring = sb.ToString();

    //            lblMSGOTP.Visible = true;
    //            //lblMSG.Visible = true;
    //            lblMSGOTP.Text = "OTP sent successfully for your registered mobile number " + somestring + " .OTP Valid for 15 minutes.";

    //            Session["OTP"] = sRandomOTP;
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //    }






        
    //}
    //protected void btnverify_Click(object sender, EventArgs e)
    //{
    //    if (txtmobile.Text == "")
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Mobile OTP')", true);
    //        return;
    //    }
    //    if (ddlSem.SelectedItem.Text == "--Select--")
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Semester/Year.')", true);
    //        return;
    //    }

    //    if (txtverifyMobileNO.Text == Session["OTP"].ToString())
    //    {

    //        SqlCommand cmd = new SqlCommand("UpdatedMobileNo", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@studentno", Session["enroll"].ToString());           
    //        cmd.Parameters.AddWithValue("@semester", ddlSem.SelectedValue);          
    //        con.Open();
    //        int result = cmd.ExecuteNonQuery();
    //        con.Close();
    //        if (result > 0)
    //        {
    //            pnl2.Visible = false;
    //            ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "confirm('Your Mobile Number has been verified sccessfully.');", true);
    //            pnl1.Visible = true;
    //            txtmobile.Enabled = false;
    //            btnsendOtp.Visible = false;
    //        }         
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "confirm('Your OTP is not correct please enter correct OTP');", true);
    //        pnl2.Visible = true;
    //    }
    //}

    private string GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters)
    {
        string sOTP = String.Empty;

        string sTempChars = String.Empty;

        Random rand = new Random();

        for (int i = 0; i < iOTPLength; i++)
        {

            int p = rand.Next(0, saAllowedCharacters.Length);
            sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
            sOTP += sTempChars;
        }
        return sOTP;
    }

    //public void SMS(String MobileNo, string Msg)
    //{
    //    //  Website: http://www.universalsmsadvertising.com/     //  Username: 9837016352   //  Password: 9837016352 
    //    MobileNo = "91" + txtmobile.Text;
    //    // MobileNo = "91" + 9015762885;
    //    string url = "http://www.universalsmsadvertising.com/universalsmsapi.php?user_name=9837016352&user_password=9837016352&mobile=" + MobileNo + "&sender_id=TMUniv&type=F&text=" + Msg + "";
    //    // string url = "http://www.universalsmsadvertising.com/universalsmsapi.php?user_name=myuserid&user_password=mypassword&mobile=919834567120&sender_id=SWEET&type=F&text= " + Msg + "";
    //    System.Net.HttpWebRequest fr;
    //    Uri targetURI = new Uri(url);
    //    fr = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(targetURI);
    //    if (fr.GetResponse().ContentLength > 0)
    //    {
    //        System.IO.StreamReader str = new System.IO.StreamReader(fr.GetResponse().GetResponseStream());
    //        Response.Write(str.ReadToEnd());
    //        str.Close();
    //    }
    //}
}