using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Configuration;
using Utility;

public partial class Image : System.Web.UI.Page
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    DataUtility Objdu = new DataUtility();    
    DataTable DT;
    protected void Page_Load(object sender, EventArgs e)
    {   
        if (Request.QueryString["ImageID"] != null)
        {
            string id =Request.QueryString["ImageID"].ToString();
            DT = new DataTable();
            DT =Objdu.GetDataTableProc("proc_GetImageByEnquiryId", id);             
            if (DT.Rows.Count > 0)
            {
                Byte[] bytes = (Byte[])DT.Rows[0]["StudentImage"];
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType =DT.Rows[0]["StudentImage"].ToString();
                Response.AddHeader("content-disposition", "attachment;filename=''");
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
            else
            {
            }

        }
    }
}