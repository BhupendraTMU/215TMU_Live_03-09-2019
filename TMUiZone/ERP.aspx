<%@ Page Title="" Language="C#" MasterPageFile="~/IndexMaster.master" AutoEventWireup="true" CodeFile="ERP.aspx.cs" Inherits="recognition" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

      <style type="text/css">
        #grdWrap_View
{
width:650px;
height:100%;
overflow:scroll;
}

          #grdViewLeaveStatuswrap
{
width:650px;
height:100%;
overflow:scroll;
}

  #GRDAppovalStatus
{
width:650px;
height:100%;
overflow:scroll;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     
    <fieldset class="boxBody">
 <asp:Label ID="Label1" runat="server" 
            Text="ERP" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

 </fieldset>


    <fieldset class="boxBody"> 

<table cellpadding="0px" cellspacing="0px">  <tr>  <td  style="width:10px">  </td>
<td  style="width:200px" valign="top"> 

<table cellpadding="0px" cellspacing="0px" class="leftbg1" style="width:220px; height:430px">  <tr> <td style="width:10px"> </td> <td>


<table cellpadding="0px" cellspacing="0px" >
    <tr> <td style="height:10px"> </td></tr>
 <tr> <td class="leftmMenu">  
     <img src="logo/Star.png" />
    <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl="~/Payslipdetail.aspx"  
        >View Pay Slip</asp:LinkButton></td></tr>

     <tr> <td style="height:10px"> </td></tr>
 <tr> <td class="leftmMenu">  
     <img src="logo/Star.png" />
    <asp:LinkButton ID="lnkBudget" runat="server" PostBackUrl="~/Budget.aspx" Visible="False"   
        >Budget</asp:LinkButton></td></tr>

    <tr> <td style="height:10px"> </td></tr>
 <tr> <td class="leftmMenu">  
     <img src="logo/Star.png" />
    <asp:LinkButton ID="LinkButton4" runat="server" PostBackUrl="~/Forecast.aspx" Visible="False"   
        >Forecast</asp:LinkButton></td></tr>

 <tr> <td style="height:10px"> </td></tr>



 <tr> <td class="leftmMenu">  
     <img src="logo/Star.png" />
    <asp:LinkButton ID="lnkUserReqest" runat="server" OnClick="lnkUserReqest_Click" 
        > Purchase Request</asp:LinkButton></td></tr>
    <tr> <td style="height:10px"> </td></tr>
     <tr> <td class="leftmMenu">  <img src="logo/Star.png" />
    <asp:LinkButton ID="lnkViewRequest" runat="server" OnClick="lnkViewRequest_Click" 
            >View Requests </asp:LinkButton></td></tr>
     <tr> <td style="height:10px"> </td></tr>
  

     
     <tr> <td class="leftmMenu">  <img src="logo/Star.png" id="imgStar" runat="server"/>
    <asp:LinkButton ID="lnkPendingApproval" runat="server" OnClick="lnkPendingApproval_Click"  
              >Approve Request</asp:LinkButton>  <asp:Label ID="lblpendingappcount" runat="server" ForeColor="Red"></asp:Label></td></tr>
     <tr> <td style="height:10px">  </td></tr>


   <%# Eval("Vendor_Address") %>


   <tr> <td class="leftmMenu">  <img src="logo/Star.png" id="img1" runat="server"/>
    <asp:LinkButton ID="lnlApprovalStatus" runat="server" OnClick="lnlApprovalStatus_Click" 
              >Purchase Request Report</asp:LinkButton> </td></tr>
     <tr> <td style="height:30px">  </td></tr>



    <tr><td align="left">


       <%--  <fieldset class="boxBody" >--%>
 <asp:Label ID="Label9" runat="server" 
            Text="ERP Client" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" Visible="False" ></asp:Label>

       
 <%--</fieldset>--%>


        </td></tr>
<%--     <tr> <td style="height:4px"> </td></tr>--%>
    <tr> <td class="leftmMenu">  </td></tr>
    <tr> <td style="height:4px"> </td></tr>
  


    <tr> <td class="leftmMenu">  
     <img src="logo/Star.png" />
    <asp:LinkButton ID="lnkAshokatesting" runat="server" OnClientClick="window.open('http://10.1.4.52:8080/DynamicsNAV71/webclient', '');" Visible="False"
        >Ashoka Testing</asp:LinkButton></td></tr>
    <tr> <td style="height:10px"> </td></tr>
     <tr> <td class="leftmMenu">  <img src="logo/Star.png" />
    <asp:LinkButton ID="lnkAshokalive" runat="server" 
        OnClientClick="window.open('http://10.1.4.52:8080/DynamicsNAV71/webclient', '');" Visible="False">Ashoka University - Live</asp:LinkButton></td></tr>
     <tr> <td style="height:10px"> </td></tr>
  
      <tr> <td class="leftmMenu">  <img src="logo/Star.png" />
    <asp:LinkButton ID="lnkIFRE" runat="server"  OnClientClick="window.open('http://10.1.4.52:8080/DynamicsNAV71/webclient', '');" Visible="False">IFRE</asp:LinkButton></td></tr>
     <tr> <td style="height:10px"> </td></tr>
  


    </table>
 </td> <td style="width:10px"> </td></tr></table>



    
</td>  <td style="width:30px">  </td>
 <td valign="top"> 
 
 
    <asp:Panel ID="pnlUserRequest" runat="server" CssClass="leftBackground">

    <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:10px"> </td> <td>
    
    <table cellpadding="0px" cellspacing="0px">  <tr> <td > 
    
   
    <br />
    <asp:Label ID="Label2" runat="server" 
            Text="Purchase Request" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
           
  </td></tr>
         <tr> <td style="height:13px"> </td></tr>
        <tr> <td class="leftm"> </td></tr>
    <tr> <td style="height:6px"> </td></tr>

        <tr> <td>
        


            <table cellpadding="0px" cellspacing="0px"> 

                 <tr> <td>  Requisition Type<asp:Label ID="Label8" runat="server" Text="Label" Visible="False"></asp:Label>
                     </td> <td style="width:10px"> </td> <td>  <asp:DropDownList ID="ddRequistionType" runat="server" Height="29px">
                     <asp:ListItem Selected="True" Value="0">--</asp:ListItem>
                     <asp:ListItem Value="1">Item</asp:ListItem>
                     <asp:ListItem Value="2">Services</asp:ListItem>
                     </asp:DropDownList>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddRequistionType" ErrorMessage="Can't be blank" ForeColor="Red" InitialValue="0" SetFocusOnError="True" ValidationGroup="userrequest"></asp:RequiredFieldValidator>
                     </td> <td>  </td></tr>

                <tr> <td style="height:10px" colspan="3"> </td></tr>

                <tr> <td>  Item / Services to Purchase</td> <td style="width:10px"> </td> <td>  <asp:TextBox ID="txtItemToPurchase" runat="server" Width="562px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtItemToPurchase" ErrorMessage="  Can't be blank" ForeColor="Red" SetFocusOnError="True" ValidationGroup="userrequest"></asp:RequiredFieldValidator>
                    </td> <td>  </td></tr>

                <tr> <td style="height:10px" colspan="3"> </td></tr>
                 <tr> <td>  Reason for Purchase</td> <td style="width:10px"> </td> <td>  <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Width="550px"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtReason" ErrorMessage="  Can't be blank" ForeColor="Red" SetFocusOnError="True" ValidationGroup="userrequest"></asp:RequiredFieldValidator>
                     </td> <td>  </td></tr>

                 <tr> <td style="height:10px" colspan="3"> </td></tr>
                 <tr> <td>  Approx Cost</td> <td style="width:10px"> </td> <td>   <table cellpadding="0px" cellspacing="0px"> <tr>  <td>  <asp:TextBox ID="txtApprox" runat="server" Width="200px"></asp:TextBox></td> <td> &nbsp;&nbsp; RS/- </td> <td style="width:140px">  
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtApprox" ErrorMessage="  Can't be blank" ForeColor="Red" SetFocusOnError="True" ValidationGroup="userrequest"></asp:RequiredFieldValidator>
                     </td><td> Quantity :  </td> <td style="width:10px"> </td> <td> 
                     <asp:TextBox ID="txtQuantity" runat="server" Width="100px"></asp:TextBox>
                     </td></tr> </table> </td> <td>  </td></tr>

                  <tr> <td style="height:10px" colspan="3"> </td></tr>
                 <tr> <td> Department </td> <td style="width:10px"> </td> <td>   <table cellpadding="0px" cellspacing="0px"> <tr>  <td>  
                     <asp:DropDownList ID="ddDepartment" runat="server" Width="300px" Height="29px">
                     </asp:DropDownList>
                     </td> <td>  </td> <td style="width:100px">  
                     
                     </td><td> Campus   </td> <td style="width:10px"> </td> <td> 
                     <asp:TextBox ID="txtCampus" runat="server" Width="100px"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCampus" ErrorMessage="  Can't be blank" ForeColor="Red" SetFocusOnError="True" ValidationGroup="userrequest"></asp:RequiredFieldValidator>
                     </td></tr> </table> </td> <td>  </td></tr>


                 <tr> <td style="height:10px" colspan="3"> </td></tr>
                 <tr> <td>  Date</td> <td style="width:10px"> </td> <td>   <table cellpadding="0px" cellspacing="0px"> <tr>  <td>   <asp:TextBox ID="txtDate" runat="server" Width="100px"></asp:TextBox>

                     <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate" Format="yyyy-MM-dd"></asp:CalendarExtender>
                      <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtDate">
      </asp:TextBoxWatermarkExtender>
                                                                     &nbsp;
                     </td> <td>  </td> <td style="width:100px">  
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDate" ErrorMessage="  Can't be blank" ForeColor="Red" SetFocusOnError="True" ValidationGroup="userrequest"></asp:RequiredFieldValidator>
                     </td><td> Store&nbsp;  </td> <td style="width:10px"> </td> <td> 
                     <asp:DropDownList ID="ddStore" runat="server" Width="310px" Height="29px"></asp:DropDownList>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddStore" ErrorMessage="  Can't be blank" ForeColor="Red" SetFocusOnError="True" ValidationGroup="userrequest"></asp:RequiredFieldValidator>
                     </td></tr> </table> </td> <td>  </td></tr>

                 
                 

                

                   <tr> <td style="height:10px" colspan="3"> </td></tr>
                 <tr> <td>  Remarks </td> <td style="width:10px"> </td> <td>  <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="550px"></asp:TextBox></td> <td>  </td></tr>



                 <tr> <td style="height:10px" colspan="3"> </td></tr>
                 <tr> <td>  Vendor (If you know) </td> <td style="width:10px"> </td> <td>  <table cellpadding="0px" cellspacing="0px"> <tr>  <td>  <asp:TextBox ID="txtVendor" runat="server" Width="300px"></asp:TextBox></td> <td style="width:20px">  </td><td> Vendor Mobile No :  </td> <td style="width:10px"> </td> <td> <asp:TextBox ID="txtMobileMo" runat="server" Width="100px"></asp:TextBox></td></tr> </table>  </td> <td>  </td></tr>


                <tr> <td style="height:10px" colspan="3"> </td></tr>
                 <tr> <td>  Vendor Contact Person </td> <td style="width:10px"> </td> <td>  <asp:TextBox ID="txtContactPerson" runat="server" Width="562px"></asp:TextBox></td> <td>  </td></tr>



                 <tr> <td style="height:10px" colspan="3"> </td></tr>
                 <tr> <td>  Vendor Address </td> <td style="width:10px"> </td> <td>  <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Width="550px"></asp:TextBox></td> <td>  </td></tr>

                 
                  <tr> <td style="height:5px" colspan="3"> </td></tr>
                 <tr> <td>   </td> <td style="width:10px"> </td> <td align="right">  
                     <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnLogin" OnClick="btnCancel_Click" Visible="False"/>
                     <asp:Button ID="btnResend" runat="server" CssClass="btnLogin"  Text="Resend" OnClick="btnResend_Click" Visible="False" />
                     <asp:Button ID="btn_sendforAproval" runat="server" Text="Send for Approval" CssClass="btnLogin" OnClick="btn_sendforAproval_Click" ValidationGroup="userrequest"  /></td> <td>  </td></tr>
                <tr> <td style="height:3px" colspan="3"> </td></tr>
            </table>



             </td></tr>
         

  
    


    
    </table>
     </td> <td style="width:10px"> </td> </tr></table>

    
    </asp:Panel>



     
       <asp:Panel ID="pnlViewRequest" runat="server" CssClass="leftBackground" Visible="false">

    <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:10px"> </td> <td>
    
    <table cellpadding="0px" cellspacing="0px">  <tr> <td > 
    
   
    <br />
    <asp:Label ID="Label3" runat="server" 
            Text="View Requests" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
           
  </td></tr>
         <tr> <td style="height:13px"> </td></tr>
        <tr> <td class="leftm"> </td></tr>
    <tr> <td style="height:6px"> </td></tr>

        <tr> <td>
        

            <table cellpadding="0px" cellspacing="0px">  <tr> <td>  From Date  </td><td style="width:10px"> </td> <td>
                <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>

                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFromDate" Format="yyyy-MM-dd"></asp:CalendarExtender>
                      <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtFromDate">
      </asp:TextBoxWatermarkExtender>
                                            
                                                                                                                  </td> <td style="width:20px"> </td> <td>To Date  </td><td style="width:10px"> </td>  <td>  <asp:TextBox ID="txtTodate" runat="server"></asp:TextBox>

                                                                                                                      <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtTodate" Format="yyyy-MM-dd"></asp:CalendarExtender>
                      <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtTodate">
      </asp:TextBoxWatermarkExtender>
                                                                                                                                                                                                       </td> <td style="width:20px"> </td><td> Status </td> <td style="width:10px">   </td> <td> <asp:DropDownList ID="ddstatus" runat="server" Height="29px" >
          <asp:ListItem Selected="True">All</asp:ListItem>
          <asp:ListItem>Pending</asp:ListItem>
          <asp:ListItem Value="Approved">Approved</asp:ListItem>
                <asp:ListItem>Rejected</asp:ListItem>
          </asp:DropDownList></td><td style="width:20px"> </td>  <td >  <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" /></td>   </tr></table>


       



             </td></tr>


            <tr> <td style="height:13px"> </td></tr>

        <tr> <td>


           <%-- <div id="grdWrap_View"> --%>
      <asp:GridView ID="grdViewdetail" runat="server" AutoGenerateColumns="False" 
             CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" Width="866px" PageSize="15" OnPageIndexChanging="grdViewdetail_PageIndexChanging">
             <AlternatingRowStyle BackColor="White" />
             <Columns>
              
                
                 <asp:BoundField DataField="Request_Date" HeaderText="Date"  DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="Requisition_Type" HeaderText="Requisition Type" />
                  <asp:BoundField DataField="Item_To_Purchase" HeaderText="Item /Services to Purchase" />
                 <asp:TemplateField HeaderText="HOD Remarks" ItemStyle-HorizontalAlign="Left">
                     <ItemTemplate>
                         <div style="width: 250px; overflow:auto; white-space: nowrap; text-overflow:clip">
                             <%# Eval("Approval_Remarks") %>
                         </div>
                     </ItemTemplate>
                     <ItemStyle HorizontalAlign="Left" />
                 </asp:TemplateField>
               

                   <asp:BoundField DataField="Approval_Status" HeaderText="Status" />

                       <asp:TemplateField>
                     <ItemTemplate>
                         <asp:Button ID="btnViewgridRequest" runat="server" Text="View"   CommandArgument='<%#Bind("id") %>' OnCommand="btnViewgridRequest_Command" />
                     </ItemTemplate>
                 </asp:TemplateField>
               

                <%-- <asp:BoundField DataField="Item_To_Purchase" HeaderText="Item To Purchase" />
            
                    <asp:TemplateField HeaderText="Reason For Purchase" ItemStyle-HorizontalAlign="Left">
                                                                                                <ItemTemplate>
                                                                                                    <div style="width: 150px; overflow:auto; white-space: nowrap; text-overflow:clip">
                                                                                                        <%# Eval("Reason_For_Purchase") %>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>


                 <asp:BoundField DataField="Approxe_Cost" HeaderText="Approx Cost" />
                 <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                 <asp:BoundField DataField="Vendor" HeaderText="Vendor" />
                 <asp:BoundField DataField="Vendor_Mobile_No" HeaderText="Vendor Mobile No" />
               
               
                     <asp:BoundField DataField="Vendor_Address" HeaderText="Vendor Address" />
               
               
                     <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left">
                                                                                                <ItemTemplate>
                                                                                                    <div style="width: 150px; overflow:auto; white-space: nowrap; text-overflow:clip">
                                                                                                        <%# Eval("Remarks") %>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>
                 <asp:BoundField DataField="Store" HeaderText="Store" />--%>
               
               
             </Columns>
             <EditRowStyle BackColor="#2461BF" />
                      <EmptyDataTemplate>
                          There are no records found
                      </EmptyDataTemplate>
             <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
             <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="White" CssClass="cssGridheaderfont" HorizontalAlign="Left"/>
             <PagerStyle BackColor="#C5122F" ForeColor="White" HorizontalAlign="Left" />
             <RowStyle BackColor="#EFF3FB" CssClass="cssGridheaderfont" HorizontalAlign="Left" />
             <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
             <SortedAscendingCellStyle BackColor="#F5F7FB" />
             <SortedAscendingHeaderStyle BackColor="#6D95E1" />
             <SortedDescendingCellStyle BackColor="#E9EBEF" />
             <SortedDescendingHeaderStyle BackColor="#4870BE" />
         </asp:GridView>
            <%--</div>--%>
             </td></tr>
    <tr> <td class="leftm"> </td></tr>
    <tr> <td style="height:13px"> </td></tr>
   

   <tr> <td>
   
   
    </td></tr>

     <tr> <td style="height:3px"> </td></tr>

    
    
           <tr> <td style="height:13px"> </td></tr>
        

                 




    <tr> <td class="leftm"> </td></tr>

     <tr> <td style="height:10px"> </td></tr>

    


     <tr> <td>  
         </td></tr>


          <tr> <td> 
          
          <table cellpadding="0px" cellspacing="0px"> <tr> <td>  </td></tr></table>
          </td></tr>
   
    </table>
     </td> <td style="width:10px"> </td> </tr></table>

    
    </asp:Panel>

    <asp:Panel ID="pnlPending_Approval" runat="server" CssClass="leftBackground" Visible="false">

    <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:10px"> </td> <td>
    
    <table cellpadding="0px" cellspacing="0px">  <tr> <td > 
    
   
    <br />
    <asp:Label ID="Label4" runat="server" 
            Text="Approve Request" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
           
  </td></tr>
         <tr> <td style="height:13px"> </td></tr>
        <tr> <td class="leftm"> </td></tr>
    <tr> <td style="height:6px"> </td></tr>

        <tr> <td>
        

            <table cellpadding="0px" cellspacing="0px">  <tr> <td>  From Date  </td><td style="width:10px"> </td> <td>
                <asp:TextBox ID="txtFromDateApproval" runat="server"></asp:TextBox>

                  <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtFromDateApproval" Format="yyyy-MM-dd"></asp:CalendarExtender>
                      <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtFromDateApproval">
      </asp:TextBoxWatermarkExtender>
                                                                                                                  </td> <td style="width:20px"> </td> <td>To Date  </td><td style="width:10px"> </td>  <td>  <asp:TextBox ID="txtTodateApproval" runat="server"></asp:TextBox>
                                                                                                                      <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtTodateApproval" Format="yyyy-MM-dd"></asp:CalendarExtender>
                      <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtTodateApproval">
      </asp:TextBoxWatermarkExtender>
                                                                                                                                                                                                       </td> <td style="width:10px"> </td><td>  </td> <td style="width:10px">   </td> <td> &nbsp;</td><td style="width:10px"> </td>  <td >  <asp:Button ID="btnShowPendingApproval" runat="server" Text="Search" OnClick="btnShowPendingApproval_Click"  /></td>   </tr></table>


       



             </td></tr>


            <tr> <td style="height:13px"> </td></tr>

        <tr> <td>

      <asp:GridView ID="grdPendingApproval" runat="server" AutoGenerateColumns="False" 
             CellPadding="4" ForeColor="#333333" GridLines="None" Width="866px" AllowPaging="True" PageSize="15" OnPageIndexChanging="grdPendingApproval_PageIndexChanging">
             <AlternatingRowStyle BackColor="White" />
             <Columns>
               <asp:BoundField DataField="Request_Date" HeaderText="Date"  DataFormatString="{0:yyyy-MM-dd}" />
                 <asp:BoundField DataField="Requisition_Type" HeaderText="Requisition Type" />
                 <asp:BoundField DataField="Item_To_Purchase" HeaderText="Item to Purchase" />
               
                 <asp:TemplateField HeaderText="Requested By">
                     <ItemTemplate>
                         <asp:Label ID="Label5" runat="server" Text= '<%# Eval("Request_Created_UserName") %>' ></asp:Label>
                         &nbsp;(&nbsp;<asp:Label ID="Label6" runat="server" Text= '<%# Eval("Request_Created_UserId") %>'></asp:Label>
                         )
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField>
                     <ItemTemplate>
                         <asp:Button ID="btnViewgrid" runat="server" Text="View" OnCommand="btnViewgrid_Command"  CommandArgument='<%#Bind("id") %>' />
                     </ItemTemplate>
                 </asp:TemplateField>
               
             </Columns>
             <EditRowStyle BackColor="#2461BF" />
                      <EmptyDataTemplate>
                          There are no records found
                      </EmptyDataTemplate>
             <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
             <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="White" CssClass="cssGridheaderfont" HorizontalAlign="Left"/>
             <PagerStyle BackColor="#C5122F" ForeColor="White" HorizontalAlign="Left" />
             <RowStyle BackColor="#EFF3FB" CssClass="cssGridheaderfont" HorizontalAlign="Left" />
             <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
             <SortedAscendingCellStyle BackColor="#F5F7FB" />
             <SortedAscendingHeaderStyle BackColor="#6D95E1" />
             <SortedDescendingCellStyle BackColor="#E9EBEF" />
             <SortedDescendingHeaderStyle BackColor="#4870BE" />
         </asp:GridView>

             </td></tr>
    <tr> <td class="leftm"> </td></tr>
    <tr> <td style="height:13px"> </td></tr>
   

 




    


    


          <tr> <td> 
          
          <table cellpadding="0px" cellspacing="0px"> <tr> <td>  </td></tr></table>
          </td></tr>
   
    </table>
     </td> <td style="width:10px"> </td> </tr></table>

    
    </asp:Panel>

 
      <asp:Panel ID="pnlApprovalSatus" runat="server" CssClass="leftBackground" Visible="false">

    <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:10px"> </td> <td>
    
    <table cellpadding="0px" cellspacing="0px">  <tr> <td > 
    
   
    <br />
    <asp:Label ID="Label7" runat="server" 
            Text="Purchase Request Report" Font-Size="15pt" ForeColor="#093A62" 
            Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
           
  </td></tr>
         <tr> <td style="height:13px"> </td></tr>
        <tr> <td class="leftm"> </td></tr>
    <tr> <td style="height:6px"> </td></tr>

        <tr> <td>
        

            <table cellpadding="0px" cellspacing="0px">  <tr> <td>  From Date  </td><td style="width:10px"> </td> <td>
                <asp:TextBox ID="txtFromdateApprovalStatus" runat="server"></asp:TextBox>

                <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtFromdateApprovalStatus" Format="yyyy-MM-dd"></asp:CalendarExtender>
                      <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtFromdateApprovalStatus">
      </asp:TextBoxWatermarkExtender>
                                            
                                                                                                                  </td> <td style="width:10px"> </td> <td>To Date  </td><td style="width:10px"> </td>  <td>  <asp:TextBox ID="txtTodateApprovalStatus" runat="server"></asp:TextBox>

                                                                                                                      <asp:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtTodateApprovalStatus" Format="yyyy-MM-dd"></asp:CalendarExtender>
                      <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" WatermarkText="yyyy-MM-dd" TargetControlID="txtTodateApprovalStatus">
      </asp:TextBoxWatermarkExtender>
                                                                                                                                                                                                       </td> <td style="width:10px"> </td><td> Status </td> <td style="width:10px">   </td> <td> <asp:DropDownList ID="ddApprovalStatus" runat="server" Height="29px" >
         <%-- <asp:ListItem Selected="True">All</asp:ListItem>--%>
          <%--<asp:ListItem>Pending</asp:ListItem>--%>
          <asp:ListItem Value="Approved">Approved</asp:ListItem>
                <asp:ListItem>Rejected</asp:ListItem>
          </asp:DropDownList></td><td style="width:10px"> </td>  <td >  <asp:Button ID="btnSearchApproval" runat="server" Text="Search" OnClick="btnSearchApproval_Click"  /></td>   </tr></table>


       



             </td></tr>


            <tr> <td style="height:13px"> </td></tr>

        <tr> <td>


            <%--<div id="GRDAppovalStatus"> --%>
      <asp:GridView ID="grdApprovalStatus" runat="server" AutoGenerateColumns="False" 
             CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" PageSize="15"  OnPageIndexChanging="grdApprovalStatus_PageIndexChanging" Width="866px">
             <AlternatingRowStyle BackColor="White" />
             <Columns>
              
                
                 <asp:BoundField DataField="Request_Date" HeaderText="Date"  DataFormatString="{0:yyyy-MM-dd}" />
                 
                 <asp:BoundField DataField="Approval_Status" HeaderText="Status" />
                   <asp:TemplateField HeaderText="Requested By">
                     <ItemTemplate>
                         <asp:Label ID="Label5" runat="server" Text= '<%# Eval("Request_Created_UserName") %>' ></asp:Label>
                         &nbsp;(&nbsp;<asp:Label ID="Label6" runat="server" Text= '<%# Eval("Request_Created_UserId") %>'></asp:Label>
                         )
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:BoundField DataField="Item_To_Purchase" HeaderText="Item / Services To Purchase" />
             
                    <asp:TemplateField HeaderText="Reason For Purchase" ItemStyle-HorizontalAlign="Left">
                                                                                                <ItemTemplate>
                                                                                                    <div style="width: 250px; overflow:auto; white-space: nowrap; text-overflow:clip">
                                                                                                        <%# Eval("Reason_For_Purchase") %>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>


                     <asp:TemplateField>
                         <ItemTemplate>
                             <asp:Button ID="btnViewApprovaldetails" runat="server" OnCommand="btnViewApprovaldetails_Command" Text="View" CommandArgument='<%#Bind("id") %>' />
                         </ItemTemplate>
                 </asp:TemplateField>


             </Columns>
             <EditRowStyle BackColor="#2461BF" />
                      <EmptyDataTemplate>
                          There are no records found
                      </EmptyDataTemplate>
             <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
             <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="White" CssClass="cssGridheaderfont" HorizontalAlign="Left"/>
             <PagerStyle BackColor="#C5122F" ForeColor="White" HorizontalAlign="Left" />
             <RowStyle BackColor="#EFF3FB" CssClass="cssGridheaderfont" HorizontalAlign="Left" />
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
   
   
    </td></tr>

     <tr> <td style="height:3px"> </td></tr>

    
    
           <tr> <td style="height:13px"> </td></tr>
        

                 




    <tr> <td class="leftm"> </td></tr>

     <tr> <td style="height:10px"> </td></tr>

    


     <tr> <td>  
         </td></tr>


          <tr> <td> 
          
          <table cellpadding="0px" cellspacing="0px"> <tr> <td>  </td></tr></table>
          </td></tr>
   
    </table>
     </td> <td style="width:10px"> </td> </tr></table>

    
    </asp:Panel>


 </td></tr></table>


   



</fieldset>
     <asp:Button ID="Button2" runat="server" Text="Button" style="display:none"/>
    <asp:Button ID="Button1" runat="server" Text="Button" style="display:none"/>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="pnlViewDetails" BackgroundCssClass="modalBackground">
        
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlViewDetails" runat="server" BackColor="White">


        <table cellpadding="0px" cellspacing="0px"> <tr> <td style="width:10px">  </td> <td> 

             <table cellpadding="0px" cellspacing="0px"> 
             <tr> <td style="height:10px" align="right"> <asp:LinkButton ID="lnkClose" runat="server" OnClick="lnkClose_Click">X</asp:LinkButton> </td></tr>

             <tr> <td style="height:10px">  </td></tr>
            <tr> <td>
                <asp:GridView ID="grdviewApprovaldata" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="1300px">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                    
                 <asp:BoundField DataField="Store" HeaderText="Store" />       
               <asp:TemplateField HeaderText="Reason For Purchase" ItemStyle-HorizontalAlign="Left">
                                                                                                <ItemTemplate>
                                                                                                    <div style="width: 100px; overflow:auto; white-space: nowrap; text-overflow:clip">
                                                                                                        <%# Eval("Reason_For_Purchase") %>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>

                        <asp:BoundField DataField="Approxe_Cost" HeaderText="Approx Cost" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />

                         <asp:BoundField DataField="Department Code" HeaderText="Department Code" />
                         <asp:BoundField DataField="Campus" HeaderText="Campus" />
                        <asp:BoundField DataField="Vendor" HeaderText="Vendor" />
                        <asp:BoundField DataField="Vendor_Mobile_No" HeaderText="Vendor Mobile" />
                           <asp:BoundField DataField="Vendor_Contact_Person" HeaderText="Vendor Contact Person" />
                       <%-- <asp:BoundField DataField="Vendor_Address" HeaderText="Vendor Address" />--%>

                           <asp:TemplateField HeaderText="Vendor Address" ItemStyle-HorizontalAlign="Left">
                                                                                                <ItemTemplate>
                                                                                                    <div style="width: 100px; overflow:auto; white-space: nowrap; text-overflow:clip">
                                                                                                        <%# Eval("Vendor_Address") %>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="User Remarks" ItemStyle-HorizontalAlign="Left">
                                                                                                <ItemTemplate>
                                                                                                    <div style="width: 150px; overflow:auto; white-space: nowrap; text-overflow:clip">
                                                                                                        <%# Eval("Remarks") %>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>

                    </Columns>
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" CssClass="cssGridheaderfont"/>
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <SortedAscendingCellStyle BackColor="#FDF5AC" />
                    <SortedAscendingHeaderStyle BackColor="#4D0000" />
                    <SortedDescendingCellStyle BackColor="#FCF6C0" />
                    <SortedDescendingHeaderStyle BackColor="#820000" />
                </asp:GridView>

                 </td></tr> 

            <tr> <td style="height:10px">  
                <asp:Label ID="lblViewID" runat="server" Visible="False"></asp:Label>
                </td></tr>
              <tr> <td > 

                  <table cellpadding="0px" cellspacing="0px"> <tr> <td>  Remarks </td> <td style="width:10px"> </td> <td>  <asp:TextBox ID="txtRemarksforApproval" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox></td><td style="width:10px"> </td> <td> <asp:Button ID="btnApproval" runat="server" Text="Approved" OnClick="btnApproval_Click" /> </td> <td style="width:10px">  </td> <td>  <asp:Button ID="btnReject" runat="server" Text="Reject" OnClick="btnReject_Click" /></td></tr> </table>


                   </td></tr>

              <tr> <td style="height:10px">  </td></tr>


                 <tr> <td style="height:10px"> <asp:Label ID="lblerror" runat="server" Font-Size="12pt" ForeColor="#CC0000"></asp:Label>  </td></tr>

                 <tr> <td style="height:10px">  </td></tr>

        </table>

                                                                                        </td> <td style="width:10px"> </td></tr> </table>

       


    </asp:Panel>


    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Button2" PopupControlID="pnlViewUserRequest" BackgroundCssClass="modalBackground">
        
    </asp:ModalPopupExtender>



         <asp:Panel ID="pnlViewUserRequest" runat="server" BackColor="White">

             <table cellpadding="0px" cellspacing="0px">

                    <tr> <td style="height:10px" align="right"> <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Close">X</asp:LinkButton> </td></tr>
                 
                 <tr> <td style="height:10px">  </td></tr>
                 
                  <tr> <td>  

                   <asp:GridView ID="grdViewRequestionforuser" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="1300px">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                    
                 <asp:BoundField DataField="Store" HeaderText="Store" />       
               <asp:TemplateField HeaderText="Reason For Purchase" ItemStyle-HorizontalAlign="Left">
                                                                                                <ItemTemplate>
                                                                                                    <div style="width: 100px; overflow:auto; white-space: nowrap; text-overflow:clip">
                                                                                                        <%# Eval("Reason_For_Purchase") %>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>

                        <asp:BoundField DataField="Approxe_Cost" HeaderText="Approx Cost" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                        <asp:BoundField DataField="Department Code" HeaderText="Department Code" />
                        <asp:BoundField DataField="Campus" HeaderText="Campus" />
                        <asp:BoundField DataField="Vendor" HeaderText="Vendor" />
                        <asp:BoundField DataField="Vendor_Mobile_No" HeaderText="Vendor Mobile" />
                       <%-- <asp:BoundField DataField="Vendor_Address" HeaderText="Vendor Address" />--%>

                           <asp:BoundField DataField="Vendor_Contact_Person" HeaderText="Vendor Contact Person" />
                        <asp:TemplateField HeaderText="Vendor Address" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <div style="width: 100px; overflow:auto; white-space: nowrap; text-overflow:clip">
                                    <%# Eval("Vendor_Address") %>
                                </div>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <div style="width: 150px; overflow:auto; white-space: nowrap; text-overflow:clip">
                                    <%# Eval("Remarks") %>
                                </div>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEditorResend" runat="server" OnCommand="lnkEditorResend_Command" CommandArgument='<%#Bind("id") %>'>Edit and Resend</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" CssClass="cssGridheaderfont"/>
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" CssClass="cssGridheaderfont"/>
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <SortedAscendingCellStyle BackColor="#FDF5AC" />
                    <SortedAscendingHeaderStyle BackColor="#4D0000" />
                    <SortedDescendingCellStyle BackColor="#FCF6C0" />
                    <SortedDescendingHeaderStyle BackColor="#820000" />
                </asp:GridView>

                                                              </td></tr>



             </table>

             </asp:Panel>



    <asp:Button ID="Button3" runat="server" Text="Button" Style="display:none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="Button3" PopupControlID="pnlHODAprovlDetails" BackgroundCssClass="modalBackground">
        
    </asp:ModalPopupExtender>


     <asp:Panel ID="pnlHODAprovlDetails" runat="server" BackColor="White">

             <table cellpadding="0px" cellspacing="0px">

                    <tr> <td style="height:10px" align="right"> <asp:LinkButton ID="LinkButton2" runat="server" ToolTip="Close">X</asp:LinkButton> </td></tr>
                 
                 <tr> <td style="height:10px">  </td></tr>
                 
                  <tr> <td>  

                   <asp:GridView ID="grdApprovallhodview" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="1300px">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                    
                 <asp:BoundField DataField="Store" HeaderText="Store" />       

                        <asp:BoundField DataField="Approxe_Cost" HeaderText="Approx Cost" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                        <asp:BoundField DataField="Department Code" HeaderText="Department Code" />
                        <asp:BoundField DataField="Campus" HeaderText="Campus" />
                        <asp:BoundField DataField="Vendor" HeaderText="Vendor" />
                        <asp:BoundField DataField="Vendor_Mobile_No" HeaderText="Vendor Mobile" />
                       <%-- <asp:BoundField DataField="Vendor_Address" HeaderText="Vendor Address" />--%>

                           <asp:BoundField DataField="Vendor_Contact_Person" HeaderText="Vendor Contact Person" />
                        <asp:TemplateField HeaderText="Vendor Address" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <div style="width: 100px; overflow:auto; white-space: nowrap; text-overflow:clip">
                                    <%# Eval("Vendor_Address") %>
                                </div>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <div style="width: 150px; overflow:auto; white-space: nowrap; text-overflow:clip">
                                    <%# Eval("Remarks") %>
                                </div>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                            <asp:TemplateField HeaderText="Approvar Remarks" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <div style="width: 150px; overflow:auto; white-space: nowrap; text-overflow:clip">
                                    <%# Eval("Approval_Remarks") %>
                                </div>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" CssClass="cssGridheaderfont"/>
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <SortedAscendingCellStyle BackColor="#FDF5AC" />
                    <SortedAscendingHeaderStyle BackColor="#4D0000" />
                    <SortedDescendingCellStyle BackColor="#FCF6C0" />
                    <SortedDescendingHeaderStyle BackColor="#820000" />
                </asp:GridView>

                                                              </td></tr>



             </table>

             </asp:Panel>


</asp:Content>

