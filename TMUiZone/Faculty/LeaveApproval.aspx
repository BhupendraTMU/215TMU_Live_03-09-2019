<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="LeaveApproval.aspx.cs" Inherits="LeaveApproval" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <fieldset class="boxBody">
 <asp:Label ID="Label1" runat="server" 
            Text="Leave Approval" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 </fieldset>


    <fieldset class="boxBody"> 

<table cellpadding="0px" cellspacing="0px">  <tr>  <td  style="width:10px">  </td>
<td  style="width:180px" valign="top"> 

<table cellpadding="0px" cellspacing="0px" class="leftbg1" style="width:180px; height:430px">  <tr> <td style="width:10px"> </td> <td>


<table cellpadding="0px" cellspacing="0px" >
 <tr> <td style="height:10px"> </td></tr>
 <tr> <td class="leftmMenu">   <img src="../logo/Star.png" />
    <asp:LinkButton ID="lnkProfileview" runat="server" onclick="lnkleaveview_Click" 
        >Approve Leave</asp:LinkButton></td></tr>
    <tr> <td style="height:10px"> </td></tr>
     <tr> <td class="leftmMenu"> <img src="../logo/Star.png" />  
    <asp:LinkButton ID="lnkRejectProfileDetail" runat="server" 
             onclick="lnkRejectLeaveDetail_Click" >Rejected Leave Report</asp:LinkButton></td></tr>
     <tr> <td style="height:10px"> </td></tr>
     <tr> <td class="leftmMenu">  <img src="../logo/Star.png" /> 
    <asp:LinkButton ID="lnkApprovedProfiledetail" runat="server" 
             onclick="lnkApprovedApproveddetail_Click" >Approved  Leave Report</asp:LinkButton></td></tr>
     <tr> <td style="height:10px"> </td></tr>

    </table>
 </td> <td style="width:10px"> </td></tr></table>



    
