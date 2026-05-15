<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="NoDetain.aspx.cs" Inherits="Faculty_NoDetain" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../Scripts/jquery-1.9.1.min.js"></script>

   
 


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 

     <asp:UpdatePanel ID="mrak" runat="server">
        <ContentTemplate>


     <fieldset class="boxBodyInner">
                <table cellpadding="0px" cellspacing="0px">
                    <caption>
                        <br />

                        <tr>
                          
                            <td style="width: 120px;font-size:large;font:bold">
                                Academic Year
                                <asp:DropDownList ID="drpAcademicYear" Width="150px" Height="30px" runat="server" OnSelectedIndexChanged="drpAcademicYear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </td>
                            
                            <td style="width: 10px"></td>
                            <td style="width: 120px;font-size:large;font:bold">
                                College Name
                                <asp:DropDownList ID="drpColleganame"  Height="30px" runat="server" Width="150px" OnSelectedIndexChanged="drpColleganame_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </td>
                            <td style="width: 20px"></td>
                           
                            <td style="width: 120px;font-size:large;font:bold">
                                Program Name
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpCourse" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="drpCourse" runat="server" AutoPostBack="true" Height="30px" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged" Width="150px"></asp:DropDownList>

                            </td>
                            <td style="width: 20px"></td>
                         
                            <td style="width: 120px;font-size:large;font:bold">
                                Semester/Year
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="drpSemester" InitialValue="" ErrorMessage="**" ForeColor="Red"></asp:RequiredFieldValidator>
                            
                                <asp:DropDownList ID="drpSemester" runat="server" AutoPostBack="true" Height="30px" OnSelectedIndexChanged="drpSemester_SelectedIndexChanged" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 10px"></td>
                           
                            <td style="width: 120px;font-size:large;font:bold"> 
                                Course
                                <asp:DropDownList ID="ddlSubject" runat="server" AutoPostBack="true" Height="30px" Width="150px">
                                </asp:DropDownList>
                            </td>
                            
                            <td style="width: 10px"></td>
                            <td style="width: 140px;vertical-align:bottom">
                                <asp:Button ID="btnShow" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" OnClick="btnShow_Click" Height="30px" Width="90px" Text="SHOW" />

                            </td>
                            <td style="width: 20px"></td>
                            <td style="width: 140px;vertical-align:bottom">

                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px"  Width="140px" Text="EXPORT TO EXCEL" OnClick="btnSubmit_Click" />


                            </td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:Button ID="brnReject" runat="server" CssClass="btn-sm btn-primary btn-block" ValidationGroup="g1" Height="30px" Visible="false" Width="90px" Text="Reject" OnClick="brnReject_Click" />


                            </td>
                         

                        </tr>
                </table>

            <asp:GridView ID="GrdDetenee" runat="server" Style="margin-left: 0%; margin-right: 0%; width: 99%" Visible="true" AutoGenerateColumns="false"
                    CssClass="table table-striped table-bordered table-hover" AlternatingRowStyle-CssClass="danger" ShowFooter="true">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <%# Container.DataItemIndex +1 %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Enrollment No">
                            <ItemTemplate>
                                <asp:Label ID="lblEnrollment" runat="server" Text='<%# Bind("EnrollmentNo") %>'></asp:Label>
                                <asp:HiddenField ID="SNumber" runat="server" Value='<%# Eval("[Student No_]") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student Name">
                            <ItemTemplate>
                                <asp:Label ID="lblStudentName" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sem Year">
                            <ItemTemplate>
                                <asp:Label ID="lblSemester" runat="server" Text='<%# Bind("SemestYear") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Course">
                            <ItemTemplate>
                                <asp:Label ID="lblCourse" runat="server" Text='<%# Bind("Course") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject">
                            <ItemTemplate>
                                <asp:Label ID="lblSubject" runat="server" Text='<%# Bind("Subject") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reason">
                            <ItemTemplate>
                                <asp:Label ID="lblReason" runat="server" Text='<%# Bind("UnDetaineeRemark") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="%">
                            <ItemTemplate>
                                <asp:Label ID="lblObtainPer" runat="server" Text='<%# Bind("Percentage") %>'></asp:Label>
                                <asp:HiddenField ID="hdnperfrom" runat="server" Value='<%# Bind("PercentageFrom") %>' />
                                <asp:HiddenField ID="hdnperTo" runat="server" Value='<%# Bind("PercentageTo") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:TemplateField ControlStyle-Width="15px">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true"  Width="30px"   OnCheckedChanged="chkAll_CheckedChanged" />
                                Select All
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkStudent" runat="server" AutoPostBack="true"  Width="20px"   OnCheckedChanged="chkStudent_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                            

                    </Columns>
                    <EmptyDataTemplate>No Record To Display</EmptyDataTemplate>
                </asp:GridView>

           <div style="text-align: right; margin-right: 5%;">
                    <asp:Button ID="btnDetanie" Text="Submit"  OnClick="btnDetanie_Click" Visible="false" OnClientClick="if(!confirm('Are you want to submit'))return false;" class="btn btn-info btn" runat="server" />
                    <br />
                    <br />
                </div>
            </fieldset>
            </ContentTemplate>

           <Triggers>
          <asp:PostBackTrigger ControlID="btnSubmit" />
          </Triggers>

         </asp:UpdatePanel>
</asp:Content>

