<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="LeaveReport.aspx.cs" Inherits="LeaveReport" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <table cellpadding="0px" cellspacing="0px" style="width:100%">
         <tr> <td style="width:10px"> </td> <td> 
               <table cellpadding="0px" cellspacing="0px" style="width:100%">
         <tr> <td > 
    
   
    <br />
    <asp:Label ID="Label4" runat="server" 
            Text="Approved Leave Report" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
           
  </td></tr>
    <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>
    <tr> <td style="height:13px"> </td></tr>
    <tr> <td align="center">  
    

    <table cellpadding="0px" cellspacing="0px"> <tr> <td> <label>Select Records by  </label> </td> <td style="width:90px"> </td> <td> 
        <asp:RadioButton ID="rdApprovedEmpid" runat="server" Text="Employee Id" 
            GroupName="Leaveapr2ra" 
            AutoPostBack="True" oncheckedchanged="rdApprovedEmpid_CheckedChanged" /> </td> <td style="width:30px"> </td> <td> 
        <asp:RadioButton ID="rdApprovedEMPName" runat="server" Text="Employee Name" 
            GroupName="Leaveapr2ra" 
            AutoPostBack="True" oncheckedchanged="rdApprovedEMPName_CheckedChanged" /> </td> <td style="width:30px"> </td> <td> 
        <asp:RadioButton ID="rdApprovedDatewise" runat="server" Text="Date Wise " 
            GroupName="Leaveapr2ra" 
            AutoPostBack="True" oncheckedchanged="rdApprovedDatewise_CheckedChanged" Checked="True" 
           /> </td>
        <td style="width:30px"> </td> <td> 
        <asp:RadioButton ID="rdAllApprove" runat="server" Text="All " 
            GroupName="Leaveapr2ra" 
            AutoPostBack="True" OnCheckedChanged="rdAllApprove_CheckedChanged" Visible="False" 
           /> </td>

        </tr></table>

   
        
        
        
         </td></tr>
         <tr> <td style="height:13px"> </td></tr>
         <tr> <td class="leftm"> </td></tr>
          <tr> <td style="height:20px"> </td></tr>

          <tr> <td align="center">  <table cellpadding="0px" cellspacing="0px"> <tr> <td>  
              <asp:Panel ID="pnlLeaveApprovedDetailEmpid" runat="server" Visible="false">

               <table cellpadding="0px" cellspacing="0px" > <tr> <td> <label>Enter Employee Id/ Name </label> </td> <td style="width:110px"> </td><td> 
                   <asp:TextBox ID="txtResolvedSearch" runat="server" Width="200px"></asp:TextBox></td> </tr></table>

              </asp:Panel>

                 <asp:Panel ID="pnlLeaveApprovedDetailDatewise" runat="server">

               <table cellpadding="0px" cellspacing="0px" > <tr> <td> <label>From Date</label> </td> <td style="width:10px"> </td><td> 
                   <asp:TextBox ID="txtApprovedFromDate" runat="server"></asp:TextBox>
                   
                   <cc1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtApprovedFromDate" Format="yyyy-MM-dd">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtApprovedFromDate">
      </cc1:TextBoxWatermarkExtender>
                   </td>  <td style="width:30px"> </td> <td>To Date </td>  <td style=" width:10px"> </td><td>
                       <asp:TextBox ID="txtApprovedToDate" runat="server"></asp:TextBox>
                       <cc1:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtApprovedToDate" Format="yyyy-MM-dd">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtApprovedToDate">
      </cc1:TextBoxWatermarkExtender>
                       
                        </td>  </tr></table>

              </asp:Panel>

          </td>   <td style="width:40px"> </td> <td>  
              <asp:Button ID="btnApprovedSearch" runat="server" Text="Search" CssClass="btnLogin" onclick="btnApprovedSearch_Click"/></td> <td style="width:10px"> </td> <td>  <asp:Button ID="btnExportApprove" runat="server" Text="Export To Excel" CssClass="btnLogin" OnClick="btnExportApprove_Click" /></td></tr></table> </td></tr>

                  <tr> <td style="height:13px"> </td></tr>


                  <tr> <td>  
         <asp:GridView ID="grdResolved" runat="server" AutoGenerateColumns="False" 
                  CellPadding="4" Width="100%" ForeColor="#333333" GridLines="None" 
                          AllowPaging="True" onpageindexchanging="grdResolved_PageIndexChanging" OnRowDataBound="grdResolved_RowDataBound" PageSize="50" >
                  <AlternatingRowStyle BackColor="White" />
                  <Columns>

                                             <asp:TemplateField HeaderText="Status" Visible="false">
                          <ItemTemplate>
                              <asp:Label ID="lblApprovalSTatus" runat="server" Text='<%#Bind("Status") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>


                                             <asp:BoundField DataField="ApprovedBy" HeaderText="Approved By" />
                     <asp:TemplateField HeaderText="No_">
                          <ItemTemplate>

                              <asp:Label ID="chkMark" runat="server" Text='<%#Bind("UserID") %>'></asp:Label>
                            <%--  <asp:CheckBox ID="chkMark" runat="server"  Text='<%#Bind("UserID") %>'/>--%>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <%--<asp:TemplateField>
                          <ItemTemplate>
                              <asp:Button ID="btnchangestatus" runat="server" Text="Change Status" CommandArgument='<%#Bind("Userid") %>' OnCommand="btnchangestatus_Command" />
                             
                          </ItemTemplate>
                      </asp:TemplateField>--%>
                      <asp:BoundField DataField="UName" HeaderText="Name" />
                 
                       <asp:BoundField DataField="F_Date" HeaderText="From Date" />
                        <asp:TemplateField HeaderText="Post Lunch">
                         <ItemTemplate>
                             <asp:Label ID="lblPostLunch_GRID" runat="server" Text='<%#Bind("PostLunch") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>
                      <asp:BoundField DataField="T_Date" HeaderText="To Date" />
                        <asp:TemplateField HeaderText="Pre Lunch">
                         <ItemTemplate>
                             <asp:Label ID="lblPreLunch_GRID" runat="server" Text='<%#Bind("PreLunch") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>

                       <asp:BoundField DataField="Leave_Period" HeaderText="Period" Visible="False" >
                                             <HeaderStyle HorizontalAlign="Center" />
                                             <ItemStyle HorizontalAlign="Center" />
                                             </asp:BoundField>
                        <%--<asp:BoundField DataField="No_Of_Days_Leave_Period" HeaderText="No Of Days" />--%>
                    <%--   <asp:BoundField DataField="Leave_Period" HeaderText="Period" >
                                             <HeaderStyle HorizontalAlign="Center" />
                                             <ItemStyle HorizontalAlign="Center" />
                                             </asp:BoundField>--%>

                      
                      

                    <asp:BoundField DataField="Half_Day_type_Desc" HeaderText="Day Mode" >
                                             <HeaderStyle HorizontalAlign="Center" />
                                             <ItemStyle HorizontalAlign="Center" />
                                             </asp:BoundField>
                     <asp:BoundField DataField="No_Of_Days_Leave_Period" HeaderText="Days" >
                                             <HeaderStyle HorizontalAlign="Center" />
                                             <ItemStyle HorizontalAlign="Center" />
                                             </asp:BoundField>
                                         <asp:BoundField DataField="Arrangement" HeaderText="Arrangement" />


                     <%-- <asp:BoundField DataField="Leave_Type" HeaderText="Leave Type" />--%>
                        <asp:TemplateField HeaderText="Leave">
                         <ItemTemplate>
                             <asp:Label ID="lblLeavetypeAppliedApproved" runat="server" Text='<%#Bind("Leave_Type") %>'></asp:Label>
                         </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <%--  <asp:BoundField DataField="CountStatusHR" HeaderText="HR Approval Status" />
                      <asp:BoundField DataField="CountStatusHOD" HeaderText="HOD Approval Status" />
