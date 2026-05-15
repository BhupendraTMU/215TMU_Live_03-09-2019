<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="MapFacultySubject1.aspx.cs" Inherits="Faculty_MapFacultySubject1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function callFeedbackMessage(inputType, inputText) {

            if (inputType == 'Error') {
                alertify.error(inputText);
                return false;
            }
            else if (inputType == 'Success') {
                alertify.success(inputText);
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
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <fieldset class="boxBody">
                   <div class="pull-left"><asp:HiddenField runat="server" ID="hdnLoad" />
                <asp:Label ID="Label2" runat="server" Text="Assign Subject" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>&nbsp;&nbsp;
             
                    <asp:DropDownList ID="drpAcademicYear" runat="server"  AutoPostBack="true" Width="100px" Height="20px" OnSelectedIndexChanged="drpAcademicYear_SelectedIndexChanged"></asp:DropDownList>&nbsp;
                    <asp:DropDownList ID="drpOddEven" runat="server" AutoPostBack="true" Width="100px" Height="20px" OnSelectedIndexChanged="drpOddEven_SelectedIndexChanged">
                         <asp:ListItem Value="ODD" Text="ODD"></asp:ListItem>
                                <asp:ListItem Value="EVEN" Text="EVEN"></asp:ListItem>
                                <asp:ListItem Value="YEAR" Text="YEAR"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="pull-right">
                    <asp:CheckBox runat="server" ID="chkShowAllFaculty" Text="Show all faculty" AutoPostBack="true" OnCheckedChanged="chkShowAllFaculty_CheckedChanged" />
                </div>
            </fieldset>

            <fieldset class="boxBodyHeader">
            </fieldset>
            <fieldset style="background: #fefefe; border-top: 1px solid #dde0e8; border-bottom: 1px solid #dde0e8; padding: 10px 20px; height: 100%">
                <br />
                <fieldset class="boxBodyInner">
                    <div class="pull-right">
                        <table>
                            <tr>
                                <td>
                                     <asp:Label runat="server" ID="lblLoad" Text="Load"></asp:Label>
                                </td>
                                <td style="width:10px"></td>
                                <td>
                                    <asp:TextBox runat="server" Height="20px" Width="50px"  ID="txtLoad"  Enabled="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Font-Size="13px" runat="server" Display="Dynamic" ErrorMessage="Please assign the load first!" ValidationGroup="g1" ForeColor="Red" InitialValue="0" ControlToValidate="txtLoad"></asp:RequiredFieldValidator></td>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtLoad" FilterType="Numbers, Custom"></asp:FilteredTextBoxExtender>
                                </td>
                            </tr>
                        </table>
                       
                        <br />
                        
                    </div>
                    <center>
                        <br />
                        <br />

                        <table style="border-style: outset; border-width: thin; font: bold; font-size: 15px" width="95%">
                            <tr style="height: 10px; background-color: rgba(0, 0, 0, 0.06);">
                                <td colspan="17"></td>
                            </tr>
                            <tr style="height: 10px; background-color: rgba(0, 0, 0, 0.06);">
                                <td style="width: 10px"></td>
                                <td>Subject Group:</td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:DropDownList ID="drpSubjectGroup" runat="server" AutoPostBack="true" Width="150px" Height="20px" BackColor="#e8e8e8" OnSelectedIndexChanged="drpSubject_SelectedIndexChanged"></asp:DropDownList></td>
                                <td style="width: 10px"></td>
                                <td>Faculty:</td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:DropDownList ID="drpFaculty" runat="server" AutoPostBack="true" Width="150px" Height="20px" BackColor="#e8e8e8" OnSelectedIndexChanged="drpFaculty_SelectedIndexChanged"></asp:DropDownList></td>
                                <td style="width: 10px"></td>
                                <td>Course:</td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:DropDownList ID="drpCourse" runat="server" AutoPostBack="true" Width="150px" Height="20px" BackColor="#e8e8e8" OnSelectedIndexChanged="drpCourse_SelectedIndexChanged"></asp:DropDownList></td>
                                <td style="width: 10px"></td>
                                <td colspan="3">
                                    <%--<asp:TextBox ID="txtSubject" runat="server" Width="100%" Height="20px" BackColor="#e8e8e8"></asp:TextBox></td>--%>
                                <asp:DropDownList ID="DdlSubject" runat="server" AutoPostBack="true" Width="150px" Height="20px" OnSelectedIndexChanged="DdlSubject_SelectedIndexChanged" BackColor="#e8e8e8"></asp:DropDownList>
                                <td style="width: 70px"></td>
                            </tr>
                            <tr style="background-color: rgba(0, 0, 0, 0.06)">
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Size="13px" runat="server" Display="Dynamic" ErrorMessage="Please select the Subject!" ValidationGroup="g1" ForeColor="Red" ControlToValidate="drpSubjectGroup"></asp:RequiredFieldValidator></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Font-Size="13px" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="drpFaculty" ValidationGroup="g1" ErrorMessage="Please select the Faculty!"></asp:RequiredFieldValidator></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Font-Size="13px" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="drpCourse" ValidationGroup="g1" ErrorMessage="Please select the Course!"></asp:RequiredFieldValidator></td>

                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td style="width: 70px"></td>
                            </tr>
                            <tr style="height: 10px; background-color: rgba(0, 0, 0, 0.06);">
                                <td colspan="17"></td>
                            </tr>
                            <tr style="background-color: rgba(0, 0, 0, 0.06);">
                                <td></td>
                                <td>Semester/Year:</td>
                                <td></td>
                                <td>
                                    <asp:DropDownList ID="drpSemester" Width="150px" Height="20px" BackColor="#e8e8e8" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpSemester_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                                <td></td>
                                <td>Section</td>
                                <td></td>
                                <td>
                                    <asp:DropDownList ID="drpSection" Width="150px" Height="20px" BackColor="#e8e8e8" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpSection_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                                <td></td>
                                <td>Group </td>
                                <td></td>
                                <td>
                                    <asp:DropDownList ID="drpGroup" Width="150px" Height="20px" BackColor="#e8e8e8" runat="server"></asp:DropDownList>
                                </td>
                                <td></td>
                                <td>Batch</td>
                                <td></td>
                                <td>
                                    <asp:DropDownList ID="drpBatch" Width="150px" Height="20px" BackColor="#e8e8e8" runat="server"></asp:DropDownList>
                                </td>
                                <td>
                                    <center>
                                        <asp:LinkButton ID="btnAdd" ValidationGroup="g1" class="btn btn-info btn-sm" runat="server" Text="Add" OnClick="btnAdd_Click">
                                        </asp:LinkButton>&nbsp
                                    </center>
                                </td>
                            </tr>
                            <tr style="background-color: rgba(0, 0, 0, 0.06)">
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Font-Size="13px" runat="server" Display="Dynamic" ErrorMessage="Please select the Semester!" ValidationGroup="g1" ForeColor="Red" ControlToValidate="drpSemester"></asp:RequiredFieldValidator></td>
                                <td colspan="12"></td>
                                <td style="width: 70px"></td>
                            </tr>
                        </table>
                        <br />
                        <asp:GridView ID="grdFacultySubjectGroup" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                            GridLines="Both" EmptyDataText="There are no data records to display." OnRowCommand="grdFacultySubjectGroup_RowCommand">
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>
                                <asp:BoundField DataField="Subject" HeaderText="Subject" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Faculty" HeaderText="Faculty" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Course" HeaderText="Course" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Semester" HeaderText="Semester" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Section" HeaderText="Section" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Group1" HeaderText="Group" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Batch" HeaderText="Batch" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgdelete" runat="server" CommandName="DeleteItem" Text="Delete" Width="20px" Height="20px" />
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
                    </center>
                    <br />
                    <div class="pull-right">
                        <asp:Button ID="btnSave" class="btn btn-info btn-lg" runat="server" Text="Assign" OnClick="btnSave_Click" Visible="false" />
                    </div>
                    <br />
                    <br />
                    <center>
                           <asp:GridView ID="grdAssignSubjects" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                            GridLines="Both" EmptyDataText="There are no data records to display." OnRowCommand="grdAssignSubjects_RowCommand" DataKeyNames="Subject Code">
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>
                                <asp:BoundField DataField="Subject Code" HeaderText="Subject" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="SubjectGRoupDescription" HeaderText="Subject Group" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Faculty Code" HeaderText="Faculty" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Course Code" HeaderText="Course" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Semester" HeaderText="Semester" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Section" HeaderText="Section" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Student Group" HeaderText="Group" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Student Batch" HeaderText="Batch" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                  <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkdelete" runat="server" CommandName="DeleteItem" Text="Delete" Width="20px" Height="20px" />
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
                    </center>
                </fieldset>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

