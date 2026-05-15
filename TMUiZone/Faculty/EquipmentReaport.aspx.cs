using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
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

public partial class Faculty_EquipmentReaport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {



            LoadCounts();
        }
    }

    void LoadCounts()
    {
        con.Open();

        SqlCommand cmd = new SqlCommand(@"
            SELECT 
            (select count(distinct No_) from [NAAC_ADV_TEST].dbo.[TMU Hospital$Fixed Asset] ) AS EquipmentCount,
            (select count(distinct [Code]) from [NAAC_ADV_TEST].dbo.[TMU Hospital$Location]  where Code !='IN TRANSIT') AS LocationCount,
            (select Count(Accessories) from [NAAC_ADV_TEST].dbo.[TMU Hospital$AMC_CMC_WARRANTY_FOC Line] where Accessories!='') AS AccessoriesCount
        ", con);

        SqlDataReader dr = cmd.ExecuteReader();

        if (dr.Read())
        {
            lblEquipmentCount.Text = dr["EquipmentCount"].ToString();
            lblLocationCount.Text = dr["LocationCount"].ToString();
            lblAccessoriesCount.Text = dr["AccessoriesCount"].ToString();
        }

        con.Close();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(currentTable)) return;

        string search = txtSearch.Text.Trim();

        string query = "";

        if (currentTable == "[NAAC_ADV_TEST].dbo.[TMU Hospital$Fixed Asset]")
        {
            query += "SELECT [Item No], Description,count(distinct [No_]) 'C' FROM " + currentTable + " WHERE ([Item No] LIKE '%" + search + "%' or  [No_] like '%" + search + "%') group by [Item No], Description";
            LoadGrid(query);
        }

        if (currentTable == "[NAAC_ADV_TEST].dbo.[TMU Hospital$Location]")
        {
            query += "SELECT Code,Name FROM " + currentTable + " WHERE [Code] LIKE '%" + search + "%' ";
            LoadGridLocation(query);
        }
        if (currentTable == "[NAAC_ADV_TEST].dbo.[TMU Hospital$AMC_CMC_WARRANTY_FOC Line]")
        {
            query += "SELECT Accessories,[Work Description],Quantity,[Equipment No_],[Name of Equipment],[Location Code],[Approx Amount],[Serial Number] FROM " + currentTable + " WHERE [Accessories] LIKE '%" + search + "%' and Accessories!='' ";
            LoadGridAccess(query);
        }



    }
    protected void lnkEquipment_Click(object sender, EventArgs e)
    {
        gvReportEQ.Visible = true;
        grdLocation.Visible = false;
        GVAccess.Visible = false;
        currentTable = "[NAAC_ADV_TEST].dbo.[TMU Hospital$Fixed Asset]";
        LoadGrid("select  [Item No], Description,count(distinct [No_]) as C  from [NAAC_ADV_TEST].dbo.[TMU Hospital$Fixed Asset] where [Item No]!='' and Description!='' group by [Item No], Description ");
    }

    protected void lnkLocation_Click(object sender, EventArgs e)
    {
        GVAccess.Visible = false;
        gvReportEQ.Visible = false;
        grdLocation.Visible = true;
        currentTable = "[NAAC_ADV_TEST].dbo.[TMU Hospital$Location]";
        LoadGridLocation("select Code,Name from [NAAC_ADV_TEST].dbo.[TMU Hospital$Location]  where Code !='IN TRANSIT'");
    }

    protected void lnkAccessories_Click(object sender, EventArgs e)
    {

        GVAccess.Visible = true;
        gvReportEQ.Visible = false;
        grdLocation.Visible = false;
        currentTable = "[NAAC_ADV_TEST].dbo.[TMU Hospital$AMC_CMC_WARRANTY_FOC Line]";
        LoadGridAccess("select Accessories,[Work Description],Quantity,[Equipment No_],[Name of Equipment],[Location Code],[Approx Amount],[Serial Number] from [NAAC_ADV_TEST].dbo.[TMU Hospital$AMC_CMC_WARRANTY_FOC Line] where Accessories!=''");


    }
    void LoadGridLocation(string query)
    {
        SqlDataAdapter da = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        grdLocation.DataSource = dt;
        grdLocation.DataBind();
    }
    void LoadGrid(string query)
    {
        SqlDataAdapter da = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvReportEQ.DataSource = dt;
        gvReportEQ.DataBind();
    }
    void LoadGridAccess(string query)
    {
        SqlDataAdapter da = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GVAccess.DataSource = dt;
        GVAccess.DataBind();
    }
    string currentTable
    {
        get
        {
            if (ViewState["currentTable"] != null)
                return ViewState["currentTable"].ToString();
            else
                return null;
        }
        set
        {
            ViewState["currentTable"] = value;
        }
    }
    public DataTable GetData(string itemNo)
    {
        DataTable dt = new DataTable();


        using (SqlCommand cmd = new SqlCommand("NAAC_ADV_TEST.dbo.sp_Equipment_Report_Format", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ItemNo", itemNo);

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }
        }


        return dt;
    }

    protected void gvReportEQ_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewFA")
        {
            string equipmentNo = e.CommandArgument.ToString();
            LoadFA(equipmentNo);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup1",
           "$('#FAModal').modal('show');", true);
        }
        if (e.CommandName == "Viewdetails")
        {

            string itemNo = e.CommandArgument.ToString();

            ViewState["ItemNo"] = itemNo;

            DataTable dt = GetReportData(itemNo);

            if (dt.Rows.Count > 0)
            {
                // ITEM HEADER
                //lblItemNo.Text = dt.Rows[0]["ItemNo"].ToString();
                //lblPO.Text = dt.Rows[0]["PurchaseOrderNo"].ToString();
                //lblPurchase.Text = dt.Rows[0]["PurchaseDate"].ToString();
                //lblVendor.Text = dt.Rows[0]["PurchaseVendor"].ToString();

                // FA TABLE
                DataTable faTable = new DataTable();
                faTable.Columns.Add("EquipmentNo");
                faTable.Columns.Add("EquipmentName");
                faTable.Columns.Add("SerialNo");
                faTable.Columns.Add("LocationName");
                faTable.Columns.Add("InstallationDate");
                faTable.Columns.Add("AMCStart");
                faTable.Columns.Add("AMCEnd");

                var groups = dt.AsEnumerable()
                    .Where(x => x["EquipmentNo"] != DBNull.Value)
                    .GroupBy(x => x["EquipmentNo"].ToString());

                foreach (var g in groups)
                {
                    var first = g.First();

                    faTable.Rows.Add(
                        first["EquipmentNo"],
                        first["EquipmentName"],
                        first["SerialNo"],
                        first["LocationName"],
                        first["InstallationDate"],
                        first["AMCStart"],
                        first["AMCEnd"]
                    );
                }

                rptFA.DataSource = faTable;
                rptFA.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "Pop", "$('#detailsModal').modal('show');", true);
            }
        }
    }


    protected void rptFA_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string faNo = DataBinder.Eval(e.Item.DataItem, "EquipmentNo").ToString();

            DataTable dt = GetReportData(ViewState["ItemNo"].ToString());

            DataView dv = dt.DefaultView;
            dv.RowFilter = "EquipmentNo = '" + faNo + "'";

            Repeater rptAccessories = (Repeater)e.Item.FindControl("rptAccessories");
            rptAccessories.DataSource = dv;
            rptAccessories.DataBind();
        }
    }
    public DataTable GetReportData1(string itemNo)
    {
        DataTable dt = new DataTable();


        SqlCommand cmd = new SqlCommand("sp_Equipment_Hierarchical_Report_Fa", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ItemNo", itemNo);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);


        return dt;
    }
    public DataTable GetReportDataDepartment(string itemNo)
    {
        DataTable dt = new DataTable();


        SqlCommand cmd = new SqlCommand("sp_Equipment_Hierarchical_Report_Department", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ItemNo", itemNo);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);


        return dt;
    }
    public DataTable GetReportData(string itemNo)
    {
        DataTable dt = new DataTable();


        SqlCommand cmd = new SqlCommand("sp_Equipment_Hierarchical_Report", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ItemNo", itemNo);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);


        return dt;
    }
    public string GetValue(DataRow row, string columnName)
    {
        if (row.Table.Columns.Contains(columnName)) // column exist check
        {
            return row[columnName] == DBNull.Value ? "" : row[columnName].ToString();
        }
        return "";
    }


    protected void btnExportAccessories_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=AccessoriesReport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        // 🔥 Optional: paging OFF (agar paging ON hai to)
        gvFA.AllowPaging = false;

        // 🔥 Serial No add karna ho to (UI me nahi hai to)
        //AddSerialNumber(gvFA);

        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gvFA.RenderControl(hw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    private void LoadFA(string equipmentNo)
    {
        // Example Query (apne table ke hisab se change karo)
        string query = "select No_,Description,[Location Code], CASE  WHEN [Warranty Date] = '1753-01-01 00:00:00.000' THEN ''  ELSE CONVERT(VARCHAR(10), [Warranty Date], 103) END AS [Warranty Date],[Serial No_],CASE  WHEN [Next Service Date] = '1753-01-01 00:00:00.000' THEN ''  ELSE CONVERT(VARCHAR(10), [Next Service Date], 103) END AS [Next Service Date] ,Brand,Model, CASE WHEN [Date of Installation] = '1753-01-01 00:00:00.000' THEN ''  ELSE CONVERT(VARCHAR(10), [Date of Installation], 103)  END AS [Date of Installation],[Department Name] from [NAAC_ADV_TEST].dbo.[TMU Hospital$Fixed Asset] where [Item No]      = '" + equipmentNo + "'";

        DataTable dt = new DataTable();


        using (SqlDataAdapter da = new SqlDataAdapter(query, con))
        {
            da.Fill(dt);
        }


        gvFA.DataSource = dt;
        gvFA.DataBind();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Required for Export
    }


    protected void btnExport_Click(object sender, EventArgs e)
    {






        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Items.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        if (GVAccess.Visible == true)
        {
            GVAccess.AllowPaging = false;
            // Agar dubara bind karna ho to yaha LoadAccessories() call karo

            GVAccess.RenderControl(hw);
        }
        else
        {
            gvReportEQ.AllowPaging = false;
            // Agar dubara bind karna ho to yaha LoadAccessories() call karo

            gvReportEQ.RenderControl(hw);
        }
        gvReportEQ.AllowPaging = false;
        // Agar dubara bind karna ho to yaha LoadAccessories() call karo

        gvReportEQ.RenderControl(hw);

        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();


    }





    protected void grdLocation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewItem")
        {
            string FA = e.CommandArgument.ToString();

            ItemDetails(FA);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup1",
             "$('#ItemModal').modal('show');", true);

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "popup",
            //    "$('#accessoriesModal').modal('show');", true);


        }

    }
    private void ItemDetails(string FA)
    {
        // Example Query (apne table ke hisab se change karo)
        string query = "select  [Item No],No_, Description,[Location Code],CASE  WHEN [Warranty Date] = '1753-01-01 00:00:00.000' THEN ''  ELSE CONVERT(VARCHAR(10), [Warranty Date], 103) END AS [Warranty Date],[Responsible Employee],[Serial No_],Brand,Model,[Purchase Order No],CASE  WHEN [Purchase Order Date] = '1753-01-01 00:00:00.000' THEN ''  ELSE CONVERT(VARCHAR(10), [Purchase Order Date], 103) END AS [Purchase Order Date],CASE  WHEN [Date of Installation] = '1753-01-01 00:00:00.000' THEN ''  ELSE CONVERT(VARCHAR(10), [Date of Installation], 103) END AS [Date of Installation],[Department Name]  from [NAAC_ADV_TEST].dbo.[TMU Hospital$Fixed Asset] where [Location Code]='" + FA + "' and Description!='' ";

        DataTable dt = new DataTable();


        using (SqlDataAdapter da = new SqlDataAdapter(query, con))
        {
            da.Fill(dt);
        }


        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void gvFA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewAccessories")
        {
            string FA = e.CommandArgument.ToString();
            
            BindFAWithAccessories(FA);
           
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "popup", "$('#accessoriesModal').modal('show');", true);
        }
        if (e.CommandName == "ViewEquipment")
        {
            

            string itemNo = e.CommandArgument.ToString();

            ViewState["ItemNo"] = itemNo;

            DataTable dt = GetReportDataDepartment(itemNo);

            if (dt.Rows.Count > 0)
            {
                // ITEM HEADER
                //lblItemNo.Text = dt.Rows[0]["ItemNo"].ToString();
                //lblPO.Text = dt.Rows[0]["PurchaseOrderNo"].ToString();
                //lblPurchase.Text = dt.Rows[0]["PurchaseDate"].ToString();
                //lblVendor.Text = dt.Rows[0]["PurchaseVendor"].ToString();

                // FA TABLE
                DataTable faTable = new DataTable();
                faTable.Columns.Add("EquipmentNo");
                faTable.Columns.Add("EquipmentName");
                faTable.Columns.Add("SerialNo");
                faTable.Columns.Add("LocationName");
                faTable.Columns.Add("InstallationDate");
                faTable.Columns.Add("AMCStart");
                faTable.Columns.Add("AMCEnd");

                var groups = dt.AsEnumerable()
                    .Where(x => x["EquipmentNo"] != DBNull.Value)
                    .GroupBy(x => x["EquipmentNo"].ToString());

                foreach (var g in groups)
                {
                    var first = g.First();

                    faTable.Rows.Add(
                        first["EquipmentNo"],
                        first["EquipmentName"],
                        first["SerialNo"],
                        first["LocationName"],
                        first["InstallationDate"],
                        first["AMCStart"],
                        first["AMCEnd"]
                    );
                }

                rptFA.DataSource = faTable;
                rptFA.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "Pop", "$('#detailsModal').modal('show');", true);
            }
        }
        
    }
    private void BindFAWithAccessoriesDepartment(string FA)
    {
        ViewState["ItemNo1"] = FA;
        string query = @"
    SELECT 
        FA.[No_] AS EquipmentNo,
        FA.[Description] AS EquipmentName,
        FA.[Serial No_] AS SerialNo,
        LOC.[Name] AS LocationName,
        FORMAT(FA.[Date of Installation],'dd-MM-yyyy') AS InstallationDate,

        AMC.[Begin Date] AS AMCStart,
        AMC.[End Date] AS AMCEnd,

        AMC_LINE.[Accessories],
        AMC_LINE.[Description] AS AccessoriesDesc,
        CAST(AMC_LINE.[Quantity] AS DECIMAL(18,2)) AS Quantity,
        CAST(AMC_LINE.[Approx Amount] AS DECIMAL(18,2)) AS Amount,
        FORMAT(AMC_LINE.[Repair Date],'dd-MM-yyyy') AS RepairDate

    FROM [NAAC_ADV_TEST].[dbo].[TMU Hospital$Fixed Asset] FA

    LEFT JOIN [NAAC_ADV_TEST].[dbo].[TMU Hospital$Location] LOC
        ON FA.[Location Code] = LOC.[Code]

    LEFT JOIN [NAAC_ADV_TEST].[dbo].[TMU Hospital$Equipment AMC-CMC Line] AMC
        ON FA.[No_] = AMC.[Equipment No_]

    LEFT JOIN [NAAC_ADV_TEST].[dbo].[TMU Hospital$AMC_CMC_WARRANTY_FOC Line] AMC_LINE
        ON FA.[No_] = AMC_LINE.[Equipment No_]

    WHERE FA.[No_] = @FA
    ";

        DataTable dt = new DataTable();

        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            cmd.Parameters.AddWithValue("@FA", FA);

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }
        }

        DataTable faTable = new DataTable();
        faTable.Columns.Add("EquipmentNo");
        faTable.Columns.Add("EquipmentName");
        faTable.Columns.Add("SerialNo");
        faTable.Columns.Add("LocationName");
        faTable.Columns.Add("InstallationDate");
        faTable.Columns.Add("AMCStart");
        faTable.Columns.Add("AMCEnd");

        var groups = dt.AsEnumerable()
            .Where(x => x["EquipmentNo"] != DBNull.Value)
            .GroupBy(x => x["EquipmentNo"].ToString());

        foreach (var g in groups)
        {
            var first = g.First();

            faTable.Rows.Add(
                first["EquipmentNo"],
                first["EquipmentName"],
                first["SerialNo"],
                first["LocationName"],
                first["InstallationDate"],
                first["AMCStart"],
                first["AMCEnd"]
            );
        }

        rptFA1.DataSource = faTable;
        rptFA1.DataBind();


    }
    private void BindFAWithAccessories(string FA)
    {
        ViewState["ItemNo1"] = FA;
        string query = @"
    SELECT 
        FA.[No_] AS EquipmentNo,
        FA.[Description] AS EquipmentName,
        FA.[Serial No_] AS SerialNo,
        LOC.[Name] AS LocationName,
        FORMAT(FA.[Date of Installation],'dd-MM-yyyy') AS InstallationDate,

        AMC.[Begin Date] AS AMCStart,
        AMC.[End Date] AS AMCEnd,

        AMC_LINE.[Accessories],
        AMC_LINE.[Description] AS AccessoriesDesc,
        CAST(AMC_LINE.[Quantity] AS DECIMAL(18,2)) AS Quantity,
        CAST(AMC_LINE.[Approx Amount] AS DECIMAL(18,2)) AS Amount,
        FORMAT(AMC_LINE.[Repair Date],'dd-MM-yyyy') AS RepairDate

    FROM [NAAC_ADV_TEST].[dbo].[TMU Hospital$Fixed Asset] FA

    LEFT JOIN [NAAC_ADV_TEST].[dbo].[TMU Hospital$Location] LOC
        ON FA.[Location Code] = LOC.[Code]

    LEFT JOIN [NAAC_ADV_TEST].[dbo].[TMU Hospital$Equipment AMC-CMC Line] AMC
        ON FA.[No_] = AMC.[Equipment No_]

    LEFT JOIN [NAAC_ADV_TEST].[dbo].[TMU Hospital$AMC_CMC_WARRANTY_FOC Line] AMC_LINE
        ON FA.[No_] = AMC_LINE.[Equipment No_]

    WHERE FA.[No_] = @FA
    ";

        DataTable dt = new DataTable();

        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            cmd.Parameters.AddWithValue("@FA", FA);

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }
        }

        DataTable faTable = new DataTable();
        faTable.Columns.Add("EquipmentNo");
        faTable.Columns.Add("EquipmentName");
        faTable.Columns.Add("SerialNo");
        faTable.Columns.Add("LocationName");
        faTable.Columns.Add("InstallationDate");
        faTable.Columns.Add("AMCStart");
        faTable.Columns.Add("AMCEnd");

        var groups = dt.AsEnumerable()
            .Where(x => x["EquipmentNo"] != DBNull.Value)
            .GroupBy(x => x["EquipmentNo"].ToString());

        foreach (var g in groups)
        {
            var first = g.First();

            faTable.Rows.Add(
                first["EquipmentNo"],
                first["EquipmentName"],
                first["SerialNo"],
                first["LocationName"],
                first["InstallationDate"],
                first["AMCStart"],
                first["AMCEnd"]
            );
        }

        rptFA1.DataSource = faTable;
        rptFA1.DataBind();

        
    }
    protected void rptFA1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
     
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string faNo = DataBinder.Eval(e.Item.DataItem, "EquipmentNo").ToString();

                DataTable dt = GetReportData1(ViewState["ItemNo1"].ToString());

                DataView dv = dt.DefaultView;
                dv.RowFilter = "EquipmentNo = '" + faNo + "'";

                Repeater rptAccessories = (Repeater)e.Item.FindControl("rptAccessories123");
                rptAccessories.DataSource = dv;
                rptAccessories.DataBind();
            }
        
    }
  
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=ItemDetailwithlocation.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        GridView1.AllowPaging = false;
        // Agar dubara bind karna ho to yaha LoadAccessories() call karo

        GridView1.RenderControl(hw);

        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    protected void GVAccess_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
}