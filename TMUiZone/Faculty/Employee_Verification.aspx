<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="Employee_Verification.aspx.cs" Inherits="Faculty_Employee_Verification" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        #grdLeavedetailwrap {
            width: 650px;
            height: 100%;
            overflow: scroll;
        }

        #grdViewLeaveStatuswrap {
            width: 650px;
            height: 100%;
            overflow: scroll;
        }

        .selected {
            background-color: coral;
        }


        table {
            border-spacing: 0;
        }
    </style>
    <script type="text/javascript">




        function ShowHideDiv(lnk, chkPassport) {
            var row = lnk.parentNode.parentNode.parentNode;




            if (document.getElementById(chkPassport).checked == false) {
                row.style.backgroundColor = '#eb4934';
                document.getElementById(chkPassport.replace("CHKVerify", "CHKHold")).checked = true;
            }
            else {
                row.style.backgroundColor = '#E3EAEB';
            }


        }
        function ShowHideDiv1(lnk, chkPassport) {
            var row = lnk.parentNode.parentNode;

            if (document.getElementById(chkPassport).checked == false) {

                document.getElementById(chkPassport.replace("CHKHold", "CHKVerify")).checked = true;
                row.style.backgroundColor = '#E3EAEB';

            }
            //if (document.getElementById(chkPassport).checked == true)
            //{
            //    document.getElementById("drpRemark").disabled = true

            //}

        }
    </script>
    <script type="text/javascript">

        function SelectAllCheckboxes(chk, selector) {
            $('#<%=grdEmployee.ClientID%>').find(selector + " input:checkbox").each(function () {
                $(this).prop("checked", $(chk).prop("checked"));
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>

    <asp:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
        PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground" CancelControlID="btnHide">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none" Width="500px" Height="500px" ScrollBars="Vertical">
        <div class="header">
            <b>
                <asp:Label ID="lblNotification" runat="server"></asp:Label>

            </b>
            <div class="close">
                <asp:Button ID="btnHide" runat="server" Text="X" />
            </div>
        </div>
        <div class="body">
            <div style="width: 100%">

                <table cellpadding="0px" cellspacing="0px" style="width: 100%">
                    <tr>
                        <td style="height: 13px"></td>
                    </tr>
                    <tr>
                        <td>&nbsp;&nbsp;&nbsp; &nbsp; 
                            <asp:Label ID="Label3" runat="server"
                                Text="View Attendance" Font-Size="15pt" ForeColor="#093A62"
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
                        <td align="center">
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
                                        <asp:DropDownList ID="ddlYear" runat="server" Height="29px"></asp:DropDownList>
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:Button ID="btnGet" runat="server" Text="Get" OnClick="btnGet_Click" />
                                    </td>
                                    <td style="width: 10px"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td style="height: 13px"></td>
                    </tr>

                    <tr>
                        <td>

                            <table cellpadding="0px" cellspacing="0px" style="width: 100%">
                                <tr>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:GridView ID="grd_ViewAttendance" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnRowDataBound="grd_ViewAttendance_RowDataBound" CssClass="table table-striped table-bordered table-hover">
                                            <Columns>
                                                <asp:BoundField DataField="Attendance Date" HeaderText="Date"></asp:BoundField>
                                                <asp:BoundField DataField="Week Day" HeaderText="Week Day" />
                                                <asp:BoundField DataField="ShiftTime" HeaderText="Shift Time" />
                                                <asp:BoundField DataField="Time From" HeaderText="In Time" />
                                                <asp:BoundField DataField="Time To" HeaderText="Out Time" />
                                                <asp:BoundField DataField="WorkingHour" HeaderText="Working Hour" />
                                                <asp:BoundField DataField="LateBy" HeaderText="Late BY" />

                                                <%--  <asp:TemplateField HeaderText="Late BY">
                                <ItemTemplate>
                                    <asp:Label ID="lblLateBY_GRID" runat="server" Text='<%#Bind("LateBy") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Early BY">
                                <ItemTemplate>
                                    <asp:Label ID="lblLateEarlyBy" runat="server" Text='<%#Bind("EarlyBy") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                                                <asp:BoundField DataField="EarlyBy" HeaderText="Early BY" />
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="shiftTimeIn" HeaderText="ShiftInTime" Visible="false" />
                                                <asp:BoundField DataField="ShiftTimeOut" HeaderText="ShiftOutTime" Visible="false" />
                                            </Columns>
                                            <EmptyDataTemplate>
                                                There is no record found
                                            </EmptyDataTemplate>
                                            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                            <HeaderStyle HorizontalAlign="Center" Height="20px" BackColor="#ff9900" ForeColor="White" />
                                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                            <RowStyle BackColor="White" ForeColor="#330099" HorizontalAlign="Left" />
                                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                            <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                            <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                            <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                            <SortedDescendingHeaderStyle BackColor="#7E0000" />

                                        </asp:GridView>
                                    </td>
                                    <td style="width: 10px"></td>
                                </tr>
                            </table>


                        </td>
                    </tr>

                    <tr>
                        <td style="height: 90px"></td>
                    </tr>
                </table>


            </div>



        </div>
    </asp:Panel>





    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div style="width: 100%; height: 600px; overflow: scroll">




        <table cellpadding="0px" cellspacing="0px" width="99%">

            <tr>

                <td>

                    <asp:Label ID="Label1" runat="server"
                        Text="Employee Verification" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                </td>
                <td style="text-align: right">Month :
               
                
                    <asp:DropDownList ID="DropDownList1" runat="server" Height="29px">
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
                    </asp:DropDownList>&nbsp&nbsp&nbsp&nbsp&nbsp
                    Year :
               
             
                    <asp:DropDownList ID="ddlYear1" runat="server" Height="29px"></asp:DropDownList>
                </td>

                <td>
                    <asp:Button ID="btnReport" runat="server" Text="Export to Excel" OnClick="btnReport_Click" />
                </td>
                <td style="width: 10px"></td>


            </tr>


            <tr>


                <td style="text-align: center">Designation :
               
                
                    <asp:DropDownList ID="drpDesignation" runat="server" Height="29px" Width="100px">
                    </asp:DropDownList>&nbsp&nbsp&nbsp&nbsp&nbsp
                    Department :
               
             
                    <asp:DropDownList ID="drpDepartment" runat="server" Height="29px" Width="100px"></asp:DropDownList>
                </td>

                <td>
                    <asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="btnFilter_Click" />
                </td>
                <td></td>
                <td style="width: 10px"></td>


            </tr>


            <tr>

                <td style="width: 100%; padding-left: 20px" colspan="11">


                    <asp:GridView ID="grdEmployee" runat="server" CellPadding="3" DataKeyNames="Remark"
                        CellSpacing="3" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" Width="1000px" OnRowDataBound="grdEmployee_RowDataBound">

                        <Columns>

                            <asp:TemplateField HeaderText="SNo">
                                <ItemTemplate>
                                    <span>
                                        <%#Container.DataItemIndex + 1%>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Emp Name">
                                <ItemTemplate>
                                    <asp:Label ID="EmpName" runat="server" Text='<%#Bind("[First Name]") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Emp Code" ControlStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label ID="EmpCode" runat="server" Text='<%#Bind("[No_]") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Designation" ControlStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label ID="EmpDesignation" runat="server" Text='<%#Bind("Designation") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dept." ControlStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label ID="Dept" runat="server" Text='<%#Bind("[Dept]") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reporting" ControlStyle-Width="80px">
                                <ItemTemplate>
                                    <asp:Label ID="Reporting" runat="server" Text='<%#Bind("[Reporting]") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Deputed" ControlStyle-Width="30px">
                                <ItemTemplate>
                                    <asp:Label ID="Deputed" runat="server" Text="NO" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Deputed/Reported College/Dept">
                                <ItemTemplate>
                                    <asp:Label ID="Deputed_R_D" runat="server" Text='<%#Bind("[Dept]") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Deputation/Reporting Authority">
                                <ItemTemplate>
                                    <asp:Label ID="RAuthority" runat="server" Text='<%#Bind("[Reporting]") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Attendance">
                                <ItemTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="btnView" runat="server" Text="View" CommandArgument='<%# Eval("No_") %>' OnClick="btnView_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PMS Status" ControlStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:Label ID="lblPMSStatus" runat="server" Text='<%#Bind("[PMSStatus]") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Verify" ControlStyle-Width="100px">
                                <HeaderTemplate>

                                    <asp:CheckBox ID="CHKVerifyAll" Checked="true" Text="Verify" runat="server" onclick="SelectAllCheckboxes(this, '.employee')" />

                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CHKVerify" runat="server" onclick="ShowHideDiv(this,this.id)" Checked='<%#Convert.ToBoolean(Eval("[verify]")) %>' CssClass="employee" />
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salary Hold">
                                <ItemTemplate>
                                    <%--Enabled='<%# Eval("[Salary Hold]").ToString().Equals("0") %>'--%>
                                    <asp:CheckBox ID="CHKHold" runat="server" onclick="ShowHideDiv1(this,this.id)"  Checked='<%#Convert.ToBoolean(Eval("[Salary Hold]")) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpRemark" runat="server" Width="120px" Height="36px">
                                        <asp:ListItem Value="1" Text="Do not hold the salary"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Hold the salary"></asp:ListItem>
                                    </asp:DropDownList>

                                </ItemTemplate>
                            </asp:TemplateField>



                        </Columns>
                        <EditRowStyle BackColor="#7C6F57" />
                        <EmptyDataTemplate>
                            There is no Record Found.
                        </EmptyDataTemplate>
                        <FooterStyle BackColor="#ed7600" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="White"
                            HorizontalAlign="Left" CssClass="cssGridheaderfont" Font-Names="Open Sans" />
                        <PagerStyle BackColor="#ed7600" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#E3EAEB" CssClass="cssGridheaderfont" Font-Names="Open Sans" Font-Size="10px" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                    </asp:GridView>

                </td>

            </tr>


            <tr>
                <td style="height: 20px" colspan="10"></td>
            </tr>
            <tr>

                <td style="width: 98%; text-align: right" colspan="10">
                    <asp:Button ID="btnsubmit" runat="server" Width="100px" Text="Submit" OnClick="btnsubmit_Click" CssClass="btnLogin" />
                </td>
            </tr>
        </table>


    </div>

    <asp:GridView ID="GridView1" runat="server" CellPadding="3"
        CellSpacing="3" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" Width="1000px">

        <Columns>

            <asp:TemplateField HeaderText="SrNo">
                <ItemTemplate>
                    <span>
                        <%#Container.DataItemIndex + 1%>
                    </span>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Emp Name">
                <ItemTemplate>
                    <asp:Label ID="EmpName" runat="server" Text='<%#Bind("[Employee Name]") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Emp Code" ControlStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label ID="EmpCode" runat="server" Text='<%#Bind("[EmployeeCode]") %>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Year" ControlStyle-Width="80px">
                <ItemTemplate>
                    <asp:Label ID="Reporting" runat="server" Text='<%#Bind("[Year]") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Month" ControlStyle-Width="80px">
                <ItemTemplate>
                    <asp:Label ID="Deputed" runat="server" Text='<%#Bind("[Month]") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Hold">
                <ItemTemplate>
                    <asp:Label ID="Hold" runat="server" Text='<%#Bind("[Hold]") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Verify">
                <ItemTemplate>
                    <asp:Label ID="Verify" runat="server" Text='<%#Bind("[Verify]") %>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Remarks">
                <ItemTemplate>
                    <asp:Label ID="txtRemark" runat="server" Width="200px" Text='<%#Bind("[Remark]") %>' />
                </ItemTemplate>
            </asp:TemplateField>



        </Columns>
        <EditRowStyle BackColor="#7C6F57" />
        <EmptyDataTemplate>
            There is no Record Found.
        </EmptyDataTemplate>
        <FooterStyle BackColor="#ed7600" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="White"
            HorizontalAlign="Left" CssClass="cssGridheaderfont" Font-Names="Open Sans" />
        <PagerStyle BackColor="#ed7600" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#E3EAEB" CssClass="cssGridheaderfont" Font-Names="Open Sans" Font-Size="10px" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F8FAFA" />
        <SortedAscendingHeaderStyle BackColor="#246B61" />
        <SortedDescendingCellStyle BackColor="#D4DFE1" />
        <SortedDescendingHeaderStyle BackColor="#15524A" />
    </asp:GridView>


</asp:Content>

