<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="CovidCertificateUpload.aspx.cs" Inherits="Faculty_CovidCertificateUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Covid Certificate" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <fieldset class="boxBody">

        <table cellpadding="0px" cellspacing="0px">
            <tr>
                <td style="width: 10px"></td>
                <td style="width: 200px" valign="top">

                    <table cellpadding="0px" cellspacing="0px" class="leftbg1" style="width: 180px; height: 430px">
                        <tr>
                            <td style="width: 10px"></td>
                            <td>


                                <table cellpadding="0px" cellspacing="0px">
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftmMenu">&nbsp;<asp:LinkButton ID="lnkUpload" runat="server" OnClick="lnkUpload_Click">Upload Certificate</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftmMenu">&nbsp;<asp:LinkButton ID="lnkApproval" runat="server" OnClick="lnkApproval_Click">Certificate Approval</asp:LinkButton></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>

                                </table>

                            </td>
                            <td style="width: 10px"></td>
                        </tr>
                    </table>




                </td>
                <td style="width: 30px"></td>
                <td valign="top">

                    <asp:Panel ID="pnlUpload" runat="server" CssClass="leftBackground">

                        <table cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td colspan="8">
                                    <asp:Label ID="MSGU" Text="Covid  Certificate Upload" Font-Bold="true" runat="server"> </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 20px"></td>
                            </tr>
                            <tr>
                                <td style="width: 120px" colspan="4">
                                    <label>First Dose Date :-</label>
                                </td>
                                <td style="width: 120px">
                                    <asp:TextBox ID="txtfDate" runat="server" AutoPostBack="True" autocomplete="off" Width="120px" Height="25px" onkeydown="return false;"
                                        oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>

                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfDate" Format="dd MMM yyyy">
                                    </cc1:CalendarExtender>
                                </td>
                                <td style="width: 30px"></td>

                                <td style="width: 130px" colspan="4">
                                    <label>Second Dose Date :-</label>
                                </td>
                                <td style="width: 120px">
                                    <asp:TextBox ID="txtSDate" runat="server" AutoPostBack="True" autocomplete="off" Width="120px" Height="25px" onkeydown="return false;"
                                        oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>

                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSDate" Format="dd MMM yyyy">
                                    </cc1:CalendarExtender>
                                </td>
                                <td style="width: 30px"></td>
                                <td style="width: 130px" colspan="4">
                                    <label>View Attachment :-</label>
                                </td>
                                <td>
                                    <asp:FileUpload ID="fUpload" runat="server" Width="130px" />
                                </td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:Button ID="btnUpload" Text="Upload File" runat="server" OnClick="btnUpload_Click" />
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td style="height: 30px"></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="grdCovidCertificate" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                                        <AlternatingRowStyle BackColor="#F7F7F7" />
                                        <Columns>
                                            <asp:BoundField DataField="Employee Code" HeaderText="Employee Code" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="Employee Name" HeaderText="Employee Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="First Vaccine Date" HeaderText="First Vaccine Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="Second Vaccine Date" HeaderText="Second Vaccine Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="File Name" HeaderText="File Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />

                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <asp:LinkButton ID="lnkdelete"  visible='<%# Convert.ToBoolean(Eval("EnableDisable")) %>'  Text="Delete" CommandArgument='<%# Eval("Employee Code") %>' runat="server" OnClick="lnkdelete_Click"></asp:LinkButton>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="lnkdelete" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                        <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                        <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
                                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                        <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                        <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>

                    </asp:Panel>

                    <asp:Panel ID="pnlApproval" runat="server" CssClass="leftBackground" Visible="false">
                        <table cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td colspan="8">
                                    <asp:Label ID="Label2" Text="Covid  Certificate Uploaded Employee List :-" Font-Bold="true" runat="server"> </asp:Label>
                                </td>
                                <td style="width: 300px; text-align: right">
                                    <asp:Button ID="btnApprove" Width="150px" Text="Approved" runat="server" OnClick="btnApprove_Click" />
                                </td>
                                <td style="width: 10px"></td>
                                <td style="width: 20px; text-align: right; margin-left: 5px">
                                    <asp:Button ID="btnReject" Width="150px" Text="Reject" runat="server" OnClick="btnReject_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 20px"></td>
                            </tr>
                            <tr>
                                <asp:GridView ID="grdEmployee" DataKeyNames="Employee Code" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                                    <AlternatingRowStyle BackColor="#F7F7F7" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Employee Code">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkMark" runat="server" Text='<%#Bind("[Employee Code]") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                        <asp:BoundField DataField="Employee Name" HeaderText="Employee Name" HtmlEncode="false" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                         <asp:TemplateField HeaderText="File Name">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkFile" Text='<%#Bind("[File Name]") %>' OnClick="lnkFile_Click" CommandArgument='<%# Eval("Employee Code") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />

                                     
                                        <asp:TemplateField HeaderText="First Dose">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkFirst" runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Second Dose">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSecond" runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remark">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtremark" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                    <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                    <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                    <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                </asp:GridView>
                            </tr>
                        </table>
                    </asp:Panel>


                </td>
            </tr>
        </table>


    </fieldset>

</asp:Content>

