<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="ODApplication.aspx.cs" Inherits="Faculty_ODApplication" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
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
    <style type="text/css">
        .modalPopup {
            background-color: #ffffdd;
            /*border-width: 3px;*/
            /*border-style: solid;*/
            /*border-color: Gray;*/
            padding: 3px;
            width: 30%;
        }

            .modalPopup .header {
                background-color: #2FBDF1;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .body {
                min-height: 50px;
                line-height: 30px;
                text-align: center;
                padding: 5px;
            }

            .modalPopup .footer {
                padding: 3px;
            }

            .modalPopup .button {
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                background-color: red;
                /*border: 1px solid #5C5C5C;*/
            }

            .modalPopup td {
                text-align: left;
            }

        .redBorder {
            /*border: 1px solid red;*/
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>

    <asp:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
        PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground" CancelControlID="btnHide">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none" Width="500px" Height="500px" ScrollBars="Vertical">
        <div class="header">
            <b>
                <asp:Label ID="lblNotification" runat="server" Text="Attachment List"></asp:Label>
            </b>
            <div class="close">
                <asp:Button ID="btnHide" runat="server" Text="X" />
            </div>
        </div>
        <div class="body">
            <div style="width: 100%">


                <asp:GridView ID="grdInbox" runat="server" AutoGenerateColumns="False" BackColor="White" BorderStyle="None" BorderWidth="1px" CellPadding="20" Width="100%" GridLines="Horizontal" EmptyDataText="Welcome">

                    <AlternatingRowStyle BackColor="#F7F7F7" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl. No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle />

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Document No">
                            <ItemTemplate>
                                <asp:Label ID="lblFCode" runat="server" Text='<%# Eval("[ID]")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle />

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Document Name">
                            <ItemTemplate>
                                <asp:Label ID="lblFCodeName" runat="server" Text='<%# Eval("[File Name]")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle />

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkselect" runat="server" AutoPostBack="true" OnCheckedChanged="chkselect_CheckedChanged"></asp:CheckBox>
                            </ItemTemplate>
                            <ItemStyle />

                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>


            </div>



        </div>

    </asp:Panel>






    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="OD / Tour-Claim" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

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
                        <td class="leftmMenu">&nbsp;<asp:LinkButton ID="lnkODApplication" runat="server" OnClick="lnkODApplication_Click">Application</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td class="leftmMenu">&nbsp;<asp:LinkButton ID="lnkODView" runat="server" OnClick="lnkODView_Click">Report</asp:LinkButton></td>
                    </tr>

                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td class="leftmMenu">
                            <asp:LinkButton ID="lnkApproval" runat="server" OnClick="lnkApproval_Click">Approval</asp:LinkButton>
                            <asp:Label ID="lblCountODAppoval" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>




                </table>

            </td>
            <td style="width: 30px"></td>
            <td style="width: 1px; background-color: #f1f1f1"></td>
            <td style="width: 30px"></td>
            <td valign="top">

                <table cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblHeader" runat="server"
                                Text="Application" Font-Size="15pt" ForeColor="#093A62"
                                Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td style="height: 1px; background-color: #f1f1f1"></td>
                    </tr>

                    <tr>
                        <td style="height: 10px">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                        </td>
                    </tr>




                    <tr>
                        <td>


                            <asp:Panel ID="pnlODApplication" runat="server" Visible="true">
                                <table cellpadding="0px" cellspacing="0px">

                                    <tr>
                                        <td style="width: 10px"></td>
                                        <td style="width: 250px">Employee Code
                                        </td>
                                        <td style="width: 5px"></td>
                                        <td>
                                            <asp:TextBox ID="txtEmployeeCode" runat="server" AutoPostBack="true" OnTextChanged="txtEmployeeCode_TextChanged" Style='text-transform: uppercase' autocomplete="off"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmployeeCode" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>


                                        </td>
                                        <td style="width: 10px"></td>
                                        <td style="width: 250px">Employee Name
                                        </td>
                                        <td style="width: 5px"></td>
                                        <td>
                                            <asp:TextBox ID="txtEmpName" Enabled="false" runat="server" Style='text-transform: uppercase' autocomplete="off"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmpName" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>


                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>

                                        <td style="width: 10px"></td>
                                        <td style="width: 100px">From Date
                                        </td>
                                        <td style="width: 5px"></td>
                                        <td>
                                            <asp:TextBox ID="txtFromDate" runat="server" onkeydown="return false;" autocomplete="off"
                                                oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" AutoPostBack="True" OnTextChanged="txtFromDate_TextChanged"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate" Format="dd MMM yyyy"></asp:CalendarExtender>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromDate" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>


                                        </td>
                                        <td style="width: 5px"></td>
                                        <td style="width: 100px">&nbsp;To Date</td>
                                        <td style="width: 5px"></td>
                                        <td>
                                            <asp:TextBox ID="txtTodate" runat="server" AutoPostBack="True" oncontextmenu="return false" autocomplete="off" oncopy="return false" oncut="return false" onkeydown="return false;" onpaste="return false" OnTextChanged="txtTodate_TextChanged"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd MMM yyyy" TargetControlID="txtTodate">
                                            </asp:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtTodate" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 5px"></td>
                                        <td style="width: 100px">Leave Type</td>
                                        <td style="width: 5px"></td>
                                        <td>
                                            <asp:DropDownList ID="drpLeaveType" runat="server" Height="23px" Width="100px">
                                                <asp:ListItem Text="OD"></asp:ListItem>
                                                <%--<asp:ListItem Text="LWP"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td>&nbsp;</td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:TextBox ID="txtToTime" runat="server" Height="23px" Width="100px" AutoPostBack="True" OnTextChanged="txtToTime_TextChanged" Enabled="False" Visible="False">00:00</asp:TextBox>
                                        </td>
                                        <td></td>
                                    </tr>


                                    <tr>
                                        <td colspan="17" style="height: 10px"></td>
                                    </tr>

                                    <tr>
                                        <td style="width: 10px"></td>
                                        <td>Destination
                                        </td>
                                        <td style="width: 5px"></td>
                                        <td>
                                            <asp:TextBox ID="txtDestination" runat="server" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDestination" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td>Purpose </td>
                                        <td style="width: 5px"></td>
                                        <td colspan="9">
                                            <asp:TextBox ID="txtPurpose" runat="server" Width="550px" MaxLength="100"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtPurpose" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>


                                    <tr>
                                        <td colspan="20" style="height: 10px"></td>
                                    </tr>

                                    <tr>
                                        <td style="width: 10px"></td>
                                        <td>Document No.
                                        </td>
                                        <td style="width: 5px"></td>
                                        <td>
                                            <asp:TextBox ID="txtDocNo" runat="server" Width="70px" Enabled="false"></asp:TextBox>
                                            <asp:Button ID="btnAddDoc" runat="server" Width="98px" Text="Add" OnClick="btnAddDoc_Click" Height="30px"></asp:Button>

                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDocNo" ErrorMessage="Required" SetFocusOnError="True" ></asp:RequiredFieldValidator>--%>
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td>Remarks
                                        </td>
                                        <td style="width: 5px"></td>
                                        <td colspan="9">
                                            <asp:TextBox ID="txtRemarks" runat="server" Width="620px"></asp:TextBox>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="16" style="height: 10px"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="20" align="right">

                                            <table cellpadding="0px" cellspacing="0px">
                                                <tr>
                                                    <td style="width: 200px">
                                                        <asp:FileUpload ID="FileUpload1" runat="server" Text="Upload OD Excel" />

                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnUploadExcel" runat="server" Text="Bulk Upload OD" OnClick="btnUploadExcel_Click" />
                                                    </td>
                                                    <td style="width: 250px"></td>


                                                    <td style="width: 200px">
                                                        <asp:FileUpload ID="FileUpload" runat="server" Text="Upload Document" />

                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                                                    </td>

                                                    <td>
                                                        <asp:Button ID="btnAdd" runat="server" Text="Add Leave" OnClick="btnAdd_Click" ValidationGroup="odapps" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td colspan="16" align="right">

                                            <table cellpadding="0px" cellspacing="0px">
                                                <tr>
                                                    <asp:GridView ID="grdTempLeave" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">

                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl. No.">

                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Employee Code">

                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblEmployeeCode" Style="text-transform: uppercase;" runat="server" Text='<%# Eval("Userid") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Employee Name">

                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblEmployeeName" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="From Date">

                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblFromDate" runat="server" Text='<%# Eval("FromDate") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="To Date">

                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblToDate" runat="server" Text='<%# Eval("ToDate") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Leave Type">

                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblLeaveType" runat="server" Text='<%# Eval("LeaveType") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Destination">

                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblDestination" runat="server" Text='<%# Eval("Destination") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Purpose">

                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblPurpose" runat="server" Text='<%# Eval("Purpose") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remark">

                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="2%" />
                                                            </asp:TemplateField>
                                                        </Columns>


                                                    </asp:GridView>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td colspan="16" style="height: 10px"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="16" align="right">

                                            <table cellpadding="0px" cellspacing="0px">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblODSuccess" runat="server" Text="Application has been send successfully for Approval" Font-Bold="True" Font-Size="12pt" ForeColor="Red" Visible="false"></asp:Label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:Button ID="btnSendForApproval" runat="server" Text="Maker" OnClick="btnSendForApproval_Click" Visible="false" ValidationGroup="odapps" />
                                                        <asp:Button ID="Button1" runat="server" Text="Maker" OnClick="Button1_Click" Visible="false" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="16" style="height: 10px"></td>
                                    </tr>

                                </table>
                            </asp:Panel>




                            <asp:Panel ID="pnlViewStatus" runat="server" Visible="false">
                                <table cellpadding="0px" cellspacing="0px" style="width: 100%">


                                    <tr>
                                        <td>From Date   </td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:TextBox ID="txtFromDate_ViewStatus" runat="server" onkeydown="return false;"
                                                oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFromDate_ViewStatus" Format="dd MMM yyyy"></asp:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtFromDate_ViewStatus" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="ODfilterOwn"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td>To Date </td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:TextBox ID="txtTodate_ViewStatus" runat="server" onkeydown="return false;"
                                                oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtTodate_ViewStatus" Format="dd MMM yyyy"></asp:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtTodate_ViewStatus" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="ODfilterOwn"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>Employee Code</td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:TextBox ID="txtEmployee" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td>Status </td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:DropDownList ID="ddStatus_ViewStatus" runat="server" Height="30px">
                                                <asp:ListItem>All</asp:ListItem>
                                                <asp:ListItem>Pending</asp:ListItem>
                                                <asp:ListItem>Approved</asp:ListItem>
                                                <asp:ListItem>Rejected</asp:ListItem>
                                                <asp:ListItem>Recommend</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:Button ID="btnGet_ViewStatus" runat="server" Text="Get" OnClick="btnGet_ViewStatus_Click" ValidationGroup="ODfilterOwn" /></td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:Button ID="btnExporttoexcel_viewStatus" runat="server" Text="Export To Excel" OnClick="btnExporttoexcel_viewStatus_Click" />
                                        </td>
                                        <td></td>
                                    </tr>

                                    <tr>
                                        <td style="height: 10px" colspan="17"></td>
                                    </tr>

                                    <tr>
                                        <td colspan="17">

                                            <asp:GridView ID="grdView_Status" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%" AllowPaging="True" OnPageIndexChanging="grdView_Status_PageIndexChanging">
                                                <Columns>
                                                    <asp:BoundField DataField="Employee No" HeaderText="Employee Code" />
                                                    <asp:BoundField DataField="Employee Name" HeaderText="Employee Name" />
                                                    <asp:BoundField DataField="From Date" HeaderText="From Date" DataFormatString="{0:d}" />

                                                    <asp:BoundField DataField="From Time" HeaderText="From Time" Visible="False" />
                                                    <asp:BoundField DataField="To Date" HeaderText="To Date" DataFormatString="{0:d}" />
                                                    <asp:BoundField DataField="date_difference" HeaderText="Total Days" />
                                                    <asp:BoundField DataField="To Time" HeaderText="To Time" Visible="False" />
                                                    <asp:BoundField DataField="Destination" HeaderText="Destination" />
                                                    <asp:BoundField DataField="Purpose" HeaderText="Purpose">
                                                        <ItemStyle Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                        <ItemStyle Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ApplyById" HeaderText="Apply by ID" />
                                                    <asp:BoundField DataField="Approval" HeaderText="Status" />
                                                    <asp:BoundField DataField="Approval By Name" HeaderText="Approval By" Visible="false" />
                                                    <asp:BoundField DataField="Rejected Remarks" HeaderText="Rejected Remarks">
                                                        <ItemStyle Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Created Date" HeaderText="Apply Date" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}" HtmlEncode="False" />
                                                    <asp:BoundField DataField="Approval Date" HeaderText="Approval Date" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}" HtmlEncode="False" />
                                                </Columns>

                                                <FooterStyle BackColor="#ff9900" ForeColor="#003399" />
                                                <HeaderStyle BackColor="#ff9900" Font-Bold="True" ForeColor="White" Font-Size="10px" />
                                                <PagerStyle CssClass="GridPager" HorizontalAlign="Left" />
                                                <RowStyle BackColor="White" ForeColor="#003399" Font-Size="9px" />
                                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                <SortedDescendingHeaderStyle BackColor="#002876" />
                                            </asp:GridView>

                                        </td>
                                    </tr>


                                    <tr>
                                        <td style="height: 10px" colspan="17"></td>
                                    </tr>

                                </table>




                            </asp:Panel>


                            <asp:Panel ID="pnlApproval" runat="server" Visible="false">
                                <table cellpadding="0px" cellspacing="0px">


                                    <tr>
                                        <td style="width: 10px"></td>
                                        <td>Filter By  </td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:RadioButton ID="rdEmployeeID" runat="server" Text="Employee ID" GroupName="odAppr" AutoPostBack="True" OnCheckedChanged="rdEmployeeID_CheckedChanged" /></td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:RadioButton ID="rdEmployeeName" runat="server" Text="Employee Name" GroupName="odAppr" AutoPostBack="True" OnCheckedChanged="rdEmployeeName_CheckedChanged" /></td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:RadioButton ID="rdDatewise" runat="server" Text="Date" GroupName="odAppr" AutoPostBack="True" OnCheckedChanged="rdDatewise_CheckedChanged" />
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:Panel ID="pnlFilterByIDName" runat="server" Visible="false">
                                                <table cellpadding="0px" cellspacing="0px">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblEmployeeIDNameText" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEmployeeIDNameFilter" runat="server"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>

                                            <asp:Panel ID="pnlFilterDate" runat="server" Visible="false">
                                                <table cellpadding="0px" cellspacing="0px">
                                                    <tr>
                                                        <td>From Date </td>
                                                        <td style="width: 10px"></td>
                                                        <td>
                                                            <asp:TextBox ID="txtFromDate_Approval" runat="server" Width="90px" onkeydown="return false;"
                                                                oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtFromDate_Approval" Format="dd MMM yyyy"></asp:CalendarExtender>
                                                        </td>
                                                        <td>To Date</td>
                                                        <td style="width: 10px"></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTodate_Approval" runat="server" Width="90px" onkeydown="return false;"
                                                                oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtTodate_Approval" Format="dd MMM yyyy"></asp:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>


                                        </td>
                                        <td style="width: 10px"></td>
                                        <td>Status </td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:DropDownList ID="ddStatus_Approval" runat="server" Height="30px" Enabled="False">

                                                <asp:ListItem>Pending</asp:ListItem>
                                                <asp:ListItem>Approved</asp:ListItem>
                                                <asp:ListItem>Rejected</asp:ListItem>
                                                <asp:ListItem>Recommend</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:Button ID="btnFIlterGet_Approval" runat="server" Text="Get" OnClick="btnFIlterGet_Approval_Click" />&nbsp;&nbsp;
                                            <asp:Button ID="btnApproveExport" runat="server" Text="Export Excel" OnClick="btnApproveExport_Click" />
                                        </td>
                                    </tr>



                                    <tr>
                                        <td colspan="16" style="height: 10px"></td>
                                    </tr>

                                    <tr>
                                        <td colspan="16">
                                            <table cellpadding="0px" cellspacing="0px">
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnApprove" runat="server" Text="Checker" Visible="False" OnClick="btnApprove_Click" />
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:Button ID="btnReject" runat="server" Text="Reject" Visible="False" OnClick="btnReject_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td colspan="16" style="height: 10px"></td>
                                    </tr>

                                    <tr>
                                        <td colspan="16">

                                            <asp:GridView ID="grdApproval" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnPageIndexChanging="grdApproval_PageIndexChanging" PageSize="1">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkMark" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="Approval" HeaderText="Status" />

                                                    <%--<asp:BoundField DataField="Employee No" HeaderText="Employee ID" />--%>
                                                    <asp:TemplateField HeaderText="Employee ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmployeeid" runat="server" Text='<%#Bind("[Employee No]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:BoundField DataField="Employee Name" HeaderText="Name" />

                                                    <asp:TemplateField HeaderText="From Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFromDate" runat="server" Text='<%#Bind("[From Date]","{0:dd MMM yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--   <asp:BoundField DataField="From Date" HeaderText="From Date" DataFormatString="{0:D}" />--%>



                                                    <asp:BoundField DataField="From Time" HeaderText="From Time" Visible="False" />

                                                    <%-- <asp:BoundField DataField="To Date" HeaderText="To Date" DataFormatString="{0:D}" />--%>
                                                    <asp:TemplateField HeaderText="To Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblToDate" runat="server" Text='<%#Bind("[To Date]","{0:dd MMM yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:BoundField DataField="To Time" HeaderText="To Time" Visible="False" />
                                                    <asp:BoundField DataField="Destination" HeaderText="Destination">
                                                        <ItemStyle Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Purpose" HeaderText="Purpose">
                                                        <ItemStyle Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                        <ItemStyle Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Rejected Remarks" HeaderText="Rejected Remarks">

                                                        <ItemStyle Width="100px" />
                                                    </asp:BoundField>

                                                    <asp:TemplateField HeaderText="id" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblid" runat="server" Text='<%#Bind("id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:BoundField DataField="Approval By Name" HeaderText="Approval By" />

                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:LinkButton ID="lnkDownload" Text="Download" Visible='<%# Convert.ToString(Eval("DocumentNo"))=="" ? false : true %>' CommandArgument='<%# Eval("DocumentNo") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:PostBackTrigger ControlID="lnkDownload" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#ff9900" ForeColor="#003399" />
                                                <HeaderStyle BackColor="#ff9900" Font-Bold="True" ForeColor="White" Font-Size="10px" />
                                                <PagerStyle CssClass="GridPager" HorizontalAlign="Left" />
                                                <RowStyle BackColor="White" ForeColor="#003399" Font-Size="9px" />
                                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                <SortedDescendingHeaderStyle BackColor="#002876" />
                                            </asp:GridView>


                                        </td>
                                    </tr>


                                    <tr>
                                        <td colspan="16" style="height: 10px"></td>
                                    </tr>



                                </table>




                            </asp:Panel>


                        </td>
                    </tr>


                </table>





            </td>
        </tr>
    </table>



    <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnReject"
        BackgroundCssClass="modalBackground" CancelControlID="btnClose">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none">

        <table cellpadding="0px" cellspacing="0px">

            <tr>
                <td></td>
                <td></td>
                <td align="right">
                    <asp:Button ID="btnClose" runat="server" Text="Close" />
                </td>
            </tr>

            <tr>
                <td style="height: 10px" colspan="3"></td>
            </tr>
            <tr>
                <td colspan="3">
                    <table cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td>
                                <fieldset class="boxBody">
                                    <asp:Label ID="Label2" runat="server"
                                        Text="Rejected Remarks" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                                </fieldset>


                            </td>
                        </tr>

                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="height: 1px; background-color: #f1f1f1"></td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>


                        <tr>
                            <td>

                                <asp:TextBox ID="txtRemarks_ByHOD" runat="server" Width="450px" MaxLength="250"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>


                        <tr>
                            <td align="right">

                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />

                            </td>
                        </tr>

                        <tr>
                            <td style="height: 10px"></td>
                        </tr>


                    </table>
                </td>
            </tr>

        </table>





    </asp:Panel>


</asp:Content>

