<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="EquipmentReaport.aspx.cs" Inherits="Faculty_EquipmentReaport" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .custom-modal {
            max-width: 90%; /* ya 95% / 100% */
        }

        .super-modal {
            max-width: 95%; /* 90%–100% adjust kar sakte ho */
        }

        .modal-body {
            overflow-x: auto;
        }

        .modern-grid {
            width: 100%;
            border-collapse: collapse;
            font-size: 13px;
        }

            .modern-grid th {
                background: linear-gradient(to right, #3b5bd6, #2a3fa0);
                color: #fff;
                padding: 10px;
                text-align: center;
                white-space: nowrap;
            }

            .modern-grid td {
                padding: 8px 10px;
                border-bottom: 1px solid #ddd;
                vertical-align: middle;
            }

            .modern-grid tr:nth-child(even) {
                background-color: #f9f9f9;
            }

        /* Fix button */
        .btn-accessories {
            background-color: #2d6ca2;
            color: #fff;
            padding: 4px 10px;
            border-radius: 4px;
            text-decoration: none;
            font-size: 12px;
            display: inline-block;
        }

            .btn-accessories:hover {
                background-color: #1f4e78;
            }

        /* Prevent header breaking */
        .modern-grid th,
        .modern-grid td {
            white-space: nowrap;
        }

            /* Allow description wrap only */
            .modern-grid td:nth-child(3) {
                white-space: normal;
            }

        .dashboard {
            display: flex;
            gap: 20px;
            justify-content: space-between;
            margin-top: 20px;
            flex-wrap: wrap;
        }

            .dashboard a {
                text-decoration: none;
                width: 32%;
            }

        .card {
            border-radius: 12px;
            padding: 25px;
            color: #fff;
            text-align: center;
            transition: 0.3s;
            box-shadow: 0 4px 12px rgba(0,0,0,0.1);
            cursor: pointer;
        }

            .card h2 {
                font-size: 32px;
                margin: 0;
                font-weight: bold;
            }

            .card p {
                margin-top: 10px;
                font-size: 16px;
                letter-spacing: 1px;
            }

        /* Colors */
        .eq {
            background: linear-gradient(135deg, #4CAF50, #2E7D32);
        }

        .loc {
            background: linear-gradient(135deg, #2196F3, #1565C0);
        }

        .acc {
            background: linear-gradient(135deg, #FF9800, #EF6C00);
        }

        /* Hover effect */
        .card:hover {
            transform: translateY(-5px);
            box-shadow: 0 8px 20px rgba(0,0,0,0.2);
        }

        .modal-backdrop.show:nth-of-type(2) {
            z-index: 1060;
        }

        #accessoriesModal {
            z-index: 1070;
        }
    </style>
    <script>
        Sys.Application.add_load(function () {

            $('#accessoriesModal').off('hidden.bs.modal').on('hidden.bs.modal', function () {
                $('#FAModal').modal('show');
            });

        });

</script>
    <script>
        function printDiv() {

            var content = document.getElementById("printArea").outerHTML;

            var myWindow = window.open('', '', 'width=1000,height=700');

            myWindow.document.write(`
        <html>
        <head>
            <title>Print</title>

            <style>
                body {
                    font-family: Arial;
                    margin: 20px;
                }

                table {
                    width: 100%;
                    border-collapse: collapse;
                }

                td, th {
                    border: 1px solid #000;
                    padding: 6px;
                    font-size: 12px;
                }

                .section-title {
                    background: #d0d8e8;
                    font-weight: bold;
                    text-align: center;
                }

                .fa-box {
                    border: 2px solid #000;
                    margin-top: 10px;
                    padding: 6px;
                }
            </style>

        </head>

        <body onload="window.print(); window.close();">

            ${content}

        </body>
        </html>
    `);

            myWindow.document.close();
        }
        function printDiv1() {

            var content = document.getElementById("printArea1").outerHTML;

            var myWindow = window.open('', '', 'width=1000,height=700');

            myWindow.document.write(`
    <html>
    <head>
        <title>Print</title>

        <style>
            body {
                font-family: Arial;
                margin: 20px;
            }

            table {
                width: 100%;
                border-collapse: collapse;
            }

            td, th {
                border: 1px solid #000;
                padding: 6px;
                font-size: 12px;
            }

            .section-title {
                background: #d0d8e8;
                font-weight: bold;
                text-align: center;
            }

            .fa-box {
                border: 2px solid #000;
                margin-top: 10px;
                padding: 6px;
            }
        </style>

    </head>

    <body onload="window.print(); window.close();">

        ${content}

    </body>
    </html>
`);

            myWindow.document.close();
        }
</script>
    <style>
        /* ===== MODAL FULL WIDTH ===== */
        .custom-modal {
            max-width: 40% !important;
            width: 40%;
        }

        /* ===== SCROLL FIX ===== */
        .modal-body {
            overflow-x: auto;
        }

        /* ===== A4 LAYOUT ===== */


        /* ===== PRINT ===== */
        @media print {

            body {
                margin: 0;
            }

                /* hide everything */
                body * {
                    visibility: hidden;
                }

            /* show only print area */
            #printArea, #printArea * {
                visibility: visible;
            }

            #printArea {
                position: absolute;
                left: 0;
                top: 0;
                width: 210mm;
                background: white;
            }
        }

        /* ===== HEADER ===== */
        .header {
            text-align: center;
            margin-bottom: 10px;
        }

            .header h2 {
                margin: 0;
            }

            .header h4 {
                margin: 0;
                font-weight: normal;
            }

        /* ===== TABLE ===== */
        .report-table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 10px;
        }

            .report-table td, .report-table th {
                border: 1px solid #000;
                padding: 6px;
                font-size: 12px;
            }

        .section-title {
            background: #d9e1f2;
            font-weight: bold;
            text-align: center;
        }

        /* ===== FA BOX ===== */
        .fa-box {
            border: 2px solid #000;
            margin-top: 15px;
            padding: 8px;
            page-break-inside: avoid;
        }
    </style>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="dashboard">

        <asp:LinkButton ID="lnkEquipment" runat="server" OnClick="lnkEquipment_Click">
            <div class="card eq">
                <h2>
                    <asp:Label ID="lblEquipmentCount" runat="server" /></h2>
                <p>Equipment</p>
            </div>
        </asp:LinkButton>

        <asp:LinkButton ID="lnkLocation" runat="server" OnClick="lnkLocation_Click">
            <div class="card loc">
                <h2>
                    <asp:Label ID="lblLocationCount" runat="server" /></h2>
                <p>Location</p>
            </div>
        </asp:LinkButton>

        <asp:LinkButton ID="lnkAccessories" runat="server" OnClick="lnkAccessories_Click">
            <div class="card acc">
                <h2>
                    <asp:Label ID="lblAccessoriesCount" runat="server" /></h2>
                <p>Accessories</p>
            </div>
        </asp:LinkButton>

    </div>
    <br />

    <div style="margin-bottom: 15px;">

        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"
            placeholder="Search here..." Style="width: 250px; display: inline-block;"></asp:TextBox>

        <asp:Button ID="btnSearch" runat="server" Text="Search"
            CssClass="btn btn-primary" OnClick="btnSearch_Click" />

        <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click"
            CssClass="btn btn-success" />

    </div>
    <br />
    <div class="grid-container" style="overflow: scroll; height: 450px">
        <asp:GridView ID="gvReportEQ" runat="server" Visible="false" OnRowCommand="gvReportEQ_RowCommand" CssClass="modern-grid" AutoGenerateColumns="false">
            <Columns>


                <asp:TemplateField HeaderText="S.No">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:BoundField DataField="Item No" HeaderText="Item No" ItemStyle-Width="350px" />
                <asp:BoundField DataField="Description" HeaderText="Description" />
                <asp:TemplateField HeaderText="Count">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkAccessories" runat="server"
                            Text='<%# Eval("C") %>'
                            CommandName="ViewFA"
                            CommandArgument='<%# Eval("Item No") %>'
                            CssClass="btn btn-sm btn-primary" BackColor="Green" ForeColor="White">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

               <%-- <asp:TemplateField HeaderText="Details">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkdetails" runat="server"
                            Text="View details"
                            CommandName="Viewdetails"
                            CommandArgument='<%# Eval("Item No") %>'
                            CssClass="btn btn-sm btn-primary" BackColor="Green" ForeColor="White">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>

            </Columns>
        </asp:GridView>

        <asp:GridView ID="grdLocation" runat="server" OnRowCommand="grdLocation_RowCommand" CssClass="modern-grid" Visible="false" AutoGenerateColumns="false">
            <Columns>


                <asp:TemplateField HeaderText="S.No">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:BoundField DataField="Code" HeaderText="Location Code" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:TemplateField HeaderText="Item Details">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEquip" runat="server"
                            Text="View"
                            CommandName="ViewItem"
                            CommandArgument='<%# Eval("Code") %>'
                            CssClass="btn btn-sm btn-primary">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>



            </Columns>
        </asp:GridView>


        <asp:GridView ID="GVAccess" runat="server"
            CssClass="modern-grid"
            Visible="false"
            AutoGenerateColumns="false">

            <Columns>


                <asp:TemplateField HeaderText="S.No">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="Accessories" HeaderText="Accessories" />

                <asp:BoundField DataField="Work Description" HeaderText="Work Description" />

                <asp:BoundField DataField="Location Code" HeaderText="Location Code" />


                <asp:BoundField DataField="Equipment No_" HeaderText="Equipment No" />


                <asp:BoundField DataField="Name of Equipment" HeaderText="Equipment Name" />


                <asp:BoundField DataField="Work Description" HeaderText="Work Description" />


                <asp:BoundField DataField="Quantity" HeaderText="Quantity" DataFormatString="{0:N2}" />

                <asp:BoundField DataField="Approx Amount" HeaderText="Approx Amount" DataFormatString="{0:N2}" />
                <asp:BoundField DataField="Serial Number" HeaderText="Serial Number"  />


            </Columns>
        </asp:GridView>

    </div>

    <div class="grid-container">
        <asp:GridView ID="gvReportLocation" runat="server" CssClass="modern-grid"></asp:GridView>
    </div>

    <div class="grid-container">
        <asp:GridView ID="gvAsseseries" runat="server" CssClass="modern-grid"></asp:GridView>
    </div>

    <div class="modal fade modal-xl" id="FAModal" tabindex="-1" role="dialog" style="overflow: scroll">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content modal-xl">

                <div class="modal-header">
                    <h5 class="modal-title">FA Details</h5>
                    <asp:Button ID="btnExportAccessories" runat="server" Text="Export"
                        CssClass="btn btn-success btn-sm"
                        OnClick="btnExportAccessories_Click" />
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <div class="modal-body">
                    <asp:GridView ID="gvFA" runat="server" CssClass="modern-grid"
                        AutoGenerateColumns="false" OnRowCommand="gvFA_RowCommand">

                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:BoundField DataField="No_" HeaderText="No" ItemStyle-Width="90px" />--%>

                            <asp:TemplateField HeaderText="No_" ItemStyle-Width="140px">
                                <ItemTemplate>
                                    <asp:LinkButton
                                        ID="lnkAccessories"
                                        runat="server"
                                        Text='<%# Eval("No_") %>'
                                        CommandName="ViewAccessories"
                                        CommandArgument='<%# Eval("No_") %>'
                                        CssClass="btn btn-sm btn-outline-primary">
        </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="Description" HeaderText="Description" />

                            <asp:BoundField DataField="Location Code" HeaderText="Location" ItemStyle-Width="100px" />

                            <asp:BoundField DataField="Warranty Date" HeaderText="Warranty"
                                DataFormatString="{0:dd/MM/yyyy}" />

                            <asp:BoundField DataField="Serial No_" HeaderText="Serial" />

                            <asp:BoundField DataField="Next Service Date" HeaderText="Next Service"
                                DataFormatString="{0:dd/MM/yyyy}" />

                            <asp:BoundField DataField="Brand" HeaderText="Brand" />

                            <asp:BoundField DataField="Model" HeaderText="Model" />

                            <asp:BoundField DataField="Date of Installation" HeaderText="Install Date"
                                DataFormatString="{0:dd/MM/yyyy}" />



                            <asp:TemplateField HeaderText="Department" ItemStyle-Width="140px">
                                <ItemTemplate>
                                    <asp:LinkButton
                                        ID="lnkEqipment"
                                        runat="server"
                                        Text='<%# Eval("Department Name") %>'
                                        CommandName="ViewEquipment"
                                        CommandArgument='<%# Eval("Department Name") %>'
                                        CssClass="btn btn-sm btn-outline-primary">