</td>  <td style="width:30px">  </td>
 <td valign="top"> 
 
 
    <asp:Panel ID="pnlProfileView" runat="server" CssClass="leftBackground" 
        >

    <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:10px"> </td> <td>
    
    <table cellpadding="0px" cellspacing="0px">  <tr> <td > 
    
   
    <br />
    <asp:Label ID="Label2" runat="server" 
            Text="Approve Leave" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
          
  </td></tr>
    <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>
    <tr> <td style="height:13px"> </td></tr>
    <tr> <td>  
    

    <table cellpadding="0px" cellspacing="0px"> <tr> <td> <label>Select Records by </label> </td> <td style="width:90px"> </td> <td> 
        <asp:RadioButton ID="rdEmployeeID" runat="server" Text="Employee Id" 
            GroupName="Leaveapr2" oncheckedchanged="rdEmployeeID_CheckedChanged" 
            AutoPostBack="True"/> </td> <td style="width:30px"> </td> <td> 
        <asp:RadioButton ID="rdEmployeeName" runat="server" Text="Employee Name" 
            GroupName="Leaveapr2" oncheckedchanged="rdEmployeeName_CheckedChanged" 
            AutoPostBack="True"/> </td> <td style="width:30px"> </td> <td> 
        <asp:RadioButton ID="rdDatewise" runat="server" Text="Date Wise " 
            GroupName="Leaveapr2" oncheckedchanged="rdDatewise_CheckedChanged" 
            AutoPostBack="True"/> </td><td style="width:30px"> </td> <td> 
        <asp:RadioButton ID="CHKAllPending" runat="server" Text="All" 
            GroupName="Leaveapr2" 
            AutoPostBack="True" OnCheckedChanged="CHKAllPending_CheckedChanged"/> </td>
        </tr></table>

   
        
        
        
         </td></tr>
         <tr> <td style="height:13px"> </td></tr>
         <tr> <td class="leftm"> </td></tr>
          <tr> <td style="height:20px"> </td></tr>

          <tr> <td >  
              <table cellpadding="0px" cellspacing="0px"> <tr> <td>  
              <asp:Panel ID="pnlEmployeeidName" runat="server" Visible="false">

               <table cellpadding="0px" cellspacing="0px" > <tr> <td> <label>Enter Employee Id/ Name </label> </td> <td style="width:110px"> </td><td> 
                   <asp:TextBox ID="txtSearchName" runat="server" Width="200px"></asp:TextBox></td> </tr></table>

              </asp:Panel>

                 <asp:Panel ID="pnlDate" runat="server" Visible="false">

               <table cellpadding="0px" cellspacing="0px" > <tr> <td> <label>From Date</label> </td> <td style="width:10px"> </td><td> 
                   <asp:TextBox ID="txtfromDate" runat="server"></asp:TextBox>
                   
                     <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfromDate" Format="yyyy-MM-dd">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtfromDate">
      </cc1:TextBoxWatermarkExtender>

                   </td>  <td style="width:30px"> </td> <td>To Date </td>  <td style=" width:10px"> </td><td>
                       <asp:TextBox ID="txtTodate" runat="server"></asp:TextBox> 
                       
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTodate" Format="yyyy-MM-dd">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtTodate">
      </cc1:TextBoxWatermarkExtender>

                       </td>  </tr></table>

              </asp:Panel>

          </td>   <td style="width:40px"> </td> <td>  
              <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btnLogin" 
                  Visible="False" onclick="btnSearch_Click"/></td> <td style="width:10px"> </td> <td>  <asp:Button ID="btnexport_Pending" runat="server" CssClass="btnLogin" Text="Export to excel" OnClick="btnexport_Pending_Click" /></td></tr></table> </td></tr>

                  <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>

     <tr> <td style="height:13px"> </td></tr>

          <tr> <td>
          <table cellpadding="0px" cellspacing="0px">
          <tr> <td align="right"> 
              <asp:Button ID="btnApprove" runat="server" Text="Approve" 
                  Visible="False" onclick="btnApprove_Click" CssClass="btnLogin" OnClientClick="this.disabled = true; this.value = 'Please wait...';" 
