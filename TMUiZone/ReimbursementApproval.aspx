<%@ Page Title="" Language="C#" MasterPageFile="~/IndexMaster.master" AutoEventWireup="true" CodeFile="ReimbursementApproval.aspx.cs" Inherits="LeaveApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="text/css">

#GridScrollProfile 
{
width:630px;
height:100%;
overflow:scroll;
}


#grdRejectedwrap 
{
width:630px;
height:100%;
overflow:scroll;
}


#grdResolvedwrap 
{
width:630px;
height:100%;
overflow:scroll;
}


 </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <fieldset class="boxBody">
 <asp:Label ID="Label1" runat="server" 
            Text="Reimbursement Approval Detail" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 </fieldset>


    <fieldset class="boxBody"> 

<table cellpadding="0px" cellspacing="0px">  <tr>  <td  style="width:10px">  </td>
<td  style="width:200px" valign="top"> 

<table cellpadding="0px" cellspacing="0px" class="leftbg1" style="width:240px; height:430px">  <tr> <td style="width:10px"> </td> <td>


<table cellpadding="0px" cellspacing="0px" >
 <tr> <td style="height:10px"> </td></tr>
 <tr> <td class="leftmMenu">   <img src="logo/Star.png" />
    <asp:LinkButton ID="lnkProfileview" runat="server" onclick="lnkleaveview_Click" 
        >Pending Reimbursement </asp:LinkButton></td></tr>
    <tr> <td style="height:10px"> </td></tr>
     <tr> <td class="leftmMenu"> <img src="logo/Star.png" />  
    <asp:LinkButton ID="lnkRejectProfileDetail" runat="server" 
             onclick="lnkRejectLeaveDetail_Click" >Rejected Reimbursement</asp:LinkButton></td></tr>
     <tr> <td style="height:10px"> </td></tr>
     <tr> <td class="leftmMenu">   <img src="logo/Star.png" />
    <asp:LinkButton ID="lnkApprovedProfiledetail" runat="server" 
             onclick="lnkApprovedApproveddetail_Click" >Approved Reimbursement </asp:LinkButton></td></tr>
     <tr> <td style="height:10px"> </td></tr>

    </table>
 </td> <td style="width:10px"> </td></tr></table>



    
