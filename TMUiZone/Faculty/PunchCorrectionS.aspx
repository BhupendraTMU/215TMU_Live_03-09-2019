<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="PunchCorrectionS.aspx.cs" Inherits="Faculty_PunchCorrectionS" %>

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

        .auto-style3 {
            width: 185px;
        }

        .auto-style4 {
            width: 194px;
        }

        .auto-style5 {
            width: 46px;
        }
    </style>

    <script type="text/javascript">
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

    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="PUNCH CORRECTION" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <table cellpadding="0px" cellspacing="0px">
        <tr>
            <td style="width: 30px"></td>
            <td valign="top" style="width: 120px">



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
                        <td class="leftmMenu">&nbsp;<asp:LinkButton ID="lnkEmployeeReport" runat="server" OnClick="lnkEmployeeReport_Click">Employee Report</asp:LinkButton></td>
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
                        <td style="width: 100px">
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


                            <asp:Panel ID="pnlCOApplication" runat="server" Visible="true">
                                <table cellpadding="0px" cellspacing="0px">

                                    <tr>
                                        <td colspan="16" align="right">

                                            <table cellpadding="0px" cellspacing="0px">

                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label6" runat="server" Text=" Note : Approval Authority : 1 ) " ForeColor="Green" Font-Size="12pt"></asp:Label>
                                                        <asp:Label ID="lblfirstApproval" runat="server" ForeColor="#FF3300" Font-Size="10pt"></asp:Label>
                                                        .&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 2) </td>
                                                    <td>
                                                        <asp:Label ID="lblSecondApproval" runat="server" ForeColor="#FF3300" Font-Size="10pt"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblApprovalAuthority1" runat="server" Text="Approval authority not tag . Please contact HR admin ,otherwise CO can't apply." Visible="False" Font-Bold="True" ForeColor="Red" Font-Size="11pt"></asp:Label></td>
                                                </tr>

                                            </table>

                                        </td>
                                    </tr>



                                    <tr>
                                        <td colspan="16" style="height: 10px"></td>
                                    </tr>

                                    <tr>
                                        <td style="width: 400px">Punch Type
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td colspan="14">

                                            <asp:DropDownList ID="drpPunchType" runat="server" Width="170px" Height="24px" AutoPostBack="true" OnSelectedIndexChanged="drpPunchType_SelectedIndexChanged">
                                                <asp:ListItem Text="Punch Correction" Value="0">

                                                </asp:ListItem>
                                                <asp:ListItem Text="Missed Punch" Value="1"></asp:ListItem>

                                            </asp:DropDownList>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td colspan="16" style="height: 10px"></td>
                                    </tr>

                                    <tr>
                                        <td style="width: 400px">Date of Punch
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:TextBox ID="txtFromDate" runat="server" onkeydown="return false;" autocomplete="off"
                                                oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" AutoPostBack="True" OnTextChanged="txtFromDate_TextChanged"></asp:TextBox>
                                            <asp:CalendarExtender ID="clndAppliedate" runat="server" TargetControlID="txtFromDate" Format="dd MMM yyyy"></asp:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromDate" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>


                                        </td>
                                        <td style="width: 0px"></td>
                                        <td>Actual Time(From)</td>
                                        <td style="width: 0px"></td>
                                        <td>
                                            <asp:TextBox ID="txtFromTime" runat="server" Enabled="false" AutoPostBack="True" Height="23px" Width="100px" onchange="validateHhMm(this);" OnTextChanged="txtFromTime_TextChanged"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtFromTime" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 0px"></td>
                                        <td>To Time</td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:TextBox ID="txtToTime" runat="server" Enabled="false" AutoPostBack="True" Height="23px" Width="100px" onchange="validateHhMm(this);" OnTextChanged="txtToTime_TextChanged"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtToTime" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td></td>
                                        <td style="width: 10px"></td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                    </tr>


                                    <tr>
                                        <td colspan="16" style="height: 10px"></td>
                                    </tr>

                                    <tr>
                                        <td style="width: 400px">Allowed Time(Correct)
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:TextBox ID="txtFromTime1" runat="server" AutoPostBack="True" onchange="validateHhMm(this);" OnTextChanged="txtFromTime_TextChanged"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromTime1" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>

                                        </td>
                                        <td style="width: 0px"></td>
                                        <td>To Time </td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:TextBox ID="txtToTime1" runat="server" AutoPostBack="True" Width="100px" onchange="validateHhMm(this);" OnTextChanged="txtToTime_TextChanged"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToTime1" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>


                                        </td>
                                        <td style="width: 10px"></td>
                                        <td>Count </td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:Label ID="lblCount" Text="0" runat="server"></asp:Label>

                                        </td>
                                    </tr>


                                    <tr>
                                        <td colspan="16" style="height: 10px"></td>
                                    </tr>

                                    <tr>
                                        <td style="width: 400px">Remarks /Reason</td>
                                        <td style="width: 10px"></td>
                                        <td colspan="13">
                                            <asp:TextBox ID="txtRemarks" runat="server" Width="800px"></asp:TextBox>
                                            <asp:HiddenField ID="hfMachineID" runat="server" />
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="16" style="height: 10px"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="16" align="right">

                                            <table cellpadding="0px" cellspacing="0px">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblCOSuccess" runat="server" Text="Application has been send successfully for Approval" Font-Bold="True" Font-Size="12pt" ForeColor="Red" Visible="false"></asp:Label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:Button ID="btnSendForApproval" runat="server" Text="Send For Approval" OnClick="btnSendForApproval_Click" ValidationGroup="odapps" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="16" style="height: 10px"></td>
                                    </tr>
                                    <asp:HiddenField ID="hfShiftFrom" runat="server" />
                                    <asp:HiddenField ID="hfShiftTo" runat="server" />
                                    <asp:HiddenField ID="hfShiftTo1" runat="server" />
                                    <asp:HiddenField ID="hfTotBUtilize" runat="server" />
                                    <asp:HiddenField ID="hfnight" runat="server" />
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
                                        <td style="width: 10px"></td>
                                        <td>Status </td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:DropDownList ID="ddStatus_ViewStatus" runat="server" Height="30px">
                                                <asp:ListItem>All</asp:ListItem>
                                                <asp:ListItem>Pending</asp:ListItem>
                                                <asp:ListItem>Approved</asp:ListItem>
                                                <asp:ListItem>Rejected</asp:ListItem>

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

                                            <asp:GridView ID="grdView_Status" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="1000px" AllowPaging="True" OnPageIndexChanging="grdView_Status_PageIndexChanging">
                                                <Columns>
                                                    <asp:BoundField DataField="Atte_Date" HeaderText="Date" DataFormatString="{0:D}" />
                                                    <asp:BoundField DataField="fromTime" HeaderText="From Time" />

                                                    <asp:BoundField DataField="ToTime" HeaderText="Till Time" />
                                                    <asp:BoundField DataField="CfromTime" HeaderText="Correct From Time" />
                                                    <asp:BoundField DataField="CToTime" HeaderText="Correct To Time" />

                                                    <asp:BoundField DataField="Purpose" HeaderText="Purpose">
                                                        <ItemStyle Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                        <ItemStyle Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ApprovalStatus" HeaderText="Status" />
                                                    <asp:TemplateField HeaderText="HR Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# (Eval("Approved by").ToString() == "" ? "Pending" : Eval("ApprovalStatus").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:BoundField DataField="ApprovalStatus" HeaderText="Status" />--%>
                                                    <asp:BoundField DataField="Approved by" HeaderText="Approval By" />
                                                    <asp:BoundField DataField="RejectedByHODRemarks" HeaderText="Rejected Remarks">
                                                        <ItemStyle Width="100px" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    There is no record found
                                                </EmptyDataTemplate>
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


                            <asp:Panel ID="pnlEmployeeReport" runat="server" Visible="false">
                                <table cellpadding="0px" cellspacing="0px" style="width: 120%">


                                    <tr>
                                        <td style="width: 100px">From Date</td>

                                        <td class="auto-style4">
                                            <asp:TextBox ID="TextBox1" runat="server" onkeydown="return false;"
                                                oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox1" Format="dd MMM yyyy"></asp:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox1" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="ODfilterOwn"></asp:RequiredFieldValidator>
                                        </td>

                                        <td style="width: 100px">To Date </td>

                                        <td class="auto-style3">
                                            <asp:TextBox ID="TextBox2" runat="server" onkeydown="return false;"
                                                oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBox2" Format="dd MMM yyyy"></asp:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox2" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="ODfilterOwn"></asp:RequiredFieldValidator>
                                        </td>

                                        <td>Status </td>
                                        <td style="width: 10px"></td>
                                        <td class="auto-style5">
                                            <asp:DropDownList ID="DropDownList1" runat="server" Height="30px">
                                                <asp:ListItem>All</asp:ListItem>
                                                <asp:ListItem>Pending</asp:ListItem>
                                                <asp:ListItem>Approved</asp:ListItem>
                                                <asp:ListItem>Rejected</asp:ListItem>

                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td class="auto-style5">
                                            <asp:Button ID="Button2" runat="server" Text="Get" ValidationGroup="ODfilterOwn" OnClick="Button2_Click" /></td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:Button ID="Button3" runat="server" Text="Export To Excel" OnClick="Button3_Click" />
                                        </td>
                                        <td></td>
                                    </tr>

                                    <tr>
                                        <td style="height: 10px" colspan="17"></td>
                                    </tr>

                                    <tr>
                                        <td colspan="17">

                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="1000px" AllowPaging="True" OnPageIndexChanging="grdView_Status_PageIndexChanging">
                                                <Columns>
                                                    <asp:BoundField DataField="Userid" HeaderText="Employee ID" DataFormatString="{0:D}" />
                                                    <asp:BoundField DataField="UName" HeaderText="Employee Name" />
                                                    <asp:BoundField DataField="Atte_Date" HeaderText="Date" DataFormatString="{0:D}" />
                                                    <asp:BoundField DataField="fromTime" HeaderText="From Time" />

                                                    <asp:BoundField DataField="ToTime" HeaderText="Till Time" />
                                                    <asp:BoundField DataField="CfromTime" HeaderText="Correct From Time" />
                                                    <asp:BoundField DataField="CToTime" HeaderText="Correct To Time" />

                                                    <asp:BoundField DataField="Purpose" HeaderText="Purpose">
                                                        <ItemStyle Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                        <ItemStyle Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ApprovalStatus" HeaderText="Status" />
                                                    <asp:BoundField DataField="Approved by" HeaderText="Approval By" />
                                                    <asp:BoundField DataField="RejectedByHODRemarks" HeaderText="Rejected Remarks">
                                                        <ItemStyle Width="100px" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    There is no record found
                                                </EmptyDataTemplate>
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


                            <asp:Panel ID="pnlApproval" runat="server" Visible="false" style="width:1000px">
                                <table cellpadding="0px" cellspacing="0px">


                                    <tr>
                                       
                                                    <td>Month</td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlMonth" runat="server" Height="29px">
                                                            <asp:ListItem Value="01">January</asp:ListItem>
                                                            <asp:ListItem Value="02">February</asp:ListItem>
                                                            <asp:ListItem Value="03">March</asp:ListItem>
                                                            <asp:ListItem Value="04">April</asp:ListItem>
                                                            <asp:ListItem Value="05">May</asp:ListItem>
                                                            <asp:ListItem Value="06">June</asp:ListItem>
                                                            <asp:ListItem Value="07">July</asp:ListItem>
                                                            <asp:ListItem Value="08">August</asp:ListItem>
                                                            <asp:ListItem Value="09">September</asp:ListItem>
                                                            <asp:ListItem Value="10">October</asp:ListItem>
                                                            <asp:ListItem Value="11">November</asp:ListItem>
                                                            <asp:ListItem Value="12">December</asp:ListItem>
                                                        </asp:DropDownList></td>
                                                    <td style="width: 10px"></td>
                                                    <td>Year </td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlYear1" runat="server" Height="29px"></asp:DropDownList>
                                                    </td>

                                                    <td style="width: 10px"></td>
                                               
                                      

                                        <%-- <td style="width: 10px"></td>
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
                                            </asp:Panel>--%>

                                        <%-- <asp:Panel ID="pnlFilterDate" runat="server" Visible="false">
                                                <table cellpadding="0px" cellspacing="0px">
                                                    <tr>
                                                        <td>From Date </td>
                                                        <td style="width: 10px"></td>
                                                        <td>
                                                            <asp:TextBox ID="txtFromDate_Approval" runat="server" Width="80px" onkeydown="return false;"
                                                                oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtFromDate_Approval" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                                        </td>
                                                        <td>To Date</td>
                                                        <td style="width: 10px"></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTodate_Approval" runat="server" Width="80px" onkeydown="return false;"
                                                                oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtTodate_Approval" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>--%>



                                        <td style="width: 10px"></td>
                                        <td>Status </td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:DropDownList ID="ddStatus_Approval" runat="server" Height="30px" Enabled="False">

                                                <asp:ListItem>Pending</asp:ListItem>
                                                <asp:ListItem>Approved</asp:ListItem>
                                                <asp:ListItem>Rejected</asp:ListItem>
                                                <asp:ListItem>Cancelled</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:Button ID="btnGet" runat="server" Text="Get" OnClick="btnGet_Click" />
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            <asp:Button ID="btnFIlterGet_Approval" runat="server" Text="Get" OnClick="btnFIlterGet_Approval_Click" />&nbsp;&nbsp;
                                            <asp:Button ID="btnApproveExport" runat="server" Text="Export Excel" OnClick="btnApproveExport_Click" />
                                        </td>
                                    </tr>



                                    <tr>
                                        <td colspan="17" style="height: 10px"></td>
                                    </tr>

                                    <tr>
                                        <td colspan="17">
                                            <table cellpadding="0px" cellspacing="0px">
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnApprove" runat="server" Text="Approve" Visible="False" OnClick="btnApprove_Click" OnClientClick="this.disabled = true; this.value = 'Please wait...';"
                                                            UseSubmitBehavior="false" />
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
                                        <td colspan="17" style="height: 10px"></td>
                                    </tr>
                                </table>
                                <table id="grdis">
                                    <tr>
                                        <td style="width:100px">

                                            <asp:GridView ID="grdApproval" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnPageIndexChanging="grdApproval_PageIndexChanging" PageSize="1" OnRowDataBound="grdApproval_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkMark" runat="server" />


                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblApprovalStatus_Status" runat="server" Text='<%#Bind("ApprovalStatus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Employee ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmployeeid_gridco" runat="server" Text='<%#Bind("Userid") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="Uname" HeaderText="Name" />

                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAtte_Date_GridCo" runat="server" Text='<%#Bind("Atte_Date","{0:dd MMM yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="fromTime" HeaderText="From Time" />

                                                    <asp:BoundField DataField="ToTime" HeaderText="To Time" />
                                                    <asp:BoundField DataField="CFromTime" HeaderText="Modify Time(From)">
                                                        <ItemStyle Width="120px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CToTime" HeaderText="To Time">
                                                        <ItemStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="MinuteDiff" HeaderText="Corrected Time(Minutes)">
                                                        <ItemStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                        <ItemStyle Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="HOD Remark">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtHODRemark" runat="server" Text='<%#Bind("HODRemark") %>'></asp:TextBox>
                                                            <%--Enabled='<%# Eval("HODRemark").ToString()=="" ? true : false %>'--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remark">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtHRRemark" runat="server"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="RejectedByHODRemarks" HeaderText="Rejected Remarks">

                                                        <ItemStyle Width="100px" />
                                                    </asp:BoundField>

                                                    <asp:TemplateField HeaderText="id" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblid" runat="server" Text='<%#Bind("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:BoundField DataField="Approved by" HeaderText="Approval By" />
                                                    <asp:TemplateField HeaderText="HOD Name" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHOD" runat="server" Text='<%#Bind("HODName") %>'></asp:Label>
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
                    <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />
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

                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="this.disabled = true; this.value = 'Please wait...';"
                                    UseSubmitBehavior="false" />

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



    <asp:Button ID="btnCancel_md" runat="server" Text="Button" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel2" TargetControlID="btnCancel_md"
        BackgroundCssClass="modalBackground" CancelControlID="btnClose">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" align="center" Style="display: none">

        <table cellpadding="0px" cellspacing="0px">

            <tr>
                <td></td>
                <td></td>
                <td align="right">
                    <asp:Button ID="Button1" runat="server" Text="Close" />
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
                                    <asp:Label ID="Label3" runat="server"
                                        Text="Cancel Remarks" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

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

                                <asp:TextBox ID="txtCancel_Remarks" runat="server" Width="450px" MaxLength="250"></asp:TextBox>



                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="Required" ControlToValidate="txtCancel_Remarks" SetFocusOnError="True" ValidationGroup="canceldCO"></asp:RequiredFieldValidator></td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="lblCancelmessage" runat="server" Text="" Font-Size="13px" ForeColor="Green" Visible="false"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td style="height: 10px"></td>
                        </tr>


                        <tr>
                            <td align="right"></td>
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

