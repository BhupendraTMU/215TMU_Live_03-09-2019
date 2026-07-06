<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="DigilockerData.aspx.cs" Inherits="Faculty_DigilockerData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        body {
            background: #fff8f0;
        }

        .bhagwa-box {
            border-radius: 12px;
            overflow: hidden;
            box-shadow: 0 4px 15px rgba(0,0,0,0.15);
            border-top: 4px solid #ff6f00;
            background: #fff;
            margin-top: 15px;
        }

        .bhagwa-header {
            background: linear-gradient(135deg,#ff6f00,#ff9800);
            color: #fff;
            padding: 15px 20px;
        }

            .bhagwa-header h3 {
                margin: 0;
                font-size: 22px;
                font-weight: 600;
            }

        .bhagwa-body {
            padding: 15px;
            background: #fff;
        }

        .table {
            width: 100%;
            border-collapse: collapse;
            font-size: 13px;
            background: #fff;
        }

            .table th {
                background: #ff8f00;
                color: #fff;
                text-align: center;
                font-weight: 600;
                padding: 10px;
                border: 1px solid #ffc107;
                white-space: nowrap;
                position: sticky;
                top: 0;
                z-index: 1;
            }

            .table td {
                padding: 8px 10px;
                border: 1px solid #ffe0b2;
                vertical-align: middle;
            }

            .table tr:nth-child(even) {
                background-color: #fff8e1;
            }

            .table tr:hover {
                background-color: #ffe0b2;
                transition: 0.3s;
            }

        .table-responsive {
            overflow-x: auto;
            max-height: 650px;
            overflow-y: auto;
        }

        .grid-title-icon {
            margin-right: 8px;
            color: #fffde7;
        }

        .modal-content {
            border-radius: 12px;
        }

        .modal-header {
            border-bottom: none;
        }

        .modal-footer {
            border-top: 1px solid #eee;
        }

        .table th {
            background: #f8f9fa;
            font-weight: 600;
        }

        .table td,
        .table th {
            vertical-align: middle;
        }

        .alert-warning {
            border-radius: 8px;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="bhagwa-box">

        <div class="bhagwa-header">
            <h3>
                <i class="fa fa-graduation-cap grid-title-icon"></i>
                Student ABC Error Data
            </h3>
        </div>

        <div class="bhagwa-body">
            <div class="row" style="margin-bottom: 15px;">

                <div class="col-md-3">
                    <asp:TextBox ID="txtCollegeCode"
                        runat="server"
                        CssClass="form-control"
                        placeholder="College Code"></asp:TextBox>
                </div>

                <div class="col-md-3">
                    <asp:TextBox ID="txtEnrollmentNo"
                        runat="server"
                        CssClass="form-control"
                        placeholder="Enrollment No"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlIsCorrected"
                        runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="All" Value=""></asp:ListItem>
                        <asp:ListItem Text="Corrected" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Pending" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnSearch"
                        runat="server"
                        Text="Search"
                        CssClass="btn btn-warning"
                        OnClick="btnSearch_Click" />
                </div>

                <div class="col-md-2">
                    <asp:Button ID="btnExport"
                        runat="server"
                        Text="Export Excel"
                        CssClass="btn btn-success"
                        OnClick="btnExport_Click" />
                </div>

            </div>
            <div class="table-responsive">

                <asp:GridView ID="gvStudentData"
                    runat="server"
                    AutoGenerateColumns="False"
                    CssClass="table"
                    DataKeyNames="ID"
                    GridLines="None">

                    <Columns>
                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <%# Container.DataItemIndex +1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- <asp:BoundField DataField="ID" HeaderText="ID" />--%>
                        <asp:BoundField DataField="INVALID_COLUMNS" HeaderText="Invalid Columns" />
                        <asp:BoundField DataField="ORG_NAME" HeaderText="Organization" />
                        <asp:BoundField DataField="ACADEMIC_COURSE_ID" HeaderText="Course ID" />
                        <asp:BoundField DataField="COURSE_NAME" HeaderText="Course Name" />
                        <asp:BoundField DataField="STREAM" HeaderText="Stream" />
                        <asp:BoundField DataField="SESSION" HeaderText="Session" />
                        <asp:BoundField DataField="REGN_NO" HeaderText="Registration No" />
                        <asp:BoundField DataField="RROLL" HeaderText="Roll No" />
                        <asp:BoundField DataField="CNAME" HeaderText="Student Name" />
                        <asp:BoundField DataField="collegeCode" HeaderText="College Code" />
                        <asp:BoundField DataField="DOB" HeaderText="DOB" />
                        <asp:BoundField DataField="FNAME" HeaderText="Father Name" />
                        <asp:BoundField DataField="MNAME" HeaderText="Mother Name" />
                        <asp:BoundField DataField="UploadDate" HeaderText="Upload Date" />
                        <asp:BoundField DataField="GENDER" HeaderText="Gender" />
                        <asp:BoundField DataField="UploadBy" HeaderText="Upload By" />
                        <asp:BoundField DataField="IsCorrected" HeaderText="Corrected" />

                    </Columns>

                </asp:GridView>

                <asp:GridView ID="gvStudentDataUpdate"
                    runat="server"
                    AutoGenerateColumns="False"
                    CssClass="table table-bordered table-hover"
                    DataKeyNames="ID"
                    Visible="false"
                    OnRowCommand="gvStudentDataUpdate_RowCommand">

                    <Columns>

                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="INVALID_COLUMNS" HeaderText="Invalid Columns" />
                        <asp:TemplateField HeaderText="Incorrect Entry">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlIncorrectEntry"
                                    runat="server"
                                    CssClass="form-control"
                                    Width="220px">

                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Student Name" Value="CNAME"></asp:ListItem>
                                    <asp:ListItem Text="Father Name" Value="FNAME"></asp:ListItem>
                                    <asp:ListItem Text="Mother Name" Value="MNAME"></asp:ListItem>
                                    <asp:ListItem Text="Adhar Number" Value="AdharNumber"></asp:ListItem>
                                    <asp:ListItem Text="Student Contact Number" Value="CONTACTNO"></asp:ListItem>
                                    <asp:ListItem Text="Student Email ID" Value="EMAILID"></asp:ListItem>
                                    <asp:ListItem Text="ABC ID" Value="ABCID"></asp:ListItem>
                                    <asp:ListItem Text="Address" Value="Address"></asp:ListItem>

                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="To Be Corrected Entry">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCorrectedValue" TextMode="MultiLine"
                                    runat="server"
                                    CssClass="form-control"
                                    Width="180px">
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:Button ID="btnUpdate"
                                    runat="server"
                                    Text="Update"
                                    CssClass="btn btn-success btn-sm"
                                    CommandName="UpdateRecord"
                                    CommandArgument='<%# Eval("REGN_NO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ORG_NAME" HeaderText="Organization" />
                        <asp:BoundField DataField="ACADEMIC_COURSE_ID" HeaderText="Course ID" />
                        <asp:BoundField DataField="COURSE_NAME" HeaderText="Course Name" />
                        <asp:BoundField DataField="STREAM" HeaderText="Stream" />
                        <asp:BoundField DataField="SESSION" HeaderText="Session" />
                        <asp:BoundField DataField="REGN_NO" HeaderText="Registration No" />
                        <asp:BoundField DataField="RROLL" HeaderText="Roll No" />
                        <asp:BoundField DataField="CNAME" HeaderText="Student Name" />
                        <asp:BoundField DataField="collegeCode" HeaderText="College Code" />
                        <asp:BoundField DataField="DOB" HeaderText="DOB" />
                        <asp:BoundField DataField="FNAME" HeaderText="Father Name" />
                        <asp:BoundField DataField="MNAME" HeaderText="Mother Name" />
                        <asp:BoundField DataField="UploadDate" HeaderText="Upload Date" />
                        <asp:BoundField DataField="GENDER" HeaderText="Gender" />
                        <asp:BoundField DataField="UploadBy" HeaderText="Upload By" />
                        <asp:BoundField DataField="IsCorrected" HeaderText="Corrected" />




                    </Columns>

                </asp:GridView>

            </div>

        </div>

    </div>

    <div id="divConfirmUpdate" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-dialog-centered modal-md" role="document">

            <div class="modal-content border-0 shadow-lg">

                <!-- Header -->
                <div class="modal-header bg-primary text-white">
                    <h5 class="modal-title">
                        <i class="fa fa-check-circle"></i>
                        Confirm Student Data Update
                    </h5>

                    <button type="button"
                        class="close text-white"
                        data-dismiss="modal">
                        &times;
                    </button>
                </div>

                <!-- Body -->
                <div class="modal-body">

                    <asp:HiddenField ID="hfID" runat="server" />
                    <asp:HiddenField ID="hfColumnName" runat="server" />
                    <asp:HiddenField ID="hfNewValue" runat="server" />

                    <div class="alert alert-warning mb-3">
                        Please verify the changes before confirming.
                    </div>

                    <table class="table table-striped table-bordered mb-0">
                        <tbody>

                            <tr>
                                <th style="width: 35%">Enrollment No</th>
                                <td>
                                    <asp:Label ID="lblEnroll"
                                        runat="server"
                                        CssClass="font-weight-bold text-primary">
                                    </asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <th>Field Name</th>
                                <td>
                                    <asp:Label ID="lblPopupColumn"
                                        runat="server">
                                    </asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <th>Current Value</th>
                                <td>
                                    <asp:Label ID="lblOldValue"
                                        runat="server"
                                        CssClass="text-danger">
                                    </asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <th>Updated Value</th>
                                <td>
                                    <asp:Label ID="lblNewValue"
                                        runat="server"
                                        CssClass="text-success font-weight-bold">
                                    </asp:Label>
                                </td>
                            </tr>

                        </tbody>
                    </table>

                </div>

                <!-- Footer -->
                <div class="modal-footer justify-content-center">

                    <asp:Button ID="btnConfirmUpdate"
                        runat="server"
                        Text="✓ Confirm Update"
                        CssClass="btn btn-success px-4"
                        OnClick="btnConfirmUpdate_Click" />

                    <button type="button"
                        class="btn btn-secondary px-4"
                        data-dismiss="modal">
                        Cancel
                    </button>

                </div>

            </div>

        </div>
    </div>

</asp:Content>

