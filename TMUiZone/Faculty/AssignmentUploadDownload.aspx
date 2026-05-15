<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="AssignmentUploadDownload.aspx.cs" Inherits="Faculty_AssignmentUploadDownload" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

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
        function checkDate(sender, args) {
            var today = new Date();
            today.getHours(0, 0, 0, 0);
            if (sender._selectedDate > today) {
                alertify.error("You cannot select greater than current date!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value('');
            }
        }
        function checkDate1(sender, args) {
            var today = new Date();
            today.getHours(0, 0, 0, 0);
            if ($('[id$=txtDateFrom]').val() == '') {
                alertify.error('First select the from date!');
                sender._textbox.set_Value('');
                return false;
            }
            else if (sender._selectedDate > today) {
                alertify.error("You cannot select greater than current date!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value('');
            }
            else {
                var f = new Date($('[id$=txtDateFrom]').val());

                if (sender._selectedDate < f) {
                    alertify.error("You cannot select less than from date!");
                    sender._textbox.set_Value('');
                }
            }
        }

        function checkDate2(sender, args) {
            var today = new Date();
            //today.getHours(0, 0, 0, 0);
            if (sender._selectedDate < today) {
                alertify.error("You cannot select less than current date!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value('');
            }
        }
        function checkDate3(sender, args) {
            var today = new Date();
            //today.getHours(0, 0, 0, 0);
            if ($('[id$=txtDueDate]').val() == '') {
                alertify.error('First select the Due date!');
                sender._textbox.set_Value('');
                return false;
            }
            else if (sender._selectedDate < today) {
                alertify.error("You cannot select less than current date!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value('');
            }
            else {
                var f = new Date($('[id$=txtDueDate]').val());

                if (sender._selectedDate < f) {
                    alertify.error("You cannot select less than Due date!");
                    sender._textbox.set_Value('');
                }
            }
        }
        $(document).ready(function () {
            $('[id$=pnlInbox]').hide(); 
            $('[id$=pnlOutbox]').hide();
            $('[id$=pnlReport]').hide();
        });
        function ShowHide1() {
            $('[id$=pnlOutbox]').slideToggle();
            return false;
        }
        function ShowHide2() {
            $('[id$=pnlInbox]').slideToggle();
            return false;
        }
        function ShowHide3() {
            $('[id$=pnlReport]').slideToggle();
            return false;
        }
        function callFeedbackMessage(inputType, inputText) {

            if (inputType == 'Error') {
                alertify.error('There is no Student');
                return false;
            }
            else if (inputType == 'Success') {
                //alertify.confirm().set('overflow', false);
                alertify.success("Upload Successfully");
                return false;
            }
            else {
                alertify.log(inputText, "", 10000);
                return false;
            }
        }
    </script>
    <style type="text/css">
        .CSS1 {
            background-color: #ff1a1a !important;
        }

        .CSS2 {
            background-color: white !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
       <fieldset class="boxBody">
 <asp:Label ID="Label1" runat="server" 
            Text="Assignment" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 </fieldset>
    <fieldset class="boxBodyHeader"> 
  
 </fieldset>
     <fieldset class="boxBodyInner"> 
                   <asp:UpdatePanel runat="server">
                       <ContentTemplate>
        <table>
            <caption>
                <br />
                <tr>
                    <td style="width:10px"></td>
                    <td>
                      <label style="line-height:20px">
                        Academic Year:</label></td>
                    <td style="width:10px"></td>
                    <td>
                         <asp:DropDownList ID="drpAcademicYear" runat="server" AutoPostBack="true" Height="20px" OnSelectedIndexChanged="drpAcademicYear_SelectedIndexChanged" Width="200px">
                        </asp:DropDownList>
                       
                    </td>
                    <td style="width:100px"></td>
                    <td>
                         <label style="line-height:20px">
                        Course:</label></td>
                    <td style="width:10px"></td>
                    <td>
                       <asp:DropDownList ID="drpCourse" runat="server" AutoPostBack="true" Height="20px" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged" Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td style="width:8px"></td>
                    <td style="width:105px">
                        <label style="line-height:20px">
                        Semester / Year:</label></td>
                    <td style="width:10px"></td>
                    <td>
                         <asp:DropDownList ID="drpSemester" runat="server" AutoPostBack="true" Height="20px" OnSelectedIndexChanged="drpSemester_SelectedIndexChanged" Width="200px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="3"></td>
                    <td>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="drpAcademicYear" Display="Dynamic" ErrorMessage="Please select the Academic Year!" ForeColor="Red" ValidationGroup="g1"></asp:RequiredFieldValidator>   
                    </td>
                    <td colspan="3"></td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpCourse" Display="Dynamic" ErrorMessage="Please select the Course!" ForeColor="Red" ValidationGroup="g1"></asp:RequiredFieldValidator>
                    </td>
                    <td colspan="2"></td>
                    <td colspan="2"><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpSemester" Display="Dynamic" ErrorMessage="Please select the Semester!" ForeColor="Red" ValidationGroup="g1"></asp:RequiredFieldValidator></td>
                </tr>
                <tr style="height:10px">
                    <td></td>
                </tr>
                
                <caption>
                   
                    <tr>
                        <td style="width:10px"></td>
                        <td>
                            <label style="line-height:20px">
                           Section:</label></td>
                        <td style="width:10px"></td>
                        <td>
                           <asp:DropDownList ID="drpSection" runat="server" Height="20px" Width="200px">
                        </asp:DropDownList>
                        </td>
                        <td style="width:10px"></td>
                        <td>
                            <label style="line-height:20px">
                            Group:</label> </td>
                        <td style="width:10px"></td>
                        <td>
                            <asp:DropDownList ID="ddlGroup" runat="server" AutoPostBack="true" Height="20px" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="width:10px"></td>
                        <td>
                            <label style="line-height:20px">
                            Batch:</label> </td>
                        <td></td>
                        <td style="width:10px">
                            <asp:DropDownList ID="ddlBatch" runat="server" Height="20px" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td></td>
                       <%-- <td style="width:10px"></td>
                        <td style="width:10px"></td>
                        <td>&nbsp;</td>--%>
                    </tr>
                    <tr>
                        <td colspan="8">
                            <label style="line-height:20px">
                            .</label> </td>
                    </tr>
                    <tr>
                        <td style="width:10px"></td>
                        <td>
                            <label style="line-height:20px">
                            Subject:</label> </td>
                        <td style="width:10px"></td>
                        <td>
                            
                        <asp:DropDownList ID="drpSubject" runat="server" AutoPostBack="true" Height="20px" OnSelectedIndexChanged="drpSubject_SelectedIndexChanged" Width="200px">
                        </asp:DropDownList>


                       
                    
                        </td>
                        <td style="width:10px"></td>
                        <td>
                            <label style="line-height:20px">
                            Subject Type:</label> </td>
                        <td style="width:10px"></td>
                        <td>
                            <asp:TextBox ID="lblSubjectType" runat="server" Enabled="false" Height="20px"></asp:TextBox>
                        </td>
                        <td style="width:5px"></td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <label style="line-height:20px">
                                        Marks:</label></td>
                                </tr>
                            </table>
                        </td>
                        <td style="width:10px"></td>
                        <td>
                            <asp:TextBox ID="txtMaximumMarks" runat="server" Height="20px" MaxLength="3" placeholder="Maximum Marks" Width="200px" ></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtMaximumMarks">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3"></td>
                        <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="drpSubject" Display="Dynamic" ErrorMessage="Please select the Subject!" ForeColor="Red" ValidationGroup="g1"></asp:RequiredFieldValidator>   
                        </td>
                        <td colspan="3"></td>
                        <td>
                            
                        </td>
                        <td colspan="2"></td>
                        <td colspan="2">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtMaximumMarks" Display="Dynamic" ErrorMessage="Please input the Marks!" ForeColor="Red" ValidationGroup="g1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height:10px">
                        <td></td>
                    </tr>
                    <tr>
                        <td style="width:10px"></td>
                        <td>
                            <label style="line-height:20px">
                            Due Date:</label></td>
                        <td style="width:10px"></td>
                        <td>
                            <asp:TextBox ID="txtDueDate" runat="server" autocomplete="off" Height="22px" oncopy="return false;" onKeyDown="preventBackspace();" onkeypress="return false" onpaste="return false;" placeholder="Due Date" Width="150px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1" Enabled="true" Format="dd MMM yyyy" OnClientDateSelectionChanged="checkDate2" TargetControlID="txtDueDate">
                            </asp:CalendarExtender>
                        </td>
                        <td style="width:100px"></td>
                        <td>
                            <label style="line-height:20px">
                            Close Date:</label></td>
                        <td style="width:10px"></td>
                        <td>
                            <asp:TextBox ID="txtCloseDate" runat="server" autocomplete="off" Height="22px" oncopy="return false;" onKeyDown="preventBackspace();" onkeypress="return false" onpaste="return false;" placeholder="Close Date" Width="150px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1" Enabled="true" Format="dd MMM yyyy" OnClientDateSelectionChanged="checkDate3" TargetControlID="txtCloseDate">
                            </asp:CalendarExtender>
                        </td>
                        <td style="width:100px"></td>
                        <td style="width:70px"></td>
                        <td style="width:10px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="3"></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDueDate" Display="Dynamic" ErrorMessage="please enter the due date!" ForeColor="Red" ValidationGroup="g1"></asp:RequiredFieldValidator>
                        </td>
                        <td colspan="3"></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtCloseDate" Display="Dynamic" ErrorMessage="please enter the close date!" ForeColor="Red" ValidationGroup="g1"></asp:RequiredFieldValidator>
                        </td>
                        <td colspan="3"></td>
                        <td></td>
                    </tr>
                </caption>
            </caption>
            </table>
                           
                       </ContentTemplate>
                       <Triggers>
                           <asp:PostBackTrigger ControlID="btnUpload" />
                       </Triggers>
                   </asp:UpdatePanel>       
        <br />
            <table>
            <tr>
                <td style="width:10px"></td>
                <td><label style="line-height:20px">Topic:</label></td>
                <td style="width:10px"></td>
                <td>
                    <asp:TextBox ID="txtSubjectDescription" runat="server" MaxLength="250" placeholder="Subject" Width="600px" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" ValidationGroup="g1" ForeColor="Red" runat="server" ControlToValidate="txtSubjectDescription" ErrorMessage="Please enter the Topic!"></asp:RequiredFieldValidator>
                </td>
                <td ></td>
             </tr>
                <tr>
                    <td colspan="5">&nbsp</td>
                </tr>
                <tr>
                    <td style="width:10px"></td>
                    <td><label style="line-height:20px">Assignment:&nbsp;</label></td>
                    <td ></td>
                    <td colspan="2" >
                      <table>
                          <tr>
                              <td>
                                  <asp:FileUpload ID="FileUpload1" CssClass="form-control input-sm"  runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" ValidationGroup="g1" ForeColor="Red" runat="server" ControlToValidate="FileUpload1" ErrorMessage="First Choose the file!"></asp:RequiredFieldValidator>
                              </td>
                              <td>
                                  &nbsp&nbsp&nbsp&nbsp
                         <asp:LinkButton  id="btnUpload" ValidationGroup="g1"  class="btn btn-info btn-sm" runat="server"  OnClick="btnUpload_Click">
                         <span class="glyphicon glyphicon-upload"></span> Upload 
                          </asp:LinkButton>   
                     </td>
                          </tr>
                      </table>  

                    </td>
                     
                </tr>
             <tr>
                <td style="width:10px"></td>

                <td>       
                        
                </td>
                 <td style="width:10px"></td>
                </tr>
            </table>
        <br />
         <asp:UpdatePanel runat="server">
             <ContentTemplate>                 
                     <center>
                          <div class="panel panel-info" style="width:90%;">
                                <div class="panel-heading" onclick="ShowHide1();" style="height:25px">
                                    <asp:Label ID="Label2" runat="server" Text="Outbox" style="line-height:5px"  Font-Size="15px"></asp:Label>
                                </div>
                                <div class="panel-body" id="pnlOutbox">    
                                    <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server"  Font-Bold="true"  Text="From"></asp:Label>
                                                </td>
                                                <td style="width:10px"></td>
                                                <td>
                                                      <asp:TextBox ID="txtDateFrom"  runat="server"  Width="150px"  Height="22px"  onkeypress="return false" onKeyDown="preventBackspace();" OnTextChanged="txtDateFrom_TextChanged"
                                                                     AutoPostBack="true"   oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                                           <asp:CalendarExtender ID="CalendarExtender3" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDate"
                                                                CssClass="cal_Theme1" Enabled="true" TargetControlID="txtDateFrom"></asp:CalendarExtender>
                                                </td>
                                                <td style="width:10px"></td>
                                                <td>
                                                    <asp:Label ID="Label5" runat="server"  Font-Bold="true"  Text="To"></asp:Label>
                                                </td>
                                                <td style="width:10px"></td>
                                                <td>
                                                      <asp:TextBox ID="txtDateTo"  runat="server" Width="150px"  Height="22px"  onkeypress="return false" onKeyDown="preventBackspace();" OnTextChanged="txtDateTo_TextChanged"
                                                                     AutoPostBack="true"   oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                                           <asp:CalendarExtender ID="CalendarExtender4" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDate1"
                                                                CssClass="cal_Theme1" Enabled="true" TargetControlID="txtDateTo"></asp:CalendarExtender>
                                                </td>
                                            </tr>
                                    </table> 
                                    <br />      
                                <asp:GridView ID="grdAssignmentOutboxReport" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal" EmptyDataText="There are no data records to display."  >
                                       <AlternatingRowStyle BackColor="#F7F7F7" />
                                                     <Columns>   
                                                          <asp:BoundField DataField="AssignmentNo_" HeaderText="Assignment No" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />                         
                                                                <asp:BoundField DataField="CourseCode" HeaderText="Course" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                                <asp:BoundField DataField="Semester" HeaderText="Semester" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                                <asp:BoundField DataField="Section" HeaderText="Section"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                                <asp:BoundField DataField="SubjectDescription" HeaderText="Subject" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                                <asp:BoundField DataField="UploadDate1" HeaderText="Upload Date"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                                <asp:BoundField DataField="DueDate1" HeaderText="Due Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            
                                                                 <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:UpdatePanel runat="server">
                                                                        <ContentTemplate>
                                                                             <asp:LinkButton ID="lnkDownload1" Text = "Download" CommandArgument = '<%# Eval("AssignmentNo_") %>' runat="server" OnClick = "DownloadOutboxFile"></asp:LinkButton>
                                                                        </ContentTemplate>
                                                                           <Triggers>
                                                                             <asp:PostBackTrigger ControlID="lnkDownload1" />
                                                                         </Triggers>
                                                                    </asp:UpdatePanel>
                                                                   
                                                                </ItemTemplate>                                 
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
                                     </div>
                                <div class="panel-heading" onclick="ShowHide2();" style="height:25px">
                                    <asp:Label ID="Label3" runat="server" Text="Inbox" style="line-height:5px"  Font-Size="15px"></asp:Label>
                                </div>
                                <div class="panel-body" id="pnlInbox">
                                    <asp:GridView ID="grdAssignmentReport" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal" OnPageIndexChanging="grdAssignmentReport_PageIndexChanging"  EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="50">
                                                   <AlternatingRowStyle BackColor="#F7F7F7" />
                                                            <Columns>  
                                                                <asp:TemplateField HeaderText="Sl. No.">
                                                            <ItemTemplate >
                                                                <%# Container.DataItemIndex + 1 %>
                                                                
                                                            </ItemTemplate>
                                                            <ItemStyle Width="7%" />
                                                        </asp:TemplateField>  
                                                                     <asp:BoundField DataField="AssignmentNo_" HeaderText="Assignment No" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />                     
                                                                <asp:BoundField DataField="StudentName" HeaderText="Student Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                                <asp:BoundField DataField="CourseCode" HeaderText="Course" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                                <asp:BoundField DataField="Semester" HeaderText="Semester" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                                <asp:BoundField DataField="SubjectDescription" HeaderText="Subject" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                                <asp:BoundField DataField="UploadDate1" HeaderText="Submited Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />                            
                                                                 <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:UpdatePanel runat="server">
                                                                        <ContentTemplate>
                                                                             <asp:LinkButton ID="lnkDownload" Text = "Download" CommandArgument = '<%# "["+Eval("AssignmentNo_")+"]"+"("+Eval("StudentCode")+")" %>' runat="server" OnClick = "DownloadInboxFile"></asp:LinkButton>
                                                                        </ContentTemplate>
                                                                          <Triggers>
                                                                             <asp:PostBackTrigger ControlID="lnkDownload" />
                                                                         </Triggers>
                                                                    </asp:UpdatePanel>
                                                                   
                                                                </ItemTemplate>                                 
                                                            </asp:TemplateField>
                                                            </Columns>                       
                                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                   <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                                                   <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                                   <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                   <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                                   <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                                   <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                                   <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                     </asp:GridView>
                                </div>
                               <div class="panel-heading" onclick="ShowHide3();" style="height:25px">
                                    <asp:Label ID="Label6" runat="server" Text="Assignment Report" style="line-height:5px"  Font-Size="15px"></asp:Label>
                                </div>
                              <div class="panel-body" id="pnlReport">
                                  <fieldset class="boxBodyInner">
                <table cellpadding="0px" cellspacing="0px">
                    <caption>
                        <br />
                         <tr>
                            <td>Academic Year  </td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:DropDownList ID="drpAcademic1" Width="70px" Height="20px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpAcademic1_SelectedIndexChanged" ></asp:DropDownList>
                            </td>
                            <td style="width: 20px"></td>
                            <td>Course
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="g2" Display="Dynamic" ControlToValidate="drpCourse1" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:DropDownList ID="drpCourse1" runat="server" AutoPostBack="true" Height="20px" Width="150px"  OnSelectedIndexChanged="drpCourse1_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </td>
                            <td style="width: 20px"></td>
                            <td>Semester/Year
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="g2" Display="Dynamic" ControlToValidate="drpSemester1" InitialValue="" ErrorMessage="**"  ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:DropDownList ID="drpSemester1" runat="server" AutoPostBack="true" Height="20px"  Width="80px" OnSelectedIndexChanged="drpSemester1_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                              <td style="width: 20px"></td>
                              <td>Section
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="g2" Display="Dynamic" ControlToValidate="DrpSection" InitialValue="" ErrorMessage="**"  ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:DropDownList ID="ddlSection" runat="server" AutoPostBack="true" Height="20px" Width="120px" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </td>
                            <td style="width: 20px"></td>
                              <td>Subject
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="g2" Display="Dynamic" ControlToValidate="ddlSubject" InitialValue="" ErrorMessage="**"  ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:DropDownList ID="ddlSubject" runat="server" AutoPostBack="true" Height="20px" Width="120px" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </td>
                             <%-- <td style="width: 20px"></td>
                              <td>Assignment No.
                            </td>
                              <td style="width: 10px"></td>
                             <td>
                                <asp:DropDownList ID="drpAssignmentNo" runat="server" AutoPostBack="true" Height="20px" Width="120px" OnSelectedIndexChanged="drpAssignmentNo_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </td>--%>
                              
                              <td style="width: 10px"></td>
                              <td>
                                <asp:Button ID="btnShow" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g2"  Height="30px" Width="90px" Text="SHOW" OnClick="btnShow_Click" />

                            </td>
                            <td style="width: 10px"></td>
                        </tr>
                        </caption>
                </table>
            </fieldset>
                                   <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="95%" SizeToReportContent = "true" AsyncRendering="true" CssClass="active"></rsweb:ReportViewer>
                                    <%--<asp:GridView ID="grdReport" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal" OnPageIndexChanging="grdReport_PageIndexChanging"  EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="15">
                                                   <AlternatingRowStyle BackColor="#F7F7F7" />
                                                            <Columns>  
                                                                <asp:TemplateField HeaderText="Sl. No.">
                                                            <ItemTemplate >
                                                                <%# Container.DataItemIndex + 1 %>
                                                                
                                                            </ItemTemplate>
                                                            <ItemStyle Width="7%" />
                                                        </asp:TemplateField>    
                                                                <asp:BoundField DataField="AssignmentNo_" HeaderText="Assignment No" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />                        
                                                                <asp:BoundField DataField="StudentName" HeaderText="Student Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                                <asp:BoundField DataField="CourseCode" HeaderText="Course" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                                <asp:BoundField DataField="Semester" HeaderText="Semester" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                                <asp:BoundField DataField="SubjectDescription" HeaderText="Subject" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                                <asp:BoundField DataField="UploadDate1" HeaderText="Submited Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                                   <asp:TemplateField HeaderText="Assignment Status">
                                                                       <ItemTemplate>
                                                                            <asp:Label ID="lblAssign" runat="server" CssClass='<%# Bind("css") %>' Text='<%# Eval("[AssignmentStatus]") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
                                                                      <asp:BoundField DataField="assign" HeaderText="No. Of Assignment" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" /> 
                                                                      <asp:BoundField DataField="Delivered" HeaderText="Submitted Assignment" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />                    
                                                            </Columns>                       
                                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                   <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                                                   <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                                   <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                   <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                                   <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                                   <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                                   <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                     </asp:GridView>--%>
                                </div>
                         </div>
                    </center>
             </ContentTemplate>
           
         </asp:UpdatePanel>
    </fieldset>
</asp:Content>

