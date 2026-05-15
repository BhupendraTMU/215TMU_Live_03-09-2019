<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="HostelAttendance.aspx.cs" Inherits="Faculty_HostelAttendance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="../bootstrap/js/jquery-1.11.2.min.js"></script>
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../bootstrap/js/bootstrap.min.js"></script>
    <style>
        .red-border {
            border: 1px solid red;
        }

        .JainStudentList {
            font-family: Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            .JainStudentList td, .JainStudentList th {
                border: 1px solid #ddd;
                padding: 8px;
            }

            .JainStudentList tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            .JainStudentList tr:hover {
                background-color: #ddd;
            }

            .JainStudentList th {
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
    </style>
    <script type="text/javascript">
        function preventBackspace(e) {
            var evt = e || window.event;
            if (evt) {
                var keyCode = evt.charCode || evt.keyCode;
                if (keyCode === 8) {
                    if (evt.preventDefault) {
                        evt.preventDefault();
                    } else {
                        evt.returnValue = false;
                    }
                }
            }
        }
        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select greater than current date!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }

        function validateHhMm(inputField) {
            var isValid = /^(([01][0-9])|(2[0-3])):([0-5][0-9])$/.test(inputField.value);

            if (isValid) {
                inputField.style.backgroundColor = '#bfa';
            } else {
                inputField.style.backgroundColor = '#fba';

                alert("Accept only Time format .. (HH:mm)!  ");
                inputField.value = "";
            }

            return isValid;


        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
        <asp:Label ID="Label3" runat="server"
            Text="" Font-Size="18pt" Font-Bold="true" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <fieldset class="boxBodyHeader">
    </fieldset>
    <fieldset id="JainStudent" runat="server" style="background: #fefefe; border-top: 1px solid #dde0e8; border-bottom: 1px solid #dde0e8; padding: 10px 20px; height: 100%">
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
        <asp:UpdatePanel ID="Orderlist" runat="server">
            <ContentTemplate>
                <div class="row align-items-end">

                    <!-- Hostel -->
                    <div class="row align-items-end">

                        <!-- Hostel -->
                        <div class="col-md-3">
                            <label class="font-weight-bold">Hostel</label>

                            <asp:DropDownList ID="ddlHostel" runat="server" CssClass="form-control">
                                <asp:ListItem Text="-- Select Hostel --" Value=""></asp:ListItem>
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator
                                ID="rfvHostel"
                                runat="server"
                                ControlToValidate="ddlHostel"
                                InitialValue=""
                                ErrorMessage="**"
                                CssClass="text-danger"
                                Display="Dynamic"
                                ValidationGroup="odapps" />
                        </div>

                        <!-- Date -->
                        <div class="col-md-3">
                            <label class="font-weight-bold">Date</label>

                            <asp:TextBox ID="txtDate"
                                runat="server"
                                CssClass="form-control"
                                onkeydown="return false;"
                                autocomplete="off">
                            </asp:TextBox>

                            <asp:CalendarExtender
                                ID="clndAppliedate"
                                runat="server"
                                TargetControlID="txtDate"
                                Format="dd MMM yyyy" />

                            <asp:RequiredFieldValidator
                                ID="rfvDate"
                                runat="server"
                                ControlToValidate="txtDate"
                                ErrorMessage="**"
                                CssClass="text-danger"
                                Display="Dynamic"
                                ValidationGroup="odapps" />
                        </div>

                        <!-- From Time -->
                        <div class="col-md-2">
                            <label class="font-weight-bold">From</label>

                            <asp:TextBox ID="txtFromTime"
                                runat="server"
                                CssClass="form-control"
                                onchange="validateHhMm(this);">
                            </asp:TextBox>

                            <asp:RequiredFieldValidator
                                ID="rfvFromTime"
                                runat="server"
                                ControlToValidate="txtFromTime"
                                ErrorMessage="**"
                                CssClass="text-danger"
                                Display="Dynamic"
                                ValidationGroup="odapps" />
                        </div>

                        <!-- To Time -->
                        <div class="col-md-2">
                            <label class="font-weight-bold">To</label>

                            <asp:TextBox ID="txtToTime"
                                runat="server"
                                CssClass="form-control"
                                onchange="validateHhMm(this);">
                            </asp:TextBox>

                            <asp:RequiredFieldValidator
                                ID="rfvToTime"
                                runat="server"
                                ControlToValidate="txtToTime"
                                ErrorMessage="**"
                                CssClass="text-danger"
                                Display="Dynamic"
                                ValidationGroup="odapps" />
                        </div>

                        <!-- Buttons -->
                        <div class="col-md-2">
                            <label class="d-block">&nbsp;</label>

                            <div class="d-flex gap-2">
                                <asp:Button ID="btnSearch"
                                    runat="server"
                                    CssClass="btn btn-primary flex-fill"
                                    Text="Search"
                                    ValidationGroup="odapps"
                                    OnClick="btnSearch_Click" />

                                <asp:Button ID="btnExport"
                                    runat="server"
                                    CssClass="btn btn-success flex-fill"
                                    Text="Export"
                                    OnClick="btnExport_Click" />
                            </div>
                        </div>

                    </div>



                    <br />

                    <table width="100%">
                        <tr>
                            <td>
                                <div class="row g-3 text-center">

                                    <div class="col-md-3">
                                        <div class="card shadow-sm border-primary">
                                            <div class="card-body p-2">
                                                <small class="fw-bold">Total Hosteller</small>
                                                <h4 class="mb-0 text-primary">
                                                    <asp:Label ID="lblTotalHosteller" runat="server" Text="1300"></asp:Label>
                                                </h4>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="card shadow-sm border-success">
                                            <div class="card-body p-2">
                                                <small class="fw-bold">Total Present</small>
                                                <h4 class="mb-0 text-success">
                                                    <asp:Label ID="lblPresent" runat="server" Text="1100"></asp:Label>
                                                </h4>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="card shadow-sm border-danger">
                                            <div class="card-body p-2">
                                                <small class="fw-bold">Total Absent</small>
                                                <h4 class="mb-0 text-danger">
                                                    <asp:Label ID="lblTotalAbsent" runat="server" Text="100"></asp:Label>
                                                </h4>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="card shadow-sm border-warning">
                                            <div class="card-body p-2">
                                                <small class="fw-bold">Hostel Leave</small>
                                                <h4 class="mb-0 text-warning">
                                                    <asp:Label ID="lblHL" runat="server" Text="50"></asp:Label>
                                                </h4>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </td>
                        </tr>
                        <tr>

                            <td>
                                <div class="panel panel-info" id="pnlAttendance" runat="server" style="width: 100%;">

                                    <div class="panel-body" id="grdInboxBody">

                                        <div class="box box-default" style="background-color: rgba(255, 255, 255, 0.80); overflow: auto; height: 720px;">
                                            <asp:GridView ID="JainStudentList" runat="server" AutoGenerateColumns="false" CssClass="JainStudentList" BackColor="White" BorderColor="#E7E7FF"
                                                EmptyDataText="There are no data records to display." BorderStyle="Solid" BorderWidth="2px" CellPadding="3" Width="1130px"
                                                GridLines="Horizontal" ShowFooter="true">
                                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex +1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" ItemStyle-Width="1%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" runat="server"
                                                                Text='<%# Eval("Status") %>'
                                                                CssClass='<%# Eval("Punch Time").ToString() == "--" 
? "btn btn-danger" 
: "btn btn-success" %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Manual Present" ItemStyle-Width="1%">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnPresent"
                                                                runat="server"
                                                                Text="Present"
                                                                BackColor="#ff6600"
                                                                CommandName="ManualPresent"
                                                                Visible='<%# Eval("Status").ToString() == "Absent" %>'
                                                                CommandArgument='<%# Eval("No_") %>'
                                                                OnClientClick="return confirm('Are you sure you want to mark this student PRESENT?');"
                                                                OnCommand="btnPresent_Command" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>




                                                    <asp:TemplateField HeaderText="Finger No" ItemStyle-Width="2%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFingerNo" runat="server" Text='<%# Bind("[Finger No_]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ST No." ItemStyle-Width="3%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNo_" runat="server" Text='<%# Bind("[No_]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="3%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStudentName" runat="server" Text='<%# Bind("[Student Name]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Phone No" ItemStyle-Width="3%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPhoneNo" runat="server" Text='<%# Bind("[Phone Number]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Course Code" ItemStyle-Width="2%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Bind("[Course Code]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Hostel Code" ItemStyle-Width="2%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHostelCode" runat="server" Text='<%# Bind("[Hostel Code]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Room No_" ItemStyle-Width="3%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblRoomNo_" runat="server" Text='<%# Bind("[Room No_]") %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Punch Time" ItemStyle-Width="3%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblPunchTime" runat="server" Text='<%# Bind("[Punch Time]") %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Room Type" ItemStyle-Width="3%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblRoomType" runat="server" Text='<%# Bind("[Room Type]") %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <div class="box-footer">
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>
        </asp:UpdatePanel>
    </fieldset>




</asp:Content>

