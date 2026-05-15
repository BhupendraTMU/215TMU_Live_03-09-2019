<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Supervisor_Complaint_Register1.aspx.cs" Inherits="Faculty_Supervisor_Complaint_Register1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server" Text="SUPERVISOR COMPLAINT REGISTER" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
</fieldset>
      <fieldset class="boxBody">
        <asp:Label ID="Label2" runat="server" Text="LOCATION OF COMPLAINT" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
</fieldset>
   <div id="divlocationBody">
       <fieldset class="boxBodyInner"> 

                       <table cellpadding="0px" cellspacing="0px"> 
                           <tr>
                               <td colspan="15">
                                   <table cellpadding="0px" cellspacing="0px">
                                   </table>
                                   <td>
                                       <label style="line-height: 25px">Floor Name. </label>
                                   </td>
                                   <td style="width: 10px"></td>
                                   <td>
                                       <asp:TextBox ID="txtfloorname" runat="server" Enabled="false"></asp:TextBox>
                                   </td>
                                   <td style="width: 10px"></td>
                                   <td>
                                       <label style="line-height: 25px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Ward Name. </label>
                                   </td>
                                   <td style="width: 10px"></td>
                                   <td>
                                       <asp:TextBox ID="txtwardno" runat="server" Enabled="false"></asp:TextBox>
                                   </td>
                                   <td style="width: 10px"></td>
                                   <td>
                                       <label style="line-height: 25px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;Room No.</label>
                                   </td>
                                   <td style="width: 10px"></td>
                                   <td>
                                       <asp:TextBox ID="txtroomno" runat="server" Width="200px" Enabled="false"></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtroomno" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                   </td>

                               </td>
                           </tr>
                           </table>
          
                           
       
           </fieldset>
       </div>
       
    <fieldset class="boxBody">
        <asp:Label ID="Label3" runat="server" Text="COMPLAINT" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
</fieldset>
     <br />
                 <div id="divGeneralBody">
                     <fieldset class="boxBodyInner"> 
    <table cellpadding="0px" cellspacing="0px"> 
        <tr> 
            <td colspan="15">      
                                  <table cellpadding="0px" cellspacing="0px">       
                                      <tr>
                                          
                                          <td class="auto-style12">
                                              <label style="line-height: 25px">Date Of Complaint. </label>
                                          </td>
                                          <td style="width: 10px"></td>
                                          <td class="auto-style10">
                                             <asp:TextBox ID="txtdateofcomplaint" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                          </td>
                                          <td></td>
                                          <td class="auto-style11">
                                              <label style="line-height: 25px; width: 131px;">Type of Complaint. </label>
                                          </td>
                                          <td style="width: 10px"></td>
                                          <td class="auto-style13">
                                              <asp:TextBox ID="txtcomplaint" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                          </td>
                                          <td></td>
                                          <td class="auto-style14">
                                              <label style="line-height: 25px; width: 119px;">Actual Complain.</label>
                                          </td>
                                          <td style="width: 10px"></td>
                                          <td>
                                              <asp:TextBox ID="txtactualcomplain" runat="server"  Width="200px" Enabled="false"></asp:TextBox>
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtactualcomplain" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                          </td>
                                      </tr>
                               <tr> <td colspan="11" style="height:10px"> </td></tr>                      
                               <tr>  
                                   <td class="auto-style12">                
                                        <label style="line-height:25px">  Person Solve Complain.</label>
                                   </td>
                                   <td style="width:10px"> </td>
                                   <td class="auto-style10"> 
                                       <asp:TextBox ID="txtpersonsolvecomplain" runat="server"  Width="200px"></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtpersonsolvecomplain" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                    </td> 
                                   <td> </td>
                                   <td class="auto-style11"><label style="line-height:25px"> FWD ON Date. </label> </td> 
                                   <td style="width:10px"> </td>
                                   <td class="auto-style13"> 
                                          <asp:TextBox ID="txtfwdofdate" runat="server" Width="200px" onkeydown="return false;" autocomplete="off"
                                                oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" AutoPostBack="True" OnTextChanged="txtfwdofdate_TextChanged"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtfwdofdate" Format="dd MMM yyyy"></asp:CalendarExtender>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfwdofdate" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                   </td> 
                                   <td> </td> 
                                   <td class="auto-style14"><label style="line-height:25px"> Resolved On Date </label> </td> 
                                   <td style="width:10px"> </td>
                                   <td> 
                                       <asp:TextBox ID="txtresolveddate" runat="server" Width="200px" onkeydown="return false;" autocomplete="off"
                                                oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" AutoPostBack="True" OnTextChanged="txtresolveddate_TextChanged"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtresolveddate" Format="dd MMM yyyy"></asp:CalendarExtender>

                                           
                                   </td>

                               </tr>
                               <tr> <td colspan="11" style="height:10px"> </td> </tr>
                                      <tr> 
                                    <td class="auto-style12">                
                                        <label style="line-height:25px; width: 153px;">  Sig. After Confirmation.</label>
                                    </td> 
                                   <td style="width:10px"> </td>
                                   <td class="auto-style10"> 
                                       <asp:DropDownList ID="drpsigconfirmation" runat="server" Width="200px" Height="28px">
                                           <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                           <asp:ListItem Text="PENDING" Value="1"></asp:ListItem>
                                           <asp:ListItem Text="RESOLVED" Value="2"></asp:ListItem>
                                          

                                       </asp:DropDownList>
                                    </td> 
                                   <td> </td>
                                   <td class="auto-style11"><label style="line-height:25px"> Remark.</label> </td> 
                                   <td style="width:10px"> </td>
                                   <td class="auto-style13"> 
                                          <asp:TextBox ID="txtremark" runat="server" Width="200px"></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtremark" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="odapps"></asp:RequiredFieldValidator>
                                   </td>
                                    <td> </td>
                                  
                                       
                                   </td>
                                   <td class="auto-style11"><label style="line-height:25px"> Remainder.</label> </td> 
                                   <td style="width:10px"> </td>
                                   <td class="auto-style10"> 
                                       <asp:DropDownList ID="drpremainder" runat="server" Width="200px" Height="28px">
                                           <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                           <asp:ListItem Text="One_OnDate" Value="1"></asp:ListItem>
                                           <asp:ListItem Text="Two_OnDate" Value="2"></asp:ListItem>
                                           <asp:ListItem Text="Three_OnDate" Value="3"></asp:ListItem>
                                           <asp:ListItem Text="More_Than_Three" Value="4"></asp:ListItem>
                                     
                                       </asp:DropDownList>
                                    </td> 
                                 </tr>
        
        <tr>
         <td class="auto-style11"> </td> 
                                   <td style="width:10px">  </td>
            <td class="auto-style13" style="visibility: hidden"> 
                                          <asp:TextBox ID="txtID" runat="server" Width="200px"></asp:TextBox>
                </td>
                                       </tr>
                                   </table>
    <table>
        <tr><td width="1140px"></td></tr>
        <tr class="pull-right">
            <td>
                <asp:Button ID="btnSave" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px" Width="90px" Text="Save" OnClick="btnSave_Click"/>
                </td>
            <td>
                <asp:Button runat="server" CssClass="btn-sm btn-primary btn-block" Text="Export to Excel" Height="30px" Width="120px" ID="btnexporttoexel" OnClick="btnexporttoexel_Click"/>
            </td>
        </tr>
        </table>

