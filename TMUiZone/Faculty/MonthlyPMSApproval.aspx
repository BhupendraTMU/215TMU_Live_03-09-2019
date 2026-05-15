<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="MonthlyPMSApproval.aspx.cs" Inherits="Faculty_MonthlyPMSApproval" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function HidePopup10() {

            $('#confirmModal1').modal('hide');
        }
        function VisiblePopup10() {
            $('#confirmModal1').modal('show');


        }

    </script>
    <style type="text/css">
        .table th {
            white-space: nowrap;
            text-align: center;
            vertical-align: middle;
        }

        .table td {
            font-size: 12px;
            vertical-align: middle;
        }

        .sticky-top {
            position: sticky;
            top: 0;
            z-index: 10;
        }

        .input-group-text,
        .form-select,
        .btn {
            border-radius: 6px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset class="boxBody text-center">
        <asp:Label ID="Label1" runat="server"
            Text="MONTHLY PMS APPROVAL"
            Font-Size="18pt"
            Font-Bold="true"
            ForeColor="#093A62"
            Font-Names="Georgia,Times New Roman,Helvetica Neue">
        </asp:Label>
    </fieldset>
    <asp:Panel ID="pnlTitleList" runat="server" Visible="true">

        <div class="card shadow-sm mb-3">
            <div class="card-body p-2">
                <div class="d-flex align-items-center gap-2 flex-nowrap overflow-auto">

                    <!-- Academic Year -->
                    <div class="input-group input-group-sm" style="min-width: 220px;">
                        <span class="input-group-text fw-semibold">Academic Year     </span>
                        <asp:DropDownList ID="dd_AcademicYear"
                            runat="server"
                            CssClass="form-select">
                        </asp:DropDownList>
                        &nbsp  &nbsp  &nbsp
                        <span class="input-group-text fw-semibold">Month     </span>
                        <asp:DropDownList ID="drpMonth"
                            runat="server"
                            AutoPostBack="true"
                            CssClass="form-select">
                            <asp:ListItem Value="1">January</asp:ListItem>
                            <asp:ListItem Value="2">February</asp:ListItem>
                            <asp:ListItem Value="3">March</asp:ListItem>
                            <asp:ListItem Value="4">April</asp:ListItem>
                            <asp:ListItem Value="5">May</asp:ListItem>
                            <asp:ListItem Value="6">June</asp:ListItem>
                            <asp:ListItem Value="7">July</asp:ListItem>
                            <asp:ListItem Value="8">August</asp:ListItem>
                            <asp:ListItem Value="9">September</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp  &nbsp  &nbsp
                        <!-- Search Button -->
                        <asp:Button ID="btnSearch"
                            runat="server"
                            Text="Search"
                            CssClass="btn btn-sm btn-primary px-4" OnClick="btnSearch_Click" />
                    </div>

                </div>
            </div>

        </div>
        <br />


        <div class="table-responsive">
            <asp:GridView ID="grdPMSList" runat="server"
                DataKeyNames="Employee_Code,Month,Academic_Year"
                AutoGenerateColumns="False"
                CssClass="table table-bordered table-striped table-hover table-sm align-middle text-center"
                EmptyDataText="There are no data records to display.">

                <Columns>
                    <asp:TemplateField HeaderText="Sl. No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Details">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDetails" runat="server"
                                Text="View"
                                CssClass="btn btn-sm btn-outline-primary"
                                CommandArgument='<%# Bind("Employee_Code") %>'
                                OnCommand="lnkDetails_Command" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Academic_Year" HeaderText="Academic Year" />
                    <asp:BoundField DataField="Employee_Code" HeaderText="Employee Code" />
                    <asp:BoundField DataField="Emp_name" HeaderText="Employee Name" />
                    <asp:BoundField DataField="Month" HeaderText="Month" />
                    <%-- <asp:BoundField DataField="Approval_status" HeaderText="Status" />--%>
                </Columns>

                <HeaderStyle CssClass="table-dark text-center" />
            </asp:GridView>
        </div>

    </asp:Panel>
    <div id="confirmModal1" class="modal fade" role="dialog" style="margin-right: 350px">
        <div class="modal-dialog modal-xl modal-dialog-centered">
            <div class="modal-content" style="width: 1000px">

                <div class="modal-header bg-primary text-white" style="width: 1000px">
                    <h5 class="modal-title">Monthly PMS Approval Document List</h5>
                    <button type="button" class="btn-close btn-close-white" style="background-color: black"
                        onclick="HidePopup10();">
                        X
                    </button>
                </div>




                <div class="modal-body">

                    <div class="table-responsive" style="max-height: 500px; overflow: auto;">
                        <asp:GridView ID="gv_a1" runat="server"
                            DataKeyNames="AutoNo"
                            AutoGenerateColumns="False"
                            CssClass="table table-bordered table-hover table-sm"
                            EmptyDataText="There are no data records to display.">

                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Employee Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpoyee_Code" runat="server"
                                            Text='<%# Bind("Empoyee_Code") %>' />
                                        <asp:HiddenField ID="hfAuto" runat="server"
                                            Value='<%# Eval("AutoNo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ActivityType" HeaderText="Activity" />

                                <asp:TemplateField HeaderText="Applicable For">
                                    <ItemTemplate>
                                        <asp:Label ID="lblApplicable_For" runat="server"
                                           Text='<%# Eval("Applicable_For") %>' />
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>




                               
                                <%--   <asp:BoundField DataField="Type_Of_research" HeaderText="Type" />--%>
                                <%--<asp:BoundField DataField="Title" HeaderText="Title" />
                                <asp:BoundField DataField="ApplicationNo" HeaderText="Journal" />
                                <asp:BoundField DataField="volume" HeaderText="Volume" />
                                <asp:BoundField DataField="Issue" HeaderText="Issue" />
                                <asp:BoundField DataField="PageNo" HeaderText="Page No." />
                                <asp:BoundField DataField="ISSNNo" HeaderText="ISSN No." />
                                <asp:BoundField DataField="DOP" HeaderText="Publication Date" />
                                <asp:BoundField DataField="No_of_auth" HeaderText="Authors" />
                                <asp:BoundField DataField="ArticleLink" HeaderText="Article" />--%>
                                <asp:BoundField DataField="Status" HeaderText="Status" />

                                <asp:TemplateField HeaderText="File">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_A1_Downalod" runat="server"
                                            Text="Download" Visible='<%# Eval("ActivityType").ToString() == "Yes" %>'
                                            CssClass="btn btn-sm btn-outline-primary"
                                            CommandArgument='<%# Bind("AutoNo") %>'
                                            OnCommand="lnk_A1_Downalod_Command" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkStudent" runat="server"
                                            Enabled='<%# Eval("Status").ToString()=="Pending" %>'
                                            Checked="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                            <HeaderStyle CssClass="table-warning text-center sticky-top" />
                        </asp:GridView>
                    </div>

                </div>

                <div class="modal-footer justify-content-center">
                    <asp:Button ID="btnApprove" runat="server"
                        Text="Approve"
                        CssClass="btn btn-success px-4"
                        OnClick="btnApprove_Click" />

                    <asp:Button ID="btnReject" runat="server"
                        Text="Reject"
                        CssClass="btn btn-danger px-4"
                        OnClick="btnReject_Click" />
                </div>

            </div>
        </div>
    </div>
</asp:Content>

