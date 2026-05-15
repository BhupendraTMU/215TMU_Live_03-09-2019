<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="changePwd.aspx.cs" Inherits="changePwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div >
<p class="Navigator">
        Change Password</p>
</div>    
<table cellpadding="0px" cellspacing="0px" width="830px" >
<tr><td colspan="2" style="height:10px"></td></tr>
<tr>
<td align="right">
<asp:Label ID="lbloldpass" Text="Old Password" runat="server"></asp:Label>
 &nbsp;&nbsp;&nbsp;
 </td>
 <td> <asp:TextBox ID="txtoldpassword" runat="server"></asp:TextBox></td>
 
</tr>
<tr><td colspan="2" style="height:10px"></td></tr>
<tr> 
<td align="right"><asp:Label ID="lblnewpass" runat="server" Text="New Password"></asp:Label>&nbsp;&nbsp;&nbsp; </td>
<td><asp:TextBox ID="txtnewpass" runat="server"></asp:TextBox> </td>

</tr>
<tr><td colspan="2" style="height:10px"></td></tr>
<tr> 
<td align="right"><asp:Label ID="lblconfirmpass" runat="server" Text="Confirm Password" ></asp:Label>&nbsp;&nbsp;&nbsp; </td>
<td><asp:TextBox ID="txtconfirmpass" runat="server"></asp:TextBox> </td>
</tr>

<tr><td colspan="2" style="height:10px"></td></tr>
<tr><td colspan="2" align="right">  
     
    <asp:Button ID="btnchange" runat="server" Text="Change Password" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>
</table>
</asp:Content>

