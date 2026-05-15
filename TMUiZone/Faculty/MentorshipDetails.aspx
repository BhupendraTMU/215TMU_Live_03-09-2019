<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="MentorshipDetails.aspx.cs" Inherits="Faculty_MentorshipDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .modalPopup {
            background-color: #ffffdd;
            border-width: 3px;
            border-style: solid;
            border-color: Gray;
            padding: 3px;
            width: 60%;
        }

            .modalPopup .header {
                background-color: #2FBDF1;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .body {
                min-height: 50px;
                line-height: 30px;
                text-align: center;
                padding: 5px;
            }

            .modalPopup .footer {
                padding: 3px;
            }

            .modalPopup .button {
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                background-color: red;
                border: 1px solid #5C5C5C;
            }

            .modalPopup td {
                text-align: left;
            }

        .redBorder {
            border: 1px solid red;
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
    </script>
    <script>
        function callFeedbackMessage(inputType, inputText) {

            if (inputType == 'Error') {
                alertify.error(inputText);
                return false;
            }
            else if (inputType == 'Success') {
                //alertify.confirm().set('overflow', false);
                alertify.success(inputText);
                return false;
            }
            else {
                alertify.log(inputText, "", 10000);
                return false;
            }
        }
        function SelectMachineCode(row) {
            var rowData = row.parentNode.parentNode;
            var rowIndex = rowData.rowIndex;
            // var strTest = rowData.cells[0].innerHTML;  //Report Open it                              
            //  PageMethods.CreateSessionViaJavascript(strTest);   //Report Open it                              
            // window.open('../Faculty/StudentDetailsForMentor.aspx?search=' + rowData.cells[7].innerHTML);
            window.open('../SredirectToReport.aspx?search=' + rowData.cells[6].innerHTML);
            // window.open('../StudentReport.aspx?search=' + rowData.cells[7].innerHTML, '_blank');  //Rport open it
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>

    <fieldset class="boxBodyInner">
        <table cellpadding="0px" cellspacing="0px">


            <tr>
                <td>Academic Year  </td>
                <td style="width: 10px"></td>
                <td>
                    <asp:DropDownList ID="drpAcademicYear" Width="100px" Height="20px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpAcademicYear_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td style="width: 20px"></td>
                <td>Course
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpCourse" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                <td style="width: 10px"></td>
                <td>
                    <asp:DropDownList ID="drpCourse" runat="server" AutoPostBack="true" Height="20px" Width="150px" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>

                <td style="width: 20px"></td>
                <td>
                    <label>Section  </label>
                </td>
                <td style="width: 10px"></td>
                <td>
                    <asp:DropDownList ID="drpSection" runat="server" Height="20px" Width="100px" OnSelectedIndexChanged="drpSection_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                </td>

                <%--<td style="width: 10px"></td>
                <td>
                    <asp:Button ID="btnShow" runat="server" CssClass="btn-sm btn-primary btn-block"
                        ValidationGroup="g1" OnClick="btnShow_Click" Height="30px" Width="90px" Text="SHOW" />

                </td>--%>

            </tr>
        </table>
    </fieldset>
    <asp:ScriptManager EnablePageMethods="true" ID="MainSM" runat="server" ScriptMode="Release" LoadScriptsBeforeUI="true"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <fieldset class="boxBody">
                <asp:Label ID="Label1" runat="server"
                    Text="Mentorship Details" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

            </fieldset>
            <fieldset class="boxBodyHeader">
            </fieldset>
            <fieldset class="boxBodyInner">
                <center>
                    <div class="pull-left">
                        <asp:CheckBox runat="server" ID="chkboxAsPrincipal" AutoPostBack="true" OnCheckedChanged="chkboxAsPrincipal_CheckedChanged" Text="Show more" Visible="false" />
                    </div>
                    <div id="Principal" class="pull-right">
                        <asp:HiddenField ID="hfFacultyCode" runat="server" />
                        <b style="font-weight: bold" visible="false" id="lblCourse" runat="server">Course:</b>&nbsp;
    <asp:DropDownList ID="drpCourseCode1" Visible="false" AutoPostBack="true" Width="200px" runat="server" OnSelectedIndexChanged="drpCourseCode1_SelectedIndexChanged"></asp:DropDownList>
                        <b style="font-weight: bold" visible="false" id="lblFaculty" runat="server">Faculty:</b>&nbsp;
    <asp:DropDownList ID="drpFacultyCode" Visible="false" AutoPostBack="true" Width="200px" runat="server" OnSelectedIndexChanged="drpFacultyCode_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="pull-right">
                        <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/images/excel.jpg" Height="35px" OnClick="btnExport_Click"></asp:ImageButton></div>
                    <br />
                    <asp:GridView ID="grdStudentDetails" runat="server" AutoGenerateColumns="False" BackColor="White" DataKeyNames="No_" BorderColor="#E7E7FF"
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal" OnPageIndexChanging="grdStudentDetails_PageIndexChanging" EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="10" OnSelectedIndexChanged="OnSelectedIndexChanged">
                        <AlternatingRowStyle BackColor="#F7F7F7" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sr. No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle Width="3%" />
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfStudentNo_" runat="server" Value='<%# Eval("No_") %>' />
                                    <asp:HiddenField ID="hfStudentName" runat="server" Value='<%# Eval("Student Name") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField DataTextField="Student Name" CommandName="Select" HeaderText="Student Name" />
                            <asp:BoundField DataField="EnrollmentNo_" HeaderText="Enrollment No" SortExpression="EnrollmentNo_" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Academic Year" HeaderText="Academic Year" SortExpression="Academic Year" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />

                            <asp:BoundField DataField="Program" HeaderText="Program" SortExpression="Program" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Section" HeaderText="Section" SortExpression="Section" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="No_" HeaderText="" SortExpression="No_" ItemStyle-Font-Size="0" />
                            <asp:TemplateField ShowHeader="true" HeaderText="Over All %">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkOP" runat="server" Text='<%# Eval("Percentage") %>' OnClick="lnkOP_Click"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnviewAttendanced" runat="server" Text="View" OnClick="btnviewAttendanced_Click"  CommandArgument='<%#Bind("Program") %> ' />
                                </ItemTemplate>
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

                    <fieldset class="boxBody">
                        <fieldset class="boxBodyInner">
                            <h2 id="lblStudentAbsent" style="color: blue" runat="server">Student Absent List(More than 2 Days)	</h2>
                            <br />
                            <asp:GridView ID="grdContinuseAbsentStudent" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None"
                                BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal" OnPageIndexChanging="grdContinuseAbsentStudent_PageIndexChanging" EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="10" OnSelectedIndexChanged="OnSelectedIndexChanged">
                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr. No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="EnrollmentNo" HeaderText="Enrollment No" SortExpression="EnrollmentNo" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="Course" HeaderText="Program" SortExpression="Course" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="SemesterYear" HeaderText="SemYear" SortExpression="SemYear" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                    <asp:ButtonField DataTextField="Section" CommandName="Select" HeaderText="Section" />

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
                        </fieldset>
                    </fieldset>

                    <asp:LinkButton Text="" ID="lnkFollowUP" runat="server" />
                    <asp:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="lnkFollowUP"
                        CancelControlID="btnClose" BackgroundCssClass="modalBackground" Drag="true" PopupDragHandleControlID="pnlPopup">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                        <div class="panel-heading" style="background-color: #ACE9FB">
                            <div class="pull-right">
                                <asp:Button ID="btnClose" runat="server" Text="X" Width="30px" CssClass="button" />
                            </div>
                            <p style="color: white; font-size: 25px"></p>
                            <asp:RadioButtonList runat="server" ID="rblInteractionAward" AutoPostBack="true" OnSelectedIndexChanged="rblInteractionAward_SelectedIndexChanged" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Interaction" Value="Interaction" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Award" Value="Award"></asp:ListItem>
                            </asp:RadioButtonList>
                            <br />
                            <p>
                                <b><u>
                                    <asp:Label runat="server" ID="lblStudentName"></asp:Label></u></b>
                            </p>
                        </div>
                        <asp:Panel runat="server" ID="pnlInteraction">
                            <table width="100%">
                                <thead>
                                    <tr>
                                        <th style="font-size: 15px">Date Committed</th>
                                        <th style="font-size: 15px">Phone No</th>
                                        <th style="font-size: 15px">Contact Person</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtDate" CssClass="form-control input-sm" runat="server" onkeypress="return false" onKeyDown="preventBackspace();"
                                                oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                            <asp:CalendarExtender ID="cleDOB" Format="dd MMM yyyy" runat="server" CssClass="cal_Theme1" Enabled="true" TargetControlID="txtDate">
                                            </asp:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPhoneNo" CssClass="form-control input-sm" runat="server" MaxLength="13"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender runat="server" TargetControlID="txtPhoneNo" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtContactPerson" CssClass="form-control input-sm" runat="server" MaxLength="30"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ValidationGroup="g1" ForeColor="Red" runat="server" ControlToValidate="txtDate" ErrorMessage="Please input the Date!"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ValidationGroup="g1" ForeColor="Red" runat="server" ControlToValidate="txtPhoneNo" ErrorMessage="Please input the Phone No!"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" ValidationGroup="g1" ForeColor="Red" runat="server" ControlToValidate="txtContactPerson" ErrorMessage="Please input the Name!"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <thead>
                                        <tr>
                                            <th style="font-size: 15px">Interaction Summary</th>
                                        </tr>
                                    </thead>
                                    <tr>
                                        <td colspan="3" style="width: 100%">
                                            <asp:TextBox ID="txtInteractionSummary" CssClass="form-control input-sm" TextMode="MultiLine" runat="server"></asp:TextBox>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" ValidationGroup="g1" ForeColor="Red" runat="server" ControlToValidate="txtInteractionSummary" ErrorMessage="Please input the interaction Summary!"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>

                                </tbody>
                                <caption>
                                    <dt></dt>
                                </caption>
                            </table>
                            <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both" Height="100" Width="100%">

                                <asp:GridView ID="grdInteractionDetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None"
                                    BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal" EmptyDataText="There are no data records to display."
                                    OnRowCommand="grdInteractionDetails_RowCommand">
                                    <AlternatingRowStyle BackColor="#F7F7F7" />
                                    <Columns>
                                        <asp:BoundField DataField="Date1" HeaderText="Date" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="Contact Person Phone No_" HeaderText="Contact Person No" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" DataFormatString="{0:N2}" ItemStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="Contact Person Name" HeaderText="Contact Person Name" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" DataFormatString="{0:N2}" ItemStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="Interaction Summary" HeaderText="Interaction Summary" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" DataFormatString="{0:N2}" ItemStyle-CssClass="visible-lg" />
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgdelete" runat="server" CommandName="DeleteInteraction" Text="Delete" ImageUrl="~/logo/close.png" Width="20px" Height="20px"
                                                    OnClientClick="return confirm('Are you sure you want to delete this Item?');" />
                                                <asp:HiddenField ID="hfLineNo" runat="server" Value='<%# Bind("LineNo") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
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

                            </asp:Panel>
                            <div class="pull-right">
                                <asp:Button runat="server" class="btn btn-info" Text="Save" ID="btnSave" OnClick="btnSave_Click" ValidationGroup="g1" />
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlAward">
                            <table width="100%">
                                <thead>
                                    <tr>
                                        <th style="font-size: 15px">Award Date </th>
                                        <th style="font-size: 15px">Award</th>
                                        <th style="font-size: 15px"></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="TextAwardDate" CssClass="form-control input-sm" runat="server" onkeypress="return false" onKeyDown="preventBackspace();"
                                                oncopy="return false;" onpaste="return false;" autocomplete="off"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" Format="dd MMM yyyy" runat="server" CssClass="cal_Theme1" Enabled="true" TargetControlID="TextAwardDate">
                                            </asp:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAward" CssClass="form-control input-sm" runat="server" MaxLength="13"></asp:TextBox>

                                        </td>
                                        <td>
                                            <%--<asp:TextBox ID="txt" CssClass="form-control input-sm" runat="server" MaxLength="30"></asp:TextBox>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" ValidationGroup="g2" ForeColor="Red" runat="server" ControlToValidate="TextAwardDate" ErrorMessage="Please input the Date!"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" ValidationGroup="g2" ForeColor="Red" runat="server" ControlToValidate="txtAward" ErrorMessage="Please input Award!"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" Display="Dynamic" ValidationGroup="g1" ForeColor="Red" runat="server" ControlToValidate="txtContactPerson" ErrorMessage="Please input the Name!"></asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <thead>
                                        <tr>
                                            <th style="font-size: 15px">Award Summary</th>
                                        </tr>
                                    </thead>
                                    <tr>
                                        <td colspan="3" style="width: 100%">
                                            <asp:TextBox ID="txtAwardSummary" CssClass="form-control input-sm" TextMode="MultiLine" runat="server"></asp:TextBox>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" Display="Dynamic" ValidationGroup="g2" ForeColor="Red" runat="server" ControlToValidate="txtAwardSummary" ErrorMessage="Please input the interaction Summary!"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>

                                </tbody>
                                <caption>
                                    <dt></dt>
                                </caption>
                            </table>
                            <asp:Panel ID="PanelgrdAward" runat="server" ScrollBars="Both" Height="100" Width="100%">

                                <asp:GridView ID="grdAward" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None"
                                    BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal" EmptyDataText="There are no data records to display."
                                    OnRowCommand="grdAward_RowCommand">
                                    <AlternatingRowStyle BackColor="#F7F7F7" />
                                    <Columns>
                                        <asp:BoundField DataField="AwardDate" HeaderText="Date" SortExpression="ApplicantName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="AwardType" HeaderText="Award" SortExpression="Award" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="AwardDescription" HeaderText="Award Summary" SortExpression="Award Summary" HeaderStyle-CssClass="visible-lg" DataFormatString="{0:N2}" ItemStyle-CssClass="visible-lg" />
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgdelete" runat="server" CommandName="DeleteAward" Text="Delete" ImageUrl="~/logo/close.png" Width="20px" Height="20px"
                                                    OnClientClick="return confirm('Are you sure you want to delete this Item?');" />
                                                <asp:HiddenField ID="hfLineNo" runat="server" Value='<%# Bind("LineNo") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
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

                            </asp:Panel>
                            <div class="pull-right">
                                <asp:Button runat="server" class="btn btn-info" Text="Save" ID="btnSaveAward" OnClick="btnSaveAward_Click" ValidationGroup="g2" />
                            </div>
                        </asp:Panel>
                    </asp:Panel>

                </center>

                <%--<ADD>--%>
                <asp:Panel ID="pnlOPDetails" CssClass="modalPopup" runat="server" Style="display: none; width: 600px; height: 500px">
                    <div class="header">
                        <b>
                            <asp:Label ID="lblNotification" runat="server" Text="Attendance Detail"></asp:Label></b><div class="close">
                                <asp:Button ID="Button1" runat="server" Text="X" />
                            </div>
                    </div>
                    <div id="Div1" runat="server" style="max-height: 400px; overflow: auto;">
                        <div class="body">
                            <div style="width: 100%">
                                <center>
                                    <asp:Label ID="lblStudent" runat="server" Text="Student Name : "></asp:Label>
                                    <asp:Label ID="lblStudentENo" runat="server">    </asp:Label>
                                    <br />
                                    <asp:GridView ID="grdOP_Details" Width="80%" EmptyDataText="There are no data records to display." runat="server" ShowFooter="true"
                                        CssClass="table table-striped table-bordered table-hover" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl. No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                        <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                                        <RowStyle ForeColor="#4A3C8C" Font-Bold="false" />

                                    </asp:GridView>
                                </center>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Button ID="btnDummy" runat="server" Style="display: none;" />
                <asp:ModalPopupExtender ID="MpOPDetails" runat="server" TargetControlID="btnDummy" PopupControlID="pnlOPDetails" BackgroundCssClass="modalBackground" />
                <%--<ADD>--%>
            </fieldset>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
            <asp:AsyncPostBackTrigger ControlID="grdStudentDetails" EventName="PageIndexChanging" />
            <asp:AsyncPostBackTrigger ControlID="grdContinuseAbsentStudent" EventName="PageIndexChanging" />

        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

