<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="FacultyCoursePlan.aspx.cs" Inherits="Faculty_FacultyCoursePlan" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">  
    <script type="text/javascript">
        function preventBackspace(e) {
            var evt = e || window.event;
            if (evt) {
                var keyCode = evt.charCode || evt.keyCode;
                if (keyCode === 8) {
                    if (evt.preventDefault) {
                        evt.preventDefault();
                    } else {
                        evt.returnValue = false;
                    }
                }
            }
        }
        function myFunction() {
            $('[id$=txtTopic]').keypress(function (e) {
                if ($(this).val().length >= 250) {
                    e.preventDefault();
                }
            });
        }
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
        function AppliedCoursePlan() {
            $('[id$=pnlAppliedCoursePlan]').slideToggle();
            return false;
        }
        $(document).ready(function () {
            $('[id$=pnlAppliedCoursePlan]').fadeOut();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <fieldset class="boxBody">
         <asp:Label ID="Label1" runat="server" 
            Text="Course Plan" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>          
 </fieldset>
     <fieldset class="boxBodyHeader">   
    </fieldset>
     <fieldset class="boxBodyInner"> 
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
           <fieldset class="boxBodyInner"> 
               <asp:Panel  id="pnlHeader" runat="server" BorderWidth="1px">     
            <table cellpadding="0px" cellspacing="10px" width="100%">                
               <caption>
                  <br />                                     
                  <tr> 
                  
                      <td><label>&nbsp Academic Year </label> </td> 
                      <td style="width:10px"> </td>
                      <td > 
                         <asp:DropDownList ID="drpAcademicYear" Width="200px" Height="20px" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="drpAcademicYear_SelectedIndexChanged"></asp:DropDownList>
                      </td> 
                      <td style="width:20px"> </td> 
                       <td>
                           <label> Course </label>
                       </td>
                       <td style="width:10px"></td>
                       <td>
                           <asp:DropDownList ID="drpCourse" runat="server" AutoPostBack="true" Height="20px" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged" Width="200px"></asp:DropDownList>
                       </td>
                       <td style="width:20px"></td>
                       <td>
                           <label> Semester/Year </label>
                       </td>
                       <td style="width:10px"></td>
                       <td>
                           <asp:DropDownList ID="drpSemester" runat="server" AutoPostBack="true" Height="20px" OnSelectedIndexChanged="drpSemester_SelectedIndexChanged" Width="200px">
                           </asp:DropDownList>
                       </td>
                       <td style="width:10px"></td>
                   
                   </tr>
                  <tr>
                       <td colspan="2"></td>
                       <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="h1" Display="Dynamic" ControlToValidate="drpAcademicYear" InitialValue="" ErrorMessage="please select Academic Year!" ForeColor="Red" ></asp:RequiredFieldValidator>
                       </td>
                       <td colspan="3"></td>
                       <td>                           
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpCourse" Display="Dynamic" ErrorMessage="please select Course!" ForeColor="Red" InitialValue="" ValidationGroup="h1"></asp:RequiredFieldValidator>
                       </td>
                       <td colspan="3"></td>
                       <td>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="drpSemester" Display="Dynamic" ErrorMessage="please select Semester!" ForeColor="Red" InitialValue="" ValidationGroup="h1"></asp:RequiredFieldValidator>                          
                       </td>
                   </tr>     
                  <tr><td colspan="11" style="height:10px"></td></tr>
                  <tr>
                        <td>
                           <label>&nbsp Section  </label>
                       </td>
                       <td style="width:10px"></td>
                       <td>
                           <asp:DropDownList ID="drpSection" runat="server" Height="20px" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="drpSection_SelectedIndexChanged">
                           </asp:DropDownList>
                       </td>
                      <td style="width:20px"> </td> 
                      <td>
                          <label> Group </label> 

                      </td>
                      <td style="width:10px"> 
                      </td> 
                      <td>
                      <asp:DropDownList ID="ddlGroup" Width="200px" Height="20px" AutoPostBack="true" runat="server"  ></asp:DropDownList>                        
                      </td> 
                      <td style="width:20px"> </td> 
                      <td>
                          <label> Batch </label> 
                      </td> 
                      <td style="width:10px"> </td>
                      <td> 
                           <asp:DropDownList ID="ddlBatch" Width="200px" Height="20px" AutoPostBack="true" runat="server"  ></asp:DropDownList>     

                      </td> 
                      <td style="width:10px"> </td> 
                     
                  </tr>
                  <tr>
                       <td colspan="6"></td>
                       <td>                       
                            
                       </td>
                       <td colspan="4">
                           <asp:HiddenField runat="server" ID="hfDocumentNo" />
                       </td>
                   </tr>
                    <tr><td colspan="11" style="height:10px"></td></tr>
                  <tr>                        
                      <td>
                          <label> &nbsp Subject </label> 

                      </td>
                      <td style="width:10px"> 
                      </td> 
                      <td>
                            <asp:DropDownList ID="drpSubject" Width="200px" Height="20px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="drpSubject_SelectedIndexChanged" ></asp:DropDownList> 
                      </td> 
                      <td style="width:20px"> </td> 
                      <td>
                          <label> Subject Type </label> 
                      </td> 
                      <td style="width:10px"> </td>
                      <td> 
                         <asp:TextBox ID="txtSubjectType" Width="200px" Height="20px" Enabled="false" runat="server"></asp:TextBox>                      
                      </td> 
                      <td style="width:10px"> </td> 
                      <td>
                         <asp:TextBox ID="txtHOD" Width="150px" Height="22px" Visible="false" placeholder="HOD Code" MaxLength="8" runat="server"></asp:TextBox>
                       </td>
                       <td style="width:10px"></td>
                       <td>
                       <asp:Button ID="btnShow" runat="server" Text="Show Data" class="btn-info" Width="100px" OnClick="btnShow_Click" ValidationGroup="h1" />                      
                       &nbsp&nbsp
                       <asp:Button ID="btnRefresh" runat="server" Text="Refresh" class="btn-info" Width="100px" OnClick="btnRefresh_Click" />                      
                      
                       </td>
                      <td style="width:10px"> </td> 
                  </tr>
                    <tr>
                       <td colspan="2"></td>
                       <td>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="h1" Display="Dynamic" ControlToValidate="drpSubject" InitialValue="" ErrorMessage="please select Subject!" ForeColor="Red" ></asp:RequiredFieldValidator>
                       </td>
                       <td colspan="3"></td>
                       <td>                           
                          
                       </td>
                       <td></td>
                        <td>
                               
                        </td>
                        <td></td>
                       <td>
                          
                       </td>
                   </tr>     
                   </caption>                 
                 </table>
                   <br />
               </asp:Panel>
               <asp:Panel ID="pnlunit" runat="server">
                   <table cellpadding="0px" cellspacing="0px" width="100%">                
                   <tr><td colspan="11" style="height:10px"></td></tr>
                   <tr> 
                           <td>
                              <label> Unit Code </label> </td> 
                          <td style="width:10px"> </td> 
                          <td>
                               <asp:DropDownList ID="drpUnitCode" Width="200px" Height="20px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="drpUnitCode_SelectedIndexChanged" ></asp:DropDownList> 
                          </td> 
                          <td style="width:20px"> </td> 
                          <td><label>Unit Name </label> </td>
                          <td style="width:10px"> </td> 
                          <td>
                            <asp:TextBox runat="server" ID="txtUnitName" Width="200px" Height="20px" Enabled="false"></asp:TextBox>
                          </td> 
                          <td style="width:10px"> </td>                     
                          <td>
                              Topic
                          </td>
                          <td></td>
                      <td rowspan="4">
                      <asp:TextBox ID="txtTopic" runat="server" MaxLength="80" Width="100%" Height="60px" onkeypress="myFunction()" TextMode="MultiLine"></asp:TextBox>                       
                       </td>
                          <td style="width:10px"></td> 
                  </tr>
                  <tr> 
                       <td colspan="2"></td>
                       <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpUnitCode" InitialValue="" ErrorMessage="please select Unit Code!" ForeColor="Red" ></asp:RequiredFieldValidator>
                       </td>
                       <td colspan="3"></td>
                       <td></td>
                       <td colspan="2"></td>
                       <td>
                             &nbsp;</td>
                      <td></td>
                  </tr>
                  <tr><td colspan="10" style="height:2px"></td><td></td></tr>
                  <tr>
                      <td>
                           <label> Chapter Code  </label>
                       </td>
                       <td style="width:10px"></td>
                       <td>
                           <asp:DropDownList ID="drpChapterCode" Width="200px" Height="20px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="drpChapterCode_SelectedIndexChanged" ></asp:DropDownList> 
                       </td>
                      <td style="width:20px"> </td> 
                      <td>
                          <label> Chapter Name </label> 
                      </td>
                      <td style="width:10px"></td> 
                      <td>
                          <asp:TextBox ID="txtChapterName"  runat="server"  Width="200px"  Height="22px" Enabled="false"></asp:TextBox>
                      </td> 
                      <td style="width:20px"> </td> 
                      <td>&nbsp;</td> 
                      <td style="width:10px"> </td>
                      <td > 
                          &nbsp;</td>
                  </tr>
                  <tr>
                       <td colspan="2"></td>
                       <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpChapterCode" InitialValue="" ErrorMessage="please select Chapter Code!" ForeColor="Red" ></asp:RequiredFieldValidator>
                       </td>
                       <td colspan="7"></td>
                       <td>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtTopic" InitialValue="" ErrorMessage="please input the Topic!" ForeColor="Red" ></asp:RequiredFieldValidator></td>

                   </tr> 
                   <tr><td colspan="11" style="height:10px"></td></tr>  
                   <tr>
                       <td>
                           <label>Lecture No.</label>
                       </td>
                       <td></td>
                       <td>
                            <asp:TextBox ID="txtPeriod"  runat="server"  Width="200px"  MaxLength="5" Height="22px"></asp:TextBox>
                       </td>
                       <td></td>
                       <td>
                           &nbsp;</td>
                       <td></td>
                       <td colspan="3">
                           &nbsp;</td> 
                      
                       <td></td>
                       <td>
                           <asp:Button ID="btnSave" runat="server" Text="Add" class="btn-info" Width="100px" OnClick="btnSave_Click" ValidationGroup="g1"/>                      
                       </td>                      
                   </tr>  
                   <tr>
                       <td colspan="2"></td>
                       <td>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtPeriod" InitialValue="" ErrorMessage="please input the Period!" ForeColor="Red" ></asp:RequiredFieldValidator>
                           <asp:FilteredTextBoxExtender FilterType="Numbers" runat="server" TargetControlID="txtPeriod"></asp:FilteredTextBoxExtender>
                       </td>
                       <td colspan="3"></td>
                       <td colspan="4">
                             
                       </td>                       
                       <td>
                            
                       </td>
                   </tr>   
                   <tr><td colspan="11" style="height:10px"></td></tr>  
                   <tr>
                       <td>
                           
                       </td>
                       <td></td>
                       <td colspan="5">
                           &nbsp;</td>
                        <td colspan="3"></td>
                      <td > 
                       
                      </td>
                   </tr>
                   <tr>
                       <td colspan="2"></td>
                       <td>
                            
                       </td>
                       <td colspan="7"></td>
                       <td>
                            &nbsp;</td>
                   </tr>
             </table>
                   </asp:Panel> 
               <br />
                <asp:GridView ID="grdFacultyCoursePlan" OnRowCommand="grdFacultyCoursePlan_RowCommand" runat="server"  AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"  BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" 
                    GridLines="Both" 
                    EmptyDataText="There are no data records to display." OnRowDataBound="grdFacultyCoursePlan_RowDataBound" >
                                   <AlternatingRowStyle BackColor="#F7F7F7" />  
                                         <Columns>             
                                            <asp:BoundField DataField="Unit Code" HeaderText="Unit Code" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="Unit Name" HeaderText="Unit Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                             <asp:BoundField DataField="Chapter Code" HeaderText="Chapter Code" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                             <asp:BoundField DataField="Chapter Name" HeaderText="Chapter Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                               <asp:TemplateField HeaderText="Topic" ItemStyle-HorizontalAlign="Left">
                                                  <ItemTemplate>
                                                        <div style="width: 150px; overflow:auto; white-space: nowrap; text-overflow:clip">
                                                               <%# Eval("Topics") %>
                                                        </div>
                                                  </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                               </asp:TemplateField>
                                             <asp:BoundField DataField="Period" HeaderText="Lecture No" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />                                             
                                             <%--<asp:BoundField DataField="No Of Minuites" HeaderText="No. Of Minuites" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                             <asp:BoundField DataField="Scheduled Date" HeaderText="Scheduled Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />--%>
                                             <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Left">
                                                  <ItemTemplate>
                                                        <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                  </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                               </asp:TemplateField>                                             
                                             <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="imgdelete" runat="server" CommandName="DeleteItem" Text="Delete" ImageUrl="~/logo/close.png" Width="20px" Height="20px"
                                                       OnClientClick="return confirm('Are you sure you want to delete this Item?');" />
                                                    <asp:HiddenField ID="hfLineNo" runat="server" Value='<%# Bind("LineNo") %>' />
                                                </ItemTemplate>
                                             </asp:TemplateField>
                                           
                                          </Columns>                     
                                   <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                               <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                               <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                               <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont"   />
                               <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
                               <SortedAscendingCellStyle BackColor="#F4F4FD" />
                               <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                               <SortedDescendingCellStyle BackColor="#D8D8F0" />
                               <SortedDescendingHeaderStyle BackColor="#3E3277" />
                </asp:GridView>
               <br />
               <div class="pull-right">
                   <asp:Button ID="btnSendForApproval" runat="server" Text="Send for Approval" Visible="false" class="btn-info" Width="200px" OnClick="btnSendForApproval_Click" ValidationGroup="g2"/>
               </div>

                  <div id="divContact" onclick="AppliedCoursePlan()">
                      <fieldset class="boxBodyHeader"> 
                            <asp:Label ID="Label2" runat="server" Text="Applied" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>              
                      </fieldset>
                   </div>
                   <asp:Panel runat="server" ID="pnlAppliedCoursePlan" >
                           <asp:GridView ID="grdAppliedCoursePlan" OnRowCommand="grdFacultyCoursePlan_RowCommand" runat="server"  AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"  BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" GridLines="Horizontal" EmptyDataText="There are no data records to display." OnSelectedIndexChanged="grdAppliedCoursePlan_SelectedIndexChanged" >
                                           <AlternatingRowStyle BackColor="#F7F7F7" />  
                                                 <Columns>             
                                                     <asp:BoundField DataField="No_" HeaderText="Document No" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                     <asp:BoundField DataField="Academic Year" HeaderText="Academic Year" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                     <asp:BoundField DataField="Course Code" HeaderText="Course Code" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                     <asp:BoundField DataField="Subject Code" HeaderText="Subject" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                     <asp:BoundField DataField="Subject Type" HeaderText="Subject Type" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                     <asp:BoundField DataField="SemesterYear" HeaderText="Semester/Year" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                     <asp:BoundField DataField="Section" HeaderText="Section" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                     <asp:BoundField DataField="Group" HeaderText="Group" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                     <asp:BoundField DataField="Batch" HeaderText="Batch" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />                                                     
                                                      <asp:BoundField DataField="PStatus" HeaderText="Plan Status" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />                                                     
                                                     <asp:ButtonField  Text="Select" ButtonType="Link"  ControlStyle-ForeColor="Orange"  CommandName="Select" HeaderText="Select" />                                         
                                                  </Columns>                     
                                           <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                       <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                                       <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                       <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont"   />
                                       <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
                                       <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                       <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                       <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                       <SortedDescendingHeaderStyle BackColor="#3E3277" />
                        </asp:GridView>
                  </asp:Panel>
           </fieldset>
           </ContentTemplate>
        </asp:UpdatePanel>
      </fieldset>
</asp:Content>

