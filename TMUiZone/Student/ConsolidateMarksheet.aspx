<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" CodeFile="ConsolidateMarksheet.aspx.cs" Inherits="Student_ConsolidateMarksheet" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
           <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <fieldset id="Fieldset1" class="boxBodyInner" runat="server" style="width: 1000px; text-align: center">
        <div align="center">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" SizeToReportContent="true" AsyncRendering="true" CssClass="active" Border="Solid"></rsweb:ReportViewer>
            <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
        </div>
    </fieldset>
</asp:Content>

