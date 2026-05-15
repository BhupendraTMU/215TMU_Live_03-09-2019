using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_DailyRound : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    private string Shift_id;
    private string Shift_timing_id;

    public object MessageBox { get; private set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {

                txtsupervisor.Text = Session["Fulname"].ToString();
                hospital();
                getsuper();
                getfloor();
                getward();
                getshift_timing();
                getmoment();
            }


            catch
            {
                Response.Redirect("../Default.aspx");
            }

        }

        }
    

    public void getfloor()
    {

        SqlCommand cmd = new SqlCommand("sp_getFLLOR", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);

        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        drpfloorname.DataSource = dtCL;
        drpfloorname.DataTextField = "Floor_Name";
        drpfloorname.DataValueField = "Floor_Id";
        drpfloorname.DataBind();
    }
    public void hospital()
    {
        SqlCommand cmd = new SqlCommand("sp_getwardassistant", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        drpwardassistantname.DataSource = dtCL;
        drpwardassistantname.DataTextField = "Employee Name";
        drpwardassistantname.DataValueField = "ID";
        drpwardassistantname.DataBind();

    }
    public void getsuper()
    {

        SqlCommand cmd = new SqlCommand("pro_getroundreport", con);
        cmd.Parameters.Add("@UserId", Session["uid"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
     
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdroundreport.DataSource = dtCL;
        grdroundreport.DataBind();
    }
    public void getward()
    {
        SqlCommand cmd = new SqlCommand("sp_getwarddata", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        cmd.Parameters.Add("@floor_Name", drpfloorname.SelectedValue);
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        drpwardname.DataSource = dtCL;
        drpwardname.DataTextField = "Ward_Name";
        drpwardname.DataValueField = "Ward_Id";
        drpwardname.DataBind();
    }
    public void getshift_timing()
    {
        SqlCommand cmd = new SqlCommand("sp_getshift", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Shift", drpshift.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpshifttiming.DataSource = dt;
        drpshifttiming.DataTextField = "Shift_timing";
        drpshifttiming.DataValueField = "Shift_timing_id";
        drpshifttiming.DataBind();
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {

        string contentType1 = "";
        byte[] Photo = new byte[720];
        if (txtPhotoUploader.HasFile)
        {
            contentType1 = txtPhotoUploader.PostedFile.ContentType;
            string filename = Path.GetFileName(txtPhotoUploader.PostedFile.FileName);
            using (Stream fs = txtPhotoUploader.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    Photo = br.ReadBytes((Int32)fs.Length);
                }
            }
        }
        else
        {
            contentType1 = "";
            string filename = "";
            using (Stream fs = txtPhotoUploader.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    Photo = br.ReadBytes((Int32)0);
                }
            }
        }


            if (drpwardassistantname.SelectedItem.Text == "--SELECT--")
        {
            string message1 = "Please Fill Ward Assistant Name.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (drpshift.SelectedIndex == 0)
        {
            string message1 = "Please Fill Shift.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (drpfloorname.SelectedIndex == 0)
        {
            string message1 = "Please Fill Floor Name.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (drpwardname.SelectedIndex == 0)
        {
            string message1 = "Please Fill Ward Name.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (drpshifttiming.SelectedIndex == 0)
        {
            string message1 = "Please Fill Shift Timing";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (DropDownList1.SelectedIndex == 0)
        {
            string message1 = "Please Fill Status.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (DropDownList2.SelectedIndex == 0)
        {
            string message1 = "Please Fill Complain.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (drproundtime.SelectedIndex == 0)
        {
            string message1 = "Please Fill Round Time.";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("sp_Insertrounddetail", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Supervisor_Name", txtsupervisor.Text);
        cmd.Parameters.AddWithValue("@Ward_Assistant_Name", drpwardassistantname.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Temp_Ward_Assistant_Name", txtwardassistant.Text);
        cmd.Parameters.AddWithValue("@floor_Name", drpfloorname.Text);
        cmd.Parameters.AddWithValue("@Ward_Name", drpwardname.Text);
        cmd.Parameters.AddWithValue("@Shift", drpshift.Text);
        cmd.Parameters.AddWithValue("@Shift_Timing", drpshifttiming.Text);
        cmd.Parameters.AddWithValue("@Round_Time", drproundtime.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Status", DropDownList1.Text);
        cmd.Parameters.AddWithValue("@Complain", DropDownList2.Text);
        cmd.Parameters.AddWithValue("@Other_Complain", txtother.Text);
        cmd.Parameters.AddWithValue("@Moment", drpmoment.SelectedValue);
        cmd.Parameters.AddWithValue("@Out_Time", txtouttime.Text);
        cmd.Parameters.AddWithValue("@In_Time", txtintime.Text);
        cmd.Parameters.AddWithValue("@Place", txtplace.Text);
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Upload_Photo", Photo);
        cmd.Parameters.AddWithValue("@ID", txtID.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "Your details have been saved successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        txtother.Text = "";
        drpwardassistantname.SelectedIndex = 0;
        drpfloorname.SelectedIndex = 0;
        drpshift.SelectedIndex = 0;
        drpshifttiming.SelectedIndex = 0;
        drpwardname.SelectedIndex = 0;
        DropDownList1.SelectedIndex = 0;
        drproundtime.SelectedIndex = 0;
        DropDownList2.SelectedIndex = 0;
        drpmoment.SelectedIndex = 0;
        txtplace.Text = "";
        txtouttime.Text = "";
        txtintime.Text = "";
        getsuper();

    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnexporttoexel_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grdroundreport.RenderControl(htmlWrite);

        Response.Clear();
        string str = "ROUNDREPORT" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();
    }
    protected void drpfloorname_SelectedIndexChanged(object sender, EventArgs e)
    {

        getward();


    }


    protected void drpshift_SelectedIndexChanged(object sender, EventArgs e)
    {
        getshift_timing();


    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedValue == "7")
        {
            txtother.Visible = true;
            other.Visible = true;
            OtherTD.Visible = true;
        }
        else
        {
            txtother.Visible = false;
            other.Visible = false;
            OtherTD.Visible = false;
        }
    }



    protected void btnselect_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        GridViewRow grow = btn.NamingContainer as GridViewRow;
        string pk = grdroundreport.DataKeys[grow.RowIndex].Values[0].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "Select * from tbl_supervisorround where ID='" + pk + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        drpwardassistantname.SelectedItem.Text = dt.Rows[0]["Ward_Assistant_Name"].ToString();
        drpfloorname.SelectedValue = dt.Rows[0]["Floor_Name"].ToString();
        getward();
        drpwardname.SelectedValue = dt.Rows[0]["Ward_Name"].ToString();

        drpshift.SelectedValue = dt.Rows[0]["Shift"].ToString();
        getshift_timing();
        drpshifttiming.SelectedValue = dt.Rows[0]["Shift_Timing"].ToString();
        txtother.Text = dt.Rows[0]["Other_Complain"].ToString();
        txtplace.Text = dt.Rows[0]["Place1"].ToString();
        txtID.Text = dt.Rows[0]["ID"].ToString();

    }
    public void getmoment()
    {
        SqlCommand cmd = new SqlCommand("sp_getmoment", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Round", drproundtime.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpmoment.DataSource = dt;
        drpmoment.DataTextField = "Moment_Time";
        drpmoment.DataValueField = "Moment_ID";
        drpmoment.DataBind();
    }




    protected void drproundtime_SelectedIndexChanged(object sender, EventArgs e)
    {
        getmoment();
    }

    protected void drpmoment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpmoment.SelectedValue == "0")
        {
            txtintime.Visible = false;
            txtouttime.Visible = false;
            lblTime.Visible = false;
            lblPlace.Visible = false;
            txtplace.Visible = false;

        }
        if (drpmoment.SelectedValue == "1")
        {
            lblTime.Text = "Out Time1";
            txtouttime.Visible = true;
            txtintime.Visible = false;
            lblTime.Visible = true;
            lblPlace.Visible = true;
            txtplace.Visible = true;
        }

        if (drpmoment.SelectedValue == "2")
        {
            lblTime.Text = "In Time1";
            txtouttime.Visible = false;
            txtintime.Visible = true;
            lblTime.Visible = true;
            lblPlace.Visible = true;
            txtplace.Visible = true;
        }
        if (drpmoment.SelectedValue == "3")
        {
            lblTime.Text = "Out Time2";
            txtouttime.Visible = true;
            txtintime.Visible = false;
            lblTime.Visible = true;
            lblPlace.Visible = true;
            txtplace.Visible = true;
        }
        if (drpmoment.SelectedValue == "4")
        {
            lblTime.Text = "In Time2";
            txtouttime.Visible = false;
            txtintime.Visible = true;
            lblTime.Visible = true;
            lblPlace.Visible = true;
            txtplace.Visible = true;
        }
        if (drpmoment.SelectedValue == "5")
        {
            lblTime.Text = "Out Time3";
            txtouttime.Visible = true;
            txtintime.Visible = false;
            lblTime.Visible = true;
            lblPlace.Visible = true;
            txtplace.Visible = true;
        }
        if (drpmoment.SelectedValue == "6")
        {
            lblTime.Text = "In Time3";
            txtouttime.Visible = false;
            txtintime.Visible = true;
            lblTime.Visible = true;
            lblPlace.Visible = true;
            txtplace.Visible = true;
        }


        if (drpmoment.SelectedValue == "7")
        {
            lblTime.Text = "Out Time4";
            txtintime.Visible = false;
            txtouttime.Visible = true;
            lblTime.Visible = true;
            lblPlace.Visible = true;
            txtplace.Visible = true;

        }
        if (drpmoment.SelectedValue == "8")
        {
            lblTime.Text = "In Time4";
            txtintime.Visible = true;
            txtouttime.Visible = false;
            lblTime.Visible = true;
            lblPlace.Visible = true;
            txtplace.Visible = true;

        }
        if (drpmoment.SelectedValue == "9")
        {
            lblTime.Text = "Out Time5";
            txtintime.Visible = false;
            txtouttime.Visible = true;
            lblTime.Visible = true;
            lblPlace.Visible = true;
            txtplace.Visible = true;

        }
        if (drpmoment.SelectedValue == "10")
        {
            lblTime.Text = "In Time5";
            txtintime.Visible = true;
            txtouttime.Visible = false;
            lblTime.Visible = true;
            lblPlace.Visible = true;
            txtplace.Visible = true;

        }
        if (drpmoment.SelectedValue == "11")
        {
            lblTime.Text = "Out Time6";
            txtintime.Visible = false;
            txtouttime.Visible = true;
            lblTime.Visible = true;
            lblPlace.Visible = true;
            txtplace.Visible = true;

        }
        if (drpmoment.SelectedValue == "12")
        {
            lblTime.Text = "In Time6";
            txtintime.Visible = true;
            txtouttime.Visible = false;
            lblTime.Visible = true;
            lblPlace.Visible = true;
            txtplace.Visible = true;

        }
    }
    protected void drpwardassistantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(drpwardassistantname.SelectedItem.Text=="OTHER")
        {
            lblwardassistant.Visible = true;
            txtwardassistant.Visible = true;
        }
        else
        {
            lblwardassistant.Visible = false;
            txtwardassistant.Visible = false;
        }
    }




    protected void drpwardassistantname_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (drpwardassistantname.SelectedItem.Text == "OTHER")
        {
            lblwardassistant.Visible = true;
            txtwardassistant.Visible = true;
        }
        else
        {
            lblwardassistant.Visible = false;
            txtwardassistant.Visible = false;
        }

    }
    public Stream DisplayImage(string theID)
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());

        string sql = "select Upload_Photo from  tbl_Supervisorround  where ID=" + theID + "";
        SqlCommand cmd = new SqlCommand(sql, connection);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@ID", theID);
        connection.Open();
        object theImg = cmd.ExecuteScalar();
        try
        {
            return new MemoryStream((byte[])theImg);
        }
        catch
        {
            return null;
        }
        finally
        {
            connection.Close();
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    protected void lnkPhoto_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        GridViewRow row = btn.NamingContainer as GridViewRow;
        string id = grdroundreport.DataKeys[row.RowIndex].Values[0].ToString();
        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select Upload_Photo from tbl_Supervisorround where ID=" + id + "";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Upload_Photo"];


                    fileName = "Photo";
                }
                con.Close();
            }
        }
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".jpeg");
        Response.ContentType = "image/jpeg"; ;
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }

    protected void drpwardassistantname_SelectedIndexChanged2(object sender, EventArgs e)
    {
        if (drpwardassistantname.SelectedItem.Text == "OTHER")
        {
            lblwardassistant.Visible = true;
            txtwardassistant.Visible = true;
        }
        else
        {
            lblwardassistant.Visible = false;
            txtwardassistant.Visible = false;
        }

    }
}






