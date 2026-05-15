<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="EquipmentApproval.aspx.cs" Inherits="Faculty_EquipmentApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .modal-panel {
            background: #fff;
            padding: 20px;
            border-radius: 12px;
            box-shadow: 0 8px 30px rgba(0,0,0,0.2);
            width: 95%;
            max-width: 1100px;
        }

        .modern-grid th {
            background-color: #343a40 !important;
            color: #fff;
            font-size: 14px;
        }

        .modern-grid td, .modern-grid th {
            border: 1px solid #000 !important;
            padding: 8px;
            font-size: 13px;
        }

        .form-control {
            height: 35px;
            padding: 6px 10px;
        }

        .text-end {
            text-align: right;
        }

        .modal-panel {
            background: #fff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 5px 25px rgba(0,0,0,0.2);
            width: 90%;
            max-width: 1000px;
        }

        .modern-grid th {
            background-color: #343a40 !important;
            color: #fff;
        }

        .modern-grid td, .modern-grid th {
            border: 1px solid #000 !important;
            padding: 8px;
        }

        .text-end {
            text-align: right;
        }

        .pageTitle {
            font-size: 20px;
            font-weight: 600;
            color: #093A62;
            margin-bottom: 15px;
        }

        .filterBox {
            background: #f4f6f9;
            padding: 12px;
            border-radius: 6px;
            margin-bottom: 15px;
        }

        .table th {
            background: #ff9900;
            color: white;
            text-align: center;
        }

        .modalBackgroundforco {
            background: rgba(0,0,0,0.6);
        }

        .modern-grid {
            border: 2px solid #000 !important;
        }

            .modern-grid th {
                border: 1px solid #000 !important;
                background-color: #343a40;
                color: #fff;
            }

            .modern-grid td {
                border: 1px solid #000 !important;
            }

            .modern-grid tr:hover {
                background-color: #f5f5f5;
            }
    </style>

    <script type="text/javascript">

        function Confirm() {
            return confirm("Do you want to Approve ?");
        }

        function Delete() {
            return confirm("Do you want to Reject ?");
        }

        function ItemReceive() {
            return confirm("Please confirm that you have receive this item");
        }

        function closeModalPopup() {
            var mpe = $find("mpeItemLineData");
            if (mpe) { mpe.hide(); }
        }

        function closeModalPopupE() {
            var mpe = $find("mpeItemLineDataE");
            if (mpe) { mpe.hide(); }
        }

</script>

</asp:Content>

