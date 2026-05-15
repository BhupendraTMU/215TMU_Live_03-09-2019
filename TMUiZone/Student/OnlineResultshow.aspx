<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" CodeFile="OnlineResultshow.aspx.cs" Inherits="Student_OnlineResultshow" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">




    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


    <asp:Panel ID="pnlmessag" runat="server" Visible="false">
        <asp:TextBox ID="txtmsg" runat="server" TextMode="MultiLine">
        </asp:TextBox>
        <asp:Button ID="btnSendmsg" runat="server" Text="Send" OnClick="btnSendmsg_Click"/>


    </asp:Panel>

    <asp:Panel ID="Panel1" runat="server" Visible="false">
    <asp:Label ID="msg" runat="server" Text="You are not authorized to access this page" Font-Bold="true" Visible="false"></asp:Label>
    <fieldset id="Fieldset1" class="boxBodyInner" runat="server" style="width: 1000px; text-align: center">





        <div class="form-group clearfix">
            <table>
                <tr>


                    <td valign="top">
                        <label id="lblsem" style="font-weight: bold">Sem/Year</label>
                    </td>
                    <td style="width: 20px"></td>
                    <td>
                        <asp:DropDownList ID="ddlSem" Width="100px" runat="server"  >
                        </asp:DropDownList>
                    </td>
                    <td style="width: 20px"></td>
                    <td valign="top">
                        <label id="lblExam" style="font-weight: bold">Exam Type</label>
                    </td>
                    <td style="width: 20px"></td>
                    <td>
                        <asp:DropDownList ID="drpExam" Width="100px" runat="server" OnSelectedIndexChanged="drpExam_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="Main" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Re-Appear" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 20px"></td>
                    <td valign="top" id="Academic">
                        <label id="lblAcademic" style="font-weight: bold" runat="server" visible="false">Academic Year</label>
                    </td>
                    <td style="width: 20px"></td>
                    <td>
                        <asp:DropDownList ID="drpAcademic" Width="200px" runat="server" Visible="false" >
                        </asp:DropDownList>
                    </td>
                     <td style="width: 20px"></td>
                    <td>
                        <asp:Button ID="btnView" Width="100px" runat="server" Text="View Report" OnClick="btnView_Click" >
                        </asp:Button>
                    </td>
                </tr>
            </table>




        </div>


        <div align="center">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" SizeToReportContent="true" AsyncRendering="true" CssClass="active" Border="Solid"></rsweb:ReportViewer>
            <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
        </div>
    </fieldset>
        </asp:Panel>
</asp:Content>

