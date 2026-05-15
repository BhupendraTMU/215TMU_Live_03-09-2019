using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_Img : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        
        DataTable dt = new DataTable();
        string ID = "1";
        ID=Request.QueryString["ID"];
        SqlCommand cmd = new SqlCommand("proc_GetStudentsListforImageByID", con);//proc_GetStudentsImage
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", ID);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        if (dt.Rows.Count == 1)
        {
            string fileName; string ext1 = ".png";
            Byte[] bytes = (Byte[])dt.Rows[0]["Img"];
            fileName = ID + ext1; // fileName = dt.Rows[0]["No_"].ToString() + ext1;
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = dt.Rows[0]["No_"].ToString();
            Response.AddHeader("content-disposition", "attachment;filename='" + fileName);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
            lblMsg.Text = "................";
            Response.Write("<script>widows.close()</script>");
        }
    }

}