<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="pageTitle mb-3">IT Indent Approval</div>

    <div class="card p-3 shadow-sm mb-4">

        <div class="row g-3 align-items-end">

            <!-- From Date -->
            <div class="col-md-3">
                <label class="form-label">From Date</label>
                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                    TargetControlID="txtFromDate"
                    Format="dd MMM yyyy" />
            </div>

            <!-- Till Date -->
            <div class="col-md-3">
                <label class="form-label">Till Date</label>
                <asp:TextBox ID="txtTillDate" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server"
                    TargetControlID="txtTillDate"
                    Format="dd MMM yyyy" />
            </div>

            <!-- Status -->
            <div class="col-md-3">
                <label class="form-label">Status</label>
                <asp:DropDownList ID="ddStatus" runat="server" CssClass="form-control">
                    <asp:ListItem Value="7">ALL</asp:ListItem>
                    <asp:ListItem Value="1">Processed for Approval</asp:ListItem>
                    <asp:ListItem Value="2">Approved (HOD)</asp:ListItem>
                    <asp:ListItem Value="4">Rejected</asp:ListItem>                   
                    <asp:ListItem Value="5">Issued</asp:ListItem>
                   
                </asp:DropDownList>
            </div>

            <!-- Buttons -->
            <div class="col-md-3 d-flex gap-2">
                <label class="form-label" style="visibility: hidden">Status</label>
                <asp:Button ID="btnSearch" runat="server"
                    Text="Search"
                    CssClass="btn btn-primary w-50"
                    OnClick="btnSearch_Click" />

                <asp:Button ID="Button4" runat="server"
                    Text="Export"
                    CssClass="btn btn-success w-50"
                    OnClick="Button4_Click" />
            </div>

        </div>

    </div>

    <!-- GRID -->

    <div class="card shadow-sm">
        <div class="table-responsive">

            <asp:GridView
                ID="grdApproval"
                runat="server"
                AutoGenerateColumns="False"
                CssClass="table table-bordered table-hover align-middle mb-0 modern-grid"
                HeaderStyle-CssClass="table-dark"
                DataKeyNames="DocumentNo"
                AllowPaging="True"
                OnRowDataBound="grdApproval_RowDataBound"
                OnPageIndexChanging="grdApproval_PageIndexChanging">

                <Columns>

                    <asp:TemplateField HeaderText="Indent No">
                        <ItemTemplate>
                            <asp:LinkButton
                                ID="lblDoumnentNo"
                                runat="server"
                                CssClass="fw-bold text-primary text-decoration-none"
                                Text='<%#Bind("DocumentNo")%>'
                                CommandArgument='<%#Bind("DocumentNo")%>'
                                OnCommand="lblDoumnentNo_Command1" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:BoundField DataField="Issue Date"
                        HeaderText="Date"
                        DataFormatString="{0:dd MMM yyyy}" />


                    <asp:BoundField DataField="Issue For"
                        HeaderText="Purpose" />


                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label
                                ID="lblStatus"
                                runat="server"
                                CssClass="badge bg-secondary"
                                Text='<%#Bind("Status")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:BoundField DataField="Issue Id" HeaderText="ID" />
                    <asp:BoundField DataField="Issue Name" HeaderText="Name" />


                    <asp:TemplateField HeaderText="HOD Remark">
                        <ItemTemplate>
                            <asp:TextBox
                                ID="txtRRemark"
                                runat="server"
                                CssClass="form-control form-control-sm"
                                Text='<%#Bind("HodRemark")%>'
                                TextMode="MultiLine"
                                Rows="2"
                                Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>


                   <%-- <asp:TemplateField HeaderText="Mgmt Remark">
                        <ItemTemplate>
                            <asp:TextBox
                                ID="txtMRemark"
                                runat="server"
                                CssClass="form-control form-control-sm"
                                Text='<%#Bind("Management_remark")%>'
                                TextMode="MultiLine"
                                Rows="2"
                                Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>--%>


                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>

                            <div class="d-flex flex-column gap-1">

                                <asp:Button
                                    ID="btnApprove"
                                    runat="server"
                                    Text="Approve"
                                    CssClass="btn btn-success btn-sm"
                                    CommandArgument='<%#Eval("DocumentNo")%>'
                                    OnClientClick="return Confirm();"
                                    OnCommand="btnApprove_Command" />

                                <asp:Button
                                    ID="btnClose"
                                    runat="server"
                                    Text="Received"
                                    CssClass="btn btn-warning btn-sm"
                                    Visible="false"
                                    CommandArgument='<%#Eval("DocumentNo")%>'
                                    OnClientClick="return ItemReceive();"
                                    OnCommand="btnClose_Command" />

                                <asp:Button
                                    ID="btnReject"
                                    runat="server"
                                    Text="Reject"
                                    CssClass="btn btn-danger btn-sm"
                                    CommandArgument='<%#Eval("DocumentNo")%>'
                                    OnClientClick="return Delete();"
                                    OnCommand="btnReject_Command" />

                            </div>

                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>

        </div>
    </div>

    <!-- HIDDEN GRID FOR EXPORT -->

    <asp:GridView
        ID="GridView2"
        runat="server"
        Visible="false">
    </asp:GridView>



    <asp:Button ID="Button1"
        runat="server"
        Style="display: none" />

    <asp:ModalPopupExtender
        ID="mdIndentLine"
        runat="server"
        TargetControlID="Button1"
        PopupControlID="PnlIndentLineData"
        BackgroundCssClass="modalBackgroundforco" />

    <asp:Panel ID="PnlIndentLineData"
        runat="server"
        CssClass="modal-panel">

        <h5 class="mb-3 text-primary fw-bold">Indent Line Details</h5>

        <div class="table-responsive">

            <asp:GridView
                ID="grdViewIndentLine"
                runat="server"
                AutoGenerateColumns="False"
                CssClass="table table-bordered table-hover modern-grid"
                HeaderStyle-CssClass="table-dark"
                AllowPaging="True"
                DataKeyNames="Line No_"
                OnPageIndexChanging="grdViewIndentLine_PageIndexChanging">

                <Columns>

                    <asp:BoundField DataField="Document No"
                        HeaderText="Indent No" />

                    <asp:TemplateField HeaderText="Employee">
                        <ItemTemplate>
                            <asp:LinkButton
                                ID="lnkNo_"
                                runat="server"
                                CssClass="text-primary fw-bold"
                                Text='<%#Bind("No_")%>'
                                OnClick="lnkNo__Click" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Item">
                        <ItemTemplate>
                            <asp:LinkButton
                                ID="lblItemNo_Grid"
                                runat="server"
                                CssClass="text-decoration-none"
                                Text='<%#Bind("[Item No]")%>'
                                OnClick="lblItemNo_Grid_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Description"
                        HeaderText="Description" />


                    <asp:BoundField DataField="Quantity"
                        HeaderText="Req Qty"
                        DataFormatString="{0:N2}" />


                    <asp:TemplateField HeaderText="HOD Qty">
                        <ItemTemplate>

                            <asp:TextBox
                                ID="lblQuantity_Grid"
                                runat="server"
                                CssClass="form-control form-control-sm text-end"
                                Text='<%#Eval("[HOD Appr_ QTY]", "{0:N2}")%>' />

                            <asp:HiddenField
                                ID="hfQuantity"
                                runat="server"
                                Value='<%#Bind("[HOD Appr_ QTY]")%>' />

                        </ItemTemplate>
                    </asp:TemplateField>


                   <%-- <asp:TemplateField HeaderText="Final Qty">
                        <ItemTemplate>

                            <asp:TextBox
                                ID="lblFinalQTY"
                                runat="server"
                                CssClass="form-control form-control-sm text-end bg-light"
                                Enabled="false"
                                Text='<%#Eval("[Management Appr_ QTY]", "{0:N2}")%>' />

                        </ItemTemplate>
                    </asp:TemplateField>--%>

                </Columns>

            </asp:GridView>

        </div>

        <div class="d-flex justify-content-end gap-2 mt-3">

            <asp:Button
                ID="Button2"
                runat="server"
                Text="Update Qty"
                CssClass="btn btn-success"
                OnClick="btnClose_Click" />

            <asp:Button
                ID="btnClose"
                runat="server"
                Text="Close"
                CssClass="btn btn-secondary" />

        </div>

    </asp:Panel>

    <!-- ITEM DETAIL POPUP -->

    <asp:Button ID="Button3"
        runat="server"
        Style="display: none" />

    <asp:ModalPopupExtender
        ID="ModalPopupExtender1"
        runat="server"
        TargetControlID="Button3"
        PopupControlID="PnlItemLineData"
        BehaviorID="mpeItemLineData"
        BackgroundCssClass="modalBackgroundforco" />

   <asp:Panel ID="PnlItemLineData"
    runat="server"
    CssClass="modal-panel">

    <!-- Header -->
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h5 class="fw-bold text-primary mb-0">Item Details</h5>

        <asp:Button
            ID="Button5"
            runat="server"
            Text="Close"
            CssClass="btn btn-danger btn-sm"
            OnClientClick="closeModalPopup();return false;" />
    </div>

    <!-- Item Info Section -->
    <div class="row g-3 mb-3">

        <div class="col-md-3">
            <label class="fw-bold">Item Code</label>
            <asp:Label ID="lblItemCode" runat="server"
                CssClass="form-control bg-light" />
        </div>

        <div class="col-md-3">
            <label class="fw-bold">Last Qty</label>
            <asp:Label ID="Label4" runat="server"
                CssClass="form-control bg-light text-end" />
        </div>

        <div class="col-md-3">
            <label class="fw-bold">Last Purchase Date</label>
            <asp:Label ID="Label5" runat="server"
                CssClass="form-control bg-light" />
        </div>

        <div class="col-md-3">
            <label class="fw-bold">Available Qty</label>
            <asp:Label ID="Label6" runat="server"
                CssClass="form-control bg-light text-end" />
        </div>

    </div>

    <!-- Grid -->
    <div class="table-responsive">

        <asp:GridView
            ID="GridView1"
            runat="server"
            CssClass="table table-bordered table-hover modern-grid"
            HeaderStyle-CssClass="table-dark"
            AutoGenerateColumns="True" />

    </div>

