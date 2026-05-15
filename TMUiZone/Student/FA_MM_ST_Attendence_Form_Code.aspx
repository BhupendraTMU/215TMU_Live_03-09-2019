<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" CodeFile="FA_MM_ST_Attendence_Form_Code.aspx.cs" Inherits="Student_FA_MM_ST_Attendence_Form_Code" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    <!-- Report Viewer Section -->
    <h1 style="text-align:center">Attendance Report</h1>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server"></rsweb:ReportViewer>
</asp:Content>

