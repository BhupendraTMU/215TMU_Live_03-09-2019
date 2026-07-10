<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="Fixed_HRA_report.aspx.cs" Inherits="Faculty_Fixed_HRA_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Fixed HRA Report</title>

    <style>
        body {
            font-family: Arial;
            font-size: 12px;
        }

        .report-table {
            width: 100%;
            border-collapse: collapse;
        }

            .report-table th,
            .report-table td {
                border: 1px solid #000;
                padding: 4px;
            }

        .heading {
            text-align: center;
            font-size: 28px;
            font-weight: bold;
        }

        .subheading {
            text-align: center;
            font-size: 22px;
            font-weight: bold;
        }

        .section {
            text-align: center;
            font-size: 18px;
            font-weight: bold;
            background-color: #f3f3f3;
        }

        .month {
            text-align: right;
            font-weight: bold;
        }

        .headerrow {
            background-color: #d9d9d9;
            font-weight: bold;
        }

        @media print {
            .noprint {
                display: none;
            }
        }
    </style>
    <style>
        @media print {
            body * {
                visibility: hidden;
            }

            #divprint, #divprint * {
                visibility: visible;
            }

            #divprint {
                position: absolute;
                left: 0;
                top: 0;
                width: 100%;
            }

            .noprint {
                display: none !important;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="noprint" style="margin-bottom: 10px;">
        <asp:Button ID="btnPrint" runat="server"
            Text="Print"
            OnClientClick="window.print(); return false;" />
    </div>
    <div id="divprint">

        <table width="100%">
            <tr>
                <td width="15%">
                    <img src="../images/tmulogo.png" height="80" />
                </td>

                <td align="center">
                    <div class="heading">TMU & TMIMT</div>
                    <br />
                    <div class="subheading">Accommodation List</div>
                </td>

                <td class="month">Month :-
                <asp:Label ID="lblMonth" runat="server"></asp:Label>
                </td>
            </tr>
        </table>

        <br />

        <table class="report-table">
            <tr>
                <td colspan="9" class="section">A. List of employees covered in Fixed HRA criteria
                    (Perquisite provided through salary sheets)
                </td>
            </tr>
        </table>

        <asp:GridView ID="gvReport"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="report-table"
            ShowHeader="true">

            <Columns>

                <asp:TemplateField HeaderText="Sl. No." ItemStyle-CssClass="column" HeaderStyle-CssClass="column">

                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <ItemStyle Width="2%" />
                </asp:TemplateField>

                <asp:BoundField DataField="Employee Code"
                    HeaderText="Employee Code" />

                <asp:BoundField DataField="First Name"
                    HeaderText="Employee Name" />

                <asp:BoundField DataField="Global Dimension 1 Code"
                    HeaderText="Unit Code" />

                <asp:BoundField DataField="Job Title_Grade Desc"
                    HeaderText="Designation" />

                <asp:BoundField DataField="Department Name"
                    HeaderText="Department" />

                <asp:BoundField DataField="Salary"
                    HeaderText="Salary Rate"
                    DataFormatString="{0:N0}" />

                <asp:BoundField DataField="Fixed HRA Amount"
                    HeaderText="Fixed HRA"
                    DataFormatString="{0:N0}" />

              

               <asp:TemplateField HeaderText="Monthly CTC">
    <ItemStyle CssClass="right" />
    <ItemTemplate>
        <%# String.Format("{0:N0}",
            Convert.ToDecimal(Eval("Salary")) +
            Convert.ToDecimal(Eval("Fixed HRA Amount"))) %>
    </ItemTemplate>
</asp:TemplateField>

            </Columns>

            <HeaderStyle CssClass="headerrow" />

        </asp:GridView>


    </div>
</asp:Content>

