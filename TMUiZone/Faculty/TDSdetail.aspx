<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="TDSdetail.aspx.cs" Inherits="Faculty_TDSdetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function showPopup2() {
            $find("mpe2").show();
        }
        function closePopup2() {
            $find("mpe2").hide();   // modal hide
        }
        function showPopup3() {
            $find("mpe3").show();
        }
        function closePopup3() {
            $find("mpe3").hide();   // modal hide
        }
    </script>
    <style>
        /* Container with 2 columns */
        .section {
            display: grid;
            grid-template-columns: 1fr 1fr; /* 2 equal columns */
            gap: 15px; /* space between fields */
            max-width: 600px; /* optional, for layout */
            margin-left: 20px;
        }

        /* Each field styling */
        .field {
            display: flex;
            flex-direction: column;
            text-align: left;
        }

            /* Remark field spans both columns */
            .field.remark {
                grid-column: span 2;
            }

            /* Optional: style textboxes */
            .field input[type="text"],
            .field textarea {
                padding: 5px;
                font-size: 14px;
            }

            /* Optional: style labels */
            .field label {
                margin-bottom: 5px;
                font-weight: bold;
            }
   
        .gridview-custom th,
        .gridview-custom td {
            white-space: nowrap;
        }

        .text-center {
            text-align: center !important;
        }

        .text-left {
            text-align: left !important;
        }

        .text-right {
            text-align: right !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <asp:UpdatePanel ID="upModal" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:LinkButton ID="lnkDummy2" runat="server" Style="display: none;"></asp:LinkButton>
            <!-- Modal Extender -->
            <asp:ModalPopupExtender ID="ModalPopupExtender2" BehaviorID="mpe2" runat="server" PopupControlID="pnlPopup2" TargetControlID="lnkDummy2" BackgroundCssClass="modalBackground" CancelControlID="btnHide2">
            </asp:ModalPopupExtender>
            <!-- Modal Panel -->
            <asp:Panel ID="pnlPopup2" runat="server" CssClass="modalPopup" Style="display: none; width: 700px;">
                <div class="header">
                    <b>
                        <asp:Label ID="lblNotification2" runat="server" Text="TDS Request"></asp:Label></b>
                    <div class="close">
                        <asp:Button ID="btnHide2" runat="server" Text="X" Style="padding: 0px;" OnClientClick="closePopup2(); return false;" />
                    </div>
                </div>

                <div class="body">
                    <div class="section">
                        <div class="field">
                            <label>Employee No</label>
                            <asp:TextBox ID="txtEmployeeNo" runat="server" Enabled="false"></asp:TextBox>
                        </div>

                        <div class="field">
                            <label>Name</label>
                            <asp:TextBox ID="txtEmployeeName" runat="server" Enabled="false"></asp:TextBox>
                        </div>

                        <div class="field">
                            <label>Amount</label>
                            <asp:TextBox ID="txtLastAmount" runat="server" Text="0" Enabled="false"></asp:TextBox>
                        </div>

                        <div class="field">
                            <label>New Amount</label>
                            <asp:TextBox ID="txtNewAmountNew" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtNewAmountNew" ErrorMessage="**" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                        </div>

                        <div class="field">
                            <label>From Date</label>
                            <asp:TextBox ID="txtfromdate" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfromdate" Format="dd MMM yyyy"></asp:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ControlToValidate="txtfromdate" ErrorMessage="**" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                        </div>

                        <div class="field">
                            <label>To Date</label>
                            <asp:TextBox ID="txtTodate" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTodate" Format="dd MMM yyyy"></asp:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtTodate" ErrorMessage="**" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                        </div>

                        <div class="field remark">
                            <label>Remark</label>
                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>

                    <asp:Button ID="btnSubmit11" runat="server" Text="Submit" CssClass="submit-btn" ValidationGroup="odapps" OnClick="btnSave_Click" Style="margin-bottom: 15px;" />
                </div>

            </asp:Panel>


            <asp:LinkButton ID="lnkDummy3" runat="server" Style="display: none;"></asp:LinkButton>
            <!-- Modal Extender -->
            <asp:ModalPopupExtender ID="ModalPopupExtender3" BehaviorID="mpe3" runat="server" PopupControlID="pnlPopup3" TargetControlID="lnkDummy3" BackgroundCssClass="modalBackground" CancelControlID="btnHide3">
            </asp:ModalPopupExtender>
            <!-- Modal Panel -->
            <asp:Panel ID="pnlPopup3" runat="server" CssClass="modalPopup" Style="display: none; width: 1200px;">
                <div class="header">
                    <b>
                        <asp:Label ID="lblNotification3" runat="server" Text="TDS Request List"></asp:Label></b>
                    <div class="close">
                        <asp:Button ID="btnHide3" runat="server" Text="X" Style="padding: 0px;" OnClientClick="closePopup3(); return false;" />
                    </div>
                </div>

                <div class="body">
                    <asp:GridView ID="getTDSRequestList" runat="server"
                        AutoGenerateColumns="False"
                        CssClass="gridview-custom"
                        BackColor="White"
                        BorderColor="#E7E7FF"
                        BorderStyle="None"
                        BorderWidth="1px"
                        CellPadding="3"
                        Width="100%"
                        GridLines="Horizontal"
                        EmptyDataText="There are no data records to display."
                        AllowSorting="true">

                        <AlternatingRowStyle BackColor="#F7F7F7" />

                        <Columns>

                            <asp:TemplateField HeaderText="Sl. No.">
                                <HeaderStyle CssClass="text-center" Width="7%" />
                                <ItemStyle CssClass="text-center" Width="7%" />
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Employee Code">
                                <HeaderStyle CssClass="text-center" Width="7%" />
                                <ItemStyle CssClass="text-center" Width="7%" />
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("EmployeeNo") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Employee Name">
                                <HeaderStyle CssClass="text-left" Width="7%" />
                                <ItemStyle CssClass="text-left" Width="7%" />
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("EmployeeName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="From Date">
                                <HeaderStyle CssClass="text-center" Width="10%" />
                                <ItemStyle CssClass="text-center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("from_date", "{0:dd MMM yyyy}") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="To Date">
                                <HeaderStyle CssClass="text-center" Width="7%" />
                                <ItemStyle CssClass="text-center" Width="7%" />
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("to_date", "{0:dd MMM yyyy}") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Actual Amount">
                                <HeaderStyle CssClass="text-right" Width="7%" />
                                <ItemStyle CssClass="text-right" Width="7%" />
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("OldAmount") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Updated Amount">
                                <HeaderStyle CssClass="text-right" Width="7%" />
                                <ItemStyle CssClass="text-right" Width="7%" />
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("NewAmount") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status">
                                <HeaderStyle CssClass="text-center" Width="7%" />
                                <ItemStyle CssClass="text-center" Width="7%" />
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("StatusText") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Total Amount">
                                <HeaderStyle CssClass="text-right" Width="7%" />
                                <ItemStyle CssClass="text-right" Width="7%" />
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("CreatedBy") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>

                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" />
                        <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" />
                        <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />

                    </asp:GridView>

                </div>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="TDS Detail" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <table>
        <tr style="height: 20px">
            <td></td>
        </tr>
        <tr>
            <td style="width: 20px"></td>
            <td style="font-size: larger">Financial Year:</td>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="drpacsession" Height="30px" Width="100px" runat="server">
                    <asp:ListItem Text="2025-2026"></asp:ListItem>
                    <asp:ListItem Text="2024-2025"></asp:ListItem>
                    <asp:ListItem Text="2023-2024"></asp:ListItem>
                    <asp:ListItem Text="2022-2023"></asp:ListItem>
                    <asp:ListItem Text="2021-2022"></asp:ListItem>
                    <asp:ListItem Text="2020-2021"></asp:ListItem>
                    <asp:ListItem Text="2019-2020"></asp:ListItem>
                    <asp:ListItem Text="2018-2019"></asp:ListItem>
                    <asp:ListItem Text="2017-2018"></asp:ListItem>
                    <asp:ListItem Text="2016-2017"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button runat="server" Text="Show" Height="30px" Width="120px" ID="btnshow" OnClick="btnshow_Click" ForeColor="White" BackColor="Green" /></td>
            <td>&nbsp;&nbsp;&nbsp;
                <asp:Button runat="server" Text="Export to Excel" Height="30px" Width="120px" ForeColor="White" BackColor="Green" ID="btnexporttoexel" OnClick="btnexporttoexel_Click" /></td>
            <td>&nbsp;&nbsp;&nbsp;
                <button type="button" onclick="showPopup2();" style="color: white; background-color: green; height: 30px; width: 120px;">TDS Request</button></td>
            <td>&nbsp;&nbsp;&nbsp;
                <button type="button" onclick="showPopup3();" style="color: white; background-color: green; height: 30px; width: 120px;">TDS Request List</button></td>
        </tr>
    </table>

    <br />
    <asp:GridView ID="grdStudentAttendanceD" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
        BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
        GridLines="Horizontal" EmptyDataText="There are no data records to display."
        AllowSorting="true">
        <AlternatingRowStyle BackColor="#F7F7F7" />
        <Columns>
            <asp:TemplateField HeaderText="Sl. No.">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
                <ItemStyle Width="7%" />
            </asp:TemplateField>
        </Columns>
        <Columns>
            <asp:TemplateField HeaderText="Employee Code">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("Employee No") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="7%" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Employee Name">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("Employee Name") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="7%" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Months">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("Month") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="10%" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Year">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("Year") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="7%" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Amount">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("Actual Amount") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="7%" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Total Amount">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="7%" />
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
        <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
        <SortedAscendingCellStyle BackColor="#F4F4FD" />
        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
        <SortedDescendingCellStyle BackColor="#D8D8F0" />
        <SortedDescendingHeaderStyle BackColor="#3E3277" />

    </asp:GridView>


</asp:Content>

