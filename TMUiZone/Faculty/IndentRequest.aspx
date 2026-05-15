<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="IndentRequest.aspx.cs" Inherits="Faculty_IndentRequest" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">  
    <script type="text/javascript">
        function callFeedbackMessage(inputType, inputText) {

            if (inputType == 'Error') {
                alertify.error(inputText);
                return false;
            }
            else if (inputType == 'Success') {
                alertify.success("Send Successfully");
                return false;
            }
            else {
                alertify.log(inputText, "", 10000);
                return false;
            }
        }        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
     <fieldset class="boxBody">
         <asp:Label ID="Label1" runat="server" 
            Text="Indent Request" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label> 
       &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
          <asp:Label ID="lblRequestError" runat="server" ForeColor="Red"></asp:Label>                
 </fieldset>
     <fieldset class="boxBodyHeader">   
    </fieldset>
     <fieldset class="boxBodyInner"> 
     
           <fieldset class="boxBodyInner"> 
               <asp:Panel  id="pnlHeader" runat="server" BorderWidth="1px">     
                      <table cellpadding="0px" cellspacing="10px" width="100%">                
               <caption>
                   <br /> 
                   
                  <tr> 
                  
                      <td  colspan="2">&nbsp Indent For : </td> 
                      
                      <td colspan="3" > 
                        <asp:RadioButtonList id="rblDepartment" runat="server" RepeatDirection="Horizontal" Width="200px">
                        <asp:ListItem Value="2" Selected="True">Self</asp:ListItem>
                        <asp:ListItem Value="1">Department</asp:ListItem>
                        </asp:RadioButtonList>
                         </td>
                       <td colspan="2" >
                          <asp:DropDownList runat="server" ID="ddlDepartment">

                          </asp:DropDownList>
                              
                      </td> 
                      
                       <td style="width:20px">Item</td>
                       <td colspan="3">
                             <asp:TextBox ID="txtItemToPurchase" Width="400px" Height="20px"  runat="server" OnTextChanged="txtItemToPurchase_TextChanged"  AutoPostBack="true"></asp:TextBox>                      
                           <asp:AutoCompleteExtender ID="aceItem" runat="server" ServiceMethod="SearchItems" MinimumPrefixLength="1" 
                    CompletionInterval="10" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtItemToPurchase"  FirstRowSelected="false"></asp:AutoCompleteExtender>
                       </td>                       
                   
                   </tr>
                  <tr>
                       <td colspan="2"></td>
                       <td>
                       
                       </td>
                       <td colspan="3"></td>
                       <td>                           
                          
                       </td>
                       <td colspan="3"> <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtItemToPurchase" ErrorMessage="  * Can't be blank" ForeColor="Red" SetFocusOnError="True" ValidationGroup="userrequest"></asp:RequiredFieldValidator></td>
                       <td>
                          
                       </td>
                   </tr>     
                  <tr><td colspan="11" style="height:3px"></td></tr>
                  <tr>
                      <td colspan="2">&nbsp Variance:   </td>                      
                      <td> <asp:DropDownList ID="ddlVariance" runat="server" Height="20px" Width="200px" >  </asp:DropDownList> </td>
                      <td> Quantity           </td>                        
                      <td> <asp:TextBox ID="txtQty" runat="server" Width="50px" Height="20px"></asp:TextBox> </td> 
                      <td style="width:20px"><asp:Label ID="lblUOM" runat="server"></asp:Label> </td>
                       <td style="width:20px"> </td>  
                      <td> Remarks            </td> 
                      <td style="width:10px" colspan="3">
                          <asp:TextBox ID="txtRemarks" runat="server" TextMode="SingleLine" width="400px" MaxLength="50" Height="20px"></asp:TextBox></td>   
                      <td> <asp:Button ID="btnADD" runat="server" Text="ADD" class="btn-info" Width="90px" OnClick="btnADD_Click" ValidationGroup="h1" /> </td>
                     
                  </tr>
                  <tr>
                       <td colspan="3"></td>
                       <td colspan="3">                       
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtQty" ErrorMessage="  * Can't be blank" ForeColor="Red" SetFocusOnError="True" ValidationGroup="userrequest"></asp:RequiredFieldValidator>
                         
                       </td>
                      <td></td>
                       <td colspan="4">
                          <asp:FilteredTextBoxExtender ID="fteQty" runat="server" TargetControlID="txtQty"  FilterType="Numbers, Custom"   ValidChars="." />
                       </td>
                   </tr> 
                   <%--<tr >
                       <td style="height:1px;"></td>
                       <td colspan="8" style="height:1px; background-color:gray"> </td>
                       <td style="height:1px;" colspan="2"></td>
                        
                   </tr> --%>
                                     
                   </caption>                 
                 </table>
            <br />
                <center>
                     <asp:GridView ID="grdAddItem" runat="server" AutoGenerateColumns="False" 
             CellPadding="4" ForeColor="#333333" GridLines="None" Width="90%" AllowPaging="True" PageSize="15" OnRowCommand="grdAddItem_RowCommand" >
             <AlternatingRowStyle BackColor="White" />
             <Columns>
               
                 <asp:BoundField DataField="ItemDescription" HeaderText="Item Description" />
                 <asp:BoundField DataField="ItemCode" HeaderText="Item Code"  />
                 <asp:BoundField DataField="Variance" HeaderText="Variance" /> 
                 <asp:BoundField DataField="Qty" HeaderText="Quantity" />                             
                 <asp:BoundField DataField="UOM" HeaderText="UOM" />                 
                 <asp:BoundField DataField="Remarks" HeaderText="Remarks" />                 
                 <asp:TemplateField HeaderText="Delete">
                     <ItemTemplate>
                         <asp:ImageButton ID="imgdelete" runat="server" CommandName="DeleteItem" Text="Delete" ImageUrl="~/logo/close.png" Width="20px" Height="20px"
                             OnClientClick="return confirm('Are you sure you want to delete this Item?');" />
                     </ItemTemplate>
                 </asp:TemplateField>
             </Columns>
             <EditRowStyle BackColor="#2461BF" />
                      <EmptyDataTemplate>
                          There are no records found
                      </EmptyDataTemplate>
             <FooterStyle BackColor="#C5122F" ForeColor="White" Font-Bold="True" />
             <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="White" CssClass="cssGridheaderfont" HorizontalAlign="Left"/>
             <PagerStyle BackColor="#C5122F" ForeColor="White" HorizontalAlign="Left" />
             <RowStyle BackColor="#EFF3FB" CssClass="cssGridheaderfont" HorizontalAlign="Left" />
             <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
             <SortedAscendingCellStyle BackColor="#F5F7FB" />
             <SortedAscendingHeaderStyle BackColor="#6D95E1" />
             <SortedDescendingCellStyle BackColor="#E9EBEF" />
             <SortedDescendingHeaderStyle BackColor="#4870BE" />
         </asp:GridView>
                    </center>
                    <div class="pull-right">
                   <asp:Button ID="btnSendForApproval" runat="server" Text="Send for Approval" Visible="false" class="btn-info" Width="200px" OnClick="btnSendForApproval_Click" ValidationGroup="g2"/>
               </div>
               <br />
               </asp:Panel>
                  <div id="divContact">
                      <fieldset class="boxBodyHeader"> 
                            <asp:Label ID="Label2" runat="server" Text="Applied" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>              
                      </fieldset>
                   </div>
                   <asp:Panel runat="server" ID="pnlAppliedIndent" >
                         <center>
                     <asp:GridView ID="grdAppliedIndent" runat="server" AutoGenerateColumns="False" 
             CellPadding="4" ForeColor="#333333" GridLines="None" Width="90%" AllowPaging="True" PageSize="15" OnRowCommand="grdAddItem_RowCommand" OnPageIndexChanging="grdAppliedIndent_PageIndexChanging" >
             <AlternatingRowStyle BackColor="White" />
             <Columns>
               
                 <asp:BoundField DataField="No_" HeaderText="Request No" />
                 <asp:BoundField DataField="Issue For" HeaderText="Issue For"  />
                 <asp:BoundField DataField="Issue Date" HeaderText="Indent Date" /> 
                 <asp:BoundField DataField="Issue Id" HeaderText="Issue Id" />                             
                 <asp:BoundField DataField="Issue Name" HeaderText="Issue Name" />  
             </Columns>
             <EditRowStyle BackColor="#2461BF" />
                      <EmptyDataTemplate>
                          There are no records found
                      </EmptyDataTemplate>
             <FooterStyle BackColor="#C5122F" ForeColor="White" Font-Bold="True" />
             <HeaderStyle BackColor="#C5122F" Font-Bold="True" ForeColor="White" CssClass="cssGridheaderfont" HorizontalAlign="Left"/>
             <PagerStyle BackColor="#C5122F" ForeColor="White" HorizontalAlign="Left" />
             <RowStyle BackColor="#EFF3FB" CssClass="cssGridheaderfont" HorizontalAlign="Left" />
             <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
             <SortedAscendingCellStyle BackColor="#F5F7FB" />
             <SortedAscendingHeaderStyle BackColor="#6D95E1" />
             <SortedDescendingCellStyle BackColor="#E9EBEF" />
             <SortedDescendingHeaderStyle BackColor="#4870BE" />
         </asp:GridView>
                    </center>
                  </asp:Panel>
           </fieldset>
           </ContentTemplate>
        </asp:UpdatePanel>      
</asp:Content>

