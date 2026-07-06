<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master"
    AutoEventWireup="true"
    CodeFile="NAACSurveyReport.aspx.cs"
    Inherits="Faculty_NAACSurveyReport" %>

<%@ Register Assembly="System.Web.DataVisualization"
    Namespace="System.Web.UI.DataVisualization.Charting"
    TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>NAAC Survey Report</title>
    <script type="text/javascript">

        function PrintReport() {

            var printContents = document.getElementById('report-container');

            document.body.classList.add('printing');

            window.print();

            document.body.classList.remove('printing');
        }
    </script>
    <style>
        @page {
            margin: 8mm;
        }

        body {
            font-family: Calibri, Arial, sans-serif;
            font-size: 18px;
            zoom: 115%;
        }

        /* Main Report */
        .report-container {
            width: 100%;
            margin: 0;
            padding: 10px;
        }

        .grid {
            width: 100% !important;
            border-collapse: collapse;
        }

        .optionRow {
            font-size: 18px;
            line-height: 34px;
        }

        .question {
            font-size: 24px;
            font-weight: bold;
        }



        @page {
            margin: 8mm;
        }

        @media print {

            /* Hide Everything */
            body * {
                visibility: hidden !important;
            }

            /* Show Only Report */
            #report-container,
            #report-container * {
                visibility: visible !important;
            }

            /* Position Report */
            #report-container {
                position: absolute;
                left: 0;
                top: 0;
                width: 100% !important;
                max-width: 100% !important;
                margin: 0 !important;
                padding: 0 !important;
            }

            /* Hide Controls */
            .row,
            #btnShow,
            #btnPrint,
            .main-header,
            .main-sidebar,
            .content-header,
            .navbar,
            .footer,
            .breadcrumb {
                display: none !important;
            }

            /* Remove Layout Margins */
            .content-wrapper,
            .right-side,
            .content {
                margin: 0 !important;
                padding: 0 !important;
            }

            /* Report Font */
            body {
                font-family: Calibri, Arial, sans-serif !important;
                zoom: 130%;
            }

            /* Question */
            .question {
                font-size: 30px !important;
                font-weight: bold !important;
                line-height: 1.4 !important;
                margin-bottom: 15px !important;
            }

            /* Options */
            .optionRow {
                font-size: 22px !important;
                line-height: 40px !important;
                margin-left: 25px !important;
            }

            /* Table */
            .grid,
            .grid th,
            .grid td {
                border: 2px solid #000 !important;
                border-collapse: collapse;
            }
        }

        /* University Header */
        .report-container table:first-child {
            width: 100% !important;
        }

            .report-container table:first-child div:nth-child(1) {
                font-size: 24px !important;
                font-weight: bold !important;
            }

            .report-container table:first-child div:nth-child(2),
            .report-container table:first-child div:nth-child(3) {
                font-size: 16px !important;
            }

            .report-container table:first-child div:nth-child(4) {
                font-size: 22px !important;
                font-weight: bold !important;
            }

        /* Chart */
        img[src*="ChartImg"] {
            max-width: 100% !important;
            width: 100% !important;
            height: auto !important;
        }

        /* One Question Per Page */
        hr {
            page-break-after: always;
            border: none !important;
            margin: 0 !important;
        }

            hr:last-of-type {
                display: none;
            }

        /* Prevent Breaking */
        .question,
        .grid,
        table,
        img {
            page-break-inside: avoid !important;
        }

        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" style="margin-bottom: 20px;">

        <div class="col-md-3">
            <label><b>Academic Year</b></label>
            <asp:DropDownList ID="ddlAcademicYear"
                runat="server"
                CssClass="form-control">
            </asp:DropDownList>
        </div>

        <div class="col-md-2" style="padding-top: 25px;">
            <asp:Button ID="btnShow"
                runat="server"
                Text="Show Report"
                CssClass="btn btn-primary"
                OnClick="btnShow_Click" />
        </div>
        <asp:Button ID="btnPrint"
            runat="server"
            Text="Print / PDF"
            CssClass="btn btn-success"
            OnClientClick="PrintReport(); return false;" />

    </div>

    <hr />
    <div id="report-container" class="report-container">

        <table style="width: 98%;">
            <tr>
                <td style="width: 1%"></td>
                <td style="width: 12%; text-align: left">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/rightlogo.png" Width="55%" />
                </td>
                <td style="width: 70%; text-align: center;">
                    <div style="font-size: 20px; font-weight: bold;">
                        Teerthanker Mahaveer University, Moradabad
           
                    </div>

                    <div style="font-size: 14px; font-weight: bold;">
                        (Established under Govt. of U.P. Act No. 30, 2008)
           
                    </div>

                    <div style="font-size: 14px; font-weight: bold;">
                        Delhi Road, Moradabad (U.P.)
           
                    </div>

                    <div style="font-size: 18px; font-weight: bold; margin-top: 10px;">
                        Student Satisfaction Survey Report
                        <asp:Label ID="lblAcademicYear" runat="server"></asp:Label>
           
                    </div>
                </td>

                <td style="width: 15%;"></td>
                <td style="width: 10%; text-align: center"></td>
            </tr>
        </table>
        <br />
        <br />

        <asp:Repeater ID="rptQuestions"
            runat="server"
            OnItemDataBound="rptQuestions_ItemDataBound">

            <ItemTemplate>

                <div class="question" style="font-family: inherit; font-size: large; font-weight: bold;">
                    Q.<%# Eval("QuestionID") %> :
   
                <%# Eval("Question") %>
                </div>

                <br />

                <asp:Repeater ID="rptOptions" runat="server">
                    <ItemTemplate>

                        <div class="optionRow">
                            <%# Eval("OptionValue") %>- <%# Eval("OptionDescription") %>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <br />
                <asp:GridView ID="gvSummary"
                    runat="server"
                    CssClass="grid"
                    AutoGenerateColumns="false"
                    GridLines="Both"
                    ShowHeader="true"
                    Width="100%"
                    BorderWidth="2px"
                    BorderStyle="Solid"
                    BorderColor="Black"
                    CellPadding="8">

                    <Columns>

                        <asp:BoundField DataField="Choice"
                            HeaderText="Choice Number/ Remark" />

                        <asp:BoundField DataField="Responses"
                            HeaderText="Students Given Responses" />

                    </Columns>

                    <RowStyle Height="35px" />
                    <HeaderStyle Height="40px" />

                </asp:GridView>
                <br />
                <br />
                <asp:Chart ID="Chart1"
                    runat="server"
                    Width="900px"
                    Height="450px">

                    <chartareas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </chartareas>

                    <legends>
                        <asp:Legend Name="Legend1"></asp:Legend>
                    </legends>

                </asp:Chart>

                <hr />

            </ItemTemplate>

        </asp:Repeater>
    </div>
</asp:Content>
