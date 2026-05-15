<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test1Img.aspx.cs" Inherits="Test1Img" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table style="padding-left: 15px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Image ID="Image12" runat="server" Height="147px" Width="157px" BorderStyle="Double" /><br />
                                                        </td>
                                                        <td valign="top" align="left">
                                                            <h2>
                                                                <asp:Label ID="lblName" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label></h2>
                                                            <br />
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                           
                                                            <asp:Button ID="btnRemove" runat="server" Font-Size="XX-Small" 
                                                                OnClick="btnRemove_Click"  
                                                                Text="Remove" Width="70px" />
                                                            <asp:FileUpload ID="fileuploadImage" runat="server" Width="120px" Font-Size="XX-Small"
                                                                Style="color: transparent; direction: rtl;" /><asp:Button ID="btnUpload" runat="server"
                                                                    Text="Upload" OnClick="btnUpload_Click" Width="70px" Font-Size="XX-Small" OnClientClick="document.forms[0].target = '_self';" />
                                                           
                                                        </td>
                                                    </tr>
                                                </table>
    </div>
    </form>
</body>
</html>
