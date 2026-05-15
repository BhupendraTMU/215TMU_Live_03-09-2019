<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="FellowshipFormApproval.aspx.cs" Inherits="Faculty_FellowshipFormApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 435px;
        }
    </style>

     <script type="text/javascript">



          function PrintDiv() {

              var divToPrint = document.getElementById('printarea');

              var popupWin = window.open('', '_blank', 'width=300px,height=400px,location=no,left=200px, margin:0mm');
              popupWin.document.open();
              popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
              popupWin.document.close();
          }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
      <fieldset>
        <%--<div class="text-right"  style="padding-left:-100px">
            <asp:Button ID="BtnSubmit" runat="server" Text="Approved" OnClick="BtnSubmit_Click" ForeColor="White" CssClass="btn"  BackColor="#ff9900" />
            <asp:Button ID="BtnRejected" runat="server" Text="Rejected" OnClick="BtnRejected_Click" ForeColor="White" CssClass="btn"  BackColor="#ff9900"/>
            <asp:Button ID="Btnexporttoexel" runat="server" Text="Export To Excel" ForeColor="White" OnClick="Btnexporttoexel_Click" CssClass="btn" BackColor="#ff9900"/>
        </div>--%>
          <br />
          <div>
              <table class="boxBody" width="1200px">
                                      <tr>
                                          <td style="width: 20px"></td>
                                        <td style="width:150px; font-size:large; font:bold"> &nbsp;&nbsp;Employee Code
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td class="auto-style1">
                                            <asp:TextBox ID="txtEmployeeCode" runat="server" Width="200px" PlaceHolder="EMPLOYEE CODE"  BorderColor="Black" autocomplete="off"
                                                oncopy="return true" onpaste="return true" oncut="return true" oncontextmenu="return false"></asp:TextBox>                                           
                                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>--%>
                                        </td>
        <td>
            <asp:Button ID="Search" runat="server" CssClass="btn-sm btn-primary btn-block" OnClick="Search_Click"  ValidationGroup="g1" Height="30px" Width="90px" Text="Show Report" />
        </td>
           </tr>
         </table>
          </div>
    </fieldset>
    <br />

    <asp:GridView ID="grdfellowshipdata" runat="server" DataKeyNames="Employee_Code,ApprovalStatus"  OnPageIndexChanging="grdfellowshipdata_PageIndexChanging"    PageSize="50"
            AllowPaging="true" OnRowDataBound="grdfellowshipdata_RowDataBound" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" Visible="true" >
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
                        <asp:HiddenField ID="Hfemployeecode" Value='<%# Eval("Employee_Code") %>' runat="server" />
                        <asp:HiddenField ID="Hfhodname" Value='<%# Eval("Employee_Code") %>' runat="server" />
                         <asp:HiddenField ID="HfEmployeeStatus" Value='<%# Eval("ApprovalStatus") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="View" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center"  ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbutton" runat="server" CommandArgument='<%# Eval("Employee_Code") %>' OnClick="lnkbutton_Click" >View</asp:LinkButton>
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
                        <asp:HiddenField ID="hfdStatusApproval" runat="server" value='<%#Bind("ApprovalStatus") %>'></asp:HiddenField>
                    </ItemTemplate>
                </asp:TemplateField>
                 <%--<asp:TemplateField ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" HeaderText="Select" ItemStyle-CssClass="text-center">                   
                    <ItemTemplate>
                        <asp:CheckBox ID="Chkemployee" runat="server" Enabled='<%# Eval("txtMarksEnableDesable").ToString().Equals("true") %>'   AutoPostBack="true"  />
                    </ItemTemplate>
                </asp:TemplateField>--%>

