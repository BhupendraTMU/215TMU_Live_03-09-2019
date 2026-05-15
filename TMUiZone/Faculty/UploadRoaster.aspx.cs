using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
public partial class Faculty_UploadRoaster : System.Web.UI.Page
{
    Connection con;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                con = new Connection();
                BindYear();
                ddlMonth.SelectedValue = System.DateTime.Now.ToString("MM");
                ddlYear.SelectedValue = System.DateTime.Now.ToString("yyyy");
                ShowEMployeeDetails();
            }
            catch (Exception ex)
            {
            }
        }
    }
    protected void grddata_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DropDownList drpShift1 = (DropDownList)e.Row.FindControl("drpShift1");
            DropDownList drpShift2 = (DropDownList)e.Row.FindControl("drpShift2");
            DropDownList drpShift3 = (DropDownList)e.Row.FindControl("drpShift3");
            DropDownList drpShift4 = (DropDownList)e.Row.FindControl("drpShift4");
            DropDownList drpShift5 = (DropDownList)e.Row.FindControl("drpShift5");
            DropDownList drpShift6 = (DropDownList)e.Row.FindControl("drpShift6");
            DropDownList drpShift7 = (DropDownList)e.Row.FindControl("drpShift7");
            DropDownList drpShift8 = (DropDownList)e.Row.FindControl("drpShift8");
            DropDownList drpShift9 = (DropDownList)e.Row.FindControl("drpShift9");
            DropDownList drpShift10 = (DropDownList)e.Row.FindControl("drpShift10");
            DropDownList drpShift11 = (DropDownList)e.Row.FindControl("drpShift11");
            DropDownList drpShift12 = (DropDownList)e.Row.FindControl("drpShift12");
            DropDownList drpShift13 = (DropDownList)e.Row.FindControl("drpShift13");
            DropDownList drpShift14 = (DropDownList)e.Row.FindControl("drpShift14");
            DropDownList drpShift15 = (DropDownList)e.Row.FindControl("drpShift15");
            DropDownList drpShift16 = (DropDownList)e.Row.FindControl("drpShift16");
            DropDownList drpShift17 = (DropDownList)e.Row.FindControl("drpShift17");
            DropDownList drpShift18 = (DropDownList)e.Row.FindControl("drpShift18");
            DropDownList drpShift19 = (DropDownList)e.Row.FindControl("drpShift19");

            DropDownList drpShift20 = (DropDownList)e.Row.FindControl("drpShift20");
            DropDownList drpShift21 = (DropDownList)e.Row.FindControl("drpShift21");
            DropDownList drpShift22 = (DropDownList)e.Row.FindControl("drpShift22");
            DropDownList drpShift23 = (DropDownList)e.Row.FindControl("drpShift23");
            DropDownList drpShift24 = (DropDownList)e.Row.FindControl("drpShift24");
            DropDownList drpShift25 = (DropDownList)e.Row.FindControl("drpShift25");
            DropDownList drpShift26 = (DropDownList)e.Row.FindControl("drpShift26");
            DropDownList drpShift27 = (DropDownList)e.Row.FindControl("drpShift27");
            DropDownList drpShift28 = (DropDownList)e.Row.FindControl("drpShift28");

            DropDownList drpShift29 = (DropDownList)e.Row.FindControl("drpShift29");
            DropDownList drpShift30 = (DropDownList)e.Row.FindControl("drpShift30");
            DropDownList drpShift31 = (DropDownList)e.Row.FindControl("drpShift31");
            if (ddlMonth.SelectedValue == "02")
            {
                drpShift29.Enabled = false;
                drpShift31.Enabled = false;
                drpShift30.Enabled = false;
            }
            if (ddlMonth.SelectedValue == "04" || ddlMonth.SelectedValue == "06" || ddlMonth.SelectedValue == "09" || ddlMonth.SelectedValue == "11" )
            {
                drpShift31.Enabled = false;
               
            }

            SqlCommand cmd = new SqlCommand("[GetEmployeeShift]", con1);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con1.Open();
            da.Fill(dt);
            con1.Close();

            SqlCommand cmdSelect = new SqlCommand("[GetEmployeeShiftfromRoaster]", con1);
            cmdSelect.Parameters.AddWithValue("@UserId", txtUserid.SelectedValue);
            cmdSelect.Parameters.AddWithValue("@Month", ddlMonth.SelectedValue);
            cmdSelect.Parameters.AddWithValue("@Year", ddlYear.SelectedValue);
            cmdSelect.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter daSelect = new SqlDataAdapter(cmdSelect);
            DataTable dtSelect = new DataTable();
            con1.Open();
            daSelect.Fill(dtSelect);
            con1.Close();

            drpShift1.DataSource = dt;
            drpShift1.DataValueField = "Shift Code";
            drpShift1.DataTextField = "Shift Description";
            drpShift1.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-1"].ToString() != "0")
                {
                    drpShift1.SelectedValue = dtSelect.Rows[0]["Shift Code-1"].ToString();
                }
            }
            drpShift2.DataSource = dt;
            drpShift2.DataValueField = "Shift Code";
            drpShift2.DataTextField = "Shift Description";
            drpShift2.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-2"].ToString() != "0")
                {
                    drpShift2.SelectedValue = dtSelect.Rows[0]["Shift Code-2"].ToString();
                }
            }
            drpShift3.DataSource = dt;
            drpShift3.DataValueField = "Shift Code";
            drpShift3.DataTextField = "Shift Description";
            drpShift3.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-3"].ToString() != "0")
                {
                    drpShift3.SelectedValue = dtSelect.Rows[0]["Shift Code-3"].ToString();
                }
            }
            drpShift4.DataSource = dt;
            drpShift4.DataValueField = "Shift Code";
            drpShift4.DataTextField = "Shift Description";
            drpShift4.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-4"].ToString() != "0")
                {
                    drpShift4.SelectedValue = dtSelect.Rows[0]["Shift Code-4"].ToString();
                }
            }
            drpShift5.DataSource = dt;
            drpShift5.DataValueField = "Shift Code";
            drpShift5.DataTextField = "Shift Description";
            drpShift5.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-5"].ToString() != "0")
                {
                    drpShift5.SelectedValue = dtSelect.Rows[0]["Shift Code-5"].ToString();
                }
            }
            drpShift6.DataSource = dt;
            drpShift6.DataValueField = "Shift Code";
            drpShift6.DataTextField = "Shift Description";
            drpShift6.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-6"].ToString() != "0")
                {
                    drpShift6.SelectedValue = dtSelect.Rows[0]["Shift Code-6"].ToString();
                }
            }
            drpShift7.DataSource = dt;
            drpShift7.DataValueField = "Shift Code";
            drpShift7.DataTextField = "Shift Description";
            drpShift7.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-7"].ToString() != "0")
                {
                    drpShift7.SelectedValue = dtSelect.Rows[0]["Shift Code-7"].ToString();
                }
            }
            drpShift8.DataSource = dt;
            drpShift8.DataValueField = "Shift Code";
            drpShift8.DataTextField = "Shift Description";
            drpShift8.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-8"].ToString() != "0")
                {
                    drpShift8.SelectedValue = dtSelect.Rows[0]["Shift Code-8"].ToString();
                }
            }
            drpShift9.DataSource = dt;
            drpShift9.DataValueField = "Shift Code";
            drpShift9.DataTextField = "Shift Description";
            drpShift9.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-9"].ToString() != "0")
                {
                    drpShift9.SelectedValue = dtSelect.Rows[0]["Shift Code-9"].ToString();
                }
            }
            drpShift10.DataSource = dt;
            drpShift10.DataValueField = "Shift Code";
            drpShift10.DataTextField = "Shift Description";
            drpShift10.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-10"].ToString() != "0")
                {
                    drpShift10.SelectedValue = dtSelect.Rows[0]["Shift Code-10"].ToString();
                }
            }
            drpShift11.DataSource = dt;
            drpShift11.DataValueField = "Shift Code";
            drpShift11.DataTextField = "Shift Description";
            drpShift11.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-11"].ToString() != "0")
                {
                    drpShift11.SelectedValue = dtSelect.Rows[0]["Shift Code-11"].ToString();
                }
            }
            drpShift12.DataSource = dt;
            drpShift12.DataValueField = "Shift Code";
            drpShift12.DataTextField = "Shift Description";
            drpShift12.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-12"].ToString() != "0")
                {
                    drpShift12.SelectedValue = dtSelect.Rows[0]["Shift Code-12"].ToString();
                }
            }
            drpShift13.DataSource = dt;
            drpShift13.DataValueField = "Shift Code";
            drpShift13.DataTextField = "Shift Description";
            drpShift13.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-13"].ToString() != "0")
                {
                    drpShift13.SelectedValue = dtSelect.Rows[0]["Shift Code-13"].ToString();
                }
            }
            drpShift14.DataSource = dt;
            drpShift14.DataValueField = "Shift Code";
            drpShift14.DataTextField = "Shift Description";
            drpShift14.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-14"].ToString() != "0")
                {
                    drpShift14.SelectedValue = dtSelect.Rows[0]["Shift Code-14"].ToString();
                }
            }
            drpShift15.DataSource = dt;
            drpShift15.DataValueField = "Shift Code";
            drpShift15.DataTextField = "Shift Description";
            drpShift15.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-15"].ToString() != "0")
                {
                    drpShift15.SelectedValue = dtSelect.Rows[0]["Shift Code-15"].ToString();
                }
            }
            drpShift16.DataSource = dt;
            drpShift16.DataValueField = "Shift Code";
            drpShift16.DataTextField = "Shift Description";
            drpShift16.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-16"].ToString() != "0")
                {
                    drpShift16.SelectedValue = dtSelect.Rows[0]["Shift Code-16"].ToString();
                }
            }
            drpShift17.DataSource = dt;
            drpShift17.DataValueField = "Shift Code";
            drpShift17.DataTextField = "Shift Description";
            drpShift17.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-17"].ToString() != "0")
                {
                    drpShift17.SelectedValue = dtSelect.Rows[0]["Shift Code-17"].ToString();
                }
            }
            drpShift18.DataSource = dt;
            drpShift18.DataValueField = "Shift Code";
            drpShift18.DataTextField = "Shift Description";
            drpShift18.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-18"].ToString() != "0")
                {
                    drpShift18.SelectedValue = dtSelect.Rows[0]["Shift Code-18"].ToString();
                }
            }
            drpShift19.DataSource = dt;
            drpShift19.DataValueField = "Shift Code";
            drpShift19.DataTextField = "Shift Description";
            drpShift19.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-19"].ToString() != "0")
                {
                    drpShift19.SelectedValue = dtSelect.Rows[0]["Shift Code-19"].ToString();
                }
            }
            drpShift20.DataSource = dt;
            drpShift20.DataValueField = "Shift Code";
            drpShift20.DataTextField = "Shift Description";
            drpShift20.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-20"].ToString() != "0")
                {
                    drpShift20.SelectedValue = dtSelect.Rows[0]["Shift Code-20"].ToString();
                }
            }
            drpShift21.DataSource = dt;
            drpShift21.DataValueField = "Shift Code";
            drpShift21.DataTextField = "Shift Description";
            drpShift21.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-21"].ToString() != "0")
                {
                    drpShift21.SelectedValue = dtSelect.Rows[0]["Shift Code-21"].ToString();
                }
            }
            drpShift22.DataSource = dt;
            drpShift22.DataValueField = "Shift Code";
            drpShift22.DataTextField = "Shift Description";
            drpShift22.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-22"].ToString() != "0")
                {
                    drpShift22.SelectedValue = dtSelect.Rows[0]["Shift Code-22"].ToString();
                }
            }
            drpShift23.DataSource = dt;
            drpShift23.DataValueField = "Shift Code";
            drpShift23.DataTextField = "Shift Description";
            drpShift23.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-23"].ToString() != "0")
                {
                    drpShift23.SelectedValue = dtSelect.Rows[0]["Shift Code-23"].ToString();
                }
            }
            drpShift24.DataSource = dt;
            drpShift24.DataValueField = "Shift Code";
            drpShift24.DataTextField = "Shift Description";
            drpShift24.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-24"].ToString() != "0")
                {
                    drpShift24.SelectedValue = dtSelect.Rows[0]["Shift Code-24"].ToString();
                }
            }
            drpShift25.DataSource = dt;
            drpShift25.DataValueField = "Shift Code";
            drpShift25.DataTextField = "Shift Description";
            drpShift25.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-25"].ToString() != "0")
                {
                    drpShift25.SelectedValue = dtSelect.Rows[0]["Shift Code-25"].ToString();
                }
            }
            drpShift26.DataSource = dt;
            drpShift26.DataValueField = "Shift Code";
            drpShift26.DataTextField = "Shift Description";
            drpShift26.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-26"].ToString() != "0")
                {
                    drpShift26.SelectedValue = dtSelect.Rows[0]["Shift Code-26"].ToString();
                }
            }
            drpShift27.DataSource = dt;
            drpShift27.DataValueField = "Shift Code";
            drpShift27.DataTextField = "Shift Description";
            drpShift27.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-27"].ToString() != "0")
                {
                    drpShift27.SelectedValue = dtSelect.Rows[0]["Shift Code-27"].ToString();
                }
            }
            drpShift28.DataSource = dt;
            drpShift28.DataValueField = "Shift Code";
            drpShift28.DataTextField = "Shift Description";
            drpShift28.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-28"].ToString() != "0")
                {
                    drpShift28.SelectedValue = dtSelect.Rows[0]["Shift Code-28"].ToString();
                }
            }
            drpShift29.DataSource = dt;
            drpShift29.DataValueField = "Shift Code";
            drpShift29.DataTextField = "Shift Description";
            drpShift29.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-29"].ToString() != "0")
                {
                    drpShift29.SelectedValue = dtSelect.Rows[0]["Shift Code-29"].ToString();
                }
            }
            drpShift30.DataSource = dt;
            drpShift30.DataValueField = "Shift Code";
            drpShift30.DataTextField = "Shift Description";
            drpShift30.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-30"].ToString() != "0")
                {
                    drpShift30.SelectedValue = dtSelect.Rows[0]["Shift Code-30"].ToString();
                }
            }
            drpShift31.DataSource = dt;
            drpShift31.DataValueField = "Shift Code";
            drpShift31.DataTextField = "Shift Description";
            drpShift31.DataBind();
            if (dtSelect.Rows.Count > 0)
            {
                if (dtSelect.Rows[0]["Shift Code-31"].ToString() != "0")
                {
                    drpShift31.SelectedValue = dtSelect.Rows[0]["Shift Code-31"].ToString();
                }
            }




        }
       
    }
    public void BindData()
    {

        SqlCommand cmd = new SqlCommand("GetEmployeeForRoaster", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        if (con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }
        cmd.Parameters.AddWithValue("@UserId", txtUserid.SelectedValue);
        cmd.Parameters.AddWithValue("@Month", ddlMonth.SelectedValue);
        cmd.Parameters.AddWithValue("@Year", ddlYear.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();


        da.Fill(dt);
        con1.Close();
        if (dt.Rows.Count > 0)
        {
            btnSubmit.Visible = true;
            grddata.DataSource = dt;
            grddata.DataBind();
        }
        else
        {
            btnSubmit.Visible = false;
            grddata.DataSource = "";
            grddata.DataBind();
        }
        

    }
    public void BindYear()
    {

        int Currentyear = System.DateTime.Now.Year;

        for (int i = Currentyear - 1; i <= Currentyear; i++)
            ddlYear.Items.Add(i.ToString());
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


        string sqlStatement = "SELECT * FROM " + tbl_EmployeeActualpunchData + "  where [Status]='0' and [Shift Pattern]=2  and (HOD='" + Session["uid"].ToString() + "' or [HOD 1]='" + Session["uid"].ToString() + "')";
        SqlCommand sqlCmd = new SqlCommand(sqlStatement, con.Con);
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);

        sqlDa.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            tblRoaster.Visible = true;
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
            tblRoaster.Visible = false;
        }

    }
    protected void btnGet_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gv in grddata.Rows)
        {
            string rating = string.Empty;
            Label EmpCode = gv.FindControl("lblEmplo_Code") as Label;
            DropDownList drpShift1 = gv.FindControl("drpShift1") as DropDownList;
            DropDownList drpShift2 = gv.FindControl("drpShift2") as DropDownList;
            DropDownList drpShift3 = gv.FindControl("drpShift3") as DropDownList;
            DropDownList drpShift4 = gv.FindControl("drpShift4") as DropDownList;
            DropDownList drpShift5 = gv.FindControl("drpShift5") as DropDownList;
            DropDownList drpShift6 = gv.FindControl("drpShift6") as DropDownList;
            DropDownList drpShift7 = gv.FindControl("drpShift7") as DropDownList;
            DropDownList drpShift8 = gv.FindControl("drpShift8") as DropDownList;
            DropDownList drpShift9 = gv.FindControl("drpShift9") as DropDownList;
            DropDownList drpShift10 = gv.FindControl("drpShift10") as DropDownList;
            DropDownList drpShift11 = gv.FindControl("drpShift11") as DropDownList;
            DropDownList drpShift12 = gv.FindControl("drpShift12") as DropDownList;
            DropDownList drpShift13 = gv.FindControl("drpShift13") as DropDownList;
            DropDownList drpShift14 = gv.FindControl("drpShift14") as DropDownList;
            DropDownList drpShift15 = gv.FindControl("drpShift15") as DropDownList;
            DropDownList drpShift16 = gv.FindControl("drpShift16") as DropDownList;
            DropDownList drpShift17 = gv.FindControl("drpShift17") as DropDownList;
            DropDownList drpShift18 = gv.FindControl("drpShift18") as DropDownList;
            DropDownList drpShift19 = gv.FindControl("drpShift19") as DropDownList;
            DropDownList drpShift20 = gv.FindControl("drpShift20") as DropDownList;
            DropDownList drpShift21 = gv.FindControl("drpShift21") as DropDownList;
            DropDownList drpShift22 = gv.FindControl("drpShift22") as DropDownList;
            DropDownList drpShift23 = gv.FindControl("drpShift23") as DropDownList;
            DropDownList drpShift24 = gv.FindControl("drpShift24") as DropDownList;
            DropDownList drpShift25 = gv.FindControl("drpShift25") as DropDownList;
            DropDownList drpShift26 = gv.FindControl("drpShift26") as DropDownList;
            DropDownList drpShift27 = gv.FindControl("drpShift27") as DropDownList;
            DropDownList drpShift28 = gv.FindControl("drpShift28") as DropDownList;
            DropDownList drpShift29 = gv.FindControl("drpShift29") as DropDownList;
            DropDownList drpShift30 = gv.FindControl("drpShift30") as DropDownList;
            DropDownList drpShift31 = gv.FindControl("drpShift31") as DropDownList;

            SqlCommand cmd = new SqlCommand("Sp_UpdateRoaster", con1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@EmpCode", txtUserid.SelectedValue);
            cmd.Parameters.AddWithValue("@Month", ddlMonth.SelectedValue);
            cmd.Parameters.AddWithValue("@Year", ddlYear.SelectedValue);
            cmd.Parameters.AddWithValue("@drpShift1", drpShift1.Text);
            cmd.Parameters.AddWithValue("@drpShift2", drpShift2.Text);
            cmd.Parameters.AddWithValue("@drpShift3", drpShift3.Text);
            cmd.Parameters.AddWithValue("@drpShift4", drpShift4.Text);
            cmd.Parameters.AddWithValue("@drpShift5", drpShift5.Text);
            cmd.Parameters.AddWithValue("@drpShift6", drpShift6.Text);
            cmd.Parameters.AddWithValue("@drpShift7", drpShift7.Text);
            cmd.Parameters.AddWithValue("@drpShift8", drpShift8.Text);
            cmd.Parameters.AddWithValue("@drpShift9", drpShift9.Text);
            cmd.Parameters.AddWithValue("@drpShift10", drpShift10.Text);
            cmd.Parameters.AddWithValue("@drpShift11", drpShift11.Text);
            cmd.Parameters.AddWithValue("@drpShift12", drpShift12.Text);
            cmd.Parameters.AddWithValue("@drpShift13", drpShift13.Text);
            cmd.Parameters.AddWithValue("@drpShift14", drpShift14.Text);
            cmd.Parameters.AddWithValue("@drpShift15", drpShift15.Text);
            cmd.Parameters.AddWithValue("@drpShift16", drpShift16.Text);
            cmd.Parameters.AddWithValue("@drpShift17", drpShift17.Text);
            cmd.Parameters.AddWithValue("@drpShift18", drpShift18.Text);
            cmd.Parameters.AddWithValue("@drpShift19", drpShift19.Text);
            cmd.Parameters.AddWithValue("@drpShift20", drpShift20.Text);
            cmd.Parameters.AddWithValue("@drpShift21", drpShift21.Text);
            cmd.Parameters.AddWithValue("@drpShift22", drpShift22.Text);
            cmd.Parameters.AddWithValue("@drpShift23", drpShift23.Text);
            cmd.Parameters.AddWithValue("@drpShift24", drpShift24.Text);
            cmd.Parameters.AddWithValue("@drpShift25", drpShift25.Text);
            cmd.Parameters.AddWithValue("@drpShift26", drpShift26.Text);
            cmd.Parameters.AddWithValue("@drpShift27", drpShift27.Text);
            cmd.Parameters.AddWithValue("@drpShift28", drpShift28.Text);
            cmd.Parameters.AddWithValue("@drpShift29", drpShift29.Text);
            cmd.Parameters.AddWithValue("@drpShift30", drpShift30.Text);
            cmd.Parameters.AddWithValue("@drpShift31", drpShift31.Text);

            if (con1.State == ConnectionState.Open)
            { con1.Close(); }
            con1.Open();
            cmd.ExecuteNonQuery();
            con1.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Roaster Update Successfully');", true);


        }



    }
}