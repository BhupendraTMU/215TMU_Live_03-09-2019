<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="AwardList.aspx.cs" Inherits="Faculty_AwardList" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../images/minus.png");

        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "../images/plus.png");
            $(this).closest("tr").next().remove();
        });
    </script>




    <script type="text/javascript">

        function OpenNewWindow() {

            window.open('../Faculty/FacultyAwardlist.aspx'); return false;
        }
        function OpenNewWindow1() {

            window.open('../Faculty/MainReport.aspx'); return false;
        }


    </script>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <fieldset class="boxBodyInner">
        <div>
            <table width="100%">
                <tr align="left" style="width: 100%; text-align: left">
                    <td style="width: 200px; font: bold" colspan="10">EXAM TYPE : &nbsp&nbsp
                          <asp:RadioButton ID="rdInternal" Text="Internal" Width="90px" Font-Bold="true" Checked="true" runat="server" GroupName="examtype"></asp:RadioButton>
                        <asp:RadioButton ID="rdExternal" Text="External" Width="90px" Font-Bold="true" runat="server" GroupName="examtype"></asp:RadioButton></td>
                </tr>
            </table>
        </div>
        <table cellpadding="0px" cellspacing="0px">
            <caption>


                <tr>
                    <td>Academic Year  </td>
                    <td style="width: 10px"></td>
                    <td>
                        <asp:DropDownList ID="drpAcademicYear" Width="100px" Height="20px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpAcademicYear_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td style="width: 20px"></td>
                    <td>Program
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpCourse" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 10px"></td>
                    <td>
                        <asp:DropDownList ID="drpCourse" runat="server" AutoPostBack="true" Height="20px" Width="150px" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged">
                        </asp:DropDownList>
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

                    <td style="width: 20px"></td>
                    <td>Course
                    </td>
                    <td style="width: 10px"></td>
                    <td>
                        <asp:DropDownList ID="ddlSubject" runat="server" AutoPostBack="true" Height="20px" Width="120px" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 20px"></td>

                    <td>
                        <label>Section  </label>
                    </td>
                    <td style="width: 10px"></td>
                    <td>
                        <asp:DropDownList ID="drpSection" AutoPostBack="true" OnSelectedIndexChanged="drpSection_SelectedIndexChanged"  runat="server" Height="20px" Width="100px">
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



                    <asp:Panel ID="faculty" runat="server">
                        <td>Faculty
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:DropDownList ID="drpFaculty" runat="server" AutoPostBack="true" Height="20px" Width="120px" OnSelectedIndexChanged="drpFaculty_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </asp:Panel>
                    <td style="width: 10px"></td>
                    <td>
                        <asp:Button ID="btnShow" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" OnClick="btnShow_Click" Height="30px" Width="90px" Text="SHOW" />

                    </td>
                    &nbsp &nbsp &nbsp &nbsp
                            <td style="width: 20px">
                                <asp:Button ID="btnSendForApproval" Visible="false" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px" Width="132px" OnClick="btnSendForApproval_Click" Text="Send For Approval" /></td>
                </tr>

            </caption>

        </table>
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


        <fieldset class="boxBodyInner" runat="server" style="width: 100%">


            <div align="center">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" SizeToReportContent="true" AsyncRendering="true" CssClass="active" Border="Solid"></rsweb:ReportViewer>
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

                        <asp:BoundField DataField="Course Code" HeaderText="Program Code" ItemStyle-CssClass="visible-lg" />
                        <asp:BoundField DataField="Subject Code" HeaderText="Course Code" ItemStyle-CssClass="visible-lg" />
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
                                <asp:LinkButton ID="btnview" runat="server" OnClick="btnview_Click" Text="View" OnClientClick="aspnetForm.target =’_blank’;"></asp:LinkButton>
                                <asp:HiddenField ID="HfStaffcode" Value='<%# Eval("[Staff Code]") %>' runat="server" />
                                <asp:HiddenField ID="HfSubjectCode" Value='<%# Eval("[Subject Code]") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-center">
                            <HeaderTemplate>
                                <asp:CheckBox ID="SChlAll" Checked="true" AutoPostBack="true" runat="server" OnCheckedChanged="SChlAll_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="SChkD" runat="server" Checked='<%# Bind("txtMarksEnableDesable") %>' Enabled='<%# Bind("txtMarksEnableDesable") %>' AutoPostBack="true" />
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
</asp:Content>

