using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;



using System.Collections.Generic;
using System.ComponentModel;

using System.Drawing;
using System.Linq;
using System.Text;

using System.Xml.Linq;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Security.Cryptography;

public partial class change_password : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal con;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["uname"].ToString() == null || Session["Company"].ToString() == null)
            {

                Response.Redirect("../Default.aspx");
            }
            else
            {
                Portalcon = new Connection();
                con = new ServicePoratal();
               // Label2.Text = Encrypt(Session["Passw"].ToString());
              //  Label3.Text =Decrypt( Session["Passw"].ToString());
              
            }

        }
        catch (Exception)
        {
            Response.Redirect("../Default.aspx");
        }
    }

    private static string sKey = "UJYHCX783her*&5@$%#(MJCX**38n*#6835ncv56tvbry(&#MX98cn342cn4*&X#&";

    public static string Encrypt(string sPainText)
    {
        if (sPainText.Length == 0)
            return (sPainText);
        return (EncryptString(sPainText, sKey));
    }
    public static string Decrypt(string sEncryptText)
    {
        if (sEncryptText.Length == 0)
            return (sEncryptText);
        return (DecryptString(sEncryptText, sKey));
    }
    protected static string EncryptString(string InputText, string Password)
    {

        RijndaelManaged RijndaelCipher = new RijndaelManaged();

        byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(InputText);

        byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());

        PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);

        ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(16), SecretKey.GetBytes(16));

        MemoryStream memoryStream = new MemoryStream();

        CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);

        cryptoStream.Write(PlainText, 0, PlainText.Length);

        cryptoStream.FlushFinalBlock();

        byte[] CipherBytes = memoryStream.ToArray();

        memoryStream.Close();
        cryptoStream.Close();

        string EncryptedData = Convert.ToBase64String(CipherBytes);

        return EncryptedData;
    }
    protected static string DecryptString(string InputText, string Password)
    {
        try
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            byte[] EncryptedData = Convert.FromBase64String(InputText);
            byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);

            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(16), SecretKey.GetBytes(16));
            MemoryStream memoryStream = new MemoryStream(EncryptedData);

            CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

            byte[] PlainText = new byte[EncryptedData.Length];

            int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
            memoryStream.Close();
            cryptoStream.Close();

            string DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);

            return DecryptedData;
        }
        catch (Exception exception)
        {
            return (exception.Message);
        }
    }
    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        //string OldPass = Decrypt(txtOldPass.Text);
        //string NewPass = Encrypt(txtNewPassword.Text);

        if (Session["Passw"].ToString() == txtOldPass.Text)
        {
            Portalcon.Update_Employee_Password_Change_Status("1", txtNewPassword.Text, Session["CompanyTableEmployee"].ToString(), Session["uid"].ToString());
            Portalcon.DisConnect();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Password changed successfully');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Old Password is not correct');", true);
        }
    }
}