<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="StudentAttendanceMarkEdit.aspx.cs" Inherits="Faculty_StudentAttendanceMarkEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
     .redBorder {
            border:1px solid red;
        }
        </style>
    <script>
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
        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select greater than current date!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
    </script>
    <script>
        function CheckOne(me) {
            $('[id$=chkPresentAll]').attr('checked', false);
            $('[id$=chkAbsentAll]').attr('checked', false);
            me.checked = true;
        }      
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
 <asp:Label ID="Label1" runat="server" 
            Text="Mark Attendance" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 </fieldset>
    <fieldset class="boxBodyHeader"> 
  
 </fieldset>
     <fieldset class="boxBodyInner"> 
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                         <ContentTemplate>
                               <fieldset class="boxBodyInner">
    <table cellpadding="0px" cellspacing="0px">  
     <tr> <td colspan="15" style="height:10px">
         
          </fieldset>
          <fieldset class="boxBodyInner"> 
                 <table cellpadding="0px" cellspacing="0px">                
               <caption>
                   <br />
                   <tr>
                       <td>
                           <label>
                           No.
                           </label>
                       </td>
                       <td style="width:10px"></td>
                       <td>
                           <asp:TextBox ID="lblNo" runat="server" Height="20px" Enabled="false" Width="220px"></asp:TextBox>
                       </td>
                       <td style="width:20px"></td>
                       <td>
                           <label>
                           Faculty Code
                           </label>
                       </td>
                       <td style="width:10px"></td>
                       <td>
                           <asp:TextBox ID="lblFacultyCode" runat="server" Height="20px" Enabled="False" Width="220px"></asp:TextBox>
                       </td>
                       <td style="width:20px"></td>
                       <td>
                           <label>
                           Date
                           </label>
                       </td>
                       <td style="width:10px"></td>
                       <td>
                           <asp:TextBox ID="txtDate" Enabled="false" runat="server"  Width="220px"  Height="22px" onkeypress="return false" onKeyDown="preventBackspace();" OnTextChanged="txtDate_TextChanged"
                                                         AutoPostBack="true"   oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                               <asp:CalendarExtender ID="CalendarExtender1" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDate"
                                                    CssClass="cal_Theme1" Enabled="true" TargetControlID="txtDate"></asp:CalendarExtender>
                       </td>
                       <td></td>
                   </tr>
                   <tr> <td colspan="12" style="height:10px"></td></tr>
                   <tr>  
                      <td>Academic Year  </td> 
                      <td style="width:10px"> </td>
                      <td > 
                         <asp:DropDownList ID="drpAcademicYear" Width="150px" Height="20px" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="drpAcademicYear_SelectedIndexChanged"></asp:DropDownList>
                      </td> 
                      <td style="width:20px"> </td> 
                       <td>
                           Course 
                       </td>
                       <td style="width:10px"></td>
                       <td>
                           <asp:DropDownList ID="drpCourse" runat="server" AutoPostBack="true" Height="20px" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged" Width="150px">
                           </asp:DropDownList>
                       </td>
                       <td style="width:20px"></td>
                       <td>
                           Semester/Year 
                       </td>
                       <td style="width:10px"></td>
                       <td>
                           <asp:DropDownList ID="drpSemester" runat="server" AutoPostBack="true" Height="20px" OnSelectedIndexChanged="drpSemester_SelectedIndexChanged" Width="150px">
                           </asp:DropDownList>
                       </td>
                       <td style="width:10px"></td>
                   
                   </tr>
                   <tr>
                       <td colspan="2"></td>
                       <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpAcademicYear" InitialValue="" ErrorMessage="please select Academic Year!" ForeColor="Red" ></asp:RequiredFieldValidator>
                       </td>
                       <td colspan="3"></td>
                       <td>                           
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpCourse" Display="Dynamic" ErrorMessage="please select Course!" ForeColor="Red" InitialValue="" ValidationGroup="g1"></asp:RequiredFieldValidator>
                       </td>
                       <td colspan="3"></td>
                       <td>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpSemester" Display="Dynamic" ErrorMessage="please select Semester!" ForeColor="Red" InitialValue="-- Semester --" ValidationGroup="g1"></asp:RequiredFieldValidator>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="drpSemester" Display="Dynamic" ErrorMessage="please select Semester!" ForeColor="Red" InitialValue="" ValidationGroup="g1"></asp:RequiredFieldValidator>                          
                       </td>
                       <td></td>
                   </tr>
                    <tr><td colspan="12" style="height:10px"></td></tr>
                  <tr> 
                       <td> Section </td> 
                      <td style="width:10px"> </td> 
                      <td>
                         <asp:DropDownList ID="drpSection" runat="server" Height="20px" Width="150px">
                           </asp:DropDownList> 
                      </td> 
                      <td style="width:10px"> </td> 
                      <td>Group</td>
                      <td style="width:10px"> </td> 
                      <td>
                         <asp:DropDownList ID="ddlGroup" Width="150px" Height="20px" runat="server" ></asp:DropDownList> 
                      </td> 
                      <td style="width:10px"> </td>                     
                      <td>Batch</td> 
                      <td style="width:10px"> </td>
                      <td > 
                         <asp:DropDownList ID="ddlBatch" Width="150px" Height="20px" AutoPostBack="true" runat="server"  ></asp:DropDownList>
                      </td> 
                         <td style="width:10px"> </td> 
                </tr>
                  <tr> 
                   <td colspan="2"></td>
                   <td>
                        
                   </td>
                  <td colspan="3"></td>
                  <td>
                       
                  </td>
                  <td colspan="3"></td>
                  <td>
                      <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtTopic" InitialValue="" ErrorMessage="please input the Topic!" ForeColor="Red" ></asp:RequiredFieldValidator>--%>
                  </td>
                      <td></td>
           </tr>    
                    <tr> <td colspan="12" style="height:10px"> </td></tr>
                  <tr> 
                      <td>
                          Subject </td> <td style="width:10px"> 
                      </td> 
                      <td>
                            <asp:DropDownList ID="drpSubject" Width="150px" Height="20px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="drpSubject_SelectedIndexChanged"></asp:DropDownList> 
                      </td> 
                      <td style="width:10px"> </td> 
                      <td>
                           Lecture
                      </td> 
                      <td style="width:10px"> </td>
                      <td> 
                          <asp:DropDownList ID="drpLecture" Width="150px" Height="20px" AutoPostBack="true" runat="server" ></asp:DropDownList>   
                      </td> 
                      <td style="width:10px"> </td> 
                      <td>Unit</td>
                      <td></td>
                      <td> <asp:DropDownList ID="drpUnit" Width="150px" Height="20px" runat="server" ></asp:DropDownList> </td>
                     <td></td>
                 </tr>
                  <tr>
                      <td colspan="2"> </td>
                      
                    <td colspan="2"><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpSubject" InitialValue="" ErrorMessage="please select Subject!" ForeColor="Red" ></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="drpSubject" Display="Dynamic" ErrorMessage="please select Subject!" ForeColor="Red" InitialValue="-- Subject --" ValidationGroup="g1"></asp:RequiredFieldValidator>
                    </td>
                     <td colspan="2"> </td>                   
                   <td colspan="2">
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpLecture" InitialValue="" ErrorMessage="please select Lecture!" ForeColor="Red" ></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="drpLecture" Display="Dynamic" ErrorMessage="please select Lecture!" ForeColor="Red" InitialValue="-- Lecture --" ValidationGroup="g1"></asp:RequiredFieldValidator>
                   </td>
                      <td colspan="2"> </td>
                      <td colspan="2">                       
                        <asp:RequiredFieldValidator ID="rfvUnit" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpUnit" InitialValue="" ErrorMessage="please select Unit!" ForeColor="Red" ></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvddlUnit" runat="server" ControlToValidate="drpUnit" Display="Dynamic" ErrorMessage="please select Unit!" ForeColor="Red" InitialValue="-- Unit --" ValidationGroup="g1"></asp:RequiredFieldValidator>
                   </td>
                </tr>
                 <tr><td colspan="12" style="height:10px"></td></tr>
                  <tr> 
                       <td>
                         Subject Type</td> 
                      <td style="width:10px"> </td> 
                      <td>
                          <asp:TextBox ID="lblSubjectType" Width="220px" Height="20px" Enabled="false" runat="server"></asp:TextBox>
                      </td> 
                      <td style="width:10px"> </td> 
                      <td>Topic </td>
                      <td style="width:10px"> </td> 
                      <td>
                        <asp:TextBox ID="txtTopic" Width="220px" MaxLength="20"  Height="20px" runat="server" Text=""></asp:TextBox>
                      </td> 
                      <td style="width:10px"> </td>                     
                      <td><label> Extra Classes </label></td> 
                      <td style="width:10px"> </td>
                      <td > 
                         <asp:CheckBox ID="chkBoxExtraClass"  runat="server" />
                      </td> 
                         <td style="width:10px"> </td> 
                </tr>
                  <tr> 
                   <td colspan="2"></td>
                   <td>
                        
                   </td>
                  <td colspan="3"></td>
                  <td>
                       
                  </td>
                  <td colspan="4"></td>
                  <td>
                      <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtTopic" InitialValue="" ErrorMessage="please input the Topic!" ForeColor="Red" ></asp:RequiredFieldValidator>--%>
                  </td>
           </tr>
        </table>
      
      
        <br />

         <asp:Button ID="btnShow" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1"   Height="30px" Width="90px" OnClick="btnShow_Click"  Text="SHOW" />
             
          </fieldset>

                              <asp:Panel runat="server" ID="pnlCheckBox" Visible="false">
         <table>
             <tr style="height:10px"><td></td></tr>
             <tr> <td><label> Present All </label> </td> <td style="width:10px"> </td> <td>
         <asp:CheckBox ID="chkPresentAll" runat="server" Checked="true" onClick="CheckOne(this)" AutoPostBack="true" OnCheckedChanged="chkPresentAll_CheckedChanged"/> </td> <td style="width:10px"> </td> 
           <td><label> Absent All </label> </td> <td style="width:10px"> </td><td> 
        <asp:CheckBox ID="chkAbsentAll" runat="server" onClick="CheckOne(this)" AutoPostBack="true" OnCheckedChanged="chkAbsentAll_CheckedChanged"/> </td> 
           </tr>
         </table>
                  </asp:Panel>

           </td></tr>
