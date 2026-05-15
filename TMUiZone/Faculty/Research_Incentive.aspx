<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="Research_Incentive.aspx.cs" Inherits="Faculty_Research_Incentive" %>

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
        $(document).ready(function () {
            var container = document.getElementById('pnlApplication');
            var div = document.createElement('div');
            var txtItemId = document.createElement('input');
            txtItemId.type = 'text';
            txtItemId.id = 'ItemId_';
            txtItemId.name = 'ItemId[]';
            txtItemId.class = 'col-md-2';
            txtItemId.value = 1;
            div.appendChild(txtItemId);

            console.log(container.appendChild(div))

        });

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

            $('#confirmModal').modal('hide');
        }
        function VisiblePopup() {
            $('#confirmModal').modal('show');

        }
        function VisibleHoldPopup() {
            $('#HoldModal').modal('show');
        }

        function HideHoldPopup() {
            $('#HoldModal').modal('hide');
        }


        function HidePopup10() {

            $('#confirmModal10').modal('hide');
        }
        function VisiblePopup10() {
            $('#confirmModal10').modal('show');
        }
        function HideRecoveryModel1() {

            $('#RecoveryModel1').modal('hide');
        }
        function VisibleRecoveryModel1() {
            $('#RecoveryModel1').modal('show');
        }
        function HideRecoveryModel2() {

            $('#RecoveryModel2').modal('hide');
        }
        function VisibleRecoveryModel2() {
            $('#RecoveryModel2').modal('show');
        }
        function CheckBoxCall(e) {
            var f;
            if (e.checked == false) {
                f = 1;
            }

            document.getElementById("ContentPlaceHolder1_chkScopus1").checked = false;
            document.getElementById("ContentPlaceHolder1_chkScopus2").checked = false;
            document.getElementById("ContentPlaceHolder1_chkScopus3").checked = false;
            document.getElementById("ContentPlaceHolder1_chkScopus4").checked = false;
            if (f == 1) {
                document.getElementById(e.id).checked = false;
            }
            else {
                document.getElementById(e.id).checked = true;
            }

        }
    </script>






