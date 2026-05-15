<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="S_S_PendingList.aspx.cs" Inherits="Faculty_S_S_PendingList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <fieldset class="boxBodyInner">
       <h2> Student Satisfaction Servey Pending List</h2>
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
                        <asp:DropDownList ID="drpSemester" runat="server" AutoPostBack="true" Height="20px" Width="120px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 10px"></td>





                    <td style="width: 10px"></td>
                    <td>
                        <asp:Button ID="btnShow" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" OnClick="btnShow_Click" Height="30px" Width="90px" Text="SHOW" />
                       
                    </td>

                    <td style="width: 10px"></td>
                    <td>
                        <asp:Button ID="btnexporttoexcel" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" OnClick="btnexporttoexcel_Click" Height="30px" Width="110px" Text="Export To Excel" />
                       
                    </td>
                   
                   

                </tr>
        </table>




    </fieldset>

    <div style="visibility:hidden">
    <asp:GridView ID="GridView1" runat="server" DataKeyNames="Enrollment No" AlternatingRowStyle-CssClass="danger" 
                PagerStyle-Font-Bold="true" PagerStyle-HorizontalAlign="Center" 
                AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" Visible="true">
                <Columns>
                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <%# Container.DataItemIndex +1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Enrolment No" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lblEnrollNo" runat="server" Text='<%# Bind("[Enrollment No]") %>'></asp:Label>
                            <asp:HiddenField ID="HfEnrollmentNo" Value='<%# Eval("[Enrollment No]") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                 
                    <%--OnClick="btnview_Click"--%>
                    <asp:TemplateField HeaderText="Name" ItemStyle-Width="4%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-left">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("Student Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course" ItemStyle-Width="4%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-left">
                        <ItemTemplate>
                            <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("Course") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course Name" ItemStyle-Width="5%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                        <ItemTemplate>
                            <asp:Label ID="lblcourseName" runat="server" Text='<%#Eval("Course Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Semester/Year" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("Semester") %>'></asp:Label>
                            <asp:HiddenField ID="HfSesterr" Value='<%# Eval("Semester") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                </Columns>
                <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
            </asp:GridView>
    </div>





    <fieldset class="boxBodyInner">
        <div class="text-center">
             <asp:HiddenField ID="HfEnrollment_No" runat="server" />
                                                <asp:HiddenField ID="HFsemester" runat="server" />
            <%--  OnPageIndexChanging="GrdExamList_PageIndexChanging"--%>
            <asp:GridView ID="GrdExamList" runat="server" DataKeyNames="Enrollment No" AlternatingRowStyle-CssClass="danger" PageSize="20"
                PagerStyle-Font-Bold="true" PagerStyle-HorizontalAlign="Center" AllowPaging="true" OnPageIndexChanging="GrdExamList_PageIndexChanging"
                AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" Visible="true">
                <Columns>
                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <%# Container.DataItemIndex +1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Enrolment No" ItemStyle-Width="3%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lblEnrollNo" runat="server" Text='<%# Bind("[Enrollment No]") %>'></asp:Label>
                            <asp:HiddenField ID="HfEnrollmentNo" Value='<%# Eval("[Enrollment No]") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                 
                    <%--OnClick="btnview_Click"--%>
                    <asp:TemplateField HeaderText="Name" ItemStyle-Width="4%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-left">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("Student Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course" ItemStyle-Width="4%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-left">
                        <ItemTemplate>
                            <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("Course") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course Name" ItemStyle-Width="5%" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                        <ItemTemplate>
                            <asp:Label ID="lblcourseName" runat="server" Text='<%#Eval("Course Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Semester/Year" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("Semester") %>'></asp:Label>
                            <asp:HiddenField ID="HfSesterr" Value='<%# Eval("Semester") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                </Columns>
                <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
            </asp:GridView>
        </div>
    </fieldset>

</asp:Content>

