<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="TMVF_Approval.aspx.cs" EnableEventValidation="false" Inherits="Faculty_TMVF_Approval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<script runat="server">



</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function HidePopup10() {

            $('#confirmModal10').modal('hide');
        }
        function VisiblePopup10() {
            $('#confirmModal10').modal('show');


        }

    </script>
    <script type="text/javascript">

        function SelectAllCheckboxes(chk, selector) {
            $('#<%=grdDetail.ClientID%>').find(selector + " input:checkbox").each(function () {
                $(this).prop("checked", $(chk).prop("checked"));
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset>

        <br />
        <div>
            <table class="boxBody" width="1200px">
                <tr>
                    <td style="width: 20px"></td>
                    <td style="width: 150px; font-size: large; font: bold">&nbsp;&nbsp;Status
                    </td>
                    <td style="width: 5px"></td>
                    <td class="auto-style1">
                        <asp:DropDownList ID="drpList" runat="server" Width="100px" CssClass="form-control" >
                            <asp:ListItem Text="Pending" Value="0"></asp:ListItem>
                             <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                             <asp:ListItem Text="Reject" Value="2"></asp:ListItem>
                        </asp:DropDownList>

                    </td>
                    <td>Month</td>
<td style="width: 10px"></td>
<td>
    <asp:DropDownList ID="ddlMonth" runat="server" Height="29px" Width="100px" CssClass="form-control">
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
     <asp:DropDownList ID="ddlYear" runat="server" Height="29px" Width="100px" CssClass="form-control"></asp:DropDownList>
 </td>
                    <td style="padding-left:300px">
                        <asp:Button ID="Search" runat="server" CssClass="btn-sm btn-primary btn-block"  Height="30px" Width="90px" Text="Show Report" OnClick="Search_Click" />
                    </td>
                     <td>
                        <asp:Button ID="btnGenerate" runat="server" CssClass="btn-sm btn-primary btn-block"  Height="30px" Width="120px" Visible="false" Text="Generate Report" OnClick="btnGenerate_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnApprove" runat="server" CssClass="btn-sm btn-primary btn-block"  Height="30px" Width="90px" Text="Approve" OnClick="btnApprove_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnReject" runat="server" CssClass="btn-sm btn-primary btn-block"  Height="30px" Width="90px" Text="Reject" OnClick="btnReject_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    <br />

    <asp:Panel ID="pnlMainTable" runat="server">
    <asp:GridView ID="grdtmvfpdata" runat="server" DataKeyNames="No_,ShiftTime" PageSize="50"
        AllowPaging="true" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" Visible="true">
        <PagerSettings Mode="NumericFirstLast" />
        <PagerStyle CssClass="csspager" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <%# Container.DataItemIndex +1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Employee Code" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:Label ID="lblemployeecode" runat="server" Text='<%# Bind("Employee_Code") %>'></asp:Label>

                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="View" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkbutton" runat="server" CommandArgument='<%# Eval("Employee_Code") %>' OnClick="lnkbutton_Click">View</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Amount View" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" Visible="false" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkAmountView" runat="server" CommandArgument='<%# Eval("Employee_Code") %>' OnClick="lnkAmountView_Click">View</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Employee Name" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                <ItemTemplate>
                    <asp:Label ID="lblemployeename" runat="server" Text='<%# Eval("Employee_Name") %>' Style="text-transform: uppercase;"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Shift Time" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                <ItemTemplate>
                    <asp:Label ID="lblShift_Time" runat="server" Text='<%# Eval("Shift_Time") %>' Style="text-transform: uppercase;"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Month" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                <ItemTemplate>
                    <asp:Label ID="lblMonth" runat="server" Text='<%# Eval("Month") %>' Style="text-transform: uppercase;"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="YEAR" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                <ItemTemplate>
                    <asp:Label ID="lblYear" runat="server" Text='<%# Eval("Year") %>' Style="text-transform: uppercase;"></asp:Label>

                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Select" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelect" runat="server" Enabled='<%# Eval("Status").ToString().Equals("0") %>'  ></asp:CheckBox>

                </ItemTemplate>
            </asp:TemplateField>


        </Columns>
    </asp:GridView>
    </asp:Panel>
     <asp:Panel ID="Panel1" runat="server" Visible="false" >

          <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" HyperlinkTarget="_blank"></rsweb:ReportViewer>
         </asp:Panel>




    <div id="confirmModal10" class="modal fade confirm-modal" role="dialog">

        <div class="modal-dialog modalPopup" style="width: 1050px; height: 550px;overflow:scroll">
           
            <div style="text-align: right; padding-bottom: -40px">
                Employee Name : <asp:Label ID="lblName" runat="server"></asp:Label> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                <asp:Button ID="btnExport" runat="server" Text="Export to excel" OnClick="btnExport_Click" Font-Size="Larger" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update Lecture" OnClick="btnUpdate_Click" Font-Size="Larger" />
                <asp:Button ID="Button2" runat="server" Text="X" OnClientClick="HidePopup10(); return false;" Font-Size="Larger" />
                 <asp:HiddenField ID="hfEmployee"  runat="server" />
            </div>
            <div class="clearfix" runat="server" style="margin-bottom: 10px; margin-top: 10px; margin-left: 20px;overflow-y: scroll" >
                <asp:UpdatePanel ID="pnlUpdate" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grdDetail" runat="server"  
                            AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" Visible="true">
                            <PagerSettings Mode="NumericFirstLast" />
                            <PagerStyle CssClass="csspager" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex +1 %>
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Program" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblemployeecode" runat="server" Text='<%# Bind("Program") %>'></asp:Label>
                                        <asp:HiddenField ID="hfID" runat="server" value='<%# Bind("ID") %>'></asp:HiddenField>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Semester" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSemester" runat="server" Text='<%# Bind("Semester") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Course" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" Visible="false" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("Course") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Name of Toppic" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center"  ItemStyle-CssClass="text-center">
                                     <ItemTemplate>
                                         <asp:Label ID="lbltopioc" runat="server" Text='<%# Eval("Name of topic") %>' ></asp:Label>
                                    </ItemTemplate>
                                 </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("Unit") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Date" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                    <ItemTemplate>
                                        <asp:Label ID="Date" runat="server" Text='<%# Eval("Lecture_Date") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Time" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTime" runat="server" Text='<%# Eval("Time") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Strength" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("TotalStrength") %>' Style="text-transform: uppercase;"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Present Strength" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPStrength" runat="server" Text='<%# Eval("StudentStrength") %>' Style="text-transform: uppercase;"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Verify" ItemStyle-Width="4%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                     <HeaderTemplate>

                                    <asp:CheckBox ID="CHKVerifyAll"  Text="Verify" runat="server" onclick="SelectAllCheckboxes(this, '.employee')" />

                                </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkVerify" runat="server"  Checked='<%#Convert.ToBoolean(Eval("[CHKVerify]")) %>' CssClass="employee" ></asp:CheckBox>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                

                            </Columns>
                        </asp:GridView>



                        
                    </ContentTemplate>
                  

                </asp:UpdatePanel>
                
            </div>
        </div>
    </div>


</asp:Content>

