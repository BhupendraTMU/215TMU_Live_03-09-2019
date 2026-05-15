<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="UserRoleMatrix.aspx.cs" Inherits="Faculty_UserRoleMatrix" %>

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
     <div style="width:100%">
        <div style="width:20%">
            <table id="tblAddCourse" runat="server" visible="false" >
                <tr>
                    <td>
                       <asp:Label ID="lblCourse" Text="COURSE :" runat="server"></asp:Label>
            <asp:DropDownList ID="ddlCourse" runat="server" Height="30px"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="g1" ControlToValidate="ddlCourse" 
                            InitialValue="" ErrorMessage="please select Course!" ForeColor="Red" ></asp:RequiredFieldValidator>
                        </td>
                    <td>
                        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" ValidationGroup="g1" Width="100px"/>
                    </td>
                    </tr>
                </table>
        </div>
          <asp:GridView ID="grdUserRoleMatrix" runat="server" Width="99%" AutoGenerateColumns="false" ShowFooter="true"  DataKeyNames="ID"
                    onrowcommand="grdUserRoleMatrix_RowCommand" 
                    onrowdeleting="grdUserRoleMatrix_RowDeleting" 
                    onrowupdating="grdUserRoleMatrix_RowUpdating" 
                    onrowcancelingedit="grdUserRoleMatrix_RowCancelingEdit" 
                    onrowediting="grdUserRoleMatrix_RowEditing"
                    HeaderStyle-BackColor="#4D4D4D"
                    HeaderStyle-ForeColor="White" OnRowDataBound="grdUserRoleMatrix_RowDataBound">
 
                               <AlternatingRowStyle BackColor="#F7F7F7" />
       
            <Columns>          
                    <asp:TemplateField HeaderText="CollegeCode" Visible="false" >
                        <ItemTemplate>
                            <asp:Label ID="lblCollegeCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Global Dimenison 1 Code") %>'></asp:Label>
                        </ItemTemplate>                        
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Course Code">
                        <ItemTemplate>
                            <asp:Label ID="lblCourseCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Course Code") %>'></asp:Label>
                        </ItemTemplate>
                       <%-- <EditItemTemplate>           
                            <asp:TextBox ID="txtEditCourseCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Course Code") %>' Width="100px"></asp:TextBox>           
                        </EditItemTemplate>--%>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlAddCourse" runat="server" Width="120px"  Height="28px"></asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>                  
                    <asp:TemplateField HeaderText="Principal">
                        <ItemTemplate>
                            <asp:Label ID="lblPrincipal" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Principal") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>           
                            <asp:TextBox ID="txtEditPrincipal" runat="server" Width="120px" Text='<%#DataBinder.Eval(Container.DataItem, "Principal") %>' ></asp:TextBox>           
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddPrincipal" runat="server" Width="120px"   placeholder="Principal"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="HOD">
                        <ItemTemplate>
                            <asp:Label ID="lblHOD" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "HOD") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>           
                            <asp:TextBox ID="txtEditHOD" runat="server" Width="120px" Text='<%#DataBinder.Eval(Container.DataItem, "HOD") %>' ></asp:TextBox>           
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddHOD" runat="server" Width="120px"   placeholder="HOD"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CourseCoOrdinator">
                        <ItemTemplate>
                            <asp:Label ID="lblCourseCoOrdinator" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Course Co-Ordinator") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>           
                            <asp:TextBox ID="txtEditCourseCoOrdinator" runat="server" Width="120px" Text='<%#DataBinder.Eval(Container.DataItem, "Course Co-Ordinator") %>'></asp:TextBox>           
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddCourseCoOrdinator" runat="server" Width="120px"   placeholder="Course Co-Ordinator"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="Proctor">
                        <ItemTemplate>
                            <asp:Label ID="lblProctor" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Proctor") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>           
                            <asp:TextBox ID="txtEditProctor" runat="server" Width="120px" Text='<%#DataBinder.Eval(Container.DataItem, "Proctor") %>'></asp:TextBox>           
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddProctor" runat="server" Width="120px"  placeholder="Proctor"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="EventCo-Ordinator">
                        <ItemTemplate>
                            <asp:Label ID="lblEventCoOrdinator" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Event Co-Ordinator") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>           
                            <asp:TextBox ID="txtEditEventCoOrdinator" runat="server" Width="120px" Text='<%#DataBinder.Eval(Container.DataItem, "Event Co-Ordinator") %>'></asp:TextBox>           
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddEventCoOrdinator" runat="server" Width="120px"  placeholder="Event Co-ordinator"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                 <asp:TemplateField HeaderText="Lab Incharger">
                        <ItemTemplate>
                            <asp:Label ID="lblLabIncharge" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Lab Incharge") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>           
                            <asp:TextBox ID="txtEditLabIncharge" runat="server" Width="120px" Text='<%#DataBinder.Eval(Container.DataItem, "Lab Incharge") %>'></asp:TextBox>           
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddLabIncharge" runat="server" Width="120px"  placeholder="Lab Incharge"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="Transport Approval">
                        <ItemTemplate>
                            <asp:Label ID="lblTransportApproval" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Transport Approval") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>           
                            <asp:TextBox ID="txtEditTransportApproval" runat="server" Width="120px" Text='<%#DataBinder.Eval(Container.DataItem, "Transport Approval") %>'></asp:TextBox>           
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddTransportApproval" runat="server" Width="120px"  placeholder="Transport Approval"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="Management approval">
                        <ItemTemplate>
                            <asp:Label ID="lblManagementapproval" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Management approval") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>           
                            <asp:TextBox ID="txtEditManagementapproval" runat="server" Width="120px" Text='<%#DataBinder.Eval(Container.DataItem, "Management approval") %>'></asp:TextBox>           
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddManagementapproval" runat="server" Width="120px"  placeholder="Managementapproval"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                  
                <asp:TemplateField HeaderText="ExamCo-Ordinator">
                        <ItemTemplate>
                            <asp:Label ID="lblExamCoapproval" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Exam Co-Ordinator") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>           
                            <asp:TextBox ID="txtEditExamapproval" runat="server" Width="120px" Text='<%#DataBinder.Eval(Container.DataItem, "Exam Co-Ordinator") %>'></asp:TextBox>           
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtExamapproval" runat="server" Width="120px"  placeholder="Exam Co-Ordinator"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                  <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                           <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/icon-edit.png" Height="32px" Width="32px"/>
                           <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" ImageUrl="~/Images/Delete.png" Height="32px" Width="32px"/>
                        </ItemTemplate>
                        <EditItemTemplate>
                           <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" ImageUrl="~/Images/icon-update.png" Height="32px" Width="32px"/>
                           <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/icon-Cancel.png" Height="32px" Width="32px"/>
                        </EditItemTemplate>
                        <FooterTemplate>
                           <asp:ImageButton ID="lbtnAdd" runat="server" CommandName="ADD" Text="Add" Height="28" Width="70px" ImageUrl="~/images/Addc.jpg" BackColor="RosyBrown" ></asp:ImageButton> 
                        </FooterTemplate>
                    </asp:TemplateField>                


                    
  </Columns>
      <HeaderStyle BackColor="#ed7600" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" CssClass="cssGridheaderfont" />
                               <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                               <RowStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="cssGridheaderfont"   />
                               <SelectedRowStyle BackColor="#88dde3" Font-Bold="True" ForeColor="#F7F7F7" />
 </asp:GridView>
        </div>
            <%-- </ContentTemplate></asp:UpdatePanel>--%>
         </fieldset>
</asp:Content>

