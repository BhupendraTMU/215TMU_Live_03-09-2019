<%@ Page Language="C#" AutoEventWireup="true" CodeFile="furl.aspx.cs" Inherits="furl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
      <div align="right" >
     <asp:LinkButton ID="LinkButtonPayFee" runat="server" 
                         onclick="LinkButtonPayFee_Click">Back</asp:LinkButton> &nbsp;&nbsp;
                          
     </div>
     <div align="center">
     
     
<asp:label runat="server" text="Label" ID="LabelError"></asp:label>
     
     </div>
    </form>
</body>
</html>