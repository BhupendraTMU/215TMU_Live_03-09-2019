<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true"  CodeFile="StudentArrangementAttendanceMark.aspx.cs" Inherits="Faculty_StudentArrangementAttendanceMark" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
     .redBorder {
            border:1px solid red;
        }
        .loader
        {
            position: fixed;
            left: 45%;
            top: 45%;
            width: 100px;
            height: 100px;
            z-index: 9999;
            background: url('../images/loader.gif') 50% 50% no-repeat rgb(249,249,249);
        }
    </style>
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
            if (sender._selectedDate > new Date()) {
                alert("You cannot select greater than current date!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
        function callFeedbackMessage(inputType, inputText) {

            if (inputType == 'Error') {
                alertify.error(inputText);
                return false;
            }
            else if (inputType == 'Success') {
                alertify.success("Save Successfully");
                return false;
            }
            else {
                alertify.log(inputText, "", 10000);
                return false;
            }
        }

        function CheckOne(me) {
            debugger
            $('[id$=chkPresentAll]').attr('checked', false);
            $('[id$=chkAbsentAll]').attr('checked', false);
            me.checked = true;
            var GridId = "<%=grdStudentAttendance.ClientID %>";
            var grid = document.getElementById(GridId);
            rowscount = grid.rows.length - 1;

            for (i = 0; i < parseInt(rowscount) ; i++) {
                if (me.id == 'ContentPlaceHolder1_chkAbsentAll') {
                    $('[id$=ContentPlaceHolder1_grdStudentAttendance_chkboxAttendance_' + i + ']').prop("checked", false);
                }
                else {
                    $('[id$=ContentPlaceHolder1_grdStudentAttendance_chkboxAttendance_' + i + ']').prop("checked", true);
                }
            }
        }

        function DisableCheckbox(me) {
            var GridId = "<%=grdStudentAttendance.ClientID %>";
            var grid = document.getElementById(GridId);
            rowscount = grid.rows.length - 1;

            for (i = 0; i < parseInt(rowscount) ; i++) {
                if (me.checked == true) {
                    $('[id$=chkbox1stAttendance_' + i + ']').attr("disabled", false);
                    $('[id$=chkbox2ndAttendance_' + i + ']').attr("disabled", false);
                    $('[id$=chkbox3rdAttendance_' + i + ']').attr("disabled", false);
                }
                else if (me.checked == false) {
                    $('[id$=chkbox1stAttendance_' + i + ']').attr("disabled", true);
                    $('[id$=chkbox2ndAttendance_' + i + ']').attr("disabled", true);
                    $('[id$=chkbox3rdAttendance_' + i + ']').attr("disabled", true);
                }
            }
        }

        function Save() {
            var elem = document.getElementById("Loader1");
            elem.style.display = "block";
            $(".loader").fadeIn("slow");
            $('[id$=btnSave2]').click();
        }
        
        function Count() {
            $('[id$=ddlUnit]').addClass("redBorder");
            if ($('[id$=ddlUnit]').val() == '')
            {
                document.getElementById('<%=btnSave.ClientID %>').style.visibility = "hidden";
                document.getElementById('<%=ddlUnit.ClientID %>').focus();
                $('[id$=lblNoOfStudent]').text('Select Unit !');
                $('[id$=Label3]').text('');
                $('[id$=lblTotalNoOfStudent]').text('');
                return false;
            }
            else {
                $('[id$=ddlUnit]').removeClass("redBorder");
                document.getElementById('<%=btnSave.ClientID %>').style.visibility = "visible";
                var j = 0;
                var k = 0;
                var GridId = "<%=grdStudentAttendance.ClientID %>";
                var grid = document.getElementById(GridId);
                rowscount = grid.rows.length - 1;
                for (i = 0; i < parseInt(rowscount) ; i++) {
                    if ($('[id$=ContentPlaceHolder1_grdStudentAttendance_chkboxAttendance_' + i + ']').prop("checked") == true) {
                        j++;
                    }
                    k++;
                }
                $('[id$=lblNoOfStudent]').text(j);
                $('[id$=Label3]').text('out of');
                $('[id$=lblTotalNoOfStudent]').text(k);
            }
        }

        function CheckAll(me) {
            var GridId = "<%=grdStudentAttendance.ClientID %>";
            var grid = document.getElementById(GridId);
            rowscount = grid.rows.length - 1;

            for (i = 0; i < parseInt(rowscount) ; i++) {
                if (me.checked == true) {
                    $('[id$=ContentPlaceHolder1_grdStudentAttendance_chkboxAttendance_' + i + ']').prop("checked", true);
                }
                else {
                    $('[id$=ContentPlaceHolder1_grdStudentAttendance_chkboxAttendance_' + i + ']').prop("checked", false);
                }
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
 <asp:Label ID="Label1" runat="server" 
            Text="Mark Combined Attendance" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>

 </fieldset>
    <fieldset class="boxBodyHeader">   
 </fieldset>
     <fieldset class="boxBodyInner"> 
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
          <fieldset class="boxBodyInner">                                   
              <div class="loader" id="Loader1" style="display:none" ></div>         
          </fieldset>
          <fieldset class="boxBodyInner">
              <table cellpadding="0px" cellspacing="0px" width="100%">
                  <caption>
                      <br />
                      <tr>
                          <td>
                              <label>
                                  No.
                              </label>
                          </td>
                          <td style="width: 10px"></td>
                          <td>
                              <asp:TextBox ID="lblNo" runat="server" Height="20px" Enabled="false" Width="200px"></asp:TextBox>
                          </td>
                          <td>
                              <label>Academic Year :</label>
                              <asp:DropDownList ID="ddlAcademicYear" runat="server" Enabled="false" BackColor="#cccccc"></asp:DropDownList>
                          </td>
                          <td style="width: 20px"></td>
                          <td>
                              <label>
                                  Date
                              </label>
                          </td>
                          <td style="width: 10px"></td>
                          <td>
                              <asp:TextBox ID="txtDate" Enabled="true" runat="server" Width="220px" Height="22px" onkeypress="return false" onKeyDown="preventBackspace();" OnTextChanged="txtDate_TextChanged"
                                  AutoPostBack="true" oncopy="return false;" onpaste="return false;" autocomplete="off" ValidationGroup="Show"></asp:TextBox>
                              <asp:CalendarExtender ID="CalendarExtender1" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDate"
                                  CssClass="cal_Theme1" Enabled="true" TargetControlID="txtDate">
                              </asp:CalendarExtender>
                              <asp:RequiredFieldValidator ID="rfvtxtDate" runat="server"  ControlToValidate="txtDate" ValidationGroup="Show" ErrorMessage="*" Font-Bold="true" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                          </td>

                          <td style="width: 20px"></td>
                          <td>Lecture</td>
                          <td style="width: 10px"></td>
                          <td>
                              <asp:DropDownList ID="ddlLecture" Width="150px" Height="20px"  runat="server"></asp:DropDownList>
                              <asp:HiddenField ID="lblFacultyCode" runat="server"></asp:HiddenField>
                              <asp:RequiredFieldValidator ID="rfvddlLecture" runat="server"  ControlToValidate="ddlLecture" InitialValue="" ValidationGroup="Show" ErrorMessage="*" Font-Bold="true" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                          </td>
                          <td>
                              <asp:Button ID="btnShow" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="Show"
                                  Height="30px" Width="90px" OnClick="btnShow_Click" Text="SHOW" />
                          </td>
                      </tr>
              </table>
                 <br />
           
          </fieldset>

          <asp:Button runat="server" ID="btnSave2" BackColor="White" Height="0px" Width="0px"  OnClick="btnSave_Click"  BorderColor="White"  /> 
              <asp:Panel runat="server" ID="pnlCheckBox" Visible="false">
                          <table >
             <tr style="height:10px"><td colspan="10"></td></tr>
                              <tr>
                                  <td style="width: 10px"></td>
                                  <td>
                                      <asp:CheckBox ID="chkPresentAll" runat="server" Text=" Present All" Checked="true" onClick="CheckOne(this)" />

                                  </td>
                                  <td style="width: 10px"></td>
                                  <td>
                                      <asp:CheckBox ID="chkAbsentAll" Text="Absent All" runat="server" onClick="CheckOne(this)" />
                                  </td>
                                  <td style="width: 150px"></td>
                                  <td>Unit:</td>
                                  <td style="width: 10px"></td>
                                  <td>
                                      <asp:DropDownList ID="ddlUnit" runat="server" Width="220px"></asp:DropDownList>
                                  </td>
                                  <td style="width: 50px"></td>
                                  <td>
                                      <td>
                                          <label>Topic : </label>
                                      </td>
                                      <td style="width: 10px"></td>
                                      <td style="width: 10px">
                                          <asp:TextBox ID="txtTopic" runat="server" Width="220px" Height="20px"></asp:TextBox>
                                      </td>
                                  </td>
                              </tr>
             <tr style="height:10px"><td colspan="10"></td></tr>
            
         </table>
                  </asp:Panel>

          <center>
            <table style="width:100%">
                <tr>
                    <td>
                        <asp:Label ID="lblMessage" runat="server" Visible="false" ForeColor="Red" Width="100%"   Text="<u>You have already mark the Attendance of this lecture..</u>"></asp:Label>
                    </td>
                </tr>
                <tr>                                 
                   <td align="center">
                      <br />                     
                       <asp:Panel ID="pnlStudentAttendance" runat="server"   Width="100%" Visible="true">              
                       
                           <asp:GridView ID="grdStudentAttendance"  DataKeyNames="Enrollment No" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" 
                           BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" 
                           GridLines="Horizontal" OnSelectedIndexChanged="OnSelectedIndexChanged" EmptyDataText="There are no data records to display."
                               AllowSorting="true"  onsorting="grdStudentAttendance_Sorting" >
                       <AlternatingRowStyle BackColor="#F7F7F7" />
                                <Columns>   
                                 <asp:TemplateField HeaderText="Sl. No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle Width="7%" />
                            </asp:TemplateField>
                                    <asp:BoundField DataField="Enrollment No" HeaderText="Roll No" SortExpression="Enrollment No"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg"  />
                                    <asp:BoundField DataField="Student Name" HeaderText="Student Name" SortExpression="Student Name"  HeaderStyle-CssClass="visible-lg"  ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="Course" HeaderText="Course" SortExpression="Course"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="Subject Code" HeaderText="Subject Code" SortExpression="Subject Code"  HeaderStyle-CssClass="visible-lg"  ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="Description" HeaderText="Subject Name" SortExpression="Description"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="SemesterYear" HeaderText="Semester/Year" SortExpression="SemesterYear"  HeaderStyle-CssClass="visible-lg"  ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="Section" HeaderText="Section" SortExpression="Section"  HeaderStyle-CssClass="visible-lg"  ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="Group" HeaderText="Group" SortExpression="Group"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="Batch" HeaderText="Batch" SortExpression="Batch"  HeaderStyle-CssClass="visible-lg"  ItemStyle-CssClass="visible-lg" />                                                                                                               
                                    <asp:TemplateField>
                                    <ItemTemplate >
                                      
                                         <asp:CheckBox ID="chkboxAttendance"  HeaderText="Today" Checked="true" TabIndex="1" runat="server" />                                
                                    </ItemTemplate>

                                        <HeaderTemplate>Today</HeaderTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField>
                                    <ItemTemplate >                                      
                                         <asp:HiddenField ID="hfSubjectType" runat="server" Value='<%# Eval("SubjectType") %>' />
                                    </ItemTemplate>
                                        <HeaderTemplate></HeaderTemplate>
                                    </asp:TemplateField>  
                                    <asp:TemplateField>
                                    <ItemTemplate >                                      
                                         <asp:HiddenField ID="hfStudentNo" runat="server" Value='<%# Eval("No_") %>' />
                                    </ItemTemplate>
                                        <HeaderTemplate></HeaderTemplate>
                                    </asp:TemplateField>  
                                    
                                    <%--<asp:ButtonField DataTextField="Percentage" ButtonType="Link"  ControlStyle-ForeColor="Orange"  CommandName="Select" HeaderText="Percentage" />  --%>
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
                   </td>
                </tr>
                <tr>
                <td>
                    <div class="btn pull-right">
                                               <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn-sm btn-primary btn-block"  OnClientClick="return Count();" data-toggle="modal" data-target="#myModal2"   Visible="false" Height="30px" Width="90px"  Text="SUBMIT"  />                                    
                                            </div>
                </td>
                </tr>                   
            </table>

            <div class="modal fade" id="myModal3">
                    <div class="modal-dialog" style="width:800px;">
                      <div class="modal-content">
                        <div class="modal-header" style="background-color:#88CCFF;">
                            <div>
                                  <button type="button" class="close" data-dismiss="modal">&times;</button>
                                  <h4 class="modal-title"><b style="font-family:Arial; font-size:15px; font-weight:bold">Attendance Details</b></h4>
                             </div>
                        </div>
                        <div class="modal-body">
                                    <br />
                                    <center>
                                  <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="200" Width="80%" >             
                        <asp:GridView ID="grdAttendanceReport" runat="server"  AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"  
                            BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" GridLines="Horizontal" EmptyDataText="There are no data records to display." >
                               <AlternatingRowStyle BackColor="#F7F7F7" />
                                        <Columns>             
                                            <asp:BoundField DataField="Student Name" HeaderText="Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="Date1" HeaderText="Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="Hour" HeaderText="Lecture No"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="Attendance" HeaderText="Attendance" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />                           
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
                                        </center>                                           
                        </div>
                        <div class="modal-footer" style="border-top-width:0px">
                           <asp:Button runat="server" Text="Export to Excel" ID="btnExportToExcel" BackColor="#ACE9FB" OnClick="ExportToExcel" />
                        </div>
                      </div>
      
                    </div>
                   </div>
              <div class="modal fade" id="myModal2">
                    <div class="modal-dialog" style="width:400px;height:100px">
                      <div class="modal-content">
                        <div class="modal-header" style="background-color:#88CCFF;">
                            <div>
                                  <button type="button" class="close" data-dismiss="modal">&times;</button>
                                  <h4 class="modal-title"><b style="font-family:Arial; font-size:15px; font-weight:bold">Number of Student - Present</b></h4>
                             </div>
                        </div>
                        <div class="modal-body">
                                    <br />
                                    <center>
                                   <asp:Label runat="server" ID="lblNoOfStudent" Font-Bold="true" Font-Size="15px"></asp:Label>
                                    <asp:Label runat="server" ID="Label3" Font-Bold="true" Font-Size="15px" Text=" out of "></asp:Label></u>
                                    <asp:Label runat="server" ID="lblTotalNoOfStudent" Font-Bold="true" Font-Size="15px"></asp:Label></u>
                                        </center>                                           
                        </div>
                        <div class="modal-footer" style="border-top-width:0px">
                          <asp:Button runat="server" ID="btnSave"   CssClass="btn-sm btn-primary"  OnClientClick="Save();" class="close" data-dismiss="modal" Width="20%"   Text="Ok"></asp:Button>
                        </div>
                      </div>
      
                    </div>
                   </div>
   
                </center>
      </table>
                 </ContentTemplate>
          <Triggers>
                        
                        <asp:AsyncPostBackTrigger ControlID="btnShow"  EventName="Click" />
                     <asp:PostBackTrigger ControlID="btnExportToExcel"/>
                    </Triggers>
                    </asp:UpdatePanel>  
         </fieldset>    
</asp:Content>

