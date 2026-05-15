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

public partial class ImageDownloadStud : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    DataUtility Objdu = new DataUtility();  DataTable DT;
    protected void Page_Load(object sender, EventArgs e)
    {
         DT = new DataTable();
         SqlCommand cmd = new SqlCommand("[proc_GetStudentsTransportListForImageDownload]", con); //proc_GetStudentsImage
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", '1');
        SqlDataAdapter da3 = new SqlDataAdapter(cmd);
        da3.Fill(DT);
        if (DT.Rows.Count > 0)
        {
            for (int i = 0; i <= DT.Rows.Count-1; i++)
            {
                string id = DT.Rows[i]["No_"].ToString();
                Response.Write("<script>window.open('Img.aspx?ID=" + id + "');</script>");             
                //Response.Write("<script>window.open('Img.aspx? ID = "+id+"','_blank', 'toolbar=0,location=1,menubar=0,resizable=0,width=250,height=200');</script>");             
            }
        }
        con.Close();
    }
}