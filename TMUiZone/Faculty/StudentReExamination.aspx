<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master"
    AutoEventWireup="true"
    CodeFile="StudentReExamination.aspx.cs"
    Inherits="Faculty_StudentReExamination" %>

<%@ Register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="../bootstrap/js/jquery-1.11.2.min.js"></script>
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../bootstrap/js/bootstrap.min.js"></script>

    <style>
        .modern-grid {
            width: 100%;
            border-collapse: collapse;
            font-family: 'Segoe UI', Arial, sans-serif;
            font-size: 14px;
            background: #ffffff;
            border-radius: 12px;
            overflow: hidden;
            box-shadow: 0 4px 12px rgba(0,0,0,0.12);
        }

            .modern-grid th {
                background: linear-gradient(135deg, #1e3c72, #2a5298);
                color: white;
                padding: 14px 10px;
                text-align: center;
                font-size: 14px;
                font-weight: 600;
                border: 1px solid #dcdcdc;
                white-space: nowrap;
            }

            .modern-grid td {
                padding: 10px;
                border: 1px solid #e5e5e5;
                color: #333;
                text-align: center;
                white-space: nowrap;
            }

            .modern-grid tr:nth-child(even) {
                background-color: #f8fbff;
            }

            .modern-grid tr:nth-child(odd) {
                background-color: #ffffff;
            }

            .modern-grid tr:hover {
                background-color: #e8f1ff;
                transition: 0.3s;
            }

            .modern-grid .footer-style {
                background: #dbeafe;
                color: #000;
                font-weight: bold;
            }

        .grid-container {
            width: 100%;
            overflow-x: auto;
            padding: 10px;
            background: #f4f7fb;
            border-radius: 12px;
        }

        .page-title {
            font-size: 24px;
            font-weight: 700;
            color: #1e3c72;
            margin-bottom: 15px;
            padding-left: 5px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <fieldset style="background: #fefefe; border-top: 1px solid #dde0e8; border-bottom: 1px solid #dde0e8; padding: 10px 20px; height: 100%">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

            <ContentTemplate>

                <br />

                <table width="100%">
                    <tr>
                        <td style="width: 10px"></td>

                        <td>

                            <div class="panel panel-info"
                                id="pnlAttendance"
                                runat="server"
                                style="width: 100%;">

                                <div class="grid-container">

                                    <div class="page-title">
                                        Student Reappear Exam Report

                                        <asp:Button ID="Button2"
                                            runat="server"
                                            Text="Export to Excel"
                                            Font-Size="18pt"
                                            Font-Bold="true"
                                            OnClick="btnExport_Click"
                                            ForeColor="White"
                                            BackColor="Green"
                                            Height="50px"
                                            Width="220px" />

                                    </div>

                                    <asp:GridView ID="grdStudentReport"
                                        runat="server"
                                        AutoGenerateColumns="false"
                                        CssClass="modern-grid"
                                        GridLines="None"
                                        ShowFooter="true"
                                        EmptyDataText="No Record Found"
                                        OnRowDataBound="grdStudentReport_RowDataBound">

                                        <Columns>

                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="Academic Year"
                                                HeaderText="Academic Year" />

                                            <asp:BoundField DataField="Global Dimension 1 Code"
                                                HeaderText="Campus" />

                                            <asp:BoundField DataField="Enrollment No"
                                                HeaderText="Enrollment No" />

                                            <asp:BoundField DataField="Student Name"
                                                HeaderText="Student Name" />

                                            <asp:BoundField DataField="Course"
                                                HeaderText="Course" />

                                            <asp:BoundField DataField="Subject Code"
                                                HeaderText="Subject Code" />

                                            <asp:BoundField DataField="sem_year"
                                                HeaderText="Sem/Year" />

                                            <asp:BoundField DataField="Reappear Exam Form Submitted"
                                                HeaderText="Form Status" />

                                            <asp:BoundField DataField="[Reappear Exam Form Submit Date]"
                                                HeaderText="Submit Date"
                                                DataFormatString="{0:dd-MMM-yyyy}" />

                                            <asp:BoundField DataField="Re-App Exam Form Invoice Date"
                                                HeaderText="Invoice Date"
                                                DataFormatString="{0:dd-MMM-yyyy}" />

                                         
                                            <asp:TemplateField HeaderText="Payment Status">

                                                <ItemTemplate>

                                                    <asp:Label ID="lblPaymentStatus"
                                                        runat="server"
                                                        Text='<%# Eval("Amount Pending") %>'>
                                                    </asp:Label>

                                                </ItemTemplate>

                                            </asp:TemplateField>

                                        </Columns>

                                        <FooterStyle CssClass="footer-style" />

                                    </asp:GridView>

                                </div>

                            </div>

                        </td>

                    </tr>

                </table>

            </ContentTemplate>


            <Triggers>
                <asp:PostBackTrigger ControlID="Button2" />
            </Triggers>

        </asp:UpdatePanel>

    </fieldset>

</asp:Content>
