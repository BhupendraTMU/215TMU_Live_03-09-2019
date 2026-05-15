<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="~/Faculty/Dashboard.aspx.cs" Inherits="Dashboard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">
         function SelectMachineCode(row) {
             var rowData = row.parentNode.parentNode;
             var rowIndex = rowData.rowIndex;
              window.open('../Faculty/StudentDetailsForMentor.aspx?search=' + rowData.cells[1].innerHTML);
             return false;
         }
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<fieldset class="boxBody">
 <asp:Label ID="Label1" runat="server" 
            Text="DASHBOARD" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 </fieldset>

 <fieldset class="boxBodyHeader"> 
  
 </fieldset>
     <fieldset class="boxBodyInner">

      <fieldset class="boxBodyInner">
            <table cellpadding="0px" cellspacing="0px" width="100%">                  
                <tr> 
                    <td colspan="2" style="height:10px">
                  <div onclick="General()">
                      <fieldset class="boxBodyHeader"> 
                      <asp:Label ID="Label3" runat="server" 
                        Text="General" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>              
                      </fieldset>
                  </div>
                 <br />
                 <div id="divGeneralBody">
                     <fieldset class="boxBodyInner"> 

                       <table cellpadding="0px" cellspacing="0px" width="100%"> 
                          <tr> 
                              <td colspan="2" >      
                                <table cellpadding="0px" cellspacing="0px">       
                               <tr> 
                                    <td colspan="2">
                                        <center>
              <asp:GridView ID="grdSummary" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="13" 
                  Width="1000px" GridLines="Both"   EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="250"  >
               <AlternatingRowStyle BackColor="#F7F7F7"   />
                        <Columns> 
                            <asp:TemplateField HeaderText="College Code"  ItemStyle-BorderWidth="1px" ItemStyle-BackColor="#ff9966" ItemStyle-HorizontalAlign ="Center" >
                                <ItemTemplate >
                                    <asp:Label ID="btnviewAttendancedCollegeCode" runat="server" Text='<%#Bind("CollegeCode") %> ' Font-Bold="true"  />
                                </ItemTemplate>
                            </asp:TemplateField >
                            <asp:TemplateField HeaderText="College Name" ItemStyle-BorderWidth="1px" ItemStyle-BackColor="#ffcc99" HeaderStyle-HorizontalAlign="Center" >
                                <ItemTemplate>
                                    <asp:Label ID="btnviewAttendancedCollegeName" runat="server" Text='<%#Bind("CollegeName") %> ' Font-Bold="false"  />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField  HeaderText="Principal Name" ItemStyle-BorderWidth="1px" ItemStyle-BackColor="Silver">
                                <ItemTemplate >
                                   <%-- <%# string.Concat(Eval("FirstName"), " ", Eval("LastName"))%>--%>
                                    <asp:LinkButton ID="btnviewAttendancedPrincipal" runat="server" Text='<%# string.Concat(Eval("PrincipalName"),"  =  ",Eval("Principal")) %> '  CommandArgument='<%#Bind("Principal") %> '   ForeColor="ActiveCaptionText"   />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="No of Course" ItemStyle-BorderWidth="1px" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="Silver">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnviewAttendancedNoOfCourse" runat="server" Text='<%#Bind("NoofCourse") %> '  Enabled="true"    CommandArgument='<%#Bind("CollegeCode") %> '  ForeColor="ActiveCaptionText"   />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No of Faculty" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderWidth="1px" ItemStyle-BackColor="Silver">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnviewAttendancedNoOfFaculty" runat="server" Text='<%#Bind("NoOfFaculty") %> '   CommandArgument='<%#Bind("CollegeCode") %> '  ForeColor="ActiveCaptionText"   />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No of Student" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderWidth="1px" ItemStyle-BackColor="Silver">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnviewAttendancedNoOfStudent" runat="server" Text='<%#Bind("NoOfStudent") %> '   CommandArgument='<%#Bind("CollegeCode") %> ' ForeColor="ActiveCaptionText"   />
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                           
                        </Columns>                       
                        <%--<FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />--%>
               <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7"  CssClass="cssGridheaderfont" />
              <%--  <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
               <RowStyle ForeColor="#003366" BackColor="#E7E7FF" CssClass="cssGridheaderfont"   />
               <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
               <SortedAscendingCellStyle BackColor="#F4F4FD" />
               <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
               <SortedDescendingCellStyle BackColor="#D8D8F0" />
               <SortedDescendingHeaderStyle BackColor="#3E3277" />--%>
                    </asp:GridView>
                  </center>
                                    </td> 
                                    <%--<td style="width:5px"> </td> --%>
                                 </tr>   
                               <tr> <td colspan="2" style="height:10px">------------------------ </td> </tr>

                           </table>       
                         </td>
                      </tr>
                    </table>

                   </fieldset>
                 </div>
                </td>
                </tr>
                <tr> 
                    <td colspan="15" style="height:10px">
                        <div onclick="PersonalInformation()">
                      <fieldset class="boxBodyHeader"> 
                                 <asp:Label ID="Label4" runat="server" Text="B" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
                                          </fieldset>
                        </div>
                        <div id="divPersonalInformationBody">
                          <fieldset class="boxBodyInner"> 
                   <table cellpadding="0px" cellspacing="0px"> 

                    <tr> 
                        <td colspan="2" >       
                            <table cellpadding="0px" cellspacing="0px">       
                                <tr> 
                                    <td><label style="line-height:25px">  -------</label> </td> 
                                    <td style="width:10px"> </td> 
                                </tr>
                                <tr> <td colspan="2" style="height:10px"> </td></tr>
                            </table>
                            </td>
                        </tr>
              </table>  
                      </fieldset> 
                         </div>    
                   </td>
               </tr>
               <tr> 
               <td colspan="2" style="height:10px">
                   <div id="divContact" onclick="Contact()">
                      <fieldset class="boxBodyHeader"> 
                            <asp:Label ID="Label2" runat="server" Text="C" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>              
                      </fieldset>
                   </div>
                   <div id="divContactBody">
                        <fieldset class="boxBodyInner"> 
                   <table cellpadding="0px" cellspacing="0px"> 

                    <tr> 
                        <td colspan="2" >       
                      <table cellpadding="0px" cellspacing="0px">       
                        <br />
                         <tr> 
                            <td><label style="line-height:25px">  6 </label> </td> 
                            <td style="width:10px"> </td> 
                       </tr>
                         <tr> <td colspan="2" style="height:20px">7 </td></tr>
                       </table>       
                       </td>
                       </tr>
                   </table>

             </fieldset>
                   </div>
             </td>
           </tr>   
                <tr> 
               <td colspan="2" style="height:10px">
                   <div onclick="Administration()">
                      <fieldset class="boxBodyHeader"> 
                      <asp:Label ID="Label5" runat="server" 
                        Text="D" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>              
                      </fieldset>
                  </div>
                   <div id="divAdministrationBody">
                        <fieldset class="boxBodyInner"> 
                   <table cellpadding="0px" cellspacing="0px"> 
                    <tr> 
                        <td colspan="15" >       
                        <table cellpadding="0px" cellspacing="0px">       
                             <br />
                             <tr> 
                                 <td>
                                     <label style="line-height:25px">  8 </label> 
                                 </td> 
                                 <td style="width:10px"> </td> 
                                  
                                 
                    </tr>
                            <tr> <td colspan="2" style="height:10px"> </td></tr>                      
                             
                  </table>
                            </td>
                    </tr>
               </table>
                      </fieldset>
                   </div>
               </td>
              </tr>   
            </table>

       </fieldset>       
         <div class="pull-right">
                  <%--<asp:Button ID="btnUpdate" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px" Width="90px" OnClientClick="return false"  Text="UPDATE" />--%>
         </div>
     </fieldset>  
   
</asp:Content>

