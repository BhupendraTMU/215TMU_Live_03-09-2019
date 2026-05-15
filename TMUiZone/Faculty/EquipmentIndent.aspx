<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="EquipmentIndent.aspx.cs" Inherits="Faculty_EquipmentIndent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
    <style>
        .popupPanel {
            background: #fff;
            width: 600px; /* medium width */
            max-width: 90%; /* responsive */
            margin: auto;
            border-radius: 6px;
            box-shadow: 0 5px 15px rgba(0,0,0,.3);
            padding: 20px;
        }


        .popupHeader {
            display: flex;
            justify-content: space-between;
            align-items: center;
            background: #0d6efd;
            color: white;
            padding: 10px 15px;
            border-radius: 5px;
            margin-bottom: 20px;
        }

            .popupHeader span {
                font-size: 18px;
                font-weight: 600;
            }

        .form-label {
            font-weight: 600;
        }

        .table th {
            background: #f5f5f5;
        }

        .section-title {
            font-size: 16px;
            font-weight: 600;
            color: #0d6efd;
            margin-top: 15px;
        }

        .modalBackgroundforco {
            background: rgba(0,0,0,0.6);
        }

        .card {
            border-radius: 10px;
        }
    </style>

    <script>

        function Confirm() {

            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";

            if (confirm("Do you want to Send For Approval ?")) {
                confirm_value.value = "Yes";
            }
            else {
                confirm_value.value = "No";
            }

            document.forms[0].appendChild(confirm_value);

        }

        function Delete() {

            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";

            if (confirm("Do you want to Delete ?")) {
                confirm_value.value = "Yes";
            }
            else {
                confirm_value.value = "No";
            }

            document.forms[0].appendChild(confirm_value);

        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="container-fluid mt-3">

        <div class="card shadow">

            <div class="card-header bg-primary text-white">

                <h5 class="mb-0">EQUIPMENT Indent</h5>

            </div>

            <div class="card-body">

                <!-- Filter Section -->

                <div class="row g-2 mb-3">

                    <div class="col-md-2">

                        <label>From Date</label>

                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control"></asp:TextBox>

                        <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                            TargetControlID="txtFromDate"
                            Format="dd MMM yyyy">
                        </asp:CalendarExtender>

                    </div>

                    <div class="col-md-2">

                        <label>Till Date</label>

                        <asp:TextBox ID="txtTillDate" runat="server" CssClass="form-control"></asp:TextBox>

                        <asp:CalendarExtender ID="CalendarExtender2" runat="server"
                            TargetControlID="txtTillDate"
                            Format="dd MMM yyyy">
                        </asp:CalendarExtender>

                    </div>

                    <div class="col-md-2">

                        <label>Status</label>

                        <asp:DropDownList ID="ddStatus" runat="server" CssClass="form-select">

                            <asp:ListItem Value="7">ALL</asp:ListItem>
                            <asp:ListItem Value="0">Open</asp:ListItem>
                            <asp:ListItem Value="1">Pending on HOD</asp:ListItem>
                            <asp:ListItem Value="2">Approved(HOD)</asp:ListItem>
                            <asp:ListItem Value="4">Rejected(HOD)</asp:ListItem>

                            <asp:ListItem Value="5">Issued</asp:ListItem>

                        </asp:DropDownList>

                    </div>

                    <div class="col-md-2 d-flex align-items-end">

                        <asp:Button ID="btnSearch"
                            runat="server"
                            Text="Search"
                            CssClass="btn btn-primary w-100"
                            OnClick="btnSearch_Click" />

                    </div>

                    <div class="col-md-2 d-flex align-items-end">

                        <asp:Button ID="btnCreate"
                            runat="server"
                            Text="Create Indent"
                            CssClass="btn btn-success w-100"
                            OnClick="btnCreate_Click" />

                    </div>

                    <div class="col-md-2 d-flex align-items-end">

                        <asp:Button ID="btnExportToExcel"
                            runat="server"
                            Text="Export Excel"
                            CssClass="btn btn-warning w-100"
                            OnClick="btnExportToExcel_Click" />

                    </div>

                </div>

                <!-- GridView -->

                <%--<asp:GridView ID="grdApproval"
                    runat="server"
                    AutoGenerateColumns="False"
                    CssClass="table table-bordered table-hover table-striped"
                    HeaderStyle-CssClass="table-dark"
                    AllowPaging="True"
                    DataKeyNames="DocumentNo"
                    OnPageIndexChanging="grdApproval_PageIndexChanging"
                    OnRowDataBound="grdApproval_RowDataBound">

                    <columns>

                        <asp:TemplateField HeaderText="Sr No">

                            <itemtemplate>

                                <%# Container.DataItemIndex + 1 %>
                            </itemtemplate>

                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Indent No">

                            <itemtemplate>

                                <asp:LinkButton ID="lblDoumnentNo"
                                    runat="server"
                                    Text='<%#Bind("DocumentNo") %>'
                                    CommandArgument='<%#Bind("DocumentNo") %>'
                                    OnCommand="lblDoumnentNo_Command">
                                </asp:LinkButton>

                            </itemtemplate>

                        </asp:TemplateField>

                        <asp:BoundField DataField="Issue Date"
                            HeaderText="Issue Date"
                            DataFormatString="{0:dd MMM yyyy}" />

                        <asp:BoundField DataField="Issue For" HeaderText="Issue For" />

                        <asp:TemplateField HeaderText="Status">

                            <itemtemplate>

                                <asp:Label ID="lblStatus"
                                    runat="server"
                                    Text='<%#Bind("Status") %>'></asp:Label>

                            </itemtemplate>

                        </asp:TemplateField>

                        <asp:BoundField DataField="Issue Id" HeaderText="Issue Id" />

                        <asp:BoundField DataField="Issue Name" HeaderText="Issue Name" />

                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" />

                        <asp:BoundField DataField="HodRemark" HeaderText="HOD Remark" />

                        <asp:BoundField DataField="Management_remark"
                            HeaderText="Management Remark" />

                        <asp:TemplateField>

                            <itemtemplate>

                                <asp:Button ID="btnApprove"
                                    runat="server"
                                    Text="Send"
                                    CssClass="btn btn-success btn-sm"
                                    CommandArgument='<%# Eval("DocumentNo") %>'
                                    OnClientClick="Confirm()"
                                    OnCommand="btnApprove_Command" />

                            </itemtemplate>

                        </asp:TemplateField>

                        <asp:TemplateField>

                            <itemtemplate>

                                <asp:Button ID="btnReject"
                                    runat="server"
                                    Text="Reject"
                                    CssClass="btn btn-danger btn-sm"
                                    CommandArgument='<%# Eval("DocumentNo") %>'
                                    OnClientClick="Delete()"
                                    OnCommand="btnReject_Command" />

                            </itemtemplate>

                        </asp:TemplateField>

                    </columns>

                </asp:GridView>--%>
                <asp:GridView ID="grdApproval" runat="server" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None"
                    CssClass="table table-striped table-bordered table-hover" OnPageIndexChanging="grdApproval_PageIndexChanging" OnDataBound="grdApproval_DataBound" OnRowDataBound="grdApproval_RowDataBound" BackColor="White" BorderColor="#3366CC" BorderStyle="None"
                    BorderWidth="1px" CellPadding="4" AllowPaging="True" DataKeyNames="DocumentNo">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr No" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Indent No">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblDoumnentNo" runat="server" Text='<%#Bind("DocumentNo") %>' CommandArgument='<%#Bind("DocumentNo") %>' OnCommand="lblDoumnentNo_Command"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--  <asp:BoundField DataField=" DocumentNo" HeaderText="Indent No" />--%>
                        <asp:BoundField DataField="Issue Date" HeaderText="Issue Date" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="Issue For" HeaderText="Issue For" />
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("Status") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Issue Id" HeaderText="Issue Id" />
                        <asp:BoundField DataField="Issue Name" HeaderText="Issue Name" />
                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                        <asp:BoundField DataField="HodRemark" HeaderText="Hod Remark" />

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnApprove" runat="server" Text="Send for Approval" CommandArgument='<%# Eval("DocumentNo") %>' OnClientClick="Confirm()" OnCommand="btnApprove_Command" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnReject" runat="server" Text="Delete" CommandArgument='<%# Eval("DocumentNo") %>' OnClientClick="Delete()" OnCommand="btnReject_Command" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        There are no record found.....
                    </EmptyDataTemplate>
                    <FooterStyle BackColor="#ff9900" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#ff9900" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#ff9900" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                    <SortedDescendingHeaderStyle BackColor="#002876" />
                </asp:GridView>
            </div>

        </div>

    </div>

    <!-- Modal -->

    <asp:Button ID="Button1"
        runat="server"
        Text="Button"
        Style="display: none" />

    <asp:ModalPopupExtender
        ID="mdIndentLine"
        runat="server"
        TargetControlID="Button1"
        PopupControlID="PnlIndentLineData"
        BackgroundCssClass="modalBackgroundforco" />

    <asp:Panel ID="PnlIndentLineData"
        runat="server"
        CssClass="card shadow p-3"
        Style="width: 900px">

        <div class="d-flex justify-content-between mb-2">

            <h5>Indent Details</h5>

            <asp:Button ID="btnClose"
                runat="server"
                Text="Close"
                CssClass="btn btn-secondary btn-sm" />

        </div>

        <asp:GridView ID="grdViewIndentLine_1"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="table table-bordered table-striped table-hover">

            <Columns>

                <asp:BoundField DataField="Document No" HeaderText="Indent No" />

                <asp:BoundField DataField="No_" HeaderText="No" />

                <asp:BoundField DataField="Name" HeaderText="Name" />

                <asp:BoundField DataField="Description" HeaderText="Description" />

                <asp:BoundField DataField="Quantity" HeaderText="Qty" DataFormatString="{0:F2}" HtmlEncode="false" />

                <asp:BoundField DataField="HOD Appr_ QTY" HeaderText="HOD Qty" DataFormatString="{0:F2}" HtmlEncode="false" />

                <%--<asp:BoundField DataField="Management Appr_ QTY" HeaderText="Management Qty" DataFormatString="{0:F2}" HtmlEncode="false" />--%>

                <asp:BoundField DataField="User Remark" HeaderText="Remarks" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnDeleted" runat="server" OnCommand="btnDeleted_Command" OnClientClick="Delete()" Text="Delete" CommandArgument='<%# Eval("[Line No_]") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>

    </asp:Panel>

    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />

    <!-- Modal Popup -->

    <asp:ModalPopupExtender
        ID="mdlCreateIndent"
        runat="server"
        TargetControlID="btnShowPopup"
        PopupControlID="pnlCreateIndent"
        CancelControlID="btnClosePopup"
        BackgroundCssClass="modalBackground" />

    <!-- Popup Panel -->

    <asp:Panel ID="pnlCreateIndent" runat="server" Visible="false" CssClass="indentPanel">

        <style>
            .indentPanel {
                background: #ffffff;
                border-radius: 6px;
                padding: 20px;
                box-shadow: 0 0 10px rgba(0,0,0,0.15);
                margin-top: 20px;
                width: 900px; /* medium width */
                max-width: 90%; /* responsive */
            }

            .indentHeader {
                background: #0d6efd;
                color: #fff;
                padding: 10px 15px;
                font-size: 18px;
                font-weight: 600;
                border-radius: 5px;
                display: flex;
                justify-content: space-between;
                align-items: center;
            }

            .sectionTitle {
                font-weight: 600;
                color: #0d6efd;
                margin-top: 15px;
                margin-bottom: 10px;
            }
        </style>


        <!-- Hidden Fields -->

        <asp:HiddenField ID="hfIssueid" runat="server" />
        <asp:HiddenField ID="hfApprovalId" runat="server" />

        <div class="indentHeader">

            <span>Create Indent</span>

            <asp:Button
                ID="btnClosePopup"
                runat="server"
                Text="Close"
                CssClass="btn btn-danger btn-sm" />

        </div>


        <div class="container-fluid">

            <!-- INDENT DETAILS -->

            <div class="sectionTitle">Indent Details</div>

            <div class="row">

                <div class="col-md-3">

                    <label>Indent No</label>

                    <asp:TextBox
                        ID="txtIndentno"
                        runat="server"
                        Enabled="false"
                        CssClass="form-control" />

                </div>


                <div class="col-md-3">

                    <label>Indent Date</label>

                    <asp:TextBox
                        ID="txtIssueDate"
                        runat="server"
                        Enabled="false"
                        CssClass="form-control" />

                </div>


                <div class="col-md-3">

                    <label>Issue For</label>

                    <asp:DropDownList
                        ID="ddIssueFor"
                        runat="server"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddIssueFor_SelectedIndexChanged">

                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem Value="1">Department</asp:ListItem>
                        <%-- <asp:ListItem Value="2">Employee</asp:ListItem>--%>
                    </asp:DropDownList>

                </div>


                <div class="col-md-3">

                    <label>User Name</label>

                    <asp:TextBox
                        ID="txtIssueUserid"
                        runat="server"
                        Enabled="false"
                        CssClass="form-control" />

                </div>

            </div>


            <div class="row mt-3">

                <div class="col-md-3">

                    <label>Department Name</label>

                    <asp:DropDownList
                        ID="ddIssueid"
                        runat="server"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddIssueid_SelectedIndexChanged" />

                </div>


                <div class="col-md-3">

                    <label>Status</label>

                    <asp:DropDownList
                        ID="DropDownList1"
                        runat="server"
                        Enabled="false"
                        CssClass="form-control">

                        <asp:ListItem Value="1">Open</asp:ListItem>

                    </asp:DropDownList>

                </div>


                <div class="col-md-3">

                    <label>Issue Name</label>

                    <asp:TextBox
                        ID="txtIssueName"
                        runat="server"
                        Enabled="false"
                        CssClass="form-control" />
                </div>
                <div class="col-md-3">
                    <label>Approval Name</label>
                    <asp:TextBox
                        ID="txtapprovalid"
                        runat="server"
                        Enabled="false"
                        CssClass="form-control mt-1" />

                </div>

            </div>


            <hr />


            <!-- ITEM ENTRY -->

            <div class="sectionTitle">Indent Items</div>


            <div class="row">

                <div class="col-md-2">

                    <label>No</label>

                    <asp:TextBox
                        ID="txtDepartmentCode"
                        runat="server"
                        Enabled="false"
                        CssClass="form-control" />

                </div>


                <div class="col-md-3">

                    <label>Name</label>

                    <asp:TextBox
                        ID="txtDepartmentName"
                        runat="server"
                        Enabled="false"
                        CssClass="form-control" />

                </div>


                <div class="col-md-2">

                    <label>Item No</label>

                    <asp:DropDownList
                        ID="ddItemNo"
                        runat="server"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddItemNo_SelectedIndexChanged" />

                </div>


                <div class="col-md-3">

                    <label>Description</label>

                    <asp:TextBox
                        ID="txtDescription"
                        runat="server"
                        Enabled="false"
                        CssClass="form-control" />

                </div>


                <div class="col-md-2">

                    <label>UOM</label>

                    <asp:TextBox
                        ID="txtUnitofMeasure"
                        runat="server"
                        Enabled="false"
                        CssClass="form-control" />

                </div>

            </div>


            <div class="row mt-3">

                <div class="col-md-2">

                    <label>Remaining Qty</label>

                    <asp:Label
                        ID="txtRemaing"
                        runat="server"
                        CssClass="form-control" />

                </div>


                <div class="col-md-2">

                    <label>Quantity</label>

                    <asp:TextBox
                        ID="txtQuantityforRequistion"
                        runat="server"
                        CssClass="form-control" />

                </div>


                <div class="col-md-4">

                    <label>Remark</label>

                    <asp:TextBox
                        ID="txtUserRemark"
                        runat="server"
                        CssClass="form-control" />

                </div>


                <div class="col-md-2 d-flex align-items-end">

                    <asp:Button
                        ID="btnAdd"
                        runat="server"
                        Text="Add Item"
                        CssClass="btn btn-success"
                        OnClick="btnAdd_Click" />

                </div>

            </div>


            <br />


            <!-- GRIDVIEW -->


            <%--<asp:GridView
                ID="grdViewIndentLine"
                runat="server"
                AutoGenerateColumns="False"
                CssClass="table table-bordered table-striped">

                <Columns>

                    <asp:BoundField DataField="No_" HeaderText="No" />

                    <asp:BoundField DataField="Name" HeaderText="Name" />

                    <asp:BoundField DataField="Item No" HeaderText="Item No" />

                    <asp:BoundField DataField="Description" HeaderText="Description" />

                    <asp:BoundField DataField="Unit of Measure" HeaderText="UOM" />

                    <asp:BoundField DataField="Quantity" HeaderText="Qty" />

                    <asp:BoundField DataField="User Remark" HeaderText="Remark" />

                    <asp:TemplateField HeaderText="Action">

                        <ItemTemplate>

                            <asp:Button
                                ID="btnDeleted"
                                runat="server"
                                Text="Delete"
                                CssClass="btn btn-danger btn-sm"
                                CommandArgument='<%# Eval("[Line No_]") %>'
                                OnCommand="btnDeleted_Command" />

                        </ItemTemplate>

                    </asp:TemplateField>

                </Columns>

            </asp:GridView>--%>

            <asp:GridView ID="grdViewIndentLine" runat="server" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None" CssClass="table table-striped table-bordered table-hover" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" AllowPaging="True" OnPageIndexChanging="grdViewIndentLine_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="Document No" HeaderText="Indent No" />
                    <asp:BoundField HeaderText="No_" DataField="No_" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:TemplateField HeaderText="Item No.">
                        <ItemTemplate>
                            <asp:Label ID="lblItemNo_Grid" runat="server" Text='<%#Bind("[Item No]") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:Label ID="lblItemNoDescription_Grid" runat="server" Text='<%#Bind("[Description]") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit of Measure">

                        <ItemTemplate>
                            <asp:Label ID="lblUnitofMeasure_Grid" runat="server" Text='<%#Bind("[Unit of Measure]") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Variance Code"></asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Qty. for Requsition">
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity_Grid" runat="server" Text='<%#Bind("Quantity","{0:n}" ) %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="HOD QTY">
                        <ItemTemplate>
                            <asp:Label ID="lblHODQTY" runat="server" Text='<%#Bind("[HOD Appr_ QTY]","{0:n}" ) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" runat="server" Text='<%#Bind("[User Remark]" ) %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>






                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#ff9900" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <br />


            <div style="text-align: right">

                <asp:Button
                    ID="btnSendforApproval"
                    runat="server"
                    Text="Send For Approval"
                    CssClass="btn btn-primary"
                    OnClick="btnSendforApproval_Click" />

            </div>


            <!-- Hidden Controls -->

            <asp:Label ID="lblAcademicyrs" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblGenpostingGroup" runat="server" Visible="false"></asp:Label>

            <asp:TextBox
                ID="txtOldIndent"
                runat="server"
                Visible="false" />


        </div>

    </asp:Panel>


</asp:Content>

