<%@ Page Language="C#" AutoEventWireup="true" CodeFile="logout.aspx.cs" Inherits="logout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ashoka's University Human Resource Management</title>
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link href="css/mainpage.css" rel="stylesheet" type="text/css" />
    <link href="css/structure.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div >
    <table cellpadding="0px" cellspacing="0px" style="width:100%"> <tr> <td> 
    
    <table cellpadding="0px" cellspacing="0px" style="width:100%">  
    <tr> <td style="height:10px;background-color:White"> </td></tr>
     
    <tr> <td style="background-color:White"> 
    <div id="wrapHeader"> 
    <table cellpadding="0px" cellspacing="0px"> <tr> <td><asp:Image ID="Image1" runat="server" ImageUrl="~/logo/Logo.jpg" />   </td>  <td style="width:30px">  </td><td style="width:500px" align="center">
    <br /><br />


            <asp:Label ID="Label1" runat="server" Text="Human Resource Management" 
            Font-Size="17pt" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" 
            ForeColor="#093A62"></asp:Label>   </td> <td> <table cellpadding="0px" cellspacing="0px"> <tr> <td> 
            &nbsp;</td> <td>
                 </td> <td>&nbsp;&nbsp; <div id="lblName" runat="server"> </div></td></tr></table>  </td></tr></table>
     
    
    </div>
     
    </td></tr>
   <%-- <tr> <td style="height:10px;background-color:White"> </td></tr>--%>
    
    <tr> <td class="boxBodyInner"> </td></tr>

     <tr> <td style="height:200px;background-color:White"> </td></tr>
    
    <tr> <td align="center"> 
    <div id="wrap" style="background: url('logo/Bg.jpg') no-repeat center center fixed; -moz-background-size: cover;-webkit-background-size: cover;-o-background-size: cover;background-size: cover;"> 
    <table cellpadding="0px" cellspacing="0px"> <tr> <td> 
        <img src="logo/check.png" width="90px" height="100px"/> </td> <td>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="You have successfully Logged out" 
            Font-Size="17pt" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" 
            ForeColor="#093A62"></asp:Label>  </td> <td style="width:50px"> </td> <td> <br /> <br />  
                <asp:LinkButton ID="lnkLoginAgain" runat="server" 
            onclick="lnkLoginAgain_Click">Login Again</asp:LinkButton></td></tr></table>
    
    
    </div>
    
    
    </td></tr>



      <tr> <td class="ffooterInner"> 
    <div id="wrapFooter">
    
     <table cellpadding="0px" cellspacing="0px" style="width:100%">
      <tr> <td style="height:19px"> </td></tr>
      
       <tr> <td align="right"> <div id="Div1">    
      
      All right reserved By  : <a href="https://www.tmu.ac.in/" style="color:Black ; text-decoration:none" target="_blank"> Teerthanker Mahaveer University.</a>
      
      
         </div> </td></tr></table>
     </div>
    
    </td></tr>

    </table>



    
    
    </td></tr></table>
    </div>
    </form>
</body>
</html>
