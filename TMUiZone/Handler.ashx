<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Configuration;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;

public class Handler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string empno;
        if (context.Request.QueryString["id"] != null)
            empno = context.Request.QueryString["id"];
        else
            throw new ArgumentException("No parameter specified");

        context.Response.ContentType = "image/jpeg";
        Stream strm = ShowEmpImage(empno);
        byte[] buffer = new byte[4096];
        int byteSeq = strm.Read(buffer, 0, 4096);

        while (byteSeq > 0)
        {
            context.Response.OutputStream.Write(buffer, 0, byteSeq);
           // byteSeq = strm.Read(buffer, 0, 4096);
        }
        context.Response.BinaryWrite(buffer);
    }
    public Stream ShowEmpImage(string empno)
    {
        string conn = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
        SqlConnection connection = new SqlConnection(conn);
        string sql = "select [Student Image] as StudentImage from [Ashoka University$Student - COLLEGE] where No_= @ID";
        SqlCommand cmd = new SqlCommand(sql, connection);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@ID", empno);
        connection.Open();
        object img = cmd.ExecuteScalar();
        try
        {
            return new MemoryStream((byte[])img);
        }
        catch
        {
            return null;
        }
        finally
        {
            connection.Close();
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }


}