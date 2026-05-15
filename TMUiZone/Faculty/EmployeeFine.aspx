<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="EmployeeFine.aspx.cs" Inherits="Faculty_EmployeeFine" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script type="text/javascript" src="dropdowneditable/jquery.min.js"></script>
 <script type="text/javascript" src="dropdowneditable/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("select").searchable();
        });
        function isNumberKey(evt) {


            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Employee Fine" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>

    <table cellpadding="0px" cellspacing="0px">
        <tr>
            <td style="width: 10px"></td>
            <td style="width: 200px" valign="top">

                <table cellpadding="0px" cellspacing="0px" class="leftbg1" style="width: 180px; height: 430px">
                    <tr>
                        <td style="width: 10px"></td>
                        <td>


                            <table cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td class="leftmMenu">&nbsp;<asp:LinkButton ID="lnkApplication" runat="server" OnClick="lnkApplication_Click">Fine Application</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td class="leftmMenu">&nbsp;<asp:LinkButton ID="lnkReport" runat="server" OnClick="lnkReport_Click">Report</asp:LinkButton></td>
                                </tr>
                                <tr>
                                    <td style="height: 10px"></td>
                                </tr>



                                <tr>
                                    <td style="height: 10px">&nbsp;</td>
                                </tr>


                                <tr>
                                    <td style="visibility: hidden">

                                        <asp:Label ID="Label4" runat="server"
                                            Text=" Color Represent in Calender" Font-Size="10pt" ForeColor="#093A62" Font-Names="Open Sans"></asp:Label>

                                    </td>
                                </tr>



                                <tr>
                                    <td style="height: 5px">&nbsp;</td>
                                </tr>







                                <tr>
                                    <td style="height: 2px"></td>
                                </tr>

                                <tr>
                                    <td></td>
                                </tr>




                            </table>

                        </td>
                        <td style="width: 10px"></td>
                    </tr>
                </table>




            </td>
            <td style="width: 30px"></td>
            <td valign="top">


                <asp:Panel ID="pnlLeaveApplication" runat="server" CssClass="leftBackground">

                    <table cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td style="width: 10px"></td>
                            <td>

                                <table cellpadding="0px" cellspacing="0px">
                                    <tr>
                                        <td>
                                            <br />

                                            <table cellpadding="0px" cellspacing="0px">


                                                <table cellpadding="0px" cellspacing="0px">

                                                    <tr>
                                                        <td colspan="15">
                                                            <table cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td>Fine Date :</td>
                                                                    <td style="width: 10px"></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtfrom" runat="server" Width="180px" onkeydown="return false;"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtfrom" Format="dd MMM yyyy">
                                                                        </cc1:CalendarExtender>
                                                                    </td>

                                                                    <td style="width: 10px"></td>
                                                                    <td>Employee : </td>
                                                                    <td style="width: 10px"></td>
                                                                    <td>
                                                                        <asp:DropDownList ID="txtUserid" runat="server" Height="28px" Width="230px"></asp:DropDownList>
                                                                    </td>
                                                                    <td style="width: 10px"></td>


                                                                </tr>
                                                                <tr style="height: 20px">
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Amount :</td>
                                                                    <td style="width: 10px"></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAmount" runat="server" onkeypress="return isNumberKey(event)" Width="180px"></asp:TextBox>

                                                                    </td>

                                                                    <td style="width: 10px"></td>
                                                                    <td>Remarks : </td>
                                                                    <td style="width: 10px"></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtremarks" runat="server" Width="230px" TextMode="MultiLine"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 10px"></td>


                                                                </tr>
                                                                <tr style="height: 20px">
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="7" style="text-align: right">
                                                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                </table>
                                            </table>
                </asp:Panel>
                <asp:Panel ID="pnlReport" runat="server" Visible="false" CssClass="leftBackground">
                    <table cellpadding="0px" cellspacing="0px" style="width: 100%">
                        <tr>
                            <td style="width: 10px"></td>
                            <td>

                                <table cellpadding="0px" cellspacing="0px">
                                    <tr>
                                        <td>


                                            <br />
                                            <asp:Label ID="Label3" runat="server"
                                                Text="View Fine Status" Font-Size="15pt" ForeColor="#093A62"
                                                Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 13px"></td>
                                    </tr>

                                    <tr>
                                        <td class="leftm"></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 13px"></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <table cellpadding="0px" cellspacing="0px" style="width: 866px">
                                                <tr>
                                                    <td>
                                                        <label></label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td></td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        <label></label>
                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td></td>
                                                    <td>
                                                        <table cellpadding="0px" cellspacing="0px">
                                                            <tr>
                                                                <td style="width: 10px"></td>
                                                                <td>
                                                                    <label>Select Status</label>
                                                                </td>
                                                                <td style="width: 10px"></td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddstatus" runat="server" Height="29px">
                                                                        <asp:ListItem>All</asp:ListItem>
                                                                        <asp:ListItem Value="0">Pending</asp:ListItem>
                                                                        <asp:ListItem Value="1">Approved</asp:ListItem>
                                                                        <asp:ListItem Value="2">Rejected</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="right">
                                                         <asp:Button ID="btnExport" runat="server" Text="Export to Excel" CssClass="btnLogin" OnClick="btnExport_Click" />
                                                        <asp:Button ID="btnLeaveViewSearch" runat="server" Text="Search" CssClass="btnLogin" OnClick="btnLeaveViewSearch_Click" />
                                                    </td>
                                                </tr>
                                            </table>

                                        </td>
                                    </tr>


                                    <tr>
                                        <td style="height: 13px"></td>
                                    </tr>

                                    <tr>
                                        <td class="leftm"></td>
                                    </tr>
                                    <tr>
                                        <td>

                                            <asp:GridView ID="grdViewLeaveStatus" runat="server" CellPadding="4" AutoGenerateColumns="false" ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanging="grdViewLeaveStatus_PageIndexChanging" PageSize="30" Width="866px">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr No" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Employee_ID" HeaderText="Employee ID" />
                                                    <asp:BoundField DataField="Employee_name" HeaderText="Employee Name" />
                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                    <asp:BoundField DataField="FineDate" HeaderText="Fine Date" />
                                                    <asp:BoundField DataField="createDate" HeaderText="Create Date" />
                                                    <asp:BoundField DataField="Reason" HeaderText="Remarks" />
                                                    <asp:BoundField DataField="Status" HeaderText="Status" />
                                                </Columns>
                                                <EditRowStyle BackColor="#7C6F57" />
                                                <EmptyDataTemplate>
                                                    There is no Fine detail
                                                </EmptyDataTemplate>
                                                <FooterStyle BackColor="#ed7600" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="White"
                                                    HorizontalAlign="Left" CssClass="cssGridheaderfont" Font-Size="10px" />
                                                <PagerStyle BackColor="#ed7600" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#E3EAEB" CssClass="cssGridheaderfont" Font-Size="9px" />
                                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                                <SortedDescendingHeaderStyle BackColor="#15524A" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 13px"></td>
                                    </tr>

                                    <tr>
                                        <td>

                                            <table cellpadding="0px" cellspacing="0px">
                                                <tr>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                </table>
                            </td>
                            <td style="width: 10px"></td>
                        </tr>
                    </table>



                </asp:Panel>
                <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="leftBackground">
                    <fieldset class="boxBody">
                        <asp:Label ID="Label11" runat="server"
                            Text="You are not Authorized for this page." Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                    </fieldset>
                </asp:Panel>
    </table>

    <%--<table cellpadding="0px" cellspacing="0px" style="width: 100%">
        <tr>
            <td style="height: 13px"></td>
        </tr>

        <tr>

            <td colspan="2">&nbsp;&nbsp;&nbsp; &nbsp; 
                     <fieldset class="boxBody">
                         <asp:Label ID="Label3" runat="server"
                             Text="Employee Fine" Font-Size="15pt" ForeColor="#093A62"
                             Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                     </fieldset>
            </td>

        </tr>


        <tr>
            <td style="height: 13px"></td>
        </tr>


        <tr>
            <td></td>
        </tr>

        <tr>
            <td style="height: 13px"></td>
        </tr>

        <tr>
            <td style="width: 120px"></td>
            <td>
                <table cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td>Fine Date :</td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="txtfrom" runat="server" Width="180px" onkeydown="return false;" AutoPostBack="true" OnTextChanged="txtfrom_TextChanged"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtfrom" Format="dd MMM yyyy">
                            </cc1:CalendarExtender>
                        </td>

                        <td style="width: 10px"></td>
                        <td>Employee : </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:DropDownList ID="txtUserid" runat="server" Height="28px"></asp:DropDownList>
                        </td>
                        <td style="width: 10px"></td>


                    </tr>
                    <tr style="height: 20px">
                        <td></td>
                    </tr>
                    <tr>
                        <td>Amount :</td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="txtAmount" runat="server" onkeypress="return isNumberKey(event)" Width="180px"></asp:TextBox>

                        </td>

                        <td style="width: 10px"></td>
                        <td>Remarks : </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="txtremarks" runat="server" Width="230px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td style="width: 10px"></td>


                    </tr>
                </table>
            </td>
        </tr>



    </table>--%>
</asp:Content>

