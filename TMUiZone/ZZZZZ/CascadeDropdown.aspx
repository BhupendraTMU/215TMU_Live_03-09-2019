<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CascadeDropdown.aspx.vb" Inherits="ZZZZZ_CascadeDropdown" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptmanager1" runat="server"></asp:ScriptManager>
<div>
<table>
<tr>
<td>
Select Country:
</td>
<td>
<asp:DropDownList ID="ddlcountry" runat="server"></asp:DropDownList>
<asp:CascadingDropDown ID="ccdCountry" runat="server" Category="No_" TargetControlID="ddlcountry" PromptText="Select Country" LoadingText="Loading Countries.." 
    ServiceMethod="BindStateDetails" ServicePath="~/CascadingDropdown.asmx">
</asp:CascadingDropDown>
</td>
</tr>
<tr>
<td>
Select State:
</td>
<td>
<asp:DropDownList ID="ddlState" runat="server"></asp:DropDownList>
<asp:CascadingDropDown ID="ccdState" runat="server" Category="State" ParentControlID="ddlcountry" TargetControlID="ddlState" PromptText="Select State"
     LoadingText="Loading States.." ServiceMethod="BindStateDetails" ServicePath="~/CascadingDropdown.asmx">
</asp:CascadingDropDown>
</td>
</tr>
<tr>
<td>
Select Region:
</td>
<td>
<asp:DropDownList ID="ddlRegion" runat="server"></asp:DropDownList>
<%--<asp:CascadingDropDown ID="ccdRegion" runat="server" Category="Region" ParentControlID="ddlState" TargetControlID="ddlRegion" PromptText="Select Region" LoadingText="Loading Regions.." ServiceMethod="BindRegionDetails" ServicePath="CascadingDropdown.asmx">
</asp:CascadingDropDown>--%>
</td>
</tr>
</table>
</div>
    </form>
</body>
</html>
