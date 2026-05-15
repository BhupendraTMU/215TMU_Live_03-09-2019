<%@ WebHandler Language="C#" Class="DisplayImg" %>

using System;
using System.Web;
using Utility;
using System.IO;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public class DisplayImg : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.Clear();
        context.Response.ContentType = "image/jpeg";
        if (context.Request.QueryString["id"] != null)
        {
            String imgId = "0";
            imgId = context.Request.QueryString["id"].ToString();
            MemoryStream memoryStream = new MemoryStream(GetImageFromDB(imgId), false);
            //MemoryStream((byte[])dt.Rows[0][0]);
           // System.Drawing.Image imgFromDataBase = System.Drawing.Image.FromStream(memoryStream);
           // imgFromDataBase.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
           
            byte[] buffer = new byte[2048];
            int byteSeq = memoryStream.Read(buffer, 0, 2048);
            while (byteSeq > 0)
            {
                context.Response.OutputStream.Write(buffer, 0, byteSeq);
                byteSeq = memoryStream.Read(buffer, 0, 2048);
            }
        }
    }
    private byte[] GetImageFromDB(string ImgId)
    {
        string strCon = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;        
        SqlDataAdapter SqlAda;
        DataSet ds;
        byte[] btImage = null;
        using (SqlConnection Sqlcon = new SqlConnection(strCon))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                Sqlcon.Open();
                cmd.Connection = Sqlcon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "proc_GetImageById";
                cmd.Parameters.AddWithValue("ID", ImgId);// cmd.Parameters["ID"].Value = ImgId;                
                SqlAda = new SqlDataAdapter(cmd);
                ds = new DataSet();
                SqlAda.Fill(ds);
                btImage = (byte[])ds.Tables[0].Rows[0][0];
            }
        }
        return btImage;
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}