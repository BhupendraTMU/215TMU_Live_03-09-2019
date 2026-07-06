using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Faculty_EquipmentApproval : System.Web.UI.Page
{
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    Connection con;
    string IndentStatus = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["uid"].ToString() != "TMU00308")
            {
                Response.Redirect("Error.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }



            if (Session["uid"] == null)

                Response.Redirect("~/Login.aspx");
                ShowApprovaldata();
        }
    }
    protected void btnClose_Command(object sender, CommandEventArgs e)
    {
        string confirmValue = Request.Form["confirm_value"];
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;

        // Find the TextBox inside that row
        TextBox txtRRemark = (TextBox)row.FindControl("txtRRemark");
        TextBox txtMRemark = (TextBox)row.FindControl("txtMRemark");
        // Get the value entered in the TextBox
        string remark = txtRRemark.Text.Trim();
        string mremark = txtMRemark.Text.Trim();
        if (confirmValue == "No")
        {
        }
        else
        {
            string documentno = e.CommandArgument.ToString();
            string ccname = "TMU Hospital";
            string rccname = ccname.Replace(".", "_");


            string tblIndentHeader = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Indent Header" + "]";
            con = new Connection();

            CloseHeader(tblIndentHeader, documentno.Trim(), "10", mremark);


            con.DisConnect();
            ShowApprovaldata();
        }

    }
    public void CloseHeader(string tbleName, string DocumentNo, string status, string Remark)
    {
        if (con1 != null && con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }



        string sqlq = "update " + tbleName + " set [Status]='" + status + "',[Indent Close Date Time]=GETDATE() where [No_]='" + DocumentNo + "' ";

        SqlCommand cmd = new SqlCommand(sqlq, con1);
        cmd.ExecuteNonQuery();


    }
    protected void btnApprove_Command(object sender, CommandEventArgs e)
    {
        string confirmValue = Request.Form["confirm_value"];
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;

        // Find the TextBox inside that row
        TextBox txtRRemark = (TextBox)row.FindControl("txtRRemark");
        //TextBox txtMRemark = (TextBox)row.FindControl("txtMRemark");
        // Get the value entered in the TextBox
        string remark = txtRRemark.Text.Trim();
        string mremark = "";
        if (confirmValue == "No")
        {
        }
        else
        {
            string documentno = e.CommandArgument.ToString();
            string ccname = "TMU Hospital";
            string rccname = ccname.Replace(".", "_");


            string tblIndentHeader = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Equipment Indent Header" + "]";
            con = new Connection();
            //if (Session["uid"].ToString().Trim() == "TMU07417")
            //{
            //    SqlDataAdapter da = new SqlDataAdapter("select count([Management Appr_ QTY]) as AppQty from[NAAC_ADV_TEST].dbo.[TMU Hospital$Equipment Indent Line] where[Document No] = '" + documentno + "' and [HOD Appr_ QTY] = 0 ", con1);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    if (Convert.ToInt32(dt.Rows[0]["AppQty"]) == 0)
            //    {
            //        UpdateSendForApprovalHeader(tblIndentHeader, documentno.Trim(), "8", mremark);
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The quantity for an indent item cannot be zero.');", true);
            //        return;
            //    }



            //}
            //else
            //{
                SqlDataAdapter da = new SqlDataAdapter("select count([HOD Appr_ QTY]) as AppQty from [NAAC_ADV_TEST].dbo.[TMU Hospital$Equipment Indent Line] with(nolock) where[Document No] = '" + documentno + "' and [HOD Appr_ QTY] = 0 ", con1);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (Convert.ToInt32(dt.Rows[0]["AppQty"]) == 0)
                {
                    UpdateSendForApprovalHeader(tblIndentHeader, documentno.Trim(), "2", remark);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The quantity for an indent item cannot be zero.');", true);
                    return;
                }



            //}

            con.DisConnect();
            ShowApprovaldata();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ShowApprovaldata();
    }

    string GetHeaderTable()
    {
        string company = "TMU Hospital";
        return "[NAAC_ADV_TEST].dbo.[" + company.Replace(".", "_") + "$Equipment Indent Header]";
    }

    string GetLineTable()
    {
        string company = "TMU Hospital";
        return "[NAAC_ADV_TEST].dbo.[" + company.Replace(".", "_") + "$Equipment Indent Line]";
    }

    public void ShowApprovaldata()
    {
        try
        {
            string table = GetHeaderTable();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;

                    if (txtFromDate.Text == "" && txtTillDate.Text == "")
                    {
                        cmd.CommandText = @"select [Remarks- HOD] HodRemark,
                        [Remarks- Management] Management_remark,
                        No_ DocumentNo,
                        [Issue Date],
                        [Issue Id],
                        [Issue Name],
                        [Status] = CASE WHEN [Status] =null THEN '' WHEN [Status] ='0' THEN 'Open' WHEN [Status] ='1' THEN 'Processed for Approval' WHEN [Status] ='2' THEN 'Approved' WHEN [Status] ='3' THEN 'Released' WHEN [Status] ='4' THEN 'Rejected' WHEN [Status] ='5' THEN 'Issued' WHEN [Status] ='6' THEN 'Partially Issued' WHEN [Status] ='7' THEN 'Rejected from BME' WHEN [Status] ='10' THEN 'Item Received'  ELSE '' END,[Issue For]
                        from " + table + " where [Approval ID]=@uid order by No_ desc";

                        cmd.Parameters.AddWithValue("@uid", Session["uid"].ToString());
                    }
                    else
                    {
                        if (ddStatus.SelectedValue == "7")
                        {
                        cmd.CommandText = @"select [Remarks- HOD] HodRemark,
                        [Remarks- Management] Management_remark,
                        No_ DocumentNo,
                        [Issue Date],
                        [Issue Id],
                        [Issue Name],
                        [Status] = CASE WHEN [Status] =null THEN '' WHEN [Status] ='0' THEN 'Open' WHEN [Status] ='1' THEN 'Processed for Approval' WHEN [Status] ='2' THEN 'Approved' WHEN [Status] ='3' THEN 'Released' WHEN [Status] ='4' THEN 'Rejected' WHEN [Status] ='5' THEN 'Issued' WHEN [Status] ='6' THEN 'Partially Issued' WHEN [Status] ='7' THEN 'Rejected from BME' WHEN [Status] ='10' THEN 'Item Received'  ELSE '' END,[Issue For]
                        from " + table + @" 
                        where 
                         convert(date,[Issue Date],103)>=@fromdate
                        and convert(date,[Issue Date],103)<=@todate
                        order by No_ desc";

                           
                            cmd.Parameters.AddWithValue("@fromdate", txtFromDate.Text);
                            cmd.Parameters.AddWithValue("@todate", txtTillDate.Text);
                        }
                        else
                        {
                        cmd.CommandText = @"select [Remarks- HOD] HodRemark,
                        [Remarks- Management] Management_remark,
                        No_ DocumentNo,
                        [Issue Date],
                        [Issue Id],
                        [Issue Name],
                        [Status] = CASE WHEN [Status] =null THEN '' WHEN [Status] ='0' THEN 'Open' WHEN [Status] ='1' THEN 'Processed for Approval' WHEN [Status] ='2' THEN 'Approved' WHEN [Status] ='3' THEN 'Released' WHEN [Status] ='4' THEN 'Rejected' WHEN [Status] ='5' THEN 'Issued' WHEN [Status] ='6' THEN 'Partially Issued' WHEN [Status] ='7' THEN 'Rejected from BME' WHEN [Status] ='10' THEN 'Item Received'  ELSE '' END,[Issue For]
                        from " + table + @" 
                        where Status=@status
                        and convert(date,[Issue Date],103)>=@fromdate
                        and convert(date,[Issue Date],103)<=@todate
                        order by No_ desc";
                            cmd.Parameters.AddWithValue("@status", ddStatus.SelectedValue);
                            cmd.Parameters.AddWithValue("@fromdate", txtFromDate.Text);
                            cmd.Parameters.AddWithValue("@todate", txtTillDate.Text);
                        }
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    grdApproval.DataSource = dt;
                    grdApproval.DataBind();
                }
            }
        }
        catch
        {
            Response.Redirect("EquipmentApproval.aspx");
        }
    }
    protected void grdViewIndentLine_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdViewIndentLine.PageIndex = e.NewPageIndex;
        ShowLinedata(Session["indentdocumt"].ToString());
    }
    public void UpdateItemQuantity(string tbleName, string Quantity, string LineNo_)
    {
        if (con1 != null && con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }
        string sqlq = "";
        if (Session["uid"].ToString().Trim() == "TMU07417")
        {
            sqlq = "update " + tbleName + " set [Issue Qty]='" + Quantity + "',[Management Appr_ QTY]='" + Quantity + "' where [Line No_]=" + LineNo_ + "  ";
            SqlCommand cmd = new SqlCommand(sqlq, con1);
            cmd.ExecuteNonQuery();
        }
        else
        {
            sqlq = "update " + tbleName + " set [HOD Appr_ QTY]='" + Quantity + "',[Management Appr_ QTY]='" + Quantity + "' where [Line No_]=" + LineNo_ + "  ";
            SqlCommand cmd = new SqlCommand(sqlq, con1);
            cmd.ExecuteNonQuery();
        }

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        con = new Connection();
        string ccname = "TMU Hospital";
        string rccname = ccname.Replace(".", "_");
        string tblIndentLine = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Equipment Indent Line" + "]";

        int a = grdViewIndentLine.Rows.Count;
        for (int i = 0; i < grdViewIndentLine.Rows.Count; i++)
        {
            string LineNo = grdViewIndentLine.DataKeys[i].Values[0].ToString();
            HiddenField hfQuantity = (HiddenField)grdViewIndentLine.Rows[i].FindControl("hfQuantity");
            TextBox txtQty = (TextBox)grdViewIndentLine.Rows[i].FindControl("lblQuantity_Grid");
            //TextBox lblFinalQTY = (TextBox)grdViewIndentLine.Rows[i].FindControl("lblFinalQTY");

            if (txtQty.Text != hfQuantity.Value)
            {
                UpdateItemQuantity(tblIndentLine, txtQty.Text, LineNo);
            }
        }
    }
    protected void lblItemNo_Grid_Click(object sender, EventArgs e)
    {
        if (con1 != null && con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }

        LinkButton clickedLinkButton = sender as LinkButton;
        if (clickedLinkButton != null)
        {

            string itemNo = clickedLinkButton.Text;
            ShowItem(itemNo, "");

            mdIndentLine.Show();
            ModalPopupExtender1.Show();
        }
    }
    public void ShowItem(string ItemID, string Employee)
    {
        if (con1 != null && con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }
        try
        {
            using (SqlCommand cmd = new SqlCommand("GetItemDetailHospital", con1))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ItemNo", ItemID);
                cmd.Parameters.AddWithValue("@Employee", Employee);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        if (Employee == "")
                        {
                            lblItemCode.Text = dt.Rows[0]["Description"].ToString();
                            Label4.Text = dt.Rows[0]["Last QTY"].ToString();
                            Label5.Text = dt.Rows[0]["Last Purchase Date"].ToString();
                            Label6.Text = dt.Rows[0]["Available QTY"].ToString();
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                        }
                        else
                        {
                            Label8.Text = dt.Rows[0]["Description"].ToString();
                            Label9.Text = dt.Rows[0]["Last QTY"].ToString();
                            Label10.Text = dt.Rows[0]["Last Purchase Date"].ToString();
                            Label11.Text = dt.Rows[0]["Available QTY"].ToString();
                            GridView3.DataSource = dt;
                            GridView3.DataBind();
                        }
                    }
                    else
                    {
                        GridView1.DataSource = "";
                        GridView1.DataBind();
                        GridView3.DataSource = "";
                        GridView3.DataBind();
                    }
                }
            }
        }
        catch
        {
        }





    }
    protected void lnkNo__Click(object sender, EventArgs e)
    {
        if (con1 != null && con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }

        LinkButton clickedLinkButton = sender as LinkButton;

        GridViewRow row = (GridViewRow)clickedLinkButton.NamingContainer;
        LinkButton lblItemNo_Grid = (LinkButton)row.FindControl("lblItemNo_Grid");
        if (clickedLinkButton != null)
        {

            string EmployeeNo = clickedLinkButton.Text;
            ShowItem(lblItemNo_Grid.Text, EmployeeNo);

            mdIndentLine.Show();
            ModalPopupExtender2.Show();
        }
    }
    protected void lblDoumnentNo_Command1(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();

        Session["indentdocumt"] = id;

        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;

        Label lblStatus = (Label)row.FindControl("lblStatus");
        IndentStatus = lblStatus.Text;

        ShowLinedata(id);
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        if (ddStatus.SelectedValue == "7")
        {
            SqlDataAdapter da = new SqlDataAdapter("select [Document No],[Item No],Name,Description,convert(decimal(16,2),Quantity) as 'Quantity',[User id],[User Remark],[Location_Room no_],(select [Approval ID Name]   from  [NAAC_ADV_TEST].dbo.[TMU Hospital$Indent Header] where No_=IL.[Document No]) 'HOD Name' from [NAAC_ADV_TEST].dbo.[TMU Hospital$Equipment Indent Line]  IL where [Document No] in (select  No_  from [NAAC_ADV_TEST].dbo.[TMU Hospital$Indent Header]  IH where  [Approved Date Time- HOD]>='2025-10-09 17:40:06.047' ) order by [Document No]", con1);

            da.Fill(dt);

        }
        else
        {
            SqlDataAdapter da = new SqlDataAdapter("select [Document No],[Item No],Name,Description,convert(decimal(16,2),Quantity) as 'Quantity',[User id],[User Remark],[Location_Room no_],(select [Approval ID Name]   from  [NAAC_ADV_TEST].dbo.[TMU Hospital$Indent Header] where No_=IL.[Document No]) 'HOD Name'  from [NAAC_ADV_TEST].dbo.[TMU Hospital$Equipment Indent Line]  IL where [Document No] in (select  No_  from [NAAC_ADV_TEST].dbo.[TMU Hospital$Indent Header]  IH where  Status='" + ddStatus.SelectedValue.Trim() + "' and [Approved Date Time- HOD]>='2025-10-09 17:40:06.047' ) order by [Document No]", con1);

            da.Fill(dt);

        }

        GridView2.DataSource = dt;
        GridView2.DataBind();
        GridView2.Visible = true;
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        GridView2.RenderControl(htmlWrite);

        Response.Clear();
        string str = "IndentApprovalReport" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();
        GridView2.Visible = false;

    }
    public void ShowLinedata(string documentno)
    {
        con = new Connection();

        SqlDataReader dr = con.SHow_IndentLinedataforduplicaet(GetLineTable(), documentno);

        DataTable dt = new DataTable();
        dt.Load(dr);

        grdViewIndentLine.DataSource = dt;
        grdViewIndentLine.DataBind();

        dr.Close();
        con.DisConnect();

        mdIndentLine.Show();

      

        foreach (GridViewRow row in grdViewIndentLine.Rows)
        {
            TextBox qty = (TextBox)row.FindControl("lblQuantity_Grid");
           

            if (IndentStatus == "Processed for Approval" )
                qty.Enabled = true;
            else
                qty.Enabled = false;
  
        }
    }


    protected void btnReject_Command(object sender, CommandEventArgs e)
    {
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue != "Yes") return;

        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;

        TextBox txtRRemark = (TextBox)row.FindControl("txtRRemark");
        TextBox txtMRemark = (TextBox)row.FindControl("txtMRemark");

        string documentno = e.CommandArgument.ToString();

        if (Session["uid"].ToString() == "TMU07417")
        {
            if (txtMRemark.Text == "")
            {
                Alert("Reject Remark is Mandatory");
                return;
            }

            UpdateSendForApprovalHeader(GetHeaderTable(), documentno, "9", txtMRemark.Text);
        }
        else
        {
            if (txtRRemark.Text == "")
            {
                Alert("Reject Remark is Mandatory");
                return;
            }

            UpdateSendForApprovalHeader(GetHeaderTable(), documentno, "4", txtRRemark.Text);
        }

        ShowApprovaldata();
    }

    public void UpdateSendForApprovalHeader(string table, string document, string status, string remark)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
        {
            con.Open();

            string sql = "";

            if (Session["uid"].ToString() == "TMU07417")
            {
                sql = "update " + table + " set Status=@status,[Remarks- Management]=@remark,[Approved Date Time- Management]=GETDATE() where No_=@doc";
            }
            else
            {
                sql = "update " + table + " set Status=@status,[Remarks- HOD]=@remark,[Approved Date Time- HOD]=GETDATE() where No_=@doc";
            }

            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@remark", remark);
            cmd.Parameters.AddWithValue("@doc", document);

            cmd.ExecuteNonQuery();
        }
    }

    protected void grdApproval_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;

        Label lblStatus = (Label)e.Row.FindControl("lblStatus");

        Button btnApprove = (Button)e.Row.FindControl("btnApprove");
        Button btnReject = (Button)e.Row.FindControl("btnReject");
        Button btnClose = (Button)e.Row.FindControl("btnClose");

        TextBox txtRRemark = (TextBox)e.Row.FindControl("txtRRemark");
        TextBox txtMRemark = (TextBox)e.Row.FindControl("txtMRemark");

        bool isManagement = Session["uid"].ToString() == "TMU07417";

        if (lblStatus.Text == "Issued" && !isManagement)
            btnClose.Visible = true;

        if (lblStatus.Text == "Processed for Approval")
        {
            btnApprove.Visible = true;
            btnReject.Visible = true;
            txtRRemark.Enabled = true;
        }

       else
        {
            btnApprove.Visible = false;
            btnReject.Visible = false;
            txtRRemark.Enabled = false;
        }
    }

    protected void grdApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdApproval.PageIndex = e.NewPageIndex;
        ShowApprovaldata();
    }

    public void Alert(string message)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
        "alert('" + message + "');", true);
    }


}
