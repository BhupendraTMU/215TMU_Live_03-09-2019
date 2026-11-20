<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="AccomdationList.aspx.cs" Inherits="Faculty_AccomdationList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Accommodation List</title>

    <%-- <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />--%>

    <style>
        body {
            font-family: Arial;
            background: #f5f5f5;
        }

        .report-container {
            width: 1200px;
            margin: 20px auto;
            background: #fff;
            border: 2px solid #000;
        }

        .report-header {
            border-bottom: 2px solid #000;
        }

        .logo {
            width: 90px;
            height: 90px;
        }

        .title {
            font-size: 34px;
            font-weight: bold;
        }

        .sub-title {
            font-size: 28px;
            font-weight: bold;
        }

        .month {
            font-size: 18px;
            font-weight: bold;
            text-align: right;
            padding-right: 20px;
        }

        .section-title {
            background: #fff;
            font-size: 24px;
            font-weight: bold;
            text-align: center;
            border-top: 2px solid #000;
            border-bottom: 2px solid #000;
            padding: 8px;
        }

        .yellow-strip {
            background: #fff3a3;
            height: 35px;
            border-bottom: 2px solid #000;
        }

        .grid th {
            background: #d9d9d9;
            text-align: center;
            font-weight: bold;
            border: 1px solid #000;
            padding: 8px;
        }

        .grid td {
            border: 1px solid #000;
            padding: 6px;
        }

        .total {
            font-weight: bold;
            background: #efefef;
        }

        .text-right {
            text-align: right;
        }

        .text-center {
            text-align: center;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="report-container">

        <!-- Header -->
        <table style="width: 100%;" class="report-header">
            <tr>
                <td style="width: 120px; padding: 10px;">
                    <asp:Image ID="imgLogo" runat="server"
                        ImageUrl="~/Images/tmulogo.png"
                        CssClass="logo" />
                </td>

                <td class="text-center">
                    <br />
                    <div class="title">TMU &amp; TMIMT</div>
                    <br />
                    <br />
                    <div class="sub-title">Accommodation List</div>
                </td>

                <td style="width: 220px;" class="month">Month :-
                    <asp:Label ID="lblMonth" runat="server" Text="Jan-26"></asp:Label>
                </td>
            </tr>
        </table>

        <!-- Section Title -->
        <div class="section-title">
            C. List of employees Fixed HRA provided by GVC sir in June 2025 at the time of Increment
   
        </div>

        <div class="yellow-strip"></div>

        <!-- Grid -->

        <asp:GridView ID="gvAccommodation"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="grid"
            Width="100%"
            ShowFooter="true">

            <Columns>

                <asp:TemplateField HeaderText="Sl. No." ItemStyle-CssClass="column" HeaderStyle-CssClass="column">

                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <ItemStyle Width="2%" />
                </asp:TemplateField>

                <asp:BoundField HeaderText="Employee Code" DataField="No_" />

                <asp:BoundField HeaderText="Employee Name" DataField="First Name" />

                <asp:BoundField HeaderText="Unit Code" DataField="Global Dimension 1 Code" />

                <asp:BoundField HeaderText="Designation" DataField="Job Title_Grade Desc" />

                <asp:BoundField HeaderText="Department" DataField="Department Name" />

                <asp:BoundField HeaderText="Salary Rate" DataField="Salary"  DataFormatString="{0:N0}">
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>

                <asp:BoundField HeaderText="Fixed HRA" DataField="Fixed HRA Amount"  DataFormatString="{0:N0}">
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>

                <asp:BoundField HeaderText="Monthly CTC" DataField="Total"  DataFormatString="{0:N0}">
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>

            </Columns>

            <FooterStyle CssClass="total" />

        </asp:GridView>

    </div>




</asp:Content>

