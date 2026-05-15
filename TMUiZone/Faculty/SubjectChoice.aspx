<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="SubjectChoice.aspx.cs" Inherits="Faculty_SubjectChoice" %>

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
    <fieldset style="background: #fefefe; border-top: 1px solid #dde0e8; border-bottom: 1px solid #dde0e8; padding: 10px 20px; height: 100%">
        <fieldset class="boxBodyHeader">
            <asp:Label ID="Label1" runat="server" Text="Subject Choice" Font-Size="12pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
        </fieldset>
        <asp:HiddenField ID="hdnValue" runat="server" />
        <br />
        <center>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr>
                            <td style="font-weight: bold">
                                <b>Academic Year:</b>
                            </td>

                            <td>
                                <asp:DropDownList ID="drpAcademicYear" runat="server" AutoPostBack="true" Width="100px" Height="20px" OnSelectedIndexChanged="drpAcademicYear_SelectedIndexChanged"></asp:DropDownList>&nbsp;
                            </td>
                            <td style="font-weight: bold">
                                <b>Type:</b>
                            </td>

                            <td>
                                <asp:DropDownList runat="server" ID="drpYearType" AutoPostBack="true">
                                    <asp:ListItem Value="ODD" Text="ODD"></asp:ListItem>
                                    <asp:ListItem Value="EVEN" Text="EVEN"></asp:ListItem>
                                    <asp:ListItem Value="YEAR" Text="YEAR"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                             <td style="font-weight: bold">
                                <b>Subject</b>
                            </td>

                            <td>
                                  <asp:DropDownList runat="server" ID="drpGroupSubject"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>

                    <div class="pull-right">
                        <asp:Button runat="server" CssClass="btn-sm btn-primary btn-block" ID="btnSave" Text="Save" OnClick="btnSave_Click" />
                    </div>
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Panel ID="Panel1" runat="server">
                        <div class="table-responsive">
                            <asp:GridView ID="grdFacultySubject" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" PageSize="20"
                                DataKeyNames="SubjectGroupCode" EmptyDataText="There are no data records to display." OnRowDeleting="grdFacultySubject_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl. No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle Width="7%" />
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="SubjectGroupCode" HeaderText="SubjectGroupCode" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg">
                                        <HeaderStyle CssClass="visible-lg" />
                                        <ItemStyle CssClass="visible-lg" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SubjectGroupDescription" HeaderText="SubjectGroupDescription" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg">
                                        <HeaderStyle CssClass="visible-lg" />
                                        <ItemStyle CssClass="visible-lg" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CollegeCode" HeaderText="CollegeCode" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs">
                                        <HeaderStyle CssClass="hidden-xs" />
                                        <ItemStyle CssClass="hidden-xs" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SubjectClassification" HeaderText="SubjectClassification" SortExpression="Subject" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs">
                                        <HeaderStyle CssClass="hidden-xs" />
                                        <ItemStyle CssClass="hidden-xs" />
                                    </asp:BoundField>
                                    <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                                <AlternatingRowStyle CssClass="danger" />
                            </asp:GridView>

                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </fieldset>
</asp:Content>

