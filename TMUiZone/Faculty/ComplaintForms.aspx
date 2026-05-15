<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="ComplaintForms.aspx.cs" Inherits="Faculty_ComplaintForms" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
    <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js">
    </script>


    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script type="text/jscript">
        function Search() {

            var t = document.getElementById("txtSearching").value;

            $('#cphContentBody_chkJudges tbody tr').each(function () {

                var str = $(this).text();
                if (str.toUpperCase().indexOf(t.toUpperCase()) >= 0) {

                    $(this).show();
                }
                else {
                    $(this).hide();
                }
            }
            );

        }

    </script>
    <script type="text/javascript">
        function initSelect2() {
            $('.select2').select2({
                placeholder: "Select Equipment",
                allowClear: true,
                width: '100%'
            });
        }

        // Initial load
        $(document).ready(function () {
            initSelect2();
        });

        // After ASP.NET postback
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            initSelect2();
        });
    </script>
    <style>
        .red-border {
            border: 1px solid red;
        }

        .grdComplaintall {
            font-family: Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            .grdComplaintall td, .grdComplaintall th {
                border: 1px solid #ddd;
                padding: 8px;
            }

            .grdComplaintall tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            .grdComplaintall tr:hover {
                background-color: #ddd;
            }

            .grdComplaintall th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #04AA6D;
                color: white;
            }

        .btn {
            padding: 4px 10px;
            border-radius: 4px;
            color: #fff;
            font-size: 13px;
        }

        .complaint-header {
            background: linear-gradient(135deg, #0d6efd, #084298);
            color: #fff;
            padding: 14px 20px;
            border-radius: 8px 8px 0 0;
            box-shadow: 0 3px 8px rgba(0,0,0,0.15);
        }

            .complaint-header h5 {
                letter-spacing: 0.5px;
            }

            .complaint-header i {
                color: #ffc107;
            }
    </style>


    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons/font/bootstrap-icons.css" rel="stylesheet">

    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style>
        #reader {
            width: 300px;
            margin: auto;
        }

        @media (max-width: 768px) {

            .form-label {
                font-size: 1.1rem; /* Bigger labels */
                font-weight: 600;
            }

            .form-control {
                font-size: 1.05rem; /* Bigger input text */
                padding: 0.6rem 0.75rem;
            }

            .btn {
                font-size: 1.05rem;
                padding: 0.6rem 1.2rem;
            }

            .card-header h5 {
                font-size: 1.2rem;
            }

            .select-scroll {
                white-space: nowrap;
                overflow-x: auto;
                overflow-y: hidden;
            }

                /* Improve scrollbar visibility */
                .select-scroll::-webkit-scrollbar {
                    height: 6px;
                    width: 6px;
                }

                .select-scroll::-webkit-scrollbar-thumb {
                    background: #999;
                    border-radius: 4px;
                }
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">









    <div class="card shadow">
        <div class="card-header complaint-header text-center">
            <div class="d-flex align-items-center justify-content-center gap-2">
                <i class="bi bi-tools fs-4"></i>
                <h5 class="mb-0 fw-bold">Equipment Complaint Entry</h5>
            </div>
        </div>

        <div class="card-body">


            <!-- Equipment No -->
            <div class="col-12 col-md-6">
                <label class="form-label">Equipment No</label>
                <asp:DropDownList ID="ddlEquipmentNo" runat="server"
                    CssClass="select2" OnSelectedIndexChanged="ddlEquipmentNo_SelectedIndexChanged" AutoPostBack="true" />
            </div>

            <!-- Make -->
            <div class="col-12 col-md-6">
                <label class="form-label">Make</label>
                <asp:TextBox ID="txtMake" runat="server"
                    Enabled="false" CssClass="form-control fs-5" />
            </div>

            <!-- Model -->
            <div class="col-12 col-md-6">
                <label class="form-label">Model</label>
                <asp:TextBox ID="txtModel" runat="server"
                    Enabled="false" CssClass="form-control fs-5" />
            </div>

            <!-- Serial No -->
            <div class="col-12 col-md-6">
                <label class="form-label">Serial No</label>
                <asp:TextBox ID="txtSerialNo" runat="server"
                    Enabled="false" CssClass="form-control fs-5" />
            </div>

            <!-- Employee Code -->
            <div class="col-12 col-md-6">
                <label class="form-label">Employee Code</label>
                <asp:TextBox ID="txtEmployeeCode" runat="server"
                    Enabled="false" CssClass="form-control fs-5" />
            </div>

            <!-- Employee Name -->
            <div class="col-12 col-md-6">
                <label class="form-label">Employee Name</label>
                <asp:TextBox ID="txtEmployeeName" runat="server"
                    Enabled="false" CssClass="form-control fs-5" />
            </div>

            <!-- Complaint Date -->
            <div class="col-12 col-md-6">
                <label class="form-label">Complaint Date</label>
                <asp:TextBox ID="txtComplaintDate" runat="server"
                    Enabled="false" CssClass="form-control fs-5" />
            </div>
            <div class="col-12 col-md-6">
                <label class="form-label">Tag ID</label>
                <asp:TextBox ID="txtTagID" runat="server"
                    Enabled="false" CssClass="form-control fs-5" />
            </div>
            <div class="col-12 col-md-6">
                <label class="form-label">Equipment Name</label>
                <asp:TextBox ID="txtEqname" runat="server"
                    Enabled="false" CssClass="form-control fs-5" />
            </div>
            <div class="col-12 col-md-6">
                <label class="form-label">Department</label>
                <asp:TextBox ID="txtDepartment" runat="server"
                    Enabled="false" CssClass="form-control fs-5" />
            </div>
            <!-- Complaint Description -->



            <div class="col-12 col-md-12">
                <label class="form-label">Complaint Description</label>
                <asp:TextBox ID="txtComplaintDescription" runat="server" CssClass="form-control fs-5"
                    TextMode="MultiLine" Rows="3" />
            </div>

            <!-- Buttons -->
            <div class="col-12 col-md-12">
                <asp:Button ID="btnSave" runat="server"
                    Text="Save" CssClass="btn btn-success px-4 me-2" OnClick="btnSave_Click" />

                <asp:Button ID="btnReset" runat="server"
                    Text="Reset" CssClass="btn btn-success px-4 me-2" />
            </div>

        </div>

    </div>





    <div class="container-fluid mt-4">
        <div class="card shadow-sm">
            <div class="card-header bg-dark text-white">
                <h6 class="mb-0">Complaint List</h6>
            </div>

            <div class="card-body table-responsive">
                <asp:GridView ID="grdComplaintall"
                    runat="server" AutoGenerateColumns="false" CssClass="grdComplaintall" BackColor="White" BorderColor="#E7E7FF"
                    EmptyDataText="There are no data records to display." BorderStyle="Solid" BorderWidth="2px" CellPadding="3">

                    <Columns>
                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <%# Container.DataItemIndex +1 %>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Complaint No" ItemStyle-Width="2%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:Label ID="lblComplaintNo" runat="server" Text='<%# Bind("[ComplaintNo]") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:BoundField DataField="EquipmentNo" HeaderText="Equipment No" />

                        <asp:BoundField DataField="Make" HeaderText="Make" />
                        <asp:BoundField DataField="Model" HeaderText="Model" />
                        <asp:BoundField DataField="SerialNo" HeaderText="Serial No" />
                        <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" />
                        <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />

                        <asp:BoundField DataField="ComplaintDate"
                            HeaderText="Complaint Date"
                            DataFormatString="{0:dd-MMM-yyyy}" />

                        <asp:BoundField DataField="ComplaintDescription"
                            HeaderText="Complaint Description" />
                        <asp:BoundField DataField="BMStatus"
    HeaderText="BME Status" />

                        <asp:TemplateField HeaderText="Close Complaint" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <asp:Button ID="btnClose"
                                    runat="server"
                                    Text="Close"
                                    BackColor="#ff6600"
                                    Visible='<%# Eval("BMStatus").ToString() == "Close" %>'
                                    CommandName="ManualPresent"
                                    CommandArgument='<%# Eval("ComplaintNo") %>'
                                    OnClientClick="return confirm('Are you sure you want to close this complaint?');"
                                    OnCommand="btnClose_Command" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                    <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" />
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>

