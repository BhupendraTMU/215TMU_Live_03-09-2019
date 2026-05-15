<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="COReport.aspx.cs" Inherits="Faculty_CLaimReport" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="CO. Approved (CO Claim Report)" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>


    <table cellpadding="0px" cellspacing="0px" style="width:100%"> <tr> <td style="width:10px">  </td> <td align="center">
         <table cellpadding="0px" cellspacing="0px"> <tr> <td>  </td></tr> 

        <tr> <td style="height:10px">  </td></tr>
        <tr> <td align="center">

             <asp:Panel ID="pnlApproval" runat="server">
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
                                            <asp:RadioButton ID="rdDatewise" runat="server" Text="Date" GroupName="odAppr" AutoPostBack="True" OnCheckedChanged="rdDatewise_CheckedChanged" Checked="True" />
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

                                            <asp:Panel ID="pnlFilterDate" runat="server">
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
                                            </asp:Panel>


                                        </td>
                                        <td style="width: 10px"></td>
                                        <td> </td>
                                        <td style="width: 10px"></td>
                                        <td>
                                            &nbsp;</td>
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
                                                        &nbsp;</td>
                                                    <td style="width: 10px"></td>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td colspan="16" style="height: 10px"></td>
                                    </tr>

                                    <tr>
                                        <td colspan="16">

                                            <asp:GridView ID="grdApproval" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnPageIndexChanging="grdApproval_PageIndexChanging" PageSize="50" Width="100%" AllowPaging="True">
                                                <Columns>

                                                        <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
 <asp:Label ID="lblApprovalStatus_Status" runat="server" Text='<%#Bind("ApprovalStatus") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

<%--                                                    <asp:BoundField DataField="ApprovalStatus" HeaderText="Status" />--%>
                                                    <%--<asp:BoundField DataField="Userid" HeaderText="Employee ID" />--%>

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


<%--                                                    <asp:BoundField DataField="Atte_Date" HeaderText="Date" DataFormatString="{0:D}" />--%>
                                                    <asp:BoundField DataField="fromTime" HeaderText="From Time" />

                                                    <asp:BoundField DataField="ToTime" HeaderText="To Time" />
                                                    <asp:BoundField DataField="Job_location" HeaderText="Destination">
                                                       <%-- <ItemStyle Width="100px" />--%>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Purpose" HeaderText="Purpose">
                                                      <%--  <ItemStyle Width="100px" />--%>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                       <%-- <ItemStyle Width="100px" />--%>
                                                    </asp:BoundField>

                                                    <asp:TemplateField HeaderText="id" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblid" runat="server" Text='<%#Bind("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:BoundField DataField="Approved by" HeaderText="Approval By" />
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

             </td></tr>



         <tr> <td style="height:90px">  </td></tr>
    </table> </td> <td style="width:10px"> </td></tr>

       
    </table>


</asp:Content>

