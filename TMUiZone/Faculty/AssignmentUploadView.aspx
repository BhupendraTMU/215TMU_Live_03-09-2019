<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="AssignmentUploadView.aspx.cs" Inherits="Faculty_AssignmentUploadView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Uploaded Assignment" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

    </fieldset>
    <fieldset class="boxBodyHeader">
    </fieldset>

     <fieldset class="boxBodyInner">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <center>
                    <%--<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>--%>
                <table>
                    <tr>
                        <td style="padding:5px;">Course</td>
                        <td style="padding:5px;">
                            <asp:DropDownList ID="DrpCourse" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DrpCourse_SelectedIndexChanged"></asp:DropDownList></td>
                        <td style="padding:5px;">Subject</td>
                        <td style="padding:5px;">
                            <asp:DropDownList ID="DrpSubject" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DrpSubject_SelectedIndexChanged"></asp:DropDownList></td>
                        <%-- </tr>  <tr>--%>
                         <td style="padding:5px;">Faculty</td>
                        <td style="padding:5px;">
                            <asp:DropDownList ID="DrpFaculty" runat="server"></asp:DropDownList></td>
                        <td style="padding:5px;">
                            <asp:Button ID="btnshow" runat="server" Text="Show" OnClick="btnshow_Click" /></td>
                    </tr>
                </table>

                </center>
                <br />
                <center>
                    <table>
                        <tr>
                            <td>

                                <asp:GridView ID="grdAssignmentOutboxReport" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="866px" GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                <Columns>
                                    <asp:BoundField DataField="CourseCode" HeaderText="Course" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="Sem Year" HeaderText="Sem Year" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="Section" HeaderText="Section" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="SubjectDescription" HeaderText="Subject" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="UploadDate1" HeaderText="Upload Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                    <asp:BoundField DataField="DueDate1" HeaderText="Due Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="lnkDownload1" Text="Download" CommandArgument='<%# Eval("AssignmentNo_") %>' runat="server" OnClick="DownloadOutboxFile"></asp:LinkButton>
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
                                <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                <SortedDescendingHeaderStyle BackColor="#3E3277" />
                            </asp:GridView>

                            </td>
                        </tr>
                    </table>
                </center>
                </ContentTemplate>
              </asp:UpdatePanel>
           </fieldset>

</asp:Content>

