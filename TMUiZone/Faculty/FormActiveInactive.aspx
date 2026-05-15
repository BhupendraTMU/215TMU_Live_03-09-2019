<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="FormActiveInactive.aspx.cs" Inherits="Faculty_FormActiveInactive" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset class="boxBody">
 <asp:Label ID="Label1" runat="server" 
            Text="User Role Matrix" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;" ></asp:Label>
 </fieldset>
    <fieldset class="boxBodyHeader">   </fieldset>
     <fieldset class="boxBodyInner"> 
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>--%>
     <div>
        <div>
            <table id="tblAddCourse" runat="server" visible="false" >
                <tr>
                    <td>
                       <asp:Label ID="lblCourse" Text="" runat="server"></asp:Label>
          
                        </td>
                    <td>
                       
                    </td>
                    </tr>
                </table>
        </div>
         <div>
             <table id="Table1" runat="server"  width="100%">
                <tr>
                    <td>
                        <asp:GridView ID="grdFormActiveInactive" runat="server" AutoGenerateColumns="false" DataKeyNames="id"
                            Width="100%" HeaderStyle-BackColor="Green" AlternatingRowStyle-BackColor="#66ffff"
                            OnRowCommand="grdFormActiveInactive_RowCommand"
                            OnRowDeleting="grdFormActiveInactive_RowDeleting"
                            OnRowUpdating="grdFormActiveInactive_RowUpdating"
                            OnRowCancelingEdit="grdFormActiveInactive_RowCancelingEdit"
                            OnRowEditing="grdFormActiveInactive_RowEditing"
                            OnRowDataBound="grdFormActiveInactive_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PageId" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPageId" runat="server" Text='<%# Eval("PageId") %>' />
                                    </ItemTemplate>                                    
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Principal">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblPrincipal" runat="server" Checked='<%# Eval("Principal") %>' Enabled="false"  />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkPrincipal" runat="server" Checked='<%# Eval("Principal") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="HOD">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblHOD" runat="server" Checked='<%# Eval("HOD") %>' Enabled="false"  />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkHOD" runat="server" Checked='<%# Eval("HOD") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Course Co">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblCourseCo" runat="server" Checked='<%# Eval("CourseCoOrdinator") %>' Enabled="false"  />
                                    </ItemTemplate>
                                     <EditItemTemplate>
                                        <asp:CheckBox ID="chkCourseCo" runat="server" Checked='<%# Eval("CourseCoOrdinator") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LabIncharge">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblLab" runat="server" Checked='<%# Eval("LabIncharge") %>' Enabled="false" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkLab" runat="server" Checked='<%# Eval("LabIncharge") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Event Co">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblEventCo" runat="server" Checked='<%# Eval("EventCoOrdinator") %>' Enabled="false"  />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkEventCo" runat="server" Checked='<%# Eval("EventCoOrdinator") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Proctor">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblProctor" runat="server" Checked='<%# Eval("Proctor") %>' Enabled="false" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkProctor" runat="server" Checked='<%# Eval("Proctor") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Faculty">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblFaculty" runat="server" Checked='<%# Eval("Faculty") %>' Enabled="false"  />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkFaculty" runat="server" Checked='<%# Eval("Faculty") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Counsellor">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblCounsellor" runat="server" Checked='<%# Eval("Counsellor") %>' Enabled="false"  />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkCounsellor" runat="server" Checked='<%# Eval("Counsellor") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HRr">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblHR" runat="server" Checked='<%# Eval("HR") %>' Enabled="false"  />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkHR" runat="server" Checked='<%# Eval("HR") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Admin">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblAdmin" runat="server" Checked='<%# Eval("Admin") %>' Enabled="false"  />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkAdmin" runat="server" Checked='<%# Eval("Admin") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Registrar">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblRegistrar" runat="server" Checked='<%# Eval("Registrar") %>' Enabled="false"  />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkRegistrar" runat="server" Checked='<%# Eval("Registrar") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="VC">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblVC" runat="server" Checked='<%# Eval("VC") %>'  Enabled="false" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkVC" runat="server" Checked='<%# Eval("VC") %>'  />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Staff">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblStafft" runat="server" Checked='<%# Eval("Staff") %>' Enabled="false"  />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkStaff" runat="server" Checked='<%# Eval("Staff") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Transport">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lbltransport" runat="server" Checked='<%# Eval("Transport") %>' Enabled="false"  />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chktransport" runat="server" Checked='<%# Eval("Transport") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Mgmt.">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblMgmt" runat="server" Checked='<%# Eval("[Management approval]") %>' Enabled="false"  />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkMgmt" runat="server" Checked='<%# Eval("[Management approval]") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="InActive" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblInActive" runat="server" Checked='<%# Eval("InActive") %>' Enabled="false"  />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkInActive" runat="server" Checked='<%# Eval("InActive") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                       <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                           <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/icon-edit.png" Height="32px" Width="32px"/>                          
                        </ItemTemplate>
                        <EditItemTemplate>
                           <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" ImageUrl="~/Images/icon-update.png" Height="32px" Width="32px"/>
                           <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/icon-Cancel.png" Height="32px" Width="32px"/>
                        </EditItemTemplate>
                    </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont" />
                            <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
                        </asp:GridView>
          
                        </td>
                    
                    </tr>
                </table>
         </div>
         </div>
     </fieldset>
</asp:Content>

