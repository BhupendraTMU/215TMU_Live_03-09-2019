using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
public partial class Team_Punch_Data : System.Web.UI.Page
{
    ServicePoratal con;
    Connection navconn;

    SqlConnection Conn = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {


            con = new ServicePoratal();
            navconn = new Connection();
            if (!IsPostBack)
            {
                txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                showHRHODisexhist();
                BindYear();
                ddlMonth.SelectedValue = System.DateTime.Now.ToString("MM");
                ddlYear.SelectedValue = System.DateTime.Now.ToString("yyyy");

                ShowEMployeeDetails();
                ShowBasicinfo();
                if (Session["uid"].ToString() == "TMU06860")
                {
                    txtUserid.Visible = false;
                    ddlMonth.Visible = false;
                    ddlYear.Visible = false;
                    txtdate.Visible = true;
                    m.Visible = false;
                    Y.Visible = false;
                    empID.Visible = false;

                }
                else
                {
                    txtUserid.Visible = true;
                    ddlMonth.Visible = true;
                    ddlYear.Visible = true;
                    m.Visible = true;
                    Y.Visible = true;
                    empID.Visible = true;
                    txtdate.Visible = false;
                    ShowPunchdata();
                }

            }
        }
        catch (Exception)
        {
            Response.Redirect("../Default.aspx");
        }
    }


    public void showHRHODisexhist()
    {

        string s = "";
        if (Session["uid"].ToString() == "TMU06860")
        {
            s = "select No_,Case when [Sanctioning Incharge]='' then HOD else [Sanctioning Incharge] end as 'AuthorisedID' from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where [Global Dimension 1 Code]='TMDC' and Status=0 ";
        }
        else
        {


            s = "Select * from (select No_,Case when [Sanctioning Incharge]='' then HOD else [Sanctioning Incharge] end as 'AuthorisedID' from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] ) T where T.AuthorisedID='" + Session["uid"].ToString() + "'";
        }
        SqlCommand cmd = new SqlCommand(s, Conn);
        if (Conn != null && Conn.State == ConnectionState.Closed)
        {
            Conn.Open();
        }
        SqlDataReader dr = cmd.ExecuteReader();

        dr.Read();
        if (dr.HasRows)
        {
            dr.Close();
            con.DisConnect();


        }
        else
        {
            dr.Close();
            con.DisConnect();
            Response.Redirect("Default.aspx");
        }
        Conn.Close();
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
        string sqlStatement = "";
        if (Session["uid"].ToString() == "TMU06860")
        {
            sqlStatement = "SELECT *,Case When [Sanctioning Incharge]='' then HOD else [Sanctioning Incharge] end as 'AuthorizedID'  FROM [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee]  where [Status]='0' and [Global Dimension 1 Code]='TMDC' order by [First Name]";
        }
        else if (Session["uid"].ToString() == "TMU07320")
        {
            sqlStatement = "SELECT *,Case When [Sanctioning Incharge]='' then HOD else [Sanctioning Incharge] end as 'AuthorizedID'  FROM [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee]  where [Status]='0'  order by [First Name]";
        }
        
        else
        {
            sqlStatement = "Select * from (SELECT *,Case When [Sanctioning Incharge]='' then HOD else [Sanctioning Incharge] end as 'AuthorizedID'  FROM [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee]  where [Status]='0') T where T.AuthorizedID='" + Session["uid"].ToString() + "' order by [First Name]";
        }
        SqlCommand sqlCmd = new SqlCommand(sqlStatement, con.Con);
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

    }

    public void BindYear()
    {

        int Currentyear = System.DateTime.Now.Year;

        for (int i = Currentyear - 1; i <= Currentyear; i++)
            ddlYear.Items.Add(i.ToString());
    }

    public void show9punchdata()
    {


        if (Session["uid"].ToString() == "TMU06860")
        {



            SqlCommand sqlCmd = new SqlCommand("select * from tble_Employee_Punch_Data where [Employee ID]  collate Latin1_General_100_CS_AS in (SELECT No_  FROM [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee]  where [Status]='0' and [Global Dimension 1 Code]='TMDC' ) and [Attendance Date]=convert(date,'" + txtdate.Text + "',103) order by [Attendance Date]", con.Con);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataTable dt1 = new DataTable();
            sqlDa.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                empID.Visible = false;
                txtUserid.Visible = false;
                iddate.Visible = true;
                txtdate.Visible = true;
                grdPunchdata.DataSource = dt1;
                grdPunchdata.DataBind();
            }
        }
        else
        {

            empID.Visible = true;
            txtUserid.Visible = true;
            iddate.Visible = false;
            txtdate.Visible = false;
            SqlDataReader dr = con.Show_9Punchdata(txtUserid.SelectedValue.ToString());
            DataTable dt = new DataTable();
            dt.Load(dr);
            grdPunchdata.Columns[0].Visible = false;
            grdPunchdata.Columns[1].Visible = false;
            grdPunchdata.DataSource = dt;
            grdPunchdata.DataBind();
            dr.Close();
            con.DisConnect();
        }


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
        if (Session["uid"].ToString() == "TMU06860")
        {
            SqlCommand sqlCmd = new SqlCommand("SELECT [First Name]  'Employee Name'	,No_ 'Employee ID' ,'" + Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd") + "' as 	'Attendance Date',	'' 'In Time',''	'Out Time', ''	P1,''	P2,''	P3,'','' P4,''	P5,''	P6,''	P7,	''	P8,''	P9,'TMU'	Company  FROM [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee]  where [Status]='0' and [Global Dimension 1 Code]='TMDC'", con.Con);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataTable dt1 = new DataTable();
            sqlDa.Fill(dt1);

            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    string EmployeeCode = dt1.Rows[i]["Employee ID"].ToString();
                    string EmployeeName = dt1.Rows[i]["Employee Name"].ToString();
                    string ccnameUN = Session["Company"].ToString();
                    string rccname = ccnameUN.Replace(".", "_");
                    string tbl_EmployeepunchData = "[" + rccname + "$Employee Device Punches" + "]";


                    string Intime = ""; string outtime = "";
                    con.delete_EmployeePunchData(EmployeeCode);
                    con.DisConnect();
                    string newName = string.Empty;

                    SqlDataReader dr = navconn.SHow_EmployeeBasicdetails(EmployeeCode, "[TMU$Employee]");
                    dr.Read();
                    if (dr.HasRows)
                    {

                        EmployeeMachineCodeD = dr["Employee Machine Code"].ToString().Trim();
                    }
                    dr.Close();
                    navconn.DisConnect();
                    string P1 = ""; string P2 = ""; string P3 = ""; string P4 = ""; string P5 = ""; string P6 = ""; string P7 = ""; string P8 = ""; string P9 = ""; string autono = "";

                    con.Insert_EmployeePunchData(EmployeeName.Trim(), EmployeeCode, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"), Session["Company"].ToString());
                    con.DisConnect();
                    SqlDataReader dr1 = navconn.Show_9PunchdataInTime(tbl_EmployeepunchData, EmployeeMachineCodeD, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
                    dr1.Read();
                    if (dr1.HasRows)
                    {

                        Intime = dr1["InTime"].ToString();

                        dr1.Close();
                        navconn.DisConnect();

                    }
                    else
                    {
                        dr1.Close();
                        navconn.DisConnect();
                    }


                    SqlDataReader drmax = navconn.Show_9PunchdataOutTime(tbl_EmployeepunchData, EmployeeMachineCodeD, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
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

                    string a = "select top(9)(FORMAT(CAST([Punch Time] AS DATETIME),'HH:mm')) as punchTime from  " + tbl_EmployeepunchData + " where [Employee Machine Code]='" + EmployeeMachineCodeD + "' and convert(date,[Punch Date],131)='" + Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd") + "' order by (FORMAT(CAST([Punch Time] AS DATETIME),'HH:mm'))";
                    SqlDataAdapter da = new SqlDataAdapter("select top(9)(FORMAT(CAST([Punch Time] AS DATETIME),'HH:mm')) as punchTime from  " + tbl_EmployeepunchData + " where [Employee Machine Code]='" + EmployeeMachineCodeD + "' and convert(date,[Punch Date],131)='" + Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd") + "' order by (FORMAT(CAST([Punch Time] AS DATETIME),'HH:mm'))", navconn.Con);
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
                        con.update_EmployeePunchDataINTime(EmployeeCode, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"), Intime, outtime, P1, P2, P3, P4, P5, P6, P7, P8, P9);
                        con.DisConnect();
                        navconn.Connect();
                    }
                    navconn.DisConnect();


                }

                SqlCommand sqlCmdS = new SqlCommand("select * from tble_Employee_Punch_Data where [Employee ID]  collate Latin1_General_100_CS_AS in (SELECT No_  FROM [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee]  where [Status]='0' and [Global Dimension 1 Code]='TMDC' ) and [Attendance Date]=convert(date,'" + txtdate.Text + "',103) order by [Attendance Date]", con.Con);
                SqlDataAdapter sqlDaS = new SqlDataAdapter(sqlCmdS);
                DataTable dtS = new DataTable();
                sqlDaS.Fill(dtS);
                if (dtS.Rows.Count > 0)
                {
                    empID.Visible = false;
                    txtUserid.Visible = false;
                    iddate.Visible = true;
                    txtdate.Visible = true;
                    grdPunchdata.DataSource = dtS;
                    grdPunchdata.DataBind();
                }

            }



        }
        else
        {
            ShowBasicinfo();
            ShowPunchdata();
        }


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
            e.Row.Cells[3].BackColor = System.Drawing.Color.Wheat;
            e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
            e.Row.Cells[4].BackColor = System.Drawing.Color.Wheat;
            e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
            string p1 = e.Row.Cells[5].Text;
            if (p1 == "" || p1 == "&nbsp;")
            {
                e.Row.Cells[5].BackColor = System.Drawing.Color.Pink;
                e.Row.Cells[5].ForeColor = System.Drawing.Color.White;
            }
            else
            {
                e.Row.Cells[5].BackColor = System.Drawing.Color.Silver;
                e.Row.Cells[5].ForeColor = System.Drawing.Color.White;
            }

            string p2 = e.Row.Cells[6].Text;
            if (p2 == "" || p2 == "&nbsp;")
            {
                e.Row.Cells[6].BackColor = System.Drawing.Color.Pink;
                e.Row.Cells[6].ForeColor = System.Drawing.Color.White;
            }
            else
            {
                e.Row.Cells[6].BackColor = System.Drawing.Color.Silver;
                e.Row.Cells[6].ForeColor = System.Drawing.Color.White;
            }
            string p3 = e.Row.Cells[7].Text;
            if (p3 == "" || p3 == "&nbsp;")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.Pink;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.White;
            }

            else
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.Silver;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.White;
            }
            string p4 = e.Row.Cells[8].Text;
            if (p4 == "" || p4 == "&nbsp;")
            {
                e.Row.Cells[8].BackColor = System.Drawing.Color.Pink;
                e.Row.Cells[8].ForeColor = System.Drawing.Color.White;
            }

            else
            {
                e.Row.Cells[8].BackColor = System.Drawing.Color.Silver;
                e.Row.Cells[8].ForeColor = System.Drawing.Color.White;
            }
            string p5 = e.Row.Cells[9].Text;
            if (p5 == "" || p5 == "&nbsp;")
            {
                e.Row.Cells[9].BackColor = System.Drawing.Color.Pink;
                e.Row.Cells[9].ForeColor = System.Drawing.Color.White;
            }
            else
            {
                e.Row.Cells[9].BackColor = System.Drawing.Color.Silver;
                e.Row.Cells[9].ForeColor = System.Drawing.Color.White;
            }
            string p6 = e.Row.Cells[10].Text;
            if (p6 == "" || p6 == "&nbsp;")
            {
                e.Row.Cells[10].BackColor = System.Drawing.Color.Pink;
                e.Row.Cells[10].ForeColor = System.Drawing.Color.White;
            }
            else
            {
                e.Row.Cells[10].BackColor = System.Drawing.Color.Silver;
                e.Row.Cells[10].ForeColor = System.Drawing.Color.White;
            }
            string p7 = e.Row.Cells[11].Text;
            if (p7 == "" || p7 == "&nbsp;")
            {
                e.Row.Cells[11].BackColor = System.Drawing.Color.Pink;
                e.Row.Cells[11].ForeColor = System.Drawing.Color.White;
            }
            else
            {
                e.Row.Cells[11].BackColor = System.Drawing.Color.Silver;
                e.Row.Cells[11].ForeColor = System.Drawing.Color.White;
            }
            string p8 = e.Row.Cells[12].Text;
            if (p8 == "" || p8 == "&nbsp;")
            {
                e.Row.Cells[12].BackColor = System.Drawing.Color.Pink;
                e.Row.Cells[12].ForeColor = System.Drawing.Color.White;
            }
            else
            {
                e.Row.Cells[12].BackColor = System.Drawing.Color.Silver;
                e.Row.Cells[12].ForeColor = System.Drawing.Color.White;
            }
            string p9 = e.Row.Cells[13].Text;
            if (p9 == "" || p9 == "&nbsp;")
            {
                e.Row.Cells[13].BackColor = System.Drawing.Color.Pink;
                e.Row.Cells[13].ForeColor = System.Drawing.Color.White;
            }
            else
            {
                e.Row.Cells[13].BackColor = System.Drawing.Color.Silver;
                e.Row.Cells[13].ForeColor = System.Drawing.Color.White;
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
        string str = "Punchdata" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();

    }
}