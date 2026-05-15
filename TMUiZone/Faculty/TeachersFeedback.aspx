<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="TeachersFeedback.aspx.cs" Inherits="Faculty_TeachersFeedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .Bold {
            font-weight: bold;
            background-color: #66D2D0;
        }

        .Color {
            background-color: #AFAFAF;
        }

        .algin {
            text-align: left;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlError" runat="server" Visible="false">
                <table style="width: 100%; text-align: center">
                    <tr>
                        <td>
                            <asp:Label ID="lblMsg" Font-Bold="true" Font-Size="Large" runat="server" Text="Syllabus Feedback is complete. "></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="Student" runat="server" Visible="false">
                <fieldset class="boxBody">
                    <asp:Label ID="Label3" runat="server"
                        Text="Syllabus Feedback" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>
                </fieldset>


                <table style="width: 100%;">
                    <tr>
                        <td style="height: 20px"></td>
                    </tr>
                    <tr>
                        <td style="width: 100%; height: 40px" align="center">
                            <img src="~/images/tmu_logo.png" id="Image1" runat="server" width="450" height="80" visible="true" />
                        </td>


                    </tr>
                    <tr>
                        <td style="height: 50px"></td>
                    </tr>
                    <tr>
                        <td style="width: 100%; padding-left: 25px" align="center">
                            <asp:Label ID="Label1" Text="Teachers’ Feedback – Syllabus" Font-Bold="true" Font-Underline="true" Font-Size="XX-Large" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%;">
                    <tr>
                        <td style="height: 20px"></td>
                    </tr>
                    <tr>
                        <td style="width: 70%;" align="center"></td>


                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="width: 80%;" align="center"></td>
                    </tr>

                </table>
                <table style="width: 100%;">
                    <tr>
                        <td style="border: 1px solid;width:250px" colspan="4" align="center">COLLEGE</td>
                        <td style="border: 1px solid;width:250px" colspan="4" align="center">PROGRAMME
                        </td>
                        <td style="border: 1px solid;width:250px" colspan="4" align="center">COURSE WITH CODE
                        </td>
                        <td style="border: 1px solid;width:245px"" colspan="4" align="center">SESSION & SEMESTER
                        </td>
                    </tr>
                    <tr style="margin-right: 250px; width: 100%">
                        <td style="border: 1px solid;width:250px" colspan="4"  align="center"  colspan="4">
                            <asp:Label ID="lblCLG" Text="CLG" Width="200px" runat="server"></asp:Label>
                        </td>
                        <td style="border: 1px solid;width:250px" colspan="4"  align="center">
                            <asp:DropDownList ID="drpProgram"  Width="300px" AutoPostBack="true" OnSelectedIndexChanged="drpProgram_SelectedIndexChanged" runat="server"></asp:DropDownList>
                        </td>
                        <td style="border: 1px solid;width:250px" colspan="4" align="center">
                            <asp:DropDownList ID="drpSession" Width="345px"  runat="server"></asp:DropDownList>
                           
                        </td>
                        <td style="border: 1px solid;width:245px"" colspan="4" align="center">
                             <asp:DropDownList ID="drpSubject" Width="350px"  runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td colspan="12" style="padding-left: 25px">
                            <strong>Directions:</strong> For each item please choose between 1 and 5 where:
                            <br />
                            <br />
                            (1 – strongly disagree, 2 - disagree, 3 – neither agree nor disagree, 4 – agree, 5 – strongly agree)
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>

                </table>
                <br />
                <table style="width: 100%;">

                    <tr>
                        <td colspan="12" style="padding-left: 25px">
                            <asp:HiddenField ID="hfcount" runat="server" />
                            <asp:HiddenField ID="hfcountYesNo" runat="server" />
                            <asp:GridView ID="grdData" runat="server" Style="width: 100%" Visible="true" AutoGenerateColumns="false"
                                CssClass="table table-striped table-bordered table-hover">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr. No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Teaching, Learning & Evaluation" HeaderStyle-Width="1500px" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuestion" Font-Bold="true" runat="server" Text='<%# Bind("[Question]") %>'></asp:Label>
                                            <asp:HiddenField ID="hfdQID" runat="server" Value='<%# Bind("[ID]") %>' />
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="1" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:RadioButton ID="chk1" runat="server" GroupName="mygroup"></asp:RadioButton>

                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="2" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:RadioButton ID="chk2" runat="server" GroupName="mygroup"></asp:RadioButton>

                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="3" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:RadioButton ID="chk3" runat="server" GroupName="mygroup"></asp:RadioButton>

                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="4" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>

                                            <asp:RadioButton ID="chk4" runat="server" GroupName="mygroup"></asp:RadioButton>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="5" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:RadioButton ID="chk5" runat="server" GroupName="mygroup"></asp:RadioButton>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle ForeColor="Black" BackColor="Transparent"></RowStyle>
                            </asp:GridView>




                        </td>
                    </tr>




                </table>
                <table style="width: 100%;">
                    <tr style="margin-right: 250px; width: 82%;">
                        <td style="padding-left: 25px">
                            <asp:TextBox ID="txtTopicP" placeholder="Topics proposed beyond the syllabus" Style="margin-left: 0px; " runat="server" TextMode="MultiLine" Width="580px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTopicT" placeholder="Topics obsolete & to be deleted" Style="margin-left: -25px; " runat="server" TextMode="MultiLine" Width="605px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%;">
                    <tr style="margin-right: 250px; width: 82%;">
                        <td style="padding-left: 25px">
                            <asp:TextBox ID="lblSuggestion" placeholder="Any further suggestion" Style="margin-left: 0px;" runat="server" TextMode="MultiLine" Width="1175px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <table style="width: 100%;">
                    <tr style="margin-right: 250px; width: 82%;">
                        <td align="left" style="padding-left: 25px">
                            <asp:Label ID="lblSig" runat="server" Font-Bold="true" Font-Size="Large" Font-Underline="true" Text="FACULTY NAME & SIGNATURE (WITH DATE)"></asp:Label>

                        </td>
                    </tr>
                </table>

                <div class="pull-right">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn-sm btn-primary btn-block" UseSubmitBehavior="false" Height="30px" Width="90px" OnClick="btnSubmit_Click" Text="SUBMIT" />

                </div>
                </fieldset>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />

</asp:Content>