</Columns>
        </asp:GridView>

    
  
     <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>

    <asp:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
        PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground" CancelControlID="btnHide">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none" Width="1200px" Height="900px" ScrollBars="Vertical">
        <div class="header">
            <b>
                <asp:Label ID="lblNotification" runat="server"></asp:Label>

            </b>
            <div class="close">
                <asp:Button ID="btnHide" runat="server" Text="X" />
            </div>
        </div>

        <div>
            <table cellpadding="0px" cellspacing="0px" style="width: 100%">
                <tr>
                    <td style="text-align:right" colspan="3" id="tdApprove" runat="server">
                       <asp:Button ID="btnApprove" Text="Approve" runat="server" OnClick="btnApprove_Click" />
                         <asp:Button ID="btnReject" Text="Reject" runat="server" OnClick="btnReject_Click" />
                        <asp:HiddenField ID="MonthMonthID" runat="server"/>     
                           <asp:HiddenField ID="MonthYearID" runat="server"/>  
                        <asp:HiddenField ID="EmployeeCode" runat="server" />
                               
                    </td>
                </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:GridView ID="grd_ViewAttendance" DataKeyNames="Employee_Code" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" OnRowDataBound="grd_ViewAttendance_RowDataBound" BorderWidth="1px" CellPadding="4" CssClass="table table-striped table-bordered table-hover">
                                <Columns>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDate" runat="server" Text='<%#Bind("[Attendance Date]") %>' Enabled="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Week Day">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWeekDay" runat="server" Text='<%#Bind("[Week Day]") %>' Enabled="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Shift Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblShiftTime" runat="server" Text='<%#Bind("[ShiftTime]") %>' Enabled="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                 
                                    <asp:TemplateField HeaderText="Research Activity">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtResearch" runat="server" Enabled="false" ForeColor="Black" Text='<%#Bind("[Research_Actuvity]") %>' TextMode="MultiLine"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fellowship Activity">
                                        <ItemTemplate>
                                             <asp:TextBox ID="txtFellow" runat="server" Enabled="false" ForeColor="Black" Text='<%#Bind("[FellowShip_Activity]") %>'  TextMode="MultiLine"></asp:TextBox>
                                           
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("Status") %>'></asp:Label>
                                             <asp:HiddenField ID="hfdStatus" runat="server" value='<%#Bind("Status_Approve") %>'></asp:HiddenField>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Approval">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkApprove" runat="server" Text="Approve" OnClick="lnkApprove_Click"   ForeColor="Green" ></asp:LinkButton>&nbsp&nbsp&nbsp
                                            <asp:LinkButton ID="lnkReject" runat="server" Text="Reject" OnClick="lnkReject_Click" ForeColor="Red" ></asp:LinkButton>
                                           <%-- Text='<%#Bind("Status") %>'--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                   
                                </Columns>
                                <EmptyDataTemplate>
                                    There is no record found
                                </EmptyDataTemplate>
                                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                <HeaderStyle HorizontalAlign="Center" Height="20px" BackColor="#ff9900" ForeColor="White" />
                                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                <RowStyle BackColor="White"  HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                <SortedDescendingHeaderStyle BackColor="#7E0000" />
                            </asp:GridView>
                        </td>
                        <td style="width: 10px"></td>
                    </tr>
                <tr>
                    <td style="text-align:center" colspan="3" id="tdApproveFinal" runat="server">
                        <asp:Button ID="btnFinalApproval" runat="server" Text="Send Final Approval"   OnClick="btnFinalApproval_Click" />
                         <br />
                <br />
                    </td>
                </tr>
               
                </table>
        </div>



        </asp:Panel>

      <asp:Panel ID="pnlGridViewDetails" CssClass="modalPopup" Width="65%" runat="server" Style="display: none;" ScrollBars="Vertical" Height="950px">
        <div class="header">
            <br />
            <div class="close">
                <asp:Button ID="btnprint" runat="server" Text="Print" BackColor="Black" ForeColor="White" OnClientClick="PrintDiv();" />
                <asp:Button ID="Button1" runat="server" Text="X" BackColor="Black" ForeColor="White"/>


                <div id="printarea">
                    <fieldset class="boxBodyInner">
                        <div>
                            <table>
                                <tr style="width: 65%">
                                    <td style="width: 1%"></td>
                                    <td style="width: 12%; text-align: left">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/UPDATEDLOGO.jpg" Width="70%" Height="50%" />

                                    </td>
                                    <td style="text-align: center">
                                        <strong>
                                            <asp:Label ID="lblCName" Font-Size="Large" Text="TEERTHANKER MAHAVEER UNIVERSITY" runat="server"></asp:Label>
                                        </strong>
                                        <br />

                                        <strong>
                                            <asp:Label ID="LblType" runat="server" Font-Size="Large" Text="Delhi Road,Moradabad(U.P) India"></asp:Label>
                                        </strong>
                                        <br />
                                        <br />


                                        <strong>
                                            <asp:Label ID="Label1" runat="server" Font-Size="Large" Text="Faculty of Medicine, Department ">
                                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label></asp:Label>
                                        </strong>
                                        <br />

                                        <strong>
                                            <asp:Label ID="Label3" Font-Size="Large" Text="TEERTHANKER MAHAVEER UNIVERSITY" runat="server"></asp:Label>
                                        </strong>
                                        <br />

                                        <strong>
                                            <asp:Label ID="Label4" runat="server" Font-Size="Large" Text="Delhi Road,Moradabad"></asp:Label>
                                        </strong>

                                        <br />

                                    </td>
                                    <td style="width: 10%; text-align: center"></td>
                                </tr>
                            </table>
                        </div>
                    </fieldset>


                    <div style="text-align: left">

                        <div>
                            <label style="width: 200px; font-size: large">Date:</label>

                            <asp:Label ID="Label5" runat="server" Text=""></asp:Label>

                        </div>
                        <br />
                        <br />

                        <div>
                            <label style="width: 200px; font-size: large">To,</label>
                        </div>
                        <br />
                        <div>
                            <label style="font-size: large">The Joint Registrar (R & D)</label>
                        </div>
                        <br />
                        <div>
                            <label style="font-size: large">TMU, Moradabad.</label>
                        </div>
                        <br />
                        <div>
                            <label id="Label6" style="font-size: large" runat="server">Sub: Request for Releasing Fellowship</label>
                        </div>
                        <div>
                            <label id="Label7" style="font-size: large" runat="server">Dear Ma'am</label>
                        </div>
                        <div>
                            <label id="Label8" style="font-size: large" runat="server">Mr./Ms.</label><asp:Label ID="Label9" Style="font-size: large" runat="server" Text=""></asp:Label>
                            <asp:Label ID="Label10" Style="font-size: large" runat="server" Text=", Works as a Research Fellow in "></asp:Label>
                            <asp:Label ID="Label11" Style="font-size: large" runat="server" Text=","></asp:Label>
                            <asp:Label ID="Label12" Style="font-size: large" runat="server" Text="Teerthanker Mahaveer University, Moradabad."></asp:Label>
                        </div>
                        <div>
                            <label id="Label13" style="font-size: large" runat="server">
                                The Detail for releasing the Fellowship for the months of
                    <asp:Label ID="Label14" runat="server" Text="">,2023, are as fellow:</asp:Label></label>
                        </div>

                    </div>
                    <br />
                    <asp:Table class="boxBodyLine" Width="100%" BackColor="White" BorderColor="Black" BorderWidth="1" ForeColor="Black" GridLines="Both" BorderStyle="Solid" ID="Table1" runat="server">
                        <asp:TableRow>
                            <asp:TableHeaderCell Style="border: 1px solid">
                    
                            </asp:TableHeaderCell>

                            <asp:TableHeaderCell Style="border: 1px solid">
                    
                            </asp:TableHeaderCell>
                            <asp:TableCell Style="border: 1px solid">
                                <asp:Label ID="Label44" runat="server" Text="Fellowship Amount" Enabled="false"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Style="text-align: left;border: 1px solid">
                                <asp:Label ID="Label51" runat="server" Text="Name of Research Scholar" Enabled="false"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Style="text-align: left;border: 1px solid">
                                <asp:Label ID="txtnameofscholar" runat="server" BorderColor="Black"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Style="text-align: left;border: 1px solid" RowSpan="4">
                                <div>
                                    <asp:Label ID="Label16" runat="server" Visible="false" Text="30,000/- for 30 days@1000/- per days"></asp:Label>
                                    <asp:Label ID="Label25" runat="server" Visible="false" Text="25,000/- for 30 days@833/- per days"></asp:Label>
                                </div>
                                <div></div>
                                <div>
                                </div>
                                <div>
                                    <asp:Label ID="Label17" runat="server" Visible="false" Text="30,000/- for 31 days@968/- per days"></asp:Label>
                                    <asp:Label ID="Label26" runat="server" Visible="false" Text="25,000/- for 31 days@806/- per days"></asp:Label>
                                </div>

                            </asp:TableCell>

                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Style="text-align: left;border: 1px solid">
                                <asp:Label ID="Label52" runat="server" Text="Date Of Joining" Enabled="false"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Style="text-align: left;border: 1px solid">
                                <asp:Label ID="txtdateofjoining" runat="server" BorderColor="Black"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Style="text-align: left;border: 1px solid">
                                <asp:Label ID="Label53" runat="server" Text="No. of Actual Working Days (Excluding Leaves Taken, Sundays & Holidays)" Enabled="false"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Style="text-align: left;border: 1px solid">
                                <asp:Label ID="txtnoofactualday" runat="server" BorderColor="Black" Enabled="false"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Style="text-align: left;border: 1px solid">
                                <asp:Label ID="Label54" runat="server" Text="No. of Leaves Taken, Sundays & Holidays" Enabled="false"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Style="text-align: left;border: 1px solid">
                                <div>
                                    <asp:Label ID="Label15" runat="server" Text="CL:"></asp:Label>

                                </div>
                                <div>
                                    <asp:Label ID="Label18" runat="server" Text="ML"></asp:Label>

                                </div>
                                <div>
                                    <asp:Label ID="Label19" runat="server" Text="AL"></asp:Label>

                                </div>
                                <div>
                                    <asp:Label ID="Label20" runat="server" Text="Sunday"></asp:Label>

                                </div>
                                <div>
                                    <asp:Label ID="Label21" runat="server" Text="Holiday"></asp:Label>

                                </div>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Style="text-align: left;border: 1px solid">
                                <asp:Label ID="Label55" runat="server" Text="Account No." Enabled="false"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Style="text-align: left;border: 1px solid">
                                <asp:Label ID="txtaccountNo" runat="server" BorderColor="Black"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Style="text-align: left;border: 1px solid" RowSpan="4">
                                <div>
                                    <asp:Label ID="Label22" runat="server" Text="Total Amount in figure-"></asp:Label>
                                </div>
                                <div></div>
                                <div>
                                </div>
                                <div>
                                    <asp:Label ID="Label23" runat="server" Text="Total Amount in Words-"></asp:Label>
                                </div>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Style="text-align: left;border: 1px solid">
                                <asp:Label ID="Label56" runat="server" Text="IFSC No." Enabled="false"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Style="text-align: left;border: 1px solid">
                                <asp:Label ID="txtifsccode" runat="server" BorderColor="Black" Enabled="false"></asp:Label>
                            </asp:TableCell>

                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Style="text-align: left;border: 1px solid">
                                <asp:Label ID="Label57" runat="server" Text="Bank Name & Branch" Enabled="false"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Style="text-align: left;border: 1px solid">
                                <asp:Label ID="txtbankname" runat="server" BorderColor="Black" Enabled="false"></asp:Label>
                            </asp:TableCell>


                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Style="text-align: left;border: 1px solid">
                                <asp:Label ID="Label58" runat="server" Text="Total Working Days (Including Leaves taken, Sundays & Holidays)" Enabled="false"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Style="text-align: left;border: 1px solid">
                                <asp:Label ID="txttotalworkingdays" runat="server" BorderColor="Black" Enabled="false"></asp:Label>
                            </asp:TableCell>


                        </asp:TableRow>

                    </asp:Table>
                    <br></br>
                    <br></br>
                    <div>
                        <table>
                            <tr>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:Label ID="Signature" runat="server"> Name of the Director/Principal </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:Label ID="Label24" runat="server"> Signature with date </asp:Label>
                                </td>
                            </tr>
                        </table>

                    </div>
                </div>
                <br />
                <br />
            </div>
    </asp:Panel>
    <asp:Button ID="btnDummy" runat="server" Style="display: none;" />
    <asp:ModalPopupExtender ID="GridViewDetails" runat="server" TargetControlID="btnDummy" CancelControlID="Button1"
        PopupControlID="pnlGridViewDetails" BackgroundCssClass="modalBackground" />


  
          
</asp:Content>

