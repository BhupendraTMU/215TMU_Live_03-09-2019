<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="Honorarium_E_Visit.aspx.cs" Inherits="Faculty_Honorarium_E_Visit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        .column {
            border: 1px solid #000;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 500px;
            height: 200px;
        }
    </style>
    <style type="text/css">
        .GridPager a, .GridPager span {
            display: block;
            height: 15px;
            width: 15px;
            font-weight: bold;
            text-align: center;
            text-decoration: none;
        }

        .GridPager a {
            background-color: #f5f5f5;
            border: 1px solid #969696;
        }

        .GridPager span {
            background-color: #A1DCF2;
            border: 1px solid #3AC0F2;
        }
    </style>

    <script type="text/javascript">

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alert("Please Enter Only Numeric Value:");
                return false;
            }

            return true;
        }
        function HidePopup1() {

            $('#confirmModal1').modal('hide');
        }
        function checkDateFrom(sender, args) {
            var today = new Date();
            if (sender._selectedDate > today) {
                alertify.error("You cannot select greater than current date !");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }

            else {
                var f = new Date($('[id$=txtDateTo]').val());
                if (new Date(sender._selectedDate).val() > f) {
                    alertify.error("You cannot select greater than To date !");
                    sender._textbox.set_Value('');
                }
            }


        }
        function checkDateTo(sender, args) {
            var today = new Date();
            if (sender._selectedDate > today) {
                alertify.error("You cannot select greater than current date !");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
            if ($('[id$=txtDateFrom]').val() == '') {
                alertify.error('First select the from date !');
                sender._textbox.set_Value('');
                return false;
            }
            else {
                var f = new Date($('[id$=txtDateFrom]').val());

                if (sender._selectedDate < f) {
                    alertify.error("You cannot select less than from date !");
                    sender._textbox.set_Value('');
                }
            }
        }

    </script>





</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Honorarium of the External Members form" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>

    <table cellpadding="0px" cellspacing="0px">
        <tr>
            <td style="width: 30px"></td>
            <td valign="top" style="width: 140px">
                <table cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td class="leftmMenu">&nbsp;<asp:LinkButton ID="lnkApplication" Width="140px" runat="server" OnClick="lnkApplication_Click">Application</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td class="leftmMenu">&nbsp;<asp:LinkButton ID="lnkApproval" Width="140px" runat="server" OnClick="lnkApproval_Click">Approval</asp:LinkButton></td>
                    </tr>

                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td class="leftmMenu">&nbsp;<asp:LinkButton ID="lnkReport" Width="140px" runat="server" OnClick="lnkReport_Click">Report</asp:LinkButton></td>
                    </tr>
                </table>

            </td>
            <td style="width: 30px"></td>
            <td style="width: 1px; background-color: #f1f1f1"></td>
            <td style="width: 30px"></td>
            <td valign="top">









                <table cellpadding="0px" cellspacing="0px">


                    <tr>
                        <td style="height: 10px">

                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>

                        </td>
                    </tr>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlApplicationList" runat="server" Visible="true">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnAddNew" OnClick="btnAddNew_Click" runat="server" Width="120px" Text="Add New" />

                                <asp:GridView ID="grdApplicationList" DataKeyNames="ApplicationNo" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="2px" CellPadding="3" Width="1000px" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl. No." ItemStyle-CssClass="column" HeaderStyle-CssClass="column">

                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle Width="2%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ApplicationNo" ItemStyle-Width="20px" HeaderText="Application No" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                        <asp:BoundField DataField="Applicationfor" HeaderText="Application Type" ItemStyle-Width="10px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                        <asp:BoundField DataField="name" HeaderText="Expert Name" ItemStyle-Width="80px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                        <asp:BoundField DataField="designation" HeaderText="Designation" ItemStyle-Width="150px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                        <asp:BoundField DataField="organization" HeaderText="Organisation" ItemStyle-Width="120px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                        <asp:BoundField DataField="visitingdate" HeaderText="Visiting Date" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                        <asp:BoundField DataField="modeofvisiting" HeaderText="Visiting Mode" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                        <asp:BoundField DataField="totalamt" HeaderText="Total Amount" ItemStyle-Width="120px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                        <asp:TemplateField HeaderText="Detail" ItemStyle-CssClass="column" HeaderStyle-CssClass="column">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDetail" Text="details" CommandArgument='<%# Eval("ApplicationNo") %>' OnClick="lnkDetail_Click" runat="server"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                    <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                    <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                    <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                </asp:GridView>


                            </td>
                        </tr>


                    </table>


                </asp:Panel>

                <asp:Panel ID="pnlApplication" Visible="false" runat="server">
                    <table>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server"
                                    Text="Application Form" Font-Size="15pt" ForeColor="#093A62"
                                    Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                <asp:HiddenField ID="hdfApplicationNo" runat="server" />
                            </td>
                            <td style="text-align: right; width: 600px">
                                <asp:LinkButton ID="btnback" runat="server" Width="140px" Text="Back to List" OnClick="btnback_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="height: 1px; background-color: #f1f1f1"></td>
                        </tr>
                    </table>

                    <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Application For</label>

                            <div class="col-sm-8">

                                <asp:DropDownList ID="drpAppType" runat="server" CssClass="form-control" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="drpAppType_SelectedIndexChanged">
                                    <%--<asp:ListItem Text="CRC" Value="CRC"></asp:ListItem>
                                    <asp:ListItem Text="BoS" Value="BoS"></asp:ListItem>
                                    <asp:ListItem Text="AC" Value="AC"></asp:ListItem>
                                    <asp:ListItem Text="EC" Value="EC"></asp:ListItem>
                                    <asp:ListItem Text="LTS" Value="LTS"></asp:ListItem>
                                    <asp:ListItem Text="IIC" Value="IIC"></asp:ListItem>--%>
                                </asp:DropDownList>

                            </div>
                        </div>
                    </div>

                    <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Name of the Expert</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txtApplicant" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtApplicant" runat="server" ValidationGroup="R1" CssClass="form-control" Width="200px"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp Designation</label>

                            <div class="col-sm-8">
                                <asp:DropDownList ID="drpDesignation" runat="server" CssClass="form-control" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="drpDesignation_SelectedIndexChanged">
                                    <asp:ListItem Text="Professor" Value="Professor"></asp:ListItem>
                                    <asp:ListItem Text="Associate Professor" Value="Associate Professor"></asp:ListItem>
                                    <asp:ListItem Text="Assistant Professor" Value="Assistant Professor"></asp:ListItem>
                                    <asp:ListItem Text="Assistant Professor" Value="Assistant Professor"></asp:ListItem>
                                    <asp:ListItem Text="Jain Supervisor" Value="Jain Supervisor"></asp:ListItem>
                                    <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp Organization</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txtOrganization" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtOrganization" runat="server" ValidationGroup="R1" CssClass="form-control" Width="200px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp  Date </label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txtDate" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" ValidationGroup="R1" Width="200px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtDate" Format="dd-MM-yyyy"></asp:CalendarExtender>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-3 p-0">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Mode of Visit </label>

                            <div class="col-sm-8">
                                <asp:DropDownList ID="drpMode" runat="server" CssClass="form-control" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="drpMode_SelectedIndexChanged">

                                    <asp:ListItem Text="Online" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Offline" Value="1"></asp:ListItem>
                                </asp:DropDownList>

                            </div>
                        </div>
                    </div>





                    <div class="col-sm-3 p-0">

                        <div class="form-group clearfix">

                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Honorarium Amount </label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txtHonoAMT" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtHonoAMT" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" AutoPostBack="true" ValidationGroup="R1" Width="200px" OnTextChanged="txtHonoAMT_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 p-0">

                        <div class="form-group clearfix">

                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Travel Allowance </label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txttravelAll" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txttravelAll" runat="server" CssClass="form-control" OnTextChanged="txttravelAll_TextChanged" AutoPostBack="true" onkeypress="return isNumberKey(event)" ValidationGroup="R1" Width="200px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 p-0">

                        <div class="form-group clearfix">

                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Total Amount </label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txttravelAMT" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txttravelAMT" runat="server" CssClass="form-control" Enabled="false" onkeypress="return isNumberKey(event)" ValidationGroup="R1" Width="200px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 p-0">

                        <div class="form-group clearfix">

                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Account Holder Name </label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txtACHolderName" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtACHolderName" runat="server" CssClass="form-control" ValidationGroup="R1" Width="200px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 p-0">

                        <div class="form-group clearfix">

                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Account Number </label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txtACCNumber" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtACCNumber" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" ValidationGroup="R1" Width="200px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 p-0">

                        <div class="form-group clearfix">

                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Bank Name </label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txtBankName" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control" ValidationGroup="R1" Width="200px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 p-0">

                        <div class="form-group clearfix">

                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Branch </label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txtBranch" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtBranch" runat="server" CssClass="form-control" ValidationGroup="R1" Width="200px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 p-0">

                        <div class="form-group clearfix">

                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp IFSC Code </label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txtIFSC" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtIFSC" runat="server" CssClass="form-control" ValidationGroup="R1" Width="200px"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-3 p-0">

                        <div class="form-group clearfix">

                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Mobile No </label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txtMobile" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator
                                ID="revMobile"
                                runat="server"
                                ControlToValidate="txtMobile"
                                ValidationGroup="R1"
                                ErrorMessage="Invalid Mobile"
                                ForeColor="Red"
                                ValidationExpression="^[6-9]\d{9}$"
                                Display="Dynamic">
                            </asp:RegularExpressionValidator>


                            <div class="col-sm-8">
                                <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" ValidationGroup="R1" Width="200px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 p-0">

                        <div class="form-group clearfix">

                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp E-Mail Address </label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txtEmail" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator
                                ID="revEmail"
                                runat="server"
                                ControlToValidate="txtEmail"
                                ValidationGroup="R1"
                                ErrorMessage="Invalid Email"
                                ForeColor="Red"
                                ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
                                Display="Dynamic">
                            </asp:RegularExpressionValidator>

                            <div class="col-sm-8">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" ValidationGroup="R1" Width="200px"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-3 p-0" runat="server" id="divSupport">

                        <div class="form-group clearfix">

                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Upload Supporting Document </label>

                            <div class="col-sm-8">
                                <asp:FileUpload ID="flSupportDoc" runat="server" Font-Bold="true" Height="32px" Font-Size="small" Width="200px"></asp:FileUpload>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-3 p-0" runat="server" id="divEvent" visible="false">

                        <div class="form-group clearfix">

                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Event Type </label>

                            <div class="col-sm-8">
                                <asp:TextBox ID="txteventType" runat="server" CssClass="form-control" Width="200px"></asp:TextBox>
                            </div>
                        </div>
                    </div>



                    <div id="divUploadDoc" runat="server">
                        <div class="col-sm-12 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font: bold; color: green; font-size: medium">&nbsp&nbsp&nbsp   Travel Document :-</label>


                            </div>
                        </div>
                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font: bold; font-size: small">&nbsp&nbsp&nbsp  Attachments Type:</label>

                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlAttachtype" runat="server" Font-Bold="true" Height="32px" Font-Size="small" Width="220px" AutoPostBack="true">
                                        <asp:ListItem Text="Travelling Bill" Value="1"></asp:ListItem>

                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="fe" runat="server">
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSubmitApplication" />
                            </Triggers>
                            <ContentTemplate>
                                <div class="col-sm-3 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font: bold; font-size: small">&nbsp&nbsp&nbsp  Attachments :</label>

                                        <div class="col-sm-8">

                                            <asp:FileUpload ID="FileUpload1" runat="server" Font-Bold="true" Height="32px" Font-Size="small" Width="250px"></asp:FileUpload>


                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-6 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font: bold; visibility: hidden; font-size: small">&nbsp&nbsp&nbsp  Registration fee details of the Conference/Seminar/Workshop </label>

                                        <div class="col-sm-8">
                                            <asp:Button ID="btnSubmitApplication" runat="server" autocomplete="off" Text="Submit" BackColor="Green" ValidationGroup="R1" ForeColor="White" Font-Bold="true" Height="32px" Font-Size="Large" class="btn btn-info" Width="120px" AutoPostBack="true" OnClick="btnSubmitApplication_Click"></asp:Button>


                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>


                </asp:Panel>
                <asp:Panel ID="pnlApplicationApproval" Visible="false" runat="server">
                    <fieldset class="boxBody">
                        <asp:Label ID="Label2" runat="server"
                            Text="Honoraium Approval" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                    </fieldset>
                    <asp:GridView ID="grdApplicationApproval" DataKeyNames="ApplicationNo" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="2px" CellPadding="3" Width="1000px" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                        <Columns>
                            <asp:TemplateField HeaderText="Sl. No." ItemStyle-CssClass="column" HeaderStyle-CssClass="column">

                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle Width="2%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ApplicationNo" ItemStyle-Width="20px" HeaderText="Application No" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                            <asp:BoundField DataField="College" HeaderText="College Code" ItemStyle-Width="10px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                            <asp:BoundField DataField="Applicationfor" HeaderText="Application Type" ItemStyle-Width="10px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                            <asp:BoundField DataField="name" HeaderText="Expert Name" ItemStyle-Width="80px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                            <asp:BoundField DataField="designation" HeaderText="Designation" ItemStyle-Width="150px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                            <asp:BoundField DataField="organization" HeaderText="Organisation" ItemStyle-Width="120px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                            <asp:BoundField DataField="visitingdate" HeaderText="Visiting Date" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                            <asp:BoundField DataField="modeofvisiting" HeaderText="Visiting Mode" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                            <asp:BoundField DataField="totalamt" HeaderText="Total Amount" ItemStyle-Width="120px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                            <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                            <%--<asp:TemplateField HeaderText="Detail" ItemStyle-CssClass="column" HeaderStyle-CssClass="column">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDetail" Text="detail" CommandArgument='<%# Eval("ApplicationNo") %>' OnClick="lnkDetail_Click" runat="server"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Approved/Rejected" ItemStyle-CssClass="column" HeaderStyle-CssClass="column">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAction" Text="Approved/Rejected" CommandArgument='<%# Eval("ApplicationNo") %>' OnClick="lnkAction_Click" runat="server"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                        <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <SortedAscendingCellStyle BackColor="#F4F4FD" />
                        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                        <SortedDescendingCellStyle BackColor="#D8D8F0" />
                        <SortedDescendingHeaderStyle BackColor="#3E3277" />
                    </asp:GridView>

                </asp:Panel>



                <asp:Panel ID="pnlRepport" runat="server" Visible="false">

                    <fieldset class="boxBody">
                        <div class="container-fluid">

                            <div class="row">

                                <!-- Report Type -->
                                <div class="col-md-3">
                                    <label>Report Type</label>
                                    <asp:DropDownList ID="ddlReportType" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="--Select Report--" Value="" />
                                        <asp:ListItem Text="Unpaid Amount Report" Value="1" />
                                        <asp:ListItem Text="Expert Detail" Value="2" />
                                        <asp:ListItem Text="Total Amount" Value="3" />
                                    </asp:DropDownList>
                                </div>

                                <!-- Event Type Checkbox -->
                                <div class="col-md-3">
                                    <label>Event Type</label>

                                    <div class="dropdown">
                                        <button class="form-control text-left dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown">
                                            Select Event Type
       
                                        </button>

                                        <div class="dropdown-menu p-2" style="max-height: 250px; overflow: auto;">
                                            <asp:CheckBoxList ID="chkEventType" runat="server"
                                                RepeatLayout="Table"
                                                CssClass="chkList">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>

                                <!-- From Date -->
                                <div class="col-md-2">
                                    <label>From Date</label>
                                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv1" runat="server"
                                        ControlToValidate="txtDateFrom" ErrorMessage="*" ForeColor="Red" />
                                </div>

                                <!-- To Date -->
                                <div class="col-md-2">
                                    <label>To Date</label>
                                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv2" runat="server"
                                        ControlToValidate="txtDateTo" ErrorMessage="*" ForeColor="Red" />
                                </div>

                                <!-- Search Button -->
                                <div class="col-md-2 d-flex align-items-end">
                                    <asp:Button ID="btnSearch" runat="server"
                                        Text="Search"
                                        CssClass="btn btn-success w-100"
                                        OnClick="btnSearch_Click" />
                                </div>

                            </div>

                            <hr />

                            <!-- Report Viewer -->
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server"
                                Width="100%" Height="500px">
                            </rsweb:ReportViewer>

                        </div>
                    </fieldset>

                </asp:Panel>




            </td>
        </tr>
    </table>

    <div id="confirmModal1" class="modal fade confirm-modal" role="dialog">

        <div class="modal-dialog modalPopup" style="width: 1000px; height: 600px">
            <div style="text-align: right; padding-bottom: -40px">
                <asp:Button ID="Button1" runat="server" Text="X" OnClientClick="HidePopup1();" Font-Size="Larger" />
            </div>
            <div class="clearfix" style="margin-bottom: 10px; margin-top: 10px; margin-left: 20px">

                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnApprove" />
                        <asp:PostBackTrigger ControlID="lnkDoc" />
                        <asp:PostBackTrigger ControlID="lnkSupport" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Application Type</label>

                                <div class="col-sm-8">
                                    <asp:Label ID="lbltype" runat="server">                                 

                                    </asp:Label>

                                </div>
                            </div>
                        </div>

                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Name of the Expert</label>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblExpert" runat="server" ValidationGroup="R1"></asp:Label>

                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp Designation</label>

                                <div class="col-sm-8">
                                    <asp:Label ID="lblDesig" runat="server" ValidationGroup="R1"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp Organization</label>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblOrganisation" runat="server" ValidationGroup="R1"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp  Date </label>

                                <div class="col-sm-8">
                                    <asp:Label ID="lblDate" runat="server" ValidationGroup="R1"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-3 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Mode of Visit </label>

                                <div class="col-sm-8">
                                    <asp:Label ID="lblMOV" runat="server" ValidationGroup="R1"></asp:Label>

                                </div>
                            </div>
                        </div>





                        <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Honorarium Amount </label>

                                <div class="col-sm-8">
                                    <asp:Label ID="lblHoAmt" runat="server" ValidationGroup="R1"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Travel Allowance </label>

                                <div class="col-sm-8">
                                    <asp:Label ID="lblTravelAllow" runat="server" ValidationGroup="R1"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Total Amount </label>

                                <div class="col-sm-8">
                                    <asp:Label ID="lblTotalAMT" runat="server" ValidationGroup="R1"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Recommended Amount </label>

                                <div class="col-sm-8">
                                    <asp:Label ID="lblRecommendedAmt" runat="server" ></asp:Label>
                                </div>
                            </div>
                        </div>


                        <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Approved Amount </label>

                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtApproveAMT" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    <asp:TextBox ID="txtVCApproveAMT" runat="server" onkeypress="return isNumberKey(event)" Visible="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Account Holder Name </label>

                                <div class="col-sm-8">
                                    <asp:Label ID="lblACHName" runat="server" ValidationGroup="R1"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Account Number </label>

                                <div class="col-sm-8">
                                    <asp:Label ID="lblACNumber" runat="server" ValidationGroup="R1"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Bank Name </label>

                                <div class="col-sm-8">
                                    <asp:Label ID="lblBank" runat="server" ValidationGroup="R1"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Branch </label>

                                <div class="col-sm-8">
                                    <asp:Label ID="lblBranch" runat="server" ValidationGroup="R1"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp IFSC Code </label>

                                <div class="col-sm-8">
                                    <asp:Label ID="lblIFSC" runat="server" ValidationGroup="R1"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Event Type </label>

                                <div class="col-sm-8">
                                    <asp:Label ID="lblEventtype" runat="server" ValidationGroup="R1"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Travelling Bill </label>

                                <div class="col-sm-8">
                                    <asp:LinkButton ID="lnkDoc" runat="server" Text="Download Travelling Bill" OnClick="lnkDoc_Click"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Supporting Document </label>

                                <div class="col-sm-8">
                                    <asp:LinkButton ID="lnkSupport" runat="server" Text="Download Supporting Document" OnClick="lnkSupport_Click"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-8 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp  </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ValidationGroup="XX" Display="Dynamic" ControlToValidate="txtRemark" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                <div class="col-sm-8">
                                    Remark: 
                                    <asp:TextBox ID="txtRemark" runat="server" ValidationGroup="XX" Width="428px" Height="60px" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>


                        <div class="col-sm-2 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font: bold; visibility: hidden; font-size: small">&nbsp&nbsp&nbsp  Registration fee details  </label>

                                <div class="col-sm-8">

                                    <asp:Button ID="btnApprove" runat="server" Text="Approved" Font-Bold="true" AutoPostBack="true" OnClick="btnApprove_Click" Height="32px" Font-Size="small" class="btn btn-info" Width="120px"></asp:Button>
                                    <asp:Button ID="btnRejectPop" runat="server" Text="Reject" Font-Bold="true" ValidationGroup="XX" AutoPostBack="true" OnClick="btnRejectPop_Click" Height="32px" Font-Size="small" class="btn btn-info" Width="120px"></asp:Button>
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>

                </asp:UpdatePanel>
            </div>
        </div>
    </div>








</asp:Content>