</asp:Panel>
    <asp:Button ID="Button6" runat="server" Text="Button" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Button6" PopupControlID="Panel1" BackgroundCssClass="modalBackgroundforco" BehaviorID="mpeItemLineDataE"></asp:ModalPopupExtender>


    <asp:Panel ID="Panel1" runat="server" CssClass="modal-panel">

        <!-- Header -->
        <div class="d-flex justify-content-between align-items-center mb-3">
            <h5 class="fw-bold text-primary mb-0">Item Details</h5>

            <asp:Button ID="Button7" runat="server"
                Text="Close"
                CssClass="btn btn-danger btn-sm"
                OnClientClick="closeModalPopupE(); return false;" />
        </div>

        <!-- Item Info -->
        <div class="row g-3 mb-3">

            <div class="col-md-3">
                <label class="fw-bold">Item Code</label>
                <asp:Label ID="Label8" runat="server" CssClass="form-control bg-light" />
            </div>

            <div class="col-md-3">
                <label class="fw-bold">Last Qty Purchase</label>
                <asp:Label ID="Label9" runat="server" CssClass="form-control bg-light text-end" />
            </div>

            <div class="col-md-3">
                <label class="fw-bold">Last Purchase Date</label>
                <asp:Label ID="Label10" runat="server" CssClass="form-control bg-light" />
            </div>

            <div class="col-md-3">
                <label class="fw-bold">Available Qty</label>
                <asp:Label ID="Label11" runat="server" CssClass="form-control bg-light text-end" />
            </div>

        </div>

        <!-- Grid -->
        <div class="table-responsive">

            <asp:GridView ID="GridView3" runat="server"
                AutoGenerateColumns="False"
                CssClass="table table-bordered table-hover modern-grid"
                HeaderStyle-CssClass="table-dark">

                <Columns>

                    <asp:BoundField DataField="Asset No_" HeaderText="Asset No" />
                    <asp:BoundField DataField="Document No" HeaderText="Document No" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="No_" HeaderText="Issue ID" />
                    <asp:BoundField DataField="Name" HeaderText="Issue Name" />

                    <asp:BoundField DataField="Posting Date"
                        HeaderText="Issue Date"
                        DataFormatString="{0:dd MMM yyyy}" />

                    <asp:BoundField DataField="Location_Room no_"
                        HeaderText="Location" />

                </Columns>

            </asp:GridView>

        </div>

    </asp:Panel>
</asp:Content>

