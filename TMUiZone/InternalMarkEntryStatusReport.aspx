<%@ Page Title="" Language="C#" MasterPageFile="~/IndexMaster.master" AutoEventWireup="true" CodeFile="InternalMarkEntryStatusReport.aspx.cs" Inherits="Faculty_InternalMarkEntryStatusReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset class="boxBodyInner">
        <table cellpadding="0px" cellspacing="0px">
            <caption>
                <br />
                <tr>
                    <td>Academic Year  </td>
                    <td style="width: 10px"></td>
                    <td>
                        <asp:DropDownList ID="drpAcademicYear" Width="100px" Height="20px" runat="server"></asp:DropDownList>
                    </td>
                    <td style="width: 20px"></td>
                    <td>Course
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
                        <asp:DropDownList ID="drpSemester" runat="server" Height="20px" Width="120px">
                        </asp:DropDownList>
                    </td>

                    <td style="width: 10px"></td>
                    <td>
                        <asp:Button ID="btnShow" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" OnClick="btnShow_Click" Height="30px" Width="90px" Text="SHOW" />

                    </td>
                    <td style="width: 10px">
                         <asp:Button ID="btnPrint" runat="server" CssClass="btn-sm btn-primary btn-block"  Height="30px" Width="90px" Text="Print" OnClientClick="return PrintElem();" Visible="false" />
                    </td>

                    <td></td>
                    <td style="width: 10px"></td>
                    <td>

                        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
        </table>
    </fieldset>
    <fieldset class="boxBodyInner">
        
        <div id="forPrint" style="min-height:300px; height:auto;">
            <asp:GridView ID="grdmarktable" runat="server" AutoGenerateColumns="False" BackColor="White" ShowFooter="true" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                <AlternatingRowStyle BackColor="#F7F7F7" />
                <Columns>

                    <asp:TemplateField HeaderText="Sr.No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <ItemStyle Width="6%" />
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Group No">
                    <ItemStyle Width="5%" />
                    <ItemTemplate>
                        <asp:Label ID="grdlblDocumentNo" runat="server" Text='<%#Eval("Group Document No_") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Method">
                    <ItemStyle Width="8%" />
                    <ItemTemplate>
                        <asp:Label ID="grdlblMethod" runat="server" Text='<%#Eval("ExamMethod") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Group">
                    <ItemStyle Width="8%" />
                    <ItemTemplate>
                        <asp:Label ID="grdlblgroup" runat="server" Text='<%#Eval("ExamGroup") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Subject Code">
                        <ItemStyle Width="8%" />
                        <ItemTemplate>
                            <asp:Label ID="grdlblCourseCode" runat="server" Text='<%#Eval("Subject Code") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Subject Name">
                        <ItemStyle Width="20%" />
                        <ItemTemplate>
                            <asp:Label ID="grdlblSubjectName" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                            <asp:HiddenField ID="hfSubject" runat="server" Value='<%#Eval("Subject Code") %>' />
                            <asp:HiddenField ID="hfStatus" runat="server" Value='<%#Eval("Status") %>' />
                            <asp:HiddenField ID="hfMaxMarks" runat="server" Value='<%#Eval("MaxMarks") %>' />
                            <asp:HiddenField ID="hfDocumentNo" runat="server" Value='<%#Eval("DocumentNo") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject Type">
                        <ItemStyle Width="7%" />
                        <ItemTemplate>
                            <asp:Label ID="grdlblSubjectType" runat="server" Text='<%#Eval("Subject Type") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- <asp:TemplateField HeaderText="Section">

                            <ItemStyle Width="8%" />
                            <ItemTemplate>
                                <asp:Label ID="grdlblSection" runat="server" Text='<%#Eval("Section") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Semester" ItemStyle-Width="8%">
                        <ItemTemplate>
                            <asp:Label ID="lblSemester" runat="server" Text='<%#Eval("Semester") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" ItemStyle-Width="16%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Statust") %>'></asp:Label>

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

    <script>

        function PrintElem() {
            var mywindow = window.open('', 'PRINT', 'height=400,width=600');
            mywindow.document.write('<html><head><title>' + "Report" + '</title>');
            mywindow.document.write('</head><body >');
            mywindow.document.write('<h3>' + "Internal Mark Entry Status Report" + '</h3>');
            mywindow.document.write(document.getElementById("forPrint").innerHTML);
            mywindow.document.write('</body></html>');

            mywindow.document.close(); // necessary for IE >= 10
            mywindow.focus(); // necessary for IE >= 10*/

            mywindow.print();
            mywindow.close();

            return true;
        }

       
    </script>
</asp:Content>

