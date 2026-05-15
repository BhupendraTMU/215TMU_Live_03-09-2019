<%@ Page Title="" Language="C#" MasterPageFile="~/IndexMaster.master" AutoEventWireup="true" CodeFile="erp1.aspx.cs" Inherits="erp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <fieldset class="boxBody">
 <asp:Label ID="Label1" runat="server" 
            Text="ERP" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 </fieldset>


    <fieldset class="boxBody"> 

<table cellpadding="0px" cellspacing="0px">  <tr>  <td  style="width:10px">  </td><td  style="width:200px" valign="top"> 

<table cellpadding="0px" cellspacing="0px" class="leftbg1" style="width:200px; height:430px">  <tr> <td style="width:10px"> </td> <td>


<table cellpadding="0px" cellspacing="0px" >
 <tr> <td style="height:10px"> </td></tr>
 <tr> <td class="leftmMenu">  <img src="logo/Star.png" /> 
    <asp:LinkButton ID="lnkAshokatesting" runat="server" OnClientClick="window.open('http://10.1.4.52:8080/DynamicsNAV71/webclient', '');"
        >Ashoka Testing</asp:LinkButton></td></tr>
    <tr> <td style="height:10px"> </td></tr>
     <tr> <td class="leftmMenu">   <img src="logo/Star.png" />
    <asp:LinkButton ID="lnkAshokalive" runat="server" 
        OnClientClick="window.open('http://10.1.4.52:8080/DynamicsNAV71/webclient', '');">Ashoka University - Live</asp:LinkButton></td></tr>
     

     <tr> <td style="height:10px"> </td></tr>
     <tr> <td class="leftmMenu">   <img src="logo/Star.png" />
    <asp:LinkButton ID="lnkIFRE" runat="server"  OnClientClick="window.open('http://10.1.4.52:8080/DynamicsNAV71/webclient', '');">IFRE</asp:LinkButton></td></tr>
    </table>
 </td> <td style="width:10px"> </td></tr></table>



    
</td>  <td style="width:30px">  </td> <td valign="top"> 
    


 

</td></tr></table>

</fieldset>

</asp:Content>