</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>



                        </Columns>
                    </asp:GridView>
                </div>

            </div>
        </div>
    </div>



    <div class="modal fade modal-xl" id="accessoriesModal" tabindex="-1" role="dialog" style="overflow: scroll">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">

                <div class="modal-body">

                    <div id="printArea1">




                        <table class="report-table">
                            <tr class="section-title">
                                <td colspan="6">FIXED ASSET DETAILS</td>
                            </tr>
                        </table>


                        <asp:Repeater ID="rptFA1" runat="server" OnItemDataBound="rptFA1_ItemDataBound">

                            <ItemTemplate>

                                <div class="fa-box">

                                    <!-- FA TABLE -->
                                    <table class="report-table">

                                        <tr>
                                            <th>FA No</th>
                                            <th>Equipment</th>
                                            <th>Serial No</th>
                                            <th>Location</th>
                                            <th>Install Date</th>
                                            <th>AMC Period</th>
                                        </tr>

                                        <tr>
                                            <td><%# Eval("EquipmentNo") %></td>
                                            <td><%# Eval("EquipmentName") %></td>
                                            <td><%# Eval("SerialNo") %></td>
                                            <td><%# Eval("LocationName") %></td>
                                            <td><%# Eval("InstallationDate") %></td>
                                            <td><%# Eval("AMCStart") %> - <%# Eval("AMCEnd") %></td>
                                        </tr>

                                    </table>


                                    <table class="report-table">

                                        <tr class="section-title">
                                            <td colspan="5">ACCESSORIES DETAILS</td>
                                        </tr>

                                        <tr>
                                            <th>Accessory</th>
                                            <th>Description</th>
                                            <th>Qty</th>
                                            <th>Amount</th>
                                            <th>Date</th>
                                        </tr>

                                        <asp:Repeater ID="rptAccessories123" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("Accessories") %></td>
                                                    <td><%# Eval("AccessoriesDesc") %></td>
                                                    <td style="text-align: center;"><%# Eval("Quantity") %></td>
                                                    <td style="text-align: right;"><%# Eval("Amount") %></td>
                                                    <td><%# Eval("RepairDate") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </table>

                                </div>

                            </ItemTemplate>
                        </asp:Repeater>

                    </div>

                </div>

                <div class="modal-footer">
                    <button class="btn btn-primary" onclick="printDiv1()">Print</button>
                </div>

            </div>
        </div>
    </div>



    <div class="modal fade modal-xl" id="ItemModal" tabindex="-1" role="dialog" style="overflow: scroll">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content modal-xl">
                <div class="modal-header">
                    <h5 class="modal-title">Item Details</h5>

                    <asp:Button ID="Button2" runat="server" Text="Export"
                        CssClass="btn btn-success btn-sm ml-2"
                        OnClick="Button2_Click" />

                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <div class="modal-body" style="height: 700px; overflow: scroll">

                    <div class="table-container">
                        <asp:GridView ID="GridView1" runat="server"
                            CssClass="table table-bordered table-striped sticky-grid"
                            HeaderStyle-CssClass="thead-dark">
                        </asp:GridView>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="detailsModal">
        <div class="modal-dialog custom-modal">
            <div class="modal-content">

                <div class="modal-body">

                    <div id="printArea">

                        <!-- 🔷 HEADER -->
                        <div class="header">
                            <h2>TMU HOSPITAL</h2>
                            <h4>EQUIPMENT INSTALLATION REPORT</h4>
                        </div>

                        <!-- 🔷 ITEM DETAILS -->
                        <%--<table class="report-table">
                            <tr class="section-title">
                                <td colspan="4">ITEM DETAILS</td>
                            </tr>

                            <tr>
                                <td><b>Item No</b></td>
                                <td>
                                    <asp:Label ID="lblItemNo" runat="server" /></td>

                                <td><b>PO No</b></td>
                                <td>
                                    <asp:Label ID="lblPO" runat="server" /></td>
                            </tr>

                            <tr>
                                <td><b>Purchase Date</b></td>
                                <td>
                                    <asp:Label ID="lblPurchase" runat="server" /></td>

                                <td><b>Vendor</b></td>
                                <td>
                                    <asp:Label ID="lblVendor" runat="server" /></td>
                            </tr>
                        </table>--%>

                        <!-- 🔷 FA HEADER -->
                        <table class="report-table">
                            <tr class="section-title">
                                <td colspan="6">FIXED ASSET DETAILS</td>
                            </tr>
                        </table>

                        <!-- 🔷 FA LOOP -->
                        <asp:Repeater ID="rptFA" runat="server" OnItemDataBound="rptFA_ItemDataBound">

                            <ItemTemplate>

                                <div class="fa-box">

                                    <!-- FA TABLE -->
                                    <table class="report-table">

                                        <tr>
                                            <th>FA No</th>
                                            <th>Equipment</th>
                                            <th>Serial No</th>
                                            <th>Location</th>
                                            <th>Install Date</th>
                                            <th>AMC Period</th>
                                        </tr>

                                        <tr>
                                            <td><%# Eval("EquipmentNo") %></td>
                                            <td><%# Eval("EquipmentName") %></td>
                                            <td><%# Eval("SerialNo") %></td>
                                            <td><%# Eval("LocationName") %></td>
                                            <td><%# Eval("InstallationDate") %></td>
                                            <td><%# Eval("AMCStart") %> - <%# Eval("AMCEnd") %></td>
                                        </tr>

                                    </table>

                                    <!-- ACCESSORIES -->
                                    <table class="report-table">

                                        <tr class="section-title">
                                            <td colspan="5">ACCESSORIES DETAILS</td>
                                        </tr>

                                        <tr>
                                            <th>Accessory</th>
                                            <th>Description</th>
                                            <th>Qty</th>
                                            <th>Amount</th>
                                            <th>Date</th>
                                        </tr>

                                        <asp:Repeater ID="rptAccessories" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("Accessories") %></td>
                                                    <td><%# Eval("AccessoriesDesc") %></td>
                                                    <td style="text-align: center;"><%# Eval("Quantity") %></td>
                                                    <td style="text-align: right;"><%# Eval("Amount") %></td>
                                                    <td><%# Eval("RepairDate") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </table>

                                </div>

                            </ItemTemplate>
                        </asp:Repeater>

                    </div>

                </div>

                <div class="modal-footer">
                    <button class="btn btn-primary" onclick="printDiv()">Print</button>
                </div>

            </div>
        </div>
    </div>



</asp:Content>

