using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
public partial class Faculty_PunchDataReport : System.Web.UI.Page
{
    ServicePoratal con;
    Connection navconn;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {


            con = new ServicePoratal();
            navconn = new Connection();
            if (!IsPostBack)
            {
                showHRHODisexhist();
                BindYear();
                bindCollege(1);
                ddlMonth.SelectedValue = System.DateTime.Now.ToString("MM");
                ddlYear.SelectedValue = System.DateTime.Now.ToString("yyyy");

                ShowEMployeeDetails();
                ShowBasicinfo();
                ShowPunchdata();
            }
        }
        catch (Exception)
        {
            Response.Redirect("../Default.aspx");
        }
    }


    public void showHRHODisexhist()
    {

        SqlDataReader dr = navconn.SHow_showHODExhistavv(Session["uid"].ToString(), Session["CompanyTableEmployee"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            dr.Close();
            navconn.DisConnect();


        }
        else
        {
            dr.Close();
            navconn.DisConnect();
            Response.Redirect("Default.aspx");
        }
    }

    string fullname = ""; string EmployeeMachineCodeD = "";
    public void ShowBasicinfo()
    {
        string newName = string.Empty;
        string ccnameUN = Session["Company"].ToString();
        string rccname = ccnameUN.Replace(".", "_");
        string tbl_EmployeeActualpunchData = "[" + rccname + "$Employee" + "]";
        SqlDataReader dr = navconn.SHow_EmployeeBasicdetails(txtUserid.SelectedValue.ToString(), tbl_EmployeeActualpunchData);
        dr.Read();
        if (dr.HasRows)
        {
            fullname = dr["First Name"].ToString().Trim() + "  " + dr["Middle Name"].ToString().Trim() + " " + dr["Last Name"].ToString().Trim();
            EmployeeMachineCodeD = dr["Employee Machine Code"].ToString().Trim();
        }
        dr.Close();
        navconn.DisConnect();
    }

   

    private void ShowEMployeeDetails()
    {

        DataTable dt = new DataTable();
        string id = string.Empty;
        string fname = string.Empty;
        string sname = string.Empty;
        string lname = string.Empty;

        string newName = string.Empty;
        string ccnameUN = Session["Company"].ToString();
        string rccname = ccnameUN.Replace(".", "_");
        string tbl_EmployeeActualpunchData = "[" + rccname + "$Employee" + "]";


        string sqlStatement = "SELECT * FROM " + tbl_EmployeeActualpunchData + "  where [Global Dimension 1 Code]='" + drpColl.SelectedValue + "' and [Global Dimension 1 Code]!='' and [Status]='0' order by [First Name] asc";


        SqlCommand sqlCmd = new SqlCommand(sqlStatement, navconn.Con);
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);

        sqlDa.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                id = dt.Rows[i]["No_"].ToString();

                fname = dt.Rows[i]["First Name"].ToString();
                sname = dt.Rows[i]["Middle Name"].ToString();
                lname = dt.Rows[i]["Last Name"].ToString();
                newName = id + "  " + fname + "    " + lname + " ";

                txtUserid.Items.Add(new ListItem(newName, id));

                txtUserid.SelectedValue = id;
            }
        }
      
            else
        {
            txtUserid.Items.Clear();
        }

    }


    private void ShowEMployeeDetailsDepartment()
    {

        DataTable dt = new DataTable();
        string id = string.Empty;
        string fname = string.Empty;
        string sname = string.Empty;
        string lname = string.Empty;

        string newName = string.Empty;
        string ccnameUN = Session["Company"].ToString();
        string rccname = ccnameUN.Replace(".", "_");
        string tbl_EmployeeActualpunchData = "[" + rccname + "$Employee" + "]";


        string sqlStatement = "SELECT * FROM " + tbl_EmployeeActualpunchData + "  where [Global Dimension 2 Code]='" + drpDepartment.SelectedValue + "' and [Global Dimension 2 Code]!='' and [Status]='0' order by [First Name] asc";


        SqlCommand sqlCmd = new SqlCommand(sqlStatement, navconn.Con);
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);

        sqlDa.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                id = dt.Rows[i]["No_"].ToString();

                fname = dt.Rows[i]["First Name"].ToString();
                sname = dt.Rows[i]["Middle Name"].ToString();
                lname = dt.Rows[i]["Last Name"].ToString();
                newName = id + "  " + fname + "    " + lname + " ";

                txtUserid.Items.Add(new ListItem(newName, id));

                txtUserid.SelectedValue = id;
            }
        }
        else
        {
            txtUserid.Items.Clear();
        }

    }
    public void bindCollege(int Status)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetCollege", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Status", Status);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            drpColl.DataSource = dt;
            drpColl.DataTextField = "Name";
            drpColl.DataValueField = "Code";
            drpColl.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void bindDepartment(int Status)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetCollege", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Status", Status);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            drpDepartment.DataSource = dt;
            drpDepartment.DataTextField = "Name";
            drpDepartment.DataValueField = "Code";
            drpDepartment.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void BindYear()
    {

        int Currentyear = System.DateTime.Now.Year;

        for (int i = Currentyear - 1; i <= Currentyear; i++)
            ddlYear.Items.Add(i.ToString());
    }

    public void show9punchdata()
    {
        SqlDataReader dr = con.Show_9Punchdata(txtUserid.SelectedValue.ToString());
        DataTable dt = new DataTable();
        dt.Load(dr);
        grdPunchdata.DataSource = dt;
        grdPunchdata.DataBind();
        dr.Close();
        con.DisConnect();

    }

    public void ShowPunchdata()
    {
        string ccnameUN = Session["Company"].ToString();
        string rccname = ccnameUN.Replace(".", "_");
        string tbl_EmployeepunchData = "[" + rccname + "$Employee Device Punches" + "]";


        string Intime = ""; string outtime = ""; string atted_date = "";
        con.delete_EmployeePunchData(txtUserid.SelectedValue.ToString());
        con.DisConnect();
        int nonofdays = Convert.ToInt32(System.DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedItem.Text), Convert.ToInt32(ddlMonth.SelectedValue.ToString())));
        for (int i = 1; i <= nonofdays; i++)
        {
            string P1 = ""; string P2 = ""; string P3 = ""; string P4 = ""; string P5 = ""; string P6 = ""; string P7 = ""; string P8 = ""; string P9 = ""; string autono = "";
            atted_date = ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue.ToString() + "-" + i.ToString();
            // atted_date = ddlMonth.SelectedValue.ToString() + "-" + i.ToString() + "-" + ddlYear.SelectedValue;
            con.Insert_EmployeePunchData(fullname.Trim(), txtUserid.SelectedValue.Trim(), Convert.ToDateTime(atted_date).ToString("yyyy-MM-dd"), Session["Company"].ToString());
            con.DisConnect();
            SqlDataReader dr = navconn.Show_9PunchdataInTime(tbl_EmployeepunchData, EmployeeMachineCodeD, atted_date);
            dr.Read();
            if (dr.HasRows)
            {

                Intime = dr["InTime"].ToString();

                dr.Close();
                navconn.DisConnect();

            }
            else
            {
                dr.Close();
                navconn.DisConnect();
            }
            SqlDataReader drmax = navconn.Show_9PunchdataOutTime(tbl_EmployeepunchData, EmployeeMachineCodeD, atted_date);
            drmax.Read();
            if (drmax.HasRows)
            {
                outtime = drmax["OutTime"].ToString();

                drmax.Close();
                navconn.DisConnect();

            }
            else
            {
                drmax.Close();
                navconn.DisConnect();
            }

            string a = "select top(9)(FORMAT(CAST([Punch Time] AS DATETIME),'HH:mm')) as punchTime from  " + tbl_EmployeepunchData + " where [Employee Machine Code]='" + EmployeeMachineCodeD + "' and convert(date,[Punch Date],131)='" + atted_date + "' order by (FORMAT(CAST([Punch Time] AS DATETIME),'HH:mm'))";
            SqlDataAdapter da = new SqlDataAdapter("select top(9)(FORMAT(CAST([Punch Time] AS DATETIME),'HH:mm')) as punchTime from  " + tbl_EmployeepunchData + " where [Employee Machine Code]='" + EmployeeMachineCodeD + "' and convert(date,[Punch Date],131)='" + atted_date + "' order by (FORMAT(CAST([Punch Time] AS DATETIME),'HH:mm'))", navconn.Con);
            DataSet ds = new DataSet();
            da.Fill(ds, "tbl_EmployeepunchData");
            for (int j = 0; j <= ds.Tables[0].Rows.Count - 1; j++)
            {
                string timedd = "";

                timedd = ds.Tables[0].Rows[j]["punchTime"].ToString();
                if (j == 0)
                {
                    P1 = timedd;
                }

                if (j == 1)
                {
                    P2 = timedd;
                }

                if (j == 2)
                {
                    P3 = timedd;
                }

                if (j == 3)
                {
                    P4 = timedd;
                }
                if (j == 4)
                {
                    P5 = timedd;
                }
                if (j == 5)
                {
                    P6 = timedd;
                }
                if (j == 6)
                {
                    P7 = timedd;
                }
                if (j == 7)
                {
                    P8 = timedd;
                }
                if (j == 8)
                {
                    P9 = timedd;
                }

                navconn.DisConnect();
                con.update_EmployeePunchDataINTime(txtUserid.SelectedValue.ToString(), atted_date, Intime, outtime, P1, P2, P3, P4, P5, P6, P7, P8, P9);
                con.DisConnect();
                navconn.Connect();
            }
            navconn.DisConnect();



        }
        show9punchdata();

    }
    protected void btnGet_Click(object sender, EventArgs e)
    {
        ShowBasicinfo();
        ShowPunchdata();
    }
    protected void grdPunchdata_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            e.Row.Cells[0].BackColor = System.Drawing.Color.LightGoldenrodYellow;
            e.Row.Cells[0].ForeColor = System.Drawing.Color.Black;

            e.Row.Cells[1].BackColor = System.Drawing.Color.Wheat;
            e.Row.Cells[1].ForeColor = System.Drawing.Color.Black;
            e.Row.Cells[2].BackColor = System.Drawing.Color.Wheat;
            e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;


            string p1 = e.Row.Cells[3].Text;
            if (p1 == "" || p1 == "&nbsp;")
            {
                e.Row.Cells[3].BackColor = System.Drawing.Color.Pink;
                e.Row.Cells[3].ForeColor = System.Drawing.Color.White;
            }
            else
            {
                e.Row.Cells[3].BackColor = System.Drawing.Color.Silver;
                e.Row.Cells[3].ForeColor = System.Drawing.Color.White;
            }

            string p2 = e.Row.Cells[4].Text;
            if (p2 == "" || p2 == "&nbsp;")
            {
                e.Row.Cells[4].BackColor = System.Drawing.Color.Pink;
                e.Row.Cells[4].ForeColor = System.Drawing.Color.White;
            }
            else
            {
                e.Row.Cells[4].BackColor = System.Drawing.Color.Silver;
                e.Row.Cells[4].ForeColor = System.Drawing.Color.White;
            }
            string p3 = e.Row.Cells[5].Text;
            if (p3 == "" || p3 == "&nbsp;")
            {
                e.Row.Cells[5].BackColor = System.Drawing.Color.Pink;
                e.Row.Cells[5].ForeColor = System.Drawing.Color.White;
            }

            else
            {
                e.Row.Cells[5].BackColor = System.Drawing.Color.Silver;
                e.Row.Cells[5].ForeColor = System.Drawing.Color.White;
            }
            string p4 = e.Row.Cells[6].Text;
            if (p4 == "" || p4 == "&nbsp;")
            {
                e.Row.Cells[6].BackColor = System.Drawing.Color.Pink;
                e.Row.Cells[6].ForeColor = System.Drawing.Color.White;
            }

            else
            {
                e.Row.Cells[6].BackColor = System.Drawing.Color.Silver;
                e.Row.Cells[6].ForeColor = System.Drawing.Color.White;
            }
            string p5 = e.Row.Cells[7].Text;
            if (p5 == "" || p5 == "&nbsp;")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.Pink;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.White;
            }
            else
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.Silver;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.White;
            }
            string p6 = e.Row.Cells[8].Text;
            if (p6 == "" || p6 == "&nbsp;")
            {
                e.Row.Cells[8].BackColor = System.Drawing.Color.Pink;
                e.Row.Cells[8].ForeColor = System.Drawing.Color.White;
            }
            else
            {
                e.Row.Cells[8].BackColor = System.Drawing.Color.Silver;
                e.Row.Cells[8].ForeColor = System.Drawing.Color.White;
            }
            string p7 = e.Row.Cells[9].Text;
            if (p7 == "" || p7 == "&nbsp;")
            {
                e.Row.Cells[9].BackColor = System.Drawing.Color.Pink;
                e.Row.Cells[9].ForeColor = System.Drawing.Color.White;
            }
            else
            {
                e.Row.Cells[9].BackColor = System.Drawing.Color.Silver;
                e.Row.Cells[9].ForeColor = System.Drawing.Color.White;
            }
            string p8 = e.Row.Cells[10].Text;
            if (p8 == "" || p8 == "&nbsp;")
            {
                e.Row.Cells[10].BackColor = System.Drawing.Color.Pink;
                e.Row.Cells[10].ForeColor = System.Drawing.Color.White;
            }
            else
            {
                e.Row.Cells[10].BackColor = System.Drawing.Color.Silver;
                e.Row.Cells[10].ForeColor = System.Drawing.Color.White;
            }
            string p9 = e.Row.Cells[11].Text;
            if (p9 == "" || p9 == "&nbsp;")
            {
                e.Row.Cells[11].BackColor = System.Drawing.Color.Pink;
                e.Row.Cells[11].ForeColor = System.Drawing.Color.White;
            }
            else
            {
                e.Row.Cells[11].BackColor = System.Drawing.Color.Silver;
                e.Row.Cells[11].ForeColor = System.Drawing.Color.White;
            }
            //e.Row.BackColor = System.Drawing.Color.Red;
            //e.Row.ForeColor = System.Drawing.Color.White;
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grdPunchdata.RenderControl(htmlWrite);

        Response.Clear();
        string str = "Punchdata" + txtUserid.SelectedValue.ToString(); 
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();

    }
    protected void drpColl_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowEMployeeDetails();
    }
    protected void chkDepart_CheckedChanged(object sender, EventArgs e)
    {
        td2.Visible = true;
        tdId.Visible = false;
        ChkCollege.Checked = false;
        bindDepartment(2);
        ShowEMployeeDetailsDepartment();
    }
    protected void ChkCollege_CheckedChanged(object sender, EventArgs e)
    {
        td2.Visible = false;
        tdId.Visible = true;
        chkDepart.Checked = false;
        bindCollege(1);
        ShowEMployeeDetails();

    }
    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowEMployeeDetailsDepartment(); 
    }
}