UseSubmitBehavior="false"/>   <asp:Button ID="btnreject" runat="server" Text="Reject" Visible="False" 
                  onclick="btnreject_Click" CssClass="btnLogin" />  
              <asp:Label ID="lblHolidayexpect" runat="server" Visible="False"></asp:Label>
              <asp:Label ID="lblOffdayexpect" runat="server" Visible="False"></asp:Label>
              </td>
             
           

               </tr>

              <tr> <td style="height:10px" colspan="3"> </td></tr>

          <tr> <td colspan="3">

             


         
             

              

       
           <%--  <div id="GridScrollProfile">
 --%>

                  <asp:Button ID="Button1" runat="server" Text="Button" Style="display:none" />
           
            
             <table cellpadding="0px" cellspacing="0px"><tr><td align="right">    </td></tr>

                  <tr> <td >  

                     

                       <asp:Button ID="btnselectchecked" runat="server" OnClick="btnselectchecked_Click" Text="Select All" Visible="False" />
                      <asp:Button ID="btnuncheked" runat="server"  Text="UN Checked All" OnClick="btnuncheked_Click" Visible="False" />
                       </td></tr>
              </table> 





              <asp:GridView ID="grdViewApproval"  BorderStyle="Solid" runat="server" AutoGenerateColumns="False" 
                  CellPadding="4"  ForeColor="#333333" GridLines="Horizontal"
                     AllowPaging="True" onpageindexchanging="grdViewApproval_PageIndexChanging" 
                     PageSize="50" OnRowDataBound="grdViewApproval_RowDataBound" Width="980px"
                     >
                  <AlternatingRowStyle BackColor="White" />
                  <Columns>
                      <%--<asp:TemplateField>
                          <ItemTemplate>
                              <asp:Button ID="btnchangestatus" runat="server" Text="Change Status" CommandArgument='<%#Bind("Userid") %>' OnCommand="btnchangestatus_Command" />
                             
                          </ItemTemplate>
                      </asp:TemplateField>--%><%-- <asp:BoundField DataField="CountStatusHR" HeaderText="HR Approval Status" />
                      <asp:BoundField DataField="CountStatusHOD" HeaderText="HOD Approval Status" />--%><%-- <asp:BoundField DataField="ApprovalStatus" HeaderText="Approval Status" />--%><%-- <asp:BoundField DataField="ProfileUpdateDate" HeaderText="Profile UpdateDate" />--%><%--<asp:BoundField DataField="Company_Name" HeaderText="Company Name" />--%><%--<asp:BoundField DataField="Leave_Type" HeaderText="Leave Type" />--%><%--<asp:BoundField DataField="Reason" HeaderText="Reason" />--%>
                      <asp:TemplateField HeaderText="   No_"  HeaderStyle-Width="150px">
                          <ItemTemplate >
                              <asp:CheckBox ID="chkMark" runat="server" Text='<%#Bind("UserID") %>' />
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Status" Visible="false" >
                          <ItemTemplate>
                              <asp:Label ID="lblApprovalSTatus" runat="server" Visible="false" Text='<%#Bind("Status") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="UName" HeaderText="Name" HeaderStyle-Width="200px"  />
                       <asp:BoundField DataField="HOD" HeaderText="HOD"  HeaderStyle-Width="250px" />
                       <asp:BoundField DataField="Department" HeaderText="Department"  HeaderStyle-Width="400px" />
                     
                                                                    <asp:TemplateField HeaderText="From Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblfromdate" runat="server" Text='<%#Bind("F_Date") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                       <asp:TemplateField HeaderText="Post Lunch" HeaderStyle-Width="200px">
                         <ItemTemplate>
                             <asp:Label ID="lblPostLunch_GRID" runat="server" Text='<%#Bind("PostLunch") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>

                      <asp:BoundField DataField="T_Date" HeaderText="To Date" HeaderStyle-Width="200px"  />
                                              <asp:TemplateField HeaderText="Pre Lunch">
                         <ItemTemplate>
                             <asp:Label ID="lblPreLunch_GRID" runat="server" Text='<%#Bind("PreLunch") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>
                       

                      <asp:TemplateField HeaderText="Leave Create Date" Visible="False" HeaderStyle-Width="200px" >
                          <ItemTemplate>
                              <asp:Label ID="lblProfilechangedate" runat="server" Text='<%#Bind("id") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="Leave_Period" HeaderText="Period" Visible="False" HeaderStyle-Width="200px"/>
                      <asp:BoundField DataField="No_Of_Days_Leave_Period" HeaderText="Days" HeaderStyle-Width="200px" >
                     
                      <HeaderStyle HorizontalAlign="Center" />
                     
                      <ItemStyle HorizontalAlign="Center" />
                      </asp:BoundField>
                     
                      <asp:TemplateField HeaderText="Leave" HeaderStyle-Width="200px" >
                          <ItemTemplate>
                              <asp:Label ID="lblLeaveTypegrid" runat="server" Text='<%#Bind("Leave_Type") %>'></asp:Label>
                          </ItemTemplate>
                        
                          <HeaderStyle HorizontalAlign="Center" />
                        
                          <ItemStyle HorizontalAlign="Center" />
                        
                      </asp:TemplateField>

                    <%--   <asp:BoundField DataField="Leave_Period" HeaderText="Period" >
                      <HeaderStyle HorizontalAlign="Center" />
                      <ItemStyle HorizontalAlign="Center" />
                      </asp:BoundField>--%>



                    <asp:BoundField DataField="Half_Day_type_Desc" HeaderText="Day Mode" HeaderStyle-Width="200px" >
                      <HeaderStyle HorizontalAlign="Center" />
                      </asp:BoundField>
                     <asp:BoundField DataField="No_Of_Days_Leave_Period" HeaderText="No Of Leave" Visible="false" HeaderStyle-Width="200px"/>
                                   <%--      <asp:BoundField DataField="Arrangement" HeaderText="Arrangement" />--%>


                       <asp:TemplateField HeaderText="Arrangement"  HeaderStyle-Width="200px" >
                          <ItemTemplate>
                              <asp:Label ID="lblArrangement" runat="server" Text='<%#Bind("Arrangement") %>'></asp:Label>
                          </ItemTemplate>
                          
                           <ItemStyle HorizontalAlign="Center" />
                          
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Reason" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="200px" >
                          <ItemTemplate>
                              <div style="width: 250px;" >
                                  <%# Eval("Reason") %>
                              </div>
                          </ItemTemplate>
                          <ItemStyle HorizontalAlign="Left" />
                      </asp:TemplateField>
                     
                      <asp:TemplateField HeaderText="Attachment" ItemStyle-HorizontalAlign="Left">
                          <ItemTemplate>
                              <asp:Label ID="lblleaveAttachmentFilename" runat="server" Text='<%#Bind("AttachmentFilename") %>' Visible="false"></asp:Label>
                              <asp:LinkButton ID="lnkDownloadgrid" runat="server" Text="Download" OnClick="DownloadFile"
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
                              <asp:Button ID="btnWorkingDetails" runat="server" CommandArgument='<%#Bind("AutoNo") %>' OnCommand="btnWorkingDetails_Command" Text="Working Details" />
                          </ItemTemplate>
                      </asp:TemplateField>
                     
                     
                  </Columns>
                  <EditRowStyle BackColor="#7C6F57" />
                  <EmptyDataTemplate>
                      There is no record found..............
                  </EmptyDataTemplate>
                  <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                  <HeaderStyle  CssClass="cssGridheaderfont" BackColor="#ed7600"  Font-Size="10px" Font-Bold="True" ForeColor="White" />
                  <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center"  />
                  <RowStyle BackColor="#E3EAEB" CssClass="cssGridheaderfont" HorizontalAlign="Left" Font-Size="9px"/>
                  <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                  <SortedAscendingCellStyle BackColor="#F8FAFA" />
                  <SortedAscendingHeaderStyle BackColor="#246B61" />
                  <SortedDescendingCellStyle BackColor="#D4DFE1" />
                  <SortedDescendingHeaderStyle BackColor="#15524A" />
              </asp:GridView> 
          
           </td></tr>
          
           </table>

                
       
          </td></tr>
   
    </table>
     </td> <td style="width:10px"> </td> </tr></table>

    
    </asp:Panel>



      <asp:Panel ID="pnlProfileRejected" runat="server" CssClass="leftBackground" 
         Visible="false">

    <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:10px"> </td> <td>
    
    <table cellpadding="0px" cellspacing="0px">  <tr> <td class="auto-style1" > 
    
   
    <br />
    <asp:Label ID="Label3" runat="server" 
            Text=" Rejected Leave Report" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
           
  </td></tr>
    <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>
    <tr> <td style="height:13px"> </td></tr>
    <tr> <td>  
    

    <table cellpadding="0px" cellspacing="0px"> <tr> <td> <label>Select Records by</label> </td> <td style="width:90px"> </td> <td> 
        <asp:RadioButton ID="rdProfileRectedEMPID" runat="server" Text="Employee Id" 
            GroupName="Leaveapr2r" 
            AutoPostBack="True" oncheckedchanged="rdLeaveRectedEMPID_CheckedChanged"/> </td> <td style="width:30px"> </td> <td> 
        <asp:RadioButton ID="rdrdProfileRectedName" runat="server" Text="Employee Name" 
            GroupName="Leaveapr2r" 
            AutoPostBack="True" oncheckedchanged="rdrdLeaveRectedName_CheckedChanged"/> </td> <td style="width:30px"> </td> <td> 
        <asp:RadioButton ID="rdProfileRectedDatewise" runat="server" Text="Date Wise " 
            GroupName="Leaveapr2r" 
            AutoPostBack="True" 
            oncheckedchanged="rdLeaveRectedDatewise_CheckedChanged"/> </td> <td style="width:30px"> </td> <td> 
        <asp:RadioButton ID="rdAllReject" runat="server" Text="All" 
            GroupName="Leaveapr2r" 
            AutoPostBack="True" OnCheckedChanged="rdAllReject_CheckedChanged" 
           /> </td>
        </tr></table>

   
        
        
        
         </td></tr>
         <tr> <td style="height:13px"> </td></tr>
         <tr> <td class="leftm"> </td></tr>
          <tr> <td style="height:20px"> </td></tr>

          <tr> <td >  <table cellpadding="0px" cellspacing="0px"> <tr> <td>  
              <asp:Panel ID="pnlLeaveRejectedEMPID" runat="server" Visible="false">

               <table cellpadding="0px" cellspacing="0px" > <tr> <td> <label>Enter Employee Id/ Name </label> </td> <td style="width:110px"> </td><td> 
                   <asp:TextBox ID="txtRejectedSearch" runat="server" Width="200px"></asp:TextBox></td> </tr></table>

              </asp:Panel>

                 <asp:Panel ID="pnlLeaveRejectedDatewise" runat="server" Visible="false">

               <table cellpadding="0px" cellspacing="0px" > <tr> <td> <label>From Date</label> </td> <td style="width:10px"> </td><td> 
                   <asp:TextBox ID="txtRejectedFromDate" runat="server"></asp:TextBox>
                   
                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtRejectedFromDate" Format="yyyy-MM-dd">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtRejectedFromDate">
      </cc1:TextBoxWatermarkExtender>

                   </td>  <td style="width:30px"> </td> <td>To Date </td>  <td style=" width:10px"> </td><td>
                       <asp:TextBox ID="txtRejectedToDate" runat="server"></asp:TextBox> 
                       
                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtRejectedToDate" Format="yyyy-MM-dd">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtRejectedToDate">
      </cc1:TextBoxWatermarkExtender>
                       </td>  </tr></table>

              </asp:Panel>

          </td>   <td style="width:40px"> </td> <td>  
              <asp:Button ID="btnrejectedsearch" runat="server" Text="Search" CssClass="btnLogin" 
                  Visible="False" onclick="btnrejectedsearch_Click"/></td> <td style="width:10px"> </td> <td> <asp:Button ID="btnrejectedexport" runat="server" CssClass="btnLogin" Text="Export to excel" OnClick="btnrejectedexport_Click"  /> </td></tr></table> </td></tr>

                  <tr> <td style="height:13px"> </td></tr>


                  <tr> <td>
                  <table cellpadding="0px" cellspacing="0px"> 
                  

                  <tr> <td> 
                 <%-- <div id="grdRejectedwrap">--%>  
                    <asp:GridView ID="grdRejected" runat="server" AutoGenerateColumns="False" 
                  CellPadding="4" Width="980px" ForeColor="#333333" GridLines="None" 
                          AllowPaging="True" onpageindexchanging="grdRejected_PageIndexChanging" OnRowDataBound="grdRejected_RowDataBound" PageSize="50">
                  <AlternatingRowStyle BackColor="White" />
                  <Columns>
                     

                      <%--<asp:TemplateField>
                          <ItemTemplate>
                              <asp:Button ID="btnchangestatus" runat="server" Text="Change Status" CommandArgument='<%#Bind("Userid") %>' OnCommand="btnchangestatus_Command" />
                             
                          </ItemTemplate>
                      </asp:TemplateField>--%>
                     
                      <%-- <asp:BoundField DataField="ApprovalStatus" HeaderText="Approval Status" />--%><%-- <asp:BoundField DataField="ProfileUpdateDate" HeaderText="Profile UpdateDate" />--%>
                       <asp:TemplateField HeaderText="No_">
                           <ItemTemplate>

                               <asp:Label ID="Label6" runat="server" Text='<%#Bind("UserID") %>'></asp:Label>
                               <%--<asp:CheckBox ID="chkMark" runat="server" Text='<%#Bind("UserID") %>' />--%>
                           </ItemTemplate>
                       </asp:TemplateField>
                         <asp:BoundField DataField="UName" HeaderText="Name" />
                     <%--  <asp:TemplateField HeaderText="Leave Apply Date">
                           <ItemTemplate>
                               <asp:Label ID="lblProfilechangedate" runat="server" Text='<%#Bind("Create_Date") %>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>--%>
                    <%--   <asp:TemplateField HeaderText="Approval Status">
                           <ItemTemplate>
                               <asp:Label ID="lblApprovalSTatus" runat="server" Text='<%#Bind("Status") %>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>--%>
                    
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
                       <%-- <asp:BoundField DataField="No_Of_Days_Leave_Period" HeaderText="No Of Days" />--%>

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
                             <asp:Label ID="lblLeavetypeAppliedrejected" runat="server" Text='<%#Bind("Leave_Type") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>


                     <%-- <asp:BoundField DataField="Reason" HeaderText="Reason" />
                      --%>
                             <asp:TemplateField HeaderText="Reason" ItemStyle-HorizontalAlign="Left">
                                                                                                <ItemTemplate>
                                                                                                    <div style="overflow:auto; white-space: nowrap; text-overflow:clip; width:150px">
                                                                                                        <%# Eval("Reason") %>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>
                      <asp:BoundField DataField="Address_Phone_No" HeaderText="Alternate Phone No" Visible="False" />
                    
                      <asp:BoundField DataField="Rejected Approval" HeaderText="Rejected" Visible="False" />
                      
                       

                          <asp:TemplateField HeaderText="Rejected Remarks" ItemStyle-HorizontalAlign="Left">
                                                                                                <ItemTemplate>
                                                                                                    <div style="width: 110px; overflow:auto; white-space: nowrap; text-overflow:clip">
                                                                                                        <%# Eval("RejectedByUserRemarks") %>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>

                     <%--  <asp:BoundField DataField="RejectedByHODRemarks" HeaderText="Rejected HOD Remarks" />--%>
                    
                    <%--   <asp:BoundField DataField="Company_Name" HeaderText="Company Name" />--%>

                      <asp:TemplateField>
                          <ItemTemplate>
                              <asp:Button ID="btnworkingDetailsrejected" runat="server" OnCommand="btnworkingDetailsrejected_Command" Text="Working Details"  CommandArgument='<%#Bind("AutoNo") %>' />
                          </ItemTemplate>
                      </asp:TemplateField>
                         <asp:TemplateField HeaderText="Attachment" ItemStyle-HorizontalAlign="Left">
                          <ItemTemplate>
                              <asp:Label ID="lblleaveAttachmentFilenameRej" runat="server" Text='<%#Bind("AttachmentFilename") %>' Visible="false"></asp:Label>
                              <asp:LinkButton ID="lnkDownloadgridRej" runat="server" Text="Download" OnClick="DownloadFile"
                    CommandArgument='<%# Eval("AutoNo") %>'></asp:LinkButton>
                           <%--   <asp:Button ID="btnViewAttachment" runat="server" CommandArgument='<%#Bind("AutoNo") %>' OnCommand="btnViewAttachment_Command" Text='<%# Eval("Upload") %>' />--%>
                              </div>
                          </ItemTemplate>
                          <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Left" />
                      </asp:TemplateField>

                  </Columns>
                  <EditRowStyle BackColor="#2461BF" />
                  <EmptyDataTemplate>
                      There is no record found..............
                  </EmptyDataTemplate>
                  <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                  <HeaderStyle BackColor="#ED7600" Font-Bold="True" ForeColor="White" 
                      HorizontalAlign="Left" CssClass="cssGridheaderfont" Font-Size="10px" />
                  <PagerStyle BackColor="#ED7600" ForeColor="White" HorizontalAlign="Left" />
                  <RowStyle BackColor="#EFF3FB" CssClass="cssGridheaderfont" Font-Size="10px" />
                  <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                  <SortedAscendingCellStyle BackColor="#F5F7FB" />
                  <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                  <SortedDescendingCellStyle BackColor="#E9EBEF" />
                  <SortedDescendingHeaderStyle BackColor="#4870BE" />
              </asp:GridView>
                  
                 <%-- </div>--%>
                  </td></tr>

                  </table>
                  
                   </td></tr>


    <tr> <td class="leftm"> </td></tr>

     <tr> <td style="height:13px"> </td></tr>

          <tr> <td> 
          
          <table cellpadding="0px" cellspacing="0px"> <tr> <td>  </td></tr></table>
          </td></tr>
   
    </table>
     </td> <td style="width:10px"> </td> </tr></table>

    
    </asp:Panel>



         <asp:Panel ID="pnlApprovedDetail" runat="server" CssClass="leftBackground" Visible="false">

    <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:10px"> </td> <td>
    
    <table cellpadding="0px" cellspacing="0px"> 
         <tr> <td > 
    
   
    <br />
    <asp:Label ID="Label4" runat="server" 
            Text="Approved Leave Report" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
           
  </td></tr>
    <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>
    <tr> <td style="height:13px"> </td></tr>
    <tr> <td>  
    

    <table cellpadding="0px" cellspacing="0px"> <tr> <td> <label>Select Records by  </label> </td> <td style="width:90px"> </td> <td> 
        <asp:RadioButton ID="rdApprovedEmpid" runat="server" Text="Employee Id" 
            GroupName="Leaveapr2ra" 
            AutoPostBack="True" oncheckedchanged="rdApprovedEmpid_CheckedChanged" /> </td> <td style="width:30px"> </td> <td> 
        <asp:RadioButton ID="rdApprovedEMPName" runat="server" Text="Employee Name" 
            GroupName="Leaveapr2ra" 
            AutoPostBack="True" oncheckedchanged="rdApprovedEMPName_CheckedChanged" /> </td> <td style="width:30px"> </td> <td> 
        <asp:RadioButton ID="rdApprovedDatewise" runat="server" Text="Date Wise " 
            GroupName="Leaveapr2ra" 
            AutoPostBack="True" oncheckedchanged="rdApprovedDatewise_CheckedChanged" 
           /> </td>
        <td style="width:30px"> </td> <td> 
        <asp:RadioButton ID="rdAllApprove" runat="server" Text="All " 
            GroupName="Leaveapr2ra" 
            AutoPostBack="True" OnCheckedChanged="rdAllApprove_CheckedChanged" 
           /> </td>

        </tr></table>

   
        
        
        
         </td></tr>
         <tr> <td style="height:13px"> </td></tr>
         <tr> <td class="leftm"> </td></tr>
          <tr> <td style="height:20px"> </td></tr>

          <tr> <td >  <table cellpadding="0px" cellspacing="0px"> <tr> <td>  
              <asp:Panel ID="pnlLeaveApprovedDetailEmpid" runat="server" Visible="false">

               <table cellpadding="0px" cellspacing="0px" > <tr> <td> <label>Enter Employee Id/ Name </label> </td> <td style="width:110px"> </td><td> 
                   <asp:TextBox ID="txtResolvedSearch" runat="server" Width="200px"></asp:TextBox></td> </tr></table>

              </asp:Panel>

                 <asp:Panel ID="pnlLeaveApprovedDetailDatewise" runat="server" Visible="false">

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
              <asp:Button ID="btnApprovedSearch" runat="server" Text="Search" CssClass="btnLogin" 
                  Visible="False" onclick="btnApprovedSearch_Click"/></td> <td style="width:10px"> </td> <td>  <asp:Button ID="btnExportApprove" runat="server" Text="Export To Excel" CssClass="btnLogin" OnClick="btnExportApprove_Click" /></td></tr></table> </td></tr>

                  <tr> <td style="height:13px"> </td></tr>


                  <tr> <td>  
                  <%--<div id="grdResolvedwrap">--%> 
                   <asp:GridView ID="grdResolved" runat="server" AutoGenerateColumns="False" 
                  CellPadding="4" Width="980px" ForeColor="#333333" GridLines="None" 
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
                         <asp:TemplateField HeaderText="Reason" ItemStyle-HorizontalAlign="Left">
                                                                                                <ItemTemplate>
                                                                                                    <div style="overflow:auto; white-space: nowrap; text-overflow:clip; width:100px">
                                                                                                        <%# Eval("Reason") %>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>

                        <asp:TemplateField HeaderText="Attachment" ItemStyle-HorizontalAlign="Left">
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
             <%-- </div>--%>
                  </td></tr>

    <tr> <td class="leftm"> </td></tr>

     <tr> <td style="height:13px"> </td></tr>

          <tr> <td> 
          
          <table cellpadding="0px" cellspacing="0px"> <tr> <td>  </td></tr></table>
          </td></tr>
   
    </table>
     </td> <td style="width:10px"> </td> </tr></table>

    
    </asp:Panel>

       <asp:Panel ID="pnlMain" runat="server" Visible="false" >

      <table cellpadding="0px" cellspacing="0px" style="width:100%"> <tr> <td align="center"> <br /> <br /> 
         &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;  <img src="h.jpg" />    </td></tr></table>
      

     </asp:Panel>
 

                       <cc1:modalpopupextender ID="ModalPopupExtender1" runat="server" TargetControlID="btnreject" PopupControlID="pnlrejectedDetail" BackgroundCssClass="modalBackground">

                      </cc1:modalpopupextender>
                      <asp:Panel ID="pnlrejectedDetail" runat="server" CssClass="modalPopup" Style="display: none" >
                      <table cellpadding="0px" cellspacing="0px" >
                      <tr> <td>  
                     <div class="header">
       
   <table cellpadding="0px" cellspacing="0px" style="width:400px"> <tr> <td align="left">   <label><asp:Label ID="Label5" runat="server" 
            Text="Rejected Remarks" Font-Size="12pt"  ForeColor="White" Font-Names="Open Sans" ></asp:Label>

 </label>   </td> <td align="right"> 
     
     
     <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/logo/close.png" Width="25px" Height="25px" />
     </td></tr></table>
                    </div></td></tr>
                      
                      <tr> <td style="height:10px"> </td></tr>
                      <tr> <td>
                      <div class="body">
                        <table cellpadding="0px" cellspacing="0px"> <tr>  <td style="width:20px"> </td> <td><label>  Remarks </label></td> <td style="width:20px"> </td> <td>
                          <asp:TextBox ID="txtRemarksRejected" runat="server" Height="150px" TextMode="MultiLine" 
                              Width="320px"></asp:TextBox> </td> <td style="width:20px"> </td> </tr> </table>
                              </div>
                               </td></tr>
 <tr> <td style="height:10px"> </td></tr>

  <tr> <td >  
  <div class="footer" align="right">
  <asp:Button ID="btnRejectProfile1" runat="server" Text="Save" CssClass="btnLogin" 
            onclick="btnRejectProfile1_Click" OnClientClick="this.disabled = true; this.value = 'Please wait...';" 
UseSubmitBehavior="false" />
     
      </div>
       </td></tr>
      <tr> <td style="height:10px"> </td></tr>
                       </table>
                         
                      </asp:Panel>

    


 </td></tr></table>


   
    

         
       
                           




</fieldset>
    

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

