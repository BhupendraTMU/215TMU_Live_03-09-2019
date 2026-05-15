<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="Honorarium_E_VisitV2.aspx.cs" Inherits="Faculty_Honorarium_E_Visit" %>

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
    <style>
        .step-tabs {
            display: flex;
            flex-wrap: wrap;
            gap: 0;
        }

            .step-tabs .tab {
                position: relative;
                padding: 12px 25px;
                background: #e5e5e5;
                color: #666;
                font-weight: 600;
                text-decoration: none;
                margin-right: 10px;
                clip-path: polygon(0 0, 90% 0, 100% 50%, 90% 100%, 0 100%);
                transition: 0.3s;
            }

                /* Active tab */
                .step-tabs .tab.active {
                    background: #4CAF50;
                    color: #fff;
                }

                /* Hover */
                .step-tabs .tab:hover {
                    background: #4CAF50;
                    color: #fff;
                }

                /* Optional: smaller spacing on wrap */
                .step-tabs .tab:last-child {
                    margin-right: 0;
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
        function Confirm() {

            return false;
            //var TOJ = document.getElementById("ContentPlaceHolder1_txtTOJ").value;
            //if (TOJ == "") {
            //    alert('Title of Journal can not be blank');
            //    return false;

            //}
            //else {

            //    return true;
            //}
        }
        function enableControl() {
            var remember = document.getElementById('ContentPlaceHolder1_chkHonorarium');
            if (remember.checked) {
                document.getElementById('ContentPlaceHolder1_txtHonoAMT').disabled = false;
                document.getElementById('ContentPlaceHolder1_txttravelAll').disabled = false;

                document.getElementById('ContentPlaceHolder1_txtACHolderName').disabled = false;
                document.getElementById('ContentPlaceHolder1_txtACCNumber').disabled = false;
                document.getElementById('ContentPlaceHolder1_txtBankName').disabled = false;
                document.getElementById('ContentPlaceHolder1_txtBranch').disabled = false;
                document.getElementById('ContentPlaceHolder1_txtIFSC').disabled = false;
                document.getElementById('ContentPlaceHolder1_flSupportDoc').disabled = false;
                document.getElementById('ContentPlaceHolder1_ddlAttachtype').disabled = false;
                document.getElementById('ContentPlaceHolder1_FileUpload1').disabled = false;

            } else {
                document.getElementById('ContentPlaceHolder1_txtHonoAMT').disabled = true;
                document.getElementById('ContentPlaceHolder1_txttravelAll').disabled = true;

                document.getElementById('ContentPlaceHolder1_txtACHolderName').disabled = true;
                document.getElementById('ContentPlaceHolder1_txtACCNumber').disabled = true;
                document.getElementById('ContentPlaceHolder1_txtBankName').disabled = true;
                document.getElementById('ContentPlaceHolder1_txtBranch').disabled = true;
                document.getElementById('ContentPlaceHolder1_txtIFSC').disabled = true;
                document.getElementById('ContentPlaceHolder1_flSupportDoc').disabled = true;
                document.getElementById('ContentPlaceHolder1_ddlAttachtype').disabled = true;
                document.getElementById('ContentPlaceHolder1_FileUpload1').disabled = true;

            }
        }
        function enableOtherControl() {
            var remember = document.getElementById('ContentPlaceHolder1_chkOther');
            if (remember.checked) {
                document.getElementById('ContentPlaceHolder1_txtOAmount').disabled = false;
                document.getElementById('ContentPlaceHolder1_txtOAccountHolder').disabled = false;
                document.getElementById('ContentPlaceHolder1_txtOAcNo').disabled = false;
                document.getElementById('ContentPlaceHolder1_txtOBankName').disabled = false;
                document.getElementById('ContentPlaceHolder1_txtOBranch').disabled = false;
                document.getElementById('ContentPlaceHolder1_txtOIfsc').disabled = false;
                document.getElementById('ContentPlaceHolder1_flApprovalCopy').disabled = false;


            } else {
                document.getElementById('ContentPlaceHolder1_txtOAmount').disabled = true;
                document.getElementById('ContentPlaceHolder1_txtOAccountHolder').disabled = true;
                document.getElementById('ContentPlaceHolder1_txtOAcNo').disabled = true;
                document.getElementById('ContentPlaceHolder1_txtOBankName').disabled = true;
                document.getElementById('ContentPlaceHolder1_txtOBranch').disabled = true;
                document.getElementById('ContentPlaceHolder1_txtOIfsc').disabled = true;
                document.getElementById('ContentPlaceHolder1_flApprovalCopy').disabled = true;

            }
        }
    </script>





</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Honorarium of the External Members form" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>

    <div class="step-tabs">


        <asp:LinkButton ID="lnkApplication"
            runat="server"
            CssClass="tab" OnClick="lnkApplication_Click">
       Application
        </asp:LinkButton>

        <asp:LinkButton ID="lnkApproval"
            runat="server"
            CssClass="tab" OnClick="lnkApproval_Click">
  Approval
        </asp:LinkButton>
        <asp:LinkButton ID="lnkReport"
            runat="server"
            CssClass="tab" OnClick="lnkReport_Click">
Report
        </asp:LinkButton>

        <asp:HiddenField ID="hdpnlid" runat="server" />

    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <table cellpadding="0px" cellspacing="0px">


        <tr>
            <td>
                <asp:Panel ID="pnlApplicationList" runat="server" Visible="true">
                    <table>
                        <tr>
                            
                               
                                <td style="text-align:right">
                                     <asp:Button ID="btnAddNew" OnClick="btnAddNew_Click" runat="server" Width="120px" Text="Add New" />
                               
                                <asp:GridView ID="grdApplicationList" DataKeyNames="ApplicationNo" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="2px" CellPadding="3"  GridLines="Horizontal" EmptyDataText="There are no data records to display.">
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
                            <td style="width: 10px"></td>
                            <td>
                                <asp:CheckBox ID="chkHonorarium" Text="Honorarium" onclick="return enableControl()" runat="server" />

                            </td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:CheckBox ID="chkOther" Text="Others" onclick="return enableOtherControl()" runat="server" />

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
                    <table>
                        <tr style="border: 1px solid">
                            <td>


                                <asp:Panel ID="pnlCommon" runat="server">

                                    <div class="col-sm-3 p-0">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 250px">&nbsp&nbsp&nbsp Application For</label>

                                            <div class="col-sm-8">

                                                <asp:DropDownList ID="drpAppType" runat="server" CssClass="form-control" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="drpAppType_SelectedIndexChanged">
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 p-0" runat="server" id="divEvent" visible="false">

                                        <div class="form-group clearfix">

                                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Event Name </label>

                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txteventType" runat="server" CssClass="form-control" Width="200px"></asp:TextBox>
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
                                    <div class="col-sm-3 p-0" runat="server" id="div1">

                                        <div class="form-group clearfix">

                                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp College Name </label>

                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="drpcollege" runat="server" CssClass="form-control" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="drpcollege_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 p-0" id="unit" runat="server" visible="false">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font: bold; font-size: small">&nbsp&nbsp&nbsp  Unit Name</label>

                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtUnitName" runat="server" CssClass="form-control" Width="200px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                </asp:Panel>
                            </td>
                        </tr>
                    </table>

                    <table>
                        <tr style="border: 1px solid">
                            <td>
                                <asp:Panel ID="PnlHonorarium" runat="server">
                                    <div class="col-sm-12 p-0">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font: bold; color: green; font-size: medium">&nbsp&nbsp&nbsp   Honorarium Details :-</label>


                                        </div>
                                    </div>



                                    <div class="col-sm-3 p-0">

                                        <div class="form-group clearfix">

                                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Honorarium Amount </label>

                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtHonoAMT" runat="server" Enabled="false" CssClass="form-control" AutoPostBack="true" onkeypress="return isNumberKey(event)" Width="200px" OnTextChanged="txtHonoAMT_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3 p-0">

                                        <div class="form-group clearfix">

                                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Travel Allowance </label>

                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txttravelAll" runat="server" CssClass="form-control" Enabled="false" OnTextChanged="txttravelAll_TextChanged" AutoPostBack="true" onkeypress="return isNumberKey(event)" Width="200px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 p-0">

                                        <div class="form-group clearfix">

                                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Total Amount </label>

                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txttravelAMT" runat="server" CssClass="form-control" Enabled="false" onkeypress="return isNumberKey(event)" Width="200px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 p-0">

                                        <div class="form-group clearfix">

                                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Account Holder Name </label>

                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtACHolderName" runat="server" Enabled="false" CssClass="form-control" Width="200px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 p-0">

                                        <div class="form-group clearfix">

                                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Account Number </label>

                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtACCNumber" runat="server" Enabled="false" CssClass="form-control" onkeypress="return isNumberKey(event)" Width="200px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 p-0">

                                        <div class="form-group clearfix">

                                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Bank Name </label>

                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtBankName" runat="server" Enabled="false" CssClass="form-control" Width="200px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 p-0">

                                        <div class="form-group clearfix">

                                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Branch </label>

                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtBranch" runat="server" Enabled="false" CssClass="form-control" ValidationGroup="R1" Width="200px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 p-0">

                                        <div class="form-group clearfix">

                                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp IFSC Code </label>

                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtIFSC" runat="server" Enabled="false" CssClass="form-control" ValidationGroup="R1" Width="200px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 p-0" runat="server" id="divSupport">

                                        <div class="form-group clearfix">

                                            <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Upload Supporting Document </label>

                                            <div class="col-sm-8">
                                                <asp:FileUpload ID="flSupportDoc" runat="server" Enabled="false" Font-Bold="true" Height="32px" Font-Size="small" Width="200px"></asp:FileUpload>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3 p-0">
                                        <div class="form-group clearfix">
                                            <label for="inputEmail3" class="col-form-label" style="font: bold; font-size: small">&nbsp&nbsp&nbsp  Travelling Bill:</label>

                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlAttachtype" runat="server" Enabled="false" Font-Bold="true" Height="32px" Font-Size="small" Width="220px" AutoPostBack="true">
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

                                                        <asp:FileUpload ID="FileUpload1" runat="server" Font-Bold="true" Height="32px" Enabled="false" Font-Size="small" Width="200px"></asp:FileUpload>


                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-3 p-0">
                                                <div class="form-group clearfix">
                                                    <label for="inputEmail3" class="col-form-label" style="font: bold; font-size: small"></label>

                                                    <div class="col-sm-8">
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr style="border: 1px solid">
                            <td>
                                <asp:Panel ID="pnlOtherExp" runat="server">
                                    <div id="divUploadDoc" runat="server">
                                        <div class="col-sm-12 p-0">
                                            <div class="form-group clearfix">
                                                <label for="inputEmail3" class="col-form-label" style="font: bold; color: green; font-size: medium">&nbsp&nbsp&nbsp   Other Expense :-</label>


                                            </div>
                                        </div>

                                        <div class="col-sm-3 p-0">

                                            <div class="form-group clearfix">

                                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Amount </label>

                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtOAmount" runat="server" Enabled="false" CssClass="form-control" Width="200px"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-3 p-0">

                                            <div class="form-group clearfix">

                                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Account Holder Name </label>

                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtOAccountHolder" runat="server" Enabled="false" CssClass="form-control" Width="200px"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 p-0">

                                            <div class="form-group clearfix">

                                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Account Number </label>

                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtOAcNo" runat="server" Enabled="false" CssClass="form-control" Width="200px"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 p-0">

                                            <div class="form-group clearfix">

                                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp  Bank Name </label>

                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtOBankName" runat="server" Enabled="false" CssClass="form-control" Width="200px"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 p-0">

                                            <div class="form-group clearfix">

                                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp  Branch </label>

                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtOBranch" runat="server" Enabled="false" CssClass="form-control" Width="200px"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 p-0">

                                            <div class="form-group clearfix">

                                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp  IFSC Code </label>

                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtOIfsc" runat="server" Enabled="false" CssClass="form-control" Width="200px"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-3 p-0">
                                            <div class="form-group clearfix">
                                                <label for="inputEmail3" class="col-form-label" style="font: bold; font-size: small">&nbsp&nbsp&nbsp  Upload Supporting Document:</label>

                                                <div class="col-sm-8">

                                                    <asp:FileUpload ID="flApprovalCopy" runat="server" Enabled="false" Font-Bold="true" Height="32px" Font-Size="small" Width="200px"></asp:FileUpload>


                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                </asp:Panel>

                            </td>
                        </tr>
                    </table>





                    <div class="col-sm-12 p-0" style="text-align: right">
                        <div class="form-group clearfix">
                            <label for="inputEmail3" class="col-form-label" style="font: bold; visibility: hidden; font-size: small">&nbsp&nbsp&nbsp  Registration fee details of the Conference/Seminar/Workshop </label>

                            <div class="col-sm-8">
                                <asp:Button ID="btnSubmitApplication" runat="server" autocomplete="off" Text="Submit" BackColor="Green" ValidationGroup="R1" ForeColor="White" Font-Bold="true" Height="32px" Font-Size="Large" class="btn btn-info" Width="120px" AutoPostBack="true" OnClick="btnSubmitApplication_Click"></asp:Button>


                            </div>
                        </div>
                    </div>
                    <br />
                    <br />

                    <%--                    </div>--%>
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
                    <table style="width: 100%">
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server"
                                    Text="Honoraium Report" Font-Size="15pt" ForeColor="#093A62"
                                    Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td>


                                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" HyperlinkTarget="_blank"></rsweb:ReportViewer>

                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>

    <div id="confirmModal1" class="modal fade confirm-modal" role="dialog">

        <div class="modal-dialog modalPopup" style="width: 1000px; height: 600px; overflow: scroll">
            <div style="text-align: right; padding-bottom: -40px">
                <asp:Button ID="Button1" runat="server" Text="X" OnClientClick="HidePopup1();" Font-Size="Larger" />
            </div>
            <div class="clearfix" style="margin-bottom: 10px; margin-top: 10px; margin-left: 20px">

                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnApprove" />
                        <asp:PostBackTrigger ControlID="lnkDoc" />
                        <asp:PostBackTrigger ControlID="lnkSupport" />
                        <asp:PostBackTrigger ControlID="lnkOAttach" />

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

                        <div class="col-sm-12 p-0">
                            <div class="form-group clearfix">
                                <label for="inputEmail3" class="col-form-label" style="font: bold; color: green; font-size: medium">&nbsp&nbsp&nbsp   Honorarium :-</label>


                            </div>
                        </div>



                        <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Honorarium Amount </label>

                                <div class="col-sm-8">
                                    <asp:Label ID="lblHoAmt" runat="server" Enabled="false" ValidationGroup="R1"></asp:Label>
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

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Event Name </label>

                                <div class="col-sm-8">
                                    <asp:Label ID="lblEventtype" runat="server" ValidationGroup="R1"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Travelling Bill </label>

                                <div class="col-sm-8">
                                    <asp:LinkButton ID="lnkDoc" runat="server" Text="Download" OnClick="lnkDoc_Click"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Supporting Document </label>

                                <div class="col-sm-8">
                                    <asp:LinkButton ID="lnkSupport" runat="server" Text="Download" OnClick="lnkSupport_Click"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 p-0">

                            <div class="form-group clearfix">

                                <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp  </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ValidationGroup="XX" Display="Dynamic" ControlToValidate="txtRemark" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                <div class="col-sm-8">
                                    Remark: 
                                    <asp:TextBox ID="txtRemark" runat="server" ValidationGroup="XX" Width="428px" Height="60px" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>


                        <div id="div2" runat="server">
                            <div class="col-sm-12 p-0">
                                <div class="form-group clearfix">
                                    <label for="inputEmail3" class="col-form-label" style="font: bold; color: green; font-size: medium">&nbsp&nbsp&nbsp   Other Expense Details :-</label>


                                </div>
                            </div>

                            <div class="col-sm-3 p-0">

                                <div class="form-group clearfix">

                                    <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Amount </label>

                                    <div class="col-sm-8">
                                        <asp:Label ID="lblOAmount" runat="server" Width="200px"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-3 p-0">

                                <div class="form-group clearfix">

                                    <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Account Holder Name </label>

                                    <div class="col-sm-8">
                                        <asp:Label ID="lblOACCHolderName" runat="server" Width="200px"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3 p-0">

                                <div class="form-group clearfix">

                                    <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp Account Number </label>

                                    <div class="col-sm-8">
                                        <asp:Label ID="lblOAccNumber" runat="server" Width="200px"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3 p-0">

                                <div class="form-group clearfix">

                                    <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp  Bank Name </label>

                                    <div class="col-sm-8">
                                        <asp:Label ID="lblOBankName" runat="server" Width="200px"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3 p-0">

                                <div class="form-group clearfix">

                                    <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp  Branch </label>

                                    <div class="col-sm-8">
                                        <asp:Label ID="lblOBranch" runat="server" Width="200px"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3 p-0">

                                <div class="form-group clearfix">

                                    <label for="inputEmail3" class="col-form-label" style="font-size: small; width: 200px">&nbsp&nbsp&nbsp  IFSC Code </label>

                                    <div class="col-sm-8">
                                        <asp:Label ID="lblOIFSC" runat="server" Width="200px"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-3 p-0">
                                <div class="form-group clearfix">
                                    <label for="inputEmail3" class="col-form-label" style="font: bold; font-size: small">&nbsp&nbsp&nbsp  Approval Copy :</label>

                                    <div class="col-sm-8">

                                        <asp:LinkButton ID="lnkOAttach" runat="server" Font-Bold="true" Height="32px" Text="Download" OnClick="lnkOAttach_Click" Font-Size="small" Width="200px"></asp:LinkButton>


                                    </div>
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

