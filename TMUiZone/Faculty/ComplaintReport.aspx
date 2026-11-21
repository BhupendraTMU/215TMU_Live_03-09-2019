
<%@ Page Title="Complaint Dashboard" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="ComplaintReport.aspx.cs" Inherits="Faculty_ComplaintReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css" />
    <link href="css/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="dist/css/AdminLTE.min.css" />
    <link rel="stylesheet" href="dist/css/skins/_all-skins.min.css" />
    <link href="css/CloudeStyleSheet.css" rel="stylesheet" />
    <link href="dist/css/style.css" rel="stylesheet" />
    <link href="css/gridstyle.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css" rel="stylesheet" />

    <style type="text/css">
        .complaint-page {
            padding: 15px;
            color: #25344a;
        }

        .complaint-header {
            display: flex;
            flex-wrap: wrap;
            align-items: center;
            justify-content: space-between;
            gap: 12px;
            margin-bottom: 20px;
            padding: 20px 22px;
            color: #fff;
            background: linear-gradient(120deg, #062b61, #1456a0);
            border-radius: 12px;
            box-shadow: 0 5px 18px rgba(6, 43, 97, .16);
        }

        .complaint-title {
            margin: 0;
            font-size: 23px;
            font-weight: 600;
        }

        .complaint-subtitle {
            margin: 6px 0 0;
            color: #e1eafa;
            font-size: 13px;
        }

        .complaint-card {
            margin-bottom: 20px;
            padding: 20px;
            background: #fff;
            border: 1px solid #e5eaf1;
            border-radius: 12px;
            box-shadow: 0 3px 14px rgba(20, 42, 80, .06);
        }

        .complaint-section-title {
            margin: 0 0 16px;
            font-size: 17px;
            font-weight: 600;
            color: #123d70;
        }

        .complaint-filter {
            max-width: 420px;
            height: 40px;
            border-radius: 7px;
            box-shadow: none;
        }

        .complaint-table-wrap {
            width: 100%;
            overflow-x: auto;
        }

        .complaint-grid {
            width: 100%;
            margin-bottom: 0;
            border-collapse: collapse;
            background: #fff;
        }

        .complaint-grid th {
            padding: 13px 12px !important;
            color: #fff;
            background: #123d70;
            border: 1px solid #dce5f0 !important;
            text-align: left;
            white-space: nowrap;
            font-weight: 600;
        }

        .complaint-grid td {
            padding: 12px !important;
            border: 1px solid #e5eaf1 !important;
            vertical-align: middle !important;
        }

        .complaint-grid tr:nth-child(even) td {
            background: #f7faff;
        }

        .complaint-grid tr:hover td {
            background: #edf5ff;
        }

        .complaint-link {
            display: inline-block;
            min-width: 30px;
            padding: 5px 10px;
            color: #0758b5 !important;
            background: #eaf3ff;
            border-radius: 6px;
            font-weight: 700;
            text-align: center;
            text-decoration: none !important;
        }

        .complaint-link:hover {
            color: #fff !important;
            background: #0758b5;
        }

        .complaint-btn {
            border-radius: 7px;
            padding: 9px 14px;
            font-weight: 600;
        }

        .complaint-modal .modal-dialog {
            width: 94%;
            max-width: 1250px;
            margin: 25px auto;
        }

        .complaint-modal .modal-content {
            border: 0;
            border-radius: 12px;
            overflow: hidden;
            box-shadow: 0 15px 50px rgba(0, 0, 0, .25);
        }

        .complaint-modal .modal-header {
            padding: 17px 22px;
            color: #fff;
            background: #062b61;
            border-bottom: 0;
        }

        .complaint-modal .modal-title {
            font-size: 19px;
            font-weight: 600;
        }

        .complaint-modal .modal-header .close {
            color: #fff;
            opacity: .9;
            font-size: 28px;
            text-shadow: none;
        }

        .complaint-modal .modal-body {
            padding: 20px;
            max-height: 70vh;
            overflow: auto;
            background: #f8fafd;
        }

        .complaint-modal .modal-footer {
            padding: 12px 20px;
            background: #fff;
        }

        .complaint-empty {
            padding: 22px;
            text-align: center;
            color: #68778b;
        }

        @media (max-width: 767px) {
            .complaint-page {
                padding: 8px;
            }

            .complaint-header {
                padding: 16px;
            }

            .complaint-title {
                font-size: 20px;
            }

            .complaint-card {
                padding: 12px;
            }

            .complaint-modal .modal-dialog {
                width: 96%;
                margin: 10px auto;
            }

            .complaint-modal .modal-body {
                padding: 10px;
                max-height: 75vh;
            }
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="complaint-page">

        <!-- Page header -->
        <div class="complaint-header">
            <div>
                <h2 class="complaint-title">
                    <i class="fa fa-ticket"></i>
                    Complaint Dashboard
                </h2>
                <p class="complaint-subtitle">
                    Department-wise complaint summary and status details
                </p>
            </div>

            <div>
                <asp:Button ID="btnBack" runat="server"
                    Text="Back to List"
                    CssClass="btn btn-default complaint-btn"
                    OnClick="btnBack_Click" />

                <asp:Button ID="btnExport" runat="server"
                    Text="Export to Excel"
                    CssClass="btn btn-success complaint-btn"
                    OnClick="btnExport_Click" />
            </div>
        </div>

        <!-- Hostel filter -->
        <div class="complaint-card">
            <h4 class="complaint-section-title">
                <i class="fa fa-filter"></i> Filter Complaints
            </h4>

            <div class="form-group" style="margin-bottom:0;">
                <label for="<%= drpHostel.ClientID %>">Select Hostel</label>

                <asp:DropDownList ID="drpHostel" runat="server"
                    AutoPostBack="true"
                    CssClass="form-control complaint-filter"
                    OnSelectedIndexChanged="drpHostel_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>

        <!-- Department summary -->
        <div class="complaint-card">
            <h4 class="complaint-section-title">
                <i class="fa fa-bar-chart"></i> Department-wise Complaint Summary
            </h4>

            <div class="complaint-table-wrap">

                <asp:UpdatePanel ID="Orderlist" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <asp:GridView ID="OrderList1"
                            runat="server"
                            DataKeyNames="Department"
                            AutoGenerateColumns="false"
                            CssClass="table table-bordered complaint-grid"
                            CellPadding="5"
                            CellSpacing="0"
                            GridLines="None"
                            EmptyDataText="No complaint records found.">

                            <Columns>

                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <ItemStyle Width="60px" />
                                </asp:TemplateField>

                                <asp:BoundField DataField="Department"
                                    HeaderText="Department" />

                                <asp:TemplateField HeaderText="Total Complaints">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkT" runat="server"
                                            CssClass="complaint-link"
                                            Text='<%# Eval("Total Complaint") %>'
                                            OnClick="lnkT_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Pending at Warden">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkPW" runat="server"
                                            CssClass="complaint-link"
                                            Text='<%# Eval("Pending at Warden") %>'
                                            OnClick="lnkPW_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Pending at Department">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkPD" runat="server"
                                            CssClass="complaint-link"
                                            Text='<%# Eval("Pending at Department") %>'
                                            OnClick="lnkPD_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Rejected by Warden">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkRD" runat="server"
                                            CssClass="complaint-link"
                                            Text='<%# Eval("Rejected by Warden") %>'
                                            OnClick="lnkRD_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Resolved by Department">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkResolved" runat="server"
                                            CssClass="complaint-link"
                                            Text='<%# Eval("Resolved from Department") %>'
                                            OnClick="lnkResolved_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Closed">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkClose" runat="server"
                                            CssClass="complaint-link"
                                            Text='<%# Eval("Close") %>'
                                            OnClick="lnkClose_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>

                        </asp:GridView>

                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>

            <p class="text-muted" style="margin:12px 0 0;">
                <i class="fa fa-info-circle"></i>
                Click any blue count to view its complaint details.
            </p>
        </div>

    </div>

    <!-- Complaint details popup -->
    <div id="complaintModal" class="modal fade complaint-modal"
        tabindex="-1" role="dialog" aria-labelledby="complaintModalTitle"
        aria-hidden="true">

        <div class="modal-dialog" role="document">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close"
                        data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>

                    <h4 class="modal-title" id="complaintModalTitle">
                        <i class="fa fa-list-alt"></i>
                        Complaint Details
                    </h4>
                </div>

                <div class="modal-body">

                    <asp:UpdatePanel ID="Panel" runat="server"
                        UpdateMode="Conditional">
                        <ContentTemplate>

                            <div class="complaint-table-wrap">

                                <asp:GridView ID="GridView1"
                                    runat="server"
                                    DataKeyNames="Complaint_No"
                                    AutoGenerateColumns="false"
                                    CssClass="table table-bordered complaint-grid"
                                    CellPadding="5"
                                    CellSpacing="0"
                                    GridLines="None"
                                    EmptyDataText="No Record Found">

                                    <Columns>

                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="Complaint_No"
                                            HeaderText="Complaint No" />

                                        <asp:BoundField DataField="Department"
                                            HeaderText="Department" />

                                        <asp:BoundField DataField="Area"
                                            HeaderText="Area" />

                                        <asp:BoundField DataField="Status"
                                            HeaderText="Status" />

                                        <asp:BoundField DataField="Room"
                                            HeaderText="Room No" />

                                        <asp:BoundField DataField="CreateDate"
                                            HeaderText="Created Date" />

                                        <asp:BoundField DataField="AttemptDate"
                                            HeaderText="Warden Approval Date" />

                                        <asp:BoundField DataField="DRD"
                                            HeaderText="Resolve Date" />

                                        <asp:BoundField DataField="CloseDate"
                                            HeaderText="Close Date" />

                                        <asp:BoundField DataField="Remarks"
                                            HeaderText="Remarks" />

                                    </Columns>

                                </asp:GridView>

                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>

                <div class="modal-footer">
                    <button type="button"
                        class="btn btn-default complaint-btn"
                        data-dismiss="modal">
                        <i class="fa fa-times"></i> Close
                    </button>
                </div>

            </div>
        </div>
    </div>

    <!--
      IMPORTANT:
      Use the jQuery and Bootstrap JS already loaded by your master page.
      If they are not loaded by the master page, add them ONCE, in this order:
      jQuery -> Bootstrap JS -> ASP.NET AJAX scripts.
    -->

    <script type="text/javascript">
        function showComplaintModal() {
            if (window.jQuery &&
                typeof jQuery.fn.modal === "function") {
                jQuery("#complaintModal").modal({
                    backdrop: true,
                    keyboard: true,
                    show: true
                });
            } else {
                alert("Bootstrap modal JavaScript is not loaded. Please check IndexMaster.master.");
            }
        }

        function hideComplaintModal() {
            if (window.jQuery &&
                typeof jQuery.fn.modal === "function") {
                jQuery("#complaintModal").modal("hide");
            }
        }

        (function () {
            function initializeComplaintGrid() {
                if (window.jQuery &&
                    jQuery.fn &&
                    typeof jQuery.fn.footable === "function") {
                    jQuery("#<%= OrderList1.ClientID %>, #<%= GridView1.ClientID %>").footable();
                }
            }

            if (window.jQuery) {
                jQuery(function () {
                    initializeComplaintGrid();

                    if (window.Sys &&
                        Sys.WebForms &&
                        Sys.WebForms.PageRequestManager) {
                        var prm = Sys.WebForms.PageRequestManager.getInstance();

                        if (prm) {
                            prm.add_endRequest(function () {
                                initializeComplaintGrid();
                            });
                        }
                    }
                });
            }
        })();
    </script>

</asp:Content>
