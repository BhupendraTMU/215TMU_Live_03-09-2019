<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="CertificateApprovalHOD.aspx.cs" Inherits="Faculty_CertificateApprovalHOD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
        <asp:Label ID="Label1" runat="server"
            Text="Year Back Approval" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
    </fieldset>
    
    <asp:UpdatePanel ID="mrak" runat="server">
        <ContentTemplate>
     <fieldset id="Fieldset1" class="boxBodyInner" runat="server" style="width: 100%">
         
         <div class="clearfix" style="width: 100%" >
             <table width="100%">
                 <tr align="right">
                     <td style="width:90%" align="right">
                          <asp:Button ID="btnSubmit"   runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px"  Width="139px" Text="Approve" OnClick="btnSubmit_Click"   />
                     </td>
                     <td  style="width:10%"><asp:Button ID="Button1"   runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px"  Width="139px" Text="Reject" OnClick="Button1_Click"   /></td>
                     
                 </tr>
                 <tr>
                     <td>

                     </td>
                 </tr>
             </table>
             &nbsp &nbsp
                     
                                <asp:GridView ID="grdCertificateApproval" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF"
                                    ShowFooter="true" BorderStyle="None" BorderWidth="1px"  CellPadding="3" Width="100%" GridLines="Horizontal"
                                    EmptyDataText="There are no data records to display.">
                                    <AlternatingRowStyle BackColor="#F7F7F7" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No." >
                                              <ItemStyle Width="3%" />
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle />
                                        </asp:TemplateField>

                                      
                                        <asp:TemplateField HeaderText="Enrollment No">
                                            <ItemStyle Width="8%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblEnroll" runat="server" Text='<%#Eval("[Enrollment No_]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Student Name">
                                            <ItemStyle Width="9%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblStudent" runat="server" Text='<%#Eval("[Student Name]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Course">
                                            <ItemStyle Width="5%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCourse" runat="server" Text='<%#Eval("[Course]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sem/Year">
                                        <ItemStyle Width="4%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSem" runat="server" Text='<%#Eval("[Sem]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Apply Date">
                                            <ItemStyle Width="5%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblApplyDate" runat="server" Text='<%#Eval("[Apply Date]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemStyle Width="8%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("[Status]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Student Remark">
                                            <ItemStyle Width="8%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSturemark" runat="server" Text='<%#Eval("[Remark]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="HOD Remarks">
                                            <ItemStyle Width="8%" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRemark" runat="server" Text='<%#Eval("[HOD Remarks]") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Principal Remark">
                                            <ItemStyle Width="8%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblPrincipalremark" runat="server" Text='<%#Eval("[Principal Remarks]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField  >
                                             <ItemStyle Width="3%" />
                                            <HeaderTemplate  >
                                                
                                                <asp:CheckBox ID="SChlAll1"  AutoPostBack="true" runat="server" OnCheckedChanged="SChlAll1_CheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="SChkD1" runat="server"  Enabled='<%#Eval("[Enable]").ToString().Equals("True") %>' AutoPostBack="true" />
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
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

