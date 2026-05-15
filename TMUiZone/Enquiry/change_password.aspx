<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="change_password.aspx.cs" Inherits="change_password" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/structure.css" rel="stylesheet" type="text/css" />
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />     
    <center>
    <div class="box" > 
<table cellpadding="0px" cellspacing="0px" style="background-color:#ACE9FB;"> <tr> <td> 
  <asp:Panel ID="Panel1" runat="server" >
    <table cellpadding="0px" cellspacing="0px" >
    <tr> <td style="background-color:#ACE9FB;height:45px;"> 
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" 
            Text="Change Password" Font-Size="Large" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label></td></tr>
        <tr>
            <td>
                <fieldset class="boxBody">

                    <label>Old Password </label>
                  <%--  &nbsp;<asp:TextBox ID="txtOldPass" runat="server" placeholder="Old Password" required TextMode="Password" ValidationGroup="vgCP"></asp:TextBox>--%>
                      &nbsp;<asp:TextBox ID="txtOldPass" runat="server" placeholder="Old Password" TextMode="Password" ValidationGroup="vgCP"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqtxtOldPass" runat="server" ControlToValidate="txtOldPass"  ErrorMessage="*" ForeColor="Red" SetFocusOnError="true" ValidationGroup="vgCP"></asp:RequiredFieldValidator>
                    <label>New Password</label>
                    <asp:TextBox ID="txtNewPassword" runat="server" placeholder="Password" TextMode="Password" ValidationGroup="vgCP"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqTxtNewPwd" runat="server" ControlToValidate="txtNewPassword" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true" ValidationGroup="vgCP"></asp:RequiredFieldValidator>
                    <label>Confirm Password</label>
                    <asp:TextBox ID="txtCPassword" runat="server" placeholder="Password" TextMode="Password" ValidationGroup="vgCP"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCPassword" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true" ValidationGroup="vgCP"></asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" ControlToValidate="txtCPassword" ControlToCompare="txtNewPassword" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true" ValidationGroup="vgCP"></asp:CompareValidator>
                    
                </fieldset>
                <footer style="background-color:#ACE9FB;">	
                    <center>
     <asp:Button ID="btnChangePassword" runat="server" class="btnLogin" Text="Change Password" OnClick="btnChangePassword_Click" ValidationGroup="vgCP" width="220px" ></asp:Button>
                        <br />
                        </center>   
	</footer>

            </td>

        </tr>
    
    </table>
    </asp:Panel>
 </td></tr></table>
 </div>
        </center>
</asp:Content>

