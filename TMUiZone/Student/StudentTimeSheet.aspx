<%@ Page Title="" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" CodeFile="StudentTimeSheet.aspx.cs" Inherits="Student_TimeSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .Bold {
            font-weight: bold;
            background-color: #81C125;
        }

        .Color {
            background-color: #c1c1c1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
        <asp:Label ID="Label3" runat="server"
            Text="Time Sheet" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
    </fieldset>
    <fieldset class="boxBodyHeader">
    </fieldset>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <fieldset style="background: #fefefe; border-top: 1px solid #dde0e8; border-bottom: 1px solid #dde0e8; padding: 10px 20px; height: 100%">
                <center>
                    <b style="font-weight: bold">Course:</b>&nbsp<asp:Label ID="lblCourse" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;
    <b style="font-weight: bold">Semester/Year:</b>&nbsp<asp:Label ID="lblSemester" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;
    <b style="font-weight: bold">Section:</b>&nbsp<asp:Label ID="lblSection" runat="server" Text="Label"><br /></asp:Label>&nbsp;&nbsp;
    <b style="font-weight: bold">Date:</b>&nbsp<asp:Label ID="lblDate" runat="server" Text="Label"><br /></asp:Label>
                </center>
                <br />
                <br />
                <div class="pull-right">
                    <b style="font-weight: bold">
                        <table>
                            <tr>
                                <td>
                                    <label>From Date  </label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFromDate" runat="server" Width="150px" Height="22px" onkeypress="return false" onKeyDown="preventBackspace();"
                                        oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender2" Format="dd MMM yyyy" runat="server"
                                        CssClass="cal_Theme1" Enabled="true" TargetControlID="txtFromDate">
                                    </asp:CalendarExtender>

                                </td>
                                <td style="width: 20px"></td>
                                <td>
                                    <label>To Date </label>
                                </td>

                                <td>
                                    <asp:TextBox ID="txtToDate" runat="server" Width="150px" Height="22px" onkeypress="return false" onKeyDown="preventBackspace();"
                                        oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" Format="dd MMM yyyy" runat="server"
                                        CssClass="cal_Theme1" Enabled="true" TargetControlID="txtToDate">
                                    </asp:CalendarExtender>

                                </td>
                                 <td style="width: 20px"></td>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-success" Width="100px" OnClick="btnSearch_Click" />
                                </td>
                                 <td style="width: 20px"></td>
                                <td>
                                    <label>Faculty: </label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="drpFacultyName" AutoPostBack="true" Width="200px" runat="server" OnSelectedIndexChanged="drpFacultyName_SelectedIndexChanged"></asp:DropDownList>
                                </td>

                            </tr>
                        </table>
                </div>
                <div class="clearfix"></div>
                <br />
                <asp:Table ID="timeTable" runat="server" CssClass="table table-bordered"></asp:Table>

            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

