using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Web.ClientServices;
using System.Collections.Specialized;
using paytm;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Text;
using QRCoder;
using System.Net.Mail;
using System.Net.Configuration;

public partial class NoDuesDownload : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    private string randomnumber;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        string stuEnr = "";
        try
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["stuEnrValue"] != null)
                {
                    stuEnr = Request.QueryString["stuEnrValue"].ToString();
                    // getdeptlibrary();
                    binddata();
                    numberseries();
                    //binddate();
                    lblprinteddateandtime.Text = String.Format("{0}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                    hidenodues();
                    SqlDataAdapter da1 = new SqlDataAdapter("select No_,[Enrollment No_] ,[Student Name],[Course Name],[Fathers Name],No_,'20' +RIGHT([Academic Year], 2) AS AcademicYear1,[Global Dimension 1 Code],[Course Name],[Admitted Year] ,case when [Student Status]=1 then 'Student' when [Student Status]=2 then 'Inactive' when [Student Status]=3 then 'Alumini' when [Student Status]=4 then 'NR' when [Student Status]=5 then 'Re-Appear' when [Student Status]=6 then 'Intership'  end 'Student Status',[Course Code],[Admitted Year],[Mobile Number],case when [Father Mobile No]='' then [Gaurdian Mobile No] else [Father Mobile No] end as PMobile,Address1 from [TMU$Student - COLLEGE] where No_='" + stuEnr + "'", con1);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);

                    if (dt1.Rows.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("STUDENT NO. : " + dt1.Rows[0]["No_"].ToString() + "");
                        sb.Append(Environment.NewLine);
                        sb.Append("ENROLLMENT NO. : " + dt1.Rows[0]["Enrollment No_"].ToString() + "");
                        sb.Append(Environment.NewLine);
                        sb.Append("STUDENT NAME : " + dt1.Rows[0]["Student Name"].ToString() + "");
                        sb.Append(Environment.NewLine);
                        sb.Append("PROGRAM : " + dt1.Rows[0]["Course Name"].ToString() + "");
                        sb.Append(Environment.NewLine);
                        sb.Append("PASSOUT YEAR : " + dt1.Rows[0]["AcademicYear1"].ToString() + "");
                        sb.Append(Environment.NewLine);
                        sb.Append("Mobile : " + dt1.Rows[0]["Mobile Number"].ToString() + "");
                        //sb.Append(Environment.NewLine);
                        //sb.Append("Address : " + dt1.Rows[0]["Address1"].ToString() + "");
                        QRCodeGenerator qrGenerator = new QRCodeGenerator();
                        QRCodeData qrCodeData = qrGenerator.CreateQrCode(sb.ToString(), QRCodeGenerator.ECCLevel.Q);
                        QRCode qrCode = new QRCode(qrCodeData);
                        Bitmap qrCodeImage = qrCode.GetGraphic(30);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            byte[] byteImage = ms.ToArray();
                            imgBarcode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                            imgBarcode.Visible = true;
                        }
                    }
                }
            }
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }

    }
    public void numberseries()
    {

        SqlDataAdapter sda = new SqlDataAdapter("Select * from Tbl_StudentNodues", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        int i = dt.Rows.Count;
        string Nodues = Convert.ToString(i + 1);
      //  txtnoduesid.Text += i.ToString("TMU/Exam/NoDues/244001") + Nodues;

    }

    public void binddata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        string strSQL = "select Case when Gender=1 then 'Male' else 'Female' end as Gender,'20' +RIGHT([Academic Year], 2) AS AcademicYear1, (Select [Exam Course Name] from [TMU$Course - COLLEGE] where [Code]=P.[Course Code]) As CourseName, *  from [EDUCOLLEGELIVE-R2].dbo.[TMU$Student - COLLEGE] as P where No_='" + Request.QueryString["stuEnrValue"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        lblname.Text = dt.Rows[0]["Student Name"].ToString();
        lblenrollment.Text = dt.Rows[0]["Enrollment No_"].ToString();
        lblfathername.Text = dt.Rows[0]["Fathers Name"].ToString();
        // txtmobile.Text = dt.Rows[0]["Mobile Number"].ToString();
        // txtemailid.Text = dt.Rows[0]["E-Mail Address"].ToString();
        lblprogram.Text = dt.Rows[0]["CourseName"].ToString();
        //lblcertifiedname.Text = dt.Rows[0]["Student Name"].ToString();
        //lblYear.Text = dt.Rows[0]["Academic Year"].ToString();      
        lblacedmicyear.Text = dt.Rows[0]["AcademicYear1"].ToString();
        //lblYear.Text = dt.Rows[0]["AcademicYear1"].ToString();
        // lblacedmicyear.Text= dt.Rows[0]["Academic Year"].ToString();
    }
    public void binddate()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "select *,  FORMAT(CAST([COE Approval Date] AS DATETIME), 'dd/MM/yyyy') AS COEApprovaldate from Tbl_StudentNodues where [Enrollement No]='" + Session["enroll"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            lblrefno.Text = dt.Rows[0]["No_Dues_Id"].ToString();
            lbldate.Text = dt.Rows[0]["COEApprovaldate"].ToString();
            //if (dt.Rows[0]["Elegible"].ToString() == "Y")
            //{
            //    chkelegebleyesorno.Checked = true;
            //}
            //else
            //{
            //    chkelegebleyesorno.Checked = false;
            //}
        }
    }
     public void hidenodues()
    {
        string COEStatus = "";
        string InternshipStatus = "";
        string enNo = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        {

            SqlCommand cmd1 = new SqlCommand("Select [Enrollement No] as EnrollementNo,[COE Status] as COEStatus,[Internship Status] as InternshipStatus from Tbl_StudentNodues where [ST NO_]='" + Request.QueryString["stuEnrValue"].ToString() + "'", con);
            con.Open();
            SqlDataReader dr = cmd1.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    COEStatus = dr["COEStatus"].ToString();
                    InternshipStatus= dr["InternshipStatus"].ToString();
                    enNo= dr["EnrollementNo"].ToString();
                }
            }
            con.Close();
            SqlCommand cmd = new SqlCommand("SP_NoDuesStatus", con);
            con.Open();
            cmd.Parameters.AddWithValue("@enrNo", enNo);
            cmd.CommandType = CommandType.StoredProcedure;
            object result = cmd.ExecuteScalar();
            int value = 0;
            if (result != null)
            {
                value = Convert.ToInt32(result.ToString());
            }
            con.Close();
            if ((value == 11 && (InternshipStatus=="Internship Done" || InternshipStatus== "Not Applicable")) || COEStatus == "Submit")

            {
                divnodues1.Visible = true;
                Button1.Visible = true;
            }
            else
            {
                divnodues1.Visible = false;
            }
        }
    }
 
}

