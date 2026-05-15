<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Payment.aspx.cs" Inherits="Payment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtamount" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtid" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        <asp:Button ID="btnSave" runat="server" Text="PAY" />
    </div>
    </form>
</body>
</html>
