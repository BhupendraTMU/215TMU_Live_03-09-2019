<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" CodeFile="StudentScrutinyreport.aspx.cs" Inherits="Student_StudentScrutinyreport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset id="Fieldset1" class="boxBodyInner" runat="server" style="width: 1000px; text-align: center">

        <%--  <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label">Sem/Year</label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlSem" CssClass="form-control" runat="server"  AutoPostBack="true">
                                  
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>--%>

        <div align="center">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" SizeToReportContent="true" AsyncRendering="true" CssClass="active" Border="Solid"></rsweb:ReportViewer>
            <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
        </div>
    </fieldset>
</asp:Content>