</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="RESEARCH INCENTIVE" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

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
                        <td class="leftmMenu">&nbsp;<asp:LinkButton ID="lnkResearchTitle" Width="140px" runat="server" OnClick="lnkResearchTitle_Click">Research Title</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td class="leftmMenu">&nbsp;<asp:LinkButton ID="lnkResearchTitleApproval" Width="140px" runat="server" OnClick="lnkResearchTitleApproval_Click">Title Approval</asp:LinkButton></td>
                    </tr>

                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td class="leftmMenu">
                            <asp:LinkButton ID="lnkApplication" runat="server" Width="140px" OnClick="lnkApplication_Click">Research Application</asp:LinkButton>

                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td class="leftmMenu">
                            <asp:LinkButton ID="lnkReApproval" Width="140px" runat="server" OnClick="lnkReApproval_Click">Application Approval</asp:LinkButton>

                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td class="leftmMenu">
                            <asp:LinkButton ID="lnkReport" Width="140px" runat="server" OnClick="lnkReport_Click">Report</asp:LinkButton>

                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td class="leftmMenu">
                            <asp:LinkButton ID="lnkRecovery" Width="140px" runat="server" Visible="false" OnClick="lnkRecovery_Click">Recovery Request</asp:LinkButton>
                            <asp:LinkButton ID="lnkRecoveryApproval" Width="140px" runat="server" Visible="false" OnClick="lnkRecoveryApproval_Click">Recovery Approval</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td class="leftmMenu">
                            <asp:LinkButton ID="lnkRecoveryReport" Width="140px" runat="server" Visible="false" OnClick="lnkRecoveryReport_Click">Recovery Report</asp:LinkButton>
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
                            <asp:Panel ID="pnlTitleList" runat="server" Visible="true">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnAddNew" OnClick="btnAddNew_Click" runat="server" Width="120px" Text="Add New" />
                                            <div style="overflow-y: scroll; width: 1000px">
                                                <asp:GridView ID="grdTitleList" DataKeyNames="id" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="2px" CellPadding="3" Width="1000px" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl. No." ItemStyle-CssClass="column" HeaderStyle-CssClass="column">

                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Title_of_Paper" ItemStyle-Width="170px" HeaderText="Title of Paper" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                        <asp:BoundField DataField="Name_of_Journal" HeaderText="Name of Journal" ItemStyle-Width="250px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                        <asp:BoundField DataField="Volume" HeaderText="Volume" ItemStyle-Width="80px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                        <asp:BoundField DataField="Page_No" HeaderText="Page No" ItemStyle-Width="80px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                        <asp:BoundField DataField="Date_Of_Publication" HeaderText="Publication Date" ItemStyle-Width="120px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                        <asp:BoundField DataField="No_of_author" HeaderText="No Of Author" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />





                                                        <asp:TemplateField HeaderText="Link of Article" ItemStyle-CssClass="column" HeaderStyle-CssClass="column">
                                                            <ItemTemplate>



                                                                <asp:HyperLink ID="lnkdelete" Text='<%# Eval("Link_of_Author") %>' Target="_blank" NavigateUrl='<%# Eval("Link_of_Author") %>' runat="server"></asp:HyperLink>



                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                        <asp:BoundField DataField="Type_of_RI_Name" HeaderText="Type Of Research Incentive" ItemStyle-Width="180px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                        <asp:BoundField DataField="Approval_status" HeaderText="Approval Status" ItemStyle-Width="180px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />



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

                            <asp:Panel ID="pnlTitleApplication" runat="server" Visible="false">
                                <%--<ContentTemplate>--%>
                                <div class="col-sm-3 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp Type Of Research Incentive</label>

                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="drpResearchIncentive" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="drpResearchIncentive_SelectedIndexChanged" Width="200px"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 p-0">
                                    <div class="form-group clearfix">
                                        <asp:Label ID="lblTitlepaper" runat="server" class="col-form-label" Text="Title of the Paper" Width="200px" Style="font-size: small; margin-left: 15px"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txttitlePaper" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txttitlePaper" runat="server" CssClass="form-control" Width="200px"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 p-0">
                                    <div class="form-group clearfix">
                                        <asp:Label ID="lNameOfJournal" runat="server" class="col-form-label" Text="Name of the Journal" Style="font-size: small; margin-left: 15px"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtNameofJournal" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtNameofJournal" runat="server" CssClass="form-control" Width="200px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-1 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Volume</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="g11" Display="Dynamic" ControlToValidate="txtVolume" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtVolume" runat="server" CssClass="form-control" Width="70px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-2 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Issue</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtNoIssue" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtNoIssue" runat="server" CssClass="form-control" Width="100px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 100px">&nbsp&nbsp&nbsp  Page No. </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtPageNop" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtPageNop" runat="server" CssClass="form-control" Width="200px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 p-0">
                                    <div class="form-group clearfix">
                                        <asp:Label ID="lblISBN" runat="server" class="col-form-label" Text="ISSN no." Width="200px" Style="font-size: small; margin-left: 15px">  </asp:Label>

                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtISSNno" runat="server" autocomplete="off" CssClass="form-control" Width="200px"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp  Date of Publication </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtDOP" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtDOP" runat="server" CssClass="form-control" onkeydown="return false;"  onpaste="return false;" Width="200px"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDOP" Format="dd-MM-yyyy"></asp:CalendarExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp Total No. of Author(s)  </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtNumberOfAuthor" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtNumberOfAuthor" runat="server" CssClass="form-control" Width="200px" onkeypress="return isNumberKey(event)">
                                            </asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp  Link of Article   </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtLOA" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtLOA" runat="server" CssClass="form-control" Width="452px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-6 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font: bold; visibility: hidden; font-size: small">&nbsp&nbsp&nbsp  Registration fee details of the Conference/Seminar/Workshop </label>

                                        <div class="col-sm-8">
                                            <asp:Button ID="btnSubmit" runat="server" autocomplete="off" Text="SUBMIT" Font-Bold="true" Height="32px" Font-Size="small" OnClick="btnSubmit_Click" class="btn btn-info" Width="120px"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                                <%--</ContentTemplate>--%>
                                <%-- <Triggers>
                                    <asp:PostBackTrigger ControlID="drpResearchIncentive" />
                                </Triggers>--%>
                            </asp:Panel>

                            <asp:Panel ID="pnlRIApproval" runat="server" Visible="false">
                                <table>
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="Label4" runat="server"
                                                Text="Title Approval" Font-Size="15pt" ForeColor="#093A62"
                                                Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 1px; background-color: #f1f1f1">
                                            <div style="overflow-y: scroll; width: 1000px">

                                                <asp:GridView ID="grdApproval" DataKeyNames="id" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="2px" CellPadding="3" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl. No." ItemStyle-CssClass="column" HeaderStyle-CssClass="column">

                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="RS" ItemStyle-Width="800px" HeaderText="Research Scholar" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                        <asp:BoundField DataField="Title_of_Paper" ItemStyle-Width="800px" HeaderText="Title of Paper" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                        <asp:BoundField DataField="Name_of_Journal" HeaderText="Name of Journal" ItemStyle-Width="300px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                        <asp:BoundField DataField="Volume" HeaderText="Volume" ItemStyle-Width="80px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                        <asp:BoundField DataField="Page_No" HeaderText="Page No" ItemStyle-Width="80px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                        <asp:BoundField DataField="Date_Of_Publication" HeaderText="Publication Date" ItemStyle-Width="120px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                        <asp:BoundField DataField="approval_status" HeaderText="Status" ItemStyle-Width="120px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                        <asp:BoundField DataField="create_by" HeaderText="Create by" ItemStyle-Width="120px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                        <%-- <asp:BoundField DataField="No_of_author" HeaderText="No Of Author" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />





                                                        <asp:TemplateField HeaderText="Link of Author">
                                                            <ItemTemplate>



                                                                <asp:LinkButton ID="lnkdelete" Text='<%# Eval("Link_of_Author") %>' CommandArgument='<%# Eval("Link_of_Author") %>' runat="server"></asp:LinkButton>



                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                        <asp:BoundField DataField="Type_of_RI_Name" HeaderText="Type Of Research Incentive" ItemStyle-Width="180px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                        --%>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-CssClass="column" HeaderStyle-CssClass="column">
                                                            <ItemTemplate>

                                                                <asp:LinkButton ID="lnSelect" Text="Select" OnClick="lnSelect_Click" CommandArgument='<%# Eval("id") %>' runat="server"></asp:LinkButton>


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


                            <asp:Panel ID="pnlApplicationList" runat="server" Visible="false">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnAddNewApplication" OnClick="btnAddNewApplication_Click" runat="server" Width="120px" Text="Add New" />

                                            <asp:GridView ID="grdApplication" DataKeyNames="ID,Hold" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="2px" CellPadding="3" Width="1000px" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-CssClass="column" HeaderStyle-CssClass="column">

                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Application_No_" ItemStyle-Width="170px" HeaderText="Application No." ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="applicant_name" ItemStyle-Width="170px" HeaderText="Employee No." ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />

                                                    <asp:BoundField DataField="Application_Title" HeaderText="Application Title" ItemStyle-Width="250px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="WebLink" HeaderText="WebLink" ItemStyle-Width="80px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="DOP" HeaderText="DOP" ItemStyle-Width="220px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="Book_Submit_RD" HeaderText="Book Submit to R&D" ItemStyle-Width="120px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <%-- <asp:BoundField DataField="SCOPUS" HeaderText="SCOPUS" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="WOS" HeaderText="WOS" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                     <asp:BoundField DataField="SCI" HeaderText="SCI" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                     <asp:BoundField DataField="SSCI" HeaderText="SSCI" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                     <asp:BoundField DataField="UGC_Core" HeaderText="UGC_Core" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />

                                                     <asp:BoundField DataField="ICI" HeaderText="ICI" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                     <asp:BoundField DataField="ABDC" HeaderText="ABDC" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />--%>

                                                    <asp:BoundField DataField="PrincipalStatus" HeaderText="Principal Status" ItemStyle-Width="180px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />

                                                    <asp:BoundField DataField="SecondApprovalStatus" HeaderText="Coordinator (R&D)" ItemStyle-Width="180px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />

                                                    <asp:BoundField DataField="ThirdApprovalStatus" HeaderText="Associate Dean (R&D)" ItemStyle-Width="180px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />

                                                    <asp:BoundField DataField="VCApprovalStatus" HeaderText="VC Approval" ItemStyle-Width="180px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />


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











                            <asp:Panel ID="pnlApplication" Visible="false" runat="server">
                                <%--  <ContentTemplate>--%>


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
                                            <asp:LinkButton ID="btnbackTitle" runat="server" Width="140px" Text="Back to List" OnClick="btnbackTitle_Click" />
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txtApplicant" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtApplicant" runat="server" CssClass="form-control" Width="200px" Enabled="false"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp Designation</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txtDesignation" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" Width="200px" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp Name of College</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txtCollege" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtCollege" runat="server" CssClass="form-control" Width="200px" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp  Department </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txtDep" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtDep" runat="server" CssClass="form-control" Width="200px" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-3 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp  Application Title   </label>

                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="drpAppTitle" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpAppTitle_SelectedIndexChanged" Width="200px">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp  Web Link /DOI </label>

                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtWeblink" runat="server" autocomplete="off" CssClass="form-control" Width="200px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp Date of Publication </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txtDOPub" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtDOPub" runat="server" autocomplete="off" CssClass="form-control" Enabled="false" Width="200px"></asp:TextBox>
                                            <asp:HiddenField ID="hdfAuth" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp ISSN/ISBN No. </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txtISSN" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtISSN" runat="server" autocomplete="off" CssClass="form-control" Width="200px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>


                                <div class="col-sm-3 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp  Name of the Journal   </label>

                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtNameofJournal1" runat="server" Enabled="false" CssClass="form-control" Width="200px">
                                            </asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-1 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp  Volume </label>

                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtVolume1" runat="server" autocomplete="off" Enabled="false" CssClass="form-control" Width="50px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-2 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Issue </label>

                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtNoIssue1" runat="server" autocomplete="off" CssClass="form-control" Width="50px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Page No. </label>

                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtPageno1" runat="server" autocomplete="off" CssClass="form-control" Enabled="false" Width="100px"></asp:TextBox>

                                        </div>
                                    </div>

                                </div>
                                <div class="col-sm-3">
                                    <label for="inputEmail3" class="col-form-label" style="font-size: small;">Author Position </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="R1" Display="Dynamic" ControlToValidate="txtAuthposition" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtAuthposition" runat="server" onkeypress="return isNumberKey(event)" ValidationGroup="R1" autocomplete="off" CssClass="form-control" Width="200px"></asp:TextBox>
                                </div>







                                <div class="col-sm-6 p-0">
                                    <div class="form-group clearfix">
                                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Book Submitted to R & D Center </label>

                                        <div class="col-sm-3">
                                            <asp:CheckBox ID="chkBookSubmit" runat="server" Checked="true"></asp:CheckBox>
                                        </div>



                                    </div>

                                </div>
                                <div class="col-sm-6 p-0">
                                    <div class="form-group clearfix">
                                        <asp:LinkButton ID="lnkAnnexure" Text="Download Undertaking proforma-Research Scholar" OnClick="lnkAnnexure_Click" Font-Italic="true" ForeColor="Green" runat="server" class="col-form-label" Style="font-size: small; width: 350px"> </asp:LinkButton>

                                        <div class="col-sm-3">
                                        </div>



                                    </div>

                                </div>

                                <fieldset class="boxBody">
                                    <div class="col-sm-16 p-0">

                                        <asp:Label ID="Label2" runat="server"
                                            Text="INDEXED" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                    </div>
                                    <br />
                                    <div class="col-sm-2 p-0">
                                        <asp:CheckBox ID="chkScopus1" runat="server" Text="SCOPUS-Q1" onclick="CheckBoxCall(this)" />
                                    </div>
                                    <div class="col-sm-2 p-0">
                                        <asp:CheckBox ID="chkScopus2" runat="server" Text="SCOPUS-Q2" onclick="CheckBoxCall(this)" />
                                    </div>
                                    <div class="col-sm-2 p-0">
                                        <asp:CheckBox ID="chkScopus3" runat="server" Text="SCOPUS-Q3" onclick="CheckBoxCall(this)" />
                                    </div>
                                    <div class="col-sm-2 p-0">
                                        <asp:CheckBox ID="chkScopus4" runat="server" Text="SCOPUS-Q4" onclick="CheckBoxCall(this)" />
                                    </div>
                                    <div class="col-sm-2 p-0">
                                        <asp:CheckBox ID="chkWOSSSCI" runat="server" Text="WOS-SSCI" />
                                    </div>
                                    <div class="col-sm-2 p-0">
                                        <asp:CheckBox ID="chkWOSSCIE" runat="server" Text="WOS-SCIE" />
                                    </div>
                                    <div class="col-sm-2 p-0">
                                        <asp:CheckBox ID="chkWOSESCI" runat="server" Text="WOS-ESCI" />
                                    </div>
                                    <div class="col-sm-2 p-0">
                                        <asp:CheckBox ID="chkWOSAHCI" runat="server" Text="WOS-AHCI" />
                                    </div>
                                    <div class="col-sm-2 p-0">
                                        <asp:CheckBox ID="chkSSCI" runat="server" Text="PUBMED" />
                                    </div>
                                    <div class="col-sm-2 p-0">
                                        <asp:CheckBox ID="chkabdc" runat="server" Text="ABDC" />
                                    </div>
                                    <div class="col-sm-2 p-0">
                                        <asp:CheckBox ID="chkICI" runat="server" Text="ICI" />
                                    </div>
                                    <div class="col-sm-2 p-0">
                                        <asp:CheckBox ID="chkNATIONALPublisher" runat="server" Text="National Publisher" />
                                    </div>

                                    <div class="col-sm-2 p-0">
                                        <asp:CheckBox ID="chkOthers" runat="server" Text="Others" />
                                    </div>



                                    <div class="col-sm-2 p-0">
                                        <asp:CheckBox ID="chkUGC" runat="server" Text="UGC Care" />
                                    </div>
                                    <div class="col-sm-2 p-0">
                                        <asp:CheckBox ID="chkNAAS" runat="server" Text="NAAS" />
                                    </div>
                                    <div class="col-sm-2 p-0">
                                        <asp:CheckBox ID="chkwos" runat="server" Text="WOS" />
                                    </div>
                                    <div class="col-sm-2 p-0">
                                        <asp:CheckBox ID="chkSCI" runat="server" Text="SCI" />
                                    </div>
                                    <div class="col-sm-3 p-0">
                                        <asp:CheckBox ID="chkInternational" runat="server" Text="International Publisher" />
                                    </div>

                                </fieldset>

                                <%-- <fieldset class="boxBody" id="divAuthor" style="width: 100%" runat="server">
                                    <div class="col-sm-16 p-0">

                                        <asp:Label ID="Label3" runat="server"
                                            Text="Name of Authors" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                    </div>
                                    <br />


                                </fieldset>--%>

                                <div class="col-sm-16 p-0" style="text-align: right">

                                    <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">Approved Amount </label>

                                    <asp:TextBox ID="txtApproveAmount" runat="server" onkeypress="return isNumberKey(event)" Enabled="false" Text="0" autocomplete="off" CssClass="form-control" Width="100px"></asp:TextBox>

                                    <asp:Button ID="btnSaveTitleApplication" runat="server" autocomplete="off" Text="Save" BackColor="Green" ValidationGroup="R1" ForeColor="White" Font-Bold="true" Height="32px" Font-Size="Large" class="btn btn-info" Width="120px" AutoPostBack="true" OnClick="btnSaveTitleApplication_Click"></asp:Button>
                                    <asp:Button ID="btnSubmitTitleApplication" runat="server" autocomplete="off" Text="Submit" Visible="false" BackColor="Green" ValidationGroup="R1" ForeColor="White" Font-Bold="true" Height="32px" Font-Size="Large" class="btn btn-info" Width="120px" AutoPostBack="true" OnClick="btnSubmitTitleApplication_Click"></asp:Button>

                                    <asp:Button ID="btnApproveTitleApplication" runat="server" autocomplete="off" Text="Approve" BackColor="Green" Visible="false" ValidationGroup="R1" ForeColor="White" Font-Bold="true" Height="32px" Font-Size="Large" class="btn btn-info" Width="120px" AutoPostBack="true" OnClick="btnApproveTitleApplication_Click"></asp:Button>
                                    <asp:Button ID="btnRejectTitleApplication" runat="server" autocomplete="off" OnClientClick="VisiblePopup(); return false;" Text="Reject" BackColor="Green" Visible="false" ForeColor="White" Font-Bold="true" Height="32px" Font-Size="Large" class="btn btn-info" Width="120px"></asp:Button>
                                    <asp:Button ID="btnHold" runat="server" autocomplete="off" OnClientClick="VisibleHoldPopup(); return false;" Text="Hold" BackColor="Green" Visible="false" ForeColor="White" Font-Bold="true" Height="32px" Font-Size="Large" class="btn btn-info" Width="120px"></asp:Button>




                                </div>

                                <%-- </ContentTemplate>--%>
                            </asp:Panel>



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
                                        <asp:PostBackTrigger ControlID="btnUploadDoc" />
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
                                                    <asp:Button ID="btnUploadDoc" runat="server" autocomplete="off" Text="Upload" Font-Bold="true" Height="32px" Font-Size="small" OnClick="btnUploadDoc_Click" class="btn btn-info" Width="120px"></asp:Button>
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


                            <asp:Panel ID="pnlRIApplicationApproval" runat="server" Visible="false">
                                <table>
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="Label5" runat="server"
                                                Text="Application Approval" Font-Size="15pt" ForeColor="#093A62"
                                                Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView1" DataKeyNames="Application_No_,Hold" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="2px" CellPadding="3" Width="1000px" GridLines="Horizontal" EmptyDataText="There are no data records to display." OnRowDataBound="GridView1_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-CssClass="column" HeaderStyle-CssClass="column">

                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Application_No_" ItemStyle-Width="170px" HeaderText="Application No." ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="applicant_name" ItemStyle-Width="170px" HeaderText="Employee Name" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />

                                                    <asp:BoundField DataField="Application_Title" HeaderText="Application Title" ItemStyle-Width="250px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="WebLink" HeaderText="WebLink" ItemStyle-Width="80px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="DOP" HeaderText="DOP" ItemStyle-Width="220px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="Book_Submit_RD" HeaderText="Book Submit to R&D" ItemStyle-Width="120px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <%-- <asp:BoundField DataField="SCOPUS" HeaderText="SCOPUS" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="WOS" HeaderText="WOS" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                     <asp:BoundField DataField="SCI" HeaderText="SCI" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                     <asp:BoundField DataField="SSCI" HeaderText="SSCI" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                     <asp:BoundField DataField="UGC_Core" HeaderText="UGC_Core" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />

                                                     <asp:BoundField DataField="ICI" HeaderText="ICI" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                     <asp:BoundField DataField="ABDC" HeaderText="ABDC" ItemStyle-Width="70px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />--%>

                                                    <asp:BoundField DataField="PrincipalStatus" HeaderText="Principal Status" ItemStyle-Width="180px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />

                                                    <asp:BoundField DataField="SecondApprovalStatus" HeaderText="Coordinator (R&D)" ItemStyle-Width="180px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />

                                                    <asp:BoundField DataField="ThirdApprovalStatus" HeaderText="Associate Dean (R&D)" ItemStyle-Width="180px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />

                                                    <asp:BoundField DataField="VCApprovalStatus" HeaderText="VC Approval" ItemStyle-Width="180px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />


                                                    <asp:TemplateField HeaderText="Select" ItemStyle-CssClass="column" HeaderStyle-CssClass="column">
                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="lnkSelect" ForeColor="Blue" CommandArgument='<%# Eval("Application_No_") %>' Text="Select" runat="server" OnClick="lnkAppSelect_Click"></asp:LinkButton>

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
                                <fieldset class="boxBody">
                                    <div class="col-sm-4">
                                        <div class="pull-left">
                                            <asp:Label ID="lblDateFrom" runat="server" Text="From:"></asp:Label>
                                        </div>
                                        <div class="pull-left" style="padding-left: 3%">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ValidationGroup="g11" Display="Dynamic" ControlToValidate="txtDateFrom" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txtDateFrom" runat="server" Width="100px" Height="22px" placeholder="From Date" onkeypress="return false" onKeyDown="preventBackspace();"
                                                oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDateFrom"
                                                CssClass="cal_Theme1" Enabled="true" TargetControlID="txtDateFrom">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="pull-left" style="padding-left: 3%">

                                            <asp:Label ID="lblDateTo" runat="server" Text="To:"></asp:Label>
                                        </div>

                                        <div class="pull-left" style="padding-left: 3%">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="g11" Display="Dynamic" ControlToValidate="txtDateTo" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txtDateTo" runat="server" Width="100px" Height="22px" placeholder="To Date" onkeypress="return false" onKeyDown="preventBackspace();"
                                                oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender3" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDateTo"
                                                CssClass="cal_Theme1" Enabled="true" TargetControlID="txtDateTo">
                                            </asp:CalendarExtender>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Height="30px" ValidationGroup="g11" class="btn btn-success" Text="Search" />
                                        </div>
                                    </div>
                                </fieldset>
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


                                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" HyperlinkTarget="_blank"></rsweb:ReportViewer>
                                            <asp:Label ID="notshow" runat="server" Font-Size="15pt" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>

                            <asp:Panel ID="pnlRecoveryReq" runat="server" Visible="false">
                                <table>
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="Label12" runat="server"
                                                Text="Recovery Request" Font-Size="15pt" ForeColor="#093A62"
                                                Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView2" DataKeyNames="Application_No_,Hold" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="2px" CellPadding="3" Width="1000px" GridLines="Horizontal" EmptyDataText="There are no data records to display." OnRowDataBound="GridView2_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-CssClass="column" HeaderStyle-CssClass="column">

                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Application_No_" ItemStyle-Width="170px" HeaderText="Application No." ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="applicant_name" ItemStyle-Width="170px" HeaderText="Employee Name" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />

                                                    <asp:BoundField DataField="Application_Title" HeaderText="Application Title" ItemStyle-Width="250px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="WebLink" HeaderText="WebLink" ItemStyle-Width="60px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="DOP" HeaderText="DOP" ItemStyle-Width="220px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="Book_Submit_RD" HeaderText="Book Submit to R&D" ItemStyle-Width="120px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />

                                                    <asp:BoundField DataField="VCApprovalStatus" HeaderText="VC Approval" ItemStyle-Width="180px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />

                                                    <asp:TemplateField HeaderText="Select" ItemStyle-CssClass="column" HeaderStyle-CssClass="column">
                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="lnkSelectRecovery" ForeColor="Blue" CommandArgument='<%# Eval("Application_No_") %>' Text="Select" runat="server" OnClick="lnkSelectRecovery_Click"></asp:LinkButton>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Recovery Request" ItemStyle-CssClass="column" HeaderStyle-CssClass="column">
                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="lnkRecoveryRequest" ForeColor="Blue" CommandArgument='<%# Eval("Application_No_") %>' Text="Recovery" runat="server" OnClick="lnkRecoveryRequest_Click"></asp:LinkButton>

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



                            <asp:Panel ID="pnlRecoveryReport" runat="server" Visible="false">
                                <fieldset class="boxBody">
                                    <div class="col-sm-4">
                                        <div class="pull-left">
                                            <asp:Label ID="Label8" runat="server" Text="From:"></asp:Label>
                                        </div>
                                        <div class="pull-left" style="padding-left: 3%">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ValidationGroup="g11" Display="Dynamic" ControlToValidate="TextBox1" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="TextBox1" runat="server" Width="100px" Height="22px" placeholder="From Date" onkeypress="return false" onKeyDown="preventBackspace();"
                                                oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender4" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDateFrom"
                                                CssClass="cal_Theme1" Enabled="true" TargetControlID="TextBox1">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="pull-left" style="padding-left: 3%">

                                            <asp:Label ID="Label9" runat="server" Text="To:"></asp:Label>
                                        </div>

                                        <div class="pull-left" style="padding-left: 3%">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ValidationGroup="g11" Display="Dynamic" ControlToValidate="TextBox2" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="TextBox2" runat="server" Width="100px" Height="22px" placeholder="To Date" onkeypress="return false" onKeyDown="preventBackspace();"
                                                oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender5" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDateTo"
                                                CssClass="cal_Theme1" Enabled="true" TargetControlID="TextBox2">
                                            </asp:CalendarExtender>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Height="30px" ValidationGroup="g11" class="btn btn-success" Text="Search" />
                                        </div>
                                    </div>
                                </fieldset>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="Label10" runat="server"
                                                Text="Research Incentive Recovery Report" Font-Size="15pt" ForeColor="#093A62"
                                                Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>
                                    <tr>
                                        <td>


                                            <rsweb:ReportViewer ID="ReportViewer2" runat="server" Width="100%" HyperlinkTarget="_blank"></rsweb:ReportViewer>
                                            <asp:Label ID="Label11" runat="server" Font-Size="15pt" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>


                            <asp:Panel ID="pnlRecoveryApproval" runat="server" Visible="false">
                                <table>
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="Label13" runat="server"
                                                Text="Recovery Request Approval" Font-Size="15pt" ForeColor="#093A62"
                                                Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView3" DataKeyNames="Application_No_,Hold" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="2px" CellPadding="3" Width="1000px" GridLines="Horizontal" EmptyDataText="There are no data records to display." OnRowDataBound="GridView3_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-CssClass="column" HeaderStyle-CssClass="column">

                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Application_No_" ItemStyle-Width="170px" HeaderText="Application No." ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="applicant_name" ItemStyle-Width="170px" HeaderText="Employee Name" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />

                                                    <asp:BoundField DataField="Application_Title" HeaderText="Application Title" ItemStyle-Width="250px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="WebLink" HeaderText="WebLink" ItemStyle-Width="60px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="DOP" HeaderText="DOP" ItemStyle-Width="220px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />
                                                    <asp:BoundField DataField="Book_Submit_RD" HeaderText="Book Submit to R&D" ItemStyle-Width="120px" ItemStyle-CssClass="column" HeaderStyle-CssClass="column" />


                                                    <asp:TemplateField HeaderText="Select" ItemStyle-CssClass="column" HeaderStyle-CssClass="column">
                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="lnkSelectRecovery" ForeColor="Blue" CommandArgument='<%# Eval("Application_No_") %>' Text="Select" runat="server" OnClick="lnkSelectRecovery_Click"></asp:LinkButton>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Recovery Approval" ItemStyle-CssClass="column" HeaderStyle-CssClass="column">
                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="lnkRecoveryRequestApproval" ForeColor="Blue" CommandArgument='<%# Eval("Application_No_") %>' Text="Approval" runat="server" OnClick="lnkRecoveryRequestApproval_Click"></asp:LinkButton>

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




                        </td>
                    </tr>


                </table>

                <br />
                <br />
            </td>
        </tr>
    </table>


    <div id="confirmModal" class="modal fade confirm-modal" role="dialog">

        <div class="modal-dialog modalPopup" style="width: 750px; height: 150px">
            <div style="text-align: right; padding-bottom: -40px">
                <asp:Label ID="lblmsg" runat="server"></asp:Label>
                <asp:Button ID="Button1" runat="server" Text="X" OnClientClick="HidePopup(); return false;" Font-Size="Larger" />
            </div>
            <div class="clearfix" style="margin-bottom: 10px; margin-top: 10px; margin-left: 20px">




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


                            <asp:Button ID="btnRejectPop" runat="server" Text="Reject" Font-Bold="true" ValidationGroup="g8" OnClick="btnRejectPop_Click" Height="32px" Font-Size="small" class="btn btn-info" Width="120px"></asp:Button>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <div id="HoldModal" class="modal fade confirm-modal" role="dialog">

        <div class="modal-dialog modalPopup" style="width: 750px; height: 200px">
            <div style="text-align: right; padding-bottom: -40px">
                <asp:Label ID="Label7" runat="server"></asp:Label>
                <asp:Button ID="Button3" runat="server" Text="X" OnClientClick="HideHoldPopup(); return false;" Font-Size="Larger" />
            </div>
            <div class="clearfix" style="margin-bottom: 10px; margin-top: 10px; margin-left: 20px">




                <div class="col-sm-8 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Hold Remarks</label>

                        <div class="col-sm-8">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ValidationGroup="g12" Display="Dynamic" ControlToValidate="txtHoldRemark" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtHoldRemark" runat="server" CssClass="form-control" ValidationGroup="g12" TextMode="MultiLine" Width="452px"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="col-sm- p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font: bold; visibility: hidden; font-size: small">&nbsp&nbsp&nbsp  Registration fee details  </label>

                        <div class="col-sm-8">
                            <asp:Button ID="btnHold1" runat="server" Text="Hold" Font-Bold="true" ValidationGroup="g12" OnClick="btnHold1_Click" Height="32px" Font-Size="small" class="btn btn-info" Width="120px"></asp:Button>
                            <asp:Button ID="btnUnhold" runat="server" Text="UnHold" Font-Bold="true" ValidationGroup="g12" OnClick="btnUnhold_Click" Height="32px" Font-Size="small" class="btn btn-info" Width="120px"></asp:Button>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <div id="confirmModal10" class="modal fade confirm-modal" role="dialog">

        <div class="modal-dialog modalPopup" style="width: 1250px; height: 450px">
            <div style="text-align: right; padding-bottom: -40px">
                <asp:Button ID="Button2" runat="server" Text="X" OnClientClick="HidePopup10(); return false;" Font-Size="Larger" />
            </div>
            <div class="clearfix" style="margin-bottom: 10px; margin-top: 10px; margin-left: 20px">

                <div class="col-sm-3 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp Title of the Paper/Patent/Book</label>

                        <div class="col-sm-8">
                            <asp:TextBox ID="lblTitleofPaper" runat="server" TextMode="MultiLine" Style="width: 295px; height: 261px;" CssClass="form-control"></asp:TextBox>
                            <asp:HiddenField ID="hdfTitleofPaper" runat="server" />

                        </div>
                    </div>
                </div>
                <div class="col-sm-3 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small; padding-left: 20px">Name of the Journal/  Application no./ Book/ Publisher name</label>

                        <div class="col-sm-8">
                            <asp:TextBox ID="lblNameofJournal" runat="server" TextMode="MultiLine" Enabled="false" CssClass="form-control" Height="50px" Width="250px"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-3 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Volume</label>

                        <div class="col-sm-8">
                            <asp:Label ID="lblVolume" runat="server" CssClass="form-control" Width="200px"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-sm-3 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp  Page No. </label>

                        <div class="col-sm-8">
                            <asp:Label ID="lblPageNo" runat="server" CssClass="form-control" Width="200px"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-sm-3 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp  Date of Publication </label>

                        <div class="col-sm-8">
                            <asp:Label ID="lblDOP" runat="server" CssClass="form-control" Width="200px"></asp:Label>

                        </div>
                    </div>
                </div>
                <div class="col-sm-3 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp No. of Author  </label>
                        <div class="col-sm-8">
                            <asp:Label ID="lblNoOFAuther" runat="server" CssClass="form-control" Width="200px">
                            </asp:Label>

                        </div>
                    </div>
                </div>
                <div class="col-sm-3 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px; padding-left: 20px">&nbsp&nbsp&nbsp  Link of Article   </label>

                        <div class="col-2" style="padding-left: 20px">
                            <asp:HyperLink ID="hpr" runat="server" Target="_blank"></asp:HyperLink>
                            <%--<asp:LinkButton ID="lblLinkOfAuhtor" runat="server"    Width="200px"></asp:LinkButton>--%>
                        </div>
                    </div>
                </div>
                <div class="col-sm-3 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp Type Of Research Incentive</label>

                        <div class="col-sm-8">
                            <asp:Label ID="lblTypeOfResearch" runat="server" CssClass="form-control" Width="200px"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-sm-3 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small;">&nbsp&nbsp&nbsp Reject Remark</label>

                        <div class="col-sm-8">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="V1" Display="Dynamic" ControlToValidate="txtTitleRejectMark" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtTitleRejectMark" runat="server" CssClass="form-control" ValidationGroup="V1" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-3 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small;">&nbsp&nbsp&nbsp ISSN/ISBN no.</label>

                        <div class="col-sm-8">

                            <asp:Label ID="lblISBNNo" runat="server" CssClass="form-control" Width="200px"></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="col-sm-8 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font: bold; visibility: hidden; font-size: small">&nbsp&nbsp&nbsp  Registration fee details of the Conference/Seminar/Workshop </label>

                        <div class="col-sm-8">
                            <asp:Button ID="btnApproveTitle" runat="server" autocomplete="off" Text="APPROVE" Font-Bold="true" Height="32px" Font-Size="small" OnClick="btnApproveTitle_Click" class="btn btn-info" Width="120px"></asp:Button>
                            <asp:Button ID="btnRejectTitle" runat="server" autocomplete="off" Text="REJECT" ValidationGroup="V1" Font-Bold="true" Height="32px" Font-Size="small" OnClick="btnRejectTitle_Click" class="btn btn-info" Width="120px"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="RecoveryModel1" class="modal fade confirm-modal" role="dialog">

        <div class="modal-dialog modalPopup" style="width: 750px; height: 300px">
            <div style="text-align: right; padding-bottom: -40px">
                <asp:Button ID="Button5" runat="server" Text="X" OnClientClick="HideRecoveryModel1(); return false;" Font-Size="Larger" />
            </div>
            <div class="clearfix" style="margin-bottom: 10px; margin-top: 10px; margin-left: 20px">
                <div class="col-sm-3 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small; padding-left: 20px">Application No.</label>

                        <div class="col-sm-8">
                            <asp:TextBox ID="txtAppNo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-3 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Amount</label>

                        <div class="col-sm-8">
                            <asp:Label ID="lblAmount" runat="server" CssClass="form-control" Width="100px"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-sm-3 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp  Remarks </label>

                        <div class="col-sm-8">
                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="form-control" Width="200px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ValidationGroup="gRecovery" Display="Dynamic" ControlToValidate="txtRemark" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
            <div class="clearfix" style="margin-bottom: 10px; margin-top: 10px; margin-left: 20px">
                <div class="col-sm-6 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp  Attachment </label>

                        <div class="col-sm-8">
                            <asp:FileUpload ID="flrecovery" runat="server" CssClass="form-control" Width="200px"></asp:FileUpload>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ValidationGroup="gRecovery" Display="Dynamic" ControlToValidate="flrecovery" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>


                <div class="col-sm-3 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font: bold; visibility: hidden; font-size: small">&nbsp&nbsp&nbsp  Registration fee details of the Conference/Seminar/Workshop </label>

                        <div class="col-sm-8">
                            <asp:Button ID="Button6" runat="server" autocomplete="off" Text="Request" ValidationGroup="gRecovery" Font-Bold="true" Height="32px" Font-Size="small" OnClick="Button6_Click" class="btn btn-info" Width="120px"></asp:Button>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="RecoveryModel2" class="modal fade confirm-modal" role="dialog">

        <div class="modal-dialog modalPopup" style="width: 750px; height: 350px">
            <div style="text-align: right; padding-bottom: -40px">
                <asp:Button ID="Button7" runat="server" Text="X" OnClientClick="HideRecoveryModel2(); return false;" Font-Size="Larger" />
            </div>
            <div class="clearfix" style="margin-bottom: 10px; margin-top: 10px; margin-left: 20px">
                <div class="col-sm-3 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small; padding-left: 20px">Application No.</label>

                        <div class="col-sm-8">
                            <asp:TextBox ID="TextBox3" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-3 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Amount</label>

                        <div class="col-sm-8">
                            <asp:Label ID="Label14" runat="server" CssClass="form-control" Width="100px"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Associate Dean R&D Remarks </label>

                        <div class="col-sm-8">
                            <asp:TextBox ID="TextBox4" runat="server" TextMode="MultiLine" CssClass="form-control" Enabled="false" Width="300px"></asp:TextBox>

                        </div>
                    </div>
                </div>
            </div>
            <div class="clearfix" style="margin-bottom: 10px; margin-top: 10px; margin-left: 20px">
                <div class="col-sm-6 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small">&nbsp&nbsp&nbsp Download Attachment </label>

                        <div class="col-sm-8">
                            <asp:LinkButton ID="lnkAttachment" runat="server" Text="Download" ForeColor="Blue" OnClick="lnkAttachment_Click" Width="200px"></asp:LinkButton>

                        </div>
                    </div>
                </div>
                <div class="col-sm-6 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font-size: small;width: 250px">&nbsp&nbsp&nbsp Remark </label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="TextBox5" runat="server" TextMode="MultiLine" CssClass="form-control"  Width="300px"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="col-sm-12 p-0">
                    <div class="form-group clearfix">
                        <label for="inputEmail3" class="col-form-label" style="font: bold; visibility: hidden; font-size: small">&nbsp&nbsp&nbsp  Registration fee details of the Conference/Seminar/Workshop </label>

                        <div class="col-sm-8">
                            <asp:Button ID="Button8" runat="server" autocomplete="off" Text="Approve" Font-Bold="true" Height="32px" Font-Size="small" OnClick="Button8_Click" class="btn btn-info" Width="120px"></asp:Button>
                            <asp:Button ID="Button9" runat="server" autocomplete="off" Text="Reject" Font-Bold="true" Height="32px" Font-Size="small" OnClick="Button9_Click" class="btn btn-info" Width="120px"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