--%>

                   <%--   <asp:BoundField DataField="UName" HeaderText="Name" />--%>
                     
                      <%-- <asp:BoundField DataField="ApprovalStatus" HeaderText="Approval Status" />--%><%-- <asp:BoundField DataField="ProfileUpdateDate" HeaderText="Profile UpdateDate" />--%>
                     
                     
                     
                     <%-- <asp:BoundField DataField="Reason" HeaderText="Reason" />--%>
                         <asp:TemplateField HeaderText="Reason" ItemStyle-HorizontalAlign="Left" Visible="False">
                                                                                                <ItemTemplate>
                                                                                                    <div style="overflow:auto; white-space: nowrap; text-overflow:clip; width:100px">
                                                                                                        <%# Eval("Reason") %>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>

                        <asp:TemplateField HeaderText="Attachment" ItemStyle-HorizontalAlign="Left" Visible="False">
                          <ItemTemplate>
                              <asp:Label ID="lblleaveAttachmentFilenameApprove" runat="server" Text='<%#Bind("AttachmentFilename") %>' Visible="false"></asp:Label>
                              <asp:LinkButton ID="lnkDownloadgridApprove" runat="server" Text="Download" OnClick="DownloadFile"
                    CommandArgument='<%# Eval("AutoNo") %>'></asp:LinkButton>
                           <%--   <asp:Button ID="btnViewAttachment" runat="server" CommandArgument='<%#Bind("AutoNo") %>' OnCommand="btnViewAttachment_Command" Text='<%# Eval("Upload") %>' />--%>
                              </div>
                          </ItemTemplate>
                          <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Left" />
                      </asp:TemplateField>


                      <asp:BoundField DataField="Address_Phone_No" HeaderText="Alternate Phone No" Visible="False" />

                       <asp:TemplateField>
                          <ItemTemplate>
                              <asp:Button ID="btnworkingDetailsApproved" runat="server" Text="Working Details"  CommandArgument='<%#Bind("AutoNo") %>' OnCommand="btnworkingDetailsApproved_Command" />
                          </ItemTemplate>
                      </asp:TemplateField>




                       <%--  <asp:TemplateField HeaderText="Leave Apply Date">
                          <ItemTemplate>
                              <asp:Label ID="lblProfilechangedate" runat="server" Text='<%#Bind("Create_Date") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>--%>
                       <%--<asp:BoundField DataField="Company_Name" HeaderText="Company Name" />--%>
                  </Columns>
                  <EditRowStyle BackColor="#2461BF" />
                  <EmptyDataTemplate>
                      There is no record found..............
                  </EmptyDataTemplate>
                  <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                  <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="White" 
                      HorizontalAlign="Left" CssClass="cssGridheaderfont" Font-Size="10px"/>
                  <PagerStyle BackColor="#ed7600" ForeColor="White" HorizontalAlign="Left" />
                  <RowStyle BackColor="#EFF3FB" CssClass="cssGridheaderfont" Font-Size="9px" />
                  <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                  <SortedAscendingCellStyle BackColor="#F5F7FB" />
                  <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                  <SortedDescendingCellStyle BackColor="#E9EBEF" />
                  <SortedDescendingHeaderStyle BackColor="#4870BE" />
              </asp:GridView>          
                  </td></tr>


    </table> </td> <td style="width:10px"> </td></tr> 



        <tr> <td colspan="3" style="height:90px">  </td></tr>

    </table>

  



     

   <asp:Button ID="Button2" runat="server" Text="Button" Style="display:none" />
     <asp:modalpopupextender ID="ModalPopupExtender2" runat="server" TargetControlID="Button2" PopupControlID="Panel1" BackgroundCssClass="modalBackgroundforco">

                      </asp:modalpopupextender>
     <asp:Panel ID="Panel1" runat="server" BackColor="White" Style="display: none">

           <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:20px">  </td> <td>

            <table cellpadding="0px" cellspacing="0px"> <tr> <td align="right"><asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/logo/close.png" Width="25px" Height="25px" /> </td></tr> 
           <tr> <td style="height:13px"> </td></tr>
            <tr> <td class="leftm"> </td></tr>
           <tr> <td style="height:13px"> </td></tr>
           <tr> <td> <asp:Label ID="Label7" runat="server" 
            Text="Working Details" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>  (Days you worked on week off / holiday) </td></tr>

            <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>
    <tr> <td style="height:13px"> </td></tr>

           <tr> <td> 
     <asp:GridView ID="grdworkingdetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="800px">
               <AlternatingRowStyle BackColor="#CCCCCC"  />
               <Columns>
                   <asp:BoundField DataField="Co_Date" HeaderText="Date"  DataFormatString="{0:yyyy-MM-dd}"/>
                   <asp:BoundField DataField="In time" HeaderText="In Time" />
                   <asp:BoundField DataField="Out Time" HeaderText="Out Time" />
                   <asp:BoundField DataField="Present Hour" HeaderText="Present Hour"  />
                   <asp:BoundField DataField="Expire on" HeaderText="Expire on" DataFormatString="{0:yyyy-MM-dd}" />
                   <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
               </Columns>
               <FooterStyle BackColor="#CCCCCC" />
               <HeaderStyle  BackColor="#C5122F" Font-Bold="True" ForeColor="White" 
                      HorizontalAlign="Left" CssClass="cssGridheaderfont" />
               <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
               <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
               <SortedAscendingCellStyle BackColor="#F1F1F1" />
               <SortedAscendingHeaderStyle BackColor="#808080" />
               <SortedDescendingCellStyle BackColor="#CAC9C9" />
               <SortedDescendingHeaderStyle BackColor="#383838" />
               </asp:GridView>
</td></tr>

       </table>


                                                                                        </td> <td style="width:20px"> </td></tr>


            <tr> <td colspan="3" style="height:20px">  </td></tr>

        </table>



     </asp:Panel>
    

</asp:Content>

