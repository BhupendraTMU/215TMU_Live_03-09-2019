using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text;
using System.Drawing;

public partial class Faculty_ComplaintList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                bindOrderList();
                PopulateDepartments();
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../Default.aspx");
        }
    }
  
    public void bindOrderList()
    {
        OrderList1.DataSource = null;
        OrderList1.DataBind();

        if (Request.Browser.IsMobileDevice)
        {
            OrderList1.Width = 380;
        }
        con1.Open();
        SqlDataAdapter da = new SqlDataAdapter("Customer_GetComplaintList '" + Session["uid"].ToString() + "'", con1);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            OrderList1.DataSource = dt;
            OrderList1.DataBind();
            if (OrderList1.Rows.Count > 0)
            {
                OrderList1.HeaderRow.Cells[0].Attributes["data-class"] = "expand";


                OrderList1.HeaderRow.Cells[1].Attributes["data-class"] = "phone";
                OrderList1.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.Cells[5].Attributes["data-class"] = "phone";
                //GridView1.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.Cells[7].Attributes["data-hide"] = "phone";


                OrderList1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }


    }
    protected void OrderList1_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        OrderList1.DataSource = null;
        OrderList1.DataBind();
        int val=Convert.ToInt32(getval.Value);
        OrderList1.PageIndex = e.NewPageIndex;
        if (val==1)
        {
            bindOrderListDone();
        }
        else if (val == 2)
        {
            bindOrderListDept();
        }
        else
        {
            bindOrderList();
        }
        
    }

    //public void BindDepartment()
    //{
    //    SqlCommand cmd = new SqlCommand("proc_fatchDepartment", con1);
    //    cmd.CommandType = CommandType.StoredProcedure;

    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        ddlDepartment.DataSource = dt;
    //        ddlDepartment.DataTextField = "ID";
    //        ddlDepartment.DataValueField = "ID";
    //        ddlDepartment.DataBind();


    //    }
    //}
    //public void BindBuilding()
    //{
    //    SqlCommand cmd = new SqlCommand("proc_fatchBuilding", con1);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.Add("@Depart", "");
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        ddlArea.DataSource = dt;
    //        ddlArea.DataTextField = "Area";
    //        ddlArea.DataValueField = "Area";
    //        ddlArea.DataBind();
    //    }
    //}

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        byte[] bytes = null;
        byte[] imgtype = { 0 };
        string filename = "";
        string contentType = "";
        string Code = "";
        if (flUpload.HasFile)
        {

            filename = Path.GetFileName(flUpload.PostedFile.FileName);
            contentType = flUpload.PostedFile.ContentType;
            using (Stream fs = flUpload.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    bytes = br.ReadBytes((Int32)fs.Length);
                }
            }
        }

        SqlCommand cmd = new SqlCommand("Customer_UpdateComplaint", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ComplaintNo", txtComplaint.Text);
        cmd.Parameters.Add("@UseId", Session["uid"].ToString());
        cmd.Parameters.Add("@Remark", txtRemarks.Text);
        cmd.Parameters.Add("@FileName", filename);
        cmd.Parameters.Add("@contentType", contentType);
        if (bytes == null)
        {
            cmd.Parameters.Add("@Attachment", imgtype);
        }
        else
        {
            cmd.Parameters.Add("@Attachment", bytes);
        }
        cmd.Parameters.Add("@Status", 2);
        if (con1.State == ConnectionState.Closed)
            con1.Open();
        cmd.ExecuteNonQuery();
        cmd.ExecuteNonQuery();
        con1.Close();
        txtRemarks.Text = "";
        SMS(CoHeadNumber.Value.ToString(), "Dear " + hfCo_Head.Value.ToString() + ", Maintenance Complaint No. " + txtComplaint.Text + " is approved by Warden " + hfWarden.Value.ToString() + ", Hostel " + txtArea.Text + ", Room No. " + txtRoom.Text + " Please login at portal.tmu.ac.in to view the complaint. Thank you. TMU");


        SMS(HeadNumber.Value.ToString(), "Dear " + hfHead.Value.ToString() + ", Maintenance Complaint No. " + txtComplaint.Text + " is received by Co-Head " + hfCo_Head.Value.ToString() + ", Hostel " + txtArea.Text + ", Warden " + hfWarden.Value.ToString() + ". Please login at portal.tmu.ac.in to view the complaint. Thank you. TMU");
       
        
        
        
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Complaint " + txtComplaint.Text + " Approved Successfully');", true);





    }


    public void SMS(String MobileNo, string Msg)
    {
        //  Website: http://www.universalsmsadvertising.com/     //  Username: 9837016352   //  Password: 9837016352 
        MobileNo = "91" + MobileNo;
        // MobileNo = "91" + 9015762885;
        string url = "http://www.universalsmsadvertising.com/universalsmsapi.php?user_name=9837016352&user_password=9837016352&mobile=" + MobileNo + "&sender_id=TMUniv&type=F&text=" + Msg + "";

        // string url = "http://www.universalsmsadvertising.com/universalsmsapi.php?user_name=myuserid&user_password=mypassword&mobile=919834567120&sender_id=SWEET&type=F&text= " + Msg + "";



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
    protected void lnkSelect_Click(object sender, EventArgs e)
    {
        string Complaint = (sender as LinkButton).CommandArgument;
        main.Visible = true;
        Orderlist.Visible = false;
        status.Visible = false;
        con1.Open();
        SqlDataAdapter da = new SqlDataAdapter("Customer_GetComplaintByNo '" + Complaint + "'", con1);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtCustCode.Text = dt.Rows[0]["Customer Code"].ToString();
            txtCustName.Text = dt.Rows[0]["Customer Name"].ToString();

            txtRoom.Text = dt.Rows[0]["Room"].ToString();



            txtDept.Text = dt.Rows[0]["Department"].ToString();
            txtArea.Text = dt.Rows[0]["Area"].ToString();
            txtRemark.Text = dt.Rows[0]["Remarks"].ToString();
            txtComplaint.Text = Complaint;
            txtRemarks.Text = dt.Rows[0]["Remark1"].ToString();
            hfCo_Head.Value = dt.Rows[0]["Co_Head"].ToString();
            hfCustNAme.Value = dt.Rows[0]["Customer Name"].ToString();
            hfWarden.Value = dt.Rows[0]["WardenName"].ToString();
            hfHead.Value = dt.Rows[0]["Head"].ToString();
            CoHeadNumber.Value = dt.Rows[0]["CoHeadNumber"].ToString();
            HeadNumber.Value = dt.Rows[0]["HeadNumber"].ToString();
            hdfCustomerMobile.Value = dt.Rows[0]["CustomerMobile"].ToString();
            hdfWardenMobile.Value = dt.Rows[0]["WardenMobile"].ToString();

            if (dt.Rows[0]["Status"].ToString() == "Pending at Warden" && Session["uid"].ToString() == dt.Rows[0]["WardenCode"].ToString())
            {
                btnSubmit.Visible = true;
                btnReject.Visible = true;
                btnClose.Visible = false;
                btnResolve.Visible = false;
                txtRemarks.Enabled = true;
            }
            if (dt.Rows[0]["Status"].ToString() == "Resolved from Department" && Session["uid"].ToString() == dt.Rows[0]["WardenCode"].ToString())
            {
                btnClose.Visible = true;
                btnSubmit.Visible = false;
                btnReject.Visible = false;
                btnResolve.Visible = false;
                txtRemarks.Enabled = true;

            }
            if (dt.Rows[0]["Status"].ToString() == "Pending at Department" && (Session["uid"].ToString() == dt.Rows[0]["Co_HeadCode"].ToString() || Session["uid"].ToString() == dt.Rows[0]["HeadCode"].ToString()))
            {
                btnClose.Visible = false;
                btnSubmit.Visible = false;
                btnReject.Visible = false;
                btnResolve.Visible = true;
                txtRemarks.Enabled = false;
                ATTACH.Visible = false;
                ATTACHUPLOAD.Visible = false;

            }


            if (dt.Rows[0]["AttachmentFilename"].ToString() == "")
            {
                lnkAttachment.Visible = false;
            }
            else
            {
                lnkAttachment.Visible = true;
            }
        }




    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ComplaintList.aspx");
    }
    protected void lnkAttachment_Click(object sender, EventArgs e)
    {


        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strportal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {

                cmd2.CommandText = "select Attachment,AttachmentFilename,AttachmentFileType from tbl_Complaint where [Complaint_No]='" + txtComplaint.Text + "' ";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Attachment"];
                    contentType = sdr["AttachmentFilename"].ToString();
                    fileName = sdr["AttachmentFileType"].ToString();
                }
                con2.Close();
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
    protected void btnReject_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("Customer_RejectComplaint", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ComplaintNo", txtComplaint.Text);
        cmd.Parameters.Add("@UseId", Session["uid"].ToString());
        cmd.Parameters.Add("@Remark", txtRemarks.Text);
        cmd.Parameters.Add("@Status", 3);
        if (con1.State == ConnectionState.Closed)
            con1.Open();
        cmd.ExecuteNonQuery();
        
        con1.Close();
        txtRemarks.Text = "";

         //{#var}, Maintenance Complaint No. {#var} is rejected by Warden {#var}. Please login at portal.tmu.ac.in to view the complaint status. Thank you. TMU
        SMS(hdfCustomerMobile.Value, "Dear Student " + txtCustName.Text.ToString() + ", Maintenance Complaint No. " + txtComplaint.Text + " is rejected by Warden " + hfWarden.Value.ToString() + ". Please login at portal.tmu.ac.in to view the complaint status. Thank you. TMU");
        

        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Complaint " + txtComplaint.Text + " Reject Successfully');", true);






    }
    protected void btnResolve_Click(object sender, EventArgs e)
    {

        SqlCommand cmd = new SqlCommand("Customer_ResolvedComplaint", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ComplaintNo", txtComplaint.Text);
        cmd.Parameters.Add("@UseId", Session["uid"].ToString());
        cmd.Parameters.Add("@Remark", txtRemarks.Text);
        cmd.Parameters.Add("@Status", 4);


        if (con1.State == ConnectionState.Closed)
            con1.Open();
        cmd.ExecuteNonQuery();
       
        con1.Close();
        txtRemarks.Text = "";

       // Dear Warden {#var}, Maintenance Complaint No. {#var} is resolved by Department Co-Head {#var}. Please login at portal.tmu.ac.in to close the complaint. Thank you. TMU


        SMS(hdfWardenMobile.Value.ToString(), "Dear Warden " + hfWarden.Value.ToString() + ", Maintenance Complaint No. " + txtComplaint.Text + " is resolved by Department Co-Head " + hfCo_Head.Value.ToString() + ". Please login at portal.tmu.ac.in to close the complaint. Thank you. TMU");
        





        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Complaint " + txtComplaint.Text + " Resolved Successfully');", true);

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("Customer_CloseComplaint", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ComplaintNo", txtComplaint.Text);
        cmd.Parameters.Add("@UseId", Session["uid"].ToString());
        cmd.Parameters.Add("@Remark", txtRemarks.Text);
        cmd.Parameters.Add("@Status", 5);
        if (con1.State == ConnectionState.Closed)
            con1.Open();
        cmd.ExecuteNonQuery();
        cmd.ExecuteNonQuery();
        con1.Close();
        txtRemarks.Text = "";
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Complaint " + txtComplaint.Text + " Closed Successfully');", true);
    }
    protected void OrderList1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string Status = e.Row.Cells[4].Text;



            if (Status == "Close")
            {
                e.Row.BackColor = Color.LightGreen;
            }
            if (Status == "Pending at Department")
            {
                e.Row.BackColor = Color.Red;
            }
            if (Status == "Pending at Warden")
            {
                e.Row.BackColor = Color.Orange;
            }

        }
    }
    public void lnkbutton_Click1xx(object sender, EventArgs e)
    {
        getval.Value = "2";
        bindOrderListDept();       
    }
    public void bindOrderListDept()
    {
        OrderList1.DataSource = null;
        OrderList1.DataBind();

        if (Request.Browser.IsMobileDevice)
        {
            OrderList1.Width = 380;
        }
        SqlCommand cmd = new SqlCommand("Customer_GetComplaintListByDept", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UseId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Dept", ddlDept.SelectedValue.ToString());
        if (con1.State == ConnectionState.Closed)
            con1.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        daCL.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            OrderList1.DataSource = dt;
            OrderList1.DataBind();
            if (OrderList1.Rows.Count > 0)
            {
                OrderList1.HeaderRow.Cells[0].Attributes["data-class"] = "expand";


                OrderList1.HeaderRow.Cells[1].Attributes["data-class"] = "phone";
                OrderList1.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.Cells[5].Attributes["data-class"] = "phone";
                //GridView1.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.Cells[7].Attributes["data-hide"] = "phone";


                OrderList1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
    }
    private void PopulateDepartments()
    {
        string query = "SELECT  distinct [Maintenance Head] AS Department FROM [EDUCOLLEGELIVE-R2].dbo.[TMU$Hostel Maintenance Setup]";
        SqlDataAdapter da = new SqlDataAdapter(query, con1);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                // Bind the data to DropDownList
                ddlDept.DataSource = dt;
                ddlDept.DataTextField = "Department";  
                ddlDept.DataValueField = "Department"; 
                ddlDept.DataBind();
                ddlDept.Items.Insert(0, new ListItem("--Select Department--", ""));
            }
            else
            {
                ddlDept.Items.Clear();
                ddlDept.Items.Add(new ListItem("No departments available", ""));
            }
        
    }
    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        using (var package = new OfficeOpenXml.ExcelPackage())
        {
            // Add a worksheet to the package
            var worksheet = package.Workbook.Worksheets.Add("Complaints");

            // Add headers
            for (int i = 0; i < OrderList1.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1].Value = OrderList1.Columns[i].HeaderText;
            }

            // Add data rows
            for (int row = 0; row < OrderList1.Rows.Count; row++)
            {
                for (int col = 0; col < OrderList1.Columns.Count; col++)
                {
                    worksheet.Cells[row + 2, col + 1].Value = OrderList1.Rows[row].Cells[col].Text;
                }
            }

            // Set the response to download the file
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", "attachment; filename=ComplaintList.xlsx");
            Response.BinaryWrite(package.GetAsByteArray());
            Response.End();
        }
    }

 
    protected void Donebutton_Click(object sender, EventArgs e)
    {
        getval.Value = "1";
        bindOrderListDone();
    }
    public void bindOrderListDone()
    {
        if (Request.Browser.IsMobileDevice)
        {
            OrderList1.Width = 380;
        }
        OrderList1.DataSource = null;
        OrderList1.DataBind();
        SqlCommand cmd = new SqlCommand("Customer_GetComplaintListNew", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UseId", Session["uid"].ToString());
        if (con1.State == ConnectionState.Closed)
            con1.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        daCL.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            OrderList1.DataSource = dt;
            OrderList1.DataBind();
            if (OrderList1.Rows.Count > 0)
            {
                OrderList1.HeaderRow.Cells[0].Attributes["data-class"] = "expand";


                OrderList1.HeaderRow.Cells[1].Attributes["data-class"] = "phone";
                OrderList1.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.Cells[5].Attributes["data-class"] = "phone";
                //GridView1.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";
                OrderList1.HeaderRow.Cells[7].Attributes["data-hide"] = "phone";


                OrderList1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
    }
    protected void Pendingbutton_Click(object sender, EventArgs e)
    {
        bindOrderList();
    }
}