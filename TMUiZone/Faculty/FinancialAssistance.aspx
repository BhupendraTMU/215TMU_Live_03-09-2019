<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="FinancialAssistance.aspx.cs" Inherits="Faculty_FinancialAssistance" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script runat="server">

    protected void btnPrincipalReject_Click1(object sender, EventArgs e)
    {

    }
</script>

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
        function UncheckOthers(objchkbox) {

            var objchkList = objchkbox.parentNode.parentNode.parentNode;

            var chkboxControls = objchkList.getElementsByTagName("input");

            for (var i = 0; i < chkboxControls.length; i++) {

                if (chkboxControls[i] != objchkbox && objchkbox.checked) {

                    chkboxControls[i].checked = false;
                }
            }
        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alert("Please Enter Only Numeric Value:");
                return false;
            }

            return true;
        }
        function HidePopup() {

            $('#confirmModal5').modal('hide');
        }
        function HidePopup() {

            $('#confirmModal6').modal('hide');
        }
        function priceCheck(element, event) {
            result = (event.charCode >= 48 && event.charCode <= 57) || event.charCode === 46;
            if (result) {
                let t = element.value;
                if (t === '' && event.charCode === 46) {
                    return false;
                }
                let dotIndex = t.indexOf(".");
                let valueLength = t.length;
                if (dotIndex > 0) {
                    if (dotIndex + 2 < valueLength) {
                        return false;
                    } else {
                        return true;
                    }
                } else if (dotIndex === 0) {
                    return false;
                } else {
                    return true;
                }
            } else {
                return false;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="FINANCIAL ASSISTANCE" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <table cellpadding="0px" cellspacing="0px">
        <tr>
            <td style="width: 30px"></td>
            <td valign="top" style="width: 100px">
                <table cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td class="leftmMenu">&nbsp;<asp:LinkButton ID="lnkFAApplication" runat="server" OnClick="lnkFAApplication_Click">Application</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td class="leftmMenu">&nbsp;<asp:LinkButton ID="lnkFAReimbursement" runat="server" OnClick="lnkFAReimbursement_Click">Reimbursement</asp:LinkButton></td>
                    </tr>

                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td class="leftmMenu">
                            <asp:LinkButton ID="lnkApproval" runat="server" OnClick="lnkApproval_Click">Approval</asp:LinkButton>
                            <asp:Label ID="lblCountFAAppoval" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td class="leftmMenu">
                            <asp:LinkButton ID="lnkReApproval" runat="server" OnClick="lnkApprovalReburs_Click">RE-Approval</asp:LinkButton>
                            <asp:Label ID="lblCountRebApproval" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td class="leftmMenu">
                            <asp:LinkButton ID="lnkReport" runat="server" OnClick="lnkReport_Click">Report</asp:LinkButton>

                        </td>
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




                    <tr>
                        <td>
                            <asp:Panel ID="pnlApplicationList" runat="server" Visible="true">
                                <table>
                                    <tr>
                                        <td>

                                            <asp:GridView ID="grdApplicationList" DataKeyNames="Application_No_" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="2px" CellPadding="3" Width="1000px" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-CssClass="column" HeaderStyle-CssClass="column">

                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Application_No_" ItemStyle-Width="70px" HeaderText="Application No" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="applicant_name" HeaderText="Name" ItemStyle-Width="180px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="academic year" HeaderText="Academic Year" ItemStyle-Width="80px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="designation" HeaderText="Designation" ItemStyle-Width="120px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="dept" HeaderText="Department" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="POT" HeaderText="Purpose of travelling" ItemStyle-Width="180px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="PrincipalStatus" HeaderText="Principal Status" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="SecondApprovalStatus" HeaderText="Coordinator (R&D)" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="ThirdApprovalStatus" HeaderText="Associate Dean (R&D)" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="VCApprovalStatus" HeaderText="VC Approval" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:TemplateField HeaderText="Reimbursement" ItemStyle-CssClass="column" HeaderStyle-CssClass="column">
                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="lnkReimburs" ForeColor="Blue" CommandArgument='<%# Eval("Application_No_") %>' Visible='<%# Eval("VCApprovalStatus").ToString().ToUpper() == "APPROVE --" && Eval("Rebursh").ToString().ToUpper() != "1"  ? true:false %>' Text="Reimbursement" runat="server" OnClick="lnkReimburs_Click"></asp:LinkButton>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Select" ItemStyle-CssClass="column" HeaderStyle-CssClass="column">
                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="lnkSelect" ForeColor="Blue" CommandArgument='<%# Eval("Application_No_") %>' Text="Select" runat="server" OnClick="lnkSelect_Click"></asp:LinkButton>

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


                            <asp:UpdatePanel ID="pnlFAApplication" runat="server" Visible="false">
                                <ContentTemplate>


                                    <table>
                                        <tr>
                                            <td style="height: 10px"></td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <asp:Label ID="lblHeader" runat="server"
                                                    Text="Application" Font-Size="15pt" ForeColor="#093A62"
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
                                            <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp Applicant Name</label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtApplicant" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtApplicant" runat="server" CssClass="form-control" Width="200px" Enabled="false" AutoPostBack="true"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 p-0">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp Designation</label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtDesignation" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" Width="200px" Enabled="false" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 p-0">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp Name of College</label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtCollege" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtCollege" runat="server" CssClass="form-control" Width="200px" Enabled="false" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 p-0">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp  Department </label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtDep" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtDep" runat="server" CssClass="form-control" Width="200px" Enabled="false" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 p-0">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp  Purpose of Travelling  </label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtPOT" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtPOT" runat="server" CssClass="form-control" Width="200px" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 p-0">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp  Date of Travelling   </label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtDOT" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtDOT" runat="server" CssClass="form-control" Width="200px" onkeydown="return false;"
                                                    oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" AutoPostBack="true">
                                                </asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtDOT" Format="dd-MM-yyyy"></asp:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5 p-0">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp  Name of the Conference/Seminar/Workshop   </label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtNOC" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtNOC" runat="server" CssClass="form-control" Width="452px" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 p-0">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp  Nature of Conference/Seminar/Workshop   </label>
                                            <div class="col-sm-8">

                                                <asp:RadioButton ID="rdInternal" Text="Internal" Width="90px" Font-Bold="true" Checked="true" runat="server" GroupName="examtype"></asp:RadioButton>
                                                <asp:RadioButton ID="rdExternal" Text="External" Width="90px" Font-Bold="true" runat="server" GroupName="examtype"></asp:RadioButton>


                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-6 p-0">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp  Venue of the Conference/Seminar/Workshop   </label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtVOC" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtVOC" runat="server" CssClass="form-control" Width="452px" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-6 p-0">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp  Broad Area of the Conference/Seminar/Workshop   </label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtBroadarea" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtBroadarea" runat="server" CssClass="form-control" Width="452px" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-6 p-0">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp  The role of the applicant in the Conference/Seminar/Workshop   </label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtApplicantRole" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtApplicantRole" runat="server" autocomplete="off" CssClass="form-control" Width="452px" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3 p-0">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp  From date  </label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtArrival" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtArrival" runat="server" CssClass="form-control" Width="200px" onkeydown="return false;"
                                                    oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" AutoPostBack="true">
                                                </asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtArrival" Format="dd-MM-yyyy"></asp:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3 p-0">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp  To date   </label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtDeparture" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtDeparture" runat="server" autocomplete="off" CssClass="form-control" Width="200px" onkeydown="return false;"
                                                    oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" AutoPostBack="true">
                                                </asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDeparture" Format="dd-MM-yyyy"></asp:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 p-0">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp  Estimate total travel fare cost in details </label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtFarecost" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtFarecost" runat="server" autocomplete="off" CssClass="form-control" onkeypress="return isNumberKey(event)" Width="452px" AutoPostBack="true"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 p-0">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp  Registration fee details of the Conference/Seminar/Workshop </label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtReg" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtReg" runat="server" autocomplete="off" CssClass="form-control" Width="452px" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-6 p-0">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font: bold; color: green; font-size: medium; visibility: hidden">&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp &nbsp&nbsp&nbsp &nbsp&nbsp&nbsp &nbsp&nbsp&nbsp   </label>
                                            <div class="col-sm-12">
                                                <asp:Button ID="btnSave1" runat="server" autocomplete="off" BackColor="White" ForeColor="Black" ValidationGroup="g1" Text="Save" Font-Bold="true" Height="32px" Font-Size="Large" class="btn btn-info" Width="120px" AutoPostBack="true" OnClick="btnSave1_Click"></asp:Button>
                                                <asp:Button ID="btnSubmit1" runat="server" autocomplete="off" Text="Submit" BackColor="Green" Visible="false" ValidationGroup="g1" ForeColor="White" Font-Bold="true" Height="32px" Font-Size="Large" class="btn btn-info" Width="120px" AutoPostBack="true" OnClick="btnSubmit1_Click"></asp:Button>
                                                <asp:Button ID="btnPrincipalApprove" Width="150px" Text="Approve" OnClick="btnPrincipalApprove_Click" Visible="false" runat="server"></asp:Button>
                                                <asp:Button ID="btnPrincipalReject" Width="150px" Text="Reject" OnClick="btnPrincipalReject_Click" Visible="false" runat="server"></asp:Button>
                                                <asp:HiddenField ID="hdfLeaveType" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div id="divUploadDoc" runat="server" visible="false">
                                        <div class="col-sm-12 p-0">
                                            <div class="form-group clearfix">
                                                <label for="inputEmail3" class="col-form-label" style="font: bold; color: green; font-size: medium">&nbsp&nbsp&nbsp   DOCUMENT SUBMITTED FOR CONSIDERATION :-</label>


                                            </div>
                                        </div>
                                        <div class="col-sm-3 p-0">
                                            <div class="form-group clearfix">
                                                <label for="inputEmail3" class="col-form-label" style="font: bold; font-size: small">&nbsp&nbsp&nbsp  Attachments Type:</label>

                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlAttachtype" runat="server" Font-Bold="true" Height="32px" Font-Size="small" Width="220px" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:UpdatePanel ID="fe" runat="server">
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnSubmit" />
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
                                                            <asp:Button ID="btnSubmit" runat="server" autocomplete="off" Text="Upload" Font-Bold="true" Height="32px" Font-Size="small" OnClick="btnSubmit_Click" class="btn btn-info" Width="120px"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>

                                    <div runat="server" id="divAttachmentGrid" visible="false">
                                        <asp:GridView ID="grdDocument" runat="server" AutoGenerateColumns="false" BackColor="White" BorderColor="#E7E7FF" HeaderStyle-BackColor="#ff9900" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl. No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Document name" HeaderText="Document name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="Academic Year" HeaderText="Academic Year" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="Content Type" HeaderText="Content Type" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="File Name" HeaderText="File Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <asp:LinkButton ID="lnkDownload" Text="Download" CommandArgument='<%# Eval("ID") %>' runat="server" OnClick="DownloadInboxFile"></asp:LinkButton>
                                                            </ContentTemplate>

                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="lnkDownload" />
                                                                <%--  <asp:PostBackTrigger ControlID="drpAcademic" />--%>
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">

                                                            <ContentTemplate>
                                                                <asp:LinkButton ID="lnkdelete" Text="Delete" CommandArgument='<%# Eval("ID") %>' runat="server" OnClick="DeleteFile"></asp:LinkButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="lnkdelete" />

                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>







                                        </asp:GridView>
                                    </div>

                                    <br />
                                    <br />
                                    <br />





                                </ContentTemplate>

                            </asp:UpdatePanel>




                            <asp:Panel ID="pnlReimbursement" runat="server" Visible="false">
                                <table>
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="Label4" runat="server"
                                                Text="Reimbursement" Font-Size="15pt" ForeColor="#093A62"
                                                Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 1px; background-color: #f1f1f1">

                                            <div runat="server" id="div1" visible="false">
                                                <asp:GridView ID="grdReb" DataKeyNames="Application_No_" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="2px" CellPadding="3" Width="1000px" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl. No." ItemStyle-CssClass="column" HeaderStyle-CssClass="column">

                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Application_No_" ItemStyle-Width="70px" HeaderText="Application No" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                        <asp:BoundField DataField="applicant_name" HeaderText="Name" ItemStyle-Width="180px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                        <asp:BoundField DataField="academic year" HeaderText="Academic Year" ItemStyle-Width="80px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                        <asp:BoundField DataField="designation" HeaderText="Designation" ItemStyle-Width="180px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                        <asp:BoundField DataField="dept" HeaderText="Department" ItemStyle-Width="180px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />

                                                        <asp:BoundField DataField="PrincipalStatus" HeaderText="Principal Status" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                        <asp:BoundField DataField="SecondApprovalStatus" HeaderText="Coordinator (R&D)" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                        <asp:BoundField DataField="ThirdApprovalStatus" HeaderText="Associate Dean (R&D)" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                        <asp:BoundField DataField="VCApprovalStatus" HeaderText="VC Approval" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />



                                                        <asp:TemplateField HeaderText="Select" ItemStyle-CssClass="column" HeaderStyle-CssClass="column">
                                                            <ItemTemplate>

                                                                <asp:LinkButton ID="lnkSelectRe" ForeColor="Blue" CommandArgument='<%# Eval("Application_No_") %>' Text="Select" runat="server" OnClick="lnkSelectRe_Click"></asp:LinkButton>

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


                                            </div>


                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>



                            <asp:UpdatePanel ID="pnlReApplication" runat="server" Visible="false">
                                <ContentTemplate>


                                    <table>
                                        <tr>
                                            <td style="height: 10px"></td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <asp:Label ID="Label6" runat="server"
                                                    Text="Reimbursement Application" Font-Size="15pt" ForeColor="#093A62"
                                                    Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                            </td>
                                            <td style="text-align: right; width: 600px">
                                                <asp:LinkButton ID="btnbackRe" runat="server" Width="140px" Text="Back to List" OnClick="btnbackRe_Click" />
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
                                            <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp Applicant Name</label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ValidationGroup="g2" Display="Dynamic" ControlToValidate="txtAppRe" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtAppRe" runat="server" CssClass="form-control" Width="200px" Enabled="false" AutoPostBack="true"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 p-0">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp Designation</label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ValidationGroup="g2" Display="Dynamic" ControlToValidate="txtDesigRe" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtDesigRe" runat="server" CssClass="form-control" Width="200px" Enabled="false" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 p-0">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp Name of College</label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ValidationGroup="g2" Display="Dynamic" ControlToValidate="txtNOCRe" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtNOCRe" runat="server" CssClass="form-control" Width="200px" Enabled="false" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 p-0">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp  Department </label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ValidationGroup="g2" Display="Dynamic" ControlToValidate="txtDeptRe" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtDeptRe" runat="server" CssClass="form-control" Width="200px" Enabled="false" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 p-0">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp  Name of the Conference/Seminar/Workshop  </label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ValidationGroup="g2" Display="Dynamic" ControlToValidate="txtNOCSW" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtNOCSW" runat="server" Enabled="false" CssClass="form-control" Width="450px" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-6 p-0">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp  Venue of the Conference/Seminar/Workshop   </label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ValidationGroup="g2" Display="Dynamic" ControlToValidate="txtVOCSW" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtVOCSW" runat="server" Enabled="false" CssClass="form-control" Width="450px" onkeydown="return false;"
                                                    oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" AutoPostBack="true">
                                                </asp:TextBox>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 p-0">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp  Registration fee details of the Conference/Seminar/Workshop </label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ValidationGroup="g2" Display="Dynamic" ControlToValidate="txtRegistrationRe" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtRegistrationRe" runat="server" autocomplete="off" Enabled="false" CssClass="form-control" Width="450px" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 p-0">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 130px">&nbsp&nbsp&nbsp Approval Amount   </label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ValidationGroup="g2" Display="Dynamic" ControlToValidate="txtApprovalAmount" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtApprovalAmount" runat="server" step=".01" min="0" onkeypress="return priceCheck(this, event);" Enabled="false" Text="0" CssClass="form-control" Width="100px"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 p-0">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp Approval Amount Remark</label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ValidationGroup="g2" Display="Dynamic" ControlToValidate="txtApprovalAmountRemark" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <div class="col-sm-12">
                                                <asp:TextBox ID="txtApprovalAmountRemark" runat="server" Text="ok" CssClass="form-control" Enabled="false" Width="285px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-16 p-0">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp &nbsp Reimbursement Claimed Amount</label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ValidationGroup="g2" Display="Dynamic" ControlToValidate="txtReimbursementAMT" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <div class="col-sm-12">
                                                <asp:TextBox ID="txtReimbursementAMT" runat="server" onkeypress="return priceCheck(this, event);" CssClass="form-control" Enabled="false" Width="250px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>


                                    <div id="divReDoc" runat="server" visible="false">
                                        <div class="col-sm-12 p-0">
                                            <div class="form-group clearfix">
                                                <label for="inputEmail3" class="col-form-label" style="font: bold; color: green; font-size: medium">&nbsp&nbsp&nbsp   DOCUMENT SUBMITTED FOR REIMBURSEMENT :-</label>


                                            </div>
                                        </div>
                                        <div class="col-sm-3 p-0">
                                            <div class="form-group clearfix">
                                                <label for="inputEmail3" class="col-form-label" style="font: bold; font-size: small">&nbsp&nbsp&nbsp  Attachments Type:</label>

                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="drpReimbursement" runat="server" Font-Bold="true" Height="32px" Font-Size="small" Width="220px" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnSubmitReAttach" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <div class="col-sm-3 p-0">
                                                    <div class="form-group clearfix">
                                                        <label for="inputEmail3" class="col-form-label" style="font: bold; font-size: small">&nbsp&nbsp&nbsp  Attachments :</label>

                                                        <div class="col-sm-8">

                                                            <asp:FileUpload ID="flAttachmentRe" runat="server" Font-Bold="true" Height="32px" Font-Size="small" Width="250px"></asp:FileUpload>

                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-6 p-0">
                                                    <div class="form-group clearfix">
                                                        <label for="inputEmail3" class="col-form-label" style="font: bold; visibility: hidden; font-size: small">&nbsp&nbsp&nbsp  Registration fee details of the Conference/Seminar/Workshop </label>

                                                        <div class="col-sm-8">
                                                            <asp:Button ID="btnSubmitReAttach" runat="server" autocomplete="off" Text="Upload" Font-Bold="true" Height="32px" Font-Size="small" OnClick="btnSubmitReAttach_Click" class="btn btn-info" Width="120px"></asp:Button>
                                                            <asp:Button ID="btnSubmitRe" runat="server" autocomplete="off" Text="Submit" BackColor="Green" ValidationGroup="g2" ForeColor="White" Font-Bold="true" Height="32px" Font-Size="Large" class="btn btn-info" Width="120px" AutoPostBack="true" OnClick="btnSubmitRe_Click"></asp:Button>

                                                            <asp:Button ID="btnApproveRe" runat="server" autocomplete="off" Text="Approve" Font-Bold="true" Height="32px" ValidationGroup="g2" Font-Size="small" class="btn btn-info" Width="120px" OnClick="btnApproveRe_Click"></asp:Button>
                                                            <asp:Button ID="btnRejectRe" runat="server" autocomplete="off" Text="Reject" BackColor="Green" ValidationGroup="g2" ForeColor="White" Font-Bold="true" Height="32px" Font-Size="Large" class="btn btn-info" Width="120px" AutoPostBack="true" OnClick="btnRejectRe_Click"></asp:Button>



                                                        </div>

                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>

                                    <div runat="server" id="divRebAttach" visible="false">
                                        <asp:GridView ID="grdRebAttach" runat="server" AutoGenerateColumns="false" BackColor="White" BorderColor="#E7E7FF" HeaderStyle-BackColor="#ff9900" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl. No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Document name" HeaderText="Document name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="Academic Year" HeaderText="Academic Year" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="Content Type" HeaderText="Content Type" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="File Name" HeaderText="File Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <asp:LinkButton ID="lnkDownloadReb" Text="Download" CommandArgument='<%# Eval("ID") %>' runat="server" OnClick="DownloadInboxFileRe"></asp:LinkButton>
                                                            </ContentTemplate>

                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="lnkDownloadReb" />

                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">

                                                            <ContentTemplate>
                                                                <asp:LinkButton ID="lnkdeleteReb" Text="Delete" CommandArgument='<%# Eval("ID") %>' runat="server" OnClick="DeleteFileRe"></asp:LinkButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="lnkdeleteReb" />

                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <br />
                                    <br />
                                    <br />
                                </ContentTemplate>

                            </asp:UpdatePanel>







                            <asp:Panel ID="pnlApproval" runat="server" Visible="false">
                                <table>
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="Label5" runat="server"
                                                Text="Approval" Font-Size="15pt" ForeColor="#093A62"
                                                Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView1" DataKeyNames="Application_No_" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="2px" CellPadding="3" Width="1000px" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-CssClass="column" HeaderStyle-CssClass="column">

                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Application_No_" ItemStyle-Width="70px" HeaderText="Application No" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="applicant_name" HeaderText="Name" ItemStyle-Width="180px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="academic year" HeaderText="Academic Year" ItemStyle-Width="80px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="designation" HeaderText="Designation" ItemStyle-Width="120px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="dept" HeaderText="Department" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="POT" HeaderText="Purpose of travelling" ItemStyle-Width="280px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <%-- <asp:BoundField DataField="PrincipalStatus" HeaderText="Principal Status" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="SecondApprovalStatus" HeaderText="Coordinator (R&D)" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="ThirdApprovalStatus" HeaderText="Associate Dean (R&D)" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="VCApprovalStatus" HeaderText="VC Approval" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />--%>

                                                    <asp:TemplateField HeaderText="Select" ItemStyle-CssClass="column" HeaderStyle-CssClass="column">
                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="lnkSelect1" ForeColor="Blue" CommandArgument='<%# Eval("Application_No_") %>' Text="Select" runat="server" OnClick="lnkSelect1_Click"></asp:LinkButton>
                                                            <asp:HiddenField ID="hdfAppNo" runat="server" />
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
                            <asp:Panel ID="pnlApprovalRebursement" runat="server" Visible="false">
                                <table>
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server"
                                                Text="Approval" Font-Size="15pt" ForeColor="#093A62"
                                                Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView2" DataKeyNames="Application_No_" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="2px" CellPadding="3" Width="1000px" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-CssClass="column" HeaderStyle-CssClass="column">

                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Application_No_" ItemStyle-Width="70px" HeaderText="Application No" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="applicant_name" HeaderText="Name" ItemStyle-Width="180px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="academic year" HeaderText="Academic Year" ItemStyle-Width="80px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="designation" HeaderText="Designation" ItemStyle-Width="250px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="dept" HeaderText="Department" ItemStyle-Width="250px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />

                                                    <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="250px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />


                                                    <asp:TemplateField HeaderText="Select" ItemStyle-CssClass="column" HeaderStyle-CssClass="column">
                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="lnkSelectReApproval" ForeColor="Blue" CommandArgument='<%# Eval("Application_No_") %>' Text="Select" runat="server" OnClick="lnkSelect123_Click"></asp:LinkButton>
                                                            <asp:HiddenField ID="HiddenField2" runat="server" />
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
                            <asp:Panel ID="pnlRepport" runat="server" Visible="false">
                                <div class="col-sm-16">
                                    <fieldset class="boxBody">
                                        <div class="col-sm-4">
                                            <div class="pull-left">
                                                <asp:Label ID="lblDateFrom" runat="server" Text="From:"></asp:Label>
                                            </div>
                                            <div class="pull-left" style="padding-left: 3%">
                                                <asp:TextBox ID="txtDateFrom" runat="server" Width="100px" Height="22px" placeholder="From Date" onkeypress="return false" onKeyDown="preventBackspace();"
                                                    oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender3" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDateFrom"
                                                    CssClass="cal_Theme1" Enabled="true" TargetControlID="txtDateFrom">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div> 
                                        <div class="col-sm-4">
                                             <div class="pull-left" style="padding-left: 3%">

                                                 <asp:Label ID="lblDateTo" runat="server" Text="To:"></asp:Label>
                                             </div>

                                             <div class="pull-left" style="padding-left: 3%">
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ValidationGroup="g11" Display="Dynamic" ControlToValidate="txtDateTo" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                                 <asp:TextBox ID="txtDateTo" runat="server" Width="100px" Height="22px" placeholder="To Date" onkeypress="return false" onKeyDown="preventBackspace();"
                                                     oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                                 <asp:CalendarExtender ID="CalendarExtender4" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDateTo"
                                                     CssClass="cal_Theme1" Enabled="true" TargetControlID="txtDateTo">
                                                 </asp:CalendarExtender>
                                        </div>
                                            <div class="col-sm-4">
                                                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Height="30px" ValidationGroup="g11" class="btn btn-success" Text="Search" />
                                                </div>
                                    </div>
                                        </fieldset>
                                      </div>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="Label3" runat="server"
                                                Text="Research Incentive Report" Font-Size="15pt" ForeColor="#093A62"
                                                Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>
                                    <tr>
                                        <td>


                                            <rsweb:reportviewer id="ReportViewer1" runat="server" width="100%" hyperlinktarget="_blank"></rsweb:reportviewer>
                                            <asp:Label ID="notshow" runat="server" Font-Size="15pt" Visible="false" ></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>





                        </td>
                    </tr>


                </table>





            </td>


        </tr>
    </table>


    <div id="confirmModal5" class="modal fade confirm-modal" role="dialog">

        <div class="modal-dialog modalPopup" style="width: 750px; height: 150px">
            <div style="text-align: right; padding-bottom: -40px">
                <asp:Button ID="btnclose" runat="server" Text="X" OnClientClick="HidePopup();" Font-Size="Larger" />
            </div>
            <div class="clearfix" style="margin-bottom: 10px; margin-top: 10px; margin-left: 20px">

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>


                        <div class="col-sm-8 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Reject Remarks</label>

                                <div class="col-sm-8">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ValidationGroup="g3" Display="Dynamic" ControlToValidate="TextBox1" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" ValidationGroup="g3" TextMode="MultiLine" Width="452px"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-2 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font: bold; visibility: hidden; font-size: small">&nbsp&nbsp&nbsp  Registration fee details  </label>

                                <div class="col-sm-8">


                                    <asp:Button ID="Button4" runat="server" autocomplete="off" Text="Reject" Font-Bold="true" ValidationGroup="g3" OnClick="Button4_Click" Height="32px" Font-Size="small" class="btn btn-info" Width="120px"></asp:Button>
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>

                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div id="confirmModal6" class="modal fade confirm-modal" role="dialog">

        <div class="modal-dialog modalPopup" style="width: 750px; height: 150px">
            <div style="text-align: right; padding-bottom: -40px">
                <asp:Button ID="Button1" runat="server" Text="X" OnClientClick="HidePopup6();" Font-Size="Larger" />
            </div>
            <div class="clearfix" style="margin-bottom: 10px; margin-top: 10px; margin-left: 20px">

                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>


                        <div class="col-sm-8 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Reject Remarks</label>

                                <div class="col-sm-8">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ValidationGroup="g8" Display="Dynamic" ControlToValidate="txtRejectRemark" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtRejectRemark" runat="server" CssClass="form-control" ValidationGroup="g8" TextMode="MultiLine" Width="452px"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-2 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font: bold; visibility: hidden; font-size: small">&nbsp&nbsp&nbsp  Registration fee details  </label>

                                <div class="col-sm-8">


                                    <asp:Button ID="btnRejectPop" runat="server" Text="Reject" Font-Bold="true" ValidationGroup="g8" AutoPostBack="true" OnClick="btnRejectPop_Click" Height="32px" Font-Size="small" class="btn btn-info" Width="120px"></asp:Button>
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>

                </asp:UpdatePanel>
            </div>
        </div>
    </div>






</asp:Content>