</fieldset>
    </div>
     <br />
    <div style="height: 250px; width: 1190px; overflow: scroll;">
          <asp:GridView ID="grdcomplainregister1" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                                        GridLines="Horizontal" EmptyDataText="There are no data records to display."
                                        AllowSorting="True" >
         
                                        <AlternatingRowStyle BackColor="#F7F7F7" />
              <Columns>
                   <asp:TemplateField HeaderText="Action">  
                    <ItemTemplate>  
                        
                        <asp:LinkButton ID="btnselect" runat="server" Text="Select" OnClick="btnselect_Click" Width="80px" />
                    </itemtemplate>  
                   
                </asp:TemplateField> 

                  <asp:TemplateField HeaderText="Sl. No.">
                      <ItemTemplate>
                          <%# Container.DataItemIndex + 1 %>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>
                   

                  <asp:TemplateField HeaderText="Complaint By">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="130px" Text='<%# Eval("Employee_Name") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Employee Code">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="130px" Text='<%# Eval("Employee_Code") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />

                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Date Of Complaint">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="130px" Text='<%# Eval("Date_Of_Complaint") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Floor Name">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="150px" Text='<%# Eval("Floor_Name") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Ward Name">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="100px" Text='<%# Eval("Ward_Name") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Room No.">
                      <ItemTemplate>

                          <asp:Label runat="server" Width="100px" Text='<%# Eval("Room_No") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Type Of Complaint">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="150px" Text='<%# Eval("Type_Of_Complaint") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>
                   <asp:TemplateField HeaderText="Actual Complaint">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="150px" Text='<%# Eval("Actual_Complaint") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>
                  
                  <asp:TemplateField HeaderText="Person Responsible To Solve Complain">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="150px" Text='<%# Eval("Person_Responsible_To_Solve_Complaint") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Complaint FWD. On Date">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="150px" Text='<%# Eval("Complaint_FWD_On_Date") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Complaint Resolved On Date">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="150px" Text='<%# Eval("Complaint_Resolved_On_Date") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Sig Of Manager After Confirmation">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="150px" Text='<%# Eval("Signature_Of_Manager_After_Confirmation") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Remark">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="150px" Text='<%# Eval("Remark") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>
                   <asp:TemplateField HeaderText="Remainder1">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="150px" Text='<%# Eval("One_OnDate") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>
                   <asp:TemplateField HeaderText="Remainder2">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="150px" Text='<%# Eval("Two_OnDate") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>
                   <asp:TemplateField HeaderText="Remainder3">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="150px" Text='<%# Eval("Three_OnDate") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>
                   <asp:TemplateField HeaderText="Remainder4">
                      <ItemTemplate>
                          <asp:Label runat="server" Width="150px" Text='<%# Eval("More_Than_Three") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="7%" />
                  </asp:TemplateField>
                 <asp:TemplateField HeaderText="Option to Download Photo">
                    <ItemTemplate>
                     <%--   <asp:Label runat="server" Width="100px" Text='<%# Eval("Upload_Photo") %>'></asp:Label>--%>
                        <asp:LinkButton ID="lnkPhoto" runat="server" OnClick="lnkPhoto_Click" ForeColor="Red" Font-Underline="true" Enabled='<%# Eval("Upload_Photo").ToString() == "0" ? false : true %>'> View Photo</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="7%" />
                </asp:TemplateField>

                  
              </Columns>
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                        <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                        <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
                                        <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                        <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                        <SortedDescendingHeaderStyle BackColor="#3E3277" />
              
                                            </asp:GridView>
        </div>
                       
       
                       


                         </table>

                       
       
                       


</asp:Content>

