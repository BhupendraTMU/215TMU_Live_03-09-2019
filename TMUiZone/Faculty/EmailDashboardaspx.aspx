
<%@ Page Title="Employee Email Dashboard"
    Language="C#"
    MasterPageFile="~/Faculty/IndexMaster.master"
    AutoEventWireup="true"
    CodeFile="EmailDashboardaspx.aspx.cs"
    Inherits="Faculty_EmailDashboardaspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@3.4.1/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />

    <style>
        body {
            background: #f4f6f9;
            font-family: "Segoe UI", Arial, sans-serif;
            color: #333;
        }

        .email-dashboard {
            padding: 20px;
        }

        .dashboard-header {
            background: linear-gradient(135deg, #182848, #4b6cb7);
            color: #fff;
            padding: 22px 25px;
            border-radius: 8px;
            margin-bottom: 18px;
            box-shadow: 0 4px 15px rgba(0,0,0,.12);
        }

        .dashboard-header h2 {
            margin: 0;
            font-weight: 600;
        }

        .dashboard-header p {
            margin: 7px 0 0;
            opacity: .85;
        }

        .action-bar {
            margin-bottom: 20px;
        }

        .action-bar .btn {
            padding: 10px 18px;
            font-weight: 600;
            border-radius: 5px;
        }

        .stat-card {
            display: block !important;
            width: 100% !important;
            height: 135px;
            position: relative;
            overflow: hidden;
            border-radius: 12px;
            padding: 22px 20px;
            color: #fff !important;
            text-decoration: none !important;
            box-sizing: border-box;
            margin-bottom: 20px;
            box-shadow: 0 5px 18px rgba(0,0,0,.12);
            transition: all .25s ease;
        }

        .stat-card:hover {
            color: #fff !important;
            text-decoration: none !important;
            transform: translateY(-4px);
            box-shadow: 0 10px 25px rgba(0,0,0,.18);
        }

        .stat-card .title {
            display: block;
            font-size: 15px;
            font-weight: 600;
            text-transform: uppercase;
            letter-spacing: .4px;
            margin-bottom: 8px;
        }

        .stat-card .number {
            display: block;
            font-size: 36px;
            line-height: 42px;
            font-weight: 700;
        }

        .stat-card .icon {
            position: absolute;
            right: 18px;
            top: 25px;
            font-size: 55px;
            opacity: .20;
            pointer-events: none;
        }

        .card-total {
            background: linear-gradient(135deg, #4e73df, #224abe);
        }

        .card-unique {
            background: linear-gradient(135deg, #1cc88a, #13855c);
        }

        .card-duplicate {
            background: linear-gradient(135deg, #e74a3b, #be2617);
        }

        .card-records {
            background: linear-gradient(135deg, #f6c23e, #dda20a);
        }

        .filter-box {
            background: #fff;
            padding: 18px;
            border-radius: 8px;
            margin-bottom: 20px;
            box-shadow: 0 2px 10px rgba(0,0,0,.07);
        }

        .filter-title {
            font-weight: 600;
            font-size: 16px;
            margin-bottom: 12px;
        }

        .grid-panel {
            background: #fff;
            border-radius: 8px;
            padding: 0;
            box-shadow: 0 2px 10px rgba(0,0,0,.07);
            overflow: hidden;
        }

        .grid-header {
            padding: 16px 20px;
            border-bottom: 1px solid #eee;
            background: #fff;
        }

        .grid-header h4 {
            margin: 0;
            font-weight: 600;
        }

        .table {
            margin-bottom: 0;
        }

        .table > thead > tr > th {
            background: #253858;
            color: #fff;
            border: none;
            padding: 12px 10px;
            font-weight: 500;
            white-space: nowrap;
        }

        .table > tbody > tr > td {
            padding: 11px 10px;
            vertical-align: middle;
        }

        .table > tbody > tr:hover {
            background: #f5f8fc;
        }

        .email-text {
            color: #337ab7;
            font-weight: 500;
        }

        .duplicate-badge {
            background: #e74c3c;
            color: #fff;
            padding: 4px 9px;
            border-radius: 15px;
            font-size: 12px;
            font-weight: 600;
        }

        .unique-badge {
            background: #27ae60;
            color: #fff;
            padding: 4px 9px;
            border-radius: 15px;
            font-size: 12px;
            font-weight: 600;
        }

        .status-active {
            color: #27ae60;
            font-weight: 600;
        }

        .status-inactive {
            color: #e74c3c;
            font-weight: 600;
        }

        .empty-data {
            padding: 40px;
            text-align: center;
            color: #999;
        }

        .paging a, .paging span {
            display: inline-block;
            padding: 6px 11px;
            margin: 2px;
            border: 1px solid #ddd;
            border-radius: 4px;
        }

        .paging span {
            background: #337ab7;
            color: #fff;
            border-color: #337ab7;
        }

        .modal-header {
            background: #253858;
            color: #fff;
            border-radius: 5px 5px 0 0;
        }

        .modal-header .close {
            color: #fff;
            opacity: .9;
        }

        .modal-title {
            font-weight: 600;
        }

        .form-group label {
            font-weight: 600;
        }

        .required {
            color: #e74c3c;
        }

        .modal-footer {
            background: #f7f8fa;
        }

        .message-label {
            display: block;
            margin-bottom: 12px;
            font-weight: 600;
        }

        @media(max-width:767px) {
            .email-dashboard {
                padding: 10px;
            }

            .stat-card {
                height: 115px;
            }

            .stat-card .number {
                font-size: 27px;
            }

            .action-bar .text-right {
                text-align: left;
                margin-top: 10px;
            }

            .grid-panel {
                overflow-x: auto;
            }
        }
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <asp:UpdatePanel ID="upDashboard" runat="server" UpdateMode="Conditional">

        <ContentTemplate>

            <div class="container-fluid email-dashboard">

                <!-- HEADER -->

                <div class="dashboard-header">
                    <h2>
                        <i class="fa fa-envelope"></i>
                        Employee Email Dashboard
                    </h2>
                    <p>Employee Email ID monitoring and duplicate analysis</p>
                </div>


                <!-- ACTION BUTTONS -->

                <div class="row action-bar">

                    <div class="col-sm-6">
                        <asp:LinkButton ID="btnAddNew"
                            runat="server"
                            CssClass="btn btn-success"
                            OnClick="btnAddNew_Click">
                            <i class="fa fa-plus"></i> Add New Employee
                        </asp:LinkButton>
                    </div>

                    <div class="col-sm-6 text-right">
                        <asp:Button ID="btnExportExcel"
                            runat="server"
                            Text="Export to Excel"
                            CssClass="btn btn-primary"
                            OnClick="btnExportExcel_Click" />
                    </div>

                </div>


                <!-- SUMMARY CARDS -->

                <div class="row">

                    <div class="col-md-3 col-sm-6">
                        <asp:LinkButton ID="btnTotal"
                            runat="server"
                            CssClass="stat-card card-total"
                            OnClick="btnTotal_Click">
                            <div class="title">Total Records</div>
                            <div class="number">
                                <asp:Label ID="lblTotal" runat="server" Text="0" />
                            </div>
                            <i class="fa fa-users icon"></i>
                        </asp:LinkButton>
                    </div>

                    <div class="col-md-3 col-sm-6">
                        <asp:LinkButton ID="btnUnique"
                            runat="server"
                            CssClass="stat-card card-unique"
                            OnClick="btnUnique_Click">
                            <div class="title">Unique Email IDs</div>
                            <div class="number">
                                <asp:Label ID="lblUnique" runat="server" Text="0" />
                            </div>
                            <i class="fa fa-check-circle icon"></i>
                        </asp:LinkButton>
                    </div>

                    <div class="col-md-3 col-sm-6">
                        <asp:LinkButton ID="btnDuplicate"
                            runat="server"
                            CssClass="stat-card card-duplicate"
                            OnClick="btnDuplicate_Click">
                            <div class="title">Duplicate Email IDs</div>
                            <div class="number">
                                <asp:Label ID="lblDuplicate" runat="server" Text="0" />
                            </div>
                            <i class="fa fa-exclamation-triangle icon"></i>
                        </asp:LinkButton>
                    </div>

                    <div class="col-md-3 col-sm-6">
                        <asp:LinkButton ID="btnDuplicateRecords"
                            runat="server"
                            CssClass="stat-card card-records"
                            OnClick="btnDuplicateRecords_Click">
                            <div class="title">Duplicate Records</div>
                            <div class="number">
                                <asp:Label ID="lblDuplicateRecords" runat="server" Text="0" />
                            </div>
                            <i class="fa fa-copy icon"></i>
                        </asp:LinkButton>
                    </div>

                </div>


                <!-- SEARCH -->

                <div class="filter-box">

                    <div class="filter-title">
                        <i class="fa fa-search"></i>
                        Search Employee Email Data
                    </div>

                    <div class="row">

                        <div class="col-md-10">
                            <asp:TextBox ID="txtSearch"
                                runat="server"
                                CssClass="form-control"
                                placeholder="Search TMU Code / Employee Name / Email ID / Mobile No / Department / Designation">
                            </asp:TextBox>
                        </div>

                        <div class="col-md-2">
                            <asp:Button ID="btnSearch"
                                runat="server"
                                Text="Search"
                                CssClass="btn btn-primary btn-block"
                                OnClick="btnSearch_Click" />
                        </div>

                    </div>

                    <div style="margin-top:10px;">
                        <asp:Button ID="btnReset"
                            runat="server"
                            Text="Reset Filter"
                            CssClass="btn btn-default"
                            OnClick="btnReset_Click" />
                    </div>

                </div>


                <!-- GRID -->

                <div class="grid-panel">

                    <div class="grid-header">

                        <div class="row">

                            <div class="col-md-8">
                                <h4>
                                    <i class="fa fa-list"></i>
                                    <asp:Label ID="lblGridTitle"
                                        runat="server"
                                        Text="All Employee Email Records" />
                                </h4>
                            </div>

                            <div class="col-md-4 text-right">
                                <asp:Label ID="lblRecordCount"
                                    runat="server"
                                    CssClass="text-muted" />
                            </div>

                        </div>

                    </div>

                    <div class="table-responsive">

                        <asp:GridView ID="gvEmail"
                            runat="server"
                            AutoGenerateColumns="False"
                            CssClass="table table-hover"
                            GridLines="None"
                            AllowPaging="True"
                            PageSize="25"
                            OnPageIndexChanging="gvEmail_PageIndexChanging"
                            EmptyDataText="No email records found.">

                            <PagerStyle CssClass="paging" />
                            <EmptyDataRowStyle CssClass="empty-data" />

                            <Columns>

                                <asp:BoundField
                                    DataField="TMUCode"
                                    HeaderText="TMU Code" />

                                <asp:BoundField
                                    DataField="EmployeeName"
                                    HeaderText="Employee Name" />

                                <asp:BoundField
                                    DataField="MobileNo"
                                    HeaderText="Mobile No." />

                                <asp:TemplateField HeaderText="Email ID">
                                    <ItemTemplate>
                                        <span class="email-text">
                                            <%# Eval("EmailID") %>
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField
                                    DataField="Department"
                                    HeaderText="Department" />

                                <asp:BoundField
                                    DataField="Designation"
                                    HeaderText="Designation" />

                                <asp:TemplateField HeaderText="Duplicate Count">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDuplicateCount"
                                            runat="server"
                                            Text='<%# Eval("DuplicateCount") %>'
                                            CssClass='<%# Convert.ToInt32(Eval("DuplicateCount")) > 1 ? "duplicate-badge" : "unique-badge" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus"
                                            runat="server"
                                            Text='<%# Convert.ToInt32(Eval("Status")) == 1 ? "Active" : "Inactive" %>'
                                            CssClass='<%# Convert.ToInt32(Eval("Status")) == 1 ? "status-active" : "status-inactive" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField
                                    DataField="CreatedDate"
                                    HeaderText="Created Date"
                                    DataFormatString="{0:dd-MM-yyyy HH:mm}" />

                            </Columns>

                        </asp:GridView>

                    </div>

                </div>


                <!-- ADD EMPLOYEE MODAL -->

                <div class="modal fade"
                    id="employeeModal"
                    tabindex="-1"
                    role="dialog"
                    aria-hidden="true">

                    <div class="modal-dialog modal-lg" role="document">

                        <div class="modal-content">

                            <div class="modal-header">

                                <button type="button"
                                    class="close"
                                    data-dismiss="modal">
                                    <span>&times;</span>
                                </button>

                                <h4 class="modal-title">
                                    <i class="fa fa-user-plus"></i>
                                    Add New Employee Email
                                </h4>

                            </div>

                            <div class="modal-body">

                                <asp:Label ID="lblMessage"
                                    runat="server"
                                    CssClass="message-label" />

                                <div class="row">

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>TMU Code <span class="required">*</span></label>
                                            <asp:TextBox ID="txtTMUCode"
                                                runat="server"
                                                CssClass="form-control"
                                                MaxLength="30"
                                                placeholder="Enter TMU Code" />
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Employee Name <span class="required">*</span></label>
                                            <asp:TextBox ID="txtEmployeeName"
                                                runat="server"
                                                CssClass="form-control"
                                                MaxLength="150"
                                                placeholder="Enter Employee Name" />
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Mobile No. <span class="required">*</span></label>
                                            <asp:TextBox ID="txtMobileNo"
                                                runat="server"
                                                CssClass="form-control"
                                                MaxLength="15"
                                                placeholder="Enter Mobile Number" />
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Email ID <span class="required">*</span></label>
                                            <asp:TextBox ID="txtEmailID"
                                                runat="server"
                                                CssClass="form-control"
                                                MaxLength="200"
                                                TextMode="Email"
                                                placeholder="Enter Email ID" />
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Department</label>
                                            <asp:TextBox ID="txtDepartment"
                                                runat="server"
                                                CssClass="form-control"
                                                MaxLength="150"
                                                placeholder="Enter Department" />
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Designation</label>
                                            <asp:TextBox ID="txtDesignation"
                                                runat="server"
                                                CssClass="form-control"
                                                MaxLength="150"
                                                placeholder="Enter Designation" />
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Status</label>
                                            <asp:DropDownList ID="ddlStatus"
                                                runat="server"
                                                CssClass="form-control">
                                                <asp:ListItem Text="Active" Value="1" />
                                                <asp:ListItem Text="Inactive" Value="0" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </div>

                            </div>

                            <div class="modal-footer">

                                <asp:Button ID="btnSaveEmployee"
                                    runat="server"
                                    Text="Save Employee"
                                    CssClass="btn btn-success"
                                    OnClick="btnSaveEmployee_Click" />

                                <button type="button"
                                    class="btn btn-default"
                                    data-dismiss="modal">
                                    Close
                                </button>

                            </div>

                        </div>

                    </div>

                </div>

            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportExcel" />
        </Triggers>

    </asp:UpdatePanel>


    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@3.4.1/dist/js/bootstrap.min.js"></script>

</asp:Content>
