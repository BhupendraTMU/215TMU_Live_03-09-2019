using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.IO;


public partial class Faculty_EventCreation : System.Web.UI.Page
{
    string CollegeCode = "";
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Session["UserGroup"].ToString() == "PRINCIPAL" || Session["EventCo-Ordinator"].ToString() != "" || Session["UserGroup"].ToString() == "REGISTRAR")
            {
                try
                {
                    txtEvent.Focus();// by me
                    CollegeCode = Session["GlobalDimension1Code"].ToString();
                    if (!IsPostBack)
                    {
                        if (Session["UserGroup"].ToString() != "REGISTRAR")
                        {
                            rblUniversity.Enabled = true;
                            drpcollege.Enabled = false;
                            drpcollege.BackColor = System.Drawing.Color.Gray;
                            rfvCollege.Visible = false;
                            rblUniversity.AutoPostBack = false;
                        }
                        else
                        {
                            bindcollegedrp();
                        }
                        BinddlEventType();
                        BindGrid();
                        // BindGridpop();
                    }

                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    public void bindcollegedrp()
    {
        using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("select Code, (Code +' - '+ Name) as BranchName from [TMU$Dimension Value] where [Dimension Code]='COLLEGE' and [Active College]='1'", Conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            drpcollege.DataSource = dt1;
            drpcollege.DataTextField = "BranchName";
            drpcollege.DataValueField = "Code";
            drpcollege.DataBind();
            drpcollege.Items.Insert(0, "-- Select College --");

        }
    }
    public void BinddlEventType()
    {
        SqlCommand cmd = new SqlCommand("Sp_getEventType", con); //ok
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlEventType.DataSource = dt;
        ddlEventType.DataTextField = "Details";
        ddlEventType.DataValueField = "Value";
        ddlEventType.DataBind();
    }
    private void BindGrid()
    {
        if (Session["UserGroup"].ToString() != "REGISTRAR")
        {
            string FrmDate = System.DateTime.Now.AddMonths(-2).ToString("dd MMM yyyy");
            string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Select Code,Event,College,Date,[Type of Event],case [Type of Event] when 0 then '' when 1 then 'Curricular' when 2 then  'Co-Curricular' when 3 then 'Extra-Curricular' when 4 then 'Work Shop' when 5 then 'Seminar' end as EventType,[Name of Guest Faculty],[Organization],[Objective of Event] from TMU$Events  where Active=0 and [Created By]='" + Session["uid"].ToString() + "' and [Date]>='" + FrmDate + "'  order by Date asc"))
                {
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        GridView1.DataSource = dt;
                        GridView1.DataBind();


                    }
                }
            }

        }
        else
        {
            string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {

                using (SqlCommand cmd = new SqlCommand("Select Code,Event,College,Date, [Type of Event],case [Type of Event] when 0 then '' when 1 then 'Curricular' when 2 then  'Co-Curricular' when 3 then 'Extra-Curricular' when 4 then 'Work Shop' when 5 then 'Seminar' end as EventType,[Name of Guest Faculty],[Organization],[Objective of Event] from TMU$Events  where Active=0  and [Date]>= '" + DateTime.Now.ToShortDateString() + "' order by Date asc"))
                {
                    cmd.Connection = con;


                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        GridView1.DataSource = dt;
                        GridView1.DataBind();


                    }
                }
            }
        }
    }

    protected void btnaddc_Click(object sender, EventArgs e)
    {
        try
        {          
            string FromTo = "";
            if (txtFromDate.Text == txtToDate.Text ) { FromTo = txtToDate.Text; } else { FromTo = txtFromDate.Text + " - " + txtToDate.Text; }
            //string a = Convert.ToDateTime(txtFromDate.Text).AddDays(1).ToString("dd MMM yyyy");
           int remaindate=  (DateTime.Parse(txtToDate.Text).Date-DateTime.Parse(txtFromDate.Text).Date).Days;
           if (remaindate == null)
           {
              // lblmsg.Text = "you have left with " + remaindate + "days.";
               return;
           }
           else
           {
               String TDate = txtFromDate.Text;
               for (int i = 1; i <= Convert.ToInt16(remaindate+1); i++)
               {
                   SqlCommand cmd = new SqlCommand();
                   cmd.CommandText = "insert into TMU$Events(Event,College,Date,[Created By],[Created Date],[Updated Date],[Upadted By],Active,University,[Type of Event],[Name of Guest Faculty],[Organization],[Objective of Event],[Image 1],[Image 2],[Image 3],FromTo) values(@Event,@College,@Date,@createdby,getdate(),getdate(),'" + Session["uid"].ToString() + "',0,'" + rblUniversity.SelectedValue + "',@typeofevent,@namegest,@Organization,@objectevent,'','','','" + FromTo + "' )";
                   cmd.Parameters.AddWithValue("@Event", txtEvent.Text);
                   if (Session["UserGroup"].ToString() != "REGISTRAR")
                   {
                       if (rblUniversity.SelectedValue == "0")
                       {
                           cmd.Parameters.AddWithValue("@College", Session["GlobalDimension1Code"].ToString());

                       }
                       else
                       {
                           cmd.Parameters.AddWithValue("@College", "");
                       }

                   }
                   else
                   {
                       if (rblUniversity.SelectedValue == "0")
                       {
                           cmd.Parameters.AddWithValue("@College", drpcollege.SelectedValue);

                       }
                       else
                       {
                           cmd.Parameters.AddWithValue("@College", "");
                       }
                   }

                   cmd.Parameters.AddWithValue("@Date", TDate);//cmd.Parameters.AddWithValue("@Date", txtToDate.Text);
                   cmd.Parameters.AddWithValue("@createdby", Session["uid"].ToString());
                   cmd.Parameters.AddWithValue("@typeofevent", ddlEventType.SelectedValue);
                   cmd.Parameters.AddWithValue("@namegest", txtguestfaculty.Text);
                   cmd.Parameters.AddWithValue("@Organization", txtorganization.Text);
                   cmd.Parameters.AddWithValue("@objectevent", txtobjectievent.Text);

                   cmd.Parameters.AddWithValue("@createdate", DateTime.Now.ToShortDateString());
                   cmd.Connection = con;
                   con.Open();
                   cmd.ExecuteNonQuery();
                   con.Close();
                   TDate=Convert.ToDateTime(txtFromDate.Text).AddDays(i).ToString("dd MMM yyyy");
               }
           }
           BindGrid();
           txtToDate.Text = "";
           txtEvent.Text = "";
           drpcollege.Text = "";
           txtguestfaculty.Text = "";
           txtorganization.Text = "";
           txtobjectievent.Text = "";
           ddlEventType.SelectedIndex = -1;
        }

        catch
        {

        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }

    }

    protected void btnDelete_Click1(object sender, EventArgs e)
    {
        try
        {

            Button btn = sender as Button;
            GridViewRow grow = btn.NamingContainer as GridViewRow;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "update TMU$Events set Active=1,[Upadted By]=@updatedby  where Code=@Code";
            cmd.Parameters.AddWithValue("@Code", (grow.FindControl("lblCustomerID") as Label).Text);
            cmd.Parameters.AddWithValue("@updatedby", (Session["uid"].ToString()));
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            BindGrid();
        }
        catch
        {

        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "update TMU$Events set Event=@Event,Date=@Date,[Upadted By]=@updateby,[Updated Date]=@updatedate,[Name of Guest Faculty]=@namegest,[Organization]=@Organization,[Objective of Event]=@objectevent , [Type of Event] =@EventType where Code=@Code";
            cmd.Parameters.AddWithValue("@Event", txtEvent.Text);
            cmd.Parameters.AddWithValue("@Date", txtToDate.Text);

            cmd.Parameters.AddWithValue("@namegest", txtguestfaculty.Text);
            cmd.Parameters.AddWithValue("@Organization", txtorganization.Text);
            cmd.Parameters.AddWithValue("@objectevent", txtobjectievent.Text);

            cmd.Parameters.AddWithValue("@updatedate", DateTime.Now);
            cmd.Parameters.AddWithValue("@updateby", (Session["uid"].ToString()));
            cmd.Parameters.AddWithValue("@Code", hidCustomerID.Value);
            cmd.Parameters.AddWithValue("@EventType", ddlEventType.SelectedValue);
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            BindGrid();
            btnaddc.Visible = true;
            btnUpdate.Visible = false;
            txtToDate.Text = "";
            txtEvent.Text = "";
            txtguestfaculty.Text = "";
            txtorganization.Text = "";
            txtobjectievent.Text = "";
            ddlEventType.SelectedIndex = -1;
        }
        catch
        {

        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            // ClearControls();
            Button btn = sender as Button;
            GridViewRow grow = btn.NamingContainer as GridViewRow;
            hidCustomerID.Value = (grow.FindControl("lblCustomerID") as Label).Text;
            txtEvent.Text = (grow.FindControl("lblEvent") as Label).Text;
            txtToDate.Text = (grow.FindControl("lbldate") as Label).Text;
            txtguestfaculty.Text = (grow.FindControl("lblfaculty") as Label).Text;
            txtobjectievent.Text = (grow.FindControl("lblobjEvent") as Label).Text;
            txtorganization.Text = (grow.FindControl("lblorganization") as Label).Text;

            ddlEventType.SelectedValue = (grow.FindControl("hfEventType") as HiddenField).Value;
            btnaddc.Visible = false;
            btnUpdate.Visible = true;

        }
        catch
        {

        }
    }

    protected void rblUniversity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblUniversity.SelectedValue == "0")
        {
            drpcollege.Enabled = true;
            drpcollege.BackColor = System.Drawing.Color.White;
        }
        else
        {
            drpcollege.Enabled = false;
            drpcollege.BackColor = System.Drawing.Color.Gray;
        }
    }



    // for popup

    private void BindGridpop()
    {
        string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Sp_ValidateAndGetImage"); // sandeep
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Code", Label1.Text);
            cmd.Connection = con;
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);
                GridViewpop.DataSource = dt;
                GridViewpop.DataBind();


            }
        }
    }

    



    protected void Btnpop_Click(object sender, EventArgs e)
    {
        Button btndetails = sender as Button;
        GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
        Label1.Text = GridView1.DataKeys[gvrow.RowIndex].Value.ToString();
        this.ModalPopupExtender1.Show();
        this.BindGridpop();


    }

    // for file upload


    public void insertevntphot(int i, string str)
    {

        using (SqlConnection con = new SqlConnection(constr))
        {

            SqlCommand cmd = new SqlCommand("update TMU$Events set [Image " + i + "]=@imgpath1 where Code='" + Label1.Text + "'", con);
            cmd.Parameters.AddWithValue("@imgpath1", str);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();


        }

    }
    public void InserAndSavefile()
    {
        if (FileUploadimg1.HasFile)
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;

                string filename = Path.GetFileName(FileUploadimg1.FileName);
                string folderName = @"C:\fileupload\Event\";
                string pathString = ""; string pathString1 = "";
                pathString = System.IO.Path.Combine(folderName, Label1.Text);                
                System.IO.Directory.CreateDirectory(pathString);
                //try
                //{
                //    pathString1 = System.IO.Path.Combine("~\\EventImage\\", Label1.Text);
                //    System.IO.Directory.CreateDirectory(pathString1);
                //}
                //catch { }
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("Sp_ValidateAndGetImage", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Code", Label1.Text);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    string s = dr["Img"].ToString();
                    if (s == "1" || s == "2" || s == "3")
                    {
                        string[] ext = filename.Split('.');
                        string ext1 = ext[1];
                        filename = s+"." + ext1;
                        pathString = System.IO.Path.Combine(pathString, filename);
                        insertevntphot(Convert.ToInt32(s), pathString);
                        FileUploadimg1.SaveAs(pathString);

                        lblmsg.Text = "Upload status: File uploaded!";
                        BindGridpop();
                        //try { FileUploadimg1.SaveAs(pathString1); }
                        //catch { }
                        

                    }
                    
                    if (s == "4")
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "You already upload 3 photo of event";
                    }
                }



            }
            catch (Exception ex)
            {
                lblmsg.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
            }
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        InserAndSavefile();
        ModalPopupExtender1.Show();
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
        ModalPopupExtender1.Show();
    }
    protected void lnkDownload2_Click(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();


        ModalPopupExtender1.Show();

    }
    protected void lnkDownload3_Click(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();


        ModalPopupExtender1.Show();
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        updateimge(1);
        string filePath = (sender as LinkButton).CommandArgument;
        string newPath = filePath.Replace(@"~\EventImage", @"C:\fileupload\Event");
        File.Delete(newPath);

    }
    public void updateimge(int i)
    {
        using (SqlConnection con = new SqlConnection(constr))
        {

            SqlCommand cmd = new SqlCommand("update TMU$Events set [Image "+i+"]='' where Code='" + Label1.Text + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            BindGridpop();
            ModalPopupExtender1.Show();
            lblmsg.Text = "File Deleted!";
        }
    }
    protected void lnkDelete3_Click(object sender, EventArgs e)
    {
        updateimge(3);
        string filePath = (sender as LinkButton).CommandArgument;
        string newPath = filePath.Replace(@"~\EventImage", @"C:\fileupload\Event");
        File.Delete(newPath);
       
           
    }
    
    protected void lnkDelete2_Click(object sender, EventArgs e)
    {
        updateimge(2);
        string filePath = (sender as LinkButton).CommandArgument;
        string newPath = filePath.Replace(@"~\EventImage", @"C:\fileupload\Event");
        File.Delete(newPath);
       
       
    }
}