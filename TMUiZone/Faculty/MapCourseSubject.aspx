<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="MapCourseSubject.aspx.cs" Inherits="Faculty_MapCourseSubject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
        <table width="100%">
            <tr>
                <td width="60%">
                    <asp:Label ID="Label1" runat="server"
                        Text="Map Subject Course" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                </td>
            </tr>
        </table>


    </fieldset>
    <fieldset class="boxBodyHeader"></fieldset>

    <fieldset class="boxBodyInner">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="250" Width="500px">
                            <asp:ListBox runat="server" ID="lstCourse" SelectionMode="Multiple" Height="200px"></asp:ListBox>
                            </asp:Panel>
                        </td>
                        <td style="width: 10px"></td>
                        <td style="vertical-align: middle;" align="center">
                            <asp:ImageButton runat="server" ImageUrl="~/images/more_icons.gif" ID="btnGetSubject" OnClick="btnGetSubject_Click" Height="30px" Width="30px" />
                        </td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:Panel ID="pnlGrid" Visible="false" runat="server" ScrollBars="Both" Height="250" Width="600px">
                                <asp:GridView ID="grdSubjectDetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None"
                                    BorderWidth="1px" CellPadding="3" Width="600px" GridLines="Horizontal" ShowFooter="true"
                                    EmptyDataText="There are no data records to display.">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkboxSelectSubject" HeaderText="Select" onclick="OnChangeCheckbox (this)" runat="server" />
                                            </ItemTemplate>
                                            <HeaderTemplate>.</HeaderTemplate>
                                        </asp:TemplateField>
                                        <%--HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" ItemStyle-HorizontalAlign="Right"--%>
                                        <asp:BoundField DataField="Code" HeaderText="Subject Code" SortExpression="ApplicantName" />
                                        <asp:BoundField DataField="Description" HeaderText="Subject Description" SortExpression="ApplicantName" />
                                        <asp:BoundField DataField="Course" HeaderText="Course" SortExpression="TotalAmount" DataFormatString="{0:N2}" />
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#F7F7F7" />
                                    <%--<FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />--%>
                                    <FooterStyle BackColor="#ed7600" ForeColor="#F7F7F7" CssClass="cssGridheaderfont" HorizontalAlign="Right" />
                                    <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Right" />
                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                    <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                    <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                    <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <br />
                <div class="pull-right">
                    <asp:TextBox runat="server" ID="txtSubjectGroup"  Visible="false" placeholder="Group Name"></asp:TextBox>&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtSubjectGroup" InitialValue="" ErrorMessage="please enter the Group Name!" ForeColor="Red" ></asp:RequiredFieldValidator>
                    <asp:Button runat="server"  Width="100px" Text="Save" Visible="false" ID="btnSave" OnClick="btnSave_Click" ValidationGroup="g1"/>
                </div>
                <br />
                <br />

                         <asp:GridView ID="grdSubjectGroup" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None"
                                    BorderWidth="1px" CellPadding="3" Width="100%" GridLines="Horizontal"
                                    EmptyDataText="There are no data records to display.">
                                    <Columns>                                      
                                        <asp:BoundField DataField="CourseCode" HeaderText="Course Code" SortExpression="ApplicantName" />
                                        <asp:BoundField DataField="SubjectCode" HeaderText="Subject Code" SortExpression="ApplicantName" />
                                        <asp:BoundField DataField="SubjectDescription" HeaderText="Subject Description" SortExpression="TotalAmount" DataFormatString="{0:N2}" />
                                        <asp:BoundField DataField="SubjectGroup" HeaderText="Subject Group" SortExpression="ApplicantName" />
                                        <asp:BoundField DataField="SubjectGroupCode" HeaderText="Subject Group Code" SortExpression="ApplicantName" />
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#F7F7F7" />
                                    <%--<FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />--%>
                                    <FooterStyle BackColor="#ed7600" ForeColor="#F7F7F7" CssClass="cssGridheaderfont" HorizontalAlign="Right" />
                                    <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Right" />
                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                    <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                    <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                    <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
</asp:Content>

