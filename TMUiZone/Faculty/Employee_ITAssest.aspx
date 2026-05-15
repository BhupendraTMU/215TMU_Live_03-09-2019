<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="Employee_ITAssest.aspx.cs" Inherits="Faculty_Employee_ITAssest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .grid-header {
            background-color: #ed7600;
            color: #fff;
            font-weight: 600;
            text-align: left;
        }

        .grid-row {
            background-color: #fdfdfd;
            color: #333;
        }

        .grid-alt-row {
            background-color: #f5f7fa;
        }

        .table td, .table th {
            padding: 10px !important;
            vertical-align: middle !important;
        }

        .text-bold {
            font-weight: 600;
        }

        .table-hover tbody tr:hover {
            background-color: #eef5ff;
        }

        .it-asset-header {
            background: linear-gradient(90deg, #ed7600, #ff9b42);
            color: #fff;
            padding: 14px 20px;
            font-size: 18px;
            font-weight: 600;
            border-radius: 6px 6px 0 0;
            letter-spacing: 0.5px;
            box-shadow: 0 2px 6px rgba(0,0,0,0.15);
            margin-bottom: 5px;
        }

        .it-grid {
            border-radius: 0 0 6px 6px;
            overflow: hidden;
            box-shadow: 0 4px 12px rgba(0,0,0,0.08);
        }

            .it-grid th {
                background-color: #ed7600 !important;
                color: white !important;
                font-size: 12px;
                padding: 10px;
                text-transform: uppercase;
            }

            .it-grid td {
                padding: 8px 10px;
                font-size: 11px;
                color: #333;
            }

            .it-grid tr:nth-child(even) {
                background-color: #f7f9fa;
            }

            .it-grid tr:hover {
                background-color: #fff3e6;
                transition: 0.2s ease-in-out;
            }

            .it-grid input[type="checkbox"] {
                transform: scale(1.2);
                cursor: pointer;
            }

            .it-grid select {
                border-radius: 4px;
                border: 1px solid #ccc;
                padding: 4px;
                font-size: 11px;
            }

            .it-grid a {
                color: #ed7600;
                font-weight: 600;
                text-decoration: none;
            }

                .it-grid a:hover {
                    text-decoration: underline;
                }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="page-container">


        <div class="it-asset-header">
            IT ASSET DETAILS
        </div>


        <asp:GridView ID="grdEmployee" runat="server"
            CssClass="it-grid"
            AutoGenerateColumns="False"
            GridLines="None"
            CellPadding="0"
            CellSpacing="0"
            DataKeyNames="Remark"
            Width="100%">

            <Columns>

                <asp:TemplateField HeaderText="S No">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Emp Name">
                    <ItemTemplate>
                        <asp:Label ID="EmpName" runat="server" Text='<%# Bind("[First Name]") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Emp Code">
                    <ItemTemplate>
                        <asp:Label ID="EmpCode" runat="server" Text='<%# Bind("[No_]") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Designation">
                    <ItemTemplate>
                        <asp:Label ID="EmpDesignation" runat="server" Text='<%# Bind("Designation") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Dept">
                    <ItemTemplate>
                        <asp:Label ID="Dept" runat="server" Text='<%# Bind("[Dept]") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Reporting">
                    <ItemTemplate>
                        <asp:Label ID="Reporting" runat="server" Text='<%# Bind("[Reporting]") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Deputed">
                    <ItemTemplate>
                        <asp:Label ID="Deputed" runat="server" Text="NO" />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--   OnClick="btnView_Click" --%>
                <asp:TemplateField HeaderText="View Asset Detail">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnView" runat="server"
                            Text="View"
                            CommandArgument='<%# Eval("No_") %>'
                            OnClick="btnView_Click" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Asset Transfer">
                    <ItemTemplate>
                        <asp:LinkButton ID="btntransfer" runat="server"
                            Text="Transfer"
                            CommandArgument='<%# Eval("No_") %>'
                            OnClick="btntransfer_Click" />
                    </ItemTemplate>
                </asp:TemplateField>



            </Columns>

            <EmptyDataTemplate>
                <div style="padding: 10px; color: red;">
                    No records found.
                </div>
            </EmptyDataTemplate>

        </asp:GridView>

    </div>
    <!-- Dummy link for first modal -->
    <asp:LinkButton ID="lnkDummy" runat="server" Style="display: none"></asp:LinkButton>

    <!-- Modal Popup Extender for Asset Details -->
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
        BehaviorID="mpeAssetDetails"
        PopupControlID="pnlPopup"
        TargetControlID="lnkDummy"
        BackgroundCssClass="modalBackground"
        CancelControlID="btnHide">
    </asp:ModalPopupExtender>

    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none" Width="1000px" Height="500px" ScrollBars="Vertical">
        <div class="header">
            <b>
                <asp:Label ID="lblNotification" runat="server"></asp:Label>
            </b>
            <div class="close">
                <asp:Button ID="btnHide" runat="server" Text="X" UseSubmitBehavior="false" OnClientClick="return false;" />
            </div>
        </div>
        <div class="body">
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="height: 13px"></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAssetHeader" runat="server" Text="Asset Details" Font-Size="15pt" ForeColor="#093A62" Font-Names="Georgia, 'Times New Roman', 'Helvetica Neue'"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 13px"></td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="grdAssestDetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderStyle="None" BorderWidth="1px"
                            CellPadding="3" Width="100%" GridLines="Horizontal" EmptyDataText="There are no data records to display." AllowSorting="true">
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <ItemStyle Width="2%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Indent No_">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndentNo" runat="server" Text='<%# Eval("Indent No_") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Asset No_">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAssetNo" runat="server" Text='<%# Eval("Asset No_") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Item Serial No_">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemSerialNo" runat="server" Text='<%# Eval("Item Serial No_") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Issue on Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAllotedDate" runat="server" Text='<%# Eval("Alloted Date") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Approver By ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblApproverID" runat="server" Text='<%# Eval("Approver ID") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Approver By Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblApproverName" runat="server" Text='<%# Eval("Approver ID Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Location_Room no_">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("Location_Room no_") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                            <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>

    <!-- Dummy link for second modal -->
    <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none"></asp:LinkButton>

    <!-- Modal Popup Extender for Employee Asset Transfer -->
    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server"
        BehaviorID="mpeEmployeeTransfer"
        PopupControlID="Panel1"
        TargetControlID="LinkButton1"
        BackgroundCssClass="modalBackground"
        CancelControlID="btnCloseTransfer">
    </asp:ModalPopupExtender>

    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Style="display: none" Width="1000px" Height="500px" ScrollBars="Vertical">
        <div class="header">
            <b>
                <asp:Label ID="lblTransferHeader" runat="server"></asp:Label>
            </b>
            <div class="close">
                <asp:Button ID="btnCloseTransfer" runat="server" Text="X" UseSubmitBehavior="false" OnClientClick="return false;" />
            </div>
        </div>
        <div class="body">
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="height: 13px"></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Font-Size="15pt" ForeColor="#093A62" Font-Names="Georgia, 'Times New Roman', 'Helvetica Neue'"></asp:Label>
                        <br />
                        <asp:Label ID="Label1" runat="server" Font-Size="15pt" ForeColor="#093A62" Font-Names="Georgia, 'Times New Roman', 'Helvetica Neue'"></asp:Label>
                        <br />
                        <asp:Label ID="lblEmployeeTransfer" runat="server" Text="Asset Transfer" Font-Size="15pt" ForeColor="#093A62" Font-Names="Georgia, 'Times New Roman', 'Helvetica Neue'"></asp:Label>

                        <asp:HiddenField ID="hfOldEmployee" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr>
                    <td style="height: 13px"></td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                            CssClass="table table-bordered table-hover"
                            HeaderStyle-CssClass="grid-header" OnRowCommand="GridView1_RowCommand"
                            RowStyle-CssClass="grid-row"
                            AlternatingRowStyle-CssClass="grid-alt-row"
                            EmptyDataText="No records found"
                            Width="100%">

                            <Columns>


                                <asp:TemplateField HeaderText="Sl. No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Indent No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndentNo" runat="server"
                                            Text='<%# Eval("Indent No_") %>' CssClass="text-bold"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Asset No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAssetNoTransfer" runat="server"
                                            Text='<%# Eval("Asset No_") %>' CssClass="text-bold"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Serial No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemSerialNoTransfer" runat="server"
                                            Text='<%# Eval("Item Serial No_") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="12%" />
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Item Code">
                                      <ItemTemplate>
                                          <asp:Label ID="lblTransferItem" runat="server"
                                              Text='<%# Eval("FA Tag Item") %>'></asp:Label>
                                      </ItemTemplate>
                                      <ItemStyle Width="18%" />
                                  </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescriptionTransfer" runat="server"
                                            Text='<%# Eval("Description") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="18%" />
                                </asp:TemplateField>


                                <%--<asp:TemplateField HeaderText="Approved By">
                                    <ItemTemplate>
                                        <asp:Label ID="lblApproverNameTransfer" runat="server"
                                            Text='<%# Eval("Approver ID Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="15%" />
                                </asp:TemplateField>--%>


                                <asp:TemplateField HeaderText="Room No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLocationTransfer" runat="server"
                                            Text='<%# Eval("Location_Room no_") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Transfer To">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="drpEmployeee" runat="server"
                                            CssClass="form-control form-control-sm"
                                            Width="100%">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Transfer">
                                    <ItemTemplate>
                                        <asp:Button ID="btnTransfer" runat="server" Text="TRANSFER"
                                            CommandName="Trans"
                                            CommandArgument='<%# Container.DataItemIndex %>'
                                            CssClass="form-control form-control-sm"
                                            Width="100%"
                                            OnClientClick="return confirm('Are you sure you want to transfer this asset?');"></asp:Button>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" />
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>







</asp:Content>

