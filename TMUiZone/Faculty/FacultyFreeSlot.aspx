<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="FacultyFreeSlot.aspx.cs" Inherits="Faculty_FacultyFreeSlot" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function checkDate(sender, args) {
            var today = new Date();
            today.setHours(0, 0, 0, 0);
            if (sender._selectedDate < today) {
                alert("You cannot select less than current date!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
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
        function callFeedbackMessage(inputType, inputText) {

            if (inputType == 'Error') {
                alertify.error(inputText);
                return false;
            }
            else if (inputType == 'Success') {
                alertify.success(inputText);
                return false;
            }
            else if (inputType == 'Message') {
                alertify.success(inputText);
                return false;
            }
        }
        $(document).ready(function () {
            $('[id$=grdInboxBody]').hide();
            $('[id$=grdOutboxBody]').hide();
            $('[id$=pnlReport]').hide();
        });
        function ShowHide() {
            $('[id$=grdInboxBody]').slideUp();
            $('[id$=grdOutboxBody]').slideDown();
        }
        function ShowHide1() {
            $('[id$=grdInboxBody]').slideDown();
            $('[id$=grdOutboxBody]').slideUp();
        }

        function ShowHide2() {
            $('[id$=pnlReport]').slideToggle();
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
        <table width="100%">
            <tr>
                <td width="60%">
                    <asp:Label ID="Label1" runat="server"
                        Text="Faculty Arrangement" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                </td>
                <td width="40%" runat="server" id="tdFaculty" visible="false">Arrangement For Faculty: &nbsp&nbsp&nbsp&nbsp
                                        <asp:DropDownList ID="ddlFaculty" runat="server" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
            </tr>
        </table>


    </fieldset>
    <fieldset class="boxBodyHeader"></fieldset>

    <fieldset class="boxBodyInner">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlPrincipal" runat="server">
                </asp:Panel>
                <asp:Panel runat="server" Style="background-color: rgba(0, 0, 0, 0.06);" ID="pnlMain" BorderColor="#e8e8e8" BorderWidth="0px" Width="100%">
                    <asp:CheckBox runat="server" ID="chkboxForAllCourse" Text="Show all free faculties" AutoPostBack="true" OnCheckedChanged="chkboxForAllCourse_CheckedChanged" Visible="false" />
                    <center>
                        <table width="90%">

                            <tr style="height: 15px">
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Date:</label></td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:TextBox ID="txtDate" runat="server" BorderColor="#B1B1B1" Width="200px" Height="22px" BackColor="#e8e8e8" onkeypress="return false"
                                        onKeyDown="preventBackspace();" OnTextChanged="txtDate_TextChanged"
                                        AutoPostBack="true" oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender2" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDate"
                                        CssClass="cal_Theme1" Enabled="true" TargetControlID="txtDate">
                                    </asp:CalendarExtender>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ValidationGroup="g1" ForeColor="Red" ControlToValidate="txtDate" ErrorMessage="Please select the Date!"></asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                                <td>
                                    <label>Lecture:</label></td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:DropDownList ID="drpLecture" runat="server" Width="200px" Height="20px" AutoPostBack="true" BackColor="#e8e8e8" OnSelectedIndexChanged="drpLecture_SelectedIndexChanged"></asp:DropDownList></td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ValidationGroup="g1" ForeColor="Red" ControlToValidate="drpLecture" ErrorMessage="Please select the Lecture!"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr style="height: 10px">
                                <td></td>
                            </tr>
                            <tr id="CourseRow" runat="server">
                                <td>
                                    <label>Course:</label></td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:DropDownList ID="drpCourse" runat="server" Width="200px" Height="20px" AutoPostBack="true" BackColor="#e8e8e8" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged"></asp:DropDownList></td>
                                <td></td>
                                <td style="width: 50px"></td>
                                <td>
                                    <label>Semester/Year:</label></td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:DropDownList ID="drpSemesterYear" runat="server" Width="200px" Height="20px" AutoPostBack="true" BackColor="#e8e8e8" OnSelectedIndexChanged="drpSemesterYear_SelectedIndexChanged"></asp:DropDownList></td>
                                <td></td>
                            </tr>
                           
                            <%--<tr>
                                <td><label>Date:</label></td>
                                <td style="width:10px"></td>  
                                <td>
                                 <asp:TextBox ID="txtDate1"  runat="server"  BorderColor="#B1B1B1" Width="200px"  Height="22px" BackColor="#e8e8e8" onkeypress="return false" onKeyDown="preventBackspace();" OnTextChanged="txtDate_TextChanged"
                                                         AutoPostBack="true"   oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                               <asp:CalendarExtender ID="CalendarExtender02" Format="dd MMM yyyy" runat="server" OnClientDateSelectionChanged="checkDate"
                                                    CssClass="cal_Theme1" Enabled="true" TargetControlID="txtDate1"></asp:CalendarExtender>
                                    </td>
                                   <td>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator03" Display="Dynamic" runat="server" ValidationGroup="g1" ForeColor="Red" ControlToValidate="txtDate1" ErrorMessage="Please select the Date!"></asp:RequiredFieldValidator>
                                    </td>                                
                                <td></td>
                                <td ><label>Lecture:</label></td>  
                                <td style="width:10px"></td>            
                                <td><asp:DropDownList ID="drpLecture" runat="server" Width="200px" Height="20px" AutoPostBack="true" BackColor="#e8e8e8" OnSelectedIndexChanged="drpLecture_SelectedIndexChanged"></asp:DropDownList></td>
                                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ValidationGroup="g1" ForeColor="Red" ControlToValidate="drpLecture" ErrorMessage="Please select the Lecture!"></asp:RequiredFieldValidator></td>
                
                              
                            </tr>--%>
                            <tr style="height: 10px">
                                <td></td>
                            </tr>
                            <tr>
                                <td> <label>Faculty Name:</label></td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:DropDownList ID="drpFacultyName" runat="server" Width="200px" Height="20px" BackColor="#e8e8e8"></asp:DropDownList></td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" ValidationGroup="g1" ForeColor="Red" ControlToValidate="drpFacultyName" ErrorMessage="Please select the Faculty!"></asp:RequiredFieldValidator></td>
                                <td></td>
                                <td style="width: 100px">
                                    <asp:LinkButton ID="btnAssign" ValidationGroup="g1" class="btn btn-info btn-sm" runat="server" Height="25px" OnClick="btnAssign_Click">
                                           Assign  <span class="glyphicon glyphicon-arrow-right"></span>  </asp:LinkButton>&nbsp
                                </td>
                                <td>
                                    
                                    <asp:HiddenField runat="server" ID="hfAcademic" />
                                </td>
                                <td><asp:CheckBox runat="server" ID="chkLab" Text="LAB/PRACTICAL"  AutoPostBack="true" OnCheckedChanged="chkLab_CheckedChanged"  Visible="false" /></td>
                                <td></td>
                            </tr>
                            <tr style="height: 15px">
                                <td></td>
                            </tr>
                        </table>
                    </center>
                </asp:Panel>
                <br />
                <center>
                    <div class="panel panel-info" style="width: 90%;">
                        <div class="panel-heading" onclick="ShowHide();" style="height: 25px">
                            <asp:Label ID="Label2" runat="server" Text="Outbox" Style="line-height: 5px" Font-Size="15px"></asp:Label>
                        </div>
                        <div class="panel-body" id="grdOutboxBody">
                            <asp:GridView ID="grdArrangementDetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                <Columns>
                                    <asp:BoundField DataField="SubstituteEmployeeName" HeaderText="Faculty Name" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="CourseCode" HeaderText="Course Code" SortExpression="CourseCode" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="SubjectCodeOriginal" HeaderText="Subject Code" SortExpression="SubjectCodeOriginal" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="RequestingDate1" HeaderText="Requesting Date" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="ArrangementDate1" HeaderText="Arrangement Date" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="Status1" HeaderText="Status" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="HourNo" HeaderText="Lecture No." SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                </Columns>
                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                <SortedDescendingHeaderStyle BackColor="#3E3277" />
                            </asp:GridView>
                        </div>
                        <div class="panel-heading" onclick="ShowHide1();" style="height: 25px">
                            <asp:Label ID="Inbox" runat="server" Text="Inbox" Style="line-height: 5px" Font-Size="15px"></asp:Label>
                        </div>
                        <div class="panel-body" id="grdInboxBody">
                            <asp:GridView ID="grdInbox" runat="server" AutoGenerateColumns="False" BackColor="White" DataKeyNames="RequestedByName" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="1000px" GridLines="Horizontal" EmptyDataText="There are no data records to display." OnRowCommand="grdInbox_RowCommand">
                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                <Columns>
                                    <asp:BoundField DataField="RequestedByName" HeaderText="Faculty Name" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="RequestedBy" HeaderText="Faculty Code" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="CourseCode" HeaderText="Course" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="Semester" HeaderText="Semester" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="Section" HeaderText="Section" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="RequestingDate1" HeaderText="Requesting Date" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="ArrangementDate1" HeaderText="Arrangement Date" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="HourNo" HeaderText="Lecture" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="SubjectCodeOriginal" HeaderText="Subject" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                    <asp:ButtonField Text="Accept" CommandName="Accept" HeaderText="Accept" ControlStyle-ForeColor="#ed7600" />
                                    <asp:ButtonField Text="Reject" CommandName="Reject" HeaderText="Reject" ControlStyle-ForeColor="#ed7600" />
                                </Columns>
                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                <SortedDescendingHeaderStyle BackColor="#3E3277" />
                            </asp:GridView>
                        </div>
                    </div>
                </center>
                <asp:LinkButton Text="" ID="lnkFollowUP" runat="server" />
                <asp:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="lnkFollowUP" CancelControlID="btnClose"
                    BackgroundCssClass="modalBackground" Drag="true" PopupDragHandleControlID="pnlPopup">
                </asp:ModalPopupExtender>

                <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                    <div class="panel-heading" style="background-color: #ACE9FB">
                        <div class="pull-right">
                            <asp:Button ID="btnClose" runat="server" Text="X" Width="30px" CssClass="button" /></div>
                        <p style="color: white; font-size: 25px" runat="server" id="PRejection">Reason for rejection</p>
                        <p style="color: white; font-size: 25px" runat="server" id="PAccept">Select Subject</p>
                        <br />
                        <p><b><u>
                            <asp:Label runat="server" ID="lblStudentName"></asp:Label></u></b></p>
                    </div>
                    <table width="100%" runat="server" id="tblRejection">
                        <tr style="height: 5px">
                            <td style="width: 5%"></td>
                            <td></td>
                            <td style="width: 5%"></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="width: 80%">
                                <asp:TextBox ID="txtReasonForRejection" placeholder="Remarks" CssClass="form-control input-sm" TextMode="MultiLine" runat="server"></asp:TextBox>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Display="Dynamic" ValidationGroup="g2" ForeColor="Red" runat="server" ControlToValidate="txtReasonForRejection" ErrorMessage="Please input the Reason!"></asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                    <table width="100%" runat="server" id="tblAccept">
                        <tr style="height: 5px">
                            <td style="width: 5%"></td>
                            <td></td>
                            <td style="width: 5%"></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="width: 80%">
                                <asp:DropDownList ID="ddlSubject" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvSubject" Display="Dynamic" ValidationGroup="g2" ForeColor="Red" runat="server"
                                    ControlToValidate="ddlSubject" ErrorMessage="Please Select Subject!"></asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                    <div class="pull-right">
                        <asp:Button runat="server" class="btn btn-info" Text="Save" ID="btnSave" OnClick="btnSave_Click" ValidationGroup="g2" />
                    </div>
                </asp:Panel>

                <asp:LinkButton ID="btnShowReport" Visible="false" class="btn btn-info btn-sm" OnClientClick=" return ShowHide2();" runat="server" Height="25px">
                    Show Report  <span class="glyphicon glyphicon-eye-open"></span>  </asp:LinkButton>
                <asp:Panel ID="pnlReport" BorderColor="#A1D6DC" BorderWidth="1px" runat="server">
                    <center>
                        <table>
                            <tr style="height: 10px">
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" Font-Bold="true" Text="Requested By"></asp:Label>
                                </td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:DropDownList ID="drpRequstedByCode" AutoPostBack="true" OnSelectedIndexChanged="drpRequstedByCode_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                </td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:Label ID="Label3" Font-Bold="true" runat="server" Text="Substitute Name"></asp:Label>
                                </td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:DropDownList ID="drpSubstituteEmployeeCode" AutoPostBack="true" OnSelectedIndexChanged="drpSubstituteEmployeeCode_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                </td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:Label ID="Label4" Font-Bold="true" runat="server" Text="Status"></asp:Label>
                                </td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:DropDownList ID="drpStatus" AutoPostBack="true" OnSelectedIndexChanged="drpStatus_SelectedIndexChanged" runat="server">
                                        <asp:ListItem Text="-- Status --" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Pending" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Accept" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Reject" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="height: 10px">
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" Font-Bold="true" Text="From"></asp:Label>
                                </td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:TextBox ID="txtDateFrom" runat="server" Width="150px" Height="22px" onkeypress="return false" onKeyDown="preventBackspace();" OnTextChanged="txtDateFrom_TextChanged"
                                        AutoPostBack="true" oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" Format="dd MMM yyyy" runat="server"
                                        CssClass="cal_Theme1" Enabled="true" TargetControlID="txtDateFrom">
                                    </asp:CalendarExtender>
                                </td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Font-Bold="true" Text="To"></asp:Label>
                                </td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:TextBox ID="txtDateTo" runat="server" Width="150px" Height="22px" onkeypress="return false" onKeyDown="preventBackspace();" OnTextChanged="txtDateTo_TextChanged"
                                        AutoPostBack="true" oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender3" Format="dd MMM yyyy" runat="server"
                                        CssClass="cal_Theme1" Enabled="true" TargetControlID="txtDateTo">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr style="height: 10px">
                                <td></td>
                            </tr>
                        </table>

                        <asp:GridView ID="grdArrangmentDetailsReport" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>
                                <asp:BoundField DataField="RequestedByName" HeaderText="Requested By" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="SubstituteEmployeeName" HeaderText="Faculty Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="ArrangementDate1" HeaderText="Arrangement Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Status1" HeaderText="Status" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="HourNo" HeaderText="Lecture No." HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Remarks1" HeaderText="Remarks" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            </Columns>
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <SortedAscendingCellStyle BackColor="#F4F4FD" />
                            <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                            <SortedDescendingCellStyle BackColor="#D8D8F0" />
                            <SortedDescendingHeaderStyle BackColor="#3E3277" />
                        </asp:GridView>
                        <br />
                    </center>

                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnExport" runat="server" Text="Export To Excel" Visible="false" OnClick="ExportToExcel" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>
        </asp:UpdatePanel>
    </fieldset>
</asp:Content>

