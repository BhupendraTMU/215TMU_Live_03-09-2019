<%@ Page Language="C#" MasterPageFile="~/Student/IndexMaster.master" AutoEventWireup="true" CodeFile="StudentFeedbackSyllabus.aspx.cs" Inherits="StudentFeedbackSyllabus" %>


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
    <script>
        function callFeedbackMessage(inputType, inputText) {

            if (inputType == 'Error') {
                alertify.error(inputText, 10000);
                return false;
            }
            else if (inputType == 'Success') {
                //alertify.confirm().set('overflow', false);
                alertify.success("Save Successfully", 1000);
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

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlError" runat="server" Visible="false">
                 <table style="width: 100%; text-align: center">
                    <tr>
                        <td>
                            <asp:Label ID="lblMsg" Font-Bold="true" Font-Size="Large" runat="server" Text="Syllabus Feedback is complete. " ></asp:Label>
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
                        <td style="width: 100%;" align="center">
                            <asp:Label Text="Students’ Feedback – Syllabus" Font-Bold="true" Font-Underline="true" Font-Size="XX-Large" runat="server"></asp:Label>
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
                        <td style="border: 1px solid; font-weight: bold" colspan="4" align="center">COLLEGE</td>
                        <td style="border: 1px solid; font-weight: bold" colspan="4" align="center">PROGRAMME
                        </td>
                        <td style="border: 1px solid; font-weight: bold" colspan="3" align="center">SESSION & SEMESTER
                        </td>
                    </tr>
                    <tr style="margin-right: 250px; width: 100%">
                        <td style="border: 1px solid" colspan="4" align="center">
                            <asp:Label ID="lblCLG" Text="CLG" runat="server"></asp:Label>
                        </td>
                        <td style="border: 1px solid" colspan="4" align="center">
                            <asp:Label ID="lblPRG" Text="PROG" runat="server"></asp:Label>
                        </td>
                        <td style="border: 1px solid" colspan="3" align="center">
                            <asp:Label ID="lblSession" Text="Session" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td colspan="11">
                            <strong>Directions:</strong> Please rate the courses on the parameters provided in the table using the 10 point scale shown below:
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px"></td>
                    </tr>
                    <tr style="margin-right: 250px; width: 80%">
                        <td style="border: 1px solid; font-weight: bold; width: 4px" align="center">10</td>
                        <td style="border: 1px solid; font-weight: bold; width: 4px" align="center">9</td>
                        <td style="border: 1px solid; font-weight: bold; width: 4px" align="center">8</td>
                        <td style="border: 1px solid; font-weight: bold; width: 4px" align="center">7</td>
                        <td style="border: 1px solid; font-weight: bold; width: 4px" align="center">6</td>
                        <td style="border: 1px solid; font-weight: bold; width: 4px" align="center">5</td>
                        <td style="border: 1px solid; font-weight: bold; width: 4px" align="center">4 </td>
                        <td style="border: 1px solid; font-weight: bold; width: 4px" align="center">3</td>
                        <td style="border: 1px solid; font-weight: bold; width: 4px" align="center">2</td>
                        <td style="border: 1px solid; font-weight: bold; width: 4px" align="center">1</td>
                        <td style="border: 1px solid; font-weight: bold; width: 4px" align="center">0</td>
                    </tr>
                    <tr style="margin-right: 250px; width: 80%">
                        <td style="border: 1px solid" colspan="2" align="center">
                            <asp:Label ID="lblVG" Text="Very Good" runat="server"></asp:Label>
                        </td>
                        <td style="border: 1px solid" colspan="2" align="center">
                            <asp:Label ID="lblG" Text="Good" runat="server"></asp:Label>
                        </td>
                        <td style="border: 1px solid" colspan="1" align="center">
                            <asp:Label ID="lblSat" Text="Satisfactory" runat="server"></asp:Label>
                        </td>
                        <td style="border: 1px solid; background-color: yellow" colspan="6" align="center">
                            <asp:Label ID="lblBel" Text="Belowthreshold" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Panel ID="pnlTextBoxes" runat="server" ScrollBars="Horizontal" Width="100%">

                    <br />
                    <asp:Table ID="FacultyAssessmentTable" runat="server" CssClass="table table-hover table-bordered"></asp:Table>
                </asp:Panel>
                <table style="width: 100%;">
                    <tr style="margin-right: 250px; width: 82%">
                        <td>
                            <asp:GridView ID="grdCourse" runat="server" Style="width: 100%" Visible="true" AutoGenerateColumns="false"
                                CssClass="table table-striped table-bordered table-hover" ShowHeader="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="Question" HeaderStyle-Width="300px" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCourse" Font-Bold="true" runat="server" Text='<%# Bind("[course]") %>'></asp:Label>

                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" HeaderStyle-Width="300px" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCode" Font-Bold="true" runat="server" Text='<%# Bind("[Subject code]") %>'></asp:Label>

                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" HeaderStyle-Width="300px" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" Font-Bold="true" runat="server" Text='<%# Bind("[Subject Description]") %>'></asp:Label>

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
                        <td>
                            <asp:TextBox ID="lblSuggestion" placeholder="Suggestion for further Improvements" Style="margin-left: 0px; margin-top: -20px;" runat="server" TextMode="MultiLine" Width="1200px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <table style="width: 100%;">
                    <tr style="margin-right: 250px; width: 82%;">
                        <td align="left">
                            <asp:Label ID="lblSig" runat="server" Font-Bold="true" Font-Size="Large" Font-Underline="true" Text=" STUDENT NAME & SIGNATURE (WITH DATE)"></asp:Label>

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