<center>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblMessage" runat="server" Visible="false" ForeColor="Red"  Text="<u>You have already mark the Attendance of this lecture..</u>"></asp:Label>
            </td>
        </tr>

                             <tr>
                                 
                                 <td align="center">
                         <br />
                                     <div class="center-block">
                                     <center>
                         <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both" Height="400" Width="100%" Visible="false">
              
              <asp:GridView ID="grdAttendanceDetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal"   EmptyDataText="There are no data records to display." >
               <AlternatingRowStyle BackColor="#F7F7F7" />
                        <Columns>   
                                                   
                            <asp:BoundField DataField="Enrollment No_" Visible="false" HeaderText="Roll No" SortExpression="ApplicantName"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Student Name" HeaderText="Student Name" SortExpression="ApplicantName"  HeaderStyle-CssClass="visible-lg" DataFormatString="{0:N2}" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="No_" HeaderText="Student No" SortExpression="ApplicantName"  HeaderStyle-CssClass="visible-lg" DataFormatString="{0:N2}" ItemStyle-CssClass="visible-lg" />
                            <asp:TemplateField>
                            <ItemTemplate >
                                <asp:CheckBox ID="chkboxAttendance"  ClientIDMode="Static" HeaderText="Attendance" Checked="true" runat="server" />                                
                            </ItemTemplate>
                                <HeaderTemplate>Attendance</HeaderTemplate>
                            </asp:TemplateField>  
                          </Columns>                       
                               <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
               <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
               <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
               <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont"   />
               <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
               <SortedAscendingCellStyle BackColor="#F4F4FD" />
               <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
               <SortedDescendingCellStyle BackColor="#D8D8F0" />
               <SortedDescendingHeaderStyle BackColor="#3E3277" />
                    </asp:GridView>
                 
                             </asp:Panel>
                                      </center>
                                         </div>
                                     </td>
                                 </tr>
                             <tr>
                                 <td>
                                <div class="btn pull-right">
                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn-sm btn-primary btn-block" Visible="false" OnClick="btnSubmit_Click" Height="30px" Width="90px"  Text="SUBMIT" />                                    
                                </div>
                                     </td>
                                 </tr>
                                 <caption>
                                     <br />
                             </caption>
                   
        </table>
    </center>
                               </table>
                 </ContentTemplate>
          <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="drpCourse"  EventName="SelectedIndexChanged" /> 
                        <asp:AsyncPostBackTrigger ControlID="btnShow"  EventName="Click" />
                    </Triggers>
                    </asp:UpdatePanel>  
         </fieldset>    
</asp:Content>

