<%@ Page Title="" Language="C#" MasterPageFile="~/Faculty/IndexMaster.master" AutoEventWireup="true" CodeFile="RemedialClass.aspx.cs" Inherits="Faculty_RemedialClass" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('input[type="checkbox"]').click(function () {


                if ($(this).prop("checked") == true) {
                    document.getElementById('<%= drpExammethod.ClientID %>').disabled = false;



                }
                else if ($(this).prop("checked") == false) {
                    document.getElementById('<%= drpExammethod.ClientID %>').disabled = true;

                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="rr" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <fieldset class="boxBody">
                <asp:Label ID="Label1" runat="server"
                    Text="Remedial Classes /Additional / Extra" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

            </fieldset>

            <table cellpadding="0px" cellspacing="0px">
                <tr>
                    <td>

                        <table cellpadding="0px" cellspacing="0px" style="width: 100%">
                            <tr>
                                <td style="width: 250px; text-align: left">
                                    <fieldset class="boxBody">
                                        <asp:Label ID="Label2" runat="server"
                                            Text="Create New" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                                    </fieldset>
                                </td>
                                <td style="width: 150px; text-align: left">
                                    <asp:CheckBox ID="chkevalution" Font-Bold="true" Width="160px" OnCheckedChanged="chkevalution_CheckedChanged" AutoPostBack="true" Text="Re Evaluation Class" runat="server" />
                                    <asp:Label ID="lblmethod" runat="server" Text="Exam Method"></asp:Label>


                                    <asp:DropDownList ID="drpExammethod" runat="server" OnSelectedIndexChanged="drpExammethod_SelectedIndexChanged" AutoPostBack="true" Enabled="false" Height="20px" Width="120px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>


                <tr>
                    <td>

                        <table cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td colspan="20" style="height: 10px"></td>
                            </tr>



                            <tr>
                                <td style="width: 10px"></td>
                                <td>Course  </td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:DropDownList ID="ddCourse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddCourse_SelectedIndexChanged" Width="140px" Height="28px"></asp:DropDownList>
                                </td>
                                <td style="width: 10px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddCourse" ErrorMessage="**" SetFocusOnError="True" ValidationGroup="remclass" InitialValue="--Course--"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Label ID="lblSemester" runat="server" Text="Semester"></asp:Label><asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>
                                </td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:DropDownList ID="ddSemester_Year" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddSemester_Year_SelectedIndexChanged" Height="28px" Width="90px"></asp:DropDownList>
                                </td>
                                <td style="width: 10px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddSemester_Year" ErrorMessage="**" SetFocusOnError="True" ValidationGroup="remclass"></asp:RequiredFieldValidator>
                                </td>
                                <td>Section  </td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:DropDownList ID="ddSection" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddSection_SelectedIndexChanged" Height="28px" Width="90px"></asp:DropDownList>
                                </td>
                                <td style="width: 10px">&nbsp;</td>
                                <td>Subject </td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:DropDownList ID="ddSubject" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddSubject_SelectedIndexChanged" Width="100px" Height="28px"></asp:DropDownList>
                                </td>
                                <td style="width: 10px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddSubject" ErrorMessage="**" SetFocusOnError="True" ValidationGroup="remclass"></asp:RequiredFieldValidator></td>


                                <td>Subject Type</td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtEnddate" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                    <asp:TextBox ID="txtsubjecttype" runat="server" Width="90px" Enabled="False"></asp:TextBox>


                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtsubjecttype" ErrorMessage="**" SetFocusOnError="True" ValidationGroup="remclass"></asp:RequiredFieldValidator>
                                </td>


                            </tr>
                            <tr>
                                <td colspan="20" style="height: 10px"></td>
                            </tr>

                            <tr>
                                <td style="width: 10px"></td>
                                <td></td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:Label ID="lblPortalid" runat="server" Visible="False"></asp:Label></td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Start Date"></asp:Label>
                                </td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:TextBox ID="txtStartdate" runat="server" Width="90px" onkeydown="return false;" autocomplete="off" oncopy="return false" onpaste="return false" oncut="return false" oncontextmenu="return false" AutoPostBack="True" OnTextChanged="txtStartdate_TextChanged"></asp:TextBox>
                                    <asp:CalendarExtender ID="clndAppliedate" runat="server" TargetControlID="txtStartdate" Format="dd MMM yyyy"></asp:CalendarExtender>
                               
                                     </td>
                                <td style="width: 10px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtStartdate" ErrorMessage="**" SetFocusOnError="True" ValidationGroup="remclass"></asp:RequiredFieldValidator>
                                </td>
                                <td>End Date</td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:TextBox ID="txtEnddate" runat="server" Width="90px" onkeydown="return false;" oncopy="return false" onpaste="return false" oncut="return false" autocomplete="off" oncontextmenu="return false" AutoPostBack="True" OnTextChanged="txtEnddate_TextChanged"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEnddate" Format="dd MMM yyyy"></asp:CalendarExtender>
                                
                                
                                </td>
                                <td style="width: 10px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtEnddate" ErrorMessage="**" SetFocusOnError="True" ValidationGroup="remclass"></asp:RequiredFieldValidator>
                                </td>
                                <td>Hour No From  </td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:DropDownList ID="ddhournofrom" runat="server" Height="28px" Width="100px" OnSelectedIndexChanged="ddhournofrom_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Selected="True"></asp:ListItem>
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                        <asp:ListItem>6</asp:ListItem>
                                        <asp:ListItem>7</asp:ListItem>
                                        <asp:ListItem>8</asp:ListItem>
                                        <asp:ListItem>9</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 10px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddhournofrom" ErrorMessage="**" SetFocusOnError="True" ValidationGroup="remclass"></asp:RequiredFieldValidator>
                                </td>
                                <td>Till </td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:DropDownList ID="ddhourTillNo" runat="server" Height="28px" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="ddhourTillNo_SelectedIndexChanged">
                                        <asp:ListItem Selected="True"></asp:ListItem>
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                        <asp:ListItem>6</asp:ListItem>
                                        <asp:ListItem>7</asp:ListItem>
                                        <asp:ListItem>8</asp:ListItem>
                                        <asp:ListItem>9</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddhourTillNo" ErrorMessage="**" SetFocusOnError="True" ValidationGroup="remclass"></asp:RequiredFieldValidator>
                                </td>

                            </tr>

                            <tr>
                                <td colspan="20" style="height: 10px"></td>
                            </tr>


                            <tr>
                                <td style="width: 10px"></td>
                                <td></td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:Label ID="lblTypeofCourse" runat="server" Text="Label" Visible="False"></asp:Label>
                                    <asp:Label ID="lblEntryNo" runat="server"></asp:Label>
                                    <asp:Label ID="lblSemesateryear" runat="server" Visible="False"></asp:Label>
                                </td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Academic Year"></asp:Label>
                                </td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtStartdate" Format="dd MMM yyyy"></asp:CalendarExtender>
                                    <asp:DropDownList ID="ddAcademicyrs" runat="server" AutoPostBack="True" Height="28px" Width="90px" OnSelectedIndexChanged="ddAcademicyrs_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                                <td style="width: 10px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddAcademicyrs" ErrorMessage="**" SetFocusOnError="True" ValidationGroup="remclass"></asp:RequiredFieldValidator>
                                </td>
                                <td>Sub. Classfi.</td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:DropDownList ID="ddSubClassification" runat="server" Enabled="false" Height="28px" Width="90px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddSubClassification" ErrorMessage="**" SetFocusOnError="True" ValidationGroup="remclass"></asp:RequiredFieldValidator>
                                </td>

                                <td style="width: 10px"></td>

                                <td>Room Allocation</td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:DropDownList ID="ddRoomAllocation" runat="server" Height="28px" Width="100px">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 10px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddRoomAllocation" ErrorMessage="**" SetFocusOnError="True" ValidationGroup="remclass" InitialValue="--Room--"></asp:RequiredFieldValidator>
                                </td>

                                <td>Faculty</td>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:DropDownList ID="ddFaculty" runat="server" OnSelectedIndexChanged="ddFaculty_SelectedIndexChanged" Width="250px" Height="28px"></asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddFaculty" ErrorMessage="**" SetFocusOnError="True" ValidationGroup="remclass" InitialValue="--Faculty--"></asp:RequiredFieldValidator>

                                </td>


                            </tr>
                            <tr>
                                <td colspan="20" style="height: 10px"></td>
                            </tr>



                        </table>

                    </td>
                </tr>




                <tr>
                    <td>

                        <table cellpadding="0px" cellspacing="0px" style="width: 100%">
                            <tr>
                                <td style="width: 10px"></td>
                                <td>
                                    <fieldset class="boxBody">
                                        <asp:Label ID="Label4" runat="server"
                                            Text="Student List" Font-Size="15pt" ForeColor="#093A62" Font-Names="&quot;Georgia&quot;,&quot;Times new roman&quot;,&quot;Helvetica Neue&quot;"></asp:Label>

                                    </fieldset>
                                </td>
                            </tr>

                            <tr>
                                <td style="height: 10px" colspan="3"></td>
                            </tr>

                            <tr>
                                <td colspan="2" align="right">
                                    <asp:Button ID="btnselectchecked" runat="server" OnClick="btnselectchecked_Click" Text="Select All" Visible="False" />
                                    <asp:Button ID="btnuncheked" runat="server" Text="UN Checked All" OnClick="btnuncheked_Click" Visible="False" />
                                </td>
                                <td></td>
                            </tr>

                            <tr>
                                <td style="width: 10px"></td>
                                <td>
                                    <asp:GridView ID="grdListofStudent" runat="server" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None" CssClass="table table-striped table-bordered table-hover" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl. No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Student No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStudentNo" runat="server" Text='<%#Bind("[Student No_]") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Enrollment No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEnrollMentNo" runat="server" Text='<%#Bind("[Enrollment No]") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStudentName" runat="server" Text='<%#Bind("[Student Name]") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkID" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <EmptyDataTemplate>
                                            There are no record found..
                                        </EmptyDataTemplate>
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#ff9900" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </td>
                                <td style="width: 10px"></td>
                            </tr>
                        </table>
                    </td>
                </tr>


                <tr>
                    <td style="height: 10px"></td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button ID="btnBack" runat="server" Text="View Already Created" CssClass="btn" OnClick="btnBack_Click" />
                        &nbsp;&nbsp;&nbsp; 
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn" OnClick="btnSave_Click" ValidationGroup="remclass" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                </tr>

                <tr>
                    <td style="height: 90px"></td>
                </tr>
                <%-- <tr> <td style="height:10px">  </td></tr>--%>
            </table>
        </ContentTemplate>

    </asp:UpdatePanel>


</asp:Content>