</td>  <td style="width:30px">  </td>
 <td valign="top"> 
 
   <asp:Panel ID="pnlMain" runat="server" >

      <table cellpadding="0px" cellspacing="0px" style="width:100%"> <tr> <td align="center"> <br /> <br /> 
         &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;  <img src="logo/reim.jpg" />             </td></tr></table>
      

     </asp:Panel>
    <asp:Panel ID="pnlProfileView" runat="server" CssClass="leftBackground" 
         Visible="false">

    <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:10px"> </td> <td>
    
    <table cellpadding="0px" cellspacing="0px">  <tr> <td > 
    
   
    <br />
    <asp:Label ID="Label2" runat="server" 
            Text="Pending Reimbursement" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
           
  </td></tr>
    <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>
    <tr> <td style="height:13px"> </td></tr>
    <tr> <td>  
    

    <table cellpadding="0px" cellspacing="0px"> 
        <tr> <td> <label>Select Records by</label> </td> <td style="width:40px"> </td> <td> 
        <asp:RadioButton ID="rdEmployeeID" runat="server" Text="Employee Id" 
            GroupName="REA" oncheckedchanged="rdEmployeeID_CheckedChanged" 
            AutoPostBack="True"/> </td> <td style="width:20px"> </td> <td> 
        <asp:RadioButton ID="rdEmployeeName" runat="server" Text="Employee Name" 
            GroupName="REA" oncheckedchanged="rdEmployeeName_CheckedChanged" 
            AutoPostBack="True"/> </td> <td style="width:20px"> </td> <td> 
        <asp:RadioButton ID="rdDatewise" runat="server" Text="Date Wise " 
            GroupName="REA" oncheckedchanged="rdDatewise_CheckedChanged" 
            AutoPostBack="True"/> </td><td style="width:20px"> </td><td> <asp:RadioButton ID="rdAllReimApproval" runat="server" Text="All " 
            GroupName="REA" 
            AutoPostBack="True" OnCheckedChanged="rdAllReimApproval_CheckedChanged"  
           /></td>
        </tr></table>

   
        
        
        
         </td></tr>
         <tr> <td style="height:13px"> </td></tr>
         <tr> <td class="leftm"> </td></tr>
          <tr> <td style="height:20px"> </td></tr>

          <tr> <td >  <table cellpadding="0px" cellspacing="0px"> <tr> <td>  
              <asp:Panel ID="pnlEmployeeidName" runat="server" Visible="false">

               <table cellpadding="0px" cellspacing="0px" > <tr> <td> <label>Enter Employee Id/ Name </label> </td> <td style="width:110px"> </td><td> 
                   <asp:TextBox ID="txtSearchName" runat="server" Width="200px"></asp:TextBox></td> </tr></table>

              </asp:Panel>

                 <asp:Panel ID="pnlDate" runat="server" Visible="false">

               <table cellpadding="0px" cellspacing="0px" > <tr> <td> <label>From Date</label> </td> <td style="width:10px"> </td><td> 
                   <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox> 
                    
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate" Format="yyyy-MM-dd">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtFromDate">
      </cc1:TextBoxWatermarkExtender>

                   </td>  <td style="width:30px"> </td> <td>To Date </td>  <td style=" width:10px"> </td><td>
                       <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                       
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate" Format="yyyy-MM-dd">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtToDate">
      </cc1:TextBoxWatermarkExtender>

                        </td>  </tr></table>

              </asp:Panel>

          </td>   <td style="width:40px"> </td> <td>  
              <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btnLogin" 
                  Visible="False" onclick="btnSearch_Click"/></td></tr></table> </td></tr>

                  <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>

 <tr> <td style="height:13px"> </td></tr>
   <tr> <td>  
   
   <table cellpadding="0px" cellspacing="0px"> 
   <tr> <td> 
              <asp:Button ID="btnApprove" runat="server" Text="Approve" 
                  Visible="False" onclick="btnApprove_Click" CssClass="btnLogin" />  </td> <td style="width:300px"> </td> <td>
              <asp:Button ID="btnreject" runat="server" Text="Reject" Visible="False" 
                  onclick="btnreject_Click" CssClass="btnLogin" />  
             <%-- <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" TargetControlID="btnreject" PopupControlID="pnlrejectedDetail">
            </cc1:ModalPopupExtender>
             --%>

               </td></tr>
   
   <tr> <td colspan="3" style="height:13px"> </td></tr>

   <tr> <td colspan="3"> 
       <div id="GridScrollProfile">
       
        <asp:GridView ID="grdReimbursment" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" GridLines="Horizontal" Width="1300px" AllowPaging="True" 
            onpageindexchanging="grdReimbursment_PageIndexChanging" PageSize="8" 
            onrowdatabound="grdReimbursment_RowDataBound">
        <AlternatingRowStyle BackColor="#F7F7F7" />
        <Columns>
          <%--  <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="Edit" runat="server" Text="Edit" />
                </ItemTemplate>
            </asp:TemplateField>--%>
            <%-- <asp:TemplateField>
                <ItemTemplate>
                  
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="Select ">
                <ItemTemplate>
                    <asp:CheckBox ID="chkMark" runat="server"  Text='<%#Bind("Userid") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Expense_Type" HeaderText="Expense Type" />
            <asp:TemplateField HeaderText="Approved Amount">
                <ItemTemplate>
                    <asp:TextBox ID="txtreimAMT" runat="server"  Width="60px"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Credit_limit" HeaderText="Credit Limit" />
            <asp:BoundField DataField="Expense_Amount" HeaderText="Expense Amount" />
            <asp:BoundField DataField="UName" HeaderText="Name" />
            <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
            <asp:BoundField DataField="From_Date" DataFormatString="{0:d}" HeaderText="From Date " />
            <asp:BoundField DataField="ToDate" DataFormatString="{0:d}" HeaderText="To Date" />
            <asp:TemplateField HeaderText="Bill Detail">
                <ItemTemplate>
                    <table cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td>
                                <asp:Label ID="lblBill_Detail" runat="server" Text='<%# Eval("Bill_Detail") %>'></asp:Label>
                            </td>
                            <td style="width:10px"></td>
                            <td>
                                <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%# Eval("Bill_Detail") %>' Font-Bold="True" ForeColor="#CC0000" oncommand="lnkView_Command">View</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sending Date">
                <ItemTemplate>
                    <asp:Label ID="lblProfilechangedate" runat="server" Text='<%#Bind("Create_Date") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Status" HeaderText="Status" />
            <asp:TemplateField HeaderText="searial no" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblNoofchange" runat="server" Text='<%#Bind("SerialNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Document_No" HeaderText="Document No" />
        </Columns>
        <EmptyDataTemplate>
            There is no record found...........
        </EmptyDataTemplate>
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="White" 
                      HorizontalAlign="Left" Font-Size="8pt" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"/>
        <PagerStyle BackColor="#C5122F" ForeColor="#4A3C8C"  HorizontalAlign="Left" Font-Size="8pt" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" />
        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Font-Size="7pt" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"/>
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <SortedAscendingCellStyle BackColor="#F4F4FD" />
        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
        <SortedDescendingCellStyle BackColor="#D8D8F0" />
        <SortedDescendingHeaderStyle BackColor="#3E3277" />
        </asp:GridView>

        </div>

        </td></tr>
   </table>

   
   
   </td></tr>


     <tr> <td style="height:13px"> </td></tr>

          <tr> <td> 
          
          <table cellpadding="0px" cellspacing="0px"> <tr> <td>  </td></tr></table>
          </td></tr>
   
    </table>
     </td> <td style="width:10px"> </td> </tr></table>

    
    </asp:Panel>



      <asp:Panel ID="pnlProfileRejected" runat="server" CssClass="leftBackground" 
         Visible="false">

    <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:10px"> </td> <td>
    
    <table cellpadding="0px" cellspacing="0px">  <tr> <td > 
    
   
    <br />
    <asp:Label ID="Label3" runat="server" 
            Text="Rejected Reimbursement" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
           
  </td></tr>
    <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>
    <tr> <td style="height:13px"> </td></tr>
    <tr> <td>  
    

    <table cellpadding="0px" cellspacing="0px"> <tr> <td> <label>Filter options </label> </td> <td style="width:40px"> </td> <td> 
        <asp:RadioButton ID="rdProfileRectedEMPID" runat="server" Text="Employee Id" 
            GroupName="REAREC" 
            AutoPostBack="True" oncheckedchanged="rdLeaveRectedEMPID_CheckedChanged"/> </td> <td style="width:20px"> </td> <td> 
        <asp:RadioButton ID="rdrdProfileRectedName" runat="server" Text="Employee Name" 
            GroupName="REAREC" 
            AutoPostBack="True" oncheckedchanged="rdrdLeaveRectedName_CheckedChanged"/> </td> <td style="width:20px"> </td> <td> 
        <asp:RadioButton ID="rdProfileRectedDatewise" runat="server" Text="Date Wise " 
            GroupName="REAREC" 
            AutoPostBack="True" 
            oncheckedchanged="rdLeaveRectedDatewise_CheckedChanged"/> </td><td style="width:20px"> </td><td> <asp:RadioButton ID="rdReimRejctAll" runat="server" Text="All " 
            GroupName="REAREC" 
            AutoPostBack="True"  
           /></td>
        </tr></table>

   
        
        
        
         </td></tr>
         <tr> <td style="height:13px"> </td></tr>
         <tr> <td class="leftm"> </td></tr>
          <tr> <td style="height:20px"> </td></tr>

          <tr> <td >  <table cellpadding="0px" cellspacing="0px"> <tr> <td>  
              <asp:Panel ID="pnlLeaveRejectedEMPID" runat="server" Visible="false">

               <table cellpadding="0px" cellspacing="0px" > <tr> <td> <label>Enter Employee Id/ Name </label> </td> <td style="width:110px"> </td><td> 
                   <asp:TextBox ID="TextBox3" runat="server" Width="200px"></asp:TextBox></td> </tr></table>

              </asp:Panel>

                 <asp:Panel ID="pnlLeaveRejectedDatewise" runat="server" Visible="false">

               <table cellpadding="0px" cellspacing="0px" > <tr> <td> <label>From Date</label> </td> <td style="width:10px"> </td><td> 
                   <asp:TextBox ID="txtRejectedFromDate" runat="server"></asp:TextBox>
                   
                     <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtRejectedFromDate" Format="dd/MM/yyyy">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" WatermarkText="dd/MM/yyyy" TargetControlID="txtRejectedFromDate">
      </cc1:TextBoxWatermarkExtender>
                   </td>  <td style="width:30px"> </td> <td>To Date </td>  <td style=" width:10px"> </td><td>
                       <asp:TextBox ID="txtRejectedToDate" runat="server"></asp:TextBox>
                         <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtRejectedToDate" Format="dd/MM/yyyy">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" WatermarkText="dd/MM/yyyy" TargetControlID="txtRejectedToDate">
      </cc1:TextBoxWatermarkExtender>
                        </td>  </tr></table>

              </asp:Panel>

          </td>   <td style="width:40px"> </td> <td>  
              <asp:Button ID="btnrejectedsearch" runat="server" Text="Search" CssClass="btnLogin" 
                  Visible="False"/></td></tr></table> </td></tr>

                  <tr> <td style="height:13px"> </td></tr>

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
    
    <table cellpadding="0px" cellspacing="0px">  <tr> <td > 
    
   
    <br />
    <asp:Label ID="Label4" runat="server" 
            Text=" Approved Reimbursement" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
           
  </td></tr>
    <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>
    <tr> <td style="height:13px"> </td></tr>
    <tr> <td>  
    

    <table cellpadding="0px" cellspacing="0px"> <tr> <td> <label>Select Records by </label> </td> <td style="width:40px"> </td> <td> 
        <asp:RadioButton ID="rdApprovedEmpid" runat="server" Text="Employee Id" 
            GroupName="REAApr" 
            AutoPostBack="True" oncheckedchanged="rdApprovedEmpid_CheckedChanged" /> </td> <td style="width:20px"> </td> <td> 
        <asp:RadioButton ID="rdApprovedEMPName" runat="server" Text="Employee Name" 
            GroupName="REAApr" 
            AutoPostBack="True" oncheckedchanged="rdApprovedEMPName_CheckedChanged" /> </td> <td style="width:20px"> </td> <td> 
        <asp:RadioButton ID="rdApprovedDatewise" runat="server" Text="Date Wise " 
            GroupName="REAApr" 
            AutoPostBack="True" oncheckedchanged="rdApprovedDatewise_CheckedChanged" 
           /> </td><td style="width:20px"> </td><td> <asp:RadioButton ID="rdAllApprove" runat="server" Text="All " 
            GroupName="REAApr" 
            AutoPostBack="True"  
           /></td>
        </tr></table>

   
        
        
        
         </td></tr>
         <tr> <td style="height:13px"> </td></tr>
         <tr> <td class="leftm"> </td></tr>
          <tr> <td style="height:20px"> </td></tr>

          <tr> <td >  <table cellpadding="0px" cellspacing="0px"> <tr> <td>  
              <asp:Panel ID="pnlLeaveApprovedDetailEmpid" runat="server" Visible="false">

               <table cellpadding="0px" cellspacing="0px" > <tr> <td> <label>Enter Employee Id/ Name </label> </td> <td style="width:110px"> </td><td> 
                   <asp:TextBox ID="TextBox6" runat="server" Width="200px"></asp:TextBox></td> </tr></table>

              </asp:Panel>

                 <asp:Panel ID="pnlLeaveApprovedDetailDatewise" runat="server" Visible="false">

               <table cellpadding="0px" cellspacing="0px" > <tr> <td> <label>From Date</label> </td> <td style="width:10px"> </td><td> 
                   <asp:TextBox ID="txtApprovalFromDate" runat="server"></asp:TextBox>
                      <cc1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtApprovalFromDate" Format="dd/MM/yyyy">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" WatermarkText="dd/MM/yyyy" TargetControlID="txtApprovalFromDate">
      </cc1:TextBoxWatermarkExtender>
                   
                   </td>  <td style="width:30px"> </td> <td>To Date </td>  <td style=" width:10px"> </td><td>
                       <asp:TextBox ID="txtApprovalTodate" runat="server"></asp:TextBox> 
                       
                        <cc1:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtApprovalTodate" Format="dd/MM/yyyy">
      </cc1:CalendarExtender>
      <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" WatermarkText="dd/MM/yyyy" TargetControlID="txtApprovalTodate">
      </cc1:TextBoxWatermarkExtender>
                       </td>  </tr></table>

              </asp:Panel>

          </td>   <td style="width:40px"> </td> <td>  
              <asp:Button ID="btnApprovedSearch" runat="server" Text="Search" CssClass="btnLogin" 
                  Visible="False"/></td></tr></table> </td></tr>

                  <tr> <td style="height:13px"> </td></tr>

    <tr> <td class="leftm"> </td></tr>

    <tr> <td>  
    
    
    
      <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnreject" PopupControlID="pnlrejectedDetail" BackgroundCssClass="modalBackground">

                      </cc1:ModalPopupExtender>
                      <asp:Panel ID="pnlrejectedDetail" runat="server" CssClass="modalPopup" Style="display: none">
                      <table cellpadding="0px" cellspacing="0px" >
                      <tr> <td>  
                     <div class="header">
       
   <table cellpadding="0px" cellspacing="0px" style="width:400px"> <tr> <td>   <label><asp:Label ID="Label5" runat="server" 
            Text="Reject Approval Detail" Font-Size="12pt"  ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

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
            onclick="btnRejectProfile1_Click" />
     
      </div>
       </td></tr>
      <tr> <td style="height:10px"> </td></tr>
                       </table>
                         
                      </asp:Panel>

    
    
    </td></tr>

     <tr> <td style="height:13px"> </td></tr>

          <tr> <td> 
          
          <table cellpadding="0px" cellspacing="0px"> <tr> <td>  </td></tr></table>
          </td></tr>
   
    </table>
     </td> <td style="width:10px"> </td> </tr></table>

    
    </asp:Panel>


 
 </td></tr></table>


   



</fieldset>

</asp:Content>

