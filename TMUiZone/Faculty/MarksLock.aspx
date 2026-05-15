<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="MarksLock.aspx.cs" Inherits="Faculty_MarksLock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function OpenNewWindow() {

            window.open('../Faculty/ReportMarksEntry.aspx'); return false;
        }
        function isNumberKey(evt) {

            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        function OpenNewWindow1() {

            window.open('../Faculty/MainReport.aspx'); return false;
        }
        function OpenNewWindow2() {

            window.open('../Faculty/Awardlist.aspx'); return false;
        }
        function PrintDiv() {
            //document.getElementById('PanelHeader').style.visibility = 'visible';
            //alert("bhup");
            var divToPrint = document.getElementById('printarea');
            var popupWin = window.open('', '_blank', 'width=300,height=400,location=no,left=200px, margin:0mm');
            popupWin.document.open();
            popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
        }
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to save data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <style type="text/css">
        .redBorder {
            border: 1px solid red;
        }

        .loader {
            position: fixed;
            left: 45%;
            top: 45%;
            width: 100px;
            height: 100px;
            z-index: 9999;
            background: url('../images/loader.gif') 50% 50% no-repeat rgb(249,249,249);
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Marks View" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>


    <asp:UpdatePanel ID="mrak" runat="server">
        <ContentTemplate>
            <div class="col-sm-12">
                <div class="col-sm-4 p-0">
                </div>

                <div class="col-sm-8 p-0">
                    <asp:RadioButtonList ID="Rblist" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="Rblist_SelectedIndexChanged" CssClass="rbl">

                        <asp:ListItem Value="1" Text="THEORY" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="2" Text="PRACTICAL"></asp:ListItem>

                    </asp:RadioButtonList>
                </div>


            </div>
            <fieldset class="boxBodyInner">
                <table cellpadding="0px" cellspacing="0px">
                    <caption>
                        <br />

                        <tr>
                            <td>Academic Year  </td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:DropDownList ID="drpAcademicYear" Width="150px" Height="20px" runat="server" OnSelectedIndexChanged="drpAcademicYear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </td>
                            <td style="width: 20px"></td>
                            <td>Program<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpCourse" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator></td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:DropDownList ID="drpCourse" runat="server" AutoPostBack="true" Height="20px" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged" Width="200px"></asp:DropDownList>

                            </td>
                            <td style="width: 20px"></td>
                            <td>Semester/Year
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpSemester" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:DropDownList ID="drpSemester" runat="server" AutoPostBack="true" Height="20px" OnSelectedIndexChanged="drpSemester_SelectedIndexChanged" Width="120px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 10px"></td>
                            <td>Course</td>
                            <td>
                                <asp:DropDownList ID="ddlSubject" runat="server" AutoPostBack="true" Height="20px" Width="120px" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </td>

                            <td style="width: 20px"></td>

                            <td>
                                <label>Section  </label>
                            </td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:DropDownList ID="drpSection" AutoPostBack="true" OnSelectedIndexChanged="drpSection_SelectedIndexChanged" runat="server" Height="20px" Width="100px">
                                </asp:DropDownList>
                            </td>


                            <td style="width: 20px"></td>


                            <td>
                                <label>Group </label>
                            </td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:DropDownList ID="ddlGroup" Width="150px" Height="20px" runat="server"></asp:DropDownList>
                            </td>



                            <td style="width: 20px"></td>


                            <div id="onetime" runat="server" visible="false">

                                <td style="width: 30%">One Time Entry
                                </td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:CheckBox ID="chkPiv" runat="server" Checked="true" AutoPostBack="true" OnCheckedChanged="chkPiv_CheckedChanged" Height="20px" Width="120px"></asp:CheckBox>

                                </td>
                                <td style="width: 10px"></td>
                            </div>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:Button ID="btnShow" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" OnClick="btnShow_Click" Height="30px" Width="90px" Text="SHOW" />

                            </td>
                            <td style="width: 20px"></td>
                            <td>
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px" Visible="false" Width="90px" Text="Approve" OnClick="btnSubmit_Click" />


                            </td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:Button ID="brnReject" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px" Visible="false" Width="90px" Text="Reject" OnClick="brnReject_Click" />


                            </td>
                            <td>
                                <asp:Button ID="btnUnlock" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px" Visible="false" Width="90px" Text="Unlock" OnClick="btnUnlock_Click" />
                            </td>
                            <td>
                                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>

                            </td>

                        </tr>
                </table>




            </fieldset>

            <fieldset class="boxBodyInner">
                <div class="col-sm-12 p-0 text-right mr-20 example-show" style="display: none">
                    <asp:ImageButton ID="BtnPrint" runat="server" ImageUrl="~/images/pdf.jpg" OnClientClick="PrintDiv();" Width="40px" Height="30px" Visible="false"></asp:ImageButton>
                </div>
                <div class="row" style="text-align: right; width: 95%">
                    <asp:LinkButton ID="btnview" runat="server" OnClick="btnview_Click" Text="View and Approve Main report" Visible="false"></asp:LinkButton>
                </div>
                <asp:Panel ID="PanelReport" runat="server">
                    <fieldset class="boxBodyInner">

                        <table width="100%">
                            <tr align="right">
                                <td align="left" style="width: 25%; font: bold; visibility: hidden">Report Status :
                                    <asp:Label ID="lblReportStatus" Visible="false" ForeColor="Red" runat="server" Text=""></asp:Label></td>
                                <asp:Panel ID="tblAR" runat="server" Visible="false">
                                    <td align="right" style="width: 62%">
                                        <asp:Button ID="btnApprove" BackColor="Green" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px" Width="132px" OnClick="btnApprove_Click" Text="Approve" /></td>
                                    <td align="left">
                                        <asp:Button ID="btnReject" runat="server" BackColor="Red" OnClientClick="if(!confirm('Do you want to Reject'))return false;" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px" Width="132px" OnClick="btnReject_Click" Text="Reject" /></td>
                                </asp:Panel>
                            </tr>
                        </table>


                        <fieldset id="Fieldset1" class="boxBodyInner" runat="server" style="width: 100%">


                            <div align="center">
                                <%--     <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" SizeToReportContent = "true" AsyncRendering="true" CssClass="active" Border="Solid" ></rsweb:ReportViewer>--%>
                                <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
                            </div>

                            <div class="clearfix" style="width: 100%">

                                <asp:GridView ID="grdViewAwardlistApproval" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
                                    ShowFooter="true" BorderStyle="None" BorderWidth="1px" OnRowDataBound="grdViewAwardlistApproval_RowDataBound" CellPadding="3" Width="100%" GridLines="Horizontal"
                                    EmptyDataText="There are no data records to display.">
                                    <AlternatingRowStyle BackColor="#F7F7F7" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="Staff Name" HeaderText="Faculty Code" ItemStyle-CssClass="visible-lg" />

                                        <asp:BoundField DataField="Course Code" HeaderText="Course Code" ItemStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="Subject Code" HeaderText="Subject Code" ItemStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="Exam Type" HeaderText="Exam Type" ItemStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-CssClass="visible-lg" />

                                        <asp:TemplateField HeaderText="Award list Status">
                                            <ItemStyle Width="8%" />
                                            <ItemTemplate>
                                                <asp:Label ID="grdAwardStatus" runat="server" Text='<%#Eval("Award List Status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnview1" runat="server" OnClick="btnview1_Click" Text="View"></asp:LinkButton>
                                                <asp:HiddenField ID="HfStaffcode" Value='<%# Eval("[Staff Code]") %>' runat="server" />
                                                <asp:HiddenField ID="HfSubjectCode" Value='<%# Eval("[Subject Code]") %>' runat="server" />
                                                <asp:HiddenField ID="HfExamType" Value='<%# Eval("Exam Type") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-center">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="SChlAll1" Checked="true" AutoPostBack="true" runat="server" OnCheckedChanged="SChlAll1_CheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="SChkD1" runat="server" Checked='<%# Bind("txtMarksEnableDesable") %>' Enabled='<%# Bind("txtMarksEnableDesable") %>' AutoPostBack="true" />
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

                            </div>
                        </fieldset>
                    </fieldset>
                </asp:Panel>
                <asp:Panel ID="panelmarksentry" runat="server">
                    <asp:GridView ID="grdmarktable" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" ShowFooter="true" BorderWidth="1px" CellPadding="3" Width="100%" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                        <AlternatingRowStyle BackColor="#F7F7F7" />
                        <Columns>

                            <asp:TemplateField HeaderText="Sr.No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Group No">
                                <ItemStyle Width="5%" />
                                <ItemTemplate>
                                    <asp:Label ID="grdlblDocumentNo" runat="server" Text='<%#Eval("Group Document No_") %>'></asp:Label>
                                    <asp:HiddenField ID="hfDocumentNo" runat="server" Value='<%#Eval("DocumentNo") %>' />
                                    <asp:HiddenField ID="HFExamType" runat="server" Value='<%#Eval("Exam Type") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Method">
                                <ItemStyle Width="8%" />
                                <ItemTemplate>
                                    <asp:Label ID="grdlblMethod" runat="server" Text='<%#Eval("ExamMethod") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Exam Type">
                                <ItemStyle Width="8%" />
                                <ItemTemplate>
                                    <asp:Label ID="grdlblExamType" runat="server" Text='<%#Eval("Exam") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Group">
                                <ItemStyle Width="8%" />
                                <ItemTemplate>
                                    <asp:Label ID="grdlblgroup" runat="server" Text='<%#Eval("ExamGroup") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Faculty Name">
                                <ItemStyle Width="8%" />
                                <ItemTemplate>
                                    <asp:Label ID="grdlblName" runat="server" Text='<%#Eval("[Name]") %>'></asp:Label>
                                    <asp:Label ID="lblfacultyCode" runat="server" Visible="false" Text='<%#Eval("[Faculty Code]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Section">
                                <ItemStyle Width="8%" />
                                <ItemTemplate>
                                    <asp:Label ID="grdlblSection" runat="server" Text='<%#Eval("Section") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Course Code">
                                <ItemStyle Width="8%" />
                                <ItemTemplate>
                                    <asp:Label ID="grdlblCourseCode" runat="server" Text='<%#Eval("Course Code")+" :: "+Eval("CourseDescription") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Subject Name">
                                <ItemStyle Width="15%" />
                                <ItemTemplate>
                                    <asp:Label ID="grdlblSubjectName" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                                    <asp:HiddenField ID="hfSubject" runat="server" Value='<%#Eval("Subject Code") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Subject Type" Visible="false">
                                <ItemStyle Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="grdlblSubjectType" runat="server" Text='<%#Eval("Subject Type") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Section">
                                            <ItemStyle Width="8%" />
                                            <ItemTemplate>
                              <asp:Label ID="grdlblSection" runat="server" Text='<%#Eval("Section") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Semester/Year" ItemStyle-Width="6%">
                                <ItemTemplate>
                                    <asp:Label ID="lblSemester" runat="server" Text='<%#Eval("Semester") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" ItemStyle-Width="12%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Statust") %>'></asp:Label>
                                    <asp:HiddenField ID="hfStatus" runat="server" Value='<%#Eval("Status") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MarkEntry">
                                <ItemStyle Width="8%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="btntblmarksshow" CssClass="btn-sm btn-primary btn-block" OnClick="btntblmarksshow_Click" runat="server">Marks Entry</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Report" HeaderStyle-CssClass="text-right">
                                <ItemStyle Width="20%" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnReport" Width="50%" OnClick="btnReport_Click" Text="View" Visible='<%# (string)Eval("Statust") =="Approved By HOD" ? true :false%>' runat="server"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="5%" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right" HeaderText="UnLock">
                                <ItemStyle Width="5%" />
                                <HeaderTemplate>
                                    <asp:CheckBox ID="SChlAllLock" AutoPostBack="true" runat="server"
                                        OnCheckedChanged="SChlAllLock_CheckedChanged" Text="Unlock" OnClick="if(!confirm('Are you sure you want to Unlock All Marks Entry?'))return false;" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="SChkLock" runat="server" Checked='<%#Eval("chk") %>' Enabled='<%#Eval("Visible") %>'
                                        OnCheckedChanged="SChkLock_CheckedChanged" AutoPostBack="true"
                                        OnClick="if(!confirm('Are you sure you want to Unlock Current Marks Entry?'))return false;" />
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


                </asp:Panel>


                <%--    <after submit>--%>
                <div id="printarea">
                    <asp:GridView ID="grdViewmarksEntrySubmit" DataKeyNames="Student No_" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" ShowFooter="true" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" GridLines="Horizontal" EmptyDataText="There are no data records to display." Visible="false">
                        <AlternatingRowStyle BackColor="#F7F7F7" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sr. No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                    <asp:HiddenField ID="hfLineNo" runat="server" Value='<%#Eval("Line No_") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="3%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Enrollement No" HeaderText="EnrollementNo" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Student Name" HeaderText="Student Name" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Examgroup" HeaderText="Group" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="ExamMethod" HeaderText="Method" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Attendance Type" HeaderText="Attendance" ItemStyle-CssClass="visible-lg" />

                            <asp:TemplateField HeaderText="Weightage">
                                <ItemTemplate>
                                    <asp:Label ID="grdlMaximummark" runat="server" Text='<%#Eval("maxmark" ,"{0:N0}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Weightage Obtained">
                                <ItemTemplate>
                                    <%-- <asp:Label ID="grdtxtMarks" runat="server"  Text='<%# Bind("Internalmark","{0:N0}") %>'></asp:Label>   --%>
                                    <asp:Label ID="grdtxtMarks" runat="server" Text='<%# (Eval("Internalmark")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="marks" HeaderText="Obtained Marks" ItemStyle-CssClass="visible-lg" />
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRemarks" runat="server" Enabled='<%# Bind("ChkEnable") %>' Text='<%# Bind("Remarks") %>' MaxLength="100"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="5%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-center" Visible="false">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="SChlAll" Checked="true" AutoPostBack="true" runat="server" OnCheckedChanged="SChlAll_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="SChkD" runat="server" Checked='<%# Bind("Chk") %>' Enabled='<%# Bind("ChkEnable") %>' AutoPostBack="true" />
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
                    <br />
                    <asp:HiddenField ID="hf_DocumentNo" runat="server" />
                    <asp:HiddenField ID="hf_Status" runat="server" />
                    <asp:HiddenField ID="hf_ExamType" runat="server" />
                    <br />
                </div>
            </fieldset>

        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>

