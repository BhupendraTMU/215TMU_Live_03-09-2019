<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="EditTimeTable.aspx.cs" Inherits="Faculty_EditTimeTable" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .upCase {
            text-transform: uppercase;
        }

        .myTableClass tr th {
            padding: 1px;
        }

        tr td {
            padding: 1px;
        }



        .style1 {
            height: 83px;
        }
    </style>
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        .Grid td {
            background-color: #A1DCF2;
            color: black;
            font-size: 10pt;
            line-height: 200%;
        }

        .Grid th {
            background-color: #3AC0F2;
            color: White;
            font-size: 10pt;
            line-height: 200%;
        }

        .ChildGrid td {
            background-color: #eee !important;
            color: black;
            font-size: 10pt;
            line-height: 200%;
        }

        .ChildGrid th {
            background-color: #6C6C6C !important;
            color: White;
            font-size: 10pt;
            line-height: 200%;
        }

        .sendbtn {
            display: block;
            font-size: 26px;
            padding: 3px 6px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Edit Time Table" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <fieldset class="boxBodyHeader">
    </fieldset>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <fieldset class="boxBodyInner">

                <table cellpadding="0px" cellspacing="0px">
                    <caption>
                        <tr>
                            <td>From Date :
                            </td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:TextBox ID="txtFromDate" runat="server" onkeydown="return false;"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate" Format="dd MMM yyyy">
                                </cc1:CalendarExtender>
                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" WatermarkText="dd MMM yyyy" TargetControlID="txtFromDate">
                                </cc1:TextBoxWatermarkExtender>
                            </td>
                            <td style="width: 20px"></td>
                            <td>To Date :
                            </td>
                            <td style="width: 10px"></td>

                            <td>
                                <asp:TextBox ID="txtToDate" runat="server" onkeydown="return false;"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate" Format="dd MMM yyyy">
                                </cc1:CalendarExtender>
                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" WatermarkText="dd MMM yyyy" TargetControlID="txtToDate">
                                </cc1:TextBoxWatermarkExtender>
                            </td>
                            <td style="width: 20px"></td>
                            <td>Course :
                            </td>
                            <td style="width: 10px"></td>

                            <td>
                                <asp:DropDownList ID="drpCourseCode" AutoPostBack="true" Width="170px" Height="30px" runat="server" OnSelectedIndexChanged="drpCourseCode_SelectedIndexChanged"></asp:DropDownList>
                            </td>

                            <td style="width: 20px"></td>
                            <td>Faculty :
                            </td>
                            <td style="width: 10px"></td>

                            <td>
                                <asp:DropDownList ID="drpFaculty" runat="server" Width="170px" Height="30px"></asp:DropDownList>
                            </td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:Button ID="btnview" Text="View" OnClick="btnview_Click" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HiddenField ID="hdfEntryNo" runat="server" />
                                <asp:HiddenField ID="AcYear" runat="server" />
                                <asp:HiddenField ID="FacultyCode" runat="server" />
                                <asp:HiddenField ID="hfHour" runat="server" />
                                <asp:HiddenField ID="hfRoom" runat="server" />
                                <asp:HiddenField ID="hfSubject" runat="server" />
                                <asp:HiddenField ID="hfSem" runat="server" />
                                <asp:HiddenField ID="hfSec" runat="server" />
                                <asp:HiddenField ID="hfCourse" runat="server" />
                                <asp:HiddenField ID="hfDay" runat="server" />
                            </td>
                        </tr>
                         <tr id="ModifyTR" runat="server" visible="false">
                            <td>Faculty :
                            </td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:DropDownList ID="drpFaculty1" runat="server" Width="170px" Height="30px" AutoPostBack="true" OnSelectedIndexChanged="drpFaculty1_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td style="width: 20px"></td>
                             <td>Section :
                            </td>
                            <td style="width: 10px"></td>

                            <td>
                                <asp:DropDownList ID="drpSection" runat="server" Width="170px" Height="30px"></asp:DropDownList>
                            </td>
                            
                            <td style="width: 20px"></td>
                            <td>Room : 
                            </td>
                            <td style="width: 10px"></td>

                            <td>
                                <asp:DropDownList ID="drpRoom" AutoPostBack="true" Width="50px" Height="30px" runat="server" >
                                     
                                </asp:DropDownList>
                             &nbsp &nbsp&nbsp&nbsp&nbsp  Hour : <asp:DropDownList ID="drpHour" AutoPostBack="true" Width="50px" Height="30px" runat="server" >

                                 
                             <asp:ListItem Value="1" Text="1"></asp:ListItem>
                             <asp:ListItem Value="2" Text="2"></asp:ListItem>
                             <asp:ListItem Value="3" Text="3"></asp:ListItem>
                             <asp:ListItem Value="4" Text="4"></asp:ListItem>
                             <asp:ListItem Value="5" Text="5"></asp:ListItem>
                             <asp:ListItem Value="6" Text="6"></asp:ListItem>
                             <asp:ListItem Value="7" Text="7"></asp:ListItem>
                             <asp:ListItem Value="8" Text="8"></asp:ListItem>
                             <asp:ListItem Value="9" Text="9"></asp:ListItem>
                             <asp:ListItem Value="10" Text="10"></asp:ListItem>   
                                                                </asp:DropDownList>
                            </td>

                            <td style="width: 20px"></td>
                            <td>Subject Code :
                            </td>
                            <td style="width: 10px"></td>

                            <td>
                                 <asp:DropDownList ID="drpSubject" runat="server" Width="170px" Height="30px"></asp:DropDownList>
                            </td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:Button ID="btnUpdate" Text="Update" OnClick="btnUpdate_Click" runat="server" />
                            </td>
                        </tr>

                       
                </table>
                </caption>
            </fieldset>
            <fieldset>
                <asp:GridView ID="grdData" runat="server" BackColor="White" DataKeyNames="Entry No_" EmptyDataText="There are no data records to display." CssClass="myTableClass" AutoGenerateColumns="false" ShowFooter="true" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" GridLines="Horizontal">
                    <AlternatingRowStyle BackColor="LightBlue" />
                    <Columns>
                        <asp:TemplateField HeaderText="Entry No">
                            <ItemTemplate>
                                <asp:Label ID="lblEntryNo" runat="server" Text='<%# Eval("Entry No_") %>' />
                                <asp:HiddenField ID="hfYear" runat="server" Value='<%# Eval("Academic Year") %>' />
                                <asp:HiddenField ID="hfFaculty" runat="server" Value='<%# Eval("Faculty Code") %>' />
                                <asp:HiddenField ID="hfHour" runat="server" Value='<%# Eval("Hour No") %>' />
                                <asp:HiddenField ID="hfRoom" runat="server" Value='<%# Eval("Room Allocation") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Day No">
                            <ItemTemplate>
                                <asp:Label ID="lblDay" runat="server" Text='<%# Eval("Day No") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Hour No">
                            <ItemTemplate>
                                <asp:Label ID="lblHour" runat="server" Text='<%# Eval("Hour No") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Course Code">
                            <ItemTemplate>
                                <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("Course Code") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject Code">
                            <ItemTemplate>
                                <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("Subject Code") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Semester Code">
                            <ItemTemplate>
                                <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("Semester Code") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Section">
                            <ItemTemplate>
                                <asp:Label ID="lblSection" runat="server" Text='<%# Eval("Section Code") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Attendance Date") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Room No">
                            <ItemTemplate>
                                <asp:Label ID="lblRomm" runat="server" Text='<%# Eval("Room Allocation") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Modify Time Table" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkModify" runat="server" OnClick="lnkModify_Click" Text="Modify" />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <FooterStyle BackColor="#B5C7DE" ForeColor="LightBlue" />
                    <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="LightBlue" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                    <RowStyle ForeColor="#4A3C8C" BackColor="LightBlue" CssClass="cssGridheaderfont" />
                    <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
                    <SortedAscendingCellStyle BackColor="#F4F4FD" />
                    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                    <SortedDescendingCellStyle BackColor="#D8D8F0" />
                    <SortedDescendingHeaderStyle BackColor="#3E3277" />
                </asp:GridView>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

