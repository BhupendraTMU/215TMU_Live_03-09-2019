<%@ Page Title="CRAC Meeting" Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" CodeFile="CRACMeetingPHD.aspx.cs" Inherits="Student_CRACMeeting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .print-form {
            max-width: 1200px;
            margin: 25px auto;
            background: #fff;
            border: 1px solid #dcdcdc;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0,0,0,.08);
            overflow: hidden;
        }

        .form-header {
            background: #ed7600;
            color: #fff;
            text-align: center;
            font-size: 24px;
            font-weight: 600;
            padding: 15px;
            letter-spacing: .5px;
        }

        .form-body {
            padding: 25px;
        }

        .info-box {
            margin-bottom: 18px;
        }

            .info-box label {
                font-weight: 600;
                color: #555;
                margin-bottom: 5px;
                display: block;
            }

            .info-box .form-control {
                border-radius: 6px;
                height: 42px;
                background: #f8f9fa;
                border: 1px solid #ced4da;
                font-weight: 600;
            }

        .grid-title {
            margin: 20px 0 10px;
            font-size: 18px;
            font-weight: 600;
            color: #ed7600;
            border-left: 4px solid #0d6efd;
            padding-left: 10px;
        }

        .table th {
            background: #ed7600;
            color: white;
            text-align: center;
        }

        .table td {
            vertical-align: middle;
            text-align: center;
        }

        .action-panel {
            margin-top: 20px;
            text-align: right;
        }

        @media print {

            .action-panel {
                display: none;
            }

            .print-form {
                border: none;
                box-shadow: none;
            }

            @page {
                size: A4;
                margin: 10mm;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div id="divPhd" runat="server" class="print-form">

        <div class="form-header">
            CRAC Meeting
        </div>

        <div class="form-body">

            <div class="row">

                <div class="col-md-6 info-box">
                    <label>Research Scholar Name</label>
                    <asp:TextBox ID="txtPhdName" runat="server"
                        CssClass="form-control" Enabled="false" />
                </div>

                <div class="col-md-6 info-box">
                    <label>Employee Code</label>
                    <asp:TextBox ID="txtEmpCode" runat="server"
                        CssClass="form-control" Enabled="false" />
                </div>

                <div class="col-md-6 info-box">
                    <label>Father's Name</label>
                    <asp:TextBox ID="txtPhdFather" runat="server"
                        CssClass="form-control" Enabled="false" />
                </div>

                <div class="col-md-6 info-box">
                    <label>CRAC Meeting</label>

                    <asp:DropDownList ID="ddlCRACMeeting"
                        runat="server"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlCRACMeeting_SelectedIndexChanged">

                        <asp:ListItem Text="Select" Value="" />
                        <asp:ListItem Text="I-CRAC" Value="1" />
                        <asp:ListItem Text="II-CRAC" Value="2" />
                        <asp:ListItem Text="III-CRAC" Value="3" />
                        <asp:ListItem Text="IV-CRAC" Value="4" />
                        <asp:ListItem Text="V-CRAC" Value="5" />
                        <asp:ListItem Text="VI-CRAC" Value="6" />

                    </asp:DropDownList>

                </div>

                <div class="col-md-6 info-box">
                    <label>Student No.</label>
                    <asp:TextBox ID="txtPhdStudentNo" runat="server"
                        CssClass="form-control" Enabled="false" />
                </div>

                <div class="col-md-6 info-box">
                    <label>Enrollment No.</label>
                    <asp:TextBox ID="txtPhdEnroll" runat="server"
                        CssClass="form-control" Enabled="false" />
                </div>

                <div class="col-md-6 info-box">
                    <label>College / Department</label>
                    <asp:TextBox ID="txtPhdCollegeDept" runat="server"
                        CssClass="form-control" Enabled="false" />
                </div>

                <div class="col-md-6 info-box">
                    <label>Course Name</label>
                    <asp:TextBox ID="txtPhdCourseName" runat="server"
                        CssClass="form-control" Enabled="false" />
                </div>

                <div class="col-md-6 info-box">
                    <label>Gender</label>
                    <asp:TextBox ID="txtPhdGender" runat="server"
                        CssClass="form-control" Enabled="false" />
                </div>

                <div class="col-md-6 info-box">
                    <label>Academic Year</label>
                    <asp:TextBox ID="txtPhdAcademicYear1" runat="server"
                        CssClass="form-control" Enabled="false" />
                </div>

            </div>

            <div class="grid-title">
                Fee Details
            </div>

            <asp:GridView ID="gvPhdFees"
                runat="server"
                AutoGenerateColumns="false"
                CssClass="table table-bordered table-striped table-hover"
                GridLines="Both"
                HeaderStyle-BackColor="#0d6efd"
                HeaderStyle-ForeColor="White"
                HeaderStyle-Font-Bold="true"
                HeaderStyle-HorizontalAlign="Center"
                RowStyle-HorizontalAlign="Center"
                EmptyDataText="No Fee Details Found."
                OnRowDataBound="gvPhdFees_RowDataBound">

                <Columns>

                    <asp:TemplateField HeaderText="S.No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="Fee Description"
                        DataField="FeeDescription" />

                    <asp:BoundField HeaderText="Fee Amount (₹)"
                        DataField="FeeAmount"
                        DataFormatString="{0:N2}"
                        HtmlEncode="false" />

                    <asp:BoundField HeaderText="Paid Amount (₹)"
                        DataField="PaidAmount"
                        DataFormatString="{0:N2}"
                        HtmlEncode="false" />

                    <asp:BoundField HeaderText="Pending Amount (₹)"
                        DataField="PendingAmount"
                        DataFormatString="{0:N2}"
                        HtmlEncode="false" />

                    <asp:BoundField HeaderText="Status"
                        DataField="Status" />

                    <asp:BoundField HeaderText="Receipt No."
                        DataField="ReceiptNo" />

                    <asp:BoundField HeaderText="Due Date"
                        DataField="DueDate"
                        DataFormatString="{0:dd-MMM-yyyy}"
                        HtmlEncode="false" />

                    <asp:BoundField HeaderText="Payment Date"
                        DataField="PaymentDate"
                        DataFormatString="{0:dd-MMM-yyyy}"
                        HtmlEncode="false" />

                    <asp:BoundField HeaderText="Academic Year"
                        DataField="AcademicYear" />

                    <asp:BoundField HeaderText="Admitted Year"
                        DataField="Admitted Year" />

                </Columns>

            </asp:GridView>

            <div class="action-panel">
                <asp:Button ID="btnPhdSave"
                    runat="server"
                    Text="Save" Visible="false"
                    CssClass="btn btn-success px-4"
                    OnClick="btnPhdSave_Click" />
            </div>

        </div>

    </div>
</asp:Content>
