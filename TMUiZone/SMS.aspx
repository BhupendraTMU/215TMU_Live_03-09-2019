<%@ Page Title="" Language="C#" MasterPageFile="~/MasterContent.master" AutoEventWireup="true" CodeFile="SMS.aspx.cs" Inherits="SMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:TextBox ID="txtMobileNo" runat="server"></asp:TextBox><asp:TextBox ID="txtMsg" runat="server"></asp:TextBox><asp:Button ID="btnSend" runat="server" Text="Button" OnClick="btnSend_Click" />
</asp:Content>

