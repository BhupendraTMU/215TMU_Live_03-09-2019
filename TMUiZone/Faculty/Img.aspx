<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Img.aspx.cs" Inherits="Faculty_Img" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
        function myFunction() {
            window.clos();
        }
</script>
</head>
<body onfocus="window.close()">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    Image Download in Progress.....
    </div>
    </form>
</body>
</html>
