<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
    <asp:ChangePassword ID="ChangePassword1" runat="server" OnChangingPassword="OnChangingPassword"
    RenderOuterTable="false" NewPasswordRegularExpression="^[\s\S]{5,}$" NewPasswordRegularExpressionErrorMessage="Password must be of minimum 5 characters." CancelDestinationPageUrl = "~/Default.aspx">
</asp:ChangePassword>
<br />
<asp:Label ID="lblMessage" runat="server" />
    </div>

</asp:Content>

