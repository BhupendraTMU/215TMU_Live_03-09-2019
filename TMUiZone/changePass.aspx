<%@ Page Language="C#" AutoEventWireup="true" CodeFile="changePass.aspx.cs" Inherits="changePass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link href="css/mainpage.css" rel="stylesheet" type="text/css" />
    <link href="dropdown_one.css" rel="stylesheet" type="text/css" />
     <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link href="css/structure.css" rel="stylesheet" type="text/css" />
</head>
<body >
    <form id="form1" runat="server">
    <div>

        <table cellpadding="0px" cellspacing="0px" style="width:100%"> 
               <tr> <td style="height:10px"> </td></tr>
            <tr> <td>  


             <div id="wrapHeader"> 
    <table cellpadding="0px" cellspacing="0px"> <tr> <td><asp:Image ID="Image1" runat="server" ImageUrl="~/logo/Logo.jpg" />   </td>  <td style="width:30px">  </td><td style="width:400px" align="center">
    <br />
    

        <asp:Label ID="Label2" runat="server" Text="Human Resource Management" 
            Font-Size="17pt" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" 
            ForeColor="#093A62"></asp:Label>   </td> <td> 
            <table cellpadding="0px" cellspacing="0px" style="width:100%"> 
            <tr> <td> 
        <asp:Image ID="imgProfile" runat="server" Width="40px"  Height="40px" 
           /> </td> <td>&nbsp;&nbsp; Welcome  
                 </td> <td>&nbsp;&nbsp;
                     <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                 </td> <td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:LinkButton ID="lnkLogout" runat="server" OnClick="lnkLogout_Click">Logout</asp:LinkButton></td></tr></table>  </td></tr></table>
     
    
    </div>



                                                         </td></tr>
             <tr> <td style="height:10px"> </td></tr>
             <tr> <td style="background-color:#C5122F ;height:10px"> </td></tr>

            <tr> <td style="background-color:#FFFFCC ;height:530px"> 
               
                 <div class="box login" > 
<table cellpadding="0px" cellspacing="0px"> <tr> <td> 


  <asp:Panel ID="Panel1" runat="server">

    <table cellpadding="0px" cellspacing="0px"> 
    
    <tr> <td style="background-color:#E0F2F7;height:45px;"> 
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" 
            Text="Change Password" Font-Size="Large" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label></td></tr>

    <tr> <td>
    
    
    
    
	<fieldset class="boxBody">

 
  
        

	  <label>Old Password </label>
        &nbsp;<asp:TextBox ID="txtOldPass" runat="server" placeholder="Old Password" required TextMode="Password" ></asp:TextBox>
        <%--<input type="text" tabindex="1" placeholder="PremiumPixel" required>--%>
	  <label>New Password</label>
       <asp:TextBox ID="txtNewPassword" runat="server" placeholder="Password" required 
            TextMode="Password"></asp:TextBox>

       <label>Confirm Password</label>
       <asp:TextBox ID="txtCPassword" runat="server" placeholder="Password" required 
            TextMode="Password"></asp:TextBox>
        
        <%-- <input type="password" tabindex="2" required>--%>
	</fieldset>
	<footer>
	 
	    <%-- <input type="submit" class="btnLogin" value="Login" tabindex="4">--%>
     <asp:Button ID="btnChangePassword" runat="server" class="btnLogin" Text="Change Password" OnClick="btnChangePassword_Click"></asp:Button>
<asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password Not Match" ControlToCompare="txtNewPassword" ControlToValidate="txtCPassword" ForeColor="#CC3300" Font-Bold="False"></asp:CompareValidator>
       
	</footer>
        
    
     </td></tr>
    
    
    
    
    
    
    
    
    </table>

    </asp:Panel>




 </td></tr></table>
 </div>
                   
                 </td></tr>
          


             <tr> <td class="ffooterInner"> 
    <div id="wrapFooter">
    
     <table cellpadding="0px" cellspacing="0px" style="width:100%">
      <tr> <td style="height:19px"> </td></tr>
      
       <tr> <td > <div id="Div1">    
      
      <table cellpadding="0px" cellspacing="0px" style="width:100%"><tr> <td> <table cellpadding="0px" cellspacing="0px">  <tr><td> <label> Company Name :</label></td> <td style="width:20px"> </td> <td>
          <asp:Label ID="lblCompanyName" runat="server" Text=""></asp:Label> </td>  <td style="width:50px"> </td><td><label>Employee ID</label> </td><td style="width:20px"> </td> <td>  <asp:Label ID="lbluserid666" runat="server" Text=""></asp:Label></td> </tr></table>   </td> <td align="right">   All right reserved By  : <a href="https://www.tmu.ac.in/" style="color:Black ; text-decoration:none" target="_blank"> Teerthanker Mahaveer University.</a> </td></tr> </table>

   
      
      
         </div> </td></tr></table>
     </div>
    
    </td></tr>
        </table>



       



             </div>
   
    </form>
</body>
</html>
