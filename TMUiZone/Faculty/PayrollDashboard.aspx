<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="PayrollDashboard.aspx.cs" Inherits="Faculty_PayrollDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Accommodation Summary</title>
    <title>Accommodation Summary</title>

    <style type="text/css">
        body {
            font-family: Arial, Helvetica, sans-serif;
            margin: 20px;
        }

        .filter-section {
            text-align: right;
            margin-bottom: 15px;
        }

        .header-container {
            position: relative;
            text-align: center;
            margin-bottom: 10px;
        }

        .logo {
            position: absolute;
            left: 0;
            top: 0;
            width: 80px;
            height: 80px;
        }

        .header {
            font-size: 24px;
            font-weight: bold;
            padding-top: 15px;
        }

        .title {
            text-align: center;
            font-size: 20px;
            font-weight: bold;
            margin-top: 15px;
        }

        .subTitle {
            text-align: center;
            font-size: 16px;
            font-weight: bold;
            margin-bottom: 20px;
        }

        .grid {
            width: 100%;
            border-collapse: collapse;
        }

            .grid th {
                background-color: #d9d9d9;
                border: 1px solid #000;
                padding: 8px;
                text-align: center;
            }

            .grid td {
                border: 1px solid #000;
                padding: 6px;
            }

        .center {
            text-align: center;
        }

        .right {
            text-align: right;
        }

        .footerRow {
            font-weight: bold;
            background-color: #f2f2f2;
        }

        .btnSearch {
            padding: 4px 12px;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="filter-section">
        Academic Year :

           

        <asp:DropDownList ID="ddlAcademicYear"
            runat="server"
            Width="150px">
        </asp:DropDownList>

        &nbsp;&nbsp;

            Month :

           

        <asp:DropDownList ID="ddlMonth"
            runat="server"
            Width="120px">
        </asp:DropDownList>

        &nbsp;&nbsp;

           

        <asp:Button ID="btnSearch"
            runat="server"
            Text="Search"
            CssClass="btnSearch"
            OnClick="btnSearch_Click" />

    </div>

    <!-- Header -->
    <div class="header-container">

        <img src="../images/tmulogo.png" class="logo" />

        <div class="header">
            TMU &amp; TMIMT
           
        </div>

    </div>

    <!-- Report Title -->
    <div class="title">
        Accommodation Summary
       
    </div>

    <div class="subTitle">
        Fixed HRA/RFA
           
        <asp:Label ID="lblMonthYear" runat="server"></asp:Label>
    </div>

    <!-- Grid -->
    <asp:GridView ID="gvSummary"
        runat="server"
        AutoGenerateColumns="False"
        CssClass="grid"
        ShowFooter="True"
        OnRowDataBound="gvSummary_RowDataBound">

        <Columns>

            <asp:TemplateField HeaderText="Sl. No." ItemStyle-CssClass="column" HeaderStyle-CssClass="column">

                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
                <ItemStyle Width="2%" />
            </asp:TemplateField>

            <asp:BoundField DataField="Description"
                HeaderText="Description" />

            <asp:TemplateField HeaderText="No. of Employees">
                <ItemStyle CssClass="center" />

                <ItemTemplate>
                    <asp:LinkButton ID="lnkEmployees"
                        runat="server"
                        Text='<%# Eval("Employees") %>'
                        CommandArgument='<%# Eval("Employees") %>'
                        OnClick="lnkEmployees_Click">
        </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="SalaryAmount"
                HeaderText="Salary Amount"
                DataFormatString="{0:N0}">
                <ItemStyle CssClass="right" />
            </asp:BoundField>

            <asp:BoundField DataField="FixedHRA"
                HeaderText="Fixed HRA/RFA"
                DataFormatString="{0:N0}">
                <ItemStyle CssClass="right" />
            </asp:BoundField>

            <asp:BoundField DataField="MonthlyCTC"
                HeaderText="Monthly CTC"
                DataFormatString="{0:N0}">
                <ItemStyle CssClass="right" />
            </asp:BoundField>

        </Columns>

        <FooterStyle CssClass="footerRow" />

    </asp:GridView>


</asp:Content>

