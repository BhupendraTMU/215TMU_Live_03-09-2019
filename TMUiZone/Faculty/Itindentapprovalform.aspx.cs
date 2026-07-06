using System;

using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using AjaxControlToolkit;
using iTextSharp.text;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.ComponentModel.DataAnnotations;






public partial class Faculty_Itindentapprovalform : System.Web.UI.Page
{
    Connection con; String IndentStatus = "";
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Session["uid"].ToString() == "TMU07417")
            {
                string[] valuesToRemove = { "1", "4" };

                // Loop backwards to safely remove items
                for (int i = ddStatus.Items.Count - 1; i >= 0; i--)
                {
                    if (valuesToRemove.Contains(ddStatus.Items[i].Value))
                    {
                        ddStatus.Items.RemoveAt(i);
                    }
                }
            }

            ShowApprovaldata();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ShowApprovaldata();
    }
    public void ShowApprovaldata()
    {
        con = new Connection();
        string ccname = "TMU Advertisement";
        string rccname = ccname.Replace(".", "_");

        string tblIndentHeader = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Indent Header" + "]";
        con.Update_Indentheaderhoddata(tblIndentHeader, Session["IndentApproval_DH"].ToString().Trim(), Session["uid"].ToString().Trim());
        con.DisConnect();
        if (txtFromDate.Text == "" && txtTillDate.Text == "")
        {
            SqlDataReader dr = SHow_IndentHeaderforApprovalbyhod(tblIndentHeader, Session["uid"].ToString().Trim());
            DataTable dt = new DataTable();
            dt.Load(dr);
            grdApproval.DataSource = dt;
            grdApproval.DataBind();
            dr.Close();
            con.DisConnect();
        }
        else
        {
            if (Session["uid"].ToString().Trim() == "TMU07417")
            {


                SqlDataAdapter da = new SqlDataAdapter("select [Remarks- HOD] as 'HodRemark',[Remarks- Management] Management_remark, No_ as DocumentNo ,[Issue Date] , [Issue For] = CASE WHEN [Issue For] =null THEN '' WHEN [Issue For] ='1' THEN 'Department' WHEN [Issue For] ='2' THEN 'Employee' ELSE '' END, [Status] = CASE WHEN [Status] =null THEN '' WHEN [Status] ='0' THEN 'Open' WHEN [Status] ='1' THEN 'Processed for Approval' WHEN [Status] ='2' THEN 'Pending on Management' WHEN [Status] ='3' THEN 'Released' WHEN [Status] ='4' THEN 'Rejected' WHEN [Status] ='5' THEN 'Issued' WHEN [Status] ='8' THEN 'Approved by Management' WHEN [Status] ='9' THEN 'Rejected by Management' WHEN [Status] ='10' THEN 'Item Received'  ELSE '' END  ,[Issue Id],[Issue Name] from [NAAC_ADV_TEST].dbo.[TMU Advertisement$Indent Header] where Status='" + ddStatus.SelectedValue.Trim() + "' and [Approved Date Time- HOD]>='2025-10-09 17:40:06.047' and convert(date,[Issue Date],103)>='" + txtFromDate.Text.Trim() + "' and convert(date,[Issue Date],103)<='" + txtTillDate.Text.Trim() + "'  ORDER BY CASE WHEN [Status] = '5' THEN 0 ELSE 1 END, DocumentNo DESC; ", con1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grdApproval.DataSource = dt;
                grdApproval.DataBind();
            }
            else
            {
                SqlDataAdapter da = new SqlDataAdapter("select [Remarks- HOD] as 'HodRemark',[Remarks- Management] Management_remark, No_ as DocumentNo ,[Issue Date] , [Issue For] = CASE WHEN [Issue For] =null THEN '' WHEN [Issue For] ='1' THEN 'Department' WHEN [Issue For] ='2' THEN 'Employee' ELSE '' END,[Status] = CASE WHEN [Status] =null THEN '' WHEN [Status] ='0' THEN 'Open' WHEN [Status] ='1' THEN 'Processed for Approval' WHEN [Status] ='2' THEN 'Pending on Management' WHEN [Status] ='3' THEN 'Released' WHEN [Status] ='4' THEN 'Rejected' WHEN [Status] ='5' THEN 'Issued' WHEN [Status] ='8' THEN 'Approved by Management' WHEN [Status] ='9' THEN 'Rejected by Management' WHEN [Status] ='10' THEN 'Item Received'  WHEN [Status] ='5' THEN 'Approved by Management' WHEN [Status] ='6' THEN 'Reject by Management' ELSE '' END ,[Issue Id],(select [Department Name] from [TMU$Employee] where [No_]=H1.[Employee ID]) as [Issue Name] from " + tblIndentHeader + " as H1 where [Approval ID]='" + Session["uid"].ToString().Trim() + "' and convert(date,[Issue Date],103)>='" + txtFromDate.Text.Trim() + "' and convert(date,[Issue Date],103)<='" + txtTillDate.Text.Trim() + "' and [Status]='" + ddStatus.SelectedValue.Trim() + "' ORDER BY CASE WHEN [Status] = '5' THEN 0 ELSE 1 END, DocumentNo DESC;", con1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grdApproval.DataSource = dt;
                grdApproval.DataBind();
            }

            //dr.Close();
            //con.DisConnect();
        }
    }
    public SqlDataReader SHow_IndentHeaderforApprovalbyhod(string companyName, string ApprovalID)
    {
        if (con1 != null && con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }
        string sqlq = "";
        if (Session["uid"].ToString().Trim() == "TMU07417")
        {
            if (ddStatus.SelectedValue.Trim() == "7")
            {
                sqlq = "select [Remarks- HOD] as 'HodRemark',[Remarks- Management] Management_remark, No_ as DocumentNo ,[Issue Date] , [Issue For] = CASE WHEN [Issue For] =null THEN '' WHEN [Issue For] ='1' THEN 'Department' WHEN [Issue For] ='2' THEN 'Employee' ELSE '' END, [Status] = CASE WHEN [Status] =null THEN '' WHEN [Status] ='0' THEN 'Open' WHEN [Status] ='1' THEN 'Processed for Approval' WHEN [Status] ='2' THEN 'Pending on Management' WHEN [Status] ='3' THEN 'Released' WHEN [Status] ='4' THEN 'Rejected' WHEN [Status] ='5' THEN 'Issued' WHEN [Status] ='8' THEN 'Approved by Management' WHEN [Status] ='10' THEN 'Item Received' WHEN [Status] ='9' THEN 'Rejected by Management'  ELSE '' END  ,[Issue Id],[Issue Name] from [NAAC_ADV_TEST].dbo.[TMU Advertisement$Indent Header] where Status in (2,8,9,5) and [Approved Date Time- HOD]>='2025-10-09 17:40:06.047' ORDER BY CASE WHEN [Status] = '5' THEN 0 ELSE 1 END, DocumentNo DESC;";
            }
            else
            {
                sqlq = "select [Remarks- HOD] as 'HodRemark',[Remarks- Management] Management_remark, No_ as DocumentNo ,[Issue Date] , [Issue For] = CASE WHEN [Issue For] =null THEN '' WHEN [Issue For] ='1' THEN 'Department' WHEN [Issue For] ='2' THEN 'Employee' ELSE '' END, [Status] = CASE WHEN [Status] =null THEN '' WHEN [Status] ='0' THEN 'Open' WHEN [Status] ='1' THEN 'Processed for Approval' WHEN [Status] ='2' THEN 'Pending on Management' WHEN [Status] ='3' THEN 'Released' WHEN [Status] ='4' THEN 'Rejected' WHEN [Status] ='5' THEN 'Issued' WHEN [Status] ='8' THEN 'Approved by Management' WHEN [Status] ='10' THEN 'Item Received' WHEN [Status] ='9' THEN 'Rejected by Management'  ELSE '' END  ,[Issue Id],[Issue Name] from [NAAC_ADV_TEST].dbo.[TMU Advertisement$Indent Header] where Status='" + ddStatus.SelectedValue.Trim() + "' and [Approved Date Time- HOD]>='2025-10-09 17:40:06.047' ORDER BY CASE WHEN [Status] = '5' THEN 0 ELSE 1 END, DocumentNo DESC;";
            }
        }
        else
        {
            if (ddStatus.SelectedValue.Trim() == "7")
            {

                sqlq = "select [Remarks- HOD] as 'HodRemark',[Remarks- Management] Management_remark, No_ as DocumentNo ,[Issue Date] , [Issue For] = CASE WHEN [Issue For] =null THEN '' WHEN [Issue For] ='1' THEN 'Department' WHEN [Issue For] ='2' THEN 'Employee' ELSE '' END, [Status] = CASE WHEN [Status] =null THEN '' WHEN [Status] ='0' THEN 'Open' WHEN [Status] ='1' THEN 'Processed for Approval' WHEN [Status] ='2' THEN 'Pending on Management' WHEN [Status] ='3' THEN 'Released' WHEN [Status] ='4' THEN 'Rejected' WHEN [Status] ='5' THEN 'Issued' WHEN [Status] ='8' THEN 'Approved by Management' WHEN [Status] ='10' THEN 'Item Received' WHEN [Status] ='9' THEN 'Rejected by Management'  ELSE '' END  ,[Issue Id],[Issue Name] from " + companyName + " where [Approval ID]='" + ApprovalID + "' ORDER BY CASE WHEN [Status] = '5' THEN 0 ELSE 1 END, DocumentNo DESC;";
            }
            else
            {
                sqlq = "select [Remarks- HOD] as 'HodRemark',[Remarks- Management] Management_remark, No_ as DocumentNo ,[Issue Date] , [Issue For] = CASE WHEN [Issue For] =null THEN '' WHEN [Issue For] ='1' THEN 'Department' WHEN [Issue For] ='2' THEN 'Employee' ELSE '' END, [Status] = CASE WHEN [Status] =null THEN '' WHEN [Status] ='0' THEN 'Open' WHEN [Status] ='1' THEN 'Processed for Approval' WHEN [Status] ='2' THEN 'Pending on Management' WHEN [Status] ='3' THEN 'Released' WHEN [Status] ='4' THEN 'Rejected' WHEN [Status] ='5' THEN 'Issued' WHEN [Status] ='8' THEN 'Approved by Management' WHEN [Status] ='10' THEN 'Item Received' WHEN [Status] ='9' THEN 'Rejected by Management'  ELSE '' END  ,[Issue Id],[Issue Name] from " + companyName + " where [Approval ID]='" + ApprovalID + "' and Status='" + ddStatus.SelectedValue.Trim() + "' ORDER BY CASE WHEN [Status] = '5' THEN 0 ELSE 1 END, DocumentNo DESC;";
            }

        }

        SqlCommand cmd = new SqlCommand(sqlq, con1);
        cmd = new SqlCommand(sqlq, con1);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    protected void lblDoumnentNo_Command(object sender, CommandEventArgs e)
    {

        string id = e.CommandArgument.ToString();
        Session["indentdocumt"] = "";
        Session["indentdocumt"] = id.Trim();
        LinkButton btn = (LinkButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        int RowIndex = gvr.RowIndex;
        Label lbStatus = (Label)grdApproval.Rows[RowIndex].FindControl("lblStatus");
        IndentStatus = lbStatus.Text;
        ShowLinedata(id);
    }
    public void ShowLinedata(string documentno)
    {

        con = new Connection();
        string ccname = "TMU Advertisement";
        string rccname = ccname.Replace(".", "_");
        string tblIndentLine = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Indent Line" + "]";
        SqlDataReader dr = con.SHow_IndentLinedataforduplicaet(tblIndentLine, documentno.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        grdViewIndentLine.DataSource = dt;
        grdViewIndentLine.DataBind();
        dr.Close();
        con.DisConnect();
        mdIndentLine.Show();

        if (IndentStatus == "Processed for Approval" && Session["uid"].ToString().Trim() != "TMU07417")
        {
            for (int i = 0; i < grdViewIndentLine.Rows.Count; i++)
            {
                TextBox type = (TextBox)grdViewIndentLine.Rows[i].FindControl("lblQuantity_Grid");
                type.Enabled = true;
                Button2.Visible = true;
            }
        }
        else
        {
            for (int i = 0; i < grdViewIndentLine.Rows.Count; i++)
            {
                TextBox type = (TextBox)grdViewIndentLine.Rows[i].FindControl("lblQuantity_Grid");
                type.Enabled = false;
                Button2.Visible = false;
            }
        }
        if (Session["uid"].ToString().Trim() == "TMU07417")
        {
            if (IndentStatus == "Pending on Management" && Session["uid"].ToString().Trim() == "TMU07417")
            {
                for (int i = 0; i < grdViewIndentLine.Rows.Count; i++)
                {
                    TextBox type = (TextBox)grdViewIndentLine.Rows[i].FindControl("lblFinalQTY");
                    type.Enabled = true;
                    Button2.Visible = true;
                }
            }
            else
            {
                for (int i = 0; i < grdViewIndentLine.Rows.Count; i++)
                {
                    TextBox type = (TextBox)grdViewIndentLine.Rows[i].FindControl("lblFinalQTY");
                    type.Enabled = false;
                    Button2.Visible = false;
                }
            }
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
            string ccname = "TMU Advertisement";
            string rccname = ccname.Replace(".", "_");


            string tblIndentHeader = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Indent Header" + "]";
            con = new Connection();

            CloseHeader(tblIndentHeader, documentno.Trim(), "10", mremark);


            con.DisConnect();
            ShowApprovaldata();
        }

    }

    protected void btnApprove_Command(object sender, CommandEventArgs e)
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
            string ccname = "TMU Advertisement";
            string rccname = ccname.Replace(".", "_");


            string tblIndentHeader = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Indent Header" + "]";
            con = new Connection();
            if (Session["uid"].ToString().Trim() == "TMU07417")
            {
                SqlDataAdapter da = new SqlDataAdapter("select count([Management Appr_ QTY]) as AppQty from[NAAC_ADV_TEST].dbo.[TMU Advertisement$Indent Line] where[Document No] = '" + documentno + "' and [HOD Appr_ QTY] = 0 ", con1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (Convert.ToInt32(dt.Rows[0]["AppQty"]) == 0)
                {
                    UpdateSendForApprovalHeader(tblIndentHeader, documentno.Trim(), "8", mremark);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The quantity for an indent item cannot be zero.');", true);
                    return;
                }


               
            }
            else
            {
                SqlDataAdapter da = new SqlDataAdapter("select count([HOD Appr_ QTY]) as AppQty from[NAAC_ADV_TEST].dbo.[TMU Advertisement$Indent Line] where[Document No] = '"+ documentno + "' and [HOD Appr_ QTY] = 0 ", con1);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (Convert.ToInt32(dt.Rows[0]["AppQty"]) ==0)
                {
                    UpdateSendForApprovalHeader(tblIndentHeader, documentno.Trim(), "2", remark);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The quantity for an indent item cannot be zero.');", true);
                    return;
                }
                

               
            }

            con.DisConnect();
            ShowApprovaldata();
        }
    }
    //public void UpdateSendForApprovalHeader(string tbleName, string DocumentNo, string status,string Remark)
    //{
    //    con = new Connection();

    //    string sqlq = "update " + tbleName + " set [Status]='" + status + "',[Remarks- HOD]='" + Remark + "',[Approved Date Time- HOD]=Getdate() where [No_]='" + DocumentNo + "' ";
    //    SqlCommand cmd = new SqlCommand();
    //    cmd = new SqlCommand(sqlq, con.Con);
    //    cmd.ExecuteNonQuery();

    //}

    protected void btnReject_Command(object sender, CommandEventArgs e)
    {
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "No")
        {
        }
        else
        {
            Button btn = (Button)sender;
            GridViewRow grow = btn.NamingContainer as GridViewRow;
            TextBox txtremark = (TextBox)grow.FindControl("txtRRemark");

            TextBox txtMRemark = (TextBox)grow.FindControl("txtMRemark");

            string documentno = e.CommandArgument.ToString();
            string mremark = txtMRemark.Text.Trim();
            if (Session["uid"].ToString().Trim() == "TMU07417" && mremark == "")
            {
                string message1 = "Reject Remark is Mandatory";
                string script1 = "window.onload = function(){ alert('";
                script1 += message1;
                script1 += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
                return;
            }

            if (txtremark.Text == "" && Session["uid"].ToString().Trim() != "TMU07417")
            {
                string message1 = "Reject Remark is Mandatory";
                string script1 = "window.onload = function(){ alert('";
                script1 += message1;
                script1 += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
                return;
            }


            string ccname = "TMU Advertisement";
            string rccname = ccname.Replace(".", "_");


            string tblIndentHeader = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Indent Header" + "]";
            con = new Connection();


            if (Session["uid"].ToString().Trim() == "TMU07417")
            {
                UpdateSendForApprovalHeader(tblIndentHeader, documentno.Trim(), "9", mremark);
            }
            else
            {
                UpdateSendForApprovalHeader(tblIndentHeader, documentno.Trim(), "4", txtremark.Text);
            }




            con.DisConnect();
            ShowApprovaldata();
        }
    }
    public void UpdateSendForApprovalHeader(string tbleName, string DocumentNo, string status, string Remark)
    {
        if (con1 != null && con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }
        if (Session["uid"].ToString().Trim() == "TMU07417")
        {
            string sqlq = "update " + tbleName + " set [Status]='" + status + "',[Remarks- Management]='" + Remark + "',[Approved Date Time- Management]=GETDATE() where [No_]='" + DocumentNo + "' ";

            SqlCommand cmd = new SqlCommand(sqlq, con1);
            cmd.ExecuteNonQuery();
        }

        else
        {


            string sqlq = "update " + tbleName + " set [Status]='" + status + "',[Remarks- HOD]='" + Remark + "',[Approved Date Time- HOD]=GETDATE() where [No_]='" + DocumentNo + "' ";

            SqlCommand cmd = new SqlCommand(sqlq, con1);
            cmd.ExecuteNonQuery();
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
    protected void grdApproval_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                Button btnApprove = (Button)e.Row.FindControl("btnApprove");
                Button btnClose = (Button)e.Row.FindControl("btnClose");
                Button btnReject = (Button)e.Row.FindControl("btnReject");
                TextBox txtRRemark = (TextBox)e.Row.FindControl("txtRRemark");
                TextBox txtMRemark = (TextBox)e.Row.FindControl("txtMRemark");
                if (lblStatus.Text == "Issued" && Session["uid"].ToString().Trim() != "TMU07417")
                {
                    btnClose.Visible = true;
                }
                if (lblStatus.Text.Trim() == "Processed for Approval" && Session["uid"].ToString().Trim() != "TMU07417")
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
                if (Session["uid"].ToString().Trim() == "TMU07417")
                {
                    if (lblStatus.Text.Trim() == "Pending on Management" && Session["uid"].ToString().Trim() == "TMU07417")
                    {
                        btnApprove.Visible = true;
                        btnReject.Visible = true;
                        txtMRemark.Enabled = true;
                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        txtMRemark.Enabled = false;
                    }
                }
            }
        }
        catch (Exception)
        { }
    }

    protected void grdApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdApproval.PageIndex = e.NewPageIndex;
        ShowApprovaldata();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        con = new Connection();
        string ccname = "TMU Advertisement";
        string rccname = ccname.Replace(".", "_");
        string tblIndentLine = "[NAAC_ADV_TEST].dbo.[" + rccname + "$Indent Line" + "]";

        int a = grdViewIndentLine.Rows.Count;
        for (int i = 0; i < grdViewIndentLine.Rows.Count; i++)
        {
            string LineNo = grdViewIndentLine.DataKeys[i].Values[0].ToString();
            HiddenField hfQuantity = (HiddenField)grdViewIndentLine.Rows[i].FindControl("hfQuantity");
            TextBox txtQty = (TextBox)grdViewIndentLine.Rows[i].FindControl("lblQuantity_Grid");
            TextBox lblFinalQTY = (TextBox)grdViewIndentLine.Rows[i].FindControl("lblFinalQTY");

            if (Session["uid"].ToString().Trim() == "TMU07417")
            {


                UpdateItemQuantity(tblIndentLine, lblFinalQTY.Text, LineNo);  //for code




            }
            else
            {

                if (txtQty.Text != hfQuantity.Value)
                {


                    UpdateItemQuantity(tblIndentLine, txtQty.Text, LineNo);  //for code



                }
            }
        }
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
    protected void grdViewIndentLine_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdViewIndentLine.PageIndex = e.NewPageIndex;
        ShowLinedata(Session["indentdocumt"].ToString());
    }
    protected void rdDocumentNo_CheckedChanged(object sender, EventArgs e)
    {
        pnlDatewisefilter.Visible = true;
        txtFromDate.Text = "";
        txtTillDate.Text = "";
        //txtSearch.Text = ""; 
        //txtSearch.Visible = true;

    }

    protected void lblDoumnentNo_Command1(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        Session["indentdocumt"] = "";
        Session["indentdocumt"] = id.Trim();
        LinkButton btn = (LinkButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        int RowIndex = gvr.RowIndex;
        Label lbStatus = (Label)grdApproval.Rows[RowIndex].FindControl("lblStatus");
        IndentStatus = lbStatus.Text;
        ShowLinedata(id);
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
            ShowItem(itemNo,"");

            mdIndentLine.Show();
            ModalPopupExtender1.Show();
        }
    }
    public void ShowItem(string ItemID,string Employee)
    {
        if (con1 != null && con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }
        try
        {
            using (SqlCommand cmd = new SqlCommand("GetItemDetail", con1))
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
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        if (ddStatus.SelectedValue == "7")
        {
            SqlDataAdapter da = new SqlDataAdapter("select [Document No],[Item No],Name,Description,convert(decimal(16,2),Quantity) as 'Quantity',[User id],[User Remark],[Location_Room no_],\r\n\r\n(select [Approval ID Name]   from  [NAAC_ADV_TEST].dbo.[TMU Advertisement$Indent Header] where No_=IL.[Document No]) 'HOD Name'\r\n\r\nfrom [NAAC_ADV_TEST].dbo.[TMU Advertisement$Indent Line]  IL where [Document No] in \r\n\r\n(select  No_  from [NAAC_ADV_TEST].dbo.[TMU Advertisement$Indent Header]  IH where  [Approval ID]='"+ Session["uid"].ToString() + "' and [Approved Date Time- HOD]>='2025-10-09 17:40:06.047' ) order by [Document No]", con1);

            da.Fill(dt);

        }
        else
        {
            SqlDataAdapter da = new SqlDataAdapter("select [Document No],[Item No],Name,Description,convert(decimal(16,2),Quantity) as 'Quantity',[User id],[User Remark],[Location_Room no_],\r\n\r\n(select [Approval ID Name]   from  [NAAC_ADV_TEST].dbo.[TMU Advertisement$Indent Header] where No_=IL.[Document No]) 'HOD Name'\r\n\r\nfrom [NAAC_ADV_TEST].dbo.[TMU Advertisement$Indent Line]  IL where [Document No] in \r\n\r\n(select  No_  from [NAAC_ADV_TEST].dbo.[TMU Advertisement$Indent Header]  IH where [Approval ID]='"+ Session["uid"].ToString() + "' and  Status='" + ddStatus.SelectedValue.Trim() + "' and [Approved Date Time- HOD]>='2025-10-09 17:40:06.047' ) order by [Document No]", con1);

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
            ShowItem(lblItemNo_Grid.Text,EmployeeNo);

            mdIndentLine.Show();
            ModalPopupExtender2.Show();
        }
    }
}