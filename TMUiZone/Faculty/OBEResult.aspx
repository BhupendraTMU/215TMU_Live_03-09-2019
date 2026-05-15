<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="OBEResult.aspx.cs" Inherits="Faculty_OBEResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <fieldset class="boxBodyInner">
        <table cellpadding="0px" cellspacing="0px">
            <caption>
                <tr>
                    <td style="width: 35%" colspan="6">
                        <asp:Label ID="Label1" runat="server"
                            Text="Download OBE External Analysis" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                    </td>

                </tr>
                </caption>
           <tr>
               <td style="height:22px">

               </td>
           </tr>
                <tr>
                   
                    <td>Academic Year  </td>
                    <td style="width: 10px"></td>
                    <td>
                        <asp:DropDownList ID="drpAcademicYear" Width="100px" Height="20px" runat="server" AutoPostBack="true">
                             <asp:ListItem Text="24-25" Value="24-25"></asp:ListItem>
                            <asp:ListItem Text="23-24" Value="23-24"></asp:ListItem>
                            <asp:ListItem Text="22-23" Value="22-23"></asp:ListItem>
                            <asp:ListItem Text="21-22" Value="21-22"></asp:ListItem>

                        </asp:DropDownList>
                    </td>
                    <td style="width: 20px"></td>
                    <td>Program
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpCourse" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 10px"></td>
                    <td>
                        <asp:DropDownList ID="drpCourse" runat="server" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged" AutoPostBack="true" Height="20px" Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 20px"></td>
                    <td>Semester
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpSemester" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 10px"></td>
                    <td>
                        <asp:DropDownList ID="drpSemester" runat="server" AutoPostBack="true" Height="20px" Width="150px">
                        </asp:DropDownList>
                    </td>

                    <%--<td style="width: 10px"></td>
                    <td>Subject
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpSubject" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 10px"></td>
                    <td>
                        <asp:DropDownList ID="drpSubject" runat="server" AutoPostBack="true" Height="20px" Width="150px">
                        </asp:DropDownList>
                    </td>--%>
                    <td style="width: 10px"></td>
                    <td>
                        <asp:Button ID="btnShow" runat="server" CssClass="btn-sm btn-primary btn-block"
                            ValidationGroup="g1" OnClick="btnShow_Click" Height="30px" Width="90px" Text="SHOW / BACK" />

                    </td>





                </tr>
                
                <tr>
                    <asp:GridView ID="grdResult" runat="server" DataKeyNames="No_" BackColor="White" AutoGenerateColumns="false" ShowFooter="true" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                        GridLines="Horizontal" EmptyDataText="There are no data records to display.">
                        <AlternatingRowStyle BackColor="#F7F7F7" />

                        <Columns>
                            <asp:TemplateField HeaderText="Sr. No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle Width="3%" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Result No." ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="grdNo" runat="server" Text='<%#Eval("No_")%>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Course Code" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblCourse" runat="server" Text='<%#Eval("Course Code")%>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Course Name" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblCourseName" runat="server" Text='<%#Eval("Course Name")%>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Subject Code" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubjectName" runat="server" Text='<%#Eval("Subject Code")%>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Semester" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="grdSem" runat="server" Text='<%#Eval("Semester")%>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>

                           
                            
                            <asp:TemplateField HeaderText="OBE Analysis" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkResAn" runat="server" Text="Download" Enabled='<%# Eval("OBE").ToString() == "0" ? false : true %>' OnClick="lnkResAn_Click"></asp:LinkButton>

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

                </tr>

            </caption>
        </table>
    </fieldset>
</asp:Content>

