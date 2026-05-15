<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LanguageHindi.aspx.cs" Inherits="LanguageHindi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
     <form id="form1" runat="server">
    <div>
    
        <table class="style1">
            <caption>
                <asp:Label ID="lblName" runat="server" Font-Bold="True" Font-Size="Large" 
                    ForeColor="#FF66FF" Text="हिंदी"></asp:Label>
                <asp:Label ID="lblheader" runat="server" Font-Bold="True" Font-Size="Large" 
                    ForeColor="#FF66FF" Text="Label"></asp:Label>
            </caption>
            <tr>
                <td align="right">
                    <asp:Label ID="lblabtme" runat="server" Font-Bold="True" ForeColor="#0066FF" 
                        Text="Label"></asp:Label>
                </td>
                <td>
                    &nbsp;
                    <asp:Label ID="lbldesc" runat="server" Font-Bold="True" Text="Label"></asp:Label>
                    &nbsp;</td>
            </tr>
            <tr>   
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                        Text="English" />
&nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Hindi" />
&nbsp;&nbsp;
                    <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
                        Text="Marathi" />
                    <br />
                </td>
            </tr>
        </table>
    <asp:GridView runat="server" ID="grd" AutoGenerateColumns="true"></asp:GridView>
    </div>
    </form>
</body>
</html>
