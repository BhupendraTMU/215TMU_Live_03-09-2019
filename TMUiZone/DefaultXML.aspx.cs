using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class DefaultXML : System.Web.UI.Page
{
    string DocketXMLString;
    ClassXML xmlclass = new ClassXML();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        xmlclass.int1 = 10;
        xmlclass.int2 = 20;
        System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(xmlclass.GetType());
        System.IO.MemoryStream stream = new System.IO.MemoryStream();
        x.Serialize(stream, xmlclass);
        stream.Position = 0;
        XmlDocument xd = new XmlDocument();
        xd.Load(stream);
        DocketXMLString = xd.InnerXml;
        DocketXMLString = DocketXMLString.Replace("&", "&amp;");

        SqlCommand cmd1 = new SqlCommand("sp_XMLData", con);
        con.Open();
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.AddWithValue("@test", DocketXMLString);
        

        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();

        da1.Fill(dt);
        con.Close();
       // return DocketXMLString;

    }
}