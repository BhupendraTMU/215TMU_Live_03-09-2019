<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="CourseSubjectGroup.aspx.cs" Inherits="Faculty_CourseSubjectGroup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .completionList {
            border: solid 1px Gray;
            margin: 0px;
            padding: 3px;
            height: 140px;
            overflow: scroll;
            background-color: #FFFFFF;
        }

        .listItem {
            color: #191919;
        }

        .itemHighlighted {
            background-color: #ADD6FF;
        }

          
                            .pull-right.allow {
                            }

                                .pull-right.allow .form-check {
                                    float: left;
                                    width: 76px;
                                    margin-top: 10px;
                                }
                                .pull-right.allow .form-check input[type=checkbox]{
                                    margin-right:-8px;
                                }

                                .pull-right.allow .form-group {
                                    float: left;
                                    margin: 0px 8px;
                                }
                                .pull-right.allow .form-group .form-control{
                                    width:200px;
                                }

                                .pull-right.allow .btn-default {
                                    height: 34px;
                                }
                        
    </style>
    <script type="text/javascript" src="dropdowneditable/jquery.min.js"></script>
    <script type="text/javascript" src="dropdowneditable/jquery.searchabledropdown-1.0.8.min.js"></script>
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
<%--    <script>

        $(document).ready(function () {
            $("select").searchable();
        });

        $(document).ready(function () {
            $("#value").html($("#ddlSubject :selected").text() + " (VALUE: " + $("#ddlSubject").val() + ")");
            $("select").change(function () {
                $("#value").html(this.options[this.selectedIndex].text + " (VALUE: " + this.value + ")");
            });
        });

        function modifySelect() {
            $("select").get(0).selectedIndex = 5;
        }

        function appendSelectOption(str) {
            $("select").append("<option value=\"" + str + "\">" + str + "</option>");
        }

        function applyOptions() {
            $("select").searchable({
                maxListSize: $("#maxListSize").val(),
                maxMultiMatch: $("#maxMultiMatch").val(),
                latency: $("#latency").val(),
                exactMatch: $("#exactMatch").get(0).checked,
                wildcards: $("#wildcards").get(0).checked,
                ignoreCase: $("#ignoreCase").get(0).checked
            });
        }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <fieldset style="background: #fefefe; border-top: 1px solid #dde0e8; border-bottom: 1px solid #dde0e8; padding: 10px 20px; height: 100%">
        <fieldset class="boxBodyHeader">
            <asp:Label ID="Label1" runat="server" Text="Course Subject Group" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
        </fieldset>
        <asp:HiddenField ID="hdnValue" runat="server" />
        <br />
        <center>
            <%--  <asp:UpdatePanel runat="server">
                <ContentTemplate>--%>
            <asp:Panel ID="Panel1" runat="server" BorderColor="#E8E8E8" BorderWidth="1px" Width="100%">
                <fieldset class="boxBodyInner">

                    <table style="width: 100%">
                        <tr>
                            <td style="font-weight: bold">
                                <b>Academic Year:</b>
                            </td>

                            <td>
                         <asp:DropDownList ID="drpAcademicYear" runat="server"  AutoPostBack="true" Width="100px" Height="20px" OnSelectedIndexChanged="drpAcademicYear_SelectedIndexChanged"></asp:DropDownList>&nbsp;
                            </td>
                            <td style="font-weight: bold">
                                <b>Type:</b>
                            </td>

                            <td>
                      <asp:DropDownList runat="server" ID="drpYearType" OnSelectedIndexChanged="drpYearType_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="ODD" Text="ODD"></asp:ListItem>
                                    <asp:ListItem Value="EVEN" Text="EVEN"></asp:ListItem>
                                    <asp:ListItem Value="YEAR" Text="YEAR"></asp:ListItem>
                      </asp:DropDownList>
                            </td>
                            <td style="font-weight: bold">
                                <b>Subject Classification</b>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="drpSubjectClassification" OnSelectedIndexChanged="drpSubjectClassification_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="Lecture" Text="Lecture"></asp:ListItem>
                                    <asp:ListItem Value="PRACTICAL" Text="PRACTICAL"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 100px"></td>
                            <td style="font-weight: bold">
                                <b>Select Subject:</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSubject" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged" Width="300px" Height="25px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>

                    <br />
                    <div class="panel-body" id="pnlGrid" runat="server">
                        <asp:GridView ID="grdSunjectCourse" AutoGenerateColumns="false" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" GridLines="Horizontal" OnRowCommand="grdSunjectCourse_RowCommand">
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                    <HeaderTemplate>Select</HeaderTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Code" HeaderText="Subject Code" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Course" HeaderText="Course" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Semester" HeaderText="Semester" ItemStyle-CssClass="visible-lg" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="gdlbtnRemove" runat="server" Text="Delete" CommandName="Select" OnClientClick="return RemoveRow(this)"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderTemplate>Remove</HeaderTemplate>
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
                    <div>
                    </div>
                    <div class="pull-right">
                      
                        <div class="pull-right allow">
                            <div class="form-check">
                                <asp:CheckBox ID="chkAllow" class="form-check-input" runat="server" Text="" />
                                <asp:Label class="form-check-label" for="exampleCheck" runat="server" id="lblAllow">ALLOW</asp:Label>
                            </div>
                            <div class="form-group mb-2">
                                <asp:TextBox runat="server" ID="txtSubjectGroup" CssClass="form-control" placeholder="Group Name"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="g1" Display="Dynamic" ControlToValidate="txtSubjectGroup" InitialValue="" ErrorMessage="please enter the Group Name!" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <asp:Button runat="server" CssClass="btn btn-default" Text="Save" ID="btnSave" OnClick="btnSave_Click" ValidationGroup="g1" />
                        </div>
                        </div>
                        <br />
                        <br />
                        <br />
                        <asp:GridView ID="grdSubjectGroup" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None"
                            BorderWidth="1px" CellPadding="3" Width="100%" GridLines="Horizontal"  DataKeyNames="SubjectGroupCode"
                            EmptyDataText="There are no data records to display.">
                           <%-- OnRowDeleting="grdSubjectGroup_RowDeleting"--%>
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfId" runat="server" Value='<%# Eval("ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CourseCode" HeaderText="Course Code" SortExpression="ApplicantName" />
                                <asp:BoundField DataField="SubjectCode" HeaderText="Subject Code" SortExpression="ApplicantName" />
                                <asp:BoundField DataField="SubjectDescription" HeaderText="Subject Description" SortExpression="TotalAmount" DataFormatString="{0:N2}" />
                                <asp:BoundField DataField="SubjectGroup" HeaderText="Subject Group" SortExpression="ApplicantName" />
                             <%--   <asp:BoundField DataField="SubjectGroupCode" HeaderText="Subject Group Code" SortExpression="ApplicantName" />
                                <asp:CommandField ShowDeleteButton="True" HeaderText="Delete" ItemStyle-ForeColor="#ed7600" />--%>
                                   <asp:TemplateField HeaderText="Subject Group Code">
                                   <ItemTemplate>
                                       <asp:Label ID="SubjectGroupCode" Text='<%# Eval("SubjectGroupCode") %>' runat="server" ></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Academic Year">
                                   <ItemTemplate>
                                       <asp:Label ID="LblAcademicYear" Text='<%# Eval("[Academic Year]") %>' runat="server" ></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField>
                                   <ItemTemplate>
                                       <asp:LinkButton ID="LnkDelete" Text="Delete" runat="server" OnClick="LnkDelete_Click"></asp:LinkButton>
                                   </ItemTemplate>
                               </asp:TemplateField>
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
                </fieldset>
            </asp:Panel>
            <%--      </ContentTemplate>
            </asp:UpdatePanel>--%>
        </center>
        <asp:HiddenField runat="server" ID="hdnStudentNo" Value=" " />
    </fieldset>
</asp:Content>

